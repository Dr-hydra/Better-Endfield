#pragma once
#include <windows.h>
#include <mmreg.h>
#include <mmdeviceapi.h>
#include <audioclient.h>
#include <avrt.h>
#include <mfapi.h>
#include <mfidl.h>
#include <mfreadwrite.h>
#include <algorithm>
#include <atomic>
#include <cmath>
#include <cstdio>
#include <cstring>
#include <vector>

#include "audio_timeline.h"

#pragma comment(lib, "mfplat.lib")
#pragma comment(lib, "mfreadwrite.lib")
#pragma comment(lib, "mfuuid.lib")
#pragma comment(lib, "ole32.lib")
#pragma comment(lib, "avrt.lib")

void Log(const char *fmt, ...);

struct DecodedAudio {
  std::vector<int16_t> pcm;
  uint64_t frames = 0;
  uint32_t sampleRate = 0;
  uint32_t sourceChannels = 0;
};

template <class T> static void AudioSafeRelease(T **object) {
  if (object && *object) {
    (*object)->Release();
    *object = nullptr;
  }
}

static int16_t AudioFloatToPcm16(float value) {
  const float clamped = (std::max)(-1.0f, (std::min)(value, 1.0f));
  return static_cast<int16_t>(std::lround(clamped * 32767.0f));
}

static bool DecodeAudioFile(const wchar_t *path, DecodedAudio *out) {
  if (!path || !out)
    return false;
  *out = DecodedAudio{};
  HRESULT hr = CoInitializeEx(nullptr, COINIT_APARTMENTTHREADED);
  const bool comOwner = SUCCEEDED(hr);
  hr = MFStartup(MF_VERSION, MFSTARTUP_LITE);
  if (FAILED(hr)) {
    Log("[AUDIO] MFStartup failed: 0x%08X", (unsigned)hr);
    if (comOwner)
      CoUninitialize();
    return false;
  }

  bool ok = false;
  IMFSourceReader *reader = nullptr;
  IMFMediaType *request = nullptr;
  IMFMediaType *actual = nullptr;
  UINT32 channels = 0, rate = 0, bits = 0;
  GUID subtype = GUID_NULL;
  do {
    hr = MFCreateSourceReaderFromURL(path, nullptr, &reader);
    if (FAILED(hr) || !reader) {
      Log("[AUDIO] MFCreateSourceReaderFromURL failed: 0x%08X",
          (unsigned)hr);
      break;
    }
    reader->SetStreamSelection(
        static_cast<DWORD>(MF_SOURCE_READER_ALL_STREAMS), FALSE);
    reader->SetStreamSelection(
        static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), TRUE);
    if (FAILED(MFCreateMediaType(&request)))
      break;
    request->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio);
    request->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_Float);
    hr = reader->SetCurrentMediaType(
        static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), nullptr,
        request);
    if (FAILED(hr)) {
      request->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_PCM);
      request->SetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, 16);
      hr = reader->SetCurrentMediaType(
          static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), nullptr,
          request);
    }
    if (FAILED(hr)) {
      Log("[AUDIO] SetCurrentMediaType failed: 0x%08X", (unsigned)hr);
      break;
    }
    if (FAILED(reader->GetCurrentMediaType(
            static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM),
            &actual)))
      break;
    actual->GetGUID(MF_MT_SUBTYPE, &subtype);
    actual->GetUINT32(MF_MT_AUDIO_NUM_CHANNELS, &channels);
    actual->GetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND, &rate);
    actual->GetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, &bits);
    const bool isFloat = subtype == MFAudioFormat_Float && bits == 32;
    const bool isPcm16 = subtype == MFAudioFormat_PCM && bits == 16;
    if (channels == 0 || rate == 0 || (!isFloat && !isPcm16)) {
      Log("[AUDIO] unsupported decoded format channels=%u rate=%u bits=%u",
          channels, rate, bits);
      break;
    }

    PROPVARIANT duration;
    PropVariantInit(&duration);
    if (SUCCEEDED(reader->GetPresentationAttribute(
            static_cast<DWORD>(MF_SOURCE_READER_MEDIASOURCE),
            MF_PD_DURATION, &duration)) &&
        duration.vt == VT_UI8) {
      const double seconds = duration.uhVal.QuadPart / 1.0e7;
      if (seconds > 0.0 && seconds < 4.0 * 3600.0)
        out->pcm.reserve(static_cast<size_t>(seconds * rate + rate) * 2);
    }
    PropVariantClear(&duration);

    const uint32_t bytesPerSample = isFloat ? 4 : 2;
    const uint32_t blockAlign = bytesPerSample * channels;
    bool readFailed = false;
    while (true) {
      DWORD flags = 0;
      IMFSample *sample = nullptr;
      hr = reader->ReadSample(
          static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), 0,
          nullptr, &flags, nullptr, &sample);
      if (FAILED(hr)) {
        readFailed = true;
        break;
      }
      if (flags & MF_SOURCE_READERF_ENDOFSTREAM) {
        AudioSafeRelease(&sample);
        break;
      }
      if (!sample)
        continue;
      IMFMediaBuffer *buffer = nullptr;
      if (SUCCEEDED(sample->ConvertToContiguousBuffer(&buffer)) && buffer) {
        BYTE *data = nullptr;
        DWORD length = 0;
        if (SUCCEEDED(buffer->Lock(&data, nullptr, &length)) && data) {
          const DWORD count = length / blockAlign;
          for (DWORD f = 0; f < count; ++f) {
            const BYTE *frame = data + static_cast<size_t>(f) * blockAlign;
            if (isFloat) {
              const float *s = reinterpret_cast<const float *>(frame);
              out->pcm.push_back(AudioFloatToPcm16(s[0]));
              out->pcm.push_back(AudioFloatToPcm16(channels > 1 ? s[1] : s[0]));
            } else {
              const int16_t *s = reinterpret_cast<const int16_t *>(frame);
              out->pcm.push_back(s[0]);
              out->pcm.push_back(channels > 1 ? s[1] : s[0]);
            }
          }
          buffer->Unlock();
        }
        buffer->Release();
      }
      sample->Release();
    }
    if (readFailed) {
      Log("[AUDIO] ReadSample failed: 0x%08X after %zu frames",
          (unsigned)hr, out->pcm.size() / 2);
      break;
    }
    out->frames = out->pcm.size() / 2;
    out->sampleRate = rate;
    out->sourceChannels = channels;
    ok = out->frames > 0;
    if (!ok)
      Log("[AUDIO] decode produced 0 frames");
  } while (false);

  AudioSafeRelease(&actual);
  AudioSafeRelease(&request);
  AudioSafeRelease(&reader);
  MFShutdown();
  if (comOwner)
    CoUninitialize();
  if (!ok)
    *out = DecodedAudio{};
  return ok;
}

