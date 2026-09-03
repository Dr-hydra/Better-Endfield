// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.DirectorModule.dll
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

namespace UnityEngine.Playables
{

    // TypeToken: 0x2000002  // size: 0x30
    public class PlayableDirector : UnityEngine.Behaviour, UnityEngine.IExposedPropertyTable
    {
        // Fields
        private System.Action<UnityEngine.Playables.PlayableDirector> played;  // 0x18
        private System.Action<UnityEngine.Playables.PlayableDirector> paused;  // 0x20
        private System.Action<UnityEngine.Playables.PlayableDirector> stopped;  // 0x28

        // Properties
        UnityEngine.Playables.PlayState state { get; /* RVA: 0x05395DC0 */ }
        UnityEngine.Playables.DirectorWrapMode extrapolationMode { get; /* RVA: 0x053947B0 */ set; /* RVA: 0x0B3F7120 */ }
        UnityEngine.Playables.PlayableAsset playableAsset { get; /* RVA: 0x039F07B0 */ set; /* RVA: 0x0B3F7128 */ }
        UnityEngine.Playables.PlayableGraph playableGraph { get; /* RVA: 0x03E0E9D0 */ }
        System.Boolean playOnAwake { get; /* RVA: 0x0B3F6F3C */ set; /* RVA: 0x0539770C */ }
        UnityEngine.Playables.DirectorUpdateMode timeUpdateMode { get; /* RVA: 0x039EDB60 */ set; /* RVA: 0x0B3F7130 */ }
        System.Double time { get; /* RVA: 0x0B3F6F48 */ set; /* RVA: 0x0366D6A0 */ }
        System.Double initialTime { get; /* RVA: 0x0B3F6F08 */ set; /* RVA: 0x0366D6F0 */ }
        System.Double duration { get; /* RVA: 0x0B3F6ED4 */ }

        // Events
        event System.Action<UnityEngine.Playables.PlayableDirector> played;
        event System.Action<UnityEngine.Playables.PlayableDirector> paused;
        event System.Action<UnityEngine.Playables.PlayableDirector> stopped;

