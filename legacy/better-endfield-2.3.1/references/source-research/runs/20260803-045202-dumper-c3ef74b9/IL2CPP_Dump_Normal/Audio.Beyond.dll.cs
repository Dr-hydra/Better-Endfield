// ========================================================
// Dumped by @desirepro
// Assembly: Audio.Beyond.dll
// Classes:  83
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000009  // size: 0x10
    public static class Context
    {
        // Fields
        public static System.UInt64 s_listenerObjId;  // static @ 0x0
        public static System.UInt64 s_2dEmitterObjectId;  // static @ 0x8
        public static System.UInt32 s_playingIdInternal;  // static @ 0x10
        public static System.Collections.Generic.Dictionary<System.UInt32,System.UInt32> s_playingIdLut;  // static @ 0x18
        public static Beyond.PoolCore.ObjectPool<Beyond.Audio.AudioAdapter.PayloadPostEvent> s_payloadPool;  // static @ 0x20
        private static readonly System.Action<Beyond.Audio.AudioAdapter.PayloadPostEvent> s_recyclePayloadPostEvent;  // static @ 0x28
        private static readonly System.Func<Beyond.Audio.AudioAdapter.PayloadPostEvent> s_createPayloadPostEvent;  // static @ 0x30
        public static readonly AkExternalSourceInfoArray S_EXTERNAL_SOURCE_INFO_ARRAY;  // static @ 0x38
        public static AkRoomParams s_reusableAkRoomParams;  // static @ 0x40

        // Methods
        // RVA: 0x04B09520  token: 0x600007E
        public static System.Void PostSoundEngineInit() { }
        // RVA: 0x03FE1AB0  token: 0x600007F
        public static System.Void Reset() { }
        // RVA: 0x011C5C00  token: 0x6000080
        private static Beyond.Audio.AudioAdapter.PayloadPostEvent _CreatePayloadPostEvent() { }
        // RVA: 0x04770C80  token: 0x6000081
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x30
    public class PayloadPostEvent
    {
        // Fields
        public System.UInt64 audioObjectId;  // 0x10
        public System.UInt32 callbackType;  // 0x18
        public System.UInt32 internalPlayingId;  // 0x1c
        public Beyond.Audio.AudioEventCallback callback;  // 0x20
        public System.Object cookie;  // 0x28

        // Methods
        // RVA: 0x0328A610  token: 0x6000082
        public static System.Void OnRecycle(Beyond.Audio.AudioAdapter.PayloadPostEvent obj) { }
        // RVA: 0x041E1670  token: 0x6000083
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public static class Device
    {
        // Methods
        // RVA: 0x03AC7440  token: 0x6000084
        public static System.Boolean GetSpeakerConfiguration(Beyond.Audio.AudioChannelMask& channelMask, System.UInt32& numChannels, Beyond.Audio.AudioChannelConfigType& configType) { }
        // RVA: 0x0635F9F8  token: 0x6000085
        public static System.Boolean AddOutputDevice(System.String deviceName, System.UInt32 deviceId, System.UInt32 numChannels, System.UInt32 speakerSetup, System.UInt64& outDeviceId) { }
        // RVA: 0x0635FB54  token: 0x6000086
        public static System.Boolean AddOutput(System.String shareSetName, System.UInt32 deviceId, System.UInt64& outId) { }
        // RVA: 0x0635FCAC  token: 0x6000087
        public static System.UInt64 GetOutputDeviceId(System.String shareSetName, System.UInt32 deviceId) { }
        // RVA: 0x0635FD38  token: 0x6000088
        public static System.Boolean RemoveOutputDevice(System.UInt64 output) { }
        // RVA: 0x04008EC0  token: 0x6000089
        public static System.Boolean Switch3DOutput(System.Boolean use3DDevice) { }
        // RVA: 0x0635FDD8  token: 0x600008A
        public static System.Void SetOutputDeviceEffect(System.UInt64 outputDeviceId, System.UInt32 effectIdx, System.UInt32 shareSetId) { }
        // RVA: 0x0635FC34  token: 0x600008B
        public static System.UInt32 GetMmDeviceIdFromScePadHandle(System.Int32 scePadHandle) { }

    }

    // TypeToken: 0x200000C  // size: 0x28
    public sealed class <LoadAndPinEventsCoroutine>d__26 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Collections.Generic.ICollection<System.String> events;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600008C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600008D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x063659F8  token: 0x600008E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06365C88  token: 0x6000090
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200000D  // size: 0x20
    public sealed class <>c__DisplayClass100_0
    {
        // Fields
        public System.Int32 doneCount;  // 0x10
        public AkCallbackManager.BankCallback <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000092
        public System.Void .ctor() { }
        // RVA: 0x04D86170  token: 0x6000093
        private System.Void <_DoLoadAndPinEventsSync>b__0(System.UInt32 _, System.IntPtr _, AKRESULT _, System.Object _) { }

    }

    // TypeToken: 0x200000E  // size: 0x20
    public sealed class <>c__DisplayClass101_0
    {
        // Fields
        public System.Int32 doneCount;  // 0x10
        public AkCallbackManager.BankCallback <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000094
        public System.Void .ctor() { }
        // RVA: 0x04D86170  token: 0x6000095
        private System.Void <_DoLoadAndPinEventsAsync>b__0(System.UInt32 _, System.IntPtr _, AKRESULT _, System.Object _) { }

    }

    // TypeToken: 0x200000F  // size: 0x48
    public sealed class <_DoLoadAndPinEventsAsync>d__101 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Collections.Generic.IList<System.UInt32> events;  // 0x20
        public System.Action<System.Object> onDone;  // 0x28
        public System.Object payload;  // 0x30
        private Beyond.Audio.AudioAdapter.<>c__DisplayClass101_0 <>8__1;  // 0x38
        private System.Int32 <allCount>5__2;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000096
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000097
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x037A6DC0  token: 0x6000098
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06365CD4  token: 0x600009A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000014  // size: 0x11
    public sealed struct WarnType
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Audio.AudioEventWarnAttribute.WarnType NONE;  // const
        public static Beyond.Audio.AudioEventWarnAttribute.WarnType ONE_SHOT;  // const
        public static Beyond.Audio.AudioEventWarnAttribute.WarnType LOOP;  // const

    }

    // TypeToken: 0x2000018  // size: 0x28
    public class CallbackCookie
    {
        // Fields
        public System.String bankName;  // 0x10
        public System.Action callback;  // 0x18
        public Beyond.Audio.BankHandle handle;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60000BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x12
    public sealed struct EBoolFlags
    {
        // Fields
        public System.UInt16 value__;  // 0x10
        public static Beyond.Audio.AudioRoomData.EBoolFlags None;  // const
        public static Beyond.Audio.AudioRoomData.EBoolFlags KeepRegistered;  // const
        public static Beyond.Audio.AudioRoomData.EBoolFlags UseForReflectionAndDiffraction;  // const
        public static Beyond.Audio.AudioRoomData.EBoolFlags AsReverbZone;  // const

    }

    // TypeToken: 0x2000034  // size: 0x20
    public sealed struct FExecuteAction
    {
        // Fields
        public System.Int32 queuedFrame;  // 0x10
        public System.UInt32 playingId;  // 0x14
        public AkActionOnEventType actionType;  // 0x18
        public System.Int32 durationMs;  // 0x1c

    }

    // TypeToken: 0x2000035  // size: 0x24
    public sealed struct FRTPCAction
    {
        // Fields
        public System.Int32 queuedFrame;  // 0x10
        public System.UInt32 playingId;  // 0x14
        public System.UInt32 rtpcId;  // 0x18
        public System.Single targetValue;  // 0x1c
        public System.Int32 durationMs;  // 0x20

    }

    // TypeToken: 0x2000036  // size: 0x30
    public sealed struct FSeekAction
    {
        // Fields
        public System.Int32 queuedFrame;  // 0x10
        public System.Single queuedTime;  // 0x14
        public System.UInt32 playingId;  // 0x18
        public System.UInt32 eventId;  // 0x1c
        public System.UInt64 audioObjectId;  // 0x20
        public System.Int32 timeMs;  // 0x28
        public System.Boolean toNearestMarker;  // 0x2c

    }

    // TypeToken: 0x2000038  // size: 0x18
    public class EventCacheInfo
    {
        // Fields
        public System.UInt32 id;  // 0x10
        public System.UInt32 refCount;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600010C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000039  // size: 0x20
    public class Cache
    {
        // Fields
        private readonly System.Collections.Generic.LinkedList<Beyond.Audio.AudioAssetCache.EventCacheInfo> m_lruCachedEventsList;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>> m_lruCachedEvents;  // 0x18

        // Properties
        System.Collections.Generic.LinkedList<Beyond.Audio.AudioAssetCache.EventCacheInfo> lruCachedEventsList { get; /* RVA: 0x0635F9A8 */ }
        System.Int32 count { get; /* RVA: 0x03EB13E0 */ }

        // Methods
        // RVA: 0x03FE2F00  token: 0x600010F
        public System.Void Clear() { }
        // RVA: 0x03EB1130  token: 0x6000110
        public System.UInt32 RemoveFirst(System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>& outNode) { }
        // RVA: 0x0328A260  token: 0x6000111
        public System.Boolean Remove(System.UInt32 inId, System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>& outNode) { }
        // RVA: 0x0328A1E0  token: 0x6000112
        public System.Void AddLast(System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>& node) { }
        // RVA: 0x049DB740  token: 0x6000113
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Audio.AudioAssetCache.<>c <>9;  // static @ 0x0
        public static System.Func<System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>> <>9__7_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D3EA70  token: 0x6000114
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000115
        public System.Void .ctor() { }
        // RVA: 0x0353D9E0  token: 0x6000116
        private System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo> <Reset>b__7_0() { }

    }

    // TypeToken: 0x200003C  // size: 0x28
    public sealed struct FOnBankLoadedDoPrepareEventPayload
    {
        // Fields
        public System.UInt32 id;  // 0x10
        public AkCallbackManager.BankCallback callback;  // 0x18
        public System.Object cookie;  // 0x20

    }

    // TypeToken: 0x200003D  // size: 0x30
    public sealed struct FPendingLoadRequest
    {
        // Fields
        public System.UInt32 id;  // 0x10
        public Beyond.Audio.AudioAssetHelper.FOnBankLoadedDoPrepareEventPayload payload;  // 0x18

    }

    // TypeToken: 0x2000042  // size: 0x20
    public sealed struct FGlobalMemoryStats
    {
        // Fields
        public System.UInt64 used;  // 0x10
        public System.UInt64 reserved;  // 0x18

    }

    // TypeToken: 0x2000043  // size: 0x30
    public sealed struct FExtraMemoryStats
    {
        // Fields
        public System.UInt64 active;  // 0x10
        public System.UInt64 mapped;  // 0x18
        public System.UInt64 retained;  // 0x20
        public System.UInt64 allocated;  // 0x28

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct UIAudioType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Audio.AudioUIUtil.UIAudioType None;  // const
        public static Beyond.Audio.AudioUIUtil.UIAudioType FullScreen;  // const
        public static Beyond.Audio.AudioUIUtil.UIAudioType Tips;  // const

    }

    // TypeToken: 0x2000047  // size: 0x20
    public sealed struct VfsLoadedInfo
    {
        // Fields
        public System.Collections.Generic.List<System.UInt32> loadedPcks;  // 0x10
        public System.Int32 loadedVersion;  // 0x18
        public System.Boolean isLoaded;  // 0x1c

        // Methods
        // RVA: 0x04653D10  token: 0x600015D
        public static Beyond.Audio.AudioVFSLoader.VfsLoadedInfo Create() { }

    }

    // TypeToken: 0x200004B  // size: 0x10
    public static class AudioWwiseProfilingData
    {
        // Fields
        public static readonly Unity.Profiling.ProfilerCounterValue<System.Single> CPU;  // static @ 0x0
        public static readonly Unity.Profiling.ProfilerCounterValue<System.Single> PLUGIN_CPU;  // static @ 0x1
        public static readonly Unity.Profiling.ProfilerCounterValue<System.Int32> ACTIVE_EVENTS;  // static @ 0x2
        public static readonly Unity.Profiling.ProfilerCounterValue<System.Int32> TOTAL_VOICES;  // static @ 0x3
        public static readonly Unity.Profiling.ProfilerCounterValue<System.Int32> PHYS_VOICES;  // static @ 0x4
        public static readonly Unity.Profiling.ProfilerCounterValue<System.Int32> VIR_VOPICES;  // static @ 0x5
        public static readonly Unity.Profiling.ProfilerCounterValue<System.UInt64> TOTAL_MEM;  // static @ 0x6
        public static readonly Unity.Profiling.ProfilerCounterValue<System.UInt64> RESERVED_MEM;  // static @ 0x7
        public static readonly Unity.Profiling.ProfilerCounterValue<System.Single> CACHE_HIT_RATE;  // static @ 0x8

        // Methods
        // RVA: 0x0635F6E8  token: 0x6000168
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x1C
    public sealed struct __StaticArrayInitTypeSize=12
    {
    }

    // TypeToken: 0x200004E  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 662FC6EB33F1099AA2416EFBD9D5716B0AC7D68036E4C225784C78C03695D2AC;  // static @ 0x0

    }

namespace Beyond.Audio
{

    // TypeToken: 0x2000008  // size: 0x10
    public static class AudioAdapter
    {
        // Fields
        private static readonly AkCallbackManager.BankCallback ON_EVENT_PREPARED_DO_POST_EVENT;  // static @ 0x0
        private static readonly AkCallbackManager.EventCallback ON_EVENT_CALLBACK;  // static @ 0x8
        private static readonly AkCallbackManager.EventCallback ON_EXTERNAL_SOURCE_EVENT_CALLBACK;  // static @ 0x10

        // Properties
        System.UInt64 listenerObjId { get; /* RVA: 0x04D4E610 */ }

        // Methods
        // RVA: 0x046D3F80  token: 0x6000008
        public static System.UInt32 PostEvent(System.String eventName) { }
        // RVA: 0x0635A6D8  token: 0x6000009
        public static System.UInt32 PostEvent(System.UInt32 eventId) { }
        // RVA: 0x0635A8AC  token: 0x600000A
        public static System.UInt32 PostEvent(System.String eventName, System.UInt64 audioObjectId) { }
        // RVA: 0x0635A924  token: 0x600000B
        public static System.UInt32 PostEvent(System.UInt32 eventId, System.UInt64 audioObjectId) { }
        // RVA: 0x0635A984  token: 0x600000C
        public static System.UInt32 PostEvent(System.String eventName, Beyond.Audio.AudioCallbackType callbackType, Beyond.Audio.AudioEventCallback callback, System.Object cookie) { }
        // RVA: 0x0635A7F8  token: 0x600000D
        public static System.UInt32 PostEvent(System.UInt32 eventId, Beyond.Audio.AudioCallbackType& callbackType, Beyond.Audio.AudioEventCallback& callback, System.Object cookie) { }
        // RVA: 0x0635A760  token: 0x600000E
        public static System.UInt32 PostEvent(System.String eventName, System.UInt64 audioObjectId, Beyond.Audio.AudioCallbackType callbackType, Beyond.Audio.AudioEventCallback callback, System.Object cookie) { }
        // RVA: 0x0635A65C  token: 0x600000F
        public static System.UInt32 PostEvent(System.UInt32 eventId, System.UInt64 audioObjectId, Beyond.Audio.AudioCallbackType callbackType, Beyond.Audio.AudioEventCallback callback, System.Object cookie) { }
        // RVA: 0x03ABF0A0  token: 0x6000010
        public static System.UInt32 PostEventExternal(System.String eventName, System.UInt64 audioObjectId, System.String externalSourceKey, System.UInt32 externalCookie, Beyond.Audio.AudioCallbackType callbackType, Beyond.Audio.AudioEventCallback callback, System.Object cookie, Beyond.Audio.AudioCodec codec) { }
        // RVA: 0x0635A51C  token: 0x6000011
        public static System.UInt32 PostEventExternal(System.UInt32 eventId, System.UInt64 audioObjectId, System.String externalSourceKey, System.UInt32 externalCookie, Beyond.Audio.AudioCallbackType callbackType, Beyond.Audio.AudioEventCallback callback, System.Object cookie, Beyond.Audio.AudioCodec codec) { }
        // RVA: 0x0635BBD0  token: 0x6000012
        public static System.Void StopByPlayingId(System.UInt32 playingId, System.Int32 fadeTimeMs) { }
        // RVA: 0x0635A480  token: 0x6000013
        public static System.Void PauseByPlayingId(System.UInt32 playingId, System.Int32 fadeTimeMs) { }
        // RVA: 0x0635AF50  token: 0x6000014
        public static System.Void ResumeByPlayingId(System.UInt32 playingId, System.Int32 fadeTimeMs) { }
        // RVA: 0x0635A4D4  token: 0x6000015
        public static System.Void PinEvent(System.UInt32 eventId) { }
        // RVA: 0x0635C160  token: 0x6000016
        public static System.Void UnpinEvent(System.UInt32 eventId) { }
        // RVA: 0x0635C1AC  token: 0x6000017
        public static System.Void UnpinEvent(System.String eventName) { }
        // RVA: 0x0635A2DC  token: 0x6000018
        public static System.Void LoadAndPinEventsSync(System.Collections.Generic.IList<System.UInt32> events) { }
        // RVA: 0x0635A0A8  token: 0x6000019
        public static System.Void LoadAndPinEventsSync(System.Collections.Generic.ICollection<System.String> events) { }
        // RVA: 0x03EB4B70  token: 0x600001A
        public static System.Void LoadAndPinEventsAsync(System.Collections.Generic.IList<System.UInt32> events, System.Action<System.Object> onDone, System.Object payload) { }
        // RVA: 0x03EB4BF0  token: 0x600001B
        public static System.Void LoadAndPinEventsAsync(System.Collections.Generic.IList<Beyond.Audio.AudioId> events, System.Action<System.Object> onDone, System.Object payload) { }
        // RVA: 0x06359D38  token: 0x600001C
        public static System.Void LoadAndPinEventsAsync(System.Collections.Generic.ICollection<System.String> events, System.Action<System.Object> onDone, System.Object payload) { }
        // RVA: 0x0635A02C  token: 0x600001D
        public static System.Collections.IEnumerator LoadAndPinEventsCoroutine(System.Collections.Generic.IList<System.UInt32> events) { }
        // RVA: 0x06359FA0  token: 0x600001E
        public static System.Collections.IEnumerator LoadAndPinEventsCoroutine(System.Collections.Generic.ICollection<System.String> events) { }
        // RVA: 0x0635B950  token: 0x600001F
        public static System.Void SetState(System.String stateGroup, System.String stateName) { }
        // RVA: 0x0635B8F8  token: 0x6000020
        public static System.Void SetState(System.UInt32 stateGroup, System.UInt32 stateName) { }
        // RVA: 0x0635C108  token: 0x6000021
        public static System.Boolean TryGetState(System.UInt32 stateGroupId, System.UInt32& stateId) { }
        // RVA: 0x0635B9AC  token: 0x6000022
        public static System.Void SetSwitch(System.UInt32 switchGroup, System.UInt32 switchValue, System.UInt64 audioObjectId) { }
        // RVA: 0x0635BA4C  token: 0x6000023
        public static System.Void SetSwitch(System.String switchGroup, System.String switchValue, System.UInt64 audioObjectId) { }
        // RVA: 0x0459C4D0  token: 0x6000024
        public static System.Void SetRtpc(System.String rtpcName, System.Single rtpcValue, System.Int32 fadeTimeMs) { }
        // RVA: 0x0459C560  token: 0x6000025
        public static System.Void SetRtpc(System.UInt32 rtpcId, System.Single rtpcValue, System.Int32 fadeTimeMs) { }
        // RVA: 0x03F92DA0  token: 0x6000026
        public static System.Void SetRtpc(System.String rtpcName, System.Single rtpcValue, System.UInt64 audioObjectId) { }
        // RVA: 0x03197B80  token: 0x6000027
        public static System.Void SetRtpc(System.UInt32 rtpcId, System.Single rtpcValue, System.UInt64 audioObjectId) { }
        // RVA: 0x0635B7E8  token: 0x6000028
        public static System.Void SetRtpc(System.UInt32 rtpcId, System.Single rtpcValue, System.UInt64 audioObjectId, System.Int32 fadeTimeMs) { }
        // RVA: 0x045E4230  token: 0x6000029
        public static System.Void SetRtpcValueByPlayingID(System.UInt32 rtpcId, System.Single rtpcValue, System.UInt32 playingId, System.Int32 fadeTimeMs) { }
        // RVA: 0x0635AE0C  token: 0x600002A
        public static System.Void ResetRtpc(System.UInt32 rtpcId, System.UInt64 audioObjectId, System.Int32 fadeTimeMs) { }
        // RVA: 0x0635AEA8  token: 0x600002B
        public static System.Void ResetRtpc(System.String rtpcName, System.UInt64 audioObjectId, System.Int32 fadeTimeMs) { }
        // RVA: 0x06359B20  token: 0x600002C
        public static System.Boolean GetRtpcValue(System.String rtpcName, System.UInt64 audioObjectId, System.UInt32 playingId, System.Single& value, System.Int32& valueType) { }
        // RVA: 0x06359C0C  token: 0x600002D
        public static System.Boolean GetRtpcValue(System.UInt32 rtpcId, System.UInt64 audioObjectId, System.UInt32 playingId, System.Single& value, System.Int32& valueType) { }
        // RVA: 0x0635B2E4  token: 0x600002E
        public static System.Void SeekOnEvent(System.String eventName, System.UInt64 audioObjectId, System.Int32 timeMs, System.Boolean toNearestMarker, System.UInt32 playingId) { }
        // RVA: 0x0635B094  token: 0x600002F
        public static System.Void SeekOnEvent(System.UInt32 eventId, System.UInt64 audioObjectId, System.Int32 timeMs, System.Boolean toNearestMarker, System.UInt32 playingId) { }
        // RVA: 0x0635AFA4  token: 0x6000030
        public static System.Void SeekOnEvent(System.String eventName, System.Int32 timeMs, System.Boolean toNearestMarker, System.UInt32 playingId) { }
        // RVA: 0x0635B1F4  token: 0x6000031
        public static System.Void SeekOnEvent(System.UInt32 eventId, System.Int32 timeMs, System.Boolean toNearestMarker, System.UInt32 playingId) { }
        // RVA: 0x0635C008  token: 0x6000032
        public static System.Boolean TryGetPlayingPosition(System.UInt32 playingId, System.Int32& posTimeMs, System.Boolean extrapolate) { }
        // RVA: 0x0635BECC  token: 0x6000033
        public static System.Boolean TryGetMusicPlayingPosition(System.UInt32 playingId, System.Int32& posTimeMs, System.Boolean extrapolate) { }
        // RVA: 0x044B02F0  token: 0x6000034
        public static System.Boolean IsPlaying(System.UInt32 playingId) { }
        // RVA: 0x0635B89C  token: 0x6000035
        public static System.UInt32 SetScalingFactor(System.UInt64 audioObjectId, System.Single factor) { }
        // RVA: 0x0635AA98  token: 0x6000036
        public static System.Boolean RegisterGameObject(System.UInt64 audioObjectId) { }
        // RVA: 0x0635AA40  token: 0x6000037
        public static System.Boolean RegisterGameObject(System.UInt64 audioObjectId, System.String name) { }
        // RVA: 0x0635C20C  token: 0x6000038
        public static System.Boolean UnregisterGameObject(System.UInt64 audioObjectId) { }
        // RVA: 0x0635B6C8  token: 0x6000039
        public static System.Void SetPosition(System.UInt64 audioObjectId, UnityEngine.Vector3 pos, UnityEngine.Vector3 forward, UnityEngine.Vector3 up) { }
        // RVA: 0x0635BB90  token: 0x600003A
        public static System.Void StopAll() { }
        // RVA: 0x0635BB44  token: 0x600003B
        public static System.Void StopAllOnObject(System.UInt64 audioObjectId) { }
        // RVA: 0x03EB1220  token: 0x600003C
        public static System.Void ReleaseAllCachedEventsSync() { }
        // RVA: 0x03B8B550  token: 0x600003D
        public static System.Void SetLargePositions(System.UInt64 audioObjectId, Beyond.Audio.AudioPositionArrayProxy positions, Beyond.Audio.AudioLargeType type) { }
        // RVA: 0x0434CC10  token: 0x600003E
        public static System.Void SetDefaultListener(System.UInt64 audioObjectId) { }
        // RVA: 0x0635B4BC  token: 0x600003F
        public static System.Void SetListener(System.UInt64 audioObjectId, System.UInt64 listenerId) { }
        // RVA: 0x0436E720  token: 0x6000040
        public static System.Boolean TryGetRealPlayingId(System.UInt32 playingId, System.UInt32& realPlayingId) { }
        // RVA: 0x0434CBA0  token: 0x6000041
        public static System.Void SetDefaultEmitter(System.UInt64 audioObjectId) { }
        // RVA: 0x03EB4480  token: 0x6000042
        public static System.Void SetAudioLanguage(System.String language) { }
        // RVA: 0x0635B410  token: 0x6000043
        public static System.Void SetDistanceProbe(System.UInt64 probeObjectId) { }
        // RVA: 0x06359A04  token: 0x6000044
        public static System.UInt32 ComputeIdFromString(System.String name) { }
        // RVA: 0x03EB41E0  token: 0x6000045
        public static System.Void SetAudioEngineInitSetting(Beyond.Audio.AudioEngineInitSetting settings) { }
        // RVA: 0x0635C254  token: 0x6000046
        public static System.Void UnsetAudioEngineInitSetting() { }
        // RVA: 0x03EB4170  token: 0x6000047
        public static System.Boolean InitWwise() { }
        // RVA: 0x0635BCAC  token: 0x6000048
        public static System.Void TermWwise(System.Boolean hgDeepTerm) { }
        // RVA: 0x03EB4710  token: 0x6000049
        public static System.Void LoadInitResources() { }
        // RVA: 0x0635AAE0  token: 0x600004A
        public static System.Void ReloadInitResources() { }
        // RVA: 0x0635AD94  token: 0x600004B
        public static System.Void RenderAudio() { }
        // RVA: 0x0635BC60  token: 0x600004C
        public static System.Void Suspend(System.Boolean renderAnyway) { }
        // RVA: 0x0635C2BC  token: 0x600004D
        public static System.Void WakeupFromSuspend() { }
        // RVA: 0x06359CF8  token: 0x600004E
        public static System.Void InitScePad() { }
        // RVA: 0x0635B3B4  token: 0x600004F
        public static System.Void SetAudioMemoryBudget(System.UInt64 inMemoryBytes, System.UInt16 inGcCount) { }
        // RVA: 0x0635A3CC  token: 0x6000050
        public static System.Void LogToWwise(System.String msg, System.Boolean isError, System.UInt32 playingId, System.UInt64 goId) { }
        // RVA: 0x0635A440  token: 0x6000051
        public static System.Void MemoryShrink() { }
        // RVA: 0x0635BDD8  token: 0x6000052
        public static System.Void TouchAudioHotfixPlaceholder() { }
        // RVA: 0x0635BD20  token: 0x6000053
        public static System.Void TouchAudioHotfixPlaceholderArgs(System.String[] stringArray, System.Int32[] intArray, System.UInt32[] uintArray, System.Single[] floatArray, System.Boolean[] boolArray, System.Int64[] longArray, System.UInt64[] ulongArray, System.Double[] doubleArray, System.Object[] objects) { }
        // RVA: 0x06359518  token: 0x6000054
        public static System.Void AudioHotfixPlaceholder1(System.String[] stringArray, System.Int32[] intArray, System.UInt32[] uintArray, System.Single[] floatArray, System.Boolean[] boolArray, System.Int64[] longArray, System.UInt64[] ulongArray, System.Double[] doubleArray, System.Object[] objects) { }
        // RVA: 0x063596BC  token: 0x6000055
        public static System.Void AudioHotfixPlaceholder2(System.String[] stringArray, System.Int32[] intArray, System.UInt32[] uintArray, System.Single[] floatArray, System.Boolean[] boolArray, System.Int64[] longArray, System.UInt64[] ulongArray, System.Double[] doubleArray, System.Object[] objects) { }
        // RVA: 0x06359860  token: 0x6000056
        public static System.Void AudioHotfixPlaceholder3(System.String[] stringArray, System.Int32[] intArray, System.UInt32[] uintArray, System.Single[] floatArray, System.Boolean[] boolArray, System.Int64[] longArray, System.UInt64[] ulongArray, System.Double[] doubleArray, System.Object[] objects) { }
        // RVA: 0x0635A354  token: 0x6000057
        public static System.Void LoadExtraPckFromPath(System.String path) { }
        // RVA: 0x03AC7880  token: 0x6000058
        private static System.Boolean _InitInternal() { }
        // RVA: 0x0635C7C8  token: 0x6000059
        private static System.Void _ReleaseInternal(System.Boolean hgDeepTerm) { }
        // RVA: 0x0635C73C  token: 0x600005A
        private static System.Void _OnBeforeTerminate() { }
        // RVA: 0x03FE2B10  token: 0x600005B
        private static System.Void _ResetStaticFields() { }
        // RVA: 0x0328C670  token: 0x600005C
        private static System.Void _OnEventPreparedDoPostEvent(System.UInt32 inId, System.IntPtr _, AKRESULT __, System.Object inCookie) { }
        // RVA: 0x0328D3E0  token: 0x600005D
        private static System.Void _OnEventCallback(System.Object inCookie, AkCallbackType inType, AkCallbackInfo inInfo) { }
        // RVA: 0x043C7930  token: 0x600005E
        private static System.Void _OnExternalSourceEventCallback(System.Object inCookie, AkCallbackType inType, AkCallbackInfo inInfo) { }
        // RVA: 0x0328A690  token: 0x600005F
        private static System.UInt32 _PostEvent(System.UInt32 eventId, System.UInt64 audioObjectId, Beyond.Audio.AudioCallbackType callbackType, Beyond.Audio.AudioEventCallback originCallback, System.Object originCookie) { }
        // RVA: 0x03ABEA70  token: 0x6000060
        private static System.UInt32 _PostEventWithExternalSource(System.UInt32 eventId, System.UInt64 audioObjectId, System.String externalSourceKey, System.UInt32 externalCookie, Beyond.Audio.AudioCallbackType callbackType, Beyond.Audio.AudioEventCallback callback, System.Object cookie, Beyond.Audio.AudioCodec codec) { }
        // RVA: 0x03870420  token: 0x6000061
        private static System.Void _ExecuteActionOnPlayingId(AkActionOnEventType type, System.UInt32 playingId, System.Int32 durationMs) { }
        // RVA: 0x0635C604  token: 0x6000062
        private static System.UInt32 _GetInternalPlayingId() { }
        // RVA: 0x0635C65C  token: 0x6000063
        private static System.Void _LoadAndPinEventsAsync(System.Collections.Generic.IList<System.UInt32>& events, System.Action<System.Object>& onDone, System.Object& payload) { }
        // RVA: 0x0635C2FC  token: 0x6000064
        private static System.Void _DoLoadAndPinEventsSync(System.Collections.Generic.IList<System.UInt32> events) { }
        // RVA: 0x03EB4DF0  token: 0x6000065
        private static System.Collections.IEnumerator _DoLoadAndPinEventsAsync(System.Collections.Generic.IList<System.UInt32> events, System.Action<System.Object> onDone, System.Object payload) { }
        // RVA: 0x04D83C80  token: 0x6000066
        private static System.Void _InitAndroidActivityEnvironment() { }
        // RVA: 0x041E1670  token: 0x6000067
        private static System.Void SetUprSamplingEnabled(System.Boolean isEnabled) { }
        // RVA: 0x041E1670  token: 0x6000068
        private static System.Void _ProfilerUpdate() { }
        // RVA: 0x0635CA5C  token: 0x6000069
        private static System.Void _SampleAllData() { }
        // RVA: 0x0635CA9C  token: 0x600006A
        private static System.Void _SampleMemoryOnly() { }
        // RVA: 0x0635BB00  token: 0x600006B
        public static System.Void StartProfileCapture(System.String fileName) { }
        // RVA: 0x0635BC20  token: 0x600006C
        public static System.Void StopProfileCapture() { }
        // RVA: 0x06359AE0  token: 0x600006D
        public static AkResourceMonitorDataSummary GetResourceSummaryData() { }
        // RVA: 0x06359A88  token: 0x600006E
        public static Beyond.Audio.AudioMemoryMonitor.FGlobalMemoryStats GetGlobalMemoryStats() { }
        // RVA: 0x06359A24  token: 0x600006F
        public static Beyond.Audio.AudioMemoryMonitor.FExtraMemoryStats GetExtraMemoryStats() { }
        // RVA: 0x040087B0  token: 0x6000070
        public static System.Void SetEnableSpatialAudio(System.Boolean enable) { }
        // RVA: 0x0635B588  token: 0x6000071
        public static System.Void SetObjectObstructionAndOcclusion(System.UInt64 audioObjectId, System.Single obstructionLevel, System.Single occlusionLevel) { }
        // RVA: 0x03529D60  token: 0x6000072
        public static System.Boolean SetGeometry(System.UInt64 geoId, UnityEngine.Vector3[] vertices, System.UInt16[] triangles, Beyond.Audio.AudioAcousticSurfaceData[] surfaceData, System.UInt16[] surfaceIdx, System.Boolean enableDiffraction, System.Boolean enableEdgeDiffraction) { }
        // RVA: 0x03CDEB70  token: 0x6000073
        public static System.Boolean SetGeometryInstance(System.UInt64 instanceId, UnityEngine.Vector3 pos, UnityEngine.Vector3 scale, UnityEngine.Quaternion rot, System.UInt64 geoId, System.Boolean useReflectionAndDiffraction) { }
        // RVA: 0x03CDF150  token: 0x6000074
        public static System.Boolean SetRoom(System.UInt64 roomId, Beyond.Audio.AudioRoomData roomData, System.UInt64 geoInstanceId, System.String roomName) { }
        // RVA: 0x03CDFFE0  token: 0x6000075
        public static System.Boolean SetPortal(System.UInt64 portalId, System.UInt64 roomIdA, System.UInt64 roomIdB, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 extent, System.Boolean enabled, System.String portalName) { }
        // RVA: 0x0635B658  token: 0x6000076
        public static System.Boolean SetPortalObstructionAndOcclusion(System.UInt64 portalId, System.Single obstruction, System.Single occlusion) { }
        // RVA: 0x0635B778  token: 0x6000077
        public static System.Boolean SetReverbZone(System.UInt64 reverbZoneId, System.UInt64 parentRoom, System.Single transitionWidth) { }
        // RVA: 0x0635AD44  token: 0x6000078
        public static System.Boolean RemoveRoom(System.UInt64 roomId) { }
        // RVA: 0x0635ACA4  token: 0x6000079
        public static System.Boolean RemovePortal(System.UInt64 portalId) { }
        // RVA: 0x0635AC54  token: 0x600007A
        public static System.Boolean RemoveGeometry(System.UInt64 geoId) { }
        // RVA: 0x0635AC04  token: 0x600007B
        public static System.Boolean RemoveGeometryInstance(System.UInt64 instanceId) { }
        // RVA: 0x0635ACF4  token: 0x600007C
        public static System.Boolean RemoveReverbZone(System.UInt64 reverbZoneId) { }
        // RVA: 0x03EAE030  token: 0x600007D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x80
    public sealed class AudioEventCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04367000  token: 0x600009C
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0622803C  token: 0x600009D
        public virtual System.Void Invoke(System.Object in_cookie, Beyond.Audio.AudioCallbackType in_type, Beyond.Audio.AudioCallbackInfo in_info) { }
        // RVA: 0x0635E244  token: 0x600009E
        public virtual System.IAsyncResult BeginInvoke(System.Object in_cookie, Beyond.Audio.AudioCallbackType in_type, Beyond.Audio.AudioCallbackInfo in_info, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600009F
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public class AudioStringAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000A0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public class AudioVectorAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x18
    public class AudioEventWarnAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public Beyond.Audio.AudioEventWarnAttribute.WarnType warnType;  // 0x10

        // Methods
        // RVA: 0x04D86130  token: 0x60000A2
        public System.Void .ctor(Beyond.Audio.AudioEventWarnAttribute.WarnType type) { }

    }

    // TypeToken: 0x2000015  // size: 0x28
    public class BankHandle : System.IDisposable
    {
        // Fields
        private readonly System.String <name>k__BackingField;  // 0x10
        private AKRESULT <result>k__BackingField;  // 0x18
        private System.Boolean <isDone>k__BackingField;  // 0x1c
        private System.Int32 m_refCount;  // 0x20

        // Properties
        System.String name { get; /* RVA: 0x02B2ECC0 */ }
        AKRESULT result { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Boolean isDone { get; /* RVA: 0x04D86150 */ set; /* RVA: 0x04D86160 */ }
        System.Int32 refCount { get; /* RVA: 0x0635F95C */ }

        // Methods
        // RVA: 0x03EAE680  token: 0x60000A9
        public System.Void .ctor(System.String bankName) { }
        // RVA: 0x0635F890  token: 0x60000AA
        public virtual System.Void Dispose() { }
        // RVA: 0x0635F910  token: 0x60000AB
        public System.Void IncRef() { }
        // RVA: 0x0635F844  token: 0x60000AC
        public System.Void DecRef() { }

    }

    // TypeToken: 0x2000016  // size: 0x18
    public class AsyncLoadBankTask : System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private readonly System.Collections.Generic.List<Beyond.Audio.BankHandle> m_handles;  // 0x10

        // Properties
        System.Object Current { get; /* RVA: 0x03CF12F0 */ }

        // Methods
        // RVA: 0x03EADFB0  token: 0x60000AD
        public static Beyond.Audio.AsyncLoadBankTask LoadBanksAsync(System.Collections.Generic.IList<System.String> bankNames) { }
        // RVA: 0x03EAE8A0  token: 0x60000AE
        public System.Void .ctor(System.Collections.Generic.IList<System.String> bankNames) { }
        // RVA: 0x0481FF90  token: 0x60000AF
        public virtual System.Boolean MoveNext() { }
        // RVA: 0x06358EE8  token: 0x60000B0
        public virtual System.Void Reset() { }
        // RVA: 0x06358D98  token: 0x60000B2
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public static class AudioBankManager
    {
        // Fields
        private static readonly AkCallbackManager.BankCallback s_loadBankCallback;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.String,Beyond.Audio.BankHandle> s_loadedBankHandles;  // static @ 0x8

        // Methods
        // RVA: 0x03EB6F50  token: 0x60000B3
        public static System.Void LoadMainPCK() { }
        // RVA: 0x03EAEE30  token: 0x60000B4
        public static Beyond.Audio.BankHandle LoadBankAsync(System.String bankName, System.Action callback) { }
        // RVA: 0x03EAE6A0  token: 0x60000B5
        private static System.Void _LoadBankCallback(System.UInt32 inBankId, System.IntPtr _, AKRESULT inLoadResult, System.Object inCookie) { }
        // RVA: 0x0635DC44  token: 0x60000B6
        public static System.Boolean UnloadBank(System.String bankName) { }
        // RVA: 0x0635DA48  token: 0x60000B7
        public static System.Void UnloadAllBanks() { }
        // RVA: 0x03EAE7F0  token: 0x60000B8
        public static System.Boolean IsBankLoaded(System.String bankName) { }
        // RVA: 0x0635D9B8  token: 0x60000B9
        public static System.Void Reset() { }
        // RVA: 0x0635D884  token: 0x60000BA
        public static System.Void LoadDebugFilePackage(System.String path) { }
        // RVA: 0x0635D7D4  token: 0x60000BB
        public static System.Void AddDebugBankPath(System.String path) { }
        // RVA: 0x0635DE30  token: 0x60000BC
        private static System.Void _PreResetBasePath(System.Collections.Generic.List<System.UInt32>& unPinnedEventIds) { }
        // RVA: 0x0635DDB4  token: 0x60000BD
        private static System.Void _PostResetBasePath(System.Collections.Generic.List<System.UInt32> unPinnedEventIds) { }
        // RVA: 0x04D14E80  token: 0x60000BE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x10
    public class AudioConstants
    {
        // Fields
        public static System.UInt64 AUDIO_INVALID_GAME_OBJECT;  // const
        public static System.UInt32 AUDIO_INVALID_UNIQUE_ID;  // const
        public static System.UInt32 AUDIO_INVALID_INTERNAL_PLAYING_ID;  // const
        public static System.UInt64 AUDIO_GLOBAL_GAME_OBJECT;  // const
        public static System.UInt64 AUDIO_ROOM_OUTDOOR_ID;  // const
        public static System.UInt64 AUDIO_ROOM_OUTDOORS_ID;  // const
        public static System.String AU_UI_BATTLE_BREAK_POISE;  // const
        public static System.String AU_GLOBAL_GAIN_ON;  // const
        public static System.String AU_GLOBAL_GAIN_OFF;  // const
        public static System.UInt32 AU_HASH_EXTERNAL_SOURCE_COOKIE;  // const
        public static System.UInt32 AU_BANK_INIT;  // const
        public static System.String AU_DEVICE_CONTROLLER_SPEAKER;  // const
        public static System.Int32 AUDIO_SPEAKER_SETUP_4_0;  // const
        public static System.String AU_DEVICE_WWISE_MOTION;  // const
        public static System.String AU_DEVICE_SYSTEM;  // const
        public static System.String AU_DEVICE_SYSTEM_3D;  // const
        public static System.UInt32 AUDIO_UI_FULLSCREEN_OPEN;  // const
        public static System.UInt32 AUDIO_UI_FULLSCREEN_CLOSE;  // const
        public static System.UInt32 AUDIO_UI_FULLSCREEN_PAUSE;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x60000C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x14
    public sealed struct AudioId
    {
        // Fields
        public System.Int32 _id;  // 0x10

        // Methods
        // RVA: 0x04D86140  token: 0x60000C1
        public System.Void .ctor(System.UInt32 id) { }
        // RVA: 0x0635E5C4  token: 0x60000C2
        public System.String GetName() { }
        // RVA: 0x03197AD0  token: 0x60000C3
        public static System.UInt32 op_Implicit(Beyond.Audio.AudioId id) { }
        // RVA: 0x03197330  token: 0x60000C4
        public static System.Int32 op_Implicit(Beyond.Audio.AudioId id) { }
        // RVA: 0x03B896B0  token: 0x60000C5
        public static Beyond.Audio.AudioId op_Implicit(System.UInt32 id) { }
        // RVA: 0x0635E6A8  token: 0x60000C6
        public static Beyond.Audio.AudioId op_Implicit(System.Int32 id) { }
        // RVA: 0x0635E63C  token: 0x60000C7
        public static Beyond.Audio.AudioId op_Implicit(System.String id) { }

    }

    // TypeToken: 0x200001B  // size: 0x10
    public static class AudioIdExtensions
    {
        // Methods
        // RVA: 0x0635E3E0  token: 0x60000C8
        public static System.UInt32[] ToUIntArray(System.Collections.Generic.ICollection<Beyond.Audio.AudioId> ids) { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public sealed struct AudioState : System.IEquatable`1
    {
        // Fields
        private System.Int32 _stateGroup;  // 0x10
        private System.Int32 _stateValue;  // 0x14

        // Properties
        System.UInt32 stateGroup { get; /* RVA: 0x0635F13C */ }
        System.UInt32 stateValue { get; /* RVA: 0x0635F188 */ }

        // Methods
        // RVA: 0x04D85C40  token: 0x60000CB
        public System.Void .ctor(System.UInt32 stateGroup, System.UInt32 stateValue) { }
        // RVA: 0x0635EEEC  token: 0x60000CC
        public virtual System.Boolean Equals(Beyond.Audio.AudioState other) { }
        // RVA: 0x0635EF58  token: 0x60000CD
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0635EFF4  token: 0x60000CE
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0635F08C  token: 0x60000CF
        public System.Boolean <>iFixBaseProxy_Equals(System.Object P0) { }
        // RVA: 0x0635F0EC  token: 0x60000D0
        public System.Int32 <>iFixBaseProxy_GetHashCode() { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public sealed struct AudioIdList
    {
        // Fields
        public Beyond.Audio.AudioId[] _ids;  // 0x10

    }

    // TypeToken: 0x200001E  // size: 0x18
    public sealed struct AudioStateList
    {
        // Fields
        public Beyond.Audio.AudioState[] _states;  // 0x10

    }

    // TypeToken: 0x200001F  // size: 0x18
    public sealed struct AudioEngineInitSetting
    {
        // Fields
        public AkWwiseInitializationSettings akInitSettings;  // 0x10

    }

    // TypeToken: 0x2000020  // size: 0x2C
    public sealed struct AudioLargeSource
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Quaternion rotation;  // 0x1c

    }

    // TypeToken: 0x2000021  // size: 0x28
    public sealed struct AudioCallbackInfo
    {
        // Fields
        public System.UInt64 objectId;  // 0x10
        public System.IntPtr pCookie;  // 0x18
        private AkCallbackInfo rawInfo;  // 0x20

        // Methods
        // RVA: 0x0635DF50  token: 0x60000D1
        public Beyond.Audio.AudioDurationCallbackInfo CastToDurationCallbackInfo() { }

    }

    // TypeToken: 0x2000022  // size: 0x24
    public sealed struct AudioDurationCallbackInfo
    {
        // Fields
        public System.Single duration;  // 0x10
        public System.Single estimatedDuration;  // 0x14
        public System.UInt32 audioNodeId;  // 0x18
        public System.UInt32 mediaId;  // 0x1c
        public System.Boolean isStreaming;  // 0x20

    }

    // TypeToken: 0x2000023  // size: 0x18
    public class AudioPositionArrayProxy
    {
        // Fields
        private AkPositionArray m_array;  // 0x10

        // Properties
        System.UInt32 count { get; /* RVA: 0x03B8B650 */ }

        // Methods
        // RVA: 0x035AB520  token: 0x60000D3
        public System.Void .ctor(System.UInt32 count) { }
        // RVA: 0x03B8B620  token: 0x60000D4
        public AkPositionArray GetRawArray() { }
        // RVA: 0x03B8B3D0  token: 0x60000D5
        public System.Void Add(UnityEngine.Vector3 position, UnityEngine.Vector3 forward, UnityEngine.Vector3 up) { }
        // RVA: 0x03B8B8A0  token: 0x60000D6
        public System.Void Reset() { }
        // RVA: 0x04416C20  token: 0x60000D7
        protected virtual System.Void Finalize() { }

    }

    // TypeToken: 0x2000024  // size: 0x18
    public sealed struct AudioAcousticSurfaceData
    {
        // Fields
        public System.UInt32 textureId;  // 0x10
        public System.Single transmissionLoss;  // 0x14

    }

    // TypeToken: 0x2000025  // size: 0x44
    public sealed struct AudioRoomData
    {
        // Fields
        public System.UInt32 roomToneId;  // 0x10
        public System.UInt32 priority;  // 0x14
        public System.UInt32 auxBusId;  // 0x18
        public System.UInt32 parentRoomId;  // 0x1c
        public System.Single auxLevel;  // 0x20
        public System.Single auxLevelToSelf;  // 0x24
        public System.Single transmissionLoss;  // 0x28
        public System.Single transitionWidth;  // 0x2c
        public System.Single t60DecayTIme;  // 0x30
        public System.Single preDelayTime;  // 0x34
        public System.Single reverbLevel;  // 0x38
        public System.Single erLevel;  // 0x3c
        public Beyond.Audio.AudioRoomData.EBoolFlags flags;  // 0x40

    }

    // TypeToken: 0x2000027  // size: 0x14
    public sealed struct AudioCallbackType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Audio.AudioCallbackType None;  // const
        public static Beyond.Audio.AudioCallbackType AU_EndOfEvent;  // const
        public static Beyond.Audio.AudioCallbackType AU_EndOfDynamicSequenceItem;  // const
        public static Beyond.Audio.AudioCallbackType AU_Marker;  // const
        public static Beyond.Audio.AudioCallbackType AU_Duration;  // const
        public static Beyond.Audio.AudioCallbackType AU_SpeakerVolumeMatrix;  // const
        public static Beyond.Audio.AudioCallbackType AU_Starvation;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicPlaylistSelect;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicPlayStarted;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncBeat;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncBar;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncEntry;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncExit;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncGrid;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncUserCue;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncPoint;  // const
        public static Beyond.Audio.AudioCallbackType AU_MusicSyncAll;  // const
        public static Beyond.Audio.AudioCallbackType AU_MIDIEvent;  // const
        public static Beyond.Audio.AudioCallbackType AU_CallbackBits;  // const
        public static Beyond.Audio.AudioCallbackType AU_EnableGetSourcePlayPosition;  // const
        public static Beyond.Audio.AudioCallbackType AU_EnableGetMusicPlayPosition;  // const
        public static Beyond.Audio.AudioCallbackType AU_EnableGetSourceStreamBuffering;  // const
        public static Beyond.Audio.AudioCallbackType AU_Monitoring;  // const
        public static Beyond.Audio.AudioCallbackType AU_Bank;  // const
        public static Beyond.Audio.AudioCallbackType AU_AudioInterruption;  // const
        public static Beyond.Audio.AudioCallbackType AU_AudioSourceChange;  // const

    }

    // TypeToken: 0x2000028  // size: 0x11
    public sealed struct AudioLargeType
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Audio.AudioLargeType Sum;  // const
        public static Beyond.Audio.AudioLargeType Nearest;  // const
        public static Beyond.Audio.AudioLargeType Single;  // const

    }

    // TypeToken: 0x2000029  // size: 0x14
    public sealed struct AudioPlayableActions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Audio.AudioPlayableActions None;  // const
        public static Beyond.Audio.AudioPlayableActions Playback;  // const
        public static Beyond.Audio.AudioPlayableActions Stop;  // const
        public static Beyond.Audio.AudioPlayableActions Seek;  // const

    }

    // TypeToken: 0x200002A  // size: 0x14
    public sealed struct AudioEventType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Audio.AudioEventType Sfx;  // const
        public static Beyond.Audio.AudioEventType Music;  // const
        public static Beyond.Audio.AudioEventType State;  // const
        public static Beyond.Audio.AudioEventType GameSync;  // const
        public static Beyond.Audio.AudioEventType Voice;  // const
        public static Beyond.Audio.AudioEventType Controller;  // const
        public static Beyond.Audio.AudioEventType Vibration;  // const
        public static Beyond.Audio.AudioEventType Global;  // const

    }

    // TypeToken: 0x200002B  // size: 0x14
    public sealed struct AudioMixDownMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Audio.AudioMixDownMode Direct;  // const
        public static Beyond.Audio.AudioMixDownMode ForceToMono;  // const

    }

    // TypeToken: 0x200002C  // size: 0x14
    public sealed struct AudioMasteringSuiteMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Audio.AudioMasteringSuiteMode Headphone;  // const
        public static Beyond.Audio.AudioMasteringSuiteMode TV;  // const
        public static Beyond.Audio.AudioMasteringSuiteMode HomeCinema;  // const
        public static Beyond.Audio.AudioMasteringSuiteMode SoundBar;  // const
        public static Beyond.Audio.AudioMasteringSuiteMode Speaker;  // const
        public static Beyond.Audio.AudioMasteringSuiteMode DevMode;  // const

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct AudioChannelConfigType
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static Beyond.Audio.AudioChannelConfigType Anonymous;  // const
        public static Beyond.Audio.AudioChannelConfigType Standard;  // const
        public static Beyond.Audio.AudioChannelConfigType Ambisonic;  // const
        public static Beyond.Audio.AudioChannelConfigType Objects;  // const
        public static Beyond.Audio.AudioChannelConfigType UseDeviceMain;  // const
        public static Beyond.Audio.AudioChannelConfigType UseDevicePassthrough;  // const

    }

    // TypeToken: 0x200002E  // size: 0x14
    public sealed struct AudioCodec
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static Beyond.Audio.AudioCodec PCM;  // const
        public static Beyond.Audio.AudioCodec ADPCM;  // const
        public static Beyond.Audio.AudioCodec VORBIS;  // const
        public static Beyond.Audio.AudioCodec ATRAC9;  // const
        public static Beyond.Audio.AudioCodec OPUS_WEM;  // const

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct AudioChannelMask
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static Beyond.Audio.AudioChannelMask NONE;  // const
        public static Beyond.Audio.AudioChannelMask FRONT_LEFT;  // const
        public static Beyond.Audio.AudioChannelMask FRONT_RIGHT;  // const
        public static Beyond.Audio.AudioChannelMask FRONT_CENTER;  // const
        public static Beyond.Audio.AudioChannelMask LOW_FREQUENCY;  // const
        public static Beyond.Audio.AudioChannelMask BACK_LEFT;  // const
        public static Beyond.Audio.AudioChannelMask BACK_RIGHT;  // const
        public static Beyond.Audio.AudioChannelMask BACK_CENTER;  // const
        public static Beyond.Audio.AudioChannelMask SIDE_LEFT;  // const
        public static Beyond.Audio.AudioChannelMask SIDE_RIGHT;  // const
        public static Beyond.Audio.AudioChannelMask TOP;  // const
        public static Beyond.Audio.AudioChannelMask HEIGHT_FRONT_LEFT;  // const
        public static Beyond.Audio.AudioChannelMask HEIGHT_FRONT_CENTER;  // const
        public static Beyond.Audio.AudioChannelMask HEIGHT_FRONT_RIGHT;  // const
        public static Beyond.Audio.AudioChannelMask HEIGHT_BACK_LEFT;  // const
        public static Beyond.Audio.AudioChannelMask HEIGHT_BACK_CENTER;  // const
        public static Beyond.Audio.AudioChannelMask HEIGHT_BACK_RIGHT;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_MONO;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_0POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_1POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_STEREO;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_2POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_3STEREO;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_3POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_4;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_4POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_5;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_5POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_6;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_6POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_7;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_7POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_SURROUND;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DPL2;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_HEIGHT_4;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_HEIGHT_5;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_HEIGHT_ALL;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_222;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_8;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_9;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_9POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_10;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_10POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_11;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_11POINT1;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_11_740;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_11POINT1_740;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_13_751;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_AURO_13POINT1_751;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_5_0_2;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_5_1_2;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_6_0_2;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_6_1_2;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_6_0_4;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_6_1_4;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_7_0_2;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_7_1_2;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_7_0_4;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_DOLBY_7_1_4;  // const
        public static Beyond.Audio.AudioChannelMask SETUP_ALL_SPEAKERS;  // const

    }

    // TypeToken: 0x2000030  // size: 0x14
    public sealed struct AudioLogChannel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Audio.AudioLogChannel NONE;  // const
        public static Beyond.Audio.AudioLogChannel API;  // const
        public static Beyond.Audio.AudioLogChannel IO;  // const
        public static Beyond.Audio.AudioLogChannel OBJECT;  // const
        public static Beyond.Audio.AudioLogChannel MUSIC;  // const
        public static Beyond.Audio.AudioLogChannel SPATIAL;  // const
        public static Beyond.Audio.AudioLogChannel SCENE;  // const
        public static Beyond.Audio.AudioLogChannel VOICE;  // const
        public static Beyond.Audio.AudioLogChannel GAMEPLAY;  // const
        public static Beyond.Audio.AudioLogChannel GP_STATE;  // const
        public static Beyond.Audio.AudioLogChannel GP_CUE;  // const
        public static Beyond.Audio.AudioLogChannel GP_FACTORY;  // const
        public static Beyond.Audio.AudioLogChannel GP_UI;  // const
        public static Beyond.Audio.AudioLogChannel GP_ANIM;  // const
        public static Beyond.Audio.AudioLogChannel GP_TIMELINE;  // const
        public static Beyond.Audio.AudioLogChannel MOTION;  // const
        public static Beyond.Audio.AudioLogChannel WWISE;  // const
        public static Beyond.Audio.AudioLogChannel EDITOR;  // const
        public static Beyond.Audio.AudioLogChannel MISC;  // const
        public static Beyond.Audio.AudioLogChannel ALL;  // const

    }

    // TypeToken: 0x2000031  // size: 0x10
    public static class AudioLogger
    {
        // Fields
        public static Beyond.Audio.AudioLogChannel s_enabledChannels;  // static @ 0x0

        // Methods
        // RVA: 0x0378D3B0  token: 0x60000D8
        public static System.Boolean IsChannelEnabled(Beyond.Audio.AudioLogChannel channel) { }
        // RVA: 0x0635E77C  token: 0x60000D9
        public static System.Void EnableChannel(Beyond.Audio.AudioLogChannel channel) { }
        // RVA: 0x0635E6F4  token: 0x60000DA
        public static System.Void DisableChannel(Beyond.Audio.AudioLogChannel channel) { }
        // RVA: 0x0635E9B4  token: 0x60000DB
        public static System.Void SetChannels(Beyond.Audio.AudioLogChannel channel) { }
        // RVA: 0x0635E910  token: 0x60000DC
        public static System.Void Log(Beyond.Audio.AudioLogChannel channel, System.String msg) { }
        // RVA: -1  // generic def  token: 0x60000DD
        public static System.Void Log(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1) { }
        // RVA: -1  // generic def  token: 0x60000DE
        public static System.Void Log(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000DF
        public static System.Void Log(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000E0
        public static System.Void Log(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000E1
        public static System.Void Log(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: 0x0635E888  token: 0x60000E2
        public static System.Void LogWarning(Beyond.Audio.AudioLogChannel channel, System.String msg) { }
        // RVA: -1  // generic def  token: 0x60000E3
        public static System.Void LogWarning(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1) { }
        // RVA: -1  // generic def  token: 0x60000E4
        public static System.Void LogWarning(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000E5
        public static System.Void LogWarning(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000E6
        public static System.Void LogWarning(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: 0x0635E800  token: 0x60000E7
        public static System.Void LogError(Beyond.Audio.AudioLogChannel channel, System.String msg) { }
        // RVA: -1  // generic def  token: 0x60000E8
        public static System.Void LogError(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1) { }
        // RVA: -1  // generic def  token: 0x60000E9
        public static System.Void LogError(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000EA
        public static System.Void LogError(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000EB
        public static System.Void LogError(Beyond.Audio.AudioLogChannel channel, System.String msg, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: 0x0635EBB4  token: 0x60000EC
        private static System.Void _DoLog(Beyond.Audio.AudioLogChannel channel, System.String msg) { }
        // RVA: 0x0635EB08  token: 0x60000ED
        private static System.Void _DoLogWarning(Beyond.Audio.AudioLogChannel channel, System.String msg) { }
        // RVA: 0x0635EA34  token: 0x60000EE
        private static System.Void _DoLogError(Beyond.Audio.AudioLogChannel channel, System.String msg) { }
        // RVA: 0x04D7AD50  token: 0x60000EF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x10
    public static class AudioAcousticUtil
    {
        // Fields
        private static System.Single SOUND_SPEED;  // const

        // Methods
        // RVA: 0x0635909C  token: 0x60000F0
        public static System.Single RT60DecayTime(System.Single volume, System.Single surfaceArea, System.Single absorptionFactor) { }
        // RVA: 0x06359024  token: 0x60000F1
        public static System.Single PreDelayTime(UnityEngine.Vector3 size) { }
        // RVA: 0x06358F38  token: 0x60000F2
        public static System.Single CriticalDistance(System.Single volume, System.Single rt60, System.Single gamma) { }
        // RVA: 0x06359144  token: 0x60000F3
        public static System.Single ReverbIntensity(UnityEngine.Vector3 size) { }
        // RVA: 0x06358FC8  token: 0x60000F4
        public static System.Single ErIntensity(UnityEngine.Vector3 size) { }

    }

    // TypeToken: 0x2000033  // size: 0x10
    public static class AudioActionQueueHelper
    {
        // Fields
        private static System.Collections.Generic.Queue<Beyond.Audio.AudioActionQueueHelper.FExecuteAction> s_executeActionQueue;  // static @ 0x0
        private static System.Collections.Generic.Queue<Beyond.Audio.AudioActionQueueHelper.FRTPCAction> s_rtpcActionQueue;  // static @ 0x8
        private static System.Collections.Generic.Queue<Beyond.Audio.AudioActionQueueHelper.FSeekAction> s_seekActionQueue;  // static @ 0x10
        private static System.Int32 QUEUE_LIFETIME_FRAME;  // const

        // Methods
        // RVA: 0x03870520  token: 0x60000F5
        private static System.Void QueueExecuteAction(System.UInt32 playingId, AkActionOnEventType actionType, System.Int32 durationMs) { }
        // RVA: 0x063592C0  token: 0x60000F6
        private static System.Void QueueRtpcAction(System.UInt32 playingId, System.UInt32 rtpcId, System.Single targetValue, System.Int32 durationMs) { }
        // RVA: 0x063593C4  token: 0x60000F7
        private static System.Void QueueSeekAction(System.UInt32 playingId, System.UInt32 eventId, System.UInt64 audioObjectId, System.Int32 timeMs, System.Boolean toNearestMarker) { }
        // RVA: 0x0328BF40  token: 0x60000F8
        private static System.Void ConsumeQueue() { }
        // RVA: 0x063591D0  token: 0x60000F9
        private static System.Void ClearQueue() { }
        // RVA: 0x0328C150  token: 0x60000FA
        private static System.Void _ConsumeExecute(System.Int32 currFrame) { }
        // RVA: 0x0328C350  token: 0x60000FB
        private static System.Void _ConsumeRtpc(System.Int32 currFrame) { }
        // RVA: 0x0328C3F0  token: 0x60000FC
        private static System.Void _ConsumeSeek(System.Int32 currFrame) { }
        // RVA: 0x04A7CB10  token: 0x60000FD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x10
    public static class AudioAssetCache
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>> s_lruUsingEvents;  // static @ 0x0
        private static Beyond.Audio.AudioAssetCache.Cache s_cachedEvents;  // static @ 0x8
        private static System.Collections.Generic.HashSet<System.UInt32> s_pinnedEvents;  // static @ 0x10
        private static System.Int32 INIT_PREPARE_CAP;  // const
        private static Beyond.PoolCore.ObjectPool<System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>> s_nodePool;  // static @ 0x18

        // Methods
        // RVA: 0x03FE2CB0  token: 0x60000FE
        private static System.Void Reset() { }
        // RVA: 0x037A6CD0  token: 0x60000FF
        private static System.Void PinEvent(System.UInt32 inId) { }
        // RVA: 0x0635CE3C  token: 0x6000100
        private static System.Void UnpinEvent(System.UInt32 inId) { }
        // RVA: 0x0635CADC  token: 0x6000101
        private static System.Void GetAllUsingAssetsAndRelease(System.Collections.Generic.List<System.UInt32>& allIds) { }
        // RVA: 0x03EB1070  token: 0x6000102
        private static System.UInt32 GetLeastActiveAssetAndUncache() { }
        // RVA: 0x03EB1360  token: 0x6000103
        private static System.Int32 GetCachedAssetCount() { }
        // RVA: 0x0328AC60  token: 0x6000104
        private static System.Boolean ActivateAsset(System.UInt32 inId) { }
        // RVA: 0x0328A390  token: 0x6000105
        private static System.Void DeactivateAsset(System.UInt32 inId) { }
        // RVA: 0x03EB0F20  token: 0x6000106
        private static System.Void ForceReleaseCachedAsset(System.UInt32 inId) { }
        // RVA: 0x0635CF20  token: 0x6000107
        private static System.Void _NotifyDebugger() { }
        // RVA: 0x0635CEDC  token: 0x6000108
        private static System.Void _NotifyDebuggerCacheHit(System.Boolean isHit) { }
        // RVA: 0x0635CFE8  token: 0x6000109
        private static System.Void _ResetDebugger() { }
        // RVA: 0x0635CF60  token: 0x600010A
        private static System.Void _PinnedEventLeakDetect() { }
        // RVA: 0x049DB5F0  token: 0x600010B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003B  // size: 0x10
    public static class AudioAssetHelper
    {
        // Fields
        private static System.UInt64 s_memoryBudget;  // static @ 0x0
        private static System.Single FACTOR_B_TO_MB;  // const
        private static System.UInt16 s_gcCount;  // static @ 0x8
        private static System.Int32 s_gcPendingCount;  // static @ 0xc
        private static System.Collections.Generic.Queue<Beyond.Audio.AudioAssetHelper.FOnBankLoadedDoPrepareEventPayload> s_waitingCallbacks;  // static @ 0x10
        private static System.Collections.Generic.Queue<Beyond.Audio.AudioAssetHelper.FPendingLoadRequest> s_pendingLoadRequests;  // static @ 0x18
        private static System.Collections.Generic.HashSet<System.UInt32> s_solidLoadedEvents;  // static @ 0x20
        private static readonly System.UInt32[] S_REUSABLE_UINT_ARRAY;  // static @ 0x28
        private static readonly AkCallbackManager.BankCallback ON_UNPREPARED_DO_UNLOAD_BANK;  // static @ 0x30
        private static readonly AkCallbackManager.BankCallback ON_DONE_UNLOAD_BANK;  // static @ 0x38
        private static readonly AkCallbackManager.BankCallback ON_BANK_LOADED_DO_PREPARE_EVENT;  // static @ 0x40
        private static readonly AkCallbackManager.BankCallback ON_DONE_PREPARE_EVENT;  // static @ 0x48

        // Properties
        System.Boolean isGcProcessing { get; /* RVA: 0x0328B210 */ }

        // Methods
        // RVA: 0x04A29290  token: 0x6000118
        private static System.Void SetMemoryBudget(System.UInt64 inBudgetBytes, System.UInt16 inGcCount) { }
        // RVA: 0x0635D028  token: 0x6000119
        private static System.Boolean HasPreparingEvents() { }
        // RVA: 0x0635D268  token: 0x600011A
        private static System.Void UnPrepareAllEventsSync() { }
        // RVA: 0x0635D0B4  token: 0x600011B
        private static System.Void LoadEventAsync(System.UInt32 inId, AkCallbackManager.BankCallback callback, System.Object cookie) { }
        // RVA: 0x04815030  token: 0x600011C
        private static System.Void _TryConsumePendingLoadRequests() { }
        // RVA: 0x0328B180  token: 0x600011D
        private static System.Void _ConsumePendingLoadRequests() { }
        // RVA: 0x0328AFB0  token: 0x600011E
        private static System.Void _DoLoadEventAsync(System.UInt32 inId, Beyond.Audio.AudioAssetHelper.FOnBankLoadedDoPrepareEventPayload payload) { }
        // RVA: 0x0635D220  token: 0x600011F
        private static System.Void PinEvent(System.UInt32 inId) { }
        // RVA: 0x0635D654  token: 0x6000120
        private static System.Void UnpinEvent(System.UInt32 inId) { }
        // RVA: 0x03FE2BA0  token: 0x6000121
        private static System.Void ResetStaticFields() { }
        // RVA: 0x03EB1280  token: 0x6000122
        private static System.Void ReleaseAllCachedEventsSync() { }
        // RVA: 0x0635D764  token: 0x6000123
        private static System.Void _TryGC() { }
        // RVA: 0x03EB0770  token: 0x6000124
        private static System.Void _DoGc(System.Int32 gcCount) { }
        // RVA: 0x0635D60C  token: 0x6000125
        private static System.Void UnloadEvent(System.UInt32 inId) { }
        // RVA: 0x03EB0930  token: 0x6000126
        private static System.Void _OnUnpreparedDoUnloadBank(System.UInt32 inId, System.IntPtr inInMemoryBankPtr, AKRESULT inLoadResult, System.Object inCookie) { }
        // RVA: 0x04814FB0  token: 0x6000127
        private static System.Void _OnDoneUnloadBank(System.UInt32 inId, System.IntPtr inInMemoryBankPtr, AKRESULT inLoadResult, System.Object inCookie) { }
        // RVA: 0x03EB0A70  token: 0x6000128
        private static System.Void _OnBankLoadedDoPrepareEvent(System.UInt32 inId, System.IntPtr _, AKRESULT inLoadResult, System.Object __) { }
        // RVA: 0x03EB0E80  token: 0x6000129
        private static System.Void _OnDonePrepareEvent(System.UInt32 inId, System.IntPtr _, AKRESULT __, System.Object ___) { }
        // RVA: 0x0328CF20  token: 0x600012A
        private static System.Void _TryDequeueAndInvokeCallback() { }
        // RVA: 0x0635D720  token: 0x600012B
        private static System.Void _RemoveLoadingDebug(System.UInt32 inId) { }
        // RVA: 0x0635D69C  token: 0x600012C
        private static System.Void _AddLoadingDebug(System.UInt32 inId) { }
        // RVA: 0x0635D6E0  token: 0x600012D
        private static System.Void _ClearLoadingDebug() { }
        // RVA: 0x03EAE220  token: 0x600012E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x10
    public static class AudioCallbackMessageParser
    {
        // Fields
        private static readonly AkCallbackManager.MonitoringCallback s_onMonitoringCallback;  // static @ 0x0

        // Methods
        // RVA: 0x04A35980  token: 0x600012F
        private static System.Void Set() { }
        // RVA: 0x041E1670  token: 0x6000130
        private static System.Void _OnMonitoringCallback(AkMonitorErrorCode inErrorCode, AkMonitorErrorLevel inErrorLevel, System.UInt32 inPlayingID, System.UInt64 inGameObjID, System.String inMsg) { }
        // RVA: 0x011C5270  token: 0x6000131
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003F  // size: 0x10
    public static class AudioDebugInfoContainer
    {
        // Methods
        // RVA: 0x0635E1F0  token: 0x6000132
        public static System.Void SetAudioObjectName(System.UInt64 audioObjId, System.String name) { }
        // RVA: 0x0635E1AC  token: 0x6000133
        public static System.Void RemoveAudioObjectName(System.UInt64 audioObjId) { }
        // RVA: 0x0635E104  token: 0x6000134
        public static System.String GetAudioObjectName(System.UInt64 audioObjId) { }
        // RVA: 0x0635E05C  token: 0x6000135
        public static System.String ForceGetEventName(System.UInt32 eventId) { }
        // RVA: 0x0635E0B0  token: 0x6000136
        public static System.String ForceGetRtpcName(System.UInt32 rtpcId) { }
        // RVA: 0x0635E174  token: 0x6000137
        public static System.String GetRtpcName(System.UInt32 rtpcId) { }
        // RVA: 0x0635E13C  token: 0x6000138
        public static System.String GetEventName(System.UInt32 eventId) { }

    }

    // TypeToken: 0x2000040  // size: 0x10
    public static class AudioHashGenerator
    {
        // Fields
        public static System.Int16 MAX_STRING_LENGTH;  // const
        private static System.UInt32 PRIME32;  // const
        private static System.UInt32 OFFSET_BASIS32;  // const

        // Methods
        // RVA: 0x0328DCD0  token: 0x6000139
        public static System.UInt32 Compute(System.String inStr) { }
        // RVA: 0x0328E280  token: 0x600013A
        public static System.UInt32 Compute(System.IntPtr inCharPtr) { }
        // RVA: 0x0328DD60  token: 0x600013B
        public static System.UInt32 Compute(System.ReadOnlySpan<System.Byte> buffer) { }
        // RVA: 0x03ABCC30  token: 0x600013C
        public static System.UInt32 Compute(System.String inStr1, System.String inStr2) { }
        // RVA: 0x0635E2E8  token: 0x600013D
        public static System.UInt32 Compute(System.String[] strings) { }
        // RVA: 0x0635E3D0  token: 0x600013E
        private static System.UInt32 _GetUintFromCharToLower(System.Char c) { }

    }

    // TypeToken: 0x2000041  // size: 0x10
    public static class AudioMemoryMonitor
    {
        // Fields
        private static AkMemCategroyStats s_categoryStats;  // static @ 0x0
        private static System.Boolean s_monitorInited;  // static @ 0x8

        // Methods
        // RVA: 0x04B44E60  token: 0x600013F
        private static System.Void Init() { }
        // RVA: 0x0635EE64  token: 0x6000140
        private static System.Void Release() { }
        // RVA: 0x0635EE2C  token: 0x6000141
        private static System.Boolean IsValid() { }
        // RVA: 0x0635ED8C  token: 0x6000142
        private static System.UInt64 GetUsedMemoryUnsafe() { }
        // RVA: 0x0635EDCC  token: 0x6000143
        private static System.UInt64 GetUsedMemory() { }
        // RVA: 0x04819930  token: 0x6000144
        public static Beyond.Audio.AudioMemoryMonitor.FGlobalMemoryStats GetGlobalMemoryStats() { }
        // RVA: 0x0635EC60  token: 0x6000145
        public static Beyond.Audio.AudioMemoryMonitor.FExtraMemoryStats GetExtraMemoryStats() { }
        // RVA: 0x0635ECC4  token: 0x6000146
        private static System.UInt64 GetUsedMemoryByCategory(AkMemID memCategory) { }

    }

    // TypeToken: 0x2000044  // size: 0x10
    public static class AudioUIUtil
    {
        // Fields
        private static System.UInt16 s_fullScreenCounter;  // static @ 0x0

        // Methods
        // RVA: 0x03EB4AA0  token: 0x6000147
        public static System.Void Init() { }
        // RVA: 0x0635F25C  token: 0x6000148
        public static System.Void Release() { }
        // RVA: 0x037989E0  token: 0x6000149
        public static System.UInt32 PostUIEvent(System.String eventName, UnityEngine.GameObject uiGameObject) { }
        // RVA: 0x0635F1D4  token: 0x600014A
        public static System.UInt32 PostUIEvent(System.String eventName, System.String objPath) { }
        // RVA: 0x038D4A60  token: 0x600014B
        public static System.Void OnUIPanelClose(Beyond.Audio.AudioUIUtil.UIAudioType uiAudioType) { }
        // RVA: 0x038D49E0  token: 0x600014C
        public static System.Void OnUIPanelOpen(Beyond.Audio.AudioUIUtil.UIAudioType uiAudioType) { }

    }

    // TypeToken: 0x2000046  // size: 0x10
    public static class AudioVFSLoader
    {
        // Fields
        private static System.String PCK_SUFFIX;  // const
        private static Beyond.Audio.AudioVFSLoader.VfsLoadedInfo s_loadedInitPckInfo;  // static @ 0x0
        private static Beyond.Audio.AudioVFSLoader.VfsLoadedInfo s_loadedMainPckInfo;  // static @ 0x10
        private static Beyond.Audio.AudioVFSLoader.VfsLoadedInfo s_loadedAuditPckInfo;  // static @ 0x20
        private static Beyond.Audio.AudioVFSLoader.VfsLoadedInfo s_loadedLangPckInfo;  // static @ 0x30
        private static Beyond.Audio.AudioVFSLoader.VfsLoadedInfo s_loadedHotfixPckInfo;  // static @ 0x40
        private static Beyond.VFS.EVFSBlockType s_pendingLanguageBlock;  // static @ 0x50
        private static readonly Beyond.VFS.FVFBlockChunkInfo[] s_emptyChunks;  // static @ 0x58

        // Methods
        // RVA: 0x0635F608  token: 0x600014D
        private static System.Void UnloadAll() { }
        // RVA: 0x03EB4910  token: 0x600014E
        private static System.Boolean TryLoadInitPck() { }
        // RVA: 0x03EB5FE0  token: 0x600014F
        private static System.Boolean TryLoadMainPck() { }
        // RVA: 0x03EB45A0  token: 0x6000150
        private static System.Boolean TryLoadLanguagePck(System.String language) { }
        // RVA: 0x03EB4EA0  token: 0x6000151
        private static System.Boolean _DoLoadLanguageAndHotfixPck(Beyond.VFS.EVFSBlockType langBlockType) { }
        // RVA: 0x046536A0  token: 0x6000152
        private static System.Void InitBasePaths() { }
        // RVA: 0x03EB5020  token: 0x6000153
        private static System.Boolean _TryLoadPcksFromVfsInfo(Beyond.VFS.VFBlockMainInfo vfsInfo, Beyond.Audio.AudioVFSLoader.VfsLoadedInfo& info) { }
        // RVA: 0x03EB6CB0  token: 0x6000154
        private static System.Collections.Generic.IReadOnlyList<Beyond.VFS.FVFBlockChunkInfo> _SplitChunksByTag(Beyond.VFS.VFBlockMainInfo block, Beyond.VFS.EVFSFileTag tag, System.Boolean includeMatching) { }
        // RVA: 0x03EB6E60  token: 0x6000155
        private static System.Boolean _TryLoadPcksFromChunks(System.Collections.Generic.IReadOnlyList<Beyond.VFS.FVFBlockChunkInfo> chunks, System.Int32 version, Beyond.Audio.AudioVFSLoader.VfsLoadedInfo& info) { }
        // RVA: 0x03EB54B0  token: 0x6000156
        private static System.Boolean _DoLoadPcksFromChunks(System.Collections.Generic.IReadOnlyList<Beyond.VFS.FVFBlockChunkInfo> chunks, System.Collections.Generic.List<System.UInt32>& loaded) { }
        // RVA: 0x03EB5100  token: 0x6000157
        private static System.Boolean _DoLoadPcksFromVfs(Beyond.VFS.VFBlockMainInfo vfs, System.Collections.Generic.List<System.UInt32>& loaded) { }
        // RVA: 0x03EB5A20  token: 0x6000158
        private static System.Boolean _DoLoadSinglePckFromVfs(Beyond.VFS.FVFBlockFileInfo fileInfo, System.UInt32& loadedId) { }
        // RVA: 0x03EB5D80  token: 0x6000159
        private static System.Void _UnloadPcks(Beyond.Audio.AudioVFSLoader.VfsLoadedInfo& info) { }
        // RVA: 0x03EB4690  token: 0x600015A
        private static Beyond.VFS.EVFSBlockType _GetVfsBlockTypeFromLanguage(System.String language) { }
        // RVA: 0x0635F304  token: 0x600015B
        public static System.Void LoadExtraPckFromPath(System.String path) { }
        // RVA: 0x04D30020  token: 0x600015C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x10
    public class AudioVoiceUtil
    {
        // Methods
        // RVA: 0x03ABBC00  token: 0x600015E
        public static System.Boolean TryGetVoiceData(System.String voiceStrId, Beyond.Cfg.VoiceData& voData, System.Int32& voiceId, System.String genderSuffix) { }
        // RVA: 0x03AC1660  token: 0x600015F
        public static System.Boolean GetVoDataFromVoId(System.Int32 voId, Beyond.Cfg.VoiceData& voData) { }
        // RVA: 0x03ABBC90  token: 0x6000160
        public static System.Int32 GetVoiceIdFromString(System.String strId) { }
        // RVA: 0x03ABCBD0  token: 0x6000161
        public static System.Int32 GetVoiceIdFromStrings(System.String str1, System.String str2) { }
        // RVA: 0x03ABBD20  token: 0x6000162
        private static System.Void _HandleVoiceIdConflict(System.Int32& voiceIntId, System.String voiceStrId) { }
        // RVA: 0x03ABCD60  token: 0x6000163
        private static System.Void _HandleVoiceIdConflict(System.Int32& voiceIntId, System.String voiceStrId1, System.String voiceStrId2) { }
        // RVA: 0x041E1670  token: 0x6000164
        public System.Void .ctor() { }

    }

}

namespace Beyond.Audio.AudioPerformanceMonitor
{

    // TypeToken: 0x200004A  // size: 0x10
    public class AudioProfiling
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000167
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004C  // size: 0x58
    public class WwiseResourceMonitor : Beyond.Singleton`1
    {
        // Fields
        private static System.Single TIME_OF_SOUND_FRAME;  // const
        private static System.Single MB_RATE;  // const
        private System.Boolean m_isActiveAkResourceMonitoring;  // 0x10
        private Beyond.Audio.AudioPerformanceMonitor.WwiseResourceMonitorItem m_wwiseResourceMonitorData;  // 0x14

        // Properties
        System.Boolean isActiveAkResourceMonitoring { get; /* RVA: 0x015EFCE0 */ }

        // Methods
        // RVA: 0x06365D38  token: 0x600016A
        private System.Void .ctor() { }
        // RVA: 0x02FFF600  token: 0x600016B
        public static System.Boolean IsSupportMemoryMonitor() { }
        // RVA: 0x06365D20  token: 0x600016C
        public System.Void StartSoundEngineResourceMonitor() { }
        // RVA: 0x06365D2C  token: 0x600016D
        public System.Void StopSoundEngineResourceMonitor() { }
        // RVA: 0x04D86180  token: 0x600016E
        public Beyond.Audio.AudioPerformanceMonitor.WwiseResourceMonitorItem SampleRawWwiseResource() { }

    }

    // TypeToken: 0x200004D  // size: 0x50
    public sealed struct WwiseResourceMonitorItem
    {
        // Fields
        public System.Single pluginCpu;  // 0x10
        public System.Single cpu;  // 0x14
        public System.Single pluginCpuInMs;  // 0x18
        public System.Single cpuInMs;  // 0x1c
        public System.Single reservedMemory;  // 0x20
        public System.Single usedMemory;  // 0x24
        public System.Single allocatedMemory;  // 0x28
        public System.Single mappedMemory;  // 0x2c
        public System.Single activeMemory;  // 0x30
        public System.Single retainedMemory;  // 0x34
        public System.Int32 eventNum;  // 0x38
        public System.Int32 totalVoiceNum;  // 0x3c
        public System.Int32 physicalVoiceNum;  // 0x40
        public System.Int32 virtualVoiceNum;  // 0x44
        public System.Int32 eventCallbackCount;  // 0x48
        public System.Int32 bankCallbackCount;  // 0x4c

    }

}

namespace Beyond.Audio.Editor
{

    // TypeToken: 0x2000049  // size: 0x10
    public static class AudioEditorAssetsLoader
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000165
        private static System.Void Init() { }
        // RVA: 0x041E1670  token: 0x6000166
        private static System.Void Release() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000003  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000003
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000005
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000002  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000050  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x600016F
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0635FE78  token: 0x6000170
        public System.UInt64 __Gen_Wrap_0() { }
        // RVA: 0x06361650  token: 0x6000171
        public System.UInt32 __Gen_Wrap_1(System.UInt32 P0, System.UInt64 P1, System.Object P2, System.UInt32 P3, Beyond.Audio.AudioCallbackType P4, System.Object P5, System.Object P6, Beyond.Audio.AudioCodec P7) { }
        // RVA: 0x06362070  token: 0x6000172
        public System.UInt32 __Gen_Wrap_2(System.Object P0, System.UInt64 P1, System.Object P2, System.UInt32 P3, Beyond.Audio.AudioCallbackType P4, System.Object P5, System.Object P6, Beyond.Audio.AudioCodec P7) { }
        // RVA: 0x06362940  token: 0x6000173
        public System.Void __Gen_Wrap_3(System.UInt32 P0, System.IntPtr P1, AKRESULT P2, System.Object P3) { }
        // RVA: 0x063633B4  token: 0x6000174
        public System.Void __Gen_Wrap_4(System.Object P0) { }
        // RVA: 0x06363568  token: 0x6000175
        public System.Collections.IEnumerator __Gen_Wrap_5(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x063637FC  token: 0x6000176
        public System.Void __Gen_Wrap_6(System.Collections.Generic.IList<System.UInt32>& P0, System.Action<System.Object>& P1, System.Object& P2) { }
        // RVA: 0x06364290  token: 0x6000177
        public System.Void __Gen_Wrap_7(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x06364DE4  token: 0x6000178
        public System.UInt32 __Gen_Wrap_8(Beyond.Audio.AudioId P0) { }
        // RVA: 0x0636591C  token: 0x6000179
        public System.Collections.IEnumerator __Gen_Wrap_9(System.Object P0) { }
        // RVA: 0x063607B4  token: 0x600017A
        public System.Void __Gen_Wrap_10(System.UInt32 P0, System.UInt32 P1, System.UInt64 P2) { }
        // RVA: 0x06360DA0  token: 0x600017B
        public System.Void __Gen_Wrap_11(System.Object P0, System.Object P1, System.UInt64 P2) { }
        // RVA: 0x06360E84  token: 0x600017C
        public System.UInt32 __Gen_Wrap_12(System.Object P0) { }
        // RVA: 0x06360F54  token: 0x600017D
        public System.Void __Gen_Wrap_13(System.UInt32 P0, System.Single P1, System.Int32 P2) { }
        // RVA: 0x0636104C  token: 0x600017E
        public System.Void __Gen_Wrap_14(System.Object P0, System.Single P1, System.Int32 P2) { }
        // RVA: 0x06361144  token: 0x600017F
        public System.Void __Gen_Wrap_15(System.UInt32 P0, System.Single P1, System.UInt64 P2) { }
        // RVA: 0x0636123C  token: 0x6000180
        public System.Void __Gen_Wrap_16(System.Object P0, System.Single P1, System.UInt64 P2) { }
        // RVA: 0x06361338  token: 0x6000181
        public System.Void __Gen_Wrap_17(System.UInt32 P0, System.Single P1, System.UInt64 P2, System.Int32 P3) { }
        // RVA: 0x06361440  token: 0x6000182
        public System.Void __Gen_Wrap_18(System.UInt32 P0, System.UInt32 P1, System.Single P2, System.Int32 P3) { }
        // RVA: 0x06361548  token: 0x6000183
        public System.Void __Gen_Wrap_19(System.UInt32 P0, System.Single P1, System.UInt32 P2, System.Int32 P3) { }
        // RVA: 0x063617A4  token: 0x6000184
        public System.Void __Gen_Wrap_20(System.UInt32 P0, System.UInt64 P1, System.Int32 P2) { }
        // RVA: 0x06361884  token: 0x6000185
        public System.Void __Gen_Wrap_21(System.Object P0, System.UInt64 P1, System.Int32 P2) { }
        // RVA: 0x06361968  token: 0x6000186
        public System.Void __Gen_Wrap_22(System.UInt32 P0, System.UInt32 P1, System.UInt64 P2, System.Int32 P3, System.Boolean P4) { }
        // RVA: 0x06361A7C  token: 0x6000187
        public System.Void __Gen_Wrap_23(System.UInt32 P0, System.UInt64 P1, System.Int32 P2, System.Boolean P3, System.UInt32 P4) { }
        // RVA: 0x06361B90  token: 0x6000188
        public System.Void __Gen_Wrap_24(System.Object P0, System.UInt64 P1, System.Int32 P2, System.Boolean P3, System.UInt32 P4) { }
        // RVA: 0x06361CA4  token: 0x6000189
        public System.Void __Gen_Wrap_25(System.Object P0, System.Int32 P1, System.Boolean P2, System.UInt32 P3) { }
        // RVA: 0x06361D9C  token: 0x600018A
        public System.Void __Gen_Wrap_26(System.UInt32 P0, System.Int32 P1, System.Boolean P2, System.UInt32 P3) { }
        // RVA: 0x06361E90  token: 0x600018B
        public System.Boolean __Gen_Wrap_27(System.UInt32 P0, System.Int32& P1, System.Boolean P2) { }
        // RVA: 0x06361FA4  token: 0x600018C
        public System.Boolean __Gen_Wrap_28(System.UInt32 P0) { }
        // RVA: 0x06360E84  token: 0x600018D
        public System.Int32 __Gen_Wrap_29(System.Object P0) { }
        // RVA: 0x063621C4  token: 0x600018E
        public System.Int32 __Gen_Wrap_30() { }
        // RVA: 0x06362270  token: 0x600018F
        public System.UInt32 __Gen_Wrap_31(System.Object P0, System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>& P1) { }
        // RVA: 0x063621C4  token: 0x6000190
        public System.UInt32 __Gen_Wrap_32() { }
        // RVA: 0x063623A8  token: 0x6000191
        public System.Boolean __Gen_Wrap_33() { }
        // RVA: 0x06362454  token: 0x6000192
        public System.Boolean __Gen_Wrap_34(System.Object P0, System.UInt32 P1, System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>& P2) { }
        // RVA: 0x063625A4  token: 0x6000193
        public System.Void __Gen_Wrap_35() { }
        // RVA: 0x06362640  token: 0x6000194
        public System.Void __Gen_Wrap_36(System.UInt32 P0, Beyond.Audio.AudioAssetHelper.FOnBankLoadedDoPrepareEventPayload P1) { }
        // RVA: 0x0635FF24  token: 0x6000195
        public System.Void __Gen_Wrap_37(System.Int32 P0) { }
        // RVA: 0x06362758  token: 0x6000196
        public AkPositionArray __Gen_Wrap_38(System.Object P0) { }
        // RVA: 0x06362848  token: 0x6000197
        public System.Void __Gen_Wrap_39(System.UInt64 P0, System.Object P1, Beyond.Audio.AudioLargeType P2) { }
        // RVA: 0x06362A48  token: 0x6000198
        public System.Void __Gen_Wrap_40(System.UInt64 P0) { }
        // RVA: 0x06362B08  token: 0x6000199
        public System.Void __Gen_Wrap_41(System.UInt64 P0, System.UInt64 P1) { }
        // RVA: 0x06362BE4  token: 0x600019A
        public System.Boolean __Gen_Wrap_42(System.UInt32 P0, System.UInt32& P1) { }
        // RVA: 0x06360E84  token: 0x600019B
        public Beyond.VFS.EVFSBlockType __Gen_Wrap_43(System.Object P0) { }
        // RVA: 0x06362CF0  token: 0x600019C
        public System.Void __Gen_Wrap_44(Beyond.Audio.AudioVFSLoader.VfsLoadedInfo& P0) { }
        // RVA: 0x06362E2C  token: 0x600019D
        public System.Boolean __Gen_Wrap_45(Beyond.VFS.FVFBlockFileInfo P0, System.UInt32& P1) { }
        // RVA: 0x06362F74  token: 0x600019E
        public System.Boolean __Gen_Wrap_46(System.Object P0, System.Collections.Generic.List<System.UInt32>& P1) { }
        // RVA: 0x063630AC  token: 0x600019F
        public System.Boolean __Gen_Wrap_47(System.Object P0, Beyond.Audio.AudioVFSLoader.VfsLoadedInfo& P1) { }
        // RVA: 0x06363214  token: 0x60001A0
        public System.Boolean __Gen_Wrap_48(Beyond.VFS.EVFSBlockType P0) { }
        // RVA: 0x063632E4  token: 0x60001A1
        public System.Boolean __Gen_Wrap_49(System.Object P0) { }
        // RVA: 0x06363474  token: 0x60001A2
        public System.Void __Gen_Wrap_50(Beyond.Audio.AudioEngineInitSetting P0) { }
        // RVA: 0x011C6280  token: 0x60001A3
        public System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo> __Gen_Wrap_51() { }
        // RVA: 0x011C6AC0  token: 0x60001A4
        public Beyond.Audio.AudioVFSLoader.VfsLoadedInfo __Gen_Wrap_52() { }
        // RVA: 0x011C72D0  token: 0x60001A5
        public System.Void __Gen_Wrap_53(System.Collections.Generic.List<System.UInt32>& P0) { }
        // RVA: 0x011C7C80  token: 0x60001A6
        public System.Void __Gen_Wrap_54(System.Boolean P0) { }
        // RVA: 0x011C7E70  token: 0x60001A7
        public System.Void __Gen_Wrap_55(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7, System.Object P8) { }
        // RVA: 0x011C8780  token: 0x60001A8
        public System.Void __Gen_Wrap_56(System.Object P0, AkCallbackType P1, System.Object P2) { }
        // RVA: 0x011C8B50  token: 0x60001A9
        public System.UInt32 __Gen_Wrap_57(System.UInt32 P0, System.UInt64 P1, Beyond.Audio.AudioCallbackType P2, System.Object P3, System.Object P4) { }
        // RVA: 0x011C8F80  token: 0x60001AA
        public System.Void __Gen_Wrap_58(System.UInt32 P0, AkActionOnEventType P1, System.Int32 P2) { }
        // RVA: 0x011C8F80  token: 0x60001AB
        public System.Void __Gen_Wrap_59(AkActionOnEventType P0, System.UInt32 P1, System.Int32 P2) { }
        // RVA: 0x011C91D0  token: 0x60001AC
        public AkResourceMonitorDataSummary __Gen_Wrap_60() { }
        // RVA: 0x011C9A10  token: 0x60001AD
        public Beyond.Audio.AudioMemoryMonitor.FGlobalMemoryStats __Gen_Wrap_61() { }
        // RVA: 0x011CA220  token: 0x60001AE
        public Beyond.Audio.AudioMemoryMonitor.FExtraMemoryStats __Gen_Wrap_62() { }
        // RVA: 0x011CAA30  token: 0x60001AF
        public System.Void __Gen_Wrap_63(System.UInt64 P0, System.Single P1, System.Single P2) { }
        // RVA: 0x011CAC60  token: 0x60001B0
        public System.Boolean __Gen_Wrap_64(System.UInt64 P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Boolean P5, System.Boolean P6) { }
        // RVA: 0x011CB1F0  token: 0x60001B1
        public System.Boolean __Gen_Wrap_65(System.UInt64 P0, UnityEngine.Vector3 P1, UnityEngine.Vector3 P2, UnityEngine.Quaternion P3, System.UInt64 P4, System.Boolean P5) { }
        // RVA: 0x011CBC60  token: 0x60001B2
        public System.Boolean __Gen_Wrap_66(System.UInt64 P0, Beyond.Audio.AudioRoomData P1, System.UInt64 P2, System.Object P3) { }
        // RVA: 0x011CC360  token: 0x60001B3
        public System.Boolean __Gen_Wrap_67(System.UInt64 P0, System.UInt64 P1, System.UInt64 P2, UnityEngine.Vector3 P3, UnityEngine.Quaternion P4, UnityEngine.Vector3 P5, System.Boolean P6, System.Object P7) { }
        // RVA: 0x011CCEC0  token: 0x60001B4
        public Beyond.Audio.AudioAdapter.PayloadPostEvent __Gen_Wrap_68() { }
        // RVA: 0x06363690  token: 0x60001B5
        public System.Boolean __Gen_Wrap_69(Beyond.Audio.AudioChannelMask& P0, System.UInt32& P1, Beyond.Audio.AudioChannelConfigType& P2) { }
        // RVA: 0x06363928  token: 0x60001B6
        public System.Boolean __Gen_Wrap_70(System.Object P0, System.UInt32 P1, System.UInt32 P2, System.UInt32 P3, System.UInt64& P4) { }
        // RVA: 0x06363A60  token: 0x60001B7
        public System.Boolean __Gen_Wrap_71(System.Object P0, System.UInt32 P1, System.UInt64& P2) { }
        // RVA: 0x06363B88  token: 0x60001B8
        public System.UInt64 __Gen_Wrap_72(System.Object P0, System.UInt32 P1) { }
        // RVA: 0x06363C60  token: 0x60001B9
        public System.Boolean __Gen_Wrap_73(System.UInt64 P0) { }
        // RVA: 0x06363D30  token: 0x60001BA
        public System.Boolean __Gen_Wrap_74(System.Boolean P0) { }
        // RVA: 0x06363E00  token: 0x60001BB
        public System.Void __Gen_Wrap_75(System.UInt64 P0, System.UInt32 P1, System.UInt32 P2) { }
        // RVA: 0x06363EE4  token: 0x60001BC
        public System.UInt32 __Gen_Wrap_76(System.Int32 P0) { }
        // RVA: 0x06363F9C  token: 0x60001BD
        public Beyond.Audio.AsyncLoadBankTask __Gen_Wrap_77(System.Object P0) { }
        // RVA: 0x0636408C  token: 0x60001BE
        public System.Object __Gen_Wrap_78(System.Object P0) { }
        // RVA: 0x06364168  token: 0x60001BF
        public System.Collections.Generic.IReadOnlyList<Beyond.VFS.FVFBlockChunkInfo> __Gen_Wrap_79(System.Object P0, Beyond.VFS.EVFSFileTag P1, System.Boolean P2) { }
        // RVA: 0x06364388  token: 0x60001C0
        public System.Boolean __Gen_Wrap_80(System.Object P0, System.Int32 P1, Beyond.Audio.AudioVFSLoader.VfsLoadedInfo& P2) { }
        // RVA: 0x06364508  token: 0x60001C1
        public Beyond.Audio.BankHandle __Gen_Wrap_81(System.Object P0, System.Object P1) { }
        // RVA: 0x06364614  token: 0x60001C2
        public System.Void __Gen_Wrap_82(System.Collections.Generic.List<System.UInt32>& P0) { }
        // RVA: 0x0636470C  token: 0x60001C3
        public System.String __Gen_Wrap_83(Beyond.Audio.AudioId& P0) { }
        // RVA: 0x06364840  token: 0x60001C4
        public System.Int32 __Gen_Wrap_84(Beyond.Audio.AudioId P0) { }
        // RVA: 0x06364940  token: 0x60001C5
        public Beyond.Audio.AudioId __Gen_Wrap_85(System.UInt32 P0) { }
        // RVA: 0x06364A2C  token: 0x60001C6
        public Beyond.Audio.AudioId __Gen_Wrap_86(System.Int32 P0) { }
        // RVA: 0x06364B04  token: 0x60001C7
        public Beyond.Audio.AudioId __Gen_Wrap_87(System.Object P0) { }
        // RVA: 0x06364BE0  token: 0x60001C8
        public System.UInt32[] __Gen_Wrap_88(System.Object P0) { }
        // RVA: 0x06364CBC  token: 0x60001C9
        public System.UInt32 __Gen_Wrap_89(Beyond.Audio.AudioState& P0) { }
        // RVA: 0x06364EE4  token: 0x60001CA
        public System.Boolean __Gen_Wrap_90(Beyond.Audio.AudioState& P0, Beyond.Audio.AudioState P1) { }
        // RVA: 0x0636503C  token: 0x60001CB
        public System.Boolean __Gen_Wrap_91(Beyond.Audio.AudioState& P0, System.Object P1) { }
        // RVA: 0x06365180  token: 0x60001CC
        public System.Int32 __Gen_Wrap_92(Beyond.Audio.AudioState& P0) { }
        // RVA: 0x063652A8  token: 0x60001CD
        public Beyond.Audio.AudioDurationCallbackInfo __Gen_Wrap_93(Beyond.Audio.AudioCallbackInfo& P0) { }
        // RVA: 0x06365428  token: 0x60001CE
        public System.Void __Gen_Wrap_94(System.Object P0, UnityEngine.Vector3 P1, UnityEngine.Vector3 P2, UnityEngine.Vector3 P3) { }
        // RVA: 0x063655AC  token: 0x60001CF
        public System.Boolean __Gen_Wrap_95(Beyond.Audio.AudioLogChannel P0) { }
        // RVA: 0x06301ED4  token: 0x60001D0
        public System.Void __Gen_Wrap_96(Beyond.Audio.AudioLogChannel P0) { }
        // RVA: 0x06365664  token: 0x60001D1
        public System.Void __Gen_Wrap_97(Beyond.Audio.AudioLogChannel P0, System.Object P1) { }
        // RVA: 0x0636572C  token: 0x60001D2
        public System.Single __Gen_Wrap_98(System.Single P0, System.Single P1, System.Single P2) { }
        // RVA: 0x06365824  token: 0x60001D3
        public System.Single __Gen_Wrap_99(UnityEngine.Vector3 P0) { }
        // RVA: 0x0635FF24  token: 0x60001D4
        public System.Void __Gen_Wrap_100(System.UInt32 P0) { }
        // RVA: 0x0635FFE4  token: 0x60001D5
        public System.Void __Gen_Wrap_101(System.Object P0, System.Collections.Generic.LinkedListNode<Beyond.Audio.AudioAssetCache.EventCacheInfo>& P1) { }
        // RVA: 0x0636010C  token: 0x60001D6
        public System.Collections.Generic.LinkedList<Beyond.Audio.AudioAssetCache.EventCacheInfo> __Gen_Wrap_102(System.Object P0) { }
        // RVA: 0x063601E8  token: 0x60001D7
        public System.Void __Gen_Wrap_103(System.UInt64 P0, System.UInt16 P1) { }
        // RVA: 0x063602C4  token: 0x60001D8
        public System.Void __Gen_Wrap_104(System.UInt64 P0, System.Object P1) { }
        // RVA: 0x0636038C  token: 0x60001D9
        public System.String __Gen_Wrap_105(System.UInt32 P0) { }
        // RVA: 0x06360464  token: 0x60001DA
        public System.UInt32 __Gen_Wrap_106(System.ReadOnlySpan<System.Byte> P0) { }
        // RVA: 0x06360554  token: 0x60001DB
        public System.UInt32 __Gen_Wrap_107(System.IntPtr P0) { }
        // RVA: 0x06360610  token: 0x60001DC
        public System.UInt32 __Gen_Wrap_108(System.Object P0, System.Object P1) { }
        // RVA: 0x063606FC  token: 0x60001DD
        public System.UInt64 __Gen_Wrap_109(AkMemID P0) { }
        // RVA: 0x06301ED4  token: 0x60001DE
        public System.Void __Gen_Wrap_110(Beyond.Audio.AudioUIUtil.UIAudioType P0) { }
        // RVA: 0x063608AC  token: 0x60001DF
        public System.Void __Gen_Wrap_111(System.Int32& P0, System.Object P1) { }
        // RVA: 0x063609AC  token: 0x60001E0
        public System.Boolean __Gen_Wrap_112(System.Int32 P0, Beyond.Cfg.VoiceData& P1) { }
        // RVA: 0x06360AF4  token: 0x60001E1
        public System.Void __Gen_Wrap_113(System.Int32& P0, System.Object P1, System.Object P2) { }
        // RVA: 0x06360610  token: 0x60001E2
        public System.Int32 __Gen_Wrap_114(System.Object P0, System.Object P1) { }
        // RVA: 0x06360C0C  token: 0x60001E3
        public System.Boolean __Gen_Wrap_115(System.Object P0, Beyond.Cfg.VoiceData& P1, System.Int32& P2, System.Object P3) { }
        // RVA: 0x04D37170  token: 0x60001E4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x58
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey, System.IDisposable, System.Collections.IEnumerator, System.Collections.Generic.IEnumerator`1
    {
        // Fields
        private System.Int32 methodId_0;  // 0x40
        private System.Int32 methodId_1;  // 0x44
        private System.Int32 methodId_2;  // 0x48
        private System.Int32 methodId_3;  // 0x4c
        private System.Int32 methodId_4;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x06365DEC */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x06365F14 */ }

        // Methods
        // RVA: 0x06365FC0  token: 0x60001E5
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06365D60  token: 0x60001E6
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06365E98  token: 0x60001E8
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0636603C  token: 0x60001EA
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x60001EB
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x2000052  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60001EC
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x06366300  token: 0x60001ED
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0328C4E0  token: 0x60001EE
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x063661BC  token: 0x60001EF
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x06366288  token: 0x60001F0
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x06366364  token: 0x60001F1
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x06366118  token: 0x60001F2
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000053  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-get_listenerObjId0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_PostEventWithExternalSource0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-PostEventExternal0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-PostEventExternal1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_DoLoadAndPinEventsSync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadAndPinEventsSync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadAndPinEventsSync1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_DoLoadAndPinEventsAsync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_LoadAndPinEventsAsync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadAndPinEventsAsync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioId-op_Implicit0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadAndPinEventsAsync1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadAndPinEventsAsync2;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadAndPinEventsCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadAndPinEventsCoroutine1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetSwitch0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetSwitch1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioHashGenerator-Compute0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetRtpc0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetRtpc1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetRtpc2;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetRtpc3;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetRtpc4;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-QueueRtpcAction0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetRtpcValueByPlayingID0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-ResetRtpc0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-ResetRtpc1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-QueueSeekAction0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SeekOnEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SeekOnEvent1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SeekOnEvent2;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SeekOnEvent3;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-TryGetPlayingPosition0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-TryGetMusicPlayingPosition0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-IsPlaying0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-Cache-get_count0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-GetCachedAssetCount0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-Cache-RemoveFirst0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-GetLeastActiveAssetAndUncache0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-get_isGcProcessing0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-Cache-Remove0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-ActivateAsset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_TryDequeueAndInvokeCallback0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_DoLoadEventAsync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_ConsumePendingLoadRequests0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_TryConsumePendingLoadRequests0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_DoGc0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-ReleaseAllCachedEventsSync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-ReleaseAllCachedEventsSync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioPositionArrayProxy-GetRawArray0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioPositionArrayProxy-get_count0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetLargePositions0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetDefaultListener0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetListener0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-TryGetRealPlayingId0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetDefaultEmitter0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_GetVfsBlockTypeFromLanguage0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_UnloadPcks0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_DoLoadSinglePckFromVfs0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_DoLoadPcksFromVfs0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_TryLoadPcksFromVfsInfo0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_DoLoadLanguageAndHotfixPck0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-TryLoadLanguagePck0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetAudioLanguage0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetDistanceProbe0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetAudioEngineInitSetting0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-UnsetAudioEngineInitSetting0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Context-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-Cache-Clear0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-ResetStaticFields0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_ResetStaticFields0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_InitAndroidActivityEnvironment0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioCallbackMessageParser-Set0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioMemoryMonitor-Init0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Context-PostSoundEngineInit0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-VfsLoadedInfo-Create0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-InitBasePaths0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_InitInternal0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-TryLoadInitPck0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadInitResources0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-InitWwise0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-ClearQueue0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-HasPreparingEvents0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_OnBeforeTerminate0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-GetAllUsingAssetsAndRelease0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-UnPrepareAllEventsSync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioMemoryMonitor-Release0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_ReleaseInternal0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-TermWwise0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-UnloadAll0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-ReloadInitResources0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-MemoryShrink0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-TouchAudioHotfixPlaceholderArgs0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-AudioHotfixPlaceholder10;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-AudioHotfixPlaceholder20;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-AudioHotfixPlaceholder30;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-TouchAudioHotfixPlaceholder0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-LoadExtraPckFromPath0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-LoadExtraPckFromPath0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_OnEventPreparedDoPostEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_OnEventCallback0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_OnExternalSourceEventCallback0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_PostEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-QueueExecuteAction0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_ExecuteActionOnPlayingId0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_SampleAllData0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-_SampleMemoryOnly0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-StartProfileCapture0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-StopProfileCapture0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-GetResourceSummaryData0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-GetGlobalMemoryStats0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-GetExtraMemoryStats0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetEnableSpatialAudio0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetObjectObstructionAndOcclusion0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetGeometry0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetGeometryInstance0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetRoom0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-SetPortal0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Context-_CreatePayloadPostEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-PayloadPostEvent-OnRecycle0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-GetSpeakerConfiguration0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-AddOutputDevice0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-AddOutput0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-GetOutputDeviceId0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-RemoveOutputDevice0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-Switch3DOutput0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-SetOutputDeviceEffect0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAdapter-Device-GetMmDeviceIdFromScePadHandle0;  // const
        public static IFix.IDMAP0 Beyond-Audio-BankHandle-get_refCount0;  // const
        public static IFix.IDMAP0 Beyond-Audio-BankHandle-DecRef0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-UnloadBank0;  // const
        public static IFix.IDMAP0 Beyond-Audio-BankHandle-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Audio-BankHandle-IncRef0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AsyncLoadBankTask-LoadBanksAsync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AsyncLoadBankTask-MoveNext0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AsyncLoadBankTask-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AsyncLoadBankTask-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AsyncLoadBankTask-get_Current0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_SplitChunksByTag0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_DoLoadPcksFromChunks0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-_TryLoadPcksFromChunks0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVFSLoader-TryLoadMainPck0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-LoadMainPCK0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-LoadBankAsync0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-_LoadBankCallback0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-UnloadAllBanks0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-IsBankLoaded0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-_PreResetBasePath0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-_PostResetBasePath0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-LoadDebugFilePackage0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioBankManager-AddDebugBankPath0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioId-GetName0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioId-op_Implicit1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioId-op_Implicit2;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioId-op_Implicit3;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioId-op_Implicit4;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioIdExtensions-ToUIntArray0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioState-get_stateGroup0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioState-get_stateValue0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioState-Equals0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioState-Equals1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioState-GetHashCode0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioCallbackInfo-CastToDurationCallbackInfo0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioPositionArrayProxy-Add0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioPositionArrayProxy-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-IsChannelEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-EnableChannel0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-DisableChannel0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-SetChannels0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-_DoLog0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-Log0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-_DoLogWarning0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-LogWarning0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-_DoLogError0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioLogger-LogError0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAcousticUtil-RT60DecayTime0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAcousticUtil-PreDelayTime0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAcousticUtil-CriticalDistance0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAcousticUtil-ReverbIntensity0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAcousticUtil-ErIntensity0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-_ConsumeExecute0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-_ConsumeRtpc0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-_ConsumeSeek0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioActionQueueHelper-ConsumeQueue0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-PinEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-UnpinEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-Cache-AddLast0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-DeactivateAsset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-ForceReleaseCachedAsset0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-_NotifyDebugger0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-_NotifyDebuggerCacheHit0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-_ResetDebugger0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-_PinnedEventLeakDetect0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetCache-Cache-get_lruCachedEventsList0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-SetMemoryBudget0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_OnUnpreparedDoUnloadBank0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_OnDoneUnloadBank0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_OnDonePrepareEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_OnBankLoadedDoPrepareEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_RemoveLoadingDebug0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_AddLoadingDebug0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioAssetHelper-_ClearLoadingDebug0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioDebugInfoContainer-SetAudioObjectName0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioDebugInfoContainer-RemoveAudioObjectName0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioDebugInfoContainer-ForceGetEventName0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioDebugInfoContainer-ForceGetRtpcName0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioHashGenerator-Compute1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioHashGenerator-Compute2;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioHashGenerator-Compute3;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioHashGenerator-Compute4;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioMemoryMonitor-GetGlobalMemoryStats0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioMemoryMonitor-GetExtraMemoryStats0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioMemoryMonitor-GetUsedMemoryByCategory0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioUIUtil-Init0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioUIUtil-Release0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioUIUtil-PostUIEvent0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioUIUtil-PostUIEvent1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioUIUtil-OnUIPanelClose0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioUIUtil-OnUIPanelOpen0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVoiceUtil-_HandleVoiceIdConflict0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVoiceUtil-GetVoiceIdFromString0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVoiceUtil-GetVoDataFromVoId0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVoiceUtil-_HandleVoiceIdConflict1;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVoiceUtil-GetVoiceIdFromStrings0;  // const
        public static IFix.IDMAP0 Beyond-Audio-AudioVoiceUtil-TryGetVoiceData0;  // const

    }

}