struct AudioClockSample {
  bool valid = false;
  bool audibleMedia = false;
  double mediaSeconds = 0.0;
  double rate = 0.0;
  uint32_t audibleEpoch = 0;
  uint32_t commandEpoch = 0;
  double latencySeconds = 0.0;
};

static uint64_t AudioQpcTo100ns(int64_t counter, int64_t frequency) {
  if (frequency <= 0 || counter < 0)
    return 0;
  const uint64_t q = static_cast<uint64_t>(counter);
  const uint64_t f = static_cast<uint64_t>(frequency);
  return (q / f) * 10000000ull + (q % f) * 10000000ull / f;
}

struct AudioPlayer {
  std::atomic<bool> loaded{false};
  std::atomic<bool> playing{false};

  AudioPlayer() {
    InitializeSRWLock(&lock_);
    InitializeSRWLock(&snapLock_);
    LARGE_INTEGER frequency = {};
    QueryPerformanceFrequency(&frequency);
    qpcFrequency_ = frequency.QuadPart;
  }

  bool Open(const wchar_t *path) {
    Close();
    if (!path || path[0] == L'\0')
      return false;
    char narrowPath[512] = {};
    WideCharToMultiByte(CP_UTF8, 0, path, -1, narrowPath, sizeof(narrowPath),
                        nullptr, nullptr);
    LARGE_INTEGER begin = {}, end = {};
    QueryPerformanceCounter(&begin);
    DecodedAudio decoded;
    if (!DecodeAudioFile(path, &decoded)) {
      Log("[AUDIO] decode failed, cannot play: %s", narrowPath);
      return false;
    }
    QueryPerformanceCounter(&end);

    AcquireSRWLockExclusive(&lock_);
    pcm_.swap(decoded.pcm);
    frames_ = decoded.frames;
    sampleRate_ = decoded.sampleRate;
    lengthMs_.store(static_cast<int>(frames_ * 1000ull / sampleRate_),
                    std::memory_order_release);
    cursor_ = 0.0;
    run_ = false;
    endedNaturally_ = false;
    fadeInFrames_ = 0;
    ++commandEpoch_;
    playing.store(false, std::memory_order_release);
    loaded.store(true, std::memory_order_release);
    loadGeneration_.fetch_add(1, std::memory_order_acq_rel);
    ReleaseSRWLockExclusive(&lock_);

    stop_.store(false, std::memory_order_release);
    deviceState_.store(kDeviceOpening, std::memory_order_release);
    stopEvent_ = CreateEventW(nullptr, TRUE, FALSE, nullptr);
    thread_ = CreateThread(nullptr, 0, &AudioPlayer::RenderThreadProc, this,
                           0, nullptr);
    if (!thread_) {
      Log("[AUDIO] render thread creation failed err=%lu", GetLastError());
      Close();
      return false;
    }
    Log("[AUDIO] Opened %s, length=%d ms, rate=%u channels=%u frames=%llu "
        "decodeMs=%.1f engine=wasapi-shared",
        narrowPath, GetLengthMs(), decoded.sampleRate,
        decoded.sourceChannels, (unsigned long long)frames_,
        QpcMs(begin.QuadPart, end.QuadPart));
    return true;
  }

