// ========================================================
// Dumped by @desirepro
// Assembly: XLua.Runtime.dll
// Classes:  243
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
    public sealed class <>f__AnonymousType0`3
    {
        // Fields
        private readonly <Type>j__TPar <Type>i__Field;  // 0x0
        private readonly <Value>j__TPar <Value>i__Field;  // 0x0
        private readonly <Index>j__TPar <Index>i__Field;  // 0x0

        // Properties
        <Type>j__TPar Type { get; /* RVA: -1  // not resolved */ }
        <Value>j__TPar Value { get; /* RVA: -1  // not resolved */ }
        <Index>j__TPar Index { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000004
        public System.Void .ctor(<Type>j__TPar Type, <Value>j__TPar Value, <Index>j__TPar Index) { }
        // RVA: -1  // not resolved  token: 0x6000005
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000006
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x6000007
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000003
    public sealed class <>f__AnonymousType1`2
    {
        // Fields
        private readonly <type>j__TPar <type>i__Field;  // 0x0
        private readonly <method>j__TPar <method>i__Field;  // 0x0

        // Properties
        <type>j__TPar type { get; /* RVA: -1  // not resolved */ }
        <method>j__TPar method { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600000A
        public System.Void .ctor(<type>j__TPar type, <method>j__TPar method) { }
        // RVA: -1  // not resolved  token: 0x600000B
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x600000C
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x600000D
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000006  // size: 0x28
    public class LuaAsset : UnityEngine.ScriptableObject
    {
        // Fields
        public static System.String LuaDecodeKey;  // static @ 0x0
        public System.Boolean encode;  // 0x18
        public System.Byte[] data;  // 0x20

        // Methods
        // RVA: 0x0B89F0AC  token: 0x6000010
        public System.Byte[] GetDecodeBytes() { }
        // RVA: 0x06DCF4A4  token: 0x6000011
        public System.Void .ctor() { }
        // RVA: 0x0B89F134  token: 0x6000012
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x11
    public sealed struct StartLuaGC
    {
    }

    // TypeToken: 0x2000049  // size: 0x11
    public sealed struct WaitLuaGC
    {
    }

    // TypeToken: 0x200004A  // size: 0x11
    public sealed struct OnGCEnd
    {
    }

    // TypeToken: 0x200004B  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Scripts.Lua.LuaMultithreadingGC.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.LowLevel.PlayerLoopSystem,System.Boolean> <>9__44_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B8A5BD8  token: 0x600004C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600004D
        public System.Void .ctor() { }
        // RVA: 0x02ED60A0  token: 0x600004E
        private System.Boolean <_Dispose>b__44_0(UnityEngine.LowLevel.PlayerLoopSystem x) { }

    }

    // TypeToken: 0x200004E  // size: 0x80
    public sealed class LuaGenericDelegateCall : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F28A80  token: 0x600005D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014D0D00  token: 0x600005E
        public virtual System.Int32 Invoke(System.IntPtr L, System.Delegate delegate, XLua.ObjectTranslator objectTranslator, System.Collections.Generic.List<System.Type> castType) { }
        // RVA: 0x0B8A0F5C  token: 0x600005F
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Delegate delegate, XLua.ObjectTranslator objectTranslator, System.Collections.Generic.List<System.Type> castType, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000060
        public virtual System.Int32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000053
    public sealed class <>c__DisplayClass8_0`1
    {
        // Fields
        public XLua.DelegateBridge <>4__this;  // 0x0
        public T1 p1;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000094
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000095
        private System.Void <ActionAfterGC>b__0() { }

    }

    // TypeToken: 0x2000054
    public sealed class <>c__DisplayClass10_0`2
    {
        // Fields
        public XLua.DelegateBridge <>4__this;  // 0x0
        public T1 p1;  // 0x0
        public T2 p2;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000096
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000097
        private System.Void <ActionAfterGC>b__0() { }

    }

    // TypeToken: 0x2000055
    public sealed class <>c__DisplayClass12_0`3
    {
        // Fields
        public XLua.DelegateBridge <>4__this;  // 0x0
        public T1 p1;  // 0x0
        public T2 p2;  // 0x0
        public T3 p3;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000098
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000099
        private System.Void <ActionAfterGC>b__0() { }

    }

    // TypeToken: 0x2000056
    public sealed class <>c__DisplayClass14_0`4
    {
        // Fields
        public XLua.DelegateBridge <>4__this;  // 0x0
        public T1 p1;  // 0x0
        public T2 p2;  // 0x0
        public T3 p3;  // 0x0
        public T4 p4;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600009A
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600009B
        private System.Void <ActionAfterGC>b__0() { }

    }

    // TypeToken: 0x2000065  // size: 0x80
    public sealed class TryArrayGet : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04CF2560  token: 0x60000AC
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014D3FD0  token: 0x60000AD
        public virtual System.Boolean Invoke(System.Type type, System.IntPtr L, XLua.ObjectTranslator translator, System.Object obj, System.Int32 index) { }
        // RVA: 0x0B8A5104  token: 0x60000AE
        public virtual System.IAsyncResult BeginInvoke(System.Type type, System.IntPtr L, XLua.ObjectTranslator translator, System.Object obj, System.Int32 index, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE668  token: 0x60000AF
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000066  // size: 0x80
    public sealed class TryArraySet : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04CF24E0  token: 0x60000B0
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014CDD50  token: 0x60000B1
        public virtual System.Boolean Invoke(System.Type type, System.IntPtr L, XLua.ObjectTranslator translator, System.Object obj, System.Int32 array_idx, System.Int32 obj_idx) { }
        // RVA: 0x0B8A51C4  token: 0x60000B2
        public virtual System.IAsyncResult BeginInvoke(System.Type type, System.IntPtr L, XLua.ObjectTranslator translator, System.Object obj, System.Int32 array_idx, System.Int32 obj_idx, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE668  token: 0x60000B3
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200006C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly XLua.LuaDeadLoopCheck.<>c <>9;  // static @ 0x0
        public static System.Func<System.Diagnostics.Stopwatch> <>9__7_0;  // static @ 0x8
        public static System.Action<System.Diagnostics.Stopwatch> <>9__7_1;  // static @ 0x10
        public static System.Action<System.Diagnostics.Stopwatch> <>9__7_2;  // static @ 0x18

        // Methods
        // RVA: 0x04D4A1D0  token: 0x60000C5
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000C6
        public System.Void .ctor() { }
        // RVA: 0x04A3E0F0  token: 0x60000C7
        private System.Diagnostics.Stopwatch <EnableCheck>b__7_0() { }
        // RVA: 0x0329ECB0  token: 0x60000C8
        private System.Void <EnableCheck>b__7_1(System.Diagnostics.Stopwatch sw) { }
        // RVA: 0x0329E930  token: 0x60000C9
        private System.Void <EnableCheck>b__7_2(System.Diagnostics.Stopwatch sw) { }

    }

    // TypeToken: 0x2000070  // size: 0x18
    public sealed struct GCAction
    {
        // Fields
        public System.Int32 Reference;  // 0x10
        public System.Boolean IsDelegate;  // 0x14

    }

    // TypeToken: 0x2000071  // size: 0x80
    public sealed class CustomLoader : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x044A3FF0  token: 0x6000102
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x6000103
        public virtual System.Byte[] Invoke(System.String& filepath) { }
        // RVA: 0x09767468  token: 0x6000104
        public virtual System.IAsyncResult BeginInvoke(System.String& filepath, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD0344  token: 0x6000105
        public virtual System.Byte[] EndInvoke(System.String& filepath, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000074
    public sealed class <>c__DisplayClass2_0`1
    {
        // Fields
        public XLua.LuaFunction <>4__this;  // 0x0
        public T a;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000114
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000115
        private System.Void <ActionAfterGC>b__0() { }

    }

    // TypeToken: 0x2000075
    public sealed class <>c__DisplayClass5_0`2
    {
        // Fields
        public XLua.LuaFunction <>4__this;  // 0x0
        public T1 a1;  // 0x0
        public T2 a2;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000116
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000117
        private System.Void <ActionAfterGC>b__0() { }

    }

    // TypeToken: 0x2000077  // size: 0x48
    public sealed class <GetKeys>d__15 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public XLua.LuaTable <>4__this;  // 0x28
        private System.IntPtr <L>5__2;  // 0x30
        private XLua.ObjectTranslator <translator>5__3;  // 0x38
        private System.Int32 <oldTop>5__4;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x600012E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600012F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B8A528C  token: 0x6000130
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B8A5484  token: 0x6000132
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B8A53F0  token: 0x6000134
        private virtual System.Collections.Generic.IEnumerator<System.Object> System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator() { }
        // RVA: 0x0B8A547C  token: 0x6000135
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000078
    public sealed class <GetKeys>d__16`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private T <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        public XLua.LuaTable <>4__this;  // 0x0
        private System.IntPtr <L>5__2;  // 0x0
        private XLua.ObjectTranslator <translator>5__3;  // 0x0
        private System.Int32 <oldTop>5__4;  // 0x0

        // Properties
        T System.Collections.Generic.IEnumerator<T>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000136
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000137
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000138
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600013A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x600013C
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600013D
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000080  // size: 0x20
    public sealed class <>c__DisplayClass11_0
    {
        // Fields
        public XLua.MethodWrapsCache <>4__this;  // 0x10
        public System.Type type;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600017A
        public System.Void .ctor() { }
        // RVA: 0x0B8A54D0  token: 0x600017B
        private System.Int32 <GetConstructorWrap>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x2000081  // size: 0x20
    public sealed class <>c__DisplayClass11_1
    {
        // Fields
        public XLua.LuaDLL.lua_CSFunction ctor;  // 0x10
        public XLua.MethodWrapsCache.<>c__DisplayClass11_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600017C
        public System.Void .ctor() { }
        // RVA: 0x048BB230  token: 0x600017D
        private System.Int32 <GetConstructorWrap>b__1(System.IntPtr L) { }

    }

    // TypeToken: 0x2000082  // size: 0x50
    public sealed class <>c__DisplayClass15_0
    {
        // Fields
        public XLua.MethodWrapsCache <>4__this;  // 0x10
        public System.Type type;  // 0x18
        public System.String eventName;  // 0x20
        public System.Boolean is_static;  // 0x28
        public System.Reflection.EventInfo eventInfo;  // 0x30
        public System.Int32 start_idx;  // 0x38
        public System.Reflection.MethodInfo add;  // 0x40
        public System.Reflection.MethodInfo remove;  // 0x48

        // Methods
        // RVA: 0x041E1670  token: 0x600017E
        public System.Void .ctor() { }
        // RVA: 0x0B8A5640  token: 0x600017F
        private System.Int32 <GetEventWrap>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x2000086  // size: 0x28
    public sealed class <>c__DisplayClass14_0
    {
        // Fields
        public XLua.ObjectCheckers <>4__this;  // 0x10
        public System.Type type;  // 0x18
        public XLua.ObjectCheck fixTypeCheck;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000198
        public System.Void .ctor() { }
        // RVA: 0x03299A40  token: 0x6000199
        private System.Boolean <genChecker>b__0(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0434B0B0  token: 0x600019A
        private System.Boolean <genChecker>b__1(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x03F70F10  token: 0x600019B
        private System.Boolean <genChecker>b__2(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A5524  token: 0x600019C
        private System.Boolean <genChecker>b__3(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x04192E50  token: 0x600019D
        private System.Boolean <genChecker>b__4(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x041AE0D0  token: 0x600019E
        private System.Boolean <genChecker>b__5(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A55C0  token: 0x600019F
        private System.Boolean <genChecker>b__6(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x03299530  token: 0x60001A0
        private System.Boolean <genChecker>b__7(System.IntPtr L, System.Int32 idx) { }

    }

    // TypeToken: 0x2000087  // size: 0x18
    public sealed class <>c__DisplayClass15_0
    {
        // Fields
        public XLua.ObjectCheck oc;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001A1
        public System.Void .ctor() { }
        // RVA: 0x0B8A5B58  token: 0x60001A2
        private System.Boolean <genNullableChecker>b__0(System.IntPtr L, System.Int32 idx) { }

    }

    // TypeToken: 0x2000089  // size: 0x28
    public sealed class <>c__DisplayClass23_0
    {
        // Fields
        public XLua.ObjectCasters <>4__this;  // 0x10
        public System.Type type;  // 0x18
        public XLua.ObjectCast fixTypeGetter;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60001BB
        public System.Void .ctor() { }
        // RVA: 0x03605A30  token: 0x60001BC
        private System.Object <genCaster>b__0(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x03605CE0  token: 0x60001BD
        private System.Object <genCaster>b__1(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8ADB5C  token: 0x60001BE
        private System.Object <genCaster>b__2(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x047F20E0  token: 0x60001BF
        private System.Object <genCaster>b__3(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x03605C80  token: 0x60001C0
        private System.Object <genCaster>b__4(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8ADC18  token: 0x60001C1
        private System.Object <genCaster>b__5(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x036055C0  token: 0x60001C2
        private System.Object <genCaster>b__6(System.IntPtr L, System.Int32 idx, System.Object target) { }

    }

    // TypeToken: 0x200008A  // size: 0x28
    public sealed class <>c__DisplayClass23_1
    {
        // Fields
        public System.Type elementType;  // 0x10
        public XLua.ObjectCast elementCaster;  // 0x18
        public XLua.ObjectCasters.<>c__DisplayClass23_0 CS$<>8__locals1;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60001C3
        public System.Void .ctor() { }
        // RVA: 0x036050E0  token: 0x60001C4
        private System.Object <genCaster>b__7(System.IntPtr L, System.Int32 idx, System.Object target) { }

    }

    // TypeToken: 0x200008B  // size: 0x38
    public sealed class <>c__DisplayClass23_2
    {
        // Fields
        public System.Type keyType;  // 0x10
        public System.Type valueType;  // 0x18
        public XLua.ObjectCast keyCaster;  // 0x20
        public XLua.ObjectCast valueCaster;  // 0x28
        public XLua.ObjectCasters.<>c__DisplayClass23_0 CS$<>8__locals2;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60001C5
        public System.Void .ctor() { }
        // RVA: 0x0B8ADF2C  token: 0x60001C6
        private System.Object <genCaster>b__8(System.IntPtr L, System.Int32 idx, System.Object target) { }

    }

    // TypeToken: 0x200008C  // size: 0x18
    public sealed class <>c__DisplayClass24_0
    {
        // Fields
        public XLua.ObjectCast oc;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001C7
        public System.Void .ctor() { }
        // RVA: 0x0B8AE218  token: 0x60001C8
        private System.Object <genNullableCaster>b__0(System.IntPtr L, System.Int32 idx, System.Object target) { }

    }

    // TypeToken: 0x200008E  // size: 0x20
    public sealed struct Slot
    {
        // Fields
        public System.Int32 next;  // 0x10
        public System.Object obj;  // 0x18

        // Methods
        // RVA: 0x06B17430  token: 0x60001D5
        public System.Void .ctor(System.Int32 next, System.Object obj) { }

    }

    // TypeToken: 0x2000096  // size: 0x14
    public sealed struct LOGLEVEL
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.ObjectTranslator.LOGLEVEL NO;  // const
        public static XLua.ObjectTranslator.LOGLEVEL INFO;  // const
        public static XLua.ObjectTranslator.LOGLEVEL WARN;  // const
        public static XLua.ObjectTranslator.LOGLEVEL ERROR;  // const

    }

    // TypeToken: 0x2000097  // size: 0x28
    public sealed struct EnumerableWrapper
    {
        // Fields
        public System.Int32 Index;  // 0x10
        public System.Collections.IList CurList;  // 0x18
        public System.Collections.IEnumerator Enumerator;  // 0x20
        public static System.Collections.Generic.Stack<XLua.ObjectTranslator.EnumerableWrapper> enumerableStack;  // static @ 0x0

        // Methods
        // RVA: 0x03E77DB0  token: 0x6000230
        public static System.Int32 ListCsPairs(System.IntPtr L) { }
        // RVA: 0x03C35420  token: 0x6000231
        public static System.Int32 DicCsPairs(System.IntPtr L) { }
        // RVA: 0x03E1DA60  token: 0x6000232
        public static System.Int32 IEnumerableCsPairs(System.IntPtr L) { }
        // RVA: 0x0B8A7444  token: 0x6000233
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000098
    public sealed struct FStructWrap`1
    {
        // Fields
        public T value;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000234
        public System.IntPtr GetPtr() { }

    }

    // TypeToken: 0x2000099  // size: 0x20
    public sealed struct CSharpStructInLua
    {
        // Fields
        public System.Int32 fake_id;  // 0x10
        public System.UInt32 len;  // 0x14
        public System.IntPtr ptr;  // 0x18

    }

    // TypeToken: 0x200009A  // size: 0x20
    public sealed struct LuaCSFunctionPtr
    {
        // Fields
        public System.IntPtr ptr;  // 0x10
        public System.Object target;  // 0x18

    }

    // TypeToken: 0x200009B
    public sealed class GetDelegatePointer`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000235
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000236
        public virtual System.ValueTuple<System.IntPtr,T1> Invoke(System.Delegate d) { }
        // RVA: -1  // runtime  token: 0x6000237
        public virtual System.IAsyncResult BeginInvoke(System.Delegate d, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000238
        public virtual System.ValueTuple<System.IntPtr,T1> EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200009C  // size: 0x80
    public sealed class PushCSObject : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F29480  token: 0x6000239
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600023A
        public virtual System.Void Invoke(System.IntPtr L, System.Object obj) { }
        // RVA: 0x0B8A9154  token: 0x600023B
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Object obj, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600023C
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200009D  // size: 0x80
    public sealed class GetCSObject : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F294F0  token: 0x600023D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x600023E
        public virtual System.Object Invoke(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A74D0  token: 0x600023F
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Int32 idx, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x6000240
        public virtual System.Object EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200009E  // size: 0x80
    public sealed class UpdateCSObject : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F29560  token: 0x6000241
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x6000242
        public virtual System.Void Invoke(System.IntPtr L, System.Int32 idx, System.Object obj) { }
        // RVA: 0x0B8AE2B8  token: 0x6000243
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Int32 idx, System.Object obj, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000244
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200009F
    public sealed class CheckFunc`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000245
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000246
        public virtual System.Boolean Invoke(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // runtime  token: 0x6000247
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Int32 idx, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000248
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000A0
    public sealed class GetFunc`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000249
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600024A
        public virtual System.Void Invoke(System.IntPtr L, System.Int32 idx, T& val) { }
        // RVA: -1  // runtime  token: 0x600024B
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Int32 idx, T& val, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x600024C
        public virtual System.Void EndInvoke(T& val, System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000A1  // size: 0x20
    public sealed class <>c__DisplayClass32_0
    {
        // Fields
        public System.Type delegateType;  // 0x10
        public System.Reflection.MethodInfo genericMethodInfo;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600024D
        public System.Void .ctor() { }
        // RVA: 0x0B8AE2A8  token: 0x600024E
        private System.Delegate <getCreatorUsingGeneric>b__7(XLua.DelegateBridgeBase o) { }

    }

    // TypeToken: 0x20000A2  // size: 0x20
    public sealed class <>c__DisplayClass32_1
    {
        // Fields
        public System.Reflection.MethodInfo methodInfo;  // 0x10
        public XLua.ObjectTranslator.<>c__DisplayClass32_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600024F
        public System.Void .ctor() { }
        // RVA: 0x035D0700  token: 0x6000250
        private System.Delegate <getCreatorUsingGeneric>b__8(XLua.DelegateBridgeBase o) { }

    }

    // TypeToken: 0x20000A3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly XLua.ObjectTranslator.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__32_1;  // static @ 0x8
        public static System.Func<System.Reflection.MethodInfo,System.Int32> <>9__32_2;  // static @ 0x10
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__32_3;  // static @ 0x18
        public static System.Func<System.Reflection.MethodInfo,System.Int32> <>9__32_4;  // static @ 0x20
        public static System.Func<XLua.DelegateBridgeBase,System.Delegate> <>9__32_0;  // static @ 0x28
        public static System.Func<XLua.DelegateBridgeBase,System.Delegate> <>9__32_5;  // static @ 0x30
        public static System.Func<System.Reflection.ParameterInfo,System.Type> <>9__32_6;  // static @ 0x38
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__33_0;  // static @ 0x40

        // Methods
        // RVA: 0x04D47950  token: 0x6000251
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000252
        public System.Void .ctor() { }
        // RVA: 0x048E05D0  token: 0x6000253
        private System.Boolean <getCreatorUsingGeneric>b__32_1(System.Reflection.MethodInfo m) { }
        // RVA: 0x03E647D0  token: 0x6000254
        private System.Int32 <getCreatorUsingGeneric>b__32_2(System.Reflection.MethodInfo m) { }
        // RVA: 0x048E0570  token: 0x6000255
        private System.Boolean <getCreatorUsingGeneric>b__32_3(System.Reflection.MethodInfo m) { }
        // RVA: 0x03E647D0  token: 0x6000256
        private System.Int32 <getCreatorUsingGeneric>b__32_4(System.Reflection.MethodInfo m) { }
        // RVA: 0x011EC580  token: 0x6000257
        private System.Delegate <getCreatorUsingGeneric>b__32_0(XLua.DelegateBridgeBase x) { }
        // RVA: 0x011EC580  token: 0x6000258
        private System.Delegate <getCreatorUsingGeneric>b__32_5(XLua.DelegateBridgeBase x) { }
        // RVA: 0x04CA3150  token: 0x6000259
        private System.Type <getCreatorUsingGeneric>b__32_6(System.Reflection.ParameterInfo pinfo) { }
        // RVA: 0x0448D790  token: 0x600025A
        private System.Boolean <getDelegate>b__33_0(System.Reflection.MethodInfo m) { }

    }

    // TypeToken: 0x20000A4  // size: 0x18
    public sealed class <>c__DisplayClass33_0
    {
        // Fields
        public System.Type delegateType;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600025B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A5  // size: 0x20
    public sealed class <>c__DisplayClass33_1
    {
        // Fields
        public System.Reflection.MethodInfo foundMethod;  // 0x10
        public XLua.ObjectTranslator.<>c__DisplayClass33_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600025C
        public System.Void .ctor() { }
        // RVA: 0x035D0700  token: 0x600025D
        private System.Delegate <getDelegate>b__1(XLua.DelegateBridgeBase o) { }

    }

    // TypeToken: 0x20000A6
    public sealed class <>c__123`1
    {
        // Fields
        public static readonly XLua.ObjectTranslator.<>c__123<T> <>9;  // static @ 0x0
        public static System.Action<System.IntPtr,System.Byte> <>9__123_0;  // static @ 0x0
        public static System.Action<System.IntPtr,System.SByte> <>9__123_1;  // static @ 0x0
        public static System.Action<System.IntPtr,System.Char> <>9__123_2;  // static @ 0x0
        public static System.Action<System.IntPtr,System.Int16> <>9__123_3;  // static @ 0x0
        public static System.Action<System.IntPtr,System.UInt16> <>9__123_4;  // static @ 0x0
        public static System.Action<System.IntPtr,System.Single> <>9__123_5;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600025E
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600025F
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000260
        private System.Void <tryGetPushFuncByType>b__123_0(System.IntPtr L, System.Byte v) { }
        // RVA: -1  // not resolved  token: 0x6000261
        private System.Void <tryGetPushFuncByType>b__123_1(System.IntPtr L, System.SByte v) { }
        // RVA: -1  // not resolved  token: 0x6000262
        private System.Void <tryGetPushFuncByType>b__123_2(System.IntPtr L, System.Char v) { }
        // RVA: -1  // not resolved  token: 0x6000263
        private System.Void <tryGetPushFuncByType>b__123_3(System.IntPtr L, System.Int16 v) { }
        // RVA: -1  // not resolved  token: 0x6000264
        private System.Void <tryGetPushFuncByType>b__123_4(System.IntPtr L, System.UInt16 v) { }
        // RVA: -1  // not resolved  token: 0x6000265
        private System.Void <tryGetPushFuncByType>b__123_5(System.IntPtr L, System.Single v) { }

    }

    // TypeToken: 0x20000A7
    public sealed class <>c__125`1
    {
        // Fields
        public static readonly XLua.ObjectTranslator.<>c__125<T> <>9;  // static @ 0x0
        public static System.Func<System.IntPtr,System.Int32,System.Byte> <>9__125_1;  // static @ 0x0
        public static System.Func<System.IntPtr,System.Int32,System.SByte> <>9__125_2;  // static @ 0x0
        public static System.Func<System.IntPtr,System.Int32,System.Char> <>9__125_3;  // static @ 0x0
        public static System.Func<System.IntPtr,System.Int32,System.Int16> <>9__125_4;  // static @ 0x0
        public static System.Func<System.IntPtr,System.Int32,System.UInt16> <>9__125_5;  // static @ 0x0
        public static System.Func<System.IntPtr,System.Int32,System.Single> <>9__125_6;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000266
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000267
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000268
        private System.Byte <tryGetGetFuncByType>b__125_1(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // not resolved  token: 0x6000269
        private System.SByte <tryGetGetFuncByType>b__125_2(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // not resolved  token: 0x600026A
        private System.Char <tryGetGetFuncByType>b__125_3(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // not resolved  token: 0x600026B
        private System.Int16 <tryGetGetFuncByType>b__125_4(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // not resolved  token: 0x600026C
        private System.UInt16 <tryGetGetFuncByType>b__125_5(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // not resolved  token: 0x600026D
        private System.Single <tryGetGetFuncByType>b__125_6(System.IntPtr L, System.Int32 idx) { }

    }

    // TypeToken: 0x20000A8
    public sealed class <>c__DisplayClass128_0`1
    {
        // Fields
        public XLua.ObjectTranslator.GetFunc<T> get;  // 0x0
        public System.Action<System.IntPtr,T> push;  // 0x0
        public System.Action<System.IntPtr,System.Int32,T> update;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600026E
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600026F
        private T <RegisterPushAndGetAndUpdate>b__0(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // not resolved  token: 0x6000270
        private System.Void <RegisterPushAndGetAndUpdate>b__1(System.IntPtr L, System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000271
        private System.Object <RegisterPushAndGetAndUpdate>b__2(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // not resolved  token: 0x6000272
        private System.Void <RegisterPushAndGetAndUpdate>b__3(System.IntPtr L, System.Int32 idx, System.Object obj) { }

    }

    // TypeToken: 0x20000A9
    public sealed class <>c__DisplayClass129_0`1
    {
        // Fields
        public XLua.ObjectTranslator.CheckFunc<T> check;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000273
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000274
        private System.Boolean <RegisterChecker>b__0(System.IntPtr L, System.Int32 idx) { }

    }

    // TypeToken: 0x20000AA
    public sealed class <>c__DisplayClass130_0`1
    {
        // Fields
        public XLua.ObjectTranslator.GetFunc<T> get;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000275
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000276
        private System.Object <RegisterCaster>b__0(System.IntPtr L, System.Int32 idx, System.Object o) { }

    }

    // TypeToken: 0x20000B3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly XLua.TypeExtensions.<>c <>9;  // static @ 0x0
        public static System.Func<System.Type,System.String> <>9__12_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B8B420C  token: 0x60002C7
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60002C8
        public System.Void .ctor() { }
        // RVA: 0x0B8B1A80  token: 0x60002C9
        private System.String <GetFriendlyName>b__12_0(System.Type x) { }

    }

    // TypeToken: 0x20000B6  // size: 0x20
    public sealed struct MethodKey
    {
        // Fields
        public System.String Name;  // 0x10
        public System.Boolean IsStatic;  // 0x18

    }

    // TypeToken: 0x20000B7  // size: 0x20
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Boolean exclude_generic_definition;  // 0x10
        public System.Func<System.Type,System.Boolean> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002F0
        public System.Void .ctor() { }
        // RVA: 0x0B8B36D0  token: 0x60002F1
        private System.Boolean <GetAllTypes>b__0(System.Type type) { }

    }

    // TypeToken: 0x20000B8  // size: 0x20
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.Reflection.FieldInfo field;  // 0x10
        public System.Type type;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002F2
        public System.Void .ctor() { }
        // RVA: 0x0B8B36F4  token: 0x60002F3
        private System.Int32 <genFieldGetter>b__0(System.IntPtr L) { }
        // RVA: 0x0B8B3794  token: 0x60002F4
        private System.Int32 <genFieldGetter>b__1(System.IntPtr L) { }

    }

    // TypeToken: 0x20000B9  // size: 0x18
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public System.Reflection.FieldInfo field;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002F5
        public System.Void .ctor() { }
        // RVA: 0x035944C0  token: 0x60002F6
        private System.Int32 <genFieldGetterByPointer>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000BA  // size: 0x20
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Reflection.FieldInfo field;  // 0x10
        public System.Type type;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002F7
        public System.Void .ctor() { }
        // RVA: 0x0B8B3B60  token: 0x60002F8
        private System.Int32 <genFieldSetter>b__0(System.IntPtr L) { }
        // RVA: 0x0B8B3DD0  token: 0x60002F9
        private System.Int32 <genFieldSetter>b__1(System.IntPtr L) { }

    }

    // TypeToken: 0x20000BB  // size: 0x20
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public System.Reflection.FieldInfo field;  // 0x10
        public System.Type type;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002FA
        public System.Void .ctor() { }
        // RVA: 0x049B9E60  token: 0x60002FB
        private System.Int32 <genFieldSetterByPointer>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000BC  // size: 0x30
    public sealed class <>c__DisplayClass9_0
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Reflection.PropertyInfo[] props;  // 0x18
        public System.Type[] params_type;  // 0x20
        public XLua.MethodWrap[] methods;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x60002FC
        public System.Void .ctor() { }
        // RVA: 0x038EDD30  token: 0x60002FD
        private System.Int32 <genItemGetterOpt>b__1(System.IntPtr L) { }

    }

    // TypeToken: 0x20000BD  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly XLua.Utils.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.PropertyInfo,System.Boolean> <>9__9_0;  // static @ 0x8
        public static System.Func<System.Reflection.PropertyInfo,System.Boolean> <>9__10_0;  // static @ 0x10
        public static System.Func<System.Reflection.PropertyInfo,System.Boolean> <>9__11_0;  // static @ 0x18
        public static System.Func<System.Reflection.PropertyInfo,System.Boolean> <>9__12_0;  // static @ 0x20
        public static System.Func<System.Type,System.Boolean> <>9__14_7;  // static @ 0x28
        public static System.Func<System.Type,System.Boolean> <>9__14_8;  // static @ 0x30
        public static System.Func<System.Type,System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>> <>9__14_0;  // static @ 0x38
        public static System.Func<System.Type,System.Reflection.MethodInfo,<>f__AnonymousType1<System.Type,System.Reflection.MethodInfo>> <>9__14_1;  // static @ 0x40
        public static System.Func<<>f__AnonymousType1<System.Type,System.Reflection.MethodInfo>,System.Boolean> <>9__14_2;  // static @ 0x48
        public static System.Func<<>f__AnonymousType1<System.Type,System.Reflection.MethodInfo>,System.Type> <>9__14_3;  // static @ 0x50
        public static System.Func<<>f__AnonymousType1<System.Type,System.Reflection.MethodInfo>,System.Reflection.MethodInfo> <>9__14_4;  // static @ 0x58
        public static System.Func<System.Linq.IGrouping<System.Type,System.Reflection.MethodInfo>,System.Type> <>9__14_5;  // static @ 0x60
        public static System.Func<System.Linq.IGrouping<System.Type,System.Reflection.MethodInfo>,System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>> <>9__14_6;  // static @ 0x68
        public static System.Func<System.Reflection.MethodInfo,System.String> <>9__16_0;  // static @ 0x70
        public static System.Func<System.Reflection.MethodInfo,System.String> <>9__16_1;  // static @ 0x78
        public static System.Func<System.Reflection.MethodInfo,System.Reflection.MethodInfo,System.Reflection.MethodInfo> <>9__16_2;  // static @ 0x80

        // Methods
        // RVA: 0x04D41AD0  token: 0x60002FE
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60002FF
        public System.Void .ctor() { }
        // RVA: 0x044E1A70  token: 0x6000300
        private System.Boolean <genItemGetterOpt>b__9_0(System.Reflection.PropertyInfo prop) { }
        // RVA: 0x0B8B1D44  token: 0x6000301
        private System.Boolean <genItemGetter>b__10_0(System.Reflection.PropertyInfo prop) { }
        // RVA: 0x044E19B0  token: 0x6000302
        private System.Boolean <genItemSetterOpt>b__11_0(System.Reflection.PropertyInfo prop) { }
        // RVA: 0x0B8B1DF0  token: 0x6000303
        private System.Boolean <genItemSetter>b__12_0(System.Reflection.PropertyInfo prop) { }
        // RVA: 0x0B8B1988  token: 0x6000304
        private System.Boolean <GetExtensionMethodsOf>b__14_7(System.Type t) { }
        // RVA: 0x0B8B1A04  token: 0x6000305
        private System.Boolean <GetExtensionMethodsOf>b__14_8(System.Type t) { }
        // RVA: 0x0B8B1740  token: 0x6000306
        private System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> <GetExtensionMethodsOf>b__14_0(System.Type type) { }
        // RVA: 0x0B8B1760  token: 0x6000307
        private <>f__AnonymousType1<System.Type,System.Reflection.MethodInfo> <GetExtensionMethodsOf>b__14_1(System.Type type, System.Reflection.MethodInfo method) { }
        // RVA: 0x0B8B17E0  token: 0x6000308
        private System.Boolean <GetExtensionMethodsOf>b__14_2(<>f__AnonymousType1<System.Type,System.Reflection.MethodInfo> <>h__TransparentIdentifier0) { }
        // RVA: 0x0B8B18A8  token: 0x6000309
        private System.Type <GetExtensionMethodsOf>b__14_3(<>f__AnonymousType1<System.Type,System.Reflection.MethodInfo> <>h__TransparentIdentifier0) { }
        // RVA: 0x0B8B1904  token: 0x600030A
        private System.Reflection.MethodInfo <GetExtensionMethodsOf>b__14_4(<>f__AnonymousType1<System.Type,System.Reflection.MethodInfo> <>h__TransparentIdentifier0) { }
        // RVA: 0x0B8B1940  token: 0x600030B
        private System.Type <GetExtensionMethodsOf>b__14_5(System.Linq.IGrouping<System.Type,System.Reflection.MethodInfo> g) { }
        // RVA: 0x04D88C30  token: 0x600030C
        private System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> <GetExtensionMethodsOf>b__14_6(System.Linq.IGrouping<System.Type,System.Reflection.MethodInfo> g) { }
        // RVA: 0x0B8B1E9C  token: 0x600030D
        private System.String <makeReflectionWrap>b__16_0(System.Reflection.MethodInfo p) { }
        // RVA: 0x0B8B1E9C  token: 0x600030E
        private System.String <makeReflectionWrap>b__16_1(System.Reflection.MethodInfo q) { }
        // RVA: 0x04D88C30  token: 0x600030F
        private System.Reflection.MethodInfo <makeReflectionWrap>b__16_2(System.Reflection.MethodInfo p, System.Reflection.MethodInfo q) { }

    }

    // TypeToken: 0x20000BE  // size: 0x30
    public sealed class <>c__DisplayClass10_0
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Reflection.PropertyInfo[] props;  // 0x18
        public System.Type[] params_type;  // 0x20
        public System.Object[] arg;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000310
        public System.Void .ctor() { }
        // RVA: 0x0B8B1EBC  token: 0x6000311
        private System.Int32 <genItemGetter>b__1(System.IntPtr L) { }

    }

    // TypeToken: 0x20000BF  // size: 0x30
    public sealed class <>c__DisplayClass11_0
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Reflection.PropertyInfo[] props;  // 0x18
        public System.Type[] params_type;  // 0x20
        public XLua.MethodWrap[] methods;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000312
        public System.Void .ctor() { }
        // RVA: 0x0B8B2410  token: 0x6000313
        private System.Int32 <genItemSetterOpt>b__1(System.IntPtr L) { }

    }

    // TypeToken: 0x20000C0  // size: 0x30
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Reflection.PropertyInfo[] props;  // 0x18
        public System.Type[] params_type;  // 0x20
        public System.Object[] arg;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000314
        public System.Void .ctor() { }
        // RVA: 0x0B8B291C  token: 0x6000315
        private System.Int32 <genItemSetter>b__1(System.IntPtr L) { }

    }

    // TypeToken: 0x20000C1  // size: 0x18
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public System.Type type;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000316
        public System.Void .ctor() { }
        // RVA: 0x0B8B2FA4  token: 0x6000317
        private System.Int32 <genEnumCastFrom>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000C2  // size: 0x18
    public sealed class <>c__DisplayClass16_0
    {
        // Fields
        public System.String fieldName;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000318
        public System.Void .ctor() { }
        // RVA: 0x04CE1D50  token: 0x6000319
        private System.Boolean <makeReflectionWrap>b__3(System.Reflection.EventInfo e) { }

    }

    // TypeToken: 0x20000C3  // size: 0x18
    public sealed class <>c__DisplayClass20_0
    {
        // Fields
        public System.String memberName;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600031A
        public System.Void .ctor() { }
        // RVA: 0x0A99AD6C  token: 0x600031B
        private System.Boolean <LazyReflectionCall>b__0(System.Reflection.MethodInfo m) { }

    }

    // TypeToken: 0x20000C4  // size: 0x18
    public sealed class <>c__DisplayClass21_0
    {
        // Fields
        public System.Type type;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600031C
        public System.Void .ctor() { }
        // RVA: 0x0B8B3638  token: 0x600031D
        private System.Int32 <ReflectionWrap>b__0(System.IntPtr LL) { }

    }

    // TypeToken: 0x20000C9  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly XLua.TemplateEngine.Parser.<>c <>9;  // static @ 0x0
        public static System.Func<System.Text.RegularExpressions.Capture,<>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32>> <>9__7_0;  // static @ 0x8
        public static System.Func<System.Text.RegularExpressions.Capture,<>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32>> <>9__7_1;  // static @ 0x10
        public static System.Func<System.Text.RegularExpressions.Capture,<>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32>> <>9__7_2;  // static @ 0x18
        public static System.Func<<>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32>,System.Int32> <>9__7_3;  // static @ 0x20
        public static System.Func<<>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32>,XLua.TemplateEngine.Chunk> <>9__7_4;  // static @ 0x28

        // Methods
        // RVA: 0x0B8B41A8  token: 0x600032B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600032C
        public System.Void .ctor() { }
        // RVA: 0x0B8B1A8C  token: 0x600032D
        private <>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32> <Parse>b__7_0(System.Text.RegularExpressions.Capture p) { }
        // RVA: 0x0B8B1B24  token: 0x600032E
        private <>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32> <Parse>b__7_1(System.Text.RegularExpressions.Capture p) { }
        // RVA: 0x0B8B1BE4  token: 0x600032F
        private <>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32> <Parse>b__7_2(System.Text.RegularExpressions.Capture p) { }
        // RVA: 0x0B8B1C80  token: 0x6000330
        private System.Int32 <Parse>b__7_3(<>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32> p) { }
        // RVA: 0x0B8B1CBC  token: 0x6000331
        private XLua.TemplateEngine.Chunk <Parse>b__7_4(<>f__AnonymousType0<XLua.TemplateEngine.TokenType,System.String,System.Int32> m) { }

    }

    // TypeToken: 0x20000CC
    public sealed class <>c__DisplayClass3_0`1
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x0
        public System.Reflection.FieldInfo field;  // 0x0
        public System.Type type;  // 0x0
        public System.Int32 offset;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000351
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000352
        private System.Int32 <GetObjFieldGetterByPushInt32>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000CD  // size: 0x30
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000353
        public System.Void .ctor() { }
        // RVA: 0x03FDBC00  token: 0x6000354
        private System.Int32 <GetObjFloatFieldGetterByPushNumber>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000CE  // size: 0x30
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000355
        public System.Void .ctor() { }
        // RVA: 0x0B8B39E4  token: 0x6000356
        private System.Int32 <GetObjDoubleFieldGetterByPushNumber>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000CF
    public sealed class <>c__DisplayClass6_0`1
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x0
        public System.Reflection.FieldInfo field;  // 0x0
        public System.Type type;  // 0x0
        public System.Int32 offset;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000357
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000358
        private System.Int32 <GetObjFieldGetterByPushInt64>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D0  // size: 0x30
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000359
        public System.Void .ctor() { }
        // RVA: 0x03DC3010  token: 0x600035A
        private System.Int32 <GetObjFieldGetterByPushBool>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D1  // size: 0x30
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600035B
        public System.Void .ctor() { }
        // RVA: 0x03C33910  token: 0x600035C
        private System.Int32 <GetObjFieldGetterByPushString>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D2  // size: 0x30
    public sealed class <>c__DisplayClass9_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600035D
        public System.Void .ctor() { }
        // RVA: 0x0329CCE0  token: 0x600035E
        private System.Int32 <GetObjFieldObjGetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D3  // size: 0x40
    public sealed class <>c__DisplayClass10_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28
        public System.Boolean isNullable;  // 0x2c
        public System.Type filedType;  // 0x30
        public System.Int32 size;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x600035F
        public System.Void .ctor() { }
        // RVA: 0x03E85C70  token: 0x6000360
        private System.Int32 <GetObjFieldStructGetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D4  // size: 0x28
    public sealed class <>c__DisplayClass11_0
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Int32 offset;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000361
        public System.Void .ctor() { }
        // RVA: 0x036FD950  token: 0x6000362
        private System.Int32 <GetObjFieldEnumGetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D5
    public sealed class <>c__DisplayClass12_0`1
    {
        // Fields
        public System.Type type;  // 0x0
        public System.Reflection.FieldInfo field;  // 0x0
        public System.Int32 offset;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000363
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000364
        private System.Int32 <GetObjFieldStructGetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D6  // size: 0x20
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public System.Reflection.FieldInfo field;  // 0x10
        public System.Type type;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000365
        public System.Void .ctor() { }
        // RVA: 0x036FD840  token: 0x6000366
        private System.Int32 <GetObjFieldObjSetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D7  // size: 0x38
    public sealed class <>c__DisplayClass14_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28
        public System.Boolean isNullable;  // 0x2c
        public System.Int32 size;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x6000367
        public System.Void .ctor() { }
        // RVA: 0x044AC740  token: 0x6000368
        private System.Int32 <GetObjFieldStructSetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D8
    public sealed class <>c__DisplayClass15_0`1
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x0
        public System.Reflection.FieldInfo field;  // 0x0
        public System.Type type;  // 0x0
        public System.Int32 offset;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000369
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600036A
        private System.Int32 <GetObjFieldSetterByGetInt32>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000D9
    public sealed class <>c__DisplayClass16_0`1
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x0
        public System.Reflection.FieldInfo field;  // 0x0
        public System.Type type;  // 0x0
        public System.Int32 offset;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600036B
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600036C
        private System.Int32 <GetObjFieldSetterByGetInt64>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000DA  // size: 0x30
    public sealed class <>c__DisplayClass17_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600036D
        public System.Void .ctor() { }
        // RVA: 0x049AEF10  token: 0x600036E
        private System.Int32 <GetObjFloatFieldSetterByGetNumber>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000DB  // size: 0x30
    public sealed class <>c__DisplayClass18_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600036F
        public System.Void .ctor() { }
        // RVA: 0x0B8B3150  token: 0x6000370
        private System.Int32 <GetObjDoubleFieldSetterByGetNumber>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000DC  // size: 0x30
    public sealed class <>c__DisplayClass19_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000371
        public System.Void .ctor() { }
        // RVA: 0x041F2450  token: 0x6000372
        private System.Int32 <GetObjFieldBoolSetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000DD  // size: 0x30
    public sealed class <>c__DisplayClass20_0
    {
        // Fields
        public System.Boolean isUnmanagedType;  // 0x10
        public System.Reflection.FieldInfo field;  // 0x18
        public System.Type type;  // 0x20
        public System.Int32 offset;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000373
        public System.Void .ctor() { }
        // RVA: 0x0B8B32BC  token: 0x6000374
        private System.Int32 <GetObjFieldEnumSetter>b__0(System.IntPtr L) { }

    }

    // TypeToken: 0x20000EC  // size: 0x80
    public sealed class HyperLuaLogCallbackDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04CF29E0  token: 0x6000426
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x6000427
        public virtual System.Void Invoke(System.IntPtr L, System.Int32 level, System.String text) { }
        // RVA: 0x0B8AE4C8  token: 0x6000428
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Int32 level, System.String text, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000429
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000ED  // size: 0x80
    public sealed class LuaUtilsLogCallbackDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04CF27E0  token: 0x600042A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600042B
        public virtual System.Void Invoke(System.Int32 level, System.String text) { }
        // RVA: 0x0B8AFC7C  token: 0x600042C
        public virtual System.IAsyncResult BeginInvoke(System.Int32 level, System.String text, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600042D
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000EE  // size: 0x80
    public sealed class DecryptionFunc : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04CF4260  token: 0x600042E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600042F
        public virtual System.IntPtr Invoke(System.IntPtr input, System.Int32 size, System.Int32& outSize) { }
        // RVA: 0x0B8AE3D8  token: 0x6000430
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr input, System.Int32 size, System.Int32& outSize, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD2D78  token: 0x6000431
        public virtual System.IntPtr EndInvoke(System.Int32& outSize, System.IAsyncResult result) { }

    }

namespace Beyond.Lua
{

    // TypeToken: 0x200004C  // size: 0x10
    public class LuaCypher
    {
        // Fields
        private static System.String KEY;  // const
        private static readonly System.String[] KEYS;  // static @ 0x0
        private static System.String INITIAL_ASSETS_ROOT_PATH;  // const
        private static readonly Unity.Profiling.ProfilerMarker decryptionMaker;  // static @ 0x8

        // Methods
        // RVA: 0x0B89FE54  token: 0x600004F
        private static System.String GetKey() { }
        // RVA: 0x0B89FBE8  token: 0x6000050
        private static System.Void GetKey(System.Span<System.Byte> buffer, System.Int32& len) { }
        // RVA: 0x0B8A0184  token: 0x6000051
        protected static System.Boolean SkipDecrypt(System.String luaString) { }
        // RVA: 0x0B89F3D0  token: 0x6000052
        public static System.String DecryptLuaString(System.String encryptLua) { }
        // RVA: 0x0B89F46C  token: 0x6000053
        public static System.Byte[] DecryptLua(System.String encryptLua) { }
        // RVA: 0x038F0A60  token: 0x6000054
        public static System.IntPtr DecryptionFunc(System.IntPtr input, System.Int32 size, System.Int32& outSize) { }
        // RVA: 0x041E1670  token: 0x6000055
        public System.Void .ctor() { }
        // RVA: 0x049D9E80  token: 0x6000056
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004D  // size: 0x10
    public class LuaGenericDelegateCaller
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.String,Beyond.Lua.LuaGenericDelegateCaller.LuaGenericDelegateCall> s_callerDic;  // static @ 0x0

        // Methods
        // RVA: 0x038EDEB0  token: 0x6000057
        public static Beyond.Lua.LuaGenericDelegateCaller.LuaGenericDelegateCall GetCaller(System.String delegateTypeName) { }
        // RVA: 0x038EE6A0  token: 0x6000058
        public static System.Void AddCaller(System.String delegateTypeName, Beyond.Lua.LuaGenericDelegateCaller.LuaGenericDelegateCall caller) { }
        // RVA: 0x0329ABB0  token: 0x6000059
        public static System.Int32 OverloadMethodCall(System.IntPtr L, XLua.ObjectTranslator objectTranslator, XLua.IOverloadMethodWrap[] overload) { }
        // RVA: 0x038EE200  token: 0x600005A
        public static System.Int32 DefaultValueMethodCall(System.IntPtr L, XLua.ObjectTranslator objectTranslator, XLua.IOverloadMethodWrap[] defaultValues) { }
        // RVA: 0x041E1670  token: 0x600005B
        public System.Void .ctor() { }
        // RVA: 0x04D179A0  token: 0x600005C
        private static System.Void .cctor() { }

    }

}

namespace Beyond.Reflection
{

    // TypeToken: 0x2000008  // size: 0x10
    public class GenericDelegateCaller
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.String,System.Type> s_customDelegateTypeDic;  // static @ 0x0

        // Methods
        // RVA: 0x038F13E0  token: 0x6000032
        public static System.Type GetCustomDelegateType(System.String delegateName) { }
        // RVA: 0x038EE520  token: 0x6000033
        public static System.Void AddCustomDelegateType(System.String delegateName, System.Type type) { }
        // RVA: 0x041E1670  token: 0x6000034
        public System.Void .ctor() { }
        // RVA: 0x045B1A60  token: 0x6000035
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x40
    public class MethodParamItem
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Boolean isRef;  // 0x18
        public System.Boolean isOut;  // 0x19
        public System.Object defaultValue;  // 0x20
        public System.Boolean hasDefaultValue;  // 0x28
        public System.String name;  // 0x30
        public System.Type realType;  // 0x38

        // Methods
        // RVA: 0x02F2D880  token: 0x6000036
        public static System.Collections.Generic.List<Beyond.Reflection.MethodParamItem> GetMethodParamList(System.Reflection.MethodInfo method, System.Boolean needLua, System.Boolean needEnum) { }
        // RVA: 0x02F2B440  token: 0x6000037
        public static System.String GetMethodSignatureString(System.Reflection.MethodInfo method, System.Boolean needLua, System.Boolean keepType, System.Boolean needEnum) { }
        // RVA: 0x041E1670  token: 0x6000038
        public System.Void .ctor() { }

    }

}

namespace Beyond.Reflection.StructSizeGen
{

    // TypeToken: 0x200000A  // size: 0x11
    public sealed struct FStructSize0
    {
    }

    // TypeToken: 0x200000B  // size: 0x11
    public sealed struct FStructSize1
    {
        // Fields
        public System.Byte b0;  // 0x10

    }

    // TypeToken: 0x200000C  // size: 0x12
    public sealed struct FStructSize2
    {
        // Fields
        public System.Int16 s0;  // 0x10

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct FStructSize3
    {
        // Fields
        public System.Int16 s0;  // 0x10
        public System.Byte b0;  // 0x12

    }

    // TypeToken: 0x200000E  // size: 0x14
    public sealed struct FStructSize4
    {
        // Fields
        public System.Int32 i0;  // 0x10

    }

    // TypeToken: 0x200000F  // size: 0x18
    public sealed struct FStructSize5
    {
        // Fields
        public System.Int32 i0;  // 0x10
        public System.Byte b0;  // 0x14

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed struct FStructSize8
    {
        // Fields
        public System.Int64 l0;  // 0x10

    }

    // TypeToken: 0x2000011  // size: 0x20
    public sealed struct FStructSize12
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int32 i0;  // 0x18

    }

    // TypeToken: 0x2000012  // size: 0x20
    public sealed struct FStructSize16
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18

    }

    // TypeToken: 0x2000013  // size: 0x28
    public sealed struct FStructSize19
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int16 s0;  // 0x20
        public System.Byte b0;  // 0x22

    }

    // TypeToken: 0x2000014  // size: 0x28
    public sealed struct FStructSize20
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int32 i0;  // 0x20

    }

    // TypeToken: 0x2000015  // size: 0x28
    public sealed struct FStructSize24
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20

    }

    // TypeToken: 0x2000016  // size: 0x30
    public sealed struct FStructSize28
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int32 i0;  // 0x28

    }

    // TypeToken: 0x2000017  // size: 0x30
    public sealed struct FStructSize32
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28

    }

    // TypeToken: 0x2000018  // size: 0x38
    public sealed struct FStructSize34
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int16 s0;  // 0x30

    }

    // TypeToken: 0x2000019  // size: 0x38
    public sealed struct FStructSize36
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int32 i0;  // 0x30

    }

    // TypeToken: 0x200001A  // size: 0x38
    public sealed struct FStructSize40
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30

    }

    // TypeToken: 0x200001B  // size: 0x40
    public sealed struct FStructSize44
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int32 i0;  // 0x38

    }

    // TypeToken: 0x200001C  // size: 0x40
    public sealed struct FStructSize48
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38

    }

    // TypeToken: 0x200001D  // size: 0x48
    public sealed struct FStructSize52
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int32 i0;  // 0x40

    }

    // TypeToken: 0x200001E  // size: 0x48
    public sealed struct FStructSize56
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40

    }

    // TypeToken: 0x200001F  // size: 0x50
    public sealed struct FStructSize60
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int32 i0;  // 0x48

    }

    // TypeToken: 0x2000020  // size: 0x50
    public sealed struct FStructSize64
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48

    }

    // TypeToken: 0x2000021  // size: 0x58
    public sealed struct FStructSize68
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int32 i0;  // 0x50

    }

    // TypeToken: 0x2000022  // size: 0x58
    public sealed struct FStructSize72
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50

    }

    // TypeToken: 0x2000023  // size: 0x60
    public sealed struct FStructSize76
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int32 i0;  // 0x58

    }

    // TypeToken: 0x2000024  // size: 0x60
    public sealed struct FStructSize80
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58

    }

    // TypeToken: 0x2000025  // size: 0x68
    public sealed struct FStructSize84
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int32 i0;  // 0x60

    }

    // TypeToken: 0x2000026  // size: 0x68
    public sealed struct FStructSize88
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60

    }

    // TypeToken: 0x2000027  // size: 0x70
    public sealed struct FStructSize92
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int32 i0;  // 0x68

    }

    // TypeToken: 0x2000028  // size: 0x70
    public sealed struct FStructSize96
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68

    }

    // TypeToken: 0x2000029  // size: 0x78
    public sealed struct FStructSize104
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70

    }

    // TypeToken: 0x200002A  // size: 0x80
    public sealed struct FStructSize108
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int32 i0;  // 0x78

    }

    // TypeToken: 0x200002B  // size: 0x80
    public sealed struct FStructSize112
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78

    }

    // TypeToken: 0x200002C  // size: 0x88
    public sealed struct FStructSize116
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int32 i0;  // 0x80

    }

    // TypeToken: 0x200002D  // size: 0x88
    public sealed struct FStructSize120
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80

    }

    // TypeToken: 0x200002E  // size: 0x90
    public sealed struct FStructSize124
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int32 i0;  // 0x88

    }

    // TypeToken: 0x200002F  // size: 0x90
    public sealed struct FStructSize128
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88

    }

    // TypeToken: 0x2000030  // size: 0x98
    public sealed struct FStructSize136
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90

    }

    // TypeToken: 0x2000031  // size: 0xA0
    public sealed struct FStructSize140
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int32 i0;  // 0x98

    }

    // TypeToken: 0x2000032  // size: 0xA0
    public sealed struct FStructSize144
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98

    }

    // TypeToken: 0x2000033  // size: 0xA8
    public sealed struct FStructSize148
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int32 i0;  // 0xa0

    }

    // TypeToken: 0x2000034  // size: 0xA8
    public sealed struct FStructSize152
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0

    }

    // TypeToken: 0x2000035  // size: 0xB0
    public sealed struct FStructSize160
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8

    }

    // TypeToken: 0x2000036  // size: 0xB8
    public sealed struct FStructSize164
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int32 i0;  // 0xb0

    }

    // TypeToken: 0x2000037  // size: 0xB8
    public sealed struct FStructSize168
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0

    }

    // TypeToken: 0x2000038  // size: 0xC0
    public sealed struct FStructSize176
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8

    }

    // TypeToken: 0x2000039  // size: 0xC8
    public sealed struct FStructSize180
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int32 i0;  // 0xc0

    }

    // TypeToken: 0x200003A  // size: 0xC8
    public sealed struct FStructSize184
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0

    }

    // TypeToken: 0x200003B  // size: 0xD0
    public sealed struct FStructSize192
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8

    }

    // TypeToken: 0x200003C  // size: 0xD8
    public sealed struct FStructSize196
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int32 i0;  // 0xd0

    }

    // TypeToken: 0x200003D  // size: 0xD8
    public sealed struct FStructSize200
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0

    }

    // TypeToken: 0x200003E  // size: 0xE0
    public sealed struct FStructSize208
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0
        public System.Int64 l25;  // 0xd8

    }

    // TypeToken: 0x200003F  // size: 0xF0
    public sealed struct FStructSize220
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0
        public System.Int64 l25;  // 0xd8
        public System.Int64 l26;  // 0xe0
        public System.Int32 i0;  // 0xe8

    }

    // TypeToken: 0x2000040  // size: 0xF0
    public sealed struct FStructSize224
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0
        public System.Int64 l25;  // 0xd8
        public System.Int64 l26;  // 0xe0
        public System.Int64 l27;  // 0xe8

    }

    // TypeToken: 0x2000041  // size: 0xF8
    public sealed struct FStructSize232
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0
        public System.Int64 l25;  // 0xd8
        public System.Int64 l26;  // 0xe0
        public System.Int64 l27;  // 0xe8
        public System.Int64 l28;  // 0xf0

    }

    // TypeToken: 0x2000042  // size: 0x100
    public sealed struct FStructSize240
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0
        public System.Int64 l25;  // 0xd8
        public System.Int64 l26;  // 0xe0
        public System.Int64 l27;  // 0xe8
        public System.Int64 l28;  // 0xf0
        public System.Int64 l29;  // 0xf8

    }

    // TypeToken: 0x2000043  // size: 0x108
    public sealed struct FStructSize248
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0
        public System.Int64 l25;  // 0xd8
        public System.Int64 l26;  // 0xe0
        public System.Int64 l27;  // 0xe8
        public System.Int64 l28;  // 0xf0
        public System.Int64 l29;  // 0xf8
        public System.Int64 l30;  // 0x100

    }

    // TypeToken: 0x2000044  // size: 0x110
    public sealed struct FStructSize256
    {
        // Fields
        public System.Int64 l0;  // 0x10
        public System.Int64 l1;  // 0x18
        public System.Int64 l2;  // 0x20
        public System.Int64 l3;  // 0x28
        public System.Int64 l4;  // 0x30
        public System.Int64 l5;  // 0x38
        public System.Int64 l6;  // 0x40
        public System.Int64 l7;  // 0x48
        public System.Int64 l8;  // 0x50
        public System.Int64 l9;  // 0x58
        public System.Int64 l10;  // 0x60
        public System.Int64 l11;  // 0x68
        public System.Int64 l12;  // 0x70
        public System.Int64 l13;  // 0x78
        public System.Int64 l14;  // 0x80
        public System.Int64 l15;  // 0x88
        public System.Int64 l16;  // 0x90
        public System.Int64 l17;  // 0x98
        public System.Int64 l18;  // 0xa0
        public System.Int64 l19;  // 0xa8
        public System.Int64 l20;  // 0xb0
        public System.Int64 l21;  // 0xb8
        public System.Int64 l22;  // 0xc0
        public System.Int64 l23;  // 0xc8
        public System.Int64 l24;  // 0xd0
        public System.Int64 l25;  // 0xd8
        public System.Int64 l26;  // 0xe0
        public System.Int64 l27;  // 0xe8
        public System.Int64 l28;  // 0xf0
        public System.Int64 l29;  // 0xf8
        public System.Int64 l30;  // 0x100
        public System.Int64 l31;  // 0x108

    }

}

namespace Beyond.Scripts.Lua
{

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct ELuaMultithreadingGCState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Lua.ELuaMultithreadingGCState MinorGC;  // const
        public static Beyond.Scripts.Lua.ELuaMultithreadingGCState MajorGC;  // const

    }

    // TypeToken: 0x2000046  // size: 0x30
    public class OnGCEndEvent : UnityEngine.Events.UnityEvent
    {
        // Methods
        // RVA: 0x05393538  token: 0x6000039
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000047  // size: 0x90
    public class LuaMultithreadingGC
    {
        // Fields
        public static System.Int32 threadGcCount;  // static @ 0x0
        public static System.Int32 mainThreadSignalCount;  // static @ 0x4
        public static System.Int32 mainTheadForceGCCount;  // static @ 0x8
        public static System.Int32 gcMarkCount;  // static @ 0xc
        public static System.Int32 gcSweepCount;  // static @ 0x10
        private System.Threading.Thread m_gcThread;  // 0x10
        private System.Threading.AutoResetEvent m_workSignal;  // 0x18
        private XLua.LuaEnv m_luaEnv;  // 0x20
        private System.Int32 m_lastMajorGCMemory;  // 0x28
        private System.Int32 m_lastMinorGCMemory;  // 0x2c
        private System.Int32 m_badGCCounter;  // 0x30
        private System.Int32 m_badGCThreshold;  // 0x34
        private System.Single m_genGcIncreaseThreshold;  // 0x38
        private System.Single m_genGcIncreaseThresholdDiff;  // 0x3c
        private System.Int32 m_forceGCBaseLine;  // 0x40
        private System.Int32 m_incGCThreshold;  // 0x44
        private System.Int32 m_majorGCFactor;  // 0x48
        private System.Int32 m_forceGCFactor;  // 0x4c
        private Beyond.Scripts.Lua.OnGCEndEvent m_afterGCEvent;  // 0x50
        private System.Collections.Generic.List<UnityEngine.Events.UnityAction> m_afterGCEventOnceEvents;  // 0x58
        public Beyond.Scripts.Lua.ELuaMultithreadingGCState gcState;  // 0x60
        public System.Int64 mainThreadWait;  // 0x68
        public System.Int64 gcThreadStop;  // 0x70
        public System.Boolean stop;  // 0x78
        public System.Boolean isRunning;  // 0x79
        private XLua.LuaDLL.lua_Hook hook;  // 0x80
        private static XLua.LuaDLL.lua_Hook oldHook;  // static @ 0x18
        private static System.Int32 oldHookMask;  // static @ 0x20
        private System.Boolean haveRunOnce;  // 0x88
        private static readonly Unity.Profiling.ProfilerMarker PROFILER_MARKER_START_GC;  // static @ 0x28
        private static readonly Unity.Profiling.ProfilerMarker PROFILER_MARKER_THREAD_NOTIFY;  // static @ 0x30

        // Methods
        // RVA: 0x03B25580  token: 0x600003A
        public System.Void .ctor(XLua.LuaEnv luaEnv) { }
        // RVA: 0x038EE820  token: 0x600003B
        public System.Void StartGCAsync() { }
        // RVA: 0x0B8A11B4  token: 0x600003C
        public System.Void NormalStop() { }
        // RVA: 0x0B8A0FF8  token: 0x600003D
        public System.Void ForceStopAsyncGC() { }
        // RVA: 0x038EF140  token: 0x600003E
        public System.Void MainThreadWait() { }
        // RVA: 0x0488B6A0  token: 0x600003F
        public System.Void AddLuaAfterGCEvent(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0423A4A0  token: 0x6000040
        public System.Void AddLuaAfterGCOnceEvent(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0B8A1254  token: 0x6000041
        public System.Void RemoveLuaAfterGCEvent(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x038EF910  token: 0x6000042
        private System.Void _OnGCEnd() { }
        // RVA: 0x03322B40  token: 0x6000043
        private System.Void _Init() { }
        // RVA: 0x0B8A12BC  token: 0x6000044
        private System.Void _Dispose() { }
        // RVA: 0x03325790  token: 0x6000045
        private static System.Int32 _FindLoopSystemIndex(UnityEngine.LowLevel.PlayerLoopSystem[] playerLoopList, System.String typeName) { }
        // RVA: 0x0B8A1070  token: 0x6000046
        public static System.Int32 LuaHook(System.IntPtr L, XLua.lua_debug& ar) { }
        // RVA: 0x0B8A178C  token: 0x6000047
        private System.Void _UpdateOldHook() { }
        // RVA: 0x038F0A20  token: 0x6000048
        private System.Boolean _DoLock() { }
        // RVA: 0x038F1730  token: 0x6000049
        private System.Boolean _DoGc(System.Int32 work) { }
        // RVA: 0x038EFB20  token: 0x600004A
        private System.Void _GCMethodWrapper() { }
        // RVA: 0x04B84B50  token: 0x600004B
        private static System.Void .cctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x20000F0  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000436
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0B8AE564  token: 0x6000437
        public System.Type __Gen_Wrap_0(System.Object P0) { }
        // RVA: 0x06377B18  token: 0x6000438
        public System.Void __Gen_Wrap_1(System.Object P0, System.Object P1) { }
        // RVA: 0x0B8AECBC  token: 0x6000439
        public System.Collections.Generic.List<Beyond.Reflection.MethodParamItem> __Gen_Wrap_2(System.Object P0, System.Boolean P1, System.Boolean P2) { }
        // RVA: 0x0B8AEDD0  token: 0x600043A
        public System.String __Gen_Wrap_3(System.Object P0, System.Boolean P1, System.Boolean P2, System.Boolean P3) { }
        // RVA: 0x063633B4  token: 0x600043B
        public System.Void __Gen_Wrap_4(System.Object P0) { }
        // RVA: 0x0B8AEEF8  token: 0x600043C
        public System.Boolean __Gen_Wrap_5(UnityEngine.LowLevel.PlayerLoopSystem P0) { }
        // RVA: 0x06360610  token: 0x600043D
        public System.Int32 __Gen_Wrap_6(System.Object P0, System.Object P1) { }
        // RVA: 0x0B8AEFFC  token: 0x600043E
        public System.Int32 __Gen_Wrap_7(System.IntPtr P0, XLua.lua_debug& P1) { }
        // RVA: 0x063632E4  token: 0x600043F
        public System.Boolean __Gen_Wrap_8(System.Object P0) { }
        // RVA: 0x066E0A9C  token: 0x6000440
        public System.Boolean __Gen_Wrap_9(System.Object P0, System.Int32 P1) { }
        // RVA: 0x0B8AE640  token: 0x6000441
        public System.String __Gen_Wrap_10() { }
        // RVA: 0x0B8AE6F8  token: 0x6000442
        public System.Void __Gen_Wrap_11(System.Span<System.Byte> P0, System.Int32& P1) { }
        // RVA: 0x0B8AE81C  token: 0x6000443
        public System.Byte[] __Gen_Wrap_12(System.Object P0) { }
        // RVA: 0x0B8AE8F8  token: 0x6000444
        public System.String __Gen_Wrap_13(System.Object P0) { }
        // RVA: 0x0B8AE9D4  token: 0x6000445
        public System.IntPtr __Gen_Wrap_14(System.IntPtr P0, System.Int32 P1, System.Int32& P2) { }
        // RVA: 0x0B8AEAEC  token: 0x6000446
        public Beyond.Lua.LuaGenericDelegateCaller.LuaGenericDelegateCall __Gen_Wrap_15(System.Object P0) { }
        // RVA: 0x0B8AEBC8  token: 0x6000447
        public System.Int32 __Gen_Wrap_16(System.IntPtr P0, System.Object P1, System.Object P2) { }
        // RVA: 0x04D37800  token: 0x6000448
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F1  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0B8AF168  token: 0x6000449
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x600044A
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x20000F2  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x600044B
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0B8B6A50  token: 0x600044C
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x038EFAD0  token: 0x600044D
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0B8B690C  token: 0x600044E
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0B8B69D8  token: 0x600044F
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0B8B6AB4  token: 0x6000450
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0B8B6868  token: 0x6000451
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x20000F3  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Reflection-GenericDelegateCaller-GetCustomDelegateType0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-GenericDelegateCaller-AddCustomDelegateType0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-MethodParamItem-GetMethodParamList0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-MethodParamItem-GetMethodSignatureString0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-StartGCAsync0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_OnGCEnd0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-NormalStop0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-ForceStopAsyncGC0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-MainThreadWait0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-AddLuaAfterGCEvent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-AddLuaAfterGCOnceEvent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-RemoveLuaAfterGCEvent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_FindLoopSystemIndex0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_Init0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-LuaHook0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_UpdateOldHook0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_DoLock0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_DoGc0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Lua-LuaMultithreadingGC-_GCMethodWrapper0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaCypher-GetKey0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaCypher-GetKey1;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaCypher-SkipDecrypt0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaCypher-DecryptLua0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaCypher-DecryptLuaString0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaCypher-DecryptionFunc0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaGenericDelegateCaller-GetCaller0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaGenericDelegateCaller-AddCaller0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaGenericDelegateCaller-OverloadMethodCall0;  // const
        public static IFix.IDMAP0 Beyond-Lua-LuaGenericDelegateCaller-DefaultValueMethodCall0;  // const

    }

}

namespace Microsoft.CodeAnalysis
{

    // TypeToken: 0x2000004  // size: 0x10
    public sealed class EmbeddedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600000E
        public System.Void .ctor() { }

    }

}

namespace Security
{

    // TypeToken: 0x2000007  // size: 0x10
    public sealed class XXTEA
    {
        // Fields
        private static readonly System.Text.UTF8Encoding utf8;  // static @ 0x0
        private static System.UInt32 delta;  // const

        // Methods
        // RVA: 0x0B8A6B74  token: 0x6000013
        private static System.UInt32 MX(System.UInt32 sum, System.UInt32 y, System.UInt32 z, System.Int32 p, System.UInt32 e, System.UInt32[] k) { }
        // RVA: 0x041E1670  token: 0x6000014
        private System.Void .ctor() { }
        // RVA: 0x0B8A6914  token: 0x6000015
        public static System.Byte[] Encrypt(System.Byte[] data, System.Byte[] key) { }
        // RVA: 0x0B8A6638  token: 0x6000016
        public static System.Byte[] Encrypt(System.String data, System.Byte[] key) { }
        // RVA: 0x0B8A69B0  token: 0x6000017
        public static System.Byte[] Encrypt(System.Byte[] data, System.String key) { }
        // RVA: 0x0B8A66B4  token: 0x6000018
        public static System.Byte[] Encrypt(System.String data, System.String key) { }
        // RVA: 0x0B8A6548  token: 0x6000019
        public static System.String EncryptToBase64String(System.Byte[] data, System.Byte[] key) { }
        // RVA: 0x0B8A65C0  token: 0x600001A
        public static System.String EncryptToBase64String(System.String data, System.Byte[] key) { }
        // RVA: 0x0B8A6458  token: 0x600001B
        public static System.String EncryptToBase64String(System.Byte[] data, System.String key) { }
        // RVA: 0x0B8A64D0  token: 0x600001C
        public static System.String EncryptToBase64String(System.String data, System.String key) { }
        // RVA: 0x0B8A6338  token: 0x600001D
        public static System.Byte[] Decrypt(System.Byte[] data, System.Byte[] key) { }
        // RVA: 0x0B8A707C  token: 0x600001E
        private static System.Int32 _Decrypt(System.Span<System.Byte> data, System.Span<System.Byte> key, System.Span<System.Byte> result) { }
        // RVA: 0x0B8A62BC  token: 0x600001F
        public static System.Byte[] Decrypt(System.Byte[] data, System.String key) { }
        // RVA: 0x0B8A63D4  token: 0x6000020
        public static System.Int32 Decrypt(System.Span<System.Byte> data, System.Span<System.Byte> key, System.Span<System.Byte> result) { }
        // RVA: 0x0B8A5D44  token: 0x6000021
        public static System.Byte[] DecryptBase64String(System.String data, System.Byte[] key) { }
        // RVA: 0x0B8A5DBC  token: 0x6000022
        public static System.Byte[] DecryptBase64String(System.String data, System.String key) { }
        // RVA: 0x0B8A6010  token: 0x6000023
        public static System.String DecryptToString(System.Byte[] data, System.Byte[] key) { }
        // RVA: 0x0B8A6094  token: 0x6000024
        public static System.String DecryptToString(System.Byte[] data, System.String key) { }
        // RVA: 0x0B8A5CC0  token: 0x6000025
        public static System.String DecryptBase64StringToString(System.String data, System.Byte[] key) { }
        // RVA: 0x0B8A5C3C  token: 0x6000026
        public static System.String DecryptBase64StringToString(System.String data, System.String key) { }
        // RVA: 0x0B8A6754  token: 0x6000027
        private static System.UInt32[] Encrypt(System.UInt32[] v, System.UInt32[] k) { }
        // RVA: 0x0B8A6118  token: 0x6000028
        private static System.UInt32[] Decrypt(System.UInt32[] v, System.UInt32[] k) { }
        // RVA: 0x0B8A5E34  token: 0x6000029
        private static System.Void DecryptInPlace(System.Span<System.UInt32> v, System.Span<System.UInt32> k) { }
        // RVA: 0x0B8A6C18  token: 0x600002A
        private static System.UInt32 MxForSpan(System.UInt32 sum, System.UInt32 y, System.UInt32 z, System.Int32 p, System.UInt32 e, System.Span<System.UInt32> k) { }
        // RVA: 0x0B8A6ADC  token: 0x600002B
        private static System.Byte[] FixKey(System.Byte[] key) { }
        // RVA: 0x0B8A6A2C  token: 0x600002C
        private static System.Void FixKey(System.Span<System.Byte> key, System.Span<System.Byte> fixedkey) { }
        // RVA: 0x0B8A6F8C  token: 0x600002D
        private static System.UInt32[] ToUInt32Array(System.Byte[] data, System.Boolean includeLength) { }
        // RVA: 0x0B8A6E9C  token: 0x600002E
        private static System.Void ToUInt32Array(System.Span<System.Byte> data, System.Boolean includeLength, System.Span<System.UInt32> result) { }
        // RVA: 0x0B8A6C7C  token: 0x600002F
        private static System.Byte[] ToByteArray(System.UInt32[] data, System.Boolean includeLength) { }
        // RVA: 0x0B8A6D58  token: 0x6000030
        private static System.Void ToByteSpanFromUInt32(System.Span<System.UInt32> data, System.Boolean includeLength, System.Span<System.Byte> result, System.Int32& len) { }
        // RVA: 0x0B8A732C  token: 0x6000031
        private static System.Void .cctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000005  // size: 0x10
    public sealed class IsUnmanagedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600000F
        public System.Void .ctor() { }

    }

}

namespace XLua
{

    // TypeToken: 0x200004F  // size: 0x10
    public class CopyByValue
    {
        // Fields
        public static XLua.CopyByValue Instance;  // static @ 0x0

        // Methods
        // RVA: 0x0B89E56C  token: 0x6000061
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.Byte field) { }
        // RVA: 0x0B89ED38  token: 0x6000062
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.Byte& field) { }
        // RVA: 0x0B89E4A8  token: 0x6000063
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.SByte field) { }
        // RVA: 0x0B89ECC8  token: 0x6000064
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.SByte& field) { }
        // RVA: 0x0B89E5D0  token: 0x6000065
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.Int16 field) { }
        // RVA: 0x0B89EC64  token: 0x6000066
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.Int16& field) { }
        // RVA: 0x0B89E7C4  token: 0x6000067
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.UInt16 field) { }
        // RVA: 0x0B89E8F0  token: 0x6000068
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.UInt16& field) { }
        // RVA: 0x03B61630  token: 0x6000069
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.Int32 field) { }
        // RVA: 0x03A1B1B0  token: 0x600006A
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.Int32& field) { }
        // RVA: 0x0B89E50C  token: 0x600006B
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.UInt32 field) { }
        // RVA: 0x0B89E9D4  token: 0x600006C
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.UInt32& field) { }
        // RVA: 0x0B89E760  token: 0x600006D
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.Int64 field) { }
        // RVA: 0x0B89E88C  token: 0x600006E
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.Int64& field) { }
        // RVA: 0x0B89E6FC  token: 0x600006F
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.UInt64 field) { }
        // RVA: 0x0B89EBF0  token: 0x6000070
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.UInt64& field) { }
        // RVA: 0x0B89E698  token: 0x6000071
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.Single field) { }
        // RVA: 0x0B89E970  token: 0x6000072
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.Single& field) { }
        // RVA: 0x0B89E634  token: 0x6000073
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.Double field) { }
        // RVA: 0x0B89EB8C  token: 0x6000074
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.Double& field) { }
        // RVA: 0x0B89E828  token: 0x6000075
        public System.Boolean Pack(System.IntPtr buff, System.Int32 offset, System.Decimal field) { }
        // RVA: 0x0B89EA44  token: 0x6000076
        public System.Boolean UnPack(System.IntPtr buff, System.Int32 offset, System.Decimal& field) { }
        // RVA: 0x02F2A650  token: 0x6000077
        public System.Boolean IsStruct(System.Type type) { }
        // RVA: 0x041E1670  token: 0x6000078
        public System.Void .ctor() { }
        // RVA: 0x04D4E3D0  token: 0x6000079
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000050  // size: 0x40
    public abstract class DelegateBridgeBase : XLua.LuaBase
    {
        // Fields
        private System.Type firstKey;  // 0x20
        private System.Delegate firstValue;  // 0x28
        private System.Collections.Generic.Dictionary<System.Type,System.Delegate> bindTo;  // 0x30
        public System.Int32 errorFuncRef;  // 0x38

        // Properties
        System.Boolean IsDispose { get; /* RVA: 0x015EFCE0 */ }

        // Methods
        // RVA: 0x03F20D50  token: 0x600007B
        public System.Void .ctor(System.Int32 reference, XLua.LuaEnv luaenv) { }
        // RVA: 0x044228D0  token: 0x600007C
        public System.Boolean TryGetDelegate(System.Type key, System.Delegate& value) { }
        // RVA: 0x03608370  token: 0x600007D
        public System.Void AddDelegate(System.Type key, System.Delegate value) { }
        // RVA: 0x011EC580  token: 0x600007E
        public virtual System.Delegate GetDelegateByType(System.Type type) { }

    }

    // TypeToken: 0x2000051  // size: 0x10
    public static class HotfixDelegateBridge
    {
        // Methods
        // RVA: 0x0B89EF94  token: 0x600007F
        public static System.Boolean xlua_get_hotfix_flag(System.Int32 idx) { }
        // RVA: 0x0B89ED9C  token: 0x6000080
        public static XLua.DelegateBridge Get(System.Int32 idx) { }
        // RVA: 0x0B89EE00  token: 0x6000081
        public static System.Void Set(System.Int32 idx, XLua.DelegateBridge val) { }

    }

    // TypeToken: 0x2000052  // size: 0x40
    public class DelegateBridge : XLua.DelegateBridgeBase
    {
        // Fields
        private static XLua.DelegateBridge[] DelegateBridgeList;  // static @ 0x0
        public static System.Boolean Gen_Flag;  // static @ 0x8
        public static System.Func<XLua.DelegateBridge,System.Type,System.Delegate> GetDelegateTypeDelegate;  // static @ 0x10

        // Methods
        // RVA: 0x053932E8  token: 0x6000082
        public System.Void .ctor(System.Int32 reference, XLua.LuaEnv luaenv) { }
        // RVA: 0x0329DBE0  token: 0x6000083
        public System.Void PCall(System.IntPtr L, System.Int32 nArgs, System.Int32 nResults, System.Int32 errFunc) { }
        // RVA: 0x03FC2290  token: 0x6000084
        public virtual System.Delegate GetDelegateByType(System.Type type) { }
        // RVA: 0x03544810  token: 0x6000085
        public System.Void Action() { }
        // RVA: -1  // generic def  token: 0x6000086
        public System.Void Action(T1 p1) { }
        // RVA: -1  // generic def  token: 0x6000087
        private System.Void ActionAfterGC(T1 p1) { }
        // RVA: -1  // generic def  token: 0x6000088
        public System.Void Action(T1 p1, T2 p2) { }
        // RVA: -1  // generic def  token: 0x6000089
        private System.Void ActionAfterGC(T1 p1, T2 p2) { }
        // RVA: -1  // generic def  token: 0x600008A
        public System.Void Action(T1 p1, T2 p2, T3 p3) { }
        // RVA: -1  // generic def  token: 0x600008B
        private System.Void ActionAfterGC(T1 p1, T2 p2, T3 p3) { }
        // RVA: -1  // generic def  token: 0x600008C
        public System.Void Action(T1 p1, T2 p2, T3 p3, T4 p4) { }
        // RVA: -1  // generic def  token: 0x600008D
        private System.Void ActionAfterGC(T1 p1, T2 p2, T3 p3, T4 p4) { }
        // RVA: -1  // generic def  token: 0x600008E
        public TResult Func() { }
        // RVA: -1  // generic def  token: 0x600008F
        public TResult Func(T1 p1) { }
        // RVA: -1  // generic def  token: 0x6000090
        public TResult Func(T1 p1, T2 p2) { }
        // RVA: -1  // generic def  token: 0x6000091
        public TResult Func(T1 p1, T2 p2, T3 p3) { }
        // RVA: -1  // generic def  token: 0x6000092
        public TResult Func(T1 p1, T2 p2, T3 p3, T4 p4) { }
        // RVA: 0x04CF5740  token: 0x6000093
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000057  // size: 0x14
    public sealed struct GenFlag
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.GenFlag No;  // const
        public static XLua.GenFlag GCOptimize;  // const

    }

    // TypeToken: 0x2000058  // size: 0x18
    public class LuaCallCSharpAttribute : System.Attribute
    {
        // Fields
        private XLua.GenFlag flag;  // 0x10

        // Properties
        XLua.GenFlag Flag { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600009D
        public System.Void .ctor(XLua.GenFlag flag) { }

    }

    // TypeToken: 0x2000059  // size: 0x10
    public class CSharpCallLuaAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600009E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005A  // size: 0x10
    public class BlackListAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600009F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005B  // size: 0x14
    public sealed struct OptimizeFlag
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.OptimizeFlag Default;  // const
        public static XLua.OptimizeFlag PackAsTable;  // const
        public static XLua.OptimizeFlag Custom;  // const

    }

    // TypeToken: 0x200005C  // size: 0x18
    public class GCOptimizeAttribute : System.Attribute
    {
        // Fields
        private XLua.OptimizeFlag flag;  // 0x10

        // Properties
        XLua.OptimizeFlag Flag { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000A1
        public System.Void .ctor(XLua.OptimizeFlag flag) { }

    }

    // TypeToken: 0x200005D  // size: 0x10
    public class ReflectionUseAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000A2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005E  // size: 0x10
    public class DoNotGenAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000A3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005F  // size: 0x10
    public class AdditionalPropertiesAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000060  // size: 0x14
    public sealed struct HotfixFlag
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.HotfixFlag Stateless;  // const
        public static XLua.HotfixFlag Stateful;  // const
        public static XLua.HotfixFlag ValueTypeBoxing;  // const
        public static XLua.HotfixFlag IgnoreProperty;  // const
        public static XLua.HotfixFlag IgnoreNotPublic;  // const
        public static XLua.HotfixFlag Inline;  // const
        public static XLua.HotfixFlag IntKey;  // const
        public static XLua.HotfixFlag AdaptByDelegate;  // const
        public static XLua.HotfixFlag IgnoreCompilerGenerated;  // const
        public static XLua.HotfixFlag NoBaseProxy;  // const

    }

    // TypeToken: 0x2000061  // size: 0x18
    public class HotfixAttribute : System.Attribute
    {
        // Fields
        private XLua.HotfixFlag flag;  // 0x10

        // Properties
        XLua.HotfixFlag Flag { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000A6
        public System.Void .ctor(XLua.HotfixFlag e) { }

    }

    // TypeToken: 0x2000062  // size: 0x10
    public class HotfixDelegateAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000A7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x10
    public static class SysGenConfig
    {
        // Properties
        System.Collections.Generic.List<System.Type> GCOptimize { get; /* RVA: 0x0B8A4F10 */ }
        System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.List<System.String>> AdditionalProperties { get; /* RVA: 0x0B8A4C8C */ }

    }

    // TypeToken: 0x2000064  // size: 0x10
    public class InternalGlobals
    {
        // Fields
        private static System.Byte[] strBuff;  // static @ 0x0
        public static XLua.InternalGlobals.TryArrayGet genTryArrayGetPtr;  // static @ 0x8
        public static XLua.InternalGlobals.TryArraySet genTryArraySetPtr;  // static @ 0x10
        private static System.Int32 LUA_REGISTRYINDEX;  // static @ 0x18
        private static System.Collections.Generic.Dictionary<System.String,System.String> supportOp;  // static @ 0x20
        public static System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>> extensionMethodMap;  // static @ 0x28
        private static XLua.LuaDLL.lua_CSFunction LazyReflectionWrap;  // static @ 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60000AA
        public System.Void .ctor() { }
        // RVA: 0x047729E0  token: 0x60000AB
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000067  // size: 0x20
    public abstract class LuaBase : System.IDisposable
    {
        // Fields
        public System.Boolean disposed;  // 0x10
        public readonly System.Int32 luaReference;  // 0x14
        public readonly XLua.LuaEnv luaEnv;  // 0x18

        // Methods
        // RVA: 0x053932C0  token: 0x60000B4
        public System.Void .ctor(System.Int32 reference, XLua.LuaEnv luaenv) { }
        // RVA: 0x032EF070  token: 0x60000B5
        protected virtual System.Void Finalize() { }
        // RVA: 0x032EF250  token: 0x60000B6
        public virtual System.Void Dispose() { }
        // RVA: 0x032EF310  token: 0x60000B7
        public virtual System.Void Dispose(System.Boolean disposeManagedResources) { }
        // RVA: 0x0B89F190  token: 0x60000B8
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0B89F2C8  token: 0x60000B9
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B89F37C  token: 0x60000BA
        public virtual System.Void push(System.IntPtr L) { }

    }

    // TypeToken: 0x2000068  // size: 0x40
    public sealed struct lua_debug
    {
        // Fields
        public System.Int32 eventCode;  // 0x10
        public System.IntPtr name;  // 0x18
        public System.IntPtr namewhat;  // 0x20
        public System.IntPtr what;  // 0x28
        public System.IntPtr source;  // 0x30
        public System.Int32 currentline;  // 0x38
        public System.Int32 linedefined;  // 0x3c

    }

    // TypeToken: 0x2000069  // size: 0x20
    public sealed struct lua_state
    {
        // Fields
        public System.IntPtr ptr;  // 0x10
        public System.Byte byte1;  // 0x18
        public System.Byte byte2;  // 0x19
        public System.Byte status;  // 0x1a
        public System.Byte allowhook;  // 0x1b

    }

    // TypeToken: 0x200006A  // size: 0x80
    public sealed class lua_Hook : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B163E34  token: 0x60000BB
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x60000BC
        public virtual System.Int32 Invoke(System.IntPtr L, XLua.lua_debug& ar) { }
        // RVA: 0x0B8A73A8  token: 0x60000BD
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, XLua.lua_debug& ar, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539ADF8  token: 0x60000BE
        public virtual System.Int32 EndInvoke(XLua.lua_debug& ar, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200006B  // size: 0x10
    public static class LuaDeadLoopCheck
    {
        // Fields
        private static System.Threading.Thread s_checkThread;  // static @ 0x0
        private static System.Collections.Generic.Stack<System.Diagnostics.Stopwatch> s_costStack;  // static @ 0x8
        private static System.Threading.SpinLock s_lockObj;  // static @ 0x10
        private static System.Boolean s_isCheck;  // static @ 0x14
        private static System.IntPtr L;  // static @ 0x18
        private static UnityEngine.Pool.ObjectPool<System.Diagnostics.Stopwatch> s_stopwatchPool;  // static @ 0x20
        private static System.Int32 s_allowHookOffset;  // static @ 0x28

        // Methods
        // RVA: 0x03B26BD0  token: 0x60000BF
        public static System.Void EnableCheck(System.IntPtr L) { }
        // RVA: 0x0B8A02CC  token: 0x60000C0
        public static System.Int32 LuaHook(System.IntPtr L, XLua.lua_debug& ar) { }
        // RVA: 0x0B8A0294  token: 0x60000C1
        public static System.Void DisableCheck(System.IntPtr L) { }
        // RVA: 0x0329EB90  token: 0x60000C2
        public static System.Void PushTime() { }
        // RVA: 0x0329E7E0  token: 0x60000C3
        public static System.Void PopTime() { }
        // RVA: 0x02EFE970  token: 0x60000C4
        public static System.Void CheckDeadLoop() { }

    }

    // TypeToken: 0x200006D
    public interface ILoadLuaProxy
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60000CA
        public virtual System.Byte[] LoadLua(System.String name) { }

    }

    // TypeToken: 0x200006E  // size: 0x10
    public class LoadLuaProxyModule
    {
        // Fields
        private static XLua.ILoadLuaProxy s_loadLuaProxy;  // static @ 0x0

        // Methods
        // RVA: 0x0B89F05C  token: 0x60000CB
        public static System.Void RegisterProxy(XLua.ILoadLuaProxy loadLuaProxy) { }
        // RVA: 0x0B89F024  token: 0x60000CC
        public static XLua.ILoadLuaProxy GetLoadLuaProxy() { }
        // RVA: 0x041E1670  token: 0x60000CD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006F  // size: 0x88
    public class LuaEnv : System.IDisposable
    {
        // Fields
        public static System.String CSHARP_NAMESPACE;  // const
        public static System.String MAIN_SHREAD;  // const
        private Beyond.Scripts.Lua.LuaMultithreadingGC m_luaMultithreadingGC;  // 0x10
        public static System.Boolean ENABLE_MULTITHREAD_GC;  // static @ 0x0
        public System.Boolean isQuittingApplication;  // 0x18
        public System.IntPtr rawL;  // 0x20
        private XLua.LuaTable _G;  // 0x28
        public XLua.ObjectTranslator translator;  // 0x30
        public System.Int32 errorFuncRef;  // 0x38
        private static System.Int32 LIB_VERSION_EXPECT;  // const
        private static System.Collections.Generic.List<System.Action<XLua.LuaEnv,XLua.ObjectTranslator>> initers;  // static @ 0x8
        private System.Collections.Generic.Dictionary<System.Int64,System.String> _stringCache;  // 0x40
        private System.Int64 _stringCacheMemory;  // 0x48
        private readonly System.Int64 _freeSize;  // 0x50
        private System.Int32 last_check_point;  // 0x58
        private System.Int32 max_check_per_tick;  // 0x5c
        private System.Func<System.Object,System.Boolean> object_valid_checker;  // 0x60
        private System.Boolean disposed;  // 0x68
        private System.Collections.Generic.Queue<XLua.LuaEnv.GCAction> refQueue;  // 0x70
        private System.Collections.Generic.List<XLua.LuaEnv.CustomLoader> customLoaders;  // 0x78
        private System.Collections.Generic.Dictionary<System.String,XLua.LuaDLL.lua_CSFunction> buildin_initer;  // 0x80

        // Properties
        System.IntPtr L { get; /* RVA: 0x0329D7C0 */ }
        XLua.LuaTable Global { get; /* RVA: 0x04D86240 */ }
        System.Int32 GcPause { get; /* RVA: 0x0B8A0C40 */ set; /* RVA: 0x0B8A0D40 */ }
        System.Int32 GcStepmul { get; /* RVA: 0x0B8A0CC0 */ set; /* RVA: 0x0B8A0DA0 */ }
        System.Int32 Memroy { get; /* RVA: 0x038EF8C0 */ }

        // Methods
        // RVA: 0x03A73F30  token: 0x60000CF
        public System.Boolean IsEnableMultithreadGC() { }
        // RVA: 0x0329DBC0  token: 0x60000D0
        public System.Boolean IsLuaGCing() { }
        // RVA: 0x0488B670  token: 0x60000D1
        public System.Boolean AddLuaEventAfterGCDo(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0B8A0A40  token: 0x60000D2
        public System.Void RemoveLuaEventAfterGCDo(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0423A460  token: 0x60000D3
        public System.Void AddOnceLuaEventAfterGCDo(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0B8A0548  token: 0x60000D4
        public System.Void CriticalLuaCallWaitLuaGC() { }
        // RVA: 0x0B8A092C  token: 0x60000D5
        public System.Void MainThreadWait() { }
        // RVA: 0x0B8A0AF8  token: 0x60000D6
        public System.Void StopMultiGC() { }
        // RVA: 0x03601B70  token: 0x60000D7
        public System.Void .ctor() { }
        // RVA: 0x04810000  token: 0x60000D8
        public static System.Void AddIniter(System.Action<XLua.LuaEnv,XLua.ObjectTranslator> initer) { }
        // RVA: -1  // generic def  token: 0x60000DA
        public T LoadString(System.Byte[] chunk, System.String chunkName, XLua.LuaTable env) { }
        // RVA: -1  // generic def  token: 0x60000DB
        public T LoadString(System.String chunk, System.String chunkName, XLua.LuaTable env) { }
        // RVA: 0x0B8A08BC  token: 0x60000DC
        public XLua.LuaFunction LoadString(System.String chunk, System.String chunkName, XLua.LuaTable env) { }
        // RVA: 0x03600E10  token: 0x60000DD
        public System.Object[] DoString(System.Byte[] chunk, System.String chunkName, XLua.LuaTable env) { }
        // RVA: 0x0B8A078C  token: 0x60000DE
        public System.Object[] DoString(System.String chunk, System.String chunkName, XLua.LuaTable env) { }
        // RVA: 0x03600CF0  token: 0x60000DF
        public System.Object[] DoFile(System.String luaFile, System.String chunkName, XLua.LuaTable env) { }
        // RVA: 0x0539321C  token: 0x60000E0
        public System.Object[] DoFile(System.String luaFile, XLua.LuaTable env) { }
        // RVA: 0x0321C3A0  token: 0x60000E1
        public System.Object[] CallGlobalFunction(System.String funcName) { }
        // RVA: -1  // generic def  token: 0x60000E2
        public System.Object[] CallGlobalFunction(System.String funcName, T0 param0) { }
        // RVA: -1  // generic def  token: 0x60000E3
        public System.Void SetGlobalTable(K key, V value) { }
        // RVA: -1  // generic def  token: 0x60000E4
        public V GetGlobalTable(K key) { }
        // RVA: 0x03600960  token: 0x60000E5
        public System.Void InitCacheString() { }
        // RVA: 0x03C16CA0  token: 0x60000E6
        public System.String TryGetLuaString(System.IntPtr p) { }
        // RVA: 0x03608420  token: 0x60000E7
        public System.Void RefString(System.IntPtr L, System.IntPtr strPoint, System.Int32 index, System.String ss) { }
        // RVA: 0x03600890  token: 0x60000E8
        public System.Void ReleaseStringCache(System.Boolean dispose) { }
        // RVA: 0x0301FCD0  token: 0x60000E9
        public System.Void AutoFreeStringCache() { }
        // RVA: 0x03602870  token: 0x60000EA
        private System.Void AddSearcher(XLua.LuaDLL.lua_CSFunction searcher, System.Int32 index) { }
        // RVA: 0x0B8A0528  token: 0x60000EB
        public System.Void Alias(System.Type type, System.String alias) { }
        // RVA: 0x03B14BD0  token: 0x60000EC
        private static System.Boolean ObjectValidCheck(System.Object obj) { }
        // RVA: 0x04034F70  token: 0x60000ED
        public System.Void ClearAllUserdata() { }
        // RVA: 0x0301FDD0  token: 0x60000EE
        public System.Void CleanTick() { }
        // RVA: 0x0B8A0850  token: 0x60000EF
        public System.Void GC() { }
        // RVA: 0x0B8A0940  token: 0x60000F0
        public XLua.LuaTable NewTable() { }
        // RVA: 0x0B8A0C38  token: 0x60000F1
        public System.Void WaitForGCComplete() { }
        // RVA: 0x0B8A0700  token: 0x60000F2
        public virtual System.Void Dispose() { }
        // RVA: 0x0B8A05A4  token: 0x60000F3
        public virtual System.Void Dispose(System.Boolean dispose) { }
        // RVA: 0x0B8A0B28  token: 0x60000F4
        public System.Void ThrowExceptionFromError(System.Int32 oldTop) { }
        // RVA: 0x032EED00  token: 0x60000F5
        private System.Void equeueGCAction(XLua.LuaEnv.GCAction action) { }
        // RVA: 0x04D54440  token: 0x60000F6
        public System.Void AddLoader(XLua.LuaEnv.CustomLoader loader) { }
        // RVA: 0x03601A00  token: 0x60000F7
        public System.Void AddBuildin(System.String name, XLua.LuaDLL.lua_CSFunction initer) { }
        // RVA: 0x0B8A07FC  token: 0x60000FC
        public System.Void FullGc() { }
        // RVA: 0x0B8A0AA8  token: 0x60000FD
        public System.Void StopGc() { }
        // RVA: 0x0B8A0A54  token: 0x60000FE
        public System.Void RestartGc() { }
        // RVA: 0x0B8A0858  token: 0x60000FF
        public System.Boolean GcStep(System.Int32 data) { }
        // RVA: 0x04D4F970  token: 0x6000101
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000072  // size: 0x90
    public class LuaException : System.Exception
    {
        // Methods
        // RVA: 0x0B8A0E00  token: 0x6000106
        public System.Void .ctor(System.String message) { }

    }

    // TypeToken: 0x2000073  // size: 0x20
    public class LuaFunction : XLua.LuaBase
    {
        // Methods
        // RVA: 0x053932C0  token: 0x6000107
        public System.Void .ctor(System.Int32 reference, XLua.LuaEnv luaenv) { }
        // RVA: -1  // generic def  token: 0x6000108
        public System.Void Action(T a) { }
        // RVA: -1  // generic def  token: 0x6000109
        private System.Void ActionAfterGC(T a) { }
        // RVA: -1  // generic def  token: 0x600010A
        public TResult Func(T a) { }
        // RVA: -1  // generic def  token: 0x600010B
        public System.Void Action(T1 a1, T2 a2) { }
        // RVA: -1  // generic def  token: 0x600010C
        private System.Void ActionAfterGC(T1 a1, T2 a2) { }
        // RVA: -1  // generic def  token: 0x600010D
        public TResult Func(T1 a1, T2 a2) { }
        // RVA: 0x03544DE0  token: 0x600010E
        public System.Object[] Call(System.Object[] args, System.Type[] returnTypes) { }
        // RVA: 0x05391968  token: 0x600010F
        public System.Object[] Call(System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x6000110
        public T Cast() { }
        // RVA: 0x0B8A0E5C  token: 0x6000111
        public System.Void SetEnv(XLua.LuaTable env) { }
        // RVA: 0x04D543E0  token: 0x6000112
        public virtual System.Void push(System.IntPtr L) { }
        // RVA: 0x0B8A0F14  token: 0x6000113
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000076  // size: 0x20
    public class LuaTable : XLua.LuaBase
    {
        // Properties
        System.Object Item { get; /* RVA: 0x0B8A1AFC */ set; /* RVA: 0x0B8A1C78 */ }
        System.Object Item { get; /* RVA: 0x0B8A1AFC */ set; /* RVA: 0x0B8A1C78 */ }
        System.Int32 Length { get; /* RVA: 0x0B8A1B8C */ }

        // Methods
        // RVA: 0x053932C0  token: 0x6000118
        public System.Void .ctor(System.Int32 reference, XLua.LuaEnv luaenv) { }
        // RVA: -1  // generic def  token: 0x6000119
        public System.Void Get(TKey key, TValue& value) { }
        // RVA: -1  // generic def  token: 0x600011A
        public System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // generic def  token: 0x600011B
        public System.Void Set(TKey key, TValue value) { }
        // RVA: -1  // generic def  token: 0x600011C
        public T GetInPath(System.String path) { }
        // RVA: -1  // generic def  token: 0x600011D
        public System.Void SetInPath(System.String path, T val) { }
        // RVA: -1  // generic def  token: 0x6000122
        public System.Void ForEach(System.Action<TKey,TValue> action) { }
        // RVA: 0x0B8A1974  token: 0x6000124
        public System.Collections.IEnumerable GetKeys() { }
        // RVA: -1  // generic def  token: 0x6000125
        public System.Collections.Generic.IEnumerable<T> GetKeys() { }
        // RVA: -1  // generic def  token: 0x6000126
        public T Get(System.Object key) { }
        // RVA: -1  // generic def  token: 0x6000127
        public TValue Get(TKey key) { }
        // RVA: -1  // generic def  token: 0x6000128
        public TValue Get(System.String key) { }
        // RVA: 0x0B8A19E4  token: 0x6000129
        public System.Void SetMetaTable(XLua.LuaTable metaTable) { }
        // RVA: -1  // generic def  token: 0x600012A
        public T Cast() { }
        // RVA: 0x0427F7E0  token: 0x600012B
        public virtual System.Void push(System.IntPtr L) { }
        // RVA: 0x0B8A1AB4  token: 0x600012C
        public virtual System.String ToString() { }
        // RVA: 0x032F12A0  token: 0x600012D
        public XLua.ObjectTranslator GetTranslator() { }

    }

    // TypeToken: 0x2000079
    public interface IOverloadMethodWrap
    {
        // Properties
        System.Boolean InitedForDelegateCall { get; /* RVA: -1  // abstract */ }
        System.Boolean HasDefalutValue { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000140
        public virtual System.Int32 Call(System.IntPtr L, XLua.ObjectTranslator translator) { }
        // RVA: -1  // abstract  token: 0x6000141
        public virtual System.Boolean Check(System.IntPtr L, System.Boolean haveOverload) { }

    }

    // TypeToken: 0x200007A  // size: 0x20
    public class OverloadMethodWrapBase
    {
        // Fields
        protected XLua.ObjectTranslator translator;  // 0x10
        protected System.Reflection.MethodBase method;  // 0x18

        // Methods
        // RVA: 0x02F2C470  token: 0x6000142
        public static XLua.IOverloadMethodWrap CreateMethodWrap(XLua.ObjectTranslator translator, System.Reflection.MethodBase method, System.Type target) { }
        // RVA: 0x041E1670  token: 0x6000143
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007B  // size: 0x58
    public class OverloadMethodWrapForDelegate : XLua.OverloadMethodWrapBase, XLua.IOverloadMethodWrap
    {
        // Fields
        private System.Collections.Generic.List<System.Type> castTypes;  // 0x20
        private System.Delegate delegate;  // 0x28
        private Beyond.Lua.LuaGenericDelegateCaller.LuaGenericDelegateCall caller;  // 0x30
        private System.Int32 <OptionParamCount>k__BackingField;  // 0x38
        private System.Object[] <DefalutValue>k__BackingField;  // 0x40
        private System.Boolean <HasDefalutValue>k__BackingField;  // 0x48
        private System.Boolean[] <OutParamFlags>k__BackingField;  // 0x50

        // Properties
        System.Boolean InitedForDelegateCall { get; /* RVA: 0x03FB3DE0 */ }
        System.Collections.Generic.List<System.Type> CastTypes { get; /* RVA: 0x04D862C0 */ }
        System.Int32 OptionParamCount { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Object[] DefalutValue { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Boolean HasDefalutValue { get; /* RVA: 0x04D867D0 */ set; /* RVA: 0x04D86810 */ }
        System.Boolean[] OutParamFlags { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }

        // Methods
        // RVA: 0x02F2C410  token: 0x600014E
        public System.Void .ctor(System.Reflection.MethodInfo method) { }
        // RVA: 0x02F2CE00  token: 0x600014F
        public static XLua.OverloadMethodWrapForDelegate TryGetWrapForDelegate(System.Reflection.MethodInfo method) { }
        // RVA: 0x04281250  token: 0x6000150
        public System.Void InitForDefaultValue() { }
        // RVA: 0x0329BD30  token: 0x6000151
        public virtual System.Int32 Call(System.IntPtr L, XLua.ObjectTranslator translator) { }
        // RVA: 0x02FFF600  token: 0x6000152
        public virtual System.Boolean Check(System.IntPtr L, System.Boolean haveOverload) { }

    }

    // TypeToken: 0x200007C  // size: 0x80
    public class OverloadMethodWrapForReflection : XLua.OverloadMethodWrapBase, XLua.IOverloadMethodWrap
    {
        // Fields
        private XLua.ObjectCheck[] checkArray;  // 0x20
        private XLua.ObjectCast[] castArray;  // 0x28
        private System.Type targetType;  // 0x30
        private System.Int32[] inPosArray;  // 0x38
        private System.Int32[] outPosArray;  // 0x40
        private System.Boolean[] isOptionalArray;  // 0x48
        private System.Object[] defaultValueArray;  // 0x50
        private System.Boolean isVoid;  // 0x58
        private System.Int32 luaStackPosStart;  // 0x5c
        private System.Boolean targetNeeded;  // 0x60
        private System.Object[] args;  // 0x68
        private System.Int32[] refPos;  // 0x70
        private System.Type paramsType;  // 0x78

        // Properties
        System.Boolean InitedForDelegateCall { get; /* RVA: 0x012081B0 */ }
        System.Boolean HasDefalutValue { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x044C5110  token: 0x6000153
        public System.Void .ctor(System.Type target, System.Reflection.MethodBase methodInfo) { }
        // RVA: 0x02F2C530  token: 0x6000154
        public System.Void Init(XLua.ObjectCheckers objCheckers, XLua.ObjectCasters objCasters) { }
        // RVA: 0x0329C0E0  token: 0x6000155
        public virtual System.Int32 Call(System.IntPtr L, XLua.ObjectTranslator translator) { }
        // RVA: 0x0329C0B0  token: 0x6000158
        public virtual System.Boolean Check(System.IntPtr L, System.Boolean haveOverload) { }
        // RVA: 0x03DC80D0  token: 0x6000159
        public System.Boolean Check(System.IntPtr L) { }
        // RVA: 0x0329B2F0  token: 0x600015A
        public System.Boolean CheckForNoOverride(System.IntPtr L) { }

    }

    // TypeToken: 0x200007D  // size: 0xB0
    public class OverloadMethodWrap
    {
        // Fields
        private XLua.ObjectTranslator translator;  // 0x10
        private System.Type targetType;  // 0x18
        private System.Reflection.MethodBase method;  // 0x20
        private XLua.ObjectCheck[] checkArray;  // 0x28
        private XLua.ObjectCast[] castArray;  // 0x30
        private System.Collections.Generic.List<System.Type> castTypes;  // 0x38
        private System.Delegate delegate;  // 0x40
        private Beyond.Lua.LuaGenericDelegateCaller.LuaGenericDelegateCall caller;  // 0x48
        private System.Int32[] inPosArray;  // 0x50
        private System.Int32[] outPosArray;  // 0x58
        private System.Boolean[] isOptionalArray;  // 0x60
        private System.Object[] defaultValueArray;  // 0x68
        private System.Boolean isVoid;  // 0x70
        private System.Int32 luaStackPosStart;  // 0x74
        private System.Boolean targetNeeded;  // 0x78
        private System.Object[] args;  // 0x80
        private System.Int32[] refPos;  // 0x88
        private System.Type paramsType;  // 0x90
        private System.Int32 <OptionParamCount>k__BackingField;  // 0x98
        private System.Object[] <DefalutValue>k__BackingField;  // 0xa0
        private System.Boolean <IsInited>k__BackingField;  // 0xa8
        private System.Boolean <HasDefalutValue>k__BackingField;  // 0xa9

        // Properties
        System.Boolean InitedForDelegateCall { get; /* RVA: 0x090BCB14 */ }
        System.Reflection.MethodBase Method { get; /* RVA: 0x04D862C0 */ }
        System.Collections.Generic.List<System.Type> CastTypes { get; /* RVA: 0x04D85A50 */ }
        System.Int32 OptionParamCount { get; /* RVA: 0x04D862D0 */ set; /* RVA: 0x04D86300 */ }
        System.Object[] DefalutValue { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        System.Boolean IsInited { get; /* RVA: 0x04D90720 */ set; /* RVA: 0x04D90750 */ }
        System.Boolean HasDefalutValue { get; /* RVA: 0x02ED0880 */ set; /* RVA: 0x04D90740 */ }

        // Methods
        // RVA: 0x0B8A4C40  token: 0x6000166
        public System.Void .ctor(XLua.ObjectTranslator translator, System.Type targetType, System.Reflection.MethodBase method) { }
        // RVA: 0x0B8A417C  token: 0x6000167
        public System.Void Init(XLua.ObjectCheckers objCheckers, XLua.ObjectCasters objCasters) { }
        // RVA: 0x0B8A38B0  token: 0x6000168
        public System.Void InitForDefaultValue() { }
        // RVA: 0x0B8A3B7C  token: 0x6000169
        public System.Void InitForDelegateCallMethod() { }
        // RVA: 0x0B8A30EC  token: 0x600016A
        public System.Int32 Call(System.IntPtr L) { }
        // RVA: 0x0B8A2F74  token: 0x600016B
        public System.Int32 CallForDelegate(System.IntPtr L, XLua.ObjectTranslator translator) { }

    }

    // TypeToken: 0x200007E  // size: 0x38
    public class MethodWrap
    {
        // Fields
        private System.String methodName;  // 0x10
        private XLua.IOverloadMethodWrap[] overloads;  // 0x18
        private System.Type targetType;  // 0x20
        private System.Reflection.MethodBase[] allMethods;  // 0x28
        private System.Boolean forceCheck;  // 0x30
        private System.Boolean initedForDelegateCall;  // 0x31
        private System.Boolean haveDefault;  // 0x32

        // Methods
        // RVA: 0x02F320F0  token: 0x600016C
        public System.Void .ctor(System.String methodName, System.Collections.Generic.List<System.Reflection.MethodBase> overloads, System.Type targetType, System.Boolean forceCheck) { }
        // RVA: 0x03FB3C80  token: 0x600016D
        private System.Void _InitForDelegateCall() { }
        // RVA: 0x0329B7B0  token: 0x600016E
        public System.Int32 Call(System.IntPtr L) { }

    }

    // TypeToken: 0x200007F  // size: 0x40
    public class MethodWrapsCache
    {
        // Fields
        public static System.Collections.Generic.Dictionary<System.Type,XLua.LuaDLL.lua_CSFunction> CodeGenConstructorMap;  // static @ 0x0
        private XLua.ObjectTranslator translator;  // 0x10
        private XLua.ObjectCheckers objCheckers;  // 0x18
        private XLua.ObjectCasters objCasters;  // 0x20
        private System.Collections.Generic.Dictionary<System.Type,XLua.LuaDLL.lua_CSFunction> constructorCache;  // 0x28
        private System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.String,XLua.LuaDLL.lua_CSFunction>> methodsCache;  // 0x30
        private System.Collections.Generic.Dictionary<System.Type,XLua.LuaDLL.lua_CSFunction> delegateCache;  // 0x38

        // Methods
        // RVA: 0x0B8A1CD0  token: 0x600016F
        public static System.Void AddCodeGenConstructor(System.Type type, XLua.LuaDLL.lua_CSFunction lua_CSFunction) { }
        // RVA: 0x0B8A1DD0  token: 0x6000170
        public static System.Void ClearCodeGenConstructorMap() { }
        // RVA: 0x04A22640  token: 0x6000171
        private static System.Void .cctor() { }
        // RVA: 0x04A226D0  token: 0x6000172
        public System.Void .ctor(XLua.ObjectTranslator translator, XLua.ObjectCheckers objCheckers, XLua.ObjectCasters objCasters) { }
        // RVA: 0x03605D90  token: 0x6000173
        public XLua.LuaDLL.lua_CSFunction GetConstructorWrap(System.Type type) { }
        // RVA: 0x0B8A218C  token: 0x6000174
        public XLua.LuaDLL.lua_CSFunction GetMethodWrap(System.Type type, System.String methodName) { }
        // RVA: 0x0B8A2034  token: 0x6000175
        public XLua.LuaDLL.lua_CSFunction GetMethodWrapInCache(System.Type type, System.String methodName) { }
        // RVA: 0x0B8A1E34  token: 0x6000176
        public XLua.LuaDLL.lua_CSFunction GetDelegateWrap(System.Type type) { }
        // RVA: 0x03600400  token: 0x6000177
        public XLua.LuaDLL.lua_CSFunction GetEventWrap(System.Type type, System.String eventName) { }
        // RVA: 0x02F31950  token: 0x6000178
        public XLua.MethodWrap _GenMethodWrap(System.Type type, System.String methodName, System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> methodBases, System.Boolean forceCheck) { }
        // RVA: 0x0B8A23D4  token: 0x6000179
        private static System.Boolean tryMakeGenericMethod(System.Reflection.MethodBase& method) { }

    }

    // TypeToken: 0x2000083  // size: 0x80
    public sealed class ObjectCheck : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03604830  token: 0x6000180
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x6000181
        public virtual System.Boolean Invoke(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A2B6C  token: 0x6000182
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Int32 idx, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE668  token: 0x6000183
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000084  // size: 0x80
    public sealed class ObjectCast : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03604170  token: 0x6000184
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x6000185
        public virtual System.Object Invoke(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A25E0  token: 0x6000186
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.Int32 idx, System.Object target, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x6000187
        public virtual System.Object EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000085  // size: 0x20
    public class ObjectCheckers
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Type,XLua.ObjectCheck> checkersMap;  // 0x10
        private XLua.ObjectTranslator translator;  // 0x18

        // Methods
        // RVA: 0x03602A10  token: 0x6000188
        public System.Void .ctor(XLua.ObjectTranslator translator) { }
        // RVA: 0x02FFF600  token: 0x6000189
        private static System.Boolean objectCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A2E94  token: 0x600018A
        private System.Boolean luaTableCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x040B5BE0  token: 0x600018B
        private System.Boolean numberCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A2CE0  token: 0x600018C
        private System.Boolean decimalCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x03FC2070  token: 0x600018D
        private System.Boolean strCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A2C00  token: 0x600018E
        private System.Boolean bytesCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x04129F90  token: 0x600018F
        private System.Boolean boolCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x04188EB0  token: 0x6000190
        private System.Boolean int64Check(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x04188EB0  token: 0x6000191
        private System.Boolean uint64Check(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A2DB4  token: 0x6000192
        private System.Boolean luaFunctionCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8A2D60  token: 0x6000193
        private System.Boolean intptrCheck(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x036044C0  token: 0x6000194
        private XLua.ObjectCheck genChecker(System.Type type) { }
        // RVA: 0x04A30C90  token: 0x6000195
        public XLua.ObjectCheck genNullableChecker(XLua.ObjectCheck oc) { }
        // RVA: 0x04A3C880  token: 0x6000196
        public System.Void AddChecker(System.Type type, XLua.ObjectCheck oc) { }
        // RVA: 0x03299E60  token: 0x6000197
        public XLua.ObjectCheck GetChecker(System.Type type) { }

    }

    // TypeToken: 0x2000088  // size: 0x20
    public class ObjectCasters
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Type,XLua.ObjectCast> castersMap;  // 0x10
        private XLua.ObjectTranslator translator;  // 0x18

        // Methods
        // RVA: 0x03603370  token: 0x60001A3
        public System.Void .ctor(XLua.ObjectTranslator translator) { }
        // RVA: 0x0B8A26EC  token: 0x60001A4
        private static System.Object charCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A29CC  token: 0x60001A5
        private static System.Object sbyteCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A267C  token: 0x60001A6
        private static System.Object byteCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A2A3C  token: 0x60001A7
        private static System.Object shortCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A2AFC  token: 0x60001A8
        private static System.Object ushortCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x034DC170  token: 0x60001A9
        private static System.Object intCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A2AAC  token: 0x60001AA
        private static System.Object uintCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x02F332A0  token: 0x60001AB
        private static System.Object longCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x034DC010  token: 0x60001AC
        private static System.Object ulongCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A28EC  token: 0x60001AD
        private static System.Object getDouble(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x034DC240  token: 0x60001AE
        private static System.Object floatCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A275C  token: 0x60001AF
        private System.Object decimalCaster(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x044606C0  token: 0x60001B0
        private static System.Object getBoolean(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x041A2A70  token: 0x60001B1
        private static System.Object getString(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A27EC  token: 0x60001B2
        private System.Object getBytes(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x0B8A295C  token: 0x60001B3
        private System.Object getIntptr(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x049B7D80  token: 0x60001B4
        private System.Object getObject(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x03606E60  token: 0x60001B5
        private System.Object getLuaTable(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x03606CD0  token: 0x60001B6
        private System.Object getLuaFunction(System.IntPtr L, System.Int32 idx, System.Object target) { }
        // RVA: 0x03602800  token: 0x60001B7
        public System.Void AddCaster(System.Type type, XLua.ObjectCast oc) { }
        // RVA: 0x03603D20  token: 0x60001B8
        private XLua.ObjectCast genCaster(System.Type type) { }
        // RVA: 0x04A30D40  token: 0x60001B9
        private XLua.ObjectCast genNullableCaster(XLua.ObjectCast oc) { }
        // RVA: 0x03298500  token: 0x60001BA
        public XLua.ObjectCast GetCaster(System.Type type) { }

    }

    // TypeToken: 0x200008D  // size: 0x28
    public class ObjectPool
    {
        // Fields
        private static System.Int32 LIST_END;  // const
        private static System.Int32 ALLOCED;  // const
        private XLua.ObjectPool.Slot[] list;  // 0x10
        private System.Int32 freelist;  // 0x18
        private System.Int32 count;  // 0x1c
        private System.Collections.Generic.List<System.Object> needCleanImmediately;  // 0x20

        // Properties
        System.Object Item { get; /* RVA: 0x0B8A7F4C */ }

        // Methods
        // RVA: 0x032F0140  token: 0x60001CA
        public System.Void AddToNeedCleanImmediately(System.Object obj) { }
        // RVA: 0x0B8A7EFC  token: 0x60001CB
        public System.Void Clear() { }
        // RVA: 0x03AC9B40  token: 0x60001CC
        private System.Void extend_capacity() { }
        // RVA: 0x031342D0  token: 0x60001CD
        public System.Int32 Add(System.Object obj) { }
        // RVA: 0x03135180  token: 0x60001CE
        public System.Boolean TryGetValue(System.Int32 index, System.Object& obj) { }
        // RVA: 0x03134170  token: 0x60001CF
        public System.Object Get(System.Int32 index) { }
        // RVA: 0x03134BA0  token: 0x60001D0
        public System.Object Remove(System.Int32 index) { }
        // RVA: 0x031346E0  token: 0x60001D1
        public System.Object Replace(System.Int32 index, System.Object o) { }
        // RVA: 0x03134D10  token: 0x60001D2
        public System.Int32 Check(System.Int32 check_pos, System.Int32 max_check, System.Func<System.Object,System.Boolean> checker, System.Collections.Generic.Dictionary<System.Object,System.Int32> reverse_map) { }
        // RVA: 0x04034FB0  token: 0x60001D3
        public System.Void ClearAllUserdata(System.Func<System.Object,System.Boolean> checker, System.Collections.Generic.Dictionary<System.Object,System.Int32> reverse_map) { }
        // RVA: 0x04CE6E20  token: 0x60001D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008F  // size: 0x10
    public class ReferenceEqualsComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Methods
        // RVA: 0x03C05140  token: 0x60001D6
        public virtual System.Boolean Equals(System.Object o1, System.Object o2) { }
        // RVA: 0x04DA3AC0  token: 0x60001D7
        public virtual System.Int32 GetHashCode(System.Object obj) { }
        // RVA: 0x041E1670  token: 0x60001D8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000090  // size: 0x18
    public class MonoPInvokeCallbackAttribute : System.Attribute
    {
        // Fields
        private System.Type type;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60001D9
        public System.Void .ctor(System.Type t) { }

    }

    // TypeToken: 0x2000091  // size: 0x14
    public sealed struct LuaTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.LuaTypes LUA_TNONE;  // const
        public static XLua.LuaTypes LUA_TNIL;  // const
        public static XLua.LuaTypes LUA_TNUMBER;  // const
        public static XLua.LuaTypes LUA_TSTRING;  // const
        public static XLua.LuaTypes LUA_TBOOLEAN;  // const
        public static XLua.LuaTypes LUA_TTABLE;  // const
        public static XLua.LuaTypes LUA_TFUNCTION;  // const
        public static XLua.LuaTypes LUA_TUSERDATA;  // const
        public static XLua.LuaTypes LUA_TTHREAD;  // const
        public static XLua.LuaTypes LUA_TLIGHTUSERDATA;  // const

    }

    // TypeToken: 0x2000092  // size: 0x14
    public sealed struct LuaGCOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.LuaGCOptions LUA_GCSTOP;  // const
        public static XLua.LuaGCOptions LUA_GCRESTART;  // const
        public static XLua.LuaGCOptions LUA_GCCOLLECT;  // const
        public static XLua.LuaGCOptions LUA_GCCOUNT;  // const
        public static XLua.LuaGCOptions LUA_GCCOUNTB;  // const
        public static XLua.LuaGCOptions LUA_GCSTEP;  // const
        public static XLua.LuaGCOptions LUA_GCSETPAUSE;  // const
        public static XLua.LuaGCOptions LUA_GCSETSTEPMUL;  // const
        public static XLua.LuaGCOptions LUA_GCISRUNNING;  // const
        public static XLua.LuaGCOptions LUA_GCGEN;  // const
        public static XLua.LuaGCOptions LUA_GCINC;  // const

    }

    // TypeToken: 0x2000093  // size: 0x14
    public sealed struct LuaThreadStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.LuaThreadStatus LUA_RESUME_ERROR;  // const
        public static XLua.LuaThreadStatus LUA_OK;  // const
        public static XLua.LuaThreadStatus LUA_YIELD;  // const
        public static XLua.LuaThreadStatus LUA_ERRRUN;  // const
        public static XLua.LuaThreadStatus LUA_ERRSYNTAX;  // const
        public static XLua.LuaThreadStatus LUA_ERRMEM;  // const
        public static XLua.LuaThreadStatus LUA_ERRERR;  // const

    }

    // TypeToken: 0x2000094  // size: 0x10
    public class LuaIndexes
    {
        // Properties
        System.Int32 LUA_REGISTRYINDEX { get; /* RVA: 0x0329B2B0 */ set; /* RVA: 0x04D58F10 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60001DC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000095  // size: 0x128
    public class ObjectTranslator
    {
        // Fields
        public XLua.MethodWrapsCache methodWrapsCache;  // 0x10
        public XLua.ObjectCheckers objectCheckers;  // 0x18
        public XLua.ObjectCasters objectCasters;  // 0x20
        private readonly XLua.ObjectPool objects;  // 0x28
        private readonly System.Collections.Generic.Dictionary<System.Object,System.Int32> reverseMap;  // 0x30
        private XLua.LuaEnv luaEnv;  // 0x38
        private XLua.StaticLuaCallbacks metaFunctions;  // 0x40
        private System.Collections.Generic.List<System.Reflection.Assembly> assemblies;  // 0x48
        private XLua.LuaDLL.lua_CSFunction importTypeFunction;  // 0x50
        private XLua.LuaDLL.lua_CSFunction loadAssemblyFunction;  // 0x58
        private XLua.LuaDLL.lua_CSFunction castFunction;  // 0x60
        private static XLua.LuaDLL.lua_CSFunction listPairFunction;  // static @ 0x0
        private static XLua.LuaDLL.lua_CSFunction dicPairFunction;  // static @ 0x8
        private static XLua.LuaDLL.lua_CSFunction normalPairFunction;  // static @ 0x10
        private readonly System.Collections.Generic.Dictionary<System.Type,System.Action<System.IntPtr>> delayWrap;  // 0x68
        private readonly System.Collections.Generic.Dictionary<System.Type,System.Func<System.Int32,XLua.LuaEnv,XLua.LuaBase>> interfaceBridgeCreators;  // 0x70
        private readonly System.Collections.Generic.Dictionary<System.Type,System.Type> aliasCfg;  // 0x78
        private System.Collections.Generic.Dictionary<System.Type,System.Boolean> loaded_types;  // 0x80
        public System.Int32 cacheRef;  // 0x88
        private System.Reflection.MethodInfo[] genericAction;  // 0x90
        private System.Reflection.MethodInfo[] genericFunc;  // 0x98
        private System.Collections.Generic.Dictionary<System.Type,System.Func<XLua.DelegateBridgeBase,System.Delegate>> delegateCreatorCache;  // 0xa0
        private System.Collections.Generic.Dictionary<System.Int32,System.WeakReference> delegate_bridges;  // 0xa8
        private System.Int32 common_array_meta;  // 0xb0
        private System.Int32 common_delegate_meta;  // 0xb4
        private System.Int32 enumerable_pairs_func;  // 0xb8
        private System.Collections.Generic.Dictionary<System.Type,System.Int32> typeIdMap;  // 0xc0
        private System.Collections.Generic.Dictionary<System.Int32,System.Type> typeMap;  // 0xc8
        private System.Collections.Generic.HashSet<System.Type> privateAccessibleFlags;  // 0xd0
        private System.Collections.Generic.Dictionary<System.Object,System.Int32> enumMap;  // 0xd8
        public static System.Int32 s_luaBuffOffset;  // static @ 0x18
        public static System.Int32 s_nullableValueOffset;  // static @ 0x1c
        private System.Collections.Generic.Dictionary<System.Type,System.Boolean> _unmanagedTypes;  // 0xe0
        private System.Collections.Generic.List<XLua.LuaDLL.lua_CSFunction> fix_cs_functions;  // 0xe8
        private System.Collections.Generic.List<XLua.ObjectTranslator.LuaCSFunctionPtr> fix_cs_function_ptrs;  // 0xf0
        public static System.Boolean enableLuaCSFunctionPtr;  // static @ 0x20
        public static XLua.ObjectTranslator.GetDelegatePointer<System.Object> getDelegatePointer;  // static @ 0x28
        private System.Collections.Generic.Dictionary<System.Type,XLua.ObjectTranslator.PushCSObject> custom_push_funcs;  // 0xf8
        private System.Collections.Generic.Dictionary<System.Type,XLua.ObjectTranslator.GetCSObject> custom_get_funcs;  // 0x100
        private System.Collections.Generic.Dictionary<System.Type,XLua.ObjectTranslator.UpdateCSObject> custom_update_funcs;  // 0x108
        private System.Collections.Generic.Dictionary<System.Type,System.Delegate> push_func_with_type;  // 0x110
        private System.Collections.Generic.Dictionary<System.Type,System.Delegate> get_func_with_type;  // 0x118
        private System.Int32 decimal_type_id;  // 0x120

        // Properties
        XLua.ObjectPool Pools { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0406D660  token: 0x60001DE
        public System.Void DelayWrapLoader(System.Type type, System.Action<System.IntPtr> loader) { }
        // RVA: 0x0439C4B0  token: 0x60001DF
        public System.Void AddInterfaceBridgeCreator(System.Type type, System.Func<System.Int32,XLua.LuaEnv,XLua.LuaBase> creator) { }
        // RVA: 0x036060D0  token: 0x60001E0
        public System.Boolean TryDelayWrapLoader(System.IntPtr L, System.Type type) { }
        // RVA: 0x03B11A90  token: 0x60001E1
        public System.Void Alias(System.Type type, System.String alias) { }
        // RVA: 0x03B0E090  token: 0x60001E2
        private System.Void addAssemblieByName(System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies_usorted, System.String name) { }
        // RVA: 0x03B0E680  token: 0x60001E3
        public System.Void .ctor(XLua.LuaEnv luaenv, System.IntPtr L) { }
        // RVA: 0x041E1670  token: 0x60001E4
        private System.Void initCSharpCallLua() { }
        // RVA: 0x043E6550  token: 0x60001E5
        private System.Func<XLua.DelegateBridgeBase,System.Delegate> getCreatorUsingGeneric(XLua.DelegateBridgeBase bridge, System.Type delegateType, System.Reflection.MethodInfo delegateMethod) { }
        // RVA: 0x03607FA0  token: 0x60001E6
        private System.Delegate getDelegate(XLua.DelegateBridgeBase bridge, System.Type delegateType) { }
        // RVA: 0x03606FF0  token: 0x60001E7
        public System.Object CreateDelegateBridge(System.IntPtr L, System.Type delegateType, System.Int32 idx) { }
        // RVA: 0x0B8A803C  token: 0x60001E8
        public System.Boolean AllDelegateBridgeReleased() { }
        // RVA: 0x032EF3E0  token: 0x60001E9
        public System.Void ReleaseLuaBase(System.IntPtr L, System.Int32 reference, System.Boolean is_delegate) { }
        // RVA: 0x0B8A8188  token: 0x60001EA
        public System.Object CreateInterfaceBridge(System.IntPtr L, System.Type interfaceType, System.Int32 idx) { }
        // RVA: 0x049E51E0  token: 0x60001EB
        public System.Void CreateArrayMetatable(System.IntPtr L) { }
        // RVA: 0x049C0530  token: 0x60001EC
        public System.Void CreateDelegateMetatable(System.IntPtr L) { }
        // RVA: 0x0321AE00  token: 0x60001ED
        private System.Void CreateEnumerablePairs(System.IntPtr L) { }
        // RVA: 0x03FE81A0  token: 0x60001EE
        public static System.Int32 CsPairs(System.IntPtr L) { }
        // RVA: 0x0B8A82DC  token: 0x60001EF
        public static System.Int32 EnumToInt(System.IntPtr L) { }
        // RVA: 0x0321ADB0  token: 0x60001F0
        private System.Void CreateEnumerableCSPairs(System.IntPtr L) { }
        // RVA: 0x04707AA0  token: 0x60001F1
        public System.Void OpenLib(System.IntPtr L) { }
        // RVA: 0x0404E170  token: 0x60001F2
        private System.Void createFunctionMetatable(System.IntPtr L) { }
        // RVA: 0x03B112A0  token: 0x60001F3
        private System.Type FindType(System.String className, System.Boolean isQualifiedName) { }
        // RVA: 0x0B8A8F4C  token: 0x60001F4
        private System.Boolean hasMethod(System.Type type, System.String methodName) { }
        // RVA: 0x02F39F90  token: 0x60001F5
        private System.Void collectObject(System.Int32 obj_index_to_collect) { }
        // RVA: 0x031339F0  token: 0x60001F6
        private System.Int32 addObject(System.Object obj, System.Boolean is_valuetype, System.Boolean is_enum) { }
        // RVA: 0x048FC960  token: 0x60001F7
        private System.Object GetObject(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x03A1B080  token: 0x60001F8
        public System.Type GetTypeOf(System.IntPtr L, System.Int32 idx) { }
        // RVA: -1  // generic def  token: 0x60001F9
        public System.Boolean Assignable(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x03299B30  token: 0x60001FA
        public System.Boolean Assignable(System.IntPtr L, System.Int32 index, System.Type type) { }
        // RVA: 0x031341B0  token: 0x60001FB
        public System.Object FastGetObject(System.IntPtr L, System.Int32 index, System.Type type) { }
        // RVA: 0x05391698  token: 0x60001FC
        public System.Object GetObject(System.IntPtr L, System.Int32 index, System.Type type) { }
        // RVA: -1  // generic def  token: 0x60001FD
        private System.Void _Get(System.IntPtr L, System.Int32 index, T& v) { }
        // RVA: -1  // generic def  token: 0x60001FE
        public System.Void Get(System.IntPtr L, System.Int32 index, T& v) { }
        // RVA: -1  // generic def  token: 0x60001FF
        public System.Void PushByType(System.IntPtr L, T v) { }
        // RVA: -1  // generic def  token: 0x6000200
        public T[] GetParams(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8A843C  token: 0x6000201
        public System.Array GetParams(System.IntPtr L, System.Int32 index, System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000202
        public T GetDelegate(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05391C58  token: 0x6000203
        public System.Int32 GetTypeId(System.IntPtr L, System.Type type) { }
        // RVA: 0x0B8A8888  token: 0x6000204
        public System.Void PrivateAccessible(System.IntPtr L, System.Type type) { }
        // RVA: 0x031333D0  token: 0x6000205
        public System.Int32 getTypeId(System.IntPtr L, System.Type type, System.Boolean& is_first, XLua.ObjectTranslator.LOGLEVEL log_level) { }
        // RVA: 0x02FB7620  token: 0x6000206
        private System.Void pushPrimitive(System.IntPtr L, System.Object o) { }
        // RVA: 0x05391E68  token: 0x6000207
        public System.Void PushObject(System.IntPtr L, System.Object o) { }
        // RVA: 0x02FB8890  token: 0x6000208
        public System.Void PushAny(System.IntPtr L, System.Object o) { }
        // RVA: 0x0B8A8E2C  token: 0x6000209
        public System.Int32 TranslateToEnumToTop(System.IntPtr L, System.Type type, System.Int32 idx) { }
        // RVA: 0x0B8A8C9C  token: 0x600020A
        public System.Void Push(System.IntPtr L, XLua.LuaDLL.lua_CSFunction o) { }
        // RVA: 0x0B8A8C38  token: 0x600020B
        public System.Void Push(System.IntPtr L, XLua.LuaBase o) { }
        // RVA: -1  // generic def  token: 0x600020C
        public System.Void Push(System.IntPtr L, T o) { }
        // RVA: 0x03132DB0  token: 0x600020D
        public System.Void Push(System.IntPtr L, System.Object o) { }
        // RVA: 0x0B8A8B2C  token: 0x600020E
        public System.Void PushObject(System.IntPtr L, System.Object o, System.Int32 type_id) { }
        // RVA: 0x03297170  token: 0x600020F
        public System.Boolean IsUnmanagedType(System.Type type) { }
        // RVA: 0x03296F20  token: 0x6000210
        public System.Void PushBrigeStruct(System.IntPtr L, System.IntPtr ptr, System.Type type, System.Int32 size) { }
        // RVA: 0x03297250  token: 0x6000211
        private System.Void _PushBrigeStruct(System.IntPtr L, System.IntPtr ptr, System.Type type, System.Int32 size) { }
        // RVA: 0x03296BE0  token: 0x6000212
        public System.Void GetBrigeStruct(System.IntPtr L, System.IntPtr ptr, System.Int32 index, System.Int32 size, System.Type type) { }
        // RVA: 0x032F0100  token: 0x6000213
        public System.Void AddNeedRemoveObjRef(System.Object obj) { }
        // RVA: 0x03297390  token: 0x6000214
        private System.Void _GetBrigeStruct(System.IntPtr L, System.IntPtr ptr, System.Int32 index, System.Int32 size, System.Type type) { }
        // RVA: 0x03EE0380  token: 0x6000215
        public System.Void UpdateStructObj(System.IntPtr L, System.IntPtr ptr, System.Int32 index, System.Int32 size, System.Type type) { }
        // RVA: 0x0B8A8178  token: 0x6000216
        public System.Void BeyondUpdateStructObj(System.IntPtr L, System.IntPtr ptr, System.Int32 index, System.Int32 size, System.Type type) { }
        // RVA: 0x03EE0420  token: 0x6000217
        private System.Void _UpdateStructObj(System.IntPtr L, System.IntPtr ptr, System.Int32 index, System.Int32 size, System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000218
        public System.Void Update(System.IntPtr L, System.Int32 index, T obj) { }
        // RVA: 0x0329CA40  token: 0x6000219
        public System.Void Update(System.IntPtr L, System.Int32 index, System.Object obj) { }
        // RVA: 0x03134890  token: 0x600021A
        private System.Object getCsObj(System.IntPtr L, System.Int32 index, System.Int32 udata) { }
        // RVA: 0x03605B50  token: 0x600021B
        private System.Object SafeGetCSObj(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8A8370  token: 0x600021C
        public System.Object FastGetCSObj(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8A8D5C  token: 0x600021D
        private System.Void ReleaseCSObj(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8A83E4  token: 0x600021E
        private XLua.LuaDLL.lua_CSFunction GetFixCSFunction(System.Int32 index) { }
        // RVA: 0x02F39AF0  token: 0x600021F
        private XLua.ObjectTranslator.LuaCSFunctionPtr GetFixCSFunctionPtr(System.Int32 index) { }
        // RVA: 0x03297D70  token: 0x6000220
        private System.Void PushFixCSFunction(System.IntPtr L, XLua.LuaDLL.lua_CSFunction func) { }
        // RVA: 0x03544FA0  token: 0x6000221
        private System.Object[] popValues(System.IntPtr L, System.Int32 oldTop) { }
        // RVA: 0x0B8A8FD0  token: 0x6000222
        private System.Object[] popValues(System.IntPtr L, System.Int32 oldTop, System.Type[] popTypes) { }
        // RVA: 0x02F29E00  token: 0x6000223
        private System.Void registerCustomOp(System.Type type, XLua.ObjectTranslator.PushCSObject push, XLua.ObjectTranslator.GetCSObject get, XLua.ObjectTranslator.UpdateCSObject update) { }
        // RVA: 0x043C05F0  token: 0x6000224
        public System.Boolean HasCustomOp(System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000225
        private System.Boolean tryGetPushFuncByType(System.Type type, T& func) { }
        // RVA: -1  // generic def  token: 0x6000226
        private System.Boolean tryGetGetFuncByType(System.Type type, T& func) { }
        // RVA: -1  // generic def  token: 0x6000227
        public System.Void RegisterPushAndGetAndUpdate(System.Action<System.IntPtr,T> push, XLua.ObjectTranslator.GetFunc<T> get, System.Action<System.IntPtr,System.Int32,T> update) { }
        // RVA: -1  // generic def  token: 0x6000228
        public System.Void RegisterChecker(XLua.ObjectTranslator.CheckFunc<T> check) { }
        // RVA: -1  // generic def  token: 0x6000229
        public System.Void RegisterCaster(XLua.ObjectTranslator.GetFunc<T> get) { }
        // RVA: 0x0B8A896C  token: 0x600022A
        public System.Void PushDecimal(System.IntPtr L, System.Decimal val) { }
        // RVA: 0x0B8A8818  token: 0x600022B
        public System.Boolean IsDecimal(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x07766224  token: 0x600022C
        public System.Decimal GetDecimal(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8A8538  token: 0x600022D
        public System.Void Get(System.IntPtr L, System.Int32 index, System.Decimal& val) { }
        // RVA: 0x04B0D4C0  token: 0x600022E
        private static System.Void .cctor() { }
        // RVA: -1  // generic def  token: 0x600022F
        private System.Decimal <tryGetGetFuncByType>b__125_0(System.IntPtr L, System.Int32 idx) { }

    }

    // TypeToken: 0x20000AB  // size: 0x28
    public class ObjectTranslatorPool
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.IntPtr,System.WeakReference> translators;  // 0x10
        private System.IntPtr lastPtr;  // 0x18
        private XLua.ObjectTranslator lastTranslator;  // 0x20
        public static XLua.ObjectTranslatorPool Instance;  // static @ 0x0

        // Methods
        // RVA: 0x04CB69A0  token: 0x6000277
        public System.Void .ctor() { }
        // RVA: 0x04CB0ED0  token: 0x6000278
        public System.Void Add(System.IntPtr L, XLua.ObjectTranslator translator) { }
        // RVA: 0x04D862C0  token: 0x6000279
        public XLua.ObjectTranslator Find(System.IntPtr L) { }
        // RVA: 0x0B8A7F80  token: 0x600027A
        public System.Void Remove(System.IntPtr L) { }
        // RVA: 0x04CB6930  token: 0x600027B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AC
    public interface RawObject
    {
        // Properties
        System.Object Target { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x20000AD  // size: 0x10
    public class InternalUtility
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Type,System.String> s_baseValueTypeDic;  // static @ 0x0

        // Methods
        // RVA: 0x02F2BF90  token: 0x600027D
        public static System.String GetTypeGenericSignatureDefine(System.Type type, System.Boolean isGetType, System.Boolean isRef, System.Boolean isOut, System.Boolean needLua, System.Boolean keepType, System.Boolean needEnum) { }
        // RVA: 0x02F2BCB0  token: 0x600027E
        public static System.Boolean CheckRegisterBaseStructOnArm(System.Type type) { }
        // RVA: 0x040B35E0  token: 0x600027F
        public static System.Boolean CheckCustomDelegate(System.String name) { }
        // RVA: 0x0B8A7564  token: 0x6000280
        public static System.Boolean CheckTypNeedToGen(System.Type type) { }
        // RVA: 0x0B8A7E48  token: 0x6000281
        public static System.Boolean IsPublic(System.Type type) { }
        // RVA: 0x035CCBA0  token: 0x6000282
        public static System.String GetTypeFullName(System.Type t) { }
        // RVA: 0x0B8A7698  token: 0x6000283
        public static System.String GetDelegateName(System.Reflection.MethodInfo method) { }
        // RVA: 0x02F2BE60  token: 0x6000284
        public static System.Boolean IsStruct(System.Type type) { }
        // RVA: 0x0B8A7BD0  token: 0x6000285
        public static System.String GetGenericTypeName(System.Type type, System.Boolean needPoint) { }
        // RVA: 0x02F2C220  token: 0x6000286
        public static System.String GeNormalName(System.String str) { }
        // RVA: 0x0B8A7A20  token: 0x6000287
        public static System.String GetGenericTypeNameString(System.Type type) { }
        // RVA: 0x041E1670  token: 0x6000288
        public System.Void .ctor() { }
        // RVA: 0x045B0DD0  token: 0x6000289
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AE  // size: 0x10
    public class MethodAndDelegateSignatureManager
    {
        // Methods
        // RVA: 0x03895320  token: 0x600028A
        public static System.Type MakeMethodDelegateType(System.Reflection.MethodInfo method) { }
        // RVA: 0x03897230  token: 0x600028B
        public static System.Type MakeMethodDelegateType(System.Reflection.MethodInfo method, System.Type customDelegateType) { }
        // RVA: 0x02F2C330  token: 0x600028C
        public static System.ValueTuple<System.Type,System.String> GetMethodDelegateType(System.Reflection.MethodInfo method) { }
        // RVA: 0x041E1670  token: 0x600028D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AF  // size: 0x28
    public class SignatureLoader
    {
        // Fields
        private XLua.LuaEnv.CustomLoader userLoader;  // 0x10
        private System.Security.Cryptography.RSACryptoServiceProvider rsa;  // 0x18
        private System.Security.Cryptography.SHA1 sha;  // 0x20

        // Methods
        // RVA: 0x0B8A91D4  token: 0x600028E
        public System.Void .ctor(System.String publicKey, XLua.LuaEnv.CustomLoader loader) { }
        // RVA: 0x0B8A92D4  token: 0x600028F
        private System.Byte[] load_and_verify(System.String& filepath) { }
        // RVA: 0x0B8A94B4  token: 0x6000290
        public static XLua.LuaEnv.CustomLoader op_Implicit(XLua.SignatureLoader signatureLoader) { }

    }

    // TypeToken: 0x20000B0
    public sealed class CallDelegatePointer`3 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000291
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000292
        public virtual TRet Invoke(System.IntPtr ptr, TParam1 target, TParam2 param) { }
        // RVA: -1  // runtime  token: 0x6000293
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr ptr, TParam1 target, TParam2 param, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000294
        public virtual TRet EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000B1  // size: 0x48
    public class StaticLuaCallbacks
    {
        // Fields
        private XLua.LuaDLL.lua_CSFunction GcMeta;  // 0x10
        private XLua.LuaDLL.lua_CSFunction ToStringMeta;  // 0x18
        private XLua.LuaDLL.lua_CSFunction EnumAndMeta;  // 0x20
        private XLua.LuaDLL.lua_CSFunction EnumOrMeta;  // 0x28
        private XLua.LuaDLL.lua_CSFunction StaticCSFunctionWraper;  // 0x30
        private XLua.LuaDLL.lua_CSFunction FixCSFunctionWraper;  // 0x38
        private XLua.LuaDLL.lua_CSFunction DelegateCtor;  // 0x40
        public static XLua.CallDelegatePointer<System.Object,System.IntPtr,System.Int32> callDelegatePointer;  // static @ 0x0

        // Methods
        // RVA: 0x049B2490  token: 0x6000295
        public System.Void .ctor() { }
        // RVA: 0x0B8AA308  token: 0x6000296
        public static System.Int32 EnumAnd(System.IntPtr L) { }
        // RVA: 0x0B8AA590  token: 0x6000297
        public static System.Int32 EnumOr(System.IntPtr L) { }
        // RVA: 0x0B8AC200  token: 0x6000298
        private static System.Int32 StaticCSFunction(System.IntPtr L) { }
        // RVA: 0x02F39600  token: 0x6000299
        private static System.Int32 FixCSFunction(System.IntPtr L) { }
        // RVA: 0x0B8A9C7C  token: 0x600029A
        public static System.Int32 DelegateCall(System.IntPtr L) { }
        // RVA: 0x02F39B70  token: 0x600029B
        public static System.Int32 LuaGC(System.IntPtr L) { }
        // RVA: 0x043DF130  token: 0x600029C
        public static System.Int32 ToString(System.IntPtr L) { }
        // RVA: 0x0B8A9E6C  token: 0x600029D
        public static System.Int32 DelegateCombine(System.IntPtr L) { }
        // RVA: 0x04611010  token: 0x600029E
        public static System.Int32 DelegateRemove(System.IntPtr L) { }
        // RVA: 0x03FD2B40  token: 0x600029F
        private static System.Boolean tryPrimitiveArrayGet(System.Type type, System.IntPtr L, System.Object obj, System.Int32 index) { }
        // RVA: 0x03FD2800  token: 0x60002A0
        public static System.Int32 ArrayIndexer(System.IntPtr L) { }
        // RVA: 0x0B8AC724  token: 0x60002A1
        public static System.Boolean TryPrimitiveArraySet(System.Type type, System.IntPtr L, System.Object obj, System.Int32 array_idx, System.Int32 obj_idx) { }
        // RVA: 0x0B8A9528  token: 0x60002A2
        public static System.Int32 ArrayNewIndexer(System.IntPtr L) { }
        // RVA: 0x0459E650  token: 0x60002A3
        public static System.Int32 ArrayLength(System.IntPtr L) { }
        // RVA: 0x042F06D0  token: 0x60002A4
        public static System.Int32 MetaFuncIndex(System.IntPtr L) { }
        // RVA: 0x0B8ABD38  token: 0x60002A5
        private static System.Int32 Panic(System.IntPtr L) { }
        // RVA: 0x0B8ABDD4  token: 0x60002A6
        private static System.Int32 Print(System.IntPtr L) { }
        // RVA: 0x0B8ABCF0  token: 0x60002A7
        private static System.Int32 LoadSocketCore(System.IntPtr L) { }
        // RVA: 0x0B8AB674  token: 0x60002A8
        private static System.Int32 LoadCS(System.IntPtr L) { }
        // RVA: 0x04564CC0  token: 0x60002A9
        private static System.Int32 LoadBuiltinLib(System.IntPtr L) { }
        // RVA: 0x0B8AB6E0  token: 0x60002AA
        private static System.Int32 LoadFromResource(System.IntPtr L) { }
        // RVA: 0x0B8AB9F4  token: 0x60002AB
        private static System.Int32 LoadFromStreamingAssetsPath(System.IntPtr L) { }
        // RVA: 0x03600F50  token: 0x60002AC
        private static System.Int32 LoadFromCustomLoaders(System.IntPtr L) { }
        // RVA: 0x0B8AB3E8  token: 0x60002AD
        public static System.Int32 LoadAssembly(System.IntPtr L) { }
        // RVA: 0x03B11160  token: 0x60002AE
        public static System.Int32 ImportType(System.IntPtr L) { }
        // RVA: 0x0B8AB008  token: 0x60002AF
        public static System.Int32 ImportGenericType(System.IntPtr L) { }
        // RVA: 0x0B8A99D8  token: 0x60002B0
        public static System.Int32 Cast(System.IntPtr L) { }
        // RVA: 0x0B8AD9D8  token: 0x60002B1
        private static System.Type getType(System.IntPtr L, XLua.ObjectTranslator translator, System.Int32 idx) { }
        // RVA: 0x0B8AD048  token: 0x60002B2
        public static System.Int32 XLuaAccess(System.IntPtr L) { }
        // RVA: 0x0B8AD7D0  token: 0x60002B3
        public static System.Int32 XLuaPrivateAccessible(System.IntPtr L) { }
        // RVA: 0x0B8AD4CC  token: 0x60002B4
        public static System.Int32 XLuaMetatableOperation(System.IntPtr L) { }
        // RVA: 0x0B8AA0CC  token: 0x60002B5
        public static System.Int32 DelegateConstructor(System.IntPtr L) { }
        // RVA: 0x0B8AC3AC  token: 0x60002B6
        public static System.Int32 ToFunction(System.IntPtr L) { }
        // RVA: 0x0B8AA818  token: 0x60002B7
        public static System.Int32 GenericMethodWraper(System.IntPtr L) { }
        // RVA: 0x0B8AAC00  token: 0x60002B8
        public static System.Int32 GetGenericMethod(System.IntPtr L) { }
        // RVA: 0x0B8AC0B8  token: 0x60002B9
        public static System.Int32 ReleaseCsObject(System.IntPtr L) { }

    }

    // TypeToken: 0x20000B2  // size: 0x10
    public static class TypeExtensions
    {
        // Methods
        // RVA: 0x03131F30  token: 0x60002BA
        public static System.Boolean IsValueType(System.Type type) { }
        // RVA: 0x02F3A240  token: 0x60002BB
        public static System.Boolean IsEnum(System.Type type) { }
        // RVA: 0x02F2AB50  token: 0x60002BC
        public static System.Boolean IsPrimitive(System.Type type) { }
        // RVA: 0x035CFFA0  token: 0x60002BD
        public static System.Boolean IsAbstract(System.Type type) { }
        // RVA: 0x0B8B1724  token: 0x60002BE
        public static System.Boolean IsSealed(System.Type type) { }
        // RVA: 0x035CFF60  token: 0x60002BF
        public static System.Boolean IsInterface(System.Type type) { }
        // RVA: 0x035CE330  token: 0x60002C0
        public static System.Boolean IsClass(System.Type type) { }
        // RVA: 0x03607F30  token: 0x60002C1
        public static System.Type BaseType(System.Type type) { }
        // RVA: 0x04D83A60  token: 0x60002C2
        public static System.Boolean IsGenericType(System.Type type) { }
        // RVA: 0x035CFF80  token: 0x60002C3
        public static System.Boolean IsGenericTypeDefinition(System.Type type) { }
        // RVA: 0x0B8B16EC  token: 0x60002C4
        public static System.Boolean IsNestedPublic(System.Type type) { }
        // RVA: 0x0B8B1708  token: 0x60002C5
        public static System.Boolean IsPublic(System.Type type) { }
        // RVA: 0x0B8B11F4  token: 0x60002C6
        public static System.String GetFriendlyName(System.Type type) { }

    }

    // TypeToken: 0x20000B4  // size: 0x14
    public sealed struct LazyMemberTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.LazyMemberTypes Method;  // const
        public static XLua.LazyMemberTypes FieldGet;  // const
        public static XLua.LazyMemberTypes FieldSet;  // const
        public static XLua.LazyMemberTypes PropertyGet;  // const
        public static XLua.LazyMemberTypes PropertySet;  // const
        public static XLua.LazyMemberTypes Event;  // const

    }

    // TypeToken: 0x20000B5  // size: 0x10
    public static class Utils
    {
        // Fields
        public static System.Boolean s_xluaReflectionOpt;  // static @ 0x0
        public static System.Int32 OBJ_META_IDX;  // const
        public static System.Int32 METHOD_IDX;  // const
        public static System.Int32 GETTER_IDX;  // const
        public static System.Int32 SETTER_IDX;  // const
        public static System.Int32 CLS_IDX;  // const
        public static System.Int32 CLS_META_IDX;  // const
        public static System.Int32 CLS_GETTER_IDX;  // const
        public static System.Int32 CLS_SETTER_IDX;  // const
        public static System.String LuaIndexsFieldName;  // const
        public static System.String LuaNewIndexsFieldName;  // const
        public static System.String LuaClassIndexsFieldName;  // const
        public static System.String LuaClassNewIndexsFieldName;  // const

        // Methods
        // RVA: 0x0B8B561C  token: 0x60002CA
        public static System.Boolean LoadField(System.IntPtr L, System.Int32 idx, System.String field_name) { }
        // RVA: 0x0B8B4570  token: 0x60002CB
        public static System.IntPtr GetMainState(System.IntPtr L) { }
        // RVA: 0x0B8B4338  token: 0x60002CC
        public static System.Collections.Generic.List<System.Type> GetAllTypes(System.Boolean exclude_generic_definition) { }
        // RVA: 0x0B8B5E40  token: 0x60002CD
        private static XLua.LuaDLL.lua_CSFunction genFieldGetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x02F29FB0  token: 0x60002CE
        private static XLua.LuaDLL.lua_CSFunction genFieldGetterByPointer(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x0B8B462C  token: 0x60002CF
        public static System.Boolean IsNullableType(System.Type type) { }
        // RVA: 0x0B8B5F34  token: 0x60002D0
        private static XLua.LuaDLL.lua_CSFunction genFieldSetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x02F2A2E0  token: 0x60002D1
        private static XLua.LuaDLL.lua_CSFunction genFieldSetterByPointer(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x04074AC0  token: 0x60002D2
        private static XLua.LuaDLL.lua_CSFunction genItemGetterOpt(System.Type type, System.Reflection.PropertyInfo[] props, XLua.ObjectTranslator translator) { }
        // RVA: 0x0B8B6028  token: 0x60002D3
        private static XLua.LuaDLL.lua_CSFunction genItemGetter(System.Type type, System.Reflection.PropertyInfo[] props) { }
        // RVA: 0x04074F00  token: 0x60002D4
        private static XLua.LuaDLL.lua_CSFunction genItemSetterOpt(System.Type type, System.Reflection.PropertyInfo[] props, XLua.ObjectTranslator translator) { }
        // RVA: 0x0B8B62E4  token: 0x60002D5
        private static XLua.LuaDLL.lua_CSFunction genItemSetter(System.Type type, System.Reflection.PropertyInfo[] props) { }
        // RVA: 0x0468A000  token: 0x60002D6
        private static XLua.LuaDLL.lua_CSFunction genEnumCastFrom(System.Type type) { }
        // RVA: 0x0408E7B0  token: 0x60002D7
        private static System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> GetExtensionMethodsOf(System.Type type_to_be_extend) { }
        // RVA: 0x02F86380  token: 0x60002D8
        private static System.Void makeReflectionWrap(System.IntPtr L, System.Type type, System.Int32 cls_field, System.Int32 cls_getter, System.Int32 cls_setter, System.Int32 obj_field, System.Int32 obj_getter, System.Int32 obj_setter, System.Int32 obj_meta, XLua.LuaDLL.lua_CSFunction& item_getter, XLua.LuaDLL.lua_CSFunction& item_setter, System.Reflection.BindingFlags access) { }
        // RVA: 0x0B8B66C4  token: 0x60002D9
        public static System.Void loadUpvalue(System.IntPtr L, System.Type type, System.String metafunc, System.Int32 index) { }
        // RVA: 0x036FDEF0  token: 0x60002DA
        public static System.Void RegisterEnumType(System.IntPtr L, System.Type type) { }
        // RVA: 0x0B8B5818  token: 0x60002DB
        public static System.Void MakePrivateAccessible(System.IntPtr L, System.Type type) { }
        // RVA: 0x0B8B49A0  token: 0x60002DC
        private static System.Int32 LazyReflectionCall(System.IntPtr L) { }
        // RVA: 0x03607450  token: 0x60002DD
        public static System.Void ReflectionWrap(System.IntPtr L, System.Type type, System.Boolean privateAccessible) { }
        // RVA: 0x032EE950  token: 0x60002DE
        public static System.Void BeginObjectRegister(System.Type type, System.IntPtr L, XLua.ObjectTranslator translator, System.Int32 meta_count, System.Int32 method_count, System.Int32 getter_count, System.Int32 setter_count, System.Int32 type_id) { }
        // RVA: 0x03296720  token: 0x60002DF
        private static System.Int32 abs_idx(System.Int32 top, System.Int32 idx) { }
        // RVA: 0x036065E0  token: 0x60002E0
        public static System.Void EndObjectRegister(System.Type type, System.IntPtr L, XLua.ObjectTranslator translator, XLua.LuaDLL.lua_CSFunction csIndexer, XLua.LuaDLL.lua_CSFunction csNewIndexer, System.Type base_type, XLua.LuaDLL.lua_CSFunction arrayIndexer, XLua.LuaDLL.lua_CSFunction arrayNewIndexer) { }
        // RVA: 0x032965C0  token: 0x60002E1
        public static System.Void RegisterFunc(System.IntPtr L, System.Int32 idx, System.String name, XLua.LuaDLL.lua_CSFunction func) { }
        // RVA: 0x0B8B5CD8  token: 0x60002E2
        public static System.Void RegisterLazyFunc(System.IntPtr L, System.Int32 idx, System.String name, System.Type type, XLua.LazyMemberTypes memberType, System.Boolean isStatic) { }
        // RVA: 0x03296260  token: 0x60002E3
        public static System.Void RegisterObject(System.IntPtr L, XLua.ObjectTranslator translator, System.Int32 idx, System.String name, System.Object obj) { }
        // RVA: 0x03605350  token: 0x60002E4
        public static System.Void BeginClassRegister(System.Type type, System.IntPtr L, XLua.LuaDLL.lua_CSFunction creator, System.Int32 class_field_count, System.Int32 static_getter_count, System.Int32 static_setter_count) { }
        // RVA: 0x03606390  token: 0x60002E5
        public static System.Void EndClassRegister(System.Type type, System.IntPtr L, XLua.ObjectTranslator translator) { }
        // RVA: 0x035AA040  token: 0x60002E6
        private static System.Collections.Generic.List<System.String> getPathOfType(System.Type type) { }
        // RVA: 0x035A82B0  token: 0x60002E7
        public static System.Void LoadCSTable(System.IntPtr L, System.Type type) { }
        // RVA: 0x03030C20  token: 0x60002E8
        public static System.Void SetCSTable(System.IntPtr L, System.Type type, System.Int32 cls_table) { }
        // RVA: 0x0463E4F0  token: 0x60002E9
        public static System.Boolean IsParamsMatch(System.Reflection.MethodInfo delegateMethod, System.Reflection.MethodInfo bridgeMethod) { }
        // RVA: 0x0B8B47C0  token: 0x60002EA
        public static System.Boolean IsSupportedMethod(System.Reflection.MethodInfo method) { }
        // RVA: 0x0B8B56B0  token: 0x60002EB
        public static System.Reflection.MethodInfo MakeGenericMethodWithConstraints(System.Reflection.MethodInfo method) { }
        // RVA: 0x0B8B65A0  token: 0x60002EC
        private static System.Type getExtendedType(System.Reflection.MethodInfo method) { }
        // RVA: 0x03601AA0  token: 0x60002ED
        public static System.Boolean IsStaticPInvokeCSFunction(XLua.LuaDLL.lua_CSFunction csFunction) { }
        // RVA: 0x0B8B46C0  token: 0x60002EE
        public static System.Boolean IsPublic(System.Type type) { }
        // RVA: 0x04D79790  token: 0x60002EF
        private static System.Void .cctor() { }

    }

}

namespace XLua.Cast
{

    // TypeToken: 0x20000DF
    public class Any`1 : XLua.RawObject
    {
        // Fields
        private T mTarget;  // 0x0

        // Properties
        System.Object Target { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000378
        public System.Void .ctor(T i) { }

    }

    // TypeToken: 0x20000E0  // size: 0x18
    public class Byte : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8AE354  token: 0x600037A
        public System.Void .ctor(System.Byte i) { }

    }

    // TypeToken: 0x20000E1  // size: 0x18
    public class SByte : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8B1168  token: 0x600037B
        public System.Void .ctor(System.SByte i) { }

    }

    // TypeToken: 0x20000E2  // size: 0x18
    public class Char : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8AE394  token: 0x600037C
        public System.Void .ctor(System.Char i) { }

    }

    // TypeToken: 0x20000E3  // size: 0x18
    public class Int16 : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8AF1FC  token: 0x600037D
        public System.Void .ctor(System.Int16 i) { }

    }

    // TypeToken: 0x20000E4  // size: 0x18
    public class UInt16 : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8B4270  token: 0x600037E
        public System.Void .ctor(System.UInt16 i) { }

    }

    // TypeToken: 0x20000E5  // size: 0x18
    public class Int32 : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8AF240  token: 0x600037F
        public System.Void .ctor(System.Int32 i) { }

    }

    // TypeToken: 0x20000E6  // size: 0x18
    public class UInt32 : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8B42B4  token: 0x6000380
        public System.Void .ctor(System.UInt32 i) { }

    }

    // TypeToken: 0x20000E7  // size: 0x18
    public class Int64 : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8AF280  token: 0x6000381
        public System.Void .ctor(System.Int64 i) { }

    }

    // TypeToken: 0x20000E8  // size: 0x18
    public class UInt64 : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8B42F4  token: 0x6000382
        public System.Void .ctor(System.UInt64 i) { }

    }

    // TypeToken: 0x20000E9  // size: 0x18
    public class Float : XLua.Cast.Any`1
    {
        // Methods
        // RVA: 0x0B8AE484  token: 0x6000383
        public System.Void .ctor(System.Single i) { }

    }

}

namespace XLua.LuaDLL
{

    // TypeToken: 0x20000EA  // size: 0x80
    public sealed class lua_CSFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F29090  token: 0x6000384
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x6000385
        public virtual System.Int32 Invoke(System.IntPtr L) { }
        // RVA: 0x0B8B6B40  token: 0x6000386
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000387
        public virtual System.Int32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000EB  // size: 0x10
    public static class Lua
    {
        // Fields
        private static System.String LUADLL;  // const
        private static System.Int32 s_mainThreadId;  // static @ 0x0
        public static System.Func<System.IntPtr,System.String> tryGetLuaStringFunc;  // static @ 0x8
        public static System.Action<System.IntPtr,System.IntPtr,System.Int32,System.String> refLuaStringFunc;  // static @ 0x10
        public static System.Int32 LUAI_MAXSHORTLEN;  // const

        // Methods
        // RVA: 0x04D4DFB0  token: 0x6000388
        private static System.Void .cctor() { }
        // RVA: 0x05391F08  token: 0x6000389
        public static System.IntPtr lua_tothread(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05393214  token: 0x600038A
        public static System.Int32 xlua_get_lib_version() { }
        // RVA: 0x05393E50  token: 0x600038B
        public static System.Int32 lua_gc(System.IntPtr L, XLua.LuaGCOptions what, System.Int32 data) { }
        // RVA: 0x0B8AFE9C  token: 0x600038C
        public static System.IntPtr lua_getupvalue(System.IntPtr L, System.Int32 funcindex, System.Int32 n) { }
        // RVA: 0x0B8AFFC8  token: 0x600038D
        public static System.IntPtr lua_setupvalue(System.IntPtr L, System.Int32 funcindex, System.Int32 n) { }
        // RVA: 0x05391F10  token: 0x600038E
        public static System.Int32 lua_pushthread(System.IntPtr L) { }
        // RVA: 0x03607F50  token: 0x600038F
        public static System.Boolean lua_isfunction(System.IntPtr L, System.Int32 stackPos) { }
        // RVA: 0x0B8AFF0C  token: 0x6000390
        public static System.Boolean lua_islightuserdata(System.IntPtr L, System.Int32 stackPos) { }
        // RVA: 0x032989B0  token: 0x6000391
        public static System.Boolean lua_istable(System.IntPtr L, System.Int32 stackPos) { }
        // RVA: 0x032F1A20  token: 0x6000392
        public static System.Boolean lua_isthread(System.IntPtr L, System.Int32 stackPos) { }
        // RVA: 0x0B8AFCFC  token: 0x6000393
        public static System.Int32 luaL_error(System.IntPtr L, System.String message) { }
        // RVA: 0x0B8AFF80  token: 0x6000394
        public static System.Int32 lua_setfenv(System.IntPtr L, System.Int32 stackPos) { }
        // RVA: 0x0539323C  token: 0x6000395
        public static System.IntPtr luaL_newstate() { }
        // RVA: 0x0B8AFDF4  token: 0x6000396
        public static System.Void lua_close(System.IntPtr L) { }
        // RVA: 0x053931EC  token: 0x6000397
        public static System.Void luaopen_xlua(System.IntPtr L) { }
        // RVA: 0x0B8AFDE4  token: 0x6000398
        public static System.Void luaL_openlibs(System.IntPtr L) { }
        // RVA: 0x0539324C  token: 0x6000399
        public static System.UInt32 xlua_objlen(System.IntPtr L, System.Int32 stackPos) { }
        // RVA: 0x053932E0  token: 0x600039A
        public static System.Void lua_createtable(System.IntPtr L, System.Int32 narr, System.Int32 nrec) { }
        // RVA: 0x03607EE0  token: 0x600039B
        public static System.Void lua_newtable(System.IntPtr L, System.Int32 arrayL, System.Int32 hashL) { }
        // RVA: 0x036085E0  token: 0x600039C
        public static System.Int32 xlua_getglobal(System.IntPtr L, System.String name) { }
        // RVA: 0x03600CB0  token: 0x600039D
        public static System.Int32 xlua_setglobal(System.IntPtr L, System.String name) { }
        // RVA: 0x05393244  token: 0x600039E
        public static System.Void xlua_getloaders(System.IntPtr L) { }
        // RVA: 0x05391D14  token: 0x600039F
        public static System.Void lua_settop(System.IntPtr L, System.Int32 newTop) { }
        // RVA: 0x0329D960  token: 0x60003A0
        public static System.Void lua_pop(System.IntPtr L, System.Int32 amount) { }
        // RVA: 0x05393ED0  token: 0x60003A1
        public static System.Void lua_insert(System.IntPtr L, System.Int32 newTop) { }
        // RVA: 0x05390E48  token: 0x60003A2
        public static System.Void lua_remove(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x053932D8  token: 0x60003A3
        public static System.Int32 lua_rawget(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05390AAC  token: 0x60003A4
        public static System.Void lua_rawset(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0539327C  token: 0x60003A5
        public static System.Int32 lua_setmetatable(System.IntPtr L, System.Int32 objIndex) { }
        // RVA: 0x0B8AFF70  token: 0x60003A6
        public static System.Int32 lua_rawequal(System.IntPtr L, System.Int32 index1, System.Int32 index2) { }
        // RVA: 0x053932D0  token: 0x60003A7
        public static System.Void lua_pushvalue(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8AFF68  token: 0x60003A8
        public static System.Void lua_pushcclosure(System.IntPtr L, System.IntPtr fn, System.Int32 n) { }
        // RVA: 0x0B8AFF78  token: 0x60003A9
        public static System.Void lua_replace(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05391CCC  token: 0x60003AA
        public static System.Int32 lua_gettop(System.IntPtr L) { }
        // RVA: 0x05391CAC  token: 0x60003AB
        public static XLua.LuaTypes lua_type(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x03299E00  token: 0x60003AC
        public static System.Boolean lua_isnil(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x02F34150  token: 0x60003AD
        public static System.Boolean lua_isnumber(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8AFEA4  token: 0x60003AE
        public static System.Boolean lua_isboolean(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x053932AC  token: 0x60003AF
        public static System.Int32 luaL_ref(System.IntPtr L, System.Int32 registryIndex) { }
        // RVA: 0x03606FA0  token: 0x60003B0
        public static System.Int32 luaL_ref(System.IntPtr L) { }
        // RVA: 0x05391CC4  token: 0x60003B1
        public static System.Void xlua_rawgeti(System.IntPtr L, System.Int32 tableIndex, System.Int64 index) { }
        // RVA: 0x05395328  token: 0x60003B2
        public static System.Void xlua_rawseti(System.IntPtr L, System.Int32 tableIndex, System.Int64 index) { }
        // RVA: 0x0329B230  token: 0x60003B3
        public static System.Void lua_getref(System.IntPtr L, System.Int32 reference) { }
        // RVA: 0x05391D0C  token: 0x60003B4
        public static System.Int32 pcall_prepare(System.IntPtr L, System.Int32 error_func_ref, System.Int32 func_ref) { }
        // RVA: 0x05391F00  token: 0x60003B5
        public static System.Void luaL_unref(System.IntPtr L, System.Int32 registryIndex, System.Int32 reference) { }
        // RVA: 0x032EF630  token: 0x60003B6
        public static System.Void lua_unref(System.IntPtr L, System.Int32 reference) { }
        // RVA: 0x03606CB0  token: 0x60003B7
        public static System.Boolean lua_isstring(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x03F70F90  token: 0x60003B8
        public static System.Boolean lua_isinteger(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05391EF8  token: 0x60003B9
        public static System.Void lua_pushnil(System.IntPtr L) { }
        // RVA: 0x03298170  token: 0x60003BA
        public static System.Void lua_pushstdcallcfunction(System.IntPtr L, XLua.LuaDLL.lua_CSFunction function, System.Int32 n) { }
        // RVA: 0x053909D0  token: 0x60003BB
        public static System.Int32 xlua_upvalueindex(System.Int32 n) { }
        // RVA: 0x0329E9F0  token: 0x60003BC
        public static System.Int32 lua_pcall(System.IntPtr L, System.Int32 nArgs, System.Int32 nResults, System.Int32 errfunc) { }
        // RVA: 0x05391D24  token: 0x60003BD
        public static System.Int32 _lua_pcall(System.IntPtr L, System.Int32 nArgs, System.Int32 nResults, System.Int32 errfunc) { }
        // RVA: 0x0539097C  token: 0x60003BE
        public static System.Double lua_tonumber(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05390990  token: 0x60003BF
        public static System.Int32 xlua_tointeger(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05391444  token: 0x60003C0
        public static System.UInt32 xlua_touint(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0339E8E0  token: 0x60003C1
        public static System.Boolean lua_toboolean(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x0B8B00AC  token: 0x60003C2
        public static System.IntPtr lua_topointer(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05391C9C  token: 0x60003C3
        public static System.IntPtr lua_tolstring(System.IntPtr L, System.Int32 index, System.IntPtr& strLen) { }
        // RVA: 0x03298C90  token: 0x60003C4
        public static System.String lua_tostring_beyond(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x03298CD0  token: 0x60003C5
        public static System.String lua_tostring(System.IntPtr L, System.Int32 index, System.Boolean cache) { }
        // RVA: 0x03826DF0  token: 0x60003C6
        public static System.IntPtr lua_atpanic(System.IntPtr L, XLua.LuaDLL.lua_CSFunction panicf) { }
        // RVA: 0x05391D1C  token: 0x60003C7
        public static System.Void lua_pushnumber(System.IntPtr L, System.Double number) { }
        // RVA: 0x0329A510  token: 0x60003C8
        public static System.Void lua_pushboolean(System.IntPtr L, System.Boolean value) { }
        // RVA: 0x03298160  token: 0x60003C9
        public static System.Void xlua_pushinteger(System.IntPtr L, System.Int32 value) { }
        // RVA: 0x05392CE0  token: 0x60003CA
        public static System.Void xlua_pushuint(System.IntPtr L, System.UInt32 value) { }
        // RVA: 0x02F84530  token: 0x60003CB
        public static System.Void lua_pushstring(System.IntPtr L, System.String str) { }
        // RVA: 0x02F847C0  token: 0x60003CC
        public static System.Void xlua_pushlstring(System.IntPtr L, System.Byte[] str, System.Int32 size) { }
        // RVA: 0x02F847E0  token: 0x60003CD
        public static System.Void xlua_pushasciistring(System.IntPtr L, System.String str) { }
        // RVA: 0x0432B1E0  token: 0x60003CE
        public static System.Void lua_pushstring(System.IntPtr L, System.Byte[] str) { }
        // RVA: 0x0B8AFFD0  token: 0x60003CF
        public static System.Byte[] lua_tobytes(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x03606330  token: 0x60003D0
        public static System.Int32 luaL_newmetatable(System.IntPtr L, System.String meta) { }
        // RVA: 0x05391CA4  token: 0x60003D1
        public static System.Int32 xlua_pgettable(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x05391D04  token: 0x60003D2
        public static System.Int32 xlua_psettable(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x03606E10  token: 0x60003D3
        public static System.Void luaL_getmetatable(System.IntPtr L, System.String meta) { }
        // RVA: 0x03601710  token: 0x60003D4
        public static System.Int32 xluaL_loadbuffer(System.IntPtr L, System.Byte[] buff, System.Int32 size, System.String name) { }
        // RVA: 0x0B8AFD50  token: 0x60003D5
        public static System.Int32 luaL_loadbuffer(System.IntPtr L, System.String buff, System.String name) { }
        // RVA: 0x053909D8  token: 0x60003D6
        public static System.Int32 xlua_tocsobj_safe(System.IntPtr L, System.Int32 obj) { }
        // RVA: 0x05391314  token: 0x60003D7
        public static System.Int32 xlua_tocsobj_fast(System.IntPtr L, System.Int32 obj) { }
        // RVA: 0x0B8AFDFC  token: 0x60003D8
        public static System.Int32 lua_error(System.IntPtr L) { }
        // RVA: 0x03606370  token: 0x60003D9
        public static System.Boolean lua_checkstack(System.IntPtr L, System.Int32 extra) { }
        // RVA: 0x0B8AFF60  token: 0x60003DA
        public static System.Int32 lua_next(System.IntPtr L, System.Int32 index) { }
        // RVA: 0x05393274  token: 0x60003DB
        public static System.Void lua_pushlightuserdata(System.IntPtr L, System.IntPtr udata) { }
        // RVA: 0x053932A4  token: 0x60003DC
        public static System.IntPtr xlua_tag() { }
        // RVA: 0x0B8AFDEC  token: 0x60003DD
        public static System.Void luaL_where(System.IntPtr L, System.Int32 level) { }
        // RVA: 0x0539130C  token: 0x60003DE
        public static System.Int32 xlua_tryget_cachedud(System.IntPtr L, System.Int32 key, System.Int32 cache_ref) { }
        // RVA: 0x053912F0  token: 0x60003DF
        public static System.Void xlua_pushcsobj(System.IntPtr L, System.Int32 key, System.Int32 meta_ref, System.Boolean need_cache, System.Int32 cache_ref) { }
        // RVA: 0x05393284  token: 0x60003E0
        public static System.Int32 gen_obj_indexer(System.IntPtr L) { }
        // RVA: 0x0539328C  token: 0x60003E1
        public static System.Int32 gen_obj_newindexer(System.IntPtr L) { }
        // RVA: 0x05393294  token: 0x60003E2
        public static System.Int32 gen_cls_indexer(System.IntPtr L) { }
        // RVA: 0x0539329C  token: 0x60003E3
        public static System.Int32 gen_cls_newindexer(System.IntPtr L) { }
        // RVA: 0x053931FC  token: 0x60003E4
        public static System.Int32 get_error_func_ref(System.IntPtr L) { }
        // RVA: 0x05392E2C  token: 0x60003E5
        public static System.Int32 load_error_func(System.IntPtr L, System.Int32 Ref) { }
        // RVA: 0x05393204  token: 0x60003E6
        public static System.Int32 luaopen_i64lib(System.IntPtr L) { }
        // RVA: 0x0B8B00B4  token: 0x60003E7
        public static System.Int32 luaopen_socket_core(System.IntPtr L) { }
        // RVA: 0x032978D0  token: 0x60003E8
        public static System.Void lua_pushint64(System.IntPtr L, System.Int64 n) { }
        // RVA: 0x0539733C  token: 0x60003E9
        public static System.Void lua_pushuint64(System.IntPtr L, System.UInt64 n) { }
        // RVA: 0x0B8AFEF8  token: 0x60003EA
        public static System.Boolean lua_isint64(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0B8AFEF8  token: 0x60003EB
        public static System.Boolean lua_isuint64(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x05390990  token: 0x60003EC
        public static System.Int64 lua_toint64(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x05390990  token: 0x60003ED
        public static System.UInt64 lua_touint64(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x05391C8C  token: 0x60003EE
        public static System.Void xlua_push_csharp_function(System.IntPtr L, System.IntPtr fn, System.Int32 n) { }
        // RVA: 0x0B8B00C4  token: 0x60003EF
        public static System.Int32 xlua_csharp_str_error(System.IntPtr L, System.String message) { }
        // RVA: 0x0B8B00BC  token: 0x60003F0
        public static System.Int32 xlua_csharp_error(System.IntPtr L) { }
        // RVA: 0x0B8B02CC  token: 0x60003F1
        public static System.Boolean xlua_pack_int8_t(System.IntPtr buff, System.Int32 offset, System.Byte field) { }
        // RVA: 0x0B8B0450  token: 0x60003F2
        public static System.Boolean xlua_unpack_int8_t(System.IntPtr buff, System.Int32 offset, System.Byte& field) { }
        // RVA: 0x0B8B02A4  token: 0x60003F3
        public static System.Boolean xlua_pack_int16_t(System.IntPtr buff, System.Int32 offset, System.Int16 field) { }
        // RVA: 0x0B8B043C  token: 0x60003F4
        public static System.Boolean xlua_unpack_int16_t(System.IntPtr buff, System.Int32 offset, System.Int16& field) { }
        // RVA: 0x03B61690  token: 0x60003F5
        public static System.Boolean xlua_pack_int32_t(System.IntPtr buff, System.Int32 offset, System.Int32 field) { }
        // RVA: 0x03A1B210  token: 0x60003F6
        public static System.Boolean xlua_unpack_int32_t(System.IntPtr buff, System.Int32 offset, System.Int32& field) { }
        // RVA: 0x0B8B02B8  token: 0x60003F7
        public static System.Boolean xlua_pack_int64_t(System.IntPtr buff, System.Int32 offset, System.Int64 field) { }
        // RVA: 0x0B8B0360  token: 0x60003F8
        public static System.Boolean xlua_unpack_int64_t(System.IntPtr buff, System.Int32 offset, System.Int64& field) { }
        // RVA: 0x0B8B0290  token: 0x60003F9
        public static System.Boolean xlua_pack_float(System.IntPtr buff, System.Int32 offset, System.Single field) { }
        // RVA: 0x0B8B0428  token: 0x60003FA
        public static System.Boolean xlua_unpack_float(System.IntPtr buff, System.Int32 offset, System.Single& field) { }
        // RVA: 0x0B8B01F4  token: 0x60003FB
        public static System.Boolean xlua_pack_double(System.IntPtr buff, System.Int32 offset, System.Double field) { }
        // RVA: 0x0B8B0360  token: 0x60003FC
        public static System.Boolean xlua_unpack_double(System.IntPtr buff, System.Int32 offset, System.Double& field) { }
        // RVA: 0x05391C50  token: 0x60003FD
        public static System.IntPtr xlua_pushstruct(System.IntPtr L, System.UInt32 size, System.Int32 meta_ref) { }
        // RVA: 0x0B8B0330  token: 0x60003FE
        public static System.Void xlua_pushcstable(System.IntPtr L, System.UInt32 field_count, System.Int32 meta_ref) { }
        // RVA: 0x05391C84  token: 0x60003FF
        public static System.IntPtr lua_touserdata(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x05394850  token: 0x6000400
        public static System.Int32 xlua_gettypeid(System.IntPtr L, System.Int32 idx) { }
        // RVA: 0x0539320C  token: 0x6000401
        public static System.Int32 xlua_get_registry_index() { }
        // RVA: 0x044BDE60  token: 0x6000402
        public static System.Int32 xlua_pgettable_bypath(System.IntPtr L, System.Int32 idx, System.String path) { }
        // RVA: 0x0B8B02E0  token: 0x6000403
        public static System.Int32 xlua_psettable_bypath(System.IntPtr L, System.Int32 idx, System.String path) { }
        // RVA: 0x032975B0  token: 0x6000404
        public static System.Boolean xlua_pack_float2(System.IntPtr buff, System.Int32 offset, System.Single f1, System.Single f2) { }
        // RVA: 0x0B8B0374  token: 0x6000405
        public static System.Boolean xlua_unpack_float2(System.IntPtr buff, System.Int32 offset, System.Single& f1, System.Single& f2) { }
        // RVA: 0x03297650  token: 0x6000406
        public static System.Boolean xlua_pack_float3(System.IntPtr buff, System.Int32 offset, System.Single f1, System.Single f2, System.Single f3) { }
        // RVA: 0x049E78C0  token: 0x6000407
        public static System.Boolean xlua_unpack_float3(System.IntPtr buff, System.Int32 offset, System.Single& f1, System.Single& f2, System.Single& f3) { }
        // RVA: 0x03A1C160  token: 0x6000408
        public static System.Boolean xlua_pack_float4(System.IntPtr buff, System.Int32 offset, System.Single f1, System.Single f2, System.Single f3, System.Single f4) { }
        // RVA: 0x0B8B0388  token: 0x6000409
        public static System.Boolean xlua_unpack_float4(System.IntPtr buff, System.Int32 offset, System.Single& f1, System.Single& f2, System.Single& f3, System.Single& f4) { }
        // RVA: 0x0B8B0208  token: 0x600040A
        public static System.Boolean xlua_pack_float5(System.IntPtr buff, System.Int32 offset, System.Single f1, System.Single f2, System.Single f3, System.Single f4, System.Single f5) { }
        // RVA: 0x0B8B03B0  token: 0x600040B
        public static System.Boolean xlua_unpack_float5(System.IntPtr buff, System.Int32 offset, System.Single& f1, System.Single& f2, System.Single& f3, System.Single& f4, System.Single& f5) { }
        // RVA: 0x0B8B0244  token: 0x600040C
        public static System.Boolean xlua_pack_float6(System.IntPtr buff, System.Int32 offset, System.Single f1, System.Single f2, System.Single f3, System.Single f4, System.Single f5, System.Single f6) { }
        // RVA: 0x0B8B03E4  token: 0x600040D
        public static System.Boolean xlua_unpack_float6(System.IntPtr buff, System.Int32 offset, System.Single& f1, System.Single& f2, System.Single& f3, System.Single& f4, System.Single& f5, System.Single& f6) { }
        // RVA: 0x0B8B01E0  token: 0x600040E
        public static System.Boolean xlua_pack_decimal(System.IntPtr buff, System.Int32 offset, System.Decimal& dec) { }
        // RVA: 0x0B8B0338  token: 0x600040F
        public static System.Boolean xlua_unpack_decimal(System.IntPtr buff, System.Int32 offset, System.Byte& scale, System.Byte& sign, System.Int32& hi32, System.UInt64& lo64) { }
        // RVA: 0x0B8B0104  token: 0x6000410
        public static System.Boolean xlua_is_eq_str(System.IntPtr L, System.Int32 index, System.String str) { }
        // RVA: 0x0B8B0184  token: 0x6000411
        public static System.Boolean xlua_is_eq_str(System.IntPtr L, System.Int32 index, System.String str, System.Int32 str_len) { }
        // RVA: 0x053980C0  token: 0x6000412
        public static System.IntPtr xlua_gl(System.IntPtr L) { }
        // RVA: 0x032F19C0  token: 0x6000413
        public static System.Int32 luaL_traceback(System.IntPtr L, System.IntPtr L1, System.String msg, System.Int32 level) { }
        // RVA: 0x0B8AFF88  token: 0x6000414
        public static System.Int32 lua_sethook(System.IntPtr L, XLua.LuaDLL.lua_Hook hook, System.Int32 mask, System.Int32 count) { }
        // RVA: 0x0B8AFE50  token: 0x6000415
        public static XLua.LuaDLL.lua_Hook lua_gethook(System.IntPtr L) { }
        // RVA: 0x0B8AFE94  token: 0x6000416
        public static System.Int32 lua_gethookmask(System.IntPtr L) { }
        // RVA: 0x0B8AFE48  token: 0x6000417
        public static System.Int32 lua_getglobal(System.IntPtr L, System.IntPtr charPtr) { }
        // RVA: 0x053911C8  token: 0x6000418
        public static System.Int32 lua_pushlstring(System.IntPtr L, System.IntPtr charPtr, System.Int32 len) { }
        // RVA: 0x0539820C  token: 0x6000419
        public static System.Int32 luaopen_HyperLua(System.IntPtr L) { }
        // RVA: 0x03826E20  token: 0x600041A
        public static System.Void HyperLua_SetLogCallback(XLua.LuaDLL.Lua.HyperLuaLogCallbackDelegate logCallback) { }
        // RVA: 0x04D22950  token: 0x600041B
        public static System.Int32 LoadHyperLua(System.IntPtr L) { }
        // RVA: 0x04D5DAA0  token: 0x600041C
        public static System.Void SetHyperLuaLogCallback(XLua.LuaDLL.Lua.HyperLuaLogCallbackDelegate logCallback) { }
        // RVA: 0x053981FC  token: 0x600041D
        public static System.Int32 luaopen_LuaUtils(System.IntPtr L) { }
        // RVA: 0x03826E40  token: 0x600041E
        public static System.Void LuaUtils_SetLogCallback(XLua.LuaDLL.Lua.LuaUtilsLogCallbackDelegate logCallback) { }
        // RVA: 0x04D228B0  token: 0x600041F
        public static System.Int32 LoadLuaUtils(System.IntPtr L) { }
        // RVA: 0x04D5DA50  token: 0x6000420
        public static System.Void SetLuaUtilsLogCallback(XLua.LuaDLL.Lua.LuaUtilsLogCallbackDelegate logCallback) { }
        // RVA: 0x053981F4  token: 0x6000421
        public static System.Int32 luaopen_rapidjson(System.IntPtr L) { }
        // RVA: 0x04D22860  token: 0x6000422
        public static System.Int32 LoadRapidJson(System.IntPtr L) { }
        // RVA: 0x05398204  token: 0x6000423
        public static System.Int32 luaopen_pb(System.IntPtr L) { }
        // RVA: 0x04D22900  token: 0x6000424
        public static System.Int32 LoadLuaProtobuf(System.IntPtr L) { }
        // RVA: 0x053931F4  token: 0x6000425
        public static System.Void beyond_setDecryptionFunc(System.IntPtr L, System.IntPtr func) { }

    }

    // TypeToken: 0x20000EF  // size: 0x80
    public sealed class lua_Hook : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03B27170  token: 0x6000432
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x6000433
        public virtual System.Int32 Invoke(System.IntPtr L, XLua.lua_debug& ar) { }
        // RVA: 0x0B8B6BB8  token: 0x6000434
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr L, XLua.lua_debug& ar, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539ADF8  token: 0x6000435
        public virtual System.Int32 EndInvoke(XLua.lua_debug& ar, System.IAsyncResult result) { }

    }

}

namespace XLua.Reflection
{

    // TypeToken: 0x20000CB  // size: 0x10
    public class LuaObjFieldGetterSetter
    {
        // Methods
        // RVA: 0x0B8AF2C4  token: 0x600033B
        private static System.Boolean _CheckRuntimeType(System.IntPtr L, System.Object obj, System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x0B8AF564  token: 0x600033C
        private static System.IntPtr _GetPtrFromObj(System.Object obj) { }
        // RVA: 0x0B8AF494  token: 0x600033D
        private static System.IntPtr _GetPtrFromLuaStack(System.IntPtr L, XLua.ObjectTranslator translator, System.Int32 index, System.Boolean isUnmanagedType) { }
        // RVA: -1  // generic def  token: 0x600033E
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldGetterByPushInt32(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x04221230  token: 0x600033F
        public static XLua.LuaDLL.lua_CSFunction GetObjFloatFieldGetterByPushNumber(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x044BB460  token: 0x6000340
        public static XLua.LuaDLL.lua_CSFunction GetObjDoubleFieldGetterByPushNumber(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: -1  // generic def  token: 0x6000341
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldGetterByPushInt64(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x040E3940  token: 0x6000342
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldGetterByPushBool(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x040FBC40  token: 0x6000343
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldGetterByPushString(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x02F2BEE0  token: 0x6000344
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldObjGetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x03EE0030  token: 0x6000345
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldStructGetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x0429F080  token: 0x6000346
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldEnumGetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: -1  // generic def  token: 0x6000347
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldStructGetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x02F2AB70  token: 0x6000348
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldObjSetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x03EE01C0  token: 0x6000349
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldStructSetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: -1  // generic def  token: 0x600034A
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldSetterByGetInt32(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: -1  // generic def  token: 0x600034B
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldSetterByGetInt64(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x04221180  token: 0x600034C
        public static XLua.LuaDLL.lua_CSFunction GetObjFloatFieldSetterByGetNumber(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x044BB380  token: 0x600034D
        public static XLua.LuaDLL.lua_CSFunction GetObjDoubleFieldSetterByGetNumber(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x040E39F0  token: 0x600034E
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldBoolSetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x04250A50  token: 0x600034F
        public static XLua.LuaDLL.lua_CSFunction GetObjFieldEnumSetter(System.Type type, System.Reflection.FieldInfo field) { }
        // RVA: 0x041E1670  token: 0x6000350
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DE  // size: 0x10
    public static class RuntimeEnumDic
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.Int32,System.Object>> s_enumDic;  // static @ 0x0

        // Methods
        // RVA: 0x036FDA70  token: 0x6000375
        public static System.Object GetEnumByTypeAndValue(System.Type enumType, System.Int32 value) { }
        // RVA: 0x036FD4A0  token: 0x6000376
        public static System.Void AddEnumValue(System.Type enumType, System.Object enumObj) { }
        // RVA: 0x04D16EF0  token: 0x6000377
        private static System.Void .cctor() { }

    }

}

namespace XLua.TemplateEngine
{

    // TypeToken: 0x20000C5  // size: 0x14
    public sealed struct TokenType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static XLua.TemplateEngine.TokenType Code;  // const
        public static XLua.TemplateEngine.TokenType Eval;  // const
        public static XLua.TemplateEngine.TokenType Text;  // const

    }

    // TypeToken: 0x20000C6  // size: 0x20
    public class Chunk
    {
        // Fields
        private XLua.TemplateEngine.TokenType <Type>k__BackingField;  // 0x10
        private System.String <Text>k__BackingField;  // 0x18

        // Properties
        XLua.TemplateEngine.TokenType Type { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.String Text { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x05391D70  token: 0x6000322
        public System.Void .ctor(XLua.TemplateEngine.TokenType type, System.String text) { }

    }

    // TypeToken: 0x20000C7  // size: 0x90
    public class TemplateFormatException : System.Exception
    {
        // Methods
        // RVA: 0x0B8B11A8  token: 0x6000323
        public System.Void .ctor(System.String message) { }

    }

    // TypeToken: 0x20000C8  // size: 0x10
    public class Parser
    {
        // Fields
        private static System.String <RegexString>k__BackingField;  // static @ 0x0

        // Properties
        System.String RegexString { get; /* RVA: 0x0B8B10BC */ set; /* RVA: 0x0B8B110C */ }

        // Methods
        // RVA: 0x0B8B104C  token: 0x6000326
        private static System.Void .cctor() { }
        // RVA: 0x0B8B0464  token: 0x6000327
        private static System.String EscapeString(System.String input) { }
        // RVA: 0x0B8B06FC  token: 0x6000328
        private static System.String GetRegexString() { }
        // RVA: 0x0B8B0934  token: 0x6000329
        public static System.Collections.Generic.List<XLua.TemplateEngine.Chunk> Parse(System.String snippet) { }
        // RVA: 0x041E1670  token: 0x600032A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CA  // size: 0x10
    public class LuaTemplate
    {
        // Fields
        private static XLua.LuaDLL.lua_CSFunction templateCompileFunction;  // static @ 0x0
        private static XLua.LuaDLL.lua_CSFunction templateExecuteFunction;  // static @ 0x8

        // Methods
        // RVA: 0x0B8AF818  token: 0x6000332
        public static System.String ComposeCode(System.Collections.Generic.List<XLua.TemplateEngine.Chunk> chunks) { }
        // RVA: 0x0B8AF5B8  token: 0x6000333
        public static XLua.LuaFunction Compile(XLua.LuaEnv luaenv, System.String snippet) { }
        // RVA: 0x0B8AFB74  token: 0x6000334
        public static System.String Execute(XLua.LuaFunction compiledTemplate, XLua.LuaTable parameters) { }
        // RVA: 0x0B8AFC04  token: 0x6000335
        public static System.String Execute(XLua.LuaFunction compiledTemplate) { }
        // RVA: 0x0B8AF65C  token: 0x6000336
        public static System.Int32 Compile(System.IntPtr L) { }
        // RVA: 0x0B8AFAA0  token: 0x6000337
        public static System.Int32 Execute(System.IntPtr L) { }
        // RVA: 0x03600B80  token: 0x6000338
        public static System.Void OpenLib(System.IntPtr L) { }
        // RVA: 0x041E1670  token: 0x6000339
        public System.Void .ctor() { }
        // RVA: 0x04CA1250  token: 0x600033A
        private static System.Void .cctor() { }

    }

}

