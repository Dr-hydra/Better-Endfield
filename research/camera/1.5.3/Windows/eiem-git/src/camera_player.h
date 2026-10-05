#pragma once
#include <cmath>
#include <cstdint>
#include <vector>
#include "vmd_parser.h"
#include "mmd_player.h"

#define CAM_SIGN_RX (-1.0f)
#define CAM_SIGN_RY (-1.0f)
#define CAM_SIGN_RZ (-1.0f)

#define CAM_SCALE    0.07f
#define CAM_FOV_BIAS 5.0f
#define CAM_YAW_BIAS 180.0f
#define CAM_YAW_SIGN 1.0f

#define CAM_REF_HEIGHT 1.245f

struct DirectVmdCameraFraming {
  float positionScale = 0.0f;
  float automaticHeightScale = 1.0f;
  float effectiveHeightScale = 1.0f;
  float fovBias = 0.0f;
  float fov = 60.0f;
  bool legacyOverride = false;
  bool usedNaturalBindHeight = false;
};

static inline bool DirectVmdResolveCameraFraming(
    bool fromOverride, float sharedMotionScale, float naturalBindHeight,
    float legacyHeightBias, float sampledFov,
    DirectVmdCameraFraming *framing) {
  if (!framing || !DirectVmdFinite(sharedMotionScale) ||
      sharedMotionScale <= 0.0f || !DirectVmdFinite(legacyHeightBias) ||
      !DirectVmdFinite(sampledFov))
    return false;

  DirectVmdCameraFraming result;
  result.legacyOverride = fromOverride;
  if (fromOverride) {
    const bool validNaturalHeight =
        DirectVmdFinite(naturalBindHeight) && naturalBindHeight > 0.1f &&
        naturalBindHeight < 5.0f && CAM_REF_HEIGHT > 0.1f;
    result.usedNaturalBindHeight = validNaturalHeight;
    result.automaticHeightScale =
        validNaturalHeight ? naturalBindHeight / CAM_REF_HEIGHT : 1.0f;
    result.effectiveHeightScale =
        result.automaticHeightScale + legacyHeightBias;
    result.positionScale = CAM_SCALE * result.effectiveHeightScale;
    result.fovBias = CAM_FOV_BIAS;
  } else {
    result.positionScale = sharedMotionScale;
  }

  if (!DirectVmdFinite(result.positionScale) ||
      result.positionScale <= 0.0f)
    return false;
  result.fov = sampledFov + result.fovBias;
  if (!DirectVmdFinite(result.fov))
    return false;
  if (result.fov < 1.0f)
    result.fov = 1.0f;
  else if (result.fov > 179.0f)
    result.fov = 179.0f;
  *framing = result;
  return true;
}

struct CameraState {
  Vec3 position;
  Quat rotation;
  float fov;
  Vec3 interest;
  Vec3 euler;
  bool valid;
};

static inline float CamBezierAxis(float p1, float p2, float s) {
  float u = 1.0f - s;
  return 3.0f * u * u * s * p1 + 3.0f * u * s * s * p2 + s * s * s;
}

static inline float CamBezierEval(uint8_t bx1, uint8_t by1, uint8_t bx2,
                                  uint8_t by2, float t) {
  float x1 = bx1 / 127.0f, y1 = by1 / 127.0f;
  float x2 = bx2 / 127.0f, y2 = by2 / 127.0f;
  if (fabsf(x1 - y1) < 1e-4f && fabsf(x2 - y2) < 1e-4f) return t;
  if (t <= 0.0f) return 0.0f;
  if (t >= 1.0f) return 1.0f;
  float s = t;
  for (int i = 0; i < 12; i++) {
    float x = CamBezierAxis(x1, x2, s) - t;
    if (fabsf(x) < 1e-5f) break;
    float u = 1.0f - s;
    float dx = 3.0f * u * u * x1 + 6.0f * u * s * (x2 - x1) +
               3.0f * s * s * (1.0f - x2);
    if (fabsf(dx) < 1e-6f) break;
    s -= x / dx;
    if (s < 0.0f) s = 0.0f;
    if (s > 1.0f) s = 1.0f;
  }
  return CamBezierAxis(y1, y2, s);
}