  void Close() {
    const bool wasLoaded = loaded.exchange(false, std::memory_order_acq_rel);
    AcquireSRWLockExclusive(&lock_);
    run_ = false;
    ++commandEpoch_;
    playing.store(false, std::memory_order_release);
    ReleaseSRWLockExclusive(&lock_);
    if (thread_) {
      stop_.store(true, std::memory_order_release);
      if (stopEvent_)
        SetEvent(stopEvent_);
      WaitForSingleObject(thread_, 3000);
      CloseHandle(thread_);
      thread_ = nullptr;
    }
    if (stopEvent_) {
      CloseHandle(stopEvent_);
      stopEvent_ = nullptr;
    }
    AcquireSRWLockExclusive(&lock_);
    std::vector<int16_t>().swap(pcm_);
    frames_ = 0;
    cursor_ = 0.0;
    lengthMs_.store(0, std::memory_order_release);
    ReleaseSRWLockExclusive(&lock_);
    AcquireSRWLockExclusive(&snapLock_);
    snap_ = Snapshot{};
    ReleaseSRWLockExclusive(&snapLock_);
    if (wasLoaded)
      Log("[AUDIO] Closed");
  }

  void Play() { PlayFrom(0); }

  void PlayFrom(int ms) { Command(true, true, ms); }

  void Pause() {
    AcquireSRWLockExclusive(&lock_);
    if (run_) {
      run_ = false;
      ++commandEpoch_;
    }
    playing.store(false, std::memory_order_release);
    ReleaseSRWLockExclusive(&lock_);
  }

  void Resume() {
    AcquireSRWLockExclusive(&lock_);
    if (loaded.load(std::memory_order_acquire) && !run_ &&
        cursor_ < static_cast<double>(frames_)) {
      run_ = true;
      endedNaturally_ = false;
      fadeInFrames_ = kFadeFrames;
      ++commandEpoch_;
      playing.store(true, std::memory_order_release);
    }
    ReleaseSRWLockExclusive(&lock_);
  }

  void Stop() { Command(false, true, 0); }

  bool SeekTo(int ms) {
    if (!loaded.load(std::memory_order_acquire))
      return false;
    Command(false, true, ms);
    return true;
  }

