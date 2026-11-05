// Compatibility repair layer contracts. Header-only: no engine, driver or game.
//
// The cases here are the ones that decide whether the layer is safe: a recorded
// rename must stay an exact match, a record that cannot prove itself must do
// nothing, and a satisfied contract must never enter the registry at all.
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

int renderer_slot,renderer_owner,renderer_free,mesh_shared,mesh_other;

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
Request Ask(const char* mesh_name,uint64_t indices,Region region,const char* receiver_path) {
    return {"chr_0036_jsspsi","chr_0036_jsspsi_postmodel",mesh_name,region,indices,true,receiver_path};
}
bool Accept(const Candidate&) { return true; }

struct Run {
    std::vector<std::string> applied;
    Match match;
};
Run Repair(const CharacterAdapter& adapter,std::span<const Candidate> candidates,Request& request,
    uint32_t component=0) {
    Run run;
    RepairContext context{adapter,nullptr,candidates,component,request,{}};
    run.match=SelectRepaired(context,Accept,[&](const char* id,const char* /*note*/) {
        run.applied.push_back(id?id:"");
    });
    return run;
}

// The shared Mesh that a 1.6.5 client keeps: the vfxpart slot and the cloth slot
// hold one and the same Mesh object, so only the declared receiver path tells
// them apart.
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
    Check(CompatFunctionRepairs().empty(),"function repair table is unexpectedly populated");
    for (const auto& record:CompatDataRepairRecords()) {
        Check(record.id && record.id[0],"data record has no id");
        Check(record.character && record.character[0],"data record has no character scope");
        Check(record.expected_mesh && record.expected_mesh[0] && record.repaired_mesh &&
            record.repaired_mesh[0],"data record does not name both sides");
        Check(std::string_view(record.expected_mesh)!=record.repaired_mesh,
            "data record claims a rename without changing the name");
        Check(record.observed_at && record.observed_at[0],"data record does not say when it was observed");
    }
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
    Check(owner_run.applied.empty(),"a satisfied contract entered the repair registry");

    Request slot=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,
        "Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0");
    auto slot_run=Repair(adapter,candidates,slot,0u);
    Check(slot_run.match.status==MatchStatus::Matched && slot_run.match.index==0,
        "recorded identity repair did not bind the declared slot");
    Check(slot.mesh_name=="S_actor_jsspsi_cloth_06_lod0",
        "repair did not rewrite the expected Mesh identity");
    Check(slot.verified_receiver_path=="Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0",
        "repair did not keep the declared receiver path");
    Check(slot_run.applied.size()==1 && slot_run.applied[0]=="jsspsi-vfxpart01-cloth06-world",
        "applied repair did not report which record ran");

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
    Check(slot4_run.applied.size()==1 && slot4_run.applied[0]=="jsspsi-vfxpart04-cloth08-world",
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
        Check(run.match.status==MatchStatus::Missing && run.applied.empty(),
            "proof accepted a repaired Mesh with a different index count");
    }
    {
        auto candidates=SharedCandidates(Region::Explicit,4680,false);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,slot_path);
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.applied.empty(),
            "proof accepted an unmeasured index count");
    }
    {
        auto candidates=SharedCandidates(Region::Explicit);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Explicit,
            "Mesh_all/lod0/S_actor_jsspsi_cloth_06_lod0");
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.applied.empty(),
            "proof repaired a request that names another receiver");
    }
    {
        auto candidates=SharedCandidates(Region::Explicit);
        Request request=Ask("S_actor_jsspsi_vfxpart_01_lod0",1752,Region::Explicit,slot_path);
        auto run=Repair(adapter,candidates,request);
        Check(run.match.status==MatchStatus::Missing && run.applied.empty(),
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
        Check(run.match.status==MatchStatus::Missing && run.applied.empty(),
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
        Check(run.match.status==MatchStatus::Missing && run.applied.empty(),
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

    Request slot=Ask("S_actor_jsspsi_vfxpart_01_lod0",4680,Region::Lod0,"");
    auto slot_run=Repair(adapter,candidates,slot,0u);
    Check(slot_run.match.status==MatchStatus::Matched && slot_run.match.index==0,
        "recorded rename did not repair a path-less package");
    Check(slot.verified_receiver_path=="Mesh_all/lod0/S_actor_jsspsi_vfxpart_01_lod0",
        "repair did not pin the slot it proved");
}
}
int main() {
    try {
        RecordTableChecks();
        DeclaredSlotChecks();
        ProofRefusalChecks();
        LegacyResourceChecks();
        std::cout<<"PASS "<<checks<<" compatibility repair scope, proof, refusal and reporting checks\n";
        return 0;
    } catch (const std::exception& error) {
        std::cerr<<error.what()<<'\n';
        return 1;
    }
}
