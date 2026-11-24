#pragma once
// Ogg Vorbis decoding for the Windows music module.
//
// Media Foundation has no in-box Vorbis decoder: Windows' own Media Player
// plays .ogg through the packaged Web Media Extensions (FFmpeg), which a
// classic Win32 process cannot activate - MFCreateSourceReaderFromURL returns
// MF_E_UNSUPPORTED_BYTESTREAM_TYPE for the same file. The module therefore
// decodes Vorbis itself and feeds the existing resampler and ring buffer.
//
// Only Ogg Vorbis is handled here; .opus still needs libopus and is rejected.
#include <algorithm>
#include <cstdio>
#include <string>
#include <vector>

#define STB_VORBIS_HEADER_ONLY
#include "../../shared/third_party/stb_vorbis/stb_vorbis.c"

namespace BetterEndfieldNext::Music::Ogg {

class Track {
public:
    Track() = default;
    ~Track() { Close(); }
    Track(const Track&) = delete;
    Track& operator=(const Track&) = delete;

    bool Open(const std::wstring& path, std::string& message) {
        Close();
#if defined(_MSC_VER)
        FILE* opened = nullptr;
        if (_wfopen_s(&opened, path.c_str(), L"rb") != 0) opened = nullptr;
        file_ = opened;
#else
        file_ = _wfopen(path.c_str(), L"rb");
#endif
        if (!file_) {
            message = "music file could not be opened";
            return false;
        }
        int error = 0;
        vorbis_ = stb_vorbis_open_file(file_, 0, &error, nullptr);
        if (!vorbis_) {
            message = "Ogg Vorbis cannot be decoded (error=" + std::to_string(error) + ")";
            Close();
            return false;
        }
        const stb_vorbis_info info = stb_vorbis_get_info(vorbis_);
        if (info.sample_rate == 0 || info.sample_rate > 384000u ||
            info.channels <= 0 || info.channels > 8) {
            message = "the Ogg stream uses an unsupported sample rate or channel count";
            Close();
            return false;
        }
        rate_ = static_cast<int32_t>(info.sample_rate);
        channels_ = info.channels;
        duration_ = std::max(0.0, static_cast<double>(stb_vorbis_stream_length_in_seconds(vorbis_)));
        return true;
    }

    void Close() {
        if (vorbis_) {
            stb_vorbis_close(vorbis_);
            vorbis_ = nullptr;
        }
        if (file_) {
            std::fclose(file_);
            file_ = nullptr;
        }
        eof_ = false;
        scratch_.clear();
    }

    bool IsOpen() const { return vorbis_ != nullptr; }
    int32_t Rate() const { return rate_; }
    int32_t Channels() const { return channels_; }
    double Duration() const { return duration_; }
    bool EndOfStream() const { return eof_; }

    bool Seek(double seconds) {
        if (!vorbis_) return false;
        seconds = std::clamp(seconds, 0.0, 24.0 * 3600.0);
        const double sample = seconds * static_cast<double>(rate_);
        const auto index = static_cast<unsigned int>(sample > 4294967295.0 ? 4294967295.0 : sample);
        if (!stb_vorbis_seek(vorbis_, index)) return false;
        eof_ = false;
        return true;
    }

    // Decodes the next frame and returns it interleaved, one float per channel.
    // The pointer stays valid until the next Read() or Close(); frames is 0 at
    // the end of the stream.
    const float* Read(uint32_t& frames) {
        frames = 0;
        if (!vorbis_) return nullptr;
        int channels = 0;
        float** planar = nullptr;
        const int count = stb_vorbis_get_frame_float(vorbis_, &channels, &planar);
        if (count <= 0 || !planar || channels <= 0) {
            eof_ = true;
            return nullptr;
        }
        if (channels != channels_) {
            // stb_vorbis guarantees a constant channel count for one stream;
            // keep the two in sync instead of writing past the interleave.
            channels_ = channels;
        }
        const size_t samples = static_cast<size_t>(count);
        scratch_.resize(samples * static_cast<size_t>(channels_));
        for (int channel = 0; channel < channels_; ++channel) {
            const float* source = planar[channel];
            for (size_t index = 0; index < samples; ++index) {
                scratch_[index * static_cast<size_t>(channels_) + static_cast<size_t>(channel)] =
                    source[index];
            }
        }
        frames = static_cast<uint32_t>(count);
        return scratch_.data();
    }

private:
    stb_vorbis* vorbis_ = nullptr;
    FILE* file_ = nullptr;
    int32_t rate_ = 48000;
    int32_t channels_ = 2;
    double duration_ = 0.0;
    bool eof_ = false;
    std::vector<float> scratch_;
};

} // namespace BetterEndfieldNext::Music::Ogg
