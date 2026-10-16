#pragma once
#ifdef __ANDROID__
#include <media/NdkMediaCodec.h>
#include <media/NdkMediaExtractor.h>
#include <media/NdkMediaFormat.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>
#include <algorithm>
#include <cmath>
#include <cstdint>
#include <cstring>
#include <string>
#include <vector>

namespace BetterEndfieldNext::MediaScreen {

class AndroidVideoDecoder {
public:
    AndroidVideoDecoder() = default;
    ~AndroidVideoDecoder() { Close(); }

    AndroidVideoDecoder(const AndroidVideoDecoder&) = delete;
    AndroidVideoDecoder& operator=(const AndroidVideoDecoder&) = delete;

    bool Open(const std::string& path) {
        Close();
        fd_ = open(path.c_str(), O_RDONLY);
        if (fd_ < 0) return false;

        struct stat st{};
        if (fstat(fd_, &st) != 0) {
            Close();
            return false;
        }
        off64_t file_size = st.st_size;

        extractor_ = AMediaExtractor_new();
        if (!extractor_) {
            Close();
            return false;
        }

        media_status_t err = AMediaExtractor_setDataSourceFd(extractor_, fd_, 0, file_size);
        if (err != AMEDIA_OK) {
            Close();
            return false;
        }

        size_t num_tracks = AMediaExtractor_getTrackCount(extractor_);
        int video_track = -1;
        const char* mime = nullptr;
        AMediaFormat* track_format = nullptr;
        for (size_t i = 0; i < num_tracks; ++i) {
            AMediaFormat* fmt = AMediaExtractor_getTrackFormat(extractor_, i);
            const char* m = nullptr;
            if (AMediaFormat_getString(fmt, AMEDIAFORMAT_KEY_MIME, &m) && m) {
                if (strncmp(m, "video/", 6) == 0) {
                    video_track = static_cast<int>(i);
                    mime = m;
                    track_format = fmt;
                    break;
                }
            }
            AMediaFormat_delete(fmt);
        }

        if (video_track < 0 || !mime || !track_format) {
            Close();
            return false;
        }

        AMediaExtractor_selectTrack(extractor_, video_track);

        width_ = 0;
        height_ = 0;
        duration_us_ = 0;
        AMediaFormat_getInt32(track_format, AMEDIAFORMAT_KEY_WIDTH, &width_);
        AMediaFormat_getInt32(track_format, AMEDIAFORMAT_KEY_HEIGHT, &height_);
        AMediaFormat_getInt64(track_format, AMEDIAFORMAT_KEY_DURATION, &duration_us_);

        crop_left_ = 0;
        crop_top_ = 0;
        crop_width_ = width_;
        crop_height_ = height_;
        stride_ = width_;
        slice_height_ = height_;
        color_format_ = 0;

        codec_ = AMediaCodec_createDecoderByType(mime);
        if (!codec_) {
            AMediaFormat_delete(track_format);
            Close();
            return false;
        }

        err = AMediaCodec_configure(codec_, track_format, nullptr, nullptr, 0);
        AMediaFormat_delete(track_format);
        if (err != AMEDIA_OK) {
            Close();
            return false;
        }

        err = AMediaCodec_start(codec_);
        if (err != AMEDIA_OK) {
            Close();
            return false;
        }

        if (crop_width_ > 0 && crop_height_ > 0) {
            rgba_buffer_.assign(static_cast<size_t>(crop_width_) * crop_height_ * 4, 0);
        }
        current_pts_us_ = -1;
        input_eos_ = false;
        output_eos_ = false;
        opened_ = true;
        return true;
    }

    void Close() {
        if (codec_) {
            AMediaCodec_stop(codec_);
            AMediaCodec_delete(codec_);
            codec_ = nullptr;
        }
        if (extractor_) {
            AMediaExtractor_delete(extractor_);
            extractor_ = nullptr;
        }
        if (fd_ >= 0) {
            close(fd_);
            fd_ = -1;
        }
        width_ = 0;
        height_ = 0;
        crop_width_ = 0;
        crop_height_ = 0;
        crop_left_ = 0;
        crop_top_ = 0;
        stride_ = 0;
        slice_height_ = 0;
        duration_us_ = 0;
        current_pts_us_ = -1;
        input_eos_ = false;
        output_eos_ = false;
        opened_ = false;
        rgba_buffer_.clear();
    }

    void Seek(double target_seconds) {
        if (!opened_ || !extractor_ || !codec_) return;
        int64_t target_us = static_cast<int64_t>(std::max(0.0, target_seconds) * 1000000.0);
        AMediaExtractor_seekTo(extractor_, target_us, AMEDIAEXTRACTOR_SEEK_CLOSEST_SYNC);
        AMediaCodec_flush(codec_);
        input_eos_ = false;
        output_eos_ = false;
        current_pts_us_ = -1;
    }