        // Methods
        // RVA: 0x0B3F67A0  token: 0x6000009
        public System.Void DeferredEvaluate() { }
        // RVA: 0x0B3F6990  token: 0x600000A
        private System.Void Play(UnityEngine.Playables.FrameRate frameRate) { }
        // RVA: 0x0B3F69AC  token: 0x600000B
        public System.Void Play(UnityEngine.Playables.PlayableAsset asset) { }
        // RVA: 0x0B3F68B4  token: 0x600000C
        public System.Void Play(UnityEngine.Playables.PlayableAsset asset, UnityEngine.Playables.DirectorWrapMode mode) { }
        // RVA: 0x0B3F6C00  token: 0x600000D
        public System.Void SetGenericBinding(UnityEngine.Object key, UnityEngine.Object value) { }
        // RVA: 0x03E0D660  token: 0x6000015
        public System.Void Evaluate() { }
        // RVA: 0x0B3F6898  token: 0x6000016
        private System.Void PlayOnFrame(UnityEngine.Playables.FrameRate frameRate) { }
        // RVA: 0x0B3F6A70  token: 0x6000017
        public System.Void Play() { }
        // RVA: 0x0B3F6CFC  token: 0x6000018
        public System.Void Stop() { }
        // RVA: 0x0366D660  token: 0x6000019
        public System.Void Pause() { }
        // RVA: 0x0B3F6B6C  token: 0x600001A
        public System.Void Resume() { }
        // RVA: 0x0B3F6B0C  token: 0x600001B
        public System.Void RebuildGraph() { }
        // RVA: 0x0B3F6784  token: 0x600001C
        public virtual System.Void ClearReferenceValue(UnityEngine.PropertyName id) { }
        // RVA: 0x0B3F6CA0  token: 0x600001D
        public virtual System.Void SetReferenceValue(UnityEngine.PropertyName id, UnityEngine.Object value) { }
        // RVA: 0x05397C5C  token: 0x600001E
        public virtual UnityEngine.Object GetReferenceValue(UnityEngine.PropertyName id, System.Boolean& idValid) { }
        // RVA: 0x03FA6DB0  token: 0x600001F
        public UnityEngine.Object GetGenericBinding(UnityEngine.Object key) { }
        // RVA: 0x0B3F66FC  token: 0x6000020
        public System.Void ClearGenericBinding(UnityEngine.Object key) { }
        // RVA: 0x0B3F6AD8  token: 0x6000021
        public System.Void RebindPlayableGraphOutputs() { }
        // RVA: 0x0B3F6AA4  token: 0x6000022
        private System.Void ProcessPendingGraphChanges() { }
        // RVA: 0x0B3F6810  token: 0x6000023
        private System.Boolean HasGenericBinding(UnityEngine.Object key) { }
        // RVA: 0x04B2D220  token: 0x6000024
        private UnityEngine.Playables.PlayState GetPlayState() { }
        // RVA: 0x0B3F6CBC  token: 0x6000025
        private System.Void SetWrapMode(UnityEngine.Playables.DirectorWrapMode mode) { }
        // RVA: 0x04A3E130  token: 0x6000026
        private UnityEngine.Playables.DirectorWrapMode GetWrapMode() { }
        // RVA: 0x0B3F67A8  token: 0x6000027
        private System.Void EvaluateNextFrame() { }
        // RVA: 0x03E0EA40  token: 0x6000028
        private UnityEngine.Playables.PlayableGraph GetGraphHandle() { }
        // RVA: 0x049E1860  token: 0x6000029
        private System.Void SetPlayOnAwake(System.Boolean on) { }
        // RVA: 0x0B3F67DC  token: 0x600002A
        private System.Boolean GetPlayOnAwake() { }
        // RVA: 0x04D56410  token: 0x600002B
        private System.Void Internal_SetGenericBinding(UnityEngine.Object key, UnityEngine.Object value) { }
        // RVA: 0x0B3F6C08  token: 0x600002C
        private System.Void SetPlayableAsset(UnityEngine.ScriptableObject asset) { }
        // RVA: 0x039F0800  token: 0x600002D
        private UnityEngine.ScriptableObject Internal_GetPlayableAsset() { }
        // RVA: 0x0B3F6B40  token: 0x6000034
        private static System.Void ResetFrameTiming() { }
        // RVA: 0x0B3F6BC0  token: 0x6000035
        private System.Void SendOnPlayableDirectorPlay() { }
        // RVA: 0x0B3F6BA0  token: 0x6000036
        private System.Void SendOnPlayableDirectorPause() { }
        // RVA: 0x0B3F6BE0  token: 0x6000037
        private System.Void SendOnPlayableDirectorStop() { }
        // RVA: 0x05393520  token: 0x6000038
        public System.Void .ctor() { }
        // RVA: 0x0B3F6854  token: 0x6000039
        private System.Void PlayOnFrame_Injected(UnityEngine.Playables.FrameRate& frameRate) { }
        // RVA: 0x0B3F6740  token: 0x600003A
        private virtual System.Void ClearReferenceValue_Injected(UnityEngine.PropertyName& id) { }
        // RVA: 0x0B3F6C4C  token: 0x600003B
        private virtual System.Void SetReferenceValue_Injected(UnityEngine.PropertyName& id, UnityEngine.Object value) { }
        // RVA: 0x04B51070  token: 0x600003C
        private virtual UnityEngine.Object GetReferenceValue_Injected(UnityEngine.PropertyName& id, System.Boolean& idValid) { }
        // RVA: 0x03E0EA70  token: 0x600003D
        private System.Void GetGraphHandle_Injected(UnityEngine.Playables.PlayableGraph& ret) { }

    }

}

