// Shader: Shape Key Dequantizer
// Version: 1.0
// Creator: SpectumQT
// Description: Converts INT32 per-vertex position offsets to FLOAT32 using quantization scales from config.
// Outputs morphed mesh (sums of per-axis positions of mesh vertices with dequantized shapekey deltas).

Texture1D<float4> IniParams        : register(t120);

#define VertexOffset                 IniParams[0].x
#define VertexCount                  IniParams[0].y
#define SourceVertexFloatsCount      IniParams[0].z
#define DestinationVertexFloatsCount IniParams[0].w

// Stores configuration for all batches:
// Data structure (2 batches example):
// 1. 1  float4: Quantization Scales [X, Y, Z], Unused [W]
// 2. 1  float4: Dequantization Scales [X, Y, Z], Unused [W]
// 3. 1  float4: Batch0 Vertex Offset [X], Unused [Y, Z, W]
// 4. 32 float4: Batch0 Shapekey Offsets [X, Y, Z, W]
// 5. 1  float4: Batch1 Vertex Offset [X], Unused [Y, Z, W]
// 6. 32 float4: Batch1 Shapekey Offsets [X, Y, Z, W]
Buffer<uint4> ShapeKeyBatchConfigs : register(t0);
Buffer<float> VertexPositions      : register(t1);

RWBuffer<uint> VertexPositionOffsetsRW : register(u0);
RWBuffer<float> VertexPositionsRW : register(u2);
//RWBuffer<float4> DebugRW : register(u7);

[numthreads(64,1,1)]
void main(uint3 vThreadID : SV_DispatchThreadID)
{
    uint vertex_index = vThreadID.x;

    if (vertex_index >= (uint)VertexCount)
        return;

    vertex_index = (uint)VertexOffset + vertex_index;

    // Get per-axis dequantization scales from batch 0 config (same for all batches).
    float3 delta_dequantization_scales = asfloat(ShapeKeyBatchConfigs[1].xyz);

    // Get per-axis quantized deltas.
    uint base_delta_index = vertex_index * 3;
    int quantized_delta_x = asint(VertexPositionOffsetsRW[base_delta_index + 0]);
    int quantized_delta_y = asint(VertexPositionOffsetsRW[base_delta_index + 1]);
    int quantized_delta_z = asint(VertexPositionOffsetsRW[base_delta_index + 2]);

    // Dequantize accumulated deltas from integer space.
    float accumulated_delta_x = quantized_delta_x * delta_dequantization_scales.x;
    float accumulated_delta_y = quantized_delta_y * delta_dequantization_scales.y;
    float accumulated_delta_z = quantized_delta_z * delta_dequantization_scales.z;

    // Load per-axis positions.
    uint input_position_index = vertex_index * (uint)SourceVertexFloatsCount;
    float position_x = VertexPositions.Load(input_position_index + 0);
    float position_y = VertexPositions.Load(input_position_index + 1);
    float position_z = VertexPositions.Load(input_position_index + 2);

    // Write positions & deltas sums to vertex position buffer.
    uint output_position_index = vertex_index * (uint)DestinationVertexFloatsCount;
    VertexPositionsRW[output_position_index + 0] = position_x + accumulated_delta_x;
    VertexPositionsRW[output_position_index + 1] = position_y + accumulated_delta_y;
    VertexPositionsRW[output_position_index + 2] = position_z + accumulated_delta_z;
}
