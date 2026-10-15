#include "../modules/custom_model/aurora_legacy_compat.h"
#include "../modules/custom_model/generic_model_matcher.h"
#include <cstdlib>
#include <iostream>
#include <limits>
#include <vector>

namespace A = BetterEndfieldNext::CustomModel::AuroraLegacy;
namespace G = BetterEndfieldNext::CustomModel::GenericMatching;
namespace {
unsigned checks = 0;
void Check(bool value, const char* message) {
    ++checks;
    if (!value) { std::cerr << "FAIL " << message << '\n'; std::exit(1); }
}
constexpr std::string_view mesh = "S_actor_aurora_cloth_01_lod0";
constexpr std::string_view world = "chr_0014_aurora_postmodel";
constexpr std::string_view ui = "chr_0014_aurora_uimodel";
std::vector<A::BoneObservation> CurrentBones() {
    std::vector<A::BoneObservation> result;
    for (const auto& bone : A::kCurrentBones) result.push_back({bone.name, bone.path, bone.bindpose});
    return result;
}
bool Valid(const std::vector<A::BoneObservation>& bones, uint64_t indices = 87288,
           uint32_t vertices = 23141, std::string_view name = mesh,
           std::string_view path = "Mesh_all/lod0/S_actor_aurora_cloth_01_lod0") {
    return A::ValidateDonor(name, path, indices, vertices, bones);
}
}
int main() {
    Check(A::kLegacyIndices == 87120 && A::kCurrentIndices == 87288 && A::kCurrentVertices == 23141 &&
          A::kBoneCount == 87, "verified C1 contract changed unexpectedly");
    for (auto resource : {world, ui}) {
        Check(A::IsLegacyTarget("chr_0014_aurora", resource, 1, mesh, 87120, false), "legacy C1 target not recognized");
        Check(!A::IsLegacyTarget("chr_0014_aurora", resource, 1, mesh, 87288, false), "new package was treated as legacy");
        Check(!A::IsLegacyTarget("chr_0014_aurora", resource, 1, mesh, 87120, true), "explicit target accepted legacy exception");
        for (uint32_t component : {0u, 2u, 11u})
            Check(!A::IsLegacyTarget("chr_0014_aurora", resource, component, mesh, 87120, false), "exception escaped C1");
    }
    Check(!A::IsLegacyTarget("chr_0013_aglina", world, 1, mesh, 87120, false), "exception escaped character");
    Check(!A::IsLegacyTarget("chr_0014_aurora", "chr_0014_aurora_othermodel", 1, mesh, 87120, false), "unknown resource accepted");
    Check(!A::IsLegacyTarget("chr_0014_aurora", world, 1, "S_actor_aurora_cloth_02_lod0", 87120, false), "exception escaped mesh");
    Check(!A::IsLegacyTarget("chr_0014_aurora", world, 1, mesh, 87123, false), "unknown old count accepted");

    const auto reference = CurrentBones();
    Check(reference.size() == 87 && Valid(reference), "verified current donor rejected");
    for (uint64_t count : {0ull, 87120ull, 87285ull, 87291ull}) Check(!Valid(reference, count), "unknown donor count accepted");
    for (uint32_t count : {0u, 23073u, 23140u, 23142u}) Check(!Valid(reference, 87288, count), "unknown donor vertex count accepted");
    Check(!Valid(reference, 87288, 23141, "S_actor_aurora_cloth_02_lod0"), "wrong donor mesh accepted");
    Check(!Valid(reference, 87288, 23141, mesh, "Mesh_all/lod1/S_actor_aurora_cloth_01_lod1"), "wrong donor LOD accepted");
    Check(!Valid(reference, 87288, 23141, mesh, "Other/lod0/S_actor_aurora_cloth_01_lod0"), "same-name foreign renderer accepted");
    auto changed = reference;
    changed.pop_back(); Check(!Valid(changed), "missing bone accepted");
    changed = reference; changed.push_back(reference.back()); Check(!Valid(changed), "extra bone accepted");
    changed = reference; std::swap(changed[37], changed[38]); Check(!Valid(changed), "wrong current bone order accepted");
    changed = reference; changed[38].name = changed[37].name; Check(!Valid(changed), "duplicate bone name accepted");
    changed = reference; changed[38].path = changed[37].path; Check(!Valid(changed), "duplicate bone path accepted");
    changed = reference; changed[37].bindpose[12] += 0.001f; Check(!Valid(changed), "material bindpose change accepted");
    changed = reference; changed[37].bindpose[12] += 0.0000001f; Check(Valid(changed), "float roundoff within tolerance rejected");
    changed = reference; changed[37].bindpose[0] = std::numeric_limits<float>::quiet_NaN(); Check(!Valid(changed), "NaN bindpose accepted");
    changed = reference; changed[37].bindpose[0] = std::numeric_limits<float>::infinity(); Check(!Valid(changed), "infinite bindpose accepted");

    // Independently recorded old-to-new slot changes from the old/current graph.
    constexpr std::array<std::pair<uint32_t, uint32_t>, 10> moved{{
        {37,38},{38,39},{39,40},{40,41},{41,42},{42,43},{43,44},{44,37},{64,65},{65,64}}};
    for (uint32_t old = 0; old < 87; ++old) {
        uint32_t expected = old;
        for (auto [from, to] : moved) if (old == from) expected = to;
        const auto actual = A::RemapBone(old, reference[expected].name);
        Check(actual && *actual == expected, "verified bone slot remapped incorrectly");
        Check(!A::RemapBone(old, "unknown_bone"), "unknown legacy bone name accepted");
    }
    for (auto [old, current] : moved)
        Check(!A::RemapBone(old, reference[old].name), "moved slot accepted the new occupant's name as legacy");
    Check(!A::RemapBone(87, reference[0].name), "out-of-bounds legacy slot accepted");
    Check(!A::RemapBone(UINT32_MAX, reference[0].name), "overflow legacy slot accepted");

    // The shared selector retains exact counts; compatibility cannot relax it globally.
    G::Candidate donor;
    donor.key = {"chr_0014_aurora", std::string(world), std::string(A::kReceiverPath), G::Region::Lod0};
    donor.renderer = &donor;
    donor.pristine = {G::DonorOrigin::Pristine, &donor, std::string(mesh), 87120, true, {}};
    std::vector<G::Candidate> candidates{donor};
    G::Request request{"chr_0014_aurora", world, mesh, G::Region::Lod0, 87120, true, {}};
    auto accept = [](const G::Candidate&) { return true; };
    Check(G::SelectUnique(candidates, request, accept).status == G::MatchStatus::Matched, "unchanged legacy donor rejected");
    candidates[0].pristine.indices = 87288;
    Check(G::SelectUnique(candidates, request, accept).status == G::MatchStatus::Missing, "shared matcher weakened old count gate");
    request.indices = 87288; request.verified_receiver_path = A::kReceiverPath;
    Check(G::SelectUnique(candidates, request, accept).status == G::MatchStatus::Matched, "exact current fallback candidate rejected");
    candidates.push_back(candidates[0]);
    Check(G::SelectUnique(candidates, request, accept).status != G::MatchStatus::Matched, "duplicate fallback receiver accepted");
    std::cout << "PASS aurora compatibility policy: " << checks << " checks\n";
}
