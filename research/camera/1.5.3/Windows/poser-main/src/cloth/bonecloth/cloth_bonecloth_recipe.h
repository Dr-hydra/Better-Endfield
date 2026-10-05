#pragma once
#include <cstdint>
#include <cmath>
#include "cloth_bonecloth_coat_source.h"
#include "cloth_bonecloth_profile.h"
#include "cloth_bonecloth_short_policy.h"
#include "cloth_bonecloth_strip_width_policy.h"
#include "../core/cloth_skin_guard.h"
#include "../collision/cloth_response_frame.h"
struct ClothBoneLocalMeshConfig {
  const char *renderer,*signature,*bundleName,*asset;
  unsigned resource,bytes;uint64_t hash;
  int sourceBones,skinVertices;const float (*bindings)[16];
  int bindingCount;const int *bindingNativeIndices;
  int sourceVertices=0,candidateVertices=0,candidateIndices=0;
  uint64_t candidateIndexHash=0;
  const char *parent=nullptr;
  const eiem_cloth_skin::Sample *samples=nullptr;size_t sampleCount=0;
  const eiem_cloth_skin::Edge *edges=nullptr;size_t edgeCount=0;
  const eiem_cloth_skin::Seam *seams=nullptr;size_t seamCount=0;
  const int *foreignBindings=nullptr;int foreignCount=0;
  const unsigned char *generatedBytes=nullptr;
  const std::array<uint16_t,4> *generatedWeights=nullptr;
  const std::array<uint8_t,4> *generatedIndices=nullptr;
  size_t generatedVertexCount=0;
  const std::array<float,4> *generatedFloatWeights=nullptr;
};
inline bool ClothGeneratedSkinMatches(const float *weights,const int *indices,const std::array<uint16_t,4> &expected,const std::array<uint8_t,4> &bones) {
  unsigned sum=0;for(auto q:expected)sum+=q;if(!sum||sum>65536)return false;
  bool raw=true,normalized=true;std::array<double,4> actual{};
  for(int k=0;k<4;++k){if(!std::isfinite(weights[k])||weights[k]<0)return false;if(weights[k]==0)continue;int found=-1;
    for(int j=0;j<4;++j)if(expected[j]&&indices[k]==bones[j]){found=j;break;}if(found<0)return false;actual[found]+=weights[k];}
  for(int k=0;k<4;++k)if(expected[k]){if(actual[k]<=0)return false;
    raw&=std::abs(actual[k]-double(expected[k])/65535)<2e-6;normalized&=std::abs(actual[k]-double(expected[k])/sum)<2e-6;}
  return raw||normalized;
}
inline bool ClothGeneratedSkinMatches(const float *weights,const int *indices,const std::array<float,4> &expected,const std::array<uint8_t,4> &bones) {
  double sum=0;for(float w:expected){if(!std::isfinite(w)||w<0)return false;sum+=w;}if(std::abs(sum-1)>.0001)return false;
  bool raw=true,normalized=true;std::array<double,4> actual{};
  for(int k=0;k<4;++k){if(!std::isfinite(weights[k])||weights[k]<0)return false;if(weights[k]==0)continue;int found=-1;
    for(int j=0;j<4;++j)if(expected[j]>0&&indices[k]==bones[j]){found=j;break;}if(found<0)return false;actual[found]+=weights[k];}
  for(int k=0;k<4;++k)if(expected[k]>0){if(actual[k]<=0)return false;raw&=std::abs(actual[k]-expected[k])<2e-6;normalized&=std::abs(actual[k]-expected[k]/sum)<2e-6;}
  return raw||normalized;
}
struct ClothBoneBodySphere {int bone;Vector3 center;float radius;float endRadius=0,length=0;
  Quaternion rotation{0,0,0,1};
  bool Capsule() const {return length>0;}
};
struct ClothBoneResponseFrame {
  eiem_cloth_response::Frame frame{};
  Vector3 center{},size{};
  bool reverse=false,separated=false,centered=false;
};
struct ClothBoneResponsePoint {const char *name=nullptr,*parent=nullptr;int attribute=0;};
struct ClothBoneResponseFace {std::array<int,3> ids{};int outside=0;};
constexpr float ClothLongPanelDistanceStiffness=.3f,ClothLongPanelTetherStretch=1.f;
constexpr int ClothLongPanelColumns=20,ClothLongPanelRows=11;
constexpr int ClothLongPanelParticles=ClothLongPanelColumns*ClothLongPanelRows;
constexpr int ClothLongPanelIdentities=40+ClothLongPanelParticles;
constexpr int ClothLongPanelFaces=2*ClothLongPanelColumns*(ClothLongPanelRows-1);
static_assert(ClothLongPanelParticles<=ClothBoneMaxParticles&&ClothLongPanelIdentities<=ClothBoneMaxIdentities);
struct ClothBoneLocalRecipe {
  const char *signature=nullptr,*baseSignature=nullptr,*prefabSha=nullptr;
  bool loop=true,bodyCoverage=false,multipleLod=false,rootSkinTransition=false;
  const char *contactProducer=nullptr;
  const int *contactOmissions=nullptr;int contactOmissionCount=0;
  int originalCount=0,originalRoots=0,depth=0,addedCount=0,rootCount=0;
  const ClothBoneAsset *added=nullptr;
  const int *columns=nullptr,*roots=nullptr,*parents=nullptr;
  const float *radii=nullptr,*radiusCurve=nullptr;
  const std::array<int,2> *cross=nullptr;int crossCount=0;
  const ClothBoneLocalMeshConfig *meshes=nullptr;int meshCount=0;
  const ClothBoneNativeGraph *graphs=nullptr;int graphCount=0;
  float distanceStiffness=1.0f,tetherStretch=0,bendingStiffness=-1;
  float rootRotation=-1;
  const float *distanceCurve=nullptr;
  bool runtimeGenerated=false;
  bool ribbonSurface=false;
  int separatedPanels=0;
  bool separatedWidth=false;
  bool partialSurface=false;
  size_t preservedVertices=0;
  bool sourcePanelFit=false;
  bool resampledPanel=false;
  const ClothBoneRendererAsset *bodyAsset=nullptr;
  const ClothBoneBodySphere *bodySpheres=nullptr;int bodySphereCount=0;
  bool sourceApronFit=false;
  bool sourceCoatWaist=false;
  bool sourceBodyOnly=false;
  bool sourceShortSkin=false;
  bool sourceShortSides=false;
  bool sourceCoatCalves=false;
  bool sourceCoatTorso=false;
  int nativeLayer=0;
  const char *layerPeer=nullptr;
  const ClothBoneResponseFace *layerFaces=nullptr;int layerFaceCount=0;
  float layerContactScale=0,layerContactRequired=0;
  size_t layerContactSamples=0;bool layerContactCapped=false;
  float layerContactClearanceLimit=0,layerContactClearanceGap=0;
  size_t layerContactClearanceChecks=0,layerContactPreloaded=0;int layerContactClearanceKind=0;
  bool CoatWaistSkinOnly() const {return sourceCoatWaist&&runtimeGenerated&&!loop&&multipleLod&&
      originalCount==24&&originalRoots==6&&depth==4&&addedCount==0&&rootCount==6&&
      meshCount>0&&meshCount<=16&&crossCount==0&&graphs&&graphCount==2&&
      !bodyCoverage&&!bodyAsset&&!bodySphereCount&&!nativeLayer&&!contactProducer&&
      !sourcePanelFit&&!sourceApronFit&&!sourceShortSkin&&!sourceShortSides&&!resampledPanel&&
      !ribbonSurface&&!separatedPanels&&!separatedWidth&&!partialSurface&&!rootSkinTransition&&
      tetherStretch==0&&bendingStiffness<0&&rootRotation<0&&radiusCurve&&distanceCurve;}
  bool FittedSkin() const {return sourcePanelFit||sourceApronFit;}
  bool NativeBodyOnly() const {return sourceBodyOnly&&runtimeGenerated&&!loop&&multipleLod&&
      originalCount==44&&originalRoots==10&&rootCount==10&&depth==4&&!addedCount&&!meshCount&&!crossCount&&
      graphCount==1&&graphs&&graphs[0].faceCount==0&&graphs[0].lineCount==25&&
      bodyCoverage&&bodyAsset&&bodySpheres&&bodySphereCount==12&&radiusCurve&&distanceCurve&&
      !nativeLayer&&!contactProducer&&!FittedSkin()&&!sourceShortSkin&&!sourceShortSides&&!sourceCoatWaist&&
      !resampledPanel&&!ribbonSurface&&!separatedPanels&&!separatedWidth&&!partialSurface&&!rootSkinTransition&&
      tetherStretch==0&&bendingStiffness<0&&rootRotation<0;}
  bool RetainsSourceReference() const {return CoatWaistSkinOnly()||NativeBodyOnly()||ForkCoatBodyOnly();}
  bool RetainsBindings() const {return CoatWaistSkinOnly();}
  bool CoatCalfCoverage() const {
    if(!sourceCoatCalves||!runtimeGenerated||nativeLayer||meshCount||!bodyCoverage||!bodyAsset||!bodyAsset->bones||
        bodySphereCount!=(sourceCoatTorso?5:2)||!bodySpheres||!eiem_cloth_asset::SourceOriginalCoverageCoat(prefabSha,"MC_Endminm_Coat"))return false;
    const char *names[]{"Bip001_L_Calf","Bip001_R_Calf"},*parents[]{"Bip001_L_Thigh","Bip001_R_Thigh"};
    for(int n=0;n<2;++n){const auto &c=bodySpheres[n];if(c.bone<0||c.bone>=bodyAsset->boneCount)return false;
      const auto &b=bodyAsset->bones[c.bone];
      const auto &q=c.rotation;const double norm=double(q.x)*q.x+double(q.y)*q.y+double(q.z)*q.z+double(q.w)*q.w;
      if(!b.name||!b.parent||strcmp(b.name,names[n])||strcmp(b.parent,parents[n])||!c.Capsule()||
          !std::isfinite(c.center.x)||!std::isfinite(c.center.y)||!std::isfinite(c.center.z)||!std::isfinite(norm)||std::abs(norm-1)>.001||
          !std::isfinite(c.radius)||!std::isfinite(c.endRadius)||!std::isfinite(c.length)||
          c.radius<=0||c.radius>.25f||c.endRadius<=0||c.endRadius>=c.radius||c.length<=c.radius+c.endRadius||c.length>1)return false;
    }
    return true;
  }
  bool CoatTorsoCoverage() const {
    const bool male=eiem_cloth_asset::SourceOriginalCoverageCoat(prefabSha,"MC_Endminm_Coat");
    if(!sourceCoatTorso||!runtimeGenerated||nativeLayer||meshCount||!bodyCoverage||!bodyAsset||!bodyAsset->bones||!bodySpheres||
        (male?!CoatCalfCoverage():(!eiem_cloth_asset::SourceForkCoatBody(prefabSha,"MC_Coat")||sourceCoatCalves))||
        bodySphereCount!=(male?5:2))return false;
    const char *names[]{"Bip001_Pelvis","Bip001_Spine1","Bip001_Spine"},*parents[]{"Bip001","Bip001_Spine","Bip001_Pelvis"};
    for(int n=male?0:1;n<3;++n){const auto &c=bodySpheres[(male?2:-1)+n];if(c.bone<0||c.bone>=bodyAsset->boneCount)return false;
      const auto &b=bodyAsset->bones[c.bone];const auto &q=c.rotation;
      const double norm=double(q.x)*q.x+double(q.y)*q.y+double(q.z)*q.z+double(q.w)*q.w;
      if(!b.name||!b.parent||strcmp(b.name,names[n])||strcmp(b.parent,parents[n])||!c.Capsule()||
          !std::isfinite(c.center.x)||!std::isfinite(c.center.y)||!std::isfinite(c.center.z)||!std::isfinite(norm)||std::abs(norm-1)>.001||
          !std::isfinite(c.radius)||!std::isfinite(c.endRadius)||!std::isfinite(c.length)||c.radius<.06f||c.radius>.16f||
          c.endRadius!=c.radius||c.length<=2*c.radius||c.length>.5f)return false;
    }
    return true;
  }
  bool ForkCoatBodyOnly() const {return CoatTorsoCoverage()&&!sourceCoatCalves&&!loop&&multipleLod&&
      originalCount==70&&originalRoots==5&&rootCount==5&&depth==6&&!addedCount&&!crossCount&&graphs&&graphCount>0&&
      !separatedPanels&&!separatedWidth&&!FittedSkin()&&!sourceShortSkin&&!sourceShortSides&&!sourceCoatWaist&&!sourceBodyOnly&&
      !resampledPanel&&!ribbonSurface&&!partialSurface&&!rootSkinTransition&&!contactProducer&&radiusCurve&&distanceCurve&&
      tetherStretch==0&&bendingStiffness<0&&rootRotation<0;}
  bool NativePanelsOnly() const {return runtimeGenerated&&separatedPanels>=2&&separatedPanels<=ClothBoneMaxSeparatedPanels&&!loop&&
      !separatedWidth&&meshCount==0&&(!bodyCoverage||(nativeLayer==2&&bodyAsset&&bodySphereCount==2)||(CoatCalfCoverage()&&(!sourceCoatTorso||CoatTorsoCoverage())))&&!FittedSkin()&&!sourceShortSkin&&!resampledPanel&&!ribbonSurface;}
  bool NativeRibbonWidth() const {return runtimeGenerated&&separatedWidth&&separatedPanels==3&&!loop&&
      originalCount==45&&originalRoots==7&&depth==6&&addedCount==74&&rootCount==23&&meshCount>0&&meshCount<=8&&
      !bodyCoverage&&!nativeLayer&&!FittedSkin()&&!sourceShortSkin&&!resampledPanel&&!ribbonSurface;}
  bool NativeSkinRetained() const {return NativeBodyOnly()||ForkCoatBodyOnly()||NativePanelsOnly()||(sourceShortSkin&&runtimeGenerated&&(!loop||sourceShortSides)&&
      separatedPanels>=0&&separatedPanels<=3&&meshCount==0&&!bodyCoverage&&!FittedSkin()&&!resampledPanel&&!ribbonSurface);}
  bool SourcePoseLease() const {return FittedSkin()||sourceShortSkin;}
  size_t fixedSkinVertices=0;
  size_t upperAnchorSkinVertices=0;
  float fittedContactRadius=0,contactRadiusScale=0;
  const char *responseConsumer=nullptr,*responseRoot=nullptr;
  const ClothBoneResponseFrame *responses=nullptr;int responseCount=0;
  const ClothBoneResponsePoint *responsePoints=nullptr;int responsePointCount=0;
  const ClothBoneResponseFace *responseFaces=nullptr;int responseFaceCount=0;
  int Total() const {return originalCount+addedCount;}
};
inline bool ClothGeneratedLayerPair(const ClothBoneProfile &outer,const ClothBoneProfile &inner) {
  const auto *a=outer.generatedLocal,*b=inner.generatedLocal;
  return eiem_cloth_asset::SourceLongLegPanels(outer)&&eiem_cloth_asset::SourceShortContract(inner)&&a&&b&&
      a->NativePanelsOnly()&&b->NativeSkinRetained()&&a->nativeLayer==2&&b->nativeLayer==1&&
      a->layerPeer&&b->layerPeer&&!strcmp(a->layerPeer,inner.component)&&!strcmp(b->layerPeer,outer.component)&&
      b->contactProducer&&!strcmp(b->contactProducer,outer.component)&&!a->contactProducer&&
      a->layerFaces&&b->layerFaces&&a->layerFaceCount>0&&a->layerFaceCount<=512&&b->layerFaceCount>0&&b->layerFaceCount<=512&&
      a->Total()==42&&b->Total()==(eiem_cloth_asset::SourceShortSides(inner)?36:20)&&b->sourceShortSides==eiem_cloth_asset::SourceShortSides(inner);
}
inline bool ClothGeneratedContactEnvelope(const ClothBoneProfile &outer,const ClothBoneProfile &inner) {
  if(!ClothGeneratedLayerPair(outer,inner)||!eiem_cloth_asset::SourceShortSides(inner))return false;
  const auto &r=*inner.generatedLocal;
  if(!r.radiusCurve||!std::isfinite(r.layerContactScale)||r.layerContactScale<=0||r.layerContactScale>1||
      !std::isfinite(r.layerContactRequired)||r.layerContactRequired<=0||r.layerContactRequired>.15f||
      r.layerContactSamples<100||r.layerContactSamples>100000||
      !std::isfinite(r.layerContactClearanceLimit)||r.layerContactClearanceLimit<=0||r.layerContactClearanceLimit>1||
      r.layerContactScale>r.layerContactClearanceLimit||!std::isfinite(r.layerContactClearanceGap)||r.layerContactClearanceGap<0||
      !r.layerContactClearanceChecks||r.layerContactClearanceChecks>1000000||r.layerContactPreloaded>r.layerContactClearanceChecks||
      r.layerContactClearanceKind<0||r.layerContactClearanceKind>3)return false;
  for(int n=0;n<16;++n)if(!std::isfinite(r.radiusCurve[n])||r.radiusCurve[n]<=0||r.radiusCurve[n]>.1f||
      r.radiusCurve[n]*r.layerContactScale<.005f-1e-6f)return false;
  return true;
}
