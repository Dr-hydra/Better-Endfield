#if defined(_WIN32)
// Windows driver-sound backend: Media Foundation decodes each effect once into
// a stereo float buffer at the mixer rate, then one WASAPI *shared-mode* render
// client mixes every live voice. Shared mode is what keeps the game's own audio
// untouched: Windows mixes our stream with the game's stream at the endpoint.
#include "driver_audio.h"

#include <Windows.h>
#include <audioclient.h>
#include <mfapi.h>
#include <mfidl.h>
#include <mfreadwrite.h>
#include <mmdeviceapi.h>

#include <algorithm>
#include <atomic>
#include <cmath>
#include <cstring>
#include <memory>
#include <mutex>
#include <string>
#include <thread>
#include <unordered_map>
#include <vector>

namespace BetterEndfieldNext::CustomModel {
namespace {

constexpr int32_t kMixRate = 48000;
constexpr int32_t kMixChannels = 2;
constexpr size_t kMaxVoices = 8;
// Bounds every decoded effect: 120 s of stereo float per voice and 480 s total.
constexpr size_t kMaxVoiceFrames = static_cast<size_t>(kMixRate) * 120u;
constexpr size_t kMaxTotalFrames = static_cast<size_t>(kMixRate) * 480u;
constexpr size_t kMaxFileBytes = 96u * 1024u * 1024u;

std::function<void(const std::string&)> g_log_sink;

void Log(const std::string& message);

std::wstring Widen(const std::string& text) {
    if (text.empty()) return {};
    const int size = MultiByteToWideChar(CP_UTF8, 0, text.data(), static_cast<int>(text.size()), nullptr, 0);
    if (size <= 0) return {};
    std::wstring value(static_cast<size_t>(size), L'\0');
    MultiByteToWideChar(CP_UTF8, 0, text.data(), static_cast<int>(text.size()), value.data(), size);
    return value;
}

struct Voice {
    std::string path;
    std::vector<float> pcm;   // interleaved stereo at kMixRate
    size_t cursor = 0;
    float gain = 1.0f;
    bool loop = true;
    std::atomic<bool> ready{false};
    std::atomic<bool> failed{false};
    std::atomic<bool> retired{false};
};

std::mutex g_voices_mutex;                 // guards the two containers below
std::unordered_map<std::string, std::shared_ptr<Voice>> g_voices;
std::vector<std::shared_ptr<Voice>> g_mix; // snapshot the mixer walks each block
size_t g_reserved_frames = 0;
std::mutex g_decode_mutex;                 // one decode at a time; keeps peaks low

std::thread g_mixer;
std::atomic<bool> g_running{false};
std::atomic<bool> g_ready{false};
HANDLE g_wake = nullptr;

// ---------------------------------------------------------------- decoding --
bool DecodeFile(const std::wstring& path, std::vector<float>& out, std::string& error) {
    out.clear();
    IMFSourceReader* reader = nullptr;
    HRESULT result = MFCreateSourceReaderFromURL(path.c_str(), nullptr, &reader);
    if (FAILED(result) || !reader) {
        error = "Media Foundation cannot open the audio file (hr=" + std::to_string(static_cast<long>(result)) + ")";
        return false;
    }
    auto release = [&]() { if (reader) reader->Release(); reader = nullptr; };

    reader->SetStreamSelection(static_cast<DWORD>(MF_SOURCE_READER_ALL_STREAMS), FALSE);
    reader->SetStreamSelection(static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), TRUE);
    IMFMediaType* requested = nullptr;
    result = MFCreateMediaType(&requested);
    if (SUCCEEDED(result)) result = requested->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio);
    if (SUCCEEDED(result)) result = requested->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_Float);
    if (SUCCEEDED(result)) {
        result = reader->SetCurrentMediaType(
            static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), nullptr, requested);
    }
    if (requested) requested->Release();
    if (FAILED(result)) { error = "the audio file has no decodable stream"; release(); return false; }

    IMFMediaType* current = nullptr;
    if (FAILED(reader->GetCurrentMediaType(
            static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), &current)) || !current) {
        error = "the audio stream format is unreadable"; release(); return false;
    }
    GUID subtype{};
    current->GetGUID(MF_MT_SUBTYPE, &subtype);
    const UINT32 rate = MFGetAttributeUINT32(current, MF_MT_AUDIO_SAMPLES_PER_SECOND, 0);
    const UINT32 channels = MFGetAttributeUINT32(current, MF_MT_AUDIO_NUM_CHANNELS, 0);
    const UINT32 bits = MFGetAttributeUINT32(current, MF_MT_AUDIO_BITS_PER_SAMPLE, 0);
    current->Release();
    if (subtype != MFAudioFormat_Float || bits != 32 || rate == 0 || rate > 384000 ||
        channels == 0 || channels > 8) {
        error = "the audio stream must decode to float32 with 1..8 channels"; release(); return false;
    }

    const double step = static_cast<double>(rate) / static_cast<double>(kMixRate);
    std::vector<float> pending;      // source-rate interleaved, stereo
    double position = 0.0;
    size_t pendingBase = 0;          // index of pending frame 0 in source frames

    auto append = [&](const float* frames, size_t count) {
        for (size_t i = 0; i < count; ++i) {
            float left = 0.0f, right = 0.0f;
            if (channels == 1) {
                left = right = frames[i];
            } else {
                left = frames[i * channels];
                right = frames[i * channels + 1];
                if (channels > 2) {
                    for (UINT32 extra = 2; extra < channels; ++extra) {
                        left += frames[i * channels + extra] * 0.5f;
                        right += frames[i * channels + extra] * 0.5f;
                    }
                }
            }
            pending.push_back(left);
            pending.push_back(right);
        }
    };

    bool eof = false;
    while (!eof) {
        if (out.size() / kMixChannels >= kMaxVoiceFrames) {
            error = "the audio file is longer than the 120 s effect limit";
            release();
            return false;
        }
        DWORD flags = 0;
        LONGLONG timestamp = 0;
        IMFSample* sample = nullptr;
        result = reader->ReadSample(static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM),
            0, nullptr, &flags, &timestamp, &sample);
        if (FAILED(result)) { if (sample) sample->Release(); error = "audio decode failed"; release(); return false; }
        if (flags & MF_SOURCE_READERF_ENDOFSTREAM) eof = true;
        if (sample) {
            IMFMediaBuffer* buffer = nullptr;
            if (SUCCEEDED(sample->ConvertToContiguousBuffer(&buffer)) && buffer) {
                BYTE* data = nullptr;
                DWORD length = 0;
                if (SUCCEEDED(buffer->Lock(&data, nullptr, &length))) {
                    const size_t frames = length / (sizeof(float) * channels);
                    append(reinterpret_cast<const float*>(data), frames);
                    buffer->Unlock();
                }
                buffer->Release();
            }
            sample->Release();
        }

        // Resample whatever is buffered beyond the last interpolated frame.
        const size_t available = pending.size() / kMixChannels;
        while (position + 1.0 < static_cast<double>(available) + static_cast<double>(pendingBase)) {
            const double local = position - static_cast<double>(pendingBase);
            const size_t index = static_cast<size_t>(local);
            const float blend = static_cast<float>(local - static_cast<double>(index));
            for (int channel = 0; channel < kMixChannels; ++channel) {
                const float a = pending[index * kMixChannels + channel];
                const float b = pending[(index + 1) * kMixChannels + channel];
                out.push_back(a + (b - a) * blend);
            }
            position += step;
        }
        // Drop source frames that can no longer be interpolated from, keeping
        // one behind the read position for the next blend.
        const double behind = std::max(0.0, position - static_cast<double>(pendingBase));
        if (behind >= 2.0) {
            const size_t drop = std::min(static_cast<size_t>(behind) - 1, available);
            if (drop) {
                pending.erase(pending.begin(), pending.begin() + static_cast<ptrdiff_t>(drop * kMixChannels));
                pendingBase += drop;
            }
        }
    }
    // Flush the tail with the last frame held.
    const size_t available = pending.size() / kMixChannels;
    while (position < static_cast<double>(pendingBase + available)) {
        const double local = position - static_cast<double>(pendingBase);
        if (local < 0.0 || local >= static_cast<double>(available)) break;
        const size_t index = std::min<size_t>(static_cast<size_t>(local), available - 1);
        for (int channel = 0; channel < kMixChannels; ++channel)
            out.push_back(pending[index * kMixChannels + channel]);
        position += step;
    }
    release();
    if (out.empty()) { error = "the audio file decoded to zero frames"; return false; }
    return true;
}

