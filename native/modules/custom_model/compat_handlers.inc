// Hand written compatibility repairs. Included inside the CustomModel namespace
// after the runtime validators they call, so a repair can read whatever its own
// case actually needs instead of what a fixed interface happens to allow.
//
// Everything here is a plain function returning a single answer. Nothing is
// shared with the data records except the entry point that consults both.

namespace GenericMatching {

// Referenced by the caller to turn a repaired match into the binding-time
// palette check and bone remap this donor requires.
inline constexpr const char* kAuroraLegacyRepairId="aurora-legacy-donor";

#if defined(_WIN32)
// Snowshine 1.5.3 -> 1.6.3. A package that still declares the 1.5.3 index count
// is answered with the verified 1.6.3 donor only when that donor reproduces the
// full contract: receiver path, Mesh name, current index count, single material
// and all 87 bone paths with their bind poses. Anything less is refused, and a
// refused repair leaves the ordinary match exactly as it was.
//
// This runs under skip_validation as well, because a name-only match there
// proves nothing about geometry and the binding phase would still remap bones.
GenericMatching::RepairResult RepairAuroraLegacy(GenericMatching::RepairContext& ctx,const void*) {
    using GenericMatching::RepairResult;
    const auto& request=ctx.request;
    if (!AuroraLegacy::IsLegacyTarget(ctx.adapter.id,request.resource,ctx.component_id,
            request.mesh_name,request.indices,ctx.adapter.explicit_resource)) return {};

    size_t hits=0,hit=0;
    for (size_t i=0;i<ctx.candidates.size();++i) {
        const auto& candidate=ctx.candidates[i];
        if (!candidate.renderer || !candidate.pristine.mesh) continue;
        if (candidate.pristine.origin!=GenericMatching::DonorOrigin::Pristine &&
            candidate.pristine.origin!=GenericMatching::DonorOrigin::SavedOriginal) continue;
        if (candidate.key.character!=request.character || candidate.key.resource!=request.resource ||
            candidate.key.region!=request.region) continue;
        if (candidate.key.path!=AuroraLegacy::kReceiverPath) continue;
        if (candidate.pristine.name!=AuroraLegacy::kMeshName) continue;
        if (!candidate.pristine.indices_known ||
            candidate.pristine.indices!=AuroraLegacy::kCurrentIndices) continue;
        if (!request.verified_receiver_path.empty() &&
            request.verified_receiver_path!=candidate.key.path) continue;
        // The contract validator reads the live renderer arrays; this only views
        // them, it does not rebuild or clone anything.
        GenericRendererCandidate entry;
        entry.key=candidate.key; entry.renderer=candidate.renderer;
        entry.mesh=GetRendererMesh(candidate.renderer);
        entry.materials=Invoke(Contract("renderer.get_shared_materials"),candidate.renderer,nullptr);
        entry.bones=GetRendererBones(candidate.renderer);
        if (!ValidateAuroraLegacyCandidate(ctx.adapter,ctx.asset,ctx.component_id,entry,candidate.pristine))
            continue;
        ++hits; hit=i;
    }
    if (hits!=1) return {};

    RepairResult result;
    result.kind=RepairResult::Kind::PickCandidate;
    result.candidate_index=hit;
    result.id=kAuroraLegacyRepairId;
    result.note="legacy 87120->87288, 87-bone palette, moved slots remapped";
    return result;
}

inline constexpr GenericMatching::FunctionRepair kCompatFunctionRepairs[]={
    {"chr_0014_aurora",nullptr,1,&RepairAuroraLegacy,nullptr,
        /*also_when_unchecked=*/true},
};
#elif defined(__ANDROID__)
// Android packs the world prefab one LOD above what a package declares, so an
// explicit-resource component can fail on the LOD digit alone even though the
// renderer sits in the same Mesh_all slot. The candidate index admits those
// sibling paths only here (generic_model_matcher.inc), which is why this is a
// platform registration rather than a data record.
//
// It takes the first sibling candidate that would also pass the caller's
// contract check, exactly as the branch it replaces did. Order is the index
// order, which is the renderer order of the prefab.
GenericMatching::RepairResult RepairAndroidSiblingLod(GenericMatching::RepairContext& ctx,const void*) {
    using GenericMatching::RepairResult;
    if (!ctx.adapter.explicit_resource) return {};
    for (size_t i=0;i<ctx.candidates.size();++i) {
        const auto& candidate=ctx.candidates[i];
        if (!candidate.renderer) continue;
        if (candidate.key.character!=ctx.request.character ||
            candidate.key.resource!=ctx.request.resource) continue;
        if (!GenericMatching::SiblingLodPathMatches(ctx.request.verified_receiver_path,candidate.key.path))
            continue;
        if (!GenericMatching::Accepts(ctx,candidate)) continue;
        RepairResult result;
        result.kind=RepairResult::Kind::PickCandidate;
        result.candidate_index=i;
        result.id="android-sibling-lod";
        result.note="sibling LOD candidate";
        return result;
    }
    return {};
}

inline constexpr GenericMatching::FunctionRepair kCompatFunctionRepairs[]={
    {nullptr,nullptr,-1,&RepairAndroidSiblingLod,nullptr,/*also_when_unchecked=*/false},
};
#else
inline constexpr std::span<const GenericMatching::FunctionRepair> kCompatFunctionRepairs{};
#endif

inline std::span<const GenericMatching::FunctionRepair> CompatFunctionRepairs() {
    return kCompatFunctionRepairs;
}

}
