// ========================================================
// Dumped by @desirepro
// Assembly: HoudiniEngineUnity.dll
// Classes:  447
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002
    public sealed class <>f__AnonymousType0`2
    {
        // Fields
        private readonly <owner>j__TPar <owner>i__Field;  // 0x0
        private readonly <houdiniVersionRequired>j__TPar <houdiniVersionRequired>i__Field;  // 0x0

        // Properties
        <owner>j__TPar owner { get; /* RVA: -1  // not resolved */ }
        <houdiniVersionRequired>j__TPar houdiniVersionRequired { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000003
        public System.Void .ctor(<owner>j__TPar owner, <houdiniVersionRequired>j__TPar houdiniVersionRequired) { }
        // RVA: -1  // not resolved  token: 0x6000004
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000005
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x6000006
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000003  // size: 0x18
    public class HEU_BoundingVolume : UnityEngine.MonoBehaviour
    {
        // Properties
        UnityEngine.Collider BoundingCollider { get; /* RVA: 0x09D38148 */ }

        // Methods
        // RVA: 0x09D37F10  token: 0x6000008
        public System.Collections.Generic.List<UnityEngine.GameObject> GetAllIntersectingObjects() { }
        // RVA: 0x05393520  token: 0x6000009
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public class HEU_ExampleEvergreenQuery
    {
        // Methods
        // RVA: 0x09D3D364  token: 0x600000A
        public static System.Void StartQuery() { }
        // RVA: 0x09D3D05C  token: 0x600000B
        public static HoudiniEngineUnity.HEU_HoudiniAsset QueryHoudiniAsset(UnityEngine.GameObject rootGO) { }
        // RVA: 0x09D3CD3C  token: 0x600000C
        public static System.Void CookAsset(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset) { }
        // RVA: 0x09D3C9CC  token: 0x600000D
        public static System.Void ChangeParmsAndCook(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset) { }
        // RVA: 0x09D3D1D8  token: 0x600000E
        public static System.Void QueryObjects(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset) { }
        // RVA: 0x09D3CF98  token: 0x600000F
        public static System.Void QueryGeoParts(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_GeoInfo& geoInfo) { }
        // RVA: 0x09D3D254  token: 0x6000010
        public static System.Void QueryPartAttributeByOwner(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeOwner owner, System.Int32 count, System.Text.StringBuilder sb) { }
        // RVA: 0x09D3CD50  token: 0x6000011
        public static System.Void QueryAttributeByStorageType(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.String attrName) { }
        // RVA: 0x09D3CECC  token: 0x6000012
        public static System.Void QueryAttribute(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset, System.String objName, System.String geoName, System.Int32 partID, System.String attrName) { }
        // RVA: 0x041E1670  token: 0x6000013
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x18
    public class HEU_ExampleInstanceCustomAttribute : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x09D3D544  token: 0x6000014
        private System.Void InstancerCallback() { }
        // RVA: -1  // generic def  token: 0x6000015
        private static System.Void LogArray(System.String name, T[] arr, System.Int32 tupleSize) { }
        // RVA: 0x09D3D8C4  token: 0x6000016
        private static System.Void LogAttr(HoudiniEngineUnity.HEU_OutputAttribute outAttr) { }
        // RVA: 0x05393520  token: 0x6000017
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public class HEU_ScriptCallbackExample : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.String _msg;  // 0x18

        // Methods
        // RVA: 0x09D53BE8  token: 0x6000018
        private System.Void AssetCallbackWithMsg(System.String msg) { }
        // RVA: 0x09D53B94  token: 0x6000019
        private System.Void AssetCallbackNoMsg() { }
        // RVA: 0x05393520  token: 0x600001A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct OutputMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HEU_ScriptMeshInputUVLayoutExample.OutputMode COPY;  // const
        public static HEU_ScriptMeshInputUVLayoutExample.OutputMode REPLACE;  // const

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class HEU_ScriptMeshInputUVLayoutExample
    {
        // Methods
        // RVA: 0x09D53C6C  token: 0x600001B
        public static System.Void ApplyUVLayoutTo(UnityEngine.GameObject[] gameObjects, HEU_ScriptMeshInputUVLayoutExample.OutputMode outputMode, System.String output_name_suffix) { }
        // RVA: 0x041E1670  token: 0x600001C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x30
    public class HEU_ScriptParameterExample : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.GameObject _evergreenGameObject;  // 0x18
        private HoudiniEngineUnity.HEU_HoudiniAsset _evergreenAsset;  // 0x20
        public System.Single _updateRate;  // 0x28
        public System.Single _scale;  // 0x2c

        // Methods
        // RVA: 0x09D53DCC  token: 0x600001D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x18
    public class HEU_AssetEventReceiverTest : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x09D36ED8  token: 0x600001E
        public System.Void ReloadCallback(HoudiniEngineUnity.HEU_ReloadEventData Data) { }
        // RVA: 0x09D36D74  token: 0x600001F
        public System.Void CookedCallback(HoudiniEngineUnity.HEU_CookedEventData Data) { }
        // RVA: 0x09D36C10  token: 0x6000020
        public System.Void BakedCallback(HoudiniEngineUnity.HEU_BakedEventData Data) { }
        // RVA: 0x05393520  token: 0x6000021
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x14
    public sealed struct CurveEditState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_Curve.CurveEditState INVALID;  // const
        public static HoudiniEngineUnity.HEU_Curve.CurveEditState GENERATED;  // const
        public static HoudiniEngineUnity.HEU_Curve.CurveEditState EDITING;  // const
        public static HoudiniEngineUnity.HEU_Curve.CurveEditState REQUIRES_GENERATION;  // const

    }

    // TypeToken: 0x2000026  // size: 0x14
    public sealed struct Interaction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_Curve.Interaction VIEW;  // const
        public static HoudiniEngineUnity.HEU_Curve.Interaction ADD;  // const
        public static HoudiniEngineUnity.HEU_Curve.Interaction EDIT;  // const

    }

    // TypeToken: 0x2000027  // size: 0x14
    public sealed struct CurveDrawCollision
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_Curve.CurveDrawCollision COLLIDERS;  // const
        public static HoudiniEngineUnity.HEU_Curve.CurveDrawCollision LAYERMASK;  // const

    }

    // TypeToken: 0x2000028  // size: 0x18
    public sealed class <>c__DisplayClass60_0
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Vector3> points;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000A1
        public System.Void .ctor() { }
        // RVA: 0x09D54290  token: 0x60000A2
        private System.Void <GetAllPoints>b__0(HoudiniEngineUnity.CurveNodeData transform) { }

    }

    // TypeToken: 0x2000029  // size: 0x40
    public sealed class <>c__DisplayClass87_0
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Vector3> positions;  // 0x10
        public System.Boolean hasRotations;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Quaternion> rotations;  // 0x20
        public System.Boolean hasScales;  // 0x28
        public System.Collections.Generic.List<UnityEngine.Vector3> scales;  // 0x30
        public System.Collections.Generic.List<System.Int32> curveCountIndices;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x60000A3
        public System.Void .ctor() { }
        // RVA: 0x09D54300  token: 0x60000A4
        private System.Void <UpdateCurveInputForCurveParts>b__0(HoudiniEngineUnity.CurveNodeData data) { }

    }

    // TypeToken: 0x200002A  // size: 0x28
    public sealed class <>c__DisplayClass88_0
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Vector3> positions;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Quaternion> rotations;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Vector3> scales;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60000A5
        public System.Void .ctor() { }
        // RVA: 0x09D54420  token: 0x60000A6
        private System.Void <UpdateCurveInputForCustomAttributes>b__0(HoudiniEngineUnity.CurveNodeData data) { }
        // RVA: 0x09D54510  token: 0x60000A7
        private System.Void <UpdateCurveInputForCustomAttributes>b__1(System.Int32 nIndex1, System.Int32 nIndex2, System.Single fCoeff, System.Int32 nInsertIndex) { }
        // RVA: 0x09D54800  token: 0x60000A8
        private System.Void <UpdateCurveInputForCustomAttributes>b__2(System.Int32 nIndex, System.Int32 nInsertIndex) { }

    }

    // TypeToken: 0x200002B  // size: 0x20
    public sealed class <>c__DisplayClass90_0
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Vector3> rotations;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Vector3> scales;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60000A9
        public System.Void .ctor() { }
        // RVA: 0x09D549F8  token: 0x60000AA
        private System.Void <UpdatePoints>b__0(HoudiniEngineUnity.CurveNodeData data) { }

    }

    // TypeToken: 0x2000030  // size: 0x14
    public sealed struct HEU_AssetType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType TYPE_INVALID;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType TYPE_HDA;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType TYPE_CURVE;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType TYPE_INPUT;  // const

    }

    // TypeToken: 0x2000031  // size: 0x14
    public sealed struct AssetBuildAction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction NONE;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction RELOAD;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction COOK;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction INVALID;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction STRIP_HEDATA;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction DUPLICATE;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction RESET_PARAMS;  // const

    }

    // TypeToken: 0x2000032  // size: 0x14
    public sealed struct AssetCookStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus NONE;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus COOKING;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus POSTCOOK;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus LOADING;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus POSTLOAD;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus PRELOAD;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus SELECT_SUBASSET;  // const

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct AssetCookResult
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult NONE;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult SUCCESS;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult ERRORED;  // const

    }

    // TypeToken: 0x2000034  // size: 0x80
    public sealed class UpdateUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x6000205
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x6000206
        public virtual System.Void Invoke() { }
        // RVA: 0x053DED94  token: 0x6000207
        public virtual System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000208
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct AssetInstantiationMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethod DEFAULT;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethod DUPLICATED;  // const
        public static HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethod UNDO;  // const

    }

    // TypeToken: 0x2000036  // size: 0x18
    public sealed class <>c__DisplayClass262_0
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.TransformData> previousTransformValues;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000209
        public System.Void .ctor() { }
        // RVA: 0x09D691FC  token: 0x600020A
        private System.Void <BakeToExistingPrefab>b__0(UnityEngine.Transform trans) { }

    }

    // TypeToken: 0x2000037  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.HEU_HoudiniAsset.<>c <>9;  // static @ 0x0
        public static System.Func<HoudiniEngineUnity.HEU_InputNode,System.Boolean> <>9__274_0;  // static @ 0x8
        public static System.Func<HoudiniEngineUnity.HEU_ObjectNode,System.Boolean> <>9__312_0;  // static @ 0x10
        public static System.Func<HoudiniEngineUnity.HEU_Curve,System.Boolean> <>9__335_0;  // static @ 0x18
        public static System.Func<HoudiniEngineUnity.HEU_AttributesStore,System.Boolean> <>9__336_0;  // static @ 0x20
        public static System.Func<HoudiniEngineUnity.HEU_Curve,System.Boolean> <>9__373_0;  // static @ 0x28
        public static System.Func<HoudiniEngineUnity.HEU_ObjectNode,System.Boolean> <>9__404_0;  // static @ 0x30
        public static System.Func<HoudiniEngineUnity.HEU_Curve,System.Boolean> <>9__404_1;  // static @ 0x38
        public static System.Func<HoudiniEngineUnity.HEU_MaterialData,System.Boolean> <>9__404_2;  // static @ 0x40

        // Methods
        // RVA: 0x09D695C0  token: 0x600020B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600020C
        public System.Void .ctor() { }
        // RVA: 0x09D69118  token: 0x600020D
        private System.Boolean <GetNonParameterInputNodes>b__274_0(HoudiniEngineUnity.HEU_InputNode node) { }
        // RVA: 0x09D690CC  token: 0x600020E
        private System.Boolean <DoPostCookWork>b__312_0(HoudiniEngineUnity.HEU_ObjectNode obj) { }
        // RVA: 0x09D691B0  token: 0x600020F
        private System.Boolean <UploadCurvesParameters>b__335_0(HoudiniEngineUnity.HEU_Curve curve) { }
        // RVA: 0x09D69164  token: 0x6000210
        private System.Boolean <UploadAttributeValues>b__336_0(HoudiniEngineUnity.HEU_AttributesStore store) { }
        // RVA: 0x09D68F84  token: 0x6000211
        private System.Boolean <ClearInvalidCurves>b__373_0(HoudiniEngineUnity.HEU_Curve curve) { }
        // RVA: 0x09D68FD0  token: 0x6000212
        private System.Boolean <ClearInvalidLists>b__404_0(HoudiniEngineUnity.HEU_ObjectNode node) { }
        // RVA: 0x09D6901C  token: 0x6000213
        private System.Boolean <ClearInvalidLists>b__404_1(HoudiniEngineUnity.HEU_Curve curve) { }
        // RVA: 0x09D69068  token: 0x6000214
        private System.Boolean <ClearInvalidLists>b__404_2(HoudiniEngineUnity.HEU_MaterialData data) { }

    }

    // TypeToken: 0x2000038  // size: 0x28
    public sealed class <>c__DisplayClass405_0
    {
        // Fields
        public HoudiniEngineUnity.HEU_HoudiniAsset newAsset;  // 0x10
        public System.Int32 i;  // 0x18
        public System.Predicate<HoudiniEngineUnity.HEU_Curve> <>9__0;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000215
        public System.Void .ctor() { }
        // RVA: 0x09D692B4  token: 0x6000216
        private System.Boolean <CopyPropertiesTo>b__0(HoudiniEngineUnity.HEU_Curve curve) { }

    }

    // TypeToken: 0x200003B  // size: 0x14
    public sealed struct InputNodeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_InputNode.InputNodeType CONNECTION;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputNodeType NODE;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputNodeType PARAMETER;  // const

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct InputObjectType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_InputNode.InputObjectType HDA;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputObjectType UNITY_MESH;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputObjectType CURVE;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputObjectType TERRAIN;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputObjectType BOUNDING_BOX;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputObjectType TILEMAP;  // const

    }

    // TypeToken: 0x200003D  // size: 0x14
    public sealed struct InternalObjectType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_InputNode.InternalObjectType UNKNOWN;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InternalObjectType HDA;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InternalObjectType UNITY_MESH;  // const

    }

    // TypeToken: 0x200003E  // size: 0x14
    public sealed struct InputActions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_InputNode.InputActions ACTION;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputActions DELETE;  // const
        public static HoudiniEngineUnity.HEU_InputNode.InputActions INSERT;  // const

    }

    // TypeToken: 0x2000042  // size: 0x10
    public class HEU_InputObjectUICache
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600027C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x10
    public class HEU_InputAssetUICache
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600027D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x14
    public sealed struct PartOutputType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_PartData.PartOutputType NONE;  // const
        public static HoudiniEngineUnity.HEU_PartData.PartOutputType MESH;  // const
        public static HoudiniEngineUnity.HEU_PartData.PartOutputType VOLUME;  // const
        public static HoudiniEngineUnity.HEU_PartData.PartOutputType CURVE;  // const
        public static HoudiniEngineUnity.HEU_PartData.PartOutputType INSTANCER;  // const

    }

    // TypeToken: 0x200004A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.HEU_PartData.<>c <>9;  // static @ 0x0
        public static System.Comparison<UnityEngine.LOD> <>9__96_0;  // static @ 0x8

        // Methods
        // RVA: 0x09D6955C  token: 0x6000302
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000303
        public System.Void .ctor() { }
        // RVA: 0x09D690B4  token: 0x6000304
        private System.Int32 <CopyGameObjectComponents>b__96_0(UnityEngine.LOD a, UnityEngine.LOD b) { }

    }

    // TypeToken: 0x200004B  // size: 0x18
    public sealed class <>c__DisplayClass97_0
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.TransformData> previousTransformValues;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000305
        public System.Void .ctor() { }
        // RVA: 0x09D69334  token: 0x6000306
        private System.Void <CopyChildGameObjects>b__0(UnityEngine.Transform trans) { }

    }

    // TypeToken: 0x200004C  // size: 0x18
    public sealed class <>c__DisplayClass99_0
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.TransformData> previousTransformValues;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000307
        public System.Void .ctor() { }
        // RVA: 0x09D693EC  token: 0x6000308
        private System.Void <BakePartToGameObject>b__0(UnityEngine.Transform trans) { }

    }

    // TypeToken: 0x200004D  // size: 0x18
    public sealed class <>c__DisplayClass99_1
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.TransformData> previousTransformValues;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000309
        public System.Void .ctor() { }
        // RVA: 0x09D694A4  token: 0x600030A
        private System.Void <BakePartToGameObject>b__1(UnityEngine.Transform trans) { }

    }

    // TypeToken: 0x200004F  // size: 0x14
    public sealed struct AttributeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeType UNDEFINED;  // const
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeType BOOL;  // const
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeType INT;  // const
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeType FLOAT;  // const
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeType STRING;  // const
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeType MAX;  // const

    }

    // TypeToken: 0x2000050  // size: 0x14
    public sealed struct AttributeState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeState INVALID;  // const
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeState SYNCED;  // const
        public static HoudiniEngineUnity.HEU_AttributeData.AttributeState LOCAL_DIRTY;  // const

    }

    // TypeToken: 0x2000052  // size: 0x80
    public sealed class SetAttributeValueFunc : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09D8518C  token: 0x6000342
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014DD080  token: 0x6000343
        public virtual System.Void Invoke(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D850C0  token: 0x6000344
        public virtual System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000345
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000057  // size: 0x14
    public sealed struct HEU_ImportAssetOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions Default;  // const
        public static HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions ForceUpdate;  // const
        public static HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions ForceSynchronousImport;  // const
        public static HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions ImportRecursive;  // const
        public static HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions DontDownloadFromCacheServer;  // const
        public static HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions ForceUncompressedImport;  // const

    }

    // TypeToken: 0x200005D  // size: 0x14
    public sealed struct RegSAM
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM QueryValue;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM SetValue;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM CreateSubKey;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM EnumerateSubKeys;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM Notify;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM CreateLink;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM WOW64_32Key;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM WOW64_64Key;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM WOW64_Res;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM Read;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM Write;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM Execute;  // const
        public static HoudiniEngineUnity.HEU_PlatformWin.RegSAM AllAccess;  // const

    }

    // TypeToken: 0x2000060  // size: 0x14
    public sealed struct DataType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_PluginStorage.DataType BOOL;  // const
        public static HoudiniEngineUnity.HEU_PluginStorage.DataType INT;  // const
        public static HoudiniEngineUnity.HEU_PluginStorage.DataType LONG;  // const
        public static HoudiniEngineUnity.HEU_PluginStorage.DataType FLOAT;  // const
        public static HoudiniEngineUnity.HEU_PluginStorage.DataType STRING;  // const

    }

    // TypeToken: 0x2000061  // size: 0x20
    public class StoreData
    {
        // Fields
        public HoudiniEngineUnity.HEU_PluginStorage.DataType _type;  // 0x10
        public System.String _valueStr;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60004B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000062
    public class StoreDataArray`1
    {
        // Fields
        public T[] _array;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60004B7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x14
    public sealed struct HEU_HandleType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_Handle.HEU_HandleType XFORM;  // const
        public static HoudiniEngineUnity.HEU_Handle.HEU_HandleType UNSUPPORTED;  // const

    }

    // TypeToken: 0x2000066  // size: 0x14
    public sealed struct HEU_HandleParamType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamType TRANSLATE;  // const
        public static HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamType ROTATE;  // const
        public static HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamType SCALE;  // const

    }

    // TypeToken: 0x20000C7  // size: 0x14
    public sealed struct ModifierAction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction MULTIPARM_INSERT;  // const
        public static HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction MULTIPARM_REMOVE;  // const
        public static HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction MULTIPARM_CLEAR;  // const
        public static HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction SET_FLOAT;  // const
        public static HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction SET_INT;  // const

    }

    // TypeToken: 0x20000C9  // size: 0x20
    public sealed class <>c__DisplayClass82_0
    {
        // Fields
        public System.Int32 parentID;  // 0x10
        public System.Predicate<HoudiniEngineUnity.HAPI_ParmInfo> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600059A
        public System.Void .ctor() { }
        // RVA: 0x0702DACC  token: 0x600059B
        private System.Boolean <Initialize>b__0(HoudiniEngineUnity.HAPI_ParmInfo p) { }

    }

    // TypeToken: 0x20000CA  // size: 0x20
    public sealed class <>c__DisplayClass83_0
    {
        // Fields
        public System.Int32 parentID;  // 0x10
        public System.Predicate<HoudiniEngineUnity.HAPI_ParmInfo> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600059C
        public System.Void .ctor() { }
        // RVA: 0x0702DACC  token: 0x600059D
        private System.Boolean <Initialize4HGTool>b__0(HoudiniEngineUnity.HAPI_ParmInfo p) { }

    }

    // TypeToken: 0x20000CF  // size: 0x14
    public sealed struct LinkState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_PDGAssetLink.LinkState INACTIVE;  // const
        public static HoudiniEngineUnity.HEU_PDGAssetLink.LinkState LINKING;  // const
        public static HoudiniEngineUnity.HEU_PDGAssetLink.LinkState LINKED;  // const
        public static HoudiniEngineUnity.HEU_PDGAssetLink.LinkState ERROR_NOT_LINKED;  // const

    }

    // TypeToken: 0x20000D0  // size: 0x80
    public sealed class UpdateUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x6000602
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x6000603
        public virtual System.Void Invoke() { }
        // RVA: 0x053DED94  token: 0x6000604
        public virtual System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000605
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000D1  // size: 0x28
    public sealed class <>c__DisplayClass107_0
    {
        // Fields
        public HoudiniEngineUnity.HEU_PDGAssetLink <>4__this;  // 0x10
        public System.Action<HoudiniEngineUnity.HEU_TOPNodeData,HoudiniEngineUnity.HEU_SyncedEventData> OnSynced;  // 0x18
        public HoudiniEngineUnity.HEU_TOPNodeData topNode;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000606
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D2  // size: 0x20
    public sealed class <>c__DisplayClass107_1
    {
        // Fields
        public HoudiniEngineUnity.HEU_GeoSync geoSync;  // 0x10
        public HoudiniEngineUnity.HEU_PDGAssetLink.<>c__DisplayClass107_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000607
        public System.Void .ctor() { }
        // RVA: 0x09D9455C  token: 0x6000608
        private System.Void <LoadResults>b__0(HoudiniEngineUnity.HEU_SyncedEventData Data) { }

    }

    // TypeToken: 0x20000D6  // size: 0x14
    public sealed struct PDGState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_TOPNodeData.PDGState NONE;  // const
        public static HoudiniEngineUnity.HEU_TOPNodeData.PDGState DIRTIED;  // const
        public static HoudiniEngineUnity.HEU_TOPNodeData.PDGState DIRTYING;  // const
        public static HoudiniEngineUnity.HEU_TOPNodeData.PDGState COOKING;  // const
        public static HoudiniEngineUnity.HEU_TOPNodeData.PDGState COOK_COMPLETE;  // const
        public static HoudiniEngineUnity.HEU_TOPNodeData.PDGState COOK_FAILED;  // const

    }

    // TypeToken: 0x20000DA  // size: 0x80
    public sealed class OnWorkItemLoadResultsDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE0AC  token: 0x600063E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600063F
        public virtual System.Void Invoke(HoudiniEngineUnity.HEU_SyncedEventData OnSynced) { }
        // RVA: 0x047FDD70  token: 0x6000640
        public virtual System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_SyncedEventData OnSynced, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000641
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000DB  // size: 0x14
    public sealed struct EventMessageColor
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_PDGSession.EventMessageColor DEFAULT;  // const
        public static HoudiniEngineUnity.HEU_PDGSession.EventMessageColor WARNING;  // const
        public static HoudiniEngineUnity.HEU_PDGSession.EventMessageColor ERROR;  // const

    }

    // TypeToken: 0x20000DC  // size: 0x28
    public sealed class <>c__DisplayClass35_0
    {
        // Fields
        public System.Int32[] allNetworkNodeIds;  // 0x10
        public System.Int32 idx;  // 0x18
        public System.Predicate<System.Int32> <>9__0;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000642
        public System.Void .ctor() { }
        // RVA: 0x09D94634  token: 0x6000643
        private System.Boolean <GetNonBypassedNetworkIds>b__0(System.Int32 id) { }

    }

    // TypeToken: 0x20000F6  // size: 0x14
    public sealed struct Source
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_MaterialData.Source DEFAULT;  // const
        public static HoudiniEngineUnity.HEU_MaterialData.Source HOUDINI;  // const
        public static HoudiniEngineUnity.HEU_MaterialData.Source UNITY;  // const
        public static HoudiniEngineUnity.HEU_MaterialData.Source SUBSTANCE;  // const

    }

    // TypeToken: 0x20000FF  // size: 0x80
    public sealed class CreateSessionFromTypeDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0835376C  token: 0x6000931
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x6000932
        public virtual HoudiniEngineUnity.HEU_SessionBase Invoke(System.Type type) { }
        // RVA: 0x047FDD70  token: 0x6000933
        public virtual System.IAsyncResult BeginInvoke(System.Type type, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x6000934
        public virtual HoudiniEngineUnity.HEU_SessionBase EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000102  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_SessionSyncData.Status Stopped;  // const
        public static HoudiniEngineUnity.HEU_SessionSyncData.Status Started;  // const
        public static HoudiniEngineUnity.HEU_SessionSyncData.Status Connecting;  // const
        public static HoudiniEngineUnity.HEU_SessionSyncData.Status Initializing;  // const
        public static HoudiniEngineUnity.HEU_SessionSyncData.Status Connected;  // const

    }

    // TypeToken: 0x2000104  // size: 0x80
    public sealed class AssetSyncCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE0AC  token: 0x6000941
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000942
        public virtual System.Void Invoke(HoudiniEngineUnity.HEU_AssetSync assetSync) { }
        // RVA: 0x047FDD70  token: 0x6000943
        public virtual System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_AssetSync assetSync, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000944
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200010B  // size: 0x14
    public sealed struct TaskStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_Task.TaskStatus NONE;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskStatus PENDING_START;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskStatus STARTED;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskStatus REQUIRE_UPDATE;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskStatus PENDING_COMPLETE;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskStatus COMPLETED;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskStatus UNUSED;  // const

    }

    // TypeToken: 0x200010C  // size: 0x14
    public sealed struct TaskResult
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_Task.TaskResult NONE;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskResult SUCCESS;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskResult FAILED;  // const
        public static HoudiniEngineUnity.HEU_Task.TaskResult KILLED;  // const

    }

    // TypeToken: 0x200010D  // size: 0x80
    public sealed class TaskCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE0AC  token: 0x600097E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600097F
        public virtual System.Void Invoke(HoudiniEngineUnity.HEU_Task task) { }
        // RVA: 0x047FDD70  token: 0x6000980
        public virtual System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_Task task, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000981
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200010F  // size: 0x14
    public sealed struct BuildType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AssetTask.BuildType NONE;  // const
        public static HoudiniEngineUnity.HEU_AssetTask.BuildType LOAD;  // const
        public static HoudiniEngineUnity.HEU_AssetTask.BuildType COOK;  // const
        public static HoudiniEngineUnity.HEU_AssetTask.BuildType RELOAD;  // const

    }

    // TypeToken: 0x2000118  // size: 0x14
    public sealed struct LoadType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadType FILE;  // const
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadType NODE;  // const
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadType ASSET;  // const

    }

    // TypeToken: 0x200011A  // size: 0x14
    public sealed struct LoadStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus NONE;  // const
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus STARTED;  // const
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus SUCCESS;  // const
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus ERROR;  // const

    }

    // TypeToken: 0x2000119  // size: 0x38
    public class HEU_LoadData
    {
        // Fields
        public System.Int32 _cookNodeID;  // 0x10
        public HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus _loadStatus;  // 0x14
        public System.Text.StringBuilder _logStr;  // 0x18
        public HoudiniEngineUnity.HEU_SessionBase _session;  // 0x20
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadObject> _loadedObjects;  // 0x28
        public System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_LoadBufferBase> _idBuffersMap;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60009D5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011B  // size: 0x30
    public class HEU_LoadObject
    {
        // Fields
        public System.Int32 _objectNodeID;  // 0x10
        public System.Int32 _displayNodeID;  // 0x14
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume> _terrainBuffers;  // 0x18
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferMesh> _meshBuffers;  // 0x20
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferInstancer> _instancerBuffers;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x60009D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011C  // size: 0x14
    public sealed struct HEU_LoadCallbackType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackType PRECOOK;  // const
        public static HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackType POSTCOOK;  // const

    }

    // TypeToken: 0x200011D  // size: 0x80
    public sealed class HEU_LoadCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09738878  token: 0x60009D7
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x60009D8
        public virtual System.Void Invoke(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackType callbackType) { }
        // RVA: 0x09DA34AC  token: 0x60009D9
        public virtual System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackType callbackType, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60009DA
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000121  // size: 0x14
    public sealed struct HEU_ReplacePrefabOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptions Default;  // const
        public static HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptions ConnectToPrefab;  // const
        public static HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptions ReplaceNameBased;  // const

    }

    // TypeToken: 0x2000127
    public sealed class GetArray1ArgDel`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000A9E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000A9F
        public virtual System.Boolean Invoke(System.Int32 arg1, T[] data, System.Int32 start, System.Int32 length) { }
        // RVA: -1  // runtime  token: 0x6000AA0
        public virtual System.IAsyncResult BeginInvoke(System.Int32 arg1, T[] data, System.Int32 start, System.Int32 length, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000AA1
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000128
    public sealed class GetArray2ArgDel`2 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000AA2
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000AA3
        public virtual System.Boolean Invoke(System.Int32 arg1, ARG2 arg2, T[] data, System.Int32 start, System.Int32 length) { }
        // RVA: -1  // runtime  token: 0x6000AA4
        public virtual System.IAsyncResult BeginInvoke(System.Int32 arg1, ARG2 arg2, T[] data, System.Int32 start, System.Int32 length, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000AA5
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000129
    public sealed class GetArray3ArgDel`3 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000AA6
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000AA7
        public virtual System.Boolean Invoke(System.Int32 arg1, ARG2 arg2, ARG3 arg3, T[] data, System.Int32 start, System.Int32 length) { }
        // RVA: -1  // runtime  token: 0x6000AA8
        public virtual System.IAsyncResult BeginInvoke(System.Int32 arg1, ARG2 arg2, ARG3 arg3, T[] data, System.Int32 start, System.Int32 length, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000AA9
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200012A
    public sealed class GetAttributeArrayInputFunc`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000AAA
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000AAB
        public virtual System.Boolean Invoke(System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[] items, System.Int32 start, System.Int32 end) { }
        // RVA: -1  // runtime  token: 0x6000AAC
        public virtual System.IAsyncResult BeginInvoke(System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[] items, System.Int32 start, System.Int32 end, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000AAD
        public virtual System.Boolean EndInvoke(HoudiniEngineUnity.HAPI_AttributeInfo& info, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200012B
    public sealed class SetAttributeArrayFunc`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000AAE
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000AAF
        public virtual System.Boolean Invoke(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, System.Int32 start, System.Int32 end) { }
        // RVA: -1  // runtime  token: 0x6000AB0
        public virtual System.IAsyncResult BeginInvoke(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, System.Int32 start, System.Int32 end, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000AB1
        public virtual System.Boolean EndInvoke(HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200012F  // size: 0x14
    public sealed struct ColliderType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType NONE;  // const
        public static HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType BOX;  // const
        public static HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType SPHERE;  // const
        public static HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType MESH;  // const
        public static HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType SIMPLE_BOX;  // const
        public static HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType SIMPLE_SPHERE;  // const
        public static HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType SIMPLE_CAPSULE;  // const

    }

    // TypeToken: 0x200012E  // size: 0x58
    public class HEU_ColliderInfo
    {
        // Fields
        public HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType _colliderType;  // 0x10
        public UnityEngine.Vector3 _colliderCenter;  // 0x14
        public UnityEngine.Vector3 _colliderSize;  // 0x20
        public System.Single _colliderRadius;  // 0x2c
        public System.Boolean _convexCollider;  // 0x30
        public System.String _collisionGroupName;  // 0x38
        public UnityEngine.Vector3[] _collisionVertices;  // 0x40
        public System.Int32[] _collisionIndices;  // 0x48
        public UnityEngine.MeshTopology _meshTopology;  // 0x50
        public System.Boolean _isTrigger;  // 0x54

        // Methods
        // RVA: 0x041E1670  token: 0x6000ACE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000130  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.HEU_GenerateGeoCache.<>c <>9;  // static @ 0x0
        public static System.Predicate<UnityEngine.Material> <>9__64_0;  // static @ 0x8
        public static System.Comparison<System.Single> <>9__66_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DB8710  token: 0x6000ACF
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000AD0
        public System.Void .ctor() { }
        // RVA: 0x09DB86C4  token: 0x6000AD1
        private System.Boolean <GetFinalMaterialsFromComparingNewWithPrevious>b__64_0(UnityEngine.Material material) { }
        // RVA: 0x09DB86A8  token: 0x6000AD2
        private System.Int32 <GenerateLODMeshesFromGeoGroups>b__66_0(System.Single a, System.Single b) { }

    }

    // TypeToken: 0x2000138  // size: 0x28
    public class HEU_InputDataMeshes : HoudiniEngineUnity.HEU_InputData
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMesh> _inputMeshes;  // 0x18
        public System.Boolean _hasLOD;  // 0x20

        // Methods
        // RVA: 0x09DA9E10  token: 0x6000AF5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000139  // size: 0x14
    public sealed struct HEU_InputColliderType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderType NONE;  // const
        public static HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderType BOX;  // const
        public static HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderType SPHERE;  // const
        public static HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderType CAPSULE;  // const
        public static HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderType MESH;  // const

    }

    // TypeToken: 0x200013A  // size: 0x20
    public class HEU_InputDataCollider
    {
        // Fields
        public UnityEngine.Collider _collider;  // 0x10
        public HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderType _colliderType;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000AF6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013B  // size: 0x60
    public class HEU_InputDataMesh
    {
        // Fields
        public UnityEngine.Mesh _mesh;  // 0x10
        public UnityEngine.Material[] _materials;  // 0x18
        public System.String _meshPath;  // 0x20
        public System.String _meshName;  // 0x28
        public System.Int32 _numVertices;  // 0x30
        public System.Int32 _numSubMeshes;  // 0x34
        public System.UInt32[] _indexStart;  // 0x38
        public System.UInt32[] _indexCount;  // 0x40
        public System.Single _LODScreenTransition;  // 0x48
        public UnityEngine.Transform _transform;  // 0x50
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataCollider> _colliders;  // 0x58

        // Methods
        // RVA: 0x041E1670  token: 0x6000AF7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013E  // size: 0x30
    public class HEU_InputDataSpline
    {
        // Fields
        public UnityEngine.Splines.Spline _spline;  // 0x10
        public System.Boolean _closed;  // 0x18
        public System.Int32 _count;  // 0x1c
        public System.Single _length;  // 0x20
        public UnityEngine.Splines.BezierKnot[] _knots;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000B01
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013F  // size: 0x28
    public class HEU_InputDataSplineContainer : HoudiniEngineUnity.HEU_InputData
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterfaceSpline.HEU_InputDataSpline> _inputSplines;  // 0x18
        public UnityEngine.Transform _transform;  // 0x20

        // Methods
        // RVA: 0x09DA9E88  token: 0x6000B02
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000141  // size: 0x80
    public class HEU_InputDataTerrain : HoudiniEngineUnity.HEU_InputData
    {
        // Fields
        public System.String _heightFieldName;  // 0x18
        public System.Int32 _parentNodeID;  // 0x20
        public System.Single _voxelSize;  // 0x24
        public UnityEngine.Terrain _terrain;  // 0x28
        public UnityEngine.TerrainData _terrainData;  // 0x30
        public System.Int32 _numPointsX;  // 0x38
        public System.Int32 _numPointsY;  // 0x3c
        public HoudiniEngineUnity.HAPI_Transform _transform;  // 0x40
        public System.Single _heightScale;  // 0x68
        public System.Int32 _heightfieldNodeID;  // 0x6c
        public System.Int32 _heightNodeID;  // 0x70
        public System.Int32 _maskNodeID;  // 0x74
        public System.Int32 _mergeNodeID;  // 0x78

        // Methods
        // RVA: 0x09DA9F00  token: 0x6000B10
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000144  // size: 0x28
    public class HEU_InputDataTilemap : HoudiniEngineUnity.HEU_InputData
    {
        // Fields
        public UnityEngine.Tilemaps.Tilemap _tilemap;  // 0x18
        public UnityEngine.Transform _transform;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000B18
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200014E  // size: 0x14
    public sealed struct ToolType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ShelfToolData.ToolType GENERATOR;  // const
        public static HoudiniEngineUnity.HEU_ShelfToolData.ToolType OPERATOR_SINGLE;  // const
        public static HoudiniEngineUnity.HEU_ShelfToolData.ToolType OPERATOR_MULTI;  // const
        public static HoudiniEngineUnity.HEU_ShelfToolData.ToolType BATCH;  // const

    }

    // TypeToken: 0x2000151  // size: 0x18
    public sealed class <>c__DisplayClass19_0
    {
        // Fields
        public System.String shelfName;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000B79
        public System.Void .ctor() { }
        // RVA: 0x06CC7538  token: 0x6000B7A
        private System.Boolean <AddShelf>b__0(HoudiniEngineUnity.HEU_Shelf shelf) { }

    }

    // TypeToken: 0x2000156  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.JSONNode.Enumerator.Type None;  // const
        public static HoudiniEngineUnity.JSONNode.Enumerator.Type Array;  // const
        public static HoudiniEngineUnity.JSONNode.Enumerator.Type Object;  // const

    }

    // TypeToken: 0x2000155  // size: 0x58
    public sealed struct Enumerator
    {
        // Fields
        private HoudiniEngineUnity.JSONNode.Enumerator.Type type;  // 0x10
        private System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> m_Object;  // 0x18
        private System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> m_Array;  // 0x40

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x068B8E74 */ }
        System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode> Current { get; /* RVA: 0x09DB8818 */ }

        // Methods
        // RVA: 0x09DB87E4  token: 0x6000BDF
        public System.Void .ctor(System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> aArrayEnum) { }
        // RVA: 0x04B0C3F0  token: 0x6000BE0
        public System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> aDictEnum) { }
        // RVA: 0x09DB8774  token: 0x6000BE2
        public System.Boolean MoveNext() { }

    }

    // TypeToken: 0x2000157  // size: 0x58
    public sealed struct ValueEnumerator
    {
        // Fields
        private HoudiniEngineUnity.JSONNode.Enumerator m_Enumerator;  // 0x10

        // Properties
        HoudiniEngineUnity.JSONNode Current { get; /* RVA: 0x09DCBB70 */ }

        // Methods
        // RVA: 0x09DC2BA0  token: 0x6000BE3
        public System.Void .ctor(System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> aArrayEnum) { }
        // RVA: 0x09DC2ACC  token: 0x6000BE4
        public System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> aDictEnum) { }
        // RVA: 0x09DC2B6C  token: 0x6000BE5
        public System.Void .ctor(HoudiniEngineUnity.JSONNode.Enumerator aEnumerator) { }
        // RVA: 0x09DC2AC4  token: 0x6000BE7
        public System.Boolean MoveNext() { }
        // RVA: 0x04DA2D60  token: 0x6000BE8
        public HoudiniEngineUnity.JSONNode.ValueEnumerator GetEnumerator() { }

    }

    // TypeToken: 0x2000158  // size: 0x58
    public sealed struct KeyEnumerator
    {
        // Fields
        private HoudiniEngineUnity.JSONNode.Enumerator m_Enumerator;  // 0x10

        // Properties
        System.String Current { get; /* RVA: 0x09DC2C38 */ }

        // Methods
        // RVA: 0x09DC2BA0  token: 0x6000BE9
        public System.Void .ctor(System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> aArrayEnum) { }
        // RVA: 0x09DC2ACC  token: 0x6000BEA
        public System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> aDictEnum) { }
        // RVA: 0x09DC2B6C  token: 0x6000BEB
        public System.Void .ctor(HoudiniEngineUnity.JSONNode.Enumerator aEnumerator) { }
        // RVA: 0x09DC2AC4  token: 0x6000BED
        public System.Boolean MoveNext() { }
        // RVA: 0x04DA2D60  token: 0x6000BEE
        public HoudiniEngineUnity.JSONNode.KeyEnumerator GetEnumerator() { }

    }

    // TypeToken: 0x2000159  // size: 0x60
    public class LinqEnumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private HoudiniEngineUnity.JSONNode m_Node;  // 0x10
        private HoudiniEngineUnity.JSONNode.Enumerator m_Enumerator;  // 0x18

        // Properties
        System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode> Current { get; /* RVA: 0x09DC2EC8 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x09DC2DF4 */ }

        // Methods
        // RVA: 0x09DC2E4C  token: 0x6000BEF
        private System.Void .ctor(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DC2D18  token: 0x6000BF2
        public virtual System.Boolean MoveNext() { }
        // RVA: 0x09DC2C78  token: 0x6000BF3
        public virtual System.Void Dispose() { }
        // RVA: 0x09DC2CB4  token: 0x6000BF4
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode>> GetEnumerator() { }
        // RVA: 0x09DC2D24  token: 0x6000BF5
        public virtual System.Void Reset() { }
        // RVA: 0x09DC2D90  token: 0x6000BF6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200015A  // size: 0x28
    public sealed class <get_Children>d__40 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private HoudiniEngineUnity.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20

        // Properties
        HoudiniEngineUnity.JSONNode System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x6000BF7
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000BF8
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04D83CA0  token: 0x6000BF9
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DCB4CC  token: 0x6000BFB
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x09DCB44C  token: 0x6000BFD
        private virtual System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator() { }
        // RVA: 0x09DCB4C4  token: 0x6000BFE
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200015B  // size: 0x40
    public sealed class <get_DeepChildren>d__42 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private HoudiniEngineUnity.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public HoudiniEngineUnity.JSONNode <>4__this;  // 0x28
        private System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> <>7__wrap1;  // 0x30
        private System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> <>7__wrap2;  // 0x38

        // Properties
        HoudiniEngineUnity.JSONNode System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x6000BFF
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x09DCB9F0  token: 0x6000C00
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DCB518  token: 0x6000C01
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DCBADC  token: 0x6000C02
        private System.Void <>m__Finally1() { }
        // RVA: 0x09DCBB24  token: 0x6000C03
        private System.Void <>m__Finally2() { }
        // RVA: 0x09DCB9A4  token: 0x6000C05
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x09DCB910  token: 0x6000C07
        private virtual System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator() { }
        // RVA: 0x09DCB99C  token: 0x6000C08
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200015D  // size: 0x48
    public sealed class <get_Children>d__22 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private HoudiniEngineUnity.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public HoudiniEngineUnity.JSONArray <>4__this;  // 0x28
        private System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> <>7__wrap1;  // 0x30

        // Properties
        HoudiniEngineUnity.JSONNode System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x6000C19
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x09DCAF90  token: 0x6000C1A
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DCAC68  token: 0x6000C1B
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DCB008  token: 0x6000C1C
        private System.Void <>m__Finally1() { }
        // RVA: 0x09DCAF44  token: 0x6000C1E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x09DCAEB0  token: 0x6000C20
        private virtual System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator() { }
        // RVA: 0x09DCAF3C  token: 0x6000C21
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200015F  // size: 0x18
    public sealed class <>c__DisplayClass21_0
    {
        // Fields
        public HoudiniEngineUnity.JSONNode aNode;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000C33
        public System.Void .ctor() { }
        // RVA: 0x09DCA518  token: 0x6000C34
        private System.Boolean <Remove>b__0(System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode> k) { }

    }

    // TypeToken: 0x2000160  // size: 0x58
    public sealed class <get_Children>d__23 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private HoudiniEngineUnity.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public HoudiniEngineUnity.JSONObject <>4__this;  // 0x28
        private System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> <>7__wrap1;  // 0x30

        // Properties
        HoudiniEngineUnity.JSONNode System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x6000C35
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x09DCB398  token: 0x6000C36
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DCB044  token: 0x6000C37
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DCB410  token: 0x6000C38
        private System.Void <>m__Finally1() { }
        // RVA: 0x09DCB34C  token: 0x6000C3A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x09DCB2B8  token: 0x6000C3C
        private virtual System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator() { }
        // RVA: 0x09DCB344  token: 0x6000C3D
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200016C
    public class RequireStruct`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000CBE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200016D
    public class RequireClass`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000CBF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000180  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_LODGroup_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.LODGroup,HoudiniEngineUnity.Test_LODGroup> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.LODGroup,HoudiniEngineUnity.Test_LODGroup> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCAB3C  token: 0x6000CDE
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000CDF
        public System.Void .ctor() { }
        // RVA: 0x09DC991C  token: 0x6000CE0
        private HoudiniEngineUnity.Test_LODGroup <ToTestObject>b__1_0(UnityEngine.LODGroup lod) { }
        // RVA: 0x09DC9EBC  token: 0x6000CE1
        private HoudiniEngineUnity.Test_LODGroup <ToTestObject>b__2_0(UnityEngine.LODGroup lod) { }

    }

    // TypeToken: 0x2000183  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_Transform_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.Transform,HoudiniEngineUnity.Test_Transform> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.Transform,HoudiniEngineUnity.Test_Transform> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA628  token: 0x6000CE8
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000CE9
        public System.Void .ctor() { }
        // RVA: 0x09DC99DC  token: 0x6000CEA
        private HoudiniEngineUnity.Test_Transform <ToTestObject>b__1_0(UnityEngine.Transform lod) { }
        // RVA: 0x09DCA398  token: 0x6000CEB
        private HoudiniEngineUnity.Test_Transform <ToTestObject>b__2_0(UnityEngine.Transform lod) { }

    }

    // TypeToken: 0x2000186  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_Material_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.Material,HoudiniEngineUnity.Test_Material> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.Material,HoudiniEngineUnity.Test_Material> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA68C  token: 0x6000CF2
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000CF3
        public System.Void .ctor() { }
        // RVA: 0x09DC9AFC  token: 0x6000CF4
        private HoudiniEngineUnity.Test_Material <ToTestObject>b__1_0(UnityEngine.Material lod) { }
        // RVA: 0x09DC9F7C  token: 0x6000CF5
        private HoudiniEngineUnity.Test_Material <ToTestObject>b__2_0(UnityEngine.Material lod) { }

    }

    // TypeToken: 0x2000189  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_Collider_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.Collider,HoudiniEngineUnity.Test_Collider> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.Collider,HoudiniEngineUnity.Test_Collider> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA754  token: 0x6000CFC
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000CFD
        public System.Void .ctor() { }
        // RVA: 0x09DC9860  token: 0x6000CFE
        private HoudiniEngineUnity.Test_Collider <ToTestObject>b__1_0(UnityEngine.Collider lod) { }
        // RVA: 0x09DCA09C  token: 0x6000CFF
        private HoudiniEngineUnity.Test_Collider <ToTestObject>b__2_0(UnityEngine.Collider lod) { }

    }

    // TypeToken: 0x200018C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_BoxCollider_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.BoxCollider,HoudiniEngineUnity.Test_BoxCollider> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.BoxCollider,HoudiniEngineUnity.Test_BoxCollider> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA560  token: 0x6000D06
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D07
        public System.Void .ctor() { }
        // RVA: 0x09DC997C  token: 0x6000D08
        private HoudiniEngineUnity.Test_BoxCollider <ToTestObject>b__1_0(UnityEngine.BoxCollider lod) { }
        // RVA: 0x09DCA4B8  token: 0x6000D09
        private HoudiniEngineUnity.Test_BoxCollider <ToTestObject>b__2_0(UnityEngine.BoxCollider lod) { }

    }

    // TypeToken: 0x200018F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_SphereCollider_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.SphereCollider,HoudiniEngineUnity.Test_SphereCollider> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.SphereCollider,HoudiniEngineUnity.Test_SphereCollider> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA948  token: 0x6000D10
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D11
        public System.Void .ctor() { }
        // RVA: 0x09DC9D3C  token: 0x6000D12
        private HoudiniEngineUnity.Test_SphereCollider <ToTestObject>b__1_0(UnityEngine.SphereCollider lod) { }
        // RVA: 0x09DCA1BC  token: 0x6000D13
        private HoudiniEngineUnity.Test_SphereCollider <ToTestObject>b__2_0(UnityEngine.SphereCollider lod) { }

    }

    // TypeToken: 0x2000192  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_CapsuleCollider_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.CapsuleCollider,HoudiniEngineUnity.Test_CapsuleCollider> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.CapsuleCollider,HoudiniEngineUnity.Test_CapsuleCollider> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA6F0  token: 0x6000D1A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D1B
        public System.Void .ctor() { }
        // RVA: 0x09DC9A9C  token: 0x6000D1C
        private HoudiniEngineUnity.Test_CapsuleCollider <ToTestObject>b__1_0(UnityEngine.CapsuleCollider lod) { }
        // RVA: 0x09DCA15C  token: 0x6000D1D
        private HoudiniEngineUnity.Test_CapsuleCollider <ToTestObject>b__2_0(UnityEngine.CapsuleCollider lod) { }

    }

    // TypeToken: 0x2000195  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_MeshCollider_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.MeshCollider,HoudiniEngineUnity.Test_MeshCollider> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.MeshCollider,HoudiniEngineUnity.Test_MeshCollider> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCAA10  token: 0x6000D24
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D25
        public System.Void .ctor() { }
        // RVA: 0x09DC9800  token: 0x6000D26
        private HoudiniEngineUnity.Test_MeshCollider <ToTestObject>b__1_0(UnityEngine.MeshCollider lod) { }
        // RVA: 0x09DC9FDC  token: 0x6000D27
        private HoudiniEngineUnity.Test_MeshCollider <ToTestObject>b__2_0(UnityEngine.MeshCollider lod) { }

    }

    // TypeToken: 0x2000198  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_Mesh_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.Mesh,HoudiniEngineUnity.Test_Mesh> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.Mesh,HoudiniEngineUnity.Test_Mesh> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA8E4  token: 0x6000D2E
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D2F
        public System.Void .ctor() { }
        // RVA: 0x09DC9B5C  token: 0x6000D30
        private HoudiniEngineUnity.Test_Mesh <ToTestObject>b__1_0(UnityEngine.Mesh lod) { }
        // RVA: 0x09DC9E5C  token: 0x6000D31
        private HoudiniEngineUnity.Test_Mesh <ToTestObject>b__2_0(UnityEngine.Mesh lod) { }

    }

    // TypeToken: 0x200019B  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_MeshRenderer_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.MeshRenderer,HoudiniEngineUnity.Test_MeshRenderer> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.MeshRenderer,HoudiniEngineUnity.Test_MeshRenderer> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA7B8  token: 0x6000D38
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D39
        public System.Void .ctor() { }
        // RVA: 0x09DC9DFC  token: 0x6000D3A
        private HoudiniEngineUnity.Test_MeshRenderer <ToTestObject>b__1_0(UnityEngine.MeshRenderer lod) { }
        // RVA: 0x09DCA458  token: 0x6000D3B
        private HoudiniEngineUnity.Test_MeshRenderer <ToTestObject>b__2_0(UnityEngine.MeshRenderer lod) { }

    }

    // TypeToken: 0x200019E  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_MeshFilter_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.MeshFilter,HoudiniEngineUnity.Test_MeshFilter> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.MeshFilter,HoudiniEngineUnity.Test_MeshFilter> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCAC04  token: 0x6000D42
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D43
        public System.Void .ctor() { }
        // RVA: 0x09DC9CDC  token: 0x6000D44
        private HoudiniEngineUnity.Test_MeshFilter <ToTestObject>b__1_0(UnityEngine.MeshFilter lod) { }
        // RVA: 0x09DC9F1C  token: 0x6000D45
        private HoudiniEngineUnity.Test_MeshFilter <ToTestObject>b__2_0(UnityEngine.MeshFilter lod) { }

    }

    // TypeToken: 0x20001A3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_Gradient_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.Gradient,HoudiniEngineUnity.Test_Gradient> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.Gradient,HoudiniEngineUnity.Test_Gradient> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA9AC  token: 0x6000D4F
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D50
        public System.Void .ctor() { }
        // RVA: 0x09DC9D9C  token: 0x6000D51
        private HoudiniEngineUnity.Test_Gradient <ToTestObject>b__1_0(UnityEngine.Gradient lod) { }
        // RVA: 0x09DCA03C  token: 0x6000D52
        private HoudiniEngineUnity.Test_Gradient <ToTestObject>b__2_0(UnityEngine.Gradient lod) { }

    }

    // TypeToken: 0x20001A6  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_AnimationCurve_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.AnimationCurve,HoudiniEngineUnity.Test_AnimationCurve> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.AnimationCurve,HoudiniEngineUnity.Test_AnimationCurve> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA880  token: 0x6000D59
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D5A
        public System.Void .ctor() { }
        // RVA: 0x09DC9BBC  token: 0x6000D5B
        private HoudiniEngineUnity.Test_AnimationCurve <ToTestObject>b__1_0(UnityEngine.AnimationCurve lod) { }
        // RVA: 0x09DCA338  token: 0x6000D5C
        private HoudiniEngineUnity.Test_AnimationCurve <ToTestObject>b__2_0(UnityEngine.AnimationCurve lod) { }

    }

    // TypeToken: 0x20001A9  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_TerrainLayer_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.TerrainLayer,HoudiniEngineUnity.Test_TerrainLayer> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.TerrainLayer,HoudiniEngineUnity.Test_TerrainLayer> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCAA74  token: 0x6000D63
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D64
        public System.Void .ctor() { }
        // RVA: 0x09DC9A3C  token: 0x6000D65
        private HoudiniEngineUnity.Test_TerrainLayer <ToTestObject>b__1_0(UnityEngine.TerrainLayer lod) { }
        // RVA: 0x09DCA2D8  token: 0x6000D66
        private HoudiniEngineUnity.Test_TerrainLayer <ToTestObject>b__2_0(UnityEngine.TerrainLayer lod) { }

    }

    // TypeToken: 0x20001AC  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_Texture2D_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.Texture2D,HoudiniEngineUnity.Test_Texture2D> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.Texture2D,HoudiniEngineUnity.Test_Texture2D> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCABA0  token: 0x6000D6D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D6E
        public System.Void .ctor() { }
        // RVA: 0x09DC97A0  token: 0x6000D6F
        private HoudiniEngineUnity.Test_Texture2D <ToTestObject>b__1_0(UnityEngine.Texture2D lod) { }
        // RVA: 0x09DCA3F8  token: 0x6000D70
        private HoudiniEngineUnity.Test_Texture2D <ToTestObject>b__2_0(UnityEngine.Texture2D lod) { }

    }

    // TypeToken: 0x20001AF  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_TreeInstance_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.TreeInstance,HoudiniEngineUnity.Test_TreeInstance> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.TreeInstance,HoudiniEngineUnity.Test_TreeInstance> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA81C  token: 0x6000D76
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D77
        public System.Void .ctor() { }
        // RVA: 0x09DC98C0  token: 0x6000D78
        private HoudiniEngineUnity.Test_TreeInstance <ToTestObject>b__1_0(UnityEngine.TreeInstance lod) { }
        // RVA: 0x09DCA21C  token: 0x6000D79
        private HoudiniEngineUnity.Test_TreeInstance <ToTestObject>b__2_0(UnityEngine.TreeInstance lod) { }

    }

    // TypeToken: 0x20001B2  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_TerrainData_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.TerrainData,HoudiniEngineUnity.Test_TerrainData> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.TerrainData,HoudiniEngineUnity.Test_TerrainData> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCAAD8  token: 0x6000D80
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D81
        public System.Void .ctor() { }
        // RVA: 0x09DC9C7C  token: 0x6000D82
        private HoudiniEngineUnity.Test_TerrainData <ToTestObject>b__1_0(UnityEngine.TerrainData lod) { }
        // RVA: 0x09DCA278  token: 0x6000D83
        private HoudiniEngineUnity.Test_TerrainData <ToTestObject>b__2_0(UnityEngine.TerrainData lod) { }

    }

    // TypeToken: 0x20001B5  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly HoudiniEngineUnity.Test_Terrain_Extensions.<>c <>9;  // static @ 0x0
        public static System.Converter<UnityEngine.Terrain,HoudiniEngineUnity.Test_Terrain> <>9__1_0;  // static @ 0x8
        public static System.Converter<UnityEngine.Terrain,HoudiniEngineUnity.Test_Terrain> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x09DCA5C4  token: 0x6000D8A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000D8B
        public System.Void .ctor() { }
        // RVA: 0x09DC9C1C  token: 0x6000D8C
        private HoudiniEngineUnity.Test_Terrain <ToTestObject>b__1_0(UnityEngine.Terrain lod) { }
        // RVA: 0x09DCA0FC  token: 0x6000D8D
        private HoudiniEngineUnity.Test_Terrain <ToTestObject>b__2_0(UnityEngine.Terrain lod) { }

    }

    // TypeToken: 0x20001B7  // size: 0x14
    public sealed struct PaintMergeMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode REPLACE;  // const
        public static HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode ADD;  // const
        public static HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode SUBTRACT;  // const
        public static HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode MULTIPLY;  // const

    }

    // TypeToken: 0x20001B8  // size: 0x14
    public sealed struct PaintMeshVisibility
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibility AUTO;  // const
        public static HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibility SHOW;  // const
        public static HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibility HIDE;  // const

    }

namespace HoudiniEngineUnity
{

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct HEU_AssetEventType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AssetEventType UNKNOWN;  // const
        public static HoudiniEngineUnity.HEU_AssetEventType RELOAD;  // const
        public static HoudiniEngineUnity.HEU_AssetEventType COOK;  // const
        public static HoudiniEngineUnity.HEU_AssetEventType BAKE_NEW;  // const
        public static HoudiniEngineUnity.HEU_AssetEventType BAKE_UPDATE;  // const

    }

    // TypeToken: 0x200000C  // size: 0x30
    public class HEU_AssetEventData
    {
        // Fields
        public HoudiniEngineUnity.HEU_HoudiniAsset Asset;  // 0x10
        public System.Boolean CookSuccess;  // 0x18
        public System.Collections.Generic.List<UnityEngine.GameObject> OutputObjects;  // 0x20
        public HoudiniEngineUnity.HEU_AssetEventType EventType;  // 0x28

        // Methods
        // RVA: 0x04044A30  token: 0x6000022
        public System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }

    }

    // TypeToken: 0x200000D  // size: 0x30
    public class HEU_ReloadEventData : HoudiniEngineUnity.HEU_AssetEventData
    {
        // Methods
        // RVA: 0x09D53B70  token: 0x6000023
        public System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }

    }

    // TypeToken: 0x200000E  // size: 0x30
    public class HEU_CookedEventData : HoudiniEngineUnity.HEU_AssetEventData
    {
        // Methods
        // RVA: 0x09D381C0  token: 0x6000024
        public System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }

    }

    // TypeToken: 0x200000F  // size: 0x38
    public class HEU_BakedEventData : HoudiniEngineUnity.HEU_AssetEventData
    {
        // Fields
        public System.Boolean IsNewBake;  // 0x30

        // Methods
        // RVA: 0x09D37EE0  token: 0x6000025
        public System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects, System.Boolean isNewBake) { }

    }

    // TypeToken: 0x2000010  // size: 0x20
    public class HEU_PreAssetEventData
    {
        // Fields
        public HoudiniEngineUnity.HEU_HoudiniAsset Asset;  // 0x10
        public HoudiniEngineUnity.HEU_AssetEventType AssetType;  // 0x18

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000026
        public System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, HoudiniEngineUnity.HEU_AssetEventType assetType) { }

    }

    // TypeToken: 0x2000011  // size: 0x30
    public class HEU_ReloadDataEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x09D53B34  token: 0x6000027
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x30
    public class HEU_CookedDataEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x09D38184  token: 0x6000028
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x30
    public class HEU_BakedDataEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x09D37EA4  token: 0x6000029
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x30
    public class HEU_PreAssetEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x09D53A38  token: 0x600002A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x50
    public class HEU_AssetPreset
    {
        // Fields
        public System.Char[] _identifier;  // 0x10
        public System.Int32 _version;  // 0x18
        public System.String _assetOPName;  // 0x20
        public System.Byte[] _parameterPreset;  // 0x28
        public System.Collections.Generic.List<System.String> _curveNames;  // 0x30
        public System.Collections.Generic.List<System.Byte[]> _curvePresets;  // 0x38
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputPreset> inputPresets;  // 0x40
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCachePreset> volumeCachePresets;  // 0x48

        // Methods
        // RVA: 0x09D37BB0  token: 0x600002B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x48
    public class HEU_InputPreset
    {
        // Fields
        private HoudiniEngineUnity.HEU_InputNode.InputObjectType _inputObjectType;  // 0x10
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputObjectPreset> _inputObjectPresets;  // 0x18
        public System.String _inputAssetName;  // 0x20
        public System.Int32 _inputIndex;  // 0x28
        public System.String _inputName;  // 0x30
        public System.Boolean _keepWorldTransform;  // 0x38
        public System.Boolean _packGeometryBeforeMerging;  // 0x39
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputAssetPreset> _inputAssetPresets;  // 0x40

        // Properties
        HoudiniEngineUnity.HEU_InputObjectTypeWrapper InputObjectType { get; /* RVA: 0x09D53A0C */ set; /* RVA: 0x09D53A18 */ }

        // Methods
        // RVA: 0x09D5394C  token: 0x600002E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x50
    public class HEU_InputObjectPreset
    {
        // Fields
        public System.String _gameObjectName;  // 0x10
        public System.Boolean _isSceneObject;  // 0x18
        public UnityEngine.GameObject _gameObject;  // 0x20
        public System.Boolean _useTransformOffset;  // 0x28
        public UnityEngine.Vector3 _translateOffset;  // 0x2c
        public UnityEngine.Vector3 _rotateOffset;  // 0x38
        public UnityEngine.Vector3 _scaleOffset;  // 0x44

        // Methods
        // RVA: 0x09D538EC  token: 0x600002F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000018  // size: 0x18
    public class HEU_InputAssetPreset
    {
        // Fields
        public System.String _gameObjectName;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000030
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x28
    public class HEU_VolumeLayerPreset
    {
        // Fields
        public System.String _layerName;  // 0x10
        public System.Single _strength;  // 0x18
        public System.Boolean _uiExpanded;  // 0x1c
        public System.Int32 _tile;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000031
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x40
    public class HEU_VolumeCachePreset
    {
        // Fields
        public System.String _objName;  // 0x10
        public System.String _geoName;  // 0x18
        public System.Boolean _uiExpanded;  // 0x20
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayerPreset> _volumeLayersPresets;  // 0x28
        public System.String _terrainDataPath;  // 0x30
        public System.Int32 _tile;  // 0x38

        // Methods
        // RVA: 0x09D54218  token: 0x6000032
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x20
    public class HEU_RecookPreset
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCachePreset> _volumeCachePresets;  // 0x10
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputPreset> _inputPresets;  // 0x18

        // Methods
        // RVA: 0x09D53A74  token: 0x6000033
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x10
    public static class HEU_AssetPresetUtility
    {
        // Fields
        public static System.Char[] PRESET_IDENTIFIER;  // static @ 0x0
        public static System.Int32 PRESET_VERSION;  // static @ 0x8

        // Methods
        // RVA: 0x09D375E8  token: 0x6000034
        public static System.Void SaveAssetPresetToFile(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String filePath) { }
        // RVA: 0x09D3703C  token: 0x6000035
        public static System.Void LoadPresetFileIntoAssetAndCook(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String filePath) { }
        // RVA: 0x09D37B2C  token: 0x6000036
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x10
    public class HEU_Vector3SerializationSurrogate : System.Runtime.Serialization.ISerializationSurrogate
    {
        // Methods
        // RVA: 0x09D53FC0  token: 0x6000037
        private virtual System.Void System.Runtime.Serialization.ISerializationSurrogate.GetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x09D54088  token: 0x6000038
        private virtual System.Object System.Runtime.Serialization.ISerializationSurrogate.SetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Runtime.Serialization.ISurrogateSelector selector) { }
        // RVA: 0x041E1670  token: 0x6000039
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public class HEU_Vector2SerializationSurrogate : System.Runtime.Serialization.ISerializationSurrogate
    {
        // Methods
        // RVA: 0x09D53DF0  token: 0x600003A
        private virtual System.Void System.Runtime.Serialization.ISerializationSurrogate.GetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x09D53E94  token: 0x600003B
        private virtual System.Object System.Runtime.Serialization.ISerializationSurrogate.SetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Runtime.Serialization.ISurrogateSelector selector) { }
        // RVA: 0x041E1670  token: 0x600003C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x30
    public class HEU_AssetSerializedMetaData : UnityEngine.ScriptableObject, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.Boolean _softDeleted;  // 0x18
        private System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData>> _savedCurveNodeData;  // 0x20
        private System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_InputCurveInfo> _savedInputCurveInfo;  // 0x28

        // Properties
        System.Boolean SoftDeleted { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData>> SavedCurveNodeData { get; /* RVA: 0x04D862C0 */ }
        System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_InputCurveInfo> SavedInputCurveInfo { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x09D37D08  token: 0x6000041
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_AssetSerializedMetaData other) { }
        // RVA: 0x09D37D9C  token: 0x6000042
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x10
    public class HEU_AssetUpdater
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000043
        private static System.Void .cctor() { }
        // RVA: 0x09D37E64  token: 0x6000044
        private static System.Void OnBeforeAssemblyReload() { }
        // RVA: 0x041E1670  token: 0x6000045
        private static System.Void Update() { }
        // RVA: 0x041E1670  token: 0x6000046
        public static System.Void AddAssetForUpdate(HoudiniEngineUnity.HEU_HoudiniAsset asset) { }
        // RVA: 0x041E1670  token: 0x6000047
        public static System.Void RemoveAsset(HoudiniEngineUnity.HEU_HoudiniAsset asset) { }
        // RVA: 0x041E1670  token: 0x6000048
        public static System.Void AddNodeSyncForUpdate(HoudiniEngineUnity.HEU_BaseSync nodeSync) { }
        // RVA: 0x041E1670  token: 0x6000049
        public static System.Void RemoveNodeSync(HoudiniEngineUnity.HEU_BaseSync nodeSync) { }
        // RVA: 0x041E1670  token: 0x600004A
        private static System.Void OnPrefabInstanceUpdate(UnityEngine.GameObject instance) { }
        // RVA: 0x041E1670  token: 0x600004B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x38
    public class CurveNodeData : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Vector3 rotation;  // 0x1c
        public UnityEngine.Vector3 scale;  // 0x28
        public System.Int32 curveCountIndex;  // 0x34

        // Methods
        // RVA: 0x09D36A84  token: 0x600004C
        public System.Void .ctor() { }
        // RVA: 0x09D36A14  token: 0x600004D
        public System.Void .ctor(UnityEngine.Vector3 position) { }
        // RVA: 0x09D36AE4  token: 0x600004E
        public System.Void .ctor(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x09D36B6C  token: 0x600004F
        public System.Void .ctor(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale) { }
        // RVA: 0x09D36974  token: 0x6000050
        public System.Void .ctor(HoudiniEngineUnity.CurveNodeData other) { }
        // RVA: 0x09D367A8  token: 0x6000051
        public UnityEngine.Quaternion GetRotation() { }
        // RVA: 0x09D367E4  token: 0x6000052
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.CurveNodeData other) { }

    }

    // TypeToken: 0x2000022  // size: 0x28
    public class HEU_InputCurveInfo
    {
        // Fields
        public HoudiniEngineUnity.HAPI_CurveType curveType;  // 0x10
        public System.Int32 order;  // 0x14
        public System.Boolean closed;  // 0x18
        public System.Boolean reverse;  // 0x19
        public HoudiniEngineUnity.HAPI_InputCurveMethod inputMethod;  // 0x1c
        public HoudiniEngineUnity.HAPI_InputCurveParameterization breakpointParameterization;  // 0x20

        // Methods
        // RVA: 0x09D53690  token: 0x6000053
        public static HoudiniEngineUnity.HEU_InputCurveInfo CreateFromHAPI_InputCurveInfo(HoudiniEngineUnity.HAPI_InputCurveInfo curveInfo) { }
        // RVA: 0x09D537BC  token: 0x6000054
        public static System.String[] GetCurveTypeNames() { }
        // RVA: 0x09D53864  token: 0x6000055
        public static System.String[] GetInputMethodNames() { }
        // RVA: 0x09D53714  token: 0x6000056
        public static System.String[] GetBreakpointParameterizationNames() { }
        // RVA: 0x04DA2680  token: 0x6000057
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x14
    public sealed struct HEU_CurveDataType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_CurveDataType INVALID;  // const
        public static HoudiniEngineUnity.HEU_CurveDataType GEO_COORDS_PARAM;  // const
        public static HoudiniEngineUnity.HEU_CurveDataType HAPI_COORDS_PARAM;  // const
        public static HoudiniEngineUnity.HEU_CurveDataType POSITION_ATTRIBUTE;  // const

    }

    // TypeToken: 0x2000024  // size: 0x90
    public class HEU_Curve : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_Curve, HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.Int32 _geoID;  // 0x18
        private System.Int32 _partID;  // 0x1c
        private System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> _curveNodeData;  // 0x20
        private UnityEngine.Vector3[] _vertices;  // 0x28
        private System.Boolean _isEditable;  // 0x30
        private HoudiniEngineUnity.HEU_Parameters _parameters;  // 0x38
        private System.Boolean _bUploadParameterPreset;  // 0x40
        private System.String _curveName;  // 0x48
        private UnityEngine.GameObject _targetGameObject;  // 0x50
        private System.Boolean _isGeoCurve;  // 0x58
        private HoudiniEngineUnity.HEU_Curve.CurveEditState _editState;  // 0x5c
        public static HoudiniEngineUnity.HEU_Curve.Interaction PreferredNextInteractionMode;  // static @ 0x0
        private HoudiniEngineUnity.HEU_HoudiniAsset _parentAsset;  // 0x60
        private System.Boolean _bIsInputCurve;  // 0x68
        private System.Boolean _bIsPartCurve;  // 0x69
        private System.Boolean _cachedCurveInfoValid;  // 0x6a
        private System.Int32[] _cachedCurveCounts;  // 0x70
        private System.Int32[] _cachedCurveCountSums;  // 0x78
        private HoudiniEngineUnity.HEU_CurveDataType _curveDataType;  // 0x80
        private HoudiniEngineUnity.HEU_InputCurveInfo _inputCurveInfo;  // 0x88

        // Properties
        UnityEngine.GameObject TargetGameObject { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Int32 GeoID { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 PartID { get; /* RVA: 0x04D86310 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> CurveNodeData { get; /* RVA: 0x04D862C0 */ }
        HoudiniEngineUnity.HEU_Parameters Parameters { get; /* RVA: 0x04D85A50 */ }
        System.String CurveName { get; /* RVA: 0x04D86200 */ }
        System.Boolean IsInputCurve { get; /* RVA: 0x04D86B40 */ }
        System.Boolean IsPartCurve { get; /* RVA: 0x04D86B10 */ }
        HoudiniEngineUnity.HEU_InputCurveInfo InputCurveInfo { get; /* RVA: 0x04D861F0 */ }
        HoudiniEngineUnity.HEU_Curve.CurveEditState EditState { get; /* RVA: 0x04D86560 */ }
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x04D86280 */ }
        HoudiniEngineUnity.HEU_CurveDataType CurveDataType { get; /* RVA: 0x04D863A0 */ }

        // Methods
        // RVA: 0x02D480B0  token: 0x6000062
        private System.Void SetUploadParameterPreset(System.Boolean bValue) { }
        // RVA: 0x09D39D50  token: 0x6000066
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: 0x09D3AB9C  token: 0x6000067
        public virtual System.Void Recook() { }
        // RVA: 0x09D3AB34  token: 0x6000068
        public System.Void Rebuild() { }
        // RVA: 0x04D865F0  token: 0x6000069
        public virtual System.Boolean IsEditable() { }
        // RVA: 0x04D86F70  token: 0x600006A
        public virtual System.Boolean IsGeoCurve() { }
        // RVA: 0x09D3AE50  token: 0x600006B
        public virtual System.Void SetCurveName(System.String name) { }
        // RVA: 0x09D3B168  token: 0x600006C
        public virtual System.Void SetCurvePoint(System.Int32 pointIndex, UnityEngine.Vector3 newPosition, System.Boolean bRecookAsset) { }
        // RVA: 0x09D3B024  token: 0x600006D
        public virtual System.Void SetCurvePoint(System.Int32 pointIndex, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset) { }
        // RVA: 0x09D3AED0  token: 0x600006E
        public virtual System.Void SetCurveNodeData(System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> curveNodeData, System.Boolean bRecookAsset) { }
        // RVA: 0x09D39638  token: 0x600006F
        public virtual UnityEngine.Vector3 GetCurvePoint(System.Int32 pointIndex) { }
        // RVA: 0x04D862C0  token: 0x6000070
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> GetAllPointTransforms() { }
        // RVA: 0x09D39400  token: 0x6000071
        public virtual System.Collections.Generic.List<UnityEngine.Vector3> GetAllPoints() { }
        // RVA: 0x09D397C0  token: 0x6000072
        public virtual System.Int32 GetNumPoints() { }
        // RVA: 0x09D3A0D8  token: 0x6000073
        public virtual System.Void InsertCurvePoint(System.Int32 index, UnityEngine.Vector3 position, System.Boolean bRecookAsset) { }
        // RVA: 0x09D3A030  token: 0x6000074
        public virtual System.Void InsertCurvePoint(System.Int32 index, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset) { }
        // RVA: 0x09D38280  token: 0x6000075
        public virtual System.Void AddCurvePointToEnd(UnityEngine.Vector3 position, System.Boolean bRecookAsset) { }
        // RVA: 0x09D381E4  token: 0x6000076
        public virtual System.Void AddCurvePointToEnd(HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset) { }
        // RVA: 0x09D3AC08  token: 0x6000077
        public virtual System.Void RemoveCurvePoint(System.Int32 pointIndex, System.Boolean bRecookAsset) { }
        // RVA: 0x09D38378  token: 0x6000078
        public virtual System.Void ClearCurveNodeData(System.Boolean bRecookAsset) { }
        // RVA: 0x09D3AAE0  token: 0x6000079
        public virtual System.Void ProjectToColliders(UnityEngine.Vector3 rayDirection, System.Single rayDistance, System.Boolean bRecookAsset) { }
        // RVA: 0x09D3AE1C  token: 0x600007A
        public virtual System.Void SetCurveGeometryVisibility(System.Boolean bVisible, System.Boolean bRecookAsset) { }
        // RVA: 0x09D39DE4  token: 0x600007B
        public virtual UnityEngine.Vector3 GetTransformedPoint(System.Int32 pointIndex) { }
        // RVA: 0x09D39EAC  token: 0x600007C
        public virtual System.Collections.Generic.List<UnityEngine.Vector3> GetTransformedPoints() { }
        // RVA: 0x09D38AC4  token: 0x600007D
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> DuplicateCurveNodeData() { }
        // RVA: 0x09D38404  token: 0x600007E
        private static HoudiniEngineUnity.HEU_Curve CreateSetupCurve(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean isEditable, System.String curveName, System.Int32 geoID, System.Int32 partID, System.Boolean bGeoCurve) { }
        // RVA: 0x09D3C6D8  token: 0x600007F
        private System.Void UsePreviousCurveData(System.String curveName) { }
        // RVA: 0x09D395F0  token: 0x6000080
        private HoudiniEngineUnity.HEU_CurveDataType GetCurveDataType(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D3B23C  token: 0x6000081
        private System.Boolean ShouldKeepNode(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D387A0  token: 0x6000082
        private System.Void DestroyAllData(System.Boolean bIsRebuild) { }
        // RVA: 0x09D3C574  token: 0x6000083
        private System.Void UploadParameterPreset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x09D3ACA0  token: 0x6000084
        private System.Void ResetCurveParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x09D3AF54  token: 0x6000085
        private System.Void SetCurveParameterPreset(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Byte[] parameterPreset) { }
        // RVA: 0x09D3BD30  token: 0x6000086
        private System.Void UpdateCurve(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partId) { }
        // RVA: 0x09D39580  token: 0x6000087
        private static System.Int32[] GetCurveCounts(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoId, System.Int32 partID) { }
        // RVA: 0x09D39030  token: 0x6000088
        private System.Void GenerateMesh(UnityEngine.GameObject inGameObject, HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D38C88  token: 0x6000089
        private System.Void GenerateMeshForSingleObject(UnityEngine.GameObject targetObject, UnityEngine.Vector3[] vertexList) { }
        // RVA: 0x09D3A4E8  token: 0x600008A
        private System.Void OnPresyncParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x09D39800  token: 0x600008B
        private static System.Int32 GetOrderForCurveType(System.Int32 requestedOrder, HoudiniEngineUnity.HAPI_CurveType curveType) { }
        // RVA: 0x09D3B444  token: 0x600008C
        private System.Boolean UpdateCurveInputForCurveParts(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x09D3B994  token: 0x600008D
        private System.Boolean UpdateCurveInputForCustomAttributes(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x09D3B294  token: 0x600008E
        private System.Void SyncFromParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bNewCurve) { }
        // RVA: 0x09D3BE4C  token: 0x600008F
        private System.Void UpdatePoints(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D3A520  token: 0x6000090
        private System.Void ProjectToCollidersInternal(HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, UnityEngine.Vector3 rayDirection, System.Single rayDistance) { }
        // RVA: 0x09D39AC4  token: 0x6000091
        public static System.String GetPointsString(System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> points) { }
        // RVA: 0x09D3984C  token: 0x6000092
        public static System.String GetPointsString(System.Collections.Generic.List<UnityEngine.Vector3> points) { }
        // RVA: 0x04D865C0  token: 0x6000093
        private System.Void SetEditState(HoudiniEngineUnity.HEU_Curve.CurveEditState editState) { }
        // RVA: 0x09D39FC0  token: 0x6000094
        private UnityEngine.Vector3 GetTransformedPosition(UnityEngine.Vector3 inPosition) { }
        // RVA: 0x09D39750  token: 0x6000095
        private UnityEngine.Vector3 GetInvertedTransformedPosition(UnityEngine.Vector3 inPosition) { }
        // RVA: 0x09D396E0  token: 0x6000096
        private UnityEngine.Vector3 GetInvertedTransformedDirection(UnityEngine.Vector3 inPosition) { }
        // RVA: 0x04D86240  token: 0x6000097
        private UnityEngine.Vector3[] GetVertices() { }
        // RVA: 0x09D3AD60  token: 0x6000098
        private System.Void SetCurveGeometryVisibilityInternal(System.Boolean bVisible) { }
        // RVA: 0x09D38A48  token: 0x6000099
        private System.Void DownloadPresetData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D3C65C  token: 0x600009A
        private System.Void UploadPresetData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D389CC  token: 0x600009B
        private System.Void DownloadAsDefaultPresetData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D3B33C  token: 0x600009C
        private System.Void UpdateCachedCurveInfo(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean copyCurveSettings) { }
        // RVA: 0x09D39510  token: 0x600009D
        private System.Int32 GetCurveCountIndexFromPositionIndex(System.Int32 positionIndex) { }
        // RVA: 0x09D3A4C0  token: 0x600009E
        private static System.Boolean IsMeshCurve(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09D3A1D0  token: 0x600009F
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_Curve other) { }
        // RVA: 0x09D3C944  token: 0x60000A0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x28
    public class HEU_GeneratedOutput : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HEU_GeneratedOutputData _outputData;  // 0x10
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutputData> _childOutputs;  // 0x18
        private System.Boolean isInstancer;  // 0x20

        // Properties
        System.Boolean IsInstancer { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }

        // Methods
        // RVA: 0x09D3EC14  token: 0x60000AD
        public static System.Void ResetMaterialOverrides(HoudiniEngineUnity.HEU_GeneratedOutput output) { }
        // RVA: 0x09D3ED5C  token: 0x60000AE
        public static System.Void ResetMaterialOverrides(HoudiniEngineUnity.HEU_GeneratedOutputData outputData) { }
        // RVA: 0x09D3E5C8  token: 0x60000AF
        public static UnityEngine.Material[] GetGeneratedMaterialsForGameObject(HoudiniEngineUnity.HEU_GeneratedOutput output, UnityEngine.GameObject inGameObject) { }
        // RVA: 0x09D3E784  token: 0x60000B0
        public static System.Boolean HasLODGroup(HoudiniEngineUnity.HEU_GeneratedOutput output) { }
        // RVA: 0x09D3EAB4  token: 0x60000B1
        public static System.Boolean IsOutputUsingMaterial(UnityEngine.Material checkMaterial, HoudiniEngineUnity.HEU_GeneratedOutput output) { }
        // RVA: 0x09D3E948  token: 0x60000B2
        public static System.Boolean IsOutputDataUsingMaterial(UnityEngine.Material checkMaterial, HoudiniEngineUnity.HEU_GeneratedOutputData outputData) { }
        // RVA: 0x09D3DD60  token: 0x60000B3
        public static System.Void ClearGeneratedMaterialReferences(HoudiniEngineUnity.HEU_GeneratedOutputData generatedOutputData) { }
        // RVA: 0x09D3E090  token: 0x60000B4
        public static System.Void DestroyAllGeneratedColliders(HoudiniEngineUnity.HEU_GeneratedOutputData outputData) { }
        // RVA: 0x09D3E424  token: 0x60000B5
        public static System.Void DestroyGeneratedOutput(HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput) { }
        // RVA: 0x09D3E268  token: 0x60000B6
        public static System.Void DestroyGeneratedOutputChildren(HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput) { }
        // RVA: 0x09D3E3CC  token: 0x60000B7
        public static System.Void DestroyGeneratedOutputData(HoudiniEngineUnity.HEU_GeneratedOutputData generatedOutputData, System.Boolean bDontDeletePersistantResources) { }
        // RVA: 0x09D3DD84  token: 0x60000B8
        public static System.Void ClearMaterialsNoLongerUsed(UnityEngine.Material[] materialsToCheck, UnityEngine.Material[] materialsInUse) { }
        // RVA: 0x09D3DE54  token: 0x60000B9
        public static System.Void CopyMaterialOverrides(HoudiniEngineUnity.HEU_GeneratedOutputData sourceOutputData, HoudiniEngineUnity.HEU_GeneratedOutputData destOutputData) { }
        // RVA: 0x09D3EE14  token: 0x60000BA
        private System.Void WriteOutputToAssetCache(UnityEngine.GameObject parentObject, System.String outputPath, System.Boolean bIsInstancer) { }
        // RVA: 0x09D3DC18  token: 0x60000BB
        private static System.Void BakeGameObjectComponents(UnityEngine.GameObject sourceGO, UnityEngine.GameObject targetGO, System.String assetName, System.String outputPath, System.Boolean bIsInstancer) { }
        // RVA: 0x09D3E7CC  token: 0x60000BC
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_GeneratedOutput other) { }
        // RVA: 0x09D3EE84  token: 0x60000BD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x28
    public class HEU_GeneratedOutputData : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.GameObject _gameObject;  // 0x10
        public UnityEngine.Material[] _renderMaterials;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Collider> _colliders;  // 0x20

        // Methods
        // RVA: 0x09D3D9F0  token: 0x60000BE
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_GeneratedOutputData other) { }
        // RVA: 0x09D3DBA0  token: 0x60000BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x78
    public class HEU_GeoNode : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_GeoNode, HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent, UnityEngine.ISerializationCallbackReceiver, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private HoudiniEngineUnity.HAPI_GeoInfo _geoInfo;  // 0x18
        private System.String _geoName;  // 0x40
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> _parts;  // 0x48
        private HoudiniEngineUnity.HEU_ObjectNode _containerObjectNode;  // 0x50
        private HoudiniEngineUnity.HEU_InputNode _inputNode;  // 0x58
        private HoudiniEngineUnity.HEU_Curve _geoCurve;  // 0x60
        private HoudiniEngineUnity.HEU_VolumeCache _volumeCache;  // 0x68
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> _volumeCaches;  // 0x70

        // Properties
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x09D42D6C */ }
        System.Int32 GeoID { get; /* RVA: 0x02B70F90 */ }
        HoudiniEngineUnity.HAPI_GeoInfo GeoInfo { get; /* RVA: 0x04D9A780 */ }
        System.String GeoName { get; /* RVA: 0x04D85A60 */ }
        HoudiniEngineUnity.HAPI_GeoType GeoType { get; /* RVA: 0x011EF5B0 */ }
        System.Boolean Editable { get; /* RVA: 0x04D86870 */ }
        System.Boolean Displayable { get; /* RVA: 0x09D42CEC */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> Parts { get; /* RVA: 0x04D86200 */ }
        HoudiniEngineUnity.HEU_ObjectNode ObjectNode { get; /* RVA: 0x04D86270 */ }
        HoudiniEngineUnity.HEU_InputNode InputNode { get; /* RVA: 0x02E56440 */ }
        HoudiniEngineUnity.HEU_Curve GeoCurve { get; /* RVA: 0x04D86280 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> VolumeCaches { get; /* RVA: 0x04D862B0 */ }

        // Methods
        // RVA: 0x09D42CCC  token: 0x60000CC
        public System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x60000CD
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: 0x09D40F28  token: 0x60000CE
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: 0x09D40660  token: 0x60000CF
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: 0x09D42038  token: 0x60000D0
        public virtual System.Void Recook() { }
        // RVA: 0x09D40F0C  token: 0x60000D1
        public virtual System.Boolean IsVisible() { }
        // RVA: 0x09D40DD8  token: 0x60000D2
        public virtual System.Boolean IsIntermediate() { }
        // RVA: 0x09D40DC0  token: 0x60000D3
        public virtual System.Boolean IsIntermediateOrEditable() { }
        // RVA: 0x09D40DAC  token: 0x60000D4
        public virtual System.Boolean IsGeoInputType() { }
        // RVA: 0x09D40DA4  token: 0x60000D5
        public virtual System.Boolean IsGeoCurveType() { }
        // RVA: 0x09D3F27C  token: 0x60000D6
        public virtual System.Void DestroyAllData(System.Boolean bIsRebuild) { }
        // RVA: 0x09D420AC  token: 0x60000D7
        public virtual System.Void RemoveAndDestroyPart(HoudiniEngineUnity.HEU_PartData part) { }
        // RVA: 0x09D40288  token: 0x60000D8
        public virtual System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: 0x09D403AC  token: 0x60000D9
        public virtual System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: 0x09D40130  token: 0x60000DA
        public virtual HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: 0x09D404D0  token: 0x60000DB
        public virtual HoudiniEngineUnity.HEU_PartData GetPartFromPartID(System.Int32 partID) { }
        // RVA: 0x09D3FB94  token: 0x60000DC
        public virtual System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly) { }
        // RVA: 0x04D86200  token: 0x60000DD
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> GetParts() { }
        // RVA: 0x09D40990  token: 0x60000DE
        public virtual System.Void HideAllGeometry() { }
        // RVA: 0x09D3F5DC  token: 0x60000DF
        public virtual System.Void DisableAllColliders() { }
        // RVA: 0x09D40704  token: 0x60000E0
        public virtual HoudiniEngineUnity.HEU_VolumeCache GetVolumeCacheByTileIndex(System.Int32 tileIndex) { }
        // RVA: 0x09D42110  token: 0x60000E1
        private System.Void Reset() { }
        // RVA: 0x09D40A24  token: 0x60000E2
        private System.Void Initialize(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_GeoInfo geoInfo, HoudiniEngineUnity.HEU_ObjectNode containerObjectNode) { }
        // RVA: 0x09D3F670  token: 0x60000E3
        private System.Boolean DoesThisRequirePotentialCook() { }
        // RVA: 0x09D42944  token: 0x60000E4
        private System.Void UpdateGeo(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D419AC  token: 0x60000E5
        private System.Void ProcessUnityScriptAttribute(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D41214  token: 0x60000E6
        private System.Void ProcessPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partID, HoudiniEngineUnity.HAPI_PartInfo& partInfo, HoudiniEngineUnity.HEU_PartData& partData) { }
        // RVA: 0x09D4246C  token: 0x60000E7
        private System.Void SetupGameObjectAndTransform(HoudiniEngineUnity.HEU_PartData partData, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x09D40580  token: 0x60000E8
        private System.Void GetPartsByOutputType(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> meshParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> volumeParts) { }
        // RVA: 0x09D3F830  token: 0x60000E9
        private System.Void GeneratePartInstances(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D3F6B8  token: 0x60000EA
        private System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D4101C  token: 0x60000EB
        private System.Void ProcessGeoCurve(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D4271C  token: 0x60000EC
        private System.Void SetupGeoCurveGameObjectAndTransform(HoudiniEngineUnity.HEU_Curve curve) { }
        // RVA: 0x09D3F1F4  token: 0x60000ED
        private System.Void ClearObjectInstances() { }
        // RVA: 0x09D423E8  token: 0x60000EE
        private System.Void SetGeoInfo(HoudiniEngineUnity.HAPI_GeoInfo geoInfo) { }
        // RVA: 0x09D3F758  token: 0x60000EF
        private System.String GeneratePartFullName(System.String partName) { }
        // RVA: 0x04D85A60  token: 0x60000F0
        private System.String GenerateGeoCurveName() { }
        // RVA: 0x09D40958  token: 0x60000F1
        private System.Boolean HasGeoNodeChanged(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D3EF30  token: 0x60000F2
        private System.Void ApplyHAPITransform(HoudiniEngineUnity.HAPI_Transform& hapiTransform) { }
        // RVA: 0x09D3FD90  token: 0x60000F3
        private System.Void GetDebugInfo(System.Text.StringBuilder sb) { }
        // RVA: 0x09D40DE0  token: 0x60000F4
        private System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData) { }
        // RVA: 0x09D3FA70  token: 0x60000F5
        private System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts) { }
        // RVA: 0x09D40840  token: 0x60000F6
        private System.Boolean HasAttribInstancer() { }
        // RVA: 0x09D421C0  token: 0x60000F7
        private System.Void SetAttributeModifiersOnPartOutputs(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D3F0E4  token: 0x60000F8
        private System.Void CalculateVisiblity(System.Boolean bParentVisibility) { }
        // RVA: 0x09D3F054  token: 0x60000F9
        private System.Void CalculateColliderState() { }
        // RVA: 0x09D41B60  token: 0x60000FA
        private System.Void ProcessVolumeParts(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> volumeParts, System.Boolean bRebuild) { }
        // RVA: 0x09D3F454  token: 0x60000FB
        private System.Void DestroyVolumeCache() { }
        // RVA: 0x09D428E8  token: 0x60000FC
        public virtual System.String ToString() { }
        // RVA: 0x09D40B40  token: 0x60000FD
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_GeoNode other) { }

    }

    // TypeToken: 0x200002F  // size: 0x268
    public sealed class HEU_HoudiniAsset : UnityEngine.MonoBehaviour, HoudiniEngineUnity.IHEU_HoudiniAsset, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType _assetType;  // 0x18
        private HoudiniEngineUnity.HAPI_AssetInfo _assetInfo;  // 0x1c
        private HoudiniEngineUnity.HAPI_NodeInfo _nodeInfo;  // 0x5c
        private System.String _assetName;  // 0xa0
        private System.String _assetOpName;  // 0xa8
        private System.String _assetHelp;  // 0xb0
        private System.Int32 _assetID;  // 0xb8
        private System.String _assetPath;  // 0xc0
        private System.Boolean _loadAssetFromMemory;  // 0xc8
        private System.Boolean _alwaysOverwriteOnLoad;  // 0xc9
        private UnityEngine.Object _assetFileObject;  // 0xd0
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectNode> _objectNodes;  // 0xd8
        private UnityEngine.GameObject _rootGameObject;  // 0xe0
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> _materialCache;  // 0xe8
        private HoudiniEngineUnity.HEU_Parameters _parameters;  // 0xf0
        private UnityEngine.Matrix4x4 _lastSyncedTransformMatrix;  // 0xf8
        private System.Collections.Generic.List<UnityEngine.Matrix4x4> _lastSyncedChildTransformMatrices;  // 0x138
        private System.String _assetCacheFolderPath;  // 0x140
        private System.String[] _subassetNames;  // 0x148
        private System.Int32 _selectedSubassetIndex;  // 0x150
        private HoudiniEngineUnity.HEU_AssetPreset _savedAssetPreset;  // 0x158
        private HoudiniEngineUnity.HEU_RecookPreset _recookPreset;  // 0x160
        private System.Int32 _totalCookCount;  // 0x168
        private HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction _requestBuildAction;  // 0x16c
        private System.Boolean _checkParameterChangeForCook;  // 0x170
        private System.Boolean _skipCookCheck;  // 0x171
        private System.Boolean _uploadParameters;  // 0x172
        private System.Boolean _forceUploadInputs;  // 0x173
        private System.Boolean _upstreamCookChanged;  // 0x174
        private HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus _cookStatus;  // 0x178
        private HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult _lastCookResult;  // 0x17c
        private System.Boolean _isCookingAssetReloaded;  // 0x180
        private System.Boolean _bForceUpdate;  // 0x181
        private System.Int64 _sessionID;  // 0x188
        private System.Boolean <WarnedPrefabNotSupported>k__BackingField;  // 0x190
        private System.Boolean _uiLocked;  // 0x191
        private System.Boolean _showHDAOptions;  // 0x192
        private System.Boolean _showGenerateSection;  // 0x193
        private System.Boolean _showBakeSection;  // 0x194
        private System.Boolean _showEventsSection;  // 0x195
        private System.Boolean _showCurvesSection;  // 0x196
        private System.Boolean _showInputNodesSection;  // 0x197
        private System.Boolean _showToolsSection;  // 0x198
        private System.Boolean _showTerrainSection;  // 0x199
        private HoudiniEngineUnity.HEU_InstanceInputUIState _instanceInputUIState;  // 0x1a0
        private HoudiniEngineUnity.HEU_ReloadDataEvent _reloadDataEvent;  // 0x1a8
        private HoudiniEngineUnity.HEU_CookedDataEvent _cookedDataEvent;  // 0x1b0
        private HoudiniEngineUnity.HEU_BakedDataEvent _bakedDataEvent;  // 0x1b8
        private HoudiniEngineUnity.HEU_PreAssetEvent _preAssetEvent;  // 0x1c0
        private HoudiniEngineUnity.HEU_HoudiniAsset.UpdateUIDelegate _refreshUIDelegate;  // 0x1c8
        private HoudiniEngineUnity.HEU_CookedDataEvent _downstreamConnectionCookedEvent;  // 0x1d0
        private System.Boolean _generateUVs;  // 0x1d8
        private System.Boolean _generateTangents;  // 0x1d9
        private System.Boolean _generateNormals;  // 0x1da
        private System.Boolean _pushTransformToHoudini;  // 0x1db
        private System.Boolean _transformChangeTriggersCooks;  // 0x1dc
        private System.Boolean _cookingTriggersDownCooks;  // 0x1dd
        private System.Boolean _autoCookOnParameterChange;  // 0x1de
        private System.Boolean _ignoreNonDisplayNodes;  // 0x1df
        private System.Boolean _useOutputNodes;  // 0x1e0
        private System.Boolean _generateMeshUsingPoints;  // 0x1e1
        private System.Boolean _useLODGroups;  // 0x1e2
        private System.Boolean _splitGeosByGroup;  // 0x1e3
        private System.Boolean _sessionSyncAutoCook;  // 0x1e4
        private System.Boolean _bakeUpdateKeepPreviousTransformValues;  // 0x1e5
        private System.Boolean _pauseCooking;  // 0x1e6
        private System.Boolean _curveEditorEnabled;  // 0x1e7
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> _curves;  // 0x1e8
        private HoudiniEngineUnity.HEU_Curve.CurveDrawCollision _curveDrawCollision;  // 0x1f0
        private System.Collections.Generic.List<UnityEngine.Collider> _curveDrawColliders;  // 0x1f8
        private UnityEngine.LayerMask _curveDrawLayerMask;  // 0x200
        private System.Single _curveProjectMaxDistance;  // 0x204
        private UnityEngine.Vector3 _curveProjectDirection;  // 0x208
        private System.Boolean _curveProjectDirectionToView;  // 0x214
        private System.Boolean _curveDisableScaleRotation;  // 0x215
        private System.Boolean _curveFrameSelectedNodes;  // 0x216
        private System.Single _curveFrameSelectedNodeDistance;  // 0x218
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> _inputNodes;  // 0x220
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> _handles;  // 0x228
        private System.Boolean _handlesEnabled;  // 0x230
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> _volumeCaches;  // 0x238
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributesStore> _attributeStores;  // 0x240
        private System.Boolean _editableNodesToolsEnabled;  // 0x248
        private HoudiniEngineUnity.HEU_ToolsInfo _toolsInfo;  // 0x250
        private HoudiniEngineUnity.HEU_AssetSerializedMetaData _serializedMetaData;  // 0x258
        private System.Boolean _pendingAutoCookOnMouseRelease;  // 0x260

        // Properties
        System.Boolean LoadAssetFromMemory { get; /* RVA: 0x04D8E270 */ set; /* RVA: 0x04D8E290 */ }
        System.Boolean AlwaysOverwriteOnLoad { get; /* RVA: 0x04DA2350 */ set; /* RVA: 0x04DA2520 */ }
        System.Boolean GenerateUVs { get; /* RVA: 0x04DA2460 */ set; /* RVA: 0x04DA2600 */ }
        System.Boolean GenerateTangents { get; /* RVA: 0x04DA2450 */ set; /* RVA: 0x04DA25F0 */ }
        System.Boolean GenerateNormals { get; /* RVA: 0x04DA2440 */ set; /* RVA: 0x04DA25E0 */ }
        System.Boolean PushTransformToHoudini { get; /* RVA: 0x04DA24C0 */ set; /* RVA: 0x04DA2630 */ }
        System.Boolean TransformChangeTriggersCooks { get; /* RVA: 0x04DA2500 */ set; /* RVA: 0x04DA2660 */ }
        System.Boolean CookingTriggersDownCooks { get; /* RVA: 0x04DA23B0 */ set; /* RVA: 0x04DA2550 */ }
        System.Boolean AutoCookOnParameterChange { get; /* RVA: 0x04DA2390 */ set; /* RVA: 0x04DA2530 */ }
        System.Boolean IgnoreNonDisplayNodes { get; /* RVA: 0x04DA2480 */ set; /* RVA: 0x04DA2620 */ }
        System.Boolean UseOutputNodes { get; /* RVA: 0x04D91ED0 */ set; /* RVA: 0x04D91EE0 */ }
        System.Boolean GenerateMeshUsingPoints { get; /* RVA: 0x04DA2430 */ set; /* RVA: 0x04DA25D0 */ }
        System.Boolean UseLODGroups { get; /* RVA: 0x04DA2510 */ set; /* RVA: 0x04DA2670 */ }
        System.Boolean SplitGeosByGroup { get; /* RVA: 0x04DA24F0 */ set; /* RVA: 0x04DA2650 */ }
        System.Boolean SessionSyncAutoCook { get; /* RVA: 0x04DA24E0 */ set; /* RVA: 0x04DA2640 */ }
        System.Boolean BakeUpdateKeepPreviousTransformValues { get; /* RVA: 0x04DA23A0 */ set; /* RVA: 0x04DA2540 */ }
        System.Boolean PauseCooking { get; /* RVA: 0x04D90890 */ set; /* RVA: 0x04D908B0 */ }
        System.Boolean CurveEditorEnabled { get; /* RVA: 0x04D908A0 */ set; /* RVA: 0x04D908C0 */ }
        HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper CurveDrawCollision { get; /* RVA: 0x09D53620 */ set; /* RVA: 0x09D53654 */ }
        UnityEngine.LayerMask CurveDrawLayerMask { get; /* RVA: 0x04DA2310 */ set; /* RVA: 0x04DA2340 */ }
        System.Single CurveProjectMaxDistance { get; /* RVA: 0x04DA2420 */ set; /* RVA: 0x04DA25C0 */ }
        UnityEngine.Vector3 CurveProjectDirection { get; /* RVA: 0x04DA2400 */ set; /* RVA: 0x04DA25A0 */ }
        System.Boolean CurveProjectDirectionToView { get; /* RVA: 0x04DA23F0 */ set; /* RVA: 0x04DA2590 */ }
        System.Boolean CurveDisableScaleRotation { get; /* RVA: 0x04D8E560 */ set; /* RVA: 0x04DA2560 */ }
        System.Boolean CurveFrameSelectedNodes { get; /* RVA: 0x04DA23E0 */ set; /* RVA: 0x04DA2580 */ }
        System.Single CurveFrameSelectedNodeDistance { get; /* RVA: 0x04DA23D0 */ set; /* RVA: 0x04DA2570 */ }
        System.Boolean HandlesEnabled { get; /* RVA: 0x04DA2470 */ set; /* RVA: 0x04DA2610 */ }
        System.Boolean EditableNodesToolsEnabled { get; /* RVA: 0x04D90930 */ set; /* RVA: 0x04D90960 */ }
        HoudiniEngineUnity.HEU_AssetTypeWrapper AssetType { get; /* RVA: 0x09D535E4 */ }
        HoudiniEngineUnity.HAPI_AssetInfo AssetInfo { get; /* RVA: 0x04DA2360 */ }
        HoudiniEngineUnity.HAPI_NodeInfo NodeInfo { get; /* RVA: 0x04DA2490 */ }
        System.String AssetName { get; /* RVA: 0x04D86210 */ }
        System.String AssetOpName { get; /* RVA: 0x04D86290 */ }
        System.String AssetHelp { get; /* RVA: 0x04D86250 */ }
        System.Int32 AssetID { get; /* RVA: 0x04D8E580 */ }
        System.String AssetPath { get; /* RVA: 0x04D892F0 */ }
        UnityEngine.GameObject OwnerGameObject { get; /* RVA: 0x053916BC */ }
        UnityEngine.GameObject RootGameObject { get; /* RVA: 0x04D8D1C0 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> MaterialCache { get; /* RVA: 0x04D88590 */ }
        HoudiniEngineUnity.HEU_Parameters Parameters { get; /* RVA: 0x04D8D1A0 */ }
        System.String AssetCacheFolder { get; /* RVA: 0x04D8DA60 */ }
        System.String[] SubassetNames { get; /* RVA: 0x04D8DAC0 */ }
        System.Int32 SelectedSubassetIndex { get; /* RVA: 0x04DA24D0 */ }
        HoudiniEngineUnity.HEU_AssetCookStatusWrapper CookStatus { get; /* RVA: 0x09D53610 */ }
        HoudiniEngineUnity.HEU_AssetCookResultWrapper LastCookResult { get; /* RVA: 0x09D53630 */ }
        System.Int64 SessionID { get; /* RVA: 0x04D8DA20 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> Curves { get; /* RVA: 0x04D87930 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> InputNodes { get; /* RVA: 0x04D8D870 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> VolumeCaches { get; /* RVA: 0x04D8DAD0 */ }
        HoudiniEngineUnity.HEU_ReloadDataEvent ReloadDataEvent { get; /* RVA: 0x04D8D9C0 */ }
        HoudiniEngineUnity.HEU_CookedDataEvent CookedDataEvent { get; /* RVA: 0x04D8D960 */ }
        HoudiniEngineUnity.HEU_BakedDataEvent BakedDataEvent { get; /* RVA: 0x04D8DA90 */ }
        HoudiniEngineUnity.HEU_PreAssetEvent PreAssetEvent { get; /* RVA: 0x04D8DA80 */ }
        HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType AssetTypeInternal { get; /* RVA: 0x011EF5B0 */ }
        System.Boolean WarnedPrefabNotSupported { get; /* RVA: 0x04D8FB60 */ set; /* RVA: 0x04D8FC50 */ }
        HoudiniEngineUnity.HEU_InstanceInputUIState InstanceInputUIState { get; /* RVA: 0x04D8DA10 */ set; /* RVA: 0x05397204 */ }
        HoudiniEngineUnity.HEU_HoudiniAsset.UpdateUIDelegate RefreshUIDelegate { get; /* RVA: 0x04D8D790 */ set; /* RVA: 0x06B8B0D0 */ }
        HoudiniEngineUnity.HEU_Curve.CurveDrawCollision CurveDrawCollisionInternal { get; /* RVA: 0x04DA23C0 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> Handles { get; /* RVA: 0x04D8D440 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributesStore> AttributeStores { get; /* RVA: 0x04D8DE80 */ }
        HoudiniEngineUnity.HEU_ToolsInfo ToolsInfo { get; /* RVA: 0x04D8DCE0 */ }
        HoudiniEngineUnity.HEU_AssetSerializedMetaData SerializedMetaData { get; /* RVA: 0x04D8DE70 */ }
        System.Boolean PendingAutoCookOnMouseRelease { get; /* RVA: 0x04D8F3F0 */ set; /* RVA: 0x09D53674 */ }

        // Methods
        // RVA: 0x04DA2300  token: 0x6000150
        private HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus GetCookStatus() { }
        // RVA: 0x04D92A50  token: 0x6000151
        private HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult GetLastCookResult() { }
        // RVA: 0x04D87920  token: 0x6000159
        private System.Collections.Generic.List<UnityEngine.Collider> GetCurveDrawColliders() { }
        // RVA: 0x04DA2310  token: 0x600015A
        private UnityEngine.LayerMask GetCurveDrawLayerMask() { }
        // RVA: 0x04DA2340  token: 0x600015B
        private System.Void SetCurveDrawLayerMask(UnityEngine.LayerMask mask) { }
        // RVA: 0x02FFF600  token: 0x6000162
        public virtual System.Boolean RequestCook(System.Boolean bCheckParametersChanged, System.Boolean bAsync, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters) { }
        // RVA: 0x02FFF600  token: 0x6000163
        public virtual System.Boolean RequestReload(System.Boolean bAsync) { }
        // RVA: 0x02FFF600  token: 0x6000164
        public virtual System.Boolean RequestResetParameters(System.Boolean bAsync) { }
        // RVA: 0x09D4866C  token: 0x6000165
        public virtual UnityEngine.GameObject DuplicateAsset(UnityEngine.GameObject newRootGameObject) { }
        // RVA: 0x09D479B4  token: 0x6000166
        public virtual System.Boolean DeleteAllGeneratedData(System.Boolean bIsRebuild) { }
        // RVA: 0x09D44B40  token: 0x6000167
        public virtual UnityEngine.GameObject BakeToNewPrefab(System.String destinationPrefabPath) { }
        // RVA: 0x09D44E54  token: 0x6000168
        public virtual UnityEngine.GameObject BakeToNewStandalone() { }
        // RVA: 0x09D43A2C  token: 0x6000169
        public virtual System.Boolean BakeToExistingPrefab(UnityEngine.GameObject bakeTargetGO) { }
        // RVA: 0x09D43F64  token: 0x600016A
        public virtual System.Boolean BakeToExistingStandalone(UnityEngine.GameObject bakeTargetGO) { }
        // RVA: 0x09D4D490  token: 0x600016B
        public virtual System.Boolean IsAssetValid() { }
        // RVA: 0x09D4BC18  token: 0x600016C
        public virtual System.Boolean GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: 0x09D4BE68  token: 0x600016D
        public virtual System.Boolean GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: 0x09D4A5A4  token: 0x600016E
        public virtual HoudiniEngineUnity.HEU_Curve GetCurve(System.String curveName) { }
        // RVA: 0x09D42F2C  token: 0x600016F
        public virtual System.Boolean AddCurveDrawCollider(UnityEngine.Collider newCollider) { }
        // RVA: 0x09D4FD40  token: 0x6000170
        public virtual System.Boolean RemoveCurveDrawCollider(UnityEngine.Collider collider) { }
        // RVA: 0x09D457B4  token: 0x6000171
        public virtual System.Boolean ClearCurveDrawColliders() { }
        // RVA: 0x09D4ACAC  token: 0x6000172
        public virtual HoudiniEngineUnity.HEU_InputNode GetInputNode(System.String inputName) { }
        // RVA: 0x09D49A8C  token: 0x6000173
        public virtual HoudiniEngineUnity.HEU_InputNode GetAssetInputNode(System.String inputName) { }
        // RVA: 0x09D4AC38  token: 0x6000174
        public virtual HoudiniEngineUnity.HEU_InputNode GetInputNodeByIndex(System.Int32 index) { }
        // RVA: 0x09D4B2DC  token: 0x6000175
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> GetNonParameterInputNodes() { }
        // RVA: 0x09D4C284  token: 0x6000176
        public virtual System.Int32 GetVolumeCacheCount() { }
        // RVA: 0x09D4A298  token: 0x6000177
        public virtual HoudiniEngineUnity.HEU_SessionBase GetAssetSession(System.Boolean bCreateIfInvalid) { }
        // RVA: 0x09D4B668  token: 0x6000178
        public virtual HoudiniEngineUnity.HEU_ObjectNode GetObjectNodeByName(System.String objName) { }
        // RVA: 0x09D4BD40  token: 0x6000179
        public virtual System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outputGeoNodes) { }
        // RVA: 0x09D4B024  token: 0x600017A
        public virtual HoudiniEngineUnity.HEU_PartData GetInternalHDAPartWithGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: 0x09D509BC  token: 0x600017B
        public virtual System.Void ResetParametersToDefault() { }
        // RVA: 0x09D4C878  token: 0x600017C
        public virtual System.Void HideAllGeometry() { }
        // RVA: 0x09D47CA8  token: 0x600017D
        public virtual System.Void DisableAllColliders() { }
        // RVA: 0x09D4B180  token: 0x600017E
        public virtual HoudiniEngineUnity.HEU_MaterialData GetMaterialData(UnityEngine.Material material) { }
        // RVA: 0x09D45C10  token: 0x600017F
        public virtual System.Void ClearMaterialCache() { }
        // RVA: 0x09D4FEA4  token: 0x6000180
        public virtual System.Void RemoveMaterial(UnityEngine.Material material) { }
        // RVA: 0x09D50860  token: 0x6000181
        public virtual System.Void ResetMaterialOverrides() { }
        // RVA: 0x09D49BD4  token: 0x6000182
        public virtual HoudiniEngineUnity.HEU_AssetPreset GetAssetPreset(System.Boolean sceneRelativeGameObjects) { }
        // RVA: 0x09D4BAB0  token: 0x6000183
        public virtual HoudiniEngineUnity.HEU_PDGAssetLink GetOrCreatePDGAssetLink() { }
        // RVA: 0x09D4C998  token: 0x6000184
        public static HoudiniEngineUnity.HEU_HoudiniAssetRoot InstantiateHDA(System.String filePath, System.Boolean bAsync, UnityEngine.Vector3 initialPosition) { }
        // RVA: 0x09D4C0B0  token: 0x6000185
        public static HoudiniEngineUnity.HEU_PartData GetSceneHDAPartWithGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: 0x09D4BF90  token: 0x6000186
        public static HoudiniEngineUnity.HEU_HoudiniAsset GetSceneHDAAssetFromGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: 0x09D4E3CC  token: 0x6000187
        public static System.Boolean IsHoudiniAssetOutput(UnityEngine.GameObject go) { }
        // RVA: 0x09D4E4F8  token: 0x6000188
        public static System.Boolean IsHoudiniAssetRoot(UnityEngine.GameObject go) { }
        // RVA: 0x09D51148  token: 0x6000189
        private System.Void SetupAsset(HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType assetType, System.String filePath, UnityEngine.GameObject rootGameObject, HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D451DC  token: 0x600018A
        private System.Void CleanUpAndDisable() { }
        // RVA: 0x09D4D440  token: 0x600018B
        private System.Boolean IsAssetSavedInScene() { }
        // RVA: 0x041E1670  token: 0x600018C
        private System.Void Awake() { }
        // RVA: 0x04DA2320  token: 0x600018D
        private System.Void InvalidateAsset() { }
        // RVA: 0x041E1670  token: 0x600018E
        private System.Void OnEnable() { }
        // RVA: 0x041E1670  token: 0x600018F
        private System.Void OnDestroy() { }
        // RVA: 0x041E1670  token: 0x6000190
        private System.Void AssetUpdate() { }
        // RVA: 0x041E1670  token: 0x6000191
        private System.Void PostAssetUpdate() { }
        // RVA: 0x09D50590  token: 0x6000192
        private System.Void RequestBakeInPlace() { }
        // RVA: 0x04DA22E0  token: 0x6000193
        private System.Void ClearBuildRequest() { }
        // RVA: 0x09D4C860  token: 0x6000194
        private System.Boolean HasValidAssetPath() { }
        // RVA: 0x09D4F664  token: 0x6000195
        private System.Void ProcessRebuild(System.Boolean bPromptForSubasset, System.Int32 desiredSubassetIndex) { }
        // RVA: 0x09D4D384  token: 0x6000196
        private System.Void InvokeReloadEvent(System.Boolean bCookSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: 0x09D51560  token: 0x6000197
        private System.Boolean StartRebuild(System.Boolean bPromptForSubasset, System.Int32 desiredSubassetIndex) { }
        // RVA: 0x09D48A88  token: 0x6000198
        private System.Boolean FinishRebuild() { }
        // RVA: 0x09D4F9DC  token: 0x6000199
        private System.Boolean RecookAsync(System.Boolean bCheckParamsChanged, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters, System.Boolean bUploadParameterPreset, System.Boolean bForceUploadInputs, System.Boolean bCookingSessionSync) { }
        // RVA: 0x09D4FAFC  token: 0x600019A
        private System.Boolean RecookBlocking(System.Boolean bCheckParamsChanged, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters, System.Boolean bUploadParameterPreset, System.Boolean bForceUploadInputs, System.Boolean bCookingSessionSync) { }
        // RVA: 0x09D47E50  token: 0x600019B
        private System.Void DoPostCookWork(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D4E564  token: 0x600019C
        private System.Boolean IsValidForInteraction(System.String& errorMessage) { }
        // RVA: 0x09D4EF4C  token: 0x600019D
        private System.Void OnValidate() { }
        // RVA: 0x09D489EC  token: 0x600019E
        private System.Void ExecutePostCookCallbacks() { }
        // RVA: 0x09D4D2C8  token: 0x600019F
        private System.Void InvokePostCookEvent(System.Boolean bCookSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: 0x09D4CBB0  token: 0x60001A0
        private System.Boolean InternalStartRecook(System.Boolean bCheckParamsChanged, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters, System.Boolean bUploadParameterPreset, System.Boolean bForceUploadInputs, System.Boolean bCookingSessionSync) { }
        // RVA: 0x09D4CA58  token: 0x60001A1
        private System.Void InternalSetAssetID(System.Int32 assetID) { }
        // RVA: 0x04DA2330  token: 0x60001A2
        private System.Void SetCookStatus(HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus status, HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult result) { }
        // RVA: 0x09D4F288  token: 0x60001A3
        private System.Void ProcessPoskCook() { }
        // RVA: 0x09D5153C  token: 0x60001A4
        private System.Boolean StartHoudiniCookNode(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D4F0D8  token: 0x60001A5
        private System.Void ProcessHoudiniCookStatus(System.Boolean bAsync) { }
        // RVA: 0x09D48194  token: 0x60001A6
        private System.Boolean DoesAssetRequireRecook() { }
        // RVA: 0x09D47C94  token: 0x60001A7
        private System.Void DeleteSessionDataOnly() { }
        // RVA: 0x09D45354  token: 0x60001A8
        private System.Void CleanUpInputNodes() { }
        // RVA: 0x09D47C44  token: 0x60001A9
        private System.Void DeleteAssetCacheData(System.Boolean bRegisterUndo) { }
        // RVA: 0x09D49878  token: 0x60001AA
        private System.Void GenerateParameters(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D485E8  token: 0x60001AB
        private System.Void DownloadParameterPresetFromHoudini(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5310C  token: 0x60001AC
        private System.Void UploadParameterPresetToHoudini(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D52514  token: 0x60001AD
        private System.Void UpdateParameterInputsToHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bForceUpdate) { }
        // RVA: 0x09D4E5D4  token: 0x60001AE
        private System.Boolean LoadAssetFileWithSubasset(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bPromptForSubasset, System.Int32 desiredSubassetIndex) { }
        // RVA: 0x09D4730C  token: 0x60001AF
        private System.Boolean CreateAndCookAsset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 subassetIndex, System.Int32& newAssetID, System.Boolean bCookTemplatedGeos) { }
        // RVA: 0x09D47434  token: 0x60001B0
        private System.Void CreateAssetInputs(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D490A0  token: 0x60001B1
        public System.Void ForceUploadParameters(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D52BBC  token: 0x60001B2
        private System.Void UploadCurvesParameters(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bCheckParamsChanged) { }
        // RVA: 0x09D52618  token: 0x60001B3
        private System.Void UploadAttributeValues(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D517C0  token: 0x60001B4
        private System.Void SyncDirtyAttributesToHoudini(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D52F3C  token: 0x60001B5
        private System.Void UploadInputNodes(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bForceUpdate, System.Boolean bUpdateAll) { }
        // RVA: 0x09D4C46C  token: 0x60001B6
        private System.Boolean HasInputNodeTransformChanged() { }
        // RVA: 0x09D4ED78  token: 0x60001B7
        private System.Void NotifyInputNodesCookFinished() { }
        // RVA: 0x09D47878  token: 0x60001B8
        private System.Boolean CreateObjects(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D51AD8  token: 0x60001B9
        private System.Void UpdateAllObjectNodes(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D47788  token: 0x60001BA
        private HoudiniEngineUnity.HEU_ObjectNode CreateObjectNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo& objectInfo, HoudiniEngineUnity.HAPI_Transform& objectTranform) { }
        // RVA: 0x09D49740  token: 0x60001BB
        private System.Void GenerateObjectsGeometry(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bRebuild) { }
        // RVA: 0x09D4EEC4  token: 0x60001BC
        private System.Int32 NumAttributeStores() { }
        // RVA: 0x09D4A310  token: 0x60001BD
        private HoudiniEngineUnity.HEU_AttributesStore GetAttributeStore(System.String geoName, System.Int32 partID) { }
        // RVA: 0x09D490D4  token: 0x60001BE
        private System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D493C4  token: 0x60001BF
        private System.Void GenerateInstances(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D491FC  token: 0x60001C0
        private System.Void GenerateHandles(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D45254  token: 0x60001C1
        private System.Void CleanUpHandles() { }
        // RVA: 0x09D4A848  token: 0x60001C2
        private HoudiniEngineUnity.HEU_Handle GetHandleByName(System.String handleName) { }
        // RVA: 0x04D8D440  token: 0x60001C3
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> GetHandles() { }
        // RVA: 0x09D4EF08  token: 0x60001C4
        private System.Int32 NumHandles() { }
        // RVA: 0x09D4B938  token: 0x60001C5
        private HoudiniEngineUnity.HAPI_Transform GetObjectTransform(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 objectID) { }
        // RVA: 0x09D4B9F4  token: 0x60001C6
        private HoudiniEngineUnity.HEU_ObjectNode GetObjectWithID(System.Int32 objId) { }
        // RVA: 0x09D4D208  token: 0x60001C7
        private System.Void InvokeBakedEvent(System.Boolean bSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects, System.Boolean isNewBake) { }
        // RVA: 0x09D45C58  token: 0x60001C8
        private UnityEngine.GameObject CloneAssetWithoutHDA(System.String& bakedAssetPath, System.Boolean bWriteMeshesToAssetDatabase, System.Boolean bReconnectPrefabInstances) { }
        // RVA: 0x09D4EE90  token: 0x60001C9
        private System.Void NotifyUpstreamCooked(HoudiniEngineUnity.HEU_CookedEventData Data) { }
        // RVA: 0x09D4618C  token: 0x60001CA
        private System.Void ConnectToUpstream(HoudiniEngineUnity.HEU_HoudiniAsset upstreamAsset) { }
        // RVA: 0x09D47DC0  token: 0x60001CB
        private System.Void DisconnectFromUpstream(HoudiniEngineUnity.HEU_HoudiniAsset upstreamAsset) { }
        // RVA: 0x09D43038  token: 0x60001CC
        private System.Void AddDownstreamConnection(UnityEngine.Events.UnityAction<HoudiniEngineUnity.HEU_CookedEventData> receiver) { }
        // RVA: 0x09D4FDF4  token: 0x60001CD
        private System.Void RemoveDownstreamConnection(UnityEngine.Events.UnityAction<HoudiniEngineUnity.HEU_CookedEventData> receiver) { }
        // RVA: 0x09D4557C  token: 0x60001CE
        private System.Void ClearAllUpstreamConnections() { }
        // RVA: 0x09D523EC  token: 0x60001CF
        private System.Void UpdateInputsOnAssetRecreation(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D4F89C  token: 0x60001D0
        private System.Void ReconnectInputsUpstreamNotifications() { }
        // RVA: 0x09D4A984  token: 0x60001D1
        private System.Void GetHoudiniTransformAndApply(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D532F0  token: 0x60001D2
        private System.Void UploadUnityTransform(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bOnlySendIfChangedFromLastSync) { }
        // RVA: 0x09D5220C  token: 0x60001D3
        private System.Void UpdateHoudiniMaterials(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D4FFBC  token: 0x60001D4
        private System.Void RemoveUnusedMaterials() { }
        // RVA: 0x09D4D45C  token: 0x60001D5
        private System.Boolean IsAssetValidInHoudini(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D4C5BC  token: 0x60001D6
        private System.Boolean HasTransformChangedSinceLastUpdate() { }
        // RVA: 0x09D4A470  token: 0x60001D7
        private System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts) { }
        // RVA: 0x09D45800  token: 0x60001D8
        private System.Void ClearInvalidCurves() { }
        // RVA: 0x09D4A710  token: 0x60001D9
        private System.Int32 GetEditableCurveCount() { }
        // RVA: 0x09D42FB4  token: 0x60001DA
        private System.Void AddCurve(HoudiniEngineUnity.HEU_Curve curve) { }
        // RVA: 0x09D4FD9C  token: 0x60001DB
        private System.Void RemoveCurve(HoudiniEngineUnity.HEU_Curve curve) { }
        // RVA: 0x04D8D870  token: 0x60001DC
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> GetInputNodes() { }
        // RVA: 0x09D430B8  token: 0x60001DD
        private System.Void AddInputNode(HoudiniEngineUnity.HEU_InputNode node) { }
        // RVA: 0x09D4FE4C  token: 0x60001DE
        private System.Void RemoveInputNode(HoudiniEngineUnity.HEU_InputNode node) { }
        // RVA: 0x09D4C990  token: 0x60001DF
        private System.Void InputNodeNotifyRemoved(HoudiniEngineUnity.HEU_InputNode node) { }
        // RVA: 0x09D431A8  token: 0x60001E0
        private System.Void AddVolumeCache(HoudiniEngineUnity.HEU_VolumeCache cache) { }
        // RVA: 0x09D50408  token: 0x60001E1
        private System.Void RemoveVolumeCache(HoudiniEngineUnity.HEU_VolumeCache cache) { }
        // RVA: 0x09D42DD8  token: 0x60001E2
        private System.Void AddAttributeStore(HoudiniEngineUnity.HEU_AttributesStore attributeStore) { }
        // RVA: 0x09D4FCE8  token: 0x60001E3
        private System.Void RemoveAttributeStore(HoudiniEngineUnity.HEU_AttributesStore attributeStore) { }
        // RVA: 0x09D5048C  token: 0x60001E4
        private System.Void ReorderAttributeStore(System.Int32 oldIndex, System.Int32 newIndex) { }
        // RVA: 0x09D4EF80  token: 0x60001E5
        private System.Void PopulateObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> objInstanceInfos) { }
        // RVA: 0x09D4313C  token: 0x60001E6
        private System.Void AddToAssetDBCache(System.String assetObjectFileName, UnityEngine.Object objectToAdd, System.String relativeFolderPath, UnityEngine.Object& targetAssetDBObject) { }
        // RVA: 0x09D50DA0  token: 0x60001E7
        private static System.Void SetCurvesVisibilityInScene(System.Boolean bShow) { }
        // RVA: 0x09D4C1EC  token: 0x60001E8
        private System.String GetValidAssetCacheFolderPath() { }
        // RVA: 0x09D450C4  token: 0x60001E9
        private System.Void CalculateVisibility() { }
        // RVA: 0x09D44FAC  token: 0x60001EA
        private System.Void CalculateColliderState() { }
        // RVA: 0x09D4E8B0  token: 0x60001EB
        private System.Void LoadAssetPresetAndCook(HoudiniEngineUnity.HEU_AssetPreset assetPreset) { }
        // RVA: 0x09D43544  token: 0x60001EC
        private System.Void ApplyRecookPreset() { }
        // RVA: 0x09D4322C  token: 0x60001ED
        private System.Boolean ApplyInputPresets(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputPreset> inputPresets, System.Boolean bAddMissingInputsToRecookPreset) { }
        // RVA: 0x09D4C2C8  token: 0x60001EE
        private HoudiniEngineUnity.HEU_VolumeCachePreset GetVolumeCachePreset(System.String objName, System.String geoName, System.Int32 tile) { }
        // RVA: 0x09D503A4  token: 0x60001EF
        private System.Void RemoveVolumeCachePreset(HoudiniEngineUnity.HEU_VolumeCachePreset preset) { }
        // RVA: 0x09D435E4  token: 0x60001F0
        private System.Boolean ApplyVolumeCachePresets(System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCachePreset> volumeCachePresets) { }
        // RVA: 0x09D51908  token: 0x60001F1
        private System.Void SyncInternalParametersForUndoCompare() { }
        // RVA: 0x09D525AC  token: 0x60001F2
        private System.Boolean UpdateSessionSync() { }
        // RVA: 0x09D52604  token: 0x60001F3
        private System.Void UpdateTotalCookCount() { }
        // RVA: 0x09D505A4  token: 0x60001F4
        private System.Void ResetAndCopyInstantiatedProperties(HoudiniEngineUnity.HEU_HoudiniAsset newAsset) { }
        // RVA: 0x09D4AE7C  token: 0x60001F5
        private HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethod GetInstantiationMethod() { }
        // RVA: 0x09D4ADF0  token: 0x60001F6
        private HoudiniEngineUnity.HEU_HoudiniAsset GetInstantiatedObject() { }
        // RVA: 0x09D45928  token: 0x60001F7
        private System.Void ClearInvalidLists() { }
        // RVA: 0x09D4621C  token: 0x60001F8
        private System.Void CopyPropertiesTo(HoudiniEngineUnity.HEU_HoudiniAsset newAsset) { }
        // RVA: 0x09D50F70  token: 0x60001F9
        private System.Void SetSoftDeleted() { }
        // RVA: 0x09D516DC  token: 0x60001FA
        private System.Void SyncChildTransforms() { }
        // RVA: 0x09D439BC  token: 0x60001FB
        private static HoudiniEngineUnity.HEU_AssetCookStatusWrapper AssetCookStatus_InternalToWrappper(HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus assetCookStatus) { }
        // RVA: 0x09D439BC  token: 0x60001FC
        private static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus AssetCookStatus_WrapperToInternal(HoudiniEngineUnity.HEU_AssetCookStatusWrapper assetCookStatus) { }
        // RVA: 0x09D4399C  token: 0x60001FD
        private static HoudiniEngineUnity.HEU_AssetCookResultWrapper AssetCookResult_InternalToWrapper(HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult assetCookResult) { }
        // RVA: 0x09D4399C  token: 0x60001FE
        private static HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult AssetCookResult_WrapperToInternal(HoudiniEngineUnity.HEU_AssetCookResultWrapper assetCookResult) { }
        // RVA: 0x09D4798C  token: 0x60001FF
        private static HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper CurveDrawCollision_InternalToWrapper(HoudiniEngineUnity.HEU_Curve.CurveDrawCollision curveDrawCollision) { }
        // RVA: 0x09D479A4  token: 0x6000200
        private static HoudiniEngineUnity.HEU_Curve.CurveDrawCollision CurveDrawCollision_WrapperToInternal(HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper curveDrawCollision) { }
        // RVA: 0x09D43A04  token: 0x6000201
        private static HoudiniEngineUnity.HEU_AssetTypeWrapper AssetType_InternalToWrapper(HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType assetType) { }
        // RVA: 0x09D43A04  token: 0x6000202
        private static HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType AssetType_WrapperToInternal(HoudiniEngineUnity.HEU_AssetTypeWrapper assetType) { }
        // RVA: 0x09D4D4CC  token: 0x6000203
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_HoudiniAsset asset) { }
        // RVA: 0x09D5335C  token: 0x6000204
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000039  // size: 0x28
    public class HEU_HoudiniAssetRoot : UnityEngine.MonoBehaviour
    {
        // Fields
        private HoudiniEngineUnity.HEU_HoudiniAsset _houdiniAsset;  // 0x18
        private System.Collections.Generic.List<UnityEngine.GameObject> _bakeTargets;  // 0x20

        // Properties
        HoudiniEngineUnity.HEU_HoudiniAsset HoudiniAsset { get; /* RVA: 0x0385B100 */ }
        System.Collections.Generic.List<UnityEngine.GameObject> BakeTargets { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x09D58C04  token: 0x6000219
        private System.Void OnDestroy() { }
        // RVA: 0x09D58CA8  token: 0x600021A
        private System.Void RemoveHoudiniEngineAssetData() { }
        // RVA: 0x09D58BA0  token: 0x600021B
        private System.Void ClearHoudiniEngineReferences() { }
        // RVA: 0x09D58BF4  token: 0x600021C
        private static System.Void DestroyRootComponent(HoudiniEngineUnity.HEU_HoudiniAssetRoot assetRoot) { }
        // RVA: 0x09D58D54  token: 0x600021D
        private System.Void Reset() { }
        // RVA: 0x09D58E10  token: 0x600021E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003A  // size: 0xB0
    public class HEU_InputNode : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_InputNode, HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private HoudiniEngineUnity.HEU_InputNode.InputNodeType _inputNodeType;  // 0x18
        private HoudiniEngineUnity.HEU_InputNode.InputObjectType _inputObjectType;  // 0x1c
        private HoudiniEngineUnity.HEU_InputNode.InputObjectType _pendingInputObjectType;  // 0x20
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputObjectInfo> _inputObjects;  // 0x28
        private System.Collections.Generic.List<System.Int32> _inputObjectsConnectedAssetIDs;  // 0x30
        private UnityEngine.GameObject _inputAsset;  // 0x38
        private UnityEngine.GameObject _connectedInputAsset;  // 0x40
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputHDAInfo> _inputAssetInfos;  // 0x48
        private System.Int32 _nodeID;  // 0x50
        private System.Int32 _inputIndex;  // 0x54
        private System.Boolean _requiresCook;  // 0x58
        private System.Boolean _requiresUpload;  // 0x59
        private System.String _inputName;  // 0x60
        private System.String _labelName;  // 0x68
        private System.String _paramName;  // 0x70
        private System.Int32 _connectedNodeID;  // 0x78
        private System.Boolean _keepWorldTransform;  // 0x7c
        private System.Boolean _packGeometryBeforeMerging;  // 0x7d
        private HoudiniEngineUnity.HEU_HoudiniAsset _parentAsset;  // 0x80
        private HoudiniEngineUnity.HEU_InputInterfaceMeshSettings _meshSettings;  // 0x88
        private HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings _tilemapSettings;  // 0x90
        private HoudiniEngineUnity.HEU_InputInterfaceSplineSettings _splineSettings;  // 0x98
        private System.Boolean _usingSelectFromHierarchy;  // 0xa0
        public HoudiniEngineUnity.HEU_InputNodeUICache _uiCache;  // 0xa8

        // Properties
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x04D86260 */ }
        System.Boolean KeepWorldTransform { get; /* RVA: 0x04D92E50 */ set; /* RVA: 0x04D92E60 */ }
        System.Boolean PackGeometryBeforeMerging { get; /* RVA: 0x04DA26A0 */ set; /* RVA: 0x04DA26B0 */ }
        HoudiniEngineUnity.HEU_InputNodeTypeWrapper NodeType { get; /* RVA: 0x09D5DBC0 */ }
        HoudiniEngineUnity.HEU_InputObjectTypeWrapper ObjectType { get; /* RVA: 0x09D5DBE0 */ }
        HoudiniEngineUnity.HEU_InputObjectTypeWrapper PendingObjectType { get; /* RVA: 0x09D5DBEC */ set; /* RVA: 0x09D5DBF8 */ }
        System.Int32 InputNodeID { get; /* RVA: 0x04D86550 */ }
        System.String InputName { get; /* RVA: 0x04D86280 */ }
        System.String LabelName { get; /* RVA: 0x04D86230 */ }
        System.String ParamName { get; /* RVA: 0x04D862B0 */ }
        HoudiniEngineUnity.HEU_InputInterfaceMeshSettings MeshSettings { get; /* RVA: 0x04D861F0 */ }
        HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings TilemapSettings { get; /* RVA: 0x04D86220 */ }
        HoudiniEngineUnity.HEU_InputInterfaceSplineSettings SplineSettings { get; /* RVA: 0x04D85EE0 */ }
        HoudiniEngineUnity.HEU_InputNode.InputNodeType InputType { get; /* RVA: 0x011EF5B0 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputObjectInfo> InputObjects { get; /* RVA: 0x04D86240 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputHDAInfo> InputAssetInfos { get; /* RVA: 0x04D86200 */ }
        System.Boolean RequiresCook { get; /* RVA: 0x04D86F70 */ set; /* RVA: 0x04D86F80 */ }
        System.Boolean RequiresUpload { get; /* RVA: 0x04D8EF10 */ set; /* RVA: 0x04D8EF20 */ }

        // Methods
        // RVA: 0x09D5A3AC  token: 0x6000236
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: 0x09D5C1F8  token: 0x6000237
        public virtual System.Void Recook() { }
        // RVA: 0x09D5ADF4  token: 0x6000238
        public virtual System.Boolean IsAssetInput() { }
        // RVA: 0x09D5B844  token: 0x6000239
        public virtual System.Int32 NumInputEntries() { }
        // RVA: 0x09D59FFC  token: 0x600023A
        public virtual UnityEngine.GameObject GetInputEntryGameObject(System.Int32 index) { }
        // RVA: 0x09D5A210  token: 0x600023B
        public virtual UnityEngine.GameObject[] GetInputEntryGameObjects() { }
        // RVA: 0x09D5C9BC  token: 0x600023C
        public virtual System.Void SetInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset) { }
        // RVA: 0x09D5AAC0  token: 0x600023D
        public virtual System.Void InsertInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset) { }
        // RVA: 0x09D59070  token: 0x600023E
        public virtual System.Void AddInputEntryAtEnd(UnityEngine.GameObject newEntryGameObject, System.Boolean bRecookAsset) { }
        // RVA: 0x09D5C544  token: 0x600023F
        public virtual System.Void ResetInputNode(System.Boolean bRecookAsset) { }
        // RVA: 0x09D59250  token: 0x6000240
        public virtual System.Void ChangeInputType(HoudiniEngineUnity.HEU_InputObjectTypeWrapper newType, System.Boolean bRecookAsset) { }
        // RVA: 0x09D5C2CC  token: 0x6000241
        public virtual System.Void RemoveInputEntry(System.Int32 index, System.Boolean bRecookAsset) { }
        // RVA: 0x09D5C264  token: 0x6000242
        public virtual System.Void RemoveAllInputEntries(System.Boolean bRecookAsset) { }
        // RVA: 0x09D5C904  token: 0x6000243
        public virtual System.Void SetInputEntryObjectUseTransformOffset(System.Int32 index, System.Boolean value, System.Boolean bRecookAsset) { }
        // RVA: 0x09D5C840  token: 0x6000244
        public virtual System.Void SetInputEntryObjectTransformTranslateOffset(System.Int32 index, UnityEngine.Vector3 translateOffset, System.Boolean bRecookAsset) { }
        // RVA: 0x09D5C6B0  token: 0x6000245
        public virtual System.Void SetInputEntryObjectTransformRotateOffset(System.Int32 index, UnityEngine.Vector3 rotateOffset, System.Boolean bRecookAsset) { }
        // RVA: 0x09D5C778  token: 0x6000246
        public virtual System.Void SetInputEntryObjectTransformScaleOffset(System.Int32 index, UnityEngine.Vector3 scaleOffset, System.Boolean bRecookAsset) { }
        // RVA: 0x09D59110  token: 0x6000247
        public virtual System.Boolean AreAnyInputHDAsConnected() { }
        // RVA: 0x09D59ECC  token: 0x6000248
        public virtual System.Int32 GetConnectedInputCount() { }
        // RVA: 0x09D59F40  token: 0x6000249
        public virtual System.Int32 GetConnectedNodeID(System.Int32 index) { }
        // RVA: 0x09D5B1E0  token: 0x600024A
        public virtual System.Void LoadPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset) { }
        // RVA: 0x09D5B8B8  token: 0x600024B
        public virtual System.Void PopulateInputPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset, System.Boolean sceneRelativeGameObjects) { }
        // RVA: 0x09D599F4  token: 0x600024C
        private static HoudiniEngineUnity.HEU_InputNode CreateSetupInput(System.Int32 nodeID, System.Int32 inputIndex, System.String inputName, System.String labelName, HoudiniEngineUnity.HEU_InputNode.InputNodeType inputNodeType, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x04D865A0  token: 0x600024D
        private System.Void SetInputNodeID(System.Int32 nodeID) { }
        // RVA: 0x09D59AA8  token: 0x600024E
        private System.Void DestroyAllData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5C5D4  token: 0x600024F
        private System.Void ResetInputObjectTransforms() { }
        // RVA: 0x09D5C588  token: 0x6000250
        private System.Void ResetInputNode(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D59034  token: 0x6000251
        private HoudiniEngineUnity.HEU_InputObjectInfo AddInputEntryAtEndMesh(UnityEngine.GameObject newEntryGameObject) { }
        // RVA: 0x09D58FF8  token: 0x6000252
        private HoudiniEngineUnity.HEU_InputHDAInfo AddInputEntryAtEndHDA(UnityEngine.GameObject newEntryGameObject) { }
        // RVA: 0x09D592B8  token: 0x6000253
        private System.Void ChangeInputType(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputNode.InputObjectType newType) { }
        // RVA: 0x09D5C4F4  token: 0x6000254
        private System.Void ResetConnectionForForceUpdate(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5D008  token: 0x6000255
        private System.Void UploadInput(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5CD84  token: 0x6000256
        private System.Void UploadHDAInput(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5D4C0  token: 0x6000257
        private System.Void UploadUnityInput(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5BFCC  token: 0x6000258
        private System.Void ReconnectToUpstreamAsset() { }
        // RVA: 0x09D59980  token: 0x6000259
        private HoudiniEngineUnity.HEU_InputObjectInfo CreateInputObjectInfo(UnityEngine.GameObject inputGameObject) { }
        // RVA: 0x09D59918  token: 0x600025A
        private HoudiniEngineUnity.HEU_InputHDAInfo CreateInputHDAInfo(UnityEngine.GameObject inputGameObject) { }
        // RVA: 0x09D5AD88  token: 0x600025B
        private HoudiniEngineUnity.HEU_InputObjectInfo InternalAddInputObjectAtEnd(UnityEngine.GameObject newInputGameObject) { }
        // RVA: 0x09D5AD1C  token: 0x600025C
        private HoudiniEngineUnity.HEU_InputHDAInfo InternalAddInputHDAAtEnd(UnityEngine.GameObject newInputHDA) { }
        // RVA: 0x09D59C7C  token: 0x600025D
        private System.Void DisconnectConnectedMergeNode(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D592E8  token: 0x600025E
        private System.Void ClearConnectedInputHDAs() { }
        // RVA: 0x09D594CC  token: 0x600025F
        private System.Void ConnectToMergeObject(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D59AEC  token: 0x6000260
        private System.Void DisconnectAndDestroyInputs(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5D434  token: 0x6000261
        private System.Boolean UploadObjectMergeTransformType(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5D3A8  token: 0x6000262
        private System.Boolean UploadObjectMergePackGeometry(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5A604  token: 0x6000263
        private System.Boolean HasInputNodeTransformChanged() { }
        // RVA: 0x09D5CEAC  token: 0x6000264
        private System.Void UploadInputObjectTransforms(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5CC18  token: 0x6000265
        private System.Void UpdateOnAssetRecreation(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D59570  token: 0x6000266
        private System.Void CopyInputValuesTo(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputNode destInputNode) { }
        // RVA: 0x09D5B218  token: 0x6000267
        private System.Void LoadPreset(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputPreset inputPreset) { }
        // RVA: 0x09D59DB0  token: 0x6000268
        private System.Boolean FindAddToInputHDA(System.String gameObjectName) { }
        // RVA: 0x09D5B7CC  token: 0x6000269
        private System.Void NotifyParentRemovedInput() { }
        // RVA: 0x09D594B4  token: 0x600026A
        private System.Void ClearUICache() { }
        // RVA: 0x09D5A524  token: 0x600026B
        private System.Void HandleSelectedObjectsForInputObjects(UnityEngine.GameObject[] selectedObjects) { }
        // RVA: 0x09D5A444  token: 0x600026C
        private System.Void HandleSelectedObjectsForInputHDAs(UnityEngine.GameObject[] selectedObjects) { }
        // RVA: 0x09D5ADFC  token: 0x600026D
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InputNode other) { }
        // RVA: 0x09D5A368  token: 0x600026E
        private static HoudiniEngineUnity.HEU_InputNode.InternalObjectType GetInternalObjectType(HoudiniEngineUnity.HEU_InputNode.InputObjectType type) { }
        // RVA: 0x09D4399C  token: 0x600026F
        private static HoudiniEngineUnity.HEU_InputNodeTypeWrapper InputNodeType_InternalToWrapper(HoudiniEngineUnity.HEU_InputNode.InputNodeType inputNodeType) { }
        // RVA: 0x09D4399C  token: 0x6000270
        private static HoudiniEngineUnity.HEU_InputNode.InputNodeType InputNodeType_InternalToWrapper(HoudiniEngineUnity.HEU_InputNodeTypeWrapper inputNodeType) { }
        // RVA: 0x09D5AA70  token: 0x6000271
        private static HoudiniEngineUnity.HEU_InputObjectTypeWrapper InputObjectType_InternalToWrapper(HoudiniEngineUnity.HEU_InputNode.InputObjectType inputType) { }
        // RVA: 0x09D5AA70  token: 0x6000272
        private static HoudiniEngineUnity.HEU_InputNode.InputObjectType InputObjectType_WrapperToInternal(HoudiniEngineUnity.HEU_InputObjectTypeWrapper inputType) { }
        // RVA: 0x09D5D9F0  token: 0x6000273
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003F  // size: 0xA8
    public class HEU_InputObjectInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.GameObject _gameObject;  // 0x10
        public UnityEngine.Terrain _terrainReference;  // 0x18
        public HEU_BoundingVolume _boundingVolumeReference;  // 0x20
        public UnityEngine.Tilemaps.Tilemap _tilemapReference;  // 0x28
        public UnityEngine.Matrix4x4 _syncdTransform;  // 0x30
        public System.Collections.Generic.List<UnityEngine.Matrix4x4> _syncdChildTransforms;  // 0x70
        public System.Boolean _useTransformOffset;  // 0x78
        public UnityEngine.Vector3 _translateOffset;  // 0x7c
        public UnityEngine.Vector3 _rotateOffset;  // 0x88
        public UnityEngine.Vector3 _scaleOffset;  // 0x94
        public System.Type _inputInterfaceType;  // 0xa0

        // Methods
        // RVA: 0x09D5DC18  token: 0x6000274
        public System.Void CopyTo(HoudiniEngineUnity.HEU_InputObjectInfo destObject) { }
        // RVA: 0x09D5DF78  token: 0x6000275
        private System.Void SetReferencesFromGameObject() { }
        // RVA: 0x09D5DD1C  token: 0x6000276
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InputObjectInfo other) { }
        // RVA: 0x09D5E060  token: 0x6000277
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x28
    public class HEU_InputHDAInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.GameObject _pendingGO;  // 0x10
        public UnityEngine.GameObject _connectedGO;  // 0x18
        public System.Int32 _connectedInputNodeID;  // 0x20
        public System.Int32 _connectedMergeNodeID;  // 0x24

        // Methods
        // RVA: 0x09D58E90  token: 0x6000278
        public System.Void CopyTo(HoudiniEngineUnity.HEU_InputHDAInfo destInfo) { }
        // RVA: 0x09D58ED4  token: 0x6000279
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InputHDAInfo other) { }
        // RVA: 0x04DA2690  token: 0x600027A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000041  // size: 0x20
    public class HEU_InputNodeUICache
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNodeUICache.HEU_InputObjectUICache> _inputObjectCache;  // 0x10
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNodeUICache.HEU_InputAssetUICache> _inputAssetCache;  // 0x18

        // Methods
        // RVA: 0x09D58F38  token: 0x600027B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x28
    public class HEU_InstanceInputUIState : UnityEngine.ScriptableObject, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.Boolean _showInstanceInputs;  // 0x18
        public System.Int32 _numInputsToShowUI;  // 0x1c
        public System.Int32 _inputsPageIndexUI;  // 0x20

        // Methods
        // RVA: 0x09D5E160  token: 0x600027E
        private System.Void CopyTo(HoudiniEngineUnity.HEU_InstanceInputUIState dest) { }
        // RVA: 0x09D5E188  token: 0x600027F
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InstanceInputUIState other) { }
        // RVA: 0x09D5E2E0  token: 0x6000280
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x40
    public class HEU_ObjectInstanceInfo : UnityEngine.ScriptableObject, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_InstancedInput> _instancedInputs;  // 0x18
        public HoudiniEngineUnity.HEU_PartData _partTarget;  // 0x20
        public System.Int32 _instancedObjectNodeID;  // 0x28
        public System.String _instancedObjectPath;  // 0x30
        public System.Collections.Generic.List<UnityEngine.GameObject> _instances;  // 0x38

        // Methods
        // RVA: 0x09D5E4AC  token: 0x6000281
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ObjectInstanceInfo other) { }
        // RVA: 0x09D5E5CC  token: 0x6000282
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000046  // size: 0x30
    public class HEU_InstancedInput : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.GameObject _instancedGameObject;  // 0x10
        public UnityEngine.Vector3 _rotationOffset;  // 0x18
        public UnityEngine.Vector3 _scaleOffset;  // 0x24

        // Methods
        // RVA: 0x09D5E2F4  token: 0x6000283
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InstancedInput other) { }
        // RVA: 0x09D5E480  token: 0x6000284
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000047  // size: 0x80
    public class HEU_ObjectNode : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_ObjectNode, HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.String _objName;  // 0x18
        private HoudiniEngineUnity.HEU_HoudiniAsset _parentAsset;  // 0x20
        private HoudiniEngineUnity.HAPI_ObjectInfo _objectInfo;  // 0x28
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> _geoNodes;  // 0x48
        private HoudiniEngineUnity.HAPI_Transform _objectTransform;  // 0x50
        private System.Collections.Generic.List<System.Int32> _recentlyDestroyedParts;  // 0x78

        // Properties
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x04D862C0 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D86640 */ }
        System.String ObjectName { get; /* RVA: 0x0385B100 */ }
        HoudiniEngineUnity.HAPI_ObjectInfo ObjectInfo { get; /* RVA: 0x04DA26E0 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> GeoNodes { get; /* RVA: 0x04D86200 */ }
        HoudiniEngineUnity.HAPI_Transform ObjectTransform { get; /* RVA: 0x04DA2700 */ }

        // Methods
        // RVA: 0x09D607B4  token: 0x600028B
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: 0x09D61298  token: 0x600028C
        public virtual System.Void Recook() { }
        // RVA: 0x04D86A90  token: 0x600028D
        public virtual System.Boolean IsInstanced() { }
        // RVA: 0x04D86DC0  token: 0x600028E
        public virtual System.Boolean IsVisible() { }
        // RVA: 0x09D60EF8  token: 0x600028F
        public virtual System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData) { }
        // RVA: 0x09D60418  token: 0x6000290
        public virtual System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: 0x09D60690  token: 0x6000291
        public virtual System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: 0x09D602C0  token: 0x6000292
        public virtual HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: 0x09D60180  token: 0x6000293
        public virtual HoudiniEngineUnity.HEU_GeoNode GetGeoNode(System.String geoName) { }
        // RVA: 0x09D5FDB4  token: 0x6000294
        public virtual System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly) { }
        // RVA: 0x09D6053C  token: 0x6000295
        public virtual System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outGeoNodes) { }
        // RVA: 0x09D60848  token: 0x6000296
        public virtual System.Void HideAllGeometry() { }
        // RVA: 0x09D5ED20  token: 0x6000297
        public virtual System.Void DisableAllColliders() { }
        // RVA: 0x09D60DD4  token: 0x6000298
        public virtual System.Boolean IsInstancer() { }
        // RVA: 0x09D616B4  token: 0x6000299
        public System.Void .ctor() { }
        // RVA: 0x09D612FC  token: 0x600029A
        private System.Void Reset() { }
        // RVA: 0x09D613FC  token: 0x600029B
        private System.Void SyncWithObjectInfo(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D6095C  token: 0x600029C
        private System.Void Initialize(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo objectInfo, HoudiniEngineUnity.HAPI_Transform objectTranform, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bUseOutputNodes, System.Boolean bGetEditableNodes) { }
        // RVA: 0x09D5EE34  token: 0x600029D
        private System.Void GatherAllAssetOutputsLegacy(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo objectInfo, System.Boolean bUseOutputNodes, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode>& geoNodes) { }
        // RVA: 0x09D5EC48  token: 0x600029E
        private System.Void DestroyAllData(System.Boolean bIsRebuild) { }
        // RVA: 0x09D5EB9C  token: 0x600029F
        private HoudiniEngineUnity.HEU_GeoNode CreateGeoNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_GeoInfo geoInfo) { }
        // RVA: 0x09D5FEE8  token: 0x60002A0
        private System.Void GetDebugInfo(System.Text.StringBuilder sb) { }
        // RVA: 0x04DA26C0  token: 0x60002A1
        private System.Void SetObjectInfo(HoudiniEngineUnity.HAPI_ObjectInfo newObjectInfo) { }
        // RVA: 0x09D61528  token: 0x60002A2
        private System.Void UpdateObject(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bForceUpdate) { }
        // RVA: 0x09D5F004  token: 0x60002A3
        private System.Void GenerateGeometry(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bRebuild) { }
        // RVA: 0x09D5FB4C  token: 0x60002A4
        private System.Void GeneratePartInstances(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5EEE0  token: 0x60002A5
        private System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5E698  token: 0x60002A6
        private System.Void ApplyObjectTransformToGeoNodes() { }
        // RVA: 0x09D5FC70  token: 0x60002A7
        private System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts) { }
        // RVA: 0x09D5F76C  token: 0x60002A8
        private System.Void GenerateObjectInstances(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5E9EC  token: 0x60002A9
        private System.Void ClearObjectInstances(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D61024  token: 0x60002AA
        private System.Void PopulateObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> objInstanceInfos) { }
        // RVA: 0x09D61174  token: 0x60002AB
        private System.Void ProcessUnityScriptAttributes(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D5E8CC  token: 0x60002AC
        private System.Void CalculateVisibility() { }
        // RVA: 0x09D5E7B8  token: 0x60002AD
        private System.Void CalculateColliderState() { }
        // RVA: 0x09D614CC  token: 0x60002AE
        public virtual System.String ToString() { }
        // RVA: 0x09D60B5C  token: 0x60002AF
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ObjectNode other) { }

    }

    // TypeToken: 0x2000048  // size: 0xA0
    public class HEU_PartData : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_PartData, HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.Int32 _partID;  // 0x18
        private System.String _partName;  // 0x20
        private System.Int32 _objectNodeID;  // 0x28
        private System.Int32 _geoID;  // 0x2c
        private HoudiniEngineUnity.HAPI_PartType _partType;  // 0x30
        private HoudiniEngineUnity.HEU_GeoNode _geoNode;  // 0x38
        private System.Boolean _isAttribInstancer;  // 0x40
        private System.Boolean _isPartInstanced;  // 0x41
        private System.Int32 _partPointCount;  // 0x44
        private System.Boolean _isObjectInstancer;  // 0x48
        private System.Boolean _objectInstancesGenerated;  // 0x49
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> _objectInstanceInfos;  // 0x50
        private UnityEngine.Vector3 _terrainOffsetPosition;  // 0x58
        private UnityEngine.Object _assetDBTerrainData;  // 0x68
        private System.Boolean _isPartEditable;  // 0x70
        private HoudiniEngineUnity.HEU_PartData.PartOutputType _partOutputType;  // 0x74
        private HoudiniEngineUnity.HEU_Curve _curve;  // 0x78
        private HoudiniEngineUnity.HEU_AttributesStore _attributesStore;  // 0x80
        private System.Boolean _haveInstancesBeenGenerated;  // 0x88
        private System.Int32 _meshVertexCount;  // 0x8c
        private HoudiniEngineUnity.HEU_GeneratedOutput _generatedOutput;  // 0x90
        private System.String _volumeLayerName;  // 0x98

        // Properties
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x09D68F14 */ }
        System.Int32 PartID { get; /* RVA: 0x011EF5B0 */ }
        System.String PartName { get; /* RVA: 0x04D862C0 */ }
        System.Int32 GeoID { get; /* RVA: 0x04D86710 */ }
        HoudiniEngineUnity.HAPI_PartType PartType { get; /* RVA: 0x04D864F0 */ }
        HoudiniEngineUnity.HEU_GeoNode ParentGeoNode { get; /* RVA: 0x04D85A50 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> ObjectInstanceInfos { get; /* RVA: 0x04D86270 */ }
        HoudiniEngineUnity.HEU_Curve Curve { get; /* RVA: 0x04D862A0 */ }
        System.Int32 MeshVertexCount { get; /* RVA: 0x04D90C40 */ }
        HoudiniEngineUnity.HEU_GeneratedOutput GeneratedOutput { get; /* RVA: 0x04D86220 */ }
        UnityEngine.GameObject OutputGameObject { get; /* RVA: 0x09D68EEC */ }

        // Methods
        // RVA: 0x09D68E68  token: 0x60002BB
        public System.Void .ctor() { }
        // RVA: 0x09D67B78  token: 0x60002BC
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: 0x09D684C8  token: 0x60002BD
        public virtual System.Void Recook() { }
        // RVA: 0x09D682C8  token: 0x60002BE
        public virtual System.Boolean IsPartInstancer() { }
        // RVA: 0x02D480A0  token: 0x60002BF
        public virtual System.Boolean IsAttribInstancer() { }
        // RVA: 0x09D682AC  token: 0x60002C0
        public virtual System.Boolean IsInstancerAnyType() { }
        // RVA: 0x04D86F40  token: 0x60002C1
        public virtual System.Boolean IsPartInstanced() { }
        // RVA: 0x04D86530  token: 0x60002C2
        public virtual System.Int32 GetPartPointCount() { }
        // RVA: 0x04D867D0  token: 0x60002C3
        public virtual System.Boolean IsObjectInstancer() { }
        // RVA: 0x09D682D8  token: 0x60002C4
        public virtual System.Boolean IsPartVolume() { }
        // RVA: 0x09D682C0  token: 0x60002C5
        public virtual System.Boolean IsPartCurve() { }
        // RVA: 0x09D682D0  token: 0x60002C6
        public virtual System.Boolean IsPartMesh() { }
        // RVA: 0x04D86A50  token: 0x60002C7
        public virtual System.Boolean IsPartEditable() { }
        // RVA: 0x011F2750  token: 0x60002C8
        public virtual System.Boolean HaveInstancesBeenGenerated() { }
        // RVA: 0x09D68560  token: 0x60002C9
        public virtual System.Void SetGameObjectName(System.String partName) { }
        // RVA: 0x09D68758  token: 0x60002CA
        public virtual System.Void SetGameObject(UnityEngine.GameObject gameObject) { }
        // RVA: 0x06352D0C  token: 0x60002CB
        public virtual System.Void SetVolumeLayerName(System.String name) { }
        // RVA: 0x04D85EE0  token: 0x60002CC
        public virtual System.String GetVolumeLayerName() { }
        // RVA: 0x09D65174  token: 0x60002CD
        public virtual System.Void DestroyAllData(System.Boolean bIsRebuild) { }
        // RVA: 0x09D682E0  token: 0x60002CE
        public virtual System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData) { }
        // RVA: 0x09D67A6C  token: 0x60002CF
        public virtual System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: 0x09D67B10  token: 0x60002D0
        public virtual System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: 0x09D67888  token: 0x60002D1
        public virtual HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject inGameObject) { }
        // RVA: 0x09D6250C  token: 0x60002D2
        public System.Void CalculateVisibility(System.Boolean bParentVisibility, System.Boolean bParentDisplayGeo) { }
        // RVA: 0x09D62638  token: 0x60002D3
        public virtual System.Void ClearInstances() { }
        // RVA: 0x09D6763C  token: 0x60002D4
        public virtual HoudiniEngineUnity.HEU_Curve GetCurve(System.Boolean bEditableOnly) { }
        // RVA: 0x09D68A50  token: 0x60002D5
        public virtual System.Void SetVisiblity(System.Boolean bVisibility) { }
        // RVA: 0x09D6853C  token: 0x60002D6
        public virtual System.Void SetColliderState(System.Boolean bEnabled) { }
        // RVA: 0x09D679A8  token: 0x60002D7
        public virtual HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectPath(System.String path) { }
        // RVA: 0x09D678F8  token: 0x60002D8
        public virtual HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectID(System.Int32 objNodeID) { }
        // RVA: 0x04DA2720  token: 0x60002D9
        public virtual System.Void SetTerrainOffsetPosition(UnityEngine.Vector3 offsetPosition) { }
        // RVA: 0x09D688AC  token: 0x60002DA
        public virtual System.Void SetTerrainData(UnityEngine.TerrainData terrainData, System.String exportPathRelative, System.String exportPathUser) { }
        // RVA: 0x09D67C1C  token: 0x60002DB
        private System.Void Initialize(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partID, System.Int32 geoID, System.Int32 objectNodeID, HoudiniEngineUnity.HEU_GeoNode geoNode, HoudiniEngineUnity.HAPI_PartInfo& partInfo, HoudiniEngineUnity.HEU_PartData.PartOutputType partOutputType, System.Boolean isEditable, System.Boolean isObjectInstancer, System.Boolean isAttribInstancer) { }
        // RVA: 0x09D6177C  token: 0x60002DC
        private System.Void ApplyHAPITransform(HoudiniEngineUnity.HAPI_Transform& hapiTransform) { }
        // RVA: 0x09D676C0  token: 0x60002DD
        private System.Void GetDebugInfo(System.Text.StringBuilder sb) { }
        // RVA: 0x09D674FC  token: 0x60002DE
        private System.Void GetClonableObjects(System.Collections.Generic.List<UnityEngine.GameObject> clonableObjects) { }
        // RVA: 0x09D675A0  token: 0x60002DF
        private System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts) { }
        // RVA: 0x04D86810  token: 0x60002E0
        private System.Void SetObjectInstancer(System.Boolean bObjectInstancer) { }
        // RVA: 0x09D62940  token: 0x60002E1
        private System.Void ClearObjectInstanceInfos() { }
        // RVA: 0x09D62720  token: 0x60002E2
        private System.Void ClearInvalidObjectInstanceInfos() { }
        // RVA: 0x09D62538  token: 0x60002E3
        private System.Void ClearGeneratedData() { }
        // RVA: 0x09D62554  token: 0x60002E4
        private System.Void ClearGeneratedMeshOutput() { }
        // RVA: 0x09D625F0  token: 0x60002E5
        private System.Void ClearGeneratedVolumeOutput() { }
        // RVA: 0x09D67368  token: 0x60002E6
        private System.Boolean GeneratePartInstances(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D6550C  token: 0x60002E7
        private System.Void GenerateInstancesFromObjectID(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 objectNodeID, System.String[] instancePrefixes, System.String[] instanceMaterialPaths) { }
        // RVA: 0x09D65FF0  token: 0x60002E8
        private System.Void GenerateInstancesFromObject(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_ObjectNode sourceObject, System.String[] instancePrefixes, System.String[] instanceMaterialPaths) { }
        // RVA: 0x09D65974  token: 0x60002E9
        private System.Void GenerateInstancesFromObjectIds(HoudiniEngineUnity.HEU_SessionBase session, System.String[] instancePrefixes, System.String[] instanceMaterialPaths) { }
        // RVA: 0x09D66364  token: 0x60002EA
        private System.Void GenerateInstancesFromUnityAssetPathAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.String unityInstanceAttr) { }
        // RVA: 0x09D64C34  token: 0x60002EB
        private System.Void CreateNewInstanceFromObject(UnityEngine.GameObject sourceObject, System.Int32 instanceIndex, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HAPI_Transform& hapiTransform, System.Int32 instancedObjectNodeID, System.String instancedObjectPath, UnityEngine.Vector3 rotationOffset, UnityEngine.Vector3 scaleOffset, System.String[] instancePrefixes, System.String[] instanceMaterialPaths, UnityEngine.GameObject collisionSrcGO, System.Boolean copyParentFlags) { }
        // RVA: 0x09D65470  token: 0x60002EC
        private System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D62270  token: 0x60002ED
        private System.Void CalculateColliderState() { }
        // RVA: 0x09D62E60  token: 0x60002EE
        private static System.Void CopyGameObjectComponents(HoudiniEngineUnity.HEU_PartData partData, UnityEngine.GameObject sourceGO, UnityEngine.GameObject targetGO, System.String assetName, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Collections.Generic.List<HoudiniEngineUnity.TransformData> lodTransformValues) { }
        // RVA: 0x09D62AB0  token: 0x60002EF
        private static System.Void CopyChildGameObjects(HoudiniEngineUnity.HEU_PartData partData, UnityEngine.GameObject sourceGO, UnityEngine.GameObject targetGO, System.String assetName, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Boolean bKeepPreviousTransformValues) { }
        // RVA: 0x09D62130  token: 0x60002F0
        private UnityEngine.GameObject BakePartToNewGameObject(UnityEngine.Transform parentTransform, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bReconnectPrefabInstances) { }
        // RVA: 0x09D618B4  token: 0x60002F1
        private static System.Void BakePartToGameObject(HoudiniEngineUnity.HEU_PartData partData, UnityEngine.GameObject srcGO, UnityEngine.GameObject targetGO, System.String assetName, System.Boolean bIsInstancer, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bReconnectPrefabInstances, System.Boolean bKeepPreviousTransformValues) { }
        // RVA: 0x09D61FE4  token: 0x60002F2
        private System.Void BakePartToGameObject(UnityEngine.GameObject targetGO, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bReconnectPrefabInstances, System.Boolean bKeepPreviousTransformValues) { }
        // RVA: 0x09D66F34  token: 0x60002F3
        private System.Boolean GenerateMesh(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bUseLODGroups) { }
        // RVA: 0x09D68360  token: 0x60002F4
        private System.Void ProcessCurvePart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partId) { }
        // RVA: 0x09D68CE4  token: 0x60002F5
        private System.Void SyncAttributesStore(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }
        // RVA: 0x09D68C18  token: 0x60002F6
        private System.Void SetupAttributeGeometry(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D652AC  token: 0x60002F7
        private System.Void DestroyAttributesStore() { }
        // RVA: 0x09D6830C  token: 0x60002F8
        private System.Void PopulateObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> objInstanceInfos) { }
        // RVA: 0x09D68788  token: 0x60002F9
        private System.Void SetObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> sourceObjectInstanceInfos) { }
        // RVA: 0x04D86270  token: 0x60002FA
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> GetObjectInstanceInfos() { }
        // RVA: 0x09D65028  token: 0x60002FB
        private HoudiniEngineUnity.HEU_ObjectInstanceInfo CreateObjectInstanceInfo(UnityEngine.GameObject instancedObject, System.Int32 instancedObjectNodeID, System.String instancedObjectPath) { }
        // RVA: 0x09D61740  token: 0x60002FC
        private static System.String AppendBakedCloneName(System.String name) { }
        // RVA: 0x09D68E0C  token: 0x60002FD
        public virtual System.String ToString() { }
        // RVA: 0x09D653C4  token: 0x60002FE
        private static System.Void DestroyParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> parts, System.Boolean bIsRebuild) { }
        // RVA: 0x09D65390  token: 0x60002FF
        private static System.Void DestroyPart(HoudiniEngineUnity.HEU_PartData part, System.Boolean bIsRebuild) { }
        // RVA: 0x09D629F8  token: 0x6000300
        private static System.Boolean ComposeUnityInstanceSplitHierarchy(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.Transform parentTransform, System.Int32 numInstances, UnityEngine.Transform[]& instanceToChildTransform) { }
        // RVA: 0x09D67DD8  token: 0x6000301
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_PartData other) { }

    }

    // TypeToken: 0x200004E  // size: 0x68
    public sealed class HEU_AttributeData : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_AttributeInfo _attributeInfo;  // 0x10
        public System.String _name;  // 0x38
        public HoudiniEngineUnity.HEU_AttributeData.AttributeType _attributeType;  // 0x40
        public System.Int32[] _intValues;  // 0x48
        public System.Single[] _floatValues;  // 0x50
        public System.String[] _stringValues;  // 0x58
        public HoudiniEngineUnity.HEU_AttributeData.AttributeState _attributeState;  // 0x60

        // Methods
        // RVA: 0x09D556F4  token: 0x600030B
        public System.Boolean IsColorAttribute() { }
        // RVA: 0x09D555C0  token: 0x600030C
        private System.Void CopyValuesTo(HoudiniEngineUnity.HEU_AttributeData destAttrData) { }
        // RVA: 0x09D5574C  token: 0x600030D
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_AttributeData other) { }
        // RVA: 0x041E1670  token: 0x600030E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x98
    public class HEU_AttributesStore : UnityEngine.ScriptableObject, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.Int32 _geoID;  // 0x18
        private System.Int32 _partID;  // 0x1c
        private System.String _geoName;  // 0x20
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributeData> _attributeDatas;  // 0x28
        private System.Boolean _hasColorAttribute;  // 0x30
        private UnityEngine.Material _localMaterial;  // 0x38
        private UnityEngine.Transform _outputTransform;  // 0x40
        private UnityEngine.Vector3[] _positionAttributeValues;  // 0x48
        private System.Int32[] _vertexIndices;  // 0x50
        private UnityEngine.GameObject _outputGameObject;  // 0x58
        private UnityEngine.Mesh _outputMesh;  // 0x60
        private UnityEngine.Material[] _outputMaterials;  // 0x68
        private UnityEngine.MeshCollider _outputCollider;  // 0x70
        private UnityEngine.Mesh _outputColliderMesh;  // 0x78
        private UnityEngine.MeshCollider _outputMeshCollider;  // 0x80
        private UnityEngine.MeshCollider _localMeshCollider;  // 0x88
        private System.Boolean _outputMeshRendererInitiallyEnabled;  // 0x90
        private System.Boolean _outputMeshColliderInitiallyEnabled;  // 0x91

        // Properties
        System.Int32 GeoID { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 PartID { get; /* RVA: 0x04D86310 */ }
        System.String GeoName { get; /* RVA: 0x04D862C0 */ }
        UnityEngine.Transform OutputTransform { get; /* RVA: 0x04D85A60 */ }
        UnityEngine.Mesh OutputMesh { get; /* RVA: 0x04D86280 */ }

        // Methods
        // RVA: 0x04D865F0  token: 0x6000312
        public System.Boolean HasColorAttribute() { }
        // RVA: 0x09D55F3C  token: 0x6000315
        public System.Void DestroyAllData(HoudiniEngineUnity.HEU_HoudiniAsset asset) { }
        // RVA: 0x09D58494  token: 0x6000316
        public System.Void SyncAllAttributesFrom(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Int32 geoID, HoudiniEngineUnity.HAPI_PartInfo& partInfo, UnityEngine.GameObject outputGameObject) { }
        // RVA: 0x09D57E40  token: 0x6000317
        public System.Void SetupMeshAndMaterials(HoudiniEngineUnity.HEU_HoudiniAsset asset, HoudiniEngineUnity.HAPI_PartType partType, UnityEngine.GameObject outputGameObject) { }
        // RVA: 0x09D56A30  token: 0x6000318
        public System.Boolean HasDirtyAttributes() { }
        // RVA: 0x09D58714  token: 0x6000319
        public System.Void SyncDirtyAttributesToHoudini(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D57320  token: 0x600031A
        private System.Void PopulateAttributeData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_AttributeData attributeData, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo) { }
        // RVA: 0x09D56938  token: 0x600031B
        private System.Void GetAttributesList(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributeData> attributesList, HoudiniEngineUnity.HAPI_AttributeOwner ownerType, System.Int32 attributeCount) { }
        // RVA: 0x09D588C8  token: 0x600031C
        private System.Void UpdateAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_AttributeData attributeData) { }
        // RVA: 0x09D58778  token: 0x600031D
        private System.Void UpdateAttributeList(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributeData> attributeDataList) { }
        // RVA: 0x09D579F8  token: 0x600031E
        public System.Void RefreshUpstreamInputs(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D58A24  token: 0x600031F
        public System.Boolean UploadAttributeViaMeshInput(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09D57C10  token: 0x6000320
        private static System.Void SetAttributeDataSyncd(HoudiniEngineUnity.HEU_AttributeData attributeData) { }
        // RVA: 0x09D57BF4  token: 0x6000321
        public static System.Void SetAttributeDataDirty(HoudiniEngineUnity.HEU_AttributeData attributeData) { }
        // RVA: 0x09D55E84  token: 0x6000322
        public HoudiniEngineUnity.HEU_AttributeData CreateAttribute(System.String attributeName, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo) { }
        // RVA: 0x09D563C4  token: 0x6000323
        public HoudiniEngineUnity.HEU_AttributeData GetAttributeData(System.String name) { }
        // RVA: 0x09D56504  token: 0x6000324
        public HoudiniEngineUnity.HEU_AttributeData GetAttributeData(System.Int32 index) { }
        // RVA: 0x09D56574  token: 0x6000325
        public System.Collections.Generic.List<System.String> GetAttributeNames() { }
        // RVA: 0x09D56168  token: 0x6000326
        public System.Void EnablePaintCollider() { }
        // RVA: 0x09D56050  token: 0x6000327
        public System.Void DisablePaintCollider() { }
        // RVA: 0x09D58208  token: 0x6000328
        public System.Void ShowPaintMesh() { }
        // RVA: 0x09D56BA4  token: 0x6000329
        public System.Void HidePaintMesh() { }
        // RVA: 0x09D56B54  token: 0x600032A
        public System.Boolean HasMeshForPainting() { }
        // RVA: 0x09D569A8  token: 0x600032B
        public UnityEngine.MeshCollider GetPaintMeshCollider() { }
        // RVA: 0x09D572BC  token: 0x600032C
        public System.Void PaintAttribute(HoudiniEngineUnity.HEU_AttributeData attributeData, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 attributeIndex, System.Single paintFactor, HoudiniEngineUnity.HEU_AttributesStore.SetAttributeValueFunc setAttrFunc) { }
        // RVA: 0x09D57CA0  token: 0x600032D
        public static System.Void SetAttributeEditValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 startIndex, System.Int32[] values) { }
        // RVA: 0x09D57C2C  token: 0x600032E
        public static System.Void SetAttributeEditValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 startIndex, System.Single[] values) { }
        // RVA: 0x09D57D14  token: 0x600032F
        public static System.Void SetAttributeEditValueString(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 startIndex, System.String[] values) { }
        // RVA: 0x09D57AE4  token: 0x6000330
        public static System.Void ReplaceAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D55B08  token: 0x6000331
        public static System.Void AddAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D583C8  token: 0x6000332
        public static System.Void SubtractAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D571AC  token: 0x6000333
        public static System.Void MultiplyAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D57A30  token: 0x6000334
        public static System.Void ReplaceAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D55A58  token: 0x6000335
        public static System.Void AddAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D58310  token: 0x6000336
        public static System.Void SubtractAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D570EC  token: 0x6000337
        public static System.Void MultiplyAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D57D9C  token: 0x6000338
        public static System.Void SetAttributeValueString(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor) { }
        // RVA: 0x09D56310  token: 0x6000339
        public System.Void FillAttribute(HoudiniEngineUnity.HEU_AttributeData attributeData, HoudiniEngineUnity.HEU_ToolsInfo sourceTools) { }
        // RVA: 0x09D55BD4  token: 0x600033A
        public System.Boolean AreAttributesDirty() { }
        // RVA: 0x09D56A10  token: 0x600033B
        public System.Void GetPositionAttributeValues(UnityEngine.Vector3[]& positionArray) { }
        // RVA: 0x09D56A20  token: 0x600033C
        public System.Void GetVertexIndices(System.Int32[]& indices) { }
        // RVA: 0x09D56708  token: 0x600033D
        public static HoudiniEngineUnity.HEU_AttributesStore.SetAttributeValueFunc GetAttributeSetValueFunction(HoudiniEngineUnity.HEU_AttributeData.AttributeType attrType, HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode paintMergeMode) { }
        // RVA: 0x09D55CEC  token: 0x600033E
        public System.Void CopyAttributeValuesTo(HoudiniEngineUnity.HEU_AttributesStore destAttrStore) { }
        // RVA: 0x09D570D4  token: 0x600033F
        public System.Boolean IsValidStore(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D56C7C  token: 0x6000340
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_AttributesStore other) { }
        // RVA: 0x09D58AD0  token: 0x6000341
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x40
    public class HEU_OutputAttribute
    {
        // Fields
        public System.String _name;  // 0x10
        public HoudiniEngineUnity.HAPI_AttributeOwner _class;  // 0x18
        public HoudiniEngineUnity.HAPI_StorageType _type;  // 0x1c
        public System.Int32 _count;  // 0x20
        public System.Int32 _tupleSize;  // 0x24
        public System.Int32[] _intValues;  // 0x28
        public System.Single[] _floatValues;  // 0x30
        public System.String[] _stringValues;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x6000346
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000054  // size: 0x28
    public class HEU_OutputAttributeDictionary : HoudiniEngineUnity.HEU_SerializableDictionary`2
    {
        // Methods
        // RVA: 0x09D6F830  token: 0x6000347
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000055  // size: 0x20
    public class HEU_OutputAttributesStore : UnityEngine.MonoBehaviour
    {
        // Fields
        private HoudiniEngineUnity.HEU_OutputAttributeDictionary _attributes;  // 0x18

        // Methods
        // RVA: 0x09D6F908  token: 0x6000348
        public System.Void SetAttribute(HoudiniEngineUnity.HEU_OutputAttribute attribute) { }
        // RVA: 0x09D6F8A0  token: 0x6000349
        public HoudiniEngineUnity.HEU_OutputAttribute GetAttribute(System.String name) { }
        // RVA: 0x09D6F858  token: 0x600034A
        public System.Void Clear() { }
        // RVA: 0x09D6F9D8  token: 0x600034B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056  // size: 0x10
    public static class HEU_AssetDatabase
    {
        // Methods
        // RVA: 0x09D6A53C  token: 0x600034C
        public static System.String GetAssetCachePath() { }
        // RVA: 0x09D6AD18  token: 0x600034D
        public static System.String GetUnityProjectPath() { }
        // RVA: 0x09D6A6D0  token: 0x600034E
        public static System.String GetAssetRelativePath(System.String inFullPath) { }
        // RVA: 0x09D6A9A0  token: 0x600034F
        public static System.String GetPackagesRelativePath(System.String inFullPath) { }
        // RVA: 0x09D6AD74  token: 0x6000350
        public static System.String GetValidAssetPath(System.String inPath) { }
        // RVA: 0x09D6A640  token: 0x6000351
        public static System.String GetAssetPath(UnityEngine.Object asset) { }
        // RVA: 0x09D6A608  token: 0x6000352
        public static System.String GetAssetPathWithSubAssetSupport(UnityEngine.Object asset) { }
        // RVA: 0x09D6AAC0  token: 0x6000353
        public static System.Void GetSubAssetPathFromPath(System.String fullPath, System.String& mainPath, System.String& subPath) { }
        // RVA: 0x09D6A678  token: 0x6000354
        private static System.String GetAssetRelativePathStart() { }
        // RVA: 0x09D6A948  token: 0x6000355
        private static System.String GetPackagesRelativePathStart() { }
        // RVA: 0x09D6A584  token: 0x6000356
        public static System.String GetAssetFullPath(System.String inPath) { }
        // RVA: 0x09D6B2F4  token: 0x6000357
        public static System.Boolean IsPathRelativeToAssets(System.String inPath) { }
        // RVA: 0x09D6B378  token: 0x6000358
        public static System.Boolean IsPathRelativeToPackages(System.String inPath) { }
        // RVA: 0x09D6A810  token: 0x6000359
        public static System.String GetAssetRootPath(UnityEngine.Object asset) { }
        // RVA: 0x09D6ACE0  token: 0x600035A
        public static System.String GetUniqueAssetPath(System.String path) { }
        // RVA: 0x09D6A5D0  token: 0x600035B
        public static System.String GetAssetOrScenePath(UnityEngine.Object inputObject) { }
        // RVA: 0x09D6B1E8  token: 0x600035C
        public static System.Boolean IsPathInAssetCache(System.String path) { }
        // RVA: 0x09D6B178  token: 0x600035D
        public static System.Boolean IsPathInAssetCacheBakedFolder(System.String path) { }
        // RVA: 0x09D6B1B0  token: 0x600035E
        public static System.Boolean IsPathInAssetCacheWorkingFolder(System.String path) { }
        // RVA: 0x09D6B0D0  token: 0x600035F
        public static System.Boolean IsAssetInAssetCacheBakedFolder(UnityEngine.Object asset) { }
        // RVA: 0x09D6B108  token: 0x6000360
        public static System.Boolean IsAssetInAssetCacheWorkingFolder(UnityEngine.Object asset) { }
        // RVA: 0x09D6A2F0  token: 0x6000361
        public static System.String CreateAssetCacheFolder(System.String suggestedAssetPath, System.Int32 hash) { }
        // RVA: 0x09D6A430  token: 0x6000362
        public static System.Void DeleteAssetCacheFolder(System.String assetCacheFolderPath) { }
        // RVA: 0x09D6A498  token: 0x6000363
        public static System.Void DeleteAsset(UnityEngine.Object asset) { }
        // RVA: 0x09D6A3FC  token: 0x6000364
        public static System.Void DeleteAssetAtPath(System.String path) { }
        // RVA: 0x09D6A464  token: 0x6000365
        public static System.Void DeleteAssetIfInBakedFolder(UnityEngine.Object asset) { }
        // RVA: 0x09D6A134  token: 0x6000366
        public static System.Boolean ContainsAsset(UnityEngine.Object assetObject) { }
        // RVA: 0x09D6A24C  token: 0x6000367
        public static System.Boolean CopyAsset(System.String path, System.String newPath) { }
        // RVA: 0x09D6A214  token: 0x6000368
        public static UnityEngine.Object CopyAndLoadAssetWithRelativePath(UnityEngine.Object srcAsset, System.String copyAssetFolder, System.String relativePath, System.Type type, System.Boolean bOverwriteExisting) { }
        // RVA: 0x09D6A1DC  token: 0x6000369
        public static UnityEngine.Object CopyAndLoadAssetFromAssetCachePath(UnityEngine.Object srcAsset, System.String copyPath, System.Type type, System.Boolean bOverwriteExisting) { }
        // RVA: 0x09D6A16C  token: 0x600036A
        public static UnityEngine.Object CopyAndLoadAssetAtAnyPath(UnityEngine.Object srcAsset, System.String copyPath, System.Type type, System.Boolean bOverwriteExisting) { }
        // RVA: 0x09D6A1A4  token: 0x600036B
        public static UnityEngine.Object CopyAndLoadAssetAtGivenPath(UnityEngine.Object srcAsset, System.String targetPath, System.Type type) { }
        // RVA: 0x09D6A284  token: 0x600036C
        public static UnityEngine.Object CopyUniqueAndLoadAssetAtAnyPath(UnityEngine.Object srcAsset, System.String copyPath, System.Type type) { }
        // RVA: 0x09D6A35C  token: 0x600036D
        public static System.Void CreateObjectInAssetCacheFolder(UnityEngine.Object objectToCreate, System.String assetCacheRoot, System.String relativeFolderPath, System.String assetFileName, System.Type type, System.Boolean bOverwriteExisting) { }
        // RVA: 0x09D6A328  token: 0x600036E
        public static System.Void CreateAsset(UnityEngine.Object asset, System.String path) { }
        // RVA: 0x09D6A2BC  token: 0x600036F
        public static System.Void CreateAddObjectInAssetCacheFolder(System.String assetName, System.String assetObjectFileName, UnityEngine.Object objectToAdd, System.String relativeFolderPath, System.String& exportRootPath, UnityEngine.Object& assetDBObject) { }
        // RVA: 0x09D69E3C  token: 0x6000370
        public static System.Void AddObjectToAsset(UnityEngine.Object objectToAdd, UnityEngine.Object assetObject) { }
        // RVA: 0x041E1670  token: 0x6000371
        public static System.Void SaveAndRefreshDatabase() { }
        // RVA: 0x041E1670  token: 0x6000372
        public static System.Void SaveAssetDatabase() { }
        // RVA: 0x041E1670  token: 0x6000373
        public static System.Void RefreshAssetDatabase() { }
        // RVA: 0x09D6B46C  token: 0x6000374
        public static UnityEngine.Object LoadAssetAtPath(System.String assetPath, System.Type type) { }
        // RVA: 0x09D6B4A4  token: 0x6000375
        public static UnityEngine.Object LoadSubAssetAtPath(System.String mainPath, System.String subAssetPath) { }
        // RVA: 0x09D6B434  token: 0x6000376
        public static UnityEngine.Object[] LoadAllAssetsAtPath(System.String assetPath) { }
        // RVA: 0x09D6B3FC  token: 0x6000377
        public static UnityEngine.Object[] LoadAllAssetRepresentationsAtPath(System.String assetPath) { }
        // RVA: 0x09D6B09C  token: 0x6000378
        public static System.Void ImportAsset(System.String assetPath, HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions heuOptions) { }
        // RVA: 0x09D6A910  token: 0x6000379
        public static System.String GetAssetWorkingPath() { }
        // RVA: 0x09D6A504  token: 0x600037A
        public static System.String GetAssetBakedPath() { }
        // RVA: 0x09D6A4CC  token: 0x600037B
        public static System.String GetAssetBakedPathWithAssetName(System.String assetName) { }
        // RVA: 0x09D6A3C4  token: 0x600037C
        public static System.String CreateUniqueBakePath(System.String assetName) { }
        // RVA: 0x09D6A390  token: 0x600037D
        public static System.Void CreatePathWithFolders(System.String inPath) { }
        // RVA: 0x09D69F28  token: 0x600037E
        public static System.String AppendMeshesPathToAssetFolder(System.String inAssetCacheFolder) { }
        // RVA: 0x09D6A0B8  token: 0x600037F
        public static System.String AppendTexturesPathToAssetFolder(System.String inAssetCacheFolder) { }
        // RVA: 0x09D69E70  token: 0x6000380
        public static System.String AppendMaterialsPathToAssetFolder(System.String inAssetCacheFolder) { }
        // RVA: 0x09D6A03C  token: 0x6000381
        public static System.String AppendTerrainPathToAssetFolder(System.String inAssetCacheFolder) { }
        // RVA: 0x09D6A848  token: 0x6000382
        public static System.String[] GetAssetSubFolders() { }
        // RVA: 0x09D69FA4  token: 0x6000383
        public static System.String AppendPrefabPath(System.String inAssetCacheFolder, System.String assetName) { }
        // RVA: 0x09D69EEC  token: 0x6000384
        public static System.String AppendMeshesAssetFileName(System.String assetName) { }
        // RVA: 0x012081B0  token: 0x6000385
        public static System.Boolean IsSubAsset(UnityEngine.Object obj) { }
        // RVA: 0x011EC580  token: 0x6000386
        public static System.String[] GetAssetPathsFromAssetBundle(System.String assetBundleFileName) { }
        // RVA: 0x09D6B140  token: 0x6000387
        public static System.Boolean IsAssetSavedInScene(UnityEngine.GameObject go) { }
        // RVA: 0x041E1670  token: 0x6000388
        public static System.Void SelectAssetAtPath(System.String path) { }
        // RVA: 0x041E1670  token: 0x6000389
        public static System.Void PrintDependencies(UnityEngine.GameObject targetGO) { }
        // RVA: 0x09D6AC30  token: 0x600038A
        public static System.String GetUniqueAssetPathForUnityAsset(UnityEngine.Object obj) { }
        // RVA: 0x02FFF600  token: 0x600038B
        public static System.Boolean IsValidFolderName(System.String name) { }
        // RVA: -1  // generic def  token: 0x600038C
        public static T LoadUnityAssetFromUniqueAssetPath(System.String assetPath) { }
        // RVA: -1  // generic def  token: 0x600038D
        public static T GetBuiltinExtraResource(System.String resourceName) { }

    }

    // TypeToken: 0x2000058  // size: 0x10
    public class HEU_Defines
    {
        // Fields
        public static System.String HEU_PRODUCT_NAME;  // const
        public static System.String HEU_NAME;  // const
        public static System.String HEU_PLUGIN_PATH;  // static @ 0x0
        public static System.String HEU_TEXTURES_PATH;  // static @ 0x8
        public static System.String HEU_BAKED_ASSETS_PATH;  // static @ 0x10
        public static System.String HEU_ENGINE_ASSETS;  // static @ 0x18
        public static System.String HAPI_PATH;  // const
        public static System.String HEU_ENVPATH_PREFIX;  // const
        public static System.String HEU_ENVPATH_KEY;  // const
        public static System.Int32 HEU_INVALID_NODE_ID;  // const
        public static System.String HEU_DEFAULT_ASSET_NAME;  // const
        public static System.String HEU_SESSION_PIPENAME;  // const
        public static System.String HEU_SESSION_LOCALHOST;  // const
        public static System.Int32 HEU_SESSION_PORT;  // const
        public static System.String HEU_SESSION_SHARED_MEMORY_NAME;  // const
        public static System.Int32 HEU_SESSION_SHARED_MEMORY_BUFFER_SIZE;  // const
        public static HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType HEU_SESSION_SHARED_MEMORY_BUFFER_TYPE;  // const
        public static System.Single HEU_SESSION_TIMEOUT;  // const
        public static System.Boolean HEU_SESSION_AUTOCLOSE;  // const
        public static System.Int32 HAPI_MAX_PAGE_SIZE;  // const
        public static System.Int32 HAPI_SEC_BEFORE_PROGRESS_BAR_SHOW;  // const
        public static System.Int32 HAPI_MAX_VERTICES_PER_FACE;  // const
        public static System.Boolean HAPI_CURVE_REFINE_TO_LINEAR;  // const
        public static System.Single HAPI_CURVE_LOD;  // const
        public static System.Single HAPI_VOLUME_POSITION_MULT;  // const
        public static System.Single HAPI_VOLUME_SURFACE_MAX_PT_PER_C;  // const
        public static System.Single HAPI_VOLUME_SURFACE_DELTA_MULT;  // const
        public static System.Single HAPI_VOLUME_SURFACE_PT_SIZE_MULT;  // const
        public static System.String DEFAULT_TOP_NODE_FILTER;  // const
        public static System.String DEFAULT_TOP_OUTPUT_FILTER;  // const
        public static System.String HAPI_ATTRIB_ORIENT;  // const
        public static System.String HAPI_ATTRIB_ROTATION;  // const
        public static System.String HAPI_ATTRIB_SCALE;  // const
        public static System.String HAPI_ATTRIB_ALPHA;  // const
        public static System.String HAPI_HANDLE_TRANSFORM;  // const
        public static System.Int32 HAPI_MAX_UVS;  // const
        public static System.String HAPI_OBJMERGE_TRANSFORM_PARAM;  // const
        public static System.String HAPI_OBJMERGE_PACK_GEOMETRY;  // const
        public static System.String HAPI_OBJPATH_1_PARAM;  // const
        public static System.String NO_EXISTING_SESSION;  // const
        public static System.String HEU_ERROR_TITLE;  // const
        public static System.String HEU_INSTALL_INFO;  // const
        public static System.String PLUGIN_STORE_KEYS;  // const
        public static System.String PLUGIN_STORE_DATA;  // const
        public static System.String PLUGIN_SESSION_DATA;  // const
        public static System.String PLUGIN_SETTINGS_FILE;  // const
        public static System.String PLUGIN_SESSION_FILE;  // const
        public static System.String COOK_LOGS_FILE;  // const
        public static System.String DEFAULT_COLLISION_GEO;  // const
        public static System.String DEFAULT_RENDERED_COLLISION_GEO;  // const
        public static System.String DEFAULT_RENDERED_CONVEX_COLLISION_GEO;  // const
        public static System.String DEFAULT_CONVEX_COLLISION_GEO;  // const
        public static System.String DEFAULT_SIMPLE_COLLISION_GEO;  // const
        public static System.String DEFAULT_SIMPLE_RENDERED_COLLISION_GEO;  // const
        public static System.String DEFAULT_SIMPLE_RENDERED_CONVEX_COLLISION_GEO;  // const
        public static System.String DEFAULT_COLLISION_TRIGGER;  // const
        public static System.String DEFAULT_UNITY_MATERIAL_ATTR;  // const
        public static System.String DEFAULT_UNITY_SUBMATERIAL_NAME_ATTR;  // const
        public static System.String DEFAULT_UNITY_SUBMATERIAL_INDEX_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TEXTURE_DIFFUSE_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TEXTURE_MASK_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TEXTURE_NORMAL_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_NORMAL_SCALE_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_METALLIC_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_SMOOTHNESS_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_SPECULAR_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TILE_OFFSET_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TILE_SIZE_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TERRAINDATA_FILE_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TERRAINDATA_EXPORT_FILE_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TERRAINLAYER_FILE_ATTR;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_TERRAINDATA_EXPORT_PATH;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_HEIGHT_RANGE;  // const
        public static System.String DEFAULT_UNITY_HEIGHTFIELD_YPOS;  // const
        public static System.String HEIGHTFIELD_TREEPROTOTYPE;  // const
        public static System.String HEIGHTFIELD_TREEINSTANCE_PROTOTYPEINDEX;  // const
        public static System.String HEIGHTFIELD_TREEINSTANCE_HEIGHTSCALE;  // const
        public static System.String HEIGHTFIELD_TREEINSTANCE_WIDTHSCALE;  // const
        public static System.String HEIGHTFIELD_TREEINSTANCE_LIGHTMAPCOLOR;  // const
        public static System.String HEIGHTFIELD_DETAIL_RESOLUTION_PER_PATCH;  // const
        public static System.String HEIGHTFIELD_UNITY_TILE;  // const
        public static System.String HEIGHTFIELD_DETAIL_DISTANCE;  // const
        public static System.String HEIGHTFIELD_DETAIL_DENSITY;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_PREFAB;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_TEXTURE;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_BENDFACTOR;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_DRYCOLOR;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_HEALTHYCOLOR;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_MAXHEIGHT;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_MAXWIDTH;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_MINHEIGHT;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_MINWIDTH;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_NOISESPREAD;  // const
        public static System.String HEIGHTFIELD_DETAIL_PROTOTYPE_RENDERMODE;  // const
        public static System.String HEIGHTFIELD_LAYER_ATTR_TYPE;  // const
        public static System.String HEIGHTFIELD_LAYER_TYPE_DETAIL;  // const
        public static System.String HAPI_HEIGHTFIELD_TILE_ATTR;  // const
        public static System.String HAPI_HEIGHTFIELD_LAYERNAME_HEIGHT;  // const
        public static System.String HAPI_HEIGHTFIELD_LAYERNAME_MASK;  // const
        public static System.String MAT_OGL_DIFF_ATTR;  // const
        public static System.String MAT_DIFF_ATTR;  // const
        public static System.String MAT_OGL_TEX1_ATTR;  // const
        public static System.String MAT_OGL_TEX1_ATTR_ENABLED;  // const
        public static System.String MAT_BASECOLOR_ATTR;  // const
        public static System.String MAT_BASECOLOR_ATTR_ENABLED;  // const
        public static System.String MAT_MAP_ATTR;  // const
        public static System.String MAT_OGL_NORMAL_ATTR;  // const
        public static System.String MAT_NORMAL_ATTR;  // const
        public static System.String MAT_NORMAL_ATTR_ENABLED;  // const
        public static System.String MAT_OGL_SPEC_ATTR;  // const
        public static System.String MAT_SPEC_ATTR;  // const
        public static System.String MAT_OGL_SPEC_MAP_ATTR;  // const
        public static System.String MAT_OGL_SPEC_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_SPEC_MAP_ATTR;  // const
        public static System.String MAT_SPEC_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_OGL_ROUGH_ATTR;  // const
        public static System.String MAT_ROUGH_ATTR;  // const
        public static System.String MAT_OGL_ROUGH_MAP_ATTR;  // const
        public static System.String MAT_OGL_ROUGH_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_ROUGH_MAP_ATTR;  // const
        public static System.String MAT_ROUGH_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_OGL_METALLIC_ATTR;  // const
        public static System.String MAT_METALLIC_ATTR;  // const
        public static System.String MAT_OGL_METALLIC_MAP_ATTR;  // const
        public static System.String MAT_OGL_METALLIC_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_METALLIC_MAP_ATTR;  // const
        public static System.String MAT_METALLIC_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_OGL_EMISSIVE_ATTR;  // const
        public static System.String MAT_EMISSIVE_ATTR;  // const
        public static System.String MAT_OGL_EMISSIVE_MAP_ATTR;  // const
        public static System.String MAT_OGL_EMISSIVE_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_EMISSIVE_MAP_ATTR;  // const
        public static System.String MAT_EMISSIVE_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_OGL_ALPHA_ATTR;  // const
        public static System.String MAT_ALPHA_ATTR;  // const
        public static System.String MAT_OGL_OPACITY_MAP_ATTR;  // const
        public static System.String MAT_OGL_OPACITY_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_OPACITY_MAP_ATTR;  // const
        public static System.String MAT_OPACITY_MAP_ATTR_ENABLED;  // const
        public static System.String MAT_OGL_TRANSPARENCY_ATTR;  // const
        public static System.String MAT_OGL_TRANSPARENCY_ATTR_ENABLED;  // const
        public static System.String MAT_OGL_OCCLUSION_MAP_ATTR;  // const
        public static System.String MAT_OGL_OCCLUSION_MAP_ATTR_ENABLED;  // const
        public static System.String CURVE_COORDS_PARAM;  // const
        public static System.String CURVE_TYPE_PARAM;  // const
        public static System.String CURVE_METHOD_PARAM;  // const
        public static System.String CURVE_CLOSE_PARAM;  // const
        public static System.String CURVE_REVERSE_PARAM;  // const
        public static System.String HENGINE_STORE_ATTR;  // const
        public static System.String DEFAULT_UNITY_TAG_ATTR;  // const
        public static System.String DEFAULT_UNITY_SCRIPT_ATTR;  // const
        public static System.String DEFAULT_UNITY_INSTANCE_ATTR;  // const
        public static System.String UNITY_USE_INSTANCE_FLAGS_ATTR;  // const
        public static System.String DEFAULT_UNITY_INPUT_MESH_ATTR;  // const
        public static System.String DEFAULT_UNITY_STATIC_ATTR;  // const
        public static System.String DEFAULT_UNITY_LAYER_ATTR;  // const
        public static System.String DEFAULT_UNITY_MESH_READABLE;  // const
        public static System.String DEFAULT_INSTANCE_PREFIX_ATTR;  // const
        public static System.String UNITY_SHADER_COLOR;  // const
        public static System.String UNITY_SHADER_SPEC_COLOR;  // const
        public static System.String UNITY_SHADER_SPEC_MAP;  // const
        public static System.String UNITY_SHADER_METALLIC;  // const
        public static System.String UNITY_SHADER_METALLIC_MAP;  // const
        public static System.String UNITY_SHADER_BUMP_MAP;  // const
        public static System.String UNITY_SHADER_EMISSION_COLOR;  // const
        public static System.String UNITY_SHADER_EMISSION_MAP;  // const
        public static System.String UNITY_SHADER_SMOOTHNESS;  // const
        public static System.String UNITY_SHADER_SMOOTHNESS_MAP;  // const
        public static System.String UNITY_SHADER_SHININESS;  // const
        public static System.String UNITY_SHADER_OCCLUSION;  // const
        public static System.String UNITY_SHADER_OCCLUSION_MAP;  // const
        public static System.String UNITY_SHADER_OPACITY;  // const
        public static System.String UNITY_SHADER_OPACITY_MAP;  // const
        public static System.String UNITY_EDITORONLY_TAG;  // const
        public static System.String UNITY_HDADATA_NAME;  // const
        public static System.String HOUDINI_SHADER_PREFIX;  // const
        public static System.String DEFAULT_STANDARD_SHADER;  // const
        public static System.String DEFAULT_VERTEXCOLOR_SHADER;  // const
        public static System.String DEFAULT_TRANSPARENT_SHADER;  // const
        public static System.String DEFAULT_STANDARD_SHADER_SPECULAR;  // const
        public static System.String DEFAULT_STANDARD_SHADER_SPECULAR_LEGACY;  // const
        public static System.String DEFAULT_VERTEXCOLOR_SHADER_SPECULAR;  // const
        public static System.String DEFAULT_VERTEXCOLOR_SHADER_SPECULAR_LEGACY;  // const
        public static System.String DEFAULT_TRANSPARENT_SHADER_SPECULAR;  // const
        public static System.String DEFAULT_TRANSPARENT_SHADER_SPECULAR_LEGACY;  // const
        public static System.String DEFAULT_CURVE_SHADER;  // const
        public static System.String DEFAULT_TERRAIN_SHADER;  // const
        public static System.String DEFAULT_STANDARD_SHADER_HDRP;  // const
        public static System.String DEFAULT_VERTEXCOLOR_SHADER_HDRP;  // const
        public static System.String DEFAULT_TRANSPARENT_SHADER_HDRP;  // const
        public static System.String DEFAULT_STANDARD_SHADER_HDRP_SPECULAR;  // const
        public static System.String DEFAULT_VERTEXCOLOR_SHADER_HDRP_SPECULAR;  // const
        public static System.String DEFAULT_TRANSPARENT_SHADER_HDRP_SPECULAR;  // const
        public static System.String DEFAULT_CURVE_SHADER_HDRP;  // const
        public static System.String DEFAULT_TERRAIN_SHADER_HDRP;  // const
        public static System.String DEFAULT_STANDARD_SHADER_URP;  // const
        public static System.String DEFAULT_VERTEXCOLOR_SHADER_URP;  // const
        public static System.String DEFAULT_TRANSPARENT_SHADER_URP;  // const
        public static System.String DEFAULT_STANDARD_SHADER_URP_SPECULAR;  // const
        public static System.String DEFAULT_VERTEXCOLOR_SHADER_URP_SPECULAR;  // const
        public static System.String DEFAULT_TRANSPARENT_SHADER_URP_SPECULAR;  // const
        public static System.String DEFAULT_CURVE_SHADER_URP;  // const
        public static System.String DEFAULT_TERRAIN_SHADER_URP;  // const
        public static System.String DEFAULT_UNITY_BUILTIN_RESOURCES;  // const
        public static System.String DEFAULT_TERRAIN_MATERIAL_PATH;  // const
        public static System.String DEFAULT_TERRAIN_MATERIAL_PATH_HDRP;  // const
        public static System.String DEFAULT_TERRAIN_MATERIAL_PATH_URP;  // const
        public static System.String DEFAULT_MATERIAL;  // const
        public static System.Int32 DEFAULT_MATERIAL_KEY;  // static @ 0x20
        public static System.String EDITABLE_MATERIAL;  // const
        public static System.Int32 EDITABLE_MATERIAL_KEY;  // static @ 0x24
        public static System.Int32 HEU_INVALID_MATERIAL;  // const
        public static System.String HEU_ASSET_CACHE_PATH;  // const
        public static System.String HEU_WORKING_PATH;  // const
        public static System.String HEU_BAKED_PATH;  // const
        public static System.String HEU_BAKED_HDA;  // const
        public static System.String HEU_BAKED_CLONE;  // const
        public static System.String HEU_INSTANCE;  // const
        public static System.String HEU_INSTANCE_PATTERN;  // const
        public static System.String HEU_INSTANCE_SPLIT_ATTR;  // const
        public static System.String HEU_DEFAULT_GEO_GROUP_NAME;  // const
        public static System.String HEU_DEFAULT_LOD_NAME;  // const
        public static System.String HEU_UNITY_LOD_TRANSITION_ATTR;  // const
        public static System.String HEU_SUBASSET;  // const
        public static System.String HEU_HENGINE_TOOLS_SHIPPED_FOLDER;  // const
        public static System.String HEU_HENGINE_SHIPPED_SHELF;  // const
        public static System.String HEU_PATH_KEY_PROJECT;  // const
        public static System.String HEU_PATH_KEY_PLUGIN;  // const
        public static System.String HEU_PATH_KEY_HFS;  // const
        public static System.String HEU_PATH_KEY_TOOL;  // const
        public static System.String HEU_USERMSG_NONEDITOR_NOT_SUPPORTED;  // const
        public static System.String HEU_TERRAIN_SPLAT_DEFAULT;  // const
        public static System.String HEU_FOLDER_MESHES;  // const
        public static System.String HEU_FOLDER_MATERIALS;  // const
        public static System.String HEU_FOLDER_TERRAIN;  // const
        public static System.String HEU_FOLDER_TILE;  // const
        public static System.String HEU_FOLDER_TEXTURES;  // const
        public static System.String HEU_EXT_ASSET;  // const
        public static System.String HEU_EXT_MAT;  // const
        public static System.String HEU_EXT_TERRAINDATA;  // const
        public static System.String HEU_EXT_TERRAINLAYER;  // const
        public static System.String HEU_KEY_CTRL;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x600038E
        public System.Void .ctor() { }
        // RVA: 0x09D6B53C  token: 0x600038F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x10
    public static class HEU_HAPIUtility
    {
        // Methods
        // RVA: 0x09D6D4F8  token: 0x6000390
        public static System.String GetHoudiniEngineInstallationInfo() { }
        // RVA: 0x09D6CEAC  token: 0x6000391
        public static System.String GetEnvironmentPath() { }
        // RVA: 0x09D6DDC4  token: 0x6000392
        public static System.String GetRealPathFromHFSPath(System.String inPath) { }
        // RVA: 0x09D6C5F0  token: 0x6000393
        public static System.Boolean DoesMappedPathExist(System.String inPath) { }
        // RVA: 0x09D6E100  token: 0x6000394
        public static System.Boolean IsHoudiniAssetFile(System.String filePath) { }
        // RVA: 0x09D6C660  token: 0x6000395
        public static System.String FindHoudiniAssetFileInPathWithExt(System.String filePath) { }
        // RVA: 0x09D6E738  token: 0x6000396
        public static System.Void Log(System.String message) { }
        // RVA: 0x09D6E730  token: 0x6000397
        public static System.Void LogWarning(System.String message) { }
        // RVA: 0x09D6E728  token: 0x6000398
        public static System.Void LogError(System.String message) { }
        // RVA: 0x09D6E6CC  token: 0x6000399
        public static System.String LocateValidFilePath(UnityEngine.Object inObject) { }
        // RVA: 0x04D88C30  token: 0x600039A
        public static System.String LocateValidFilePath(System.String assetName, System.String inFilePath) { }
        // RVA: 0x09D6DEE8  token: 0x600039B
        public static UnityEngine.GameObject InstantiateHDA(System.String filePath, UnityEngine.Vector3 initialPosition, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, System.Boolean bLoadFromMemory, System.Boolean bAlwaysOverwriteOnLoad, UnityEngine.GameObject rootGO) { }
        // RVA: 0x09D6E618  token: 0x600039C
        public static System.Boolean LoadHDAFile(HoudiniEngineUnity.HEU_SessionBase session, System.String assetPath, System.Int32& assetLibraryID, System.String[]& assetNames) { }
        // RVA: 0x09D6C04C  token: 0x600039D
        public static System.Boolean CreateAndCookAssetNode(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName, System.Boolean bCookTemplatedGeos, System.Int32& newAssetID) { }
        // RVA: 0x09D6C0B8  token: 0x600039E
        public static System.Boolean CreateAndCookCurveAsset(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName, System.Boolean bCookTemplatedGeos, System.Int32& newAssetID) { }
        // RVA: 0x09D6C10C  token: 0x600039F
        public static System.Boolean CreateAndCookInputAsset(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName, System.Boolean bCookTemplatedGeos, System.Int32& newAssetID) { }
        // RVA: 0x09D6BE10  token: 0x60003A0
        public static System.Boolean CookNodeInHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Boolean bCookTemplatedGeos, System.String assetName) { }
        // RVA: 0x09D6BE10  token: 0x60003A1
        public static System.Boolean CookNodeInHoudiniWithOptions(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_CookOptions options, System.String assetName) { }
        // RVA: 0x09D6CE70  token: 0x60003A2
        public static HoudiniEngineUnity.HAPI_CookOptions GetDefaultCookOptions(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D6E740  token: 0x60003A3
        public static System.Boolean ProcessHoudiniCookStatus(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName) { }
        // RVA: 0x09D6C174  token: 0x60003A4
        public static UnityEngine.GameObject CreateNewAsset(HoudiniEngineUnity.HEU_AssetTypeWrapper assetType, System.String rootName, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO) { }
        // RVA: 0x09D6C1C8  token: 0x60003A5
        private static UnityEngine.GameObject CreateNewAsset(HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType assetType, System.String rootName, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO) { }
        // RVA: 0x09D6C27C  token: 0x60003A6
        public static UnityEngine.GameObject CreateNewCurveAsset(System.String name, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO) { }
        // RVA: 0x09D6C2B0  token: 0x60003A7
        public static UnityEngine.GameObject CreateNewInputAsset(System.String name, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO) { }
        // RVA: 0x011EC580  token: 0x60003A8
        public static UnityEngine.GameObject LoadGeoWithNewGeoSync(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D6C2E4  token: 0x60003A9
        public static System.Void DestroyChildren(UnityEngine.Transform inTransform) { }
        // RVA: 0x09D6C5E0  token: 0x60003AA
        public static System.Void DestroyGameObject(UnityEngine.GameObject gameObect, System.Boolean bRegisterUndo) { }
        // RVA: -1  // generic def  token: 0x60003AB
        public static System.Void DestroyChildrenWithComponent(UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D6E218  token: 0x60003AC
        public static System.Boolean IsNodeValidInHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID) { }
        // RVA: 0x09D6CD90  token: 0x60003AD
        public static HoudiniEngineUnity.HEU_HoudiniAssetRoot GetAssetInScene(System.Int32 assetID) { }
        // RVA: 0x09D6BB9C  token: 0x60003AE
        public static System.Void ApplyWorldTransfromFromHoudiniToUnity(HoudiniEngineUnity.HAPI_Transform& hapiTransform, UnityEngine.Transform unityTransform) { }
        // RVA: 0x09D6B938  token: 0x60003AF
        public static System.Void ApplyLocalTransfromFromHoudiniToUnity(HoudiniEngineUnity.HAPI_Transform& hapiTransform, UnityEngine.Transform unityTransform) { }
        // RVA: 0x09D6B708  token: 0x60003B0
        public static System.Void ApplyLocalTransfromFromHoudiniToUnityForInstance(HoudiniEngineUnity.HAPI_Transform& hapiTransform, UnityEngine.Transform unityTransform) { }
        // RVA: 0x09D6BAE8  token: 0x60003B1
        public static System.Void ApplyMatrixToLocalTransform(UnityEngine.Matrix4x4& matrix, UnityEngine.Transform transform) { }
        // RVA: 0x09D6D5D4  token: 0x60003B2
        public static UnityEngine.Matrix4x4 GetMatrixFromHAPITransform(HoudiniEngineUnity.HAPI_Transform& hapiTransform, System.Boolean bConvertToUnity) { }
        // RVA: 0x09D6D9FC  token: 0x60003B3
        public static UnityEngine.Quaternion GetQuaternion(UnityEngine.Matrix4x4& m) { }
        // RVA: 0x09D6D9B0  token: 0x60003B4
        public static UnityEngine.Vector3 GetPosition(UnityEngine.Matrix4x4& m) { }
        // RVA: 0x09D6E7C0  token: 0x60003B5
        public static System.Void SetMatrixPosition(UnityEngine.Matrix4x4& m, UnityEngine.Vector3& position) { }
        // RVA: 0x04DA2840  token: 0x60003B6
        public static UnityEngine.Vector3 GetScale(UnityEngine.Matrix4x4& m) { }
        // RVA: 0x09D6CF1C  token: 0x60003B7
        public static HoudiniEngineUnity.HAPI_TransformEuler GetHAPITransformFromMatrix(UnityEngine.Matrix4x4& mat) { }
        // RVA: 0x09D6D364  token: 0x60003B8
        public static HoudiniEngineUnity.HAPI_TransformEuler GetHAPITransform(UnityEngine.Vector3& p, UnityEngine.Vector3& r, UnityEngine.Vector3& s) { }
        // RVA: 0x09D6D108  token: 0x60003B9
        public static HoudiniEngineUnity.HAPI_Transform GetHAPITransformQuatFromMatrix(UnityEngine.Matrix4x4& mat) { }
        // RVA: 0x09D6D524  token: 0x60003BA
        public static UnityEngine.Matrix4x4 GetMatrix4x4(UnityEngine.Vector3& p, UnityEngine.Vector3& r, UnityEngine.Vector3& s) { }
        // RVA: 0x09D6E290  token: 0x60003BB
        public static System.Boolean IsSameTransform(UnityEngine.Matrix4x4& transformMatrix, UnityEngine.Vector3& p, UnityEngine.Vector3& r, UnityEngine.Vector3& s) { }
        // RVA: 0x03418B30  token: 0x60003BC
        public static System.Boolean IsEqualTol(System.Single a, System.Single b, System.Single t) { }
        // RVA: 0x09D6E374  token: 0x60003BD
        public static System.Boolean IsTransformEqual(HoudiniEngineUnity.HAPI_Transform& transA, HoudiniEngineUnity.HAPI_Transform& transB) { }
        // RVA: 0x09D6E51C  token: 0x60003BE
        public static System.Boolean IsViewportEqual(HoudiniEngineUnity.HAPI_Viewport& viewA, HoudiniEngineUnity.HAPI_Viewport& viewB) { }
        // RVA: 0x09D6E348  token: 0x60003BF
        public static System.Boolean IsSessionSyncEqual(HoudiniEngineUnity.HAPI_SessionSyncInfo& syncA, HoudiniEngineUnity.HAPI_SessionSyncInfo& syncB) { }
        // RVA: 0x09D6BE10  token: 0x60003C0
        public static System.Boolean DoesGeoPartHaveAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner owner, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo) { }
        // RVA: 0x041E1670  token: 0x60003C1
        public static System.Void SetAnimationCurveTangentModes(UnityEngine.AnimationCurve animCurve, System.Collections.Generic.List<System.Int32> tangentValues) { }
        // RVA: 0x09D6E360  token: 0x60003C2
        public static System.Boolean IsSupportedPolygonType(HoudiniEngineUnity.HAPI_PartType partType) { }
        // RVA: 0x09D6D990  token: 0x60003C3
        public static System.Int32 GetParentNodeID(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID) { }
        // RVA: 0x09D6D7F8  token: 0x60003C4
        public static System.Boolean GetObjectInfos(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, HoudiniEngineUnity.HAPI_ObjectInfo[]& objectInfos, HoudiniEngineUnity.HAPI_Transform[]& objectTransforms) { }
        // RVA: 0x09D6BE10  token: 0x60003C5
        public static System.Boolean ContainsSopNodes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeId) { }
        // RVA: 0x09D6E234  token: 0x60003C6
        public static System.Boolean IsObjNodeFullyVisible(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.HashSet<System.Int32> allObjectIds, System.Int32 inRootNodeId, System.Int32 inChildNodeId) { }
        // RVA: 0x09D6D958  token: 0x60003C7
        public static System.Boolean GetOutputIndex(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeId, System.Int32& outputIndex) { }
        // RVA: 0x09D6C7DC  token: 0x60003C8
        private static System.Void GatherAllAssetGeoInfos(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_AssetInfo assetInfo, HoudiniEngineUnity.HAPI_ObjectInfo objectInfo, System.Boolean bUseOutputNodes, System.Boolean bGetEditableNodes, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_GeoInfo>& outGeoInfos) { }
        // RVA: 0x09D6CCF4  token: 0x60003C9
        private static System.Void GatherAllObjectGeoInfos(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetId, System.Boolean bUseOutputNodes, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_GeoInfo>& outGeoInfos) { }
        // RVA: 0x09D6C904  token: 0x60003CA
        private static System.Void GatherAllAssetOutputs(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeId, System.Boolean bUseOutputNodes, System.Boolean bOutputTemplatedGeos, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_GeoInfo>& outGeoInfos) { }
        // RVA: 0x09D6E80C  token: 0x60003CB
        public static System.String ToHapiVariableName(System.String name) { }
        // RVA: 0x04DA27D0  token: 0x60003CC
        public static System.Void ConvertPositionUnityToHoudini(UnityEngine.Vector3& position) { }
        // RVA: 0x04DA27B0  token: 0x60003CD
        public static System.Void ConvertPositionUnityToHoudini(UnityEngine.Vector3 position, System.Single& outputX, System.Single& outputY, System.Single& outputZ) { }
        // RVA: 0x04DA2790  token: 0x60003CE
        public static UnityEngine.Vector3 ConvertPositionUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ) { }
        // RVA: 0x04DA2760  token: 0x60003CF
        public static UnityEngine.Vector3 ConvertPositionUnityToHoudini(UnityEngine.Vector3 inputVec) { }
        // RVA: 0x04DA27E0  token: 0x60003D0
        public static System.Void ConvertPositionUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ, UnityEngine.Vector3& outputVec) { }
        // RVA: 0x09D6BF6C  token: 0x60003D1
        public static System.Void ConvertRotationUnityToHoudini(UnityEngine.Quaternion& rotation) { }
        // RVA: 0x09D6BE28  token: 0x60003D2
        public static System.Void ConvertRotationUnityToHoudini(UnityEngine.Quaternion rotation, System.Single& outputX, System.Single& outputY, System.Single& outputZ, System.Single& outputW) { }
        // RVA: 0x09D6BEDC  token: 0x60003D3
        public static UnityEngine.Quaternion ConvertRotationUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ, System.Single inputW) { }
        // RVA: 0x09D6BFDC  token: 0x60003D4
        public static UnityEngine.Quaternion ConvertRotationUnityToHoudini(UnityEngine.Quaternion inputQuat) { }
        // RVA: 0x041E1670  token: 0x60003D5
        public static System.Void ConvertScaleUnityToHoudini(UnityEngine.Vector3& position) { }
        // RVA: 0x04DA2800  token: 0x60003D6
        public static System.Void ConvertScaleUnityToHoudini(UnityEngine.Vector3 position, System.Single& outputX, System.Single& outputY, System.Single& outputZ) { }
        // RVA: 0x04DA2820  token: 0x60003D7
        public static UnityEngine.Vector3 ConvertScaleUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ) { }
        // RVA: 0x04D93250  token: 0x60003D8
        public static UnityEngine.Vector3 ConvertScaleUnityToHoudini(UnityEngine.Vector3 inputVec) { }

    }

    // TypeToken: 0x200005A  // size: 0x98
    public class HEU_HoudiniEngineError : System.Exception
    {
        // Fields
        protected System.String _errorMsg;  // 0x90

        // Methods
        // RVA: 0x09D6F7C8  token: 0x60003D9
        public System.Void .ctor() { }
        // RVA: 0x09D6F740  token: 0x60003DA
        public System.Void .ctor(System.String errorMsg) { }
        // RVA: 0x04D86220  token: 0x60003DB
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200005B  // size: 0x10
    public class HEU_Platform
    {
        // Fields
        private static System.String _lastErrorMsg;  // static @ 0x0
        private static System.String _libPath;  // static @ 0x8
        private static System.Boolean _pathSet;  // static @ 0x10

        // Properties
        System.String LibPath { get; /* RVA: 0x09D7D048 */ }
        System.Boolean IsPathSet { get; /* RVA: 0x09D7CFF8 */ }
        System.Char DirectorySeparator { get; /* RVA: 0x04DA29E0 */ }
        System.String DirectorySeparatorStr { get; /* RVA: 0x09D7CFCC */ }

        // Methods
        // RVA: 0x09D7CFC4  token: 0x60003DE
        private static System.Void .cctor() { }
        // RVA: 0x09D7C8C4  token: 0x60003DF
        public static System.String GetHoudiniEnginePath() { }
        // RVA: 0x09D7C63C  token: 0x60003E0
        public static System.String GetHoudiniEngineDefaultPath() { }
        // RVA: 0x09D7C93C  token: 0x60003E1
        public static System.String GetSavedHoudiniPath() { }
        // RVA: 0x09D7CDC4  token: 0x60003E2
        public static System.Void SetHapiClientName() { }
        // RVA: 0x041E1670  token: 0x60003E3
        public static System.Void SetHoudiniEnginePath() { }
        // RVA: 0x09D7C298  token: 0x60003E4
        public static System.String GetAllFoldersInPath(System.String path) { }
        // RVA: 0x09D7C178  token: 0x60003E5
        private static System.Void GetAllFoldersInPathHelper(System.String inPath, System.Text.StringBuilder pathBuilder) { }
        // RVA: 0x09D7C3F0  token: 0x60003E6
        public static System.String[] GetFilesInFolder(System.String folderPath, System.String searchPattern, System.Boolean bRecursive) { }
        // RVA: 0x09D7C3A4  token: 0x60003E7
        public static System.String GetFileName(System.String path) { }
        // RVA: 0x09D7C358  token: 0x60003E8
        public static System.String GetFileNameWithoutExtension(System.String path) { }
        // RVA: 0x09D7C508  token: 0x60003E9
        public static System.String GetFolderPath(System.String path, System.Boolean bRemoveDirectorySeparatorAtEnd) { }
        // RVA: 0x09D7C000  token: 0x60003EC
        public static System.String BuildPath(System.String folder1, System.String folder2, System.Object[] args) { }
        // RVA: 0x09D7CE0C  token: 0x60003ED
        public static System.String TrimLastDirectorySeparator(System.String inPath) { }
        // RVA: 0x09D7C148  token: 0x60003EE
        public static System.Boolean DoesPathExist(System.String inPath) { }
        // RVA: 0x09D7C140  token: 0x60003EF
        public static System.Boolean DoesFileExist(System.String inPath) { }
        // RVA: 0x09D7C138  token: 0x60003F0
        public static System.Boolean DoesDirectoryExist(System.String inPath) { }
        // RVA: 0x09D7C110  token: 0x60003F1
        public static System.Boolean CreateDirectory(System.String inPath) { }
        // RVA: 0x09D7C91C  token: 0x60003F2
        public static System.String GetParentDirectory(System.String inPath) { }
        // RVA: 0x09D7C5F0  token: 0x60003F3
        public static System.String GetFullPath(System.String inPath) { }
        // RVA: 0x09D7CA58  token: 0x60003F4
        public static System.Boolean IsPathRooted(System.String inPath) { }
        // RVA: 0x09D7CFBC  token: 0x60003F5
        public static System.Void WriteBytes(System.String path, System.Byte[] bytes) { }
        // RVA: 0x09D7CE9C  token: 0x60003F6
        public static System.Boolean WriteAllText(System.String path, System.String text) { }
        // RVA: 0x09D7CC6C  token: 0x60003F7
        public static System.String ReadAllText(System.String path) { }
        // RVA: 0x09D7C348  token: 0x60003F8
        public static System.String GetEnvironmentValue(System.String key) { }
        // RVA: 0x09D7C838  token: 0x60003F9
        public static System.String GetHoudiniEngineEnvironmentFilePathFull() { }
        // RVA: 0x09D7CAA4  token: 0x60003FA
        public static System.Boolean LoadFileIntoMemory(System.String path, System.Byte[]& buffer) { }
        // RVA: 0x041E1670  token: 0x60003FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005C  // size: 0x10
    public static class HEU_PlatformWin
    {
        // Fields
        public static System.UIntPtr HKEY_LOCAL_MACHINE;  // static @ 0x0
        public static System.UIntPtr HKEY_CURRENT_USER;  // static @ 0x8

        // Methods
        // RVA: 0x09D7BDE8  token: 0x60003FC
        private static System.UInt32 RegOpenKeyEx(System.UIntPtr hKey, System.String lpSubKey, System.UInt32 ulOptions, System.Int32 samDesired, System.Int32& phkResult) { }
        // RVA: 0x09D7BEAC  token: 0x60003FD
        public static System.Int32 RegQueryValueEx(System.Int32 hKey, System.String lpValueName, System.Int32 lpReserved, System.UInt32& lpType, System.Text.StringBuilder lpData, System.UInt32& lpcbData) { }
        // RVA: 0x09D7BD70  token: 0x60003FE
        private static System.UInt32 RegCloseKey(System.Int32 hKey) { }
        // RVA: 0x09D7BA98  token: 0x60003FF
        public static System.String GetRegistryKeyValue(System.UIntPtr rootKey, System.String keyName, HoudiniEngineUnity.HEU_PlatformWin.RegSAM is32or64Key, System.String inPropertyName) { }
        // RVA: 0x09D7BD04  token: 0x6000400
        public static System.String GetRegistryKeyvalue_x86(System.UIntPtr rootKey, System.String keyName, System.String inPropertyName) { }
        // RVA: 0x09D7BC98  token: 0x6000401
        public static System.String GetRegistryKeyvalue_x64(System.UIntPtr rootKey, System.String keyName, System.String inPropertyName) { }
        // RVA: 0x09D7B758  token: 0x6000402
        public static System.String GetApplicationPath(System.String appName) { }
        // RVA: 0x09D7BFAC  token: 0x6000403
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005E  // size: 0x10
    public static class HEU_PluginSettings
    {
        // Fields
        public static System.Action<System.Boolean> s_onEnableRawHoudiniChanged;  // static @ 0x0

        // Properties
        System.String HoudiniEngineEnvFilePath { get; /* RVA: 0x09D7DDF0 */ set; /* RVA: 0x09D7FAB0 */ }
        System.Boolean EnableRawHoudini { get; /* RVA: 0x09D7DB6C */ set; /* RVA: 0x09D7F88C */ }
        System.Boolean CookingEnabled { get; /* RVA: 0x09D7D360 */ set; /* RVA: 0x09D7F524 */ }
        System.Boolean CookingTriggersDownstreamCooks { get; /* RVA: 0x09D7D3C0 */ set; /* RVA: 0x09D7F574 */ }
        System.Boolean CookDisabledGameObjects { get; /* RVA: 0x09D7D1E0 */ set; /* RVA: 0x09D7F3E4 */ }
        System.Boolean CookTemplatedGeos { get; /* RVA: 0x09D7D300 */ set; /* RVA: 0x09D7F4D4 */ }
        System.Boolean PushUnityTransformToHoudini { get; /* RVA: 0x09D7E484 */ set; /* RVA: 0x09D7FFC0 */ }
        System.Boolean TransformChangeTriggersCooks { get; /* RVA: 0x09D7EBD8 */ set; /* RVA: 0x09D80580 */ }
        System.Boolean ChildTransformChangeTriggersCooks { get; /* RVA: 0x09D7D10C */ set; /* RVA: 0x09D7F340 */ }
        System.String CollisionGroupName { get; /* RVA: 0x09D7D16C */ set; /* RVA: 0x09D7F390 */ }
        System.String RenderedCollisionGroupName { get; /* RVA: 0x09D7E4E4 */ set; /* RVA: 0x09D80010 */ }
        System.String RenderedConvexCollisionGroupName { get; /* RVA: 0x09D7E558 */ set; /* RVA: 0x09D80064 */ }
        System.String UnityMaterialAttribName { get; /* RVA: 0x09D7ED94 */ set; /* RVA: 0x09D806CC */ }
        System.String UnitySubMaterialAttribName { get; /* RVA: 0x09D7EEF0 */ set; /* RVA: 0x09D807C8 */ }
        System.String UnitySubMaterialIndexAttribName { get; /* RVA: 0x09D7EF64 */ set; /* RVA: 0x09D8081C */ }
        System.String UnityTagAttributeName { get; /* RVA: 0x09D7EFD8 */ set; /* RVA: 0x09D80870 */ }
        System.String UnityStaticAttributeName { get; /* RVA: 0x09D7EE7C */ set; /* RVA: 0x09D80774 */ }
        System.String UnityScriptAttributeName { get; /* RVA: 0x09D7EE08 */ set; /* RVA: 0x09D80720 */ }
        System.String UnityLayerAttributeName { get; /* RVA: 0x09D7ED20 */ set; /* RVA: 0x09D80678 */ }
        System.Single ImageGamma { get; /* RVA: 0x09D7DED8 */ set; /* RVA: 0x09D7FB6C */ }
        System.Single NormalGenerationThresholdAngle { get; /* RVA: 0x09D7E41C */ set; /* RVA: 0x09D7FF68 */ }
        System.String LastLoadHDAPath { get; /* RVA: 0x09D7E238 */ set; /* RVA: 0x09D7FE04 */ }
        System.String LastLoadHIPPath { get; /* RVA: 0x09D7E2AC */ set; /* RVA: 0x09D7FE58 */ }
        System.String InstanceAttr { get; /* RVA: 0x09D7E0DC */ set; /* RVA: 0x09D7FD08 */ }
        System.String UnityInstanceAttr { get; /* RVA: 0x09D7ECAC */ set; /* RVA: 0x09D80624 */ }
        System.String UnityInputMeshAttr { get; /* RVA: 0x09D7EC38 */ set; /* RVA: 0x09D805D0 */ }
        UnityEngine.Color LineColor { get; /* RVA: 0x09D7E320 */ set; /* RVA: 0x09D7FEAC */ }
        System.Boolean UseHybridCurveEditing { get; /* RVA: 0x09D7F10C */ set; /* RVA: 0x09D80964 */ }
        System.String EditorOnly_Tag { get; /* RVA: 0x09D7DAF8 */ set; /* RVA: 0x09D7F808 */ }
        System.String HDAData_Name { get; /* RVA: 0x09D7DBCC */ set; /* RVA: 0x09D7F95C */ }
        HoudiniEngineUnity.SessionMode Session_Mode { get; /* RVA: 0x09D7E700 */ set; /* RVA: 0x09D801AC */ }
        System.String Session_PipeName { get; /* RVA: 0x09D7E768 */ set; /* RVA: 0x09D801FC */ }
        System.String Session_Localhost { get; /* RVA: 0x09D7E68C */ set; /* RVA: 0x09D80158 */ }
        System.Int32 Session_Port { get; /* RVA: 0x09D7E7DC */ set; /* RVA: 0x09D80250 */ }
        System.String Session_SharedMemoryName { get; /* RVA: 0x09D7E908 */ set; /* RVA: 0x09D80340 */ }
        System.Int32 Session_SharedMemoryBufferSize { get; /* RVA: 0x09D7E840 */ set; /* RVA: 0x09D802A0 */ }
        HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType Session_SharedMemoryBufferType { get; /* RVA: 0x09D7E8A4 */ set; /* RVA: 0x09D802F0 */ }
        System.Single Session_Timeout { get; /* RVA: 0x09D7E97C */ set; /* RVA: 0x09D80394 */ }
        System.Boolean Session_AutoClose { get; /* RVA: 0x09D7E62C */ set; /* RVA: 0x09D80108 */ }
        System.Boolean Curves_ShowInSceneView { get; /* RVA: 0x09D7D420 */ set; /* RVA: 0x09D7F5C4 */ }
        System.String AssetCachePath { get; /* RVA: 0x09D7D098 */ set; /* RVA: 0x09D7F2EC */ }
        System.Boolean UseFullPathNamesForOutput { get; /* RVA: 0x09D7F04C */ set; /* RVA: 0x09D808C4 */ }
        System.Collections.Generic.List<System.String> HEngineToolsShelves { get; /* RVA: 0x09D7DCA0 */ set; /* RVA: 0x09D7FA00 */ }
        System.Int32 HEngineShelfSelectedIndex { get; /* RVA: 0x09D7DC40 */ set; /* RVA: 0x09D7F9B0 */ }
        System.String DefaultTerrainMaterial { get; /* RVA: 0x09D7D71C */ set; /* RVA: 0x09D7F6BC */ }
        System.String TerrainSplatTextureDefault { get; /* RVA: 0x09D7EB64 */ set; /* RVA: 0x09D8052C */ }
        System.String DefaultStandardShader { get; /* RVA: 0x09D7D598 */ set; /* RVA: 0x09D7F668 */ }
        System.String DefaultVertexColorShader { get; /* RVA: 0x09D7D914 */ set; /* RVA: 0x09D7F764 */ }
        System.String DefaultTransparentShader { get; /* RVA: 0x09D7D790 */ set; /* RVA: 0x09D7F710 */ }
        System.String DefaultCurveShader { get; /* RVA: 0x09D7D480 */ set; /* RVA: 0x09D7F614 */ }
        System.Boolean SupportHoudiniBoxType { get; /* RVA: 0x09D7EAA4 */ set; /* RVA: 0x09D8048C */ }
        System.Boolean SupportHoudiniSphereType { get; /* RVA: 0x09D7EB04 */ set; /* RVA: 0x09D804DC */ }
        System.Boolean SetCurrentThreadToInvariantCulture { get; /* RVA: 0x09D7E9E4 */ set; /* RVA: 0x09D803EC */ }
        System.String HoudiniDebugLaunchPath { get; /* RVA: 0x09D7DD04 */ set; /* RVA: 0x09D7FA5C */ }
        System.String LastExportPath { get; /* RVA: 0x09D7E150 */ set; /* RVA: 0x09D7FD5C */ }
        System.Int32 InputSelectionFilterLocation { get; /* RVA: 0x09D7DF40 */ set; /* RVA: 0x09D7FBC4 */ }
        System.Int32 InputSelectionFilterState { get; /* RVA: 0x09D7E078 */ set; /* RVA: 0x09D7FCB8 */ }
        System.Boolean InputSelectionFilterRoots { get; /* RVA: 0x09D7E018 */ set; /* RVA: 0x09D7FC68 */ }
        System.String InputSelectionFilterName { get; /* RVA: 0x09D7DFA4 */ set; /* RVA: 0x09D7FC14 */ }
        System.Boolean CookOptionSplitGeosByGroup { get; /* RVA: 0x09D7D2A0 */ set; /* RVA: 0x09D7F484 */ }
        System.Int32 MaxVerticesPerPrimitive { get; /* RVA: 0x09D7E3B8 */ set; /* RVA: 0x09D7FF18 */ }
        System.String HoudiniInstallPath { get; /* RVA: 0x09D7DE64 */ set; /* RVA: 0x09D7FB18 */ }
        System.String LastHoudiniVersion { get; /* RVA: 0x09D7E1C4 */ set; /* RVA: 0x09D7FDB0 */ }
        System.Boolean SessionSyncAutoCook { get; /* RVA: 0x09D7E5CC */ set; /* RVA: 0x09D800B8 */ }
        System.Boolean WriteCookLogs { get; /* RVA: 0x09D7F28C */ set; /* RVA: 0x09D80AA4 */ }
        System.Boolean UseHDRColor { get; /* RVA: 0x09D7F0AC */ set; /* RVA: 0x09D80914 */ }
        System.Boolean UseSpecularShader { get; /* RVA: 0x09D7F22C */ set; /* RVA: 0x09D80A54 */ }
        System.Boolean UseLegacyShaders { get; /* RVA: 0x09D7F1CC */ set; /* RVA: 0x09D80A04 */ }
        System.Boolean ShortenFolderPaths { get; /* RVA: 0x09D7EA44 */ set; /* RVA: 0x09D8043C */ }
        System.Boolean UseLegacyInputCurves { get; /* RVA: 0x09D7F16C */ set; /* RVA: 0x09D809B4 */ }
        System.Boolean CookOnMouseUp { get; /* RVA: 0x09D7D240 */ set; /* RVA: 0x09D7F434 */ }
        System.Boolean EditableNodesToolsEnabled { get; /* RVA: 0x09D7DA98 */ set; /* RVA: 0x09D7F7B8 */ }

    }

    // TypeToken: 0x200005F  // size: 0x28
    public class HEU_PluginStorage
    {
        // Fields
        protected System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_PluginStorage.StoreData> _dataMap;  // 0x10
        private System.Collections.Generic.Dictionary<System.String,System.String> _envPathMap;  // 0x18
        protected System.Boolean _requiresSave;  // 0x20
        private static HoudiniEngineUnity.HEU_PluginStorage _instance;  // static @ 0x0
        public static System.String PluginSettingsLine1;  // const
        public static System.String PluginSettingsLine2;  // const
        public static System.String PluginSettingsVersion;  // const

        // Properties
        System.Boolean RequiresSave { get; /* RVA: 0x04D866B0 */ }
        HoudiniEngineUnity.HEU_PluginStorage Instance { get; /* RVA: 0x09D82F3C */ }

        // Methods
        // RVA: 0x0385B100  token: 0x6000494
        public System.Collections.Generic.Dictionary<System.String,System.String> GetEnvironmentPathMap() { }
        // RVA: 0x09D816AC  token: 0x6000497
        public static System.Void InstantiateAndLoad() { }
        // RVA: 0x041E1670  token: 0x6000498
        public static System.Void SetCurrentCulture(System.Boolean useInvariant) { }
        // RVA: -1  // generic def  token: 0x6000499
        private T[] GetJSONArray(System.String jsonArray) { }
        // RVA: 0x09D82D50  token: 0x600049A
        public System.Void Set(System.String key, System.Boolean value) { }
        // RVA: 0x09D82A8C  token: 0x600049B
        public System.Void Set(System.String key, System.Int32 value) { }
        // RVA: 0x09D829C8  token: 0x600049C
        public System.Void Set(System.String key, System.Int64 value) { }
        // RVA: 0x09D82C8C  token: 0x600049D
        public System.Void Set(System.String key, System.Single value) { }
        // RVA: 0x09D82930  token: 0x600049E
        public System.Void Set(System.String key, System.String value) { }
        // RVA: 0x09D82B50  token: 0x600049F
        public System.Void Set(System.String key, System.Collections.Generic.List<System.String> values, System.Char delimiter) { }
        // RVA: 0x09D81414  token: 0x60004A0
        public System.Boolean Get(System.String key, System.Boolean& value, System.Boolean defaultValue) { }
        // RVA: 0x09D81120  token: 0x60004A1
        public System.Boolean Get(System.String key, System.Int32& value, System.Int32 defaultValue) { }
        // RVA: 0x09D81340  token: 0x60004A2
        public System.Boolean Get(System.String key, System.Int64& value, System.Int64 defaultValue) { }
        // RVA: 0x09D815AC  token: 0x60004A3
        public System.Boolean Get(System.String key, System.Single& value, System.Single defaultValue) { }
        // RVA: 0x09D814E8  token: 0x60004A4
        public System.Boolean Get(System.String key, System.String& value, System.String defaultValue) { }
        // RVA: 0x09D811F4  token: 0x60004A5
        public System.Boolean Get(System.String key, System.Collections.Generic.List<System.String>& values, System.Char delimiter) { }
        // RVA: 0x041E1670  token: 0x60004A6
        protected virtual System.Void MarkDirtyForSave() { }
        // RVA: 0x09D823DC  token: 0x60004A7
        public static System.Void SaveIfRequired() { }
        // RVA: 0x09D82E14  token: 0x60004A8
        public static System.String SettingsFilePath() { }
        // RVA: 0x09D82428  token: 0x60004A9
        public virtual System.Boolean SavePluginData(System.String file) { }
        // RVA: 0x09D81BCC  token: 0x60004AA
        public virtual System.Boolean LoadPluginData(System.String file) { }
        // RVA: 0x012081B0  token: 0x60004AB
        private System.Boolean ReadFromEditorPrefs() { }
        // RVA: 0x09D80AF4  token: 0x60004AC
        public static System.Void ClearPluginData() { }
        // RVA: 0x09D81B84  token: 0x60004AD
        public static System.Void LoadFromSavedFile() { }
        // RVA: 0x09D82880  token: 0x60004AE
        public static System.String SessionFilePath() { }
        // RVA: 0x041E1670  token: 0x60004AF
        public static System.Void SaveAllSessionData(System.Collections.Generic.List<HoudiniEngineUnity.HEU_SessionBase> allSessions, System.String path) { }
        // RVA: 0x09D81794  token: 0x60004B0
        public static System.Collections.Generic.List<HoudiniEngineUnity.HEU_SessionData> LoadAllSessionData(System.String path) { }
        // RVA: 0x041E1670  token: 0x60004B1
        public static System.Void DeleteAllSavedSessionData(System.String file) { }
        // RVA: 0x09D817F4  token: 0x60004B2
        public System.Void LoadAssetEnvironmentPaths() { }
        // RVA: 0x09D80E50  token: 0x60004B3
        public System.String ConvertRealPathToEnvKeyedPath(System.String inPath) { }
        // RVA: 0x09D80BAC  token: 0x60004B4
        public System.String ConvertEnvKeyedPathToReal(System.String inPath) { }
        // RVA: 0x09D82EC4  token: 0x60004B5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x98
    public class HEU_Handle : UnityEngine.ScriptableObject, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.String _handleName;  // 0x18
        private HoudiniEngineUnity.HEU_Handle.HEU_HandleType _handleType;  // 0x20
        private System.Int32 _handleIndex;  // 0x24
        private HoudiniEngineUnity.HEU_HandleParamBinding _handleParamTranslateBinding;  // 0x28
        private HoudiniEngineUnity.HEU_HandleParamBinding _handleParamRotateBinding;  // 0x30
        private HoudiniEngineUnity.HEU_HandleParamBinding _handleParamScaleBinding;  // 0x38
        private UnityEngine.Vector3 _handlePosition;  // 0x40
        private UnityEngine.Quaternion _handleRotation;  // 0x4c
        private UnityEngine.Vector3 _handleScale;  // 0x5c
        private HoudiniEngineUnity.HAPI_RSTOrder _rstOrder;  // 0x68
        private HoudiniEngineUnity.HAPI_XYZOrder _xyzOrder;  // 0x6c
        private HoudiniEngineUnity.HAPI_TransformEuler _convertedTransformEuler;  // 0x70

        // Properties
        System.String HandleName { get; /* RVA: 0x0385B100 */ }
        HoudiniEngineUnity.HEU_Handle.HEU_HandleType HandleType { get; /* RVA: 0x02B70F90 */ }
        HoudiniEngineUnity.HAPI_RSTOrder RSTOrder { get; /* RVA: 0x04D86CF0 */ }
        HoudiniEngineUnity.HAPI_XYZOrder XYZOrder { get; /* RVA: 0x04D86B50 */ }
        HoudiniEngineUnity.HAPI_TransformEuler ConvertedTransformEuler { get; /* RVA: 0x04DA2930 */ }
        UnityEngine.Vector3 HandlePosition { get; /* RVA: 0x04D89660 */ }
        UnityEngine.Quaternion HandleRotation { get; /* RVA: 0x04D8D360 */ }
        UnityEngine.Vector3 HandleScale { get; /* RVA: 0x04DA2960 */ }

        // Methods
        // RVA: 0x068C60A8  token: 0x60004BD
        public System.Boolean HasTranslateHandle() { }
        // RVA: 0x09B11948  token: 0x60004BE
        public System.Boolean HasRotateHandle() { }
        // RVA: 0x09B0008C  token: 0x60004BF
        public System.Boolean HasScaleHandle() { }
        // RVA: 0x09D6F5A0  token: 0x60004C0
        public System.Boolean IsTranslateHandleDisabled() { }
        // RVA: 0x09D6F564  token: 0x60004C1
        public System.Boolean IsRotateHandleDisabled() { }
        // RVA: 0x09D6F578  token: 0x60004C2
        public System.Boolean IsScaleHandleDisabled() { }
        // RVA: 0x04D86240  token: 0x60004C3
        public HoudiniEngineUnity.HEU_HandleParamBinding GetTranslateBinding() { }
        // RVA: 0x011F36E0  token: 0x60004C4
        public HoudiniEngineUnity.HEU_HandleParamBinding GetRotateBinding() { }
        // RVA: 0x04D85A50  token: 0x60004C5
        public HoudiniEngineUnity.HEU_HandleParamBinding GetScaleBinding() { }
        // RVA: 0x09D6F5B4  token: 0x60004C9
        public System.Boolean SetupHandle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32 handleIndex, System.String handleName, HoudiniEngineUnity.HEU_Handle.HEU_HandleType handleType, HoudiniEngineUnity.HAPI_HandleInfo& handleInfo, HoudiniEngineUnity.HEU_Parameters parameters) { }
        // RVA: 0x09D6EAAC  token: 0x60004CA
        public System.Void CleanUp() { }
        // RVA: 0x09D6EAE4  token: 0x60004CB
        public System.Void GenerateTransform(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_Parameters parameters) { }
        // RVA: 0x09D6EEFC  token: 0x60004CC
        public System.Boolean GetUpdatedPosition(HoudiniEngineUnity.HEU_HoudiniAsset asset, UnityEngine.Vector3& inPosition) { }
        // RVA: 0x09D6EFB8  token: 0x60004CD
        public System.Boolean GetUpdatedRotation(HoudiniEngineUnity.HEU_HoudiniAsset asset, UnityEngine.Quaternion& inRotation) { }
        // RVA: 0x09D6F58C  token: 0x60004CE
        public static System.Boolean IsSpecialRSTOrder(HoudiniEngineUnity.HAPI_RSTOrder rstOrder) { }
        // RVA: 0x09D6F0EC  token: 0x60004CF
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_Handle other) { }
        // RVA: 0x09D6F6D8  token: 0x60004D0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000065  // size: 0x30
    public class HEU_HandleParamBinding : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamType _paramType;  // 0x10
        public System.Int32 _parmID;  // 0x14
        public System.String _paramName;  // 0x18
        public System.Boolean _bDisabled;  // 0x20
        public System.Boolean[] _boundChannels;  // 0x28

        // Methods
        // RVA: 0x09D6E898  token: 0x60004D1
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_HandleParamBinding other) { }
        // RVA: 0x09D6EA64  token: 0x60004D2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000067  // size: 0x10
    public static class HEU_HAPIConstants
    {
        // Fields
        public static System.Int32 HAPI_POSITION_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_SCALE_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_SHEAR_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_NORMAL_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_QUATERNION_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_EULER_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_UV_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_COLOR_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_CV_VECTOR_SIZE;  // const
        public static System.Int32 HAPI_PRIM_MIN_VERTEX_COUNT;  // const
        public static System.Int32 HAPI_PRIM_MAX_VERTEX_COUNT;  // const
        public static System.Int32 HAPI_INVALID_PARM_ID;  // const
        public static System.Int32 HAPI_MAX_NUM_CONNECTIONS;  // const
        public static System.String HAPI_ATTRIB_POSITION;  // const
        public static System.String HAPI_ATTRIB_UV;  // const
        public static System.String HAPI_ATTRIB_UV2;  // const
        public static System.String HAPI_ATTRIB_NORMAL;  // const
        public static System.String HAPI_ATTRIB_TANGENT;  // const
        public static System.String HAPI_ATTRIB_TANGENT2;  // const
        public static System.String HAPI_ATTRIB_COLOR;  // const
        public static System.String HAPI_ATTRIB_NAME;  // const
        public static System.String HAPI_ATTRIB_INSTANCE;  // const
        public static System.String HAPI_ATTRIB_ROT;  // const
        public static System.String HAPI_ATTRIB_SCALE;  // const
        public static System.String HAPI_UNGROUPED_GROUP_NAME;  // const
        public static System.String HAPI_RAW_FORMAT_NAME;  // const
        public static System.String HAPI_PNG_FORMAT_NAME;  // const
        public static System.String HAPI_JPEG_FORMAT_NAME;  // const
        public static System.String HAPI_BMP_FORMAT_NAME;  // const
        public static System.String HAPI_TIFF_FORMAT_NAME;  // const
        public static System.String HAPI_TGA_FORMAT_NAME;  // const
        public static System.String HAPI_DEFAULT_IMAGE_FORMAT_NAME;  // const
        public static System.String HAPI_GLOBAL_NODES_NODE_NAME;  // const
        public static System.String HAPI_ENV_HIP;  // const
        public static System.String HAPI_ENV_JOB;  // const
        public static System.String HAPI_ENV_CLIENT_NAME;  // const
        public static System.String HAPI_CACHE_COP2_COOK;  // const
        public static System.String HAPI_CACHE_COP2_FLIPBOOK;  // const
        public static System.String HAPI_CACHE_IMAGE;  // const
        public static System.String HAPI_CACHE_OBJ;  // const
        public static System.String HAPI_CACHE_GL_TEXTURE;  // const
        public static System.String HAPI_CACHE_GL_VERTEX;  // const
        public static System.String HAPI_CACHE_SOP;  // const
        public static System.String HAPI_CACHE_VEX;  // const
        public static System.String HAPI_ATTRIB_INPUT_CURVE_COORDS;  // const

    }

    // TypeToken: 0x2000068  // size: 0x14
    public sealed struct HAPI_License
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_NONE;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_FX;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE_INDIE;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_INDIE;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE_UNITY_UNREAL;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_EDUCATION;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE_EDUCATION;  // const
        public static HoudiniEngineUnity.HAPI_License HAPI_LICENSE_MAX;  // const

    }

    // TypeToken: 0x2000069  // size: 0x14
    public sealed struct HAPI_StatusType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_StatusType HAPI_STATUS_CALL_RESULT;  // const
        public static HoudiniEngineUnity.HAPI_StatusType HAPI_STATUS_COOK_RESULT;  // const
        public static HoudiniEngineUnity.HAPI_StatusType HAPI_STATUS_COOK_STATE;  // const
        public static HoudiniEngineUnity.HAPI_StatusType HAPI_STATUS_MAX;  // const

    }

    // TypeToken: 0x200006A  // size: 0x14
    public sealed struct HAPI_StatusVerbosity
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_StatusVerbosity HAPI_STATUSVERBOSITY_0;  // const
        public static HoudiniEngineUnity.HAPI_StatusVerbosity HAPI_STATUSVERBOSITY_1;  // const
        public static HoudiniEngineUnity.HAPI_StatusVerbosity HAPI_STATUSVERBOSITY_2;  // const
        public static HoudiniEngineUnity.HAPI_StatusVerbosity HAPI_STATUSVERBOSITY_ALL;  // const
        public static HoudiniEngineUnity.HAPI_StatusVerbosity HAPI_STATUSVERBOSITY_ERRORS;  // const
        public static HoudiniEngineUnity.HAPI_StatusVerbosity HAPI_STATUSVERBOSITY_WARNINGS;  // const
        public static HoudiniEngineUnity.HAPI_StatusVerbosity HAPI_STATUSVERBOSITY_MESSAGES;  // const

    }

    // TypeToken: 0x200006B  // size: 0x14
    public sealed struct HAPI_JobStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_JobStatus HAPI_JOB_STATUS_RUNNING;  // const
        public static HoudiniEngineUnity.HAPI_JobStatus HAPI_JOB_STATUS_IDLE;  // const
        public static HoudiniEngineUnity.HAPI_JobStatus HAPI_JOB_STATUS_MAX;  // const

    }

    // TypeToken: 0x200006C  // size: 0x14
    public sealed struct HAPI_Result
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_SUCCESS;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_FAILURE;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_ALREADY_INITIALIZED;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_NOT_INITIALIZED;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_CANT_LOADFILE;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_PARM_SET_FAILED;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_INVALID_ARGUMENT;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_CANT_LOAD_GEO;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_CANT_GENERATE_PRESET;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_CANT_LOAD_PRESET;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_ASSET_DEF_ALREADY_LOADED;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_NO_LICENSE_FOUND;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_DISALLOWED_NC_LICENSE_FOUND;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_DISALLOWED_NC_ASSET_WITH_C_LICENSE;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_DISALLOWED_NC_ASSET_WITH_LC_LICENSE;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_DISALLOWED_LC_ASSET_WITH_C_LICENSE;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_DISALLOWED_HENGINEINDIE_W_3PARTY_PLUGIN;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_SHARED_MEMORY_BUFFER_OVERFLOW;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_INVALID_SHARED_MEMORY_BUFFER;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_ASSET_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_NODE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_USER_INTERRUPTED;  // const
        public static HoudiniEngineUnity.HAPI_Result HAPI_RESULT_INVALID_SESSION;  // const

    }

    // TypeToken: 0x200006D  // size: 0x14
    public sealed struct HAPI_ErrorCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_ErrorCode HAPI_ERRORCODE_ASSET_DEF_NOT_FOUND;  // const
        public static HoudiniEngineUnity.HAPI_ErrorCode HAPI_ERRORCODE_PYTHON_NODE_ERROR;  // const

    }

    // TypeToken: 0x200006E  // size: 0x14
    public sealed struct HAPI_SessionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_SessionType HAPI_SESSION_INPROCESS;  // const
        public static HoudiniEngineUnity.HAPI_SessionType HAPI_SESSION_THRIFT;  // const
        public static HoudiniEngineUnity.HAPI_SessionType HAPI_SESSION_CUSTOM1;  // const
        public static HoudiniEngineUnity.HAPI_SessionType HAPI_SESSION_CUSTOM2;  // const
        public static HoudiniEngineUnity.HAPI_SessionType HAPI_SESSION_CUSTOM3;  // const
        public static HoudiniEngineUnity.HAPI_SessionType HAPI_SESSION_MAX;  // const

    }

    // TypeToken: 0x200006F  // size: 0x14
    public sealed struct HAPI_State
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_READY;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_READY_WITH_FATAL_ERRORS;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_READY_WITH_COOK_ERRORS;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_STARTING_COOK;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_COOKING;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_STARTING_LOAD;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_LOADING;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_MAX;  // const
        public static HoudiniEngineUnity.HAPI_State HAPI_STATE_MAX_READY_STATE;  // const

    }

    // TypeToken: 0x2000070  // size: 0x14
    public sealed struct HAPI_PackedPrimInstancingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_PackedPrimInstancingMode HAPI_PACKEDPRIM_INSTANCING_MODE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_PackedPrimInstancingMode HAPI_PACKEDPRIM_INSTANCING_MODE_DISABLED;  // const
        public static HoudiniEngineUnity.HAPI_PackedPrimInstancingMode HAPI_PACKEDPRIM_INSTANCING_MODE_HIERARCHY;  // const
        public static HoudiniEngineUnity.HAPI_PackedPrimInstancingMode HAPI_PACKEDPRIM_INSTANCING_MODE_FLAT;  // const
        public static HoudiniEngineUnity.HAPI_PackedPrimInstancingMode HAPI_PACKEDPRIM_INSTANCING_MODE_MAX;  // const

    }

    // TypeToken: 0x2000071  // size: 0x14
    public sealed struct HAPI_Permissions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_Permissions HAPI_PERMISSIONS_NON_APPLICABLE;  // const
        public static HoudiniEngineUnity.HAPI_Permissions HAPI_PERMISSIONS_READ_WRITE;  // const
        public static HoudiniEngineUnity.HAPI_Permissions HAPI_PERMISSIONS_READ_ONLY;  // const
        public static HoudiniEngineUnity.HAPI_Permissions HAPI_PERMISSIONS_WRITE_ONLY;  // const
        public static HoudiniEngineUnity.HAPI_Permissions HAPI_PERMISSIONS_MAX;  // const

    }

    // TypeToken: 0x2000072  // size: 0x14
    public sealed struct HAPI_RampType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_RampType HAPI_RAMPTYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_RampType HAPI_RAMPTYPE_FLOAT;  // const
        public static HoudiniEngineUnity.HAPI_RampType HAPI_RAMPTYPE_COLOR;  // const
        public static HoudiniEngineUnity.HAPI_RampType HAPI_RAMPTYPE_MAX;  // const

    }

    // TypeToken: 0x2000073  // size: 0x14
    public sealed struct HAPI_ParmType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_INT;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_MULTIPARMLIST;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_TOGGLE;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_BUTTON;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_FLOAT;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_COLOR;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_STRING;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_PATH_FILE;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_PATH_FILE_GEO;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_PATH_FILE_IMAGE;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_NODE;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_FOLDERLIST;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_FOLDERLIST_RADIO;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_FOLDER;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_LABEL;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_SEPARATOR;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_PATH_FILE_DIR;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_MAX;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_INT_START;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_INT_END;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_FLOAT_START;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_FLOAT_END;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_STRING_START;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_STRING_END;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_PATH_START;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_PATH_END;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_NODE_START;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_NODE_END;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_CONTAINER_START;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_CONTAINER_END;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_NONVALUE_START;  // const
        public static HoudiniEngineUnity.HAPI_ParmType HAPI_PARMTYPE_NONVALUE_END;  // const

    }

    // TypeToken: 0x2000074  // size: 0x14
    public sealed struct HAPI_PrmScriptType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_INT;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_FLOAT;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_ANGLE;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_STRING;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_FILE;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_DIRECTORY;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_IMAGE;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_GEOMETRY;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_TOGGLE;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_BUTTON;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_VECTOR2;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_VECTOR3;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_VECTOR4;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_INTVECTOR2;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_INTVECTOR3;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_INTVECTOR4;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_UV;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_UVW;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_DIR;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_COLOR;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_COLOR4;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_HUECIRCLE;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_OPPATH;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_OPLIST;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_OBJECT;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_OBJECTLIST;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_RENDER;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_SEPARATOR;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_GEOMETRY_DATA;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_KEY_VALUE_DICT;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_LABEL;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_RGBAMASK;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_ORDINAL;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_RAMP_FLT;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_RAMP_RGB;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_FLOAT_LOG;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_INT_LOG;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_DATA;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_FLOAT_MINMAX;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_INT_MINMAX;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_INT_STARTEND;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_BUTTONSTRIP;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_ICONSTRIP;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_GROUPRADIO;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_GROUPCOLLAPSIBLE;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_GROUPSIMPLE;  // const
        public static HoudiniEngineUnity.HAPI_PrmScriptType HAPI_PRM_SCRIPT_TYPE_GROUP;  // const

    }

    // TypeToken: 0x2000075  // size: 0x14
    public sealed struct HAPI_ChoiceListType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_ChoiceListType HAPI_CHOICELISTTYPE_NONE;  // const
        public static HoudiniEngineUnity.HAPI_ChoiceListType HAPI_CHOICELISTTYPE_NORMAL;  // const
        public static HoudiniEngineUnity.HAPI_ChoiceListType HAPI_CHOICELISTTYPE_MINI;  // const
        public static HoudiniEngineUnity.HAPI_ChoiceListType HAPI_CHOICELISTTYPE_REPLACE;  // const
        public static HoudiniEngineUnity.HAPI_ChoiceListType HAPI_CHOICELISTTYPE_TOGGLE;  // const

    }

    // TypeToken: 0x2000076  // size: 0x14
    public sealed struct HAPI_PresetType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_PresetType HAPI_PRESETTYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_PresetType HAPI_PRESETTYPE_BINARY;  // const
        public static HoudiniEngineUnity.HAPI_PresetType HAPI_PRESETTYPE_IDX;  // const
        public static HoudiniEngineUnity.HAPI_PresetType HAPI_PRESETTYPE_MAX;  // const

    }

    // TypeToken: 0x2000077  // size: 0x14
    public sealed struct HAPI_NodeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_ANY;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_NONE;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_OBJ;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_SOP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_CHOP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_ROP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_SHOP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_COP2;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_VOP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_DOP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_TOP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_COP;  // const
        public static HoudiniEngineUnity.HAPI_NodeType HAPI_NODETYPE_LOP;  // const

    }

    // TypeToken: 0x2000078  // size: 0x14
    public sealed struct HAPI_NodeFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_ANY;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_NONE;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_DISPLAY;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_RENDER;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_TEMPLATED;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_LOCKED;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_EDITABLE;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_BYPASS;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_NETWORK;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_OBJ_GEOMETRY;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_OBJ_CAMERA;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_OBJ_LIGHT;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_OBJ_SUBNET;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_SOP_CURVE;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_SOP_GUIDE;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_TOP_NONSCHEDULER;  // const
        public static HoudiniEngineUnity.HAPI_NodeFlags HAPI_NODEFLAGS_NON_BYPASS;  // const

    }

    // TypeToken: 0x2000079  // size: 0x14
    public sealed struct HAPI_GroupType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_GroupType HAPI_GROUPTYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_GroupType HAPI_GROUPTYPE_POINT;  // const
        public static HoudiniEngineUnity.HAPI_GroupType HAPI_GROUPTYPE_PRIM;  // const
        public static HoudiniEngineUnity.HAPI_GroupType HAPI_GROUPTYPE_EDGE;  // const
        public static HoudiniEngineUnity.HAPI_GroupType HAPI_GROUPTYPE_MAX;  // const

    }

    // TypeToken: 0x200007A  // size: 0x14
    public sealed struct HAPI_AttributeOwner
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_AttributeOwner HAPI_ATTROWNER_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_AttributeOwner HAPI_ATTROWNER_VERTEX;  // const
        public static HoudiniEngineUnity.HAPI_AttributeOwner HAPI_ATTROWNER_POINT;  // const
        public static HoudiniEngineUnity.HAPI_AttributeOwner HAPI_ATTROWNER_PRIM;  // const
        public static HoudiniEngineUnity.HAPI_AttributeOwner HAPI_ATTROWNER_DETAIL;  // const
        public static HoudiniEngineUnity.HAPI_AttributeOwner HAPI_ATTROWNER_MAX;  // const

    }

    // TypeToken: 0x200007B  // size: 0x14
    public sealed struct HAPI_CurveType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_CurveType HAPI_CURVETYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_CurveType HAPI_CURVETYPE_LINEAR;  // const
        public static HoudiniEngineUnity.HAPI_CurveType HAPI_CURVETYPE_NURBS;  // const
        public static HoudiniEngineUnity.HAPI_CurveType HAPI_CURVETYPE_BEZIER;  // const
        public static HoudiniEngineUnity.HAPI_CurveType HAPI_CURVETYPE_MAX;  // const

    }

    // TypeToken: 0x200007C  // size: 0x14
    public sealed struct HAPI_InputCurveMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_InputCurveMethod HAPI_CURVEMETHOD_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_InputCurveMethod HAPI_CURVEMETHOD_CVS;  // const
        public static HoudiniEngineUnity.HAPI_InputCurveMethod HAPI_CURVEMETHOD_BREAKPOINTS;  // const
        public static HoudiniEngineUnity.HAPI_InputCurveMethod HAPI_CURVEMETHOD_MAX;  // const

    }

    // TypeToken: 0x200007D  // size: 0x14
    public sealed struct HAPI_InputCurveParameterization
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_InputCurveParameterization HAPI_CURVEPARAMETERIZATION_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_InputCurveParameterization HAPI_CURVEPARAMETERIZATION_UNIFORM;  // const
        public static HoudiniEngineUnity.HAPI_InputCurveParameterization HAPI_CURVEPARAMETERIZATION_CHORD;  // const
        public static HoudiniEngineUnity.HAPI_InputCurveParameterization HAPI_CURVEPARAMETERIZATION_CENTRIPETAL;  // const
        public static HoudiniEngineUnity.HAPI_InputCurveParameterization HAPI_CURVEPARAMETERIZATION_MAX;  // const

    }

    // TypeToken: 0x200007E  // size: 0x14
    public sealed struct HAPI_VolumeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_VolumeType HAPI_VOLUMETYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_VolumeType HAPI_VOLUMETYPE_HOUDINI;  // const
        public static HoudiniEngineUnity.HAPI_VolumeType HAPI_VOLUMETYPE_VDB;  // const
        public static HoudiniEngineUnity.HAPI_VolumeType HAPI_VOLUMETYPE_MAX;  // const

    }

    // TypeToken: 0x200007F  // size: 0x14
    public sealed struct HAPI_VolumeVisualType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_VolumeVisualType HAPI_VOLUMEVISTYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_VolumeVisualType HAPI_VOLUMEVISTYPE_SMOKE;  // const
        public static HoudiniEngineUnity.HAPI_VolumeVisualType HAPI_VOLUMEVISTYPE_RAINBOW;  // const
        public static HoudiniEngineUnity.HAPI_VolumeVisualType HAPI_VOLUMEVISTYPE_ISO;  // const
        public static HoudiniEngineUnity.HAPI_VolumeVisualType HAPI_VOLUMEVISTYPE_INVISIBLE;  // const
        public static HoudiniEngineUnity.HAPI_VolumeVisualType HAPI_VOLUMEVISTYPE_HEIGHTFIELD;  // const
        public static HoudiniEngineUnity.HAPI_VolumeVisualType HAPI_VOLUMEVISTYPE_MAX;  // const

    }

    // TypeToken: 0x2000080  // size: 0x14
    public sealed struct HAPI_StorageType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT64;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_FLOAT;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_FLOAT64;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_STRING;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_UINT8;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT8;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT16;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_DICTIONARY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT64_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_FLOAT_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_FLOAT64_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_STRING_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_UINT8_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT8_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_INT16_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_DICTIONARY_ARRAY;  // const
        public static HoudiniEngineUnity.HAPI_StorageType HAPI_STORAGETYPE_MAX;  // const

    }

    // TypeToken: 0x2000081  // size: 0x14
    public sealed struct HAPI_AttributeTypeInfo
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_NONE;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_POINT;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_HPOINT;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_VECTOR;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_NORMAL;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_COLOR;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_QUATERNION;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_MATRIX3;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_MATRIX;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_ST;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_HIDDEN;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_BOX2;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_BOX;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_TEXTURE;  // const
        public static HoudiniEngineUnity.HAPI_AttributeTypeInfo HAPI_ATTRIBUTE_TYPE_MAX;  // const

    }

    // TypeToken: 0x2000082  // size: 0x14
    public sealed struct HAPI_GeoType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_DEFAULT;  // const
        public static HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_INTERMEDIATE;  // const
        public static HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_INPUT;  // const
        public static HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_CURVE;  // const
        public static HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_MAX;  // const

    }

    // TypeToken: 0x2000083  // size: 0x14
    public sealed struct HAPI_PartType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_MESH;  // const
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_CURVE;  // const
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_VOLUME;  // const
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_INSTANCER;  // const
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_BOX;  // const
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_SPHERE;  // const
        public static HoudiniEngineUnity.HAPI_PartType HAPI_PARTTYPE_MAX;  // const

    }

    // TypeToken: 0x2000084  // size: 0x14
    public sealed struct HAPI_InputType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_InputType HAPI_INPUT_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_InputType HAPI_INPUT_TRANSFORM;  // const
        public static HoudiniEngineUnity.HAPI_InputType HAPI_INPUT_GEOMETRY;  // const
        public static HoudiniEngineUnity.HAPI_InputType HAPI_INPUT_MAX;  // const

    }

    // TypeToken: 0x2000085  // size: 0x14
    public sealed struct HAPI_CurveOrders
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_CurveOrders HAPI_CURVE_ORDER_VARYING;  // const
        public static HoudiniEngineUnity.HAPI_CurveOrders HAPI_CURVE_ORDER_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_CurveOrders HAPI_CURVE_ORDER_LINEAR;  // const
        public static HoudiniEngineUnity.HAPI_CurveOrders HAPI_CURVE_ORDER_QUADRATIC;  // const
        public static HoudiniEngineUnity.HAPI_CurveOrders HAPI_CURVE_ORDER_CUBIC;  // const

    }

    // TypeToken: 0x2000086  // size: 0x14
    public sealed struct HAPI_TransformComponent
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_TX;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_TY;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_TZ;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_RX;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_RY;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_RZ;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_QX;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_QY;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_QZ;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_QW;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_SX;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_SY;  // const
        public static HoudiniEngineUnity.HAPI_TransformComponent HAPI_TRANSFORM_SZ;  // const

    }

    // TypeToken: 0x2000087  // size: 0x14
    public sealed struct HAPI_RSTOrder
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_RSTOrder HAPI_TRS;  // const
        public static HoudiniEngineUnity.HAPI_RSTOrder HAPI_TSR;  // const
        public static HoudiniEngineUnity.HAPI_RSTOrder HAPI_RTS;  // const
        public static HoudiniEngineUnity.HAPI_RSTOrder HAPI_RST;  // const
        public static HoudiniEngineUnity.HAPI_RSTOrder HAPI_STR;  // const
        public static HoudiniEngineUnity.HAPI_RSTOrder HAPI_SRT;  // const
        public static HoudiniEngineUnity.HAPI_RSTOrder HAPI_RSTORDER_DEFAULT;  // const

    }

    // TypeToken: 0x2000088  // size: 0x14
    public sealed struct HAPI_XYZOrder
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_XYZOrder HAPI_XYZ;  // const
        public static HoudiniEngineUnity.HAPI_XYZOrder HAPI_XZY;  // const
        public static HoudiniEngineUnity.HAPI_XYZOrder HAPI_YXZ;  // const
        public static HoudiniEngineUnity.HAPI_XYZOrder HAPI_YZX;  // const
        public static HoudiniEngineUnity.HAPI_XYZOrder HAPI_ZXY;  // const
        public static HoudiniEngineUnity.HAPI_XYZOrder HAPI_ZYX;  // const
        public static HoudiniEngineUnity.HAPI_XYZOrder HAPI_XYZORDER_DEFAULT;  // const

    }

    // TypeToken: 0x2000089  // size: 0x14
    public sealed struct HAPI_ImageDataFormat
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_UNKNOWN;  // const
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_INT8;  // const
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_INT16;  // const
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_INT32;  // const
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_FLOAT16;  // const
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_FLOAT32;  // const
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_MAX;  // const
        public static HoudiniEngineUnity.HAPI_ImageDataFormat HAPI_IMAGE_DATA_DEFAULT;  // const

    }

    // TypeToken: 0x200008A  // size: 0x14
    public sealed struct HAPI_ImagePacking
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_UNKNOWN;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_SINGLE;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_DUAL;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_RGB;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_BGR;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_RGBA;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_ABGR;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_MAX;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_DEFAULT3;  // const
        public static HoudiniEngineUnity.HAPI_ImagePacking HAPI_IMAGE_PACKING_DEFAULT4;  // const

    }

    // TypeToken: 0x200008B  // size: 0x14
    public sealed struct HAPI_EnvIntType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_VERSION_HOUDINI_MAJOR;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_VERSION_HOUDINI_MINOR;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_VERSION_HOUDINI_BUILD;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_VERSION_HOUDINI_PATCH;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_VERSION_HOUDINI_ENGINE_MAJOR;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_VERSION_HOUDINI_ENGINE_MINOR;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_VERSION_HOUDINI_ENGINE_API;  // const
        public static HoudiniEngineUnity.HAPI_EnvIntType HAPI_ENVINT_MAX;  // const

    }

    // TypeToken: 0x200008C  // size: 0x14
    public sealed struct HAPI_SessionEnvIntType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_SessionEnvIntType HAPI_SESSIONENVINT_INVALID;  // const
        public static HoudiniEngineUnity.HAPI_SessionEnvIntType HAPI_SESSIONENVINT_LICENSE;  // const
        public static HoudiniEngineUnity.HAPI_SessionEnvIntType HAPI_SESSIONENVINT_MAX;  // const

    }

    // TypeToken: 0x200008D  // size: 0x14
    public sealed struct HAPI_CacheProperty
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_CacheProperty HAPI_CACHEPROP_CURRENT;  // const
        public static HoudiniEngineUnity.HAPI_CacheProperty HAPI_CACHEPROP_HAS_MIN;  // const
        public static HoudiniEngineUnity.HAPI_CacheProperty HAPI_CACHEPROP_MIN;  // const
        public static HoudiniEngineUnity.HAPI_CacheProperty HAPI_CACHEPROP_HAS_MAX;  // const
        public static HoudiniEngineUnity.HAPI_CacheProperty HAPI_CACHEPROP_MAX;  // const
        public static HoudiniEngineUnity.HAPI_CacheProperty HAPI_CACHEPROP_CULL_LEVEL;  // const

    }

    // TypeToken: 0x200008E  // size: 0x14
    public sealed struct HAPI_HeightFieldSampling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_HeightFieldSampling HAPI_HEIGHTFIELD_SAMPLING_CENTER;  // const
        public static HoudiniEngineUnity.HAPI_HeightFieldSampling HAPI_HEIGHTFIELD_SAMPLING_CORNER;  // const

    }

    // TypeToken: 0x200008F  // size: 0x14
    public sealed struct HAPI_PDG_State
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_PDG_State HAPI_PDG_STATE_READY;  // const
        public static HoudiniEngineUnity.HAPI_PDG_State HAPI_PDG_STATE_COOKING;  // const
        public static HoudiniEngineUnity.HAPI_PDG_State HAPI_PDG_STATE_MAX;  // const
        public static HoudiniEngineUnity.HAPI_PDG_State HAPI_PDG_STATE_MAX_READY_STATE;  // const

    }

    // TypeToken: 0x2000090  // size: 0x14
    public sealed struct HAPI_PDG_EventType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NULL;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_ADD;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_REMOVE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_STATE_CHANGE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_ADD_DEP;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_REMOVE_DEP;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_ADD_PARENT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_REMOVE_PARENT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_CLEAR;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_COOK_ERROR;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_COOK_WARNING;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_COOK_COMPLETE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_DIRTY_START;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_DIRTY_STOP;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_DIRTY_ALL;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_UI_SELECT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_CREATE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_REMOVE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_RENAME;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_CONNECT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_DISCONNECT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_FIRST_COOK;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_SET_INT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_SET_FLOAT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_SET_STRING;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_SET_FILE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_SET_DICT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_SET_PYOBJECT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_SET_GEOMETRY;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_MERGE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_RESULT;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_PRIORITY;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_COOK_START;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_ADD_STATIC_ANCESTOR;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_REMOVE_STATIC_ANCESTOR;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_PROGRESS_UPDATE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_BATCH_ITEM_INITIALIZED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_ALL;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_LOG;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_SCHEDULER_ADDED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_SCHEDULER_REMOVED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_SET_SCHEDULER;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_SERVICE_MANAGER_ALL;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_COOKED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_NODE_GENERATED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_EVENT_WORKITEM_FRAME;  // const
        public static HoudiniEngineUnity.HAPI_PDG_EventType HAPI_PDG_CONTEXT_EVENTS;  // const

    }

    // TypeToken: 0x2000091  // size: 0x14
    public sealed struct HAPI_PDG_WorkItemState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_UNDEFINED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_UNCOOKED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_WAITING;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_SCHEDULED;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_COOKING;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_COOKED_SUCCESS;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_COOKED_CACHE;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_COOKED_FAIL;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_COOKED_CANCEL;  // const
        public static HoudiniEngineUnity.HAPI_PDG_WorkItemState HAPI_PDG_WORKITEM_DIRTY;  // const

    }

    // TypeToken: 0x2000092  // size: 0x14
    public sealed struct HAPI_TCP_PortType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_TCP_PortType HAPI_TCP_PORT_ANY;  // const
        public static HoudiniEngineUnity.HAPI_TCP_PortType HAPI_TCP_PORT_RANGE;  // const
        public static HoudiniEngineUnity.HAPI_TCP_PortType HAPI_TCP_PORT_LIST;  // const

    }

    // TypeToken: 0x2000093  // size: 0x14
    public sealed struct HAPI_ThriftSharedMemoryBufferType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType HAPI_THRIFT_SHARED_MEMORY_FIXED_LENGTH_BUFFER;  // const
        public static HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType HAPI_THRIFT_SHARED_MEMORY_RING_BUFFER;  // const

    }

    // TypeToken: 0x2000094  // size: 0x10
    public static class HEU_HAPIFunctions
    {
    }

    // TypeToken: 0x2000095  // size: 0x38
    public sealed struct HAPI_Transform
    {
        // Fields
        public System.Single[] position;  // 0x10
        public System.Single[] rotationQuaternion;  // 0x18
        public System.Single[] scale;  // 0x20
        public System.Single[] shear;  // 0x28
        public HoudiniEngineUnity.HAPI_RSTOrder rstOrder;  // 0x30

        // Methods
        // RVA: 0x09D69C78  token: 0x60004D3
        public System.Void .ctor(System.Boolean initializeFields) { }
        // RVA: 0x09D69BB8  token: 0x60004D4
        public System.Void Init() { }

    }

    // TypeToken: 0x2000096  // size: 0x38
    public sealed struct HAPI_TransformEuler
    {
        // Fields
        public System.Single[] position;  // 0x10
        public System.Single[] rotationEuler;  // 0x18
        public System.Single[] scale;  // 0x20
        public System.Single[] shear;  // 0x28
        public HoudiniEngineUnity.HAPI_XYZOrder rotationOrder;  // 0x30
        public HoudiniEngineUnity.HAPI_RSTOrder rstOrder;  // 0x34

        // Methods
        // RVA: 0x09D69AF0  token: 0x60004D5
        public System.Void .ctor(System.Boolean initializeFields) { }
        // RVA: 0x09D69A4C  token: 0x60004D6
        public System.Void Init() { }

    }

    // TypeToken: 0x2000097  // size: 0x20
    public sealed struct HAPI_Session
    {
        // Fields
        public HoudiniEngineUnity.HAPI_SessionType type;  // 0x10
        public System.Int64 id;  // 0x18

    }

    // TypeToken: 0x2000098  // size: 0x40
    public sealed struct HAPI_SessionInfo
    {
        // Fields
        public System.Int32 connectionCount;  // 0x10
        public HoudiniEngineUnity.HAPI_TCP_PortType portType;  // 0x14
        public System.Int32 minPort;  // 0x18
        public System.Int32 maxPort;  // 0x1c
        public System.Int32[] ports;  // 0x20
        public HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType;  // 0x28
        public System.Int64 sharedMemoryBufferSize;  // 0x30
        public System.Boolean enableSharedMemoryDataTransfer;  // 0x38

    }

    // TypeToken: 0x2000099  // size: 0x28
    public sealed struct HAPI_ThriftServerOptions
    {
        // Fields
        public System.Boolean autoClose;  // 0x10
        public System.Single timeoutMs;  // 0x14
        public HoudiniEngineUnity.HAPI_StatusVerbosity verbosity;  // 0x18
        public HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType;  // 0x1c
        public System.Int64 sharedMemoryBufferSize;  // 0x20

    }

    // TypeToken: 0x200009A  // size: 0x28
    public sealed struct HAPI_TimelineOptions
    {
        // Fields
        public System.Double fps;  // 0x10
        public System.Double startTime;  // 0x18
        public System.Double endTime;  // 0x20

    }

    // TypeToken: 0x200009B  // size: 0x50
    public sealed struct HAPI_AssetInfo
    {
        // Fields
        public System.Int32 nodeId;  // 0x10
        public System.Int32 objectNodeId;  // 0x14
        public System.Boolean hasEverCooked;  // 0x18
        public System.Int32 nameSH;  // 0x1c
        public System.Int32 labelSH;  // 0x20
        public System.Int32 filePathSH;  // 0x24
        public System.Int32 versionSH;  // 0x28
        public System.Int32 fullOpNameSH;  // 0x2c
        public System.Int32 helpTextSH;  // 0x30
        public System.Int32 helpURLSH;  // 0x34
        public System.Int32 objectCount;  // 0x38
        public System.Int32 handleCount;  // 0x3c
        public System.Int32 transformInputCount;  // 0x40
        public System.Int32 geoInputCount;  // 0x44
        public System.Int32 geoOutputCount;  // 0x48
        public System.Boolean haveObjectsChanged;  // 0x4c
        public System.Boolean haveMaterialsChanged;  // 0x4d

    }

    // TypeToken: 0x200009C  // size: 0x40
    public sealed struct HAPI_CookOptions
    {
        // Fields
        public System.Boolean splitGeosByGroup;  // 0x10
        public System.Int32 splitGroupSH;  // 0x14
        public System.Boolean splitGeosByAttribute;  // 0x18
        public System.Int32 splitAttrSH;  // 0x1c
        public System.Int32 maxVerticesPerPrimitive;  // 0x20
        public System.Boolean refineCurveToLinear;  // 0x24
        public System.Single curveRefineLOD;  // 0x28
        public System.Boolean clearErrorsAndWarnings;  // 0x2c
        public System.Boolean cookTemplatedGeos;  // 0x2d
        public System.Boolean splitPointsByVertexAttributes;  // 0x2e
        public HoudiniEngineUnity.HAPI_PackedPrimInstancingMode packedPrimInstancingMode;  // 0x30
        public System.Boolean handleBoxPartTypes;  // 0x34
        public System.Boolean handleSpherePartTypes;  // 0x35
        public System.Boolean checkPartChanges;  // 0x36
        public System.Boolean cacheMeshTopology;  // 0x37
        public System.Boolean preferOutputNodes;  // 0x38
        public System.Int32 extraFlags;  // 0x3c

    }

    // TypeToken: 0x200009D  // size: 0x54
    public sealed struct HAPI_NodeInfo
    {
        // Fields
        public System.Int32 id;  // 0x10
        public System.Int32 parentId;  // 0x14
        public System.Int32 nameSH;  // 0x18
        public HoudiniEngineUnity.HAPI_NodeType type;  // 0x1c
        public System.Boolean isValid;  // 0x20
        public System.Int32 totalCookCount;  // 0x24
        public System.Int32 uniqueHoudiniNodeId;  // 0x28
        public System.Int32 internalNodePathSH;  // 0x2c
        public System.Int32 parmCount;  // 0x30
        public System.Int32 parmIntValueCount;  // 0x34
        public System.Int32 parmFloatValueCount;  // 0x38
        public System.Int32 parmStringValueCount;  // 0x3c
        public System.Int32 parmChoiceCount;  // 0x40
        public System.Int32 childNodeCount;  // 0x44
        public System.Int32 inputCount;  // 0x48
        public System.Int32 outputCount;  // 0x4c
        public System.Boolean createdPostAssetLoad;  // 0x50
        public System.Boolean isTimeDependent;  // 0x51

    }

    // TypeToken: 0x200009E  // size: 0xA4
    public sealed struct HAPI_ParmInfo
    {
        // Fields
        public System.Int32 id;  // 0x10
        public System.Int32 parentId;  // 0x14
        public System.Int32 childIndex;  // 0x18
        public HoudiniEngineUnity.HAPI_ParmType type;  // 0x1c
        public HoudiniEngineUnity.HAPI_PrmScriptType scriptType;  // 0x20
        public System.Int32 typeInfoSH;  // 0x24
        public HoudiniEngineUnity.HAPI_Permissions permissions;  // 0x28
        public System.Int32 tagCount;  // 0x2c
        public System.Int32 size;  // 0x30
        public HoudiniEngineUnity.HAPI_ChoiceListType choiceListType;  // 0x34
        public System.Int32 choiceCount;  // 0x38
        public System.Int32 nameSH;  // 0x3c
        public System.Int32 labelSH;  // 0x40
        public System.Int32 templateNameSH;  // 0x44
        public System.Int32 helpSH;  // 0x48
        public System.Boolean hasMin;  // 0x4c
        public System.Boolean hasMax;  // 0x4d
        public System.Boolean hasUIMin;  // 0x4e
        public System.Boolean hasUIMax;  // 0x4f
        public System.Single min;  // 0x50
        public System.Single max;  // 0x54
        public System.Single UIMin;  // 0x58
        public System.Single UIMax;  // 0x5c
        public System.Boolean invisible;  // 0x60
        public System.Boolean disabled;  // 0x61
        public System.Boolean spare;  // 0x62
        public System.Boolean joinNext;  // 0x63
        public System.Boolean labelNone;  // 0x64
        public System.Int32 intValuesIndex;  // 0x68
        public System.Int32 floatValuesIndex;  // 0x6c
        public System.Int32 stringValuesIndex;  // 0x70
        public System.Int32 choiceIndex;  // 0x74
        public HoudiniEngineUnity.HAPI_NodeType inputNodeType;  // 0x78
        public HoudiniEngineUnity.HAPI_NodeFlags inputNodeFlag;  // 0x7c
        public System.Boolean isChildOfMultiParm;  // 0x80
        public System.Int32 instanceNum;  // 0x84
        public System.Int32 instanceLength;  // 0x88
        public System.Int32 instanceCount;  // 0x8c
        public System.Int32 instanceStartOffset;  // 0x90
        public HoudiniEngineUnity.HAPI_RampType rampType;  // 0x94
        public System.Int32 visibilityConditionSH;  // 0x98
        public System.Int32 disabledConditionSH;  // 0x9c
        public System.Boolean useMenuItemTokenAsValue;  // 0xa0

        // Methods
        // RVA: 0x09D69724  token: 0x60004D7
        public System.Boolean isInt() { }
        // RVA: 0x09D69710  token: 0x60004D8
        public System.Boolean isFloat() { }
        // RVA: 0x09D69780  token: 0x60004D9
        public System.Boolean isString() { }
        // RVA: 0x09D69768  token: 0x60004DA
        public System.Boolean isPath() { }
        // RVA: 0x09D69744  token: 0x60004DB
        public System.Boolean isNode() { }
        // RVA: 0x09D69754  token: 0x60004DC
        public System.Boolean isNonValue() { }

    }

    // TypeToken: 0x200009F  // size: 0x1C
    public sealed struct HAPI_ParmChoiceInfo
    {
        // Fields
        public System.Int32 parentParmId;  // 0x10
        public System.Int32 labelSH;  // 0x14
        public System.Int32 valueSH;  // 0x18

    }

    // TypeToken: 0x20000A0  // size: 0x1C
    public sealed struct HAPI_HandleInfo
    {
        // Fields
        public System.Int32 nameSH;  // 0x10
        public System.Int32 typeNameSH;  // 0x14
        public System.Int32 bindingsCount;  // 0x18

    }

    // TypeToken: 0x20000A1  // size: 0x20
    public sealed struct HAPI_HandleBindingInfo
    {
        // Fields
        public System.Int32 handleParmNameSH;  // 0x10
        public System.Int32 assetParmNameSH;  // 0x14
        public System.Int32 assetParmId;  // 0x18
        public System.Int32 assetParmIndex;  // 0x1c

    }

    // TypeToken: 0x20000A2  // size: 0x2C
    public sealed struct HAPI_ObjectInfo
    {
        // Fields
        public System.Int32 nameSH;  // 0x10
        public System.Int32 objectInstancePathSH;  // 0x14
        public System.Boolean hasTransformChanged;  // 0x18
        public System.Boolean haveGeosChanged;  // 0x19
        public System.Boolean isVisible;  // 0x1a
        public System.Boolean isInstancer;  // 0x1b
        public System.Boolean isInstanced;  // 0x1c
        public System.Int32 geoCount;  // 0x20
        public System.Int32 nodeId;  // 0x24
        public System.Int32 objectToInstanceId;  // 0x28

    }

    // TypeToken: 0x20000A3  // size: 0x34
    public sealed struct HAPI_GeoInfo
    {
        // Fields
        public HoudiniEngineUnity.HAPI_GeoType type;  // 0x10
        public System.Int32 nameSH;  // 0x14
        public System.Int32 nodeId;  // 0x18
        public System.Boolean isEditable;  // 0x1c
        public System.Boolean isTemplated;  // 0x1d
        public System.Boolean isDisplayGeo;  // 0x1e
        public System.Boolean hasGeoChanged;  // 0x1f
        public System.Boolean hasMaterialChanged;  // 0x20
        public System.Int32 pointGroupCount;  // 0x24
        public System.Int32 primitiveGroupCount;  // 0x28
        public System.Int32 edgeGroupCount;  // 0x2c
        public System.Int32 partCount;  // 0x30

        // Methods
        // RVA: 0x09D696AC  token: 0x60004DD
        public System.Int32 getGroupCountByType(HoudiniEngineUnity.HAPI_GroupType type) { }

    }

    // TypeToken: 0x20000A4  // size: 0x40
    public sealed struct HAPI_PartInfo
    {
        // Fields
        public System.Int32 id;  // 0x10
        public System.Int32 nameSH;  // 0x14
        public HoudiniEngineUnity.HAPI_PartType type;  // 0x18
        public System.Int32 faceCount;  // 0x1c
        public System.Int32 vertexCount;  // 0x20
        public System.Int32 pointCount;  // 0x24
        public System.Int32[] attributeCounts;  // 0x28
        public System.Boolean isInstanced;  // 0x30
        public System.Int32 instancedPartCount;  // 0x34
        public System.Int32 instanceCount;  // 0x38
        public System.Boolean hasChanged;  // 0x3c

        // Properties
        System.Int32 pointAttributeCount { get; /* RVA: 0x09D69810 */ set; /* RVA: 0x09D6993C */ }
        System.Int32 primitiveAttributeCount { get; /* RVA: 0x09D69844 */ set; /* RVA: 0x09D6997C */ }
        System.Int32 vertexAttributeCount { get; /* RVA: 0x09D69878 */ set; /* RVA: 0x09D699BC */ }
        System.Int32 detailAttributeCount { get; /* RVA: 0x09D697DC */ set; /* RVA: 0x09D698FC */ }

        // Methods
        // RVA: 0x09D697A0  token: 0x60004DE
        public System.Int32 getElementCountByAttributeOwner(HoudiniEngineUnity.HAPI_AttributeOwner owner) { }
        // RVA: 0x09D697C8  token: 0x60004DF
        public System.Int32 getElementCountByGroupType(HoudiniEngineUnity.HAPI_GroupType type) { }
        // RVA: 0x09D698AC  token: 0x60004E0
        public System.Void init() { }

    }

    // TypeToken: 0x20000A5  // size: 0x38
    public sealed struct HAPI_AttributeInfo
    {
        // Fields
        public System.Boolean exists;  // 0x10
        public HoudiniEngineUnity.HAPI_AttributeOwner owner;  // 0x14
        public HoudiniEngineUnity.HAPI_StorageType storage;  // 0x18
        public HoudiniEngineUnity.HAPI_AttributeOwner originalOwner;  // 0x1c
        public System.Int32 count;  // 0x20
        public System.Int32 tupleSize;  // 0x24
        public System.Int64 totalArrayElements;  // 0x28
        public HoudiniEngineUnity.HAPI_AttributeTypeInfo typeInfo;  // 0x30

        // Methods
        // RVA: 0x04DA2730  token: 0x60004E9
        public System.Void .ctor(System.String ignored) { }

    }

    // TypeToken: 0x20000A6  // size: 0x18
    public sealed struct HAPI_MaterialInfo
    {
        // Fields
        public System.Int32 nodeId;  // 0x10
        public System.Boolean exists;  // 0x14
        public System.Boolean hasChanged;  // 0x15

    }

    // TypeToken: 0x20000A7  // size: 0x1C
    public sealed struct HAPI_ImageFileFormat
    {
        // Fields
        public System.Int32 nameSH;  // 0x10
        public System.Int32 descriptionSH;  // 0x14
        public System.Int32 defaultExtensionSH;  // 0x18

    }

    // TypeToken: 0x20000A8  // size: 0x30
    public sealed struct HAPI_ImageInfo
    {
        // Fields
        public System.Int32 imageFileFormatNameSH;  // 0x10
        public System.Int32 xRes;  // 0x14
        public System.Int32 yRes;  // 0x18
        public HoudiniEngineUnity.HAPI_ImageDataFormat dataFormat;  // 0x1c
        public System.Boolean interleaved;  // 0x20
        public HoudiniEngineUnity.HAPI_ImagePacking packing;  // 0x24
        public System.Double gamma;  // 0x28

    }

    // TypeToken: 0x20000A9  // size: 0x20
    public sealed struct HAPI_Keyframe
    {
        // Fields
        public System.Single time;  // 0x10
        public System.Single value;  // 0x14
        public System.Single inTangent;  // 0x18
        public System.Single outTangent;  // 0x1c

        // Methods
        // RVA: 0x04D88AD0  token: 0x60004EA
        public System.Void .ctor(System.Single t, System.Single v, System.Single in_tangent, System.Single out_tangent) { }

    }

    // TypeToken: 0x20000AA  // size: 0x78
    public sealed struct HAPI_VolumeInfo
    {
        // Fields
        public System.Int32 nameSH;  // 0x10
        public HoudiniEngineUnity.HAPI_VolumeType type;  // 0x14
        public System.Int32 xLength;  // 0x18
        public System.Int32 yLength;  // 0x1c
        public System.Int32 zLength;  // 0x20
        public System.Int32 minX;  // 0x24
        public System.Int32 minY;  // 0x28
        public System.Int32 minZ;  // 0x2c
        public System.Int32 tupleSize;  // 0x30
        public HoudiniEngineUnity.HAPI_StorageType storage;  // 0x34
        public System.Int32 tileSize;  // 0x38
        public HoudiniEngineUnity.HAPI_Transform transform;  // 0x40
        public System.Boolean hasTaper;  // 0x68
        public System.Single xTaper;  // 0x6c
        public System.Single yTaper;  // 0x70

    }

    // TypeToken: 0x20000AB  // size: 0x20
    public sealed struct HAPI_VolumeTileInfo
    {
        // Fields
        public System.Int32 minX;  // 0x10
        public System.Int32 minY;  // 0x14
        public System.Int32 minZ;  // 0x18
        public System.Boolean isValid;  // 0x1c

    }

    // TypeToken: 0x20000AC  // size: 0x1C
    public sealed struct HAPI_VolumeVisualInfo
    {
        // Fields
        public HoudiniEngineUnity.HAPI_VolumeVisualType type;  // 0x10
        public System.Single iso;  // 0x14
        public System.Single density;  // 0x18

    }

    // TypeToken: 0x20000AD  // size: 0x2C
    public sealed struct HAPI_CurveInfo
    {
        // Fields
        public HoudiniEngineUnity.HAPI_CurveType curveType;  // 0x10
        public System.Int32 curveCount;  // 0x14
        public System.Int32 vertexCount;  // 0x18
        public System.Int32 knotCount;  // 0x1c
        public System.Boolean isPeriodic;  // 0x20
        public System.Boolean isRational;  // 0x21
        public System.Int32 order;  // 0x24
        public System.Boolean hasKnots;  // 0x28
        public System.Boolean isClosed;  // 0x29

    }

    // TypeToken: 0x20000AE  // size: 0x24
    public sealed struct HAPI_InputCurveInfo
    {
        // Fields
        public HoudiniEngineUnity.HAPI_CurveType curveType;  // 0x10
        public System.Int32 order;  // 0x14
        public System.Boolean closed;  // 0x18
        public System.Boolean reverse;  // 0x19
        public HoudiniEngineUnity.HAPI_InputCurveMethod inputMethod;  // 0x1c
        public HoudiniEngineUnity.HAPI_InputCurveParameterization breakpointParameterization;  // 0x20

        // Methods
        // RVA: 0x09D696C0  token: 0x60004EB
        public System.Void FillData(HoudiniEngineUnity.HEU_InputCurveInfo curveInfo) { }

    }

    // TypeToken: 0x20000AF  // size: 0x28
    public sealed struct HAPI_BoxInfo
    {
        // Fields
        public System.Single[] center;  // 0x10
        public System.Single[] size;  // 0x18
        public System.Single[] rotation;  // 0x20

        // Methods
        // RVA: 0x09D69624  token: 0x60004EC
        public System.Void .ctor(System.Boolean initialize_fields) { }

    }

    // TypeToken: 0x20000B0  // size: 0x20
    public sealed struct HAPI_SphereInfo
    {
        // Fields
        public System.Single[] center;  // 0x10
        public System.Single radius;  // 0x18

        // Methods
        // RVA: 0x09D699FC  token: 0x60004ED
        public System.Void .ctor(System.Boolean initialize_fields) { }

    }

    // TypeToken: 0x20000B1  // size: 0x2C
    public sealed struct HAPI_PDG_EventInfo
    {
        // Fields
        public System.Int32 nodeId;  // 0x10
        public System.Int32 workItemId;  // 0x14
        public System.Int32 dependencyId;  // 0x18
        public System.Int32 currentState;  // 0x1c
        public System.Int32 lastState;  // 0x20
        public System.Int32 eventType;  // 0x24
        public System.Int32 msgSH;  // 0x28

    }

    // TypeToken: 0x20000B2  // size: 0x1C
    public sealed struct HAPI_PDG_WorkItemInfo
    {
        // Fields
        public System.Int32 index;  // 0x10
        public System.Int32 outputFileCount;  // 0x14
        public System.Int32 nameSH;  // 0x18

    }

    // TypeToken: 0x20000B3  // size: 0x20
    public sealed struct HAPI_PDG_WorkItemOutputFile
    {
        // Fields
        public System.Int32 filePathSH;  // 0x10
        public System.Int32 tagSH;  // 0x14
        public System.Int64 hash;  // 0x18

    }

    // TypeToken: 0x20000B4  // size: 0x28
    public sealed struct HAPI_Viewport
    {
        // Fields
        public System.Single[] position;  // 0x10
        public System.Single[] rotationQuaternion;  // 0x18
        public System.Single offset;  // 0x20

        // Methods
        // RVA: 0x09D69DB8  token: 0x60004EE
        public System.Void .ctor(System.Boolean initializeFields) { }
        // RVA: 0x09D69D40  token: 0x60004EF
        public System.Void Init() { }

    }

    // TypeToken: 0x20000B5  // size: 0x12
    public sealed struct HAPI_SessionSyncInfo
    {
        // Fields
        public System.Boolean cookUsingHoudiniTime;  // 0x10
        public System.Boolean syncViewport;  // 0x11

    }

    // TypeToken: 0x20000B6  // size: 0x18
    public sealed struct HAPI_CompositorOptions
    {
        // Fields
        public System.Int32 maximumResolutionX;  // 0x10
        public System.Int32 maximumResolutionY;  // 0x14

    }

    // TypeToken: 0x20000B7  // size: 0x10
    public class HEU_HARCImports
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60004F0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B8  // size: 0x10
    public class HEU_HoudiniVersion
    {
        // Fields
        public static System.Int32 HOUDINI_MAJOR;  // const
        public static System.Int32 HOUDINI_MINOR;  // const
        public static System.Int32 HOUDINI_BUILD;  // const
        public static System.Int32 HOUDINI_PATCH;  // const
        public static System.String HOUDINI_VERSION_STRING;  // const
        public static System.Int32 HOUDINI_ENGINE_MAJOR;  // const
        public static System.Int32 HOUDINI_ENGINE_MINOR;  // const
        public static System.Int32 HOUDINI_ENGINE_API;  // const
        public static System.Int32 UNITY_PLUGIN_VERSION;  // const
        public static System.String HAPI_BIN_PATH;  // const
        public static System.String HAPI_LIBRARY;  // const
        public static System.String HARC_LIBRARY;  // const
        public static System.String SIDEFX_SOFTWARE_REGISTRY;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x60004F1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B9  // size: 0x10
    public class HTC_APIConstants
    {
        // Fields
        public static System.String HOUDINI_MASTER_SERVER_HOSTNAME;  // const
        public static System.Int32 HOUDINI_MASTER_QUERY_FOR_UNITY_PORT;  // const
        public static System.Int32 TCP_HEADER_SIZE;  // const
        public static System.Int32 MAX_ERROR_INFO_SIZE;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x60004F2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BA  // size: 0x10
    public class HTC_VERSION
    {
        // Fields
        public static System.String LIBRARY;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x60004F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BB  // size: 0x10
    public class HoudiniConstsIdentifier
    {
        // Fields
        public static System.String HOUDINI_VERSION;  // const
        public static System.String PROJECT_TAG;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x60004F4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BC  // size: 0x10
    public class HTC_APIFunctions
    {
        // Methods
        // RVA: 0x09D830A0  token: 0x60004F5
        private static System.Void InitHoudiniTcpClient(System.String address, System.Int32 port, System.Boolean use_ip, System.Text.StringBuilder error_msg) { }
        // RVA: 0x09D82F90  token: 0x60004F6
        private static System.Void GetHarsInfo(HoudiniEngineUnity.HarsInfo& harsInfo, System.Text.StringBuilder error_msg) { }
        // RVA: 0x09D83180  token: 0x60004F7
        private static System.Void UninitializeHoudiniTcpClient(System.Text.StringBuilder error_msg) { }
        // RVA: 0x041E1670  token: 0x60004F8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BD  // size: 0x20
    public sealed struct HarsInfo
    {
        // Fields
        public System.String hars_address;  // 0x10
        public System.Int32 port;  // 0x18

    }

    // TypeToken: 0x20000BE  // size: 0x20
    public sealed struct GenericMessage
    {
        // Fields
        public System.String protocol;  // 0x10
        public System.Object body;  // 0x18

    }

    // TypeToken: 0x20000BF  // size: 0x10
    public static class Logger
    {
        // Methods
        // RVA: 0x09D84FDC  token: 0x60004F9
        public static System.Void LogError(System.String msg) { }
        // RVA: 0x09D85074  token: 0x60004FA
        public static System.Void LogWarning(System.String msg) { }
        // RVA: 0x09D85028  token: 0x60004FB
        public static System.Void LogInfo(System.String msg) { }

    }

    // TypeToken: 0x20000C0  // size: 0x18
    public class HTC_SessionManager : System.IDisposable
    {
        // Fields
        private System.Boolean disposedValue;  // 0x10
        private static HoudiniEngineUnity.HTC_SessionManager instance_;  // static @ 0x0

        // Properties
        HoudiniEngineUnity.HTC_SessionManager Instance { get; /* RVA: 0x09D83D5C */ }

        // Methods
        // RVA: 0x09D83500  token: 0x60004FC
        private System.Void .ctor() { }
        // RVA: 0x09D833CC  token: 0x60004FD
        protected virtual System.Void Finalize() { }
        // RVA: 0x09D832D8  token: 0x60004FE
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x09D83288  token: 0x60004FF
        public virtual System.Void Dispose() { }
        // RVA: 0x09D83440  token: 0x6000500
        public System.Boolean GetOrRequestAvailableHARSInfo(System.String& harsAddress, System.Int32& harsPort, System.String& errorInfo) { }
        // RVA: 0x09D83220  token: 0x6000501
        public static System.Void Close() { }

    }

    // TypeToken: 0x20000C1  // size: 0x10
    public static class HTC_Utils
    {
        // Fields
        private static readonly System.String CurrentUnityProjectPrefix;  // static @ 0x0

        // Methods
        // RVA: 0x09D83DE0  token: 0x6000503
        public static System.String GetUnityPathFromSystemPath(System.String systemPath) { }
        // RVA: 0x09D83FE8  token: 0x6000504
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C2  // size: 0x10
    public static class HoudiniExtTools
    {
        // Fields
        private static readonly System.String LOCAL_IP_ADDRESS;  // static @ 0x0
        private static readonly System.String REMOTE_HOUDINI_SMB_PATH_PREFIX;  // static @ 0x8
        private static readonly System.String REMOTE_HOUDINI_DEBUG_SMB_PATH_PREFIX;  // static @ 0x10
        public static System.String REMOTE_HOUDINI_HDA_SMB_URL;  // const
        public static System.String REMOTE_HOUDINI_SMB_URL;  // const
        public static System.String LINUX_MOUNT_DIR_PREFIX;  // const
        public static System.String PROJECT_TAG;  // const

        // Properties
        System.Int64 DefaultSessionID { get; /* RVA: 0x09D84F8C */ }

        // Methods
        // RVA: 0x09D84BCC  token: 0x6000505
        private static System.Int64 GetSessionID(System.Int64 sessionId) { }
        // RVA: 0x09D84694  token: 0x6000506
        public static System.String GetDefaultDirectoryOnTheSMB() { }
        // RVA: 0x09D84710  token: 0x6000507
        public static System.String GetDefaultSessionDirectoryOnTheSMB(System.Int64 sessionId) { }
        // RVA: 0x09D84318  token: 0x6000508
        public static System.String ConvertLocalDirPathToSessionDirPath(System.String localPath, System.Boolean doMd5, System.Int64 sessionId) { }
        // RVA: 0x09D84220  token: 0x6000509
        public static System.String ConvertLocalDebugFilePathToSessionDebugFilePath(System.String localFilePath, System.Int64 sessionId) { }
        // RVA: 0x09D84448  token: 0x600050A
        public static System.String ConvertLocalFilePathToSessionFilePath(System.String localFilePath, System.Int64 sessionId) { }
        // RVA: 0x09D84C18  token: 0x600050B
        public static System.Boolean IsLocalPath(System.String path) { }
        // RVA: 0x09D84AC8  token: 0x600050C
        public static System.String GetMD5WithString(System.String str) { }
        // RVA: 0x09D847DC  token: 0x600050D
        public static System.String GetLocalIPv4() { }
        // RVA: 0x09D847B0  token: 0x600050E
        public static System.String GetHoudiniSMBUrl() { }
        // RVA: 0x09D84634  token: 0x600050F
        public static System.String GetDefaultCachePath(System.Int64 sessionId) { }
        // RVA: 0x09D846E4  token: 0x6000510
        public static System.String GetDefaultHdaPath() { }
        // RVA: 0x09D84E90  token: 0x6000512
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C3  // size: 0x10
    public static class HoudiniExtBridge
    {
        // Methods
        // RVA: 0x09D840D4  token: 0x6000513
        public static System.Boolean GetParametersFromHDA(System.Byte[] hdaBytes, HoudiniEngineUnity.HEU_Parameters& parameters, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode>& inputNodes) { }
        // RVA: 0x09D84094  token: 0x6000514
        public static System.Void AddInputNodeToHoudiniAsset(HoudiniEngineUnity.HEU_HoudiniAsset asset, HoudiniEngineUnity.HEU_InputNode node) { }
        // RVA: 0x09D840B0  token: 0x6000515
        public static System.Void Clear(HoudiniEngineUnity.HEU_Parameters parameters) { }

    }

    // TypeToken: 0x20000C4  // size: 0x150
    public sealed class HEU_ParameterData : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.Int32 _unityIndex;  // 0x10
        public System.String _name;  // 0x18
        public System.String _labelName;  // 0x20
        public System.String _help;  // 0x28
        public System.Collections.Generic.List<System.Int32> _childParameterIDs;  // 0x30
        public System.Int32 _choiceValue;  // 0x38
        public System.Int32[] _intValues;  // 0x40
        public System.Single[] _floatValues;  // 0x48
        public System.String[] _stringValues;  // 0x50
        public System.Boolean _toggle;  // 0x58
        public UnityEngine.Color _color;  // 0x5c
        public UnityEngine.Gradient _gradient;  // 0x70
        public UnityEngine.AnimationCurve _animCurve;  // 0x78
        public UnityEngine.GUIContent[] _choiceLabels;  // 0x80
        public System.String[] _choiceStringValues;  // 0x88
        public System.Int32[] _choiceIntValues;  // 0x90
        public HoudiniEngineUnity.HAPI_ParmInfo _parmInfo;  // 0x98
        public System.Boolean _showChildren;  // 0x12c
        public System.String _fileTypeInfo;  // 0x130
        public System.Int32 _folderListChildrenProcessed;  // 0x138
        public System.Int32 _tabSelectedIndex;  // 0x13c
        public HoudiniEngineUnity.HEU_InputNode _paramInputNode;  // 0x140
        public System.Boolean _hasAssetPathTag;  // 0x148

        // Properties
        System.Int32 ParmID { get; /* RVA: 0x04D862D0 */ }
        System.Int32 ParentID { get; /* RVA: 0x04D86C60 */ }
        System.Int32 ChildIndex { get; /* RVA: 0x04D88CD0 */ }
        System.Int32 ParmSize { get; /* RVA: 0x04D8E580 */ }
        System.Int32 IntMin { get; /* RVA: 0x09D70374 */ }
        System.Int32 IntMax { get; /* RVA: 0x09D70364 */ }
        System.Int32 IntUIMin { get; /* RVA: 0x09D70394 */ }
        System.Int32 IntUIMax { get; /* RVA: 0x09D70384 */ }
        System.Single FloatMin { get; /* RVA: 0x04D9DFB0 */ }
        System.Single FloatMax { get; /* RVA: 0x04DA29C0 */ }
        System.Single FloatUIMin { get; /* RVA: 0x04D8E2D0 */ }
        System.Single FloatUIMax { get; /* RVA: 0x04DA29D0 */ }

        // Methods
        // RVA: 0x09D7020C  token: 0x600051A
        public System.Boolean IsInt() { }
        // RVA: 0x09D701F4  token: 0x600051B
        public System.Boolean IsFloat() { }
        // RVA: 0x09D70254  token: 0x600051C
        public System.Boolean IsString() { }
        // RVA: 0x09D70224  token: 0x600051D
        public System.Boolean IsPathFile() { }
        // RVA: 0x04DA2990  token: 0x600051E
        public System.Boolean HasMin() { }
        // RVA: 0x04DA2980  token: 0x600051F
        public System.Boolean HasMax() { }
        // RVA: 0x04DA29B0  token: 0x6000520
        public System.Boolean HasUIMin() { }
        // RVA: 0x04DA29A0  token: 0x6000521
        public System.Boolean HasUIMax() { }
        // RVA: 0x09D6FA6C  token: 0x600052A
        public System.Boolean IsContainer() { }
        // RVA: 0x09D70218  token: 0x600052B
        public System.Boolean IsMultiParam() { }
        // RVA: 0x09D7023C  token: 0x600052C
        public System.Boolean IsRamp() { }
        // RVA: 0x09D701E8  token: 0x600052D
        public System.Boolean IsFloatRamp() { }
        // RVA: 0x09D6FA54  token: 0x600052E
        public System.Boolean IsColorRamp() { }
        // RVA: 0x09D70260  token: 0x600052F
        public System.Boolean IsToggle() { }
        // RVA: 0x09D6FA60  token: 0x6000530
        public System.Boolean IsColor() { }
        // RVA: 0x09D70200  token: 0x6000531
        public System.Boolean IsFolder() { }
        // RVA: 0x09D6FA48  token: 0x6000532
        public System.Boolean IsButton() { }
        // RVA: 0x09D7026C  token: 0x6000533
        public UnityEngine.Vector3 ToVector3() { }
        // RVA: 0x04D8FE60  token: 0x6000534
        public System.Boolean IsAssetPath() { }
        // RVA: 0x09D6FAB0  token: 0x6000535
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ParameterData other) { }
        // RVA: 0x09D702EC  token: 0x6000536
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C5  // size: 0x14
    public sealed struct HEU_ModifierActionWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_ModifierActionWrapper MULTIPARM_INSERT;  // const
        public static HoudiniEngineUnity.HEU_ModifierActionWrapper MULTIPARM_REMOVE;  // const
        public static HoudiniEngineUnity.HEU_ModifierActionWrapper MULTIPARM_CLEAR;  // const
        public static HoudiniEngineUnity.HEU_ModifierActionWrapper SET_FLOAT;  // const
        public static HoudiniEngineUnity.HEU_ModifierActionWrapper SET_INT;  // const

    }

    // TypeToken: 0x20000C6  // size: 0x28
    public class HEU_ParameterModifier : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.Int32 _parameterIndex;  // 0x10
        private HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction _action;  // 0x14
        private System.Int32 _instanceIndex;  // 0x18
        private System.Int32 _modifierValue;  // 0x1c
        private System.Single _floatValue;  // 0x20
        private System.Int32 _intValue;  // 0x24

        // Properties
        System.Int32 ParameterIndex { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        HoudiniEngineUnity.HEU_ModifierActionWrapper Action { get; /* RVA: 0x09D7067C */ set; /* RVA: 0x09D70688 */ }
        System.Int32 InstanceIndex { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Int32 ModifierValue { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }
        System.Single FloatValue { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }
        System.Int32 IntValue { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }

        // Methods
        // RVA: 0x09D703A4  token: 0x6000543
        public static HoudiniEngineUnity.HEU_ParameterModifier GetNewModifier(HoudiniEngineUnity.HEU_ModifierActionWrapper action, System.Int32 parameterIndex, System.Int32 instanceIndex, System.Int32 modifierValue) { }
        // RVA: 0x09D703EC  token: 0x6000544
        private static HoudiniEngineUnity.HEU_ParameterModifier GetNewModifier(HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction action, System.Int32 parameterIndex, System.Int32 instanceIndex, System.Int32 modifierValue) { }
        // RVA: 0x09D70460  token: 0x6000545
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ParameterModifier other) { }
        // RVA: 0x09D70648  token: 0x6000546
        private static HoudiniEngineUnity.HEU_ModifierActionWrapper ModifierAction_InternalToWrapper(HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction action) { }
        // RVA: 0x09D70648  token: 0x6000547
        private static HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction ModifierAction_WrapperToInternal(HoudiniEngineUnity.HEU_ModifierActionWrapper action) { }
        // RVA: 0x041E1670  token: 0x6000548
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C8  // size: 0x88
    public class HEU_Parameters : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_Parameters, HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private System.Int32 _nodeID;  // 0x18
        private System.String _uiLabel;  // 0x20
        private System.Int32[] _paramInts;  // 0x28
        private System.Single[] _paramFloats;  // 0x30
        private System.String[] _paramStrings;  // 0x38
        private HoudiniEngineUnity.HAPI_ParmChoiceInfo[] _paramChoices;  // 0x40
        private System.Collections.Generic.List<System.Int32> _rootParameters;  // 0x48
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterData> _parameterList;  // 0x50
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterModifier> _parameterModifiers;  // 0x58
        private System.Boolean _regenerateParameters;  // 0x60
        private System.Byte[] _presetData;  // 0x68
        private System.Byte[] _defaultPresetData;  // 0x70
        private System.Boolean _validParameters;  // 0x78
        private System.Boolean _showParameters;  // 0x79
        private System.Boolean _recacheUI;  // 0x7a
        private HoudiniEngineUnity.HEU_HoudiniAsset _parentAsset;  // 0x80

        // Properties
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x04D86260 */ }
        System.Boolean ShowParameters { get; /* RVA: 0x04D88E40 */ set; /* RVA: 0x04D88E90 */ }
        System.Int32 NodeID { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Collections.Generic.List<System.Int32> RootParameters { get; /* RVA: 0x04D86200 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterModifier> ParameterModifiers { get; /* RVA: 0x02E56440 */ }
        System.Boolean RequiresRegeneration { get; /* RVA: 0x04D88340 */ set; /* RVA: 0x04D88350 */ }
        System.Boolean RecacheUI { get; /* RVA: 0x04D88E50 */ set; /* RVA: 0x04D88EA0 */ }

        // Methods
        // RVA: 0x04D86230  token: 0x6000552
        private System.Byte[] GetPresetData() { }
        // RVA: 0x0485F170  token: 0x6000553
        private System.Void SetPresetData(System.Byte[] data) { }
        // RVA: 0x04D862B0  token: 0x6000554
        private System.Byte[] GetDefaultPresetData() { }
        // RVA: 0x04D86F90  token: 0x6000557
        public virtual System.Boolean AreParametersValid() { }
        // RVA: 0x09D73114  token: 0x6000558
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: 0x09D78444  token: 0x6000559
        public virtual System.Void Recook() { }
        // RVA: 0x04D86270  token: 0x600055A
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterData> GetParameters() { }
        // RVA: 0x09D72E4C  token: 0x600055B
        public virtual HoudiniEngineUnity.HEU_ParameterData GetParameter(System.Int32 listIndex) { }
        // RVA: 0x09D72EBC  token: 0x600055C
        public virtual HoudiniEngineUnity.HEU_ParameterData GetParameter(System.String name) { }
        // RVA: 0x09D72D2C  token: 0x600055D
        public virtual HoudiniEngineUnity.HEU_ParameterData GetParameterWithParmID(System.Int32 parmID) { }
        // RVA: 0x09D78534  token: 0x600055E
        public virtual System.Void RemoveParameter(System.Int32 listIndex) { }
        // RVA: 0x09D73590  token: 0x600055F
        public virtual System.Boolean HaveParametersChanged() { }
        // RVA: 0x09D7874C  token: 0x6000560
        public virtual System.Boolean ResetAllToDefault(System.Boolean bRecookAsset) { }
        // RVA: 0x09D793E0  token: 0x6000561
        public virtual System.Boolean SetFloatParameterValue(System.String parameterName, System.Single value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: 0x09D71888  token: 0x6000562
        public virtual System.Boolean GetFloatParameterValue(System.String parameterName, System.Single& value, System.Int32 atIndex) { }
        // RVA: 0x09D795E8  token: 0x6000563
        public virtual System.Boolean SetFloatParameterValues(System.String parameterName, System.Single[] values, System.Boolean bRecookAsset) { }
        // RVA: 0x09D71A7C  token: 0x6000564
        public virtual System.Boolean GetFloatParameterValues(System.String parameterName, System.Single[]& values) { }
        // RVA: 0x09D78E24  token: 0x6000565
        public virtual System.Boolean SetColorParameterValue(System.String parameterName, UnityEngine.Color value, System.Boolean bRecookAsset) { }
        // RVA: 0x09D71244  token: 0x6000566
        public virtual System.Boolean GetColorParameterValue(System.String parameterName, UnityEngine.Color& value) { }
        // RVA: 0x09D79CDC  token: 0x6000567
        public virtual System.Boolean SetIntParameterValue(System.String parameterName, System.Int32 value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: 0x09D7241C  token: 0x6000568
        public virtual System.Boolean GetIntParameterValue(System.String parameterName, System.Int32& value, System.Int32 atIndex) { }
        // RVA: 0x09D79EE4  token: 0x6000569
        public virtual System.Boolean SetIntParameterValues(System.String parameterName, System.Int32[] values, System.Boolean bRecookAsset) { }
        // RVA: 0x09D72610  token: 0x600056A
        public virtual System.Boolean GetIntParameterValues(System.String parameterName, System.Int32[]& values) { }
        // RVA: 0x09D78C54  token: 0x600056B
        public virtual System.Boolean SetChoiceParameterValue(System.String parameterName, System.Int32 value, System.Boolean bRecookAsset) { }
        // RVA: 0x09D7109C  token: 0x600056C
        public virtual System.Boolean GetChoiceParameterValue(System.String parameterName, System.Int32& value) { }
        // RVA: 0x09D78B38  token: 0x600056D
        public virtual System.Boolean SetBoolParameterValue(System.String parameterName, System.Boolean value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: 0x09D70F90  token: 0x600056E
        public virtual System.Boolean GetBoolParameterValue(System.String parameterName, System.Boolean& value, System.Int32 atIndex) { }
        // RVA: 0x09D7A7A8  token: 0x600056F
        public virtual System.Boolean SetStringParameterValue(System.String parameterName, System.String value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: 0x09D731AC  token: 0x6000570
        public virtual System.Boolean GetStringParameterValue(System.String parameterName, System.String& value, System.Int32 atIndex) { }
        // RVA: 0x09D7A9B0  token: 0x6000571
        public virtual System.Boolean SetStringParameterValues(System.String parameterName, System.String[] values, System.Boolean bRecookAsset) { }
        // RVA: 0x09D733C8  token: 0x6000572
        public virtual System.Boolean GetStringParameterValues(System.String parameterName, System.String[]& values) { }
        // RVA: 0x09D78794  token: 0x6000573
        public virtual System.Boolean SetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: 0x09D7895C  token: 0x6000574
        public virtual System.Boolean SetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[] values, System.Boolean bRecookAsset) { }
        // RVA: 0x09D70C4C  token: 0x6000575
        public virtual System.Boolean GetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject& value, System.Int32 atIndex) { }
        // RVA: 0x09D70DC4  token: 0x6000576
        public virtual System.Boolean GetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[]& values) { }
        // RVA: 0x09D7A534  token: 0x6000577
        public virtual System.Boolean SetRampParameterNumPoints(System.String parameterName, System.Int32 numPoints, System.Boolean bRecookAsset) { }
        // RVA: 0x09D72FFC  token: 0x6000578
        public virtual System.Boolean GetRampParameterNumPoints(System.String parameterName, System.Int32& numPoints) { }
        // RVA: 0x09D79844  token: 0x6000579
        public virtual System.Boolean SetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, System.Single pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolationType, System.Boolean bRecookAsset) { }
        // RVA: 0x09D71C14  token: 0x600057A
        public virtual System.Boolean GetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, System.Single& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType) { }
        // RVA: 0x09D79B3C  token: 0x600057B
        public virtual System.Boolean SetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[] rampPoints, System.Boolean bRecookAsset) { }
        // RVA: 0x09D71F08  token: 0x600057C
        public virtual System.Boolean GetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[]& rampPoints) { }
        // RVA: 0x09D78F50  token: 0x600057D
        public virtual System.Boolean SetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, UnityEngine.Color pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolationType, System.Boolean bRecookAsset) { }
        // RVA: 0x09D71364  token: 0x600057E
        public virtual System.Boolean GetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, UnityEngine.Color& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType) { }
        // RVA: 0x09D79234  token: 0x600057F
        public virtual System.Boolean SetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[] rampPoints, System.Boolean bRecookAsset) { }
        // RVA: 0x09D71650  token: 0x6000580
        public virtual System.Boolean GetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[]& rampPoints) { }
        // RVA: 0x09D7B134  token: 0x6000581
        public virtual System.Boolean TriggerButtonParameter(System.String parameterName) { }
        // RVA: 0x09D7A148  token: 0x6000582
        public virtual System.Boolean SetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper> parameterTuples, System.Boolean bRecook) { }
        // RVA: 0x09D72948  token: 0x6000583
        public virtual System.Boolean GetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper>& parameterTuples) { }
        // RVA: 0x09D75B80  token: 0x6000584
        private System.Boolean Initialize(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterData> previousParamFolders, System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_InputNode> previousParamInputNodes, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset) { }
        // RVA: 0x09D73A3C  token: 0x6000585
        private System.Boolean Initialize4HGTool(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, System.Int32 parentNodeId) { }
        // RVA: 0x09D7AC20  token: 0x6000586
        private System.Void SetupRampParameter(HoudiniEngineUnity.HEU_ParameterData rampParameter) { }
        // RVA: 0x09D7B514  token: 0x6000587
        private System.Boolean UploadValuesToHoudini(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bDoCheck, System.Boolean bForceUploadInputs) { }
        // RVA: 0x09D77CF8  token: 0x6000588
        private System.Void InsertInstanceToMultiParm(System.Int32 unityParamIndex, System.Int32 instanceIndex, System.Int32 numInstancesToAdd) { }
        // RVA: 0x09D784AC  token: 0x6000589
        private System.Void RemoveInstancesFromMultiParm(System.Int32 unityParamIndex, System.Int32 instanceIndex, System.Int32 numInstancesToRemove) { }
        // RVA: 0x09D70B44  token: 0x600058A
        private System.Void ClearInstancesFromMultiParm(System.Int32 unityParamIndex) { }
        // RVA: 0x09D7354C  token: 0x600058B
        private System.Boolean HasModifiersPending() { }
        // RVA: 0x09D77FE0  token: 0x600058C
        private System.Void ProcessModifiers(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D727AC  token: 0x600058D
        private System.Void GetParameterDataForUIRestore(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterData> folderParams, System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_InputNode> inputNodeParams) { }
        // RVA: 0x09D7213C  token: 0x600058E
        private System.Void GetInputNodeConnectionObjects(System.Collections.Generic.List<UnityEngine.GameObject> inputNodeObjects) { }
        // RVA: 0x09D70C00  token: 0x600058F
        private System.Void DownloadPresetData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D7B4EC  token: 0x6000590
        private System.Void UploadPresetData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D70BB4  token: 0x6000591
        private System.Void DownloadAsDefaultPresetData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D7B300  token: 0x6000592
        private System.Void UploadParameterInputs(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bForceUpdate) { }
        // RVA: 0x09D7B270  token: 0x6000593
        private System.Void UpdateTransformParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_TransformEuler& HAPITransform) { }
        // RVA: 0x09D7B104  token: 0x6000594
        private System.Void SyncParameterFromHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.String parameterName) { }
        // RVA: 0x09D7B02C  token: 0x6000595
        private System.Void SyncInternalParametersForUndoCompare(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D706A8  token: 0x6000596
        private System.Void CleanUp() { }
        // RVA: 0x09D785A0  token: 0x6000597
        private System.Void ResetAllToDefault(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D77D80  token: 0x6000598
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_Parameters other) { }
        // RVA: 0x09D7B618  token: 0x6000599
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CB  // size: 0x10
    public static class HEU_ParameterUtility
    {
        // Methods
        // RVA: 0x09D90A64  token: 0x600059E
        public static System.Boolean GetToggle(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Boolean& outValue) { }
        // RVA: 0x09D91090  token: 0x600059F
        public static System.Boolean SetToggle(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Boolean setValue) { }
        // RVA: 0x09D90658  token: 0x60005A0
        public static System.Boolean GetInt(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32& outValue) { }
        // RVA: 0x09D90F08  token: 0x60005A1
        public static System.Boolean SetInt(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32 setValue) { }
        // RVA: 0x09D90404  token: 0x60005A2
        public static System.Boolean GetFloat(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single& outValue) { }
        // RVA: 0x09D904C8  token: 0x60005A3
        public static System.Boolean GetFloats(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single[]& outValues) { }
        // RVA: 0x09D90CB4  token: 0x60005A4
        public static System.Boolean SetFloat(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single setValue) { }
        // RVA: 0x09D90D78  token: 0x60005A5
        public static System.Boolean SetFloats(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single[] setValues) { }
        // RVA: 0x09D90998  token: 0x60005A6
        public static System.Boolean GetString(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.String& outValue) { }
        // RVA: 0x09D90FCC  token: 0x60005A7
        public static System.Boolean SetString(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.String setValue) { }
        // RVA: 0x09D90B28  token: 0x60005A8
        public static System.Boolean SetChoice(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32 setValue) { }
        // RVA: 0x09D9027C  token: 0x60005A9
        public static System.Boolean GetChoice(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32& outValue) { }
        // RVA: 0x09D90E38  token: 0x60005AA
        public static System.Boolean SetInputNode(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, UnityEngine.GameObject obj, System.Int32 index) { }
        // RVA: 0x09D9058C  token: 0x60005AB
        public static System.Boolean GetInputNode(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32 index, UnityEngine.GameObject& obj) { }
        // RVA: 0x09D90338  token: 0x60005AC
        public static System.Boolean GetColor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, UnityEngine.Color& getValue) { }
        // RVA: 0x09D90BE8  token: 0x60005AD
        public static System.Boolean SetColor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, UnityEngine.Color setValue) { }
        // RVA: 0x09D908DC  token: 0x60005AE
        public static System.Int32 GetParameterIndexFromName(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName) { }
        // RVA: 0x09D908A8  token: 0x60005AF
        public static System.Int32 GetParameterIndexFromNameOrTag(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName) { }
        // RVA: 0x09D90190  token: 0x60005B0
        public static System.Int32 FindTextureParamByNameOrTag(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName, System.String useTextureParmName) { }
        // RVA: 0x09D907FC  token: 0x60005B1
        public static System.Boolean GetParameterFloatValue(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName, System.Single defaultValue, System.Single& returnValue) { }
        // RVA: 0x09D9071C  token: 0x60005B2
        public static System.Boolean GetParameterColor3Value(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName, UnityEngine.Color defaultValue, UnityEngine.Color& outputColor) { }

    }

    // TypeToken: 0x20000CC  // size: 0x30
    public class HEU_PDGCookedDataEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x09D8F8F0  token: 0x60005B3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CD  // size: 0x20
    public class HEU_PDGCookedEventData
    {
        // Fields
        public System.Boolean CookSuccess;  // 0x10
        public HoudiniEngineUnity.HEU_TOPNodeData TopNodeData;  // 0x18

        // Methods
        // RVA: 0x03219380  token: 0x60005B4
        public System.Void .ctor(System.Boolean bSuccess, HoudiniEngineUnity.HEU_TOPNodeData bTopNodeData) { }

    }

    // TypeToken: 0x20000CE  // size: 0xA8
    public class HEU_PDGAssetLink : UnityEngine.MonoBehaviour, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private HoudiniEngineUnity.HEU_PDGCookedDataEvent _cookedDataEvent;  // 0x18
        private System.String _assetPath;  // 0x20
        private UnityEngine.GameObject _assetGO;  // 0x28
        private System.String _assetName;  // 0x30
        private System.Int32 _assetID;  // 0x38
        private HoudiniEngineUnity.HEU_HoudiniAsset _heu;  // 0x40
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNetworkData> _topNetworks;  // 0x48
        private System.String[] _topNetworkNames;  // 0x50
        private System.Int32 _selectedTOPNetwork;  // 0x58
        private HoudiniEngineUnity.HEU_PDGAssetLink.LinkState _linkState;  // 0x5c
        private System.Boolean _autoCook;  // 0x60
        private System.Boolean _useHEngineData;  // 0x61
        public HoudiniEngineUnity.HEU_PDGAssetLink.UpdateUIDelegate _repaintUIDelegate;  // 0x68
        public HoudiniEngineUnity.HEU_WorkItemTally _workItemTally;  // 0x70
        private UnityEngine.GameObject _loadRootGameObject;  // 0x78
        private System.String _outputCachePathRoot;  // 0x80
        private System.Boolean _bUseTOPNodeFilter;  // 0x88
        private System.Boolean _bUseTOPOutputFilter;  // 0x89
        private System.String _topNodeFilter;  // 0x90
        private System.String _topOutputFilter;  // 0x98
        private System.Int32 _numLoadingResults;  // 0xa0
        private System.Int32 _numTotalResults;  // 0xa4

        // Properties
        System.Boolean AutoCook { get; /* RVA: 0x04D88340 */ set; /* RVA: 0x04D88350 */ }
        System.Boolean UseHEngineData { get; /* RVA: 0x04D892D0 */ set; /* RVA: 0x04D8FF40 */ }
        System.Boolean UseTOPNodeFilter { get; /* RVA: 0x011F2750 */ set; /* RVA: 0x011F2760 */ }
        System.Boolean UseTOPOutputFilter { get; /* RVA: 0x04D89060 */ set; /* RVA: 0x04D90C50 */ }
        System.String TopNodeFilter { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }
        System.String TopOutputFilter { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x04D85A60 */ }
        System.String AssetPath { get; /* RVA: 0x04D862C0 */ }
        UnityEngine.GameObject AssetGO { get; /* RVA: 0x04D86240 */ }
        System.String AssetName { get; /* RVA: 0x011F36E0 */ }
        System.Int32 AssetID { get; /* RVA: 0x04D863B0 */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNetworkData> TopNetworks { get; /* RVA: 0x04D86200 */ }
        System.String[] TopNetworkNames { get; /* RVA: 0x04D86270 */ }
        System.Int32 SelectedTOPNetwork { get; /* RVA: 0x04D86570 */ }
        HoudiniEngineUnity.HEU_LinkStateWrapper PDGLinkState { get; /* RVA: 0x09D8F8C4 */ }
        UnityEngine.GameObject LoadRootGameObject { get; /* RVA: 0x04D862A0 */ }
        System.String OutputCachePathRoot { get; /* RVA: 0x04D86260 */ }
        HoudiniEngineUnity.HEU_PDGCookedDataEvent CookedDataEvent { get; /* RVA: 0x0385B100 */ }
        HoudiniEngineUnity.HEU_PDGAssetLink.LinkState AssetLinkStateInternal { get; /* RVA: 0x04D86560 */ }

        // Methods
        // RVA: 0x09D8F20C  token: 0x60005CE
        public System.Void Setup(HoudiniEngineUnity.HEU_HoudiniAsset hdaAsset) { }
        // RVA: 0x09D8F068  token: 0x60005CF
        public System.Void Reset() { }
        // RVA: 0x09D8ED18  token: 0x60005D0
        public System.Void Refresh() { }
        // RVA: 0x09D8D1F4  token: 0x60005D1
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,HoudiniEngineUnity.HEU_TOPNodeData>> GetNonHiddenTOPNodes(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork) { }
        // RVA: 0x09D8F070  token: 0x60005D2
        public System.Void SelectTOPNetwork(System.Int32 newIndex) { }
        // RVA: 0x09D8F0C4  token: 0x60005D3
        public System.Void SelectTOPNode(HoudiniEngineUnity.HEU_TOPNetworkData network, System.Int32 newIndex) { }
        // RVA: 0x09D8D340  token: 0x60005D4
        public HoudiniEngineUnity.HEU_TOPNetworkData GetSelectedTOPNetwork() { }
        // RVA: 0x09D8D34C  token: 0x60005D5
        public HoudiniEngineUnity.HEU_TOPNodeData GetSelectedTOPNode() { }
        // RVA: 0x09D8D470  token: 0x60005D6
        public HoudiniEngineUnity.HEU_TOPNetworkData GetTOPNetwork(System.Int32 index) { }
        // RVA: 0x09D8CEE8  token: 0x60005D7
        public System.Void DirtyTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8CEE8  token: 0x60005D8
        public System.Void CookTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8D0C0  token: 0x60005D9
        public System.Void DirtyAll() { }
        // RVA: 0x09D8CEA4  token: 0x60005DA
        public System.Void CookOutput() { }
        // RVA: 0x09D8C924  token: 0x60005DB
        public System.Void PauseCook() { }
        // RVA: 0x09D8C924  token: 0x60005DC
        public System.Void CancelCook() { }
        // RVA: 0x09D8D0E0  token: 0x60005DD
        public HoudiniEngineUnity.HEU_SessionBase GetHAPISession() { }
        // RVA: 0x09D8D658  token: 0x60005DE
        public HoudiniEngineUnity.HEU_TOPNodeData GetTOPNode(System.Int32 nodeID) { }
        // RVA: 0x09D8D588  token: 0x60005DF
        public System.String GetTOPNodeStatus(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8D3C8  token: 0x60005E0
        public static HoudiniEngineUnity.HEU_TOPNetworkData GetTOPNetworkByName(System.String name, System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNetworkData> topNetworks) { }
        // RVA: 0x09D8D4E0  token: 0x60005E1
        public static HoudiniEngineUnity.HEU_TOPNodeData GetTOPNodeByName(System.String name, System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNodeData> topNodes) { }
        // RVA: 0x041E1670  token: 0x60005E2
        private System.Void Awake() { }
        // RVA: 0x041E1670  token: 0x60005E3
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: 0x041E1670  token: 0x60005E4
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: 0x041E1670  token: 0x60005E5
        private System.Void HandleInitialLoad() { }
        // RVA: 0x09D8E0DC  token: 0x60005E6
        private System.Void OnDestroy() { }
        // RVA: 0x09D8E0A4  token: 0x60005E7
        private System.Void NotifyAssetCooked(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean bSuccess, System.Collections.Generic.List<UnityEngine.GameObject> generatedOutputs) { }
        // RVA: 0x09D8E07C  token: 0x60005E8
        private System.Void NotifyAssetCooked(HoudiniEngineUnity.HEU_CookedEventData cookedEventData) { }
        // RVA: 0x09D8E07C  token: 0x60005E9
        private System.Void NotifyAssetCooked(HoudiniEngineUnity.HEU_ReloadEventData reloadEventData) { }
        // RVA: 0x09D8E880  token: 0x60005EA
        private System.Void PopulateFromHDA() { }
        // RVA: 0x09D8E9C8  token: 0x60005EB
        public System.Boolean PopulateTOPNetworks() { }
        // RVA: 0x09D8EBA0  token: 0x60005EC
        private System.Boolean PopulateTOPNodes(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_TOPNetworkData topNetwork, System.Int32[] topNodeIDs, System.Boolean useHEngineData) { }
        // RVA: 0x09D8C934  token: 0x60005ED
        private System.Void ClearAllTOPData() { }
        // RVA: 0x09D8CC00  token: 0x60005EE
        private static System.Void ClearTOPNetworkWorkItemResults(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork) { }
        // RVA: 0x09D8CD14  token: 0x60005EF
        private static System.Void ClearTOPNodeWorkItemResults(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8CE10  token: 0x60005F0
        private static System.Void ClearWorkItemResultByID(HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 workItemID) { }
        // RVA: 0x09D8CE34  token: 0x60005F1
        private static System.Void ClearWorkItemResult(HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_TOPWorkResult result) { }
        // RVA: 0x09D8F39C  token: 0x60005F2
        private System.Void UpdateTOPNodeResultsVisibility(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8D7BC  token: 0x60005F3
        private static HoudiniEngineUnity.HEU_TOPWorkResult GetWorkResultByID(HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 workItemID) { }
        // RVA: 0x09D8CF10  token: 0x60005F4
        private static System.Void DestroyWorkItemResultData(HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_TOPWorkResult result) { }
        // RVA: 0x09D8D908  token: 0x60005F5
        private System.Void LoadResults(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HAPI_PDG_WorkItemInfo workItemInfo, HoudiniEngineUnity.HAPI_PDG_WorkItemOutputFile[] resultInfos, System.Int32 workItemID, System.Action<HoudiniEngineUnity.HEU_TOPNodeData,HoudiniEngineUnity.HEU_SyncedEventData> OnSynced) { }
        // RVA: 0x09D8D154  token: 0x60005F6
        private UnityEngine.Transform GetLoadRootTransform() { }
        // RVA: 0x09D8EFA4  token: 0x60005F7
        public System.Void RepaintUI() { }
        // RVA: 0x09D8F410  token: 0x60005F8
        public System.Void UpdateWorkItemTally() { }
        // RVA: 0x09D8EFBC  token: 0x60005F9
        private System.Void ResetTOPNetworkWorkItemTally(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork) { }
        // RVA: 0x09D8E144  token: 0x60005FA
        private System.Void OnTOPNodeFilterChanged(System.String filter) { }
        // RVA: 0x09D8E4C0  token: 0x60005FB
        private System.Void OnTOPOutputFilterChanged(System.String filter) { }
        // RVA: 0x09D8E820  token: 0x60005FC
        private static System.Void ParseHEngineData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 topNodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, HoudiniEngineUnity.TOPNodeTags& nodeTags) { }
        // RVA: 0x09D8F11C  token: 0x60005FD
        private System.Void SetupTopNetworkNames(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork) { }
        // RVA: 0x09D8E0EC  token: 0x60005FE
        private System.Void OnSyncComplete(HoudiniEngineUnity.HEU_PDGCookedEventData Data) { }
        // RVA: 0x09D43A04  token: 0x60005FF
        private static HoudiniEngineUnity.HEU_LinkStateWrapper LinkState_InternalToWrapper(HoudiniEngineUnity.HEU_PDGAssetLink.LinkState linkState) { }
        // RVA: 0x09D43A04  token: 0x6000600
        private static HoudiniEngineUnity.HEU_PDGAssetLink.LinkState LinkState_WrapperToInternal(HoudiniEngineUnity.HEU_LinkStateWrapper linkState) { }
        // RVA: 0x09D8F7B4  token: 0x6000601
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D3  // size: 0x18
    public class TOPNodeTags
    {
        // Fields
        public System.Boolean _show;  // 0x10
        public System.Boolean _autoload;  // 0x11
        public System.Boolean _showHEngineData;  // 0x12
        public System.Boolean _autoloadHEngineData;  // 0x13

        // Methods
        // RVA: 0x041E1670  token: 0x6000609
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D4  // size: 0x48
    public class HEU_TOPNetworkData
    {
        // Fields
        public System.Int32 _nodeID;  // 0x10
        public System.String _nodeName;  // 0x18
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNodeData> _topNodes;  // 0x20
        public System.String[] _topNodeNames;  // 0x28
        public System.Int32 _selectedTOPIndex;  // 0x30
        public System.String _parentName;  // 0x38
        public HoudiniEngineUnity.TOPNodeTags _tags;  // 0x40

        // Methods
        // RVA: 0x09D93B54  token: 0x600060A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x50
    public class HEU_TOPNodeData
    {
        // Fields
        public System.Int32 _nodeID;  // 0x10
        public System.String _nodeName;  // 0x18
        public System.String _parentName;  // 0x20
        public UnityEngine.GameObject _workResultParentGO;  // 0x28
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPWorkResult> _workResults;  // 0x30
        public HoudiniEngineUnity.TOPNodeTags _tags;  // 0x38
        public System.Boolean _showResults;  // 0x40
        public HoudiniEngineUnity.HEU_TOPNodeData.PDGState _pdgState;  // 0x44
        public HoudiniEngineUnity.HEU_WorkItemTally _workItemTally;  // 0x48

        // Methods
        // RVA: 0x09D93C7C  token: 0x600060B
        public System.Void Reset() { }
        // RVA: 0x09D93C5C  token: 0x600060C
        public System.Boolean AreAllWorkItemsComplete() { }
        // RVA: 0x09D93C1C  token: 0x600060D
        public System.Boolean AnyWorkItemsFailed() { }
        // RVA: 0x09D93C3C  token: 0x600060E
        public System.Boolean AnyWorkItemsPending() { }
        // RVA: 0x09D93CA8  token: 0x600060F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D7  // size: 0x20
    public class HEU_TOPWorkResult
    {
        // Fields
        public System.Int32 _workItemIndex;  // 0x10
        public System.Int32 _workItemID;  // 0x14
        public System.Collections.Generic.List<UnityEngine.GameObject> _generatedGOs;  // 0x18

        // Methods
        // RVA: 0x09D93D78  token: 0x6000610
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D8  // size: 0x28
    public class HEU_WorkItemTally
    {
        // Fields
        public System.Int32 _totalWorkItems;  // 0x10
        public System.Int32 _waitingWorkItems;  // 0x14
        public System.Int32 _scheduledWorkItems;  // 0x18
        public System.Int32 _cookingWorkItems;  // 0x1c
        public System.Int32 _cookedWorkItems;  // 0x20
        public System.Int32 _erroredWorkItems;  // 0x24

        // Methods
        // RVA: 0x04DA2BD0  token: 0x6000611
        public System.Void ZeroAll() { }
        // RVA: 0x09D944AC  token: 0x6000612
        public System.Boolean AreAllWorkItemsComplete() { }
        // RVA: 0x09D94488  token: 0x6000613
        public System.Boolean AnyWorkItemsFailed() { }
        // RVA: 0x09D94490  token: 0x6000614
        public System.Boolean AnyWorkItemsPending() { }
        // RVA: 0x09D944CC  token: 0x6000615
        public System.String ProgressRatio() { }
        // RVA: 0x041E1670  token: 0x6000616
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D9  // size: 0x70
    public class HEU_PDGSession
    {
        // Fields
        private static HoudiniEngineUnity.HEU_PDGSession _pdgSession;  // static @ 0x0
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_PDGAssetLink> _pdgAssets;  // 0x10
        public System.Int32 _pdgMaxProcessEvents;  // 0x18
        public HoudiniEngineUnity.HAPI_PDG_EventInfo[] _pdgQueryEvents;  // 0x20
        public System.Int32[] _pdgContextIDs;  // 0x28
        public System.Boolean _errored;  // 0x30
        public System.String _errorMsg;  // 0x38
        public HoudiniEngineUnity.HAPI_PDG_State _pdgState;  // 0x40
        private System.Action<HoudiniEngineUnity.HEU_PDGCookedEventData> _cookedDataEvent;  // 0x48
        private System.Boolean _pendingCallback;  // 0x50
        private System.Int32 _numItemsCompleted;  // 0x54
        private System.Int32 _totalNumItems;  // 0x58
        private System.Boolean _callbackSuccess;  // 0x5c
        private System.Text.StringBuilder _pdgEventMessages;  // 0x60
        private System.String[] _eventMessageColorCode;  // 0x68

        // Properties
        System.Action<HoudiniEngineUnity.HEU_PDGCookedEventData> CookedDataEvent { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x09D8FC38  token: 0x6000617
        public static HoudiniEngineUnity.HEU_PDGSession GetPDGSession() { }
        // RVA: 0x09D90040  token: 0x6000618
        public System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x6000619
        public System.Void AddAsset(HoudiniEngineUnity.HEU_PDGAssetLink asset) { }
        // RVA: 0x041E1670  token: 0x600061A
        public System.Void RemoveAsset(HoudiniEngineUnity.HEU_PDGAssetLink asset) { }
        // RVA: 0x041E1670  token: 0x600061B
        private System.Void Update() { }
        // RVA: 0x09D8FA00  token: 0x600061C
        private System.Void CleanUp() { }
        // RVA: 0x041E1670  token: 0x600061D
        private System.Void UpdatePDGContext() { }
        // RVA: 0x041E1670  token: 0x600061E
        public System.Void ReinitializePDGContext() { }
        // RVA: 0x041E1670  token: 0x600061F
        private System.Void ProcessPDGEvent(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 contextID, HoudiniEngineUnity.HAPI_PDG_EventInfo& eventInfo) { }
        // RVA: 0x09D8FF90  token: 0x6000620
        private System.Void OnWorkItemLoadResults(HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_SyncedEventData OnSynced) { }
        // RVA: 0x09D8F94C  token: 0x6000621
        private System.Void CheckCallback(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8FCC8  token: 0x6000622
        private System.Boolean GetTOPAssetLinkAndNode(System.Int32 nodeID, HoudiniEngineUnity.HEU_PDGAssetLink& assetLink, HoudiniEngineUnity.HEU_TOPNodeData& topNode) { }
        // RVA: 0x09D8FFE4  token: 0x6000623
        private System.Void SetTOPNodePDGState(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_TOPNodeData.PDGState pdgState) { }
        // RVA: 0x09D8FE90  token: 0x6000624
        private System.Void NotifyTOPNodePDGStateClear(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8FF10  token: 0x6000625
        private System.Void NotifyTOPNodeTotalWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc) { }
        // RVA: 0x09D8FDE8  token: 0x6000626
        private System.Void NotifyTOPNodeCookedWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8FE5C  token: 0x6000627
        private System.Void NotifyTOPNodeErrorWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x09D8FF50  token: 0x6000628
        private System.Void NotifyTOPNodeWaitingWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc) { }
        // RVA: 0x09D8FED0  token: 0x6000629
        private System.Void NotifyTOPNodeScheduledWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc) { }
        // RVA: 0x09D8FE1C  token: 0x600062A
        private System.Void NotifyTOPNodeCookingWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc) { }
        // RVA: 0x04DA2A40  token: 0x600062B
        private static System.Void ResetPDGEventInfo(HoudiniEngineUnity.HAPI_PDG_EventInfo& eventInfo) { }
        // RVA: 0x09D8FF9C  token: 0x600062C
        private System.Void SetErrorState(System.String msg, System.Boolean bLogIt) { }
        // RVA: 0x09D8FAC8  token: 0x600062D
        private System.Void ClearErrorState() { }
        // RVA: 0x09D8FB54  token: 0x600062E
        public HoudiniEngineUnity.HEU_SessionBase GetHAPIPDGSession(System.Boolean bCreate) { }
        // RVA: 0x041E1670  token: 0x600062F
        public System.Void CookTOPNetworkOutputNode(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork, System.Action<HoudiniEngineUnity.HEU_PDGCookedEventData> OnCook) { }
        // RVA: 0x041E1670  token: 0x6000630
        public System.Void PauseCook(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork) { }
        // RVA: 0x041E1670  token: 0x6000631
        public System.Void CancelCook(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork) { }
        // RVA: 0x041E1670  token: 0x6000632
        public System.Void ClearWorkItemResult(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 contextID, HoudiniEngineUnity.HAPI_PDG_EventInfo eventInfo, HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: 0x012081B0  token: 0x6000633
        public System.Boolean DirtyTOPNode(System.Int32 nodeID) { }
        // RVA: 0x012081B0  token: 0x6000634
        public System.Boolean CookTOPNode(System.Int32 nodeID) { }
        // RVA: 0x012081B0  token: 0x6000635
        public System.Boolean DirtyAll(System.Int32 nodeID) { }
        // RVA: 0x09D8F92C  token: 0x6000636
        public System.Void AddEventMessage(System.String msg) { }
        // RVA: 0x09D8FB30  token: 0x6000637
        public System.String GetEventMessages() { }
        // RVA: 0x09D8FB0C  token: 0x6000638
        public System.Void ClearEventMessages() { }
        // RVA: 0x09D8FBA4  token: 0x6000639
        public static System.Int32[] GetNonBypassedNetworkIds(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetId) { }
        // RVA: 0x09D8FDCC  token: 0x600063A
        public static System.Boolean IsPDGAsset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetId) { }
        // RVA: 0x04DA2A30  token: 0x600063B
        private System.Void ResetCallbackVariables() { }

    }

    // TypeToken: 0x20000DD
    public interface IHEU_Curve
    {
        // Properties
        UnityEngine.GameObject TargetGameObject { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Int32 GeoID { get; /* RVA: -1  // abstract */ }
        System.Int32 PartID { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> CurveNodeData { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_Parameters Parameters { get; /* RVA: -1  // abstract */ }
        System.String CurveName { get; /* RVA: -1  // abstract */ }
        System.Boolean IsInputCurve { get; /* RVA: -1  // abstract */ }
        System.Boolean IsPartCurve { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputCurveInfo InputCurveInfo { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600064E
        public virtual System.Boolean IsEditable() { }
        // RVA: -1  // abstract  token: 0x600064F
        public virtual System.Boolean IsGeoCurve() { }
        // RVA: -1  // abstract  token: 0x6000650
        public virtual System.Void SetCurveName(System.String name) { }
        // RVA: -1  // abstract  token: 0x6000651
        public virtual System.Void SetCurvePoint(System.Int32 pointIndex, UnityEngine.Vector3 newPosition, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000652
        public virtual System.Void SetCurvePoint(System.Int32 pointIndex, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000653
        public virtual System.Void SetCurveNodeData(System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> curveNodeData, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000654
        public virtual UnityEngine.Vector3 GetCurvePoint(System.Int32 pointIndex) { }
        // RVA: -1  // abstract  token: 0x6000655
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> GetAllPointTransforms() { }
        // RVA: -1  // abstract  token: 0x6000656
        public virtual System.Collections.Generic.List<UnityEngine.Vector3> GetAllPoints() { }
        // RVA: -1  // abstract  token: 0x6000657
        public virtual System.Int32 GetNumPoints() { }
        // RVA: -1  // abstract  token: 0x6000658
        public virtual System.Void InsertCurvePoint(System.Int32 index, UnityEngine.Vector3 position, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000659
        public virtual System.Void InsertCurvePoint(System.Int32 index, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600065A
        public virtual System.Void AddCurvePointToEnd(UnityEngine.Vector3 position, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600065B
        public virtual System.Void AddCurvePointToEnd(HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600065C
        public virtual System.Void RemoveCurvePoint(System.Int32 pointIndex, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600065D
        public virtual System.Void ClearCurveNodeData(System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600065E
        public virtual System.Void ProjectToColliders(UnityEngine.Vector3 rayDirection, System.Single rayDistance, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600065F
        public virtual System.Void SetCurveGeometryVisibility(System.Boolean bVisible, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000660
        public virtual UnityEngine.Vector3 GetTransformedPoint(System.Int32 pointIndex) { }
        // RVA: -1  // abstract  token: 0x6000661
        public virtual System.Collections.Generic.List<UnityEngine.Vector3> GetTransformedPoints() { }
        // RVA: -1  // abstract  token: 0x6000662
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> DuplicateCurveNodeData() { }

    }

    // TypeToken: 0x20000DE
    public interface IHEU_GeoNode
    {
        // Properties
        System.Int32 GeoID { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HAPI_GeoInfo GeoInfo { get; /* RVA: -1  // abstract */ }
        System.String GeoName { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HAPI_GeoType GeoType { get; /* RVA: -1  // abstract */ }
        System.Boolean Editable { get; /* RVA: -1  // abstract */ }
        System.Boolean Displayable { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> Parts { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_ObjectNode ObjectNode { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputNode InputNode { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_Curve GeoCurve { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> VolumeCaches { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600066E
        public virtual System.Boolean IsVisible() { }
        // RVA: -1  // abstract  token: 0x600066F
        public virtual System.Boolean IsIntermediate() { }
        // RVA: -1  // abstract  token: 0x6000670
        public virtual System.Boolean IsIntermediateOrEditable() { }
        // RVA: -1  // abstract  token: 0x6000671
        public virtual System.Boolean IsGeoInputType() { }
        // RVA: -1  // abstract  token: 0x6000672
        public virtual System.Boolean IsGeoCurveType() { }
        // RVA: -1  // abstract  token: 0x6000673
        public virtual System.Void DestroyAllData(System.Boolean bIsRebuild) { }
        // RVA: -1  // abstract  token: 0x6000674
        public virtual System.Void RemoveAndDestroyPart(HoudiniEngineUnity.HEU_PartData part) { }
        // RVA: -1  // abstract  token: 0x6000675
        public virtual System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: -1  // abstract  token: 0x6000676
        public virtual System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: -1  // abstract  token: 0x6000677
        public virtual HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: -1  // abstract  token: 0x6000678
        public virtual HoudiniEngineUnity.HEU_PartData GetPartFromPartID(System.Int32 partID) { }
        // RVA: -1  // abstract  token: 0x6000679
        public virtual System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly) { }
        // RVA: -1  // abstract  token: 0x600067A
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> GetParts() { }
        // RVA: -1  // abstract  token: 0x600067B
        public virtual System.Void HideAllGeometry() { }
        // RVA: -1  // abstract  token: 0x600067C
        public virtual System.Void DisableAllColliders() { }
        // RVA: -1  // abstract  token: 0x600067D
        public virtual HoudiniEngineUnity.HEU_VolumeCache GetVolumeCacheByTileIndex(System.Int32 tileIndex) { }

    }

    // TypeToken: 0x20000DF  // size: 0x14
    public sealed struct HEU_AssetCookStatusWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AssetCookStatusWrapper NONE;  // const
        public static HoudiniEngineUnity.HEU_AssetCookStatusWrapper COOKING;  // const
        public static HoudiniEngineUnity.HEU_AssetCookStatusWrapper POSTCOOK;  // const
        public static HoudiniEngineUnity.HEU_AssetCookStatusWrapper LOADING;  // const
        public static HoudiniEngineUnity.HEU_AssetCookStatusWrapper POSTLOAD;  // const
        public static HoudiniEngineUnity.HEU_AssetCookStatusWrapper PRELOAD;  // const
        public static HoudiniEngineUnity.HEU_AssetCookStatusWrapper SELECT_SUBASSET;  // const

    }

    // TypeToken: 0x20000E0  // size: 0x14
    public sealed struct HEU_AssetCookResultWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AssetCookResultWrapper NONE;  // const
        public static HoudiniEngineUnity.HEU_AssetCookResultWrapper SUCCESS;  // const
        public static HoudiniEngineUnity.HEU_AssetCookResultWrapper ERRORED;  // const

    }

    // TypeToken: 0x20000E1  // size: 0x14
    public sealed struct HEU_CurveDrawCollisionWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper INVALID;  // const
        public static HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper COLLIDERS;  // const
        public static HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper LAYERMASK;  // const

    }

    // TypeToken: 0x20000E2  // size: 0x14
    public sealed struct HEU_AssetTypeWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_AssetTypeWrapper TYPE_INVALID;  // const
        public static HoudiniEngineUnity.HEU_AssetTypeWrapper TYPE_HDA;  // const
        public static HoudiniEngineUnity.HEU_AssetTypeWrapper TYPE_CURVE;  // const
        public static HoudiniEngineUnity.HEU_AssetTypeWrapper TYPE_INPUT;  // const

    }

    // TypeToken: 0x20000E3
    public interface IHEU_HoudiniAsset
    {
        // Properties
        System.Boolean LoadAssetFromMemory { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean AlwaysOverwriteOnLoad { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean GenerateUVs { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean GenerateTangents { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean GenerateNormals { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean PushTransformToHoudini { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean TransformChangeTriggersCooks { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean CookingTriggersDownCooks { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean AutoCookOnParameterChange { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean IgnoreNonDisplayNodes { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean UseOutputNodes { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean GenerateMeshUsingPoints { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean UseLODGroups { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean SplitGeosByGroup { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean SessionSyncAutoCook { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean BakeUpdateKeepPreviousTransformValues { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean PauseCooking { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean CurveEditorEnabled { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper CurveDrawCollision { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.LayerMask CurveDrawLayerMask { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single CurveProjectMaxDistance { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.Vector3 CurveProjectDirection { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean CurveProjectDirectionToView { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean CurveDisableScaleRotation { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean CurveFrameSelectedNodes { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single CurveFrameSelectedNodeDistance { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean HandlesEnabled { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean EditableNodesToolsEnabled { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_AssetTypeWrapper AssetType { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HAPI_AssetInfo AssetInfo { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HAPI_NodeInfo NodeInfo { get; /* RVA: -1  // abstract */ }
        System.String AssetName { get; /* RVA: -1  // abstract */ }
        System.String AssetOpName { get; /* RVA: -1  // abstract */ }
        System.String AssetHelp { get; /* RVA: -1  // abstract */ }
        System.Int32 AssetID { get; /* RVA: -1  // abstract */ }
        System.String AssetPath { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject OwnerGameObject { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject RootGameObject { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> MaterialCache { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_Parameters Parameters { get; /* RVA: -1  // abstract */ }
        System.String AssetCacheFolder { get; /* RVA: -1  // abstract */ }
        System.String[] SubassetNames { get; /* RVA: -1  // abstract */ }
        System.Int32 SelectedSubassetIndex { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_AssetCookStatusWrapper CookStatus { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_AssetCookResultWrapper LastCookResult { get; /* RVA: -1  // abstract */ }
        System.Int64 SessionID { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> Curves { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> InputNodes { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> VolumeCaches { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_ReloadDataEvent ReloadDataEvent { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_CookedDataEvent CookedDataEvent { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_BakedDataEvent BakedDataEvent { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_PreAssetEvent PreAssetEvent { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60006CF
        public virtual System.Boolean RequestCook(System.Boolean bCheckParametersChanged, System.Boolean bAsync, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters) { }
        // RVA: -1  // abstract  token: 0x60006D0
        public virtual System.Boolean RequestReload(System.Boolean bAsync) { }
        // RVA: -1  // abstract  token: 0x60006D1
        public virtual System.Boolean RequestResetParameters(System.Boolean bAsync) { }
        // RVA: -1  // abstract  token: 0x60006D2
        public virtual UnityEngine.GameObject DuplicateAsset(UnityEngine.GameObject newRootGameObject) { }
        // RVA: -1  // abstract  token: 0x60006D3
        public virtual System.Boolean DeleteAllGeneratedData(System.Boolean bIsRebuild) { }
        // RVA: -1  // abstract  token: 0x60006D4
        public virtual UnityEngine.GameObject BakeToNewPrefab(System.String destinationPrefabPath) { }
        // RVA: -1  // abstract  token: 0x60006D5
        public virtual UnityEngine.GameObject BakeToNewStandalone() { }
        // RVA: -1  // abstract  token: 0x60006D6
        public virtual System.Boolean BakeToExistingPrefab(UnityEngine.GameObject bakeTargetGO) { }
        // RVA: -1  // abstract  token: 0x60006D7
        public virtual System.Boolean BakeToExistingStandalone(UnityEngine.GameObject bakeTargetGO) { }
        // RVA: -1  // abstract  token: 0x60006D8
        public virtual System.Boolean IsAssetValid() { }
        // RVA: -1  // abstract  token: 0x60006D9
        public virtual System.Boolean GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: -1  // abstract  token: 0x60006DA
        public virtual System.Boolean GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: -1  // abstract  token: 0x60006DB
        public virtual HoudiniEngineUnity.HEU_Curve GetCurve(System.String curveName) { }
        // RVA: -1  // abstract  token: 0x60006DC
        public virtual System.Boolean AddCurveDrawCollider(UnityEngine.Collider newCollider) { }
        // RVA: -1  // abstract  token: 0x60006DD
        public virtual System.Boolean RemoveCurveDrawCollider(UnityEngine.Collider collider) { }
        // RVA: -1  // abstract  token: 0x60006DE
        public virtual System.Boolean ClearCurveDrawColliders() { }
        // RVA: -1  // abstract  token: 0x60006DF
        public virtual HoudiniEngineUnity.HEU_InputNode GetInputNode(System.String inputName) { }
        // RVA: -1  // abstract  token: 0x60006E0
        public virtual HoudiniEngineUnity.HEU_InputNode GetAssetInputNode(System.String inputName) { }
        // RVA: -1  // abstract  token: 0x60006E1
        public virtual HoudiniEngineUnity.HEU_InputNode GetInputNodeByIndex(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x60006E2
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> GetNonParameterInputNodes() { }
        // RVA: -1  // abstract  token: 0x60006E3
        public virtual System.Int32 GetVolumeCacheCount() { }
        // RVA: -1  // abstract  token: 0x60006E4
        public virtual HoudiniEngineUnity.HEU_SessionBase GetAssetSession(System.Boolean bCreateIfInvalid) { }
        // RVA: -1  // abstract  token: 0x60006E5
        public virtual HoudiniEngineUnity.HEU_ObjectNode GetObjectNodeByName(System.String objName) { }
        // RVA: -1  // abstract  token: 0x60006E6
        public virtual System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outputGeoNodes) { }
        // RVA: -1  // abstract  token: 0x60006E7
        public virtual HoudiniEngineUnity.HEU_PartData GetInternalHDAPartWithGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: -1  // abstract  token: 0x60006E8
        public virtual System.Void ResetParametersToDefault() { }
        // RVA: -1  // abstract  token: 0x60006E9
        public virtual System.Void HideAllGeometry() { }
        // RVA: -1  // abstract  token: 0x60006EA
        public virtual System.Void DisableAllColliders() { }
        // RVA: -1  // abstract  token: 0x60006EB
        public virtual HoudiniEngineUnity.HEU_MaterialData GetMaterialData(UnityEngine.Material material) { }
        // RVA: -1  // abstract  token: 0x60006EC
        public virtual System.Void ClearMaterialCache() { }
        // RVA: -1  // abstract  token: 0x60006ED
        public virtual System.Void RemoveMaterial(UnityEngine.Material material) { }
        // RVA: -1  // abstract  token: 0x60006EE
        public virtual System.Void ResetMaterialOverrides() { }
        // RVA: -1  // abstract  token: 0x60006EF
        public virtual HoudiniEngineUnity.HEU_AssetPreset GetAssetPreset(System.Boolean sceneRelativeObjects) { }
        // RVA: -1  // abstract  token: 0x60006F0
        public virtual HoudiniEngineUnity.HEU_PDGAssetLink GetOrCreatePDGAssetLink() { }

    }

    // TypeToken: 0x20000E4
    public interface IHEU_HoudiniAssetSubcomponent
    {
        // Properties
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60006F2
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: -1  // abstract  token: 0x60006F3
        public virtual System.Void Recook() { }

    }

    // TypeToken: 0x20000E5  // size: 0x14
    public sealed struct HEU_InputNodeTypeWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_InputNodeTypeWrapper CONNECTION;  // const
        public static HoudiniEngineUnity.HEU_InputNodeTypeWrapper NODE;  // const
        public static HoudiniEngineUnity.HEU_InputNodeTypeWrapper PARAMETER;  // const

    }

    // TypeToken: 0x20000E6  // size: 0x14
    public sealed struct HEU_InputObjectTypeWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_InputObjectTypeWrapper HDA;  // const
        public static HoudiniEngineUnity.HEU_InputObjectTypeWrapper UNITY_MESH;  // const
        public static HoudiniEngineUnity.HEU_InputObjectTypeWrapper CURVE;  // const
        public static HoudiniEngineUnity.HEU_InputObjectTypeWrapper TERRAIN;  // const
        public static HoudiniEngineUnity.HEU_InputObjectTypeWrapper BOUNDING_BOX;  // const
        public static HoudiniEngineUnity.HEU_InputObjectTypeWrapper TILEMAP;  // const

    }

    // TypeToken: 0x20000E7
    public interface IHEU_InputNode
    {
        // Properties
        System.Boolean KeepWorldTransform { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean PackGeometryBeforeMerging { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputNodeTypeWrapper NodeType { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputObjectTypeWrapper ObjectType { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputObjectTypeWrapper PendingObjectType { get; /* RVA: -1  // abstract */ }
        System.Int32 InputNodeID { get; /* RVA: -1  // abstract */ }
        System.String InputName { get; /* RVA: -1  // abstract */ }
        System.String LabelName { get; /* RVA: -1  // abstract */ }
        System.String ParamName { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputInterfaceMeshSettings MeshSettings { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings TilemapSettings { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_InputInterfaceSplineSettings SplineSettings { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000702
        public virtual System.Boolean IsAssetInput() { }
        // RVA: -1  // abstract  token: 0x6000703
        public virtual System.Int32 NumInputEntries() { }
        // RVA: -1  // abstract  token: 0x6000704
        public virtual UnityEngine.GameObject GetInputEntryGameObject(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x6000705
        public virtual UnityEngine.GameObject[] GetInputEntryGameObjects() { }
        // RVA: -1  // abstract  token: 0x6000706
        public virtual System.Void SetInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000707
        public virtual System.Void InsertInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000708
        public virtual System.Void AddInputEntryAtEnd(UnityEngine.GameObject newEntryGameObject, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000709
        public virtual System.Void ResetInputNode(System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600070A
        public virtual System.Void ChangeInputType(HoudiniEngineUnity.HEU_InputObjectTypeWrapper newType, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600070B
        public virtual System.Void RemoveInputEntry(System.Int32 index, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600070C
        public virtual System.Void RemoveAllInputEntries(System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600070D
        public virtual System.Void SetInputEntryObjectUseTransformOffset(System.Int32 index, System.Boolean value, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600070E
        public virtual System.Void SetInputEntryObjectTransformTranslateOffset(System.Int32 index, UnityEngine.Vector3 translateOffset, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600070F
        public virtual System.Void SetInputEntryObjectTransformRotateOffset(System.Int32 index, UnityEngine.Vector3 rotateOffset, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000710
        public virtual System.Void SetInputEntryObjectTransformScaleOffset(System.Int32 index, UnityEngine.Vector3 scaleOffset, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000711
        public virtual System.Boolean AreAnyInputHDAsConnected() { }
        // RVA: -1  // abstract  token: 0x6000712
        public virtual System.Int32 GetConnectedInputCount() { }
        // RVA: -1  // abstract  token: 0x6000713
        public virtual System.Int32 GetConnectedNodeID(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x6000714
        public virtual System.Void LoadPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset) { }
        // RVA: -1  // abstract  token: 0x6000715
        public virtual System.Void PopulateInputPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset, System.Boolean sceneRelativeGameObjects) { }

    }

    // TypeToken: 0x20000E8  // size: 0x14
    public sealed struct HEU_MaterialSourceWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_MaterialSourceWrapper DEFAULT;  // const
        public static HoudiniEngineUnity.HEU_MaterialSourceWrapper HOUDINI;  // const
        public static HoudiniEngineUnity.HEU_MaterialSourceWrapper UNITY;  // const
        public static HoudiniEngineUnity.HEU_MaterialSourceWrapper SUBSTANCE;  // const

    }

    // TypeToken: 0x20000E9
    public interface IHEU_MaterialData
    {
        // Properties
        UnityEngine.Material Material { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_MaterialSourceWrapper MaterialSource { get; /* RVA: -1  // abstract */ }
        System.Int32 MaterialKey { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600071A
        public virtual System.Boolean UpdateMaterialFromHoudini(HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String assetCacheFolderPath) { }
        // RVA: -1  // abstract  token: 0x600071B
        public virtual System.Boolean IsExistingMaterial() { }

    }

    // TypeToken: 0x20000EA
    public interface IHEU_ObjectNode
    {
        // Properties
        System.Int32 ObjectID { get; /* RVA: -1  // abstract */ }
        System.String ObjectName { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HAPI_ObjectInfo ObjectInfo { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> GeoNodes { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HAPI_Transform ObjectTransform { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000721
        public virtual System.Boolean IsInstanced() { }
        // RVA: -1  // abstract  token: 0x6000722
        public virtual System.Boolean IsVisible() { }
        // RVA: -1  // abstract  token: 0x6000723
        public virtual System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData) { }
        // RVA: -1  // abstract  token: 0x6000724
        public virtual System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: -1  // abstract  token: 0x6000725
        public virtual System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: -1  // abstract  token: 0x6000726
        public virtual HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject) { }
        // RVA: -1  // abstract  token: 0x6000727
        public virtual HoudiniEngineUnity.HEU_GeoNode GetGeoNode(System.String geoName) { }
        // RVA: -1  // abstract  token: 0x6000728
        public virtual System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly) { }
        // RVA: -1  // abstract  token: 0x6000729
        public virtual System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outGeoNodes) { }
        // RVA: -1  // abstract  token: 0x600072A
        public virtual System.Void HideAllGeometry() { }
        // RVA: -1  // abstract  token: 0x600072B
        public virtual System.Void DisableAllColliders() { }
        // RVA: -1  // abstract  token: 0x600072C
        public virtual System.Boolean IsInstancer() { }

    }

    // TypeToken: 0x20000EB  // size: 0x14
    public sealed struct HEU_HoudiniRampInterpolationTypeWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper CONSTANT;  // const
        public static HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper LINEAR;  // const
        public static HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper CATMULL_ROM;  // const

    }

    // TypeToken: 0x20000EC  // size: 0x18
    public class HEU_RampPointWrapper
    {
        // Fields
        private System.Single <Position>k__BackingField;  // 0x10
        private HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper <Interpolation>k__BackingField;  // 0x14

        // Properties
        System.Single Position { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }
        HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper Interpolation { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }

        // Methods
        // RVA: 0x04DA2A60  token: 0x6000731
        public System.Void .ctor(System.Single position, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation) { }

    }

    // TypeToken: 0x20000ED  // size: 0x20
    public class HEU_FloatRampPointWrapper : HoudiniEngineUnity.HEU_RampPointWrapper
    {
        // Fields
        private System.Single <Value>k__BackingField;  // 0x18

        // Properties
        System.Single Value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }

        // Methods
        // RVA: 0x04DA2A20  token: 0x6000734
        public System.Void .ctor(System.Single position, System.Single value, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation) { }

    }

    // TypeToken: 0x20000EE  // size: 0x28
    public class HEU_ColorRampPointWrapper : HoudiniEngineUnity.HEU_RampPointWrapper
    {
        // Fields
        private UnityEngine.Color <Value>k__BackingField;  // 0x18

        // Properties
        UnityEngine.Color Value { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x04D890D0 */ }

        // Methods
        // RVA: 0x04DA2A00  token: 0x6000737
        public System.Void .ctor(System.Single position, UnityEngine.Color value, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation) { }

    }

    // TypeToken: 0x20000EF  // size: 0x40
    public class HEU_ParameterTupleWrapper
    {
        // Fields
        private System.Boolean[] _boolValues;  // 0x10
        private System.Int32[] _intValues;  // 0x18
        private System.Single[] _floatValues;  // 0x20
        private System.String[] _stringValues;  // 0x28
        private HoudiniEngineUnity.HEU_FloatRampPointWrapper[] _floatRampValues;  // 0x30
        private HoudiniEngineUnity.HEU_ColorRampPointWrapper[] _colorRampValues;  // 0x38

        // Properties
        System.Boolean[] BoolValues { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Int32[] IntValues { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Single[] FloatValues { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.String[] StringValues { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        HoudiniEngineUnity.HEU_FloatRampPointWrapper[] FloatRampValues { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        HoudiniEngineUnity.HEU_ColorRampPointWrapper[] ColorRampValues { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000744
        public System.Void .ctor() { }
        // RVA: 0x053908C0  token: 0x6000745
        public System.Void .ctor(System.Boolean[] boolValues) { }
        // RVA: 0x05392C40  token: 0x6000746
        public System.Void .ctor(System.Int32[] intValues) { }
        // RVA: 0x05390F40  token: 0x6000747
        public System.Void .ctor(System.Single[] floatValues) { }
        // RVA: 0x02FDB880  token: 0x6000748
        public System.Void .ctor(System.String[] stringValues) { }
        // RVA: 0x05396200  token: 0x6000749
        public System.Void .ctor(HoudiniEngineUnity.HEU_FloatRampPointWrapper[] floatRampValues) { }
        // RVA: 0x046A91B0  token: 0x600074A
        public System.Void .ctor(HoudiniEngineUnity.HEU_ColorRampPointWrapper[] colorRampValues) { }

    }

    // TypeToken: 0x20000F0
    public interface IHEU_Parameters
    {
        // Properties
        System.Boolean ShowParameters { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Int32 NodeID { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<System.Int32> RootParameters { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterModifier> ParameterModifiers { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000750
        public virtual System.Boolean AreParametersValid() { }
        // RVA: -1  // abstract  token: 0x6000751
        public virtual System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterData> GetParameters() { }
        // RVA: -1  // abstract  token: 0x6000752
        public virtual HoudiniEngineUnity.HEU_ParameterData GetParameter(System.Int32 listIndex) { }
        // RVA: -1  // abstract  token: 0x6000753
        public virtual HoudiniEngineUnity.HEU_ParameterData GetParameter(System.String name) { }
        // RVA: -1  // abstract  token: 0x6000754
        public virtual HoudiniEngineUnity.HEU_ParameterData GetParameterWithParmID(System.Int32 parmID) { }
        // RVA: -1  // abstract  token: 0x6000755
        public virtual System.Void RemoveParameter(System.Int32 listIndex) { }
        // RVA: -1  // abstract  token: 0x6000756
        public virtual System.Boolean HaveParametersChanged() { }
        // RVA: -1  // abstract  token: 0x6000757
        public virtual System.Boolean ResetAllToDefault(System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000758
        public virtual System.Boolean SetFloatParameterValue(System.String parameterName, System.Single value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000759
        public virtual System.Boolean GetFloatParameterValue(System.String parameterName, System.Single& value, System.Int32 atIndex) { }
        // RVA: -1  // abstract  token: 0x600075A
        public virtual System.Boolean SetFloatParameterValues(System.String parameterName, System.Single[] values, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600075B
        public virtual System.Boolean GetFloatParameterValues(System.String parameterName, System.Single[]& values) { }
        // RVA: -1  // abstract  token: 0x600075C
        public virtual System.Boolean SetColorParameterValue(System.String parameterName, UnityEngine.Color value, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600075D
        public virtual System.Boolean GetColorParameterValue(System.String parameterName, UnityEngine.Color& value) { }
        // RVA: -1  // abstract  token: 0x600075E
        public virtual System.Boolean SetIntParameterValue(System.String parameterName, System.Int32 value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600075F
        public virtual System.Boolean GetIntParameterValue(System.String parameterName, System.Int32& value, System.Int32 atIndex) { }
        // RVA: -1  // abstract  token: 0x6000760
        public virtual System.Boolean SetIntParameterValues(System.String parameterName, System.Int32[] values, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000761
        public virtual System.Boolean GetIntParameterValues(System.String parameterName, System.Int32[]& values) { }
        // RVA: -1  // abstract  token: 0x6000762
        public virtual System.Boolean SetChoiceParameterValue(System.String parameterName, System.Int32 value, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000763
        public virtual System.Boolean GetChoiceParameterValue(System.String parameterName, System.Int32& value) { }
        // RVA: -1  // abstract  token: 0x6000764
        public virtual System.Boolean SetBoolParameterValue(System.String parameterName, System.Boolean value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000765
        public virtual System.Boolean GetBoolParameterValue(System.String parameterName, System.Boolean& value, System.Int32 atIndex) { }
        // RVA: -1  // abstract  token: 0x6000766
        public virtual System.Boolean SetStringParameterValue(System.String parameterName, System.String value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000767
        public virtual System.Boolean GetStringParameterValue(System.String parameterName, System.String& value, System.Int32 atIndex) { }
        // RVA: -1  // abstract  token: 0x6000768
        public virtual System.Boolean SetStringParameterValues(System.String parameterName, System.String[] values, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000769
        public virtual System.Boolean GetStringParameterValues(System.String parameterName, System.String[]& values) { }
        // RVA: -1  // abstract  token: 0x600076A
        public virtual System.Boolean SetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject value, System.Int32 atIndex, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600076B
        public virtual System.Boolean GetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject& value, System.Int32 atIndex) { }
        // RVA: -1  // abstract  token: 0x600076C
        public virtual System.Boolean SetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[] values, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600076D
        public virtual System.Boolean GetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[]& values) { }
        // RVA: -1  // abstract  token: 0x600076E
        public virtual System.Boolean SetRampParameterNumPoints(System.String parameterName, System.Int32 numPoints, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600076F
        public virtual System.Boolean GetRampParameterNumPoints(System.String parameterName, System.Int32& numPoints) { }
        // RVA: -1  // abstract  token: 0x6000770
        public virtual System.Boolean SetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, System.Single pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolationType, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000771
        public virtual System.Boolean GetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, System.Single& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType) { }
        // RVA: -1  // abstract  token: 0x6000772
        public virtual System.Boolean SetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[] rampPoints, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000773
        public virtual System.Boolean GetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[]& rampPoints) { }
        // RVA: -1  // abstract  token: 0x6000774
        public virtual System.Boolean SetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, UnityEngine.Color pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000775
        public virtual System.Boolean GetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, UnityEngine.Color& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType) { }
        // RVA: -1  // abstract  token: 0x6000776
        public virtual System.Boolean SetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[] rampPoints, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x6000777
        public virtual System.Boolean GetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[]& rampPoints) { }
        // RVA: -1  // abstract  token: 0x6000778
        public virtual System.Boolean TriggerButtonParameter(System.String parameterName) { }
        // RVA: -1  // abstract  token: 0x6000779
        public virtual System.Boolean SetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper> parameterTuples, System.Boolean bRecookAsset) { }
        // RVA: -1  // abstract  token: 0x600077A
        public virtual System.Boolean GetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper>& parameterTuples) { }

    }

    // TypeToken: 0x20000F1
    public interface IHEU_PartData
    {
        // Properties
        System.Int32 PartID { get; /* RVA: -1  // abstract */ }
        System.String PartName { get; /* RVA: -1  // abstract */ }
        System.Int32 GeoID { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HAPI_PartType PartType { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_GeoNode ParentGeoNode { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> ObjectInstanceInfos { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_Curve Curve { get; /* RVA: -1  // abstract */ }
        System.Int32 MeshVertexCount { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_GeneratedOutput GeneratedOutput { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject OutputGameObject { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000785
        public virtual System.Boolean IsPartInstancer() { }
        // RVA: -1  // abstract  token: 0x6000786
        public virtual System.Boolean IsAttribInstancer() { }
        // RVA: -1  // abstract  token: 0x6000787
        public virtual System.Boolean IsInstancerAnyType() { }
        // RVA: -1  // abstract  token: 0x6000788
        public virtual System.Boolean IsPartInstanced() { }
        // RVA: -1  // abstract  token: 0x6000789
        public virtual System.Int32 GetPartPointCount() { }
        // RVA: -1  // abstract  token: 0x600078A
        public virtual System.Boolean IsObjectInstancer() { }
        // RVA: -1  // abstract  token: 0x600078B
        public virtual System.Boolean IsPartVolume() { }
        // RVA: -1  // abstract  token: 0x600078C
        public virtual System.Boolean IsPartCurve() { }
        // RVA: -1  // abstract  token: 0x600078D
        public virtual System.Boolean IsPartMesh() { }
        // RVA: -1  // abstract  token: 0x600078E
        public virtual System.Boolean IsPartEditable() { }
        // RVA: -1  // abstract  token: 0x600078F
        public virtual System.Boolean HaveInstancesBeenGenerated() { }
        // RVA: -1  // abstract  token: 0x6000790
        public virtual System.Void SetGameObjectName(System.String partName) { }
        // RVA: -1  // abstract  token: 0x6000791
        public virtual System.Void SetGameObject(UnityEngine.GameObject gameObject) { }
        // RVA: -1  // abstract  token: 0x6000792
        public virtual System.Void SetVolumeLayerName(System.String name) { }
        // RVA: -1  // abstract  token: 0x6000793
        public virtual System.String GetVolumeLayerName() { }
        // RVA: -1  // abstract  token: 0x6000794
        public virtual System.Void DestroyAllData(System.Boolean bIsRebuild) { }
        // RVA: -1  // abstract  token: 0x6000795
        public virtual System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData) { }
        // RVA: -1  // abstract  token: 0x6000796
        public virtual System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects) { }
        // RVA: -1  // abstract  token: 0x6000797
        public virtual System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs) { }
        // RVA: -1  // abstract  token: 0x6000798
        public virtual HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject inGameObject) { }
        // RVA: -1  // abstract  token: 0x6000799
        public virtual System.Void ClearInstances() { }
        // RVA: -1  // abstract  token: 0x600079A
        public virtual HoudiniEngineUnity.HEU_Curve GetCurve(System.Boolean bEditableOnly) { }
        // RVA: -1  // abstract  token: 0x600079B
        public virtual System.Void SetVisiblity(System.Boolean bVisibility) { }
        // RVA: -1  // abstract  token: 0x600079C
        public virtual System.Void SetColliderState(System.Boolean bEnabled) { }
        // RVA: -1  // abstract  token: 0x600079D
        public virtual HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectPath(System.String path) { }
        // RVA: -1  // abstract  token: 0x600079E
        public virtual HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectID(System.Int32 objNodeID) { }
        // RVA: -1  // abstract  token: 0x600079F
        public virtual System.Void SetTerrainOffsetPosition(UnityEngine.Vector3 offsetPosition) { }
        // RVA: -1  // abstract  token: 0x60007A0
        public virtual System.Void SetTerrainData(UnityEngine.TerrainData terrainData, System.String exportPathRelative, System.String exportPathUser) { }

    }

    // TypeToken: 0x20000F2  // size: 0x14
    public sealed struct HEU_LinkStateWrapper
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_LinkStateWrapper INACTIVE;  // const
        public static HoudiniEngineUnity.HEU_LinkStateWrapper LINKING;  // const
        public static HoudiniEngineUnity.HEU_LinkStateWrapper LINKED;  // const
        public static HoudiniEngineUnity.HEU_LinkStateWrapper ERROR_NOT_LINKED;  // const

    }

    // TypeToken: 0x20000F3
    public interface IHEU_PDGAssetLink
    {
        // Properties
        System.Boolean AutoCook { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean UseHEngineData { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean UseTOPNodeFilter { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean UseTOPOutputFilter { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.String TopNodeFilter { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.String TopOutputFilter { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: -1  // abstract */ }
        System.String AssetPath { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject AssetGO { get; /* RVA: -1  // abstract */ }
        System.String AssetName { get; /* RVA: -1  // abstract */ }
        System.Int32 AssetID { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNetworkData> TopNetworks { get; /* RVA: -1  // abstract */ }
        System.String[] TopNetworkNames { get; /* RVA: -1  // abstract */ }
        System.Int32 SelectedTOPNetwork { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_LinkStateWrapper PDGLinkState { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject LoadRootGameObject { get; /* RVA: -1  // abstract */ }
        System.String OutputCachePathRoot { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60007B8
        public virtual System.Void Setup(HoudiniEngineUnity.HEU_HoudiniAsset hdaAsset) { }
        // RVA: -1  // abstract  token: 0x60007B9
        public virtual System.Void Reset() { }
        // RVA: -1  // abstract  token: 0x60007BA
        public virtual System.Void Refresh() { }
        // RVA: -1  // abstract  token: 0x60007BB
        public virtual System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,HoudiniEngineUnity.HEU_TOPNodeData>> GetNonHiddenTOPNodes(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork) { }
        // RVA: -1  // abstract  token: 0x60007BC
        public virtual System.Void SelectTOPNetwork(System.Int32 newIndex) { }
        // RVA: -1  // abstract  token: 0x60007BD
        public virtual System.Void SelectTOPNode(HoudiniEngineUnity.HEU_TOPNetworkData network, System.Int32 newIndex) { }
        // RVA: -1  // abstract  token: 0x60007BE
        public virtual HoudiniEngineUnity.HEU_TOPNetworkData GetSelectedTOPNetwork() { }
        // RVA: -1  // abstract  token: 0x60007BF
        public virtual HoudiniEngineUnity.HEU_TOPNodeData GetSelectedTOPNode() { }
        // RVA: -1  // abstract  token: 0x60007C0
        public virtual HoudiniEngineUnity.HEU_TOPNetworkData GetTOPNetwork(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x60007C1
        public virtual System.Void DirtyTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: -1  // abstract  token: 0x60007C2
        public virtual System.Void CookTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }
        // RVA: -1  // abstract  token: 0x60007C3
        public virtual System.Void DirtyAll() { }
        // RVA: -1  // abstract  token: 0x60007C4
        public virtual System.Void CookOutput() { }
        // RVA: -1  // abstract  token: 0x60007C5
        public virtual System.Void PauseCook() { }
        // RVA: -1  // abstract  token: 0x60007C6
        public virtual System.Void CancelCook() { }
        // RVA: -1  // abstract  token: 0x60007C7
        public virtual HoudiniEngineUnity.HEU_SessionBase GetHAPISession() { }
        // RVA: -1  // abstract  token: 0x60007C8
        public virtual HoudiniEngineUnity.HEU_TOPNodeData GetTOPNode(System.Int32 nodeID) { }
        // RVA: -1  // abstract  token: 0x60007C9
        public virtual System.String GetTOPNodeStatus(HoudiniEngineUnity.HEU_TOPNodeData topNode) { }

    }

    // TypeToken: 0x20000F4
    public interface IHEU_VolumeCache
    {
        // Properties
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayer> Layers { get; /* RVA: -1  // abstract */ }
        System.Int32 TileIndex { get; /* RVA: -1  // abstract */ }
        System.String ObjectName { get; /* RVA: -1  // abstract */ }
        System.String GeoName { get; /* RVA: -1  // abstract */ }
        UnityEngine.TerrainData TerrainData { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_VolumeScatterTrees ScatterTrees { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.HEU_DetailProperties DetailProperties { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60007D1
        public virtual System.Void ResetParameters() { }
        // RVA: -1  // abstract  token: 0x60007D2
        public virtual HoudiniEngineUnity.HEU_VolumeLayer GetLayer(System.String layerName) { }
        // RVA: -1  // abstract  token: 0x60007D3
        public virtual System.Void PopulatePreset(HoudiniEngineUnity.HEU_VolumeCachePreset cachePreset) { }
        // RVA: -1  // abstract  token: 0x60007D4
        public virtual System.Boolean ApplyPreset(HoudiniEngineUnity.HEU_VolumeCachePreset volumeCachePreset) { }

    }

    // TypeToken: 0x20000F5  // size: 0x28
    public class HEU_MaterialData : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_MaterialData, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private UnityEngine.Material _material;  // 0x18
        private HoudiniEngineUnity.HEU_MaterialData.Source _materialSource;  // 0x20
        private System.Int32 _materialKey;  // 0x24

        // Properties
        UnityEngine.Material Material { get; /* RVA: 0x0385B100 */ }
        HoudiniEngineUnity.HEU_MaterialSourceWrapper MaterialSource { get; /* RVA: 0x09D8ACD8 */ }
        System.Int32 MaterialKey { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }

        // Methods
        // RVA: 0x09D8A2E0  token: 0x60007D9
        public virtual System.Boolean UpdateMaterialFromHoudini(HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String assetCacheFolderPath) { }
        // RVA: 0x09D8A288  token: 0x60007DA
        public virtual System.Boolean IsExistingMaterial() { }
        // RVA: 0x09D8A5E8  token: 0x60007DB
        private System.Boolean UseLegacyShaders(HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String assetCacheFolderPath, HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_NodeInfo nodeInfo, HoudiniEngineUnity.HAPI_ParmInfo[] parmInfos) { }
        // RVA: 0x09D89FE8  token: 0x60007DC
        private static System.String GetTextureFileNameFromMaterialParam(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo parmInfo) { }
        // RVA: 0x09D8A29C  token: 0x60007DD
        private static System.Boolean IsTransparentMaterial(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters) { }
        // RVA: 0x09D89D74  token: 0x60007DE
        private static System.Boolean GetMaterialAlpha(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.Single defaultValue, System.Single& alpha) { }
        // RVA: 0x09D89EE8  token: 0x60007DF
        private static System.String GetSupportedFileFormat(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ImageInfo& imageInfo) { }
        // RVA: 0x09D8A0EC  token: 0x60007E0
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_MaterialData other) { }
        // RVA: 0x09D43A04  token: 0x60007E1
        private static HoudiniEngineUnity.HEU_MaterialData.Source MaterialSource_WrapperToInternal(HoudiniEngineUnity.HEU_MaterialSourceWrapper materialSource) { }
        // RVA: 0x09D43A04  token: 0x60007E2
        private static HoudiniEngineUnity.HEU_MaterialSourceWrapper MaterialSource_WrapperToInternal(HoudiniEngineUnity.HEU_MaterialData.Source materialSource) { }
        // RVA: 0x09D8ACC8  token: 0x60007E3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F7  // size: 0x10
    public class HEU_MaterialFactory
    {
        // Fields
        public static UnityEngine.Material _defaultStandardMaterial;  // static @ 0x0

        // Methods
        // RVA: 0x09D8B57C  token: 0x60007E4
        public static UnityEngine.Shader FindShader(System.String shaderName) { }
        // RVA: 0x09D8B544  token: 0x60007E5
        public static UnityEngine.Shader FindPluginShader(System.String shaderName) { }
        // RVA: 0x09D8B9E8  token: 0x60007E6
        public static UnityEngine.Material GetNewMaterialWithShader(System.String assetCacheFolderPath, System.String shaderName, System.String materialName, System.Boolean bWriteToFile) { }
        // RVA: 0x09D8B058  token: 0x60007E7
        public static UnityEngine.Material CreateNewHoudiniStandardMaterial(System.String assetCacheFolderPath, System.String materialName, System.Boolean bWriteToFile) { }
        // RVA: 0x09D8C5B0  token: 0x60007E8
        public static System.Void WriteMaterialToAssetCache(UnityEngine.Material material, System.String assetCacheFolderPath, System.String materialName, System.Boolean bOverwriteExisting) { }
        // RVA: 0x09D8B3B8  token: 0x60007E9
        public static System.Boolean DoesMaterialExistInAssetCache(UnityEngine.Material material) { }
        // RVA: 0x09D8B344  token: 0x60007EA
        public static System.Void DestroyNonAssetMaterial(UnityEngine.Material material, System.Boolean bRegisterUndo) { }
        // RVA: 0x09D8B33C  token: 0x60007EB
        public static System.Void DeleteAssetMaterial(UnityEngine.Material material) { }
        // RVA: 0x09D8C4E8  token: 0x60007EC
        public static UnityEngine.Texture2D RenderAndExtractImageToTexture(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.Int32 textureParmID, System.String textureName, System.String assetCacheFolderPath, System.Boolean isNormalMap, System.Boolean invertTexture) { }
        // RVA: 0x09D8B480  token: 0x60007ED
        private static UnityEngine.Texture2D ExtractHoudiniImageToTexturePNGJPEG(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String imagePlanes) { }
        // RVA: 0x09D8B4DC  token: 0x60007EE
        private static UnityEngine.Texture2D ExtractHoudiniImageToTextureRaw(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String imagePlanes) { }
        // RVA: 0x09D8B3FC  token: 0x60007EF
        public static UnityEngine.Texture2D ExtractHoudiniImageToTextureFile(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String imagePlanes, System.String assetCacheFolderPath) { }
        // RVA: 0x09D8C1E0  token: 0x60007F0
        public static UnityEngine.Material LoadUnityMaterial(System.String materialPath) { }
        // RVA: 0x09D8BF4C  token: 0x60007F1
        public static UnityEngine.Material LoadSubstanceMaterialWithName(System.String materialPath, System.String substanceName) { }
        // RVA: 0x09D8BD84  token: 0x60007F2
        public static UnityEngine.Material LoadSubstanceMaterialWithIndex(System.String materialPath, System.Int32 substanceMaterialIndex) { }
        // RVA: 0x09D8BC88  token: 0x60007F3
        public static System.Int32 GetUnitySubstanceMaterialKey(System.String unityMaterialPath, System.String substanceName, System.Int32 substanceIndex) { }
        // RVA: 0x053E31AC  token: 0x60007F4
        public static System.Int32 MaterialNameToKey(System.String materialName) { }
        // RVA: 0x09D8B3DC  token: 0x60007F5
        public static System.Void EnableGPUInstancing(UnityEngine.Material material) { }
        // RVA: 0x09D8C4CC  token: 0x60007F6
        public static System.Boolean MaterialHasGPUInstancingEnabled(UnityEngine.Material material) { }
        // RVA: 0x09D8AD04  token: 0x60007F7
        public static UnityEngine.Material CopyMaterial(UnityEngine.Material material) { }
        // RVA: 0x09D8B650  token: 0x60007F8
        public static UnityEngine.Material GetDefaultStandardMaterial() { }
        // RVA: 0x09D8C0D8  token: 0x60007F9
        public static UnityEngine.Texture2D LoadTexture(System.String path) { }
        // RVA: 0x09D8C5A8  token: 0x60007FA
        public static UnityEngine.Texture2D WhiteTexture() { }
        // RVA: 0x09D8B0A4  token: 0x60007FB
        public static HoudiniEngineUnity.HEU_MaterialData CreateUnitySubstanceMaterialData(System.Int32 materialKey, System.String materialPath, System.String substanceName, System.Int32 substanceIndex, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath) { }
        // RVA: 0x09D8AEF0  token: 0x60007FC
        public static HoudiniEngineUnity.HEU_MaterialData CreateMaterialInCache(System.Int32 materialKey, System.String materialName, HoudiniEngineUnity.HEU_MaterialSourceWrapper sourceType, System.Boolean bWriteToFile, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath) { }
        // RVA: 0x09D8AF4C  token: 0x60007FD
        private static HoudiniEngineUnity.HEU_MaterialData CreateMaterialInCache(System.Int32 materialKey, System.String materialName, HoudiniEngineUnity.HEU_MaterialData.Source sourceType, System.Boolean bWriteToFile, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath) { }
        // RVA: 0x09D8BBB8  token: 0x60007FE
        public static HoudiniEngineUnity.HEU_MaterialData GetOrCreateDefaultMaterialInCache(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Boolean bWriteToFile, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath) { }
        // RVA: 0x09D8B700  token: 0x60007FF
        public static HoudiniEngineUnity.HEU_MaterialData GetMaterialDataFromCache(System.Int32 materialKey, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache) { }
        // RVA: 0x09D8B5B4  token: 0x6000800
        public static System.String GenerateDefaultMaterialName(System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09D8AD64  token: 0x6000801
        public static HoudiniEngineUnity.HEU_MaterialData CreateHoudiniMaterialData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32 materialID, System.Int32 geoID, System.Int32 partID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath) { }
        // RVA: 0x09D8B81C  token: 0x6000802
        public static System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MaterialData> GetMaterialDataMapFromCache(System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache) { }
        // RVA: 0x041E1670  token: 0x6000803
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F8  // size: 0x28
    public class HEU_UnityMaterialInfo
    {
        // Fields
        public System.String _unityMaterialPath;  // 0x10
        public System.String _substancePath;  // 0x18
        public System.Int32 _substanceIndex;  // 0x20

        // Methods
        // RVA: 0x047E1B40  token: 0x6000804
        public System.Void .ctor(System.String unityMaterialPath, System.String substancePath, System.Int32 substanceIndex) { }

    }

    // TypeToken: 0x20000F9  // size: 0x30
    public class HEU_SessionBase
    {
        // Fields
        protected HoudiniEngineUnity.HEU_SessionData _sessionData;  // 0x10
        private System.Boolean <UserNotifiedSessionInvalid>k__BackingField;  // 0x18
        private System.String _sessionErrorMsg;  // 0x20
        private System.Boolean <LogErrorOverride>k__BackingField;  // 0x28
        private System.Boolean <ThrowErrorOverride>k__BackingField;  // 0x29
        private HoudiniEngineUnity.HAPI_Result <LastCallResultCode>k__BackingField;  // 0x2c

        // Properties
        System.Boolean UserNotifiedSessionInvalid { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        HoudiniEngineUnity.SessionConnectionState ConnectionState { get; /* RVA: 0x09D915EC */ set; /* RVA: 0x09D91614 */ }
        HoudiniEngineUnity.SessionMode ThisSessionMode { get; /* RVA: 0x09D91600 */ set; /* RVA: 0x09D91624 */ }
        System.Boolean LogErrorOverride { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.Boolean ThrowErrorOverride { get; /* RVA: 0x04D867B0 */ set; /* RVA: 0x04D867C0 */ }
        HoudiniEngineUnity.HAPI_Result LastCallResultCode { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }

        // Methods
        // RVA: 0x04D862C0  token: 0x600080B
        public System.String GetSessionErrorMsg() { }
        // RVA: 0x09D9131C  token: 0x6000810
        public System.Boolean IsSessionSync() { }
        // RVA: 0x011EC580  token: 0x6000813
        public virtual HoudiniEngineUnity.HEU_HoudiniAsset GetRegisteredAssetFromID(System.Int32 id) { }
        // RVA: 0x012081B0  token: 0x6000814
        public virtual System.Boolean IsAssetRegistered(HoudiniEngineUnity.HEU_HoudiniAsset asset) { }
        // RVA: 0x041E1670  token: 0x6000815
        public virtual System.Void RegisterAsset(HoudiniEngineUnity.HEU_HoudiniAsset asset) { }
        // RVA: 0x041E1670  token: 0x6000816
        public virtual System.Void UnregisterAsset(System.Int32 id) { }
        // RVA: 0x041E1670  token: 0x6000817
        public virtual System.Void ReregisterOnAwake(HoudiniEngineUnity.HEU_HoudiniAsset asset) { }
        // RVA: 0x04DA2BA0  token: 0x6000818
        public System.Void .ctor() { }
        // RVA: 0x09D9157C  token: 0x6000819
        public virtual System.Void SetSessionErrorMsg(System.String msg, System.Boolean bLogError) { }
        // RVA: 0x09D913C0  token: 0x600081A
        public virtual System.Void SetSessionConnectionErrorMsg(System.String introMsg, HoudiniEngineUnity.HAPI_Result result, System.Boolean bIsHARSRunning, System.Boolean bLogError) { }
        // RVA: 0x09D91334  token: 0x600081B
        public virtual System.Void SetLibraryErrorMsg(System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x600081C
        protected virtual System.Boolean CreateSessionData(System.Boolean bOverwriteExisting, System.Boolean bIsDefaultSession) { }
        // RVA: 0x012081B0  token: 0x600081D
        public virtual System.Boolean CreateInProcessSession(System.Boolean bIsDefaultSession) { }
        // RVA: 0x012081B0  token: 0x600081E
        public virtual System.Boolean CreateThriftSocketSession(System.Boolean bIsDefaultSession, System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x600081F
        public virtual System.Boolean CreateThriftPipeSession(System.Boolean bIsDefaultSession, System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000820
        public virtual System.Boolean CreateThriftSharedMemorySession(System.Boolean bIsDefaultSession, System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000821
        public virtual System.Boolean CreateCustomSession(System.Boolean bIsDefaultSession) { }
        // RVA: 0x012081B0  token: 0x6000822
        public virtual System.Boolean ConnectThriftSocketSession(System.Boolean bIsDefaultSession, System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean logError, System.Boolean autoInitialize) { }
        // RVA: 0x012081B0  token: 0x6000823
        public virtual System.Boolean ConnectThriftPipeSession(System.Boolean bIsDefaultSession, System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean logError, System.Boolean autoInitialize) { }
        // RVA: 0x012081B0  token: 0x6000824
        public virtual System.Boolean ConnectThriftSharedMemorySession(System.Boolean bIsDefaultSession, System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean logError, System.Boolean autoInitialize) { }
        // RVA: 0x012081B0  token: 0x6000825
        public virtual System.Boolean CloseSession() { }
        // RVA: 0x012081B0  token: 0x6000826
        protected virtual System.Boolean CheckAndCloseExistingSession() { }
        // RVA: 0x09D91154  token: 0x6000827
        protected virtual System.Void ClearSessionInfo() { }
        // RVA: 0x053908C0  token: 0x6000828
        public System.Void SetSessionData(HoudiniEngineUnity.HEU_SessionData sessionData) { }
        // RVA: 0x02B2ECC0  token: 0x6000829
        public HoudiniEngineUnity.HEU_SessionData GetSessionData() { }
        // RVA: 0x09D912C4  token: 0x600082A
        public virtual System.String GetSessionInfo() { }
        // RVA: 0x012081B0  token: 0x600082B
        public virtual System.Boolean IsSessionValid() { }
        // RVA: 0x012081B0  token: 0x600082C
        public virtual System.Boolean RestartSession() { }
        // RVA: 0x04D862C0  token: 0x600082D
        public System.String GetLastSessionError() { }
        // RVA: 0x012081B0  token: 0x600082E
        public virtual System.Boolean CheckVersionMatch() { }
        // RVA: 0x012081B0  token: 0x600082F
        public virtual System.Boolean InitializeSession(HoudiniEngineUnity.HEU_SessionData sessionData) { }
        // RVA: 0x012081B0  token: 0x6000830
        public virtual System.Boolean HandleStatusResult(HoudiniEngineUnity.HAPI_Result result, System.String prependMsg, System.Boolean bThrowError, System.Boolean bLogError) { }
        // RVA: 0x041E1670  token: 0x6000831
        public virtual System.Void SetServerEnvString(System.String name, System.String value) { }
        // RVA: 0x09D912A8  token: 0x6000832
        public virtual System.Boolean GetServerEnvString(System.String name, System.String& value) { }
        // RVA: 0x04DA2B00  token: 0x6000833
        public virtual System.Boolean GetServerEnvVarCount(System.Int32& env_count) { }
        // RVA: 0x04DA2B00  token: 0x6000834
        public virtual System.Boolean GetCallResult(HoudiniEngineUnity.HAPI_Result& result) { }
        // RVA: 0x04DA2B00  token: 0x6000835
        public virtual System.Boolean GetCookResult(HoudiniEngineUnity.HAPI_Result& result) { }
        // RVA: 0x04DA2B00  token: 0x6000836
        public virtual System.Boolean GetCookState(HoudiniEngineUnity.HAPI_State& state) { }
        // RVA: 0x09D912F0  token: 0x6000837
        public virtual System.String GetStatusString(HoudiniEngineUnity.HAPI_StatusType statusType, HoudiniEngineUnity.HAPI_StatusVerbosity verbosity) { }
        // RVA: 0x09D9119C  token: 0x6000838
        public virtual System.String ComposeNodeCookResult(System.Int32 nodeId, HoudiniEngineUnity.HAPI_StatusVerbosity verbosity) { }
        // RVA: 0x011EC580  token: 0x6000839
        public virtual System.Int32 GetEnvInt(HoudiniEngineUnity.HAPI_EnvIntType intType) { }
        // RVA: 0x011EC580  token: 0x600083A
        public virtual System.Int32 GetSessionEnvInt(HoudiniEngineUnity.HAPI_SessionEnvIntType intType, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x600083B
        public virtual System.Boolean GetString(System.Int32 stringHandle, System.String& resultString, System.Int32 bufferLength) { }
        // RVA: 0x011EC580  token: 0x600083C
        public virtual System.Int32 GetStringBufferLength(System.Int32 stringHandle) { }
        // RVA: 0x011EC580  token: 0x600083D
        public virtual System.Int32 CheckForSpecificErrors(System.Int32 nodeID, System.Int32 errorsToCheck) { }
        // RVA: 0x04D879D0  token: 0x600083E
        public virtual System.Double GetTime() { }
        // RVA: 0x012081B0  token: 0x600083F
        public virtual System.Boolean SetTime(System.Double time) { }
        // RVA: 0x012081B0  token: 0x6000840
        public virtual System.Boolean GetUseHoudiniTime() { }
        // RVA: 0x012081B0  token: 0x6000841
        public virtual System.Boolean SetUseHoudiniTime(System.Boolean enable) { }
        // RVA: 0x04DA2B10  token: 0x6000842
        public virtual System.Boolean LoadAssetLibraryFromFile(System.String assetPath, System.Boolean bAllowOverwrite, System.Int32& libraryID) { }
        // RVA: 0x04DA2B10  token: 0x6000843
        public virtual System.Boolean LoadAssetLibraryFromMemory(System.Byte[] buffer, System.Boolean bAllowOverwrite, System.Int32& libraryID) { }
        // RVA: 0x04DA2AF0  token: 0x6000844
        public virtual System.Boolean CreateNode(System.Int32 parentNodeID, System.String operatorName, System.String nodeLabel, System.Boolean bCookOnCreation, System.Int32& newNodeID) { }
        // RVA: 0x041E1670  token: 0x6000845
        public virtual System.Void DeleteNode(System.Int32 nodeID) { }
        // RVA: 0x012081B0  token: 0x6000846
        public virtual System.Boolean CookNode(System.Int32 nodeID, System.Boolean bCookTemplatedGeos, System.Boolean bSplitGeosByGroup) { }
        // RVA: 0x012081B0  token: 0x6000847
        public virtual System.Boolean CookNodeWithOptions(System.Int32 nodeID, HoudiniEngineUnity.HAPI_CookOptions cookOptions) { }
        // RVA: 0x012081B0  token: 0x6000848
        public virtual System.Boolean RenameNode(System.Int32 nodeID, System.String newName) { }
        // RVA: 0x012081B0  token: 0x6000849
        public virtual System.Boolean ConnectNodeInput(System.Int32 nodeID, System.Int32 inputIndex, System.Int32 nodeIDToConnect, System.Int32 outputIndex) { }
        // RVA: 0x012081B0  token: 0x600084A
        public virtual System.Boolean DisconnectNodeInput(System.Int32 nodeID, System.Int32 inputIndex, System.Boolean bLogError) { }
        // RVA: 0x04DA2B40  token: 0x600084B
        public virtual System.Boolean QueryNodeInput(System.Int32 nodeID, System.Int32 inputIndex, System.Int32& connectedNodeID, System.Boolean bLogError) { }
        // RVA: 0x04DA2B10  token: 0x600084C
        public virtual System.Boolean GetNodeInputName(System.Int32 nodeID, System.Int32 inputIndex, System.Int32& nodeNameIndex) { }
        // RVA: 0x04DA2A70  token: 0x600084D
        public virtual System.Boolean GetAvailableAssetCount(System.Int32 libraryID, System.Int32& assetCount) { }
        // RVA: 0x012081B0  token: 0x600084E
        public virtual System.Boolean GetAvailableAssets(System.Int32 libraryID, System.Int32[]& assetNames, System.Int32 assetCount) { }
        // RVA: 0x012081B0  token: 0x600084F
        public virtual System.Boolean GetAssetInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_AssetInfo& assetInfo, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000850
        public virtual System.Boolean GetNodeInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, System.Boolean bLogError) { }
        // RVA: 0x080165E0  token: 0x6000851
        public virtual System.Boolean GetNodePath(System.Int32 nodeID, System.Int32 relativeNodeID, System.String& path) { }
        // RVA: 0x012081B0  token: 0x6000852
        public virtual System.Boolean IsNodeValid(System.Int32 nodeID, System.Int32 uniqueNodeID) { }
        // RVA: 0x012081B0  token: 0x6000853
        public virtual System.Boolean ComposeChildNodeList(System.Int32 parentNodeID, System.Int32 nodeTypeFilter, System.Int32 nodeFlagFilter, System.Boolean bRecursive, System.Int32& count, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000854
        public virtual System.Boolean GetComposedChildNodeList(System.Int32 parentNodeID, System.Int32[] childNodeIDs, System.Int32 count, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000855
        public virtual System.Boolean LoadHIPFile(System.String fileName, System.Boolean bCookOnLoad) { }
        // RVA: 0x012081B0  token: 0x6000856
        public virtual System.Boolean SaveHIPFile(System.String fileName, System.Boolean bLockNodes) { }
        // RVA: 0x012081B0  token: 0x6000857
        public virtual System.Boolean GetObjectInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ObjectInfo& objectInfo) { }
        // RVA: 0x012081B0  token: 0x6000858
        public virtual System.Boolean GetObjectTransform(System.Int32 nodeID, System.Int32 relativeToNodeID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform& hapiTransform) { }
        // RVA: 0x012081B0  token: 0x6000859
        public virtual System.Boolean SetObjectTransform(System.Int32 nodeID, HoudiniEngineUnity.HAPI_TransformEuler& hapiTransform) { }
        // RVA: 0x04DA2A70  token: 0x600085A
        public virtual System.Boolean ComposeObjectList(System.Int32 nodeID, System.Int32& objectCount) { }
        // RVA: 0x012081B0  token: 0x600085B
        public virtual System.Boolean GetComposedObjectList(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ObjectInfo[] objectInfos, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600085C
        public virtual System.Boolean GetComposedObjectTransforms(System.Int32 nodeID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transforms, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600085D
        public virtual System.Boolean GetDisplayGeoInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GeoInfo& geoInfo, System.Boolean bLogError) { }
        // RVA: 0x04DA2A70  token: 0x600085E
        public virtual System.Boolean GetOutputGeoCount(System.Int32 nodeID, System.Int32& count, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x600085F
        public virtual System.Boolean GetOutputGeoInfos(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GeoInfo[]& geoInfosArray, System.Int32 count, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000860
        public virtual System.Boolean GetGeoInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GeoInfo& geoInfo, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000861
        public virtual System.Boolean GetPartInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }
        // RVA: 0x012081B0  token: 0x6000862
        public virtual System.Boolean GetAttributeInfo(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeOwner owner, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo) { }
        // RVA: 0x012081B0  token: 0x6000863
        public virtual System.Boolean GetAttributeNames(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeOwner owner, System.String[]& attributeNames, System.Int32 count) { }
        // RVA: 0x012081B0  token: 0x6000864
        public virtual System.Boolean GetAttributeStringData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int32[] dataArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000865
        public virtual System.Boolean GetAttributeFloatData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Single[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000866
        public virtual System.Boolean GetAttributeFloatArrayData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Single[]& data, System.Int32 dataLength, System.Int32[]& sizesArray, System.Int32 start, System.Int32 sizesLength) { }
        // RVA: 0x012081B0  token: 0x6000867
        public virtual System.Boolean GetAttributeFloat64Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Double[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000868
        public virtual System.Boolean GetAttributeIntData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int32[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000869
        public virtual System.Boolean GetAttributeUInt8Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Byte[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600086A
        public virtual System.Boolean GetAttributeInt8Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.SByte[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600086B
        public virtual System.Boolean GetAttributeInt16Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int16[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600086C
        public virtual System.Boolean GetAttributeInt64Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int64[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600086D
        public virtual System.Boolean GetGroupNames(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GroupType groupType, System.Int32[]& names, System.Int32 count) { }
        // RVA: 0x012081B0  token: 0x600086E
        public virtual System.Boolean GetGroupMembership(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Boolean& membershipArrayAllEqual, System.Int32[] membershipArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x04DA2B20  token: 0x600086F
        public virtual System.Boolean GetGroupCountOnPackedInstancePart(System.Int32 nodeID, System.Int32 partID, System.Int32& pointGroupCount, System.Int32& primitiveGroupCount) { }
        // RVA: 0x012081B0  token: 0x6000870
        public virtual System.Boolean GetGroupNamesOnPackedInstancePart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.Int32[]& groupNamesArray, System.Int32 groupCount) { }
        // RVA: 0x012081B0  token: 0x6000871
        public virtual System.Boolean GetGroupMembershipOnPackedInstancePart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Boolean& membershipArrayAllEqual, System.Int32[] membershipArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000872
        public virtual System.Boolean GetInstancedPartIds(System.Int32 nodeID, System.Int32 partID, System.Int32[] instancedPartsArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000873
        public virtual System.Boolean GetInstancerPartTransforms(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transformsArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000874
        public virtual System.Boolean GetInstanceTransformsOnPart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transformsArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000875
        public virtual System.Boolean GetInstancedObjectIds(System.Int32 nodeID, System.Int32[] instanced_node_id_array, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000876
        public virtual System.Boolean GetFaceCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] faceCounts, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000877
        public virtual System.Boolean GetFaceCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] faceCounts, System.Int32 start, System.Int32 length, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x6000878
        public virtual System.Boolean GetVertexList(System.Int32 nodeID, System.Int32 partID, System.Int32[] vertexList, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000879
        public virtual System.Boolean GetBoxInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_BoxInfo& boxInfo) { }
        // RVA: 0x012081B0  token: 0x600087A
        public virtual System.Boolean GetSphereInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_SphereInfo& sphereInfo) { }
        // RVA: 0x012081B0  token: 0x600087B
        public virtual System.Boolean GetCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_CurveInfo& curveInfo) { }
        // RVA: 0x012081B0  token: 0x600087C
        public virtual System.Boolean GetCurveCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] counts, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600087D
        public virtual System.Boolean GetCurveOrders(System.Int32 nodeID, System.Int32 partID, System.Int32[] orders, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600087E
        public virtual System.Boolean GetCurveKnots(System.Int32 nodeID, System.Int32 partID, System.Single[] knots, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600087F
        public virtual System.Boolean GetInputCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_InputCurveInfo& inputCurveInfo) { }
        // RVA: 0x012081B0  token: 0x6000880
        public virtual System.Boolean SetPartInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }
        // RVA: 0x012081B0  token: 0x6000881
        public virtual System.Boolean SetFaceCount(System.Int32 nodeID, System.Int32 partID, System.Int32[] faceCounts, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000882
        public virtual System.Boolean SetVertexList(System.Int32 nodeID, System.Int32 partID, System.Int32[] vertexList, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000883
        public virtual System.Boolean SetAttributeIntData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Int32[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000884
        public virtual System.Boolean SetAttributeInt8Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.SByte[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000885
        public virtual System.Boolean SetAttributeInt16Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Int16[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000886
        public virtual System.Boolean SetAttributeInt64Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Int64[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000887
        public virtual System.Boolean SetAttributeFloatData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Single[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000888
        public virtual System.Boolean SetAttributeFloatArrayData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Single[] data, System.Int32 dataLength, System.Int32[] sizesArray, System.Int32 start, System.Int32 sizesLength) { }
        // RVA: 0x012081B0  token: 0x6000889
        public virtual System.Boolean SetAttributeStringData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.String[] data, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600088A
        public virtual System.Boolean AddAttribute(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo) { }
        // RVA: 0x012081B0  token: 0x600088B
        public virtual System.Boolean AddGroup(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName) { }
        // RVA: 0x012081B0  token: 0x600088C
        public virtual System.Boolean DeleteGroup(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName) { }
        // RVA: 0x012081B0  token: 0x600088D
        public virtual System.Boolean SetGroupMembership(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Int32[] membershipArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x600088E
        public virtual System.Boolean CommitGeo(System.Int32 nodeID) { }
        // RVA: 0x012081B0  token: 0x600088F
        public virtual System.Boolean RevertGeo(System.Int32 nodeID) { }
        // RVA: 0x012081B0  token: 0x6000890
        public virtual System.Boolean SetCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_CurveInfo& curveInfo) { }
        // RVA: 0x012081B0  token: 0x6000891
        public virtual System.Boolean SetCurveCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] counts, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000892
        public virtual System.Boolean SetCurveOrders(System.Int32 nodeID, System.Int32 partID, System.Int32[] orders, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000893
        public virtual System.Boolean SetCurveKnots(System.Int32 nodeID, System.Int32 partID, System.Single[] knots, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000894
        public virtual System.Boolean SetInputCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_InputCurveInfo& curveInfo) { }
        // RVA: 0x012081B0  token: 0x6000895
        public virtual System.Boolean SetInputCurvePositions(System.Int32 nodeID, System.Int32 partID, System.Single[] positionsArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x6000896
        public virtual System.Boolean SetInputCurvePositionsRotationsScales(System.Int32 nodeID, System.Int32 partID, System.Single[] positionsArray, System.Int32 start, System.Int32 length, System.Single[] rotationsArray, System.Int32 rotationsStart, System.Int32 rotationsLength, System.Single[] scalesArray, System.Int32 scalesStart, System.Int32 scalesLength) { }
        // RVA: 0x012081B0  token: 0x6000897
        public virtual System.Boolean GetMaterialOnPart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_MaterialInfo& materialInfo) { }
        // RVA: 0x012081B0  token: 0x6000898
        public virtual System.Boolean GetMaterialNodeIDsOnFaces(System.Int32 nodeID, System.Int32 partID, System.Boolean& bSingleFaceMaterial, System.Int32[] materialNodeIDs, System.Int32 faceCount) { }
        // RVA: 0x012081B0  token: 0x6000899
        public virtual System.Boolean GetMaterialInfo(System.Int32 materialNodeID, HoudiniEngineUnity.HAPI_MaterialInfo& materialInfo, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x600089A
        public virtual System.Boolean GetImageInfo(System.Int32 materialNodeID, HoudiniEngineUnity.HAPI_ImageInfo& imageInfo) { }
        // RVA: 0x012081B0  token: 0x600089B
        public virtual System.Boolean SetImageInfo(System.Int32 materialNodeID, HoudiniEngineUnity.HAPI_ImageInfo& imageInfo) { }
        // RVA: 0x012081B0  token: 0x600089C
        public virtual System.Boolean RenderTextureToImage(System.Int32 materialNodeID, System.Int32 parmID, System.Boolean bLogError) { }
        // RVA: 0x012081B0  token: 0x600089D
        public virtual System.Boolean RenderCOPToImage(System.Int32 copNodeID) { }
        // RVA: 0x09D911E4  token: 0x600089E
        public virtual System.Boolean ExtractImageToMemory(System.Int32 nodeID, System.String fileFormat, System.String imagePlanes, System.Byte[]& buffer) { }
        // RVA: 0x09D91228  token: 0x600089F
        public virtual System.Boolean GetImagePlanes(System.Int32 nodeID, System.Int32[] imagePlanes, System.Int32 numImagePlanes) { }
        // RVA: 0x09D911C8  token: 0x60008A0
        public virtual System.Boolean ExtractImageToFile(System.Int32 nodeID, System.String fileFormat, System.String imagePlanes, System.String destinationFolderPath, System.String& destinationFilePath) { }
        // RVA: 0x012081B0  token: 0x60008A1
        public virtual System.Boolean GetParams(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parmInfos, System.Int32 start, System.Int32 length) { }
        // RVA: 0x04DA2B30  token: 0x60008A2
        public virtual System.Boolean GetParmTagName(System.Int32 nodeID, System.Int32 parmID, System.Int32 tagIndex, System.Int32& tagName) { }
        // RVA: 0x04DA2B30  token: 0x60008A3
        public virtual System.Boolean GetParmTagValue(System.Int32 nodeID, System.Int32 parmID, System.String tagName, System.Int32& tagValue) { }
        // RVA: 0x012081B0  token: 0x60008A4
        public virtual System.Boolean ParmHasTag(System.Int32 nodeID, System.Int32 parmID, System.String tagName, System.Boolean& hasTag) { }
        // RVA: 0x012081B0  token: 0x60008A5
        public virtual System.Boolean GetParamIntValues(System.Int32 nodeID, System.Int32[] values, System.Int32 start, System.Int32 length) { }
        // RVA: 0x04DA2B30  token: 0x60008A6
        public virtual System.Boolean GetParamIntValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Int32& value) { }
        // RVA: 0x012081B0  token: 0x60008A7
        public virtual System.Boolean GetParamFloatValues(System.Int32 nodeID, System.Single[] values, System.Int32 start, System.Int32 length) { }
        // RVA: 0x04DA2B30  token: 0x60008A8
        public virtual System.Boolean GetParamFloatValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Single& value) { }
        // RVA: 0x012081B0  token: 0x60008A9
        public virtual System.Boolean GetParamStringValues(System.Int32 nodeID, System.Int32[] values, System.Int32 start, System.Int32 length) { }
        // RVA: 0x04DA2B30  token: 0x60008AA
        public virtual System.Boolean GetParamStringValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Int32& value) { }
        // RVA: 0x04DA2B40  token: 0x60008AB
        public virtual System.Boolean GetParamNodeValue(System.Int32 nodeID, System.String paramName, System.Int32& nodeValue) { }
        // RVA: 0x012081B0  token: 0x60008AC
        public virtual System.Boolean GetParamChoiceValues(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmChoiceInfo[] values, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x60008AD
        public virtual System.Boolean SetParamIntValues(System.Int32 nodeID, System.Int32[]& values, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x60008AE
        public virtual System.Boolean SetParamIntValue(System.Int32 nodeID, System.String paramName, System.Int32 index, System.Int32 value) { }
        // RVA: 0x012081B0  token: 0x60008AF
        public virtual System.Boolean SetParamFloatValues(System.Int32 nodeID, System.Single[]& values, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x60008B0
        public virtual System.Boolean SetParamFloatValue(System.Int32 nodeID, System.String paramName, System.Int32 index, System.Single value) { }
        // RVA: 0x012081B0  token: 0x60008B1
        public virtual System.Boolean SetParamStringValue(System.Int32 nodeID, System.String strValue, System.Int32 parmID, System.Int32 index) { }
        // RVA: 0x012081B0  token: 0x60008B2
        public virtual System.Boolean SetParamStringValue(System.Int32 nodeID, System.String parmName, System.String parmValue, System.Int32 index) { }
        // RVA: 0x012081B0  token: 0x60008B3
        public virtual System.Boolean SetParamNodeValue(System.Int32 nodeID, System.String paramName, System.Int32 nodeValueID) { }
        // RVA: 0x012081B0  token: 0x60008B4
        public virtual System.Boolean InsertMultiparmInstance(System.Int32 nodeID, System.Int32 parmID, System.Int32 instancePosition) { }
        // RVA: 0x012081B0  token: 0x60008B5
        public virtual System.Boolean RemoveMultiParmInstance(System.Int32 nodeID, System.Int32 parmID, System.Int32 instancePosition) { }
        // RVA: 0x012081B0  token: 0x60008B6
        public virtual System.Boolean GetParmWithTag(System.Int32 nodeID, System.String tagName, System.Int32& parmID) { }
        // RVA: 0x012081B0  token: 0x60008B7
        public virtual System.Boolean RevertParmToDefault(System.Int32 nodeID, System.String parm_name, System.Int32 index) { }
        // RVA: 0x012081B0  token: 0x60008B8
        public virtual System.Boolean RevertParmToDefaults(System.Int32 nodeID, System.String parm_name) { }
        // RVA: 0x04DA2B40  token: 0x60008B9
        public virtual System.Boolean GetParmIDFromName(System.Int32 nodeID, System.String parmName, System.Int32& parmID) { }
        // RVA: 0x04DA2B50  token: 0x60008BA
        public virtual System.Boolean GetParmStringValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Boolean evaluate, System.Int32& value) { }
        // RVA: 0x04DA2AE0  token: 0x60008BB
        public virtual System.Boolean CreateInputNode(System.Int32& nodeID, System.String name) { }
        // RVA: 0x04DA2AE0  token: 0x60008BC
        public virtual System.Boolean CreateInputCurveNode(System.Int32& nodeID, System.String name) { }
        // RVA: 0x04DA2AA0  token: 0x60008BD
        public virtual System.Boolean CreateHeightFieldInput(System.Int32 parentNodeID, System.String name, System.Int32 xSize, System.Int32 ySize, System.Single voxelSize, HoudiniEngineUnity.HAPI_HeightFieldSampling sampling, System.Int32& heightfieldNodeID, System.Int32& heightNodeID, System.Int32& maskNodeID, System.Int32& mergeNodeID) { }
        // RVA: 0x04DA2AD0  token: 0x60008BE
        public virtual System.Boolean CreateHeightfieldInputVolumeNode(System.Int32 parentNodeID, System.Int32& newNodeID, System.String name, System.Int32 xSize, System.Int32 ySize, System.Single voxelSize) { }
        // RVA: 0x09D91260  token: 0x60008BF
        public virtual System.Boolean GetPreset(System.Int32 nodeID, System.Byte[]& presetData) { }
        // RVA: 0x012081B0  token: 0x60008C0
        public virtual System.Boolean SetPreset(System.Int32 nodeID, System.Byte[] presetData) { }
        // RVA: 0x012081B0  token: 0x60008C1
        public virtual System.Boolean GetVolumeInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo) { }
        // RVA: 0x012081B0  token: 0x60008C2
        public virtual System.Boolean GetHeightFieldData(System.Int32 nodeID, System.Int32 partID, System.Single[] valuesArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x60008C3
        public virtual System.Boolean SetVolumeInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo) { }
        // RVA: 0x09D915B4  token: 0x60008C4
        public virtual System.Boolean SetVolumeTileFloatData(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_VolumeTileInfo& tileInfo, System.Single[] valuesArray, System.Int32 length) { }
        // RVA: 0x04DA2B60  token: 0x60008C5
        public virtual System.Boolean GetVolumeBounds(System.Int32 nodeID, System.Int32 partID, System.Single& x_min, System.Single& y_min, System.Single& z_min, System.Single& x_max, System.Single& y_max, System.Single& z_max, System.Single& x_center, System.Single& y_center, System.Single& z_center) { }
        // RVA: 0x012081B0  token: 0x60008C6
        public virtual System.Boolean SetHeightFieldData(System.Int32 nodeID, System.Int32 partID, System.String name, System.Single[] valuesArray, System.Int32 start, System.Int32 length) { }
        // RVA: 0x04DA2B00  token: 0x60008C7
        public virtual System.Boolean GetActiveCacheCount(System.Int32& activeCacheCount) { }
        // RVA: 0x012081B0  token: 0x60008C8
        public virtual System.Boolean GetActiveCacheNames(System.Int32[] cacheNamesArray, System.Int32 activeCacheCount) { }
        // RVA: 0x04DA2B10  token: 0x60008C9
        public virtual System.Boolean GetCacheProperty(System.String cacheName, HoudiniEngineUnity.HAPI_CacheProperty cacheProperty, System.Int32& propertyValue) { }
        // RVA: 0x012081B0  token: 0x60008CA
        public virtual System.Boolean SetCacheProperty(System.String cacheName, HoudiniEngineUnity.HAPI_CacheProperty cacheProperty, System.Int32 propertyValue) { }
        // RVA: 0x012081B0  token: 0x60008CB
        public virtual System.Boolean SaveGeoToFile(System.Int32 nodeID, System.String fileName) { }
        // RVA: 0x012081B0  token: 0x60008CC
        public virtual System.Boolean LoadGeoFromFile(System.Int32 nodeID, System.String file_name) { }
        // RVA: 0x012081B0  token: 0x60008CD
        public virtual System.Boolean SaveNodeToFile(System.Int32 nodeID, System.String fileName) { }
        // RVA: 0x04DA2AF0  token: 0x60008CE
        public virtual System.Boolean LoadNodeFromFile(System.String file_name, System.Int32 parentNodeID, System.String nodeLabel, System.Boolean cook_on_load, System.Int32& newNodeID) { }
        // RVA: 0x04DA2B10  token: 0x60008CF
        public virtual System.Boolean GetGeoSize(System.Int32 nodeID, System.String format, System.Int32& size) { }
        // RVA: 0x012081B0  token: 0x60008D0
        public virtual System.Boolean GetHandleInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_HandleInfo[] handleInfos, System.Int32 start, System.Int32 length) { }
        // RVA: 0x012081B0  token: 0x60008D1
        public virtual System.Boolean GetHandleBindingInfo(System.Int32 nodeID, System.Int32 handleIndex, HoudiniEngineUnity.HAPI_HandleBindingInfo[] handleBindingInfos, System.Int32 start, System.Int32 length) { }
        // RVA: 0x04DA2A80  token: 0x60008D2
        public virtual System.Boolean ConvertTransform(HoudiniEngineUnity.HAPI_TransformEuler& inTransform, HoudiniEngineUnity.HAPI_RSTOrder RSTOrder, HoudiniEngineUnity.HAPI_XYZOrder ROTOrder, HoudiniEngineUnity.HAPI_TransformEuler& outTransform) { }
        // RVA: 0x04DA2B50  token: 0x60008D3
        public virtual System.Boolean GetTotalCookCount(System.Int32 nodeID, System.Int32 nodeTypeFilter, System.Int32 nodeFlagFilter, System.Boolean includeChildren, System.Int32& count) { }
        // RVA: 0x012081B0  token: 0x60008D4
        public virtual System.Boolean SetSessionSync(System.Boolean enable) { }
        // RVA: 0x012081B0  token: 0x60008D5
        public virtual System.Boolean GetViewport(HoudiniEngineUnity.HAPI_Viewport& viewport) { }
        // RVA: 0x012081B0  token: 0x60008D6
        public virtual System.Boolean SetViewport(HoudiniEngineUnity.HAPI_Viewport& viewport) { }
        // RVA: 0x012081B0  token: 0x60008D7
        public virtual System.Boolean GetSessionSyncInfo(HoudiniEngineUnity.HAPI_SessionSyncInfo& syncInfo) { }
        // RVA: 0x012081B0  token: 0x60008D8
        public virtual System.Boolean SetSessionSyncInfo(HoudiniEngineUnity.HAPI_SessionSyncInfo& syncInfo) { }
        // RVA: 0x012081B0  token: 0x60008D9
        public virtual System.Boolean SetNodeDisplay(System.Int32 node_id, System.Int32 onOff) { }

    }

    // TypeToken: 0x20000FA  // size: 0x14
    public sealed struct SessionConnectionState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.SessionConnectionState NOT_CONNECTED;  // const
        public static HoudiniEngineUnity.SessionConnectionState CONNECTED;  // const
        public static HoudiniEngineUnity.SessionConnectionState FAILED_TO_CONNECT;  // const

    }

    // TypeToken: 0x20000FB  // size: 0x14
    public sealed struct SessionMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.SessionMode Socket;  // const
        public static HoudiniEngineUnity.SessionMode Pipe;  // const
        public static HoudiniEngineUnity.SessionMode SharedMemory;  // const

    }

    // TypeToken: 0x20000FC  // size: 0x68
    public sealed class HEU_SessionData
    {
        // Fields
        public static System.Int64 INVALID_SESSION_ID;  // static @ 0x0
        public HoudiniEngineUnity.HAPI_Session _HAPISession;  // 0x10
        private System.Int32 _serverProcessID;  // 0x20
        private System.Boolean _initialized;  // 0x24
        private System.String _pipeName;  // 0x28
        private System.Int32 _port;  // 0x30
        private System.String _sharedMemoryName;  // 0x38
        private HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType _sharedMemoryBufferType;  // 0x40
        private System.Int32 _sharedMemoryBufferSize;  // 0x44
        private System.String _sessionClassType;  // 0x48
        private System.Boolean _isDefaultSession;  // 0x50
        private HoudiniEngineUnity.HEU_SessionSyncData _sessionSync;  // 0x58
        private HoudiniEngineUnity.SessionConnectionState _connectionState;  // 0x60
        private HoudiniEngineUnity.SessionMode _sessionMode;  // 0x64

        // Properties
        System.Int64 SessionID { get; /* RVA: 0x09D91784 */ set; /* RVA: 0x011EC9D0 */ }
        System.Int32 ProcessID { get; /* RVA: 0x04DA2BB0 */ set; /* RVA: 0x04D86330 */ }
        HoudiniEngineUnity.HAPI_SessionType SessionType { get; /* RVA: 0x011EC580 */ set; /* RVA: 0x014F51F0 */ }
        System.Boolean IsInitialized { get; /* RVA: 0x012081B0 */ set; /* RVA: 0x04D86880 */ }
        System.Boolean IsValidSessionID { get; /* RVA: 0x012081B0 */ }
        System.String PipeName { get; /* RVA: 0x09D916E0 */ set; /* RVA: 0x02FDB880 */ }
        System.Int32 Port { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        System.String SharedMemoryName { get; /* RVA: 0x09D917D4 */ set; /* RVA: 0x046A91B0 */ }
        HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType SharedMemoryBufferType { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04D86610 */ }
        System.Int32 SharedMemoryBufferSize { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x04D86580 */ }
        System.Type SessionClassType { get; /* RVA: 0x09D9170C */ set; /* RVA: 0x09D91800 */ }
        System.Boolean IsDefaultSession { get; /* RVA: 0x04D86800 */ set; /* RVA: 0x04D86840 */ }
        System.Boolean IsSessionSync { get; /* RVA: 0x09B00064 */ }
        HoudiniEngineUnity.SessionConnectionState ThisConnectionMode { get; /* RVA: 0x04D86790 */ set; /* RVA: 0x04D86D50 */ }
        HoudiniEngineUnity.SessionMode ThisSessionMode { get; /* RVA: 0x04D88110 */ set; /* RVA: 0x04D88120 */ }

        // Methods
        // RVA: 0x09D91634  token: 0x60008DA
        public HoudiniEngineUnity.HEU_SessionSyncData GetOrCreateSessionSync() { }
        // RVA: 0x02E56440  token: 0x60008DB
        public HoudiniEngineUnity.HEU_SessionSyncData GetSessionSync() { }
        // RVA: 0x036E5590  token: 0x60008DC
        public System.Void SetSessionSync(HoudiniEngineUnity.HEU_SessionSyncData syncData) { }
        // RVA: 0x04D91890  token: 0x60008F9
        public System.Void .ctor() { }
        // RVA: 0x09D916A4  token: 0x60008FA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FD  // size: 0x30
    public class HEU_SessionHAPI : HoudiniEngineUnity.HEU_SessionBase
    {
        // Methods
        // RVA: 0x04DA2BA0  token: 0x60008FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FE  // size: 0x10
    public static class HEU_SessionManager
    {
        // Fields
        private static HoudiniEngineUnity.HEU_SessionBase _defaultSession;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.Int64,HoudiniEngineUnity.HEU_SessionBase> _sessionMap;  // static @ 0x8
        public static HoudiniEngineUnity.HEU_SessionManager.CreateSessionFromTypeDelegate _createSessionFromTypeDelegate;  // static @ 0x10

        // Methods
        // RVA: 0x09D92078  token: 0x60008FC
        public static HoudiniEngineUnity.HEU_SessionBase CreateSessionObject() { }
        // RVA: 0x09D92034  token: 0x60008FD
        public static HoudiniEngineUnity.HEU_SessionBase CreateSessionFromType(System.Type type) { }
        // RVA: 0x09D92474  token: 0x60008FE
        public static HoudiniEngineUnity.HEU_SessionBase GetDefaultSession() { }
        // RVA: 0x09D936CC  token: 0x60008FF
        public static System.Void RegisterSession(System.Int64 sessionID, HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D93910  token: 0x6000900
        public static System.Void UnregisterSession(System.Int64 sessionID) { }
        // RVA: 0x09D92BA4  token: 0x6000901
        public static HoudiniEngineUnity.HEU_SessionBase GetSessionWithID(System.Int64 sessionID) { }
        // RVA: 0x09D937F4  token: 0x6000902
        public static System.Void SaveAllSessionData() { }
        // RVA: 0x09D92FBC  token: 0x6000903
        public static System.Void LoadAllSessionData() { }
        // RVA: 0x09D92E34  token: 0x6000904
        private static System.Void InternalValidateSceneAssets() { }
        // RVA: 0x09D92674  token: 0x6000905
        public static HoudiniEngineUnity.HEU_SessionBase GetOrCreateDefaultSession(System.Boolean bNotifyUserError) { }
        // RVA: 0x09D91FB4  token: 0x6000906
        public static System.Boolean CreateInProcessSession() { }
        // RVA: 0x09D921BC  token: 0x6000907
        public static System.Boolean CreateThriftSocketSession(System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean logError) { }
        // RVA: 0x09D920BC  token: 0x6000908
        public static System.Boolean CreateThriftPipeSession(System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean logError) { }
        // RVA: 0x09D9213C  token: 0x6000909
        public static System.Boolean CreateThriftSharedMemorySession(System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean logError) { }
        // RVA: 0x09D91F34  token: 0x600090A
        public static System.Boolean CreateCustomSession() { }
        // RVA: 0x09D91EB4  token: 0x600090B
        public static System.Boolean ConnectThriftSocketSession(System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout) { }
        // RVA: 0x09D91DB4  token: 0x600090C
        public static System.Boolean ConnectThriftPipeSession(System.String pipeName, System.Boolean autoClose, System.Single timeout) { }
        // RVA: 0x09D91E34  token: 0x600090D
        public static System.Boolean ConnectThriftSharedMemorySession(System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout) { }
        // RVA: 0x09D93668  token: 0x600090E
        public static System.Void RecreateDefaultSessionData() { }
        // RVA: 0x09D91D38  token: 0x600090F
        public static System.Boolean ConnectSessionSyncUsingThriftSocket(System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean logError) { }
        // RVA: 0x09D91C40  token: 0x6000910
        public static System.Boolean ConnectSessionSyncUsingThriftPipe(System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean logError) { }
        // RVA: 0x09D91CBC  token: 0x6000911
        public static System.Boolean ConnectSessionSyncUsingThriftSharedMemory(System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean logError) { }
        // RVA: 0x09D92DCC  token: 0x6000912
        public static System.Boolean InitializeDefaultSession() { }
        // RVA: 0x09D91BA4  token: 0x6000913
        public static System.Boolean CloseDefaultSession() { }
        // RVA: 0x09D91900  token: 0x6000914
        public static System.Void CloseAllSessions() { }
        // RVA: 0x09D91830  token: 0x6000915
        private static System.Void CheckAndCloseExistingSession() { }
        // RVA: 0x09D92B00  token: 0x6000916
        public static HoudiniEngineUnity.HEU_SessionData GetSessionData() { }
        // RVA: 0x09D92B44  token: 0x6000917
        public static System.String GetSessionInfo() { }
        // RVA: 0x09D93460  token: 0x6000918
        public static System.Boolean LoadStoredDefaultSession() { }
        // RVA: 0x09D93750  token: 0x6000919
        public static System.Boolean RestartSession() { }
        // RVA: 0x09D93988  token: 0x600091A
        public static System.Boolean ValidatePluginSession(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D9255C  token: 0x600091B
        public static System.String GetLastSessionError() { }
        // RVA: 0x09D918C4  token: 0x600091C
        public static System.Boolean CheckVersionMatch() { }
        // RVA: 0x02FFF600  token: 0x600091D
        public static System.Boolean ClearConnectionError() { }
        // RVA: 0x09D92404  token: 0x600091E
        public static System.String GetConnectionError(System.Boolean clear) { }
        // RVA: 0x09D92F40  token: 0x600091F
        public static System.Boolean IsHARSProcessRunning(System.Int32 processID) { }
        // RVA: 0x09D933EC  token: 0x6000920
        public static System.Boolean LoadSessionFromHIP(System.Boolean bCookNodes, HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D9389C  token: 0x6000921
        public static System.Boolean SaveSessionToHIP(System.Boolean bLockNodes, HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x0464E630  token: 0x6000922
        public static System.String GetHoudiniPathOnMacOS(System.String houdiniPath) { }
        // RVA: 0x09D934D0  token: 0x6000923
        public static System.Boolean OpenHoudini(System.String args) { }
        // RVA: 0x09D93584  token: 0x6000924
        public static System.Boolean OpenSessionInHoudini(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D92430  token: 0x6000925
        public static HoudiniEngineUnity.HAPI_License GetCurrentLicense(System.Boolean bLogError) { }
        // RVA: 0x09D92CFC  token: 0x6000926
        public static System.String GetString(System.Int32 stringHandle, HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D92C24  token: 0x6000927
        public static System.String[] GetStringValuesFromStringIndices(System.Int32[] strIndices) { }
        // RVA: 0x09D9251C  token: 0x6000928
        public static System.String[] GetGroupNames(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.Boolean isInstanced) { }
        // RVA: 0x09D924E4  token: 0x6000929
        public static System.Boolean GetGroupMembership(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Int32[]& membership, System.Boolean isInstanced) { }
        // RVA: 0x09D92614  token: 0x600092A
        public static System.String GetNodeName(System.Int32 nodeID, HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D925B4  token: 0x600092B
        public static System.Boolean GetNodeInputName(System.Int32 nodeID, System.Int32 inputIndex, System.String& inputName) { }
        // RVA: 0x09D9223C  token: 0x600092C
        public static System.Boolean GetComposedChildNodeList(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 parentNodeID, System.Int32 nodeTypeFilter, System.Int32 nodeFlagFilter, System.Boolean bRecursive, System.Int32[]& childNodeIDs, System.Boolean bLogIfError) { }
        // RVA: 0x09D92288  token: 0x600092D
        public static System.Boolean GetComposedObjectListMemorySafe(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ObjectInfo[] objectInfos, System.Int32 start, System.Int32 length) { }
        // RVA: 0x09D92344  token: 0x600092E
        public static System.Boolean GetComposedObjectTransformsMemorySafe(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transforms, System.Int32 start, System.Int32 length) { }
        // RVA: 0x09D92D64  token: 0x600092F
        public static System.String GetUniqueMaterialShopName(System.Int32 assetID, System.Int32 materialID) { }
        // RVA: 0x09D939D0  token: 0x6000930
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000100  // size: 0x10
    public static class HEU_SessionPDG
    {
    }

    // TypeToken: 0x2000101  // size: 0x68
    public class HEU_SessionSyncData
    {
        // Fields
        private System.Int32 _status;  // 0x10
        public System.Single _timeLastUpdate;  // 0x14
        public System.Single _timeStartConnection;  // 0x18
        public System.String _newNodeName;  // 0x20
        public System.Int32 _nodeTypeIndex;  // 0x28
        public System.Boolean _validForConnection;  // 0x2c
        public HoudiniEngineUnity.HAPI_Viewport _viewportHAPI;  // 0x30
        public HoudiniEngineUnity.HAPI_Viewport _viewportLocal;  // 0x48
        public System.Boolean _viewportJustUpdated;  // 0x60
        public HoudiniEngineUnity.HAPI_SessionSyncInfo _syncInfo;  // 0x61

        // Properties
        HoudiniEngineUnity.HEU_SessionSyncData.Status SyncStatus { get; /* RVA: 0x09D93B1C */ set; /* RVA: 0x04DA2BC0 */ }

        // Methods
        // RVA: 0x09D93A64  token: 0x6000937
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000103  // size: 0x98
    public class HEU_AssetSync : HoudiniEngineUnity.HEU_BaseSync
    {
        // Fields
        public HoudiniEngineUnity.HEU_AssetSync.AssetSyncCallback _onAssetLoaded;  // 0x88
        public System.String _assetPath;  // 0x90

        // Methods
        // RVA: 0x041E1670  token: 0x6000938
        private System.Void OnEnable() { }
        // RVA: 0x09D85394  token: 0x6000939
        private System.Void OnDestroy() { }
        // RVA: 0x09D852BC  token: 0x600093A
        public System.Void InitializeAsset(HoudiniEngineUnity.HEU_SessionBase session, System.String assetPath, System.String nodeName, UnityEngine.Transform parent, UnityEngine.Vector3 startPosition) { }
        // RVA: 0x09D85260  token: 0x600093B
        private HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo CreateThreadedTask() { }
        // RVA: 0x09D85404  token: 0x600093C
        public System.Void SetLoadCallback(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallback callback) { }
        // RVA: 0x09D85458  token: 0x600093D
        protected virtual System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D8539C  token: 0x600093E
        public virtual System.Void OnLoadComplete(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData) { }
        // RVA: 0x09D853D8  token: 0x600093F
        public virtual System.Void Resync() { }
        // RVA: 0x09D854D0  token: 0x6000940
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000105  // size: 0x28
    public class HEU_SyncedEventData
    {
        // Fields
        public System.Boolean CookSuccess;  // 0x10
        public HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData TopNodeData;  // 0x18
        public HoudiniEngineUnity.HEU_BaseSync OutputObject;  // 0x20

        // Methods
        // RVA: 0x09D93B2C  token: 0x6000945
        public System.Void .ctor(System.Boolean bSuccess, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData bTopNodeData, HoudiniEngineUnity.HEU_BaseSync bOutputObject) { }

    }

    // TypeToken: 0x2000106  // size: 0x88
    public class HEU_BaseSync : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Int32 _cookNodeID;  // 0x18
        public System.Int64 _sessionID;  // 0x20
        public System.String _nodeName;  // 0x28
        public System.Boolean _initialized;  // 0x30
        public System.Boolean _syncing;  // 0x31
        public System.Boolean _deleteParent;  // 0x32
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> _generatedOutputs;  // 0x38
        public System.String _outputCacheDirectory;  // 0x40
        public System.Collections.Generic.List<System.String> _outputCacheFilePaths;  // 0x48
        public HoudiniEngineUnity.HEU_GenerateOptions _generateOptions;  // 0x50
        public System.Text.StringBuilder _log;  // 0x58
        public System.Text.StringBuilder _error;  // 0x60
        public System.Boolean _sessionSyncAutoCook;  // 0x68
        private System.Action<HoudiniEngineUnity.HEU_SyncedEventData> _onSynced;  // 0x70
        protected HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo _loadTask;  // 0x78
        protected System.Int32 _totalCookCount;  // 0x80
        protected System.Boolean _firstSyncComplete;  // 0x84

        // Properties
        System.Action<HoudiniEngineUnity.HEU_SyncedEventData> OnSynced { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000946
        private System.Void Awake() { }
        // RVA: 0x09D85394  token: 0x6000947
        private System.Void OnDestroy() { }
        // RVA: 0x09D8639C  token: 0x6000948
        public virtual System.Void DeleteSessionData() { }
        // RVA: 0x09D863C0  token: 0x6000949
        public virtual System.Void DestroyGeneratedData() { }
        // RVA: 0x04DA29F0  token: 0x600094A
        protected virtual System.Void Initialize() { }
        // RVA: 0x09D892E4  token: 0x600094B
        public virtual HoudiniEngineUnity.HEU_SessionBase GetHoudiniSession(System.Boolean bCreateIfNotFound) { }
        // RVA: 0x09D893E8  token: 0x600094C
        private System.Int32 GetParentNodeID(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D89418  token: 0x600094D
        public System.Void Log(System.String msg) { }
        // RVA: 0x09D860E8  token: 0x600094E
        public System.Void ClearLog() { }
        // RVA: 0x09D86608  token: 0x600094F
        public System.Void Error(System.String error) { }
        // RVA: 0x09D89404  token: 0x6000950
        public System.Boolean IsLoaded() { }
        // RVA: 0x09D89954  token: 0x6000951
        public virtual System.Void StartSync() { }
        // RVA: 0x041E1670  token: 0x6000952
        protected virtual System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D89A18  token: 0x6000953
        public virtual System.Void StopSync() { }
        // RVA: 0x09D89748  token: 0x6000954
        public virtual System.Void Resync() { }
        // RVA: 0x09D85DB0  token: 0x6000955
        public virtual System.Void Bake() { }
        // RVA: 0x09D89A74  token: 0x6000956
        public virtual System.Void Unload() { }
        // RVA: 0x09D8969C  token: 0x6000957
        public virtual System.Void Reset() { }
        // RVA: 0x09D894DC  token: 0x6000958
        public virtual System.Void OnLoadComplete(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData) { }
        // RVA: 0x09D87EE0  token: 0x6000959
        public virtual System.Void GenerateObjects(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData) { }
        // RVA: 0x09D866D8  token: 0x600095A
        public virtual System.Void GenerateGeometry(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData, System.Int32 objIndex) { }
        // RVA: 0x09D895E0  token: 0x600095B
        public System.Void OnStopped(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData) { }
        // RVA: 0x09D87F60  token: 0x600095C
        private System.Void GenerateTerrain(System.Int32 cookNodeId, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume> terrainBuffers) { }
        // RVA: 0x09D87948  token: 0x600095D
        private System.Void GenerateMesh(System.Int32 cookNodeId, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferMesh> meshBuffers) { }
        // RVA: 0x09D86628  token: 0x600095E
        private System.Void GenerateAllInstancers(System.Int32 cookNodeId, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferInstancer> instancerBuffers, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData) { }
        // RVA: 0x09D867E0  token: 0x600095F
        private System.Void GenerateInstancer(System.Int32 cookNodeId, HoudiniEngineUnity.HEU_LoadBufferInstancer instancerBuffer, System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_LoadBufferBase> idBuffersMap) { }
        // RVA: 0x09D872B0  token: 0x6000960
        private System.Void GenerateInstancesFromNodeIDs(System.Int32 cookNodeId, HoudiniEngineUnity.HEU_LoadBufferInstancer instancerBuffer, System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_LoadBufferBase> idBuffersMap, UnityEngine.Transform instanceRootTransform) { }
        // RVA: 0x09D86A78  token: 0x6000961
        private System.Void GenerateInstancesFromAssetPaths(HoudiniEngineUnity.HEU_LoadBufferInstancer instancerBuffer, UnityEngine.Transform instanceRootTransform) { }
        // RVA: 0x09D86218  token: 0x6000962
        private System.Void CreateNewInstanceFromObject(UnityEngine.GameObject assetSourceGO, System.Int32 instanceIndex, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HAPI_Transform& hapiTransform, System.String[] instancePrefixes, System.String instanceName, UnityEngine.GameObject collisionSourceGO) { }
        // RVA: 0x09D85D38  token: 0x6000963
        private System.Void ApplyAttributeModifiersOnGameObjectOutput(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partId, UnityEngine.GameObject& go) { }
        // RVA: 0x09D863C8  token: 0x6000964
        private System.Void DestroyOutputs() { }
        // RVA: 0x09D89774  token: 0x6000965
        private System.Void SetOutputVisiblity(HoudiniEngineUnity.HEU_LoadBufferBase buffer) { }
        // RVA: 0x09D8938C  token: 0x6000966
        private System.String GetOutputCacheDirectory() { }
        // RVA: 0x042B4AE0  token: 0x6000967
        public System.Void SetOutputCacheDirectory(System.String directory) { }
        // RVA: 0x09D85CB0  token: 0x6000968
        private System.Void AddGeneratedOutputFilePath(System.String path) { }
        // RVA: 0x041E1670  token: 0x6000969
        public virtual System.Void SyncUpdate() { }
        // RVA: 0x09D89AE8  token: 0x600096C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000107  // size: 0x15
    public sealed struct HEU_GenerateOptions
    {
        // Fields
        public System.Boolean _generateUVs;  // 0x10
        public System.Boolean _generateTangents;  // 0x11
        public System.Boolean _generateNormals;  // 0x12
        public System.Boolean _useLODGroups;  // 0x13
        public System.Boolean _splitPoints;  // 0x14

    }

    // TypeToken: 0x2000108  // size: 0x90
    public class HEU_GeoSync : HoudiniEngineUnity.HEU_BaseSync
    {
        // Fields
        public System.String _filePath;  // 0x88

        // Methods
        // RVA: 0x09D89C70  token: 0x600096D
        protected virtual System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D89D24  token: 0x600096E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000109  // size: 0x90
    public class HEU_NodeSync : HoudiniEngineUnity.HEU_BaseSync
    {
        // Fields
        public System.String _nodeSaveFilePath;  // 0x88

        // Methods
        // RVA: 0x041E1670  token: 0x600096F
        private System.Void OnEnable() { }
        // RVA: 0x09D85394  token: 0x6000970
        private System.Void OnDestroy() { }
        // RVA: 0x09D8C72C  token: 0x6000971
        public System.Void InitializeFromHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.String nodeName, System.String filePath) { }
        // RVA: 0x09D8C830  token: 0x6000972
        protected virtual System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09D8C7B4  token: 0x6000973
        public System.Boolean SaveNodeToFile(System.String filePath) { }
        // RVA: 0x09D8C688  token: 0x6000974
        public static System.Void CreateNodeSync(HoudiniEngineUnity.HEU_SessionBase session, System.String opName, System.String nodeNabel) { }
        // RVA: 0x09D853D8  token: 0x6000975
        public virtual System.Void Resync() { }
        // RVA: 0x09D8C8E0  token: 0x6000976
        public virtual System.Void SyncUpdate() { }
        // RVA: 0x09D854D0  token: 0x6000977
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010A  // size: 0x30
    public abstract class HEU_Task
    {
        // Fields
        public HoudiniEngineUnity.HEU_Task.TaskStatus _status;  // 0x10
        public HoudiniEngineUnity.HEU_Task.TaskResult _result;  // 0x14
        private System.Guid _guid;  // 0x18
        public HoudiniEngineUnity.HEU_Task.TaskCallback _taskCompletedDelegate;  // 0x28

        // Properties
        System.Guid TaskGuid { get; /* RVA: 0x04D890C0 */ }

        // Methods
        // RVA: 0x09D94464  token: 0x6000979
        public System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x600097A
        public virtual System.Void DoTask() { }
        // RVA: 0x041E1670  token: 0x600097B
        public virtual System.Void UpdateTask() { }
        // RVA: -1  // abstract  token: 0x600097C
        public virtual System.Void KillTask() { }
        // RVA: -1  // abstract  token: 0x600097D
        public virtual System.Void CompleteTask(HoudiniEngineUnity.HEU_Task.TaskResult result) { }

    }

    // TypeToken: 0x200010E  // size: 0x60
    public class HEU_AssetTask : HoudiniEngineUnity.HEU_Task
    {
        // Fields
        public HoudiniEngineUnity.HEU_AssetTask.BuildType _buildType;  // 0x30
        public HoudiniEngineUnity.HEU_HoudiniAsset _asset;  // 0x38
        public System.String _assetPath;  // 0x40
        public UnityEngine.Vector3 _position;  // 0x48
        public System.Boolean _buildResult;  // 0x54
        public System.Int64 _forceSessionID;  // 0x58

        // Methods
        // RVA: 0x09D85A54  token: 0x6000982
        public HoudiniEngineUnity.HEU_SessionBase GetTaskSession() { }
        // RVA: 0x09D856B8  token: 0x6000983
        public virtual System.Void DoTask() { }
        // RVA: 0x09D85AE4  token: 0x6000984
        public virtual System.Void KillTask() { }
        // RVA: 0x09D854D8  token: 0x6000985
        public virtual System.Void CompleteTask(HoudiniEngineUnity.HEU_Task.TaskResult result) { }
        // RVA: 0x09D8565C  token: 0x6000986
        private System.Void CookCompletedCallback(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean bSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputs) { }
        // RVA: 0x09D85634  token: 0x6000987
        private System.Void CookCompletedCallback(HoudiniEngineUnity.HEU_CookedEventData cookedEventData) { }
        // RVA: 0x09D85634  token: 0x6000988
        private System.Void CookCompletedCallback(HoudiniEngineUnity.HEU_ReloadEventData reloadEventData) { }
        // RVA: 0x09D85C40  token: 0x6000989
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000110  // size: 0x10
    public class HEU_TaskManager
    {
        // Fields
        private static System.Collections.Generic.List<HoudiniEngineUnity.HEU_Task> _tasks;  // static @ 0x0
        private static System.Collections.Generic.List<HoudiniEngineUnity.HEU_Task> _pendingAdd;  // static @ 0x8
        private static System.Collections.Generic.List<HoudiniEngineUnity.HEU_Task> _pendingRemove;  // static @ 0x10

        // Methods
        // RVA: 0x09D94338  token: 0x600098A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600098B
        private static System.Void Update() { }
        // RVA: 0x09D93F48  token: 0x600098C
        public static HoudiniEngineUnity.HEU_Task GetTask(System.Guid taskGuid) { }
        // RVA: 0x09D93DF8  token: 0x600098D
        public static System.Void AddTask(HoudiniEngineUnity.HEU_Task task) { }
        // RVA: 0x09D94110  token: 0x600098E
        public static System.Void KillTask(HoudiniEngineUnity.HEU_Task task, System.Boolean bRemove) { }
        // RVA: 0x09D941D0  token: 0x600098F
        public static System.Void KillTask(System.Guid taskGuid, System.Boolean bRemove) { }
        // RVA: 0x09D9424C  token: 0x6000990
        public static System.Void RemoveTask(HoudiniEngineUnity.HEU_Task task) { }
        // RVA: 0x09D93F14  token: 0x6000991
        public static System.Void ExecuteTask(HoudiniEngineUnity.HEU_Task task) { }
        // RVA: 0x09D93EF0  token: 0x6000992
        public static System.Void CompleteTask(HoudiniEngineUnity.HEU_Task task, HoudiniEngineUnity.HEU_Task.TaskResult result) { }
        // RVA: 0x09D940C0  token: 0x6000993
        private static System.Void InternalCompleteTask(HoudiniEngineUnity.HEU_Task task) { }
        // RVA: 0x041E1670  token: 0x6000994
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000111  // size: 0x30
    public class HEU_LoadBufferBase
    {
        // Fields
        public System.Int32 _id;  // 0x10
        public System.String _name;  // 0x18
        public System.Boolean _bInstanced;  // 0x20
        public System.Boolean _bInstancer;  // 0x21
        public HoudiniEngineUnity.HEU_GeneratedOutput _generatedOutput;  // 0x28

        // Methods
        // RVA: 0x09DA3330  token: 0x6000995
        public System.Void InitializeBuffer(System.Int32 id, System.String name, System.Boolean bInstanced, System.Boolean bInstancer) { }
        // RVA: 0x041E1670  token: 0x6000996
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000112  // size: 0x48
    public class HEU_LoadBufferMesh : HoudiniEngineUnity.HEU_LoadBufferBase
    {
        // Fields
        public HoudiniEngineUnity.HEU_GenerateGeoCache _geoCache;  // 0x30
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup> _LODGroupMeshes;  // 0x38
        public System.Int32 _defaultMaterialKey;  // 0x40
        public System.Boolean _bGenerateUVs;  // 0x44
        public System.Boolean _bGenerateTangents;  // 0x45
        public System.Boolean _bGenerateNormals;  // 0x46
        public System.Boolean _bPartInstanced;  // 0x47

        // Methods
        // RVA: 0x041E1670  token: 0x6000997
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000113  // size: 0xA8
    public class HEU_LoadBufferVolume : HoudiniEngineUnity.HEU_LoadBufferBase
    {
        // Fields
        public System.Int32 _tileIndex;  // 0x30
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolumeLayer> _splatLayers;  // 0x38
        public System.Int32 _heightMapWidth;  // 0x40
        public System.Int32 _heightMapHeight;  // 0x44
        public System.Single[,] _heightMap;  // 0x48
        public System.Single[,,] _splatMaps;  // 0x50
        public System.Single _terrainSizeX;  // 0x58
        public System.Single _terrainSizeY;  // 0x5c
        public System.Single _heightRange;  // 0x60
        public UnityEngine.Vector3 _position;  // 0x64
        public System.String _terrainDataPath;  // 0x70
        public System.String _terrainDataExportPath;  // 0x78
        public HoudiniEngineUnity.HEU_VolumeScatterTrees _scatterTrees;  // 0x80
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_DetailPrototype> _detailPrototypes;  // 0x88
        public System.Collections.Generic.List<System.Int32[,]> _detailMaps;  // 0x90
        public HoudiniEngineUnity.HEU_DetailProperties _detailProperties;  // 0x98
        public System.String _specifiedTerrainMaterialName;  // 0xa0

        // Methods
        // RVA: 0x09DA3394  token: 0x6000998
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000114  // size: 0xD8
    public class HEU_LoadBufferVolumeLayer
    {
        // Fields
        public System.String _layerName;  // 0x10
        public System.Int32 _partID;  // 0x18
        public System.Int32 _heightMapWidth;  // 0x1c
        public System.Int32 _heightMapHeight;  // 0x20
        public System.Single _strength;  // 0x24
        public System.String _diffuseTexturePath;  // 0x28
        public System.String _maskTexturePath;  // 0x30
        public System.Single _metallic;  // 0x38
        public System.String _normalTexturePath;  // 0x40
        public System.Single _normalScale;  // 0x48
        public System.Single _smoothness;  // 0x4c
        public UnityEngine.Color _specularColor;  // 0x50
        public UnityEngine.Vector2 _tileSize;  // 0x60
        public UnityEngine.Vector2 _tileOffset;  // 0x68
        public System.Boolean _uiExpanded;  // 0x70
        public System.Int32 _tile;  // 0x74
        public System.Single[] _normalizedHeights;  // 0x78
        public System.Single _minHeight;  // 0x80
        public System.Single _maxHeight;  // 0x84
        public System.Single _heightRange;  // 0x88
        public System.Single _terrainSizeX;  // 0x8c
        public System.Single _terrainSizeY;  // 0x90
        public UnityEngine.Vector3 _position;  // 0x94
        public UnityEngine.Vector3 _minBounds;  // 0xa0
        public UnityEngine.Vector3 _maxBounds;  // 0xac
        public UnityEngine.Vector3 _center;  // 0xb8
        public System.String _layerPath;  // 0xc8
        public System.Boolean _hasLayerAttributes;  // 0xd0
        public HoudiniEngineUnity.HFLayerType _layerType;  // 0xd4

        // Methods
        // RVA: 0x09DA335C  token: 0x6000999
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000115  // size: 0x58
    public class HEU_LoadBufferInstancer : HoudiniEngineUnity.HEU_LoadBufferBase
    {
        // Fields
        public HoudiniEngineUnity.HAPI_Transform[] _instanceTransforms;  // 0x30
        public System.String[] _instancePrefixes;  // 0x38
        public System.Int32[] _instanceNodeIDs;  // 0x40
        public System.String[] _assetPaths;  // 0x48
        public System.String[] _collisionAssetPaths;  // 0x50

        // Methods
        // RVA: 0x041E1670  token: 0x600099A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000116  // size: 0x38
    public class HEU_ThreadedTask
    {
        // Fields
        private System.Boolean _isComplete;  // 0x10
        private System.Boolean _isActive;  // 0x11
        private System.Boolean _stopRequested;  // 0x12
        private System.Object _lockHandle;  // 0x18
        private System.Threading.Thread _thread;  // 0x20
        private System.Threading.ThreadPriority _priority;  // 0x28
        private System.Boolean _isBackground;  // 0x2c
        protected System.String _name;  // 0x30

        // Properties
        System.String TaskName { get; /* RVA: 0x011F36E0 */ }
        System.Boolean IsComplete { get; /* RVA: 0x09DA8030 */ set; /* RVA: 0x09DA8234 */ }
        System.Boolean IsActive { get; /* RVA: 0x09DA7F84 */ set; /* RVA: 0x09DA8188 */ }
        System.Boolean StopRequested { get; /* RVA: 0x09DA80DC */ set; /* RVA: 0x09DA82E0 */ }
        System.Threading.ThreadPriority Priority { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Boolean IsBackground { get; /* RVA: 0x04D869F0 */ set; /* RVA: 0x04D86A00 */ }

        // Methods
        // RVA: 0x09DA7D48  token: 0x600099B
        public virtual System.Void Start() { }
        // RVA: 0x09DA7E98  token: 0x600099C
        public virtual System.Void Stop() { }
        // RVA: 0x09DA7C58  token: 0x600099D
        public virtual System.Void Abort() { }
        // RVA: 0x09DA7C88  token: 0x600099E
        public virtual System.Void Reset() { }
        // RVA: 0x09DA7ED8  token: 0x600099F
        public virtual System.Void Update() { }
        // RVA: 0x041E1670  token: 0x60009A0
        protected virtual System.Void DoWork() { }
        // RVA: 0x041E1670  token: 0x60009A1
        protected virtual System.Void OnComplete() { }
        // RVA: 0x041E1670  token: 0x60009A2
        protected virtual System.Void OnStopped() { }
        // RVA: 0x041E1670  token: 0x60009A3
        protected virtual System.Void CleanUp() { }
        // RVA: 0x09DA7D20  token: 0x60009A4
        private System.Void Run() { }
        // RVA: 0x09DA7F30  token: 0x60009B0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000117  // size: 0x78
    public class HEU_ThreadedTaskLoadGeo : HoudiniEngineUnity.HEU_ThreadedTask
    {
        // Fields
        private HoudiniEngineUnity.HEU_BaseSync _ownerSync;  // 0x38
        private HoudiniEngineUnity.HEU_SessionBase _session;  // 0x40
        private HoudiniEngineUnity.HEU_GenerateOptions _generateOptions;  // 0x48
        protected System.String _assetCachePath;  // 0x50
        private HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadType _loadType;  // 0x58
        private System.String _filePath;  // 0x60
        private HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData _loadData;  // 0x68
        private HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallback _loadCallback;  // 0x70

        // Methods
        // RVA: 0x09DA7ABC  token: 0x60009B1
        public System.Void SetupLoad(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadType loadType, System.Int32 cookNodeID, System.String name, System.String filePath) { }
        // RVA: 0x09DA7A8C  token: 0x60009B2
        public System.Void SetupLoadNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, System.Int32 cookNodeID, System.String name) { }
        // RVA: 0x09DA7A5C  token: 0x60009B3
        public System.Void SetupLoadFile(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, System.Int32 cookNodeID, System.String filePath) { }
        // RVA: 0x09DA7A28  token: 0x60009B4
        public System.Void SetupLoadAsset(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, System.String assetPath, System.String name) { }
        // RVA: 0x063AD760  token: 0x60009B5
        public System.Void SetLoadCallback(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallback loadCallback) { }
        // RVA: 0x09DA4560  token: 0x60009B6
        protected virtual System.Void DoWork() { }
        // RVA: 0x09DA4188  token: 0x60009B7
        protected virtual System.Boolean CookNode(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 cookNodeID) { }
        // RVA: 0x09DA75F4  token: 0x60009B8
        protected virtual System.Boolean LoadObjectBuffers(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo& objectInfo) { }
        // RVA: 0x09DA7304  token: 0x60009B9
        protected virtual System.Boolean LoadNodeBuffer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadObject loadObject) { }
        // RVA: 0x09DA3C48  token: 0x60009BA
        protected virtual System.Void BuildBufferIDsMap(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData) { }
        // RVA: 0x09DA43F8  token: 0x60009BB
        public virtual System.Boolean DoFileLoad() { }
        // RVA: 0x09DA42F8  token: 0x60009BC
        public virtual System.Boolean DoAssetLoad() { }
        // RVA: 0x09DA77C8  token: 0x60009BD
        private System.Boolean QueryParts(System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& meshParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& volumeParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& instancerParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& curveParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& scatterInstancerParts) { }
        // RVA: 0x09DA76D0  token: 0x60009BE
        protected virtual System.Void OnComplete() { }
        // RVA: 0x09DA7758  token: 0x60009BF
        protected virtual System.Void OnStopped() { }
        // RVA: 0x09B105DC  token: 0x60009C0
        protected virtual System.Void CleanUp() { }
        // RVA: 0x09DA426C  token: 0x60009C1
        private System.String CreateLogString(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus status, System.String logStr) { }
        // RVA: 0x09DA3B20  token: 0x60009C2
        private System.Void AppendLog(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus status, System.String logStr) { }
        // RVA: 0x09DA7888  token: 0x60009C3
        private System.Void SetLog(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus status, System.String logStr) { }
        // RVA: 0x09DA4214  token: 0x60009C4
        private System.Boolean CreateFileNode(System.Int32& fileNodeID) { }
        // RVA: 0x09DA6E54  token: 0x60009C5
        public virtual System.Int32 GetCookNodeID() { }
        // RVA: 0x09DA6E70  token: 0x60009C6
        private System.Int32 GetDisplayNodeID(System.Int32 objNodeID) { }
        // RVA: 0x09DA784C  token: 0x60009C7
        private System.Boolean SetFileParm(System.Int32 fileNodeID, System.String filePath) { }
        // RVA: 0x09DA7C04  token: 0x60009C8
        private System.Void Sleep() { }
        // RVA: 0x09DA5380  token: 0x60009C9
        public System.Boolean GenerateTerrainBuffers(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> volumeParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> scatterInstancerParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume>& volumeBuffers) { }
        // RVA: 0x09DA7648  token: 0x60009CA
        private System.Void LoadStringFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.String& strValue) { }
        // RVA: 0x09DA6FB4  token: 0x60009CB
        private System.Void LoadFloatFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Single& floatValue) { }
        // RVA: 0x09DA70A4  token: 0x60009CC
        private System.Void LoadLayerColorFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Color& colorValue) { }
        // RVA: 0x09DA71F4  token: 0x60009CD
        private System.Void LoadLayerVector2FromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Vector2& vectorValue) { }
        // RVA: 0x09DA49A0  token: 0x60009CE
        public System.Boolean GenerateMeshBuffers(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> meshParts, System.Boolean bSplitPoints, System.Boolean bUseLODGroups, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadObject loadObject, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferMesh>& meshBuffers) { }
        // RVA: 0x09DA4600  token: 0x60009CF
        public System.Boolean GenerateInstancerBuffers(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> instancerParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferInstancer>& instancerBuffers) { }
        // RVA: 0x09DA4F60  token: 0x60009D0
        private HoudiniEngineUnity.HEU_LoadBufferInstancer GeneratePartsInstancerBuffer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String partName, HoudiniEngineUnity.HAPI_PartInfo partInfo) { }
        // RVA: 0x09DA5238  token: 0x60009D1
        private HoudiniEngineUnity.HEU_LoadBufferInstancer GeneratePointAttributeInstancerBuffer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String partName, HoudiniEngineUnity.HAPI_PartInfo partInfo) { }
        // RVA: 0x09DA6E8C  token: 0x60009D2
        public static HoudiniEngineUnity.HEU_LoadBufferVolume GetLoadBufferVolumeFromTileIndex(System.Int32 tileIndex, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume> buffers) { }
        // RVA: 0x09DA6FA8  token: 0x60009D3
        public static System.String GetValidAssetCacheFolderPath(System.String name) { }
        // RVA: 0x09DA7C10  token: 0x60009D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011E  // size: 0x28
    public class HEU_ThreadManager
    {
        // Fields
        private static HoudiniEngineUnity.HEU_ThreadManager _instance;  // static @ 0x0
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTask> _tasks;  // 0x10
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTask> _pendingAdd;  // 0x18
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTask> _pendingRemove;  // 0x20

        // Properties
        HoudiniEngineUnity.HEU_ThreadManager Instance { get; /* RVA: 0x09DA3AE8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60009DC
        private static System.Void CreateInstance() { }
        // RVA: 0x09DA35D0  token: 0x60009DD
        protected virtual System.Void Finalize() { }
        // RVA: 0x041E1670  token: 0x60009DE
        public System.Void Register() { }
        // RVA: 0x041E1670  token: 0x60009DF
        public System.Void Unregister() { }
        // RVA: 0x09DA36BC  token: 0x60009E0
        public System.Void Update() { }
        // RVA: 0x09DA3534  token: 0x60009E1
        public System.Void AddTask(HoudiniEngineUnity.HEU_ThreadedTask task) { }
        // RVA: 0x09DA3620  token: 0x60009E2
        public System.Void RemoveTask(HoudiniEngineUnity.HEU_ThreadedTask task) { }
        // RVA: 0x09DA3A0C  token: 0x60009E3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011F  // size: 0x30
    public class HEU_CookLogs
    {
        // Fields
        private static HoudiniEngineUnity.HEU_CookLogs _instance;  // static @ 0x0
        private System.Text.StringBuilder _cookLogs;  // 0x10
        private System.Int32 _currentCookLogCount;  // 0x18
        private static System.Int32 MAX_COOK_LOG_COUNT;  // const
        private System.String _lastLogStr;  // 0x20
        private System.Boolean _uniqueStrOnly;  // 0x28
        public static System.Int64 MaxLogSize;  // const

        // Properties
        HoudiniEngineUnity.HEU_CookLogs Instance { get; /* RVA: 0x09D94C14 */ }

        // Methods
        // RVA: 0x09043210  token: 0x60009E5
        public System.String GetCookLogString() { }
        // RVA: 0x09D94664  token: 0x60009E6
        public System.Void AppendCookLog(System.String logStr) { }
        // RVA: 0x09D94780  token: 0x60009E7
        public System.Void ClearCookLog() { }
        // RVA: 0x09D94874  token: 0x60009E8
        public System.String GetCookLogFilePath() { }
        // RVA: 0x09D947EC  token: 0x60009E9
        public System.Void DeleteCookingFile() { }
        // RVA: 0x09D949AC  token: 0x60009EA
        public System.Void WriteToLogFile(System.String logStr, System.Boolean checkLastLogStr) { }
        // RVA: 0x09D94924  token: 0x60009EB
        public System.Int64 GetFileSizeOfLogFile() { }
        // RVA: 0x09D94B88  token: 0x60009EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000120  // size: 0x10
    public static class HEU_EditorUtility
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60009ED
        public static System.Void MarkSceneDirty() { }
        // RVA: 0x041E1670  token: 0x60009EE
        public static System.Void SelectObject(UnityEngine.GameObject gameObject) { }
        // RVA: 0x041E1670  token: 0x60009EF
        public static System.Void SelectObjects(UnityEngine.GameObject[] gameObjects) { }
        // RVA: 0x09D954F8  token: 0x60009F0
        public static UnityEngine.Vector3 GetSelectedObjectsMeanPosition() { }
        // RVA: 0x04DA2BE0  token: 0x60009F1
        public static UnityEngine.Matrix4x4 GetSelectedObjectsMeanTransform() { }
        // RVA: 0x09D957E4  token: 0x60009F2
        public static UnityEngine.GameObject SaveAsPrefabAsset(System.String path, UnityEngine.GameObject go) { }
        // RVA: 0x012081B0  token: 0x60009F3
        public static System.Boolean PrefabIsAddedComponentOverride(UnityEngine.Component comp) { }
        // RVA: 0x012081B0  token: 0x60009F4
        public static System.Boolean IsEditorPlaying() { }
        // RVA: 0x09D957AC  token: 0x60009F5
        public static UnityEngine.GameObject ReplacePrefab(UnityEngine.GameObject go, UnityEngine.Object targetPrefab, HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptions heuOptions) { }
        // RVA: 0x09D95698  token: 0x60009F6
        public static System.Boolean IsPrefabInstance(UnityEngine.GameObject go) { }
        // RVA: 0x09D95660  token: 0x60009F7
        public static System.Boolean IsPrefabAsset(UnityEngine.GameObject go) { }
        // RVA: 0x012081B0  token: 0x60009F8
        public static System.Boolean IsEditingInPrefabMode(UnityEngine.GameObject obj) { }
        // RVA: 0x09D955F0  token: 0x60009F9
        public static System.Boolean IsDisconnectedPrefabInstance(UnityEngine.GameObject go) { }
        // RVA: 0x09D95448  token: 0x60009FA
        public static UnityEngine.Object GetPrefabAsset(UnityEngine.GameObject go) { }
        // RVA: 0x09D95410  token: 0x60009FB
        public static System.String GetPrefabAssetPath(UnityEngine.Object obj) { }
        // RVA: 0x09D94EDC  token: 0x60009FC
        public static System.Void DisconnectPrefabInstance(UnityEngine.GameObject instance) { }
        // RVA: 0x09D955B8  token: 0x60009FD
        public static UnityEngine.Object InstantiatePrefab(UnityEngine.GameObject prefabOriginal) { }
        // RVA: 0x09D95548  token: 0x60009FE
        public static UnityEngine.GameObject InstantiateGameObject(UnityEngine.GameObject sourceGameObject, UnityEngine.Transform parentTransform, System.Boolean instantiateInWorldSpace, System.Boolean bRegisterUndo) { }
        // RVA: -1  // generic def  token: 0x60009FF
        public static UnityEngine.Component AddComponent(UnityEngine.GameObject target, System.Boolean bRegisterUndo) { }
        // RVA: 0x041E1670  token: 0x6000A00
        public static System.Void UndoRecordObject(UnityEngine.Object objectToUndo, System.String name) { }
        // RVA: 0x041E1670  token: 0x6000A01
        public static System.Void UndoCollapseCurrentGroup() { }
        // RVA: 0x09D94DC8  token: 0x6000A02
        public static UnityEngine.Object[] CollectDependencies(UnityEngine.Object obj) { }
        // RVA: 0x09D95628  token: 0x6000A03
        public static System.Boolean IsPersistant(UnityEngine.Object obj) { }
        // RVA: 0x09D95510  token: 0x6000A04
        public static System.String GetUniqueNameForSibling(UnityEngine.Transform parentTransform, System.String name) { }
        // RVA: 0x09D94FD8  token: 0x6000A05
        public static System.Void DisplayProgressBar(System.String title, System.String info, System.Single progress) { }
        // RVA: 0x09D94D94  token: 0x6000A06
        public static System.Void ClearProgressBar() { }
        // RVA: 0x04D85B20  token: 0x6000A07
        public static System.Boolean IsEditorNotInPlayModeAndNotGoingToPlayMode() { }
        // RVA: 0x09D94F10  token: 0x6000A08
        public static System.Boolean DisplayDialog(System.String title, System.String message, System.String ok, System.String cancel) { }
        // RVA: 0x09D94F68  token: 0x6000A09
        public static System.Boolean DisplayErrorDialog(System.String title, System.String message, System.String ok, System.String cancel) { }
        // RVA: 0x041E1670  token: 0x6000A0A
        public static System.Void RevealInFinder(System.String path) { }
        // RVA: 0x041E1670  token: 0x6000A0B
        public static System.Void SetObjectDirtyForEditorUpdate(UnityEngine.Object obj) { }
        // RVA: 0x041E1670  token: 0x6000A0C
        public static System.Void SetStatic(UnityEngine.GameObject go, System.Boolean bStatic, System.Boolean bIncludeChildren) { }
        // RVA: 0x041E1670  token: 0x6000A0D
        public static System.Void SetIsHidden(UnityEngine.GameObject go, System.Boolean isHidden, System.Boolean bIncludeChildren) { }
        // RVA: 0x011EC580  token: 0x6000A0E
        public static UnityEngine.GameObject[] GetSelectedObjects() { }
        // RVA: 0x09D95480  token: 0x6000A0F
        public static HoudiniEngineUnity.HEU_HoudiniAssetRoot[] GetSelectedAssetRoots() { }
        // RVA: 0x09D95364  token: 0x6000A10
        public static HoudiniEngineUnity.HEU_HoudiniAssetRoot[] GetAllAssetRoots() { }
        // RVA: 0x09D94EC0  token: 0x6000A11
        public static System.Void CookSelected() { }
        // RVA: 0x09D94E00  token: 0x6000A12
        public static System.Void CookAll() { }
        // RVA: 0x09D94E1C  token: 0x6000A13
        public static System.Void CookAssets(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets) { }
        // RVA: 0x09D95790  token: 0x6000A14
        public static System.Void RebuildSelected() { }
        // RVA: 0x09D956D0  token: 0x6000A15
        public static System.Void RebuildAll() { }
        // RVA: 0x09D956EC  token: 0x6000A16
        public static System.Void RebuildAssets(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets) { }
        // RVA: 0x09D94D78  token: 0x6000A17
        public static System.Void BakeAndReplaceSelectedInScene() { }
        // RVA: 0x09D94CA4  token: 0x6000A18
        public static System.Void BakeAndReplaceAllInScene() { }
        // RVA: 0x09D94CC0  token: 0x6000A19
        public static System.Void BakeAndReplaceAssets(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets) { }
        // RVA: 0x09D95348  token: 0x6000A1A
        public static System.Void ExportSelectedAssetsToGeoFiles() { }
        // RVA: 0x09D95044  token: 0x6000A1B
        public static System.Void ExportAllAssetsToGeoFiles() { }
        // RVA: 0x09D95060  token: 0x6000A1C
        public static System.Void ExportAssetsToGeoFiles(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets) { }
        // RVA: 0x09D9500C  token: 0x6000A1D
        public static System.String EditorSaveFolderPanel(System.String title, System.String folder, System.String defaultName) { }
        // RVA: 0x041E1670  token: 0x6000A1E
        public static System.Void QuerySelectedMeshTopology() { }
        // RVA: 0x09D953E4  token: 0x6000A1F
        public static System.String GetObjectParentFolder(UnityEngine.GameObject parentObject, System.Collections.Generic.HashSet<UnityEngine.Material> generatedMaterials) { }
        // RVA: 0x09D953B8  token: 0x6000A20
        private static System.String GetObjectParentFolderHelper(System.Int32 instanceID) { }
        // RVA: 0x041E1670  token: 0x6000A21
        public static System.Void RepaintScene() { }
        // RVA: 0x041E1670  token: 0x6000A22
        public static System.Void SetTextureToNormalMap(System.String filename) { }
        // RVA: 0x02FFF600  token: 0x6000A23
        public static System.Boolean ReleasedMouse() { }

    }

    // TypeToken: 0x2000122  // size: 0x10
    public static class HEU_Extensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000A24
        public static System.Collections.Generic.List<R> Map(System.Collections.Generic.IEnumerable<T> self, System.Func<T,R> selector) { }
        // RVA: -1  // generic def  token: 0x6000A25
        public static T Reduce(System.Collections.Generic.IEnumerable<T> self, System.Func<T,T,T> func) { }
        // RVA: -1  // generic def  token: 0x6000A26
        public static System.Collections.Generic.List<T> Filter(System.Collections.Generic.IEnumerable<T> self, System.Func<T,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x6000A27
        public static System.Boolean IsValidIndex(System.Collections.Generic.List<T> self, System.Int32 index) { }
        // RVA: -1  // generic def  token: 0x6000A28
        public static System.Boolean IsEquivalentList(System.Collections.Generic.List<T> self, System.Collections.Generic.List<T> other) { }
        // RVA: 0x09D9581C  token: 0x6000A29
        public static System.Boolean ApproximatelyEquals(UnityEngine.Quaternion quatA, UnityEngine.Quaternion value) { }
        // RVA: -1  // generic def  token: 0x6000A2A
        public static System.Collections.Generic.List<U> ConvertList(System.Collections.Generic.List<T> self) { }
        // RVA: -1  // generic def  token: 0x6000A2B
        public static System.Collections.Generic.List<HoudiniEngineUnity.IEquivable<T>> ConvertListToEquivable(System.Collections.Generic.List<T> self) { }
        // RVA: -1  // generic def  token: 0x6000A2C
        public static HoudiniEngineUnity.IEquivable<T>[] ConvertArrayToEquivable(T[] self) { }
        // RVA: 0x06935188  token: 0x6000A2D
        public static System.Boolean ApproximatelyEquals(System.Single self, System.Single other, System.Single epsilon) { }
        // RVA: 0x09D95868  token: 0x6000A2E
        public static System.Byte[] AsByteArray(System.String self) { }
        // RVA: 0x09D958D4  token: 0x6000A2F
        public static System.String AsString(System.Byte[] buffer) { }
        // RVA: 0x04DA2C20  token: 0x6000A30
        public static UnityEngine.Vector3 SwapXAndY(UnityEngine.Vector3 self) { }
        // RVA: 0x04DA2C50  token: 0x6000A31
        public static UnityEngine.Vector3 SwapXAndZ(UnityEngine.Vector3 self) { }
        // RVA: 0x04DA2C70  token: 0x6000A32
        public static UnityEngine.Vector3 SwapYAndZ(UnityEngine.Vector3 self) { }
        // RVA: 0x09D6D9B0  token: 0x6000A33
        public static UnityEngine.Vector3 DecomposeToPosition(UnityEngine.Matrix4x4 self) { }
        // RVA: 0x09D95938  token: 0x6000A34
        public static UnityEngine.Quaternion DecomposeToRotation(UnityEngine.Matrix4x4 self) { }
        // RVA: 0x09D95A00  token: 0x6000A35
        public static UnityEngine.Vector3 DecomposeToScale(UnityEngine.Matrix4x4 self) { }

    }

    // TypeToken: 0x2000123  // size: 0x10
    public static class ArrayExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000A36
        public static System.Void Init(T[] array, T defaultValue) { }
        // RVA: -1  // generic def  token: 0x6000A37
        public static System.Void Init(System.Collections.Generic.List<T> array, T defaultValue) { }
        // RVA: -1  // generic def  token: 0x6000A38
        public static System.Void CopyToWithResize(T[] srcArray, T[]& destArray) { }
        // RVA: -1  // generic def  token: 0x6000A39
        public static System.Boolean IsEquivalentArray(T[] arr, T[] other) { }
        // RVA: -1  // generic def  token: 0x6000A3A
        public static System.Boolean IsNull(T[] arr) { }

    }

    // TypeToken: 0x2000124  // size: 0x10
    public static class DictionaryExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000A3B
        public static System.Void AddOrSet(System.Collections.Generic.Dictionary<T,U> dict, T key, U value) { }

    }

    // TypeToken: 0x2000125  // size: 0x60
    public sealed struct TransformData
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Quaternion rotation;  // 0x1c
        public UnityEngine.Vector3 localPosition;  // 0x2c
        public UnityEngine.Vector3 localScale;  // 0x38
        public UnityEngine.Quaternion localRotation;  // 0x44
        public UnityEngine.Transform parent;  // 0x58

        // Methods
        // RVA: 0x09DA8550  token: 0x6000A3C
        public System.Void .ctor(UnityEngine.Transform other) { }
        // RVA: 0x09DA84AC  token: 0x6000A3D
        public System.Void CopyTo(UnityEngine.Transform other, System.Boolean copyParent) { }
        // RVA: 0x09DA8404  token: 0x6000A3E
        public System.Void CopyToLocal(UnityEngine.Transform other, System.Boolean copyParent) { }

    }

    // TypeToken: 0x2000126  // size: 0x10
    public class HEU_GeneralUtility
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000A3F
        public static System.Boolean GetArray1Arg(System.Int32 arg1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func, T[] data, System.Int32 start, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000A40
        public static System.Boolean GetArray2Arg(System.Int32 arg1, ARG2 arg2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func, T[] data, System.Int32 start, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000A41
        public static System.Boolean GetArray3Arg(System.Int32 arg1, ARG2 arg2, ARG3 arg3, HoudiniEngineUnity.HEU_GeneralUtility.GetArray3ArgDel<ARG3,ARG2,T> func, T[] data, System.Int32 start, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000A42
        private static System.Boolean GetArray(System.Int32 arg1, ARG2 arg2, ARG3 arg3, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray3ArgDel<ARG3,ARG2,T> func3, T[] data, System.Int32 start, System.Int32 count, System.Int32 tupleSize) { }
        // RVA: -1  // generic def  token: 0x6000A43
        public static System.Boolean SetArray1Arg(System.Int32 arg1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func, T[] data, System.Int32 start, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000A44
        public static System.Boolean SetArray2Arg(System.Int32 arg1, ARG2 arg2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func, T[] data, System.Int32 start, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000A45
        public static System.Boolean SetArray(System.Int32 arg1, ARG2 arg2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func2, T[] data, System.Int32 start, System.Int32 count, System.Int32 tupleSize) { }
        // RVA: -1  // generic def  token: 0x6000A46
        public static System.Boolean DoArrayElementsMatch(T[] array1, T[] array2) { }
        // RVA: -1  // generic def  token: 0x6000A47
        public static System.Boolean DoArrayElementsMatch(T[] array1, System.Int32 startOffset1, T[] array2, System.Int32 startOffset2, System.Int32 length) { }
        // RVA: -1  // generic def  token: 0x6000A48
        public static System.Boolean GetAttributeArray(System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[] items, HoudiniEngineUnity.HEU_GeneralUtility.GetAttributeArrayInputFunc<T> getFunc, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000A49
        public static System.Boolean GetAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[]& data, HoudiniEngineUnity.HEU_GeneralUtility.GetAttributeArrayInputFunc<T> getFunc) { }
        // RVA: -1  // generic def  token: 0x6000A4A
        public static System.Boolean GetAttributeStrict(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[]& data, HoudiniEngineUnity.HEU_GeneralUtility.GetAttributeArrayInputFunc<T> getFunc) { }
        // RVA: 0x09D97EC0  token: 0x6000A4B
        public static System.Void GetAttributeStringDataHelper(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, System.Int32[]& data) { }
        // RVA: 0x09D97F10  token: 0x6000A4C
        public static System.String[] GetAttributeStringData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo) { }
        // RVA: 0x09D97C64  token: 0x6000A4D
        public static System.String[] GetAttributeDataAsString(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo) { }
        // RVA: -1  // generic def  token: 0x6000A4E
        public static System.Boolean SetAttributeArray(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, HoudiniEngineUnity.HEU_GeneralUtility.SetAttributeArrayFunc<T> setFunc, System.Int32 count, System.Int32 start) { }
        // RVA: -1  // generic def  token: 0x6000A4F
        public static System.Boolean SetAttribute(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, HoudiniEngineUnity.HEU_GeneralUtility.SetAttributeArrayFunc<T> setFunc) { }
        // RVA: 0x09D6BE10  token: 0x6000A50
        public static System.Boolean CheckAttributeExists(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attribName, HoudiniEngineUnity.HAPI_AttributeOwner attribOwner) { }
        // RVA: 0x09D97DC0  token: 0x6000A51
        public static System.Boolean GetAttributeInfo(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attribName, HoudiniEngineUnity.HAPI_AttributeInfo& attribInfo) { }
        // RVA: 0x09D99174  token: 0x6000A52
        public static System.Boolean HasValidInstanceAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attribName) { }
        // RVA: 0x09D9A27C  token: 0x6000A53
        public static System.Void UpdateGeneratedAttributeStore(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject go) { }
        // RVA: 0x09D9693C  token: 0x6000A54
        public static HoudiniEngineUnity.HEU_OutputAttribute CreateOutputAttributeHelper(System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo) { }
        // RVA: 0x09D969BC  token: 0x6000A55
        public static HoudiniEngineUnity.HEU_OutputAttribute CreateOutputAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo) { }
        // RVA: 0x09D967E4  token: 0x6000A56
        public static System.Void CopyWorldTransformValues(UnityEngine.Transform src, UnityEngine.Transform dest) { }
        // RVA: 0x09D95AB8  token: 0x6000A57
        public static System.Void ApplyTransformTo(UnityEngine.Transform src, UnityEngine.Transform target) { }
        // RVA: 0x09D96728  token: 0x6000A58
        public static System.Void CopyLocalTransformValues(UnityEngine.Transform src, UnityEngine.Transform dest) { }
        // RVA: 0x09D98630  token: 0x6000A59
        public static System.Collections.Generic.List<UnityEngine.GameObject> GetChildGameObjects(UnityEngine.GameObject parentGO) { }
        // RVA: 0x09D983C8  token: 0x6000A5A
        public static System.Collections.Generic.List<UnityEngine.GameObject> GetChildGameObjectsWithNamePattern(UnityEngine.GameObject parentGO, System.String pattern, System.Boolean bExclude) { }
        // RVA: 0x09D98A94  token: 0x6000A5B
        public static System.Collections.Generic.List<UnityEngine.GameObject> GetInstanceChildObjects(UnityEngine.GameObject parentGO) { }
        // RVA: 0x09D98CEC  token: 0x6000A5C
        public static System.Collections.Generic.List<UnityEngine.GameObject> GetNonInstanceChildObjects(UnityEngine.GameObject parentGO) { }
        // RVA: 0x09D98918  token: 0x6000A5D
        public static UnityEngine.GameObject GetGameObjectByName(System.Collections.Generic.List<UnityEngine.GameObject> goList, System.String name) { }
        // RVA: 0x09D98828  token: 0x6000A5E
        public static UnityEngine.GameObject GetGameObjectByNameInProjectOnly(System.String name) { }
        // RVA: 0x09D991A0  token: 0x6000A5F
        public static System.Boolean IsGameObjectInProject(UnityEngine.GameObject go) { }
        // RVA: 0x09D988E0  token: 0x6000A60
        public static UnityEngine.GameObject GetGameObjectByNameInScene(System.String name) { }
        // RVA: 0x09D98A5C  token: 0x6000A61
        public static HoudiniEngineUnity.HEU_HoudiniAssetRoot GetHDAByGameObjectNameInScene(System.String name) { }
        // RVA: -1  // generic def  token: 0x6000A62
        public static T GetOrCreateComponent(UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D96F38  token: 0x6000A63
        public static System.Void DestroyGeneratedComponents(UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D97110  token: 0x6000A64
        public static System.Void DestroyGeneratedMeshComponents(UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D97600  token: 0x6000A65
        public static System.Void DestroyTerrainComponents(UnityEngine.GameObject gameObject) { }
        // RVA: -1  // generic def  token: 0x6000A66
        public static System.Void DestroyComponent(UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D97384  token: 0x6000A67
        public static System.Void DestroyImmediate(UnityEngine.Object obj, System.Boolean bAllowDestroyingAssets, System.Boolean bRegisterUndo) { }
        // RVA: 0x09D96E6C  token: 0x6000A68
        public static System.Void DestroyBakedGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> gameObjectsToDestroy) { }
        // RVA: 0x09D96D60  token: 0x6000A69
        public static System.Void DestroyBakedGameObjectsWithEndName(System.Collections.Generic.List<UnityEngine.GameObject> gameObjectsToDestroy, System.String endName) { }
        // RVA: 0x09D973B8  token: 0x6000A6A
        public static System.Void DestroyLODGroup(UnityEngine.GameObject targetGO, System.Boolean bDontDeletePersistantResources) { }
        // RVA: 0x09D98AD4  token: 0x6000A6B
        public static System.Collections.Generic.List<UnityEngine.Transform> GetLODTransforms(UnityEngine.GameObject targetGO) { }
        // RVA: 0x09D99E1C  token: 0x6000A6C
        public static System.Void SetLODTransformValues(UnityEngine.GameObject targetGO, System.Collections.Generic.List<HoudiniEngineUnity.TransformData> transformData) { }
        // RVA: 0x09D9719C  token: 0x6000A6D
        public static System.Void DestroyGeneratedMeshMaterialsLODGroups(UnityEngine.GameObject targetGO, System.Boolean bDontDeletePersistantResources) { }
        // RVA: 0x09D97014  token: 0x6000A6E
        public static System.Void DestroyGeneratedMaterial(UnityEngine.Material material) { }
        // RVA: 0x09D97524  token: 0x6000A6F
        public static System.Void DestroyMeshCollider(UnityEngine.MeshCollider meshCollider, System.Boolean bDontDeletePersistantResources) { }
        // RVA: 0x09D99D6C  token: 0x6000A70
        public static System.Void SetGameObjectRenderVisiblity(UnityEngine.GameObject gameObject, System.Boolean bVisible) { }
        // RVA: 0x09D99C04  token: 0x6000A71
        public static System.Void SetGameObjectChildrenRenderVisibility(UnityEngine.GameObject gameObject, System.Boolean bVisible) { }
        // RVA: 0x09D99CBC  token: 0x6000A72
        public static System.Void SetGameObjectColliderState(UnityEngine.GameObject gameObject, System.Boolean bEnabled) { }
        // RVA: 0x09D99B4C  token: 0x6000A73
        public static System.Void SetGameObjectChildrenColliderState(UnityEngine.GameObject gameObject, System.Boolean bVisible) { }
        // RVA: 0x09D96360  token: 0x6000A74
        public static System.String ColorToString(UnityEngine.Color c) { }
        // RVA: 0x09D9A184  token: 0x6000A75
        public static UnityEngine.Color StringToColor(System.String colorString) { }
        // RVA: 0x02FFF600  token: 0x6000A76
        public static System.Boolean DoesUnityTagExist(System.String tagName) { }
        // RVA: 0x09D99F58  token: 0x6000A77
        public static System.Void SetLayer(UnityEngine.GameObject rootGO, System.Int32 layer, System.Boolean bIncludeChildren) { }
        // RVA: 0x09D9A0C0  token: 0x6000A78
        public static System.Void SetTag(UnityEngine.GameObject rootGO, System.String tag, System.Boolean bIncludeChildren) { }
        // RVA: 0x09D9662C  token: 0x6000A79
        public static System.Void CopyFlags(UnityEngine.GameObject srcGO, UnityEngine.GameObject dstGO, System.Boolean bIncludeChildren) { }
        // RVA: 0x09D9927C  token: 0x6000A7A
        public static System.Boolean IsMouseWithinSceneView(UnityEngine.Camera camera, UnityEngine.Vector2 mousePosition) { }
        // RVA: 0x09D99230  token: 0x6000A7B
        public static System.Boolean IsMouseOverRect(UnityEngine.Camera camera, UnityEngine.Vector2 mousePosition, UnityEngine.Rect& rect) { }
        // RVA: 0x09D99040  token: 0x6000A7C
        public static System.Type GetSystemTypeByName(System.String typeName) { }
        // RVA: 0x09D95CE8  token: 0x6000A7D
        public static System.Void AssignUnityTag(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D95BCC  token: 0x6000A7E
        public static System.Void AssignUnityLayer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D99600  token: 0x6000A7F
        public static System.Void MakeStaticIfHasAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D99078  token: 0x6000A80
        public static System.String GetUnityScriptAttributeValue(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09D982C4  token: 0x6000A81
        public static System.String GetAttributeStringValueSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner) { }
        // RVA: 0x09D97FD8  token: 0x6000A82
        public static System.String GetAttributeStringValueSingleStrict(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner) { }
        // RVA: 0x09D97CE0  token: 0x6000A83
        public static System.Boolean GetAttributeFloatSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Single& value) { }
        // RVA: 0x09D97DE0  token: 0x6000A84
        public static System.Boolean GetAttributeIntSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32& value) { }
        // RVA: 0x09D97B90  token: 0x6000A85
        public static System.Boolean GetAttributeColorSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Color& value) { }
        // RVA: 0x09D99150  token: 0x6000A86
        public static System.Boolean HasAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner) { }
        // RVA: 0x09D95DEC  token: 0x6000A87
        public static System.Void AttachScriptWithInvokeFunction(System.String scriptSet, UnityEngine.GameObject gameObject) { }
        // RVA: 0x09D991A8  token: 0x6000A88
        public static System.Boolean IsInCameraView(UnityEngine.Camera camera, UnityEngine.Vector3 point) { }
        // RVA: 0x09D97744  token: 0x6000A89
        private static System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> FindOrGenerateHandles(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_AssetInfo& assetInfo, System.Int32 assetID, System.String assetName, HoudiniEngineUnity.HEU_Parameters parameters, System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> currentHandles) { }
        // RVA: 0x041E1670  token: 0x6000A8A
        public static System.Void CopyComponents(UnityEngine.GameObject srcGO, UnityEngine.GameObject destGO) { }
        // RVA: 0x09D992EC  token: 0x6000A8B
        public static UnityEngine.Texture LoadTextureFromFile(System.String filePath) { }
        // RVA: 0x09D996F8  token: 0x6000A8C
        public static UnityEngine.Texture2D MakeTexture(System.Int32 width, System.Int32 height, UnityEngine.Color color) { }
        // RVA: 0x09D99AB4  token: 0x6000A8D
        public static System.String ReplaceFirstOccurrence(System.String srcStr, System.String searchStr, System.String replaceStr) { }
        // RVA: 0x09D9A01C  token: 0x6000A8E
        public static System.Void SetParentWithCleanTransform(UnityEngine.Transform parentTransform, UnityEngine.Transform childTransform) { }
        // RVA: 0x09D9669C  token: 0x6000A8F
        public static System.Void CopyHAPITransform(HoudiniEngineUnity.HAPI_Transform& src, HoudiniEngineUnity.HAPI_Transform& dest) { }
        // RVA: 0x09D98C14  token: 0x6000A90
        public static System.String GetMaterialAttributeValueFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09D99988  token: 0x6000A91
        public static System.Void ReplaceColliderMeshFromMeshFilter(UnityEngine.GameObject targetGO, UnityEngine.GameObject sourceColliderGO) { }
        // RVA: 0x09D99868  token: 0x6000A92
        public static System.Void ReplaceColliderMeshFromMeshCollider(UnityEngine.GameObject targetGO, UnityEngine.GameObject sourceColliderGO) { }
        // RVA: 0x09D96308  token: 0x6000A93
        public static System.Single BiLerpf(System.Single p00, System.Single p10, System.Single p01, System.Single p11, System.Single fracX, System.Single fracY) { }
        // RVA: 0x04DA2CA0  token: 0x6000A94
        public static System.Single Fractionalf(System.Single value) { }
        // RVA: 0x09D99498  token: 0x6000A95
        public static System.String LongestCommonPrefix(System.Collections.Generic.List<System.String> list) { }
        // RVA: 0x09D98FE4  token: 0x6000A96
        public static System.String GetRawOperatorName(System.String assetOpName) { }
        // RVA: 0x09D98D2C  token: 0x6000A97
        public static UnityEngine.GameObject GetPrefabFromPath(System.String path) { }
        // RVA: 0x09D964FC  token: 0x6000A98
        public static System.Void ComposeNChildren(UnityEngine.GameObject parent, System.Int32 n, System.Collections.Generic.List<UnityEngine.GameObject>& childGameObjects, System.Boolean destroyIfExists) { }
        // RVA: 0x09D96E78  token: 0x6000A99
        private static System.Void DestroyChildWithName(UnityEngine.Transform parent, System.String name) { }
        // RVA: 0x09D96C04  token: 0x6000A9A
        public static System.Void DestroyAutoGeneratedChildren(UnityEngine.GameObject parent) { }
        // RVA: 0x09D968A0  token: 0x6000A9B
        public static UnityEngine.GameObject CreateNewGameObject(System.String name) { }
        // RVA: 0x09D997F8  token: 0x6000A9C
        public static System.Void RenameGameObject(UnityEngine.GameObject obj, System.String name) { }
        // RVA: 0x041E1670  token: 0x6000A9D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012C  // size: 0x10
    public class ReverseCompare : System.Collections.IComparer
    {
        // Methods
        // RVA: 0x09DA838C  token: 0x6000AB2
        public virtual System.Int32 Compare(System.Object x, System.Object y) { }
        // RVA: 0x041E1670  token: 0x6000AB3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012D  // size: 0x2B8
    public class HEU_GenerateGeoCache
    {
        // Fields
        private System.Int32 <AssetID>k__BackingField;  // 0x10
        public HoudiniEngineUnity.HAPI_GeoInfo _geoInfo;  // 0x14
        public HoudiniEngineUnity.HAPI_PartInfo _partInfo;  // 0x38
        public System.String _partName;  // 0x68
        public System.Int32[] _vertexList;  // 0x70
        public System.Int32[] _faceCounts;  // 0x78
        public System.Int32[] _houdiniMaterialIDs;  // 0x80
        public System.Boolean _singleFaceUnityMaterial;  // 0x88
        public System.Boolean _singleFaceHoudiniMaterial;  // 0x89
        public System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_UnityMaterialInfo> _unityMaterialInfos;  // 0x90
        public HoudiniEngineUnity.HAPI_AttributeInfo _unityMaterialAttrInfo;  // 0x98
        public System.Int32[] _unityMaterialAttrName;  // 0xc0
        public System.Collections.Generic.List<System.String> _unityMaterialAttrStringsMap;  // 0xc8
        public HoudiniEngineUnity.HAPI_AttributeInfo _substanceMaterialAttrNameInfo;  // 0xd0
        public System.Int32[] _substanceMaterialAttrName;  // 0xf8
        public System.Collections.Generic.List<System.String> _substanceMaterialAttrStringsMap;  // 0x100
        public HoudiniEngineUnity.HAPI_AttributeInfo _substanceMaterialAttrIndexInfo;  // 0x108
        public System.Int32[] _substanceMaterialAttrIndex;  // 0x130
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> _inUseMaterials;  // 0x138
        public HoudiniEngineUnity.HAPI_AttributeInfo _posAttrInfo;  // 0x140
        public HoudiniEngineUnity.HAPI_AttributeInfo[] _uvsAttrInfo;  // 0x168
        public HoudiniEngineUnity.HAPI_AttributeInfo _normalAttrInfo;  // 0x170
        public HoudiniEngineUnity.HAPI_AttributeInfo _colorAttrInfo;  // 0x198
        public HoudiniEngineUnity.HAPI_AttributeInfo _alphaAttrInfo;  // 0x1c0
        public HoudiniEngineUnity.HAPI_AttributeInfo _tangentAttrInfo;  // 0x1e8
        public System.Single[] _posAttr;  // 0x210
        public System.Single[][] _uvsAttr;  // 0x218
        public System.Single[] _normalAttr;  // 0x220
        public System.Single[] _colorAttr;  // 0x228
        public System.Single[] _alphaAttr;  // 0x230
        public System.Single[] _tangentAttr;  // 0x238
        public System.String[] _groups;  // 0x240
        public System.Boolean _hasGroupGeometry;  // 0x248
        public System.Collections.Generic.Dictionary<System.String,System.Int32[]> _groupSplitVertexIndices;  // 0x250
        public System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.Int32>> _groupSplitFaceIndices;  // 0x258
        public System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.Int32>> _groupVertexOffsets;  // 0x260
        public System.Int32[] _allCollisionVertexList;  // 0x268
        public System.Int32[] _allCollisionFaceIndices;  // 0x270
        public System.Single _normalCosineThreshold;  // 0x278
        public System.Boolean _hasLODGroups;  // 0x27c
        public System.Single[] _LODTransitionValues;  // 0x280
        public System.Boolean _isMeshReadWrite;  // 0x288
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo> _colliderInfos;  // 0x290
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> _materialCache;  // 0x298
        public System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MaterialData> _materialIDToDataMap;  // 0x2a0
        public System.String _assetCacheFolderPath;  // 0x2a8
        public HoudiniEngineUnity.HEU_MeshIndexFormat _meshIndexFormat;  // 0x2b0

        // Properties
        System.Int32 GeoID { get; /* RVA: 0x04D86310 */ }
        System.Int32 PartID { get; /* RVA: 0x04D863B0 */ }
        System.Int32 AssetID { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x09DA1A44  token: 0x6000AB8
        public static HoudiniEngineUnity.HEU_GenerateGeoCache GetPopulatedGeoCache(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32 geoID, System.Int32 partID, System.Boolean bUseLODGroups, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath) { }
        // RVA: 0x09DA1AFC  token: 0x6000AB9
        public System.Void GetStringAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, System.Int32[]& stringIndices, System.Collections.Generic.List<System.String>& stringTable) { }
        // RVA: 0x09DA2378  token: 0x6000ABA
        public System.Void PopulateUnityMaterialData(HoudiniEngineUnity.HEU_SessionBase session) { }
        // RVA: 0x09DA18BC  token: 0x6000ABB
        public static System.Int32 GetMaterialKeyFromAttributeIndex(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 attributeIndex, System.String& unityMaterialName, System.String& substanceName, System.Int32& substanceIndex) { }
        // RVA: 0x09D9AD08  token: 0x6000ABC
        public static System.Void CreateMaterialInfoEntryFromAttributeIndex(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 materialAttributeIndex) { }
        // RVA: 0x09DA1FE0  token: 0x6000ABD
        public System.Boolean PopulateGeometryData(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bUseLODGroups) { }
        // RVA: 0x09DA1F00  token: 0x6000ABE
        public static System.Void ParseLODTransitionAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Single[]& LODTransitionValues) { }
        // RVA: 0x09DA2F84  token: 0x6000ABF
        public static System.Void UpdateColliders(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutputData outputData) { }
        // RVA: 0x09DA2930  token: 0x6000AC0
        public static System.Void UpdateCollider(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutputData outputData, HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo colliderInfo) { }
        // RVA: 0x09DA1580  token: 0x6000AC1
        private static System.Void GetFinalMaterialsFromComparingNewWithPrevious(UnityEngine.GameObject gameObject, UnityEngine.Material[] previousMaterials, UnityEngine.Material[] newMaterials, UnityEngine.Material[]& finalMaterials) { }
        // RVA: 0x09DA1370  token: 0x6000AC2
        public static System.Boolean GenerateMeshFromSingleGroup(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GeoGroup GeoGroup, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput, System.Int32 defaultMaterialKey, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bPartInstanced) { }
        // RVA: 0x09D9F190  token: 0x6000AC3
        public static System.Boolean GenerateLODMeshesFromGeoGroups(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup> GeoGroupMeshes, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput, System.Int32 defaultMaterialKey, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bPartInstanced) { }
        // RVA: 0x09D9FB88  token: 0x6000AC4
        public static System.Boolean GenerateMeshFromGeoGroup(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GeoGroup GeoGroup, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 defaultMaterialKey, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bPartInstanced, UnityEngine.Mesh& newMesh, UnityEngine.Material[]& newMaterials) { }
        // RVA: 0x09D9A790  token: 0x6000AC5
        public static UnityEngine.Mesh CombineQuadMeshes(System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MeshData> subMeshesMap, System.Collections.Generic.List<System.Int32> subMeshIndices, System.Boolean bGenerateNormals) { }
        // RVA: 0x09D9A518  token: 0x6000AC6
        public static UnityEngine.Mesh CombineMeshes(System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MeshData> subMeshesMap, System.Collections.Generic.List<System.Int32> submeshIndices, System.Boolean bGenerateUVs, System.Boolean bGenerateNormals, HoudiniEngineUnity.HEU_MeshIndexFormat meshIndexFormat) { }
        // RVA: 0x09D9AE14  token: 0x6000AC7
        public static UnityEngine.Mesh CreateMeshFromMeshData(HoudiniEngineUnity.HEU_MeshData submesh, System.Boolean bGenerateUVs, System.Boolean bGenerateNormals, HoudiniEngineUnity.HEU_MeshIndexFormat meshIndexFormat) { }
        // RVA: 0x09DA2670  token: 0x6000AC8
        public static System.Void TransferRegularAttributesToVertices(System.Int32[] groupVertexList, System.Int32[] allFaceCounts, System.Collections.Generic.List<System.Int32> groupFaces, System.Collections.Generic.List<System.Int32> groupVertexOffset, HoudiniEngineUnity.HAPI_AttributeInfo& attribInfo, System.Single[] inData, System.Single[]& outData) { }
        // RVA: 0x09D9CCBC  token: 0x6000AC9
        public static System.Boolean GenerateGeoGroupUsingGeoCacheVertices(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bUseLODGroups, System.Boolean bPartInstanced, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup>& LODGroupMeshes, System.Int32& defaultMaterialKey) { }
        // RVA: 0x09D9B0A4  token: 0x6000ACA
        public static System.Boolean GenerateGeoGroupUsingGeoCachePoints(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bUseLODGroups, System.Boolean bPartInstanced, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup>& LODGroupMeshes, System.Int32& defaultMaterialKey) { }
        // RVA: 0x09DA1C5C  token: 0x6000ACB
        private static System.Void GetSubmeshIDForFace(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 faceMaterialID, System.Int32 groupFace, System.Int32 vertexFaceIndex, System.Int32[] groupVertexList, System.Int32 defaultMaterialKey, System.Int32& submeshID, System.Int32& singleFaceUnityMaterialKey, System.Int32& singleFaceHoudiniMaterialKey) { }
        // RVA: 0x09D9A3E4  token: 0x6000ACC
        public static UnityEngine.MeshTopology CalculateGroupMeshTopology(System.Collections.Generic.List<System.Int32> groupFaces, System.Int32[] allFaceCounts) { }
        // RVA: 0x09DA30C4  token: 0x6000ACD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000131  // size: 0x28
    public class HEU_GeoGroup : System.IComparable`1
    {
        // Fields
        public System.String _groupName;  // 0x10
        public System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MeshData> _subMeshesMap;  // 0x18
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_VertexEntry>[] _sharedNormalIndices;  // 0x20

        // Methods
        // RVA: 0x09DA8618  token: 0x6000AD3
        public virtual System.Int32 CompareTo(HoudiniEngineUnity.HEU_GeoGroup other) { }
        // RVA: 0x09DA8650  token: 0x6000AD4
        public System.Void SetupNormalIndices(System.Int32 indicesCount) { }
        // RVA: 0x09DA8744  token: 0x6000AD5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000132  // size: 0x20
    public class HEU_VertexEntry
    {
        // Fields
        public System.Int32 _meshKey;  // 0x10
        public System.Int32 _vertexIndex;  // 0x14
        public System.Int32 _normalIndex;  // 0x18

        // Methods
        // RVA: 0x04D88580  token: 0x6000AD6
        public System.Void .ctor(System.Int32 meshKey, System.Int32 vertexIndex, System.Int32 normalIndex) { }

    }

    // TypeToken: 0x2000133  // size: 0x10
    public static class HEU_GeometryUtility
    {
        // Methods
        // RVA: 0x09DA9CF4  token: 0x6000AD7
        public static UnityEngine.Vector2[] GeneratePerTriangle(UnityEngine.Mesh meshSrc) { }
        // RVA: 0x09DA9D2C  token: 0x6000AD8
        public static System.Void GenerateSecondaryUVSet(UnityEngine.Mesh meshsrc) { }
        // RVA: 0x09DA87BC  token: 0x6000AD9
        public static System.Void CalculateMeshTangents(UnityEngine.Mesh mesh) { }
        // RVA: 0x09DA8F60  token: 0x6000ADA
        public static UnityEngine.Mesh GenerateCubeMeshFromPoints(UnityEngine.Vector3[] points, UnityEngine.Color[] pointsColor, System.Single size) { }
        // RVA: 0x09DA9D60  token: 0x6000ADB
        public static System.String GetInstanceOutputName(System.String partName, System.String[] userPrefix, System.Int32 index) { }

    }

    // TypeToken: 0x2000134  // size: 0x18
    public class HEU_InputData
    {
        // Fields
        public UnityEngine.GameObject _inputObject;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000ADC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000135  // size: 0x18
    public abstract class HEU_InputInterface
    {
        // Fields
        public static System.Int32 DEFAULT_PRIORITY;  // const
        protected System.Int32 _priority;  // 0x10

        // Properties
        System.Int32 Priority { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000ADE
        public System.Void .ctor(System.Int32 priority) { }
        // RVA: 0x09DAFE14  token: 0x6000ADF
        public System.Void RegisterInterface() { }
        // RVA: -1  // abstract  token: 0x6000AE0
        public virtual System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject) { }
        // RVA: -1  // abstract  token: 0x6000AE1
        public virtual System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID) { }

    }

    // TypeToken: 0x2000136  // size: 0x18
    public class HEU_InputInterfaceMeshSettings
    {
        // Fields
        private System.Boolean _exportColliders;  // 0x10

        // Properties
        System.Boolean ExportColliders { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000AE4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000137  // size: 0x20
    public class HEU_InputInterfaceMesh : HoudiniEngineUnity.HEU_InputInterface
    {
        // Fields
        private HoudiniEngineUnity.HEU_InputInterfaceMeshSettings settings;  // 0x18

        // Methods
        // RVA: 0x04DA2CD0  token: 0x6000AE5
        private System.Void .ctor() { }
        // RVA: 0x05392C40  token: 0x6000AE6
        public System.Void Initialize(HoudiniEngineUnity.HEU_InputInterfaceMeshSettings settings) { }
        // RVA: 0x09DA9FD4  token: 0x6000AE7
        public virtual System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID) { }
        // RVA: 0x09DAAB48  token: 0x6000AE8
        public virtual System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject) { }
        // RVA: 0x09DAAAC4  token: 0x6000AE9
        public static System.Void GetUVsFromMesh(UnityEngine.Mesh mesh, UnityEngine.Vector2[] srcUVs, System.Collections.Generic.List<UnityEngine.Vector3> destUVs, System.Int32 index) { }
        // RVA: 0x09DAB928  token: 0x6000AEA
        public System.Boolean UploadData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 inputNodeID, HoudiniEngineUnity.HEU_InputData inputData) { }
        // RVA: 0x09DAB3F4  token: 0x6000AEB
        private System.Boolean UploadColliderData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 mergeNodeID, HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMeshes inputData, System.Int32 parentNodeId) { }
        // RVA: 0x09DAAC64  token: 0x6000AEC
        private System.Boolean UploadBoxColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.BoxCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID) { }
        // RVA: 0x09DAD038  token: 0x6000AED
        private System.Boolean UploadSphereColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.SphereCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID) { }
        // RVA: 0x09DAADD8  token: 0x6000AEE
        private System.Boolean UploadCapsuleColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.CapsuleCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID) { }
        // RVA: 0x09DACD54  token: 0x6000AEF
        private System.Boolean UploadMeshColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.MeshCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID) { }
        // RVA: 0x09DAA8D4  token: 0x6000AF0
        private System.String GetColliderGroupBaseName(UnityEngine.Collider collider, System.Boolean bIsConvex, System.Boolean bIsSimple, System.Boolean bIsRendered) { }
        // RVA: 0x09DA9F60  token: 0x6000AF1
        private System.Boolean CreateInputNodeForCollider(HoudiniEngineUnity.HEU_SessionBase session, System.Int32& outNodeID, System.Int32 parentNodeId, System.Int32 colliderIndex, System.String colliderName, System.Single[] colliderVertices, System.Int32[] colliderIndices) { }
        // RVA: 0x09DAA5FC  token: 0x6000AF2
        public HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMeshes GenerateMeshDatasFromGameObject(UnityEngine.GameObject inputObject, System.Boolean bExportColliders) { }
        // RVA: 0x09DAA114  token: 0x6000AF3
        public static HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMesh CreateSingleMeshData(UnityEngine.GameObject meshGameObject, System.Boolean bExportColliders) { }
        // RVA: 0x09DAA9C4  token: 0x6000AF4
        private static UnityEngine.Mesh GetMeshFromObject(UnityEngine.GameObject meshGameObject) { }

    }

    // TypeToken: 0x200013C  // size: 0x18
    public class HEU_InputInterfaceSplineSettings
    {
        // Fields
        private System.Single _samplingResolution;  // 0x10

        // Properties
        System.Single SamplingResolution { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }

        // Methods
        // RVA: 0x04DA2CE0  token: 0x6000AFA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013D  // size: 0x20
    public class HEU_InputInterfaceSpline : HoudiniEngineUnity.HEU_InputInterface
    {
        // Fields
        private HoudiniEngineUnity.HEU_InputInterfaceSplineSettings settings;  // 0x18

        // Methods
        // RVA: 0x04DA2CD0  token: 0x6000AFB
        private System.Void .ctor() { }
        // RVA: 0x09DAD6C8  token: 0x6000AFC
        public System.Void Initialize(HoudiniEngineUnity.HEU_InputInterfaceSplineSettings settings) { }
        // RVA: 0x09DAD734  token: 0x6000AFD
        public virtual System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject) { }
        // RVA: 0x09DAD1B0  token: 0x6000AFE
        public virtual System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID) { }
        // RVA: 0x09DAD338  token: 0x6000AFF
        public HoudiniEngineUnity.HEU_InputInterfaceSpline.HEU_InputDataSplineContainer GenerateSplineDataFromGameObject(UnityEngine.GameObject inputObject) { }
        // RVA: 0x09DAD7C8  token: 0x6000B00
        public System.Boolean UploadData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 inputNodeID, HoudiniEngineUnity.HEU_InputInterfaceSpline.HEU_InputDataSpline inputSpline, UnityEngine.Matrix4x4 localToWorld) { }

    }

    // TypeToken: 0x2000140  // size: 0x18
    public class HEU_InputInterfaceTerrain : HoudiniEngineUnity.HEU_InputInterface
    {
        // Methods
        // RVA: 0x04DA2CD0  token: 0x6000B03
        public System.Void .ctor() { }
        // RVA: 0x09DAD8C8  token: 0x6000B04
        public virtual System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID) { }
        // RVA: 0x09DADF14  token: 0x6000B05
        private System.Boolean SetMaskLayer(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt, HoudiniEngineUnity.HAPI_VolumeInfo& baseVolumeInfo) { }
        // RVA: 0x09DADE14  token: 0x6000B06
        public virtual System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject) { }
        // RVA: 0x09DAD850  token: 0x6000B07
        public System.Boolean CreateHeightFieldInputNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt) { }
        // RVA: 0x09DAE758  token: 0x6000B08
        public System.Boolean UploadHeightValuesWithTransform(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo) { }
        // RVA: 0x09DAE264  token: 0x6000B09
        public System.Boolean UploadAlphaMaps(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt, HoudiniEngineUnity.HAPI_VolumeInfo& baseVolumeInfo, System.Boolean& bMaskSet) { }
        // RVA: 0x09DADEAC  token: 0x6000B0A
        public System.Boolean SetHeightFieldData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 volumeNodeID, System.Int32 partID, System.Single[] heightValues, System.String heightFieldName, HoudiniEngineUnity.HAPI_VolumeInfo& baseVolumeInfo) { }
        // RVA: 0x09DAE018  token: 0x6000B0B
        public System.Boolean SetTerrainDataAttributesToHeightField(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainData terrainData) { }
        // RVA: 0x09DAE09C  token: 0x6000B0C
        public System.Boolean SetTerrainLayerAttributesToHeightField(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainLayer terrainLayer) { }
        // RVA: 0x09DAE140  token: 0x6000B0D
        public System.Void SetTreePrototypes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainData terrainData) { }
        // RVA: 0x09DAE120  token: 0x6000B0E
        public System.Void SetTreeInstances(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainData terrainData) { }
        // RVA: 0x09DADA44  token: 0x6000B0F
        public HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain GenerateTerrainDataFromGameObject(UnityEngine.GameObject inputObject) { }

    }

    // TypeToken: 0x2000142  // size: 0x18
    public class HEU_InputInterfaceTilemapSettings
    {
        // Fields
        public System.Boolean _createGroupsForTiles;  // 0x10
        public System.Boolean _exportUnusedTiles;  // 0x11
        public System.Boolean _applyTileColor;  // 0x12
        public System.Boolean _applyTilemapOrientation;  // 0x13

        // Methods
        // RVA: 0x04DA2CF0  token: 0x6000B11
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000143  // size: 0x20
    public class HEU_InputInterfaceTilemap : HoudiniEngineUnity.HEU_InputInterface
    {
        // Fields
        private HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings settings;  // 0x18

        // Methods
        // RVA: 0x04DA2CD0  token: 0x6000B12
        private System.Void .ctor() { }
        // RVA: 0x09DAE9E8  token: 0x6000B13
        public System.Void Initialize(HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings settings) { }
        // RVA: 0x09DAE844  token: 0x6000B14
        public virtual System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID) { }
        // RVA: 0x09DAEA54  token: 0x6000B15
        public virtual System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject) { }
        // RVA: 0x09DAEAE8  token: 0x6000B16
        private System.Boolean UploadData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 inputNodeID, HoudiniEngineUnity.HEU_InputData inputData) { }
        // RVA: 0x09DAE91C  token: 0x6000B17
        public HoudiniEngineUnity.HEU_InputInterfaceTilemap.HEU_InputDataTilemap GenerateTilemapDataFromGameObject(UnityEngine.GameObject inputObject) { }

    }

    // TypeToken: 0x2000145  // size: 0x10
    public static class HEU_InputMeshUtility
    {
        // Methods
        // RVA: 0x09DAFFB0  token: 0x6000B19
        public static System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo, System.Boolean bConvertToHoudiniCoordinateSystem) { }
        // RVA: 0x09DB033C  token: 0x6000B1A
        public static System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, System.Single[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }
        // RVA: 0x09DB0494  token: 0x6000B1B
        public static System.Boolean SetMeshVertexAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3[] data, System.Int32[] indices, HoudiniEngineUnity.HAPI_PartInfo& partInfo, System.Boolean bConvertToHoudiniCoordinateSystem) { }
        // RVA: 0x09DB05E8  token: 0x6000B1C
        public static System.Boolean SetMeshVertexFloatAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, System.Single[] data, System.Int32[] indices, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }
        // RVA: 0x09DB073C  token: 0x6000B1D
        public static System.Boolean UploadMeshIntoHoudiniNode(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetNodeID, System.Int32 objectID, System.Int32 geoID, UnityEngine.Mesh& mesh) { }
        // RVA: 0x09DB0108  token: 0x6000B1E
        public static System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3Int[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }
        // RVA: 0x09DB0258  token: 0x6000B1F
        public static System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.String[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }
        // RVA: 0x09DAFE60  token: 0x6000B20
        public static System.Boolean SetMeshDetailAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3 data, HoudiniEngineUnity.HAPI_PartInfo& partInfo) { }

    }

    // TypeToken: 0x2000146  // size: 0x10
    public static class HEU_InputUtility
    {
        // Fields
        private static System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterface> _inputInterfaces;  // static @ 0x0

        // Methods
        // RVA: 0x09DB0CD4  token: 0x6000B21
        public static System.Int32 GetHighestPriority() { }
        // RVA: 0x09DB1078  token: 0x6000B22
        public static System.Void RegisterInputInterface(HoudiniEngineUnity.HEU_InputInterface inputInterface) { }
        // RVA: 0x09DB1278  token: 0x6000B23
        public static System.Void UnregisterInputInterface(HoudiniEngineUnity.HEU_InputInterface inputInterface) { }
        // RVA: 0x09DB0D78  token: 0x6000B24
        public static HoudiniEngineUnity.HEU_InputInterface GetInputInterfaceByType(System.Type type) { }
        // RVA: 0x09DB0F70  token: 0x6000B25
        public static HoudiniEngineUnity.HEU_InputInterface GetInputInterface(UnityEngine.GameObject inputObject) { }
        // RVA: 0x09DB0EA4  token: 0x6000B26
        private static HoudiniEngineUnity.HEU_InputInterface GetInputInterface(HoudiniEngineUnity.HEU_InputObjectInfo inputObjectInfo) { }
        // RVA: 0x09DB0A20  token: 0x6000B27
        private static System.Boolean CreateInputNodeWithMultiObjects(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32& connectMergeID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputObjectInfo>& inputObjects, System.Collections.Generic.List<System.Int32>& inputObjectsConnectedAssetIDs, HoudiniEngineUnity.HEU_InputNode inputNode) { }
        // RVA: 0x09DB0964  token: 0x6000B28
        private static System.Boolean CreateInputNodeWithMultiAssets(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Int32& connectMergeID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputHDAInfo>& inputAssetInfos, System.Boolean bKeepWorldTransform, System.Int32 mergeParentID) { }
        // RVA: 0x09DB12E8  token: 0x6000B29
        private static System.Boolean UploadInputObjectTransform(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputObjectInfo inputObject, System.Int32 inputNodeID, System.Boolean bKeepWorldTransform) { }
        // RVA: 0x09DB0B78  token: 0x6000B2A
        public static System.Void GetChildrenTransforms(UnityEngine.Transform transform, System.Collections.Generic.List<UnityEngine.Matrix4x4>& childTransforms) { }
        // RVA: 0x09DB156C  token: 0x6000B2B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000147  // size: 0x10
    public class HEU_Logger
    {
        // Methods
        // RVA: 0x09DB18D4  token: 0x6000B2C
        public static System.Void Log(System.String text) { }
        // RVA: 0x09DB1730  token: 0x6000B2D
        public static System.Void LogFormat(System.String text, System.Object[] args) { }
        // RVA: 0x09DB1888  token: 0x6000B2E
        public static System.Void LogWarning(System.String text) { }
        // RVA: 0x09DB1828  token: 0x6000B2F
        public static System.Void LogWarningFormat(System.String text, System.Object[] args) { }
        // RVA: 0x09DB1668  token: 0x6000B30
        public static System.Void LogError(System.String text) { }
        // RVA: 0x09DB1608  token: 0x6000B31
        public static System.Void LogErrorFormat(System.String text, System.Object[] args) { }
        // RVA: 0x09DB16B4  token: 0x6000B32
        public static System.Void LogError(System.Exception ex) { }
        // RVA: 0x09DB1600  token: 0x6000B33
        public static System.Void LogAssertion(System.String text) { }
        // RVA: 0x09DB15F8  token: 0x6000B34
        public static System.Void LogAssertionFormat(System.String text, System.Object[] args) { }
        // RVA: 0x09DB17E8  token: 0x6000B35
        private static System.Void LogToCookLogsIfOn(System.String text) { }
        // RVA: 0x09DB178C  token: 0x6000B36
        private static System.Void LogToCookLogsIfOnFormat(System.String text, System.Object[] args) { }
        // RVA: 0x041E1670  token: 0x6000B37
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000148  // size: 0x58
    public class HEU_MeshData
    {
        // Fields
        public System.Collections.Generic.List<System.Int32> _indices;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Vector3> _vertices;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Color32> _colors;  // 0x20
        public System.Collections.Generic.List<UnityEngine.Vector3> _normals;  // 0x28
        public System.Collections.Generic.List<UnityEngine.Vector4> _tangents;  // 0x30
        public System.Collections.Generic.List<UnityEngine.Vector4>[] _uvs;  // 0x38
        public System.Collections.Generic.List<UnityEngine.Vector3> _triangleNormals;  // 0x40
        public System.Collections.Generic.Dictionary<System.Int32,System.Int32> _pointIndexToMeshIndexMap;  // 0x48
        public UnityEngine.MeshTopology _meshTopology;  // 0x50

        // Methods
        // RVA: 0x09DB1920  token: 0x6000B38
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000149  // size: 0x18
    public class HEU_MeshIndexFormat
    {
        // Fields
        public UnityEngine.Rendering.IndexFormat _indexFormat;  // 0x10

        // Methods
        // RVA: 0x09DB1B58  token: 0x6000B39
        public System.Void CalculateIndexFormat(System.Int32 numVertices) { }
        // RVA: 0x09DB1BB4  token: 0x6000B3A
        public System.Void SetFormatForMesh(UnityEngine.Mesh mesh) { }
        // RVA: 0x041E1670  token: 0x6000B3B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200014A  // size: 0x14
    public sealed struct HEU_PipelineType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HEU_PipelineType Unsupported;  // const
        public static HoudiniEngineUnity.HEU_PipelineType BiRP;  // const
        public static HoudiniEngineUnity.HEU_PipelineType URP;  // const
        public static HoudiniEngineUnity.HEU_PipelineType HDRP;  // const

    }

    // TypeToken: 0x200014B  // size: 0x10
    public class HEU_RenderingPipelineDefines
    {
        // Methods
        // RVA: 0x09DB1F50  token: 0x6000B3C
        private static System.Void .cctor() { }
        // RVA: 0x09DB1E8C  token: 0x6000B3D
        private static System.Void UpdateDefines() { }
        // RVA: 0x09DB1CDC  token: 0x6000B3E
        public static HoudiniEngineUnity.HEU_PipelineType GetPipeline() { }
        // RVA: 0x09DB1BD8  token: 0x6000B3F
        private static System.Void AddDefine(System.String define) { }
        // RVA: 0x09DB1DE8  token: 0x6000B40
        public static System.Void RemoveDefine(System.String define) { }
        // RVA: 0x09DB1C7C  token: 0x6000B41
        public static System.Collections.Generic.List<System.String> GetDefines() { }
        // RVA: 0x041E1670  token: 0x6000B42
        public static System.Void SetDefines(System.Collections.Generic.List<System.String> definesList) { }
        // RVA: 0x041E1670  token: 0x6000B43
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200014C
    public class HEU_SerializableDictionary`2 : System.Collections.Generic.IDictionary`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private System.Collections.Generic.Dictionary<TKey,TValue> _dictionary;  // 0x0
        private TKey[] _keys;  // 0x0
        private TValue[] _values;  // 0x0

        // Properties
        TValue Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.ICollection<TKey> Keys { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.ICollection<TValue> Values { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B4A
        public virtual System.Void Add(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000B4B
        public virtual System.Void Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000B4C
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000B4D
        public virtual System.Boolean Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000B4E
        public virtual System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000B4F
        public virtual System.Void CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000B50
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000B51
        public virtual System.Boolean Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000B52
        public virtual System.Boolean Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000B53
        public virtual System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000B54
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000B55
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: -1  // not resolved  token: 0x6000B56
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: -1  // not resolved  token: 0x6000B57
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200014D  // size: 0x50
    public class HEU_ShelfToolData
    {
        // Fields
        public System.String _name;  // 0x10
        public HoudiniEngineUnity.HEU_ShelfToolData.ToolType _toolType;  // 0x18
        public System.String _toolTip;  // 0x20
        public System.String _iconPath;  // 0x28
        public System.String _assetPath;  // 0x30
        public System.String _helpURL;  // 0x38
        public System.String[] _targets;  // 0x40
        public System.String _jsonPath;  // 0x48

        // Methods
        // RVA: 0x09DB1F58  token: 0x6000B58
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200014F  // size: 0x30
    public class HEU_Shelf
    {
        // Fields
        public System.String _shelfName;  // 0x10
        public System.String _shelfPath;  // 0x18
        public System.Boolean _defaultShelf;  // 0x20
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_ShelfToolData> _tools;  // 0x28

        // Methods
        // RVA: 0x09DB5398  token: 0x6000B59
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000150  // size: 0x10
    public class HEU_ShelfTools
    {
        // Fields
        private static System.Collections.Generic.List<HoudiniEngineUnity.HEU_Shelf> _shelves;  // static @ 0x0
        private static System.Boolean _shelvesLoaded;  // static @ 0x8
        private static System.Int32 _currentSelectedShelf;  // static @ 0xc
        public static System.String TARGET_ALL;  // const
        public static System.String TARGET_UNITY;  // const

        // Methods
        // RVA: 0x09DB219C  token: 0x6000B5A
        public static System.Boolean AreShelvesLoaded() { }
        // RVA: 0x09DB5228  token: 0x6000B5B
        public static System.Void SetReloadShelves() { }
        // RVA: 0x09DB21EC  token: 0x6000B5C
        public static System.Void ClearShelves() { }
        // RVA: 0x09DB320C  token: 0x6000B5D
        public static System.Int32 GetNumShelves() { }
        // RVA: 0x09DB31BC  token: 0x6000B5E
        public static System.Int32 GetCurrentShelfIndex() { }
        // RVA: 0x09DB51D4  token: 0x6000B5F
        public static System.Void SetCurrentShelf(System.Int32 index) { }
        // RVA: 0x09DB3410  token: 0x6000B60
        public static HoudiniEngineUnity.HEU_Shelf GetShelf(System.Int32 index) { }
        // RVA: 0x09DB32B4  token: 0x6000B61
        public static HoudiniEngineUnity.HEU_Shelf GetShelf(System.String shelfName) { }
        // RVA: 0x09DB3268  token: 0x6000B62
        public static System.String GetShelfStorageEntry(System.String shelfName, System.String shelfPath) { }
        // RVA: 0x09DB34B4  token: 0x6000B63
        public static System.Void GetSplitShelfEntry(System.String shelfEntry, System.String& shelfName, System.String& shelfPath) { }
        // RVA: 0x09DB3B48  token: 0x6000B64
        public static System.Void LoadShelves() { }
        // RVA: 0x09DB4C6C  token: 0x6000B65
        public static System.Boolean LoadToolsFromDirectory(System.String folderPath, System.Collections.Generic.List<HoudiniEngineUnity.HEU_ShelfToolData>& tools) { }
        // RVA: 0x09DB4224  token: 0x6000B66
        public static HoudiniEngineUnity.HEU_ShelfToolData LoadToolFromJsonFile(System.String jsonFilePath) { }
        // RVA: 0x09DB43C0  token: 0x6000B67
        public static HoudiniEngineUnity.HEU_ShelfToolData LoadToolFromJsonString(System.String json, System.String jsonFilePath) { }
        // RVA: 0x09DB1FFC  token: 0x6000B68
        public static HoudiniEngineUnity.HEU_Shelf AddShelf(System.String shelfName, System.String shelfPath) { }
        // RVA: 0x09DB4E90  token: 0x6000B69
        public static System.Void RemoveShelf(System.Int32 shelfIndex) { }
        // RVA: 0x09DB4FC0  token: 0x6000B6A
        public static System.Void SaveShelf() { }
        // RVA: 0x09DB2F14  token: 0x6000B6B
        public static System.Void ExecuteTool(System.Int32 toolSlot) { }
        // RVA: 0x09DB22B0  token: 0x6000B6C
        public static System.Void ExecuteToolGenerator(System.String toolName, System.String toolPath, UnityEngine.Vector3 targetPosition, UnityEngine.Quaternion targetRotation, UnityEngine.Vector3 targetScale) { }
        // RVA: 0x09DB39B8  token: 0x6000B6D
        public static System.Boolean IsValidInputMesh(UnityEngine.GameObject gameObject) { }
        // RVA: 0x09DB3928  token: 0x6000B6E
        public static System.Boolean IsValidInputHDA(UnityEngine.GameObject gameObject) { }
        // RVA: 0x09DB5278  token: 0x6000B6F
        private static System.Boolean ShouldUseHDA(UnityEngine.GameObject[] gameObjectList) { }
        // RVA: 0x09DB244C  token: 0x6000B70
        public static System.Void ExecuteToolNoInput(System.String toolName, System.String toolPath) { }
        // RVA: 0x09DB299C  token: 0x6000B71
        public static System.Void ExecuteToolOperatorSingle(System.String toolName, System.String toolPath, UnityEngine.GameObject[] inputObjects) { }
        // RVA: 0x09DB2580  token: 0x6000B72
        public static System.Void ExecuteToolOperatorMultiple(System.String toolName, System.String toolPath, UnityEngine.GameObject[] inputObjects) { }
        // RVA: 0x09DB2250  token: 0x6000B73
        public static System.Void ExecuteToolBatch(System.String toolName, System.String toolPath, UnityEngine.GameObject[] batchObjects) { }
        // RVA: 0x09DB386C  token: 0x6000B74
        public static System.String GetToolResourcePath(HoudiniEngineUnity.HEU_ShelfToolData tool, System.String inPath, System.String ext) { }
        // RVA: 0x09DB3704  token: 0x6000B75
        public static System.String GetToolIconPath(HoudiniEngineUnity.HEU_ShelfToolData tool, System.String inPath) { }
        // RVA: 0x09DB3594  token: 0x6000B76
        public static System.String GetToolAssetPath(HoudiniEngineUnity.HEU_ShelfToolData tool, System.String inPath) { }
        // RVA: 0x041E1670  token: 0x6000B77
        public System.Void .ctor() { }
        // RVA: 0x09DB52F8  token: 0x6000B78
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000152  // size: 0x14
    public sealed struct JSONNodeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.JSONNodeType Array;  // const
        public static HoudiniEngineUnity.JSONNodeType Object;  // const
        public static HoudiniEngineUnity.JSONNodeType String;  // const
        public static HoudiniEngineUnity.JSONNodeType Number;  // const
        public static HoudiniEngineUnity.JSONNodeType NullValue;  // const
        public static HoudiniEngineUnity.JSONNodeType Boolean;  // const
        public static HoudiniEngineUnity.JSONNodeType None;  // const
        public static HoudiniEngineUnity.JSONNodeType Custom;  // const

    }

    // TypeToken: 0x2000153  // size: 0x14
    public sealed struct JSONTextMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.JSONTextMode Compact;  // const
        public static HoudiniEngineUnity.JSONTextMode Indent;  // const

    }

    // TypeToken: 0x2000154  // size: 0x10
    public abstract class JSONNode
    {
        // Fields
        public static System.Boolean forceASCII;  // static @ 0x0
        public static System.Boolean longAsString;  // static @ 0x1
        private static System.Text.StringBuilder m_EscapeBuilder;  // static @ 0xffffffff
        public static HoudiniEngineUnity.JSONContainerType VectorContainerType;  // static @ 0x4
        public static HoudiniEngineUnity.JSONContainerType QuaternionContainerType;  // static @ 0x8
        public static HoudiniEngineUnity.JSONContainerType RectContainerType;  // static @ 0xc

        // Properties
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: -1  // abstract */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x011EC580 */ set; /* RVA: 0x041E1670 */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x011EC580 */ set; /* RVA: 0x041E1670 */ }
        System.String Value { get; /* RVA: 0x09DB7C0C */ set; /* RVA: 0x041E1670 */ }
        System.Int32 Count { get; /* RVA: 0x011EC580 */ }
        System.Boolean IsNumber { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsString { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsBoolean { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsNull { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsArray { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsObject { get; /* RVA: 0x012081B0 */ }
        System.Boolean Inline { get; /* RVA: 0x012081B0 */ set; /* RVA: 0x041E1670 */ }
        System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode> Children { get; /* RVA: 0x09DB7980 */ }
        System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode> DeepChildren { get; /* RVA: 0x09DB79D4 */ }
        System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode>> Linq { get; /* RVA: 0x09DB7BAC */ }
        HoudiniEngineUnity.JSONNode.KeyEnumerator Keys { get; /* RVA: 0x09DB7AF8 */ }
        HoudiniEngineUnity.JSONNode.ValueEnumerator Values { get; /* RVA: 0x09DB7AF8 */ }
        System.Double AsDouble { get; /* RVA: 0x09DB7840 */ set; /* RVA: 0x09DB85E8 */ }
        System.Int32 AsInt { get; /* RVA: 0x09DB78E8 */ set; /* RVA: 0x09DB8660 */ }
        System.Single AsFloat { get; /* RVA: 0x09DB78CC */ set; /* RVA: 0x09DB864C */ }
        System.Boolean AsBool { get; /* RVA: 0x09DB77A4 */ set; /* RVA: 0x09DB857C */ }
        System.Int64 AsLong { get; /* RVA: 0x09DB7904 */ set; /* RVA: 0x09DB8678 */ }
        HoudiniEngineUnity.JSONArray AsArray { get; /* RVA: 0x09DB7768 */ }
        HoudiniEngineUnity.JSONObject AsObject { get; /* RVA: 0x09DB7944 */ }
        System.Text.StringBuilder EscapeBuilder { get; /* RVA: 0x09DB7A44 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000B8B
        public virtual System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem) { }
        // RVA: 0x09DB5410  token: 0x6000B8C
        public virtual System.Void Add(HoudiniEngineUnity.JSONNode aItem) { }
        // RVA: 0x011EC580  token: 0x6000B8D
        public virtual HoudiniEngineUnity.JSONNode Remove(System.String aKey) { }
        // RVA: 0x011EC580  token: 0x6000B8E
        public virtual HoudiniEngineUnity.JSONNode Remove(System.Int32 aIndex) { }
        // RVA: 0x04D88C30  token: 0x6000B8F
        public virtual HoudiniEngineUnity.JSONNode Remove(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DB6BEC  token: 0x6000B92
        public virtual System.String ToString() { }
        // RVA: 0x09DB6C70  token: 0x6000B93
        public virtual System.String ToString(System.Int32 aIndent) { }
        // RVA: -1  // abstract  token: 0x6000B94
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }
        // RVA: -1  // abstract  token: 0x6000B95
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x09DB7D60  token: 0x6000BA5
        public static HoudiniEngineUnity.JSONNode op_Implicit(System.String s) { }
        // RVA: 0x09DB8234  token: 0x6000BA6
        public static System.String op_Implicit(HoudiniEngineUnity.JSONNode d) { }
        // RVA: 0x09DB8360  token: 0x6000BA7
        public static HoudiniEngineUnity.JSONNode op_Implicit(System.Double n) { }
        // RVA: 0x09DB7D20  token: 0x6000BA8
        public static System.Double op_Implicit(HoudiniEngineUnity.JSONNode d) { }
        // RVA: 0x09DB8274  token: 0x6000BA9
        public static HoudiniEngineUnity.JSONNode op_Implicit(System.Single n) { }
        // RVA: 0x09DB84F0  token: 0x6000BAA
        public static System.Single op_Implicit(HoudiniEngineUnity.JSONNode d) { }
        // RVA: 0x09DB7E38  token: 0x6000BAB
        public static HoudiniEngineUnity.JSONNode op_Implicit(System.Int32 n) { }
        // RVA: 0x09DB80EC  token: 0x6000BAC
        public static System.Int32 op_Implicit(HoudiniEngineUnity.JSONNode d) { }
        // RVA: 0x09DB7ED0  token: 0x6000BAD
        public static HoudiniEngineUnity.JSONNode op_Implicit(System.Int64 n) { }
        // RVA: 0x09DB84B0  token: 0x6000BAE
        public static System.Int64 op_Implicit(HoudiniEngineUnity.JSONNode d) { }
        // RVA: 0x09DB7E88  token: 0x6000BAF
        public static HoudiniEngineUnity.JSONNode op_Implicit(System.Boolean b) { }
        // RVA: 0x09DB82E8  token: 0x6000BB0
        public static System.Boolean op_Implicit(HoudiniEngineUnity.JSONNode d) { }
        // RVA: 0x09DB8328  token: 0x6000BB1
        public static HoudiniEngineUnity.JSONNode op_Implicit(System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode> aKeyValue) { }
        // RVA: 0x09DB7C38  token: 0x6000BB2
        public static System.Boolean op_Equality(HoudiniEngineUnity.JSONNode a, System.Object b) { }
        // RVA: 0x09DB8568  token: 0x6000BB3
        public static System.Boolean op_Inequality(HoudiniEngineUnity.JSONNode a, System.Object b) { }
        // RVA: 0x02FB1840  token: 0x6000BB4
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04D87100  token: 0x6000BB5
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09DB5458  token: 0x6000BB7
        private static System.String Escape(System.String aText) { }
        // RVA: 0x09DB5744  token: 0x6000BB8
        private static HoudiniEngineUnity.JSONNode ParseElement(System.String token, System.Boolean quoted) { }
        // RVA: 0x09DB5898  token: 0x6000BB9
        public static HoudiniEngineUnity.JSONNode Parse(System.String aJSON) { }
        // RVA: 0x09DB56C0  token: 0x6000BBA
        private static HoudiniEngineUnity.JSONNode GetContainer(HoudiniEngineUnity.JSONContainerType aType) { }
        // RVA: 0x09DB7F90  token: 0x6000BBB
        public static HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Vector2 aVec) { }
        // RVA: 0x09DB802C  token: 0x6000BBC
        public static HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Vector3 aVec) { }
        // RVA: 0x09DB7DC0  token: 0x6000BBD
        public static HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Vector4 aVec) { }
        // RVA: 0x09DB841C  token: 0x6000BBE
        public static HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Quaternion aRot) { }
        // RVA: 0x09DB81BC  token: 0x6000BBF
        public static HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Rect aRect) { }
        // RVA: 0x09DB83B0  token: 0x6000BC0
        public static HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.RectOffset aRect) { }
        // RVA: 0x09DB8494  token: 0x6000BC1
        public static UnityEngine.Vector2 op_Implicit(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DB8530  token: 0x6000BC2
        public static UnityEngine.Vector3 op_Implicit(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DB818C  token: 0x6000BC3
        public static UnityEngine.Vector4 op_Implicit(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DB812C  token: 0x6000BC4
        public static UnityEngine.Quaternion op_Implicit(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DB815C  token: 0x6000BC5
        public static UnityEngine.Rect op_Implicit(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DB82CC  token: 0x6000BC6
        public static UnityEngine.RectOffset op_Implicit(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DB6580  token: 0x6000BC7
        public UnityEngine.Vector2 ReadVector2(UnityEngine.Vector2 aDefault) { }
        // RVA: 0x09DB6668  token: 0x6000BC8
        public UnityEngine.Vector2 ReadVector2(System.String aXName, System.String aYName) { }
        // RVA: 0x09DB6708  token: 0x6000BC9
        public UnityEngine.Vector2 ReadVector2() { }
        // RVA: 0x09DB7364  token: 0x6000BCA
        public HoudiniEngineUnity.JSONNode WriteVector2(UnityEngine.Vector2 aVec, System.String aXName, System.String aYName) { }
        // RVA: 0x09DB6720  token: 0x6000BCB
        public UnityEngine.Vector3 ReadVector3(UnityEngine.Vector3 aDefault) { }
        // RVA: 0x09DB689C  token: 0x6000BCC
        public UnityEngine.Vector3 ReadVector3(System.String aXName, System.String aYName, System.String aZName) { }
        // RVA: 0x09DB6984  token: 0x6000BCD
        public UnityEngine.Vector3 ReadVector3() { }
        // RVA: 0x09DB745C  token: 0x6000BCE
        public HoudiniEngineUnity.JSONNode WriteVector3(UnityEngine.Vector3 aVec, System.String aXName, System.String aYName, System.String aZName) { }
        // RVA: 0x09DB69D4  token: 0x6000BCF
        public UnityEngine.Vector4 ReadVector4(UnityEngine.Vector4 aDefault) { }
        // RVA: 0x09DB6BB0  token: 0x6000BD0
        public UnityEngine.Vector4 ReadVector4() { }
        // RVA: 0x09DB759C  token: 0x6000BD1
        public HoudiniEngineUnity.JSONNode WriteVector4(UnityEngine.Vector4 aVec) { }
        // RVA: 0x09DB5F18  token: 0x6000BD2
        public UnityEngine.Quaternion ReadQuaternion(UnityEngine.Quaternion aDefault) { }
        // RVA: 0x09DB5EDC  token: 0x6000BD3
        public UnityEngine.Quaternion ReadQuaternion() { }
        // RVA: 0x09DB6D94  token: 0x6000BD4
        public HoudiniEngineUnity.JSONNode WriteQuaternion(UnityEngine.Quaternion aRot) { }
        // RVA: 0x09DB63A4  token: 0x6000BD5
        public UnityEngine.Rect ReadRect(UnityEngine.Rect aDefault) { }
        // RVA: 0x09DB6370  token: 0x6000BD6
        public UnityEngine.Rect ReadRect() { }
        // RVA: 0x09DB7198  token: 0x6000BD7
        public HoudiniEngineUnity.JSONNode WriteRect(UnityEngine.Rect aRect) { }
        // RVA: 0x09DB60F4  token: 0x6000BD8
        public UnityEngine.RectOffset ReadRectOffset(UnityEngine.RectOffset aDefault) { }
        // RVA: 0x09DB6308  token: 0x6000BD9
        public UnityEngine.RectOffset ReadRectOffset() { }
        // RVA: 0x09DB6F60  token: 0x6000BDA
        public HoudiniEngineUnity.JSONNode WriteRectOffset(UnityEngine.RectOffset aRect) { }
        // RVA: 0x09DB5E28  token: 0x6000BDB
        public UnityEngine.Matrix4x4 ReadMatrix() { }
        // RVA: 0x09DB6D00  token: 0x6000BDC
        public HoudiniEngineUnity.JSONNode WriteMatrix(UnityEngine.Matrix4x4 aMatrix) { }
        // RVA: 0x041E1670  token: 0x6000BDD
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200015C  // size: 0x20
    public class JSONArray : HoudiniEngineUnity.JSONNode
    {
        // Fields
        private System.Collections.Generic.List<HoudiniEngineUnity.JSONNode> m_List;  // 0x10
        private System.Boolean inline;  // 0x18

        // Properties
        System.Boolean Inline { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: 0x04D85B80 */ }
        System.Boolean IsArray { get; /* RVA: 0x02FFF600 */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x09DC0A88 */ set; /* RVA: 0x09DC0B28 */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x09DC0A88 */ set; /* RVA: 0x09DC0B28 */ }
        System.Int32 Count { get; /* RVA: 0x09DC09E8 */ }
        System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode> Children { get; /* RVA: 0x09DC0978 */ }

        // Methods
        // RVA: 0x09DC0614  token: 0x6000C0D
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x09DC058C  token: 0x6000C13
        public virtual System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem) { }
        // RVA: 0x09DC06A4  token: 0x6000C14
        public virtual HoudiniEngineUnity.JSONNode Remove(System.Int32 aIndex) { }
        // RVA: 0x09DC0748  token: 0x6000C15
        public virtual HoudiniEngineUnity.JSONNode Remove(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DC07A0  token: 0x6000C17
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }
        // RVA: 0x09DC0900  token: 0x6000C18
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200015E  // size: 0x20
    public class JSONObject : HoudiniEngineUnity.JSONNode
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.JSONNode> m_Dict;  // 0x10
        private System.Boolean inline;  // 0x18

        // Properties
        System.Boolean Inline { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: 0x02BBE540 */ }
        System.Boolean IsObject { get; /* RVA: 0x02FFF600 */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x09DC268C */ set; /* RVA: 0x09DC27D8 */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x09DC268C */ set; /* RVA: 0x09DC27D8 */ }
        System.Int32 Count { get; /* RVA: 0x09DC264C */ }
        System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode> Children { get; /* RVA: 0x09DC25DC */ }

        // Methods
        // RVA: 0x09DC1E74  token: 0x6000C26
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x09DC1D28  token: 0x6000C2C
        public virtual System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem) { }
        // RVA: 0x09DC2184  token: 0x6000C2D
        public virtual HoudiniEngineUnity.JSONNode Remove(System.String aKey) { }
        // RVA: 0x09DC1F18  token: 0x6000C2E
        public virtual HoudiniEngineUnity.JSONNode Remove(System.Int32 aIndex) { }
        // RVA: 0x09DC1FD8  token: 0x6000C2F
        public virtual HoudiniEngineUnity.JSONNode Remove(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x09DC223C  token: 0x6000C31
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }
        // RVA: 0x09DC2564  token: 0x6000C32
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000161  // size: 0x18
    public class JSONString : HoudiniEngineUnity.JSONNode
    {
        // Fields
        private System.String m_Data;  // 0x10

        // Properties
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: 0x04D8C9E0 */ }
        System.Boolean IsString { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000C40
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x053908C0  token: 0x6000C43
        public System.Void .ctor(System.String aData) { }
        // RVA: 0x09DC2A50  token: 0x6000C44
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }
        // RVA: 0x09DC29AC  token: 0x6000C45
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x08479504  token: 0x6000C46
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000162  // size: 0x18
    public class JSONNumber : HoudiniEngineUnity.JSONNode
    {
        // Fields
        private System.Double m_Data;  // 0x10

        // Properties
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: 0x04D882B0 */ }
        System.Boolean IsNumber { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x09DC1C5C */ set; /* RVA: 0x09DC1CA8 */ }
        System.Double AsDouble { get; /* RVA: 0x04D96320 */ set; /* RVA: 0x04D85BA0 */ }
        System.Int64 AsLong { get; /* RVA: 0x04DA2D40 */ set; /* RVA: 0x04DA2D50 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000C49
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x04D85BA0  token: 0x6000C50
        public System.Void .ctor(System.Double aData) { }
        // RVA: 0x09DC0D94  token: 0x6000C51
        public System.Void .ctor(System.String aData) { }
        // RVA: 0x09DC1C28  token: 0x6000C52
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }
        // RVA: 0x09DC1AE0  token: 0x6000C53
        private static System.Boolean IsNumeric(System.Object value) { }
        // RVA: 0x09DC1A00  token: 0x6000C54
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x09DC1AD4  token: 0x6000C55
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000163  // size: 0x18
    public class JSONBool : HoudiniEngineUnity.JSONNode
    {
        // Fields
        private System.Boolean m_Data;  // 0x10

        // Properties
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: 0x04D9EC60 */ }
        System.Boolean IsBoolean { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x09DC0DA8 */ set; /* RVA: 0x09DC0DFC */ }
        System.Boolean AsBool { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000C58
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x04D86130  token: 0x6000C5D
        public System.Void .ctor(System.Boolean aData) { }
        // RVA: 0x09DC0D94  token: 0x6000C5E
        public System.Void .ctor(System.String aData) { }
        // RVA: 0x09DC0D28  token: 0x6000C5F
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }
        // RVA: 0x09DC0C84  token: 0x6000C60
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x09DC0CE0  token: 0x6000C61
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000164  // size: 0x10
    public class JSONNull : HoudiniEngineUnity.JSONNode
    {
        // Fields
        private static HoudiniEngineUnity.JSONNull m_StaticInstance;  // static @ 0x0
        public static System.Boolean reuseSameInstance;  // static @ 0x8

        // Properties
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: 0x04D9EC50 */ }
        System.Boolean IsNull { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x09DC19D4 */ set; /* RVA: 0x041E1670 */ }
        System.Boolean AsBool { get; /* RVA: 0x012081B0 */ set; /* RVA: 0x041E1670 */ }

        // Methods
        // RVA: 0x09DC1850  token: 0x6000C62
        public static HoudiniEngineUnity.JSONNull CreateOrGet() { }
        // RVA: 0x041E1670  token: 0x6000C63
        private System.Void .ctor() { }
        // RVA: 0x04DA2D20  token: 0x6000C66
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x09DC18BC  token: 0x6000C6B
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x011EC580  token: 0x6000C6C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09DC1914  token: 0x6000C6D
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }
        // RVA: 0x09DC195C  token: 0x6000C6E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000165  // size: 0x20
    public class JSONLazyCreator : HoudiniEngineUnity.JSONNode
    {
        // Fields
        private HoudiniEngineUnity.JSONNode m_Node;  // 0x10
        private System.String m_Key;  // 0x18

        // Properties
        HoudiniEngineUnity.JSONNodeType Tag { get; /* RVA: 0x04D9BE00 */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x09DC1368 */ set; /* RVA: 0x09DC1718 */ }
        HoudiniEngineUnity.JSONNode Item { get; /* RVA: 0x09DC1368 */ set; /* RVA: 0x09DC1718 */ }
        System.Int32 AsInt { get; /* RVA: 0x09DC11A0 */ set; /* RVA: 0x09DC15A4 */ }
        System.Single AsFloat { get; /* RVA: 0x09DC1138 */ set; /* RVA: 0x09DC152C */ }
        System.Double AsDouble { get; /* RVA: 0x09DC10D0 */ set; /* RVA: 0x09DC14B8 */ }
        System.Int64 AsLong { get; /* RVA: 0x09DC1208 */ set; /* RVA: 0x09DC161C */ }
        System.Boolean AsBool { get; /* RVA: 0x09DC1068 */ set; /* RVA: 0x09DC1448 */ }
        HoudiniEngineUnity.JSONArray AsArray { get; /* RVA: 0x09DC0FF0 */ }
        HoudiniEngineUnity.JSONObject AsObject { get; /* RVA: 0x09DC12F0 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000C70
        public virtual HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x03E8CD90  token: 0x6000C71
        public System.Void .ctor(HoudiniEngineUnity.JSONNode aNode) { }
        // RVA: 0x032AF420  token: 0x6000C72
        public System.Void .ctor(HoudiniEngineUnity.JSONNode aNode, System.String aKey) { }
        // RVA: -1  // generic def  token: 0x6000C73
        private T Set(T aVal) { }
        // RVA: 0x09DC0F04  token: 0x6000C78
        public virtual System.Void Add(HoudiniEngineUnity.JSONNode aItem) { }
        // RVA: 0x09DC0E60  token: 0x6000C79
        public virtual System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem) { }
        // RVA: 0x09DC0F98  token: 0x6000C7A
        public static System.Boolean op_Equality(HoudiniEngineUnity.JSONLazyCreator a, System.Object b) { }
        // RVA: 0x09DC1438  token: 0x6000C7B
        public static System.Boolean op_Inequality(HoudiniEngineUnity.JSONLazyCreator a, System.Object b) { }
        // RVA: 0x09DC0F98  token: 0x6000C7C
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x011EC580  token: 0x6000C7D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09DC0FA8  token: 0x6000C8A
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode) { }

    }

    // TypeToken: 0x2000166  // size: 0x10
    public static class JSON
    {
        // Methods
        // RVA: 0x09DC2ABC  token: 0x6000C8B
        public static HoudiniEngineUnity.JSONNode Parse(System.String aJSON) { }

    }

    // TypeToken: 0x2000167  // size: 0x14
    public sealed struct JSONContainerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.JSONContainerType Array;  // const
        public static HoudiniEngineUnity.JSONContainerType Object;  // const

    }

    // TypeToken: 0x2000168  // size: 0x10
    public static class HEU_TerrainUtility
    {
        // Methods
        // RVA: 0x09DB9884  token: 0x6000C8C
        public static System.Boolean GenerateTerrainFromVolume(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject, UnityEngine.TerrainData& terrainData, UnityEngine.Vector3& volumePositionOffset, UnityEngine.Terrain& terrain, System.String bakedMaterialPath) { }
        // RVA: 0x09DBC14C  token: 0x6000C8D
        public static System.Void SetTerrainMaterial(UnityEngine.Terrain terrain, System.String specifiedMaterialName, System.String bakedMaterialPath) { }
        // RVA: 0x09DBA2B0  token: 0x6000C8E
        public static System.String GetDefaultTerrainShaderName() { }
        // RVA: 0x09DBA230  token: 0x6000C8F
        public static System.String GetDefaultTerrainMaterialPath() { }
        // RVA: 0x09DBA6E8  token: 0x6000C90
        public static System.Single[] GetNormalizedHeightmapFromPartWithMinMax(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Single& minHeight, System.Single& maxHeight, System.Single& heightRange, System.Boolean bUseHeightRangeOverride) { }
        // RVA: 0x09DBA330  token: 0x6000C91
        public static System.Int32[,] GetDetailMapFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32& detailResolution) { }
        // RVA: 0x09DBA570  token: 0x6000C92
        public static System.Boolean GetHeightmapFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 xLength, System.Int32 yLength, System.Int32 geoID, System.Int32 partID, System.Single[]& heightValues, System.Single& minHeight, System.Single& maxHeight) { }
        // RVA: 0x09DB97A8  token: 0x6000C93
        public static System.Single[,] ConvertHeightMapHoudiniToUnity(System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Single[] heightValues) { }
        // RVA: 0x09DB9664  token: 0x6000C94
        public static System.Single[,,] ConvertHeightFieldToAlphaMap(System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Collections.Generic.List<System.Single[]> heightFields) { }
        // RVA: 0x09DB8BA0  token: 0x6000C95
        public static System.Single[,,] AppendConvertedHeightFieldToAlphaMap(System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Single[,,] existingAlphaMaps, System.Collections.Generic.List<System.Single[]> heightFields, System.Single[] strengths, System.Collections.Generic.List<System.Int32> alphaMapIndices) { }
        // RVA: 0x09DBAB28  token: 0x6000C96
        public static UnityEngine.Vector3 GetVolumePositionOffset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.Vector3 volumePosition, System.Single terrainSizeX, System.Single heightMapSize, System.Int32 mapWidth, System.Int32 mapHeight, System.Single minHeight) { }
        // RVA: 0x09DBAA34  token: 0x6000C97
        public static System.Collections.Generic.List<HoudiniEngineUnity.HEU_TreePrototypeInfo> GetTreePrototypeInfosFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09DBB308  token: 0x6000C98
        public static System.Void PopulateScatterTrees(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32 pointCount, HoudiniEngineUnity.HEU_VolumeScatterTrees& scatterTrees, System.Boolean throwWarningIfNoTileAttribute) { }
        // RVA: 0x09DB9178  token: 0x6000C99
        public static System.Void ApplyScatterTrees(UnityEngine.TerrainData terrainData, HoudiniEngineUnity.HEU_VolumeScatterTrees scatterTrees, System.Int32 tileIndex) { }
        // RVA: 0x09DBAEE4  token: 0x6000C9A
        public static System.Void PopulateDetailPrototype(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_DetailPrototype& detailPrototype) { }
        // RVA: 0x09DBAC14  token: 0x6000C9B
        public static System.Void PopulateDetailProperties(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_DetailProperties& detailProperties) { }
        // RVA: 0x09DB8D98  token: 0x6000C9C
        public static System.Void ApplyDetailLayers(UnityEngine.Terrain terrain, UnityEngine.TerrainData terrainData, HoudiniEngineUnity.HEU_DetailProperties detailProperties, System.Collections.Generic.List<HoudiniEngineUnity.HEU_DetailPrototype> heuDetailPrototypes, System.Collections.Generic.List<System.Int32[,]> convertedDetailMaps) { }
        // RVA: 0x09DBA830  token: 0x6000C9D
        private static System.Int32 GetTerrainLayerIndexByName(System.String layerName, UnityEngine.TerrainLayer[] terrainLayers) { }
        // RVA: 0x09DBA99C  token: 0x6000C9E
        public static System.Int32 GetTerrainLayerIndex(UnityEngine.TerrainLayer layer, UnityEngine.TerrainLayer[] terrainLayers) { }
        // RVA: 0x09DBC2B8  token: 0x6000C9F
        public static System.Boolean VolumeLayerHasAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09DBA434  token: 0x6000CA0
        public static HoudiniEngineUnity.HFLayerType GetHeightfieldLayerType(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String volumeName) { }
        // RVA: 0x09DBA3C0  token: 0x6000CA1
        public static System.Single GetHeightRangeFromHeightfield(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09DBA768  token: 0x6000CA2
        public static System.String GetTerrainDataExportPathFromHeightfieldAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID) { }
        // RVA: 0x09DBBEB4  token: 0x6000CA3
        public static System.Single[] ResampleData(System.Single[] data, System.Int32 oldWidth, System.Int32 oldHeight, System.Int32 newWidth, System.Int32 newHeight) { }
        // RVA: 0x09DBA1C0  token: 0x6000CA4
        public static System.Boolean GetAttributeTile(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32& outTileAttribute) { }

    }

    // TypeToken: 0x2000169
    public interface IEquivable`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000CA5
        public virtual System.Boolean IsEquivalentTo(T other) { }

    }

    // TypeToken: 0x200016A
    public interface IEquivableWrapperClass`1 : HoudiniEngineUnity.IEquivable`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000CA6
        public virtual System.Boolean IsNull() { }

    }

    // TypeToken: 0x200016B  // size: 0x10
    public class HEU_TestHelpers
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000CA7
        public static System.Boolean AssertTrueLogEquivalent(T a, T b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3, HoudiniEngineUnity.HEU_TestHelpers.RequireStruct<T> _) { }
        // RVA: 0x09DBC5AC  token: 0x6000CA8
        public static System.Boolean AssertTrueLogEquivalent(UnityEngine.GameObject a, UnityEngine.GameObject b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CA9
        public static System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivable<T> a, HoudiniEngineUnity.IEquivable<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CAA
        public static System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivableWrapperClass<T> a, HoudiniEngineUnity.IEquivableWrapperClass<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: 0x09DBC6A8  token: 0x6000CAB
        public static System.Boolean AssertTrueLogEquivalent(System.String a, System.String b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CAC
        public static System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<T> a, System.Collections.Generic.List<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3, HoudiniEngineUnity.HEU_TestHelpers.RequireStruct<T> _) { }
        // RVA: -1  // generic def  token: 0x6000CAD
        public static System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<T> a, System.Collections.Generic.List<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3, HoudiniEngineUnity.HEU_TestHelpers.RequireClass<T> _) { }
        // RVA: -1  // generic def  token: 0x6000CAE
        public static System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<HoudiniEngineUnity.IEquivable<T>> a, System.Collections.Generic.List<HoudiniEngineUnity.IEquivable<T>> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CAF
        public static System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<HoudiniEngineUnity.IEquivableWrapperClass<T>> a, System.Collections.Generic.List<HoudiniEngineUnity.IEquivableWrapperClass<T>> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CB0
        public static System.Boolean AssertTrueLogEquivalent(T[] a, T[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: 0x09DBC448  token: 0x6000CB1
        public static System.Boolean AssertTrueLogEquivalent(System.String[] a, System.String[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CB2
        public static System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivable<T>[] a, HoudiniEngineUnity.IEquivable<T>[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CB3
        public static System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivableWrapperClass<T>[] a, HoudiniEngineUnity.IEquivableWrapperClass<T>[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: 0x09DBC778  token: 0x6000CB4
        public static System.Void PrintTestLogAndSetResult(System.Boolean expression, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3) { }
        // RVA: -1  // generic def  token: 0x6000CB5
        public static System.Boolean ShouldBeTested(T a, T b, System.Boolean& bResult, System.String header, System.String subject) { }
        // RVA: 0x09DBC8BC  token: 0x6000CB6
        public static System.Boolean ShouldBeTested(UnityEngine.GameObject a, UnityEngine.GameObject b, System.Boolean& bResult, System.String header, System.String subject) { }
        // RVA: -1  // generic def  token: 0x6000CB7
        public static System.Boolean ShouldBeTested(HoudiniEngineUnity.IEquivable<T> a, HoudiniEngineUnity.IEquivable<T> b, System.Boolean& bResult, System.String header, System.String subject) { }
        // RVA: -1  // generic def  token: 0x6000CB8
        public static System.Boolean ShouldBeTested(HoudiniEngineUnity.IEquivableWrapperClass<T> a, HoudiniEngineUnity.IEquivableWrapperClass<T> b, System.Boolean& bResult, System.String header, System.String subject) { }
        // RVA: -1  // generic def  token: 0x6000CB9
        private static System.Boolean ShouldBeTested(System.Collections.Generic.List<T> a, System.Collections.Generic.List<T> b, System.Boolean& bResult, System.String header, System.String subject) { }
        // RVA: -1  // generic def  token: 0x6000CBA
        public static System.Boolean ShouldBeTested(T[] a, T[] b, System.Boolean& bResult, System.String header, System.String subject) { }
        // RVA: 0x09DBCB04  token: 0x6000CBB
        public static System.Boolean ShouldBeTested(System.String a, System.String b, System.Boolean& bResult, System.String header, System.String subject) { }
        // RVA: 0x09DBCB98  token: 0x6000CBC
        public static System.Boolean TestOutputObjectEquivalence(UnityEngine.GameObject a, UnityEngine.GameObject b) { }
        // RVA: 0x041E1670  token: 0x6000CBD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200016E  // size: 0x50
    public class Test_HAPI_AssetInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_AssetInfo self;  // 0x10

        // Methods
        // RVA: 0x04D997E0  token: 0x6000CC0
        public System.Void .ctor(HoudiniEngineUnity.HAPI_AssetInfo self) { }
        // RVA: 0x09DC45F0  token: 0x6000CC1
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_AssetInfo other) { }

    }

    // TypeToken: 0x200016F  // size: 0x10
    public static class Test_HAPI_AssetInfo_Extensions
    {
        // Methods
        // RVA: 0x09DC458C  token: 0x6000CC2
        public static HoudiniEngineUnity.Test_HAPI_AssetInfo ToTestObject(HoudiniEngineUnity.HAPI_AssetInfo self) { }

    }

    // TypeToken: 0x2000170  // size: 0x58
    public class Test_HAPI_NodeInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_NodeInfo self;  // 0x10

        // Methods
        // RVA: 0x04DA2DB0  token: 0x6000CC3
        public System.Void .ctor(HoudiniEngineUnity.HAPI_NodeInfo self) { }
        // RVA: 0x09DC4C00  token: 0x6000CC4
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_NodeInfo other) { }

    }

    // TypeToken: 0x2000171  // size: 0x10
    public static class Test_HAPI_NodeInfo_Extensions
    {
        // Methods
        // RVA: 0x09DC4B90  token: 0x6000CC5
        public static HoudiniEngineUnity.Test_HAPI_NodeInfo ToTestObject(HoudiniEngineUnity.HAPI_NodeInfo self) { }

    }

    // TypeToken: 0x2000172  // size: 0x30
    public class Test_HAPI_ObjectInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_ObjectInfo self;  // 0x10

        // Methods
        // RVA: 0x04D99850  token: 0x6000CC6
        public System.Void .ctor(HoudiniEngineUnity.HAPI_ObjectInfo self) { }
        // RVA: 0x09DC4E68  token: 0x6000CC7
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_ObjectInfo other) { }

    }

    // TypeToken: 0x2000173  // size: 0x10
    public static class Test_HAPI_ObjectInfo_Extensions
    {
        // Methods
        // RVA: 0x09DC4E08  token: 0x6000CC8
        public static HoudiniEngineUnity.Test_HAPI_ObjectInfo ToTestObject(HoudiniEngineUnity.HAPI_ObjectInfo self) { }

    }

    // TypeToken: 0x2000174  // size: 0x38
    public class Test_HAPI_Transform : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_Transform self;  // 0x10

        // Methods
        // RVA: 0x09DC5750  token: 0x6000CC9
        public System.Void .ctor(HoudiniEngineUnity.HAPI_Transform self) { }
        // RVA: 0x09DC57F4  token: 0x6000CCA
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_Transform other) { }

    }

    // TypeToken: 0x2000175  // size: 0x10
    public static class Test_HAPI_Transform_Extensions
    {
        // Methods
        // RVA: 0x09DC5774  token: 0x6000CCB
        public static HoudiniEngineUnity.Test_HAPI_Transform ToTestObject(HoudiniEngineUnity.HAPI_Transform self) { }

    }

    // TypeToken: 0x2000176  // size: 0x38
    public class Test_HAPI_GeoInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_GeoInfo self;  // 0x10

        // Methods
        // RVA: 0x04D997C0  token: 0x6000CCC
        public System.Void .ctor(HoudiniEngineUnity.HAPI_GeoInfo self) { }
        // RVA: 0x09DC498C  token: 0x6000CCD
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_GeoInfo other) { }

    }

    // TypeToken: 0x2000177  // size: 0x10
    public static class Test_HAPI_GeoInfo_Extensions
    {
        // Methods
        // RVA: 0x09DC492C  token: 0x6000CCE
        public static HoudiniEngineUnity.Test_HAPI_GeoInfo ToTestObject(HoudiniEngineUnity.HAPI_GeoInfo self) { }

    }

    // TypeToken: 0x2000178  // size: 0x38
    public class Test_HAPI_AttributeInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_AttributeInfo self;  // 0x10

        // Methods
        // RVA: 0x04DA2D90  token: 0x6000CCF
        public System.Void .ctor(HoudiniEngineUnity.HAPI_AttributeInfo self) { }
        // RVA: 0x09DC4828  token: 0x6000CD0
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_AttributeInfo other) { }

    }

    // TypeToken: 0x2000179  // size: 0x10
    public static class Test_HAPI_AttributeInfo_Extensions
    {
        // Methods
        // RVA: 0x09DC47CC  token: 0x6000CD1
        public static HoudiniEngineUnity.Test_HAPI_AttributeInfo ToTestObject(HoudiniEngineUnity.HAPI_AttributeInfo self) { }

    }

    // TypeToken: 0x200017A  // size: 0x38
    public class Test_HAPI_TransformEuler : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_TransformEuler self;  // 0x10

        // Methods
        // RVA: 0x09DC5750  token: 0x6000CD2
        public System.Void .ctor(HoudiniEngineUnity.HAPI_TransformEuler self) { }
        // RVA: 0x09DC5678  token: 0x6000CD3
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_TransformEuler other) { }

    }

    // TypeToken: 0x200017B  // size: 0x10
    public static class Test_HAPI_TransformEuler_Extensions
    {
        // Methods
        // RVA: 0x09DC55F8  token: 0x6000CD4
        public static HoudiniEngineUnity.Test_HAPI_TransformEuler ToTestObject(HoudiniEngineUnity.HAPI_TransformEuler self) { }

    }

    // TypeToken: 0x200017C  // size: 0xA8
    public class Test_HAPI_ParmInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public HoudiniEngineUnity.HAPI_ParmInfo self;  // 0x10

        // Methods
        // RVA: 0x04DA2DE0  token: 0x6000CD5
        public System.Void .ctor(HoudiniEngineUnity.HAPI_ParmInfo self) { }
        // RVA: 0x09DC5034  token: 0x6000CD6
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_ParmInfo other) { }

    }

    // TypeToken: 0x200017D  // size: 0x10
    public static class Test_HAPI_ParmInfo_Extensions
    {
        // Methods
        // RVA: 0x09DC4F94  token: 0x6000CD7
        public static HoudiniEngineUnity.Test_HAPI_ParmInfo ToTestObject(HoudiniEngineUnity.HAPI_ParmInfo self) { }

    }

    // TypeToken: 0x200017E  // size: 0x18
    public class Test_LODGroup : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.LODGroup self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000CD8
        public System.Void .ctor(UnityEngine.LODGroup self) { }
        // RVA: 0x09DC5D18  token: 0x6000CD9
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC5AF8  token: 0x6000CDA
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_LODGroup other) { }

    }

    // TypeToken: 0x200017F  // size: 0x10
    public static class Test_LODGroup_Extensions
    {
        // Methods
        // RVA: 0x09DC5888  token: 0x6000CDB
        public static HoudiniEngineUnity.Test_LODGroup ToTestObject(UnityEngine.LODGroup self) { }
        // RVA: 0x09DC59F4  token: 0x6000CDC
        public static HoudiniEngineUnity.Test_LODGroup[] ToTestObject(UnityEngine.LODGroup[] self) { }
        // RVA: 0x09DC58E8  token: 0x6000CDD
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_LODGroup> ToTestObject(System.Collections.Generic.List<UnityEngine.LODGroup> self) { }

    }

    // TypeToken: 0x2000181  // size: 0x18
    public class Test_Transform : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Transform self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000CE2
        public System.Void .ctor(UnityEngine.Transform self) { }
        // RVA: 0x09DC934C  token: 0x6000CE3
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC91C8  token: 0x6000CE4
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Transform other) { }

    }

    // TypeToken: 0x2000182  // size: 0x10
    public static class Test_Transform_Extensions
    {
        // Methods
        // RVA: 0x09DC8F58  token: 0x6000CE5
        public static HoudiniEngineUnity.Test_Transform ToTestObject(UnityEngine.Transform self) { }
        // RVA: 0x09DC90C4  token: 0x6000CE6
        public static HoudiniEngineUnity.Test_Transform[] ToTestObject(UnityEngine.Transform[] self) { }
        // RVA: 0x09DC8FB8  token: 0x6000CE7
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_Transform> ToTestObject(System.Collections.Generic.List<UnityEngine.Transform> self) { }

    }

    // TypeToken: 0x2000184  // size: 0x18
    public class Test_Material : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Material self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000CEC
        public System.Void .ctor(UnityEngine.Material self) { }
        // RVA: 0x09DC62AC  token: 0x6000CED
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC60D8  token: 0x6000CEE
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Material other) { }

    }

    // TypeToken: 0x2000185  // size: 0x10
    public static class Test_Material_Extensions
    {
        // Methods
        // RVA: 0x09DC5F4C  token: 0x6000CEF
        public static HoudiniEngineUnity.Test_Material ToTestObject(UnityEngine.Material self) { }
        // RVA: 0x09DC5FAC  token: 0x6000CF0
        public static HoudiniEngineUnity.Test_Material[] ToTestObject(UnityEngine.Material[] self) { }
        // RVA: 0x09DC5E40  token: 0x6000CF1
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_Material> ToTestObject(System.Collections.Generic.List<UnityEngine.Material> self) { }

    }

    // TypeToken: 0x2000187  // size: 0x18
    public class Test_Collider : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Collider self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000CF6
        public System.Void .ctor(UnityEngine.Collider self) { }
        // RVA: 0x09DC41D0  token: 0x6000CF7
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC3D54  token: 0x6000CF8
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Collider other) { }

    }

    // TypeToken: 0x2000188  // size: 0x10
    public static class Test_Collider_Extensions
    {
        // Methods
        // RVA: 0x09DC3CF4  token: 0x6000CF9
        public static HoudiniEngineUnity.Test_Collider ToTestObject(UnityEngine.Collider self) { }
        // RVA: 0x09DC3BF0  token: 0x6000CFA
        public static HoudiniEngineUnity.Test_Collider[] ToTestObject(UnityEngine.Collider[] self) { }
        // RVA: 0x09DC3AE4  token: 0x6000CFB
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_Collider> ToTestObject(System.Collections.Generic.List<UnityEngine.Collider> self) { }

    }

    // TypeToken: 0x200018A  // size: 0x18
    public class Test_BoxCollider : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.BoxCollider self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D00
        public System.Void .ctor(UnityEngine.BoxCollider self) { }
        // RVA: 0x09DC364C  token: 0x6000D01
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC34C0  token: 0x6000D02
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_BoxCollider other) { }

    }

    // TypeToken: 0x200018B  // size: 0x10
    public static class Test_BoxCollider_Extensions
    {
        // Methods
        // RVA: 0x09DC3460  token: 0x6000D03
        public static HoudiniEngineUnity.Test_BoxCollider ToTestObject(UnityEngine.BoxCollider self) { }
        // RVA: 0x09DC3250  token: 0x6000D04
        public static HoudiniEngineUnity.Test_BoxCollider[] ToTestObject(UnityEngine.BoxCollider[] self) { }
        // RVA: 0x09DC3354  token: 0x6000D05
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_BoxCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.BoxCollider> self) { }

    }

    // TypeToken: 0x200018D  // size: 0x18
    public class Test_SphereCollider : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.SphereCollider self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D0A
        public System.Void .ctor(UnityEngine.SphereCollider self) { }
        // RVA: 0x09DC78F0  token: 0x6000D0B
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC7788  token: 0x6000D0C
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_SphereCollider other) { }

    }

    // TypeToken: 0x200018E  // size: 0x10
    public static class Test_SphereCollider_Extensions
    {
        // Methods
        // RVA: 0x09DC761C  token: 0x6000D0D
        public static HoudiniEngineUnity.Test_SphereCollider ToTestObject(UnityEngine.SphereCollider self) { }
        // RVA: 0x09DC7518  token: 0x6000D0E
        public static HoudiniEngineUnity.Test_SphereCollider[] ToTestObject(UnityEngine.SphereCollider[] self) { }
        // RVA: 0x09DC767C  token: 0x6000D0F
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_SphereCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.SphereCollider> self) { }

    }

    // TypeToken: 0x2000190  // size: 0x18
    public class Test_CapsuleCollider : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.CapsuleCollider self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D14
        public System.Void .ctor(UnityEngine.CapsuleCollider self) { }
        // RVA: 0x09DC3A94  token: 0x6000D15
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC390C  token: 0x6000D16
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_CapsuleCollider other) { }

    }

    // TypeToken: 0x2000191  // size: 0x10
    public static class Test_CapsuleCollider_Extensions
    {
        // Methods
        // RVA: 0x09DC369C  token: 0x6000D17
        public static HoudiniEngineUnity.Test_CapsuleCollider ToTestObject(UnityEngine.CapsuleCollider self) { }
        // RVA: 0x09DC3808  token: 0x6000D18
        public static HoudiniEngineUnity.Test_CapsuleCollider[] ToTestObject(UnityEngine.CapsuleCollider[] self) { }
        // RVA: 0x09DC36FC  token: 0x6000D19
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_CapsuleCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.CapsuleCollider> self) { }

    }

    // TypeToken: 0x2000193  // size: 0x18
    public class Test_MeshCollider : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.MeshCollider self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D1E
        public System.Void .ctor(UnityEngine.MeshCollider self) { }
        // RVA: 0x09DC6644  token: 0x6000D1F
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC656C  token: 0x6000D20
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_MeshCollider other) { }

    }

    // TypeToken: 0x2000194  // size: 0x10
    public static class Test_MeshCollider_Extensions
    {
        // Methods
        // RVA: 0x09DC62FC  token: 0x6000D21
        public static HoudiniEngineUnity.Test_MeshCollider ToTestObject(UnityEngine.MeshCollider self) { }
        // RVA: 0x09DC635C  token: 0x6000D22
        public static HoudiniEngineUnity.Test_MeshCollider[] ToTestObject(UnityEngine.MeshCollider[] self) { }
        // RVA: 0x09DC6460  token: 0x6000D23
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_MeshCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.MeshCollider> self) { }

    }

    // TypeToken: 0x2000196  // size: 0x18
    public class Test_Mesh : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Mesh self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D28
        public System.Void .ctor(UnityEngine.Mesh self) { }
        // RVA: 0x09DC74C8  token: 0x6000D29
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC710C  token: 0x6000D2A
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Mesh other) { }

    }

    // TypeToken: 0x2000197  // size: 0x10
    public static class Test_Mesh_Extensions
    {
        // Methods
        // RVA: 0x09DC6E9C  token: 0x6000D2B
        public static HoudiniEngineUnity.Test_Mesh ToTestObject(UnityEngine.Mesh self) { }
        // RVA: 0x09DC7008  token: 0x6000D2C
        public static HoudiniEngineUnity.Test_Mesh[] ToTestObject(UnityEngine.Mesh[] self) { }
        // RVA: 0x09DC6EFC  token: 0x6000D2D
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_Mesh> ToTestObject(System.Collections.Generic.List<UnityEngine.Mesh> self) { }

    }

    // TypeToken: 0x2000199  // size: 0x18
    public class Test_MeshRenderer : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.MeshRenderer self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D32
        public System.Void .ctor(UnityEngine.MeshRenderer self) { }
        // RVA: 0x09DC6E4C  token: 0x6000D33
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC6D28  token: 0x6000D34
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_MeshRenderer other) { }

    }

    // TypeToken: 0x200019A  // size: 0x10
    public static class Test_MeshRenderer_Extensions
    {
        // Methods
        // RVA: 0x09DC6AB8  token: 0x6000D35
        public static HoudiniEngineUnity.Test_MeshRenderer ToTestObject(UnityEngine.MeshRenderer self) { }
        // RVA: 0x09DC6C24  token: 0x6000D36
        public static HoudiniEngineUnity.Test_MeshRenderer[] ToTestObject(UnityEngine.MeshRenderer[] self) { }
        // RVA: 0x09DC6B18  token: 0x6000D37
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_MeshRenderer> ToTestObject(System.Collections.Generic.List<UnityEngine.MeshRenderer> self) { }

    }

    // TypeToken: 0x200019C  // size: 0x18
    public class Test_MeshFilter : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.MeshFilter self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D3C
        public System.Void .ctor(UnityEngine.MeshFilter self) { }
        // RVA: 0x09DC6A68  token: 0x6000D3D
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC6904  token: 0x6000D3E
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_MeshFilter other) { }

    }

    // TypeToken: 0x200019D  // size: 0x10
    public static class Test_MeshFilter_Extensions
    {
        // Methods
        // RVA: 0x09DC68A4  token: 0x6000D3F
        public static HoudiniEngineUnity.Test_MeshFilter ToTestObject(UnityEngine.MeshFilter self) { }
        // RVA: 0x09DC67A0  token: 0x6000D40
        public static HoudiniEngineUnity.Test_MeshFilter[] ToTestObject(UnityEngine.MeshFilter[] self) { }
        // RVA: 0x09DC6694  token: 0x6000D41
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_MeshFilter> ToTestObject(System.Collections.Generic.List<UnityEngine.MeshFilter> self) { }

    }

    // TypeToken: 0x200019F  // size: 0x18
    public class Test_LayerMask : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.LayerMask self;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x6000D46
        public System.Void .ctor(UnityEngine.LayerMask self) { }
        // RVA: 0x09DC5DAC  token: 0x6000D47
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_LayerMask other) { }

    }

    // TypeToken: 0x20001A0  // size: 0x10
    public static class Test_LayerMask_Extensions
    {
        // Methods
        // RVA: 0x09DC5D68  token: 0x6000D48
        public static HoudiniEngineUnity.Test_LayerMask ToTestObject(UnityEngine.LayerMask self) { }

    }

    // TypeToken: 0x20001A1  // size: 0x18
    public class Test_Gradient : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Gradient self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D49
        public System.Void .ctor(UnityEngine.Gradient self) { }
        // RVA: 0x03BA32C0  token: 0x6000D4A
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC4490  token: 0x6000D4B
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Gradient other) { }

    }

    // TypeToken: 0x20001A2  // size: 0x10
    public static class Test_Gradient_Extensions
    {
        // Methods
        // RVA: 0x09DC4430  token: 0x6000D4C
        public static HoudiniEngineUnity.Test_Gradient ToTestObject(UnityEngine.Gradient self) { }
        // RVA: 0x09DC432C  token: 0x6000D4D
        public static HoudiniEngineUnity.Test_Gradient[] ToTestObject(UnityEngine.Gradient[] self) { }
        // RVA: 0x09DC4220  token: 0x6000D4E
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_Gradient> ToTestObject(System.Collections.Generic.List<UnityEngine.Gradient> self) { }

    }

    // TypeToken: 0x20001A4  // size: 0x18
    public class Test_AnimationCurve : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.AnimationCurve self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D53
        public System.Void .ctor(UnityEngine.AnimationCurve self) { }
        // RVA: 0x03BA32C0  token: 0x6000D54
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC3164  token: 0x6000D55
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_AnimationCurve other) { }

    }

    // TypeToken: 0x20001A5  // size: 0x10
    public static class Test_AnimationCurve_Extensions
    {
        // Methods
        // RVA: 0x09DC2EF4  token: 0x6000D56
        public static HoudiniEngineUnity.Test_AnimationCurve ToTestObject(UnityEngine.AnimationCurve self) { }
        // RVA: 0x09DC2F54  token: 0x6000D57
        public static HoudiniEngineUnity.Test_AnimationCurve[] ToTestObject(UnityEngine.AnimationCurve[] self) { }
        // RVA: 0x09DC3058  token: 0x6000D58
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_AnimationCurve> ToTestObject(System.Collections.Generic.List<UnityEngine.AnimationCurve> self) { }

    }

    // TypeToken: 0x20001A7  // size: 0x18
    public class Test_TerrainLayer : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.TerrainLayer self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D5D
        public System.Void .ctor(UnityEngine.TerrainLayer self) { }
        // RVA: 0x09DC8710  token: 0x6000D5E
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC8348  token: 0x6000D5F
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_TerrainLayer other) { }

    }

    // TypeToken: 0x20001A8  // size: 0x10
    public static class Test_TerrainLayer_Extensions
    {
        // Methods
        // RVA: 0x09DC80D8  token: 0x6000D60
        public static HoudiniEngineUnity.Test_TerrainLayer ToTestObject(UnityEngine.TerrainLayer self) { }
        // RVA: 0x09DC8244  token: 0x6000D61
        public static HoudiniEngineUnity.Test_TerrainLayer[] ToTestObject(UnityEngine.TerrainLayer[] self) { }
        // RVA: 0x09DC8138  token: 0x6000D62
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_TerrainLayer> ToTestObject(System.Collections.Generic.List<UnityEngine.TerrainLayer> self) { }

    }

    // TypeToken: 0x20001AA  // size: 0x18
    public class Test_Texture2D : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Texture2D self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D67
        public System.Void .ctor(UnityEngine.Texture2D self) { }
        // RVA: 0x09DC8F08  token: 0x6000D68
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC8DDC  token: 0x6000D69
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Texture2D other) { }

    }

    // TypeToken: 0x20001AB  // size: 0x10
    public static class Test_Texture2D_Extensions
    {
        // Methods
        // RVA: 0x09DC8B6C  token: 0x6000D6A
        public static HoudiniEngineUnity.Test_Texture2D ToTestObject(UnityEngine.Texture2D self) { }
        // RVA: 0x09DC8BCC  token: 0x6000D6B
        public static HoudiniEngineUnity.Test_Texture2D[] ToTestObject(UnityEngine.Texture2D[] self) { }
        // RVA: 0x09DC8CD0  token: 0x6000D6C
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_Texture2D> ToTestObject(System.Collections.Generic.List<UnityEngine.Texture2D> self) { }

    }

    // TypeToken: 0x20001AD  // size: 0x38
    public class Test_TreeInstance : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.TreeInstance self;  // 0x10

        // Methods
        // RVA: 0x04DA2D90  token: 0x6000D71
        public System.Void .ctor(UnityEngine.TreeInstance self) { }
        // RVA: 0x09DC9608  token: 0x6000D72
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_TreeInstance other) { }

    }

    // TypeToken: 0x20001AE  // size: 0x10
    public static class Test_TreeInstance_Extensions
    {
        // Methods
        // RVA: 0x09DC94A0  token: 0x6000D73
        public static HoudiniEngineUnity.Test_TreeInstance ToTestObject(UnityEngine.TreeInstance self) { }
        // RVA: 0x09DC939C  token: 0x6000D74
        public static HoudiniEngineUnity.Test_TreeInstance[] ToTestObject(UnityEngine.TreeInstance[] self) { }
        // RVA: 0x09DC94FC  token: 0x6000D75
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_TreeInstance> ToTestObject(System.Collections.Generic.List<UnityEngine.TreeInstance> self) { }

    }

    // TypeToken: 0x20001B0  // size: 0x18
    public class Test_TerrainData : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.TerrainData self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D7A
        public System.Void .ctor(UnityEngine.TerrainData self) { }
        // RVA: 0x09DC8088  token: 0x6000D7B
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC7BB0  token: 0x6000D7C
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_TerrainData other) { }

    }

    // TypeToken: 0x20001B1  // size: 0x10
    public static class Test_TerrainData_Extensions
    {
        // Methods
        // RVA: 0x09DC7B50  token: 0x6000D7D
        public static HoudiniEngineUnity.Test_TerrainData ToTestObject(UnityEngine.TerrainData self) { }
        // RVA: 0x09DC7A4C  token: 0x6000D7E
        public static HoudiniEngineUnity.Test_TerrainData[] ToTestObject(UnityEngine.TerrainData[] self) { }
        // RVA: 0x09DC7940  token: 0x6000D7F
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_TerrainData> ToTestObject(System.Collections.Generic.List<UnityEngine.TerrainData> self) { }

    }

    // TypeToken: 0x20001B3  // size: 0x18
    public class Test_Terrain : HoudiniEngineUnity.IEquivableWrapperClass`1, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public UnityEngine.Terrain self;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000D84
        public System.Void .ctor(UnityEngine.Terrain self) { }
        // RVA: 0x09DC8B1C  token: 0x6000D85
        public virtual System.Boolean IsNull() { }
        // RVA: 0x09DC89D0  token: 0x6000D86
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Terrain other) { }

    }

    // TypeToken: 0x20001B4  // size: 0x10
    public static class Test_Terrain_Extensions
    {
        // Methods
        // RVA: 0x09DC8760  token: 0x6000D87
        public static HoudiniEngineUnity.Test_Terrain ToTestObject(UnityEngine.Terrain self) { }
        // RVA: 0x09DC87C0  token: 0x6000D88
        public static HoudiniEngineUnity.Test_Terrain[] ToTestObject(UnityEngine.Terrain[] self) { }
        // RVA: 0x09DC88C4  token: 0x6000D89
        public static System.Collections.Generic.List<HoudiniEngineUnity.Test_Terrain> ToTestObject(System.Collections.Generic.List<UnityEngine.Terrain> self) { }

    }

    // TypeToken: 0x20001B6  // size: 0xA8
    public class HEU_ToolsInfo : UnityEngine.ScriptableObject, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.Single _paintBrushSize;  // 0x18
        public System.Single _paintBrushOpacity;  // 0x1c
        public System.Int32[] _paintIntValue;  // 0x20
        public System.Single[] _paintFloatValue;  // 0x28
        public System.String[] _paintStringValue;  // 0x30
        public System.Int32 _lastAttributesGeoID;  // 0x38
        public System.Int32 _lastAttributesPartID;  // 0x3c
        public System.String _lastAttributeNodeName;  // 0x40
        public System.String _lastAttributeName;  // 0x48
        public UnityEngine.Color _brushHandleColor;  // 0x50
        public UnityEngine.Color _affectedAreaPaintColor;  // 0x60
        public System.Boolean _liveUpdate;  // 0x70
        public System.Boolean _isPainting;  // 0x71
        public System.Single _editPointBoxSize;  // 0x74
        public UnityEngine.Color _editPointBoxUnselectedColor;  // 0x78
        public UnityEngine.Color _editPointBoxSelectedColor;  // 0x88
        public System.Boolean _recacheRequired;  // 0x98
        public HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode _paintMergeMode;  // 0x9c
        public System.Boolean _showOnlyEditGeometry;  // 0xa0
        public System.Boolean _alwaysCookUpstream;  // 0xa1
        public HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibility _paintMeshVisiblity;  // 0xa4

        // Methods
        // RVA: 0x09DBD1F0  token: 0x6000D8E
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ToolsInfo other) { }
        // RVA: 0x09DBD734  token: 0x6000D8F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001B9  // size: 0x14
    public sealed struct HFLayerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static HoudiniEngineUnity.HFLayerType DEFAULT;  // const
        public static HoudiniEngineUnity.HFLayerType HEIGHT;  // const
        public static HoudiniEngineUnity.HFLayerType MASK;  // const
        public static HoudiniEngineUnity.HFLayerType DETAIL;  // const

    }

    // TypeToken: 0x20001BA  // size: 0x50
    public class HEU_VolumeLayer : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.String _layerName;  // 0x10
        public System.Single _strength;  // 0x18
        public System.Boolean _uiExpanded;  // 0x1c
        public System.Int32 _tile;  // 0x20
        public System.Int32 _xLength;  // 0x24
        public System.Int32 _yLength;  // 0x28
        public System.Boolean _hasLayerAttributes;  // 0x2c
        public UnityEngine.TerrainLayer _terrainLayer;  // 0x30
        public HoudiniEngineUnity.HFLayerType _layerType;  // 0x38
        public HoudiniEngineUnity.HEU_DetailPrototype _detailPrototype;  // 0x40
        private HoudiniEngineUnity.HEU_PartData _part;  // 0x48

        // Methods
        // RVA: 0x09DBFFDC  token: 0x6000D90
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_VolumeLayer other) { }
        // RVA: 0x04D8CF30  token: 0x6000D91
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001BB  // size: 0x58
    public class HEU_VolumeScatterTrees : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.Collections.Generic.List<HoudiniEngineUnity.HEU_TreePrototypeInfo> _treePrototypInfos;  // 0x10
        public UnityEngine.Color32[] _colors;  // 0x18
        public System.Single[] _heightScales;  // 0x20
        public UnityEngine.Color32[] _lightmapColors;  // 0x28
        public UnityEngine.Vector3[] _positions;  // 0x30
        public System.Int32[] _prototypeIndices;  // 0x38
        public System.Single[] _rotations;  // 0x40
        public System.Single[] _widthScales;  // 0x48
        public System.Int32[] _terrainTiles;  // 0x50

        // Methods
        // RVA: 0x09DC02B8  token: 0x6000D92
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_VolumeScatterTrees other) { }
        // RVA: 0x041E1670  token: 0x6000D93
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001BC  // size: 0x20
    public class HEU_TreePrototypeInfo : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.String _prefabPath;  // 0x10
        public System.Single _bendfactor;  // 0x18

        // Methods
        // RVA: 0x09DBD834  token: 0x6000D94
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_TreePrototypeInfo other) { }
        // RVA: 0x041E1670  token: 0x6000D95
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001BD  // size: 0x20
    public class HEU_DetailProperties : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.Single _detailDistance;  // 0x10
        public System.Single _detailDensity;  // 0x14
        public System.Int32 _detailResolution;  // 0x18
        public System.Int32 _detailResolutionPerPatch;  // 0x1c

        // Methods
        // RVA: 0x09DB88BC  token: 0x6000D96
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_DetailProperties other) { }
        // RVA: 0x04DA2D00  token: 0x6000D97
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001BE  // size: 0x60
    public class HEU_DetailPrototype : HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        public System.String _prototypePrefab;  // 0x10
        public System.String _prototypeTexture;  // 0x18
        public System.Single _bendFactor;  // 0x20
        public UnityEngine.Color _dryColor;  // 0x24
        public UnityEngine.Color _healthyColor;  // 0x34
        public System.Single _maxHeight;  // 0x44
        public System.Single _maxWidth;  // 0x48
        public System.Single _minHeight;  // 0x4c
        public System.Single _minWidth;  // 0x50
        public System.Single _noiseSpread;  // 0x54
        public System.Int32 _renderMode;  // 0x58

        // Methods
        // RVA: 0x09DB8AE8  token: 0x6000D98
        public System.Void .ctor() { }
        // RVA: 0x09DB8A1C  token: 0x6000D99
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_DetailPrototype other) { }

    }

    // TypeToken: 0x20001BF  // size: 0x70
    public class HEU_VolumeCache : UnityEngine.ScriptableObject, HoudiniEngineUnity.IHEU_VolumeCache, HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent, HoudiniEngineUnity.IEquivable`1
    {
        // Fields
        private HoudiniEngineUnity.HEU_GeoNode _ownerNode;  // 0x18
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayer> _layers;  // 0x20
        private System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayer> _updatedLayers;  // 0x28
        private System.Int32 _tileIndex;  // 0x30
        private System.Boolean _isDirty;  // 0x34
        private System.String _geoName;  // 0x38
        private System.String _objName;  // 0x40
        public System.Boolean _uiExpanded;  // 0x48
        private UnityEngine.TerrainData _terrainData;  // 0x50
        private HoudiniEngineUnity.HEU_VolumeScatterTrees _scatterTrees;  // 0x58
        private HoudiniEngineUnity.HEU_DetailProperties _detailProperties;  // 0x60
        private HoudiniEngineUnity.HEU_HoudiniAsset _parentAsset;  // 0x68

        // Properties
        System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayer> Layers { get; /* RVA: 0x04D862C0 */ }
        System.Int32 TileIndex { get; /* RVA: 0x04D864F0 */ }
        System.String ObjectName { get; /* RVA: 0x04D85A60 */ }
        System.String GeoName { get; /* RVA: 0x04D85A50 */ }
        UnityEngine.TerrainData TerrainData { get; /* RVA: 0x04D86270 */ }
        HoudiniEngineUnity.HEU_VolumeScatterTrees ScatterTrees { get; /* RVA: 0x02E56440 */ }
        HoudiniEngineUnity.HEU_DetailProperties DetailProperties { get; /* RVA: 0x04D86280 */ }
        HoudiniEngineUnity.HEU_HoudiniAsset ParentAsset { get; /* RVA: 0x04D86230 */ }
        System.Boolean IsDirty { get; /* RVA: 0x04D86A90 */ set; /* RVA: 0x04D86AA0 */ }
        System.Boolean UIExpanded { get; /* RVA: 0x04D867D0 */ set; /* RVA: 0x04D86810 */ }

        // Methods
        // RVA: 0x09DBE4A8  token: 0x6000DA6
        public virtual HoudiniEngineUnity.HEU_SessionBase GetSession() { }
        // RVA: 0x09DBF594  token: 0x6000DA7
        public virtual System.Void Recook() { }
        // RVA: 0x09DBF5FC  token: 0x6000DA8
        public virtual System.Void ResetParameters() { }
        // RVA: 0x09DBE2FC  token: 0x6000DA9
        public virtual HoudiniEngineUnity.HEU_VolumeLayer GetLayer(System.String layerName) { }
        // RVA: 0x09DBF2D0  token: 0x6000DAA
        public virtual System.Void PopulatePreset(HoudiniEngineUnity.HEU_VolumeCachePreset cachePreset) { }
        // RVA: 0x09DBD948  token: 0x6000DAB
        public virtual System.Boolean ApplyPreset(HoudiniEngineUnity.HEU_VolumeCachePreset volumeCachePreset) { }
        // RVA: 0x09DBF86C  token: 0x6000DAC
        private static System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> UpdateVolumeCachesFromParts(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GeoNode ownerNode, System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> volumeParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> volumeCaches) { }
        // RVA: 0x09DBE53C  token: 0x6000DAD
        private System.Void Initialize(HoudiniEngineUnity.HEU_GeoNode ownerNode, System.Int32 tileIndex) { }
        // RVA: 0x09DBF768  token: 0x6000DAE
        private System.Void StartUpdateLayers() { }
        // RVA: 0x09DBDF98  token: 0x6000DAF
        private System.Void FinishUpdateLayers() { }
        // RVA: 0x09DBE43C  token: 0x6000DB0
        private System.Void GetPartLayerAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_VolumeLayer layer) { }
        // RVA: 0x09DBF08C  token: 0x6000DB1
        private System.Boolean LoadLayerTextureFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Texture2D& outTexture) { }
        // RVA: 0x09DBEB94  token: 0x6000DB2
        private System.Boolean LoadLayerFloatFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Single& floatValue) { }
        // RVA: 0x09DBEA28  token: 0x6000DB3
        private System.Boolean LoadLayerColorFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Color& colorValue) { }
        // RVA: 0x09DBF17C  token: 0x6000DB4
        private System.Boolean LoadLayerVector2FromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Vector2& vectorValue) { }
        // RVA: 0x09DBF7E0  token: 0x6000DB5
        private System.Void UpdateLayerFromPart(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_PartData part) { }
        // RVA: 0x09DBDFC8  token: 0x6000DB6
        private System.Void GenerateTerrainWithAlphamaps(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset, System.Boolean bRebuild) { }
        // RVA: 0x09DBEC8C  token: 0x6000DB7
        public System.Void LoadLayerPropertiesFromAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.TerrainLayer terrainLayer, System.Boolean bNewTerrainLayer, UnityEngine.Texture2D defaultTexture) { }
        // RVA: 0x09DBF558  token: 0x6000DB8
        private System.Void PopulateScatterTrees(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32 pointCount, System.Boolean throwWarningIfNoTileAttribute) { }
        // RVA: 0x09DBF294  token: 0x6000DB9
        private System.Void PopulateDetailPrototype(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_VolumeLayer layer) { }
        // RVA: 0x09DBDD54  token: 0x6000DBA
        private System.Void CopyValuesTo(HoudiniEngineUnity.HEU_VolumeCache destCache) { }
        // RVA: 0x09DBDBD8  token: 0x6000DBB
        private static System.Void CopyDetailProperties(HoudiniEngineUnity.HEU_DetailProperties srcProp, HoudiniEngineUnity.HEU_DetailProperties destProp) { }
        // RVA: 0x09DBDC08  token: 0x6000DBC
        private static System.Void CopyLayer(HoudiniEngineUnity.HEU_VolumeLayer srcLayer, HoudiniEngineUnity.HEU_VolumeLayer destLayer) { }
        // RVA: 0x09DBDCE4  token: 0x6000DBD
        private static System.Void CopyPrototype(HoudiniEngineUnity.HEU_DetailPrototype srcProto, HoudiniEngineUnity.HEU_DetailPrototype destProto) { }
        // RVA: 0x09DBE9C0  token: 0x6000DBE
        private static UnityEngine.Texture2D LoadDefaultSplatTexture() { }
        // RVA: 0x09DBE8F4  token: 0x6000DBF
        private static UnityEngine.Texture2D LoadAssetTexture(System.String path) { }
        // RVA: 0x09DBE62C  token: 0x6000DC0
        public virtual System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_VolumeCache other) { }
        // RVA: 0x09DBFF58  token: 0x6000DC1
        public System.Void .ctor() { }

    }

}

