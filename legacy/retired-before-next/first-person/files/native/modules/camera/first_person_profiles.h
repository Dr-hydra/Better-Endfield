#pragma once

// Data/helper only: the caller owns live Transform ancestry, mesh cloning,
// visible indices, complete shadow geometry and restoration. No palette indices
// or renderer-wide BEM deletion are stored here.
#include <array>
#include <cmath>
#include <cstddef>
#include <cstdint>
#include <string_view>

namespace BetterEndfield::FirstPersonProfiles {

enum class BoneSemantic : uint8_t { Preserve = 0, Hide = 1, Neck = 2 };
enum class Region : uint8_t { Body, Head, Neck, Tail, HeadAccessory };
enum class Evidence : uint8_t { GraphVerified, NamePathInferred, DrawWeightsVerified };
enum class Scope : uint8_t { Head, Face, Hair, Fur, HeadAccessory };

struct BoneRule {
    std::string_view path;
    BoneSemantic semantic;
    bool subtree;
    Evidence evidence;
    Region region = Region::Body;
};
struct LocalRule {
    // Exact path relative to the actor model root, at an observed LOD. A BEM
    // may rename/reorder its mesh/palette; this scope never implies hide_all.
    std::string_view renderer_path, original_mesh_name;
    int lod;
    Scope scope;
    Evidence evidence;
    double min_head_neck_weight = 0.5;
    double min_head_weight = 0.0;
    // Neck origin -> Head origin is one unit. These are policy bounds, not
    // measured geometric extents. Pure Head/tail hiding uses the generic path.
    double min_along = -0.5, max_along = 4.0, max_radius = 4.0;
    std::string_view evidence_source;
};
struct Source {
    std::string_view model_root, platform, view, revision, source;
    std::string_view head_path, neck_path;
    uint32_t lod_mask;
    bool graph_verified;
    const BoneRule* bones;
    size_t bone_count;
    const LocalRule* local_rules;
    size_t local_rule_count;
};
struct Profile {
    std::string_view model_id, display_name;
    const Source* sources;
    size_t source_count;
};

#include "first_person_profiles.generated.inc"

inline const Profile* LookupProfile(std::string_view modelId) {
    if (modelId.empty()) return nullptr;
    for (const auto& profile : Generated::kProfiles) {
        if (profile.model_id == modelId) return &profile;
        for (size_t i = 0; i < profile.source_count; ++i)
            if (profile.sources[i].model_root == modelId) return &profile;
    }
    return nullptr; // Unknown models retain the caller's generic classification.
}
inline bool ValidPath(std::string_view path) {
    if (path.empty() || path.front() == '/' || path.back() == '/') return false;
    size_t begin = 0;
    while (begin < path.size()) {
        auto end = path.find('/', begin);
        if (end == std::string_view::npos) end = path.size();
        auto part = path.substr(begin, end - begin);
        if (part.empty() || part == "." || part == ".." ||
            part.find('\\') != std::string_view::npos || part.find('\0') != std::string_view::npos) return false;
        begin = end + 1;
    }
    return true;
}
inline bool AtOrBelow(std::string_view path, std::string_view root) {
    return !root.empty() && (path == root ||
        (path.size() > root.size() && path.substr(0, root.size()) == root && path[root.size()] == '/'));
}
// Full paths accept only this exact observed model root; relative paths must
// already be relative to the current actor (never to a detached donor/prop).
inline std::string_view RelativePath(const Source& source, std::string_view path) {
    if (!ValidPath(path)) return {};
    if (AtOrBelow(path, source.model_root)) {
        if (path.size() == source.model_root.size()) return {};
        return path.substr(source.model_root.size() + 1);
    }
    if (path.substr(0, 4) == "chr_") return {};
    return path;
}
inline bool RevisionMatches(const Source& source, std::string_view revision) {
    // With no version metadata, these are live-path structural hints. Their
    // recorded Windows origin is never promoted to Android weight evidence.
    return revision.empty() || revision == source.revision;
}
struct BoneMatch {
    BoneSemantic semantic = BoneSemantic::Preserve;
    const BoneRule* rule = nullptr;
    const Source* source = nullptr;
};
inline BoneMatch MatchBonePath(const Profile* profile, std::string_view path,
                              std::string_view revision = {}) {
    BoneMatch match;
    if (!profile) return match;
    for (size_t i = 0; i < profile->source_count; ++i) {
        const auto& source = profile->sources[i];
        if (!RevisionMatches(source, revision)) continue;
        auto relative = RelativePath(source, path);
        if (relative.empty()) continue;
        for (size_t j = 0; j < source.bone_count; ++j) {
            const auto& rule = source.bones[j];
            if ((rule.subtree ? AtOrBelow(relative, rule.path) : relative == rule.path) &&
                (!match.rule || rule.path.size() > match.rule->path.size()))
                match = {rule.semantic, &rule, &source};
        }
    }
    return match;
}
inline const LocalRule* FindLocalRule(const Profile* profile, std::string_view rendererPath,
                                     int lod = -1, std::string_view revision = {}) {
    if (!profile) return nullptr;
    for (size_t i = 0; i < profile->source_count; ++i) {
        const auto& source = profile->sources[i];
        if (!RevisionMatches(source, revision)) continue;
        auto relative = RelativePath(source, rendererPath);
        if (relative.empty()) continue;
        for (size_t j = 0; j < source.local_rule_count; ++j) {
            const auto& rule = source.local_rules[j];
            if (rule.renderer_path == relative && (lod < 0 || lod == rule.lod)) return &rule;
        }
    }
    return nullptr;
}
struct LocalVertex {
    // Sum strictly nonzero influences from the CURRENT draw/palette. Head
    // includes proven head accessory roots; tail counts as other here.
    double head_weight = 0, neck_weight = 0, other_weight = 0;
    double along = 0, radius = 0;
    bool frame_valid = false;
};
inline bool ShouldHideLocalVertex(const LocalRule* rule, const LocalVertex& vertex) {
    if (!rule || !vertex.frame_valid || !std::isfinite(vertex.along) ||
        !std::isfinite(vertex.radius) || vertex.radius < 0) return false;
    double total = 0;
    for (double weight : {vertex.head_weight, vertex.neck_weight, vertex.other_weight}) {
        if (!std::isfinite(weight) || weight < 0 || weight > 1.001) return false;
        total += weight;
    }
    if (total < 0.9 || total > 1.1) return false;
    return vertex.head_weight + vertex.neck_weight >= rule->min_head_neck_weight * total &&
        vertex.head_weight >= rule->min_head_weight * total &&
        vertex.along >= rule->min_along && vertex.along <= rule->max_along &&
        vertex.radius <= rule->max_radius;
}
inline bool ShouldHideLocalTriangle(const LocalRule* rule, const std::array<LocalVertex, 3>& vertices) {
    for (const auto& vertex : vertices) if (!ShouldHideLocalVertex(rule, vertex)) return false;
    return true;
}

} // namespace BetterEndfield::FirstPersonProfiles
