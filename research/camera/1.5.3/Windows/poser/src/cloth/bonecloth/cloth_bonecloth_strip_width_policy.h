#pragma once
#include "cloth_bonecloth_profile.h"
namespace eiem_cloth_asset {
inline bool SourceLegRibbonWidth(const ClothBoneProfile &p) {
  return p.runtimeGenerated&&p.prefabSha&&p.component&&
      !strcmp(p.prefabSha,"95462411bcce0cc31914ad7f6cd0c37909eff18577f51cff319befbe4c349ab6")&&
      !strcmp(p.component,"MC_coat")&&p.boneCount==45&&p.rootCount==7&&p.depth==6&&!p.loop;
}
}
