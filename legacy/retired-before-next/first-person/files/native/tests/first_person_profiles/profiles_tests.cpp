#include "../../modules/camera/first_person_profiles.h"
#include <cstdlib>
#include <iomanip>
#include <iostream>
#include <limits>
#include <string>

namespace Fp = BetterEndfield::FirstPersonProfiles;
namespace {
int checks = 0;
void Check(bool value, const char* message) {
    ++checks;
    if (!value) { std::cerr << message << '\n'; std::exit(1); }
}
const std::string spine = "Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2";
const std::string neck = spine + "/Bip001_Neck";
const std::string head = neck + "/Bip001_Head";

void CoverageAndPaths() {
    Check(std::size(Fp::Generated::kProfiles) == 33, "32 native graphs + 1 supplemental profile");
    Check(!Fp::LookupProfile("chr_0999_future"), "unknown model must use generic fallback");
    Check(!Fp::LookupProfile("CHR_0034_TYPHOEA"), "no fuzzy model identity");
    Check(!Fp::LookupProfile("chr_0034_typhoea_postmodel(Clone)"), "unobserved aliases not invented");
    size_t graph_sources = 0;
    for (const auto& profile : Fp::Generated::kProfiles) {
        Check(Fp::LookupProfile(profile.model_id) == &profile, "base model lookup");
        for (size_t i = 0; i < profile.source_count; ++i) {
            const auto& source = profile.sources[i];
            Check(Fp::LookupProfile(source.model_root) == &profile, "observed root lookup");
            Check(source.platform == "windows-x64", "source platform must remain Windows");
            Check(source.lod_mask != 0, "actual LOD coverage required");
            if (!source.graph_verified) continue;
            ++graph_sources;
            auto full = std::string(source.model_root) + "/";
            auto h = Fp::MatchBonePath(&profile, full + head);
            Check(h.semantic == Fp::BoneSemantic::Hide && h.rule->region == Fp::Region::Head, "primary head");
            Check(Fp::MatchBonePath(&profile, head + "/faceJoint").semantic == Fp::BoneSemantic::Hide,
                  "Head descendants remain hidden");
            Check(Fp::MatchBonePath(&profile, neck).semantic == Fp::BoneSemantic::Neck, "Neck stays separate");
            Check(Fp::MatchBonePath(&profile, spine).semantic == Fp::BoneSemantic::Preserve, "Spine2 stays intact");
            Check(Fp::MatchBonePath(&profile, spine + "/Bip001_L_Clavicle").semantic == Fp::BoneSemantic::Preserve,
                  "basic torso/shoulder retained");
            Check(Fp::MatchBonePath(&profile, head + "Fake").semantic == Fp::BoneSemantic::Preserve,
                  "path component boundary");
            Check(!Fp::MatchBonePath(&profile, "chr_0999_future_postmodel/" + head).rule, "foreign actor rejected");
            Check(!Fp::MatchBonePath(&profile, "Root/Soldier_Root/Rush01/" + head).rule, "nested soldier excluded");
            Check(!Fp::MatchBonePath(&profile, head, "future-version").rule, "unknown version fallback");
            Check(Fp::MatchBonePath(&profile, head, source.revision).rule != nullptr, "known version");
            for (size_t j = 0; j < source.local_rule_count; ++j) {
                const auto& rule = source.local_rules[j];
                Check(Fp::FindLocalRule(&profile, full + std::string(rule.renderer_path), rule.lod) != nullptr,
                      "observed local scope");
                Check((source.lod_mask & (1u << rule.lod)) != 0, "scope LOD is observed");
                Check(rule.renderer_path.substr(0, 9) == "Mesh_all/", "shadow/proxy cannot enter local scope");
            }
        }
    }
    Check(graph_sources == 64, "own world/UI primary skeletons only");
    for (auto malformed : {"/Root/Bip001", "Root//Bip001", "Root/../Bip001", "Root/./Bip001",
                           "Root\\Bip001", "Root/Bip001/"})
        Check(!Fp::ValidPath(malformed), "reject noncanonical path");
    Check(!Fp::ValidPath(std::string_view("Root/\0Bip001", 12)), "reject embedded NUL");
}

void AccessoriesAndTail() {
    struct Expected { const char* model; const char* root; };
    const Expected hats[] = {
        {"chr_0002_endminm", "maozi_a1_M"}, {"chr_0003_endminf", "maozi_a1_M"},
        {"chr_0004_pelica", "maozi_base_M_a_01_jnt_ctrl"},
        {"chr_0007_ikut", "Bip001_L_Clavicle/maozi_base_L_a_01_jnt"},
        {"chr_0027_tangtang", "hat_base_M_a_01_jnt"}, {"chr_0032_lizhiyan", "hat_01_jnt"}
    };
    for (const auto& expected : hats) {
        auto profile = Fp::LookupProfile(expected.model);
        auto match = Fp::MatchBonePath(profile, spine + "/" + expected.root);
        Check(match.semantic == Fp::BoneSemantic::Hide, "authorized extra accessory root enabled");
        Check(match.rule->evidence == Fp::Evidence::NamePathInferred && match.rule->region == Fp::Region::HeadAccessory,
              "name/path inference must not claim weight verification");
        Check(Fp::MatchBonePath(profile, spine + "/" + expected.root + "/child").semantic == Fp::BoneSemantic::Hide,
              "precise accessory subtree");
    }
    auto aglina = Fp::LookupProfile("chr_0013_aglina");
    auto tail = Fp::MatchBonePath(aglina, "Root/Bip001/Bip001_Pelvis/tail_base_M_a_01_jnt/tail_base_M_a_02_jnt");
    Check(tail.semantic == Fp::BoneSemantic::Hide && tail.rule->region == Fp::Region::Tail, "verified tail path");
    Check(!Fp::MatchBonePath(aglina, "tail_base_M_a_01_jnt").rule, "detached tail name cannot hide");
    Check(Fp::MatchBonePath(aglina, spine + "/hat_01_jnt").semantic == Fp::BoneSemantic::Preserve,
          "another role's hat rule cannot leak");
}

void LocalGeometry() {
    auto profile = Fp::LookupProfile("chr_0034_typhoea");
    auto hair = Fp::FindLocalRule(profile, "Mesh_all/lod0/S_actor_typhoea_hair_01_lod0", 0);
    auto face = Fp::FindLocalRule(profile, "Mesh_all/lod0/S_actor_typhoea_face_01_lod0", 0);
    Check(hair && face, "Typhoea face/hair scopes");
    Check(!Fp::FindLocalRule(profile, "Mesh_all/lod0/S_actor_typhoea_cloth_02_lod0"),
          "rabbit/campus cloth_02 never renderer-wide exception");
    Check(!Fp::FindLocalRule(profile, "Mesh_all/lod0/S_actor_typhoea_body_01_lod0"), "body scope excluded");
    Check(!Fp::FindLocalRule(profile, "Shadow_Proxy/SP_Desktop/S_actor_typhoea_hair_01_shadowProxyDesktop"),
          "complete shadow geometry excluded");
    Check(!Fp::FindLocalRule(profile, hair->renderer_path, 1), "wrong LOD rejected");
    Check(!Fp::FindLocalRule(profile, hair->renderer_path, 0, "future-version"), "unknown scope version fallback");
    Fp::LocalVertex boundary{0.25, 0.25, 0.5, 0.5, 0.5, true};
    Check(Fp::ShouldHideLocalVertex(hair, boundary), ">=0.5 head/neck includes exact threshold");
    auto below = boundary; below.head_weight -= 1.0 / 65535; below.other_weight += 1.0 / 65535;
    Check(!Fp::ShouldHideLocalVertex(hair, below), "below majority preserves body");
    Fp::LocalVertex collar{0.0, 0.7, 0.3, -0.25, 0.8, true};
    Check(Fp::ShouldHideLocalVertex(face, collar), "authorized local collar trim");
    auto torso = collar; torso.along = -2;
    Check(!Fp::ShouldHideLocalVertex(face, torso), "torso below neck excluded");
    auto remote = boundary; remote.radius = 4.01;
    Check(!Fp::ShouldHideLocalVertex(hair, remote), "space threshold rejects remote geometry");
    auto unknown = boundary; unknown.frame_valid = false;
    Check(!Fp::ShouldHideLocalVertex(hair, unknown), "missing spatial evidence rejected");
    auto nan = boundary; nan.head_weight = std::numeric_limits<double>::quiet_NaN();
    Check(!Fp::ShouldHideLocalVertex(hair, nan), "invalid skin rejected");
    Check(!Fp::ShouldHideLocalVertex(nullptr, boundary), "scope mandatory");
    Check(Fp::ShouldHideLocalTriangle(hair, {boundary, collar, boundary}), "head/neck boundary triangle");
    Check(!Fp::ShouldHideLocalTriangle(hair, {boundary, torso, boundary}), "triangle touching torso retained");
    auto purrche = Fp::LookupProfile("chr_0038_purrche");
    auto fur = Fp::FindLocalRule(purrche, "Mesh_all/lod0/S_actor_purrchena_fur_03_lod0", 0);
    Check(fur && fur->evidence == Fp::Evidence::DrawWeightsVerified, "Purrche fur03 existing PC weight evidence");
    Check(fur->original_mesh_name == "S_actor_purrchena_fur_03_lod0_20", "Mesh name differs from renderer scope");
    Check(!Fp::FindLocalRule(purrche, "Mesh_all/lod0/S_actor_purrchena_fur_03_lod0_20", 0),
          "mesh name cannot be substituted for observed renderer path");
    Check(Fp::ShouldHideLocalVertex(fur, boundary), "fur03 head/neck mixed accepted locally");
    Check(!Fp::ShouldHideLocalVertex(fur, collar), "fur requires some actual Head contribution");
    Check(!Fp::MatchBonePath(purrche, head).rule, "supplemental scope is not fabricated complete bone graph");
    auto fur01 = Fp::FindLocalRule(purrche, "Mesh_all/lod0/S_actor_purrchena_fur_01_lod0", 0);
    Check(fur01 && !Fp::ShouldHideLocalVertex(fur01, {0.0, 0.0, 1.0, 0.5, 0.5, true}),
          "fur01 scarf/torso vertices preserved");
}
// Raw VB/IB audit streams transient, normalized evidence into the actual C++
// helper. It exports no game vertex/index payload and stores no palette index.
int ProbeStdin() {
    std::string operation, model, path;
    while (std::cin >> operation >> std::quoted(model) >> std::quoted(path)) {
        auto profile = Fp::LookupProfile(model);
        if (operation == "B") {
            auto match = Fp::MatchBonePath(profile, path);
            std::cout << int(match.semantic) << ' ' << int(match.rule ? match.rule->region : Fp::Region::Body) << '\n';
        } else if (operation == "V") {
            Fp::LocalVertex vertex;
            int lod = -1, frame = 0;
            if (!(std::cin >> lod >> vertex.head_weight >> vertex.neck_weight >> vertex.other_weight >>
                  vertex.along >> vertex.radius >> frame)) return 2;
            vertex.frame_valid = frame != 0;
            std::cout << Fp::ShouldHideLocalVertex(Fp::FindLocalRule(profile, path, lod), vertex) << '\n';
        } else return 3;
    }
    return std::cin.eof() ? 0 : 4;
}
} // namespace

int main(int argc, char** argv) {
    if (argc == 2 && std::string_view(argv[1]) == "--probe-stdin") return ProbeStdin();
    CoverageAndPaths(); AccessoriesAndTail(); LocalGeometry();
    std::cout << "first_person_profiles: " << checks << " checks passed\n";
}