void DecodeInto(const std::shared_ptr<Voice>& voice) {
    std::string error;
    std::vector<float> pcm;
    bool ok = false;
    {
        std::scoped_lock lock(g_decode_mutex);
        ok = DecodeFile(Widen(voice->path), pcm, error);
    }
    if (!ok) {
        Log("[driver-audio] " + voice->path + ": " + error);
        voice->failed.store(true, std::memory_order_release);
        return;
    }
    {
        std::scoped_lock lock(g_voices_mutex);
        if (voice->retired.load(std::memory_order_acquire)) return;
        const size_t frames = pcm.size() / kMixChannels;
        if (g_reserved_frames + frames > kMaxTotalFrames) {
            Log("[driver-audio] total effect audio budget exhausted; voice dropped");
            voice->failed.store(true, std::memory_order_release);
            return;
        }
        g_reserved_frames += frames;
        voice->pcm = std::move(pcm);
    }
    voice->ready.store(true, std::memory_order_release);
}

// ------------------------------------------------------------------- mixing --
void MixInto(float* output, size_t frames, int32_t channels) {
    std::vector<std::shared_ptr<Voice>> snapshot;
    {
        std::scoped_lock lock(g_voices_mutex);
        snapshot = g_mix;
    }
    for (size_t frame = 0; frame < frames; ++frame) {
        float left = 0.0f, right = 0.0f;
        for (const auto& voice : snapshot) {
            if (!voice->ready.load(std::memory_order_acquire) || voice->pcm.empty()) continue;
            const size_t total = voice->pcm.size() / kMixChannels;
            if (voice->cursor >= total) {
                if (!voice->loop) continue;
                voice->cursor = 0;
            }
            left += voice->pcm[voice->cursor * kMixChannels] * voice->gain;
            right += voice->pcm[voice->cursor * kMixChannels + 1] * voice->gain;
            ++voice->cursor;
        }
        left = std::clamp(left, -1.0f, 1.0f);
        right = std::clamp(right, -1.0f, 1.0f);
        if (channels == 1) {
            output[frame] = (left + right) * 0.5f;
        } else {
            output[frame * channels] = left;
            output[frame * channels + 1] = right;
            for (int32_t extra = 2; extra < channels; ++extra)
                output[frame * channels + extra] = 0.0f;
        }
    }
}

