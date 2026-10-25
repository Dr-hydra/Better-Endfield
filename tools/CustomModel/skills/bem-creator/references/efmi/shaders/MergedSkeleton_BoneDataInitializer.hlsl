// Shader: Merged Skeleton Bone Data Initializer
// Version: 1.0
// Creator: SpectumQT
// 
// Initializes Endfield's-compatible bone data rolling buffer which starts with static data for implicit transforms.
// With dispatch size of `8, 1, 1` writes `1 0 0 0   0 1 0 0   0 0 1 0` pattern to the first 512 bone entries.

Texture1D<float4> IniParams : register(t120);

RWStructuredBuffer<float4> MergedSkeletonRW : register(u0);
// RWBuffer<float4> DebugRW : register(u7);

[numthreads(64,1,1)]
void main(uint3 vThreadID : SV_DispatchThreadID)
{
    uint merged_bone = vThreadID.x;

    uint dst_entry = merged_bone * 3;

    MergedSkeletonRW[dst_entry + 0] = float4(1, 0, 0, 0);
    MergedSkeletonRW[dst_entry + 1] = float4(0, 1, 0, 0);
    MergedSkeletonRW[dst_entry + 2] = float4(0, 0, 1, 0);
}
