#pragma once
#include <windows.h>
#include <mmsystem.h>
#include <digitalv.h>
#include <algorithm>
#include <cmath>
#include <cstdio>
#include <cstring>

void Log(const char *fmt, ...);

#include <mfapi.h>
#include <mfidl.h>
#include <mfreadwrite.h>

#pragma comment(lib, "mfplat.lib")
#pragma comment(lib, "mfreadwrite.lib")
#pragma comment(lib, "mfuuid.lib")
#pragma comment(lib, "ole32.lib")

static bool DecodeMp3ToWav(const wchar_t *mp3Path, const wchar_t *wavPath) {
  HRESULT hr;

  hr = CoInitializeEx(nullptr, COINIT_APARTMENTTHREADED);
  bool comOwner = SUCCEEDED(hr);

  hr = MFStartup(MF_VERSION);
  if (FAILED(hr)) {
    Log("[AUDIO] MFStartup failed: 0x%08X", (unsigned)hr);
    if (comOwner) CoUninitialize();
    return false;
  }

  IMFSourceReader *reader = nullptr;
  hr = MFCreateSourceReaderFromURL(mp3Path, nullptr, &reader);
  if (FAILED(hr) || !reader) {
    Log("[AUDIO] MFCreateSourceReaderFromURL failed: 0x%08X", (unsigned)hr);
    MFShutdown();
    if (comOwner) CoUninitialize();
    return false;
  }

  IMFMediaType *pcmType = nullptr;
  MFCreateMediaType(&pcmType);
  pcmType->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio);
  pcmType->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_PCM);
  pcmType->SetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, 16);
  pcmType->SetUINT32(MF_MT_AUDIO_NUM_CHANNELS, 2);
  pcmType->SetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND, 44100);
  pcmType->SetUINT32(MF_MT_AUDIO_BLOCK_ALIGNMENT, 4);
  pcmType->SetUINT32(MF_MT_AUDIO_AVG_BYTES_PER_SECOND, 44100 * 4);

  hr = reader->SetCurrentMediaType(MF_SOURCE_READER_FIRST_AUDIO_STREAM,
                                    nullptr, pcmType);
  pcmType->Release();
  if (FAILED(hr)) {
    Log("[AUDIO] SetCurrentMediaType failed: 0x%08X", (unsigned)hr);
    reader->Release();
    MFShutdown();
    if (comOwner) CoUninitialize();
    return false;
  }

  DWORD totalSize = 0;
  DWORD bufCap = 10 * 1024 * 1024;
  BYTE *pcmBuf = (BYTE *)malloc(bufCap);
  if (!pcmBuf) {
    reader->Release();
    MFShutdown();
    if (comOwner) CoUninitialize();
    return false;
  }

  while (true) {
    DWORD flags = 0;
    IMFSample *sample = nullptr;
    hr = reader->ReadSample(MF_SOURCE_READER_FIRST_AUDIO_STREAM,
                            0, nullptr, &flags, nullptr, &sample);
    if (FAILED(hr) || (flags & MF_SOURCE_READERF_ENDOFSTREAM))
      break;
    if (!sample) continue;

    IMFMediaBuffer *buf = nullptr;
    sample->ConvertToContiguousBuffer(&buf);
    if (buf) {
      BYTE *data = nullptr;
      DWORD len = 0;
      buf->Lock(&data, nullptr, &len);
      if (data && len > 0) {
        if (totalSize + len > bufCap) {
          bufCap = (totalSize + len) * 2;
          pcmBuf = (BYTE *)realloc(pcmBuf, bufCap);
        }
        memcpy(pcmBuf + totalSize, data, len);
        totalSize += len;
      }
      buf->Unlock();
      buf->Release();
    }
    sample->Release();
  }
  reader->Release();
  MFShutdown();
  if (comOwner) CoUninitialize();

  if (totalSize == 0) {
    free(pcmBuf);
    Log("[AUDIO] MP3 decode produced 0 bytes");
    return false;
  }

  FILE *fp = _wfopen(wavPath, L"wb");
  if (!fp) {
    free(pcmBuf);
    Log("[AUDIO] Cannot create temp WAV");
    return false;
  }

  DWORD channels = 2, sampleRate = 44100, bitsPerSample = 16;
  DWORD byteRate = sampleRate * channels * bitsPerSample / 8;
  WORD blockAlign = (WORD)(channels * bitsPerSample / 8);

  fwrite("RIFF", 1, 4, fp);
  DWORD fileSize = 36 + totalSize;
  fwrite(&fileSize, 4, 1, fp);
  fwrite("WAVE", 1, 4, fp);

  fwrite("fmt ", 1, 4, fp);
  DWORD fmtSize = 16;
  fwrite(&fmtSize, 4, 1, fp);
  WORD audioFmt = 1;
  fwrite(&audioFmt, 2, 1, fp);
  WORD ch = (WORD)channels;
  fwrite(&ch, 2, 1, fp);
  fwrite(&sampleRate, 4, 1, fp);
  fwrite(&byteRate, 4, 1, fp);
  fwrite(&blockAlign, 2, 1, fp);
  WORD bps = (WORD)bitsPerSample;
  fwrite(&bps, 2, 1, fp);

  fwrite("data", 1, 4, fp);
  fwrite(&totalSize, 4, 1, fp);
  fwrite(pcmBuf, 1, totalSize, fp);

  fclose(fp);
  free(pcmBuf);

  Log("[AUDIO] MP3 decoded to WAV: %u bytes PCM", totalSize);
  return true;
}

