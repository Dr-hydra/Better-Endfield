// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AnimationModule.dll
// Classes:  106
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000F  // size: 0x20
    public sealed class Enumerator : System.Collections.IEnumerator
    {
        // Fields
        private UnityEngine.Animation m_Outer;  // 0x10
        private System.Int32 m_CurrentIndex;  // 0x18

        // Properties
        System.Object Current { get; /* RVA: 0x0400B4A0 */ }

        // Methods
        // RVA: 0x032ED9A0  token: 0x6000051
        private System.Void .ctor(UnityEngine.Animation outer) { }
        // RVA: 0x03F2F310  token: 0x6000053
        public virtual System.Boolean MoveNext() { }
        // RVA: 0x04D87910  token: 0x6000054
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x200002B  // size: 0x80
    public sealed class AnimationEventCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03B19A60  token: 0x60002B6
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0622AD60  token: 0x60002B7
        public virtual System.Boolean Invoke(UnityEngine.Animator animator, UnityEngine.FastAnimationEvent fastAnimationEvent) { }

    }

    // TypeToken: 0x200002C  // size: 0x80
    public sealed class AnimatorStateChangeCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0408A900  token: 0x60002B8
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B3999A8  token: 0x60002B9
        public virtual System.Void Invoke(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo animatorStateInfo, System.Int32 layerIndex) { }

    }

    // TypeToken: 0x200002D  // size: 0x80
    public sealed class AnimationEventCallbackOpt : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0378DD20  token: 0x60002BA
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B395A90  token: 0x60002BB
        public virtual System.Boolean Invoke(UnityEngine.Animator animator, UnityEngine.FastAnimationEventGroup fastAnimationEvent) { }

    }

    // TypeToken: 0x200002E  // size: 0x80
    public sealed class AniamtorCommonCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x074FF394  token: 0x60002BC
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60002BD
        public virtual System.Void Invoke(UnityEngine.Animator animator) { }

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct RetargetBoneMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Animator.RetargetBoneMode Translation;  // const
        public static UnityEngine.Animator.RetargetBoneMode Scale;  // const

    }

    // TypeToken: 0x2000033  // size: 0x80
    public sealed class OnOverrideControllerDirtyCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x60002E0
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x60002E1
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x2000052
    public sealed class ExecuteJobFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000337
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000338
        public virtual System.Void Invoke(T& data, System.IntPtr animationStreamPtr, System.IntPtr unusedPtr, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }

    }

namespace Microsoft.CodeAnalysis
{

    // TypeToken: 0x2000002  // size: 0x10
    public sealed class EmbeddedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class IsReadOnlyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

namespace UnityEngine
{

    // TypeToken: 0x2000004
    public interface IAnimationClipSource
    {
    }

