// Shader: Merged Skeleton - Component Instance Config Overrider - Constant Buffer
// Version: 1.0
// Creator: SpectumQT
// 
// Copies instance config from CB.

Texture1D<float4> IniParams : register(t120);

#define MergedSkeletonOffset       (uint)IniParams[0].x
#define InputInstanceConfigOffset  (uint)IniParams[0].y
#define OutputInstanceConfigOffset (uint)IniParams[0].z
#define MergedSkeletonEntriesCount (uint)IniParams[0].w

cbuffer cb0 : register(b0)
{
    uint4 InstanceConfigCB[16];
}

RWBuffer<uint4> InstanceConfigRW : register(u0);

//RWBuffer<float4> DebugRW : register(u7);


[numthreads(1,1,1)]
void main(uint3 ThreadId : SV_DispatchThreadID)
{
    [unroll]
    for (uint i = 0; i < 4; ++i)
    {
        uint4 config_entry = InstanceConfigCB[InputInstanceConfigOffset + i];

        InstanceConfigRW[i] = config_entry;
    }
}
