// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.XRModule.dll
// Classes:  32
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

CLASS: TrackingStateEventType
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.XR.InputTracking.TrackingStateEventTypeNodeAdded  // const
  public    static  UnityEngine.XR.InputTracking.TrackingStateEventTypeNodeRemoved  // const
  public    static  UnityEngine.XR.InputTracking.TrackingStateEventTypeTrackingAcquired  // const
  public    static  UnityEngine.XR.InputTracking.TrackingStateEventTypeTrackingLost  // const
METHODS:
END_CLASS

CLASS: XRRenderPass
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0xE8
FIELDS:
  private           System.IntPtr                   displaySubsystemInstance  // 0x10
  public            System.Int32                    renderPassIndex  // 0x18
  public            UnityEngine.Rendering.RenderTargetIdentifierrenderTarget  // 0x20
  public            UnityEngine.RenderTextureDescriptorrenderTargetDesc  // 0x48
  public            System.Boolean                  hasMotionVectorPass  // 0x7c
  public            UnityEngine.Rendering.RenderTargetIdentifiermotionVectorRenderTarget  // 0x80
  public            UnityEngine.RenderTextureDescriptormotionVectorRenderTargetDesc  // 0xa8
  public            System.Boolean                  shouldFillOutDepth  // 0xdc
  public            System.Int32                    cullingPassIndex  // 0xe0
METHODS:
END_CLASS

CLASS: XRMirrorViewBlitDesc
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x20
FIELDS:
  private           System.IntPtr                   displaySubsystemInstance  // 0x10
  public            System.Boolean                  nativeBlitAvailable  // 0x18
  public            System.Boolean                  nativeBlitInvalidStates  // 0x19
  public            System.Int32                    blitParamsCount  // 0x1c
METHODS:
END_CLASS

CLASS: MeshTransformList
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly System.IntPtr                   m_Self  // 0x10
METHODS:
  RVA=0x0B59129C  token=0x6000041  System.Void Dispose()
  RVA=0x0B5912A8  token=0x6000042  System.Void Dispose(System.IntPtr self)
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.IsReadOnlyAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.InputTracking
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  System.Action<UnityEngine.XR.XRNodeState>trackingAcquired  // static @ 0x0
  private   static  System.Action<UnityEngine.XR.XRNodeState>trackingLost  // static @ 0x8
  private   static  System.Action<UnityEngine.XR.XRNodeState>nodeAdded  // static @ 0x10
  private   static  System.Action<UnityEngine.XR.XRNodeState>nodeRemoved  // static @ 0x18
METHODS:
  RVA=0x0B590C24  token=0x6000003  System.Void InvokeTrackingEvent(UnityEngine.XR.InputTracking.TrackingStateEventType eventType, UnityEngine.XR.XRNode nodeType, System.Int64 uniqueID, System.Boolean tracked)
END_CLASS

CLASS: UnityEngine.XR.XRNode
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.XR.XRNode           LeftEye  // const
  public    static  UnityEngine.XR.XRNode           RightEye  // const
  public    static  UnityEngine.XR.XRNode           CenterEye  // const
  public    static  UnityEngine.XR.XRNode           Head  // const
  public    static  UnityEngine.XR.XRNode           LeftHand  // const
  public    static  UnityEngine.XR.XRNode           RightHand  // const
  public    static  UnityEngine.XR.XRNode           GameController  // const
  public    static  UnityEngine.XR.XRNode           TrackingReference  // const
  public    static  UnityEngine.XR.XRNode           HardwareTracker  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.AvailableTrackingData
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.XR.AvailableTrackingDataNone  // const
  public    static  UnityEngine.XR.AvailableTrackingDataPositionAvailable  // const
  public    static  UnityEngine.XR.AvailableTrackingDataRotationAvailable  // const
  public    static  UnityEngine.XR.AvailableTrackingDataVelocityAvailable  // const
  public    static  UnityEngine.XR.AvailableTrackingDataAngularVelocityAvailable  // const
  public    static  UnityEngine.XR.AvailableTrackingDataAccelerationAvailable  // const
  public    static  UnityEngine.XR.AvailableTrackingDataAngularAccelerationAvailable  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.XRNodeState
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x70
FIELDS:
  private           UnityEngine.XR.XRNode           m_Type  // 0x10
  private           UnityEngine.XR.AvailableTrackingDatam_AvailableFields  // 0x14
  private           UnityEngine.Vector3             m_Position  // 0x18
  private           UnityEngine.Quaternion          m_Rotation  // 0x24
  private           UnityEngine.Vector3             m_Velocity  // 0x34
  private           UnityEngine.Vector3             m_AngularVelocity  // 0x40
  private           UnityEngine.Vector3             m_Acceleration  // 0x4c
  private           UnityEngine.Vector3             m_AngularAcceleration  // 0x58
  private           System.Int32                    m_Tracked  // 0x64
  private           System.UInt64                   m_UniqueID  // 0x68
