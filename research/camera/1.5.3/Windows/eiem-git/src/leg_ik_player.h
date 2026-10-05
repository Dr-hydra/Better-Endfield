#pragma once

#include <cmath>

#define IK_MMD_SCALE 0.08f

static const char *IK_BONE_LEFT_FOOT  = "\xe5\xb7\xa6\xe8\xb6\xb3\xef\xbc\xa9\xef\xbc\xab";
static const char *IK_BONE_RIGHT_FOOT = "\xe5\x8f\xb3\xe8\xb6\xb3\xef\xbc\xa9\xef\xbc\xab";
static const char *IK_BONE_LEFT_TOE   = "\xe5\xb7\xa6\xe3\x81\xa4\xe3\x81\xbe\xe5\x85\x88\xef\xbc\xa9\xef\xbc\xab";
static const char *IK_BONE_RIGHT_TOE  = "\xe5\x8f\xb3\xe3\x81\xa4\xe3\x81\xbe\xe5\x85\x88\xef\xbc\xa9\xef\xbc\xab";

static const char *IK_BONE_LEFT_FOOT_HW  = "\xe5\xb7\xa6\xe8\xb6\xb3IK";
static const char *IK_BONE_RIGHT_FOOT_HW = "\xe5\x8f\xb3\xe8\xb6\xb3IK";
static const char *IK_BONE_LEFT_TOE_HW   = "\xe5\xb7\xa6\xe3\x81\xa4\xe3\x81\xbe\xe5\x85\x88IK";
static const char *IK_BONE_RIGHT_TOE_HW  = "\xe5\x8f\xb3\xe3\x81\xa4\xe3\x81\xbe\xe5\x85\x88IK";

struct LegIKTarget {
  float pos[3];
  float rot[4];
  bool valid;
};

struct LegIKState {
  const VmdBoneTimeline *leftFoot;
  const VmdBoneTimeline *rightFoot;
  const VmdBoneTimeline *leftToe;
  const VmdBoneTimeline *rightToe;

  const VmdIKTimeline *leftFootIK;
  const VmdIKTimeline *rightFootIK;
  const VmdIKTimeline *leftToeIK;
  const VmdIKTimeline *rightToeIK;

  float leftFootBase[3];
  float rightFootBase[3];

  const VmdBoneTimeline *center;
  const VmdBoneTimeline *groove;
  const VmdBoneTimeline *master;

  bool hasData;
};

static LegIKState g_legIK = {};

static const VmdBoneTimeline *FindIKBoneTimeline(
    const VmdFile *vmd, const char *fullWidth, const char *halfWidth) {
  auto it = vmd->boneTimelines.find(fullWidth);
  if (it != vmd->boneTimelines.end() && !it->second.keys.empty())
    return &it->second;
  it = vmd->boneTimelines.find(halfWidth);
  if (it != vmd->boneTimelines.end() && !it->second.keys.empty())
    return &it->second;
  return nullptr;
}

static const VmdIKTimeline *FindIKOnOffTimeline(
    const VmdFile *vmd, const char *fullWidth, const char *halfWidth) {
  auto it = vmd->ikTimelines.find(fullWidth);
  if (it != vmd->ikTimelines.end() && !it->second.keys.empty())
    return &it->second;
  it = vmd->ikTimelines.find(halfWidth);
  if (it != vmd->ikTimelines.end() && !it->second.keys.empty())
    return &it->second;
  return nullptr;
}

