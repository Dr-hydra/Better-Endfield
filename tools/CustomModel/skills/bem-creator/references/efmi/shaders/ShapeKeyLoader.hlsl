// Shader: Shape Key Loader
// Version: 1.0
// Creator: SpectumQT
// Comment: Accumulates per-vertex weighted deltas of all shape keys of current batch in INT32 storage.

Texture1D<float4> IniParams : register(t120);

#define ShapeKeyBatchID IniParams[0].x

// Stores configuration for all batches:
// Data structure (2 batches example):
// 1. 1  float4: Quantization Scales [X, Y, Z], Unused [W]
// 2. 1  float4: Dequantization Scales [X, Y, Z], Unused [W]
// 3. 1  float4: Batch0 Vertex Offset [X], Unused [Y, Z, W]
// 4. 32 float4: Batch0 Shapekey Offsets [X, Y, Z, W]
// 5. 1  float4: Batch1 Vertex Offset [X], Unused [Y, Z, W]
// 6. 32 float4: Batch1 Shapekey Offsets [X, Y, Z, W]
Buffer<uint4> ShapeKeyBatchConfigs : register(t0);
Buffer<uint4> ShapeKeyVertexIds : register(t1);
Buffer<float4> ShapeKeyVertexOffsets : register(t2);

RWBuffer<uint> VertexPositionOffsetsRW : register(u0);
RWBuffer<uint4> ShapeKeyValuesRW : register(u3);
//RWBuffer<float4> DebugRW : register(u7);

groupshared struct { uint val; } shapekey_offsets[128];
groupshared struct { float val; } shapekey_values[128];

[numthreads(32,1,1)]
void main(uint3 vThreadID : SV_DispatchThreadID, uint3 vThreadIDInGroup : SV_GroupThreadID)
{
    // DebugRW[vThreadID.y] = float4(vThreadID.x, vThreadID.y, vThreadIDInGroup.x, vThreadIDInGroupFlattened.x);

    uint shapekey_index = vThreadIDInGroup.x;

    uint batch_config_offset = 2 + (uint)ShapeKeyBatchID * 33;

    // Load shape key vertex offsets.
    uint4 offsets = ShapeKeyBatchConfigs[batch_config_offset + 1 + shapekey_index]; // ShapeKeyBatchConfigRW[shapekey_index]

    // Load shape key values.
    uint shapekey_values_offset = (uint)ShapeKeyBatchID * 32;
    float4 values = asfloat(ShapeKeyValuesRW[shapekey_values_offset + shapekey_index]); // ShapeKeyBatchConfigRW[32 + shapekey_index]

    // Expand vectors into scalar shared-memory arrays.
    [unroll]
    for (uint component = 0; component < 4; component++)
    {
        shapekey_values[shapekey_index * 4 + component].val = values[component];
        shapekey_offsets[shapekey_index * 4 + component].val = offsets[component];
    }

    GroupMemoryBarrierWithGroupSync();

    // vThreadID.y is a flattened index of shapekeyed vertex within the current dispatch batch.
    uint dispatch_vertex_index = vThreadID.x;

    // Here we determine which shape key owns this shapekeyed vertex (procedded by current dispatch entry).
    //
    // Buffer `shapekey_offsets` contains offsets to first vertex of each shape key of current shape key batch:
    //
    // Example:
    //   shapekey_offsets = [0, 12, 25, 40, 40, 40 ... 40]
    // Meaning:
    //   ShapeKey0 -> shapekeyed vertices [0, 11]
    //   ShapeKey1 -> shapekeyed vertices [12, 24]
    //   ShapeKey2 -> shapekeyed vertices [25, 39]
    //   ShapeKey3 -> no shapekeyed vertices (should not be processed)
    //
    // We need the largest shapekey_index such that:
    //
    //   shapekey_offsets[shapekey_index] <= dispatch_vertex_index
    //
    // This is effectively an upper-bound binary search over the offset table.
    //
    // Example:
    //   dispatch_vertex_index = 24
    //
    //   12 <= 24  -> ShapeKey1 is valid
    //   25 >  24  -> ShapeKey2 is outside the range
    //
    // Result:
    //   shapekey_index = 1
     shapekey_index = 0;
    [unroll]
    for (uint step = 64; step > 0; step >>= 1)
    {
        if (dispatch_vertex_index >= shapekey_offsets[shapekey_index + step].val) {
            shapekey_index += step;
        }
    }           

    // First vertex index of next shapekey is the upper bound for current shapekey.
    uint next_shapekey_first_vertex_index = shapekey_offsets[min(shapekey_index + 1, 127)].val;

    // Skip processing shapekeyed vertex which index is outside of the range of current batch.
    if (dispatch_vertex_index >= next_shapekey_first_vertex_index) {
        return;
    }

    // Get shape key value.
    float shapekey_value = shapekey_values[shapekey_index].val;

    // Skip processing shape key with zero weight.
    if (shapekey_value == 0.0f) {
        return;
    }

    // Base offset of the current shape key batch inside the global packed shape key vertex stream.
    uint shapekey_batch_vertex_offset = ShapeKeyBatchConfigs[batch_config_offset].x;

    // Absolute shape key vertex index inside the packed global shapekeyed vertex stream.
    uint shapekey_vertex_index = shapekey_batch_vertex_offset + dispatch_vertex_index;

    // Lookup vertex affected by this shape key entry.
    uint mesh_vertex_index = ShapeKeyVertexIds.Load(shapekey_vertex_index).x;

    // Each thread group lane processes 3 channels (position x/y/z deltas):
    // Position XYZ delta #0 with per-axis quantization scales.
    for (uint axis = 0; axis < 3; axis++) {

        // ShapeKey delta storage layout (per shapekeyed vertex):
        float delta = ShapeKeyVertexOffsets.Load(shapekey_vertex_index * 3 + axis).x;

        // Apply shape key value.
        float weighted_delta = delta * shapekey_value;

        // Quantize into integer accumulation space.
        
        // Position channels use separate per-axis scales.
        float quantization_scale = asfloat(ShapeKeyBatchConfigs[0][axis]);

        // Convert float delta into fixed-point integer.
        int quantized_delta = (int)round(weighted_delta * quantization_scale);

        // Output layout: [x0 y0 z0]
        uint output_buffer_index = mesh_vertex_index * 3 + axis;

        // Atomically accumulate shapekey contribution.
        InterlockedAdd(VertexPositionOffsetsRW[output_buffer_index], quantized_delta);
    }

    return;
}
