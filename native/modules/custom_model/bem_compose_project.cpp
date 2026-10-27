#include "bem_compose_internal.h"
#include <algorithm>
#include <chrono>
#include <fstream>
#include <map>
#include <set>
#include <stdexcept>

namespace BetterEndfieldNext::CustomModel {
namespace {
using J = nlohmann::json;
void Require(bool ok, const std::string& error) { if (!ok) throw std::runtime_error(error); }
std::string Utf8(const std::filesystem::path& p) { auto s = p.u8string(); return {s.begin(), s.end()}; }
// Header versions do not identify the target branch. Keep ordinary world/UI
// contracts intact. Same-target explicit roots share source option state;
// different target identities and legacy/explicit contracts are companion parts.
J ProjectPlan(J request) {
    Require(request.is_object() && request.value("schema", 0) == 1, "COMPOSE_REQUEST: expected schema 1");
    const auto& sources = request.at("sources");
    Require(sources.is_array() && sources.size() >= 2 && sources.size() <= 16,
        "COMPOSE_SOURCES: select 2 to 16 distinct packages");
    struct Part { J sources = J::array(); std::set<std::string> roots; };
    std::map<std::string, size_t> keys;
    std::vector<Part> parts;
    std::set<std::string> characters, versions;
    std::vector<std::filesystem::path> paths;
    std::string kind, targetId;
    for (const auto& source : sources) {
        auto path = std::filesystem::u8path(source.at("path").get<std::string>());
        for (const auto& previous : paths)
            Require(!std::filesystem::equivalent(previous, path), "COMPOSE_SOURCES: duplicate source file");
        paths.push_back(path);
        BemPackageReader reader; std::string failure;
        const bool opened = reader.Open(path, failure);
        Require(opened, "COMPOSE_SOURCE_UNREADABLE: " + failure);
        const auto& info = reader.Info();
        const auto manifest = J::parse(info.manifest_json);
        const auto& target = manifest.at("target");
        const auto id = info.target_id.empty() ? info.character_id : info.target_id;
        if (kind.empty() || info.target_kind == "character") { kind = info.target_kind; targetId = id; }
        if (info.target_kind == "character") characters.insert(id);
        Require(characters.size() <= 1, "COMPOSE_TARGET_MISMATCH: select one character; weapons may be added as whole items");
        versions.insert(info.version);
        const bool explicitTarget = target.contains("resources");
        std::set<std::string> roots;
        if (explicitTarget) {
            for (const auto& r : target.at("resources")) {
                const auto& platforms = r.at("platforms");
                if (std::find(platforms.begin(), platforms.end(), request.value("platform", std::string{"windows-x64"})) == platforms.end()) continue;
                const auto name = r.at("name").get<std::string>(); roots.insert(name);
            }
        } else {
            roots.insert(target.at("world_resource").get<std::string>());
            roots.insert(target.at("ui_resource").get<std::string>());
        }
        auto add = [&](const std::set<std::string>& names) {
            const auto key = J::array({info.target_kind, id, explicitTarget}).dump();
            const auto [it, added] = keys.emplace(key, parts.size());
            if (added) parts.push_back(Part{});
            auto& part = parts[it->second]; part.roots.insert(names.begin(), names.end());
            J filtered = source;
            if (explicitTarget) filtered["resource_names"] = names;
            part.sources.push_back(std::move(filtered));
        };
        if (!explicitTarget) add(roots);
        else {
            Require(!roots.empty(), "COMPOSE_UNSUPPORTED_PLATFORM: no Windows resources");
            add(roots); // Keep one source's option state shared across all roots.
        }
    }
    if (!request.contains("version")) {
        std::string version;
        for (const auto& value : versions) { if (!version.empty()) version += " + "; version += value; }
        request["version"] = version.empty() ? "1" : version.size() <= 256 ? version : "composed";
    }
    // Validate top-level metadata and option keys before deriving output IDs.
    J validation = request, ignored; std::string failure;
    validation["sources"] = J::array({sources.at(0)});
    Require(PlanBemCompositionPart(validation, ignored, failure), failure);
    if (parts.size() == 1) {
        J result;
        request["sources"] = parts[0].sources;
        Require(PlanBemCompositionPart(request, result, failure), failure);
        return result;
    }
    std::set<std::string> occupied;
    J projects = J::array(), slots = J::array(), atoms = J::array(), warnings = J::array();
    size_t components = 0; unsigned minor = 2;
    for (size_t i = 0; i < parts.size(); ++i) {
        for (const auto& root : parts[i].roots)
            Require(occupied.insert(root).second,
                "COMPOSE_ATOMIC_ROOT_MISMATCH: overlapping output roots require the same resource set: " + root);
        J subrequest = request, subplan;
        const auto partId = "p" + std::to_string(i);
        subrequest["project_id"] = request.at("project_id").get<std::string>().substr(0, 64) + "." + partId;
        const auto name = request.at("name").get<std::string>();
        const auto suffix = std::string(" / ") + *parts[i].roots.begin();
        if (name.size() + suffix.size() <= 256) subrequest["name"] = name + suffix;
        subrequest["sources"] = parts[i].sources;
        Require(PlanBemCompositionPart(subrequest, subplan, failure), failure);
        components += subplan.at("estimated_components").get<size_t>();
        minor = std::max(minor, subplan.at("bem_minor").get<unsigned>());
        for (auto group : subplan.at("slots")) {
            for(auto& segment:group["segments"]) segment["id"]=partId+"."+segment.at("id").get<std::string>();
            group["id"] = partId + "." + group.at("id").get<std::string>(); slots.push_back(group);
        }
        for (auto group : subplan.at("atomic_items")) {
            group["id"] = partId + "." + group.at("id").get<std::string>(); atoms.push_back(group);
        }
        projects.push_back({{"id", partId}, {"plan", subplan}});
    }
    warnings.push_back("Ordinary world/UI models are preserved. Abilities and weapons are optional whole items, emitted as companion packages when needed.");
    warnings.push_back("Geometry seams and visual compatibility require in-game verification.");
    return {{"schema", 1}, {"platform", request.value("platform", std::string{"windows-x64"})}, {"request", request},
        {"target_kind", kind}, {"target_id", targetId}, {"bem_minor", minor},
        {"package_version", request.at("version")}, {"projects", projects},
        {"slots", slots}, {"atomic_items", atoms}, {"estimated_components", components},
        {"estimated_groups", slots.size() + atoms.size()}, {"component_limit", 64}, {"warnings", warnings}};
}
}
bool PlanBemComposition(const J& request, J& plan, std::string& error) {
    error.clear(); plan = J();
    try { plan = ProjectPlan(request); return true; }
    catch (const std::exception& e) { error = e.what(); return false; }
}
static bool BuildComposition(const J& plan, const J& defaults, const std::filesystem::path& destination,
    J& report, const std::function<void()>& checkpoint, std::string& error, bool previewOnly) {
    if (!plan.contains("projects")) return BuildBemCompositionPart(plan, defaults, destination, report, checkpoint, error, previewOnly);
    error.clear(); report = J(); std::filesystem::path stage; bool ownsStage = false;
    try {
        Require(defaults.is_object(), "COMPOSE_CHOICES: expected an object of group IDs to candidate IDs");
        checkpoint();
        Require(ProjectPlan(plan.at("request")) == plan, "COMPOSE_STALE_PLAN: source generation or plan changed; plan again");
        J groups = plan.at("slots"); for (const auto& group : plan.at("atomic_items")) groups.push_back(group);
        J effective = J::object();
        for (const auto& group : groups) {
            const auto id = group.at("id").get<std::string>();
            const auto value = defaults.value(id, group.at("default").get<std::string>());
            bool valid = false; for (const auto& candidate : group.at("candidates")) valid |= candidate.at("id") == value;
            Require(valid, "COMPOSE_CHOICES: unknown candidate for " + id); effective[id] = value;
        }
        for (const auto& part : plan.at("projects")) {
            const auto prefix=part.at("id").get<std::string>()+".";
            for(const auto& group:part.at("plan").at("slots")) for(const auto& segment:group.at("segments")) {
                const auto id=prefix+segment.at("id").get<std::string>();
                if(!defaults.contains(id)) continue;
                Require(defaults.at(id)=="on" || defaults.at(id)=="off","COMPOSE_CHOICES: invalid segment choice for "+id);
                effective[id]=defaults.at(id);
            }
            for(const auto& source:part.at("plan").at("source_options")) {
                size_t gi=0;
                for(const auto& group:source.at("groups")) {
                    const auto id=prefix+"s"+std::to_string(source.at("source_index").get<size_t>())+".g"+std::to_string(gi++);
                    if(!defaults.contains(id)) continue;
                    bool valid=false; for(const auto& choice:group.at("choices")) valid|=choice.at("id")==defaults.at(id);
                    Require(valid,"COMPOSE_CHOICES: unknown candidate for "+id); effective[id]=defaults.at(id);
                }
            }
        }
        if(defaults.contains("body_edits")) {
            Require(defaults.at("body_edits").is_object(),"COMPOSE_BODY: expected an edit map");std::set<std::string> known;
            for(const auto& part:plan.at("projects"))for(const auto& slot:part.at("plan").at("slots"))for(const auto& segment:slot.at("segments"))
                if(segment.contains("body_draw"))known.insert(segment.at("body_draw"));
            for(const auto& [key,value]:defaults.at("body_edits").items())Require(known.contains(key),"COMPOSE_BODY: unknown source draw");
            effective["body_edits"]=defaults.at("body_edits");
        }
        for (const auto& [id, value] : defaults.items()) Require(effective.contains(id), "COMPOSE_CHOICES: unknown group " + id);
        const auto output = std::filesystem::absolute(destination).lexically_normal();
        Require(output.has_filename() && !std::filesystem::exists(output), "COMPOSE_OUTPUT_EXISTS: output directory must be new");
        Require(std::filesystem::is_directory(output.parent_path()), "COMPOSE_OUTPUT: parent directory does not exist");
        for (unsigned attempt = 0; attempt < 100; ++attempt) {
            stage = output.parent_path() / (Utf8(output.filename()) + ".stage-" +
                std::to_string(std::chrono::steady_clock::now().time_since_epoch().count()) + "-" + std::to_string(attempt));
            if (std::filesystem::create_directory(stage)) { ownsStage = true; break; }
        }
        Require(ownsStage, "COMPOSE_OUTPUT: cannot create staging directory");
        J outputs = J::array(), ids = J::array(), reports = J::array(); uint64_t bytes = 0; unsigned actualMinor = 2;
        for (const auto& part : plan.at("projects")) {
            checkpoint(); const auto partId = part.at("id").get<std::string>();
            const auto& child = part.at("plan"); J choices = J::object(); bool selected = !child.at("slots").empty();
            J childGroups = child.at("slots"); for (const auto& atom : child.at("atomic_items")) childGroups.push_back(atom);
            for (const auto& group : childGroups) {
                const auto id = group.at("id").get<std::string>();
                const auto value = effective.at(partId + "." + id).get<std::string>(); choices[id] = value;
                selected |= value != "original";
            }
            if (!selected) continue;
            for(const auto& [id,value]:effective.items())
                if(id.starts_with(partId+".")) choices[id.substr(partId.size()+1)]=value;
            if(effective.contains("body_edits"))for(const auto& slot:child.at("slots"))for(const auto& segment:slot.at("segments"))
                if(segment.contains("body_draw") && effective.at("body_edits").contains(segment.at("body_draw")))
                    choices["body_edits"][segment.at("body_draw").get<std::string>()]=effective.at("body_edits").at(segment.at("body_draw"));
            J childReport; std::string failure;
            Require(BuildBemCompositionPart(child, choices, stage / partId, childReport, checkpoint, failure, previewOnly), failure);
            const auto path = Utf8(output / partId / "composition.bem");
            childReport["outputs"] = J::array({path});
            std::ofstream childLog(stage / partId / "compose-report.json", std::ios::binary);
            childLog << childReport.dump(2); childLog.close(); Require(bool(childLog), "COMPOSE_OUTPUT: report flush failed");
            outputs.push_back(path); ids.push_back(child.at("request").at("project_id"));
            bytes += childReport.at("bytes").get<uint64_t>(); reports.push_back(childReport);
            actualMinor = std::max(actualMinor, childReport.at("bem_minor").get<unsigned>());
        }
        Require(!outputs.empty(), "COMPOSE_CHOICES: select at least one whole ability or weapon item");
        checkpoint(); Require(ProjectPlan(plan.at("request")) == plan, "COMPOSE_SOURCE_CHANGED: source changed during build");
        report = {{"schema", 1}, {"project_id", plan.at("request").at("project_id")},
            {"platform", plan.at("platform")}, {"target_kind", plan.at("target_kind")}, {"target_id", plan.at("target_id")},
            {"bem_minor", actualMinor}, {"outputs", outputs}, {"output_ids", ids},
            {"bytes", bytes}, {"preview_only", previewOnly}, {"parts", reports}, {"defaults", effective}, {"warnings", plan.at("warnings")}};
        std::ofstream log(stage / "compose-report.json", std::ios::binary);
        log << report.dump(2); log.close(); Require(bool(log), "COMPOSE_OUTPUT: report flush failed");
        checkpoint(); std::filesystem::rename(stage, output); ownsStage = false; return true;
    } catch (const std::exception& e) {
        error = e.what(); report = J();
        if (ownsStage) { std::error_code ignored; std::filesystem::remove_all(stage, ignored); }
        return false;
    }
}
bool BuildBemComposition(const J& plan, const J& defaults, const std::filesystem::path& output,
    J& report, const std::function<void()>& checkpoint, std::string& error) {
    return BuildComposition(plan, defaults, output, report, checkpoint, error, false);
}
bool DraftBemComposition(const J& plan, const J& defaults, const std::filesystem::path& output,
    J& report, const std::function<void()>& checkpoint, std::string& error) {
    return BuildComposition(plan, defaults, output, report, checkpoint, error, true);
}

}