  bool SetPlaybackSpeed(float speed) {
    if (!std::isfinite(speed))
      return false;
    const float clamped = (std::max)(0.05f, (std::min)(speed, 4.0f));
    AcquireSRWLockExclusive(&lock_);
    const bool changed = std::fabs(clamped - speed_) > 1.0e-4f;
    speed_ = clamped;
    ReleaseSRWLockExclusive(&lock_);
    if (changed)
      Log("[AUDIO-SPEED] speed=%.3f resampled=1", clamped);
    return true;
  }

  void SetVolume(int vol) {
    vol = (std::max)(0, (std::min)(vol, 1000));
    AcquireSRWLockExclusive(&lock_);
    gain_ = static_cast<float>(vol) / 1000.0f;
    ReleaseSRWLockExclusive(&lock_);
  }

  int GetLengthMs() const { return lengthMs_.load(std::memory_order_acquire); }

  uint32_t LoadGeneration() const {
    return loadGeneration_.load(std::memory_order_acquire);
  }

  bool DevicePending() const {
    return loaded.load(std::memory_order_acquire) &&
           deviceState_.load(std::memory_order_acquire) == kDeviceOpening;
  }

  bool EndedNaturally() {
    AcquireSRWLockShared(&lock_);
    const bool ended = endedNaturally_;
    ReleaseSRWLockShared(&lock_);
    return ended;
  }

  bool QueryClock(AudioClockSample *out) {
    if (!out)
      return false;
    *out = AudioClockSample{};
    AcquireSRWLockShared(&lock_);
    out->commandEpoch = commandEpoch_;
    ReleaseSRWLockShared(&lock_);
    Snapshot snap;
    AcquireSRWLockShared(&snapLock_);
    snap = snap_;
    ReleaseSRWLockShared(&snapLock_);
    if (!snap.valid || !loaded.load(std::memory_order_acquire))
      return false;
    LARGE_INTEGER now = {};
    QueryPerformanceCounter(&now);
    const uint64_t now100ns = AudioQpcTo100ns(now.QuadPart, qpcFrequency_);
    const double sincePosition =
        now100ns > snap.position100ns
            ? (now100ns - snap.position100ns) / 1.0e7
            : 0.0;
    const double sincePublish =
        now100ns > snap.publish100ns ? (now100ns - snap.publish100ns) / 1.0e7
                                     : 0.0;
    if (sincePublish > kStaleSeconds)
      return false;
    const double extrapolation = (std::min)(sincePosition, kMaxExtrapolation);
    double media = snap.mediaSeconds + extrapolation * snap.rate;
    const double length = GetLengthMs() / 1000.0;
    if (length > 0.0)
      media = (std::min)(media, length);
    out->valid = true;
    out->audibleMedia = snap.media;
    out->mediaSeconds = media;
    out->rate = snap.rate;
    out->audibleEpoch = snap.epoch;
    out->latencySeconds = snap.latencySeconds;
    return true;
  }

  int GetPositionMs() {
    AudioClockSample sample;
    if (!QueryClock(&sample) || sample.audibleEpoch != sample.commandEpoch)
      return -1;
    return static_cast<int>(std::lround(sample.mediaSeconds * 1000.0));
  }

  bool AtEnd() {
    const int length = GetLengthMs();
    if (!loaded.load(std::memory_order_acquire) || length <= 0)
      return false;
    if (EndedNaturally())
      return true;
    const int position = GetPositionMs();
    return position >= 0 && position >= length - 30;
  }

private:
  static constexpr uint32_t kFadeFrames = 96;
  static constexpr double kMaxExtrapolation = 0.05;
  static constexpr double kStaleSeconds = 0.25;
  static constexpr REFERENCE_TIME kBufferDuration = 300000;
  static constexpr int kDeviceOpening = 0;
  static constexpr int kDeviceReady = 1;
  static constexpr int kDeviceFailed = 2;

  enum class SampleType : uint8_t { Float32, Int16, Int24, Int32 };

