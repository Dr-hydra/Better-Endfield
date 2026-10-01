#pragma once

#include <algorithm>
#include <cmath>
#include <cstdint>

struct AudioTimelineSegment {
  uint64_t deviceStart = 0;
  uint64_t deviceCount = 0;
  double mediaStartSeconds = 0.0;
  double mediaSecondsPerDeviceFrame = 0.0;
  uint32_t epoch = 0;
  uint8_t media = 0;
};

struct AudioTimelinePoint {
  uint8_t found = 0;
  uint8_t media = 0;
  uint32_t epoch = 0;
  double mediaSeconds = 0.0;
  double mediaSecondsPerDeviceFrame = 0.0;
};

static constexpr uint32_t kAudioTimelineCapacity = 512;

struct AudioTimelineMap {
  AudioTimelineSegment segments[kAudioTimelineCapacity];
  uint32_t head = 0;
  uint32_t count = 0;

  void Clear() {
    head = 0;
    count = 0;
  }

  void Append(const AudioTimelineSegment &segment) {
    if (segment.deviceCount == 0)
      return;
    if (count > 0) {
      AudioTimelineSegment &last =
          segments[(head + kAudioTimelineCapacity - 1) %
                   kAudioTimelineCapacity];
      const double expectedMedia =
          last.mediaStartSeconds +
          static_cast<double>(last.deviceCount) *
              last.mediaSecondsPerDeviceFrame;
      if (last.deviceStart + last.deviceCount == segment.deviceStart &&
          last.epoch == segment.epoch && last.media == segment.media &&
          std::fabs(last.mediaSecondsPerDeviceFrame -
                    segment.mediaSecondsPerDeviceFrame) <= 1.0e-15 &&
          std::fabs(expectedMedia - segment.mediaStartSeconds) <= 1.0e-6) {
        last.deviceCount += segment.deviceCount;
        return;
      }
    }
    segments[head] = segment;
    head = (head + 1) % kAudioTimelineCapacity;
    if (count < kAudioTimelineCapacity)
      ++count;
  }

  AudioTimelinePoint Lookup(double deviceFrame) const {
    AudioTimelinePoint point;
    if (count == 0 || !std::isfinite(deviceFrame))
      return point;
    for (uint32_t i = 0; i < count; ++i) {
      const AudioTimelineSegment &segment =
          segments[(head + kAudioTimelineCapacity - 1 - i) %
                   kAudioTimelineCapacity];
      if (deviceFrame < static_cast<double>(segment.deviceStart))
        continue;
      const double offset = (std::min)(
          deviceFrame - static_cast<double>(segment.deviceStart),
          static_cast<double>(segment.deviceCount));
      point.found = 1;
      point.media = segment.media;
      point.epoch = segment.epoch;
      point.mediaSecondsPerDeviceFrame = segment.mediaSecondsPerDeviceFrame;
      point.mediaSeconds =
          segment.mediaStartSeconds +
          offset * segment.mediaSecondsPerDeviceFrame;
      return point;
    }
    return point;
  }
};

static inline float AudioHermite(float xm1, float x0, float x1, float x2,
                                 float t) {
  const float c = (x1 - xm1) * 0.5f;
  const float v = x0 - x1;
  const float w = c + v;
  const float a = w + v + (x2 - x0) * 0.5f;
  const float bNeg = w + a;
  return (((a * t) - bNeg) * t + c) * t + x0;
}

static inline void AudioSampleStereo(const int16_t *pcm, uint64_t frames,
                                     double position, float *left,
                                     float *right) {
  *left = 0.0f;
  *right = 0.0f;
  if (!pcm || frames == 0 || !std::isfinite(position))
    return;
  const double base = std::floor(position);
  const float t = static_cast<float>(position - base);
  const int64_t index = static_cast<int64_t>(base);
  float l[4] = {};
  float r[4] = {};
  for (int k = 0; k < 4; ++k) {
    const int64_t i = index - 1 + k;
    if (i < 0 || static_cast<uint64_t>(i) >= frames)
      continue;
    l[k] = pcm[i * 2] * (1.0f / 32768.0f);
    r[k] = pcm[i * 2 + 1] * (1.0f / 32768.0f);
  }
  *left = AudioHermite(l[0], l[1], l[2], l[3], t);
  *right = AudioHermite(r[0], r[1], r[2], r[3], t);
}