void MixerLoop() {
    const HRESULT com = CoInitializeEx(nullptr, COINIT_MULTITHREADED);
    IMMDeviceEnumerator* enumerator = nullptr;
    IMMDevice* device = nullptr;
    IAudioClient* client = nullptr;
    IAudioRenderClient* render = nullptr;
    WAVEFORMATEX* format = nullptr;
    UINT32 bufferFrames = 0;
    int32_t channels = 2;
    bool floatOutput = true;
    bool started = false;

    auto cleanup = [&]() {
        if (client && started) client->Stop();
        if (render) render->Release();
        if (client) client->Release();
        if (format) CoTaskMemFree(format);
        if (device) device->Release();
        if (enumerator) enumerator->Release();
        if (SUCCEEDED(com)) CoUninitialize();
    };

    HRESULT result = CoCreateInstance(__uuidof(MMDeviceEnumerator), nullptr, CLSCTX_ALL,
        __uuidof(IMMDeviceEnumerator), reinterpret_cast<void**>(&enumerator));
    if (FAILED(result) || !enumerator) { Log("[driver-audio] no audio endpoint enumerator"); cleanup(); return; }
    result = enumerator->GetDefaultAudioEndpoint(eRender, eConsole, &device);
    if (FAILED(result) || !device) { Log("[driver-audio] no default render endpoint"); cleanup(); return; }
    result = device->Activate(__uuidof(IAudioClient), CLSCTX_ALL, nullptr, reinterpret_cast<void**>(&client));
    if (FAILED(result) || !client) { Log("[driver-audio] IAudioClient unavailable"); cleanup(); return; }
    result = client->GetMixFormat(&format);
    if (FAILED(result) || !format) { Log("[driver-audio] mix format unavailable"); cleanup(); return; }

    channels = format->nChannels;
    floatOutput = format->wBitsPerSample == 32 &&
        (format->wFormatTag == WAVE_FORMAT_IEEE_FLOAT ||
         (format->wFormatTag == WAVE_FORMAT_EXTENSIBLE &&
          reinterpret_cast<WAVEFORMATEXTENSIBLE*>(format)->SubFormat == KSDATAFORMAT_SUBTYPE_IEEE_FLOAT));
    if (!floatOutput || channels < 1 || channels > 8) {
        Log("[driver-audio] endpoint mix format is not float; effects stay silent");
        cleanup();
        return;
    }

    const REFERENCE_TIME duration = 10000000 / 20; // 50 ms
    result = client->Initialize(AUDCLNT_SHAREMODE_SHARED, 0, duration, 0, format, nullptr);
    if (FAILED(result)) { Log("[driver-audio] shared-mode initialise failed (hr=" +
        std::to_string(static_cast<long>(result)) + ")"); cleanup(); return; }
    result = client->GetBufferSize(&bufferFrames);
    if (FAILED(result)) { Log("[driver-audio] buffer size unavailable"); cleanup(); return; }
    result = client->GetService(__uuidof(IAudioRenderClient), reinterpret_cast<void**>(&render));
    if (FAILED(result) || !render) { Log("[driver-audio] render client unavailable"); cleanup(); return; }

    // Pre-fill with silence so the first Start() has a full buffer.
    {
        BYTE* data = nullptr;
        if (SUCCEEDED(render->GetBuffer(bufferFrames, &data)) && data)
            render->ReleaseBuffer(bufferFrames, AUDCLNT_BUFFERFLAGS_SILENT);
    }
    if (FAILED(client->Start())) { Log("[driver-audio] endpoint start failed"); cleanup(); return; }
    started = true;
    g_ready.store(true, std::memory_order_release);
    Log("[driver-audio] WASAPI shared-mode mixer ready (" + std::to_string(format->nSamplesPerSec) +
        " Hz, " + std::to_string(channels) + " ch)");

    const DWORD sleepMs = std::max<DWORD>(1, static_cast<DWORD>(1000ull * bufferFrames /
        std::max<UINT32>(format->nSamplesPerSec, 1u) / 2));
    std::vector<float> scratch;

    while (g_running.load(std::memory_order_acquire)) {
        WaitForSingleObject(g_wake, sleepMs);
        UINT32 padding = 0;
        if (FAILED(client->GetCurrentPadding(&padding))) continue;
        const UINT32 available = bufferFrames > padding ? bufferFrames - padding : 0;
        if (!available) continue;
        BYTE* data = nullptr;
        if (FAILED(render->GetBuffer(available, &data)) || !data) continue;
        scratch.assign(static_cast<size_t>(available) * channels, 0.0f);
        MixInto(scratch.data(), available, channels);
        // The endpoint may run at a rate other than kMixRate; a nearest-sample
        // copy is enough for short effects and avoids a second resampler here.
        std::memcpy(data, scratch.data(), scratch.size() * sizeof(float));
        render->ReleaseBuffer(available, 0);
    }
    cleanup();
    g_ready.store(false, std::memory_order_release);
}

