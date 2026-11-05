// Compatibility repair layer contracts. Header-only: no engine, driver or game.
//
// The cases here are the ones that decide whether the layer is safe: a recorded
// rename must stay an exact match, a record that cannot prove itself must do
// nothing, a satisfied contract must never enter the registry, and a hand
// written repair must still answer through the same checked door.
#include "../modules/custom_model/compat_registry.h"
#include "../modules/custom_model/compat_records.inc"

#include <iostream>
#include <stdexcept>
#include <string>
#include <string_view>
#include <vector>

using namespace BetterEndfieldNext::CustomModel;
using namespace BetterEndfieldNext::CustomModel::GenericMatching;

namespace {
size_t checks=0;
void Check(bool value,const char* message) { ++checks; if (!value) throw std::runtime_error(message); }

int renderer_slot,renderer_owner,renderer_free,renderer_alternate,mesh_shared,mesh_other;

// A test-only hand written repair, standing in for the ones a module registers.
// It answers with a candidate instead of a rewritten request, which is the path
// no data record can take.
RepairResult PinAlternate(RepairContext& ctx,const void*) {
    if (ctx.request.mesh_name!="Pinned_lod0_20") return {};
    for (size_t i=0;i<ctx.candidates.size();++i) {
        if (ctx.candidates[i].key.path!="Mesh_all/lod0/Pinned_lod0") continue;
        RepairResult result;
        result.kind=RepairResult::Kind::PickCandidate;
        result.candidate_index=i;
        result.id="test-pin";
        result.note="picked by test repair";
        return result;
    }
    return {};
}
}

namespace BetterEndfieldNext::CustomModel::GenericMatching {
inline constexpr FunctionRepair kCompatFunctionRepairs[]={
    {"chr_pin_on",nullptr,-1,&PinAlternate,nullptr,/*also_when_unchecked=*/true},
    {"chr_pin_off",nullptr,-1,&PinAlternate,nullptr,/*also_when_unchecked=*/false},
};
std::span<const FunctionRepair> CompatFunctionRepairs() { return kCompatFunctionRepairs; }
}

namespace {
CharacterAdapter Adapter(const char* id,bool explicit_resource) {
    CharacterAdapter adapter{};
    adapter.id=id;
    adapter.world_resource="chr_0036_jsspsi_postmodel";
    adapter.ui_resource="chr_0036_jsspsi_uimodel";
    adapter.explicit_resource=explicit_resource;
    return adapter;
}
Candidate Slot(const char* character,const char* path,Region region,void* renderer,void* mesh,
    const char* mesh_name,uint64_t indices,bool indices_known=true) {
    Candidate candidate;
    candidate.key.character=character;
    candidate.key.resource="chr_0036_jsspsi_postmodel";
    candidate.key.path=path;
    candidate.key.region=region;
    candidate.renderer=renderer;
    candidate.pristine={DonorOrigin::Pristine,mesh,mesh_name,indices,indices_known,""};
    return candidate;
}
Request Ask(const char* mesh_name,uint64_t indices,Region region,const char* receiver_path,bool check_indices=true,
    const char* character="chr_0036_jsspsi") {
    return {character,"chr_0036_jsspsi_postmodel",mesh_name,region,indices,check_indices,receiver_path};
}
bool Accept(const Candidate&) { return true; }

struct Run {
    std::vector<std::string> reported;
    const char* applied_id=nullptr;
    Match match;
};
Run Repair(const CharacterAdapter& adapter,std::span<const Candidate> candidates,Request& request,
    uint32_t component=0) {
    Run run;
    RepairContext context{adapter,nullptr,candidates,component,request,{}};
    run.match=SelectRepaired(context,Accept,[&](const char* id,const char* /*note*/) {
        run.reported.push_back(id?id:"");
    },&run.applied_id);
    return run;
}

// The shared Mesh a 1.6.5 client keeps: the vfxpart slot and the cloth slot hold
// one and the same Mesh object, so only the declared receiver path tells them
// apart.
std::vector<Candidate> SharedCandidates(Region region,uint64_t indices=4680,bool indices_known=true) {
    return {
        Slot("chr_0036_jsspsi","Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0",region,
            &renderer_slot,&mesh_shared,"S_actor_jsspsi_cloth_06_lod0",indices,indices_known),
        Slot("chr_0036_jsspsi","Mesh_all/lod0/S_actor_jsspsi_cloth_06_lod0",region,
            &renderer_owner,&mesh_shared,"S_actor_jsspsi_cloth_06_lod0",indices,indices_known),
    };
}

void RecordTableChecks() {
    Check(!CompatDataRepairRecords().empty(),"data repair table is empty");
    for (const auto& record:CompatDataRepairRecords()) {
        Check(record.id && record.id[0],"data record has no id");
        Check(record.character && record.character[0],"data record has no character scope");
        Check(record.expected_mesh && record.expected_mesh[0] && record.repaired_mesh &&
            record.repaired_mesh[0],"data record does not name both sides");
        Check(std::string_view(record.expected_mesh)!=record.repaired_mesh,
            "data record claims a rename without changing the name");
        Check(record.observed_at && record.observed_at[0],"data record does not say when it was observed");
    }
    for (const auto& repair:CompatFunctionRepairs())
        Check(repair.fn,"function repair entry has no function");
}

// Resource-scoped (BEM 1.4) packages declare the receiver path, which is the
// only evidence that separates two slots sharing one Mesh.
void DeclaredSlotChecks() {
    const auto adapter=Adapter("chr_0036_jsspsi",true);
    auto candidates=SharedCandidates(Region::Explicit);

    Request owner=Ask("S_actor_jsspsi_cloth_06_lod0",4680,Region::Explicit,
        "Mesh_all/lod0/S_actor_jsspsi_cloth_06_lod0");
    auto owner_run=Repair(adapter,candidates,owner,1u);
    Check(owner_run.match.status==MatchStatus::Matched && owner_run.match.index==1,
        "untouched shared-Mesh owner slot no longer matches");
    Check(owner_run.reported.empty() && !owner_run.applied_id,
        "a satisfied contract entered the repair registry");

    Request slot=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,
        "Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0");
    auto slot_run=Repair(adapter,candidates,slot,0u);
    Check(slot_run.match.status==MatchStatus::Matched && slot_run.match.index==0,
        "recorded identity repair did not bind the declared slot");
    Check(slot.mesh_name=="S_actor_jsspsi_cloth_06_lod0",
        "repair did not rewrite the expected Mesh identity");
    Check(slot.verified_receiver_path=="Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0",
        "repair did not keep the declared receiver path");
    Check(slot_run.reported.size()==1 && slot_run.reported[0]=="jsspsi-vfxpart01-cloth06-world",
        "applied repair did not report which record ran");
    Check(slot_run.applied_id && std::string_view(slot_run.applied_id)=="jsspsi-vfxpart01-cloth06-world",
        "caller was not told which record produced the match");