static void LegIK_Init(const VmdFile *vmd) {
  memset(&g_legIK, 0, sizeof(g_legIK));

  if (!vmd || !vmd->loaded) return;

  g_legIK.leftFoot  = FindIKBoneTimeline(vmd, IK_BONE_LEFT_FOOT,  IK_BONE_LEFT_FOOT_HW);
  g_legIK.rightFoot = FindIKBoneTimeline(vmd, IK_BONE_RIGHT_FOOT, IK_BONE_RIGHT_FOOT_HW);
  g_legIK.leftToe   = FindIKBoneTimeline(vmd, IK_BONE_LEFT_TOE,   IK_BONE_LEFT_TOE_HW);
  g_legIK.rightToe  = FindIKBoneTimeline(vmd, IK_BONE_RIGHT_TOE,  IK_BONE_RIGHT_TOE_HW);

  g_legIK.leftFootIK  = FindIKOnOffTimeline(vmd, IK_BONE_LEFT_FOOT,  IK_BONE_LEFT_FOOT_HW);
  g_legIK.rightFootIK = FindIKOnOffTimeline(vmd, IK_BONE_RIGHT_FOOT, IK_BONE_RIGHT_FOOT_HW);
  g_legIK.leftToeIK   = FindIKOnOffTimeline(vmd, IK_BONE_LEFT_TOE,   IK_BONE_LEFT_TOE_HW);
  g_legIK.rightToeIK  = FindIKOnOffTimeline(vmd, IK_BONE_RIGHT_TOE,  IK_BONE_RIGHT_TOE_HW);

  g_legIK.hasData = (g_legIK.leftFoot || g_legIK.rightFoot);

  if (g_legIK.leftFoot && !g_legIK.leftFoot->keys.empty()) {
    const auto &k = g_legIK.leftFoot->keys.front();
    g_legIK.leftFootBase[0] = k.pos[0];
    g_legIK.leftFootBase[1] = k.pos[1];
    g_legIK.leftFootBase[2] = k.pos[2];
    Log("[LEG-IK] Left foot baseline: (%.3f, %.3f, %.3f)",
        g_legIK.leftFootBase[0], g_legIK.leftFootBase[1], g_legIK.leftFootBase[2]);
  }
  if (g_legIK.rightFoot && !g_legIK.rightFoot->keys.empty()) {
    const auto &k = g_legIK.rightFoot->keys.front();
    g_legIK.rightFootBase[0] = k.pos[0];
    g_legIK.rightFootBase[1] = k.pos[1];
    g_legIK.rightFootBase[2] = k.pos[2];
    Log("[LEG-IK] Right foot baseline: (%.3f, %.3f, %.3f)",
        g_legIK.rightFootBase[0], g_legIK.rightFootBase[1], g_legIK.rightFootBase[2]);
  }

  {
    const char *centerName = "\xe3\x82\xbb\xe3\x83\xb3\xe3\x82\xbf\xe3\x83\xbc";
    const char *grooveName = "\xe3\x82\xb0\xe3\x83\xab\xe3\x83\xbc\xe3\x83\x96";
    const char *masterName = "\xe5\x85\xa8\xe3\x81\xa6\xe3\x81\xae\xe8\xa6\xaa";

    auto it = vmd->boneTimelines.find(centerName);
    if (it != vmd->boneTimelines.end() && !it->second.keys.empty()) {
      g_legIK.center = &it->second;
      const auto &k = it->second.keys.front();
      Log("[LEG-IK] Center bone: %zu keys, frame0=(%.3f, %.3f, %.3f)",
          it->second.keys.size(), k.pos[0], k.pos[1], k.pos[2]);
    } else {
      Log("[LEG-IK] WARNING: Center bone not found");
    }

    it = vmd->boneTimelines.find(grooveName);
    if (it != vmd->boneTimelines.end() && !it->second.keys.empty()) {
      g_legIK.groove = &it->second;
      const auto &k = it->second.keys.front();
      Log("[LEG-IK] Groove bone: %zu keys, frame0=(%.3f, %.3f, %.3f)",
          it->second.keys.size(), k.pos[0], k.pos[1], k.pos[2]);
    } else {
      Log("[LEG-IK] Groove bone not found (may not exist in this VMD)");
    }

    it = vmd->boneTimelines.find(masterName);
    if (it != vmd->boneTimelines.end() && !it->second.keys.empty()) {
      g_legIK.master = &it->second;
      const auto &k = it->second.keys.front();
      Log("[LEG-IK] Master bone: %zu keys, frame0=(%.3f, %.3f, %.3f)",
          it->second.keys.size(), k.pos[0], k.pos[1], k.pos[2]);
    } else {
      Log("[LEG-IK] Master bone not found (may not exist in this VMD)");
    }
  }

  {
    Log("[LEG-IK] All bone timelines with position data:");
    for (const auto &pair : vmd->boneTimelines) {
      float maxPos = 0;
      for (const auto &k : pair.second.keys) {
        float m = fabsf(k.pos[0]) + fabsf(k.pos[1]) + fabsf(k.pos[2]);
        if (m > maxPos) maxPos = m;
      }
      if (maxPos > 0.01f) {
        const auto &k0 = pair.second.keys.front();
        Log("[LEG-IK]   '%s': %zu keys, frame0=(%.3f,%.3f,%.3f) maxPosMag=%.2f",
            pair.first.c_str(), pair.second.keys.size(),
            k0.pos[0], k0.pos[1], k0.pos[2], maxPos);
      }
    }
  }

  Log("[LEG-IK] Init: leftFoot=%s rightFoot=%s leftToe=%s rightToe=%s",
      g_legIK.leftFoot  ? "YES" : "NO",
      g_legIK.rightFoot ? "YES" : "NO",
      g_legIK.leftToe   ? "YES" : "NO",
      g_legIK.rightToe  ? "YES" : "NO");

  if (!vmd->ikTimelines.empty()) {
    Log("[LEG-IK] IK On/Off timelines: %zu", vmd->ikTimelines.size());
    for (const auto &pair : vmd->ikTimelines) {
      Log("[LEG-IK]   '%s': %zu keys", pair.first.c_str(), pair.second.keys.size());
    }
  }
}

