using AnimeStudio;
using System.Buffers.Binary;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

// Original game resource identities only; no generated-artifact hashes.
static class NativeResourceIdentity
{
    public static uint Crc32C(ReadOnlySpan<byte> data, uint seed = 0)
    {
        uint crc = ~seed;
        if (Sse42.IsSupported) {
            while (data.Length >= 4) {
                crc = Sse42.Crc32(crc, BinaryPrimitives.ReadUInt32LittleEndian(data));
                data = data[4..];
            }
            foreach (byte b in data) crc = Sse42.Crc32(crc, b);
        } else {
            foreach (byte b in data) {
                crc ^= b;
                for (int i = 0; i < 8; ++i) crc = (crc >> 1) ^ ((crc & 1) != 0 ? 0x82F63B78u : 0);
            }
        }
        return ~crc;
    }

    public static object MeshIdentity(Mesh mesh)
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var indices = (uint[]?)typeof(Mesh).GetField("m_IndexBuffer", flags)?.GetValue(mesh)
            ?? throw new InvalidDataException("Backend index buffer unavailable");
        bool shortIndices = (bool)(typeof(Mesh).GetField("m_Use16BitIndices", flags)?.GetValue(mesh)
            ?? throw new InvalidDataException("Backend index format unavailable"));
        int size = shortIndices ? 2 : 4;
        byte[] bytes = new byte[checked(indices.Length * size)];
        for (int i = 0; i < indices.Length; ++i) {
            if (size == 2) BinaryPrimitives.WriteUInt16LittleEndian(bytes.AsSpan(i * size), checked((ushort)indices[i]));
            else BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(i * size), indices[i]);
        }
        return new { algorithm = "crc32c", index_size = size, byte_width = bytes.Length,
            index_count = indices.Length, data_crc32c = Crc32C(bytes).ToString("x8") };
    }

    public static object TextureIdentity(Texture2D texture)
    {
        var nodes=texture.serializedType?.m_Type?.m_Nodes
            ?? throw new InvalidDataException("Texture type tree unavailable");
        int color=nodes.FindIndex(n=>n.m_Level==1 && n.m_Name=="m_ColorSpace");
        if(color<1) throw new InvalidDataException("Texture color space unavailable");
        var prefix=new TypeTree { m_Nodes=nodes.Take(color+1).ToList() };
        var metadata=TypeTreeHelper.ReadType(prefix,texture.reader);
        int colorSpace=Convert.ToInt32(metadata["m_ColorSpace"]);
        int block = texture.m_TextureFormat.ToString() switch {
            "DXT1" or "BC4" => 8,
            "DXT5" or "BC5" or "BC6H" or "BC7" => 16,
            _ => 0
        };
        int pixel = texture.m_TextureFormat.ToString() switch {
            "RGBA32" or "ARGB32" or "BGRA32" => 4, "R8" or "Alpha8" => 1,
            "RG16" or "R16" => 2, "RGBAHalf" => 8, "RGBAFloat" => 16, _ => 0
        };
        if (block == 0 && pixel == 0) throw new InvalidDataException("Unmapped native texture encoding");
        byte[] bytes = texture.image_data.GetData();
        int w=texture.m_Width, h=texture.m_Height, offset=0;
        var levels=new List<object>();
        for(int mip=0; mip<texture.m_MipCount; ++mip) {
            int size=checked(block>0 ? ((w+3)/4)*((h+3)/4)*block : w*h*pixel);
            if(offset+size>bytes.Length) throw new InvalidDataException("Texture mip payload truncated");
            // texture_hash=0 uses the smaller of w*h and the complete first
            // subresource size for tightly packed, single-array textures.
            int legacyLength=Math.Min(checked(w*h),size);
            levels.Add(new { mip, width=w, height=h, byte_size=size,
                legacy_crc32c=Crc32C(bytes.AsSpan(offset,legacyLength)).ToString("x8"),
                full_crc32c=Crc32C(bytes.AsSpan(offset,size)).ToString("x8") });
            offset+=size;w=Math.Max(w/2,1);h=Math.Max(h/2,1);
        }
        return new { algorithm="crc32c", format=texture.m_TextureFormat.ToString(), color_space=colorSpace,
            payload_bytes=bytes.Length, accounted_bytes=offset, levels };
    }
}
