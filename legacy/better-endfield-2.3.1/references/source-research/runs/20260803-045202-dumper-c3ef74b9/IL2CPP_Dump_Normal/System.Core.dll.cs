// ========================================================
// Dumped by @desirepro
// Assembly: System.Core.dll
// Classes:  673
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x10
    public static class SR
    {
        // Methods
        // RVA: 0x0464E630  token: 0x6000001
        private static System.String GetString(System.String name) { }
        // RVA: 0x0ACCAD50  token: 0x6000002
        private static System.String Format(System.String resourceFormat, System.Object[] args) { }
        // RVA: 0x0ACCAC74  token: 0x6000003
        private static System.String Format(System.String resourceFormat, System.Object p1) { }
        // RVA: 0x0ACCAC04  token: 0x6000004
        private static System.String Format(System.String resourceFormat, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACCACD0  token: 0x6000005
        private static System.String Format(System.String resourceFormat, System.Object p1, System.Object p2, System.Object p3) { }

    }

    // TypeToken: 0x200001D
    public abstract class Iterator`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 threadId;  // 0x0
        private System.Int32 state;  // 0x0
        private TSource current;  // 0x0

        // Properties
        TSource Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000AE
        public System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x60000B0
        public virtual System.Linq.Enumerable.Iterator<TSource> Clone() { }
        // RVA: -1  // not resolved  token: 0x60000B1
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000B2
        public virtual System.Collections.Generic.IEnumerator<TSource> GetEnumerator() { }
        // RVA: -1  // abstract  token: 0x60000B3
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // abstract  token: 0x60000B4
        public virtual System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector) { }
        // RVA: -1  // abstract  token: 0x60000B5
        public virtual System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // not resolved  token: 0x60000B7
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60000B8
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200001E
    public class WhereEnumerableIterator`1 : Iterator`1
    {
        // Fields
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        private System.Func<TSource,System.Boolean> predicate;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> enumerator;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000B9
        public System.Void .ctor(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // not resolved  token: 0x60000BA
        public virtual System.Linq.Enumerable.Iterator<TSource> Clone() { }
        // RVA: -1  // not resolved  token: 0x60000BB
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000BC
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // generic def  token: 0x60000BD
        public virtual System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector) { }
        // RVA: -1  // not resolved  token: 0x60000BE
        public virtual System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate) { }

    }

    // TypeToken: 0x200001F
    public class WhereArrayIterator`1 : Iterator`1
    {
        // Fields
        private TSource[] source;  // 0x0
        private System.Func<TSource,System.Boolean> predicate;  // 0x0
        private System.Int32 index;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000BF
        public System.Void .ctor(TSource[] source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // not resolved  token: 0x60000C0
        public virtual System.Linq.Enumerable.Iterator<TSource> Clone() { }
        // RVA: -1  // not resolved  token: 0x60000C1
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // generic def  token: 0x60000C2
        public virtual System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector) { }
        // RVA: -1  // not resolved  token: 0x60000C3
        public virtual System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate) { }

    }

    // TypeToken: 0x2000020
    public class WhereListIterator`1 : Iterator`1
    {
        // Fields
        private System.Collections.Generic.List<TSource> source;  // 0x0
        private System.Func<TSource,System.Boolean> predicate;  // 0x0
        private System.Collections.Generic.List.Enumerator<TSource> enumerator;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000C4
        public System.Void .ctor(System.Collections.Generic.List<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // not resolved  token: 0x60000C5
        public virtual System.Linq.Enumerable.Iterator<TSource> Clone() { }
        // RVA: -1  // not resolved  token: 0x60000C6
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // generic def  token: 0x60000C7
        public virtual System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector) { }
        // RVA: -1  // not resolved  token: 0x60000C8
        public virtual System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate) { }

    }

    // TypeToken: 0x2000021
    public class WhereSelectEnumerableIterator`2 : Iterator`1
    {
        // Fields
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        private System.Func<TSource,System.Boolean> predicate;  // 0x0
        private System.Func<TSource,TResult> selector;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> enumerator;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000C9
        public System.Void .ctor(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate, System.Func<TSource,TResult> selector) { }
        // RVA: -1  // not resolved  token: 0x60000CA
        public virtual System.Linq.Enumerable.Iterator<TResult> Clone() { }
        // RVA: -1  // not resolved  token: 0x60000CB
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000CC
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // generic def  token: 0x60000CD
        public virtual System.Collections.Generic.IEnumerable<TResult2> Select(System.Func<TResult,TResult2> selector) { }
        // RVA: -1  // not resolved  token: 0x60000CE
        public virtual System.Collections.Generic.IEnumerable<TResult> Where(System.Func<TResult,System.Boolean> predicate) { }

    }

    // TypeToken: 0x2000022
    public class WhereSelectArrayIterator`2 : Iterator`1
    {
        // Fields
        private TSource[] source;  // 0x0
        private System.Func<TSource,System.Boolean> predicate;  // 0x0
        private System.Func<TSource,TResult> selector;  // 0x0
        private System.Int32 index;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000CF
        public System.Void .ctor(TSource[] source, System.Func<TSource,System.Boolean> predicate, System.Func<TSource,TResult> selector) { }
        // RVA: -1  // not resolved  token: 0x60000D0
        public virtual System.Linq.Enumerable.Iterator<TResult> Clone() { }
        // RVA: -1  // not resolved  token: 0x60000D1
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // generic def  token: 0x60000D2
        public virtual System.Collections.Generic.IEnumerable<TResult2> Select(System.Func<TResult,TResult2> selector) { }
        // RVA: -1  // not resolved  token: 0x60000D3
        public virtual System.Collections.Generic.IEnumerable<TResult> Where(System.Func<TResult,System.Boolean> predicate) { }

    }

    // TypeToken: 0x2000023
    public class WhereSelectListIterator`2 : Iterator`1
    {
        // Fields
        private System.Collections.Generic.List<TSource> source;  // 0x0
        private System.Func<TSource,System.Boolean> predicate;  // 0x0
        private System.Func<TSource,TResult> selector;  // 0x0
        private System.Collections.Generic.List.Enumerator<TSource> enumerator;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000D4
        public System.Void .ctor(System.Collections.Generic.List<TSource> source, System.Func<TSource,System.Boolean> predicate, System.Func<TSource,TResult> selector) { }
        // RVA: -1  // not resolved  token: 0x60000D5
        public virtual System.Linq.Enumerable.Iterator<TResult> Clone() { }
        // RVA: -1  // not resolved  token: 0x60000D6
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // generic def  token: 0x60000D7
        public virtual System.Collections.Generic.IEnumerable<TResult2> Select(System.Func<TResult,TResult2> selector) { }
        // RVA: -1  // not resolved  token: 0x60000D8
        public virtual System.Collections.Generic.IEnumerable<TResult> Where(System.Func<TResult,System.Boolean> predicate) { }

    }

    // TypeToken: 0x2000024
    public sealed class <>c__DisplayClass6_0`1
    {
        // Fields
        public System.Func<TSource,System.Boolean> predicate1;  // 0x0
        public System.Func<TSource,System.Boolean> predicate2;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000D9
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60000DA
        private System.Boolean <CombinePredicates>b__0(TSource x) { }

    }

    // TypeToken: 0x2000025
    public sealed class <>c__DisplayClass7_0`3
    {
        // Fields
        public System.Func<TMiddle,TResult> selector2;  // 0x0
        public System.Func<TSource,TMiddle> selector1;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000DB
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60000DC
        private TResult <CombineSelectors>b__0(TSource x) { }

    }

    // TypeToken: 0x2000026
    public sealed class <SelectManyIterator>d__17`2 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TResult <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__source;  // 0x0
        private System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> selector;  // 0x0
        public System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> <>3__selector;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <>7__wrap1;  // 0x0
        private System.Collections.Generic.IEnumerator<TResult> <>7__wrap2;  // 0x0

        // Properties
        TResult System.Collections.Generic.IEnumerator<TResult>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000DD
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60000DE
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000DF
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60000E0
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x60000E1
        private System.Void <>m__Finally2() { }
        // RVA: -1  // not resolved  token: 0x60000E3
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x60000E5
        private virtual System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60000E6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000027
    public sealed class <SelectManyIterator>d__23`3 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TResult <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__source;  // 0x0
        private System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>> collectionSelector;  // 0x0
        public System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>> <>3__collectionSelector;  // 0x0
        private System.Func<TSource,TCollection,TResult> resultSelector;  // 0x0
        public System.Func<TSource,TCollection,TResult> <>3__resultSelector;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <>7__wrap1;  // 0x0
        private TSource <element>5__3;  // 0x0
        private System.Collections.Generic.IEnumerator<TCollection> <>7__wrap3;  // 0x0

        // Properties
        TResult System.Collections.Generic.IEnumerator<TResult>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000E7
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60000E8
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000E9
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60000EA
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x60000EB
        private System.Void <>m__Finally2() { }
        // RVA: -1  // not resolved  token: 0x60000ED
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x60000EF
        private virtual System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60000F0
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000028
    public sealed class <SkipIterator>d__31`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TSource <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__source;  // 0x0
        private System.Int32 count;  // 0x0
        public System.Int32 <>3__count;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <e>5__2;  // 0x0

        // Properties
        TSource System.Collections.Generic.IEnumerator<TSource>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000F1
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60000F2
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000F3
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60000F4
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x60000F6
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x60000F8
        private virtual System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60000F9
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000029
    public sealed class <JoinIterator>d__38`4 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TResult <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEnumerable<TInner> inner;  // 0x0
        public System.Collections.Generic.IEnumerable<TInner> <>3__inner;  // 0x0
        private System.Func<TInner,TKey> innerKeySelector;  // 0x0
        public System.Func<TInner,TKey> <>3__innerKeySelector;  // 0x0
        private System.Collections.Generic.IEqualityComparer<TKey> comparer;  // 0x0
        public System.Collections.Generic.IEqualityComparer<TKey> <>3__comparer;  // 0x0
        private System.Collections.Generic.IEnumerable<TOuter> outer;  // 0x0
        public System.Collections.Generic.IEnumerable<TOuter> <>3__outer;  // 0x0
        private System.Func<TOuter,TKey> outerKeySelector;  // 0x0
        public System.Func<TOuter,TKey> <>3__outerKeySelector;  // 0x0
        private System.Func<TOuter,TInner,TResult> resultSelector;  // 0x0
        public System.Func<TOuter,TInner,TResult> <>3__resultSelector;  // 0x0
        private System.Linq.Lookup<TKey,TInner> <lookup>5__2;  // 0x0
        private System.Collections.Generic.IEnumerator<TOuter> <>7__wrap2;  // 0x0
        private TOuter <item>5__4;  // 0x0
        private System.Linq.Lookup.Grouping<TKey,TInner> <g>5__5;  // 0x0
        private System.Int32 <i>5__6;  // 0x0

        // Properties
        TResult System.Collections.Generic.IEnumerator<TResult>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000FA
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60000FB
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000FC
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60000FD
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x60000FF
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x6000101
        private virtual System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000102
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002A
    public sealed class <ConcatIterator>d__59`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TSource <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> first;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__first;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> second;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__second;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <>7__wrap1;  // 0x0

        // Properties
        TSource System.Collections.Generic.IEnumerator<TSource>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000103
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000104
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000105
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000106
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x6000107
        private System.Void <>m__Finally2() { }
        // RVA: -1  // not resolved  token: 0x6000109
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x600010B
        private virtual System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600010C
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002B
    public sealed class <PrependIterator>d__63`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TSource <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private TSource element;  // 0x0
        public TSource <>3__element;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__source;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <>7__wrap1;  // 0x0

        // Properties
        TSource System.Collections.Generic.IEnumerator<TSource>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600010D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x600010E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x600010F
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000110
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x6000112
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x6000114
        private virtual System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000115
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002C
    public sealed class <DistinctIterator>d__68`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TSource <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEqualityComparer<TSource> comparer;  // 0x0
        public System.Collections.Generic.IEqualityComparer<TSource> <>3__comparer;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__source;  // 0x0
        private System.Linq.Set<TSource> <set>5__2;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <>7__wrap2;  // 0x0

        // Properties
        TSource System.Collections.Generic.IEnumerator<TSource>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000116
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000117
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000118
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000119
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x600011B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x600011D
        private virtual System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600011E
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002D
    public sealed class <UnionIterator>d__71`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TSource <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEqualityComparer<TSource> comparer;  // 0x0
        public System.Collections.Generic.IEqualityComparer<TSource> <>3__comparer;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> first;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__first;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> second;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__second;  // 0x0
        private System.Linq.Set<TSource> <set>5__2;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <>7__wrap2;  // 0x0

        // Properties
        TSource System.Collections.Generic.IEnumerator<TSource>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600011F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000120
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000121
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000122
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x6000123
        private System.Void <>m__Finally2() { }
        // RVA: -1  // not resolved  token: 0x6000125
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x6000127
        private virtual System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000128
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002E
    public sealed class <IntersectIterator>d__74`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TSource <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEqualityComparer<TSource> comparer;  // 0x0
        public System.Collections.Generic.IEqualityComparer<TSource> <>3__comparer;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> second;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__second;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> first;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__first;  // 0x0
        private System.Linq.Set<TSource> <set>5__2;  // 0x0
        private System.Collections.Generic.IEnumerator<TSource> <>7__wrap2;  // 0x0

        // Properties
        TSource System.Collections.Generic.IEnumerator<TSource>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000129
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x600012A
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x600012B
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600012C
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x600012E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x6000130
        private virtual System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000131
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002F
    public sealed class <ReverseIterator>d__79`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TSource <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        public System.Collections.Generic.IEnumerable<TSource> <>3__source;  // 0x0
        private System.Linq.Buffer<TSource> <buffer>5__2;  // 0x0
        private System.Int32 <i>5__3;  // 0x0

        // Properties
        TSource System.Collections.Generic.IEnumerator<TSource>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000132
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000133
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000134
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000136
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x6000138
        private virtual System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000139
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000030
    public sealed class <OfTypeIterator>d__97`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TResult <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.IEnumerable source;  // 0x0
        public System.Collections.IEnumerable <>3__source;  // 0x0
        private System.Collections.IEnumerator <>7__wrap1;  // 0x0

        // Properties
        TResult System.Collections.Generic.IEnumerator<TResult>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600013A
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x600013B
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x600013C
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600013D
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x600013F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x6000141
        private virtual System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000142
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000031
    public sealed class <CastIterator>d__99`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TResult <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.IEnumerable source;  // 0x0
        public System.Collections.IEnumerable <>3__source;  // 0x0
        private System.Collections.IEnumerator <>7__wrap1;  // 0x0

        // Properties
        TResult System.Collections.Generic.IEnumerator<TResult>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000143
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000144
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000145
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000146
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x6000148
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x600014A
        private virtual System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600014B
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000032  // size: 0x30
    public sealed class <RangeIterator>d__115 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Int32 <>2__current;  // 0x14
        private System.Int32 <>l__initialThreadId;  // 0x18
        private System.Int32 start;  // 0x1c
        public System.Int32 <>3__start;  // 0x20
        private System.Int32 count;  // 0x24
        public System.Int32 <>3__count;  // 0x28
        private System.Int32 <i>5__2;  // 0x2c

        // Properties
        System.Int32 System.Collections.Generic.IEnumerator<System.Int32>.Current { get; /* RVA: 0x04D882A0 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0ACCAE60 */ }

        // Methods
        // RVA: 0x02EDACA0  token: 0x600014C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600014D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0460CAA0  token: 0x600014E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0ACCAE14  token: 0x6000150
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x04CA1C00  token: 0x6000152
        private virtual System.Collections.Generic.IEnumerator<System.Int32> System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator() { }
        // RVA: 0x0ACCAE0C  token: 0x6000153
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000033
    public sealed class <RepeatIterator>d__117`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TResult <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private TResult element;  // 0x0
        public TResult <>3__element;  // 0x0
        private System.Int32 count;  // 0x0
        public System.Int32 <>3__count;  // 0x0
        private System.Int32 <i>5__2;  // 0x0

        // Properties
        TResult System.Collections.Generic.IEnumerator<TResult>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000154
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000155
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000156
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000158
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x600015A
        private virtual System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600015B
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000036
    public sealed class <>c
    {
        // Fields
        public static readonly System.Linq.IdentityFunction.<>c<TElement> <>9;  // static @ 0x0
        public static System.Func<TElement,TElement> <>9__1_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600015E
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600015F
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000160
        private TElement <get_Instance>b__1_0(TElement x) { }

    }

    // TypeToken: 0x200003C
    public sealed class <GetEnumerator>d__7 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TElement <>2__current;  // 0x0
        public System.Linq.Lookup.Grouping<TKey,TElement> <>4__this;  // 0x0
        private System.Int32 <i>5__2;  // 0x0

        // Properties
        TElement System.Collections.Generic.IEnumerator<TElement>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600017C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x600017D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x600017E
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000180
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200003B
    public class Grouping : System.Linq.IGrouping`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1
    {
        // Fields
        private TKey key;  // 0x0
        private System.Int32 hashCode;  // 0x0
        private TElement[] elements;  // 0x0
        private System.Int32 count;  // 0x0
        private System.Linq.Lookup.Grouping<TKey,TElement> hashNext;  // 0x0
        private System.Linq.Lookup.Grouping<TKey,TElement> next;  // 0x0

        // Properties
        TKey Key { get; /* RVA: -1  // not resolved */ }
        System.Int32 System.Collections.Generic.ICollection<TElement>.Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.Generic.ICollection<TElement>.IsReadOnly { get; /* RVA: -1  // not resolved */ }
        TElement System.Collections.Generic.IList<TElement>.Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600016B
        private System.Void Add(TElement element) { }
        // RVA: -1  // not resolved  token: 0x600016C
        public virtual System.Collections.Generic.IEnumerator<TElement> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600016D
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000171
        private virtual System.Void System.Collections.Generic.ICollection<TElement>.Add(TElement item) { }
        // RVA: -1  // not resolved  token: 0x6000172
        private virtual System.Void System.Collections.Generic.ICollection<TElement>.Clear() { }
        // RVA: -1  // not resolved  token: 0x6000173
        private virtual System.Boolean System.Collections.Generic.ICollection<TElement>.Contains(TElement item) { }
        // RVA: -1  // not resolved  token: 0x6000174
        private virtual System.Void System.Collections.Generic.ICollection<TElement>.CopyTo(TElement[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000175
        private virtual System.Boolean System.Collections.Generic.ICollection<TElement>.Remove(TElement item) { }
        // RVA: -1  // not resolved  token: 0x6000176
        private virtual System.Int32 System.Collections.Generic.IList<TElement>.IndexOf(TElement item) { }
        // RVA: -1  // not resolved  token: 0x6000177
        private virtual System.Void System.Collections.Generic.IList<TElement>.Insert(System.Int32 index, TElement item) { }
        // RVA: -1  // not resolved  token: 0x6000178
        private virtual System.Void System.Collections.Generic.IList<TElement>.RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600017B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003D
    public sealed class <GetEnumerator>d__12 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Linq.IGrouping<TKey,TElement> <>2__current;  // 0x0
        public System.Linq.Lookup<TKey,TElement> <>4__this;  // 0x0
        private System.Linq.Lookup.Grouping<TKey,TElement> <g>5__2;  // 0x0

        // Properties
        System.Linq.IGrouping<TKey,TElement> System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000182
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000183
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000184
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000186
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200003F
    public sealed struct Slot
    {
        // Fields
        private System.Int32 hashCode;  // 0x0
        private TElement value;  // 0x0
        private System.Int32 next;  // 0x0

    }

    // TypeToken: 0x2000042
    public sealed class <GetEnumerator>d__1 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TElement <>2__current;  // 0x0
        public System.Linq.OrderedEnumerable<TElement> <>4__this;  // 0x0
        private System.Linq.Buffer<TElement> <buffer>5__2;  // 0x0
        private System.Int32[] <map>5__3;  // 0x0
        private System.Int32 <i>5__4;  // 0x0

        // Properties
        TElement System.Collections.Generic.IEnumerator<TElement>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000196
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000197
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000198
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600019A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000050  // size: 0x10
    public class BinaryExpressionProxy
    {
    }

    // TypeToken: 0x2000051  // size: 0x10
    public class BlockExpressionProxy
    {
    }

    // TypeToken: 0x2000052  // size: 0x10
    public class CatchBlockProxy
    {
    }

    // TypeToken: 0x2000053  // size: 0x10
    public class ConditionalExpressionProxy
    {
    }

    // TypeToken: 0x2000054  // size: 0x10
    public class ConstantExpressionProxy
    {
    }

    // TypeToken: 0x2000055  // size: 0x10
    public class DebugInfoExpressionProxy
    {
    }

    // TypeToken: 0x2000056  // size: 0x10
    public class DefaultExpressionProxy
    {
    }

    // TypeToken: 0x2000057  // size: 0x10
    public class GotoExpressionProxy
    {
    }

    // TypeToken: 0x2000058  // size: 0x10
    public class IndexExpressionProxy
    {
    }

    // TypeToken: 0x2000059  // size: 0x10
    public class InvocationExpressionProxy
    {
    }

    // TypeToken: 0x200005A  // size: 0x10
    public class LabelExpressionProxy
    {
    }

    // TypeToken: 0x200005B  // size: 0x10
    public class LambdaExpressionProxy
    {
    }

    // TypeToken: 0x200005C  // size: 0x10
    public class ListInitExpressionProxy
    {
    }

    // TypeToken: 0x200005D  // size: 0x10
    public class LoopExpressionProxy
    {
    }

    // TypeToken: 0x200005E  // size: 0x10
    public class MemberExpressionProxy
    {
    }

    // TypeToken: 0x200005F  // size: 0x10
    public class MemberInitExpressionProxy
    {
    }

    // TypeToken: 0x2000060  // size: 0x10
    public class MethodCallExpressionProxy
    {
    }

    // TypeToken: 0x2000061  // size: 0x10
    public class NewArrayExpressionProxy
    {
    }

    // TypeToken: 0x2000062  // size: 0x10
    public class NewExpressionProxy
    {
    }

    // TypeToken: 0x2000063  // size: 0x10
    public class ParameterExpressionProxy
    {
    }

    // TypeToken: 0x2000064  // size: 0x10
    public class RuntimeVariablesExpressionProxy
    {
    }

    // TypeToken: 0x2000065  // size: 0x10
    public class SwitchCaseProxy
    {
    }

    // TypeToken: 0x2000066  // size: 0x10
    public class SwitchExpressionProxy
    {
    }

    // TypeToken: 0x2000067  // size: 0x10
    public class TryExpressionProxy
    {
    }

    // TypeToken: 0x2000068  // size: 0x10
    public class TypeBinaryExpressionProxy
    {
    }

    // TypeToken: 0x2000069  // size: 0x10
    public class UnaryExpressionProxy
    {
    }

    // TypeToken: 0x200006A  // size: 0x20
    public class ExtensionInfo
    {
        // Fields
        private readonly System.Linq.Expressions.ExpressionType NodeType;  // 0x10
        private readonly System.Type Type;  // 0x18

    }

    // TypeToken: 0x2000076  // size: 0x30
    public sealed class <GetEnumerator>d__18 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Linq.Expressions.Expression <>2__current;  // 0x18
        public System.Linq.Expressions.BlockExpressionList <>4__this;  // 0x20
        private System.Int32 <i>5__2;  // 0x28

        // Properties
        System.Linq.Expressions.Expression System.Collections.Generic.IEnumerator<System.Linq.Expressions.Expression>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002FC
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002FD
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0ACE2610  token: 0x60002FE
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0ACE26D4  token: 0x6000300
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000C2
    public sealed class <>c__3`2
    {
        // Fields
        public static readonly System.Linq.Expressions.StackGuard.<>c__3<T1,T2> <>9;  // static @ 0x0
        public static System.Func<System.Object,System.Object> <>9__3_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60004E4
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60004E5
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60004E6
        private System.Object <RunOnEmptyStack>b__3_0(System.Object s) { }

    }

    // TypeToken: 0x20000CC  // size: 0x10
    public sealed class AddInt16 : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCB400  token: 0x6000590
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000591
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CD  // size: 0x10
    public sealed class AddInt32 : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCB514  token: 0x6000592
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000593
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CE  // size: 0x10
    public sealed class AddInt64 : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCB618  token: 0x6000594
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000595
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CF  // size: 0x10
    public sealed class AddUInt16 : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCC518  token: 0x6000596
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000597
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D0  // size: 0x10
    public sealed class AddUInt32 : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCC62C  token: 0x6000598
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000599
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D1  // size: 0x10
    public sealed class AddUInt64 : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCC73C  token: 0x600059A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600059B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D2  // size: 0x10
    public sealed class AddSingle : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCC3F8  token: 0x600059C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600059D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D3  // size: 0x10
    public sealed class AddDouble : System.Linq.Expressions.Interpreter.AddInstruction
    {
        // Methods
        // RVA: 0x0ACCAEA4  token: 0x600059E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600059F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x10
    public sealed class AddOvfInt16 : System.Linq.Expressions.Interpreter.AddOvfInstruction
    {
        // Methods
        // RVA: 0x0ACCBA74  token: 0x60005A5
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005A6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D6  // size: 0x10
    public sealed class AddOvfInt32 : System.Linq.Expressions.Interpreter.AddOvfInstruction
    {
        // Methods
        // RVA: 0x0ACCBC68  token: 0x60005A7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005A8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D7  // size: 0x10
    public sealed class AddOvfInt64 : System.Linq.Expressions.Interpreter.AddOvfInstruction
    {
        // Methods
        // RVA: 0x0ACCBE00  token: 0x60005A9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005AA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D8  // size: 0x10
    public sealed class AddOvfUInt16 : System.Linq.Expressions.Interpreter.AddOvfInstruction
    {
        // Methods
        // RVA: 0x0ACCBF70  token: 0x60005AB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005AC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D9  // size: 0x10
    public sealed class AddOvfUInt32 : System.Linq.Expressions.Interpreter.AddOvfInstruction
    {
        // Methods
        // RVA: 0x0ACCC11C  token: 0x60005AD
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005AE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DA  // size: 0x10
    public sealed class AddOvfUInt64 : System.Linq.Expressions.Interpreter.AddOvfInstruction
    {
        // Methods
        // RVA: 0x0ACCC28C  token: 0x60005AF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005B0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DC  // size: 0x10
    public sealed class AndSByte : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCD0D0  token: 0x60005B6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005B7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DD  // size: 0x10
    public sealed class AndInt16 : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCCEB0  token: 0x60005B8
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005B9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DE  // size: 0x10
    public sealed class AndInt32 : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCCF60  token: 0x60005BA
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005BB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DF  // size: 0x10
    public sealed class AndInt64 : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCD010  token: 0x60005BC
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005BD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E0  // size: 0x10
    public sealed class AndByte : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCC944  token: 0x60005BE
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E1  // size: 0x10
    public sealed class AndUInt16 : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCD180  token: 0x60005C0
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005C1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E2  // size: 0x10
    public sealed class AndUInt32 : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCD230  token: 0x60005C2
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005C3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E3  // size: 0x10
    public sealed class AndUInt64 : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCD2EC  token: 0x60005C4
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005C5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E4  // size: 0x10
    public sealed class AndBoolean : System.Linq.Expressions.Interpreter.AndInstruction
    {
        // Methods
        // RVA: 0x0ACCC850  token: 0x60005C6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60005C7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000106  // size: 0x10
    public sealed class DecrementInt16 : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD0688  token: 0x6000682
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000683
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000107  // size: 0x10
    public sealed class DecrementInt32 : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD070C  token: 0x6000684
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000685
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000108  // size: 0x10
    public sealed class DecrementInt64 : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD0784  token: 0x6000686
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000687
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000109  // size: 0x10
    public sealed class DecrementUInt16 : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD08AC  token: 0x6000688
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000689
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010A  // size: 0x10
    public sealed class DecrementUInt32 : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD0930  token: 0x600068A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600068B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010B  // size: 0x10
    public sealed class DecrementUInt64 : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD09C0  token: 0x600068C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600068D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010C  // size: 0x10
    public sealed class DecrementSingle : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD0814  token: 0x600068E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600068F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010D  // size: 0x10
    public sealed class DecrementDouble : System.Linq.Expressions.Interpreter.DecrementInstruction
    {
        // Methods
        // RVA: 0x0ACD01B4  token: 0x6000690
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000691
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000110  // size: 0x10
    public sealed class DivInt16 : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD1108  token: 0x600069C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600069D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000111  // size: 0x10
    public sealed class DivInt32 : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD121C  token: 0x600069E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600069F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000112  // size: 0x10
    public sealed class DivInt64 : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD1324  token: 0x60006A0
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000113  // size: 0x10
    public sealed class DivUInt16 : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD1554  token: 0x60006A2
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006A3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000114  // size: 0x10
    public sealed class DivUInt32 : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD1668  token: 0x60006A4
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000115  // size: 0x10
    public sealed class DivUInt64 : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD177C  token: 0x60006A6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006A7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000116  // size: 0x10
    public sealed class DivSingle : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD143C  token: 0x60006A8
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006A9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000117  // size: 0x10
    public sealed class DivDouble : System.Linq.Expressions.Interpreter.DivInstruction
    {
        // Methods
        // RVA: 0x0ACD0BB4  token: 0x60006AA
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006AB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000119  // size: 0x10
    public sealed class EqualBoolean : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD2910  token: 0x60006B1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006B2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011A  // size: 0x10
    public sealed class EqualSByte : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3F1C  token: 0x60006B3
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011B  // size: 0x10
    public sealed class EqualInt16 : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3AAC  token: 0x60006B5
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011C  // size: 0x10
    public sealed class EqualChar : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD2BD0  token: 0x60006B7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006B8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011D  // size: 0x10
    public sealed class EqualInt32 : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3C0C  token: 0x60006B9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006BA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011E  // size: 0x10
    public sealed class EqualInt64 : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3D6C  token: 0x60006BB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011F  // size: 0x10
    public sealed class EqualByte : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD2A70  token: 0x60006BD
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006BE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000120  // size: 0x10
    public sealed class EqualUInt16 : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD4208  token: 0x60006BF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000121  // size: 0x10
    public sealed class EqualUInt32 : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD4368  token: 0x60006C1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006C2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000122  // size: 0x10
    public sealed class EqualUInt64 : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD44C8  token: 0x60006C3
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006C4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000123  // size: 0x10
    public sealed class EqualSingle : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD4090  token: 0x60006C5
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006C6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000124  // size: 0x10
    public sealed class EqualDouble : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD2D44  token: 0x60006C7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006C8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000125  // size: 0x10
    public sealed class EqualReference : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3E18  token: 0x60006C9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006CA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000126  // size: 0x10
    public sealed class EqualBooleanLiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD285C  token: 0x60006CB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006CC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000127  // size: 0x10
    public sealed class EqualSByteLiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3E68  token: 0x60006CD
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006CE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000128  // size: 0x10
    public sealed class EqualInt16LiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD39F8  token: 0x60006CF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006D0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000129  // size: 0x10
    public sealed class EqualCharLiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD2B1C  token: 0x60006D1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006D2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012A  // size: 0x10
    public sealed class EqualInt32LiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3B58  token: 0x60006D3
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012B  // size: 0x10
    public sealed class EqualInt64LiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3CB8  token: 0x60006D5
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012C  // size: 0x10
    public sealed class EqualByteLiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD29BC  token: 0x60006D7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006D8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012D  // size: 0x10
    public sealed class EqualUInt16LiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD4154  token: 0x60006D9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006DA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012E  // size: 0x10
    public sealed class EqualUInt32LiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD42B4  token: 0x60006DB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006DC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012F  // size: 0x10
    public sealed class EqualUInt64LiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD4414  token: 0x60006DD
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006DE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000130  // size: 0x10
    public sealed class EqualSingleLiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD3FC8  token: 0x60006DF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000131  // size: 0x10
    public sealed class EqualDoubleLiftedToNull : System.Linq.Expressions.Interpreter.EqualInstruction
    {
        // Methods
        // RVA: 0x0ACD2C7C  token: 0x60006E1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006E2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000133  // size: 0x10
    public sealed class ExclusiveOrSByte : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACD879C  token: 0x60006E8
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006E9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000134  // size: 0x10
    public sealed class ExclusiveOrInt16 : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACD857C  token: 0x60006EA
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006EB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000135  // size: 0x10
    public sealed class ExclusiveOrInt32 : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACD862C  token: 0x60006EC
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006ED
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000136  // size: 0x10
    public sealed class ExclusiveOrInt64 : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACD86DC  token: 0x60006EE
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006EF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000137  // size: 0x10
    public sealed class ExclusiveOrByte : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACE4D80  token: 0x60006F0
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006F1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000138  // size: 0x10
    public sealed class ExclusiveOrUInt16 : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACE4E30  token: 0x60006F2
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000139  // size: 0x10
    public sealed class ExclusiveOrUInt32 : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACE4EE0  token: 0x60006F4
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006F5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013A  // size: 0x10
    public sealed class ExclusiveOrUInt64 : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACE4F9C  token: 0x60006F6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006F7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013B  // size: 0x10
    public sealed class ExclusiveOrBoolean : System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
    {
        // Methods
        // RVA: 0x0ACE4CCC  token: 0x60006F8
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60006F9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000142  // size: 0x18
    public sealed class GreaterThanSByte : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000712
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE76E4  token: 0x6000713
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000143  // size: 0x18
    public sealed class GreaterThanInt16 : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000714
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE5FF0  token: 0x6000715
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000144  // size: 0x18
    public sealed class GreaterThanChar : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000716
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE51A0  token: 0x6000717
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000145  // size: 0x18
    public sealed class GreaterThanInt32 : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000718
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE60A8  token: 0x6000719
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000146  // size: 0x18
    public sealed class GreaterThanInt64 : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600071A
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE6160  token: 0x600071B
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000147  // size: 0x18
    public sealed class GreaterThanByte : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600071C
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE50E8  token: 0x600071D
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000148  // size: 0x18
    public sealed class GreaterThanUInt16 : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600071E
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE7858  token: 0x600071F
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000149  // size: 0x18
    public sealed class GreaterThanUInt32 : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000720
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE7910  token: 0x6000721
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200014A  // size: 0x18
    public sealed class GreaterThanUInt64 : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000722
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE79C8  token: 0x6000723
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200014B  // size: 0x18
    public sealed class GreaterThanSingle : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000724
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE779C  token: 0x6000725
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200014C  // size: 0x18
    public sealed class GreaterThanDouble : System.Linq.Expressions.Interpreter.GreaterThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000726
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE5258  token: 0x6000727
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200014E  // size: 0x18
    public sealed class GreaterThanOrEqualSByte : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600072D
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE7348  token: 0x600072E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200014F  // size: 0x18
    public sealed class GreaterThanOrEqualInt16 : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600072F
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE7120  token: 0x6000730
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000150  // size: 0x18
    public sealed class GreaterThanOrEqualChar : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000731
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE62D0  token: 0x6000732
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000151  // size: 0x18
    public sealed class GreaterThanOrEqualInt32 : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000733
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE71D8  token: 0x6000734
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000152  // size: 0x18
    public sealed class GreaterThanOrEqualInt64 : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000735
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE7290  token: 0x6000736
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000153  // size: 0x18
    public sealed class GreaterThanOrEqualByte : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000737
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE6218  token: 0x6000738
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000154  // size: 0x18
    public sealed class GreaterThanOrEqualUInt16 : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000739
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE74BC  token: 0x600073A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000155  // size: 0x18
    public sealed class GreaterThanOrEqualUInt32 : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600073B
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE7574  token: 0x600073C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000156  // size: 0x18
    public sealed class GreaterThanOrEqualUInt64 : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600073D
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE762C  token: 0x600073E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000157  // size: 0x18
    public sealed class GreaterThanOrEqualSingle : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600073F
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE7400  token: 0x6000740
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000158  // size: 0x18
    public sealed class GreaterThanOrEqualDouble : System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000741
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE6388  token: 0x6000742
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200015A  // size: 0x10
    public sealed class IncrementInt16 : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE7F54  token: 0x6000748
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000749
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200015B  // size: 0x10
    public sealed class IncrementInt32 : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE7FD8  token: 0x600074A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600074B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200015C  // size: 0x10
    public sealed class IncrementInt64 : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE8050  token: 0x600074C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600074D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200015D  // size: 0x10
    public sealed class IncrementUInt16 : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE8178  token: 0x600074E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600074F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200015E  // size: 0x10
    public sealed class IncrementUInt32 : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE81FC  token: 0x6000750
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000751
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200015F  // size: 0x10
    public sealed class IncrementUInt64 : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE828C  token: 0x6000752
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000753
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000160  // size: 0x10
    public sealed class IncrementSingle : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE80E0  token: 0x6000754
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000755
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000161  // size: 0x10
    public sealed class IncrementDouble : System.Linq.Expressions.Interpreter.IncrementInstruction
    {
        // Methods
        // RVA: 0x0ACE7A80  token: 0x6000756
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000757
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000164  // size: 0x38
    public sealed class DebugView
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.InstructionArray _array;  // 0x10

        // Methods
        // RVA: 0x0ACE4A98  token: 0x6000765
        public System.Void .ctor(System.Linq.Expressions.Interpreter.InstructionArray array) { }
        // RVA: 0x0ACE49AC  token: 0x6000766
        public System.Linq.Expressions.Interpreter.InstructionList.DebugView.InstructionView[] GetInstructionViews(System.Boolean includeDebugCookies) { }
        // RVA: 0x0ACE4A68  token: 0x6000767
        private System.Int32 <GetInstructionViews>b__4_0(System.Int32 index) { }

    }

    // TypeToken: 0x2000167  // size: 0x30
    public sealed struct InstructionView
    {
        // Fields
        private readonly System.Int32 _index;  // 0x10
        private readonly System.Int32 _stackDepth;  // 0x14
        private readonly System.Int32 _continuationsDepth;  // 0x18
        private readonly System.String _name;  // 0x20
        private readonly System.Linq.Expressions.Interpreter.Instruction _instruction;  // 0x28

        // Methods
        // RVA: 0x02B2ECC0  token: 0x60007DB
        private System.String GetValue() { }
        // RVA: 0x0ACEC234  token: 0x60007DC
        public System.Void .ctor(System.Linq.Expressions.Interpreter.Instruction instruction, System.String name, System.Int32 index, System.Int32 stackDepth, System.Int32 continuationsDepth) { }

    }

    // TypeToken: 0x2000166  // size: 0x10
    public sealed class DebugView
    {
        // Methods
        // RVA: 0x0ACE46AC  token: 0x60007DA
        private static System.Linq.Expressions.Interpreter.InstructionList.DebugView.InstructionView[] GetInstructionViews(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Interpreter.Instruction> instructions, System.Collections.Generic.IReadOnlyList<System.Object> objects, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>> debugCookies) { }

    }

    // TypeToken: 0x2000169  // size: 0x40
    public sealed class <GetStackTraceDebugInfo>d__29 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Linq.Expressions.Interpreter.InterpretedFrameInfo <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x28
        public System.Linq.Expressions.Interpreter.InterpretedFrame <>4__this;  // 0x30
        private System.Linq.Expressions.Interpreter.InterpretedFrame <frame>5__2;  // 0x38

        // Properties
        System.Linq.Expressions.Interpreter.InterpretedFrameInfo System.Collections.Generic.IEnumerator<System.Linq.Expressions.Interpreter.InterpretedFrameInfo>.Current { get; /* RVA: 0x04D890C0 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0ACFD1CC */ }

        // Methods
        // RVA: 0x06C07014  token: 0x60007F8
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60007F9
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0ACFD014  token: 0x60007FA
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0ACFD180  token: 0x60007FC
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0ACFD0EC  token: 0x60007FE
        private virtual System.Collections.Generic.IEnumerator<System.Linq.Expressions.Interpreter.InterpretedFrameInfo> System.Collections.Generic.IEnumerable<System.Linq.Expressions.Interpreter.InterpretedFrameInfo>.GetEnumerator() { }
        // RVA: 0x0ACFD178  token: 0x60007FF
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200016C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Linq.Expressions.Interpreter.LabelInfo.<>c <>9;  // static @ 0x0
        public static System.Func<System.Linq.Expressions.Interpreter.LabelScopeInfo,System.Linq.Expressions.Interpreter.LabelScopeInfo> <>9__9_0;  // static @ 0x8

        // Methods
        // RVA: 0x0ACFD214  token: 0x6000815
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000816
        public System.Void .ctor() { }
        // RVA: 0x09B24654  token: 0x6000817
        private System.Linq.Expressions.Interpreter.LabelScopeInfo <ValidateJump>b__9_0(System.Linq.Expressions.Interpreter.LabelScopeInfo b) { }

    }

    // TypeToken: 0x2000170  // size: 0x10
    public sealed class LeftShiftSByte : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEE5D0  token: 0x6000822
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000823
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000171  // size: 0x10
    public sealed class LeftShiftInt16 : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEE38C  token: 0x6000824
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000825
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000172  // size: 0x10
    public sealed class LeftShiftInt32 : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEE450  token: 0x6000826
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000827
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000173  // size: 0x10
    public sealed class LeftShiftInt64 : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEE504  token: 0x6000828
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000829
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000174  // size: 0x10
    public sealed class LeftShiftByte : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEDE90  token: 0x600082A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600082B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000175  // size: 0x10
    public sealed class LeftShiftUInt16 : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEE690  token: 0x600082C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600082D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000176  // size: 0x10
    public sealed class LeftShiftUInt32 : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEE754  token: 0x600082E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x600082F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000177  // size: 0x10
    public sealed class LeftShiftUInt64 : System.Linq.Expressions.Interpreter.LeftShiftInstruction
    {
        // Methods
        // RVA: 0x0ACEE820  token: 0x6000830
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000831
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000179  // size: 0x18
    public sealed class LessThanSByte : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000837
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0EE8  token: 0x6000838
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200017A  // size: 0x18
    public sealed class LessThanInt16 : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000839
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEF7F4  token: 0x600083A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200017B  // size: 0x18
    public sealed class LessThanChar : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600083B
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEE9A4  token: 0x600083C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200017C  // size: 0x18
    public sealed class LessThanInt32 : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600083D
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEF8AC  token: 0x600083E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200017D  // size: 0x18
    public sealed class LessThanInt64 : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600083F
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEF964  token: 0x6000840
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200017E  // size: 0x18
    public sealed class LessThanByte : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000841
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEE8EC  token: 0x6000842
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200017F  // size: 0x18
    public sealed class LessThanUInt16 : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000843
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF105C  token: 0x6000844
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000180  // size: 0x18
    public sealed class LessThanUInt32 : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000845
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF1114  token: 0x6000846
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000181  // size: 0x18
    public sealed class LessThanUInt64 : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000847
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF11CC  token: 0x6000848
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000182  // size: 0x18
    public sealed class LessThanSingle : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000849
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0FA0  token: 0x600084A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000183  // size: 0x18
    public sealed class LessThanDouble : System.Linq.Expressions.Interpreter.LessThanInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600084B
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEEA5C  token: 0x600084C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000185  // size: 0x18
    public sealed class LessThanOrEqualSByte : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000852
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0B4C  token: 0x6000853
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000186  // size: 0x18
    public sealed class LessThanOrEqualInt16 : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000854
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0924  token: 0x6000855
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000187  // size: 0x18
    public sealed class LessThanOrEqualChar : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000856
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEFAD4  token: 0x6000857
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000188  // size: 0x18
    public sealed class LessThanOrEqualInt32 : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000858
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF09DC  token: 0x6000859
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000189  // size: 0x18
    public sealed class LessThanOrEqualInt64 : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600085A
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0A94  token: 0x600085B
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200018A  // size: 0x18
    public sealed class LessThanOrEqualByte : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600085C
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEFA1C  token: 0x600085D
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200018B  // size: 0x18
    public sealed class LessThanOrEqualUInt16 : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x600085E
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0CC0  token: 0x600085F
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200018C  // size: 0x18
    public sealed class LessThanOrEqualUInt32 : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000860
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0D78  token: 0x6000861
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200018D  // size: 0x18
    public sealed class LessThanOrEqualUInt64 : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000862
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0E30  token: 0x6000863
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200018E  // size: 0x18
    public sealed class LessThanOrEqualSingle : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000864
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACF0C04  token: 0x6000865
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200018F  // size: 0x18
    public sealed class LessThanOrEqualDouble : System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000866
        public System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEFB8C  token: 0x6000867
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000196  // size: 0x10
    public class DebugInfoComparer : System.Collections.Generic.IComparer`1
    {
        // Methods
        // RVA: 0x0ACE4380  token: 0x6000879
        private virtual System.Int32 System.Collections.Generic.IComparer<System.Linq.Expressions.Interpreter.DebugInfo>.Compare(System.Linq.Expressions.Interpreter.DebugInfo d1, System.Linq.Expressions.Interpreter.DebugInfo d2) { }
        // RVA: 0x041E1670  token: 0x600087A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000199  // size: 0x20
    public sealed class QuoteVisitor : System.Linq.Expressions.ExpressionVisitor
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Int32> _definedParameters;  // 0x10
        public readonly System.Collections.Generic.HashSet<System.Linq.Expressions.ParameterExpression> _hoistedParameters;  // 0x18

        // Methods
        // RVA: 0x0AD10BD8  token: 0x60008DA
        protected internal virtual System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node) { }
        // RVA: 0x0AD10A5C  token: 0x60008DB
        protected internal virtual System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node) { }
        // RVA: 0x0AD10AC4  token: 0x60008DC
        protected virtual System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node) { }
        // RVA: -1  // generic def  token: 0x60008DD
        protected internal virtual System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node) { }
        // RVA: 0x0AD108BC  token: 0x60008DE
        private System.Void PushParameters(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: 0x0AD10718  token: 0x60008DF
        private System.Void PopParameters(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: 0x0AD10C58  token: 0x60008E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200019A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Linq.Expressions.Interpreter.LightCompiler.<>c <>9;  // static @ 0x0
        public static System.Func<System.Linq.Expressions.Expression,System.Boolean> <>9__56_1;  // static @ 0x8
        public static System.Func<System.Linq.Expressions.SwitchCase,System.Boolean> <>9__56_0;  // static @ 0x10
        public static System.Action<System.Linq.Expressions.Interpreter.LightCompiler,System.Linq.Expressions.Expression> <>9__101_0;  // static @ 0x18

        // Methods
        // RVA: 0x0AD147FC  token: 0x60008E1
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60008E2
        public System.Void .ctor() { }
        // RVA: 0x0AD14600  token: 0x60008E3
        private System.Boolean <CompileSwitchExpression>b__56_0(System.Linq.Expressions.SwitchCase c) { }
        // RVA: 0x0AD14714  token: 0x60008E4
        private System.Boolean <CompileSwitchExpression>b__56_1(System.Linq.Expressions.Expression t) { }
        // RVA: 0x0AD145D8  token: 0x60008E5
        private System.Void <CompileNoLabelPush>b__101_0(System.Linq.Expressions.Interpreter.LightCompiler this, System.Linq.Expressions.Expression e) { }

    }

    // TypeToken: 0x20001A3  // size: 0x38
    public class DebugViewPrinter
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.Interpreter _interpreter;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.Int32,System.Int32> _tryStart;  // 0x18
        private readonly System.Collections.Generic.Dictionary<System.Int32,System.String> _handlerEnter;  // 0x20
        private readonly System.Collections.Generic.Dictionary<System.Int32,System.Int32> _handlerExit;  // 0x28
        private System.String _indent;  // 0x30

        // Methods
        // RVA: 0x0AD00D64  token: 0x6000946
        public System.Void .ctor(System.Linq.Expressions.Interpreter.Interpreter interpreter) { }
        // RVA: 0x0AD0047C  token: 0x6000947
        private System.Void Analyze() { }
        // RVA: 0x0AD003C8  token: 0x6000948
        private System.Void AddTryStart(System.Int32 index) { }
        // RVA: 0x0AD00324  token: 0x6000949
        private System.Void AddHandlerExit(System.Int32 index) { }
        // RVA: 0x0AD00808  token: 0x600094A
        private System.Void Indent() { }
        // RVA: 0x0AD00708  token: 0x600094B
        private System.Void Dedent() { }
        // RVA: 0x0AD00848  token: 0x600094C
        public virtual System.String ToString() { }
        // RVA: 0x0AD00748  token: 0x600094D
        private System.Void EmitExits(System.Text.StringBuilder sb, System.Int32 index) { }

    }

    // TypeToken: 0x20001A4  // size: 0x20
    public sealed class <>c__DisplayClass74_0
    {
        // Fields
        public System.Reflection.MethodInfo targetMethod;  // 0x10
        public System.Type delegateType;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600094E
        public System.Void .ctor() { }
        // RVA: 0x0AD14774  token: 0x600094F
        private System.Delegate <MakeRunDelegateCtor>b__0(System.Linq.Expressions.Interpreter.LightLambda lambda) { }

    }

    // TypeToken: 0x20001B2  // size: 0x18
    public sealed class Reference : System.Linq.Expressions.Interpreter.InitializeLocalInstruction, System.Linq.Expressions.Interpreter.IBoxableInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD10DC4 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000984
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD10D90  token: 0x6000985
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD10D3C  token: 0x6000986
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001B3  // size: 0x20
    public sealed class ImmutableValue : System.Linq.Expressions.Interpreter.InitializeLocalInstruction, System.Linq.Expressions.Interpreter.IBoxableInstruction
    {
        // Fields
        private readonly System.Object _defaultValue;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD05A98 */ }

        // Methods
        // RVA: 0x05391D70  token: 0x6000988
        private System.Void .ctor(System.Int32 index, System.Object defaultValue) { }
        // RVA: 0x0AD05A3C  token: 0x6000989
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD059CC  token: 0x600098A
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001B4  // size: 0x20
    public sealed class ImmutableBox : System.Linq.Expressions.Interpreter.InitializeLocalInstruction
    {
        // Fields
        private readonly System.Object _defaultValue;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD058DC */ }

        // Methods
        // RVA: 0x05391D70  token: 0x600098C
        private System.Void .ctor(System.Int32 index, System.Object defaultValue) { }
        // RVA: 0x0AD05824  token: 0x600098D
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001B5  // size: 0x18
    public sealed class ImmutableRefBox : System.Linq.Expressions.Interpreter.InitializeLocalInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD059A0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600098F
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD05908  token: 0x6000990
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001B6  // size: 0x18
    public sealed class ParameterBox : System.Linq.Expressions.Interpreter.InitializeLocalInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD101FC */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000992
        public System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD10134  token: 0x6000993
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001B7  // size: 0x18
    public sealed class Parameter : System.Linq.Expressions.Interpreter.InitializeLocalInstruction, System.Linq.Expressions.Interpreter.IBoxableInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD103A8 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000995
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x04D85B80  token: 0x6000996
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD10354  token: 0x6000997
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001B8  // size: 0x20
    public sealed class MutableValue : System.Linq.Expressions.Interpreter.InitializeLocalInstruction, System.Linq.Expressions.Interpreter.IBoxableInstruction
    {
        // Fields
        private readonly System.Type _type;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD0BA6C */ }

        // Methods
        // RVA: 0x05391D70  token: 0x6000999
        private System.Void .ctor(System.Int32 index, System.Type type) { }
        // RVA: 0x0AD0B990  token: 0x600099A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD0B920  token: 0x600099B
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001B9  // size: 0x20
    public sealed class MutableBox : System.Linq.Expressions.Interpreter.InitializeLocalInstruction
    {
        // Fields
        private readonly System.Type _type;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD0B8F4 */ }

        // Methods
        // RVA: 0x05391D70  token: 0x600099D
        private System.Void .ctor(System.Int32 index, System.Type type) { }
        // RVA: 0x0AD0B7BC  token: 0x600099E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001BE  // size: 0x30
    public sealed class VariableScope
    {
        // Fields
        public readonly System.Int32 Start;  // 0x10
        public System.Int32 Stop;  // 0x14
        public readonly System.Linq.Expressions.Interpreter.LocalVariable Variable;  // 0x18
        public readonly System.Linq.Expressions.Interpreter.LocalVariables.VariableScope Parent;  // 0x20
        public System.Collections.Generic.List<System.Linq.Expressions.Interpreter.LocalVariables.VariableScope> ChildScopes;  // 0x28

        // Methods
        // RVA: 0x0AD15614  token: 0x60009B7
        public System.Void .ctor(System.Linq.Expressions.Interpreter.LocalVariable variable, System.Int32 start, System.Linq.Expressions.Interpreter.LocalVariables.VariableScope parent) { }

    }

    // TypeToken: 0x20001C0  // size: 0x10
    public sealed class ModuloInt16 : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD09634  token: 0x60009BD
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009BE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C1  // size: 0x10
    public sealed class ModuloInt32 : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD09748  token: 0x60009BF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C2  // size: 0x10
    public sealed class ModuloInt64 : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD09850  token: 0x60009C1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009C2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C3  // size: 0x10
    public sealed class ModuloUInt16 : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD09A94  token: 0x60009C3
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009C4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C4  // size: 0x10
    public sealed class ModuloUInt32 : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD09BA8  token: 0x60009C5
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009C6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C5  // size: 0x10
    public sealed class ModuloUInt64 : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD09CBC  token: 0x60009C7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009C8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C6  // size: 0x10
    public sealed class ModuloSingle : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD09968  token: 0x60009C9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009CA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C7  // size: 0x10
    public sealed class ModuloDouble : System.Linq.Expressions.Interpreter.ModuloInstruction
    {
        // Methods
        // RVA: 0x0AD090CC  token: 0x60009CB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009CC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001C9  // size: 0x10
    public sealed class MulInt16 : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD0A330  token: 0x60009D2
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009D3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001CA  // size: 0x10
    public sealed class MulInt32 : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD0A444  token: 0x60009D4
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009D5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001CB  // size: 0x10
    public sealed class MulInt64 : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD0A548  token: 0x60009D6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009D7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001CC  // size: 0x10
    public sealed class MulUInt16 : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD0B484  token: 0x60009D8
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009D9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001CD  // size: 0x10
    public sealed class MulUInt32 : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD0B598  token: 0x60009DA
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009DB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001CE  // size: 0x10
    public sealed class MulUInt64 : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD0B6A8  token: 0x60009DC
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009DD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001CF  // size: 0x10
    public sealed class MulSingle : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD0B364  token: 0x60009DE
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009DF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D0  // size: 0x10
    public sealed class MulDouble : System.Linq.Expressions.Interpreter.MulInstruction
    {
        // Methods
        // RVA: 0x0AD09DD4  token: 0x60009E0
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009E1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D2  // size: 0x10
    public sealed class MulOvfInt16 : System.Linq.Expressions.Interpreter.MulOvfInstruction
    {
        // Methods
        // RVA: 0x0AD0A9A4  token: 0x60009E7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009E8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D3  // size: 0x10
    public sealed class MulOvfInt32 : System.Linq.Expressions.Interpreter.MulOvfInstruction
    {
        // Methods
        // RVA: 0x0AD0AB9C  token: 0x60009E9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009EA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D4  // size: 0x10
    public sealed class MulOvfInt64 : System.Linq.Expressions.Interpreter.MulOvfInstruction
    {
        // Methods
        // RVA: 0x0AD0AD34  token: 0x60009EB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D5  // size: 0x10
    public sealed class MulOvfUInt16 : System.Linq.Expressions.Interpreter.MulOvfInstruction
    {
        // Methods
        // RVA: 0x0AD0AEA4  token: 0x60009ED
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009EE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D6  // size: 0x10
    public sealed class MulOvfUInt32 : System.Linq.Expressions.Interpreter.MulOvfInstruction
    {
        // Methods
        // RVA: 0x0AD0B084  token: 0x60009EF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009F0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D7  // size: 0x10
    public sealed class MulOvfUInt64 : System.Linq.Expressions.Interpreter.MulOvfInstruction
    {
        // Methods
        // RVA: 0x0AD0B1F4  token: 0x60009F1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009F2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001D9  // size: 0x10
    public sealed class NegateInt16 : System.Linq.Expressions.Interpreter.NegateInstruction
    {
        // Methods
        // RVA: 0x0AD0C31C  token: 0x60009F8
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009F9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001DA  // size: 0x10
    public sealed class NegateInt32 : System.Linq.Expressions.Interpreter.NegateInstruction
    {
        // Methods
        // RVA: 0x0AD0C398  token: 0x60009FA
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001DB  // size: 0x10
    public sealed class NegateInt64 : System.Linq.Expressions.Interpreter.NegateInstruction
    {
        // Methods
        // RVA: 0x0AD0C410  token: 0x60009FC
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009FD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001DC  // size: 0x10
    public sealed class NegateSingle : System.Linq.Expressions.Interpreter.NegateInstruction
    {
        // Methods
        // RVA: 0x0AD0C4A0  token: 0x60009FE
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x60009FF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001DD  // size: 0x10
    public sealed class NegateDouble : System.Linq.Expressions.Interpreter.NegateInstruction
    {
        // Methods
        // RVA: 0x0AD0BF84  token: 0x6000A00
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A01
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001DF  // size: 0x10
    public sealed class NegateCheckedInt32 : System.Linq.Expressions.Interpreter.NegateCheckedInstruction
    {
        // Methods
        // RVA: 0x0AD0BDC0  token: 0x6000A07
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A08
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001E0  // size: 0x10
    public sealed class NegateCheckedInt16 : System.Linq.Expressions.Interpreter.NegateCheckedInstruction
    {
        // Methods
        // RVA: 0x0AD0BCA0  token: 0x6000A09
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A0A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001E1  // size: 0x10
    public sealed class NegateCheckedInt64 : System.Linq.Expressions.Interpreter.NegateCheckedInstruction
    {
        // Methods
        // RVA: 0x0AD0BEA4  token: 0x6000A0B
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A0C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001E5  // size: 0x10
    public sealed class NotEqualBoolean : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0C97C  token: 0x6000A1C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A1D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001E6  // size: 0x10
    public sealed class NotEqualSByte : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DF40  token: 0x6000A1E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A1F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001E7  // size: 0x10
    public sealed class NotEqualInt16 : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DAC8  token: 0x6000A20
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A21
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001E8  // size: 0x10
    public sealed class NotEqualChar : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0CC3C  token: 0x6000A22
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A23
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001E9  // size: 0x10
    public sealed class NotEqualInt32 : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DC2C  token: 0x6000A24
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A25
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001EA  // size: 0x10
    public sealed class NotEqualInt64 : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DD8C  token: 0x6000A26
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A27
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001EB  // size: 0x10
    public sealed class NotEqualByte : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0CADC  token: 0x6000A28
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A29
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001EC  // size: 0x10
    public sealed class NotEqualUInt16 : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0E22C  token: 0x6000A2A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A2B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001ED  // size: 0x10
    public sealed class NotEqualUInt32 : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0E390  token: 0x6000A2C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A2D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001EE  // size: 0x10
    public sealed class NotEqualUInt64 : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0E4F0  token: 0x6000A2E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A2F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001EF  // size: 0x10
    public sealed class NotEqualSingle : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0E0B4  token: 0x6000A30
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A31
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F0  // size: 0x10
    public sealed class NotEqualDouble : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0CDB4  token: 0x6000A32
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A33
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F1  // size: 0x10
    public sealed class NotEqualReference : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DE3C  token: 0x6000A34
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A35
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F2  // size: 0x10
    public sealed class NotEqualSByteLiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DE8C  token: 0x6000A36
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A37
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F3  // size: 0x10
    public sealed class NotEqualInt16LiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DA14  token: 0x6000A38
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A39
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F4  // size: 0x10
    public sealed class NotEqualCharLiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0CB88  token: 0x6000A3A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A3B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F5  // size: 0x10
    public sealed class NotEqualInt32LiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DB78  token: 0x6000A3C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A3D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F6  // size: 0x10
    public sealed class NotEqualInt64LiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DCD8  token: 0x6000A3E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A3F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F7  // size: 0x10
    public sealed class NotEqualByteLiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0CA28  token: 0x6000A40
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A41
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F8  // size: 0x10
    public sealed class NotEqualUInt16LiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0E178  token: 0x6000A42
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A43
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001F9  // size: 0x10
    public sealed class NotEqualUInt32LiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0E2DC  token: 0x6000A44
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A45
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001FA  // size: 0x10
    public sealed class NotEqualUInt64LiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0E43C  token: 0x6000A46
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A47
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001FB  // size: 0x10
    public sealed class NotEqualSingleLiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0DFEC  token: 0x6000A48
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A49
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001FC  // size: 0x10
    public sealed class NotEqualDoubleLiftedToNull : System.Linq.Expressions.Interpreter.NotEqualInstruction
    {
        // Methods
        // RVA: 0x0AD0CCEC  token: 0x6000A4A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A4B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001FE  // size: 0x10
    public sealed class NotBoolean : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0C888  token: 0x6000A51
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A52
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001FF  // size: 0x10
    public sealed class NotInt64 : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0EB50  token: 0x6000A53
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A54
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000200  // size: 0x10
    public sealed class NotInt32 : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0EAD8  token: 0x6000A55
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A56
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000201  // size: 0x10
    public sealed class NotInt16 : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0EA5C  token: 0x6000A57
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A58
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000202  // size: 0x10
    public sealed class NotUInt64 : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0ED64  token: 0x6000A59
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A5A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000203  // size: 0x10
    public sealed class NotUInt32 : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0ECD4  token: 0x6000A5B
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A5C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000204  // size: 0x10
    public sealed class NotUInt16 : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0EC58  token: 0x6000A5D
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A5E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000205  // size: 0x10
    public sealed class NotByte : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0C904  token: 0x6000A5F
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A60
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000206  // size: 0x10
    public sealed class NotSByte : System.Linq.Expressions.Interpreter.NotInstruction
    {
        // Methods
        // RVA: 0x0AD0EBE0  token: 0x6000A61
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A62
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000209  // size: 0x20
    public sealed class Unchecked : System.Linq.Expressions.Interpreter.NumericConvertInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD154B8 */ }

        // Methods
        // RVA: 0x04DA8680  token: 0x6000A71
        public System.Void .ctor(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull) { }
        // RVA: 0x0AD15258  token: 0x6000A72
        protected virtual System.Object Convert(System.Object obj) { }
        // RVA: 0x0AD14AE0  token: 0x6000A73
        private System.Object ConvertInt32(System.Int32 obj) { }
        // RVA: 0x0AD14D70  token: 0x6000A74
        private System.Object ConvertInt64(System.Int64 obj) { }
        // RVA: 0x0AD14FC0  token: 0x6000A75
        private System.Object ConvertUInt64(System.UInt64 obj) { }
        // RVA: 0x0AD14860  token: 0x6000A76
        private System.Object ConvertDouble(System.Double obj) { }

    }

    // TypeToken: 0x200020A  // size: 0x20
    public sealed class Checked : System.Linq.Expressions.Interpreter.NumericConvertInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACFFE84 */ }

        // Methods
        // RVA: 0x04DA8680  token: 0x6000A78
        public System.Void .ctor(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull) { }
        // RVA: 0x0ACFFC24  token: 0x6000A79
        protected virtual System.Object Convert(System.Object obj) { }
        // RVA: 0x0ACFF1D8  token: 0x6000A7A
        private System.Object ConvertInt32(System.Int32 obj) { }
        // RVA: 0x0ACFF51C  token: 0x6000A7B
        private System.Object ConvertInt64(System.Int64 obj) { }
        // RVA: 0x0ACFF868  token: 0x6000A7C
        private System.Object ConvertUInt64(System.UInt64 obj) { }
        // RVA: 0x0ACFEDE8  token: 0x6000A7D
        private System.Object ConvertDouble(System.Double obj) { }

    }

    // TypeToken: 0x200020B  // size: 0x20
    public sealed class ToUnderlying : System.Linq.Expressions.Interpreter.NumericConvertInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD14134 */ }

        // Methods
        // RVA: 0x04DAA000  token: 0x6000A7F
        public System.Void .ctor(System.TypeCode to, System.Boolean isLiftedToNull) { }
        // RVA: 0x0AD13EAC  token: 0x6000A80
        protected virtual System.Object Convert(System.Object obj) { }

    }

    // TypeToken: 0x200020D  // size: 0x10
    public sealed class OrSByte : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0FE58  token: 0x6000A86
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A87
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200020E  // size: 0x10
    public sealed class OrInt16 : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0FC38  token: 0x6000A88
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A89
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200020F  // size: 0x10
    public sealed class OrInt32 : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0FCE8  token: 0x6000A8A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A8B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000210  // size: 0x10
    public sealed class OrInt64 : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0FD98  token: 0x6000A8C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A8D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000211  // size: 0x10
    public sealed class OrByte : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0F6CC  token: 0x6000A8E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A8F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000212  // size: 0x10
    public sealed class OrUInt16 : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0FF08  token: 0x6000A90
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A91
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000213  // size: 0x10
    public sealed class OrUInt32 : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0FFB8  token: 0x6000A92
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A93
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000214  // size: 0x10
    public sealed class OrUInt64 : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD10074  token: 0x6000A94
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A95
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000215  // size: 0x10
    public sealed class OrBoolean : System.Linq.Expressions.Interpreter.OrInstruction
    {
        // Methods
        // RVA: 0x0AD0F5D4  token: 0x6000A96
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A97
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000217  // size: 0x10
    public sealed class RightShiftSByte : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD1152C  token: 0x6000A9D
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000A9E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000218  // size: 0x10
    public sealed class RightShiftInt16 : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD112EC  token: 0x6000A9F
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AA0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000219  // size: 0x10
    public sealed class RightShiftInt32 : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD113AC  token: 0x6000AA1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AA2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200021A  // size: 0x10
    public sealed class RightShiftInt64 : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD11460  token: 0x6000AA3
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AA4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200021B  // size: 0x10
    public sealed class RightShiftByte : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD10DF0  token: 0x6000AA5
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AA6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200021C  // size: 0x10
    public sealed class RightShiftUInt16 : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD115EC  token: 0x6000AA7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AA8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200021D  // size: 0x10
    public sealed class RightShiftUInt32 : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD116AC  token: 0x6000AA9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AAA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200021E  // size: 0x10
    public sealed class RightShiftUInt64 : System.Linq.Expressions.Interpreter.RightShiftInstruction
    {
        // Methods
        // RVA: 0x0AD11778  token: 0x6000AAB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AAC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000225  // size: 0x10
    public sealed class SubInt16 : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD12650  token: 0x6000AC9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000ACA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000226  // size: 0x10
    public sealed class SubInt32 : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD12764  token: 0x6000ACB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000ACC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000227  // size: 0x10
    public sealed class SubInt64 : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD12868  token: 0x6000ACD
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000ACE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000228  // size: 0x10
    public sealed class SubUInt16 : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD137BC  token: 0x6000ACF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AD0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000229  // size: 0x10
    public sealed class SubUInt32 : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD138D0  token: 0x6000AD1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AD2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200022A  // size: 0x10
    public sealed class SubUInt64 : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD139E0  token: 0x6000AD3
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AD4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200022B  // size: 0x10
    public sealed class SubSingle : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD13698  token: 0x6000AD5
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AD6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200022C  // size: 0x10
    public sealed class SubDouble : System.Linq.Expressions.Interpreter.SubInstruction
    {
        // Methods
        // RVA: 0x0AD120F0  token: 0x6000AD7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AD8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200022E  // size: 0x10
    public sealed class SubOvfInt16 : System.Linq.Expressions.Interpreter.SubOvfInstruction
    {
        // Methods
        // RVA: 0x0AD12CC4  token: 0x6000ADE
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000ADF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200022F  // size: 0x10
    public sealed class SubOvfInt32 : System.Linq.Expressions.Interpreter.SubOvfInstruction
    {
        // Methods
        // RVA: 0x0AD12EBC  token: 0x6000AE0
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AE1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000230  // size: 0x10
    public sealed class SubOvfInt64 : System.Linq.Expressions.Interpreter.SubOvfInstruction
    {
        // Methods
        // RVA: 0x0AD13054  token: 0x6000AE2
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AE3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000231  // size: 0x10
    public sealed class SubOvfUInt16 : System.Linq.Expressions.Interpreter.SubOvfInstruction
    {
        // Methods
        // RVA: 0x0AD131C4  token: 0x6000AE4
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AE5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000232  // size: 0x10
    public sealed class SubOvfUInt32 : System.Linq.Expressions.Interpreter.SubOvfInstruction
    {
        // Methods
        // RVA: 0x0AD133B8  token: 0x6000AE6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AE7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000233  // size: 0x10
    public sealed class SubOvfUInt64 : System.Linq.Expressions.Interpreter.SubOvfInstruction
    {
        // Methods
        // RVA: 0x0AD13528  token: 0x6000AE8
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000AE9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000239  // size: 0x10
    public sealed class HasValue : System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
    {
        // Methods
        // RVA: 0x0AD057E8  token: 0x6000B07
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000B08
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200023A  // size: 0x10
    public sealed class GetValue : System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
    {
        // Methods
        // RVA: 0x0AD05784  token: 0x6000B09
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000B0A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200023B  // size: 0x18
    public sealed class GetValueOrDefault : System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
    {
        // Fields
        private readonly System.Type _defaultValueType;  // 0x10

        // Methods
        // RVA: 0x0AD05754  token: 0x6000B0B
        public System.Void .ctor(System.Reflection.MethodInfo mi) { }
        // RVA: 0x0AD056F4  token: 0x6000B0C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200023C  // size: 0x10
    public sealed class GetValueOrDefault1 : System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x0AD056A0  token: 0x6000B0E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000B0F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200023D  // size: 0x10
    public sealed class EqualsClass : System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x0AD02D24  token: 0x6000B11
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000B12
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200023E  // size: 0x10
    public sealed class ToStringClass : System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
    {
        // Methods
        // RVA: 0x0AD13E3C  token: 0x6000B13
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000B14
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200023F  // size: 0x10
    public sealed class GetHashCodeClass : System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
    {
        // Methods
        // RVA: 0x0AD05648  token: 0x6000B15
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000B16
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000241
    public sealed class CastInstructionT`1 : System.Linq.Expressions.Interpreter.CastInstruction
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000B1C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: -1  // not resolved  token: 0x6000B1D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000243  // size: 0x18
    public sealed class Ref : CastInstructionNoT
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000B22
        public System.Void .ctor(System.Type t) { }
        // RVA: 0x0AD10D18  token: 0x6000B23
        protected virtual System.Void ConvertNull(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000244  // size: 0x18
    public sealed class Value : CastInstructionNoT
    {
        // Methods
        // RVA: 0x053908C0  token: 0x6000B24
        public System.Void .ctor(System.Type t) { }
        // RVA: 0x0AD155C8  token: 0x6000B25
        protected virtual System.Void ConvertNull(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000242  // size: 0x18
    public abstract class CastInstructionNoT : System.Linq.Expressions.Interpreter.CastInstruction
    {
        // Fields
        private readonly System.Type _t;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000B1E
        protected System.Void .ctor(System.Type t) { }
        // RVA: 0x0ACFDFEC  token: 0x6000B1F
        public static System.Linq.Expressions.Interpreter.CastInstruction Create(System.Type t) { }
        // RVA: 0x0ACFE0B4  token: 0x6000B20
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: -1  // abstract  token: 0x6000B21
        protected virtual System.Void ConvertNull(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000248  // size: 0x28
    public sealed class ExpressionQuoter : System.Linq.Expressions.ExpressionVisitor
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> _variables;  // 0x10
        private readonly System.Linq.Expressions.Interpreter.InterpretedFrame _frame;  // 0x18
        private readonly System.Collections.Generic.Stack<System.Collections.Generic.HashSet<System.Linq.Expressions.ParameterExpression>> _shadowedVars;  // 0x20

        // Methods
        // RVA: 0x0AD0542C  token: 0x6000B2E
        private System.Void .ctor(System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> hoistedVariables, System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: -1  // generic def  token: 0x6000B2F
        protected internal virtual System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node) { }
        // RVA: 0x0AD05080  token: 0x6000B30
        protected internal virtual System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node) { }
        // RVA: 0x0AD051FC  token: 0x6000B31
        protected virtual System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node) { }
        // RVA: 0x0AD0537C  token: 0x6000B32
        protected internal virtual System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node) { }
        // RVA: 0x0AD04FA8  token: 0x6000B33
        private System.Runtime.CompilerServices.IStrongBox GetBox(System.Linq.Expressions.ParameterExpression variable) { }

    }

    // TypeToken: 0x200024A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Linq.Expressions.Interpreter.DelegateHelpers.<>c <>9;  // static @ 0x0
        public static System.Func<System.Type,System.Boolean> <>9__1_0;  // static @ 0x8

        // Methods
        // RVA: 0x0AD14798  token: 0x6000B35
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000B36
        public System.Void .ctor() { }
        // RVA: 0x0AD14754  token: 0x6000B37
        private System.Boolean <MakeDelegate>b__1_0(System.Type t) { }

    }

    // TypeToken: 0x200024E
    public sealed class <GetEnumeratorWorker>d__7 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Collections.Generic.KeyValuePair<TKey,TValue> <>2__current;  // 0x0
        public System.Linq.Expressions.Interpreter.HybridReferenceDictionary<TKey,TValue> <>4__this;  // 0x0
        private System.Int32 <i>5__2;  // 0x0

        // Properties
        System.Collections.Generic.KeyValuePair<TKey,TValue> System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B43
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000B44
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000B45
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000B47
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000252
    public sealed class <>c
    {
        // Fields
        public static readonly System.Runtime.CompilerServices.CallSite.<>c<T> <>9;  // static @ 0x0
        public static System.Converter<System.Reflection.ParameterInfo,System.Linq.Expressions.ParameterExpression> <>9__20_0;  // static @ 0x0
        public static System.Converter<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Expression> <>9__20_1;  // static @ 0x0
        public static System.Converter<System.Reflection.ParameterInfo,System.Linq.Expressions.ParameterExpression> <>9__21_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B5D
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000B5E
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000B5F
        private System.Linq.Expressions.ParameterExpression <CreateCustomUpdateDelegate>b__20_0(System.Reflection.ParameterInfo p) { }
        // RVA: -1  // not resolved  token: 0x6000B60
        private System.Linq.Expressions.Expression <CreateCustomUpdateDelegate>b__20_1(System.Linq.Expressions.ParameterExpression p) { }
        // RVA: -1  // not resolved  token: 0x6000B61
        private System.Linq.Expressions.ParameterExpression <CreateCustomNoMatchDelegate>b__21_0(System.Reflection.ParameterInfo p) { }

    }

    // TypeToken: 0x2000254
    public sealed class LambdaSignature`1
    {
        // Fields
        private static System.Runtime.CompilerServices.CallSiteBinder.LambdaSignature<T> s_instance;  // static @ 0x0
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> Parameters;  // 0x0
        private readonly System.Linq.Expressions.LabelTarget ReturnLabel;  // 0x0

        // Properties
        System.Runtime.CompilerServices.CallSiteBinder.LambdaSignature<T> Instance { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B6C
        private System.Void .ctor() { }

    }

    // TypeToken: 0x2000258
    public class Enumerator : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private readonly System.Runtime.CompilerServices.ReadOnlyCollectionBuilder<T> _builder;  // 0x0
        private readonly System.Int32 _version;  // 0x0
        private System.Int32 _index;  // 0x0
        private T _current;  // 0x0

        // Properties
        T Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B9A
        private System.Void .ctor(System.Runtime.CompilerServices.ReadOnlyCollectionBuilder<T> builder) { }
        // RVA: -1  // not resolved  token: 0x6000B9C
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000B9E
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000B9F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000260  // size: 0x20
    public sealed struct AndNode
    {
        // Fields
        private System.Int32 Depth;  // 0x10
        private System.Linq.Expressions.Expression Node;  // 0x18

    }

    // TypeToken: 0x200025F  // size: 0x20
    public sealed class TestBuilder
    {
        // Fields
        private readonly System.Collections.Generic.HashSet<System.Dynamic.BindingRestrictions> _unique;  // 0x10
        private readonly System.Collections.Generic.Stack<System.Dynamic.BindingRestrictions.TestBuilder.AndNode> _tests;  // 0x18

        // Methods
        // RVA: 0x0AD13AF4  token: 0x6000BB8
        private System.Void Append(System.Dynamic.BindingRestrictions restrictions) { }
        // RVA: 0x0AD13CB0  token: 0x6000BB9
        private System.Linq.Expressions.Expression ToExpression() { }
        // RVA: 0x0AD13B70  token: 0x6000BBA
        private System.Void Push(System.Linq.Expressions.Expression node, System.Int32 depth) { }
        // RVA: 0x0AD13D7C  token: 0x6000BBB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000261  // size: 0x20
    public sealed class MergedRestriction : System.Dynamic.BindingRestrictions
    {
        // Fields
        private readonly System.Dynamic.BindingRestrictions Left;  // 0x10
        private readonly System.Dynamic.BindingRestrictions Right;  // 0x18

        // Methods
        // RVA: 0x0AD09060  token: 0x6000BBC
        private System.Void .ctor(System.Dynamic.BindingRestrictions left, System.Dynamic.BindingRestrictions right) { }
        // RVA: 0x0AD08F34  token: 0x6000BBD
        private virtual System.Linq.Expressions.Expression GetExpression() { }

    }

    // TypeToken: 0x2000262  // size: 0x18
    public sealed class CustomRestriction : System.Dynamic.BindingRestrictions
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _expression;  // 0x10

        // Methods
        // RVA: 0x0AD002CC  token: 0x6000BBE
        private System.Void .ctor(System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0AD00244  token: 0x6000BBF
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AD002A4  token: 0x6000BC0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x02B2ECC0  token: 0x6000BC1
        private virtual System.Linq.Expressions.Expression GetExpression() { }

    }

    // TypeToken: 0x2000263  // size: 0x20
    public sealed class TypeRestriction : System.Dynamic.BindingRestrictions
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _expression;  // 0x10
        private readonly System.Type _type;  // 0x18

        // Methods
        // RVA: 0x0AD1456C  token: 0x6000BC2
        private System.Void .ctor(System.Linq.Expressions.Expression parameter, System.Type type) { }
        // RVA: 0x0AD1442C  token: 0x6000BC3
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AD1451C  token: 0x6000BC4
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AD144C4  token: 0x6000BC5
        private virtual System.Linq.Expressions.Expression GetExpression() { }

    }

    // TypeToken: 0x2000264  // size: 0x20
    public sealed class InstanceRestriction : System.Dynamic.BindingRestrictions
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _expression;  // 0x10
        private readonly System.Object _instance;  // 0x18

        // Methods
        // RVA: 0x0AD06194  token: 0x6000BC6
        private System.Void .ctor(System.Linq.Expressions.Expression parameter, System.Object instance) { }
        // RVA: 0x0AD05DF8  token: 0x6000BC7
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AD06158  token: 0x6000BC8
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AD05E6C  token: 0x6000BC9
        private virtual System.Linq.Expressions.Expression GetExpression() { }

    }

    // TypeToken: 0x2000265  // size: 0x10
    public sealed class BindingRestrictionsProxy
    {
    }

    // TypeToken: 0x200026E  // size: 0x10
    public sealed class KeyCollectionDebugView
    {
    }

    // TypeToken: 0x2000270  // size: 0x30
    public sealed class <GetEnumerator>d__15 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.String <>2__current;  // 0x18
        public System.Dynamic.ExpandoObject.KeyCollection <>4__this;  // 0x20
        private System.Int32 <i>5__2;  // 0x28
        private System.Int32 <n>5__3;  // 0x2c

        // Properties
        System.String System.Collections.Generic.IEnumerator<System.String>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000C2C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000C2D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0AD1C360  token: 0x6000C2E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0AD1C4EC  token: 0x6000C30
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200026F  // size: 0x28
    public class KeyCollection : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Dynamic.ExpandoObject _expando;  // 0x10
        private readonly System.Int32 _expandoVersion;  // 0x18
        private readonly System.Int32 _expandoCount;  // 0x1c
        private readonly System.Dynamic.ExpandoObject.ExpandoData _expandoData;  // 0x20

        // Properties
        System.Int32 Count { get; /* RVA: 0x0AD06864 */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0AD066E8  token: 0x6000C21
        private System.Void .ctor(System.Dynamic.ExpandoObject expando) { }
        // RVA: 0x0AD06228  token: 0x6000C22
        private System.Void CheckVersion() { }
        // RVA: 0x0AD06200  token: 0x6000C23
        public virtual System.Void Add(System.String item) { }
        // RVA: 0x0AD06288  token: 0x6000C24
        public virtual System.Void Clear() { }
        // RVA: 0x0AD062B0  token: 0x6000C25
        public virtual System.Boolean Contains(System.String item) { }
        // RVA: 0x0AD063BC  token: 0x6000C26
        public virtual System.Void CopyTo(System.String[] array, System.Int32 arrayIndex) { }
        // RVA: 0x0AD066B8  token: 0x6000C29
        public virtual System.Boolean Remove(System.String item) { }
        // RVA: 0x0AD0665C  token: 0x6000C2A
        public virtual System.Collections.Generic.IEnumerator<System.String> GetEnumerator() { }
        // RVA: 0x0AD066E0  token: 0x6000C2B
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000271  // size: 0x10
    public sealed class ValueCollectionDebugView
    {
    }

    // TypeToken: 0x2000273  // size: 0x38
    public sealed class <GetEnumerator>d__15 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Dynamic.ExpandoObject.ValueCollection <>4__this;  // 0x20
        private System.Dynamic.ExpandoObject.ExpandoData <data>5__2;  // 0x28
        private System.Int32 <i>5__3;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000C3D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000C3E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0AD1C24C  token: 0x6000C3F
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0AD1C4A0  token: 0x6000C41
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000272  // size: 0x28
    public class ValueCollection : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Dynamic.ExpandoObject _expando;  // 0x10
        private readonly System.Int32 _expandoVersion;  // 0x18
        private readonly System.Int32 _expandoCount;  // 0x1c
        private readonly System.Dynamic.ExpandoObject.ExpandoData _expandoData;  // 0x20

        // Properties
        System.Int32 Count { get; /* RVA: 0x0AD1CE74 */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0AD066E8  token: 0x6000C32
        private System.Void .ctor(System.Dynamic.ExpandoObject expando) { }
        // RVA: 0x0AD1C95C  token: 0x6000C33
        private System.Void CheckVersion() { }
        // RVA: 0x0AD1C934  token: 0x6000C34
        public virtual System.Void Add(System.Object item) { }
        // RVA: 0x0AD1C9BC  token: 0x6000C35
        public virtual System.Void Clear() { }
        // RVA: 0x0AD1C9E4  token: 0x6000C36
        public virtual System.Boolean Contains(System.Object item) { }
        // RVA: 0x0AD1CB60  token: 0x6000C37
        public virtual System.Void CopyTo(System.Object[] array, System.Int32 arrayIndex) { }
        // RVA: 0x0AD1CE44  token: 0x6000C3A
        public virtual System.Boolean Remove(System.Object item) { }
        // RVA: 0x0AD1CDE8  token: 0x6000C3B
        public virtual System.Collections.Generic.IEnumerator<System.Object> GetEnumerator() { }
        // RVA: 0x0AD1CE6C  token: 0x6000C3C
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000275  // size: 0x20
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Dynamic.InvokeMemberBinder binder;  // 0x10
        public System.Dynamic.DynamicMetaObject[] args;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000C4F
        public System.Void .ctor() { }
        // RVA: 0x0AD1C7AC  token: 0x6000C50
        private System.Dynamic.DynamicMetaObject <BindInvokeMember>b__0(System.Dynamic.DynamicMetaObject value) { }

    }

    // TypeToken: 0x2000276  // size: 0x48
    public sealed class <GetDynamicMemberNames>d__6 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.String <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public System.Dynamic.ExpandoObject.MetaExpando <>4__this;  // 0x28
        private System.Dynamic.ExpandoObject.ExpandoData <expandoData>5__2;  // 0x30
        private System.Dynamic.ExpandoClass <klass>5__3;  // 0x38
        private System.Int32 <i>5__4;  // 0x40

        // Properties
        System.String System.Collections.Generic.IEnumerator<System.String>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x6000C51
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000C52
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0AD1C030  token: 0x6000C53
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0AD1C200  token: 0x6000C55
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0AD1C16C  token: 0x6000C57
        private virtual System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator() { }
        // RVA: 0x0AD1C1F8  token: 0x6000C58
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000274  // size: 0x28
    public class MetaExpando : System.Dynamic.DynamicMetaObject
    {
        // Properties
        System.Dynamic.ExpandoObject Value { get; /* RVA: 0x0AD185C8 */ }

        // Methods
        // RVA: 0x0AD1852C  token: 0x6000C43
        public System.Void .ctor(System.Linq.Expressions.Expression expression, System.Dynamic.ExpandoObject value) { }
        // RVA: 0x0AD1788C  token: 0x6000C44
        private System.Dynamic.DynamicMetaObject BindGetOrInvokeMember(System.Dynamic.DynamicMetaObjectBinder binder, System.String name, System.Boolean ignoreCase, System.Dynamic.DynamicMetaObject fallback, System.Func<System.Dynamic.DynamicMetaObject,System.Dynamic.DynamicMetaObject> fallbackInvoke) { }
        // RVA: 0x0AD177B4  token: 0x6000C45
        public virtual System.Dynamic.DynamicMetaObject BindGetMember(System.Dynamic.GetMemberBinder binder) { }
        // RVA: 0x0AD17D8C  token: 0x6000C46
        public virtual System.Dynamic.DynamicMetaObject BindInvokeMember(System.Dynamic.InvokeMemberBinder binder, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD17F14  token: 0x6000C47
        public virtual System.Dynamic.DynamicMetaObject BindSetMember(System.Dynamic.SetMemberBinder binder, System.Dynamic.DynamicMetaObject value) { }
        // RVA: 0x0AD174F0  token: 0x6000C48
        public virtual System.Dynamic.DynamicMetaObject BindDeleteMember(System.Dynamic.DeleteMemberBinder binder) { }
        // RVA: 0x0AD183AC  token: 0x6000C49
        public virtual System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames() { }
        // RVA: 0x0AD17274  token: 0x6000C4A
        private System.Dynamic.DynamicMetaObject AddDynamicTestAndDefer(System.Dynamic.DynamicMetaObjectBinder binder, System.Dynamic.ExpandoClass klass, System.Dynamic.ExpandoClass originalClass, System.Dynamic.DynamicMetaObject succeeds) { }
        // RVA: 0x0AD182DC  token: 0x6000C4B
        private System.Dynamic.ExpandoClass GetClassEnsureIndex(System.String name, System.Boolean caseInsensitive, System.Dynamic.ExpandoObject obj, System.Dynamic.ExpandoClass& klass, System.Int32& index) { }
        // RVA: 0x0AD1841C  token: 0x6000C4C
        private System.Linq.Expressions.Expression GetLimitedSelf() { }
        // RVA: 0x0AD184E0  token: 0x6000C4D
        private System.Dynamic.BindingRestrictions GetRestrictions() { }

    }

    // TypeToken: 0x2000277  // size: 0x28
    public class ExpandoData
    {
        // Fields
        private static System.Dynamic.ExpandoObject.ExpandoData Empty;  // static @ 0x0
        private readonly System.Dynamic.ExpandoClass Class;  // 0x10
        private readonly System.Object[] _dataArray;  // 0x18
        private System.Int32 _version;  // 0x20

        // Properties
        System.Object Item { get; /* RVA: 0x0A7AAD6C */ set; /* RVA: 0x0AD15C90 */ }
        System.Int32 Version { get; /* RVA: 0x02B70F90 */ }
        System.Int32 Length { get; /* RVA: 0x053932FC */ }

        // Methods
        // RVA: 0x0AD15C14  token: 0x6000C5D
        private System.Void .ctor() { }
        // RVA: 0x047E1B40  token: 0x6000C5E
        private System.Void .ctor(System.Dynamic.ExpandoClass klass, System.Object[] data, System.Int32 version) { }
        // RVA: 0x0AD159D8  token: 0x6000C5F
        private System.Dynamic.ExpandoObject.ExpandoData UpdateClass(System.Dynamic.ExpandoClass newClass) { }
        // RVA: 0x04D8D030  token: 0x6000C60
        private static System.Int32 GetAlignedSize(System.Int32 len) { }
        // RVA: 0x0AD15BA4  token: 0x6000C61
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000278  // size: 0x48
    public sealed class <GetExpandoEnumerator>d__51 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Collections.Generic.KeyValuePair<System.String,System.Object> <>2__current;  // 0x18
        public System.Dynamic.ExpandoObject <>4__this;  // 0x28
        public System.Int32 version;  // 0x30
        public System.Dynamic.ExpandoObject.ExpandoData data;  // 0x38
        private System.Int32 <i>5__2;  // 0x40

        // Properties
        System.Collections.Generic.KeyValuePair<System.String,System.Object> System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Current { get; /* RVA: 0x04D890C0 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0AD1C724 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000C62
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000C63
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0AD1C538  token: 0x6000C64
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0AD1C6D8  token: 0x6000C66
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000282
    public sealed class Entry
    {
        // Fields
        private readonly System.Int32 _hash;  // 0x0
        private readonly TKey _key;  // 0x0
        private readonly TValue _value;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C90
        private System.Void .ctor(System.Int32 hash, TKey key, TValue value) { }

    }

    // TypeToken: 0x200028A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Dynamic.Utils.TypeUtils.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0AD1C7E0  token: 0x6000CDE
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000CDF
        public System.Void .ctor() { }
        // RVA: 0x0AD1C76C  token: 0x6000CE0
        private System.Boolean <.cctor>b__44_0(System.Type i) { }
        // RVA: 0x0AD1C78C  token: 0x6000CE1
        private System.Type <.cctor>b__44_1(System.Type i) { }

    }

    // TypeToken: 0x2000295
    public sealed struct ElementCount
    {
        // Fields
        private System.Int32 uniqueCount;  // 0x0
        private System.Int32 unfoundCount;  // 0x0

    }

    // TypeToken: 0x2000296
    public sealed struct Slot
    {
        // Fields
        private System.Int32 hashCode;  // 0x0
        private System.Int32 next;  // 0x0
        private T value;  // 0x0

    }

    // TypeToken: 0x2000297
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Collections.Generic.HashSet<T> _set;  // 0x0
        private System.Int32 _index;  // 0x0
        private System.Int32 _version;  // 0x0
        private T _current;  // 0x0

        // Properties
        T Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000D41
        private System.Void .ctor(System.Collections.Generic.HashSet<T> set) { }
        // RVA: -1  // not resolved  token: 0x6000D42
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000D43
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000D46
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200029D  // size: 0x18
    public sealed struct TimeoutTracker
    {
        // Fields
        private System.Int32 m_total;  // 0x10
        private System.Int32 m_start;  // 0x14

        // Properties
        System.Int32 RemainingMilliseconds { get; /* RVA: 0x0AD192FC */ }
        System.Boolean IsExpired { get; /* RVA: 0x0AD192E4 */ }

        // Methods
        // RVA: 0x03E6C1D0  token: 0x6000D79
        public System.Void .ctor(System.Int32 millisecondsTimeout) { }

    }

    // TypeToken: 0x200029F  // size: 0x88
    public sealed struct __StaticArrayInitTypeSize=120
    {
    }

    // TypeToken: 0x20002A0  // size: 0x110
    public sealed struct __StaticArrayInitTypeSize=256
    {
    }

    // TypeToken: 0x20002A1  // size: 0x410
    public sealed struct __StaticArrayInitTypeSize=1024
    {
    }

    // TypeToken: 0x200029E  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 0270BFF41CB170C33C20788C368CB1B5A66B0FD0B98D638A827B783537583821;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 09FDC69AA887AC8D36E0C8284C7B1D53E580E4880B72A67FF80D7E38317115D9;  // static @ 0x400
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 0F4764044BF2E91EFEFAF3D4F1E81C10A2B7F0F45305489925CFC01198CF45B5;  // static @ 0x800
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 15A0E7628178B3FE6546E9B60F35E2B1C4864E020B16376866F38F72C74B45A9;  // static @ 0xc00
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 4CF8DC711CD3210D673D86981355E2609233025ED22E4DC7A1F9B8E75ADE35F4;  // static @ 0x1000
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 783FB0A44BE7670EB6EC170CF7F16BB2B6F23F4964B87B60C4EB506C7C6A286D;  // static @ 0x1400
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=120 914FCE8DC82DA59038745B264F743222527FBAE2E4A28E71C89760B7E3DBBA67;  // static @ 0x1800
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=256 93631B0726F6FE6629DAA743EE51B49F4477ED07391B68EEEA0672A4A90018AA;  // static @ 0x1878
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=256 C2D8E5EED6CBEBD8625FC18F81486A7733C04F9B0129FFBE974C68B90308B4F2;  // static @ 0x1978
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 C40A9A77EE05E10AF16DA9D1781A2053159EA8570ADA03637BB5FE3D076C9F89;  // static @ 0x1a78
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 E8058399671D13082C35B6B3CE7F9E282507EDD458CCFDD3BAD9F35D90016F62;  // static @ 0x1e78

    }

namespace Microsoft.Win32.SafeHandles
{

    // TypeToken: 0x2000003  // size: 0x20
    public sealed class SafeMemoryMappedFileHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
    {
        // Methods
        // RVA: 0x039B85E0  token: 0x6000006
        public System.Void .ctor(System.IntPtr preexistingHandle, System.Boolean ownsHandle) { }
        // RVA: 0x04D83740  token: 0x6000007
        protected virtual System.Boolean ReleaseHandle() { }

    }

    // TypeToken: 0x2000004  // size: 0x30
    public sealed class SafeMemoryMappedViewHandle : System.Runtime.InteropServices.SafeBuffer
    {
        // Fields
        private System.IntPtr mmap_handle;  // 0x28

        // Methods
        // RVA: 0x0459B1E0  token: 0x6000008
        private System.Void .ctor(System.IntPtr mmap_handle, System.IntPtr base_address, System.Int64 size) { }
        // RVA: 0x0ACCADB8  token: 0x6000009
        private System.Void Flush() { }
        // RVA: 0x04D7AFD0  token: 0x600000A
        protected virtual System.Boolean ReleaseHandle() { }

    }

}

namespace System
{

    // TypeToken: 0x2000005
    public sealed class Action`9 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600000B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600000C
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) { }

    }

    // TypeToken: 0x2000006
    public sealed class Action`10 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600000D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600000E
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10) { }

    }

    // TypeToken: 0x2000007
    public sealed class Action`11 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600000F
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000010
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) { }

    }

    // TypeToken: 0x2000008
    public sealed class Action`12 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000011
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000012
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12) { }

    }

    // TypeToken: 0x2000009
    public sealed class Action`13 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000013
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000014
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13) { }

    }

    // TypeToken: 0x200000A
    public sealed class Action`14 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000015
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000016
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14) { }

    }

    // TypeToken: 0x200000B
    public sealed class Action`15 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000017
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000018
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15) { }

    }

    // TypeToken: 0x200000C
    public sealed class Action`16 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000019
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600001A
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16) { }

    }

    // TypeToken: 0x200000D
    public sealed class Func`10 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600001B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600001C
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) { }

    }

    // TypeToken: 0x200000E
    public sealed class Func`11 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600001D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600001E
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10) { }

    }

    // TypeToken: 0x200000F
    public sealed class Func`12 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600001F
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000020
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) { }

    }

    // TypeToken: 0x2000010
    public sealed class Func`13 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000021
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000022
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12) { }

    }

    // TypeToken: 0x2000011
    public sealed class Func`14 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000023
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000024
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13) { }

    }

    // TypeToken: 0x2000012
    public sealed class Func`15 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000025
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000026
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14) { }

    }

    // TypeToken: 0x2000013
    public sealed class Func`16 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000027
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000028
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15) { }

    }

    // TypeToken: 0x2000014
    public sealed class Func`17 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000029
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600002A
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16) { }

    }

    // TypeToken: 0x2000015  // size: 0x10
    public static class MonoUtil
    {
        // Fields
        public static readonly System.Boolean IsUnix;  // static @ 0x0

        // Methods
        // RVA: 0x04D193A0  token: 0x600002B
        private static System.Void .cctor() { }

    }

}

namespace System.Collections.Generic
{

    // TypeToken: 0x2000292
    public sealed struct ArrayBuilder`1
    {
        // Fields
        private T[] _array;  // 0x0
        private System.Int32 _count;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000D01
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000D02
        public T[] ToArray() { }
        // RVA: -1  // not resolved  token: 0x6000D03
        public System.Void UncheckedAdd(T item) { }

    }

    // TypeToken: 0x2000293  // size: 0x30
    public sealed class BitHelper
    {
        // Fields
        private readonly System.Int32 _length;  // 0x10
        private readonly System.Int32* _arrayPtr;  // 0x18
        private readonly System.Int32[] _array;  // 0x20
        private readonly System.Boolean _useStackAlloc;  // 0x28

        // Methods
        // RVA: 0x04DAA010  token: 0x6000D04
        private System.Void .ctor(System.Int32* bitArrayPtr, System.Int32 length) { }
        // RVA: 0x0AD157D0  token: 0x6000D05
        private System.Void .ctor(System.Int32[] bitArray, System.Int32 length) { }
        // RVA: 0x0AD1575C  token: 0x6000D06
        private System.Void MarkBit(System.Int32 bitPosition) { }
        // RVA: 0x0AD156F4  token: 0x6000D07
        private System.Boolean IsMarked(System.Int32 bitPosition) { }
        // RVA: 0x0AD157B8  token: 0x6000D08
        private static System.Int32 ToIntArrayLength(System.Int32 n) { }

    }

    // TypeToken: 0x2000294
    public class HashSet`1 : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.ISet`1, System.Collections.Generic.IReadOnlyCollection`1, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
    {
        // Fields
        private static System.Int32 Lower31BitMask;  // const
        private static System.Int32 StackAllocThreshold;  // const
        private static System.Int32 ShrinkThreshold;  // const
        private static System.String CapacityName;  // const
        private static System.String ElementsName;  // const
        private static System.String ComparerName;  // const
        private static System.String VersionName;  // const
        private System.Int32[] _buckets;  // 0x0
        private System.Collections.Generic.HashSet.Slot<T>[] _slots;  // 0x0
        private System.Int32 _count;  // 0x0
        private System.Int32 _lastIndex;  // 0x0
        private System.Int32 _freeList;  // 0x0
        private System.Collections.Generic.IEqualityComparer<T> _comparer;  // 0x0
        private System.Int32 _version;  // 0x0
        private System.Runtime.Serialization.SerializationInfo _siInfo;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.Generic.ICollection<T>.IsReadOnly { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.IEqualityComparer<T> Comparer { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000D09
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000D0A
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<T> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000D0B
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000D0C
        public System.Void .ctor(System.Collections.Generic.IEnumerable<T> collection) { }
        // RVA: -1  // not resolved  token: 0x6000D0D
        public System.Void .ctor(System.Collections.Generic.IEnumerable<T> collection, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000D0E
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: -1  // not resolved  token: 0x6000D0F
        private System.Void CopyFrom(System.Collections.Generic.HashSet<T> source) { }
        // RVA: -1  // not resolved  token: 0x6000D10
        public System.Void .ctor(System.Int32 capacity, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000D11
        private virtual System.Void System.Collections.Generic.ICollection<T>.Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000D12
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000D13
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000D14
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000D15
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x6000D18
        public System.Collections.Generic.HashSet.Enumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000D19
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000D1A
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000D1B
        public virtual System.Void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: -1  // not resolved  token: 0x6000D1C
        public virtual System.Void OnDeserialization(System.Object sender) { }
        // RVA: -1  // not resolved  token: 0x6000D1D
        public virtual System.Boolean Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000D1E
        public System.Boolean TryGetValue(T equalValue, T& actualValue) { }
        // RVA: -1  // not resolved  token: 0x6000D1F
        public virtual System.Void UnionWith(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D20
        public virtual System.Void IntersectWith(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D21
        public virtual System.Void ExceptWith(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D22
        public virtual System.Void SymmetricExceptWith(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D23
        public virtual System.Boolean IsSubsetOf(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D24
        public virtual System.Boolean IsProperSubsetOf(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D25
        public virtual System.Boolean IsSupersetOf(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D26
        public virtual System.Boolean IsProperSupersetOf(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D27
        public virtual System.Boolean Overlaps(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D28
        public virtual System.Boolean SetEquals(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D29
        public System.Void CopyTo(T[] array) { }
        // RVA: -1  // not resolved  token: 0x6000D2A
        public System.Void CopyTo(T[] array, System.Int32 arrayIndex, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000D2B
        public System.Int32 RemoveWhere(System.Predicate<T> match) { }
        // RVA: -1  // not resolved  token: 0x6000D2D
        public System.Int32 EnsureCapacity(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000D2E
        public System.Void TrimExcess() { }
        // RVA: -1  // not resolved  token: 0x6000D2F
        public static System.Collections.Generic.IEqualityComparer<System.Collections.Generic.HashSet<T>> CreateSetComparer() { }
        // RVA: -1  // not resolved  token: 0x6000D30
        private System.Int32 Initialize(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000D31
        private System.Void IncreaseCapacity() { }
        // RVA: -1  // not resolved  token: 0x6000D32
        private System.Void SetCapacity(System.Int32 newSize) { }
        // RVA: -1  // not resolved  token: 0x6000D33
        private System.Boolean AddIfNotPresent(T value) { }
        // RVA: -1  // not resolved  token: 0x6000D34
        private System.Void AddValue(System.Int32 index, System.Int32 hashCode, T value) { }
        // RVA: -1  // not resolved  token: 0x6000D35
        private System.Boolean ContainsAllElements(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D36
        private System.Boolean IsSubsetOfHashSetWithSameEC(System.Collections.Generic.HashSet<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D37
        private System.Void IntersectWithHashSetWithSameEC(System.Collections.Generic.HashSet<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D38
        private System.Void IntersectWithEnumerable(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D39
        private System.Int32 InternalIndexOf(T item) { }
        // RVA: -1  // not resolved  token: 0x6000D3A
        private System.Void SymmetricExceptWithUniqueHashSet(System.Collections.Generic.HashSet<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D3B
        private System.Void SymmetricExceptWithEnumerable(System.Collections.Generic.IEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000D3C
        private System.Boolean AddOrGetLocation(T value, System.Int32& location) { }
        // RVA: -1  // not resolved  token: 0x6000D3D
        private System.Collections.Generic.HashSet.ElementCount<T> CheckUniqueAndUnfoundElements(System.Collections.Generic.IEnumerable<T> other, System.Boolean returnIfUnfound) { }
        // RVA: -1  // not resolved  token: 0x6000D3E
        private static System.Boolean HashSetEquals(System.Collections.Generic.HashSet<T> set1, System.Collections.Generic.HashSet<T> set2, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000D3F
        private static System.Boolean AreEqualityComparersEqual(System.Collections.Generic.HashSet<T> set1, System.Collections.Generic.HashSet<T> set2) { }
        // RVA: -1  // not resolved  token: 0x6000D40
        private System.Int32 InternalGetHashCode(T item) { }

    }

    // TypeToken: 0x2000298
    public sealed class HashSetEqualityComparer`1 : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<T> _comparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000D47
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000D48
        public virtual System.Boolean Equals(System.Collections.Generic.HashSet<T> x, System.Collections.Generic.HashSet<T> y) { }
        // RVA: -1  // not resolved  token: 0x6000D49
        public virtual System.Int32 GetHashCode(System.Collections.Generic.HashSet<T> obj) { }
        // RVA: -1  // not resolved  token: 0x6000D4A
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000D4B
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000299
    public sealed class ICollectionDebugView`1
    {
    }

}

namespace System.Dynamic
{

    // TypeToken: 0x200025D  // size: 0x20
    public abstract class BinaryOperationBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Fields
        private readonly System.Linq.Expressions.ExpressionType <Operation>k__BackingField;  // 0x18

        // Properties
        System.Linq.Expressions.ExpressionType Operation { get; /* RVA: 0x011EF5B0 */ }

        // Methods
        // RVA: 0x0ACFD7DC  token: 0x6000BAD
        public System.Dynamic.DynamicMetaObject FallbackBinaryOperation(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject arg) { }
        // RVA: -1  // abstract  token: 0x6000BAE
        public virtual System.Dynamic.DynamicMetaObject FallbackBinaryOperation(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject arg, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: 0x0ACFD64C  token: 0x6000BAF
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }

    }

    // TypeToken: 0x200025E  // size: 0x10
    public abstract class BindingRestrictions
    {
        // Fields
        public static readonly System.Dynamic.BindingRestrictions Empty;  // static @ 0x0

        // Methods
        // RVA: 0x041E1670  token: 0x6000BB0
        private System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x6000BB1
        private virtual System.Linq.Expressions.Expression GetExpression() { }
        // RVA: 0x0ACFDA98  token: 0x6000BB2
        public System.Dynamic.BindingRestrictions Merge(System.Dynamic.BindingRestrictions restrictions) { }
        // RVA: 0x0ACFD970  token: 0x6000BB3
        public static System.Dynamic.BindingRestrictions GetTypeRestriction(System.Linq.Expressions.Expression expression, System.Type type) { }
        // RVA: 0x0ACFD8C8  token: 0x6000BB4
        private static System.Dynamic.BindingRestrictions GetTypeRestriction(System.Dynamic.DynamicMetaObject obj) { }
        // RVA: 0x0ACFD7FC  token: 0x6000BB5
        public static System.Dynamic.BindingRestrictions GetInstanceRestriction(System.Linq.Expressions.Expression expression, System.Object instance) { }
        // RVA: 0x0ACFDBB0  token: 0x6000BB6
        public System.Linq.Expressions.Expression ToExpression() { }
        // RVA: 0x0ACFDBC0  token: 0x6000BB7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000266  // size: 0x20
    public abstract class ConvertBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0ACFFFA0  token: 0x6000BCB
        public System.Dynamic.DynamicMetaObject FallbackConvert(System.Dynamic.DynamicMetaObject target) { }
        // RVA: -1  // abstract  token: 0x6000BCC
        public virtual System.Dynamic.DynamicMetaObject FallbackConvert(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: 0x0ACFFEB0  token: 0x6000BCD
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }

    }

    // TypeToken: 0x2000267  // size: 0x18
    public abstract class CreateInstanceBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Methods
        // RVA: 0x0AD0021C  token: 0x6000BCE
        public System.Dynamic.DynamicMetaObject FallbackCreateInstance(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // abstract  token: 0x6000BCF
        public virtual System.Dynamic.DynamicMetaObject FallbackCreateInstance(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: 0x0AD00130  token: 0x6000BD0
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }

    }

    // TypeToken: 0x2000268  // size: 0x18
    public abstract class DeleteIndexBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Methods
        // RVA: 0x0AD014FC  token: 0x6000BD1
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD0021C  token: 0x6000BD2
        public System.Dynamic.DynamicMetaObject FallbackDeleteIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes) { }
        // RVA: -1  // abstract  token: 0x6000BD3
        public virtual System.Dynamic.DynamicMetaObject FallbackDeleteIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x2000269  // size: 0x28
    public abstract class DeleteMemberBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Fields
        private readonly System.String <Name>k__BackingField;  // 0x18
        private readonly System.Boolean <IgnoreCase>k__BackingField;  // 0x20

        // Properties
        System.String Name { get; /* RVA: 0x0385B100 */ }
        System.Boolean IgnoreCase { get; /* RVA: 0x04D866B0 */ }

        // Methods
        // RVA: 0x0ACFFFA0  token: 0x6000BD6
        public System.Dynamic.DynamicMetaObject FallbackDeleteMember(System.Dynamic.DynamicMetaObject target) { }
        // RVA: -1  // abstract  token: 0x6000BD7
        public virtual System.Dynamic.DynamicMetaObject FallbackDeleteMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: 0x0AD015E8  token: 0x6000BD8
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }

    }

    // TypeToken: 0x200026A  // size: 0x28
    public class DynamicMetaObject
    {
        // Fields
        public static readonly System.Dynamic.DynamicMetaObject[] EmptyMetaObjects;  // static @ 0x0
        private static readonly System.Object s_noValueSentinel;  // static @ 0x8
        private readonly System.Object _value;  // 0x10
        private readonly System.Linq.Expressions.Expression <Expression>k__BackingField;  // 0x18
        private readonly System.Dynamic.BindingRestrictions <Restrictions>k__BackingField;  // 0x20

        // Properties
        System.Linq.Expressions.Expression Expression { get; /* RVA: 0x0385B100 */ }
        System.Dynamic.BindingRestrictions Restrictions { get; /* RVA: 0x04D862C0 */ }
        System.Object Value { get; /* RVA: 0x0AD02D00 */ }
        System.Boolean HasValue { get; /* RVA: 0x0AD02C00 */ }
        System.Type RuntimeType { get; /* RVA: 0x0AD02C8C */ }
        System.Type LimitType { get; /* RVA: 0x0AD02C50 */ }

        // Methods
        // RVA: 0x0AD02ABC  token: 0x6000BD9
        public System.Void .ctor(System.Linq.Expressions.Expression expression, System.Dynamic.BindingRestrictions restrictions) { }
        // RVA: 0x0AD02A8C  token: 0x6000BDA
        public System.Void .ctor(System.Linq.Expressions.Expression expression, System.Dynamic.BindingRestrictions restrictions, System.Object value) { }
        // RVA: 0x0AD02114  token: 0x6000BE1
        public virtual System.Dynamic.DynamicMetaObject BindConvert(System.Dynamic.ConvertBinder binder) { }
        // RVA: 0x0AD0243C  token: 0x6000BE2
        public virtual System.Dynamic.DynamicMetaObject BindGetMember(System.Dynamic.GetMemberBinder binder) { }
        // RVA: 0x0AD026E0  token: 0x6000BE3
        public virtual System.Dynamic.DynamicMetaObject BindSetMember(System.Dynamic.SetMemberBinder binder, System.Dynamic.DynamicMetaObject value) { }
        // RVA: 0x0AD022FC  token: 0x6000BE4
        public virtual System.Dynamic.DynamicMetaObject BindDeleteMember(System.Dynamic.DeleteMemberBinder binder) { }
        // RVA: 0x0AD02394  token: 0x6000BE5
        public virtual System.Dynamic.DynamicMetaObject BindGetIndex(System.Dynamic.GetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes) { }
        // RVA: 0x0AD02624  token: 0x6000BE6
        public virtual System.Dynamic.DynamicMetaObject BindSetIndex(System.Dynamic.SetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value) { }
        // RVA: 0x0AD02254  token: 0x6000BE7
        public virtual System.Dynamic.DynamicMetaObject BindDeleteIndex(System.Dynamic.DeleteIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes) { }
        // RVA: 0x0AD024D4  token: 0x6000BE8
        public virtual System.Dynamic.DynamicMetaObject BindInvokeMember(System.Dynamic.InvokeMemberBinder binder, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD0257C  token: 0x6000BE9
        public virtual System.Dynamic.DynamicMetaObject BindInvoke(System.Dynamic.InvokeBinder binder, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD021AC  token: 0x6000BEA
        public virtual System.Dynamic.DynamicMetaObject BindCreateInstance(System.Dynamic.CreateInstanceBinder binder, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD02788  token: 0x6000BEB
        public virtual System.Dynamic.DynamicMetaObject BindUnaryOperation(System.Dynamic.UnaryOperationBinder binder) { }
        // RVA: 0x0AD0206C  token: 0x6000BEC
        public virtual System.Dynamic.DynamicMetaObject BindBinaryOperation(System.Dynamic.BinaryOperationBinder binder, System.Dynamic.DynamicMetaObject arg) { }
        // RVA: 0x0AD029AC  token: 0x6000BED
        public virtual System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames() { }
        // RVA: 0x0AD02820  token: 0x6000BEE
        public static System.Dynamic.DynamicMetaObject Create(System.Object value, System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0AD029DC  token: 0x6000BEF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200026B  // size: 0x18
    public abstract class DynamicMetaObjectBinder : System.Runtime.CompilerServices.CallSiteBinder
    {
        // Properties
        System.Type ReturnType { get; /* RVA: 0x0AD0201C */ }
        System.Boolean IsStandardBinder { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0AD01FDC  token: 0x6000BF0
        protected System.Void .ctor() { }
        // RVA: 0x0AD01790  token: 0x6000BF2
        public virtual System.Linq.Expressions.Expression Bind(System.Object[] args, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters, System.Linq.Expressions.LabelTarget returnLabel) { }
        // RVA: 0x0AD01E40  token: 0x6000BF3
        private static System.Dynamic.DynamicMetaObject[] CreateArgumentMetaObjects(System.Object[] args, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: -1  // abstract  token: 0x6000BF4
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD01F6C  token: 0x6000BF5
        public System.Linq.Expressions.Expression GetUpdateExpression(System.Type type) { }

    }

    // TypeToken: 0x200026C  // size: 0x28
    public class ExpandoClass
    {
        // Fields
        private readonly System.String[] _keys;  // 0x10
        private readonly System.Int32 _hashCode;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.WeakReference>> _transitions;  // 0x20
        private static readonly System.Dynamic.ExpandoClass Empty;  // static @ 0x0

        // Properties
        System.String[] Keys { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0AD03530  token: 0x6000BF7
        private System.Void .ctor() { }
        // RVA: 0x0AD0351C  token: 0x6000BF8
        private System.Void .ctor(System.String[] keys, System.Int32 hashCode) { }
        // RVA: 0x0AD02DF4  token: 0x6000BF9
        private System.Dynamic.ExpandoClass FindNewClass(System.String newKey) { }
        // RVA: 0x0AD03168  token: 0x6000BFA
        private System.Collections.Generic.List<System.WeakReference> GetTransitionList(System.Int32 hashCode) { }
        // RVA: 0x0AD03480  token: 0x6000BFB
        private System.Int32 GetValueIndex(System.String name, System.Boolean caseInsensitive, System.Dynamic.ExpandoObject obj) { }
        // RVA: 0x0AD03414  token: 0x6000BFC
        private System.Int32 GetValueIndexCaseSensitive(System.String name) { }
        // RVA: 0x0AD03298  token: 0x6000BFD
        private System.Int32 GetValueIndexCaseInsensitive(System.String name, System.Dynamic.ExpandoObject obj) { }
        // RVA: 0x0AD034AC  token: 0x6000BFF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200026D  // size: 0x30
    public sealed class ExpandoObject : System.Dynamic.IDynamicMetaObjectProvider, System.Collections.Generic.IDictionary`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.ComponentModel.INotifyPropertyChanged
    {
        // Fields
        private static readonly System.Reflection.MethodInfo s_expandoTryGetValue;  // static @ 0x0
        private static readonly System.Reflection.MethodInfo s_expandoTrySetValue;  // static @ 0x8
        private static readonly System.Reflection.MethodInfo s_expandoTryDeleteValue;  // static @ 0x10
        private static readonly System.Reflection.MethodInfo s_expandoPromoteClass;  // static @ 0x18
        private static readonly System.Reflection.MethodInfo s_expandoCheckVersion;  // static @ 0x20
        private readonly System.Object LockObject;  // 0x10
        private System.Dynamic.ExpandoObject.ExpandoData _data;  // 0x18
        private System.Int32 _count;  // 0x20
        private static readonly System.Object Uninitialized;  // static @ 0x28
        private System.ComponentModel.PropertyChangedEventHandler _propertyChanged;  // 0x28

        // Properties
        System.Dynamic.ExpandoClass Class { get; /* RVA: 0x0A8DA4D0 */ }
        System.Collections.Generic.ICollection<System.String> System.Collections.Generic.IDictionary<System.String,System.Object>.Keys { get; /* RVA: 0x0AD04124 */ }
        System.Collections.Generic.ICollection<System.Object> System.Collections.Generic.IDictionary<System.String,System.Object>.Values { get; /* RVA: 0x0AD04184 */ }
        System.Object System.Collections.Generic.IDictionary<System.String,System.Object>.Item { get; /* RVA: 0x0AD040D4 */ set; /* RVA: 0x0AD041E4 */ }
        System.Int32 System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Count { get; /* RVA: 0x02B70F90 */ }
        System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.IsReadOnly { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0AD04F24  token: 0x6000C00
        public System.Void .ctor() { }
        // RVA: 0x0AD04828  token: 0x6000C01
        private System.Boolean TryGetValue(System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase, System.Object& value) { }
        // RVA: 0x0AD04950  token: 0x6000C02
        private System.Void TrySetValue(System.Object indexClass, System.Int32 index, System.Object value, System.String name, System.Boolean ignoreCase, System.Boolean add) { }
        // RVA: 0x0AD04510  token: 0x6000C03
        private System.Boolean TryDeleteValue(System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase, System.Object deleteValue) { }
        // RVA: 0x0AD03630  token: 0x6000C04
        private System.Boolean IsDeletedMember(System.Int32 index) { }
        // RVA: 0x0AD036BC  token: 0x6000C06
        private System.Dynamic.ExpandoObject.ExpandoData PromoteClassCore(System.Dynamic.ExpandoClass oldClass, System.Dynamic.ExpandoClass newClass) { }
        // RVA: 0x0AD03704  token: 0x6000C07
        private System.Void PromoteClass(System.Object oldClass, System.Object newClass) { }
        // RVA: 0x0AD043E0  token: 0x6000C08
        private virtual System.Dynamic.DynamicMetaObject System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression parameter) { }
        // RVA: 0x0AD04450  token: 0x6000C09
        private System.Void TryAddMember(System.String key, System.Object value) { }
        // RVA: 0x0AD047FC  token: 0x6000C0A
        private System.Boolean TryGetValueForKey(System.String key, System.Object& value) { }
        // RVA: 0x0AD0357C  token: 0x6000C0B
        private System.Boolean ExpandoContainsKey(System.String key) { }
        // RVA: 0x0AD03EF8  token: 0x6000C10
        private virtual System.Void System.Collections.Generic.IDictionary<System.String,System.Object>.Add(System.String key, System.Object value) { }
        // RVA: 0x0AD03F00  token: 0x6000C11
        private virtual System.Boolean System.Collections.Generic.IDictionary<System.String,System.Object>.ContainsKey(System.String key) { }
        // RVA: 0x0AD03FF4  token: 0x6000C12
        private virtual System.Boolean System.Collections.Generic.IDictionary<System.String,System.Object>.Remove(System.String key) { }
        // RVA: 0x0AD040CC  token: 0x6000C13
        private virtual System.Boolean System.Collections.Generic.IDictionary<System.String,System.Object>.TryGetValue(System.String key, System.Object& value) { }
        // RVA: 0x0AD03810  token: 0x6000C16
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Add(System.Collections.Generic.KeyValuePair<System.String,System.Object> item) { }
        // RVA: 0x0AD03864  token: 0x6000C17
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Clear() { }
        // RVA: 0x0AD03AD8  token: 0x6000C18
        private virtual System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Contains(System.Collections.Generic.KeyValuePair<System.String,System.Object> item) { }
        // RVA: 0x0AD03B70  token: 0x6000C19
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.CopyTo(System.Collections.Generic.KeyValuePair<System.String,System.Object>[] array, System.Int32 arrayIndex) { }
        // RVA: 0x0AD03E90  token: 0x6000C1A
        private virtual System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Remove(System.Collections.Generic.KeyValuePair<System.String,System.Object> item) { }
        // RVA: 0x0AD042A4  token: 0x6000C1B
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.GetEnumerator() { }
        // RVA: 0x0AD042A4  token: 0x6000C1C
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x0AD035AC  token: 0x6000C1D
        private System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>> GetExpandoEnumerator(System.Dynamic.ExpandoObject.ExpandoData data, System.Int32 version) { }
        // RVA: 0x0AD042C8  token: 0x6000C1E
        private virtual System.Void System.ComponentModel.INotifyPropertyChanged.add_PropertyChanged(System.ComponentModel.PropertyChangedEventHandler value) { }
        // RVA: 0x0AD04354  token: 0x6000C1F
        private virtual System.Void System.ComponentModel.INotifyPropertyChanged.remove_PropertyChanged(System.ComponentModel.PropertyChangedEventHandler value) { }
        // RVA: 0x0AD04CBC  token: 0x6000C20
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000279  // size: 0x18
    public abstract class GetIndexBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Methods
        // RVA: 0x0AD1699C  token: 0x6000C68
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD0021C  token: 0x6000C69
        public System.Dynamic.DynamicMetaObject FallbackGetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes) { }
        // RVA: -1  // abstract  token: 0x6000C6A
        public virtual System.Dynamic.DynamicMetaObject FallbackGetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x200027A  // size: 0x28
    public abstract class GetMemberBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Fields
        private readonly System.String <Name>k__BackingField;  // 0x18
        private readonly System.Boolean <IgnoreCase>k__BackingField;  // 0x20

        // Properties
        System.Type ReturnType { get; /* RVA: 0x0AD16C2C */ }
        System.String Name { get; /* RVA: 0x0385B100 */ }
        System.Boolean IgnoreCase { get; /* RVA: 0x04D866B0 */ }
        System.Boolean IsStandardBinder { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0AD16B78  token: 0x6000C6B
        protected System.Void .ctor(System.String name, System.Boolean ignoreCase) { }
        // RVA: 0x0ACFFFA0  token: 0x6000C6F
        public System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target) { }
        // RVA: -1  // abstract  token: 0x6000C70
        public virtual System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: 0x0AD16A88  token: 0x6000C71
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }

    }

    // TypeToken: 0x200027B
    public interface IDynamicMetaObjectProvider
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000C73
        public virtual System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter) { }

    }

    // TypeToken: 0x200027C  // size: 0x18
    public abstract class InvokeBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Methods
        // RVA: 0x0AD0021C  token: 0x6000C74
        public System.Dynamic.DynamicMetaObject FallbackInvoke(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // abstract  token: 0x6000C75
        public virtual System.Dynamic.DynamicMetaObject FallbackInvoke(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: 0x0AD16C7C  token: 0x6000C76
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }

    }

    // TypeToken: 0x200027D  // size: 0x28
    public abstract class InvokeMemberBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Fields
        private readonly System.String <Name>k__BackingField;  // 0x18
        private readonly System.Boolean <IgnoreCase>k__BackingField;  // 0x20

        // Properties
        System.String Name { get; /* RVA: 0x0385B100 */ }
        System.Boolean IgnoreCase { get; /* RVA: 0x04D866B0 */ }

        // Methods
        // RVA: 0x0AD16D68  token: 0x6000C79
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD0021C  token: 0x6000C7A
        public System.Dynamic.DynamicMetaObject FallbackInvokeMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // abstract  token: 0x6000C7B
        public virtual System.Dynamic.DynamicMetaObject FallbackInvokeMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: -1  // abstract  token: 0x6000C7C
        public virtual System.Dynamic.DynamicMetaObject FallbackInvoke(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x200027E  // size: 0x18
    public abstract class SetIndexBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Methods
        // RVA: 0x0AD18E3C  token: 0x6000C7D
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0AD19028  token: 0x6000C7E
        public System.Dynamic.DynamicMetaObject FallbackSetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value) { }
        // RVA: -1  // abstract  token: 0x6000C7F
        public virtual System.Dynamic.DynamicMetaObject FallbackSetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x200027F  // size: 0x28
    public abstract class SetMemberBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Fields
        private readonly System.String <Name>k__BackingField;  // 0x18
        private readonly System.Boolean <IgnoreCase>k__BackingField;  // 0x20

        // Properties
        System.Type ReturnType { get; /* RVA: 0x0AD19294 */ }
        System.String Name { get; /* RVA: 0x0385B100 */ }
        System.Boolean IgnoreCase { get; /* RVA: 0x04D866B0 */ }
        System.Boolean IsStandardBinder { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0AD191E0  token: 0x6000C80
        protected System.Void .ctor(System.String name, System.Boolean ignoreCase) { }
        // RVA: 0x0AD19050  token: 0x6000C84
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: 0x0ACFD7DC  token: 0x6000C86
        public System.Dynamic.DynamicMetaObject FallbackSetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject value) { }
        // RVA: -1  // abstract  token: 0x6000C87
        public virtual System.Dynamic.DynamicMetaObject FallbackSetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject value, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x2000280  // size: 0x18
    public abstract class UnaryOperationBinder : System.Dynamic.DynamicMetaObjectBinder
    {
        // Methods
        // RVA: 0x0ACFFFA0  token: 0x6000C88
        public System.Dynamic.DynamicMetaObject FallbackUnaryOperation(System.Dynamic.DynamicMetaObject target) { }
        // RVA: -1  // abstract  token: 0x6000C89
        public virtual System.Dynamic.DynamicMetaObject FallbackUnaryOperation(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion) { }
        // RVA: 0x0AD1C844  token: 0x6000C8A
        public virtual System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args) { }

    }

}

namespace System.Dynamic.Utils
{

    // TypeToken: 0x2000281
    public sealed class CacheDict`2
    {
        // Fields
        private readonly System.Int32 _mask;  // 0x0
        private readonly System.Dynamic.Utils.CacheDict.Entry<TKey,TValue>[] _entries;  // 0x0

        // Properties
        TKey Item { set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C8B
        private System.Void .ctor(System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x6000C8C
        private static System.Int32 AlignSize(System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x6000C8D
        private System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000C8E
        private System.Void Add(TKey key, TValue value) { }

    }

    // TypeToken: 0x2000283  // size: 0x10
    public static class CollectionExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000C91
        public static System.Runtime.CompilerServices.TrueReadOnlyCollection<T> AddFirst(System.Collections.ObjectModel.ReadOnlyCollection<T> list, T item) { }
        // RVA: -1  // generic def  token: 0x6000C92
        public static T[] AddLast(T[] array, T item) { }
        // RVA: -1  // generic def  token: 0x6000C93
        public static T[] RemoveFirst(T[] array) { }
        // RVA: -1  // generic def  token: 0x6000C94
        public static T[] RemoveLast(T[] array) { }
        // RVA: -1  // generic def  token: 0x6000C95
        public static System.Collections.ObjectModel.ReadOnlyCollection<T> ToReadOnly(System.Collections.Generic.IEnumerable<T> enumerable) { }

    }

    // TypeToken: 0x2000284  // size: 0x10
    public static class ContractUtils
    {
        // Properties
        System.Exception Unreachable { get; /* RVA: 0x0AD15974 */ }

        // Methods
        // RVA: 0x0AD15934  token: 0x6000C97
        public static System.Void Requires(System.Boolean precondition, System.String paramName) { }
        // RVA: 0x0AD158D8  token: 0x6000C98
        public static System.Void RequiresNotNull(System.Object value, System.String paramName) { }
        // RVA: 0x0AD15868  token: 0x6000C99
        public static System.Void RequiresNotNull(System.Object value, System.String paramName, System.Int32 index) { }
        // RVA: -1  // generic def  token: 0x6000C9A
        public static System.Void RequiresNotNullItems(System.Collections.Generic.IList<T> array, System.String arrayName) { }
        // RVA: 0x0AD157F0  token: 0x6000C9B
        private static System.String GetParamName(System.String paramName, System.Int32 index) { }
        // RVA: -1  // generic def  token: 0x6000C9C
        public static System.Void RequiresArrayRange(System.Collections.Generic.IList<T> array, System.Int32 offset, System.Int32 count, System.String offsetName, System.String countName) { }

    }

    // TypeToken: 0x2000285
    public static class EmptyReadOnlyCollection`1
    {
        // Fields
        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<T> Instance;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C9D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000286  // size: 0x10
    public static class ExpressionUtils
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000C9E
        public static System.Collections.ObjectModel.ReadOnlyCollection<T> ReturnReadOnly(System.Collections.Generic.IReadOnlyList<T>& collection) { }
        // RVA: -1  // generic def  token: 0x6000C9F
        public static T ReturnObject(System.Object collectionOrT) { }
        // RVA: 0x0AD16140  token: 0x6000CA0
        public static System.Void ValidateArgumentTypes(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String methodParamName) { }
        // RVA: 0x0AD16068  token: 0x6000CA1
        public static System.Void ValidateArgumentCount(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Int32 count, System.Reflection.ParameterInfo[] pis) { }
        // RVA: 0x0AD163B0  token: 0x6000CA2
        public static System.Linq.Expressions.Expression ValidateOneArgument(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Linq.Expressions.Expression arguments, System.Reflection.ParameterInfo pi, System.String methodParamName, System.String argumentParamName, System.Int32 index) { }
        // RVA: 0x0AD15D4C  token: 0x6000CA3
        public static System.Void RequiresCanRead(System.Linq.Expressions.Expression expression, System.String paramName) { }
        // RVA: 0x0AD15D5C  token: 0x6000CA4
        public static System.Void RequiresCanRead(System.Linq.Expressions.Expression expression, System.String paramName, System.Int32 idx) { }
        // RVA: 0x0AD15F34  token: 0x6000CA5
        public static System.Boolean TryQuote(System.Type parameterType, System.Linq.Expressions.Expression& argument) { }
        // RVA: 0x0AD15CE0  token: 0x6000CA6
        private static System.Reflection.ParameterInfo[] GetParametersForValidation(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind) { }
        // RVA: -1  // generic def  token: 0x6000CA7
        private static System.Boolean SameElements(System.Collections.Generic.IEnumerable<T>& replacement, System.Collections.Generic.IReadOnlyList<T> current) { }
        // RVA: -1  // generic def  token: 0x6000CA8
        private static System.Boolean SameElementsInCollection(System.Collections.Generic.ICollection<T> replacement, System.Collections.Generic.IReadOnlyList<T> current) { }
        // RVA: 0x0AD16020  token: 0x6000CA9
        public static System.Void ValidateArgumentCount(System.Linq.Expressions.LambdaExpression lambda) { }

    }

    // TypeToken: 0x2000287  // size: 0x10
    public static class ExpressionVisitorUtils
    {
        // Methods
        // RVA: 0x0AD16714  token: 0x6000CAA
        public static System.Linq.Expressions.Expression[] VisitBlockExpressions(System.Linq.Expressions.ExpressionVisitor visitor, System.Linq.Expressions.BlockExpression block) { }
        // RVA: 0x0AD16850  token: 0x6000CAB
        public static System.Linq.Expressions.ParameterExpression[] VisitParameters(System.Linq.Expressions.ExpressionVisitor visitor, System.Linq.Expressions.IParameterProvider nodes, System.String callerName) { }
        // RVA: 0x0AD165E8  token: 0x6000CAC
        public static System.Linq.Expressions.Expression[] VisitArguments(System.Linq.Expressions.ExpressionVisitor visitor, System.Linq.Expressions.IArgumentProvider nodes) { }

    }

    // TypeToken: 0x2000288  // size: 0x10
    public static class TypeExtensions
    {
        // Fields
        private static readonly System.Dynamic.Utils.CacheDict<System.Reflection.MethodBase,System.Reflection.ParameterInfo[]> s_paramInfoCache;  // static @ 0x0

        // Methods
        // RVA: 0x0AD19334  token: 0x6000CAD
        public static System.Reflection.MethodInfo GetAnyStaticMethodValidated(System.Type type, System.String name, System.Type[] types) { }
        // RVA: 0x0AD19584  token: 0x6000CAE
        private static System.Boolean MatchesArgumentTypes(System.Reflection.MethodInfo mi, System.Type[] argTypes) { }
        // RVA: 0x0AD194B8  token: 0x6000CAF
        public static System.Type GetReturnType(System.Reflection.MethodBase mi) { }
        // RVA: 0x0AD19538  token: 0x6000CB0
        public static System.TypeCode GetTypeCode(System.Type type) { }
        // RVA: 0x0AD193CC  token: 0x6000CB1
        private static System.Reflection.ParameterInfo[] GetParametersCached(System.Reflection.MethodBase method) { }
        // RVA: 0x0AD19678  token: 0x6000CB2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000289  // size: 0x10
    public static class TypeUtils
    {
        // Fields
        private static readonly System.Type[] s_arrayAssignableInterfaces;  // static @ 0x0

        // Methods
        // RVA: 0x0AD19D64  token: 0x6000CB3
        public static System.Type GetNonNullableType(System.Type type) { }
        // RVA: 0x0AD19E1C  token: 0x6000CB4
        public static System.Type GetNullableType(System.Type type) { }
        // RVA: 0x0AD1B3E0  token: 0x6000CB5
        public static System.Boolean IsNullableType(System.Type type) { }
        // RVA: 0x0AD1B37C  token: 0x6000CB6
        public static System.Boolean IsNullableOrReferenceType(System.Type type) { }
        // RVA: 0x0AD1AA6C  token: 0x6000CB7
        public static System.Boolean IsBool(System.Type type) { }
        // RVA: 0x0AD1B4D8  token: 0x6000CB8
        public static System.Boolean IsNumeric(System.Type type) { }
        // RVA: 0x0AD1B054  token: 0x6000CB9
        public static System.Boolean IsInteger(System.Type type) { }
        // RVA: 0x0AD1A9DC  token: 0x6000CBA
        public static System.Boolean IsArithmetic(System.Type type) { }
        // RVA: 0x0AD1B5DC  token: 0x6000CBB
        public static System.Boolean IsUnsignedInt(System.Type type) { }
        // RVA: 0x0AD1AFBC  token: 0x6000CBC
        public static System.Boolean IsIntegerOrBool(System.Type type) { }
        // RVA: 0x0AD1B474  token: 0x6000CBD
        public static System.Boolean IsNumericOrBool(System.Type type) { }
        // RVA: 0x0AD1B680  token: 0x6000CBE
        public static System.Boolean IsValidInstanceType(System.Reflection.MemberInfo member, System.Type instanceType) { }
        // RVA: 0x0AD1A42C  token: 0x6000CBF
        public static System.Boolean HasIdentityPrimitiveOrNullableConversionTo(System.Type source, System.Type dest) { }
        // RVA: 0x0AD1A778  token: 0x6000CC0
        public static System.Boolean HasReferenceConversionTo(System.Type source, System.Type dest) { }
        // RVA: 0x0AD1B9B0  token: 0x6000CC1
        private static System.Boolean StrictHasReferenceConversionTo(System.Type source, System.Type dest, System.Boolean skipNonArray) { }
        // RVA: 0x0AD1A110  token: 0x6000CC2
        private static System.Boolean HasArrayToInterfaceConversion(System.Type source, System.Type dest) { }
        // RVA: 0x0AD1A5FC  token: 0x6000CC3
        private static System.Boolean HasInterfaceToArrayConversion(System.Type source, System.Type dest) { }
        // RVA: 0x0AD1ABAC  token: 0x6000CC4
        private static System.Boolean IsCovariant(System.Type t) { }
        // RVA: 0x0AD1AAF4  token: 0x6000CC5
        private static System.Boolean IsContravariant(System.Type t) { }
        // RVA: 0x0AD1B0E4  token: 0x6000CC6
        private static System.Boolean IsInvariant(System.Type t) { }
        // RVA: 0x0AD1ABD0  token: 0x6000CC7
        private static System.Boolean IsDelegate(System.Type t) { }
        // RVA: 0x0AD1B10C  token: 0x6000CC8
        public static System.Boolean IsLegalExplicitVariantDelegateConversion(System.Type source, System.Type dest) { }
        // RVA: 0x0AD1AB1C  token: 0x6000CC9
        public static System.Boolean IsConvertible(System.Type type) { }
        // RVA: 0x0AD1A918  token: 0x6000CCA
        public static System.Boolean HasReferenceEquality(System.Type left, System.Type right) { }
        // RVA: 0x0AD1A28C  token: 0x6000CCB
        public static System.Boolean HasBuiltInEqualityOperator(System.Type left, System.Type right) { }
        // RVA: 0x0AD1AEEC  token: 0x6000CCC
        public static System.Boolean IsImplicitlyConvertibleTo(System.Type source, System.Type destination) { }
        // RVA: 0x0AD19F20  token: 0x6000CCD
        public static System.Reflection.MethodInfo GetUserDefinedCoercionMethod(System.Type convertFrom, System.Type convertToType) { }
        // RVA: 0x0AD19804  token: 0x6000CCE
        private static System.Reflection.MethodInfo FindConversionOperator(System.Reflection.MethodInfo[] methods, System.Type typeFrom, System.Type typeTo) { }
        // RVA: 0x0AD1ADC8  token: 0x6000CCF
        private static System.Boolean IsImplicitNumericConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0AD1AECC  token: 0x6000CD0
        private static System.Boolean IsImplicitReferenceConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0AD1AC48  token: 0x6000CD1
        private static System.Boolean IsImplicitBoxingConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0AD1AD44  token: 0x6000CD2
        private static System.Boolean IsImplicitNullableConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0AD19984  token: 0x6000CD3
        public static System.Type FindGenericType(System.Type definition, System.Type type) { }
        // RVA: 0x0AD19BF8  token: 0x6000CD4
        public static System.Reflection.MethodInfo GetBooleanOperator(System.Type type, System.String name) { }
        // RVA: 0x0AD19DDC  token: 0x6000CD5
        public static System.Type GetNonRefType(System.Type type) { }
        // RVA: 0x0AD1970C  token: 0x6000CD6
        public static System.Boolean AreEquivalent(System.Type t1, System.Type t2) { }
        // RVA: 0x0AD19768  token: 0x6000CD7
        public static System.Boolean AreReferenceAssignable(System.Type dest, System.Type src) { }
        // RVA: 0x0AD1B568  token: 0x6000CD8
        public static System.Boolean IsSameOrSubclass(System.Type type, System.Type subType) { }
        // RVA: 0x0AD1BD20  token: 0x6000CD9
        public static System.Void ValidateType(System.Type type, System.String paramName) { }
        // RVA: 0x0AD1BC28  token: 0x6000CDA
        public static System.Void ValidateType(System.Type type, System.String paramName, System.Boolean allowByRef, System.Boolean allowPointer) { }
        // RVA: 0x0AD1BD7C  token: 0x6000CDB
        public static System.Boolean ValidateType(System.Type type, System.String paramName, System.Int32 index) { }
        // RVA: 0x0AD19D14  token: 0x6000CDC
        public static System.Reflection.MethodInfo GetInvokeMethod(System.Type delegateType) { }
        // RVA: 0x0AD1BE60  token: 0x6000CDD
        private static System.Void .cctor() { }

    }

}

namespace System.IO
{

    // TypeToken: 0x200028B  // size: 0x14
    public sealed struct HandleInheritability
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.IO.HandleInheritability None;  // const
        public static System.IO.HandleInheritability Inheritable;  // const

    }

}

namespace System.IO.MemoryMappedFiles
{

    // TypeToken: 0x200028C  // size: 0x14
    public sealed struct MemoryMappedFileAccess
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.IO.MemoryMappedFiles.MemoryMappedFileAccess ReadWrite;  // const
        public static System.IO.MemoryMappedFiles.MemoryMappedFileAccess Read;  // const
        public static System.IO.MemoryMappedFiles.MemoryMappedFileAccess Write;  // const
        public static System.IO.MemoryMappedFiles.MemoryMappedFileAccess CopyOnWrite;  // const
        public static System.IO.MemoryMappedFiles.MemoryMappedFileAccess ReadExecute;  // const
        public static System.IO.MemoryMappedFiles.MemoryMappedFileAccess ReadWriteExecute;  // const

    }

    // TypeToken: 0x200028D  // size: 0x14
    public sealed struct MemoryMappedFileOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.IO.MemoryMappedFiles.MemoryMappedFileOptions None;  // const
        public static System.IO.MemoryMappedFiles.MemoryMappedFileOptions DelayAllocatePages;  // const

    }

    // TypeToken: 0x200028E  // size: 0x38
    public sealed class MemoryMappedViewAccessor : System.IO.UnmanagedMemoryAccessor
    {
        // Fields
        private System.IO.MemoryMappedFiles.MemoryMappedView m_view;  // 0x30

        // Properties
        Microsoft.Win32.SafeHandles.SafeMemoryMappedViewHandle SafeMemoryMappedViewHandle { get; /* RVA: 0x040F2060 */ }

        // Methods
        // RVA: 0x0459B2F0  token: 0x6000CE2
        private System.Void .ctor(System.IO.MemoryMappedFiles.MemoryMappedView view) { }
        // RVA: 0x04CAFA50  token: 0x6000CE4
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AD171B4  token: 0x6000CE5
        public System.Void Flush() { }

    }

    // TypeToken: 0x200028F  // size: 0x10
    public static class MemoryMapImpl
    {
        // Methods
        // RVA: 0x05397BDC  token: 0x6000CE6
        private static System.IntPtr OpenHandleInternal(System.IntPtr handle, System.Char* mapName, System.Int32 mapName_length, System.Int64& capacity, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IO.MemoryMappedFiles.MemoryMappedFileOptions options, System.Int32& error) { }
        // RVA: 0x053982F0  token: 0x6000CE7
        private static System.Void CloseMapping(System.IntPtr handle) { }
        // RVA: 0x0AD171A8  token: 0x6000CE8
        private static System.Void Flush(System.IntPtr file_handle) { }
        // RVA: 0x05397BE4  token: 0x6000CE9
        private static System.Void ConfigureHandleInheritability(System.IntPtr handle, System.IO.HandleInheritability inheritability) { }
        // RVA: 0x053982C4  token: 0x6000CEA
        private static System.Boolean Unmap(System.IntPtr mmap_handle) { }
        // RVA: 0x05397918  token: 0x6000CEB
        private static System.Int32 MapInternal(System.IntPtr handle, System.Int64 offset, System.Int64& size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IntPtr& mmap_handle, System.IntPtr& base_address) { }
        // RVA: 0x0459B1B0  token: 0x6000CEC
        private static System.Void Map(System.IntPtr handle, System.Int64 offset, System.Int64& size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IntPtr& mmap_handle, System.IntPtr& base_address) { }
        // RVA: 0x0AD16E54  token: 0x6000CED
        private static System.Exception CreateException(System.Int32 error, System.String path) { }
        // RVA: 0x04776320  token: 0x6000CEE
        private static System.Int32 StringLength(System.String a) { }
        // RVA: 0x04776360  token: 0x6000CEF
        private static System.Void CheckString(System.String name, System.String value) { }
        // RVA: 0x04776270  token: 0x6000CF0
        private static System.IntPtr OpenHandle(System.IntPtr handle, System.String mapName, System.Int64& capacity, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IO.MemoryMappedFiles.MemoryMappedFileOptions options) { }

    }

    // TypeToken: 0x2000290  // size: 0x28
    public class MemoryMappedFile : System.IDisposable
    {
        // Fields
        private System.IO.FileStream stream;  // 0x10
        private System.Boolean keepOpen;  // 0x18
        private Microsoft.Win32.SafeHandles.SafeMemoryMappedFileHandle handle;  // 0x20

        // Methods
        // RVA: 0x047760C0  token: 0x6000CF1
        public static System.IO.MemoryMappedFiles.MemoryMappedFile CreateFromFile(System.IO.FileStream fileStream, System.String mapName, System.Int64 capacity, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IO.HandleInheritability inheritability, System.Boolean leaveOpen) { }
        // RVA: 0x0459AFD0  token: 0x6000CF2
        public System.IO.MemoryMappedFiles.MemoryMappedViewAccessor CreateViewAccessor(System.Int64 offset, System.Int64 size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access) { }
        // RVA: 0x041E1670  token: 0x6000CF3
        private System.Void .ctor() { }
        // RVA: 0x05396F44  token: 0x6000CF4
        public virtual System.Void Dispose() { }
        // RVA: 0x040F0D90  token: 0x6000CF5
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0459B380  token: 0x6000CF6
        private static System.IO.FileAccess GetFileAccess(System.IO.MemoryMappedFiles.MemoryMappedFileAccess access) { }

    }

    // TypeToken: 0x2000291  // size: 0x30
    public class MemoryMappedView : System.IDisposable
    {
        // Fields
        private Microsoft.Win32.SafeHandles.SafeMemoryMappedViewHandle m_viewHandle;  // 0x10
        private System.Int64 m_pointerOffset;  // 0x18
        private System.Int64 m_size;  // 0x20
        private System.IO.MemoryMappedFiles.MemoryMappedFileAccess m_access;  // 0x28

        // Properties
        Microsoft.Win32.SafeHandles.SafeMemoryMappedViewHandle ViewHandle { get; /* RVA: 0x02B2ECC0 */ }
        System.Int64 PointerOffset { get; /* RVA: 0x0385B100 */ }
        System.Int64 Size { get; /* RVA: 0x04D862C0 */ }
        System.IO.MemoryMappedFiles.MemoryMappedFileAccess Access { get; /* RVA: 0x011F2A90 */ }
        System.Boolean IsClosed { get; /* RVA: 0x04CAFAE0 */ }

        // Methods
        // RVA: 0x0459B180  token: 0x6000CF7
        private System.Void .ctor(Microsoft.Win32.SafeHandles.SafeMemoryMappedViewHandle viewHandle, System.Int64 pointerOffset, System.Int64 size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access) { }
        // RVA: 0x0459B080  token: 0x6000CFC
        private static System.IO.MemoryMappedFiles.MemoryMappedView Create(System.IntPtr handle, System.Int64 offset, System.Int64 size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access) { }
        // RVA: 0x0AD1724C  token: 0x6000CFD
        public System.Void Flush(System.IntPtr capacity) { }
        // RVA: 0x04D14B60  token: 0x6000CFE
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x04D14B00  token: 0x6000CFF
        public virtual System.Void Dispose() { }

    }

}

namespace System.Linq
{

    // TypeToken: 0x200001B  // size: 0x10
    public static class Error
    {
        // Methods
        // RVA: 0x0ACB4C44  token: 0x600005F
        private static System.Exception ArgumentNull(System.String s) { }
        // RVA: 0x0ACB4CA4  token: 0x6000060
        private static System.Exception ArgumentOutOfRange(System.String s) { }
        // RVA: 0x0ACB4D04  token: 0x6000061
        private static System.Exception MoreThanOneElement() { }
        // RVA: 0x0ACB4D68  token: 0x6000062
        private static System.Exception MoreThanOneMatch() { }
        // RVA: 0x0ACB4DCC  token: 0x6000063
        private static System.Exception NoElements() { }
        // RVA: 0x0ACB4E30  token: 0x6000064
        private static System.Exception NoMatch() { }
        // RVA: 0x0ACB4E94  token: 0x6000065
        private static System.Exception NotSupported() { }

    }

    // TypeToken: 0x200001C  // size: 0x10
    public static class Enumerable
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000066
        public static System.Collections.Generic.IEnumerable<TSource> Where(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x6000067
        public static System.Collections.Generic.IEnumerable<TResult> Select(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TResult> selector) { }
        // RVA: -1  // generic def  token: 0x6000068
        private static System.Func<TSource,System.Boolean> CombinePredicates(System.Func<TSource,System.Boolean> predicate1, System.Func<TSource,System.Boolean> predicate2) { }
        // RVA: -1  // generic def  token: 0x6000069
        private static System.Func<TSource,TResult> CombineSelectors(System.Func<TSource,TMiddle> selector1, System.Func<TMiddle,TResult> selector2) { }
        // RVA: -1  // generic def  token: 0x600006A
        public static System.Collections.Generic.IEnumerable<TResult> SelectMany(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> selector) { }
        // RVA: -1  // generic def  token: 0x600006B
        private static System.Collections.Generic.IEnumerable<TResult> SelectManyIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> selector) { }
        // RVA: -1  // generic def  token: 0x600006C
        public static System.Collections.Generic.IEnumerable<TResult> SelectMany(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>> collectionSelector, System.Func<TSource,TCollection,TResult> resultSelector) { }
        // RVA: -1  // generic def  token: 0x600006D
        private static System.Collections.Generic.IEnumerable<TResult> SelectManyIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>> collectionSelector, System.Func<TSource,TCollection,TResult> resultSelector) { }
        // RVA: -1  // generic def  token: 0x600006E
        public static System.Collections.Generic.IEnumerable<TSource> Skip(System.Collections.Generic.IEnumerable<TSource> source, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x600006F
        private static System.Collections.Generic.IEnumerable<TSource> SkipIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000070
        public static System.Collections.Generic.IEnumerable<TResult> Join(System.Collections.Generic.IEnumerable<TOuter> outer, System.Collections.Generic.IEnumerable<TInner> inner, System.Func<TOuter,TKey> outerKeySelector, System.Func<TInner,TKey> innerKeySelector, System.Func<TOuter,TInner,TResult> resultSelector) { }
        // RVA: -1  // generic def  token: 0x6000071
        private static System.Collections.Generic.IEnumerable<TResult> JoinIterator(System.Collections.Generic.IEnumerable<TOuter> outer, System.Collections.Generic.IEnumerable<TInner> inner, System.Func<TOuter,TKey> outerKeySelector, System.Func<TInner,TKey> innerKeySelector, System.Func<TOuter,TInner,TResult> resultSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        // RVA: -1  // generic def  token: 0x6000072
        public static System.Linq.IOrderedEnumerable<TSource> OrderBy(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector) { }
        // RVA: -1  // generic def  token: 0x6000073
        public static System.Linq.IOrderedEnumerable<TSource> OrderByDescending(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector) { }
        // RVA: -1  // generic def  token: 0x6000074
        public static System.Linq.IOrderedEnumerable<TSource> ThenBy(System.Linq.IOrderedEnumerable<TSource> source, System.Func<TSource,TKey> keySelector) { }
        // RVA: -1  // generic def  token: 0x6000075
        public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey,TSource>> GroupBy(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector) { }
        // RVA: -1  // generic def  token: 0x6000076
        public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey,TElement>> GroupBy(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector) { }
        // RVA: -1  // generic def  token: 0x6000077
        public static System.Collections.Generic.IEnumerable<TSource> Concat(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second) { }
        // RVA: -1  // generic def  token: 0x6000078
        private static System.Collections.Generic.IEnumerable<TSource> ConcatIterator(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second) { }
        // RVA: -1  // generic def  token: 0x6000079
        public static System.Collections.Generic.IEnumerable<TSource> Prepend(System.Collections.Generic.IEnumerable<TSource> source, TSource element) { }
        // RVA: -1  // generic def  token: 0x600007A
        private static System.Collections.Generic.IEnumerable<TSource> PrependIterator(System.Collections.Generic.IEnumerable<TSource> source, TSource element) { }
        // RVA: -1  // generic def  token: 0x600007B
        public static System.Collections.Generic.IEnumerable<TSource> Distinct(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x600007C
        public static System.Collections.Generic.IEnumerable<TSource> Distinct(System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEqualityComparer<TSource> comparer) { }
        // RVA: -1  // generic def  token: 0x600007D
        private static System.Collections.Generic.IEnumerable<TSource> DistinctIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEqualityComparer<TSource> comparer) { }
        // RVA: -1  // generic def  token: 0x600007E
        public static System.Collections.Generic.IEnumerable<TSource> Union(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second) { }
        // RVA: -1  // generic def  token: 0x600007F
        private static System.Collections.Generic.IEnumerable<TSource> UnionIterator(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second, System.Collections.Generic.IEqualityComparer<TSource> comparer) { }
        // RVA: -1  // generic def  token: 0x6000080
        public static System.Collections.Generic.IEnumerable<TSource> Intersect(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second) { }
        // RVA: -1  // generic def  token: 0x6000081
        private static System.Collections.Generic.IEnumerable<TSource> IntersectIterator(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second, System.Collections.Generic.IEqualityComparer<TSource> comparer) { }
        // RVA: -1  // generic def  token: 0x6000082
        public static System.Collections.Generic.IEnumerable<TSource> Reverse(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000083
        private static System.Collections.Generic.IEnumerable<TSource> ReverseIterator(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000084
        public static System.Boolean SequenceEqual(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second) { }
        // RVA: -1  // generic def  token: 0x6000085
        public static System.Boolean SequenceEqual(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second, System.Collections.Generic.IEqualityComparer<TSource> comparer) { }
        // RVA: -1  // generic def  token: 0x6000086
        public static System.Collections.Generic.IEnumerable<TSource> AsEnumerable(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000087
        public static TSource[] ToArray(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000088
        public static System.Collections.Generic.List<TSource> ToList(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000089
        public static System.Collections.Generic.Dictionary<TKey,TSource> ToDictionary(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector) { }
        // RVA: -1  // generic def  token: 0x600008A
        public static System.Collections.Generic.Dictionary<TKey,TElement> ToDictionary(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector) { }
        // RVA: -1  // generic def  token: 0x600008B
        public static System.Collections.Generic.Dictionary<TKey,TElement> ToDictionary(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        // RVA: -1  // generic def  token: 0x600008C
        public static System.Collections.Generic.IEnumerable<TResult> OfType(System.Collections.IEnumerable source) { }
        // RVA: -1  // generic def  token: 0x600008D
        private static System.Collections.Generic.IEnumerable<TResult> OfTypeIterator(System.Collections.IEnumerable source) { }
        // RVA: -1  // generic def  token: 0x600008E
        public static System.Collections.Generic.IEnumerable<TResult> Cast(System.Collections.IEnumerable source) { }
        // RVA: -1  // generic def  token: 0x600008F
        private static System.Collections.Generic.IEnumerable<TResult> CastIterator(System.Collections.IEnumerable source) { }
        // RVA: -1  // generic def  token: 0x6000090
        public static TSource First(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000091
        public static TSource First(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x6000092
        public static TSource FirstOrDefault(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000093
        public static TSource FirstOrDefault(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x6000094
        public static TSource Last(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000095
        public static TSource LastOrDefault(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000096
        public static TSource LastOrDefault(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x6000097
        public static TSource Single(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x6000098
        public static TSource Single(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x6000099
        public static TSource SingleOrDefault(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x600009A
        public static TSource SingleOrDefault(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x600009B
        public static TSource ElementAt(System.Collections.Generic.IEnumerable<TSource> source, System.Int32 index) { }
        // RVA: 0x02EDAEE0  token: 0x600009C
        public static System.Collections.Generic.IEnumerable<System.Int32> Range(System.Int32 start, System.Int32 count) { }
        // RVA: 0x02EDADA0  token: 0x600009D
        private static System.Collections.Generic.IEnumerable<System.Int32> RangeIterator(System.Int32 start, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x600009E
        public static System.Collections.Generic.IEnumerable<TResult> Repeat(TResult element, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x600009F
        private static System.Collections.Generic.IEnumerable<TResult> RepeatIterator(TResult element, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x60000A0
        public static System.Collections.Generic.IEnumerable<TResult> Empty() { }
        // RVA: -1  // generic def  token: 0x60000A1
        public static System.Boolean Any(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x60000A2
        public static System.Boolean Any(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x60000A3
        public static System.Boolean All(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x60000A4
        public static System.Int32 Count(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x60000A5
        public static System.Int32 Count(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x60000A6
        public static System.Boolean Contains(System.Collections.Generic.IEnumerable<TSource> source, TSource value) { }
        // RVA: -1  // generic def  token: 0x60000A7
        public static System.Boolean Contains(System.Collections.Generic.IEnumerable<TSource> source, TSource value, System.Collections.Generic.IEqualityComparer<TSource> comparer) { }
        // RVA: -1  // generic def  token: 0x60000A8
        public static TSource Aggregate(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TSource,TSource> func) { }
        // RVA: -1  // generic def  token: 0x60000A9
        public static TAccumulate Aggregate(System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, System.Func<TAccumulate,TSource,TAccumulate> func) { }
        // RVA: 0x0ACB4A9C  token: 0x60000AA
        public static System.Int32 Min(System.Collections.Generic.IEnumerable<System.Int32> source) { }
        // RVA: 0x0ACB48F4  token: 0x60000AB
        public static System.Int32 Max(System.Collections.Generic.IEnumerable<System.Int32> source) { }
        // RVA: -1  // generic def  token: 0x60000AC
        public static System.Collections.Generic.HashSet<TSource> ToHashSet(System.Collections.Generic.IEnumerable<TSource> source) { }
        // RVA: -1  // generic def  token: 0x60000AD
        public static System.Collections.Generic.HashSet<TSource> ToHashSet(System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEqualityComparer<TSource> comparer) { }

    }

    // TypeToken: 0x2000034
    public class EmptyEnumerable`1
    {
        // Fields
        public static readonly TElement[] Instance;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600015C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000035
    public class IdentityFunction`1
    {
        // Properties
        System.Func<TElement,TElement> Instance { get; /* RVA: -1  // not resolved */ }

    }

    // TypeToken: 0x2000037
    public interface IOrderedEnumerable`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000161
        public virtual System.Linq.IOrderedEnumerable<TElement> CreateOrderedEnumerable(System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending) { }

    }

    // TypeToken: 0x2000038
    public interface IGrouping`2 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Properties
        TKey Key { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000039
    public interface ILookup`2 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
    }

    // TypeToken: 0x200003A
    public class Lookup`2 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Linq.ILookup`2
    {
        // Fields
        private System.Collections.Generic.IEqualityComparer<TKey> comparer;  // 0x0
        private System.Linq.Lookup.Grouping<TKey,TElement>[] groupings;  // 0x0
        private System.Linq.Lookup.Grouping<TKey,TElement> lastGrouping;  // 0x0
        private System.Int32 count;  // 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000163
        private static System.Linq.Lookup<TKey,TElement> Create(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000164
        private static System.Linq.Lookup<TKey,TElement> CreateForJoin(System.Collections.Generic.IEnumerable<TElement> source, System.Func<TElement,TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000165
        private System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000166
        public virtual System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000167
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000168
        private System.Int32 InternalGetHashCode(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000169
        private System.Linq.Lookup.Grouping<TKey,TElement> GetGrouping(TKey key, System.Boolean create) { }
        // RVA: -1  // not resolved  token: 0x600016A
        private System.Void Resize() { }

    }

    // TypeToken: 0x200003E
    public class Set`1
    {
        // Fields
        private System.Int32[] buckets;  // 0x0
        private System.Linq.Set.Slot<TElement>[] slots;  // 0x0
        private System.Int32 count;  // 0x0
        private System.Int32 freeList;  // 0x0
        private System.Collections.Generic.IEqualityComparer<TElement> comparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000188
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TElement> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000189
        public System.Boolean Add(TElement value) { }
        // RVA: -1  // not resolved  token: 0x600018A
        public System.Boolean Remove(TElement value) { }
        // RVA: -1  // not resolved  token: 0x600018B
        private System.Boolean Find(TElement value, System.Boolean add) { }
        // RVA: -1  // not resolved  token: 0x600018C
        private System.Void Resize() { }
        // RVA: -1  // not resolved  token: 0x600018D
        private System.Int32 InternalGetHashCode(TElement value) { }

    }

    // TypeToken: 0x2000040
    public class GroupedEnumerable`3 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Collections.Generic.IEnumerable<TSource> source;  // 0x0
        private System.Func<TSource,TKey> keySelector;  // 0x0
        private System.Func<TSource,TElement> elementSelector;  // 0x0
        private System.Collections.Generic.IEqualityComparer<TKey> comparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600018E
        public System.Void .ctor(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        // RVA: -1  // not resolved  token: 0x600018F
        public virtual System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000190
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000041
    public abstract class OrderedEnumerable`1 : System.Linq.IOrderedEnumerable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Collections.Generic.IEnumerable<TElement> source;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000191
        public virtual System.Collections.Generic.IEnumerator<TElement> GetEnumerator() { }
        // RVA: -1  // abstract  token: 0x6000192
        private virtual System.Linq.EnumerableSorter<TElement> GetEnumerableSorter(System.Linq.EnumerableSorter<TElement> next) { }
        // RVA: -1  // not resolved  token: 0x6000193
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // generic def  token: 0x6000194
        private virtual System.Linq.IOrderedEnumerable<TElement> System.Linq.IOrderedEnumerable<TElement>.CreateOrderedEnumerable(System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending) { }
        // RVA: -1  // not resolved  token: 0x6000195
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000043
    public class OrderedEnumerable`2 : System.Linq.OrderedEnumerable`1
    {
        // Fields
        private System.Linq.OrderedEnumerable<TElement> parent;  // 0x0
        private System.Func<TElement,TKey> keySelector;  // 0x0
        private System.Collections.Generic.IComparer<TKey> comparer;  // 0x0
        private System.Boolean descending;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600019C
        private System.Void .ctor(System.Collections.Generic.IEnumerable<TElement> source, System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending) { }
        // RVA: -1  // not resolved  token: 0x600019D
        private virtual System.Linq.EnumerableSorter<TElement> GetEnumerableSorter(System.Linq.EnumerableSorter<TElement> next) { }

    }

    // TypeToken: 0x2000044
    public abstract class EnumerableSorter`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600019E
        private virtual System.Void ComputeKeys(TElement[] elements, System.Int32 count) { }
        // RVA: -1  // abstract  token: 0x600019F
        private virtual System.Int32 CompareKeys(System.Int32 index1, System.Int32 index2) { }
        // RVA: -1  // not resolved  token: 0x60001A0
        private System.Int32[] Sort(TElement[] elements, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60001A1
        private System.Void QuickSort(System.Int32[] map, System.Int32 left, System.Int32 right) { }
        // RVA: -1  // not resolved  token: 0x60001A2
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000045
    public class EnumerableSorter`2 : System.Linq.EnumerableSorter`1
    {
        // Fields
        private System.Func<TElement,TKey> keySelector;  // 0x0
        private System.Collections.Generic.IComparer<TKey> comparer;  // 0x0
        private System.Boolean descending;  // 0x0
        private System.Linq.EnumerableSorter<TElement> next;  // 0x0
        private TKey[] keys;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001A3
        private System.Void .ctor(System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending, System.Linq.EnumerableSorter<TElement> next) { }
        // RVA: -1  // not resolved  token: 0x60001A4
        private virtual System.Void ComputeKeys(TElement[] elements, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60001A5
        private virtual System.Int32 CompareKeys(System.Int32 index1, System.Int32 index2) { }

    }

    // TypeToken: 0x2000046
    public sealed struct Buffer`1
    {
        // Fields
        private TElement[] items;  // 0x0
        private System.Int32 count;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001A6
        private System.Void .ctor(System.Collections.Generic.IEnumerable<TElement> source) { }
        // RVA: -1  // not resolved  token: 0x60001A7
        private TElement[] ToArray() { }

    }

}

namespace System.Linq.Expressions
{

    // TypeToken: 0x2000047  // size: 0x10
    public static class CachedReflectionInfo
    {
        // Fields
        private static System.Reflection.MethodInfo s_CallSiteOps_SetNotMatched;  // static @ 0x0
        private static System.Reflection.MethodInfo s_CallSiteOps_CreateMatchmaker;  // static @ 0x8
        private static System.Reflection.MethodInfo s_CallSiteOps_GetMatch;  // static @ 0x10
        private static System.Reflection.MethodInfo s_CallSiteOps_ClearMatch;  // static @ 0x18
        private static System.Reflection.MethodInfo s_CallSiteOps_UpdateRules;  // static @ 0x20
        private static System.Reflection.MethodInfo s_CallSiteOps_GetRules;  // static @ 0x28
        private static System.Reflection.MethodInfo s_CallSiteOps_GetRuleCache;  // static @ 0x30
        private static System.Reflection.MethodInfo s_CallSiteOps_GetCachedRules;  // static @ 0x38
        private static System.Reflection.MethodInfo s_CallSiteOps_AddRule;  // static @ 0x40
        private static System.Reflection.MethodInfo s_CallSiteOps_MoveRule;  // static @ 0x48
        private static System.Reflection.MethodInfo s_CallSiteOps_Bind;  // static @ 0x50
        private static System.Reflection.MethodInfo s_String_op_Equality_String_String;  // static @ 0x58
        private static System.Reflection.MethodInfo s_Math_Pow_Double_Double;  // static @ 0x60

        // Properties
        System.Reflection.MethodInfo CallSiteOps_SetNotMatched { get; /* RVA: 0x0ACB4430 */ }
        System.Reflection.MethodInfo CallSiteOps_CreateMatchmaker { get; /* RVA: 0x0ACB3F68 */ }
        System.Reflection.MethodInfo CallSiteOps_GetMatch { get; /* RVA: 0x0ACB4100 */ }
        System.Reflection.MethodInfo CallSiteOps_ClearMatch { get; /* RVA: 0x0ACB3E9C */ }
        System.Reflection.MethodInfo CallSiteOps_UpdateRules { get; /* RVA: 0x0ACB44F8 */ }
        System.Reflection.MethodInfo CallSiteOps_GetRules { get; /* RVA: 0x0ACB4298 */ }
        System.Reflection.MethodInfo CallSiteOps_GetRuleCache { get; /* RVA: 0x0ACB41CC */ }
        System.Reflection.MethodInfo CallSiteOps_GetCachedRules { get; /* RVA: 0x0ACB4034 */ }
        System.Reflection.MethodInfo CallSiteOps_AddRule { get; /* RVA: 0x0ACB3D04 */ }
        System.Reflection.MethodInfo CallSiteOps_MoveRule { get; /* RVA: 0x0ACB4364 */ }
        System.Reflection.MethodInfo CallSiteOps_Bind { get; /* RVA: 0x0ACB3DD0 */ }
        System.Reflection.MethodInfo String_op_Equality_String_String { get; /* RVA: 0x0ACB4738 */ }
        System.Reflection.MethodInfo Math_Pow_Double_Double { get; /* RVA: 0x0ACB45C4 */ }

    }

    // TypeToken: 0x2000048  // size: 0x20
    public class BinaryExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.Expression <Right>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <Left>k__BackingField;  // 0x18

        // Properties
        System.Boolean CanReduce { get; /* RVA: 0x0ACB394C */ }
        System.Linq.Expressions.Expression Right { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.Expression Left { get; /* RVA: 0x0385B100 */ }
        System.Reflection.MethodInfo Method { get; /* RVA: 0x0ACB3CF4 */ }
        System.Linq.Expressions.LambdaExpression Conversion { get; /* RVA: 0x0ACB396C */ }
        System.Boolean IsLifted { get; /* RVA: 0x0ACB3B08 */ }
        System.Boolean IsLiftedToNull { get; /* RVA: 0x0ACB3AA4 */ }
        System.Boolean IsLiftedLogical { get; /* RVA: 0x0ACB3974 */ }
        System.Boolean IsReferenceComparison { get; /* RVA: 0x0ACB3C3C */ }

        // Methods
        // RVA: 0x0ACB38E0  token: 0x60001B5
        private System.Void .ctor(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACB2988  token: 0x60001B7
        private static System.Boolean IsOpAssignment(System.Linq.Expressions.ExpressionType op) { }
        // RVA: 0x011EC580  token: 0x60001BB
        private virtual System.Reflection.MethodInfo GetMethod() { }
        // RVA: 0x0ACB37C8  token: 0x60001BC
        public System.Linq.Expressions.BinaryExpression Update(System.Linq.Expressions.Expression left, System.Linq.Expressions.LambdaExpression conversion, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACB3758  token: 0x60001BD
        public virtual System.Linq.Expressions.Expression Reduce() { }
        // RVA: 0x0ACB28A8  token: 0x60001BE
        private static System.Linq.Expressions.ExpressionType GetBinaryOpFromAssignmentOp(System.Linq.Expressions.ExpressionType op) { }
        // RVA: 0x0ACB3664  token: 0x60001BF
        private System.Linq.Expressions.Expression ReduceVariable() { }
        // RVA: 0x0ACB2DA4  token: 0x60001C0
        private System.Linq.Expressions.Expression ReduceMember() { }
        // RVA: 0x0ACB2994  token: 0x60001C1
        private System.Linq.Expressions.Expression ReduceIndex() { }
        // RVA: 0x011EC580  token: 0x60001C3
        private virtual System.Linq.Expressions.LambdaExpression GetConversion() { }
        // RVA: 0x0ACB2860  token: 0x60001C6
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACB311C  token: 0x60001C9
        private System.Linq.Expressions.Expression ReduceUserdefinedLifted() { }

    }

    // TypeToken: 0x2000049  // size: 0x28
    public sealed class LogicalBinaryExpression : System.Linq.Expressions.BinaryExpression
    {
        // Fields
        private readonly System.Linq.Expressions.ExpressionType <NodeType>k__BackingField;  // 0x20

        // Properties
        System.Type Type { get; /* RVA: 0x0ACCA86C */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x02B70F90 */ }

        // Methods
        // RVA: 0x0ACCA838  token: 0x60001CA
        private System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }

    }

    // TypeToken: 0x200004A  // size: 0x20
    public class AssignBinaryExpression : System.Linq.Expressions.BinaryExpression
    {
        // Properties
        System.Type Type { get; /* RVA: 0x0ACB283C */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA9EE0 */ }

        // Methods
        // RVA: 0x0ACB2834  token: 0x60001CD
        private System.Void .ctor(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }

    }

    // TypeToken: 0x200004B  // size: 0x28
    public sealed class CoalesceConversionBinaryExpression : System.Linq.Expressions.BinaryExpression
    {
        // Fields
        private readonly System.Linq.Expressions.LambdaExpression _conversion;  // 0x20

        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04D9BE00 */ }
        System.Type Type { get; /* RVA: 0x0ACB48D0 */ }

        // Methods
        // RVA: 0x0ACB48A0  token: 0x60001D0
        private System.Void .ctor(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x04D862C0  token: 0x60001D1
        private virtual System.Linq.Expressions.LambdaExpression GetConversion() { }

    }

    // TypeToken: 0x200004C  // size: 0x40
    public sealed class OpAssignMethodConversionBinaryExpression : System.Linq.Expressions.MethodBinaryExpression
    {
        // Fields
        private readonly System.Linq.Expressions.LambdaExpression _conversion;  // 0x38

        // Methods
        // RVA: 0x0ACCA8F4  token: 0x60001D4
        private System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Type type, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x04D85A50  token: 0x60001D5
        private virtual System.Linq.Expressions.LambdaExpression GetConversion() { }

    }

    // TypeToken: 0x200004D  // size: 0x30
    public class SimpleBinaryExpression : System.Linq.Expressions.BinaryExpression
    {
        // Fields
        private readonly System.Linq.Expressions.ExpressionType <NodeType>k__BackingField;  // 0x20
        private readonly System.Type <Type>k__BackingField;  // 0x28

        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x02B70F90 */ }
        System.Type Type { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0ACCADC8  token: 0x60001D6
        private System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Type type) { }

    }

    // TypeToken: 0x200004E  // size: 0x38
    public class MethodBinaryExpression : System.Linq.Expressions.SimpleBinaryExpression
    {
        // Fields
        private readonly System.Reflection.MethodInfo _method;  // 0x30

        // Methods
        // RVA: 0x0ACCA8BC  token: 0x60001D9
        private System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Type type, System.Reflection.MethodInfo method) { }
        // RVA: 0x011F36E0  token: 0x60001DA
        private virtual System.Reflection.MethodInfo GetMethod() { }

    }

    // TypeToken: 0x200004F  // size: 0x10
    public abstract class Expression
    {
        // Fields
        private static readonly System.Dynamic.Utils.CacheDict<System.Type,System.Reflection.MethodInfo> s_lambdaDelegateCache;  // static @ 0x0
        private static System.Dynamic.Utils.CacheDict<System.Type,System.Func<System.Linq.Expressions.Expression,System.String,System.Boolean,System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression>,System.Linq.Expressions.LambdaExpression>> s_lambdaFactories;  // static @ 0x8
        private static System.Runtime.CompilerServices.ConditionalWeakTable<System.Linq.Expressions.Expression,System.Linq.Expressions.Expression.ExtensionInfo> s_legacyCtorSupportTable;  // static @ 0x10

        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x0ACCA688 */ }
        System.Type Type { get; /* RVA: 0x0ACCA760 */ }
        System.Boolean CanReduce { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0ACB6844  token: 0x60001DB
        public static System.Linq.Expressions.BinaryExpression Assign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACBDF8C  token: 0x60001DC
        private static System.Linq.Expressions.BinaryExpression GetUserDefinedBinaryOperator(System.Linq.Expressions.ExpressionType binaryType, System.String name, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull) { }
        // RVA: 0x0ACBC868  token: 0x60001DD
        private static System.Linq.Expressions.BinaryExpression GetMethodBasedBinaryOperator(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Boolean liftToNull) { }
        // RVA: 0x0ACBC61C  token: 0x60001DE
        private static System.Linq.Expressions.BinaryExpression GetMethodBasedAssignOperator(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion, System.Boolean liftToNull) { }
        // RVA: 0x0ACBDD98  token: 0x60001DF
        private static System.Linq.Expressions.BinaryExpression GetUserDefinedBinaryOperatorOrThrow(System.Linq.Expressions.ExpressionType binaryType, System.String name, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull) { }
        // RVA: 0x0ACBDB4C  token: 0x60001E0
        private static System.Linq.Expressions.BinaryExpression GetUserDefinedAssignOperatorOrThrow(System.Linq.Expressions.ExpressionType binaryType, System.String name, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.LambdaExpression conversion, System.Boolean liftToNull) { }
        // RVA: 0x0ACBE2E4  token: 0x60001E1
        private static System.Reflection.MethodInfo GetUserDefinedBinaryOperator(System.Linq.Expressions.ExpressionType binaryType, System.Type leftType, System.Type rightType, System.String name) { }
        // RVA: 0x0ACC0120  token: 0x60001E2
        private static System.Boolean IsLiftingConditionalLogicalOperator(System.Type left, System.Type right, System.Reflection.MethodInfo method, System.Linq.Expressions.ExpressionType binaryType) { }
        // RVA: 0x0ACC4C0C  token: 0x60001E3
        private static System.Boolean ParameterIsAssignable(System.Reflection.ParameterInfo pi, System.Type argType) { }
        // RVA: 0x0ACC9330  token: 0x60001E4
        private static System.Void ValidateParamswithOperandsOrThrow(System.Type paramType, System.Type operandType, System.Linq.Expressions.ExpressionType exprType, System.String name) { }
        // RVA: 0x0ACC91F8  token: 0x60001E5
        private static System.Void ValidateOperator(System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC8EF8  token: 0x60001E6
        private static System.Void ValidateMethodInfo(System.Reflection.MethodInfo method, System.String paramName) { }
        // RVA: 0x0ACC01C0  token: 0x60001E7
        private static System.Boolean IsNullComparison(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACC028C  token: 0x60001E8
        private static System.Boolean IsNullConstant(System.Linq.Expressions.Expression e) { }
        // RVA: 0x0ACC9A18  token: 0x60001E9
        private static System.Void ValidateUserDefinedConditionalLogicOperator(System.Linq.Expressions.ExpressionType nodeType, System.Type left, System.Type right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACCA3CC  token: 0x60001EA
        private static System.Void VerifyOpTrueFalse(System.Linq.Expressions.ExpressionType nodeType, System.Type left, System.Reflection.MethodInfo opTrue, System.String paramName) { }
        // RVA: 0x0ACC04CC  token: 0x60001EB
        private static System.Boolean IsValidLiftedConditionalLogicalOperator(System.Type left, System.Type right, System.Reflection.ParameterInfo[] pms) { }
        // RVA: 0x0ACC1A00  token: 0x60001EC
        public static System.Linq.Expressions.BinaryExpression MakeBinary(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC10FC  token: 0x60001ED
        public static System.Linq.Expressions.BinaryExpression MakeBinary(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACBB408  token: 0x60001EE
        public static System.Linq.Expressions.BinaryExpression Equal(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACBB464  token: 0x60001EF
        public static System.Linq.Expressions.BinaryExpression Equal(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC5C28  token: 0x60001F0
        public static System.Linq.Expressions.BinaryExpression ReferenceEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACC3F38  token: 0x60001F1
        public static System.Linq.Expressions.BinaryExpression NotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACC3F94  token: 0x60001F2
        public static System.Linq.Expressions.BinaryExpression NotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC5D88  token: 0x60001F3
        public static System.Linq.Expressions.BinaryExpression ReferenceNotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACBC0EC  token: 0x60001F4
        private static System.Linq.Expressions.BinaryExpression GetEqualityComparisonOperator(System.Linq.Expressions.ExpressionType binaryType, System.String opName, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull) { }
        // RVA: 0x0ACBEBB4  token: 0x60001F5
        public static System.Linq.Expressions.BinaryExpression GreaterThan(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC0F00  token: 0x60001F6
        public static System.Linq.Expressions.BinaryExpression LessThan(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBEAD8  token: 0x60001F7
        public static System.Linq.Expressions.BinaryExpression GreaterThanOrEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC0E24  token: 0x60001F8
        public static System.Linq.Expressions.BinaryExpression LessThanOrEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBBF00  token: 0x60001F9
        private static System.Linq.Expressions.BinaryExpression GetComparisonOperator(System.Linq.Expressions.ExpressionType binaryType, System.String opName, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull) { }
        // RVA: 0x0ACB5BB8  token: 0x60001FA
        public static System.Linq.Expressions.BinaryExpression AndAlso(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACB5750  token: 0x60001FB
        public static System.Linq.Expressions.BinaryExpression AndAlso(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC45C8  token: 0x60001FC
        public static System.Linq.Expressions.BinaryExpression OrElse(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACB99A0  token: 0x60001FD
        public static System.Linq.Expressions.BinaryExpression Coalesce(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC7EAC  token: 0x60001FE
        private static System.Type ValidateCoalesceArgTypes(System.Type left, System.Type right) { }
        // RVA: 0x0ACB5580  token: 0x60001FF
        public static System.Linq.Expressions.BinaryExpression Add(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACB5168  token: 0x6000200
        public static System.Linq.Expressions.BinaryExpression AddAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC8FA4  token: 0x6000201
        private static System.Void ValidateOpAssignConversionLambda(System.Linq.Expressions.LambdaExpression conversion, System.Linq.Expressions.Expression left, System.Reflection.MethodInfo method, System.Linq.Expressions.ExpressionType nodeType) { }
        // RVA: 0x0ACB4F2C  token: 0x6000202
        public static System.Linq.Expressions.BinaryExpression AddAssignChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACB53A4  token: 0x6000203
        public static System.Linq.Expressions.BinaryExpression AddChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC6CE4  token: 0x6000204
        public static System.Linq.Expressions.BinaryExpression Subtract(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC68CC  token: 0x6000205
        public static System.Linq.Expressions.BinaryExpression SubtractAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC6690  token: 0x6000206
        public static System.Linq.Expressions.BinaryExpression SubtractAssignChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC6B08  token: 0x6000207
        public static System.Linq.Expressions.BinaryExpression SubtractChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBB198  token: 0x6000208
        public static System.Linq.Expressions.BinaryExpression Divide(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBAF5C  token: 0x6000209
        public static System.Linq.Expressions.BinaryExpression DivideAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC2B10  token: 0x600020A
        public static System.Linq.Expressions.BinaryExpression Modulo(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC28D4  token: 0x600020B
        public static System.Linq.Expressions.BinaryExpression ModuloAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC3340  token: 0x600020C
        public static System.Linq.Expressions.BinaryExpression Multiply(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC2F28  token: 0x600020D
        public static System.Linq.Expressions.BinaryExpression MultiplyAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC2CEC  token: 0x600020E
        public static System.Linq.Expressions.BinaryExpression MultiplyAssignChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC3164  token: 0x600020F
        public static System.Linq.Expressions.BinaryExpression MultiplyChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC02D8  token: 0x6000210
        private static System.Boolean IsSimpleShift(System.Type left, System.Type right) { }
        // RVA: 0x0ACBDA40  token: 0x6000211
        private static System.Type GetResultTypeOfShift(System.Type left, System.Type right) { }
        // RVA: 0x0ACC0C50  token: 0x6000212
        public static System.Linq.Expressions.BinaryExpression LeftShift(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC0A24  token: 0x6000213
        public static System.Linq.Expressions.BinaryExpression LeftShiftAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC64BC  token: 0x6000214
        public static System.Linq.Expressions.BinaryExpression RightShift(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC6290  token: 0x6000215
        public static System.Linq.Expressions.BinaryExpression RightShiftAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACB5E48  token: 0x6000216
        public static System.Linq.Expressions.BinaryExpression And(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACB5C0C  token: 0x6000217
        public static System.Linq.Expressions.BinaryExpression AndAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC4A30  token: 0x6000218
        public static System.Linq.Expressions.BinaryExpression Or(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC438C  token: 0x6000219
        public static System.Linq.Expressions.BinaryExpression OrAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACBB77C  token: 0x600021A
        public static System.Linq.Expressions.BinaryExpression ExclusiveOr(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBB540  token: 0x600021B
        public static System.Linq.Expressions.BinaryExpression ExclusiveOrAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACC4F8C  token: 0x600021C
        public static System.Linq.Expressions.BinaryExpression Power(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC4E1C  token: 0x600021D
        public static System.Linq.Expressions.BinaryExpression PowerAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion) { }
        // RVA: 0x0ACB646C  token: 0x600021E
        public static System.Linq.Expressions.BinaryExpression ArrayIndex(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression index) { }
        // RVA: 0x0ACB71B4  token: 0x600021F
        public static System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACB73F8  token: 0x6000220
        public static System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACB70A4  token: 0x6000221
        public static System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3) { }
        // RVA: 0x0ACB72C0  token: 0x6000222
        public static System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4) { }
        // RVA: 0x0ACB7660  token: 0x6000223
        public static System.Linq.Expressions.BlockExpression Block(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACB6FF4  token: 0x6000224
        public static System.Linq.Expressions.BlockExpression Block(System.Type type, System.Linq.Expressions.Expression[] expressions) { }
        // RVA: 0x0ACB6F6C  token: 0x6000225
        public static System.Linq.Expressions.BlockExpression Block(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACB76D8  token: 0x6000226
        public static System.Linq.Expressions.BlockExpression Block(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] expressions) { }
        // RVA: 0x0ACB7260  token: 0x6000227
        public static System.Linq.Expressions.BlockExpression Block(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] expressions) { }
        // RVA: 0x0ACB74D4  token: 0x6000228
        public static System.Linq.Expressions.BlockExpression Block(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACB6D14  token: 0x6000229
        public static System.Linq.Expressions.BlockExpression Block(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACB6A00  token: 0x600022A
        private static System.Linq.Expressions.BlockExpression BlockCore(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACCA06C  token: 0x600022B
        private static System.Void ValidateVariables(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> varList, System.String collectionName) { }
        // RVA: 0x0ACBD444  token: 0x600022C
        private static System.Linq.Expressions.BlockExpression GetOptimizedBlockExpression(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACC1A84  token: 0x600022D
        public static System.Linq.Expressions.CatchBlock MakeCatchBlock(System.Type type, System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression filter) { }
        // RVA: 0x0ACBA0AC  token: 0x600022E
        public static System.Linq.Expressions.ConditionalExpression Condition(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse) { }
        // RVA: 0x0ACB9E34  token: 0x600022F
        public static System.Linq.Expressions.ConditionalExpression Condition(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse, System.Type type) { }
        // RVA: 0x0ACBEC90  token: 0x6000230
        public static System.Linq.Expressions.ConditionalExpression IfThen(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue) { }
        // RVA: 0x0ACBA4B4  token: 0x6000231
        public static System.Linq.Expressions.ConstantExpression Constant(System.Object value) { }
        // RVA: 0x0ACBA284  token: 0x6000232
        public static System.Linq.Expressions.ConstantExpression Constant(System.Object value, System.Type type) { }
        // RVA: 0x0ACBB374  token: 0x6000233
        public static System.Linq.Expressions.DefaultExpression Empty() { }
        // RVA: 0x0ACBAE78  token: 0x6000234
        public static System.Linq.Expressions.DefaultExpression Default(System.Type type) { }
        // RVA: 0x041E1670  token: 0x6000235
        protected System.Void .ctor() { }
        // RVA: 0x0ACC5BE0  token: 0x6000239
        public virtual System.Linq.Expressions.Expression Reduce() { }
        // RVA: 0x0ACCA584  token: 0x600023A
        protected internal virtual System.Linq.Expressions.Expression VisitChildren(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACB4EE4  token: 0x600023B
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACC5ACC  token: 0x600023C
        public System.Linq.Expressions.Expression ReduceAndCheck() { }
        // RVA: 0x0ACC703C  token: 0x600023D
        public virtual System.String ToString() { }
        // RVA: 0x0ACC5EE8  token: 0x600023E
        private static System.Void RequiresCanRead(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> items, System.String paramName) { }
        // RVA: 0x0ACC5F8C  token: 0x600023F
        private static System.Void RequiresCanWrite(System.Linq.Expressions.Expression expression, System.String paramName) { }
        // RVA: 0x0ACB7734  token: 0x6000240
        public static System.Linq.Expressions.GotoExpression Break(System.Linq.Expressions.LabelTarget target) { }
        // RVA: 0x0ACC6160  token: 0x6000241
        public static System.Linq.Expressions.GotoExpression Return(System.Linq.Expressions.LabelTarget target) { }
        // RVA: 0x0ACC61F0  token: 0x6000242
        public static System.Linq.Expressions.GotoExpression Return(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value) { }
        // RVA: 0x0ACBEA7C  token: 0x6000243
        public static System.Linq.Expressions.GotoExpression Goto(System.Linq.Expressions.LabelTarget target, System.Type type) { }
        // RVA: 0x0ACBE9E0  token: 0x6000244
        public static System.Linq.Expressions.GotoExpression Goto(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value) { }
        // RVA: 0x0ACC1D00  token: 0x6000245
        public static System.Linq.Expressions.GotoExpression MakeGoto(System.Linq.Expressions.GotoExpressionKind kind, System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value, System.Type type) { }
        // RVA: 0x0ACC8134  token: 0x6000246
        private static System.Void ValidateGoto(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression& value, System.String targetParameter, System.String valueParameter, System.Type type) { }
        // RVA: 0x0ACC8000  token: 0x6000247
        private static System.Void ValidateGotoType(System.Type expectedType, System.Linq.Expressions.Expression& value, System.String paramName) { }
        // RVA: 0x0ACC1EA0  token: 0x6000248
        public static System.Linq.Expressions.IndexExpression MakeIndex(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments) { }
        // RVA: 0x0ACB60B4  token: 0x6000249
        public static System.Linq.Expressions.IndexExpression ArrayAccess(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression[] indexes) { }
        // RVA: 0x0ACB6110  token: 0x600024A
        public static System.Linq.Expressions.IndexExpression ArrayAccess(System.Linq.Expressions.Expression array, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> indexes) { }
        // RVA: 0x0ACC5860  token: 0x600024B
        public static System.Linq.Expressions.IndexExpression Property(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments) { }
        // RVA: 0x0ACC1DE4  token: 0x600024C
        private static System.Linq.Expressions.IndexExpression MakeIndexProperty(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.String paramName, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> argList) { }
        // RVA: 0x0ACC82B4  token: 0x600024D
        private static System.Void ValidateIndexedProperty(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.String paramName, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& argList) { }
        // RVA: 0x0ACC7BEC  token: 0x600024E
        private static System.Void ValidateAccessor(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] indexes, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String paramName) { }
        // RVA: 0x0ACC77EC  token: 0x600024F
        private static System.Void ValidateAccessorArgumentTypes(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] indexes, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String paramName) { }
        // RVA: 0x0ACBF284  token: 0x6000250
        private static System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0ACBF9F8  token: 0x6000251
        private static System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0) { }
        // RVA: 0x0ACBF370  token: 0x6000252
        private static System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACBF528  token: 0x6000253
        private static System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACBFB54  token: 0x6000254
        private static System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3) { }
        // RVA: 0x0ACBF73C  token: 0x6000255
        private static System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4) { }
        // RVA: 0x0ACBEE8C  token: 0x6000256
        public static System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments) { }
        // RVA: 0x0ACBC47C  token: 0x6000257
        private static System.Reflection.MethodInfo GetInvokeMethod(System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0ACC06EC  token: 0x6000258
        public static System.Linq.Expressions.LabelExpression Label(System.Linq.Expressions.LabelTarget target) { }
        // RVA: 0x0ACC061C  token: 0x6000259
        public static System.Linq.Expressions.LabelExpression Label(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression defaultValue) { }
        // RVA: 0x0ACC0738  token: 0x600025A
        public static System.Linq.Expressions.LabelTarget Label() { }
        // RVA: 0x0ACC0594  token: 0x600025B
        public static System.Linq.Expressions.LabelTarget Label(System.String name) { }
        // RVA: 0x0ACC08A4  token: 0x600025C
        public static System.Linq.Expressions.LabelTarget Label(System.Type type) { }
        // RVA: 0x0ACC07B0  token: 0x600025D
        public static System.Linq.Expressions.LabelTarget Label(System.Type type, System.String name) { }
        // RVA: 0x0ACBA95C  token: 0x600025E
        private static System.Linq.Expressions.LambdaExpression CreateLambda(System.Type delegateType, System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: -1  // generic def  token: 0x600025F
        public static System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }
        // RVA: -1  // generic def  token: 0x6000260
        public static System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: -1  // generic def  token: 0x6000261
        public static System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.Boolean tailCall, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: -1  // generic def  token: 0x6000262
        public static System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: 0x0ACC08F0  token: 0x6000263
        public static System.Linq.Expressions.LambdaExpression Lambda(System.Type delegateType, System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }
        // RVA: 0x0ACC095C  token: 0x6000264
        public static System.Linq.Expressions.LambdaExpression Lambda(System.Type delegateType, System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: 0x0ACC8818  token: 0x6000265
        private static System.Void ValidateLambdaArgs(System.Type delegateType, System.Linq.Expressions.Expression& body, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters, System.String paramName) { }
        // RVA: 0x0ACC0FDC  token: 0x6000266
        public static System.Linq.Expressions.LoopExpression Loop(System.Linq.Expressions.Expression body, System.Linq.Expressions.LabelTarget break, System.Linq.Expressions.LabelTarget continue) { }
        // RVA: 0x0ACBBADC  token: 0x6000267
        public static System.Linq.Expressions.MemberExpression Field(System.Linq.Expressions.Expression expression, System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACBB958  token: 0x6000268
        public static System.Linq.Expressions.MemberExpression Field(System.Linq.Expressions.Expression expression, System.String fieldName) { }
        // RVA: 0x0ACC56C8  token: 0x6000269
        public static System.Linq.Expressions.MemberExpression Property(System.Linq.Expressions.Expression expression, System.String propertyName) { }
        // RVA: 0x0ACC53AC  token: 0x600026A
        public static System.Linq.Expressions.MemberExpression Property(System.Linq.Expressions.Expression expression, System.Reflection.PropertyInfo property) { }
        // RVA: 0x0ACC58F8  token: 0x600026B
        public static System.Linq.Expressions.MemberExpression Property(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo propertyAccessor) { }
        // RVA: 0x0ACBD86C  token: 0x600026C
        private static System.Reflection.PropertyInfo GetProperty(System.Reflection.MethodInfo mi, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACB98BC  token: 0x600026D
        private static System.Boolean CheckMethod(System.Reflection.MethodInfo method, System.Reflection.MethodInfo propertyMethod) { }
        // RVA: 0x0ACC1F28  token: 0x600026E
        public static System.Linq.Expressions.MemberExpression MakeMemberAccess(System.Linq.Expressions.Expression expression, System.Reflection.MemberInfo member) { }
        // RVA: 0x0ACB8B48  token: 0x600026F
        private static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACB9010  token: 0x6000270
        public static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0) { }
        // RVA: 0x0ACB91C4  token: 0x6000271
        public static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACB7C54  token: 0x6000272
        public static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACB8C40  token: 0x6000273
        public static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3) { }
        // RVA: 0x0ACB8228  token: 0x6000274
        public static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4) { }
        // RVA: 0x0ACB942C  token: 0x6000275
        public static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression[] arguments) { }
        // RVA: 0x0ACB9868  token: 0x6000276
        public static System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments) { }
        // RVA: 0x0ACB8A08  token: 0x6000277
        public static System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACB9480  token: 0x6000278
        public static System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression[] arguments) { }
        // RVA: 0x0ACB94E0  token: 0x6000279
        private static System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0) { }
        // RVA: 0x0ACB7F6C  token: 0x600027A
        public static System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACB8690  token: 0x600027B
        public static System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACB96E4  token: 0x600027C
        public static System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.String methodName, System.Type[] typeArguments, System.Linq.Expressions.Expression[] arguments) { }
        // RVA: 0x0ACB77C4  token: 0x600027D
        public static System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments) { }
        // RVA: 0x0ACC8E78  token: 0x600027E
        private static System.Reflection.ParameterInfo[] ValidateMethodAndGetParameters(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC93FC  token: 0x600027F
        private static System.Void ValidateStaticOrInstanceMethod(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC7E10  token: 0x6000280
        private static System.Void ValidateCallInstanceType(System.Type instanceType, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC7E00  token: 0x6000281
        private static System.Void ValidateArgumentTypes(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String methodParamName) { }
        // RVA: 0x0ACBD864  token: 0x6000282
        private static System.Reflection.ParameterInfo[] GetParametersForValidation(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind) { }
        // RVA: 0x0ACC7DF0  token: 0x6000283
        private static System.Void ValidateArgumentCount(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Int32 count, System.Reflection.ParameterInfo[] pis) { }
        // RVA: 0x0ACC8F70  token: 0x6000284
        private static System.Linq.Expressions.Expression ValidateOneArgument(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Linq.Expressions.Expression arg, System.Reflection.ParameterInfo pi, System.String methodParamName, System.String argumentParamName) { }
        // RVA: 0x0ACC70A4  token: 0x6000285
        private static System.Boolean TryQuote(System.Type parameterType, System.Linq.Expressions.Expression& argument) { }
        // RVA: 0x0ACBBCD0  token: 0x6000286
        private static System.Reflection.MethodInfo FindMethod(System.Type type, System.String methodName, System.Type[] typeArgs, System.Linq.Expressions.Expression[] args, System.Reflection.BindingFlags flags) { }
        // RVA: 0x0ACBFDB8  token: 0x6000287
        private static System.Boolean IsCompatible(System.Reflection.MethodBase m, System.Linq.Expressions.Expression[] arguments) { }
        // RVA: 0x0ACB6024  token: 0x6000288
        private static System.Reflection.MethodInfo ApplyTypeArgs(System.Reflection.MethodInfo m, System.Type[] typeArgs) { }
        // RVA: 0x0ACC3EDC  token: 0x6000289
        public static System.Linq.Expressions.NewArrayExpression NewArrayInit(System.Type type, System.Linq.Expressions.Expression[] initializers) { }
        // RVA: 0x0ACC3AFC  token: 0x600028A
        public static System.Linq.Expressions.NewArrayExpression NewArrayInit(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> initializers) { }
        // RVA: 0x0ACC380C  token: 0x600028B
        public static System.Linq.Expressions.NewArrayExpression NewArrayBounds(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> bounds) { }
        // RVA: 0x0ACC4C90  token: 0x600028C
        public static System.Linq.Expressions.ParameterExpression Parameter(System.Type type) { }
        // RVA: 0x0ACC4CDC  token: 0x600028D
        public static System.Linq.Expressions.ParameterExpression Parameter(System.Type type, System.String name) { }
        // RVA: 0x0ACCA36C  token: 0x600028E
        public static System.Linq.Expressions.ParameterExpression Variable(System.Type type, System.String name) { }
        // RVA: 0x0ACCA244  token: 0x600028F
        private static System.Void Validate(System.Type type, System.Boolean allowByRef) { }
        // RVA: 0x0ACC7044  token: 0x6000290
        public static System.Linq.Expressions.TryExpression TryFinally(System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally) { }
        // RVA: 0x0ACC22D8  token: 0x6000291
        public static System.Linq.Expressions.TryExpression MakeTry(System.Type type, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally, System.Linq.Expressions.Expression fault, System.Collections.Generic.IEnumerable<System.Linq.Expressions.CatchBlock> handlers) { }
        // RVA: 0x0ACC94E4  token: 0x6000292
        private static System.Void ValidateTryAndCatchHaveSameType(System.Type type, System.Linq.Expressions.Expression tryBody, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock> handlers) { }
        // RVA: 0x0ACC735C  token: 0x6000293
        public static System.Linq.Expressions.TypeBinaryExpression TypeIs(System.Linq.Expressions.Expression expression, System.Type type) { }
        // RVA: 0x0ACC722C  token: 0x6000294
        public static System.Linq.Expressions.TypeBinaryExpression TypeEqual(System.Linq.Expressions.Expression expression, System.Type type) { }
        // RVA: 0x0ACC2504  token: 0x6000295
        public static System.Linq.Expressions.UnaryExpression MakeUnary(System.Linq.Expressions.ExpressionType unaryType, System.Linq.Expressions.Expression operand, System.Type type, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBE624  token: 0x6000296
        private static System.Linq.Expressions.UnaryExpression GetUserDefinedUnaryOperatorOrThrow(System.Linq.Expressions.ExpressionType unaryType, System.String name, System.Linq.Expressions.Expression operand) { }
        // RVA: 0x0ACBE780  token: 0x6000297
        private static System.Linq.Expressions.UnaryExpression GetUserDefinedUnaryOperator(System.Linq.Expressions.ExpressionType unaryType, System.String name, System.Linq.Expressions.Expression operand) { }
        // RVA: 0x0ACBD0F0  token: 0x6000298
        private static System.Linq.Expressions.UnaryExpression GetMethodBasedUnaryOperator(System.Linq.Expressions.ExpressionType unaryType, System.Linq.Expressions.Expression operand, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBE494  token: 0x6000299
        private static System.Linq.Expressions.UnaryExpression GetUserDefinedCoercionOrThrow(System.Linq.Expressions.ExpressionType coercionType, System.Linq.Expressions.Expression expression, System.Type convertToType) { }
        // RVA: 0x0ACBE53C  token: 0x600029A
        private static System.Linq.Expressions.UnaryExpression GetUserDefinedCoercion(System.Linq.Expressions.ExpressionType coercionType, System.Linq.Expressions.Expression expression, System.Type convertToType) { }
        // RVA: 0x0ACBCDA0  token: 0x600029B
        private static System.Linq.Expressions.UnaryExpression GetMethodBasedCoercionOperator(System.Linq.Expressions.ExpressionType unaryType, System.Linq.Expressions.Expression operand, System.Type convertToType, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC3694  token: 0x600029C
        public static System.Linq.Expressions.UnaryExpression Negate(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC748C  token: 0x600029D
        public static System.Linq.Expressions.UnaryExpression UnaryPlus(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC351C  token: 0x600029E
        public static System.Linq.Expressions.UnaryExpression NegateChecked(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC4070  token: 0x600029F
        public static System.Linq.Expressions.UnaryExpression Not(System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0ACC40BC  token: 0x60002A0
        public static System.Linq.Expressions.UnaryExpression Not(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBFFD4  token: 0x60002A1
        public static System.Linq.Expressions.UnaryExpression IsFalse(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC0380  token: 0x60002A2
        public static System.Linq.Expressions.UnaryExpression IsTrue(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC4240  token: 0x60002A3
        public static System.Linq.Expressions.UnaryExpression OnesComplement(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC70AC  token: 0x60002A4
        public static System.Linq.Expressions.UnaryExpression TypeAs(System.Linq.Expressions.Expression expression, System.Type type) { }
        // RVA: 0x0ACC75D8  token: 0x60002A5
        public static System.Linq.Expressions.UnaryExpression Unbox(System.Linq.Expressions.Expression expression, System.Type type) { }
        // RVA: 0x0ACBA908  token: 0x60002A6
        public static System.Linq.Expressions.UnaryExpression Convert(System.Linq.Expressions.Expression expression, System.Type type) { }
        // RVA: 0x0ACBA71C  token: 0x60002A7
        public static System.Linq.Expressions.UnaryExpression Convert(System.Linq.Expressions.Expression expression, System.Type type, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBA514  token: 0x60002A8
        public static System.Linq.Expressions.UnaryExpression ConvertChecked(System.Linq.Expressions.Expression expression, System.Type type, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACB6664  token: 0x60002A9
        public static System.Linq.Expressions.UnaryExpression ArrayLength(System.Linq.Expressions.Expression array) { }
        // RVA: 0x0ACC59D0  token: 0x60002AA
        public static System.Linq.Expressions.UnaryExpression Quote(System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0ACC6EC0  token: 0x60002AB
        public static System.Linq.Expressions.UnaryExpression Throw(System.Linq.Expressions.Expression value, System.Type type) { }
        // RVA: 0x0ACBED40  token: 0x60002AC
        public static System.Linq.Expressions.UnaryExpression Increment(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACBAD2C  token: 0x60002AD
        public static System.Linq.Expressions.UnaryExpression Decrement(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC5358  token: 0x60002AE
        public static System.Linq.Expressions.UnaryExpression PreIncrementAssign(System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0ACC5304  token: 0x60002AF
        public static System.Linq.Expressions.UnaryExpression PreIncrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC52B0  token: 0x60002B0
        public static System.Linq.Expressions.UnaryExpression PreDecrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC4DC8  token: 0x60002B1
        public static System.Linq.Expressions.UnaryExpression PostIncrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC4D74  token: 0x60002B2
        public static System.Linq.Expressions.UnaryExpression PostDecrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACC2094  token: 0x60002B3
        private static System.Linq.Expressions.UnaryExpression MakeOpAssignUnary(System.Linq.Expressions.ExpressionType kind, System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACCA5F4  token: 0x60002B4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006B  // size: 0x10
    public class BlockExpression : System.Linq.Expressions.Expression
    {
        // Properties
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> Expressions { get; /* RVA: 0x0ACCE210 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> Variables { get; /* RVA: 0x0ACCE284 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA29E0 */ }
        System.Type Type { get; /* RVA: 0x0ACCE240 */ }
        System.Int32 ExpressionCount { get; /* RVA: 0x0ACCE1E8 */ }

        // Methods
        // RVA: 0x0ACCE1A8  token: 0x60002B7
        private System.Void .ctor() { }
        // RVA: 0x0ACCDFA0  token: 0x60002B8
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACCDFE8  token: 0x60002BB
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACCE010  token: 0x60002BD
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACCE038  token: 0x60002BE
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> GetOrMakeVariables() { }
        // RVA: 0x0ACCE180  token: 0x60002BF
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }
        // RVA: 0x0ACCE088  token: 0x60002C0
        private static System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> ReturnReadOnlyExpressions(System.Linq.Expressions.BlockExpression provider, System.Object& collection) { }

    }

    // TypeToken: 0x200006C  // size: 0x20
    public sealed class Block2 : System.Linq.Expressions.BlockExpression
    {
        // Fields
        private System.Object _arg0;  // 0x10
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x18

        // Properties
        System.Int32 ExpressionCount { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x0ACCD5E8  token: 0x60002C1
        private System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACCD4C8  token: 0x60002C2
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACCD550  token: 0x60002C4
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACCD55C  token: 0x60002C5
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x200006D  // size: 0x28
    public sealed class Block3 : System.Linq.Expressions.BlockExpression
    {
        // Fields
        private System.Object _arg0;  // 0x10
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x18
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x20

        // Properties
        System.Int32 ExpressionCount { get; /* RVA: 0x04D8C9E0 */ }

        // Methods
        // RVA: 0x0ACCD76C  token: 0x60002C6
        private System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACCD630  token: 0x60002C7
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACCD550  token: 0x60002C9
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACCD6C4  token: 0x60002CA
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x200006E  // size: 0x30
    public sealed class Block4 : System.Linq.Expressions.BlockExpression
    {
        // Fields
        private System.Object _arg0;  // 0x10
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x18
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg3;  // 0x28

        // Properties
        System.Int32 ExpressionCount { get; /* RVA: 0x04D882B0 */ }

        // Methods
        // RVA: 0x0ACCD92C  token: 0x60002CB
        private System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3) { }
        // RVA: 0x0ACCD7D0  token: 0x60002CC
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACCD550  token: 0x60002CE
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACCD86C  token: 0x60002CF
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x200006F  // size: 0x38
    public sealed class Block5 : System.Linq.Expressions.BlockExpression
    {
        // Fields
        private System.Object _arg0;  // 0x10
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x18
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg3;  // 0x28
        private readonly System.Linq.Expressions.Expression _arg4;  // 0x30

        // Properties
        System.Int32 ExpressionCount { get; /* RVA: 0x04D9EC50 */ }

        // Methods
        // RVA: 0x0ACCDB18  token: 0x60002D0
        private System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4) { }
        // RVA: 0x0ACCD9A0  token: 0x60002D1
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACCD550  token: 0x60002D3
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACCDA48  token: 0x60002D4
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x2000070  // size: 0x18
    public class BlockN : System.Linq.Expressions.BlockExpression
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> _expressions;  // 0x10

        // Properties
        System.Int32 ExpressionCount { get; /* RVA: 0x0ACCE3D4 */ }

        // Methods
        // RVA: 0x0ACCE3A4  token: 0x60002D5
        private System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACCE2B4  token: 0x60002D6
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACCE308  token: 0x60002D8
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACCE344  token: 0x60002D9
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x2000071  // size: 0x18
    public class ScopeExpression : System.Linq.Expressions.BlockExpression
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> _variables;  // 0x10

        // Properties
        System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> VariablesList { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0ACCE3A4  token: 0x60002DA
        private System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables) { }
        // RVA: 0x0ACDFCC4  token: 0x60002DB
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> GetOrMakeVariables() { }
        // RVA: 0x0ACDFD00  token: 0x60002DD
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> ReuseOrValidateVariables(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables) { }

    }

    // TypeToken: 0x2000072  // size: 0x20
    public sealed class Scope1 : System.Linq.Expressions.ScopeExpression
    {
        // Fields
        private System.Object _body;  // 0x18

        // Properties
        System.Int32 ExpressionCount { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0ACDFC8C  token: 0x60002DE
        private System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression body) { }
        // RVA: 0x0ACDFC94  token: 0x60002DF
        private System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Object body) { }
        // RVA: 0x0ACDFB14  token: 0x60002E0
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACDFB90  token: 0x60002E2
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACDFB9C  token: 0x60002E3
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x2000073  // size: 0x20
    public class ScopeN : System.Linq.Expressions.ScopeExpression
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> _body;  // 0x18

        // Properties
        System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> Body { get; /* RVA: 0x0385B100 */ }
        System.Int32 ExpressionCount { get; /* RVA: 0x0ACDFEF4 */ }

        // Methods
        // RVA: 0x0ACDFC94  token: 0x60002E4
        private System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> body) { }
        // RVA: 0x0ACDFD78  token: 0x60002E6
        private virtual System.Linq.Expressions.Expression GetExpression(System.Int32 index) { }
        // RVA: 0x0ACDFDCC  token: 0x60002E8
        private virtual System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions() { }
        // RVA: 0x0ACDFE08  token: 0x60002E9
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x2000074  // size: 0x28
    public sealed class ScopeWithType : System.Linq.Expressions.ScopeN
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x20

        // Properties
        System.Type Type { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0ACE0060  token: 0x60002EA
        private System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> expressions, System.Type type) { }
        // RVA: 0x0ACDFF3C  token: 0x60002EC
        private virtual System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args) { }

    }

    // TypeToken: 0x2000075  // size: 0x20
    public class BlockExpressionList : System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Linq.Expressions.BlockExpression _block;  // 0x10
        private readonly System.Linq.Expressions.Expression _arg0;  // 0x18

        // Properties
        System.Linq.Expressions.Expression Item { get; /* RVA: 0x0ACCDF48 */ set; /* RVA: 0x0ACCDF78 */ }
        System.Int32 Count { get; /* RVA: 0x0A8E29E4 */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x0ACCDF20 */ }

        // Methods
        // RVA: 0x032AF420  token: 0x60002ED
        private System.Void .ctor(System.Linq.Expressions.BlockExpression provider, System.Linq.Expressions.Expression arg0) { }
        // RVA: 0x0ACCDE28  token: 0x60002EE
        public virtual System.Int32 IndexOf(System.Linq.Expressions.Expression item) { }
        // RVA: 0x0ACCDEA0  token: 0x60002EF
        public virtual System.Void Insert(System.Int32 index, System.Linq.Expressions.Expression item) { }
        // RVA: 0x0ACCDEC8  token: 0x60002F0
        public virtual System.Void RemoveAt(System.Int32 index) { }
        // RVA: 0x0ACCDBA0  token: 0x60002F3
        public virtual System.Void Add(System.Linq.Expressions.Expression item) { }
        // RVA: 0x0ACCDBC8  token: 0x60002F4
        public virtual System.Void Clear() { }
        // RVA: 0x0ACCDBF0  token: 0x60002F5
        public virtual System.Boolean Contains(System.Linq.Expressions.Expression item) { }
        // RVA: 0x0ACCDC08  token: 0x60002F6
        public virtual System.Void CopyTo(System.Linq.Expressions.Expression[] array, System.Int32 index) { }
        // RVA: 0x0ACCDEF0  token: 0x60002F9
        public virtual System.Boolean Remove(System.Linq.Expressions.Expression item) { }
        // RVA: 0x0ACCDDCC  token: 0x60002FA
        public virtual System.Collections.Generic.IEnumerator<System.Linq.Expressions.Expression> GetEnumerator() { }
        // RVA: 0x0ACCDF18  token: 0x60002FB
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000077  // size: 0x30
    public sealed class CatchBlock
    {
        // Fields
        private readonly System.Linq.Expressions.ParameterExpression <Variable>k__BackingField;  // 0x10
        private readonly System.Type <Test>k__BackingField;  // 0x18
        private readonly System.Linq.Expressions.Expression <Body>k__BackingField;  // 0x20
        private readonly System.Linq.Expressions.Expression <Filter>k__BackingField;  // 0x28

        // Properties
        System.Linq.Expressions.ParameterExpression Variable { get; /* RVA: 0x02B2ECC0 */ }
        System.Type Test { get; /* RVA: 0x0385B100 */ }
        System.Linq.Expressions.Expression Body { get; /* RVA: 0x04D862C0 */ }
        System.Linq.Expressions.Expression Filter { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0ACCF93C  token: 0x6000302
        private System.Void .ctor(System.Type test, System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression filter) { }
        // RVA: 0x0ACCF8A4  token: 0x6000307
        public virtual System.String ToString() { }
        // RVA: 0x0ACCF8AC  token: 0x6000308
        public System.Linq.Expressions.CatchBlock Update(System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Expression filter, System.Linq.Expressions.Expression body) { }

    }

    // TypeToken: 0x2000078  // size: 0x10
    public static class ArrayBuilderExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000309
        public static System.Collections.ObjectModel.ReadOnlyCollection<T> ToReadOnly(System.Collections.Generic.ArrayBuilder<T> builder) { }

    }

    // TypeToken: 0x2000079  // size: 0x14
    public sealed struct AnalyzeTypeIsResult
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Linq.Expressions.AnalyzeTypeIsResult KnownFalse;  // const
        public static System.Linq.Expressions.AnalyzeTypeIsResult KnownTrue;  // const
        public static System.Linq.Expressions.AnalyzeTypeIsResult KnownAssignable;  // const
        public static System.Linq.Expressions.AnalyzeTypeIsResult Unknown;  // const

    }

    // TypeToken: 0x200007A  // size: 0x10
    public static class ConstantCheck
    {
        // Methods
        // RVA: 0x0ACCFDE0  token: 0x600030A
        private static System.Linq.Expressions.AnalyzeTypeIsResult AnalyzeTypeIs(System.Linq.Expressions.TypeBinaryExpression typeIs) { }
        // RVA: 0x0ACCFE04  token: 0x600030B
        private static System.Linq.Expressions.AnalyzeTypeIsResult AnalyzeTypeIs(System.Linq.Expressions.Expression operand, System.Type testType) { }

    }

    // TypeToken: 0x200007B  // size: 0x20
    public class ConditionalExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.Expression <Test>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <IfTrue>k__BackingField;  // 0x18

        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04D8D060 */ }
        System.Type Type { get; /* RVA: 0x0ACB283C */ }
        System.Linq.Expressions.Expression Test { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.Expression IfTrue { get; /* RVA: 0x0385B100 */ }
        System.Linq.Expressions.Expression IfFalse { get; /* RVA: 0x0AA6D17C */ }

        // Methods
        // RVA: 0x0ACCFD74  token: 0x600030C
        private System.Void .ctor(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue) { }
        // RVA: 0x0ACCFB0C  token: 0x600030D
        private static System.Linq.Expressions.ConditionalExpression Make(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse, System.Type type) { }
        // RVA: 0x0ACCFAB8  token: 0x6000313
        private virtual System.Linq.Expressions.Expression GetFalse() { }
        // RVA: 0x0ACCFA70  token: 0x6000314
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACCFCD0  token: 0x6000315
        public System.Linq.Expressions.ConditionalExpression Update(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse) { }

    }

    // TypeToken: 0x200007C  // size: 0x28
    public class FullConditionalExpression : System.Linq.Expressions.ConditionalExpression
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _false;  // 0x20

        // Methods
        // RVA: 0x0ACDB414  token: 0x6000316
        private System.Void .ctor(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse) { }
        // RVA: 0x04D862C0  token: 0x6000317
        private virtual System.Linq.Expressions.Expression GetFalse() { }

    }

    // TypeToken: 0x200007D  // size: 0x30
    public sealed class FullConditionalExpressionWithType : System.Linq.Expressions.FullConditionalExpression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x28

        // Properties
        System.Type Type { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0ACDB3E4  token: 0x6000318
        private System.Void .ctor(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse, System.Type type) { }

    }

    // TypeToken: 0x200007E  // size: 0x18
    public class ConstantExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Object <Value>k__BackingField;  // 0x10

        // Properties
        System.Type Type { get; /* RVA: 0x0ACD0024 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA4FC0 */ }
        System.Object Value { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0ACCFFCC  token: 0x600031A
        private System.Void .ctor(System.Object value) { }
        // RVA: 0x0ACCFF84  token: 0x600031E
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }

    }

    // TypeToken: 0x200007F  // size: 0x20
    public class TypedConstantExpression : System.Linq.Expressions.ConstantExpression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0ACE25A8  token: 0x600031F
        private System.Void .ctor(System.Object value, System.Type type) { }

    }

    // TypeToken: 0x2000080  // size: 0x18
    public class DebugInfoExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.SymbolDocumentInfo <Document>k__BackingField;  // 0x10

        // Properties
        System.Int32 StartLine { get; /* RVA: 0x0ACD018C */ }
        System.Int32 EndLine { get; /* RVA: 0x0ACD013C */ }
        System.Linq.Expressions.SymbolDocumentInfo Document { get; /* RVA: 0x02B2ECC0 */ }
        System.Boolean IsClear { get; /* RVA: 0x0ACD0164 */ }

    }

    // TypeToken: 0x2000081  // size: 0x18
    public sealed class DefaultExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA9F60 */ }

        // Methods
        // RVA: 0x0ACD0A98  token: 0x6000325
        private System.Void .ctor(System.Type type) { }
        // RVA: 0x0ACD0A50  token: 0x6000328
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }

    }

    // TypeToken: 0x2000082  // size: 0x20
    public sealed class ElementInit : System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private readonly System.Reflection.MethodInfo <AddMethod>k__BackingField;  // 0x10
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> <Arguments>k__BackingField;  // 0x18

        // Properties
        System.Reflection.MethodInfo AddMethod { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> Arguments { get; /* RVA: 0x0385B100 */ }
        System.Int32 ArgumentCount { get; /* RVA: 0x0ACD18E8 */ }

        // Methods
        // RVA: 0x0ACD1894  token: 0x600032B
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }

    }

    // TypeToken: 0x2000083  // size: 0x10
    public static class Error
    {
        // Methods
        // RVA: 0x0ACD767C  token: 0x600032D
        private static System.Exception ReducibleMustOverrideReduce() { }
        // RVA: 0x0ACD6318  token: 0x600032E
        private static System.Exception InvalidMetaObjectCreated(System.Object p0) { }
        // RVA: 0x0ACD46EC  token: 0x600032F
        private static System.Exception AmbiguousMatchInExpandoObject(System.Object p0) { }
        // RVA: 0x0ACD77C0  token: 0x6000330
        private static System.Exception SameKeyExistsInExpando(System.Object key) { }
        // RVA: 0x0ACD6574  token: 0x6000331
        private static System.Exception KeyDoesNotExistInExpando(System.Object p0) { }
        // RVA: 0x0ACD4F94  token: 0x6000332
        private static System.Exception CollectionModifiedWhileEnumerating() { }
        // RVA: 0x0ACD4FFC  token: 0x6000333
        private static System.Exception CollectionReadOnly() { }
        // RVA: 0x0ACD6C18  token: 0x6000334
        private static System.Exception MustReduceToDifferent() { }
        // RVA: 0x0ACD4C08  token: 0x6000335
        private static System.Exception BinderNotCompatibleWithCallSite(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACD539C  token: 0x6000336
        private static System.Exception DynamicBindingNeedsRestrictions(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD5410  token: 0x6000337
        private static System.Exception DynamicObjectResultNotAssignable(System.Object p0, System.Object p1, System.Object p2, System.Object p3) { }
        // RVA: 0x0ACD5318  token: 0x6000338
        private static System.Exception DynamicBinderResultNotAssignable(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACD4C8C  token: 0x6000339
        private static System.Exception BindingCannotBeNull() { }
        // RVA: 0x0ACD7614  token: 0x600033A
        private static System.Exception ReducedNotCompatible() { }
        // RVA: 0x0ACD7840  token: 0x600033B
        private static System.Exception SetterHasNoParams(System.String paramName) { }
        // RVA: 0x0ACD72BC  token: 0x600033C
        private static System.Exception PropertyCannotHaveRefType(System.String paramName) { }
        // RVA: 0x0ACD6020  token: 0x600033D
        private static System.Exception IndexesOfSetGetMustMatch(System.String paramName) { }
        // RVA: 0x0ACD7C48  token: 0x600033E
        private static System.Exception TypeParameterIsNotDelegate(System.Object p0) { }
        // RVA: 0x0ACD5C34  token: 0x600033F
        private static System.Exception FirstArgumentMustBeCallSite() { }
        // RVA: 0x0ACD4608  token: 0x6000340
        private static System.Exception AccessorsCannotHaveVarArgs(System.String paramName) { }
        // RVA: 0x0ACD4590  token: 0x6000341
        private static System.Exception AccessorsCannotHaveByRefArgs(System.String paramName) { }
        // RVA: 0x0ACD4574  token: 0x6000342
        private static System.Exception AccessorsCannotHaveByRefArgs(System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD7AF0  token: 0x6000343
        private static System.Exception TypeMustBeDerivedFromSystemDelegate() { }
        // RVA: 0x0ACD6DFC  token: 0x6000344
        private static System.Exception NoOrInvalidRuleProduced() { }
        // RVA: 0x0ACD4DD4  token: 0x6000345
        private static System.Exception BoundsCannotBeLessThanOne(System.String paramName) { }
        // RVA: 0x0ACD7B58  token: 0x6000346
        private static System.Exception TypeMustNotBeByRef(System.String paramName) { }
        // RVA: 0x0ACD7BD0  token: 0x6000347
        private static System.Exception TypeMustNotBePointer(System.String paramName) { }
        // RVA: 0x0ACD78B8  token: 0x6000348
        private static System.Exception SetterMustBeVoid(System.String paramName) { }
        // RVA: 0x0ACD74AC  token: 0x6000349
        private static System.Exception PropertyTypeMustMatchGetter(System.String paramName) { }
        // RVA: 0x0ACD7524  token: 0x600034A
        private static System.Exception PropertyTypeMustMatchSetter(System.String paramName) { }
        // RVA: 0x0ACD4D5C  token: 0x600034B
        private static System.Exception BothAccessorsMustBeStatic(System.String paramName) { }
        // RVA: 0x0ACD6F88  token: 0x600034C
        private static System.Exception OnlyStaticFieldsHaveNullInstance(System.String paramName) { }
        // RVA: 0x0ACD7068  token: 0x600034D
        private static System.Exception OnlyStaticPropertiesHaveNullInstance(System.String paramName) { }
        // RVA: 0x0ACD7000  token: 0x600034E
        private static System.Exception OnlyStaticMethodsHaveNullInstance() { }
        // RVA: 0x0ACD7434  token: 0x600034F
        private static System.Exception PropertyTypeCannotBeVoid(System.String paramName) { }
        // RVA: 0x0ACD64FC  token: 0x6000350
        private static System.Exception InvalidUnboxType(System.String paramName) { }
        // RVA: 0x0ACD55A4  token: 0x6000351
        private static System.Exception ExpressionMustBeWriteable(System.String paramName) { }
        // RVA: 0x0ACD4A44  token: 0x6000352
        private static System.Exception ArgumentMustNotHaveValueType(System.String paramName) { }
        // RVA: 0x0ACD6BB0  token: 0x6000353
        private static System.Exception MustBeReducible() { }
        // RVA: 0x0ACD65E0  token: 0x6000354
        private static System.Exception LabelMustBeVoidOrHaveExpression(System.String paramName) { }
        // RVA: 0x0ACD6730  token: 0x6000355
        private static System.Exception LabelTypeMustBeVoid(System.String paramName) { }
        // RVA: 0x0ACD759C  token: 0x6000356
        private static System.Exception QuotedExpressionMustBeLambda(System.String paramName) { }
        // RVA: 0x0ACD803C  token: 0x6000357
        private static System.Exception VariableMustNotBeByRef(System.Object p0, System.Object p1, System.String paramName) { }
        // RVA: 0x0ACD8000  token: 0x6000358
        private static System.Exception VariableMustNotBeByRef(System.Object p0, System.Object p1, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD526C  token: 0x6000359
        private static System.Exception DuplicateVariable(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD52E8  token: 0x600035A
        private static System.Exception DuplicateVariable(System.Object p0, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD5B38  token: 0x600035B
        private static System.Exception FaultCannotHaveCatchOrFinally(System.String paramName) { }
        // RVA: 0x0ACD7930  token: 0x600035C
        private static System.Exception TryMustHaveCatchFinallyOrFault() { }
        // RVA: 0x0ACD4CF4  token: 0x600035D
        private static System.Exception BodyOfCatchMustHaveSameTypeAsBodyOfTry() { }
        // RVA: 0x0ACD5ACC  token: 0x600035E
        private static System.Exception ExtensionNodeMustOverrideProperty(System.Object p0) { }
        // RVA: 0x0ACD7F08  token: 0x600035F
        private static System.Exception UserDefinedOperatorMustBeStatic(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD7F84  token: 0x6000360
        private static System.Exception UserDefinedOperatorMustNotBeVoid(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD4F20  token: 0x6000361
        private static System.Exception CoercionOperatorNotDefined(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD7CB4  token: 0x6000362
        private static System.Exception UnaryOperatorNotDefined(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD4B84  token: 0x6000363
        private static System.Exception BinaryOperatorNotDefined(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACD76E4  token: 0x6000364
        private static System.Exception ReferenceEqualityNotDefined(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD70E0  token: 0x6000365
        private static System.Exception OperandTypesDoNotMatchParameters(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD71D4  token: 0x6000366
        private static System.Exception OverloadOperatorTypeDoesNotMatchConversionType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD5204  token: 0x6000367
        private static System.Exception ConversionIsNotSupportedForArithmeticTypes() { }
        // RVA: 0x0ACD4848  token: 0x6000368
        private static System.Exception ArgumentMustBeArray(System.String paramName) { }
        // RVA: 0x0ACD48C0  token: 0x6000369
        private static System.Exception ArgumentMustBeBoolean(System.String paramName) { }
        // RVA: 0x0ACD4938  token: 0x600036A
        private static System.Exception ArgumentMustBeInteger(System.String paramName) { }
        // RVA: 0x0ACD49B0  token: 0x600036B
        private static System.Exception ArgumentMustBeInteger(System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD47D0  token: 0x600036C
        private static System.Exception ArgumentMustBeArrayIndexType(System.String paramName) { }
        // RVA: 0x0ACD49CC  token: 0x600036D
        private static System.Exception ArgumentMustBeSingleDimensionalArrayType(System.String paramName) { }
        // RVA: 0x0ACD4B1C  token: 0x600036E
        private static System.Exception ArgumentTypesMustMatch() { }
        // RVA: 0x0ACD4E4C  token: 0x600036F
        private static System.Exception CannotAutoInitializeValueTypeMemberThroughProperty(System.Object p0) { }
        // RVA: 0x0ACD5FA4  token: 0x6000370
        private static System.Exception IncorrectTypeForTypeAs(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD4EB8  token: 0x6000371
        private static System.Exception CoalesceUsedOnNonNullType() { }
        // RVA: 0x0ACD561C  token: 0x6000372
        private static System.Exception ExpressionTypeCannotInitializeArrayType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD59DC  token: 0x6000373
        private static System.Exception ExpressionTypeDoesNotMatchReturn(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD5690  token: 0x6000374
        private static System.Exception ExpressionTypeDoesNotMatchAssignment(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD57C4  token: 0x6000375
        private static System.Exception ExpressionTypeDoesNotMatchLabel(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD5A50  token: 0x6000376
        private static System.Exception ExpressionTypeNotInvocable(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD611C  token: 0x6000377
        private static System.Exception InstanceFieldNotDefinedForType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD5BB0  token: 0x6000378
        private static System.Exception FieldInfoNotDefinedForType(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACD5DF0  token: 0x6000379
        private static System.Exception IncorrectNumberOfIndexes() { }
        // RVA: 0x0ACD5EC0  token: 0x600037A
        private static System.Exception IncorrectNumberOfLambdaDeclarationParameters() { }
        // RVA: 0x0ACD67A8  token: 0x600037B
        private static System.Exception LambdaTypeMustBeDerivedFromSystemDelegate(System.String paramName) { }
        // RVA: 0x0ACD6894  token: 0x600037C
        private static System.Exception MemberNotFieldOrProperty(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD6910  token: 0x600037D
        private static System.Exception MethodContainsGenericParameters(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD698C  token: 0x600037E
        private static System.Exception MethodIsGeneric(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD6A08  token: 0x600037F
        private static System.Exception MethodNotPropertyAccessor(System.Object p0, System.Object p1, System.String paramName) { }
        // RVA: 0x0ACD6A8C  token: 0x6000380
        private static System.Exception MethodNotPropertyAccessor(System.Object p0, System.Object p1, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD7334  token: 0x6000381
        private static System.Exception PropertyDoesNotHaveAccessor(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD7248  token: 0x6000382
        private static System.Exception ParameterExpressionNotValidAsDelegate(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD73B0  token: 0x6000383
        private static System.Exception PropertyNotDefinedForType(System.Object p0, System.Object p1, System.String paramName) { }
        // RVA: 0x0ACD6190  token: 0x6000384
        private static System.Exception InstancePropertyNotDefinedForType(System.Object p0, System.Object p1, System.String paramName) { }
        // RVA: 0x0ACD6098  token: 0x6000385
        private static System.Exception InstanceAndMethodTypeMismatch(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACD7D28  token: 0x6000386
        private static System.Exception UnhandledBinary(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD7DA4  token: 0x6000387
        private static System.Exception UnhandledUnary(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD7E20  token: 0x6000388
        private static System.Exception UserDefinedOpMustHaveConsistentTypes(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD7E94  token: 0x6000389
        private static System.Exception UserDefinedOpMustHaveValidReturnType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD6820  token: 0x600038A
        private static System.Exception LogicalOperatorMustHaveBooleanOperators(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD6AC8  token: 0x600038B
        private static System.Exception MethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD5C9C  token: 0x600038C
        private static System.Exception GenericMethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD6B3C  token: 0x600038D
        private static System.Exception MethodWithMoreThanOneMatch(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD4758  token: 0x600038E
        private static System.Exception ArgumentCannotBeOfTypeVoid(System.String paramName) { }
        // RVA: 0x0ACD7154  token: 0x600038F
        private static System.Exception OutOfRange(System.String paramName, System.Object p1) { }
        // RVA: 0x0ACD6658  token: 0x6000390
        private static System.Exception LabelTargetAlreadyDefined(System.Object p0) { }
        // RVA: 0x0ACD66C4  token: 0x6000391
        private static System.Exception LabelTargetUndefined(System.Object p0) { }
        // RVA: 0x0ACD519C  token: 0x6000392
        private static System.Exception ControlCannotLeaveFinally() { }
        // RVA: 0x0ACD5134  token: 0x6000393
        private static System.Exception ControlCannotLeaveFilterTest() { }
        // RVA: 0x0ACD4680  token: 0x6000394
        private static System.Exception AmbiguousJump(System.Object p0) { }
        // RVA: 0x0ACD50CC  token: 0x6000395
        private static System.Exception ControlCannotEnterTry() { }
        // RVA: 0x0ACD5064  token: 0x6000396
        private static System.Exception ControlCannotEnterExpression() { }
        // RVA: 0x0ACD6ECC  token: 0x6000397
        private static System.Exception NonLocalJumpWithValue(System.Object p0) { }
        // RVA: 0x0ACD628C  token: 0x6000398
        private static System.Exception InvalidLvalue(System.Linq.Expressions.ExpressionType p0) { }
        // RVA: 0x0ACD7758  token: 0x6000399
        private static System.Exception RethrowRequiresCatch() { }
        // RVA: 0x0ACD6D04  token: 0x600039A
        private static System.Exception MustRewriteToSameNode(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACD6C80  token: 0x600039B
        private static System.Exception MustRewriteChildToSameType(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACD6D88  token: 0x600039C
        private static System.Exception MustRewriteWithoutMethod(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACD4ABC  token: 0x600039D
        private static System.Exception ArgumentOutOfRange(System.String paramName) { }
        // RVA: 0x0ACD6F38  token: 0x600039E
        private static System.Exception NotSupported() { }
        // RVA: 0x0ACD6E64  token: 0x600039F
        private static System.Exception NonAbstractConstructorRequired() { }
        // RVA: 0x0ACD6400  token: 0x60003A0
        private static System.Exception InvalidProgram() { }
        // RVA: 0x0ACD54A8  token: 0x60003A1
        private static System.Exception EnumerationIsDone() { }
        // RVA: 0x0ACD79C8  token: 0x60003A2
        private static System.Exception TypeContainsGenericParameters(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD7998  token: 0x60003A3
        private static System.Exception TypeContainsGenericParameters(System.Object p0, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD7A44  token: 0x60003A4
        private static System.Exception TypeIsGeneric(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD7AC0  token: 0x60003A5
        private static System.Exception TypeIsGeneric(System.Object p0, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD5D88  token: 0x60003A6
        private static System.Exception IncorrectNumberOfConstructorArguments() { }
        // RVA: 0x0ACD5838  token: 0x60003A7
        private static System.Exception ExpressionTypeDoesNotMatchMethodParameter(System.Object p0, System.Object p1, System.Object p2, System.String paramName) { }
        // RVA: 0x0ACD58CC  token: 0x60003A8
        private static System.Exception ExpressionTypeDoesNotMatchMethodParameter(System.Object p0, System.Object p1, System.Object p2, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD5958  token: 0x60003A9
        private static System.Exception ExpressionTypeDoesNotMatchParameter(System.Object p0, System.Object p1, System.String paramName) { }
        // RVA: 0x0ACD591C  token: 0x60003AA
        private static System.Exception ExpressionTypeDoesNotMatchParameter(System.Object p0, System.Object p1, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD5E58  token: 0x60003AB
        private static System.Exception IncorrectNumberOfLambdaArguments() { }
        // RVA: 0x0ACD5F28  token: 0x60003AC
        private static System.Exception IncorrectNumberOfMethodCallArguments(System.Object p0, System.String paramName) { }
        // RVA: 0x0ACD5704  token: 0x60003AD
        private static System.Exception ExpressionTypeDoesNotMatchConstructorParameter(System.Object p0, System.Object p1, System.String paramName) { }
        // RVA: 0x0ACD5788  token: 0x60003AE
        private static System.Exception ExpressionTypeDoesNotMatchConstructorParameter(System.Object p0, System.Object p1, System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD5510  token: 0x60003AF
        private static System.Exception ExpressionMustBeReadable(System.String paramName) { }
        // RVA: 0x0ACD5588  token: 0x60003B0
        private static System.Exception ExpressionMustBeReadable(System.String paramName, System.Int32 index) { }
        // RVA: 0x0ACD6214  token: 0x60003B1
        private static System.Exception InvalidArgumentValue(System.String paramName) { }
        // RVA: 0x0ACD6384  token: 0x60003B2
        private static System.Exception InvalidNullValue(System.Type type, System.String paramName) { }
        // RVA: 0x0ACD6450  token: 0x60003B3
        private static System.Exception InvalidTypeException(System.Object value, System.Type type, System.String paramName) { }
        // RVA: 0x0ACD5D10  token: 0x60003B4
        private static System.String GetParamName(System.String paramName, System.Int32 index) { }

    }

    // TypeToken: 0x2000084  // size: 0x20
    public sealed class ExpressionStringBuilder : System.Linq.Expressions.ExpressionVisitor
    {
        // Fields
        private readonly System.Text.StringBuilder _out;  // 0x10
        private System.Collections.Generic.Dictionary<System.Object,System.Int32> _ids;  // 0x18

        // Methods
        // RVA: 0x0ACDA680  token: 0x60003B5
        private System.Void .ctor() { }
        // RVA: 0x09043210  token: 0x60003B6
        public virtual System.String ToString() { }
        // RVA: 0x0ACD8ADC  token: 0x60003B7
        private System.Int32 GetLabelId(System.Linq.Expressions.LabelTarget label) { }
        // RVA: 0x0ACD8ADC  token: 0x60003B8
        private System.Int32 GetParamId(System.Linq.Expressions.ParameterExpression p) { }
        // RVA: 0x0ACD89D4  token: 0x60003B9
        private System.Int32 GetId(System.Object o) { }
        // RVA: 0x0ACD8C40  token: 0x60003BA
        private System.Void Out(System.String s) { }
        // RVA: 0x0ACD8C60  token: 0x60003BB
        private System.Void Out(System.Char c) { }
        // RVA: 0x0ACD8960  token: 0x60003BC
        private static System.String ExpressionToString(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACD884C  token: 0x60003BD
        private static System.String CatchBlockToString(System.Linq.Expressions.CatchBlock node) { }
        // RVA: -1  // generic def  token: 0x60003BE
        private System.Void VisitExpressions(System.Char open, System.Collections.ObjectModel.ReadOnlyCollection<T> expressions, System.Char close) { }
        // RVA: -1  // generic def  token: 0x60003BF
        private System.Void VisitExpressions(System.Char open, System.Collections.ObjectModel.ReadOnlyCollection<T> expressions, System.Char close, System.String seperator) { }
        // RVA: 0x0ACD8C80  token: 0x60003C0
        protected internal virtual System.Linq.Expressions.Expression VisitBinary(System.Linq.Expressions.BinaryExpression node) { }
        // RVA: 0x0ACDA11C  token: 0x60003C1
        protected internal virtual System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node) { }
        // RVA: -1  // generic def  token: 0x60003C2
        protected internal virtual System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node) { }
        // RVA: 0x0ACD9564  token: 0x60003C3
        protected internal virtual System.Linq.Expressions.Expression VisitConditional(System.Linq.Expressions.ConditionalExpression node) { }
        // RVA: 0x0ACD9634  token: 0x60003C4
        protected internal virtual System.Linq.Expressions.Expression VisitConstant(System.Linq.Expressions.ConstantExpression node) { }
        // RVA: 0x0ACD8BB0  token: 0x60003C5
        private System.Void OutMember(System.Linq.Expressions.Expression instance, System.Reflection.MemberInfo member) { }
        // RVA: 0x0ACD9E3C  token: 0x60003C6
        protected internal virtual System.Linq.Expressions.Expression VisitMember(System.Linq.Expressions.MemberExpression node) { }
        // RVA: 0x0ACD9C88  token: 0x60003C7
        protected internal virtual System.Linq.Expressions.Expression VisitInvocation(System.Linq.Expressions.InvocationExpression node) { }
        // RVA: 0x0ACD9E90  token: 0x60003C8
        protected internal virtual System.Linq.Expressions.Expression VisitMethodCall(System.Linq.Expressions.MethodCallExpression node) { }
        // RVA: 0x0ACDA024  token: 0x60003C9
        protected internal virtual System.Linq.Expressions.Expression VisitNewArray(System.Linq.Expressions.NewArrayExpression node) { }
        // RVA: 0x0ACDA228  token: 0x60003CA
        protected internal virtual System.Linq.Expressions.Expression VisitTypeBinary(System.Linq.Expressions.TypeBinaryExpression node) { }
        // RVA: 0x0ACDA300  token: 0x60003CB
        protected internal virtual System.Linq.Expressions.Expression VisitUnary(System.Linq.Expressions.UnaryExpression node) { }
        // RVA: 0x0ACD92D4  token: 0x60003CC
        protected internal virtual System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node) { }
        // RVA: 0x0ACD977C  token: 0x60003CD
        protected internal virtual System.Linq.Expressions.Expression VisitDefault(System.Linq.Expressions.DefaultExpression node) { }
        // RVA: 0x0ACD9D78  token: 0x60003CE
        protected internal virtual System.Linq.Expressions.Expression VisitLabel(System.Linq.Expressions.LabelExpression node) { }
        // RVA: 0x0ACD99C4  token: 0x60003CF
        protected internal virtual System.Linq.Expressions.Expression VisitGoto(System.Linq.Expressions.GotoExpression node) { }
        // RVA: 0x0ACD9DF0  token: 0x60003D0
        protected internal virtual System.Linq.Expressions.Expression VisitLoop(System.Linq.Expressions.LoopExpression node) { }
        // RVA: 0x0ACD9488  token: 0x60003D1
        protected virtual System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node) { }
        // RVA: 0x0ACDA1DC  token: 0x60003D2
        protected internal virtual System.Linq.Expressions.Expression VisitTry(System.Linq.Expressions.TryExpression node) { }
        // RVA: 0x0ACD9B28  token: 0x60003D3
        protected internal virtual System.Linq.Expressions.Expression VisitIndex(System.Linq.Expressions.IndexExpression node) { }
        // RVA: 0x0ACD9810  token: 0x60003D4
        protected internal virtual System.Linq.Expressions.Expression VisitExtension(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACD88D0  token: 0x60003D5
        private System.Void DumpLabel(System.Linq.Expressions.LabelTarget target) { }
        // RVA: 0x0ACD8AE4  token: 0x60003D6
        private static System.Boolean IsBool(System.Linq.Expressions.Expression node) { }

    }

    // TypeToken: 0x2000085  // size: 0x14
    public sealed struct ExpressionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Linq.Expressions.ExpressionType Add;  // const
        public static System.Linq.Expressions.ExpressionType AddChecked;  // const
        public static System.Linq.Expressions.ExpressionType And;  // const
        public static System.Linq.Expressions.ExpressionType AndAlso;  // const
        public static System.Linq.Expressions.ExpressionType ArrayLength;  // const
        public static System.Linq.Expressions.ExpressionType ArrayIndex;  // const
        public static System.Linq.Expressions.ExpressionType Call;  // const
        public static System.Linq.Expressions.ExpressionType Coalesce;  // const
        public static System.Linq.Expressions.ExpressionType Conditional;  // const
        public static System.Linq.Expressions.ExpressionType Constant;  // const
        public static System.Linq.Expressions.ExpressionType Convert;  // const
        public static System.Linq.Expressions.ExpressionType ConvertChecked;  // const
        public static System.Linq.Expressions.ExpressionType Divide;  // const
        public static System.Linq.Expressions.ExpressionType Equal;  // const
        public static System.Linq.Expressions.ExpressionType ExclusiveOr;  // const
        public static System.Linq.Expressions.ExpressionType GreaterThan;  // const
        public static System.Linq.Expressions.ExpressionType GreaterThanOrEqual;  // const
        public static System.Linq.Expressions.ExpressionType Invoke;  // const
        public static System.Linq.Expressions.ExpressionType Lambda;  // const
        public static System.Linq.Expressions.ExpressionType LeftShift;  // const
        public static System.Linq.Expressions.ExpressionType LessThan;  // const
        public static System.Linq.Expressions.ExpressionType LessThanOrEqual;  // const
        public static System.Linq.Expressions.ExpressionType ListInit;  // const
        public static System.Linq.Expressions.ExpressionType MemberAccess;  // const
        public static System.Linq.Expressions.ExpressionType MemberInit;  // const
        public static System.Linq.Expressions.ExpressionType Modulo;  // const
        public static System.Linq.Expressions.ExpressionType Multiply;  // const
        public static System.Linq.Expressions.ExpressionType MultiplyChecked;  // const
        public static System.Linq.Expressions.ExpressionType Negate;  // const
        public static System.Linq.Expressions.ExpressionType UnaryPlus;  // const
        public static System.Linq.Expressions.ExpressionType NegateChecked;  // const
        public static System.Linq.Expressions.ExpressionType New;  // const
        public static System.Linq.Expressions.ExpressionType NewArrayInit;  // const
        public static System.Linq.Expressions.ExpressionType NewArrayBounds;  // const
        public static System.Linq.Expressions.ExpressionType Not;  // const
        public static System.Linq.Expressions.ExpressionType NotEqual;  // const
        public static System.Linq.Expressions.ExpressionType Or;  // const
        public static System.Linq.Expressions.ExpressionType OrElse;  // const
        public static System.Linq.Expressions.ExpressionType Parameter;  // const
        public static System.Linq.Expressions.ExpressionType Power;  // const
        public static System.Linq.Expressions.ExpressionType Quote;  // const
        public static System.Linq.Expressions.ExpressionType RightShift;  // const
        public static System.Linq.Expressions.ExpressionType Subtract;  // const
        public static System.Linq.Expressions.ExpressionType SubtractChecked;  // const
        public static System.Linq.Expressions.ExpressionType TypeAs;  // const
        public static System.Linq.Expressions.ExpressionType TypeIs;  // const
        public static System.Linq.Expressions.ExpressionType Assign;  // const
        public static System.Linq.Expressions.ExpressionType Block;  // const
        public static System.Linq.Expressions.ExpressionType DebugInfo;  // const
        public static System.Linq.Expressions.ExpressionType Decrement;  // const
        public static System.Linq.Expressions.ExpressionType Dynamic;  // const
        public static System.Linq.Expressions.ExpressionType Default;  // const
        public static System.Linq.Expressions.ExpressionType Extension;  // const
        public static System.Linq.Expressions.ExpressionType Goto;  // const
        public static System.Linq.Expressions.ExpressionType Increment;  // const
        public static System.Linq.Expressions.ExpressionType Index;  // const
        public static System.Linq.Expressions.ExpressionType Label;  // const
        public static System.Linq.Expressions.ExpressionType RuntimeVariables;  // const
        public static System.Linq.Expressions.ExpressionType Loop;  // const
        public static System.Linq.Expressions.ExpressionType Switch;  // const
        public static System.Linq.Expressions.ExpressionType Throw;  // const
        public static System.Linq.Expressions.ExpressionType Try;  // const
        public static System.Linq.Expressions.ExpressionType Unbox;  // const
        public static System.Linq.Expressions.ExpressionType AddAssign;  // const
        public static System.Linq.Expressions.ExpressionType AndAssign;  // const
        public static System.Linq.Expressions.ExpressionType DivideAssign;  // const
        public static System.Linq.Expressions.ExpressionType ExclusiveOrAssign;  // const
        public static System.Linq.Expressions.ExpressionType LeftShiftAssign;  // const
        public static System.Linq.Expressions.ExpressionType ModuloAssign;  // const
        public static System.Linq.Expressions.ExpressionType MultiplyAssign;  // const
        public static System.Linq.Expressions.ExpressionType OrAssign;  // const
        public static System.Linq.Expressions.ExpressionType PowerAssign;  // const
        public static System.Linq.Expressions.ExpressionType RightShiftAssign;  // const
        public static System.Linq.Expressions.ExpressionType SubtractAssign;  // const
        public static System.Linq.Expressions.ExpressionType AddAssignChecked;  // const
        public static System.Linq.Expressions.ExpressionType MultiplyAssignChecked;  // const
        public static System.Linq.Expressions.ExpressionType SubtractAssignChecked;  // const
        public static System.Linq.Expressions.ExpressionType PreIncrementAssign;  // const
        public static System.Linq.Expressions.ExpressionType PreDecrementAssign;  // const
        public static System.Linq.Expressions.ExpressionType PostIncrementAssign;  // const
        public static System.Linq.Expressions.ExpressionType PostDecrementAssign;  // const
        public static System.Linq.Expressions.ExpressionType TypeEqual;  // const
        public static System.Linq.Expressions.ExpressionType OnesComplement;  // const
        public static System.Linq.Expressions.ExpressionType IsTrue;  // const
        public static System.Linq.Expressions.ExpressionType IsFalse;  // const

    }

    // TypeToken: 0x2000086  // size: 0x10
    public abstract class ExpressionVisitor
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60003D7
        protected System.Void .ctor() { }
        // RVA: 0x0ACDB150  token: 0x60003D8
        public virtual System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACDB198  token: 0x60003D9
        public System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> Visit(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> nodes) { }
        // RVA: 0x0ACDA9F4  token: 0x60003DA
        private System.Linq.Expressions.Expression[] VisitArguments(System.Linq.Expressions.IArgumentProvider nodes) { }
        // RVA: 0x0ACDAFC0  token: 0x60003DB
        private System.Linq.Expressions.ParameterExpression[] VisitParameters(System.Linq.Expressions.IParameterProvider nodes, System.String callerName) { }
        // RVA: -1  // generic def  token: 0x60003DC
        public static System.Collections.ObjectModel.ReadOnlyCollection<T> Visit(System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, System.Func<T,T> elementVisitor) { }
        // RVA: -1  // generic def  token: 0x60003DD
        public T VisitAndConvert(T node, System.String callerName) { }
        // RVA: -1  // generic def  token: 0x60003DE
        public System.Collections.ObjectModel.ReadOnlyCollection<T> VisitAndConvert(System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, System.String callerName) { }
        // RVA: 0x0ACDA9FC  token: 0x60003DF
        protected internal virtual System.Linq.Expressions.Expression VisitBinary(System.Linq.Expressions.BinaryExpression node) { }
        // RVA: 0x0ACDAAC8  token: 0x60003E0
        protected internal virtual System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node) { }
        // RVA: 0x0ACDAC38  token: 0x60003E1
        protected internal virtual System.Linq.Expressions.Expression VisitConditional(System.Linq.Expressions.ConditionalExpression node) { }
        // RVA: 0x04D88C30  token: 0x60003E2
        protected internal virtual System.Linq.Expressions.Expression VisitConstant(System.Linq.Expressions.ConstantExpression node) { }
        // RVA: 0x04D88C30  token: 0x60003E3
        protected internal virtual System.Linq.Expressions.Expression VisitDefault(System.Linq.Expressions.DefaultExpression node) { }
        // RVA: 0x0ACDACC4  token: 0x60003E4
        protected internal virtual System.Linq.Expressions.Expression VisitExtension(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACDACE8  token: 0x60003E5
        protected internal virtual System.Linq.Expressions.Expression VisitGoto(System.Linq.Expressions.GotoExpression node) { }
        // RVA: 0x0ACDAD9C  token: 0x60003E6
        protected internal virtual System.Linq.Expressions.Expression VisitInvocation(System.Linq.Expressions.InvocationExpression node) { }
        // RVA: 0x04D88C30  token: 0x60003E7
        protected virtual System.Linq.Expressions.LabelTarget VisitLabelTarget(System.Linq.Expressions.LabelTarget node) { }
        // RVA: 0x0ACDAE20  token: 0x60003E8
        protected internal virtual System.Linq.Expressions.Expression VisitLabel(System.Linq.Expressions.LabelExpression node) { }
        // RVA: -1  // generic def  token: 0x60003E9
        protected internal virtual System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node) { }
        // RVA: 0x0ACDAE64  token: 0x60003EA
        protected internal virtual System.Linq.Expressions.Expression VisitLoop(System.Linq.Expressions.LoopExpression node) { }
        // RVA: 0x0ACDAEBC  token: 0x60003EB
        protected internal virtual System.Linq.Expressions.Expression VisitMember(System.Linq.Expressions.MemberExpression node) { }
        // RVA: 0x0ACDAD2C  token: 0x60003EC
        protected internal virtual System.Linq.Expressions.Expression VisitIndex(System.Linq.Expressions.IndexExpression node) { }
        // RVA: 0x0ACDAEF0  token: 0x60003ED
        protected internal virtual System.Linq.Expressions.Expression VisitMethodCall(System.Linq.Expressions.MethodCallExpression node) { }
        // RVA: 0x0ACDAF8C  token: 0x60003EE
        protected internal virtual System.Linq.Expressions.Expression VisitNewArray(System.Linq.Expressions.NewArrayExpression node) { }
        // RVA: 0x04D88C30  token: 0x60003EF
        protected internal virtual System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node) { }
        // RVA: 0x0ACDAB80  token: 0x60003F0
        protected virtual System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node) { }
        // RVA: 0x0ACDAFC8  token: 0x60003F1
        protected internal virtual System.Linq.Expressions.Expression VisitTry(System.Linq.Expressions.TryExpression node) { }
        // RVA: 0x0ACDB0D8  token: 0x60003F2
        protected internal virtual System.Linq.Expressions.Expression VisitTypeBinary(System.Linq.Expressions.TypeBinaryExpression node) { }
        // RVA: 0x0ACDB10C  token: 0x60003F3
        protected internal virtual System.Linq.Expressions.Expression VisitUnary(System.Linq.Expressions.UnaryExpression node) { }
        // RVA: 0x0ACDA904  token: 0x60003F4
        private static System.Linq.Expressions.UnaryExpression ValidateUnary(System.Linq.Expressions.UnaryExpression before, System.Linq.Expressions.UnaryExpression after) { }
        // RVA: 0x0ACDA6E4  token: 0x60003F5
        private static System.Linq.Expressions.BinaryExpression ValidateBinary(System.Linq.Expressions.BinaryExpression before, System.Linq.Expressions.BinaryExpression after) { }
        // RVA: 0x0ACDA848  token: 0x60003F6
        private static System.Void ValidateChildType(System.Type before, System.Type after, System.String methodName) { }

    }

    // TypeToken: 0x2000087  // size: 0x14
    public sealed struct GotoExpressionKind
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Linq.Expressions.GotoExpressionKind Goto;  // const
        public static System.Linq.Expressions.GotoExpressionKind Return;  // const
        public static System.Linq.Expressions.GotoExpressionKind Break;  // const
        public static System.Linq.Expressions.GotoExpressionKind Continue;  // const

    }

    // TypeToken: 0x2000088  // size: 0x30
    public sealed class GotoExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <Value>k__BackingField;  // 0x18
        private readonly System.Linq.Expressions.LabelTarget <Target>k__BackingField;  // 0x20
        private readonly System.Linq.Expressions.GotoExpressionKind <Kind>k__BackingField;  // 0x28

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA9F80 */ }
        System.Linq.Expressions.Expression Value { get; /* RVA: 0x0385B100 */ }
        System.Linq.Expressions.LabelTarget Target { get; /* RVA: 0x04D862C0 */ }
        System.Linq.Expressions.GotoExpressionKind Kind { get; /* RVA: 0x011F2A90 */ }

        // Methods
        // RVA: 0x0ACDB644  token: 0x60003F7
        private System.Void .ctor(System.Linq.Expressions.GotoExpressionKind kind, System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value, System.Type type) { }
        // RVA: 0x0ACDB568  token: 0x60003FD
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDB5B0  token: 0x60003FE
        public System.Linq.Expressions.GotoExpression Update(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value) { }

    }

    // TypeToken: 0x2000089
    public interface IArgumentProvider
    {
        // Properties
        System.Int32 ArgumentCount { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60003FF
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }

    }

    // TypeToken: 0x200008A
    public interface IParameterProvider
    {
        // Properties
        System.Int32 ParameterCount { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000401
        public virtual System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index) { }

    }

    // TypeToken: 0x200008B  // size: 0x28
    public sealed class IndexExpression : System.Linq.Expressions.Expression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> _arguments;  // 0x10
        private readonly System.Linq.Expressions.Expression <Object>k__BackingField;  // 0x18
        private readonly System.Reflection.PropertyInfo <Indexer>k__BackingField;  // 0x20

        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA9FB0 */ }
        System.Type Type { get; /* RVA: 0x0ACDBB44 */ }
        System.Linq.Expressions.Expression Object { get; /* RVA: 0x0385B100 */ }
        System.Reflection.PropertyInfo Indexer { get; /* RVA: 0x04D862C0 */ }
        System.Int32 ArgumentCount { get; /* RVA: 0x0ACDBAFC */ }

        // Methods
        // RVA: 0x0ACDBA6C  token: 0x6000403
        private System.Void .ctor(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> arguments) { }
        // RVA: 0x0ACDB9A0  token: 0x6000408
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDB958  token: 0x600040A
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDB9F4  token: 0x600040B
        private System.Linq.Expressions.Expression Rewrite(System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x200008C  // size: 0x20
    public class InvocationExpression : System.Linq.Expressions.Expression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <Expression>k__BackingField;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA5E90 */ }
        System.Linq.Expressions.Expression Expression { get; /* RVA: 0x0385B100 */ }
        System.Int32 ArgumentCount { get; /* RVA: 0x0ACDD0B4 */ }

        // Methods
        // RVA: 0x0ACDD048  token: 0x600040C
        private System.Void .ctor(System.Linq.Expressions.Expression expression, System.Type returnType) { }
        // RVA: 0x0ACDCFF8  token: 0x6000410
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDCFB0  token: 0x6000412
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDD020  token: 0x6000413
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x200008D  // size: 0x28
    public sealed class InvocationExpressionN : System.Linq.Expressions.InvocationExpression
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> _arguments;  // 0x20

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x0ACDCF68 */ }

        // Methods
        // RVA: 0x0ACDCF30  token: 0x6000414
        public System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> arguments, System.Type returnType) { }
        // RVA: 0x0ACDCE74  token: 0x6000415
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDCEC8  token: 0x6000417
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x200008E  // size: 0x20
    public sealed class InvocationExpression0 : System.Linq.Expressions.InvocationExpression
    {
        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0ACDC514  token: 0x6000418
        public System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType) { }
        // RVA: 0x0ACDC46C  token: 0x6000419
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDC4C8  token: 0x600041B
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x200008F  // size: 0x28
    public sealed class InvocationExpression1 : System.Linq.Expressions.InvocationExpression
    {
        // Fields
        private System.Object _arg0;  // 0x20

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0ACDC650  token: 0x600041C
        public System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0) { }
        // RVA: 0x0ACDC51C  token: 0x600041D
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDC5BC  token: 0x600041F
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x2000090  // size: 0x30
    public sealed class InvocationExpression2 : System.Linq.Expressions.InvocationExpression
    {
        // Fields
        private System.Object _arg0;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x28

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x0ACDC7DC  token: 0x6000420
        public System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACDC680  token: 0x6000421
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDC72C  token: 0x6000423
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x2000091  // size: 0x38
    public sealed class InvocationExpression3 : System.Linq.Expressions.InvocationExpression
    {
        // Fields
        private System.Object _arg0;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x28
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x30

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D8C9E0 */ }

        // Methods
        // RVA: 0x0ACDC9A4  token: 0x6000424
        public System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACDC820  token: 0x6000425
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDC8D4  token: 0x6000427
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x2000092  // size: 0x40
    public sealed class InvocationExpression4 : System.Linq.Expressions.InvocationExpression
    {
        // Fields
        private System.Object _arg0;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x28
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x30
        private readonly System.Linq.Expressions.Expression _arg3;  // 0x38

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D882B0 */ }

        // Methods
        // RVA: 0x0ACDCBA8  token: 0x6000428
        public System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3) { }
        // RVA: 0x0ACDC9F8  token: 0x6000429
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDCAB8  token: 0x600042B
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x2000093  // size: 0x48
    public sealed class InvocationExpression5 : System.Linq.Expressions.InvocationExpression
    {
        // Fields
        private System.Object _arg0;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x28
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x30
        private readonly System.Linq.Expressions.Expression _arg3;  // 0x38
        private readonly System.Linq.Expressions.Expression _arg4;  // 0x40

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D9EC50 */ }

        // Methods
        // RVA: 0x0ACDCDFC  token: 0x600042C
        public System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4) { }
        // RVA: 0x0ACDCC10  token: 0x600042D
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDCCE0  token: 0x600042F
        private virtual System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments) { }

    }

    // TypeToken: 0x2000094  // size: 0x20
    public sealed class LabelExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.LabelTarget <Target>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <DefaultValue>k__BackingField;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x0861D7BC */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA9FC0 */ }
        System.Linq.Expressions.LabelTarget Target { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.Expression DefaultValue { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0ACDD194  token: 0x6000430
        private System.Void .ctor(System.Linq.Expressions.LabelTarget label, System.Linq.Expressions.Expression defaultValue) { }
        // RVA: 0x0ACDD0DC  token: 0x6000435
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDD124  token: 0x6000436
        public System.Linq.Expressions.LabelExpression Update(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression defaultValue) { }

    }

    // TypeToken: 0x2000095  // size: 0x20
    public sealed class LabelTarget
    {
        // Fields
        private readonly System.String <Name>k__BackingField;  // 0x10
        private readonly System.Type <Type>k__BackingField;  // 0x18

        // Properties
        System.String Name { get; /* RVA: 0x02B2ECC0 */ }
        System.Type Type { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03739160  token: 0x6000437
        private System.Void .ctor(System.Type type, System.String name) { }
        // RVA: 0x0ACDD200  token: 0x600043A
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000096  // size: 0x18
    public abstract class LambdaExpression : System.Linq.Expressions.Expression, System.Linq.Expressions.IParameterProvider
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _body;  // 0x10

        // Properties
        System.Type Type { get; /* RVA: 0x0ACDD390 */ }
        System.Type TypeCore { get; /* RVA: -1  // abstract */ }
        System.Type PublicType { get; /* RVA: -1  // abstract */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA3EE0 */ }
        System.String Name { get; /* RVA: 0x0ACDD2E0 */ }
        System.String NameCore { get; /* RVA: 0x011EC580 */ }
        System.Linq.Expressions.Expression Body { get; /* RVA: 0x02B2ECC0 */ }
        System.Type ReturnType { get; /* RVA: 0x0ACDD318 */ }
        System.Boolean TailCall { get; /* RVA: 0x0ACDD380 */ }
        System.Boolean TailCallCore { get; /* RVA: 0x012081B0 */ }
        System.Int32 System.Linq.Expressions.IParameterProvider.ParameterCount { get; /* RVA: 0x0ACDD278 */ }
        System.Int32 ParameterCount { get; /* RVA: 0x0ACDD2F0 */ }

        // Methods
        // RVA: 0x0ACDD288  token: 0x600043B
        private System.Void .ctor(System.Linq.Expressions.Expression body) { }
        // RVA: 0x0ACDD26C  token: 0x6000446
        private virtual System.Linq.Expressions.ParameterExpression System.Linq.Expressions.IParameterProvider.GetParameter(System.Int32 index) { }
        // RVA: 0x0ACDD244  token: 0x6000447
        private virtual System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index) { }

    }

    // TypeToken: 0x2000097
    public class Expression`1 : System.Linq.Expressions.LambdaExpression
    {
        // Properties
        System.Type TypeCore { get; /* RVA: -1  // not resolved */ }
        System.Type PublicType { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600044A
        private System.Void .ctor(System.Linq.Expressions.Expression body) { }
        // RVA: -1  // not resolved  token: 0x600044D
        public TDelegate Compile() { }
        // RVA: -1  // not resolved  token: 0x600044E
        public TDelegate Compile(System.Boolean preferInterpretation) { }
        // RVA: -1  // not resolved  token: 0x600044F
        private virtual System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }
        // RVA: -1  // not resolved  token: 0x6000450
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }

    }

    // TypeToken: 0x2000098
    public static class ExpressionCreator`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000451
        public static System.Linq.Expressions.LambdaExpression CreateExpressionFunc(System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters) { }

    }

    // TypeToken: 0x2000099
    public sealed class Expression0`1 : System.Linq.Expressions.Expression`1
    {
        // Properties
        System.Int32 ParameterCount { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000452
        public System.Void .ctor(System.Linq.Expressions.Expression body) { }
        // RVA: -1  // not resolved  token: 0x6000454
        private virtual System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000455
        private virtual System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }

    }

    // TypeToken: 0x200009A
    public sealed class Expression1`1 : System.Linq.Expressions.Expression`1
    {
        // Fields
        private System.Object _par0;  // 0x0

        // Properties
        System.Int32 ParameterCount { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000456
        public System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression par0) { }
        // RVA: -1  // not resolved  token: 0x6000458
        private virtual System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000459
        private virtual System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }

    }

    // TypeToken: 0x200009B
    public sealed class Expression2`1 : System.Linq.Expressions.Expression`1
    {
        // Fields
        private System.Object _par0;  // 0x0
        private readonly System.Linq.Expressions.ParameterExpression _par1;  // 0x0

        // Properties
        System.Int32 ParameterCount { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600045A
        public System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression par0, System.Linq.Expressions.ParameterExpression par1) { }
        // RVA: -1  // not resolved  token: 0x600045C
        private virtual System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600045D
        private virtual System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }

    }

    // TypeToken: 0x200009C
    public sealed class Expression3`1 : System.Linq.Expressions.Expression`1
    {
        // Fields
        private System.Object _par0;  // 0x0
        private readonly System.Linq.Expressions.ParameterExpression _par1;  // 0x0
        private readonly System.Linq.Expressions.ParameterExpression _par2;  // 0x0

        // Properties
        System.Int32 ParameterCount { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600045E
        public System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression par0, System.Linq.Expressions.ParameterExpression par1, System.Linq.Expressions.ParameterExpression par2) { }
        // RVA: -1  // not resolved  token: 0x6000460
        private virtual System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000461
        private virtual System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }

    }

    // TypeToken: 0x200009D
    public class ExpressionN`1 : System.Linq.Expressions.Expression`1
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> _parameters;  // 0x0

        // Properties
        System.Int32 ParameterCount { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000462
        public System.Void .ctor(System.Linq.Expressions.Expression body, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> parameters) { }
        // RVA: -1  // not resolved  token: 0x6000464
        private virtual System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000465
        private virtual System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters) { }

    }

    // TypeToken: 0x200009E
    public sealed class FullExpression`1 : System.Linq.Expressions.ExpressionN`1
    {
        // Fields
        private readonly System.String <NameCore>k__BackingField;  // 0x0
        private readonly System.Boolean <TailCallCore>k__BackingField;  // 0x0

        // Properties
        System.String NameCore { get; /* RVA: -1  // not resolved */ }
        System.Boolean TailCallCore { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000466
        public System.Void .ctor(System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> parameters) { }

    }

    // TypeToken: 0x200009F  // size: 0x20
    public sealed class ListInitExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.NewExpression <NewExpression>k__BackingField;  // 0x10
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit> <Initializers>k__BackingField;  // 0x18

        // Properties
        System.Linq.Expressions.NewExpression NewExpression { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit> Initializers { get; /* RVA: 0x0385B100 */ }

    }

    // TypeToken: 0x20000A0  // size: 0x28
    public sealed class LoopExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.Expression <Body>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.LabelTarget <BreakLabel>k__BackingField;  // 0x18
        private readonly System.Linq.Expressions.LabelTarget <ContinueLabel>k__BackingField;  // 0x20

        // Properties
        System.Type Type { get; /* RVA: 0x0ACDD8D0 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA4DD0 */ }
        System.Linq.Expressions.Expression Body { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.LabelTarget BreakLabel { get; /* RVA: 0x0385B100 */ }
        System.Linq.Expressions.LabelTarget ContinueLabel { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0ACDD84C  token: 0x600046B
        private System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.LabelTarget break, System.Linq.Expressions.LabelTarget continue) { }
        // RVA: 0x0ACDD77C  token: 0x6000471
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDD7C4  token: 0x6000472
        public System.Linq.Expressions.LoopExpression Update(System.Linq.Expressions.LabelTarget breakLabel, System.Linq.Expressions.LabelTarget continueLabel, System.Linq.Expressions.Expression body) { }

    }

    // TypeToken: 0x20000A1  // size: 0x28
    public sealed class MemberAssignment : System.Linq.Expressions.MemberBinding
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _expression;  // 0x20

        // Properties
        System.Linq.Expressions.Expression Expression { get; /* RVA: 0x04D862C0 */ }

    }

    // TypeToken: 0x20000A2  // size: 0x14
    public sealed struct MemberBindingType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Linq.Expressions.MemberBindingType Assignment;  // const
        public static System.Linq.Expressions.MemberBindingType MemberBinding;  // const
        public static System.Linq.Expressions.MemberBindingType ListBinding;  // const

    }

    // TypeToken: 0x20000A3  // size: 0x20
    public abstract class MemberBinding
    {
        // Fields
        private readonly System.Linq.Expressions.MemberBindingType <BindingType>k__BackingField;  // 0x10
        private readonly System.Reflection.MemberInfo <Member>k__BackingField;  // 0x18

        // Properties
        System.Linq.Expressions.MemberBindingType BindingType { get; /* RVA: 0x02B2E2D0 */ }
        System.Reflection.MemberInfo Member { get; /* RVA: 0x0385B100 */ }

    }

    // TypeToken: 0x20000A4  // size: 0x18
    public class MemberExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.Expression <Expression>k__BackingField;  // 0x10

        // Properties
        System.Reflection.MemberInfo Member { get; /* RVA: 0x0ACDDB58 */ }
        System.Linq.Expressions.Expression Expression { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA4C00 */ }

        // Methods
        // RVA: 0x0ACDDB00  token: 0x6000478
        private System.Void .ctor(System.Linq.Expressions.Expression expression) { }
        // RVA: 0x0ACDDA24  token: 0x6000479
        private static System.Linq.Expressions.PropertyExpression Make(System.Linq.Expressions.Expression expression, System.Reflection.PropertyInfo property) { }
        // RVA: 0x0ACDD9B4  token: 0x600047A
        private static System.Linq.Expressions.FieldExpression Make(System.Linq.Expressions.Expression expression, System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACDD98C  token: 0x600047C
        private virtual System.Reflection.MemberInfo GetMember() { }
        // RVA: 0x0ACDD944  token: 0x600047D
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDDA94  token: 0x600047E
        public System.Linq.Expressions.MemberExpression Update(System.Linq.Expressions.Expression expression) { }

    }

    // TypeToken: 0x20000A5  // size: 0x20
    public sealed class FieldExpression : System.Linq.Expressions.MemberExpression
    {
        // Fields
        private readonly System.Reflection.FieldInfo _field;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x0ACDB3C0 */ }

        // Methods
        // RVA: 0x0ACDB390  token: 0x600047F
        public System.Void .ctor(System.Linq.Expressions.Expression expression, System.Reflection.FieldInfo member) { }
        // RVA: 0x0385B100  token: 0x6000480
        private virtual System.Reflection.MemberInfo GetMember() { }

    }

    // TypeToken: 0x20000A6  // size: 0x20
    public sealed class PropertyExpression : System.Linq.Expressions.MemberExpression
    {
        // Fields
        private readonly System.Reflection.PropertyInfo _property;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x0ACDFA08 */ }

        // Methods
        // RVA: 0x0ACDB390  token: 0x6000482
        public System.Void .ctor(System.Linq.Expressions.Expression expression, System.Reflection.PropertyInfo member) { }
        // RVA: 0x0385B100  token: 0x6000483
        private virtual System.Reflection.MemberInfo GetMember() { }

    }

    // TypeToken: 0x20000A7  // size: 0x20
    public sealed class MemberInitExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.NewExpression <NewExpression>k__BackingField;  // 0x10
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> <Bindings>k__BackingField;  // 0x18

        // Properties
        System.Linq.Expressions.NewExpression NewExpression { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> Bindings { get; /* RVA: 0x0385B100 */ }

    }

    // TypeToken: 0x20000A8  // size: 0x28
    public sealed class MemberListBinding : System.Linq.Expressions.MemberBinding
    {
        // Fields
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit> <Initializers>k__BackingField;  // 0x20

        // Properties
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit> Initializers { get; /* RVA: 0x04D862C0 */ }

    }

    // TypeToken: 0x20000A9  // size: 0x28
    public sealed class MemberMemberBinding : System.Linq.Expressions.MemberBinding
    {
        // Fields
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> <Bindings>k__BackingField;  // 0x20

        // Properties
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> Bindings { get; /* RVA: 0x04D862C0 */ }

    }

    // TypeToken: 0x20000AA  // size: 0x18
    public class MethodCallExpression : System.Linq.Expressions.Expression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private readonly System.Reflection.MethodInfo <Method>k__BackingField;  // 0x10

        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04D9EC60 */ }
        System.Type Type { get; /* RVA: 0x0ACDE864 */ }
        System.Reflection.MethodInfo Method { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.Expression Object { get; /* RVA: 0x0ACDE854 */ }
        System.Int32 ArgumentCount { get; /* RVA: 0x0ACDE82C */ }

        // Methods
        // RVA: 0x0ACDE7D4  token: 0x6000489
        private System.Void .ctor(System.Reflection.MethodInfo method) { }
        // RVA: 0x011EC580  token: 0x600048A
        private virtual System.Linq.Expressions.Expression GetInstance() { }
        // RVA: 0x0ACDE73C  token: 0x600048F
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDE7AC  token: 0x6000490
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }
        // RVA: 0x0ACDE784  token: 0x6000491
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }

    }

    // TypeToken: 0x20000AB  // size: 0x20
    public class InstanceMethodCallExpression : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _instance;  // 0x18

        // Methods
        // RVA: 0x0ACDC43C  token: 0x6000493
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance) { }
        // RVA: 0x0385B100  token: 0x6000494
        private virtual System.Linq.Expressions.Expression GetInstance() { }

    }

    // TypeToken: 0x20000AC  // size: 0x20
    public sealed class MethodCallExpressionN : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> _arguments;  // 0x18

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x0ACDE6F4 */ }

        // Methods
        // RVA: 0x0ACDC43C  token: 0x6000495
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }
        // RVA: 0x0ACDE638  token: 0x6000496
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDE68C  token: 0x6000498
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000AD  // size: 0x28
    public sealed class InstanceMethodCallExpressionN : System.Linq.Expressions.InstanceMethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> _arguments;  // 0x20

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x0ACDC3F4 */ }

        // Methods
        // RVA: 0x0ACDBF18  token: 0x6000499
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }
        // RVA: 0x0ACDC328  token: 0x600049A
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDC37C  token: 0x600049C
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000AE  // size: 0x18
    public sealed class MethodCallExpression0 : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0ACDDC34  token: 0x600049D
        public System.Void .ctor(System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACDDB88  token: 0x600049E
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDDBE4  token: 0x60004A0
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000AF  // size: 0x20
    public sealed class MethodCallExpression1 : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x18

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0ACDC43C  token: 0x60004A1
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0) { }
        // RVA: 0x0ACDDC3C  token: 0x60004A2
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDDCDC  token: 0x60004A4
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B0  // size: 0x28
    public sealed class MethodCallExpression2 : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x18
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x20

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x0ACDDEFC  token: 0x60004A5
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACDDD80  token: 0x60004A6
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDDE2C  token: 0x60004A8
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B1  // size: 0x30
    public sealed class MethodCallExpression3 : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x18
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x28

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D8C9E0 */ }

        // Methods
        // RVA: 0x0ACDE0F8  token: 0x60004A9
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACDDF48  token: 0x60004AA
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDDFFC  token: 0x60004AC
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B2  // size: 0x38
    public sealed class MethodCallExpression4 : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x18
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x28
        private readonly System.Linq.Expressions.Expression _arg3;  // 0x30

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D882B0 */ }

        // Methods
        // RVA: 0x0ACDE330  token: 0x60004AD
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3) { }
        // RVA: 0x0ACDE154  token: 0x60004AE
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDE214  token: 0x60004B0
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B3  // size: 0x40
    public sealed class MethodCallExpression5 : System.Linq.Expressions.MethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x18
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x28
        private readonly System.Linq.Expressions.Expression _arg3;  // 0x30
        private readonly System.Linq.Expressions.Expression _arg4;  // 0x38

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D9EC50 */ }

        // Methods
        // RVA: 0x0ACDE5B8  token: 0x60004B1
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4) { }
        // RVA: 0x0ACDE3A0  token: 0x60004B2
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDE470  token: 0x60004B4
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B4  // size: 0x20
    public sealed class InstanceMethodCallExpression0 : System.Linq.Expressions.InstanceMethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0ACDBDBC  token: 0x60004B5
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance) { }
        // RVA: 0x0ACDBD0C  token: 0x60004B6
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDBD68  token: 0x60004B8
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B5  // size: 0x28
    public sealed class InstanceMethodCallExpression1 : System.Linq.Expressions.InstanceMethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x20

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0ACDBF18  token: 0x60004B9
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression arg0) { }
        // RVA: 0x0ACDBDC4  token: 0x60004BA
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDBE64  token: 0x60004BC
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B6  // size: 0x30
    public sealed class InstanceMethodCallExpression2 : System.Linq.Expressions.InstanceMethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x28

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x0ACDC0D8  token: 0x60004BD
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1) { }
        // RVA: 0x0ACDBF48  token: 0x60004BE
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDBFF4  token: 0x60004C0
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B7  // size: 0x38
    public sealed class InstanceMethodCallExpression3 : System.Linq.Expressions.InstanceMethodCallExpression, System.Linq.Expressions.IArgumentProvider
    {
        // Fields
        private System.Object _arg0;  // 0x20
        private readonly System.Linq.Expressions.Expression _arg1;  // 0x28
        private readonly System.Linq.Expressions.Expression _arg2;  // 0x30

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x04D8C9E0 */ }

        // Methods
        // RVA: 0x0ACDC2D4  token: 0x60004C1
        public System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2) { }
        // RVA: 0x0ACDC11C  token: 0x60004C2
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }
        // RVA: 0x0ACDC1D0  token: 0x60004C4
        private virtual System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args) { }

    }

    // TypeToken: 0x20000B8  // size: 0x20
    public class NewArrayExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> <Expressions>k__BackingField;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> Expressions { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0ACDF0A8  token: 0x60004C5
        private System.Void .ctor(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACDEEBC  token: 0x60004C6
        private static System.Linq.Expressions.NewArrayExpression Make(System.Linq.Expressions.ExpressionType nodeType, System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions) { }
        // RVA: 0x0ACDEE74  token: 0x60004C9
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACDEF48  token: 0x60004CA
        public System.Linq.Expressions.NewArrayExpression Update(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions) { }

    }

    // TypeToken: 0x20000B9  // size: 0x20
    public sealed class NewArrayInitExpression : System.Linq.Expressions.NewArrayExpression
    {
        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA43E0 */ }

        // Methods
        // RVA: 0x0ACDED34  token: 0x60004CB
        private System.Void .ctor(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions) { }

    }

    // TypeToken: 0x20000BA  // size: 0x20
    public sealed class NewArrayBoundsExpression : System.Linq.Expressions.NewArrayExpression
    {
        // Properties
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA7800 */ }

        // Methods
        // RVA: 0x0ACDED34  token: 0x60004CD
        private System.Void .ctor(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions) { }

    }

    // TypeToken: 0x20000BB  // size: 0x20
    public class NewExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> _arguments;  // 0x10
        private readonly System.Reflection.ConstructorInfo <Constructor>k__BackingField;  // 0x18

        // Properties
        System.Reflection.ConstructorInfo Constructor { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0ACDF280  token: 0x60004D0
        public virtual System.Linq.Expressions.Expression GetArgument(System.Int32 index) { }

    }

    // TypeToken: 0x20000BC  // size: 0x18
    public class ParameterExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.String <Name>k__BackingField;  // 0x10

        // Properties
        System.Type Type { get; /* RVA: 0x0ACDF9B8 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA9FD0 */ }
        System.String Name { get; /* RVA: 0x02B2ECC0 */ }
        System.Boolean IsByRef { get; /* RVA: 0x0ACDF9A8 */ }

        // Methods
        // RVA: 0x0ACDF950  token: 0x60004D1
        private System.Void .ctor(System.String name) { }
        // RVA: 0x0ACDF4E8  token: 0x60004D2
        private static System.Linq.Expressions.ParameterExpression Make(System.Type type, System.String name, System.Boolean isByRef) { }
        // RVA: 0x012081B0  token: 0x60004D7
        private virtual System.Boolean GetIsByRef() { }
        // RVA: 0x0ACDF4A0  token: 0x60004D8
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }

    }

    // TypeToken: 0x20000BD  // size: 0x20
    public sealed class ByRefParameterExpression : System.Linq.Expressions.TypedParameterExpression
    {
        // Methods
        // RVA: 0x0ACCF178  token: 0x60004D9
        private System.Void .ctor(System.Type type, System.String name) { }
        // RVA: 0x02FFF600  token: 0x60004DA
        private virtual System.Boolean GetIsByRef() { }

    }

    // TypeToken: 0x20000BE  // size: 0x20
    public class TypedParameterExpression : System.Linq.Expressions.ParameterExpression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0ACE25D8  token: 0x60004DB
        private System.Void .ctor(System.Type type, System.String name) { }

    }

    // TypeToken: 0x20000BF
    public sealed class PrimitiveParameterExpression`1 : System.Linq.Expressions.ParameterExpression
    {
        // Properties
        System.Type Type { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60004DD
        private System.Void .ctor(System.String name) { }

    }

    // TypeToken: 0x20000C0  // size: 0x18
    public sealed class RuntimeVariablesExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> <Variables>k__BackingField;  // 0x10

        // Properties
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> Variables { get; /* RVA: 0x02B2ECC0 */ }

    }

    // TypeToken: 0x20000C1  // size: 0x18
    public sealed class StackGuard
    {
        // Fields
        private System.Int32 _executionStackCount;  // 0x10

        // Methods
        // RVA: 0x02FFF600  token: 0x60004E0
        public System.Boolean TryEnterOnCurrentStack() { }
        // RVA: -1  // generic def  token: 0x60004E1
        public System.Void RunOnEmptyStack(System.Action<T1,T2> action, T1 arg1, T2 arg2) { }
        // RVA: -1  // generic def  token: 0x60004E2
        private R RunOnEmptyStackCore(System.Func<System.Object,R> action, System.Object state) { }
        // RVA: 0x041E1670  token: 0x60004E3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C3  // size: 0x10
    public static class Strings
    {
        // Properties
        System.String ReducibleMustOverrideReduce { get; /* RVA: 0x0ACE1DB0 */ }
        System.String MustReduceToDifferent { get; /* RVA: 0x0ACE1BA0 */ }
        System.String ReducedNotCompatible { get; /* RVA: 0x0ACE1D84 */ }
        System.String SetterHasNoParams { get; /* RVA: 0x0ACE1E08 */ }
        System.String PropertyCannotHaveRefType { get; /* RVA: 0x0ACE1CA8 */ }
        System.String IndexesOfSetGetMustMatch { get; /* RVA: 0x0ACE1A6C */ }
        System.String AccessorsCannotHaveVarArgs { get; /* RVA: 0x0ACE1544 */ }
        System.String AccessorsCannotHaveByRefArgs { get; /* RVA: 0x0ACE1518 */ }
        System.String BoundsCannotBeLessThanOne { get; /* RVA: 0x0ACE1754 */ }
        System.String TypeMustNotBeByRef { get; /* RVA: 0x0ACE1EB8 */ }
        System.String TypeMustNotBePointer { get; /* RVA: 0x0ACE1EE4 */ }
        System.String SetterMustBeVoid { get; /* RVA: 0x0ACE1E34 */ }
        System.String PropertyTypeMustMatchGetter { get; /* RVA: 0x0ACE1D00 */ }
        System.String PropertyTypeMustMatchSetter { get; /* RVA: 0x0ACE1D2C */ }
        System.String BothAccessorsMustBeStatic { get; /* RVA: 0x0ACE1728 */ }
        System.String OnlyStaticFieldsHaveNullInstance { get; /* RVA: 0x0ACE1C24 */ }
        System.String OnlyStaticPropertiesHaveNullInstance { get; /* RVA: 0x0ACE1C7C */ }
        System.String OnlyStaticMethodsHaveNullInstance { get; /* RVA: 0x0ACE1C50 */ }
        System.String PropertyTypeCannotBeVoid { get; /* RVA: 0x0ACE1CD4 */ }
        System.String InvalidUnboxType { get; /* RVA: 0x0ACE1AC4 */ }
        System.String ExpressionMustBeWriteable { get; /* RVA: 0x0ACE1938 */ }
        System.String ArgumentMustNotHaveValueType { get; /* RVA: 0x0ACE1678 */ }
        System.String MustBeReducible { get; /* RVA: 0x0ACE1B74 */ }
        System.String LabelMustBeVoidOrHaveExpression { get; /* RVA: 0x0ACE1AF0 */ }
        System.String LabelTypeMustBeVoid { get; /* RVA: 0x0ACE1B1C */ }
        System.String QuotedExpressionMustBeLambda { get; /* RVA: 0x0ACE1D58 */ }
        System.String CollectionModifiedWhileEnumerating { get; /* RVA: 0x0ACE17AC */ }
        System.String CollectionReadOnly { get; /* RVA: 0x0ACE17D8 */ }
        System.String BindingCannotBeNull { get; /* RVA: 0x0ACE16D0 */ }
        System.String NoOrInvalidRuleProduced { get; /* RVA: 0x0ACE1BCC */ }
        System.String TypeMustBeDerivedFromSystemDelegate { get; /* RVA: 0x0ACE1E8C */ }
        System.String FirstArgumentMustBeCallSite { get; /* RVA: 0x0ACE1990 */ }
        System.String FaultCannotHaveCatchOrFinally { get; /* RVA: 0x0ACE1964 */ }
        System.String TryMustHaveCatchFinallyOrFault { get; /* RVA: 0x0ACE1E60 */ }
        System.String BodyOfCatchMustHaveSameTypeAsBodyOfTry { get; /* RVA: 0x0ACE16FC */ }
        System.String ConversionIsNotSupportedForArithmeticTypes { get; /* RVA: 0x0ACE18B4 */ }
        System.String ArgumentMustBeArray { get; /* RVA: 0x0ACE15C8 */ }
        System.String ArgumentMustBeBoolean { get; /* RVA: 0x0ACE15F4 */ }
        System.String ArgumentMustBeInteger { get; /* RVA: 0x0ACE1620 */ }
        System.String ArgumentMustBeArrayIndexType { get; /* RVA: 0x0ACE159C */ }
        System.String ArgumentMustBeSingleDimensionalArrayType { get; /* RVA: 0x0ACE164C */ }
        System.String ArgumentTypesMustMatch { get; /* RVA: 0x0ACE16A4 */ }
        System.String CoalesceUsedOnNonNullType { get; /* RVA: 0x0ACE1780 */ }
        System.String IncorrectNumberOfIndexes { get; /* RVA: 0x0ACE19E8 */ }
        System.String IncorrectNumberOfLambdaDeclarationParameters { get; /* RVA: 0x0ACE1A40 */ }
        System.String LambdaTypeMustBeDerivedFromSystemDelegate { get; /* RVA: 0x0ACE1B48 */ }
        System.String ArgumentCannotBeOfTypeVoid { get; /* RVA: 0x0ACE1570 */ }
        System.String ControlCannotLeaveFinally { get; /* RVA: 0x0ACE1888 */ }
        System.String ControlCannotLeaveFilterTest { get; /* RVA: 0x0ACE185C */ }
        System.String ControlCannotEnterTry { get; /* RVA: 0x0ACE1830 */ }
        System.String ControlCannotEnterExpression { get; /* RVA: 0x0ACE1804 */ }
        System.String RethrowRequiresCatch { get; /* RVA: 0x0ACE1DDC */ }
        System.String NonAbstractConstructorRequired { get; /* RVA: 0x0ACE1BF8 */ }
        System.String ExpressionMustBeReadable { get; /* RVA: 0x0ACE190C */ }
        System.String EnumerationIsDone { get; /* RVA: 0x0ACE18E0 */ }
        System.String InvalidArgumentValue { get; /* RVA: 0x0ACE1A98 */ }
        System.String IncorrectNumberOfLambdaArguments { get; /* RVA: 0x0ACE1A14 */ }
        System.String IncorrectNumberOfConstructorArguments { get; /* RVA: 0x0ACE19BC */ }

        // Methods
        // RVA: 0x0ACE14CC  token: 0x6000502
        private static System.String VariableMustNotBeByRef(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE02F8  token: 0x6000504
        private static System.String AmbiguousMatchInExpandoObject(System.Object p0) { }
        // RVA: 0x0ACE1208  token: 0x6000505
        private static System.String SameKeyExistsInExpando(System.Object p0) { }
        // RVA: 0x0ACE0C24  token: 0x6000506
        private static System.String KeyDoesNotExistInExpando(System.Object p0) { }
        // RVA: 0x0ACE0B60  token: 0x6000507
        private static System.String InvalidMetaObjectCreated(System.Object p0) { }
        // RVA: 0x0ACE0394  token: 0x6000508
        private static System.String BinderNotCompatibleWithCallSite(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE0518  token: 0x6000509
        private static System.String DynamicBindingNeedsRestrictions(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0564  token: 0x600050A
        private static System.String DynamicObjectResultNotAssignable(System.Object p0, System.Object p1, System.Object p2, System.Object p3) { }
        // RVA: 0x0ACE04B8  token: 0x600050B
        private static System.String DynamicBinderResultNotAssignable(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE047C  token: 0x600050D
        private static System.String DuplicateVariable(System.Object p0) { }
        // RVA: 0x0ACE12BC  token: 0x600050E
        private static System.String TypeParameterIsNotDelegate(System.Object p0) { }
        // RVA: 0x0ACE08CC  token: 0x6000515
        private static System.String ExtensionNodeMustOverrideProperty(System.Object p0) { }
        // RVA: 0x0ACE1454  token: 0x6000516
        private static System.String UserDefinedOperatorMustBeStatic(System.Object p0) { }
        // RVA: 0x0ACE1490  token: 0x6000517
        private static System.String UserDefinedOperatorMustNotBeVoid(System.Object p0) { }
        // RVA: 0x0ACE0430  token: 0x6000518
        private static System.String CoercionOperatorNotDefined(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE12F8  token: 0x6000519
        private static System.String UnaryOperatorNotDefined(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0334  token: 0x600051A
        private static System.String BinaryOperatorNotDefined(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE11BC  token: 0x600051B
        private static System.String ReferenceEqualityNotDefined(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE1004  token: 0x600051C
        private static System.String OperandTypesDoNotMatchParameters(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE109C  token: 0x600051D
        private static System.String OverloadOperatorTypeDoesNotMatchConversionType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE03F4  token: 0x6000525
        private static System.String CannotAutoInitializeValueTypeMemberThroughProperty(System.Object p0) { }
        // RVA: 0x0ACE09F0  token: 0x6000526
        private static System.String IncorrectTypeForTypeAs(System.Object p0) { }
        // RVA: 0x0ACE0668  token: 0x6000528
        private static System.String ExpressionTypeCannotInitializeArrayType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0844  token: 0x6000529
        private static System.String ExpressionTypeDoesNotMatchReturn(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE06B4  token: 0x600052A
        private static System.String ExpressionTypeDoesNotMatchAssignment(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE074C  token: 0x600052B
        private static System.String ExpressionTypeDoesNotMatchLabel(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0890  token: 0x600052C
        private static System.String ExpressionTypeNotInvocable(System.Object p0) { }
        // RVA: 0x0ACE0A8C  token: 0x600052D
        private static System.String InstanceFieldNotDefinedForType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0908  token: 0x600052E
        private static System.String FieldInfoNotDefinedForType(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE0D24  token: 0x6000532
        private static System.String MemberNotFieldOrProperty(System.Object p0) { }
        // RVA: 0x0ACE0D60  token: 0x6000533
        private static System.String MethodContainsGenericParameters(System.Object p0) { }
        // RVA: 0x0ACE0D9C  token: 0x6000534
        private static System.String MethodIsGeneric(System.Object p0) { }
        // RVA: 0x0ACE0DD8  token: 0x6000535
        private static System.String MethodNotPropertyAccessor(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE1134  token: 0x6000536
        private static System.String PropertyDoesNotHaveAccessor(System.Object p0) { }
        // RVA: 0x0ACE10E8  token: 0x6000537
        private static System.String ParameterExpressionNotValidAsDelegate(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE1170  token: 0x6000538
        private static System.String PropertyNotDefinedForType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0AD8  token: 0x6000539
        private static System.String InstancePropertyNotDefinedForType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0A2C  token: 0x600053A
        private static System.String InstanceAndMethodTypeMismatch(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE1344  token: 0x600053B
        private static System.String UnhandledBinary(System.Object p0) { }
        // RVA: 0x0ACE1380  token: 0x600053C
        private static System.String UnhandledUnary(System.Object p0) { }
        // RVA: 0x0ACE13BC  token: 0x600053D
        private static System.String UserDefinedOpMustHaveConsistentTypes(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE1408  token: 0x600053E
        private static System.String UserDefinedOpMustHaveValidReturnType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0CD8  token: 0x600053F
        private static System.String LogicalOperatorMustHaveBooleanOperators(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0E24  token: 0x6000540
        private static System.String MethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0968  token: 0x6000541
        private static System.String GenericMethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0E70  token: 0x6000542
        private static System.String MethodWithMoreThanOneMatch(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE1050  token: 0x6000544
        private static System.String OutOfRange(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0C60  token: 0x6000545
        private static System.String LabelTargetAlreadyDefined(System.Object p0) { }
        // RVA: 0x0ACE0C9C  token: 0x6000546
        private static System.String LabelTargetUndefined(System.Object p0) { }
        // RVA: 0x0ACE02BC  token: 0x6000549
        private static System.String AmbiguousJump(System.Object p0) { }
        // RVA: 0x0ACE0FC8  token: 0x600054C
        private static System.String NonLocalJumpWithValue(System.Object p0) { }
        // RVA: 0x0ACE0B24  token: 0x600054D
        private static System.String InvalidLvalue(System.Object p0) { }
        // RVA: 0x0ACE0F1C  token: 0x600054F
        private static System.String MustRewriteToSameNode(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE0EBC  token: 0x6000550
        private static System.String MustRewriteChildToSameType(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE0F7C  token: 0x6000551
        private static System.String MustRewriteWithoutMethod(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0700  token: 0x6000554
        private static System.String ExpressionTypeDoesNotMatchConstructorParameter(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE1244  token: 0x6000556
        private static System.String TypeContainsGenericParameters(System.Object p0) { }
        // RVA: 0x0ACE1280  token: 0x6000557
        private static System.String TypeIsGeneric(System.Object p0) { }
        // RVA: 0x0ACE0B9C  token: 0x6000559
        private static System.String InvalidNullValue(System.Object p0) { }
        // RVA: 0x0ACE0BD8  token: 0x600055A
        private static System.String InvalidObjectType(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE0798  token: 0x600055B
        private static System.String ExpressionTypeDoesNotMatchMethodParameter(System.Object p0, System.Object p1, System.Object p2) { }
        // RVA: 0x0ACE07F8  token: 0x600055C
        private static System.String ExpressionTypeDoesNotMatchParameter(System.Object p0, System.Object p1) { }
        // RVA: 0x0ACE09B4  token: 0x600055D
        private static System.String IncorrectNumberOfMethodCallArguments(System.Object p0) { }

    }

    // TypeToken: 0x20000C4  // size: 0x20
    public sealed class SwitchCase
    {
        // Fields
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> <TestValues>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <Body>k__BackingField;  // 0x18

        // Properties
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> TestValues { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.Expression Body { get; /* RVA: 0x0385B100 */ }

    }

    // TypeToken: 0x20000C5  // size: 0x30
    public sealed class SwitchExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.Expression <SwitchValue>k__BackingField;  // 0x10
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.SwitchCase> <Cases>k__BackingField;  // 0x18
        private readonly System.Linq.Expressions.Expression <DefaultBody>k__BackingField;  // 0x20
        private readonly System.Reflection.MethodInfo <Comparison>k__BackingField;  // 0x28

        // Properties
        System.Linq.Expressions.Expression SwitchValue { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.SwitchCase> Cases { get; /* RVA: 0x0385B100 */ }
        System.Linq.Expressions.Expression DefaultBody { get; /* RVA: 0x04D862C0 */ }
        System.Reflection.MethodInfo Comparison { get; /* RVA: 0x04D86240 */ }

    }

    // TypeToken: 0x20000C6  // size: 0x18
    public class SymbolDocumentInfo
    {
        // Fields
        private readonly System.String <FileName>k__BackingField;  // 0x10
        private static readonly System.Guid DocumentType_Text;  // static @ 0x0

        // Properties
        System.String FileName { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0ACE1F10  token: 0x6000567
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C7  // size: 0x38
    public sealed class TryExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <Body>k__BackingField;  // 0x18
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock> <Handlers>k__BackingField;  // 0x20
        private readonly System.Linq.Expressions.Expression <Finally>k__BackingField;  // 0x28
        private readonly System.Linq.Expressions.Expression <Fault>k__BackingField;  // 0x30

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x04DA9FF0 */ }
        System.Linq.Expressions.Expression Body { get; /* RVA: 0x0385B100 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock> Handlers { get; /* RVA: 0x04D862C0 */ }
        System.Linq.Expressions.Expression Finally { get; /* RVA: 0x04D86240 */ }
        System.Linq.Expressions.Expression Fault { get; /* RVA: 0x011F36E0 */ }

        // Methods
        // RVA: 0x0ACE2364  token: 0x6000568
        private System.Void .ctor(System.Type type, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally, System.Linq.Expressions.Expression fault, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock> handlers) { }
        // RVA: 0x0ACE2240  token: 0x600056F
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACE2288  token: 0x6000570
        public System.Linq.Expressions.TryExpression Update(System.Linq.Expressions.Expression body, System.Collections.Generic.IEnumerable<System.Linq.Expressions.CatchBlock> handlers, System.Linq.Expressions.Expression finally, System.Linq.Expressions.Expression fault) { }

    }

    // TypeToken: 0x20000C8  // size: 0x28
    public sealed class TypeBinaryExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Linq.Expressions.ExpressionType <NodeType>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.Expression <Expression>k__BackingField;  // 0x18
        private readonly System.Type <TypeOperand>k__BackingField;  // 0x20

        // Properties
        System.Type Type { get; /* RVA: 0x0ACE2558 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x02B2E2D0 */ }
        System.Linq.Expressions.Expression Expression { get; /* RVA: 0x0385B100 */ }
        System.Type TypeOperand { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0ACE24DC  token: 0x6000571
        private System.Void .ctor(System.Linq.Expressions.Expression expression, System.Type typeOperand, System.Linq.Expressions.ExpressionType nodeType) { }
        // RVA: 0x0ACE240C  token: 0x6000576
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACE2454  token: 0x6000577
        public System.Linq.Expressions.TypeBinaryExpression Update(System.Linq.Expressions.Expression expression) { }

    }

    // TypeToken: 0x20000C9  // size: 0x30
    public sealed class UnaryExpression : System.Linq.Expressions.Expression
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.ExpressionType <NodeType>k__BackingField;  // 0x18
        private readonly System.Linq.Expressions.Expression <Operand>k__BackingField;  // 0x20
        private readonly System.Reflection.MethodInfo <Method>k__BackingField;  // 0x28

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }
        System.Linq.Expressions.ExpressionType NodeType { get; /* RVA: 0x011EF5B0 */ }
        System.Linq.Expressions.Expression Operand { get; /* RVA: 0x04D862C0 */ }
        System.Reflection.MethodInfo Method { get; /* RVA: 0x04D86240 */ }
        System.Boolean IsLifted { get; /* RVA: 0x0ACE34E8 */ }
        System.Boolean IsLiftedToNull { get; /* RVA: 0x0ACE3484 */ }
        System.Boolean CanReduce { get; /* RVA: 0x0ACE3464 */ }
        System.Boolean IsPrefix { get; /* RVA: 0x0ACE36B0 */ }

        // Methods
        // RVA: 0x0ACE33D8  token: 0x6000578
        private System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression expression, System.Type type, System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACE2720  token: 0x600057F
        protected internal virtual System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor) { }
        // RVA: 0x0ACE32D0  token: 0x6000581
        public virtual System.Linq.Expressions.Expression Reduce() { }
        // RVA: 0x0ACE2768  token: 0x6000583
        private System.Linq.Expressions.UnaryExpression FunctionalOp(System.Linq.Expressions.Expression operand) { }
        // RVA: 0x0ACE3084  token: 0x6000584
        private System.Linq.Expressions.Expression ReduceVariable() { }
        // RVA: 0x0ACE2CB8  token: 0x6000585
        private System.Linq.Expressions.Expression ReduceMember() { }
        // RVA: 0x0ACE283C  token: 0x6000586
        private System.Linq.Expressions.Expression ReduceIndex() { }
        // RVA: 0x0ACE333C  token: 0x6000587
        public System.Linq.Expressions.UnaryExpression Update(System.Linq.Expressions.Expression operand) { }

    }

    // TypeToken: 0x20000CA  // size: 0x10
    public static class Utils
    {
        // Fields
        public static readonly System.Object BoxedFalse;  // static @ 0x0
        public static readonly System.Object BoxedTrue;  // static @ 0x8
        public static readonly System.Object BoxedIntM1;  // static @ 0x10
        public static readonly System.Object BoxedInt0;  // static @ 0x18
        public static readonly System.Object BoxedInt1;  // static @ 0x20
        public static readonly System.Object BoxedInt2;  // static @ 0x28
        public static readonly System.Object BoxedInt3;  // static @ 0x30
        public static readonly System.Object BoxedDefaultSByte;  // static @ 0x38
        public static readonly System.Object BoxedDefaultChar;  // static @ 0x40
        public static readonly System.Object BoxedDefaultInt16;  // static @ 0x48
        public static readonly System.Object BoxedDefaultInt64;  // static @ 0x50
        public static readonly System.Object BoxedDefaultByte;  // static @ 0x58
        public static readonly System.Object BoxedDefaultUInt16;  // static @ 0x60
        public static readonly System.Object BoxedDefaultUInt32;  // static @ 0x68
        public static readonly System.Object BoxedDefaultUInt64;  // static @ 0x70
        public static readonly System.Object BoxedDefaultSingle;  // static @ 0x78
        public static readonly System.Object BoxedDefaultDouble;  // static @ 0x80
        public static readonly System.Object BoxedDefaultDecimal;  // static @ 0x88
        public static readonly System.Object BoxedDefaultDateTime;  // static @ 0x90
        private static readonly System.Linq.Expressions.ConstantExpression s_true;  // static @ 0x98
        private static readonly System.Linq.Expressions.ConstantExpression s_false;  // static @ 0xa0
        private static readonly System.Linq.Expressions.ConstantExpression s_m1;  // static @ 0xa8
        private static readonly System.Linq.Expressions.ConstantExpression s_0;  // static @ 0xb0
        private static readonly System.Linq.Expressions.ConstantExpression s_1;  // static @ 0xb8
        private static readonly System.Linq.Expressions.ConstantExpression s_2;  // static @ 0xc0
        private static readonly System.Linq.Expressions.ConstantExpression s_3;  // static @ 0xc8
        public static readonly System.Linq.Expressions.DefaultExpression Empty;  // static @ 0xd0
        public static readonly System.Linq.Expressions.ConstantExpression Null;  // static @ 0xd8

        // Methods
        // RVA: 0x0ACE3834  token: 0x6000588
        public static System.Linq.Expressions.ConstantExpression Constant(System.Boolean value) { }
        // RVA: 0x0ACE36E8  token: 0x6000589
        public static System.Linq.Expressions.ConstantExpression Constant(System.Int32 value) { }
        // RVA: 0x0ACE388C  token: 0x600058A
        private static System.Void .cctor() { }

    }

}

namespace System.Linq.Expressions.Interpreter
{

    // TypeToken: 0x20000CB  // size: 0x10
    public abstract class AddInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACCB3D4 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600058E
        private System.Void .ctor() { }
        // RVA: 0x0ACCAFC4  token: 0x600058F
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20000D4  // size: 0x10
    public abstract class AddOvfInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACCBA48 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005A3
        private System.Void .ctor() { }
        // RVA: 0x0ACCB72C  token: 0x60005A4
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20000DB  // size: 0x10
    public abstract class AndInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_Boolean;  // static @ 0x40

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACCCE84 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005B4
        private System.Void .ctor() { }
        // RVA: 0x0ACCC9F4  token: 0x60005B5
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20000E5  // size: 0x20
    public sealed class NewArrayInitInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Type _elementType;  // 0x10
        private readonly System.Int32 _elementCount;  // 0x18

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACDF19C */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x60005C8
        private System.Void .ctor(System.Type elementType, System.Int32 elementCount) { }
        // RVA: 0x0ACDF114  token: 0x60005CC
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20000E6  // size: 0x18
    public sealed class NewArrayInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Type _elementType;  // 0x10

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACDF254 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60005CD
        private System.Void .ctor(System.Type elementType) { }
        // RVA: 0x0ACDF1C8  token: 0x60005D1
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20000E7  // size: 0x20
    public sealed class NewArrayBoundsInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Type _elementType;  // 0x10
        private readonly System.Int32 _rank;  // 0x18

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACDEE48 */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x60005D2
        private System.Void .ctor(System.Type elementType, System.Int32 rank) { }
        // RVA: 0x0ACDED3C  token: 0x60005D6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20000E8  // size: 0x10
    public sealed class GetArrayItemInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.GetArrayItemInstruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACDB53C */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005D7
        private System.Void .ctor() { }
        // RVA: 0x0ACDB444  token: 0x60005DB
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDB4D8  token: 0x60005DC
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E9  // size: 0x10
    public sealed class SetArrayItemInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.SetArrayItemInstruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D8C9E0 */ }
        System.String InstructionName { get; /* RVA: 0x0ACE01A8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005DD
        private System.Void .ctor() { }
        // RVA: 0x0ACE0090  token: 0x60005E0
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACE0144  token: 0x60005E1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000EA  // size: 0x10
    public sealed class ArrayLengthInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        public static readonly System.Linq.Expressions.Interpreter.ArrayLengthInstruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACCD49C */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005E5
        private System.Void .ctor() { }
        // RVA: 0x0ACCD3AC  token: 0x60005E6
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACCD438  token: 0x60005E7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000EB  // size: 0x10
    public static class ConvertHelper
    {
        // Methods
        // RVA: 0x0ACD0090  token: 0x60005E8
        public static System.Int32 ToInt32NoNull(System.Object val) { }

    }

    // TypeToken: 0x20000EC  // size: 0x1C
    public sealed struct RuntimeLabel
    {
        // Fields
        public readonly System.Int32 Index;  // 0x10
        public readonly System.Int32 StackDepth;  // 0x14
        public readonly System.Int32 ContinuationStackDepth;  // 0x18

        // Methods
        // RVA: 0x04DA9FE0  token: 0x60005E9
        public System.Void .ctor(System.Int32 index, System.Int32 continuationStackDepth, System.Int32 stackDepth) { }
        // RVA: 0x0ACDFA2C  token: 0x60005EA
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000ED  // size: 0x30
    public sealed class BranchLabel
    {
        // Fields
        private System.Int32 _targetIndex;  // 0x10
        private System.Int32 _stackDepth;  // 0x14
        private System.Int32 _continuationStackDepth;  // 0x18
        private System.Collections.Generic.List<System.Int32> _forwardBranchFixups;  // 0x20
        private System.Int32 <LabelIndex>k__BackingField;  // 0x28

        // Properties
        System.Int32 LabelIndex { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Boolean HasRuntimeLabel { get; /* RVA: 0x0ACCEA04 */ }
        System.Int32 TargetIndex { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x04DA9F20  token: 0x60005EF
        private System.Linq.Expressions.Interpreter.RuntimeLabel ToRuntimeLabel() { }
        // RVA: 0x0ACCE87C  token: 0x60005F0
        private System.Void Mark(System.Linq.Expressions.Interpreter.InstructionList instructions) { }
        // RVA: 0x0ACCE780  token: 0x60005F1
        private System.Void AddBranch(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Int32 branchIndex) { }
        // RVA: 0x0ACCE84C  token: 0x60005F2
        private System.Void FixupBranch(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Int32 branchIndex) { }
        // RVA: 0x04DA9F40  token: 0x60005F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EE  // size: 0x10
    public abstract class CallInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Properties
        System.Int32 ArgumentCount { get; /* RVA: -1  // abstract */ }
        System.String InstructionName { get; /* RVA: 0x0ACCF878 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x0ACCF868 */ }

        // Methods
        // RVA: 0x0ACCF208  token: 0x60005F6
        public static System.Linq.Expressions.Interpreter.CallInstruction Create(System.Reflection.MethodInfo info) { }
        // RVA: 0x0ACCF258  token: 0x60005F7
        public static System.Linq.Expressions.Interpreter.CallInstruction Create(System.Reflection.MethodInfo info, System.Reflection.ParameterInfo[] parameters) { }
        // RVA: 0x0ACCF3B0  token: 0x60005F8
        private static System.Linq.Expressions.Interpreter.CallInstruction GetArrayAccessor(System.Reflection.MethodInfo info, System.Int32 argumentCount) { }
        // RVA: 0x0ACCF180  token: 0x60005F9
        public static System.Void ArrayItemSetter1(System.Array array, System.Int32 index0, System.Object value) { }
        // RVA: 0x0ACCF1A4  token: 0x60005FA
        public static System.Void ArrayItemSetter2(System.Array array, System.Int32 index0, System.Int32 index1, System.Object value) { }
        // RVA: 0x0ACCF1D4  token: 0x60005FB
        public static System.Void ArrayItemSetter3(System.Array array, System.Int32 index0, System.Int32 index1, System.Int32 index2, System.Object value) { }
        // RVA: 0x0ACCF7A8  token: 0x60005FD
        protected static System.Boolean TryGetLightLambdaTarget(System.Object instance, System.Linq.Expressions.Interpreter.LightLambda& lightLambda) { }
        // RVA: 0x0ACCF74C  token: 0x60005FE
        protected System.Object InterpretLambdaInvoke(System.Linq.Expressions.Interpreter.LightLambda targetLambda, System.Object[] args) { }
        // RVA: 0x041E1670  token: 0x60005FF
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000EF  // size: 0x20
    public class MethodInfoCallInstruction : System.Linq.Expressions.Interpreter.CallInstruction
    {
        // Fields
        protected readonly System.Reflection.MethodInfo _target;  // 0x10
        protected readonly System.Int32 _argumentCount;  // 0x18

        // Properties
        System.Int32 ArgumentCount { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x0ACDECA4 */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000601
        private System.Void .ctor(System.Reflection.MethodInfo target, System.Int32 argumentCount) { }
        // RVA: 0x0ACDE97C  token: 0x6000603
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDE888  token: 0x6000604
        protected System.Object[] GetArgs(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Int32 first, System.Int32 skip) { }
        // RVA: 0x0ACDEC30  token: 0x6000605
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000F0  // size: 0x28
    public class ByRefMethodInfoCallInstruction : System.Linq.Expressions.Interpreter.MethodInfoCallInstruction
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.ByRefUpdater[] _byrefArgs;  // 0x20

        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x0ACCF0E8 */ }

        // Methods
        // RVA: 0x032B2C30  token: 0x6000606
        private System.Void .ctor(System.Reflection.MethodInfo target, System.Int32 argumentCount, System.Linq.Expressions.Interpreter.ByRefUpdater[] byrefArgs) { }
        // RVA: 0x0ACCEB30  token: 0x6000608
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20000F1  // size: 0x18
    public abstract class OffsetInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        protected System.Int32 _offset;  // 0x10

        // Properties
        System.Linq.Expressions.Interpreter.Instruction[] Cache { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x0ACDF2D4  token: 0x600060A
        public System.Linq.Expressions.Interpreter.Instruction Fixup(System.Int32 offset) { }
        // RVA: 0x0ACDF35C  token: 0x600060B
        public virtual System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects) { }
        // RVA: 0x0ACDF400  token: 0x600060C
        public virtual System.String ToString() { }
        // RVA: 0x04DA9EF0  token: 0x600060D
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000F2  // size: 0x18
    public sealed class BranchFalseInstruction : System.Linq.Expressions.Interpreter.OffsetInstruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction[] s_cache;  // static @ 0x0

        // Properties
        System.Linq.Expressions.Interpreter.Instruction[] Cache { get; /* RVA: 0x0ACCE484 */ }
        System.String InstructionName { get; /* RVA: 0x0ACCE510 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0ACCE41C  token: 0x6000611
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x04DA9EF0  token: 0x6000612
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F3  // size: 0x18
    public sealed class BranchTrueInstruction : System.Linq.Expressions.Interpreter.OffsetInstruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction[] s_cache;  // static @ 0x0

        // Properties
        System.Linq.Expressions.Interpreter.Instruction[] Cache { get; /* RVA: 0x0ACCEA78 */ }
        System.String InstructionName { get; /* RVA: 0x0ACCEB04 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0ACCEA10  token: 0x6000616
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x04DA9EF0  token: 0x6000617
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F4  // size: 0x18
    public sealed class CoalescingBranchInstruction : System.Linq.Expressions.Interpreter.OffsetInstruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction[] s_cache;  // static @ 0x0

        // Properties
        System.Linq.Expressions.Interpreter.Instruction[] Cache { get; /* RVA: 0x0ACCF9B8 */ }
        System.String InstructionName { get; /* RVA: 0x0ACCFA44 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0ACCF984  token: 0x600061C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x04DA9EF0  token: 0x600061D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F5  // size: 0x20
    public class BranchInstruction : System.Linq.Expressions.Interpreter.OffsetInstruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction[][][] s_caches;  // static @ 0x0
        private readonly System.Boolean _hasResult;  // 0x18
        private readonly System.Boolean _hasValue;  // 0x19

        // Properties
        System.Linq.Expressions.Interpreter.Instruction[] Cache { get; /* RVA: 0x0ACCE53C */ }
        System.String InstructionName { get; /* RVA: 0x0ACCE748 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x0ACCE73C */ }
        System.Int32 ProducedStack { get; /* RVA: 0x0ACCE774 */ }

        // Methods
        // RVA: 0x04DA9F00  token: 0x600061F
        private System.Void .ctor() { }
        // RVA: 0x04DA9F10  token: 0x6000620
        public System.Void .ctor(System.Boolean hasResult, System.Boolean hasValue) { }
        // RVA: 0x02B2E2D0  token: 0x6000624
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20000F6  // size: 0x18
    public abstract class IndexedBranchInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Int32 _labelIndex;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x6000625
        public System.Void .ctor(System.Int32 labelIndex) { }
        // RVA: 0x0ACDBBA0  token: 0x6000626
        public System.Linq.Expressions.Interpreter.RuntimeLabel GetLabel(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDBBDC  token: 0x6000627
        public virtual System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects) { }
        // RVA: 0x0ACDBC90  token: 0x6000628
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000F7  // size: 0x20
    public sealed class GotoInstruction : System.Linq.Expressions.Interpreter.IndexedBranchInstruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.GotoInstruction[] s_cache;  // static @ 0x0
        private readonly System.Boolean _hasResult;  // 0x18
        private readonly System.Boolean _hasValue;  // 0x19
        private readonly System.Boolean _labelTargetGetsValue;  // 0x1a

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACDB92C */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x0ACCE73C */ }
        System.Int32 ProducedStack { get; /* RVA: 0x0ACCE774 */ }

        // Methods
        // RVA: 0x04DA9F90  token: 0x600062C
        private System.Void .ctor(System.Int32 targetIndex, System.Boolean hasResult, System.Boolean hasValue, System.Boolean labelTargetGetsValue) { }
        // RVA: 0x0ACDB6D0  token: 0x600062D
        private static System.Linq.Expressions.Interpreter.GotoInstruction Create(System.Int32 labelIndex, System.Boolean hasResult, System.Boolean hasValue, System.Boolean labelTargetGetsValue) { }
        // RVA: 0x0ACDB808  token: 0x600062E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDB8C4  token: 0x600062F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F8  // size: 0x28
    public sealed class EnterTryCatchFinallyInstruction : System.Linq.Expressions.Interpreter.IndexedBranchInstruction
    {
        // Fields
        private readonly System.Boolean _hasFinally;  // 0x18
        private System.Linq.Expressions.Interpreter.TryCatchFinallyHandler _tryHandler;  // 0x20

        // Properties
        System.Linq.Expressions.Interpreter.TryCatchFinallyHandler Handler { get; /* RVA: 0x04D862C0 */ }
        System.Int32 ProducedContinuations { get; /* RVA: 0x0ACCE774 */ }
        System.String InstructionName { get; /* RVA: 0x0ACD2500 */ }

        // Methods
        // RVA: 0x05390F40  token: 0x6000630
        private System.Void SetTryHandler(System.Linq.Expressions.Interpreter.TryCatchFinallyHandler tryHandler) { }
        // RVA: 0x04DA9F70  token: 0x6000633
        private System.Void .ctor(System.Int32 targetIndex, System.Boolean hasFinally) { }
        // RVA: 0x0ACD1E58  token: 0x6000634
        private static System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction CreateTryFinally(System.Int32 labelIndex) { }
        // RVA: 0x0ACD1E10  token: 0x6000635
        private static System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction CreateTryCatch() { }
        // RVA: 0x0ACD1EA0  token: 0x6000636
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACD2484  token: 0x6000638
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000F9  // size: 0x20
    public sealed class EnterTryFaultInstruction : System.Linq.Expressions.Interpreter.IndexedBranchInstruction
    {
        // Fields
        private System.Linq.Expressions.Interpreter.TryFaultHandler _tryHandler;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACD2830 */ }
        System.Int32 ProducedContinuations { get; /* RVA: 0x04D85B80 */ }
        System.Linq.Expressions.Interpreter.TryFaultHandler Handler { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000639
        private System.Void .ctor(System.Int32 targetIndex) { }
        // RVA: 0x05392C40  token: 0x600063D
        private System.Void SetTryHandler(System.Linq.Expressions.Interpreter.TryFaultHandler tryHandler) { }
        // RVA: 0x0ACD254C  token: 0x600063E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20000FA  // size: 0x18
    public sealed class EnterFinallyInstruction : System.Linq.Expressions.Interpreter.IndexedBranchInstruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.EnterFinallyInstruction[] s_cache;  // static @ 0x0

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACD1DE4 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ConsumedContinuations { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600063F
        private System.Void .ctor(System.Int32 labelIndex) { }
        // RVA: 0x0ACD1C54  token: 0x6000643
        private static System.Linq.Expressions.Interpreter.EnterFinallyInstruction Create(System.Int32 labelIndex) { }
        // RVA: 0x0ACD1D28  token: 0x6000644
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACD1D7C  token: 0x6000645
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FB  // size: 0x10
    public sealed class LeaveFinallyInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.Instruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.String InstructionName { get; /* RVA: 0x0ACDD750 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000646
        private System.Void .ctor() { }
        // RVA: 0x0ACDD6AC  token: 0x6000649
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDD6EC  token: 0x600064A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FC  // size: 0x18
    public sealed class EnterFaultInstruction : System.Linq.Expressions.Interpreter.IndexedBranchInstruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.EnterFaultInstruction[] s_cache;  // static @ 0x0

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACD1C28 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600064B
        private System.Void .ctor(System.Int32 labelIndex) { }
        // RVA: 0x0ACD1A9C  token: 0x600064E
        private static System.Linq.Expressions.Interpreter.EnterFaultInstruction Create(System.Int32 labelIndex) { }
        // RVA: 0x0ACD1B70  token: 0x600064F
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACD1BC0  token: 0x6000650
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FD  // size: 0x10
    public sealed class LeaveFaultInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.Instruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ConsumedContinuations { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACDD680 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000651
        private System.Void .ctor() { }
        // RVA: 0x0ACDD5F4  token: 0x6000655
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDD61C  token: 0x6000656
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FE  // size: 0x10
    public sealed class EnterExceptionFilterInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.EnterExceptionFilterInstruction Instance;  // static @ 0x0

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACD1994 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000657
        private System.Void .ctor() { }
        // RVA: 0x04D85B80  token: 0x600065A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACD1930  token: 0x600065B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FF  // size: 0x10
    public sealed class LeaveExceptionFilterInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.LeaveExceptionFilterInstruction Instance;  // static @ 0x0

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACDD404 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600065C
        private System.Void .ctor() { }
        // RVA: 0x04D85B80  token: 0x600065F
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDD3A0  token: 0x6000660
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000100  // size: 0x18
    public sealed class EnterExceptionHandlerInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.EnterExceptionHandlerInstruction Void;  // static @ 0x0
        private static readonly System.Linq.Expressions.Interpreter.EnterExceptionHandlerInstruction NonVoid;  // static @ 0x8
        private readonly System.Boolean _hasValue;  // 0x10

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACD1A70 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x0ACD1A64 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x04D86130  token: 0x6000661
        private System.Void .ctor(System.Boolean hasValue) { }
        // RVA: 0x04D85B80  token: 0x6000665
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACD19C0  token: 0x6000666
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000101  // size: 0x20
    public sealed class LeaveExceptionHandlerInstruction : System.Linq.Expressions.Interpreter.IndexedBranchInstruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.LeaveExceptionHandlerInstruction[] s_cache;  // static @ 0x0
        private readonly System.Boolean _hasValue;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACDD5C8 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x0ACCE774 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x0ACCE774 */ }

        // Methods
        // RVA: 0x04DA9F70  token: 0x6000667
        private System.Void .ctor(System.Int32 labelIndex, System.Boolean hasValue) { }
        // RVA: 0x0ACDD430  token: 0x600066B
        private static System.Linq.Expressions.Interpreter.LeaveExceptionHandlerInstruction Create(System.Int32 labelIndex, System.Boolean hasValue) { }
        // RVA: 0x0ACDD52C  token: 0x600066C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACDD560  token: 0x600066D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000102  // size: 0x18
    public sealed class ThrowInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.ThrowInstruction Throw;  // static @ 0x0
        private static readonly System.Linq.Expressions.Interpreter.ThrowInstruction VoidThrow;  // static @ 0x8
        private static readonly System.Linq.Expressions.Interpreter.ThrowInstruction Rethrow;  // static @ 0x10
        private static readonly System.Linq.Expressions.Interpreter.ThrowInstruction VoidRethrow;  // static @ 0x18
        private readonly System.Boolean _hasResult;  // 0x10
        private readonly System.Boolean _rethrow;  // 0x11

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACE2214 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x0ACD1A64 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x04DA3CB0  token: 0x600066E
        private System.Void .ctor(System.Boolean hasResult, System.Boolean isRethrow) { }
        // RVA: 0x0ACE1FA4  token: 0x6000672
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACE2050  token: 0x6000673
        private static System.Exception WrapThrownObject(System.Object thrown) { }
        // RVA: 0x0ACE20E8  token: 0x6000674
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000103
    public sealed class IntSwitchInstruction`1 : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<T,System.Int32> _cases;  // 0x0

        // Properties
        System.String InstructionName { get; /* RVA: -1  // not resolved */ }
        System.Int32 ConsumedStack { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000675
        private System.Void .ctor(System.Collections.Generic.Dictionary<T,System.Int32> cases) { }
        // RVA: -1  // not resolved  token: 0x6000678
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000104  // size: 0x20
    public sealed class StringSwitchInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.String,System.Int32> _cases;  // 0x10
        private readonly System.Runtime.CompilerServices.StrongBox<System.Int32> _nullCase;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACE0290 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x032AF420  token: 0x6000679
        private System.Void .ctor(System.Collections.Generic.Dictionary<System.String,System.Int32> cases, System.Runtime.CompilerServices.StrongBox<System.Int32> nullCase) { }
        // RVA: 0x0ACE01D4  token: 0x600067C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000105  // size: 0x10
    public abstract class DecrementInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACD065C */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000680
        private System.Void .ctor() { }
        // RVA: 0x0ACD024C  token: 0x6000681
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x200010E  // size: 0x18
    public sealed class DefaultValueInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Type _type;  // 0x10

        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACD0B88 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000692
        private System.Void .ctor(System.Type type) { }
        // RVA: 0x0ACD0AF0  token: 0x6000695
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACD0B28  token: 0x6000696
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200010F  // size: 0x10
    public abstract class DivInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACD10DC */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600069A
        private System.Void .ctor() { }
        // RVA: 0x0ACD0CCC  token: 0x600069B
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x2000118  // size: 0x10
    public abstract class EqualInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_reference;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Boolean;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Char;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.Instruction s_BooleanLiftedToNull;  // static @ 0x68
        private static System.Linq.Expressions.Interpreter.Instruction s_SByteLiftedToNull;  // static @ 0x70
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16LiftedToNull;  // static @ 0x78
        private static System.Linq.Expressions.Interpreter.Instruction s_CharLiftedToNull;  // static @ 0x80
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32LiftedToNull;  // static @ 0x88
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64LiftedToNull;  // static @ 0x90
        private static System.Linq.Expressions.Interpreter.Instruction s_ByteLiftedToNull;  // static @ 0x98
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16LiftedToNull;  // static @ 0xa0
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32LiftedToNull;  // static @ 0xa8
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64LiftedToNull;  // static @ 0xb0
        private static System.Linq.Expressions.Interpreter.Instruction s_SingleLiftedToNull;  // static @ 0xb8
        private static System.Linq.Expressions.Interpreter.Instruction s_DoubleLiftedToNull;  // static @ 0xc0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACD39CC */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60006AF
        private System.Void .ctor() { }
        // RVA: 0x0ACD2E08  token: 0x60006B0
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull) { }

    }

    // TypeToken: 0x2000132  // size: 0x10
    public abstract class ExclusiveOrInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_Boolean;  // static @ 0x40

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACD8550 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60006E6
        private System.Void .ctor() { }
        // RVA: 0x0ACD80C0  token: 0x60006E7
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x200013C  // size: 0x18
    public abstract class FieldInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        protected readonly System.Reflection.FieldInfo _field;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60006FA
        public System.Void .ctor(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACE505C  token: 0x60006FB
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200013D  // size: 0x18
    public sealed class LoadStaticFieldInstruction : System.Linq.Expressions.Interpreter.FieldInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACFCB10 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60006FC
        public System.Void .ctor(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACFCACC  token: 0x60006FF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200013E  // size: 0x18
    public sealed class LoadFieldInstruction : System.Linq.Expressions.Interpreter.FieldInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACFCAA0 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000700
        public System.Void .ctor(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACFCA40  token: 0x6000704
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x200013F  // size: 0x18
    public sealed class StoreFieldInstruction : System.Linq.Expressions.Interpreter.FieldInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACFCBF0 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000705
        public System.Void .ctor(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACFCB88  token: 0x6000708
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000140  // size: 0x18
    public sealed class StoreStaticFieldInstruction : System.Linq.Expressions.Interpreter.FieldInstruction
    {
        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACFCC60 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000709
        public System.Void .ctor(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACFCC1C  token: 0x600070C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000141  // size: 0x18
    public abstract class GreaterThanInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Object _nullValue;  // 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Char;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSByte;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt16;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullChar;  // static @ 0x68
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt32;  // static @ 0x70
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt64;  // static @ 0x78
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullByte;  // static @ 0x80
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt16;  // static @ 0x88
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt32;  // static @ 0x90
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt64;  // static @ 0x98
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSingle;  // static @ 0xa0
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullDouble;  // static @ 0xa8

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACE5FC4 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000710
        private System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE5314  token: 0x6000711
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull) { }

    }

    // TypeToken: 0x200014D  // size: 0x18
    public abstract class GreaterThanOrEqualInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Object _nullValue;  // 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Char;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSByte;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt16;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullChar;  // static @ 0x68
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt32;  // static @ 0x70
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt64;  // static @ 0x78
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullByte;  // static @ 0x80
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt16;  // static @ 0x88
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt32;  // static @ 0x90
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt64;  // static @ 0x98
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSingle;  // static @ 0xa0
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullDouble;  // static @ 0xa8

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACE70F4 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600072B
        private System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACE6444  token: 0x600072C
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull) { }

    }

    // TypeToken: 0x2000159  // size: 0x10
    public abstract class IncrementInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACE7F28 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000746
        private System.Void .ctor() { }
        // RVA: 0x0ACE7B18  token: 0x6000747
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x2000162  // size: 0x10
    public abstract class Instruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x011EC580 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x011EC580 */ }
        System.Int32 ConsumedContinuations { get; /* RVA: 0x011EC580 */ }
        System.Int32 ProducedContinuations { get; /* RVA: 0x011EC580 */ }
        System.Int32 StackBalance { get; /* RVA: 0x0ACEC2F4 */ }
        System.Int32 ContinuationsBalance { get; /* RVA: 0x0ACEC2BC */ }
        System.String InstructionName { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600075E
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACEC270  token: 0x6000760
        public virtual System.String ToString() { }
        // RVA: 0x05397C78  token: 0x6000761
        public virtual System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects) { }
        // RVA: 0x09D3CD3C  token: 0x6000762
        protected static System.Void NullCheck(System.Object o) { }
        // RVA: 0x041E1670  token: 0x6000763
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000163  // size: 0x38
    public sealed struct InstructionArray
    {
        // Fields
        private readonly System.Int32 MaxStackDepth;  // 0x10
        private readonly System.Int32 MaxContinuationDepth;  // 0x14
        private readonly System.Linq.Expressions.Interpreter.Instruction[] Instructions;  // 0x18
        private readonly System.Object[] Objects;  // 0x20
        private readonly System.Linq.Expressions.Interpreter.RuntimeLabel[] Labels;  // 0x28
        private readonly System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>> DebugCookies;  // 0x30

        // Methods
        // RVA: 0x0ACE831C  token: 0x6000764
        private System.Void .ctor(System.Int32 maxStackDepth, System.Int32 maxContinuationDepth, System.Linq.Expressions.Interpreter.Instruction[] instructions, System.Object[] objects, System.Linq.Expressions.Interpreter.RuntimeLabel[] labels, System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>> debugCookies) { }

    }

    // TypeToken: 0x2000165  // size: 0x48
    public sealed class InstructionList
    {
        // Fields
        private readonly System.Collections.Generic.List<System.Linq.Expressions.Interpreter.Instruction> _instructions;  // 0x10
        private System.Collections.Generic.List<System.Object> _objects;  // 0x18
        private System.Int32 _currentStackDepth;  // 0x20
        private System.Int32 _maxStackDepth;  // 0x24
        private System.Int32 _currentContinuationsDepth;  // 0x28
        private System.Int32 _maxContinuationDepth;  // 0x2c
        private System.Int32 _runtimeLabelCount;  // 0x30
        private System.Collections.Generic.List<System.Linq.Expressions.Interpreter.BranchLabel> _labels;  // 0x38
        private System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>> _debugCookies;  // 0x40
        private static System.Linq.Expressions.Interpreter.Instruction s_null;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_true;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_false;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction[] s_Ints;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction[] s_loadObjectCached;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction[] s_loadLocal;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction[] s_loadLocalBoxed;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction[] s_loadLocalFromClosure;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction[] s_loadLocalFromClosureBoxed;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.Instruction[] s_assignLocal;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.Instruction[] s_storeLocal;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.Instruction[] s_assignLocalBoxed;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.Instruction[] s_storeLocalBoxed;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.Instruction[] s_assignLocalToClosure;  // static @ 0x68
        private static readonly System.Collections.Generic.Dictionary<System.Reflection.FieldInfo,System.Linq.Expressions.Interpreter.Instruction> s_loadFields;  // static @ 0x70
        private static readonly System.Linq.Expressions.Interpreter.RuntimeLabel[] s_emptyRuntimeLabels;  // static @ 0x78

        // Properties
        System.Int32 Count { get; /* RVA: 0x0ACEC1F4 */ }
        System.Int32 CurrentStackDepth { get; /* RVA: 0x02B70F90 */ }
        System.Int32 CurrentContinuationsDepth { get; /* RVA: 0x011F2A90 */ }

        // Methods
        // RVA: 0x0ACEB348  token: 0x6000768
        public System.Void Emit(System.Linq.Expressions.Interpreter.Instruction instruction) { }
        // RVA: 0x0ACEBFE0  token: 0x6000769
        private System.Void UpdateStackDepth(System.Linq.Expressions.Interpreter.Instruction instruction) { }
        // RVA: 0x0ACEBEEC  token: 0x600076A
        public System.Void UnEmit() { }
        // RVA: 0x0ACEB4A4  token: 0x600076E
        private System.Linq.Expressions.Interpreter.Instruction GetInstruction(System.Int32 index) { }
        // RVA: 0x0ACEBE04  token: 0x600076F
        public System.Linq.Expressions.Interpreter.InstructionArray ToArray() { }
        // RVA: 0x0ACEA700  token: 0x6000770
        public System.Void EmitLoad(System.Object value) { }
        // RVA: 0x0ACEA568  token: 0x6000771
        public System.Void EmitLoad(System.Boolean value) { }
        // RVA: 0x0ACEA0A4  token: 0x6000772
        public System.Void EmitLoad(System.Object value, System.Type type) { }
        // RVA: 0x0ACE92D8  token: 0x6000773
        public System.Void EmitDup() { }
        // RVA: 0x0ACEAC30  token: 0x6000774
        public System.Void EmitPop() { }
        // RVA: 0x0ACEBC98  token: 0x6000775
        private System.Void SwitchToBoxed(System.Int32 index, System.Int32 instructionIndex) { }
        // RVA: 0x0ACE9EF0  token: 0x6000776
        public System.Void EmitLoadLocal(System.Int32 index) { }
        // RVA: 0x0ACE9B30  token: 0x6000777
        public System.Void EmitLoadLocalBoxed(System.Int32 index) { }
        // RVA: 0x0ACEB7AC  token: 0x6000778
        private static System.Linq.Expressions.Interpreter.Instruction LoadLocalBoxed(System.Int32 index) { }
        // RVA: 0x0ACE9D3C  token: 0x6000779
        public System.Void EmitLoadLocalFromClosure(System.Int32 index) { }
        // RVA: 0x0ACE9B88  token: 0x600077A
        public System.Void EmitLoadLocalFromClosureBoxed(System.Int32 index) { }
        // RVA: 0x0ACE89E8  token: 0x600077B
        public System.Void EmitAssignLocal(System.Int32 index) { }
        // RVA: 0x0ACEAEE0  token: 0x600077C
        public System.Void EmitStoreLocal(System.Int32 index) { }
        // RVA: 0x0ACE87DC  token: 0x600077D
        public System.Void EmitAssignLocalBoxed(System.Int32 index) { }
        // RVA: 0x0ACE8370  token: 0x600077E
        private static System.Linq.Expressions.Interpreter.Instruction AssignLocalBoxed(System.Int32 index) { }
        // RVA: 0x0ACEAE68  token: 0x600077F
        public System.Void EmitStoreLocalBoxed(System.Int32 index) { }
        // RVA: 0x0ACEBB04  token: 0x6000780
        private static System.Linq.Expressions.Interpreter.Instruction StoreLocalBoxed(System.Int32 index) { }
        // RVA: 0x0ACE8834  token: 0x6000781
        public System.Void EmitAssignLocalToClosure(System.Int32 index) { }
        // RVA: 0x0ACEAEC0  token: 0x6000782
        public System.Void EmitStoreLocalToClosure(System.Int32 index) { }
        // RVA: 0x0ACE97B0  token: 0x6000783
        public System.Void EmitInitializeLocal(System.Int32 index, System.Type type) { }
        // RVA: 0x0ACE98A8  token: 0x6000784
        private System.Void EmitInitializeParameter(System.Int32 index) { }
        // RVA: 0x0ACEBAC0  token: 0x6000785
        private static System.Linq.Expressions.Interpreter.Instruction Parameter(System.Int32 index) { }
        // RVA: 0x0ACEBA7C  token: 0x6000786
        private static System.Linq.Expressions.Interpreter.Instruction ParameterBox(System.Int32 index) { }
        // RVA: 0x0ACEB768  token: 0x6000787
        private static System.Linq.Expressions.Interpreter.Instruction InitReference(System.Int32 index) { }
        // RVA: 0x0ACEB724  token: 0x6000788
        private static System.Linq.Expressions.Interpreter.Instruction InitImmutableRefBox(System.Int32 index) { }
        // RVA: 0x0ACEA958  token: 0x6000789
        public System.Void EmitNewRuntimeVariables(System.Int32 count) { }
        // RVA: 0x0ACE9634  token: 0x600078A
        public System.Void EmitGetArrayItem() { }
        // RVA: 0x0ACEAD58  token: 0x600078B
        public System.Void EmitSetArrayItem() { }
        // RVA: 0x0ACEA8E0  token: 0x600078C
        public System.Void EmitNewArray(System.Type elementType) { }
        // RVA: 0x0ACEA7D0  token: 0x600078D
        public System.Void EmitNewArrayBounds(System.Type elementType, System.Int32 rank) { }
        // RVA: 0x0ACEA858  token: 0x600078E
        public System.Void EmitNewArrayInit(System.Type elementType, System.Int32 elementCount) { }
        // RVA: 0x0ACE871C  token: 0x600078F
        public System.Void EmitAdd(System.Type type, System.Boolean checked) { }
        // RVA: 0x0ACEB11C  token: 0x6000790
        public System.Void EmitSub(System.Type type, System.Boolean checked) { }
        // RVA: 0x0ACEA738  token: 0x6000791
        public System.Void EmitMul(System.Type type, System.Boolean checked) { }
        // RVA: 0x0ACE92AC  token: 0x6000792
        public System.Void EmitDiv(System.Type type) { }
        // RVA: 0x0ACEA70C  token: 0x6000793
        public System.Void EmitModulo(System.Type type) { }
        // RVA: 0x0ACE9608  token: 0x6000794
        public System.Void EmitExclusiveOr(System.Type type) { }
        // RVA: 0x0ACE875C  token: 0x6000795
        public System.Void EmitAnd(System.Type type) { }
        // RVA: 0x0ACEAC04  token: 0x6000796
        public System.Void EmitOr(System.Type type) { }
        // RVA: 0x0ACE9A78  token: 0x6000797
        public System.Void EmitLeftShift(System.Type type) { }
        // RVA: 0x0ACEAD2C  token: 0x6000798
        public System.Void EmitRightShift(System.Type type) { }
        // RVA: 0x0ACE95D4  token: 0x6000799
        public System.Void EmitEqual(System.Type type, System.Boolean liftedToNull) { }
        // RVA: 0x0ACEAA44  token: 0x600079A
        public System.Void EmitNotEqual(System.Type type, System.Boolean liftedToNull) { }
        // RVA: 0x0ACE9AD8  token: 0x600079B
        public System.Void EmitLessThan(System.Type type, System.Boolean liftedToNull) { }
        // RVA: 0x0ACE9AA4  token: 0x600079C
        public System.Void EmitLessThanOrEqual(System.Type type, System.Boolean liftedToNull) { }
        // RVA: 0x0ACE9750  token: 0x600079D
        public System.Void EmitGreaterThan(System.Type type, System.Boolean liftedToNull) { }
        // RVA: 0x0ACE971C  token: 0x600079E
        public System.Void EmitGreaterThanOrEqual(System.Type type, System.Boolean liftedToNull) { }
        // RVA: 0x0ACEAB04  token: 0x600079F
        public System.Void EmitNumericConvertChecked(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull) { }
        // RVA: 0x0ACEAB84  token: 0x60007A0
        public System.Void EmitNumericConvertUnchecked(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull) { }
        // RVA: 0x0ACE911C  token: 0x60007A1
        public System.Void EmitConvertToUnderlying(System.TypeCode to, System.Boolean isLiftedToNull) { }
        // RVA: 0x0ACE9088  token: 0x60007A2
        public System.Void EmitCast(System.Type toType) { }
        // RVA: 0x0ACE9010  token: 0x60007A3
        public System.Void EmitCastToEnum(System.Type toType) { }
        // RVA: 0x0ACE8F98  token: 0x60007A4
        public System.Void EmitCastReferenceToEnum(System.Type toType) { }
        // RVA: 0x0ACEAA78  token: 0x60007A5
        public System.Void EmitNot(System.Type type) { }
        // RVA: 0x0ACE9234  token: 0x60007A6
        public System.Void EmitDefaultValue(System.Type type) { }
        // RVA: 0x0ACEA9B8  token: 0x60007A7
        public System.Void EmitNew(System.Reflection.ConstructorInfo constructorInfo, System.Reflection.ParameterInfo[] parameters) { }
        // RVA: 0x0ACE8E68  token: 0x60007A8
        public System.Void EmitByRefNew(System.Reflection.ConstructorInfo constructorInfo, System.Reflection.ParameterInfo[] parameters, System.Linq.Expressions.Interpreter.ByRefUpdater[] updaters) { }
        // RVA: 0x0ACE9190  token: 0x60007A9
        private System.Void EmitCreateDelegate(System.Linq.Expressions.Interpreter.LightDelegateCreator creator) { }
        // RVA: 0x0ACEB27C  token: 0x60007AA
        public System.Void EmitTypeEquals() { }
        // RVA: 0x0ACE8788  token: 0x60007AB
        public System.Void EmitArrayLength() { }
        // RVA: 0x0ACEA7A4  token: 0x60007AC
        public System.Void EmitNegate(System.Type type) { }
        // RVA: 0x0ACEA778  token: 0x60007AD
        public System.Void EmitNegateChecked(System.Type type) { }
        // RVA: 0x0ACE9784  token: 0x60007AE
        public System.Void EmitIncrement(System.Type type) { }
        // RVA: 0x0ACE9208  token: 0x60007AF
        public System.Void EmitDecrement(System.Type type) { }
        // RVA: 0x0ACEB2D0  token: 0x60007B0
        public System.Void EmitTypeIs(System.Type type) { }
        // RVA: 0x0ACEB204  token: 0x60007B1
        public System.Void EmitTypeAs(System.Type type) { }
        // RVA: 0x0ACE9B0C  token: 0x60007B2
        public System.Void EmitLoadField(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACEB4F8  token: 0x60007B3
        private System.Linq.Expressions.Interpreter.Instruction GetLoadField(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACEADAC  token: 0x60007B4
        public System.Void EmitStoreField(System.Reflection.FieldInfo field) { }
        // RVA: 0x0ACE8F38  token: 0x60007B5
        public System.Void EmitCall(System.Reflection.MethodInfo method) { }
        // RVA: 0x0ACE8F08  token: 0x60007B6
        public System.Void EmitCall(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters) { }
        // RVA: 0x0ACE8DAC  token: 0x60007B7
        public System.Void EmitByRefCall(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters, System.Linq.Expressions.Interpreter.ByRefUpdater[] byrefArgs) { }
        // RVA: 0x0ACEAAA4  token: 0x60007B8
        public System.Void EmitNullableCall(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters) { }
        // RVA: 0x0ACE8504  token: 0x60007B9
        private System.Linq.Expressions.Interpreter.RuntimeLabel[] BuildRuntimeLabels() { }
        // RVA: 0x0ACEB940  token: 0x60007BA
        public System.Linq.Expressions.Interpreter.BranchLabel MakeLabel() { }
        // RVA: 0x0ACEB3D8  token: 0x60007BB
        private System.Void FixupBranch(System.Int32 branchIndex, System.Int32 offset) { }
        // RVA: 0x0ACEB3AC  token: 0x60007BC
        private System.Int32 EnsureLabelIndex(System.Linq.Expressions.Interpreter.BranchLabel label) { }
        // RVA: 0x0ACEBA3C  token: 0x60007BD
        public System.Int32 MarkRuntimeLabel() { }
        // RVA: 0x0ACEBA18  token: 0x60007BE
        public System.Void MarkLabel(System.Linq.Expressions.Interpreter.BranchLabel label) { }
        // RVA: 0x0ACE9688  token: 0x60007BF
        public System.Void EmitGoto(System.Linq.Expressions.Interpreter.BranchLabel label, System.Boolean hasResult, System.Boolean hasValue, System.Boolean labelTargetGetsValue) { }
        // RVA: 0x0ACE8D60  token: 0x60007C0
        private System.Void EmitBranch(System.Linq.Expressions.Interpreter.OffsetInstruction instruction, System.Linq.Expressions.Interpreter.BranchLabel label) { }
        // RVA: 0x0ACE8C6C  token: 0x60007C1
        public System.Void EmitBranch(System.Linq.Expressions.Interpreter.BranchLabel label) { }
        // RVA: 0x0ACE8CD8  token: 0x60007C2
        public System.Void EmitBranch(System.Linq.Expressions.Interpreter.BranchLabel label, System.Boolean hasResult, System.Boolean hasValue) { }
        // RVA: 0x0ACE90B4  token: 0x60007C3
        public System.Void EmitCoalescingBranch(System.Linq.Expressions.Interpreter.BranchLabel leftNotNull) { }
        // RVA: 0x0ACE8C04  token: 0x60007C4
        public System.Void EmitBranchTrue(System.Linq.Expressions.Interpreter.BranchLabel elseLabel) { }
        // RVA: 0x0ACE8B9C  token: 0x60007C5
        public System.Void EmitBranchFalse(System.Linq.Expressions.Interpreter.BranchLabel elseLabel) { }
        // RVA: 0x0ACEB1B0  token: 0x60007C6
        public System.Void EmitThrow() { }
        // RVA: 0x0ACEB15C  token: 0x60007C7
        public System.Void EmitThrowVoid() { }
        // RVA: 0x0ACEACD8  token: 0x60007C8
        public System.Void EmitRethrow() { }
        // RVA: 0x0ACEAC84  token: 0x60007C9
        public System.Void EmitRethrowVoid() { }
        // RVA: 0x0ACE95A4  token: 0x60007CA
        public System.Void EmitEnterTryFinally(System.Linq.Expressions.Interpreter.BranchLabel finallyStartLabel) { }
        // RVA: 0x0ACE9500  token: 0x60007CB
        public System.Void EmitEnterTryCatch() { }
        // RVA: 0x0ACE9524  token: 0x60007CC
        public System.Linq.Expressions.Interpreter.EnterTryFaultInstruction EmitEnterTryFault(System.Linq.Expressions.Interpreter.BranchLabel tryEnd) { }
        // RVA: 0x0ACE9494  token: 0x60007CD
        public System.Void EmitEnterFinally(System.Linq.Expressions.Interpreter.BranchLabel finallyStartLabel) { }
        // RVA: 0x0ACE9A24  token: 0x60007CE
        public System.Void EmitLeaveFinally() { }
        // RVA: 0x0ACE9428  token: 0x60007CF
        public System.Void EmitEnterFault(System.Linq.Expressions.Interpreter.BranchLabel faultStartLabel) { }
        // RVA: 0x0ACE99D0  token: 0x60007D0
        public System.Void EmitLeaveFault() { }
        // RVA: 0x0ACE932C  token: 0x60007D1
        public System.Void EmitEnterExceptionFilter() { }
        // RVA: 0x0ACE9900  token: 0x60007D2
        public System.Void EmitLeaveExceptionFilter() { }
        // RVA: 0x0ACE9380  token: 0x60007D3
        public System.Void EmitEnterExceptionHandlerNonVoid() { }
        // RVA: 0x0ACE93D4  token: 0x60007D4
        public System.Void EmitEnterExceptionHandlerVoid() { }
        // RVA: 0x0ACE9954  token: 0x60007D5
        public System.Void EmitLeaveExceptionHandler(System.Boolean hasValue, System.Linq.Expressions.Interpreter.BranchLabel tryExpressionEndLabel) { }
        // RVA: -1  // generic def  token: 0x60007D6
        public System.Void EmitIntSwitch(System.Collections.Generic.Dictionary<T,System.Int32> cases) { }
        // RVA: 0x0ACEB094  token: 0x60007D7
        public System.Void EmitStringSwitch(System.Collections.Generic.Dictionary<System.String,System.Int32> cases, System.Runtime.CompilerServices.StrongBox<System.Int32> nullCase) { }
        // RVA: 0x0ACEC17C  token: 0x60007D8
        public System.Void .ctor() { }
        // RVA: 0x0ACEC06C  token: 0x60007D9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000168  // size: 0x50
    public sealed class InterpretedFrame
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.InterpretedFrame s_currentFrame;  // static @ 0xffffffff
        private readonly System.Linq.Expressions.Interpreter.Interpreter Interpreter;  // 0x10
        private System.Linq.Expressions.Interpreter.InterpretedFrame _parent;  // 0x18
        private readonly System.Int32[] _continuations;  // 0x20
        private System.Int32 _continuationIndex;  // 0x28
        private System.Int32 _pendingContinuation;  // 0x2c
        private System.Object _pendingValue;  // 0x30
        public readonly System.Object[] Data;  // 0x38
        public readonly System.Runtime.CompilerServices.IStrongBox[] Closure;  // 0x40
        public System.Int32 StackIndex;  // 0x48
        public System.Int32 InstructionIndex;  // 0x4c

        // Properties
        System.String Name { get; /* RVA: 0x0ACED078 */ }
        System.Linq.Expressions.Interpreter.InterpretedFrame Parent { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0ACECF7C  token: 0x60007DD
        private System.Void .ctor(System.Linq.Expressions.Interpreter.Interpreter interpreter, System.Runtime.CompilerServices.IStrongBox[] closure) { }
        // RVA: 0x0ACEC49C  token: 0x60007DE
        public System.Linq.Expressions.Interpreter.DebugInfo GetDebugInfo(System.Int32 instructionIndex) { }
        // RVA: 0x0ACEC840  token: 0x60007E0
        public System.Void Push(System.Object value) { }
        // RVA: 0x0ACECAB8  token: 0x60007E1
        public System.Void Push(System.Boolean value) { }
        // RVA: 0x0ACECA58  token: 0x60007E2
        public System.Void Push(System.Int32 value) { }
        // RVA: 0x0ACEC928  token: 0x60007E3
        public System.Void Push(System.Byte value) { }
        // RVA: 0x0ACEC9C0  token: 0x60007E4
        public System.Void Push(System.SByte value) { }
        // RVA: 0x0ACEC890  token: 0x60007E5
        public System.Void Push(System.Int16 value) { }
        // RVA: 0x0ACECB5C  token: 0x60007E6
        public System.Void Push(System.UInt16 value) { }
        // RVA: 0x0ACEC75C  token: 0x60007E7
        public System.Object Pop() { }
        // RVA: 0x0ACECD50  token: 0x60007E8
        private System.Void SetStackDepth(System.Int32 depth) { }
        // RVA: 0x0ACEC6CC  token: 0x60007E9
        public System.Object Peek() { }
        // RVA: 0x0ACEC3A0  token: 0x60007EA
        public System.Void Dup() { }
        // RVA: 0x0ACEC500  token: 0x60007EC
        public System.Collections.Generic.IEnumerable<System.Linq.Expressions.Interpreter.InterpretedFrameInfo> GetStackTraceDebugInfo() { }
        // RVA: 0x0ACECBF4  token: 0x60007ED
        private System.Void SaveTraceToException(System.Exception exception) { }
        // RVA: 0x0ACEC414  token: 0x60007EE
        private System.Linq.Expressions.Interpreter.InterpretedFrame Enter() { }
        // RVA: 0x0ACEC670  token: 0x60007EF
        private System.Void Leave(System.Linq.Expressions.Interpreter.InterpretedFrame prevFrame) { }
        // RVA: 0x03BE2580  token: 0x60007F0
        private System.Boolean IsJumpHappened() { }
        // RVA: 0x04D9EBE0  token: 0x60007F1
        public System.Void RemoveContinuation() { }
        // RVA: 0x0ACEC794  token: 0x60007F2
        public System.Void PushContinuation(System.Int32 continuation) { }
        // RVA: 0x0ACECD74  token: 0x60007F3
        public System.Int32 YieldToCurrentContinuation() { }
        // RVA: 0x0ACECDF0  token: 0x60007F4
        public System.Int32 YieldToPendingContinuation() { }
        // RVA: 0x0ACEC7C8  token: 0x60007F5
        private System.Void PushPendingContinuation() { }
        // RVA: 0x0ACEC6FC  token: 0x60007F6
        private System.Void PopPendingContinuation() { }
        // RVA: 0x0ACEC570  token: 0x60007F7
        public System.Int32 Goto(System.Int32 labelIndex, System.Object value, System.Boolean gotoExceptionHandler) { }

    }

    // TypeToken: 0x200016A  // size: 0x68
    public sealed class Interpreter
    {
        // Fields
        private static readonly System.Object NoValue;  // static @ 0x0
        private readonly System.Linq.Expressions.Interpreter.InstructionArray _instructions;  // 0x10
        private readonly System.Object[] _objects;  // 0x38
        private readonly System.Linq.Expressions.Interpreter.RuntimeLabel[] _labels;  // 0x40
        private readonly System.Linq.Expressions.Interpreter.DebugInfo[] _debugInfos;  // 0x48
        private readonly System.String <Name>k__BackingField;  // 0x50
        private readonly System.Int32 <LocalCount>k__BackingField;  // 0x58
        private readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> <ClosureVariables>k__BackingField;  // 0x60

        // Properties
        System.String Name { get; /* RVA: 0x04D86270 */ }
        System.Int32 LocalCount { get; /* RVA: 0x04D86570 */ }
        System.Int32 ClosureSize { get; /* RVA: 0x0ACED20C */ }
        System.Linq.Expressions.Interpreter.InstructionArray Instructions { get; /* RVA: 0x02B2D2D0 */ }
        System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> ClosureVariables { get; /* RVA: 0x04D86280 */ }

        // Methods
        // RVA: 0x0ACED168  token: 0x6000800
        private System.Void .ctor(System.String name, System.Linq.Expressions.Interpreter.LocalVariables locals, System.Linq.Expressions.Interpreter.InstructionArray instructions, System.Linq.Expressions.Interpreter.DebugInfo[] debugInfos) { }
        // RVA: 0x0ACED094  token: 0x6000806
        public System.Void Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACED0F8  token: 0x6000807
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200016B  // size: 0x38
    public sealed class LabelInfo
    {
        // Fields
        private readonly System.Linq.Expressions.LabelTarget _node;  // 0x10
        private System.Linq.Expressions.Interpreter.BranchLabel _label;  // 0x18
        private System.Object _definitions;  // 0x20
        private readonly System.Collections.Generic.List<System.Linq.Expressions.Interpreter.LabelScopeInfo> _references;  // 0x28
        private System.Boolean _acrossBlockJump;  // 0x30

        // Properties
        System.Boolean HasDefinitions { get; /* RVA: 0x09B00070 */ }
        System.Boolean HasMultipleDefinitions { get; /* RVA: 0x0ACEDCB4 */ }

        // Methods
        // RVA: 0x0ACEDC24  token: 0x6000808
        private System.Void .ctor(System.Linq.Expressions.LabelTarget node) { }
        // RVA: 0x0ACED83C  token: 0x6000809
        private System.Linq.Expressions.Interpreter.BranchLabel GetLabel(System.Linq.Expressions.Interpreter.LightCompiler compiler) { }
        // RVA: 0x0ACED858  token: 0x600080A
        private System.Void Reference(System.Linq.Expressions.Interpreter.LabelScopeInfo block) { }
        // RVA: 0x0ACED368  token: 0x600080B
        private System.Void Define(System.Linq.Expressions.Interpreter.LabelScopeInfo block) { }
        // RVA: 0x0ACED93C  token: 0x600080C
        private System.Void ValidateJump(System.Linq.Expressions.Interpreter.LabelScopeInfo reference) { }
        // RVA: 0x0ACED8C4  token: 0x600080D
        private System.Void ValidateFinish() { }
        // RVA: 0x0ACED660  token: 0x600080E
        private System.Void EnsureLabel(System.Linq.Expressions.Interpreter.LightCompiler compiler) { }
        // RVA: 0x0ACED5E8  token: 0x600080F
        private System.Boolean DefinedIn(System.Linq.Expressions.Interpreter.LabelScopeInfo scope) { }
        // RVA: 0x0ACED6A0  token: 0x6000811
        private System.Linq.Expressions.Interpreter.LabelScopeInfo FirstDefinition() { }
        // RVA: 0x0ACED24C  token: 0x6000812
        private System.Void AddDefinition(System.Linq.Expressions.Interpreter.LabelScopeInfo scope) { }
        // RVA: -1  // generic def  token: 0x6000814
        private static T CommonNode(T first, T second, System.Func<T,T> parent) { }

    }

    // TypeToken: 0x200016D  // size: 0x14
    public sealed struct LabelScopeKind
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Statement;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Block;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Switch;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Lambda;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Try;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Catch;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Finally;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Filter;  // const
        public static System.Linq.Expressions.Interpreter.LabelScopeKind Expression;  // const

    }

    // TypeToken: 0x200016E  // size: 0x28
    public sealed class LabelScopeInfo
    {
        // Fields
        private System.Linq.Expressions.Interpreter.HybridReferenceDictionary<System.Linq.Expressions.LabelTarget,System.Linq.Expressions.Interpreter.LabelInfo> _labels;  // 0x10
        private readonly System.Linq.Expressions.Interpreter.LabelScopeKind Kind;  // 0x18
        private readonly System.Linq.Expressions.Interpreter.LabelScopeInfo Parent;  // 0x20

        // Properties
        System.Boolean CanJumpInto { get; /* RVA: 0x0ACEDE88 */ }

        // Methods
        // RVA: 0x0ACEDE68  token: 0x6000818
        private System.Void .ctor(System.Linq.Expressions.Interpreter.LabelScopeInfo parent, System.Linq.Expressions.Interpreter.LabelScopeKind kind) { }
        // RVA: 0x0ACEDD9C  token: 0x600081A
        private System.Boolean ContainsTarget(System.Linq.Expressions.LabelTarget target) { }
        // RVA: 0x0ACEDDF4  token: 0x600081B
        private System.Boolean TryGetLabelInfo(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Interpreter.LabelInfo& info) { }
        // RVA: 0x0ACEDCF8  token: 0x600081C
        private System.Void AddLabelInfo(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Interpreter.LabelInfo info) { }

    }

    // TypeToken: 0x200016F  // size: 0x10
    public abstract class LeftShiftInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACEE360 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000820
        private System.Void .ctor() { }
        // RVA: 0x0ACEDF50  token: 0x6000821
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x2000178  // size: 0x18
    public abstract class LessThanInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Object _nullValue;  // 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Char;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSByte;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt16;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullChar;  // static @ 0x68
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt32;  // static @ 0x70
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt64;  // static @ 0x78
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullByte;  // static @ 0x80
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt16;  // static @ 0x88
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt32;  // static @ 0x90
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt64;  // static @ 0x98
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSingle;  // static @ 0xa0
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullDouble;  // static @ 0xa8

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACEF7C8 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000835
        private System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEEB18  token: 0x6000836
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull) { }

    }

    // TypeToken: 0x2000184  // size: 0x18
    public abstract class LessThanOrEqualInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Object _nullValue;  // 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Char;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSByte;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt16;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullChar;  // static @ 0x68
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt32;  // static @ 0x70
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullInt64;  // static @ 0x78
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullByte;  // static @ 0x80
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt16;  // static @ 0x88
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt32;  // static @ 0x90
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullUInt64;  // static @ 0x98
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullSingle;  // static @ 0xa0
        private static System.Linq.Expressions.Interpreter.Instruction s_liftedToNullDouble;  // static @ 0xa8

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACF08F8 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000850
        private System.Void .ctor(System.Object nullValue) { }
        // RVA: 0x0ACEFC48  token: 0x6000851
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull) { }

    }

    // TypeToken: 0x2000190  // size: 0x20
    public sealed class ExceptionFilter
    {
        // Fields
        public readonly System.Int32 LabelIndex;  // 0x10
        public readonly System.Int32 StartIndex;  // 0x14
        public readonly System.Int32 EndIndex;  // 0x18

        // Methods
        // RVA: 0x04D88580  token: 0x6000868
        private System.Void .ctor(System.Int32 labelIndex, System.Int32 start, System.Int32 end) { }

    }

    // TypeToken: 0x2000191  // size: 0x30
    public sealed class ExceptionHandler
    {
        // Fields
        private readonly System.Type _exceptionType;  // 0x10
        public readonly System.Int32 LabelIndex;  // 0x18
        public readonly System.Int32 HandlerStartIndex;  // 0x1c
        public readonly System.Int32 HandlerEndIndex;  // 0x20
        public readonly System.Linq.Expressions.Interpreter.ExceptionFilter Filter;  // 0x28

        // Methods
        // RVA: 0x0ACE4C8C  token: 0x6000869
        private System.Void .ctor(System.Int32 labelIndex, System.Int32 handlerStartIndex, System.Int32 handlerEndIndex, System.Type exceptionType, System.Linq.Expressions.Interpreter.ExceptionFilter filter) { }
        // RVA: 0x0ACE4B80  token: 0x600086A
        public System.Boolean Matches(System.Type exceptionType) { }
        // RVA: 0x0ACE4BA4  token: 0x600086B
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000192  // size: 0x30
    public sealed class TryCatchFinallyHandler
    {
        // Fields
        private readonly System.Int32 TryStartIndex;  // 0x10
        private readonly System.Int32 TryEndIndex;  // 0x14
        private readonly System.Int32 FinallyStartIndex;  // 0x18
        private readonly System.Int32 FinallyEndIndex;  // 0x1c
        private readonly System.Int32 GotoEndTargetIndex;  // 0x20
        private readonly System.Linq.Expressions.Interpreter.ExceptionHandler[] _handlers;  // 0x28

        // Properties
        System.Boolean IsFinallyBlockExist { get; /* RVA: 0x0ACFD008 */ }
        System.Linq.Expressions.Interpreter.ExceptionHandler[] Handlers { get; /* RVA: 0x04D86240 */ }
        System.Boolean IsCatchBlockExist { get; /* RVA: 0x068C60A8 */ }

        // Methods
        // RVA: 0x0ACFCFDC  token: 0x600086F
        private System.Void .ctor(System.Int32 tryStart, System.Int32 tryEnd, System.Int32 gotoEndTargetIndex, System.Linq.Expressions.Interpreter.ExceptionHandler[] handlers) { }
        // RVA: 0x0ACFCFB0  token: 0x6000870
        private System.Void .ctor(System.Int32 tryStart, System.Int32 tryEnd, System.Int32 gotoEndLabelIndex, System.Int32 finallyStart, System.Int32 finallyEnd, System.Linq.Expressions.Interpreter.ExceptionHandler[] handlers) { }
        // RVA: 0x0ACFCE54  token: 0x6000871
        private System.Boolean HasHandler(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Exception exception, System.Linq.Expressions.Interpreter.ExceptionHandler& handler, System.Object& unwrappedException) { }
        // RVA: 0x0ACFCC8C  token: 0x6000872
        private static System.Boolean FilterPasses(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object& exception, System.Linq.Expressions.Interpreter.ExceptionFilter filter) { }

    }

    // TypeToken: 0x2000193  // size: 0x20
    public sealed class TryFaultHandler
    {
        // Fields
        private readonly System.Int32 TryStartIndex;  // 0x10
        private readonly System.Int32 TryEndIndex;  // 0x14
        private readonly System.Int32 FinallyStartIndex;  // 0x18
        private readonly System.Int32 FinallyEndIndex;  // 0x1c

        // Methods
        // RVA: 0x04D8B5C0  token: 0x6000873
        private System.Void .ctor(System.Int32 tryStart, System.Int32 tryEnd, System.Int32 finallyStart, System.Int32 finallyEnd) { }

    }

    // TypeToken: 0x2000194  // size: 0x90
    public sealed class RethrowException : System.Exception
    {
        // Methods
        // RVA: 0x0ACFCB3C  token: 0x6000874
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000195  // size: 0x30
    public sealed class DebugInfo
    {
        // Fields
        public System.Int32 StartLine;  // 0x10
        public System.Int32 EndLine;  // 0x14
        public System.Int32 Index;  // 0x18
        public System.String FileName;  // 0x20
        public System.Boolean IsClear;  // 0x28
        private static readonly System.Linq.Expressions.Interpreter.DebugInfo.DebugInfoComparer s_debugComparer;  // static @ 0x0

        // Methods
        // RVA: 0x0ACE43B4  token: 0x6000875
        public static System.Linq.Expressions.Interpreter.DebugInfo GetMatchingDebugInfo(System.Linq.Expressions.Interpreter.DebugInfo[] debugInfos, System.Int32 index) { }
        // RVA: 0x0ACE4474  token: 0x6000876
        public virtual System.String ToString() { }
        // RVA: 0x041E1670  token: 0x6000877
        public System.Void .ctor() { }
        // RVA: 0x0ACE463C  token: 0x6000878
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000197  // size: 0x20
    public sealed struct InterpretedFrameInfo
    {
        // Fields
        private readonly System.String _methodName;  // 0x10
        private readonly System.Linq.Expressions.Interpreter.DebugInfo _debugInfo;  // 0x18

        // Methods
        // RVA: 0x039CF630  token: 0x600087B
        public System.Void .ctor(System.String methodName, System.Linq.Expressions.Interpreter.DebugInfo info) { }
        // RVA: 0x0ACEC32C  token: 0x600087C
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000198  // size: 0x50
    public sealed class LightCompiler
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.InstructionList _instructions;  // 0x10
        private readonly System.Linq.Expressions.Interpreter.LocalVariables _locals;  // 0x18
        private readonly System.Collections.Generic.List<System.Linq.Expressions.Interpreter.DebugInfo> _debugInfos;  // 0x20
        private readonly System.Linq.Expressions.Interpreter.HybridReferenceDictionary<System.Linq.Expressions.LabelTarget,System.Linq.Expressions.Interpreter.LabelInfo> _treeLabels;  // 0x28
        private System.Linq.Expressions.Interpreter.LabelScopeInfo _labelBlock;  // 0x30
        private readonly System.Collections.Generic.Stack<System.Linq.Expressions.ParameterExpression> _exceptionForRethrowStack;  // 0x38
        private readonly System.Linq.Expressions.Interpreter.LightCompiler _parent;  // 0x40
        private readonly System.Linq.Expressions.StackGuard _guard;  // 0x48
        private static readonly System.Linq.Expressions.Interpreter.LocalDefinition[] s_emptyLocals;  // static @ 0x0

        // Properties
        System.Linq.Expressions.Interpreter.InstructionList Instructions { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0ACFC864  token: 0x600087D
        public System.Void .ctor() { }
        // RVA: 0x0ACFC834  token: 0x600087E
        private System.Void .ctor(System.Linq.Expressions.Interpreter.LightCompiler parent) { }
        // RVA: 0x0ACF9B64  token: 0x6000880
        public System.Linq.Expressions.Interpreter.LightDelegateCreator CompileTop(System.Linq.Expressions.LambdaExpression node) { }
        // RVA: 0x0ACFBED4  token: 0x6000881
        private System.Linq.Expressions.Interpreter.Interpreter MakeInterpreter(System.String lambdaName) { }
        // RVA: 0x0ACF3FC0  token: 0x6000882
        private System.Void CompileConstantExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF4C34  token: 0x6000883
        private System.Void CompileDefaultExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF4B54  token: 0x6000884
        private System.Void CompileDefaultExpression(System.Type type) { }
        // RVA: 0x0ACFBB60  token: 0x6000885
        private System.Linq.Expressions.Interpreter.LocalVariable EnsureAvailableForClosure(System.Linq.Expressions.ParameterExpression expr) { }
        // RVA: 0x0ACFC26C  token: 0x6000886
        private System.Linq.Expressions.Interpreter.LocalVariable ResolveLocal(System.Linq.Expressions.ParameterExpression variable) { }
        // RVA: 0x0ACF4D34  token: 0x6000887
        private System.Void CompileGetVariable(System.Linq.Expressions.ParameterExpression variable) { }
        // RVA: 0x0ACFB718  token: 0x6000888
        private System.Void EmitCopyValueType(System.Type valueType) { }
        // RVA: 0x0ACFBE6C  token: 0x6000889
        private System.Void LoadLocalNoValueTypeCopy(System.Linq.Expressions.ParameterExpression variable) { }
        // RVA: 0x0ACFC130  token: 0x600088A
        private System.Boolean MaybeMutableValueType(System.Type type) { }
        // RVA: 0x0ACF4CE4  token: 0x600088B
        private System.Void CompileGetBoxedVariable(System.Linq.Expressions.ParameterExpression variable) { }
        // RVA: 0x0ACF8CAC  token: 0x600088C
        private System.Void CompileSetVariable(System.Linq.Expressions.ParameterExpression variable, System.Boolean isVoid) { }
        // RVA: 0x0ACF8794  token: 0x600088D
        private System.Void CompileParameterExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF30B4  token: 0x600088E
        private System.Void CompileBlockExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid) { }
        // RVA: 0x0ACF31CC  token: 0x600088F
        private System.Linq.Expressions.Interpreter.LocalDefinition[] CompileBlockStart(System.Linq.Expressions.BlockExpression node) { }
        // RVA: 0x0ACF3028  token: 0x6000890
        private System.Void CompileBlockEnd(System.Linq.Expressions.Interpreter.LocalDefinition[] locals) { }
        // RVA: 0x0ACF51B4  token: 0x6000891
        private System.Void CompileIndexExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFB798  token: 0x6000892
        private System.Void EmitIndexGet(System.Linq.Expressions.IndexExpression index) { }
        // RVA: 0x0ACF4F1C  token: 0x6000893
        private System.Void CompileIndexAssignment(System.Linq.Expressions.BinaryExpression node, System.Boolean asVoid) { }
        // RVA: 0x0ACF69CC  token: 0x6000894
        private System.Void CompileMemberAssignment(System.Linq.Expressions.BinaryExpression node, System.Boolean asVoid) { }
        // RVA: 0x0ACF6664  token: 0x6000895
        private System.Void CompileMemberAssignment(System.Boolean asVoid, System.Reflection.MemberInfo refMember, System.Linq.Expressions.Expression value, System.Boolean forBinding) { }
        // RVA: 0x0ACFB488  token: 0x6000896
        private System.Void CompileVariableAssignment(System.Linq.Expressions.BinaryExpression node, System.Boolean asVoid) { }
        // RVA: 0x0ACF2130  token: 0x6000897
        private System.Void CompileAssignBinaryExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid) { }
        // RVA: 0x0ACF2234  token: 0x6000898
        private System.Void CompileBinaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF4C68  token: 0x6000899
        private System.Void CompileEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftedToNull) { }
        // RVA: 0x0ACF8664  token: 0x600089A
        private System.Void CompileNotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftedToNull) { }
        // RVA: 0x0ACF3C08  token: 0x600089B
        private System.Void CompileComparison(System.Linq.Expressions.BinaryExpression node) { }
        // RVA: 0x0ACF1B6C  token: 0x600089C
        private System.Void CompileArithmetic(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right) { }
        // RVA: 0x0ACF44D0  token: 0x600089D
        private System.Void CompileConvertUnaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF403C  token: 0x600089E
        private System.Void CompileConvertToType(System.Type typeFrom, System.Type typeTo, System.Boolean isChecked, System.Boolean isLiftedToNull) { }
        // RVA: 0x0ACF86E0  token: 0x600089F
        private System.Void CompileNotExpression(System.Linq.Expressions.UnaryExpression node) { }
        // RVA: 0x0ACFAF78  token: 0x60008A0
        private System.Void CompileUnaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFBA5C  token: 0x60008A1
        private System.Void EmitUnaryMethodCall(System.Linq.Expressions.UnaryExpression node) { }
        // RVA: 0x0ACFB884  token: 0x60008A2
        private System.Void EmitUnaryBoolCheck(System.Linq.Expressions.UnaryExpression node) { }
        // RVA: 0x0ACF1B14  token: 0x60008A3
        private System.Void CompileAndAlsoBinaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF873C  token: 0x60008A4
        private System.Void CompileOrElseBinaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF6380  token: 0x60008A5
        private System.Void CompileLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression b, System.Boolean andAlso) { }
        // RVA: 0x0ACF7600  token: 0x60008A6
        private System.Void CompileMethodLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression expr, System.Boolean andAlso) { }
        // RVA: 0x0ACF585C  token: 0x60008A7
        private System.Void CompileLiftedLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression node, System.Boolean andAlso) { }
        // RVA: 0x0ACFB360  token: 0x60008A8
        private System.Void CompileUnliftedLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression expr, System.Boolean andAlso) { }
        // RVA: 0x0ACF3DD0  token: 0x60008A9
        private System.Void CompileConditionalExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid) { }
        // RVA: 0x0ACF64C0  token: 0x60008AA
        private System.Void CompileLoopExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF92F8  token: 0x60008AB
        private System.Void CompileSwitchExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: -1  // generic def  token: 0x60008AC
        private System.Void CompileIntSwitchExpression(System.Linq.Expressions.SwitchExpression node) { }
        // RVA: 0x0ACF8D58  token: 0x60008AD
        private System.Void CompileStringSwitchExpression(System.Linq.Expressions.SwitchExpression node) { }
        // RVA: 0x0ACF5468  token: 0x60008AE
        private System.Void CompileLabelExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF4D7C  token: 0x60008AF
        private System.Void CompileGotoExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFC1A4  token: 0x60008B0
        private System.Void PushLabelBlock(System.Linq.Expressions.Interpreter.LabelScopeKind type) { }
        // RVA: 0x0ACFC17C  token: 0x60008B1
        private System.Void PopLabelBlock(System.Linq.Expressions.Interpreter.LabelScopeKind kind) { }
        // RVA: 0x0ACFBCA4  token: 0x60008B2
        private System.Linq.Expressions.Interpreter.LabelInfo EnsureLabel(System.Linq.Expressions.LabelTarget node) { }
        // RVA: 0x0ACFC22C  token: 0x60008B3
        private System.Linq.Expressions.Interpreter.LabelInfo ReferenceLabel(System.Linq.Expressions.LabelTarget node) { }
        // RVA: 0x0ACFB688  token: 0x60008B4
        private System.Linq.Expressions.Interpreter.LabelInfo DefineLabel(System.Linq.Expressions.LabelTarget node) { }
        // RVA: 0x0ACFC400  token: 0x60008B5
        private System.Boolean TryPushLabelBlock(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACFB58C  token: 0x60008B6
        private System.Void DefineBlockLabels(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACF1284  token: 0x60008B7
        private System.Void CheckRethrow() { }
        // RVA: 0x0ACF9A64  token: 0x60008B8
        private System.Void CompileThrowUnaryExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid) { }
        // RVA: 0x0ACF9D10  token: 0x60008B9
        private System.Void CompileTryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFA918  token: 0x60008BA
        private System.Void CompileTryFaultExpression(System.Linq.Expressions.TryExpression expr) { }
        // RVA: 0x0ACF7588  token: 0x60008BB
        private System.Void CompileMethodCallExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF713C  token: 0x60008BC
        private System.Void CompileMethodCallExpression(System.Linq.Expressions.Expression object, System.Reflection.MethodInfo method, System.Linq.Expressions.IArgumentProvider arguments) { }
        // RVA: 0x0ACF1D90  token: 0x60008BD
        private System.Linq.Expressions.Interpreter.ByRefUpdater CompileArrayIndexAddress(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression index, System.Int32 argumentIndex) { }
        // RVA: 0x0ACFB874  token: 0x60008BE
        private System.Void EmitThisForMethodCall(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACFC2C4  token: 0x60008BF
        private static System.Boolean ShouldWritebackNode(System.Linq.Expressions.Expression node) { }
        // RVA: 0x0ACF12D0  token: 0x60008C0
        private System.Linq.Expressions.Interpreter.ByRefUpdater CompileAddress(System.Linq.Expressions.Expression node, System.Int32 index) { }
        // RVA: 0x0ACF7774  token: 0x60008C1
        private System.Linq.Expressions.Interpreter.ByRefUpdater CompileMultiDimArrayAccess(System.Linq.Expressions.Expression array, System.Linq.Expressions.IArgumentProvider arguments, System.Int32 index) { }
        // RVA: 0x0ACF7D30  token: 0x60008C2
        private System.Void CompileNewExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF6A78  token: 0x60008C3
        private System.Void CompileMemberExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF6EA0  token: 0x60008C4
        private System.Void CompileMember(System.Linq.Expressions.Expression from, System.Reflection.MemberInfo member, System.Boolean forBinding) { }
        // RVA: 0x0ACF7AD4  token: 0x60008C5
        private System.Void CompileNewArrayExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF4AA8  token: 0x60008C6
        private System.Void CompileDebugInfoExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF8AE4  token: 0x60008C7
        private System.Void CompileRuntimeVariablesExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF5608  token: 0x60008C8
        private System.Void CompileLambdaExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF36E8  token: 0x60008C9
        private System.Void CompileCoalesceBinaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF5274  token: 0x60008CA
        private System.Void CompileInvocationExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF6000  token: 0x60008CB
        private System.Void CompileListInitExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF6074  token: 0x60008CC
        private System.Void CompileListInit(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit> initializers) { }
        // RVA: 0x0ACF6AF0  token: 0x60008CD
        private System.Void CompileMemberInitExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF6B64  token: 0x60008CE
        private System.Void CompileMemberInit(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> bindings) { }
        // RVA: 0x0ACFBD74  token: 0x60008CF
        private static System.Type GetMemberType(System.Reflection.MemberInfo member) { }
        // RVA: 0x0ACF87E8  token: 0x60008D0
        private System.Void CompileQuoteUnaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFB25C  token: 0x60008D1
        private System.Void CompileUnboxUnaryExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFABCC  token: 0x60008D2
        private System.Void CompileTypeEqualExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFAB74  token: 0x60008D3
        private System.Void CompileTypeAsExpression(System.Linq.Expressions.UnaryExpression node) { }
        // RVA: 0x0ACFAD70  token: 0x60008D4
        private System.Void CompileTypeIsExpression(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFB568  token: 0x60008D5
        private System.Void Compile(System.Linq.Expressions.Expression expr, System.Boolean asVoid) { }
        // RVA: 0x0ACF1FD0  token: 0x60008D6
        private System.Void CompileAsVoid(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACF8004  token: 0x60008D7
        private System.Void CompileNoLabelPush(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFB508  token: 0x60008D8
        private System.Void Compile(System.Linq.Expressions.Expression expr) { }
        // RVA: 0x0ACFC7D4  token: 0x60008D9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200019B  // size: 0x18
    public abstract class ByRefUpdater
    {
        // Fields
        public readonly System.Int32 ArgumentIndex;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x60008E6
        public System.Void .ctor(System.Int32 argumentIndex) { }
        // RVA: -1  // abstract  token: 0x60008E7
        public virtual System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value) { }
        // RVA: 0x041E1670  token: 0x60008E8
        public virtual System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals) { }

    }

    // TypeToken: 0x200019C  // size: 0x20
    public sealed class ParameterByRefUpdater : System.Linq.Expressions.Interpreter.ByRefUpdater
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.LocalVariable _parameter;  // 0x18

        // Methods
        // RVA: 0x0AD10340  token: 0x60008E9
        public System.Void .ctor(System.Linq.Expressions.Interpreter.LocalVariable parameter, System.Int32 argumentIndex) { }
        // RVA: 0x0AD10228  token: 0x60008EA
        public virtual System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value) { }

    }

    // TypeToken: 0x200019D  // size: 0x38
    public sealed class ArrayByRefUpdater : System.Linq.Expressions.Interpreter.ByRefUpdater
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.LocalDefinition _array;  // 0x18
        private readonly System.Linq.Expressions.Interpreter.LocalDefinition _index;  // 0x28

        // Methods
        // RVA: 0x0ACFD3E0  token: 0x60008EB
        public System.Void .ctor(System.Linq.Expressions.Interpreter.LocalDefinition array, System.Linq.Expressions.Interpreter.LocalDefinition index, System.Int32 argumentIndex) { }
        // RVA: 0x0ACFD308  token: 0x60008EC
        public virtual System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value) { }
        // RVA: 0x0ACFD278  token: 0x60008ED
        public virtual System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals) { }

    }

    // TypeToken: 0x200019E  // size: 0x38
    public sealed class FieldByRefUpdater : System.Linq.Expressions.Interpreter.ByRefUpdater
    {
        // Fields
        private readonly System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> _object;  // 0x18
        private readonly System.Reflection.FieldInfo _field;  // 0x30

        // Methods
        // RVA: 0x0AD0560C  token: 0x60008EE
        public System.Void .ctor(System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> obj, System.Reflection.FieldInfo field, System.Int32 argumentIndex) { }
        // RVA: 0x0AD05570  token: 0x60008EF
        public virtual System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value) { }
        // RVA: 0x0AD054D8  token: 0x60008F0
        public virtual System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals) { }

    }

    // TypeToken: 0x200019F  // size: 0x38
    public sealed class PropertyByRefUpdater : System.Linq.Expressions.Interpreter.ByRefUpdater
    {
        // Fields
        private readonly System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> _object;  // 0x18
        private readonly System.Reflection.PropertyInfo _property;  // 0x30

        // Methods
        // RVA: 0x0AD0560C  token: 0x60008F1
        public System.Void .ctor(System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> obj, System.Reflection.PropertyInfo property, System.Int32 argumentIndex) { }
        // RVA: 0x0AD10524  token: 0x60008F2
        public virtual System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value) { }
        // RVA: 0x0AD1048C  token: 0x60008F3
        public virtual System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals) { }

    }

    // TypeToken: 0x20001A0  // size: 0x40
    public sealed class IndexMethodByRefUpdater : System.Linq.Expressions.Interpreter.ByRefUpdater
    {
        // Fields
        private readonly System.Reflection.MethodInfo _indexer;  // 0x18
        private readonly System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> _obj;  // 0x20
        private readonly System.Linq.Expressions.Interpreter.LocalDefinition[] _args;  // 0x38

        // Methods
        // RVA: 0x0AD05DAC  token: 0x60008F4
        public System.Void .ctor(System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> obj, System.Linq.Expressions.Interpreter.LocalDefinition[] args, System.Reflection.MethodInfo indexer, System.Int32 argumentIndex) { }
        // RVA: 0x0AD05BBC  token: 0x60008F5
        public virtual System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value) { }
        // RVA: 0x0AD05AC4  token: 0x60008F6
        public virtual System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals) { }

    }

    // TypeToken: 0x20001A1  // size: 0x20
    public sealed class LightDelegateCreator
    {
        // Fields
        private readonly System.Linq.Expressions.LambdaExpression _lambda;  // 0x10
        private readonly System.Linq.Expressions.Interpreter.Interpreter <Interpreter>k__BackingField;  // 0x18

        // Properties
        System.Linq.Expressions.Interpreter.Interpreter Interpreter { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03739160  token: 0x60008F7
        private System.Void .ctor(System.Linq.Expressions.Interpreter.Interpreter interpreter, System.Linq.Expressions.LambdaExpression lambda) { }
        // RVA: 0x0AD06880  token: 0x60008F9
        public System.Delegate CreateDelegate() { }
        // RVA: 0x0AD0688C  token: 0x60008FA
        private System.Delegate CreateDelegate(System.Runtime.CompilerServices.IStrongBox[] closure) { }

    }

    // TypeToken: 0x20001A2  // size: 0x28
    public class LightLambda
    {
        // Fields
        private readonly System.Runtime.CompilerServices.IStrongBox[] _closure;  // 0x10
        private readonly System.Linq.Expressions.Interpreter.Interpreter _interpreter;  // 0x18
        private static readonly System.Dynamic.Utils.CacheDict<System.Type,System.Func<System.Linq.Expressions.Interpreter.LightLambda,System.Delegate>> _runCache;  // static @ 0x0
        private readonly System.Linq.Expressions.Interpreter.LightDelegateCreator _delegateCreator;  // 0x20

        // Properties
        System.String DebugView { get; /* RVA: 0x0AD07F20 */ }

        // Methods
        // RVA: -1  // generic def  token: 0x60008FB
        private TRet Run0() { }
        // RVA: 0x0AD079C4  token: 0x60008FC
        private System.Void RunVoid0() { }
        // RVA: -1  // generic def  token: 0x60008FD
        private static System.Delegate MakeRun0(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: 0x0AD07950  token: 0x60008FE
        private static System.Delegate MakeRunVoid0(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x60008FF
        private TRet Run1(T0 arg0) { }
        // RVA: -1  // generic def  token: 0x6000900
        private System.Void RunVoid1(T0 arg0) { }
        // RVA: -1  // generic def  token: 0x6000901
        private static System.Delegate MakeRun1(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000902
        private static System.Delegate MakeRunVoid1(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000903
        private TRet Run2(T0 arg0, T1 arg1) { }
        // RVA: -1  // generic def  token: 0x6000904
        private System.Void RunVoid2(T0 arg0, T1 arg1) { }
        // RVA: -1  // generic def  token: 0x6000905
        private static System.Delegate MakeRun2(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000906
        private static System.Delegate MakeRunVoid2(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000907
        private TRet Run3(T0 arg0, T1 arg1, T2 arg2) { }
        // RVA: -1  // generic def  token: 0x6000908
        private System.Void RunVoid3(T0 arg0, T1 arg1, T2 arg2) { }
        // RVA: -1  // generic def  token: 0x6000909
        private static System.Delegate MakeRun3(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600090A
        private static System.Delegate MakeRunVoid3(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600090B
        private TRet Run4(T0 arg0, T1 arg1, T2 arg2, T3 arg3) { }
        // RVA: -1  // generic def  token: 0x600090C
        private System.Void RunVoid4(T0 arg0, T1 arg1, T2 arg2, T3 arg3) { }
        // RVA: -1  // generic def  token: 0x600090D
        private static System.Delegate MakeRun4(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600090E
        private static System.Delegate MakeRunVoid4(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600090F
        private TRet Run5(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) { }
        // RVA: -1  // generic def  token: 0x6000910
        private System.Void RunVoid5(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) { }
        // RVA: -1  // generic def  token: 0x6000911
        private static System.Delegate MakeRun5(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000912
        private static System.Delegate MakeRunVoid5(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000913
        private TRet Run6(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) { }
        // RVA: -1  // generic def  token: 0x6000914
        private System.Void RunVoid6(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) { }
        // RVA: -1  // generic def  token: 0x6000915
        private static System.Delegate MakeRun6(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000916
        private static System.Delegate MakeRunVoid6(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000917
        private TRet Run7(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) { }
        // RVA: -1  // generic def  token: 0x6000918
        private System.Void RunVoid7(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) { }
        // RVA: -1  // generic def  token: 0x6000919
        private static System.Delegate MakeRun7(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600091A
        private static System.Delegate MakeRunVoid7(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600091B
        private TRet Run8(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) { }
        // RVA: -1  // generic def  token: 0x600091C
        private System.Void RunVoid8(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) { }
        // RVA: -1  // generic def  token: 0x600091D
        private static System.Delegate MakeRun8(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600091E
        private static System.Delegate MakeRunVoid8(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600091F
        private TRet Run9(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) { }
        // RVA: -1  // generic def  token: 0x6000920
        private System.Void RunVoid9(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) { }
        // RVA: -1  // generic def  token: 0x6000921
        private static System.Delegate MakeRun9(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000922
        private static System.Delegate MakeRunVoid9(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000923
        private TRet Run10(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) { }
        // RVA: -1  // generic def  token: 0x6000924
        private System.Void RunVoid10(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) { }
        // RVA: -1  // generic def  token: 0x6000925
        private static System.Delegate MakeRun10(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000926
        private static System.Delegate MakeRunVoid10(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000927
        private TRet Run11(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10) { }
        // RVA: -1  // generic def  token: 0x6000928
        private System.Void RunVoid11(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10) { }
        // RVA: -1  // generic def  token: 0x6000929
        private static System.Delegate MakeRun11(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600092A
        private static System.Delegate MakeRunVoid11(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600092B
        private TRet Run12(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) { }
        // RVA: -1  // generic def  token: 0x600092C
        private System.Void RunVoid12(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) { }
        // RVA: -1  // generic def  token: 0x600092D
        private static System.Delegate MakeRun12(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600092E
        private static System.Delegate MakeRunVoid12(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600092F
        private TRet Run13(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12) { }
        // RVA: -1  // generic def  token: 0x6000930
        private System.Void RunVoid13(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12) { }
        // RVA: -1  // generic def  token: 0x6000931
        private static System.Delegate MakeRun13(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000932
        private static System.Delegate MakeRunVoid13(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000933
        private TRet Run14(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13) { }
        // RVA: -1  // generic def  token: 0x6000934
        private System.Void RunVoid14(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13) { }
        // RVA: -1  // generic def  token: 0x6000935
        private static System.Delegate MakeRun14(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000936
        private static System.Delegate MakeRunVoid14(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x6000937
        private TRet Run15(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14) { }
        // RVA: -1  // generic def  token: 0x6000938
        private System.Void RunVoid15(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14) { }
        // RVA: -1  // generic def  token: 0x6000939
        private static System.Delegate MakeRun15(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: -1  // generic def  token: 0x600093A
        private static System.Delegate MakeRunVoid15(System.Linq.Expressions.Interpreter.LightLambda lambda) { }
        // RVA: 0x0AD07ED8  token: 0x600093B
        private System.Void .ctor(System.Linq.Expressions.Interpreter.LightDelegateCreator delegateCreator, System.Runtime.CompilerServices.IStrongBox[] closure) { }
        // RVA: 0x0AD0703C  token: 0x600093D
        private static System.Func<System.Linq.Expressions.Interpreter.LightLambda,System.Delegate> GetRunDelegateCtor(System.Type delegateType) { }
        // RVA: 0x0AD072A0  token: 0x600093E
        private static System.Func<System.Linq.Expressions.Interpreter.LightLambda,System.Delegate> MakeRunDelegateCtor(System.Type delegateType) { }
        // RVA: 0x0AD06918  token: 0x600093F
        private System.Delegate CreateCustomDelegate(System.Type delegateType) { }
        // RVA: 0x0AD071BC  token: 0x6000940
        private System.Delegate MakeDelegate(System.Type delegateType) { }
        // RVA: 0x0AD0722C  token: 0x6000941
        private System.Linq.Expressions.Interpreter.InterpretedFrame MakeFrame() { }
        // RVA: -1  // generic def  token: 0x6000942
        private System.Void RunVoidRef2(T0& arg0, T1& arg1) { }
        // RVA: 0x0AD07C64  token: 0x6000943
        public System.Object Run(System.Object[] arguments) { }
        // RVA: 0x0AD07AA0  token: 0x6000944
        public System.Object RunVoid(System.Object[] arguments) { }
        // RVA: 0x0AD07E44  token: 0x6000945
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20001A5
    public interface IBoxableInstruction
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000950
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001A6  // size: 0x18
    public abstract class LocalAccessInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Int32 _index;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x6000951
        protected System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD085B8  token: 0x6000952
        public virtual System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects) { }

    }

    // TypeToken: 0x20001A7  // size: 0x18
    public sealed class LoadLocalInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction, System.Linq.Expressions.Interpreter.IBoxableInstruction
    {
        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD08474 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000953
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD083F8  token: 0x6000956
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD083A4  token: 0x6000957
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001A8  // size: 0x18
    public sealed class LoadLocalBoxedInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction
    {
        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0821C */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000958
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD08164  token: 0x600095B
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001A9  // size: 0x18
    public sealed class LoadLocalFromClosureInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction
    {
        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD08378 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600095C
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD082F0  token: 0x600095F
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001AA  // size: 0x18
    public sealed class LoadLocalFromClosureBoxedInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction
    {
        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD082C4 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000960
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD08248  token: 0x6000963
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001AB  // size: 0x18
    public sealed class AssignLocalInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction, System.Linq.Expressions.Interpreter.IBoxableInstruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACFD59C */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000964
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0ACFD534  token: 0x6000968
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0ACFD4E0  token: 0x6000969
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001AC  // size: 0x18
    public sealed class StoreLocalInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction, System.Linq.Expressions.Interpreter.IBoxableInstruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD120C4 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600096A
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD1205C  token: 0x600096D
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD12008  token: 0x600096E
        public virtual System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index) { }

    }

    // TypeToken: 0x20001AD  // size: 0x18
    public sealed class AssignLocalBoxedInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACFD4B4 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600096F
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0ACFD414  token: 0x6000973
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001AE  // size: 0x18
    public sealed class StoreLocalBoxedInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD11FDC */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000974
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0AD11F2C  token: 0x6000977
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001AF  // size: 0x18
    public sealed class AssignLocalToClosureInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction
    {
        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACFD620 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000978
        private System.Void .ctor(System.Int32 index) { }
        // RVA: 0x0ACFD5C8  token: 0x600097C
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001B0  // size: 0x10
    public sealed class ValueTypeCopyInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        public static readonly System.Linq.Expressions.Interpreter.ValueTypeCopyInstruction Instruction;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD1559C */ }

        // Methods
        // RVA: 0x0AD154E4  token: 0x6000980
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x041E1670  token: 0x6000981
        public System.Void .ctor() { }
        // RVA: 0x0AD15538  token: 0x6000982
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20001B1  // size: 0x18
    public abstract class InitializeLocalInstruction : System.Linq.Expressions.Interpreter.LocalAccessInstruction
    {
        // Methods
        // RVA: 0x014F51F0  token: 0x6000983
        private System.Void .ctor(System.Int32 index) { }

    }

    // TypeToken: 0x20001BA  // size: 0x18
    public sealed class RuntimeVariablesInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Int32 _count;  // 0x10

        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x02B2E2D0 */ }
        System.String InstructionName { get; /* RVA: 0x0AD11A90 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60009A0
        public System.Void .ctor(System.Int32 count) { }
        // RVA: 0x0AD1199C  token: 0x60009A4
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001BB  // size: 0x18
    public sealed class LocalVariable
    {
        // Fields
        public readonly System.Int32 Index;  // 0x10
        private System.Int32 _flags;  // 0x14

        // Properties
        System.Boolean IsBoxed { get; /* RVA: 0x0AD088A0 */ set; /* RVA: 0x0AD088A8 */ }
        System.Boolean InClosure { get; /* RVA: 0x0AD08898 */ }

        // Methods
        // RVA: 0x0AD08888  token: 0x60009A8
        private System.Void .ctor(System.Int32 index, System.Boolean closure) { }
        // RVA: 0x0AD087B4  token: 0x60009A9
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20001BC  // size: 0x20
    public sealed struct LocalDefinition
    {
        // Fields
        private readonly System.Int32 <Index>k__BackingField;  // 0x10
        private readonly System.Linq.Expressions.ParameterExpression <Parameter>k__BackingField;  // 0x18

        // Properties
        System.Int32 Index { get; /* RVA: 0x02B77620 */ }
        System.Linq.Expressions.ParameterExpression Parameter { get; /* RVA: 0x04D88320 */ }

        // Methods
        // RVA: 0x06B17430  token: 0x60009AA
        private System.Void .ctor(System.Int32 localIndex, System.Linq.Expressions.ParameterExpression parameter) { }
        // RVA: 0x0AD08718  token: 0x60009AD
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AD08788  token: 0x60009AE
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x20001BD  // size: 0x28
    public sealed class LocalVariables
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.HybridReferenceDictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariables.VariableScope> _variables;  // 0x10
        private System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> _closureVariables;  // 0x18
        private System.Int32 _localCount;  // 0x20
        private System.Int32 _maxLocalCount;  // 0x24

        // Properties
        System.Int32 LocalCount { get; /* RVA: 0x011F0020 */ }
        System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> ClosureVariables { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0AD08B24  token: 0x60009AF
        public System.Linq.Expressions.Interpreter.LocalDefinition DefineLocal(System.Linq.Expressions.ParameterExpression variable, System.Int32 start) { }
        // RVA: 0x0AD08E14  token: 0x60009B0
        public System.Void UndefineLocal(System.Linq.Expressions.Interpreter.LocalDefinition definition, System.Int32 end) { }
        // RVA: 0x0AD089D8  token: 0x60009B1
        private System.Void Box(System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Interpreter.InstructionList instructions) { }
        // RVA: 0x0AD08D48  token: 0x60009B3
        public System.Boolean TryGetLocalOrClosure(System.Linq.Expressions.ParameterExpression var, System.Linq.Expressions.Interpreter.LocalVariable& local) { }
        // RVA: 0x0AD088C4  token: 0x60009B5
        private System.Linq.Expressions.Interpreter.LocalVariable AddClosureVariable(System.Linq.Expressions.ParameterExpression variable) { }
        // RVA: 0x0AD08ED8  token: 0x60009B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20001BF  // size: 0x10
    public abstract class ModuloInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD09608 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60009BB
        private System.Void .ctor() { }
        // RVA: 0x0AD091F8  token: 0x60009BC
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20001C8  // size: 0x10
    public abstract class MulInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0A304 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60009D0
        private System.Void .ctor() { }
        // RVA: 0x0AD09EF4  token: 0x60009D1
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20001D1  // size: 0x10
    public abstract class MulOvfInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0A978 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60009E5
        private System.Void .ctor() { }
        // RVA: 0x0AD0A65C  token: 0x60009E6
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20001D8  // size: 0x10
    public abstract class NegateInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x20

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0C2F0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60009F6
        private System.Void .ctor() { }
        // RVA: 0x0AD0C01C  token: 0x60009F7
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20001DE  // size: 0x10
    public abstract class NegateCheckedInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0BC74 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000A05
        private System.Void .ctor() { }
        // RVA: 0x0AD0BA98  token: 0x6000A06
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x20001E2  // size: 0x20
    public class NewInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        protected readonly System.Reflection.ConstructorInfo _constructor;  // 0x10
        protected readonly System.Int32 _argumentCount;  // 0x18

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0C85C */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000A0D
        public System.Void .ctor(System.Reflection.ConstructorInfo constructor, System.Int32 argumentCount) { }
        // RVA: 0x0AD0C624  token: 0x6000A11
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD0C538  token: 0x6000A12
        protected System.Object[] GetArgs(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Int32 first) { }
        // RVA: 0x0AD0C730  token: 0x6000A13
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20001E3  // size: 0x28
    public class ByRefNewInstruction : System.Linq.Expressions.Interpreter.NewInstruction
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.ByRefUpdater[] _byrefArgs;  // 0x20

        // Properties
        System.String InstructionName { get; /* RVA: 0x0ACFDEC0 */ }

        // Methods
        // RVA: 0x032B2C30  token: 0x6000A14
        private System.Void .ctor(System.Reflection.ConstructorInfo target, System.Int32 argumentCount, System.Linq.Expressions.Interpreter.ByRefUpdater[] byrefArgs) { }
        // RVA: 0x0ACFDC6C  token: 0x6000A16
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x20001E4  // size: 0x10
    public abstract class NotEqualInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_reference;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Boolean;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Char;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.Instruction s_SByteLiftedToNull;  // static @ 0x68
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16LiftedToNull;  // static @ 0x70
        private static System.Linq.Expressions.Interpreter.Instruction s_CharLiftedToNull;  // static @ 0x78
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32LiftedToNull;  // static @ 0x80
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64LiftedToNull;  // static @ 0x88
        private static System.Linq.Expressions.Interpreter.Instruction s_ByteLiftedToNull;  // static @ 0x90
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16LiftedToNull;  // static @ 0x98
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32LiftedToNull;  // static @ 0xa0
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64LiftedToNull;  // static @ 0xa8
        private static System.Linq.Expressions.Interpreter.Instruction s_SingleLiftedToNull;  // static @ 0xb0
        private static System.Linq.Expressions.Interpreter.Instruction s_DoubleLiftedToNull;  // static @ 0xb8

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0D9E8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000A1A
        private System.Void .ctor() { }
        // RVA: 0x0AD0CE78  token: 0x6000A1B
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull) { }

    }

    // TypeToken: 0x20001FD  // size: 0x10
    public abstract class NotInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        public static System.Linq.Expressions.Interpreter.Instruction s_Boolean;  // static @ 0x0
        public static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x8
        public static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x10
        public static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x18
        public static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x20
        public static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x28
        public static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x30
        public static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x38
        public static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x40

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0EA30 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000A4C
        private System.Void .ctor() { }
        // RVA: 0x0AD0E5A0  token: 0x6000A50
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x2000207  // size: 0x10
    public sealed class NullCheckInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        public static readonly System.Linq.Expressions.Interpreter.Instruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0EEC4 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000A63
        private System.Void .ctor() { }
        // RVA: 0x0AD0EDF4  token: 0x6000A67
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD0EE60  token: 0x6000A68
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000208  // size: 0x20
    public abstract class NumericConvertInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.TypeCode _from;  // 0x10
        private readonly System.TypeCode _to;  // 0x14
        private readonly System.Boolean _isLiftedToNull;  // 0x18

        // Properties
        System.String InstructionName { get; /* RVA: 0x0AD0F5A8 */ }
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x04DA8680  token: 0x6000A69
        protected System.Void .ctor(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull) { }
        // RVA: 0x0AD0F388  token: 0x6000A6A
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: -1  // abstract  token: 0x6000A6B
        protected virtual System.Object Convert(System.Object obj) { }
        // RVA: 0x0AD0F45C  token: 0x6000A6F
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200020C  // size: 0x10
    public abstract class OrInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.Instruction s_Boolean;  // static @ 0x40

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0FC0C */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000A84
        private System.Void .ctor() { }
        // RVA: 0x0AD0F77C  token: 0x6000A85
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x2000216  // size: 0x10
    public abstract class RightShiftInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_SByte;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_Byte;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD112C0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000A9B
        private System.Void .ctor() { }
        // RVA: 0x0AD10EB0  token: 0x6000A9C
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x200021F  // size: 0x18
    public sealed class RuntimeVariables : System.Runtime.CompilerServices.IRuntimeVariables
    {
        // Fields
        private readonly System.Runtime.CompilerServices.IStrongBox[] _boxes;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000AAD
        private System.Void .ctor(System.Runtime.CompilerServices.IStrongBox[] boxes) { }
        // RVA: 0x0AD11ABC  token: 0x6000AAE
        private static System.Runtime.CompilerServices.IRuntimeVariables Create(System.Runtime.CompilerServices.IStrongBox[] boxes) { }

    }

    // TypeToken: 0x2000220  // size: 0x18
    public sealed class LoadObjectInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Object _value;  // 0x10

        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0858C */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000AAF
        private System.Void .ctor(System.Object value) { }
        // RVA: 0x0AD084A0  token: 0x6000AB2
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD08500  token: 0x6000AB3
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000221  // size: 0x18
    public sealed class LoadCachedObjectInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.UInt32 _index;  // 0x10

        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD08138 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000AB4
        private System.Void .ctor(System.UInt32 index) { }
        // RVA: 0x0AD07F8C  token: 0x6000AB7
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD0800C  token: 0x6000AB8
        public virtual System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects) { }
        // RVA: 0x0AD080DC  token: 0x6000AB9
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000222  // size: 0x10
    public sealed class PopInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.PopInstruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD10460 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000ABA
        private System.Void .ctor() { }
        // RVA: 0x0AD103D4  token: 0x6000ABD
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD103FC  token: 0x6000ABE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000223  // size: 0x10
    public sealed class DupInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static readonly System.Linq.Expressions.Interpreter.DupInstruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD01764 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000ABF
        private System.Void .ctor() { }
        // RVA: 0x0AD016D8  token: 0x6000AC2
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD01700  token: 0x6000AC3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000224  // size: 0x10
    public abstract class SubInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.Instruction s_Single;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.Instruction s_Double;  // static @ 0x38

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD12624 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000AC7
        private System.Void .ctor() { }
        // RVA: 0x0AD12214  token: 0x6000AC8
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x200022D  // size: 0x10
    public abstract class SubOvfInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.Instruction s_Int16;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.Instruction s_Int32;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.Instruction s_Int64;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt16;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt32;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.Instruction s_UInt64;  // static @ 0x28

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD12C98 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000ADC
        private System.Void .ctor() { }
        // RVA: 0x0AD1297C  token: 0x6000ADD
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type type) { }

    }

    // TypeToken: 0x2000234  // size: 0x18
    public sealed class CreateDelegateInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Linq.Expressions.Interpreter.LightDelegateCreator _creator;  // 0x10

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x0AD000DC */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD00104 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000AEA
        private System.Void .ctor(System.Linq.Expressions.Interpreter.LightDelegateCreator delegateCreator) { }
        // RVA: 0x0ACFFFB0  token: 0x6000AEE
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000235  // size: 0x18
    public sealed class TypeIsInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Type _type;  // 0x10

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD14400 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000AEF
        private System.Void .ctor(System.Type type) { }
        // RVA: 0x0AD14350  token: 0x6000AF3
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD143A8  token: 0x6000AF4
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000236  // size: 0x18
    public sealed class TypeAsInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Type _type;  // 0x10

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD14224 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000AF5
        private System.Void .ctor(System.Type type) { }
        // RVA: 0x0AD14160  token: 0x6000AF9
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD141CC  token: 0x6000AFA
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000237  // size: 0x10
    public sealed class TypeEqualsInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        public static readonly System.Linq.Expressions.Interpreter.TypeEqualsInstruction Instance;  // static @ 0x0

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x02BBE540 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD14324 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000AFE
        private System.Void .ctor() { }
        // RVA: 0x0AD14250  token: 0x6000AFF
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }
        // RVA: 0x0AD142C0  token: 0x6000B00
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000238  // size: 0x10
    public abstract class NullableMethodCallInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.NullableMethodCallInstruction s_hasValue;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.NullableMethodCallInstruction s_value;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.NullableMethodCallInstruction s_equals;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.NullableMethodCallInstruction s_getHashCode;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.NullableMethodCallInstruction s_getValueOrDefault1;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.NullableMethodCallInstruction s_toString;  // static @ 0x28

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD0F35C */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000B04
        private System.Void .ctor() { }
        // RVA: 0x0AD0EF80  token: 0x6000B05
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.String method, System.Int32 argCount, System.Reflection.MethodInfo mi) { }
        // RVA: 0x0AD0EEF0  token: 0x6000B06
        public static System.Linq.Expressions.Interpreter.Instruction CreateGetValue() { }

    }

    // TypeToken: 0x2000240  // size: 0x10
    public abstract class CastInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Boolean;  // static @ 0x0
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Byte;  // static @ 0x8
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Char;  // static @ 0x10
        private static System.Linq.Expressions.Interpreter.CastInstruction s_DateTime;  // static @ 0x18
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Decimal;  // static @ 0x20
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Double;  // static @ 0x28
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Int16;  // static @ 0x30
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Int32;  // static @ 0x38
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Int64;  // static @ 0x40
        private static System.Linq.Expressions.Interpreter.CastInstruction s_SByte;  // static @ 0x48
        private static System.Linq.Expressions.Interpreter.CastInstruction s_Single;  // static @ 0x50
        private static System.Linq.Expressions.Interpreter.CastInstruction s_String;  // static @ 0x58
        private static System.Linq.Expressions.Interpreter.CastInstruction s_UInt16;  // static @ 0x60
        private static System.Linq.Expressions.Interpreter.CastInstruction s_UInt32;  // static @ 0x68
        private static System.Linq.Expressions.Interpreter.CastInstruction s_UInt64;  // static @ 0x70

        // Properties
        System.Int32 ConsumedStack { get; /* RVA: 0x04D85B80 */ }
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0ACFE9F4 */ }

        // Methods
        // RVA: 0x0ACFE24C  token: 0x6000B1A
        public static System.Linq.Expressions.Interpreter.Instruction Create(System.Type t) { }
        // RVA: 0x041E1670  token: 0x6000B1B
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000245  // size: 0x18
    public sealed class CastToEnumInstruction : System.Linq.Expressions.Interpreter.CastInstruction
    {
        // Fields
        private readonly System.Type _t;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000B26
        public System.Void .ctor(System.Type t) { }
        // RVA: 0x0ACFED48  token: 0x6000B27
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000246  // size: 0x18
    public sealed class CastReferenceToEnumInstruction : System.Linq.Expressions.Interpreter.CastInstruction
    {
        // Fields
        private readonly System.Type _t;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000B28
        public System.Void .ctor(System.Type t) { }
        // RVA: 0x0ACFEA20  token: 0x6000B29
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000247  // size: 0x20
    public sealed class QuoteInstruction : System.Linq.Expressions.Interpreter.Instruction
    {
        // Fields
        private readonly System.Linq.Expressions.Expression _operand;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> _hoistedVariables;  // 0x18

        // Properties
        System.Int32 ProducedStack { get; /* RVA: 0x04D85B80 */ }
        System.String InstructionName { get; /* RVA: 0x0AD106EC */ }

        // Methods
        // RVA: 0x032AF420  token: 0x6000B2A
        public System.Void .ctor(System.Linq.Expressions.Expression operand, System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> hoistedVariables) { }
        // RVA: 0x0AD1063C  token: 0x6000B2D
        public virtual System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame) { }

    }

    // TypeToken: 0x2000249  // size: 0x10
    public static class DelegateHelpers
    {
        // Methods
        // RVA: 0x0AD00EA0  token: 0x6000B34
        private static System.Type MakeDelegate(System.Type[] types) { }

    }

    // TypeToken: 0x200024B  // size: 0x10
    public static class ScriptingRuntimeHelpers
    {
        // Methods
        // RVA: 0x0AD11E18  token: 0x6000B38
        public static System.Object Int32ToObject(System.Int32 i) { }
        // RVA: 0x0AD11B1C  token: 0x6000B39
        private static System.Object GetPrimitiveDefaultValue(System.Type type) { }

    }

    // TypeToken: 0x200024C  // size: 0x10
    public static class ExceptionHelpers
    {
        // Methods
        // RVA: 0x0AD02DD8  token: 0x6000B3A
        public static System.Void UnwrapAndRethrow(System.Reflection.TargetInvocationException exception) { }

    }

    // TypeToken: 0x200024D
    public class HybridReferenceDictionary`2
    {
        // Fields
        private System.Collections.Generic.KeyValuePair<TKey,TValue>[] _keysAndValues;  // 0x0
        private System.Collections.Generic.Dictionary<TKey,TValue> _dict;  // 0x0

        // Properties
        TValue Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B3B
        public System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000B3C
        public System.Void Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000B3D
        public System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000B3E
        public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000B3F
        private System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumeratorWorker() { }
        // RVA: -1  // not resolved  token: 0x6000B42
        public System.Void .ctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x200024F  // size: 0x10
    public static class RuntimeOps
    {
        // Methods
        // RVA: 0x0AD11928  token: 0x6000B49
        public static System.Boolean ExpandoTryGetValue(System.Dynamic.ExpandoObject expando, System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase, System.Object& value) { }
        // RVA: 0x0AD1195C  token: 0x6000B4A
        public static System.Object ExpandoTrySetValue(System.Dynamic.ExpandoObject expando, System.Object indexClass, System.Int32 index, System.Object value, System.String name, System.Boolean ignoreCase) { }
        // RVA: 0x0AD11888  token: 0x6000B4B
        public static System.Boolean ExpandoTryDeleteValue(System.Dynamic.ExpandoObject expando, System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase) { }
        // RVA: 0x0AD11844  token: 0x6000B4C
        public static System.Boolean ExpandoCheckVersion(System.Dynamic.ExpandoObject expando, System.Object version) { }
        // RVA: 0x0AD1186C  token: 0x6000B4D
        public static System.Void ExpandoPromoteClass(System.Dynamic.ExpandoObject expando, System.Object oldClass, System.Object newClass) { }

    }

    // TypeToken: 0x2000250  // size: 0x20
    public class CallSite
    {
        // Fields
        private readonly System.Runtime.CompilerServices.CallSiteBinder _binder;  // 0x10
        private System.Boolean _match;  // 0x18

        // Properties
        System.Runtime.CompilerServices.CallSiteBinder Binder { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000B4E
        private System.Void .ctor(System.Runtime.CompilerServices.CallSiteBinder binder) { }

    }

    // TypeToken: 0x2000251
    public class CallSite`1 : System.Runtime.CompilerServices.CallSite
    {
        // Fields
        public T Target;  // 0x0
        private T[] Rules;  // 0x0
        private static T s_cachedUpdate;  // static @ 0x0
        private static T s_cachedNoMatch;  // static @ 0x0

        // Properties
        T Update { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B51
        private System.Void .ctor(System.Runtime.CompilerServices.CallSiteBinder binder) { }
        // RVA: -1  // not resolved  token: 0x6000B52
        private System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000B53
        private System.Runtime.CompilerServices.CallSite<T> CreateMatchMaker() { }
        // RVA: -1  // not resolved  token: 0x6000B54
        public static System.Runtime.CompilerServices.CallSite<T> Create(System.Runtime.CompilerServices.CallSiteBinder binder) { }
        // RVA: -1  // not resolved  token: 0x6000B55
        private T GetUpdateDelegate() { }
        // RVA: -1  // not resolved  token: 0x6000B56
        private T GetUpdateDelegate(T& addr) { }
        // RVA: -1  // not resolved  token: 0x6000B57
        private System.Void AddRule(T newRule) { }
        // RVA: -1  // not resolved  token: 0x6000B58
        private System.Void MoveRule(System.Int32 i) { }
        // RVA: -1  // not resolved  token: 0x6000B59
        private T MakeUpdateDelegate() { }
        // RVA: -1  // not resolved  token: 0x6000B5A
        private T CreateCustomUpdateDelegate(System.Reflection.MethodInfo invoke) { }
        // RVA: -1  // not resolved  token: 0x6000B5B
        private T CreateCustomNoMatchDelegate(System.Reflection.MethodInfo invoke) { }
        // RVA: -1  // not resolved  token: 0x6000B5C
        private static System.Linq.Expressions.Expression Convert(System.Linq.Expressions.Expression arg, System.Type type) { }

    }

    // TypeToken: 0x2000253  // size: 0x18
    public abstract class CallSiteBinder
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Type,System.Object> Cache;  // 0x10
        private static readonly System.Linq.Expressions.LabelTarget <UpdateLabel>k__BackingField;  // static @ 0x0

        // Properties
        System.Linq.Expressions.LabelTarget UpdateLabel { get; /* RVA: 0x0ACFDF68 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000B62
        protected System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x6000B64
        public virtual System.Linq.Expressions.Expression Bind(System.Object[] args, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters, System.Linq.Expressions.LabelTarget returnLabel) { }
        // RVA: -1  // generic def  token: 0x6000B65
        public virtual T BindDelegate(System.Runtime.CompilerServices.CallSite<T> site, System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x6000B66
        private T BindCore(System.Runtime.CompilerServices.CallSite<T> site, System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x6000B67
        protected System.Void CacheTarget(T target) { }
        // RVA: -1  // generic def  token: 0x6000B68
        private static System.Linq.Expressions.Expression<T> Stitch(System.Linq.Expressions.Expression binding, System.Runtime.CompilerServices.CallSiteBinder.LambdaSignature<T> signature) { }
        // RVA: -1  // generic def  token: 0x6000B69
        private System.Runtime.CompilerServices.RuleCache<T> GetRuleCache() { }
        // RVA: 0x0ACFDEEC  token: 0x6000B6A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000255  // size: 0x10
    public static class CallSiteOps
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000B6D
        public static System.Runtime.CompilerServices.CallSite<T> CreateMatchmaker(System.Runtime.CompilerServices.CallSite<T> site) { }
        // RVA: 0x0ACFDFD0  token: 0x6000B6E
        public static System.Boolean SetNotMatched(System.Runtime.CompilerServices.CallSite site) { }
        // RVA: 0x0952EB04  token: 0x6000B6F
        public static System.Boolean GetMatch(System.Runtime.CompilerServices.CallSite site) { }
        // RVA: 0x0ACFDFB8  token: 0x6000B70
        public static System.Void ClearMatch(System.Runtime.CompilerServices.CallSite site) { }
        // RVA: -1  // generic def  token: 0x6000B71
        public static System.Void AddRule(System.Runtime.CompilerServices.CallSite<T> site, T rule) { }
        // RVA: -1  // generic def  token: 0x6000B72
        public static System.Void UpdateRules(System.Runtime.CompilerServices.CallSite<T> this, System.Int32 matched) { }
        // RVA: -1  // generic def  token: 0x6000B73
        public static T[] GetRules(System.Runtime.CompilerServices.CallSite<T> site) { }
        // RVA: -1  // generic def  token: 0x6000B74
        public static System.Runtime.CompilerServices.RuleCache<T> GetRuleCache(System.Runtime.CompilerServices.CallSite<T> site) { }
        // RVA: -1  // generic def  token: 0x6000B75
        public static System.Void MoveRule(System.Runtime.CompilerServices.RuleCache<T> cache, T rule, System.Int32 i) { }
        // RVA: -1  // generic def  token: 0x6000B76
        public static T[] GetCachedRules(System.Runtime.CompilerServices.RuleCache<T> cache) { }
        // RVA: -1  // generic def  token: 0x6000B77
        public static T Bind(System.Runtime.CompilerServices.CallSiteBinder binder, System.Runtime.CompilerServices.CallSite<T> site, System.Object[] args) { }

    }

    // TypeToken: 0x2000256
    public interface IRuntimeVariables
    {
    }

    // TypeToken: 0x2000257
    public sealed class ReadOnlyCollectionBuilder`1 : System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.IList, System.Collections.ICollection
    {
        // Fields
        private T[] _items;  // 0x0
        private System.Int32 _size;  // 0x0
        private System.Int32 _version;  // 0x0

        // Properties
        System.Int32 Capacity { set; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.Generic.ICollection<T>.IsReadOnly { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.IList.IsReadOnly { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.IList.IsFixedSize { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IList.Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.ICollection.IsSynchronized { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.ICollection.SyncRoot { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000B78
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000B79
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000B7C
        public virtual System.Int32 IndexOf(T item) { }
        // RVA: -1  // not resolved  token: 0x6000B7D
        public virtual System.Void Insert(System.Int32 index, T item) { }
        // RVA: -1  // not resolved  token: 0x6000B7E
        public virtual System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000B81
        public virtual System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000B82
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000B83
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000B84
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000B86
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x6000B87
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000B88
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000B8A
        private virtual System.Int32 System.Collections.IList.Add(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000B8B
        private virtual System.Boolean System.Collections.IList.Contains(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000B8C
        private virtual System.Int32 System.Collections.IList.IndexOf(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000B8D
        private virtual System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000B8F
        private virtual System.Void System.Collections.IList.Remove(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000B92
        private virtual System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000B95
        public T[] ToArray() { }
        // RVA: -1  // not resolved  token: 0x6000B96
        public System.Collections.ObjectModel.ReadOnlyCollection<T> ToReadOnlyCollection() { }
        // RVA: -1  // not resolved  token: 0x6000B97
        private System.Void EnsureCapacity(System.Int32 min) { }
        // RVA: -1  // not resolved  token: 0x6000B98
        private static System.Boolean IsCompatibleObject(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000B99
        private static System.Void ValidateNullValue(System.Object value, System.String argument) { }

    }

    // TypeToken: 0x2000259
    public class RuleCache`1
    {
        // Fields
        private T[] _rules;  // 0x0
        private readonly System.Object _cacheLock;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BA0
        private System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BA1
        private T[] GetRules() { }
        // RVA: -1  // not resolved  token: 0x6000BA2
        private System.Void MoveRule(T rule, System.Int32 i) { }
        // RVA: -1  // not resolved  token: 0x6000BA3
        private System.Void AddRule(T newRule) { }
        // RVA: -1  // not resolved  token: 0x6000BA4
        private static T[] AddOrInsert(T[] rules, T item) { }

    }

    // TypeToken: 0x200025A
    public sealed class TrueReadOnlyCollection`1 : System.Collections.ObjectModel.ReadOnlyCollection`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000BA5
        public System.Void .ctor(T[] list) { }

    }

    // TypeToken: 0x200025B
    public class StrongBox`1 : System.Runtime.CompilerServices.IStrongBox
    {
        // Fields
        public T Value;  // 0x0

        // Properties
        System.Object System.Runtime.CompilerServices.IStrongBox.Value { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BA6
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BA7
        public System.Void .ctor(T value) { }

    }

    // TypeToken: 0x200025C
    public interface IStrongBox
    {
        // Properties
        System.Object Value { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

    }

}

namespace System.Security.Cryptography
{

    // TypeToken: 0x2000016  // size: 0x50
    public sealed class AesManaged : System.Security.Cryptography.Aes
    {
        // Fields
        private System.Security.Cryptography.RijndaelManaged m_rijndael;  // 0x48

        // Properties
        System.Int32 FeedbackSize { get; /* RVA: 0x0ACAFC30 */ }
        System.Byte[] IV { get; /* RVA: 0x0ACAFC54 */ set; /* RVA: 0x0ACAFD08 */ }
        System.Byte[] Key { get; /* RVA: 0x0ACAFC9C */ set; /* RVA: 0x0ACAFD50 */ }
        System.Int32 KeySize { get; /* RVA: 0x0ACAFC78 */ set; /* RVA: 0x0ACAFD2C */ }
        System.Security.Cryptography.CipherMode Mode { get; /* RVA: 0x0ACAFCC0 */ set; /* RVA: 0x0ACAFD74 */ }
        System.Security.Cryptography.PaddingMode Padding { get; /* RVA: 0x0ACAFCE4 */ set; /* RVA: 0x0ACAFE04 */ }

        // Methods
        // RVA: 0x0ACAFB58  token: 0x600002C
        public System.Void .ctor() { }
        // RVA: 0x0ACAF870  token: 0x6000038
        public virtual System.Security.Cryptography.ICryptoTransform CreateDecryptor() { }
        // RVA: 0x0ACAF6E4  token: 0x6000039
        public virtual System.Security.Cryptography.ICryptoTransform CreateDecryptor(System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x0ACAFA20  token: 0x600003A
        public virtual System.Security.Cryptography.ICryptoTransform CreateEncryptor() { }
        // RVA: 0x0ACAF894  token: 0x600003B
        public virtual System.Security.Cryptography.ICryptoTransform CreateEncryptor(System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x0ACAFA44  token: 0x600003C
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0ACAFB10  token: 0x600003D
        public virtual System.Void GenerateIV() { }
        // RVA: 0x0ACAFB34  token: 0x600003E
        public virtual System.Void GenerateKey() { }

    }

    // TypeToken: 0x2000017  // size: 0x48
    public sealed class AesCryptoServiceProvider : System.Security.Cryptography.Aes
    {
        // Properties
        System.Byte[] IV { get; /* RVA: 0x04A389B0 */ set; /* RVA: 0x0489A460 */ }
        System.Byte[] Key { get; /* RVA: 0x04A38940 */ set; /* RVA: 0x041CF1C0 */ }
        System.Int32 KeySize { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x0ACAF6DC */ }
        System.Int32 FeedbackSize { get; /* RVA: 0x04D882A0 */ }
        System.Security.Cryptography.CipherMode Mode { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x04B4EFB0 */ }
        System.Security.Cryptography.PaddingMode Padding { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04CC6110 */ }

        // Methods
        // RVA: 0x04891980  token: 0x600003F
        public System.Void .ctor() { }
        // RVA: 0x0ACAF68C  token: 0x6000040
        public virtual System.Void GenerateIV() { }
        // RVA: 0x0ACAF6B4  token: 0x6000041
        public virtual System.Void GenerateKey() { }
        // RVA: 0x03FA4E20  token: 0x6000042
        public virtual System.Security.Cryptography.ICryptoTransform CreateDecryptor(System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x0ACAF594  token: 0x6000043
        public virtual System.Security.Cryptography.ICryptoTransform CreateEncryptor(System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x03121110  token: 0x600004F
        public virtual System.Security.Cryptography.ICryptoTransform CreateDecryptor() { }
        // RVA: 0x0A773E08  token: 0x6000050
        public virtual System.Security.Cryptography.ICryptoTransform CreateEncryptor() { }
        // RVA: 0x048B52F0  token: 0x6000051
        protected virtual System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x2000018  // size: 0x68
    public class AesTransform : Mono.Security.Cryptography.SymmetricTransform
    {
        // Fields
        private System.UInt32[] expandedKey;  // 0x58
        private System.Int32 Nk;  // 0x60
        private System.Int32 Nr;  // 0x64
        private static readonly System.UInt32[] Rcon;  // static @ 0x0
        private static readonly System.Byte[] SBox;  // static @ 0x8
        private static readonly System.Byte[] iSBox;  // static @ 0x10
        private static readonly System.UInt32[] T0;  // static @ 0x18
        private static readonly System.UInt32[] T1;  // static @ 0x20
        private static readonly System.UInt32[] T2;  // static @ 0x28
        private static readonly System.UInt32[] T3;  // static @ 0x30
        private static readonly System.UInt32[] iT0;  // static @ 0x38
        private static readonly System.UInt32[] iT1;  // static @ 0x40
        private static readonly System.UInt32[] iT2;  // static @ 0x48
        private static readonly System.UInt32[] iT3;  // static @ 0x50

        // Methods
        // RVA: 0x03FA4ED0  token: 0x6000052
        public System.Void .ctor(System.Security.Cryptography.Aes algo, System.Boolean encryption, System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x03774920  token: 0x6000053
        protected virtual System.Void ECB(System.Byte[] input, System.Byte[] output) { }
        // RVA: 0x03FA53F0  token: 0x6000054
        private System.UInt32 SubByte(System.UInt32 a) { }
        // RVA: 0x0ACAFE28  token: 0x6000055
        private System.Void Encrypt128(System.Byte[] indata, System.Byte[] outdata, System.UInt32[] ekey) { }
        // RVA: 0x03774950  token: 0x6000056
        private System.Void Decrypt128(System.Byte[] indata, System.Byte[] outdata, System.UInt32[] ekey) { }
        // RVA: 0x037DA710  token: 0x6000057
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x30
    public sealed class SHA256CryptoServiceProvider : System.Security.Cryptography.SHA256
    {
        // Fields
        private static System.Byte[] Empty;  // static @ 0x0
        private System.Security.Cryptography.SHA256 hash;  // 0x28

        // Methods
        // RVA: 0x0ACCAB98  token: 0x6000058
        public System.Void .ctor() { }
        // RVA: 0x0ACCAB10  token: 0x6000059
        public virtual System.Void Initialize() { }
        // RVA: 0x0ACCAA48  token: 0x600005A
        protected virtual System.Void HashCore(System.Byte[] array, System.Int32 ibStart, System.Int32 cbSize) { }
        // RVA: 0x0ACCAA78  token: 0x600005B
        protected virtual System.Byte[] HashFinal() { }
        // RVA: 0x0ACCA9E8  token: 0x600005C
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0ACCAB34  token: 0x600005D
        private static System.Void .cctor() { }

    }

}

namespace System.Security.Cryptography.X509Certificates
{

    // TypeToken: 0x200001A  // size: 0x10
    public static class RSACertificateExtensions
    {
        // Methods
        // RVA: 0x0ACCA93C  token: 0x600005E
        public static System.Security.Cryptography.RSA GetRSAPublicKey(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate) { }

    }

}

namespace System.Threading
{

    // TypeToken: 0x200029A  // size: 0x14
    public sealed struct LockRecursionPolicy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Threading.LockRecursionPolicy NoRecursion;  // const
        public static System.Threading.LockRecursionPolicy SupportsRecursion;  // const

    }

    // TypeToken: 0x200029B  // size: 0x30
    public class ReaderWriterCount
    {
        // Fields
        public System.Int64 lockID;  // 0x10
        public System.Int32 readercount;  // 0x18
        public System.Int32 writercount;  // 0x1c
        public System.Int32 upgradecount;  // 0x20
        public System.Threading.ReaderWriterCount next;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000D4C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200029C  // size: 0x70
    public class ReaderWriterLockSlim : System.IDisposable
    {
        // Fields
        private System.Boolean fIsReentrant;  // 0x10
        private System.Int32 myLock;  // 0x14
        private System.UInt32 numWriteWaiters;  // 0x18
        private System.UInt32 numReadWaiters;  // 0x1c
        private System.UInt32 numWriteUpgradeWaiters;  // 0x20
        private System.UInt32 numUpgradeWaiters;  // 0x24
        private System.Boolean fNoWaiters;  // 0x28
        private System.Int32 upgradeLockOwnerId;  // 0x2c
        private System.Int32 writeLockOwnerId;  // 0x30
        private System.Threading.EventWaitHandle writeEvent;  // 0x38
        private System.Threading.EventWaitHandle readEvent;  // 0x40
        private System.Threading.EventWaitHandle upgradeEvent;  // 0x48
        private System.Threading.EventWaitHandle waitUpgradeEvent;  // 0x50
        private static System.Int64 s_nextLockID;  // static @ 0x0
        private System.Int64 lockID;  // 0x58
        private static System.Threading.ReaderWriterCount t_rwc;  // static @ 0xffffffff
        private System.Boolean fUpgradeThreadHoldingRead;  // 0x60
        private System.UInt32 owners;  // 0x64
        private System.Boolean fDisposed;  // 0x68

        // Properties
        System.Boolean IsReadLockHeld { get; /* RVA: 0x0AD18D34 */ }
        System.Boolean IsUpgradeableReadLockHeld { get; /* RVA: 0x0AD18D4C */ }
        System.Boolean IsWriteLockHeld { get; /* RVA: 0x0AD18D64 */ }
        System.Int32 RecursiveReadCount { get; /* RVA: 0x0AD18D7C */ }
        System.Int32 RecursiveUpgradeCount { get; /* RVA: 0x0AD18D94 */ }
        System.Int32 RecursiveWriteCount { get; /* RVA: 0x0AD18DE8 */ }
        System.Int32 WaitingReadCount { get; /* RVA: 0x04D86310 */ }
        System.Int32 WaitingUpgradeCount { get; /* RVA: 0x011F0020 */ }
        System.Int32 WaitingWriteCount { get; /* RVA: 0x011EF5B0 */ }

        // Methods
        // RVA: 0x04DAA070  token: 0x6000D4D
        private System.Void InitializeThreadCounts() { }
        // RVA: 0x053973F4  token: 0x6000D4E
        public System.Void .ctor() { }
        // RVA: 0x04D34B20  token: 0x6000D4F
        public System.Void .ctor(System.Threading.LockRecursionPolicy recursionPolicy) { }
        // RVA: 0x0AD156C4  token: 0x6000D50
        private static System.Boolean IsRWEntryEmpty(System.Threading.ReaderWriterCount rwc) { }
        // RVA: 0x0AD18A78  token: 0x6000D51
        private System.Boolean IsRwHashEntryChanged(System.Threading.ReaderWriterCount lrwc) { }
        // RVA: 0x0AD1894C  token: 0x6000D52
        private System.Threading.ReaderWriterCount GetThreadRWCount(System.Boolean dontAllocate) { }
        // RVA: 0x05397E1C  token: 0x6000D53
        public System.Void EnterWriteLock() { }
        // RVA: 0x03E6C150  token: 0x6000D54
        public System.Boolean TryEnterWriteLock(System.Int32 millisecondsTimeout) { }
        // RVA: 0x0539640C  token: 0x6000D55
        private System.Boolean TryEnterWriteLock(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout) { }
        // RVA: 0x04CA3420  token: 0x6000D56
        private System.Boolean TryEnterWriteLockCore(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout) { }
        // RVA: 0x0539305C  token: 0x6000D57
        public System.Void EnterUpgradeableReadLock() { }
        // RVA: 0x03E6C190  token: 0x6000D58
        public System.Boolean TryEnterUpgradeableReadLock(System.Int32 millisecondsTimeout) { }
        // RVA: 0x05396414  token: 0x6000D59
        private System.Boolean TryEnterUpgradeableReadLock(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout) { }
        // RVA: 0x03353B10  token: 0x6000D5A
        private System.Boolean TryEnterUpgradeableReadLockCore(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout) { }
        // RVA: 0x04D30460  token: 0x6000D5B
        public System.Void ExitWriteLock() { }
        // RVA: 0x03353610  token: 0x6000D5C
        public System.Void ExitUpgradeableReadLock() { }
        // RVA: 0x0AD18A98  token: 0x6000D5D
        private System.Void LazyCreateEvent(System.Threading.EventWaitHandle& waitEvent, System.Boolean makeAutoResetEvent) { }
        // RVA: 0x0AD18BC8  token: 0x6000D5E
        private System.Boolean WaitOnEvent(System.Threading.EventWaitHandle waitEvent, System.UInt32& numWaiters, System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout, System.Boolean isWriteWaiter) { }
        // RVA: 0x033535F0  token: 0x6000D5F
        private System.Void ExitAndWakeUpAppropriateWaiters() { }
        // RVA: 0x0AD188E0  token: 0x6000D60
        private System.Void ExitAndWakeUpAppropriateWaitersPreferringWriters() { }
        // RVA: 0x0AD18860  token: 0x6000D61
        private System.Void ExitAndWakeUpAppropriateReadWaiters() { }
        // RVA: 0x04CA3530  token: 0x6000D62
        private System.Boolean IsWriterAcquired() { }
        // RVA: 0x04DAA090  token: 0x6000D63
        private System.Void SetWriterAcquired() { }
        // RVA: 0x04DAA030  token: 0x6000D64
        private System.Void ClearWriterAcquired() { }
        // RVA: 0x04DAA0A0  token: 0x6000D65
        private System.Void SetWritersWaiting() { }
        // RVA: 0x04DAA040  token: 0x6000D66
        private System.Void ClearWritersWaiting() { }
        // RVA: 0x04DAA080  token: 0x6000D67
        private System.Void SetUpgraderWaiting() { }
        // RVA: 0x04DAA020  token: 0x6000D68
        private System.Void ClearUpgraderWaiting() { }
        // RVA: 0x04DAA060  token: 0x6000D69
        private System.UInt32 GetNumReaders() { }
        // RVA: 0x03354760  token: 0x6000D6A
        private System.Void EnterMyLock() { }
        // RVA: 0x0AD187CC  token: 0x6000D6B
        private System.Void EnterMyLockSpin() { }
        // RVA: 0x04DAA050  token: 0x6000D6C
        private System.Void ExitMyLock() { }
        // RVA: 0x0AD18B6C  token: 0x6000D6D
        private static System.Void SpinWait(System.Int32 SpinCount) { }
        // RVA: 0x0AD187C0  token: 0x6000D6E
        public virtual System.Void Dispose() { }
        // RVA: 0x0AD18618  token: 0x6000D6F
        private System.Void Dispose(System.Boolean disposing) { }

    }

}