PROPERTIES:
  uniqueID  set=0x04D86A60
  nodeType  set=0x04D86140
  tracked  set=0x0B591538
METHODS:
END_CLASS

CLASS: UnityEngine.XR.InputFeatureType
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.UInt32                   value__  // 0x10
  public    static  UnityEngine.XR.InputFeatureType Custom  // const
  public    static  UnityEngine.XR.InputFeatureType Binary  // const
  public    static  UnityEngine.XR.InputFeatureType DiscreteStates  // const
  public    static  UnityEngine.XR.InputFeatureType Axis1D  // const
  public    static  UnityEngine.XR.InputFeatureType Axis2D  // const
  public    static  UnityEngine.XR.InputFeatureType Axis3D  // const
  public    static  UnityEngine.XR.InputFeatureType Rotation  // const
  public    static  UnityEngine.XR.InputFeatureType Hand  // const
  public    static  UnityEngine.XR.InputFeatureType Bone  // const
  public    static  UnityEngine.XR.InputFeatureType Eyes  // const
  public    static  UnityEngine.XR.InputFeatureType kUnityXRInputFeatureTypeInvalid  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.ConnectionChangeType
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.UInt32                   value__  // 0x10
  public    static  UnityEngine.XR.ConnectionChangeTypeConnected  // const
  public    static  UnityEngine.XR.ConnectionChangeTypeDisconnected  // const
  public    static  UnityEngine.XR.ConnectionChangeTypeConfigChange  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.InputDeviceCharacteristics
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.UInt32                   value__  // 0x10
  public    static  UnityEngine.XR.InputDeviceCharacteristicsNone  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsHeadMounted  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsCamera  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsHeldInHand  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsHandTracking  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsEyeTracking  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsTrackedDevice  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsController  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsTrackingReference  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsLeft  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsRight  // const
  public    static  UnityEngine.XR.InputDeviceCharacteristicsSimulated6DOF  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.InputTrackingState
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.UInt32                   value__  // 0x10
  public    static  UnityEngine.XR.InputTrackingStateNone  // const
  public    static  UnityEngine.XR.InputTrackingStatePosition  // const
  public    static  UnityEngine.XR.InputTrackingStateRotation  // const
  public    static  UnityEngine.XR.InputTrackingStateVelocity  // const
  public    static  UnityEngine.XR.InputTrackingStateAngularVelocity  // const
  public    static  UnityEngine.XR.InputTrackingStateAcceleration  // const
  public    static  UnityEngine.XR.InputTrackingStateAngularAcceleration  // const
  public    static  UnityEngine.XR.InputTrackingStateAll  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.InputFeatureUsage
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.String                   m_Name  // 0x10
  private           UnityEngine.XR.InputFeatureType m_InternalType  // 0x18
PROPERTIES:
  name  get=0x02FB0860
  internalType  get=0x04D9CF30
METHODS:
  RVA=0x0B590B3C  token=0x6000009  System.Boolean Equals(System.Object obj)
  RVA=0x0B590BB4  token=0x600000A  System.Boolean Equals(UnityEngine.XR.InputFeatureUsage other)
  RVA=0x0B590BF4  token=0x600000B  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.XR.InputDevice
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.UInt64                   m_DeviceId  // 0x10
  private           System.Boolean                  m_Initialized  // 0x18
PROPERTIES:
  deviceId  get=0x0B590A68
METHODS:
  RVA=0x04DBF420  token=0x600000C  System.Void .ctor(System.UInt64 deviceId)
  RVA=0x0B590990  token=0x600000E  System.Boolean Equals(System.Object obj)
  RVA=0x0B590A18  token=0x600000F  System.Boolean Equals(UnityEngine.XR.InputDevice other)
  RVA=0x0B590A4C  token=0x6000010  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.XR.Hand
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.UInt64                   m_DeviceId  // 0x10
  private           System.UInt32                   m_FeatureIndex  // 0x18