struct AudioPlayer {
  bool loaded = false;
  bool playing = false;
  bool isMp3 = false;
  int lengthMs = 0;
  int speedPermille = 1000;
  MCIDEVICEID devId = 0;
  wchar_t tempWav[MAX_PATH] = {};

  bool Open(const wchar_t *path) {
    Close();
    if (!path || path[0] == L'\0')
      return false;

    const wchar_t *ext = wcsrchr(path, L'.');
    isMp3 = (ext && _wcsicmp(ext, L".mp3") == 0);

    const wchar_t *mciPath = path;

    if (isMp3) {
      wchar_t tempDir[MAX_PATH] = {};
      GetTempPathW(MAX_PATH, tempDir);
      _snwprintf(tempWav, MAX_PATH, L"%seiem_bgm_temp.wav", tempDir);

      if (!DecodeMp3ToWav(path, tempWav)) {
        Log("[AUDIO] MP3 decode failed, cannot play");
        return false;
      }
      mciPath = tempWav;
    }

    MCI_OPEN_PARMSW openParms = {};
    openParms.lpstrDeviceType = L"mpegvideo";
    openParms.lpstrElementName = mciPath;

    DWORD flags = MCI_OPEN_TYPE | MCI_OPEN_ELEMENT | MCI_WAIT;
    MCIERROR err = mciSendCommandW(0, MCI_OPEN, flags, (DWORD_PTR)&openParms);

    if (err != 0) {
      memset(&openParms, 0, sizeof(openParms));
      openParms.lpstrElementName = mciPath;
      flags = MCI_OPEN_ELEMENT | MCI_WAIT;
      err = mciSendCommandW(0, MCI_OPEN, flags, (DWORD_PTR)&openParms);
    }

    if (err != 0) {
      wchar_t errBuf[256] = {};
      mciGetErrorStringW(err, errBuf, 256);
      char narrow[256] = {};
      WideCharToMultiByte(CP_UTF8, 0, errBuf, -1, narrow, sizeof(narrow), nullptr, nullptr);
      char narrowPath[512] = {};
      WideCharToMultiByte(CP_UTF8, 0, path, -1, narrowPath, sizeof(narrowPath), nullptr, nullptr);
      Log("[AUDIO] MCI open failed (err=%u): %s -> %s", (unsigned)err, narrowPath, narrow);
      return false;
    }

    devId = openParms.wDeviceID;

    MCI_SET_PARMS setParms = {};
    setParms.dwTimeFormat = MCI_FORMAT_MILLISECONDS;
    mciSendCommandW(devId, MCI_SET, MCI_SET_TIME_FORMAT | MCI_WAIT, (DWORD_PTR)&setParms);

    MCI_STATUS_PARMS statusParms = {};
    statusParms.dwItem = MCI_STATUS_LENGTH;
    err = mciSendCommandW(devId, MCI_STATUS, MCI_STATUS_ITEM | MCI_WAIT, (DWORD_PTR)&statusParms);
    lengthMs = (err == 0) ? (int)statusParms.dwReturn : 0;

    loaded = true;
    playing = false;
    speedPermille = 1000;
    char narrowPath[512] = {};
    WideCharToMultiByte(CP_UTF8, 0, path, -1, narrowPath, sizeof(narrowPath), nullptr, nullptr);
    Log("[AUDIO] Opened %s, length=%d ms, isMp3=%d", narrowPath, lengthMs, isMp3 ? 1 : 0);
    return true;
  }

  void Play() {
    if (!loaded || !devId)
      return;
    MCI_PLAY_PARMS playParms = {};
    playParms.dwFrom = 0;
    MCIERROR err = mciSendCommandW(devId, MCI_PLAY, MCI_FROM, (DWORD_PTR)&playParms);
    if (err == 0) {
      playing = true;
    } else {
      Log("[AUDIO] Play FAILED (err=%u)", (unsigned)err);
    }
  }

  void PlayFrom(int ms) {
    if (!loaded || !devId)
      return;
    if (ms < 0)
      ms = 0;
    if (lengthMs > 0 && ms > lengthMs)
      ms = lengthMs;
    MCI_PLAY_PARMS playParms = {};
    playParms.dwFrom = (DWORD)ms;
    MCIERROR err = mciSendCommandW(devId, MCI_PLAY, MCI_FROM, (DWORD_PTR)&playParms);
    if (err == 0) {
      playing = true;
    } else {
      Log("[AUDIO] PlayFrom %d ms FAILED (err=%u)", ms, (unsigned)err);
    }
  }

