// Production matcher contracts; no engine, graphics driver or game required.
#include "../modules/custom_model/generic_model_matcher.h"
#include <iostream>
#include <stdexcept>
#include <unordered_map>

using namespace BetterEndfieldNext::CustomModel::GenericMatching;
namespace {
size_t checks=0;
void Check(bool value,const char* message) { ++checks; if (!value) throw std::runtime_error(message); }
int mesh_a,mesh_b,renderer_a,renderer_b,proxy,root,outer,lod,mesh_all;
Candidate Body(std::string path="Mesh_all/lod0/Body_lod0") {
    return {{"chr_test","chr_test_uimodel",path,ClassifyReceiver(path)},&renderer_a,
        {DonorOrigin::Pristine,&mesh_a,"Body_lod0_20",51300,true}};
}
Request BodyRequest() { return {"chr_test","chr_test_uimodel","Body_lod0_20",Region::Lod0,51300,true,{}}; }
Match Find(std::span<const Candidate> candidates,const Request& request) {
    return SelectUnique(candidates,request,[](const auto&) { return true; });
}
void PathChecks() {
    std::unordered_map<void*,void*> parents{{&root,&outer},{&renderer_a,&lod},{&lod,&mesh_all},{&mesh_all,&root}};
    std::unordered_map<void*,std::string> names{{&renderer_a,"Body_lod0"},{&lod,"lod0"},{&mesh_all,"Mesh_all"}};
    auto path=[&] { return CheckedRelativePath(&root,&renderer_a,[&](void* p) {return parents[p];},
        [&](void* p) {return names[p];}); };
    Check(path().status==PathStatus::Valid && path().path=="Mesh_all/lod0/Body_lod0","outer scene parent polluted root-relative identity");
    parents[&mesh_all]=nullptr;
    Check(path().status==PathStatus::OutsideRoot,"unrelated root accepted");
    parents[&mesh_all]=&lod;
    Check(path().status==PathStatus::Cycle,"cyclic parent chain accepted");
    parents[&mesh_all]=&root; names[&lod]="lod0/forged";
    Check(path().status==PathStatus::InvalidName,"slash in Transform name forged a path");
    names[&lod]="lod0";
    Check(CheckedRelativePath(&root,&renderer_a,[&](void* p) {return parents[p];},
        [&](void* p) {return names[p];},2).status==PathStatus::TooDeep,"depth exhaustion silently truncated path");
    for (const auto* nested_path:{"Weapon/Mesh_all/lod0/Body_lod0","Mesh_all/lod0/VFX/Body_lod0",
        "Mesh_all/lod10/Body_lod0","Shadow_Proxy/SP_Mobile/Nested/Body"})
        Check(ClassifyReceiver(nested_path)==Region::Unknown,"nested/unknown region accepted");
    Check(ClassifyReceiver("Shadow_Proxy/SP_Mobile/Anything")==Region::MobileProxy,"mobile proxy region lost");
    Check(ClassifyReceiver("Shadow_Proxy/SP_Desktop/Anything")==Region::DesktopProxy,"desktop proxy region lost");
}
void IdentityChecks() {
    std::vector<Candidate> candidates{Body()}; auto request=BodyRequest();
    Check(Find(candidates,request).status==MatchStatus::Matched,"full _20 Mesh identity did not match unsuffixed Renderer");
    auto unknown=request; unknown.region=Region::Unknown;
    Check(Find(candidates,unknown).status==MatchStatus::InvalidIndex,"unknown region accepted as a matching contract");
    for (const auto* full_name:{"Body_lod0","Body_lod0_2","Body_lod0_21","body_lod0_20"}) {
        candidates[0].pristine.name=full_name;
        Check(Find(candidates,request).status==MatchStatus::Missing,"Mesh identity was normalized");
    }
    candidates={Body()}; candidates[0].pristine.indices=51303;
    Check(Find(candidates,request).status==MatchStatus::Missing,"original index count ignored");
    candidates[0].pristine.indices_known=false;
    Check(Find(candidates,request).status==MatchStatus::Missing,"missing count treated as verified");
    candidates={Body()}; candidates[0].key.character="chr_other";
    Check(Find(candidates,request).status==MatchStatus::Missing,"other character accepted");
    candidates={Body()}; candidates[0].key.resource="chr_test_postmodel";
    Check(Find(candidates,request).status==MatchStatus::Missing,"other resource route accepted");
    for (const auto origin:{DonorOrigin::CompletedWithoutOriginal,DonorOrigin::Unavailable,DonorOrigin::Ambiguous}) {
        candidates={Body()}; candidates[0].pristine.origin=origin;
        Check(Find(candidates,request).status==MatchStatus::Missing,"known custom Mesh reused as pristine by name");
    }
    candidates={Body()}; candidates[0].pristine.origin=DonorOrigin::SavedOriginal;
    Check(Find(candidates,request).status==MatchStatus::Matched,"verified saved Original rejected");
    for (const auto* path:{"Mesh_all/lod1/Body_lod0","Shadow_Proxy/SP_Mobile/Body_lod0","Weapon/Mesh_all/lod0/Body_lod0"}) {
        candidates={Body(),Body(path)}; candidates.back().renderer=&proxy;
        Check(Find(candidates,request).status==MatchStatus::Matched && Find(candidates,request).index==0,
            "wrong LOD/proxy/nested weapon entered body candidates");
    }
    candidates={Body(),Body("Mesh_all/lod0/Second")}; candidates.back().renderer=&renderer_b;
    Check(Find(candidates,request).status==MatchStatus::Ambiguous,"shared Mesh selected first Renderer");
    candidates.back().pristine.mesh=&mesh_b;
    Check(Find(candidates,request).status==MatchStatus::Ambiguous,"distinct same-name Meshes merged");
    request.verified_receiver_path=candidates[0].key.path;
    Check(Find(candidates,request).status==MatchStatus::Matched,"independent exact receiver evidence ignored");
    request.verified_receiver_path={};
    Check(SelectUnique(candidates,request,[](const auto& c) { return c.renderer==&renderer_b; }).index==1,
        "read-only compatibility validation did not precede uniqueness");
    candidates[1].key=candidates[0].key;
    Check(Find(candidates,request).status==MatchStatus::InvalidIndex,"duplicate receiver paths accepted");
    std::vector<void*> used{&renderer_a,&renderer_b};
    Check(DistinctReceivers(used),"distinct receiver mapping rejected");
    used.push_back(&renderer_a);
    Check(!DistinctReceivers(used),"two components allowed to claim one receiver");
}
void ExplicitResourceChecks() {
    Check(ReceiverLodAgrees("Mesh_all/lod1/body",1) && ReceiverLodAgrees("Mesh_all/lod1",1),"matching known receiver LOD rejected");
    Check(!ReceiverLodAgrees("Mesh_all/lod1/body",0) && !ReceiverLodAgrees("Mesh_all/lod1",0),"declared LOD contradicted known receiver path");
    for (const auto* path:{"","Mesh_all/lod10/body","Weapon/Mesh_all/lod1/body","Shadow_Proxy/SP_Mobile/body","Meshes/body"})
        Check(ReceiverLodAgrees(path,0),"LOD inferred from an unknown or nested path");
    auto candidate=Body("Weapon/Meshes/blade");candidate.key.region=Region::Explicit;
    auto request=BodyRequest();request.region=Region::Explicit;request.verified_receiver_path="Weapon/Meshes/blade";
    std::vector<Candidate> candidates{candidate};
    Check(Find(candidates,request).status==MatchStatus::Matched,"explicit non-character path rejected");
    request.verified_receiver_path="Meshes/blade";
    Check(Find(candidates,request).status==MatchStatus::Missing,"explicit path fell back to nested weapon name");
    request.verified_receiver_path="";
    Check(Find(candidates,request).status==MatchStatus::Missing,"empty root path matched a nested receiver");
    candidates[0].key.path="";
    Check(Find(candidates,request).status==MatchStatus::Matched,"explicit root renderer rejected");
    candidates[0].key.resource="other_resource";
    Check(Find(candidates,request).status==MatchStatus::Missing,"explicit root path crossed resources");
    candidates={candidate};request=BodyRequest();
    Check(Find(candidates,request).status==MatchStatus::Missing,"legacy contract acquired explicitly scoped nested geometry");
}
void KnownNameChecks() {
    // Names in the existing Windows reference audit. These verify same-LOD
    // matching only, and do not certify Android lower-LOD Mesh names.
    struct Row { const char* character; const char* actor; };
    constexpr Row rows[]{{"chr_0014_aurora","aurora"},{"chr_0018_dapan","dapan"},
        {"chr_0019_karin","karin"},{"chr_0021_whiten","whiten"},{"chr_0024_deepfin","deepfin"},
        {"chr_0025_ardelia","ardelia"},{"chr_0027_tangtang","tangtang"}};
    for (const auto& row:rows) {
        const std::string renderer="S_actor_"+std::string(row.actor)+"_fur_01_lod0";
        Candidate c=Body("Mesh_all/lod0/"+renderer);
        c.key.character=row.character; c.key.resource=std::string(row.character)+"_uimodel";
        c.pristine.name=renderer+"_20";
        Request request{c.key.character,c.key.resource,c.pristine.name,Region::Lod0,51300,true,{}};
        Check(Find(std::span<const Candidate>(&c,1),request).status==MatchStatus::Matched,"known fur _20 same-LOD binding failed");
        if (std::string_view(row.actor)=="ardelia") continue; // no _8 record in that snapshot.
        const auto lower=renderer.substr(0,renderer.size()-1)+"1";
        c.key.path="Mesh_all/lod1/"+lower; c.key.region=Region::Lod1; c.pristine.name=lower+"_8";
        request.region=Region::Lod1; request.mesh_name=c.pristine.name;
        Check(Find(std::span<const Candidate>(&c,1),request).status==MatchStatus::Matched,"known fur _8 same-LOD binding failed");
        request.mesh_name=lower;
        Check(Find(std::span<const Candidate>(&c,1),request).status==MatchStatus::Missing,"_8 removed from full source Mesh contract");
    }
}
void LodProxyChecks() {
    LodCounterpart relation;
    Check(AndroidLod1Counterpart("Mesh_all/lod0/S_actor_any_fur_01_lod0",relation) &&
        relation.target_renderer=="S_actor_any_fur_01_lod1" && relation.target_mesh==relation.target_renderer,
        "exact legacy Renderer LOD rule failed");
    Check(!AndroidLod1Counterpart("Mesh_all/lod0/S_actor_any_fur_01_lod0_20",relation),"source Mesh suffix guessed Renderer counterpart");
    Check(!AndroidLod1Counterpart("Weapon/Mesh_all/lod0/Body_lod0",relation),"nested LOD relation accepted");
    ExactLodRelation exact{{"Android","snapshot-test"},"chr_test","chr_test_uimodel","chr_test_postmodel",
        "Mesh_all/lod0/Body_lod0","Body_lod0_20","Mesh_all/lod1/Body_lod1","Body_lod1_8",true,true,8000,true};
    std::vector<ExactLodRelation> rows{exact};
    auto lookup=[&](AssetScope scope) { return FindExactAndroidLod1Relation(rows,scope,"chr_test","chr_test_uimodel",
        "chr_test_postmodel","Mesh_all/lod0/Body_lod0","Body_lod0_20",relation); };
    Check(lookup({"Android","snapshot-test"})==RelationStatus::Matched && relation.target_mesh=="Body_lod1_8" &&
        relation.target_renderer=="Body_lod1" && relation.target_indices==8000,
        "verified lower-LOD relationship merged Renderer/Mesh or UI/world count identities");
    for (const auto scope:{AssetScope{"Android",""},AssetScope{"Windows","snapshot-test"},AssetScope{"Android","new-snapshot"}})
        Check(lookup(scope)==RelationStatus::Missing,"unknown platform/version used a precise relation");
    rows[0].mesh_names_confirmed=false;
    Check(lookup({"Android","snapshot-test"})==RelationStatus::Missing,"reference-only record treated as confirmed Mesh name evidence");
    rows[0]=exact; rows[0].reference_confirmed=false;
    Check(lookup({"Android","snapshot-test"})==RelationStatus::Missing,"names-only record inferred cross-LOD correspondence");
    rows={exact,exact};
    Check(lookup({"Android","snapshot-test"})==RelationStatus::Ambiguous,"duplicate precise relationships selected by order");
    std::vector<void*> visible{&mesh_a,&mesh_b};
    Check(FindProxyOwner(&mesh_a,visible).status==ProxyOwnerStatus::Matched,"live pristine Mesh reference did not identify proxy owner");
    visible.push_back(&mesh_a);
    Check(FindProxyOwner(&mesh_a,visible).status==ProxyOwnerStatus::Ambiguous,"shared visible Mesh owner selected by order");
    Check(FindProxyOwner(&proxy,visible).status==ProxyOwnerStatus::Missing,"dedicated proxy Mesh guessed owner by name");
}
void BoundedAndroidFallbackChecks() {
    Candidate target{{"chr_test","chr_test_postmodel","Mesh_all/lod1/Body_lod1",Region::Lod1},&renderer_a,
        {DonorOrigin::Pristine,&mesh_a,"Body_lod1_8",7,true}};
    FuzzyLodRequest request{"chr_test","chr_test_postmodel","Mesh_all/lod0/Body_lod0","Body_lod0_20"};
    auto validate=[](const auto&) { return true; };
    std::vector<Candidate> candidates{target};
    auto result=SelectBoundedAndroidLod1(candidates,request,validate);
    Check(result.status==MatchStatus::Matched && result.index==0,"bounded Android _8 fallback did not match");
    candidates[0].pristine.origin=DonorOrigin::CompletedWithoutOriginal;
    Check(SelectBoundedAndroidLod1(candidates,request,validate).status==MatchStatus::Missing,
        "bounded fallback reused a completed Mesh as pristine");
    candidates[0]=target; candidates[0].pristine.indices_known=false;
    Check(SelectBoundedAndroidLod1(candidates,request,validate).status==MatchStatus::Missing,
        "bounded fallback accepted unknown index bounds");
    candidates={target,{{"chr_test","chr_test_postmodel","Mesh_all/lod1/Body_lod1_20",Region::Lod1},&renderer_b,
        {DonorOrigin::Pristine,&mesh_b,"Body_lod1_20",8,true}}};
    Check(SelectBoundedAndroidLod1(candidates,request,validate).status==MatchStatus::Ambiguous,
        "bounded fallback selected between _8/_20 candidates");
    candidates[1].key.resource="other_postmodel";
    Check(SelectBoundedAndroidLod1(candidates,request,validate).status==MatchStatus::Matched,
        "bounded fallback let another resource create ambiguity");
    candidates={target}; candidates[0].key.path="Mesh_all/lod1/Body_lod1/Child";
    Check(SelectBoundedAndroidLod1(candidates,request,validate).status==MatchStatus::Missing,
        "bounded fallback crossed the component path boundary");
}
}
int main() {
    try { PathChecks(); IdentityChecks(); ExplicitResourceChecks(); KnownNameChecks(); LodProxyChecks(); BoundedAndroidFallbackChecks();
        std::cout<<"PASS "<<checks<<" generic matching identity, boundaries, lineage, uniqueness and proxy checks\n"; return 0;
    } catch (const std::exception& e) { std::cerr<<e.what()<<'\n'; return 1; }
}
