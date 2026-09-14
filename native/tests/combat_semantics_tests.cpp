#include "../modules/combat_stats/combat_semantics.h"
#include "../modules/combat_stats/rdps_math.h"

#include <algorithm>
#include <cmath>
#include <filesystem>
#include <iostream>
#include <numeric>
#include <string>
#include <vector>

using namespace BetterEndfield::CombatStats;

namespace {

bool NearlyEqual(double left, double right, double epsilon = 1.0e-12) {
    return std::abs(left - right) <= epsilon *
        std::max({1.0, std::abs(left), std::abs(right)});
}

int Fail(const std::string& message) {
    std::cerr << message << '\n';
    return 1;
}

} // namespace

int main(int argc, char** argv) {
    if (argc != 2) return Fail("usage: test <combat-semantics.besem>");
    CombatSemanticCatalog catalog;
    std::string error;
    if (!catalog.Load(std::filesystem::path(argv[1]), error)) {
        return Fail(error);
    }
    if (catalog.Summary().schema_version != 1 ||
        catalog.Summary().effect_count < 200 ||
        catalog.Summary().game_version != "1.4.4") {
        return Fail("catalogue summary regression");
    }
    const BuffSemantic* arts =
        catalog.FindBuff("buff_chr_0029_pograni_talent1");
    if (!arts || std::none_of(arts->effects.begin(), arts->effects.end(),
        [](const SemanticEffect& effect) {
            return effect.status == SemanticStatus::Verified &&
                effect.zone == SemanticZone::ArtsStrength &&
                effect.value_key == "physpell_up";
        })) {
        return Fail("arts-strength semantic regression");
    }
    if (catalog.ResolveBuffId("1029") !=
        "buff_chr_0016_laevat_ignore_fire_resist") {
        return Fail("numeric Buff alias regression");
    }
    if (!CombatSemanticCatalog::ElementMatches(SemanticElement::Fire, 2) ||
        CombatSemanticCatalog::ElementMatches(SemanticElement::Fire, 3)) {
        return Fail("damage element mapping regression");
    }

    const std::vector<RdpsMathZone> zones{
        {1.25, {0.10, 0.15}},
        {1.40, {0.40}},
        {1.10, {2.0, 1.0}},
    };
    std::vector<std::vector<double>> allocation;
    double external = 0.0;
    if (!AllocateExternalFractions(zones, allocation, external)) {
        return Fail("rDPS allocation rejected a valid input");
    }
    double sum = 0.0;
    for (const auto& zone : allocation) {
        sum = std::accumulate(zone.begin(), zone.end(), sum);
    }
    const double expected = 1.0 - 1.0 / (1.25 * 1.40 * 1.10);
    if (!NearlyEqual(external, expected) || !NearlyEqual(sum, expected)) {
        return Fail("rDPS conservation regression");
    }
    if (!NearlyEqual(allocation[0][0] / allocation[0][1], 0.10 / 0.15) ||
        !NearlyEqual(allocation[2][0] / allocation[2][1], 2.0)) {
        return Fail("rDPS within-zone weighting regression");
    }

    // Dynamic JSON catalog test
    const std::filesystem::path temp_json =
        std::filesystem::temp_directory_path() / "betterendfield_test_buff_catalog.json";
    SemanticEffect dynamic_effect;
    dynamic_effect.name = "测试动态Buff";
    dynamic_effect.source_kind = "character";
    dynamic_effect.source_id = "chr_0007_chen";
    dynamic_effect.skill_id = "sk_chr_0007_chen_01";
    dynamic_effect.zone = SemanticZone::DamageIncrease;
    dynamic_effect.element = SemanticElement::Physical;
    dynamic_effect.status = SemanticStatus::Candidate;
    dynamic_effect.is_dynamic = true;

    if (!catalog.AppendDynamicEntry(temp_json, "buff_dynamic_test_01", dynamic_effect)) {
        return Fail("AppendDynamicEntry failed");
    }

    const BuffSemantic* dyn_buff = catalog.FindBuff("buff_dynamic_test_01");
    if (!dyn_buff || !dyn_buff->is_dynamic || dyn_buff->effects.empty() ||
        dyn_buff->effects[0].source_id != "chr_0007_chen") {
        return Fail("Dynamic buff lookup regression");
    }

    CombatSemanticCatalog loaded_json_catalog;
    std::string json_load_error;
    if (!loaded_json_catalog.LoadJson(temp_json, json_load_error)) {
        return Fail("LoadJson failed: " + json_load_error);
    }
    const BuffSemantic* reloaded_dyn = loaded_json_catalog.FindBuff("buff_dynamic_test_01");
    if (!reloaded_dyn || reloaded_dyn->effects.empty() ||
        reloaded_dyn->effects[0].skill_id != "sk_chr_0007_chen_01") {
        return Fail("Reloaded dynamic buff JSON regression");
    }
    std::error_code ec;
    std::filesystem::remove(temp_json, ec);

    std::cout << "combat semantics and rDPS math passed\n";
    return 0;
}
