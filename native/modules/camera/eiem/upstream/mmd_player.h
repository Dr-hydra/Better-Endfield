#pragma once
#include <cmath>
#include <cstdint>

struct Vec3 { float x, y, z; };
struct Quat { float x, y, z, w; };

static Quat QuatMul(Quat a, Quat b) {
  return {
    a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
    a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
    a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
    a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z
  };
}

static Quat QuatInv(Quat q) {
  return { -q.x, -q.y, -q.z, q.w };
}

static Vec3 Vec3Lerp(Vec3 a, Vec3 b, float t) {
  return { a.x + (b.x - a.x) * t,
           a.y + (b.y - a.y) * t,
           a.z + (b.z - a.z) * t };
}

static float QuatDot(Quat a, Quat b) {
  return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
}

static Quat QuatSlerp(Quat a, Quat b, float t) {
  float dot = QuatDot(a, b);
  if (dot < 0.0f) {
    b.x = -b.x; b.y = -b.y; b.z = -b.z; b.w = -b.w;
    dot = -dot;
  }

  if (dot > 0.9995f) {
    Quat r = {
      a.x + (b.x - a.x) * t,
      a.y + (b.y - a.y) * t,
      a.z + (b.z - a.z) * t,
      a.w + (b.w - a.w) * t
    };
    float len = sqrtf(r.x*r.x + r.y*r.y + r.z*r.z + r.w*r.w);
    if (len > 0.0001f) { r.x /= len; r.y /= len; r.z /= len; r.w /= len; }
    return r;
  }

  float theta = acosf(dot);
  float sinTheta = sinf(theta);
  float wa = sinf((1.0f - t) * theta) / sinTheta;
  float wb = sinf(t * theta) / sinTheta;

  return {
    wa * a.x + wb * b.x,
    wa * a.y + wb * b.y,
    wa * a.z + wb * b.z,
    wa * a.w + wb * b.w
  };
}


static Quat RotationTo(Vec3 from, Vec3 to) {
  float fl = sqrtf(from.x*from.x + from.y*from.y + from.z*from.z);
  float tl = sqrtf(to.x*to.x + to.y*to.y + to.z*to.z);
  if (fl < 0.0001f || tl < 0.0001f) return {0,0,0,1};
  from.x/=fl; from.y/=fl; from.z/=fl;
  to.x/=tl; to.y/=tl; to.z/=tl;

  float dot = from.x*to.x + from.y*to.y + from.z*to.z;
  if (dot > 0.9999f) return {0,0,0,1};
  if (dot < -0.9999f) {
    Vec3 perp = {1,0,0};
    if (fabsf(from.x) > 0.9f) perp = {0,1,0};
    Vec3 axis = {
      from.y*perp.z - from.z*perp.y,
      from.z*perp.x - from.x*perp.z,
      from.x*perp.y - from.y*perp.x
    };
    float al = sqrtf(axis.x*axis.x + axis.y*axis.y + axis.z*axis.z);
    if (al > 0.0001f) { axis.x/=al; axis.y/=al; axis.z/=al; }
    return {axis.x, axis.y, axis.z, 0};
  }
  Vec3 c = {
    from.y*to.z - from.z*to.y,
    from.z*to.x - from.x*to.z,
    from.x*to.y - from.y*to.x
  };
  float w = 1.0f + dot;
  float len = sqrtf(c.x*c.x + c.y*c.y + c.z*c.z + w*w);
  return {c.x/len, c.y/len, c.z/len, w/len};
}

static Quat RetargetRotation(Quat vmd, Quat fromStance, Quat toStance) {
  return QuatMul(QuatMul(QuatInv(fromStance), vmd), toStance);
}

struct MmdStanceEntry {
  const char* mmdName;
  Vec3 defaultAxis;
  Vec3 boneDir;
};

static const MmdStanceEntry g_mmdStanceTable[] = {
  {"\xe5\xb7\xa6\xe8\x82\xa9",   {1,0,0}, { 0.9687f,-0.2480f, 0.0138f}},
  {"\xe5\xb7\xa6\xe8\x85\x95",   {1,0,0}, { 0.7941f,-0.6076f, 0.0120f}},
  {"\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x98",{1,0,0}, { 0.7962f,-0.6047f,-0.0182f}},
  {"\xe5\xb7\xa6\xe6\x89\x8b\xe9\xa6\x96",{1,0,0}, { 0.7999f,-0.5972f,-0.0596f}},
  {"\xe5\x8f\xb3\xe8\x82\xa9",   {-1,0,0},{-0.9687f,-0.2480f, 0.0138f}},
  {"\xe5\x8f\xb3\xe8\x85\x95",   {-1,0,0},{-0.7941f,-0.6076f, 0.0120f}},
  {"\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x98",{-1,0,0},{-0.7962f,-0.6047f,-0.0182f}},
  {"\xe5\x8f\xb3\xe6\x89\x8b\xe9\xa6\x96",{-1,0,0},{-0.7999f,-0.5972f,-0.0596f}},
  {"\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab",      {0,1,0}, { 0.0000f, 0.9990f, 0.0440f}},
  {"\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab\x32",   {0,1,0}, { 0.0000f, 0.9990f, 0.0440f}},
  {"\xe9\xa6\x96",               {0,1,0}, { 0.0000f, 1.0000f,-0.0099f}},
  {"\xe9\xa0\xad",               {0,1,0}, { 0.0000f, 1.0000f, 0.0000f}},
  {"\xe5\xb7\xa6\xe8\xb6\xb3",   {0,-1,0},{ 0.0151f,-0.9979f, 0.0637f}},
  {"\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x96",{0,-1,0},{ 0.0073f,-0.9922f, 0.1247f}},
  {"\xe5\xb7\xa6\xe8\xb6\xb3\xe9\xa6\x96",{0,-1,0},{ 0.0000f,-1.0000f, 0.0000f}},
  {"\xe5\x8f\xb3\xe8\xb6\xb3",   {0,-1,0},{-0.0151f,-0.9979f, 0.0637f}},
  {"\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x96",{0,-1,0},{-0.0073f,-0.9922f, 0.1247f}},
  {"\xe5\x8f\xb3\xe8\xb6\xb3\xe9\xa6\x96",{0,-1,0},{ 0.0000f,-1.0000f, 0.0000f}},
};
static const int g_mmdStanceCount = sizeof(g_mmdStanceTable) / sizeof(g_mmdStanceTable[0]);

