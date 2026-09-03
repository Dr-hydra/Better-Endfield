// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.ImageConversionModule.dll
// Classes:  2
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class ImageConversion
    {
        // Methods
        // RVA: 0x0B3FC77C  token: 0x6000001
        public static System.Byte[] EncodeToTGA(UnityEngine.Texture2D tex) { }
        // RVA: 0x0B3FC748  token: 0x6000002
        public static System.Byte[] EncodeToPNG(UnityEngine.Texture2D tex) { }
        // RVA: 0x0B3FC6F8  token: 0x6000003
        public static System.Byte[] EncodeToJPG(UnityEngine.Texture2D tex, System.Int32 quality) { }
        // RVA: 0x0B3FC738  token: 0x6000004
        public static System.Byte[] EncodeToJPG(UnityEngine.Texture2D tex) { }
        // RVA: 0x0B3FC6B8  token: 0x6000005
        public static System.Byte[] EncodeToEXR(UnityEngine.Texture2D tex, UnityEngine.Texture2D.EXRFlags flags) { }
        // RVA: 0x0B3FC6AC  token: 0x6000006
        public static System.Byte[] EncodeToEXR(UnityEngine.Texture2D tex) { }
        // RVA: 0x0B3FC7C0  token: 0x6000007
        public static System.Boolean LoadImage(UnityEngine.Texture2D tex, System.Byte[] data, System.Boolean markNonReadable) { }
        // RVA: 0x0B3FC7B0  token: 0x6000008
        public static System.Boolean LoadImage(UnityEngine.Texture2D tex, System.Byte[] data) { }

    }

}

