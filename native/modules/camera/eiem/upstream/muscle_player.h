#pragma once
#include <cstdio>
#include <cstdint>
#include <cstring>
#include <cmath>
#include <vector>
#include <string>

#define ARM_BONE_COUNT 6
#define FINGER_BONE_COUNT 30
#define MAX_BLEND_SHAPES 128

struct MuscleFrame {
  float bodyPos[3];
  float bodyRot[4];
  float muscles[95];
  float armBoneRots[ARM_BONE_COUNT * 4];
  float fingerBoneRots[FINGER_BONE_COUNT * 4];
  float blendShapes[MAX_BLEND_SHAPES];
};

struct MuscleAnim {
  float fps = 30.0f;
  int frameCount = 0;
  int muscleCount = 95;
  int armBoneCount = 0;
  int fingerBoneCount = 0;
  int blendShapeCount = 0;
  float armRestRots[ARM_BONE_COUNT * 4] = {};
  float fingerRestRots[FINGER_BONE_COUNT * 4] = {};
  std::vector<std::string> blendShapeNames;
  std::vector<MuscleFrame> frames;
  bool loaded = false;
  bool hasArmBones = false;
  bool hasFingerBones = false;
  bool hasBlendShapes = false;

  bool Load(const char *path) {
    FILE *f = fopen(path, "rb");
    if (!f) return false;

    char hdr[4];
    fread(hdr, 1, 4, f);

    bool v4 = (memcmp(hdr, "MUS4", 4) == 0);
    bool v3 = (memcmp(hdr, "MUS3", 4) == 0);
    bool v2 = (memcmp(hdr, "MUS2", 4) == 0);
    bool v1 = (memcmp(hdr, "MUSC", 4) == 0);

    if (!v1 && !v2 && !v3 && !v4) {
      fclose(f);
      return false;
    }

    fread(&fps, sizeof(float), 1, f);
    fread(&frameCount, sizeof(int), 1, f);
    fread(&muscleCount, sizeof(int), 1, f);

    if (v2 || v3 || v4) {
      fread(&armBoneCount, sizeof(int), 1, f);
      if (armBoneCount > ARM_BONE_COUNT) armBoneCount = ARM_BONE_COUNT;
      hasArmBones = (armBoneCount > 0);
    }

    if (v3 || v4) {
      fread(&fingerBoneCount, sizeof(int), 1, f);
      if (fingerBoneCount > FINGER_BONE_COUNT) fingerBoneCount = FINGER_BONE_COUNT;
      hasFingerBones = (fingerBoneCount > 0);
    }

    if (v4) {
      fread(&blendShapeCount, sizeof(int), 1, f);
      if (blendShapeCount > MAX_BLEND_SHAPES) blendShapeCount = MAX_BLEND_SHAPES;
      hasBlendShapes = (blendShapeCount > 0);
    }

    if (v2 || v3 || v4) {
      fread(armRestRots, sizeof(float), armBoneCount * 4, f);
    }

    if (v3 || v4) {
      fread(fingerRestRots, sizeof(float), fingerBoneCount * 4, f);
    }

    if (v4 && blendShapeCount > 0) {
      blendShapeNames.resize(blendShapeCount);
      for (int i = 0; i < blendShapeCount; i++) {
        int nameLen = 0;
        fread(&nameLen, sizeof(int), 1, f);
        if (nameLen > 0 && nameLen < 1024) {
          std::vector<char> buf(nameLen + 1, 0);
          fread(buf.data(), 1, nameLen, f);
          blendShapeNames[i] = std::string(buf.data(), nameLen);
        }
      }
    }

    if (frameCount <= 0 || frameCount > 100000 || muscleCount != 95) {
      fclose(f);
      return false;
    }

    frames.resize(frameCount);
    for (int i = 0; i < frameCount; i++) {
      fread(frames[i].bodyPos, sizeof(float), 3, f);
      fread(frames[i].bodyRot, sizeof(float), 4, f);
      fread(frames[i].muscles, sizeof(float), 95, f);

      if ((v2 || v3 || v4) && armBoneCount > 0) {
        fread(frames[i].armBoneRots, sizeof(float), armBoneCount * 4, f);
      } else {
        memset(frames[i].armBoneRots, 0, sizeof(frames[i].armBoneRots));
      }

      if ((v3 || v4) && fingerBoneCount > 0) {
        fread(frames[i].fingerBoneRots, sizeof(float), fingerBoneCount * 4, f);
      } else {
        for (int b = 0; b < FINGER_BONE_COUNT; b++) {
          frames[i].fingerBoneRots[b*4+0] = 0;
          frames[i].fingerBoneRots[b*4+1] = 0;
          frames[i].fingerBoneRots[b*4+2] = 0;
          frames[i].fingerBoneRots[b*4+3] = 1;
        }
      }

      if (v4 && blendShapeCount > 0) {
        fread(frames[i].blendShapes, sizeof(float), blendShapeCount, f);
      } else {
        memset(frames[i].blendShapes, 0, sizeof(frames[i].blendShapes));
      }
    }

    fclose(f);
    loaded = true;
    return true;
  }