  struct Snapshot {
    bool valid = false;
    bool media = false;
    double mediaSeconds = 0.0;
    double rate = 0.0;
    uint32_t epoch = 0;
    double latencySeconds = 0.0;
    uint64_t position100ns = 0;
    uint64_t publish100ns = 0;
  };

  void Command(bool run, bool reposition, int ms) {
    AcquireSRWLockExclusive(&lock_);
    if (!loaded.load(std::memory_order_acquire)) {
      ReleaseSRWLockExclusive(&lock_);
      return;
    }
    if (reposition) {
      const double target =
          (std::max)(0, ms) * static_cast<double>(sampleRate_) / 1000.0;
      cursor_ = (std::min)(target, static_cast<double>(frames_));
    }
    run_ = run && cursor_ < static_cast<double>(frames_);
    endedNaturally_ = false;
    fadeInFrames_ = run_ ? kFadeFrames : 0;
    ++commandEpoch_;
    playing.store(run_, std::memory_order_release);
    ReleaseSRWLockExclusive(&lock_);
  }

  double QpcMs(int64_t from, int64_t to) const {
    return qpcFrequency_ > 0
               ? static_cast<double>(to - from) * 1000.0 / qpcFrequency_
               : 0.0;
  }

  static DWORD WINAPI RenderThreadProc(LPVOID parameter) {
    HMODULE module = nullptr;
    GetModuleHandleExW(
        GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS,
        reinterpret_cast<LPCWSTR>(&AudioPlayer::RenderThreadProc), &module);
    static_cast<AudioPlayer *>(parameter)->RenderLoop();
    if (module)
      FreeLibraryAndExitThread(module, 0);
    return 0;
  }