    // TypeToken: 0x2000005  // size: 0x10
    public sealed class SharedBetweenAnimatorsAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000003
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x18
    public abstract class StateMachineBehaviour : UnityEngine.ScriptableObject
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public virtual System.Void OnStateEnter(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex) { }
        // RVA: 0x041E1670  token: 0x6000005
        public virtual System.Void OnStateUpdate(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex) { }
        // RVA: 0x041E1670  token: 0x6000006
        public virtual System.Void OnStateExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex) { }
        // RVA: 0x041E1670  token: 0x6000007
        public virtual System.Void OnStateMove(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex) { }
        // RVA: 0x041E1670  token: 0x6000008
        public virtual System.Void OnStateIK(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex) { }
        // RVA: 0x041E1670  token: 0x6000009
        public virtual System.Void OnStatePreExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex) { }
        // RVA: 0x041E1670  token: 0x600000A
        public virtual System.Void OnStateMachineEnter(UnityEngine.Animator animator, System.Int32 stateMachinePathHash) { }
        // RVA: 0x041E1670  token: 0x600000B
        public virtual System.Void OnStateMachineExit(UnityEngine.Animator animator, System.Int32 stateMachinePathHash) { }
        // RVA: 0x041E1670  token: 0x600000C
        public virtual System.Void OnStateEnter(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x041E1670  token: 0x600000D
        public virtual System.Void OnStateUpdate(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x041E1670  token: 0x600000E
        public virtual System.Void OnStateExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x041E1670  token: 0x600000F
        public virtual System.Void OnStateMove(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x041E1670  token: 0x6000010
        public virtual System.Void OnStateIK(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x041E1670  token: 0x6000011
        public virtual System.Void OnStatePreExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x041E1670  token: 0x6000012
        public virtual System.Void OnStateMachineEnter(UnityEngine.Animator animator, System.Int32 stateMachinePathHash, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x041E1670  token: 0x6000013
        public virtual System.Void OnStateMachineExit(UnityEngine.Animator animator, System.Int32 stateMachinePathHash, UnityEngine.Animations.AnimatorControllerPlayable controller) { }
        // RVA: 0x03573370  token: 0x6000014
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct PlayMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.PlayMode StopSameLayer;  // const
        public static UnityEngine.PlayMode StopAll;  // const

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct QueueMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.QueueMode CompleteOthers;  // const
        public static UnityEngine.QueueMode PlayNow;  // const

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct AnimationBlendMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimationBlendMode Blend;  // const
        public static UnityEngine.AnimationBlendMode Additive;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct AnimationPlayMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimationPlayMode Stop;  // const
        public static UnityEngine.AnimationPlayMode Queue;  // const
        public static UnityEngine.AnimationPlayMode Mix;  // const

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct AnimationCullingType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimationCullingType AlwaysAnimate;  // const
        public static UnityEngine.AnimationCullingType BasedOnRenderers;  // const
        public static UnityEngine.AnimationCullingType BasedOnClipBounds;  // const
        public static UnityEngine.AnimationCullingType BasedOnUserBounds;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct AnimationEventSource
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimationEventSource NoSource;  // const
        public static UnityEngine.AnimationEventSource Legacy;  // const
        public static UnityEngine.AnimationEventSource Animator;  // const

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct AnimEventHashCodeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimEventHashCodeType None;  // const
        public static UnityEngine.AnimEventHashCodeType CRC32;  // const
        public static UnityEngine.AnimEventHashCodeType HashCode64;  // const

    }

    // TypeToken: 0x200000E  // size: 0x18
    public sealed class Animation : UnityEngine.Behaviour, System.Collections.IEnumerable
    {
        // Properties
        UnityEngine.AnimationClip clip { get; /* RVA: 0x0B3982AC */ set; /* RVA: 0x0B3984AC */ }
        System.Boolean playAutomatically { get; /* RVA: 0x0B3983BC */ set; /* RVA: 0x032E9F60 */ }
        UnityEngine.WrapMode wrapMode { get; /* RVA: 0x0B3983F0 */ set; /* RVA: 0x0B398588 */ }
        System.Boolean isPlaying { get; /* RVA: 0x0B398314 */ }
        UnityEngine.AnimationState Item { get; /* RVA: 0x0B398238 */ }
        System.Boolean animatePhysics { get; /* RVA: 0x0B398278 */ set; /* RVA: 0x0B398468 */ }
        System.Boolean animateOnlyIfVisible { get; /* RVA: 0x0B398244 */ set; /* RVA: 0x0B398424 */ }
        UnityEngine.AnimationCullingType cullingType { get; /* RVA: 0x0B3982E0 */ set; /* RVA: 0x0B3984F0 */ }
        UnityEngine.Bounds localBounds { get; /* RVA: 0x0B39838C */ set; /* RVA: 0x0B398574 */ }

        // Methods
        // RVA: 0x0378E160  token: 0x600001B
        public System.Void Stop() { }
        // RVA: 0x0B3981F0  token: 0x600001C
        public System.Void Stop(System.String name) { }
        // RVA: 0x0B3981AC  token: 0x600001D
        private System.Void StopNamed(System.String name) { }
        // RVA: 0x0B39813C  token: 0x600001E
        public System.Void Rewind() { }
        // RVA: 0x0B398170  token: 0x600001F
        public System.Void Rewind(System.String name) { }
        // RVA: 0x0B3980F8  token: 0x6000020
        private System.Void RewindNamed(System.String name) { }
        // RVA: 0x0B398178  token: 0x6000021
        public System.Void Sample() { }
        // RVA: 0x0B397F88  token: 0x6000023
        public System.Boolean IsPlaying(System.String name) { }
        // RVA: 0x045C64C0  token: 0x6000025
        public System.Boolean Play() { }
        // RVA: 0x05397994  token: 0x6000026
        public System.Boolean Play(UnityEngine.PlayMode mode) { }
        // RVA: 0x04CD8EA0  token: 0x6000027
        private System.Boolean PlayDefaultAnimation(UnityEngine.PlayMode mode) { }
        // RVA: 0x053979A0  token: 0x6000028
        public System.Boolean Play(System.String animation) { }
        // RVA: 0x0378E100  token: 0x6000029
        public System.Boolean Play(System.String animation, UnityEngine.PlayMode mode) { }
        // RVA: 0x0B397F44  token: 0x600002A
        public System.Void CrossFade(System.String animation) { }
        // RVA: 0x0B397EC4  token: 0x600002B
        public System.Void CrossFade(System.String animation, System.Single fadeLength) { }
        // RVA: 0x0B397EE0  token: 0x600002C
        public System.Void CrossFade(System.String animation, System.Single fadeLength, UnityEngine.PlayMode mode) { }
        // RVA: 0x0B397D80  token: 0x600002D
        public System.Void Blend(System.String animation) { }
        // RVA: 0x0B397DF4  token: 0x600002E
        public System.Void Blend(System.String animation, System.Single targetWeight) { }
        // RVA: 0x0B397D90  token: 0x600002F
        public System.Void Blend(System.String animation, System.Single targetWeight, System.Single fadeLength) { }
        // RVA: 0x0B397E4C  token: 0x6000030
        public UnityEngine.AnimationState CrossFadeQueued(System.String animation) { }
        // RVA: 0x0B397E14  token: 0x6000031
        public UnityEngine.AnimationState CrossFadeQueued(System.String animation, System.Single fadeLength) { }
        // RVA: 0x0B397E30  token: 0x6000032
        public UnityEngine.AnimationState CrossFadeQueued(System.String animation, System.Single fadeLength, UnityEngine.QueueMode queue) { }
        // RVA: 0x0B397E60  token: 0x6000033
        public UnityEngine.AnimationState CrossFadeQueued(System.String animation, System.Single fadeLength, UnityEngine.QueueMode queue, UnityEngine.PlayMode mode) { }
        // RVA: 0x0B397FCC  token: 0x6000034
        public UnityEngine.AnimationState PlayQueued(System.String animation) { }
        // RVA: 0x0B398040  token: 0x6000035
        public UnityEngine.AnimationState PlayQueued(System.String animation, UnityEngine.QueueMode queue) { }
        // RVA: 0x0B397FDC  token: 0x6000036
        public UnityEngine.AnimationState PlayQueued(System.String animation, UnityEngine.QueueMode queue, UnityEngine.PlayMode mode) { }
        // RVA: 0x0B397CF4  token: 0x6000037
        public System.Void AddClip(UnityEngine.AnimationClip clip, System.String newName) { }
        // RVA: 0x0B397CD0  token: 0x6000038
        public System.Void AddClip(UnityEngine.AnimationClip clip, System.String newName, System.Int32 firstFrame, System.Int32 lastFrame) { }
        // RVA: 0x0B397D1C  token: 0x6000039
        public System.Void AddClip(UnityEngine.AnimationClip clip, System.String newName, System.Int32 firstFrame, System.Int32 lastFrame, System.Boolean addLoopFrame) { }
        // RVA: 0x0B3980B4  token: 0x600003A
        public System.Void RemoveClip(UnityEngine.AnimationClip clip) { }
        // RVA: 0x0B3980AC  token: 0x600003B
        public System.Void RemoveClip(System.String clipName) { }
        // RVA: 0x0B398068  token: 0x600003C
        private System.Void RemoveClipNamed(System.String clipName) { }
        // RVA: 0x0B397F54  token: 0x600003D
        public System.Int32 GetClipCount() { }
        // RVA: 0x05397994  token: 0x600003E
        public System.Boolean Play(UnityEngine.AnimationPlayMode mode) { }
        // RVA: 0x0B39805C  token: 0x600003F
        public System.Boolean Play(System.String animation, UnityEngine.AnimationPlayMode mode) { }
        // RVA: 0x0B3981F8  token: 0x6000040
        public System.Void SyncLayer(System.Int32 layer) { }
        // RVA: 0x032ED960  token: 0x6000041
        public virtual System.Collections.IEnumerator GetEnumerator() { }
        // RVA: 0x0378E1A0  token: 0x6000042
        private UnityEngine.AnimationState GetState(System.String name) { }
        // RVA: 0x0400B500  token: 0x6000043
        private UnityEngine.AnimationState GetStateAtIndex(System.Int32 index) { }
        // RVA: 0x03F2F380  token: 0x6000044
        private System.Int32 GetStateCount() { }
        // RVA: 0x0378E1F0  token: 0x6000045
        public UnityEngine.AnimationClip GetClip(System.String name) { }
        // RVA: 0x05393520  token: 0x600004E
        public System.Void .ctor() { }
        // RVA: 0x0B398348  token: 0x600004F
        private System.Void get_localBounds_Injected(UnityEngine.Bounds& ret) { }
        // RVA: 0x0B398530  token: 0x6000050
        private System.Void set_localBounds_Injected(UnityEngine.Bounds& value) { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed class AnimationState : UnityEngine.TrackedReference
    {
        // Properties
        System.Boolean enabled { get; /* RVA: 0x0B3975F0 */ set; /* RVA: 0x0B39779C */ }
        System.Single weight { get; /* RVA: 0x0B397728 */ set; /* RVA: 0x0B397934 */ }
        UnityEngine.WrapMode wrapMode { get; /* RVA: 0x032EE490 */ set; /* RVA: 0x0B397978 */ }
        System.Single time { get; /* RVA: 0x0B3976F4 */ set; /* RVA: 0x0B3978F0 */ }
        System.Single normalizedTime { get; /* RVA: 0x0B39768C */ set; /* RVA: 0x0B397868 */ }
        System.Single speed { get; /* RVA: 0x0B3976C0 */ set; /* RVA: 0x0B3978AC */ }
        System.Single normalizedSpeed { get; /* RVA: 0x0B397658 */ set; /* RVA: 0x0B397824 */ }
        System.Single length { get; /* RVA: 0x032EB770 */ }
        System.Int32 layer { get; /* RVA: 0x0B397624 */ set; /* RVA: 0x032ED3A0 */ }
        UnityEngine.AnimationClip clip { get; /* RVA: 0x032ECBE0 */ }
        System.String name { get; /* RVA: 0x032ED360 */ set; /* RVA: 0x0B3977E0 */ }
        UnityEngine.AnimationBlendMode blendMode { get; /* RVA: 0x0B3975BC */ set; /* RVA: 0x0B39775C */ }

        // Methods
        // RVA: 0x0B397518  token: 0x600006B
        public System.Void AddMixingTransform(UnityEngine.Transform mix) { }
        // RVA: 0x0B397524  token: 0x600006C
        public System.Void AddMixingTransform(UnityEngine.Transform mix, System.Boolean recursive) { }
        // RVA: 0x0B397578  token: 0x600006D
        public System.Void RemoveMixingTransform(UnityEngine.Transform mix) { }
        // RVA: 0x041E1670  token: 0x600006E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x48
    public sealed struct FastAnimationEvent
    {
        // Fields
        private System.Single m_Time;  // 0x10
        private System.Single m_ClipWeight;  // 0x14
        private System.String m_FunctionName;  // 0x18
        private System.String m_StringParameter;  // 0x20
        private UnityEngine.Object m_ObjectReferenceParameter;  // 0x28
        private System.Single m_FloatParameter;  // 0x30
        private System.Int32 m_IntParameter;  // 0x34
        private UnityEngine.SendMessageOptions m_MessageOptions;  // 0x38
        private System.Int32 m_StringParameterHashCode;  // 0x3c
        private System.Int32 m_FunctionNameHashCode;  // 0x40
        private System.Boolean m_IsInTransition;  // 0x44
        private System.Boolean m_IsFromMaxWeightClipState;  // 0x45
        private System.Boolean m_IsFromNextMaxWeightClipState;  // 0x46

        // Properties
        System.String stringParameter { get; /* RVA: 0x04D9B600 */ set; /* RVA: 0x053908C0 */ }
        System.Single floatParameter { get; /* RVA: 0x04DBA960 */ set; /* RVA: 0x011EC9E0 */ }
        System.Int32 intParameter { get; /* RVA: 0x04DBA990 */ set; /* RVA: 0x011F0030 */ }
        System.String functionName { get; /* RVA: 0x04DBA980 */ }
        System.Single time { get; /* RVA: 0x04DBA9D0 */ set; /* RVA: 0x04D932C0 */ }
        UnityEngine.SendMessageOptions messageOptions { set; /* RVA: 0x011F2AA0 */ }
        System.Single clipWeight { get; /* RVA: 0x04DBA740 */ set; /* RVA: 0x04DAB920 */ }
        System.Boolean isInTransition { get; /* RVA: 0x04DBA9C0 */ set; /* RVA: 0x04D86AA0 */ }
        System.Boolean isFromMaxWeightClipState { get; /* RVA: 0x04DBA9A0 */ set; /* RVA: 0x04D86D40 */ }
        System.Boolean isFromNextMaxWeightClipState { get; /* RVA: 0x04DBA9B0 */ set; /* RVA: 0x04DBA9E0 */ }
        System.Int32 functionNameHashCode { get; /* RVA: 0x04DBA970 */ set; /* RVA: 0x04D86740 */ }

    }

    // TypeToken: 0x2000012  // size: 0x78
    public sealed struct FastAnimationEventGroup
    {
        // Fields
        public System.Int32 count;  // 0x10
        public System.IntPtr times;  // 0x18
        public System.IntPtr clipWeights;  // 0x20
        public System.IntPtr functionNames;  // 0x28
        public System.IntPtr stringParameters;  // 0x30
        public System.IntPtr functionNamesHashCode;  // 0x38
        public System.IntPtr stringParametersHashCode;  // 0x40
        public System.IntPtr floatParameters;  // 0x48
        public System.IntPtr intParameters;  // 0x50
        public System.IntPtr messageOptions;  // 0x58
        public System.IntPtr inTransitions;  // 0x60
        public System.IntPtr isFromMaxWeightClipStatus;  // 0x68
        public System.IntPtr isFromNextMaxWeightClipStatus;  // 0x70

    }

    // TypeToken: 0x2000013  // size: 0x90
    public sealed class AnimationEvent
    {
        // Fields
        private System.Single m_Time;  // 0x10
        private System.String m_FunctionName;  // 0x18
        private System.String m_StringParameter;  // 0x20
        private UnityEngine.Object m_ObjectReferenceParameter;  // 0x28
        private System.Single m_FloatParameter;  // 0x30
        private System.Int32 m_IntParameter;  // 0x34
        private System.Int32 m_MessageOptions;  // 0x38
        private UnityEngine.AnimationEventSource m_Source;  // 0x3c
        private UnityEngine.AnimationState m_StateSender;  // 0x40
        private UnityEngine.AnimatorStateInfo m_AnimatorStateInfo;  // 0x48
        private UnityEngine.AnimatorClipInfo m_AnimatorClipInfo;  // 0x7c
        private System.Int32 m_FunctionNameHashCode;  // 0x84
        private System.Int32 m_StringParameterHashCode;  // 0x88
        private UnityEngine.AnimEventHashCodeType m_HashCodeType;  // 0x8c

        // Properties
        System.String stringParameter { get; /* RVA: 0x04DBA910 */ set; /* RVA: 0x05390F40 */ }
        System.Single floatParameter { get; /* RVA: 0x04DB9A40 */ set; /* RVA: 0x04D86E30 */ }
        System.Int32 intParameter { get; /* RVA: 0x04DBA8F0 */ set; /* RVA: 0x04D86760 */ }
        UnityEngine.Object objectReferenceParameter { set; /* RVA: 0x02FDB880 */ }
        System.String functionName { get; /* RVA: 0x04D96330 */ set; /* RVA: 0x05392C40 */ }
        System.Single time { get; /* RVA: 0x04DBA720 */ set; /* RVA: 0x04D88280 */ }
        UnityEngine.SendMessageOptions messageOptions { get; /* RVA: 0x04DBA900 */ set; /* RVA: 0x04D86430 */ }
        System.Boolean isFiredByAnimator { get; /* RVA: 0x0B395BF4 */ }
        UnityEngine.AnimatorClipInfo animatorClipInfo { get; /* RVA: 0x0B395B98 */ }

        // Methods
        // RVA: 0x0B395AFC  token: 0x6000083
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x20
    public sealed class AnimationClip : UnityEngine.Motion
    {
        // Properties
        System.Single length { get; /* RVA: 0x0343B670 */ }
        System.Single startTime { get; /* RVA: 0x0B395810 */ }
        System.Single stopTime { get; /* RVA: 0x0B395844 */ }
        System.Single frameRate { get; /* RVA: 0x0B395698 */ set; /* RVA: 0x0B39592C */ }
        System.UInt16 clipTag { get; /* RVA: 0x0B395620 */ set; /* RVA: 0x0B3958E0 */ }
        UnityEngine.WrapMode wrapMode { get; /* RVA: 0x032ECCE0 */ set; /* RVA: 0x0B395A50 */ }
        UnityEngine.Bounds localBounds { get; /* RVA: 0x0B3957E0 */ set; /* RVA: 0x0B3959F8 */ }
        System.Boolean legacy { get; /* RVA: 0x04D770C0 */ set; /* RVA: 0x0B395970 */ }
        System.Boolean humanMotion { get; /* RVA: 0x0B395768 */ }
        System.Boolean empty { get; /* RVA: 0x039F6FC0 */ }
        System.Boolean hasGenericRootTransform { get; /* RVA: 0x03FA5D90 */ }
        System.Boolean hasMotionFloatCurves { get; /* RVA: 0x0B395700 */ }
        System.Boolean hasMotionCurves { get; /* RVA: 0x0B3956CC */ }
        System.Boolean hasRootCurves { get; /* RVA: 0x0B395734 */ }
        System.Boolean hasRootMotion { get; /* RVA: 0x03FA5D50 */ }
        System.Boolean transferRotationCompressed { get; /* RVA: 0x0B395878 */ set; /* RVA: 0x0B395A0C */ }
        System.UInt64 uniqueId { get; /* RVA: 0x0B3958AC */ }
        UnityEngine.AnimationEvent[] events { get; /* RVA: 0x0B395654 */ set; /* RVA: 0x0B395924 */ }

        // Methods
        // RVA: 0x0386FF40  token: 0x6000093
        public System.Void .ctor() { }
        // RVA: 0x0386FF70  token: 0x6000094
        private static System.Void Internal_CreateAnimationClip(UnityEngine.AnimationClip self) { }
        // RVA: 0x032ECC40  token: 0x6000095
        public System.Void SampleAnimation(UnityEngine.GameObject go, System.Single time) { }
        // RVA: 0x03435540  token: 0x6000096
        public System.Void SampleAnimationFast(UnityEngine.GameObject go, System.Single time) { }
        // RVA: 0x032ECD20  token: 0x6000097
        private static System.Void SampleAnimation(UnityEngine.GameObject go, UnityEngine.AnimationClip clip, System.Single inTime, UnityEngine.WrapMode wrapMode) { }
        // RVA: 0x034354D0  token: 0x6000098
        private static System.Void SampleAnimationFast(UnityEngine.GameObject go, UnityEngine.AnimationClip clip, System.Single inTime, UnityEngine.WrapMode wrapMode) { }
        // RVA: 0x0B395578  token: 0x60000A0
        public System.Void SetCurve(System.String relativePath, System.Type type, System.String propertyName, UnityEngine.AnimationCurve curve) { }
        // RVA: 0x0B3954BC  token: 0x60000A1
        public System.Boolean GetAnimatorFloatCurve(System.String propertyName, UnityEngine.AnimationCurve curve) { }
        // RVA: 0x0B395488  token: 0x60000A2
        public System.Void EnsureQuaternionContinuity() { }
        // RVA: 0x0B395454  token: 0x60000A3
        public System.Void ClearCurves() { }
        // RVA: 0x0B395544  token: 0x60000A4
        public System.UInt32 GetRuntimeMemorySize() { }
        // RVA: 0x0B3953E8  token: 0x60000B5
        public System.Void AddEvent(UnityEngine.AnimationEvent evt) { }
        // RVA: 0x0B3953A4  token: 0x60000B6
        private System.Void AddEventInternal(System.Object evt) { }
        // RVA: 0x0B3955DC  token: 0x60000B9
        private System.Void SetEventsInternal(System.Array value) { }
        // RVA: 0x0B395510  token: 0x60000BA
        private System.Array GetEventsInternal() { }
        // RVA: 0x0B39579C  token: 0x60000BB
        private System.Void get_localBounds_Injected(UnityEngine.Bounds& ret) { }
        // RVA: 0x0B3959B4  token: 0x60000BC
        private System.Void set_localBounds_Injected(UnityEngine.Bounds& value) { }

    }

    // TypeToken: 0x2000015  // size: 0x14
    public sealed struct AvatarTarget
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AvatarTarget Root;  // const
        public static UnityEngine.AvatarTarget Body;  // const
        public static UnityEngine.AvatarTarget LeftFoot;  // const
        public static UnityEngine.AvatarTarget RightFoot;  // const
        public static UnityEngine.AvatarTarget LeftHand;  // const
        public static UnityEngine.AvatarTarget RightHand;  // const

    }

    // TypeToken: 0x2000016  // size: 0x14
    public sealed struct AvatarIKGoal
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AvatarIKGoal LeftFoot;  // const
        public static UnityEngine.AvatarIKGoal RightFoot;  // const
        public static UnityEngine.AvatarIKGoal LeftHand;  // const
        public static UnityEngine.AvatarIKGoal RightHand;  // const

    }

    // TypeToken: 0x2000017  // size: 0x14
    public sealed struct AvatarIKHint
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AvatarIKHint LeftKnee;  // const
        public static UnityEngine.AvatarIKHint RightKnee;  // const
        public static UnityEngine.AvatarIKHint LeftElbow;  // const
        public static UnityEngine.AvatarIKHint RightElbow;  // const

    }

    // TypeToken: 0x2000018  // size: 0x14
    public sealed struct AnimatorControllerParameterType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimatorControllerParameterType Float;  // const
        public static UnityEngine.AnimatorControllerParameterType Int;  // const
        public static UnityEngine.AnimatorControllerParameterType Bool;  // const
        public static UnityEngine.AnimatorControllerParameterType Trigger;  // const

    }

    // TypeToken: 0x2000019  // size: 0x14
    public sealed struct StateInfoIndex
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.StateInfoIndex CurrentState;  // const
        public static UnityEngine.StateInfoIndex NextState;  // const
        public static UnityEngine.StateInfoIndex ExitState;  // const
        public static UnityEngine.StateInfoIndex InterruptedState;  // const

    }

    // TypeToken: 0x200001A  // size: 0x14
    public sealed struct AnimationBlendStyle
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimationBlendStyle Linear;  // const
        public static UnityEngine.AnimationBlendStyle CubicIn;  // const
        public static UnityEngine.AnimationBlendStyle HermiteCubic;  // const
        public static UnityEngine.AnimationBlendStyle Sinusoidal;  // const
        public static UnityEngine.AnimationBlendStyle CubicInOut;  // const

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct AnimatorTransitionInterruptionSource
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimatorTransitionInterruptionSource None;  // const
        public static UnityEngine.AnimatorTransitionInterruptionSource Current;  // const
        public static UnityEngine.AnimatorTransitionInterruptionSource Next;  // const
        public static UnityEngine.AnimatorTransitionInterruptionSource CurrentThenNext;  // const
        public static UnityEngine.AnimatorTransitionInterruptionSource NextThenCurrent;  // const

    }

    // TypeToken: 0x200001C  // size: 0x14
    public sealed struct AnimatorRecorderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimatorRecorderMode Offline;  // const
        public static UnityEngine.AnimatorRecorderMode Playback;  // const
        public static UnityEngine.AnimatorRecorderMode Record;  // const

    }

    // TypeToken: 0x200001D  // size: 0x14
    public sealed struct AnimatorUroMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimatorUroMode Origin;  // const
        public static UnityEngine.AnimatorUroMode Predict;  // const
        public static UnityEngine.AnimatorUroMode PredictWithTick;  // const
        public static UnityEngine.AnimatorUroMode PredictWithTickAndRootMotion;  // const

    }

    // TypeToken: 0x200001E  // size: 0x14
    public sealed struct AnimatorCullingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimatorCullingMode AlwaysAnimate;  // const
        public static UnityEngine.AnimatorCullingMode CullUpdateTransforms;  // const
        public static UnityEngine.AnimatorCullingMode CullCompletely;  // const

    }

    // TypeToken: 0x200001F  // size: 0x14
    public sealed struct AnimatorUpdateMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimatorUpdateMode Normal;  // const
        public static UnityEngine.AnimatorUpdateMode AnimatePhysics;  // const
        public static UnityEngine.AnimatorUpdateMode UnscaledTime;  // const

    }

    // TypeToken: 0x2000020  // size: 0x14
    public sealed struct AnimatorJobDispatchMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimatorJobDispatchMode Default;  // const
        public static UnityEngine.AnimatorJobDispatchMode ForceMainThread;  // const
        public static UnityEngine.AnimatorJobDispatchMode PreUpdate;  // const
        public static UnityEngine.AnimatorJobDispatchMode CrossFrame;  // const
        public static UnityEngine.AnimatorJobDispatchMode CrossFrameLateUpdate;  // const

    }

    // TypeToken: 0x2000021  // size: 0x2C
    public sealed struct AnimatorLOD
    {
        // Fields
        public System.Single worldAbsoluteDistance;  // 0x10
        public System.Single maxDeltaTime;  // 0x14
        public System.Int32 framesPerUpdate;  // 0x18
        public System.Single maxBlendDeltaTime;  // 0x1c
        public System.Int32 framesPerWrite;  // 0x20
        public System.Int32 delayFrame;  // 0x24
        public UnityEngine.AnimatorUroMode uroMode;  // 0x28

        // Methods
        // RVA: 0x04DBA920  token: 0x60000BD
        public System.Void .ctor(System.Single worldAbsoluteDistance, System.Int32 framesPerUpdate, System.Single maxDeltaTime, System.Int32 framesPerWrite, System.Single maxBlendDeltaTime, UnityEngine.AnimatorUroMode uroMode, System.Int32 delayFrame) { }

    }

    // TypeToken: 0x2000022  // size: 0x80
    public sealed struct AnimationTransformRWBufferHandle
    {
        // Fields
        public System.UInt16 count;  // 0x10
        public System.UInt16 invalidCount;  // 0x12
        public System.IntPtr validTransformIndexsPtr;  // 0x18
        public System.IntPtr invalidTransformIndexsPtr;  // 0x20
        public System.IntPtr localPositionsPtr;  // 0x28
        public System.IntPtr localRotationsPtr;  // 0x30
        public System.IntPtr localScalesPtr;  // 0x38
        public System.IntPtr positionsPtr;  // 0x40
        public System.IntPtr rotationsPtr;  // 0x48
        public System.IntPtr scalesPtr;  // 0x50
        public System.IntPtr physicsRatioPtr;  // 0x58
        public System.IntPtr writeFlagPtr;  // 0x60
        public System.IntPtr relativePostionPtr;  // 0x68
        public System.IntPtr relativeRotationPtr;  // 0x70
        public System.IntPtr relativeScalePtr;  // 0x78

    }

    // TypeToken: 0x2000023  // size: 0x58
    public sealed struct HGAnimParameterSGHandle
    {
        // Fields
        public System.Int32 errorCode;  // 0x10
        public System.UInt16 floatParameterCount;  // 0x14
        public System.UInt16 boolParameterCount;  // 0x16
        public System.UInt16 intParameterCount;  // 0x18
        public System.UInt16 layerCount;  // 0x1a
        public System.IntPtr floatParameterNamesPtr;  // 0x20
        public System.IntPtr floatParameterValuesPtr;  // 0x28
        public System.IntPtr boolParameterNamesPtr;  // 0x30
        public System.IntPtr boolParameterValuesPtr;  // 0x38
        public System.IntPtr intParameterNamesPtr;  // 0x40
        public System.IntPtr intParameterValuesPtr;  // 0x48
        public System.IntPtr layerWeightPtr;  // 0x50

    }

    // TypeToken: 0x2000024  // size: 0x14
    public sealed struct AnimFrameSyncMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AnimFrameSyncMode FrameSyncInRootPlayable;  // const
        public static UnityEngine.AnimFrameSyncMode FrameSyncInBaseLayerState;  // const

    }

    // TypeToken: 0x2000025  // size: 0x18
    public sealed struct AnimatorClipInfo
    {
        // Fields
        private System.Int32 m_ClipInstanceID;  // 0x10
        private System.Single m_Weight;  // 0x14

        // Properties
        UnityEngine.AnimationClip clip { get; /* RVA: 0x0B3985FC */ }
        System.Single weight { get; /* RVA: 0x04DBA740 */ }

        // Methods
        // RVA: 0x0B3985C8  token: 0x60000C0
        private static UnityEngine.AnimationClip InstanceIDToAnimationClipPPtr(System.Int32 instanceID) { }

    }

    // TypeToken: 0x2000026  // size: 0x44
    public sealed struct AnimatorStateInfo
    {
        // Fields
        private System.Int32 m_Name;  // 0x10
        private System.Int32 m_Path;  // 0x14
        private System.Int32 m_FullPath;  // 0x18
        private System.Single m_NormalizedTime;  // 0x1c
        private System.Single m_Length;  // 0x20
        private System.Single m_Speed;  // 0x24
        private System.Single m_SpeedMultiplier;  // 0x28
        private System.Int32 m_Tag;  // 0x2c
        private System.Int32 m_Loop;  // 0x30
        private System.Int32 m_SyncGroup;  // 0x34
        private System.Int32 m_SyncGroupRole;  // 0x38
        private System.Int32 m_CullingMode;  // 0x3c
        private System.Int32 m_SourceMsg;  // 0x40

        // Properties
        System.Int32 fullPathHash { get; /* RVA: 0x04D9CF30 */ }
        System.Int32 shortNameHash { get; /* RVA: 0x04DBA750 */ }
        System.Single normalizedTime { get; /* RVA: 0x04DBA950 */ }
        System.Single length { get; /* RVA: 0x04DBA720 */ }
        System.Int32 tagHash { get; /* RVA: 0x04D9B610 */ }
        System.Boolean loop { get; /* RVA: 0x0343C970 */ }

        // Methods
        // RVA: 0x03F7F9F0  token: 0x60000C1
        public System.Boolean IsName(System.String name) { }
        // RVA: 0x03B071C0  token: 0x60000C7
        public System.Boolean IsTag(System.String tag) { }

    }

    // TypeToken: 0x2000027  // size: 0x7C
    public sealed struct AnimatorStateRootMotionInfo
    {
        // Fields
        private System.Int32 m_NameHashLeft;  // 0x10
        private UnityEngine.Vector3 m_DeltaPositionLeft;  // 0x14
        private UnityEngine.Quaternion m_DeltaRotationLeft;  // 0x20
        private System.Int32 m_NameHashRight;  // 0x30
        private UnityEngine.Vector3 m_DeltaPositionRight;  // 0x34
        private UnityEngine.Quaternion m_DeltaRotationRight;  // 0x40
        private System.Single m_TransitionWeight;  // 0x50
        private System.Single m_DeltaTime;  // 0x54
        private System.Single m_InterruptedTransitionWeight;  // 0x58
        private System.Int32 m_NameHashinterrupted;  // 0x5c
        private UnityEngine.Vector3 m_DeltaPositioninterrupted;  // 0x60
        private UnityEngine.Quaternion m_DeltaRotationinterrupted;  // 0x6c

    }

    // TypeToken: 0x2000028  // size: 0x30
    public sealed struct AnimatorTransitionInfo
    {
        // Fields
        private System.Int32 m_FullPath;  // 0x10
        private System.Int32 m_UserName;  // 0x14
        private System.Int32 m_Name;  // 0x18
        private System.Boolean m_HasFixedDuration;  // 0x1c
        private System.Single m_Duration;  // 0x20
        private System.Single m_NormalizedTime;  // 0x24
        private System.Boolean m_AnyState;  // 0x28
        private System.Int32 m_TransitionType;  // 0x2c

        // Properties
        System.Int32 nameHash { get; /* RVA: 0x04D9CF30 */ }

        // Methods
        // RVA: 0x0B3999F0  token: 0x60000C9
        public System.Boolean IsUserName(System.String name) { }

    }

    // TypeToken: 0x2000029  // size: 0x20
    public sealed struct MatchTargetWeightMask
    {
        // Fields
        private UnityEngine.Vector3 m_PositionXYZWeight;  // 0x10
        private System.Single m_RotationWeight;  // 0x1c

        // Methods
        // RVA: 0x04D8C1B0  token: 0x60000CB
        public System.Void .ctor(UnityEngine.Vector3 positionXYZWeight, System.Single rotationWeight) { }

    }

    // TypeToken: 0x200002A  // size: 0x48
    public class Animator : UnityEngine.Behaviour
    {
        // Fields
        private UnityEngine.Animator.AnimationEventCallback onAnimationEvent;  // 0x18
        private UnityEngine.Animator.AnimationEventCallbackOpt onAnimationEventOpt;  // 0x20
        private UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStateEnter;  // 0x28
        private UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStateExit;  // 0x30
        private UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStatePreExit;  // 0x38
        private UnityEngine.Animator.AniamtorCommonCallback afterAnimatorUpdateGraphCB;  // 0x40

        // Properties
        System.Boolean fastAnimationEventsOpt { get; /* RVA: 0x0B39E1E8 */ set; /* RVA: 0x0378DC80 */ }
        System.Boolean callBackAfterUpdateGraph { get; /* RVA: 0x0B39E14C */ set; /* RVA: 0x0B39ECDC */ }
        System.Boolean fastAnimationEvents { get; /* RVA: 0x0B39E21C */ set; /* RVA: 0x0378DCD0 */ }
        System.Boolean notifyAnimatorStateEnterAndExitEvent { get; /* RVA: 0x0B39E604 */ set; /* RVA: 0x0408AA50 */ }
        System.Boolean keepStateOnChangeActive { get; /* RVA: 0x0B39E430 */ set; /* RVA: 0x0B39EDF4 */ }
        System.Boolean pausePlayableOnChangeActive { get; /* RVA: 0x0B39E638 */ set; /* RVA: 0x0B39EFCC */ }
        System.Boolean resetControllerPlayableOnChangeActive { get; /* RVA: 0x0B39E7C8 */ set; /* RVA: 0x0B39F054 */ }
        System.Boolean isOptimizable { get; /* RVA: 0x0B39E388 */ }
        System.Boolean isHuman { get; /* RVA: 0x03FA6110 */ }
        System.Boolean hasRootMotion { get; /* RVA: 0x0B39E2B8 */ }
        System.Boolean isRootPositionOrRotationControlledByCurves { get; /* RVA: 0x0B39E3BC */ }
        System.Single humanScale { get; /* RVA: 0x0B39E2EC */ }
        System.Boolean isInitialized { get; /* RVA: 0x0B39E320 */ }
        System.Boolean isVisible { get; /* RVA: 0x033D0530 */ }
        System.Boolean updatedLastFrame { get; /* RVA: 0x03503520 */ }
        System.Boolean enableLOD { get; /* RVA: 0x0B39E180 */ set; /* RVA: 0x036276B0 */ }
        System.Int32 LODLevel { get; /* RVA: 0x02F937E0 */ }
        System.Single motionDeltaTime { get; /* RVA: 0x0B39E5D0 */ }
        UnityEngine.Vector3 deltaPosition { get; /* RVA: 0x0343D7F0 */ }
        UnityEngine.Quaternion deltaRotation { get; /* RVA: 0x0343D8E0 */ }
        UnityEngine.Vector3 velocity { get; /* RVA: 0x0343D690 */ }
        UnityEngine.Vector3 angularVelocity { get; /* RVA: 0x0343D740 */ }
        UnityEngine.Vector3 rootPosition { get; /* RVA: 0x0B39E874 */ set; /* RVA: 0x0B39F0DC */ }
        UnityEngine.Quaternion rootRotation { get; /* RVA: 0x0B39E8E4 */ set; /* RVA: 0x0B39F134 */ }
        System.Boolean applyRootMotion { get; /* RVA: 0x0B39DF70 */ set; /* RVA: 0x0B39EB68 */ }
        System.Boolean linearVelocityBlending { get; /* RVA: 0x0B39E4CC */ set; /* RVA: 0x0B39EE7C */ }
        System.Boolean animatePhysics { get; /* RVA: 0x0B39DF24 */ set; /* RVA: 0x0B39EB04 */ }
        UnityEngine.AnimatorUpdateMode updateMode { get; /* RVA: 0x035BD460 */ set; /* RVA: 0x0B39F18C */ }
        System.Boolean hasTransformHierarchy { get; /* RVA: 0x039C7C00 */ }
        System.Boolean allowConstantClipSamplingOptimization { get; /* RVA: 0x0B39DEF0 */ set; /* RVA: 0x0B39EAC0 */ }
        System.Single gravityWeight { get; /* RVA: 0x0343D8A0 */ }
        UnityEngine.Vector3 bodyPosition { get; /* RVA: 0x0B39E048 */ set; /* RVA: 0x0B39EC04 */ }
        UnityEngine.Vector3 bodyPositionInternal { get; /* RVA: 0x0B39E01C */ set; /* RVA: 0x0B39EBF0 */ }
        UnityEngine.Quaternion bodyRotation { get; /* RVA: 0x0B39E108 */ set; /* RVA: 0x0B39ECA0 */ }
        UnityEngine.Quaternion bodyRotationInternal { get; /* RVA: 0x0B39E0DC */ set; /* RVA: 0x0B39EC8C */ }
        System.Boolean stabilizeFeet { get; /* RVA: 0x0B39E944 */ set; /* RVA: 0x0B39F148 */ }
        System.Int32 layerCount { get; /* RVA: 0x0322D150 */ }
        UnityEngine.AnimatorControllerParameter[] parameters { get; /* RVA: 0x0378ADA0 */ }
        System.Int32 parameterCount { get; /* RVA: 0x0378ADE0 */ }
        System.Single feetPivotActive { get; /* RVA: 0x0B39E250 */ set; /* RVA: 0x0B39ED64 */ }
        System.Single pivotWeight { get; /* RVA: 0x0B39E6DC */ }
        UnityEngine.Vector3 pivotPosition { get; /* RVA: 0x0B39E6B0 */ }
        System.Boolean isMatchingTarget { get; /* RVA: 0x0B39E354 */ }
        System.Single timeScale { get; /* RVA: 0x03860A10 */ set; /* RVA: 0x038602D0 */ }
        System.Single speed { get; /* RVA: 0x0B39E910 */ set; /* RVA: 0x03861540 */ }
        UnityEngine.Vector3 targetPosition { get; /* RVA: 0x0B39E9F0 */ }
        UnityEngine.Quaternion targetRotation { get; /* RVA: 0x0B39EA60 */ }
        UnityEngine.Transform avatarRoot { get; /* RVA: 0x0B39DFA4 */ }
        UnityEngine.AnimatorCullingMode cullingMode { get; /* RVA: 0x035BD8B0 */ set; /* RVA: 0x031DD660 */ }
        System.Single playbackTime { get; /* RVA: 0x0B39E748 */ set; /* RVA: 0x0B39F010 */ }
        System.Single recorderStartTime { get; /* RVA: 0x0B39E7B0 */ set; /* RVA: 0x041E1670 */ }
        System.Single recorderStopTime { get; /* RVA: 0x0B39E7BC */ set; /* RVA: 0x041E1670 */ }
        UnityEngine.AnimatorRecorderMode recorderMode { get; /* RVA: 0x0B39E77C */ }
        UnityEngine.RuntimeAnimatorController runtimeAnimatorController { get; /* RVA: 0x0300FA90 */ set; /* RVA: 0x0322E3B0 */ }
        System.Boolean hasBoundPlayables { get; /* RVA: 0x03796920 */ }
        UnityEngine.Avatar avatar { get; /* RVA: 0x039C99F0 */ set; /* RVA: 0x03DD0120 */ }
        UnityEngine.HGSkeletalMorphAvatar morphAvatar { get; /* RVA: 0x0B39E59C */ set; /* RVA: 0x0B39EF88 */ }
        UnityEngine.LodBoneMask lodBoneMask { get; /* RVA: 0x0B39E534 */ set; /* RVA: 0x0B39EF00 */ }
        System.UInt32 lodBoneMaskLevel { get; /* RVA: 0x0B39E500 */ set; /* RVA: 0x0B39EEC0 */ }
        UnityEngine.Playables.PlayableGraph playableGraph { get; /* RVA: 0x0B39E710 */ }
        System.Boolean applyMorphPose { get; /* RVA: 0x0B39DF3C */ set; /* RVA: 0x0B39EB24 */ }
        System.Boolean layersAffectMassCenter { get; /* RVA: 0x0B39E464 */ set; /* RVA: 0x0B39EE38 */ }
        System.Single leftFeetBottomHeight { get; /* RVA: 0x0B39E498 */ }
        System.Single rightFeetBottomHeight { get; /* RVA: 0x0B39E7FC */ }
        System.Boolean supportsOnAnimatorMove { get; /* RVA: 0x0B39E978 */ }
        System.Boolean logWarnings { get; /* RVA: 0x0B39E568 */ set; /* RVA: 0x0B39EF44 */ }
        System.Boolean enableParameterWarnings { get; /* RVA: 0x0B39E1B4 */ set; /* RVA: 0x0B39ED20 */ }
        System.Boolean fireEvents { get; /* RVA: 0x0B39E284 */ set; /* RVA: 0x0B39EDA8 */ }
        System.Boolean keepAnimatorControllerStateOnDisable { get; /* RVA: 0x0B39E3F0 */ set; /* RVA: 0x0B39EDEC */ }
        System.Boolean keepAnimatorStateOnDisable { get; /* RVA: 0x0B39E3FC */ set; /* RVA: 0x03DD00D0 */ }
        System.Boolean writeDefaultValuesOnDisable { get; /* RVA: 0x0B39EA8C */ set; /* RVA: 0x0322E400 */ }

        // Methods
        // RVA: 0x0B39DEA4  token: 0x60000CC
        public System.Void .ctor() { }
        // RVA: 0x045D15D0  token: 0x60000CD
        private static System.Void Internal_CallAnimatorStateChangeCallback(UnityEngine.Animator anim, UnityEngine.AnimatorStateInfo animatorStateInfo, System.Int32 layerIndex, System.Int32 methodIdx) { }
        // RVA: 0x0B39BC44  token: 0x60000CE
        private static System.Void Internal_CallAfterUpdateGraph(UnityEngine.Animator anim) { }
        // RVA: 0x04231DD0  token: 0x60000CF
        private static System.Boolean Internal_InvokeAnimationEventCallback(UnityEngine.Animator animator, UnityEngine.FastAnimationEvent fastAnimationEvent) { }
        // RVA: 0x03F32C60  token: 0x60000D0
        private static System.Boolean Internal_InvokeAnimationEventCallbackOpt(UnityEngine.Animator animator, UnityEngine.FastAnimationEventGroup fastAnimationEvent) { }
        // RVA: 0x042B4AE0  token: 0x60000D1
        public System.Void SetCallbackAfterRootMotionStageCallback(UnityEngine.Animator.AniamtorCommonCallback cb) { }
        // RVA: 0x05392C40  token: 0x60000D2
        public System.Void SetAnimationEventCallback(UnityEngine.Animator.AnimationEventCallback onAnimationEvent) { }
        // RVA: 0x05390F40  token: 0x60000D3
        public System.Void SetAnimationEventCallbackOpt(UnityEngine.Animator.AnimationEventCallbackOpt onAnimationEvent) { }
        // RVA: 0x0408AA20  token: 0x60000D4
        public System.Void SetNotifyAnimatorStateEnterAndExitEventCallBack(UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStateEnter, UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStateExit) { }
        // RVA: 0x046A91B0  token: 0x60000D5
        public System.Void SetNotifyAnimatorStatePreExitEventCallBack(UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStatePreExit) { }
        // RVA: 0x03787AB0  token: 0x60000EF
        public System.Boolean SetLODLevel(System.Int32 level) { }
        // RVA: 0x0B39B208  token: 0x60000F0
        public System.Int32 GetCurrentLODLevel() { }
        // RVA: 0x033D1900  token: 0x60000F1
        public UnityEngine.AnimatorLOD[] GetLODs() { }
        // RVA: 0x037878D0  token: 0x60000F2
        public System.Void SetLODs(UnityEngine.AnimatorLOD[] lods) { }
        // RVA: 0x0B39C984  token: 0x60000F3
        public System.Void SetConstraintEnableImmediately(System.Int32 nameHash) { }
        // RVA: 0x0B39C8F4  token: 0x60000F4
        public System.Void SetConstraintDisableImmediately(System.Int32 nameHash) { }
        // RVA: 0x0B39C934  token: 0x60000F5
        public System.Void SetConstraintEnableBlendIn(System.Int32 nameHash, System.Single duration) { }
        // RVA: 0x0B39C8A4  token: 0x60000F6
        public System.Void SetConstraintDisableBlendOut(System.Int32 nameHash, System.Single duration) { }
        // RVA: 0x03434620  token: 0x60000F7
        public System.Void BecameVisible() { }
        // RVA: 0x0464F170  token: 0x60000F8
        public static System.Void SetAnimatorJobOptEnable(System.Boolean enabel) { }
        // RVA: 0x0464F1B0  token: 0x60000F9
        public static System.Void SetAnimatorJobOptFunc(System.Int32 value) { }
        // RVA: 0x0464F1F0  token: 0x60000FA
        public static System.Void SetAnimatorWriteTransformOpt(System.Boolean value) { }
        // RVA: 0x0464F230  token: 0x60000FB
        public static System.Void SetGlobalAnimatorClipBindingsOpt(System.Boolean value) { }
        // RVA: 0x0464F270  token: 0x60000FC
        public static System.Void SetGlobalAnimatorClipBindingsConstantOpt(System.Boolean value) { }
        // RVA: 0x0464F2B0  token: 0x60000FD
        public static System.Void SetGlobalAnimatorClipBindingsOptByAvatar(System.Boolean value) { }
        // RVA: 0x0B39C644  token: 0x60000FE
        public static System.Void SetAnimatorUroMaxDeltaTimePerframe(System.Single value) { }
        // RVA: 0x0464F2F0  token: 0x60000FF
        public static System.Void SetGlobalAnimatorCacheClipBindingsOpt(System.Boolean value) { }
        // RVA: 0x0B39ABE0  token: 0x6000100
        public static System.Single GetAnimatorUroMaxDeltaTimePerframe() { }
        // RVA: 0x0B39C610  token: 0x6000101
        public static System.Void SetAnimatorMainThreadJobCount(System.Int32 value) { }
        // RVA: 0x0B39D8EC  token: 0x6000102
        public static System.Void SetPrepareFrameJobBatchCountOpt(System.Int32 value) { }
        // RVA: 0x0B39D920  token: 0x6000103
        public static System.Void SetProcessAnimJobBatchCountOpt(System.Int32 value) { }
        // RVA: 0x0464F330  token: 0x6000104
        public static System.Void SetAnimatorEnableProcessOneStage(System.Boolean value) { }
        // RVA: 0x0B39C574  token: 0x6000105
        public static System.Void SetAnimatorEnableApplyOnMoveInJob(System.Boolean value) { }
        // RVA: 0x0B39C5DC  token: 0x6000106
        public static System.Void SetAnimatorEnableRequireEventsAndBehaviour(System.Boolean value) { }
        // RVA: 0x0B39C680  token: 0x6000107
        public System.Void SetApplyBuildInRootMotionInJob(System.Boolean enable) { }
        // RVA: 0x0464F370  token: 0x6000108
        public static System.Void SetAnimatorForbbidenApplyOnMoveCb(System.Boolean value) { }
        // RVA: 0x0B39C5A8  token: 0x6000109
        public static System.Void SetAnimatorEnableCrossFrameJobs(System.Boolean enabel) { }
        // RVA: 0x0378B130  token: 0x600010A
        public System.Void SetAbilityLevel(System.Int32 abilityLevel) { }
        // RVA: 0x02F937A0  token: 0x600010B
        public System.Int32 GetAbilityLevel() { }
        // RVA: 0x0B39DBD4  token: 0x600010C
        public static System.Void SetSampleClipCacheCreatePreFrameCount(System.Int32 value) { }
        // RVA: 0x0B39DC08  token: 0x600010D
        public static System.Void SetSampleClipCacheMaxCount(System.Int32 value) { }
        // RVA: 0x0378AAA0  token: 0x600010E
        public System.Void SetLayerMeshSpace(System.Int32 layerIndex, System.Boolean enable) { }
        // RVA: 0x0B39B7D0  token: 0x600010F
        public System.Boolean GetLayerMeshSpace(System.Int32 layerIndex) { }
        // RVA: 0x0B39BD00  token: 0x6000110
        private System.Void Internal_SetLayerMaskBoneWeights(System.Int32 layerIndex, System.IntPtr bonePathHashes, System.IntPtr weights, System.Int32 count) { }
        // RVA: 0x0B399C30  token: 0x6000111
        public System.Void ClearLayerMaskBoneWeights(System.Int32 layerIndex) { }
        // RVA: 0x0B39D1D0  token: 0x6000112
        public System.Void SetLayerMaskBoneWeights(System.Int32 layerIndex, System.Int32[] bonePathHashes, System.Single[] weights) { }
        // RVA: 0x0B39D318  token: 0x6000113
        public System.Void SetLayerMaskBoneWeights(System.Int32 layerIndex, Unity.Collections.NativeArray<System.Int32> bonePathHashes, Unity.Collections.NativeArray<System.Single> weights) { }
        // RVA: 0x0322E550  token: 0x6000114
        public System.Void SetRetargetScaleEnabled(System.Boolean enabled) { }
        // RVA: 0x0B39BA90  token: 0x6000115
        public System.Boolean GetRetargetScaleEnabled() { }
        // RVA: 0x0B39D954  token: 0x6000116
        public System.Void SetRetargetScaleByAvatar(UnityEngine.Avatar referenceAvatar, System.String[] boneNames) { }
        // RVA: 0x0B39BDC0  token: 0x6000117
        private System.Void Internal_SetRetargetScaleByAvatar(UnityEngine.Avatar referenceAvatar, System.Int32[] boneNameHashes, System.Int32 count) { }
        // RVA: 0x0B39DA98  token: 0x6000118
        public System.Void SetRetargetScale(System.String[] boneNames, UnityEngine.Vector3 scaleFactor, UnityEngine.Animator.RetargetBoneMode[] boneModes) { }
        // RVA: 0x0B39BE88  token: 0x6000119
        private System.Void Internal_SetRetargetScale(System.Int32[] boneNameHashes, System.Int32 count, UnityEngine.Vector3 scaleFactor, System.Int32[] boneModes) { }
        // RVA: 0x0B39DA54  token: 0x600011A
        public System.Void SetRetargetScaleWeight(System.Single weight) { }
        // RVA: 0x0B39BAC4  token: 0x600011B
        public System.Single GetRetargetScaleWeight() { }
        // RVA: 0x0B39C9C4  token: 0x600011C
        public System.Void SetControllerDefaultValues(System.Boolean value) { }
        // RVA: 0x053925AC  token: 0x600011D
        public System.Single GetFloat(System.String name) { }
        // RVA: 0x05390B1C  token: 0x600011E
        public System.Single GetFloat(System.Int32 id) { }
        // RVA: 0x0B39B358  token: 0x600011F
        public System.Single GetFloatFromTransition(System.Int32 layerIndex, System.Int32 id) { }
        // RVA: 0x053944F4  token: 0x6000120
        public System.Single GetFloatFromTransitionWithDefault(System.Int32 layerIndex, System.Int32 id, System.Single defValue) { }
        // RVA: 0x0B39CD4C  token: 0x6000121
        public System.Void SetFloat(System.String name, System.Single value) { }
        // RVA: 0x0B39CD3C  token: 0x6000122
        public System.Void SetFloat(System.String name, System.Single value, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x053925B8  token: 0x6000123
        public System.Void SetFloat(System.Int32 id, System.Single value) { }
        // RVA: 0x0B39CD54  token: 0x6000124
        public System.Void SetFloat(System.Int32 id, System.Single value, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x0B39B040  token: 0x6000125
        public System.Boolean GetBool(System.String name) { }
        // RVA: 0x05393DC4  token: 0x6000126
        public System.Boolean GetBool(System.Int32 id) { }
        // RVA: 0x05397548  token: 0x6000127
        public System.Void SetBool(System.String name, System.Boolean value) { }
        // RVA: 0x0539463C  token: 0x6000128
        public System.Void SetBool(System.Int32 id, System.Boolean value) { }
        // RVA: 0x0B39B7C4  token: 0x6000129
        public System.Int32 GetInteger(System.String name) { }
        // RVA: 0x053925A0  token: 0x600012A
        public System.Int32 GetInteger(System.Int32 id) { }
        // RVA: 0x0B39D1C8  token: 0x600012B
        public System.Void SetInteger(System.String name, System.Int32 value) { }
        // RVA: 0x05396E38  token: 0x600012C
        public System.Void SetInteger(System.Int32 id, System.Int32 value) { }
        // RVA: 0x05393888  token: 0x600012D
        public System.Void SetTrigger(System.String name) { }
        // RVA: 0x0B39DC8C  token: 0x600012E
        public System.Void SetTrigger(System.Int32 id) { }
        // RVA: 0x05393880  token: 0x600012F
        public System.Void ResetTrigger(System.String name) { }
        // RVA: 0x05390B14  token: 0x6000130
        public System.Void ResetTrigger(System.Int32 id) { }
        // RVA: 0x0B39C060  token: 0x6000131
        public System.Boolean IsParameterControlledByCurve(System.String name) { }
        // RVA: 0x0B39C06C  token: 0x6000132
        public System.Boolean IsParameterControlledByCurve(System.Int32 id) { }
        // RVA: 0x0B39C530  token: 0x6000145
        public System.Void SetAllowConstantClipSamplingOpt(System.Boolean enable) { }
        // RVA: 0x0B39B698  token: 0x6000151
        public UnityEngine.Vector3 GetIKPosition(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39B3B4  token: 0x6000152
        private UnityEngine.Vector3 GetGoalPosition(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39D098  token: 0x6000153
        public System.Void SetIKPosition(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3 goalPosition) { }
        // RVA: 0x0B39CDF8  token: 0x6000154
        private System.Void SetGoalPosition(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3 goalPosition) { }
        // RVA: 0x0B39B728  token: 0x6000155
        public UnityEngine.Quaternion GetIKRotation(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39B434  token: 0x6000156
        private UnityEngine.Quaternion GetGoalRotation(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39D12C  token: 0x6000157
        public System.Void SetIKRotation(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion goalRotation) { }
        // RVA: 0x0B39CE5C  token: 0x6000158
        private System.Void SetGoalRotation(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion goalRotation) { }
        // RVA: 0x0B39B668  token: 0x6000159
        public System.Single GetIKPositionWeight(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39B464  token: 0x600015A
        private System.Single GetGoalWeightPosition(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39D058  token: 0x600015B
        public System.Void SetIKPositionWeight(UnityEngine.AvatarIKGoal goal, System.Single value) { }
        // RVA: 0x0B39CE70  token: 0x600015C
        private System.Void SetGoalWeightPosition(UnityEngine.AvatarIKGoal goal, System.Single value) { }
        // RVA: 0x0B39B6F8  token: 0x600015D
        public System.Single GetIKRotationWeight(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39B4A4  token: 0x600015E
        private System.Single GetGoalWeightRotation(UnityEngine.AvatarIKGoal goal) { }
        // RVA: 0x0B39D0EC  token: 0x600015F
        public System.Void SetIKRotationWeight(UnityEngine.AvatarIKGoal goal, System.Single value) { }
        // RVA: 0x0B39CEC0  token: 0x6000160
        private System.Void SetGoalWeightRotation(UnityEngine.AvatarIKGoal goal, System.Single value) { }
        // RVA: 0x0B39B608  token: 0x6000161
        public UnityEngine.Vector3 GetIKHintPosition(UnityEngine.AvatarIKHint hint) { }
        // RVA: 0x0B39B568  token: 0x6000162
        private UnityEngine.Vector3 GetHintPosition(UnityEngine.AvatarIKHint hint) { }
        // RVA: 0x0B39D004  token: 0x6000163
        public System.Void SetIKHintPosition(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3 hintPosition) { }
        // RVA: 0x0B39CF60  token: 0x6000164
        private System.Void SetHintPosition(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3 hintPosition) { }
        // RVA: 0x0B39B5D8  token: 0x6000165
        public System.Single GetIKHintPositionWeight(UnityEngine.AvatarIKHint hint) { }
        // RVA: 0x0B39B598  token: 0x6000166
        private System.Single GetHintWeightPosition(UnityEngine.AvatarIKHint hint) { }
        // RVA: 0x0B39CFC4  token: 0x6000167
        public System.Void SetIKHintPositionWeight(UnityEngine.AvatarIKHint hint, System.Single value) { }
        // RVA: 0x0B39CF74  token: 0x6000168
        private System.Void SetHintWeightPosition(UnityEngine.AvatarIKHint hint, System.Single value) { }
        // RVA: 0x0B39D448  token: 0x6000169
        public System.Void SetLookAtPosition(UnityEngine.Vector3 lookAtPosition) { }
        // RVA: 0x0B39D434  token: 0x600016A
        private System.Void SetLookAtPositionInternal(UnityEngine.Vector3 lookAtPosition) { }
        // RVA: 0x0B39D6B8  token: 0x600016B
        public System.Void SetLookAtWeight(System.Single weight) { }
        // RVA: 0x0B39D4F4  token: 0x600016C
        public System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight) { }
        // RVA: 0x0B39D648  token: 0x600016D
        public System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight) { }
        // RVA: 0x0B39D558  token: 0x600016E
        public System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight) { }
        // RVA: 0x0B39D5D0  token: 0x600016F
        public System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight, System.Single clampWeight) { }
        // RVA: 0x0B39D48C  token: 0x6000170
        private System.Void SetLookAtWeightInternal(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight, System.Single clampWeight) { }
        // RVA: 0x0B39C7B8  token: 0x6000171
        public System.Void SetBoneLocalRotation(UnityEngine.HumanBodyBones humanBoneId, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B39C7A4  token: 0x6000172
        private System.Void SetBoneLocalRotationInternal(System.Int32 humanBoneId, UnityEngine.Quaternion rotation) { }
        // RVA: 0x05395CA8  token: 0x6000173
        public System.Void SetTransformHierarchy(System.Boolean bHas) { }
        // RVA: 0x04B0E8C0  token: 0x6000174
        private System.Void SetTransformHierarchyInternal(System.Boolean bHas) { }
        // RVA: 0x0B39AD48  token: 0x6000175
        public System.Boolean GetBoneGlobalTRS(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& trs) { }
        // RVA: 0x0B39ACF8  token: 0x6000176
        private System.Boolean GetBoneGlobalTRSInternal(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& trs) { }
        // RVA: 0x0B39AEAC  token: 0x6000177
        public UnityEngine.AnimationTransformf GetBoneRootTRS(System.Int32 avatarAssetBoneID) { }
        // RVA: 0x0B39AE74  token: 0x6000178
        private UnityEngine.AnimationTransformf GetBoneRootTRSInternal(System.Int32 avatarAssetBoneID) { }
        // RVA: 0x0B39ADDC  token: 0x6000179
        public UnityEngine.AnimationTransformf GetBoneLocalTRS(System.Int32 avatarAssetBoneID) { }
        // RVA: 0x0B39ADA4  token: 0x600017A
        private UnityEngine.AnimationTransformf GetBoneLocalTRSInternal(System.Int32 avatarAssetBoneID) { }
        // RVA: 0x0B39CA08  token: 0x600017B
        public System.Void SetCorrectiveBoneData(UnityEngine.HGCorrectiveBoneData bindings) { }
        // RVA: 0x0B39CB68  token: 0x600017C
        public System.Void SetEnableCorrectiveBoneSystem(System.Boolean enable) { }
        // RVA: 0x0B39C710  token: 0x600017D
        public System.Void SetBlendProfileData(UnityEngine.HGAvatarBlendProfile bindings) { }
        // RVA: 0x0322E650  token: 0x600017E
        public System.Void SetEnableAvatarBlendProfile(System.Boolean enable) { }
        // RVA: 0x03788050  token: 0x600017F
        public System.Void SetSequenceLayerSkipTimeThreshold(System.Single value) { }
        // RVA: 0x0B39BAF8  token: 0x6000180
        public System.Single GetSequenceLayerSkipTimeThreshold() { }
        // RVA: 0x0B39A92C  token: 0x6000181
        public System.Void ForceUpdateCorrectiveBoneDataInEditor() { }
        // RVA: 0x0B39C6CC  token: 0x6000182
        public System.Void SetBakedBindingData(UnityEngine.HGAnimationSetBindings bindings) { }
        // RVA: 0x0B39A844  token: 0x6000183
        public System.Void EnableBakedBindingData(System.Boolean enable) { }
        // RVA: 0x0B39C80C  token: 0x6000184
        public System.Boolean SetClipByIndex(UnityEngine.AnimationClip overrideClip, System.Int32 index) { }
        // RVA: 0x0B39DDE8  token: 0x6000185
        public System.Void UnloadClipsNoRebind(UnityEngine.AnimationClip[] overrideClip, System.Boolean clearCache) { }
        // RVA: 0x0B39C478  token: 0x6000186
        public System.Void ReloadClipsNoRebind(UnityEngine.AnimationClip[] overrideClip) { }
        // RVA: 0x03892500  token: 0x6000187
        public System.Void CaptureLastHumanPose() { }
        // RVA: 0x0322E310  token: 0x6000188
        public System.Void SetWriteTransformDefAsPostProcess(System.Boolean enable) { }
        // RVA: 0x0B39DC94  token: 0x6000189
        public System.Void SetWriteTransformDefAsPostProcessSkipOpt(System.Boolean enable) { }
        // RVA: 0x0408AAA0  token: 0x600018A
        public System.Void SetAnimatorJobDispatchMode(UnityEngine.AnimatorJobDispatchMode mode) { }
        // RVA: 0x0B39AA58  token: 0x600018B
        public UnityEngine.AnimatorJobDispatchMode GetAnimatorJobDispatchMode() { }
        // RVA: 0x0B399A14  token: 0x600018C
        public System.Boolean AddToFrameSyncGroup(System.String groupName, System.Boolean isMaster, UnityEngine.AnimFrameSyncMode syncMode) { }
        // RVA: 0x0B39C4BC  token: 0x600018D
        public System.Boolean RemoveFromFrameSyncGroup() { }
        // RVA: 0x0B39CB24  token: 0x600018E
        public System.Void SetEnableCollisionConstraintSolver(System.Boolean enable) { }
        // RVA: 0x0B39CBAC  token: 0x600018F
        public System.Void SetEnableDefaultValuesForceUseDefaultPose(System.Boolean enable) { }
        // RVA: 0x0B39D8A8  token: 0x6000190
        public System.Void SetPoseDriverSystemWeight(System.Single weight) { }
        // RVA: 0x0B39B9D4  token: 0x6000191
        public System.Single GetPoseDriverSystemWeight() { }
        // RVA: 0x0B39BF74  token: 0x6000192
        public System.Boolean IsEnableDefaultValuesForceUseDefaultPose() { }
        // RVA: 0x03FA6060  token: 0x6000193
        public System.Void SetEnableConstantOptByDefaultPose(System.Boolean enable) { }
        // RVA: 0x0B39BF40  token: 0x6000194
        public System.Boolean IsEnableConstantOptByDefaultPose() { }
        // RVA: 0x0B39CC34  token: 0x6000195
        public System.Void SetEnableWriteDefaultValues(System.Boolean enable) { }
        // RVA: 0x0B39CAE0  token: 0x6000196
        public System.Void SetEnableClipBindingOpt(System.Boolean enable) { }
        // RVA: 0x0B39CBF0  token: 0x6000197
        public System.Void SetEnableOptBindingByAvatar(System.Boolean enable) { }
        // RVA: 0x0464E5D0  token: 0x6000198
        public System.Boolean AddClipsToBindingsNoGrown(UnityEngine.AnimationClip[] overrideClip, System.Int32 nameHash) { }
        // RVA: 0x0322E360  token: 0x6000199
        public System.Void SetDynamicBindingReserveCount(System.Int32 count) { }
        // RVA: 0x0B39CA4C  token: 0x600019A
        public System.Void SetDynamicBindingReserveCountByTag(System.Int32 stringHash, System.Int32 count) { }
        // RVA: 0x0B39C430  token: 0x600019B
        public System.Void ReCreatePlayableMemory() { }
        // RVA: 0x0B39CD64  token: 0x600019C
        public System.Void SetForceNonConstantBindingTransforms(UnityEngine.Transform[] transforms) { }
        // RVA: 0x0B399D60  token: 0x600019D
        public UnityEngine.AnimationTransformRWBufferHandle CreateClothBindings(UnityEngine.Transform[] transformAccessArray) { }
        // RVA: 0x0B399CC4  token: 0x600019E
        public UnityEngine.AnimationTransformRWBufferHandle CreateClothBindingsByNameLst(System.String[] transformPathArray) { }
        // RVA: 0x0B39A7DC  token: 0x600019F
        public System.Void DestroyClothBindings() { }
        // RVA: 0x0B39A888  token: 0x60001A0
        public System.Void EnableClothBindings() { }
        // RVA: 0x0B39A810  token: 0x60001A1
        public System.Void DisableClothBindings() { }
        // RVA: 0x0B39C860  token: 0x60001A2
        public System.Void SetClothBindingsEnableWorldSpace(System.Boolean value) { }
        // RVA: 0x0B399AAC  token: 0x60001A3
        public System.Boolean CheckClothBindingsEnableWorldSpace() { }
        // RVA: 0x0B39D710  token: 0x60001A4
        public System.Void SetMorphBindingsEnableWorldSpace(System.Boolean value) { }
        // RVA: 0x0B399BC8  token: 0x60001A5
        public System.Boolean CheckMorphBindingsEnableWorldSpace() { }
        // RVA: 0x03B6A110  token: 0x60001A6
        public UnityEngine.AnimationTransformRWBufferHandle CreateMorphBindings(UnityEngine.Transform[] transformAccessArray) { }
        // RVA: 0x0B399DFC  token: 0x60001A7
        public UnityEngine.AnimationTransformRWBufferHandle CreateMorphBindingsByNameLst(System.String[] transformPathArray) { }
        // RVA: 0x0B399B84  token: 0x60001A8
        public System.Boolean CheckMorphBindingValid(UnityEngine.AnimationTransformRWBufferHandle& handle) { }
        // RVA: 0x03B6A0D0  token: 0x60001A9
        public System.Void EnableMorphBindings() { }
        // RVA: 0x04B6CCA0  token: 0x60001AA
        public System.Void DisableMorphBindings() { }
        // RVA: 0x044E35C0  token: 0x60001AB
        public System.Void SetOptimizedAvatarDataSet(System.Boolean optimized) { }
        // RVA: 0x0B39CA9C  token: 0x60001AC
        public System.Void SetEnabelMorphBindingWriteInLateUpdate(System.Boolean enable) { }
        // RVA: 0x0B39AC0C  token: 0x60001AD
        private UnityEngine.ScriptableObject GetBehaviour(System.Type type) { }
        // RVA: -1  // generic def  token: 0x60001AE
        public T GetBehaviour() { }
        // RVA: -1  // generic def  token: 0x60001AF
        private static T[] ConvertStateMachineBehaviour(UnityEngine.ScriptableObject[] rawObjects) { }
        // RVA: -1  // generic def  token: 0x60001B0
        public T[] GetBehaviours() { }
        // RVA: 0x0B39BC00  token: 0x60001B1
        private UnityEngine.ScriptableObject[] InternalGetBehaviours(System.Type type) { }
        // RVA: 0x0B39AC50  token: 0x60001B2
        public UnityEngine.StateMachineBehaviour[] GetBehaviours(System.Int32 fullPathHash, System.Int32 layerIndex) { }
        // RVA: 0x0B39BBA0  token: 0x60001B3
        private UnityEngine.ScriptableObject[] InternalGetBehavioursByKey(System.Int32 fullPathHash, System.Int32 layerIndex, System.Type type) { }
        // RVA: 0x0485A130  token: 0x60001B7
        public System.String GetLayerName(System.Int32 layerIndex) { }
        // RVA: 0x0378A7D0  token: 0x60001B8
        public System.Int32 GetLayerIndex(System.String layerName) { }
        // RVA: 0x0B39B810  token: 0x60001B9
        public System.Single GetLayerWeight(System.Int32 layerIndex) { }
        // RVA: 0x0413A9F0  token: 0x60001BA
        public System.Void SetLayerWeight(System.Int32 layerIndex, System.Single weight) { }
        // RVA: 0x0300E630  token: 0x60001BB
        private System.Void GetAnimatorStateInfo(System.Int32 layerIndex, UnityEngine.StateInfoIndex stateInfoIndex, UnityEngine.AnimatorStateInfo& info) { }
        // RVA: 0x0300E580  token: 0x60001BC
        public UnityEngine.AnimatorStateInfo GetCurrentAnimatorStateInfo(System.Int32 layerIndex) { }
        // RVA: 0x0300E4E0  token: 0x60001BD
        public UnityEngine.AnimatorStateInfo GetNextAnimatorStateInfo(System.Int32 layerIndex) { }
        // RVA: 0x0B39AB3C  token: 0x60001BE
        private System.Void GetAnimatorTransitionInfo(System.Int32 layerIndex, UnityEngine.AnimatorTransitionInfo& info) { }
        // RVA: 0x0B39AB8C  token: 0x60001BF
        public UnityEngine.AnimatorTransitionInfo GetAnimatorTransitionInfo(System.Int32 layerIndex) { }
        // RVA: 0x0B39A9A4  token: 0x60001C0
        private System.Int32 GetAnimatorClipInfoCount(System.Int32 layerIndex, System.Boolean current) { }
        // RVA: 0x0B39B04C  token: 0x60001C1
        public System.Int32 GetCurrentAnimatorClipInfoCount(System.Int32 layerIndex) { }
        // RVA: 0x0B39B8FC  token: 0x60001C2
        public System.Int32 GetNextAnimatorClipInfoCount(System.Int32 layerIndex) { }
        // RVA: 0x0B39B0D4  token: 0x60001C3
        public UnityEngine.AnimatorClipInfo[] GetCurrentAnimatorClipInfo(System.Int32 layerIndex) { }
        // RVA: 0x0B39B984  token: 0x60001C4
        public UnityEngine.AnimatorClipInfo[] GetNextAnimatorClipInfo(System.Int32 layerIndex) { }
        // RVA: 0x0B39B29C  token: 0x60001C5
        public System.Boolean GetDebugAnimatorClipInfo(System.Int32 layerIndex, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips) { }
        // RVA: 0x0B39B05C  token: 0x60001C6
        public System.Void GetCurrentAnimatorClipInfo(System.Int32 layerIndex, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips) { }
        // RVA: 0x0B39A9F8  token: 0x60001C7
        private System.Void GetAnimatorClipInfoInternal(System.Int32 layerIndex, System.Boolean isCurrent, System.Object clips) { }
        // RVA: 0x0B39B24C  token: 0x60001C8
        private System.Boolean GetDebugAnimatorClipInfoInternal(System.Int32 layerIndex, System.Object clips) { }
        // RVA: 0x0B39B90C  token: 0x60001C9
        public System.Void GetNextAnimatorClipInfo(System.Int32 layerIndex, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips) { }
        // RVA: 0x0300E6A0  token: 0x60001CA
        public System.Boolean IsInTransition(System.Int32 layerIndex) { }
        // RVA: 0x0343E5E0  token: 0x60001CB
        public System.Boolean IsPlayingTag(System.Int32 layerIndex, System.Int32 tagHash) { }
        // RVA: 0x03008F60  token: 0x60001CC
        public System.Boolean IsPlayingState(System.Int32 layerIndex, System.Int32 stateNameHash) { }
        // RVA: 0x0343EA60  token: 0x60001CD
        public System.Int32 GetAnimatorStateCullMode(System.Int32 layerIndex) { }
        // RVA: 0x0B39B114  token: 0x60001CE
        public UnityEngine.AnimatorStateRootMotionInfo GetCurrentAnimatorStateRootmotionInfo(System.Int32 layerIndex) { }
        // RVA: 0x0B39AAEC  token: 0x60001CF
        private System.Void GetAnimatorStateRootMotionInfo(System.Int32 layerIndex, UnityEngine.AnimatorStateRootMotionInfo& output) { }
        // RVA: 0x0B39BB60  token: 0x60001D0
        public System.Boolean HasTrigger(System.Int32 triggerId) { }
        // RVA: 0x041CCA50  token: 0x60001D3
        private UnityEngine.AnimatorControllerParameter GetParameterInternal(System.Int32 index) { }
        // RVA: 0x041CCA10  token: 0x60001D4
        public UnityEngine.AnimatorControllerParameter GetParameter(System.Int32 index) { }
        // RVA: 0x0B39C1D4  token: 0x60001D9
        private System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, System.Int32 targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime, System.Boolean completeMatch) { }
        // RVA: 0x0B39C158  token: 0x60001DA
        public System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, UnityEngine.AvatarTarget targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime) { }
        // RVA: 0x0B39C0DC  token: 0x60001DB
        public System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, UnityEngine.AvatarTarget targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime) { }
        // RVA: 0x0B39C224  token: 0x60001DC
        public System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, UnityEngine.AvatarTarget targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime, System.Boolean completeMatch) { }
        // RVA: 0x0B39BEF0  token: 0x60001DD
        public System.Void InterruptMatchTarget() { }
        // RVA: 0x0B39BEAC  token: 0x60001DE
        public System.Void InterruptMatchTarget(System.Boolean completeMatch) { }
        // RVA: 0x0B39A90C  token: 0x60001E4
        public System.Void ForceStateNormalizedTime(System.Single normalizedTime) { }
        // RVA: 0x0B39A1E8  token: 0x60001E5
        public System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration) { }
        // RVA: 0x0B399FAC  token: 0x60001E6
        public System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer) { }
        // RVA: 0x0B39A0B8  token: 0x60001E7
        public System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset) { }
        // RVA: 0x0B39A008  token: 0x60001E8
        public System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B399EB0  token: 0x60001E9
        public System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A238  token: 0x60001EA
        public System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion) { }
        // RVA: 0x0B399F28  token: 0x60001EB
        public System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A078  token: 0x60001EC
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, System.Single normalizedTransitionTime) { }
        // RVA: 0x0B39A11C  token: 0x60001ED
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset) { }
        // RVA: 0x0B399E44  token: 0x60001EE
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer) { }
        // RVA: 0x0B399E78  token: 0x60001EF
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration) { }
        // RVA: 0x0B39A158  token: 0x60001F0
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x03D70D00  token: 0x60001F1
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A1A4  token: 0x60001F2
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x03D710E0  token: 0x60001F3
        public System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion) { }
        // RVA: 0x0B39DE70  token: 0x60001F4
        public System.Void WriteDefaultValues() { }
        // RVA: 0x04A31860  token: 0x60001F5
        public System.Void WriteDefaultValuesDirty() { }
        // RVA: 0x0B39A694  token: 0x60001F6
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime) { }
        // RVA: 0x0B39A3AC  token: 0x60001F7
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset) { }
        // RVA: 0x0B39A378  token: 0x60001F8
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer) { }
        // RVA: 0x0B39A60C  token: 0x60001F9
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration) { }
        // RVA: 0x0B39A6D0  token: 0x60001FA
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion) { }
        // RVA: 0x0B39A2BC  token: 0x60001FB
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A510  token: 0x60001FC
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A588  token: 0x60001FD
        public System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A470  token: 0x60001FE
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion) { }
        // RVA: 0x0B39A3E4  token: 0x60001FF
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime) { }
        // RVA: 0x0B39A754  token: 0x6000200
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset) { }
        // RVA: 0x0B39A33C  token: 0x6000201
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer) { }
        // RVA: 0x0B39A4D0  token: 0x6000202
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration) { }
        // RVA: 0x0B39A648  token: 0x6000203
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A424  token: 0x6000204
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x0B39A790  token: 0x6000205
        public System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource) { }
        // RVA: 0x04CDE4F0  token: 0x6000206
        public System.Void ResetToDefaultStates() { }
        // RVA: 0x0B39C3A8  token: 0x6000207
        public System.Void PlayInFixedTime(System.String stateName, System.Int32 layer) { }
        // RVA: 0x0B39C330  token: 0x6000208
        public System.Void PlayInFixedTime(System.String stateName) { }
        // RVA: 0x0B39C358  token: 0x6000209
        public System.Void PlayInFixedTime(System.String stateName, System.Int32 layer, System.Single fixedTime) { }
        // RVA: 0x0495ABA0  token: 0x600020A
        public System.Void PlayInFixedTime(System.Int32 stateNameHash, System.Int32 layer, System.Single fixedTime) { }
        // RVA: 0x0B39C310  token: 0x600020B
        public System.Void PlayInFixedTime(System.Int32 stateNameHash, System.Int32 layer) { }
        // RVA: 0x0B39C3C8  token: 0x600020C
        public System.Void PlayInFixedTime(System.Int32 stateNameHash) { }
        // RVA: 0x0B39C3F0  token: 0x600020D
        public System.Void Play(System.String stateName, System.Int32 layer) { }
        // RVA: 0x031CEA20  token: 0x600020E
        public System.Void Play(System.String stateName) { }
        // RVA: 0x031CEA50  token: 0x600020F
        public System.Void Play(System.String stateName, System.Int32 layer, System.Single normalizedTime) { }
        // RVA: 0x031CEAA0  token: 0x6000210
        public System.Void Play(System.Int32 stateNameHash, System.Int32 layer, System.Single normalizedTime) { }
        // RVA: 0x0B39C410  token: 0x6000211
        public System.Void Play(System.Int32 stateNameHash, System.Int32 layer) { }
        // RVA: 0x034FCD00  token: 0x6000212
        public System.Void Play(System.Int32 stateNameHash) { }
        // RVA: 0x0B39DC3C  token: 0x6000213
        public System.Void SetTarget(UnityEngine.AvatarTarget targetIndex, System.Single targetNormalizedTime) { }
        // RVA: 0x04D85B20  token: 0x6000216
        public System.Boolean IsControlled(UnityEngine.Transform transform) { }
        // RVA: 0x0B39BEFC  token: 0x6000217
        private System.Boolean IsBoneTransform(UnityEngine.Transform transform) { }
        // RVA: 0x0B39AF34  token: 0x6000219
        public UnityEngine.Transform GetBoneTransform(UnityEngine.HumanBodyBones humanBoneId) { }
        // RVA: 0x0B39AEF4  token: 0x600021A
        private UnityEngine.Transform GetBoneTransformInternal(System.Int32 humanBoneId) { }
        // RVA: 0x039C7AE0  token: 0x600021B
        public UnityEngine.Transform GetBoneTransformByBoneId(System.Int32 boneID) { }
        // RVA: 0x039C7A90  token: 0x600021C
        private UnityEngine.Transform GetBoneTransformByBoneIdInternal(System.Int32 boneId) { }
        // RVA: 0x0B39DCD8  token: 0x600021F
        public System.Void StartPlayback() { }
        // RVA: 0x0B39DD4C  token: 0x6000220
        public System.Void StopPlayback() { }
        // RVA: 0x0B39DD0C  token: 0x6000223
        public System.Void StartRecording(System.Int32 frameCount) { }
        // RVA: 0x0B39DD80  token: 0x6000224
        public System.Void StopRecording() { }
        // RVA: 0x0B39BA28  token: 0x6000227
        private System.Single GetRecorderStartTime() { }
        // RVA: 0x0B39BA5C  token: 0x600022A
        private System.Single GetRecorderStopTime() { }
        // RVA: 0x0B399BFC  token: 0x600022F
        private System.Void ClearInternalControllerPlayable() { }
        // RVA: 0x0322C0A0  token: 0x6000230
        public System.Boolean HasState(System.Int32 layerIndex, System.Int32 stateID) { }
        // RVA: 0x03B06430  token: 0x6000231
        public static System.Int32 StringToHash(System.String name) { }
        // RVA: 0x0B39DDB4  token: 0x6000232
        public static System.Int32 StringToHashCRC32(System.IntPtr name) { }
        // RVA: 0x0B39BB2C  token: 0x600023B
        private System.String GetStats() { }
        // RVA: 0x0B39B1C4  token: 0x600023D
        private System.Void GetCurrentGraph(UnityEngine.Playables.PlayableGraph& graph) { }
        // RVA: 0x0B399B14  token: 0x600023E
        private System.Void CheckIfInIKPass() { }
        // RVA: 0x0B39BFA8  token: 0x600023F
        private System.Boolean IsInIKPass() { }
        // RVA: 0x03DF2D80  token: 0x6000240
        private System.Void SetFloatString(System.String name, System.Single value) { }
        // RVA: 0x03BDEE90  token: 0x6000241
        private System.Void SetFloatID(System.Int32 id, System.Single value) { }
        // RVA: 0x040B91E0  token: 0x6000242
        private System.Single GetFloatString(System.String name) { }
        // RVA: 0x03A09690  token: 0x6000243
        private System.Single GetFloatID(System.Int32 id) { }
        // RVA: 0x0B39B308  token: 0x6000244
        private System.Single GetFloatFromTransitionByID(System.Int32 layerIndex, System.Int32 id) { }
        // RVA: 0x043EE130  token: 0x6000245
        private System.Single GetFloatFromTransitionByIDWithDefault(System.Int32 layerIndex, System.Int32 id, System.Single defValue) { }
        // RVA: 0x046F7D80  token: 0x6000246
        private System.Void SetBoolString(System.String name, System.Boolean value) { }
        // RVA: 0x0408E410  token: 0x6000247
        private System.Void SetBoolID(System.Int32 id, System.Boolean value) { }
        // RVA: 0x0B39AFFC  token: 0x6000248
        private System.Boolean GetBoolString(System.String name) { }
        // RVA: 0x03FA3EE0  token: 0x6000249
        private System.Boolean GetBoolID(System.Int32 id) { }
        // RVA: 0x0B39D174  token: 0x600024A
        private System.Void SetIntegerString(System.String name, System.Int32 value) { }
        // RVA: 0x044E7160  token: 0x600024B
        private System.Void SetIntegerID(System.Int32 id, System.Int32 value) { }
        // RVA: 0x0B39B780  token: 0x600024C
        private System.Int32 GetIntegerString(System.String name) { }
        // RVA: 0x04072CD0  token: 0x600024D
        private System.Int32 GetIntegerID(System.Int32 id) { }
        // RVA: 0x041ECAE0  token: 0x600024E
        private System.Void SetTriggerString(System.String name) { }
        // RVA: 0x045C8690  token: 0x600024F
        private System.Void SetTriggerID(System.Int32 id) { }
        // RVA: 0x03F82A30  token: 0x6000250
        private System.Void ResetTriggerString(System.String name) { }
        // RVA: 0x03B5B250  token: 0x6000251
        private System.Void ResetTriggerID(System.Int32 id) { }
        // RVA: 0x0B39C01C  token: 0x6000252
        private System.Boolean IsParameterControlledByCurveString(System.String name) { }
        // RVA: 0x0B39BFDC  token: 0x6000253
        private System.Boolean IsParameterControlledByCurveID(System.Int32 id) { }
        // RVA: 0x0B39CCD8  token: 0x6000254
        private System.Void SetFloatStringDamp(System.String name, System.Single value, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x0B39CC78  token: 0x6000255
        private System.Void SetFloatIDDamp(System.Int32 id, System.Single value, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x0B39B4E4  token: 0x6000256
        public UnityEngine.HGAnimParameterSGHandle[] GetHGParameterHandles() { }
        // RVA: 0x0B399AE0  token: 0x6000257
        public System.Boolean CheckHGParmeterHandleValid() { }
        // RVA: 0x0B39B850  token: 0x600025A
        public System.Int32 GetMorphBoneCount() { }
        // RVA: 0x0B39B8D8  token: 0x600025B
        public System.Void GetMorphBoneIds(Unity.Collections.NativeArray<System.Int32> arrayToFill) { }
        // RVA: 0x0B39B884  token: 0x600025C
        public System.Void GetMorphBoneIdsUnsafe(System.Int32 count, Unity.Collections.NativeArray<System.Int32> arrayToFill) { }
        // RVA: 0x0B39BCB0  token: 0x600025D
        private System.Void Internal_GetMorphBoneIds(System.Int32 count, System.IntPtr arrayToFill) { }
        // RVA: 0x0B39D850  token: 0x600025E
        public System.Void SetMorphTransforms(Unity.Collections.NativeArray<System.Int32> boneIds, Unity.Collections.NativeArray<UnityEngine.Vector3> translation, Unity.Collections.NativeArray<UnityEngine.Vector3> euler, Unity.Collections.NativeArray<UnityEngine.Vector3> scale) { }
        // RVA: 0x0B39D754  token: 0x600025F
        public System.Void SetMorphTransformsUnsafe(Unity.Collections.NativeArray<System.Int32> boneIds, Unity.Collections.NativeArray<UnityEngine.Vector3> translation, Unity.Collections.NativeArray<UnityEngine.Vector3> euler, Unity.Collections.NativeArray<UnityEngine.Vector3> scale) { }
        // RVA: 0x0B39BD60  token: 0x6000260
        private System.Void Internal_SetMorphTransforms(System.Int32 count, System.IntPtr boneIds, System.IntPtr translation, System.IntPtr euler, System.IntPtr scale) { }
        // RVA: 0x0B39C2DC  token: 0x6000266
        private System.Void OnUpdateModeChanged() { }
        // RVA: 0x0B39C2A8  token: 0x6000267
        private System.Void OnCullingModeChanged() { }
        // RVA: 0x0B39DE3C  token: 0x6000268
        private System.Void WriteDefaultPose() { }
        // RVA: 0x0378AA50  token: 0x6000269
        public System.Void Update(System.Single deltaTime) { }
        // RVA: 0x0B39A960  token: 0x600026A
        public System.Void ForceUpdate(System.Single deltaTime) { }
        // RVA: 0x0B39C464  token: 0x600026B
        public System.Void Rebind() { }
        // RVA: 0x049BBFC0  token: 0x600026C
        private System.Void Rebind(System.Boolean writeDefaultValues) { }
        // RVA: 0x0B399A78  token: 0x600026D
        public System.Void ApplyBuiltinRootMotion() { }
        // RVA: 0x0B39A900  token: 0x600026E
        private System.Void EvaluateController() { }
        // RVA: 0x0B39A8BC  token: 0x600026F
        private System.Void EvaluateController(System.Single deltaTime) { }
        // RVA: 0x0B39B23C  token: 0x6000270
        private System.String GetCurrentStateName(System.Int32 layerIndex) { }
        // RVA: 0x0B39B9C4  token: 0x6000271
        private System.String GetNextStateName(System.Int32 layerIndex) { }
        // RVA: 0x0B39AA98  token: 0x6000272
        private System.String GetAnimatorStateName(System.Int32 layerIndex, System.Boolean current) { }
        // RVA: 0x0B39C4F0  token: 0x6000273
        private System.String ResolveHash(System.Int32 hash) { }
        // RVA: 0x0B39AA8C  token: 0x6000274
        public System.String GetAnimatorStateName(System.Int32 stateID) { }
        // RVA: 0x05397594  token: 0x6000275
        public System.Void GotoState(System.Int32 layer, System.Int32 stateId, System.Single normalizedTime, System.Single transitionDuration, System.Single transitionTime, UnityEngine.AnimationBlendStyle blendStyle) { }
        // RVA: 0x05396E48  token: 0x6000276
        public System.Void GotoStateInFixedTime(System.Int32 layerIndex, System.Int32 stateId, System.Single fixedTime, System.Single fixedTransitionDuration, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle) { }
        // RVA: 0x05391A5C  token: 0x6000277
        public System.Void SetAnimationStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride, System.Boolean rebind) { }
        // RVA: 0x05396E40  token: 0x6000278
        public System.Void RefreshStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride) { }
        // RVA: 0x05393BF0  token: 0x6000279
        public System.Void SetRuntimeControllerNoRebind(UnityEngine.RuntimeAnimatorController controller) { }
        // RVA: 0x0B39C470  token: 0x600027A
        public System.Void RefreshAnimationStateAsyncCompleteOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride) { }
        // RVA: 0x04970B70  token: 0x600027B
        private System.Void Internal_GotoState(System.Int32 layer, System.Int32 stateId, System.Single normalizedTime, System.Single transitionDuration, System.Single transitionTime, UnityEngine.AnimationBlendStyle blendStyle) { }
        // RVA: 0x049C7590  token: 0x600027C
        private System.Void Internal_GotoStateInFixedTime(System.Int32 layerIndex, System.Int32 stateId, System.Single fixedTime, System.Single fixedTransitionDuration, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle) { }
        // RVA: 0x045DFE50  token: 0x600027D
        private System.Void Internal_SetAnimationStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride, System.Boolean rebind) { }
        // RVA: 0x04654110  token: 0x600027E
        private System.Void Internal_RefreshAnimationStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride) { }
        // RVA: 0x047DAB80  token: 0x600027F
        private System.Void Internal_SetRuntimeControllerNoRebind(UnityEngine.RuntimeAnimatorController controller) { }
        // RVA: 0x04A3DAA0  token: 0x6000280
        private System.Void Internal_RefreshAnimationStateAsyncCompleteOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride) { }
        // RVA: 0x0B39C6C4  token: 0x6000281
        public System.Void SetAvatarNoBind(UnityEngine.Avatar avatar) { }
        // RVA: 0x044E3610  token: 0x6000282
        private System.Void Internal_SetAvatarNoBind(UnityEngine.Avatar avatar) { }
        // RVA: 0x032C6EA0  token: 0x600028F
        public UnityEngine.Vector3 GetVector(System.String name) { }
        // RVA: 0x032C6EA0  token: 0x6000290
        public UnityEngine.Vector3 GetVector(System.Int32 id) { }
        // RVA: 0x041E1670  token: 0x6000291
        public System.Void SetVector(System.String name, UnityEngine.Vector3 value) { }
        // RVA: 0x041E1670  token: 0x6000292
        public System.Void SetVector(System.Int32 id, UnityEngine.Vector3 value) { }
        // RVA: 0x0B39BA08  token: 0x6000293
        public UnityEngine.Quaternion GetQuaternion(System.String name) { }
        // RVA: 0x0B39BA08  token: 0x6000294
        public UnityEngine.Quaternion GetQuaternion(System.Int32 id) { }
        // RVA: 0x041E1670  token: 0x6000295
        public System.Void SetQuaternion(System.String name, UnityEngine.Quaternion value) { }
        // RVA: 0x041E1670  token: 0x6000296
        public System.Void SetQuaternion(System.Int32 id, UnityEngine.Quaternion value) { }
        // RVA: 0x0B39BE24  token: 0x6000297
        private System.Void Internal_SetRetargetScale_Injected(System.Int32[] boneNameHashes, System.Int32 count, UnityEngine.Vector3& scaleFactor, System.Int32[] boneModes) { }
        // RVA: 0x0343D850  token: 0x6000298
        private System.Void get_deltaPosition_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0343CE60  token: 0x6000299
        private System.Void get_deltaRotation_Injected(UnityEngine.Quaternion& ret) { }
        // RVA: 0x0343D6F0  token: 0x600029A
        private System.Void get_velocity_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0343D7A0  token: 0x600029B
        private System.Void get_angularVelocity_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B39E830  token: 0x600029C
        private System.Void get_rootPosition_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B39F098  token: 0x600029D
        private System.Void set_rootPosition_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B39E8A0  token: 0x600029E
        private System.Void get_rootRotation_Injected(UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B39F0F0  token: 0x600029F
        private System.Void set_rootRotation_Injected(UnityEngine.Quaternion& value) { }
        // RVA: 0x0B39DFD8  token: 0x60002A0
        private System.Void get_bodyPositionInternal_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B39EBAC  token: 0x60002A1
        private System.Void set_bodyPositionInternal_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B39E098  token: 0x60002A2
        private System.Void get_bodyRotationInternal_Injected(UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B39EC48  token: 0x60002A3
        private System.Void set_bodyRotationInternal_Injected(UnityEngine.Quaternion& value) { }
        // RVA: 0x0B39B364  token: 0x60002A4
        private System.Void GetGoalPosition_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B39CDA8  token: 0x60002A5
        private System.Void SetGoalPosition_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3& goalPosition) { }
        // RVA: 0x0B39B3E4  token: 0x60002A6
        private System.Void GetGoalRotation_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B39CE0C  token: 0x60002A7
        private System.Void SetGoalRotation_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion& goalRotation) { }
        // RVA: 0x0B39B518  token: 0x60002A8
        private System.Void GetHintPosition_Injected(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B39CF10  token: 0x60002A9
        private System.Void SetHintPosition_Injected(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3& hintPosition) { }
        // RVA: 0x0B39D3F0  token: 0x60002AA
        private System.Void SetLookAtPositionInternal_Injected(UnityEngine.Vector3& lookAtPosition) { }
        // RVA: 0x0B39C754  token: 0x60002AB
        private System.Void SetBoneLocalRotationInternal_Injected(System.Int32 humanBoneId, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B39AE24  token: 0x60002AC
        private System.Void GetBoneRootTRSInternal_Injected(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& ret) { }
        // RVA: 0x0B39AD54  token: 0x60002AD
        private System.Void GetBoneLocalTRSInternal_Injected(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& ret) { }
        // RVA: 0x0B399D0C  token: 0x60002AE
        private System.Void CreateClothBindings_Injected(UnityEngine.Transform[] transformAccessArray, UnityEngine.AnimationTransformRWBufferHandle& ret) { }
        // RVA: 0x0B399C70  token: 0x60002AF
        private System.Void CreateClothBindingsByNameLst_Injected(System.String[] transformPathArray, UnityEngine.AnimationTransformRWBufferHandle& ret) { }
        // RVA: 0x03B6A070  token: 0x60002B0
        private System.Void CreateMorphBindings_Injected(UnityEngine.Transform[] transformAccessArray, UnityEngine.AnimationTransformRWBufferHandle& ret) { }
        // RVA: 0x0B399DA8  token: 0x60002B1
        private System.Void CreateMorphBindingsByNameLst_Injected(System.String[] transformPathArray, UnityEngine.AnimationTransformRWBufferHandle& ret) { }
        // RVA: 0x0B39E66C  token: 0x60002B2
        private System.Void get_pivotPosition_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B39C078  token: 0x60002B3
        private System.Void MatchTarget_Injected(UnityEngine.Vector3& matchPosition, UnityEngine.Quaternion& matchRotation, System.Int32 targetBodyPart, UnityEngine.MatchTargetWeightMask& weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime, System.Boolean completeMatch) { }
        // RVA: 0x0B39E9AC  token: 0x60002B4
        private System.Void get_targetPosition_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B39EA1C  token: 0x60002B5
        private System.Void get_targetRotation_Injected(UnityEngine.Quaternion& ret) { }

    }

    // TypeToken: 0x2000030  // size: 0x28
    public class AnimatorControllerParameter
    {
        // Fields
        private System.String m_Name;  // 0x10
        private UnityEngine.AnimatorControllerParameterType m_Type;  // 0x18
        private System.Single m_DefaultFloat;  // 0x1c
        private System.Int32 m_DefaultInt;  // 0x20
        private System.Boolean m_DefaultBool;  // 0x24

        // Properties
        System.String name { get; /* RVA: 0x04D9B600 */ }
        System.Int32 nameHash { get; /* RVA: 0x05393844 */ }
        UnityEngine.AnimatorControllerParameterType type { get; /* RVA: 0x04D9D860 */ }

        // Methods
        // RVA: 0x0B398618  token: 0x60002C1
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x08479504  token: 0x60002C2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B3986B0  token: 0x60002C3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000031  // size: 0x20
    public class AnimationClipPair
    {
        // Fields
        public UnityEngine.AnimationClip originalClip;  // 0x10
        public UnityEngine.AnimationClip overrideClip;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002C4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x20
    public class AnimatorOverrideController : UnityEngine.RuntimeAnimatorController
    {
        // Fields
        private UnityEngine.AnimatorOverrideController.OnOverrideControllerDirtyCallback OnOverrideControllerDirty;  // 0x18

        // Properties
        UnityEngine.RuntimeAnimatorController runtimeAnimatorController { get; /* RVA: 0x0B399808 */ set; /* RVA: 0x0B399964 */ }
        UnityEngine.AnimationClip Item { get; /* RVA: 0x0B399614 */ set; /* RVA: 0x0B399858 */ }
        UnityEngine.AnimationClip Item { get; /* RVA: 0x0B399614 */ set; /* RVA: 0x0B399858 */ }
        System.Boolean enableBindingOpt { get; /* RVA: 0x0B39976C */ set; /* RVA: 0x0B3998DC */ }
        System.Byte reserveCount { get; /* RVA: 0x0B3997D4 */ set; /* RVA: 0x0B399920 */ }
        System.Int32 overridesCount { get; /* RVA: 0x0B3997A0 */ }
        UnityEngine.AnimationClipPair[] clips { get; /* RVA: 0x0B399624 */ set; /* RVA: 0x0B399860 */ }

        // Methods
        // RVA: 0x0B399590  token: 0x60002C5
        public System.Void .ctor() { }
        // RVA: 0x0B3995C4  token: 0x60002C6
        public System.Void .ctor(UnityEngine.RuntimeAnimatorController controller) { }
        // RVA: 0x0B3993AC  token: 0x60002C7
        private static System.Void Internal_Create(UnityEngine.AnimatorOverrideController self, UnityEngine.RuntimeAnimatorController controller) { }
        // RVA: 0x0B3993F0  token: 0x60002CC
        private UnityEngine.AnimationClip Internal_GetClipByName(System.String name, System.Boolean returnEffectiveClip) { }
        // RVA: 0x0B399444  token: 0x60002CD
        private System.Void Internal_SetClipByName(System.String name, UnityEngine.AnimationClip clip) { }
        // RVA: 0x0B399150  token: 0x60002D4
        private UnityEngine.AnimationClip GetClip(UnityEngine.AnimationClip originalClip, System.Boolean returnEffectiveClip) { }
        // RVA: 0x0B39952C  token: 0x60002D5
        private System.Void SetClip(UnityEngine.AnimationClip originalClip, UnityEngine.AnimationClip overrideClip, System.Boolean notify) { }
        // RVA: 0x0B3994F8  token: 0x60002D6
        private System.Void SendNotification() { }
        // RVA: 0x0B3991A4  token: 0x60002D7
        private UnityEngine.AnimationClip GetOriginalClip(System.Int32 index) { }
        // RVA: 0x0B3991E4  token: 0x60002D8
        private UnityEngine.AnimationClip GetOverrideClip(UnityEngine.AnimationClip originalClip) { }
        // RVA: 0x0B399228  token: 0x60002DA
        public System.Void GetOverrides(System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<UnityEngine.AnimationClip,UnityEngine.AnimationClip>> overrides) { }
        // RVA: 0x0B399024  token: 0x60002DB
        public System.Void ApplyOverrides(System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<UnityEngine.AnimationClip,UnityEngine.AnimationClip>> overrides) { }
        // RVA: 0x0B3994C4  token: 0x60002DE
        private System.Void PerformOverrideClipListCleanup() { }
        // RVA: 0x0B399498  token: 0x60002DF
        private static System.Void OnInvalidateOverrideController(UnityEngine.AnimatorOverrideController controller) { }

    }

    // TypeToken: 0x2000034  // size: 0x10
    public class AnimatorUtility
    {
        // Methods
        // RVA: 0x044E3660  token: 0x60002E2
        public static System.Void OptimizeTransformHierarchy(UnityEngine.GameObject go, System.String[] exposedTransforms) { }
        // RVA: 0x03DD01B0  token: 0x60002E3
        public static System.Void DeoptimizeTransformHierarchy(UnityEngine.GameObject go) { }

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct HumanBodyBones
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.HumanBodyBones Hips;  // const
        public static UnityEngine.HumanBodyBones LeftUpperLeg;  // const
        public static UnityEngine.HumanBodyBones RightUpperLeg;  // const
        public static UnityEngine.HumanBodyBones LeftLowerLeg;  // const
        public static UnityEngine.HumanBodyBones RightLowerLeg;  // const
        public static UnityEngine.HumanBodyBones LeftFoot;  // const
        public static UnityEngine.HumanBodyBones RightFoot;  // const
        public static UnityEngine.HumanBodyBones Spine;  // const
        public static UnityEngine.HumanBodyBones Chest;  // const
        public static UnityEngine.HumanBodyBones UpperChest;  // const
        public static UnityEngine.HumanBodyBones Neck;  // const
        public static UnityEngine.HumanBodyBones Head;  // const
        public static UnityEngine.HumanBodyBones LeftShoulder;  // const
        public static UnityEngine.HumanBodyBones RightShoulder;  // const
        public static UnityEngine.HumanBodyBones LeftUpperArm;  // const
        public static UnityEngine.HumanBodyBones RightUpperArm;  // const
        public static UnityEngine.HumanBodyBones LeftLowerArm;  // const
        public static UnityEngine.HumanBodyBones RightLowerArm;  // const
        public static UnityEngine.HumanBodyBones LeftHand;  // const
        public static UnityEngine.HumanBodyBones RightHand;  // const
        public static UnityEngine.HumanBodyBones LeftToes;  // const
        public static UnityEngine.HumanBodyBones RightToes;  // const
        public static UnityEngine.HumanBodyBones LeftEye;  // const
        public static UnityEngine.HumanBodyBones RightEye;  // const
        public static UnityEngine.HumanBodyBones Jaw;  // const
        public static UnityEngine.HumanBodyBones LeftThumbProximal;  // const
        public static UnityEngine.HumanBodyBones LeftThumbIntermediate;  // const
        public static UnityEngine.HumanBodyBones LeftThumbDistal;  // const
        public static UnityEngine.HumanBodyBones LeftIndexProximal;  // const
        public static UnityEngine.HumanBodyBones LeftIndexIntermediate;  // const
        public static UnityEngine.HumanBodyBones LeftIndexDistal;  // const
        public static UnityEngine.HumanBodyBones LeftMiddleProximal;  // const
        public static UnityEngine.HumanBodyBones LeftMiddleIntermediate;  // const
        public static UnityEngine.HumanBodyBones LeftMiddleDistal;  // const
        public static UnityEngine.HumanBodyBones LeftRingProximal;  // const
        public static UnityEngine.HumanBodyBones LeftRingIntermediate;  // const
        public static UnityEngine.HumanBodyBones LeftRingDistal;  // const
        public static UnityEngine.HumanBodyBones LeftLittleProximal;  // const
        public static UnityEngine.HumanBodyBones LeftLittleIntermediate;  // const
        public static UnityEngine.HumanBodyBones LeftLittleDistal;  // const
        public static UnityEngine.HumanBodyBones RightThumbProximal;  // const
        public static UnityEngine.HumanBodyBones RightThumbIntermediate;  // const
        public static UnityEngine.HumanBodyBones RightThumbDistal;  // const
        public static UnityEngine.HumanBodyBones RightIndexProximal;  // const
        public static UnityEngine.HumanBodyBones RightIndexIntermediate;  // const
        public static UnityEngine.HumanBodyBones RightIndexDistal;  // const
        public static UnityEngine.HumanBodyBones RightMiddleProximal;  // const
        public static UnityEngine.HumanBodyBones RightMiddleIntermediate;  // const
        public static UnityEngine.HumanBodyBones RightMiddleDistal;  // const
        public static UnityEngine.HumanBodyBones RightRingProximal;  // const
        public static UnityEngine.HumanBodyBones RightRingIntermediate;  // const
        public static UnityEngine.HumanBodyBones RightRingDistal;  // const
        public static UnityEngine.HumanBodyBones RightLittleProximal;  // const
        public static UnityEngine.HumanBodyBones RightLittleIntermediate;  // const
        public static UnityEngine.HumanBodyBones RightLittleDistal;  // const
        public static UnityEngine.HumanBodyBones LastBone;  // const

    }

    // TypeToken: 0x2000036  // size: 0x38
    public sealed struct AnimationTransformf
    {
        // Fields
        public UnityEngine.Vector3 t;  // 0x10
        public UnityEngine.Quaternion q;  // 0x1c
        public UnityEngine.Vector3 s;  // 0x2c

    }

    // TypeToken: 0x2000037  // size: 0x18
    public class Avatar : UnityEngine.Object
    {
        // Properties
        System.Boolean isValid { get; /* RVA: 0x039C9A80 */ }
        System.Boolean isHuman { get; /* RVA: 0x039C7C40 */ }
        UnityEngine.HumanDescription humanDescription { get; /* RVA: 0x0B39F944 */ }

        // Methods
        // RVA: 0x0B39F8C0  token: 0x60002E4
        private System.Void .ctor() { }
        // RVA: 0x0B39F810  token: 0x60002E8
        private System.Void SetMuscleMinMax(System.Int32 muscleId, System.Single min, System.Single max) { }
        // RVA: 0x0B39F870  token: 0x60002E9
        private System.Void SetParameter(System.Int32 parameterId, System.Single value) { }
        // RVA: 0x0B39F2DC  token: 0x60002EA
        private System.Single GetAxisLength(System.Int32 humanId) { }
        // RVA: 0x0B39F3D4  token: 0x60002EB
        private UnityEngine.Quaternion GetPreRotation(System.Int32 humanId) { }
        // RVA: 0x0B39F38C  token: 0x60002EC
        private UnityEngine.Quaternion GetPostRotation(System.Int32 humanId) { }
        // RVA: 0x0B39F41C  token: 0x60002ED
        private UnityEngine.Quaternion GetZYPostQ(System.Int32 humanId, UnityEngine.Quaternion parentQ, UnityEngine.Quaternion q) { }
        // RVA: 0x0B39F4A0  token: 0x60002EE
        private UnityEngine.Quaternion GetZYRoll(System.Int32 humanId, UnityEngine.Vector3 uvw) { }
        // RVA: 0x0B39F338  token: 0x60002EF
        private UnityEngine.Vector3 GetLimitSign(System.Int32 humanId) { }
        // RVA: 0x0B39F510  token: 0x60002F0
        private System.Single Internal_GetAxisLength(System.Int32 humanId) { }
        // RVA: 0x0B39F6A0  token: 0x60002F1
        private UnityEngine.Quaternion Internal_GetPreRotation(System.Int32 humanId) { }
        // RVA: 0x0B39F620  token: 0x60002F2
        private UnityEngine.Quaternion Internal_GetPostRotation(System.Int32 humanId) { }
        // RVA: 0x0B39F730  token: 0x60002F3
        private UnityEngine.Quaternion Internal_GetZYPostQ(System.Int32 humanId, UnityEngine.Quaternion parentQ, UnityEngine.Quaternion q) { }
        // RVA: 0x0B39F7D4  token: 0x60002F4
        private UnityEngine.Quaternion Internal_GetZYRoll(System.Int32 humanId, UnityEngine.Vector3 uvw) { }
        // RVA: 0x0B39F5A0  token: 0x60002F5
        private UnityEngine.Vector3 Internal_GetLimitSign(System.Int32 humanId) { }
        // RVA: 0x0B39F304  token: 0x60002F6
        public System.String[] GetBonePaths() { }
        // RVA: 0x0B39F900  token: 0x60002F7
        private System.Void get_humanDescription_Injected(UnityEngine.HumanDescription& ret) { }
        // RVA: 0x0B39F650  token: 0x60002F8
        private System.Void Internal_GetPreRotation_Injected(System.Int32 humanId, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B39F5D0  token: 0x60002F9
        private System.Void Internal_GetPostRotation_Injected(System.Int32 humanId, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B39F6D0  token: 0x60002FA
        private System.Void Internal_GetZYPostQ_Injected(System.Int32 humanId, UnityEngine.Quaternion& parentQ, UnityEngine.Quaternion& q, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B39F774  token: 0x60002FB
        private System.Void Internal_GetZYRoll_Injected(System.Int32 humanId, UnityEngine.Vector3& uvw, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B39F550  token: 0x60002FC
        private System.Void Internal_GetLimitSign_Injected(System.Int32 humanId, UnityEngine.Vector3& ret) { }

    }

    // TypeToken: 0x2000038  // size: 0x48
    public sealed struct SkeletonBone
    {
        // Fields
        public System.String name;  // 0x10
        private System.String parentName;  // 0x18
        public UnityEngine.Vector3 position;  // 0x20
        public UnityEngine.Quaternion rotation;  // 0x2c
        public UnityEngine.Vector3 scale;  // 0x3c

    }

    // TypeToken: 0x2000039  // size: 0x3C
    public sealed struct HumanLimit
    {
        // Fields
        private UnityEngine.Vector3 m_Min;  // 0x10
        private UnityEngine.Vector3 m_Max;  // 0x1c
        private UnityEngine.Vector3 m_Center;  // 0x28
        private System.Single m_AxisLength;  // 0x34
        private System.Int32 m_UseDefaultValues;  // 0x38

    }

    // TypeToken: 0x200003A  // size: 0x50
    public sealed struct HumanBone
    {
        // Fields
        private System.String m_BoneName;  // 0x10
        private System.String m_HumanName;  // 0x18
        public UnityEngine.HumanLimit limit;  // 0x20

    }

    // TypeToken: 0x200003B  // size: 0x50
    public sealed struct HumanDescription
    {
        // Fields
        public UnityEngine.HumanBone[] human;  // 0x10
        public UnityEngine.SkeletonBone[] skeleton;  // 0x18
        private System.Single m_ArmTwist;  // 0x20
        private System.Single m_ForeArmTwist;  // 0x24
        private System.Single m_UpperLegTwist;  // 0x28
        private System.Single m_LegTwist;  // 0x2c
        private System.Single m_ArmStretch;  // 0x30
        private System.Single m_LegStretch;  // 0x34
        private System.Single m_FeetSpacing;  // 0x38
        private System.Single m_GlobalScale;  // 0x3c
        private System.String m_RootMotionBoneName;  // 0x40
        private System.Boolean m_HasTranslationDoF;  // 0x48
        private System.Boolean m_HasExtraRoot;  // 0x49
        private System.Boolean m_SkeletonHasParents;  // 0x4a

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct AvatarMaskBodyPart
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AvatarMaskBodyPart Root;  // const
        public static UnityEngine.AvatarMaskBodyPart Body;  // const
        public static UnityEngine.AvatarMaskBodyPart Head;  // const
        public static UnityEngine.AvatarMaskBodyPart LeftLeg;  // const
        public static UnityEngine.AvatarMaskBodyPart RightLeg;  // const
        public static UnityEngine.AvatarMaskBodyPart LeftArm;  // const
        public static UnityEngine.AvatarMaskBodyPart RightArm;  // const
        public static UnityEngine.AvatarMaskBodyPart LeftFingers;  // const
        public static UnityEngine.AvatarMaskBodyPart RightFingers;  // const
        public static UnityEngine.AvatarMaskBodyPart LeftFootIK;  // const
        public static UnityEngine.AvatarMaskBodyPart RightFootIK;  // const
        public static UnityEngine.AvatarMaskBodyPart LeftHandIK;  // const
        public static UnityEngine.AvatarMaskBodyPart RightHandIK;  // const
        public static UnityEngine.AvatarMaskBodyPart LastBodyPart;  // const

    }

    // TypeToken: 0x200003D  // size: 0x18
    public sealed class AvatarMask : UnityEngine.Object
    {
        // Properties
        System.Int32 transformCount { get; /* RVA: 0x0B39F2A8 */ }

        // Methods
        // RVA: 0x0B39F1CC  token: 0x60002FD
        public System.Boolean GetHumanoidBodyPartActive(UnityEngine.AvatarMaskBodyPart index) { }
        // RVA: 0x0B39F228  token: 0x60002FF
        public System.String GetTransformPath(System.Int32 index) { }
        // RVA: 0x0B39F268  token: 0x6000300
        private System.Single GetTransformWeight(System.Int32 index) { }
        // RVA: 0x0B39F20C  token: 0x6000301
        public System.Boolean GetTransformActive(System.Int32 index) { }

    }

    // TypeToken: 0x200003E  // size: 0x18
    public class HGAnimationSetBindings : UnityEngine.Object
    {
    }

    // TypeToken: 0x200003F  // size: 0x18
    public class HGAvatarBlendProfile : UnityEngine.Object
    {
        // Methods
        // RVA: 0x0B39F9B0  token: 0x6000302
        public System.Void .ctor() { }
        // RVA: 0x0B39F97C  token: 0x6000303
        private static System.Void Internal_Create(UnityEngine.HGAvatarBlendProfile self) { }

    }

    // TypeToken: 0x2000040  // size: 0x18
    public class HGCorrectiveBoneData : UnityEngine.Object
    {
        // Methods
        // RVA: 0x0B39FA30  token: 0x6000304
        public System.Void .ctor() { }
        // RVA: 0x0B39F9FC  token: 0x6000305
        private static System.Void Internal_Create(UnityEngine.HGCorrectiveBoneData self) { }

    }

    // TypeToken: 0x2000041  // size: 0x18
    public sealed class HGSkeletalMorphAvatar : UnityEngine.Object
    {
        // Methods
        // RVA: 0x0B39FAB0  token: 0x6000306
        private System.Void .ctor() { }
        // RVA: 0x0B39FA7C  token: 0x6000307
        private static System.Void Internal_Create(UnityEngine.HGSkeletalMorphAvatar self) { }

    }

    // TypeToken: 0x2000042  // size: 0x38
    public sealed struct HumanPose
    {
        // Fields
        public UnityEngine.Vector3 bodyPosition;  // 0x10
        public UnityEngine.Quaternion bodyRotation;  // 0x1c
        public System.Single[] muscles;  // 0x30

        // Methods
        // RVA: 0x0B39FED8  token: 0x6000308
        private System.Void Init() { }

    }

    // TypeToken: 0x2000043  // size: 0x18
    public class HumanPoseHandler : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10

        // Methods
        // RVA: 0x0B39FC54  token: 0x6000309
        private static System.IntPtr Internal_CreateFromRoot(UnityEngine.Avatar avatar, UnityEngine.Transform root) { }
        // RVA: 0x0B39FC98  token: 0x600030A
        private static System.Void Internal_Destroy(System.IntPtr ptr) { }
        // RVA: 0x0B39FB54  token: 0x600030B
        private System.Void GetHumanPose(UnityEngine.Vector3& bodyPosition, UnityEngine.Quaternion& bodyRotation, System.Single[] muscles) { }
        // RVA: 0x0B39FAFC  token: 0x600030C
        public virtual System.Void Dispose() { }
        // RVA: 0x0B39FCCC  token: 0x600030D
        public System.Void .ctor(UnityEngine.Avatar avatar, UnityEngine.Transform root) { }
        // RVA: 0x0B39FBB8  token: 0x600030E
        public System.Void GetHumanPose(UnityEngine.HumanPose& humanPose) { }

    }

    // TypeToken: 0x2000044  // size: 0x10
    public class HumanTrait
    {
        // Properties
        System.Int32 MuscleCount { get; /* RVA: 0x0B3A0010 */ }
        System.String[] MuscleName { get; /* RVA: 0x0B3A003C */ }

        // Methods
        // RVA: 0x0B39FFDC  token: 0x6000310
        private static System.Int32 GetBoneIndexFromMono(System.Int32 humanId) { }

    }

    // TypeToken: 0x2000045  // size: 0x18
    public class LodBoneMask : UnityEngine.Object
    {
        // Methods
        // RVA: 0x0B3A009C  token: 0x6000312
        public System.Void .ctor() { }
        // RVA: 0x0B3A0068  token: 0x6000313
        private static System.Void Internal_Create(UnityEngine.LodBoneMask self) { }

    }

    // TypeToken: 0x2000046  // size: 0x20
    public class Motion : UnityEngine.Object
    {
        // Fields
        private readonly System.Boolean <isAnimatorMotion>k__BackingField;  // 0x18

        // Properties
        System.Boolean isLooping { get; /* RVA: 0x039F6F80 */ }

        // Methods
        // RVA: 0x03695570  token: 0x6000314
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000047  // size: 0x18
    public class RuntimeAnimatorController : UnityEngine.Object
    {
        // Properties
        UnityEngine.AnimationClip[] animationClips { get; /* RVA: 0x04CDE3B0 */ }

        // Methods
        // RVA: 0x0B3A08B8  token: 0x6000316
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x18
    public class RuntimeAnimatorStateOverride : UnityEngine.Object
    {
        // Methods
        // RVA: 0x0322E2A0  token: 0x6000318
        public System.Void .ctor() { }
        // RVA: 0x0322E2D0  token: 0x6000319
        private static System.Void Internal_Create(UnityEngine.RuntimeAnimatorStateOverride self) { }
        // RVA: 0x05393BD0  token: 0x600031A
        public System.Void SetClip(System.Int32 stateID, UnityEngine.AnimationClip overrideClip) { }
        // RVA: 0x05393BD8  token: 0x600031B
        public System.Void SetClipAsync(System.Int32 stateID, System.Single length, System.Boolean isHumanoid) { }
        // RVA: 0x0B3A0978  token: 0x600031C
        public System.Void RemoveClip(System.Int32 stateID) { }
        // RVA: 0x05393BE8  token: 0x600031D
        public System.Void ClearClip() { }
        // RVA: 0x05393BC4  token: 0x600031E
        public System.Boolean PreloadClip(UnityEngine.AnimationClip clipB) { }
        // RVA: 0x05393560  token: 0x600031F
        public System.Void SetTransition(System.Int32 transUserNameID, System.Single transitionDuration, System.Single transitionOffset, System.Single exitTime) { }
        // RVA: 0x0B3A0980  token: 0x6000320
        public System.Void RemoveTransition(System.Int32 transUserNameID) { }
        // RVA: 0x05391A54  token: 0x6000321
        public System.Void SetSyncGroupCurve(System.UInt32 clipNameID, UnityEngine.AnimationCurve curve) { }
        // RVA: 0x045304F0  token: 0x6000322
        private System.Void Internal_SetClip(System.UInt32 stateID, UnityEngine.AnimationClip overrideClip) { }
        // RVA: 0x04640880  token: 0x6000323
        private System.Void Internal_SetClipAsync(System.UInt32 stateID, System.Single length, System.Boolean isHumanoid) { }
        // RVA: 0x0B3A08F8  token: 0x6000324
        private System.Void Internal_RemoveClip(System.UInt32 stateID) { }
        // RVA: 0x049B8A50  token: 0x6000325
        private System.Void Internal_ClearClip() { }
        // RVA: 0x0455DEF0  token: 0x6000326
        private System.Boolean Internal_PreloadClip(UnityEngine.AnimationClip clipB) { }
        // RVA: 0x04416620  token: 0x6000327
        private System.Void Internal_SetTransition(System.UInt32 transUserNameID, System.Single transitionDuration, System.Single transitionOffset, System.Single exitTime) { }
        // RVA: 0x0B3A0938  token: 0x6000328
        private System.Void Internal_RemoveTransition(System.UInt32 transUserNameID) { }
        // RVA: 0x040E0620  token: 0x6000329
        private System.Void Internal_SetSyncGroupCurve(System.UInt32 clipNameID, UnityEngine.AnimationCurve curve) { }

    }

}

namespace UnityEngine.Animations
{

    // TypeToken: 0x200004B  // size: 0x10
    public static class AnimationPlayableBinding
    {
        // Methods
        // RVA: 0x0441D9A0  token: 0x6000330
        public static UnityEngine.Playables.PlayableBinding Create(System.String name, UnityEngine.Object key) { }
        // RVA: 0x04D2B9A0  token: 0x6000331
        private static UnityEngine.Playables.PlayableOutput CreateAnimationOutput(UnityEngine.Playables.PlayableGraph graph, System.String name) { }

    }

    // TypeToken: 0x200004C  // size: 0x10
    public class DiscreteEvaluationAttribute : System.Attribute
    {
    }

    // TypeToken: 0x200004D
    public interface IAnimationJob
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000332
        public virtual System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: -1  // abstract  token: 0x6000333
        public virtual System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream) { }

    }

    // TypeToken: 0x200004E
    public interface IAnimationWindowPreview
    {
    }

    // TypeToken: 0x200004F  // size: 0x10
    public class NotKeyableAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000334
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000050  // size: 0x14
    public sealed struct JobMethodIndex
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Animations.JobMethodIndex ProcessRootMotionMethodIndex;  // const
        public static UnityEngine.Animations.JobMethodIndex ProcessAnimationMethodIndex;  // const
        public static UnityEngine.Animations.JobMethodIndex MethodIndexCount;  // const

    }

    // TypeToken: 0x2000051  // size: 0x11
    public sealed struct ProcessAnimationJobStruct`1
    {
        // Fields
        private static System.IntPtr jobReflectionData;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000335
        public static System.IntPtr GetJobReflectionData() { }
        // RVA: -1  // not resolved  token: 0x6000336
        public static System.Void Execute(T& data, System.IntPtr animationStreamPtr, System.IntPtr methodIndex, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }

    }

    // TypeToken: 0x2000053  // size: 0x20
    public sealed struct AdvancedAnimationMixerPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AdvancedAnimationMixerPlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: 0x03E0F350  token: 0x6000339
        public static UnityEngine.Animations.AdvancedAnimationMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x03E0F450  token: 0x600033A
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x03E0FA80  token: 0x600033B
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x600033C
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x0B39513C  token: 0x600033D
        public virtual System.Boolean Equals(UnityEngine.Animations.AdvancedAnimationMixerPlayable other) { }
        // RVA: 0x03E0F5C0  token: 0x600033E
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x04B47850  token: 0x600033F
        private static System.Void .cctor() { }
        // RVA: 0x03E0FA30  token: 0x6000340
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x2000054  // size: 0x20
    public sealed struct AnimationClipPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10

        // Methods
        // RVA: 0x03438E90  token: 0x6000341
        public static UnityEngine.Animations.AnimationClipPlayable Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip) { }
        // RVA: 0x03439010  token: 0x6000342
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip) { }
        // RVA: 0x03439130  token: 0x6000343
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x6000344
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x0315BC70  token: 0x6000345
        public static UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationClipPlayable playable) { }
        // RVA: 0x0B39532C  token: 0x6000346
        public static UnityEngine.Animations.AnimationClipPlayable op_Explicit(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B3951C8  token: 0x6000347
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationClipPlayable other) { }
        // RVA: 0x05392594  token: 0x6000348
        public UnityEngine.AnimationClip GetAnimationClip() { }
        // RVA: 0x0B395270  token: 0x6000349
        public System.Single GetAnimationClipPlayableLengthUncheck() { }
        // RVA: 0x0B3952D8  token: 0x600034A
        public System.Void SetAnimationClip(UnityEngine.AnimationClip clip) { }
        // RVA: 0x0B3952D0  token: 0x600034B
        public System.Void SetAnimationClipAsync(System.Single length, System.Boolean isHumanoid) { }
        // RVA: 0x0539815C  token: 0x600034C
        public System.Void SetApplyFootIK(System.Boolean value) { }
        // RVA: 0x0B395324  token: 0x600034D
        public System.Void SetApplyPlayableIK(System.Boolean value) { }
        // RVA: 0x05398144  token: 0x600034E
        private System.Void SetRemoveStartOffset(System.Boolean value) { }
        // RVA: 0x0539814C  token: 0x600034F
        private System.Void SetOverrideLoopTime(System.Boolean value) { }
        // RVA: 0x05398154  token: 0x6000350
        private System.Void SetLoopTime(System.Boolean value) { }
        // RVA: 0x05392580  token: 0x6000351
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x03F633B0  token: 0x6000352
        private static UnityEngine.AnimationClip GetAnimationClipInternal(UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x04CD8EF0  token: 0x6000353
        private static System.Void SetAnimationClipInternal(UnityEngine.Playables.PlayableHandle& handle, UnityEngine.AnimationClip clip) { }
        // RVA: 0x0B39527C  token: 0x6000354
        private static System.Void SetAnimationClipAsyncInternal(UnityEngine.Playables.PlayableHandle& handle, System.Single length, System.Boolean isHumanoid) { }
        // RVA: 0x04D5E540  token: 0x6000355
        private static System.Void SetApplyFootIKInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x0B3952E0  token: 0x6000356
        private static System.Void SetApplyPlayableIKInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x04D5E450  token: 0x6000357
        private static System.Void SetRemoveStartOffsetInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x04D5E4A0  token: 0x6000358
        private static System.Void SetOverrideLoopTimeInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x04D5E4F0  token: 0x6000359
        private static System.Void SetLoopTimeInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x0B39523C  token: 0x600035A
        private static System.Single GetAnimationClipPlayableLengthInternalUncheck(UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x04126100  token: 0x600035B
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.AnimationClip clip, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x2000055  // size: 0x18
    public sealed struct AnimationHumanStream
    {
        // Fields
        private System.IntPtr stream;  // 0x10

    }

    // TypeToken: 0x2000056  // size: 0x20
    public sealed struct AnimationLayerMixerPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimationLayerMixerPlayable m_NullPlayable;  // static @ 0x0

        // Properties
        UnityEngine.Animations.AnimationLayerMixerPlayable Null { get; /* RVA: 0x0B3963D4 */ }

        // Methods
        // RVA: 0x03E0ECE0  token: 0x600035D
        public static UnityEngine.Animations.AnimationLayerMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x03E0ED80  token: 0x600035E
        public static UnityEngine.Animations.AnimationLayerMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount, System.Boolean singleLayerOptimization) { }
        // RVA: 0x03E0EE50  token: 0x600035F
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x03E0FC00  token: 0x6000360
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle, System.Boolean singleLayerOptimization) { }
        // RVA: 0x04D9E560  token: 0x6000361
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x03FA72D0  token: 0x6000362
        public static UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationLayerMixerPlayable playable) { }
        // RVA: 0x0B39642C  token: 0x6000363
        public static UnityEngine.Animations.AnimationLayerMixerPlayable op_Explicit(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B395BFC  token: 0x6000364
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationLayerMixerPlayable other) { }
        // RVA: 0x0B395CDC  token: 0x6000365
        public System.Void SetLayerAdditive(System.UInt32 layerIndex, System.Boolean value) { }
        // RVA: 0x0B396040  token: 0x6000366
        public System.Void SetLayerMaskFromAvatarMask(System.UInt32 layerIndex, UnityEngine.AvatarMask mask) { }
        // RVA: 0x0B396274  token: 0x6000367
        public System.Void SetLayerRootMotionDisable(System.UInt32 layerIndex, System.Boolean value) { }
        // RVA: 0x0B395E90  token: 0x6000368
        public System.Void SetLayerEnableFloatCurve(System.UInt32 layerIndex, System.Boolean value) { }
        // RVA: 0x03E0FB50  token: 0x6000369
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x0B395C88  token: 0x600036A
        private static System.Void SetLayerAdditiveInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, System.Boolean value) { }
        // RVA: 0x03E0FD00  token: 0x600036B
        private static System.Void SetSingleLayerOptimizationInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x0B395FF0  token: 0x600036C
        private static System.Void SetLayerMaskFromAvatarMaskInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, UnityEngine.AvatarMask mask) { }
        // RVA: 0x0B396220  token: 0x600036D
        private static System.Void SetLayerRootMotionDisableInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, System.Boolean value) { }
        // RVA: 0x0B395E3C  token: 0x600036E
        private static System.Void SetLayerEnableFloatCurveInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, System.Boolean value) { }
        // RVA: 0x04D11E00  token: 0x600036F
        private static System.Void .cctor() { }
        // RVA: 0x03E0FBB0  token: 0x6000370
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x2000057  // size: 0x20
    public sealed struct AnimationMixerPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimationMixerPlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: 0x0B3964A8  token: 0x6000371
        public static UnityEngine.Animations.AnimationMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount, System.Boolean normalizeWeights) { }
        // RVA: 0x03E0F600  token: 0x6000372
        public static UnityEngine.Animations.AnimationMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x03E0F700  token: 0x6000373
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x03E0F9B0  token: 0x6000374
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x6000375
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x03FA6B30  token: 0x6000376
        public static UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationMixerPlayable playable) { }
        // RVA: 0x0B396528  token: 0x6000377
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationMixerPlayable other) { }
        // RVA: 0x03E0F890  token: 0x6000378
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x04B47960  token: 0x6000379
        private static System.Void .cctor() { }
        // RVA: 0x03E0F960  token: 0x600037A
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x2000058  // size: 0x20
    public sealed struct AnimationMotionXToDeltaPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimationMotionXToDeltaPlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: 0x03FA7230  token: 0x600037B
        public static UnityEngine.Animations.AnimationMotionXToDeltaPlayable Create(UnityEngine.Playables.PlayableGraph graph) { }
        // RVA: 0x03FA6FB0  token: 0x600037C
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph) { }
        // RVA: 0x03FA5E80  token: 0x600037D
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x600037E
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x03FA92E0  token: 0x600037F
        public static UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationMotionXToDeltaPlayable playable) { }
        // RVA: 0x0B3965B4  token: 0x6000380
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationMotionXToDeltaPlayable other) { }
        // RVA: 0x03FA5FB0  token: 0x6000381
        public System.Void SetAbsoluteMotion(System.Boolean value) { }
        // RVA: 0x03FA60B0  token: 0x6000382
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x03FA6010  token: 0x6000383
        private static System.Void SetAbsoluteMotionInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x03FA5DD0  token: 0x6000384
        private static System.Void .cctor() { }
        // RVA: 0x03FA7120  token: 0x6000385
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x2000059  // size: 0x20
    public sealed struct AnimationOffsetPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimationOffsetPlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: 0x03E108B0  token: 0x6000386
        public static UnityEngine.Animations.AnimationOffsetPlayable Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Int32 inputCount) { }
        // RVA: 0x03E109A0  token: 0x6000387
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Int32 inputCount) { }
        // RVA: 0x03E10D50  token: 0x6000388
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x6000389
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x0315BC10  token: 0x600038A
        public static UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationOffsetPlayable playable) { }
        // RVA: 0x0B396640  token: 0x600038B
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationOffsetPlayable other) { }
        // RVA: 0x03E10C60  token: 0x600038C
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x03E114B0  token: 0x600038D
        private static System.Void .cctor() { }
        // RVA: 0x03E10CE0  token: 0x600038E
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x200005A  // size: 0x10
    public static class AnimationPlayableExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600038F
        public static System.Void SetAnimatedProperties(U playable, UnityEngine.AnimationClip clip) { }
        // RVA: 0x048AC0A0  token: 0x6000390
        private static System.Void SetAnimatedPropertiesInternal(UnityEngine.Playables.PlayableHandle& playable, UnityEngine.AnimationClip animatedProperties) { }

    }

    // TypeToken: 0x200005B  // size: 0x10
    public static class AnimationPlayableGraphExtensions
    {
        // Methods
        // RVA: 0x03FC8C80  token: 0x6000391
        private static System.Boolean InternalCreateAnimationOutput(UnityEngine.Playables.PlayableGraph& graph, System.String name, UnityEngine.Playables.PlayableOutputHandle& handle) { }

    }

    // TypeToken: 0x200005C  // size: 0x20
    public sealed struct AnimationPlayableOutput : UnityEngine.Playables.IPlayableOutput
    {
        // Fields
        private UnityEngine.Playables.PlayableOutputHandle m_Handle;  // 0x10

        // Properties
        UnityEngine.Animations.AnimationPlayableOutput Null { get; /* RVA: 0x0B39684C */ }

        // Methods
        // RVA: 0x03FC8B10  token: 0x6000392
        public static UnityEngine.Animations.AnimationPlayableOutput Create(UnityEngine.Playables.PlayableGraph graph, System.String name, UnityEngine.Animator target) { }
        // RVA: 0x03FC8CE0  token: 0x6000393
        private System.Void .ctor(UnityEngine.Playables.PlayableOutputHandle handle) { }
        // RVA: 0x04D9E560  token: 0x6000395
        public virtual UnityEngine.Playables.PlayableOutputHandle GetHandle() { }
        // RVA: 0x0315BC70  token: 0x6000396
        public static UnityEngine.Playables.PlayableOutput op_Implicit(UnityEngine.Animations.AnimationPlayableOutput output) { }
        // RVA: 0x04D26620  token: 0x6000397
        public static UnityEngine.Animations.AnimationPlayableOutput op_Explicit(UnityEngine.Playables.PlayableOutput output) { }
        // RVA: 0x0B3967C0  token: 0x6000398
        public UnityEngine.Animator GetTarget() { }
        // RVA: 0x05396C40  token: 0x6000399
        public System.Void SetTarget(UnityEngine.Animator value) { }
        // RVA: 0x02ED1AA0  token: 0x600039A
        public System.Void PreBindAnimator(UnityEngine.Animator value) { }
        // RVA: 0x0B396844  token: 0x600039B
        public System.Void SetNeedRebindOnChanged(System.Boolean value) { }
        // RVA: 0x0B3967CC  token: 0x600039C
        private static UnityEngine.Animator InternalGetTarget(UnityEngine.Playables.PlayableOutputHandle& handle) { }
        // RVA: 0x043C1180  token: 0x600039D
        private static System.Void InternalSetTarget(UnityEngine.Playables.PlayableOutputHandle& handle, UnityEngine.Animator target) { }
        // RVA: 0x02ED1AA0  token: 0x600039E
        private static System.Void InternalPreBindAnimator(UnityEngine.Playables.PlayableOutputHandle& handle, UnityEngine.Animator target) { }
        // RVA: 0x0B396800  token: 0x600039F
        private static System.Void InternalSetNeedRebindOnChanged(UnityEngine.Playables.PlayableOutputHandle& handle, System.Boolean value) { }

    }

    // TypeToken: 0x200005D  // size: 0x20
    public sealed struct AnimationPosePlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimationPosePlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: 0x0B3969DC  token: 0x60003A0
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x60003A1
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x0B3968C4  token: 0x60003A2
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationPosePlayable other) { }
        // RVA: 0x0B396958  token: 0x60003A3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005E  // size: 0x20
    public sealed struct AnimationRemoveScalePlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimationRemoveScalePlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: 0x0B396C3C  token: 0x60003A4
        public static UnityEngine.Animations.AnimationRemoveScalePlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x0B396B54  token: 0x60003A5
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x0B396DF8  token: 0x60003A6
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x60003A7
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x0B396ED0  token: 0x60003A8
        public static UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationRemoveScalePlayable playable) { }
        // RVA: 0x0B396CE0  token: 0x60003A9
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationRemoveScalePlayable other) { }
        // RVA: 0x0B396AF8  token: 0x60003AA
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x0B396D74  token: 0x60003AB
        private static System.Void .cctor() { }
        // RVA: 0x0B396AB4  token: 0x60003AC
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x200005F  // size: 0x20
    public sealed struct AnimationScriptPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimationScriptPlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x60003AD
        public static UnityEngine.Animations.AnimationScriptPlayable Create(UnityEngine.Playables.PlayableGraph graph, T jobData, System.Int32 inputCount) { }
        // RVA: -1  // generic def  token: 0x60003AE
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x0B3973EC  token: 0x60003AF
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x60003B0
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: -1  // generic def  token: 0x60003B1
        private System.Void CheckJobTypeValidity() { }
        // RVA: -1  // generic def  token: 0x60003B2
        public virtual System.Void SetJobData(T jobData) { }
        // RVA: 0x0B3974C4  token: 0x60003B3
        public static UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationScriptPlayable playable) { }
        // RVA: 0x0B3970E4  token: 0x60003B4
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimationScriptPlayable other) { }
        // RVA: 0x0B39730C  token: 0x60003B5
        public System.Void SetForceWriteRootMotionParam(System.Boolean value) { }
        // RVA: 0x0B397210  token: 0x60003B6
        public System.Void SetForceResetParam(System.Boolean value) { }
        // RVA: 0x0B397080  token: 0x60003B7
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle, System.IntPtr jobReflectionData) { }
        // RVA: 0x0B3972B0  token: 0x60003B8
        private static System.Void SetForceWriteRootMotionParamInternal(UnityEngine.Playables.PlayableHandle handle, System.Boolean value) { }
        // RVA: 0x0B3971B4  token: 0x60003B9
        private static System.Void SetForceResetParamInternal(UnityEngine.Playables.PlayableHandle handle, System.Boolean value) { }
        // RVA: 0x0B397368  token: 0x60003BA
        private static System.Void .cctor() { }
        // RVA: 0x0B39702C  token: 0x60003BB
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle, System.IntPtr jobReflectionData) { }
        // RVA: 0x0B39726C  token: 0x60003BC
        private static System.Void SetForceWriteRootMotionParamInternal_Injected(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }
        // RVA: 0x0B397170  token: 0x60003BD
        private static System.Void SetForceResetParamInternal_Injected(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value) { }

    }

    // TypeToken: 0x2000060  // size: 0x48
    public sealed struct AnimationStream
    {
        // Fields
        private System.UInt32 m_AnimatorBindingsVersion;  // 0x10
        private System.IntPtr constant;  // 0x18
        private System.IntPtr input;  // 0x20
        private System.IntPtr output;  // 0x28
        private System.IntPtr workspace;  // 0x30
        private System.IntPtr inputStreamAccessor;  // 0x38
        private System.IntPtr animationHandleBinder;  // 0x40

        // Properties
        System.UInt32 animatorBindingsVersion { get; /* RVA: 0x04DBA750 */ }
        System.Boolean isValid { get; /* RVA: 0x0B397CA8 */ }
        System.Single deltaTime { get; /* RVA: 0x0B397C88 */ }

        // Methods
        // RVA: 0x0B397BE0  token: 0x60003C0
        private System.Void CheckIsValid() { }
        // RVA: 0x0B397C80  token: 0x60003C2
        private System.Single GetDeltaTime() { }
        // RVA: 0x0B397C4C  token: 0x60003C3
        private static System.Single GetDeltaTime_Injected(UnityEngine.Animations.AnimationStream& _unity_self) { }

    }

    // TypeToken: 0x2000061  // size: 0x1C
    public sealed struct TransformStreamHandle
    {
        // Fields
        private System.UInt32 m_AnimatorBindingsVersion;  // 0x10
        private System.Int32 handleIndex;  // 0x14
        private System.Int32 skeletonIndex;  // 0x18

        // Properties
        System.Boolean createdByNative { get; /* RVA: 0x0B3A088C */ }
        System.Boolean hasHandleIndex { get; /* RVA: 0x0B3A08A0 */ }
        System.Boolean hasSkeletonIndex { get; /* RVA: 0x0B3A08AC */ }
        System.UInt32 animatorBindingsVersion { get; /* RVA: 0x04DBA750 */ }

        // Methods
        // RVA: 0x0B3A18FC  token: 0x60003C4
        public System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A18CC  token: 0x60003C5
        private System.Boolean IsValidInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A04F0  token: 0x60003C7
        private System.Boolean IsSameVersionAsStream(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A195C  token: 0x60003CB
        public System.Void Resolve(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A18B8  token: 0x60003CC
        public System.Boolean IsResolved(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A187C  token: 0x60003CD
        private System.Boolean IsResolvedInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A112C  token: 0x60003CE
        private System.Void CheckIsValidAndResolve(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A1738  token: 0x60003CF
        public UnityEngine.Vector3 GetPosition(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A1E3C  token: 0x60003D0
        public System.Void SetPosition(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position) { }
        // RVA: 0x0B3A1820  token: 0x60003D1
        public UnityEngine.Quaternion GetRotation(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A1EF8  token: 0x60003D2
        public System.Void SetRotation(UnityEngine.Animations.AnimationStream stream, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B3A13A8  token: 0x60003D3
        public UnityEngine.Vector3 GetLocalPosition(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A1ADC  token: 0x60003D4
        public System.Void SetLocalPosition(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position) { }
        // RVA: 0x0B3A1490  token: 0x60003D5
        public UnityEngine.Quaternion GetLocalRotation(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A1B98  token: 0x60003D6
        public System.Void SetLocalRotation(UnityEngine.Animations.AnimationStream stream, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B3A1574  token: 0x60003D7
        public UnityEngine.Vector3 GetLocalScale(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A1C4C  token: 0x60003D8
        public System.Void SetLocalScale(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 scale) { }
        // RVA: 0x0B3A164C  token: 0x60003D9
        public System.Void GetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale) { }
        // RVA: 0x0B3A1D30  token: 0x60003DA
        public System.Void SetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, System.Boolean useMask) { }
        // RVA: 0x0B3A12C8  token: 0x60003DB
        public System.Void GetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B3A19F4  token: 0x60003DC
        public System.Void SetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Boolean useMask) { }
        // RVA: 0x0B3A1954  token: 0x60003DD
        private System.Void ResolveInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A1704  token: 0x60003DE
        private UnityEngine.Vector3 GetPositionInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A1E28  token: 0x60003DF
        private System.Void SetPositionInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position) { }
        // RVA: 0x0B3A17F0  token: 0x60003E0
        private UnityEngine.Quaternion GetRotationInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A1EE4  token: 0x60003E1
        private System.Void SetRotationInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B3A1374  token: 0x60003E2
        private UnityEngine.Vector3 GetLocalPositionInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A1AC8  token: 0x60003E3
        private System.Void SetLocalPositionInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position) { }
        // RVA: 0x0B3A1460  token: 0x60003E4
        private UnityEngine.Quaternion GetLocalRotationInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A1B84  token: 0x60003E5
        private System.Void SetLocalRotationInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B3A1540  token: 0x60003E6
        private UnityEngine.Vector3 GetLocalScaleInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A1C38  token: 0x60003E7
        private System.Void SetLocalScaleInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 scale) { }
        // RVA: 0x0B3A163C  token: 0x60003E8
        private System.Void GetLocalTRSInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale) { }
        // RVA: 0x0B3A1D04  token: 0x60003E9
        private System.Void SetLocalTRSInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, System.Boolean useMask) { }
        // RVA: 0x0B3A12B8  token: 0x60003EA
        private System.Void GetGlobalTRInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B3A19D4  token: 0x60003EB
        private System.Void SetGlobalTRInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Boolean useMask) { }
        // RVA: 0x0B3A1910  token: 0x60003EC
        private static System.Void ResolveInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A16B0  token: 0x60003ED
        private static System.Void GetPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B3A1DD4  token: 0x60003EE
        private static System.Void SetPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position) { }
        // RVA: 0x0B3A179C  token: 0x60003EF
        private static System.Void GetRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B3A1E90  token: 0x60003F0
        private static System.Void SetRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B3A1320  token: 0x60003F1
        private static System.Void GetLocalPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B3A1A74  token: 0x60003F2
        private static System.Void SetLocalPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position) { }
        // RVA: 0x0B3A140C  token: 0x60003F3
        private static System.Void GetLocalRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B3A1B30  token: 0x60003F4
        private static System.Void SetLocalRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B3A14EC  token: 0x60003F5
        private static System.Void GetLocalScaleInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B3A1BE4  token: 0x60003F6
        private static System.Void SetLocalScaleInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& scale) { }
        // RVA: 0x0B3A15D8  token: 0x60003F7
        private static System.Void GetLocalTRSInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale) { }
        // RVA: 0x0B3A1CA0  token: 0x60003F8
        private static System.Void SetLocalTRSInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale, System.Boolean useMask) { }
        // RVA: 0x0B3A1254  token: 0x60003F9
        private static System.Void GetGlobalTRInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B3A1970  token: 0x60003FA
        private static System.Void SetGlobalTRInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, System.Boolean useMask) { }

    }

    // TypeToken: 0x2000062  // size: 0x20
    public sealed struct PropertyStreamHandle
    {
        // Fields
        private System.UInt32 m_AnimatorBindingsVersion;  // 0x10
        private System.Int32 handleIndex;  // 0x14
        private System.Int32 valueArrayIndex;  // 0x18
        private System.Int32 bindType;  // 0x1c

        // Properties
        System.Boolean createdByNative { get; /* RVA: 0x0B3A088C */ }
        System.Boolean hasHandleIndex { get; /* RVA: 0x0B3A08A0 */ }
        System.Boolean hasValueArrayIndex { get; /* RVA: 0x0B3A08AC */ }
        System.Boolean hasBindType { get; /* RVA: 0x0B3A0898 */ }
        System.UInt32 animatorBindingsVersion { get; /* RVA: 0x04DBA750 */ }

        // Methods
        // RVA: 0x0B3A0530  token: 0x60003FB
        public System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A04FC  token: 0x60003FC
        private System.Boolean IsValidInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A04F0  token: 0x60003FE
        private System.Boolean IsSameVersionAsStream(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A04B4  token: 0x6000403
        private System.Boolean IsResolvedInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A00E8  token: 0x6000404
        private System.Void CheckIsValidAndResolve(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0344  token: 0x6000405
        public System.Single GetFloat(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A06EC  token: 0x6000406
        public System.Void SetFloat(UnityEngine.Animations.AnimationStream stream, System.Single value) { }
        // RVA: 0x0B3A041C  token: 0x6000407
        public System.Int32 GetInt(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A07E4  token: 0x6000408
        public System.Void SetInt(UnityEngine.Animations.AnimationStream stream, System.Int32 value) { }
        // RVA: 0x0B3A0264  token: 0x6000409
        public System.Boolean GetBool(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A05EC  token: 0x600040A
        public System.Void SetBool(UnityEngine.Animations.AnimationStream stream, System.Boolean value) { }
        // RVA: 0x0B3A0588  token: 0x600040B
        private System.Void ResolveInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A033C  token: 0x600040C
        private System.Single GetFloatInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A06E4  token: 0x600040D
        private System.Void SetFloatInternal(UnityEngine.Animations.AnimationStream& stream, System.Single value) { }
        // RVA: 0x0B3A0414  token: 0x600040E
        private System.Int32 GetIntInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A07DC  token: 0x600040F
        private System.Void SetIntInternal(UnityEngine.Animations.AnimationStream& stream, System.Int32 value) { }
        // RVA: 0x0B3A025C  token: 0x6000410
        private System.Boolean GetBoolInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A05E4  token: 0x6000411
        private System.Void SetBoolInternal(UnityEngine.Animations.AnimationStream& stream, System.Boolean value) { }
        // RVA: 0x0B3A0544  token: 0x6000412
        private static System.Void ResolveInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A02F8  token: 0x6000413
        private static System.Single GetFloatInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0690  token: 0x6000414
        private static System.Void SetFloatInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, System.Single value) { }
        // RVA: 0x0B3A03D0  token: 0x6000415
        private static System.Int32 GetIntInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0788  token: 0x6000416
        private static System.Void SetIntInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, System.Int32 value) { }
        // RVA: 0x0B3A0218  token: 0x6000417
        private static System.Boolean GetBoolInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0590  token: 0x6000418
        private static System.Void SetBoolInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, System.Boolean value) { }

    }

    // TypeToken: 0x2000063  // size: 0x18
    public sealed struct TransformSceneHandle
    {
        // Fields
        private System.UInt32 valid;  // 0x10
        private System.Int32 transformSceneHandleDefinitionIndex;  // 0x14

        // Properties
        System.Boolean createdByNative { get; /* RVA: 0x068B8E74 */ }
        System.Boolean hasTransformSceneHandleDefinitionIndex { get; /* RVA: 0x0B3A08A0 */ }

        // Methods
        // RVA: 0x0B3A10E4  token: 0x6000419
        public System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A0988  token: 0x600041C
        private System.Void CheckIsValid(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0F54  token: 0x600041D
        public UnityEngine.Vector3 GetPosition(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A0BC4  token: 0x600041E
        public UnityEngine.Vector3 GetLocalPosition(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A103C  token: 0x600041F
        public UnityEngine.Quaternion GetRotation(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A0CAC  token: 0x6000420
        public UnityEngine.Quaternion GetLocalRotation(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A0D90  token: 0x6000421
        public UnityEngine.Vector3 GetLocalScale(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B3A0E68  token: 0x6000422
        public System.Void GetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale) { }
        // RVA: 0x0B3A0AE4  token: 0x6000423
        public System.Void GetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B3A10DC  token: 0x6000424
        private System.Boolean HasValidTransform(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0F20  token: 0x6000425
        private UnityEngine.Vector3 GetPositionInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0B90  token: 0x6000426
        private UnityEngine.Vector3 GetLocalPositionInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A100C  token: 0x6000427
        private UnityEngine.Quaternion GetRotationInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0C7C  token: 0x6000428
        private UnityEngine.Quaternion GetLocalRotationInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0D5C  token: 0x6000429
        private UnityEngine.Vector3 GetLocalScaleInternal(UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0E58  token: 0x600042A
        private System.Void GetLocalTRSInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale) { }
        // RVA: 0x0B3A0AD4  token: 0x600042B
        private System.Void GetGlobalTRInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B3A1098  token: 0x600042C
        private static System.Boolean HasValidTransform_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream) { }
        // RVA: 0x0B3A0ECC  token: 0x600042D
        private static System.Void GetPositionInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B3A0B3C  token: 0x600042E
        private static System.Void GetLocalPositionInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B3A0FB8  token: 0x600042F
        private static System.Void GetRotationInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B3A0C28  token: 0x6000430
        private static System.Void GetLocalRotationInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B3A0D08  token: 0x6000431
        private static System.Void GetLocalScaleInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B3A0DF4  token: 0x6000432
        private static System.Void GetLocalTRSInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale) { }
        // RVA: 0x0B3A0A70  token: 0x6000433
        private static System.Void GetGlobalTRInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }

    }

    // TypeToken: 0x2000064  // size: 0x18
    public sealed struct PropertySceneHandle
    {
        // Fields
        private System.UInt32 valid;  // 0x10
        private System.Int32 handleIndex;  // 0x14

    }

    // TypeToken: 0x2000065  // size: 0x10
    public static class AnimationSceneHandleUtility
    {
        // Methods
        // RVA: 0x0B396F24  token: 0x6000434
        public static System.Void ReadFloats(UnityEngine.Animations.AnimationStream stream, Unity.Collections.NativeArray<UnityEngine.Animations.PropertySceneHandle> handles, Unity.Collections.NativeArray<System.Single> buffer) { }
        // RVA: -1  // generic def  token: 0x6000435
        private static System.Int32 ValidateAndGetArrayCount(UnityEngine.Animations.AnimationStream& stream, Unity.Collections.NativeArray<T0> handles, Unity.Collections.NativeArray<T1> buffer) { }
        // RVA: 0x0B396FC8  token: 0x6000436
        private static System.Void ReadSceneFloatsInternal(UnityEngine.Animations.AnimationStream& stream, System.Void* propertySceneHandles, System.Void* floatBuffer, System.Int32 count) { }

    }

    // TypeToken: 0x2000066  // size: 0x10
    public static class AnimationStreamHandleUtility
    {
        // Methods
        // RVA: 0x0B397AC4  token: 0x6000437
        public static System.Void WriteFloats(UnityEngine.Animations.AnimationStream stream, Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle> handles, Unity.Collections.NativeArray<System.Single> buffer, System.Boolean useMask) { }
        // RVA: 0x0B3979B8  token: 0x6000438
        public static System.Void ReadFloats(UnityEngine.Animations.AnimationStream stream, Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle> handles, Unity.Collections.NativeArray<System.Single> buffer) { }
        // RVA: 0x0B397A60  token: 0x6000439
        private static System.Void ReadStreamFloatsInternal(UnityEngine.Animations.AnimationStream& stream, System.Void* propertyStreamHandles, System.Void* floatBuffer, System.Int32 count) { }
        // RVA: 0x0B397B7C  token: 0x600043A
        private static System.Void WriteStreamFloatsInternal(UnityEngine.Animations.AnimationStream& stream, System.Void* propertyStreamHandles, System.Void* floatBuffer, System.Int32 count, System.Boolean useMask) { }

    }

    // TypeToken: 0x2000067  // size: 0x20
    public sealed struct AnimatorControllerPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10
        private static readonly UnityEngine.Animations.AnimatorControllerPlayable m_NullPlayable;  // static @ 0x0

        // Methods
        // RVA: 0x0B398880  token: 0x600043B
        public static UnityEngine.Animations.AnimatorControllerPlayable Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.RuntimeAnimatorController controller) { }
        // RVA: 0x0B3987A8  token: 0x600043C
        private static UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, UnityEngine.RuntimeAnimatorController controller) { }
        // RVA: 0x0B398C10  token: 0x600043D
        private System.Void .ctor(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x04D9E560  token: 0x600043E
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x0B398A48  token: 0x600043F
        public System.Void SetHandle(UnityEngine.Playables.PlayableHandle handle) { }
        // RVA: 0x0B398924  token: 0x6000440
        public virtual System.Boolean Equals(UnityEngine.Animations.AnimatorControllerPlayable other) { }
        // RVA: 0x0B3989F0  token: 0x6000441
        public System.Boolean IsInTransition(System.Int32 layerIndex) { }
        // RVA: 0x0B398744  token: 0x6000442
        private static System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.RuntimeAnimatorController controller, UnityEngine.Playables.PlayableHandle& handle) { }
        // RVA: 0x0B3989B0  token: 0x6000443
        private static System.Boolean IsInTransitionInternal(UnityEngine.Playables.PlayableHandle& handle, System.Int32 layerIndex) { }
        // RVA: 0x0B398B8C  token: 0x6000444
        private static System.Void .cctor() { }
        // RVA: 0x0B3986F0  token: 0x6000445
        private static System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.RuntimeAnimatorController controller, UnityEngine.Playables.PlayableHandle& handle) { }

    }

    // TypeToken: 0x2000068  // size: 0x14
    public sealed struct CustomStreamPropertyType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Animations.CustomStreamPropertyType Float;  // const
        public static UnityEngine.Animations.CustomStreamPropertyType Bool;  // const
        public static UnityEngine.Animations.CustomStreamPropertyType Int;  // const

    }

    // TypeToken: 0x2000069  // size: 0x10
    public static class AnimatorJobExtensions
    {
        // Methods
        // RVA: 0x0B398E00  token: 0x6000446
        public static UnityEngine.Animations.TransformStreamHandle BindStreamTransform(UnityEngine.Animator animator, UnityEngine.Transform transform) { }
        // RVA: 0x0B398DBC  token: 0x6000447
        public static UnityEngine.Animations.PropertyStreamHandle BindStreamProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property) { }
        // RVA: 0x0B398C98  token: 0x6000448
        public static UnityEngine.Animations.PropertyStreamHandle BindCustomStreamProperty(UnityEngine.Animator animator, System.String property, UnityEngine.Animations.CustomStreamPropertyType type) { }
        // RVA: 0x0B398D54  token: 0x6000449
        public static UnityEngine.Animations.PropertyStreamHandle BindStreamProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference) { }
        // RVA: 0x0B398D30  token: 0x600044A
        public static UnityEngine.Animations.TransformSceneHandle BindSceneTransform(UnityEngine.Animator animator, UnityEngine.Transform transform) { }
        // RVA: 0x0B398D14  token: 0x600044B
        public static UnityEngine.Animations.PropertySceneHandle BindSceneProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property) { }
        // RVA: 0x0B398CE0  token: 0x600044C
        public static UnityEngine.Animations.PropertySceneHandle BindSceneProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference) { }
        // RVA: 0x0B398FD0  token: 0x600044D
        private static System.Void InternalBindStreamTransform(UnityEngine.Animator animator, UnityEngine.Transform transform, UnityEngine.Animations.TransformStreamHandle& transformStreamHandle) { }
        // RVA: 0x0B398F6C  token: 0x600044E
        private static System.Void InternalBindStreamProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference, UnityEngine.Animations.PropertyStreamHandle& propertyStreamHandle) { }
        // RVA: 0x0B398E50  token: 0x600044F
        private static System.Void InternalBindCustomStreamProperty(UnityEngine.Animator animator, System.String property, UnityEngine.Animations.CustomStreamPropertyType propertyType, UnityEngine.Animations.PropertyStreamHandle& propertyStreamHandle) { }
        // RVA: 0x0B398F18  token: 0x6000450
        private static System.Void InternalBindSceneTransform(UnityEngine.Animator animator, UnityEngine.Transform transform, UnityEngine.Animations.TransformSceneHandle& transformSceneHandle) { }
        // RVA: 0x0B398EB4  token: 0x6000451
        private static System.Void InternalBindSceneProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference, UnityEngine.Animations.PropertySceneHandle& propertySceneHandle) { }

    }

    // TypeToken: 0x200006A  // size: 0x14
    public sealed struct Axis
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Animations.Axis None;  // const
        public static UnityEngine.Animations.Axis X;  // const
        public static UnityEngine.Animations.Axis Y;  // const
        public static UnityEngine.Animations.Axis Z;  // const

    }

}

namespace UnityEngine.Experimental.Animations
{

    // TypeToken: 0x2000049  // size: 0x14
    public sealed struct AnimationStreamSource
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Animations.AnimationStreamSource DefaultValues;  // const
        public static UnityEngine.Experimental.Animations.AnimationStreamSource PreviousInputs;  // const

    }

    // TypeToken: 0x200004A  // size: 0x10
    public static class AnimationPlayableOutputExtensions
    {
        // Methods
        // RVA: 0x0B39677C  token: 0x600032A
        public static System.Void SetAnimationStreamSource(UnityEngine.Animations.AnimationPlayableOutput output, UnityEngine.Experimental.Animations.AnimationStreamSource streamSource) { }
        // RVA: 0x0B39679C  token: 0x600032B
        public static System.Void SetSortingOrder(UnityEngine.Animations.AnimationPlayableOutput output, System.UInt16 sortingOrder) { }
        // RVA: 0x0B396714  token: 0x600032C
        private static System.Void InternalSetAnimationStreamSource(UnityEngine.Playables.PlayableOutputHandle output, UnityEngine.Experimental.Animations.AnimationStreamSource streamSource) { }
        // RVA: 0x0B396768  token: 0x600032D
        private static System.Void InternalSetSortingOrder(UnityEngine.Playables.PlayableOutputHandle output, System.Int32 sortingOrder) { }
        // RVA: 0x0B3966D4  token: 0x600032E
        private static System.Void InternalSetAnimationStreamSource_Injected(UnityEngine.Playables.PlayableOutputHandle& output, UnityEngine.Experimental.Animations.AnimationStreamSource streamSource) { }
        // RVA: 0x0B396728  token: 0x600032F
        private static System.Void InternalSetSortingOrder_Injected(UnityEngine.Playables.PlayableOutputHandle& output, System.Int32 sortingOrder) { }

    }

}

