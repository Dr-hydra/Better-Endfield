// Shader: Shape Key Setter
// Version: 1.0
// Creator: SpectumQT
// Comment: Sets `ShapeKeyId` shapekey value to `ShapeKeyValue` in multi-batch storage.

Texture1D<float4> IniParams : register(t120);

#define ShapeKeyId IniParams[0].x
#define ShapeKeyValue IniParams[0].y

// Stores shapekey values for all batches:
// Data structure (2 batches example):
// 0. 32 float4: Batch0 Shapekey Values [X, Y, Z, W] (0   - 126)
// 1. 32 float4: Batch1 Shapekey Values [X, Y, Z, W] (127 - 253)
RWBuffer<float4> ShapeKeyValuesRW : register(u3);
// RWBuffer<float4> DebugRW : register(u7);

[numthreads(1,1,1)]
void main(uint3 ThreadId : SV_DispatchThreadID)
{
    // Expected user input for ShapeKeyId is in [0, 253] range
    // When more than 1 shapekeys batch is used, we need to align value pos to 128
    // It's required because 1 batch can fit only 127 shapekeys, while ShapeKeyLoader reads aligned sequence of 128
    // We don't want to confuse the user with gaps in ids, and don't want to complicate component math of ShapeKeyLoader
    // So we add +1 to offset per each batch before the one where ShapeKeyId belongs
    // ShapeKey 0   / 127 = 0 (batch 0, offset +0)
    // ShapeKey 126 / 127 = 0 (batch 0, offset +0)
    // ShapeKey 127 / 127 = 1 (batch 1, offset +1)
    // ShapeKey 256 / 127 = 2 (batch 2, offset +2)
    uint container_offset = uint(ShapeKeyId / 127);

    // ShapeKey 126 (batch 0 (handles   0-126), offset +0) goes to index 126 (last used index of 32 float32 block)
    // ShapeKey 127 (batch 1 (handles 127-253), offset +1) goes to index 128 (first index of next 32 float32 block)
    uint shape_key_index = container_offset + uint(ShapeKeyId);

    // Determine to which component of values entry this shape_key_index belongs.
    uint shape_key_component_id = shape_key_index % uint(4);

    // Each config entry stores values of 4 shapekeys.
    uint shape_key_config_entry_id = (shape_key_index - shape_key_component_id) / uint(4);

    // DebugRW[0] = float4(shape_key_index, shape_key_component_id, shape_key_config_entry_id, shape_key_value);
    
    // Get config entry that stores value of requested shapekey.
    float4 config_entry = ShapeKeyValuesRW[shape_key_config_entry_id];
 
    // Update corresponding component of config entry with new shapekey value.
	switch(shape_key_component_id) {
		case 0:
			config_entry.x = ShapeKeyValue;
			break;
		case 1:
			config_entry.y = ShapeKeyValue;
			break;
		case 2:
			config_entry.z = ShapeKeyValue;
			break;
		case 3:
			config_entry.w = ShapeKeyValue;
			break;
	};

    // Store updated config entry.
    ShapeKeyValuesRW[shape_key_config_entry_id] = config_entry;
}
