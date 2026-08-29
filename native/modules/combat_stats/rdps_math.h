#pragma once

#include <cmath>
#include <span>
#include <vector>

namespace BetterEndfield::CombatStats {

struct RdpsMathZone {
    double multiplier = 1.0;
    std::vector<double> contributor_weights;
};

// Splits the externally created portion of one hit across multiplicative
// zones in log space, then across contributors by their observed weights.
// The returned fractions are relative to the final hit and therefore sum to
// exactly 1 - 1 / product(zone multipliers), apart from floating-point noise.
inline bool AllocateExternalFractions(std::span<const RdpsMathZone> zones,
    std::vector<std::vector<double>>& fractions,
    double& external_fraction) {
    fractions.clear();
    fractions.resize(zones.size());
    external_fraction = 0.0;
    double product = 1.0;
    double log_total = 0.0;
    for (const RdpsMathZone& zone : zones) {
        if (!std::isfinite(zone.multiplier) || zone.multiplier <= 1.0) {
            return false;
        }
        product *= zone.multiplier;
        log_total += std::log(zone.multiplier);
    }
    if (!std::isfinite(product) || product <= 1.0 ||
        !std::isfinite(log_total) || log_total <= 1.0e-12) {
        return false;
    }
    external_fraction = 1.0 - 1.0 / product;
    if (!std::isfinite(external_fraction) || external_fraction <= 1.0e-12) {
        return false;
    }
    for (size_t zone_index = 0; zone_index < zones.size(); ++zone_index) {
        const RdpsMathZone& zone = zones[zone_index];
        double weight_total = 0.0;
        for (double weight : zone.contributor_weights) {
            if (std::isfinite(weight) && weight > 0.0) weight_total += weight;
        }
        if (weight_total <= 1.0e-12) continue;
        const double zone_fraction = external_fraction *
            std::log(zone.multiplier) / log_total;
        fractions[zone_index].resize(zone.contributor_weights.size());
        for (size_t contributor_index = 0;
             contributor_index < zone.contributor_weights.size();
             ++contributor_index) {
            const double weight =
                zone.contributor_weights[contributor_index];
            fractions[zone_index][contributor_index] =
                std::isfinite(weight) && weight > 0.0
                ? zone_fraction * weight / weight_total : 0.0;
        }
    }
    return true;
}

} // namespace BetterEndfield::CombatStats