    // A second slot with the same declared geometry takes its own record.
    auto candidates_v4=std::vector<Candidate>{
        Slot("chr_0036_jsspsi","Mesh_all/lod0/S_actor_jsspsi_vfxpart_04_lod0",Region::Explicit,
            &renderer_free,&mesh_other,"S_actor_jsspsi_cloth_08_lod0",1752),
        Slot("chr_0036_jsspsi","Mesh_all/lod0/S_actor_jsspsi_cloth_08_lod0",Region::Explicit,
            &renderer_owner,&mesh_other,"S_actor_jsspsi_cloth_08_lod0",1752),
    };
    Request slot4=Ask("S_actor_jsspsi_vfxpart_04_lod0",1752,Region::Explicit,
        "Mesh_all/lod0/S_actor_jsspsi_vfxpart_04_lod0");
    auto slot4_run=Repair(adapter,candidates_v4,slot4,0u);
    Check(slot4_run.match.status==MatchStatus::Matched && slot4_run.match.index==0,
        "second recorded rename did not bind its slot");
    Check(slot4_run.reported.size()==1 && slot4_run.reported[0]=="jsspsi-vfxpart04-cloth08-world",
        "second recorded rename reported the wrong record");
}

// Every rejection below has to leave the ordinary failure untouched.
void ProofRefusalChecks() {
    const auto adapter=Adapter("chr_0036_jsspsi",true);
    const char* slot_path="Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0";

    {
        auto candidates=SharedCandidates(Region::Explicit,9999);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,slot_path);
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.reported.empty(),
            "proof accepted a repaired Mesh with a different index count");
    }
    {
        auto candidates=SharedCandidates(Region::Explicit,4680,false);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,slot_path);
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.reported.empty(),
            "proof accepted an unmeasured index count");
    }
    {
        auto candidates=SharedCandidates(Region::Explicit);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,
            "Mesh_all/lod0/S_actor_jsspsi_cloth_06_lod0");
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.reported.empty(),
            "proof repaired a request that names another receiver");
    }
    {
        auto candidates=SharedCandidates(Region::Explicit);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",1752,Region::Explicit,slot_path);
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.reported.empty(),
            "record fired for an index count it was not observed with");
    }
    {
        // Expected identity that no record declares: the layer must stay out.
        std::vector<Candidate> candidates{
            Slot("chr_0036_jsspsi","Mesh_all/lod0/S_actor_jsspsi_body_01_lod0",Region::Explicit,
                &renderer_slot,&mesh_other,"S_actor_jsspsi_body_01_lod1",27468)};
        Request request=Ask("S_actor_jsspsi_body_01_lod0",27468,Region::Explicit,
            "Mesh_all/lod0/S_actor_jsspsi_body_01_lod0");
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.reported.empty(),
            "unrecorded rename was repaired by guesswork");
        Check(request.mesh_name=="S_actor_jsspsi_body_01_lod0",
            "failed repair left the request rewritten");
    }
    {
        // Records are scoped by character; another character must not benefit.
        auto candidates=SharedCandidates(Region::Explicit);
        const auto other=Adapter("chr_0002_endminm",true);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,slot_path);
        auto run=Repair(other,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.reported.empty(),
            "record crossed the character scope");
    }
}

