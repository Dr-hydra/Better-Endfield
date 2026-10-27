#include "bem_compose_internal.h"
#include "bem_compose_logic.h"
#include <algorithm>
#include <array>
#include <chrono>
#include <cctype>
#include <cmath>
#include <cstring>
#include <fstream>
#include <map>
#include <set>
#include <stdexcept>

namespace BetterEndfieldNext::CustomModel {
namespace {
using J=nlohmann::json;
constexpr uint64_t MiB=1024*1024;
void Require(bool ok,const std::string& error) { if(!ok) throw std::runtime_error(error); }
std::string Utf8(const std::filesystem::path& path) { auto s=path.u8string(); return {s.begin(),s.end()}; }
std::string Stable(const J& value) {
    auto s=value.get<std::string>();
    Require(!s.empty() && s.size()<=80 && std::isalnum(static_cast<unsigned char>(s[0])),"COMPOSE_PROJECT_ID: invalid project ID");
    for(auto c:s) Require((c>='a'&&c<='z') || (c>='A'&&c<='Z') || (c>='0'&&c<='9') || c=='_' || c=='-' || c=='.',"COMPOSE_PROJECT_ID: invalid project ID");
    Require(s.back()!='.',"COMPOSE_PROJECT_ID: trailing dot"); return s;
}
struct Source { std::unique_ptr<BemPackageReader> reader; J manifest, generation; std::map<uint32_t,std::string> components; };
struct Component { J descriptor; std::map<size_t,uint32_t> sources; std::string resource; bool atomic=false; std::string atomicGroup; };
struct Context { J request,plan; std::vector<Source> sources; std::map<std::string,Component> components; std::map<std::string,J> resources; uint16_t minor=2; };
J Generation(const std::filesystem::path& path) {
    return {{"path",Utf8(std::filesystem::canonical(path))},{"size",std::filesystem::file_size(path)},
        {"write_time",std::filesystem::last_write_time(path).time_since_epoch().count()}};
}
bool Ordinary(const J& resource) {
    // Fail closed: unknown roots are one atomic item, never guessed apart.
    auto name=resource.at("name").get<std::string>();
    std::transform(name.begin(),name.end(),name.begin(),[](unsigned char ch){return char(std::tolower(ch));});
    if(name.find("ultimate")!=name.npos || name.find("ability")!=name.npos || name.find("mirror")!=name.npos) return false;
    return name.find("postmodel")!=name.npos || name.find("uimodel")!=name.npos;
}
J Candidate(size_t source,const Source& s,const J& operation) {
    return {{"id","s"+std::to_string(source)},{"source_index",source},
        {"source_package_id",s.reader->Info().package_id},{"name",s.reader->Info().name},
        {"operation",operation.at("operation")}};
}
J RenameCondition(J condition,const std::map<std::string,std::string>& ids) {
    if(condition.is_boolean()) return condition;
    if(condition.contains("eq")) condition["eq"][0]=ids.at(condition.at("eq").at(0).get<std::string>());
    else if(condition.contains("not")) condition["not"]=RenameCondition(condition.at("not"),ids);
    else for(const auto* key:{"all","any"}) if(condition.contains(key))
        for(auto& child:condition[key]) child=RenameCondition(std::move(child),ids);
    return condition;
}
J And(J a,J b) {
    if(a==true) return b; if(b==true) return a;
    return J{{"all",J::array({std::move(a),std::move(b)})}};
}
void SegmentOptionDependencies(const J& condition,std::set<std::string>& groups) {
    if(condition.is_boolean()) return;
    if(condition.contains("eq")) groups.insert(condition.at("eq").at(0).get<std::string>());
    else if(condition.contains("not")) SegmentOptionDependencies(condition.at("not"),groups);
    else for(const auto* key:{"all","any"}) if(condition.contains(key))
        for(const auto& child:condition.at(key)) SegmentOptionDependencies(child,groups);
}
J Any(const J& conditions) {
    if(conditions.empty()) return false; if(conditions.size()==1) return conditions[0];
    J level=J::array();
    for(size_t i=0;i<conditions.size();i+=32) {
        J batch=J::array();for(size_t j=i;j<std::min(i+32,conditions.size());++j) batch.push_back(conditions[j]);
        level.push_back(J{{"any",batch}});
    }
    return level.size()==1?level[0]:Any(level);
}
#include "bem_compose_body.h"
#include "bem_compose_segments.h"
void Prepare(const J& request,Context& c) {
    Require(request.is_object() && request.value("schema",0)==1,"COMPOSE_REQUEST: expected schema 1");
    const std::set<std::string> requestKeys={"schema","platform","project_id","name","author","version","sources"};
    for(const auto& [key,value]:request.items()) Require(requestKeys.contains(key),"COMPOSE_UNSUPPORTED_FEATURE: request option "+key);
    const auto platform=request.value("platform",std::string{"windows-x64"});
    Require(platform=="windows-x64" || platform=="android-arm64","COMPOSE_UNSUPPORTED_PLATFORM: unknown platform");
    Stable(request.at("project_id"));
    for(const auto* key:{"name","author","version"}) {
        auto s=request.at(key).get<std::string>(); Require(!s.empty() && s.size()<=256 && s.find(char(0))==s.npos,"COMPOSE_REQUEST: invalid metadata");
    }
    const auto& inputs=request.at("sources");
    Require(inputs.is_array() && !inputs.empty() && inputs.size()<=16,"COMPOSE_SOURCES: select 1 to 16 distinct packages per output");
    c.request=request; J identity; bool explicitTargets=false;
    std::map<std::string,J> rootContracts;
    std::vector<std::set<std::string>> atomicRoots;
    J generations=J::array();
    for(size_t si=0;si<inputs.size();++si) {
        const auto& input=inputs.at(si); Source s; const auto path=std::filesystem::u8path(input.at("path").get<std::string>());
        const std::set<std::string> sourceKeys={"path","selection","parameters","resource_names"};
        for(const auto& [key,value]:input.items()) Require(sourceKeys.contains(key),"COMPOSE_UNSUPPORTED_FEATURE: source option "+key);
        Require(input.value("parameters","").empty(),"COMPOSE_UNSUPPORTED_FEATURE: saved parameters are not implemented");
        s.generation=Generation(path);
        for(const auto& earlier:c.sources) Require(!std::filesystem::equivalent(path,std::filesystem::u8path(earlier.generation.at("path").get<std::string>())),"COMPOSE_SOURCES: duplicate source file");
        s.reader=std::make_unique<BemPackageReader>(); std::string failure;
        const bool opened=s.reader->Open(path,failure); Require(opened,"COMPOSE_SOURCE_UNREADABLE: "+failure);
        const auto& info=s.reader->Info(); const auto raw=J::parse(info.manifest_json);
        auto outputId=request.at("project_id").get<std::string>(),sourceId=info.package_id;
        for(auto* id:{&outputId,&sourceId}) std::transform(id->begin(),id->end(),id->begin(),[](unsigned char ch){return char(std::tolower(ch));});
        Require(outputId!=sourceId,"COMPOSE_PROJECT_ID: output ID must differ from source IDs");
        Require(info.minor<=5,"COMPOSE_UNSUPPORTED_FEATURE: unknown BEM format");
        for(const auto* key:{"parameters","parameter_groups","mesh_deformations","drivers"})
            Require(!raw.contains(key) || raw.at(key).empty(),"COMPOSE_UNSUPPORTED_FEATURE: parameters, deformations and drivers are not implemented");
        Require(!raw.contains("root_offset") && !raw.contains("visual_root_offset"),"COMPOSE_UNSUPPORTED_FEATURE: root offsets are not implemented");
        for(const auto& mesh:raw.at("meshes")) for(const auto& draw:mesh.at("draws"))
            Require(!draw.contains("material_overrides") || draw.at("material_overrides").empty(),"COMPOSE_UNSUPPORTED_FEATURE: material property overrides are not implemented");
        const auto& target=raw.at("target"); const bool explicitSource=target.contains("resources");
        std::set<std::string> selectedResources;
        if(input.contains("resource_names")) {
            Require(explicitSource && input.at("resource_names").is_array() && !input.at("resource_names").empty(),"COMPOSE_RESOURCE_MISMATCH: resource filter requires explicit resources");
            for(const auto& name:input.at("resource_names")) selectedResources.insert(name.get<std::string>());
        }
        // Catalog revisions record provenance; compatibility is checked against
        // the actual resource and receiver contracts below.
        J current={{"kind",info.target_kind},{"id",info.target_id.empty()?info.character_id:info.target_id},
            {"explicit",explicitSource}};
        if(!explicitSource) {current["world"]=target.at("world_resource");current["ui"]=target.at("ui_resource");}
        if(si==0) { identity=current; explicitTargets=explicitSource; }
        Require(current==identity,"COMPOSE_TARGET_MISMATCH: target or legacy/explicit resource contract differs");
        const bool snap=s.reader->Composition(input.value("selection",""),s.manifest,failure);
        Require(snap,"COMPOSE_SELECTION: "+failure);
        std::map<std::string,std::string> resourceMap; std::set<std::string> roots;
        if(explicitTargets) for(auto resource:target.at("resources")) {
            if(!selectedResources.empty() && !selectedResources.contains(resource.at("name").get<std::string>())) continue;
            const auto& platforms=resource.at("platforms");
            if(std::find(platforms.begin(),platforms.end(),platform)==platforms.end()) continue;
            const auto local=resource.at("id").get<std::string>(); resource.erase("id");
            resource["platforms"]=J::array({platform}); const auto key=resource.dump();
            auto [contract,added]=rootContracts.emplace(resource.at("name").get<std::string>(),resource);
            Require(added || contract->second==resource,"COMPOSE_RESOURCE_MISMATCH: resource path, LOD or contract differs");
            Require(std::none_of(resourceMap.begin(),resourceMap.end(),[&](const auto& p){return p.second==key;}),"COMPOSE_RESOURCE_MISMATCH: duplicate resource receiver");
            resourceMap.emplace(local,key); c.resources.emplace(key,resource);
            if(info.target_kind=="weapon" || !Ordinary(resource)) roots.insert(key);
        }
        Require(!explicitTargets || !resourceMap.empty(),"COMPOSE_UNSUPPORTED_PLATFORM: no resources for requested platform");
        if(!selectedResources.empty()) Require(resourceMap.size()==selectedResources.size(),"COMPOSE_RESOURCE_MISMATCH: filtered resource is unavailable on requested platform");
        atomicRoots.push_back(roots);
        for(auto descriptor:target.at("components")) {
            const auto local=descriptor.at("id").get<uint32_t>(); std::string resource; bool atomic=false;
            if(explicitTargets) {
                const auto it=resourceMap.find(descriptor.at("resource").get<std::string>());
                if(it==resourceMap.end()) continue; resource=it->second; descriptor["resource"]=resource; atomic=roots.contains(resource);
            }
            descriptor.erase("id");
            const auto key=J::array({resource,descriptor.at("mesh_name")}).dump();
            auto [entry,added]=c.components.try_emplace(key,Component{descriptor,{},resource,atomic});
            Require(added || entry->second.descriptor==descriptor,"COMPOSE_COMPONENT_IDENTITY_MISMATCH: receiver bone/material/index contract differs");
            Require(entry->second.sources.emplace(si,local).second,"COMPOSE_COMPONENT_IDENTITY_MISMATCH: duplicate receiver");
            s.components.emplace(local,key);
        }
        Require(Generation(path)==s.generation,"COMPOSE_SOURCE_CHANGED: source changed while planning");
        generations.push_back(s.generation); c.sources.push_back(std::move(s));
    }
    Require(c.components.size()<=kMaxBemComponents,"COMPOSE_COMPONENT_BUDGET: unique receivers exceed 64");
    Require(c.resources.size()<=32,"COMPOSE_RESOURCE_BUDGET: resource roots exceed 32");
    Require(!c.components.empty(),"COMPOSE_RESOURCE_MISMATCH: no compatible components");
    // Atomic candidates must carry exactly the same roots and receiver contracts.
    std::vector<std::set<std::string>> atomicSets;
    std::map<std::string,std::string> atomicGroups;
    for(const auto& roots:atomicRoots) if(!roots.empty()) {
        if(std::find(atomicSets.begin(),atomicSets.end(),roots)!=atomicSets.end()) continue;
        for(const auto& root:roots) Require(!atomicGroups.contains(root),"COMPOSE_ATOMIC_ROOT_MISMATCH: overlapping atomic resource sets differ");
        const auto id=atomicSets.empty()?"atomic":"atomic"+std::to_string(atomicSets.size());
        for(const auto& root:roots) atomicGroups.emplace(root,id);
        atomicSets.push_back(roots);
    }
    J slots=J::array(),atoms=J::array(); size_t index=0;
    for(auto& [key,component]:c.components) {
        if(component.atomic) {
            component.atomicGroup=atomicGroups.at(component.resource);
            for(size_t si=0;si<c.sources.size();++si) if(atomicRoots[si].contains(component.resource))
                Require(component.sources.contains(si),"COMPOSE_ATOMIC_ROOT_MISMATCH: atomic receiver table differs");
            continue;
        }
        J choices=J::array();
        for(const auto& [si,local]:component.sources) choices.push_back(Candidate(si,c.sources[si],J{{"operation","source"}}));
        choices.push_back({{"id","original"},{"name","Original"},{"operation","keep"}});
        const auto first=component.sources.contains(0)?"s0":"original";
        const auto label=component.descriptor.at("mesh_name").get<std::string>();
        const auto partId="part"+std::to_string(index++);J segments=J::array();
        for(const auto& [si,local]:component.sources) {
            std::set<uint32_t> seenMeshes;
            for(const auto& op:c.sources[si].manifest.at("component_rules").at(local).at("candidates")) {
                if(op.at("operation")!="replace") continue;
                const auto mi=op.at("mesh").get<uint32_t>();if(!seenMeshes.insert(mi).second) continue;
                const auto& mesh=c.sources[si].manifest.at("meshes").at(mi);
                const auto& draws=mesh.at("draws");
                J strides=J::array();for(const auto& stream:mesh.at("streams"))strides.push_back(stream.at("stride"));
                J layout={{"static",mesh.value("renderer_kind",std::string{})=="static"},{"attributes",mesh.at("attributes")},{"strides",strides}};
                for(size_t di=0;di<draws.size();++di) {
                    const auto& draw=draws[di];
                    std::set<std::string> dependencies;
                    const auto& manifest=c.sources[si].manifest;
                    SegmentOptionDependencies(draw.value("when",J(true)),dependencies);
                    auto textureDependencies=[&](const J& refs) {
                        for(const auto& ref:refs) if(ref.is_object() && ref.contains("slot"))
                            for(const auto& slot:manifest.value("texture_slots",J::array())) if(slot.at("id")==ref.at("slot"))
                                for(const auto& candidate:slot.at("candidates")) SegmentOptionDependencies(candidate.value("when",J(true)),dependencies);
                    };
                    textureDependencies(draw.at("textures"));
                    for(const auto& candidate:manifest.at("component_rules").at(local).at("candidates")) {
                        if(candidate.at("operation")=="replace" && candidate.at("mesh")==mi)
                            SegmentOptionDependencies(candidate.value("when",J(true)),dependencies);
                        if(candidate.contains("material_overrides")) for(const auto& material:candidate.at("material_overrides"))
                            if(material.at("material_name")==draw.at("material_name")) {
                                SegmentOptionDependencies(candidate.value("when",J(true)),dependencies);
                                textureDependencies(material.at("textures"));
                            }
                    }
                    J bindings=J::array();
                    for(const auto& option:manifest.at("option_groups")) if(dependencies.contains(option.at("id").get<std::string>()))
                        bindings.push_back({{"id",option.at("id")},{"name",option.at("name")}});
                    segments.push_back({{"id",partId+".d"+std::to_string(segments.size())},{"source_index",si},
                        {"source_id","s"+std::to_string(si)},{"source_name",c.sources[si].reader->Info().name},
                        {"mesh",mi},{"draw",di},{"preview_key","s"+std::to_string(si)+".m"+std::to_string(mi)+".d"+std::to_string(di)},{"material_name",draw.at("material_name")},
                        {"option_bindings",bindings},{"layout",layout},{"count",draw.at("count")},{"default",first=="s"+std::to_string(si)?"on":"off"}});
                    if(BodyReceiver(component.descriptor)) {
                        const J base=segments.back();segments.erase(segments.size()-1);
                        const auto partition=BodyPartition(c.sources[si],mesh,draw);
                        for(const auto& region:BodyRegions) {
                            auto segment=base;segment["id"]=partId+".d"+std::to_string(segments.size());
                            segment["body_draw"]=base.at("preview_key");segment["body_region"]=region;
                            segment["preview_key"]=base.at("preview_key").get<std::string>()+".r."+region;
                            segment["triangles"]=partition.at(region);segment["count"]=partition.at(region).size()*3;
                            segments.push_back(std::move(segment));
                        }
                    }
                }
            }
        }
        // A gate or palette used by every draw is a shared source option, not
        // a separate physical part attached to every row (e.g. sockscolor).
        for(const auto& [si,local]:component.sources) {
            std::set<std::string> common;bool firstSegment=true;
            for(const auto& segment:segments) if(segment.at("source_index")==si) {
                std::set<std::string> ids;for(const auto& binding:segment.at("option_bindings"))ids.insert(binding.at("id").get<std::string>());
                if(firstSegment) {common=ids;firstSegment=false;}
                else std::erase_if(common,[&](const auto& id){return !ids.contains(id);});
            }
            bool hasSpecific=false;
            for(const auto& segment:segments) if(segment.at("source_index")==si)
                for(const auto& binding:segment.at("option_bindings")) if(!common.contains(binding.at("id").get<std::string>()))hasSpecific=true;
            if(common.empty() || !hasSpecific)continue;
            for(auto& segment:segments) if(segment.at("source_index")==si) {
                J specific=J::array(),shared=J::array();
                for(const auto& binding:segment.at("option_bindings"))
                    (common.contains(binding.at("id").get<std::string>())?shared:specific).push_back(binding);
                segment["option_bindings"]=std::move(specific);segment["shared_option_bindings"]=std::move(shared);
            }
        }
        if(!segments.empty()) choices.push_back({{"id","split"},{"name","Draw segments"},{"operation","split"}});
        slots.push_back({{"id",partId},{"key",key},{"display_name",label},{"segments",segments},
            {"resource_name",component.resource.empty()?std::string{}:c.resources.at(component.resource).at("name").get<std::string>()},
            {"renderer_path",component.descriptor.value("renderer_path","")},{"default",first},{"candidates",choices}});
    }
    for(size_t ai=0;ai<atomicSets.size();++ai) {
        const auto& atomicSet=atomicSets[ai];
        J choices=J::array();
        J names=J::array();for(const auto& key:atomicSet) names.push_back(c.resources.at(key).at("name"));
        for(size_t si=0;si<c.sources.size();++si) if(atomicRoots[si]==atomicSet)
            choices.push_back(Candidate(si,c.sources[si],J{{"operation","atomic"}}));
        choices.push_back({{"id","original"},{"name","Original"},{"operation","keep"}});
        atoms.push_back({{"id",ai==0?"atomic":"atomic"+std::to_string(ai)},{"display_name",identity.at("kind")=="weapon"?"Weapon":"Ability / other resources"},
            {"resources",atomicSet},{"resource_names",names},{"default","original"},{"optional",true},{"candidates",choices}});
    }
    size_t sourceGroups=0; J sourceOptions=J::array();
    for(size_t si=0;si<c.sources.size();++si) {
        const auto& s=c.sources[si]; sourceGroups+=s.manifest.at("option_groups").size();
        sourceOptions.push_back({{"source_index",si},{"package_id",s.reader->Info().package_id},{"name",s.reader->Info().name},{"groups",s.manifest.at("option_groups")}});
    }
    Require(slots.size()+atoms.size()<=64,"COMPOSE_TOO_MANY_GROUPS"); c.minor=explicitTargets?4:2;
    c.plan={{"schema",1},{"platform",c.request.value("platform",std::string{"windows-x64"})},{"request",request},{"generations",generations},
        {"target_kind",identity.at("kind")},{"target_id",identity.at("id")},{"bem_minor",c.minor},{"package_version",request.at("version")},
        {"slots",slots},{"atomic_items",atoms},{"estimated_components",c.components.size()},
        {"component_limit",64},{"estimated_groups",slots.size()+atoms.size()+sourceGroups},{"source_options",sourceOptions},
        {"warnings",J::array({"Source option groups, availability and constraints are preserved with independent namespaces.",
            "Geometry seams and visual compatibility require in-game verification."})}};
    if(!atoms.empty()) c.plan["warnings"].push_back("Nonstandard resources are kept together as one atomic item.");
}
}
bool PlanBemCompositionPart(const J& request,J& plan,std::string& error) {
    error.clear(); plan=J();
    try {Context c; Prepare(request,c); plan=std::move(c.plan);return true;}
    catch(const std::exception& e) {error=e.what();return false;}
}
bool BuildBemCompositionPart(const J& plan,const J& defaults,const std::filesystem::path& destination,
        J& report,const std::function<void()>& checkpoint,std::string& error,bool previewOnly) {
    error.clear(); report=J(); std::filesystem::path stage; bool ownsStage=false;
    try {
        Require(defaults.is_object(),"COMPOSE_CHOICES: expected an object of group IDs to candidate IDs");
        Context c; checkpoint(); Prepare(plan.at("request"),c);
        Require(c.plan==plan,"COMPOSE_STALE_PLAN: source generation or plan changed; plan again");
        std::set<std::string> omittedAtoms;
        for(const auto& atom:plan.at("atomic_items")) {
            const auto id=atom.at("id").get<std::string>();
            if(defaults.value(id,atom.at("default").get<std::string>())=="original") omittedAtoms.insert(id);
        }
        if(!omittedAtoms.empty()) {
            std::erase_if(c.components,[&](const auto& entry){return entry.second.atomic && omittedAtoms.contains(entry.second.atomicGroup);});
            std::erase_if(c.resources,[&](const auto& entry){
                return std::none_of(c.components.begin(),c.components.end(),[&](const auto& component){return component.second.resource==entry.first;});
            });
        }
        Require(!c.components.empty(),"COMPOSE_CHOICES: select at least one whole ability or weapon item");
        const auto output=std::filesystem::absolute(destination).lexically_normal();
        Require(output.has_filename() && !std::filesystem::exists(output),"COMPOSE_OUTPUT_EXISTS: output directory must be new");
        Require(std::filesystem::is_directory(output.parent_path()),"COMPOSE_OUTPUT: parent directory does not exist");
        uint64_t diskBytes=4*MiB;
        for(const auto& s:c.sources) diskBytes+=s.generation.at("size").get<uint64_t>();
        Require(std::filesystem::space(output.parent_path()).available>=diskBytes*2,"COMPOSE_DISK_SPACE");
        for(unsigned attempt=0;attempt<100;++attempt) {
            stage=output.parent_path()/(Utf8(output.filename())+".stage-"+
                std::to_string(std::chrono::steady_clock::now().time_since_epoch().count())+"-"+std::to_string(attempt));
            if(std::filesystem::create_directory(stage)) {ownsStage=true;break;}
        }
        Require(ownsStage,"COMPOSE_OUTPUT: cannot create staging directory");
        const auto package=stage/"composition.bem",spool=stage/"payloads.tmp";
        std::ofstream data(spool,std::ios::binary); Require(bool(data),"COMPOSE_OUTPUT: cannot create payload spool");
#pragma pack(push,1)
        struct Header { char magic[8]; uint16_t major,minor; uint32_t size; uint64_t file,manifest; uint32_t count,flags; };
        struct Entry { uint32_t codec,reserved; uint64_t offset,stored,decoded; };
#pragma pack(pop)
        static_assert(sizeof(Header)==40 && sizeof(Entry)==32);
        std::vector<Entry> entries; std::map<std::pair<size_t,uint32_t>,uint32_t> payloads,meshes,textures;
        uint64_t spoolBytes=0;
        auto payload=[&](size_t source,J& reference) {
            const auto key=std::make_pair(source,reference.get<uint32_t>()); const auto found=payloads.find(key);
            if(found!=payloads.end()) {reference=found->second;return;}
            Require(entries.size()<16384,"COMPOSE_PAYLOAD_LIMIT: directory exceeds 16384");
            Entry e{}; std::string failure; checkpoint();
            const bool copied=c.sources[source].reader->CopyPayload(key.second,data,e.codec,e.stored,e.decoded,checkpoint,failure);
            Require(copied,"COMPOSE_SOURCE_UNREADABLE: "+failure); spoolBytes+=e.stored;
            Require(spoolBytes<=2ull*1024*MiB,"COMPOSE_PAYLOAD_LIMIT: output exceeds 2 GiB");
            reference=entries.size(); payloads.emplace(key,uint32_t(entries.size())); entries.push_back(e);
        };
        auto generatedPayload=[&](const std::vector<uint8_t>& bytes) {
            checkpoint();Require(!bytes.empty(),"COMPOSE_SEGMENT_PAYLOAD: empty geometry");
            Require(entries.size()<16384,"COMPOSE_PAYLOAD_LIMIT: too many payloads");
            Entry e{};e.stored=e.decoded=bytes.size();
            data.write(reinterpret_cast<const char*>(bytes.data()),bytes.size());
            Require(bool(data),"COMPOSE_OUTPUT: geometry write failed");spoolBytes+=e.stored;
            Require(spoolBytes<=2ull*1024*MiB,"COMPOSE_PAYLOAD_LIMIT: output exceeds 2 GiB");
            const auto id=uint32_t(entries.size());entries.push_back(e);return id;
        };
        J manifest={{"schema",1},{"package_id",Stable(c.request.at("project_id"))},
            {"name",c.request.at("name")},{"author",c.request.at("author")},{"version",c.request.at("version")},
            {"target",c.sources[0].manifest.at("target")},{"meshes",J::array()},{"textures",J::array()},
            {"option_groups",J::array()},{"component_rules",J::array()},{"selection_constraints",J::array()}};
        std::vector<std::map<std::string,std::string>> optionIds(c.sources.size()),slotIds(c.sources.size());
        for(size_t si=0;si<c.sources.size();++si) {
            size_t i=0;
            for(const auto& g:c.sources[si].manifest.at("option_groups"))
                optionIds[si].emplace(g.at("id").get<std::string>(),"s"+std::to_string(si)+".g"+std::to_string(i++));
            i=0;for(const auto& slot:c.sources[si].manifest.value("texture_slots",J::array()))
                slotIds[si].emplace(slot.at("id").get<std::string>(),"s"+std::to_string(si)+".t"+std::to_string(i++));
        }
        auto& target=manifest["target"]; target["snapshot"]="Composed with Better Endfield Next"; target["components"]=J::array();
        std::map<std::string,std::string> resources;
        if(!c.resources.empty()) {
            target["resources"]=J::array();
            for(const auto& [key,descriptor]:c.resources) {
                auto resource=descriptor; const auto id="res"+std::to_string(resources.size()); resources.emplace(key,id);
                resource["id"]=id; target["resources"].push_back(std::move(resource));
            }
        }
        std::map<std::string,uint32_t> componentIds;
        for(const auto& [key,component]:c.components) {
            auto descriptor=component.descriptor; descriptor["id"]=componentIds.size();
            componentIds.emplace(key,uint32_t(componentIds.size()));
            if(!component.resource.empty()) descriptor["resource"]=resources.at(component.resource);
            target["components"].push_back(std::move(descriptor));
        }
        auto component=[&](size_t si,J& ref) {ref=componentIds.at(c.sources[si].components.at(ref.get<uint32_t>()));};
        auto textureRefs=[&](size_t si,J& refs) {
            for(auto& ref:refs) {
                if(ref.is_object()) {ref["slot"]=slotIds[si].at(ref.at("slot").get<std::string>());continue;}
                const auto key=std::make_pair(si,ref.get<uint32_t>()); auto found=textures.find(key);
                if(found==textures.end()) {
                    auto texture=c.sources[si].manifest.at("textures").at(key.second);
                    // Only draft snapshots carry this identity. The host enables its
                    // use exclusively for its own generated composition snapshots.
                    texture.erase("preview_source");
                    if(previewOnly) texture["preview_source"]={{"generation",c.sources[si].generation},{"texture",key.second}};
                    payload(si,texture["payload"]);
                    Require(manifest["textures"].size()<4096,"COMPOSE_TEXTURE_LIMIT");
                    const auto index=uint32_t(manifest["textures"].size()); manifest["textures"].push_back(std::move(texture));
                    found=textures.emplace(key,index).first;
                }
                ref=found->second;
            }
        };
        auto meshRef=[&](size_t si,J& ref) {
            const auto key=std::make_pair(si,ref.get<uint32_t>()); auto found=meshes.find(key);
            if(found==meshes.end()) {
                auto mesh=c.sources[si].manifest.at("meshes").at(key.second);
                for(auto& stream:mesh["streams"]) payload(si,stream["payload"]);
                for(auto& bone:mesh["bones"]) component(si,bone["component"]);
                size_t drawIndex=0;
                for(auto& draw:mesh["draws"]) {
                    draw.erase("composition_triangles");
                    draw["composition_segment"]="s"+std::to_string(si)+".m"+std::to_string(key.second)+".d"+std::to_string(drawIndex++);
                    for(const auto& slot:plan.at("slots"))for(const auto& segment:slot.at("segments"))
                        if(segment.contains("body_draw") && segment.at("body_draw")==draw.at("composition_segment")) {draw["composition_body_draw"]=segment.at("body_draw");break;}
                    payload(si,draw["indices"]); component(si,draw["material_component"]); textureRefs(si,draw["textures"]);
                    if(draw.contains("when")) draw["when"]=RenameCondition(draw.at("when"),optionIds[si]);
                }
                Require(manifest["meshes"].size()<4096,"COMPOSE_MESH_LIMIT");
                const auto index=uint32_t(manifest["meshes"].size()); manifest["meshes"].push_back(std::move(mesh));
                found=meshes.emplace(key,index).first;
            }
            ref=found->second;
        };
        J groups=plan.at("slots");
        ApplyBodyEdits(groups,defaults.value("body_edits",J::object()));
        J segmentDefaults=J::object();
        for(auto& group:groups) {
            const auto parent=group.at("id").get<std::string>();
            const bool refined=defaults.value(parent,group.at("default").get<std::string>())=="split";
            if(!refined) {
                J choices=J::array();for(const auto& choice:group.at("candidates")) if(choice.at("id")!="split") choices.push_back(choice);
                group["candidates"]=choices;
            }
            for(const auto& segment:group.at("segments")) {
                const auto id=segment.at("id").get<std::string>();const auto value=defaults.value(id,segment.at("default").get<std::string>());
                Require(value=="on" || value=="off","COMPOSE_CHOICES: invalid segment choice for "+id);
                segmentDefaults[id]=value;
            }
        }
        for(const auto& atom:plan.at("atomic_items"))
            if(!omittedAtoms.contains(atom.at("id").get<std::string>())) groups.push_back(atom);
        std::map<std::string,J> groupById; std::map<std::string,std::string> componentGroups;
        J effectiveDefaults=J::object();
        if(defaults.contains("body_edits"))effectiveDefaults["body_edits"]=defaults.at("body_edits");
        for(const auto& group:groups) {
            const auto id=group.at("id").get<std::string>(); groupById.emplace(id,group);
            if(group.contains("key")) componentGroups.emplace(group.at("key").get<std::string>(),id);
            const auto chosen=defaults.value(id,group.at("default").get<std::string>());
            J choices=J::array(); bool valid=false;
            for(const auto& candidate:group.at("candidates")) {
                choices.push_back({{"id",candidate.at("id")},{"name",candidate.at("name")}}); valid|=candidate.at("id")==chosen;
            }
            Require(valid,"COMPOSE_CHOICES: unknown candidate for "+id);
            effectiveDefaults[id]=chosen;
            manifest["option_groups"].push_back({{"id",id},{"name",group.at("display_name")},{"default",chosen},{"choices",choices}});
        }
        // One shared copy per source, even if the source controls many receivers.
        // Outer selectors precede imported groups so availability remains ordered.
        for(size_t si=0;si<c.sources.size();++si) {
            const auto& source=c.sources[si];const auto sourceId="s"+std::to_string(si);
            J uses=J::array();
            for(const auto& g:groups) for(const auto& choice:g.at("candidates"))
                if(choice.at("id")==sourceId) uses.push_back(J{{"eq",J::array({g.at("id"),sourceId})}});
            for(const auto& g:groups) if(g.contains("segments")) {
                const auto parent=g.at("id").get<std::string>();
                if(defaults.value(parent,g.at("default").get<std::string>())!="split") continue;
                for(const auto& segment:g.at("segments")) if(segment.at("source_id")==sourceId && segmentDefaults.at(segment.at("id").get<std::string>())=="on") {
                    uses.push_back(J{{"eq",J::array({parent,"split"})}});break;
                }
            }
            if(uses.empty()) continue;
            const J active=Any(uses);
            for(auto group:source.manifest.at("option_groups")) {
                group["id"]=optionIds[si].at(group.at("id").get<std::string>());
                group["name"]=std::to_string(si+1)+". "+source.reader->Info().name+" / "+group.at("name").get<std::string>();
                group["available_when"]=And(active,RenameCondition(group.value("available_when",J(true)),optionIds[si]));
                manifest["option_groups"].push_back(std::move(group));
            }
            for(const auto& constraint:source.manifest.value("selection_constraints",J::array()))
                manifest["selection_constraints"].push_back(J{{"any",J::array({J{{"not",active}},RenameCondition(constraint,optionIds[si])})}});
            for(auto slot:source.manifest.value("texture_slots",J::array())) {
                slot["id"]=slotIds[si].at(slot.at("id").get<std::string>());
                for(auto& candidate:slot["candidates"]) {
                    candidate["when"]=And(active,RenameCondition(candidate.value("when",J(true)),optionIds[si]));
                    if(!candidate.at("texture").is_null()) {
                        J refs=J::array({candidate.at("texture")});textureRefs(si,refs);candidate["texture"]=refs[0];
                    }
                }
                slot["candidates"].push_back({{"texture",nullptr},{"when",J{{"not",active}}}});
                manifest["texture_slots"].push_back(std::move(slot));
            }
        }
        // Preview drafts can override imported source defaults as well as outer selectors.
        for(auto& group:manifest["option_groups"]) {
            const auto id=group.at("id").get<std::string>();
            const auto chosen=defaults.value(id,group.at("default").get<std::string>());
            bool valid=false; for(const auto& choice:group.at("choices")) valid|=choice.at("id")==chosen;
            Require(valid,"COMPOSE_CHOICES: unknown candidate for "+id);
            group["default"]=chosen; effectiveDefaults[id]=chosen;
        }
        for(const auto& [id,value]:defaults.items()) {
            if(effectiveDefaults.contains(id) || omittedAtoms.contains(id) || segmentDefaults.contains(id)) continue;
            // Retain remembered choices for sources omitted by optional whole items.
            bool known=false;
            for(size_t si=0;si<c.sources.size();++si) for(const auto& group:c.sources[si].manifest.at("option_groups")) {
                if(optionIds[si].at(group.at("id").get<std::string>())!=id) continue;
                known=true; bool valid=false;
                for(const auto& choice:group.at("choices")) valid|=choice.at("id")==value;
                Require(valid,"COMPOSE_CHOICES: unknown candidate for "+id);
            }
            Require(known,"COMPOSE_CHOICES: unknown group "+id);
        }
        Require(manifest["option_groups"].size()<=64,"COMPOSE_TOO_MANY_GROUPS: source options plus part selectors exceed 64");
        J budgetRules=J::array();
        for(const auto& [key,receiver]:c.components) {
            const auto id=componentIds.at(key); const auto groupId=receiver.atomic?receiver.atomicGroup:componentGroups.at(key);
            J candidates=J::array();
            for(const auto& choice:groupById.at(groupId).at("candidates")) {
                const J gate={{"eq",J::array({groupId,choice.at("id")})}};
                if(choice.at("id")=="split") {
                    J active;
                    auto mesh=MergeCompositionSegments(c,receiver,groupById.at(groupId),segmentDefaults,optionIds,component,textureRefs,generatedPayload,active,checkpoint);
                    J op={{"operation","hide"},{"when",And(gate,J{{"not",active}})}};
                    candidates.push_back(op);budgetRules.push_back({{"group",groupId},{"choice","split"},{"operation",op}});
                    if(!mesh.is_null()) {
                        Require(manifest["meshes"].size()<4096,"COMPOSE_MESH_LIMIT");
                        op={{"operation","replace"},{"mesh",manifest["meshes"].size()},{"when",And(gate,active)}};
                        manifest["meshes"].push_back(std::move(mesh));candidates.push_back(op);
                        budgetRules.push_back({{"group",groupId},{"choice","split"},{"operation",op}});
                    }
                    continue;
                }
                J sourceCandidates=J::array({{{"operation","keep"}}});
                if(choice.contains("source_index")) {
                    const auto si=choice.at("source_index").get<size_t>();
                    const auto local=receiver.sources.at(si);
                    sourceCandidates=c.sources[si].manifest.at("component_rules").at(local).at("candidates");
                    for(auto& op:sourceCandidates) {
                        if(op.contains("when")) op["when"]=RenameCondition(op.at("when"),optionIds[si]);
                        if(op.at("operation")=="replace") meshRef(si,op["mesh"]);
                        if(op.contains("material_overrides")) for(auto& material:op["material_overrides"]) textureRefs(si,material["textures"]);
                    }
                }
                for(auto op:sourceCandidates) {
                    op["when"]=And(gate,op.value("when",J(true)));
                    budgetRules.push_back({{"group",groupId},{"choice",choice.at("id")},{"operation",op}});
                    candidates.push_back(std::move(op));
                }
            }
            manifest["component_rules"].push_back({{"target",id},{"candidates",candidates}});
        }
        manifest["composition_segments"]=J::array();
        for(const auto& group:groups) if(group.contains("segments") && defaults.value(group.at("id").get<std::string>(),group.at("default").get<std::string>())=="split")
            for(auto segment:group.at("segments")) {
                segment["enabled"]=segmentDefaults.at(segment.at("id").get<std::string>())=="on";
                segment["parent"]=group.at("id");manifest["composition_segments"].push_back(segment);
            }
        for(const auto& [id,value]:segmentDefaults.items()) if(defaults.contains(id)) effectiveDefaults[id]=value;
        std::set<std::string> caps={"composable-options"};
        if(manifest.contains("texture_slots")) caps.insert("texture-slots");
        for(const auto& s:c.sources) for(const auto& cap:s.manifest.at("required_capabilities")) {
            const auto name=cap.get<std::string>();
            if(name!="fixed-appearances" && name!="texture-slots" && name!="body-parameters" && name!="mesh-position-deltas" &&
                name!="material-overrides-v1" && name!="root-offset-v1" && name!="dynamic-drivers-v1") caps.insert(name);
        }
        manifest["required_capabilities"]=caps;
        uint64_t worstBytes=0,worstTextures=0;bool budgetExact=true;
        if(!previewOnly) {
            std::vector<uint64_t> sizes;for(const auto& entry:entries)sizes.push_back(entry.decoded);
            const auto bounds=ComposeResourceBounds(manifest,sizes,checkpoint);
            worstBytes=bounds.bytes;worstTextures=bounds.textures;budgetExact=bounds.exact;
            const auto details="bytes="+std::to_string(worstBytes)+"/"+std::to_string(kMaxBemResidentBytes)+
                ";textures="+std::to_string(worstTextures)+"/"+std::to_string(kMaxBemTextures);
            Require(worstBytes<=kMaxBemResidentBytes && worstTextures<=kMaxBemTextures,
                std::string(budgetExact?"COMPOSE_SELECTION_BUDGET: ":"COMPOSE_CONDITION_BUDGET: cannot prove resource limits; ")+details);
        }
        data.close();Require(bool(data),"COMPOSE_OUTPUT: spool flush failed");
        const auto json=manifest.dump();Require(json.size()<=4*MiB,"COMPOSE_MANIFEST_LIMIT");
        uint64_t end=sizeof(Header)+json.size()+entries.size()*sizeof(Entry);
        for(auto& entry:entries) {entry.offset=end;end+=entry.stored;}
        Require(end<=2ull*1024*MiB,"COMPOSE_PAYLOAD_LIMIT");
        Header header{};std::memcpy(header.magic,"BEM\0PKG\0",8);header.major=1;header.minor=c.minor;
        header.size=sizeof(Header);header.file=end;header.manifest=json.size();header.count=uint32_t(entries.size());
        std::ofstream out(package,std::ios::binary);Require(bool(out),"COMPOSE_OUTPUT: cannot create package");
        out.write(reinterpret_cast<const char*>(&header),sizeof(header));out.write(json.data(),json.size());
        out.write(reinterpret_cast<const char*>(entries.data()),entries.size()*sizeof(Entry));
        std::ifstream in(spool,std::ios::binary);std::array<char,65536> buffer{};
        while(in) {checkpoint();in.read(buffer.data(),buffer.size());out.write(buffer.data(),in.gcount());}
        Require(in.eof(),"COMPOSE_OUTPUT: spool read failed");out.close();Require(bool(out),"COMPOSE_OUTPUT: package flush failed");
        in.close();std::filesystem::remove(spool); checkpoint();
        BemPackageInfo info;std::string failure;const bool valid=ReadBemPackageInfo(package,info,failure);
        Require(valid,"COMPOSE_VALIDATION_FAILED: "+failure);
        uint64_t selectedBytes=0,selectedTextures=0;
        auto load=[&](const std::string& selection,const std::string& resource,bool allowUnreachable) {
            checkpoint();BemPocData parsed;std::string why;
            const bool ok=LoadBem(package,parsed,why,selection,nullptr,false,true,{},UINT64_MAX,previewOnly,resource);
            if(!ok && allowUnreachable && why=="Unreachable option combination") return false;
            Require(ok,"COMPOSE_VALIDATION_FAILED: "+why);
            uint64_t resident=0;
            for(const auto& texture:parsed.textures)resident+=texture.info.data_size;
            for(const auto& component:parsed.components) {
                for(const auto& stream:component.streams)resident+=stream.size();
                resident+=component.indices.size();
            }
            selectedBytes=std::max(selectedBytes,resident);selectedTextures=std::max<uint64_t>(selectedTextures,parsed.textures.size());
            return true;
        };
        auto validate=[&](const std::string& selection,bool allowUnreachable=false) {
            if(info.resources.empty()) return load(selection,{},allowUnreachable);
            for(const auto& resource:info.resources) if(!load(selection,resource.id,allowUnreachable)) return false;
            return true;
        };
        validate(info.default_options);size_t samples=1;
        if(!previewOnly) for(const auto& selection:ComposeValidationSelections(manifest,checkpoint)) {validate(selection);++samples;}
        for(const auto& s:c.sources) Require(Generation(std::filesystem::u8path(s.generation.at("path").get<std::string>()))==s.generation,"COMPOSE_SOURCE_CHANGED: source changed during build");
        J provenance=J::array();
        for(size_t si=0;si<c.sources.size();++si) {
            auto source=c.request.at("sources").at(si);const auto& info=c.sources[si].reader->Info();
            source["package_id"]=info.package_id;source["name"]=info.name;source["version"]=info.version;
            if(source.value("selection","").empty()) source["selection"]=info.minor?info.default_options:info.default_appearance;
            source["generation"]=c.sources[si].generation;provenance.push_back(std::move(source));
        }
        size_t rules=0;for(const auto& rule:manifest.at("component_rules")) rules+=rule.at("candidates").size();
        report={{"schema",1},{"project_id",c.request.at("project_id")},{"platform",c.request.value("platform",std::string{"windows-x64"})},
            {"target_kind",plan.at("target_kind")},{"target_id",plan.at("target_id")},{"bem_minor",c.minor},
            {"outputs",J::array({Utf8(output/"composition.bem")})},{"output_ids",J::array({c.request.at("project_id")})},{"bytes",end},{"payloads",entries.size()},
            {"groups",manifest.at("option_groups").size()},{"components",c.components.size()},{"validation_samples",samples},{"preview_only",previewOnly},
            {"worst_case_decoded_bytes_bound",previewOnly?J():J(worstBytes)},{"worst_case_texture_count_bound",previewOnly?J():J(worstTextures)},
            {"budget_exact",budgetExact},{"validated_decoded_bytes",selectedBytes},{"validated_texture_count",selectedTextures},
            {"decoded_bytes_limit",kMaxBemResidentBytes},{"texture_count_limit",kMaxBemTextures},
            {"sources",provenance},{"rules",rules},{"defaults",effectiveDefaults},{"warnings",plan.at("warnings")}};
        std::ofstream log(stage/"compose-report.json",std::ios::binary);log<<report.dump(2);log.close();
        Require(bool(log),"COMPOSE_OUTPUT: report flush failed");checkpoint();
        std::filesystem::rename(stage,output);ownsStage=false;return true;
    } catch(const std::exception& e) {
        error=e.what();report=J();
        if(ownsStage) {std::error_code ignored;std::filesystem::remove_all(stage,ignored);}
        return false;
    }
}
}
