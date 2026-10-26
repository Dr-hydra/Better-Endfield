#pragma once
#include "pose_policy.h"
#include <cstdint>
#include <iterator>
#include <string>

// Compiled-in loop quality data: a per-bone seam split and a phase-locked
// accessory lap. No file, no Unity dependency; jsspsi_quality_data.h holds the
// generated arrays.
namespace BetterEndfieldNext::Actions {
// A Transform below an Animator root. Paths for Transform.Find are rebuilt from
// parent links; the root itself is never a node.
struct QualityNode { int16_t parent; const char* name; };
struct QualityTree { const QualityNode* nodes; uint16_t count; };
// Seam difference D = first * end^-1 (stored xyz, w >= 0) and dp = first - end,
// measured between the loop target and the loop begin.
struct QualitySeamBone { uint16_t node; int16_t rotation[3]; int16_t position[3]; };
struct QualitySeamEntry { int32_t state_hash; const QualitySeamBone* bones; uint16_t count; };
// rotations[sample * bones + bone]; sample j is phase j / samples.
struct QualityAccessorySide { int32_t state_hash; const float (*positions)[3]; const int16_t (*rotations)[4]; };
struct LoopQualityData {
    float begin, target;
    QualityTree rider, mount;
    QualitySeamEntry rider_seam[2], mount_seam;
    const uint16_t* accessory_nodes;
    uint16_t accessory_count, accessory_samples;
    QualityAccessorySide accessory[2];
};
inline constexpr float kQualityRotationUnit = 1.f / 32767;
inline constexpr float kQualityPositionUnit = 1e-5f; // metres
// Each endpoint takes half the difference over this share of the lap.
inline constexpr float kQualitySeamWindow = .5f;
// Accessory bones ease back to the native pose after the dash ends.
inline constexpr float kQualityFadeSeconds = .4f;
// A drop larger than this between frames is a rewind, not progress jitter.
inline constexpr float kQualityRewindThreshold = .02f;
// Rider and mount lap progress may differ this much when the seam arms (about
// three frames); a larger lag would leave part of the mount's seam at the cut.
inline constexpr float kQualityArmTolerance = .05f;
inline bool QualityPath(const QualityTree& tree, uint16_t index, std::string& path) {
    path.clear();
    for (int guard = 0, node = index; node >= 0; node = tree.nodes[node].parent) {
        if (node >= tree.count || ++guard > tree.count) return false;
        path.insert(0, tree.nodes[node].name);
        if (tree.nodes[node].parent >= 0) path.insert(path.begin(), '/');
    }
    return !path.empty();
}
inline PoseQuaternion QualityMul(PoseQuaternion a, PoseQuaternion b) {
    return {a.w*b.x+a.x*b.w+a.y*b.z-a.z*b.y, a.w*b.y-a.x*b.z+a.y*b.w+a.z*b.x,
            a.w*b.z+a.x*b.y-a.y*b.x+a.z*b.w, a.w*b.w-a.x*b.x-a.y*b.y-a.z*b.z};
}
inline PoseQuaternion QualityConj(PoseQuaternion q) { return {-q.x, -q.y, -q.z, q.w}; }
inline PoseQuaternion QualitySeamRotation(const QualitySeamBone& bone) {
    const float x = bone.rotation[0] * kQualityRotationUnit, y = bone.rotation[1] * kQualityRotationUnit,
        z = bone.rotation[2] * kQualityRotationUnit;
    return PoseNormalize({x, y, z, std::sqrt(std::max(0.f, 1 - x*x - y*y - z*z))});
}
inline PoseVector QualitySeamPosition(const QualitySeamBone& bone) {
    return {bone.position[0] * kQualityPositionUnit, bone.position[1] * kQualityPositionUnit, bone.position[2] * kQualityPositionUnit};
}
// +1/2 over the end of the lap and -1/2 over the start of a rewound lap: both
// sides of the seam meet at the same pose, so the rewind can be a hard cut.
// The first natural lap keeps its native start.
inline float QualitySeamShare(float phase, bool from_rewind) {
    auto smooth = [](float x) { return x * x * (3 - 2 * x); };
    const float w = kQualitySeamWindow;
    if (phase > 1 - w) return .5f * smooth((phase - (1 - w)) / w);
    if (phase < w && from_rewind) return -.5f * (1 - smooth(phase / w));
    return 0;
}
inline BonePose QualitySeamApply(const BonePose& native, const QualitySeamBone& bone, float share) {
    if (share == 0) return native;
    const auto turn = PoseSlerp(PoseQuaternion{}, QualitySeamRotation(bone), std::fabs(share));
    const auto dp = QualitySeamPosition(bone);
    return {PoseNormalize(QualityMul(share > 0 ? turn : QualityConj(turn), native.rotation)),
            {native.position.x + dp.x * share, native.position.y + dp.y * share, native.position.z + dp.z * share}};
}
inline bool QualityPhase(float u, float begin, float target, double& phase) {
    if (!std::isfinite(u) || !(begin > target)) return false;
    const double turns = (double(u) - target) / (double(begin) - target);
    phase = turns - std::floor(turns);
    if (phase >= 1) phase = 0;
    return std::isfinite(phase);
}
inline BonePose QualityAccessorySample(const LoopQualityData& data, int side, uint16_t bone, double phase) {
    const auto& table = data.accessory[side];
    const uint32_t samples = data.accessory_samples, bones = data.accessory_count;
    const double frame = phase * samples;
    const auto k = std::min(static_cast<uint32_t>(frame), samples - 1);
    auto rotation = [&](uint32_t sample) {
        const auto* q = table.rotations[size_t(sample) * bones + bone];
        return PoseNormalize({q[0] * kQualityRotationUnit, q[1] * kQualityRotationUnit, q[2] * kQualityRotationUnit, q[3] * kQualityRotationUnit});
    };
    const auto* p = table.positions[bone];
    return {PoseSlerp(rotation(k), rotation((k + 1) % samples), static_cast<float>(frame - k)), {p[0], p[1], p[2]}};
}
}
