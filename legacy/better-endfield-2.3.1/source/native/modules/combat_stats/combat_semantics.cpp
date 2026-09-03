#include "combat_semantics.h"

#include <algorithm>
#include <cerrno>
#include <charconv>
#include <cmath>
#include <cctype>
#include <cstdlib>
#include <fstream>
#include <limits>
#include <sstream>

namespace BetterEndfield::CombatStats {
namespace {

constexpr uint32_t kFlagDynamic = 1u << 0;
constexpr uint32_t kFlagGuarded = 1u << 1;
constexpr uint32_t kFlagProjection = 1u << 2;
constexpr uint32_t kFlagFormula = 1u << 3;
constexpr uint32_t kFlagExposeValue = 1u << 4;

std::vector<std::string_view> Split(std::string_view line, char separator) {
    std::vector<std::string_view> result;
    size_t start = 0;
    while (start <= line.size()) {
        const size_t end = line.find(separator, start);
        if (end == std::string_view::npos) {
            result.push_back(line.substr(start));
            break;
        }
        result.push_back(line.substr(start, end - start));
        start = end + 1;
    }
    return result;
}

SemanticStatus ParseStatus(std::string_view value) {
    if (value == "verified" || value == "verified_alias") {
        return SemanticStatus::Verified;
    }
    if (value == "candidate") return SemanticStatus::Candidate;
    if (value == "excluded") return SemanticStatus::Excluded;
    if (value == "structural") return SemanticStatus::Structural;
    return SemanticStatus::Unknown;
}

uint32_t ParseFlags(std::string_view text) {
    uint32_t result = 0;
    for (std::string_view value : Split(text, ',')) {
        if (value == "dynamic") result |= kFlagDynamic;
        else if (value == "guarded") result |= kFlagGuarded;
        else if (value == "projection") result |= kFlagProjection;
        else if (value == "formula") result |= kFlagFormula;
        else if (value == "expose_value") result |= kFlagExposeValue;
    }
    return result;
}

bool ParseDouble(std::string_view text, double& output) {
    if (text.empty()) return false;
    std::string value(text);
    char* end = nullptr;
    errno = 0;
    const double parsed = std::strtod(value.c_str(), &end);
    if (errno != 0 || end != value.c_str() + value.size() ||
        !std::isfinite(parsed)) {
        return false;
    }
    output = parsed;
    return true;
}

} // namespace

std::string CombatSemanticCatalog::Normalize(std::string_view value) {
    std::string result(value);
    std::transform(result.begin(), result.end(), result.begin(), [](unsigned char ch) {
        return static_cast<char>(std::tolower(ch));
    });
    return result;
}

bool CombatSemanticCatalog::Load(const std::filesystem::path& path,
    std::string& error) {
    path_.clear();
    summary_ = {};
    buffs_.clear();
    buff_aliases_.clear();
    skill_aliases_.clear();
    prefixes_.clear();

    std::ifstream input(path, std::ios::binary);
    if (!input) {
        error = "catalogue could not be opened: " + path.string();
        return false;
    }
    std::string line;
    if (!std::getline(input, line) || line != "BESEM\t1") {
        error = "catalogue header is invalid";
        return false;
    }
    summary_.schema_version = 1;
    size_t line_number = 1;
    while (std::getline(input, line)) {
        ++line_number;
        if (!line.empty() && line.back() == '\r') line.pop_back();
        if (line.empty()) continue;
        const std::vector<std::string_view> fields = Split(line, '\t');
        if (fields.empty()) continue;
        const auto malformed = [&]() {
            error = "catalogue row is invalid at line " +
                std::to_string(line_number);
            return false;
        };
        if (fields[0] == "M") {
            if (fields.size() != 3) return malformed();
            if (fields[1] == "gameVersion") summary_.game_version = fields[2];
            else if (fields[1] == "hotfixVersion") summary_.hotfix_version = fields[2];
            else if (fields[1] == "sourceSha256") summary_.source_sha256 = fields[2];
            continue;
        }
        if (fields[0] == "E") {
            if (fields.size() != 13 || fields[1].empty()) return malformed();
            SemanticEffect effect;
            effect.status = ParseStatus(fields[2]);
            effect.zone = ParseZone(fields[3]);
            effect.element = ParseElement(fields[4]);
            effect.value_key = fields[5];
            effect.has_static_rate = ParseDouble(fields[6], effect.static_rate);
            effect.role = fields[7];
            effect.source_kind = fields[8];
            effect.source_id = fields[9];
            effect.skill_id = fields[10];
            effect.confidence = fields[11];
            effect.flags = ParseFlags(fields[12]);
            BuffSemantic& buff = buffs_[Normalize(fields[1])];
            if (static_cast<uint8_t>(effect.status) <
                static_cast<uint8_t>(buff.status) ||
                buff.status == SemanticStatus::Unknown) {
                buff.status = effect.status;
            }
            buff.effects.push_back(std::move(effect));
            ++summary_.effect_count;
            continue;
        }
        if (fields[0] == "X") {
            if (fields.size() != 3 || fields[1].empty()) return malformed();
            BuffSemantic& buff = buffs_[Normalize(fields[1])];
            buff.status = SemanticStatus::Excluded;
            buff.exclusion_category = fields[2];
            ++summary_.excluded_count;
            continue;
        }
        if (fields[0] == "P") {
            if (fields.size() != 3 || fields[1].empty()) return malformed();
            PrefixRule rule;
            rule.prefix = Normalize(fields[1]);
            rule.semantic.status = ParseStatus(fields[2]);
            prefixes_.push_back(std::move(rule));
            continue;
        }
        if (fields[0] == "B" || fields[0] == "A") {
            if (fields.size() != 3 || fields[1].empty() || fields[2].empty()) {
                return malformed();
            }
            buff_aliases_[Normalize(fields[1])] = Normalize(fields[2]);
            continue;
        }
        if (fields[0] == "S") {
            if (fields.size() != 3 || fields[1].empty() || fields[2].empty()) {
                return malformed();
            }
            skill_aliases_[Normalize(fields[1])] = Normalize(fields[2]);
            continue;
        }
        return malformed();
    }
    if (input.bad() || buffs_.empty() || summary_.effect_count == 0) {
        error = input.bad() ? "catalogue read failed" : "catalogue has no effects";
        return false;
    }
    std::sort(prefixes_.begin(), prefixes_.end(), [](const PrefixRule& left,
        const PrefixRule& right) { return left.prefix.size() > right.prefix.size(); });
    summary_.buff_count = buffs_.size();
    summary_.buff_alias_count = buff_aliases_.size();
    summary_.skill_alias_count = skill_aliases_.size();
    path_ = path;
    return true;
}

bool CombatSemanticCatalog::LoadSourceMap(const std::filesystem::path& path,
    std::string& error) {
    buff_sources_.clear();
    skill_buff_edges_.clear();
    source_map_sha256_.clear();
    source_candidate_count_ = 0;
    skill_buff_edge_count_ = 0;
    std::ifstream input(path, std::ios::binary);
    if (!input) {
        error = "Buff source map could not be opened: " + path.string();
        return false;
    }
    std::string line;
    if (!std::getline(input, line) ||
        (line != "BESOURCE\t1" && line != "BESOURCE\t2")) {
        error = "Buff source map header is invalid";
        return false;
    }
    size_t line_number = 1;
    while (std::getline(input, line)) {
        ++line_number;
        if (!line.empty() && line.back() == '\r') line.pop_back();
        if (line.empty()) continue;
        const std::vector<std::string_view> fields = Split(line, '\t');
        if (fields.empty()) continue;
        if (fields[0] == "M") {
            if (fields.size() != 3) {
                error = "Buff source metadata is invalid at line " +
                    std::to_string(line_number);
                return false;
            }
            if (fields[1] == "sourceSha256") {
                source_map_sha256_ = std::string(fields[2]);
            }
            continue;
        }
        if (fields[0] == "S") {
            if (fields.size() != 3 || fields[1].empty() || fields[2].empty()) {
                error = "Skill-buff edge row is invalid at line " +
                    std::to_string(line_number);
                return false;
            }
            skill_buff_edges_[Normalize(fields[1])].insert(Normalize(fields[2]));
            ++skill_buff_edge_count_;
            continue;
        }
        if (fields[0] != "C" || fields.size() != 6 ||
            fields[1].empty() || fields[2].empty() || fields[3].empty()) {
            error = "Buff source row is invalid at line " +
                std::to_string(line_number);
            return false;
        }
        const bool valid_kind = fields[2] == "characterSkill" ||
            fields[2] == "characterPassive" || fields[2] == "weapon" ||
            fields[2] == "equipSuit" || fields[2] == "system";
        if (!valid_kind) {
            error = "Buff source kind is invalid at line " +
                std::to_string(line_number);
            return false;
        }
        BuffSourceCandidate candidate;
        candidate.kind = std::string(fields[2]);
        candidate.template_id = Normalize(fields[3]);
        candidate.source_skill_id = Normalize(fields[4]);
        candidate.trigger_skill_id = Normalize(fields[5]);
        buff_sources_[Normalize(fields[1])].push_back(std::move(candidate));
        ++source_candidate_count_;
    }
    if (input.bad() || buff_sources_.empty() || source_candidate_count_ == 0) {
        error = input.bad() ? "Buff source map read failed" :
            "Buff source map has no candidates";
        buff_sources_.clear();
        source_candidate_count_ = 0;
        return false;
    }
    return true;
}

const BuffSemantic* CombatSemanticCatalog::FindBuff(std::string_view id) const {
    const std::string normalized = ResolveBuffId(id);
    if (const auto found = buffs_.find(normalized); found != buffs_.end()) {
        return &found->second;
    }
    for (const PrefixRule& rule : prefixes_) {
        if (normalized.starts_with(rule.prefix)) {
            return &rule.semantic;
        }
    }
    return nullptr;
}

const std::vector<BuffSourceCandidate>*
CombatSemanticCatalog::FindBuffSources(std::string_view id) const {
    const std::string normalized = ResolveBuffId(id);
    if (const auto found = buff_sources_.find(normalized);
        found != buff_sources_.end()) {
        return &found->second;
    }
    return nullptr;
}

bool CombatSemanticCatalog::HasSkillBuffReference(std::string_view skill_id,
    std::string_view buff_id) const {
    const auto found = skill_buff_edges_.find(ResolveSkillId(skill_id));
    if (found == skill_buff_edges_.end()) return false;
    return found->second.contains(ResolveBuffId(buff_id));
}

std::string CombatSemanticCatalog::ResolveBuffId(std::string_view id) const {
    std::string normalized = Normalize(id);
    if (const auto found = buff_aliases_.find(normalized);
        found != buff_aliases_.end()) {
        return found->second;
    }
    return normalized;
}

std::string CombatSemanticCatalog::ResolveSkillId(std::string_view id) const {
    std::string normalized = Normalize(id);
    if (const auto found = skill_aliases_.find(normalized);
        found != skill_aliases_.end()) {
        return found->second;
    }
    return normalized;
}

const SemanticCatalogSummary& CombatSemanticCatalog::Summary() const noexcept {
    return summary_;
}

const std::filesystem::path& CombatSemanticCatalog::Path() const noexcept {
    return path_;
}

SemanticStatus CombatSemanticCatalog::ParseStatus(std::string_view value) noexcept {
    if (value == "verified" || value == "verified_alias") {
        return SemanticStatus::Verified;
    }
    if (value == "candidate") return SemanticStatus::Candidate;
    if (value == "excluded") return SemanticStatus::Excluded;
    if (value == "structural") return SemanticStatus::Structural;
    return SemanticStatus::Unknown;
}

const char* CombatSemanticCatalog::StatusId(SemanticStatus value) noexcept {
    switch (value) {
    case SemanticStatus::Verified: return "verified";
    case SemanticStatus::Candidate: return "candidate";
    case SemanticStatus::Excluded: return "excluded";
    case SemanticStatus::Structural: return "structural";
    default: return "unknown";
    }
}

const std::unordered_map<std::string, BuffSemantic>&
CombatSemanticCatalog::Buffs() const noexcept {
    return buffs_;
}

size_t CombatSemanticCatalog::SourceMappedBuffCount() const noexcept {
    return buff_sources_.size();
}

size_t CombatSemanticCatalog::SourceCandidateCount() const noexcept {
    return source_candidate_count_;
}

const std::string& CombatSemanticCatalog::SourceMapSha256() const noexcept {
    return source_map_sha256_;
}

namespace {

std::string JsonEscape(std::string_view input) {
    std::string output;
    output.reserve(input.size() + 8);
    for (const char ch : input) {
        if (ch == '\\') output += "\\\\";
        else if (ch == '"') output += "\\\"";
        else if (ch == '\n') output += "\\n";
        else if (ch == '\r') output += "\\r";
        else if (ch == '\t') output += "\\t";
        else output += ch;
    }
    return output;
}

std::string ExtractJsonString(std::string_view block, std::string_view key) {
    const std::string pattern = "\"" + std::string(key) + "\"";
    const size_t pos = block.find(pattern);
    if (pos == std::string_view::npos) return {};
    const size_t colon = block.find(':', pos + pattern.size());
    if (colon == std::string_view::npos) return {};
    const size_t quote_start = block.find('"', colon + 1);
    if (quote_start == std::string_view::npos) return {};
    const size_t quote_end = block.find('"', quote_start + 1);
    if (quote_end == std::string_view::npos) return {};
    return std::string(block.substr(quote_start + 1, quote_end - quote_start - 1));
}

bool ExtractJsonBool(std::string_view block, std::string_view key, bool default_value = false) {
    const std::string pattern = "\"" + std::string(key) + "\"";
    const size_t pos = block.find(pattern);
    if (pos == std::string_view::npos) return default_value;
    const size_t colon = block.find(':', pos + pattern.size());
    if (colon == std::string_view::npos) return default_value;
    const size_t true_pos = block.find("true", colon);
    const size_t false_pos = block.find("false", colon);
    if (true_pos != std::string_view::npos && (false_pos == std::string_view::npos || true_pos < false_pos)) {
        return true;
    }
    if (false_pos != std::string_view::npos) {
        return false;
    }
    return default_value;
}

} // namespace

bool CombatSemanticCatalog::LoadJson(const std::filesystem::path& path,
    std::string& error) {
    std::ifstream input(path, std::ios::binary);
    if (!input) {
        error = "json catalogue could not be opened: " + path.string();
        return false;
    }
    std::stringstream buffer;
    buffer << input.rdbuf();
    const std::string content = buffer.str();
    if (content.empty()) {
        error = "json catalogue is empty";
        return false;
    }

    // Find "buffs" object
    const size_t buffs_key = content.find("\"buffs\"");
    if (buffs_key == std::string_view::npos) {
        error = "json catalogue missing \"buffs\" key";
        return false;
    }
    const size_t open_brace = content.find('{', buffs_key);
    if (open_brace == std::string_view::npos) {
        error = "json catalogue malformed";
        return false;
    }

    size_t cursor = open_brace + 1;
    while (cursor < content.size()) {
        // Find next buff key
        const size_t key_start_quote = content.find('"', cursor);
        if (key_start_quote == std::string::npos) break;
        const size_t key_end_quote = content.find('"', key_start_quote + 1);
        if (key_end_quote == std::string::npos) break;
        const std::string buff_id = content.substr(
            key_start_quote + 1, key_end_quote - key_start_quote - 1);

        const size_t obj_start = content.find('{', key_end_quote + 1);
        if (obj_start == std::string::npos) break;
        const size_t obj_end = content.find('}', obj_start + 1);
        if (obj_end == std::string::npos) break;

        const std::string_view block(content.data() + obj_start, obj_end - obj_start + 1);
        SemanticEffect effect;
        effect.name = ExtractJsonString(block, "name");
        effect.source_kind = ExtractJsonString(block, "sourceKind");
        effect.source_id = ExtractJsonString(block, "sourceId");
        effect.skill_id = ExtractJsonString(block, "skillId");
        effect.zone = ParseZone(ExtractJsonString(block, "zone"));
        effect.status = ParseStatus(ExtractJsonString(block, "status"));
        effect.element = ParseElement(ExtractJsonString(block, "element"));
        effect.is_dynamic = ExtractJsonBool(block, "isDynamic", false);

        BuffSemantic& buff = buffs_[Normalize(buff_id)];
        buff.name = effect.name;
        buff.status = effect.status != SemanticStatus::Unknown ? effect.status : SemanticStatus::Candidate;
        buff.is_dynamic = effect.is_dynamic;
        if (buff.effects.empty()) {
            buff.effects.push_back(std::move(effect));
            ++summary_.effect_count;
        } else {
            buff.effects[0] = std::move(effect);
        }
        if (buff.is_dynamic) {
            ++summary_.dynamic_count;
        }

        cursor = obj_end + 1;
    }

    summary_.buff_count = buffs_.size();
    return true;
}

bool CombatSemanticCatalog::AppendDynamicEntry(const std::filesystem::path& path,
    const std::string& buff_id, const SemanticEffect& effect) {
    if (buff_id.empty()) return false;
    const std::string normalized = Normalize(buff_id);
    BuffSemantic& buff = buffs_[normalized];
    buff.name = effect.name;
    buff.status = effect.status != SemanticStatus::Unknown ? effect.status : SemanticStatus::Candidate;
    buff.is_dynamic = true;
    if (buff.effects.empty()) {
        buff.effects.push_back(effect);
        ++summary_.effect_count;
    } else {
        buff.effects[0] = effect;
    }
    ++summary_.dynamic_count;
    summary_.buff_count = buffs_.size();

    // Ensure parent directory exists
    std::error_code ec;
    if (path.has_parent_path()) {
        std::filesystem::create_directories(path.parent_path(), ec);
    }

    // Rewrite/Save JSON catalog with all dynamic/catalog entries
    std::ofstream output(path, std::ios::binary | std::ios::trunc);
    if (!output) return false;

    output << "{\n  \"schemaVersion\": 1,\n  \"buffs\": {\n";
    bool first = true;
    for (const auto& [id, item] : buffs_) {
        if (!first) output << ",\n";
        first = false;
        output << "    \"" << JsonEscape(id) << "\": {\n";
        if (!item.name.empty()) {
            output << "      \"name\": \"" << JsonEscape(item.name) << "\",\n";
        }
        const SemanticEffect* eff = item.effects.empty() ? nullptr : &item.effects[0];
        if (eff) {
            output << "      \"sourceKind\": \"" << JsonEscape(eff->source_kind) << "\",\n";
            output << "      \"sourceId\": \"" << JsonEscape(eff->source_id) << "\",\n";
            output << "      \"skillId\": \"" << JsonEscape(eff->skill_id) << "\",\n";
            output << "      \"zone\": \"" << ZoneId(eff->zone) << "\",\n";
            output << "      \"element\": \"" << ElementId(eff->element) << "\",\n";
        }
        output << "      \"status\": \"" << StatusId(item.status) << "\",\n";
        output << "      \"isDynamic\": " << (item.is_dynamic ? "true" : "false") << "\n";
        output << "    }";
    }
    output << "\n  }\n}\n";
    return true;
}

SemanticZone CombatSemanticCatalog::ParseZone(std::string_view value) noexcept {
    if (value == "atk" || value == "Attack") return SemanticZone::Attack;
    if (value == "dmg_inc" || value == "DamageIncrease") return SemanticZone::DamageIncrease;
    if (value == "amp" || value == "Amplification") return SemanticZone::Amplification;
    if (value == "fragile" || value == "Fragile") return SemanticZone::Fragile;
    if (value == "vuln_taken" || value == "VulnerabilityTaken") return SemanticZone::VulnerabilityTaken;
    if (value == "res" || value == "Resistance") return SemanticZone::Resistance;
    if (value == "combo" || value == "Combo") return SemanticZone::Combo;
    if (value == "arts_strength" || value == "ArtsStrength") return SemanticZone::ArtsStrength;
    return SemanticZone::Unknown;
}

SemanticElement CombatSemanticCatalog::ParseElement(std::string_view value) noexcept {
    if (value.empty() || value == "all" || value == "All") return SemanticElement::All;
    if (value == "physical" || value == "Physical") return SemanticElement::Physical;
    if (value == "fire" || value == "Fire") return SemanticElement::Fire;
    if (value == "pulse" || value == "Pulse") return SemanticElement::Pulse;
    if (value == "cryst" || value == "Cryst") return SemanticElement::Cryst;
    if (value == "natural" || value == "Natural") return SemanticElement::Natural;
    if (value == "spell" || value == "Spell") return SemanticElement::Spell;
    return SemanticElement::Unknown;
}

const char* CombatSemanticCatalog::ZoneId(SemanticZone value) noexcept {
    switch (value) {
    case SemanticZone::Attack: return "atk";
    case SemanticZone::DamageIncrease: return "dmg_inc";
    case SemanticZone::Amplification: return "amp";
    case SemanticZone::Fragile: return "fragile";
    case SemanticZone::VulnerabilityTaken: return "vuln_taken";
    case SemanticZone::Resistance: return "res";
    case SemanticZone::Combo: return "combo";
    case SemanticZone::ArtsStrength: return "arts_strength";
    default: return "unknown";
    }
}

const char* CombatSemanticCatalog::ElementId(SemanticElement value) noexcept {
    switch (value) {
    case SemanticElement::All: return "all";
    case SemanticElement::Physical: return "physical";
    case SemanticElement::Fire: return "fire";
    case SemanticElement::Pulse: return "pulse";
    case SemanticElement::Cryst: return "cryst";
    case SemanticElement::Natural: return "natural";
    case SemanticElement::Spell: return "spell";
    default: return "unknown";
    }
}

bool CombatSemanticCatalog::ElementMatches(SemanticElement effect,
    int32_t damage_type) noexcept {
    if (effect == SemanticElement::All) return true;
    // DamageType is declared in this order by Beyond.GEnums: Physical, Real,
    // Fire, Pulse, Cryst, LifeDrain, Natural, Ether.  The IL2CPP enum uses the
    // default contiguous values and the existing runtime path independently
    // confirms LifeDrain == 5.
    if (effect == SemanticElement::Physical) return damage_type == 0;
    if (effect == SemanticElement::Fire) return damage_type == 2;
    if (effect == SemanticElement::Pulse) return damage_type == 3;
    if (effect == SemanticElement::Cryst) return damage_type == 4;
    if (effect == SemanticElement::Natural) return damage_type == 6;
    if (effect == SemanticElement::Spell) {
        return damage_type == 2 || damage_type == 3 || damage_type == 4 ||
            damage_type == 6;
    }
    return false;
}

} // namespace BetterEndfield::CombatStats
