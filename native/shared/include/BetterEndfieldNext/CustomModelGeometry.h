#pragma once
#include "ModuleApi.h"
// Optional read-only bridge; it does not extend BE_HostApiV1. Query on the game
// thread only. Views are borrowed exclusively for the synchronous callback.
// Consumers must copy what they need and must not call back into CustomModel.
#define BETTER_ENDFIELD_NEXT_CUSTOM_MODEL_GEOMETRY_V1 1u
typedef struct BE_CustomModelDrawV1 { uint32_t first_index,index_count; } BE_CustomModelDrawV1;
typedef struct BE_CustomModelGeometryV1 {
    uint32_t struct_size,version,vertex_count,index_count;
    const float* positions_xyz; // vertex_count tightly packed Float32 XYZ
    const uint8_t* skin;
    // 12: UNorm16 weight[4] + UInt8 bone[4]; 32: Float32 weight[4] + UInt32 bone[4].
    // 4: UInt8 bone[4]; production skinning uses first bone only, implicit weight 1.
    uint32_t skin_stride;
    const uint32_t* indices;
    const BE_CustomModelDrawV1* draws;
    uint32_t draw_count;
} BE_CustomModelGeometryV1;
typedef BE_Result (BE_CALL *BE_CustomModelGeometryVisitorV1)(void* context,const BE_CustomModelGeometryV1* view);
typedef BE_Result (BE_CALL *BE_QueryCustomModelGeometryV1)(void* managed_mesh,
    BE_CustomModelGeometryVisitorV1 visitor,void* context);
// Named optional export: BetterEndfieldNext_QueryCustomModelGeometryV1.
// NotFound includes cache eviction or a mesh that is not a BEM replacement.
