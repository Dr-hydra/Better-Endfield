#include "shmem.h"

#include <windows.h>
#include <cmath>
#include <cstring>

namespace ecl {

namespace {

constexpr char kMapName[] = "EndfieldCameraBridgeV1";
constexpr int kPacketSize = 128;

HANDLE g_map = nullptr;
const uint8_t* g_view = nullptr;
uint64_t g_lastSequence = 0;

float* F(uint8_t* base, int offset) { return reinterpret_cast<float*>(base + offset); }

// 由 forward/up 构造 Unity 四元数 (等价 Quaternion.LookRotation)
void LookRotation(const float f[3], const float u[3], float q[4]) {
    auto norm = [](const float* v, float* o) {
        const float len = std::sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
        if (len < 1e-6f) { o[0] = o[1] = o[2] = 0.f; return; }
        o[0] = v[0] / len; o[1] = v[1] / len; o[2] = v[2] / len;
    };
    float fw[3], up[3], rt[3], upN[3];
    norm(f, fw);
    norm(u, up);
    // right = normalize(cross(up, forward))
    rt[0] = up[1] * fw[2] - up[2] * fw[1];
    rt[1] = up[2] * fw[0] - up[0] * fw[2];
    rt[2] = up[0] * fw[1] - up[1] * fw[0];
    norm(rt, rt);
    // upN = cross(forward, right)
    upN[0] = fw[1] * rt[2] - fw[2] * rt[1];
    upN[1] = fw[2] * rt[0] - fw[0] * rt[2];
    upN[2] = fw[0] * rt[1] - fw[1] * rt[0];
    // 旋转矩阵列 = (right, upN, forward) -> 四元数
    const float m00 = rt[0], m01 = upN[0], m02 = fw[0];
    const float m10 = rt[1], m11 = upN[1], m12 = fw[1];
    const float m20 = rt[2], m21 = upN[2], m22 = fw[2];
    const float tr = m00 + m11 + m22;
    if (tr > 0.f) {
        const float s = std::sqrt(tr + 1.f) * 2.f;
        q[3] = 0.25f * s;
        q[0] = (m21 - m12) / s;
        q[1] = (m02 - m20) / s;
        q[2] = (m10 - m01) / s;
    } else if (m00 > m11 && m00 > m22) {
        const float s = std::sqrt(1.f + m00 - m11 - m22) * 2.f;
        q[3] = (m21 - m12) / s;
        q[0] = 0.25f * s;
        q[1] = (m01 + m10) / s;
        q[2] = (m02 + m20) / s;
    } else if (m11 > m22) {
        const float s = std::sqrt(1.f + m11 - m00 - m22) * 2.f;
        q[3] = (m02 - m20) / s;
        q[0] = (m01 + m10) / s;
        q[1] = 0.25f * s;
        q[2] = (m12 + m21) / s;
    } else {
        const float s = std::sqrt(1.f + m22 - m00 - m11) * 2.f;
        q[3] = (m10 - m01) / s;
        q[0] = (m02 + m20) / s;
        q[1] = (m12 + m21) / s;
        q[2] = 0.25f * s;
    }
}

}  // namespace

bool ShmemStart() {
    if (g_view) return true;
    g_map = OpenFileMappingA(FILE_MAP_READ, FALSE, kMapName);
    if (!g_map) return false;
    g_view = reinterpret_cast<const uint8_t*>(
        MapViewOfFile(g_map, FILE_MAP_READ, 0, 0, kPacketSize));
    if (!g_view) {
        CloseHandle(g_map);
        g_map = nullptr;
        return false;
    }
    return true;
}

void ShmemStop() {
    if (g_view) { UnmapViewOfFile(g_view); g_view = nullptr; }
    if (g_map) { CloseHandle(g_map); g_map = nullptr; }
}

bool ShmemLatest(BridgePose& out) {
    if (!ShmemStart()) return false;
    uint8_t pkt[kPacketSize];
    memcpy(pkt, g_view, kPacketSize);

    if (memcmp(pkt, "ECB1", 4) != 0) return false;
    const uint32_t version = *reinterpret_cast<uint32_t*>(pkt + 4);
    if (version != 1) return false;
    const uint64_t seq = *reinterpret_cast<uint64_t*>(pkt + 8);
    if ((seq & 1ULL) != 0ULL || seq == g_lastSequence) return false;  // 奇数=写入中
    const uint64_t seqEnd = *reinterpret_cast<uint64_t*>(pkt + 120);
    if (seq != seqEnd) return false;  // 撕裂检测

    out.sequence = seq;
    out.flags = *reinterpret_cast<uint32_t*>(pkt + 24);
    out.frame = *reinterpret_cast<int32_t*>(pkt + 28);
    memcpy(out.pos, pkt + 32, 12);
    const float* fwd = reinterpret_cast<const float*>(pkt + 44);
    const float* up = reinterpret_cast<const float*>(pkt + 56);
    out.focalLength = *F(pkt, 68);
    out.sensorW = *F(pkt, 72);
    out.sensorH = *F(pkt, 76);
    out.clipStart = *F(pkt, 80);
    out.clipEnd = *F(pkt, 84);
    out.focusDistance = *F(pkt, 88);
    out.fStop = *F(pkt, 92);
    out.fps = *F(pkt, 96);
    // v1.1: 参考物件位置(基线原点)放在 5 个备用浮点的前 3 个
    out.hasRef = (out.flags & kFlagReference) != 0;
    out.refPos[0] = *F(pkt, 100);
    out.refPos[1] = *F(pkt, 104);
    out.refPos[2] = *F(pkt, 108);
    // 垂直 FOV = 2*atan(sensorH / (2*focal))
    out.fovDeg = 60.f;
    if (out.focalLength > 0.01f && out.sensorH > 0.01f) {
        out.fovDeg = static_cast<float>(
            2.0 * std::atan(out.sensorH / (2.0 * out.focalLength)) * 180.0 / 3.14159265358979);
    }
    LookRotation(fwd, up, out.quat);
    g_lastSequence = seq;
    return true;
}

}  // namespace ecl