    bool AdvanceTo(double target_seconds, const uint8_t*& out_rgba, bool& fresh) {
        fresh = false;
        out_rgba = nullptr;
        if (!opened_ || !extractor_ || !codec_) return false;

        int64_t target_us = static_cast<int64_t>(std::max(0.0, target_seconds) * 1000000.0);

        if (current_pts_us_ >= 0 && (target_us < current_pts_us_ - 200000 || target_us > current_pts_us_ + 2000000)) {
            Seek(target_seconds);
        }

        while (!input_eos_) {
            ssize_t in_idx = AMediaCodec_dequeueInputBuffer(codec_, 0);
            if (in_idx < 0) break;

            size_t in_size = 0;
            uint8_t* buf = AMediaCodec_getInputBuffer(codec_, in_idx, &in_size);
            if (!buf) {
                AMediaCodec_queueInputBuffer(codec_, in_idx, 0, 0, 0, 0);
                break;
            }

            ssize_t sample_size = AMediaExtractor_readSampleData(extractor_, buf, in_size);
            if (sample_size < 0) {
                input_eos_ = true;
                AMediaCodec_queueInputBuffer(codec_, in_idx, 0, 0, 0, AMEDIACODEC_BUFFER_FLAG_END_OF_STREAM);
                break;
            }

            int64_t pts = AMediaExtractor_getSampleTime(extractor_);
            AMediaCodec_queueInputBuffer(codec_, in_idx, 0, sample_size, pts, 0);
            AMediaExtractor_advance(extractor_);
        }

        AMediaCodecBufferInfo info{};
        bool updated = false;
        for (int drain_iter = 0; drain_iter < 8; ++drain_iter) {
            if (current_pts_us_ >= target_us && (current_pts_us_ - target_us) < 33000) {
                break;
            }

            ssize_t out_idx = AMediaCodec_dequeueOutputBuffer(codec_, &info, 0);
            if (out_idx == AMEDIACODEC_INFO_TRY_AGAIN_LATER) {
                break;
            }
            if (out_idx == AMEDIACODEC_INFO_OUTPUT_FORMAT_CHANGED) {
                AMediaFormat* out_fmt = AMediaCodec_getOutputFormat(codec_);
                UpdateFormat(out_fmt);
                AMediaFormat_delete(out_fmt);
                continue;
            }
            if (out_idx == AMEDIACODEC_INFO_OUTPUT_BUFFERS_CHANGED) {
                continue;
            }
            if (out_idx >= 0) {
                if (info.flags & AMEDIACODEC_BUFFER_FLAG_END_OF_STREAM) {
                    output_eos_ = true;
                }
                if (info.size > 0) {
                    size_t out_size = 0;
                    uint8_t* out_buf = AMediaCodec_getOutputBuffer(codec_, out_idx, &out_size);
                    if (out_buf) {
                        bool skip_convert = (info.presentationTimeUs + 40000 < target_us) && (drain_iter < 7);
                        if (!skip_convert) {
                            ConvertFrame(out_buf, info);
                            current_pts_us_ = info.presentationTimeUs;
                            updated = true;
                        }
                    }
                }
                AMediaCodec_releaseOutputBuffer(codec_, out_idx, false);
                if (current_pts_us_ >= target_us) {
                    break;
                }
            }
        }

        if (updated) {
            fresh = true;
            out_rgba = rgba_buffer_.data();
            return true;
        }

        if (current_pts_us_ >= 0 && !rgba_buffer_.empty()) {
            fresh = false;
            out_rgba = rgba_buffer_.data();
            return true;
        }

        return false;
    }

    int32_t CropWidth() const { return crop_width_ > 0 ? crop_width_ : width_; }
    int32_t CropHeight() const { return crop_height_ > 0 ? crop_height_ : height_; }
    double Duration() const { return duration_us_ > 0 ? static_cast<double>(duration_us_) / 1000000.0 : 0.0; }
    bool IsOpened() const { return opened_; }

private:
    void UpdateFormat(AMediaFormat* fmt) {
        if (!fmt) return;
        int32_t val = 0;
        if (AMediaFormat_getInt32(fmt, AMEDIAFORMAT_KEY_WIDTH, &val) && val > 0) width_ = val;
        if (AMediaFormat_getInt32(fmt, AMEDIAFORMAT_KEY_HEIGHT, &val) && val > 0) height_ = val;

        stride_ = width_;
        slice_height_ = height_;
        if (AMediaFormat_getInt32(fmt, AMEDIAFORMAT_KEY_STRIDE, &val) && val > 0) stride_ = val;
        if (AMediaFormat_getInt32(fmt, AMEDIAFORMAT_KEY_SLICE_HEIGHT, &val) && val > 0) slice_height_ = val;
        if (stride_ < width_) stride_ = width_;
        if (slice_height_ < height_) slice_height_ = height_;

        if (AMediaFormat_getInt32(fmt, AMEDIAFORMAT_KEY_COLOR_FORMAT, &val)) color_format_ = val;

        int32_t c_left = 0, c_top = 0, c_right = 0, c_bottom = 0;
        bool has_crop = AMediaFormat_getInt32(fmt, "crop-left", &c_left) &&
                        AMediaFormat_getInt32(fmt, "crop-top", &c_top) &&
                        AMediaFormat_getInt32(fmt, "crop-right", &c_right) &&
                        AMediaFormat_getInt32(fmt, "crop-bottom", &c_bottom);
        if (has_crop && c_right > c_left && c_bottom > c_top) {
            crop_left_ = c_left;
            crop_top_ = c_top;
            crop_width_ = c_right - c_left + 1;
            crop_height_ = c_bottom - c_top + 1;
        } else {
            crop_left_ = 0;
            crop_top_ = 0;
            crop_width_ = width_;
            crop_height_ = height_;
        }

        if (crop_width_ > 0 && crop_height_ > 0) {
            rgba_buffer_.resize(static_cast<size_t>(crop_width_) * crop_height_ * 4);
        }
    }