// BEM 1.3 packages carry no receiver path, so two slots sharing one Mesh stay
// ambiguous. The rename still repairs, because the proof pins the slot it
// proved; the untouched owner slot cannot be disambiguated and is left as is.
void LegacyResourceChecks() {
    const auto adapter=Adapter("chr_0036_jsspsi",false);
    auto candidates=SharedCandidates(Region::Lod0);

    Request owner=Ask("S_actor_jsspsi_cloth_06_lod0",4680,Region::Lod0,"");
    auto owner_run=Repair(adapter,candidates,owner,1u);
    Check(owner_run.match.status==MatchStatus::Ambiguous,"shared Mesh without a declared path was not ambiguous");
    Check(owner_run.reported.empty(),"an ambiguous match was repaired");

    Request slot=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Lod0,"");
    auto slot_run=Repair(adapter,candidates,slot,0u);
    Check(slot_run.match.status==MatchStatus::Matched && slot_run.match.index==0,
        "recorded rename did not repair a path-less package");
    Check(slot.verified_receiver_path=="Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0",
        "repair did not pin the slot it proved");
}

// A hand written repair is consulted only on its own terms: never on a checked
// satisfied match, only on an unchecked one when it asked for that, and its pick
// still has to pass the caller's validation.
void HandWrittenRepairChecks() {
    auto candidates=std::vector<Candidate>{
        Slot("chr_pin_on","Mesh_all/lod0/Other_lod0",Region::Lod0,
            &renderer_owner,&mesh_other,"Pinned_lod0_20",100),
        Slot("chr_pin_on","Mesh_all/lod0/Pinned_lod0",Region::Lod0,
            &renderer_alternate,&mesh_other,"Pinned_actual_lod0",200),
    };
    const auto on=Adapter("chr_pin_on",false);

    {
        // Checked: the ordinary match is the final answer, the repair is not run.
        Request request=Ask("Pinned_lod0_20",100,Region::Lod0,"",/*check_indices=*/true,"chr_pin_on");
        auto run=Repair(on,candidates,request);
        Check(run.match.status==MatchStatus::Matched && run.match.index==0,
            "checked match did not keep the ordinary answer");
        Check(!run.applied_id,"a checked satisfied match was re-opened by a repair");
    }
    {
        // Unchecked: the repair asked for this case and may replace the answer.
        Request request=Ask("Pinned_lod0_20",100,Region::Lod0,"",/*check_indices=*/false,"chr_pin_on");
        auto run=Repair(on,candidates,request);
        Check(run.match.status==MatchStatus::Matched && run.match.index==1,
            "repair did not replace an unchecked satisfied match");
        Check(run.applied_id && std::string_view(run.applied_id)=="test-pin",
            "picked candidate was not attributed to its repair");
        Check(request.mesh_name=="Pinned_lod0_20" && request.verified_receiver_path.empty(),
            "picked candidate left the request rewritten");
    }
    {
        // Same repair, but the caller's validation refuses the candidate.
        Request request=Ask("Pinned_lod0_20",100,Region::Lod0,"",/*check_indices=*/false,"chr_pin_on");
        RepairContext context{on,nullptr,candidates,0u,request,{}};
        auto reject=[](const Candidate& c) { return c.renderer!=&renderer_alternate; };
        const char* applied=nullptr;
        const auto match=SelectRepaired(context,reject,[](const char*,const char*) {},&applied);
        Check(match.status==MatchStatus::Matched && match.index==0,
            "picked candidate bypassed the caller's validation");
        Check(!applied,"a refused pick was still reported as applied");
    }
    {
        // A repair that did not declare itself for unchecked matches stays out.
        candidates[0].key.character="chr_pin_off";
        candidates[1].key.character="chr_pin_off";
        const auto off=Adapter("chr_pin_off",false);
        Request request=Ask("Pinned_lod0_20",100,Region::Lod0,"",/*check_indices=*/false,"chr_pin_off");
        auto run=Repair(off,candidates,request);
        Check(run.match.status==MatchStatus::Matched && run.match.index==0,
            "a repair that did not ask for unchecked matches replaced one");
        Check(!run.applied_id,"a non-declaring repair was consulted on a satisfied match");
    }
}
}
int main() {
    try {
        RecordTableChecks();
        DeclaredSlotChecks();
        ProofRefusalChecks();
        LegacyResourceChecks();
        HandWrittenRepairChecks();
        std::cout<<"PASS "<<checks<<" compatibility repair scope, proof, refusal, pick and reporting checks\n";
        return 0;
    } catch (const std::exception& error) {
        std::cerr<<error.what()<<'\n';
        return 1;
    }
}
