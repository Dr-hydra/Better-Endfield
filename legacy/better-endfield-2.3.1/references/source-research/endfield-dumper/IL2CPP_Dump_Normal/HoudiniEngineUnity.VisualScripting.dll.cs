// ========================================================
// Dumped by @desirepro
// Assembly: HoudiniEngineUnity.VisualScripting.dll
// Classes:  7
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x28
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public Unity.VisualScripting.Flow flow;  // 0x10
        public HoudiniEngineUnity.HEU_InstantiateHDA <>4__this;  // 0x18
        public System.Action ContinueFlow;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000008
        public System.Void .ctor() { }
        // RVA: 0x09D554E8  token: 0x6000009
        private System.Void <TriggerButton>b__0() { }
        // RVA: 0x09D55538  token: 0x600000A
        private System.Void <TriggerButton>b__1(HoudiniEngineUnity.HEU_ReloadEventData data) { }

    }

    // TypeToken: 0x2000005  // size: 0x20
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public Unity.VisualScripting.Flow flow;  // 0x10
        public HoudiniEngineUnity.HEU_ModifyHDA <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000010
        public System.Void .ctor() { }
        // RVA: 0x09D55570  token: 0x6000011
        private System.Void <TriggerButton>b__0() { }

    }

namespace HoudiniEngineUnity
{

    // TypeToken: 0x2000002  // size: 0xD0
    public class HEU_InstantiateHDA : Unity.VisualScripting.ManualEventUnit`1
    {
        // Fields
        public HoudiniEngineUnity.HEU_UnitButton triggerButton;  // 0x80
        public Unity.VisualScripting.ValueInput inputPath;  // 0x88
        public Unity.VisualScripting.ValueInput inputAsync;  // 0x90
        public Unity.VisualScripting.ValueInput inputPosition;  // 0x98
        public Unity.VisualScripting.ValueOutput outputHDARoot;  // 0xa0
        public Unity.VisualScripting.ValueOutput outputHDAAsset;  // 0xa8
        public Unity.VisualScripting.ValueOutput outputSuccess;  // 0xb0
        private HoudiniEngineUnity.HEU_HoudiniAssetRoot hdaRoot;  // 0xb8
        private HoudiniEngineUnity.HEU_HoudiniAsset hdaAsset;  // 0xc0
        private System.Boolean bSuccess;  // 0xc8

        // Properties
        System.String hookName { get; /* RVA: 0x09D551A0 */ }

        // Methods
        // RVA: 0x09D54A94  token: 0x6000002
        protected virtual System.Void Definition() { }
        // RVA: 0x09D54D70  token: 0x6000003
        public System.Void TriggerButton(Unity.VisualScripting.GraphReference reference) { }
        // RVA: 0x09D55164  token: 0x6000004
        public System.Void .ctor() { }
        // RVA: 0x04D85EF0  token: 0x6000005
        private HoudiniEngineUnity.HEU_HoudiniAssetRoot <Definition>b__12_0(Unity.VisualScripting.Flow flow) { }
        // RVA: 0x04D892F0  token: 0x6000006
        private HoudiniEngineUnity.HEU_HoudiniAsset <Definition>b__12_1(Unity.VisualScripting.Flow flow) { }
        // RVA: 0x04D8E270  token: 0x6000007
        private System.Boolean <Definition>b__12_2(Unity.VisualScripting.Flow flow) { }

    }

    // TypeToken: 0x2000004  // size: 0xA0
    public class HEU_ModifyHDA : Unity.VisualScripting.ManualEventUnit`1
    {
        // Fields
        public HoudiniEngineUnity.HEU_UnitButton triggerButton;  // 0x80
        public Unity.VisualScripting.ValueInput inputHDA;  // 0x88
        public Unity.VisualScripting.ValueOutput outputHDAAsset;  // 0x90
        private HoudiniEngineUnity.HEU_HoudiniAsset hdaAsset;  // 0x98

        // Properties
        System.String hookName { get; /* RVA: 0x09D554BC */ }

        // Methods
        // RVA: 0x09D551CC  token: 0x600000C
        protected virtual System.Void Definition() { }
        // RVA: 0x09D552E0  token: 0x600000D
        public System.Void TriggerButton(Unity.VisualScripting.GraphReference reference) { }
        // RVA: 0x09D55480  token: 0x600000E
        public System.Void .ctor() { }
        // RVA: 0x04D85EE0  token: 0x600000F
        private HoudiniEngineUnity.HEU_HoudiniAsset <Definition>b__6_0(Unity.VisualScripting.Flow flow) { }

    }

    // TypeToken: 0x2000006  // size: 0x18
    public class HEU_UnitButton
    {
        // Fields
        public System.Action action;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000012
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x28
    public class HEU_UnitButtonAttribute : System.Attribute
    {
        // Fields
        public System.String functionName;  // 0x10
        public System.String buttonLabel;  // 0x18
        public System.Int32 buttonWidth;  // 0x20

        // Methods
        // RVA: 0x047E1B40  token: 0x6000013
        public System.Void .ctor(System.String fnName, System.String btnLabel, System.Int32 btnWidth) { }

    }

}

