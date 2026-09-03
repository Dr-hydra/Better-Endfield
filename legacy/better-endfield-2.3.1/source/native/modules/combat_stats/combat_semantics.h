#pragma once

#include <cstdint>
#include <filesystem>
#include <string>
#include <string_view>
#include <unordered_map>
#include <unordered_set>
#include <utility>
#include <vector>

namespace BetterEndfield::CombatStats {

enum class SemanticStatus : uint8_t {
    Unknown = 0,
    Verified,
    Candidate,
    Excluded,
    Structural,
};

enum class SemanticZone : uint8_t {
    Unknown = 0,
    Attack,
    DamageIncrease,
    Amplification,
    Fragile,
    VulnerabilityTaken,
    Resistance,
    Combo,
    ArtsStrength,
};

enum class SemanticElement : uint8_t {
    All = 0,
    Physical,
    Fire,
    Pulse,
    Cryst,
    Natural,
    Spell,
    Unknown,
};

struct SemanticEffect {
    SemanticStatus status = SemanticStatus::Unknown;
    SemanticZone zone = SemanticZone::Unknown;
    SemanticElement element = SemanticElement::Unknown;
    std::string value_key;
    double static_rate = 0.0;
    bool has_static_rate = false;
    std::string role;
    std::string source_kind;
    std::string source_id;
    std::string skill_id;
    std::string confidence;
    uint32_t flags = 0;
    std::string name;
    bool is_dynamic = false;
};

struct BuffSemantic {
    SemanticStatus status = SemanticStatus::Unknown;
    std::string exclusion_category;
    std::string name;
    bool is_dynamic = false;
    std::vector<SemanticEffect> effects;
};

struct BuffSourceCandidate {
    std::string kind;
    std::string template_id;
    std::string source_skill_id;
    std::string trigger_skill_id;
};

struct SemanticCatalogSummary {
    uint32_t schema_version = 0;
    std::string game_version;
    std::string hotfix_version;
    std::string source_sha256;
    size_t buff_count = 0;
    size_t effect_count = 0;
    size_t excluded_count = 0;
    size_t buff_alias_count = 0;
    size_t skill_alias_count = 0;
    size_t dynamic_count = 0;
};

class CombatSemanticCatalog final {
public:
    static constexpr uint32_t kSupportedSchemaVersion = 1;

    bool Load(const std::filesystem::path& path, std::string& error);
    bool LoadSourceMap(const std::filesystem::path& path, std::string& error);
    bool LoadJson(const std::filesystem::path& path, std::string& error);
    bool AppendDynamicEntry(const std::filesystem::path& path,
        const std::string& buff_id, const SemanticEffect& effect);

    [[nodiscard]] const BuffSemantic* FindBuff(std::string_view id) const;
    [[nodiscard]] const std::vector<BuffSourceCandidate>* FindBuffSources(
        std::string_view id) const;
    [[nodiscard]] bool HasSkillBuffReference(std::string_view skill_id,
        std::string_view buff_id) const;
    [[nodiscard]] std::string ResolveBuffId(std::string_view id) const;
    [[nodiscard]] std::string ResolveSkillId(std::string_view id) const;
    [[nodiscard]] const SemanticCatalogSummary& Summary() const noexcept;
    [[nodiscard]] const std::filesystem::path& Path() const noexcept;
    [[nodiscard]] const std::unordered_map<std::string, BuffSemantic>& Buffs() const noexcept;
    [[nodiscard]] size_t SourceMappedBuffCount() const noexcept;
    [[nodiscard]] size_t SourceCandidateCount() const noexcept;
    [[nodiscard]] const std::string& SourceMapSha256() const noexcept;

    static SemanticStatus ParseStatus(std::string_view value) noexcept;
    static SemanticZone ParseZone(std::string_view value) noexcept;
    static SemanticElement ParseElement(std::string_view value) noexcept;
    static const char* StatusId(SemanticStatus value) noexcept;
    static const char* ZoneId(SemanticZone value) noexcept;
    static const char* ElementId(SemanticElement value) noexcept;
    static bool ElementMatches(SemanticElement effect, int32_t damage_type) noexcept;

private:
    struct PrefixRule {
        std::string prefix;
        BuffSemantic semantic;
    };

    static std::string Normalize(std::string_view value);

    std::filesystem::path path_;
    SemanticCatalogSummary summary_{};
    std::unordered_map<std::string, BuffSemantic> buffs_;
    std::unordered_map<std::string, std::string> buff_aliases_;
    std::unordered_map<std::string, std::string> skill_aliases_;
    std::vector<PrefixRule> prefixes_;
    std::unordered_map<std::string, std::vector<BuffSourceCandidate>> buff_sources_;
    std::unordered_map<std::string, std::unordered_set<std::string>> skill_buff_edges_;
    std::string source_map_sha256_;
    size_t source_candidate_count_ = 0;
    size_t skill_buff_edge_count_ = 0;
};

} // namespace BetterEndfield::CombatStats