  static void QuatSlerp(const float *a, const float *b, float t, float *out) {
    float dot = a[0]*b[0] + a[1]*b[1] + a[2]*b[2] + a[3]*b[3];
    float nb[4] = { b[0], b[1], b[2], b[3] };
    if (dot < 0) {
      dot = -dot;
      nb[0] = -nb[0]; nb[1] = -nb[1]; nb[2] = -nb[2]; nb[3] = -nb[3];
    }
    if (dot > 0.9995f) {
      for (int i = 0; i < 4; i++) out[i] = a[i] + (nb[i] - a[i]) * t;
    } else {
      float theta = acosf(dot);
      float sinTheta = sinf(theta);
      float wa = sinf((1.0f - t) * theta) / sinTheta;
      float wb = sinf(t * theta) / sinTheta;
      for (int i = 0; i < 4; i++) out[i] = wa * a[i] + wb * nb[i];
    }
    float len = sqrtf(out[0]*out[0] + out[1]*out[1] + out[2]*out[2] + out[3]*out[3]);
    if (len > 0.0001f) { for (int i = 0; i < 4; i++) out[i] /= len; }
  }

  MuscleFrame GetFrame(float t) const {
    if (!loaded || frames.empty()) {
      MuscleFrame zero = {};
      zero.bodyRot[3] = 1.0f;
      for (int i = 0; i < ARM_BONE_COUNT; i++) zero.armBoneRots[i*4+3] = 1.0f;
      for (int i = 0; i < FINGER_BONE_COUNT; i++) zero.fingerBoneRots[i*4+3] = 1.0f;
      return zero;
    }

    float frameF = t * fps;
    int f0 = (int)frameF;
    float frac = frameF - f0;

    if (f0 < 0) { f0 = 0; frac = 0; }
    if (f0 >= frameCount - 1) { f0 = frameCount - 1; frac = 0; }
    int f1 = (f0 + 1 < frameCount) ? f0 + 1 : f0;

    MuscleFrame result;
    const MuscleFrame &a = frames[f0];
    const MuscleFrame &b = frames[f1];

    for (int i = 0; i < 3; i++)
      result.bodyPos[i] = a.bodyPos[i] + (b.bodyPos[i] - a.bodyPos[i]) * frac;
    for (int i = 0; i < 4; i++)
      result.bodyRot[i] = a.bodyRot[i] + (b.bodyRot[i] - a.bodyRot[i]) * frac;
    for (int i = 0; i < 95; i++)
      result.muscles[i] = a.muscles[i] + (b.muscles[i] - a.muscles[i]) * frac;

    if (hasArmBones) {
      for (int bone = 0; bone < armBoneCount; bone++)
        QuatSlerp(&a.armBoneRots[bone*4], &b.armBoneRots[bone*4], frac, &result.armBoneRots[bone*4]);
    }

    if (hasFingerBones) {
      for (int bone = 0; bone < fingerBoneCount; bone++)
        QuatSlerp(&a.fingerBoneRots[bone*4], &b.fingerBoneRots[bone*4], frac, &result.fingerBoneRots[bone*4]);
    }

    if (hasBlendShapes) {
      for (int i = 0; i < blendShapeCount; i++)
        result.blendShapes[i] = a.blendShapes[i] + (b.blendShapes[i] - a.blendShapes[i]) * frac;
    }

    float len = 0;
    for (int i = 0; i < 4; i++) len += result.bodyRot[i] * result.bodyRot[i];
    if (len > 0.0001f) {
      len = sqrtf(len);
      for (int i = 0; i < 4; i++) result.bodyRot[i] /= len;
    }

    return result;
  }

  float Duration() const {
    return loaded ? (frameCount - 1) / fps : 0;
  }
};