static void SampleBoneKeyframe(const VmdBoneTimeline *tl, float frameF,
                                float outPos[3], float outRot[4]) {
  if (!tl || tl->keys.empty()) {
    outPos[0] = outPos[1] = outPos[2] = 0;
    outRot[0] = outRot[1] = outRot[2] = 0; outRot[3] = 1;
    return;
  }

  const auto &keys = tl->keys;
  if (frameF <= keys.front().frame) {
    memcpy(outPos, keys.front().pos, 12);
    memcpy(outRot, keys.front().rot, 16);
    return;
  }
  if (frameF >= keys.back().frame) {
    memcpy(outPos, keys.back().pos, 12);
    memcpy(outRot, keys.back().rot, 16);
    return;
  }

  int lo = 0, hi = (int)keys.size() - 1;
  while (lo < hi - 1) {
    int mid = (lo + hi) / 2;
    if (keys[mid].frame <= frameF) lo = mid;
    else hi = mid;
  }

  float t = (frameF - keys[lo].frame) / (float)(keys[hi].frame - keys[lo].frame);

  for (int i = 0; i < 3; i++)
    outPos[i] = keys[lo].pos[i] + (keys[hi].pos[i] - keys[lo].pos[i]) * t;

  float ax = keys[lo].rot[0], ay = keys[lo].rot[1], az = keys[lo].rot[2], aw = keys[lo].rot[3];
  float bx = keys[hi].rot[0], by = keys[hi].rot[1], bz = keys[hi].rot[2], bw = keys[hi].rot[3];

  float dot = ax*bx + ay*by + az*bz + aw*bw;
  if (dot < 0) { bx = -bx; by = -by; bz = -bz; bw = -bw; dot = -dot; }

  if (dot > 0.9995f) {
    outRot[0] = ax + (bx - ax) * t;
    outRot[1] = ay + (by - ay) * t;
    outRot[2] = az + (bz - az) * t;
    outRot[3] = aw + (bw - aw) * t;
  } else {
    float theta = acosf(dot < 1.0f ? dot : 1.0f);
    float sinT = sinf(theta);
    float w0 = sinf((1 - t) * theta) / sinT;
    float w1 = sinf(t * theta) / sinT;
    outRot[0] = ax * w0 + bx * w1;
    outRot[1] = ay * w0 + by * w1;
    outRot[2] = az * w0 + bz * w1;
    outRot[3] = aw * w0 + bw * w1;
  }

  float len = sqrtf(outRot[0]*outRot[0] + outRot[1]*outRot[1] +
                    outRot[2]*outRot[2] + outRot[3]*outRot[3]);
  if (len > 0.0001f) {
    outRot[0] /= len; outRot[1] /= len;
    outRot[2] /= len; outRot[3] /= len;
  }
}