static inline Quat CamQuatFromEuler(float rx, float ry, float rz) {
  rx *= CAM_SIGN_RX;
  ry *= CAM_SIGN_RY;
  rz *= CAM_SIGN_RZ;
  float hx = rx * 0.5f, hy = ry * 0.5f, hz = rz * 0.5f;
  Quat qx = {sinf(hx), 0, 0, cosf(hx)};
  Quat qy = {0, sinf(hy), 0, cosf(hy)};
  Quat qz = {0, 0, sinf(hz), cosf(hz)};
  return QuatMul(QuatMul(qy, qx), qz);
}

static inline Vec3 CamQuatRotate(Quat q, Vec3 v) {
  Vec3 u = {q.x, q.y, q.z};
  Vec3 t = {2.0f * (u.y * v.z - u.z * v.y), 2.0f * (u.z * v.x - u.x * v.z),
            2.0f * (u.x * v.y - u.y * v.x)};
  Vec3 c = {u.y * t.z - u.z * t.y, u.z * t.x - u.x * t.z,
            u.x * t.y - u.y * t.x};
  return {v.x + q.w * t.x + c.x, v.y + q.w * t.y + c.y, v.z + q.w * t.z + c.z};
}

static inline bool DirectVmdBuildCameraWorldPose(
    const DirectVmdCameraSamplePod &camera,
    const DirectVmdWorldPosePod &anchor, float cameraScale,
    const VmdVec3 &targetSideWorldOffset,
    DirectVmdWorldPosePod *worldPose) {
  if (!worldPose || !camera.valid || !DirectVmdFinite(cameraScale) ||
      cameraScale <= 0.0f ||
      !DirectVmdFinite(targetSideWorldOffset.x) ||
      !DirectVmdFinite(targetSideWorldOffset.y) ||
      !DirectVmdFinite(targetSideWorldOffset.z))
    return false;
  const Quat sourceCameraRotation = CamQuatFromEuler(
      camera.rotationEuler.x, camera.rotationEuler.y,
      camera.rotationEuler.z);
  const VmdQuaternion sourceRotation = DirectVmdNormalizeQuaternion(
      {sourceCameraRotation.x, sourceCameraRotation.y,
       sourceCameraRotation.z, sourceCameraRotation.w});
  const VmdVec3 sourceOffset = DirectVmdRotateVector(
      sourceRotation, {0.0f, 0.0f, camera.distance});
  const VmdVec3 sourceCameraPosition = DirectVmdAdd(
      camera.interest, sourceOffset);
  const DirectVmdLocalPosePod ownerPose = {
      DirectVmdScale(
          SourceToGameBasis::ConvertPosition(sourceCameraPosition),
          cameraScale),
      DirectVmdQuaternionMultiply(SourceToGameBasis::Rotation(),
                                  sourceRotation)};
  *worldPose = DirectVmdComposeWorldPose(anchor, ownerPose);
  worldPose->position = DirectVmdAdd(
      worldPose->position, targetSideWorldOffset);
  return true;
}

struct CameraPlayer {
  const VmdFile *vmd = nullptr;
  float scale = CAM_SCALE;

  bool HasData() const { return vmd && !vmd->cameraKeys.empty(); }

  void SetVmd(const VmdFile *v) { vmd = v; }

  Vec3 SampleInterest(float timeSec) const {
    Vec3 out = {0, 0, 0};
    if (!HasData()) return out;
    const auto &keys = vmd->cameraKeys;
    float frameF = timeSec * 30.0f;
    if (frameF <= keys.front().frame)
      return {keys.front().position[0], keys.front().position[1],
              keys.front().position[2]};
    if (frameF >= keys.back().frame)
      return {keys.back().position[0], keys.back().position[1],
              keys.back().position[2]};
    int lo = 0, hi = (int)keys.size() - 1;
    while (lo < hi - 1) {
      int mid = (lo + hi) / 2;
      if ((float)keys[mid].frame <= frameF) lo = mid;
      else hi = mid;
    }
    const VmdCameraKeyframe &k0 = keys[lo];
    const VmdCameraKeyframe &k1 = keys[hi];
    float range = (float)(k1.frame - k0.frame);
    float t = (range > 0.0f) ? (frameF - k0.frame) / range : 0.0f;
    const uint8_t *ip = k1.interp;
    float tx = CamBezierEval(ip[0], ip[2], ip[1], ip[3], t);
    float ty = CamBezierEval(ip[4], ip[6], ip[5], ip[7], t);
    float tz = CamBezierEval(ip[8], ip[10], ip[9], ip[11], t);
    return {k0.position[0] + (k1.position[0] - k0.position[0]) * tx,
            k0.position[1] + (k1.position[1] - k0.position[1]) * ty,
            k0.position[2] + (k1.position[2] - k0.position[2]) * tz};
  }