    void ConvertFrame(const uint8_t* src, const AMediaCodecBufferInfo& info) {
        const int w = crop_width_ > 0 ? crop_width_ : width_;
        const int h = crop_height_ > 0 ? crop_height_ : height_;
        if (w <= 0 || h <= 0) return;

        size_t required = static_cast<size_t>(w) * h * 4;
        if (rgba_buffer_.size() != required) {
            rgba_buffer_.resize(required);
        }

        const uint8_t* y_plane = src + info.offset;
        const uint8_t* uv_plane = y_plane + (stride_ * slice_height_);

        bool is_nv21 = (color_format_ == 0x11);
        bool is_i420 = (color_format_ == 19 || color_format_ == 20);

        for (int y = 0; y < h; ++y) {
            int src_y = crop_top_ + y;
            // 垂直翻转适配 Unity Texture2D 坐标系（原点在左下角）
            int dst_y = h - 1 - y;

            const uint8_t* py = y_plane + (src_y * stride_) + crop_left_;
            uint8_t* pdst = rgba_buffer_.data() + (static_cast<size_t>(dst_y) * w * 4);

            int uv_y = src_y / 2;
            if (is_i420) {
                int u_stride = (stride_ + 1) / 2;
                const uint8_t* pu = uv_plane + (uv_y * u_stride) + (crop_left_ / 2);
                const uint8_t* pv = pu + ((stride_ / 2) * (slice_height_ / 2));
                for (int x = 0; x < w; ++x) {
                    int Y = py[x];
                    int U = pu[x / 2];
                    int V = pv[x / 2];

                    int c = Y - 16;
                    if (c < 0) c = 0;
                    int d = U - 128;
                    int e = V - 128;

                    pdst[x * 4 + 0] = static_cast<uint8_t>(std::clamp((298 * c + 409 * e + 128) >> 8, 0, 255));
                    pdst[x * 4 + 1] = static_cast<uint8_t>(std::clamp((298 * c - 100 * d - 208 * e + 128) >> 8, 0, 255));
                    pdst[x * 4 + 2] = static_cast<uint8_t>(std::clamp((298 * c + 516 * d + 128) >> 8, 0, 255));
                    pdst[x * 4 + 3] = 255;
                }
            } else {
                const uint8_t* puv = uv_plane + (uv_y * stride_) + (crop_left_ & ~1);
                for (int x = 0; x < w; ++x) {
                    int Y = py[x];
                    int u_idx = (x / 2) * 2;
                    int U = is_nv21 ? puv[u_idx + 1] : puv[u_idx];
                    int V = is_nv21 ? puv[u_idx] : puv[u_idx + 1];

                    int c = Y - 16;
                    if (c < 0) c = 0;
                    int d = U - 128;
                    int e = V - 128;

                    pdst[x * 4 + 0] = static_cast<uint8_t>(std::clamp((298 * c + 409 * e + 128) >> 8, 0, 255));
                    pdst[x * 4 + 1] = static_cast<uint8_t>(std::clamp((298 * c - 100 * d - 208 * e + 128) >> 8, 0, 255));
                    pdst[x * 4 + 2] = static_cast<uint8_t>(std::clamp((298 * c + 516 * d + 128) >> 8, 0, 255));
                    pdst[x * 4 + 3] = 255;
                }
            }
        }
    }

    int fd_ = -1;
    AMediaExtractor* extractor_ = nullptr;
    AMediaCodec* codec_ = nullptr;

    int32_t width_ = 0;
    int32_t height_ = 0;
    int32_t crop_left_ = 0;
    int32_t crop_top_ = 0;
    int32_t crop_width_ = 0;
    int32_t crop_height_ = 0;
    int32_t stride_ = 0;
    int32_t slice_height_ = 0;
    int32_t color_format_ = 0;
    int64_t duration_us_ = 0;

    int64_t current_pts_us_ = -1;
    bool input_eos_ = false;
    bool output_eos_ = false;
    bool opened_ = false;

    std::vector<uint8_t> rgba_buffer_;
};

} // namespace BetterEndfieldNext::MediaScreen
#endif
