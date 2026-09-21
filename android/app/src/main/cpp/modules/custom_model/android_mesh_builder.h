#pragma once
#include "core/runtime.h"
#include "bem.h"
#include <vector>

namespace betterendfield {
// Private, same-binary platform boundary; does not extend BE_HostApiV1.
void ConfigureAndroidMeshBuilder(Il2CppRuntime& runtime, bool rollback_test = false, bool pipeline_lod = false, bool npc_parameters = false, bool inspect = false);
bool AndroidMeshRollbackTest();
bool AndroidPipelineLodEnabled();
bool AndroidNpcParametersEnabled();
bool AndroidInspectionEnabled();
void AndroidAuditNormalTexture(void* texture,const std::string& name);
bool AndroidAuditMaterialCopy(void* original, void* copy);
void AndroidAuditTextureColorSpace(void* original, void* replacement, const std::string& name);
bool AndroidMeshBuilderReady();
bool AndroidReadMeshStrides(void* mesh, std::vector<int32_t>& strides);
// Caller owns and roots the unpublished Mesh and initializes its skin field.
bool AndroidSubmitMesh(void* mesh, const BetterEndfield::CustomModel::BemComponent& component);
// Retains the game's resource handle until Release; does not instantiate or
// display the donor prefab. Only called inside a main-thread delivery scope.
void* AndroidLoadUiDonor(const std::string& resource, void*& handle, uint32_t& root);
void AndroidReleaseUiDonor(void* handle, uint32_t root);
}
