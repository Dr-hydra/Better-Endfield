// ========================================================
// Dumped by @desirepro
// Assembly: IFix.Define.dll
// Classes:  14
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace Beyond.HotFix
{

    // TypeToken: 0x2000002  // size: 0x30
    public class HotfixDownloadPayload
    {
        // Fields
        public System.Int32 effectiveVersion;  // 0x10
        public System.String luahotfix;  // 0x18
        public System.String i18nhotfix;  // 0x20
        public System.String ifixhotfix;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000003  // size: 0x30
    public class PatchClipboardData
    {
        // Fields
        public System.Int32 ver;  // 0x10
        public System.Int32 overrideVFSIFixVersionFrom;  // 0x14
        public System.Int32 overrideVFSIFixVersionTo;  // 0x18
        public System.String platform;  // 0x20
        public System.Collections.Generic.List<Beyond.HotFix.PatchItem> patches;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x30
    public class LuaPatchClipboardData
    {
        // Fields
        public System.Int32 ver;  // 0x10
        public System.Int32 overrideVFSIFixVersionFrom;  // 0x14
        public System.Int32 overrideVFSIFixVersionTo;  // 0x18
        public System.String platform;  // 0x20
        public System.Collections.Generic.List<Beyond.HotFix.PatchItem> luaPatches;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000003
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x30
    public class PatchItem
    {
        // Fields
        public System.String name;  // 0x10
        public System.String md5;  // 0x18
        public System.Int32 size;  // 0x20
        public System.String data;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x28
    public class HotfixDownloadPackage
    {
        // Fields
        public System.String alg;  // 0x10
        public System.String data;  // 0x18
        public System.String sign;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public static class HotFixPatchDefine
    {
        // Fields
        public static readonly System.String hotFixPatchFileExtension;  // static @ 0x0
        public static System.String HOTFIX_DOWNLOAD_ALGORITHM;  // const
        public static readonly System.String CLIENT_NET_RSA_PUBLIC_KEY;  // static @ 0x8
        private static readonly System.Byte[] s_luaPatchAesKey;  // static @ 0x10
        private static readonly System.Byte[] s_luaPatchAesIV;  // static @ 0x18
        private static readonly System.Byte[] s_hotfixDownloadHmacKey;  // static @ 0x20
        private static readonly System.Byte[] s_hotfixDownloadEnvelopeAesKey;  // static @ 0x28
        private static readonly System.Byte[] s_hotfixDownloadEnvelopeAesIV;  // static @ 0x30

        // Methods
        // RVA: 0x09E13E40  token: 0x6000006
        public static System.Byte[] EncryptLuaPatchPayload(System.Byte[] luaBytes) { }
        // RVA: 0x03C0D460  token: 0x6000007
        public static System.Byte[] DecryptLuaPatchPayload(System.Byte[] encryptedBytes, System.Int32 offset, System.Int32 count) { }
        // RVA: 0x09E140F4  token: 0x6000008
        public static System.String PackHotfixDownloadText(System.String rawJson) { }
        // RVA: 0x03C0B760  token: 0x6000009
        public static System.Boolean TryDecodeHotfixDownloadText(System.String downloadText, System.String& json, System.String& error) { }
        // RVA: 0x09E142AC  token: 0x600000A
        public static System.Boolean TryExtractLuaHotfix(System.String json, System.String& luaHotfix, System.String& error) { }
        // RVA: 0x09E1446C  token: 0x600000B
        public static System.Boolean VerifyMD5(System.Byte[] bytes, System.String expectMd5) { }
        // RVA: 0x09E13DB0  token: 0x600000C
        public static System.Byte[] EncryptHotfixDownloadEnvelope(System.Byte[] bytes) { }
        // RVA: 0x03C0A7A0  token: 0x600000D
        public static System.Byte[] DecryptHotfixDownloadEnvelope(System.Byte[] encryptedBytes, System.Int32 offset, System.Int32 count) { }
        // RVA: 0x03C0B3B0  token: 0x600000E
        private static System.Boolean _VerifyHotfixDownloadHmac(System.String content, System.String signatureBase64) { }
        // RVA: 0x03C0A530  token: 0x600000F
        private static System.Byte[] _ComputeHotfixDownloadHmac(System.String content) { }
        // RVA: 0x04593100  token: 0x6000010
        private static System.Boolean _FixedTimeEquals(System.Byte[] left, System.Byte[] right) { }
        // RVA: 0x09E14650  token: 0x6000011
        private static System.Byte[] _EncryptAes(System.Byte[] bytes, System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x03C0B460  token: 0x6000012
        private static System.Byte[] _DecryptAes(System.Byte[] encryptedBytes, System.Int32 offset, System.Int32 count, System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x04994BE0  token: 0x6000013
        private static System.Void .cctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000008  // size: 0x10
    public class HotfixPatchAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000014
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class HotfixInterpretAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000015
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public class IFixIgnoreAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000016
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000017
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x09E148CC  token: 0x6000018
        public System.Byte[] __Gen_Wrap_0(System.Object P0) { }
        // RVA: 0x09E149BC  token: 0x6000019
        public System.Byte[] __Gen_Wrap_1(System.Object P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x09E14AE0  token: 0x600001A
        public System.Byte[] __Gen_Wrap_2(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x09E14BF4  token: 0x600001B
        public System.String __Gen_Wrap_3(System.Object P0) { }
        // RVA: 0x09E14CD0  token: 0x600001C
        public System.Byte[] __Gen_Wrap_4(System.Object P0, System.Int32 P1, System.Int32 P2, System.Object P3, System.Object P4) { }
        // RVA: 0x06366BC4  token: 0x600001D
        public System.Boolean __Gen_Wrap_5(System.Object P0, System.Object P1) { }
        // RVA: 0x09E14E14  token: 0x600001E
        public System.Boolean __Gen_Wrap_6(System.Object P0, System.String& P1, System.String& P2) { }
        // RVA: 0x04D37870  token: 0x600001F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x09E14F90  token: 0x6000020
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x6000021
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200000D  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000022
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x09E1520C  token: 0x6000023
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x04593190  token: 0x6000024
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x09E150C8  token: 0x6000025
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x09E15194  token: 0x6000026
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x09E15270  token: 0x6000027
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x09E15024  token: 0x6000028
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x200000E  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-EncryptLuaPatchPayload0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-DecryptLuaPatchPayload0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-_ComputeHotfixDownloadHmac0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-_EncryptAes0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-EncryptHotfixDownloadEnvelope0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-PackHotfixDownloadText0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-_DecryptAes0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-DecryptHotfixDownloadEnvelope0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-_FixedTimeEquals0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-_VerifyHotfixDownloadHmac0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-TryDecodeHotfixDownloadText0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-TryExtractLuaHotfix0;  // const
        public static IFix.IDMAP0 Beyond-HotFix-HotFixPatchDefine-VerifyMD50;  // const

    }

}