static Quat LookupMmdStance(const std::string &mmdName) {
  for (int i = 0; i < g_mmdStanceCount; i++) {
    if (mmdName == g_mmdStanceTable[i].mmdName) {
      return RotationTo(g_mmdStanceTable[i].defaultAxis, g_mmdStanceTable[i].boneDir);
    }
  }
  return {0,0,0,1};
}

static Vec3 MmdPosToUnity(Vec3 p) {
  float scale = 0.08f;
  return { p.x * scale, p.y * scale, p.z * scale };
}

struct InterpResult {
  Vec3 position;
  Quat rotation;
  bool hasPosition;
};

static InterpResult InterpolateBone(
    const std::vector<VmdBoneKeyframe> &keys,
    float frame,
    bool isPositionBone)
{
  InterpResult result;
  result.hasPosition = isPositionBone;
  result.rotation = { 0, 0, 0, 1 };
  result.position = { 0, 0, 0 };

  if (keys.empty()) return result;

  if (frame <= keys.front().frame) {
    const auto &k = keys.front();
    result.rotation = { k.rot[0], k.rot[1], k.rot[2], k.rot[3] };
    if (isPositionBone) result.position = { k.pos[0], k.pos[1], k.pos[2] };
    return result;
  }

  if (frame >= keys.back().frame) {
    const auto &k = keys.back();
    result.rotation = { k.rot[0], k.rot[1], k.rot[2], k.rot[3] };
    if (isPositionBone) result.position = { k.pos[0], k.pos[1], k.pos[2] };
    return result;
  }

  int lo = 0, hi = (int)keys.size() - 1;
  while (lo < hi - 1) {
    int mid = (lo + hi) / 2;
    if (keys[mid].frame <= frame)
      lo = mid;
    else
      hi = mid;
  }

  const auto &prev = keys[lo];
  const auto &next = keys[hi];

  float range = (float)(next.frame - prev.frame);
  float t = (range > 0.0f) ? (frame - prev.frame) / range : 0.0f;
  t = (t < 0.0f) ? 0.0f : (t > 1.0f) ? 1.0f : t;

  Quat qa = { prev.rot[0], prev.rot[1], prev.rot[2], prev.rot[3] };
  Quat qb = { next.rot[0], next.rot[1], next.rot[2], next.rot[3] };
  result.rotation = QuatSlerp(qa, qb, t);

  if (isPositionBone) {
    Vec3 pa = { prev.pos[0], prev.pos[1], prev.pos[2] };
    Vec3 pb = { next.pos[0], next.pos[1], next.pos[2] };
    result.position = Vec3Lerp(pa, pb, t);
  }

  return result;
}

struct MmdPlayer {
  bool playing;
  bool loop;
  bool ended;
  float currentTime;
  float speed;
  float totalDuration;

  LARGE_INTEGER lastTick;
  LARGE_INTEGER freq;

  MmdPlayer() : playing(false), loop(false), ended(false), currentTime(0),
                speed(1.0f), totalDuration(0) {
    QueryPerformanceFrequency(&freq);
    QueryPerformanceCounter(&lastTick);
  }

  void Start(float duration) {
    totalDuration = duration;
    currentTime = 0;
    playing = true;
    ended = false;
    QueryPerformanceCounter(&lastTick);
  }

  void Stop() {
    playing = false;
    ended = false;
    currentTime = 0;
  }

  void TogglePause() {
    if (playing) {
      playing = false;
      ended = false;
    } else {
      playing = true;
      ended = false;
      QueryPerformanceCounter(&lastTick);
    }
  }

  float Tick() {
    if (!playing) return currentTime * 30.0f;

    LARGE_INTEGER now;
    QueryPerformanceCounter(&now);
    double elapsed = (double)(now.QuadPart - lastTick.QuadPart) / (double)freq.QuadPart;
    lastTick = now;

    currentTime += (float)(elapsed * speed);

    if (currentTime >= totalDuration) {
      if (loop) {
        currentTime = fmodf(currentTime, totalDuration);
        ended = false;
      } else {
        currentTime = totalDuration;
        playing = false;
        ended = true;
      }
    }

    return currentTime * 30.0f;
  }
};
