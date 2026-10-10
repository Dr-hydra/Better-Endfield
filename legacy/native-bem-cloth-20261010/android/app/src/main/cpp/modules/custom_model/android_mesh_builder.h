#pragma once
#include "core/runtime.h"
#include "bem.h"
#include <vector>

namespace betterendfieldnext {
// Private, same-binary platform boundary; does not extend BE_HostApiV1.
void ConfigureAndroidMeshBuilder(Il2CppRuntime& runtime, bool rollback_test = false, bool pipeline_lod = false, bool npc_parameters = false, bool inspect = false);
bool AndroidMeshRollbackTest();
bool AndroidPipelineLodEnabled();
bool AndroidNpcParametersEnabled();
bool AndroidInspectionEnabled();
// Explicit, read-only one-shot mip sampling request in the game's private cache.
std::string AndroidTextureMipRequestToken();
// Named native icall for optional read-only observation; never invoked here.
void* AndroidLodStreamingOffsetEntry();
void* AndroidTextureBudgetSetterEntry();
void* AndroidTextureBudgetGetterEntry();
void* AndroidQualityLevelSetterEntry();
void* AndroidQualityLevelGetterEntry();
bool AndroidReadMemoryHeadroom(uint64_t& total_bytes,uint64_t& available_bytes);
void AndroidAuditNormalTexture(void* texture,const std::string& name);
bool AndroidAuditMaterialCopy(void* original, void* copy);
void AndroidAuditTextureColorSpace(void* original, void* replacement, const std::string& name);
bool AndroidMeshBuilderReady();
bool AndroidReadMeshStrides(void* mesh, std::vector<int32_t>& strides);
// Readable generated meshes only; used to own an instance-local cloth output.
bool AndroidReadMeshGeometry(void* mesh, BetterEndfieldNext::CustomModel::BemComponent& component);
bool AndroidUpdateMeshPackedGeometry(void* mesh,const std::vector<float>& positions,const std::vector<uint32_t>& frames);
// Caller owns and roots the unpublished Mesh and initializes its skin field.
bool AndroidSubmitMesh(void* mesh, const BetterEndfieldNext::CustomModel::BemComponent& component);
// Retains the game's resource handle until Release; does not instantiate or
// display the donor prefab. Only called inside a main-thread delivery scope.
void* AndroidLoadUiDonor(const std::string& resource, void*& handle, uint32_t& root);
void AndroidReleaseUiDonor(void* handle, uint32_t root);
}