PROPERTIES:
  deviceId  get=0x02FB0860
  featureIndex  get=0x04D9CF30
METHODS:
  RVA=0x0B590850  token=0x6000013  System.Boolean Equals(System.Object obj)
  RVA=0x0B590748  token=0x6000014  System.Boolean Equals(UnityEngine.XR.Hand other)
  RVA=0x04DBF3B0  token=0x6000015  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.XR.Eyes
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.UInt64                   m_DeviceId  // 0x10
  private           System.UInt32                   m_FeatureIndex  // 0x18
PROPERTIES:
  deviceId  get=0x02FB0860
  featureIndex  get=0x04D9CF30
METHODS:
  RVA=0x0B5907D8  token=0x6000018  System.Boolean Equals(System.Object obj)
  RVA=0x0B590748  token=0x6000019  System.Boolean Equals(UnityEngine.XR.Eyes other)
  RVA=0x04DBF3B0  token=0x600001A  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.XR.Bone
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.UInt64                   m_DeviceId  // 0x10
  private           System.UInt32                   m_FeatureIndex  // 0x18
PROPERTIES:
  deviceId  get=0x02FB0860
  featureIndex  get=0x04D9CF30
METHODS:
  RVA=0x0B590760  token=0x600001D  System.Boolean Equals(System.Object obj)
  RVA=0x0B590748  token=0x600001E  System.Boolean Equals(UnityEngine.XR.Bone other)
  RVA=0x04DBF3B0  token=0x600001F  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.XR.InputDevices
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
  private   static  System.Action<UnityEngine.XR.InputDevice>deviceConnected  // static @ 0x0
  private   static  System.Action<UnityEngine.XR.InputDevice>deviceDisconnected  // static @ 0x8
  private   static  System.Action<UnityEngine.XR.InputDevice>deviceConfigChanged  // static @ 0x10
METHODS:
  RVA=0x0B590A7C  token=0x6000020  System.Void InvokeConnectionEvent(System.UInt64 deviceId, UnityEngine.XR.ConnectionChangeType change)
END_CLASS

CLASS: UnityEngine.XR.XRDisplaySubsystem
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x28
EXTENDS: UnityEngine.IntegratedSubsystem`1
FIELDS:
  private           System.Action<System.Boolean>   displayFocusChanged  // 0x20
METHODS:
  RVA=0x0B591304  token=0x6000021  System.Void InvokeDisplayFocusChanged(System.Boolean focus)
  RVA=0x0B591320  token=0x6000022  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.XRDisplaySubsystemDescriptor
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x18
EXTENDS: UnityEngine.IntegratedSubsystemDescriptor`1
FIELDS:
METHODS:
  RVA=0x0B5912DC  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.XRInputSubsystem
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x30
EXTENDS: UnityEngine.IntegratedSubsystem`1
FIELDS:
  private           System.Action<UnityEngine.XR.XRInputSubsystem>trackingOriginUpdated  // 0x20
  private           System.Action<UnityEngine.XR.XRInputSubsystem>boundaryChanged  // 0x28
METHODS:
  RVA=0x0B5913E8  token=0x6000024  System.Void InvokeTrackingOriginUpdatedEvent(System.IntPtr internalPtr)
  RVA=0x0B591370  token=0x6000025  System.Void InvokeBoundaryChangedEvent(System.IntPtr internalPtr)
  RVA=0x0B591460  token=0x6000026  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.XRInputSubsystemDescriptor
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x18
EXTENDS: UnityEngine.IntegratedSubsystemDescriptor`1
FIELDS:
METHODS:
  RVA=0x0B591348  token=0x6000027  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.MeshId
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   static  UnityEngine.XR.MeshId           s_InvalidId  // static @ 0x0
  private           System.UInt64                   m_SubId1  // 0x10
  private           System.UInt64                   m_SubId2  // 0x18
