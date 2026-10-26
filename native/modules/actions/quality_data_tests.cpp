// Check the compiled-in Jsspsi quality data and the pure seam/accessory math.
// No Unity or module state: the production integration is loop_quality_tests.cpp.
#include "dash_policy.h"
#include "jsspsi_quality_data.h"
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <set>
#include <utility>
using namespace BetterEndfieldNext::Actions;
#define CHECK(x) do { if (!(x)) { std::fprintf(stderr, "line %d: %s\n", __LINE__, #x); std::exit(1); } } while (false)
namespace {
const LoopQualityData& data = JsspsiQuality::kData;
// The generator reports the 64-bit target's padded arrays and descriptor.
static_assert(sizeof(void*) != 8 || (sizeof(QualityNode) == 16 && alignof(QualityNode) == 8));
static_assert(sizeof(void*) != 8 || sizeof(LoopQualityData) == 176);
// Angle of a * b^-1 from its vector part; acos of a float dot is too coarse here.
float Degrees(PoseQuaternion a, PoseQuaternion b) {
    const auto d = QualityMul(a, QualityConj(b));
    const double v = std::sqrt(double(d.x)*d.x + double(d.y)*d.y + double(d.z)*d.z);
    return float(2 * std::atan2(v, std::fabs(double(d.w))) * 57.29577951308232);
}
float Distance(PoseVector a, PoseVector b) {
    return std::sqrt((a.x-b.x)*(a.x-b.x) + (a.y-b.y)*(a.y-b.y) + (a.z-b.z)*(a.z-b.z));
}
void CheckTree(const QualityTree& tree) {
    CHECK(tree.count > 0 && tree.count < 4096);
    std::set<std::pair<int, std::string>> seen;
    for (uint16_t i = 0; i < tree.count; ++i) {
        const auto& node = tree.nodes[i];
        CHECK(node.parent >= -1 && node.parent < static_cast<int>(i));
        CHECK(node.name && *node.name && std::strlen(node.name) < 128);
        for (const char* c = node.name; *c; ++c) CHECK(*c > 0x20 && *c < 0x7f && *c != '/' && *c != '\\');
        CHECK(seen.emplace(node.parent, node.name).second);
        std::string path;
        CHECK(QualityPath(tree, i, path) && path.size() < 512 && path.front() != '/' && path.back() != '/');
    }
    CHECK(std::strcmp(tree.nodes[0].name, "Root") == 0 && tree.nodes[0].parent == -1);
}
void CheckSeam(const QualitySeamEntry& entry, const QualityTree& tree, size_t minimum) {
    CHECK(entry.bones && entry.count >= minimum);
    uint32_t seed = 12345;
    auto random = [&] { seed = seed * 1664525u + 1013904223u; return (seed >> 8) / float(1u << 24) * 2 - 1; };
    for (uint16_t i = 0; i < entry.count; ++i) {
        const auto& bone = entry.bones[i];
        CHECK(bone.node < tree.count);
        if (i) CHECK(entry.bones[i - 1].node < bone.node);
        const auto d = QualitySeamRotation(bone);
        CHECK(std::fabs(PoseDot(d, d) - 1) < 1e-5f && d.w >= 0);
        const auto dp = QualitySeamPosition(bone);
        // Rows below 0.1 degree and 0.1 mm were dropped by the generator.
        CHECK(Degrees(d, PoseQuaternion{}) > .1f || Distance(dp, {}) > 1e-4f);
        // Seam closure: the end pose with +1/2 equals the start pose with -1/2.
        BonePose end{PoseNormalize({random(), random(), random(), random() + 2}), {random(), random(), random()}};
        BonePose start{PoseNormalize(QualityMul(d, end.rotation)), {end.position.x + dp.x, end.position.y + dp.y, end.position.z + dp.z}};
        const auto a = QualitySeamApply(end, bone, .5f), b = QualitySeamApply(start, bone, -.5f);
        CHECK(Degrees(a.rotation, b.rotation) < .01f && Distance(a.position, b.position) < 1e-6f);
        const auto none = QualitySeamApply(end, bone, 0);
        CHECK(PoseSame(none.rotation, end.rotation) && PoseSame(none.position, end.position));
    }
}
}
int main() {
    // The data was cut for the profile's rewind window.
    CHECK(data.begin == kJsspsiLoop.begin && data.target == kJsspsiLoop.target);
    CheckTree(data.rider);
    CheckTree(data.mount);
    CHECK(data.rider_seam[0].state_hash == -445642423 && data.rider_seam[1].state_hash == 528430122);
    CHECK(data.accessory[0].state_hash == data.rider_seam[0].state_hash && data.accessory[1].state_hash == data.rider_seam[1].state_hash);
    CHECK(data.mount_seam.state_hash == 340743731);
    CheckSeam(data.rider_seam[0], data.rider, 300);
    CheckSeam(data.rider_seam[1], data.rider, 300);
    CheckSeam(data.mount_seam, data.mount, 20);
    std::string path;
    CHECK(QualityPath(data.rider, data.rider_seam[0].bones[0].node, path));

    // Share: half the difference on each side of the seam, zero slope at both ends.
    CHECK(QualitySeamShare(1, false) == .5f && QualitySeamShare(1, true) == .5f);
    CHECK(QualitySeamShare(0, true) == -.5f && QualitySeamShare(0, false) == 0);
    CHECK(QualitySeamShare(.5f, true) == 0 && QualitySeamShare(.25f, false) == 0);
    CHECK(std::fabs(QualitySeamShare(1, true) - QualitySeamShare(.999f, true)) < 1e-4f);
    CHECK(std::fabs(QualitySeamShare(.001f, true) - QualitySeamShare(0, true)) < 1e-4f);
    for (float p = 0; p < 1; p += .01f) CHECK(std::fabs(QualitySeamShare(p, true)) <= .5f);

    double phase = -1;
    CHECK(QualityPhase(data.target, data.begin, data.target, phase) && phase == 0);
    CHECK(QualityPhase(data.begin, data.begin, data.target, phase) && phase < 1e-6);
    CHECK(QualityPhase((data.begin + data.target) / 2, data.begin, data.target, phase) && std::fabs(phase - .5) < 1e-6);
    CHECK(QualityPhase(.316f, data.begin, data.target, phase) && phase > 0 && phase < .01);
    CHECK(!QualityPhase(NAN, data.begin, data.target, phase) && !QualityPhase(.2f, data.target, data.begin, phase));

    // Accessory lap: tail and back hair, both sides, unit rotations, closed lap.
    CHECK(data.accessory_count == 30 && data.accessory_samples == 128);
    std::set<uint16_t> nodes;
    for (uint16_t i = 0; i < data.accessory_count; ++i) {
        CHECK(data.accessory_nodes[i] < data.rider.count && nodes.insert(data.accessory_nodes[i]).second);
        const char* name = data.rider.nodes[data.accessory_nodes[i]].name;
        CHECK(std::strncmp(name, "tail_", 5) == 0 || std::strncmp(name, "L_hairE_", 8) == 0 || std::strncmp(name, "R_hairE_", 8) == 0);
    }
    CHECK(QualityPath(data.rider, data.accessory_nodes[9], path));
    CHECK(path == "Root/Bip001/Bip001_Pelvis/Bip001_Spine/tail_01_jnt/tail_02_jnt/tail_03_jnt/tail_04_jnt/tail_05_jnt/tail_06_jnt/tail_07_jnt/tail_08_jnt/tail_09_jnt/tail_10_jnt");
    for (int side = 0; side < 2; ++side) {
        const auto& table = data.accessory[side];
        for (uint32_t i = 0; i < uint32_t(data.accessory_samples) * data.accessory_count; ++i) {
            const auto* q = table.rotations[i];
            const float n = (float(q[0])*q[0] + float(q[1])*q[1] + float(q[2])*q[2] + float(q[3])*q[3]) * kQualityRotationUnit * kQualityRotationUnit;
            CHECK(std::fabs(n - 1) < 1e-3f);
        }
        for (uint16_t bone = 0; bone < data.accessory_count; ++bone) {
            for (float v : table.positions[bone]) CHECK(std::isfinite(v) && std::fabs(v) < 1);
            for (uint32_t j = 0; j < data.accessory_samples; ++j) {
                const auto* q = table.rotations[j * data.accessory_count + bone];
                const auto exact = PoseNormalize({q[0] * kQualityRotationUnit, q[1] * kQualityRotationUnit, q[2] * kQualityRotationUnit, q[3] * kQualityRotationUnit});
                const auto sampled = QualityAccessorySample(data, side, bone, double(j) / data.accessory_samples);
                CHECK(Degrees(sampled.rotation, exact) < 1e-3f);
                const auto next = QualityAccessorySample(data, side, bone, double((j + 1) % data.accessory_samples) / data.accessory_samples);
                CHECK(Degrees(sampled.rotation, next.rotation) < 20); // No corrupted sample.
            }
            // The last interval interpolates back to sample 0.
            const auto before = QualityAccessorySample(data, side, bone, 1 - 1e-9);
            const auto first = QualityAccessorySample(data, side, bone, 0);
            CHECK(Degrees(before.rotation, first.rotation) < .01f);
            const auto mid = QualityAccessorySample(data, side, bone, .5 / data.accessory_samples);
            const auto a = QualityAccessorySample(data, side, bone, 0), b = QualityAccessorySample(data, side, bone, 1.0 / data.accessory_samples);
            CHECK(std::fabs(Degrees(a.rotation, mid.rotation) - Degrees(mid.rotation, b.rotation)) < .05f);
        }
    }
    std::puts("Jsspsi quality data: window, trees, seam rows and closure, share, phase and accessory lap passed.");
}
