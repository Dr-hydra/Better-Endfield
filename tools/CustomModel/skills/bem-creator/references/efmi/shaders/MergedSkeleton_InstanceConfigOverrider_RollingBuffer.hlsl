// Shader: Merged Skeleton - Component Instance Config Overrider - Rolling Buffer
// Version: 1.0
// Creator: SpectumQT
// 
// Copies instance config from CB and overrides offsets of non-empty skeletons with specified ones.

Texture1D<float4> IniParams : register(t120);

#define MergedSkeletonOffset       (uint)IniParams[0].x
#define InputInstanceConfigOffset  (uint)IniParams[0].y
#define OutputInstanceConfigOffset (uint)IniParams[0].z
#define MergedSkeletonEntriesCount (uint)IniParams[0].w

#define STATIC_TRANSFORMS_ENTRY_COUNT 2 * 256 * 3

cbuffer cb0 : register(b0)
{
    uint4 InstanceConfigCB[16];
}

RWBuffer<uint4> InstanceConfigRW : register(u0);

//RWBuffer<float4> DebugRW : register(u7);


[numthreads(1,1,1)]
void main(uint3 ThreadId : SV_DispatchThreadID)
{
    uint4 bone_offsets_entry;

    [unroll]
    for (uint i = 0; i < 16; ++i)
    {
        uint4 config_entry = InstanceConfigCB[InputInstanceConfigOffset + i];

        InstanceConfigRW[i] = config_entry;

        if (i == 5) {
            bone_offsets_entry = config_entry;
        }
    }
    
    if (bone_offsets_entry.x >= STATIC_TRANSFORMS_ENTRY_COUNT) {
        bone_offsets_entry.x = MergedSkeletonOffset;
    }

    if (bone_offsets_entry.y >= STATIC_TRANSFORMS_ENTRY_COUNT) {
        bone_offsets_entry.y = MergedSkeletonEntriesCount + MergedSkeletonOffset;
    }

    InstanceConfigRW[OutputInstanceConfigOffset + 5] = bone_offsets_entry;
}