  void Pause() {
    if (!loaded || !playing || !devId)
      return;
    mciSendCommandW(devId, MCI_PAUSE, MCI_WAIT, 0);
    playing = false;
  }

  void Resume() {
    if (!loaded || !devId)
      return;
    MCIERROR err = mciSendCommandW(devId, MCI_RESUME, 0, 0);
    if (err == 0)
      playing = true;
  }

  void Stop() {
    if (!loaded || !devId)
      return;
    mciSendCommandW(devId, MCI_STOP, MCI_WAIT, 0);
    MCI_SEEK_PARMS seekParms = {};
    mciSendCommandW(devId, MCI_SEEK, MCI_SEEK_TO_START | MCI_WAIT, (DWORD_PTR)&seekParms);
    playing = false;
  }

  bool SeekTo(int ms) {
    if (!loaded || !devId)
      return false;
    if (ms < 0)
      ms = 0;
    if (lengthMs > 0 && ms > lengthMs)
      ms = lengthMs;
    if (playing)
      Pause();
    MCI_SEEK_PARMS seekParms = {};
    seekParms.dwTo = static_cast<DWORD>(ms);
    MCIERROR err = mciSendCommandW(
        devId, MCI_SEEK, MCI_TO | MCI_WAIT,
        reinterpret_cast<DWORD_PTR>(&seekParms));
    if (err != 0) {
      Log("[AUDIO] SeekTo %d ms FAILED (err=%u)", ms,
          static_cast<unsigned>(err));
      return false;
    }
    playing = false;
    return true;
  }

  bool SetPlaybackSpeed(float speed) {
    if (!loaded || !devId || !std::isfinite(speed))
      return false;
    const int requested = (std::max)(50, (std::min)(4000,
        static_cast<int>(std::lround(speed * 1000.0f))));
    if (requested == speedPermille)
      return true;
    MCI_DGV_SET_PARMS speedParms = {};
    speedParms.dwSpeed = static_cast<DWORD>(requested);
    MCIERROR err = mciSendCommandW(
        devId, MCI_SET, MCI_DGV_SET_SPEED | MCI_WAIT,
        reinterpret_cast<DWORD_PTR>(&speedParms));
    if (err != 0) {
      static int s_speedErrorCount = 0;
      if (s_speedErrorCount < 4) {
        Log("[P6-AUDIO-SPEED] requested=%d supported=0 err=%u",
            requested, static_cast<unsigned>(err));
        ++s_speedErrorCount;
      }
      return false;
    }
    speedPermille = requested;
    Log("[P6-AUDIO-SPEED] requested=%d supported=1", requested);
    return true;
  }

  void Close() {
    if (loaded && devId) {
      mciSendCommandW(devId, MCI_CLOSE, MCI_WAIT, 0);
      Log("[AUDIO] Closed");
    }
    devId = 0;
    loaded = false;
    playing = false;
    lengthMs = 0;
    speedPermille = 1000;
    if (tempWav[0] != L'\0') {
      DeleteFileW(tempWav);
      tempWav[0] = L'\0';
    }
  }

  int GetPositionMs() {
    if (!loaded || !devId)
      return -1;
    MCI_STATUS_PARMS statusParms = {};
    statusParms.dwItem = MCI_STATUS_POSITION;
    MCIERROR err = mciSendCommandW(devId, MCI_STATUS, MCI_STATUS_ITEM | MCI_WAIT, (DWORD_PTR)&statusParms);
    if (err != 0)
      return -1;
    return (int)statusParms.dwReturn;
  }

  int GetLengthMs() const { return lengthMs; }

  bool AtEnd() {
    if (!loaded || lengthMs <= 0)
      return false;
    int pos = GetPositionMs();
    if (pos < 0)
      return false;
    return pos >= lengthMs - 30;
  }

  void SetVolume(int vol) {
    if (!loaded || !devId)
      return;
    if (vol < 0) vol = 0;
    if (vol > 1000) vol = 1000;
    MCI_DGV_SETAUDIO_PARMSW audioParms = {};
    audioParms.dwItem = MCI_DGV_SETAUDIO_VOLUME;
    audioParms.dwValue = (DWORD)vol;
    MCIERROR err = mciSendCommandW(devId, MCI_SETAUDIO,
                     MCI_DGV_SETAUDIO_ITEM | MCI_DGV_SETAUDIO_VALUE,
                     (DWORD_PTR)&audioParms);
    if (err != 0) {
      static int s_volErrCount = 0;
      if (s_volErrCount < 3) {
        wchar_t errBuf[256] = {};
        mciGetErrorStringW(err, errBuf, 256);
        char narrow[256] = {};
        WideCharToMultiByte(CP_UTF8, 0, errBuf, -1, narrow, sizeof(narrow), nullptr, nullptr);
        Log("[AUDIO] SetVolume(%d) failed: %s", vol, narrow);
        s_volErrCount++;
      }
    }
  }
};
