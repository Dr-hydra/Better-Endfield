// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.JSONSerializeModule.dll
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
    public static class JsonUtility
    {
        // Methods
        // RVA: 0x04047440  token: 0x6000001
        private static System.String ToJsonInternal(System.Object obj, System.Boolean prettyPrint) { }
        // RVA: 0x03F2EE30  token: 0x6000002
        private static System.Object FromJsonInternal(System.String json, System.Object objectToOverwrite, System.Type type) { }
        // RVA: 0x05393424  token: 0x6000003
        public static System.String ToJson(System.Object obj) { }
        // RVA: 0x040473E0  token: 0x6000004
        public static System.String ToJson(System.Object obj, System.Boolean prettyPrint) { }
        // RVA: -1  // generic def  token: 0x6000005
        public static T FromJson(System.String json) { }
        // RVA: 0x03F2ED80  token: 0x6000006
        public static System.Object FromJson(System.String json, System.Type type) { }
        // RVA: 0x0B418274  token: 0x6000007
        public static System.Void FromJsonOverwrite(System.String json, System.Object objectToOverwrite) { }

    }

}

