#pragma once
#include <cstdio>
#include <cstdint>
#include <cstring>
#include <cmath>

struct BoneFrame {
  float hipsDeltaPos[3];
  float bones[55][4];
};

struct BoneAnim {
  bool loaded = false;
  float fps = 30.0f;
  uint32_t frameCount = 0;
  uint32_t boneCount = 0;
  float restPose[55][4];
  BoneFrame *frames = nullptr;

  ~BoneAnim() { delete[] frames; }

  float Duration() const { return frameCount > 0 ? (frameCount - 1) / fps : 0.0f; }

  bool Load(const char *path) {
    FILE *f = fopen(path, "rb");
    if (!f) return false;

    char magic[4];
    fread(magic, 1, 4, f);

    uint32_t version;
    fread(&version, 4, 1, f);

    bool isDelta = (memcmp(magic, "BNED", 4) == 0 && version == 2);
    bool isAbsolute = (memcmp(magic, "BONE", 4) == 0 && version == 1);

    if (!isDelta && !isAbsolute) { fclose(f); return false; }

    fread(&fps, 4, 1, f);
    fread(&frameCount, 4, 1, f);
    fread(&boneCount, 4, 1, f);

    if (frameCount == 0 || boneCount == 0 || boneCount > 55) {
      fclose(f); return false;
    }

    if (isDelta) {
      for (uint32_t b = 0; b < boneCount; b++) {
        fread(restPose[b], sizeof(float), 4, f);
      }
    } else {
      for (uint32_t b = 0; b < 55; b++) {
        restPose[b][0] = 0; restPose[b][1] = 0;
        restPose[b][2] = 0; restPose[b][3] = 1;
      }
    }

    frames = new BoneFrame[frameCount];

    for (uint32_t i = 0; i < frameCount; i++) {
      if (isDelta) {
        fread(frames[i].hipsDeltaPos, sizeof(float), 3, f);
      } else {
        float rootPos[3], rootRot[4];
        fread(rootPos, sizeof(float), 3, f);
        fread(rootRot, sizeof(float), 4, f);
        frames[i].hipsDeltaPos[0] = 0;
        frames[i].hipsDeltaPos[1] = 0;
        frames[i].hipsDeltaPos[2] = 0;
      }

      for (uint32_t b = 0; b < boneCount; b++) {
        fread(frames[i].bones[b], sizeof(float), 4, f);
      }
      for (uint32_t b = boneCount; b < 55; b++) {
        frames[i].bones[b][0] = 0; frames[i].bones[b][1] = 0;
        frames[i].bones[b][2] = 0; frames[i].bones[b][3] = 1;
      }
    }

    fclose(f);
    loaded = true;
    return true;
  }

  static void QLerp(const float a[4], const float b[4], float t, float out[4]) {
    float dot = a[0]*b[0] + a[1]*b[1] + a[2]*b[2] + a[3]*b[3];
    float sign = dot < 0 ? -1.0f : 1.0f;
    float it = 1.0f - t;
    out[0] = it * a[0] + t * sign * b[0];
    out[1] = it * a[1] + t * sign * b[1];
    out[2] = it * a[2] + t * sign * b[2];
    out[3] = it * a[3] + t * sign * b[3];
    float len = sqrtf(out[0]*out[0] + out[1]*out[1] + out[2]*out[2] + out[3]*out[3]);
    if (len > 1e-6f) { out[0]/=len; out[1]/=len; out[2]/=len; out[3]/=len; }
  }

  BoneFrame GetFrame(float time) const {
    if (!loaded || frameCount == 0) {
      BoneFrame empty = {};
      for (int i = 0; i < 55; i++) empty.bones[i][3] = 1.0f;
      return empty;
    }

    float frameF = time * fps;
    int f0 = (int)frameF;
    float frac = frameF - f0;

    if (f0 < 0) { f0 = 0; frac = 0; }
    if (f0 >= (int)frameCount - 1) {
      return frames[frameCount - 1];
    }

    int f1 = f0 + 1;
    const BoneFrame &a = frames[f0];
    const BoneFrame &b = frames[f1];
    BoneFrame result;

    for (int i = 0; i < 3; i++)
      result.hipsDeltaPos[i] = a.hipsDeltaPos[i] + frac * (b.hipsDeltaPos[i] - a.hipsDeltaPos[i]);

    for (int bone = 0; bone < 55; bone++) {
      QLerp(a.bones[bone], b.bones[bone], frac, result.bones[bone]);
    }

    return result;
  }
};