static void TransformIKToWorld(const float mmdPos[3], const float mmdRot[4],
                                float scale, const Vec3 &charPos, float charYawRad,
                                float outPos[3], float outRot[4]) {
  float s = IK_MMD_SCALE * scale;
  float ux = -mmdPos[0] * s;
  float uy =  mmdPos[1] * s;
  float uz =  mmdPos[2] * s;

  float cy = cosf(charYawRad), sy = sinf(charYawRad);
  float wx = ux * cy - uz * sy;
  float wz = ux * sy + uz * cy;

  outPos[0] = charPos.x + wx;
  outPos[1] = charPos.y + uy;
  outPos[2] = charPos.z + wz;

  if (outRot) {
    outRot[0] =  mmdRot[0];
    outRot[1] = -mmdRot[1];
    outRot[2] = -mmdRot[2];
    outRot[3] =  mmdRot[3];

    float qy[4] = { 0, sinf(charYawRad * 0.5f), 0, cosf(charYawRad * 0.5f) };
    float rx = qy[3]*outRot[0] + qy[0]*outRot[3] + qy[1]*outRot[2] - qy[2]*outRot[1];
    float ry = qy[3]*outRot[1] - qy[0]*outRot[2] + qy[1]*outRot[3] + qy[2]*outRot[0];
    float rz = qy[3]*outRot[2] + qy[0]*outRot[1] - qy[1]*outRot[0] + qy[2]*outRot[3];
    float rw = qy[3]*outRot[3] - qy[0]*outRot[0] - qy[1]*outRot[1] - qy[2]*outRot[2];
    outRot[0] = rx; outRot[1] = ry; outRot[2] = rz; outRot[3] = rw;
  }
}

static void LegIK_Sample(float frameF, float scale,
                          const Vec3 &charPos, float charYawRad,
                          LegIKTarget &leftFoot, LegIKTarget &rightFoot) {
  leftFoot.valid = false;
  rightFoot.valid = false;

  if (!g_legIK.hasData) return;

  bool leftEnabled  = g_legIK.leftFootIK  ? g_legIK.leftFootIK->IsEnabled(frameF) : true;
  bool rightEnabled = g_legIK.rightFootIK ? g_legIK.rightFootIK->IsEnabled(frameF) : true;

  if (g_legIK.leftFoot && leftEnabled) {
    float mmdPos[3], mmdRot[4];
    SampleBoneKeyframe(g_legIK.leftFoot, frameF, mmdPos, mmdRot);
    mmdPos[0] -= g_legIK.leftFootBase[0];
    mmdPos[1] -= g_legIK.leftFootBase[1];
    mmdPos[2] -= g_legIK.leftFootBase[2];
    TransformIKToWorld(mmdPos, mmdRot, scale, charPos, charYawRad,
                       leftFoot.pos, leftFoot.rot);
    leftFoot.valid = true;
  }

  if (g_legIK.rightFoot && rightEnabled) {
    float mmdPos[3], mmdRot[4];
    SampleBoneKeyframe(g_legIK.rightFoot, frameF, mmdPos, mmdRot);
    mmdPos[0] -= g_legIK.rightFootBase[0];
    mmdPos[1] -= g_legIK.rightFootBase[1];
    mmdPos[2] -= g_legIK.rightFootBase[2];
    TransformIKToWorld(mmdPos, mmdRot, scale, charPos, charYawRad,
                       rightFoot.pos, rightFoot.rot);
    rightFoot.valid = true;
  }
}