  CameraState Sample(float timeSec) const {
    CameraState out;
    out.valid = false;
    out.rotation = {0, 0, 0, 1};
    out.position = {0, 0, 0};
    out.fov = 45.0f;
    if (!HasData()) return out;

    const auto &keys = vmd->cameraKeys;
    float frameF = timeSec * 30.0f;

    if (frameF <= keys.front().frame) {
      BuildState(keys.front(), out);
      return out;
    }
    if (frameF >= keys.back().frame) {
      BuildState(keys.back(), out);
      return out;
    }

    int lo = 0, hi = (int)keys.size() - 1;
    while (lo < hi - 1) {
      int mid = (lo + hi) / 2;
      if ((float)keys[mid].frame <= frameF)
        lo = mid;
      else
        hi = mid;
    }
    const VmdCameraKeyframe &k0 = keys[lo];
    const VmdCameraKeyframe &k1 = keys[hi];

    float range = (float)(k1.frame - k0.frame);
    float t = (range > 0.0f) ? (frameF - k0.frame) / range : 0.0f;

    const uint8_t *ip = k1.interp;
    float tx = CamBezierEval(ip[0], ip[2], ip[1], ip[3], t);
    float ty = CamBezierEval(ip[4], ip[6], ip[5], ip[7], t);
    float tz = CamBezierEval(ip[8], ip[10], ip[9], ip[11], t);
    float tr = CamBezierEval(ip[12], ip[14], ip[13], ip[15], t);
    float td = CamBezierEval(ip[16], ip[18], ip[17], ip[19], t);
    float tf = CamBezierEval(ip[20], ip[22], ip[21], ip[23], t);

    Vec3 interest = {
        k0.position[0] + (k1.position[0] - k0.position[0]) * tx,
        k0.position[1] + (k1.position[1] - k0.position[1]) * ty,
        k0.position[2] + (k1.position[2] - k0.position[2]) * tz};
    Vec3 euler = {k0.rotation[0] + (k1.rotation[0] - k0.rotation[0]) * tr,
                  k0.rotation[1] + (k1.rotation[1] - k0.rotation[1]) * tr,
                  k0.rotation[2] + (k1.rotation[2] - k0.rotation[2]) * tr};
    float distance = k0.distance + (k1.distance - k0.distance) * td;
    float fov = (float)k0.fov + ((float)k1.fov - (float)k0.fov) * tf;

    BuildStateRaw(interest, euler, distance, fov, out);
    return out;
  }

 private:
  void BuildState(const VmdCameraKeyframe &k, CameraState &out) const {
    Vec3 interest = {k.position[0], k.position[1], k.position[2]};
    Vec3 euler = {k.rotation[0], k.rotation[1], k.rotation[2]};
    BuildStateRaw(interest, euler, k.distance, (float)k.fov, out);
  }

  void BuildStateRaw(Vec3 interest, Vec3 euler, float distance, float fov,
                     CameraState &out) const {
    out.euler = euler;
    Quat camRot = CamQuatFromEuler(euler.x, euler.y, euler.z);
    Vec3 offset = CamQuatRotate(camRot, {0.0f, 0.0f, distance});
    Vec3 camPos = {(interest.x + offset.x) * scale,
                   (interest.y + offset.y) * scale,
                   (interest.z + offset.z) * scale};
    out.position = camPos;
    out.rotation = camRot;
    out.interest = interest;
    out.fov = fov + CAM_FOV_BIAS;
    out.valid = true;
  }
};
