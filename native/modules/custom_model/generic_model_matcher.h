#pragma once
#include <algorithm>
#include <cstdint>
#include <iterator>
#include <span>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::CustomModel::GenericMatching {
enum class Region { Unknown, Lod0, Lod1, Lod2, Lod3, MobileProxy, DesktopProxy };
struct ReceiverKey {
    std::string character, resource, path;
    Region region=Region::Unknown;
    bool operator==(const ReceiverKey&) const = default;
};
inline Region ClassifyReceiver(std::string_view path) {
    constexpr std::string_view prefixes[]{"Mesh_all/lod0/","Mesh_all/lod1/","Mesh_all/lod2/",
        "Mesh_all/lod3/","Shadow_Proxy/SP_Mobile/","Shadow_Proxy/SP_Desktop/"};
    constexpr Region regions[]{Region::Lod0,Region::Lod1,Region::Lod2,Region::Lod3,
        Region::MobileProxy,Region::DesktopProxy};
    for (size_t i=0;i<std::size(prefixes);++i) if (path.starts_with(prefixes[i])) {
        const auto leaf=path.substr(prefixes[i].size());
        if (!leaf.empty() && leaf.find('/')==leaf.npos) return regions[i];
    }
    return Region::Unknown;
}
enum class PathStatus { Valid, OutsideRoot, Cycle, TooDeep, InvalidName };
struct RelativePath { PathStatus status=PathStatus::OutsideRoot; std::string path; };
// Walk actual parent objects. An outer scene parent/name never defines the root.
template<class Parent,class Name>
RelativePath CheckedRelativePath(void* root,void* transform,Parent&& parent,Name&& name,size_t limit=128) {
    if (!root || !transform) return {};
    std::vector<void*> visited;
    std::vector<std::string> names;
    while (transform!=root) {
        if (!transform) return {};
        if (std::find(visited.begin(),visited.end(),transform)!=visited.end()) return {PathStatus::Cycle,{}};
        if (visited.size()>=limit) return {PathStatus::TooDeep,{}};
        visited.push_back(transform);
        std::string part=name(transform);
        if (part.empty() || part.find('/')!=part.npos) return {PathStatus::InvalidName,{}};
        names.push_back(std::move(part)); transform=parent(transform);
    }
    RelativePath result{PathStatus::Valid,{}};
    for (auto it=names.rbegin();it!=names.rend();++it) {
        if (!result.path.empty()) result.path.push_back('/');
        result.path+=*it;
    }
    return result;
}
enum class DonorOrigin { Pristine, SavedOriginal, CompletedWithoutOriginal, Unavailable, Ambiguous };
struct MeshIdentity {
    DonorOrigin origin=DonorOrigin::Unavailable;
    void* mesh=nullptr;
    std::string name;
    uint64_t indices=0;
    bool indices_known=false;
    std::string detail; // Why lineage was refused; diagnostics only, never identity.
};
struct Candidate { ReceiverKey key; void* renderer=nullptr; MeshIdentity pristine; };
struct Request {
    std::string_view character,resource,mesh_name;
    Region region=Region::Lod0;
    uint64_t indices=0;
    bool check_indices=true;
    std::string_view verified_receiver_path;
};
enum class MatchStatus { Matched, Missing, Ambiguous, InvalidIndex };
struct Match { MatchStatus status=MatchStatus::Missing; size_t index=0; size_t valid_count=0; };
// Validation must be read-only. Upload/material construction starts after the
// complete component mapping is unique, including cross-component contracts.
template<class Validate>
Match SelectUnique(std::span<const Candidate> candidates,const Request& request,Validate&& validate) {
    if (request.region==Region::Unknown || request.character.empty() || request.resource.empty() || request.mesh_name.empty())
        return {MatchStatus::InvalidIndex,0,0};
    Match result;
    for (size_t i=0;i<candidates.size();++i) {
        const auto& c=candidates[i];
        if (!c.renderer || c.key.character!=request.character || c.key.resource!=request.resource ||
            c.key.region!=request.region || ClassifyReceiver(c.key.path)!=c.key.region) continue;
        for (size_t j=0;j<i;++j) if (candidates[j].key==c.key) return {MatchStatus::InvalidIndex,0,0};
        if (!request.verified_receiver_path.empty() && c.key.path!=request.verified_receiver_path) continue;
        const auto& identity=c.pristine;
        if ((identity.origin!=DonorOrigin::Pristine && identity.origin!=DonorOrigin::SavedOriginal) ||
            !identity.mesh || identity.name!=request.mesh_name ||
            (request.check_indices && (!identity.indices_known || identity.indices!=request.indices)) || !validate(c)) continue;
        result.index=i; ++result.valid_count;
    }
    result.status=result.valid_count==1?MatchStatus::Matched:
        result.valid_count?MatchStatus::Ambiguous:MatchStatus::Missing;
    return result;
}
inline bool DistinctReceivers(std::span<void* const> receivers) {
    for (size_t i=0;i<receivers.size();++i)
        if (!receivers[i] || std::find(receivers.begin(),receivers.begin()+i,receivers[i])!=receivers.begin()+i) return false;
    return true;
}
struct AssetScope { std::string platform, snapshot; };
struct ExactLodRelation {
    AssetScope scope;
    std::string character, source_resource, target_resource;
    std::string source_path, source_mesh, target_path, target_mesh;
    bool reference_confirmed=false, mesh_names_confirmed=false;
    uint64_t target_indices=0;
    bool target_indices_known=false;
};
struct LodCounterpart {
    std::string target_path, target_renderer, target_mesh;
    uint64_t target_indices=0;
    bool target_indices_known=false;
};
enum class RelationStatus { Matched, Missing, Ambiguous };
inline RelationStatus FindExactAndroidLod1Relation(std::span<const ExactLodRelation> relations,
    const AssetScope& scope,std::string_view character,std::string_view source_resource,
    std::string_view target_resource,std::string_view source_path,std::string_view source_mesh,LodCounterpart& out) {
    out={}; size_t matches=0;
    if (scope.platform!="Android" || scope.snapshot.empty() || ClassifyReceiver(source_path)!=Region::Lod0)
        return RelationStatus::Missing;
    for (const auto& r:relations) {
        if (r.scope.platform!=scope.platform || r.scope.snapshot!=scope.snapshot || r.character!=character ||
            r.source_resource!=source_resource || r.target_resource!=target_resource || r.source_path!=source_path ||
            r.source_mesh!=source_mesh || !r.reference_confirmed || !r.mesh_names_confirmed || r.target_mesh.empty() ||
            ClassifyReceiver(r.target_path)!=Region::Lod1) continue;
        ++matches;
        out={r.target_path,r.target_path.substr(std::string_view("Mesh_all/lod1/").size()),r.target_mesh,
            r.target_indices,r.target_indices_known};
    }
    return matches==1?RelationStatus::Matched:matches?RelationStatus::Ambiguous:RelationStatus::Missing;
}
// The exact legacy prefab contract covers an unsuffixed _lodN Mesh only.
// Source _20 and target _8 remain independent full Mesh identities. This
// function generates a location, never guesses a suffixed target Mesh name.
inline bool AndroidLod1Counterpart(std::string_view source_path,LodCounterpart& out) {
    out={};
    if (ClassifyReceiver(source_path)!=Region::Lod0) return false;
    auto renderer=source_path.substr(std::string_view("Mesh_all/lod0/").size());
    if (!renderer.ends_with("_lod0")) return false;
    out.target_renderer=renderer; out.target_renderer.back()='1';
    out.target_path="Mesh_all/lod1/"+out.target_renderer;
    out.target_mesh=out.target_renderer;
    return true;
}
// Proxy ownership is by original live Mesh reference. A single component may
// have several proxies; a shared original Mesh with several visible receivers
// needs independent ownership evidence and cannot be resolved by order.
enum class ProxyOwnerStatus { Matched, Missing, Ambiguous };
struct ProxyOwner { ProxyOwnerStatus status=ProxyOwnerStatus::Missing; size_t index=0; };
inline ProxyOwner FindProxyOwner(void* pristine_proxy_mesh,std::span<void* const> visible_pristine_meshes) {
    ProxyOwner result; size_t count=0;
    if (!pristine_proxy_mesh) return result;
    for (size_t i=0;i<visible_pristine_meshes.size();++i) if (visible_pristine_meshes[i]==pristine_proxy_mesh) {
        result.index=i; ++count;
    }
    result.status=count==1?ProxyOwnerStatus::Matched:count?ProxyOwnerStatus::Ambiguous:ProxyOwnerStatus::Missing;
    return result;
}
}