  void RenderLoop() {
    const HRESULT com = CoInitializeEx(nullptr, COINIT_MULTITHREADED);
    DWORD taskIndex = 0;
    HANDLE task = AvSetMmThreadCharacteristicsW(L"Pro Audio", &taskIndex);
    if (!task)
      SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_TIME_CRITICAL);
    audioEvent_ = CreateEventW(nullptr, FALSE, FALSE, nullptr);
    int openFailures = 0;
    ULONGLONG lastDeviceCheck = GetTickCount64();
    while (!stop_.load(std::memory_order_acquire)) {
      if (!client_) {
        if (!OpenDevice()) {
          deviceState_.store(kDeviceFailed, std::memory_order_release);
          if (openFailures++ < 3)
            Log("[AUDIO-DEVICE] open failed; retrying");
          WaitForSingleObject(stopEvent_, 1000);
          continue;
        }
        openFailures = 0;
      }
      HANDLE handles[2] = {audioEvent_, stopEvent_};
      const DWORD wait = WaitForMultipleObjects(2, handles, FALSE, 200);
      if (wait == WAIT_OBJECT_0 + 1)
        break;
      const HRESULT hr = RenderAvailable();
      if (FAILED(hr)) {
        Log("[AUDIO-DEVICE] render failed hr=0x%08X; reopening default "
            "endpoint",
            (unsigned)hr);
        CloseDevice();
        continue;
      }
      if (PublishSnapshot())
        deviceState_.store(kDeviceReady, std::memory_order_release);
      const ULONGLONG now = GetTickCount64();
      if (now - lastDeviceCheck >= 1000) {
        lastDeviceCheck = now;
        if (DefaultDeviceChanged()) {
          Log("[AUDIO-DEVICE] default endpoint changed; reopening");
          CloseDevice();
        }
      }
    }
    CloseDevice();
    if (audioEvent_) {
      CloseHandle(audioEvent_);
      audioEvent_ = nullptr;
    }
    if (task)
      AvRevertMmThreadCharacteristics(task);
    if (SUCCEEDED(com))
      CoUninitialize();
  }

  bool OpenDevice() {
    HRESULT hr = CoCreateInstance(__uuidof(MMDeviceEnumerator), nullptr,
                                  CLSCTX_ALL, IID_PPV_ARGS(&enumerator_));
    if (FAILED(hr))
      return false;
    IMMDevice *device = nullptr;
    WAVEFORMATEX *mix = nullptr;
    bool ok = false;
    do {
      if (FAILED(enumerator_->GetDefaultAudioEndpoint(eRender, eConsole,
                                                      &device)))
        break;
      LPWSTR id = nullptr;
      if (SUCCEEDED(device->GetId(&id)) && id) {
        wcsncpy_s(deviceId_, id, _TRUNCATE);
        CoTaskMemFree(id);
      }
      if (FAILED(device->Activate(__uuidof(IAudioClient), CLSCTX_ALL,
                                  nullptr,
                                  reinterpret_cast<void **>(&client_))))
        break;
      if (FAILED(client_->GetMixFormat(&mix)) || !mix)
        break;
      if (!ParseMixFormat(mix))
        break;
      hr = client_->Initialize(AUDCLNT_SHAREMODE_SHARED,
                               AUDCLNT_STREAMFLAGS_EVENTCALLBACK |
                                   AUDCLNT_STREAMFLAGS_NOPERSIST,
                               kBufferDuration, 0, mix, nullptr);
      if (FAILED(hr)) {
        Log("[AUDIO-DEVICE] Initialize failed hr=0x%08X", (unsigned)hr);
        break;
      }
      if (FAILED(client_->SetEventHandle(audioEvent_)) ||
          FAILED(client_->GetBufferSize(&bufferFrames_)) ||
          FAILED(client_->GetService(IID_PPV_ARGS(&renderClient_))) ||
          FAILED(client_->GetService(IID_PPV_ARGS(&clock_))) ||
          FAILED(clock_->GetFrequency(&clockFrequency_)) ||
          clockFrequency_ == 0)
        break;
      deviceWritten_ = 0;
      timeline_.Clear();
      scratch_.assign(static_cast<size_t>(bufferFrames_) * 2, 0.0f);
      if (FAILED(RenderAvailable()))
        break;
      if (FAILED(client_->Start()))
        break;
      REFERENCE_TIME streamLatency = 0;
      client_->GetStreamLatency(&streamLatency);
      Log("[AUDIO-DEVICE] opened rate=%u channels=%u type=%u container=%u "
          "bufferFrames=%u streamLatencyMs=%.1f",
          deviceRate_, deviceChannels_, static_cast<unsigned>(sampleType_),
          containerBytes_ * 8, bufferFrames_, streamLatency / 10000.0);
      ok = true;
    } while (false);
    if (mix)
      CoTaskMemFree(mix);
    AudioSafeRelease(&device);
    if (!ok)
      CloseDevice();
    return ok;
  }

  bool ParseMixFormat(const WAVEFORMATEX *mix) {
    deviceRate_ = mix->nSamplesPerSec;
    deviceChannels_ = mix->nChannels;
    containerBytes_ = mix->nChannels ? mix->nBlockAlign / mix->nChannels : 0;
    WORD tag = mix->wFormatTag;
    if (tag == WAVE_FORMAT_EXTENSIBLE &&
        mix->cbSize >= sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)) {
      const WAVEFORMATEXTENSIBLE *ext =
          reinterpret_cast<const WAVEFORMATEXTENSIBLE *>(mix);
      tag = static_cast<WORD>(ext->SubFormat.Data1);
    }
    if (tag == WAVE_FORMAT_IEEE_FLOAT && containerBytes_ == 4)
      sampleType_ = SampleType::Float32;
    else if (tag == WAVE_FORMAT_PCM && containerBytes_ == 2)
      sampleType_ = SampleType::Int16;
    else if (tag == WAVE_FORMAT_PCM && containerBytes_ == 3)
      sampleType_ = SampleType::Int24;
    else if (tag == WAVE_FORMAT_PCM && containerBytes_ == 4)
      sampleType_ = SampleType::Int32;
    else {
      Log("[AUDIO-DEVICE] unsupported mix format tag=%u container=%u", tag,
          containerBytes_ * 8);
      return false;
    }
    return deviceRate_ > 0 && deviceChannels_ > 0;
  }

  void CloseDevice() {
    deviceState_.store(kDeviceOpening, std::memory_order_release);
    if (client_)
      client_->Stop();
    AudioSafeRelease(&clock_);
    AudioSafeRelease(&renderClient_);
    AudioSafeRelease(&client_);
    AudioSafeRelease(&enumerator_);
    deviceId_[0] = L'\0';
    AcquireSRWLockExclusive(&snapLock_);
    snap_.valid = false;
    ReleaseSRWLockExclusive(&snapLock_);
  }

  bool DefaultDeviceChanged() {
    if (!enumerator_)
      return false;
    IMMDevice *device = nullptr;
    if (FAILED(enumerator_->GetDefaultAudioEndpoint(eRender, eConsole,
                                                    &device)))
      return false;
    bool changed = false;
    LPWSTR id = nullptr;
    if (SUCCEEDED(device->GetId(&id)) && id) {
      changed = wcscmp(id, deviceId_) != 0;
      CoTaskMemFree(id);
    }
    device->Release();
    return changed;
  }

  HRESULT RenderAvailable() {
    UINT32 padding = 0;
    HRESULT hr = client_->GetCurrentPadding(&padding);
    if (FAILED(hr))
      return hr;
    if (padding >= bufferFrames_)
      return S_OK;
    const UINT32 available = bufferFrames_ - padding;
    BYTE *data = nullptr;
    hr = renderClient_->GetBuffer(available, &data);
    if (FAILED(hr))
      return hr;
    FillScratch(available);
    WriteDevice(data, available);
    hr = renderClient_->ReleaseBuffer(available, 0);
    deviceWritten_ += available;
    return hr;
  }

  void FillScratch(UINT32 count) {
    AcquireSRWLockExclusive(&lock_);
    const double step =
        static_cast<double>(sampleRate_) * speed_ / deviceRate_;
    const double mediaPerDevice =
        sampleRate_ ? step / static_cast<double>(sampleRate_) : 0.0;
    UINT32 produced = 0;
    if (run_ && frames_ > 0 && step > 0.0) {
      const double remaining = static_cast<double>(frames_) - cursor_;
      const double fit = remaining > 0.0 ? std::ceil(remaining / step) : 0.0;
      const UINT32 mediaFrames =
          static_cast<UINT32>((std::min)(fit, static_cast<double>(count)));
      AudioTimelineSegment segment;
      segment.deviceStart = deviceWritten_;
      segment.deviceCount = mediaFrames;
      segment.mediaStartSeconds = cursor_ / sampleRate_;
      segment.mediaSecondsPerDeviceFrame = mediaPerDevice;
      segment.epoch = commandEpoch_;
      segment.media = 1;
      timeline_.Append(segment);
      for (UINT32 i = 0; i < mediaFrames; ++i) {
        float l = 0.0f, r = 0.0f;
        AudioSampleStereo(pcm_.data(), frames_, cursor_ + i * step, &l, &r);
        float g = gain_;
        if (fadeInFrames_ > 0) {
          g *= 1.0f - static_cast<float>(fadeInFrames_) / kFadeFrames;
          --fadeInFrames_;
        }
        scratch_[i * 2] = l * g;
        scratch_[i * 2 + 1] = r * g;
      }
      cursor_ += mediaFrames * step;
      produced = mediaFrames;
      if (cursor_ >= static_cast<double>(frames_)) {
        cursor_ = static_cast<double>(frames_);
        run_ = false;
        endedNaturally_ = true;
        playing.store(false, std::memory_order_release);
      }
    }
    if (produced < count) {
      AudioTimelineSegment silence;
      silence.deviceStart = deviceWritten_ + produced;
      silence.deviceCount = count - produced;
      silence.mediaStartSeconds = sampleRate_ ? cursor_ / sampleRate_ : 0.0;
      silence.epoch = commandEpoch_;
      timeline_.Append(silence);
      std::fill(scratch_.begin() + static_cast<size_t>(produced) * 2,
                scratch_.begin() + static_cast<size_t>(count) * 2, 0.0f);
    }
    ReleaseSRWLockExclusive(&lock_);
  }

  void WriteDevice(BYTE *data, UINT32 count) {
    for (UINT32 i = 0; i < count; ++i) {
      const float l = scratch_[i * 2];
      const float r = scratch_[i * 2 + 1];
      for (UINT32 c = 0; c < deviceChannels_; ++c) {
        float v = 0.0f;
        if (deviceChannels_ == 1)
          v = (l + r) * 0.5f;
        else if (c == 0)
          v = l;
        else if (c == 1)
          v = r;
        v = (std::max)(-1.0f, (std::min)(v, 1.0f));
        BYTE *out = data + (static_cast<size_t>(i) * deviceChannels_ + c) *
                               containerBytes_;
        switch (sampleType_) {
        case SampleType::Float32:
          memcpy(out, &v, 4);
          break;
        case SampleType::Int16: {
          const int16_t s = static_cast<int16_t>(std::lround(v * 32767.0f));
          memcpy(out, &s, 2);
          break;
        }
        case SampleType::Int24: {
          const int32_t s = static_cast<int32_t>(std::lround(v * 8388607.0f));
          out[0] = static_cast<BYTE>(s & 0xFF);
          out[1] = static_cast<BYTE>((s >> 8) & 0xFF);
          out[2] = static_cast<BYTE>((s >> 16) & 0xFF);
          break;
        }
        case SampleType::Int32: {
          const int32_t s = static_cast<int32_t>(
              std::llround(static_cast<double>(v) * 2147483647.0));
          memcpy(out, &s, 4);
          break;
        }
        }
      }
    }
  }

  bool PublishSnapshot() {
    UINT64 position = 0, positionQpc = 0;
    if (!clock_ || FAILED(clock_->GetPosition(&position, &positionQpc)))
      return false;
    const double playedFrames =
        static_cast<double>(position) * deviceRate_ / clockFrequency_;
    const AudioTimelinePoint point = timeline_.Lookup(playedFrames);
    if (!point.found)
      return false;
    LARGE_INTEGER now = {};
    QueryPerformanceCounter(&now);
    Snapshot snap;
    snap.valid = true;
    snap.media = point.media != 0;
    snap.mediaSeconds = point.mediaSeconds;
    snap.rate = point.mediaSecondsPerDeviceFrame * deviceRate_;
    snap.epoch = point.epoch;
    snap.latencySeconds =
        (static_cast<double>(deviceWritten_) - playedFrames) / deviceRate_;
    snap.position100ns = positionQpc;
    snap.publish100ns = AudioQpcTo100ns(now.QuadPart, qpcFrequency_);
    AcquireSRWLockExclusive(&snapLock_);
    snap_ = snap;
    ReleaseSRWLockExclusive(&snapLock_);
    return true;
  }

  SRWLOCK lock_;
  std::vector<int16_t> pcm_;
  uint64_t frames_ = 0;
  uint32_t sampleRate_ = 0;
  double cursor_ = 0.0;
  bool run_ = false;
  bool endedNaturally_ = false;
  float speed_ = 1.0f;
  float gain_ = 1.0f;
  uint32_t fadeInFrames_ = 0;
  uint32_t commandEpoch_ = 0;
  std::atomic<int> lengthMs_{0};
  std::atomic<uint32_t> loadGeneration_{0};

  SRWLOCK snapLock_;
  Snapshot snap_;
  int64_t qpcFrequency_ = 0;

  HANDLE thread_ = nullptr;
  HANDLE stopEvent_ = nullptr;
  HANDLE audioEvent_ = nullptr;
  std::atomic<bool> stop_{false};
  std::atomic<int> deviceState_{kDeviceOpening};
  IMMDeviceEnumerator *enumerator_ = nullptr;
  IAudioClient *client_ = nullptr;
  IAudioRenderClient *renderClient_ = nullptr;
  IAudioClock *clock_ = nullptr;
  UINT64 clockFrequency_ = 0;
  UINT32 bufferFrames_ = 0;
  uint32_t deviceRate_ = 0;
  uint32_t deviceChannels_ = 0;
  uint32_t containerBytes_ = 0;
  SampleType sampleType_ = SampleType::Float32;
  uint64_t deviceWritten_ = 0;
  AudioTimelineMap timeline_;
  std::vector<float> scratch_;
  wchar_t deviceId_[256] = {};
};