bool EnsureMixer() {
    if (g_running.load(std::memory_order_acquire)) return true;
    g_wake = CreateEventW(nullptr, FALSE, FALSE, nullptr);
    if (!g_wake) return false;
    g_running.store(true, std::memory_order_release);
    try {
        g_mixer = std::thread(MixerLoop);
    } catch (...) {
        g_running.store(false, std::memory_order_release);
        return false;
    }
    return true;
}

void Log(const std::string& message) {
    if (g_log_sink) g_log_sink(message);
}

}  // namespace

void SetDriverAudioLogger(std::function<void(const std::string&)> sink) {
    g_log_sink = std::move(sink);
}

bool DriverAudioAvailable() { return g_ready.load(std::memory_order_acquire); }

bool StartDriverSound(const std::string& key, const DriverSoundSpec& spec) {
    if (key.empty() || spec.path.empty()) return false;
    if (!EnsureMixer()) return false;
    std::shared_ptr<Voice> voice;
    {
        std::scoped_lock lock(g_voices_mutex);
        const auto found = g_voices.find(key);
        if (found != g_voices.end() && found->second->path == spec.path &&
            !found->second->failed.load(std::memory_order_acquire)) {
            found->second->gain = std::clamp(spec.gain, 0.0f, 2.0f);
            found->second->loop = spec.loop;
            return true;   // already playing this file
        }
        if (found != g_voices.end()) {
            found->second->retired.store(true, std::memory_order_release);
            g_voices.erase(found);
        }
        if (g_voices.size() >= kMaxVoices) {
            Log("[driver-audio] voice quota reached; '" + key + "' dropped");
            return false;
        }
        voice = std::make_shared<Voice>();
        voice->path = spec.path;
        voice->gain = std::clamp(spec.gain, 0.0f, 2.0f);
        voice->loop = spec.loop;
        g_voices.emplace(key, voice);
        g_mix.clear();
        for (const auto& entry : g_voices) g_mix.push_back(entry.second);
    }
    try {
        std::thread(DecodeInto, voice).detach();
    } catch (...) {
        return false;
    }
    return true;
}