METHODS:
  RVA=0x0B5911E4  token=0x6000028  System.String ToString()
  RVA=0x04DBAE50  token=0x6000029  System.Int32 GetHashCode()
  RVA=0x0B591160  token=0x600002A  System.Boolean Equals(System.Object obj)
  RVA=0x067C4D3C  token=0x600002B  System.Boolean Equals(UnityEngine.XR.MeshId other)
  RVA=0x0B591260  token=0x600002C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.XR.MeshGenerationStatus
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.XR.MeshGenerationStatusSuccess  // const
  public    static  UnityEngine.XR.MeshGenerationStatusInvalidMeshId  // const
  public    static  UnityEngine.XR.MeshGenerationStatusGenerationAlreadyInProgress  // const
  public    static  UnityEngine.XR.MeshGenerationStatusCanceled  // const
  public    static  UnityEngine.XR.MeshGenerationStatusUnknownError  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.HashCodeHelper
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04DBF3D0  token=0x600002D  System.Int32 Combine(System.Int32 hash1, System.Int32 hash2)
  RVA=0x04DBF3E0  token=0x600002E  System.Int32 Combine(System.Int32 hash1, System.Int32 hash2, System.Int32 hash3)
  RVA=0x04DBF400  token=0x600002F  System.Int32 Combine(System.Int32 hash1, System.Int32 hash2, System.Int32 hash3, System.Int32 hash4)
  RVA=0x0B59092C  token=0x6000030  System.Int32 Combine(System.Int32 hash1, System.Int32 hash2, System.Int32 hash3, System.Int32 hash4, System.Int32 hash5)
  RVA=0x0B590900  token=0x6000031  System.Int32 Combine(System.Int32 hash1, System.Int32 hash2, System.Int32 hash3, System.Int32 hash4, System.Int32 hash5, System.Int32 hash6)
  RVA=0x0B5908C8  token=0x6000032  System.Int32 Combine(System.Int32 hash1, System.Int32 hash2, System.Int32 hash3, System.Int32 hash4, System.Int32 hash5, System.Int32 hash6, System.Int32 hash7)
  RVA=0x0B590950  token=0x6000033  System.Int32 Combine(System.Int32 hash1, System.Int32 hash2, System.Int32 hash3, System.Int32 hash4, System.Int32 hash5, System.Int32 hash6, System.Int32 hash7, System.Int32 hash8)
END_CLASS

CLASS: UnityEngine.XR.MeshGenerationResult
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x68
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   readonly UnityEngine.XR.MeshId           <MeshId>k__BackingField  // 0x10
  private   readonly UnityEngine.Mesh                <Mesh>k__BackingField  // 0x20
  private   readonly UnityEngine.MeshCollider        <MeshCollider>k__BackingField  // 0x28
  private   readonly UnityEngine.XR.MeshGenerationStatus<Status>k__BackingField  // 0x30
  private   readonly UnityEngine.XR.MeshVertexAttributes<Attributes>k__BackingField  // 0x34
  private   readonly System.UInt64                   <Timestamp>k__BackingField  // 0x38
  private   readonly UnityEngine.Vector3             <Position>k__BackingField  // 0x40
  private   readonly UnityEngine.Quaternion          <Rotation>k__BackingField  // 0x4c
  private   readonly UnityEngine.Vector3             <Scale>k__BackingField  // 0x5c
PROPERTIES:
  MeshId  get=0x02B77630
  Mesh  get=0x02B2ECC0
  MeshCollider  get=0x0385B100
  Status  get=0x02B70F90
  Attributes  get=0x011F0020
  Position  get=0x04D88ED0
  Rotation  get=0x04D8D280
  Scale  get=0x04D914D0
METHODS:
  RVA=0x0B590DE4  token=0x600003C  System.Boolean Equals(System.Object obj)
  RVA=0x0B590E94  token=0x600003D  System.Boolean Equals(UnityEngine.XR.MeshGenerationResult other)
  RVA=0x0B590FFC  token=0x600003E  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.XR.MeshVertexAttributes
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.XR.MeshVertexAttributesNone  // const
  public    static  UnityEngine.XR.MeshVertexAttributesNormals  // const
  public    static  UnityEngine.XR.MeshVertexAttributesTangents  // const
  public    static  UnityEngine.XR.MeshVertexAttributesUVs  // const
  public    static  UnityEngine.XR.MeshVertexAttributesColors  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.XRMeshSubsystem
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x20
EXTENDS: UnityEngine.IntegratedSubsystem`1
FIELDS:
METHODS:
  RVA=0x0B5914B0  token=0x600003F  System.Void InvokeMeshReadyDelegate(UnityEngine.XR.MeshGenerationResult result, System.Action<UnityEngine.XR.MeshGenerationResult> onMeshGenerationComplete)
  RVA=0x0B591510  token=0x6000040  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.XR.XRMeshSubsystemDescriptor
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x18
EXTENDS: UnityEngine.IntegratedSubsystemDescriptor`1
FIELDS:
METHODS:
  RVA=0x0B591488  token=0x6000043  System.Void .ctor()
END_CLASS

