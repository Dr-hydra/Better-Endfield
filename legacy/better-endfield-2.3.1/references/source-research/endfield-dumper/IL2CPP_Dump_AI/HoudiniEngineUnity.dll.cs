// ========================================================
// Dumped by @desirepro
// Assembly: HoudiniEngineUnity.dll
// Classes:  447
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

# AI-FRIENDLY STRUCTURED DUMP
# Optimized for LLM parsing / code generation

CLASS: <Module>
TYPE:  class
TOKEN: 0x2000001
SIZE:  0x1
FIELDS:
METHODS:
END_CLASS

CLASS: <>f__AnonymousType0`2
TYPE:  sealed class
TOKEN: 0x2000002
FIELDS:
  private   readonly <owner>j__TPar                  <owner>i__Field  // 0x0
  private   readonly <houdiniVersionRequired>j__TPar <houdiniVersionRequired>i__Field  // 0x0
PROPERTIES:
  owner  get=-1  // not resolved
  houdiniVersionRequired  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000003  System.Void .ctor(<owner>j__TPar owner, <houdiniVersionRequired>j__TPar houdiniVersionRequired)
  RVA=-1  // not resolved  token=0x6000004  System.Boolean Equals(System.Object value)
  RVA=-1  // not resolved  token=0x6000005  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x6000006  System.String ToString()
END_CLASS

CLASS: HEU_BoundingVolume
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
PROPERTIES:
  BoundingCollider  get=0x09D38148
METHODS:
  RVA=0x09D37F10  token=0x6000008  System.Collections.Generic.List<UnityEngine.GameObject> GetAllIntersectingObjects()
  RVA=0x05393520  token=0x6000009  System.Void .ctor()
END_CLASS

CLASS: HEU_ExampleEvergreenQuery
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D3D364  token=0x600000A  System.Void StartQuery()
  RVA=0x09D3D05C  token=0x600000B  HoudiniEngineUnity.HEU_HoudiniAsset QueryHoudiniAsset(UnityEngine.GameObject rootGO)
  RVA=0x09D3CD3C  token=0x600000C  System.Void CookAsset(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset)
  RVA=0x09D3C9CC  token=0x600000D  System.Void ChangeParmsAndCook(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset)
  RVA=0x09D3D1D8  token=0x600000E  System.Void QueryObjects(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset)
  RVA=0x09D3CF98  token=0x600000F  System.Void QueryGeoParts(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_GeoInfo& geoInfo)
  RVA=0x09D3D254  token=0x6000010  System.Void QueryPartAttributeByOwner(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeOwner owner, System.Int32 count, System.Text.StringBuilder sb)
  RVA=0x09D3CD50  token=0x6000011  System.Void QueryAttributeByStorageType(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.String attrName)
  RVA=0x09D3CECC  token=0x6000012  System.Void QueryAttribute(HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset, System.String objName, System.String geoName, System.Int32 partID, System.String attrName)
  RVA=0x041E1670  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: HEU_ExampleInstanceCustomAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x09D3D544  token=0x6000014  System.Void InstancerCallback()
  RVA=-1  // generic def  token=0x6000015  System.Void LogArray(System.String name, T[] arr, System.Int32 tupleSize)
  RVA=0x09D3D8C4  token=0x6000016  System.Void LogAttr(HoudiniEngineUnity.HEU_OutputAttribute outAttr)
  RVA=0x05393520  token=0x6000017  System.Void .ctor()
END_CLASS

CLASS: HEU_ScriptCallbackExample
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   _msg  // 0x18
METHODS:
  RVA=0x09D53BE8  token=0x6000018  System.Void AssetCallbackWithMsg(System.String msg)
  RVA=0x09D53B94  token=0x6000019  System.Void AssetCallbackNoMsg()
  RVA=0x05393520  token=0x600001A  System.Void .ctor()
END_CLASS

CLASS: OutputMode
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HEU_ScriptMeshInputUVLayoutExample.OutputModeCOPY  // const
  public    static  HEU_ScriptMeshInputUVLayoutExample.OutputModeREPLACE  // const
METHODS:
END_CLASS

CLASS: HEU_ScriptMeshInputUVLayoutExample
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D53C6C  token=0x600001B  System.Void ApplyUVLayoutTo(UnityEngine.GameObject[] gameObjects, HEU_ScriptMeshInputUVLayoutExample.OutputMode outputMode, System.String output_name_suffix)
  RVA=0x041E1670  token=0x600001C  System.Void .ctor()
END_CLASS

CLASS: HEU_ScriptParameterExample
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.GameObject          _evergreenGameObject  // 0x18
  private           HoudiniEngineUnity.HEU_HoudiniAsset_evergreenAsset  // 0x20
  public            System.Single                   _updateRate  // 0x28
  public            System.Single                   _scale  // 0x2c
METHODS:
  RVA=0x09D53DCC  token=0x600001D  System.Void .ctor()
END_CLASS

CLASS: HEU_AssetEventReceiverTest
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x09D36ED8  token=0x600001E  System.Void ReloadCallback(HoudiniEngineUnity.HEU_ReloadEventData Data)
  RVA=0x09D36D74  token=0x600001F  System.Void CookedCallback(HoudiniEngineUnity.HEU_CookedEventData Data)
  RVA=0x09D36C10  token=0x6000020  System.Void BakedCallback(HoudiniEngineUnity.HEU_BakedEventData Data)
  RVA=0x05393520  token=0x6000021  System.Void .ctor()
END_CLASS

CLASS: CurveEditState
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_Curve.CurveEditStateINVALID  // const
  public    static  HoudiniEngineUnity.HEU_Curve.CurveEditStateGENERATED  // const
  public    static  HoudiniEngineUnity.HEU_Curve.CurveEditStateEDITING  // const
  public    static  HoudiniEngineUnity.HEU_Curve.CurveEditStateREQUIRES_GENERATION  // const
METHODS:
END_CLASS

CLASS: Interaction
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_Curve.InteractionVIEW  // const
  public    static  HoudiniEngineUnity.HEU_Curve.InteractionADD  // const
  public    static  HoudiniEngineUnity.HEU_Curve.InteractionEDIT  // const
METHODS:
END_CLASS

CLASS: CurveDrawCollision
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_Curve.CurveDrawCollisionCOLLIDERS  // const
  public    static  HoudiniEngineUnity.HEU_Curve.CurveDrawCollisionLAYERMASK  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass60_0
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Vector3>points  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000A1  System.Void .ctor()
  RVA=0x09D54290  token=0x60000A2  System.Void <GetAllPoints>b__0(HoudiniEngineUnity.CurveNodeData transform)
END_CLASS

CLASS: <>c__DisplayClass87_0
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x40
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Vector3>positions  // 0x10
  public            System.Boolean                  hasRotations  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Quaternion>rotations  // 0x20
  public            System.Boolean                  hasScales  // 0x28
  public            System.Collections.Generic.List<UnityEngine.Vector3>scales  // 0x30
  public            System.Collections.Generic.List<System.Int32>curveCountIndices  // 0x38
METHODS:
  RVA=0x041E1670  token=0x60000A3  System.Void .ctor()
  RVA=0x09D54300  token=0x60000A4  System.Void <UpdateCurveInputForCurveParts>b__0(HoudiniEngineUnity.CurveNodeData data)
END_CLASS

CLASS: <>c__DisplayClass88_0
TYPE:  sealed class
TOKEN: 0x200002A
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Vector3>positions  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Quaternion>rotations  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Vector3>scales  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60000A5  System.Void .ctor()
  RVA=0x09D54420  token=0x60000A6  System.Void <UpdateCurveInputForCustomAttributes>b__0(HoudiniEngineUnity.CurveNodeData data)
  RVA=0x09D54510  token=0x60000A7  System.Void <UpdateCurveInputForCustomAttributes>b__1(System.Int32 nIndex1, System.Int32 nIndex2, System.Single fCoeff, System.Int32 nInsertIndex)
  RVA=0x09D54800  token=0x60000A8  System.Void <UpdateCurveInputForCustomAttributes>b__2(System.Int32 nIndex, System.Int32 nInsertIndex)
END_CLASS

CLASS: <>c__DisplayClass90_0
TYPE:  sealed class
TOKEN: 0x200002B
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Vector3>rotations  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Vector3>scales  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000A9  System.Void .ctor()
  RVA=0x09D549F8  token=0x60000AA  System.Void <UpdatePoints>b__0(HoudiniEngineUnity.CurveNodeData data)
END_CLASS

CLASS: HEU_AssetType
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetTypeTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetTypeTYPE_HDA  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetTypeTYPE_CURVE  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetTypeTYPE_INPUT  // const
METHODS:
END_CLASS

CLASS: AssetBuildAction
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildActionNONE  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildActionRELOAD  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildActionCOOK  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildActionINVALID  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildActionSTRIP_HEDATA  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildActionDUPLICATE  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildActionRESET_PARAMS  // const
METHODS:
END_CLASS

CLASS: AssetCookStatus
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatusNONE  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatusCOOKING  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatusPOSTCOOK  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatusLOADING  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatusPOSTLOAD  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatusPRELOAD  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatusSELECT_SUBASSET  // const
METHODS:
END_CLASS

CLASS: AssetCookResult
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResultNONE  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResultSUCCESS  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResultERRORED  // const
METHODS:
END_CLASS

CLASS: UpdateUIDelegate
TYPE:  sealed class
TOKEN: 0x2000034
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x6000205  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000206  System.Void Invoke()
  RVA=0x053DED94  token=0x6000207  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000208  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: AssetInstantiationMethod
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethodDEFAULT  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethodDUPLICATED  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethodUNDO  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass262_0
TYPE:  sealed class
TOKEN: 0x2000036
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.TransformData>previousTransformValues  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000209  System.Void .ctor()
  RVA=0x09D691FC  token=0x600020A  System.Void <BakeToExistingPrefab>b__0(UnityEngine.Transform trans)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000037
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.HEU_HoudiniAsset.<>c<>9  // static @ 0x0
  public    static  System.Func<HoudiniEngineUnity.HEU_InputNode,System.Boolean><>9__274_0  // static @ 0x8
  public    static  System.Func<HoudiniEngineUnity.HEU_ObjectNode,System.Boolean><>9__312_0  // static @ 0x10
  public    static  System.Func<HoudiniEngineUnity.HEU_Curve,System.Boolean><>9__335_0  // static @ 0x18
  public    static  System.Func<HoudiniEngineUnity.HEU_AttributesStore,System.Boolean><>9__336_0  // static @ 0x20
  public    static  System.Func<HoudiniEngineUnity.HEU_Curve,System.Boolean><>9__373_0  // static @ 0x28
  public    static  System.Func<HoudiniEngineUnity.HEU_ObjectNode,System.Boolean><>9__404_0  // static @ 0x30
  public    static  System.Func<HoudiniEngineUnity.HEU_Curve,System.Boolean><>9__404_1  // static @ 0x38
  public    static  System.Func<HoudiniEngineUnity.HEU_MaterialData,System.Boolean><>9__404_2  // static @ 0x40
METHODS:
  RVA=0x09D695C0  token=0x600020B  System.Void .cctor()
  RVA=0x041E1670  token=0x600020C  System.Void .ctor()
  RVA=0x09D69118  token=0x600020D  System.Boolean <GetNonParameterInputNodes>b__274_0(HoudiniEngineUnity.HEU_InputNode node)
  RVA=0x09D690CC  token=0x600020E  System.Boolean <DoPostCookWork>b__312_0(HoudiniEngineUnity.HEU_ObjectNode obj)
  RVA=0x09D691B0  token=0x600020F  System.Boolean <UploadCurvesParameters>b__335_0(HoudiniEngineUnity.HEU_Curve curve)
  RVA=0x09D69164  token=0x6000210  System.Boolean <UploadAttributeValues>b__336_0(HoudiniEngineUnity.HEU_AttributesStore store)
  RVA=0x09D68F84  token=0x6000211  System.Boolean <ClearInvalidCurves>b__373_0(HoudiniEngineUnity.HEU_Curve curve)
  RVA=0x09D68FD0  token=0x6000212  System.Boolean <ClearInvalidLists>b__404_0(HoudiniEngineUnity.HEU_ObjectNode node)
  RVA=0x09D6901C  token=0x6000213  System.Boolean <ClearInvalidLists>b__404_1(HoudiniEngineUnity.HEU_Curve curve)
  RVA=0x09D69068  token=0x6000214  System.Boolean <ClearInvalidLists>b__404_2(HoudiniEngineUnity.HEU_MaterialData data)
END_CLASS

CLASS: <>c__DisplayClass405_0
TYPE:  sealed class
TOKEN: 0x2000038
SIZE:  0x28
FIELDS:
  public            HoudiniEngineUnity.HEU_HoudiniAssetnewAsset  // 0x10
  public            System.Int32                    i  // 0x18
  public            System.Predicate<HoudiniEngineUnity.HEU_Curve><>9__0  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000215  System.Void .ctor()
  RVA=0x09D692B4  token=0x6000216  System.Boolean <CopyPropertiesTo>b__0(HoudiniEngineUnity.HEU_Curve curve)
END_CLASS

CLASS: InputNodeType
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_InputNode.InputNodeTypeCONNECTION  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputNodeTypeNODE  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputNodeTypePARAMETER  // const
METHODS:
END_CLASS

CLASS: InputObjectType
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_InputNode.InputObjectTypeHDA  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputObjectTypeUNITY_MESH  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputObjectTypeCURVE  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputObjectTypeTERRAIN  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputObjectTypeBOUNDING_BOX  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputObjectTypeTILEMAP  // const
METHODS:
END_CLASS

CLASS: InternalObjectType
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_InputNode.InternalObjectTypeUNKNOWN  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InternalObjectTypeHDA  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InternalObjectTypeUNITY_MESH  // const
METHODS:
END_CLASS

CLASS: InputActions
TYPE:  sealed struct
TOKEN: 0x200003E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_InputNode.InputActionsACTION  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputActionsDELETE  // const
  public    static  HoudiniEngineUnity.HEU_InputNode.InputActionsINSERT  // const
METHODS:
END_CLASS

CLASS: HEU_InputObjectUICache
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600027C  System.Void .ctor()
END_CLASS

CLASS: HEU_InputAssetUICache
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600027D  System.Void .ctor()
END_CLASS

CLASS: PartOutputType
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_PartData.PartOutputTypeNONE  // const
  public    static  HoudiniEngineUnity.HEU_PartData.PartOutputTypeMESH  // const
  public    static  HoudiniEngineUnity.HEU_PartData.PartOutputTypeVOLUME  // const
  public    static  HoudiniEngineUnity.HEU_PartData.PartOutputTypeCURVE  // const
  public    static  HoudiniEngineUnity.HEU_PartData.PartOutputTypeINSTANCER  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200004A
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.HEU_PartData.<>c<>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.LOD><>9__96_0  // static @ 0x8
METHODS:
  RVA=0x09D6955C  token=0x6000302  System.Void .cctor()
  RVA=0x041E1670  token=0x6000303  System.Void .ctor()
  RVA=0x09D690B4  token=0x6000304  System.Int32 <CopyGameObjectComponents>b__96_0(UnityEngine.LOD a, UnityEngine.LOD b)
END_CLASS

CLASS: <>c__DisplayClass97_0
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.TransformData>previousTransformValues  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000305  System.Void .ctor()
  RVA=0x09D69334  token=0x6000306  System.Void <CopyChildGameObjects>b__0(UnityEngine.Transform trans)
END_CLASS

CLASS: <>c__DisplayClass99_0
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.TransformData>previousTransformValues  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000307  System.Void .ctor()
  RVA=0x09D693EC  token=0x6000308  System.Void <BakePartToGameObject>b__0(UnityEngine.Transform trans)
END_CLASS

CLASS: <>c__DisplayClass99_1
TYPE:  sealed class
TOKEN: 0x200004D
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.TransformData>previousTransformValues  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000309  System.Void .ctor()
  RVA=0x09D694A4  token=0x600030A  System.Void <BakePartToGameObject>b__1(UnityEngine.Transform trans)
END_CLASS

CLASS: AttributeType
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeTypeUNDEFINED  // const
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeTypeBOOL  // const
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeTypeINT  // const
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeTypeFLOAT  // const
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeTypeSTRING  // const
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeTypeMAX  // const
METHODS:
END_CLASS

CLASS: AttributeState
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeStateINVALID  // const
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeStateSYNCED  // const
  public    static  HoudiniEngineUnity.HEU_AttributeData.AttributeStateLOCAL_DIRTY  // const
METHODS:
END_CLASS

CLASS: SetAttributeValueFunc
TYPE:  sealed class
TOKEN: 0x2000052
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09D8518C  token=0x6000342  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014DD080  token=0x6000343  System.Void Invoke(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D850C0  token=0x6000344  System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000345  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: HEU_ImportAssetOptions
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptionsDefault  // const
  public    static  HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptionsForceUpdate  // const
  public    static  HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptionsForceSynchronousImport  // const
  public    static  HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptionsImportRecursive  // const
  public    static  HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptionsDontDownloadFromCacheServer  // const
  public    static  HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptionsForceUncompressedImport  // const
METHODS:
END_CLASS

CLASS: RegSAM
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMQueryValue  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMSetValue  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMCreateSubKey  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMEnumerateSubKeys  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMNotify  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMCreateLink  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMWOW64_32Key  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMWOW64_64Key  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMWOW64_Res  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMRead  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMWrite  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMExecute  // const
  public    static  HoudiniEngineUnity.HEU_PlatformWin.RegSAMAllAccess  // const
METHODS:
END_CLASS

CLASS: DataType
TYPE:  sealed struct
TOKEN: 0x2000060
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_PluginStorage.DataTypeBOOL  // const
  public    static  HoudiniEngineUnity.HEU_PluginStorage.DataTypeINT  // const
  public    static  HoudiniEngineUnity.HEU_PluginStorage.DataTypeLONG  // const
  public    static  HoudiniEngineUnity.HEU_PluginStorage.DataTypeFLOAT  // const
  public    static  HoudiniEngineUnity.HEU_PluginStorage.DataTypeSTRING  // const
METHODS:
END_CLASS

CLASS: StoreData
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x20
FIELDS:
  public            HoudiniEngineUnity.HEU_PluginStorage.DataType_type  // 0x10
  public            System.String                   _valueStr  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60004B6  System.Void .ctor()
END_CLASS

CLASS: StoreDataArray`1
TYPE:  class
TOKEN: 0x2000062
FIELDS:
  public            T[]                             _array  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004B7  System.Void .ctor()
END_CLASS

CLASS: HEU_HandleType
TYPE:  sealed struct
TOKEN: 0x2000064
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_Handle.HEU_HandleTypeXFORM  // const
  public    static  HoudiniEngineUnity.HEU_Handle.HEU_HandleTypeUNSUPPORTED  // const
METHODS:
END_CLASS

CLASS: HEU_HandleParamType
TYPE:  sealed struct
TOKEN: 0x2000066
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamTypeTRANSLATE  // const
  public    static  HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamTypeROTATE  // const
  public    static  HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamTypeSCALE  // const
METHODS:
END_CLASS

CLASS: ModifierAction
TYPE:  sealed struct
TOKEN: 0x20000C7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ParameterModifier.ModifierActionMULTIPARM_INSERT  // const
  public    static  HoudiniEngineUnity.HEU_ParameterModifier.ModifierActionMULTIPARM_REMOVE  // const
  public    static  HoudiniEngineUnity.HEU_ParameterModifier.ModifierActionMULTIPARM_CLEAR  // const
  public    static  HoudiniEngineUnity.HEU_ParameterModifier.ModifierActionSET_FLOAT  // const
  public    static  HoudiniEngineUnity.HEU_ParameterModifier.ModifierActionSET_INT  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass82_0
TYPE:  sealed class
TOKEN: 0x20000C9
SIZE:  0x20
FIELDS:
  public            System.Int32                    parentID  // 0x10
  public            System.Predicate<HoudiniEngineUnity.HAPI_ParmInfo><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600059A  System.Void .ctor()
  RVA=0x0702DACC  token=0x600059B  System.Boolean <Initialize>b__0(HoudiniEngineUnity.HAPI_ParmInfo p)
END_CLASS

CLASS: <>c__DisplayClass83_0
TYPE:  sealed class
TOKEN: 0x20000CA
SIZE:  0x20
FIELDS:
  public            System.Int32                    parentID  // 0x10
  public            System.Predicate<HoudiniEngineUnity.HAPI_ParmInfo><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600059C  System.Void .ctor()
  RVA=0x0702DACC  token=0x600059D  System.Boolean <Initialize4HGTool>b__0(HoudiniEngineUnity.HAPI_ParmInfo p)
END_CLASS

CLASS: LinkState
TYPE:  sealed struct
TOKEN: 0x20000CF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_PDGAssetLink.LinkStateINACTIVE  // const
  public    static  HoudiniEngineUnity.HEU_PDGAssetLink.LinkStateLINKING  // const
  public    static  HoudiniEngineUnity.HEU_PDGAssetLink.LinkStateLINKED  // const
  public    static  HoudiniEngineUnity.HEU_PDGAssetLink.LinkStateERROR_NOT_LINKED  // const
METHODS:
END_CLASS

CLASS: UpdateUIDelegate
TYPE:  sealed class
TOKEN: 0x20000D0
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x6000602  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000603  System.Void Invoke()
  RVA=0x053DED94  token=0x6000604  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000605  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass107_0
TYPE:  sealed class
TOKEN: 0x20000D1
SIZE:  0x28
FIELDS:
  public            HoudiniEngineUnity.HEU_PDGAssetLink<>4__this  // 0x10
  public            System.Action<HoudiniEngineUnity.HEU_TOPNodeData,HoudiniEngineUnity.HEU_SyncedEventData>OnSynced  // 0x18
  public            HoudiniEngineUnity.HEU_TOPNodeDatatopNode  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000606  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass107_1
TYPE:  sealed class
TOKEN: 0x20000D2
SIZE:  0x20
FIELDS:
  public            HoudiniEngineUnity.HEU_GeoSync  geoSync  // 0x10
  public            HoudiniEngineUnity.HEU_PDGAssetLink.<>c__DisplayClass107_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000607  System.Void .ctor()
  RVA=0x09D9455C  token=0x6000608  System.Void <LoadResults>b__0(HoudiniEngineUnity.HEU_SyncedEventData Data)
END_CLASS

CLASS: PDGState
TYPE:  sealed struct
TOKEN: 0x20000D6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_TOPNodeData.PDGStateNONE  // const
  public    static  HoudiniEngineUnity.HEU_TOPNodeData.PDGStateDIRTIED  // const
  public    static  HoudiniEngineUnity.HEU_TOPNodeData.PDGStateDIRTYING  // const
  public    static  HoudiniEngineUnity.HEU_TOPNodeData.PDGStateCOOKING  // const
  public    static  HoudiniEngineUnity.HEU_TOPNodeData.PDGStateCOOK_COMPLETE  // const
  public    static  HoudiniEngineUnity.HEU_TOPNodeData.PDGStateCOOK_FAILED  // const
METHODS:
END_CLASS

CLASS: OnWorkItemLoadResultsDelegate
TYPE:  sealed class
TOKEN: 0x20000DA
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x068FE0AC  token=0x600063E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600063F  System.Void Invoke(HoudiniEngineUnity.HEU_SyncedEventData OnSynced)
  RVA=0x047FDD70  token=0x6000640  System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_SyncedEventData OnSynced, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000641  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: EventMessageColor
TYPE:  sealed struct
TOKEN: 0x20000DB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_PDGSession.EventMessageColorDEFAULT  // const
  public    static  HoudiniEngineUnity.HEU_PDGSession.EventMessageColorWARNING  // const
  public    static  HoudiniEngineUnity.HEU_PDGSession.EventMessageColorERROR  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass35_0
TYPE:  sealed class
TOKEN: 0x20000DC
SIZE:  0x28
FIELDS:
  public            System.Int32[]                  allNetworkNodeIds  // 0x10
  public            System.Int32                    idx  // 0x18
  public            System.Predicate<System.Int32>  <>9__0  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000642  System.Void .ctor()
  RVA=0x09D94634  token=0x6000643  System.Boolean <GetNonBypassedNetworkIds>b__0(System.Int32 id)
END_CLASS

CLASS: Source
TYPE:  sealed struct
TOKEN: 0x20000F6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_MaterialData.SourceDEFAULT  // const
  public    static  HoudiniEngineUnity.HEU_MaterialData.SourceHOUDINI  // const
  public    static  HoudiniEngineUnity.HEU_MaterialData.SourceUNITY  // const
  public    static  HoudiniEngineUnity.HEU_MaterialData.SourceSUBSTANCE  // const
METHODS:
END_CLASS

CLASS: CreateSessionFromTypeDelegate
TYPE:  sealed class
TOKEN: 0x20000FF
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0835376C  token=0x6000931  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014E2060  token=0x6000932  HoudiniEngineUnity.HEU_SessionBase Invoke(System.Type type)
  RVA=0x047FDD70  token=0x6000933  System.IAsyncResult BeginInvoke(System.Type type, System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x6000934  HoudiniEngineUnity.HEU_SessionBase EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Status
TYPE:  sealed struct
TOKEN: 0x2000102
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_SessionSyncData.StatusStopped  // const
  public    static  HoudiniEngineUnity.HEU_SessionSyncData.StatusStarted  // const
  public    static  HoudiniEngineUnity.HEU_SessionSyncData.StatusConnecting  // const
  public    static  HoudiniEngineUnity.HEU_SessionSyncData.StatusInitializing  // const
  public    static  HoudiniEngineUnity.HEU_SessionSyncData.StatusConnected  // const
METHODS:
END_CLASS

CLASS: AssetSyncCallback
TYPE:  sealed class
TOKEN: 0x2000104
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x068FE0AC  token=0x6000941  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000942  System.Void Invoke(HoudiniEngineUnity.HEU_AssetSync assetSync)
  RVA=0x047FDD70  token=0x6000943  System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_AssetSync assetSync, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000944  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: TaskStatus
TYPE:  sealed struct
TOKEN: 0x200010B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_Task.TaskStatusNONE  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskStatusPENDING_START  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskStatusSTARTED  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskStatusREQUIRE_UPDATE  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskStatusPENDING_COMPLETE  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskStatusCOMPLETED  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskStatusUNUSED  // const
METHODS:
END_CLASS

CLASS: TaskResult
TYPE:  sealed struct
TOKEN: 0x200010C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_Task.TaskResultNONE  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskResultSUCCESS  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskResultFAILED  // const
  public    static  HoudiniEngineUnity.HEU_Task.TaskResultKILLED  // const
METHODS:
END_CLASS

CLASS: TaskCallback
TYPE:  sealed class
TOKEN: 0x200010D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x068FE0AC  token=0x600097E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600097F  System.Void Invoke(HoudiniEngineUnity.HEU_Task task)
  RVA=0x047FDD70  token=0x6000980  System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_Task task, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000981  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: BuildType
TYPE:  sealed struct
TOKEN: 0x200010F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AssetTask.BuildTypeNONE  // const
  public    static  HoudiniEngineUnity.HEU_AssetTask.BuildTypeLOAD  // const
  public    static  HoudiniEngineUnity.HEU_AssetTask.BuildTypeCOOK  // const
  public    static  HoudiniEngineUnity.HEU_AssetTask.BuildTypeRELOAD  // const
METHODS:
END_CLASS

CLASS: LoadType
TYPE:  sealed struct
TOKEN: 0x2000118
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadTypeFILE  // const
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadTypeNODE  // const
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadTypeASSET  // const
METHODS:
END_CLASS

CLASS: LoadStatus
TYPE:  sealed struct
TOKEN: 0x200011A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatusNONE  // const
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatusSTARTED  // const
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatusSUCCESS  // const
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatusERROR  // const
METHODS:
END_CLASS

CLASS: HEU_LoadData
TYPE:  class
TOKEN: 0x2000119
SIZE:  0x38
FIELDS:
  public            System.Int32                    _cookNodeID  // 0x10
  public            HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus_loadStatus  // 0x14
  public            System.Text.StringBuilder       _logStr  // 0x18
  public            HoudiniEngineUnity.HEU_SessionBase_session  // 0x20
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadObject>_loadedObjects  // 0x28
  public            System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_LoadBufferBase>_idBuffersMap  // 0x30
METHODS:
  RVA=0x041E1670  token=0x60009D5  System.Void .ctor()
END_CLASS

CLASS: HEU_LoadObject
TYPE:  class
TOKEN: 0x200011B
SIZE:  0x30
FIELDS:
  public            System.Int32                    _objectNodeID  // 0x10
  public            System.Int32                    _displayNodeID  // 0x14
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume>_terrainBuffers  // 0x18
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferMesh>_meshBuffers  // 0x20
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferInstancer>_instancerBuffers  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60009D6  System.Void .ctor()
END_CLASS

CLASS: HEU_LoadCallbackType
TYPE:  sealed struct
TOKEN: 0x200011C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackTypePRECOOK  // const
  public    static  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackTypePOSTCOOK  // const
METHODS:
END_CLASS

CLASS: HEU_LoadCallback
TYPE:  sealed class
TOKEN: 0x200011D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09738878  token=0x60009D7  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x01546280  token=0x60009D8  System.Void Invoke(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackType callbackType)
  RVA=0x09DA34AC  token=0x60009D9  System.IAsyncResult BeginInvoke(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallbackType callbackType, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60009DA  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: HEU_ReplacePrefabOptions
TYPE:  sealed struct
TOKEN: 0x2000121
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptionsDefault  // const
  public    static  HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptionsConnectToPrefab  // const
  public    static  HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptionsReplaceNameBased  // const
METHODS:
END_CLASS

CLASS: GetArray1ArgDel`1
TYPE:  sealed class
TOKEN: 0x2000127
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000A9E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000A9F  System.Boolean Invoke(System.Int32 arg1, T[] data, System.Int32 start, System.Int32 length)
  RVA=-1  // runtime  token=0x6000AA0  System.IAsyncResult BeginInvoke(System.Int32 arg1, T[] data, System.Int32 start, System.Int32 length, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000AA1  System.Boolean EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: GetArray2ArgDel`2
TYPE:  sealed class
TOKEN: 0x2000128
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000AA2  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000AA3  System.Boolean Invoke(System.Int32 arg1, ARG2 arg2, T[] data, System.Int32 start, System.Int32 length)
  RVA=-1  // runtime  token=0x6000AA4  System.IAsyncResult BeginInvoke(System.Int32 arg1, ARG2 arg2, T[] data, System.Int32 start, System.Int32 length, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000AA5  System.Boolean EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: GetArray3ArgDel`3
TYPE:  sealed class
TOKEN: 0x2000129
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000AA6  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000AA7  System.Boolean Invoke(System.Int32 arg1, ARG2 arg2, ARG3 arg3, T[] data, System.Int32 start, System.Int32 length)
  RVA=-1  // runtime  token=0x6000AA8  System.IAsyncResult BeginInvoke(System.Int32 arg1, ARG2 arg2, ARG3 arg3, T[] data, System.Int32 start, System.Int32 length, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000AA9  System.Boolean EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: GetAttributeArrayInputFunc`1
TYPE:  sealed class
TOKEN: 0x200012A
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000AAA  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000AAB  System.Boolean Invoke(System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[] items, System.Int32 start, System.Int32 end)
  RVA=-1  // runtime  token=0x6000AAC  System.IAsyncResult BeginInvoke(System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[] items, System.Int32 start, System.Int32 end, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000AAD  System.Boolean EndInvoke(HoudiniEngineUnity.HAPI_AttributeInfo& info, System.IAsyncResult result)
END_CLASS

CLASS: SetAttributeArrayFunc`1
TYPE:  sealed class
TOKEN: 0x200012B
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000AAE  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000AAF  System.Boolean Invoke(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, System.Int32 start, System.Int32 end)
  RVA=-1  // runtime  token=0x6000AB0  System.IAsyncResult BeginInvoke(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, System.Int32 start, System.Int32 end, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000AB1  System.Boolean EndInvoke(HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.IAsyncResult result)
END_CLASS

CLASS: ColliderType
TYPE:  sealed struct
TOKEN: 0x200012F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderTypeNONE  // const
  public    static  HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderTypeBOX  // const
  public    static  HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderTypeSPHERE  // const
  public    static  HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderTypeMESH  // const
  public    static  HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderTypeSIMPLE_BOX  // const
  public    static  HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderTypeSIMPLE_SPHERE  // const
  public    static  HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderTypeSIMPLE_CAPSULE  // const
METHODS:
END_CLASS

CLASS: HEU_ColliderInfo
TYPE:  class
TOKEN: 0x200012E
SIZE:  0x58
FIELDS:
  public            HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo.ColliderType_colliderType  // 0x10
  public            UnityEngine.Vector3             _colliderCenter  // 0x14
  public            UnityEngine.Vector3             _colliderSize  // 0x20
  public            System.Single                   _colliderRadius  // 0x2c
  public            System.Boolean                  _convexCollider  // 0x30
  public            System.String                   _collisionGroupName  // 0x38
  public            UnityEngine.Vector3[]           _collisionVertices  // 0x40
  public            System.Int32[]                  _collisionIndices  // 0x48
  public            UnityEngine.MeshTopology        _meshTopology  // 0x50
  public            System.Boolean                  _isTrigger  // 0x54
METHODS:
  RVA=0x041E1670  token=0x6000ACE  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000130
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.HEU_GenerateGeoCache.<>c<>9  // static @ 0x0
  public    static  System.Predicate<UnityEngine.Material><>9__64_0  // static @ 0x8
  public    static  System.Comparison<System.Single><>9__66_0  // static @ 0x10
METHODS:
  RVA=0x09DB8710  token=0x6000ACF  System.Void .cctor()
  RVA=0x041E1670  token=0x6000AD0  System.Void .ctor()
  RVA=0x09DB86C4  token=0x6000AD1  System.Boolean <GetFinalMaterialsFromComparingNewWithPrevious>b__64_0(UnityEngine.Material material)
  RVA=0x09DB86A8  token=0x6000AD2  System.Int32 <GenerateLODMeshesFromGeoGroups>b__66_0(System.Single a, System.Single b)
END_CLASS

CLASS: HEU_InputDataMeshes
TYPE:  class
TOKEN: 0x2000138
SIZE:  0x28
EXTENDS: HoudiniEngineUnity.HEU_InputData
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMesh>_inputMeshes  // 0x18
  public            System.Boolean                  _hasLOD  // 0x20
METHODS:
  RVA=0x09DA9E10  token=0x6000AF5  System.Void .ctor()
END_CLASS

CLASS: HEU_InputColliderType
TYPE:  sealed struct
TOKEN: 0x2000139
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderTypeNONE  // const
  public    static  HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderTypeBOX  // const
  public    static  HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderTypeSPHERE  // const
  public    static  HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderTypeCAPSULE  // const
  public    static  HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderTypeMESH  // const
METHODS:
END_CLASS

CLASS: HEU_InputDataCollider
TYPE:  class
TOKEN: 0x200013A
SIZE:  0x20
FIELDS:
  public            UnityEngine.Collider            _collider  // 0x10
  public            HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputColliderType_colliderType  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000AF6  System.Void .ctor()
END_CLASS

CLASS: HEU_InputDataMesh
TYPE:  class
TOKEN: 0x200013B
SIZE:  0x60
FIELDS:
  public            UnityEngine.Mesh                _mesh  // 0x10
  public            UnityEngine.Material[]          _materials  // 0x18
  public            System.String                   _meshPath  // 0x20
  public            System.String                   _meshName  // 0x28
  public            System.Int32                    _numVertices  // 0x30
  public            System.Int32                    _numSubMeshes  // 0x34
  public            System.UInt32[]                 _indexStart  // 0x38
  public            System.UInt32[]                 _indexCount  // 0x40
  public            System.Single                   _LODScreenTransition  // 0x48
  public            UnityEngine.Transform           _transform  // 0x50
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataCollider>_colliders  // 0x58
METHODS:
  RVA=0x041E1670  token=0x6000AF7  System.Void .ctor()
END_CLASS

CLASS: HEU_InputDataSpline
TYPE:  class
TOKEN: 0x200013E
SIZE:  0x30
FIELDS:
  public            UnityEngine.Splines.Spline      _spline  // 0x10
  public            System.Boolean                  _closed  // 0x18
  public            System.Int32                    _count  // 0x1c
  public            System.Single                   _length  // 0x20
  public            UnityEngine.Splines.BezierKnot[]_knots  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000B01  System.Void .ctor()
END_CLASS

CLASS: HEU_InputDataSplineContainer
TYPE:  class
TOKEN: 0x200013F
SIZE:  0x28
EXTENDS: HoudiniEngineUnity.HEU_InputData
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterfaceSpline.HEU_InputDataSpline>_inputSplines  // 0x18
  public            UnityEngine.Transform           _transform  // 0x20
METHODS:
  RVA=0x09DA9E88  token=0x6000B02  System.Void .ctor()
END_CLASS

CLASS: HEU_InputDataTerrain
TYPE:  class
TOKEN: 0x2000141
SIZE:  0x80
EXTENDS: HoudiniEngineUnity.HEU_InputData
FIELDS:
  public            System.String                   _heightFieldName  // 0x18
  public            System.Int32                    _parentNodeID  // 0x20
  public            System.Single                   _voxelSize  // 0x24
  public            UnityEngine.Terrain             _terrain  // 0x28
  public            UnityEngine.TerrainData         _terrainData  // 0x30
  public            System.Int32                    _numPointsX  // 0x38
  public            System.Int32                    _numPointsY  // 0x3c
  public            HoudiniEngineUnity.HAPI_Transform_transform  // 0x40
  public            System.Single                   _heightScale  // 0x68
  public            System.Int32                    _heightfieldNodeID  // 0x6c
  public            System.Int32                    _heightNodeID  // 0x70
  public            System.Int32                    _maskNodeID  // 0x74
  public            System.Int32                    _mergeNodeID  // 0x78
METHODS:
  RVA=0x09DA9F00  token=0x6000B10  System.Void .ctor()
END_CLASS

CLASS: HEU_InputDataTilemap
TYPE:  class
TOKEN: 0x2000144
SIZE:  0x28
EXTENDS: HoudiniEngineUnity.HEU_InputData
FIELDS:
  public            UnityEngine.Tilemaps.Tilemap    _tilemap  // 0x18
  public            UnityEngine.Transform           _transform  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000B18  System.Void .ctor()
END_CLASS

CLASS: ToolType
TYPE:  sealed struct
TOKEN: 0x200014E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ShelfToolData.ToolTypeGENERATOR  // const
  public    static  HoudiniEngineUnity.HEU_ShelfToolData.ToolTypeOPERATOR_SINGLE  // const
  public    static  HoudiniEngineUnity.HEU_ShelfToolData.ToolTypeOPERATOR_MULTI  // const
  public    static  HoudiniEngineUnity.HEU_ShelfToolData.ToolTypeBATCH  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass19_0
TYPE:  sealed class
TOKEN: 0x2000151
SIZE:  0x18
FIELDS:
  public            System.String                   shelfName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000B79  System.Void .ctor()
  RVA=0x06CC7538  token=0x6000B7A  System.Boolean <AddShelf>b__0(HoudiniEngineUnity.HEU_Shelf shelf)
END_CLASS

CLASS: Type
TYPE:  sealed struct
TOKEN: 0x2000156
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.JSONNode.Enumerator.TypeNone  // const
  public    static  HoudiniEngineUnity.JSONNode.Enumerator.TypeArray  // const
  public    static  HoudiniEngineUnity.JSONNode.Enumerator.TypeObject  // const
METHODS:
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000155
SIZE:  0x58
FIELDS:
  private           HoudiniEngineUnity.JSONNode.Enumerator.Typetype  // 0x10
  private           System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode>m_Object  // 0x18
  private           System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode>m_Array  // 0x40
PROPERTIES:
  IsValid  get=0x068B8E74
  Current  get=0x09DB8818
METHODS:
  RVA=0x09DB87E4  token=0x6000BDF  System.Void .ctor(System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> aArrayEnum)
  RVA=0x04B0C3F0  token=0x6000BE0  System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> aDictEnum)
  RVA=0x09DB8774  token=0x6000BE2  System.Boolean MoveNext()
END_CLASS

CLASS: ValueEnumerator
TYPE:  sealed struct
TOKEN: 0x2000157
SIZE:  0x58
FIELDS:
  private           HoudiniEngineUnity.JSONNode.Enumeratorm_Enumerator  // 0x10
PROPERTIES:
  Current  get=0x09DCBB70
METHODS:
  RVA=0x09DC2BA0  token=0x6000BE3  System.Void .ctor(System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> aArrayEnum)
  RVA=0x09DC2ACC  token=0x6000BE4  System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> aDictEnum)
  RVA=0x09DC2B6C  token=0x6000BE5  System.Void .ctor(HoudiniEngineUnity.JSONNode.Enumerator aEnumerator)
  RVA=0x09DC2AC4  token=0x6000BE7  System.Boolean MoveNext()
  RVA=0x04DA2D60  token=0x6000BE8  HoudiniEngineUnity.JSONNode.ValueEnumerator GetEnumerator()
END_CLASS

CLASS: KeyEnumerator
TYPE:  sealed struct
TOKEN: 0x2000158
SIZE:  0x58
FIELDS:
  private           HoudiniEngineUnity.JSONNode.Enumeratorm_Enumerator  // 0x10
PROPERTIES:
  Current  get=0x09DC2C38
METHODS:
  RVA=0x09DC2BA0  token=0x6000BE9  System.Void .ctor(System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode> aArrayEnum)
  RVA=0x09DC2ACC  token=0x6000BEA  System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode> aDictEnum)
  RVA=0x09DC2B6C  token=0x6000BEB  System.Void .ctor(HoudiniEngineUnity.JSONNode.Enumerator aEnumerator)
  RVA=0x09DC2AC4  token=0x6000BED  System.Boolean MoveNext()
  RVA=0x04DA2D60  token=0x6000BEE  HoudiniEngineUnity.JSONNode.KeyEnumerator GetEnumerator()
END_CLASS

CLASS: LinqEnumerator
TYPE:  class
TOKEN: 0x2000159
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           HoudiniEngineUnity.JSONNode     m_Node  // 0x10
  private           HoudiniEngineUnity.JSONNode.Enumeratorm_Enumerator  // 0x18
PROPERTIES:
  Current  get=0x09DC2EC8
  System.Collections.IEnumerator.Current  get=0x09DC2DF4
METHODS:
  RVA=0x09DC2E4C  token=0x6000BEF  System.Void .ctor(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DC2D18  token=0x6000BF2  System.Boolean MoveNext()
  RVA=0x09DC2C78  token=0x6000BF3  System.Void Dispose()
  RVA=0x09DC2CB4  token=0x6000BF4  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode>> GetEnumerator()
  RVA=0x09DC2D24  token=0x6000BF5  System.Void Reset()
  RVA=0x09DC2D90  token=0x6000BF6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <get_Children>d__40
TYPE:  sealed class
TOKEN: 0x200015A
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           HoudiniEngineUnity.JSONNode     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000BF7  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000BF8  System.Void System.IDisposable.Dispose()
  RVA=0x04D83CA0  token=0x6000BF9  System.Boolean MoveNext()
  RVA=0x09DCB4CC  token=0x6000BFB  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x09DCB44C  token=0x6000BFD  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator()
  RVA=0x09DCB4C4  token=0x6000BFE  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <get_DeepChildren>d__42
TYPE:  sealed class
TOKEN: 0x200015B
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           HoudiniEngineUnity.JSONNode     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            HoudiniEngineUnity.JSONNode     <>4__this  // 0x28
  private           System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode><>7__wrap1  // 0x30
  private           System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode><>7__wrap2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000BFF  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x09DCB9F0  token=0x6000C00  System.Void System.IDisposable.Dispose()
  RVA=0x09DCB518  token=0x6000C01  System.Boolean MoveNext()
  RVA=0x09DCBADC  token=0x6000C02  System.Void <>m__Finally1()
  RVA=0x09DCBB24  token=0x6000C03  System.Void <>m__Finally2()
  RVA=0x09DCB9A4  token=0x6000C05  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x09DCB910  token=0x6000C07  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator()
  RVA=0x09DCB99C  token=0x6000C08  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <get_Children>d__22
TYPE:  sealed class
TOKEN: 0x200015D
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           HoudiniEngineUnity.JSONNode     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            HoudiniEngineUnity.JSONArray    <>4__this  // 0x28
  private           System.Collections.Generic.List.Enumerator<HoudiniEngineUnity.JSONNode><>7__wrap1  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000C19  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x09DCAF90  token=0x6000C1A  System.Void System.IDisposable.Dispose()
  RVA=0x09DCAC68  token=0x6000C1B  System.Boolean MoveNext()
  RVA=0x09DCB008  token=0x6000C1C  System.Void <>m__Finally1()
  RVA=0x09DCAF44  token=0x6000C1E  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x09DCAEB0  token=0x6000C20  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator()
  RVA=0x09DCAF3C  token=0x6000C21  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x200015F
SIZE:  0x18
FIELDS:
  public            HoudiniEngineUnity.JSONNode     aNode  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000C33  System.Void .ctor()
  RVA=0x09DCA518  token=0x6000C34  System.Boolean <Remove>b__0(System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode> k)
END_CLASS

CLASS: <get_Children>d__23
TYPE:  sealed class
TOKEN: 0x2000160
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           HoudiniEngineUnity.JSONNode     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            HoudiniEngineUnity.JSONObject   <>4__this  // 0x28
  private           System.Collections.Generic.Dictionary.Enumerator<System.String,HoudiniEngineUnity.JSONNode><>7__wrap1  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000C35  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x09DCB398  token=0x6000C36  System.Void System.IDisposable.Dispose()
  RVA=0x09DCB044  token=0x6000C37  System.Boolean MoveNext()
  RVA=0x09DCB410  token=0x6000C38  System.Void <>m__Finally1()
  RVA=0x09DCB34C  token=0x6000C3A  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x09DCB2B8  token=0x6000C3C  System.Collections.Generic.IEnumerator<HoudiniEngineUnity.JSONNode> System.Collections.Generic.IEnumerable<HoudiniEngineUnity.JSONNode>.GetEnumerator()
  RVA=0x09DCB344  token=0x6000C3D  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: RequireStruct`1
TYPE:  class
TOKEN: 0x200016C
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000CBE  System.Void .ctor()
END_CLASS

CLASS: RequireClass`1
TYPE:  class
TOKEN: 0x200016D
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000CBF  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000180
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_LODGroup_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.LODGroup,HoudiniEngineUnity.Test_LODGroup><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.LODGroup,HoudiniEngineUnity.Test_LODGroup><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCAB3C  token=0x6000CDE  System.Void .cctor()
  RVA=0x041E1670  token=0x6000CDF  System.Void .ctor()
  RVA=0x09DC991C  token=0x6000CE0  HoudiniEngineUnity.Test_LODGroup <ToTestObject>b__1_0(UnityEngine.LODGroup lod)
  RVA=0x09DC9EBC  token=0x6000CE1  HoudiniEngineUnity.Test_LODGroup <ToTestObject>b__2_0(UnityEngine.LODGroup lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000183
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_Transform_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.Transform,HoudiniEngineUnity.Test_Transform><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.Transform,HoudiniEngineUnity.Test_Transform><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA628  token=0x6000CE8  System.Void .cctor()
  RVA=0x041E1670  token=0x6000CE9  System.Void .ctor()
  RVA=0x09DC99DC  token=0x6000CEA  HoudiniEngineUnity.Test_Transform <ToTestObject>b__1_0(UnityEngine.Transform lod)
  RVA=0x09DCA398  token=0x6000CEB  HoudiniEngineUnity.Test_Transform <ToTestObject>b__2_0(UnityEngine.Transform lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000186
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_Material_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.Material,HoudiniEngineUnity.Test_Material><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.Material,HoudiniEngineUnity.Test_Material><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA68C  token=0x6000CF2  System.Void .cctor()
  RVA=0x041E1670  token=0x6000CF3  System.Void .ctor()
  RVA=0x09DC9AFC  token=0x6000CF4  HoudiniEngineUnity.Test_Material <ToTestObject>b__1_0(UnityEngine.Material lod)
  RVA=0x09DC9F7C  token=0x6000CF5  HoudiniEngineUnity.Test_Material <ToTestObject>b__2_0(UnityEngine.Material lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000189
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_Collider_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.Collider,HoudiniEngineUnity.Test_Collider><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.Collider,HoudiniEngineUnity.Test_Collider><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA754  token=0x6000CFC  System.Void .cctor()
  RVA=0x041E1670  token=0x6000CFD  System.Void .ctor()
  RVA=0x09DC9860  token=0x6000CFE  HoudiniEngineUnity.Test_Collider <ToTestObject>b__1_0(UnityEngine.Collider lod)
  RVA=0x09DCA09C  token=0x6000CFF  HoudiniEngineUnity.Test_Collider <ToTestObject>b__2_0(UnityEngine.Collider lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200018C
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_BoxCollider_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.BoxCollider,HoudiniEngineUnity.Test_BoxCollider><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.BoxCollider,HoudiniEngineUnity.Test_BoxCollider><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA560  token=0x6000D06  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D07  System.Void .ctor()
  RVA=0x09DC997C  token=0x6000D08  HoudiniEngineUnity.Test_BoxCollider <ToTestObject>b__1_0(UnityEngine.BoxCollider lod)
  RVA=0x09DCA4B8  token=0x6000D09  HoudiniEngineUnity.Test_BoxCollider <ToTestObject>b__2_0(UnityEngine.BoxCollider lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200018F
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_SphereCollider_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.SphereCollider,HoudiniEngineUnity.Test_SphereCollider><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.SphereCollider,HoudiniEngineUnity.Test_SphereCollider><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA948  token=0x6000D10  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D11  System.Void .ctor()
  RVA=0x09DC9D3C  token=0x6000D12  HoudiniEngineUnity.Test_SphereCollider <ToTestObject>b__1_0(UnityEngine.SphereCollider lod)
  RVA=0x09DCA1BC  token=0x6000D13  HoudiniEngineUnity.Test_SphereCollider <ToTestObject>b__2_0(UnityEngine.SphereCollider lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000192
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_CapsuleCollider_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.CapsuleCollider,HoudiniEngineUnity.Test_CapsuleCollider><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.CapsuleCollider,HoudiniEngineUnity.Test_CapsuleCollider><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA6F0  token=0x6000D1A  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D1B  System.Void .ctor()
  RVA=0x09DC9A9C  token=0x6000D1C  HoudiniEngineUnity.Test_CapsuleCollider <ToTestObject>b__1_0(UnityEngine.CapsuleCollider lod)
  RVA=0x09DCA15C  token=0x6000D1D  HoudiniEngineUnity.Test_CapsuleCollider <ToTestObject>b__2_0(UnityEngine.CapsuleCollider lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000195
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_MeshCollider_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.MeshCollider,HoudiniEngineUnity.Test_MeshCollider><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.MeshCollider,HoudiniEngineUnity.Test_MeshCollider><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCAA10  token=0x6000D24  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D25  System.Void .ctor()
  RVA=0x09DC9800  token=0x6000D26  HoudiniEngineUnity.Test_MeshCollider <ToTestObject>b__1_0(UnityEngine.MeshCollider lod)
  RVA=0x09DC9FDC  token=0x6000D27  HoudiniEngineUnity.Test_MeshCollider <ToTestObject>b__2_0(UnityEngine.MeshCollider lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000198
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_Mesh_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.Mesh,HoudiniEngineUnity.Test_Mesh><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.Mesh,HoudiniEngineUnity.Test_Mesh><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA8E4  token=0x6000D2E  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D2F  System.Void .ctor()
  RVA=0x09DC9B5C  token=0x6000D30  HoudiniEngineUnity.Test_Mesh <ToTestObject>b__1_0(UnityEngine.Mesh lod)
  RVA=0x09DC9E5C  token=0x6000D31  HoudiniEngineUnity.Test_Mesh <ToTestObject>b__2_0(UnityEngine.Mesh lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200019B
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_MeshRenderer_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.MeshRenderer,HoudiniEngineUnity.Test_MeshRenderer><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.MeshRenderer,HoudiniEngineUnity.Test_MeshRenderer><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA7B8  token=0x6000D38  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D39  System.Void .ctor()
  RVA=0x09DC9DFC  token=0x6000D3A  HoudiniEngineUnity.Test_MeshRenderer <ToTestObject>b__1_0(UnityEngine.MeshRenderer lod)
  RVA=0x09DCA458  token=0x6000D3B  HoudiniEngineUnity.Test_MeshRenderer <ToTestObject>b__2_0(UnityEngine.MeshRenderer lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200019E
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_MeshFilter_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.MeshFilter,HoudiniEngineUnity.Test_MeshFilter><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.MeshFilter,HoudiniEngineUnity.Test_MeshFilter><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCAC04  token=0x6000D42  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D43  System.Void .ctor()
  RVA=0x09DC9CDC  token=0x6000D44  HoudiniEngineUnity.Test_MeshFilter <ToTestObject>b__1_0(UnityEngine.MeshFilter lod)
  RVA=0x09DC9F1C  token=0x6000D45  HoudiniEngineUnity.Test_MeshFilter <ToTestObject>b__2_0(UnityEngine.MeshFilter lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001A3
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_Gradient_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.Gradient,HoudiniEngineUnity.Test_Gradient><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.Gradient,HoudiniEngineUnity.Test_Gradient><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA9AC  token=0x6000D4F  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D50  System.Void .ctor()
  RVA=0x09DC9D9C  token=0x6000D51  HoudiniEngineUnity.Test_Gradient <ToTestObject>b__1_0(UnityEngine.Gradient lod)
  RVA=0x09DCA03C  token=0x6000D52  HoudiniEngineUnity.Test_Gradient <ToTestObject>b__2_0(UnityEngine.Gradient lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001A6
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_AnimationCurve_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.AnimationCurve,HoudiniEngineUnity.Test_AnimationCurve><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.AnimationCurve,HoudiniEngineUnity.Test_AnimationCurve><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA880  token=0x6000D59  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D5A  System.Void .ctor()
  RVA=0x09DC9BBC  token=0x6000D5B  HoudiniEngineUnity.Test_AnimationCurve <ToTestObject>b__1_0(UnityEngine.AnimationCurve lod)
  RVA=0x09DCA338  token=0x6000D5C  HoudiniEngineUnity.Test_AnimationCurve <ToTestObject>b__2_0(UnityEngine.AnimationCurve lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001A9
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_TerrainLayer_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.TerrainLayer,HoudiniEngineUnity.Test_TerrainLayer><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.TerrainLayer,HoudiniEngineUnity.Test_TerrainLayer><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCAA74  token=0x6000D63  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D64  System.Void .ctor()
  RVA=0x09DC9A3C  token=0x6000D65  HoudiniEngineUnity.Test_TerrainLayer <ToTestObject>b__1_0(UnityEngine.TerrainLayer lod)
  RVA=0x09DCA2D8  token=0x6000D66  HoudiniEngineUnity.Test_TerrainLayer <ToTestObject>b__2_0(UnityEngine.TerrainLayer lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001AC
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_Texture2D_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.Texture2D,HoudiniEngineUnity.Test_Texture2D><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.Texture2D,HoudiniEngineUnity.Test_Texture2D><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCABA0  token=0x6000D6D  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D6E  System.Void .ctor()
  RVA=0x09DC97A0  token=0x6000D6F  HoudiniEngineUnity.Test_Texture2D <ToTestObject>b__1_0(UnityEngine.Texture2D lod)
  RVA=0x09DCA3F8  token=0x6000D70  HoudiniEngineUnity.Test_Texture2D <ToTestObject>b__2_0(UnityEngine.Texture2D lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001AF
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_TreeInstance_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.TreeInstance,HoudiniEngineUnity.Test_TreeInstance><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.TreeInstance,HoudiniEngineUnity.Test_TreeInstance><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA81C  token=0x6000D76  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D77  System.Void .ctor()
  RVA=0x09DC98C0  token=0x6000D78  HoudiniEngineUnity.Test_TreeInstance <ToTestObject>b__1_0(UnityEngine.TreeInstance lod)
  RVA=0x09DCA21C  token=0x6000D79  HoudiniEngineUnity.Test_TreeInstance <ToTestObject>b__2_0(UnityEngine.TreeInstance lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001B2
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_TerrainData_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.TerrainData,HoudiniEngineUnity.Test_TerrainData><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.TerrainData,HoudiniEngineUnity.Test_TerrainData><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCAAD8  token=0x6000D80  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D81  System.Void .ctor()
  RVA=0x09DC9C7C  token=0x6000D82  HoudiniEngineUnity.Test_TerrainData <ToTestObject>b__1_0(UnityEngine.TerrainData lod)
  RVA=0x09DCA278  token=0x6000D83  HoudiniEngineUnity.Test_TerrainData <ToTestObject>b__2_0(UnityEngine.TerrainData lod)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001B5
SIZE:  0x10
FIELDS:
  public    static readonly HoudiniEngineUnity.Test_Terrain_Extensions.<>c<>9  // static @ 0x0
  public    static  System.Converter<UnityEngine.Terrain,HoudiniEngineUnity.Test_Terrain><>9__1_0  // static @ 0x8
  public    static  System.Converter<UnityEngine.Terrain,HoudiniEngineUnity.Test_Terrain><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x09DCA5C4  token=0x6000D8A  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D8B  System.Void .ctor()
  RVA=0x09DC9C1C  token=0x6000D8C  HoudiniEngineUnity.Test_Terrain <ToTestObject>b__1_0(UnityEngine.Terrain lod)
  RVA=0x09DCA0FC  token=0x6000D8D  HoudiniEngineUnity.Test_Terrain <ToTestObject>b__2_0(UnityEngine.Terrain lod)
END_CLASS

CLASS: PaintMergeMode
TYPE:  sealed struct
TOKEN: 0x20001B7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeModeREPLACE  // const
  public    static  HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeModeADD  // const
  public    static  HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeModeSUBTRACT  // const
  public    static  HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeModeMULTIPLY  // const
METHODS:
END_CLASS

CLASS: PaintMeshVisibility
TYPE:  sealed struct
TOKEN: 0x20001B8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibilityAUTO  // const
  public    static  HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibilitySHOW  // const
  public    static  HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibilityHIDE  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetEventType
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AssetEventTypeUNKNOWN  // const
  public    static  HoudiniEngineUnity.HEU_AssetEventTypeRELOAD  // const
  public    static  HoudiniEngineUnity.HEU_AssetEventTypeCOOK  // const
  public    static  HoudiniEngineUnity.HEU_AssetEventTypeBAKE_NEW  // const
  public    static  HoudiniEngineUnity.HEU_AssetEventTypeBAKE_UPDATE  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetEventData
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x30
FIELDS:
  public            HoudiniEngineUnity.HEU_HoudiniAssetAsset  // 0x10
  public            System.Boolean                  CookSuccess  // 0x18
  public            System.Collections.Generic.List<UnityEngine.GameObject>OutputObjects  // 0x20
  public            HoudiniEngineUnity.HEU_AssetEventTypeEventType  // 0x28
METHODS:
  RVA=0x04044A30  token=0x6000022  System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ReloadEventData
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x30
EXTENDS: HoudiniEngineUnity.HEU_AssetEventData
FIELDS:
METHODS:
  RVA=0x09D53B70  token=0x6000023  System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_CookedEventData
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x30
EXTENDS: HoudiniEngineUnity.HEU_AssetEventData
FIELDS:
METHODS:
  RVA=0x09D381C0  token=0x6000024  System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_BakedEventData
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x38
EXTENDS: HoudiniEngineUnity.HEU_AssetEventData
FIELDS:
  public            System.Boolean                  IsNewBake  // 0x30
METHODS:
  RVA=0x09D37EE0  token=0x6000025  System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean successful, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects, System.Boolean isNewBake)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PreAssetEventData
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x20
FIELDS:
  public            HoudiniEngineUnity.HEU_HoudiniAssetAsset  // 0x10
  public            HoudiniEngineUnity.HEU_AssetEventTypeAssetType  // 0x18
METHODS:
  RVA=0x03B46FF0  token=0x6000026  System.Void .ctor(HoudiniEngineUnity.HEU_HoudiniAsset asset, HoudiniEngineUnity.HEU_AssetEventType assetType)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ReloadDataEvent
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x09D53B34  token=0x6000027  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_CookedDataEvent
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x09D38184  token=0x6000028  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_BakedDataEvent
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x09D37EA4  token=0x6000029  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PreAssetEvent
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x09D53A38  token=0x600002A  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetPreset
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x50
FIELDS:
  public            System.Char[]                   _identifier  // 0x10
  public            System.Int32                    _version  // 0x18
  public            System.String                   _assetOPName  // 0x20
  public            System.Byte[]                   _parameterPreset  // 0x28
  public            System.Collections.Generic.List<System.String>_curveNames  // 0x30
  public            System.Collections.Generic.List<System.Byte[]>_curvePresets  // 0x38
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputPreset>inputPresets  // 0x40
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCachePreset>volumeCachePresets  // 0x48
METHODS:
  RVA=0x09D37BB0  token=0x600002B  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputPreset
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x48
FIELDS:
  private           HoudiniEngineUnity.HEU_InputNode.InputObjectType_inputObjectType  // 0x10
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputObjectPreset>_inputObjectPresets  // 0x18
  public            System.String                   _inputAssetName  // 0x20
  public            System.Int32                    _inputIndex  // 0x28
  public            System.String                   _inputName  // 0x30
  public            System.Boolean                  _keepWorldTransform  // 0x38
  public            System.Boolean                  _packGeometryBeforeMerging  // 0x39
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputAssetPreset>_inputAssetPresets  // 0x40
PROPERTIES:
  InputObjectType  get=0x09D53A0C  set=0x09D53A18
METHODS:
  RVA=0x09D5394C  token=0x600002E  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputObjectPreset
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x50
FIELDS:
  public            System.String                   _gameObjectName  // 0x10
  public            System.Boolean                  _isSceneObject  // 0x18
  public            UnityEngine.GameObject          _gameObject  // 0x20
  public            System.Boolean                  _useTransformOffset  // 0x28
  public            UnityEngine.Vector3             _translateOffset  // 0x2c
  public            UnityEngine.Vector3             _rotateOffset  // 0x38
  public            UnityEngine.Vector3             _scaleOffset  // 0x44
METHODS:
  RVA=0x09D538EC  token=0x600002F  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputAssetPreset
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x18
FIELDS:
  public            System.String                   _gameObjectName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000030  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_VolumeLayerPreset
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x28
FIELDS:
  public            System.String                   _layerName  // 0x10
  public            System.Single                   _strength  // 0x18
  public            System.Boolean                  _uiExpanded  // 0x1c
  public            System.Int32                    _tile  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000031  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_VolumeCachePreset
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x40
FIELDS:
  public            System.String                   _objName  // 0x10
  public            System.String                   _geoName  // 0x18
  public            System.Boolean                  _uiExpanded  // 0x20
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayerPreset>_volumeLayersPresets  // 0x28
  public            System.String                   _terrainDataPath  // 0x30
  public            System.Int32                    _tile  // 0x38
METHODS:
  RVA=0x09D54218  token=0x6000032  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_RecookPreset
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCachePreset>_volumeCachePresets  // 0x10
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputPreset>_inputPresets  // 0x18
METHODS:
  RVA=0x09D53A74  token=0x6000033  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetPresetUtility
TYPE:  static class
TOKEN: 0x200001C
SIZE:  0x10
FIELDS:
  public    static  System.Char[]                   PRESET_IDENTIFIER  // static @ 0x0
  public    static  System.Int32                    PRESET_VERSION  // static @ 0x8
METHODS:
  RVA=0x09D375E8  token=0x6000034  System.Void SaveAssetPresetToFile(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String filePath)
  RVA=0x09D3703C  token=0x6000035  System.Void LoadPresetFileIntoAssetAndCook(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String filePath)
  RVA=0x09D37B2C  token=0x6000036  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Vector3SerializationSurrogate
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x10
IMPLEMENTS: System.Runtime.Serialization.ISerializationSurrogate
FIELDS:
METHODS:
  RVA=0x09D53FC0  token=0x6000037  System.Void System.Runtime.Serialization.ISerializationSurrogate.GetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=0x09D54088  token=0x6000038  System.Object System.Runtime.Serialization.ISerializationSurrogate.SetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Runtime.Serialization.ISurrogateSelector selector)
  RVA=0x041E1670  token=0x6000039  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Vector2SerializationSurrogate
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x10
IMPLEMENTS: System.Runtime.Serialization.ISerializationSurrogate
FIELDS:
METHODS:
  RVA=0x09D53DF0  token=0x600003A  System.Void System.Runtime.Serialization.ISerializationSurrogate.GetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=0x09D53E94  token=0x600003B  System.Object System.Runtime.Serialization.ISerializationSurrogate.SetObjectData(System.Object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Runtime.Serialization.ISurrogateSelector selector)
  RVA=0x041E1670  token=0x600003C  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetSerializedMetaData
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x30
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.Boolean                  _softDeleted  // 0x18
  private           System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData>>_savedCurveNodeData  // 0x20
  private           System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_InputCurveInfo>_savedInputCurveInfo  // 0x28
PROPERTIES:
  SoftDeleted  get=0x04D86340  set=0x04D86350
  SavedCurveNodeData  get=0x04D862C0
  SavedInputCurveInfo  get=0x04D86240
METHODS:
  RVA=0x09D37D08  token=0x6000041  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_AssetSerializedMetaData other)
  RVA=0x09D37D9C  token=0x6000042  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetUpdater
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000043  System.Void .cctor()
  RVA=0x09D37E64  token=0x6000044  System.Void OnBeforeAssemblyReload()
  RVA=0x041E1670  token=0x6000045  System.Void Update()
  RVA=0x041E1670  token=0x6000046  System.Void AddAssetForUpdate(HoudiniEngineUnity.HEU_HoudiniAsset asset)
  RVA=0x041E1670  token=0x6000047  System.Void RemoveAsset(HoudiniEngineUnity.HEU_HoudiniAsset asset)
  RVA=0x041E1670  token=0x6000048  System.Void AddNodeSyncForUpdate(HoudiniEngineUnity.HEU_BaseSync nodeSync)
  RVA=0x041E1670  token=0x6000049  System.Void RemoveNodeSync(HoudiniEngineUnity.HEU_BaseSync nodeSync)
  RVA=0x041E1670  token=0x600004A  System.Void OnPrefabInstanceUpdate(UnityEngine.GameObject instance)
  RVA=0x041E1670  token=0x600004B  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.CurveNodeData
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x38
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Vector3             rotation  // 0x1c
  public            UnityEngine.Vector3             scale  // 0x28
  public            System.Int32                    curveCountIndex  // 0x34
METHODS:
  RVA=0x09D36A84  token=0x600004C  System.Void .ctor()
  RVA=0x09D36A14  token=0x600004D  System.Void .ctor(UnityEngine.Vector3 position)
  RVA=0x09D36AE4  token=0x600004E  System.Void .ctor(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x09D36B6C  token=0x600004F  System.Void .ctor(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale)
  RVA=0x09D36974  token=0x6000050  System.Void .ctor(HoudiniEngineUnity.CurveNodeData other)
  RVA=0x09D367A8  token=0x6000051  UnityEngine.Quaternion GetRotation()
  RVA=0x09D367E4  token=0x6000052  System.Boolean IsEquivalentTo(HoudiniEngineUnity.CurveNodeData other)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputCurveInfo
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x28
FIELDS:
  public            HoudiniEngineUnity.HAPI_CurveTypecurveType  // 0x10
  public            System.Int32                    order  // 0x14
  public            System.Boolean                  closed  // 0x18
  public            System.Boolean                  reverse  // 0x19
  public            HoudiniEngineUnity.HAPI_InputCurveMethodinputMethod  // 0x1c
  public            HoudiniEngineUnity.HAPI_InputCurveParameterizationbreakpointParameterization  // 0x20
METHODS:
  RVA=0x09D53690  token=0x6000053  HoudiniEngineUnity.HEU_InputCurveInfo CreateFromHAPI_InputCurveInfo(HoudiniEngineUnity.HAPI_InputCurveInfo curveInfo)
  RVA=0x09D537BC  token=0x6000054  System.String[] GetCurveTypeNames()
  RVA=0x09D53864  token=0x6000055  System.String[] GetInputMethodNames()
  RVA=0x09D53714  token=0x6000056  System.String[] GetBreakpointParameterizationNames()
  RVA=0x04DA2680  token=0x6000057  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_CurveDataType
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_CurveDataTypeINVALID  // const
  public    static  HoudiniEngineUnity.HEU_CurveDataTypeGEO_COORDS_PARAM  // const
  public    static  HoudiniEngineUnity.HEU_CurveDataTypeHAPI_COORDS_PARAM  // const
  public    static  HoudiniEngineUnity.HEU_CurveDataTypePOSITION_ATTRIBUTE  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Curve
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x90
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_Curve HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.Int32                    _geoID  // 0x18
  private           System.Int32                    _partID  // 0x1c
  private           System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData>_curveNodeData  // 0x20
  private           UnityEngine.Vector3[]           _vertices  // 0x28
  private           System.Boolean                  _isEditable  // 0x30
  private           HoudiniEngineUnity.HEU_Parameters_parameters  // 0x38
  private           System.Boolean                  _bUploadParameterPreset  // 0x40
  private           System.String                   _curveName  // 0x48
  private           UnityEngine.GameObject          _targetGameObject  // 0x50
  private           System.Boolean                  _isGeoCurve  // 0x58
  private           HoudiniEngineUnity.HEU_Curve.CurveEditState_editState  // 0x5c
  public    static  HoudiniEngineUnity.HEU_Curve.InteractionPreferredNextInteractionMode  // static @ 0x0
  private           HoudiniEngineUnity.HEU_HoudiniAsset_parentAsset  // 0x60
  private           System.Boolean                  _bIsInputCurve  // 0x68
  private           System.Boolean                  _bIsPartCurve  // 0x69
  private           System.Boolean                  _cachedCurveInfoValid  // 0x6a
  private           System.Int32[]                  _cachedCurveCounts  // 0x70
  private           System.Int32[]                  _cachedCurveCountSums  // 0x78
  private           HoudiniEngineUnity.HEU_CurveDataType_curveDataType  // 0x80
  private           HoudiniEngineUnity.HEU_InputCurveInfo_inputCurveInfo  // 0x88
PROPERTIES:
  TargetGameObject  get=0x04D86270  set=0x02FDAEA0
  GeoID  get=0x011EF5B0
  PartID  get=0x04D86310
  CurveNodeData  get=0x04D862C0
  Parameters  get=0x04D85A50
  CurveName  get=0x04D86200
  IsInputCurve  get=0x04D86B40
  IsPartCurve  get=0x04D86B10
  InputCurveInfo  get=0x04D861F0
  EditState  get=0x04D86560
  ParentAsset  get=0x04D86280
  CurveDataType  get=0x04D863A0
METHODS:
  RVA=0x02D480B0  token=0x6000062  System.Void SetUploadParameterPreset(System.Boolean bValue)
  RVA=0x09D39D50  token=0x6000066  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=0x09D3AB9C  token=0x6000067  System.Void Recook()
  RVA=0x09D3AB34  token=0x6000068  System.Void Rebuild()
  RVA=0x04D865F0  token=0x6000069  System.Boolean IsEditable()
  RVA=0x04D86F70  token=0x600006A  System.Boolean IsGeoCurve()
  RVA=0x09D3AE50  token=0x600006B  System.Void SetCurveName(System.String name)
  RVA=0x09D3B168  token=0x600006C  System.Void SetCurvePoint(System.Int32 pointIndex, UnityEngine.Vector3 newPosition, System.Boolean bRecookAsset)
  RVA=0x09D3B024  token=0x600006D  System.Void SetCurvePoint(System.Int32 pointIndex, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset)
  RVA=0x09D3AED0  token=0x600006E  System.Void SetCurveNodeData(System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> curveNodeData, System.Boolean bRecookAsset)
  RVA=0x09D39638  token=0x600006F  UnityEngine.Vector3 GetCurvePoint(System.Int32 pointIndex)
  RVA=0x04D862C0  token=0x6000070  System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> GetAllPointTransforms()
  RVA=0x09D39400  token=0x6000071  System.Collections.Generic.List<UnityEngine.Vector3> GetAllPoints()
  RVA=0x09D397C0  token=0x6000072  System.Int32 GetNumPoints()
  RVA=0x09D3A0D8  token=0x6000073  System.Void InsertCurvePoint(System.Int32 index, UnityEngine.Vector3 position, System.Boolean bRecookAsset)
  RVA=0x09D3A030  token=0x6000074  System.Void InsertCurvePoint(System.Int32 index, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset)
  RVA=0x09D38280  token=0x6000075  System.Void AddCurvePointToEnd(UnityEngine.Vector3 position, System.Boolean bRecookAsset)
  RVA=0x09D381E4  token=0x6000076  System.Void AddCurvePointToEnd(HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset)
  RVA=0x09D3AC08  token=0x6000077  System.Void RemoveCurvePoint(System.Int32 pointIndex, System.Boolean bRecookAsset)
  RVA=0x09D38378  token=0x6000078  System.Void ClearCurveNodeData(System.Boolean bRecookAsset)
  RVA=0x09D3AAE0  token=0x6000079  System.Void ProjectToColliders(UnityEngine.Vector3 rayDirection, System.Single rayDistance, System.Boolean bRecookAsset)
  RVA=0x09D3AE1C  token=0x600007A  System.Void SetCurveGeometryVisibility(System.Boolean bVisible, System.Boolean bRecookAsset)
  RVA=0x09D39DE4  token=0x600007B  UnityEngine.Vector3 GetTransformedPoint(System.Int32 pointIndex)
  RVA=0x09D39EAC  token=0x600007C  System.Collections.Generic.List<UnityEngine.Vector3> GetTransformedPoints()
  RVA=0x09D38AC4  token=0x600007D  System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> DuplicateCurveNodeData()
  RVA=0x09D38404  token=0x600007E  HoudiniEngineUnity.HEU_Curve CreateSetupCurve(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean isEditable, System.String curveName, System.Int32 geoID, System.Int32 partID, System.Boolean bGeoCurve)
  RVA=0x09D3C6D8  token=0x600007F  System.Void UsePreviousCurveData(System.String curveName)
  RVA=0x09D395F0  token=0x6000080  HoudiniEngineUnity.HEU_CurveDataType GetCurveDataType(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D3B23C  token=0x6000081  System.Boolean ShouldKeepNode(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D387A0  token=0x6000082  System.Void DestroyAllData(System.Boolean bIsRebuild)
  RVA=0x09D3C574  token=0x6000083  System.Void UploadParameterPreset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x09D3ACA0  token=0x6000084  System.Void ResetCurveParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x09D3AF54  token=0x6000085  System.Void SetCurveParameterPreset(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Byte[] parameterPreset)
  RVA=0x09D3BD30  token=0x6000086  System.Void UpdateCurve(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partId)
  RVA=0x09D39580  token=0x6000087  System.Int32[] GetCurveCounts(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoId, System.Int32 partID)
  RVA=0x09D39030  token=0x6000088  System.Void GenerateMesh(UnityEngine.GameObject inGameObject, HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D38C88  token=0x6000089  System.Void GenerateMeshForSingleObject(UnityEngine.GameObject targetObject, UnityEngine.Vector3[] vertexList)
  RVA=0x09D3A4E8  token=0x600008A  System.Void OnPresyncParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x09D39800  token=0x600008B  System.Int32 GetOrderForCurveType(System.Int32 requestedOrder, HoudiniEngineUnity.HAPI_CurveType curveType)
  RVA=0x09D3B444  token=0x600008C  System.Boolean UpdateCurveInputForCurveParts(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x09D3B994  token=0x600008D  System.Boolean UpdateCurveInputForCustomAttributes(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x09D3B294  token=0x600008E  System.Void SyncFromParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bNewCurve)
  RVA=0x09D3BE4C  token=0x600008F  System.Void UpdatePoints(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D3A520  token=0x6000090  System.Void ProjectToCollidersInternal(HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, UnityEngine.Vector3 rayDirection, System.Single rayDistance)
  RVA=0x09D39AC4  token=0x6000091  System.String GetPointsString(System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> points)
  RVA=0x09D3984C  token=0x6000092  System.String GetPointsString(System.Collections.Generic.List<UnityEngine.Vector3> points)
  RVA=0x04D865C0  token=0x6000093  System.Void SetEditState(HoudiniEngineUnity.HEU_Curve.CurveEditState editState)
  RVA=0x09D39FC0  token=0x6000094  UnityEngine.Vector3 GetTransformedPosition(UnityEngine.Vector3 inPosition)
  RVA=0x09D39750  token=0x6000095  UnityEngine.Vector3 GetInvertedTransformedPosition(UnityEngine.Vector3 inPosition)
  RVA=0x09D396E0  token=0x6000096  UnityEngine.Vector3 GetInvertedTransformedDirection(UnityEngine.Vector3 inPosition)
  RVA=0x04D86240  token=0x6000097  UnityEngine.Vector3[] GetVertices()
  RVA=0x09D3AD60  token=0x6000098  System.Void SetCurveGeometryVisibilityInternal(System.Boolean bVisible)
  RVA=0x09D38A48  token=0x6000099  System.Void DownloadPresetData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D3C65C  token=0x600009A  System.Void UploadPresetData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D389CC  token=0x600009B  System.Void DownloadAsDefaultPresetData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D3B33C  token=0x600009C  System.Void UpdateCachedCurveInfo(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean copyCurveSettings)
  RVA=0x09D39510  token=0x600009D  System.Int32 GetCurveCountIndexFromPositionIndex(System.Int32 positionIndex)
  RVA=0x09D3A4C0  token=0x600009E  System.Boolean IsMeshCurve(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09D3A1D0  token=0x600009F  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_Curve other)
  RVA=0x09D3C944  token=0x60000A0  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GeneratedOutput
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x28
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HEU_GeneratedOutputData_outputData  // 0x10
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutputData>_childOutputs  // 0x18
  private           System.Boolean                  isInstancer  // 0x20
PROPERTIES:
  IsInstancer  get=0x04D866B0  set=0x04D866D0
METHODS:
  RVA=0x09D3EC14  token=0x60000AD  System.Void ResetMaterialOverrides(HoudiniEngineUnity.HEU_GeneratedOutput output)
  RVA=0x09D3ED5C  token=0x60000AE  System.Void ResetMaterialOverrides(HoudiniEngineUnity.HEU_GeneratedOutputData outputData)
  RVA=0x09D3E5C8  token=0x60000AF  UnityEngine.Material[] GetGeneratedMaterialsForGameObject(HoudiniEngineUnity.HEU_GeneratedOutput output, UnityEngine.GameObject inGameObject)
  RVA=0x09D3E784  token=0x60000B0  System.Boolean HasLODGroup(HoudiniEngineUnity.HEU_GeneratedOutput output)
  RVA=0x09D3EAB4  token=0x60000B1  System.Boolean IsOutputUsingMaterial(UnityEngine.Material checkMaterial, HoudiniEngineUnity.HEU_GeneratedOutput output)
  RVA=0x09D3E948  token=0x60000B2  System.Boolean IsOutputDataUsingMaterial(UnityEngine.Material checkMaterial, HoudiniEngineUnity.HEU_GeneratedOutputData outputData)
  RVA=0x09D3DD60  token=0x60000B3  System.Void ClearGeneratedMaterialReferences(HoudiniEngineUnity.HEU_GeneratedOutputData generatedOutputData)
  RVA=0x09D3E090  token=0x60000B4  System.Void DestroyAllGeneratedColliders(HoudiniEngineUnity.HEU_GeneratedOutputData outputData)
  RVA=0x09D3E424  token=0x60000B5  System.Void DestroyGeneratedOutput(HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput)
  RVA=0x09D3E268  token=0x60000B6  System.Void DestroyGeneratedOutputChildren(HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput)
  RVA=0x09D3E3CC  token=0x60000B7  System.Void DestroyGeneratedOutputData(HoudiniEngineUnity.HEU_GeneratedOutputData generatedOutputData, System.Boolean bDontDeletePersistantResources)
  RVA=0x09D3DD84  token=0x60000B8  System.Void ClearMaterialsNoLongerUsed(UnityEngine.Material[] materialsToCheck, UnityEngine.Material[] materialsInUse)
  RVA=0x09D3DE54  token=0x60000B9  System.Void CopyMaterialOverrides(HoudiniEngineUnity.HEU_GeneratedOutputData sourceOutputData, HoudiniEngineUnity.HEU_GeneratedOutputData destOutputData)
  RVA=0x09D3EE14  token=0x60000BA  System.Void WriteOutputToAssetCache(UnityEngine.GameObject parentObject, System.String outputPath, System.Boolean bIsInstancer)
  RVA=0x09D3DC18  token=0x60000BB  System.Void BakeGameObjectComponents(UnityEngine.GameObject sourceGO, UnityEngine.GameObject targetGO, System.String assetName, System.String outputPath, System.Boolean bIsInstancer)
  RVA=0x09D3E7CC  token=0x60000BC  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_GeneratedOutput other)
  RVA=0x09D3EE84  token=0x60000BD  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GeneratedOutputData
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x28
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.GameObject          _gameObject  // 0x10
  public            UnityEngine.Material[]          _renderMaterials  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Collider>_colliders  // 0x20
METHODS:
  RVA=0x09D3D9F0  token=0x60000BE  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_GeneratedOutputData other)
  RVA=0x09D3DBA0  token=0x60000BF  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GeoNode
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x78
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_GeoNode HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent UnityEngine.ISerializationCallbackReceiver HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           HoudiniEngineUnity.HAPI_GeoInfo _geoInfo  // 0x18
  private           System.String                   _geoName  // 0x40
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData>_parts  // 0x48
  private           HoudiniEngineUnity.HEU_ObjectNode_containerObjectNode  // 0x50
  private           HoudiniEngineUnity.HEU_InputNode_inputNode  // 0x58
  private           HoudiniEngineUnity.HEU_Curve    _geoCurve  // 0x60
  private           HoudiniEngineUnity.HEU_VolumeCache_volumeCache  // 0x68
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache>_volumeCaches  // 0x70
PROPERTIES:
  ParentAsset  get=0x09D42D6C
  GeoID  get=0x02B70F90
  GeoInfo  get=0x04D9A780
  GeoName  get=0x04D85A60
  GeoType  get=0x011EF5B0
  Editable  get=0x04D86870
  Displayable  get=0x09D42CEC
  Parts  get=0x04D86200
  ObjectNode  get=0x04D86270
  InputNode  get=0x02E56440
  GeoCurve  get=0x04D86280
  VolumeCaches  get=0x04D862B0
METHODS:
  RVA=0x09D42CCC  token=0x60000CC  System.Void .ctor()
  RVA=0x041E1670  token=0x60000CD  System.Void OnBeforeSerialize()
  RVA=0x09D40F28  token=0x60000CE  System.Void OnAfterDeserialize()
  RVA=0x09D40660  token=0x60000CF  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=0x09D42038  token=0x60000D0  System.Void Recook()
  RVA=0x09D40F0C  token=0x60000D1  System.Boolean IsVisible()
  RVA=0x09D40DD8  token=0x60000D2  System.Boolean IsIntermediate()
  RVA=0x09D40DC0  token=0x60000D3  System.Boolean IsIntermediateOrEditable()
  RVA=0x09D40DAC  token=0x60000D4  System.Boolean IsGeoInputType()
  RVA=0x09D40DA4  token=0x60000D5  System.Boolean IsGeoCurveType()
  RVA=0x09D3F27C  token=0x60000D6  System.Void DestroyAllData(System.Boolean bIsRebuild)
  RVA=0x09D420AC  token=0x60000D7  System.Void RemoveAndDestroyPart(HoudiniEngineUnity.HEU_PartData part)
  RVA=0x09D40288  token=0x60000D8  System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=0x09D403AC  token=0x60000D9  System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=0x09D40130  token=0x60000DA  HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject)
  RVA=0x09D404D0  token=0x60000DB  HoudiniEngineUnity.HEU_PartData GetPartFromPartID(System.Int32 partID)
  RVA=0x09D3FB94  token=0x60000DC  System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly)
  RVA=0x04D86200  token=0x60000DD  System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> GetParts()
  RVA=0x09D40990  token=0x60000DE  System.Void HideAllGeometry()
  RVA=0x09D3F5DC  token=0x60000DF  System.Void DisableAllColliders()
  RVA=0x09D40704  token=0x60000E0  HoudiniEngineUnity.HEU_VolumeCache GetVolumeCacheByTileIndex(System.Int32 tileIndex)
  RVA=0x09D42110  token=0x60000E1  System.Void Reset()
  RVA=0x09D40A24  token=0x60000E2  System.Void Initialize(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_GeoInfo geoInfo, HoudiniEngineUnity.HEU_ObjectNode containerObjectNode)
  RVA=0x09D3F670  token=0x60000E3  System.Boolean DoesThisRequirePotentialCook()
  RVA=0x09D42944  token=0x60000E4  System.Void UpdateGeo(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D419AC  token=0x60000E5  System.Void ProcessUnityScriptAttribute(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D41214  token=0x60000E6  System.Void ProcessPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partID, HoudiniEngineUnity.HAPI_PartInfo& partInfo, HoudiniEngineUnity.HEU_PartData& partData)
  RVA=0x09D4246C  token=0x60000E7  System.Void SetupGameObjectAndTransform(HoudiniEngineUnity.HEU_PartData partData, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x09D40580  token=0x60000E8  System.Void GetPartsByOutputType(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> meshParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> volumeParts)
  RVA=0x09D3F830  token=0x60000E9  System.Void GeneratePartInstances(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D3F6B8  token=0x60000EA  System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D4101C  token=0x60000EB  System.Void ProcessGeoCurve(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D4271C  token=0x60000EC  System.Void SetupGeoCurveGameObjectAndTransform(HoudiniEngineUnity.HEU_Curve curve)
  RVA=0x09D3F1F4  token=0x60000ED  System.Void ClearObjectInstances()
  RVA=0x09D423E8  token=0x60000EE  System.Void SetGeoInfo(HoudiniEngineUnity.HAPI_GeoInfo geoInfo)
  RVA=0x09D3F758  token=0x60000EF  System.String GeneratePartFullName(System.String partName)
  RVA=0x04D85A60  token=0x60000F0  System.String GenerateGeoCurveName()
  RVA=0x09D40958  token=0x60000F1  System.Boolean HasGeoNodeChanged(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D3EF30  token=0x60000F2  System.Void ApplyHAPITransform(HoudiniEngineUnity.HAPI_Transform& hapiTransform)
  RVA=0x09D3FD90  token=0x60000F3  System.Void GetDebugInfo(System.Text.StringBuilder sb)
  RVA=0x09D40DE0  token=0x60000F4  System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData)
  RVA=0x09D3FA70  token=0x60000F5  System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts)
  RVA=0x09D40840  token=0x60000F6  System.Boolean HasAttribInstancer()
  RVA=0x09D421C0  token=0x60000F7  System.Void SetAttributeModifiersOnPartOutputs(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D3F0E4  token=0x60000F8  System.Void CalculateVisiblity(System.Boolean bParentVisibility)
  RVA=0x09D3F054  token=0x60000F9  System.Void CalculateColliderState()
  RVA=0x09D41B60  token=0x60000FA  System.Void ProcessVolumeParts(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> volumeParts, System.Boolean bRebuild)
  RVA=0x09D3F454  token=0x60000FB  System.Void DestroyVolumeCache()
  RVA=0x09D428E8  token=0x60000FC  System.String ToString()
  RVA=0x09D40B40  token=0x60000FD  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_GeoNode other)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HoudiniAsset
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x268
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: HoudiniEngineUnity.IHEU_HoudiniAsset HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType_assetType  // 0x18
  private           HoudiniEngineUnity.HAPI_AssetInfo_assetInfo  // 0x1c
  private           HoudiniEngineUnity.HAPI_NodeInfo_nodeInfo  // 0x5c
  private           System.String                   _assetName  // 0xa0
  private           System.String                   _assetOpName  // 0xa8
  private           System.String                   _assetHelp  // 0xb0
  private           System.Int32                    _assetID  // 0xb8
  private           System.String                   _assetPath  // 0xc0
  private           System.Boolean                  _loadAssetFromMemory  // 0xc8
  private           System.Boolean                  _alwaysOverwriteOnLoad  // 0xc9
  private           UnityEngine.Object              _assetFileObject  // 0xd0
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectNode>_objectNodes  // 0xd8
  private           UnityEngine.GameObject          _rootGameObject  // 0xe0
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData>_materialCache  // 0xe8
  private           HoudiniEngineUnity.HEU_Parameters_parameters  // 0xf0
  private           UnityEngine.Matrix4x4           _lastSyncedTransformMatrix  // 0xf8
  private           System.Collections.Generic.List<UnityEngine.Matrix4x4>_lastSyncedChildTransformMatrices  // 0x138
  private           System.String                   _assetCacheFolderPath  // 0x140
  private           System.String[]                 _subassetNames  // 0x148
  private           System.Int32                    _selectedSubassetIndex  // 0x150
  private           HoudiniEngineUnity.HEU_AssetPreset_savedAssetPreset  // 0x158
  private           HoudiniEngineUnity.HEU_RecookPreset_recookPreset  // 0x160
  private           System.Int32                    _totalCookCount  // 0x168
  private           HoudiniEngineUnity.HEU_HoudiniAsset.AssetBuildAction_requestBuildAction  // 0x16c
  private           System.Boolean                  _checkParameterChangeForCook  // 0x170
  private           System.Boolean                  _skipCookCheck  // 0x171
  private           System.Boolean                  _uploadParameters  // 0x172
  private           System.Boolean                  _forceUploadInputs  // 0x173
  private           System.Boolean                  _upstreamCookChanged  // 0x174
  private           HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus_cookStatus  // 0x178
  private           HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult_lastCookResult  // 0x17c
  private           System.Boolean                  _isCookingAssetReloaded  // 0x180
  private           System.Boolean                  _bForceUpdate  // 0x181
  private           System.Int64                    _sessionID  // 0x188
  private           System.Boolean                  <WarnedPrefabNotSupported>k__BackingField  // 0x190
  private           System.Boolean                  _uiLocked  // 0x191
  private           System.Boolean                  _showHDAOptions  // 0x192
  private           System.Boolean                  _showGenerateSection  // 0x193
  private           System.Boolean                  _showBakeSection  // 0x194
  private           System.Boolean                  _showEventsSection  // 0x195
  private           System.Boolean                  _showCurvesSection  // 0x196
  private           System.Boolean                  _showInputNodesSection  // 0x197
  private           System.Boolean                  _showToolsSection  // 0x198
  private           System.Boolean                  _showTerrainSection  // 0x199
  private           HoudiniEngineUnity.HEU_InstanceInputUIState_instanceInputUIState  // 0x1a0
  private           HoudiniEngineUnity.HEU_ReloadDataEvent_reloadDataEvent  // 0x1a8
  private           HoudiniEngineUnity.HEU_CookedDataEvent_cookedDataEvent  // 0x1b0
  private           HoudiniEngineUnity.HEU_BakedDataEvent_bakedDataEvent  // 0x1b8
  private           HoudiniEngineUnity.HEU_PreAssetEvent_preAssetEvent  // 0x1c0
  private           HoudiniEngineUnity.HEU_HoudiniAsset.UpdateUIDelegate_refreshUIDelegate  // 0x1c8
  private           HoudiniEngineUnity.HEU_CookedDataEvent_downstreamConnectionCookedEvent  // 0x1d0
  private           System.Boolean                  _generateUVs  // 0x1d8
  private           System.Boolean                  _generateTangents  // 0x1d9
  private           System.Boolean                  _generateNormals  // 0x1da
  private           System.Boolean                  _pushTransformToHoudini  // 0x1db
  private           System.Boolean                  _transformChangeTriggersCooks  // 0x1dc
  private           System.Boolean                  _cookingTriggersDownCooks  // 0x1dd
  private           System.Boolean                  _autoCookOnParameterChange  // 0x1de
  private           System.Boolean                  _ignoreNonDisplayNodes  // 0x1df
  private           System.Boolean                  _useOutputNodes  // 0x1e0
  private           System.Boolean                  _generateMeshUsingPoints  // 0x1e1
  private           System.Boolean                  _useLODGroups  // 0x1e2
  private           System.Boolean                  _splitGeosByGroup  // 0x1e3
  private           System.Boolean                  _sessionSyncAutoCook  // 0x1e4
  private           System.Boolean                  _bakeUpdateKeepPreviousTransformValues  // 0x1e5
  private           System.Boolean                  _pauseCooking  // 0x1e6
  private           System.Boolean                  _curveEditorEnabled  // 0x1e7
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve>_curves  // 0x1e8
  private           HoudiniEngineUnity.HEU_Curve.CurveDrawCollision_curveDrawCollision  // 0x1f0
  private           System.Collections.Generic.List<UnityEngine.Collider>_curveDrawColliders  // 0x1f8
  private           UnityEngine.LayerMask           _curveDrawLayerMask  // 0x200
  private           System.Single                   _curveProjectMaxDistance  // 0x204
  private           UnityEngine.Vector3             _curveProjectDirection  // 0x208
  private           System.Boolean                  _curveProjectDirectionToView  // 0x214
  private           System.Boolean                  _curveDisableScaleRotation  // 0x215
  private           System.Boolean                  _curveFrameSelectedNodes  // 0x216
  private           System.Single                   _curveFrameSelectedNodeDistance  // 0x218
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode>_inputNodes  // 0x220
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle>_handles  // 0x228
  private           System.Boolean                  _handlesEnabled  // 0x230
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache>_volumeCaches  // 0x238
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributesStore>_attributeStores  // 0x240
  private           System.Boolean                  _editableNodesToolsEnabled  // 0x248
  private           HoudiniEngineUnity.HEU_ToolsInfo_toolsInfo  // 0x250
  private           HoudiniEngineUnity.HEU_AssetSerializedMetaData_serializedMetaData  // 0x258
  private           System.Boolean                  _pendingAutoCookOnMouseRelease  // 0x260
PROPERTIES:
  LoadAssetFromMemory  get=0x04D8E270  set=0x04D8E290
  AlwaysOverwriteOnLoad  get=0x04DA2350  set=0x04DA2520
  GenerateUVs  get=0x04DA2460  set=0x04DA2600
  GenerateTangents  get=0x04DA2450  set=0x04DA25F0
  GenerateNormals  get=0x04DA2440  set=0x04DA25E0
  PushTransformToHoudini  get=0x04DA24C0  set=0x04DA2630
  TransformChangeTriggersCooks  get=0x04DA2500  set=0x04DA2660
  CookingTriggersDownCooks  get=0x04DA23B0  set=0x04DA2550
  AutoCookOnParameterChange  get=0x04DA2390  set=0x04DA2530
  IgnoreNonDisplayNodes  get=0x04DA2480  set=0x04DA2620
  UseOutputNodes  get=0x04D91ED0  set=0x04D91EE0
  GenerateMeshUsingPoints  get=0x04DA2430  set=0x04DA25D0
  UseLODGroups  get=0x04DA2510  set=0x04DA2670
  SplitGeosByGroup  get=0x04DA24F0  set=0x04DA2650
  SessionSyncAutoCook  get=0x04DA24E0  set=0x04DA2640
  BakeUpdateKeepPreviousTransformValues  get=0x04DA23A0  set=0x04DA2540
  PauseCooking  get=0x04D90890  set=0x04D908B0
  CurveEditorEnabled  get=0x04D908A0  set=0x04D908C0
  CurveDrawCollision  get=0x09D53620  set=0x09D53654
  CurveDrawLayerMask  get=0x04DA2310  set=0x04DA2340
  CurveProjectMaxDistance  get=0x04DA2420  set=0x04DA25C0
  CurveProjectDirection  get=0x04DA2400  set=0x04DA25A0
  CurveProjectDirectionToView  get=0x04DA23F0  set=0x04DA2590
  CurveDisableScaleRotation  get=0x04D8E560  set=0x04DA2560
  CurveFrameSelectedNodes  get=0x04DA23E0  set=0x04DA2580
  CurveFrameSelectedNodeDistance  get=0x04DA23D0  set=0x04DA2570
  HandlesEnabled  get=0x04DA2470  set=0x04DA2610
  EditableNodesToolsEnabled  get=0x04D90930  set=0x04D90960
  AssetType  get=0x09D535E4
  AssetInfo  get=0x04DA2360
  NodeInfo  get=0x04DA2490
  AssetName  get=0x04D86210
  AssetOpName  get=0x04D86290
  AssetHelp  get=0x04D86250
  AssetID  get=0x04D8E580
  AssetPath  get=0x04D892F0
  OwnerGameObject  get=0x053916BC
  RootGameObject  get=0x04D8D1C0
  MaterialCache  get=0x04D88590
  Parameters  get=0x04D8D1A0
  AssetCacheFolder  get=0x04D8DA60
  SubassetNames  get=0x04D8DAC0
  SelectedSubassetIndex  get=0x04DA24D0
  CookStatus  get=0x09D53610
  LastCookResult  get=0x09D53630
  SessionID  get=0x04D8DA20
  Curves  get=0x04D87930
  InputNodes  get=0x04D8D870
  VolumeCaches  get=0x04D8DAD0
  ReloadDataEvent  get=0x04D8D9C0
  CookedDataEvent  get=0x04D8D960
  BakedDataEvent  get=0x04D8DA90
  PreAssetEvent  get=0x04D8DA80
  AssetTypeInternal  get=0x011EF5B0
  WarnedPrefabNotSupported  get=0x04D8FB60  set=0x04D8FC50
  InstanceInputUIState  get=0x04D8DA10  set=0x05397204
  RefreshUIDelegate  get=0x04D8D790  set=0x06B8B0D0
  CurveDrawCollisionInternal  get=0x04DA23C0
  Handles  get=0x04D8D440
  AttributeStores  get=0x04D8DE80
  ToolsInfo  get=0x04D8DCE0
  SerializedMetaData  get=0x04D8DE70
  PendingAutoCookOnMouseRelease  get=0x04D8F3F0  set=0x09D53674
METHODS:
  RVA=0x04DA2300  token=0x6000150  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus GetCookStatus()
  RVA=0x04D92A50  token=0x6000151  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult GetLastCookResult()
  RVA=0x04D87920  token=0x6000159  System.Collections.Generic.List<UnityEngine.Collider> GetCurveDrawColliders()
  RVA=0x04DA2310  token=0x600015A  UnityEngine.LayerMask GetCurveDrawLayerMask()
  RVA=0x04DA2340  token=0x600015B  System.Void SetCurveDrawLayerMask(UnityEngine.LayerMask mask)
  RVA=0x02FFF600  token=0x6000162  System.Boolean RequestCook(System.Boolean bCheckParametersChanged, System.Boolean bAsync, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters)
  RVA=0x02FFF600  token=0x6000163  System.Boolean RequestReload(System.Boolean bAsync)
  RVA=0x02FFF600  token=0x6000164  System.Boolean RequestResetParameters(System.Boolean bAsync)
  RVA=0x09D4866C  token=0x6000165  UnityEngine.GameObject DuplicateAsset(UnityEngine.GameObject newRootGameObject)
  RVA=0x09D479B4  token=0x6000166  System.Boolean DeleteAllGeneratedData(System.Boolean bIsRebuild)
  RVA=0x09D44B40  token=0x6000167  UnityEngine.GameObject BakeToNewPrefab(System.String destinationPrefabPath)
  RVA=0x09D44E54  token=0x6000168  UnityEngine.GameObject BakeToNewStandalone()
  RVA=0x09D43A2C  token=0x6000169  System.Boolean BakeToExistingPrefab(UnityEngine.GameObject bakeTargetGO)
  RVA=0x09D43F64  token=0x600016A  System.Boolean BakeToExistingStandalone(UnityEngine.GameObject bakeTargetGO)
  RVA=0x09D4D490  token=0x600016B  System.Boolean IsAssetValid()
  RVA=0x09D4BC18  token=0x600016C  System.Boolean GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=0x09D4BE68  token=0x600016D  System.Boolean GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=0x09D4A5A4  token=0x600016E  HoudiniEngineUnity.HEU_Curve GetCurve(System.String curveName)
  RVA=0x09D42F2C  token=0x600016F  System.Boolean AddCurveDrawCollider(UnityEngine.Collider newCollider)
  RVA=0x09D4FD40  token=0x6000170  System.Boolean RemoveCurveDrawCollider(UnityEngine.Collider collider)
  RVA=0x09D457B4  token=0x6000171  System.Boolean ClearCurveDrawColliders()
  RVA=0x09D4ACAC  token=0x6000172  HoudiniEngineUnity.HEU_InputNode GetInputNode(System.String inputName)
  RVA=0x09D49A8C  token=0x6000173  HoudiniEngineUnity.HEU_InputNode GetAssetInputNode(System.String inputName)
  RVA=0x09D4AC38  token=0x6000174  HoudiniEngineUnity.HEU_InputNode GetInputNodeByIndex(System.Int32 index)
  RVA=0x09D4B2DC  token=0x6000175  System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> GetNonParameterInputNodes()
  RVA=0x09D4C284  token=0x6000176  System.Int32 GetVolumeCacheCount()
  RVA=0x09D4A298  token=0x6000177  HoudiniEngineUnity.HEU_SessionBase GetAssetSession(System.Boolean bCreateIfInvalid)
  RVA=0x09D4B668  token=0x6000178  HoudiniEngineUnity.HEU_ObjectNode GetObjectNodeByName(System.String objName)
  RVA=0x09D4BD40  token=0x6000179  System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outputGeoNodes)
  RVA=0x09D4B024  token=0x600017A  HoudiniEngineUnity.HEU_PartData GetInternalHDAPartWithGameObject(UnityEngine.GameObject outputGameObject)
  RVA=0x09D509BC  token=0x600017B  System.Void ResetParametersToDefault()
  RVA=0x09D4C878  token=0x600017C  System.Void HideAllGeometry()
  RVA=0x09D47CA8  token=0x600017D  System.Void DisableAllColliders()
  RVA=0x09D4B180  token=0x600017E  HoudiniEngineUnity.HEU_MaterialData GetMaterialData(UnityEngine.Material material)
  RVA=0x09D45C10  token=0x600017F  System.Void ClearMaterialCache()
  RVA=0x09D4FEA4  token=0x6000180  System.Void RemoveMaterial(UnityEngine.Material material)
  RVA=0x09D50860  token=0x6000181  System.Void ResetMaterialOverrides()
  RVA=0x09D49BD4  token=0x6000182  HoudiniEngineUnity.HEU_AssetPreset GetAssetPreset(System.Boolean sceneRelativeGameObjects)
  RVA=0x09D4BAB0  token=0x6000183  HoudiniEngineUnity.HEU_PDGAssetLink GetOrCreatePDGAssetLink()
  RVA=0x09D4C998  token=0x6000184  HoudiniEngineUnity.HEU_HoudiniAssetRoot InstantiateHDA(System.String filePath, System.Boolean bAsync, UnityEngine.Vector3 initialPosition)
  RVA=0x09D4C0B0  token=0x6000185  HoudiniEngineUnity.HEU_PartData GetSceneHDAPartWithGameObject(UnityEngine.GameObject outputGameObject)
  RVA=0x09D4BF90  token=0x6000186  HoudiniEngineUnity.HEU_HoudiniAsset GetSceneHDAAssetFromGameObject(UnityEngine.GameObject outputGameObject)
  RVA=0x09D4E3CC  token=0x6000187  System.Boolean IsHoudiniAssetOutput(UnityEngine.GameObject go)
  RVA=0x09D4E4F8  token=0x6000188  System.Boolean IsHoudiniAssetRoot(UnityEngine.GameObject go)
  RVA=0x09D51148  token=0x6000189  System.Void SetupAsset(HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType assetType, System.String filePath, UnityEngine.GameObject rootGameObject, HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D451DC  token=0x600018A  System.Void CleanUpAndDisable()
  RVA=0x09D4D440  token=0x600018B  System.Boolean IsAssetSavedInScene()
  RVA=0x041E1670  token=0x600018C  System.Void Awake()
  RVA=0x04DA2320  token=0x600018D  System.Void InvalidateAsset()
  RVA=0x041E1670  token=0x600018E  System.Void OnEnable()
  RVA=0x041E1670  token=0x600018F  System.Void OnDestroy()
  RVA=0x041E1670  token=0x6000190  System.Void AssetUpdate()
  RVA=0x041E1670  token=0x6000191  System.Void PostAssetUpdate()
  RVA=0x09D50590  token=0x6000192  System.Void RequestBakeInPlace()
  RVA=0x04DA22E0  token=0x6000193  System.Void ClearBuildRequest()
  RVA=0x09D4C860  token=0x6000194  System.Boolean HasValidAssetPath()
  RVA=0x09D4F664  token=0x6000195  System.Void ProcessRebuild(System.Boolean bPromptForSubasset, System.Int32 desiredSubassetIndex)
  RVA=0x09D4D384  token=0x6000196  System.Void InvokeReloadEvent(System.Boolean bCookSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=0x09D51560  token=0x6000197  System.Boolean StartRebuild(System.Boolean bPromptForSubasset, System.Int32 desiredSubassetIndex)
  RVA=0x09D48A88  token=0x6000198  System.Boolean FinishRebuild()
  RVA=0x09D4F9DC  token=0x6000199  System.Boolean RecookAsync(System.Boolean bCheckParamsChanged, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters, System.Boolean bUploadParameterPreset, System.Boolean bForceUploadInputs, System.Boolean bCookingSessionSync)
  RVA=0x09D4FAFC  token=0x600019A  System.Boolean RecookBlocking(System.Boolean bCheckParamsChanged, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters, System.Boolean bUploadParameterPreset, System.Boolean bForceUploadInputs, System.Boolean bCookingSessionSync)
  RVA=0x09D47E50  token=0x600019B  System.Void DoPostCookWork(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D4E564  token=0x600019C  System.Boolean IsValidForInteraction(System.String& errorMessage)
  RVA=0x09D4EF4C  token=0x600019D  System.Void OnValidate()
  RVA=0x09D489EC  token=0x600019E  System.Void ExecutePostCookCallbacks()
  RVA=0x09D4D2C8  token=0x600019F  System.Void InvokePostCookEvent(System.Boolean bCookSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=0x09D4CBB0  token=0x60001A0  System.Boolean InternalStartRecook(System.Boolean bCheckParamsChanged, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters, System.Boolean bUploadParameterPreset, System.Boolean bForceUploadInputs, System.Boolean bCookingSessionSync)
  RVA=0x09D4CA58  token=0x60001A1  System.Void InternalSetAssetID(System.Int32 assetID)
  RVA=0x04DA2330  token=0x60001A2  System.Void SetCookStatus(HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus status, HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult result)
  RVA=0x09D4F288  token=0x60001A3  System.Void ProcessPoskCook()
  RVA=0x09D5153C  token=0x60001A4  System.Boolean StartHoudiniCookNode(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D4F0D8  token=0x60001A5  System.Void ProcessHoudiniCookStatus(System.Boolean bAsync)
  RVA=0x09D48194  token=0x60001A6  System.Boolean DoesAssetRequireRecook()
  RVA=0x09D47C94  token=0x60001A7  System.Void DeleteSessionDataOnly()
  RVA=0x09D45354  token=0x60001A8  System.Void CleanUpInputNodes()
  RVA=0x09D47C44  token=0x60001A9  System.Void DeleteAssetCacheData(System.Boolean bRegisterUndo)
  RVA=0x09D49878  token=0x60001AA  System.Void GenerateParameters(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D485E8  token=0x60001AB  System.Void DownloadParameterPresetFromHoudini(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5310C  token=0x60001AC  System.Void UploadParameterPresetToHoudini(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D52514  token=0x60001AD  System.Void UpdateParameterInputsToHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bForceUpdate)
  RVA=0x09D4E5D4  token=0x60001AE  System.Boolean LoadAssetFileWithSubasset(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bPromptForSubasset, System.Int32 desiredSubassetIndex)
  RVA=0x09D4730C  token=0x60001AF  System.Boolean CreateAndCookAsset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 subassetIndex, System.Int32& newAssetID, System.Boolean bCookTemplatedGeos)
  RVA=0x09D47434  token=0x60001B0  System.Void CreateAssetInputs(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D490A0  token=0x60001B1  System.Void ForceUploadParameters(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D52BBC  token=0x60001B2  System.Void UploadCurvesParameters(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bCheckParamsChanged)
  RVA=0x09D52618  token=0x60001B3  System.Void UploadAttributeValues(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D517C0  token=0x60001B4  System.Void SyncDirtyAttributesToHoudini(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D52F3C  token=0x60001B5  System.Void UploadInputNodes(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bForceUpdate, System.Boolean bUpdateAll)
  RVA=0x09D4C46C  token=0x60001B6  System.Boolean HasInputNodeTransformChanged()
  RVA=0x09D4ED78  token=0x60001B7  System.Void NotifyInputNodesCookFinished()
  RVA=0x09D47878  token=0x60001B8  System.Boolean CreateObjects(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D51AD8  token=0x60001B9  System.Void UpdateAllObjectNodes(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D47788  token=0x60001BA  HoudiniEngineUnity.HEU_ObjectNode CreateObjectNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo& objectInfo, HoudiniEngineUnity.HAPI_Transform& objectTranform)
  RVA=0x09D49740  token=0x60001BB  System.Void GenerateObjectsGeometry(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bRebuild)
  RVA=0x09D4EEC4  token=0x60001BC  System.Int32 NumAttributeStores()
  RVA=0x09D4A310  token=0x60001BD  HoudiniEngineUnity.HEU_AttributesStore GetAttributeStore(System.String geoName, System.Int32 partID)
  RVA=0x09D490D4  token=0x60001BE  System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D493C4  token=0x60001BF  System.Void GenerateInstances(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D491FC  token=0x60001C0  System.Void GenerateHandles(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D45254  token=0x60001C1  System.Void CleanUpHandles()
  RVA=0x09D4A848  token=0x60001C2  HoudiniEngineUnity.HEU_Handle GetHandleByName(System.String handleName)
  RVA=0x04D8D440  token=0x60001C3  System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> GetHandles()
  RVA=0x09D4EF08  token=0x60001C4  System.Int32 NumHandles()
  RVA=0x09D4B938  token=0x60001C5  HoudiniEngineUnity.HAPI_Transform GetObjectTransform(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 objectID)
  RVA=0x09D4B9F4  token=0x60001C6  HoudiniEngineUnity.HEU_ObjectNode GetObjectWithID(System.Int32 objId)
  RVA=0x09D4D208  token=0x60001C7  System.Void InvokeBakedEvent(System.Boolean bSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputObjects, System.Boolean isNewBake)
  RVA=0x09D45C58  token=0x60001C8  UnityEngine.GameObject CloneAssetWithoutHDA(System.String& bakedAssetPath, System.Boolean bWriteMeshesToAssetDatabase, System.Boolean bReconnectPrefabInstances)
  RVA=0x09D4EE90  token=0x60001C9  System.Void NotifyUpstreamCooked(HoudiniEngineUnity.HEU_CookedEventData Data)
  RVA=0x09D4618C  token=0x60001CA  System.Void ConnectToUpstream(HoudiniEngineUnity.HEU_HoudiniAsset upstreamAsset)
  RVA=0x09D47DC0  token=0x60001CB  System.Void DisconnectFromUpstream(HoudiniEngineUnity.HEU_HoudiniAsset upstreamAsset)
  RVA=0x09D43038  token=0x60001CC  System.Void AddDownstreamConnection(UnityEngine.Events.UnityAction<HoudiniEngineUnity.HEU_CookedEventData> receiver)
  RVA=0x09D4FDF4  token=0x60001CD  System.Void RemoveDownstreamConnection(UnityEngine.Events.UnityAction<HoudiniEngineUnity.HEU_CookedEventData> receiver)
  RVA=0x09D4557C  token=0x60001CE  System.Void ClearAllUpstreamConnections()
  RVA=0x09D523EC  token=0x60001CF  System.Void UpdateInputsOnAssetRecreation(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D4F89C  token=0x60001D0  System.Void ReconnectInputsUpstreamNotifications()
  RVA=0x09D4A984  token=0x60001D1  System.Void GetHoudiniTransformAndApply(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D532F0  token=0x60001D2  System.Void UploadUnityTransform(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bOnlySendIfChangedFromLastSync)
  RVA=0x09D5220C  token=0x60001D3  System.Void UpdateHoudiniMaterials(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D4FFBC  token=0x60001D4  System.Void RemoveUnusedMaterials()
  RVA=0x09D4D45C  token=0x60001D5  System.Boolean IsAssetValidInHoudini(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D4C5BC  token=0x60001D6  System.Boolean HasTransformChangedSinceLastUpdate()
  RVA=0x09D4A470  token=0x60001D7  System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts)
  RVA=0x09D45800  token=0x60001D8  System.Void ClearInvalidCurves()
  RVA=0x09D4A710  token=0x60001D9  System.Int32 GetEditableCurveCount()
  RVA=0x09D42FB4  token=0x60001DA  System.Void AddCurve(HoudiniEngineUnity.HEU_Curve curve)
  RVA=0x09D4FD9C  token=0x60001DB  System.Void RemoveCurve(HoudiniEngineUnity.HEU_Curve curve)
  RVA=0x04D8D870  token=0x60001DC  System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> GetInputNodes()
  RVA=0x09D430B8  token=0x60001DD  System.Void AddInputNode(HoudiniEngineUnity.HEU_InputNode node)
  RVA=0x09D4FE4C  token=0x60001DE  System.Void RemoveInputNode(HoudiniEngineUnity.HEU_InputNode node)
  RVA=0x09D4C990  token=0x60001DF  System.Void InputNodeNotifyRemoved(HoudiniEngineUnity.HEU_InputNode node)
  RVA=0x09D431A8  token=0x60001E0  System.Void AddVolumeCache(HoudiniEngineUnity.HEU_VolumeCache cache)
  RVA=0x09D50408  token=0x60001E1  System.Void RemoveVolumeCache(HoudiniEngineUnity.HEU_VolumeCache cache)
  RVA=0x09D42DD8  token=0x60001E2  System.Void AddAttributeStore(HoudiniEngineUnity.HEU_AttributesStore attributeStore)
  RVA=0x09D4FCE8  token=0x60001E3  System.Void RemoveAttributeStore(HoudiniEngineUnity.HEU_AttributesStore attributeStore)
  RVA=0x09D5048C  token=0x60001E4  System.Void ReorderAttributeStore(System.Int32 oldIndex, System.Int32 newIndex)
  RVA=0x09D4EF80  token=0x60001E5  System.Void PopulateObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> objInstanceInfos)
  RVA=0x09D4313C  token=0x60001E6  System.Void AddToAssetDBCache(System.String assetObjectFileName, UnityEngine.Object objectToAdd, System.String relativeFolderPath, UnityEngine.Object& targetAssetDBObject)
  RVA=0x09D50DA0  token=0x60001E7  System.Void SetCurvesVisibilityInScene(System.Boolean bShow)
  RVA=0x09D4C1EC  token=0x60001E8  System.String GetValidAssetCacheFolderPath()
  RVA=0x09D450C4  token=0x60001E9  System.Void CalculateVisibility()
  RVA=0x09D44FAC  token=0x60001EA  System.Void CalculateColliderState()
  RVA=0x09D4E8B0  token=0x60001EB  System.Void LoadAssetPresetAndCook(HoudiniEngineUnity.HEU_AssetPreset assetPreset)
  RVA=0x09D43544  token=0x60001EC  System.Void ApplyRecookPreset()
  RVA=0x09D4322C  token=0x60001ED  System.Boolean ApplyInputPresets(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputPreset> inputPresets, System.Boolean bAddMissingInputsToRecookPreset)
  RVA=0x09D4C2C8  token=0x60001EE  HoudiniEngineUnity.HEU_VolumeCachePreset GetVolumeCachePreset(System.String objName, System.String geoName, System.Int32 tile)
  RVA=0x09D503A4  token=0x60001EF  System.Void RemoveVolumeCachePreset(HoudiniEngineUnity.HEU_VolumeCachePreset preset)
  RVA=0x09D435E4  token=0x60001F0  System.Boolean ApplyVolumeCachePresets(System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCachePreset> volumeCachePresets)
  RVA=0x09D51908  token=0x60001F1  System.Void SyncInternalParametersForUndoCompare()
  RVA=0x09D525AC  token=0x60001F2  System.Boolean UpdateSessionSync()
  RVA=0x09D52604  token=0x60001F3  System.Void UpdateTotalCookCount()
  RVA=0x09D505A4  token=0x60001F4  System.Void ResetAndCopyInstantiatedProperties(HoudiniEngineUnity.HEU_HoudiniAsset newAsset)
  RVA=0x09D4AE7C  token=0x60001F5  HoudiniEngineUnity.HEU_HoudiniAsset.AssetInstantiationMethod GetInstantiationMethod()
  RVA=0x09D4ADF0  token=0x60001F6  HoudiniEngineUnity.HEU_HoudiniAsset GetInstantiatedObject()
  RVA=0x09D45928  token=0x60001F7  System.Void ClearInvalidLists()
  RVA=0x09D4621C  token=0x60001F8  System.Void CopyPropertiesTo(HoudiniEngineUnity.HEU_HoudiniAsset newAsset)
  RVA=0x09D50F70  token=0x60001F9  System.Void SetSoftDeleted()
  RVA=0x09D516DC  token=0x60001FA  System.Void SyncChildTransforms()
  RVA=0x09D439BC  token=0x60001FB  HoudiniEngineUnity.HEU_AssetCookStatusWrapper AssetCookStatus_InternalToWrappper(HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus assetCookStatus)
  RVA=0x09D439BC  token=0x60001FC  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookStatus AssetCookStatus_WrapperToInternal(HoudiniEngineUnity.HEU_AssetCookStatusWrapper assetCookStatus)
  RVA=0x09D4399C  token=0x60001FD  HoudiniEngineUnity.HEU_AssetCookResultWrapper AssetCookResult_InternalToWrapper(HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult assetCookResult)
  RVA=0x09D4399C  token=0x60001FE  HoudiniEngineUnity.HEU_HoudiniAsset.AssetCookResult AssetCookResult_WrapperToInternal(HoudiniEngineUnity.HEU_AssetCookResultWrapper assetCookResult)
  RVA=0x09D4798C  token=0x60001FF  HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper CurveDrawCollision_InternalToWrapper(HoudiniEngineUnity.HEU_Curve.CurveDrawCollision curveDrawCollision)
  RVA=0x09D479A4  token=0x6000200  HoudiniEngineUnity.HEU_Curve.CurveDrawCollision CurveDrawCollision_WrapperToInternal(HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper curveDrawCollision)
  RVA=0x09D43A04  token=0x6000201  HoudiniEngineUnity.HEU_AssetTypeWrapper AssetType_InternalToWrapper(HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType assetType)
  RVA=0x09D43A04  token=0x6000202  HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType AssetType_WrapperToInternal(HoudiniEngineUnity.HEU_AssetTypeWrapper assetType)
  RVA=0x09D4D4CC  token=0x6000203  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_HoudiniAsset asset)
  RVA=0x09D5335C  token=0x6000204  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HoudiniAssetRoot
TYPE:  class
TOKEN: 0x2000039
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           HoudiniEngineUnity.HEU_HoudiniAsset_houdiniAsset  // 0x18
  private           System.Collections.Generic.List<UnityEngine.GameObject>_bakeTargets  // 0x20
PROPERTIES:
  HoudiniAsset  get=0x0385B100
  BakeTargets  get=0x04D862C0
METHODS:
  RVA=0x09D58C04  token=0x6000219  System.Void OnDestroy()
  RVA=0x09D58CA8  token=0x600021A  System.Void RemoveHoudiniEngineAssetData()
  RVA=0x09D58BA0  token=0x600021B  System.Void ClearHoudiniEngineReferences()
  RVA=0x09D58BF4  token=0x600021C  System.Void DestroyRootComponent(HoudiniEngineUnity.HEU_HoudiniAssetRoot assetRoot)
  RVA=0x09D58D54  token=0x600021D  System.Void Reset()
  RVA=0x09D58E10  token=0x600021E  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputNode
TYPE:  class
TOKEN: 0x200003A
SIZE:  0xB0
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_InputNode HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           HoudiniEngineUnity.HEU_InputNode.InputNodeType_inputNodeType  // 0x18
  private           HoudiniEngineUnity.HEU_InputNode.InputObjectType_inputObjectType  // 0x1c
  private           HoudiniEngineUnity.HEU_InputNode.InputObjectType_pendingInputObjectType  // 0x20
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputObjectInfo>_inputObjects  // 0x28
  private           System.Collections.Generic.List<System.Int32>_inputObjectsConnectedAssetIDs  // 0x30
  private           UnityEngine.GameObject          _inputAsset  // 0x38
  private           UnityEngine.GameObject          _connectedInputAsset  // 0x40
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputHDAInfo>_inputAssetInfos  // 0x48
  private           System.Int32                    _nodeID  // 0x50
  private           System.Int32                    _inputIndex  // 0x54
  private           System.Boolean                  _requiresCook  // 0x58
  private           System.Boolean                  _requiresUpload  // 0x59
  private           System.String                   _inputName  // 0x60
  private           System.String                   _labelName  // 0x68
  private           System.String                   _paramName  // 0x70
  private           System.Int32                    _connectedNodeID  // 0x78
  private           System.Boolean                  _keepWorldTransform  // 0x7c
  private           System.Boolean                  _packGeometryBeforeMerging  // 0x7d
  private           HoudiniEngineUnity.HEU_HoudiniAsset_parentAsset  // 0x80
  private           HoudiniEngineUnity.HEU_InputInterfaceMeshSettings_meshSettings  // 0x88
  private           HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings_tilemapSettings  // 0x90
  private           HoudiniEngineUnity.HEU_InputInterfaceSplineSettings_splineSettings  // 0x98
  private           System.Boolean                  _usingSelectFromHierarchy  // 0xa0
  public            HoudiniEngineUnity.HEU_InputNodeUICache_uiCache  // 0xa8
PROPERTIES:
  ParentAsset  get=0x04D86260
  KeepWorldTransform  get=0x04D92E50  set=0x04D92E60
  PackGeometryBeforeMerging  get=0x04DA26A0  set=0x04DA26B0
  NodeType  get=0x09D5DBC0
  ObjectType  get=0x09D5DBE0
  PendingObjectType  get=0x09D5DBEC  set=0x09D5DBF8
  InputNodeID  get=0x04D86550
  InputName  get=0x04D86280
  LabelName  get=0x04D86230
  ParamName  get=0x04D862B0
  MeshSettings  get=0x04D861F0
  TilemapSettings  get=0x04D86220
  SplineSettings  get=0x04D85EE0
  InputType  get=0x011EF5B0
  InputObjects  get=0x04D86240
  InputAssetInfos  get=0x04D86200
  RequiresCook  get=0x04D86F70  set=0x04D86F80
  RequiresUpload  get=0x04D8EF10  set=0x04D8EF20
METHODS:
  RVA=0x09D5A3AC  token=0x6000236  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=0x09D5C1F8  token=0x6000237  System.Void Recook()
  RVA=0x09D5ADF4  token=0x6000238  System.Boolean IsAssetInput()
  RVA=0x09D5B844  token=0x6000239  System.Int32 NumInputEntries()
  RVA=0x09D59FFC  token=0x600023A  UnityEngine.GameObject GetInputEntryGameObject(System.Int32 index)
  RVA=0x09D5A210  token=0x600023B  UnityEngine.GameObject[] GetInputEntryGameObjects()
  RVA=0x09D5C9BC  token=0x600023C  System.Void SetInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset)
  RVA=0x09D5AAC0  token=0x600023D  System.Void InsertInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset)
  RVA=0x09D59070  token=0x600023E  System.Void AddInputEntryAtEnd(UnityEngine.GameObject newEntryGameObject, System.Boolean bRecookAsset)
  RVA=0x09D5C544  token=0x600023F  System.Void ResetInputNode(System.Boolean bRecookAsset)
  RVA=0x09D59250  token=0x6000240  System.Void ChangeInputType(HoudiniEngineUnity.HEU_InputObjectTypeWrapper newType, System.Boolean bRecookAsset)
  RVA=0x09D5C2CC  token=0x6000241  System.Void RemoveInputEntry(System.Int32 index, System.Boolean bRecookAsset)
  RVA=0x09D5C264  token=0x6000242  System.Void RemoveAllInputEntries(System.Boolean bRecookAsset)
  RVA=0x09D5C904  token=0x6000243  System.Void SetInputEntryObjectUseTransformOffset(System.Int32 index, System.Boolean value, System.Boolean bRecookAsset)
  RVA=0x09D5C840  token=0x6000244  System.Void SetInputEntryObjectTransformTranslateOffset(System.Int32 index, UnityEngine.Vector3 translateOffset, System.Boolean bRecookAsset)
  RVA=0x09D5C6B0  token=0x6000245  System.Void SetInputEntryObjectTransformRotateOffset(System.Int32 index, UnityEngine.Vector3 rotateOffset, System.Boolean bRecookAsset)
  RVA=0x09D5C778  token=0x6000246  System.Void SetInputEntryObjectTransformScaleOffset(System.Int32 index, UnityEngine.Vector3 scaleOffset, System.Boolean bRecookAsset)
  RVA=0x09D59110  token=0x6000247  System.Boolean AreAnyInputHDAsConnected()
  RVA=0x09D59ECC  token=0x6000248  System.Int32 GetConnectedInputCount()
  RVA=0x09D59F40  token=0x6000249  System.Int32 GetConnectedNodeID(System.Int32 index)
  RVA=0x09D5B1E0  token=0x600024A  System.Void LoadPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset)
  RVA=0x09D5B8B8  token=0x600024B  System.Void PopulateInputPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset, System.Boolean sceneRelativeGameObjects)
  RVA=0x09D599F4  token=0x600024C  HoudiniEngineUnity.HEU_InputNode CreateSetupInput(System.Int32 nodeID, System.Int32 inputIndex, System.String inputName, System.String labelName, HoudiniEngineUnity.HEU_InputNode.InputNodeType inputNodeType, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x04D865A0  token=0x600024D  System.Void SetInputNodeID(System.Int32 nodeID)
  RVA=0x09D59AA8  token=0x600024E  System.Void DestroyAllData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5C5D4  token=0x600024F  System.Void ResetInputObjectTransforms()
  RVA=0x09D5C588  token=0x6000250  System.Void ResetInputNode(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D59034  token=0x6000251  HoudiniEngineUnity.HEU_InputObjectInfo AddInputEntryAtEndMesh(UnityEngine.GameObject newEntryGameObject)
  RVA=0x09D58FF8  token=0x6000252  HoudiniEngineUnity.HEU_InputHDAInfo AddInputEntryAtEndHDA(UnityEngine.GameObject newEntryGameObject)
  RVA=0x09D592B8  token=0x6000253  System.Void ChangeInputType(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputNode.InputObjectType newType)
  RVA=0x09D5C4F4  token=0x6000254  System.Void ResetConnectionForForceUpdate(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5D008  token=0x6000255  System.Void UploadInput(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5CD84  token=0x6000256  System.Void UploadHDAInput(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5D4C0  token=0x6000257  System.Void UploadUnityInput(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5BFCC  token=0x6000258  System.Void ReconnectToUpstreamAsset()
  RVA=0x09D59980  token=0x6000259  HoudiniEngineUnity.HEU_InputObjectInfo CreateInputObjectInfo(UnityEngine.GameObject inputGameObject)
  RVA=0x09D59918  token=0x600025A  HoudiniEngineUnity.HEU_InputHDAInfo CreateInputHDAInfo(UnityEngine.GameObject inputGameObject)
  RVA=0x09D5AD88  token=0x600025B  HoudiniEngineUnity.HEU_InputObjectInfo InternalAddInputObjectAtEnd(UnityEngine.GameObject newInputGameObject)
  RVA=0x09D5AD1C  token=0x600025C  HoudiniEngineUnity.HEU_InputHDAInfo InternalAddInputHDAAtEnd(UnityEngine.GameObject newInputHDA)
  RVA=0x09D59C7C  token=0x600025D  System.Void DisconnectConnectedMergeNode(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D592E8  token=0x600025E  System.Void ClearConnectedInputHDAs()
  RVA=0x09D594CC  token=0x600025F  System.Void ConnectToMergeObject(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D59AEC  token=0x6000260  System.Void DisconnectAndDestroyInputs(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5D434  token=0x6000261  System.Boolean UploadObjectMergeTransformType(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5D3A8  token=0x6000262  System.Boolean UploadObjectMergePackGeometry(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5A604  token=0x6000263  System.Boolean HasInputNodeTransformChanged()
  RVA=0x09D5CEAC  token=0x6000264  System.Void UploadInputObjectTransforms(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5CC18  token=0x6000265  System.Void UpdateOnAssetRecreation(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D59570  token=0x6000266  System.Void CopyInputValuesTo(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputNode destInputNode)
  RVA=0x09D5B218  token=0x6000267  System.Void LoadPreset(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputPreset inputPreset)
  RVA=0x09D59DB0  token=0x6000268  System.Boolean FindAddToInputHDA(System.String gameObjectName)
  RVA=0x09D5B7CC  token=0x6000269  System.Void NotifyParentRemovedInput()
  RVA=0x09D594B4  token=0x600026A  System.Void ClearUICache()
  RVA=0x09D5A524  token=0x600026B  System.Void HandleSelectedObjectsForInputObjects(UnityEngine.GameObject[] selectedObjects)
  RVA=0x09D5A444  token=0x600026C  System.Void HandleSelectedObjectsForInputHDAs(UnityEngine.GameObject[] selectedObjects)
  RVA=0x09D5ADFC  token=0x600026D  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InputNode other)
  RVA=0x09D5A368  token=0x600026E  HoudiniEngineUnity.HEU_InputNode.InternalObjectType GetInternalObjectType(HoudiniEngineUnity.HEU_InputNode.InputObjectType type)
  RVA=0x09D4399C  token=0x600026F  HoudiniEngineUnity.HEU_InputNodeTypeWrapper InputNodeType_InternalToWrapper(HoudiniEngineUnity.HEU_InputNode.InputNodeType inputNodeType)
  RVA=0x09D4399C  token=0x6000270  HoudiniEngineUnity.HEU_InputNode.InputNodeType InputNodeType_InternalToWrapper(HoudiniEngineUnity.HEU_InputNodeTypeWrapper inputNodeType)
  RVA=0x09D5AA70  token=0x6000271  HoudiniEngineUnity.HEU_InputObjectTypeWrapper InputObjectType_InternalToWrapper(HoudiniEngineUnity.HEU_InputNode.InputObjectType inputType)
  RVA=0x09D5AA70  token=0x6000272  HoudiniEngineUnity.HEU_InputNode.InputObjectType InputObjectType_WrapperToInternal(HoudiniEngineUnity.HEU_InputObjectTypeWrapper inputType)
  RVA=0x09D5D9F0  token=0x6000273  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputObjectInfo
TYPE:  class
TOKEN: 0x200003F
SIZE:  0xA8
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.GameObject          _gameObject  // 0x10
  public            UnityEngine.Terrain             _terrainReference  // 0x18
  public            HEU_BoundingVolume              _boundingVolumeReference  // 0x20
  public            UnityEngine.Tilemaps.Tilemap    _tilemapReference  // 0x28
  public            UnityEngine.Matrix4x4           _syncdTransform  // 0x30
  public            System.Collections.Generic.List<UnityEngine.Matrix4x4>_syncdChildTransforms  // 0x70
  public            System.Boolean                  _useTransformOffset  // 0x78
  public            UnityEngine.Vector3             _translateOffset  // 0x7c
  public            UnityEngine.Vector3             _rotateOffset  // 0x88
  public            UnityEngine.Vector3             _scaleOffset  // 0x94
  public            System.Type                     _inputInterfaceType  // 0xa0
METHODS:
  RVA=0x09D5DC18  token=0x6000274  System.Void CopyTo(HoudiniEngineUnity.HEU_InputObjectInfo destObject)
  RVA=0x09D5DF78  token=0x6000275  System.Void SetReferencesFromGameObject()
  RVA=0x09D5DD1C  token=0x6000276  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InputObjectInfo other)
  RVA=0x09D5E060  token=0x6000277  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputHDAInfo
TYPE:  class
TOKEN: 0x2000040
SIZE:  0x28
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.GameObject          _pendingGO  // 0x10
  public            UnityEngine.GameObject          _connectedGO  // 0x18
  public            System.Int32                    _connectedInputNodeID  // 0x20
  public            System.Int32                    _connectedMergeNodeID  // 0x24
METHODS:
  RVA=0x09D58E90  token=0x6000278  System.Void CopyTo(HoudiniEngineUnity.HEU_InputHDAInfo destInfo)
  RVA=0x09D58ED4  token=0x6000279  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InputHDAInfo other)
  RVA=0x04DA2690  token=0x600027A  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputNodeUICache
TYPE:  class
TOKEN: 0x2000041
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNodeUICache.HEU_InputObjectUICache>_inputObjectCache  // 0x10
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNodeUICache.HEU_InputAssetUICache>_inputAssetCache  // 0x18
METHODS:
  RVA=0x09D58F38  token=0x600027B  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InstanceInputUIState
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.Boolean                  _showInstanceInputs  // 0x18
  public            System.Int32                    _numInputsToShowUI  // 0x1c
  public            System.Int32                    _inputsPageIndexUI  // 0x20
METHODS:
  RVA=0x09D5E160  token=0x600027E  System.Void CopyTo(HoudiniEngineUnity.HEU_InstanceInputUIState dest)
  RVA=0x09D5E188  token=0x600027F  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InstanceInputUIState other)
  RVA=0x09D5E2E0  token=0x6000280  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ObjectInstanceInfo
TYPE:  class
TOKEN: 0x2000045
SIZE:  0x40
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_InstancedInput>_instancedInputs  // 0x18
  public            HoudiniEngineUnity.HEU_PartData _partTarget  // 0x20
  public            System.Int32                    _instancedObjectNodeID  // 0x28
  public            System.String                   _instancedObjectPath  // 0x30
  public            System.Collections.Generic.List<UnityEngine.GameObject>_instances  // 0x38
METHODS:
  RVA=0x09D5E4AC  token=0x6000281  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ObjectInstanceInfo other)
  RVA=0x09D5E5CC  token=0x6000282  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InstancedInput
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x30
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.GameObject          _instancedGameObject  // 0x10
  public            UnityEngine.Vector3             _rotationOffset  // 0x18
  public            UnityEngine.Vector3             _scaleOffset  // 0x24
METHODS:
  RVA=0x09D5E2F4  token=0x6000283  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_InstancedInput other)
  RVA=0x09D5E480  token=0x6000284  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ObjectNode
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x80
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_ObjectNode HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.String                   _objName  // 0x18
  private           HoudiniEngineUnity.HEU_HoudiniAsset_parentAsset  // 0x20
  private           HoudiniEngineUnity.HAPI_ObjectInfo_objectInfo  // 0x28
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode>_geoNodes  // 0x48
  private           HoudiniEngineUnity.HAPI_Transform_objectTransform  // 0x50
  private           System.Collections.Generic.List<System.Int32>_recentlyDestroyedParts  // 0x78
PROPERTIES:
  ParentAsset  get=0x04D862C0
  ObjectID  get=0x04D86640
  ObjectName  get=0x0385B100
  ObjectInfo  get=0x04DA26E0
  GeoNodes  get=0x04D86200
  ObjectTransform  get=0x04DA2700
METHODS:
  RVA=0x09D607B4  token=0x600028B  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=0x09D61298  token=0x600028C  System.Void Recook()
  RVA=0x04D86A90  token=0x600028D  System.Boolean IsInstanced()
  RVA=0x04D86DC0  token=0x600028E  System.Boolean IsVisible()
  RVA=0x09D60EF8  token=0x600028F  System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData)
  RVA=0x09D60418  token=0x6000290  System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=0x09D60690  token=0x6000291  System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=0x09D602C0  token=0x6000292  HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject)
  RVA=0x09D60180  token=0x6000293  HoudiniEngineUnity.HEU_GeoNode GetGeoNode(System.String geoName)
  RVA=0x09D5FDB4  token=0x6000294  System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly)
  RVA=0x09D6053C  token=0x6000295  System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outGeoNodes)
  RVA=0x09D60848  token=0x6000296  System.Void HideAllGeometry()
  RVA=0x09D5ED20  token=0x6000297  System.Void DisableAllColliders()
  RVA=0x09D60DD4  token=0x6000298  System.Boolean IsInstancer()
  RVA=0x09D616B4  token=0x6000299  System.Void .ctor()
  RVA=0x09D612FC  token=0x600029A  System.Void Reset()
  RVA=0x09D613FC  token=0x600029B  System.Void SyncWithObjectInfo(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D6095C  token=0x600029C  System.Void Initialize(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo objectInfo, HoudiniEngineUnity.HAPI_Transform objectTranform, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bUseOutputNodes, System.Boolean bGetEditableNodes)
  RVA=0x09D5EE34  token=0x600029D  System.Void GatherAllAssetOutputsLegacy(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo objectInfo, System.Boolean bUseOutputNodes, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode>& geoNodes)
  RVA=0x09D5EC48  token=0x600029E  System.Void DestroyAllData(System.Boolean bIsRebuild)
  RVA=0x09D5EB9C  token=0x600029F  HoudiniEngineUnity.HEU_GeoNode CreateGeoNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_GeoInfo geoInfo)
  RVA=0x09D5FEE8  token=0x60002A0  System.Void GetDebugInfo(System.Text.StringBuilder sb)
  RVA=0x04DA26C0  token=0x60002A1  System.Void SetObjectInfo(HoudiniEngineUnity.HAPI_ObjectInfo newObjectInfo)
  RVA=0x09D61528  token=0x60002A2  System.Void UpdateObject(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bForceUpdate)
  RVA=0x09D5F004  token=0x60002A3  System.Void GenerateGeometry(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bRebuild)
  RVA=0x09D5FB4C  token=0x60002A4  System.Void GeneratePartInstances(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5EEE0  token=0x60002A5  System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5E698  token=0x60002A6  System.Void ApplyObjectTransformToGeoNodes()
  RVA=0x09D5FC70  token=0x60002A7  System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts)
  RVA=0x09D5F76C  token=0x60002A8  System.Void GenerateObjectInstances(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5E9EC  token=0x60002A9  System.Void ClearObjectInstances(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D61024  token=0x60002AA  System.Void PopulateObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> objInstanceInfos)
  RVA=0x09D61174  token=0x60002AB  System.Void ProcessUnityScriptAttributes(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D5E8CC  token=0x60002AC  System.Void CalculateVisibility()
  RVA=0x09D5E7B8  token=0x60002AD  System.Void CalculateColliderState()
  RVA=0x09D614CC  token=0x60002AE  System.String ToString()
  RVA=0x09D60B5C  token=0x60002AF  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ObjectNode other)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PartData
TYPE:  class
TOKEN: 0x2000048
SIZE:  0xA0
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_PartData HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.Int32                    _partID  // 0x18
  private           System.String                   _partName  // 0x20
  private           System.Int32                    _objectNodeID  // 0x28
  private           System.Int32                    _geoID  // 0x2c
  private           HoudiniEngineUnity.HAPI_PartType_partType  // 0x30
  private           HoudiniEngineUnity.HEU_GeoNode  _geoNode  // 0x38
  private           System.Boolean                  _isAttribInstancer  // 0x40
  private           System.Boolean                  _isPartInstanced  // 0x41
  private           System.Int32                    _partPointCount  // 0x44
  private           System.Boolean                  _isObjectInstancer  // 0x48
  private           System.Boolean                  _objectInstancesGenerated  // 0x49
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo>_objectInstanceInfos  // 0x50
  private           UnityEngine.Vector3             _terrainOffsetPosition  // 0x58
  private           UnityEngine.Object              _assetDBTerrainData  // 0x68
  private           System.Boolean                  _isPartEditable  // 0x70
  private           HoudiniEngineUnity.HEU_PartData.PartOutputType_partOutputType  // 0x74
  private           HoudiniEngineUnity.HEU_Curve    _curve  // 0x78
  private           HoudiniEngineUnity.HEU_AttributesStore_attributesStore  // 0x80
  private           System.Boolean                  _haveInstancesBeenGenerated  // 0x88
  private           System.Int32                    _meshVertexCount  // 0x8c
  private           HoudiniEngineUnity.HEU_GeneratedOutput_generatedOutput  // 0x90
  private           System.String                   _volumeLayerName  // 0x98
PROPERTIES:
  ParentAsset  get=0x09D68F14
  PartID  get=0x011EF5B0
  PartName  get=0x04D862C0
  GeoID  get=0x04D86710
  PartType  get=0x04D864F0
  ParentGeoNode  get=0x04D85A50
  ObjectInstanceInfos  get=0x04D86270
  Curve  get=0x04D862A0
  MeshVertexCount  get=0x04D90C40
  GeneratedOutput  get=0x04D86220
  OutputGameObject  get=0x09D68EEC
METHODS:
  RVA=0x09D68E68  token=0x60002BB  System.Void .ctor()
  RVA=0x09D67B78  token=0x60002BC  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=0x09D684C8  token=0x60002BD  System.Void Recook()
  RVA=0x09D682C8  token=0x60002BE  System.Boolean IsPartInstancer()
  RVA=0x02D480A0  token=0x60002BF  System.Boolean IsAttribInstancer()
  RVA=0x09D682AC  token=0x60002C0  System.Boolean IsInstancerAnyType()
  RVA=0x04D86F40  token=0x60002C1  System.Boolean IsPartInstanced()
  RVA=0x04D86530  token=0x60002C2  System.Int32 GetPartPointCount()
  RVA=0x04D867D0  token=0x60002C3  System.Boolean IsObjectInstancer()
  RVA=0x09D682D8  token=0x60002C4  System.Boolean IsPartVolume()
  RVA=0x09D682C0  token=0x60002C5  System.Boolean IsPartCurve()
  RVA=0x09D682D0  token=0x60002C6  System.Boolean IsPartMesh()
  RVA=0x04D86A50  token=0x60002C7  System.Boolean IsPartEditable()
  RVA=0x011F2750  token=0x60002C8  System.Boolean HaveInstancesBeenGenerated()
  RVA=0x09D68560  token=0x60002C9  System.Void SetGameObjectName(System.String partName)
  RVA=0x09D68758  token=0x60002CA  System.Void SetGameObject(UnityEngine.GameObject gameObject)
  RVA=0x06352D0C  token=0x60002CB  System.Void SetVolumeLayerName(System.String name)
  RVA=0x04D85EE0  token=0x60002CC  System.String GetVolumeLayerName()
  RVA=0x09D65174  token=0x60002CD  System.Void DestroyAllData(System.Boolean bIsRebuild)
  RVA=0x09D682E0  token=0x60002CE  System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData)
  RVA=0x09D67A6C  token=0x60002CF  System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=0x09D67B10  token=0x60002D0  System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=0x09D67888  token=0x60002D1  HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject inGameObject)
  RVA=0x09D6250C  token=0x60002D2  System.Void CalculateVisibility(System.Boolean bParentVisibility, System.Boolean bParentDisplayGeo)
  RVA=0x09D62638  token=0x60002D3  System.Void ClearInstances()
  RVA=0x09D6763C  token=0x60002D4  HoudiniEngineUnity.HEU_Curve GetCurve(System.Boolean bEditableOnly)
  RVA=0x09D68A50  token=0x60002D5  System.Void SetVisiblity(System.Boolean bVisibility)
  RVA=0x09D6853C  token=0x60002D6  System.Void SetColliderState(System.Boolean bEnabled)
  RVA=0x09D679A8  token=0x60002D7  HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectPath(System.String path)
  RVA=0x09D678F8  token=0x60002D8  HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectID(System.Int32 objNodeID)
  RVA=0x04DA2720  token=0x60002D9  System.Void SetTerrainOffsetPosition(UnityEngine.Vector3 offsetPosition)
  RVA=0x09D688AC  token=0x60002DA  System.Void SetTerrainData(UnityEngine.TerrainData terrainData, System.String exportPathRelative, System.String exportPathUser)
  RVA=0x09D67C1C  token=0x60002DB  System.Void Initialize(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partID, System.Int32 geoID, System.Int32 objectNodeID, HoudiniEngineUnity.HEU_GeoNode geoNode, HoudiniEngineUnity.HAPI_PartInfo& partInfo, HoudiniEngineUnity.HEU_PartData.PartOutputType partOutputType, System.Boolean isEditable, System.Boolean isObjectInstancer, System.Boolean isAttribInstancer)
  RVA=0x09D6177C  token=0x60002DC  System.Void ApplyHAPITransform(HoudiniEngineUnity.HAPI_Transform& hapiTransform)
  RVA=0x09D676C0  token=0x60002DD  System.Void GetDebugInfo(System.Text.StringBuilder sb)
  RVA=0x09D674FC  token=0x60002DE  System.Void GetClonableObjects(System.Collections.Generic.List<UnityEngine.GameObject> clonableObjects)
  RVA=0x09D675A0  token=0x60002DF  System.Void GetClonableParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> clonableParts)
  RVA=0x04D86810  token=0x60002E0  System.Void SetObjectInstancer(System.Boolean bObjectInstancer)
  RVA=0x09D62940  token=0x60002E1  System.Void ClearObjectInstanceInfos()
  RVA=0x09D62720  token=0x60002E2  System.Void ClearInvalidObjectInstanceInfos()
  RVA=0x09D62538  token=0x60002E3  System.Void ClearGeneratedData()
  RVA=0x09D62554  token=0x60002E4  System.Void ClearGeneratedMeshOutput()
  RVA=0x09D625F0  token=0x60002E5  System.Void ClearGeneratedVolumeOutput()
  RVA=0x09D67368  token=0x60002E6  System.Boolean GeneratePartInstances(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D6550C  token=0x60002E7  System.Void GenerateInstancesFromObjectID(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 objectNodeID, System.String[] instancePrefixes, System.String[] instanceMaterialPaths)
  RVA=0x09D65FF0  token=0x60002E8  System.Void GenerateInstancesFromObject(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_ObjectNode sourceObject, System.String[] instancePrefixes, System.String[] instanceMaterialPaths)
  RVA=0x09D65974  token=0x60002E9  System.Void GenerateInstancesFromObjectIds(HoudiniEngineUnity.HEU_SessionBase session, System.String[] instancePrefixes, System.String[] instanceMaterialPaths)
  RVA=0x09D66364  token=0x60002EA  System.Void GenerateInstancesFromUnityAssetPathAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.String unityInstanceAttr)
  RVA=0x09D64C34  token=0x60002EB  System.Void CreateNewInstanceFromObject(UnityEngine.GameObject sourceObject, System.Int32 instanceIndex, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HAPI_Transform& hapiTransform, System.Int32 instancedObjectNodeID, System.String instancedObjectPath, UnityEngine.Vector3 rotationOffset, UnityEngine.Vector3 scaleOffset, System.String[] instancePrefixes, System.String[] instanceMaterialPaths, UnityEngine.GameObject collisionSrcGO, System.Boolean copyParentFlags)
  RVA=0x09D65470  token=0x60002EC  System.Void GenerateAttributesStore(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D62270  token=0x60002ED  System.Void CalculateColliderState()
  RVA=0x09D62E60  token=0x60002EE  System.Void CopyGameObjectComponents(HoudiniEngineUnity.HEU_PartData partData, UnityEngine.GameObject sourceGO, UnityEngine.GameObject targetGO, System.String assetName, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Collections.Generic.List<HoudiniEngineUnity.TransformData> lodTransformValues)
  RVA=0x09D62AB0  token=0x60002EF  System.Void CopyChildGameObjects(HoudiniEngineUnity.HEU_PartData partData, UnityEngine.GameObject sourceGO, UnityEngine.GameObject targetGO, System.String assetName, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Boolean bKeepPreviousTransformValues)
  RVA=0x09D62130  token=0x60002F0  UnityEngine.GameObject BakePartToNewGameObject(UnityEngine.Transform parentTransform, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bReconnectPrefabInstances)
  RVA=0x09D618B4  token=0x60002F1  System.Void BakePartToGameObject(HoudiniEngineUnity.HEU_PartData partData, UnityEngine.GameObject srcGO, UnityEngine.GameObject targetGO, System.String assetName, System.Boolean bIsInstancer, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bReconnectPrefabInstances, System.Boolean bKeepPreviousTransformValues)
  RVA=0x09D61FE4  token=0x60002F2  System.Void BakePartToGameObject(UnityEngine.GameObject targetGO, System.Boolean bDeleteExistingComponents, System.Boolean bDontDeletePersistantResources, System.Boolean bWriteMeshesToAssetDatabase, System.String& bakedAssetPath, System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> sourceToTargetMeshMap, System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> sourceToCopiedMaterials, UnityEngine.Object& assetDBObject, System.String assetObjectFileName, System.Boolean bReconnectPrefabInstances, System.Boolean bKeepPreviousTransformValues)
  RVA=0x09D66F34  token=0x60002F3  System.Boolean GenerateMesh(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bUseLODGroups)
  RVA=0x09D68360  token=0x60002F4  System.Void ProcessCurvePart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 partId)
  RVA=0x09D68CE4  token=0x60002F5  System.Void SyncAttributesStore(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
  RVA=0x09D68C18  token=0x60002F6  System.Void SetupAttributeGeometry(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D652AC  token=0x60002F7  System.Void DestroyAttributesStore()
  RVA=0x09D6830C  token=0x60002F8  System.Void PopulateObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> objInstanceInfos)
  RVA=0x09D68788  token=0x60002F9  System.Void SetObjectInstanceInfos(System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> sourceObjectInstanceInfos)
  RVA=0x04D86270  token=0x60002FA  System.Collections.Generic.List<HoudiniEngineUnity.HEU_ObjectInstanceInfo> GetObjectInstanceInfos()
  RVA=0x09D65028  token=0x60002FB  HoudiniEngineUnity.HEU_ObjectInstanceInfo CreateObjectInstanceInfo(UnityEngine.GameObject instancedObject, System.Int32 instancedObjectNodeID, System.String instancedObjectPath)
  RVA=0x09D61740  token=0x60002FC  System.String AppendBakedCloneName(System.String name)
  RVA=0x09D68E0C  token=0x60002FD  System.String ToString()
  RVA=0x09D653C4  token=0x60002FE  System.Void DestroyParts(System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> parts, System.Boolean bIsRebuild)
  RVA=0x09D65390  token=0x60002FF  System.Void DestroyPart(HoudiniEngineUnity.HEU_PartData part, System.Boolean bIsRebuild)
  RVA=0x09D629F8  token=0x6000300  System.Boolean ComposeUnityInstanceSplitHierarchy(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.Transform parentTransform, System.Int32 numInstances, UnityEngine.Transform[]& instanceToChildTransform)
  RVA=0x09D67DD8  token=0x6000301  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_PartData other)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AttributeData
TYPE:  sealed class
TOKEN: 0x200004E
SIZE:  0x68
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_AttributeInfo_attributeInfo  // 0x10
  public            System.String                   _name  // 0x38
  public            HoudiniEngineUnity.HEU_AttributeData.AttributeType_attributeType  // 0x40
  public            System.Int32[]                  _intValues  // 0x48
  public            System.Single[]                 _floatValues  // 0x50
  public            System.String[]                 _stringValues  // 0x58
  public            HoudiniEngineUnity.HEU_AttributeData.AttributeState_attributeState  // 0x60
METHODS:
  RVA=0x09D556F4  token=0x600030B  System.Boolean IsColorAttribute()
  RVA=0x09D555C0  token=0x600030C  System.Void CopyValuesTo(HoudiniEngineUnity.HEU_AttributeData destAttrData)
  RVA=0x09D5574C  token=0x600030D  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_AttributeData other)
  RVA=0x041E1670  token=0x600030E  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AttributesStore
TYPE:  class
TOKEN: 0x2000051
SIZE:  0x98
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.Int32                    _geoID  // 0x18
  private           System.Int32                    _partID  // 0x1c
  private           System.String                   _geoName  // 0x20
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributeData>_attributeDatas  // 0x28
  private           System.Boolean                  _hasColorAttribute  // 0x30
  private           UnityEngine.Material            _localMaterial  // 0x38
  private           UnityEngine.Transform           _outputTransform  // 0x40
  private           UnityEngine.Vector3[]           _positionAttributeValues  // 0x48
  private           System.Int32[]                  _vertexIndices  // 0x50
  private           UnityEngine.GameObject          _outputGameObject  // 0x58
  private           UnityEngine.Mesh                _outputMesh  // 0x60
  private           UnityEngine.Material[]          _outputMaterials  // 0x68
  private           UnityEngine.MeshCollider        _outputCollider  // 0x70
  private           UnityEngine.Mesh                _outputColliderMesh  // 0x78
  private           UnityEngine.MeshCollider        _outputMeshCollider  // 0x80
  private           UnityEngine.MeshCollider        _localMeshCollider  // 0x88
  private           System.Boolean                  _outputMeshRendererInitiallyEnabled  // 0x90
  private           System.Boolean                  _outputMeshColliderInitiallyEnabled  // 0x91
PROPERTIES:
  GeoID  get=0x011EF5B0
  PartID  get=0x04D86310
  GeoName  get=0x04D862C0
  OutputTransform  get=0x04D85A60
  OutputMesh  get=0x04D86280
METHODS:
  RVA=0x04D865F0  token=0x6000312  System.Boolean HasColorAttribute()
  RVA=0x09D55F3C  token=0x6000315  System.Void DestroyAllData(HoudiniEngineUnity.HEU_HoudiniAsset asset)
  RVA=0x09D58494  token=0x6000316  System.Void SyncAllAttributesFrom(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Int32 geoID, HoudiniEngineUnity.HAPI_PartInfo& partInfo, UnityEngine.GameObject outputGameObject)
  RVA=0x09D57E40  token=0x6000317  System.Void SetupMeshAndMaterials(HoudiniEngineUnity.HEU_HoudiniAsset asset, HoudiniEngineUnity.HAPI_PartType partType, UnityEngine.GameObject outputGameObject)
  RVA=0x09D56A30  token=0x6000318  System.Boolean HasDirtyAttributes()
  RVA=0x09D58714  token=0x6000319  System.Void SyncDirtyAttributesToHoudini(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D57320  token=0x600031A  System.Void PopulateAttributeData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_AttributeData attributeData, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo)
  RVA=0x09D56938  token=0x600031B  System.Void GetAttributesList(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributeData> attributesList, HoudiniEngineUnity.HAPI_AttributeOwner ownerType, System.Int32 attributeCount)
  RVA=0x09D588C8  token=0x600031C  System.Void UpdateAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_AttributeData attributeData)
  RVA=0x09D58778  token=0x600031D  System.Void UpdateAttributeList(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_AttributeData> attributeDataList)
  RVA=0x09D579F8  token=0x600031E  System.Void RefreshUpstreamInputs(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D58A24  token=0x600031F  System.Boolean UploadAttributeViaMeshInput(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09D57C10  token=0x6000320  System.Void SetAttributeDataSyncd(HoudiniEngineUnity.HEU_AttributeData attributeData)
  RVA=0x09D57BF4  token=0x6000321  System.Void SetAttributeDataDirty(HoudiniEngineUnity.HEU_AttributeData attributeData)
  RVA=0x09D55E84  token=0x6000322  HoudiniEngineUnity.HEU_AttributeData CreateAttribute(System.String attributeName, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo)
  RVA=0x09D563C4  token=0x6000323  HoudiniEngineUnity.HEU_AttributeData GetAttributeData(System.String name)
  RVA=0x09D56504  token=0x6000324  HoudiniEngineUnity.HEU_AttributeData GetAttributeData(System.Int32 index)
  RVA=0x09D56574  token=0x6000325  System.Collections.Generic.List<System.String> GetAttributeNames()
  RVA=0x09D56168  token=0x6000326  System.Void EnablePaintCollider()
  RVA=0x09D56050  token=0x6000327  System.Void DisablePaintCollider()
  RVA=0x09D58208  token=0x6000328  System.Void ShowPaintMesh()
  RVA=0x09D56BA4  token=0x6000329  System.Void HidePaintMesh()
  RVA=0x09D56B54  token=0x600032A  System.Boolean HasMeshForPainting()
  RVA=0x09D569A8  token=0x600032B  UnityEngine.MeshCollider GetPaintMeshCollider()
  RVA=0x09D572BC  token=0x600032C  System.Void PaintAttribute(HoudiniEngineUnity.HEU_AttributeData attributeData, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 attributeIndex, System.Single paintFactor, HoudiniEngineUnity.HEU_AttributesStore.SetAttributeValueFunc setAttrFunc)
  RVA=0x09D57CA0  token=0x600032D  System.Void SetAttributeEditValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 startIndex, System.Int32[] values)
  RVA=0x09D57C2C  token=0x600032E  System.Void SetAttributeEditValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 startIndex, System.Single[] values)
  RVA=0x09D57D14  token=0x600032F  System.Void SetAttributeEditValueString(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 startIndex, System.String[] values)
  RVA=0x09D57AE4  token=0x6000330  System.Void ReplaceAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D55B08  token=0x6000331  System.Void AddAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D583C8  token=0x6000332  System.Void SubtractAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D571AC  token=0x6000333  System.Void MultiplyAttributeValueInt(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D57A30  token=0x6000334  System.Void ReplaceAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D55A58  token=0x6000335  System.Void AddAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D58310  token=0x6000336  System.Void SubtractAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D570EC  token=0x6000337  System.Void MultiplyAttributeValueFloat(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D57D9C  token=0x6000338  System.Void SetAttributeValueString(HoudiniEngineUnity.HEU_AttributeData attributeData, System.Int32 targetIndex, HoudiniEngineUnity.HEU_ToolsInfo sourceTools, System.Int32 sourceIndex, System.Single factor)
  RVA=0x09D56310  token=0x6000339  System.Void FillAttribute(HoudiniEngineUnity.HEU_AttributeData attributeData, HoudiniEngineUnity.HEU_ToolsInfo sourceTools)
  RVA=0x09D55BD4  token=0x600033A  System.Boolean AreAttributesDirty()
  RVA=0x09D56A10  token=0x600033B  System.Void GetPositionAttributeValues(UnityEngine.Vector3[]& positionArray)
  RVA=0x09D56A20  token=0x600033C  System.Void GetVertexIndices(System.Int32[]& indices)
  RVA=0x09D56708  token=0x600033D  HoudiniEngineUnity.HEU_AttributesStore.SetAttributeValueFunc GetAttributeSetValueFunction(HoudiniEngineUnity.HEU_AttributeData.AttributeType attrType, HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode paintMergeMode)
  RVA=0x09D55CEC  token=0x600033E  System.Void CopyAttributeValuesTo(HoudiniEngineUnity.HEU_AttributesStore destAttrStore)
  RVA=0x09D570D4  token=0x600033F  System.Boolean IsValidStore(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D56C7C  token=0x6000340  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_AttributesStore other)
  RVA=0x09D58AD0  token=0x6000341  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_OutputAttribute
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x40
FIELDS:
  public            System.String                   _name  // 0x10
  public            HoudiniEngineUnity.HAPI_AttributeOwner_class  // 0x18
  public            HoudiniEngineUnity.HAPI_StorageType_type  // 0x1c
  public            System.Int32                    _count  // 0x20
  public            System.Int32                    _tupleSize  // 0x24
  public            System.Int32[]                  _intValues  // 0x28
  public            System.Single[]                 _floatValues  // 0x30
  public            System.String[]                 _stringValues  // 0x38
METHODS:
  RVA=0x041E1670  token=0x6000346  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_OutputAttributeDictionary
TYPE:  class
TOKEN: 0x2000054
SIZE:  0x28
EXTENDS: HoudiniEngineUnity.HEU_SerializableDictionary`2
FIELDS:
METHODS:
  RVA=0x09D6F830  token=0x6000347  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_OutputAttributesStore
TYPE:  class
TOKEN: 0x2000055
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           HoudiniEngineUnity.HEU_OutputAttributeDictionary_attributes  // 0x18
METHODS:
  RVA=0x09D6F908  token=0x6000348  System.Void SetAttribute(HoudiniEngineUnity.HEU_OutputAttribute attribute)
  RVA=0x09D6F8A0  token=0x6000349  HoudiniEngineUnity.HEU_OutputAttribute GetAttribute(System.String name)
  RVA=0x09D6F858  token=0x600034A  System.Void Clear()
  RVA=0x09D6F9D8  token=0x600034B  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetDatabase
TYPE:  static class
TOKEN: 0x2000056
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D6A53C  token=0x600034C  System.String GetAssetCachePath()
  RVA=0x09D6AD18  token=0x600034D  System.String GetUnityProjectPath()
  RVA=0x09D6A6D0  token=0x600034E  System.String GetAssetRelativePath(System.String inFullPath)
  RVA=0x09D6A9A0  token=0x600034F  System.String GetPackagesRelativePath(System.String inFullPath)
  RVA=0x09D6AD74  token=0x6000350  System.String GetValidAssetPath(System.String inPath)
  RVA=0x09D6A640  token=0x6000351  System.String GetAssetPath(UnityEngine.Object asset)
  RVA=0x09D6A608  token=0x6000352  System.String GetAssetPathWithSubAssetSupport(UnityEngine.Object asset)
  RVA=0x09D6AAC0  token=0x6000353  System.Void GetSubAssetPathFromPath(System.String fullPath, System.String& mainPath, System.String& subPath)
  RVA=0x09D6A678  token=0x6000354  System.String GetAssetRelativePathStart()
  RVA=0x09D6A948  token=0x6000355  System.String GetPackagesRelativePathStart()
  RVA=0x09D6A584  token=0x6000356  System.String GetAssetFullPath(System.String inPath)
  RVA=0x09D6B2F4  token=0x6000357  System.Boolean IsPathRelativeToAssets(System.String inPath)
  RVA=0x09D6B378  token=0x6000358  System.Boolean IsPathRelativeToPackages(System.String inPath)
  RVA=0x09D6A810  token=0x6000359  System.String GetAssetRootPath(UnityEngine.Object asset)
  RVA=0x09D6ACE0  token=0x600035A  System.String GetUniqueAssetPath(System.String path)
  RVA=0x09D6A5D0  token=0x600035B  System.String GetAssetOrScenePath(UnityEngine.Object inputObject)
  RVA=0x09D6B1E8  token=0x600035C  System.Boolean IsPathInAssetCache(System.String path)
  RVA=0x09D6B178  token=0x600035D  System.Boolean IsPathInAssetCacheBakedFolder(System.String path)
  RVA=0x09D6B1B0  token=0x600035E  System.Boolean IsPathInAssetCacheWorkingFolder(System.String path)
  RVA=0x09D6B0D0  token=0x600035F  System.Boolean IsAssetInAssetCacheBakedFolder(UnityEngine.Object asset)
  RVA=0x09D6B108  token=0x6000360  System.Boolean IsAssetInAssetCacheWorkingFolder(UnityEngine.Object asset)
  RVA=0x09D6A2F0  token=0x6000361  System.String CreateAssetCacheFolder(System.String suggestedAssetPath, System.Int32 hash)
  RVA=0x09D6A430  token=0x6000362  System.Void DeleteAssetCacheFolder(System.String assetCacheFolderPath)
  RVA=0x09D6A498  token=0x6000363  System.Void DeleteAsset(UnityEngine.Object asset)
  RVA=0x09D6A3FC  token=0x6000364  System.Void DeleteAssetAtPath(System.String path)
  RVA=0x09D6A464  token=0x6000365  System.Void DeleteAssetIfInBakedFolder(UnityEngine.Object asset)
  RVA=0x09D6A134  token=0x6000366  System.Boolean ContainsAsset(UnityEngine.Object assetObject)
  RVA=0x09D6A24C  token=0x6000367  System.Boolean CopyAsset(System.String path, System.String newPath)
  RVA=0x09D6A214  token=0x6000368  UnityEngine.Object CopyAndLoadAssetWithRelativePath(UnityEngine.Object srcAsset, System.String copyAssetFolder, System.String relativePath, System.Type type, System.Boolean bOverwriteExisting)
  RVA=0x09D6A1DC  token=0x6000369  UnityEngine.Object CopyAndLoadAssetFromAssetCachePath(UnityEngine.Object srcAsset, System.String copyPath, System.Type type, System.Boolean bOverwriteExisting)
  RVA=0x09D6A16C  token=0x600036A  UnityEngine.Object CopyAndLoadAssetAtAnyPath(UnityEngine.Object srcAsset, System.String copyPath, System.Type type, System.Boolean bOverwriteExisting)
  RVA=0x09D6A1A4  token=0x600036B  UnityEngine.Object CopyAndLoadAssetAtGivenPath(UnityEngine.Object srcAsset, System.String targetPath, System.Type type)
  RVA=0x09D6A284  token=0x600036C  UnityEngine.Object CopyUniqueAndLoadAssetAtAnyPath(UnityEngine.Object srcAsset, System.String copyPath, System.Type type)
  RVA=0x09D6A35C  token=0x600036D  System.Void CreateObjectInAssetCacheFolder(UnityEngine.Object objectToCreate, System.String assetCacheRoot, System.String relativeFolderPath, System.String assetFileName, System.Type type, System.Boolean bOverwriteExisting)
  RVA=0x09D6A328  token=0x600036E  System.Void CreateAsset(UnityEngine.Object asset, System.String path)
  RVA=0x09D6A2BC  token=0x600036F  System.Void CreateAddObjectInAssetCacheFolder(System.String assetName, System.String assetObjectFileName, UnityEngine.Object objectToAdd, System.String relativeFolderPath, System.String& exportRootPath, UnityEngine.Object& assetDBObject)
  RVA=0x09D69E3C  token=0x6000370  System.Void AddObjectToAsset(UnityEngine.Object objectToAdd, UnityEngine.Object assetObject)
  RVA=0x041E1670  token=0x6000371  System.Void SaveAndRefreshDatabase()
  RVA=0x041E1670  token=0x6000372  System.Void SaveAssetDatabase()
  RVA=0x041E1670  token=0x6000373  System.Void RefreshAssetDatabase()
  RVA=0x09D6B46C  token=0x6000374  UnityEngine.Object LoadAssetAtPath(System.String assetPath, System.Type type)
  RVA=0x09D6B4A4  token=0x6000375  UnityEngine.Object LoadSubAssetAtPath(System.String mainPath, System.String subAssetPath)
  RVA=0x09D6B434  token=0x6000376  UnityEngine.Object[] LoadAllAssetsAtPath(System.String assetPath)
  RVA=0x09D6B3FC  token=0x6000377  UnityEngine.Object[] LoadAllAssetRepresentationsAtPath(System.String assetPath)
  RVA=0x09D6B09C  token=0x6000378  System.Void ImportAsset(System.String assetPath, HoudiniEngineUnity.HEU_AssetDatabase.HEU_ImportAssetOptions heuOptions)
  RVA=0x09D6A910  token=0x6000379  System.String GetAssetWorkingPath()
  RVA=0x09D6A504  token=0x600037A  System.String GetAssetBakedPath()
  RVA=0x09D6A4CC  token=0x600037B  System.String GetAssetBakedPathWithAssetName(System.String assetName)
  RVA=0x09D6A3C4  token=0x600037C  System.String CreateUniqueBakePath(System.String assetName)
  RVA=0x09D6A390  token=0x600037D  System.Void CreatePathWithFolders(System.String inPath)
  RVA=0x09D69F28  token=0x600037E  System.String AppendMeshesPathToAssetFolder(System.String inAssetCacheFolder)
  RVA=0x09D6A0B8  token=0x600037F  System.String AppendTexturesPathToAssetFolder(System.String inAssetCacheFolder)
  RVA=0x09D69E70  token=0x6000380  System.String AppendMaterialsPathToAssetFolder(System.String inAssetCacheFolder)
  RVA=0x09D6A03C  token=0x6000381  System.String AppendTerrainPathToAssetFolder(System.String inAssetCacheFolder)
  RVA=0x09D6A848  token=0x6000382  System.String[] GetAssetSubFolders()
  RVA=0x09D69FA4  token=0x6000383  System.String AppendPrefabPath(System.String inAssetCacheFolder, System.String assetName)
  RVA=0x09D69EEC  token=0x6000384  System.String AppendMeshesAssetFileName(System.String assetName)
  RVA=0x012081B0  token=0x6000385  System.Boolean IsSubAsset(UnityEngine.Object obj)
  RVA=0x011EC580  token=0x6000386  System.String[] GetAssetPathsFromAssetBundle(System.String assetBundleFileName)
  RVA=0x09D6B140  token=0x6000387  System.Boolean IsAssetSavedInScene(UnityEngine.GameObject go)
  RVA=0x041E1670  token=0x6000388  System.Void SelectAssetAtPath(System.String path)
  RVA=0x041E1670  token=0x6000389  System.Void PrintDependencies(UnityEngine.GameObject targetGO)
  RVA=0x09D6AC30  token=0x600038A  System.String GetUniqueAssetPathForUnityAsset(UnityEngine.Object obj)
  RVA=0x02FFF600  token=0x600038B  System.Boolean IsValidFolderName(System.String name)
  RVA=-1  // generic def  token=0x600038C  T LoadUnityAssetFromUniqueAssetPath(System.String assetPath)
  RVA=-1  // generic def  token=0x600038D  T GetBuiltinExtraResource(System.String resourceName)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Defines
TYPE:  class
TOKEN: 0x2000058
SIZE:  0x10
FIELDS:
  public    static  System.String                   HEU_PRODUCT_NAME  // const
  public    static  System.String                   HEU_NAME  // const
  public    static  System.String                   HEU_PLUGIN_PATH  // static @ 0x0
  public    static  System.String                   HEU_TEXTURES_PATH  // static @ 0x8
  public    static  System.String                   HEU_BAKED_ASSETS_PATH  // static @ 0x10
  public    static  System.String                   HEU_ENGINE_ASSETS  // static @ 0x18
  public    static  System.String                   HAPI_PATH  // const
  public    static  System.String                   HEU_ENVPATH_PREFIX  // const
  public    static  System.String                   HEU_ENVPATH_KEY  // const
  public    static  System.Int32                    HEU_INVALID_NODE_ID  // const
  public    static  System.String                   HEU_DEFAULT_ASSET_NAME  // const
  public    static  System.String                   HEU_SESSION_PIPENAME  // const
  public    static  System.String                   HEU_SESSION_LOCALHOST  // const
  public    static  System.Int32                    HEU_SESSION_PORT  // const
  public    static  System.String                   HEU_SESSION_SHARED_MEMORY_NAME  // const
  public    static  System.Int32                    HEU_SESSION_SHARED_MEMORY_BUFFER_SIZE  // const
  public    static  HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferTypeHEU_SESSION_SHARED_MEMORY_BUFFER_TYPE  // const
  public    static  System.Single                   HEU_SESSION_TIMEOUT  // const
  public    static  System.Boolean                  HEU_SESSION_AUTOCLOSE  // const
  public    static  System.Int32                    HAPI_MAX_PAGE_SIZE  // const
  public    static  System.Int32                    HAPI_SEC_BEFORE_PROGRESS_BAR_SHOW  // const
  public    static  System.Int32                    HAPI_MAX_VERTICES_PER_FACE  // const
  public    static  System.Boolean                  HAPI_CURVE_REFINE_TO_LINEAR  // const
  public    static  System.Single                   HAPI_CURVE_LOD  // const
  public    static  System.Single                   HAPI_VOLUME_POSITION_MULT  // const
  public    static  System.Single                   HAPI_VOLUME_SURFACE_MAX_PT_PER_C  // const
  public    static  System.Single                   HAPI_VOLUME_SURFACE_DELTA_MULT  // const
  public    static  System.Single                   HAPI_VOLUME_SURFACE_PT_SIZE_MULT  // const
  public    static  System.String                   DEFAULT_TOP_NODE_FILTER  // const
  public    static  System.String                   DEFAULT_TOP_OUTPUT_FILTER  // const
  public    static  System.String                   HAPI_ATTRIB_ORIENT  // const
  public    static  System.String                   HAPI_ATTRIB_ROTATION  // const
  public    static  System.String                   HAPI_ATTRIB_SCALE  // const
  public    static  System.String                   HAPI_ATTRIB_ALPHA  // const
  public    static  System.String                   HAPI_HANDLE_TRANSFORM  // const
  public    static  System.Int32                    HAPI_MAX_UVS  // const
  public    static  System.String                   HAPI_OBJMERGE_TRANSFORM_PARAM  // const
  public    static  System.String                   HAPI_OBJMERGE_PACK_GEOMETRY  // const
  public    static  System.String                   HAPI_OBJPATH_1_PARAM  // const
  public    static  System.String                   NO_EXISTING_SESSION  // const
  public    static  System.String                   HEU_ERROR_TITLE  // const
  public    static  System.String                   HEU_INSTALL_INFO  // const
  public    static  System.String                   PLUGIN_STORE_KEYS  // const
  public    static  System.String                   PLUGIN_STORE_DATA  // const
  public    static  System.String                   PLUGIN_SESSION_DATA  // const
  public    static  System.String                   PLUGIN_SETTINGS_FILE  // const
  public    static  System.String                   PLUGIN_SESSION_FILE  // const
  public    static  System.String                   COOK_LOGS_FILE  // const
  public    static  System.String                   DEFAULT_COLLISION_GEO  // const
  public    static  System.String                   DEFAULT_RENDERED_COLLISION_GEO  // const
  public    static  System.String                   DEFAULT_RENDERED_CONVEX_COLLISION_GEO  // const
  public    static  System.String                   DEFAULT_CONVEX_COLLISION_GEO  // const
  public    static  System.String                   DEFAULT_SIMPLE_COLLISION_GEO  // const
  public    static  System.String                   DEFAULT_SIMPLE_RENDERED_COLLISION_GEO  // const
  public    static  System.String                   DEFAULT_SIMPLE_RENDERED_CONVEX_COLLISION_GEO  // const
  public    static  System.String                   DEFAULT_COLLISION_TRIGGER  // const
  public    static  System.String                   DEFAULT_UNITY_MATERIAL_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_SUBMATERIAL_NAME_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_SUBMATERIAL_INDEX_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TEXTURE_DIFFUSE_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TEXTURE_MASK_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TEXTURE_NORMAL_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_NORMAL_SCALE_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_METALLIC_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_SMOOTHNESS_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_SPECULAR_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TILE_OFFSET_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TILE_SIZE_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TERRAINDATA_FILE_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TERRAINDATA_EXPORT_FILE_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TERRAINLAYER_FILE_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_TERRAINDATA_EXPORT_PATH  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_HEIGHT_RANGE  // const
  public    static  System.String                   DEFAULT_UNITY_HEIGHTFIELD_YPOS  // const
  public    static  System.String                   HEIGHTFIELD_TREEPROTOTYPE  // const
  public    static  System.String                   HEIGHTFIELD_TREEINSTANCE_PROTOTYPEINDEX  // const
  public    static  System.String                   HEIGHTFIELD_TREEINSTANCE_HEIGHTSCALE  // const
  public    static  System.String                   HEIGHTFIELD_TREEINSTANCE_WIDTHSCALE  // const
  public    static  System.String                   HEIGHTFIELD_TREEINSTANCE_LIGHTMAPCOLOR  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_RESOLUTION_PER_PATCH  // const
  public    static  System.String                   HEIGHTFIELD_UNITY_TILE  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_DISTANCE  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_DENSITY  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_PREFAB  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_TEXTURE  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_BENDFACTOR  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_DRYCOLOR  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_HEALTHYCOLOR  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_MAXHEIGHT  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_MAXWIDTH  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_MINHEIGHT  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_MINWIDTH  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_NOISESPREAD  // const
  public    static  System.String                   HEIGHTFIELD_DETAIL_PROTOTYPE_RENDERMODE  // const
  public    static  System.String                   HEIGHTFIELD_LAYER_ATTR_TYPE  // const
  public    static  System.String                   HEIGHTFIELD_LAYER_TYPE_DETAIL  // const
  public    static  System.String                   HAPI_HEIGHTFIELD_TILE_ATTR  // const
  public    static  System.String                   HAPI_HEIGHTFIELD_LAYERNAME_HEIGHT  // const
  public    static  System.String                   HAPI_HEIGHTFIELD_LAYERNAME_MASK  // const
  public    static  System.String                   MAT_OGL_DIFF_ATTR  // const
  public    static  System.String                   MAT_DIFF_ATTR  // const
  public    static  System.String                   MAT_OGL_TEX1_ATTR  // const
  public    static  System.String                   MAT_OGL_TEX1_ATTR_ENABLED  // const
  public    static  System.String                   MAT_BASECOLOR_ATTR  // const
  public    static  System.String                   MAT_BASECOLOR_ATTR_ENABLED  // const
  public    static  System.String                   MAT_MAP_ATTR  // const
  public    static  System.String                   MAT_OGL_NORMAL_ATTR  // const
  public    static  System.String                   MAT_NORMAL_ATTR  // const
  public    static  System.String                   MAT_NORMAL_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OGL_SPEC_ATTR  // const
  public    static  System.String                   MAT_SPEC_ATTR  // const
  public    static  System.String                   MAT_OGL_SPEC_MAP_ATTR  // const
  public    static  System.String                   MAT_OGL_SPEC_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_SPEC_MAP_ATTR  // const
  public    static  System.String                   MAT_SPEC_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OGL_ROUGH_ATTR  // const
  public    static  System.String                   MAT_ROUGH_ATTR  // const
  public    static  System.String                   MAT_OGL_ROUGH_MAP_ATTR  // const
  public    static  System.String                   MAT_OGL_ROUGH_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_ROUGH_MAP_ATTR  // const
  public    static  System.String                   MAT_ROUGH_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OGL_METALLIC_ATTR  // const
  public    static  System.String                   MAT_METALLIC_ATTR  // const
  public    static  System.String                   MAT_OGL_METALLIC_MAP_ATTR  // const
  public    static  System.String                   MAT_OGL_METALLIC_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_METALLIC_MAP_ATTR  // const
  public    static  System.String                   MAT_METALLIC_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OGL_EMISSIVE_ATTR  // const
  public    static  System.String                   MAT_EMISSIVE_ATTR  // const
  public    static  System.String                   MAT_OGL_EMISSIVE_MAP_ATTR  // const
  public    static  System.String                   MAT_OGL_EMISSIVE_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_EMISSIVE_MAP_ATTR  // const
  public    static  System.String                   MAT_EMISSIVE_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OGL_ALPHA_ATTR  // const
  public    static  System.String                   MAT_ALPHA_ATTR  // const
  public    static  System.String                   MAT_OGL_OPACITY_MAP_ATTR  // const
  public    static  System.String                   MAT_OGL_OPACITY_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OPACITY_MAP_ATTR  // const
  public    static  System.String                   MAT_OPACITY_MAP_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OGL_TRANSPARENCY_ATTR  // const
  public    static  System.String                   MAT_OGL_TRANSPARENCY_ATTR_ENABLED  // const
  public    static  System.String                   MAT_OGL_OCCLUSION_MAP_ATTR  // const
  public    static  System.String                   MAT_OGL_OCCLUSION_MAP_ATTR_ENABLED  // const
  public    static  System.String                   CURVE_COORDS_PARAM  // const
  public    static  System.String                   CURVE_TYPE_PARAM  // const
  public    static  System.String                   CURVE_METHOD_PARAM  // const
  public    static  System.String                   CURVE_CLOSE_PARAM  // const
  public    static  System.String                   CURVE_REVERSE_PARAM  // const
  public    static  System.String                   HENGINE_STORE_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_TAG_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_SCRIPT_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_INSTANCE_ATTR  // const
  public    static  System.String                   UNITY_USE_INSTANCE_FLAGS_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_INPUT_MESH_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_STATIC_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_LAYER_ATTR  // const
  public    static  System.String                   DEFAULT_UNITY_MESH_READABLE  // const
  public    static  System.String                   DEFAULT_INSTANCE_PREFIX_ATTR  // const
  public    static  System.String                   UNITY_SHADER_COLOR  // const
  public    static  System.String                   UNITY_SHADER_SPEC_COLOR  // const
  public    static  System.String                   UNITY_SHADER_SPEC_MAP  // const
  public    static  System.String                   UNITY_SHADER_METALLIC  // const
  public    static  System.String                   UNITY_SHADER_METALLIC_MAP  // const
  public    static  System.String                   UNITY_SHADER_BUMP_MAP  // const
  public    static  System.String                   UNITY_SHADER_EMISSION_COLOR  // const
  public    static  System.String                   UNITY_SHADER_EMISSION_MAP  // const
  public    static  System.String                   UNITY_SHADER_SMOOTHNESS  // const
  public    static  System.String                   UNITY_SHADER_SMOOTHNESS_MAP  // const
  public    static  System.String                   UNITY_SHADER_SHININESS  // const
  public    static  System.String                   UNITY_SHADER_OCCLUSION  // const
  public    static  System.String                   UNITY_SHADER_OCCLUSION_MAP  // const
  public    static  System.String                   UNITY_SHADER_OPACITY  // const
  public    static  System.String                   UNITY_SHADER_OPACITY_MAP  // const
  public    static  System.String                   UNITY_EDITORONLY_TAG  // const
  public    static  System.String                   UNITY_HDADATA_NAME  // const
  public    static  System.String                   HOUDINI_SHADER_PREFIX  // const
  public    static  System.String                   DEFAULT_STANDARD_SHADER  // const
  public    static  System.String                   DEFAULT_VERTEXCOLOR_SHADER  // const
  public    static  System.String                   DEFAULT_TRANSPARENT_SHADER  // const
  public    static  System.String                   DEFAULT_STANDARD_SHADER_SPECULAR  // const
  public    static  System.String                   DEFAULT_STANDARD_SHADER_SPECULAR_LEGACY  // const
  public    static  System.String                   DEFAULT_VERTEXCOLOR_SHADER_SPECULAR  // const
  public    static  System.String                   DEFAULT_VERTEXCOLOR_SHADER_SPECULAR_LEGACY  // const
  public    static  System.String                   DEFAULT_TRANSPARENT_SHADER_SPECULAR  // const
  public    static  System.String                   DEFAULT_TRANSPARENT_SHADER_SPECULAR_LEGACY  // const
  public    static  System.String                   DEFAULT_CURVE_SHADER  // const
  public    static  System.String                   DEFAULT_TERRAIN_SHADER  // const
  public    static  System.String                   DEFAULT_STANDARD_SHADER_HDRP  // const
  public    static  System.String                   DEFAULT_VERTEXCOLOR_SHADER_HDRP  // const
  public    static  System.String                   DEFAULT_TRANSPARENT_SHADER_HDRP  // const
  public    static  System.String                   DEFAULT_STANDARD_SHADER_HDRP_SPECULAR  // const
  public    static  System.String                   DEFAULT_VERTEXCOLOR_SHADER_HDRP_SPECULAR  // const
  public    static  System.String                   DEFAULT_TRANSPARENT_SHADER_HDRP_SPECULAR  // const
  public    static  System.String                   DEFAULT_CURVE_SHADER_HDRP  // const
  public    static  System.String                   DEFAULT_TERRAIN_SHADER_HDRP  // const
  public    static  System.String                   DEFAULT_STANDARD_SHADER_URP  // const
  public    static  System.String                   DEFAULT_VERTEXCOLOR_SHADER_URP  // const
  public    static  System.String                   DEFAULT_TRANSPARENT_SHADER_URP  // const
  public    static  System.String                   DEFAULT_STANDARD_SHADER_URP_SPECULAR  // const
  public    static  System.String                   DEFAULT_VERTEXCOLOR_SHADER_URP_SPECULAR  // const
  public    static  System.String                   DEFAULT_TRANSPARENT_SHADER_URP_SPECULAR  // const
  public    static  System.String                   DEFAULT_CURVE_SHADER_URP  // const
  public    static  System.String                   DEFAULT_TERRAIN_SHADER_URP  // const
  public    static  System.String                   DEFAULT_UNITY_BUILTIN_RESOURCES  // const
  public    static  System.String                   DEFAULT_TERRAIN_MATERIAL_PATH  // const
  public    static  System.String                   DEFAULT_TERRAIN_MATERIAL_PATH_HDRP  // const
  public    static  System.String                   DEFAULT_TERRAIN_MATERIAL_PATH_URP  // const
  public    static  System.String                   DEFAULT_MATERIAL  // const
  public    static  System.Int32                    DEFAULT_MATERIAL_KEY  // static @ 0x20
  public    static  System.String                   EDITABLE_MATERIAL  // const
  public    static  System.Int32                    EDITABLE_MATERIAL_KEY  // static @ 0x24
  public    static  System.Int32                    HEU_INVALID_MATERIAL  // const
  public    static  System.String                   HEU_ASSET_CACHE_PATH  // const
  public    static  System.String                   HEU_WORKING_PATH  // const
  public    static  System.String                   HEU_BAKED_PATH  // const
  public    static  System.String                   HEU_BAKED_HDA  // const
  public    static  System.String                   HEU_BAKED_CLONE  // const
  public    static  System.String                   HEU_INSTANCE  // const
  public    static  System.String                   HEU_INSTANCE_PATTERN  // const
  public    static  System.String                   HEU_INSTANCE_SPLIT_ATTR  // const
  public    static  System.String                   HEU_DEFAULT_GEO_GROUP_NAME  // const
  public    static  System.String                   HEU_DEFAULT_LOD_NAME  // const
  public    static  System.String                   HEU_UNITY_LOD_TRANSITION_ATTR  // const
  public    static  System.String                   HEU_SUBASSET  // const
  public    static  System.String                   HEU_HENGINE_TOOLS_SHIPPED_FOLDER  // const
  public    static  System.String                   HEU_HENGINE_SHIPPED_SHELF  // const
  public    static  System.String                   HEU_PATH_KEY_PROJECT  // const
  public    static  System.String                   HEU_PATH_KEY_PLUGIN  // const
  public    static  System.String                   HEU_PATH_KEY_HFS  // const
  public    static  System.String                   HEU_PATH_KEY_TOOL  // const
  public    static  System.String                   HEU_USERMSG_NONEDITOR_NOT_SUPPORTED  // const
  public    static  System.String                   HEU_TERRAIN_SPLAT_DEFAULT  // const
  public    static  System.String                   HEU_FOLDER_MESHES  // const
  public    static  System.String                   HEU_FOLDER_MATERIALS  // const
  public    static  System.String                   HEU_FOLDER_TERRAIN  // const
  public    static  System.String                   HEU_FOLDER_TILE  // const
  public    static  System.String                   HEU_FOLDER_TEXTURES  // const
  public    static  System.String                   HEU_EXT_ASSET  // const
  public    static  System.String                   HEU_EXT_MAT  // const
  public    static  System.String                   HEU_EXT_TERRAINDATA  // const
  public    static  System.String                   HEU_EXT_TERRAINLAYER  // const
  public    static  System.String                   HEU_KEY_CTRL  // const
METHODS:
  RVA=0x041E1670  token=0x600038E  System.Void .ctor()
  RVA=0x09D6B53C  token=0x600038F  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HAPIUtility
TYPE:  static class
TOKEN: 0x2000059
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D6D4F8  token=0x6000390  System.String GetHoudiniEngineInstallationInfo()
  RVA=0x09D6CEAC  token=0x6000391  System.String GetEnvironmentPath()
  RVA=0x09D6DDC4  token=0x6000392  System.String GetRealPathFromHFSPath(System.String inPath)
  RVA=0x09D6C5F0  token=0x6000393  System.Boolean DoesMappedPathExist(System.String inPath)
  RVA=0x09D6E100  token=0x6000394  System.Boolean IsHoudiniAssetFile(System.String filePath)
  RVA=0x09D6C660  token=0x6000395  System.String FindHoudiniAssetFileInPathWithExt(System.String filePath)
  RVA=0x09D6E738  token=0x6000396  System.Void Log(System.String message)
  RVA=0x09D6E730  token=0x6000397  System.Void LogWarning(System.String message)
  RVA=0x09D6E728  token=0x6000398  System.Void LogError(System.String message)
  RVA=0x09D6E6CC  token=0x6000399  System.String LocateValidFilePath(UnityEngine.Object inObject)
  RVA=0x04D88C30  token=0x600039A  System.String LocateValidFilePath(System.String assetName, System.String inFilePath)
  RVA=0x09D6DEE8  token=0x600039B  UnityEngine.GameObject InstantiateHDA(System.String filePath, UnityEngine.Vector3 initialPosition, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, System.Boolean bLoadFromMemory, System.Boolean bAlwaysOverwriteOnLoad, UnityEngine.GameObject rootGO)
  RVA=0x09D6E618  token=0x600039C  System.Boolean LoadHDAFile(HoudiniEngineUnity.HEU_SessionBase session, System.String assetPath, System.Int32& assetLibraryID, System.String[]& assetNames)
  RVA=0x09D6C04C  token=0x600039D  System.Boolean CreateAndCookAssetNode(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName, System.Boolean bCookTemplatedGeos, System.Int32& newAssetID)
  RVA=0x09D6C0B8  token=0x600039E  System.Boolean CreateAndCookCurveAsset(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName, System.Boolean bCookTemplatedGeos, System.Int32& newAssetID)
  RVA=0x09D6C10C  token=0x600039F  System.Boolean CreateAndCookInputAsset(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName, System.Boolean bCookTemplatedGeos, System.Int32& newAssetID)
  RVA=0x09D6BE10  token=0x60003A0  System.Boolean CookNodeInHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Boolean bCookTemplatedGeos, System.String assetName)
  RVA=0x09D6BE10  token=0x60003A1  System.Boolean CookNodeInHoudiniWithOptions(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_CookOptions options, System.String assetName)
  RVA=0x09D6CE70  token=0x60003A2  HoudiniEngineUnity.HAPI_CookOptions GetDefaultCookOptions(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D6E740  token=0x60003A3  System.Boolean ProcessHoudiniCookStatus(HoudiniEngineUnity.HEU_SessionBase session, System.String assetName)
  RVA=0x09D6C174  token=0x60003A4  UnityEngine.GameObject CreateNewAsset(HoudiniEngineUnity.HEU_AssetTypeWrapper assetType, System.String rootName, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO)
  RVA=0x09D6C1C8  token=0x60003A5  UnityEngine.GameObject CreateNewAsset(HoudiniEngineUnity.HEU_HoudiniAsset.HEU_AssetType assetType, System.String rootName, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO)
  RVA=0x09D6C27C  token=0x60003A6  UnityEngine.GameObject CreateNewCurveAsset(System.String name, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO)
  RVA=0x09D6C2B0  token=0x60003A7  UnityEngine.GameObject CreateNewInputAsset(System.String name, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bBuildAsync, UnityEngine.GameObject rootGO)
  RVA=0x011EC580  token=0x60003A8  UnityEngine.GameObject LoadGeoWithNewGeoSync(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D6C2E4  token=0x60003A9  System.Void DestroyChildren(UnityEngine.Transform inTransform)
  RVA=0x09D6C5E0  token=0x60003AA  System.Void DestroyGameObject(UnityEngine.GameObject gameObect, System.Boolean bRegisterUndo)
  RVA=-1  // generic def  token=0x60003AB  System.Void DestroyChildrenWithComponent(UnityEngine.GameObject gameObject)
  RVA=0x09D6E218  token=0x60003AC  System.Boolean IsNodeValidInHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID)
  RVA=0x09D6CD90  token=0x60003AD  HoudiniEngineUnity.HEU_HoudiniAssetRoot GetAssetInScene(System.Int32 assetID)
  RVA=0x09D6BB9C  token=0x60003AE  System.Void ApplyWorldTransfromFromHoudiniToUnity(HoudiniEngineUnity.HAPI_Transform& hapiTransform, UnityEngine.Transform unityTransform)
  RVA=0x09D6B938  token=0x60003AF  System.Void ApplyLocalTransfromFromHoudiniToUnity(HoudiniEngineUnity.HAPI_Transform& hapiTransform, UnityEngine.Transform unityTransform)
  RVA=0x09D6B708  token=0x60003B0  System.Void ApplyLocalTransfromFromHoudiniToUnityForInstance(HoudiniEngineUnity.HAPI_Transform& hapiTransform, UnityEngine.Transform unityTransform)
  RVA=0x09D6BAE8  token=0x60003B1  System.Void ApplyMatrixToLocalTransform(UnityEngine.Matrix4x4& matrix, UnityEngine.Transform transform)
  RVA=0x09D6D5D4  token=0x60003B2  UnityEngine.Matrix4x4 GetMatrixFromHAPITransform(HoudiniEngineUnity.HAPI_Transform& hapiTransform, System.Boolean bConvertToUnity)
  RVA=0x09D6D9FC  token=0x60003B3  UnityEngine.Quaternion GetQuaternion(UnityEngine.Matrix4x4& m)
  RVA=0x09D6D9B0  token=0x60003B4  UnityEngine.Vector3 GetPosition(UnityEngine.Matrix4x4& m)
  RVA=0x09D6E7C0  token=0x60003B5  System.Void SetMatrixPosition(UnityEngine.Matrix4x4& m, UnityEngine.Vector3& position)
  RVA=0x04DA2840  token=0x60003B6  UnityEngine.Vector3 GetScale(UnityEngine.Matrix4x4& m)
  RVA=0x09D6CF1C  token=0x60003B7  HoudiniEngineUnity.HAPI_TransformEuler GetHAPITransformFromMatrix(UnityEngine.Matrix4x4& mat)
  RVA=0x09D6D364  token=0x60003B8  HoudiniEngineUnity.HAPI_TransformEuler GetHAPITransform(UnityEngine.Vector3& p, UnityEngine.Vector3& r, UnityEngine.Vector3& s)
  RVA=0x09D6D108  token=0x60003B9  HoudiniEngineUnity.HAPI_Transform GetHAPITransformQuatFromMatrix(UnityEngine.Matrix4x4& mat)
  RVA=0x09D6D524  token=0x60003BA  UnityEngine.Matrix4x4 GetMatrix4x4(UnityEngine.Vector3& p, UnityEngine.Vector3& r, UnityEngine.Vector3& s)
  RVA=0x09D6E290  token=0x60003BB  System.Boolean IsSameTransform(UnityEngine.Matrix4x4& transformMatrix, UnityEngine.Vector3& p, UnityEngine.Vector3& r, UnityEngine.Vector3& s)
  RVA=0x03418B30  token=0x60003BC  System.Boolean IsEqualTol(System.Single a, System.Single b, System.Single t)
  RVA=0x09D6E374  token=0x60003BD  System.Boolean IsTransformEqual(HoudiniEngineUnity.HAPI_Transform& transA, HoudiniEngineUnity.HAPI_Transform& transB)
  RVA=0x09D6E51C  token=0x60003BE  System.Boolean IsViewportEqual(HoudiniEngineUnity.HAPI_Viewport& viewA, HoudiniEngineUnity.HAPI_Viewport& viewB)
  RVA=0x09D6E348  token=0x60003BF  System.Boolean IsSessionSyncEqual(HoudiniEngineUnity.HAPI_SessionSyncInfo& syncA, HoudiniEngineUnity.HAPI_SessionSyncInfo& syncB)
  RVA=0x09D6BE10  token=0x60003C0  System.Boolean DoesGeoPartHaveAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner owner, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo)
  RVA=0x041E1670  token=0x60003C1  System.Void SetAnimationCurveTangentModes(UnityEngine.AnimationCurve animCurve, System.Collections.Generic.List<System.Int32> tangentValues)
  RVA=0x09D6E360  token=0x60003C2  System.Boolean IsSupportedPolygonType(HoudiniEngineUnity.HAPI_PartType partType)
  RVA=0x09D6D990  token=0x60003C3  System.Int32 GetParentNodeID(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID)
  RVA=0x09D6D7F8  token=0x60003C4  System.Boolean GetObjectInfos(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, HoudiniEngineUnity.HAPI_ObjectInfo[]& objectInfos, HoudiniEngineUnity.HAPI_Transform[]& objectTransforms)
  RVA=0x09D6BE10  token=0x60003C5  System.Boolean ContainsSopNodes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeId)
  RVA=0x09D6E234  token=0x60003C6  System.Boolean IsObjNodeFullyVisible(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.HashSet<System.Int32> allObjectIds, System.Int32 inRootNodeId, System.Int32 inChildNodeId)
  RVA=0x09D6D958  token=0x60003C7  System.Boolean GetOutputIndex(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeId, System.Int32& outputIndex)
  RVA=0x09D6C7DC  token=0x60003C8  System.Void GatherAllAssetGeoInfos(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_AssetInfo assetInfo, HoudiniEngineUnity.HAPI_ObjectInfo objectInfo, System.Boolean bUseOutputNodes, System.Boolean bGetEditableNodes, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_GeoInfo>& outGeoInfos)
  RVA=0x09D6CCF4  token=0x60003C9  System.Void GatherAllObjectGeoInfos(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetId, System.Boolean bUseOutputNodes, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_GeoInfo>& outGeoInfos)
  RVA=0x09D6C904  token=0x60003CA  System.Void GatherAllAssetOutputs(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeId, System.Boolean bUseOutputNodes, System.Boolean bOutputTemplatedGeos, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_GeoInfo>& outGeoInfos)
  RVA=0x09D6E80C  token=0x60003CB  System.String ToHapiVariableName(System.String name)
  RVA=0x04DA27D0  token=0x60003CC  System.Void ConvertPositionUnityToHoudini(UnityEngine.Vector3& position)
  RVA=0x04DA27B0  token=0x60003CD  System.Void ConvertPositionUnityToHoudini(UnityEngine.Vector3 position, System.Single& outputX, System.Single& outputY, System.Single& outputZ)
  RVA=0x04DA2790  token=0x60003CE  UnityEngine.Vector3 ConvertPositionUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ)
  RVA=0x04DA2760  token=0x60003CF  UnityEngine.Vector3 ConvertPositionUnityToHoudini(UnityEngine.Vector3 inputVec)
  RVA=0x04DA27E0  token=0x60003D0  System.Void ConvertPositionUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ, UnityEngine.Vector3& outputVec)
  RVA=0x09D6BF6C  token=0x60003D1  System.Void ConvertRotationUnityToHoudini(UnityEngine.Quaternion& rotation)
  RVA=0x09D6BE28  token=0x60003D2  System.Void ConvertRotationUnityToHoudini(UnityEngine.Quaternion rotation, System.Single& outputX, System.Single& outputY, System.Single& outputZ, System.Single& outputW)
  RVA=0x09D6BEDC  token=0x60003D3  UnityEngine.Quaternion ConvertRotationUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ, System.Single inputW)
  RVA=0x09D6BFDC  token=0x60003D4  UnityEngine.Quaternion ConvertRotationUnityToHoudini(UnityEngine.Quaternion inputQuat)
  RVA=0x041E1670  token=0x60003D5  System.Void ConvertScaleUnityToHoudini(UnityEngine.Vector3& position)
  RVA=0x04DA2800  token=0x60003D6  System.Void ConvertScaleUnityToHoudini(UnityEngine.Vector3 position, System.Single& outputX, System.Single& outputY, System.Single& outputZ)
  RVA=0x04DA2820  token=0x60003D7  UnityEngine.Vector3 ConvertScaleUnityToHoudini(System.Single inputX, System.Single inputY, System.Single inputZ)
  RVA=0x04D93250  token=0x60003D8  UnityEngine.Vector3 ConvertScaleUnityToHoudini(UnityEngine.Vector3 inputVec)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HoudiniEngineError
TYPE:  class
TOKEN: 0x200005A
SIZE:  0x98
EXTENDS: System.Exception
FIELDS:
  protected         System.String                   _errorMsg  // 0x90
METHODS:
  RVA=0x09D6F7C8  token=0x60003D9  System.Void .ctor()
  RVA=0x09D6F740  token=0x60003DA  System.Void .ctor(System.String errorMsg)
  RVA=0x04D86220  token=0x60003DB  System.String ToString()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Platform
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x10
FIELDS:
  private   static  System.String                   _lastErrorMsg  // static @ 0x0
  private   static  System.String                   _libPath  // static @ 0x8
  private   static  System.Boolean                  _pathSet  // static @ 0x10
PROPERTIES:
  LibPath  get=0x09D7D048
  IsPathSet  get=0x09D7CFF8
  DirectorySeparator  get=0x04DA29E0
  DirectorySeparatorStr  get=0x09D7CFCC
METHODS:
  RVA=0x09D7CFC4  token=0x60003DE  System.Void .cctor()
  RVA=0x09D7C8C4  token=0x60003DF  System.String GetHoudiniEnginePath()
  RVA=0x09D7C63C  token=0x60003E0  System.String GetHoudiniEngineDefaultPath()
  RVA=0x09D7C93C  token=0x60003E1  System.String GetSavedHoudiniPath()
  RVA=0x09D7CDC4  token=0x60003E2  System.Void SetHapiClientName()
  RVA=0x041E1670  token=0x60003E3  System.Void SetHoudiniEnginePath()
  RVA=0x09D7C298  token=0x60003E4  System.String GetAllFoldersInPath(System.String path)
  RVA=0x09D7C178  token=0x60003E5  System.Void GetAllFoldersInPathHelper(System.String inPath, System.Text.StringBuilder pathBuilder)
  RVA=0x09D7C3F0  token=0x60003E6  System.String[] GetFilesInFolder(System.String folderPath, System.String searchPattern, System.Boolean bRecursive)
  RVA=0x09D7C3A4  token=0x60003E7  System.String GetFileName(System.String path)
  RVA=0x09D7C358  token=0x60003E8  System.String GetFileNameWithoutExtension(System.String path)
  RVA=0x09D7C508  token=0x60003E9  System.String GetFolderPath(System.String path, System.Boolean bRemoveDirectorySeparatorAtEnd)
  RVA=0x09D7C000  token=0x60003EC  System.String BuildPath(System.String folder1, System.String folder2, System.Object[] args)
  RVA=0x09D7CE0C  token=0x60003ED  System.String TrimLastDirectorySeparator(System.String inPath)
  RVA=0x09D7C148  token=0x60003EE  System.Boolean DoesPathExist(System.String inPath)
  RVA=0x09D7C140  token=0x60003EF  System.Boolean DoesFileExist(System.String inPath)
  RVA=0x09D7C138  token=0x60003F0  System.Boolean DoesDirectoryExist(System.String inPath)
  RVA=0x09D7C110  token=0x60003F1  System.Boolean CreateDirectory(System.String inPath)
  RVA=0x09D7C91C  token=0x60003F2  System.String GetParentDirectory(System.String inPath)
  RVA=0x09D7C5F0  token=0x60003F3  System.String GetFullPath(System.String inPath)
  RVA=0x09D7CA58  token=0x60003F4  System.Boolean IsPathRooted(System.String inPath)
  RVA=0x09D7CFBC  token=0x60003F5  System.Void WriteBytes(System.String path, System.Byte[] bytes)
  RVA=0x09D7CE9C  token=0x60003F6  System.Boolean WriteAllText(System.String path, System.String text)
  RVA=0x09D7CC6C  token=0x60003F7  System.String ReadAllText(System.String path)
  RVA=0x09D7C348  token=0x60003F8  System.String GetEnvironmentValue(System.String key)
  RVA=0x09D7C838  token=0x60003F9  System.String GetHoudiniEngineEnvironmentFilePathFull()
  RVA=0x09D7CAA4  token=0x60003FA  System.Boolean LoadFileIntoMemory(System.String path, System.Byte[]& buffer)
  RVA=0x041E1670  token=0x60003FB  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PlatformWin
TYPE:  static class
TOKEN: 0x200005C
SIZE:  0x10
FIELDS:
  public    static  System.UIntPtr                  HKEY_LOCAL_MACHINE  // static @ 0x0
  public    static  System.UIntPtr                  HKEY_CURRENT_USER  // static @ 0x8
METHODS:
  RVA=0x09D7BDE8  token=0x60003FC  System.UInt32 RegOpenKeyEx(System.UIntPtr hKey, System.String lpSubKey, System.UInt32 ulOptions, System.Int32 samDesired, System.Int32& phkResult)
  RVA=0x09D7BEAC  token=0x60003FD  System.Int32 RegQueryValueEx(System.Int32 hKey, System.String lpValueName, System.Int32 lpReserved, System.UInt32& lpType, System.Text.StringBuilder lpData, System.UInt32& lpcbData)
  RVA=0x09D7BD70  token=0x60003FE  System.UInt32 RegCloseKey(System.Int32 hKey)
  RVA=0x09D7BA98  token=0x60003FF  System.String GetRegistryKeyValue(System.UIntPtr rootKey, System.String keyName, HoudiniEngineUnity.HEU_PlatformWin.RegSAM is32or64Key, System.String inPropertyName)
  RVA=0x09D7BD04  token=0x6000400  System.String GetRegistryKeyvalue_x86(System.UIntPtr rootKey, System.String keyName, System.String inPropertyName)
  RVA=0x09D7BC98  token=0x6000401  System.String GetRegistryKeyvalue_x64(System.UIntPtr rootKey, System.String keyName, System.String inPropertyName)
  RVA=0x09D7B758  token=0x6000402  System.String GetApplicationPath(System.String appName)
  RVA=0x09D7BFAC  token=0x6000403  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PluginSettings
TYPE:  static class
TOKEN: 0x200005E
SIZE:  0x10
FIELDS:
  public    static  System.Action<System.Boolean>   s_onEnableRawHoudiniChanged  // static @ 0x0
PROPERTIES:
  HoudiniEngineEnvFilePath  get=0x09D7DDF0  set=0x09D7FAB0
  EnableRawHoudini  get=0x09D7DB6C  set=0x09D7F88C
  CookingEnabled  get=0x09D7D360  set=0x09D7F524
  CookingTriggersDownstreamCooks  get=0x09D7D3C0  set=0x09D7F574
  CookDisabledGameObjects  get=0x09D7D1E0  set=0x09D7F3E4
  CookTemplatedGeos  get=0x09D7D300  set=0x09D7F4D4
  PushUnityTransformToHoudini  get=0x09D7E484  set=0x09D7FFC0
  TransformChangeTriggersCooks  get=0x09D7EBD8  set=0x09D80580
  ChildTransformChangeTriggersCooks  get=0x09D7D10C  set=0x09D7F340
  CollisionGroupName  get=0x09D7D16C  set=0x09D7F390
  RenderedCollisionGroupName  get=0x09D7E4E4  set=0x09D80010
  RenderedConvexCollisionGroupName  get=0x09D7E558  set=0x09D80064
  UnityMaterialAttribName  get=0x09D7ED94  set=0x09D806CC
  UnitySubMaterialAttribName  get=0x09D7EEF0  set=0x09D807C8
  UnitySubMaterialIndexAttribName  get=0x09D7EF64  set=0x09D8081C
  UnityTagAttributeName  get=0x09D7EFD8  set=0x09D80870
  UnityStaticAttributeName  get=0x09D7EE7C  set=0x09D80774
  UnityScriptAttributeName  get=0x09D7EE08  set=0x09D80720
  UnityLayerAttributeName  get=0x09D7ED20  set=0x09D80678
  ImageGamma  get=0x09D7DED8  set=0x09D7FB6C
  NormalGenerationThresholdAngle  get=0x09D7E41C  set=0x09D7FF68
  LastLoadHDAPath  get=0x09D7E238  set=0x09D7FE04
  LastLoadHIPPath  get=0x09D7E2AC  set=0x09D7FE58
  InstanceAttr  get=0x09D7E0DC  set=0x09D7FD08
  UnityInstanceAttr  get=0x09D7ECAC  set=0x09D80624
  UnityInputMeshAttr  get=0x09D7EC38  set=0x09D805D0
  LineColor  get=0x09D7E320  set=0x09D7FEAC
  UseHybridCurveEditing  get=0x09D7F10C  set=0x09D80964
  EditorOnly_Tag  get=0x09D7DAF8  set=0x09D7F808
  HDAData_Name  get=0x09D7DBCC  set=0x09D7F95C
  Session_Mode  get=0x09D7E700  set=0x09D801AC
  Session_PipeName  get=0x09D7E768  set=0x09D801FC
  Session_Localhost  get=0x09D7E68C  set=0x09D80158
  Session_Port  get=0x09D7E7DC  set=0x09D80250
  Session_SharedMemoryName  get=0x09D7E908  set=0x09D80340
  Session_SharedMemoryBufferSize  get=0x09D7E840  set=0x09D802A0
  Session_SharedMemoryBufferType  get=0x09D7E8A4  set=0x09D802F0
  Session_Timeout  get=0x09D7E97C  set=0x09D80394
  Session_AutoClose  get=0x09D7E62C  set=0x09D80108
  Curves_ShowInSceneView  get=0x09D7D420  set=0x09D7F5C4
  AssetCachePath  get=0x09D7D098  set=0x09D7F2EC
  UseFullPathNamesForOutput  get=0x09D7F04C  set=0x09D808C4
  HEngineToolsShelves  get=0x09D7DCA0  set=0x09D7FA00
  HEngineShelfSelectedIndex  get=0x09D7DC40  set=0x09D7F9B0
  DefaultTerrainMaterial  get=0x09D7D71C  set=0x09D7F6BC
  TerrainSplatTextureDefault  get=0x09D7EB64  set=0x09D8052C
  DefaultStandardShader  get=0x09D7D598  set=0x09D7F668
  DefaultVertexColorShader  get=0x09D7D914  set=0x09D7F764
  DefaultTransparentShader  get=0x09D7D790  set=0x09D7F710
  DefaultCurveShader  get=0x09D7D480  set=0x09D7F614
  SupportHoudiniBoxType  get=0x09D7EAA4  set=0x09D8048C
  SupportHoudiniSphereType  get=0x09D7EB04  set=0x09D804DC
  SetCurrentThreadToInvariantCulture  get=0x09D7E9E4  set=0x09D803EC
  HoudiniDebugLaunchPath  get=0x09D7DD04  set=0x09D7FA5C
  LastExportPath  get=0x09D7E150  set=0x09D7FD5C
  InputSelectionFilterLocation  get=0x09D7DF40  set=0x09D7FBC4
  InputSelectionFilterState  get=0x09D7E078  set=0x09D7FCB8
  InputSelectionFilterRoots  get=0x09D7E018  set=0x09D7FC68
  InputSelectionFilterName  get=0x09D7DFA4  set=0x09D7FC14
  CookOptionSplitGeosByGroup  get=0x09D7D2A0  set=0x09D7F484
  MaxVerticesPerPrimitive  get=0x09D7E3B8  set=0x09D7FF18
  HoudiniInstallPath  get=0x09D7DE64  set=0x09D7FB18
  LastHoudiniVersion  get=0x09D7E1C4  set=0x09D7FDB0
  SessionSyncAutoCook  get=0x09D7E5CC  set=0x09D800B8
  WriteCookLogs  get=0x09D7F28C  set=0x09D80AA4
  UseHDRColor  get=0x09D7F0AC  set=0x09D80914
  UseSpecularShader  get=0x09D7F22C  set=0x09D80A54
  UseLegacyShaders  get=0x09D7F1CC  set=0x09D80A04
  ShortenFolderPaths  get=0x09D7EA44  set=0x09D8043C
  UseLegacyInputCurves  get=0x09D7F16C  set=0x09D809B4
  CookOnMouseUp  get=0x09D7D240  set=0x09D7F434
  EditableNodesToolsEnabled  get=0x09D7DA98  set=0x09D7F7B8
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PluginStorage
TYPE:  class
TOKEN: 0x200005F
SIZE:  0x28
FIELDS:
  protected         System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_PluginStorage.StoreData>_dataMap  // 0x10
  private           System.Collections.Generic.Dictionary<System.String,System.String>_envPathMap  // 0x18
  protected         System.Boolean                  _requiresSave  // 0x20
  private   static  HoudiniEngineUnity.HEU_PluginStorage_instance  // static @ 0x0
  public    static  System.String                   PluginSettingsLine1  // const
  public    static  System.String                   PluginSettingsLine2  // const
  public    static  System.String                   PluginSettingsVersion  // const
PROPERTIES:
  RequiresSave  get=0x04D866B0
  Instance  get=0x09D82F3C
METHODS:
  RVA=0x0385B100  token=0x6000494  System.Collections.Generic.Dictionary<System.String,System.String> GetEnvironmentPathMap()
  RVA=0x09D816AC  token=0x6000497  System.Void InstantiateAndLoad()
  RVA=0x041E1670  token=0x6000498  System.Void SetCurrentCulture(System.Boolean useInvariant)
  RVA=-1  // generic def  token=0x6000499  T[] GetJSONArray(System.String jsonArray)
  RVA=0x09D82D50  token=0x600049A  System.Void Set(System.String key, System.Boolean value)
  RVA=0x09D82A8C  token=0x600049B  System.Void Set(System.String key, System.Int32 value)
  RVA=0x09D829C8  token=0x600049C  System.Void Set(System.String key, System.Int64 value)
  RVA=0x09D82C8C  token=0x600049D  System.Void Set(System.String key, System.Single value)
  RVA=0x09D82930  token=0x600049E  System.Void Set(System.String key, System.String value)
  RVA=0x09D82B50  token=0x600049F  System.Void Set(System.String key, System.Collections.Generic.List<System.String> values, System.Char delimiter)
  RVA=0x09D81414  token=0x60004A0  System.Boolean Get(System.String key, System.Boolean& value, System.Boolean defaultValue)
  RVA=0x09D81120  token=0x60004A1  System.Boolean Get(System.String key, System.Int32& value, System.Int32 defaultValue)
  RVA=0x09D81340  token=0x60004A2  System.Boolean Get(System.String key, System.Int64& value, System.Int64 defaultValue)
  RVA=0x09D815AC  token=0x60004A3  System.Boolean Get(System.String key, System.Single& value, System.Single defaultValue)
  RVA=0x09D814E8  token=0x60004A4  System.Boolean Get(System.String key, System.String& value, System.String defaultValue)
  RVA=0x09D811F4  token=0x60004A5  System.Boolean Get(System.String key, System.Collections.Generic.List<System.String>& values, System.Char delimiter)
  RVA=0x041E1670  token=0x60004A6  System.Void MarkDirtyForSave()
  RVA=0x09D823DC  token=0x60004A7  System.Void SaveIfRequired()
  RVA=0x09D82E14  token=0x60004A8  System.String SettingsFilePath()
  RVA=0x09D82428  token=0x60004A9  System.Boolean SavePluginData(System.String file)
  RVA=0x09D81BCC  token=0x60004AA  System.Boolean LoadPluginData(System.String file)
  RVA=0x012081B0  token=0x60004AB  System.Boolean ReadFromEditorPrefs()
  RVA=0x09D80AF4  token=0x60004AC  System.Void ClearPluginData()
  RVA=0x09D81B84  token=0x60004AD  System.Void LoadFromSavedFile()
  RVA=0x09D82880  token=0x60004AE  System.String SessionFilePath()
  RVA=0x041E1670  token=0x60004AF  System.Void SaveAllSessionData(System.Collections.Generic.List<HoudiniEngineUnity.HEU_SessionBase> allSessions, System.String path)
  RVA=0x09D81794  token=0x60004B0  System.Collections.Generic.List<HoudiniEngineUnity.HEU_SessionData> LoadAllSessionData(System.String path)
  RVA=0x041E1670  token=0x60004B1  System.Void DeleteAllSavedSessionData(System.String file)
  RVA=0x09D817F4  token=0x60004B2  System.Void LoadAssetEnvironmentPaths()
  RVA=0x09D80E50  token=0x60004B3  System.String ConvertRealPathToEnvKeyedPath(System.String inPath)
  RVA=0x09D80BAC  token=0x60004B4  System.String ConvertEnvKeyedPathToReal(System.String inPath)
  RVA=0x09D82EC4  token=0x60004B5  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Handle
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x98
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.String                   _handleName  // 0x18
  private           HoudiniEngineUnity.HEU_Handle.HEU_HandleType_handleType  // 0x20
  private           System.Int32                    _handleIndex  // 0x24
  private           HoudiniEngineUnity.HEU_HandleParamBinding_handleParamTranslateBinding  // 0x28
  private           HoudiniEngineUnity.HEU_HandleParamBinding_handleParamRotateBinding  // 0x30
  private           HoudiniEngineUnity.HEU_HandleParamBinding_handleParamScaleBinding  // 0x38
  private           UnityEngine.Vector3             _handlePosition  // 0x40
  private           UnityEngine.Quaternion          _handleRotation  // 0x4c
  private           UnityEngine.Vector3             _handleScale  // 0x5c
  private           HoudiniEngineUnity.HAPI_RSTOrder_rstOrder  // 0x68
  private           HoudiniEngineUnity.HAPI_XYZOrder_xyzOrder  // 0x6c
  private           HoudiniEngineUnity.HAPI_TransformEuler_convertedTransformEuler  // 0x70
PROPERTIES:
  HandleName  get=0x0385B100
  HandleType  get=0x02B70F90
  RSTOrder  get=0x04D86CF0
  XYZOrder  get=0x04D86B50
  ConvertedTransformEuler  get=0x04DA2930
  HandlePosition  get=0x04D89660
  HandleRotation  get=0x04D8D360
  HandleScale  get=0x04DA2960
METHODS:
  RVA=0x068C60A8  token=0x60004BD  System.Boolean HasTranslateHandle()
  RVA=0x09B11948  token=0x60004BE  System.Boolean HasRotateHandle()
  RVA=0x09B0008C  token=0x60004BF  System.Boolean HasScaleHandle()
  RVA=0x09D6F5A0  token=0x60004C0  System.Boolean IsTranslateHandleDisabled()
  RVA=0x09D6F564  token=0x60004C1  System.Boolean IsRotateHandleDisabled()
  RVA=0x09D6F578  token=0x60004C2  System.Boolean IsScaleHandleDisabled()
  RVA=0x04D86240  token=0x60004C3  HoudiniEngineUnity.HEU_HandleParamBinding GetTranslateBinding()
  RVA=0x011F36E0  token=0x60004C4  HoudiniEngineUnity.HEU_HandleParamBinding GetRotateBinding()
  RVA=0x04D85A50  token=0x60004C5  HoudiniEngineUnity.HEU_HandleParamBinding GetScaleBinding()
  RVA=0x09D6F5B4  token=0x60004C9  System.Boolean SetupHandle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32 handleIndex, System.String handleName, HoudiniEngineUnity.HEU_Handle.HEU_HandleType handleType, HoudiniEngineUnity.HAPI_HandleInfo& handleInfo, HoudiniEngineUnity.HEU_Parameters parameters)
  RVA=0x09D6EAAC  token=0x60004CA  System.Void CleanUp()
  RVA=0x09D6EAE4  token=0x60004CB  System.Void GenerateTransform(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_Parameters parameters)
  RVA=0x09D6EEFC  token=0x60004CC  System.Boolean GetUpdatedPosition(HoudiniEngineUnity.HEU_HoudiniAsset asset, UnityEngine.Vector3& inPosition)
  RVA=0x09D6EFB8  token=0x60004CD  System.Boolean GetUpdatedRotation(HoudiniEngineUnity.HEU_HoudiniAsset asset, UnityEngine.Quaternion& inRotation)
  RVA=0x09D6F58C  token=0x60004CE  System.Boolean IsSpecialRSTOrder(HoudiniEngineUnity.HAPI_RSTOrder rstOrder)
  RVA=0x09D6F0EC  token=0x60004CF  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_Handle other)
  RVA=0x09D6F6D8  token=0x60004D0  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HandleParamBinding
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x30
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HEU_HandleParamBinding.HEU_HandleParamType_paramType  // 0x10
  public            System.Int32                    _parmID  // 0x14
  public            System.String                   _paramName  // 0x18
  public            System.Boolean                  _bDisabled  // 0x20
  public            System.Boolean[]                _boundChannels  // 0x28
METHODS:
  RVA=0x09D6E898  token=0x60004D1  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_HandleParamBinding other)
  RVA=0x09D6EA64  token=0x60004D2  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HAPIConstants
TYPE:  static class
TOKEN: 0x2000067
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    HAPI_POSITION_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_SCALE_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_SHEAR_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_NORMAL_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_QUATERNION_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_EULER_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_UV_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_COLOR_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_CV_VECTOR_SIZE  // const
  public    static  System.Int32                    HAPI_PRIM_MIN_VERTEX_COUNT  // const
  public    static  System.Int32                    HAPI_PRIM_MAX_VERTEX_COUNT  // const
  public    static  System.Int32                    HAPI_INVALID_PARM_ID  // const
  public    static  System.Int32                    HAPI_MAX_NUM_CONNECTIONS  // const
  public    static  System.String                   HAPI_ATTRIB_POSITION  // const
  public    static  System.String                   HAPI_ATTRIB_UV  // const
  public    static  System.String                   HAPI_ATTRIB_UV2  // const
  public    static  System.String                   HAPI_ATTRIB_NORMAL  // const
  public    static  System.String                   HAPI_ATTRIB_TANGENT  // const
  public    static  System.String                   HAPI_ATTRIB_TANGENT2  // const
  public    static  System.String                   HAPI_ATTRIB_COLOR  // const
  public    static  System.String                   HAPI_ATTRIB_NAME  // const
  public    static  System.String                   HAPI_ATTRIB_INSTANCE  // const
  public    static  System.String                   HAPI_ATTRIB_ROT  // const
  public    static  System.String                   HAPI_ATTRIB_SCALE  // const
  public    static  System.String                   HAPI_UNGROUPED_GROUP_NAME  // const
  public    static  System.String                   HAPI_RAW_FORMAT_NAME  // const
  public    static  System.String                   HAPI_PNG_FORMAT_NAME  // const
  public    static  System.String                   HAPI_JPEG_FORMAT_NAME  // const
  public    static  System.String                   HAPI_BMP_FORMAT_NAME  // const
  public    static  System.String                   HAPI_TIFF_FORMAT_NAME  // const
  public    static  System.String                   HAPI_TGA_FORMAT_NAME  // const
  public    static  System.String                   HAPI_DEFAULT_IMAGE_FORMAT_NAME  // const
  public    static  System.String                   HAPI_GLOBAL_NODES_NODE_NAME  // const
  public    static  System.String                   HAPI_ENV_HIP  // const
  public    static  System.String                   HAPI_ENV_JOB  // const
  public    static  System.String                   HAPI_ENV_CLIENT_NAME  // const
  public    static  System.String                   HAPI_CACHE_COP2_COOK  // const
  public    static  System.String                   HAPI_CACHE_COP2_FLIPBOOK  // const
  public    static  System.String                   HAPI_CACHE_IMAGE  // const
  public    static  System.String                   HAPI_CACHE_OBJ  // const
  public    static  System.String                   HAPI_CACHE_GL_TEXTURE  // const
  public    static  System.String                   HAPI_CACHE_GL_VERTEX  // const
  public    static  System.String                   HAPI_CACHE_SOP  // const
  public    static  System.String                   HAPI_CACHE_VEX  // const
  public    static  System.String                   HAPI_ATTRIB_INPUT_CURVE_COORDS  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_License
TYPE:  sealed struct
TOKEN: 0x2000068
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_NONE  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_FX  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE_INDIE  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_INDIE  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE_UNITY_UNREAL  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_EDUCATION  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_HOUDINI_ENGINE_EDUCATION  // const
  public    static  HoudiniEngineUnity.HAPI_License HAPI_LICENSE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_StatusType
TYPE:  sealed struct
TOKEN: 0x2000069
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_StatusTypeHAPI_STATUS_CALL_RESULT  // const
  public    static  HoudiniEngineUnity.HAPI_StatusTypeHAPI_STATUS_COOK_RESULT  // const
  public    static  HoudiniEngineUnity.HAPI_StatusTypeHAPI_STATUS_COOK_STATE  // const
  public    static  HoudiniEngineUnity.HAPI_StatusTypeHAPI_STATUS_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_StatusVerbosity
TYPE:  sealed struct
TOKEN: 0x200006A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_StatusVerbosityHAPI_STATUSVERBOSITY_0  // const
  public    static  HoudiniEngineUnity.HAPI_StatusVerbosityHAPI_STATUSVERBOSITY_1  // const
  public    static  HoudiniEngineUnity.HAPI_StatusVerbosityHAPI_STATUSVERBOSITY_2  // const
  public    static  HoudiniEngineUnity.HAPI_StatusVerbosityHAPI_STATUSVERBOSITY_ALL  // const
  public    static  HoudiniEngineUnity.HAPI_StatusVerbosityHAPI_STATUSVERBOSITY_ERRORS  // const
  public    static  HoudiniEngineUnity.HAPI_StatusVerbosityHAPI_STATUSVERBOSITY_WARNINGS  // const
  public    static  HoudiniEngineUnity.HAPI_StatusVerbosityHAPI_STATUSVERBOSITY_MESSAGES  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_JobStatus
TYPE:  sealed struct
TOKEN: 0x200006B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_JobStatusHAPI_JOB_STATUS_RUNNING  // const
  public    static  HoudiniEngineUnity.HAPI_JobStatusHAPI_JOB_STATUS_IDLE  // const
  public    static  HoudiniEngineUnity.HAPI_JobStatusHAPI_JOB_STATUS_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_Result
TYPE:  sealed struct
TOKEN: 0x200006C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_SUCCESS  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_FAILURE  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_ALREADY_INITIALIZED  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_NOT_INITIALIZED  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_CANT_LOADFILE  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_PARM_SET_FAILED  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_INVALID_ARGUMENT  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_CANT_LOAD_GEO  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_CANT_GENERATE_PRESET  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_CANT_LOAD_PRESET  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_ASSET_DEF_ALREADY_LOADED  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_NO_LICENSE_FOUND  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_DISALLOWED_NC_LICENSE_FOUND  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_DISALLOWED_NC_ASSET_WITH_C_LICENSE  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_DISALLOWED_NC_ASSET_WITH_LC_LICENSE  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_DISALLOWED_LC_ASSET_WITH_C_LICENSE  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_DISALLOWED_HENGINEINDIE_W_3PARTY_PLUGIN  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_SHARED_MEMORY_BUFFER_OVERFLOW  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_INVALID_SHARED_MEMORY_BUFFER  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_ASSET_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_NODE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_USER_INTERRUPTED  // const
  public    static  HoudiniEngineUnity.HAPI_Result  HAPI_RESULT_INVALID_SESSION  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ErrorCode
TYPE:  sealed struct
TOKEN: 0x200006D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_ErrorCodeHAPI_ERRORCODE_ASSET_DEF_NOT_FOUND  // const
  public    static  HoudiniEngineUnity.HAPI_ErrorCodeHAPI_ERRORCODE_PYTHON_NODE_ERROR  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_SessionType
TYPE:  sealed struct
TOKEN: 0x200006E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_SessionTypeHAPI_SESSION_INPROCESS  // const
  public    static  HoudiniEngineUnity.HAPI_SessionTypeHAPI_SESSION_THRIFT  // const
  public    static  HoudiniEngineUnity.HAPI_SessionTypeHAPI_SESSION_CUSTOM1  // const
  public    static  HoudiniEngineUnity.HAPI_SessionTypeHAPI_SESSION_CUSTOM2  // const
  public    static  HoudiniEngineUnity.HAPI_SessionTypeHAPI_SESSION_CUSTOM3  // const
  public    static  HoudiniEngineUnity.HAPI_SessionTypeHAPI_SESSION_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_State
TYPE:  sealed struct
TOKEN: 0x200006F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_READY  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_READY_WITH_FATAL_ERRORS  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_READY_WITH_COOK_ERRORS  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_STARTING_COOK  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_COOKING  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_STARTING_LOAD  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_LOADING  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_MAX  // const
  public    static  HoudiniEngineUnity.HAPI_State   HAPI_STATE_MAX_READY_STATE  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PackedPrimInstancingMode
TYPE:  sealed struct
TOKEN: 0x2000070
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PackedPrimInstancingModeHAPI_PACKEDPRIM_INSTANCING_MODE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_PackedPrimInstancingModeHAPI_PACKEDPRIM_INSTANCING_MODE_DISABLED  // const
  public    static  HoudiniEngineUnity.HAPI_PackedPrimInstancingModeHAPI_PACKEDPRIM_INSTANCING_MODE_HIERARCHY  // const
  public    static  HoudiniEngineUnity.HAPI_PackedPrimInstancingModeHAPI_PACKEDPRIM_INSTANCING_MODE_FLAT  // const
  public    static  HoudiniEngineUnity.HAPI_PackedPrimInstancingModeHAPI_PACKEDPRIM_INSTANCING_MODE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_Permissions
TYPE:  sealed struct
TOKEN: 0x2000071
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PermissionsHAPI_PERMISSIONS_NON_APPLICABLE  // const
  public    static  HoudiniEngineUnity.HAPI_PermissionsHAPI_PERMISSIONS_READ_WRITE  // const
  public    static  HoudiniEngineUnity.HAPI_PermissionsHAPI_PERMISSIONS_READ_ONLY  // const
  public    static  HoudiniEngineUnity.HAPI_PermissionsHAPI_PERMISSIONS_WRITE_ONLY  // const
  public    static  HoudiniEngineUnity.HAPI_PermissionsHAPI_PERMISSIONS_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_RampType
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_RampTypeHAPI_RAMPTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_RampTypeHAPI_RAMPTYPE_FLOAT  // const
  public    static  HoudiniEngineUnity.HAPI_RampTypeHAPI_RAMPTYPE_COLOR  // const
  public    static  HoudiniEngineUnity.HAPI_RampTypeHAPI_RAMPTYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ParmType
TYPE:  sealed struct
TOKEN: 0x2000073
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_INT  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_MULTIPARMLIST  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_TOGGLE  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_BUTTON  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_FLOAT  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_COLOR  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_STRING  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_PATH_FILE  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_PATH_FILE_GEO  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_PATH_FILE_IMAGE  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_NODE  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_FOLDERLIST  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_FOLDERLIST_RADIO  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_FOLDER  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_LABEL  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_SEPARATOR  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_PATH_FILE_DIR  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_MAX  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_INT_START  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_INT_END  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_FLOAT_START  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_FLOAT_END  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_STRING_START  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_STRING_END  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_PATH_START  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_PATH_END  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_NODE_START  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_NODE_END  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_CONTAINER_START  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_CONTAINER_END  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_NONVALUE_START  // const
  public    static  HoudiniEngineUnity.HAPI_ParmTypeHAPI_PARMTYPE_NONVALUE_END  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PrmScriptType
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_INT  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_FLOAT  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_ANGLE  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_STRING  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_FILE  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_DIRECTORY  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_IMAGE  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_GEOMETRY  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_TOGGLE  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_BUTTON  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_VECTOR2  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_VECTOR3  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_VECTOR4  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_INTVECTOR2  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_INTVECTOR3  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_INTVECTOR4  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_UV  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_UVW  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_DIR  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_COLOR  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_COLOR4  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_HUECIRCLE  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_OPPATH  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_OPLIST  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_OBJECT  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_OBJECTLIST  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_RENDER  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_SEPARATOR  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_GEOMETRY_DATA  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_KEY_VALUE_DICT  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_LABEL  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_RGBAMASK  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_ORDINAL  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_RAMP_FLT  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_RAMP_RGB  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_FLOAT_LOG  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_INT_LOG  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_DATA  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_FLOAT_MINMAX  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_INT_MINMAX  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_INT_STARTEND  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_BUTTONSTRIP  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_ICONSTRIP  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_GROUPRADIO  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_GROUPCOLLAPSIBLE  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_GROUPSIMPLE  // const
  public    static  HoudiniEngineUnity.HAPI_PrmScriptTypeHAPI_PRM_SCRIPT_TYPE_GROUP  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ChoiceListType
TYPE:  sealed struct
TOKEN: 0x2000075
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_ChoiceListTypeHAPI_CHOICELISTTYPE_NONE  // const
  public    static  HoudiniEngineUnity.HAPI_ChoiceListTypeHAPI_CHOICELISTTYPE_NORMAL  // const
  public    static  HoudiniEngineUnity.HAPI_ChoiceListTypeHAPI_CHOICELISTTYPE_MINI  // const
  public    static  HoudiniEngineUnity.HAPI_ChoiceListTypeHAPI_CHOICELISTTYPE_REPLACE  // const
  public    static  HoudiniEngineUnity.HAPI_ChoiceListTypeHAPI_CHOICELISTTYPE_TOGGLE  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PresetType
TYPE:  sealed struct
TOKEN: 0x2000076
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PresetTypeHAPI_PRESETTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_PresetTypeHAPI_PRESETTYPE_BINARY  // const
  public    static  HoudiniEngineUnity.HAPI_PresetTypeHAPI_PRESETTYPE_IDX  // const
  public    static  HoudiniEngineUnity.HAPI_PresetTypeHAPI_PRESETTYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_NodeType
TYPE:  sealed struct
TOKEN: 0x2000077
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_ANY  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_NONE  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_OBJ  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_SOP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_CHOP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_ROP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_SHOP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_COP2  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_VOP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_DOP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_TOP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_COP  // const
  public    static  HoudiniEngineUnity.HAPI_NodeTypeHAPI_NODETYPE_LOP  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_NodeFlags
TYPE:  sealed struct
TOKEN: 0x2000078
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_ANY  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_NONE  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_DISPLAY  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_RENDER  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_TEMPLATED  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_LOCKED  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_EDITABLE  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_BYPASS  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_NETWORK  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_OBJ_GEOMETRY  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_OBJ_CAMERA  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_OBJ_LIGHT  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_OBJ_SUBNET  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_SOP_CURVE  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_SOP_GUIDE  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_TOP_NONSCHEDULER  // const
  public    static  HoudiniEngineUnity.HAPI_NodeFlagsHAPI_NODEFLAGS_NON_BYPASS  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_GroupType
TYPE:  sealed struct
TOKEN: 0x2000079
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_GroupTypeHAPI_GROUPTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_GroupTypeHAPI_GROUPTYPE_POINT  // const
  public    static  HoudiniEngineUnity.HAPI_GroupTypeHAPI_GROUPTYPE_PRIM  // const
  public    static  HoudiniEngineUnity.HAPI_GroupTypeHAPI_GROUPTYPE_EDGE  // const
  public    static  HoudiniEngineUnity.HAPI_GroupTypeHAPI_GROUPTYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_AttributeOwner
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_AttributeOwnerHAPI_ATTROWNER_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeOwnerHAPI_ATTROWNER_VERTEX  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeOwnerHAPI_ATTROWNER_POINT  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeOwnerHAPI_ATTROWNER_PRIM  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeOwnerHAPI_ATTROWNER_DETAIL  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeOwnerHAPI_ATTROWNER_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_CurveType
TYPE:  sealed struct
TOKEN: 0x200007B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_CurveTypeHAPI_CURVETYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_CurveTypeHAPI_CURVETYPE_LINEAR  // const
  public    static  HoudiniEngineUnity.HAPI_CurveTypeHAPI_CURVETYPE_NURBS  // const
  public    static  HoudiniEngineUnity.HAPI_CurveTypeHAPI_CURVETYPE_BEZIER  // const
  public    static  HoudiniEngineUnity.HAPI_CurveTypeHAPI_CURVETYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_InputCurveMethod
TYPE:  sealed struct
TOKEN: 0x200007C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_InputCurveMethodHAPI_CURVEMETHOD_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_InputCurveMethodHAPI_CURVEMETHOD_CVS  // const
  public    static  HoudiniEngineUnity.HAPI_InputCurveMethodHAPI_CURVEMETHOD_BREAKPOINTS  // const
  public    static  HoudiniEngineUnity.HAPI_InputCurveMethodHAPI_CURVEMETHOD_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_InputCurveParameterization
TYPE:  sealed struct
TOKEN: 0x200007D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_InputCurveParameterizationHAPI_CURVEPARAMETERIZATION_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_InputCurveParameterizationHAPI_CURVEPARAMETERIZATION_UNIFORM  // const
  public    static  HoudiniEngineUnity.HAPI_InputCurveParameterizationHAPI_CURVEPARAMETERIZATION_CHORD  // const
  public    static  HoudiniEngineUnity.HAPI_InputCurveParameterizationHAPI_CURVEPARAMETERIZATION_CENTRIPETAL  // const
  public    static  HoudiniEngineUnity.HAPI_InputCurveParameterizationHAPI_CURVEPARAMETERIZATION_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_VolumeType
TYPE:  sealed struct
TOKEN: 0x200007E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_VolumeTypeHAPI_VOLUMETYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeTypeHAPI_VOLUMETYPE_HOUDINI  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeTypeHAPI_VOLUMETYPE_VDB  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeTypeHAPI_VOLUMETYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_VolumeVisualType
TYPE:  sealed struct
TOKEN: 0x200007F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_VolumeVisualTypeHAPI_VOLUMEVISTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeVisualTypeHAPI_VOLUMEVISTYPE_SMOKE  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeVisualTypeHAPI_VOLUMEVISTYPE_RAINBOW  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeVisualTypeHAPI_VOLUMEVISTYPE_ISO  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeVisualTypeHAPI_VOLUMEVISTYPE_INVISIBLE  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeVisualTypeHAPI_VOLUMEVISTYPE_HEIGHTFIELD  // const
  public    static  HoudiniEngineUnity.HAPI_VolumeVisualTypeHAPI_VOLUMEVISTYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_StorageType
TYPE:  sealed struct
TOKEN: 0x2000080
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT64  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_FLOAT  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_FLOAT64  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_STRING  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_UINT8  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT8  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT16  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_DICTIONARY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT64_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_FLOAT_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_FLOAT64_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_STRING_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_UINT8_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT8_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_INT16_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_DICTIONARY_ARRAY  // const
  public    static  HoudiniEngineUnity.HAPI_StorageTypeHAPI_STORAGETYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_AttributeTypeInfo
TYPE:  sealed struct
TOKEN: 0x2000081
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_NONE  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_POINT  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_HPOINT  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_VECTOR  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_NORMAL  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_COLOR  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_QUATERNION  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_MATRIX3  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_MATRIX  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_ST  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_HIDDEN  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_BOX2  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_BOX  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_TEXTURE  // const
  public    static  HoudiniEngineUnity.HAPI_AttributeTypeInfoHAPI_ATTRIBUTE_TYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_GeoType
TYPE:  sealed struct
TOKEN: 0x2000082
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_DEFAULT  // const
  public    static  HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_INTERMEDIATE  // const
  public    static  HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_INPUT  // const
  public    static  HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_CURVE  // const
  public    static  HoudiniEngineUnity.HAPI_GeoType HAPI_GEOTYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PartType
TYPE:  sealed struct
TOKEN: 0x2000083
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_MESH  // const
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_CURVE  // const
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_VOLUME  // const
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_INSTANCER  // const
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_BOX  // const
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_SPHERE  // const
  public    static  HoudiniEngineUnity.HAPI_PartTypeHAPI_PARTTYPE_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_InputType
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_InputTypeHAPI_INPUT_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_InputTypeHAPI_INPUT_TRANSFORM  // const
  public    static  HoudiniEngineUnity.HAPI_InputTypeHAPI_INPUT_GEOMETRY  // const
  public    static  HoudiniEngineUnity.HAPI_InputTypeHAPI_INPUT_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_CurveOrders
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_CurveOrdersHAPI_CURVE_ORDER_VARYING  // const
  public    static  HoudiniEngineUnity.HAPI_CurveOrdersHAPI_CURVE_ORDER_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_CurveOrdersHAPI_CURVE_ORDER_LINEAR  // const
  public    static  HoudiniEngineUnity.HAPI_CurveOrdersHAPI_CURVE_ORDER_QUADRATIC  // const
  public    static  HoudiniEngineUnity.HAPI_CurveOrdersHAPI_CURVE_ORDER_CUBIC  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_TransformComponent
TYPE:  sealed struct
TOKEN: 0x2000086
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_TX  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_TY  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_TZ  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_RX  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_RY  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_RZ  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_QX  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_QY  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_QZ  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_QW  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_SX  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_SY  // const
  public    static  HoudiniEngineUnity.HAPI_TransformComponentHAPI_TRANSFORM_SZ  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_RSTOrder
TYPE:  sealed struct
TOKEN: 0x2000087
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_RSTOrderHAPI_TRS  // const
  public    static  HoudiniEngineUnity.HAPI_RSTOrderHAPI_TSR  // const
  public    static  HoudiniEngineUnity.HAPI_RSTOrderHAPI_RTS  // const
  public    static  HoudiniEngineUnity.HAPI_RSTOrderHAPI_RST  // const
  public    static  HoudiniEngineUnity.HAPI_RSTOrderHAPI_STR  // const
  public    static  HoudiniEngineUnity.HAPI_RSTOrderHAPI_SRT  // const
  public    static  HoudiniEngineUnity.HAPI_RSTOrderHAPI_RSTORDER_DEFAULT  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_XYZOrder
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_XYZOrderHAPI_XYZ  // const
  public    static  HoudiniEngineUnity.HAPI_XYZOrderHAPI_XZY  // const
  public    static  HoudiniEngineUnity.HAPI_XYZOrderHAPI_YXZ  // const
  public    static  HoudiniEngineUnity.HAPI_XYZOrderHAPI_YZX  // const
  public    static  HoudiniEngineUnity.HAPI_XYZOrderHAPI_ZXY  // const
  public    static  HoudiniEngineUnity.HAPI_XYZOrderHAPI_ZYX  // const
  public    static  HoudiniEngineUnity.HAPI_XYZOrderHAPI_XYZORDER_DEFAULT  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ImageDataFormat
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_UNKNOWN  // const
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_INT8  // const
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_INT16  // const
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_INT32  // const
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_FLOAT16  // const
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_FLOAT32  // const
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_MAX  // const
  public    static  HoudiniEngineUnity.HAPI_ImageDataFormatHAPI_IMAGE_DATA_DEFAULT  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ImagePacking
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_UNKNOWN  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_SINGLE  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_DUAL  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_RGB  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_BGR  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_RGBA  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_ABGR  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_MAX  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_DEFAULT3  // const
  public    static  HoudiniEngineUnity.HAPI_ImagePackingHAPI_IMAGE_PACKING_DEFAULT4  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_EnvIntType
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_VERSION_HOUDINI_MAJOR  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_VERSION_HOUDINI_MINOR  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_VERSION_HOUDINI_BUILD  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_VERSION_HOUDINI_PATCH  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_VERSION_HOUDINI_ENGINE_MAJOR  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_VERSION_HOUDINI_ENGINE_MINOR  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_VERSION_HOUDINI_ENGINE_API  // const
  public    static  HoudiniEngineUnity.HAPI_EnvIntTypeHAPI_ENVINT_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_SessionEnvIntType
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_SessionEnvIntTypeHAPI_SESSIONENVINT_INVALID  // const
  public    static  HoudiniEngineUnity.HAPI_SessionEnvIntTypeHAPI_SESSIONENVINT_LICENSE  // const
  public    static  HoudiniEngineUnity.HAPI_SessionEnvIntTypeHAPI_SESSIONENVINT_MAX  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_CacheProperty
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_CachePropertyHAPI_CACHEPROP_CURRENT  // const
  public    static  HoudiniEngineUnity.HAPI_CachePropertyHAPI_CACHEPROP_HAS_MIN  // const
  public    static  HoudiniEngineUnity.HAPI_CachePropertyHAPI_CACHEPROP_MIN  // const
  public    static  HoudiniEngineUnity.HAPI_CachePropertyHAPI_CACHEPROP_HAS_MAX  // const
  public    static  HoudiniEngineUnity.HAPI_CachePropertyHAPI_CACHEPROP_MAX  // const
  public    static  HoudiniEngineUnity.HAPI_CachePropertyHAPI_CACHEPROP_CULL_LEVEL  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_HeightFieldSampling
TYPE:  sealed struct
TOKEN: 0x200008E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_HeightFieldSamplingHAPI_HEIGHTFIELD_SAMPLING_CENTER  // const
  public    static  HoudiniEngineUnity.HAPI_HeightFieldSamplingHAPI_HEIGHTFIELD_SAMPLING_CORNER  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PDG_State
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PDG_StateHAPI_PDG_STATE_READY  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_StateHAPI_PDG_STATE_COOKING  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_StateHAPI_PDG_STATE_MAX  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_StateHAPI_PDG_STATE_MAX_READY_STATE  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PDG_EventType
TYPE:  sealed struct
TOKEN: 0x2000090
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NULL  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_ADD  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_REMOVE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_STATE_CHANGE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_ADD_DEP  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_REMOVE_DEP  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_ADD_PARENT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_REMOVE_PARENT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_CLEAR  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_COOK_ERROR  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_COOK_WARNING  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_COOK_COMPLETE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_DIRTY_START  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_DIRTY_STOP  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_DIRTY_ALL  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_UI_SELECT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_CREATE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_REMOVE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_RENAME  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_CONNECT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_DISCONNECT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_FIRST_COOK  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_SET_INT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_SET_FLOAT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_SET_STRING  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_SET_FILE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_SET_DICT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_SET_PYOBJECT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_SET_GEOMETRY  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_MERGE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_RESULT  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_PRIORITY  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_COOK_START  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_ADD_STATIC_ANCESTOR  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_REMOVE_STATIC_ANCESTOR  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_PROGRESS_UPDATE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_BATCH_ITEM_INITIALIZED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_ALL  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_LOG  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_SCHEDULER_ADDED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_SCHEDULER_REMOVED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_SET_SCHEDULER  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_SERVICE_MANAGER_ALL  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_COOKED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_NODE_GENERATED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_EVENT_WORKITEM_FRAME  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_EventTypeHAPI_PDG_CONTEXT_EVENTS  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PDG_WorkItemState
TYPE:  sealed struct
TOKEN: 0x2000091
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_UNDEFINED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_UNCOOKED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_WAITING  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_SCHEDULED  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_COOKING  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_COOKED_SUCCESS  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_COOKED_CACHE  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_COOKED_FAIL  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_COOKED_CANCEL  // const
  public    static  HoudiniEngineUnity.HAPI_PDG_WorkItemStateHAPI_PDG_WORKITEM_DIRTY  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_TCP_PortType
TYPE:  sealed struct
TOKEN: 0x2000092
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_TCP_PortTypeHAPI_TCP_PORT_ANY  // const
  public    static  HoudiniEngineUnity.HAPI_TCP_PortTypeHAPI_TCP_PORT_RANGE  // const
  public    static  HoudiniEngineUnity.HAPI_TCP_PortTypeHAPI_TCP_PORT_LIST  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType
TYPE:  sealed struct
TOKEN: 0x2000093
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferTypeHAPI_THRIFT_SHARED_MEMORY_FIXED_LENGTH_BUFFER  // const
  public    static  HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferTypeHAPI_THRIFT_SHARED_MEMORY_RING_BUFFER  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HAPIFunctions
TYPE:  static class
TOKEN: 0x2000094
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_Transform
TYPE:  sealed struct
TOKEN: 0x2000095
SIZE:  0x38
FIELDS:
  public            System.Single[]                 position  // 0x10
  public            System.Single[]                 rotationQuaternion  // 0x18
  public            System.Single[]                 scale  // 0x20
  public            System.Single[]                 shear  // 0x28
  public            HoudiniEngineUnity.HAPI_RSTOrderrstOrder  // 0x30
METHODS:
  RVA=0x09D69C78  token=0x60004D3  System.Void .ctor(System.Boolean initializeFields)
  RVA=0x09D69BB8  token=0x60004D4  System.Void Init()
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_TransformEuler
TYPE:  sealed struct
TOKEN: 0x2000096
SIZE:  0x38
FIELDS:
  public            System.Single[]                 position  // 0x10
  public            System.Single[]                 rotationEuler  // 0x18
  public            System.Single[]                 scale  // 0x20
  public            System.Single[]                 shear  // 0x28
  public            HoudiniEngineUnity.HAPI_XYZOrderrotationOrder  // 0x30
  public            HoudiniEngineUnity.HAPI_RSTOrderrstOrder  // 0x34
METHODS:
  RVA=0x09D69AF0  token=0x60004D5  System.Void .ctor(System.Boolean initializeFields)
  RVA=0x09D69A4C  token=0x60004D6  System.Void Init()
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_Session
TYPE:  sealed struct
TOKEN: 0x2000097
SIZE:  0x20
FIELDS:
  public            HoudiniEngineUnity.HAPI_SessionTypetype  // 0x10
  public            System.Int64                    id  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_SessionInfo
TYPE:  sealed struct
TOKEN: 0x2000098
SIZE:  0x40
FIELDS:
  public            System.Int32                    connectionCount  // 0x10
  public            HoudiniEngineUnity.HAPI_TCP_PortTypeportType  // 0x14
  public            System.Int32                    minPort  // 0x18
  public            System.Int32                    maxPort  // 0x1c
  public            System.Int32[]                  ports  // 0x20
  public            HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferTypesharedMemoryBufferType  // 0x28
  public            System.Int64                    sharedMemoryBufferSize  // 0x30
  public            System.Boolean                  enableSharedMemoryDataTransfer  // 0x38
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ThriftServerOptions
TYPE:  sealed struct
TOKEN: 0x2000099
SIZE:  0x28
FIELDS:
  public            System.Boolean                  autoClose  // 0x10
  public            System.Single                   timeoutMs  // 0x14
  public            HoudiniEngineUnity.HAPI_StatusVerbosityverbosity  // 0x18
  public            HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferTypesharedMemoryBufferType  // 0x1c
  public            System.Int64                    sharedMemoryBufferSize  // 0x20
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_TimelineOptions
TYPE:  sealed struct
TOKEN: 0x200009A
SIZE:  0x28
FIELDS:
  public            System.Double                   fps  // 0x10
  public            System.Double                   startTime  // 0x18
  public            System.Double                   endTime  // 0x20
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_AssetInfo
TYPE:  sealed struct
TOKEN: 0x200009B
SIZE:  0x50
FIELDS:
  public            System.Int32                    nodeId  // 0x10
  public            System.Int32                    objectNodeId  // 0x14
  public            System.Boolean                  hasEverCooked  // 0x18
  public            System.Int32                    nameSH  // 0x1c
  public            System.Int32                    labelSH  // 0x20
  public            System.Int32                    filePathSH  // 0x24
  public            System.Int32                    versionSH  // 0x28
  public            System.Int32                    fullOpNameSH  // 0x2c
  public            System.Int32                    helpTextSH  // 0x30
  public            System.Int32                    helpURLSH  // 0x34
  public            System.Int32                    objectCount  // 0x38
  public            System.Int32                    handleCount  // 0x3c
  public            System.Int32                    transformInputCount  // 0x40
  public            System.Int32                    geoInputCount  // 0x44
  public            System.Int32                    geoOutputCount  // 0x48
  public            System.Boolean                  haveObjectsChanged  // 0x4c
  public            System.Boolean                  haveMaterialsChanged  // 0x4d
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_CookOptions
TYPE:  sealed struct
TOKEN: 0x200009C
SIZE:  0x40
FIELDS:
  public            System.Boolean                  splitGeosByGroup  // 0x10
  public            System.Int32                    splitGroupSH  // 0x14
  public            System.Boolean                  splitGeosByAttribute  // 0x18
  public            System.Int32                    splitAttrSH  // 0x1c
  public            System.Int32                    maxVerticesPerPrimitive  // 0x20
  public            System.Boolean                  refineCurveToLinear  // 0x24
  public            System.Single                   curveRefineLOD  // 0x28
  public            System.Boolean                  clearErrorsAndWarnings  // 0x2c
  public            System.Boolean                  cookTemplatedGeos  // 0x2d
  public            System.Boolean                  splitPointsByVertexAttributes  // 0x2e
  public            HoudiniEngineUnity.HAPI_PackedPrimInstancingModepackedPrimInstancingMode  // 0x30
  public            System.Boolean                  handleBoxPartTypes  // 0x34
  public            System.Boolean                  handleSpherePartTypes  // 0x35
  public            System.Boolean                  checkPartChanges  // 0x36
  public            System.Boolean                  cacheMeshTopology  // 0x37
  public            System.Boolean                  preferOutputNodes  // 0x38
  public            System.Int32                    extraFlags  // 0x3c
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_NodeInfo
TYPE:  sealed struct
TOKEN: 0x200009D
SIZE:  0x54
FIELDS:
  public            System.Int32                    id  // 0x10
  public            System.Int32                    parentId  // 0x14
  public            System.Int32                    nameSH  // 0x18
  public            HoudiniEngineUnity.HAPI_NodeTypetype  // 0x1c
  public            System.Boolean                  isValid  // 0x20
  public            System.Int32                    totalCookCount  // 0x24
  public            System.Int32                    uniqueHoudiniNodeId  // 0x28
  public            System.Int32                    internalNodePathSH  // 0x2c
  public            System.Int32                    parmCount  // 0x30
  public            System.Int32                    parmIntValueCount  // 0x34
  public            System.Int32                    parmFloatValueCount  // 0x38
  public            System.Int32                    parmStringValueCount  // 0x3c
  public            System.Int32                    parmChoiceCount  // 0x40
  public            System.Int32                    childNodeCount  // 0x44
  public            System.Int32                    inputCount  // 0x48
  public            System.Int32                    outputCount  // 0x4c
  public            System.Boolean                  createdPostAssetLoad  // 0x50
  public            System.Boolean                  isTimeDependent  // 0x51
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ParmInfo
TYPE:  sealed struct
TOKEN: 0x200009E
SIZE:  0xA4
FIELDS:
  public            System.Int32                    id  // 0x10
  public            System.Int32                    parentId  // 0x14
  public            System.Int32                    childIndex  // 0x18
  public            HoudiniEngineUnity.HAPI_ParmTypetype  // 0x1c
  public            HoudiniEngineUnity.HAPI_PrmScriptTypescriptType  // 0x20
  public            System.Int32                    typeInfoSH  // 0x24
  public            HoudiniEngineUnity.HAPI_Permissionspermissions  // 0x28
  public            System.Int32                    tagCount  // 0x2c
  public            System.Int32                    size  // 0x30
  public            HoudiniEngineUnity.HAPI_ChoiceListTypechoiceListType  // 0x34
  public            System.Int32                    choiceCount  // 0x38
  public            System.Int32                    nameSH  // 0x3c
  public            System.Int32                    labelSH  // 0x40
  public            System.Int32                    templateNameSH  // 0x44
  public            System.Int32                    helpSH  // 0x48
  public            System.Boolean                  hasMin  // 0x4c
  public            System.Boolean                  hasMax  // 0x4d
  public            System.Boolean                  hasUIMin  // 0x4e
  public            System.Boolean                  hasUIMax  // 0x4f
  public            System.Single                   min  // 0x50
  public            System.Single                   max  // 0x54
  public            System.Single                   UIMin  // 0x58
  public            System.Single                   UIMax  // 0x5c
  public            System.Boolean                  invisible  // 0x60
  public            System.Boolean                  disabled  // 0x61
  public            System.Boolean                  spare  // 0x62
  public            System.Boolean                  joinNext  // 0x63
  public            System.Boolean                  labelNone  // 0x64
  public            System.Int32                    intValuesIndex  // 0x68
  public            System.Int32                    floatValuesIndex  // 0x6c
  public            System.Int32                    stringValuesIndex  // 0x70
  public            System.Int32                    choiceIndex  // 0x74
  public            HoudiniEngineUnity.HAPI_NodeTypeinputNodeType  // 0x78
  public            HoudiniEngineUnity.HAPI_NodeFlagsinputNodeFlag  // 0x7c
  public            System.Boolean                  isChildOfMultiParm  // 0x80
  public            System.Int32                    instanceNum  // 0x84
  public            System.Int32                    instanceLength  // 0x88
  public            System.Int32                    instanceCount  // 0x8c
  public            System.Int32                    instanceStartOffset  // 0x90
  public            HoudiniEngineUnity.HAPI_RampTyperampType  // 0x94
  public            System.Int32                    visibilityConditionSH  // 0x98
  public            System.Int32                    disabledConditionSH  // 0x9c
  public            System.Boolean                  useMenuItemTokenAsValue  // 0xa0
METHODS:
  RVA=0x09D69724  token=0x60004D7  System.Boolean isInt()
  RVA=0x09D69710  token=0x60004D8  System.Boolean isFloat()
  RVA=0x09D69780  token=0x60004D9  System.Boolean isString()
  RVA=0x09D69768  token=0x60004DA  System.Boolean isPath()
  RVA=0x09D69744  token=0x60004DB  System.Boolean isNode()
  RVA=0x09D69754  token=0x60004DC  System.Boolean isNonValue()
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ParmChoiceInfo
TYPE:  sealed struct
TOKEN: 0x200009F
SIZE:  0x1C
FIELDS:
  public            System.Int32                    parentParmId  // 0x10
  public            System.Int32                    labelSH  // 0x14
  public            System.Int32                    valueSH  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_HandleInfo
TYPE:  sealed struct
TOKEN: 0x20000A0
SIZE:  0x1C
FIELDS:
  public            System.Int32                    nameSH  // 0x10
  public            System.Int32                    typeNameSH  // 0x14
  public            System.Int32                    bindingsCount  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_HandleBindingInfo
TYPE:  sealed struct
TOKEN: 0x20000A1
SIZE:  0x20
FIELDS:
  public            System.Int32                    handleParmNameSH  // 0x10
  public            System.Int32                    assetParmNameSH  // 0x14
  public            System.Int32                    assetParmId  // 0x18
  public            System.Int32                    assetParmIndex  // 0x1c
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ObjectInfo
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x2C
FIELDS:
  public            System.Int32                    nameSH  // 0x10
  public            System.Int32                    objectInstancePathSH  // 0x14
  public            System.Boolean                  hasTransformChanged  // 0x18
  public            System.Boolean                  haveGeosChanged  // 0x19
  public            System.Boolean                  isVisible  // 0x1a
  public            System.Boolean                  isInstancer  // 0x1b
  public            System.Boolean                  isInstanced  // 0x1c
  public            System.Int32                    geoCount  // 0x20
  public            System.Int32                    nodeId  // 0x24
  public            System.Int32                    objectToInstanceId  // 0x28
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_GeoInfo
TYPE:  sealed struct
TOKEN: 0x20000A3
SIZE:  0x34
FIELDS:
  public            HoudiniEngineUnity.HAPI_GeoType type  // 0x10
  public            System.Int32                    nameSH  // 0x14
  public            System.Int32                    nodeId  // 0x18
  public            System.Boolean                  isEditable  // 0x1c
  public            System.Boolean                  isTemplated  // 0x1d
  public            System.Boolean                  isDisplayGeo  // 0x1e
  public            System.Boolean                  hasGeoChanged  // 0x1f
  public            System.Boolean                  hasMaterialChanged  // 0x20
  public            System.Int32                    pointGroupCount  // 0x24
  public            System.Int32                    primitiveGroupCount  // 0x28
  public            System.Int32                    edgeGroupCount  // 0x2c
  public            System.Int32                    partCount  // 0x30
METHODS:
  RVA=0x09D696AC  token=0x60004DD  System.Int32 getGroupCountByType(HoudiniEngineUnity.HAPI_GroupType type)
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PartInfo
TYPE:  sealed struct
TOKEN: 0x20000A4
SIZE:  0x40
FIELDS:
  public            System.Int32                    id  // 0x10
  public            System.Int32                    nameSH  // 0x14
  public            HoudiniEngineUnity.HAPI_PartTypetype  // 0x18
  public            System.Int32                    faceCount  // 0x1c
  public            System.Int32                    vertexCount  // 0x20
  public            System.Int32                    pointCount  // 0x24
  public            System.Int32[]                  attributeCounts  // 0x28
  public            System.Boolean                  isInstanced  // 0x30
  public            System.Int32                    instancedPartCount  // 0x34
  public            System.Int32                    instanceCount  // 0x38
  public            System.Boolean                  hasChanged  // 0x3c
PROPERTIES:
  pointAttributeCount  get=0x09D69810  set=0x09D6993C
  primitiveAttributeCount  get=0x09D69844  set=0x09D6997C
  vertexAttributeCount  get=0x09D69878  set=0x09D699BC
  detailAttributeCount  get=0x09D697DC  set=0x09D698FC
METHODS:
  RVA=0x09D697A0  token=0x60004DE  System.Int32 getElementCountByAttributeOwner(HoudiniEngineUnity.HAPI_AttributeOwner owner)
  RVA=0x09D697C8  token=0x60004DF  System.Int32 getElementCountByGroupType(HoudiniEngineUnity.HAPI_GroupType type)
  RVA=0x09D698AC  token=0x60004E0  System.Void init()
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_AttributeInfo
TYPE:  sealed struct
TOKEN: 0x20000A5
SIZE:  0x38
FIELDS:
  public            System.Boolean                  exists  // 0x10
  public            HoudiniEngineUnity.HAPI_AttributeOwnerowner  // 0x14
  public            HoudiniEngineUnity.HAPI_StorageTypestorage  // 0x18
  public            HoudiniEngineUnity.HAPI_AttributeOwneroriginalOwner  // 0x1c
  public            System.Int32                    count  // 0x20
  public            System.Int32                    tupleSize  // 0x24
  public            System.Int64                    totalArrayElements  // 0x28
  public            HoudiniEngineUnity.HAPI_AttributeTypeInfotypeInfo  // 0x30
METHODS:
  RVA=0x04DA2730  token=0x60004E9  System.Void .ctor(System.String ignored)
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_MaterialInfo
TYPE:  sealed struct
TOKEN: 0x20000A6
SIZE:  0x18
FIELDS:
  public            System.Int32                    nodeId  // 0x10
  public            System.Boolean                  exists  // 0x14
  public            System.Boolean                  hasChanged  // 0x15
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ImageFileFormat
TYPE:  sealed struct
TOKEN: 0x20000A7
SIZE:  0x1C
FIELDS:
  public            System.Int32                    nameSH  // 0x10
  public            System.Int32                    descriptionSH  // 0x14
  public            System.Int32                    defaultExtensionSH  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_ImageInfo
TYPE:  sealed struct
TOKEN: 0x20000A8
SIZE:  0x30
FIELDS:
  public            System.Int32                    imageFileFormatNameSH  // 0x10
  public            System.Int32                    xRes  // 0x14
  public            System.Int32                    yRes  // 0x18
  public            HoudiniEngineUnity.HAPI_ImageDataFormatdataFormat  // 0x1c
  public            System.Boolean                  interleaved  // 0x20
  public            HoudiniEngineUnity.HAPI_ImagePackingpacking  // 0x24
  public            System.Double                   gamma  // 0x28
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_Keyframe
TYPE:  sealed struct
TOKEN: 0x20000A9
SIZE:  0x20
FIELDS:
  public            System.Single                   time  // 0x10
  public            System.Single                   value  // 0x14
  public            System.Single                   inTangent  // 0x18
  public            System.Single                   outTangent  // 0x1c
METHODS:
  RVA=0x04D88AD0  token=0x60004EA  System.Void .ctor(System.Single t, System.Single v, System.Single in_tangent, System.Single out_tangent)
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_VolumeInfo
TYPE:  sealed struct
TOKEN: 0x20000AA
SIZE:  0x78
FIELDS:
  public            System.Int32                    nameSH  // 0x10
  public            HoudiniEngineUnity.HAPI_VolumeTypetype  // 0x14
  public            System.Int32                    xLength  // 0x18
  public            System.Int32                    yLength  // 0x1c
  public            System.Int32                    zLength  // 0x20
  public            System.Int32                    minX  // 0x24
  public            System.Int32                    minY  // 0x28
  public            System.Int32                    minZ  // 0x2c
  public            System.Int32                    tupleSize  // 0x30
  public            HoudiniEngineUnity.HAPI_StorageTypestorage  // 0x34
  public            System.Int32                    tileSize  // 0x38
  public            HoudiniEngineUnity.HAPI_Transformtransform  // 0x40
  public            System.Boolean                  hasTaper  // 0x68
  public            System.Single                   xTaper  // 0x6c
  public            System.Single                   yTaper  // 0x70
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_VolumeTileInfo
TYPE:  sealed struct
TOKEN: 0x20000AB
SIZE:  0x20
FIELDS:
  public            System.Int32                    minX  // 0x10
  public            System.Int32                    minY  // 0x14
  public            System.Int32                    minZ  // 0x18
  public            System.Boolean                  isValid  // 0x1c
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_VolumeVisualInfo
TYPE:  sealed struct
TOKEN: 0x20000AC
SIZE:  0x1C
FIELDS:
  public            HoudiniEngineUnity.HAPI_VolumeVisualTypetype  // 0x10
  public            System.Single                   iso  // 0x14
  public            System.Single                   density  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_CurveInfo
TYPE:  sealed struct
TOKEN: 0x20000AD
SIZE:  0x2C
FIELDS:
  public            HoudiniEngineUnity.HAPI_CurveTypecurveType  // 0x10
  public            System.Int32                    curveCount  // 0x14
  public            System.Int32                    vertexCount  // 0x18
  public            System.Int32                    knotCount  // 0x1c
  public            System.Boolean                  isPeriodic  // 0x20
  public            System.Boolean                  isRational  // 0x21
  public            System.Int32                    order  // 0x24
  public            System.Boolean                  hasKnots  // 0x28
  public            System.Boolean                  isClosed  // 0x29
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_InputCurveInfo
TYPE:  sealed struct
TOKEN: 0x20000AE
SIZE:  0x24
FIELDS:
  public            HoudiniEngineUnity.HAPI_CurveTypecurveType  // 0x10
  public            System.Int32                    order  // 0x14
  public            System.Boolean                  closed  // 0x18
  public            System.Boolean                  reverse  // 0x19
  public            HoudiniEngineUnity.HAPI_InputCurveMethodinputMethod  // 0x1c
  public            HoudiniEngineUnity.HAPI_InputCurveParameterizationbreakpointParameterization  // 0x20
METHODS:
  RVA=0x09D696C0  token=0x60004EB  System.Void FillData(HoudiniEngineUnity.HEU_InputCurveInfo curveInfo)
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_BoxInfo
TYPE:  sealed struct
TOKEN: 0x20000AF
SIZE:  0x28
FIELDS:
  public            System.Single[]                 center  // 0x10
  public            System.Single[]                 size  // 0x18
  public            System.Single[]                 rotation  // 0x20
METHODS:
  RVA=0x09D69624  token=0x60004EC  System.Void .ctor(System.Boolean initialize_fields)
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_SphereInfo
TYPE:  sealed struct
TOKEN: 0x20000B0
SIZE:  0x20
FIELDS:
  public            System.Single[]                 center  // 0x10
  public            System.Single                   radius  // 0x18
METHODS:
  RVA=0x09D699FC  token=0x60004ED  System.Void .ctor(System.Boolean initialize_fields)
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PDG_EventInfo
TYPE:  sealed struct
TOKEN: 0x20000B1
SIZE:  0x2C
FIELDS:
  public            System.Int32                    nodeId  // 0x10
  public            System.Int32                    workItemId  // 0x14
  public            System.Int32                    dependencyId  // 0x18
  public            System.Int32                    currentState  // 0x1c
  public            System.Int32                    lastState  // 0x20
  public            System.Int32                    eventType  // 0x24
  public            System.Int32                    msgSH  // 0x28
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PDG_WorkItemInfo
TYPE:  sealed struct
TOKEN: 0x20000B2
SIZE:  0x1C
FIELDS:
  public            System.Int32                    index  // 0x10
  public            System.Int32                    outputFileCount  // 0x14
  public            System.Int32                    nameSH  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_PDG_WorkItemOutputFile
TYPE:  sealed struct
TOKEN: 0x20000B3
SIZE:  0x20
FIELDS:
  public            System.Int32                    filePathSH  // 0x10
  public            System.Int32                    tagSH  // 0x14
  public            System.Int64                    hash  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_Viewport
TYPE:  sealed struct
TOKEN: 0x20000B4
SIZE:  0x28
FIELDS:
  public            System.Single[]                 position  // 0x10
  public            System.Single[]                 rotationQuaternion  // 0x18
  public            System.Single                   offset  // 0x20
METHODS:
  RVA=0x09D69DB8  token=0x60004EE  System.Void .ctor(System.Boolean initializeFields)
  RVA=0x09D69D40  token=0x60004EF  System.Void Init()
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_SessionSyncInfo
TYPE:  sealed struct
TOKEN: 0x20000B5
SIZE:  0x12
FIELDS:
  public            System.Boolean                  cookUsingHoudiniTime  // 0x10
  public            System.Boolean                  syncViewport  // 0x11
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HAPI_CompositorOptions
TYPE:  sealed struct
TOKEN: 0x20000B6
SIZE:  0x18
FIELDS:
  public            System.Int32                    maximumResolutionX  // 0x10
  public            System.Int32                    maximumResolutionY  // 0x14
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HARCImports
TYPE:  class
TOKEN: 0x20000B7
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60004F0  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HoudiniVersion
TYPE:  class
TOKEN: 0x20000B8
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    HOUDINI_MAJOR  // const
  public    static  System.Int32                    HOUDINI_MINOR  // const
  public    static  System.Int32                    HOUDINI_BUILD  // const
  public    static  System.Int32                    HOUDINI_PATCH  // const
  public    static  System.String                   HOUDINI_VERSION_STRING  // const
  public    static  System.Int32                    HOUDINI_ENGINE_MAJOR  // const
  public    static  System.Int32                    HOUDINI_ENGINE_MINOR  // const
  public    static  System.Int32                    HOUDINI_ENGINE_API  // const
  public    static  System.Int32                    UNITY_PLUGIN_VERSION  // const
  public    static  System.String                   HAPI_BIN_PATH  // const
  public    static  System.String                   HAPI_LIBRARY  // const
  public    static  System.String                   HARC_LIBRARY  // const
  public    static  System.String                   SIDEFX_SOFTWARE_REGISTRY  // const
METHODS:
  RVA=0x041E1670  token=0x60004F1  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HTC_APIConstants
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0x10
FIELDS:
  public    static  System.String                   HOUDINI_MASTER_SERVER_HOSTNAME  // const
  public    static  System.Int32                    HOUDINI_MASTER_QUERY_FOR_UNITY_PORT  // const
  public    static  System.Int32                    TCP_HEADER_SIZE  // const
  public    static  System.Int32                    MAX_ERROR_INFO_SIZE  // const
METHODS:
  RVA=0x041E1670  token=0x60004F2  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HTC_VERSION
TYPE:  class
TOKEN: 0x20000BA
SIZE:  0x10
FIELDS:
  public    static  System.String                   LIBRARY  // const
METHODS:
  RVA=0x041E1670  token=0x60004F3  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HoudiniConstsIdentifier
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x10
FIELDS:
  public    static  System.String                   HOUDINI_VERSION  // const
  public    static  System.String                   PROJECT_TAG  // const
METHODS:
  RVA=0x041E1670  token=0x60004F4  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HTC_APIFunctions
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D830A0  token=0x60004F5  System.Void InitHoudiniTcpClient(System.String address, System.Int32 port, System.Boolean use_ip, System.Text.StringBuilder error_msg)
  RVA=0x09D82F90  token=0x60004F6  System.Void GetHarsInfo(HoudiniEngineUnity.HarsInfo& harsInfo, System.Text.StringBuilder error_msg)
  RVA=0x09D83180  token=0x60004F7  System.Void UninitializeHoudiniTcpClient(System.Text.StringBuilder error_msg)
  RVA=0x041E1670  token=0x60004F8  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HarsInfo
TYPE:  sealed struct
TOKEN: 0x20000BD
SIZE:  0x20
FIELDS:
  public            System.String                   hars_address  // 0x10
  public            System.Int32                    port  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.GenericMessage
TYPE:  sealed struct
TOKEN: 0x20000BE
SIZE:  0x20
FIELDS:
  public            System.String                   protocol  // 0x10
  public            System.Object                   body  // 0x18
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.Logger
TYPE:  static class
TOKEN: 0x20000BF
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D84FDC  token=0x60004F9  System.Void LogError(System.String msg)
  RVA=0x09D85074  token=0x60004FA  System.Void LogWarning(System.String msg)
  RVA=0x09D85028  token=0x60004FB  System.Void LogInfo(System.String msg)
END_CLASS

CLASS: HoudiniEngineUnity.HTC_SessionManager
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Boolean                  disposedValue  // 0x10
  private   static  HoudiniEngineUnity.HTC_SessionManagerinstance_  // static @ 0x0
PROPERTIES:
  Instance  get=0x09D83D5C
METHODS:
  RVA=0x09D83500  token=0x60004FC  System.Void .ctor()
  RVA=0x09D833CC  token=0x60004FD  System.Void Finalize()
  RVA=0x09D832D8  token=0x60004FE  System.Void Dispose(System.Boolean disposing)
  RVA=0x09D83288  token=0x60004FF  System.Void Dispose()
  RVA=0x09D83440  token=0x6000500  System.Boolean GetOrRequestAvailableHARSInfo(System.String& harsAddress, System.Int32& harsPort, System.String& errorInfo)
  RVA=0x09D83220  token=0x6000501  System.Void Close()
END_CLASS

CLASS: HoudiniEngineUnity.HTC_Utils
TYPE:  static class
TOKEN: 0x20000C1
SIZE:  0x10
FIELDS:
  private   static readonly System.String                   CurrentUnityProjectPrefix  // static @ 0x0
METHODS:
  RVA=0x09D83DE0  token=0x6000503  System.String GetUnityPathFromSystemPath(System.String systemPath)
  RVA=0x09D83FE8  token=0x6000504  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HoudiniExtTools
TYPE:  static class
TOKEN: 0x20000C2
SIZE:  0x10
FIELDS:
  private   static readonly System.String                   LOCAL_IP_ADDRESS  // static @ 0x0
  private   static readonly System.String                   REMOTE_HOUDINI_SMB_PATH_PREFIX  // static @ 0x8
  private   static readonly System.String                   REMOTE_HOUDINI_DEBUG_SMB_PATH_PREFIX  // static @ 0x10
  public    static  System.String                   REMOTE_HOUDINI_HDA_SMB_URL  // const
  public    static  System.String                   REMOTE_HOUDINI_SMB_URL  // const
  public    static  System.String                   LINUX_MOUNT_DIR_PREFIX  // const
  public    static  System.String                   PROJECT_TAG  // const
PROPERTIES:
  DefaultSessionID  get=0x09D84F8C
METHODS:
  RVA=0x09D84BCC  token=0x6000505  System.Int64 GetSessionID(System.Int64 sessionId)
  RVA=0x09D84694  token=0x6000506  System.String GetDefaultDirectoryOnTheSMB()
  RVA=0x09D84710  token=0x6000507  System.String GetDefaultSessionDirectoryOnTheSMB(System.Int64 sessionId)
  RVA=0x09D84318  token=0x6000508  System.String ConvertLocalDirPathToSessionDirPath(System.String localPath, System.Boolean doMd5, System.Int64 sessionId)
  RVA=0x09D84220  token=0x6000509  System.String ConvertLocalDebugFilePathToSessionDebugFilePath(System.String localFilePath, System.Int64 sessionId)
  RVA=0x09D84448  token=0x600050A  System.String ConvertLocalFilePathToSessionFilePath(System.String localFilePath, System.Int64 sessionId)
  RVA=0x09D84C18  token=0x600050B  System.Boolean IsLocalPath(System.String path)
  RVA=0x09D84AC8  token=0x600050C  System.String GetMD5WithString(System.String str)
  RVA=0x09D847DC  token=0x600050D  System.String GetLocalIPv4()
  RVA=0x09D847B0  token=0x600050E  System.String GetHoudiniSMBUrl()
  RVA=0x09D84634  token=0x600050F  System.String GetDefaultCachePath(System.Int64 sessionId)
  RVA=0x09D846E4  token=0x6000510  System.String GetDefaultHdaPath()
  RVA=0x09D84E90  token=0x6000512  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HoudiniExtBridge
TYPE:  static class
TOKEN: 0x20000C3
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D840D4  token=0x6000513  System.Boolean GetParametersFromHDA(System.Byte[] hdaBytes, HoudiniEngineUnity.HEU_Parameters& parameters, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode>& inputNodes)
  RVA=0x09D84094  token=0x6000514  System.Void AddInputNodeToHoudiniAsset(HoudiniEngineUnity.HEU_HoudiniAsset asset, HoudiniEngineUnity.HEU_InputNode node)
  RVA=0x09D840B0  token=0x6000515  System.Void Clear(HoudiniEngineUnity.HEU_Parameters parameters)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ParameterData
TYPE:  sealed class
TOKEN: 0x20000C4
SIZE:  0x150
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.Int32                    _unityIndex  // 0x10
  public            System.String                   _name  // 0x18
  public            System.String                   _labelName  // 0x20
  public            System.String                   _help  // 0x28
  public            System.Collections.Generic.List<System.Int32>_childParameterIDs  // 0x30
  public            System.Int32                    _choiceValue  // 0x38
  public            System.Int32[]                  _intValues  // 0x40
  public            System.Single[]                 _floatValues  // 0x48
  public            System.String[]                 _stringValues  // 0x50
  public            System.Boolean                  _toggle  // 0x58
  public            UnityEngine.Color               _color  // 0x5c
  public            UnityEngine.Gradient            _gradient  // 0x70
  public            UnityEngine.AnimationCurve      _animCurve  // 0x78
  public            UnityEngine.GUIContent[]        _choiceLabels  // 0x80
  public            System.String[]                 _choiceStringValues  // 0x88
  public            System.Int32[]                  _choiceIntValues  // 0x90
  public            HoudiniEngineUnity.HAPI_ParmInfo_parmInfo  // 0x98
  public            System.Boolean                  _showChildren  // 0x12c
  public            System.String                   _fileTypeInfo  // 0x130
  public            System.Int32                    _folderListChildrenProcessed  // 0x138
  public            System.Int32                    _tabSelectedIndex  // 0x13c
  public            HoudiniEngineUnity.HEU_InputNode_paramInputNode  // 0x140
  public            System.Boolean                  _hasAssetPathTag  // 0x148
PROPERTIES:
  ParmID  get=0x04D862D0
  ParentID  get=0x04D86C60
  ChildIndex  get=0x04D88CD0
  ParmSize  get=0x04D8E580
  IntMin  get=0x09D70374
  IntMax  get=0x09D70364
  IntUIMin  get=0x09D70394
  IntUIMax  get=0x09D70384
  FloatMin  get=0x04D9DFB0
  FloatMax  get=0x04DA29C0
  FloatUIMin  get=0x04D8E2D0
  FloatUIMax  get=0x04DA29D0
METHODS:
  RVA=0x09D7020C  token=0x600051A  System.Boolean IsInt()
  RVA=0x09D701F4  token=0x600051B  System.Boolean IsFloat()
  RVA=0x09D70254  token=0x600051C  System.Boolean IsString()
  RVA=0x09D70224  token=0x600051D  System.Boolean IsPathFile()
  RVA=0x04DA2990  token=0x600051E  System.Boolean HasMin()
  RVA=0x04DA2980  token=0x600051F  System.Boolean HasMax()
  RVA=0x04DA29B0  token=0x6000520  System.Boolean HasUIMin()
  RVA=0x04DA29A0  token=0x6000521  System.Boolean HasUIMax()
  RVA=0x09D6FA6C  token=0x600052A  System.Boolean IsContainer()
  RVA=0x09D70218  token=0x600052B  System.Boolean IsMultiParam()
  RVA=0x09D7023C  token=0x600052C  System.Boolean IsRamp()
  RVA=0x09D701E8  token=0x600052D  System.Boolean IsFloatRamp()
  RVA=0x09D6FA54  token=0x600052E  System.Boolean IsColorRamp()
  RVA=0x09D70260  token=0x600052F  System.Boolean IsToggle()
  RVA=0x09D6FA60  token=0x6000530  System.Boolean IsColor()
  RVA=0x09D70200  token=0x6000531  System.Boolean IsFolder()
  RVA=0x09D6FA48  token=0x6000532  System.Boolean IsButton()
  RVA=0x09D7026C  token=0x6000533  UnityEngine.Vector3 ToVector3()
  RVA=0x04D8FE60  token=0x6000534  System.Boolean IsAssetPath()
  RVA=0x09D6FAB0  token=0x6000535  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ParameterData other)
  RVA=0x09D702EC  token=0x6000536  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ModifierActionWrapper
TYPE:  sealed struct
TOKEN: 0x20000C5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_ModifierActionWrapperMULTIPARM_INSERT  // const
  public    static  HoudiniEngineUnity.HEU_ModifierActionWrapperMULTIPARM_REMOVE  // const
  public    static  HoudiniEngineUnity.HEU_ModifierActionWrapperMULTIPARM_CLEAR  // const
  public    static  HoudiniEngineUnity.HEU_ModifierActionWrapperSET_FLOAT  // const
  public    static  HoudiniEngineUnity.HEU_ModifierActionWrapperSET_INT  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ParameterModifier
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x28
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.Int32                    _parameterIndex  // 0x10
  private           HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction_action  // 0x14
  private           System.Int32                    _instanceIndex  // 0x18
  private           System.Int32                    _modifierValue  // 0x1c
  private           System.Single                   _floatValue  // 0x20
  private           System.Int32                    _intValue  // 0x24
PROPERTIES:
  ParameterIndex  get=0x02B2E2D0  set=0x014F51F0
  Action  get=0x09D7067C  set=0x09D70688
  InstanceIndex  get=0x011EF5B0  set=0x011EF9B0
  ModifierValue  get=0x04D86310  set=0x04D86320
  FloatValue  get=0x011EC8A0  set=0x011EC9E0
  IntValue  get=0x011F0020  set=0x011F0030
METHODS:
  RVA=0x09D703A4  token=0x6000543  HoudiniEngineUnity.HEU_ParameterModifier GetNewModifier(HoudiniEngineUnity.HEU_ModifierActionWrapper action, System.Int32 parameterIndex, System.Int32 instanceIndex, System.Int32 modifierValue)
  RVA=0x09D703EC  token=0x6000544  HoudiniEngineUnity.HEU_ParameterModifier GetNewModifier(HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction action, System.Int32 parameterIndex, System.Int32 instanceIndex, System.Int32 modifierValue)
  RVA=0x09D70460  token=0x6000545  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ParameterModifier other)
  RVA=0x09D70648  token=0x6000546  HoudiniEngineUnity.HEU_ModifierActionWrapper ModifierAction_InternalToWrapper(HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction action)
  RVA=0x09D70648  token=0x6000547  HoudiniEngineUnity.HEU_ParameterModifier.ModifierAction ModifierAction_WrapperToInternal(HoudiniEngineUnity.HEU_ModifierActionWrapper action)
  RVA=0x041E1670  token=0x6000548  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Parameters
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x88
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_Parameters HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           System.Int32                    _nodeID  // 0x18
  private           System.String                   _uiLabel  // 0x20
  private           System.Int32[]                  _paramInts  // 0x28
  private           System.Single[]                 _paramFloats  // 0x30
  private           System.String[]                 _paramStrings  // 0x38
  private           HoudiniEngineUnity.HAPI_ParmChoiceInfo[]_paramChoices  // 0x40
  private           System.Collections.Generic.List<System.Int32>_rootParameters  // 0x48
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterData>_parameterList  // 0x50
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterModifier>_parameterModifiers  // 0x58
  private           System.Boolean                  _regenerateParameters  // 0x60
  private           System.Byte[]                   _presetData  // 0x68
  private           System.Byte[]                   _defaultPresetData  // 0x70
  private           System.Boolean                  _validParameters  // 0x78
  private           System.Boolean                  _showParameters  // 0x79
  private           System.Boolean                  _recacheUI  // 0x7a
  private           HoudiniEngineUnity.HEU_HoudiniAsset_parentAsset  // 0x80
PROPERTIES:
  ParentAsset  get=0x04D86260
  ShowParameters  get=0x04D88E40  set=0x04D88E90
  NodeID  get=0x011EF5B0  set=0x011EF9B0
  RootParameters  get=0x04D86200
  ParameterModifiers  get=0x02E56440
  RequiresRegeneration  get=0x04D88340  set=0x04D88350
  RecacheUI  get=0x04D88E50  set=0x04D88EA0
METHODS:
  RVA=0x04D86230  token=0x6000552  System.Byte[] GetPresetData()
  RVA=0x0485F170  token=0x6000553  System.Void SetPresetData(System.Byte[] data)
  RVA=0x04D862B0  token=0x6000554  System.Byte[] GetDefaultPresetData()
  RVA=0x04D86F90  token=0x6000557  System.Boolean AreParametersValid()
  RVA=0x09D73114  token=0x6000558  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=0x09D78444  token=0x6000559  System.Void Recook()
  RVA=0x04D86270  token=0x600055A  System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterData> GetParameters()
  RVA=0x09D72E4C  token=0x600055B  HoudiniEngineUnity.HEU_ParameterData GetParameter(System.Int32 listIndex)
  RVA=0x09D72EBC  token=0x600055C  HoudiniEngineUnity.HEU_ParameterData GetParameter(System.String name)
  RVA=0x09D72D2C  token=0x600055D  HoudiniEngineUnity.HEU_ParameterData GetParameterWithParmID(System.Int32 parmID)
  RVA=0x09D78534  token=0x600055E  System.Void RemoveParameter(System.Int32 listIndex)
  RVA=0x09D73590  token=0x600055F  System.Boolean HaveParametersChanged()
  RVA=0x09D7874C  token=0x6000560  System.Boolean ResetAllToDefault(System.Boolean bRecookAsset)
  RVA=0x09D793E0  token=0x6000561  System.Boolean SetFloatParameterValue(System.String parameterName, System.Single value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=0x09D71888  token=0x6000562  System.Boolean GetFloatParameterValue(System.String parameterName, System.Single& value, System.Int32 atIndex)
  RVA=0x09D795E8  token=0x6000563  System.Boolean SetFloatParameterValues(System.String parameterName, System.Single[] values, System.Boolean bRecookAsset)
  RVA=0x09D71A7C  token=0x6000564  System.Boolean GetFloatParameterValues(System.String parameterName, System.Single[]& values)
  RVA=0x09D78E24  token=0x6000565  System.Boolean SetColorParameterValue(System.String parameterName, UnityEngine.Color value, System.Boolean bRecookAsset)
  RVA=0x09D71244  token=0x6000566  System.Boolean GetColorParameterValue(System.String parameterName, UnityEngine.Color& value)
  RVA=0x09D79CDC  token=0x6000567  System.Boolean SetIntParameterValue(System.String parameterName, System.Int32 value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=0x09D7241C  token=0x6000568  System.Boolean GetIntParameterValue(System.String parameterName, System.Int32& value, System.Int32 atIndex)
  RVA=0x09D79EE4  token=0x6000569  System.Boolean SetIntParameterValues(System.String parameterName, System.Int32[] values, System.Boolean bRecookAsset)
  RVA=0x09D72610  token=0x600056A  System.Boolean GetIntParameterValues(System.String parameterName, System.Int32[]& values)
  RVA=0x09D78C54  token=0x600056B  System.Boolean SetChoiceParameterValue(System.String parameterName, System.Int32 value, System.Boolean bRecookAsset)
  RVA=0x09D7109C  token=0x600056C  System.Boolean GetChoiceParameterValue(System.String parameterName, System.Int32& value)
  RVA=0x09D78B38  token=0x600056D  System.Boolean SetBoolParameterValue(System.String parameterName, System.Boolean value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=0x09D70F90  token=0x600056E  System.Boolean GetBoolParameterValue(System.String parameterName, System.Boolean& value, System.Int32 atIndex)
  RVA=0x09D7A7A8  token=0x600056F  System.Boolean SetStringParameterValue(System.String parameterName, System.String value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=0x09D731AC  token=0x6000570  System.Boolean GetStringParameterValue(System.String parameterName, System.String& value, System.Int32 atIndex)
  RVA=0x09D7A9B0  token=0x6000571  System.Boolean SetStringParameterValues(System.String parameterName, System.String[] values, System.Boolean bRecookAsset)
  RVA=0x09D733C8  token=0x6000572  System.Boolean GetStringParameterValues(System.String parameterName, System.String[]& values)
  RVA=0x09D78794  token=0x6000573  System.Boolean SetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=0x09D7895C  token=0x6000574  System.Boolean SetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[] values, System.Boolean bRecookAsset)
  RVA=0x09D70C4C  token=0x6000575  System.Boolean GetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject& value, System.Int32 atIndex)
  RVA=0x09D70DC4  token=0x6000576  System.Boolean GetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[]& values)
  RVA=0x09D7A534  token=0x6000577  System.Boolean SetRampParameterNumPoints(System.String parameterName, System.Int32 numPoints, System.Boolean bRecookAsset)
  RVA=0x09D72FFC  token=0x6000578  System.Boolean GetRampParameterNumPoints(System.String parameterName, System.Int32& numPoints)
  RVA=0x09D79844  token=0x6000579  System.Boolean SetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, System.Single pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolationType, System.Boolean bRecookAsset)
  RVA=0x09D71C14  token=0x600057A  System.Boolean GetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, System.Single& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType)
  RVA=0x09D79B3C  token=0x600057B  System.Boolean SetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[] rampPoints, System.Boolean bRecookAsset)
  RVA=0x09D71F08  token=0x600057C  System.Boolean GetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[]& rampPoints)
  RVA=0x09D78F50  token=0x600057D  System.Boolean SetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, UnityEngine.Color pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolationType, System.Boolean bRecookAsset)
  RVA=0x09D71364  token=0x600057E  System.Boolean GetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, UnityEngine.Color& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType)
  RVA=0x09D79234  token=0x600057F  System.Boolean SetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[] rampPoints, System.Boolean bRecookAsset)
  RVA=0x09D71650  token=0x6000580  System.Boolean GetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[]& rampPoints)
  RVA=0x09D7B134  token=0x6000581  System.Boolean TriggerButtonParameter(System.String parameterName)
  RVA=0x09D7A148  token=0x6000582  System.Boolean SetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper> parameterTuples, System.Boolean bRecook)
  RVA=0x09D72948  token=0x6000583  System.Boolean GetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper>& parameterTuples)
  RVA=0x09D75B80  token=0x6000584  System.Boolean Initialize(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterData> previousParamFolders, System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_InputNode> previousParamInputNodes, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset)
  RVA=0x09D73A3C  token=0x6000585  System.Boolean Initialize4HGTool(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, System.Int32 parentNodeId)
  RVA=0x09D7AC20  token=0x6000586  System.Void SetupRampParameter(HoudiniEngineUnity.HEU_ParameterData rampParameter)
  RVA=0x09D7B514  token=0x6000587  System.Boolean UploadValuesToHoudini(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bDoCheck, System.Boolean bForceUploadInputs)
  RVA=0x09D77CF8  token=0x6000588  System.Void InsertInstanceToMultiParm(System.Int32 unityParamIndex, System.Int32 instanceIndex, System.Int32 numInstancesToAdd)
  RVA=0x09D784AC  token=0x6000589  System.Void RemoveInstancesFromMultiParm(System.Int32 unityParamIndex, System.Int32 instanceIndex, System.Int32 numInstancesToRemove)
  RVA=0x09D70B44  token=0x600058A  System.Void ClearInstancesFromMultiParm(System.Int32 unityParamIndex)
  RVA=0x09D7354C  token=0x600058B  System.Boolean HasModifiersPending()
  RVA=0x09D77FE0  token=0x600058C  System.Void ProcessModifiers(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D727AC  token=0x600058D  System.Void GetParameterDataForUIRestore(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterData> folderParams, System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_InputNode> inputNodeParams)
  RVA=0x09D7213C  token=0x600058E  System.Void GetInputNodeConnectionObjects(System.Collections.Generic.List<UnityEngine.GameObject> inputNodeObjects)
  RVA=0x09D70C00  token=0x600058F  System.Void DownloadPresetData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D7B4EC  token=0x6000590  System.Void UploadPresetData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D70BB4  token=0x6000591  System.Void DownloadAsDefaultPresetData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D7B300  token=0x6000592  System.Void UploadParameterInputs(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Boolean bForceUpdate)
  RVA=0x09D7B270  token=0x6000593  System.Void UpdateTransformParameters(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_TransformEuler& HAPITransform)
  RVA=0x09D7B104  token=0x6000594  System.Void SyncParameterFromHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.String parameterName)
  RVA=0x09D7B02C  token=0x6000595  System.Void SyncInternalParametersForUndoCompare(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D706A8  token=0x6000596  System.Void CleanUp()
  RVA=0x09D785A0  token=0x6000597  System.Void ResetAllToDefault(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D77D80  token=0x6000598  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_Parameters other)
  RVA=0x09D7B618  token=0x6000599  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ParameterUtility
TYPE:  static class
TOKEN: 0x20000CB
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09D90A64  token=0x600059E  System.Boolean GetToggle(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Boolean& outValue)
  RVA=0x09D91090  token=0x600059F  System.Boolean SetToggle(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Boolean setValue)
  RVA=0x09D90658  token=0x60005A0  System.Boolean GetInt(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32& outValue)
  RVA=0x09D90F08  token=0x60005A1  System.Boolean SetInt(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32 setValue)
  RVA=0x09D90404  token=0x60005A2  System.Boolean GetFloat(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single& outValue)
  RVA=0x09D904C8  token=0x60005A3  System.Boolean GetFloats(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single[]& outValues)
  RVA=0x09D90CB4  token=0x60005A4  System.Boolean SetFloat(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single setValue)
  RVA=0x09D90D78  token=0x60005A5  System.Boolean SetFloats(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Single[] setValues)
  RVA=0x09D90998  token=0x60005A6  System.Boolean GetString(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.String& outValue)
  RVA=0x09D90FCC  token=0x60005A7  System.Boolean SetString(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.String setValue)
  RVA=0x09D90B28  token=0x60005A8  System.Boolean SetChoice(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32 setValue)
  RVA=0x09D9027C  token=0x60005A9  System.Boolean GetChoice(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32& outValue)
  RVA=0x09D90E38  token=0x60005AA  System.Boolean SetInputNode(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, UnityEngine.GameObject obj, System.Int32 index)
  RVA=0x09D9058C  token=0x60005AB  System.Boolean GetInputNode(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, System.Int32 index, UnityEngine.GameObject& obj)
  RVA=0x09D90338  token=0x60005AC  System.Boolean GetColor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, UnityEngine.Color& getValue)
  RVA=0x09D90BE8  token=0x60005AD  System.Boolean SetColor(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.String paramName, UnityEngine.Color setValue)
  RVA=0x09D908DC  token=0x60005AE  System.Int32 GetParameterIndexFromName(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName)
  RVA=0x09D908A8  token=0x60005AF  System.Int32 GetParameterIndexFromNameOrTag(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName)
  RVA=0x09D90190  token=0x60005B0  System.Int32 FindTextureParamByNameOrTag(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName, System.String useTextureParmName)
  RVA=0x09D907FC  token=0x60005B1  System.Boolean GetParameterFloatValue(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName, System.Single defaultValue, System.Single& returnValue)
  RVA=0x09D9071C  token=0x60005B2  System.Boolean GetParameterColor3Value(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.String parameterName, UnityEngine.Color defaultValue, UnityEngine.Color& outputColor)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PDGCookedDataEvent
TYPE:  class
TOKEN: 0x20000CC
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x09D8F8F0  token=0x60005B3  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PDGCookedEventData
TYPE:  class
TOKEN: 0x20000CD
SIZE:  0x20
FIELDS:
  public            System.Boolean                  CookSuccess  // 0x10
  public            HoudiniEngineUnity.HEU_TOPNodeDataTopNodeData  // 0x18
METHODS:
  RVA=0x03219380  token=0x60005B4  System.Void .ctor(System.Boolean bSuccess, HoudiniEngineUnity.HEU_TOPNodeData bTopNodeData)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PDGAssetLink
TYPE:  class
TOKEN: 0x20000CE
SIZE:  0xA8
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           HoudiniEngineUnity.HEU_PDGCookedDataEvent_cookedDataEvent  // 0x18
  private           System.String                   _assetPath  // 0x20
  private           UnityEngine.GameObject          _assetGO  // 0x28
  private           System.String                   _assetName  // 0x30
  private           System.Int32                    _assetID  // 0x38
  private           HoudiniEngineUnity.HEU_HoudiniAsset_heu  // 0x40
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNetworkData>_topNetworks  // 0x48
  private           System.String[]                 _topNetworkNames  // 0x50
  private           System.Int32                    _selectedTOPNetwork  // 0x58
  private           HoudiniEngineUnity.HEU_PDGAssetLink.LinkState_linkState  // 0x5c
  private           System.Boolean                  _autoCook  // 0x60
  private           System.Boolean                  _useHEngineData  // 0x61
  public            HoudiniEngineUnity.HEU_PDGAssetLink.UpdateUIDelegate_repaintUIDelegate  // 0x68
  public            HoudiniEngineUnity.HEU_WorkItemTally_workItemTally  // 0x70
  private           UnityEngine.GameObject          _loadRootGameObject  // 0x78
  private           System.String                   _outputCachePathRoot  // 0x80
  private           System.Boolean                  _bUseTOPNodeFilter  // 0x88
  private           System.Boolean                  _bUseTOPOutputFilter  // 0x89
  private           System.String                   _topNodeFilter  // 0x90
  private           System.String                   _topOutputFilter  // 0x98
  private           System.Int32                    _numLoadingResults  // 0xa0
  private           System.Int32                    _numTotalResults  // 0xa4
PROPERTIES:
  AutoCook  get=0x04D88340  set=0x04D88350
  UseHEngineData  get=0x04D892D0  set=0x04D8FF40
  UseTOPNodeFilter  get=0x011F2750  set=0x011F2760
  UseTOPOutputFilter  get=0x04D89060  set=0x04D90C50
  TopNodeFilter  get=0x04D86220  set=0x06402330
  TopOutputFilter  get=0x04D85EE0  set=0x06352D0C
  ParentAsset  get=0x04D85A60
  AssetPath  get=0x04D862C0
  AssetGO  get=0x04D86240
  AssetName  get=0x011F36E0
  AssetID  get=0x04D863B0
  TopNetworks  get=0x04D86200
  TopNetworkNames  get=0x04D86270
  SelectedTOPNetwork  get=0x04D86570
  PDGLinkState  get=0x09D8F8C4
  LoadRootGameObject  get=0x04D862A0
  OutputCachePathRoot  get=0x04D86260
  CookedDataEvent  get=0x0385B100
  AssetLinkStateInternal  get=0x04D86560
METHODS:
  RVA=0x09D8F20C  token=0x60005CE  System.Void Setup(HoudiniEngineUnity.HEU_HoudiniAsset hdaAsset)
  RVA=0x09D8F068  token=0x60005CF  System.Void Reset()
  RVA=0x09D8ED18  token=0x60005D0  System.Void Refresh()
  RVA=0x09D8D1F4  token=0x60005D1  System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,HoudiniEngineUnity.HEU_TOPNodeData>> GetNonHiddenTOPNodes(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork)
  RVA=0x09D8F070  token=0x60005D2  System.Void SelectTOPNetwork(System.Int32 newIndex)
  RVA=0x09D8F0C4  token=0x60005D3  System.Void SelectTOPNode(HoudiniEngineUnity.HEU_TOPNetworkData network, System.Int32 newIndex)
  RVA=0x09D8D340  token=0x60005D4  HoudiniEngineUnity.HEU_TOPNetworkData GetSelectedTOPNetwork()
  RVA=0x09D8D34C  token=0x60005D5  HoudiniEngineUnity.HEU_TOPNodeData GetSelectedTOPNode()
  RVA=0x09D8D470  token=0x60005D6  HoudiniEngineUnity.HEU_TOPNetworkData GetTOPNetwork(System.Int32 index)
  RVA=0x09D8CEE8  token=0x60005D7  System.Void DirtyTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8CEE8  token=0x60005D8  System.Void CookTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8D0C0  token=0x60005D9  System.Void DirtyAll()
  RVA=0x09D8CEA4  token=0x60005DA  System.Void CookOutput()
  RVA=0x09D8C924  token=0x60005DB  System.Void PauseCook()
  RVA=0x09D8C924  token=0x60005DC  System.Void CancelCook()
  RVA=0x09D8D0E0  token=0x60005DD  HoudiniEngineUnity.HEU_SessionBase GetHAPISession()
  RVA=0x09D8D658  token=0x60005DE  HoudiniEngineUnity.HEU_TOPNodeData GetTOPNode(System.Int32 nodeID)
  RVA=0x09D8D588  token=0x60005DF  System.String GetTOPNodeStatus(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8D3C8  token=0x60005E0  HoudiniEngineUnity.HEU_TOPNetworkData GetTOPNetworkByName(System.String name, System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNetworkData> topNetworks)
  RVA=0x09D8D4E0  token=0x60005E1  HoudiniEngineUnity.HEU_TOPNodeData GetTOPNodeByName(System.String name, System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNodeData> topNodes)
  RVA=0x041E1670  token=0x60005E2  System.Void Awake()
  RVA=0x041E1670  token=0x60005E3  System.Void OnBeforeSerialize()
  RVA=0x041E1670  token=0x60005E4  System.Void OnAfterDeserialize()
  RVA=0x041E1670  token=0x60005E5  System.Void HandleInitialLoad()
  RVA=0x09D8E0DC  token=0x60005E6  System.Void OnDestroy()
  RVA=0x09D8E0A4  token=0x60005E7  System.Void NotifyAssetCooked(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean bSuccess, System.Collections.Generic.List<UnityEngine.GameObject> generatedOutputs)
  RVA=0x09D8E07C  token=0x60005E8  System.Void NotifyAssetCooked(HoudiniEngineUnity.HEU_CookedEventData cookedEventData)
  RVA=0x09D8E07C  token=0x60005E9  System.Void NotifyAssetCooked(HoudiniEngineUnity.HEU_ReloadEventData reloadEventData)
  RVA=0x09D8E880  token=0x60005EA  System.Void PopulateFromHDA()
  RVA=0x09D8E9C8  token=0x60005EB  System.Boolean PopulateTOPNetworks()
  RVA=0x09D8EBA0  token=0x60005EC  System.Boolean PopulateTOPNodes(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_TOPNetworkData topNetwork, System.Int32[] topNodeIDs, System.Boolean useHEngineData)
  RVA=0x09D8C934  token=0x60005ED  System.Void ClearAllTOPData()
  RVA=0x09D8CC00  token=0x60005EE  System.Void ClearTOPNetworkWorkItemResults(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork)
  RVA=0x09D8CD14  token=0x60005EF  System.Void ClearTOPNodeWorkItemResults(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8CE10  token=0x60005F0  System.Void ClearWorkItemResultByID(HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 workItemID)
  RVA=0x09D8CE34  token=0x60005F1  System.Void ClearWorkItemResult(HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_TOPWorkResult result)
  RVA=0x09D8F39C  token=0x60005F2  System.Void UpdateTOPNodeResultsVisibility(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8D7BC  token=0x60005F3  HoudiniEngineUnity.HEU_TOPWorkResult GetWorkResultByID(HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 workItemID)
  RVA=0x09D8CF10  token=0x60005F4  System.Void DestroyWorkItemResultData(HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_TOPWorkResult result)
  RVA=0x09D8D908  token=0x60005F5  System.Void LoadResults(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HAPI_PDG_WorkItemInfo workItemInfo, HoudiniEngineUnity.HAPI_PDG_WorkItemOutputFile[] resultInfos, System.Int32 workItemID, System.Action<HoudiniEngineUnity.HEU_TOPNodeData,HoudiniEngineUnity.HEU_SyncedEventData> OnSynced)
  RVA=0x09D8D154  token=0x60005F6  UnityEngine.Transform GetLoadRootTransform()
  RVA=0x09D8EFA4  token=0x60005F7  System.Void RepaintUI()
  RVA=0x09D8F410  token=0x60005F8  System.Void UpdateWorkItemTally()
  RVA=0x09D8EFBC  token=0x60005F9  System.Void ResetTOPNetworkWorkItemTally(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork)
  RVA=0x09D8E144  token=0x60005FA  System.Void OnTOPNodeFilterChanged(System.String filter)
  RVA=0x09D8E4C0  token=0x60005FB  System.Void OnTOPOutputFilterChanged(System.String filter)
  RVA=0x09D8E820  token=0x60005FC  System.Void ParseHEngineData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 topNodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, HoudiniEngineUnity.TOPNodeTags& nodeTags)
  RVA=0x09D8F11C  token=0x60005FD  System.Void SetupTopNetworkNames(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork)
  RVA=0x09D8E0EC  token=0x60005FE  System.Void OnSyncComplete(HoudiniEngineUnity.HEU_PDGCookedEventData Data)
  RVA=0x09D43A04  token=0x60005FF  HoudiniEngineUnity.HEU_LinkStateWrapper LinkState_InternalToWrapper(HoudiniEngineUnity.HEU_PDGAssetLink.LinkState linkState)
  RVA=0x09D43A04  token=0x6000600  HoudiniEngineUnity.HEU_PDGAssetLink.LinkState LinkState_WrapperToInternal(HoudiniEngineUnity.HEU_LinkStateWrapper linkState)
  RVA=0x09D8F7B4  token=0x6000601  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.TOPNodeTags
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x18
FIELDS:
  public            System.Boolean                  _show  // 0x10
  public            System.Boolean                  _autoload  // 0x11
  public            System.Boolean                  _showHEngineData  // 0x12
  public            System.Boolean                  _autoloadHEngineData  // 0x13
METHODS:
  RVA=0x041E1670  token=0x6000609  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_TOPNetworkData
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x48
FIELDS:
  public            System.Int32                    _nodeID  // 0x10
  public            System.String                   _nodeName  // 0x18
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPNodeData>_topNodes  // 0x20
  public            System.String[]                 _topNodeNames  // 0x28
  public            System.Int32                    _selectedTOPIndex  // 0x30
  public            System.String                   _parentName  // 0x38
  public            HoudiniEngineUnity.TOPNodeTags  _tags  // 0x40
METHODS:
  RVA=0x09D93B54  token=0x600060A  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_TOPNodeData
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x50
FIELDS:
  public            System.Int32                    _nodeID  // 0x10
  public            System.String                   _nodeName  // 0x18
  public            System.String                   _parentName  // 0x20
  public            UnityEngine.GameObject          _workResultParentGO  // 0x28
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_TOPWorkResult>_workResults  // 0x30
  public            HoudiniEngineUnity.TOPNodeTags  _tags  // 0x38
  public            System.Boolean                  _showResults  // 0x40
  public            HoudiniEngineUnity.HEU_TOPNodeData.PDGState_pdgState  // 0x44
  public            HoudiniEngineUnity.HEU_WorkItemTally_workItemTally  // 0x48
METHODS:
  RVA=0x09D93C7C  token=0x600060B  System.Void Reset()
  RVA=0x09D93C5C  token=0x600060C  System.Boolean AreAllWorkItemsComplete()
  RVA=0x09D93C1C  token=0x600060D  System.Boolean AnyWorkItemsFailed()
  RVA=0x09D93C3C  token=0x600060E  System.Boolean AnyWorkItemsPending()
  RVA=0x09D93CA8  token=0x600060F  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_TOPWorkResult
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x20
FIELDS:
  public            System.Int32                    _workItemIndex  // 0x10
  public            System.Int32                    _workItemID  // 0x14
  public            System.Collections.Generic.List<UnityEngine.GameObject>_generatedGOs  // 0x18
METHODS:
  RVA=0x09D93D78  token=0x6000610  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_WorkItemTally
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0x28
FIELDS:
  public            System.Int32                    _totalWorkItems  // 0x10
  public            System.Int32                    _waitingWorkItems  // 0x14
  public            System.Int32                    _scheduledWorkItems  // 0x18
  public            System.Int32                    _cookingWorkItems  // 0x1c
  public            System.Int32                    _cookedWorkItems  // 0x20
  public            System.Int32                    _erroredWorkItems  // 0x24
METHODS:
  RVA=0x04DA2BD0  token=0x6000611  System.Void ZeroAll()
  RVA=0x09D944AC  token=0x6000612  System.Boolean AreAllWorkItemsComplete()
  RVA=0x09D94488  token=0x6000613  System.Boolean AnyWorkItemsFailed()
  RVA=0x09D94490  token=0x6000614  System.Boolean AnyWorkItemsPending()
  RVA=0x09D944CC  token=0x6000615  System.String ProgressRatio()
  RVA=0x041E1670  token=0x6000616  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PDGSession
TYPE:  class
TOKEN: 0x20000D9
SIZE:  0x70
FIELDS:
  private   static  HoudiniEngineUnity.HEU_PDGSession_pdgSession  // static @ 0x0
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_PDGAssetLink>_pdgAssets  // 0x10
  public            System.Int32                    _pdgMaxProcessEvents  // 0x18
  public            HoudiniEngineUnity.HAPI_PDG_EventInfo[]_pdgQueryEvents  // 0x20
  public            System.Int32[]                  _pdgContextIDs  // 0x28
  public            System.Boolean                  _errored  // 0x30
  public            System.String                   _errorMsg  // 0x38
  public            HoudiniEngineUnity.HAPI_PDG_State_pdgState  // 0x40
  private           System.Action<HoudiniEngineUnity.HEU_PDGCookedEventData>_cookedDataEvent  // 0x48
  private           System.Boolean                  _pendingCallback  // 0x50
  private           System.Int32                    _numItemsCompleted  // 0x54
  private           System.Int32                    _totalNumItems  // 0x58
  private           System.Boolean                  _callbackSuccess  // 0x5c
  private           System.Text.StringBuilder       _pdgEventMessages  // 0x60
  private           System.String[]                 _eventMessageColorCode  // 0x68
PROPERTIES:
  CookedDataEvent  get=0x04D86200  set=0x035A41A0
METHODS:
  RVA=0x09D8FC38  token=0x6000617  HoudiniEngineUnity.HEU_PDGSession GetPDGSession()
  RVA=0x09D90040  token=0x6000618  System.Void .ctor()
  RVA=0x041E1670  token=0x6000619  System.Void AddAsset(HoudiniEngineUnity.HEU_PDGAssetLink asset)
  RVA=0x041E1670  token=0x600061A  System.Void RemoveAsset(HoudiniEngineUnity.HEU_PDGAssetLink asset)
  RVA=0x041E1670  token=0x600061B  System.Void Update()
  RVA=0x09D8FA00  token=0x600061C  System.Void CleanUp()
  RVA=0x041E1670  token=0x600061D  System.Void UpdatePDGContext()
  RVA=0x041E1670  token=0x600061E  System.Void ReinitializePDGContext()
  RVA=0x041E1670  token=0x600061F  System.Void ProcessPDGEvent(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 contextID, HoudiniEngineUnity.HAPI_PDG_EventInfo& eventInfo)
  RVA=0x09D8FF90  token=0x6000620  System.Void OnWorkItemLoadResults(HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_SyncedEventData OnSynced)
  RVA=0x09D8F94C  token=0x6000621  System.Void CheckCallback(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8FCC8  token=0x6000622  System.Boolean GetTOPAssetLinkAndNode(System.Int32 nodeID, HoudiniEngineUnity.HEU_PDGAssetLink& assetLink, HoudiniEngineUnity.HEU_TOPNodeData& topNode)
  RVA=0x09D8FFE4  token=0x6000623  System.Void SetTOPNodePDGState(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, HoudiniEngineUnity.HEU_TOPNodeData.PDGState pdgState)
  RVA=0x09D8FE90  token=0x6000624  System.Void NotifyTOPNodePDGStateClear(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8FF10  token=0x6000625  System.Void NotifyTOPNodeTotalWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc)
  RVA=0x09D8FDE8  token=0x6000626  System.Void NotifyTOPNodeCookedWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8FE5C  token=0x6000627  System.Void NotifyTOPNodeErrorWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x09D8FF50  token=0x6000628  System.Void NotifyTOPNodeWaitingWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc)
  RVA=0x09D8FED0  token=0x6000629  System.Void NotifyTOPNodeScheduledWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc)
  RVA=0x09D8FE1C  token=0x600062A  System.Void NotifyTOPNodeCookingWorkItem(HoudiniEngineUnity.HEU_PDGAssetLink assetLink, HoudiniEngineUnity.HEU_TOPNodeData topNode, System.Int32 inc)
  RVA=0x04DA2A40  token=0x600062B  System.Void ResetPDGEventInfo(HoudiniEngineUnity.HAPI_PDG_EventInfo& eventInfo)
  RVA=0x09D8FF9C  token=0x600062C  System.Void SetErrorState(System.String msg, System.Boolean bLogIt)
  RVA=0x09D8FAC8  token=0x600062D  System.Void ClearErrorState()
  RVA=0x09D8FB54  token=0x600062E  HoudiniEngineUnity.HEU_SessionBase GetHAPIPDGSession(System.Boolean bCreate)
  RVA=0x041E1670  token=0x600062F  System.Void CookTOPNetworkOutputNode(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork, System.Action<HoudiniEngineUnity.HEU_PDGCookedEventData> OnCook)
  RVA=0x041E1670  token=0x6000630  System.Void PauseCook(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork)
  RVA=0x041E1670  token=0x6000631  System.Void CancelCook(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork)
  RVA=0x041E1670  token=0x6000632  System.Void ClearWorkItemResult(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 contextID, HoudiniEngineUnity.HAPI_PDG_EventInfo eventInfo, HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=0x012081B0  token=0x6000633  System.Boolean DirtyTOPNode(System.Int32 nodeID)
  RVA=0x012081B0  token=0x6000634  System.Boolean CookTOPNode(System.Int32 nodeID)
  RVA=0x012081B0  token=0x6000635  System.Boolean DirtyAll(System.Int32 nodeID)
  RVA=0x09D8F92C  token=0x6000636  System.Void AddEventMessage(System.String msg)
  RVA=0x09D8FB30  token=0x6000637  System.String GetEventMessages()
  RVA=0x09D8FB0C  token=0x6000638  System.Void ClearEventMessages()
  RVA=0x09D8FBA4  token=0x6000639  System.Int32[] GetNonBypassedNetworkIds(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetId)
  RVA=0x09D8FDCC  token=0x600063A  System.Boolean IsPDGAsset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetId)
  RVA=0x04DA2A30  token=0x600063B  System.Void ResetCallbackVariables()
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_Curve
TYPE:  interface
TOKEN: 0x20000DD
FIELDS:
PROPERTIES:
  TargetGameObject  get=-1  // abstract  set=-1  // abstract
  GeoID  get=-1  // abstract
  PartID  get=-1  // abstract
  CurveNodeData  get=-1  // abstract
  Parameters  get=-1  // abstract
  CurveName  get=-1  // abstract
  IsInputCurve  get=-1  // abstract
  IsPartCurve  get=-1  // abstract
  InputCurveInfo  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600064E  System.Boolean IsEditable()
  RVA=-1  // abstract  token=0x600064F  System.Boolean IsGeoCurve()
  RVA=-1  // abstract  token=0x6000650  System.Void SetCurveName(System.String name)
  RVA=-1  // abstract  token=0x6000651  System.Void SetCurvePoint(System.Int32 pointIndex, UnityEngine.Vector3 newPosition, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000652  System.Void SetCurvePoint(System.Int32 pointIndex, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000653  System.Void SetCurveNodeData(System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> curveNodeData, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000654  UnityEngine.Vector3 GetCurvePoint(System.Int32 pointIndex)
  RVA=-1  // abstract  token=0x6000655  System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> GetAllPointTransforms()
  RVA=-1  // abstract  token=0x6000656  System.Collections.Generic.List<UnityEngine.Vector3> GetAllPoints()
  RVA=-1  // abstract  token=0x6000657  System.Int32 GetNumPoints()
  RVA=-1  // abstract  token=0x6000658  System.Void InsertCurvePoint(System.Int32 index, UnityEngine.Vector3 position, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000659  System.Void InsertCurvePoint(System.Int32 index, HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600065A  System.Void AddCurvePointToEnd(UnityEngine.Vector3 position, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600065B  System.Void AddCurvePointToEnd(HoudiniEngineUnity.CurveNodeData curveData, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600065C  System.Void RemoveCurvePoint(System.Int32 pointIndex, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600065D  System.Void ClearCurveNodeData(System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600065E  System.Void ProjectToColliders(UnityEngine.Vector3 rayDirection, System.Single rayDistance, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600065F  System.Void SetCurveGeometryVisibility(System.Boolean bVisible, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000660  UnityEngine.Vector3 GetTransformedPoint(System.Int32 pointIndex)
  RVA=-1  // abstract  token=0x6000661  System.Collections.Generic.List<UnityEngine.Vector3> GetTransformedPoints()
  RVA=-1  // abstract  token=0x6000662  System.Collections.Generic.List<HoudiniEngineUnity.CurveNodeData> DuplicateCurveNodeData()
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_GeoNode
TYPE:  interface
TOKEN: 0x20000DE
FIELDS:
PROPERTIES:
  GeoID  get=-1  // abstract
  GeoInfo  get=-1  // abstract
  GeoName  get=-1  // abstract
  GeoType  get=-1  // abstract
  Editable  get=-1  // abstract
  Displayable  get=-1  // abstract
  Parts  get=-1  // abstract
  ObjectNode  get=-1  // abstract
  InputNode  get=-1  // abstract
  GeoCurve  get=-1  // abstract
  VolumeCaches  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600066E  System.Boolean IsVisible()
  RVA=-1  // abstract  token=0x600066F  System.Boolean IsIntermediate()
  RVA=-1  // abstract  token=0x6000670  System.Boolean IsIntermediateOrEditable()
  RVA=-1  // abstract  token=0x6000671  System.Boolean IsGeoInputType()
  RVA=-1  // abstract  token=0x6000672  System.Boolean IsGeoCurveType()
  RVA=-1  // abstract  token=0x6000673  System.Void DestroyAllData(System.Boolean bIsRebuild)
  RVA=-1  // abstract  token=0x6000674  System.Void RemoveAndDestroyPart(HoudiniEngineUnity.HEU_PartData part)
  RVA=-1  // abstract  token=0x6000675  System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=-1  // abstract  token=0x6000676  System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=-1  // abstract  token=0x6000677  HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject)
  RVA=-1  // abstract  token=0x6000678  HoudiniEngineUnity.HEU_PartData GetPartFromPartID(System.Int32 partID)
  RVA=-1  // abstract  token=0x6000679  System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly)
  RVA=-1  // abstract  token=0x600067A  System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> GetParts()
  RVA=-1  // abstract  token=0x600067B  System.Void HideAllGeometry()
  RVA=-1  // abstract  token=0x600067C  System.Void DisableAllColliders()
  RVA=-1  // abstract  token=0x600067D  HoudiniEngineUnity.HEU_VolumeCache GetVolumeCacheByTileIndex(System.Int32 tileIndex)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetCookStatusWrapper
TYPE:  sealed struct
TOKEN: 0x20000DF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AssetCookStatusWrapperNONE  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookStatusWrapperCOOKING  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookStatusWrapperPOSTCOOK  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookStatusWrapperLOADING  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookStatusWrapperPOSTLOAD  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookStatusWrapperPRELOAD  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookStatusWrapperSELECT_SUBASSET  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetCookResultWrapper
TYPE:  sealed struct
TOKEN: 0x20000E0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AssetCookResultWrapperNONE  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookResultWrapperSUCCESS  // const
  public    static  HoudiniEngineUnity.HEU_AssetCookResultWrapperERRORED  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_CurveDrawCollisionWrapper
TYPE:  sealed struct
TOKEN: 0x20000E1
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_CurveDrawCollisionWrapperINVALID  // const
  public    static  HoudiniEngineUnity.HEU_CurveDrawCollisionWrapperCOLLIDERS  // const
  public    static  HoudiniEngineUnity.HEU_CurveDrawCollisionWrapperLAYERMASK  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetTypeWrapper
TYPE:  sealed struct
TOKEN: 0x20000E2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_AssetTypeWrapperTYPE_INVALID  // const
  public    static  HoudiniEngineUnity.HEU_AssetTypeWrapperTYPE_HDA  // const
  public    static  HoudiniEngineUnity.HEU_AssetTypeWrapperTYPE_CURVE  // const
  public    static  HoudiniEngineUnity.HEU_AssetTypeWrapperTYPE_INPUT  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_HoudiniAsset
TYPE:  interface
TOKEN: 0x20000E3
FIELDS:
PROPERTIES:
  LoadAssetFromMemory  get=-1  // abstract  set=-1  // abstract
  AlwaysOverwriteOnLoad  get=-1  // abstract  set=-1  // abstract
  GenerateUVs  get=-1  // abstract  set=-1  // abstract
  GenerateTangents  get=-1  // abstract  set=-1  // abstract
  GenerateNormals  get=-1  // abstract  set=-1  // abstract
  PushTransformToHoudini  get=-1  // abstract  set=-1  // abstract
  TransformChangeTriggersCooks  get=-1  // abstract  set=-1  // abstract
  CookingTriggersDownCooks  get=-1  // abstract  set=-1  // abstract
  AutoCookOnParameterChange  get=-1  // abstract  set=-1  // abstract
  IgnoreNonDisplayNodes  get=-1  // abstract  set=-1  // abstract
  UseOutputNodes  get=-1  // abstract  set=-1  // abstract
  GenerateMeshUsingPoints  get=-1  // abstract  set=-1  // abstract
  UseLODGroups  get=-1  // abstract  set=-1  // abstract
  SplitGeosByGroup  get=-1  // abstract  set=-1  // abstract
  SessionSyncAutoCook  get=-1  // abstract  set=-1  // abstract
  BakeUpdateKeepPreviousTransformValues  get=-1  // abstract  set=-1  // abstract
  PauseCooking  get=-1  // abstract  set=-1  // abstract
  CurveEditorEnabled  get=-1  // abstract  set=-1  // abstract
  CurveDrawCollision  get=-1  // abstract  set=-1  // abstract
  CurveDrawLayerMask  get=-1  // abstract  set=-1  // abstract
  CurveProjectMaxDistance  get=-1  // abstract  set=-1  // abstract
  CurveProjectDirection  get=-1  // abstract  set=-1  // abstract
  CurveProjectDirectionToView  get=-1  // abstract  set=-1  // abstract
  CurveDisableScaleRotation  get=-1  // abstract  set=-1  // abstract
  CurveFrameSelectedNodes  get=-1  // abstract  set=-1  // abstract
  CurveFrameSelectedNodeDistance  get=-1  // abstract  set=-1  // abstract
  HandlesEnabled  get=-1  // abstract  set=-1  // abstract
  EditableNodesToolsEnabled  get=-1  // abstract  set=-1  // abstract
  AssetType  get=-1  // abstract
  AssetInfo  get=-1  // abstract
  NodeInfo  get=-1  // abstract
  AssetName  get=-1  // abstract
  AssetOpName  get=-1  // abstract
  AssetHelp  get=-1  // abstract
  AssetID  get=-1  // abstract
  AssetPath  get=-1  // abstract
  OwnerGameObject  get=-1  // abstract
  RootGameObject  get=-1  // abstract
  MaterialCache  get=-1  // abstract
  Parameters  get=-1  // abstract
  AssetCacheFolder  get=-1  // abstract
  SubassetNames  get=-1  // abstract
  SelectedSubassetIndex  get=-1  // abstract
  CookStatus  get=-1  // abstract
  LastCookResult  get=-1  // abstract
  SessionID  get=-1  // abstract
  Curves  get=-1  // abstract
  InputNodes  get=-1  // abstract
  VolumeCaches  get=-1  // abstract
  ReloadDataEvent  get=-1  // abstract
  CookedDataEvent  get=-1  // abstract
  BakedDataEvent  get=-1  // abstract
  PreAssetEvent  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60006CF  System.Boolean RequestCook(System.Boolean bCheckParametersChanged, System.Boolean bAsync, System.Boolean bSkipCookCheck, System.Boolean bUploadParameters)
  RVA=-1  // abstract  token=0x60006D0  System.Boolean RequestReload(System.Boolean bAsync)
  RVA=-1  // abstract  token=0x60006D1  System.Boolean RequestResetParameters(System.Boolean bAsync)
  RVA=-1  // abstract  token=0x60006D2  UnityEngine.GameObject DuplicateAsset(UnityEngine.GameObject newRootGameObject)
  RVA=-1  // abstract  token=0x60006D3  System.Boolean DeleteAllGeneratedData(System.Boolean bIsRebuild)
  RVA=-1  // abstract  token=0x60006D4  UnityEngine.GameObject BakeToNewPrefab(System.String destinationPrefabPath)
  RVA=-1  // abstract  token=0x60006D5  UnityEngine.GameObject BakeToNewStandalone()
  RVA=-1  // abstract  token=0x60006D6  System.Boolean BakeToExistingPrefab(UnityEngine.GameObject bakeTargetGO)
  RVA=-1  // abstract  token=0x60006D7  System.Boolean BakeToExistingStandalone(UnityEngine.GameObject bakeTargetGO)
  RVA=-1  // abstract  token=0x60006D8  System.Boolean IsAssetValid()
  RVA=-1  // abstract  token=0x60006D9  System.Boolean GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=-1  // abstract  token=0x60006DA  System.Boolean GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=-1  // abstract  token=0x60006DB  HoudiniEngineUnity.HEU_Curve GetCurve(System.String curveName)
  RVA=-1  // abstract  token=0x60006DC  System.Boolean AddCurveDrawCollider(UnityEngine.Collider newCollider)
  RVA=-1  // abstract  token=0x60006DD  System.Boolean RemoveCurveDrawCollider(UnityEngine.Collider collider)
  RVA=-1  // abstract  token=0x60006DE  System.Boolean ClearCurveDrawColliders()
  RVA=-1  // abstract  token=0x60006DF  HoudiniEngineUnity.HEU_InputNode GetInputNode(System.String inputName)
  RVA=-1  // abstract  token=0x60006E0  HoudiniEngineUnity.HEU_InputNode GetAssetInputNode(System.String inputName)
  RVA=-1  // abstract  token=0x60006E1  HoudiniEngineUnity.HEU_InputNode GetInputNodeByIndex(System.Int32 index)
  RVA=-1  // abstract  token=0x60006E2  System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputNode> GetNonParameterInputNodes()
  RVA=-1  // abstract  token=0x60006E3  System.Int32 GetVolumeCacheCount()
  RVA=-1  // abstract  token=0x60006E4  HoudiniEngineUnity.HEU_SessionBase GetAssetSession(System.Boolean bCreateIfInvalid)
  RVA=-1  // abstract  token=0x60006E5  HoudiniEngineUnity.HEU_ObjectNode GetObjectNodeByName(System.String objName)
  RVA=-1  // abstract  token=0x60006E6  System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outputGeoNodes)
  RVA=-1  // abstract  token=0x60006E7  HoudiniEngineUnity.HEU_PartData GetInternalHDAPartWithGameObject(UnityEngine.GameObject outputGameObject)
  RVA=-1  // abstract  token=0x60006E8  System.Void ResetParametersToDefault()
  RVA=-1  // abstract  token=0x60006E9  System.Void HideAllGeometry()
  RVA=-1  // abstract  token=0x60006EA  System.Void DisableAllColliders()
  RVA=-1  // abstract  token=0x60006EB  HoudiniEngineUnity.HEU_MaterialData GetMaterialData(UnityEngine.Material material)
  RVA=-1  // abstract  token=0x60006EC  System.Void ClearMaterialCache()
  RVA=-1  // abstract  token=0x60006ED  System.Void RemoveMaterial(UnityEngine.Material material)
  RVA=-1  // abstract  token=0x60006EE  System.Void ResetMaterialOverrides()
  RVA=-1  // abstract  token=0x60006EF  HoudiniEngineUnity.HEU_AssetPreset GetAssetPreset(System.Boolean sceneRelativeObjects)
  RVA=-1  // abstract  token=0x60006F0  HoudiniEngineUnity.HEU_PDGAssetLink GetOrCreatePDGAssetLink()
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent
TYPE:  interface
TOKEN: 0x20000E4
FIELDS:
PROPERTIES:
  ParentAsset  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60006F2  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=-1  // abstract  token=0x60006F3  System.Void Recook()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputNodeTypeWrapper
TYPE:  sealed struct
TOKEN: 0x20000E5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_InputNodeTypeWrapperCONNECTION  // const
  public    static  HoudiniEngineUnity.HEU_InputNodeTypeWrapperNODE  // const
  public    static  HoudiniEngineUnity.HEU_InputNodeTypeWrapperPARAMETER  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputObjectTypeWrapper
TYPE:  sealed struct
TOKEN: 0x20000E6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_InputObjectTypeWrapperHDA  // const
  public    static  HoudiniEngineUnity.HEU_InputObjectTypeWrapperUNITY_MESH  // const
  public    static  HoudiniEngineUnity.HEU_InputObjectTypeWrapperCURVE  // const
  public    static  HoudiniEngineUnity.HEU_InputObjectTypeWrapperTERRAIN  // const
  public    static  HoudiniEngineUnity.HEU_InputObjectTypeWrapperBOUNDING_BOX  // const
  public    static  HoudiniEngineUnity.HEU_InputObjectTypeWrapperTILEMAP  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_InputNode
TYPE:  interface
TOKEN: 0x20000E7
FIELDS:
PROPERTIES:
  KeepWorldTransform  get=-1  // abstract  set=-1  // abstract
  PackGeometryBeforeMerging  get=-1  // abstract  set=-1  // abstract
  NodeType  get=-1  // abstract
  ObjectType  get=-1  // abstract
  PendingObjectType  get=-1  // abstract
  InputNodeID  get=-1  // abstract
  InputName  get=-1  // abstract
  LabelName  get=-1  // abstract
  ParamName  get=-1  // abstract
  MeshSettings  get=-1  // abstract
  TilemapSettings  get=-1  // abstract
  SplineSettings  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000702  System.Boolean IsAssetInput()
  RVA=-1  // abstract  token=0x6000703  System.Int32 NumInputEntries()
  RVA=-1  // abstract  token=0x6000704  UnityEngine.GameObject GetInputEntryGameObject(System.Int32 index)
  RVA=-1  // abstract  token=0x6000705  UnityEngine.GameObject[] GetInputEntryGameObjects()
  RVA=-1  // abstract  token=0x6000706  System.Void SetInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000707  System.Void InsertInputEntry(System.Int32 index, UnityEngine.GameObject newInputGameObject, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000708  System.Void AddInputEntryAtEnd(UnityEngine.GameObject newEntryGameObject, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000709  System.Void ResetInputNode(System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600070A  System.Void ChangeInputType(HoudiniEngineUnity.HEU_InputObjectTypeWrapper newType, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600070B  System.Void RemoveInputEntry(System.Int32 index, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600070C  System.Void RemoveAllInputEntries(System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600070D  System.Void SetInputEntryObjectUseTransformOffset(System.Int32 index, System.Boolean value, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600070E  System.Void SetInputEntryObjectTransformTranslateOffset(System.Int32 index, UnityEngine.Vector3 translateOffset, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600070F  System.Void SetInputEntryObjectTransformRotateOffset(System.Int32 index, UnityEngine.Vector3 rotateOffset, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000710  System.Void SetInputEntryObjectTransformScaleOffset(System.Int32 index, UnityEngine.Vector3 scaleOffset, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000711  System.Boolean AreAnyInputHDAsConnected()
  RVA=-1  // abstract  token=0x6000712  System.Int32 GetConnectedInputCount()
  RVA=-1  // abstract  token=0x6000713  System.Int32 GetConnectedNodeID(System.Int32 index)
  RVA=-1  // abstract  token=0x6000714  System.Void LoadPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset)
  RVA=-1  // abstract  token=0x6000715  System.Void PopulateInputPreset(HoudiniEngineUnity.HEU_InputPreset inputPreset, System.Boolean sceneRelativeGameObjects)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_MaterialSourceWrapper
TYPE:  sealed struct
TOKEN: 0x20000E8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_MaterialSourceWrapperDEFAULT  // const
  public    static  HoudiniEngineUnity.HEU_MaterialSourceWrapperHOUDINI  // const
  public    static  HoudiniEngineUnity.HEU_MaterialSourceWrapperUNITY  // const
  public    static  HoudiniEngineUnity.HEU_MaterialSourceWrapperSUBSTANCE  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_MaterialData
TYPE:  interface
TOKEN: 0x20000E9
FIELDS:
PROPERTIES:
  Material  get=-1  // abstract
  MaterialSource  get=-1  // abstract
  MaterialKey  get=-1  // abstract  set=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600071A  System.Boolean UpdateMaterialFromHoudini(HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String assetCacheFolderPath)
  RVA=-1  // abstract  token=0x600071B  System.Boolean IsExistingMaterial()
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_ObjectNode
TYPE:  interface
TOKEN: 0x20000EA
FIELDS:
PROPERTIES:
  ObjectID  get=-1  // abstract
  ObjectName  get=-1  // abstract
  ObjectInfo  get=-1  // abstract
  GeoNodes  get=-1  // abstract
  ObjectTransform  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000721  System.Boolean IsInstanced()
  RVA=-1  // abstract  token=0x6000722  System.Boolean IsVisible()
  RVA=-1  // abstract  token=0x6000723  System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData)
  RVA=-1  // abstract  token=0x6000724  System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=-1  // abstract  token=0x6000725  System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=-1  // abstract  token=0x6000726  HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject outputGameObject)
  RVA=-1  // abstract  token=0x6000727  HoudiniEngineUnity.HEU_GeoNode GetGeoNode(System.String geoName)
  RVA=-1  // abstract  token=0x6000728  System.Void GetCurves(System.Collections.Generic.List<HoudiniEngineUnity.HEU_Curve> curves, System.Boolean bEditableOnly)
  RVA=-1  // abstract  token=0x6000729  System.Void GetOutputGeoNodes(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoNode> outGeoNodes)
  RVA=-1  // abstract  token=0x600072A  System.Void HideAllGeometry()
  RVA=-1  // abstract  token=0x600072B  System.Void DisableAllColliders()
  RVA=-1  // abstract  token=0x600072C  System.Boolean IsInstancer()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper
TYPE:  sealed struct
TOKEN: 0x20000EB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapperCONSTANT  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapperLINEAR  // const
  public    static  HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapperCATMULL_ROM  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_RampPointWrapper
TYPE:  class
TOKEN: 0x20000EC
SIZE:  0x18
FIELDS:
  private           System.Single                   <Position>k__BackingField  // 0x10
  private           HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper<Interpolation>k__BackingField  // 0x14
PROPERTIES:
  Position  get=0x04D85F70  set=0x04D88280
  Interpolation  get=0x04D882A0  set=0x04D8F540
METHODS:
  RVA=0x04DA2A60  token=0x6000731  System.Void .ctor(System.Single position, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_FloatRampPointWrapper
TYPE:  class
TOKEN: 0x20000ED
SIZE:  0x20
EXTENDS: HoudiniEngineUnity.HEU_RampPointWrapper
FIELDS:
  private           System.Single                   <Value>k__BackingField  // 0x18
PROPERTIES:
  Value  get=0x04D86380  set=0x04D86390
METHODS:
  RVA=0x04DA2A20  token=0x6000734  System.Void .ctor(System.Single position, System.Single value, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ColorRampPointWrapper
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0x28
EXTENDS: HoudiniEngineUnity.HEU_RampPointWrapper
FIELDS:
  private           UnityEngine.Color               <Value>k__BackingField  // 0x18
PROPERTIES:
  Value  get=0x04D890C0  set=0x04D890D0
METHODS:
  RVA=0x04DA2A00  token=0x6000737  System.Void .ctor(System.Single position, UnityEngine.Color value, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ParameterTupleWrapper
TYPE:  class
TOKEN: 0x20000EF
SIZE:  0x40
FIELDS:
  private           System.Boolean[]                _boolValues  // 0x10
  private           System.Int32[]                  _intValues  // 0x18
  private           System.Single[]                 _floatValues  // 0x20
  private           System.String[]                 _stringValues  // 0x28
  private           HoudiniEngineUnity.HEU_FloatRampPointWrapper[]_floatRampValues  // 0x30
  private           HoudiniEngineUnity.HEU_ColorRampPointWrapper[]_colorRampValues  // 0x38
PROPERTIES:
  BoolValues  get=0x02B2ECC0  set=0x053908C0
  IntValues  get=0x0385B100  set=0x05392C40
  FloatValues  get=0x04D862C0  set=0x05390F40
  StringValues  get=0x04D86240  set=0x02FDB880
  FloatRampValues  get=0x011F36E0  set=0x05396200
  ColorRampValues  get=0x04D85A50  set=0x046A91B0
METHODS:
  RVA=0x041E1670  token=0x6000744  System.Void .ctor()
  RVA=0x053908C0  token=0x6000745  System.Void .ctor(System.Boolean[] boolValues)
  RVA=0x05392C40  token=0x6000746  System.Void .ctor(System.Int32[] intValues)
  RVA=0x05390F40  token=0x6000747  System.Void .ctor(System.Single[] floatValues)
  RVA=0x02FDB880  token=0x6000748  System.Void .ctor(System.String[] stringValues)
  RVA=0x05396200  token=0x6000749  System.Void .ctor(HoudiniEngineUnity.HEU_FloatRampPointWrapper[] floatRampValues)
  RVA=0x046A91B0  token=0x600074A  System.Void .ctor(HoudiniEngineUnity.HEU_ColorRampPointWrapper[] colorRampValues)
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_Parameters
TYPE:  interface
TOKEN: 0x20000F0
FIELDS:
PROPERTIES:
  ShowParameters  get=-1  // abstract  set=-1  // abstract
  NodeID  get=-1  // abstract
  RootParameters  get=-1  // abstract
  ParameterModifiers  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000750  System.Boolean AreParametersValid()
  RVA=-1  // abstract  token=0x6000751  System.Collections.Generic.List<HoudiniEngineUnity.HEU_ParameterData> GetParameters()
  RVA=-1  // abstract  token=0x6000752  HoudiniEngineUnity.HEU_ParameterData GetParameter(System.Int32 listIndex)
  RVA=-1  // abstract  token=0x6000753  HoudiniEngineUnity.HEU_ParameterData GetParameter(System.String name)
  RVA=-1  // abstract  token=0x6000754  HoudiniEngineUnity.HEU_ParameterData GetParameterWithParmID(System.Int32 parmID)
  RVA=-1  // abstract  token=0x6000755  System.Void RemoveParameter(System.Int32 listIndex)
  RVA=-1  // abstract  token=0x6000756  System.Boolean HaveParametersChanged()
  RVA=-1  // abstract  token=0x6000757  System.Boolean ResetAllToDefault(System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000758  System.Boolean SetFloatParameterValue(System.String parameterName, System.Single value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000759  System.Boolean GetFloatParameterValue(System.String parameterName, System.Single& value, System.Int32 atIndex)
  RVA=-1  // abstract  token=0x600075A  System.Boolean SetFloatParameterValues(System.String parameterName, System.Single[] values, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600075B  System.Boolean GetFloatParameterValues(System.String parameterName, System.Single[]& values)
  RVA=-1  // abstract  token=0x600075C  System.Boolean SetColorParameterValue(System.String parameterName, UnityEngine.Color value, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600075D  System.Boolean GetColorParameterValue(System.String parameterName, UnityEngine.Color& value)
  RVA=-1  // abstract  token=0x600075E  System.Boolean SetIntParameterValue(System.String parameterName, System.Int32 value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600075F  System.Boolean GetIntParameterValue(System.String parameterName, System.Int32& value, System.Int32 atIndex)
  RVA=-1  // abstract  token=0x6000760  System.Boolean SetIntParameterValues(System.String parameterName, System.Int32[] values, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000761  System.Boolean GetIntParameterValues(System.String parameterName, System.Int32[]& values)
  RVA=-1  // abstract  token=0x6000762  System.Boolean SetChoiceParameterValue(System.String parameterName, System.Int32 value, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000763  System.Boolean GetChoiceParameterValue(System.String parameterName, System.Int32& value)
  RVA=-1  // abstract  token=0x6000764  System.Boolean SetBoolParameterValue(System.String parameterName, System.Boolean value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000765  System.Boolean GetBoolParameterValue(System.String parameterName, System.Boolean& value, System.Int32 atIndex)
  RVA=-1  // abstract  token=0x6000766  System.Boolean SetStringParameterValue(System.String parameterName, System.String value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000767  System.Boolean GetStringParameterValue(System.String parameterName, System.String& value, System.Int32 atIndex)
  RVA=-1  // abstract  token=0x6000768  System.Boolean SetStringParameterValues(System.String parameterName, System.String[] values, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000769  System.Boolean GetStringParameterValues(System.String parameterName, System.String[]& values)
  RVA=-1  // abstract  token=0x600076A  System.Boolean SetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject value, System.Int32 atIndex, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600076B  System.Boolean GetAssetRefParameterValue(System.String parameterName, UnityEngine.GameObject& value, System.Int32 atIndex)
  RVA=-1  // abstract  token=0x600076C  System.Boolean SetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[] values, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600076D  System.Boolean GetAssetRefParameterValues(System.String parameterName, UnityEngine.GameObject[]& values)
  RVA=-1  // abstract  token=0x600076E  System.Boolean SetRampParameterNumPoints(System.String parameterName, System.Int32 numPoints, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600076F  System.Boolean GetRampParameterNumPoints(System.String parameterName, System.Int32& numPoints)
  RVA=-1  // abstract  token=0x6000770  System.Boolean SetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, System.Single pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolationType, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000771  System.Boolean GetFloatRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, System.Single& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType)
  RVA=-1  // abstract  token=0x6000772  System.Boolean SetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[] rampPoints, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000773  System.Boolean GetFloatRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_FloatRampPointWrapper[]& rampPoints)
  RVA=-1  // abstract  token=0x6000774  System.Boolean SetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single pointPosition, UnityEngine.Color pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper interpolation, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000775  System.Boolean GetColorRampParameterPointValue(System.String parameterName, System.Int32 pointIndex, System.Single& pointPosition, UnityEngine.Color& pointValue, HoudiniEngineUnity.HEU_HoudiniRampInterpolationTypeWrapper& interpolationType)
  RVA=-1  // abstract  token=0x6000776  System.Boolean SetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[] rampPoints, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x6000777  System.Boolean GetColorRampParameterPoints(System.String parameterName, HoudiniEngineUnity.HEU_ColorRampPointWrapper[]& rampPoints)
  RVA=-1  // abstract  token=0x6000778  System.Boolean TriggerButtonParameter(System.String parameterName)
  RVA=-1  // abstract  token=0x6000779  System.Boolean SetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper> parameterTuples, System.Boolean bRecookAsset)
  RVA=-1  // abstract  token=0x600077A  System.Boolean GetParameterTuples(System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.HEU_ParameterTupleWrapper>& parameterTuples)
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_PartData
TYPE:  interface
TOKEN: 0x20000F1
FIELDS:
PROPERTIES:
  PartID  get=-1  // abstract
  PartName  get=-1  // abstract
  GeoID  get=-1  // abstract
  PartType  get=-1  // abstract
  ParentGeoNode  get=-1  // abstract
  ObjectInstanceInfos  get=-1  // abstract
  Curve  get=-1  // abstract
  MeshVertexCount  get=-1  // abstract
  GeneratedOutput  get=-1  // abstract
  OutputGameObject  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000785  System.Boolean IsPartInstancer()
  RVA=-1  // abstract  token=0x6000786  System.Boolean IsAttribInstancer()
  RVA=-1  // abstract  token=0x6000787  System.Boolean IsInstancerAnyType()
  RVA=-1  // abstract  token=0x6000788  System.Boolean IsPartInstanced()
  RVA=-1  // abstract  token=0x6000789  System.Int32 GetPartPointCount()
  RVA=-1  // abstract  token=0x600078A  System.Boolean IsObjectInstancer()
  RVA=-1  // abstract  token=0x600078B  System.Boolean IsPartVolume()
  RVA=-1  // abstract  token=0x600078C  System.Boolean IsPartCurve()
  RVA=-1  // abstract  token=0x600078D  System.Boolean IsPartMesh()
  RVA=-1  // abstract  token=0x600078E  System.Boolean IsPartEditable()
  RVA=-1  // abstract  token=0x600078F  System.Boolean HaveInstancesBeenGenerated()
  RVA=-1  // abstract  token=0x6000790  System.Void SetGameObjectName(System.String partName)
  RVA=-1  // abstract  token=0x6000791  System.Void SetGameObject(UnityEngine.GameObject gameObject)
  RVA=-1  // abstract  token=0x6000792  System.Void SetVolumeLayerName(System.String name)
  RVA=-1  // abstract  token=0x6000793  System.String GetVolumeLayerName()
  RVA=-1  // abstract  token=0x6000794  System.Void DestroyAllData(System.Boolean bIsRebuild)
  RVA=-1  // abstract  token=0x6000795  System.Boolean IsUsingMaterial(HoudiniEngineUnity.HEU_MaterialData materialData)
  RVA=-1  // abstract  token=0x6000796  System.Void GetOutputGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> outputObjects)
  RVA=-1  // abstract  token=0x6000797  System.Void GetOutput(System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput> outputs)
  RVA=-1  // abstract  token=0x6000798  HoudiniEngineUnity.HEU_PartData GetHDAPartWithGameObject(UnityEngine.GameObject inGameObject)
  RVA=-1  // abstract  token=0x6000799  System.Void ClearInstances()
  RVA=-1  // abstract  token=0x600079A  HoudiniEngineUnity.HEU_Curve GetCurve(System.Boolean bEditableOnly)
  RVA=-1  // abstract  token=0x600079B  System.Void SetVisiblity(System.Boolean bVisibility)
  RVA=-1  // abstract  token=0x600079C  System.Void SetColliderState(System.Boolean bEnabled)
  RVA=-1  // abstract  token=0x600079D  HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectPath(System.String path)
  RVA=-1  // abstract  token=0x600079E  HoudiniEngineUnity.HEU_ObjectInstanceInfo GetObjectInstanceInfoWithObjectID(System.Int32 objNodeID)
  RVA=-1  // abstract  token=0x600079F  System.Void SetTerrainOffsetPosition(UnityEngine.Vector3 offsetPosition)
  RVA=-1  // abstract  token=0x60007A0  System.Void SetTerrainData(UnityEngine.TerrainData terrainData, System.String exportPathRelative, System.String exportPathUser)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_LinkStateWrapper
TYPE:  sealed struct
TOKEN: 0x20000F2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_LinkStateWrapperINACTIVE  // const
  public    static  HoudiniEngineUnity.HEU_LinkStateWrapperLINKING  // const
  public    static  HoudiniEngineUnity.HEU_LinkStateWrapperLINKED  // const
  public    static  HoudiniEngineUnity.HEU_LinkStateWrapperERROR_NOT_LINKED  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_PDGAssetLink
TYPE:  interface
TOKEN: 0x20000F3
FIELDS:
PROPERTIES:
  AutoCook  get=-1  // abstract  set=-1  // abstract
  UseHEngineData  get=-1  // abstract  set=-1  // abstract
  UseTOPNodeFilter  get=-1  // abstract  set=-1  // abstract
  UseTOPOutputFilter  get=-1  // abstract  set=-1  // abstract
  TopNodeFilter  get=-1  // abstract  set=-1  // abstract
  TopOutputFilter  get=-1  // abstract  set=-1  // abstract
  ParentAsset  get=-1  // abstract
  AssetPath  get=-1  // abstract
  AssetGO  get=-1  // abstract
  AssetName  get=-1  // abstract
  AssetID  get=-1  // abstract
  TopNetworks  get=-1  // abstract
  TopNetworkNames  get=-1  // abstract
  SelectedTOPNetwork  get=-1  // abstract
  PDGLinkState  get=-1  // abstract
  LoadRootGameObject  get=-1  // abstract
  OutputCachePathRoot  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60007B8  System.Void Setup(HoudiniEngineUnity.HEU_HoudiniAsset hdaAsset)
  RVA=-1  // abstract  token=0x60007B9  System.Void Reset()
  RVA=-1  // abstract  token=0x60007BA  System.Void Refresh()
  RVA=-1  // abstract  token=0x60007BB  System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,HoudiniEngineUnity.HEU_TOPNodeData>> GetNonHiddenTOPNodes(HoudiniEngineUnity.HEU_TOPNetworkData topNetwork)
  RVA=-1  // abstract  token=0x60007BC  System.Void SelectTOPNetwork(System.Int32 newIndex)
  RVA=-1  // abstract  token=0x60007BD  System.Void SelectTOPNode(HoudiniEngineUnity.HEU_TOPNetworkData network, System.Int32 newIndex)
  RVA=-1  // abstract  token=0x60007BE  HoudiniEngineUnity.HEU_TOPNetworkData GetSelectedTOPNetwork()
  RVA=-1  // abstract  token=0x60007BF  HoudiniEngineUnity.HEU_TOPNodeData GetSelectedTOPNode()
  RVA=-1  // abstract  token=0x60007C0  HoudiniEngineUnity.HEU_TOPNetworkData GetTOPNetwork(System.Int32 index)
  RVA=-1  // abstract  token=0x60007C1  System.Void DirtyTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=-1  // abstract  token=0x60007C2  System.Void CookTOPNode(HoudiniEngineUnity.HEU_TOPNodeData topNode)
  RVA=-1  // abstract  token=0x60007C3  System.Void DirtyAll()
  RVA=-1  // abstract  token=0x60007C4  System.Void CookOutput()
  RVA=-1  // abstract  token=0x60007C5  System.Void PauseCook()
  RVA=-1  // abstract  token=0x60007C6  System.Void CancelCook()
  RVA=-1  // abstract  token=0x60007C7  HoudiniEngineUnity.HEU_SessionBase GetHAPISession()
  RVA=-1  // abstract  token=0x60007C8  HoudiniEngineUnity.HEU_TOPNodeData GetTOPNode(System.Int32 nodeID)
  RVA=-1  // abstract  token=0x60007C9  System.String GetTOPNodeStatus(HoudiniEngineUnity.HEU_TOPNodeData topNode)
END_CLASS

CLASS: HoudiniEngineUnity.IHEU_VolumeCache
TYPE:  interface
TOKEN: 0x20000F4
FIELDS:
PROPERTIES:
  Layers  get=-1  // abstract
  TileIndex  get=-1  // abstract
  ObjectName  get=-1  // abstract
  GeoName  get=-1  // abstract
  TerrainData  get=-1  // abstract
  ScatterTrees  get=-1  // abstract
  DetailProperties  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60007D1  System.Void ResetParameters()
  RVA=-1  // abstract  token=0x60007D2  HoudiniEngineUnity.HEU_VolumeLayer GetLayer(System.String layerName)
  RVA=-1  // abstract  token=0x60007D3  System.Void PopulatePreset(HoudiniEngineUnity.HEU_VolumeCachePreset cachePreset)
  RVA=-1  // abstract  token=0x60007D4  System.Boolean ApplyPreset(HoudiniEngineUnity.HEU_VolumeCachePreset volumeCachePreset)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_MaterialData
TYPE:  class
TOKEN: 0x20000F5
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_MaterialData HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           UnityEngine.Material            _material  // 0x18
  private           HoudiniEngineUnity.HEU_MaterialData.Source_materialSource  // 0x20
  private           System.Int32                    _materialKey  // 0x24
PROPERTIES:
  Material  get=0x0385B100
  MaterialSource  get=0x09D8ACD8
  MaterialKey  get=0x011F0020  set=0x011F0030
METHODS:
  RVA=0x09D8A2E0  token=0x60007D9  System.Boolean UpdateMaterialFromHoudini(HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String assetCacheFolderPath)
  RVA=0x09D8A288  token=0x60007DA  System.Boolean IsExistingMaterial()
  RVA=0x09D8A5E8  token=0x60007DB  System.Boolean UseLegacyShaders(HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String assetCacheFolderPath, HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_NodeInfo nodeInfo, HoudiniEngineUnity.HAPI_ParmInfo[] parmInfos)
  RVA=0x09D89FE8  token=0x60007DC  System.String GetTextureFileNameFromMaterialParam(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo parmInfo)
  RVA=0x09D8A29C  token=0x60007DD  System.Boolean IsTransparentMaterial(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters)
  RVA=0x09D89D74  token=0x60007DE  System.Boolean GetMaterialAlpha(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parameters, System.Single defaultValue, System.Single& alpha)
  RVA=0x09D89EE8  token=0x60007DF  System.String GetSupportedFileFormat(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ImageInfo& imageInfo)
  RVA=0x09D8A0EC  token=0x60007E0  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_MaterialData other)
  RVA=0x09D43A04  token=0x60007E1  HoudiniEngineUnity.HEU_MaterialData.Source MaterialSource_WrapperToInternal(HoudiniEngineUnity.HEU_MaterialSourceWrapper materialSource)
  RVA=0x09D43A04  token=0x60007E2  HoudiniEngineUnity.HEU_MaterialSourceWrapper MaterialSource_WrapperToInternal(HoudiniEngineUnity.HEU_MaterialData.Source materialSource)
  RVA=0x09D8ACC8  token=0x60007E3  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_MaterialFactory
TYPE:  class
TOKEN: 0x20000F7
SIZE:  0x10
FIELDS:
  public    static  UnityEngine.Material            _defaultStandardMaterial  // static @ 0x0
METHODS:
  RVA=0x09D8B57C  token=0x60007E4  UnityEngine.Shader FindShader(System.String shaderName)
  RVA=0x09D8B544  token=0x60007E5  UnityEngine.Shader FindPluginShader(System.String shaderName)
  RVA=0x09D8B9E8  token=0x60007E6  UnityEngine.Material GetNewMaterialWithShader(System.String assetCacheFolderPath, System.String shaderName, System.String materialName, System.Boolean bWriteToFile)
  RVA=0x09D8B058  token=0x60007E7  UnityEngine.Material CreateNewHoudiniStandardMaterial(System.String assetCacheFolderPath, System.String materialName, System.Boolean bWriteToFile)
  RVA=0x09D8C5B0  token=0x60007E8  System.Void WriteMaterialToAssetCache(UnityEngine.Material material, System.String assetCacheFolderPath, System.String materialName, System.Boolean bOverwriteExisting)
  RVA=0x09D8B3B8  token=0x60007E9  System.Boolean DoesMaterialExistInAssetCache(UnityEngine.Material material)
  RVA=0x09D8B344  token=0x60007EA  System.Void DestroyNonAssetMaterial(UnityEngine.Material material, System.Boolean bRegisterUndo)
  RVA=0x09D8B33C  token=0x60007EB  System.Void DeleteAssetMaterial(UnityEngine.Material material)
  RVA=0x09D8C4E8  token=0x60007EC  UnityEngine.Texture2D RenderAndExtractImageToTexture(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.Int32 textureParmID, System.String textureName, System.String assetCacheFolderPath, System.Boolean isNormalMap, System.Boolean invertTexture)
  RVA=0x09D8B480  token=0x60007ED  UnityEngine.Texture2D ExtractHoudiniImageToTexturePNGJPEG(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String imagePlanes)
  RVA=0x09D8B4DC  token=0x60007EE  UnityEngine.Texture2D ExtractHoudiniImageToTextureRaw(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String imagePlanes)
  RVA=0x09D8B3FC  token=0x60007EF  UnityEngine.Texture2D ExtractHoudiniImageToTextureFile(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_MaterialInfo materialInfo, System.String imagePlanes, System.String assetCacheFolderPath)
  RVA=0x09D8C1E0  token=0x60007F0  UnityEngine.Material LoadUnityMaterial(System.String materialPath)
  RVA=0x09D8BF4C  token=0x60007F1  UnityEngine.Material LoadSubstanceMaterialWithName(System.String materialPath, System.String substanceName)
  RVA=0x09D8BD84  token=0x60007F2  UnityEngine.Material LoadSubstanceMaterialWithIndex(System.String materialPath, System.Int32 substanceMaterialIndex)
  RVA=0x09D8BC88  token=0x60007F3  System.Int32 GetUnitySubstanceMaterialKey(System.String unityMaterialPath, System.String substanceName, System.Int32 substanceIndex)
  RVA=0x053E31AC  token=0x60007F4  System.Int32 MaterialNameToKey(System.String materialName)
  RVA=0x09D8B3DC  token=0x60007F5  System.Void EnableGPUInstancing(UnityEngine.Material material)
  RVA=0x09D8C4CC  token=0x60007F6  System.Boolean MaterialHasGPUInstancingEnabled(UnityEngine.Material material)
  RVA=0x09D8AD04  token=0x60007F7  UnityEngine.Material CopyMaterial(UnityEngine.Material material)
  RVA=0x09D8B650  token=0x60007F8  UnityEngine.Material GetDefaultStandardMaterial()
  RVA=0x09D8C0D8  token=0x60007F9  UnityEngine.Texture2D LoadTexture(System.String path)
  RVA=0x09D8C5A8  token=0x60007FA  UnityEngine.Texture2D WhiteTexture()
  RVA=0x09D8B0A4  token=0x60007FB  HoudiniEngineUnity.HEU_MaterialData CreateUnitySubstanceMaterialData(System.Int32 materialKey, System.String materialPath, System.String substanceName, System.Int32 substanceIndex, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath)
  RVA=0x09D8AEF0  token=0x60007FC  HoudiniEngineUnity.HEU_MaterialData CreateMaterialInCache(System.Int32 materialKey, System.String materialName, HoudiniEngineUnity.HEU_MaterialSourceWrapper sourceType, System.Boolean bWriteToFile, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath)
  RVA=0x09D8AF4C  token=0x60007FD  HoudiniEngineUnity.HEU_MaterialData CreateMaterialInCache(System.Int32 materialKey, System.String materialName, HoudiniEngineUnity.HEU_MaterialData.Source sourceType, System.Boolean bWriteToFile, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath)
  RVA=0x09D8BBB8  token=0x60007FE  HoudiniEngineUnity.HEU_MaterialData GetOrCreateDefaultMaterialInCache(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Boolean bWriteToFile, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath)
  RVA=0x09D8B700  token=0x60007FF  HoudiniEngineUnity.HEU_MaterialData GetMaterialDataFromCache(System.Int32 materialKey, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache)
  RVA=0x09D8B5B4  token=0x6000800  System.String GenerateDefaultMaterialName(System.Int32 geoID, System.Int32 partID)
  RVA=0x09D8AD64  token=0x6000801  HoudiniEngineUnity.HEU_MaterialData CreateHoudiniMaterialData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32 materialID, System.Int32 geoID, System.Int32 partID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath)
  RVA=0x09D8B81C  token=0x6000802  System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MaterialData> GetMaterialDataMapFromCache(System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache)
  RVA=0x041E1670  token=0x6000803  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_UnityMaterialInfo
TYPE:  class
TOKEN: 0x20000F8
SIZE:  0x28
FIELDS:
  public            System.String                   _unityMaterialPath  // 0x10
  public            System.String                   _substancePath  // 0x18
  public            System.Int32                    _substanceIndex  // 0x20
METHODS:
  RVA=0x047E1B40  token=0x6000804  System.Void .ctor(System.String unityMaterialPath, System.String substancePath, System.Int32 substanceIndex)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SessionBase
TYPE:  class
TOKEN: 0x20000F9
SIZE:  0x30
FIELDS:
  protected         HoudiniEngineUnity.HEU_SessionData_sessionData  // 0x10
  private           System.Boolean                  <UserNotifiedSessionInvalid>k__BackingField  // 0x18
  private           System.String                   _sessionErrorMsg  // 0x20
  private           System.Boolean                  <LogErrorOverride>k__BackingField  // 0x28
  private           System.Boolean                  <ThrowErrorOverride>k__BackingField  // 0x29
  private           HoudiniEngineUnity.HAPI_Result  <LastCallResultCode>k__BackingField  // 0x2c
PROPERTIES:
  UserNotifiedSessionInvalid  get=0x04D86340  set=0x04D86350
  ConnectionState  get=0x09D915EC  set=0x09D91614
  ThisSessionMode  get=0x09D91600  set=0x09D91624
  LogErrorOverride  get=0x011F33C0  set=0x011F33D0
  ThrowErrorOverride  get=0x04D867B0  set=0x04D867C0
  LastCallResultCode  get=0x04D86710  set=0x04D86720
METHODS:
  RVA=0x04D862C0  token=0x600080B  System.String GetSessionErrorMsg()
  RVA=0x09D9131C  token=0x6000810  System.Boolean IsSessionSync()
  RVA=0x011EC580  token=0x6000813  HoudiniEngineUnity.HEU_HoudiniAsset GetRegisteredAssetFromID(System.Int32 id)
  RVA=0x012081B0  token=0x6000814  System.Boolean IsAssetRegistered(HoudiniEngineUnity.HEU_HoudiniAsset asset)
  RVA=0x041E1670  token=0x6000815  System.Void RegisterAsset(HoudiniEngineUnity.HEU_HoudiniAsset asset)
  RVA=0x041E1670  token=0x6000816  System.Void UnregisterAsset(System.Int32 id)
  RVA=0x041E1670  token=0x6000817  System.Void ReregisterOnAwake(HoudiniEngineUnity.HEU_HoudiniAsset asset)
  RVA=0x04DA2BA0  token=0x6000818  System.Void .ctor()
  RVA=0x09D9157C  token=0x6000819  System.Void SetSessionErrorMsg(System.String msg, System.Boolean bLogError)
  RVA=0x09D913C0  token=0x600081A  System.Void SetSessionConnectionErrorMsg(System.String introMsg, HoudiniEngineUnity.HAPI_Result result, System.Boolean bIsHARSRunning, System.Boolean bLogError)
  RVA=0x09D91334  token=0x600081B  System.Void SetLibraryErrorMsg(System.Boolean bLogError)
  RVA=0x012081B0  token=0x600081C  System.Boolean CreateSessionData(System.Boolean bOverwriteExisting, System.Boolean bIsDefaultSession)
  RVA=0x012081B0  token=0x600081D  System.Boolean CreateInProcessSession(System.Boolean bIsDefaultSession)
  RVA=0x012081B0  token=0x600081E  System.Boolean CreateThriftSocketSession(System.Boolean bIsDefaultSession, System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean bLogError)
  RVA=0x012081B0  token=0x600081F  System.Boolean CreateThriftPipeSession(System.Boolean bIsDefaultSession, System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000820  System.Boolean CreateThriftSharedMemorySession(System.Boolean bIsDefaultSession, System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000821  System.Boolean CreateCustomSession(System.Boolean bIsDefaultSession)
  RVA=0x012081B0  token=0x6000822  System.Boolean ConnectThriftSocketSession(System.Boolean bIsDefaultSession, System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean logError, System.Boolean autoInitialize)
  RVA=0x012081B0  token=0x6000823  System.Boolean ConnectThriftPipeSession(System.Boolean bIsDefaultSession, System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean logError, System.Boolean autoInitialize)
  RVA=0x012081B0  token=0x6000824  System.Boolean ConnectThriftSharedMemorySession(System.Boolean bIsDefaultSession, System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean logError, System.Boolean autoInitialize)
  RVA=0x012081B0  token=0x6000825  System.Boolean CloseSession()
  RVA=0x012081B0  token=0x6000826  System.Boolean CheckAndCloseExistingSession()
  RVA=0x09D91154  token=0x6000827  System.Void ClearSessionInfo()
  RVA=0x053908C0  token=0x6000828  System.Void SetSessionData(HoudiniEngineUnity.HEU_SessionData sessionData)
  RVA=0x02B2ECC0  token=0x6000829  HoudiniEngineUnity.HEU_SessionData GetSessionData()
  RVA=0x09D912C4  token=0x600082A  System.String GetSessionInfo()
  RVA=0x012081B0  token=0x600082B  System.Boolean IsSessionValid()
  RVA=0x012081B0  token=0x600082C  System.Boolean RestartSession()
  RVA=0x04D862C0  token=0x600082D  System.String GetLastSessionError()
  RVA=0x012081B0  token=0x600082E  System.Boolean CheckVersionMatch()
  RVA=0x012081B0  token=0x600082F  System.Boolean InitializeSession(HoudiniEngineUnity.HEU_SessionData sessionData)
  RVA=0x012081B0  token=0x6000830  System.Boolean HandleStatusResult(HoudiniEngineUnity.HAPI_Result result, System.String prependMsg, System.Boolean bThrowError, System.Boolean bLogError)
  RVA=0x041E1670  token=0x6000831  System.Void SetServerEnvString(System.String name, System.String value)
  RVA=0x09D912A8  token=0x6000832  System.Boolean GetServerEnvString(System.String name, System.String& value)
  RVA=0x04DA2B00  token=0x6000833  System.Boolean GetServerEnvVarCount(System.Int32& env_count)
  RVA=0x04DA2B00  token=0x6000834  System.Boolean GetCallResult(HoudiniEngineUnity.HAPI_Result& result)
  RVA=0x04DA2B00  token=0x6000835  System.Boolean GetCookResult(HoudiniEngineUnity.HAPI_Result& result)
  RVA=0x04DA2B00  token=0x6000836  System.Boolean GetCookState(HoudiniEngineUnity.HAPI_State& state)
  RVA=0x09D912F0  token=0x6000837  System.String GetStatusString(HoudiniEngineUnity.HAPI_StatusType statusType, HoudiniEngineUnity.HAPI_StatusVerbosity verbosity)
  RVA=0x09D9119C  token=0x6000838  System.String ComposeNodeCookResult(System.Int32 nodeId, HoudiniEngineUnity.HAPI_StatusVerbosity verbosity)
  RVA=0x011EC580  token=0x6000839  System.Int32 GetEnvInt(HoudiniEngineUnity.HAPI_EnvIntType intType)
  RVA=0x011EC580  token=0x600083A  System.Int32 GetSessionEnvInt(HoudiniEngineUnity.HAPI_SessionEnvIntType intType, System.Boolean bLogError)
  RVA=0x012081B0  token=0x600083B  System.Boolean GetString(System.Int32 stringHandle, System.String& resultString, System.Int32 bufferLength)
  RVA=0x011EC580  token=0x600083C  System.Int32 GetStringBufferLength(System.Int32 stringHandle)
  RVA=0x011EC580  token=0x600083D  System.Int32 CheckForSpecificErrors(System.Int32 nodeID, System.Int32 errorsToCheck)
  RVA=0x04D879D0  token=0x600083E  System.Double GetTime()
  RVA=0x012081B0  token=0x600083F  System.Boolean SetTime(System.Double time)
  RVA=0x012081B0  token=0x6000840  System.Boolean GetUseHoudiniTime()
  RVA=0x012081B0  token=0x6000841  System.Boolean SetUseHoudiniTime(System.Boolean enable)
  RVA=0x04DA2B10  token=0x6000842  System.Boolean LoadAssetLibraryFromFile(System.String assetPath, System.Boolean bAllowOverwrite, System.Int32& libraryID)
  RVA=0x04DA2B10  token=0x6000843  System.Boolean LoadAssetLibraryFromMemory(System.Byte[] buffer, System.Boolean bAllowOverwrite, System.Int32& libraryID)
  RVA=0x04DA2AF0  token=0x6000844  System.Boolean CreateNode(System.Int32 parentNodeID, System.String operatorName, System.String nodeLabel, System.Boolean bCookOnCreation, System.Int32& newNodeID)
  RVA=0x041E1670  token=0x6000845  System.Void DeleteNode(System.Int32 nodeID)
  RVA=0x012081B0  token=0x6000846  System.Boolean CookNode(System.Int32 nodeID, System.Boolean bCookTemplatedGeos, System.Boolean bSplitGeosByGroup)
  RVA=0x012081B0  token=0x6000847  System.Boolean CookNodeWithOptions(System.Int32 nodeID, HoudiniEngineUnity.HAPI_CookOptions cookOptions)
  RVA=0x012081B0  token=0x6000848  System.Boolean RenameNode(System.Int32 nodeID, System.String newName)
  RVA=0x012081B0  token=0x6000849  System.Boolean ConnectNodeInput(System.Int32 nodeID, System.Int32 inputIndex, System.Int32 nodeIDToConnect, System.Int32 outputIndex)
  RVA=0x012081B0  token=0x600084A  System.Boolean DisconnectNodeInput(System.Int32 nodeID, System.Int32 inputIndex, System.Boolean bLogError)
  RVA=0x04DA2B40  token=0x600084B  System.Boolean QueryNodeInput(System.Int32 nodeID, System.Int32 inputIndex, System.Int32& connectedNodeID, System.Boolean bLogError)
  RVA=0x04DA2B10  token=0x600084C  System.Boolean GetNodeInputName(System.Int32 nodeID, System.Int32 inputIndex, System.Int32& nodeNameIndex)
  RVA=0x04DA2A70  token=0x600084D  System.Boolean GetAvailableAssetCount(System.Int32 libraryID, System.Int32& assetCount)
  RVA=0x012081B0  token=0x600084E  System.Boolean GetAvailableAssets(System.Int32 libraryID, System.Int32[]& assetNames, System.Int32 assetCount)
  RVA=0x012081B0  token=0x600084F  System.Boolean GetAssetInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_AssetInfo& assetInfo, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000850  System.Boolean GetNodeInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_NodeInfo& nodeInfo, System.Boolean bLogError)
  RVA=0x080165E0  token=0x6000851  System.Boolean GetNodePath(System.Int32 nodeID, System.Int32 relativeNodeID, System.String& path)
  RVA=0x012081B0  token=0x6000852  System.Boolean IsNodeValid(System.Int32 nodeID, System.Int32 uniqueNodeID)
  RVA=0x012081B0  token=0x6000853  System.Boolean ComposeChildNodeList(System.Int32 parentNodeID, System.Int32 nodeTypeFilter, System.Int32 nodeFlagFilter, System.Boolean bRecursive, System.Int32& count, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000854  System.Boolean GetComposedChildNodeList(System.Int32 parentNodeID, System.Int32[] childNodeIDs, System.Int32 count, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000855  System.Boolean LoadHIPFile(System.String fileName, System.Boolean bCookOnLoad)
  RVA=0x012081B0  token=0x6000856  System.Boolean SaveHIPFile(System.String fileName, System.Boolean bLockNodes)
  RVA=0x012081B0  token=0x6000857  System.Boolean GetObjectInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ObjectInfo& objectInfo)
  RVA=0x012081B0  token=0x6000858  System.Boolean GetObjectTransform(System.Int32 nodeID, System.Int32 relativeToNodeID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform& hapiTransform)
  RVA=0x012081B0  token=0x6000859  System.Boolean SetObjectTransform(System.Int32 nodeID, HoudiniEngineUnity.HAPI_TransformEuler& hapiTransform)
  RVA=0x04DA2A70  token=0x600085A  System.Boolean ComposeObjectList(System.Int32 nodeID, System.Int32& objectCount)
  RVA=0x012081B0  token=0x600085B  System.Boolean GetComposedObjectList(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ObjectInfo[] objectInfos, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600085C  System.Boolean GetComposedObjectTransforms(System.Int32 nodeID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transforms, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600085D  System.Boolean GetDisplayGeoInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GeoInfo& geoInfo, System.Boolean bLogError)
  RVA=0x04DA2A70  token=0x600085E  System.Boolean GetOutputGeoCount(System.Int32 nodeID, System.Int32& count, System.Boolean bLogError)
  RVA=0x012081B0  token=0x600085F  System.Boolean GetOutputGeoInfos(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GeoInfo[]& geoInfosArray, System.Int32 count, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000860  System.Boolean GetGeoInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GeoInfo& geoInfo, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000861  System.Boolean GetPartInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
  RVA=0x012081B0  token=0x6000862  System.Boolean GetAttributeInfo(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeOwner owner, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo)
  RVA=0x012081B0  token=0x6000863  System.Boolean GetAttributeNames(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeOwner owner, System.String[]& attributeNames, System.Int32 count)
  RVA=0x012081B0  token=0x6000864  System.Boolean GetAttributeStringData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int32[] dataArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000865  System.Boolean GetAttributeFloatData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Single[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000866  System.Boolean GetAttributeFloatArrayData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Single[]& data, System.Int32 dataLength, System.Int32[]& sizesArray, System.Int32 start, System.Int32 sizesLength)
  RVA=0x012081B0  token=0x6000867  System.Boolean GetAttributeFloat64Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Double[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000868  System.Boolean GetAttributeIntData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int32[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000869  System.Boolean GetAttributeUInt8Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Byte[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600086A  System.Boolean GetAttributeInt8Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.SByte[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600086B  System.Boolean GetAttributeInt16Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int16[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600086C  System.Boolean GetAttributeInt64Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attributeInfo, System.Int64[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600086D  System.Boolean GetGroupNames(System.Int32 nodeID, HoudiniEngineUnity.HAPI_GroupType groupType, System.Int32[]& names, System.Int32 count)
  RVA=0x012081B0  token=0x600086E  System.Boolean GetGroupMembership(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Boolean& membershipArrayAllEqual, System.Int32[] membershipArray, System.Int32 start, System.Int32 length)
  RVA=0x04DA2B20  token=0x600086F  System.Boolean GetGroupCountOnPackedInstancePart(System.Int32 nodeID, System.Int32 partID, System.Int32& pointGroupCount, System.Int32& primitiveGroupCount)
  RVA=0x012081B0  token=0x6000870  System.Boolean GetGroupNamesOnPackedInstancePart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.Int32[]& groupNamesArray, System.Int32 groupCount)
  RVA=0x012081B0  token=0x6000871  System.Boolean GetGroupMembershipOnPackedInstancePart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Boolean& membershipArrayAllEqual, System.Int32[] membershipArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000872  System.Boolean GetInstancedPartIds(System.Int32 nodeID, System.Int32 partID, System.Int32[] instancedPartsArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000873  System.Boolean GetInstancerPartTransforms(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transformsArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000874  System.Boolean GetInstanceTransformsOnPart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transformsArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000875  System.Boolean GetInstancedObjectIds(System.Int32 nodeID, System.Int32[] instanced_node_id_array, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000876  System.Boolean GetFaceCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] faceCounts, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000877  System.Boolean GetFaceCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] faceCounts, System.Int32 start, System.Int32 length, System.Boolean bLogError)
  RVA=0x012081B0  token=0x6000878  System.Boolean GetVertexList(System.Int32 nodeID, System.Int32 partID, System.Int32[] vertexList, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000879  System.Boolean GetBoxInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_BoxInfo& boxInfo)
  RVA=0x012081B0  token=0x600087A  System.Boolean GetSphereInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_SphereInfo& sphereInfo)
  RVA=0x012081B0  token=0x600087B  System.Boolean GetCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_CurveInfo& curveInfo)
  RVA=0x012081B0  token=0x600087C  System.Boolean GetCurveCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] counts, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600087D  System.Boolean GetCurveOrders(System.Int32 nodeID, System.Int32 partID, System.Int32[] orders, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600087E  System.Boolean GetCurveKnots(System.Int32 nodeID, System.Int32 partID, System.Single[] knots, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600087F  System.Boolean GetInputCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_InputCurveInfo& inputCurveInfo)
  RVA=0x012081B0  token=0x6000880  System.Boolean SetPartInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
  RVA=0x012081B0  token=0x6000881  System.Boolean SetFaceCount(System.Int32 nodeID, System.Int32 partID, System.Int32[] faceCounts, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000882  System.Boolean SetVertexList(System.Int32 nodeID, System.Int32 partID, System.Int32[] vertexList, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000883  System.Boolean SetAttributeIntData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Int32[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000884  System.Boolean SetAttributeInt8Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.SByte[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000885  System.Boolean SetAttributeInt16Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Int16[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000886  System.Boolean SetAttributeInt64Data(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Int64[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000887  System.Boolean SetAttributeFloatData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Single[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000888  System.Boolean SetAttributeFloatArrayData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.Single[] data, System.Int32 dataLength, System.Int32[] sizesArray, System.Int32 start, System.Int32 sizesLength)
  RVA=0x012081B0  token=0x6000889  System.Boolean SetAttributeStringData(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, System.String[] data, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600088A  System.Boolean AddAttribute(System.Int32 nodeID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo)
  RVA=0x012081B0  token=0x600088B  System.Boolean AddGroup(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName)
  RVA=0x012081B0  token=0x600088C  System.Boolean DeleteGroup(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName)
  RVA=0x012081B0  token=0x600088D  System.Boolean SetGroupMembership(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Int32[] membershipArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x600088E  System.Boolean CommitGeo(System.Int32 nodeID)
  RVA=0x012081B0  token=0x600088F  System.Boolean RevertGeo(System.Int32 nodeID)
  RVA=0x012081B0  token=0x6000890  System.Boolean SetCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_CurveInfo& curveInfo)
  RVA=0x012081B0  token=0x6000891  System.Boolean SetCurveCounts(System.Int32 nodeID, System.Int32 partID, System.Int32[] counts, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000892  System.Boolean SetCurveOrders(System.Int32 nodeID, System.Int32 partID, System.Int32[] orders, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000893  System.Boolean SetCurveKnots(System.Int32 nodeID, System.Int32 partID, System.Single[] knots, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000894  System.Boolean SetInputCurveInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_InputCurveInfo& curveInfo)
  RVA=0x012081B0  token=0x6000895  System.Boolean SetInputCurvePositions(System.Int32 nodeID, System.Int32 partID, System.Single[] positionsArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x6000896  System.Boolean SetInputCurvePositionsRotationsScales(System.Int32 nodeID, System.Int32 partID, System.Single[] positionsArray, System.Int32 start, System.Int32 length, System.Single[] rotationsArray, System.Int32 rotationsStart, System.Int32 rotationsLength, System.Single[] scalesArray, System.Int32 scalesStart, System.Int32 scalesLength)
  RVA=0x012081B0  token=0x6000897  System.Boolean GetMaterialOnPart(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_MaterialInfo& materialInfo)
  RVA=0x012081B0  token=0x6000898  System.Boolean GetMaterialNodeIDsOnFaces(System.Int32 nodeID, System.Int32 partID, System.Boolean& bSingleFaceMaterial, System.Int32[] materialNodeIDs, System.Int32 faceCount)
  RVA=0x012081B0  token=0x6000899  System.Boolean GetMaterialInfo(System.Int32 materialNodeID, HoudiniEngineUnity.HAPI_MaterialInfo& materialInfo, System.Boolean bLogError)
  RVA=0x012081B0  token=0x600089A  System.Boolean GetImageInfo(System.Int32 materialNodeID, HoudiniEngineUnity.HAPI_ImageInfo& imageInfo)
  RVA=0x012081B0  token=0x600089B  System.Boolean SetImageInfo(System.Int32 materialNodeID, HoudiniEngineUnity.HAPI_ImageInfo& imageInfo)
  RVA=0x012081B0  token=0x600089C  System.Boolean RenderTextureToImage(System.Int32 materialNodeID, System.Int32 parmID, System.Boolean bLogError)
  RVA=0x012081B0  token=0x600089D  System.Boolean RenderCOPToImage(System.Int32 copNodeID)
  RVA=0x09D911E4  token=0x600089E  System.Boolean ExtractImageToMemory(System.Int32 nodeID, System.String fileFormat, System.String imagePlanes, System.Byte[]& buffer)
  RVA=0x09D91228  token=0x600089F  System.Boolean GetImagePlanes(System.Int32 nodeID, System.Int32[] imagePlanes, System.Int32 numImagePlanes)
  RVA=0x09D911C8  token=0x60008A0  System.Boolean ExtractImageToFile(System.Int32 nodeID, System.String fileFormat, System.String imagePlanes, System.String destinationFolderPath, System.String& destinationFilePath)
  RVA=0x012081B0  token=0x60008A1  System.Boolean GetParams(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmInfo[] parmInfos, System.Int32 start, System.Int32 length)
  RVA=0x04DA2B30  token=0x60008A2  System.Boolean GetParmTagName(System.Int32 nodeID, System.Int32 parmID, System.Int32 tagIndex, System.Int32& tagName)
  RVA=0x04DA2B30  token=0x60008A3  System.Boolean GetParmTagValue(System.Int32 nodeID, System.Int32 parmID, System.String tagName, System.Int32& tagValue)
  RVA=0x012081B0  token=0x60008A4  System.Boolean ParmHasTag(System.Int32 nodeID, System.Int32 parmID, System.String tagName, System.Boolean& hasTag)
  RVA=0x012081B0  token=0x60008A5  System.Boolean GetParamIntValues(System.Int32 nodeID, System.Int32[] values, System.Int32 start, System.Int32 length)
  RVA=0x04DA2B30  token=0x60008A6  System.Boolean GetParamIntValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Int32& value)
  RVA=0x012081B0  token=0x60008A7  System.Boolean GetParamFloatValues(System.Int32 nodeID, System.Single[] values, System.Int32 start, System.Int32 length)
  RVA=0x04DA2B30  token=0x60008A8  System.Boolean GetParamFloatValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Single& value)
  RVA=0x012081B0  token=0x60008A9  System.Boolean GetParamStringValues(System.Int32 nodeID, System.Int32[] values, System.Int32 start, System.Int32 length)
  RVA=0x04DA2B30  token=0x60008AA  System.Boolean GetParamStringValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Int32& value)
  RVA=0x04DA2B40  token=0x60008AB  System.Boolean GetParamNodeValue(System.Int32 nodeID, System.String paramName, System.Int32& nodeValue)
  RVA=0x012081B0  token=0x60008AC  System.Boolean GetParamChoiceValues(System.Int32 nodeID, HoudiniEngineUnity.HAPI_ParmChoiceInfo[] values, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x60008AD  System.Boolean SetParamIntValues(System.Int32 nodeID, System.Int32[]& values, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x60008AE  System.Boolean SetParamIntValue(System.Int32 nodeID, System.String paramName, System.Int32 index, System.Int32 value)
  RVA=0x012081B0  token=0x60008AF  System.Boolean SetParamFloatValues(System.Int32 nodeID, System.Single[]& values, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x60008B0  System.Boolean SetParamFloatValue(System.Int32 nodeID, System.String paramName, System.Int32 index, System.Single value)
  RVA=0x012081B0  token=0x60008B1  System.Boolean SetParamStringValue(System.Int32 nodeID, System.String strValue, System.Int32 parmID, System.Int32 index)
  RVA=0x012081B0  token=0x60008B2  System.Boolean SetParamStringValue(System.Int32 nodeID, System.String parmName, System.String parmValue, System.Int32 index)
  RVA=0x012081B0  token=0x60008B3  System.Boolean SetParamNodeValue(System.Int32 nodeID, System.String paramName, System.Int32 nodeValueID)
  RVA=0x012081B0  token=0x60008B4  System.Boolean InsertMultiparmInstance(System.Int32 nodeID, System.Int32 parmID, System.Int32 instancePosition)
  RVA=0x012081B0  token=0x60008B5  System.Boolean RemoveMultiParmInstance(System.Int32 nodeID, System.Int32 parmID, System.Int32 instancePosition)
  RVA=0x012081B0  token=0x60008B6  System.Boolean GetParmWithTag(System.Int32 nodeID, System.String tagName, System.Int32& parmID)
  RVA=0x012081B0  token=0x60008B7  System.Boolean RevertParmToDefault(System.Int32 nodeID, System.String parm_name, System.Int32 index)
  RVA=0x012081B0  token=0x60008B8  System.Boolean RevertParmToDefaults(System.Int32 nodeID, System.String parm_name)
  RVA=0x04DA2B40  token=0x60008B9  System.Boolean GetParmIDFromName(System.Int32 nodeID, System.String parmName, System.Int32& parmID)
  RVA=0x04DA2B50  token=0x60008BA  System.Boolean GetParmStringValue(System.Int32 nodeID, System.String parmName, System.Int32 index, System.Boolean evaluate, System.Int32& value)
  RVA=0x04DA2AE0  token=0x60008BB  System.Boolean CreateInputNode(System.Int32& nodeID, System.String name)
  RVA=0x04DA2AE0  token=0x60008BC  System.Boolean CreateInputCurveNode(System.Int32& nodeID, System.String name)
  RVA=0x04DA2AA0  token=0x60008BD  System.Boolean CreateHeightFieldInput(System.Int32 parentNodeID, System.String name, System.Int32 xSize, System.Int32 ySize, System.Single voxelSize, HoudiniEngineUnity.HAPI_HeightFieldSampling sampling, System.Int32& heightfieldNodeID, System.Int32& heightNodeID, System.Int32& maskNodeID, System.Int32& mergeNodeID)
  RVA=0x04DA2AD0  token=0x60008BE  System.Boolean CreateHeightfieldInputVolumeNode(System.Int32 parentNodeID, System.Int32& newNodeID, System.String name, System.Int32 xSize, System.Int32 ySize, System.Single voxelSize)
  RVA=0x09D91260  token=0x60008BF  System.Boolean GetPreset(System.Int32 nodeID, System.Byte[]& presetData)
  RVA=0x012081B0  token=0x60008C0  System.Boolean SetPreset(System.Int32 nodeID, System.Byte[] presetData)
  RVA=0x012081B0  token=0x60008C1  System.Boolean GetVolumeInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo)
  RVA=0x012081B0  token=0x60008C2  System.Boolean GetHeightFieldData(System.Int32 nodeID, System.Int32 partID, System.Single[] valuesArray, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x60008C3  System.Boolean SetVolumeInfo(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo)
  RVA=0x09D915B4  token=0x60008C4  System.Boolean SetVolumeTileFloatData(System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_VolumeTileInfo& tileInfo, System.Single[] valuesArray, System.Int32 length)
  RVA=0x04DA2B60  token=0x60008C5  System.Boolean GetVolumeBounds(System.Int32 nodeID, System.Int32 partID, System.Single& x_min, System.Single& y_min, System.Single& z_min, System.Single& x_max, System.Single& y_max, System.Single& z_max, System.Single& x_center, System.Single& y_center, System.Single& z_center)
  RVA=0x012081B0  token=0x60008C6  System.Boolean SetHeightFieldData(System.Int32 nodeID, System.Int32 partID, System.String name, System.Single[] valuesArray, System.Int32 start, System.Int32 length)
  RVA=0x04DA2B00  token=0x60008C7  System.Boolean GetActiveCacheCount(System.Int32& activeCacheCount)
  RVA=0x012081B0  token=0x60008C8  System.Boolean GetActiveCacheNames(System.Int32[] cacheNamesArray, System.Int32 activeCacheCount)
  RVA=0x04DA2B10  token=0x60008C9  System.Boolean GetCacheProperty(System.String cacheName, HoudiniEngineUnity.HAPI_CacheProperty cacheProperty, System.Int32& propertyValue)
  RVA=0x012081B0  token=0x60008CA  System.Boolean SetCacheProperty(System.String cacheName, HoudiniEngineUnity.HAPI_CacheProperty cacheProperty, System.Int32 propertyValue)
  RVA=0x012081B0  token=0x60008CB  System.Boolean SaveGeoToFile(System.Int32 nodeID, System.String fileName)
  RVA=0x012081B0  token=0x60008CC  System.Boolean LoadGeoFromFile(System.Int32 nodeID, System.String file_name)
  RVA=0x012081B0  token=0x60008CD  System.Boolean SaveNodeToFile(System.Int32 nodeID, System.String fileName)
  RVA=0x04DA2AF0  token=0x60008CE  System.Boolean LoadNodeFromFile(System.String file_name, System.Int32 parentNodeID, System.String nodeLabel, System.Boolean cook_on_load, System.Int32& newNodeID)
  RVA=0x04DA2B10  token=0x60008CF  System.Boolean GetGeoSize(System.Int32 nodeID, System.String format, System.Int32& size)
  RVA=0x012081B0  token=0x60008D0  System.Boolean GetHandleInfo(System.Int32 nodeID, HoudiniEngineUnity.HAPI_HandleInfo[] handleInfos, System.Int32 start, System.Int32 length)
  RVA=0x012081B0  token=0x60008D1  System.Boolean GetHandleBindingInfo(System.Int32 nodeID, System.Int32 handleIndex, HoudiniEngineUnity.HAPI_HandleBindingInfo[] handleBindingInfos, System.Int32 start, System.Int32 length)
  RVA=0x04DA2A80  token=0x60008D2  System.Boolean ConvertTransform(HoudiniEngineUnity.HAPI_TransformEuler& inTransform, HoudiniEngineUnity.HAPI_RSTOrder RSTOrder, HoudiniEngineUnity.HAPI_XYZOrder ROTOrder, HoudiniEngineUnity.HAPI_TransformEuler& outTransform)
  RVA=0x04DA2B50  token=0x60008D3  System.Boolean GetTotalCookCount(System.Int32 nodeID, System.Int32 nodeTypeFilter, System.Int32 nodeFlagFilter, System.Boolean includeChildren, System.Int32& count)
  RVA=0x012081B0  token=0x60008D4  System.Boolean SetSessionSync(System.Boolean enable)
  RVA=0x012081B0  token=0x60008D5  System.Boolean GetViewport(HoudiniEngineUnity.HAPI_Viewport& viewport)
  RVA=0x012081B0  token=0x60008D6  System.Boolean SetViewport(HoudiniEngineUnity.HAPI_Viewport& viewport)
  RVA=0x012081B0  token=0x60008D7  System.Boolean GetSessionSyncInfo(HoudiniEngineUnity.HAPI_SessionSyncInfo& syncInfo)
  RVA=0x012081B0  token=0x60008D8  System.Boolean SetSessionSyncInfo(HoudiniEngineUnity.HAPI_SessionSyncInfo& syncInfo)
  RVA=0x012081B0  token=0x60008D9  System.Boolean SetNodeDisplay(System.Int32 node_id, System.Int32 onOff)
END_CLASS

CLASS: HoudiniEngineUnity.SessionConnectionState
TYPE:  sealed struct
TOKEN: 0x20000FA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.SessionConnectionStateNOT_CONNECTED  // const
  public    static  HoudiniEngineUnity.SessionConnectionStateCONNECTED  // const
  public    static  HoudiniEngineUnity.SessionConnectionStateFAILED_TO_CONNECT  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.SessionMode
TYPE:  sealed struct
TOKEN: 0x20000FB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.SessionMode  Socket  // const
  public    static  HoudiniEngineUnity.SessionMode  Pipe  // const
  public    static  HoudiniEngineUnity.SessionMode  SharedMemory  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SessionData
TYPE:  sealed class
TOKEN: 0x20000FC
SIZE:  0x68
FIELDS:
  public    static  System.Int64                    INVALID_SESSION_ID  // static @ 0x0
  public            HoudiniEngineUnity.HAPI_Session _HAPISession  // 0x10
  private           System.Int32                    _serverProcessID  // 0x20
  private           System.Boolean                  _initialized  // 0x24
  private           System.String                   _pipeName  // 0x28
  private           System.Int32                    _port  // 0x30
  private           System.String                   _sharedMemoryName  // 0x38
  private           HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType_sharedMemoryBufferType  // 0x40
  private           System.Int32                    _sharedMemoryBufferSize  // 0x44
  private           System.String                   _sessionClassType  // 0x48
  private           System.Boolean                  _isDefaultSession  // 0x50
  private           HoudiniEngineUnity.HEU_SessionSyncData_sessionSync  // 0x58
  private           HoudiniEngineUnity.SessionConnectionState_connectionState  // 0x60
  private           HoudiniEngineUnity.SessionMode  _sessionMode  // 0x64
PROPERTIES:
  SessionID  get=0x09D91784  set=0x011EC9D0
  ProcessID  get=0x04DA2BB0  set=0x04D86330
  SessionType  get=0x011EC580  set=0x014F51F0
  IsInitialized  get=0x012081B0  set=0x04D86880
  IsValidSessionID  get=0x012081B0
  PipeName  get=0x09D916E0  set=0x02FDB880
  Port  get=0x04D864F0  set=0x04D86740
  SharedMemoryName  get=0x09D917D4  set=0x046A91B0
  SharedMemoryBufferType  get=0x04D865E0  set=0x04D86610
  SharedMemoryBufferSize  get=0x04D86530  set=0x04D86580
  SessionClassType  get=0x09D9170C  set=0x09D91800
  IsDefaultSession  get=0x04D86800  set=0x04D86840
  IsSessionSync  get=0x09B00064
  ThisConnectionMode  get=0x04D86790  set=0x04D86D50
  ThisSessionMode  get=0x04D88110  set=0x04D88120
METHODS:
  RVA=0x09D91634  token=0x60008DA  HoudiniEngineUnity.HEU_SessionSyncData GetOrCreateSessionSync()
  RVA=0x02E56440  token=0x60008DB  HoudiniEngineUnity.HEU_SessionSyncData GetSessionSync()
  RVA=0x036E5590  token=0x60008DC  System.Void SetSessionSync(HoudiniEngineUnity.HEU_SessionSyncData syncData)
  RVA=0x04D91890  token=0x60008F9  System.Void .ctor()
  RVA=0x09D916A4  token=0x60008FA  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SessionHAPI
TYPE:  class
TOKEN: 0x20000FD
SIZE:  0x30
EXTENDS: HoudiniEngineUnity.HEU_SessionBase
FIELDS:
METHODS:
  RVA=0x04DA2BA0  token=0x60008FB  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SessionManager
TYPE:  static class
TOKEN: 0x20000FE
SIZE:  0x10
FIELDS:
  private   static  HoudiniEngineUnity.HEU_SessionBase_defaultSession  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.Int64,HoudiniEngineUnity.HEU_SessionBase>_sessionMap  // static @ 0x8
  public    static  HoudiniEngineUnity.HEU_SessionManager.CreateSessionFromTypeDelegate_createSessionFromTypeDelegate  // static @ 0x10
METHODS:
  RVA=0x09D92078  token=0x60008FC  HoudiniEngineUnity.HEU_SessionBase CreateSessionObject()
  RVA=0x09D92034  token=0x60008FD  HoudiniEngineUnity.HEU_SessionBase CreateSessionFromType(System.Type type)
  RVA=0x09D92474  token=0x60008FE  HoudiniEngineUnity.HEU_SessionBase GetDefaultSession()
  RVA=0x09D936CC  token=0x60008FF  System.Void RegisterSession(System.Int64 sessionID, HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D93910  token=0x6000900  System.Void UnregisterSession(System.Int64 sessionID)
  RVA=0x09D92BA4  token=0x6000901  HoudiniEngineUnity.HEU_SessionBase GetSessionWithID(System.Int64 sessionID)
  RVA=0x09D937F4  token=0x6000902  System.Void SaveAllSessionData()
  RVA=0x09D92FBC  token=0x6000903  System.Void LoadAllSessionData()
  RVA=0x09D92E34  token=0x6000904  System.Void InternalValidateSceneAssets()
  RVA=0x09D92674  token=0x6000905  HoudiniEngineUnity.HEU_SessionBase GetOrCreateDefaultSession(System.Boolean bNotifyUserError)
  RVA=0x09D91FB4  token=0x6000906  System.Boolean CreateInProcessSession()
  RVA=0x09D921BC  token=0x6000907  System.Boolean CreateThriftSocketSession(System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean logError)
  RVA=0x09D920BC  token=0x6000908  System.Boolean CreateThriftPipeSession(System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean logError)
  RVA=0x09D9213C  token=0x6000909  System.Boolean CreateThriftSharedMemorySession(System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean logError)
  RVA=0x09D91F34  token=0x600090A  System.Boolean CreateCustomSession()
  RVA=0x09D91EB4  token=0x600090B  System.Boolean ConnectThriftSocketSession(System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout)
  RVA=0x09D91DB4  token=0x600090C  System.Boolean ConnectThriftPipeSession(System.String pipeName, System.Boolean autoClose, System.Single timeout)
  RVA=0x09D91E34  token=0x600090D  System.Boolean ConnectThriftSharedMemorySession(System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout)
  RVA=0x09D93668  token=0x600090E  System.Void RecreateDefaultSessionData()
  RVA=0x09D91D38  token=0x600090F  System.Boolean ConnectSessionSyncUsingThriftSocket(System.String hostName, System.Int32 serverPort, System.Boolean autoClose, System.Single timeout, System.Boolean logError)
  RVA=0x09D91C40  token=0x6000910  System.Boolean ConnectSessionSyncUsingThriftPipe(System.String pipeName, System.Boolean autoClose, System.Single timeout, System.Boolean logError)
  RVA=0x09D91CBC  token=0x6000911  System.Boolean ConnectSessionSyncUsingThriftSharedMemory(System.String sharedMemoryName, HoudiniEngineUnity.HAPI_ThriftSharedMemoryBufferType sharedMemoryBufferType, System.Int32 sharedMemoryBufferSize, System.Boolean autoClose, System.Single timeout, System.Boolean logError)
  RVA=0x09D92DCC  token=0x6000912  System.Boolean InitializeDefaultSession()
  RVA=0x09D91BA4  token=0x6000913  System.Boolean CloseDefaultSession()
  RVA=0x09D91900  token=0x6000914  System.Void CloseAllSessions()
  RVA=0x09D91830  token=0x6000915  System.Void CheckAndCloseExistingSession()
  RVA=0x09D92B00  token=0x6000916  HoudiniEngineUnity.HEU_SessionData GetSessionData()
  RVA=0x09D92B44  token=0x6000917  System.String GetSessionInfo()
  RVA=0x09D93460  token=0x6000918  System.Boolean LoadStoredDefaultSession()
  RVA=0x09D93750  token=0x6000919  System.Boolean RestartSession()
  RVA=0x09D93988  token=0x600091A  System.Boolean ValidatePluginSession(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D9255C  token=0x600091B  System.String GetLastSessionError()
  RVA=0x09D918C4  token=0x600091C  System.Boolean CheckVersionMatch()
  RVA=0x02FFF600  token=0x600091D  System.Boolean ClearConnectionError()
  RVA=0x09D92404  token=0x600091E  System.String GetConnectionError(System.Boolean clear)
  RVA=0x09D92F40  token=0x600091F  System.Boolean IsHARSProcessRunning(System.Int32 processID)
  RVA=0x09D933EC  token=0x6000920  System.Boolean LoadSessionFromHIP(System.Boolean bCookNodes, HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D9389C  token=0x6000921  System.Boolean SaveSessionToHIP(System.Boolean bLockNodes, HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x0464E630  token=0x6000922  System.String GetHoudiniPathOnMacOS(System.String houdiniPath)
  RVA=0x09D934D0  token=0x6000923  System.Boolean OpenHoudini(System.String args)
  RVA=0x09D93584  token=0x6000924  System.Boolean OpenSessionInHoudini(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D92430  token=0x6000925  HoudiniEngineUnity.HAPI_License GetCurrentLicense(System.Boolean bLogError)
  RVA=0x09D92CFC  token=0x6000926  System.String GetString(System.Int32 stringHandle, HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D92C24  token=0x6000927  System.String[] GetStringValuesFromStringIndices(System.Int32[] strIndices)
  RVA=0x09D9251C  token=0x6000928  System.String[] GetGroupNames(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.Boolean isInstanced)
  RVA=0x09D924E4  token=0x6000929  System.Boolean GetGroupMembership(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Int32 partID, HoudiniEngineUnity.HAPI_GroupType groupType, System.String groupName, System.Int32[]& membership, System.Boolean isInstanced)
  RVA=0x09D92614  token=0x600092A  System.String GetNodeName(System.Int32 nodeID, HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D925B4  token=0x600092B  System.Boolean GetNodeInputName(System.Int32 nodeID, System.Int32 inputIndex, System.String& inputName)
  RVA=0x09D9223C  token=0x600092C  System.Boolean GetComposedChildNodeList(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 parentNodeID, System.Int32 nodeTypeFilter, System.Int32 nodeFlagFilter, System.Boolean bRecursive, System.Int32[]& childNodeIDs, System.Boolean bLogIfError)
  RVA=0x09D92288  token=0x600092D  System.Boolean GetComposedObjectListMemorySafe(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_ObjectInfo[] objectInfos, System.Int32 start, System.Int32 length)
  RVA=0x09D92344  token=0x600092E  System.Boolean GetComposedObjectTransformsMemorySafe(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HAPI_RSTOrder rstOrder, HoudiniEngineUnity.HAPI_Transform[] transforms, System.Int32 start, System.Int32 length)
  RVA=0x09D92D64  token=0x600092F  System.String GetUniqueMaterialShopName(System.Int32 assetID, System.Int32 materialID)
  RVA=0x09D939D0  token=0x6000930  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SessionPDG
TYPE:  static class
TOKEN: 0x2000100
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SessionSyncData
TYPE:  class
TOKEN: 0x2000101
SIZE:  0x68
FIELDS:
  private           System.Int32                    _status  // 0x10
  public            System.Single                   _timeLastUpdate  // 0x14
  public            System.Single                   _timeStartConnection  // 0x18
  public            System.String                   _newNodeName  // 0x20
  public            System.Int32                    _nodeTypeIndex  // 0x28
  public            System.Boolean                  _validForConnection  // 0x2c
  public            HoudiniEngineUnity.HAPI_Viewport_viewportHAPI  // 0x30
  public            HoudiniEngineUnity.HAPI_Viewport_viewportLocal  // 0x48
  public            System.Boolean                  _viewportJustUpdated  // 0x60
  public            HoudiniEngineUnity.HAPI_SessionSyncInfo_syncInfo  // 0x61
PROPERTIES:
  SyncStatus  get=0x09D93B1C  set=0x04DA2BC0
METHODS:
  RVA=0x09D93A64  token=0x6000937  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetSync
TYPE:  class
TOKEN: 0x2000103
SIZE:  0x98
EXTENDS: HoudiniEngineUnity.HEU_BaseSync
FIELDS:
  public            HoudiniEngineUnity.HEU_AssetSync.AssetSyncCallback_onAssetLoaded  // 0x88
  public            System.String                   _assetPath  // 0x90
METHODS:
  RVA=0x041E1670  token=0x6000938  System.Void OnEnable()
  RVA=0x09D85394  token=0x6000939  System.Void OnDestroy()
  RVA=0x09D852BC  token=0x600093A  System.Void InitializeAsset(HoudiniEngineUnity.HEU_SessionBase session, System.String assetPath, System.String nodeName, UnityEngine.Transform parent, UnityEngine.Vector3 startPosition)
  RVA=0x09D85260  token=0x600093B  HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo CreateThreadedTask()
  RVA=0x09D85404  token=0x600093C  System.Void SetLoadCallback(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallback callback)
  RVA=0x09D85458  token=0x600093D  System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D8539C  token=0x600093E  System.Void OnLoadComplete(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData)
  RVA=0x09D853D8  token=0x600093F  System.Void Resync()
  RVA=0x09D854D0  token=0x6000940  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SyncedEventData
TYPE:  class
TOKEN: 0x2000105
SIZE:  0x28
FIELDS:
  public            System.Boolean                  CookSuccess  // 0x10
  public            HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadDataTopNodeData  // 0x18
  public            HoudiniEngineUnity.HEU_BaseSync OutputObject  // 0x20
METHODS:
  RVA=0x09D93B2C  token=0x6000945  System.Void .ctor(System.Boolean bSuccess, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData bTopNodeData, HoudiniEngineUnity.HEU_BaseSync bOutputObject)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_BaseSync
TYPE:  class
TOKEN: 0x2000106
SIZE:  0x88
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Int32                    _cookNodeID  // 0x18
  public            System.Int64                    _sessionID  // 0x20
  public            System.String                   _nodeName  // 0x28
  public            System.Boolean                  _initialized  // 0x30
  public            System.Boolean                  _syncing  // 0x31
  public            System.Boolean                  _deleteParent  // 0x32
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeneratedOutput>_generatedOutputs  // 0x38
  public            System.String                   _outputCacheDirectory  // 0x40
  public            System.Collections.Generic.List<System.String>_outputCacheFilePaths  // 0x48
  public            HoudiniEngineUnity.HEU_GenerateOptions_generateOptions  // 0x50
  public            System.Text.StringBuilder       _log  // 0x58
  public            System.Text.StringBuilder       _error  // 0x60
  public            System.Boolean                  _sessionSyncAutoCook  // 0x68
  private           System.Action<HoudiniEngineUnity.HEU_SyncedEventData>_onSynced  // 0x70
  protected         HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo_loadTask  // 0x78
  protected         System.Int32                    _totalCookCount  // 0x80
  protected         System.Boolean                  _firstSyncComplete  // 0x84
PROPERTIES:
  OnSynced  get=0x04D862B0  set=0x063AD760
METHODS:
  RVA=0x041E1670  token=0x6000946  System.Void Awake()
  RVA=0x09D85394  token=0x6000947  System.Void OnDestroy()
  RVA=0x09D8639C  token=0x6000948  System.Void DeleteSessionData()
  RVA=0x09D863C0  token=0x6000949  System.Void DestroyGeneratedData()
  RVA=0x04DA29F0  token=0x600094A  System.Void Initialize()
  RVA=0x09D892E4  token=0x600094B  HoudiniEngineUnity.HEU_SessionBase GetHoudiniSession(System.Boolean bCreateIfNotFound)
  RVA=0x09D893E8  token=0x600094C  System.Int32 GetParentNodeID(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D89418  token=0x600094D  System.Void Log(System.String msg)
  RVA=0x09D860E8  token=0x600094E  System.Void ClearLog()
  RVA=0x09D86608  token=0x600094F  System.Void Error(System.String error)
  RVA=0x09D89404  token=0x6000950  System.Boolean IsLoaded()
  RVA=0x09D89954  token=0x6000951  System.Void StartSync()
  RVA=0x041E1670  token=0x6000952  System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D89A18  token=0x6000953  System.Void StopSync()
  RVA=0x09D89748  token=0x6000954  System.Void Resync()
  RVA=0x09D85DB0  token=0x6000955  System.Void Bake()
  RVA=0x09D89A74  token=0x6000956  System.Void Unload()
  RVA=0x09D8969C  token=0x6000957  System.Void Reset()
  RVA=0x09D894DC  token=0x6000958  System.Void OnLoadComplete(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData)
  RVA=0x09D87EE0  token=0x6000959  System.Void GenerateObjects(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData)
  RVA=0x09D866D8  token=0x600095A  System.Void GenerateGeometry(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData, System.Int32 objIndex)
  RVA=0x09D895E0  token=0x600095B  System.Void OnStopped(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData)
  RVA=0x09D87F60  token=0x600095C  System.Void GenerateTerrain(System.Int32 cookNodeId, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume> terrainBuffers)
  RVA=0x09D87948  token=0x600095D  System.Void GenerateMesh(System.Int32 cookNodeId, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferMesh> meshBuffers)
  RVA=0x09D86628  token=0x600095E  System.Void GenerateAllInstancers(System.Int32 cookNodeId, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferInstancer> instancerBuffers, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData)
  RVA=0x09D867E0  token=0x600095F  System.Void GenerateInstancer(System.Int32 cookNodeId, HoudiniEngineUnity.HEU_LoadBufferInstancer instancerBuffer, System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_LoadBufferBase> idBuffersMap)
  RVA=0x09D872B0  token=0x6000960  System.Void GenerateInstancesFromNodeIDs(System.Int32 cookNodeId, HoudiniEngineUnity.HEU_LoadBufferInstancer instancerBuffer, System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_LoadBufferBase> idBuffersMap, UnityEngine.Transform instanceRootTransform)
  RVA=0x09D86A78  token=0x6000961  System.Void GenerateInstancesFromAssetPaths(HoudiniEngineUnity.HEU_LoadBufferInstancer instancerBuffer, UnityEngine.Transform instanceRootTransform)
  RVA=0x09D86218  token=0x6000962  System.Void CreateNewInstanceFromObject(UnityEngine.GameObject assetSourceGO, System.Int32 instanceIndex, UnityEngine.Transform parentTransform, HoudiniEngineUnity.HAPI_Transform& hapiTransform, System.String[] instancePrefixes, System.String instanceName, UnityEngine.GameObject collisionSourceGO)
  RVA=0x09D85D38  token=0x6000963  System.Void ApplyAttributeModifiersOnGameObjectOutput(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partId, UnityEngine.GameObject& go)
  RVA=0x09D863C8  token=0x6000964  System.Void DestroyOutputs()
  RVA=0x09D89774  token=0x6000965  System.Void SetOutputVisiblity(HoudiniEngineUnity.HEU_LoadBufferBase buffer)
  RVA=0x09D8938C  token=0x6000966  System.String GetOutputCacheDirectory()
  RVA=0x042B4AE0  token=0x6000967  System.Void SetOutputCacheDirectory(System.String directory)
  RVA=0x09D85CB0  token=0x6000968  System.Void AddGeneratedOutputFilePath(System.String path)
  RVA=0x041E1670  token=0x6000969  System.Void SyncUpdate()
  RVA=0x09D89AE8  token=0x600096C  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GenerateOptions
TYPE:  sealed struct
TOKEN: 0x2000107
SIZE:  0x15
FIELDS:
  public            System.Boolean                  _generateUVs  // 0x10
  public            System.Boolean                  _generateTangents  // 0x11
  public            System.Boolean                  _generateNormals  // 0x12
  public            System.Boolean                  _useLODGroups  // 0x13
  public            System.Boolean                  _splitPoints  // 0x14
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GeoSync
TYPE:  class
TOKEN: 0x2000108
SIZE:  0x90
EXTENDS: HoudiniEngineUnity.HEU_BaseSync
FIELDS:
  public            System.String                   _filePath  // 0x88
METHODS:
  RVA=0x09D89C70  token=0x600096D  System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D89D24  token=0x600096E  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_NodeSync
TYPE:  class
TOKEN: 0x2000109
SIZE:  0x90
EXTENDS: HoudiniEngineUnity.HEU_BaseSync
FIELDS:
  public            System.String                   _nodeSaveFilePath  // 0x88
METHODS:
  RVA=0x041E1670  token=0x600096F  System.Void OnEnable()
  RVA=0x09D85394  token=0x6000970  System.Void OnDestroy()
  RVA=0x09D8C72C  token=0x6000971  System.Void InitializeFromHoudini(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.String nodeName, System.String filePath)
  RVA=0x09D8C830  token=0x6000972  System.Void SetupLoadTask(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09D8C7B4  token=0x6000973  System.Boolean SaveNodeToFile(System.String filePath)
  RVA=0x09D8C688  token=0x6000974  System.Void CreateNodeSync(HoudiniEngineUnity.HEU_SessionBase session, System.String opName, System.String nodeNabel)
  RVA=0x09D853D8  token=0x6000975  System.Void Resync()
  RVA=0x09D8C8E0  token=0x6000976  System.Void SyncUpdate()
  RVA=0x09D854D0  token=0x6000977  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Task
TYPE:  abstract class
TOKEN: 0x200010A
SIZE:  0x30
FIELDS:
  public            HoudiniEngineUnity.HEU_Task.TaskStatus_status  // 0x10
  public            HoudiniEngineUnity.HEU_Task.TaskResult_result  // 0x14
  private           System.Guid                     _guid  // 0x18
  public            HoudiniEngineUnity.HEU_Task.TaskCallback_taskCompletedDelegate  // 0x28
PROPERTIES:
  TaskGuid  get=0x04D890C0
METHODS:
  RVA=0x09D94464  token=0x6000979  System.Void .ctor()
  RVA=-1  // abstract  token=0x600097A  System.Void DoTask()
  RVA=0x041E1670  token=0x600097B  System.Void UpdateTask()
  RVA=-1  // abstract  token=0x600097C  System.Void KillTask()
  RVA=-1  // abstract  token=0x600097D  System.Void CompleteTask(HoudiniEngineUnity.HEU_Task.TaskResult result)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_AssetTask
TYPE:  class
TOKEN: 0x200010E
SIZE:  0x60
EXTENDS: HoudiniEngineUnity.HEU_Task
FIELDS:
  public            HoudiniEngineUnity.HEU_AssetTask.BuildType_buildType  // 0x30
  public            HoudiniEngineUnity.HEU_HoudiniAsset_asset  // 0x38
  public            System.String                   _assetPath  // 0x40
  public            UnityEngine.Vector3             _position  // 0x48
  public            System.Boolean                  _buildResult  // 0x54
  public            System.Int64                    _forceSessionID  // 0x58
METHODS:
  RVA=0x09D85A54  token=0x6000982  HoudiniEngineUnity.HEU_SessionBase GetTaskSession()
  RVA=0x09D856B8  token=0x6000983  System.Void DoTask()
  RVA=0x09D85AE4  token=0x6000984  System.Void KillTask()
  RVA=0x09D854D8  token=0x6000985  System.Void CompleteTask(HoudiniEngineUnity.HEU_Task.TaskResult result)
  RVA=0x09D8565C  token=0x6000986  System.Void CookCompletedCallback(HoudiniEngineUnity.HEU_HoudiniAsset asset, System.Boolean bSuccess, System.Collections.Generic.List<UnityEngine.GameObject> outputs)
  RVA=0x09D85634  token=0x6000987  System.Void CookCompletedCallback(HoudiniEngineUnity.HEU_CookedEventData cookedEventData)
  RVA=0x09D85634  token=0x6000988  System.Void CookCompletedCallback(HoudiniEngineUnity.HEU_ReloadEventData reloadEventData)
  RVA=0x09D85C40  token=0x6000989  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_TaskManager
TYPE:  class
TOKEN: 0x2000110
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<HoudiniEngineUnity.HEU_Task>_tasks  // static @ 0x0
  private   static  System.Collections.Generic.List<HoudiniEngineUnity.HEU_Task>_pendingAdd  // static @ 0x8
  private   static  System.Collections.Generic.List<HoudiniEngineUnity.HEU_Task>_pendingRemove  // static @ 0x10
METHODS:
  RVA=0x09D94338  token=0x600098A  System.Void .cctor()
  RVA=0x041E1670  token=0x600098B  System.Void Update()
  RVA=0x09D93F48  token=0x600098C  HoudiniEngineUnity.HEU_Task GetTask(System.Guid taskGuid)
  RVA=0x09D93DF8  token=0x600098D  System.Void AddTask(HoudiniEngineUnity.HEU_Task task)
  RVA=0x09D94110  token=0x600098E  System.Void KillTask(HoudiniEngineUnity.HEU_Task task, System.Boolean bRemove)
  RVA=0x09D941D0  token=0x600098F  System.Void KillTask(System.Guid taskGuid, System.Boolean bRemove)
  RVA=0x09D9424C  token=0x6000990  System.Void RemoveTask(HoudiniEngineUnity.HEU_Task task)
  RVA=0x09D93F14  token=0x6000991  System.Void ExecuteTask(HoudiniEngineUnity.HEU_Task task)
  RVA=0x09D93EF0  token=0x6000992  System.Void CompleteTask(HoudiniEngineUnity.HEU_Task task, HoudiniEngineUnity.HEU_Task.TaskResult result)
  RVA=0x09D940C0  token=0x6000993  System.Void InternalCompleteTask(HoudiniEngineUnity.HEU_Task task)
  RVA=0x041E1670  token=0x6000994  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_LoadBufferBase
TYPE:  class
TOKEN: 0x2000111
SIZE:  0x30
FIELDS:
  public            System.Int32                    _id  // 0x10
  public            System.String                   _name  // 0x18
  public            System.Boolean                  _bInstanced  // 0x20
  public            System.Boolean                  _bInstancer  // 0x21
  public            HoudiniEngineUnity.HEU_GeneratedOutput_generatedOutput  // 0x28
METHODS:
  RVA=0x09DA3330  token=0x6000995  System.Void InitializeBuffer(System.Int32 id, System.String name, System.Boolean bInstanced, System.Boolean bInstancer)
  RVA=0x041E1670  token=0x6000996  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_LoadBufferMesh
TYPE:  class
TOKEN: 0x2000112
SIZE:  0x48
EXTENDS: HoudiniEngineUnity.HEU_LoadBufferBase
FIELDS:
  public            HoudiniEngineUnity.HEU_GenerateGeoCache_geoCache  // 0x30
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup>_LODGroupMeshes  // 0x38
  public            System.Int32                    _defaultMaterialKey  // 0x40
  public            System.Boolean                  _bGenerateUVs  // 0x44
  public            System.Boolean                  _bGenerateTangents  // 0x45
  public            System.Boolean                  _bGenerateNormals  // 0x46
  public            System.Boolean                  _bPartInstanced  // 0x47
METHODS:
  RVA=0x041E1670  token=0x6000997  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_LoadBufferVolume
TYPE:  class
TOKEN: 0x2000113
SIZE:  0xA8
EXTENDS: HoudiniEngineUnity.HEU_LoadBufferBase
FIELDS:
  public            System.Int32                    _tileIndex  // 0x30
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolumeLayer>_splatLayers  // 0x38
  public            System.Int32                    _heightMapWidth  // 0x40
  public            System.Int32                    _heightMapHeight  // 0x44
  public            System.Single[,]                _heightMap  // 0x48
  public            System.Single[,,]               _splatMaps  // 0x50
  public            System.Single                   _terrainSizeX  // 0x58
  public            System.Single                   _terrainSizeY  // 0x5c
  public            System.Single                   _heightRange  // 0x60
  public            UnityEngine.Vector3             _position  // 0x64
  public            System.String                   _terrainDataPath  // 0x70
  public            System.String                   _terrainDataExportPath  // 0x78
  public            HoudiniEngineUnity.HEU_VolumeScatterTrees_scatterTrees  // 0x80
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_DetailPrototype>_detailPrototypes  // 0x88
  public            System.Collections.Generic.List<System.Int32[,]>_detailMaps  // 0x90
  public            HoudiniEngineUnity.HEU_DetailProperties_detailProperties  // 0x98
  public            System.String                   _specifiedTerrainMaterialName  // 0xa0
METHODS:
  RVA=0x09DA3394  token=0x6000998  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_LoadBufferVolumeLayer
TYPE:  class
TOKEN: 0x2000114
SIZE:  0xD8
FIELDS:
  public            System.String                   _layerName  // 0x10
  public            System.Int32                    _partID  // 0x18
  public            System.Int32                    _heightMapWidth  // 0x1c
  public            System.Int32                    _heightMapHeight  // 0x20
  public            System.Single                   _strength  // 0x24
  public            System.String                   _diffuseTexturePath  // 0x28
  public            System.String                   _maskTexturePath  // 0x30
  public            System.Single                   _metallic  // 0x38
  public            System.String                   _normalTexturePath  // 0x40
  public            System.Single                   _normalScale  // 0x48
  public            System.Single                   _smoothness  // 0x4c
  public            UnityEngine.Color               _specularColor  // 0x50
  public            UnityEngine.Vector2             _tileSize  // 0x60
  public            UnityEngine.Vector2             _tileOffset  // 0x68
  public            System.Boolean                  _uiExpanded  // 0x70
  public            System.Int32                    _tile  // 0x74
  public            System.Single[]                 _normalizedHeights  // 0x78
  public            System.Single                   _minHeight  // 0x80
  public            System.Single                   _maxHeight  // 0x84
  public            System.Single                   _heightRange  // 0x88
  public            System.Single                   _terrainSizeX  // 0x8c
  public            System.Single                   _terrainSizeY  // 0x90
  public            UnityEngine.Vector3             _position  // 0x94
  public            UnityEngine.Vector3             _minBounds  // 0xa0
  public            UnityEngine.Vector3             _maxBounds  // 0xac
  public            UnityEngine.Vector3             _center  // 0xb8
  public            System.String                   _layerPath  // 0xc8
  public            System.Boolean                  _hasLayerAttributes  // 0xd0
  public            HoudiniEngineUnity.HFLayerType  _layerType  // 0xd4
METHODS:
  RVA=0x09DA335C  token=0x6000999  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_LoadBufferInstancer
TYPE:  class
TOKEN: 0x2000115
SIZE:  0x58
EXTENDS: HoudiniEngineUnity.HEU_LoadBufferBase
FIELDS:
  public            HoudiniEngineUnity.HAPI_Transform[]_instanceTransforms  // 0x30
  public            System.String[]                 _instancePrefixes  // 0x38
  public            System.Int32[]                  _instanceNodeIDs  // 0x40
  public            System.String[]                 _assetPaths  // 0x48
  public            System.String[]                 _collisionAssetPaths  // 0x50
METHODS:
  RVA=0x041E1670  token=0x600099A  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ThreadedTask
TYPE:  class
TOKEN: 0x2000116
SIZE:  0x38
FIELDS:
  private           System.Boolean                  _isComplete  // 0x10
  private           System.Boolean                  _isActive  // 0x11
  private           System.Boolean                  _stopRequested  // 0x12
  private           System.Object                   _lockHandle  // 0x18
  private           System.Threading.Thread         _thread  // 0x20
  private           System.Threading.ThreadPriority _priority  // 0x28
  private           System.Boolean                  _isBackground  // 0x2c
  protected         System.String                   _name  // 0x30
PROPERTIES:
  TaskName  get=0x011F36E0
  IsComplete  get=0x09DA8030  set=0x09DA8234
  IsActive  get=0x09DA7F84  set=0x09DA8188
  StopRequested  get=0x09DA80DC  set=0x09DA82E0
  Priority  get=0x011F2A90  set=0x011F2AA0
  IsBackground  get=0x04D869F0  set=0x04D86A00
METHODS:
  RVA=0x09DA7D48  token=0x600099B  System.Void Start()
  RVA=0x09DA7E98  token=0x600099C  System.Void Stop()
  RVA=0x09DA7C58  token=0x600099D  System.Void Abort()
  RVA=0x09DA7C88  token=0x600099E  System.Void Reset()
  RVA=0x09DA7ED8  token=0x600099F  System.Void Update()
  RVA=0x041E1670  token=0x60009A0  System.Void DoWork()
  RVA=0x041E1670  token=0x60009A1  System.Void OnComplete()
  RVA=0x041E1670  token=0x60009A2  System.Void OnStopped()
  RVA=0x041E1670  token=0x60009A3  System.Void CleanUp()
  RVA=0x09DA7D20  token=0x60009A4  System.Void Run()
  RVA=0x09DA7F30  token=0x60009B0  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo
TYPE:  class
TOKEN: 0x2000117
SIZE:  0x78
EXTENDS: HoudiniEngineUnity.HEU_ThreadedTask
FIELDS:
  private           HoudiniEngineUnity.HEU_BaseSync _ownerSync  // 0x38
  private           HoudiniEngineUnity.HEU_SessionBase_session  // 0x40
  private           HoudiniEngineUnity.HEU_GenerateOptions_generateOptions  // 0x48
  protected         System.String                   _assetCachePath  // 0x50
  private           HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadType_loadType  // 0x58
  private           System.String                   _filePath  // 0x60
  private           HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData_loadData  // 0x68
  private           HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallback_loadCallback  // 0x70
METHODS:
  RVA=0x09DA7ABC  token=0x60009B1  System.Void SetupLoad(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.LoadType loadType, System.Int32 cookNodeID, System.String name, System.String filePath)
  RVA=0x09DA7A8C  token=0x60009B2  System.Void SetupLoadNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, System.Int32 cookNodeID, System.String name)
  RVA=0x09DA7A5C  token=0x60009B3  System.Void SetupLoadFile(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, System.Int32 cookNodeID, System.String filePath)
  RVA=0x09DA7A28  token=0x60009B4  System.Void SetupLoadAsset(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_BaseSync ownerSync, System.String assetPath, System.String name)
  RVA=0x063AD760  token=0x60009B5  System.Void SetLoadCallback(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadCallback loadCallback)
  RVA=0x09DA4560  token=0x60009B6  System.Void DoWork()
  RVA=0x09DA4188  token=0x60009B7  System.Boolean CookNode(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 cookNodeID)
  RVA=0x09DA75F4  token=0x60009B8  System.Boolean LoadObjectBuffers(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_ObjectInfo& objectInfo)
  RVA=0x09DA7304  token=0x60009B9  System.Boolean LoadNodeBuffer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadObject loadObject)
  RVA=0x09DA3C48  token=0x60009BA  System.Void BuildBufferIDsMap(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData loadData)
  RVA=0x09DA43F8  token=0x60009BB  System.Boolean DoFileLoad()
  RVA=0x09DA42F8  token=0x60009BC  System.Boolean DoAssetLoad()
  RVA=0x09DA77C8  token=0x60009BD  System.Boolean QueryParts(System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& meshParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& volumeParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& instancerParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& curveParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo>& scatterInstancerParts)
  RVA=0x09DA76D0  token=0x60009BE  System.Void OnComplete()
  RVA=0x09DA7758  token=0x60009BF  System.Void OnStopped()
  RVA=0x09B105DC  token=0x60009C0  System.Void CleanUp()
  RVA=0x09DA426C  token=0x60009C1  System.String CreateLogString(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus status, System.String logStr)
  RVA=0x09DA3B20  token=0x60009C2  System.Void AppendLog(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus status, System.String logStr)
  RVA=0x09DA7888  token=0x60009C3  System.Void SetLog(HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadData.LoadStatus status, System.String logStr)
  RVA=0x09DA4214  token=0x60009C4  System.Boolean CreateFileNode(System.Int32& fileNodeID)
  RVA=0x09DA6E54  token=0x60009C5  System.Int32 GetCookNodeID()
  RVA=0x09DA6E70  token=0x60009C6  System.Int32 GetDisplayNodeID(System.Int32 objNodeID)
  RVA=0x09DA784C  token=0x60009C7  System.Boolean SetFileParm(System.Int32 fileNodeID, System.String filePath)
  RVA=0x09DA7C04  token=0x60009C8  System.Void Sleep()
  RVA=0x09DA5380  token=0x60009C9  System.Boolean GenerateTerrainBuffers(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> volumeParts, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> scatterInstancerParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume>& volumeBuffers)
  RVA=0x09DA7648  token=0x60009CA  System.Void LoadStringFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.String& strValue)
  RVA=0x09DA6FB4  token=0x60009CB  System.Void LoadFloatFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Single& floatValue)
  RVA=0x09DA70A4  token=0x60009CC  System.Void LoadLayerColorFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Color& colorValue)
  RVA=0x09DA71F4  token=0x60009CD  System.Void LoadLayerVector2FromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Vector2& vectorValue)
  RVA=0x09DA49A0  token=0x60009CE  System.Boolean GenerateMeshBuffers(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> meshParts, System.Boolean bSplitPoints, System.Boolean bUseLODGroups, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, HoudiniEngineUnity.HEU_ThreadedTaskLoadGeo.HEU_LoadObject loadObject, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferMesh>& meshBuffers)
  RVA=0x09DA4600  token=0x60009CF  System.Boolean GenerateInstancerBuffers(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 nodeID, System.Collections.Generic.List<HoudiniEngineUnity.HAPI_PartInfo> instancerParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferInstancer>& instancerBuffers)
  RVA=0x09DA4F60  token=0x60009D0  HoudiniEngineUnity.HEU_LoadBufferInstancer GeneratePartsInstancerBuffer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String partName, HoudiniEngineUnity.HAPI_PartInfo partInfo)
  RVA=0x09DA5238  token=0x60009D1  HoudiniEngineUnity.HEU_LoadBufferInstancer GeneratePointAttributeInstancerBuffer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String partName, HoudiniEngineUnity.HAPI_PartInfo partInfo)
  RVA=0x09DA6E8C  token=0x60009D2  HoudiniEngineUnity.HEU_LoadBufferVolume GetLoadBufferVolumeFromTileIndex(System.Int32 tileIndex, System.Collections.Generic.List<HoudiniEngineUnity.HEU_LoadBufferVolume> buffers)
  RVA=0x09DA6FA8  token=0x60009D3  System.String GetValidAssetCacheFolderPath(System.String name)
  RVA=0x09DA7C10  token=0x60009D4  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ThreadManager
TYPE:  class
TOKEN: 0x200011E
SIZE:  0x28
FIELDS:
  private   static  HoudiniEngineUnity.HEU_ThreadManager_instance  // static @ 0x0
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTask>_tasks  // 0x10
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTask>_pendingAdd  // 0x18
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_ThreadedTask>_pendingRemove  // 0x20
PROPERTIES:
  Instance  get=0x09DA3AE8
METHODS:
  RVA=0x041E1670  token=0x60009DC  System.Void CreateInstance()
  RVA=0x09DA35D0  token=0x60009DD  System.Void Finalize()
  RVA=0x041E1670  token=0x60009DE  System.Void Register()
  RVA=0x041E1670  token=0x60009DF  System.Void Unregister()
  RVA=0x09DA36BC  token=0x60009E0  System.Void Update()
  RVA=0x09DA3534  token=0x60009E1  System.Void AddTask(HoudiniEngineUnity.HEU_ThreadedTask task)
  RVA=0x09DA3620  token=0x60009E2  System.Void RemoveTask(HoudiniEngineUnity.HEU_ThreadedTask task)
  RVA=0x09DA3A0C  token=0x60009E3  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_CookLogs
TYPE:  class
TOKEN: 0x200011F
SIZE:  0x30
FIELDS:
  private   static  HoudiniEngineUnity.HEU_CookLogs _instance  // static @ 0x0
  private           System.Text.StringBuilder       _cookLogs  // 0x10
  private           System.Int32                    _currentCookLogCount  // 0x18
  private   static  System.Int32                    MAX_COOK_LOG_COUNT  // const
  private           System.String                   _lastLogStr  // 0x20
  private           System.Boolean                  _uniqueStrOnly  // 0x28
  public    static  System.Int64                    MaxLogSize  // const
PROPERTIES:
  Instance  get=0x09D94C14
METHODS:
  RVA=0x09043210  token=0x60009E5  System.String GetCookLogString()
  RVA=0x09D94664  token=0x60009E6  System.Void AppendCookLog(System.String logStr)
  RVA=0x09D94780  token=0x60009E7  System.Void ClearCookLog()
  RVA=0x09D94874  token=0x60009E8  System.String GetCookLogFilePath()
  RVA=0x09D947EC  token=0x60009E9  System.Void DeleteCookingFile()
  RVA=0x09D949AC  token=0x60009EA  System.Void WriteToLogFile(System.String logStr, System.Boolean checkLastLogStr)
  RVA=0x09D94924  token=0x60009EB  System.Int64 GetFileSizeOfLogFile()
  RVA=0x09D94B88  token=0x60009EC  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_EditorUtility
TYPE:  static class
TOKEN: 0x2000120
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60009ED  System.Void MarkSceneDirty()
  RVA=0x041E1670  token=0x60009EE  System.Void SelectObject(UnityEngine.GameObject gameObject)
  RVA=0x041E1670  token=0x60009EF  System.Void SelectObjects(UnityEngine.GameObject[] gameObjects)
  RVA=0x09D954F8  token=0x60009F0  UnityEngine.Vector3 GetSelectedObjectsMeanPosition()
  RVA=0x04DA2BE0  token=0x60009F1  UnityEngine.Matrix4x4 GetSelectedObjectsMeanTransform()
  RVA=0x09D957E4  token=0x60009F2  UnityEngine.GameObject SaveAsPrefabAsset(System.String path, UnityEngine.GameObject go)
  RVA=0x012081B0  token=0x60009F3  System.Boolean PrefabIsAddedComponentOverride(UnityEngine.Component comp)
  RVA=0x012081B0  token=0x60009F4  System.Boolean IsEditorPlaying()
  RVA=0x09D957AC  token=0x60009F5  UnityEngine.GameObject ReplacePrefab(UnityEngine.GameObject go, UnityEngine.Object targetPrefab, HoudiniEngineUnity.HEU_EditorUtility.HEU_ReplacePrefabOptions heuOptions)
  RVA=0x09D95698  token=0x60009F6  System.Boolean IsPrefabInstance(UnityEngine.GameObject go)
  RVA=0x09D95660  token=0x60009F7  System.Boolean IsPrefabAsset(UnityEngine.GameObject go)
  RVA=0x012081B0  token=0x60009F8  System.Boolean IsEditingInPrefabMode(UnityEngine.GameObject obj)
  RVA=0x09D955F0  token=0x60009F9  System.Boolean IsDisconnectedPrefabInstance(UnityEngine.GameObject go)
  RVA=0x09D95448  token=0x60009FA  UnityEngine.Object GetPrefabAsset(UnityEngine.GameObject go)
  RVA=0x09D95410  token=0x60009FB  System.String GetPrefabAssetPath(UnityEngine.Object obj)
  RVA=0x09D94EDC  token=0x60009FC  System.Void DisconnectPrefabInstance(UnityEngine.GameObject instance)
  RVA=0x09D955B8  token=0x60009FD  UnityEngine.Object InstantiatePrefab(UnityEngine.GameObject prefabOriginal)
  RVA=0x09D95548  token=0x60009FE  UnityEngine.GameObject InstantiateGameObject(UnityEngine.GameObject sourceGameObject, UnityEngine.Transform parentTransform, System.Boolean instantiateInWorldSpace, System.Boolean bRegisterUndo)
  RVA=-1  // generic def  token=0x60009FF  UnityEngine.Component AddComponent(UnityEngine.GameObject target, System.Boolean bRegisterUndo)
  RVA=0x041E1670  token=0x6000A00  System.Void UndoRecordObject(UnityEngine.Object objectToUndo, System.String name)
  RVA=0x041E1670  token=0x6000A01  System.Void UndoCollapseCurrentGroup()
  RVA=0x09D94DC8  token=0x6000A02  UnityEngine.Object[] CollectDependencies(UnityEngine.Object obj)
  RVA=0x09D95628  token=0x6000A03  System.Boolean IsPersistant(UnityEngine.Object obj)
  RVA=0x09D95510  token=0x6000A04  System.String GetUniqueNameForSibling(UnityEngine.Transform parentTransform, System.String name)
  RVA=0x09D94FD8  token=0x6000A05  System.Void DisplayProgressBar(System.String title, System.String info, System.Single progress)
  RVA=0x09D94D94  token=0x6000A06  System.Void ClearProgressBar()
  RVA=0x04D85B20  token=0x6000A07  System.Boolean IsEditorNotInPlayModeAndNotGoingToPlayMode()
  RVA=0x09D94F10  token=0x6000A08  System.Boolean DisplayDialog(System.String title, System.String message, System.String ok, System.String cancel)
  RVA=0x09D94F68  token=0x6000A09  System.Boolean DisplayErrorDialog(System.String title, System.String message, System.String ok, System.String cancel)
  RVA=0x041E1670  token=0x6000A0A  System.Void RevealInFinder(System.String path)
  RVA=0x041E1670  token=0x6000A0B  System.Void SetObjectDirtyForEditorUpdate(UnityEngine.Object obj)
  RVA=0x041E1670  token=0x6000A0C  System.Void SetStatic(UnityEngine.GameObject go, System.Boolean bStatic, System.Boolean bIncludeChildren)
  RVA=0x041E1670  token=0x6000A0D  System.Void SetIsHidden(UnityEngine.GameObject go, System.Boolean isHidden, System.Boolean bIncludeChildren)
  RVA=0x011EC580  token=0x6000A0E  UnityEngine.GameObject[] GetSelectedObjects()
  RVA=0x09D95480  token=0x6000A0F  HoudiniEngineUnity.HEU_HoudiniAssetRoot[] GetSelectedAssetRoots()
  RVA=0x09D95364  token=0x6000A10  HoudiniEngineUnity.HEU_HoudiniAssetRoot[] GetAllAssetRoots()
  RVA=0x09D94EC0  token=0x6000A11  System.Void CookSelected()
  RVA=0x09D94E00  token=0x6000A12  System.Void CookAll()
  RVA=0x09D94E1C  token=0x6000A13  System.Void CookAssets(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets)
  RVA=0x09D95790  token=0x6000A14  System.Void RebuildSelected()
  RVA=0x09D956D0  token=0x6000A15  System.Void RebuildAll()
  RVA=0x09D956EC  token=0x6000A16  System.Void RebuildAssets(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets)
  RVA=0x09D94D78  token=0x6000A17  System.Void BakeAndReplaceSelectedInScene()
  RVA=0x09D94CA4  token=0x6000A18  System.Void BakeAndReplaceAllInScene()
  RVA=0x09D94CC0  token=0x6000A19  System.Void BakeAndReplaceAssets(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets)
  RVA=0x09D95348  token=0x6000A1A  System.Void ExportSelectedAssetsToGeoFiles()
  RVA=0x09D95044  token=0x6000A1B  System.Void ExportAllAssetsToGeoFiles()
  RVA=0x09D95060  token=0x6000A1C  System.Void ExportAssetsToGeoFiles(HoudiniEngineUnity.HEU_HoudiniAssetRoot[] rootAssets)
  RVA=0x09D9500C  token=0x6000A1D  System.String EditorSaveFolderPanel(System.String title, System.String folder, System.String defaultName)
  RVA=0x041E1670  token=0x6000A1E  System.Void QuerySelectedMeshTopology()
  RVA=0x09D953E4  token=0x6000A1F  System.String GetObjectParentFolder(UnityEngine.GameObject parentObject, System.Collections.Generic.HashSet<UnityEngine.Material> generatedMaterials)
  RVA=0x09D953B8  token=0x6000A20  System.String GetObjectParentFolderHelper(System.Int32 instanceID)
  RVA=0x041E1670  token=0x6000A21  System.Void RepaintScene()
  RVA=0x041E1670  token=0x6000A22  System.Void SetTextureToNormalMap(System.String filename)
  RVA=0x02FFF600  token=0x6000A23  System.Boolean ReleasedMouse()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Extensions
TYPE:  static class
TOKEN: 0x2000122
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000A24  System.Collections.Generic.List<R> Map(System.Collections.Generic.IEnumerable<T> self, System.Func<T,R> selector)
  RVA=-1  // generic def  token=0x6000A25  T Reduce(System.Collections.Generic.IEnumerable<T> self, System.Func<T,T,T> func)
  RVA=-1  // generic def  token=0x6000A26  System.Collections.Generic.List<T> Filter(System.Collections.Generic.IEnumerable<T> self, System.Func<T,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6000A27  System.Boolean IsValidIndex(System.Collections.Generic.List<T> self, System.Int32 index)
  RVA=-1  // generic def  token=0x6000A28  System.Boolean IsEquivalentList(System.Collections.Generic.List<T> self, System.Collections.Generic.List<T> other)
  RVA=0x09D9581C  token=0x6000A29  System.Boolean ApproximatelyEquals(UnityEngine.Quaternion quatA, UnityEngine.Quaternion value)
  RVA=-1  // generic def  token=0x6000A2A  System.Collections.Generic.List<U> ConvertList(System.Collections.Generic.List<T> self)
  RVA=-1  // generic def  token=0x6000A2B  System.Collections.Generic.List<HoudiniEngineUnity.IEquivable<T>> ConvertListToEquivable(System.Collections.Generic.List<T> self)
  RVA=-1  // generic def  token=0x6000A2C  HoudiniEngineUnity.IEquivable<T>[] ConvertArrayToEquivable(T[] self)
  RVA=0x06935188  token=0x6000A2D  System.Boolean ApproximatelyEquals(System.Single self, System.Single other, System.Single epsilon)
  RVA=0x09D95868  token=0x6000A2E  System.Byte[] AsByteArray(System.String self)
  RVA=0x09D958D4  token=0x6000A2F  System.String AsString(System.Byte[] buffer)
  RVA=0x04DA2C20  token=0x6000A30  UnityEngine.Vector3 SwapXAndY(UnityEngine.Vector3 self)
  RVA=0x04DA2C50  token=0x6000A31  UnityEngine.Vector3 SwapXAndZ(UnityEngine.Vector3 self)
  RVA=0x04DA2C70  token=0x6000A32  UnityEngine.Vector3 SwapYAndZ(UnityEngine.Vector3 self)
  RVA=0x09D6D9B0  token=0x6000A33  UnityEngine.Vector3 DecomposeToPosition(UnityEngine.Matrix4x4 self)
  RVA=0x09D95938  token=0x6000A34  UnityEngine.Quaternion DecomposeToRotation(UnityEngine.Matrix4x4 self)
  RVA=0x09D95A00  token=0x6000A35  UnityEngine.Vector3 DecomposeToScale(UnityEngine.Matrix4x4 self)
END_CLASS

CLASS: HoudiniEngineUnity.ArrayExtensions
TYPE:  static class
TOKEN: 0x2000123
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000A36  System.Void Init(T[] array, T defaultValue)
  RVA=-1  // generic def  token=0x6000A37  System.Void Init(System.Collections.Generic.List<T> array, T defaultValue)
  RVA=-1  // generic def  token=0x6000A38  System.Void CopyToWithResize(T[] srcArray, T[]& destArray)
  RVA=-1  // generic def  token=0x6000A39  System.Boolean IsEquivalentArray(T[] arr, T[] other)
  RVA=-1  // generic def  token=0x6000A3A  System.Boolean IsNull(T[] arr)
END_CLASS

CLASS: HoudiniEngineUnity.DictionaryExtensions
TYPE:  static class
TOKEN: 0x2000124
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000A3B  System.Void AddOrSet(System.Collections.Generic.Dictionary<T,U> dict, T key, U value)
END_CLASS

CLASS: HoudiniEngineUnity.TransformData
TYPE:  sealed struct
TOKEN: 0x2000125
SIZE:  0x60
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Quaternion          rotation  // 0x1c
  public            UnityEngine.Vector3             localPosition  // 0x2c
  public            UnityEngine.Vector3             localScale  // 0x38
  public            UnityEngine.Quaternion          localRotation  // 0x44
  public            UnityEngine.Transform           parent  // 0x58
METHODS:
  RVA=0x09DA8550  token=0x6000A3C  System.Void .ctor(UnityEngine.Transform other)
  RVA=0x09DA84AC  token=0x6000A3D  System.Void CopyTo(UnityEngine.Transform other, System.Boolean copyParent)
  RVA=0x09DA8404  token=0x6000A3E  System.Void CopyToLocal(UnityEngine.Transform other, System.Boolean copyParent)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GeneralUtility
TYPE:  class
TOKEN: 0x2000126
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000A3F  System.Boolean GetArray1Arg(System.Int32 arg1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func, T[] data, System.Int32 start, System.Int32 count)
  RVA=-1  // generic def  token=0x6000A40  System.Boolean GetArray2Arg(System.Int32 arg1, ARG2 arg2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func, T[] data, System.Int32 start, System.Int32 count)
  RVA=-1  // generic def  token=0x6000A41  System.Boolean GetArray3Arg(System.Int32 arg1, ARG2 arg2, ARG3 arg3, HoudiniEngineUnity.HEU_GeneralUtility.GetArray3ArgDel<ARG3,ARG2,T> func, T[] data, System.Int32 start, System.Int32 count)
  RVA=-1  // generic def  token=0x6000A42  System.Boolean GetArray(System.Int32 arg1, ARG2 arg2, ARG3 arg3, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray3ArgDel<ARG3,ARG2,T> func3, T[] data, System.Int32 start, System.Int32 count, System.Int32 tupleSize)
  RVA=-1  // generic def  token=0x6000A43  System.Boolean SetArray1Arg(System.Int32 arg1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func, T[] data, System.Int32 start, System.Int32 count)
  RVA=-1  // generic def  token=0x6000A44  System.Boolean SetArray2Arg(System.Int32 arg1, ARG2 arg2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func, T[] data, System.Int32 start, System.Int32 count)
  RVA=-1  // generic def  token=0x6000A45  System.Boolean SetArray(System.Int32 arg1, ARG2 arg2, HoudiniEngineUnity.HEU_GeneralUtility.GetArray1ArgDel<T> func1, HoudiniEngineUnity.HEU_GeneralUtility.GetArray2ArgDel<ARG2,T> func2, T[] data, System.Int32 start, System.Int32 count, System.Int32 tupleSize)
  RVA=-1  // generic def  token=0x6000A46  System.Boolean DoArrayElementsMatch(T[] array1, T[] array2)
  RVA=-1  // generic def  token=0x6000A47  System.Boolean DoArrayElementsMatch(T[] array1, System.Int32 startOffset1, T[] array2, System.Int32 startOffset2, System.Int32 length)
  RVA=-1  // generic def  token=0x6000A48  System.Boolean GetAttributeArray(System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[] items, HoudiniEngineUnity.HEU_GeneralUtility.GetAttributeArrayInputFunc<T> getFunc, System.Int32 count)
  RVA=-1  // generic def  token=0x6000A49  System.Boolean GetAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[]& data, HoudiniEngineUnity.HEU_GeneralUtility.GetAttributeArrayInputFunc<T> getFunc)
  RVA=-1  // generic def  token=0x6000A4A  System.Boolean GetAttributeStrict(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, T[]& data, HoudiniEngineUnity.HEU_GeneralUtility.GetAttributeArrayInputFunc<T> getFunc)
  RVA=0x09D97EC0  token=0x6000A4B  System.Void GetAttributeStringDataHelper(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, System.Int32[]& data)
  RVA=0x09D97F10  token=0x6000A4C  System.String[] GetAttributeStringData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo)
  RVA=0x09D97C64  token=0x6000A4D  System.String[] GetAttributeDataAsString(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo)
  RVA=-1  // generic def  token=0x6000A4E  System.Boolean SetAttributeArray(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, HoudiniEngineUnity.HEU_GeneralUtility.SetAttributeArrayFunc<T> setFunc, System.Int32 count, System.Int32 start)
  RVA=-1  // generic def  token=0x6000A4F  System.Boolean SetAttribute(System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo, T[] items, HoudiniEngineUnity.HEU_GeneralUtility.SetAttributeArrayFunc<T> setFunc)
  RVA=0x09D6BE10  token=0x6000A50  System.Boolean CheckAttributeExists(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attribName, HoudiniEngineUnity.HAPI_AttributeOwner attribOwner)
  RVA=0x09D97DC0  token=0x6000A51  System.Boolean GetAttributeInfo(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attribName, HoudiniEngineUnity.HAPI_AttributeInfo& attribInfo)
  RVA=0x09D99174  token=0x6000A52  System.Boolean HasValidInstanceAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attribName)
  RVA=0x09D9A27C  token=0x6000A53  System.Void UpdateGeneratedAttributeStore(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject go)
  RVA=0x09D9693C  token=0x6000A54  HoudiniEngineUnity.HEU_OutputAttribute CreateOutputAttributeHelper(System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo)
  RVA=0x09D969BC  token=0x6000A55  HoudiniEngineUnity.HEU_OutputAttribute CreateOutputAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeInfo& attrInfo)
  RVA=0x09D967E4  token=0x6000A56  System.Void CopyWorldTransformValues(UnityEngine.Transform src, UnityEngine.Transform dest)
  RVA=0x09D95AB8  token=0x6000A57  System.Void ApplyTransformTo(UnityEngine.Transform src, UnityEngine.Transform target)
  RVA=0x09D96728  token=0x6000A58  System.Void CopyLocalTransformValues(UnityEngine.Transform src, UnityEngine.Transform dest)
  RVA=0x09D98630  token=0x6000A59  System.Collections.Generic.List<UnityEngine.GameObject> GetChildGameObjects(UnityEngine.GameObject parentGO)
  RVA=0x09D983C8  token=0x6000A5A  System.Collections.Generic.List<UnityEngine.GameObject> GetChildGameObjectsWithNamePattern(UnityEngine.GameObject parentGO, System.String pattern, System.Boolean bExclude)
  RVA=0x09D98A94  token=0x6000A5B  System.Collections.Generic.List<UnityEngine.GameObject> GetInstanceChildObjects(UnityEngine.GameObject parentGO)
  RVA=0x09D98CEC  token=0x6000A5C  System.Collections.Generic.List<UnityEngine.GameObject> GetNonInstanceChildObjects(UnityEngine.GameObject parentGO)
  RVA=0x09D98918  token=0x6000A5D  UnityEngine.GameObject GetGameObjectByName(System.Collections.Generic.List<UnityEngine.GameObject> goList, System.String name)
  RVA=0x09D98828  token=0x6000A5E  UnityEngine.GameObject GetGameObjectByNameInProjectOnly(System.String name)
  RVA=0x09D991A0  token=0x6000A5F  System.Boolean IsGameObjectInProject(UnityEngine.GameObject go)
  RVA=0x09D988E0  token=0x6000A60  UnityEngine.GameObject GetGameObjectByNameInScene(System.String name)
  RVA=0x09D98A5C  token=0x6000A61  HoudiniEngineUnity.HEU_HoudiniAssetRoot GetHDAByGameObjectNameInScene(System.String name)
  RVA=-1  // generic def  token=0x6000A62  T GetOrCreateComponent(UnityEngine.GameObject gameObject)
  RVA=0x09D96F38  token=0x6000A63  System.Void DestroyGeneratedComponents(UnityEngine.GameObject gameObject)
  RVA=0x09D97110  token=0x6000A64  System.Void DestroyGeneratedMeshComponents(UnityEngine.GameObject gameObject)
  RVA=0x09D97600  token=0x6000A65  System.Void DestroyTerrainComponents(UnityEngine.GameObject gameObject)
  RVA=-1  // generic def  token=0x6000A66  System.Void DestroyComponent(UnityEngine.GameObject gameObject)
  RVA=0x09D97384  token=0x6000A67  System.Void DestroyImmediate(UnityEngine.Object obj, System.Boolean bAllowDestroyingAssets, System.Boolean bRegisterUndo)
  RVA=0x09D96E6C  token=0x6000A68  System.Void DestroyBakedGameObjects(System.Collections.Generic.List<UnityEngine.GameObject> gameObjectsToDestroy)
  RVA=0x09D96D60  token=0x6000A69  System.Void DestroyBakedGameObjectsWithEndName(System.Collections.Generic.List<UnityEngine.GameObject> gameObjectsToDestroy, System.String endName)
  RVA=0x09D973B8  token=0x6000A6A  System.Void DestroyLODGroup(UnityEngine.GameObject targetGO, System.Boolean bDontDeletePersistantResources)
  RVA=0x09D98AD4  token=0x6000A6B  System.Collections.Generic.List<UnityEngine.Transform> GetLODTransforms(UnityEngine.GameObject targetGO)
  RVA=0x09D99E1C  token=0x6000A6C  System.Void SetLODTransformValues(UnityEngine.GameObject targetGO, System.Collections.Generic.List<HoudiniEngineUnity.TransformData> transformData)
  RVA=0x09D9719C  token=0x6000A6D  System.Void DestroyGeneratedMeshMaterialsLODGroups(UnityEngine.GameObject targetGO, System.Boolean bDontDeletePersistantResources)
  RVA=0x09D97014  token=0x6000A6E  System.Void DestroyGeneratedMaterial(UnityEngine.Material material)
  RVA=0x09D97524  token=0x6000A6F  System.Void DestroyMeshCollider(UnityEngine.MeshCollider meshCollider, System.Boolean bDontDeletePersistantResources)
  RVA=0x09D99D6C  token=0x6000A70  System.Void SetGameObjectRenderVisiblity(UnityEngine.GameObject gameObject, System.Boolean bVisible)
  RVA=0x09D99C04  token=0x6000A71  System.Void SetGameObjectChildrenRenderVisibility(UnityEngine.GameObject gameObject, System.Boolean bVisible)
  RVA=0x09D99CBC  token=0x6000A72  System.Void SetGameObjectColliderState(UnityEngine.GameObject gameObject, System.Boolean bEnabled)
  RVA=0x09D99B4C  token=0x6000A73  System.Void SetGameObjectChildrenColliderState(UnityEngine.GameObject gameObject, System.Boolean bVisible)
  RVA=0x09D96360  token=0x6000A74  System.String ColorToString(UnityEngine.Color c)
  RVA=0x09D9A184  token=0x6000A75  UnityEngine.Color StringToColor(System.String colorString)
  RVA=0x02FFF600  token=0x6000A76  System.Boolean DoesUnityTagExist(System.String tagName)
  RVA=0x09D99F58  token=0x6000A77  System.Void SetLayer(UnityEngine.GameObject rootGO, System.Int32 layer, System.Boolean bIncludeChildren)
  RVA=0x09D9A0C0  token=0x6000A78  System.Void SetTag(UnityEngine.GameObject rootGO, System.String tag, System.Boolean bIncludeChildren)
  RVA=0x09D9662C  token=0x6000A79  System.Void CopyFlags(UnityEngine.GameObject srcGO, UnityEngine.GameObject dstGO, System.Boolean bIncludeChildren)
  RVA=0x09D9927C  token=0x6000A7A  System.Boolean IsMouseWithinSceneView(UnityEngine.Camera camera, UnityEngine.Vector2 mousePosition)
  RVA=0x09D99230  token=0x6000A7B  System.Boolean IsMouseOverRect(UnityEngine.Camera camera, UnityEngine.Vector2 mousePosition, UnityEngine.Rect& rect)
  RVA=0x09D99040  token=0x6000A7C  System.Type GetSystemTypeByName(System.String typeName)
  RVA=0x09D95CE8  token=0x6000A7D  System.Void AssignUnityTag(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject)
  RVA=0x09D95BCC  token=0x6000A7E  System.Void AssignUnityLayer(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject)
  RVA=0x09D99600  token=0x6000A7F  System.Void MakeStaticIfHasAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject)
  RVA=0x09D99078  token=0x6000A80  System.String GetUnityScriptAttributeValue(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09D982C4  token=0x6000A81  System.String GetAttributeStringValueSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner)
  RVA=0x09D97FD8  token=0x6000A82  System.String GetAttributeStringValueSingleStrict(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner)
  RVA=0x09D97CE0  token=0x6000A83  System.Boolean GetAttributeFloatSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Single& value)
  RVA=0x09D97DE0  token=0x6000A84  System.Boolean GetAttributeIntSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32& value)
  RVA=0x09D97B90  token=0x6000A85  System.Boolean GetAttributeColorSingle(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Color& value)
  RVA=0x09D99150  token=0x6000A86  System.Boolean HasAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, HoudiniEngineUnity.HAPI_AttributeOwner attrOwner)
  RVA=0x09D95DEC  token=0x6000A87  System.Void AttachScriptWithInvokeFunction(System.String scriptSet, UnityEngine.GameObject gameObject)
  RVA=0x09D991A8  token=0x6000A88  System.Boolean IsInCameraView(UnityEngine.Camera camera, UnityEngine.Vector3 point)
  RVA=0x09D97744  token=0x6000A89  System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> FindOrGenerateHandles(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_AssetInfo& assetInfo, System.Int32 assetID, System.String assetName, HoudiniEngineUnity.HEU_Parameters parameters, System.Collections.Generic.List<HoudiniEngineUnity.HEU_Handle> currentHandles)
  RVA=0x041E1670  token=0x6000A8A  System.Void CopyComponents(UnityEngine.GameObject srcGO, UnityEngine.GameObject destGO)
  RVA=0x09D992EC  token=0x6000A8B  UnityEngine.Texture LoadTextureFromFile(System.String filePath)
  RVA=0x09D996F8  token=0x6000A8C  UnityEngine.Texture2D MakeTexture(System.Int32 width, System.Int32 height, UnityEngine.Color color)
  RVA=0x09D99AB4  token=0x6000A8D  System.String ReplaceFirstOccurrence(System.String srcStr, System.String searchStr, System.String replaceStr)
  RVA=0x09D9A01C  token=0x6000A8E  System.Void SetParentWithCleanTransform(UnityEngine.Transform parentTransform, UnityEngine.Transform childTransform)
  RVA=0x09D9669C  token=0x6000A8F  System.Void CopyHAPITransform(HoudiniEngineUnity.HAPI_Transform& src, HoudiniEngineUnity.HAPI_Transform& dest)
  RVA=0x09D98C14  token=0x6000A90  System.String GetMaterialAttributeValueFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09D99988  token=0x6000A91  System.Void ReplaceColliderMeshFromMeshFilter(UnityEngine.GameObject targetGO, UnityEngine.GameObject sourceColliderGO)
  RVA=0x09D99868  token=0x6000A92  System.Void ReplaceColliderMeshFromMeshCollider(UnityEngine.GameObject targetGO, UnityEngine.GameObject sourceColliderGO)
  RVA=0x09D96308  token=0x6000A93  System.Single BiLerpf(System.Single p00, System.Single p10, System.Single p01, System.Single p11, System.Single fracX, System.Single fracY)
  RVA=0x04DA2CA0  token=0x6000A94  System.Single Fractionalf(System.Single value)
  RVA=0x09D99498  token=0x6000A95  System.String LongestCommonPrefix(System.Collections.Generic.List<System.String> list)
  RVA=0x09D98FE4  token=0x6000A96  System.String GetRawOperatorName(System.String assetOpName)
  RVA=0x09D98D2C  token=0x6000A97  UnityEngine.GameObject GetPrefabFromPath(System.String path)
  RVA=0x09D964FC  token=0x6000A98  System.Void ComposeNChildren(UnityEngine.GameObject parent, System.Int32 n, System.Collections.Generic.List<UnityEngine.GameObject>& childGameObjects, System.Boolean destroyIfExists)
  RVA=0x09D96E78  token=0x6000A99  System.Void DestroyChildWithName(UnityEngine.Transform parent, System.String name)
  RVA=0x09D96C04  token=0x6000A9A  System.Void DestroyAutoGeneratedChildren(UnityEngine.GameObject parent)
  RVA=0x09D968A0  token=0x6000A9B  UnityEngine.GameObject CreateNewGameObject(System.String name)
  RVA=0x09D997F8  token=0x6000A9C  System.Void RenameGameObject(UnityEngine.GameObject obj, System.String name)
  RVA=0x041E1670  token=0x6000A9D  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.ReverseCompare
TYPE:  class
TOKEN: 0x200012C
SIZE:  0x10
IMPLEMENTS: System.Collections.IComparer
FIELDS:
METHODS:
  RVA=0x09DA838C  token=0x6000AB2  System.Int32 Compare(System.Object x, System.Object y)
  RVA=0x041E1670  token=0x6000AB3  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GenerateGeoCache
TYPE:  class
TOKEN: 0x200012D
SIZE:  0x2B8
FIELDS:
  private           System.Int32                    <AssetID>k__BackingField  // 0x10
  public            HoudiniEngineUnity.HAPI_GeoInfo _geoInfo  // 0x14
  public            HoudiniEngineUnity.HAPI_PartInfo_partInfo  // 0x38
  public            System.String                   _partName  // 0x68
  public            System.Int32[]                  _vertexList  // 0x70
  public            System.Int32[]                  _faceCounts  // 0x78
  public            System.Int32[]                  _houdiniMaterialIDs  // 0x80
  public            System.Boolean                  _singleFaceUnityMaterial  // 0x88
  public            System.Boolean                  _singleFaceHoudiniMaterial  // 0x89
  public            System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_UnityMaterialInfo>_unityMaterialInfos  // 0x90
  public            HoudiniEngineUnity.HAPI_AttributeInfo_unityMaterialAttrInfo  // 0x98
  public            System.Int32[]                  _unityMaterialAttrName  // 0xc0
  public            System.Collections.Generic.List<System.String>_unityMaterialAttrStringsMap  // 0xc8
  public            HoudiniEngineUnity.HAPI_AttributeInfo_substanceMaterialAttrNameInfo  // 0xd0
  public            System.Int32[]                  _substanceMaterialAttrName  // 0xf8
  public            System.Collections.Generic.List<System.String>_substanceMaterialAttrStringsMap  // 0x100
  public            HoudiniEngineUnity.HAPI_AttributeInfo_substanceMaterialAttrIndexInfo  // 0x108
  public            System.Int32[]                  _substanceMaterialAttrIndex  // 0x130
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData>_inUseMaterials  // 0x138
  public            HoudiniEngineUnity.HAPI_AttributeInfo_posAttrInfo  // 0x140
  public            HoudiniEngineUnity.HAPI_AttributeInfo[]_uvsAttrInfo  // 0x168
  public            HoudiniEngineUnity.HAPI_AttributeInfo_normalAttrInfo  // 0x170
  public            HoudiniEngineUnity.HAPI_AttributeInfo_colorAttrInfo  // 0x198
  public            HoudiniEngineUnity.HAPI_AttributeInfo_alphaAttrInfo  // 0x1c0
  public            HoudiniEngineUnity.HAPI_AttributeInfo_tangentAttrInfo  // 0x1e8
  public            System.Single[]                 _posAttr  // 0x210
  public            System.Single[][]               _uvsAttr  // 0x218
  public            System.Single[]                 _normalAttr  // 0x220
  public            System.Single[]                 _colorAttr  // 0x228
  public            System.Single[]                 _alphaAttr  // 0x230
  public            System.Single[]                 _tangentAttr  // 0x238
  public            System.String[]                 _groups  // 0x240
  public            System.Boolean                  _hasGroupGeometry  // 0x248
  public            System.Collections.Generic.Dictionary<System.String,System.Int32[]>_groupSplitVertexIndices  // 0x250
  public            System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.Int32>>_groupSplitFaceIndices  // 0x258
  public            System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<System.Int32>>_groupVertexOffsets  // 0x260
  public            System.Int32[]                  _allCollisionVertexList  // 0x268
  public            System.Int32[]                  _allCollisionFaceIndices  // 0x270
  public            System.Single                   _normalCosineThreshold  // 0x278
  public            System.Boolean                  _hasLODGroups  // 0x27c
  public            System.Single[]                 _LODTransitionValues  // 0x280
  public            System.Boolean                  _isMeshReadWrite  // 0x288
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo>_colliderInfos  // 0x290
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData>_materialCache  // 0x298
  public            System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MaterialData>_materialIDToDataMap  // 0x2a0
  public            System.String                   _assetCacheFolderPath  // 0x2a8
  public            HoudiniEngineUnity.HEU_MeshIndexFormat_meshIndexFormat  // 0x2b0
PROPERTIES:
  GeoID  get=0x04D86310
  PartID  get=0x04D863B0
  AssetID  get=0x02B2E2D0  set=0x014F51F0
METHODS:
  RVA=0x09DA1A44  token=0x6000AB8  HoudiniEngineUnity.HEU_GenerateGeoCache GetPopulatedGeoCache(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32 geoID, System.Int32 partID, System.Boolean bUseLODGroups, System.Collections.Generic.List<HoudiniEngineUnity.HEU_MaterialData> materialCache, System.String assetCacheFolderPath)
  RVA=0x09DA1AFC  token=0x6000AB9  System.Void GetStringAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String name, HoudiniEngineUnity.HAPI_AttributeInfo& info, System.Int32[]& stringIndices, System.Collections.Generic.List<System.String>& stringTable)
  RVA=0x09DA2378  token=0x6000ABA  System.Void PopulateUnityMaterialData(HoudiniEngineUnity.HEU_SessionBase session)
  RVA=0x09DA18BC  token=0x6000ABB  System.Int32 GetMaterialKeyFromAttributeIndex(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 attributeIndex, System.String& unityMaterialName, System.String& substanceName, System.Int32& substanceIndex)
  RVA=0x09D9AD08  token=0x6000ABC  System.Void CreateMaterialInfoEntryFromAttributeIndex(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 materialAttributeIndex)
  RVA=0x09DA1FE0  token=0x6000ABD  System.Boolean PopulateGeometryData(HoudiniEngineUnity.HEU_SessionBase session, System.Boolean bUseLODGroups)
  RVA=0x09DA1F00  token=0x6000ABE  System.Void ParseLODTransitionAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Single[]& LODTransitionValues)
  RVA=0x09DA2F84  token=0x6000ABF  System.Void UpdateColliders(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutputData outputData)
  RVA=0x09DA2930  token=0x6000AC0  System.Void UpdateCollider(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutputData outputData, HoudiniEngineUnity.HEU_GenerateGeoCache.HEU_ColliderInfo colliderInfo)
  RVA=0x09DA1580  token=0x6000AC1  System.Void GetFinalMaterialsFromComparingNewWithPrevious(UnityEngine.GameObject gameObject, UnityEngine.Material[] previousMaterials, UnityEngine.Material[] newMaterials, UnityEngine.Material[]& finalMaterials)
  RVA=0x09DA1370  token=0x6000AC2  System.Boolean GenerateMeshFromSingleGroup(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GeoGroup GeoGroup, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput, System.Int32 defaultMaterialKey, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bPartInstanced)
  RVA=0x09D9F190  token=0x6000AC3  System.Boolean GenerateLODMeshesFromGeoGroups(HoudiniEngineUnity.HEU_SessionBase session, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup> GeoGroupMeshes, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, HoudiniEngineUnity.HEU_GeneratedOutput generatedOutput, System.Int32 defaultMaterialKey, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bPartInstanced)
  RVA=0x09D9FB88  token=0x6000AC4  System.Boolean GenerateMeshFromGeoGroup(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GeoGroup GeoGroup, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 defaultMaterialKey, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bPartInstanced, UnityEngine.Mesh& newMesh, UnityEngine.Material[]& newMaterials)
  RVA=0x09D9A790  token=0x6000AC5  UnityEngine.Mesh CombineQuadMeshes(System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MeshData> subMeshesMap, System.Collections.Generic.List<System.Int32> subMeshIndices, System.Boolean bGenerateNormals)
  RVA=0x09D9A518  token=0x6000AC6  UnityEngine.Mesh CombineMeshes(System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MeshData> subMeshesMap, System.Collections.Generic.List<System.Int32> submeshIndices, System.Boolean bGenerateUVs, System.Boolean bGenerateNormals, HoudiniEngineUnity.HEU_MeshIndexFormat meshIndexFormat)
  RVA=0x09D9AE14  token=0x6000AC7  UnityEngine.Mesh CreateMeshFromMeshData(HoudiniEngineUnity.HEU_MeshData submesh, System.Boolean bGenerateUVs, System.Boolean bGenerateNormals, HoudiniEngineUnity.HEU_MeshIndexFormat meshIndexFormat)
  RVA=0x09DA2670  token=0x6000AC8  System.Void TransferRegularAttributesToVertices(System.Int32[] groupVertexList, System.Int32[] allFaceCounts, System.Collections.Generic.List<System.Int32> groupFaces, System.Collections.Generic.List<System.Int32> groupVertexOffset, HoudiniEngineUnity.HAPI_AttributeInfo& attribInfo, System.Single[] inData, System.Single[]& outData)
  RVA=0x09D9CCBC  token=0x6000AC9  System.Boolean GenerateGeoGroupUsingGeoCacheVertices(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bUseLODGroups, System.Boolean bPartInstanced, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup>& LODGroupMeshes, System.Int32& defaultMaterialKey)
  RVA=0x09D9B0A4  token=0x6000ACA  System.Boolean GenerateGeoGroupUsingGeoCachePoints(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Boolean bGenerateUVs, System.Boolean bGenerateTangents, System.Boolean bGenerateNormals, System.Boolean bUseLODGroups, System.Boolean bPartInstanced, System.Collections.Generic.List<HoudiniEngineUnity.HEU_GeoGroup>& LODGroupMeshes, System.Int32& defaultMaterialKey)
  RVA=0x09DA1C5C  token=0x6000ACB  System.Void GetSubmeshIDForFace(HoudiniEngineUnity.HEU_GenerateGeoCache geoCache, System.Int32 faceMaterialID, System.Int32 groupFace, System.Int32 vertexFaceIndex, System.Int32[] groupVertexList, System.Int32 defaultMaterialKey, System.Int32& submeshID, System.Int32& singleFaceUnityMaterialKey, System.Int32& singleFaceHoudiniMaterialKey)
  RVA=0x09D9A3E4  token=0x6000ACC  UnityEngine.MeshTopology CalculateGroupMeshTopology(System.Collections.Generic.List<System.Int32> groupFaces, System.Int32[] allFaceCounts)
  RVA=0x09DA30C4  token=0x6000ACD  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GeoGroup
TYPE:  class
TOKEN: 0x2000131
SIZE:  0x28
IMPLEMENTS: System.IComparable`1
FIELDS:
  public            System.String                   _groupName  // 0x10
  public            System.Collections.Generic.Dictionary<System.Int32,HoudiniEngineUnity.HEU_MeshData>_subMeshesMap  // 0x18
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_VertexEntry>[]_sharedNormalIndices  // 0x20
METHODS:
  RVA=0x09DA8618  token=0x6000AD3  System.Int32 CompareTo(HoudiniEngineUnity.HEU_GeoGroup other)
  RVA=0x09DA8650  token=0x6000AD4  System.Void SetupNormalIndices(System.Int32 indicesCount)
  RVA=0x09DA8744  token=0x6000AD5  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_VertexEntry
TYPE:  class
TOKEN: 0x2000132
SIZE:  0x20
FIELDS:
  public            System.Int32                    _meshKey  // 0x10
  public            System.Int32                    _vertexIndex  // 0x14
  public            System.Int32                    _normalIndex  // 0x18
METHODS:
  RVA=0x04D88580  token=0x6000AD6  System.Void .ctor(System.Int32 meshKey, System.Int32 vertexIndex, System.Int32 normalIndex)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_GeometryUtility
TYPE:  static class
TOKEN: 0x2000133
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DA9CF4  token=0x6000AD7  UnityEngine.Vector2[] GeneratePerTriangle(UnityEngine.Mesh meshSrc)
  RVA=0x09DA9D2C  token=0x6000AD8  System.Void GenerateSecondaryUVSet(UnityEngine.Mesh meshsrc)
  RVA=0x09DA87BC  token=0x6000AD9  System.Void CalculateMeshTangents(UnityEngine.Mesh mesh)
  RVA=0x09DA8F60  token=0x6000ADA  UnityEngine.Mesh GenerateCubeMeshFromPoints(UnityEngine.Vector3[] points, UnityEngine.Color[] pointsColor, System.Single size)
  RVA=0x09DA9D60  token=0x6000ADB  System.String GetInstanceOutputName(System.String partName, System.String[] userPrefix, System.Int32 index)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputData
TYPE:  class
TOKEN: 0x2000134
SIZE:  0x18
FIELDS:
  public            UnityEngine.GameObject          _inputObject  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000ADC  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterface
TYPE:  abstract class
TOKEN: 0x2000135
SIZE:  0x18
FIELDS:
  public    static  System.Int32                    DEFAULT_PRIORITY  // const
  protected         System.Int32                    _priority  // 0x10
PROPERTIES:
  Priority  get=0x02B2E2D0
METHODS:
  RVA=0x014F51F0  token=0x6000ADE  System.Void .ctor(System.Int32 priority)
  RVA=0x09DAFE14  token=0x6000ADF  System.Void RegisterInterface()
  RVA=-1  // abstract  token=0x6000AE0  System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject)
  RVA=-1  // abstract  token=0x6000AE1  System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterfaceMeshSettings
TYPE:  class
TOKEN: 0x2000136
SIZE:  0x18
FIELDS:
  private           System.Boolean                  _exportColliders  // 0x10
PROPERTIES:
  ExportColliders  get=0x015EFCE0  set=0x04D86130
METHODS:
  RVA=0x041E1670  token=0x6000AE4  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterfaceMesh
TYPE:  class
TOKEN: 0x2000137
SIZE:  0x20
EXTENDS: HoudiniEngineUnity.HEU_InputInterface
FIELDS:
  private           HoudiniEngineUnity.HEU_InputInterfaceMeshSettingssettings  // 0x18
METHODS:
  RVA=0x04DA2CD0  token=0x6000AE5  System.Void .ctor()
  RVA=0x05392C40  token=0x6000AE6  System.Void Initialize(HoudiniEngineUnity.HEU_InputInterfaceMeshSettings settings)
  RVA=0x09DA9FD4  token=0x6000AE7  System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID)
  RVA=0x09DAAB48  token=0x6000AE8  System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject)
  RVA=0x09DAAAC4  token=0x6000AE9  System.Void GetUVsFromMesh(UnityEngine.Mesh mesh, UnityEngine.Vector2[] srcUVs, System.Collections.Generic.List<UnityEngine.Vector3> destUVs, System.Int32 index)
  RVA=0x09DAB928  token=0x6000AEA  System.Boolean UploadData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 inputNodeID, HoudiniEngineUnity.HEU_InputData inputData)
  RVA=0x09DAB3F4  token=0x6000AEB  System.Boolean UploadColliderData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 mergeNodeID, HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMeshes inputData, System.Int32 parentNodeId)
  RVA=0x09DAAC64  token=0x6000AEC  System.Boolean UploadBoxColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.BoxCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID)
  RVA=0x09DAD038  token=0x6000AED  System.Boolean UploadSphereColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.SphereCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID)
  RVA=0x09DAADD8  token=0x6000AEE  System.Boolean UploadCapsuleColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.CapsuleCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID)
  RVA=0x09DACD54  token=0x6000AEF  System.Boolean UploadMeshColliderData(HoudiniEngineUnity.HEU_SessionBase session, UnityEngine.MeshCollider collider, System.Int32 inputIndex, System.Int32 parentNodeID, System.Int32& inputNodeID)
  RVA=0x09DAA8D4  token=0x6000AF0  System.String GetColliderGroupBaseName(UnityEngine.Collider collider, System.Boolean bIsConvex, System.Boolean bIsSimple, System.Boolean bIsRendered)
  RVA=0x09DA9F60  token=0x6000AF1  System.Boolean CreateInputNodeForCollider(HoudiniEngineUnity.HEU_SessionBase session, System.Int32& outNodeID, System.Int32 parentNodeId, System.Int32 colliderIndex, System.String colliderName, System.Single[] colliderVertices, System.Int32[] colliderIndices)
  RVA=0x09DAA5FC  token=0x6000AF2  HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMeshes GenerateMeshDatasFromGameObject(UnityEngine.GameObject inputObject, System.Boolean bExportColliders)
  RVA=0x09DAA114  token=0x6000AF3  HoudiniEngineUnity.HEU_InputInterfaceMesh.HEU_InputDataMesh CreateSingleMeshData(UnityEngine.GameObject meshGameObject, System.Boolean bExportColliders)
  RVA=0x09DAA9C4  token=0x6000AF4  UnityEngine.Mesh GetMeshFromObject(UnityEngine.GameObject meshGameObject)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterfaceSplineSettings
TYPE:  class
TOKEN: 0x200013C
SIZE:  0x18
FIELDS:
  private           System.Single                   _samplingResolution  // 0x10
PROPERTIES:
  SamplingResolution  get=0x04D85F70  set=0x04D88280
METHODS:
  RVA=0x04DA2CE0  token=0x6000AFA  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterfaceSpline
TYPE:  class
TOKEN: 0x200013D
SIZE:  0x20
EXTENDS: HoudiniEngineUnity.HEU_InputInterface
FIELDS:
  private           HoudiniEngineUnity.HEU_InputInterfaceSplineSettingssettings  // 0x18
METHODS:
  RVA=0x04DA2CD0  token=0x6000AFB  System.Void .ctor()
  RVA=0x09DAD6C8  token=0x6000AFC  System.Void Initialize(HoudiniEngineUnity.HEU_InputInterfaceSplineSettings settings)
  RVA=0x09DAD734  token=0x6000AFD  System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject)
  RVA=0x09DAD1B0  token=0x6000AFE  System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID)
  RVA=0x09DAD338  token=0x6000AFF  HoudiniEngineUnity.HEU_InputInterfaceSpline.HEU_InputDataSplineContainer GenerateSplineDataFromGameObject(UnityEngine.GameObject inputObject)
  RVA=0x09DAD7C8  token=0x6000B00  System.Boolean UploadData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 inputNodeID, HoudiniEngineUnity.HEU_InputInterfaceSpline.HEU_InputDataSpline inputSpline, UnityEngine.Matrix4x4 localToWorld)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterfaceTerrain
TYPE:  class
TOKEN: 0x2000140
SIZE:  0x18
EXTENDS: HoudiniEngineUnity.HEU_InputInterface
FIELDS:
METHODS:
  RVA=0x04DA2CD0  token=0x6000B03  System.Void .ctor()
  RVA=0x09DAD8C8  token=0x6000B04  System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID)
  RVA=0x09DADF14  token=0x6000B05  System.Boolean SetMaskLayer(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt, HoudiniEngineUnity.HAPI_VolumeInfo& baseVolumeInfo)
  RVA=0x09DADE14  token=0x6000B06  System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject)
  RVA=0x09DAD850  token=0x6000B07  System.Boolean CreateHeightFieldInputNode(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt)
  RVA=0x09DAE758  token=0x6000B08  System.Boolean UploadHeightValuesWithTransform(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo)
  RVA=0x09DAE264  token=0x6000B09  System.Boolean UploadAlphaMaps(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain idt, HoudiniEngineUnity.HAPI_VolumeInfo& baseVolumeInfo, System.Boolean& bMaskSet)
  RVA=0x09DADEAC  token=0x6000B0A  System.Boolean SetHeightFieldData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 volumeNodeID, System.Int32 partID, System.Single[] heightValues, System.String heightFieldName, HoudiniEngineUnity.HAPI_VolumeInfo& baseVolumeInfo)
  RVA=0x09DAE018  token=0x6000B0B  System.Boolean SetTerrainDataAttributesToHeightField(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainData terrainData)
  RVA=0x09DAE09C  token=0x6000B0C  System.Boolean SetTerrainLayerAttributesToHeightField(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainLayer terrainLayer)
  RVA=0x09DAE140  token=0x6000B0D  System.Void SetTreePrototypes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainData terrainData)
  RVA=0x09DAE120  token=0x6000B0E  System.Void SetTreeInstances(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoNodeID, System.Int32 partID, UnityEngine.TerrainData terrainData)
  RVA=0x09DADA44  token=0x6000B0F  HoudiniEngineUnity.HEU_InputInterfaceTerrain.HEU_InputDataTerrain GenerateTerrainDataFromGameObject(UnityEngine.GameObject inputObject)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings
TYPE:  class
TOKEN: 0x2000142
SIZE:  0x18
FIELDS:
  public            System.Boolean                  _createGroupsForTiles  // 0x10
  public            System.Boolean                  _exportUnusedTiles  // 0x11
  public            System.Boolean                  _applyTileColor  // 0x12
  public            System.Boolean                  _applyTilemapOrientation  // 0x13
METHODS:
  RVA=0x04DA2CF0  token=0x6000B11  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputInterfaceTilemap
TYPE:  class
TOKEN: 0x2000143
SIZE:  0x20
EXTENDS: HoudiniEngineUnity.HEU_InputInterface
FIELDS:
  private           HoudiniEngineUnity.HEU_InputInterfaceTilemapSettingssettings  // 0x18
METHODS:
  RVA=0x04DA2CD0  token=0x6000B12  System.Void .ctor()
  RVA=0x09DAE9E8  token=0x6000B13  System.Void Initialize(HoudiniEngineUnity.HEU_InputInterfaceTilemapSettings settings)
  RVA=0x09DAE844  token=0x6000B14  System.Boolean CreateInputNodeWithDataUpload(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 connectNodeID, UnityEngine.GameObject inputObject, System.Int32& inputNodeID)
  RVA=0x09DAEA54  token=0x6000B15  System.Boolean IsThisInputObjectSupported(UnityEngine.GameObject inputObject)
  RVA=0x09DAEAE8  token=0x6000B16  System.Boolean UploadData(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 inputNodeID, HoudiniEngineUnity.HEU_InputData inputData)
  RVA=0x09DAE91C  token=0x6000B17  HoudiniEngineUnity.HEU_InputInterfaceTilemap.HEU_InputDataTilemap GenerateTilemapDataFromGameObject(UnityEngine.GameObject inputObject)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputMeshUtility
TYPE:  static class
TOKEN: 0x2000145
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DAFFB0  token=0x6000B19  System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo, System.Boolean bConvertToHoudiniCoordinateSystem)
  RVA=0x09DB033C  token=0x6000B1A  System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, System.Single[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
  RVA=0x09DB0494  token=0x6000B1B  System.Boolean SetMeshVertexAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3[] data, System.Int32[] indices, HoudiniEngineUnity.HAPI_PartInfo& partInfo, System.Boolean bConvertToHoudiniCoordinateSystem)
  RVA=0x09DB05E8  token=0x6000B1C  System.Boolean SetMeshVertexFloatAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, System.Single[] data, System.Int32[] indices, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
  RVA=0x09DB073C  token=0x6000B1D  System.Boolean UploadMeshIntoHoudiniNode(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetNodeID, System.Int32 objectID, System.Int32 geoID, UnityEngine.Mesh& mesh)
  RVA=0x09DB0108  token=0x6000B1E  System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3Int[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
  RVA=0x09DB0258  token=0x6000B1F  System.Boolean SetMeshPointAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.String[] data, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
  RVA=0x09DAFE60  token=0x6000B20  System.Boolean SetMeshDetailAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Int32 tupleSize, UnityEngine.Vector3 data, HoudiniEngineUnity.HAPI_PartInfo& partInfo)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InputUtility
TYPE:  static class
TOKEN: 0x2000146
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputInterface>_inputInterfaces  // static @ 0x0
METHODS:
  RVA=0x09DB0CD4  token=0x6000B21  System.Int32 GetHighestPriority()
  RVA=0x09DB1078  token=0x6000B22  System.Void RegisterInputInterface(HoudiniEngineUnity.HEU_InputInterface inputInterface)
  RVA=0x09DB1278  token=0x6000B23  System.Void UnregisterInputInterface(HoudiniEngineUnity.HEU_InputInterface inputInterface)
  RVA=0x09DB0D78  token=0x6000B24  HoudiniEngineUnity.HEU_InputInterface GetInputInterfaceByType(System.Type type)
  RVA=0x09DB0F70  token=0x6000B25  HoudiniEngineUnity.HEU_InputInterface GetInputInterface(UnityEngine.GameObject inputObject)
  RVA=0x09DB0EA4  token=0x6000B26  HoudiniEngineUnity.HEU_InputInterface GetInputInterface(HoudiniEngineUnity.HEU_InputObjectInfo inputObjectInfo)
  RVA=0x09DB0A20  token=0x6000B27  System.Boolean CreateInputNodeWithMultiObjects(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 assetID, System.Int32& connectMergeID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputObjectInfo>& inputObjects, System.Collections.Generic.List<System.Int32>& inputObjectsConnectedAssetIDs, HoudiniEngineUnity.HEU_InputNode inputNode)
  RVA=0x09DB0964  token=0x6000B28  System.Boolean CreateInputNodeWithMultiAssets(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset parentAsset, System.Int32& connectMergeID, System.Collections.Generic.List<HoudiniEngineUnity.HEU_InputHDAInfo>& inputAssetInfos, System.Boolean bKeepWorldTransform, System.Int32 mergeParentID)
  RVA=0x09DB12E8  token=0x6000B29  System.Boolean UploadInputObjectTransform(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_InputObjectInfo inputObject, System.Int32 inputNodeID, System.Boolean bKeepWorldTransform)
  RVA=0x09DB0B78  token=0x6000B2A  System.Void GetChildrenTransforms(UnityEngine.Transform transform, System.Collections.Generic.List<UnityEngine.Matrix4x4>& childTransforms)
  RVA=0x09DB156C  token=0x6000B2B  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Logger
TYPE:  class
TOKEN: 0x2000147
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DB18D4  token=0x6000B2C  System.Void Log(System.String text)
  RVA=0x09DB1730  token=0x6000B2D  System.Void LogFormat(System.String text, System.Object[] args)
  RVA=0x09DB1888  token=0x6000B2E  System.Void LogWarning(System.String text)
  RVA=0x09DB1828  token=0x6000B2F  System.Void LogWarningFormat(System.String text, System.Object[] args)
  RVA=0x09DB1668  token=0x6000B30  System.Void LogError(System.String text)
  RVA=0x09DB1608  token=0x6000B31  System.Void LogErrorFormat(System.String text, System.Object[] args)
  RVA=0x09DB16B4  token=0x6000B32  System.Void LogError(System.Exception ex)
  RVA=0x09DB1600  token=0x6000B33  System.Void LogAssertion(System.String text)
  RVA=0x09DB15F8  token=0x6000B34  System.Void LogAssertionFormat(System.String text, System.Object[] args)
  RVA=0x09DB17E8  token=0x6000B35  System.Void LogToCookLogsIfOn(System.String text)
  RVA=0x09DB178C  token=0x6000B36  System.Void LogToCookLogsIfOnFormat(System.String text, System.Object[] args)
  RVA=0x041E1670  token=0x6000B37  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_MeshData
TYPE:  class
TOKEN: 0x2000148
SIZE:  0x58
FIELDS:
  public            System.Collections.Generic.List<System.Int32>_indices  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Vector3>_vertices  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Color32>_colors  // 0x20
  public            System.Collections.Generic.List<UnityEngine.Vector3>_normals  // 0x28
  public            System.Collections.Generic.List<UnityEngine.Vector4>_tangents  // 0x30
  public            System.Collections.Generic.List<UnityEngine.Vector4>[]_uvs  // 0x38
  public            System.Collections.Generic.List<UnityEngine.Vector3>_triangleNormals  // 0x40
  public            System.Collections.Generic.Dictionary<System.Int32,System.Int32>_pointIndexToMeshIndexMap  // 0x48
  public            UnityEngine.MeshTopology        _meshTopology  // 0x50
METHODS:
  RVA=0x09DB1920  token=0x6000B38  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_MeshIndexFormat
TYPE:  class
TOKEN: 0x2000149
SIZE:  0x18
FIELDS:
  public            UnityEngine.Rendering.IndexFormat_indexFormat  // 0x10
METHODS:
  RVA=0x09DB1B58  token=0x6000B39  System.Void CalculateIndexFormat(System.Int32 numVertices)
  RVA=0x09DB1BB4  token=0x6000B3A  System.Void SetFormatForMesh(UnityEngine.Mesh mesh)
  RVA=0x041E1670  token=0x6000B3B  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_PipelineType
TYPE:  sealed struct
TOKEN: 0x200014A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HEU_PipelineTypeUnsupported  // const
  public    static  HoudiniEngineUnity.HEU_PipelineTypeBiRP  // const
  public    static  HoudiniEngineUnity.HEU_PipelineTypeURP  // const
  public    static  HoudiniEngineUnity.HEU_PipelineTypeHDRP  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_RenderingPipelineDefines
TYPE:  class
TOKEN: 0x200014B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DB1F50  token=0x6000B3C  System.Void .cctor()
  RVA=0x09DB1E8C  token=0x6000B3D  System.Void UpdateDefines()
  RVA=0x09DB1CDC  token=0x6000B3E  HoudiniEngineUnity.HEU_PipelineType GetPipeline()
  RVA=0x09DB1BD8  token=0x6000B3F  System.Void AddDefine(System.String define)
  RVA=0x09DB1DE8  token=0x6000B40  System.Void RemoveDefine(System.String define)
  RVA=0x09DB1C7C  token=0x6000B41  System.Collections.Generic.List<System.String> GetDefines()
  RVA=0x041E1670  token=0x6000B42  System.Void SetDefines(System.Collections.Generic.List<System.String> definesList)
  RVA=0x041E1670  token=0x6000B43  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_SerializableDictionary`2
TYPE:  class
TOKEN: 0x200014C
IMPLEMENTS: System.Collections.Generic.IDictionary`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.Collections.Generic.Dictionary<TKey,TValue>_dictionary  // 0x0
  private           TKey[]                          _keys  // 0x0
  private           TValue[]                        _values  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
  Keys  get=-1  // not resolved
  Values  get=-1  // not resolved
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B4A  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x6000B4B  System.Void Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000B4C  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000B4D  System.Boolean Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000B4E  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000B4F  System.Void CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000B50  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000B51  System.Boolean Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000B52  System.Boolean Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000B53  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000B54  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000B55  System.Void OnAfterDeserialize()
  RVA=-1  // not resolved  token=0x6000B56  System.Void OnBeforeSerialize()
  RVA=-1  // not resolved  token=0x6000B57  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ShelfToolData
TYPE:  class
TOKEN: 0x200014D
SIZE:  0x50
FIELDS:
  public            System.String                   _name  // 0x10
  public            HoudiniEngineUnity.HEU_ShelfToolData.ToolType_toolType  // 0x18
  public            System.String                   _toolTip  // 0x20
  public            System.String                   _iconPath  // 0x28
  public            System.String                   _assetPath  // 0x30
  public            System.String                   _helpURL  // 0x38
  public            System.String[]                 _targets  // 0x40
  public            System.String                   _jsonPath  // 0x48
METHODS:
  RVA=0x09DB1F58  token=0x6000B58  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_Shelf
TYPE:  class
TOKEN: 0x200014F
SIZE:  0x30
FIELDS:
  public            System.String                   _shelfName  // 0x10
  public            System.String                   _shelfPath  // 0x18
  public            System.Boolean                  _defaultShelf  // 0x20
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_ShelfToolData>_tools  // 0x28
METHODS:
  RVA=0x09DB5398  token=0x6000B59  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ShelfTools
TYPE:  class
TOKEN: 0x2000150
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<HoudiniEngineUnity.HEU_Shelf>_shelves  // static @ 0x0
  private   static  System.Boolean                  _shelvesLoaded  // static @ 0x8
  private   static  System.Int32                    _currentSelectedShelf  // static @ 0xc
  public    static  System.String                   TARGET_ALL  // const
  public    static  System.String                   TARGET_UNITY  // const
METHODS:
  RVA=0x09DB219C  token=0x6000B5A  System.Boolean AreShelvesLoaded()
  RVA=0x09DB5228  token=0x6000B5B  System.Void SetReloadShelves()
  RVA=0x09DB21EC  token=0x6000B5C  System.Void ClearShelves()
  RVA=0x09DB320C  token=0x6000B5D  System.Int32 GetNumShelves()
  RVA=0x09DB31BC  token=0x6000B5E  System.Int32 GetCurrentShelfIndex()
  RVA=0x09DB51D4  token=0x6000B5F  System.Void SetCurrentShelf(System.Int32 index)
  RVA=0x09DB3410  token=0x6000B60  HoudiniEngineUnity.HEU_Shelf GetShelf(System.Int32 index)
  RVA=0x09DB32B4  token=0x6000B61  HoudiniEngineUnity.HEU_Shelf GetShelf(System.String shelfName)
  RVA=0x09DB3268  token=0x6000B62  System.String GetShelfStorageEntry(System.String shelfName, System.String shelfPath)
  RVA=0x09DB34B4  token=0x6000B63  System.Void GetSplitShelfEntry(System.String shelfEntry, System.String& shelfName, System.String& shelfPath)
  RVA=0x09DB3B48  token=0x6000B64  System.Void LoadShelves()
  RVA=0x09DB4C6C  token=0x6000B65  System.Boolean LoadToolsFromDirectory(System.String folderPath, System.Collections.Generic.List<HoudiniEngineUnity.HEU_ShelfToolData>& tools)
  RVA=0x09DB4224  token=0x6000B66  HoudiniEngineUnity.HEU_ShelfToolData LoadToolFromJsonFile(System.String jsonFilePath)
  RVA=0x09DB43C0  token=0x6000B67  HoudiniEngineUnity.HEU_ShelfToolData LoadToolFromJsonString(System.String json, System.String jsonFilePath)
  RVA=0x09DB1FFC  token=0x6000B68  HoudiniEngineUnity.HEU_Shelf AddShelf(System.String shelfName, System.String shelfPath)
  RVA=0x09DB4E90  token=0x6000B69  System.Void RemoveShelf(System.Int32 shelfIndex)
  RVA=0x09DB4FC0  token=0x6000B6A  System.Void SaveShelf()
  RVA=0x09DB2F14  token=0x6000B6B  System.Void ExecuteTool(System.Int32 toolSlot)
  RVA=0x09DB22B0  token=0x6000B6C  System.Void ExecuteToolGenerator(System.String toolName, System.String toolPath, UnityEngine.Vector3 targetPosition, UnityEngine.Quaternion targetRotation, UnityEngine.Vector3 targetScale)
  RVA=0x09DB39B8  token=0x6000B6D  System.Boolean IsValidInputMesh(UnityEngine.GameObject gameObject)
  RVA=0x09DB3928  token=0x6000B6E  System.Boolean IsValidInputHDA(UnityEngine.GameObject gameObject)
  RVA=0x09DB5278  token=0x6000B6F  System.Boolean ShouldUseHDA(UnityEngine.GameObject[] gameObjectList)
  RVA=0x09DB244C  token=0x6000B70  System.Void ExecuteToolNoInput(System.String toolName, System.String toolPath)
  RVA=0x09DB299C  token=0x6000B71  System.Void ExecuteToolOperatorSingle(System.String toolName, System.String toolPath, UnityEngine.GameObject[] inputObjects)
  RVA=0x09DB2580  token=0x6000B72  System.Void ExecuteToolOperatorMultiple(System.String toolName, System.String toolPath, UnityEngine.GameObject[] inputObjects)
  RVA=0x09DB2250  token=0x6000B73  System.Void ExecuteToolBatch(System.String toolName, System.String toolPath, UnityEngine.GameObject[] batchObjects)
  RVA=0x09DB386C  token=0x6000B74  System.String GetToolResourcePath(HoudiniEngineUnity.HEU_ShelfToolData tool, System.String inPath, System.String ext)
  RVA=0x09DB3704  token=0x6000B75  System.String GetToolIconPath(HoudiniEngineUnity.HEU_ShelfToolData tool, System.String inPath)
  RVA=0x09DB3594  token=0x6000B76  System.String GetToolAssetPath(HoudiniEngineUnity.HEU_ShelfToolData tool, System.String inPath)
  RVA=0x041E1670  token=0x6000B77  System.Void .ctor()
  RVA=0x09DB52F8  token=0x6000B78  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.JSONNodeType
TYPE:  sealed struct
TOKEN: 0x2000152
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.JSONNodeType Array  // const
  public    static  HoudiniEngineUnity.JSONNodeType Object  // const
  public    static  HoudiniEngineUnity.JSONNodeType String  // const
  public    static  HoudiniEngineUnity.JSONNodeType Number  // const
  public    static  HoudiniEngineUnity.JSONNodeType NullValue  // const
  public    static  HoudiniEngineUnity.JSONNodeType Boolean  // const
  public    static  HoudiniEngineUnity.JSONNodeType None  // const
  public    static  HoudiniEngineUnity.JSONNodeType Custom  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.JSONTextMode
TYPE:  sealed struct
TOKEN: 0x2000153
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.JSONTextMode Compact  // const
  public    static  HoudiniEngineUnity.JSONTextMode Indent  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.JSONNode
TYPE:  abstract class
TOKEN: 0x2000154
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  forceASCII  // static @ 0x0
  public    static  System.Boolean                  longAsString  // static @ 0x1
  private   static  System.Text.StringBuilder       m_EscapeBuilder  // static @ 0xffffffff
  public    static  HoudiniEngineUnity.JSONContainerTypeVectorContainerType  // static @ 0x4
  public    static  HoudiniEngineUnity.JSONContainerTypeQuaternionContainerType  // static @ 0x8
  public    static  HoudiniEngineUnity.JSONContainerTypeRectContainerType  // static @ 0xc
PROPERTIES:
  Tag  get=-1  // abstract
  Item  get=0x011EC580  set=0x041E1670
  Item  get=0x011EC580  set=0x041E1670
  Value  get=0x09DB7C0C  set=0x041E1670
  Count  get=0x011EC580
  IsNumber  get=0x012081B0
  IsString  get=0x012081B0
  IsBoolean  get=0x012081B0
  IsNull  get=0x012081B0
  IsArray  get=0x012081B0
  IsObject  get=0x012081B0
  Inline  get=0x012081B0  set=0x041E1670
  Children  get=0x09DB7980
  DeepChildren  get=0x09DB79D4
  Linq  get=0x09DB7BAC
  Keys  get=0x09DB7AF8
  Values  get=0x09DB7AF8
  AsDouble  get=0x09DB7840  set=0x09DB85E8
  AsInt  get=0x09DB78E8  set=0x09DB8660
  AsFloat  get=0x09DB78CC  set=0x09DB864C
  AsBool  get=0x09DB77A4  set=0x09DB857C
  AsLong  get=0x09DB7904  set=0x09DB8678
  AsArray  get=0x09DB7768
  AsObject  get=0x09DB7944
  EscapeBuilder  get=0x09DB7A44
METHODS:
  RVA=0x041E1670  token=0x6000B8B  System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem)
  RVA=0x09DB5410  token=0x6000B8C  System.Void Add(HoudiniEngineUnity.JSONNode aItem)
  RVA=0x011EC580  token=0x6000B8D  HoudiniEngineUnity.JSONNode Remove(System.String aKey)
  RVA=0x011EC580  token=0x6000B8E  HoudiniEngineUnity.JSONNode Remove(System.Int32 aIndex)
  RVA=0x04D88C30  token=0x6000B8F  HoudiniEngineUnity.JSONNode Remove(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DB6BEC  token=0x6000B92  System.String ToString()
  RVA=0x09DB6C70  token=0x6000B93  System.String ToString(System.Int32 aIndent)
  RVA=-1  // abstract  token=0x6000B94  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
  RVA=-1  // abstract  token=0x6000B95  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x09DB7D60  token=0x6000BA5  HoudiniEngineUnity.JSONNode op_Implicit(System.String s)
  RVA=0x09DB8234  token=0x6000BA6  System.String op_Implicit(HoudiniEngineUnity.JSONNode d)
  RVA=0x09DB8360  token=0x6000BA7  HoudiniEngineUnity.JSONNode op_Implicit(System.Double n)
  RVA=0x09DB7D20  token=0x6000BA8  System.Double op_Implicit(HoudiniEngineUnity.JSONNode d)
  RVA=0x09DB8274  token=0x6000BA9  HoudiniEngineUnity.JSONNode op_Implicit(System.Single n)
  RVA=0x09DB84F0  token=0x6000BAA  System.Single op_Implicit(HoudiniEngineUnity.JSONNode d)
  RVA=0x09DB7E38  token=0x6000BAB  HoudiniEngineUnity.JSONNode op_Implicit(System.Int32 n)
  RVA=0x09DB80EC  token=0x6000BAC  System.Int32 op_Implicit(HoudiniEngineUnity.JSONNode d)
  RVA=0x09DB7ED0  token=0x6000BAD  HoudiniEngineUnity.JSONNode op_Implicit(System.Int64 n)
  RVA=0x09DB84B0  token=0x6000BAE  System.Int64 op_Implicit(HoudiniEngineUnity.JSONNode d)
  RVA=0x09DB7E88  token=0x6000BAF  HoudiniEngineUnity.JSONNode op_Implicit(System.Boolean b)
  RVA=0x09DB82E8  token=0x6000BB0  System.Boolean op_Implicit(HoudiniEngineUnity.JSONNode d)
  RVA=0x09DB8328  token=0x6000BB1  HoudiniEngineUnity.JSONNode op_Implicit(System.Collections.Generic.KeyValuePair<System.String,HoudiniEngineUnity.JSONNode> aKeyValue)
  RVA=0x09DB7C38  token=0x6000BB2  System.Boolean op_Equality(HoudiniEngineUnity.JSONNode a, System.Object b)
  RVA=0x09DB8568  token=0x6000BB3  System.Boolean op_Inequality(HoudiniEngineUnity.JSONNode a, System.Object b)
  RVA=0x02FB1840  token=0x6000BB4  System.Boolean Equals(System.Object obj)
  RVA=0x04D87100  token=0x6000BB5  System.Int32 GetHashCode()
  RVA=0x09DB5458  token=0x6000BB7  System.String Escape(System.String aText)
  RVA=0x09DB5744  token=0x6000BB8  HoudiniEngineUnity.JSONNode ParseElement(System.String token, System.Boolean quoted)
  RVA=0x09DB5898  token=0x6000BB9  HoudiniEngineUnity.JSONNode Parse(System.String aJSON)
  RVA=0x09DB56C0  token=0x6000BBA  HoudiniEngineUnity.JSONNode GetContainer(HoudiniEngineUnity.JSONContainerType aType)
  RVA=0x09DB7F90  token=0x6000BBB  HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Vector2 aVec)
  RVA=0x09DB802C  token=0x6000BBC  HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Vector3 aVec)
  RVA=0x09DB7DC0  token=0x6000BBD  HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Vector4 aVec)
  RVA=0x09DB841C  token=0x6000BBE  HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Quaternion aRot)
  RVA=0x09DB81BC  token=0x6000BBF  HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.Rect aRect)
  RVA=0x09DB83B0  token=0x6000BC0  HoudiniEngineUnity.JSONNode op_Implicit(UnityEngine.RectOffset aRect)
  RVA=0x09DB8494  token=0x6000BC1  UnityEngine.Vector2 op_Implicit(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DB8530  token=0x6000BC2  UnityEngine.Vector3 op_Implicit(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DB818C  token=0x6000BC3  UnityEngine.Vector4 op_Implicit(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DB812C  token=0x6000BC4  UnityEngine.Quaternion op_Implicit(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DB815C  token=0x6000BC5  UnityEngine.Rect op_Implicit(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DB82CC  token=0x6000BC6  UnityEngine.RectOffset op_Implicit(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DB6580  token=0x6000BC7  UnityEngine.Vector2 ReadVector2(UnityEngine.Vector2 aDefault)
  RVA=0x09DB6668  token=0x6000BC8  UnityEngine.Vector2 ReadVector2(System.String aXName, System.String aYName)
  RVA=0x09DB6708  token=0x6000BC9  UnityEngine.Vector2 ReadVector2()
  RVA=0x09DB7364  token=0x6000BCA  HoudiniEngineUnity.JSONNode WriteVector2(UnityEngine.Vector2 aVec, System.String aXName, System.String aYName)
  RVA=0x09DB6720  token=0x6000BCB  UnityEngine.Vector3 ReadVector3(UnityEngine.Vector3 aDefault)
  RVA=0x09DB689C  token=0x6000BCC  UnityEngine.Vector3 ReadVector3(System.String aXName, System.String aYName, System.String aZName)
  RVA=0x09DB6984  token=0x6000BCD  UnityEngine.Vector3 ReadVector3()
  RVA=0x09DB745C  token=0x6000BCE  HoudiniEngineUnity.JSONNode WriteVector3(UnityEngine.Vector3 aVec, System.String aXName, System.String aYName, System.String aZName)
  RVA=0x09DB69D4  token=0x6000BCF  UnityEngine.Vector4 ReadVector4(UnityEngine.Vector4 aDefault)
  RVA=0x09DB6BB0  token=0x6000BD0  UnityEngine.Vector4 ReadVector4()
  RVA=0x09DB759C  token=0x6000BD1  HoudiniEngineUnity.JSONNode WriteVector4(UnityEngine.Vector4 aVec)
  RVA=0x09DB5F18  token=0x6000BD2  UnityEngine.Quaternion ReadQuaternion(UnityEngine.Quaternion aDefault)
  RVA=0x09DB5EDC  token=0x6000BD3  UnityEngine.Quaternion ReadQuaternion()
  RVA=0x09DB6D94  token=0x6000BD4  HoudiniEngineUnity.JSONNode WriteQuaternion(UnityEngine.Quaternion aRot)
  RVA=0x09DB63A4  token=0x6000BD5  UnityEngine.Rect ReadRect(UnityEngine.Rect aDefault)
  RVA=0x09DB6370  token=0x6000BD6  UnityEngine.Rect ReadRect()
  RVA=0x09DB7198  token=0x6000BD7  HoudiniEngineUnity.JSONNode WriteRect(UnityEngine.Rect aRect)
  RVA=0x09DB60F4  token=0x6000BD8  UnityEngine.RectOffset ReadRectOffset(UnityEngine.RectOffset aDefault)
  RVA=0x09DB6308  token=0x6000BD9  UnityEngine.RectOffset ReadRectOffset()
  RVA=0x09DB6F60  token=0x6000BDA  HoudiniEngineUnity.JSONNode WriteRectOffset(UnityEngine.RectOffset aRect)
  RVA=0x09DB5E28  token=0x6000BDB  UnityEngine.Matrix4x4 ReadMatrix()
  RVA=0x09DB6D00  token=0x6000BDC  HoudiniEngineUnity.JSONNode WriteMatrix(UnityEngine.Matrix4x4 aMatrix)
  RVA=0x041E1670  token=0x6000BDD  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.JSONArray
TYPE:  class
TOKEN: 0x200015C
SIZE:  0x20
EXTENDS: HoudiniEngineUnity.JSONNode
FIELDS:
  private           System.Collections.Generic.List<HoudiniEngineUnity.JSONNode>m_List  // 0x10
  private           System.Boolean                  inline  // 0x18
PROPERTIES:
  Inline  get=0x04D86340  set=0x04D86350
  Tag  get=0x04D85B80
  IsArray  get=0x02FFF600
  Item  get=0x09DC0A88  set=0x09DC0B28
  Item  get=0x09DC0A88  set=0x09DC0B28
  Count  get=0x09DC09E8
  Children  get=0x09DC0978
METHODS:
  RVA=0x09DC0614  token=0x6000C0D  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x09DC058C  token=0x6000C13  System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem)
  RVA=0x09DC06A4  token=0x6000C14  HoudiniEngineUnity.JSONNode Remove(System.Int32 aIndex)
  RVA=0x09DC0748  token=0x6000C15  HoudiniEngineUnity.JSONNode Remove(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DC07A0  token=0x6000C17  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
  RVA=0x09DC0900  token=0x6000C18  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.JSONObject
TYPE:  class
TOKEN: 0x200015E
SIZE:  0x20
EXTENDS: HoudiniEngineUnity.JSONNode
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,HoudiniEngineUnity.JSONNode>m_Dict  // 0x10
  private           System.Boolean                  inline  // 0x18
PROPERTIES:
  Inline  get=0x04D86340  set=0x04D86350
  Tag  get=0x02BBE540
  IsObject  get=0x02FFF600
  Item  get=0x09DC268C  set=0x09DC27D8
  Item  get=0x09DC268C  set=0x09DC27D8
  Count  get=0x09DC264C
  Children  get=0x09DC25DC
METHODS:
  RVA=0x09DC1E74  token=0x6000C26  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x09DC1D28  token=0x6000C2C  System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem)
  RVA=0x09DC2184  token=0x6000C2D  HoudiniEngineUnity.JSONNode Remove(System.String aKey)
  RVA=0x09DC1F18  token=0x6000C2E  HoudiniEngineUnity.JSONNode Remove(System.Int32 aIndex)
  RVA=0x09DC1FD8  token=0x6000C2F  HoudiniEngineUnity.JSONNode Remove(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x09DC223C  token=0x6000C31  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
  RVA=0x09DC2564  token=0x6000C32  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.JSONString
TYPE:  class
TOKEN: 0x2000161
SIZE:  0x18
EXTENDS: HoudiniEngineUnity.JSONNode
FIELDS:
  private           System.String                   m_Data  // 0x10
PROPERTIES:
  Tag  get=0x04D8C9E0
  IsString  get=0x02FFF600
  Value  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x04DA2D20  token=0x6000C40  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x053908C0  token=0x6000C43  System.Void .ctor(System.String aData)
  RVA=0x09DC2A50  token=0x6000C44  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
  RVA=0x09DC29AC  token=0x6000C45  System.Boolean Equals(System.Object obj)
  RVA=0x08479504  token=0x6000C46  System.Int32 GetHashCode()
END_CLASS

CLASS: HoudiniEngineUnity.JSONNumber
TYPE:  class
TOKEN: 0x2000162
SIZE:  0x18
EXTENDS: HoudiniEngineUnity.JSONNode
FIELDS:
  private           System.Double                   m_Data  // 0x10
PROPERTIES:
  Tag  get=0x04D882B0
  IsNumber  get=0x02FFF600
  Value  get=0x09DC1C5C  set=0x09DC1CA8
  AsDouble  get=0x04D96320  set=0x04D85BA0
  AsLong  get=0x04DA2D40  set=0x04DA2D50
METHODS:
  RVA=0x04DA2D20  token=0x6000C49  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x04D85BA0  token=0x6000C50  System.Void .ctor(System.Double aData)
  RVA=0x09DC0D94  token=0x6000C51  System.Void .ctor(System.String aData)
  RVA=0x09DC1C28  token=0x6000C52  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
  RVA=0x09DC1AE0  token=0x6000C53  System.Boolean IsNumeric(System.Object value)
  RVA=0x09DC1A00  token=0x6000C54  System.Boolean Equals(System.Object obj)
  RVA=0x09DC1AD4  token=0x6000C55  System.Int32 GetHashCode()
END_CLASS

CLASS: HoudiniEngineUnity.JSONBool
TYPE:  class
TOKEN: 0x2000163
SIZE:  0x18
EXTENDS: HoudiniEngineUnity.JSONNode
FIELDS:
  private           System.Boolean                  m_Data  // 0x10
PROPERTIES:
  Tag  get=0x04D9EC60
  IsBoolean  get=0x02FFF600
  Value  get=0x09DC0DA8  set=0x09DC0DFC
  AsBool  get=0x015EFCE0  set=0x04D86130
METHODS:
  RVA=0x04DA2D20  token=0x6000C58  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x04D86130  token=0x6000C5D  System.Void .ctor(System.Boolean aData)
  RVA=0x09DC0D94  token=0x6000C5E  System.Void .ctor(System.String aData)
  RVA=0x09DC0D28  token=0x6000C5F  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
  RVA=0x09DC0C84  token=0x6000C60  System.Boolean Equals(System.Object obj)
  RVA=0x09DC0CE0  token=0x6000C61  System.Int32 GetHashCode()
END_CLASS

CLASS: HoudiniEngineUnity.JSONNull
TYPE:  class
TOKEN: 0x2000164
SIZE:  0x10
EXTENDS: HoudiniEngineUnity.JSONNode
FIELDS:
  private   static  HoudiniEngineUnity.JSONNull     m_StaticInstance  // static @ 0x0
  public    static  System.Boolean                  reuseSameInstance  // static @ 0x8
PROPERTIES:
  Tag  get=0x04D9EC50
  IsNull  get=0x02FFF600
  Value  get=0x09DC19D4  set=0x041E1670
  AsBool  get=0x012081B0  set=0x041E1670
METHODS:
  RVA=0x09DC1850  token=0x6000C62  HoudiniEngineUnity.JSONNull CreateOrGet()
  RVA=0x041E1670  token=0x6000C63  System.Void .ctor()
  RVA=0x04DA2D20  token=0x6000C66  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x09DC18BC  token=0x6000C6B  System.Boolean Equals(System.Object obj)
  RVA=0x011EC580  token=0x6000C6C  System.Int32 GetHashCode()
  RVA=0x09DC1914  token=0x6000C6D  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
  RVA=0x09DC195C  token=0x6000C6E  System.Void .cctor()
END_CLASS

CLASS: HoudiniEngineUnity.JSONLazyCreator
TYPE:  class
TOKEN: 0x2000165
SIZE:  0x20
EXTENDS: HoudiniEngineUnity.JSONNode
FIELDS:
  private           HoudiniEngineUnity.JSONNode     m_Node  // 0x10
  private           System.String                   m_Key  // 0x18
PROPERTIES:
  Tag  get=0x04D9BE00
  Item  get=0x09DC1368  set=0x09DC1718
  Item  get=0x09DC1368  set=0x09DC1718
  AsInt  get=0x09DC11A0  set=0x09DC15A4
  AsFloat  get=0x09DC1138  set=0x09DC152C
  AsDouble  get=0x09DC10D0  set=0x09DC14B8
  AsLong  get=0x09DC1208  set=0x09DC161C
  AsBool  get=0x09DC1068  set=0x09DC1448
  AsArray  get=0x09DC0FF0
  AsObject  get=0x09DC12F0
METHODS:
  RVA=0x04DA2D20  token=0x6000C70  HoudiniEngineUnity.JSONNode.Enumerator GetEnumerator()
  RVA=0x03E8CD90  token=0x6000C71  System.Void .ctor(HoudiniEngineUnity.JSONNode aNode)
  RVA=0x032AF420  token=0x6000C72  System.Void .ctor(HoudiniEngineUnity.JSONNode aNode, System.String aKey)
  RVA=-1  // generic def  token=0x6000C73  T Set(T aVal)
  RVA=0x09DC0F04  token=0x6000C78  System.Void Add(HoudiniEngineUnity.JSONNode aItem)
  RVA=0x09DC0E60  token=0x6000C79  System.Void Add(System.String aKey, HoudiniEngineUnity.JSONNode aItem)
  RVA=0x09DC0F98  token=0x6000C7A  System.Boolean op_Equality(HoudiniEngineUnity.JSONLazyCreator a, System.Object b)
  RVA=0x09DC1438  token=0x6000C7B  System.Boolean op_Inequality(HoudiniEngineUnity.JSONLazyCreator a, System.Object b)
  RVA=0x09DC0F98  token=0x6000C7C  System.Boolean Equals(System.Object obj)
  RVA=0x011EC580  token=0x6000C7D  System.Int32 GetHashCode()
  RVA=0x09DC0FA8  token=0x6000C8A  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, HoudiniEngineUnity.JSONTextMode aMode)
END_CLASS

CLASS: HoudiniEngineUnity.JSON
TYPE:  static class
TOKEN: 0x2000166
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC2ABC  token=0x6000C8B  HoudiniEngineUnity.JSONNode Parse(System.String aJSON)
END_CLASS

CLASS: HoudiniEngineUnity.JSONContainerType
TYPE:  sealed struct
TOKEN: 0x2000167
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.JSONContainerTypeArray  // const
  public    static  HoudiniEngineUnity.JSONContainerTypeObject  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_TerrainUtility
TYPE:  static class
TOKEN: 0x2000168
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DB9884  token=0x6000C8C  System.Boolean GenerateTerrainFromVolume(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HAPI_VolumeInfo& volumeInfo, System.Int32 geoID, System.Int32 partID, UnityEngine.GameObject gameObject, UnityEngine.TerrainData& terrainData, UnityEngine.Vector3& volumePositionOffset, UnityEngine.Terrain& terrain, System.String bakedMaterialPath)
  RVA=0x09DBC14C  token=0x6000C8D  System.Void SetTerrainMaterial(UnityEngine.Terrain terrain, System.String specifiedMaterialName, System.String bakedMaterialPath)
  RVA=0x09DBA2B0  token=0x6000C8E  System.String GetDefaultTerrainShaderName()
  RVA=0x09DBA230  token=0x6000C8F  System.String GetDefaultTerrainMaterialPath()
  RVA=0x09DBA6E8  token=0x6000C90  System.Single[] GetNormalizedHeightmapFromPartWithMinMax(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Single& minHeight, System.Single& maxHeight, System.Single& heightRange, System.Boolean bUseHeightRangeOverride)
  RVA=0x09DBA330  token=0x6000C91  System.Int32[,] GetDetailMapFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32& detailResolution)
  RVA=0x09DBA570  token=0x6000C92  System.Boolean GetHeightmapFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 xLength, System.Int32 yLength, System.Int32 geoID, System.Int32 partID, System.Single[]& heightValues, System.Single& minHeight, System.Single& maxHeight)
  RVA=0x09DB97A8  token=0x6000C93  System.Single[,] ConvertHeightMapHoudiniToUnity(System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Single[] heightValues)
  RVA=0x09DB9664  token=0x6000C94  System.Single[,,] ConvertHeightFieldToAlphaMap(System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Collections.Generic.List<System.Single[]> heightFields)
  RVA=0x09DB8BA0  token=0x6000C95  System.Single[,,] AppendConvertedHeightFieldToAlphaMap(System.Int32 heightMapWidth, System.Int32 heightMapHeight, System.Single[,,] existingAlphaMaps, System.Collections.Generic.List<System.Single[]> heightFields, System.Single[] strengths, System.Collections.Generic.List<System.Int32> alphaMapIndices)
  RVA=0x09DBAB28  token=0x6000C96  UnityEngine.Vector3 GetVolumePositionOffset(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.Vector3 volumePosition, System.Single terrainSizeX, System.Single heightMapSize, System.Int32 mapWidth, System.Int32 mapHeight, System.Single minHeight)
  RVA=0x09DBAA34  token=0x6000C97  System.Collections.Generic.List<HoudiniEngineUnity.HEU_TreePrototypeInfo> GetTreePrototypeInfosFromPart(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09DBB308  token=0x6000C98  System.Void PopulateScatterTrees(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32 pointCount, HoudiniEngineUnity.HEU_VolumeScatterTrees& scatterTrees, System.Boolean throwWarningIfNoTileAttribute)
  RVA=0x09DB9178  token=0x6000C99  System.Void ApplyScatterTrees(UnityEngine.TerrainData terrainData, HoudiniEngineUnity.HEU_VolumeScatterTrees scatterTrees, System.Int32 tileIndex)
  RVA=0x09DBAEE4  token=0x6000C9A  System.Void PopulateDetailPrototype(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_DetailPrototype& detailPrototype)
  RVA=0x09DBAC14  token=0x6000C9B  System.Void PopulateDetailProperties(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_DetailProperties& detailProperties)
  RVA=0x09DB8D98  token=0x6000C9C  System.Void ApplyDetailLayers(UnityEngine.Terrain terrain, UnityEngine.TerrainData terrainData, HoudiniEngineUnity.HEU_DetailProperties detailProperties, System.Collections.Generic.List<HoudiniEngineUnity.HEU_DetailPrototype> heuDetailPrototypes, System.Collections.Generic.List<System.Int32[,]> convertedDetailMaps)
  RVA=0x09DBA830  token=0x6000C9D  System.Int32 GetTerrainLayerIndexByName(System.String layerName, UnityEngine.TerrainLayer[] terrainLayers)
  RVA=0x09DBA99C  token=0x6000C9E  System.Int32 GetTerrainLayerIndex(UnityEngine.TerrainLayer layer, UnityEngine.TerrainLayer[] terrainLayers)
  RVA=0x09DBC2B8  token=0x6000C9F  System.Boolean VolumeLayerHasAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09DBA434  token=0x6000CA0  HoudiniEngineUnity.HFLayerType GetHeightfieldLayerType(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String volumeName)
  RVA=0x09DBA3C0  token=0x6000CA1  System.Single GetHeightRangeFromHeightfield(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09DBA768  token=0x6000CA2  System.String GetTerrainDataExportPathFromHeightfieldAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID)
  RVA=0x09DBBEB4  token=0x6000CA3  System.Single[] ResampleData(System.Single[] data, System.Int32 oldWidth, System.Int32 oldHeight, System.Int32 newWidth, System.Int32 newHeight)
  RVA=0x09DBA1C0  token=0x6000CA4  System.Boolean GetAttributeTile(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32& outTileAttribute)
END_CLASS

CLASS: HoudiniEngineUnity.IEquivable`1
TYPE:  interface
TOKEN: 0x2000169
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000CA5  System.Boolean IsEquivalentTo(T other)
END_CLASS

CLASS: HoudiniEngineUnity.IEquivableWrapperClass`1
TYPE:  interface
TOKEN: 0x200016A
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000CA6  System.Boolean IsNull()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_TestHelpers
TYPE:  class
TOKEN: 0x200016B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000CA7  System.Boolean AssertTrueLogEquivalent(T a, T b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3, HoudiniEngineUnity.HEU_TestHelpers.RequireStruct<T> _)
  RVA=0x09DBC5AC  token=0x6000CA8  System.Boolean AssertTrueLogEquivalent(UnityEngine.GameObject a, UnityEngine.GameObject b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CA9  System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivable<T> a, HoudiniEngineUnity.IEquivable<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CAA  System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivableWrapperClass<T> a, HoudiniEngineUnity.IEquivableWrapperClass<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=0x09DBC6A8  token=0x6000CAB  System.Boolean AssertTrueLogEquivalent(System.String a, System.String b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CAC  System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<T> a, System.Collections.Generic.List<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3, HoudiniEngineUnity.HEU_TestHelpers.RequireStruct<T> _)
  RVA=-1  // generic def  token=0x6000CAD  System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<T> a, System.Collections.Generic.List<T> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3, HoudiniEngineUnity.HEU_TestHelpers.RequireClass<T> _)
  RVA=-1  // generic def  token=0x6000CAE  System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<HoudiniEngineUnity.IEquivable<T>> a, System.Collections.Generic.List<HoudiniEngineUnity.IEquivable<T>> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CAF  System.Boolean AssertTrueLogEquivalent(System.Collections.Generic.List<HoudiniEngineUnity.IEquivableWrapperClass<T>> a, System.Collections.Generic.List<HoudiniEngineUnity.IEquivableWrapperClass<T>> b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CB0  System.Boolean AssertTrueLogEquivalent(T[] a, T[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=0x09DBC448  token=0x6000CB1  System.Boolean AssertTrueLogEquivalent(System.String[] a, System.String[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CB2  System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivable<T>[] a, HoudiniEngineUnity.IEquivable<T>[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CB3  System.Boolean AssertTrueLogEquivalent(HoudiniEngineUnity.IEquivableWrapperClass<T>[] a, HoudiniEngineUnity.IEquivableWrapperClass<T>[] b, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=0x09DBC778  token=0x6000CB4  System.Void PrintTestLogAndSetResult(System.Boolean expression, System.Boolean& result, System.String header, System.String subject, System.String optional1, System.String optional2, System.String optional3)
  RVA=-1  // generic def  token=0x6000CB5  System.Boolean ShouldBeTested(T a, T b, System.Boolean& bResult, System.String header, System.String subject)
  RVA=0x09DBC8BC  token=0x6000CB6  System.Boolean ShouldBeTested(UnityEngine.GameObject a, UnityEngine.GameObject b, System.Boolean& bResult, System.String header, System.String subject)
  RVA=-1  // generic def  token=0x6000CB7  System.Boolean ShouldBeTested(HoudiniEngineUnity.IEquivable<T> a, HoudiniEngineUnity.IEquivable<T> b, System.Boolean& bResult, System.String header, System.String subject)
  RVA=-1  // generic def  token=0x6000CB8  System.Boolean ShouldBeTested(HoudiniEngineUnity.IEquivableWrapperClass<T> a, HoudiniEngineUnity.IEquivableWrapperClass<T> b, System.Boolean& bResult, System.String header, System.String subject)
  RVA=-1  // generic def  token=0x6000CB9  System.Boolean ShouldBeTested(System.Collections.Generic.List<T> a, System.Collections.Generic.List<T> b, System.Boolean& bResult, System.String header, System.String subject)
  RVA=-1  // generic def  token=0x6000CBA  System.Boolean ShouldBeTested(T[] a, T[] b, System.Boolean& bResult, System.String header, System.String subject)
  RVA=0x09DBCB04  token=0x6000CBB  System.Boolean ShouldBeTested(System.String a, System.String b, System.Boolean& bResult, System.String header, System.String subject)
  RVA=0x09DBCB98  token=0x6000CBC  System.Boolean TestOutputObjectEquivalence(UnityEngine.GameObject a, UnityEngine.GameObject b)
  RVA=0x041E1670  token=0x6000CBD  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_AssetInfo
TYPE:  class
TOKEN: 0x200016E
SIZE:  0x50
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_AssetInfoself  // 0x10
METHODS:
  RVA=0x04D997E0  token=0x6000CC0  System.Void .ctor(HoudiniEngineUnity.HAPI_AssetInfo self)
  RVA=0x09DC45F0  token=0x6000CC1  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_AssetInfo other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_AssetInfo_Extensions
TYPE:  static class
TOKEN: 0x200016F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC458C  token=0x6000CC2  HoudiniEngineUnity.Test_HAPI_AssetInfo ToTestObject(HoudiniEngineUnity.HAPI_AssetInfo self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_NodeInfo
TYPE:  class
TOKEN: 0x2000170
SIZE:  0x58
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_NodeInfoself  // 0x10
METHODS:
  RVA=0x04DA2DB0  token=0x6000CC3  System.Void .ctor(HoudiniEngineUnity.HAPI_NodeInfo self)
  RVA=0x09DC4C00  token=0x6000CC4  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_NodeInfo other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_NodeInfo_Extensions
TYPE:  static class
TOKEN: 0x2000171
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC4B90  token=0x6000CC5  HoudiniEngineUnity.Test_HAPI_NodeInfo ToTestObject(HoudiniEngineUnity.HAPI_NodeInfo self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_ObjectInfo
TYPE:  class
TOKEN: 0x2000172
SIZE:  0x30
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_ObjectInfoself  // 0x10
METHODS:
  RVA=0x04D99850  token=0x6000CC6  System.Void .ctor(HoudiniEngineUnity.HAPI_ObjectInfo self)
  RVA=0x09DC4E68  token=0x6000CC7  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_ObjectInfo other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_ObjectInfo_Extensions
TYPE:  static class
TOKEN: 0x2000173
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC4E08  token=0x6000CC8  HoudiniEngineUnity.Test_HAPI_ObjectInfo ToTestObject(HoudiniEngineUnity.HAPI_ObjectInfo self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_Transform
TYPE:  class
TOKEN: 0x2000174
SIZE:  0x38
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_Transformself  // 0x10
METHODS:
  RVA=0x09DC5750  token=0x6000CC9  System.Void .ctor(HoudiniEngineUnity.HAPI_Transform self)
  RVA=0x09DC57F4  token=0x6000CCA  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_Transform other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_Transform_Extensions
TYPE:  static class
TOKEN: 0x2000175
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC5774  token=0x6000CCB  HoudiniEngineUnity.Test_HAPI_Transform ToTestObject(HoudiniEngineUnity.HAPI_Transform self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_GeoInfo
TYPE:  class
TOKEN: 0x2000176
SIZE:  0x38
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_GeoInfo self  // 0x10
METHODS:
  RVA=0x04D997C0  token=0x6000CCC  System.Void .ctor(HoudiniEngineUnity.HAPI_GeoInfo self)
  RVA=0x09DC498C  token=0x6000CCD  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_GeoInfo other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_GeoInfo_Extensions
TYPE:  static class
TOKEN: 0x2000177
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC492C  token=0x6000CCE  HoudiniEngineUnity.Test_HAPI_GeoInfo ToTestObject(HoudiniEngineUnity.HAPI_GeoInfo self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_AttributeInfo
TYPE:  class
TOKEN: 0x2000178
SIZE:  0x38
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_AttributeInfoself  // 0x10
METHODS:
  RVA=0x04DA2D90  token=0x6000CCF  System.Void .ctor(HoudiniEngineUnity.HAPI_AttributeInfo self)
  RVA=0x09DC4828  token=0x6000CD0  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_AttributeInfo other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_AttributeInfo_Extensions
TYPE:  static class
TOKEN: 0x2000179
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC47CC  token=0x6000CD1  HoudiniEngineUnity.Test_HAPI_AttributeInfo ToTestObject(HoudiniEngineUnity.HAPI_AttributeInfo self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_TransformEuler
TYPE:  class
TOKEN: 0x200017A
SIZE:  0x38
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_TransformEulerself  // 0x10
METHODS:
  RVA=0x09DC5750  token=0x6000CD2  System.Void .ctor(HoudiniEngineUnity.HAPI_TransformEuler self)
  RVA=0x09DC5678  token=0x6000CD3  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_TransformEuler other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_TransformEuler_Extensions
TYPE:  static class
TOKEN: 0x200017B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC55F8  token=0x6000CD4  HoudiniEngineUnity.Test_HAPI_TransformEuler ToTestObject(HoudiniEngineUnity.HAPI_TransformEuler self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_ParmInfo
TYPE:  class
TOKEN: 0x200017C
SIZE:  0xA8
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            HoudiniEngineUnity.HAPI_ParmInfoself  // 0x10
METHODS:
  RVA=0x04DA2DE0  token=0x6000CD5  System.Void .ctor(HoudiniEngineUnity.HAPI_ParmInfo self)
  RVA=0x09DC5034  token=0x6000CD6  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_HAPI_ParmInfo other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_HAPI_ParmInfo_Extensions
TYPE:  static class
TOKEN: 0x200017D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC4F94  token=0x6000CD7  HoudiniEngineUnity.Test_HAPI_ParmInfo ToTestObject(HoudiniEngineUnity.HAPI_ParmInfo self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_LODGroup
TYPE:  class
TOKEN: 0x200017E
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.LODGroup            self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000CD8  System.Void .ctor(UnityEngine.LODGroup self)
  RVA=0x09DC5D18  token=0x6000CD9  System.Boolean IsNull()
  RVA=0x09DC5AF8  token=0x6000CDA  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_LODGroup other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_LODGroup_Extensions
TYPE:  static class
TOKEN: 0x200017F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC5888  token=0x6000CDB  HoudiniEngineUnity.Test_LODGroup ToTestObject(UnityEngine.LODGroup self)
  RVA=0x09DC59F4  token=0x6000CDC  HoudiniEngineUnity.Test_LODGroup[] ToTestObject(UnityEngine.LODGroup[] self)
  RVA=0x09DC58E8  token=0x6000CDD  System.Collections.Generic.List<HoudiniEngineUnity.Test_LODGroup> ToTestObject(System.Collections.Generic.List<UnityEngine.LODGroup> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Transform
TYPE:  class
TOKEN: 0x2000181
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Transform           self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000CE2  System.Void .ctor(UnityEngine.Transform self)
  RVA=0x09DC934C  token=0x6000CE3  System.Boolean IsNull()
  RVA=0x09DC91C8  token=0x6000CE4  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Transform other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Transform_Extensions
TYPE:  static class
TOKEN: 0x2000182
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC8F58  token=0x6000CE5  HoudiniEngineUnity.Test_Transform ToTestObject(UnityEngine.Transform self)
  RVA=0x09DC90C4  token=0x6000CE6  HoudiniEngineUnity.Test_Transform[] ToTestObject(UnityEngine.Transform[] self)
  RVA=0x09DC8FB8  token=0x6000CE7  System.Collections.Generic.List<HoudiniEngineUnity.Test_Transform> ToTestObject(System.Collections.Generic.List<UnityEngine.Transform> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Material
TYPE:  class
TOKEN: 0x2000184
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Material            self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000CEC  System.Void .ctor(UnityEngine.Material self)
  RVA=0x09DC62AC  token=0x6000CED  System.Boolean IsNull()
  RVA=0x09DC60D8  token=0x6000CEE  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Material other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Material_Extensions
TYPE:  static class
TOKEN: 0x2000185
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC5F4C  token=0x6000CEF  HoudiniEngineUnity.Test_Material ToTestObject(UnityEngine.Material self)
  RVA=0x09DC5FAC  token=0x6000CF0  HoudiniEngineUnity.Test_Material[] ToTestObject(UnityEngine.Material[] self)
  RVA=0x09DC5E40  token=0x6000CF1  System.Collections.Generic.List<HoudiniEngineUnity.Test_Material> ToTestObject(System.Collections.Generic.List<UnityEngine.Material> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Collider
TYPE:  class
TOKEN: 0x2000187
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Collider            self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000CF6  System.Void .ctor(UnityEngine.Collider self)
  RVA=0x09DC41D0  token=0x6000CF7  System.Boolean IsNull()
  RVA=0x09DC3D54  token=0x6000CF8  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Collider other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Collider_Extensions
TYPE:  static class
TOKEN: 0x2000188
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC3CF4  token=0x6000CF9  HoudiniEngineUnity.Test_Collider ToTestObject(UnityEngine.Collider self)
  RVA=0x09DC3BF0  token=0x6000CFA  HoudiniEngineUnity.Test_Collider[] ToTestObject(UnityEngine.Collider[] self)
  RVA=0x09DC3AE4  token=0x6000CFB  System.Collections.Generic.List<HoudiniEngineUnity.Test_Collider> ToTestObject(System.Collections.Generic.List<UnityEngine.Collider> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_BoxCollider
TYPE:  class
TOKEN: 0x200018A
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.BoxCollider         self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D00  System.Void .ctor(UnityEngine.BoxCollider self)
  RVA=0x09DC364C  token=0x6000D01  System.Boolean IsNull()
  RVA=0x09DC34C0  token=0x6000D02  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_BoxCollider other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_BoxCollider_Extensions
TYPE:  static class
TOKEN: 0x200018B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC3460  token=0x6000D03  HoudiniEngineUnity.Test_BoxCollider ToTestObject(UnityEngine.BoxCollider self)
  RVA=0x09DC3250  token=0x6000D04  HoudiniEngineUnity.Test_BoxCollider[] ToTestObject(UnityEngine.BoxCollider[] self)
  RVA=0x09DC3354  token=0x6000D05  System.Collections.Generic.List<HoudiniEngineUnity.Test_BoxCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.BoxCollider> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_SphereCollider
TYPE:  class
TOKEN: 0x200018D
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.SphereCollider      self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D0A  System.Void .ctor(UnityEngine.SphereCollider self)
  RVA=0x09DC78F0  token=0x6000D0B  System.Boolean IsNull()
  RVA=0x09DC7788  token=0x6000D0C  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_SphereCollider other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_SphereCollider_Extensions
TYPE:  static class
TOKEN: 0x200018E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC761C  token=0x6000D0D  HoudiniEngineUnity.Test_SphereCollider ToTestObject(UnityEngine.SphereCollider self)
  RVA=0x09DC7518  token=0x6000D0E  HoudiniEngineUnity.Test_SphereCollider[] ToTestObject(UnityEngine.SphereCollider[] self)
  RVA=0x09DC767C  token=0x6000D0F  System.Collections.Generic.List<HoudiniEngineUnity.Test_SphereCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.SphereCollider> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_CapsuleCollider
TYPE:  class
TOKEN: 0x2000190
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.CapsuleCollider     self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D14  System.Void .ctor(UnityEngine.CapsuleCollider self)
  RVA=0x09DC3A94  token=0x6000D15  System.Boolean IsNull()
  RVA=0x09DC390C  token=0x6000D16  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_CapsuleCollider other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_CapsuleCollider_Extensions
TYPE:  static class
TOKEN: 0x2000191
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC369C  token=0x6000D17  HoudiniEngineUnity.Test_CapsuleCollider ToTestObject(UnityEngine.CapsuleCollider self)
  RVA=0x09DC3808  token=0x6000D18  HoudiniEngineUnity.Test_CapsuleCollider[] ToTestObject(UnityEngine.CapsuleCollider[] self)
  RVA=0x09DC36FC  token=0x6000D19  System.Collections.Generic.List<HoudiniEngineUnity.Test_CapsuleCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.CapsuleCollider> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_MeshCollider
TYPE:  class
TOKEN: 0x2000193
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.MeshCollider        self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D1E  System.Void .ctor(UnityEngine.MeshCollider self)
  RVA=0x09DC6644  token=0x6000D1F  System.Boolean IsNull()
  RVA=0x09DC656C  token=0x6000D20  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_MeshCollider other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_MeshCollider_Extensions
TYPE:  static class
TOKEN: 0x2000194
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC62FC  token=0x6000D21  HoudiniEngineUnity.Test_MeshCollider ToTestObject(UnityEngine.MeshCollider self)
  RVA=0x09DC635C  token=0x6000D22  HoudiniEngineUnity.Test_MeshCollider[] ToTestObject(UnityEngine.MeshCollider[] self)
  RVA=0x09DC6460  token=0x6000D23  System.Collections.Generic.List<HoudiniEngineUnity.Test_MeshCollider> ToTestObject(System.Collections.Generic.List<UnityEngine.MeshCollider> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Mesh
TYPE:  class
TOKEN: 0x2000196
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Mesh                self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D28  System.Void .ctor(UnityEngine.Mesh self)
  RVA=0x09DC74C8  token=0x6000D29  System.Boolean IsNull()
  RVA=0x09DC710C  token=0x6000D2A  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Mesh other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Mesh_Extensions
TYPE:  static class
TOKEN: 0x2000197
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC6E9C  token=0x6000D2B  HoudiniEngineUnity.Test_Mesh ToTestObject(UnityEngine.Mesh self)
  RVA=0x09DC7008  token=0x6000D2C  HoudiniEngineUnity.Test_Mesh[] ToTestObject(UnityEngine.Mesh[] self)
  RVA=0x09DC6EFC  token=0x6000D2D  System.Collections.Generic.List<HoudiniEngineUnity.Test_Mesh> ToTestObject(System.Collections.Generic.List<UnityEngine.Mesh> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_MeshRenderer
TYPE:  class
TOKEN: 0x2000199
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.MeshRenderer        self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D32  System.Void .ctor(UnityEngine.MeshRenderer self)
  RVA=0x09DC6E4C  token=0x6000D33  System.Boolean IsNull()
  RVA=0x09DC6D28  token=0x6000D34  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_MeshRenderer other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_MeshRenderer_Extensions
TYPE:  static class
TOKEN: 0x200019A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC6AB8  token=0x6000D35  HoudiniEngineUnity.Test_MeshRenderer ToTestObject(UnityEngine.MeshRenderer self)
  RVA=0x09DC6C24  token=0x6000D36  HoudiniEngineUnity.Test_MeshRenderer[] ToTestObject(UnityEngine.MeshRenderer[] self)
  RVA=0x09DC6B18  token=0x6000D37  System.Collections.Generic.List<HoudiniEngineUnity.Test_MeshRenderer> ToTestObject(System.Collections.Generic.List<UnityEngine.MeshRenderer> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_MeshFilter
TYPE:  class
TOKEN: 0x200019C
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.MeshFilter          self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D3C  System.Void .ctor(UnityEngine.MeshFilter self)
  RVA=0x09DC6A68  token=0x6000D3D  System.Boolean IsNull()
  RVA=0x09DC6904  token=0x6000D3E  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_MeshFilter other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_MeshFilter_Extensions
TYPE:  static class
TOKEN: 0x200019D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC68A4  token=0x6000D3F  HoudiniEngineUnity.Test_MeshFilter ToTestObject(UnityEngine.MeshFilter self)
  RVA=0x09DC67A0  token=0x6000D40  HoudiniEngineUnity.Test_MeshFilter[] ToTestObject(UnityEngine.MeshFilter[] self)
  RVA=0x09DC6694  token=0x6000D41  System.Collections.Generic.List<HoudiniEngineUnity.Test_MeshFilter> ToTestObject(System.Collections.Generic.List<UnityEngine.MeshFilter> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_LayerMask
TYPE:  class
TOKEN: 0x200019F
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.LayerMask           self  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x6000D46  System.Void .ctor(UnityEngine.LayerMask self)
  RVA=0x09DC5DAC  token=0x6000D47  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_LayerMask other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_LayerMask_Extensions
TYPE:  static class
TOKEN: 0x20001A0
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC5D68  token=0x6000D48  HoudiniEngineUnity.Test_LayerMask ToTestObject(UnityEngine.LayerMask self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Gradient
TYPE:  class
TOKEN: 0x20001A1
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Gradient            self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D49  System.Void .ctor(UnityEngine.Gradient self)
  RVA=0x03BA32C0  token=0x6000D4A  System.Boolean IsNull()
  RVA=0x09DC4490  token=0x6000D4B  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Gradient other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Gradient_Extensions
TYPE:  static class
TOKEN: 0x20001A2
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC4430  token=0x6000D4C  HoudiniEngineUnity.Test_Gradient ToTestObject(UnityEngine.Gradient self)
  RVA=0x09DC432C  token=0x6000D4D  HoudiniEngineUnity.Test_Gradient[] ToTestObject(UnityEngine.Gradient[] self)
  RVA=0x09DC4220  token=0x6000D4E  System.Collections.Generic.List<HoudiniEngineUnity.Test_Gradient> ToTestObject(System.Collections.Generic.List<UnityEngine.Gradient> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_AnimationCurve
TYPE:  class
TOKEN: 0x20001A4
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.AnimationCurve      self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D53  System.Void .ctor(UnityEngine.AnimationCurve self)
  RVA=0x03BA32C0  token=0x6000D54  System.Boolean IsNull()
  RVA=0x09DC3164  token=0x6000D55  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_AnimationCurve other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_AnimationCurve_Extensions
TYPE:  static class
TOKEN: 0x20001A5
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC2EF4  token=0x6000D56  HoudiniEngineUnity.Test_AnimationCurve ToTestObject(UnityEngine.AnimationCurve self)
  RVA=0x09DC2F54  token=0x6000D57  HoudiniEngineUnity.Test_AnimationCurve[] ToTestObject(UnityEngine.AnimationCurve[] self)
  RVA=0x09DC3058  token=0x6000D58  System.Collections.Generic.List<HoudiniEngineUnity.Test_AnimationCurve> ToTestObject(System.Collections.Generic.List<UnityEngine.AnimationCurve> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_TerrainLayer
TYPE:  class
TOKEN: 0x20001A7
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.TerrainLayer        self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D5D  System.Void .ctor(UnityEngine.TerrainLayer self)
  RVA=0x09DC8710  token=0x6000D5E  System.Boolean IsNull()
  RVA=0x09DC8348  token=0x6000D5F  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_TerrainLayer other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_TerrainLayer_Extensions
TYPE:  static class
TOKEN: 0x20001A8
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC80D8  token=0x6000D60  HoudiniEngineUnity.Test_TerrainLayer ToTestObject(UnityEngine.TerrainLayer self)
  RVA=0x09DC8244  token=0x6000D61  HoudiniEngineUnity.Test_TerrainLayer[] ToTestObject(UnityEngine.TerrainLayer[] self)
  RVA=0x09DC8138  token=0x6000D62  System.Collections.Generic.List<HoudiniEngineUnity.Test_TerrainLayer> ToTestObject(System.Collections.Generic.List<UnityEngine.TerrainLayer> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Texture2D
TYPE:  class
TOKEN: 0x20001AA
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Texture2D           self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D67  System.Void .ctor(UnityEngine.Texture2D self)
  RVA=0x09DC8F08  token=0x6000D68  System.Boolean IsNull()
  RVA=0x09DC8DDC  token=0x6000D69  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Texture2D other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Texture2D_Extensions
TYPE:  static class
TOKEN: 0x20001AB
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC8B6C  token=0x6000D6A  HoudiniEngineUnity.Test_Texture2D ToTestObject(UnityEngine.Texture2D self)
  RVA=0x09DC8BCC  token=0x6000D6B  HoudiniEngineUnity.Test_Texture2D[] ToTestObject(UnityEngine.Texture2D[] self)
  RVA=0x09DC8CD0  token=0x6000D6C  System.Collections.Generic.List<HoudiniEngineUnity.Test_Texture2D> ToTestObject(System.Collections.Generic.List<UnityEngine.Texture2D> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_TreeInstance
TYPE:  class
TOKEN: 0x20001AD
SIZE:  0x38
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.TreeInstance        self  // 0x10
METHODS:
  RVA=0x04DA2D90  token=0x6000D71  System.Void .ctor(UnityEngine.TreeInstance self)
  RVA=0x09DC9608  token=0x6000D72  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_TreeInstance other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_TreeInstance_Extensions
TYPE:  static class
TOKEN: 0x20001AE
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC94A0  token=0x6000D73  HoudiniEngineUnity.Test_TreeInstance ToTestObject(UnityEngine.TreeInstance self)
  RVA=0x09DC939C  token=0x6000D74  HoudiniEngineUnity.Test_TreeInstance[] ToTestObject(UnityEngine.TreeInstance[] self)
  RVA=0x09DC94FC  token=0x6000D75  System.Collections.Generic.List<HoudiniEngineUnity.Test_TreeInstance> ToTestObject(System.Collections.Generic.List<UnityEngine.TreeInstance> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_TerrainData
TYPE:  class
TOKEN: 0x20001B0
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.TerrainData         self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D7A  System.Void .ctor(UnityEngine.TerrainData self)
  RVA=0x09DC8088  token=0x6000D7B  System.Boolean IsNull()
  RVA=0x09DC7BB0  token=0x6000D7C  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_TerrainData other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_TerrainData_Extensions
TYPE:  static class
TOKEN: 0x20001B1
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC7B50  token=0x6000D7D  HoudiniEngineUnity.Test_TerrainData ToTestObject(UnityEngine.TerrainData self)
  RVA=0x09DC7A4C  token=0x6000D7E  HoudiniEngineUnity.Test_TerrainData[] ToTestObject(UnityEngine.TerrainData[] self)
  RVA=0x09DC7940  token=0x6000D7F  System.Collections.Generic.List<HoudiniEngineUnity.Test_TerrainData> ToTestObject(System.Collections.Generic.List<UnityEngine.TerrainData> self)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Terrain
TYPE:  class
TOKEN: 0x20001B3
SIZE:  0x18
IMPLEMENTS: HoudiniEngineUnity.IEquivableWrapperClass`1 HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            UnityEngine.Terrain             self  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000D84  System.Void .ctor(UnityEngine.Terrain self)
  RVA=0x09DC8B1C  token=0x6000D85  System.Boolean IsNull()
  RVA=0x09DC89D0  token=0x6000D86  System.Boolean IsEquivalentTo(HoudiniEngineUnity.Test_Terrain other)
END_CLASS

CLASS: HoudiniEngineUnity.Test_Terrain_Extensions
TYPE:  static class
TOKEN: 0x20001B4
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DC8760  token=0x6000D87  HoudiniEngineUnity.Test_Terrain ToTestObject(UnityEngine.Terrain self)
  RVA=0x09DC87C0  token=0x6000D88  HoudiniEngineUnity.Test_Terrain[] ToTestObject(UnityEngine.Terrain[] self)
  RVA=0x09DC88C4  token=0x6000D89  System.Collections.Generic.List<HoudiniEngineUnity.Test_Terrain> ToTestObject(System.Collections.Generic.List<UnityEngine.Terrain> self)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ToolsInfo
TYPE:  class
TOKEN: 0x20001B6
SIZE:  0xA8
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.Single                   _paintBrushSize  // 0x18
  public            System.Single                   _paintBrushOpacity  // 0x1c
  public            System.Int32[]                  _paintIntValue  // 0x20
  public            System.Single[]                 _paintFloatValue  // 0x28
  public            System.String[]                 _paintStringValue  // 0x30
  public            System.Int32                    _lastAttributesGeoID  // 0x38
  public            System.Int32                    _lastAttributesPartID  // 0x3c
  public            System.String                   _lastAttributeNodeName  // 0x40
  public            System.String                   _lastAttributeName  // 0x48
  public            UnityEngine.Color               _brushHandleColor  // 0x50
  public            UnityEngine.Color               _affectedAreaPaintColor  // 0x60
  public            System.Boolean                  _liveUpdate  // 0x70
  public            System.Boolean                  _isPainting  // 0x71
  public            System.Single                   _editPointBoxSize  // 0x74
  public            UnityEngine.Color               _editPointBoxUnselectedColor  // 0x78
  public            UnityEngine.Color               _editPointBoxSelectedColor  // 0x88
  public            System.Boolean                  _recacheRequired  // 0x98
  public            HoudiniEngineUnity.HEU_ToolsInfo.PaintMergeMode_paintMergeMode  // 0x9c
  public            System.Boolean                  _showOnlyEditGeometry  // 0xa0
  public            System.Boolean                  _alwaysCookUpstream  // 0xa1
  public            HoudiniEngineUnity.HEU_ToolsInfo.PaintMeshVisibility_paintMeshVisiblity  // 0xa4
METHODS:
  RVA=0x09DBD1F0  token=0x6000D8E  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_ToolsInfo other)
  RVA=0x09DBD734  token=0x6000D8F  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HFLayerType
TYPE:  sealed struct
TOKEN: 0x20001B9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  HoudiniEngineUnity.HFLayerType  DEFAULT  // const
  public    static  HoudiniEngineUnity.HFLayerType  HEIGHT  // const
  public    static  HoudiniEngineUnity.HFLayerType  MASK  // const
  public    static  HoudiniEngineUnity.HFLayerType  DETAIL  // const
METHODS:
END_CLASS

CLASS: HoudiniEngineUnity.HEU_VolumeLayer
TYPE:  class
TOKEN: 0x20001BA
SIZE:  0x50
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.String                   _layerName  // 0x10
  public            System.Single                   _strength  // 0x18
  public            System.Boolean                  _uiExpanded  // 0x1c
  public            System.Int32                    _tile  // 0x20
  public            System.Int32                    _xLength  // 0x24
  public            System.Int32                    _yLength  // 0x28
  public            System.Boolean                  _hasLayerAttributes  // 0x2c
  public            UnityEngine.TerrainLayer        _terrainLayer  // 0x30
  public            HoudiniEngineUnity.HFLayerType  _layerType  // 0x38
  public            HoudiniEngineUnity.HEU_DetailPrototype_detailPrototype  // 0x40
  private           HoudiniEngineUnity.HEU_PartData _part  // 0x48
METHODS:
  RVA=0x09DBFFDC  token=0x6000D90  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_VolumeLayer other)
  RVA=0x04D8CF30  token=0x6000D91  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_VolumeScatterTrees
TYPE:  class
TOKEN: 0x20001BB
SIZE:  0x58
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.Collections.Generic.List<HoudiniEngineUnity.HEU_TreePrototypeInfo>_treePrototypInfos  // 0x10
  public            UnityEngine.Color32[]           _colors  // 0x18
  public            System.Single[]                 _heightScales  // 0x20
  public            UnityEngine.Color32[]           _lightmapColors  // 0x28
  public            UnityEngine.Vector3[]           _positions  // 0x30
  public            System.Int32[]                  _prototypeIndices  // 0x38
  public            System.Single[]                 _rotations  // 0x40
  public            System.Single[]                 _widthScales  // 0x48
  public            System.Int32[]                  _terrainTiles  // 0x50
METHODS:
  RVA=0x09DC02B8  token=0x6000D92  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_VolumeScatterTrees other)
  RVA=0x041E1670  token=0x6000D93  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_TreePrototypeInfo
TYPE:  class
TOKEN: 0x20001BC
SIZE:  0x20
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.String                   _prefabPath  // 0x10
  public            System.Single                   _bendfactor  // 0x18
METHODS:
  RVA=0x09DBD834  token=0x6000D94  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_TreePrototypeInfo other)
  RVA=0x041E1670  token=0x6000D95  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_DetailProperties
TYPE:  class
TOKEN: 0x20001BD
SIZE:  0x20
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.Single                   _detailDistance  // 0x10
  public            System.Single                   _detailDensity  // 0x14
  public            System.Int32                    _detailResolution  // 0x18
  public            System.Int32                    _detailResolutionPerPatch  // 0x1c
METHODS:
  RVA=0x09DB88BC  token=0x6000D96  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_DetailProperties other)
  RVA=0x04DA2D00  token=0x6000D97  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_DetailPrototype
TYPE:  class
TOKEN: 0x20001BE
SIZE:  0x60
IMPLEMENTS: HoudiniEngineUnity.IEquivable`1
FIELDS:
  public            System.String                   _prototypePrefab  // 0x10
  public            System.String                   _prototypeTexture  // 0x18
  public            System.Single                   _bendFactor  // 0x20
  public            UnityEngine.Color               _dryColor  // 0x24
  public            UnityEngine.Color               _healthyColor  // 0x34
  public            System.Single                   _maxHeight  // 0x44
  public            System.Single                   _maxWidth  // 0x48
  public            System.Single                   _minHeight  // 0x4c
  public            System.Single                   _minWidth  // 0x50
  public            System.Single                   _noiseSpread  // 0x54
  public            System.Int32                    _renderMode  // 0x58
METHODS:
  RVA=0x09DB8AE8  token=0x6000D98  System.Void .ctor()
  RVA=0x09DB8A1C  token=0x6000D99  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_DetailPrototype other)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_VolumeCache
TYPE:  class
TOKEN: 0x20001BF
SIZE:  0x70
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: HoudiniEngineUnity.IHEU_VolumeCache HoudiniEngineUnity.IHEU_HoudiniAssetSubcomponent HoudiniEngineUnity.IEquivable`1
FIELDS:
  private           HoudiniEngineUnity.HEU_GeoNode  _ownerNode  // 0x18
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayer>_layers  // 0x20
  private           System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeLayer>_updatedLayers  // 0x28
  private           System.Int32                    _tileIndex  // 0x30
  private           System.Boolean                  _isDirty  // 0x34
  private           System.String                   _geoName  // 0x38
  private           System.String                   _objName  // 0x40
  public            System.Boolean                  _uiExpanded  // 0x48
  private           UnityEngine.TerrainData         _terrainData  // 0x50
  private           HoudiniEngineUnity.HEU_VolumeScatterTrees_scatterTrees  // 0x58
  private           HoudiniEngineUnity.HEU_DetailProperties_detailProperties  // 0x60
  private           HoudiniEngineUnity.HEU_HoudiniAsset_parentAsset  // 0x68
PROPERTIES:
  Layers  get=0x04D862C0
  TileIndex  get=0x04D864F0
  ObjectName  get=0x04D85A60
  GeoName  get=0x04D85A50
  TerrainData  get=0x04D86270
  ScatterTrees  get=0x02E56440
  DetailProperties  get=0x04D86280
  ParentAsset  get=0x04D86230
  IsDirty  get=0x04D86A90  set=0x04D86AA0
  UIExpanded  get=0x04D867D0  set=0x04D86810
METHODS:
  RVA=0x09DBE4A8  token=0x6000DA6  HoudiniEngineUnity.HEU_SessionBase GetSession()
  RVA=0x09DBF594  token=0x6000DA7  System.Void Recook()
  RVA=0x09DBF5FC  token=0x6000DA8  System.Void ResetParameters()
  RVA=0x09DBE2FC  token=0x6000DA9  HoudiniEngineUnity.HEU_VolumeLayer GetLayer(System.String layerName)
  RVA=0x09DBF2D0  token=0x6000DAA  System.Void PopulatePreset(HoudiniEngineUnity.HEU_VolumeCachePreset cachePreset)
  RVA=0x09DBD948  token=0x6000DAB  System.Boolean ApplyPreset(HoudiniEngineUnity.HEU_VolumeCachePreset volumeCachePreset)
  RVA=0x09DBF86C  token=0x6000DAC  System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> UpdateVolumeCachesFromParts(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_GeoNode ownerNode, System.Collections.Generic.List<HoudiniEngineUnity.HEU_PartData> volumeParts, System.Collections.Generic.List<HoudiniEngineUnity.HEU_VolumeCache> volumeCaches)
  RVA=0x09DBE53C  token=0x6000DAD  System.Void Initialize(HoudiniEngineUnity.HEU_GeoNode ownerNode, System.Int32 tileIndex)
  RVA=0x09DBF768  token=0x6000DAE  System.Void StartUpdateLayers()
  RVA=0x09DBDF98  token=0x6000DAF  System.Void FinishUpdateLayers()
  RVA=0x09DBE43C  token=0x6000DB0  System.Void GetPartLayerAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_VolumeLayer layer)
  RVA=0x09DBF08C  token=0x6000DB1  System.Boolean LoadLayerTextureFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Texture2D& outTexture)
  RVA=0x09DBEB94  token=0x6000DB2  System.Boolean LoadLayerFloatFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, System.Single& floatValue)
  RVA=0x09DBEA28  token=0x6000DB3  System.Boolean LoadLayerColorFromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Color& colorValue)
  RVA=0x09DBF17C  token=0x6000DB4  System.Boolean LoadLayerVector2FromAttribute(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.String attrName, UnityEngine.Vector2& vectorValue)
  RVA=0x09DBF7E0  token=0x6000DB5  System.Void UpdateLayerFromPart(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_PartData part)
  RVA=0x09DBDFC8  token=0x6000DB6  System.Void GenerateTerrainWithAlphamaps(HoudiniEngineUnity.HEU_SessionBase session, HoudiniEngineUnity.HEU_HoudiniAsset houdiniAsset, System.Boolean bRebuild)
  RVA=0x09DBEC8C  token=0x6000DB7  System.Void LoadLayerPropertiesFromAttributes(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, UnityEngine.TerrainLayer terrainLayer, System.Boolean bNewTerrainLayer, UnityEngine.Texture2D defaultTexture)
  RVA=0x09DBF558  token=0x6000DB8  System.Void PopulateScatterTrees(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, System.Int32 pointCount, System.Boolean throwWarningIfNoTileAttribute)
  RVA=0x09DBF294  token=0x6000DB9  System.Void PopulateDetailPrototype(HoudiniEngineUnity.HEU_SessionBase session, System.Int32 geoID, System.Int32 partID, HoudiniEngineUnity.HEU_VolumeLayer layer)
  RVA=0x09DBDD54  token=0x6000DBA  System.Void CopyValuesTo(HoudiniEngineUnity.HEU_VolumeCache destCache)
  RVA=0x09DBDBD8  token=0x6000DBB  System.Void CopyDetailProperties(HoudiniEngineUnity.HEU_DetailProperties srcProp, HoudiniEngineUnity.HEU_DetailProperties destProp)
  RVA=0x09DBDC08  token=0x6000DBC  System.Void CopyLayer(HoudiniEngineUnity.HEU_VolumeLayer srcLayer, HoudiniEngineUnity.HEU_VolumeLayer destLayer)
  RVA=0x09DBDCE4  token=0x6000DBD  System.Void CopyPrototype(HoudiniEngineUnity.HEU_DetailPrototype srcProto, HoudiniEngineUnity.HEU_DetailPrototype destProto)
  RVA=0x09DBE9C0  token=0x6000DBE  UnityEngine.Texture2D LoadDefaultSplatTexture()
  RVA=0x09DBE8F4  token=0x6000DBF  UnityEngine.Texture2D LoadAssetTexture(System.String path)
  RVA=0x09DBE62C  token=0x6000DC0  System.Boolean IsEquivalentTo(HoudiniEngineUnity.HEU_VolumeCache other)
  RVA=0x09DBFF58  token=0x6000DC1  System.Void .ctor()
END_CLASS

