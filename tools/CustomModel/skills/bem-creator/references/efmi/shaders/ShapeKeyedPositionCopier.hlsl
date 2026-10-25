// Shader: ShapeKeyed Position Copier
// Version: 1.0
// Creator: SpectumQT
// Description: Copies float3 vertex positions between buffers with differing strides.

Texture1D<float4> IniParams : register(t120);

#define VertexOffset                 IniParams[0].x
#define VertexCount                  IniParams[0].y
#define SourceVertexFloatsCount      IniParams[0].z
#define DestinationVertexFloatsCount IniParams[0].w

Buffer<float> SrcVertexPositions : register(t0);
RWBuffer<float> DstVertexPositions : register(u0);
// RWBuffer<float4> DebugRW : register(u7);

[numthreads(64,1,1)]
void main(uint3 vThreadID : SV_DispatchThreadID)
{
    if (vThreadID.x >= (uint)VertexCount) {
        return;
    }

    uint vertex_id = vThreadID.x + (uint)VertexOffset;

    uint src_offset = vertex_id * (uint)SourceVertexFloatsCount;
    uint dst_offset = vertex_id * (uint)DestinationVertexFloatsCount;

    DstVertexPositions[dst_offset + 0] = SrcVertexPositions.Load(src_offset + 0);
    DstVertexPositions[dst_offset + 1] = SrcVertexPositions.Load(src_offset + 1);
    DstVertexPositions[dst_offset + 2] = SrcVertexPositions.Load(src_offset + 2);
}
