// Shader: Merged Skeleton - Bone Data Importer - Rolling Buffer
// Version: 1.0
// Creator: SpectumQT
//
// Copies bone entires from current component skeleton into the global merged skeleton.
// Intended for Endfield's pipeline that supplies both offsets via CB and bone data via Resource.
//
// Each component owns a contiguous range inside the merged skeleton, beginning at ComponentVertexGroupOffset.

Texture1D<float4> IniParams : register(t120);

#define ComponentVertexGroupOffset   (uint)IniParams[0].x
#define ComponentVertexGroupCount    (uint)IniParams[0].y
#define UseOriginalComponentLODRemap (uint)IniParams[0].z
#define CustomComponentMeshScale     IniParams[0].w

#define InstanceConfigOffset         (uint)IniParams[1].x
#define MergedSkeletonOffset         (uint)IniParams[1].y
#define MergedSkeletonEntriesCount   (uint)IniParams[1].z

#define STATIC_TRANSFORMS_ENTRY_COUNT 2 * 256 * 3

cbuffer cb0 : register(b0)
{
    uint4 InstanceConfigCB[16];
}

StructuredBuffer<float4> ComponentBoneMatrices : register(t0);
Buffer<uint> OriginalComponentLODRemap         : register(t1);

RWStructuredBuffer<float4> MergedSkeletonRW : register(u0);
// RWBuffer<float4> DebugRW : register(u7);


[numthreads(64,1,1)]
void main(uint3 vThreadID : SV_DispatchThreadID)
{
    uint original_bone_id = vThreadID.x;

    if (original_bone_id >= ComponentVertexGroupCount)
        return;

    uint source_bone = original_bone_id;

    if (UseOriginalComponentLODRemap)
        source_bone = OriginalComponentLODRemap[source_bone];

    uint merged_bone = ComponentVertexGroupOffset + original_bone_id;
    
    uint src_entry = 3 + source_bone * 3;
    uint dst_entry = MergedSkeletonOffset + merged_bone * 3;

    uint4 src_offsets = InstanceConfigCB[InstanceConfigOffset];

    if (src_offsets.x >= STATIC_TRANSFORMS_ENTRY_COUNT)
    {
        uint src = src_offsets.x + src_entry;

        MergedSkeletonRW[dst_entry + 0] = ComponentBoneMatrices[src + 0] * CustomComponentMeshScale;
        MergedSkeletonRW[dst_entry + 1] = ComponentBoneMatrices[src + 1] * CustomComponentMeshScale;
        MergedSkeletonRW[dst_entry + 2] = ComponentBoneMatrices[src + 2] * CustomComponentMeshScale;
    }

    if (src_offsets.y >= STATIC_TRANSFORMS_ENTRY_COUNT)
    {
        uint src = src_offsets.y + src_entry;
        uint dst = MergedSkeletonEntriesCount + dst_entry;

        MergedSkeletonRW[dst + 0] = ComponentBoneMatrices[src + 0] * CustomComponentMeshScale;
        MergedSkeletonRW[dst + 1] = ComponentBoneMatrices[src + 1] * CustomComponentMeshScale;
        MergedSkeletonRW[dst + 2] = ComponentBoneMatrices[src + 2] * CustomComponentMeshScale;
    }
}
