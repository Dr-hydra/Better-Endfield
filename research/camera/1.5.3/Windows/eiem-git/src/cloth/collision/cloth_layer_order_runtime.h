#pragma once
static bool ClothBoneLayerRequested(const ClothBoneRuntime &s) {
  return (s.local.recipe&&s.local.recipe->nativeLayer==1) ||
      ClothBoneResponseContact(s) || (s.contactPartner>=0 && s.local.recipe && s.local.recipe->signature &&
      !strcmp(s.local.recipe->signature,ClothLayerInnerSignature) &&
      !strcmp(ClothPartnerSurfaceSignature,ClothLayerOuterSignature));
}
static bool ClothBoneLayerPreflight() {
  if(!ClothBoneLayerRequested(ClothBoneState()))return true;
  if(s_clothLayerInstaller && s_clothLayerInstaller())return true;
  return ClothBoneContactPreflightReject(s_clothLayerInstallIssue[0]?s_clothLayerInstallIssue:
      "native-layer-order-adapter-unavailable");
}
static eiem_cloth_layer::Ticket ClothBoneLayerTicket(const ClothBoneRuntime &s,const ClothBoneRuntime &p) {
  return {s.owner.session,s.owner.generation,s.command,
      {uint64_t(uintptr_t(CollisionGc(s.process[1]))),uint64_t(uintptr_t(CollisionGc(p.process[1])))},
      {uint64_t(uintptr_t(CollisionGc(s.candidateData))),uint64_t(uintptr_t(CollisionGc(p.candidateData)))}};
}
template<class ReferenceFace> static bool ClothBoneLayerSurface(const ClothBoneRuntime &s,const ClothInputChunk &chunk,
    const ReferenceFace *reference,size_t count,eiem_cloth_layer::Surface &out) {
  using namespace eiem_cloth_layer;
  const auto &mapping=s.registeredVertices;
  if(!reference||!count||count>ClothBoneMaxFaceChoices||chunk.count<1 || chunk.count>ClothBoneMaxParticles || chunk.start<0 || chunk.start>INT32_MAX-chunk.count ||
      size_t(chunk.count)!=mapping.size() || s.registeredFaces.empty() || s.registeredFaces.size()>out.faces.size())return false;
  Surface result{};result.team=s.team[1];result.start=chunk.start;result.count=chunk.count;
  result.faceCount=int(s.registeredFaces.size());
  std::vector<int> unique=mapping;std::sort(unique.begin(),unique.end());
  if(unique.front()<0 || std::adjacent_find(unique.begin(),unique.end())!=unique.end())return false;
  for(size_t n=0;n<s.registeredFaces.size();++n) {
    auto asset=s.registeredFaces[n];std::sort(asset.begin(),asset.end());
    const auto ref=std::lower_bound(reference,reference+count,asset,[](const ReferenceFace &f,const std::array<int,3>&ids){return f.ids<ids;});
    if(ref==reference+count || ref->ids!=asset)return false;
    auto &f=result.faces[n];
    for(int k=0;k<3;++k) {
      const auto found=std::find(mapping.begin(),mapping.end(),asset[k]);
      if(found==mapping.end())return false;f.ids[k]=chunk.start+int(found-mapping.begin());
    }
    f.outside=ref->outside*Parity(f.ids);std::sort(f.ids.begin(),f.ids.end());
  }
  std::sort(result.faces.begin(),result.faces.begin()+result.faceCount,[](const Face &a,const Face &b){return a.ids<b.ids;});
  if(!Valid(result))return false;out=result;return true;
}
static bool ClothBoneLayerPrepare(ClothBoneRuntime &s) {
  if(!ClothBoneLayerRequested(s))return true;
  if(ClothBoneResponseContact(s))return ClothBoneResponseLayerPrepare(s);
  if(!ClothOnMainThread() || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 ||
      !s_clothLayerInstalled.load(std::memory_order_acquire))return false;
  s_clothLayerGate.Clear();
  eiem_cloth_rebuild::Identity identity[2]{};uint64_t flags[2]{};
  if(!ClothBoneContactPairIdentity(s,identity,flags))return false;
  auto &p=s_clothBoneSlots[s.contactPartner];
  const bool generated=ClothBoneGeneratedPair(p,s);
  if(!generated&&(!ClothBoneSurfacePartner(p) || strcmp(ClothBoneCandidate(p).signature,ClothLayerOuterSignature)))return false;
  eiem_cloth_layer::Policy policy{};policy.ticket=ClothBoneLayerTicket(s,p);
  policy.list=s.local.contactListHeaders[1];
  ClothBoneRuntime *pair[]{&s,&p};ClothInputChunk chunks[2]{};
  for(int n=0;n<2;++n) {
    void *box=nullptr;
    if(!ClothBoneContactTeam(pair[n]->team[1],CollisionGc(pair[n]->process[1]),box) ||
        !ClothInputTeamField(box,"particleChunk","BeyondDynamicBone.DataChunk",chunks[n]))return false;
  }
  void *manager=CollisionGc(s.local.contactManager);eiem_cloth_contact_job::Container current{};
  if(!manager || !ClothField(manager,"pointTriangleContactList",
      "Unity.Collections.NativeList<BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact>",current) || current.data!=policy.list)return false;
  if(generated) {
    if(!ClothBoneLayerSurface(s,chunks[0],s.local.recipe->layerFaces,size_t(s.local.recipe->layerFaceCount),policy.inner)||
        !ClothBoneLayerSurface(p,chunks[1],p.local.recipe->layerFaces,size_t(p.local.recipe->layerFaceCount),policy.outer))return false;
  } else if(!ClothBoneLayerSurface(s,chunks[0],ClothLayerInnerFaces,std::size(ClothLayerInnerFaces),policy.inner)||
      !ClothBoneLayerSurface(p,chunks[1],ClothLayerOuterFaces,std::size(ClothLayerOuterFaces),policy.outer))return false;
  eiem_cloth_rebuild::Identity after[2]{};uint64_t afterFlags[2]{};
  if(!ClothBoneContactPairIdentity(s,after,afterFlags) || !(identity[0]==after[0]) || !(identity[1]==after[1]) ||
      !(policy.ticket==ClothBoneLayerTicket(s,p)))return false;
  eiem_cloth_layer::Policy before{};eiem_cloth_layer::Counts counts{};
  s_clothLayerGate.Snapshot(before,counts);
  if(!s_clothLayerGate.Publish(policy))return false;
  if(!(before.ticket==policy.ticket))
    Log("[CLOTH-LAYER-ORDER] stage=armed generation=%llu command=%u innerTeam=%d outerTeam=%d faces=%d/%d rule=inner-inside-outer solverPointTriangleSign=asset-oriented sourceContactUnchanged=1 edgeContactsUnchanged=1 bodyColliderInflation=0 visualVerified=0",
        s.owner.generation,s.command,s.team[1],p.team[1],policy.inner.faceCount,policy.outer.faceCount);
  return true;
}
static bool (*s_clothLayerPrepare)(ClothBoneRuntime &)=ClothBoneLayerPrepare;
static bool ClothBoneLayerStart(ClothBoneRuntime &s) { return s_clothLayerPrepare && s_clothLayerPrepare(s); }
static void ClothBoneLayerRefresh() {
  s_clothLayerGate.Clear();
  for(int n=0;n<s_clothBoneCount;++n) {
    auto &s=s_clothBoneSlots[n];
    if(!ClothBoneLayerRequested(s) || !s.local.contactConfirmed || !s.pending || !s.lease ||
        s.stopRequested || s.failed || s.tx.cancelled || s.tx.phase!=eiem_cloth_rebuild::Phase::Active)continue;
    if(!s_clothLayerPrepare(s)) {
      if(!s.failure[0])strncpy_s(s.failure,"layer-order-owner-or-native-mapping-changed-restoring",_TRUNCATE);
      s.failed=s.stopRequested=true;
      Log("[CLOTH-LAYER-ORDER] stage=revoked generation=%llu reason=owned-pair-or-mapping-unconfirmed detail=%s",s.owner.generation,s.local.readbackIssue?s.local.readbackIssue:"no-detail");
    }
    break;
  }
  static uint64_t next=0;const auto now=GetTickCount64();
  if(now<next)return;next=now+2000;
  eiem_cloth_layer::Policy policy{};eiem_cloth_layer::Counts counts{};
  if(s_clothLayerGate.Snapshot(policy,counts))
    Log("[CLOTH-LAYER-ORDER] stage=solver-inputs frame=%d generation=%llu command=%llu kept=%llu corrected=%llu enabled=%llu enabledCorrected=%llu unknownFace=%llu invalid=%llu bufferMismatch=%llu paths=%llu,%llu,%llu,%llu,%llu,%llu scope=original-solver-local-input sourceListUnchanged=1 callsAreNotUniqueContacts=1 renderedLayerOrderUnconfirmed=1",
        ClothFrame(),policy.ticket.generation,policy.ticket.command,counts.kept,counts.changed,counts.enabled,counts.enabledChanged,counts.unknown,counts.invalid,counts.bufferMismatch,
        counts.paths[0],counts.paths[1],counts.paths[2],counts.paths[3],counts.paths[4],counts.paths[5]);
}