void StopDriverSound(const std::string& key) {
    std::scoped_lock lock(g_voices_mutex);
    const auto found = g_voices.find(key);
    if (found == g_voices.end()) return;
    found->second->retired.store(true, std::memory_order_release);
    g_voices.erase(found);
    g_mix.clear();
    for (const auto& entry : g_voices) g_mix.push_back(entry.second);
}

void StopAllDriverSounds() {
    std::scoped_lock lock(g_voices_mutex);
    for (auto& entry : g_voices) entry.second->retired.store(true, std::memory_order_release);
    g_voices.clear();
    g_mix.clear();
}

std::string DriverAudioStatus() {
    std::scoped_lock lock(g_voices_mutex);
    size_t ready = 0, frames = 0;
    for (const auto& entry : g_voices) {
        if (entry.second->ready.load(std::memory_order_acquire)) ++ready;
        frames += entry.second->pcm.size() / kMixChannels;
    }
    if (g_voices.empty() && !g_ready.load(std::memory_order_acquire)) return {};
    return "voices=" + std::to_string(g_voices.size()) + " ready=" + std::to_string(ready) +
        " frames=" + std::to_string(frames) +
        " device=" + (g_ready.load(std::memory_order_acquire) ? "up" : "down");
}

}  // namespace BetterEndfieldNext::CustomModel
#endif
