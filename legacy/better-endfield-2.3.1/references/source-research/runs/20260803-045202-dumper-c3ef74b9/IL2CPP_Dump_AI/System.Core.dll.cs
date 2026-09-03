// ========================================================
// Dumped by @desirepro
// Assembly: System.Core.dll
// Classes:  673
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

CLASS: SR
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0464E630  token=0x6000001  System.String GetString(System.String name)
  RVA=0x0ACCAD50  token=0x6000002  System.String Format(System.String resourceFormat, System.Object[] args)
  RVA=0x0ACCAC74  token=0x6000003  System.String Format(System.String resourceFormat, System.Object p1)
  RVA=0x0ACCAC04  token=0x6000004  System.String Format(System.String resourceFormat, System.Object p1, System.Object p2)
  RVA=0x0ACCACD0  token=0x6000005  System.String Format(System.String resourceFormat, System.Object p1, System.Object p2, System.Object p3)
END_CLASS

CLASS: Iterator`1
TYPE:  abstract class
TOKEN: 0x200001D
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    threadId  // 0x0
  private           System.Int32                    state  // 0x0
  private           TSource                         current  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000AE  System.Void .ctor()
  RVA=-1  // abstract  token=0x60000B0  System.Linq.Enumerable.Iterator<TSource> Clone()
  RVA=-1  // not resolved  token=0x60000B1  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60000B2  System.Collections.Generic.IEnumerator<TSource> GetEnumerator()
  RVA=-1  // abstract  token=0x60000B3  System.Boolean MoveNext()
  RVA=-1  // abstract  token=0x60000B4  System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector)
  RVA=-1  // abstract  token=0x60000B5  System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // not resolved  token=0x60000B7  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000B8  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: WhereEnumerableIterator`1
TYPE:  class
TOKEN: 0x200001E
EXTENDS: Iterator`1
FIELDS:
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  private           System.Func<TSource,System.Boolean>predicate  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource>enumerator  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000B9  System.Void .ctor(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // not resolved  token=0x60000BA  System.Linq.Enumerable.Iterator<TSource> Clone()
  RVA=-1  // not resolved  token=0x60000BB  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60000BC  System.Boolean MoveNext()
  RVA=-1  // generic def  token=0x60000BD  System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector)
  RVA=-1  // not resolved  token=0x60000BE  System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate)
END_CLASS

CLASS: WhereArrayIterator`1
TYPE:  class
TOKEN: 0x200001F
EXTENDS: Iterator`1
FIELDS:
  private           TSource[]                       source  // 0x0
  private           System.Func<TSource,System.Boolean>predicate  // 0x0
  private           System.Int32                    index  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000BF  System.Void .ctor(TSource[] source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // not resolved  token=0x60000C0  System.Linq.Enumerable.Iterator<TSource> Clone()
  RVA=-1  // not resolved  token=0x60000C1  System.Boolean MoveNext()
  RVA=-1  // generic def  token=0x60000C2  System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector)
  RVA=-1  // not resolved  token=0x60000C3  System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate)
END_CLASS

CLASS: WhereListIterator`1
TYPE:  class
TOKEN: 0x2000020
EXTENDS: Iterator`1
FIELDS:
  private           System.Collections.Generic.List<TSource>source  // 0x0
  private           System.Func<TSource,System.Boolean>predicate  // 0x0
  private           System.Collections.Generic.List.Enumerator<TSource>enumerator  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000C4  System.Void .ctor(System.Collections.Generic.List<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // not resolved  token=0x60000C5  System.Linq.Enumerable.Iterator<TSource> Clone()
  RVA=-1  // not resolved  token=0x60000C6  System.Boolean MoveNext()
  RVA=-1  // generic def  token=0x60000C7  System.Collections.Generic.IEnumerable<TResult> Select(System.Func<TSource,TResult> selector)
  RVA=-1  // not resolved  token=0x60000C8  System.Collections.Generic.IEnumerable<TSource> Where(System.Func<TSource,System.Boolean> predicate)
END_CLASS

CLASS: WhereSelectEnumerableIterator`2
TYPE:  class
TOKEN: 0x2000021
EXTENDS: Iterator`1
FIELDS:
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  private           System.Func<TSource,System.Boolean>predicate  // 0x0
  private           System.Func<TSource,TResult>    selector  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource>enumerator  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000C9  System.Void .ctor(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate, System.Func<TSource,TResult> selector)
  RVA=-1  // not resolved  token=0x60000CA  System.Linq.Enumerable.Iterator<TResult> Clone()
  RVA=-1  // not resolved  token=0x60000CB  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60000CC  System.Boolean MoveNext()
  RVA=-1  // generic def  token=0x60000CD  System.Collections.Generic.IEnumerable<TResult2> Select(System.Func<TResult,TResult2> selector)
  RVA=-1  // not resolved  token=0x60000CE  System.Collections.Generic.IEnumerable<TResult> Where(System.Func<TResult,System.Boolean> predicate)
END_CLASS

CLASS: WhereSelectArrayIterator`2
TYPE:  class
TOKEN: 0x2000022
EXTENDS: Iterator`1
FIELDS:
  private           TSource[]                       source  // 0x0
  private           System.Func<TSource,System.Boolean>predicate  // 0x0
  private           System.Func<TSource,TResult>    selector  // 0x0
  private           System.Int32                    index  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000CF  System.Void .ctor(TSource[] source, System.Func<TSource,System.Boolean> predicate, System.Func<TSource,TResult> selector)
  RVA=-1  // not resolved  token=0x60000D0  System.Linq.Enumerable.Iterator<TResult> Clone()
  RVA=-1  // not resolved  token=0x60000D1  System.Boolean MoveNext()
  RVA=-1  // generic def  token=0x60000D2  System.Collections.Generic.IEnumerable<TResult2> Select(System.Func<TResult,TResult2> selector)
  RVA=-1  // not resolved  token=0x60000D3  System.Collections.Generic.IEnumerable<TResult> Where(System.Func<TResult,System.Boolean> predicate)
END_CLASS

CLASS: WhereSelectListIterator`2
TYPE:  class
TOKEN: 0x2000023
EXTENDS: Iterator`1
FIELDS:
  private           System.Collections.Generic.List<TSource>source  // 0x0
  private           System.Func<TSource,System.Boolean>predicate  // 0x0
  private           System.Func<TSource,TResult>    selector  // 0x0
  private           System.Collections.Generic.List.Enumerator<TSource>enumerator  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000D4  System.Void .ctor(System.Collections.Generic.List<TSource> source, System.Func<TSource,System.Boolean> predicate, System.Func<TSource,TResult> selector)
  RVA=-1  // not resolved  token=0x60000D5  System.Linq.Enumerable.Iterator<TResult> Clone()
  RVA=-1  // not resolved  token=0x60000D6  System.Boolean MoveNext()
  RVA=-1  // generic def  token=0x60000D7  System.Collections.Generic.IEnumerable<TResult2> Select(System.Func<TResult,TResult2> selector)
  RVA=-1  // not resolved  token=0x60000D8  System.Collections.Generic.IEnumerable<TResult> Where(System.Func<TResult,System.Boolean> predicate)
END_CLASS

CLASS: <>c__DisplayClass6_0`1
TYPE:  sealed class
TOKEN: 0x2000024
FIELDS:
  public            System.Func<TSource,System.Boolean>predicate1  // 0x0
  public            System.Func<TSource,System.Boolean>predicate2  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000D9  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000DA  System.Boolean <CombinePredicates>b__0(TSource x)
END_CLASS

CLASS: <>c__DisplayClass7_0`3
TYPE:  sealed class
TOKEN: 0x2000025
FIELDS:
  public            System.Func<TMiddle,TResult>    selector2  // 0x0
  public            System.Func<TSource,TMiddle>    selector1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000DB  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000DC  TResult <CombineSelectors>b__0(TSource x)
END_CLASS

CLASS: <SelectManyIterator>d__17`2
TYPE:  sealed class
TOKEN: 0x2000026
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TResult                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__source  // 0x0
  private           System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>>selector  // 0x0
  public            System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>><>3__selector  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><>7__wrap1  // 0x0
  private           System.Collections.Generic.IEnumerator<TResult><>7__wrap2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TResult>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000DD  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60000DE  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60000DF  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60000E0  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x60000E1  System.Void <>m__Finally2()
  RVA=-1  // not resolved  token=0x60000E3  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x60000E5  System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000E6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <SelectManyIterator>d__23`3
TYPE:  sealed class
TOKEN: 0x2000027
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TResult                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__source  // 0x0
  private           System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>>collectionSelector  // 0x0
  public            System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>><>3__collectionSelector  // 0x0
  private           System.Func<TSource,TCollection,TResult>resultSelector  // 0x0
  public            System.Func<TSource,TCollection,TResult><>3__resultSelector  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><>7__wrap1  // 0x0
  private           TSource                         <element>5__3  // 0x0
  private           System.Collections.Generic.IEnumerator<TCollection><>7__wrap3  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TResult>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000E7  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60000E8  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60000E9  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60000EA  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x60000EB  System.Void <>m__Finally2()
  RVA=-1  // not resolved  token=0x60000ED  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x60000EF  System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000F0  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <SkipIterator>d__31`1
TYPE:  sealed class
TOKEN: 0x2000028
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TSource                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__source  // 0x0
  private           System.Int32                    count  // 0x0
  public            System.Int32                    <>3__count  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><e>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TSource>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000F1  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60000F2  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60000F3  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60000F4  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x60000F6  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x60000F8  System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000F9  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <JoinIterator>d__38`4
TYPE:  sealed class
TOKEN: 0x2000029
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TResult                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEnumerable<TInner>inner  // 0x0
  public            System.Collections.Generic.IEnumerable<TInner><>3__inner  // 0x0
  private           System.Func<TInner,TKey>        innerKeySelector  // 0x0
  public            System.Func<TInner,TKey>        <>3__innerKeySelector  // 0x0
  private           System.Collections.Generic.IEqualityComparer<TKey>comparer  // 0x0
  public            System.Collections.Generic.IEqualityComparer<TKey><>3__comparer  // 0x0
  private           System.Collections.Generic.IEnumerable<TOuter>outer  // 0x0
  public            System.Collections.Generic.IEnumerable<TOuter><>3__outer  // 0x0
  private           System.Func<TOuter,TKey>        outerKeySelector  // 0x0
  public            System.Func<TOuter,TKey>        <>3__outerKeySelector  // 0x0
  private           System.Func<TOuter,TInner,TResult>resultSelector  // 0x0
  public            System.Func<TOuter,TInner,TResult><>3__resultSelector  // 0x0
  private           System.Linq.Lookup<TKey,TInner> <lookup>5__2  // 0x0
  private           System.Collections.Generic.IEnumerator<TOuter><>7__wrap2  // 0x0
  private           TOuter                          <item>5__4  // 0x0
  private           System.Linq.Lookup.Grouping<TKey,TInner><g>5__5  // 0x0
  private           System.Int32                    <i>5__6  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TResult>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000FA  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60000FB  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60000FC  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60000FD  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x60000FF  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x6000101  System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000102  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <ConcatIterator>d__59`1
TYPE:  sealed class
TOKEN: 0x200002A
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TSource                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>first  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__first  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>second  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__second  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><>7__wrap1  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TSource>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000103  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000104  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000105  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000106  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x6000107  System.Void <>m__Finally2()
  RVA=-1  // not resolved  token=0x6000109  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x600010B  System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600010C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <PrependIterator>d__63`1
TYPE:  sealed class
TOKEN: 0x200002B
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TSource                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           TSource                         element  // 0x0
  public            TSource                         <>3__element  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__source  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><>7__wrap1  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TSource>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600010D  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600010E  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600010F  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000110  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x6000112  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x6000114  System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000115  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <DistinctIterator>d__68`1
TYPE:  sealed class
TOKEN: 0x200002C
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TSource                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEqualityComparer<TSource>comparer  // 0x0
  public            System.Collections.Generic.IEqualityComparer<TSource><>3__comparer  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__source  // 0x0
  private           System.Linq.Set<TSource>        <set>5__2  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><>7__wrap2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TSource>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000116  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000117  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000118  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000119  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x600011B  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x600011D  System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600011E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <UnionIterator>d__71`1
TYPE:  sealed class
TOKEN: 0x200002D
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TSource                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEqualityComparer<TSource>comparer  // 0x0
  public            System.Collections.Generic.IEqualityComparer<TSource><>3__comparer  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>first  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__first  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>second  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__second  // 0x0
  private           System.Linq.Set<TSource>        <set>5__2  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><>7__wrap2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TSource>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600011F  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000120  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000121  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000122  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x6000123  System.Void <>m__Finally2()
  RVA=-1  // not resolved  token=0x6000125  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x6000127  System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000128  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <IntersectIterator>d__74`1
TYPE:  sealed class
TOKEN: 0x200002E
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TSource                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEqualityComparer<TSource>comparer  // 0x0
  public            System.Collections.Generic.IEqualityComparer<TSource><>3__comparer  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>second  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__second  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>first  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__first  // 0x0
  private           System.Linq.Set<TSource>        <set>5__2  // 0x0
  private           System.Collections.Generic.IEnumerator<TSource><>7__wrap2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TSource>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000129  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600012A  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600012B  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600012C  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x600012E  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x6000130  System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000131  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <ReverseIterator>d__79`1
TYPE:  sealed class
TOKEN: 0x200002F
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TSource                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  public            System.Collections.Generic.IEnumerable<TSource><>3__source  // 0x0
  private           System.Linq.Buffer<TSource>     <buffer>5__2  // 0x0
  private           System.Int32                    <i>5__3  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TSource>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000132  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000133  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000134  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000136  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x6000138  System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000139  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <OfTypeIterator>d__97`1
TYPE:  sealed class
TOKEN: 0x2000030
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TResult                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.IEnumerable  source  // 0x0
  public            System.Collections.IEnumerable  <>3__source  // 0x0
  private           System.Collections.IEnumerator  <>7__wrap1  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TResult>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600013A  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600013B  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600013C  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600013D  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x600013F  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x6000141  System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000142  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <CastIterator>d__99`1
TYPE:  sealed class
TOKEN: 0x2000031
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TResult                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.IEnumerable  source  // 0x0
  public            System.Collections.IEnumerable  <>3__source  // 0x0
  private           System.Collections.IEnumerator  <>7__wrap1  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TResult>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000143  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000144  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000145  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000146  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x6000148  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x600014A  System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600014B  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <RangeIterator>d__115
TYPE:  sealed class
TOKEN: 0x2000032
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Int32                    <>2__current  // 0x14
  private           System.Int32                    <>l__initialThreadId  // 0x18
  private           System.Int32                    start  // 0x1c
  public            System.Int32                    <>3__start  // 0x20
  private           System.Int32                    count  // 0x24
  public            System.Int32                    <>3__count  // 0x28
  private           System.Int32                    <i>5__2  // 0x2c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Int32>.Current  get=0x04D882A0
  System.Collections.IEnumerator.Current  get=0x0ACCAE60
METHODS:
  RVA=0x02EDACA0  token=0x600014C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600014D  System.Void System.IDisposable.Dispose()
  RVA=0x0460CAA0  token=0x600014E  System.Boolean MoveNext()
  RVA=0x0ACCAE14  token=0x6000150  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x04CA1C00  token=0x6000152  System.Collections.Generic.IEnumerator<System.Int32> System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator()
  RVA=0x0ACCAE0C  token=0x6000153  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <RepeatIterator>d__117`1
TYPE:  sealed class
TOKEN: 0x2000033
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TResult                         <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           TResult                         element  // 0x0
  public            TResult                         <>3__element  // 0x0
  private           System.Int32                    count  // 0x0
  public            System.Int32                    <>3__count  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TResult>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000154  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000155  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000156  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000158  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x600015A  System.Collections.Generic.IEnumerator<TResult> System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600015B  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000036
FIELDS:
  public    static readonly System.Linq.IdentityFunction.<>c<TElement><>9  // static @ 0x0
  public    static  System.Func<TElement,TElement>  <>9__1_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600015E  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600015F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000160  TElement <get_Instance>b__1_0(TElement x)
END_CLASS

CLASS: <GetEnumerator>d__7
TYPE:  sealed class
TOKEN: 0x200003C
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TElement                        <>2__current  // 0x0
  public            System.Linq.Lookup.Grouping<TKey,TElement><>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TElement>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600017C  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600017D  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600017E  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000180  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Grouping
TYPE:  class
TOKEN: 0x200003B
IMPLEMENTS: System.Linq.IGrouping`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1
FIELDS:
  private           TKey                            key  // 0x0
  private           System.Int32                    hashCode  // 0x0
  private           TElement[]                      elements  // 0x0
  private           System.Int32                    count  // 0x0
  private           System.Linq.Lookup.Grouping<TKey,TElement>hashNext  // 0x0
  private           System.Linq.Lookup.Grouping<TKey,TElement>next  // 0x0
PROPERTIES:
  Key  get=-1  // not resolved
  System.Collections.Generic.ICollection<TElement>.Count  get=-1  // not resolved
  System.Collections.Generic.ICollection<TElement>.IsReadOnly  get=-1  // not resolved
  System.Collections.Generic.IList<TElement>.Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600016B  System.Void Add(TElement element)
  RVA=-1  // not resolved  token=0x600016C  System.Collections.Generic.IEnumerator<TElement> GetEnumerator()
  RVA=-1  // not resolved  token=0x600016D  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000171  System.Void System.Collections.Generic.ICollection<TElement>.Add(TElement item)
  RVA=-1  // not resolved  token=0x6000172  System.Void System.Collections.Generic.ICollection<TElement>.Clear()
  RVA=-1  // not resolved  token=0x6000173  System.Boolean System.Collections.Generic.ICollection<TElement>.Contains(TElement item)
  RVA=-1  // not resolved  token=0x6000174  System.Void System.Collections.Generic.ICollection<TElement>.CopyTo(TElement[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000175  System.Boolean System.Collections.Generic.ICollection<TElement>.Remove(TElement item)
  RVA=-1  // not resolved  token=0x6000176  System.Int32 System.Collections.Generic.IList<TElement>.IndexOf(TElement item)
  RVA=-1  // not resolved  token=0x6000177  System.Void System.Collections.Generic.IList<TElement>.Insert(System.Int32 index, TElement item)
  RVA=-1  // not resolved  token=0x6000178  System.Void System.Collections.Generic.IList<TElement>.RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x600017B  System.Void .ctor()
END_CLASS

CLASS: <GetEnumerator>d__12
TYPE:  sealed class
TOKEN: 0x200003D
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Linq.IGrouping<TKey,TElement><>2__current  // 0x0
  public            System.Linq.Lookup<TKey,TElement><>4__this  // 0x0
  private           System.Linq.Lookup.Grouping<TKey,TElement><g>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000182  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000183  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000184  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000186  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Slot
TYPE:  sealed struct
TOKEN: 0x200003F
FIELDS:
  private           System.Int32                    hashCode  // 0x0
  private           TElement                        value  // 0x0
  private           System.Int32                    next  // 0x0
METHODS:
END_CLASS

CLASS: <GetEnumerator>d__1
TYPE:  sealed class
TOKEN: 0x2000042
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TElement                        <>2__current  // 0x0
  public            System.Linq.OrderedEnumerable<TElement><>4__this  // 0x0
  private           System.Linq.Buffer<TElement>    <buffer>5__2  // 0x0
  private           System.Int32[]                  <map>5__3  // 0x0
  private           System.Int32                    <i>5__4  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TElement>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000196  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000197  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000198  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600019A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: BinaryExpressionProxy
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: BlockExpressionProxy
TYPE:  class
TOKEN: 0x2000051
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: CatchBlockProxy
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: ConditionalExpressionProxy
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: ConstantExpressionProxy
TYPE:  class
TOKEN: 0x2000054
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DebugInfoExpressionProxy
TYPE:  class
TOKEN: 0x2000055
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DefaultExpressionProxy
TYPE:  class
TOKEN: 0x2000056
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: GotoExpressionProxy
TYPE:  class
TOKEN: 0x2000057
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: IndexExpressionProxy
TYPE:  class
TOKEN: 0x2000058
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: InvocationExpressionProxy
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: LabelExpressionProxy
TYPE:  class
TOKEN: 0x200005A
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: LambdaExpressionProxy
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: ListInitExpressionProxy
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: LoopExpressionProxy
TYPE:  class
TOKEN: 0x200005D
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: MemberExpressionProxy
TYPE:  class
TOKEN: 0x200005E
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: MemberInitExpressionProxy
TYPE:  class
TOKEN: 0x200005F
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: MethodCallExpressionProxy
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: NewArrayExpressionProxy
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: NewExpressionProxy
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: ParameterExpressionProxy
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: RuntimeVariablesExpressionProxy
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: SwitchCaseProxy
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: SwitchExpressionProxy
TYPE:  class
TOKEN: 0x2000066
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: TryExpressionProxy
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: TypeBinaryExpressionProxy
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: UnaryExpressionProxy
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: ExtensionInfo
TYPE:  class
TOKEN: 0x200006A
SIZE:  0x20
FIELDS:
  private   readonly System.Linq.Expressions.ExpressionTypeNodeType  // 0x10
  private   readonly System.Type                     Type  // 0x18
METHODS:
END_CLASS

CLASS: <GetEnumerator>d__18
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Linq.Expressions.Expression<>2__current  // 0x18
  public            System.Linq.Expressions.BlockExpressionList<>4__this  // 0x20
  private           System.Int32                    <i>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Linq.Expressions.Expression>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002FC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002FD  System.Void System.IDisposable.Dispose()
  RVA=0x0ACE2610  token=0x60002FE  System.Boolean MoveNext()
  RVA=0x0ACE26D4  token=0x6000300  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__3`2
TYPE:  sealed class
TOKEN: 0x20000C2
FIELDS:
  public    static readonly System.Linq.Expressions.StackGuard.<>c__3<T1,T2><>9  // static @ 0x0
  public    static  System.Func<System.Object,System.Object><>9__3_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004E4  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60004E5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004E6  System.Object <RunOnEmptyStack>b__3_0(System.Object s)
END_CLASS

CLASS: AddInt16
TYPE:  sealed class
TOKEN: 0x20000CC
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCB400  token=0x6000590  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000591  System.Void .ctor()
END_CLASS

CLASS: AddInt32
TYPE:  sealed class
TOKEN: 0x20000CD
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCB514  token=0x6000592  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000593  System.Void .ctor()
END_CLASS

CLASS: AddInt64
TYPE:  sealed class
TOKEN: 0x20000CE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCB618  token=0x6000594  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000595  System.Void .ctor()
END_CLASS

CLASS: AddUInt16
TYPE:  sealed class
TOKEN: 0x20000CF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC518  token=0x6000596  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000597  System.Void .ctor()
END_CLASS

CLASS: AddUInt32
TYPE:  sealed class
TOKEN: 0x20000D0
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC62C  token=0x6000598  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000599  System.Void .ctor()
END_CLASS

CLASS: AddUInt64
TYPE:  sealed class
TOKEN: 0x20000D1
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC73C  token=0x600059A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600059B  System.Void .ctor()
END_CLASS

CLASS: AddSingle
TYPE:  sealed class
TOKEN: 0x20000D2
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC3F8  token=0x600059C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600059D  System.Void .ctor()
END_CLASS

CLASS: AddDouble
TYPE:  sealed class
TOKEN: 0x20000D3
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddInstruction
FIELDS:
METHODS:
  RVA=0x0ACCAEA4  token=0x600059E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600059F  System.Void .ctor()
END_CLASS

CLASS: AddOvfInt16
TYPE:  sealed class
TOKEN: 0x20000D5
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddOvfInstruction
FIELDS:
METHODS:
  RVA=0x0ACCBA74  token=0x60005A5  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005A6  System.Void .ctor()
END_CLASS

CLASS: AddOvfInt32
TYPE:  sealed class
TOKEN: 0x20000D6
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddOvfInstruction
FIELDS:
METHODS:
  RVA=0x0ACCBC68  token=0x60005A7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005A8  System.Void .ctor()
END_CLASS

CLASS: AddOvfInt64
TYPE:  sealed class
TOKEN: 0x20000D7
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddOvfInstruction
FIELDS:
METHODS:
  RVA=0x0ACCBE00  token=0x60005A9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005AA  System.Void .ctor()
END_CLASS

CLASS: AddOvfUInt16
TYPE:  sealed class
TOKEN: 0x20000D8
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddOvfInstruction
FIELDS:
METHODS:
  RVA=0x0ACCBF70  token=0x60005AB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005AC  System.Void .ctor()
END_CLASS

CLASS: AddOvfUInt32
TYPE:  sealed class
TOKEN: 0x20000D9
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddOvfInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC11C  token=0x60005AD  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005AE  System.Void .ctor()
END_CLASS

CLASS: AddOvfUInt64
TYPE:  sealed class
TOKEN: 0x20000DA
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AddOvfInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC28C  token=0x60005AF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005B0  System.Void .ctor()
END_CLASS

CLASS: AndSByte
TYPE:  sealed class
TOKEN: 0x20000DC
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCD0D0  token=0x60005B6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005B7  System.Void .ctor()
END_CLASS

CLASS: AndInt16
TYPE:  sealed class
TOKEN: 0x20000DD
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCCEB0  token=0x60005B8  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005B9  System.Void .ctor()
END_CLASS

CLASS: AndInt32
TYPE:  sealed class
TOKEN: 0x20000DE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCCF60  token=0x60005BA  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005BB  System.Void .ctor()
END_CLASS

CLASS: AndInt64
TYPE:  sealed class
TOKEN: 0x20000DF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCD010  token=0x60005BC  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005BD  System.Void .ctor()
END_CLASS

CLASS: AndByte
TYPE:  sealed class
TOKEN: 0x20000E0
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC944  token=0x60005BE  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005BF  System.Void .ctor()
END_CLASS

CLASS: AndUInt16
TYPE:  sealed class
TOKEN: 0x20000E1
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCD180  token=0x60005C0  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005C1  System.Void .ctor()
END_CLASS

CLASS: AndUInt32
TYPE:  sealed class
TOKEN: 0x20000E2
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCD230  token=0x60005C2  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005C3  System.Void .ctor()
END_CLASS

CLASS: AndUInt64
TYPE:  sealed class
TOKEN: 0x20000E3
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCD2EC  token=0x60005C4  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005C5  System.Void .ctor()
END_CLASS

CLASS: AndBoolean
TYPE:  sealed class
TOKEN: 0x20000E4
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.AndInstruction
FIELDS:
METHODS:
  RVA=0x0ACCC850  token=0x60005C6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60005C7  System.Void .ctor()
END_CLASS

CLASS: DecrementInt16
TYPE:  sealed class
TOKEN: 0x2000106
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD0688  token=0x6000682  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000683  System.Void .ctor()
END_CLASS

CLASS: DecrementInt32
TYPE:  sealed class
TOKEN: 0x2000107
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD070C  token=0x6000684  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000685  System.Void .ctor()
END_CLASS

CLASS: DecrementInt64
TYPE:  sealed class
TOKEN: 0x2000108
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD0784  token=0x6000686  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000687  System.Void .ctor()
END_CLASS

CLASS: DecrementUInt16
TYPE:  sealed class
TOKEN: 0x2000109
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD08AC  token=0x6000688  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000689  System.Void .ctor()
END_CLASS

CLASS: DecrementUInt32
TYPE:  sealed class
TOKEN: 0x200010A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD0930  token=0x600068A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600068B  System.Void .ctor()
END_CLASS

CLASS: DecrementUInt64
TYPE:  sealed class
TOKEN: 0x200010B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD09C0  token=0x600068C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600068D  System.Void .ctor()
END_CLASS

CLASS: DecrementSingle
TYPE:  sealed class
TOKEN: 0x200010C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD0814  token=0x600068E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600068F  System.Void .ctor()
END_CLASS

CLASS: DecrementDouble
TYPE:  sealed class
TOKEN: 0x200010D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DecrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACD01B4  token=0x6000690  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000691  System.Void .ctor()
END_CLASS

CLASS: DivInt16
TYPE:  sealed class
TOKEN: 0x2000110
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD1108  token=0x600069C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600069D  System.Void .ctor()
END_CLASS

CLASS: DivInt32
TYPE:  sealed class
TOKEN: 0x2000111
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD121C  token=0x600069E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600069F  System.Void .ctor()
END_CLASS

CLASS: DivInt64
TYPE:  sealed class
TOKEN: 0x2000112
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD1324  token=0x60006A0  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006A1  System.Void .ctor()
END_CLASS

CLASS: DivUInt16
TYPE:  sealed class
TOKEN: 0x2000113
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD1554  token=0x60006A2  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006A3  System.Void .ctor()
END_CLASS

CLASS: DivUInt32
TYPE:  sealed class
TOKEN: 0x2000114
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD1668  token=0x60006A4  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006A5  System.Void .ctor()
END_CLASS

CLASS: DivUInt64
TYPE:  sealed class
TOKEN: 0x2000115
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD177C  token=0x60006A6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006A7  System.Void .ctor()
END_CLASS

CLASS: DivSingle
TYPE:  sealed class
TOKEN: 0x2000116
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD143C  token=0x60006A8  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006A9  System.Void .ctor()
END_CLASS

CLASS: DivDouble
TYPE:  sealed class
TOKEN: 0x2000117
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.DivInstruction
FIELDS:
METHODS:
  RVA=0x0ACD0BB4  token=0x60006AA  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006AB  System.Void .ctor()
END_CLASS

CLASS: EqualBoolean
TYPE:  sealed class
TOKEN: 0x2000119
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD2910  token=0x60006B1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006B2  System.Void .ctor()
END_CLASS

CLASS: EqualSByte
TYPE:  sealed class
TOKEN: 0x200011A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3F1C  token=0x60006B3  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006B4  System.Void .ctor()
END_CLASS

CLASS: EqualInt16
TYPE:  sealed class
TOKEN: 0x200011B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3AAC  token=0x60006B5  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006B6  System.Void .ctor()
END_CLASS

CLASS: EqualChar
TYPE:  sealed class
TOKEN: 0x200011C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD2BD0  token=0x60006B7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006B8  System.Void .ctor()
END_CLASS

CLASS: EqualInt32
TYPE:  sealed class
TOKEN: 0x200011D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3C0C  token=0x60006B9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006BA  System.Void .ctor()
END_CLASS

CLASS: EqualInt64
TYPE:  sealed class
TOKEN: 0x200011E
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3D6C  token=0x60006BB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006BC  System.Void .ctor()
END_CLASS

CLASS: EqualByte
TYPE:  sealed class
TOKEN: 0x200011F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD2A70  token=0x60006BD  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006BE  System.Void .ctor()
END_CLASS

CLASS: EqualUInt16
TYPE:  sealed class
TOKEN: 0x2000120
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD4208  token=0x60006BF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006C0  System.Void .ctor()
END_CLASS

CLASS: EqualUInt32
TYPE:  sealed class
TOKEN: 0x2000121
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD4368  token=0x60006C1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006C2  System.Void .ctor()
END_CLASS

CLASS: EqualUInt64
TYPE:  sealed class
TOKEN: 0x2000122
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD44C8  token=0x60006C3  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006C4  System.Void .ctor()
END_CLASS

CLASS: EqualSingle
TYPE:  sealed class
TOKEN: 0x2000123
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD4090  token=0x60006C5  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006C6  System.Void .ctor()
END_CLASS

CLASS: EqualDouble
TYPE:  sealed class
TOKEN: 0x2000124
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD2D44  token=0x60006C7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006C8  System.Void .ctor()
END_CLASS

CLASS: EqualReference
TYPE:  sealed class
TOKEN: 0x2000125
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3E18  token=0x60006C9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006CA  System.Void .ctor()
END_CLASS

CLASS: EqualBooleanLiftedToNull
TYPE:  sealed class
TOKEN: 0x2000126
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD285C  token=0x60006CB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006CC  System.Void .ctor()
END_CLASS

CLASS: EqualSByteLiftedToNull
TYPE:  sealed class
TOKEN: 0x2000127
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3E68  token=0x60006CD  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006CE  System.Void .ctor()
END_CLASS

CLASS: EqualInt16LiftedToNull
TYPE:  sealed class
TOKEN: 0x2000128
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD39F8  token=0x60006CF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006D0  System.Void .ctor()
END_CLASS

CLASS: EqualCharLiftedToNull
TYPE:  sealed class
TOKEN: 0x2000129
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD2B1C  token=0x60006D1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006D2  System.Void .ctor()
END_CLASS

CLASS: EqualInt32LiftedToNull
TYPE:  sealed class
TOKEN: 0x200012A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3B58  token=0x60006D3  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006D4  System.Void .ctor()
END_CLASS

CLASS: EqualInt64LiftedToNull
TYPE:  sealed class
TOKEN: 0x200012B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3CB8  token=0x60006D5  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006D6  System.Void .ctor()
END_CLASS

CLASS: EqualByteLiftedToNull
TYPE:  sealed class
TOKEN: 0x200012C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD29BC  token=0x60006D7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006D8  System.Void .ctor()
END_CLASS

CLASS: EqualUInt16LiftedToNull
TYPE:  sealed class
TOKEN: 0x200012D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD4154  token=0x60006D9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006DA  System.Void .ctor()
END_CLASS

CLASS: EqualUInt32LiftedToNull
TYPE:  sealed class
TOKEN: 0x200012E
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD42B4  token=0x60006DB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006DC  System.Void .ctor()
END_CLASS

CLASS: EqualUInt64LiftedToNull
TYPE:  sealed class
TOKEN: 0x200012F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD4414  token=0x60006DD  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006DE  System.Void .ctor()
END_CLASS

CLASS: EqualSingleLiftedToNull
TYPE:  sealed class
TOKEN: 0x2000130
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD3FC8  token=0x60006DF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006E0  System.Void .ctor()
END_CLASS

CLASS: EqualDoubleLiftedToNull
TYPE:  sealed class
TOKEN: 0x2000131
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.EqualInstruction
FIELDS:
METHODS:
  RVA=0x0ACD2C7C  token=0x60006E1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006E2  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrSByte
TYPE:  sealed class
TOKEN: 0x2000133
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACD879C  token=0x60006E8  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006E9  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrInt16
TYPE:  sealed class
TOKEN: 0x2000134
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACD857C  token=0x60006EA  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006EB  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrInt32
TYPE:  sealed class
TOKEN: 0x2000135
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACD862C  token=0x60006EC  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006ED  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrInt64
TYPE:  sealed class
TOKEN: 0x2000136
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACD86DC  token=0x60006EE  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006EF  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrByte
TYPE:  sealed class
TOKEN: 0x2000137
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACE4D80  token=0x60006F0  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006F1  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrUInt16
TYPE:  sealed class
TOKEN: 0x2000138
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACE4E30  token=0x60006F2  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006F3  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrUInt32
TYPE:  sealed class
TOKEN: 0x2000139
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACE4EE0  token=0x60006F4  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006F5  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrUInt64
TYPE:  sealed class
TOKEN: 0x200013A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACE4F9C  token=0x60006F6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006F7  System.Void .ctor()
END_CLASS

CLASS: ExclusiveOrBoolean
TYPE:  sealed class
TOKEN: 0x200013B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
FIELDS:
METHODS:
  RVA=0x0ACE4CCC  token=0x60006F8  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60006F9  System.Void .ctor()
END_CLASS

CLASS: GreaterThanSByte
TYPE:  sealed class
TOKEN: 0x2000142
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000712  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE76E4  token=0x6000713  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanInt16
TYPE:  sealed class
TOKEN: 0x2000143
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000714  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE5FF0  token=0x6000715  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanChar
TYPE:  sealed class
TOKEN: 0x2000144
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000716  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE51A0  token=0x6000717  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanInt32
TYPE:  sealed class
TOKEN: 0x2000145
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000718  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE60A8  token=0x6000719  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanInt64
TYPE:  sealed class
TOKEN: 0x2000146
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600071A  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE6160  token=0x600071B  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanByte
TYPE:  sealed class
TOKEN: 0x2000147
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600071C  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE50E8  token=0x600071D  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanUInt16
TYPE:  sealed class
TOKEN: 0x2000148
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600071E  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE7858  token=0x600071F  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanUInt32
TYPE:  sealed class
TOKEN: 0x2000149
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000720  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE7910  token=0x6000721  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanUInt64
TYPE:  sealed class
TOKEN: 0x200014A
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000722  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE79C8  token=0x6000723  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanSingle
TYPE:  sealed class
TOKEN: 0x200014B
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000724  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE779C  token=0x6000725  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanDouble
TYPE:  sealed class
TOKEN: 0x200014C
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000726  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE5258  token=0x6000727  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualSByte
TYPE:  sealed class
TOKEN: 0x200014E
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600072D  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE7348  token=0x600072E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualInt16
TYPE:  sealed class
TOKEN: 0x200014F
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600072F  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE7120  token=0x6000730  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualChar
TYPE:  sealed class
TOKEN: 0x2000150
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000731  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE62D0  token=0x6000732  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualInt32
TYPE:  sealed class
TOKEN: 0x2000151
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000733  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE71D8  token=0x6000734  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualInt64
TYPE:  sealed class
TOKEN: 0x2000152
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000735  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE7290  token=0x6000736  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualByte
TYPE:  sealed class
TOKEN: 0x2000153
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000737  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE6218  token=0x6000738  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualUInt16
TYPE:  sealed class
TOKEN: 0x2000154
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000739  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE74BC  token=0x600073A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualUInt32
TYPE:  sealed class
TOKEN: 0x2000155
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600073B  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE7574  token=0x600073C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualUInt64
TYPE:  sealed class
TOKEN: 0x2000156
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600073D  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE762C  token=0x600073E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualSingle
TYPE:  sealed class
TOKEN: 0x2000157
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600073F  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE7400  token=0x6000740  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GreaterThanOrEqualDouble
TYPE:  sealed class
TOKEN: 0x2000158
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000741  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE6388  token=0x6000742  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: IncrementInt16
TYPE:  sealed class
TOKEN: 0x200015A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE7F54  token=0x6000748  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000749  System.Void .ctor()
END_CLASS

CLASS: IncrementInt32
TYPE:  sealed class
TOKEN: 0x200015B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE7FD8  token=0x600074A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600074B  System.Void .ctor()
END_CLASS

CLASS: IncrementInt64
TYPE:  sealed class
TOKEN: 0x200015C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE8050  token=0x600074C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600074D  System.Void .ctor()
END_CLASS

CLASS: IncrementUInt16
TYPE:  sealed class
TOKEN: 0x200015D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE8178  token=0x600074E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600074F  System.Void .ctor()
END_CLASS

CLASS: IncrementUInt32
TYPE:  sealed class
TOKEN: 0x200015E
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE81FC  token=0x6000750  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000751  System.Void .ctor()
END_CLASS

CLASS: IncrementUInt64
TYPE:  sealed class
TOKEN: 0x200015F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE828C  token=0x6000752  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000753  System.Void .ctor()
END_CLASS

CLASS: IncrementSingle
TYPE:  sealed class
TOKEN: 0x2000160
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE80E0  token=0x6000754  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000755  System.Void .ctor()
END_CLASS

CLASS: IncrementDouble
TYPE:  sealed class
TOKEN: 0x2000161
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.IncrementInstruction
FIELDS:
METHODS:
  RVA=0x0ACE7A80  token=0x6000756  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000757  System.Void .ctor()
END_CLASS

CLASS: DebugView
TYPE:  sealed class
TOKEN: 0x2000164
SIZE:  0x38
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.InstructionArray_array  // 0x10
METHODS:
  RVA=0x0ACE4A98  token=0x6000765  System.Void .ctor(System.Linq.Expressions.Interpreter.InstructionArray array)
  RVA=0x0ACE49AC  token=0x6000766  System.Linq.Expressions.Interpreter.InstructionList.DebugView.InstructionView[] GetInstructionViews(System.Boolean includeDebugCookies)
  RVA=0x0ACE4A68  token=0x6000767  System.Int32 <GetInstructionViews>b__4_0(System.Int32 index)
END_CLASS

CLASS: InstructionView
TYPE:  sealed struct
TOKEN: 0x2000167
SIZE:  0x30
FIELDS:
  private   readonly System.Int32                    _index  // 0x10
  private   readonly System.Int32                    _stackDepth  // 0x14
  private   readonly System.Int32                    _continuationsDepth  // 0x18
  private   readonly System.String                   _name  // 0x20
  private   readonly System.Linq.Expressions.Interpreter.Instruction_instruction  // 0x28
METHODS:
  RVA=0x02B2ECC0  token=0x60007DB  System.String GetValue()
  RVA=0x0ACEC234  token=0x60007DC  System.Void .ctor(System.Linq.Expressions.Interpreter.Instruction instruction, System.String name, System.Int32 index, System.Int32 stackDepth, System.Int32 continuationsDepth)
END_CLASS

CLASS: DebugView
TYPE:  sealed class
TOKEN: 0x2000166
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACE46AC  token=0x60007DA  System.Linq.Expressions.Interpreter.InstructionList.DebugView.InstructionView[] GetInstructionViews(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Interpreter.Instruction> instructions, System.Collections.Generic.IReadOnlyList<System.Object> objects, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>> debugCookies)
END_CLASS

CLASS: <GetStackTraceDebugInfo>d__29
TYPE:  sealed class
TOKEN: 0x2000169
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Linq.Expressions.Interpreter.InterpretedFrameInfo<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x28
  public            System.Linq.Expressions.Interpreter.InterpretedFrame<>4__this  // 0x30
  private           System.Linq.Expressions.Interpreter.InterpretedFrame<frame>5__2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Linq.Expressions.Interpreter.InterpretedFrameInfo>.Current  get=0x04D890C0
  System.Collections.IEnumerator.Current  get=0x0ACFD1CC
METHODS:
  RVA=0x06C07014  token=0x60007F8  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60007F9  System.Void System.IDisposable.Dispose()
  RVA=0x0ACFD014  token=0x60007FA  System.Boolean MoveNext()
  RVA=0x0ACFD180  token=0x60007FC  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0ACFD0EC  token=0x60007FE  System.Collections.Generic.IEnumerator<System.Linq.Expressions.Interpreter.InterpretedFrameInfo> System.Collections.Generic.IEnumerable<System.Linq.Expressions.Interpreter.InterpretedFrameInfo>.GetEnumerator()
  RVA=0x0ACFD178  token=0x60007FF  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200016C
SIZE:  0x10
FIELDS:
  public    static readonly System.Linq.Expressions.Interpreter.LabelInfo.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Linq.Expressions.Interpreter.LabelScopeInfo,System.Linq.Expressions.Interpreter.LabelScopeInfo><>9__9_0  // static @ 0x8
METHODS:
  RVA=0x0ACFD214  token=0x6000815  System.Void .cctor()
  RVA=0x041E1670  token=0x6000816  System.Void .ctor()
  RVA=0x09B24654  token=0x6000817  System.Linq.Expressions.Interpreter.LabelScopeInfo <ValidateJump>b__9_0(System.Linq.Expressions.Interpreter.LabelScopeInfo b)
END_CLASS

CLASS: LeftShiftSByte
TYPE:  sealed class
TOKEN: 0x2000170
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEE5D0  token=0x6000822  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000823  System.Void .ctor()
END_CLASS

CLASS: LeftShiftInt16
TYPE:  sealed class
TOKEN: 0x2000171
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEE38C  token=0x6000824  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000825  System.Void .ctor()
END_CLASS

CLASS: LeftShiftInt32
TYPE:  sealed class
TOKEN: 0x2000172
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEE450  token=0x6000826  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000827  System.Void .ctor()
END_CLASS

CLASS: LeftShiftInt64
TYPE:  sealed class
TOKEN: 0x2000173
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEE504  token=0x6000828  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000829  System.Void .ctor()
END_CLASS

CLASS: LeftShiftByte
TYPE:  sealed class
TOKEN: 0x2000174
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEDE90  token=0x600082A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600082B  System.Void .ctor()
END_CLASS

CLASS: LeftShiftUInt16
TYPE:  sealed class
TOKEN: 0x2000175
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEE690  token=0x600082C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600082D  System.Void .ctor()
END_CLASS

CLASS: LeftShiftUInt32
TYPE:  sealed class
TOKEN: 0x2000176
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEE754  token=0x600082E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x600082F  System.Void .ctor()
END_CLASS

CLASS: LeftShiftUInt64
TYPE:  sealed class
TOKEN: 0x2000177
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
FIELDS:
METHODS:
  RVA=0x0ACEE820  token=0x6000830  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000831  System.Void .ctor()
END_CLASS

CLASS: LessThanSByte
TYPE:  sealed class
TOKEN: 0x2000179
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000837  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0EE8  token=0x6000838  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanInt16
TYPE:  sealed class
TOKEN: 0x200017A
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000839  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEF7F4  token=0x600083A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanChar
TYPE:  sealed class
TOKEN: 0x200017B
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600083B  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEE9A4  token=0x600083C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanInt32
TYPE:  sealed class
TOKEN: 0x200017C
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600083D  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEF8AC  token=0x600083E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanInt64
TYPE:  sealed class
TOKEN: 0x200017D
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600083F  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEF964  token=0x6000840  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanByte
TYPE:  sealed class
TOKEN: 0x200017E
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000841  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEE8EC  token=0x6000842  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanUInt16
TYPE:  sealed class
TOKEN: 0x200017F
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000843  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF105C  token=0x6000844  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanUInt32
TYPE:  sealed class
TOKEN: 0x2000180
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000845  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF1114  token=0x6000846  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanUInt64
TYPE:  sealed class
TOKEN: 0x2000181
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000847  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF11CC  token=0x6000848  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanSingle
TYPE:  sealed class
TOKEN: 0x2000182
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000849  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0FA0  token=0x600084A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanDouble
TYPE:  sealed class
TOKEN: 0x2000183
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600084B  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEEA5C  token=0x600084C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualSByte
TYPE:  sealed class
TOKEN: 0x2000185
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000852  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0B4C  token=0x6000853  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualInt16
TYPE:  sealed class
TOKEN: 0x2000186
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000854  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0924  token=0x6000855  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualChar
TYPE:  sealed class
TOKEN: 0x2000187
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000856  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEFAD4  token=0x6000857  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualInt32
TYPE:  sealed class
TOKEN: 0x2000188
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000858  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF09DC  token=0x6000859  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualInt64
TYPE:  sealed class
TOKEN: 0x2000189
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600085A  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0A94  token=0x600085B  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualByte
TYPE:  sealed class
TOKEN: 0x200018A
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600085C  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEFA1C  token=0x600085D  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualUInt16
TYPE:  sealed class
TOKEN: 0x200018B
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x600085E  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0CC0  token=0x600085F  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualUInt32
TYPE:  sealed class
TOKEN: 0x200018C
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000860  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0D78  token=0x6000861  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualUInt64
TYPE:  sealed class
TOKEN: 0x200018D
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000862  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0E30  token=0x6000863  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualSingle
TYPE:  sealed class
TOKEN: 0x200018E
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000864  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACF0C04  token=0x6000865  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: LessThanOrEqualDouble
TYPE:  sealed class
TOKEN: 0x200018F
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000866  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEFB8C  token=0x6000867  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: DebugInfoComparer
TYPE:  class
TOKEN: 0x2000196
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
METHODS:
  RVA=0x0ACE4380  token=0x6000879  System.Int32 System.Collections.Generic.IComparer<System.Linq.Expressions.Interpreter.DebugInfo>.Compare(System.Linq.Expressions.Interpreter.DebugInfo d1, System.Linq.Expressions.Interpreter.DebugInfo d2)
  RVA=0x041E1670  token=0x600087A  System.Void .ctor()
END_CLASS

CLASS: QuoteVisitor
TYPE:  sealed class
TOKEN: 0x2000199
SIZE:  0x20
EXTENDS: System.Linq.Expressions.ExpressionVisitor
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Int32>_definedParameters  // 0x10
  public    readonly System.Collections.Generic.HashSet<System.Linq.Expressions.ParameterExpression>_hoistedParameters  // 0x18
METHODS:
  RVA=0x0AD10BD8  token=0x60008DA  System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node)
  RVA=0x0AD10A5C  token=0x60008DB  System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node)
  RVA=0x0AD10AC4  token=0x60008DC  System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node)
  RVA=-1  // generic def  token=0x60008DD  System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node)
  RVA=0x0AD108BC  token=0x60008DE  System.Void PushParameters(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=0x0AD10718  token=0x60008DF  System.Void PopParameters(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=0x0AD10C58  token=0x60008E0  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200019A
SIZE:  0x10
FIELDS:
  public    static readonly System.Linq.Expressions.Interpreter.LightCompiler.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Linq.Expressions.Expression,System.Boolean><>9__56_1  // static @ 0x8
  public    static  System.Func<System.Linq.Expressions.SwitchCase,System.Boolean><>9__56_0  // static @ 0x10
  public    static  System.Action<System.Linq.Expressions.Interpreter.LightCompiler,System.Linq.Expressions.Expression><>9__101_0  // static @ 0x18
METHODS:
  RVA=0x0AD147FC  token=0x60008E1  System.Void .cctor()
  RVA=0x041E1670  token=0x60008E2  System.Void .ctor()
  RVA=0x0AD14600  token=0x60008E3  System.Boolean <CompileSwitchExpression>b__56_0(System.Linq.Expressions.SwitchCase c)
  RVA=0x0AD14714  token=0x60008E4  System.Boolean <CompileSwitchExpression>b__56_1(System.Linq.Expressions.Expression t)
  RVA=0x0AD145D8  token=0x60008E5  System.Void <CompileNoLabelPush>b__101_0(System.Linq.Expressions.Interpreter.LightCompiler this, System.Linq.Expressions.Expression e)
END_CLASS

CLASS: DebugViewPrinter
TYPE:  class
TOKEN: 0x20001A3
SIZE:  0x38
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.Interpreter_interpreter  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Int32,System.Int32>_tryStart  // 0x18
  private   readonly System.Collections.Generic.Dictionary<System.Int32,System.String>_handlerEnter  // 0x20
  private   readonly System.Collections.Generic.Dictionary<System.Int32,System.Int32>_handlerExit  // 0x28
  private           System.String                   _indent  // 0x30
METHODS:
  RVA=0x0AD00D64  token=0x6000946  System.Void .ctor(System.Linq.Expressions.Interpreter.Interpreter interpreter)
  RVA=0x0AD0047C  token=0x6000947  System.Void Analyze()
  RVA=0x0AD003C8  token=0x6000948  System.Void AddTryStart(System.Int32 index)
  RVA=0x0AD00324  token=0x6000949  System.Void AddHandlerExit(System.Int32 index)
  RVA=0x0AD00808  token=0x600094A  System.Void Indent()
  RVA=0x0AD00708  token=0x600094B  System.Void Dedent()
  RVA=0x0AD00848  token=0x600094C  System.String ToString()
  RVA=0x0AD00748  token=0x600094D  System.Void EmitExits(System.Text.StringBuilder sb, System.Int32 index)
END_CLASS

CLASS: <>c__DisplayClass74_0
TYPE:  sealed class
TOKEN: 0x20001A4
SIZE:  0x20
FIELDS:
  public            System.Reflection.MethodInfo    targetMethod  // 0x10
  public            System.Type                     delegateType  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600094E  System.Void .ctor()
  RVA=0x0AD14774  token=0x600094F  System.Delegate <MakeRunDelegateCtor>b__0(System.Linq.Expressions.Interpreter.LightLambda lambda)
END_CLASS

CLASS: Reference
TYPE:  sealed class
TOKEN: 0x20001B2
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
IMPLEMENTS: System.Linq.Expressions.Interpreter.IBoxableInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0AD10DC4
METHODS:
  RVA=0x014F51F0  token=0x6000984  System.Void .ctor(System.Int32 index)
  RVA=0x0AD10D90  token=0x6000985  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD10D3C  token=0x6000986  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: ImmutableValue
TYPE:  sealed class
TOKEN: 0x20001B3
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
IMPLEMENTS: System.Linq.Expressions.Interpreter.IBoxableInstruction
FIELDS:
  private   readonly System.Object                   _defaultValue  // 0x18
PROPERTIES:
  InstructionName  get=0x0AD05A98
METHODS:
  RVA=0x05391D70  token=0x6000988  System.Void .ctor(System.Int32 index, System.Object defaultValue)
  RVA=0x0AD05A3C  token=0x6000989  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD059CC  token=0x600098A  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: ImmutableBox
TYPE:  sealed class
TOKEN: 0x20001B4
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
FIELDS:
  private   readonly System.Object                   _defaultValue  // 0x18
PROPERTIES:
  InstructionName  get=0x0AD058DC
METHODS:
  RVA=0x05391D70  token=0x600098C  System.Void .ctor(System.Int32 index, System.Object defaultValue)
  RVA=0x0AD05824  token=0x600098D  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: ImmutableRefBox
TYPE:  sealed class
TOKEN: 0x20001B5
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0AD059A0
METHODS:
  RVA=0x014F51F0  token=0x600098F  System.Void .ctor(System.Int32 index)
  RVA=0x0AD05908  token=0x6000990  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: ParameterBox
TYPE:  sealed class
TOKEN: 0x20001B6
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0AD101FC
METHODS:
  RVA=0x014F51F0  token=0x6000992  System.Void .ctor(System.Int32 index)
  RVA=0x0AD10134  token=0x6000993  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: Parameter
TYPE:  sealed class
TOKEN: 0x20001B7
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
IMPLEMENTS: System.Linq.Expressions.Interpreter.IBoxableInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0AD103A8
METHODS:
  RVA=0x014F51F0  token=0x6000995  System.Void .ctor(System.Int32 index)
  RVA=0x04D85B80  token=0x6000996  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD10354  token=0x6000997  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: MutableValue
TYPE:  sealed class
TOKEN: 0x20001B8
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
IMPLEMENTS: System.Linq.Expressions.Interpreter.IBoxableInstruction
FIELDS:
  private   readonly System.Type                     _type  // 0x18
PROPERTIES:
  InstructionName  get=0x0AD0BA6C
METHODS:
  RVA=0x05391D70  token=0x6000999  System.Void .ctor(System.Int32 index, System.Type type)
  RVA=0x0AD0B990  token=0x600099A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD0B920  token=0x600099B  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: MutableBox
TYPE:  sealed class
TOKEN: 0x20001B9
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
FIELDS:
  private   readonly System.Type                     _type  // 0x18
PROPERTIES:
  InstructionName  get=0x0AD0B8F4
METHODS:
  RVA=0x05391D70  token=0x600099D  System.Void .ctor(System.Int32 index, System.Type type)
  RVA=0x0AD0B7BC  token=0x600099E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: VariableScope
TYPE:  sealed class
TOKEN: 0x20001BE
SIZE:  0x30
FIELDS:
  public    readonly System.Int32                    Start  // 0x10
  public            System.Int32                    Stop  // 0x14
  public    readonly System.Linq.Expressions.Interpreter.LocalVariableVariable  // 0x18
  public    readonly System.Linq.Expressions.Interpreter.LocalVariables.VariableScopeParent  // 0x20
  public            System.Collections.Generic.List<System.Linq.Expressions.Interpreter.LocalVariables.VariableScope>ChildScopes  // 0x28
METHODS:
  RVA=0x0AD15614  token=0x60009B7  System.Void .ctor(System.Linq.Expressions.Interpreter.LocalVariable variable, System.Int32 start, System.Linq.Expressions.Interpreter.LocalVariables.VariableScope parent)
END_CLASS

CLASS: ModuloInt16
TYPE:  sealed class
TOKEN: 0x20001C0
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD09634  token=0x60009BD  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009BE  System.Void .ctor()
END_CLASS

CLASS: ModuloInt32
TYPE:  sealed class
TOKEN: 0x20001C1
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD09748  token=0x60009BF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009C0  System.Void .ctor()
END_CLASS

CLASS: ModuloInt64
TYPE:  sealed class
TOKEN: 0x20001C2
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD09850  token=0x60009C1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009C2  System.Void .ctor()
END_CLASS

CLASS: ModuloUInt16
TYPE:  sealed class
TOKEN: 0x20001C3
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD09A94  token=0x60009C3  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009C4  System.Void .ctor()
END_CLASS

CLASS: ModuloUInt32
TYPE:  sealed class
TOKEN: 0x20001C4
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD09BA8  token=0x60009C5  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009C6  System.Void .ctor()
END_CLASS

CLASS: ModuloUInt64
TYPE:  sealed class
TOKEN: 0x20001C5
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD09CBC  token=0x60009C7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009C8  System.Void .ctor()
END_CLASS

CLASS: ModuloSingle
TYPE:  sealed class
TOKEN: 0x20001C6
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD09968  token=0x60009C9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009CA  System.Void .ctor()
END_CLASS

CLASS: ModuloDouble
TYPE:  sealed class
TOKEN: 0x20001C7
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.ModuloInstruction
FIELDS:
METHODS:
  RVA=0x0AD090CC  token=0x60009CB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009CC  System.Void .ctor()
END_CLASS

CLASS: MulInt16
TYPE:  sealed class
TOKEN: 0x20001C9
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD0A330  token=0x60009D2  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009D3  System.Void .ctor()
END_CLASS

CLASS: MulInt32
TYPE:  sealed class
TOKEN: 0x20001CA
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD0A444  token=0x60009D4  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009D5  System.Void .ctor()
END_CLASS

CLASS: MulInt64
TYPE:  sealed class
TOKEN: 0x20001CB
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD0A548  token=0x60009D6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009D7  System.Void .ctor()
END_CLASS

CLASS: MulUInt16
TYPE:  sealed class
TOKEN: 0x20001CC
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD0B484  token=0x60009D8  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009D9  System.Void .ctor()
END_CLASS

CLASS: MulUInt32
TYPE:  sealed class
TOKEN: 0x20001CD
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD0B598  token=0x60009DA  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009DB  System.Void .ctor()
END_CLASS

CLASS: MulUInt64
TYPE:  sealed class
TOKEN: 0x20001CE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD0B6A8  token=0x60009DC  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009DD  System.Void .ctor()
END_CLASS

CLASS: MulSingle
TYPE:  sealed class
TOKEN: 0x20001CF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD0B364  token=0x60009DE  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009DF  System.Void .ctor()
END_CLASS

CLASS: MulDouble
TYPE:  sealed class
TOKEN: 0x20001D0
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulInstruction
FIELDS:
METHODS:
  RVA=0x0AD09DD4  token=0x60009E0  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009E1  System.Void .ctor()
END_CLASS

CLASS: MulOvfInt16
TYPE:  sealed class
TOKEN: 0x20001D2
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD0A9A4  token=0x60009E7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009E8  System.Void .ctor()
END_CLASS

CLASS: MulOvfInt32
TYPE:  sealed class
TOKEN: 0x20001D3
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD0AB9C  token=0x60009E9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009EA  System.Void .ctor()
END_CLASS

CLASS: MulOvfInt64
TYPE:  sealed class
TOKEN: 0x20001D4
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD0AD34  token=0x60009EB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009EC  System.Void .ctor()
END_CLASS

CLASS: MulOvfUInt16
TYPE:  sealed class
TOKEN: 0x20001D5
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD0AEA4  token=0x60009ED  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009EE  System.Void .ctor()
END_CLASS

CLASS: MulOvfUInt32
TYPE:  sealed class
TOKEN: 0x20001D6
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD0B084  token=0x60009EF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009F0  System.Void .ctor()
END_CLASS

CLASS: MulOvfUInt64
TYPE:  sealed class
TOKEN: 0x20001D7
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.MulOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD0B1F4  token=0x60009F1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009F2  System.Void .ctor()
END_CLASS

CLASS: NegateInt16
TYPE:  sealed class
TOKEN: 0x20001D9
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateInstruction
FIELDS:
METHODS:
  RVA=0x0AD0C31C  token=0x60009F8  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009F9  System.Void .ctor()
END_CLASS

CLASS: NegateInt32
TYPE:  sealed class
TOKEN: 0x20001DA
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateInstruction
FIELDS:
METHODS:
  RVA=0x0AD0C398  token=0x60009FA  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009FB  System.Void .ctor()
END_CLASS

CLASS: NegateInt64
TYPE:  sealed class
TOKEN: 0x20001DB
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateInstruction
FIELDS:
METHODS:
  RVA=0x0AD0C410  token=0x60009FC  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009FD  System.Void .ctor()
END_CLASS

CLASS: NegateSingle
TYPE:  sealed class
TOKEN: 0x20001DC
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateInstruction
FIELDS:
METHODS:
  RVA=0x0AD0C4A0  token=0x60009FE  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x60009FF  System.Void .ctor()
END_CLASS

CLASS: NegateDouble
TYPE:  sealed class
TOKEN: 0x20001DD
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateInstruction
FIELDS:
METHODS:
  RVA=0x0AD0BF84  token=0x6000A00  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A01  System.Void .ctor()
END_CLASS

CLASS: NegateCheckedInt32
TYPE:  sealed class
TOKEN: 0x20001DF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateCheckedInstruction
FIELDS:
METHODS:
  RVA=0x0AD0BDC0  token=0x6000A07  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A08  System.Void .ctor()
END_CLASS

CLASS: NegateCheckedInt16
TYPE:  sealed class
TOKEN: 0x20001E0
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateCheckedInstruction
FIELDS:
METHODS:
  RVA=0x0AD0BCA0  token=0x6000A09  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A0A  System.Void .ctor()
END_CLASS

CLASS: NegateCheckedInt64
TYPE:  sealed class
TOKEN: 0x20001E1
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NegateCheckedInstruction
FIELDS:
METHODS:
  RVA=0x0AD0BEA4  token=0x6000A0B  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A0C  System.Void .ctor()
END_CLASS

CLASS: NotEqualBoolean
TYPE:  sealed class
TOKEN: 0x20001E5
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0C97C  token=0x6000A1C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A1D  System.Void .ctor()
END_CLASS

CLASS: NotEqualSByte
TYPE:  sealed class
TOKEN: 0x20001E6
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DF40  token=0x6000A1E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A1F  System.Void .ctor()
END_CLASS

CLASS: NotEqualInt16
TYPE:  sealed class
TOKEN: 0x20001E7
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DAC8  token=0x6000A20  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A21  System.Void .ctor()
END_CLASS

CLASS: NotEqualChar
TYPE:  sealed class
TOKEN: 0x20001E8
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0CC3C  token=0x6000A22  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A23  System.Void .ctor()
END_CLASS

CLASS: NotEqualInt32
TYPE:  sealed class
TOKEN: 0x20001E9
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DC2C  token=0x6000A24  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A25  System.Void .ctor()
END_CLASS

CLASS: NotEqualInt64
TYPE:  sealed class
TOKEN: 0x20001EA
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DD8C  token=0x6000A26  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A27  System.Void .ctor()
END_CLASS

CLASS: NotEqualByte
TYPE:  sealed class
TOKEN: 0x20001EB
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0CADC  token=0x6000A28  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A29  System.Void .ctor()
END_CLASS

CLASS: NotEqualUInt16
TYPE:  sealed class
TOKEN: 0x20001EC
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0E22C  token=0x6000A2A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A2B  System.Void .ctor()
END_CLASS

CLASS: NotEqualUInt32
TYPE:  sealed class
TOKEN: 0x20001ED
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0E390  token=0x6000A2C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A2D  System.Void .ctor()
END_CLASS

CLASS: NotEqualUInt64
TYPE:  sealed class
TOKEN: 0x20001EE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0E4F0  token=0x6000A2E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A2F  System.Void .ctor()
END_CLASS

CLASS: NotEqualSingle
TYPE:  sealed class
TOKEN: 0x20001EF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0E0B4  token=0x6000A30  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A31  System.Void .ctor()
END_CLASS

CLASS: NotEqualDouble
TYPE:  sealed class
TOKEN: 0x20001F0
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0CDB4  token=0x6000A32  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A33  System.Void .ctor()
END_CLASS

CLASS: NotEqualReference
TYPE:  sealed class
TOKEN: 0x20001F1
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DE3C  token=0x6000A34  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A35  System.Void .ctor()
END_CLASS

CLASS: NotEqualSByteLiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F2
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DE8C  token=0x6000A36  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A37  System.Void .ctor()
END_CLASS

CLASS: NotEqualInt16LiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F3
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DA14  token=0x6000A38  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A39  System.Void .ctor()
END_CLASS

CLASS: NotEqualCharLiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F4
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0CB88  token=0x6000A3A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A3B  System.Void .ctor()
END_CLASS

CLASS: NotEqualInt32LiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F5
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DB78  token=0x6000A3C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A3D  System.Void .ctor()
END_CLASS

CLASS: NotEqualInt64LiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F6
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DCD8  token=0x6000A3E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A3F  System.Void .ctor()
END_CLASS

CLASS: NotEqualByteLiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F7
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0CA28  token=0x6000A40  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A41  System.Void .ctor()
END_CLASS

CLASS: NotEqualUInt16LiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F8
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0E178  token=0x6000A42  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A43  System.Void .ctor()
END_CLASS

CLASS: NotEqualUInt32LiftedToNull
TYPE:  sealed class
TOKEN: 0x20001F9
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0E2DC  token=0x6000A44  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A45  System.Void .ctor()
END_CLASS

CLASS: NotEqualUInt64LiftedToNull
TYPE:  sealed class
TOKEN: 0x20001FA
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0E43C  token=0x6000A46  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A47  System.Void .ctor()
END_CLASS

CLASS: NotEqualSingleLiftedToNull
TYPE:  sealed class
TOKEN: 0x20001FB
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0DFEC  token=0x6000A48  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A49  System.Void .ctor()
END_CLASS

CLASS: NotEqualDoubleLiftedToNull
TYPE:  sealed class
TOKEN: 0x20001FC
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotEqualInstruction
FIELDS:
METHODS:
  RVA=0x0AD0CCEC  token=0x6000A4A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A4B  System.Void .ctor()
END_CLASS

CLASS: NotBoolean
TYPE:  sealed class
TOKEN: 0x20001FE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0C888  token=0x6000A51  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A52  System.Void .ctor()
END_CLASS

CLASS: NotInt64
TYPE:  sealed class
TOKEN: 0x20001FF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0EB50  token=0x6000A53  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A54  System.Void .ctor()
END_CLASS

CLASS: NotInt32
TYPE:  sealed class
TOKEN: 0x2000200
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0EAD8  token=0x6000A55  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A56  System.Void .ctor()
END_CLASS

CLASS: NotInt16
TYPE:  sealed class
TOKEN: 0x2000201
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0EA5C  token=0x6000A57  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A58  System.Void .ctor()
END_CLASS

CLASS: NotUInt64
TYPE:  sealed class
TOKEN: 0x2000202
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0ED64  token=0x6000A59  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A5A  System.Void .ctor()
END_CLASS

CLASS: NotUInt32
TYPE:  sealed class
TOKEN: 0x2000203
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0ECD4  token=0x6000A5B  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A5C  System.Void .ctor()
END_CLASS

CLASS: NotUInt16
TYPE:  sealed class
TOKEN: 0x2000204
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0EC58  token=0x6000A5D  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A5E  System.Void .ctor()
END_CLASS

CLASS: NotByte
TYPE:  sealed class
TOKEN: 0x2000205
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0C904  token=0x6000A5F  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A60  System.Void .ctor()
END_CLASS

CLASS: NotSByte
TYPE:  sealed class
TOKEN: 0x2000206
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NotInstruction
FIELDS:
METHODS:
  RVA=0x0AD0EBE0  token=0x6000A61  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A62  System.Void .ctor()
END_CLASS

CLASS: Unchecked
TYPE:  sealed class
TOKEN: 0x2000209
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.NumericConvertInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0AD154B8
METHODS:
  RVA=0x04DA8680  token=0x6000A71  System.Void .ctor(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull)
  RVA=0x0AD15258  token=0x6000A72  System.Object Convert(System.Object obj)
  RVA=0x0AD14AE0  token=0x6000A73  System.Object ConvertInt32(System.Int32 obj)
  RVA=0x0AD14D70  token=0x6000A74  System.Object ConvertInt64(System.Int64 obj)
  RVA=0x0AD14FC0  token=0x6000A75  System.Object ConvertUInt64(System.UInt64 obj)
  RVA=0x0AD14860  token=0x6000A76  System.Object ConvertDouble(System.Double obj)
END_CLASS

CLASS: Checked
TYPE:  sealed class
TOKEN: 0x200020A
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.NumericConvertInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0ACFFE84
METHODS:
  RVA=0x04DA8680  token=0x6000A78  System.Void .ctor(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull)
  RVA=0x0ACFFC24  token=0x6000A79  System.Object Convert(System.Object obj)
  RVA=0x0ACFF1D8  token=0x6000A7A  System.Object ConvertInt32(System.Int32 obj)
  RVA=0x0ACFF51C  token=0x6000A7B  System.Object ConvertInt64(System.Int64 obj)
  RVA=0x0ACFF868  token=0x6000A7C  System.Object ConvertUInt64(System.UInt64 obj)
  RVA=0x0ACFEDE8  token=0x6000A7D  System.Object ConvertDouble(System.Double obj)
END_CLASS

CLASS: ToUnderlying
TYPE:  sealed class
TOKEN: 0x200020B
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.NumericConvertInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0AD14134
METHODS:
  RVA=0x04DAA000  token=0x6000A7F  System.Void .ctor(System.TypeCode to, System.Boolean isLiftedToNull)
  RVA=0x0AD13EAC  token=0x6000A80  System.Object Convert(System.Object obj)
END_CLASS

CLASS: OrSByte
TYPE:  sealed class
TOKEN: 0x200020D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0FE58  token=0x6000A86  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A87  System.Void .ctor()
END_CLASS

CLASS: OrInt16
TYPE:  sealed class
TOKEN: 0x200020E
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0FC38  token=0x6000A88  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A89  System.Void .ctor()
END_CLASS

CLASS: OrInt32
TYPE:  sealed class
TOKEN: 0x200020F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0FCE8  token=0x6000A8A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A8B  System.Void .ctor()
END_CLASS

CLASS: OrInt64
TYPE:  sealed class
TOKEN: 0x2000210
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0FD98  token=0x6000A8C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A8D  System.Void .ctor()
END_CLASS

CLASS: OrByte
TYPE:  sealed class
TOKEN: 0x2000211
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0F6CC  token=0x6000A8E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A8F  System.Void .ctor()
END_CLASS

CLASS: OrUInt16
TYPE:  sealed class
TOKEN: 0x2000212
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0FF08  token=0x6000A90  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A91  System.Void .ctor()
END_CLASS

CLASS: OrUInt32
TYPE:  sealed class
TOKEN: 0x2000213
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0FFB8  token=0x6000A92  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A93  System.Void .ctor()
END_CLASS

CLASS: OrUInt64
TYPE:  sealed class
TOKEN: 0x2000214
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD10074  token=0x6000A94  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A95  System.Void .ctor()
END_CLASS

CLASS: OrBoolean
TYPE:  sealed class
TOKEN: 0x2000215
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.OrInstruction
FIELDS:
METHODS:
  RVA=0x0AD0F5D4  token=0x6000A96  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A97  System.Void .ctor()
END_CLASS

CLASS: RightShiftSByte
TYPE:  sealed class
TOKEN: 0x2000217
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD1152C  token=0x6000A9D  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000A9E  System.Void .ctor()
END_CLASS

CLASS: RightShiftInt16
TYPE:  sealed class
TOKEN: 0x2000218
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD112EC  token=0x6000A9F  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AA0  System.Void .ctor()
END_CLASS

CLASS: RightShiftInt32
TYPE:  sealed class
TOKEN: 0x2000219
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD113AC  token=0x6000AA1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AA2  System.Void .ctor()
END_CLASS

CLASS: RightShiftInt64
TYPE:  sealed class
TOKEN: 0x200021A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD11460  token=0x6000AA3  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AA4  System.Void .ctor()
END_CLASS

CLASS: RightShiftByte
TYPE:  sealed class
TOKEN: 0x200021B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD10DF0  token=0x6000AA5  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AA6  System.Void .ctor()
END_CLASS

CLASS: RightShiftUInt16
TYPE:  sealed class
TOKEN: 0x200021C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD115EC  token=0x6000AA7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AA8  System.Void .ctor()
END_CLASS

CLASS: RightShiftUInt32
TYPE:  sealed class
TOKEN: 0x200021D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD116AC  token=0x6000AA9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AAA  System.Void .ctor()
END_CLASS

CLASS: RightShiftUInt64
TYPE:  sealed class
TOKEN: 0x200021E
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.RightShiftInstruction
FIELDS:
METHODS:
  RVA=0x0AD11778  token=0x6000AAB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AAC  System.Void .ctor()
END_CLASS

CLASS: SubInt16
TYPE:  sealed class
TOKEN: 0x2000225
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD12650  token=0x6000AC9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000ACA  System.Void .ctor()
END_CLASS

CLASS: SubInt32
TYPE:  sealed class
TOKEN: 0x2000226
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD12764  token=0x6000ACB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000ACC  System.Void .ctor()
END_CLASS

CLASS: SubInt64
TYPE:  sealed class
TOKEN: 0x2000227
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD12868  token=0x6000ACD  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000ACE  System.Void .ctor()
END_CLASS

CLASS: SubUInt16
TYPE:  sealed class
TOKEN: 0x2000228
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD137BC  token=0x6000ACF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AD0  System.Void .ctor()
END_CLASS

CLASS: SubUInt32
TYPE:  sealed class
TOKEN: 0x2000229
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD138D0  token=0x6000AD1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AD2  System.Void .ctor()
END_CLASS

CLASS: SubUInt64
TYPE:  sealed class
TOKEN: 0x200022A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD139E0  token=0x6000AD3  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AD4  System.Void .ctor()
END_CLASS

CLASS: SubSingle
TYPE:  sealed class
TOKEN: 0x200022B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD13698  token=0x6000AD5  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AD6  System.Void .ctor()
END_CLASS

CLASS: SubDouble
TYPE:  sealed class
TOKEN: 0x200022C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubInstruction
FIELDS:
METHODS:
  RVA=0x0AD120F0  token=0x6000AD7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AD8  System.Void .ctor()
END_CLASS

CLASS: SubOvfInt16
TYPE:  sealed class
TOKEN: 0x200022E
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD12CC4  token=0x6000ADE  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000ADF  System.Void .ctor()
END_CLASS

CLASS: SubOvfInt32
TYPE:  sealed class
TOKEN: 0x200022F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD12EBC  token=0x6000AE0  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AE1  System.Void .ctor()
END_CLASS

CLASS: SubOvfInt64
TYPE:  sealed class
TOKEN: 0x2000230
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD13054  token=0x6000AE2  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AE3  System.Void .ctor()
END_CLASS

CLASS: SubOvfUInt16
TYPE:  sealed class
TOKEN: 0x2000231
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD131C4  token=0x6000AE4  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AE5  System.Void .ctor()
END_CLASS

CLASS: SubOvfUInt32
TYPE:  sealed class
TOKEN: 0x2000232
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD133B8  token=0x6000AE6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AE7  System.Void .ctor()
END_CLASS

CLASS: SubOvfUInt64
TYPE:  sealed class
TOKEN: 0x2000233
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.SubOvfInstruction
FIELDS:
METHODS:
  RVA=0x0AD13528  token=0x6000AE8  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000AE9  System.Void .ctor()
END_CLASS

CLASS: HasValue
TYPE:  sealed class
TOKEN: 0x2000239
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
FIELDS:
METHODS:
  RVA=0x0AD057E8  token=0x6000B07  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000B08  System.Void .ctor()
END_CLASS

CLASS: GetValue
TYPE:  sealed class
TOKEN: 0x200023A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
FIELDS:
METHODS:
  RVA=0x0AD05784  token=0x6000B09  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000B0A  System.Void .ctor()
END_CLASS

CLASS: GetValueOrDefault
TYPE:  sealed class
TOKEN: 0x200023B
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
FIELDS:
  private   readonly System.Type                     _defaultValueType  // 0x10
METHODS:
  RVA=0x0AD05754  token=0x6000B0B  System.Void .ctor(System.Reflection.MethodInfo mi)
  RVA=0x0AD056F4  token=0x6000B0C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: GetValueOrDefault1
TYPE:  sealed class
TOKEN: 0x200023C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x02BBE540
METHODS:
  RVA=0x0AD056A0  token=0x6000B0E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000B0F  System.Void .ctor()
END_CLASS

CLASS: EqualsClass
TYPE:  sealed class
TOKEN: 0x200023D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x02BBE540
METHODS:
  RVA=0x0AD02D24  token=0x6000B11  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000B12  System.Void .ctor()
END_CLASS

CLASS: ToStringClass
TYPE:  sealed class
TOKEN: 0x200023E
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
FIELDS:
METHODS:
  RVA=0x0AD13E3C  token=0x6000B13  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000B14  System.Void .ctor()
END_CLASS

CLASS: GetHashCodeClass
TYPE:  sealed class
TOKEN: 0x200023F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
FIELDS:
METHODS:
  RVA=0x0AD05648  token=0x6000B15  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000B16  System.Void .ctor()
END_CLASS

CLASS: CastInstructionT`1
TYPE:  sealed class
TOKEN: 0x2000241
EXTENDS: System.Linq.Expressions.Interpreter.CastInstruction
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000B1C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=-1  // not resolved  token=0x6000B1D  System.Void .ctor()
END_CLASS

CLASS: Ref
TYPE:  sealed class
TOKEN: 0x2000243
SIZE:  0x18
EXTENDS: CastInstructionNoT
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000B22  System.Void .ctor(System.Type t)
  RVA=0x0AD10D18  token=0x6000B23  System.Void ConvertNull(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: Value
TYPE:  sealed class
TOKEN: 0x2000244
SIZE:  0x18
EXTENDS: CastInstructionNoT
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x6000B24  System.Void .ctor(System.Type t)
  RVA=0x0AD155C8  token=0x6000B25  System.Void ConvertNull(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: CastInstructionNoT
TYPE:  abstract class
TOKEN: 0x2000242
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.CastInstruction
FIELDS:
  private   readonly System.Type                     _t  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000B1E  System.Void .ctor(System.Type t)
  RVA=0x0ACFDFEC  token=0x6000B1F  System.Linq.Expressions.Interpreter.CastInstruction Create(System.Type t)
  RVA=0x0ACFE0B4  token=0x6000B20  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=-1  // abstract  token=0x6000B21  System.Void ConvertNull(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: ExpressionQuoter
TYPE:  sealed class
TOKEN: 0x2000248
SIZE:  0x28
EXTENDS: System.Linq.Expressions.ExpressionVisitor
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable>_variables  // 0x10
  private   readonly System.Linq.Expressions.Interpreter.InterpretedFrame_frame  // 0x18
  private   readonly System.Collections.Generic.Stack<System.Collections.Generic.HashSet<System.Linq.Expressions.ParameterExpression>>_shadowedVars  // 0x20
METHODS:
  RVA=0x0AD0542C  token=0x6000B2E  System.Void .ctor(System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> hoistedVariables, System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=-1  // generic def  token=0x6000B2F  System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node)
  RVA=0x0AD05080  token=0x6000B30  System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node)
  RVA=0x0AD051FC  token=0x6000B31  System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node)
  RVA=0x0AD0537C  token=0x6000B32  System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node)
  RVA=0x0AD04FA8  token=0x6000B33  System.Runtime.CompilerServices.IStrongBox GetBox(System.Linq.Expressions.ParameterExpression variable)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200024A
SIZE:  0x10
FIELDS:
  public    static readonly System.Linq.Expressions.Interpreter.DelegateHelpers.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Type,System.Boolean><>9__1_0  // static @ 0x8
METHODS:
  RVA=0x0AD14798  token=0x6000B35  System.Void .cctor()
  RVA=0x041E1670  token=0x6000B36  System.Void .ctor()
  RVA=0x0AD14754  token=0x6000B37  System.Boolean <MakeDelegate>b__1_0(System.Type t)
END_CLASS

CLASS: <GetEnumeratorWorker>d__7
TYPE:  sealed class
TOKEN: 0x200024E
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Collections.Generic.KeyValuePair<TKey,TValue><>2__current  // 0x0
  public            System.Linq.Expressions.Interpreter.HybridReferenceDictionary<TKey,TValue><>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B43  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000B44  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000B45  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000B47  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000252
FIELDS:
  public    static readonly System.Runtime.CompilerServices.CallSite.<>c<T><>9  // static @ 0x0
  public    static  System.Converter<System.Reflection.ParameterInfo,System.Linq.Expressions.ParameterExpression><>9__20_0  // static @ 0x0
  public    static  System.Converter<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Expression><>9__20_1  // static @ 0x0
  public    static  System.Converter<System.Reflection.ParameterInfo,System.Linq.Expressions.ParameterExpression><>9__21_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000B5D  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000B5E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000B5F  System.Linq.Expressions.ParameterExpression <CreateCustomUpdateDelegate>b__20_0(System.Reflection.ParameterInfo p)
  RVA=-1  // not resolved  token=0x6000B60  System.Linq.Expressions.Expression <CreateCustomUpdateDelegate>b__20_1(System.Linq.Expressions.ParameterExpression p)
  RVA=-1  // not resolved  token=0x6000B61  System.Linq.Expressions.ParameterExpression <CreateCustomNoMatchDelegate>b__21_0(System.Reflection.ParameterInfo p)
END_CLASS

CLASS: LambdaSignature`1
TYPE:  sealed class
TOKEN: 0x2000254
FIELDS:
  private   static  System.Runtime.CompilerServices.CallSiteBinder.LambdaSignature<T>s_instance  // static @ 0x0
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression>Parameters  // 0x0
  private   readonly System.Linq.Expressions.LabelTargetReturnLabel  // 0x0
PROPERTIES:
  Instance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B6C  System.Void .ctor()
END_CLASS

CLASS: Enumerator
TYPE:  class
TOKEN: 0x2000258
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private   readonly System.Runtime.CompilerServices.ReadOnlyCollectionBuilder<T>_builder  // 0x0
  private   readonly System.Int32                    _version  // 0x0
  private           System.Int32                    _index  // 0x0
  private           T                               _current  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B9A  System.Void .ctor(System.Runtime.CompilerServices.ReadOnlyCollectionBuilder<T> builder)
  RVA=-1  // not resolved  token=0x6000B9C  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000B9E  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000B9F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: AndNode
TYPE:  sealed struct
TOKEN: 0x2000260
SIZE:  0x20
FIELDS:
  private           System.Int32                    Depth  // 0x10
  private           System.Linq.Expressions.ExpressionNode  // 0x18
METHODS:
END_CLASS

CLASS: TestBuilder
TYPE:  sealed class
TOKEN: 0x200025F
SIZE:  0x20
FIELDS:
  private   readonly System.Collections.Generic.HashSet<System.Dynamic.BindingRestrictions>_unique  // 0x10
  private   readonly System.Collections.Generic.Stack<System.Dynamic.BindingRestrictions.TestBuilder.AndNode>_tests  // 0x18
METHODS:
  RVA=0x0AD13AF4  token=0x6000BB8  System.Void Append(System.Dynamic.BindingRestrictions restrictions)
  RVA=0x0AD13CB0  token=0x6000BB9  System.Linq.Expressions.Expression ToExpression()
  RVA=0x0AD13B70  token=0x6000BBA  System.Void Push(System.Linq.Expressions.Expression node, System.Int32 depth)
  RVA=0x0AD13D7C  token=0x6000BBB  System.Void .ctor()
END_CLASS

CLASS: MergedRestriction
TYPE:  sealed class
TOKEN: 0x2000261
SIZE:  0x20
EXTENDS: System.Dynamic.BindingRestrictions
FIELDS:
  private   readonly System.Dynamic.BindingRestrictionsLeft  // 0x10
  private   readonly System.Dynamic.BindingRestrictionsRight  // 0x18
METHODS:
  RVA=0x0AD09060  token=0x6000BBC  System.Void .ctor(System.Dynamic.BindingRestrictions left, System.Dynamic.BindingRestrictions right)
  RVA=0x0AD08F34  token=0x6000BBD  System.Linq.Expressions.Expression GetExpression()
END_CLASS

CLASS: CustomRestriction
TYPE:  sealed class
TOKEN: 0x2000262
SIZE:  0x18
EXTENDS: System.Dynamic.BindingRestrictions
FIELDS:
  private   readonly System.Linq.Expressions.Expression_expression  // 0x10
METHODS:
  RVA=0x0AD002CC  token=0x6000BBE  System.Void .ctor(System.Linq.Expressions.Expression expression)
  RVA=0x0AD00244  token=0x6000BBF  System.Boolean Equals(System.Object obj)
  RVA=0x0AD002A4  token=0x6000BC0  System.Int32 GetHashCode()
  RVA=0x02B2ECC0  token=0x6000BC1  System.Linq.Expressions.Expression GetExpression()
END_CLASS

CLASS: TypeRestriction
TYPE:  sealed class
TOKEN: 0x2000263
SIZE:  0x20
EXTENDS: System.Dynamic.BindingRestrictions
FIELDS:
  private   readonly System.Linq.Expressions.Expression_expression  // 0x10
  private   readonly System.Type                     _type  // 0x18
METHODS:
  RVA=0x0AD1456C  token=0x6000BC2  System.Void .ctor(System.Linq.Expressions.Expression parameter, System.Type type)
  RVA=0x0AD1442C  token=0x6000BC3  System.Boolean Equals(System.Object obj)
  RVA=0x0AD1451C  token=0x6000BC4  System.Int32 GetHashCode()
  RVA=0x0AD144C4  token=0x6000BC5  System.Linq.Expressions.Expression GetExpression()
END_CLASS

CLASS: InstanceRestriction
TYPE:  sealed class
TOKEN: 0x2000264
SIZE:  0x20
EXTENDS: System.Dynamic.BindingRestrictions
FIELDS:
  private   readonly System.Linq.Expressions.Expression_expression  // 0x10
  private   readonly System.Object                   _instance  // 0x18
METHODS:
  RVA=0x0AD06194  token=0x6000BC6  System.Void .ctor(System.Linq.Expressions.Expression parameter, System.Object instance)
  RVA=0x0AD05DF8  token=0x6000BC7  System.Boolean Equals(System.Object obj)
  RVA=0x0AD06158  token=0x6000BC8  System.Int32 GetHashCode()
  RVA=0x0AD05E6C  token=0x6000BC9  System.Linq.Expressions.Expression GetExpression()
END_CLASS

CLASS: BindingRestrictionsProxy
TYPE:  sealed class
TOKEN: 0x2000265
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: KeyCollectionDebugView
TYPE:  sealed class
TOKEN: 0x200026E
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <GetEnumerator>d__15
TYPE:  sealed class
TOKEN: 0x2000270
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.String                   <>2__current  // 0x18
  public            System.Dynamic.ExpandoObject.KeyCollection<>4__this  // 0x20
  private           System.Int32                    <i>5__2  // 0x28
  private           System.Int32                    <n>5__3  // 0x2c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.String>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000C2C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000C2D  System.Void System.IDisposable.Dispose()
  RVA=0x0AD1C360  token=0x6000C2E  System.Boolean MoveNext()
  RVA=0x0AD1C4EC  token=0x6000C30  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: KeyCollection
TYPE:  class
TOKEN: 0x200026F
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Dynamic.ExpandoObject    _expando  // 0x10
  private   readonly System.Int32                    _expandoVersion  // 0x18
  private   readonly System.Int32                    _expandoCount  // 0x1c
  private   readonly System.Dynamic.ExpandoObject.ExpandoData_expandoData  // 0x20
PROPERTIES:
  Count  get=0x0AD06864
  IsReadOnly  get=0x02FFF600
METHODS:
  RVA=0x0AD066E8  token=0x6000C21  System.Void .ctor(System.Dynamic.ExpandoObject expando)
  RVA=0x0AD06228  token=0x6000C22  System.Void CheckVersion()
  RVA=0x0AD06200  token=0x6000C23  System.Void Add(System.String item)
  RVA=0x0AD06288  token=0x6000C24  System.Void Clear()
  RVA=0x0AD062B0  token=0x6000C25  System.Boolean Contains(System.String item)
  RVA=0x0AD063BC  token=0x6000C26  System.Void CopyTo(System.String[] array, System.Int32 arrayIndex)
  RVA=0x0AD066B8  token=0x6000C29  System.Boolean Remove(System.String item)
  RVA=0x0AD0665C  token=0x6000C2A  System.Collections.Generic.IEnumerator<System.String> GetEnumerator()
  RVA=0x0AD066E0  token=0x6000C2B  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: ValueCollectionDebugView
TYPE:  sealed class
TOKEN: 0x2000271
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <GetEnumerator>d__15
TYPE:  sealed class
TOKEN: 0x2000273
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Dynamic.ExpandoObject.ValueCollection<>4__this  // 0x20
  private           System.Dynamic.ExpandoObject.ExpandoData<data>5__2  // 0x28
  private           System.Int32                    <i>5__3  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000C3D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000C3E  System.Void System.IDisposable.Dispose()
  RVA=0x0AD1C24C  token=0x6000C3F  System.Boolean MoveNext()
  RVA=0x0AD1C4A0  token=0x6000C41  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: ValueCollection
TYPE:  class
TOKEN: 0x2000272
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Dynamic.ExpandoObject    _expando  // 0x10
  private   readonly System.Int32                    _expandoVersion  // 0x18
  private   readonly System.Int32                    _expandoCount  // 0x1c
  private   readonly System.Dynamic.ExpandoObject.ExpandoData_expandoData  // 0x20
PROPERTIES:
  Count  get=0x0AD1CE74
  IsReadOnly  get=0x02FFF600
METHODS:
  RVA=0x0AD066E8  token=0x6000C32  System.Void .ctor(System.Dynamic.ExpandoObject expando)
  RVA=0x0AD1C95C  token=0x6000C33  System.Void CheckVersion()
  RVA=0x0AD1C934  token=0x6000C34  System.Void Add(System.Object item)
  RVA=0x0AD1C9BC  token=0x6000C35  System.Void Clear()
  RVA=0x0AD1C9E4  token=0x6000C36  System.Boolean Contains(System.Object item)
  RVA=0x0AD1CB60  token=0x6000C37  System.Void CopyTo(System.Object[] array, System.Int32 arrayIndex)
  RVA=0x0AD1CE44  token=0x6000C3A  System.Boolean Remove(System.Object item)
  RVA=0x0AD1CDE8  token=0x6000C3B  System.Collections.Generic.IEnumerator<System.Object> GetEnumerator()
  RVA=0x0AD1CE6C  token=0x6000C3C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000275
SIZE:  0x20
FIELDS:
  public            System.Dynamic.InvokeMemberBinderbinder  // 0x10
  public            System.Dynamic.DynamicMetaObject[]args  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000C4F  System.Void .ctor()
  RVA=0x0AD1C7AC  token=0x6000C50  System.Dynamic.DynamicMetaObject <BindInvokeMember>b__0(System.Dynamic.DynamicMetaObject value)
END_CLASS

CLASS: <GetDynamicMemberNames>d__6
TYPE:  sealed class
TOKEN: 0x2000276
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.String                   <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            System.Dynamic.ExpandoObject.MetaExpando<>4__this  // 0x28
  private           System.Dynamic.ExpandoObject.ExpandoData<expandoData>5__2  // 0x30
  private           System.Dynamic.ExpandoClass     <klass>5__3  // 0x38
  private           System.Int32                    <i>5__4  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.String>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000C51  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000C52  System.Void System.IDisposable.Dispose()
  RVA=0x0AD1C030  token=0x6000C53  System.Boolean MoveNext()
  RVA=0x0AD1C200  token=0x6000C55  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0AD1C16C  token=0x6000C57  System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator()
  RVA=0x0AD1C1F8  token=0x6000C58  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: MetaExpando
TYPE:  class
TOKEN: 0x2000274
SIZE:  0x28
EXTENDS: System.Dynamic.DynamicMetaObject
FIELDS:
PROPERTIES:
  Value  get=0x0AD185C8
METHODS:
  RVA=0x0AD1852C  token=0x6000C43  System.Void .ctor(System.Linq.Expressions.Expression expression, System.Dynamic.ExpandoObject value)
  RVA=0x0AD1788C  token=0x6000C44  System.Dynamic.DynamicMetaObject BindGetOrInvokeMember(System.Dynamic.DynamicMetaObjectBinder binder, System.String name, System.Boolean ignoreCase, System.Dynamic.DynamicMetaObject fallback, System.Func<System.Dynamic.DynamicMetaObject,System.Dynamic.DynamicMetaObject> fallbackInvoke)
  RVA=0x0AD177B4  token=0x6000C45  System.Dynamic.DynamicMetaObject BindGetMember(System.Dynamic.GetMemberBinder binder)
  RVA=0x0AD17D8C  token=0x6000C46  System.Dynamic.DynamicMetaObject BindInvokeMember(System.Dynamic.InvokeMemberBinder binder, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD17F14  token=0x6000C47  System.Dynamic.DynamicMetaObject BindSetMember(System.Dynamic.SetMemberBinder binder, System.Dynamic.DynamicMetaObject value)
  RVA=0x0AD174F0  token=0x6000C48  System.Dynamic.DynamicMetaObject BindDeleteMember(System.Dynamic.DeleteMemberBinder binder)
  RVA=0x0AD183AC  token=0x6000C49  System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames()
  RVA=0x0AD17274  token=0x6000C4A  System.Dynamic.DynamicMetaObject AddDynamicTestAndDefer(System.Dynamic.DynamicMetaObjectBinder binder, System.Dynamic.ExpandoClass klass, System.Dynamic.ExpandoClass originalClass, System.Dynamic.DynamicMetaObject succeeds)
  RVA=0x0AD182DC  token=0x6000C4B  System.Dynamic.ExpandoClass GetClassEnsureIndex(System.String name, System.Boolean caseInsensitive, System.Dynamic.ExpandoObject obj, System.Dynamic.ExpandoClass& klass, System.Int32& index)
  RVA=0x0AD1841C  token=0x6000C4C  System.Linq.Expressions.Expression GetLimitedSelf()
  RVA=0x0AD184E0  token=0x6000C4D  System.Dynamic.BindingRestrictions GetRestrictions()
END_CLASS

CLASS: ExpandoData
TYPE:  class
TOKEN: 0x2000277
SIZE:  0x28
FIELDS:
  private   static  System.Dynamic.ExpandoObject.ExpandoDataEmpty  // static @ 0x0
  private   readonly System.Dynamic.ExpandoClass     Class  // 0x10
  private   readonly System.Object[]                 _dataArray  // 0x18
  private           System.Int32                    _version  // 0x20
PROPERTIES:
  Item  get=0x0A7AAD6C  set=0x0AD15C90
  Version  get=0x02B70F90
  Length  get=0x053932FC
METHODS:
  RVA=0x0AD15C14  token=0x6000C5D  System.Void .ctor()
  RVA=0x047E1B40  token=0x6000C5E  System.Void .ctor(System.Dynamic.ExpandoClass klass, System.Object[] data, System.Int32 version)
  RVA=0x0AD159D8  token=0x6000C5F  System.Dynamic.ExpandoObject.ExpandoData UpdateClass(System.Dynamic.ExpandoClass newClass)
  RVA=0x04D8D030  token=0x6000C60  System.Int32 GetAlignedSize(System.Int32 len)
  RVA=0x0AD15BA4  token=0x6000C61  System.Void .cctor()
END_CLASS

CLASS: <GetExpandoEnumerator>d__51
TYPE:  sealed class
TOKEN: 0x2000278
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Collections.Generic.KeyValuePair<System.String,System.Object><>2__current  // 0x18
  public            System.Dynamic.ExpandoObject    <>4__this  // 0x28
  public            System.Int32                    version  // 0x30
  public            System.Dynamic.ExpandoObject.ExpandoDatadata  // 0x38
  private           System.Int32                    <i>5__2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Current  get=0x04D890C0
  System.Collections.IEnumerator.Current  get=0x0AD1C724
METHODS:
  RVA=0x014F51F0  token=0x6000C62  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000C63  System.Void System.IDisposable.Dispose()
  RVA=0x0AD1C538  token=0x6000C64  System.Boolean MoveNext()
  RVA=0x0AD1C6D8  token=0x6000C66  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Entry
TYPE:  sealed class
TOKEN: 0x2000282
FIELDS:
  private   readonly System.Int32                    _hash  // 0x0
  private   readonly TKey                            _key  // 0x0
  private   readonly TValue                          _value  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C90  System.Void .ctor(System.Int32 hash, TKey key, TValue value)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200028A
SIZE:  0x10
FIELDS:
  public    static readonly System.Dynamic.Utils.TypeUtils.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x0AD1C7E0  token=0x6000CDE  System.Void .cctor()
  RVA=0x041E1670  token=0x6000CDF  System.Void .ctor()
  RVA=0x0AD1C76C  token=0x6000CE0  System.Boolean <.cctor>b__44_0(System.Type i)
  RVA=0x0AD1C78C  token=0x6000CE1  System.Type <.cctor>b__44_1(System.Type i)
END_CLASS

CLASS: ElementCount
TYPE:  sealed struct
TOKEN: 0x2000295
FIELDS:
  private           System.Int32                    uniqueCount  // 0x0
  private           System.Int32                    unfoundCount  // 0x0
METHODS:
END_CLASS

CLASS: Slot
TYPE:  sealed struct
TOKEN: 0x2000296
FIELDS:
  private           System.Int32                    hashCode  // 0x0
  private           System.Int32                    next  // 0x0
  private           T                               value  // 0x0
METHODS:
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000297
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Collections.Generic.HashSet<T>_set  // 0x0
  private           System.Int32                    _index  // 0x0
  private           System.Int32                    _version  // 0x0
  private           T                               _current  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000D41  System.Void .ctor(System.Collections.Generic.HashSet<T> set)
  RVA=-1  // not resolved  token=0x6000D42  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000D43  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000D46  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: TimeoutTracker
TYPE:  sealed struct
TOKEN: 0x200029D
SIZE:  0x18
FIELDS:
  private           System.Int32                    m_total  // 0x10
  private           System.Int32                    m_start  // 0x14
PROPERTIES:
  RemainingMilliseconds  get=0x0AD192FC
  IsExpired  get=0x0AD192E4
METHODS:
  RVA=0x03E6C1D0  token=0x6000D79  System.Void .ctor(System.Int32 millisecondsTimeout)
END_CLASS

CLASS: __StaticArrayInitTypeSize=120
TYPE:  sealed struct
TOKEN: 0x200029F
SIZE:  0x88
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=256
TYPE:  sealed struct
TOKEN: 0x20002A0
SIZE:  0x110
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=1024
TYPE:  sealed struct
TOKEN: 0x20002A1
SIZE:  0x410
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x200029E
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10240270BFF41CB170C33C20788C368CB1B5A66B0FD0B98D638A827B783537583821  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=102409FDC69AA887AC8D36E0C8284C7B1D53E580E4880B72A67FF80D7E38317115D9  // static @ 0x400
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10240F4764044BF2E91EFEFAF3D4F1E81C10A2B7F0F45305489925CFC01198CF45B5  // static @ 0x800
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=102415A0E7628178B3FE6546E9B60F35E2B1C4864E020B16376866F38F72C74B45A9  // static @ 0xc00
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10244CF8DC711CD3210D673D86981355E2609233025ED22E4DC7A1F9B8E75ADE35F4  // static @ 0x1000
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024783FB0A44BE7670EB6EC170CF7F16BB2B6F23F4964B87B60C4EB506C7C6A286D  // static @ 0x1400
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=120914FCE8DC82DA59038745B264F743222527FBAE2E4A28E71C89760B7E3DBBA67  // static @ 0x1800
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=25693631B0726F6FE6629DAA743EE51B49F4477ED07391B68EEEA0672A4A90018AA  // static @ 0x1878
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=256C2D8E5EED6CBEBD8625FC18F81486A7733C04F9B0129FFBE974C68B90308B4F2  // static @ 0x1978
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024C40A9A77EE05E10AF16DA9D1781A2053159EA8570ADA03637BB5FE3D076C9F89  // static @ 0x1a78
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024E8058399671D13082C35B6B3CE7F9E282507EDD458CCFDD3BAD9F35D90016F62  // static @ 0x1e78
METHODS:
END_CLASS

CLASS: Microsoft.Win32.SafeHandles.SafeMemoryMappedFileHandle
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x20
EXTENDS: Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
FIELDS:
METHODS:
  RVA=0x039B85E0  token=0x6000006  System.Void .ctor(System.IntPtr preexistingHandle, System.Boolean ownsHandle)
  RVA=0x04D83740  token=0x6000007  System.Boolean ReleaseHandle()
END_CLASS

CLASS: Microsoft.Win32.SafeHandles.SafeMemoryMappedViewHandle
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x30
EXTENDS: System.Runtime.InteropServices.SafeBuffer
FIELDS:
  private           System.IntPtr                   mmap_handle  // 0x28
METHODS:
  RVA=0x0459B1E0  token=0x6000008  System.Void .ctor(System.IntPtr mmap_handle, System.IntPtr base_address, System.Int64 size)
  RVA=0x0ACCADB8  token=0x6000009  System.Void Flush()
  RVA=0x04D7AFD0  token=0x600000A  System.Boolean ReleaseHandle()
END_CLASS

CLASS: System.Action`9
TYPE:  sealed class
TOKEN: 0x2000005
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600000B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600000C  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
END_CLASS

CLASS: System.Action`10
TYPE:  sealed class
TOKEN: 0x2000006
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600000D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600000E  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
END_CLASS

CLASS: System.Action`11
TYPE:  sealed class
TOKEN: 0x2000007
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600000F  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000010  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
END_CLASS

CLASS: System.Action`12
TYPE:  sealed class
TOKEN: 0x2000008
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000011  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000012  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
END_CLASS

CLASS: System.Action`13
TYPE:  sealed class
TOKEN: 0x2000009
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000013  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000014  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
END_CLASS

CLASS: System.Action`14
TYPE:  sealed class
TOKEN: 0x200000A
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000015  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000016  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
END_CLASS

CLASS: System.Action`15
TYPE:  sealed class
TOKEN: 0x200000B
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000017  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000018  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
END_CLASS

CLASS: System.Action`16
TYPE:  sealed class
TOKEN: 0x200000C
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000019  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600001A  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
END_CLASS

CLASS: System.Func`10
TYPE:  sealed class
TOKEN: 0x200000D
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600001B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600001C  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
END_CLASS

CLASS: System.Func`11
TYPE:  sealed class
TOKEN: 0x200000E
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600001D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600001E  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
END_CLASS

CLASS: System.Func`12
TYPE:  sealed class
TOKEN: 0x200000F
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600001F  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000020  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
END_CLASS

CLASS: System.Func`13
TYPE:  sealed class
TOKEN: 0x2000010
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000021  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000022  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
END_CLASS

CLASS: System.Func`14
TYPE:  sealed class
TOKEN: 0x2000011
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000023  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000024  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
END_CLASS

CLASS: System.Func`15
TYPE:  sealed class
TOKEN: 0x2000012
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000025  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000026  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
END_CLASS

CLASS: System.Func`16
TYPE:  sealed class
TOKEN: 0x2000013
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000027  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000028  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
END_CLASS

CLASS: System.Func`17
TYPE:  sealed class
TOKEN: 0x2000014
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000029  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600002A  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
END_CLASS

CLASS: System.MonoUtil
TYPE:  static class
TOKEN: 0x2000015
SIZE:  0x10
FIELDS:
  public    static readonly System.Boolean                  IsUnix  // static @ 0x0
METHODS:
  RVA=0x04D193A0  token=0x600002B  System.Void .cctor()
END_CLASS

CLASS: System.Collections.Generic.ArrayBuilder`1
TYPE:  sealed struct
TOKEN: 0x2000292
FIELDS:
  private           T[]                             _array  // 0x0
  private           System.Int32                    _count  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000D01  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000D02  T[] ToArray()
  RVA=-1  // not resolved  token=0x6000D03  System.Void UncheckedAdd(T item)
END_CLASS

CLASS: System.Collections.Generic.BitHelper
TYPE:  sealed class
TOKEN: 0x2000293
SIZE:  0x30
FIELDS:
  private   readonly System.Int32                    _length  // 0x10
  private   readonly System.Int32*                   _arrayPtr  // 0x18
  private   readonly System.Int32[]                  _array  // 0x20
  private   readonly System.Boolean                  _useStackAlloc  // 0x28
METHODS:
  RVA=0x04DAA010  token=0x6000D04  System.Void .ctor(System.Int32* bitArrayPtr, System.Int32 length)
  RVA=0x0AD157D0  token=0x6000D05  System.Void .ctor(System.Int32[] bitArray, System.Int32 length)
  RVA=0x0AD1575C  token=0x6000D06  System.Void MarkBit(System.Int32 bitPosition)
  RVA=0x0AD156F4  token=0x6000D07  System.Boolean IsMarked(System.Int32 bitPosition)
  RVA=0x0AD157B8  token=0x6000D08  System.Int32 ToIntArrayLength(System.Int32 n)
END_CLASS

CLASS: System.Collections.Generic.HashSet`1
TYPE:  class
TOKEN: 0x2000294
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.ISet`1 System.Collections.Generic.IReadOnlyCollection`1 System.Runtime.Serialization.ISerializable System.Runtime.Serialization.IDeserializationCallback
FIELDS:
  private   static  System.Int32                    Lower31BitMask  // const
  private   static  System.Int32                    StackAllocThreshold  // const
  private   static  System.Int32                    ShrinkThreshold  // const
  private   static  System.String                   CapacityName  // const
  private   static  System.String                   ElementsName  // const
  private   static  System.String                   ComparerName  // const
  private   static  System.String                   VersionName  // const
  private           System.Int32[]                  _buckets  // 0x0
  private           System.Collections.Generic.HashSet.Slot<T>[]_slots  // 0x0
  private           System.Int32                    _count  // 0x0
  private           System.Int32                    _lastIndex  // 0x0
  private           System.Int32                    _freeList  // 0x0
  private           System.Collections.Generic.IEqualityComparer<T>_comparer  // 0x0
  private           System.Int32                    _version  // 0x0
  private           System.Runtime.Serialization.SerializationInfo_siInfo  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  System.Collections.Generic.ICollection<T>.IsReadOnly  get=-1  // not resolved
  Comparer  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000D09  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000D0A  System.Void .ctor(System.Collections.Generic.IEqualityComparer<T> comparer)
  RVA=-1  // not resolved  token=0x6000D0B  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000D0C  System.Void .ctor(System.Collections.Generic.IEnumerable<T> collection)
  RVA=-1  // not resolved  token=0x6000D0D  System.Void .ctor(System.Collections.Generic.IEnumerable<T> collection, System.Collections.Generic.IEqualityComparer<T> comparer)
  RVA=-1  // not resolved  token=0x6000D0E  System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=-1  // not resolved  token=0x6000D0F  System.Void CopyFrom(System.Collections.Generic.HashSet<T> source)
  RVA=-1  // not resolved  token=0x6000D10  System.Void .ctor(System.Int32 capacity, System.Collections.Generic.IEqualityComparer<T> comparer)
  RVA=-1  // not resolved  token=0x6000D11  System.Void System.Collections.Generic.ICollection<T>.Add(T item)
  RVA=-1  // not resolved  token=0x6000D12  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000D13  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000D14  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000D15  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x6000D18  System.Collections.Generic.HashSet.Enumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000D19  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000D1A  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000D1B  System.Void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=-1  // not resolved  token=0x6000D1C  System.Void OnDeserialization(System.Object sender)
  RVA=-1  // not resolved  token=0x6000D1D  System.Boolean Add(T item)
  RVA=-1  // not resolved  token=0x6000D1E  System.Boolean TryGetValue(T equalValue, T& actualValue)
  RVA=-1  // not resolved  token=0x6000D1F  System.Void UnionWith(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D20  System.Void IntersectWith(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D21  System.Void ExceptWith(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D22  System.Void SymmetricExceptWith(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D23  System.Boolean IsSubsetOf(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D24  System.Boolean IsProperSubsetOf(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D25  System.Boolean IsSupersetOf(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D26  System.Boolean IsProperSupersetOf(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D27  System.Boolean Overlaps(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D28  System.Boolean SetEquals(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D29  System.Void CopyTo(T[] array)
  RVA=-1  // not resolved  token=0x6000D2A  System.Void CopyTo(T[] array, System.Int32 arrayIndex, System.Int32 count)
  RVA=-1  // not resolved  token=0x6000D2B  System.Int32 RemoveWhere(System.Predicate<T> match)
  RVA=-1  // not resolved  token=0x6000D2D  System.Int32 EnsureCapacity(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000D2E  System.Void TrimExcess()
  RVA=-1  // not resolved  token=0x6000D2F  System.Collections.Generic.IEqualityComparer<System.Collections.Generic.HashSet<T>> CreateSetComparer()
  RVA=-1  // not resolved  token=0x6000D30  System.Int32 Initialize(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000D31  System.Void IncreaseCapacity()
  RVA=-1  // not resolved  token=0x6000D32  System.Void SetCapacity(System.Int32 newSize)
  RVA=-1  // not resolved  token=0x6000D33  System.Boolean AddIfNotPresent(T value)
  RVA=-1  // not resolved  token=0x6000D34  System.Void AddValue(System.Int32 index, System.Int32 hashCode, T value)
  RVA=-1  // not resolved  token=0x6000D35  System.Boolean ContainsAllElements(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D36  System.Boolean IsSubsetOfHashSetWithSameEC(System.Collections.Generic.HashSet<T> other)
  RVA=-1  // not resolved  token=0x6000D37  System.Void IntersectWithHashSetWithSameEC(System.Collections.Generic.HashSet<T> other)
  RVA=-1  // not resolved  token=0x6000D38  System.Void IntersectWithEnumerable(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D39  System.Int32 InternalIndexOf(T item)
  RVA=-1  // not resolved  token=0x6000D3A  System.Void SymmetricExceptWithUniqueHashSet(System.Collections.Generic.HashSet<T> other)
  RVA=-1  // not resolved  token=0x6000D3B  System.Void SymmetricExceptWithEnumerable(System.Collections.Generic.IEnumerable<T> other)
  RVA=-1  // not resolved  token=0x6000D3C  System.Boolean AddOrGetLocation(T value, System.Int32& location)
  RVA=-1  // not resolved  token=0x6000D3D  System.Collections.Generic.HashSet.ElementCount<T> CheckUniqueAndUnfoundElements(System.Collections.Generic.IEnumerable<T> other, System.Boolean returnIfUnfound)
  RVA=-1  // not resolved  token=0x6000D3E  System.Boolean HashSetEquals(System.Collections.Generic.HashSet<T> set1, System.Collections.Generic.HashSet<T> set2, System.Collections.Generic.IEqualityComparer<T> comparer)
  RVA=-1  // not resolved  token=0x6000D3F  System.Boolean AreEqualityComparersEqual(System.Collections.Generic.HashSet<T> set1, System.Collections.Generic.HashSet<T> set2)
  RVA=-1  // not resolved  token=0x6000D40  System.Int32 InternalGetHashCode(T item)
END_CLASS

CLASS: System.Collections.Generic.HashSetEqualityComparer`1
TYPE:  sealed class
TOKEN: 0x2000298
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<T>_comparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000D47  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000D48  System.Boolean Equals(System.Collections.Generic.HashSet<T> x, System.Collections.Generic.HashSet<T> y)
  RVA=-1  // not resolved  token=0x6000D49  System.Int32 GetHashCode(System.Collections.Generic.HashSet<T> obj)
  RVA=-1  // not resolved  token=0x6000D4A  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000D4B  System.Int32 GetHashCode()
END_CLASS

CLASS: System.Collections.Generic.ICollectionDebugView`1
TYPE:  sealed class
TOKEN: 0x2000299
FIELDS:
METHODS:
END_CLASS

CLASS: System.Dynamic.BinaryOperationBinder
TYPE:  abstract class
TOKEN: 0x200025D
SIZE:  0x20
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
  private   readonly System.Linq.Expressions.ExpressionType<Operation>k__BackingField  // 0x18
PROPERTIES:
  Operation  get=0x011EF5B0
METHODS:
  RVA=0x0ACFD7DC  token=0x6000BAD  System.Dynamic.DynamicMetaObject FallbackBinaryOperation(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject arg)
  RVA=-1  // abstract  token=0x6000BAE  System.Dynamic.DynamicMetaObject FallbackBinaryOperation(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject arg, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=0x0ACFD64C  token=0x6000BAF  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
END_CLASS

CLASS: System.Dynamic.BindingRestrictions
TYPE:  abstract class
TOKEN: 0x200025E
SIZE:  0x10
FIELDS:
  public    static readonly System.Dynamic.BindingRestrictionsEmpty  // static @ 0x0
METHODS:
  RVA=0x041E1670  token=0x6000BB0  System.Void .ctor()
  RVA=-1  // abstract  token=0x6000BB1  System.Linq.Expressions.Expression GetExpression()
  RVA=0x0ACFDA98  token=0x6000BB2  System.Dynamic.BindingRestrictions Merge(System.Dynamic.BindingRestrictions restrictions)
  RVA=0x0ACFD970  token=0x6000BB3  System.Dynamic.BindingRestrictions GetTypeRestriction(System.Linq.Expressions.Expression expression, System.Type type)
  RVA=0x0ACFD8C8  token=0x6000BB4  System.Dynamic.BindingRestrictions GetTypeRestriction(System.Dynamic.DynamicMetaObject obj)
  RVA=0x0ACFD7FC  token=0x6000BB5  System.Dynamic.BindingRestrictions GetInstanceRestriction(System.Linq.Expressions.Expression expression, System.Object instance)
  RVA=0x0ACFDBB0  token=0x6000BB6  System.Linq.Expressions.Expression ToExpression()
  RVA=0x0ACFDBC0  token=0x6000BB7  System.Void .cctor()
END_CLASS

CLASS: System.Dynamic.ConvertBinder
TYPE:  abstract class
TOKEN: 0x2000266
SIZE:  0x20
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x18
PROPERTIES:
  Type  get=0x0385B100
METHODS:
  RVA=0x0ACFFFA0  token=0x6000BCB  System.Dynamic.DynamicMetaObject FallbackConvert(System.Dynamic.DynamicMetaObject target)
  RVA=-1  // abstract  token=0x6000BCC  System.Dynamic.DynamicMetaObject FallbackConvert(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=0x0ACFFEB0  token=0x6000BCD  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
END_CLASS

CLASS: System.Dynamic.CreateInstanceBinder
TYPE:  abstract class
TOKEN: 0x2000267
SIZE:  0x18
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
METHODS:
  RVA=0x0AD0021C  token=0x6000BCE  System.Dynamic.DynamicMetaObject FallbackCreateInstance(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // abstract  token=0x6000BCF  System.Dynamic.DynamicMetaObject FallbackCreateInstance(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=0x0AD00130  token=0x6000BD0  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
END_CLASS

CLASS: System.Dynamic.DeleteIndexBinder
TYPE:  abstract class
TOKEN: 0x2000268
SIZE:  0x18
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
METHODS:
  RVA=0x0AD014FC  token=0x6000BD1  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD0021C  token=0x6000BD2  System.Dynamic.DynamicMetaObject FallbackDeleteIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes)
  RVA=-1  // abstract  token=0x6000BD3  System.Dynamic.DynamicMetaObject FallbackDeleteIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: System.Dynamic.DeleteMemberBinder
TYPE:  abstract class
TOKEN: 0x2000269
SIZE:  0x28
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
  private   readonly System.String                   <Name>k__BackingField  // 0x18
  private   readonly System.Boolean                  <IgnoreCase>k__BackingField  // 0x20
PROPERTIES:
  Name  get=0x0385B100
  IgnoreCase  get=0x04D866B0
METHODS:
  RVA=0x0ACFFFA0  token=0x6000BD6  System.Dynamic.DynamicMetaObject FallbackDeleteMember(System.Dynamic.DynamicMetaObject target)
  RVA=-1  // abstract  token=0x6000BD7  System.Dynamic.DynamicMetaObject FallbackDeleteMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=0x0AD015E8  token=0x6000BD8  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
END_CLASS

CLASS: System.Dynamic.DynamicMetaObject
TYPE:  class
TOKEN: 0x200026A
SIZE:  0x28
FIELDS:
  public    static readonly System.Dynamic.DynamicMetaObject[]EmptyMetaObjects  // static @ 0x0
  private   static readonly System.Object                   s_noValueSentinel  // static @ 0x8
  private   readonly System.Object                   _value  // 0x10
  private   readonly System.Linq.Expressions.Expression<Expression>k__BackingField  // 0x18
  private   readonly System.Dynamic.BindingRestrictions<Restrictions>k__BackingField  // 0x20
PROPERTIES:
  Expression  get=0x0385B100
  Restrictions  get=0x04D862C0
  Value  get=0x0AD02D00
  HasValue  get=0x0AD02C00
  RuntimeType  get=0x0AD02C8C
  LimitType  get=0x0AD02C50
METHODS:
  RVA=0x0AD02ABC  token=0x6000BD9  System.Void .ctor(System.Linq.Expressions.Expression expression, System.Dynamic.BindingRestrictions restrictions)
  RVA=0x0AD02A8C  token=0x6000BDA  System.Void .ctor(System.Linq.Expressions.Expression expression, System.Dynamic.BindingRestrictions restrictions, System.Object value)
  RVA=0x0AD02114  token=0x6000BE1  System.Dynamic.DynamicMetaObject BindConvert(System.Dynamic.ConvertBinder binder)
  RVA=0x0AD0243C  token=0x6000BE2  System.Dynamic.DynamicMetaObject BindGetMember(System.Dynamic.GetMemberBinder binder)
  RVA=0x0AD026E0  token=0x6000BE3  System.Dynamic.DynamicMetaObject BindSetMember(System.Dynamic.SetMemberBinder binder, System.Dynamic.DynamicMetaObject value)
  RVA=0x0AD022FC  token=0x6000BE4  System.Dynamic.DynamicMetaObject BindDeleteMember(System.Dynamic.DeleteMemberBinder binder)
  RVA=0x0AD02394  token=0x6000BE5  System.Dynamic.DynamicMetaObject BindGetIndex(System.Dynamic.GetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes)
  RVA=0x0AD02624  token=0x6000BE6  System.Dynamic.DynamicMetaObject BindSetIndex(System.Dynamic.SetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value)
  RVA=0x0AD02254  token=0x6000BE7  System.Dynamic.DynamicMetaObject BindDeleteIndex(System.Dynamic.DeleteIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes)
  RVA=0x0AD024D4  token=0x6000BE8  System.Dynamic.DynamicMetaObject BindInvokeMember(System.Dynamic.InvokeMemberBinder binder, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD0257C  token=0x6000BE9  System.Dynamic.DynamicMetaObject BindInvoke(System.Dynamic.InvokeBinder binder, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD021AC  token=0x6000BEA  System.Dynamic.DynamicMetaObject BindCreateInstance(System.Dynamic.CreateInstanceBinder binder, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD02788  token=0x6000BEB  System.Dynamic.DynamicMetaObject BindUnaryOperation(System.Dynamic.UnaryOperationBinder binder)
  RVA=0x0AD0206C  token=0x6000BEC  System.Dynamic.DynamicMetaObject BindBinaryOperation(System.Dynamic.BinaryOperationBinder binder, System.Dynamic.DynamicMetaObject arg)
  RVA=0x0AD029AC  token=0x6000BED  System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames()
  RVA=0x0AD02820  token=0x6000BEE  System.Dynamic.DynamicMetaObject Create(System.Object value, System.Linq.Expressions.Expression expression)
  RVA=0x0AD029DC  token=0x6000BEF  System.Void .cctor()
END_CLASS

CLASS: System.Dynamic.DynamicMetaObjectBinder
TYPE:  abstract class
TOKEN: 0x200026B
SIZE:  0x18
EXTENDS: System.Runtime.CompilerServices.CallSiteBinder
FIELDS:
PROPERTIES:
  ReturnType  get=0x0AD0201C
  IsStandardBinder  get=0x012081B0
METHODS:
  RVA=0x0AD01FDC  token=0x6000BF0  System.Void .ctor()
  RVA=0x0AD01790  token=0x6000BF2  System.Linq.Expressions.Expression Bind(System.Object[] args, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters, System.Linq.Expressions.LabelTarget returnLabel)
  RVA=0x0AD01E40  token=0x6000BF3  System.Dynamic.DynamicMetaObject[] CreateArgumentMetaObjects(System.Object[] args, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=-1  // abstract  token=0x6000BF4  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD01F6C  token=0x6000BF5  System.Linq.Expressions.Expression GetUpdateExpression(System.Type type)
END_CLASS

CLASS: System.Dynamic.ExpandoClass
TYPE:  class
TOKEN: 0x200026C
SIZE:  0x28
FIELDS:
  private   readonly System.String[]                 _keys  // 0x10
  private   readonly System.Int32                    _hashCode  // 0x18
  private           System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.WeakReference>>_transitions  // 0x20
  private   static readonly System.Dynamic.ExpandoClass     Empty  // static @ 0x0
PROPERTIES:
  Keys  get=0x02B2ECC0
METHODS:
  RVA=0x0AD03530  token=0x6000BF7  System.Void .ctor()
  RVA=0x0AD0351C  token=0x6000BF8  System.Void .ctor(System.String[] keys, System.Int32 hashCode)
  RVA=0x0AD02DF4  token=0x6000BF9  System.Dynamic.ExpandoClass FindNewClass(System.String newKey)
  RVA=0x0AD03168  token=0x6000BFA  System.Collections.Generic.List<System.WeakReference> GetTransitionList(System.Int32 hashCode)
  RVA=0x0AD03480  token=0x6000BFB  System.Int32 GetValueIndex(System.String name, System.Boolean caseInsensitive, System.Dynamic.ExpandoObject obj)
  RVA=0x0AD03414  token=0x6000BFC  System.Int32 GetValueIndexCaseSensitive(System.String name)
  RVA=0x0AD03298  token=0x6000BFD  System.Int32 GetValueIndexCaseInsensitive(System.String name, System.Dynamic.ExpandoObject obj)
  RVA=0x0AD034AC  token=0x6000BFF  System.Void .cctor()
END_CLASS

CLASS: System.Dynamic.ExpandoObject
TYPE:  sealed class
TOKEN: 0x200026D
SIZE:  0x30
IMPLEMENTS: System.Dynamic.IDynamicMetaObjectProvider System.Collections.Generic.IDictionary`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.ComponentModel.INotifyPropertyChanged
FIELDS:
  private   static readonly System.Reflection.MethodInfo    s_expandoTryGetValue  // static @ 0x0
  private   static readonly System.Reflection.MethodInfo    s_expandoTrySetValue  // static @ 0x8
  private   static readonly System.Reflection.MethodInfo    s_expandoTryDeleteValue  // static @ 0x10
  private   static readonly System.Reflection.MethodInfo    s_expandoPromoteClass  // static @ 0x18
  private   static readonly System.Reflection.MethodInfo    s_expandoCheckVersion  // static @ 0x20
  private   readonly System.Object                   LockObject  // 0x10
  private           System.Dynamic.ExpandoObject.ExpandoData_data  // 0x18
  private           System.Int32                    _count  // 0x20
  private   static readonly System.Object                   Uninitialized  // static @ 0x28
  private           System.ComponentModel.PropertyChangedEventHandler_propertyChanged  // 0x28
PROPERTIES:
  Class  get=0x0A8DA4D0
  System.Collections.Generic.IDictionary<System.String,System.Object>.Keys  get=0x0AD04124
  System.Collections.Generic.IDictionary<System.String,System.Object>.Values  get=0x0AD04184
  System.Collections.Generic.IDictionary<System.String,System.Object>.Item  get=0x0AD040D4  set=0x0AD041E4
  System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Count  get=0x02B70F90
  System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.IsReadOnly  get=0x012081B0
METHODS:
  RVA=0x0AD04F24  token=0x6000C00  System.Void .ctor()
  RVA=0x0AD04828  token=0x6000C01  System.Boolean TryGetValue(System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase, System.Object& value)
  RVA=0x0AD04950  token=0x6000C02  System.Void TrySetValue(System.Object indexClass, System.Int32 index, System.Object value, System.String name, System.Boolean ignoreCase, System.Boolean add)
  RVA=0x0AD04510  token=0x6000C03  System.Boolean TryDeleteValue(System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase, System.Object deleteValue)
  RVA=0x0AD03630  token=0x6000C04  System.Boolean IsDeletedMember(System.Int32 index)
  RVA=0x0AD036BC  token=0x6000C06  System.Dynamic.ExpandoObject.ExpandoData PromoteClassCore(System.Dynamic.ExpandoClass oldClass, System.Dynamic.ExpandoClass newClass)
  RVA=0x0AD03704  token=0x6000C07  System.Void PromoteClass(System.Object oldClass, System.Object newClass)
  RVA=0x0AD043E0  token=0x6000C08  System.Dynamic.DynamicMetaObject System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression parameter)
  RVA=0x0AD04450  token=0x6000C09  System.Void TryAddMember(System.String key, System.Object value)
  RVA=0x0AD047FC  token=0x6000C0A  System.Boolean TryGetValueForKey(System.String key, System.Object& value)
  RVA=0x0AD0357C  token=0x6000C0B  System.Boolean ExpandoContainsKey(System.String key)
  RVA=0x0AD03EF8  token=0x6000C10  System.Void System.Collections.Generic.IDictionary<System.String,System.Object>.Add(System.String key, System.Object value)
  RVA=0x0AD03F00  token=0x6000C11  System.Boolean System.Collections.Generic.IDictionary<System.String,System.Object>.ContainsKey(System.String key)
  RVA=0x0AD03FF4  token=0x6000C12  System.Boolean System.Collections.Generic.IDictionary<System.String,System.Object>.Remove(System.String key)
  RVA=0x0AD040CC  token=0x6000C13  System.Boolean System.Collections.Generic.IDictionary<System.String,System.Object>.TryGetValue(System.String key, System.Object& value)
  RVA=0x0AD03810  token=0x6000C16  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Add(System.Collections.Generic.KeyValuePair<System.String,System.Object> item)
  RVA=0x0AD03864  token=0x6000C17  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Clear()
  RVA=0x0AD03AD8  token=0x6000C18  System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Contains(System.Collections.Generic.KeyValuePair<System.String,System.Object> item)
  RVA=0x0AD03B70  token=0x6000C19  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.CopyTo(System.Collections.Generic.KeyValuePair<System.String,System.Object>[] array, System.Int32 arrayIndex)
  RVA=0x0AD03E90  token=0x6000C1A  System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.Remove(System.Collections.Generic.KeyValuePair<System.String,System.Object> item)
  RVA=0x0AD042A4  token=0x6000C1B  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Object>>.GetEnumerator()
  RVA=0x0AD042A4  token=0x6000C1C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0AD035AC  token=0x6000C1D  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>> GetExpandoEnumerator(System.Dynamic.ExpandoObject.ExpandoData data, System.Int32 version)
  RVA=0x0AD042C8  token=0x6000C1E  System.Void System.ComponentModel.INotifyPropertyChanged.add_PropertyChanged(System.ComponentModel.PropertyChangedEventHandler value)
  RVA=0x0AD04354  token=0x6000C1F  System.Void System.ComponentModel.INotifyPropertyChanged.remove_PropertyChanged(System.ComponentModel.PropertyChangedEventHandler value)
  RVA=0x0AD04CBC  token=0x6000C20  System.Void .cctor()
END_CLASS

CLASS: System.Dynamic.GetIndexBinder
TYPE:  abstract class
TOKEN: 0x2000279
SIZE:  0x18
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
METHODS:
  RVA=0x0AD1699C  token=0x6000C68  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD0021C  token=0x6000C69  System.Dynamic.DynamicMetaObject FallbackGetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes)
  RVA=-1  // abstract  token=0x6000C6A  System.Dynamic.DynamicMetaObject FallbackGetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: System.Dynamic.GetMemberBinder
TYPE:  abstract class
TOKEN: 0x200027A
SIZE:  0x28
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
  private   readonly System.String                   <Name>k__BackingField  // 0x18
  private   readonly System.Boolean                  <IgnoreCase>k__BackingField  // 0x20
PROPERTIES:
  ReturnType  get=0x0AD16C2C
  Name  get=0x0385B100
  IgnoreCase  get=0x04D866B0
  IsStandardBinder  get=0x02FFF600
METHODS:
  RVA=0x0AD16B78  token=0x6000C6B  System.Void .ctor(System.String name, System.Boolean ignoreCase)
  RVA=0x0ACFFFA0  token=0x6000C6F  System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target)
  RVA=-1  // abstract  token=0x6000C70  System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=0x0AD16A88  token=0x6000C71  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
END_CLASS

CLASS: System.Dynamic.IDynamicMetaObjectProvider
TYPE:  interface
TOKEN: 0x200027B
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000C73  System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
END_CLASS

CLASS: System.Dynamic.InvokeBinder
TYPE:  abstract class
TOKEN: 0x200027C
SIZE:  0x18
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
METHODS:
  RVA=0x0AD0021C  token=0x6000C74  System.Dynamic.DynamicMetaObject FallbackInvoke(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // abstract  token=0x6000C75  System.Dynamic.DynamicMetaObject FallbackInvoke(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=0x0AD16C7C  token=0x6000C76  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
END_CLASS

CLASS: System.Dynamic.InvokeMemberBinder
TYPE:  abstract class
TOKEN: 0x200027D
SIZE:  0x28
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
  private   readonly System.String                   <Name>k__BackingField  // 0x18
  private   readonly System.Boolean                  <IgnoreCase>k__BackingField  // 0x20
PROPERTIES:
  Name  get=0x0385B100
  IgnoreCase  get=0x04D866B0
METHODS:
  RVA=0x0AD16D68  token=0x6000C79  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD0021C  token=0x6000C7A  System.Dynamic.DynamicMetaObject FallbackInvokeMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // abstract  token=0x6000C7B  System.Dynamic.DynamicMetaObject FallbackInvokeMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=-1  // abstract  token=0x6000C7C  System.Dynamic.DynamicMetaObject FallbackInvoke(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: System.Dynamic.SetIndexBinder
TYPE:  abstract class
TOKEN: 0x200027E
SIZE:  0x18
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
METHODS:
  RVA=0x0AD18E3C  token=0x6000C7D  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0AD19028  token=0x6000C7E  System.Dynamic.DynamicMetaObject FallbackSetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value)
  RVA=-1  // abstract  token=0x6000C7F  System.Dynamic.DynamicMetaObject FallbackSetIndex(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: System.Dynamic.SetMemberBinder
TYPE:  abstract class
TOKEN: 0x200027F
SIZE:  0x28
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
  private   readonly System.String                   <Name>k__BackingField  // 0x18
  private   readonly System.Boolean                  <IgnoreCase>k__BackingField  // 0x20
PROPERTIES:
  ReturnType  get=0x0AD19294
  Name  get=0x0385B100
  IgnoreCase  get=0x04D866B0
  IsStandardBinder  get=0x02FFF600
METHODS:
  RVA=0x0AD191E0  token=0x6000C80  System.Void .ctor(System.String name, System.Boolean ignoreCase)
  RVA=0x0AD19050  token=0x6000C84  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
  RVA=0x0ACFD7DC  token=0x6000C86  System.Dynamic.DynamicMetaObject FallbackSetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject value)
  RVA=-1  // abstract  token=0x6000C87  System.Dynamic.DynamicMetaObject FallbackSetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject value, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: System.Dynamic.UnaryOperationBinder
TYPE:  abstract class
TOKEN: 0x2000280
SIZE:  0x18
EXTENDS: System.Dynamic.DynamicMetaObjectBinder
FIELDS:
METHODS:
  RVA=0x0ACFFFA0  token=0x6000C88  System.Dynamic.DynamicMetaObject FallbackUnaryOperation(System.Dynamic.DynamicMetaObject target)
  RVA=-1  // abstract  token=0x6000C89  System.Dynamic.DynamicMetaObject FallbackUnaryOperation(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion)
  RVA=0x0AD1C844  token=0x6000C8A  System.Dynamic.DynamicMetaObject Bind(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject[] args)
END_CLASS

CLASS: System.Dynamic.Utils.CacheDict`2
TYPE:  sealed class
TOKEN: 0x2000281
FIELDS:
  private   readonly System.Int32                    _mask  // 0x0
  private   readonly System.Dynamic.Utils.CacheDict.Entry<TKey,TValue>[]_entries  // 0x0
PROPERTIES:
  Item  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C8B  System.Void .ctor(System.Int32 size)
  RVA=-1  // not resolved  token=0x6000C8C  System.Int32 AlignSize(System.Int32 size)
  RVA=-1  // not resolved  token=0x6000C8D  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000C8E  System.Void Add(TKey key, TValue value)
END_CLASS

CLASS: System.Dynamic.Utils.CollectionExtensions
TYPE:  static class
TOKEN: 0x2000283
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000C91  System.Runtime.CompilerServices.TrueReadOnlyCollection<T> AddFirst(System.Collections.ObjectModel.ReadOnlyCollection<T> list, T item)
  RVA=-1  // generic def  token=0x6000C92  T[] AddLast(T[] array, T item)
  RVA=-1  // generic def  token=0x6000C93  T[] RemoveFirst(T[] array)
  RVA=-1  // generic def  token=0x6000C94  T[] RemoveLast(T[] array)
  RVA=-1  // generic def  token=0x6000C95  System.Collections.ObjectModel.ReadOnlyCollection<T> ToReadOnly(System.Collections.Generic.IEnumerable<T> enumerable)
END_CLASS

CLASS: System.Dynamic.Utils.ContractUtils
TYPE:  static class
TOKEN: 0x2000284
SIZE:  0x10
FIELDS:
PROPERTIES:
  Unreachable  get=0x0AD15974
METHODS:
  RVA=0x0AD15934  token=0x6000C97  System.Void Requires(System.Boolean precondition, System.String paramName)
  RVA=0x0AD158D8  token=0x6000C98  System.Void RequiresNotNull(System.Object value, System.String paramName)
  RVA=0x0AD15868  token=0x6000C99  System.Void RequiresNotNull(System.Object value, System.String paramName, System.Int32 index)
  RVA=-1  // generic def  token=0x6000C9A  System.Void RequiresNotNullItems(System.Collections.Generic.IList<T> array, System.String arrayName)
  RVA=0x0AD157F0  token=0x6000C9B  System.String GetParamName(System.String paramName, System.Int32 index)
  RVA=-1  // generic def  token=0x6000C9C  System.Void RequiresArrayRange(System.Collections.Generic.IList<T> array, System.Int32 offset, System.Int32 count, System.String offsetName, System.String countName)
END_CLASS

CLASS: System.Dynamic.Utils.EmptyReadOnlyCollection`1
TYPE:  static class
TOKEN: 0x2000285
FIELDS:
  public    static readonly System.Collections.ObjectModel.ReadOnlyCollection<T>Instance  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C9D  System.Void .cctor()
END_CLASS

CLASS: System.Dynamic.Utils.ExpressionUtils
TYPE:  static class
TOKEN: 0x2000286
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000C9E  System.Collections.ObjectModel.ReadOnlyCollection<T> ReturnReadOnly(System.Collections.Generic.IReadOnlyList<T>& collection)
  RVA=-1  // generic def  token=0x6000C9F  T ReturnObject(System.Object collectionOrT)
  RVA=0x0AD16140  token=0x6000CA0  System.Void ValidateArgumentTypes(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String methodParamName)
  RVA=0x0AD16068  token=0x6000CA1  System.Void ValidateArgumentCount(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Int32 count, System.Reflection.ParameterInfo[] pis)
  RVA=0x0AD163B0  token=0x6000CA2  System.Linq.Expressions.Expression ValidateOneArgument(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Linq.Expressions.Expression arguments, System.Reflection.ParameterInfo pi, System.String methodParamName, System.String argumentParamName, System.Int32 index)
  RVA=0x0AD15D4C  token=0x6000CA3  System.Void RequiresCanRead(System.Linq.Expressions.Expression expression, System.String paramName)
  RVA=0x0AD15D5C  token=0x6000CA4  System.Void RequiresCanRead(System.Linq.Expressions.Expression expression, System.String paramName, System.Int32 idx)
  RVA=0x0AD15F34  token=0x6000CA5  System.Boolean TryQuote(System.Type parameterType, System.Linq.Expressions.Expression& argument)
  RVA=0x0AD15CE0  token=0x6000CA6  System.Reflection.ParameterInfo[] GetParametersForValidation(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind)
  RVA=-1  // generic def  token=0x6000CA7  System.Boolean SameElements(System.Collections.Generic.IEnumerable<T>& replacement, System.Collections.Generic.IReadOnlyList<T> current)
  RVA=-1  // generic def  token=0x6000CA8  System.Boolean SameElementsInCollection(System.Collections.Generic.ICollection<T> replacement, System.Collections.Generic.IReadOnlyList<T> current)
  RVA=0x0AD16020  token=0x6000CA9  System.Void ValidateArgumentCount(System.Linq.Expressions.LambdaExpression lambda)
END_CLASS

CLASS: System.Dynamic.Utils.ExpressionVisitorUtils
TYPE:  static class
TOKEN: 0x2000287
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AD16714  token=0x6000CAA  System.Linq.Expressions.Expression[] VisitBlockExpressions(System.Linq.Expressions.ExpressionVisitor visitor, System.Linq.Expressions.BlockExpression block)
  RVA=0x0AD16850  token=0x6000CAB  System.Linq.Expressions.ParameterExpression[] VisitParameters(System.Linq.Expressions.ExpressionVisitor visitor, System.Linq.Expressions.IParameterProvider nodes, System.String callerName)
  RVA=0x0AD165E8  token=0x6000CAC  System.Linq.Expressions.Expression[] VisitArguments(System.Linq.Expressions.ExpressionVisitor visitor, System.Linq.Expressions.IArgumentProvider nodes)
END_CLASS

CLASS: System.Dynamic.Utils.TypeExtensions
TYPE:  static class
TOKEN: 0x2000288
SIZE:  0x10
FIELDS:
  private   static readonly System.Dynamic.Utils.CacheDict<System.Reflection.MethodBase,System.Reflection.ParameterInfo[]>s_paramInfoCache  // static @ 0x0
METHODS:
  RVA=0x0AD19334  token=0x6000CAD  System.Reflection.MethodInfo GetAnyStaticMethodValidated(System.Type type, System.String name, System.Type[] types)
  RVA=0x0AD19584  token=0x6000CAE  System.Boolean MatchesArgumentTypes(System.Reflection.MethodInfo mi, System.Type[] argTypes)
  RVA=0x0AD194B8  token=0x6000CAF  System.Type GetReturnType(System.Reflection.MethodBase mi)
  RVA=0x0AD19538  token=0x6000CB0  System.TypeCode GetTypeCode(System.Type type)
  RVA=0x0AD193CC  token=0x6000CB1  System.Reflection.ParameterInfo[] GetParametersCached(System.Reflection.MethodBase method)
  RVA=0x0AD19678  token=0x6000CB2  System.Void .cctor()
END_CLASS

CLASS: System.Dynamic.Utils.TypeUtils
TYPE:  static class
TOKEN: 0x2000289
SIZE:  0x10
FIELDS:
  private   static readonly System.Type[]                   s_arrayAssignableInterfaces  // static @ 0x0
METHODS:
  RVA=0x0AD19D64  token=0x6000CB3  System.Type GetNonNullableType(System.Type type)
  RVA=0x0AD19E1C  token=0x6000CB4  System.Type GetNullableType(System.Type type)
  RVA=0x0AD1B3E0  token=0x6000CB5  System.Boolean IsNullableType(System.Type type)
  RVA=0x0AD1B37C  token=0x6000CB6  System.Boolean IsNullableOrReferenceType(System.Type type)
  RVA=0x0AD1AA6C  token=0x6000CB7  System.Boolean IsBool(System.Type type)
  RVA=0x0AD1B4D8  token=0x6000CB8  System.Boolean IsNumeric(System.Type type)
  RVA=0x0AD1B054  token=0x6000CB9  System.Boolean IsInteger(System.Type type)
  RVA=0x0AD1A9DC  token=0x6000CBA  System.Boolean IsArithmetic(System.Type type)
  RVA=0x0AD1B5DC  token=0x6000CBB  System.Boolean IsUnsignedInt(System.Type type)
  RVA=0x0AD1AFBC  token=0x6000CBC  System.Boolean IsIntegerOrBool(System.Type type)
  RVA=0x0AD1B474  token=0x6000CBD  System.Boolean IsNumericOrBool(System.Type type)
  RVA=0x0AD1B680  token=0x6000CBE  System.Boolean IsValidInstanceType(System.Reflection.MemberInfo member, System.Type instanceType)
  RVA=0x0AD1A42C  token=0x6000CBF  System.Boolean HasIdentityPrimitiveOrNullableConversionTo(System.Type source, System.Type dest)
  RVA=0x0AD1A778  token=0x6000CC0  System.Boolean HasReferenceConversionTo(System.Type source, System.Type dest)
  RVA=0x0AD1B9B0  token=0x6000CC1  System.Boolean StrictHasReferenceConversionTo(System.Type source, System.Type dest, System.Boolean skipNonArray)
  RVA=0x0AD1A110  token=0x6000CC2  System.Boolean HasArrayToInterfaceConversion(System.Type source, System.Type dest)
  RVA=0x0AD1A5FC  token=0x6000CC3  System.Boolean HasInterfaceToArrayConversion(System.Type source, System.Type dest)
  RVA=0x0AD1ABAC  token=0x6000CC4  System.Boolean IsCovariant(System.Type t)
  RVA=0x0AD1AAF4  token=0x6000CC5  System.Boolean IsContravariant(System.Type t)
  RVA=0x0AD1B0E4  token=0x6000CC6  System.Boolean IsInvariant(System.Type t)
  RVA=0x0AD1ABD0  token=0x6000CC7  System.Boolean IsDelegate(System.Type t)
  RVA=0x0AD1B10C  token=0x6000CC8  System.Boolean IsLegalExplicitVariantDelegateConversion(System.Type source, System.Type dest)
  RVA=0x0AD1AB1C  token=0x6000CC9  System.Boolean IsConvertible(System.Type type)
  RVA=0x0AD1A918  token=0x6000CCA  System.Boolean HasReferenceEquality(System.Type left, System.Type right)
  RVA=0x0AD1A28C  token=0x6000CCB  System.Boolean HasBuiltInEqualityOperator(System.Type left, System.Type right)
  RVA=0x0AD1AEEC  token=0x6000CCC  System.Boolean IsImplicitlyConvertibleTo(System.Type source, System.Type destination)
  RVA=0x0AD19F20  token=0x6000CCD  System.Reflection.MethodInfo GetUserDefinedCoercionMethod(System.Type convertFrom, System.Type convertToType)
  RVA=0x0AD19804  token=0x6000CCE  System.Reflection.MethodInfo FindConversionOperator(System.Reflection.MethodInfo[] methods, System.Type typeFrom, System.Type typeTo)
  RVA=0x0AD1ADC8  token=0x6000CCF  System.Boolean IsImplicitNumericConversion(System.Type source, System.Type destination)
  RVA=0x0AD1AECC  token=0x6000CD0  System.Boolean IsImplicitReferenceConversion(System.Type source, System.Type destination)
  RVA=0x0AD1AC48  token=0x6000CD1  System.Boolean IsImplicitBoxingConversion(System.Type source, System.Type destination)
  RVA=0x0AD1AD44  token=0x6000CD2  System.Boolean IsImplicitNullableConversion(System.Type source, System.Type destination)
  RVA=0x0AD19984  token=0x6000CD3  System.Type FindGenericType(System.Type definition, System.Type type)
  RVA=0x0AD19BF8  token=0x6000CD4  System.Reflection.MethodInfo GetBooleanOperator(System.Type type, System.String name)
  RVA=0x0AD19DDC  token=0x6000CD5  System.Type GetNonRefType(System.Type type)
  RVA=0x0AD1970C  token=0x6000CD6  System.Boolean AreEquivalent(System.Type t1, System.Type t2)
  RVA=0x0AD19768  token=0x6000CD7  System.Boolean AreReferenceAssignable(System.Type dest, System.Type src)
  RVA=0x0AD1B568  token=0x6000CD8  System.Boolean IsSameOrSubclass(System.Type type, System.Type subType)
  RVA=0x0AD1BD20  token=0x6000CD9  System.Void ValidateType(System.Type type, System.String paramName)
  RVA=0x0AD1BC28  token=0x6000CDA  System.Void ValidateType(System.Type type, System.String paramName, System.Boolean allowByRef, System.Boolean allowPointer)
  RVA=0x0AD1BD7C  token=0x6000CDB  System.Boolean ValidateType(System.Type type, System.String paramName, System.Int32 index)
  RVA=0x0AD19D14  token=0x6000CDC  System.Reflection.MethodInfo GetInvokeMethod(System.Type delegateType)
  RVA=0x0AD1BE60  token=0x6000CDD  System.Void .cctor()
END_CLASS

CLASS: System.IO.HandleInheritability
TYPE:  sealed struct
TOKEN: 0x200028B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.IO.HandleInheritability  None  // const
  public    static  System.IO.HandleInheritability  Inheritable  // const
METHODS:
END_CLASS

CLASS: System.IO.MemoryMappedFiles.MemoryMappedFileAccess
TYPE:  sealed struct
TOKEN: 0x200028C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileAccessReadWrite  // const
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileAccessRead  // const
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileAccessWrite  // const
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileAccessCopyOnWrite  // const
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileAccessReadExecute  // const
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileAccessReadWriteExecute  // const
METHODS:
END_CLASS

CLASS: System.IO.MemoryMappedFiles.MemoryMappedFileOptions
TYPE:  sealed struct
TOKEN: 0x200028D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileOptionsNone  // const
  public    static  System.IO.MemoryMappedFiles.MemoryMappedFileOptionsDelayAllocatePages  // const
METHODS:
END_CLASS

CLASS: System.IO.MemoryMappedFiles.MemoryMappedViewAccessor
TYPE:  sealed class
TOKEN: 0x200028E
SIZE:  0x38
EXTENDS: System.IO.UnmanagedMemoryAccessor
FIELDS:
  private           System.IO.MemoryMappedFiles.MemoryMappedViewm_view  // 0x30
PROPERTIES:
  SafeMemoryMappedViewHandle  get=0x040F2060
METHODS:
  RVA=0x0459B2F0  token=0x6000CE2  System.Void .ctor(System.IO.MemoryMappedFiles.MemoryMappedView view)
  RVA=0x04CAFA50  token=0x6000CE4  System.Void Dispose(System.Boolean disposing)
  RVA=0x0AD171B4  token=0x6000CE5  System.Void Flush()
END_CLASS

CLASS: System.IO.MemoryMappedFiles.MemoryMapImpl
TYPE:  static class
TOKEN: 0x200028F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x05397BDC  token=0x6000CE6  System.IntPtr OpenHandleInternal(System.IntPtr handle, System.Char* mapName, System.Int32 mapName_length, System.Int64& capacity, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IO.MemoryMappedFiles.MemoryMappedFileOptions options, System.Int32& error)
  RVA=0x053982F0  token=0x6000CE7  System.Void CloseMapping(System.IntPtr handle)
  RVA=0x0AD171A8  token=0x6000CE8  System.Void Flush(System.IntPtr file_handle)
  RVA=0x05397BE4  token=0x6000CE9  System.Void ConfigureHandleInheritability(System.IntPtr handle, System.IO.HandleInheritability inheritability)
  RVA=0x053982C4  token=0x6000CEA  System.Boolean Unmap(System.IntPtr mmap_handle)
  RVA=0x05397918  token=0x6000CEB  System.Int32 MapInternal(System.IntPtr handle, System.Int64 offset, System.Int64& size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IntPtr& mmap_handle, System.IntPtr& base_address)
  RVA=0x0459B1B0  token=0x6000CEC  System.Void Map(System.IntPtr handle, System.Int64 offset, System.Int64& size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IntPtr& mmap_handle, System.IntPtr& base_address)
  RVA=0x0AD16E54  token=0x6000CED  System.Exception CreateException(System.Int32 error, System.String path)
  RVA=0x04776320  token=0x6000CEE  System.Int32 StringLength(System.String a)
  RVA=0x04776360  token=0x6000CEF  System.Void CheckString(System.String name, System.String value)
  RVA=0x04776270  token=0x6000CF0  System.IntPtr OpenHandle(System.IntPtr handle, System.String mapName, System.Int64& capacity, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IO.MemoryMappedFiles.MemoryMappedFileOptions options)
END_CLASS

CLASS: System.IO.MemoryMappedFiles.MemoryMappedFile
TYPE:  class
TOKEN: 0x2000290
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IO.FileStream            stream  // 0x10
  private           System.Boolean                  keepOpen  // 0x18
  private           Microsoft.Win32.SafeHandles.SafeMemoryMappedFileHandlehandle  // 0x20
METHODS:
  RVA=0x047760C0  token=0x6000CF1  System.IO.MemoryMappedFiles.MemoryMappedFile CreateFromFile(System.IO.FileStream fileStream, System.String mapName, System.Int64 capacity, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access, System.IO.HandleInheritability inheritability, System.Boolean leaveOpen)
  RVA=0x0459AFD0  token=0x6000CF2  System.IO.MemoryMappedFiles.MemoryMappedViewAccessor CreateViewAccessor(System.Int64 offset, System.Int64 size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access)
  RVA=0x041E1670  token=0x6000CF3  System.Void .ctor()
  RVA=0x05396F44  token=0x6000CF4  System.Void Dispose()
  RVA=0x040F0D90  token=0x6000CF5  System.Void Dispose(System.Boolean disposing)
  RVA=0x0459B380  token=0x6000CF6  System.IO.FileAccess GetFileAccess(System.IO.MemoryMappedFiles.MemoryMappedFileAccess access)
END_CLASS

CLASS: System.IO.MemoryMappedFiles.MemoryMappedView
TYPE:  class
TOKEN: 0x2000291
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Microsoft.Win32.SafeHandles.SafeMemoryMappedViewHandlem_viewHandle  // 0x10
  private           System.Int64                    m_pointerOffset  // 0x18
  private           System.Int64                    m_size  // 0x20
  private           System.IO.MemoryMappedFiles.MemoryMappedFileAccessm_access  // 0x28
PROPERTIES:
  ViewHandle  get=0x02B2ECC0
  PointerOffset  get=0x0385B100
  Size  get=0x04D862C0
  Access  get=0x011F2A90
  IsClosed  get=0x04CAFAE0
METHODS:
  RVA=0x0459B180  token=0x6000CF7  System.Void .ctor(Microsoft.Win32.SafeHandles.SafeMemoryMappedViewHandle viewHandle, System.Int64 pointerOffset, System.Int64 size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access)
  RVA=0x0459B080  token=0x6000CFC  System.IO.MemoryMappedFiles.MemoryMappedView Create(System.IntPtr handle, System.Int64 offset, System.Int64 size, System.IO.MemoryMappedFiles.MemoryMappedFileAccess access)
  RVA=0x0AD1724C  token=0x6000CFD  System.Void Flush(System.IntPtr capacity)
  RVA=0x04D14B60  token=0x6000CFE  System.Void Dispose(System.Boolean disposing)
  RVA=0x04D14B00  token=0x6000CFF  System.Void Dispose()
END_CLASS

CLASS: System.Linq.Error
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACB4C44  token=0x600005F  System.Exception ArgumentNull(System.String s)
  RVA=0x0ACB4CA4  token=0x6000060  System.Exception ArgumentOutOfRange(System.String s)
  RVA=0x0ACB4D04  token=0x6000061  System.Exception MoreThanOneElement()
  RVA=0x0ACB4D68  token=0x6000062  System.Exception MoreThanOneMatch()
  RVA=0x0ACB4DCC  token=0x6000063  System.Exception NoElements()
  RVA=0x0ACB4E30  token=0x6000064  System.Exception NoMatch()
  RVA=0x0ACB4E94  token=0x6000065  System.Exception NotSupported()
END_CLASS

CLASS: System.Linq.Enumerable
TYPE:  static class
TOKEN: 0x200001C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000066  System.Collections.Generic.IEnumerable<TSource> Where(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6000067  System.Collections.Generic.IEnumerable<TResult> Select(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TResult> selector)
  RVA=-1  // generic def  token=0x6000068  System.Func<TSource,System.Boolean> CombinePredicates(System.Func<TSource,System.Boolean> predicate1, System.Func<TSource,System.Boolean> predicate2)
  RVA=-1  // generic def  token=0x6000069  System.Func<TSource,TResult> CombineSelectors(System.Func<TSource,TMiddle> selector1, System.Func<TMiddle,TResult> selector2)
  RVA=-1  // generic def  token=0x600006A  System.Collections.Generic.IEnumerable<TResult> SelectMany(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> selector)
  RVA=-1  // generic def  token=0x600006B  System.Collections.Generic.IEnumerable<TResult> SelectManyIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TResult>> selector)
  RVA=-1  // generic def  token=0x600006C  System.Collections.Generic.IEnumerable<TResult> SelectMany(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>> collectionSelector, System.Func<TSource,TCollection,TResult> resultSelector)
  RVA=-1  // generic def  token=0x600006D  System.Collections.Generic.IEnumerable<TResult> SelectManyIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Collections.Generic.IEnumerable<TCollection>> collectionSelector, System.Func<TSource,TCollection,TResult> resultSelector)
  RVA=-1  // generic def  token=0x600006E  System.Collections.Generic.IEnumerable<TSource> Skip(System.Collections.Generic.IEnumerable<TSource> source, System.Int32 count)
  RVA=-1  // generic def  token=0x600006F  System.Collections.Generic.IEnumerable<TSource> SkipIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Int32 count)
  RVA=-1  // generic def  token=0x6000070  System.Collections.Generic.IEnumerable<TResult> Join(System.Collections.Generic.IEnumerable<TOuter> outer, System.Collections.Generic.IEnumerable<TInner> inner, System.Func<TOuter,TKey> outerKeySelector, System.Func<TInner,TKey> innerKeySelector, System.Func<TOuter,TInner,TResult> resultSelector)
  RVA=-1  // generic def  token=0x6000071  System.Collections.Generic.IEnumerable<TResult> JoinIterator(System.Collections.Generic.IEnumerable<TOuter> outer, System.Collections.Generic.IEnumerable<TInner> inner, System.Func<TOuter,TKey> outerKeySelector, System.Func<TInner,TKey> innerKeySelector, System.Func<TOuter,TInner,TResult> resultSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
  RVA=-1  // generic def  token=0x6000072  System.Linq.IOrderedEnumerable<TSource> OrderBy(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector)
  RVA=-1  // generic def  token=0x6000073  System.Linq.IOrderedEnumerable<TSource> OrderByDescending(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector)
  RVA=-1  // generic def  token=0x6000074  System.Linq.IOrderedEnumerable<TSource> ThenBy(System.Linq.IOrderedEnumerable<TSource> source, System.Func<TSource,TKey> keySelector)
  RVA=-1  // generic def  token=0x6000075  System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey,TSource>> GroupBy(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector)
  RVA=-1  // generic def  token=0x6000076  System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey,TElement>> GroupBy(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector)
  RVA=-1  // generic def  token=0x6000077  System.Collections.Generic.IEnumerable<TSource> Concat(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second)
  RVA=-1  // generic def  token=0x6000078  System.Collections.Generic.IEnumerable<TSource> ConcatIterator(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second)
  RVA=-1  // generic def  token=0x6000079  System.Collections.Generic.IEnumerable<TSource> Prepend(System.Collections.Generic.IEnumerable<TSource> source, TSource element)
  RVA=-1  // generic def  token=0x600007A  System.Collections.Generic.IEnumerable<TSource> PrependIterator(System.Collections.Generic.IEnumerable<TSource> source, TSource element)
  RVA=-1  // generic def  token=0x600007B  System.Collections.Generic.IEnumerable<TSource> Distinct(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x600007C  System.Collections.Generic.IEnumerable<TSource> Distinct(System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEqualityComparer<TSource> comparer)
  RVA=-1  // generic def  token=0x600007D  System.Collections.Generic.IEnumerable<TSource> DistinctIterator(System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEqualityComparer<TSource> comparer)
  RVA=-1  // generic def  token=0x600007E  System.Collections.Generic.IEnumerable<TSource> Union(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second)
  RVA=-1  // generic def  token=0x600007F  System.Collections.Generic.IEnumerable<TSource> UnionIterator(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second, System.Collections.Generic.IEqualityComparer<TSource> comparer)
  RVA=-1  // generic def  token=0x6000080  System.Collections.Generic.IEnumerable<TSource> Intersect(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second)
  RVA=-1  // generic def  token=0x6000081  System.Collections.Generic.IEnumerable<TSource> IntersectIterator(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second, System.Collections.Generic.IEqualityComparer<TSource> comparer)
  RVA=-1  // generic def  token=0x6000082  System.Collections.Generic.IEnumerable<TSource> Reverse(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000083  System.Collections.Generic.IEnumerable<TSource> ReverseIterator(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000084  System.Boolean SequenceEqual(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second)
  RVA=-1  // generic def  token=0x6000085  System.Boolean SequenceEqual(System.Collections.Generic.IEnumerable<TSource> first, System.Collections.Generic.IEnumerable<TSource> second, System.Collections.Generic.IEqualityComparer<TSource> comparer)
  RVA=-1  // generic def  token=0x6000086  System.Collections.Generic.IEnumerable<TSource> AsEnumerable(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000087  TSource[] ToArray(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000088  System.Collections.Generic.List<TSource> ToList(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000089  System.Collections.Generic.Dictionary<TKey,TSource> ToDictionary(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector)
  RVA=-1  // generic def  token=0x600008A  System.Collections.Generic.Dictionary<TKey,TElement> ToDictionary(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector)
  RVA=-1  // generic def  token=0x600008B  System.Collections.Generic.Dictionary<TKey,TElement> ToDictionary(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
  RVA=-1  // generic def  token=0x600008C  System.Collections.Generic.IEnumerable<TResult> OfType(System.Collections.IEnumerable source)
  RVA=-1  // generic def  token=0x600008D  System.Collections.Generic.IEnumerable<TResult> OfTypeIterator(System.Collections.IEnumerable source)
  RVA=-1  // generic def  token=0x600008E  System.Collections.Generic.IEnumerable<TResult> Cast(System.Collections.IEnumerable source)
  RVA=-1  // generic def  token=0x600008F  System.Collections.Generic.IEnumerable<TResult> CastIterator(System.Collections.IEnumerable source)
  RVA=-1  // generic def  token=0x6000090  TSource First(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000091  TSource First(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6000092  TSource FirstOrDefault(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000093  TSource FirstOrDefault(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6000094  TSource Last(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000095  TSource LastOrDefault(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000096  TSource LastOrDefault(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6000097  TSource Single(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x6000098  TSource Single(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6000099  TSource SingleOrDefault(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x600009A  TSource SingleOrDefault(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x600009B  TSource ElementAt(System.Collections.Generic.IEnumerable<TSource> source, System.Int32 index)
  RVA=0x02EDAEE0  token=0x600009C  System.Collections.Generic.IEnumerable<System.Int32> Range(System.Int32 start, System.Int32 count)
  RVA=0x02EDADA0  token=0x600009D  System.Collections.Generic.IEnumerable<System.Int32> RangeIterator(System.Int32 start, System.Int32 count)
  RVA=-1  // generic def  token=0x600009E  System.Collections.Generic.IEnumerable<TResult> Repeat(TResult element, System.Int32 count)
  RVA=-1  // generic def  token=0x600009F  System.Collections.Generic.IEnumerable<TResult> RepeatIterator(TResult element, System.Int32 count)
  RVA=-1  // generic def  token=0x60000A0  System.Collections.Generic.IEnumerable<TResult> Empty()
  RVA=-1  // generic def  token=0x60000A1  System.Boolean Any(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x60000A2  System.Boolean Any(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x60000A3  System.Boolean All(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x60000A4  System.Int32 Count(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x60000A5  System.Int32 Count(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x60000A6  System.Boolean Contains(System.Collections.Generic.IEnumerable<TSource> source, TSource value)
  RVA=-1  // generic def  token=0x60000A7  System.Boolean Contains(System.Collections.Generic.IEnumerable<TSource> source, TSource value, System.Collections.Generic.IEqualityComparer<TSource> comparer)
  RVA=-1  // generic def  token=0x60000A8  TSource Aggregate(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TSource,TSource> func)
  RVA=-1  // generic def  token=0x60000A9  TAccumulate Aggregate(System.Collections.Generic.IEnumerable<TSource> source, TAccumulate seed, System.Func<TAccumulate,TSource,TAccumulate> func)
  RVA=0x0ACB4A9C  token=0x60000AA  System.Int32 Min(System.Collections.Generic.IEnumerable<System.Int32> source)
  RVA=0x0ACB48F4  token=0x60000AB  System.Int32 Max(System.Collections.Generic.IEnumerable<System.Int32> source)
  RVA=-1  // generic def  token=0x60000AC  System.Collections.Generic.HashSet<TSource> ToHashSet(System.Collections.Generic.IEnumerable<TSource> source)
  RVA=-1  // generic def  token=0x60000AD  System.Collections.Generic.HashSet<TSource> ToHashSet(System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEqualityComparer<TSource> comparer)
END_CLASS

CLASS: System.Linq.EmptyEnumerable`1
TYPE:  class
TOKEN: 0x2000034
FIELDS:
  public    static readonly TElement[]                      Instance  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600015C  System.Void .cctor()
END_CLASS

CLASS: System.Linq.IdentityFunction`1
TYPE:  class
TOKEN: 0x2000035
FIELDS:
PROPERTIES:
  Instance  get=-1  // not resolved
METHODS:
END_CLASS

CLASS: System.Linq.IOrderedEnumerable`1
TYPE:  interface
TOKEN: 0x2000037
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000161  System.Linq.IOrderedEnumerable<TElement> CreateOrderedEnumerable(System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending)
END_CLASS

CLASS: System.Linq.IGrouping`2
TYPE:  interface
TOKEN: 0x2000038
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  Key  get=-1  // abstract
METHODS:
END_CLASS

CLASS: System.Linq.ILookup`2
TYPE:  interface
TOKEN: 0x2000039
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
METHODS:
END_CLASS

CLASS: System.Linq.Lookup`2
TYPE:  class
TOKEN: 0x200003A
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Linq.ILookup`2
FIELDS:
  private           System.Collections.Generic.IEqualityComparer<TKey>comparer  // 0x0
  private           System.Linq.Lookup.Grouping<TKey,TElement>[]groupings  // 0x0
  private           System.Linq.Lookup.Grouping<TKey,TElement>lastGrouping  // 0x0
  private           System.Int32                    count  // 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000163  System.Linq.Lookup<TKey,TElement> Create(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
  RVA=-1  // not resolved  token=0x6000164  System.Linq.Lookup<TKey,TElement> CreateForJoin(System.Collections.Generic.IEnumerable<TElement> source, System.Func<TElement,TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
  RVA=-1  // not resolved  token=0x6000165  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> comparer)
  RVA=-1  // not resolved  token=0x6000166  System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000167  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000168  System.Int32 InternalGetHashCode(TKey key)
  RVA=-1  // not resolved  token=0x6000169  System.Linq.Lookup.Grouping<TKey,TElement> GetGrouping(TKey key, System.Boolean create)
  RVA=-1  // not resolved  token=0x600016A  System.Void Resize()
END_CLASS

CLASS: System.Linq.Set`1
TYPE:  class
TOKEN: 0x200003E
FIELDS:
  private           System.Int32[]                  buckets  // 0x0
  private           System.Linq.Set.Slot<TElement>[]slots  // 0x0
  private           System.Int32                    count  // 0x0
  private           System.Int32                    freeList  // 0x0
  private           System.Collections.Generic.IEqualityComparer<TElement>comparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000188  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TElement> comparer)
  RVA=-1  // not resolved  token=0x6000189  System.Boolean Add(TElement value)
  RVA=-1  // not resolved  token=0x600018A  System.Boolean Remove(TElement value)
  RVA=-1  // not resolved  token=0x600018B  System.Boolean Find(TElement value, System.Boolean add)
  RVA=-1  // not resolved  token=0x600018C  System.Void Resize()
  RVA=-1  // not resolved  token=0x600018D  System.Int32 InternalGetHashCode(TElement value)
END_CLASS

CLASS: System.Linq.GroupedEnumerable`3
TYPE:  class
TOKEN: 0x2000040
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Collections.Generic.IEnumerable<TSource>source  // 0x0
  private           System.Func<TSource,TKey>       keySelector  // 0x0
  private           System.Func<TSource,TElement>   elementSelector  // 0x0
  private           System.Collections.Generic.IEqualityComparer<TKey>comparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600018E  System.Void .ctor(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,TKey> keySelector, System.Func<TSource,TElement> elementSelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
  RVA=-1  // not resolved  token=0x600018F  System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000190  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: System.Linq.OrderedEnumerable`1
TYPE:  abstract class
TOKEN: 0x2000041
IMPLEMENTS: System.Linq.IOrderedEnumerable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Collections.Generic.IEnumerable<TElement>source  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000191  System.Collections.Generic.IEnumerator<TElement> GetEnumerator()
  RVA=-1  // abstract  token=0x6000192  System.Linq.EnumerableSorter<TElement> GetEnumerableSorter(System.Linq.EnumerableSorter<TElement> next)
  RVA=-1  // not resolved  token=0x6000193  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // generic def  token=0x6000194  System.Linq.IOrderedEnumerable<TElement> System.Linq.IOrderedEnumerable<TElement>.CreateOrderedEnumerable(System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending)
  RVA=-1  // not resolved  token=0x6000195  System.Void .ctor()
END_CLASS

CLASS: System.Linq.OrderedEnumerable`2
TYPE:  class
TOKEN: 0x2000043
EXTENDS: System.Linq.OrderedEnumerable`1
FIELDS:
  private           System.Linq.OrderedEnumerable<TElement>parent  // 0x0
  private           System.Func<TElement,TKey>      keySelector  // 0x0
  private           System.Collections.Generic.IComparer<TKey>comparer  // 0x0
  private           System.Boolean                  descending  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600019C  System.Void .ctor(System.Collections.Generic.IEnumerable<TElement> source, System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending)
  RVA=-1  // not resolved  token=0x600019D  System.Linq.EnumerableSorter<TElement> GetEnumerableSorter(System.Linq.EnumerableSorter<TElement> next)
END_CLASS

CLASS: System.Linq.EnumerableSorter`1
TYPE:  abstract class
TOKEN: 0x2000044
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600019E  System.Void ComputeKeys(TElement[] elements, System.Int32 count)
  RVA=-1  // abstract  token=0x600019F  System.Int32 CompareKeys(System.Int32 index1, System.Int32 index2)
  RVA=-1  // not resolved  token=0x60001A0  System.Int32[] Sort(TElement[] elements, System.Int32 count)
  RVA=-1  // not resolved  token=0x60001A1  System.Void QuickSort(System.Int32[] map, System.Int32 left, System.Int32 right)
  RVA=-1  // not resolved  token=0x60001A2  System.Void .ctor()
END_CLASS

CLASS: System.Linq.EnumerableSorter`2
TYPE:  class
TOKEN: 0x2000045
EXTENDS: System.Linq.EnumerableSorter`1
FIELDS:
  private           System.Func<TElement,TKey>      keySelector  // 0x0
  private           System.Collections.Generic.IComparer<TKey>comparer  // 0x0
  private           System.Boolean                  descending  // 0x0
  private           System.Linq.EnumerableSorter<TElement>next  // 0x0
  private           TKey[]                          keys  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001A3  System.Void .ctor(System.Func<TElement,TKey> keySelector, System.Collections.Generic.IComparer<TKey> comparer, System.Boolean descending, System.Linq.EnumerableSorter<TElement> next)
  RVA=-1  // not resolved  token=0x60001A4  System.Void ComputeKeys(TElement[] elements, System.Int32 count)
  RVA=-1  // not resolved  token=0x60001A5  System.Int32 CompareKeys(System.Int32 index1, System.Int32 index2)
END_CLASS

CLASS: System.Linq.Buffer`1
TYPE:  sealed struct
TOKEN: 0x2000046
FIELDS:
  private           TElement[]                      items  // 0x0
  private           System.Int32                    count  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001A6  System.Void .ctor(System.Collections.Generic.IEnumerable<TElement> source)
  RVA=-1  // not resolved  token=0x60001A7  TElement[] ToArray()
END_CLASS

CLASS: System.Linq.Expressions.CachedReflectionInfo
TYPE:  static class
TOKEN: 0x2000047
SIZE:  0x10
FIELDS:
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_SetNotMatched  // static @ 0x0
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_CreateMatchmaker  // static @ 0x8
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_GetMatch  // static @ 0x10
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_ClearMatch  // static @ 0x18
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_UpdateRules  // static @ 0x20
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_GetRules  // static @ 0x28
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_GetRuleCache  // static @ 0x30
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_GetCachedRules  // static @ 0x38
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_AddRule  // static @ 0x40
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_MoveRule  // static @ 0x48
  private   static  System.Reflection.MethodInfo    s_CallSiteOps_Bind  // static @ 0x50
  private   static  System.Reflection.MethodInfo    s_String_op_Equality_String_String  // static @ 0x58
  private   static  System.Reflection.MethodInfo    s_Math_Pow_Double_Double  // static @ 0x60
PROPERTIES:
  CallSiteOps_SetNotMatched  get=0x0ACB4430
  CallSiteOps_CreateMatchmaker  get=0x0ACB3F68
  CallSiteOps_GetMatch  get=0x0ACB4100
  CallSiteOps_ClearMatch  get=0x0ACB3E9C
  CallSiteOps_UpdateRules  get=0x0ACB44F8
  CallSiteOps_GetRules  get=0x0ACB4298
  CallSiteOps_GetRuleCache  get=0x0ACB41CC
  CallSiteOps_GetCachedRules  get=0x0ACB4034
  CallSiteOps_AddRule  get=0x0ACB3D04
  CallSiteOps_MoveRule  get=0x0ACB4364
  CallSiteOps_Bind  get=0x0ACB3DD0
  String_op_Equality_String_String  get=0x0ACB4738
  Math_Pow_Double_Double  get=0x0ACB45C4
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.BinaryExpression
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.Expression<Right>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<Left>k__BackingField  // 0x18
PROPERTIES:
  CanReduce  get=0x0ACB394C
  Right  get=0x02B2ECC0
  Left  get=0x0385B100
  Method  get=0x0ACB3CF4
  Conversion  get=0x0ACB396C
  IsLifted  get=0x0ACB3B08
  IsLiftedToNull  get=0x0ACB3AA4
  IsLiftedLogical  get=0x0ACB3974
  IsReferenceComparison  get=0x0ACB3C3C
METHODS:
  RVA=0x0ACB38E0  token=0x60001B5  System.Void .ctor(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACB2988  token=0x60001B7  System.Boolean IsOpAssignment(System.Linq.Expressions.ExpressionType op)
  RVA=0x011EC580  token=0x60001BB  System.Reflection.MethodInfo GetMethod()
  RVA=0x0ACB37C8  token=0x60001BC  System.Linq.Expressions.BinaryExpression Update(System.Linq.Expressions.Expression left, System.Linq.Expressions.LambdaExpression conversion, System.Linq.Expressions.Expression right)
  RVA=0x0ACB3758  token=0x60001BD  System.Linq.Expressions.Expression Reduce()
  RVA=0x0ACB28A8  token=0x60001BE  System.Linq.Expressions.ExpressionType GetBinaryOpFromAssignmentOp(System.Linq.Expressions.ExpressionType op)
  RVA=0x0ACB3664  token=0x60001BF  System.Linq.Expressions.Expression ReduceVariable()
  RVA=0x0ACB2DA4  token=0x60001C0  System.Linq.Expressions.Expression ReduceMember()
  RVA=0x0ACB2994  token=0x60001C1  System.Linq.Expressions.Expression ReduceIndex()
  RVA=0x011EC580  token=0x60001C3  System.Linq.Expressions.LambdaExpression GetConversion()
  RVA=0x0ACB2860  token=0x60001C6  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACB311C  token=0x60001C9  System.Linq.Expressions.Expression ReduceUserdefinedLifted()
END_CLASS

CLASS: System.Linq.Expressions.LogicalBinaryExpression
TYPE:  sealed class
TOKEN: 0x2000049
SIZE:  0x28
EXTENDS: System.Linq.Expressions.BinaryExpression
FIELDS:
  private   readonly System.Linq.Expressions.ExpressionType<NodeType>k__BackingField  // 0x20
PROPERTIES:
  Type  get=0x0ACCA86C
  NodeType  get=0x02B70F90
METHODS:
  RVA=0x0ACCA838  token=0x60001CA  System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
END_CLASS

CLASS: System.Linq.Expressions.AssignBinaryExpression
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x20
EXTENDS: System.Linq.Expressions.BinaryExpression
FIELDS:
PROPERTIES:
  Type  get=0x0ACB283C
  NodeType  get=0x04DA9EE0
METHODS:
  RVA=0x0ACB2834  token=0x60001CD  System.Void .ctor(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
END_CLASS

CLASS: System.Linq.Expressions.CoalesceConversionBinaryExpression
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x28
EXTENDS: System.Linq.Expressions.BinaryExpression
FIELDS:
  private   readonly System.Linq.Expressions.LambdaExpression_conversion  // 0x20
PROPERTIES:
  NodeType  get=0x04D9BE00
  Type  get=0x0ACB48D0
METHODS:
  RVA=0x0ACB48A0  token=0x60001D0  System.Void .ctor(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x04D862C0  token=0x60001D1  System.Linq.Expressions.LambdaExpression GetConversion()
END_CLASS

CLASS: System.Linq.Expressions.OpAssignMethodConversionBinaryExpression
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x40
EXTENDS: System.Linq.Expressions.MethodBinaryExpression
FIELDS:
  private   readonly System.Linq.Expressions.LambdaExpression_conversion  // 0x38
METHODS:
  RVA=0x0ACCA8F4  token=0x60001D4  System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Type type, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x04D85A50  token=0x60001D5  System.Linq.Expressions.LambdaExpression GetConversion()
END_CLASS

CLASS: System.Linq.Expressions.SimpleBinaryExpression
TYPE:  class
TOKEN: 0x200004D
SIZE:  0x30
EXTENDS: System.Linq.Expressions.BinaryExpression
FIELDS:
  private   readonly System.Linq.Expressions.ExpressionType<NodeType>k__BackingField  // 0x20
  private   readonly System.Type                     <Type>k__BackingField  // 0x28
PROPERTIES:
  NodeType  get=0x02B70F90
  Type  get=0x04D86240
METHODS:
  RVA=0x0ACCADC8  token=0x60001D6  System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.MethodBinaryExpression
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x38
EXTENDS: System.Linq.Expressions.SimpleBinaryExpression
FIELDS:
  private   readonly System.Reflection.MethodInfo    _method  // 0x30
METHODS:
  RVA=0x0ACCA8BC  token=0x60001D9  System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Type type, System.Reflection.MethodInfo method)
  RVA=0x011F36E0  token=0x60001DA  System.Reflection.MethodInfo GetMethod()
END_CLASS

CLASS: System.Linq.Expressions.Expression
TYPE:  abstract class
TOKEN: 0x200004F
SIZE:  0x10
FIELDS:
  private   static readonly System.Dynamic.Utils.CacheDict<System.Type,System.Reflection.MethodInfo>s_lambdaDelegateCache  // static @ 0x0
  private   static  System.Dynamic.Utils.CacheDict<System.Type,System.Func<System.Linq.Expressions.Expression,System.String,System.Boolean,System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression>,System.Linq.Expressions.LambdaExpression>>s_lambdaFactories  // static @ 0x8
  private   static  System.Runtime.CompilerServices.ConditionalWeakTable<System.Linq.Expressions.Expression,System.Linq.Expressions.Expression.ExtensionInfo>s_legacyCtorSupportTable  // static @ 0x10
PROPERTIES:
  NodeType  get=0x0ACCA688
  Type  get=0x0ACCA760
  CanReduce  get=0x012081B0
METHODS:
  RVA=0x0ACB6844  token=0x60001DB  System.Linq.Expressions.BinaryExpression Assign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACBDF8C  token=0x60001DC  System.Linq.Expressions.BinaryExpression GetUserDefinedBinaryOperator(System.Linq.Expressions.ExpressionType binaryType, System.String name, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull)
  RVA=0x0ACBC868  token=0x60001DD  System.Linq.Expressions.BinaryExpression GetMethodBasedBinaryOperator(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Boolean liftToNull)
  RVA=0x0ACBC61C  token=0x60001DE  System.Linq.Expressions.BinaryExpression GetMethodBasedAssignOperator(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion, System.Boolean liftToNull)
  RVA=0x0ACBDD98  token=0x60001DF  System.Linq.Expressions.BinaryExpression GetUserDefinedBinaryOperatorOrThrow(System.Linq.Expressions.ExpressionType binaryType, System.String name, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull)
  RVA=0x0ACBDB4C  token=0x60001E0  System.Linq.Expressions.BinaryExpression GetUserDefinedAssignOperatorOrThrow(System.Linq.Expressions.ExpressionType binaryType, System.String name, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.LambdaExpression conversion, System.Boolean liftToNull)
  RVA=0x0ACBE2E4  token=0x60001E1  System.Reflection.MethodInfo GetUserDefinedBinaryOperator(System.Linq.Expressions.ExpressionType binaryType, System.Type leftType, System.Type rightType, System.String name)
  RVA=0x0ACC0120  token=0x60001E2  System.Boolean IsLiftingConditionalLogicalOperator(System.Type left, System.Type right, System.Reflection.MethodInfo method, System.Linq.Expressions.ExpressionType binaryType)
  RVA=0x0ACC4C0C  token=0x60001E3  System.Boolean ParameterIsAssignable(System.Reflection.ParameterInfo pi, System.Type argType)
  RVA=0x0ACC9330  token=0x60001E4  System.Void ValidateParamswithOperandsOrThrow(System.Type paramType, System.Type operandType, System.Linq.Expressions.ExpressionType exprType, System.String name)
  RVA=0x0ACC91F8  token=0x60001E5  System.Void ValidateOperator(System.Reflection.MethodInfo method)
  RVA=0x0ACC8EF8  token=0x60001E6  System.Void ValidateMethodInfo(System.Reflection.MethodInfo method, System.String paramName)
  RVA=0x0ACC01C0  token=0x60001E7  System.Boolean IsNullComparison(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACC028C  token=0x60001E8  System.Boolean IsNullConstant(System.Linq.Expressions.Expression e)
  RVA=0x0ACC9A18  token=0x60001E9  System.Void ValidateUserDefinedConditionalLogicOperator(System.Linq.Expressions.ExpressionType nodeType, System.Type left, System.Type right, System.Reflection.MethodInfo method)
  RVA=0x0ACCA3CC  token=0x60001EA  System.Void VerifyOpTrueFalse(System.Linq.Expressions.ExpressionType nodeType, System.Type left, System.Reflection.MethodInfo opTrue, System.String paramName)
  RVA=0x0ACC04CC  token=0x60001EB  System.Boolean IsValidLiftedConditionalLogicalOperator(System.Type left, System.Type right, System.Reflection.ParameterInfo[] pms)
  RVA=0x0ACC1A00  token=0x60001EC  System.Linq.Expressions.BinaryExpression MakeBinary(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method)
  RVA=0x0ACC10FC  token=0x60001ED  System.Linq.Expressions.BinaryExpression MakeBinary(System.Linq.Expressions.ExpressionType binaryType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACBB408  token=0x60001EE  System.Linq.Expressions.BinaryExpression Equal(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACBB464  token=0x60001EF  System.Linq.Expressions.BinaryExpression Equal(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method)
  RVA=0x0ACC5C28  token=0x60001F0  System.Linq.Expressions.BinaryExpression ReferenceEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACC3F38  token=0x60001F1  System.Linq.Expressions.BinaryExpression NotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACC3F94  token=0x60001F2  System.Linq.Expressions.BinaryExpression NotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method)
  RVA=0x0ACC5D88  token=0x60001F3  System.Linq.Expressions.BinaryExpression ReferenceNotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACBC0EC  token=0x60001F4  System.Linq.Expressions.BinaryExpression GetEqualityComparisonOperator(System.Linq.Expressions.ExpressionType binaryType, System.String opName, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull)
  RVA=0x0ACBEBB4  token=0x60001F5  System.Linq.Expressions.BinaryExpression GreaterThan(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method)
  RVA=0x0ACC0F00  token=0x60001F6  System.Linq.Expressions.BinaryExpression LessThan(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method)
  RVA=0x0ACBEAD8  token=0x60001F7  System.Linq.Expressions.BinaryExpression GreaterThanOrEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method)
  RVA=0x0ACC0E24  token=0x60001F8  System.Linq.Expressions.BinaryExpression LessThanOrEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull, System.Reflection.MethodInfo method)
  RVA=0x0ACBBF00  token=0x60001F9  System.Linq.Expressions.BinaryExpression GetComparisonOperator(System.Linq.Expressions.ExpressionType binaryType, System.String opName, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftToNull)
  RVA=0x0ACB5BB8  token=0x60001FA  System.Linq.Expressions.BinaryExpression AndAlso(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACB5750  token=0x60001FB  System.Linq.Expressions.BinaryExpression AndAlso(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC45C8  token=0x60001FC  System.Linq.Expressions.BinaryExpression OrElse(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACB99A0  token=0x60001FD  System.Linq.Expressions.BinaryExpression Coalesce(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC7EAC  token=0x60001FE  System.Type ValidateCoalesceArgTypes(System.Type left, System.Type right)
  RVA=0x0ACB5580  token=0x60001FF  System.Linq.Expressions.BinaryExpression Add(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACB5168  token=0x6000200  System.Linq.Expressions.BinaryExpression AddAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC8FA4  token=0x6000201  System.Void ValidateOpAssignConversionLambda(System.Linq.Expressions.LambdaExpression conversion, System.Linq.Expressions.Expression left, System.Reflection.MethodInfo method, System.Linq.Expressions.ExpressionType nodeType)
  RVA=0x0ACB4F2C  token=0x6000202  System.Linq.Expressions.BinaryExpression AddAssignChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACB53A4  token=0x6000203  System.Linq.Expressions.BinaryExpression AddChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC6CE4  token=0x6000204  System.Linq.Expressions.BinaryExpression Subtract(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC68CC  token=0x6000205  System.Linq.Expressions.BinaryExpression SubtractAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC6690  token=0x6000206  System.Linq.Expressions.BinaryExpression SubtractAssignChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC6B08  token=0x6000207  System.Linq.Expressions.BinaryExpression SubtractChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACBB198  token=0x6000208  System.Linq.Expressions.BinaryExpression Divide(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACBAF5C  token=0x6000209  System.Linq.Expressions.BinaryExpression DivideAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC2B10  token=0x600020A  System.Linq.Expressions.BinaryExpression Modulo(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC28D4  token=0x600020B  System.Linq.Expressions.BinaryExpression ModuloAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC3340  token=0x600020C  System.Linq.Expressions.BinaryExpression Multiply(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC2F28  token=0x600020D  System.Linq.Expressions.BinaryExpression MultiplyAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC2CEC  token=0x600020E  System.Linq.Expressions.BinaryExpression MultiplyAssignChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC3164  token=0x600020F  System.Linq.Expressions.BinaryExpression MultiplyChecked(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC02D8  token=0x6000210  System.Boolean IsSimpleShift(System.Type left, System.Type right)
  RVA=0x0ACBDA40  token=0x6000211  System.Type GetResultTypeOfShift(System.Type left, System.Type right)
  RVA=0x0ACC0C50  token=0x6000212  System.Linq.Expressions.BinaryExpression LeftShift(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC0A24  token=0x6000213  System.Linq.Expressions.BinaryExpression LeftShiftAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC64BC  token=0x6000214  System.Linq.Expressions.BinaryExpression RightShift(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC6290  token=0x6000215  System.Linq.Expressions.BinaryExpression RightShiftAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACB5E48  token=0x6000216  System.Linq.Expressions.BinaryExpression And(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACB5C0C  token=0x6000217  System.Linq.Expressions.BinaryExpression AndAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC4A30  token=0x6000218  System.Linq.Expressions.BinaryExpression Or(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC438C  token=0x6000219  System.Linq.Expressions.BinaryExpression OrAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACBB77C  token=0x600021A  System.Linq.Expressions.BinaryExpression ExclusiveOr(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACBB540  token=0x600021B  System.Linq.Expressions.BinaryExpression ExclusiveOrAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACC4F8C  token=0x600021C  System.Linq.Expressions.BinaryExpression Power(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method)
  RVA=0x0ACC4E1C  token=0x600021D  System.Linq.Expressions.BinaryExpression PowerAssign(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Reflection.MethodInfo method, System.Linq.Expressions.LambdaExpression conversion)
  RVA=0x0ACB646C  token=0x600021E  System.Linq.Expressions.BinaryExpression ArrayIndex(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression index)
  RVA=0x0ACB71B4  token=0x600021F  System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACB73F8  token=0x6000220  System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACB70A4  token=0x6000221  System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3)
  RVA=0x0ACB72C0  token=0x6000222  System.Linq.Expressions.BlockExpression Block(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4)
  RVA=0x0ACB7660  token=0x6000223  System.Linq.Expressions.BlockExpression Block(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACB6FF4  token=0x6000224  System.Linq.Expressions.BlockExpression Block(System.Type type, System.Linq.Expressions.Expression[] expressions)
  RVA=0x0ACB6F6C  token=0x6000225  System.Linq.Expressions.BlockExpression Block(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACB76D8  token=0x6000226  System.Linq.Expressions.BlockExpression Block(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] expressions)
  RVA=0x0ACB7260  token=0x6000227  System.Linq.Expressions.BlockExpression Block(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] expressions)
  RVA=0x0ACB74D4  token=0x6000228  System.Linq.Expressions.BlockExpression Block(System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACB6D14  token=0x6000229  System.Linq.Expressions.BlockExpression Block(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACB6A00  token=0x600022A  System.Linq.Expressions.BlockExpression BlockCore(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACCA06C  token=0x600022B  System.Void ValidateVariables(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> varList, System.String collectionName)
  RVA=0x0ACBD444  token=0x600022C  System.Linq.Expressions.BlockExpression GetOptimizedBlockExpression(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACC1A84  token=0x600022D  System.Linq.Expressions.CatchBlock MakeCatchBlock(System.Type type, System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression filter)
  RVA=0x0ACBA0AC  token=0x600022E  System.Linq.Expressions.ConditionalExpression Condition(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse)
  RVA=0x0ACB9E34  token=0x600022F  System.Linq.Expressions.ConditionalExpression Condition(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse, System.Type type)
  RVA=0x0ACBEC90  token=0x6000230  System.Linq.Expressions.ConditionalExpression IfThen(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue)
  RVA=0x0ACBA4B4  token=0x6000231  System.Linq.Expressions.ConstantExpression Constant(System.Object value)
  RVA=0x0ACBA284  token=0x6000232  System.Linq.Expressions.ConstantExpression Constant(System.Object value, System.Type type)
  RVA=0x0ACBB374  token=0x6000233  System.Linq.Expressions.DefaultExpression Empty()
  RVA=0x0ACBAE78  token=0x6000234  System.Linq.Expressions.DefaultExpression Default(System.Type type)
  RVA=0x041E1670  token=0x6000235  System.Void .ctor()
  RVA=0x0ACC5BE0  token=0x6000239  System.Linq.Expressions.Expression Reduce()
  RVA=0x0ACCA584  token=0x600023A  System.Linq.Expressions.Expression VisitChildren(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACB4EE4  token=0x600023B  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACC5ACC  token=0x600023C  System.Linq.Expressions.Expression ReduceAndCheck()
  RVA=0x0ACC703C  token=0x600023D  System.String ToString()
  RVA=0x0ACC5EE8  token=0x600023E  System.Void RequiresCanRead(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> items, System.String paramName)
  RVA=0x0ACC5F8C  token=0x600023F  System.Void RequiresCanWrite(System.Linq.Expressions.Expression expression, System.String paramName)
  RVA=0x0ACB7734  token=0x6000240  System.Linq.Expressions.GotoExpression Break(System.Linq.Expressions.LabelTarget target)
  RVA=0x0ACC6160  token=0x6000241  System.Linq.Expressions.GotoExpression Return(System.Linq.Expressions.LabelTarget target)
  RVA=0x0ACC61F0  token=0x6000242  System.Linq.Expressions.GotoExpression Return(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value)
  RVA=0x0ACBEA7C  token=0x6000243  System.Linq.Expressions.GotoExpression Goto(System.Linq.Expressions.LabelTarget target, System.Type type)
  RVA=0x0ACBE9E0  token=0x6000244  System.Linq.Expressions.GotoExpression Goto(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value)
  RVA=0x0ACC1D00  token=0x6000245  System.Linq.Expressions.GotoExpression MakeGoto(System.Linq.Expressions.GotoExpressionKind kind, System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value, System.Type type)
  RVA=0x0ACC8134  token=0x6000246  System.Void ValidateGoto(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression& value, System.String targetParameter, System.String valueParameter, System.Type type)
  RVA=0x0ACC8000  token=0x6000247  System.Void ValidateGotoType(System.Type expectedType, System.Linq.Expressions.Expression& value, System.String paramName)
  RVA=0x0ACC1EA0  token=0x6000248  System.Linq.Expressions.IndexExpression MakeIndex(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
  RVA=0x0ACB60B4  token=0x6000249  System.Linq.Expressions.IndexExpression ArrayAccess(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression[] indexes)
  RVA=0x0ACB6110  token=0x600024A  System.Linq.Expressions.IndexExpression ArrayAccess(System.Linq.Expressions.Expression array, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> indexes)
  RVA=0x0ACC5860  token=0x600024B  System.Linq.Expressions.IndexExpression Property(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
  RVA=0x0ACC1DE4  token=0x600024C  System.Linq.Expressions.IndexExpression MakeIndexProperty(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.String paramName, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> argList)
  RVA=0x0ACC82B4  token=0x600024D  System.Void ValidateIndexedProperty(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.String paramName, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& argList)
  RVA=0x0ACC7BEC  token=0x600024E  System.Void ValidateAccessor(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] indexes, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String paramName)
  RVA=0x0ACC77EC  token=0x600024F  System.Void ValidateAccessorArgumentTypes(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] indexes, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String paramName)
  RVA=0x0ACBF284  token=0x6000250  System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression)
  RVA=0x0ACBF9F8  token=0x6000251  System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0)
  RVA=0x0ACBF370  token=0x6000252  System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACBF528  token=0x6000253  System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACBFB54  token=0x6000254  System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3)
  RVA=0x0ACBF73C  token=0x6000255  System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4)
  RVA=0x0ACBEE8C  token=0x6000256  System.Linq.Expressions.InvocationExpression Invoke(System.Linq.Expressions.Expression expression, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
  RVA=0x0ACBC47C  token=0x6000257  System.Reflection.MethodInfo GetInvokeMethod(System.Linq.Expressions.Expression expression)
  RVA=0x0ACC06EC  token=0x6000258  System.Linq.Expressions.LabelExpression Label(System.Linq.Expressions.LabelTarget target)
  RVA=0x0ACC061C  token=0x6000259  System.Linq.Expressions.LabelExpression Label(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression defaultValue)
  RVA=0x0ACC0738  token=0x600025A  System.Linq.Expressions.LabelTarget Label()
  RVA=0x0ACC0594  token=0x600025B  System.Linq.Expressions.LabelTarget Label(System.String name)
  RVA=0x0ACC08A4  token=0x600025C  System.Linq.Expressions.LabelTarget Label(System.Type type)
  RVA=0x0ACC07B0  token=0x600025D  System.Linq.Expressions.LabelTarget Label(System.Type type, System.String name)
  RVA=0x0ACBA95C  token=0x600025E  System.Linq.Expressions.LambdaExpression CreateLambda(System.Type delegateType, System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=-1  // generic def  token=0x600025F  System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
  RVA=-1  // generic def  token=0x6000260  System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=-1  // generic def  token=0x6000261  System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.Boolean tailCall, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=-1  // generic def  token=0x6000262  System.Linq.Expressions.Expression<TDelegate> Lambda(System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=0x0ACC08F0  token=0x6000263  System.Linq.Expressions.LambdaExpression Lambda(System.Type delegateType, System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
  RVA=0x0ACC095C  token=0x6000264  System.Linq.Expressions.LambdaExpression Lambda(System.Type delegateType, System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.Generic.IEnumerable<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=0x0ACC8818  token=0x6000265  System.Void ValidateLambdaArgs(System.Type delegateType, System.Linq.Expressions.Expression& body, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters, System.String paramName)
  RVA=0x0ACC0FDC  token=0x6000266  System.Linq.Expressions.LoopExpression Loop(System.Linq.Expressions.Expression body, System.Linq.Expressions.LabelTarget break, System.Linq.Expressions.LabelTarget continue)
  RVA=0x0ACBBADC  token=0x6000267  System.Linq.Expressions.MemberExpression Field(System.Linq.Expressions.Expression expression, System.Reflection.FieldInfo field)
  RVA=0x0ACBB958  token=0x6000268  System.Linq.Expressions.MemberExpression Field(System.Linq.Expressions.Expression expression, System.String fieldName)
  RVA=0x0ACC56C8  token=0x6000269  System.Linq.Expressions.MemberExpression Property(System.Linq.Expressions.Expression expression, System.String propertyName)
  RVA=0x0ACC53AC  token=0x600026A  System.Linq.Expressions.MemberExpression Property(System.Linq.Expressions.Expression expression, System.Reflection.PropertyInfo property)
  RVA=0x0ACC58F8  token=0x600026B  System.Linq.Expressions.MemberExpression Property(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo propertyAccessor)
  RVA=0x0ACBD86C  token=0x600026C  System.Reflection.PropertyInfo GetProperty(System.Reflection.MethodInfo mi, System.String paramName, System.Int32 index)
  RVA=0x0ACB98BC  token=0x600026D  System.Boolean CheckMethod(System.Reflection.MethodInfo method, System.Reflection.MethodInfo propertyMethod)
  RVA=0x0ACC1F28  token=0x600026E  System.Linq.Expressions.MemberExpression MakeMemberAccess(System.Linq.Expressions.Expression expression, System.Reflection.MemberInfo member)
  RVA=0x0ACB8B48  token=0x600026F  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method)
  RVA=0x0ACB9010  token=0x6000270  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0)
  RVA=0x0ACB91C4  token=0x6000271  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACB7C54  token=0x6000272  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACB8C40  token=0x6000273  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3)
  RVA=0x0ACB8228  token=0x6000274  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4)
  RVA=0x0ACB942C  token=0x6000275  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression[] arguments)
  RVA=0x0ACB9868  token=0x6000276  System.Linq.Expressions.MethodCallExpression Call(System.Reflection.MethodInfo method, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
  RVA=0x0ACB8A08  token=0x6000277  System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method)
  RVA=0x0ACB9480  token=0x6000278  System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression[] arguments)
  RVA=0x0ACB94E0  token=0x6000279  System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0)
  RVA=0x0ACB7F6C  token=0x600027A  System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACB8690  token=0x600027B  System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACB96E4  token=0x600027C  System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.String methodName, System.Type[] typeArguments, System.Linq.Expressions.Expression[] arguments)
  RVA=0x0ACB77C4  token=0x600027D  System.Linq.Expressions.MethodCallExpression Call(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
  RVA=0x0ACC8E78  token=0x600027E  System.Reflection.ParameterInfo[] ValidateMethodAndGetParameters(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method)
  RVA=0x0ACC93FC  token=0x600027F  System.Void ValidateStaticOrInstanceMethod(System.Linq.Expressions.Expression instance, System.Reflection.MethodInfo method)
  RVA=0x0ACC7E10  token=0x6000280  System.Void ValidateCallInstanceType(System.Type instanceType, System.Reflection.MethodInfo method)
  RVA=0x0ACC7E00  token=0x6000281  System.Void ValidateArgumentTypes(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>& arguments, System.String methodParamName)
  RVA=0x0ACBD864  token=0x6000282  System.Reflection.ParameterInfo[] GetParametersForValidation(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind)
  RVA=0x0ACC7DF0  token=0x6000283  System.Void ValidateArgumentCount(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Int32 count, System.Reflection.ParameterInfo[] pis)
  RVA=0x0ACC8F70  token=0x6000284  System.Linq.Expressions.Expression ValidateOneArgument(System.Reflection.MethodBase method, System.Linq.Expressions.ExpressionType nodeKind, System.Linq.Expressions.Expression arg, System.Reflection.ParameterInfo pi, System.String methodParamName, System.String argumentParamName)
  RVA=0x0ACC70A4  token=0x6000285  System.Boolean TryQuote(System.Type parameterType, System.Linq.Expressions.Expression& argument)
  RVA=0x0ACBBCD0  token=0x6000286  System.Reflection.MethodInfo FindMethod(System.Type type, System.String methodName, System.Type[] typeArgs, System.Linq.Expressions.Expression[] args, System.Reflection.BindingFlags flags)
  RVA=0x0ACBFDB8  token=0x6000287  System.Boolean IsCompatible(System.Reflection.MethodBase m, System.Linq.Expressions.Expression[] arguments)
  RVA=0x0ACB6024  token=0x6000288  System.Reflection.MethodInfo ApplyTypeArgs(System.Reflection.MethodInfo m, System.Type[] typeArgs)
  RVA=0x0ACC3EDC  token=0x6000289  System.Linq.Expressions.NewArrayExpression NewArrayInit(System.Type type, System.Linq.Expressions.Expression[] initializers)
  RVA=0x0ACC3AFC  token=0x600028A  System.Linq.Expressions.NewArrayExpression NewArrayInit(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> initializers)
  RVA=0x0ACC380C  token=0x600028B  System.Linq.Expressions.NewArrayExpression NewArrayBounds(System.Type type, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> bounds)
  RVA=0x0ACC4C90  token=0x600028C  System.Linq.Expressions.ParameterExpression Parameter(System.Type type)
  RVA=0x0ACC4CDC  token=0x600028D  System.Linq.Expressions.ParameterExpression Parameter(System.Type type, System.String name)
  RVA=0x0ACCA36C  token=0x600028E  System.Linq.Expressions.ParameterExpression Variable(System.Type type, System.String name)
  RVA=0x0ACCA244  token=0x600028F  System.Void Validate(System.Type type, System.Boolean allowByRef)
  RVA=0x0ACC7044  token=0x6000290  System.Linq.Expressions.TryExpression TryFinally(System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally)
  RVA=0x0ACC22D8  token=0x6000291  System.Linq.Expressions.TryExpression MakeTry(System.Type type, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally, System.Linq.Expressions.Expression fault, System.Collections.Generic.IEnumerable<System.Linq.Expressions.CatchBlock> handlers)
  RVA=0x0ACC94E4  token=0x6000292  System.Void ValidateTryAndCatchHaveSameType(System.Type type, System.Linq.Expressions.Expression tryBody, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock> handlers)
  RVA=0x0ACC735C  token=0x6000293  System.Linq.Expressions.TypeBinaryExpression TypeIs(System.Linq.Expressions.Expression expression, System.Type type)
  RVA=0x0ACC722C  token=0x6000294  System.Linq.Expressions.TypeBinaryExpression TypeEqual(System.Linq.Expressions.Expression expression, System.Type type)
  RVA=0x0ACC2504  token=0x6000295  System.Linq.Expressions.UnaryExpression MakeUnary(System.Linq.Expressions.ExpressionType unaryType, System.Linq.Expressions.Expression operand, System.Type type, System.Reflection.MethodInfo method)
  RVA=0x0ACBE624  token=0x6000296  System.Linq.Expressions.UnaryExpression GetUserDefinedUnaryOperatorOrThrow(System.Linq.Expressions.ExpressionType unaryType, System.String name, System.Linq.Expressions.Expression operand)
  RVA=0x0ACBE780  token=0x6000297  System.Linq.Expressions.UnaryExpression GetUserDefinedUnaryOperator(System.Linq.Expressions.ExpressionType unaryType, System.String name, System.Linq.Expressions.Expression operand)
  RVA=0x0ACBD0F0  token=0x6000298  System.Linq.Expressions.UnaryExpression GetMethodBasedUnaryOperator(System.Linq.Expressions.ExpressionType unaryType, System.Linq.Expressions.Expression operand, System.Reflection.MethodInfo method)
  RVA=0x0ACBE494  token=0x6000299  System.Linq.Expressions.UnaryExpression GetUserDefinedCoercionOrThrow(System.Linq.Expressions.ExpressionType coercionType, System.Linq.Expressions.Expression expression, System.Type convertToType)
  RVA=0x0ACBE53C  token=0x600029A  System.Linq.Expressions.UnaryExpression GetUserDefinedCoercion(System.Linq.Expressions.ExpressionType coercionType, System.Linq.Expressions.Expression expression, System.Type convertToType)
  RVA=0x0ACBCDA0  token=0x600029B  System.Linq.Expressions.UnaryExpression GetMethodBasedCoercionOperator(System.Linq.Expressions.ExpressionType unaryType, System.Linq.Expressions.Expression operand, System.Type convertToType, System.Reflection.MethodInfo method)
  RVA=0x0ACC3694  token=0x600029C  System.Linq.Expressions.UnaryExpression Negate(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC748C  token=0x600029D  System.Linq.Expressions.UnaryExpression UnaryPlus(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC351C  token=0x600029E  System.Linq.Expressions.UnaryExpression NegateChecked(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC4070  token=0x600029F  System.Linq.Expressions.UnaryExpression Not(System.Linq.Expressions.Expression expression)
  RVA=0x0ACC40BC  token=0x60002A0  System.Linq.Expressions.UnaryExpression Not(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACBFFD4  token=0x60002A1  System.Linq.Expressions.UnaryExpression IsFalse(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC0380  token=0x60002A2  System.Linq.Expressions.UnaryExpression IsTrue(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC4240  token=0x60002A3  System.Linq.Expressions.UnaryExpression OnesComplement(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC70AC  token=0x60002A4  System.Linq.Expressions.UnaryExpression TypeAs(System.Linq.Expressions.Expression expression, System.Type type)
  RVA=0x0ACC75D8  token=0x60002A5  System.Linq.Expressions.UnaryExpression Unbox(System.Linq.Expressions.Expression expression, System.Type type)
  RVA=0x0ACBA908  token=0x60002A6  System.Linq.Expressions.UnaryExpression Convert(System.Linq.Expressions.Expression expression, System.Type type)
  RVA=0x0ACBA71C  token=0x60002A7  System.Linq.Expressions.UnaryExpression Convert(System.Linq.Expressions.Expression expression, System.Type type, System.Reflection.MethodInfo method)
  RVA=0x0ACBA514  token=0x60002A8  System.Linq.Expressions.UnaryExpression ConvertChecked(System.Linq.Expressions.Expression expression, System.Type type, System.Reflection.MethodInfo method)
  RVA=0x0ACB6664  token=0x60002A9  System.Linq.Expressions.UnaryExpression ArrayLength(System.Linq.Expressions.Expression array)
  RVA=0x0ACC59D0  token=0x60002AA  System.Linq.Expressions.UnaryExpression Quote(System.Linq.Expressions.Expression expression)
  RVA=0x0ACC6EC0  token=0x60002AB  System.Linq.Expressions.UnaryExpression Throw(System.Linq.Expressions.Expression value, System.Type type)
  RVA=0x0ACBED40  token=0x60002AC  System.Linq.Expressions.UnaryExpression Increment(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACBAD2C  token=0x60002AD  System.Linq.Expressions.UnaryExpression Decrement(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC5358  token=0x60002AE  System.Linq.Expressions.UnaryExpression PreIncrementAssign(System.Linq.Expressions.Expression expression)
  RVA=0x0ACC5304  token=0x60002AF  System.Linq.Expressions.UnaryExpression PreIncrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC52B0  token=0x60002B0  System.Linq.Expressions.UnaryExpression PreDecrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC4DC8  token=0x60002B1  System.Linq.Expressions.UnaryExpression PostIncrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC4D74  token=0x60002B2  System.Linq.Expressions.UnaryExpression PostDecrementAssign(System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACC2094  token=0x60002B3  System.Linq.Expressions.UnaryExpression MakeOpAssignUnary(System.Linq.Expressions.ExpressionType kind, System.Linq.Expressions.Expression expression, System.Reflection.MethodInfo method)
  RVA=0x0ACCA5F4  token=0x60002B4  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.BlockExpression
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
PROPERTIES:
  Expressions  get=0x0ACCE210
  Variables  get=0x0ACCE284
  NodeType  get=0x04DA29E0
  Type  get=0x0ACCE240
  ExpressionCount  get=0x0ACCE1E8
METHODS:
  RVA=0x0ACCE1A8  token=0x60002B7  System.Void .ctor()
  RVA=0x0ACCDFA0  token=0x60002B8  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACCDFE8  token=0x60002BB  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACCE010  token=0x60002BD  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACCE038  token=0x60002BE  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> GetOrMakeVariables()
  RVA=0x0ACCE180  token=0x60002BF  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
  RVA=0x0ACCE088  token=0x60002C0  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> ReturnReadOnlyExpressions(System.Linq.Expressions.BlockExpression provider, System.Object& collection)
END_CLASS

CLASS: System.Linq.Expressions.Block2
TYPE:  sealed class
TOKEN: 0x200006C
SIZE:  0x20
EXTENDS: System.Linq.Expressions.BlockExpression
FIELDS:
  private           System.Object                   _arg0  // 0x10
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x18
PROPERTIES:
  ExpressionCount  get=0x02BBE540
METHODS:
  RVA=0x0ACCD5E8  token=0x60002C1  System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACCD4C8  token=0x60002C2  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACCD550  token=0x60002C4  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACCD55C  token=0x60002C5  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.Block3
TYPE:  sealed class
TOKEN: 0x200006D
SIZE:  0x28
EXTENDS: System.Linq.Expressions.BlockExpression
FIELDS:
  private           System.Object                   _arg0  // 0x10
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x18
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x20
PROPERTIES:
  ExpressionCount  get=0x04D8C9E0
METHODS:
  RVA=0x0ACCD76C  token=0x60002C6  System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACCD630  token=0x60002C7  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACCD550  token=0x60002C9  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACCD6C4  token=0x60002CA  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.Block4
TYPE:  sealed class
TOKEN: 0x200006E
SIZE:  0x30
EXTENDS: System.Linq.Expressions.BlockExpression
FIELDS:
  private           System.Object                   _arg0  // 0x10
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x18
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg3  // 0x28
PROPERTIES:
  ExpressionCount  get=0x04D882B0
METHODS:
  RVA=0x0ACCD92C  token=0x60002CB  System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3)
  RVA=0x0ACCD7D0  token=0x60002CC  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACCD550  token=0x60002CE  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACCD86C  token=0x60002CF  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.Block5
TYPE:  sealed class
TOKEN: 0x200006F
SIZE:  0x38
EXTENDS: System.Linq.Expressions.BlockExpression
FIELDS:
  private           System.Object                   _arg0  // 0x10
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x18
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg3  // 0x28
  private   readonly System.Linq.Expressions.Expression_arg4  // 0x30
PROPERTIES:
  ExpressionCount  get=0x04D9EC50
METHODS:
  RVA=0x0ACCDB18  token=0x60002D0  System.Void .ctor(System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4)
  RVA=0x0ACCD9A0  token=0x60002D1  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACCD550  token=0x60002D3  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACCDA48  token=0x60002D4  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.BlockN
TYPE:  class
TOKEN: 0x2000070
SIZE:  0x18
EXTENDS: System.Linq.Expressions.BlockExpression
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression>_expressions  // 0x10
PROPERTIES:
  ExpressionCount  get=0x0ACCE3D4
METHODS:
  RVA=0x0ACCE3A4  token=0x60002D5  System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACCE2B4  token=0x60002D6  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACCE308  token=0x60002D8  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACCE344  token=0x60002D9  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.ScopeExpression
TYPE:  class
TOKEN: 0x2000071
SIZE:  0x18
EXTENDS: System.Linq.Expressions.BlockExpression
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression>_variables  // 0x10
PROPERTIES:
  VariablesList  get=0x02B2ECC0
METHODS:
  RVA=0x0ACCE3A4  token=0x60002DA  System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables)
  RVA=0x0ACDFCC4  token=0x60002DB  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> GetOrMakeVariables()
  RVA=0x0ACDFD00  token=0x60002DD  System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> ReuseOrValidateVariables(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables)
END_CLASS

CLASS: System.Linq.Expressions.Scope1
TYPE:  sealed class
TOKEN: 0x2000072
SIZE:  0x20
EXTENDS: System.Linq.Expressions.ScopeExpression
FIELDS:
  private           System.Object                   _body  // 0x18
PROPERTIES:
  ExpressionCount  get=0x04D85B80
METHODS:
  RVA=0x0ACDFC8C  token=0x60002DE  System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression body)
  RVA=0x0ACDFC94  token=0x60002DF  System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Object body)
  RVA=0x0ACDFB14  token=0x60002E0  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACDFB90  token=0x60002E2  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACDFB9C  token=0x60002E3  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.ScopeN
TYPE:  class
TOKEN: 0x2000073
SIZE:  0x20
EXTENDS: System.Linq.Expressions.ScopeExpression
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression>_body  // 0x18
PROPERTIES:
  Body  get=0x0385B100
  ExpressionCount  get=0x0ACDFEF4
METHODS:
  RVA=0x0ACDFC94  token=0x60002E4  System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> body)
  RVA=0x0ACDFD78  token=0x60002E6  System.Linq.Expressions.Expression GetExpression(System.Int32 index)
  RVA=0x0ACDFDCC  token=0x60002E8  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> GetOrMakeExpressions()
  RVA=0x0ACDFE08  token=0x60002E9  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.ScopeWithType
TYPE:  sealed class
TOKEN: 0x2000074
SIZE:  0x28
EXTENDS: System.Linq.Expressions.ScopeN
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x20
PROPERTIES:
  Type  get=0x04D862C0
METHODS:
  RVA=0x0ACE0060  token=0x60002EA  System.Void .ctor(System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> variables, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> expressions, System.Type type)
  RVA=0x0ACDFF3C  token=0x60002EC  System.Linq.Expressions.BlockExpression Rewrite(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> variables, System.Linq.Expressions.Expression[] args)
END_CLASS

CLASS: System.Linq.Expressions.BlockExpressionList
TYPE:  class
TOKEN: 0x2000075
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Linq.Expressions.BlockExpression_block  // 0x10
  private   readonly System.Linq.Expressions.Expression_arg0  // 0x18
PROPERTIES:
  Item  get=0x0ACCDF48  set=0x0ACCDF78
  Count  get=0x0A8E29E4
  IsReadOnly  get=0x0ACCDF20
METHODS:
  RVA=0x032AF420  token=0x60002ED  System.Void .ctor(System.Linq.Expressions.BlockExpression provider, System.Linq.Expressions.Expression arg0)
  RVA=0x0ACCDE28  token=0x60002EE  System.Int32 IndexOf(System.Linq.Expressions.Expression item)
  RVA=0x0ACCDEA0  token=0x60002EF  System.Void Insert(System.Int32 index, System.Linq.Expressions.Expression item)
  RVA=0x0ACCDEC8  token=0x60002F0  System.Void RemoveAt(System.Int32 index)
  RVA=0x0ACCDBA0  token=0x60002F3  System.Void Add(System.Linq.Expressions.Expression item)
  RVA=0x0ACCDBC8  token=0x60002F4  System.Void Clear()
  RVA=0x0ACCDBF0  token=0x60002F5  System.Boolean Contains(System.Linq.Expressions.Expression item)
  RVA=0x0ACCDC08  token=0x60002F6  System.Void CopyTo(System.Linq.Expressions.Expression[] array, System.Int32 index)
  RVA=0x0ACCDEF0  token=0x60002F9  System.Boolean Remove(System.Linq.Expressions.Expression item)
  RVA=0x0ACCDDCC  token=0x60002FA  System.Collections.Generic.IEnumerator<System.Linq.Expressions.Expression> GetEnumerator()
  RVA=0x0ACCDF18  token=0x60002FB  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: System.Linq.Expressions.CatchBlock
TYPE:  sealed class
TOKEN: 0x2000077
SIZE:  0x30
FIELDS:
  private   readonly System.Linq.Expressions.ParameterExpression<Variable>k__BackingField  // 0x10
  private   readonly System.Type                     <Test>k__BackingField  // 0x18
  private   readonly System.Linq.Expressions.Expression<Body>k__BackingField  // 0x20
  private   readonly System.Linq.Expressions.Expression<Filter>k__BackingField  // 0x28
PROPERTIES:
  Variable  get=0x02B2ECC0
  Test  get=0x0385B100
  Body  get=0x04D862C0
  Filter  get=0x04D86240
METHODS:
  RVA=0x0ACCF93C  token=0x6000302  System.Void .ctor(System.Type test, System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression filter)
  RVA=0x0ACCF8A4  token=0x6000307  System.String ToString()
  RVA=0x0ACCF8AC  token=0x6000308  System.Linq.Expressions.CatchBlock Update(System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Expression filter, System.Linq.Expressions.Expression body)
END_CLASS

CLASS: System.Linq.Expressions.ArrayBuilderExtensions
TYPE:  static class
TOKEN: 0x2000078
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000309  System.Collections.ObjectModel.ReadOnlyCollection<T> ToReadOnly(System.Collections.Generic.ArrayBuilder<T> builder)
END_CLASS

CLASS: System.Linq.Expressions.AnalyzeTypeIsResult
TYPE:  sealed struct
TOKEN: 0x2000079
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Linq.Expressions.AnalyzeTypeIsResultKnownFalse  // const
  public    static  System.Linq.Expressions.AnalyzeTypeIsResultKnownTrue  // const
  public    static  System.Linq.Expressions.AnalyzeTypeIsResultKnownAssignable  // const
  public    static  System.Linq.Expressions.AnalyzeTypeIsResultUnknown  // const
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.ConstantCheck
TYPE:  static class
TOKEN: 0x200007A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACCFDE0  token=0x600030A  System.Linq.Expressions.AnalyzeTypeIsResult AnalyzeTypeIs(System.Linq.Expressions.TypeBinaryExpression typeIs)
  RVA=0x0ACCFE04  token=0x600030B  System.Linq.Expressions.AnalyzeTypeIsResult AnalyzeTypeIs(System.Linq.Expressions.Expression operand, System.Type testType)
END_CLASS

CLASS: System.Linq.Expressions.ConditionalExpression
TYPE:  class
TOKEN: 0x200007B
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.Expression<Test>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<IfTrue>k__BackingField  // 0x18
PROPERTIES:
  NodeType  get=0x04D8D060
  Type  get=0x0ACB283C
  Test  get=0x02B2ECC0
  IfTrue  get=0x0385B100
  IfFalse  get=0x0AA6D17C
METHODS:
  RVA=0x0ACCFD74  token=0x600030C  System.Void .ctor(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue)
  RVA=0x0ACCFB0C  token=0x600030D  System.Linq.Expressions.ConditionalExpression Make(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse, System.Type type)
  RVA=0x0ACCFAB8  token=0x6000313  System.Linq.Expressions.Expression GetFalse()
  RVA=0x0ACCFA70  token=0x6000314  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACCFCD0  token=0x6000315  System.Linq.Expressions.ConditionalExpression Update(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse)
END_CLASS

CLASS: System.Linq.Expressions.FullConditionalExpression
TYPE:  class
TOKEN: 0x200007C
SIZE:  0x28
EXTENDS: System.Linq.Expressions.ConditionalExpression
FIELDS:
  private   readonly System.Linq.Expressions.Expression_false  // 0x20
METHODS:
  RVA=0x0ACDB414  token=0x6000316  System.Void .ctor(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse)
  RVA=0x04D862C0  token=0x6000317  System.Linq.Expressions.Expression GetFalse()
END_CLASS

CLASS: System.Linq.Expressions.FullConditionalExpressionWithType
TYPE:  sealed class
TOKEN: 0x200007D
SIZE:  0x30
EXTENDS: System.Linq.Expressions.FullConditionalExpression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x28
PROPERTIES:
  Type  get=0x04D86240
METHODS:
  RVA=0x0ACDB3E4  token=0x6000318  System.Void .ctor(System.Linq.Expressions.Expression test, System.Linq.Expressions.Expression ifTrue, System.Linq.Expressions.Expression ifFalse, System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.ConstantExpression
TYPE:  class
TOKEN: 0x200007E
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Object                   <Value>k__BackingField  // 0x10
PROPERTIES:
  Type  get=0x0ACD0024
  NodeType  get=0x04DA4FC0
  Value  get=0x02B2ECC0
METHODS:
  RVA=0x0ACCFFCC  token=0x600031A  System.Void .ctor(System.Object value)
  RVA=0x0ACCFF84  token=0x600031E  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
END_CLASS

CLASS: System.Linq.Expressions.TypedConstantExpression
TYPE:  class
TOKEN: 0x200007F
SIZE:  0x20
EXTENDS: System.Linq.Expressions.ConstantExpression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x18
PROPERTIES:
  Type  get=0x0385B100
METHODS:
  RVA=0x0ACE25A8  token=0x600031F  System.Void .ctor(System.Object value, System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.DebugInfoExpression
TYPE:  class
TOKEN: 0x2000080
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.SymbolDocumentInfo<Document>k__BackingField  // 0x10
PROPERTIES:
  StartLine  get=0x0ACD018C
  EndLine  get=0x0ACD013C
  Document  get=0x02B2ECC0
  IsClear  get=0x0ACD0164
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.DefaultExpression
TYPE:  sealed class
TOKEN: 0x2000081
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
PROPERTIES:
  Type  get=0x02B2ECC0
  NodeType  get=0x04DA9F60
METHODS:
  RVA=0x0ACD0A98  token=0x6000325  System.Void .ctor(System.Type type)
  RVA=0x0ACD0A50  token=0x6000328  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
END_CLASS

CLASS: System.Linq.Expressions.ElementInit
TYPE:  sealed class
TOKEN: 0x2000082
SIZE:  0x20
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private   readonly System.Reflection.MethodInfo    <AddMethod>k__BackingField  // 0x10
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression><Arguments>k__BackingField  // 0x18
PROPERTIES:
  AddMethod  get=0x02B2ECC0
  Arguments  get=0x0385B100
  ArgumentCount  get=0x0ACD18E8
METHODS:
  RVA=0x0ACD1894  token=0x600032B  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.Error
TYPE:  static class
TOKEN: 0x2000083
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACD767C  token=0x600032D  System.Exception ReducibleMustOverrideReduce()
  RVA=0x0ACD6318  token=0x600032E  System.Exception InvalidMetaObjectCreated(System.Object p0)
  RVA=0x0ACD46EC  token=0x600032F  System.Exception AmbiguousMatchInExpandoObject(System.Object p0)
  RVA=0x0ACD77C0  token=0x6000330  System.Exception SameKeyExistsInExpando(System.Object key)
  RVA=0x0ACD6574  token=0x6000331  System.Exception KeyDoesNotExistInExpando(System.Object p0)
  RVA=0x0ACD4F94  token=0x6000332  System.Exception CollectionModifiedWhileEnumerating()
  RVA=0x0ACD4FFC  token=0x6000333  System.Exception CollectionReadOnly()
  RVA=0x0ACD6C18  token=0x6000334  System.Exception MustReduceToDifferent()
  RVA=0x0ACD4C08  token=0x6000335  System.Exception BinderNotCompatibleWithCallSite(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACD539C  token=0x6000336  System.Exception DynamicBindingNeedsRestrictions(System.Object p0, System.Object p1)
  RVA=0x0ACD5410  token=0x6000337  System.Exception DynamicObjectResultNotAssignable(System.Object p0, System.Object p1, System.Object p2, System.Object p3)
  RVA=0x0ACD5318  token=0x6000338  System.Exception DynamicBinderResultNotAssignable(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACD4C8C  token=0x6000339  System.Exception BindingCannotBeNull()
  RVA=0x0ACD7614  token=0x600033A  System.Exception ReducedNotCompatible()
  RVA=0x0ACD7840  token=0x600033B  System.Exception SetterHasNoParams(System.String paramName)
  RVA=0x0ACD72BC  token=0x600033C  System.Exception PropertyCannotHaveRefType(System.String paramName)
  RVA=0x0ACD6020  token=0x600033D  System.Exception IndexesOfSetGetMustMatch(System.String paramName)
  RVA=0x0ACD7C48  token=0x600033E  System.Exception TypeParameterIsNotDelegate(System.Object p0)
  RVA=0x0ACD5C34  token=0x600033F  System.Exception FirstArgumentMustBeCallSite()
  RVA=0x0ACD4608  token=0x6000340  System.Exception AccessorsCannotHaveVarArgs(System.String paramName)
  RVA=0x0ACD4590  token=0x6000341  System.Exception AccessorsCannotHaveByRefArgs(System.String paramName)
  RVA=0x0ACD4574  token=0x6000342  System.Exception AccessorsCannotHaveByRefArgs(System.String paramName, System.Int32 index)
  RVA=0x0ACD7AF0  token=0x6000343  System.Exception TypeMustBeDerivedFromSystemDelegate()
  RVA=0x0ACD6DFC  token=0x6000344  System.Exception NoOrInvalidRuleProduced()
  RVA=0x0ACD4DD4  token=0x6000345  System.Exception BoundsCannotBeLessThanOne(System.String paramName)
  RVA=0x0ACD7B58  token=0x6000346  System.Exception TypeMustNotBeByRef(System.String paramName)
  RVA=0x0ACD7BD0  token=0x6000347  System.Exception TypeMustNotBePointer(System.String paramName)
  RVA=0x0ACD78B8  token=0x6000348  System.Exception SetterMustBeVoid(System.String paramName)
  RVA=0x0ACD74AC  token=0x6000349  System.Exception PropertyTypeMustMatchGetter(System.String paramName)
  RVA=0x0ACD7524  token=0x600034A  System.Exception PropertyTypeMustMatchSetter(System.String paramName)
  RVA=0x0ACD4D5C  token=0x600034B  System.Exception BothAccessorsMustBeStatic(System.String paramName)
  RVA=0x0ACD6F88  token=0x600034C  System.Exception OnlyStaticFieldsHaveNullInstance(System.String paramName)
  RVA=0x0ACD7068  token=0x600034D  System.Exception OnlyStaticPropertiesHaveNullInstance(System.String paramName)
  RVA=0x0ACD7000  token=0x600034E  System.Exception OnlyStaticMethodsHaveNullInstance()
  RVA=0x0ACD7434  token=0x600034F  System.Exception PropertyTypeCannotBeVoid(System.String paramName)
  RVA=0x0ACD64FC  token=0x6000350  System.Exception InvalidUnboxType(System.String paramName)
  RVA=0x0ACD55A4  token=0x6000351  System.Exception ExpressionMustBeWriteable(System.String paramName)
  RVA=0x0ACD4A44  token=0x6000352  System.Exception ArgumentMustNotHaveValueType(System.String paramName)
  RVA=0x0ACD6BB0  token=0x6000353  System.Exception MustBeReducible()
  RVA=0x0ACD65E0  token=0x6000354  System.Exception LabelMustBeVoidOrHaveExpression(System.String paramName)
  RVA=0x0ACD6730  token=0x6000355  System.Exception LabelTypeMustBeVoid(System.String paramName)
  RVA=0x0ACD759C  token=0x6000356  System.Exception QuotedExpressionMustBeLambda(System.String paramName)
  RVA=0x0ACD803C  token=0x6000357  System.Exception VariableMustNotBeByRef(System.Object p0, System.Object p1, System.String paramName)
  RVA=0x0ACD8000  token=0x6000358  System.Exception VariableMustNotBeByRef(System.Object p0, System.Object p1, System.String paramName, System.Int32 index)
  RVA=0x0ACD526C  token=0x6000359  System.Exception DuplicateVariable(System.Object p0, System.String paramName)
  RVA=0x0ACD52E8  token=0x600035A  System.Exception DuplicateVariable(System.Object p0, System.String paramName, System.Int32 index)
  RVA=0x0ACD5B38  token=0x600035B  System.Exception FaultCannotHaveCatchOrFinally(System.String paramName)
  RVA=0x0ACD7930  token=0x600035C  System.Exception TryMustHaveCatchFinallyOrFault()
  RVA=0x0ACD4CF4  token=0x600035D  System.Exception BodyOfCatchMustHaveSameTypeAsBodyOfTry()
  RVA=0x0ACD5ACC  token=0x600035E  System.Exception ExtensionNodeMustOverrideProperty(System.Object p0)
  RVA=0x0ACD7F08  token=0x600035F  System.Exception UserDefinedOperatorMustBeStatic(System.Object p0, System.String paramName)
  RVA=0x0ACD7F84  token=0x6000360  System.Exception UserDefinedOperatorMustNotBeVoid(System.Object p0, System.String paramName)
  RVA=0x0ACD4F20  token=0x6000361  System.Exception CoercionOperatorNotDefined(System.Object p0, System.Object p1)
  RVA=0x0ACD7CB4  token=0x6000362  System.Exception UnaryOperatorNotDefined(System.Object p0, System.Object p1)
  RVA=0x0ACD4B84  token=0x6000363  System.Exception BinaryOperatorNotDefined(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACD76E4  token=0x6000364  System.Exception ReferenceEqualityNotDefined(System.Object p0, System.Object p1)
  RVA=0x0ACD70E0  token=0x6000365  System.Exception OperandTypesDoNotMatchParameters(System.Object p0, System.Object p1)
  RVA=0x0ACD71D4  token=0x6000366  System.Exception OverloadOperatorTypeDoesNotMatchConversionType(System.Object p0, System.Object p1)
  RVA=0x0ACD5204  token=0x6000367  System.Exception ConversionIsNotSupportedForArithmeticTypes()
  RVA=0x0ACD4848  token=0x6000368  System.Exception ArgumentMustBeArray(System.String paramName)
  RVA=0x0ACD48C0  token=0x6000369  System.Exception ArgumentMustBeBoolean(System.String paramName)
  RVA=0x0ACD4938  token=0x600036A  System.Exception ArgumentMustBeInteger(System.String paramName)
  RVA=0x0ACD49B0  token=0x600036B  System.Exception ArgumentMustBeInteger(System.String paramName, System.Int32 index)
  RVA=0x0ACD47D0  token=0x600036C  System.Exception ArgumentMustBeArrayIndexType(System.String paramName)
  RVA=0x0ACD49CC  token=0x600036D  System.Exception ArgumentMustBeSingleDimensionalArrayType(System.String paramName)
  RVA=0x0ACD4B1C  token=0x600036E  System.Exception ArgumentTypesMustMatch()
  RVA=0x0ACD4E4C  token=0x600036F  System.Exception CannotAutoInitializeValueTypeMemberThroughProperty(System.Object p0)
  RVA=0x0ACD5FA4  token=0x6000370  System.Exception IncorrectTypeForTypeAs(System.Object p0, System.String paramName)
  RVA=0x0ACD4EB8  token=0x6000371  System.Exception CoalesceUsedOnNonNullType()
  RVA=0x0ACD561C  token=0x6000372  System.Exception ExpressionTypeCannotInitializeArrayType(System.Object p0, System.Object p1)
  RVA=0x0ACD59DC  token=0x6000373  System.Exception ExpressionTypeDoesNotMatchReturn(System.Object p0, System.Object p1)
  RVA=0x0ACD5690  token=0x6000374  System.Exception ExpressionTypeDoesNotMatchAssignment(System.Object p0, System.Object p1)
  RVA=0x0ACD57C4  token=0x6000375  System.Exception ExpressionTypeDoesNotMatchLabel(System.Object p0, System.Object p1)
  RVA=0x0ACD5A50  token=0x6000376  System.Exception ExpressionTypeNotInvocable(System.Object p0, System.String paramName)
  RVA=0x0ACD611C  token=0x6000377  System.Exception InstanceFieldNotDefinedForType(System.Object p0, System.Object p1)
  RVA=0x0ACD5BB0  token=0x6000378  System.Exception FieldInfoNotDefinedForType(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACD5DF0  token=0x6000379  System.Exception IncorrectNumberOfIndexes()
  RVA=0x0ACD5EC0  token=0x600037A  System.Exception IncorrectNumberOfLambdaDeclarationParameters()
  RVA=0x0ACD67A8  token=0x600037B  System.Exception LambdaTypeMustBeDerivedFromSystemDelegate(System.String paramName)
  RVA=0x0ACD6894  token=0x600037C  System.Exception MemberNotFieldOrProperty(System.Object p0, System.String paramName)
  RVA=0x0ACD6910  token=0x600037D  System.Exception MethodContainsGenericParameters(System.Object p0, System.String paramName)
  RVA=0x0ACD698C  token=0x600037E  System.Exception MethodIsGeneric(System.Object p0, System.String paramName)
  RVA=0x0ACD6A08  token=0x600037F  System.Exception MethodNotPropertyAccessor(System.Object p0, System.Object p1, System.String paramName)
  RVA=0x0ACD6A8C  token=0x6000380  System.Exception MethodNotPropertyAccessor(System.Object p0, System.Object p1, System.String paramName, System.Int32 index)
  RVA=0x0ACD7334  token=0x6000381  System.Exception PropertyDoesNotHaveAccessor(System.Object p0, System.String paramName)
  RVA=0x0ACD7248  token=0x6000382  System.Exception ParameterExpressionNotValidAsDelegate(System.Object p0, System.Object p1)
  RVA=0x0ACD73B0  token=0x6000383  System.Exception PropertyNotDefinedForType(System.Object p0, System.Object p1, System.String paramName)
  RVA=0x0ACD6190  token=0x6000384  System.Exception InstancePropertyNotDefinedForType(System.Object p0, System.Object p1, System.String paramName)
  RVA=0x0ACD6098  token=0x6000385  System.Exception InstanceAndMethodTypeMismatch(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACD7D28  token=0x6000386  System.Exception UnhandledBinary(System.Object p0, System.String paramName)
  RVA=0x0ACD7DA4  token=0x6000387  System.Exception UnhandledUnary(System.Object p0, System.String paramName)
  RVA=0x0ACD7E20  token=0x6000388  System.Exception UserDefinedOpMustHaveConsistentTypes(System.Object p0, System.Object p1)
  RVA=0x0ACD7E94  token=0x6000389  System.Exception UserDefinedOpMustHaveValidReturnType(System.Object p0, System.Object p1)
  RVA=0x0ACD6820  token=0x600038A  System.Exception LogicalOperatorMustHaveBooleanOperators(System.Object p0, System.Object p1)
  RVA=0x0ACD6AC8  token=0x600038B  System.Exception MethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1)
  RVA=0x0ACD5C9C  token=0x600038C  System.Exception GenericMethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1)
  RVA=0x0ACD6B3C  token=0x600038D  System.Exception MethodWithMoreThanOneMatch(System.Object p0, System.Object p1)
  RVA=0x0ACD4758  token=0x600038E  System.Exception ArgumentCannotBeOfTypeVoid(System.String paramName)
  RVA=0x0ACD7154  token=0x600038F  System.Exception OutOfRange(System.String paramName, System.Object p1)
  RVA=0x0ACD6658  token=0x6000390  System.Exception LabelTargetAlreadyDefined(System.Object p0)
  RVA=0x0ACD66C4  token=0x6000391  System.Exception LabelTargetUndefined(System.Object p0)
  RVA=0x0ACD519C  token=0x6000392  System.Exception ControlCannotLeaveFinally()
  RVA=0x0ACD5134  token=0x6000393  System.Exception ControlCannotLeaveFilterTest()
  RVA=0x0ACD4680  token=0x6000394  System.Exception AmbiguousJump(System.Object p0)
  RVA=0x0ACD50CC  token=0x6000395  System.Exception ControlCannotEnterTry()
  RVA=0x0ACD5064  token=0x6000396  System.Exception ControlCannotEnterExpression()
  RVA=0x0ACD6ECC  token=0x6000397  System.Exception NonLocalJumpWithValue(System.Object p0)
  RVA=0x0ACD628C  token=0x6000398  System.Exception InvalidLvalue(System.Linq.Expressions.ExpressionType p0)
  RVA=0x0ACD7758  token=0x6000399  System.Exception RethrowRequiresCatch()
  RVA=0x0ACD6D04  token=0x600039A  System.Exception MustRewriteToSameNode(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACD6C80  token=0x600039B  System.Exception MustRewriteChildToSameType(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACD6D88  token=0x600039C  System.Exception MustRewriteWithoutMethod(System.Object p0, System.Object p1)
  RVA=0x0ACD4ABC  token=0x600039D  System.Exception ArgumentOutOfRange(System.String paramName)
  RVA=0x0ACD6F38  token=0x600039E  System.Exception NotSupported()
  RVA=0x0ACD6E64  token=0x600039F  System.Exception NonAbstractConstructorRequired()
  RVA=0x0ACD6400  token=0x60003A0  System.Exception InvalidProgram()
  RVA=0x0ACD54A8  token=0x60003A1  System.Exception EnumerationIsDone()
  RVA=0x0ACD79C8  token=0x60003A2  System.Exception TypeContainsGenericParameters(System.Object p0, System.String paramName)
  RVA=0x0ACD7998  token=0x60003A3  System.Exception TypeContainsGenericParameters(System.Object p0, System.String paramName, System.Int32 index)
  RVA=0x0ACD7A44  token=0x60003A4  System.Exception TypeIsGeneric(System.Object p0, System.String paramName)
  RVA=0x0ACD7AC0  token=0x60003A5  System.Exception TypeIsGeneric(System.Object p0, System.String paramName, System.Int32 index)
  RVA=0x0ACD5D88  token=0x60003A6  System.Exception IncorrectNumberOfConstructorArguments()
  RVA=0x0ACD5838  token=0x60003A7  System.Exception ExpressionTypeDoesNotMatchMethodParameter(System.Object p0, System.Object p1, System.Object p2, System.String paramName)
  RVA=0x0ACD58CC  token=0x60003A8  System.Exception ExpressionTypeDoesNotMatchMethodParameter(System.Object p0, System.Object p1, System.Object p2, System.String paramName, System.Int32 index)
  RVA=0x0ACD5958  token=0x60003A9  System.Exception ExpressionTypeDoesNotMatchParameter(System.Object p0, System.Object p1, System.String paramName)
  RVA=0x0ACD591C  token=0x60003AA  System.Exception ExpressionTypeDoesNotMatchParameter(System.Object p0, System.Object p1, System.String paramName, System.Int32 index)
  RVA=0x0ACD5E58  token=0x60003AB  System.Exception IncorrectNumberOfLambdaArguments()
  RVA=0x0ACD5F28  token=0x60003AC  System.Exception IncorrectNumberOfMethodCallArguments(System.Object p0, System.String paramName)
  RVA=0x0ACD5704  token=0x60003AD  System.Exception ExpressionTypeDoesNotMatchConstructorParameter(System.Object p0, System.Object p1, System.String paramName)
  RVA=0x0ACD5788  token=0x60003AE  System.Exception ExpressionTypeDoesNotMatchConstructorParameter(System.Object p0, System.Object p1, System.String paramName, System.Int32 index)
  RVA=0x0ACD5510  token=0x60003AF  System.Exception ExpressionMustBeReadable(System.String paramName)
  RVA=0x0ACD5588  token=0x60003B0  System.Exception ExpressionMustBeReadable(System.String paramName, System.Int32 index)
  RVA=0x0ACD6214  token=0x60003B1  System.Exception InvalidArgumentValue(System.String paramName)
  RVA=0x0ACD6384  token=0x60003B2  System.Exception InvalidNullValue(System.Type type, System.String paramName)
  RVA=0x0ACD6450  token=0x60003B3  System.Exception InvalidTypeException(System.Object value, System.Type type, System.String paramName)
  RVA=0x0ACD5D10  token=0x60003B4  System.String GetParamName(System.String paramName, System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.ExpressionStringBuilder
TYPE:  sealed class
TOKEN: 0x2000084
SIZE:  0x20
EXTENDS: System.Linq.Expressions.ExpressionVisitor
FIELDS:
  private   readonly System.Text.StringBuilder       _out  // 0x10
  private           System.Collections.Generic.Dictionary<System.Object,System.Int32>_ids  // 0x18
METHODS:
  RVA=0x0ACDA680  token=0x60003B5  System.Void .ctor()
  RVA=0x09043210  token=0x60003B6  System.String ToString()
  RVA=0x0ACD8ADC  token=0x60003B7  System.Int32 GetLabelId(System.Linq.Expressions.LabelTarget label)
  RVA=0x0ACD8ADC  token=0x60003B8  System.Int32 GetParamId(System.Linq.Expressions.ParameterExpression p)
  RVA=0x0ACD89D4  token=0x60003B9  System.Int32 GetId(System.Object o)
  RVA=0x0ACD8C40  token=0x60003BA  System.Void Out(System.String s)
  RVA=0x0ACD8C60  token=0x60003BB  System.Void Out(System.Char c)
  RVA=0x0ACD8960  token=0x60003BC  System.String ExpressionToString(System.Linq.Expressions.Expression node)
  RVA=0x0ACD884C  token=0x60003BD  System.String CatchBlockToString(System.Linq.Expressions.CatchBlock node)
  RVA=-1  // generic def  token=0x60003BE  System.Void VisitExpressions(System.Char open, System.Collections.ObjectModel.ReadOnlyCollection<T> expressions, System.Char close)
  RVA=-1  // generic def  token=0x60003BF  System.Void VisitExpressions(System.Char open, System.Collections.ObjectModel.ReadOnlyCollection<T> expressions, System.Char close, System.String seperator)
  RVA=0x0ACD8C80  token=0x60003C0  System.Linq.Expressions.Expression VisitBinary(System.Linq.Expressions.BinaryExpression node)
  RVA=0x0ACDA11C  token=0x60003C1  System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node)
  RVA=-1  // generic def  token=0x60003C2  System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node)
  RVA=0x0ACD9564  token=0x60003C3  System.Linq.Expressions.Expression VisitConditional(System.Linq.Expressions.ConditionalExpression node)
  RVA=0x0ACD9634  token=0x60003C4  System.Linq.Expressions.Expression VisitConstant(System.Linq.Expressions.ConstantExpression node)
  RVA=0x0ACD8BB0  token=0x60003C5  System.Void OutMember(System.Linq.Expressions.Expression instance, System.Reflection.MemberInfo member)
  RVA=0x0ACD9E3C  token=0x60003C6  System.Linq.Expressions.Expression VisitMember(System.Linq.Expressions.MemberExpression node)
  RVA=0x0ACD9C88  token=0x60003C7  System.Linq.Expressions.Expression VisitInvocation(System.Linq.Expressions.InvocationExpression node)
  RVA=0x0ACD9E90  token=0x60003C8  System.Linq.Expressions.Expression VisitMethodCall(System.Linq.Expressions.MethodCallExpression node)
  RVA=0x0ACDA024  token=0x60003C9  System.Linq.Expressions.Expression VisitNewArray(System.Linq.Expressions.NewArrayExpression node)
  RVA=0x0ACDA228  token=0x60003CA  System.Linq.Expressions.Expression VisitTypeBinary(System.Linq.Expressions.TypeBinaryExpression node)
  RVA=0x0ACDA300  token=0x60003CB  System.Linq.Expressions.Expression VisitUnary(System.Linq.Expressions.UnaryExpression node)
  RVA=0x0ACD92D4  token=0x60003CC  System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node)
  RVA=0x0ACD977C  token=0x60003CD  System.Linq.Expressions.Expression VisitDefault(System.Linq.Expressions.DefaultExpression node)
  RVA=0x0ACD9D78  token=0x60003CE  System.Linq.Expressions.Expression VisitLabel(System.Linq.Expressions.LabelExpression node)
  RVA=0x0ACD99C4  token=0x60003CF  System.Linq.Expressions.Expression VisitGoto(System.Linq.Expressions.GotoExpression node)
  RVA=0x0ACD9DF0  token=0x60003D0  System.Linq.Expressions.Expression VisitLoop(System.Linq.Expressions.LoopExpression node)
  RVA=0x0ACD9488  token=0x60003D1  System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node)
  RVA=0x0ACDA1DC  token=0x60003D2  System.Linq.Expressions.Expression VisitTry(System.Linq.Expressions.TryExpression node)
  RVA=0x0ACD9B28  token=0x60003D3  System.Linq.Expressions.Expression VisitIndex(System.Linq.Expressions.IndexExpression node)
  RVA=0x0ACD9810  token=0x60003D4  System.Linq.Expressions.Expression VisitExtension(System.Linq.Expressions.Expression node)
  RVA=0x0ACD88D0  token=0x60003D5  System.Void DumpLabel(System.Linq.Expressions.LabelTarget target)
  RVA=0x0ACD8AE4  token=0x60003D6  System.Boolean IsBool(System.Linq.Expressions.Expression node)
END_CLASS

CLASS: System.Linq.Expressions.ExpressionType
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Linq.Expressions.ExpressionTypeAdd  // const
  public    static  System.Linq.Expressions.ExpressionTypeAddChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypeAnd  // const
  public    static  System.Linq.Expressions.ExpressionTypeAndAlso  // const
  public    static  System.Linq.Expressions.ExpressionTypeArrayLength  // const
  public    static  System.Linq.Expressions.ExpressionTypeArrayIndex  // const
  public    static  System.Linq.Expressions.ExpressionTypeCall  // const
  public    static  System.Linq.Expressions.ExpressionTypeCoalesce  // const
  public    static  System.Linq.Expressions.ExpressionTypeConditional  // const
  public    static  System.Linq.Expressions.ExpressionTypeConstant  // const
  public    static  System.Linq.Expressions.ExpressionTypeConvert  // const
  public    static  System.Linq.Expressions.ExpressionTypeConvertChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypeDivide  // const
  public    static  System.Linq.Expressions.ExpressionTypeEqual  // const
  public    static  System.Linq.Expressions.ExpressionTypeExclusiveOr  // const
  public    static  System.Linq.Expressions.ExpressionTypeGreaterThan  // const
  public    static  System.Linq.Expressions.ExpressionTypeGreaterThanOrEqual  // const
  public    static  System.Linq.Expressions.ExpressionTypeInvoke  // const
  public    static  System.Linq.Expressions.ExpressionTypeLambda  // const
  public    static  System.Linq.Expressions.ExpressionTypeLeftShift  // const
  public    static  System.Linq.Expressions.ExpressionTypeLessThan  // const
  public    static  System.Linq.Expressions.ExpressionTypeLessThanOrEqual  // const
  public    static  System.Linq.Expressions.ExpressionTypeListInit  // const
  public    static  System.Linq.Expressions.ExpressionTypeMemberAccess  // const
  public    static  System.Linq.Expressions.ExpressionTypeMemberInit  // const
  public    static  System.Linq.Expressions.ExpressionTypeModulo  // const
  public    static  System.Linq.Expressions.ExpressionTypeMultiply  // const
  public    static  System.Linq.Expressions.ExpressionTypeMultiplyChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypeNegate  // const
  public    static  System.Linq.Expressions.ExpressionTypeUnaryPlus  // const
  public    static  System.Linq.Expressions.ExpressionTypeNegateChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypeNew  // const
  public    static  System.Linq.Expressions.ExpressionTypeNewArrayInit  // const
  public    static  System.Linq.Expressions.ExpressionTypeNewArrayBounds  // const
  public    static  System.Linq.Expressions.ExpressionTypeNot  // const
  public    static  System.Linq.Expressions.ExpressionTypeNotEqual  // const
  public    static  System.Linq.Expressions.ExpressionTypeOr  // const
  public    static  System.Linq.Expressions.ExpressionTypeOrElse  // const
  public    static  System.Linq.Expressions.ExpressionTypeParameter  // const
  public    static  System.Linq.Expressions.ExpressionTypePower  // const
  public    static  System.Linq.Expressions.ExpressionTypeQuote  // const
  public    static  System.Linq.Expressions.ExpressionTypeRightShift  // const
  public    static  System.Linq.Expressions.ExpressionTypeSubtract  // const
  public    static  System.Linq.Expressions.ExpressionTypeSubtractChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypeTypeAs  // const
  public    static  System.Linq.Expressions.ExpressionTypeTypeIs  // const
  public    static  System.Linq.Expressions.ExpressionTypeAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeBlock  // const
  public    static  System.Linq.Expressions.ExpressionTypeDebugInfo  // const
  public    static  System.Linq.Expressions.ExpressionTypeDecrement  // const
  public    static  System.Linq.Expressions.ExpressionTypeDynamic  // const
  public    static  System.Linq.Expressions.ExpressionTypeDefault  // const
  public    static  System.Linq.Expressions.ExpressionTypeExtension  // const
  public    static  System.Linq.Expressions.ExpressionTypeGoto  // const
  public    static  System.Linq.Expressions.ExpressionTypeIncrement  // const
  public    static  System.Linq.Expressions.ExpressionTypeIndex  // const
  public    static  System.Linq.Expressions.ExpressionTypeLabel  // const
  public    static  System.Linq.Expressions.ExpressionTypeRuntimeVariables  // const
  public    static  System.Linq.Expressions.ExpressionTypeLoop  // const
  public    static  System.Linq.Expressions.ExpressionTypeSwitch  // const
  public    static  System.Linq.Expressions.ExpressionTypeThrow  // const
  public    static  System.Linq.Expressions.ExpressionTypeTry  // const
  public    static  System.Linq.Expressions.ExpressionTypeUnbox  // const
  public    static  System.Linq.Expressions.ExpressionTypeAddAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeAndAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeDivideAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeExclusiveOrAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeLeftShiftAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeModuloAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeMultiplyAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeOrAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypePowerAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeRightShiftAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeSubtractAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeAddAssignChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypeMultiplyAssignChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypeSubtractAssignChecked  // const
  public    static  System.Linq.Expressions.ExpressionTypePreIncrementAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypePreDecrementAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypePostIncrementAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypePostDecrementAssign  // const
  public    static  System.Linq.Expressions.ExpressionTypeTypeEqual  // const
  public    static  System.Linq.Expressions.ExpressionTypeOnesComplement  // const
  public    static  System.Linq.Expressions.ExpressionTypeIsTrue  // const
  public    static  System.Linq.Expressions.ExpressionTypeIsFalse  // const
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.ExpressionVisitor
TYPE:  abstract class
TOKEN: 0x2000086
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60003D7  System.Void .ctor()
  RVA=0x0ACDB150  token=0x60003D8  System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression node)
  RVA=0x0ACDB198  token=0x60003D9  System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> Visit(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> nodes)
  RVA=0x0ACDA9F4  token=0x60003DA  System.Linq.Expressions.Expression[] VisitArguments(System.Linq.Expressions.IArgumentProvider nodes)
  RVA=0x0ACDAFC0  token=0x60003DB  System.Linq.Expressions.ParameterExpression[] VisitParameters(System.Linq.Expressions.IParameterProvider nodes, System.String callerName)
  RVA=-1  // generic def  token=0x60003DC  System.Collections.ObjectModel.ReadOnlyCollection<T> Visit(System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, System.Func<T,T> elementVisitor)
  RVA=-1  // generic def  token=0x60003DD  T VisitAndConvert(T node, System.String callerName)
  RVA=-1  // generic def  token=0x60003DE  System.Collections.ObjectModel.ReadOnlyCollection<T> VisitAndConvert(System.Collections.ObjectModel.ReadOnlyCollection<T> nodes, System.String callerName)
  RVA=0x0ACDA9FC  token=0x60003DF  System.Linq.Expressions.Expression VisitBinary(System.Linq.Expressions.BinaryExpression node)
  RVA=0x0ACDAAC8  token=0x60003E0  System.Linq.Expressions.Expression VisitBlock(System.Linq.Expressions.BlockExpression node)
  RVA=0x0ACDAC38  token=0x60003E1  System.Linq.Expressions.Expression VisitConditional(System.Linq.Expressions.ConditionalExpression node)
  RVA=0x04D88C30  token=0x60003E2  System.Linq.Expressions.Expression VisitConstant(System.Linq.Expressions.ConstantExpression node)
  RVA=0x04D88C30  token=0x60003E3  System.Linq.Expressions.Expression VisitDefault(System.Linq.Expressions.DefaultExpression node)
  RVA=0x0ACDACC4  token=0x60003E4  System.Linq.Expressions.Expression VisitExtension(System.Linq.Expressions.Expression node)
  RVA=0x0ACDACE8  token=0x60003E5  System.Linq.Expressions.Expression VisitGoto(System.Linq.Expressions.GotoExpression node)
  RVA=0x0ACDAD9C  token=0x60003E6  System.Linq.Expressions.Expression VisitInvocation(System.Linq.Expressions.InvocationExpression node)
  RVA=0x04D88C30  token=0x60003E7  System.Linq.Expressions.LabelTarget VisitLabelTarget(System.Linq.Expressions.LabelTarget node)
  RVA=0x0ACDAE20  token=0x60003E8  System.Linq.Expressions.Expression VisitLabel(System.Linq.Expressions.LabelExpression node)
  RVA=-1  // generic def  token=0x60003E9  System.Linq.Expressions.Expression VisitLambda(System.Linq.Expressions.Expression<T> node)
  RVA=0x0ACDAE64  token=0x60003EA  System.Linq.Expressions.Expression VisitLoop(System.Linq.Expressions.LoopExpression node)
  RVA=0x0ACDAEBC  token=0x60003EB  System.Linq.Expressions.Expression VisitMember(System.Linq.Expressions.MemberExpression node)
  RVA=0x0ACDAD2C  token=0x60003EC  System.Linq.Expressions.Expression VisitIndex(System.Linq.Expressions.IndexExpression node)
  RVA=0x0ACDAEF0  token=0x60003ED  System.Linq.Expressions.Expression VisitMethodCall(System.Linq.Expressions.MethodCallExpression node)
  RVA=0x0ACDAF8C  token=0x60003EE  System.Linq.Expressions.Expression VisitNewArray(System.Linq.Expressions.NewArrayExpression node)
  RVA=0x04D88C30  token=0x60003EF  System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node)
  RVA=0x0ACDAB80  token=0x60003F0  System.Linq.Expressions.CatchBlock VisitCatchBlock(System.Linq.Expressions.CatchBlock node)
  RVA=0x0ACDAFC8  token=0x60003F1  System.Linq.Expressions.Expression VisitTry(System.Linq.Expressions.TryExpression node)
  RVA=0x0ACDB0D8  token=0x60003F2  System.Linq.Expressions.Expression VisitTypeBinary(System.Linq.Expressions.TypeBinaryExpression node)
  RVA=0x0ACDB10C  token=0x60003F3  System.Linq.Expressions.Expression VisitUnary(System.Linq.Expressions.UnaryExpression node)
  RVA=0x0ACDA904  token=0x60003F4  System.Linq.Expressions.UnaryExpression ValidateUnary(System.Linq.Expressions.UnaryExpression before, System.Linq.Expressions.UnaryExpression after)
  RVA=0x0ACDA6E4  token=0x60003F5  System.Linq.Expressions.BinaryExpression ValidateBinary(System.Linq.Expressions.BinaryExpression before, System.Linq.Expressions.BinaryExpression after)
  RVA=0x0ACDA848  token=0x60003F6  System.Void ValidateChildType(System.Type before, System.Type after, System.String methodName)
END_CLASS

CLASS: System.Linq.Expressions.GotoExpressionKind
TYPE:  sealed struct
TOKEN: 0x2000087
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Linq.Expressions.GotoExpressionKindGoto  // const
  public    static  System.Linq.Expressions.GotoExpressionKindReturn  // const
  public    static  System.Linq.Expressions.GotoExpressionKindBreak  // const
  public    static  System.Linq.Expressions.GotoExpressionKindContinue  // const
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.GotoExpression
TYPE:  sealed class
TOKEN: 0x2000088
SIZE:  0x30
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<Value>k__BackingField  // 0x18
  private   readonly System.Linq.Expressions.LabelTarget<Target>k__BackingField  // 0x20
  private   readonly System.Linq.Expressions.GotoExpressionKind<Kind>k__BackingField  // 0x28
PROPERTIES:
  Type  get=0x02B2ECC0
  NodeType  get=0x04DA9F80
  Value  get=0x0385B100
  Target  get=0x04D862C0
  Kind  get=0x011F2A90
METHODS:
  RVA=0x0ACDB644  token=0x60003F7  System.Void .ctor(System.Linq.Expressions.GotoExpressionKind kind, System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value, System.Type type)
  RVA=0x0ACDB568  token=0x60003FD  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDB5B0  token=0x60003FE  System.Linq.Expressions.GotoExpression Update(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression value)
END_CLASS

CLASS: System.Linq.Expressions.IArgumentProvider
TYPE:  interface
TOKEN: 0x2000089
FIELDS:
PROPERTIES:
  ArgumentCount  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60003FF  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.IParameterProvider
TYPE:  interface
TOKEN: 0x200008A
FIELDS:
PROPERTIES:
  ParameterCount  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000401  System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.IndexExpression
TYPE:  sealed class
TOKEN: 0x200008B
SIZE:  0x28
EXTENDS: System.Linq.Expressions.Expression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression>_arguments  // 0x10
  private   readonly System.Linq.Expressions.Expression<Object>k__BackingField  // 0x18
  private   readonly System.Reflection.PropertyInfo  <Indexer>k__BackingField  // 0x20
PROPERTIES:
  NodeType  get=0x04DA9FB0
  Type  get=0x0ACDBB44
  Object  get=0x0385B100
  Indexer  get=0x04D862C0
  ArgumentCount  get=0x0ACDBAFC
METHODS:
  RVA=0x0ACDBA6C  token=0x6000403  System.Void .ctor(System.Linq.Expressions.Expression instance, System.Reflection.PropertyInfo indexer, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> arguments)
  RVA=0x0ACDB9A0  token=0x6000408  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDB958  token=0x600040A  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDB9F4  token=0x600040B  System.Linq.Expressions.Expression Rewrite(System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpression
TYPE:  class
TOKEN: 0x200008C
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<Expression>k__BackingField  // 0x18
PROPERTIES:
  Type  get=0x02B2ECC0
  NodeType  get=0x04DA5E90
  Expression  get=0x0385B100
  ArgumentCount  get=0x0ACDD0B4
METHODS:
  RVA=0x0ACDD048  token=0x600040C  System.Void .ctor(System.Linq.Expressions.Expression expression, System.Type returnType)
  RVA=0x0ACDCFF8  token=0x6000410  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDCFB0  token=0x6000412  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDD020  token=0x6000413  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpressionN
TYPE:  sealed class
TOKEN: 0x200008D
SIZE:  0x28
EXTENDS: System.Linq.Expressions.InvocationExpression
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression>_arguments  // 0x20
PROPERTIES:
  ArgumentCount  get=0x0ACDCF68
METHODS:
  RVA=0x0ACDCF30  token=0x6000414  System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> arguments, System.Type returnType)
  RVA=0x0ACDCE74  token=0x6000415  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDCEC8  token=0x6000417  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpression0
TYPE:  sealed class
TOKEN: 0x200008E
SIZE:  0x20
EXTENDS: System.Linq.Expressions.InvocationExpression
FIELDS:
PROPERTIES:
  ArgumentCount  get=0x011EC580
METHODS:
  RVA=0x0ACDC514  token=0x6000418  System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType)
  RVA=0x0ACDC46C  token=0x6000419  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDC4C8  token=0x600041B  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpression1
TYPE:  sealed class
TOKEN: 0x200008F
SIZE:  0x28
EXTENDS: System.Linq.Expressions.InvocationExpression
FIELDS:
  private           System.Object                   _arg0  // 0x20
PROPERTIES:
  ArgumentCount  get=0x04D85B80
METHODS:
  RVA=0x0ACDC650  token=0x600041C  System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0)
  RVA=0x0ACDC51C  token=0x600041D  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDC5BC  token=0x600041F  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpression2
TYPE:  sealed class
TOKEN: 0x2000090
SIZE:  0x30
EXTENDS: System.Linq.Expressions.InvocationExpression
FIELDS:
  private           System.Object                   _arg0  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x28
PROPERTIES:
  ArgumentCount  get=0x02BBE540
METHODS:
  RVA=0x0ACDC7DC  token=0x6000420  System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACDC680  token=0x6000421  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDC72C  token=0x6000423  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpression3
TYPE:  sealed class
TOKEN: 0x2000091
SIZE:  0x38
EXTENDS: System.Linq.Expressions.InvocationExpression
FIELDS:
  private           System.Object                   _arg0  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x28
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x30
PROPERTIES:
  ArgumentCount  get=0x04D8C9E0
METHODS:
  RVA=0x0ACDC9A4  token=0x6000424  System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACDC820  token=0x6000425  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDC8D4  token=0x6000427  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpression4
TYPE:  sealed class
TOKEN: 0x2000092
SIZE:  0x40
EXTENDS: System.Linq.Expressions.InvocationExpression
FIELDS:
  private           System.Object                   _arg0  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x28
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x30
  private   readonly System.Linq.Expressions.Expression_arg3  // 0x38
PROPERTIES:
  ArgumentCount  get=0x04D882B0
METHODS:
  RVA=0x0ACDCBA8  token=0x6000428  System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3)
  RVA=0x0ACDC9F8  token=0x6000429  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDCAB8  token=0x600042B  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.InvocationExpression5
TYPE:  sealed class
TOKEN: 0x2000093
SIZE:  0x48
EXTENDS: System.Linq.Expressions.InvocationExpression
FIELDS:
  private           System.Object                   _arg0  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x28
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x30
  private   readonly System.Linq.Expressions.Expression_arg3  // 0x38
  private   readonly System.Linq.Expressions.Expression_arg4  // 0x40
PROPERTIES:
  ArgumentCount  get=0x04D9EC50
METHODS:
  RVA=0x0ACDCDFC  token=0x600042C  System.Void .ctor(System.Linq.Expressions.Expression lambda, System.Type returnType, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4)
  RVA=0x0ACDCC10  token=0x600042D  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDCCE0  token=0x600042F  System.Linq.Expressions.InvocationExpression Rewrite(System.Linq.Expressions.Expression lambda, System.Linq.Expressions.Expression[] arguments)
END_CLASS

CLASS: System.Linq.Expressions.LabelExpression
TYPE:  sealed class
TOKEN: 0x2000094
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.LabelTarget<Target>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<DefaultValue>k__BackingField  // 0x18
PROPERTIES:
  Type  get=0x0861D7BC
  NodeType  get=0x04DA9FC0
  Target  get=0x02B2ECC0
  DefaultValue  get=0x0385B100
METHODS:
  RVA=0x0ACDD194  token=0x6000430  System.Void .ctor(System.Linq.Expressions.LabelTarget label, System.Linq.Expressions.Expression defaultValue)
  RVA=0x0ACDD0DC  token=0x6000435  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDD124  token=0x6000436  System.Linq.Expressions.LabelExpression Update(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Expression defaultValue)
END_CLASS

CLASS: System.Linq.Expressions.LabelTarget
TYPE:  sealed class
TOKEN: 0x2000095
SIZE:  0x20
FIELDS:
  private   readonly System.String                   <Name>k__BackingField  // 0x10
  private   readonly System.Type                     <Type>k__BackingField  // 0x18
PROPERTIES:
  Name  get=0x02B2ECC0
  Type  get=0x0385B100
METHODS:
  RVA=0x03739160  token=0x6000437  System.Void .ctor(System.Type type, System.String name)
  RVA=0x0ACDD200  token=0x600043A  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.LambdaExpression
TYPE:  abstract class
TOKEN: 0x2000096
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
IMPLEMENTS: System.Linq.Expressions.IParameterProvider
FIELDS:
  private   readonly System.Linq.Expressions.Expression_body  // 0x10
PROPERTIES:
  Type  get=0x0ACDD390
  TypeCore  get=-1  // abstract
  PublicType  get=-1  // abstract
  NodeType  get=0x04DA3EE0
  Name  get=0x0ACDD2E0
  NameCore  get=0x011EC580
  Body  get=0x02B2ECC0
  ReturnType  get=0x0ACDD318
  TailCall  get=0x0ACDD380
  TailCallCore  get=0x012081B0
  System.Linq.Expressions.IParameterProvider.ParameterCount  get=0x0ACDD278
  ParameterCount  get=0x0ACDD2F0
METHODS:
  RVA=0x0ACDD288  token=0x600043B  System.Void .ctor(System.Linq.Expressions.Expression body)
  RVA=0x0ACDD26C  token=0x6000446  System.Linq.Expressions.ParameterExpression System.Linq.Expressions.IParameterProvider.GetParameter(System.Int32 index)
  RVA=0x0ACDD244  token=0x6000447  System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.Expression`1
TYPE:  class
TOKEN: 0x2000097
EXTENDS: System.Linq.Expressions.LambdaExpression
FIELDS:
PROPERTIES:
  TypeCore  get=-1  // not resolved
  PublicType  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600044A  System.Void .ctor(System.Linq.Expressions.Expression body)
  RVA=-1  // not resolved  token=0x600044D  TDelegate Compile()
  RVA=-1  // not resolved  token=0x600044E  TDelegate Compile(System.Boolean preferInterpretation)
  RVA=-1  // not resolved  token=0x600044F  System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
  RVA=-1  // not resolved  token=0x6000450  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
END_CLASS

CLASS: System.Linq.Expressions.ExpressionCreator`1
TYPE:  static class
TOKEN: 0x2000098
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000451  System.Linq.Expressions.LambdaExpression CreateExpressionFunc(System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters)
END_CLASS

CLASS: System.Linq.Expressions.Expression0`1
TYPE:  sealed class
TOKEN: 0x2000099
EXTENDS: System.Linq.Expressions.Expression`1
FIELDS:
PROPERTIES:
  ParameterCount  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000452  System.Void .ctor(System.Linq.Expressions.Expression body)
  RVA=-1  // not resolved  token=0x6000454  System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000455  System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
END_CLASS

CLASS: System.Linq.Expressions.Expression1`1
TYPE:  sealed class
TOKEN: 0x200009A
EXTENDS: System.Linq.Expressions.Expression`1
FIELDS:
  private           System.Object                   _par0  // 0x0
PROPERTIES:
  ParameterCount  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000456  System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression par0)
  RVA=-1  // not resolved  token=0x6000458  System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000459  System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
END_CLASS

CLASS: System.Linq.Expressions.Expression2`1
TYPE:  sealed class
TOKEN: 0x200009B
EXTENDS: System.Linq.Expressions.Expression`1
FIELDS:
  private           System.Object                   _par0  // 0x0
  private   readonly System.Linq.Expressions.ParameterExpression_par1  // 0x0
PROPERTIES:
  ParameterCount  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600045A  System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression par0, System.Linq.Expressions.ParameterExpression par1)
  RVA=-1  // not resolved  token=0x600045C  System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index)
  RVA=-1  // not resolved  token=0x600045D  System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
END_CLASS

CLASS: System.Linq.Expressions.Expression3`1
TYPE:  sealed class
TOKEN: 0x200009C
EXTENDS: System.Linq.Expressions.Expression`1
FIELDS:
  private           System.Object                   _par0  // 0x0
  private   readonly System.Linq.Expressions.ParameterExpression_par1  // 0x0
  private   readonly System.Linq.Expressions.ParameterExpression_par2  // 0x0
PROPERTIES:
  ParameterCount  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600045E  System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression par0, System.Linq.Expressions.ParameterExpression par1, System.Linq.Expressions.ParameterExpression par2)
  RVA=-1  // not resolved  token=0x6000460  System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000461  System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
END_CLASS

CLASS: System.Linq.Expressions.ExpressionN`1
TYPE:  class
TOKEN: 0x200009D
EXTENDS: System.Linq.Expressions.Expression`1
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression>_parameters  // 0x0
PROPERTIES:
  ParameterCount  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000462  System.Void .ctor(System.Linq.Expressions.Expression body, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> parameters)
  RVA=-1  // not resolved  token=0x6000464  System.Linq.Expressions.ParameterExpression GetParameter(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000465  System.Linq.Expressions.Expression<TDelegate> Rewrite(System.Linq.Expressions.Expression body, System.Linq.Expressions.ParameterExpression[] parameters)
END_CLASS

CLASS: System.Linq.Expressions.FullExpression`1
TYPE:  sealed class
TOKEN: 0x200009E
EXTENDS: System.Linq.Expressions.ExpressionN`1
FIELDS:
  private   readonly System.String                   <NameCore>k__BackingField  // 0x0
  private   readonly System.Boolean                  <TailCallCore>k__BackingField  // 0x0
PROPERTIES:
  NameCore  get=-1  // not resolved
  TailCallCore  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000466  System.Void .ctor(System.Linq.Expressions.Expression body, System.String name, System.Boolean tailCall, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.ParameterExpression> parameters)
END_CLASS

CLASS: System.Linq.Expressions.ListInitExpression
TYPE:  sealed class
TOKEN: 0x200009F
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.NewExpression<NewExpression>k__BackingField  // 0x10
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit><Initializers>k__BackingField  // 0x18
PROPERTIES:
  NewExpression  get=0x02B2ECC0
  Initializers  get=0x0385B100
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.LoopExpression
TYPE:  sealed class
TOKEN: 0x20000A0
SIZE:  0x28
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.Expression<Body>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.LabelTarget<BreakLabel>k__BackingField  // 0x18
  private   readonly System.Linq.Expressions.LabelTarget<ContinueLabel>k__BackingField  // 0x20
PROPERTIES:
  Type  get=0x0ACDD8D0
  NodeType  get=0x04DA4DD0
  Body  get=0x02B2ECC0
  BreakLabel  get=0x0385B100
  ContinueLabel  get=0x04D862C0
METHODS:
  RVA=0x0ACDD84C  token=0x600046B  System.Void .ctor(System.Linq.Expressions.Expression body, System.Linq.Expressions.LabelTarget break, System.Linq.Expressions.LabelTarget continue)
  RVA=0x0ACDD77C  token=0x6000471  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDD7C4  token=0x6000472  System.Linq.Expressions.LoopExpression Update(System.Linq.Expressions.LabelTarget breakLabel, System.Linq.Expressions.LabelTarget continueLabel, System.Linq.Expressions.Expression body)
END_CLASS

CLASS: System.Linq.Expressions.MemberAssignment
TYPE:  sealed class
TOKEN: 0x20000A1
SIZE:  0x28
EXTENDS: System.Linq.Expressions.MemberBinding
FIELDS:
  private   readonly System.Linq.Expressions.Expression_expression  // 0x20
PROPERTIES:
  Expression  get=0x04D862C0
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.MemberBindingType
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Linq.Expressions.MemberBindingTypeAssignment  // const
  public    static  System.Linq.Expressions.MemberBindingTypeMemberBinding  // const
  public    static  System.Linq.Expressions.MemberBindingTypeListBinding  // const
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.MemberBinding
TYPE:  abstract class
TOKEN: 0x20000A3
SIZE:  0x20
FIELDS:
  private   readonly System.Linq.Expressions.MemberBindingType<BindingType>k__BackingField  // 0x10
  private   readonly System.Reflection.MemberInfo    <Member>k__BackingField  // 0x18
PROPERTIES:
  BindingType  get=0x02B2E2D0
  Member  get=0x0385B100
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.MemberExpression
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.Expression<Expression>k__BackingField  // 0x10
PROPERTIES:
  Member  get=0x0ACDDB58
  Expression  get=0x02B2ECC0
  NodeType  get=0x04DA4C00
METHODS:
  RVA=0x0ACDDB00  token=0x6000478  System.Void .ctor(System.Linq.Expressions.Expression expression)
  RVA=0x0ACDDA24  token=0x6000479  System.Linq.Expressions.PropertyExpression Make(System.Linq.Expressions.Expression expression, System.Reflection.PropertyInfo property)
  RVA=0x0ACDD9B4  token=0x600047A  System.Linq.Expressions.FieldExpression Make(System.Linq.Expressions.Expression expression, System.Reflection.FieldInfo field)
  RVA=0x0ACDD98C  token=0x600047C  System.Reflection.MemberInfo GetMember()
  RVA=0x0ACDD944  token=0x600047D  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDDA94  token=0x600047E  System.Linq.Expressions.MemberExpression Update(System.Linq.Expressions.Expression expression)
END_CLASS

CLASS: System.Linq.Expressions.FieldExpression
TYPE:  sealed class
TOKEN: 0x20000A5
SIZE:  0x20
EXTENDS: System.Linq.Expressions.MemberExpression
FIELDS:
  private   readonly System.Reflection.FieldInfo     _field  // 0x18
PROPERTIES:
  Type  get=0x0ACDB3C0
METHODS:
  RVA=0x0ACDB390  token=0x600047F  System.Void .ctor(System.Linq.Expressions.Expression expression, System.Reflection.FieldInfo member)
  RVA=0x0385B100  token=0x6000480  System.Reflection.MemberInfo GetMember()
END_CLASS

CLASS: System.Linq.Expressions.PropertyExpression
TYPE:  sealed class
TOKEN: 0x20000A6
SIZE:  0x20
EXTENDS: System.Linq.Expressions.MemberExpression
FIELDS:
  private   readonly System.Reflection.PropertyInfo  _property  // 0x18
PROPERTIES:
  Type  get=0x0ACDFA08
METHODS:
  RVA=0x0ACDB390  token=0x6000482  System.Void .ctor(System.Linq.Expressions.Expression expression, System.Reflection.PropertyInfo member)
  RVA=0x0385B100  token=0x6000483  System.Reflection.MemberInfo GetMember()
END_CLASS

CLASS: System.Linq.Expressions.MemberInitExpression
TYPE:  sealed class
TOKEN: 0x20000A7
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.NewExpression<NewExpression>k__BackingField  // 0x10
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding><Bindings>k__BackingField  // 0x18
PROPERTIES:
  NewExpression  get=0x02B2ECC0
  Bindings  get=0x0385B100
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.MemberListBinding
TYPE:  sealed class
TOKEN: 0x20000A8
SIZE:  0x28
EXTENDS: System.Linq.Expressions.MemberBinding
FIELDS:
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit><Initializers>k__BackingField  // 0x20
PROPERTIES:
  Initializers  get=0x04D862C0
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.MemberMemberBinding
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x28
EXTENDS: System.Linq.Expressions.MemberBinding
FIELDS:
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding><Bindings>k__BackingField  // 0x20
PROPERTIES:
  Bindings  get=0x04D862C0
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpression
TYPE:  class
TOKEN: 0x20000AA
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private   readonly System.Reflection.MethodInfo    <Method>k__BackingField  // 0x10
PROPERTIES:
  NodeType  get=0x04D9EC60
  Type  get=0x0ACDE864
  Method  get=0x02B2ECC0
  Object  get=0x0ACDE854
  ArgumentCount  get=0x0ACDE82C
METHODS:
  RVA=0x0ACDE7D4  token=0x6000489  System.Void .ctor(System.Reflection.MethodInfo method)
  RVA=0x011EC580  token=0x600048A  System.Linq.Expressions.Expression GetInstance()
  RVA=0x0ACDE73C  token=0x600048F  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDE7AC  token=0x6000490  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
  RVA=0x0ACDE784  token=0x6000491  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.InstanceMethodCallExpression
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x20
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private   readonly System.Linq.Expressions.Expression_instance  // 0x18
METHODS:
  RVA=0x0ACDC43C  token=0x6000493  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance)
  RVA=0x0385B100  token=0x6000494  System.Linq.Expressions.Expression GetInstance()
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpressionN
TYPE:  sealed class
TOKEN: 0x20000AC
SIZE:  0x20
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression>_arguments  // 0x18
PROPERTIES:
  ArgumentCount  get=0x0ACDE6F4
METHODS:
  RVA=0x0ACDC43C  token=0x6000495  System.Void .ctor(System.Reflection.MethodInfo method, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
  RVA=0x0ACDE638  token=0x6000496  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDE68C  token=0x6000498  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.InstanceMethodCallExpressionN
TYPE:  sealed class
TOKEN: 0x20000AD
SIZE:  0x28
EXTENDS: System.Linq.Expressions.InstanceMethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression>_arguments  // 0x20
PROPERTIES:
  ArgumentCount  get=0x0ACDC3F4
METHODS:
  RVA=0x0ACDBF18  token=0x6000499  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
  RVA=0x0ACDC328  token=0x600049A  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDC37C  token=0x600049C  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpression0
TYPE:  sealed class
TOKEN: 0x20000AE
SIZE:  0x18
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
PROPERTIES:
  ArgumentCount  get=0x011EC580
METHODS:
  RVA=0x0ACDDC34  token=0x600049D  System.Void .ctor(System.Reflection.MethodInfo method)
  RVA=0x0ACDDB88  token=0x600049E  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDDBE4  token=0x60004A0  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpression1
TYPE:  sealed class
TOKEN: 0x20000AF
SIZE:  0x20
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x18
PROPERTIES:
  ArgumentCount  get=0x04D85B80
METHODS:
  RVA=0x0ACDC43C  token=0x60004A1  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0)
  RVA=0x0ACDDC3C  token=0x60004A2  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDDCDC  token=0x60004A4  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpression2
TYPE:  sealed class
TOKEN: 0x20000B0
SIZE:  0x28
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x18
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x20
PROPERTIES:
  ArgumentCount  get=0x02BBE540
METHODS:
  RVA=0x0ACDDEFC  token=0x60004A5  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACDDD80  token=0x60004A6  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDDE2C  token=0x60004A8  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpression3
TYPE:  sealed class
TOKEN: 0x20000B1
SIZE:  0x30
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x18
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x28
PROPERTIES:
  ArgumentCount  get=0x04D8C9E0
METHODS:
  RVA=0x0ACDE0F8  token=0x60004A9  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACDDF48  token=0x60004AA  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDDFFC  token=0x60004AC  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpression4
TYPE:  sealed class
TOKEN: 0x20000B2
SIZE:  0x38
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x18
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x28
  private   readonly System.Linq.Expressions.Expression_arg3  // 0x30
PROPERTIES:
  ArgumentCount  get=0x04D882B0
METHODS:
  RVA=0x0ACDE330  token=0x60004AD  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3)
  RVA=0x0ACDE154  token=0x60004AE  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDE214  token=0x60004B0  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.MethodCallExpression5
TYPE:  sealed class
TOKEN: 0x20000B3
SIZE:  0x40
EXTENDS: System.Linq.Expressions.MethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x18
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x28
  private   readonly System.Linq.Expressions.Expression_arg3  // 0x30
  private   readonly System.Linq.Expressions.Expression_arg4  // 0x38
PROPERTIES:
  ArgumentCount  get=0x04D9EC50
METHODS:
  RVA=0x0ACDE5B8  token=0x60004B1  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2, System.Linq.Expressions.Expression arg3, System.Linq.Expressions.Expression arg4)
  RVA=0x0ACDE3A0  token=0x60004B2  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDE470  token=0x60004B4  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.InstanceMethodCallExpression0
TYPE:  sealed class
TOKEN: 0x20000B4
SIZE:  0x20
EXTENDS: System.Linq.Expressions.InstanceMethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
PROPERTIES:
  ArgumentCount  get=0x011EC580
METHODS:
  RVA=0x0ACDBDBC  token=0x60004B5  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance)
  RVA=0x0ACDBD0C  token=0x60004B6  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDBD68  token=0x60004B8  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.InstanceMethodCallExpression1
TYPE:  sealed class
TOKEN: 0x20000B5
SIZE:  0x28
EXTENDS: System.Linq.Expressions.InstanceMethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x20
PROPERTIES:
  ArgumentCount  get=0x04D85B80
METHODS:
  RVA=0x0ACDBF18  token=0x60004B9  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression arg0)
  RVA=0x0ACDBDC4  token=0x60004BA  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDBE64  token=0x60004BC  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.InstanceMethodCallExpression2
TYPE:  sealed class
TOKEN: 0x20000B6
SIZE:  0x30
EXTENDS: System.Linq.Expressions.InstanceMethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x28
PROPERTIES:
  ArgumentCount  get=0x02BBE540
METHODS:
  RVA=0x0ACDC0D8  token=0x60004BD  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1)
  RVA=0x0ACDBF48  token=0x60004BE  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDBFF4  token=0x60004C0  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.InstanceMethodCallExpression3
TYPE:  sealed class
TOKEN: 0x20000B7
SIZE:  0x38
EXTENDS: System.Linq.Expressions.InstanceMethodCallExpression
IMPLEMENTS: System.Linq.Expressions.IArgumentProvider
FIELDS:
  private           System.Object                   _arg0  // 0x20
  private   readonly System.Linq.Expressions.Expression_arg1  // 0x28
  private   readonly System.Linq.Expressions.Expression_arg2  // 0x30
PROPERTIES:
  ArgumentCount  get=0x04D8C9E0
METHODS:
  RVA=0x0ACDC2D4  token=0x60004C1  System.Void .ctor(System.Reflection.MethodInfo method, System.Linq.Expressions.Expression instance, System.Linq.Expressions.Expression arg0, System.Linq.Expressions.Expression arg1, System.Linq.Expressions.Expression arg2)
  RVA=0x0ACDC11C  token=0x60004C2  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
  RVA=0x0ACDC1D0  token=0x60004C4  System.Linq.Expressions.MethodCallExpression Rewrite(System.Linq.Expressions.Expression instance, System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression> args)
END_CLASS

CLASS: System.Linq.Expressions.NewArrayExpression
TYPE:  class
TOKEN: 0x20000B8
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression><Expressions>k__BackingField  // 0x18
PROPERTIES:
  Type  get=0x02B2ECC0
  Expressions  get=0x0385B100
METHODS:
  RVA=0x0ACDF0A8  token=0x60004C5  System.Void .ctor(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACDEEBC  token=0x60004C6  System.Linq.Expressions.NewArrayExpression Make(System.Linq.Expressions.ExpressionType nodeType, System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions)
  RVA=0x0ACDEE74  token=0x60004C9  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACDEF48  token=0x60004CA  System.Linq.Expressions.NewArrayExpression Update(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> expressions)
END_CLASS

CLASS: System.Linq.Expressions.NewArrayInitExpression
TYPE:  sealed class
TOKEN: 0x20000B9
SIZE:  0x20
EXTENDS: System.Linq.Expressions.NewArrayExpression
FIELDS:
PROPERTIES:
  NodeType  get=0x04DA43E0
METHODS:
  RVA=0x0ACDED34  token=0x60004CB  System.Void .ctor(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions)
END_CLASS

CLASS: System.Linq.Expressions.NewArrayBoundsExpression
TYPE:  sealed class
TOKEN: 0x20000BA
SIZE:  0x20
EXTENDS: System.Linq.Expressions.NewArrayExpression
FIELDS:
PROPERTIES:
  NodeType  get=0x04DA7800
METHODS:
  RVA=0x0ACDED34  token=0x60004CD  System.Void .ctor(System.Type type, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> expressions)
END_CLASS

CLASS: System.Linq.Expressions.NewExpression
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private           System.Collections.Generic.IReadOnlyList<System.Linq.Expressions.Expression>_arguments  // 0x10
  private   readonly System.Reflection.ConstructorInfo<Constructor>k__BackingField  // 0x18
PROPERTIES:
  Constructor  get=0x0385B100
METHODS:
  RVA=0x0ACDF280  token=0x60004D0  System.Linq.Expressions.Expression GetArgument(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.ParameterExpression
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.String                   <Name>k__BackingField  // 0x10
PROPERTIES:
  Type  get=0x0ACDF9B8
  NodeType  get=0x04DA9FD0
  Name  get=0x02B2ECC0
  IsByRef  get=0x0ACDF9A8
METHODS:
  RVA=0x0ACDF950  token=0x60004D1  System.Void .ctor(System.String name)
  RVA=0x0ACDF4E8  token=0x60004D2  System.Linq.Expressions.ParameterExpression Make(System.Type type, System.String name, System.Boolean isByRef)
  RVA=0x012081B0  token=0x60004D7  System.Boolean GetIsByRef()
  RVA=0x0ACDF4A0  token=0x60004D8  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
END_CLASS

CLASS: System.Linq.Expressions.ByRefParameterExpression
TYPE:  sealed class
TOKEN: 0x20000BD
SIZE:  0x20
EXTENDS: System.Linq.Expressions.TypedParameterExpression
FIELDS:
METHODS:
  RVA=0x0ACCF178  token=0x60004D9  System.Void .ctor(System.Type type, System.String name)
  RVA=0x02FFF600  token=0x60004DA  System.Boolean GetIsByRef()
END_CLASS

CLASS: System.Linq.Expressions.TypedParameterExpression
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0x20
EXTENDS: System.Linq.Expressions.ParameterExpression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x18
PROPERTIES:
  Type  get=0x0385B100
METHODS:
  RVA=0x0ACE25D8  token=0x60004DB  System.Void .ctor(System.Type type, System.String name)
END_CLASS

CLASS: System.Linq.Expressions.PrimitiveParameterExpression`1
TYPE:  sealed class
TOKEN: 0x20000BF
EXTENDS: System.Linq.Expressions.ParameterExpression
FIELDS:
PROPERTIES:
  Type  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60004DD  System.Void .ctor(System.String name)
END_CLASS

CLASS: System.Linq.Expressions.RuntimeVariablesExpression
TYPE:  sealed class
TOKEN: 0x20000C0
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression><Variables>k__BackingField  // 0x10
PROPERTIES:
  Variables  get=0x02B2ECC0
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.StackGuard
TYPE:  sealed class
TOKEN: 0x20000C1
SIZE:  0x18
FIELDS:
  private           System.Int32                    _executionStackCount  // 0x10
METHODS:
  RVA=0x02FFF600  token=0x60004E0  System.Boolean TryEnterOnCurrentStack()
  RVA=-1  // generic def  token=0x60004E1  System.Void RunOnEmptyStack(System.Action<T1,T2> action, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x60004E2  R RunOnEmptyStackCore(System.Func<System.Object,R> action, System.Object state)
  RVA=0x041E1670  token=0x60004E3  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Strings
TYPE:  static class
TOKEN: 0x20000C3
SIZE:  0x10
FIELDS:
PROPERTIES:
  ReducibleMustOverrideReduce  get=0x0ACE1DB0
  MustReduceToDifferent  get=0x0ACE1BA0
  ReducedNotCompatible  get=0x0ACE1D84
  SetterHasNoParams  get=0x0ACE1E08
  PropertyCannotHaveRefType  get=0x0ACE1CA8
  IndexesOfSetGetMustMatch  get=0x0ACE1A6C
  AccessorsCannotHaveVarArgs  get=0x0ACE1544
  AccessorsCannotHaveByRefArgs  get=0x0ACE1518
  BoundsCannotBeLessThanOne  get=0x0ACE1754
  TypeMustNotBeByRef  get=0x0ACE1EB8
  TypeMustNotBePointer  get=0x0ACE1EE4
  SetterMustBeVoid  get=0x0ACE1E34
  PropertyTypeMustMatchGetter  get=0x0ACE1D00
  PropertyTypeMustMatchSetter  get=0x0ACE1D2C
  BothAccessorsMustBeStatic  get=0x0ACE1728
  OnlyStaticFieldsHaveNullInstance  get=0x0ACE1C24
  OnlyStaticPropertiesHaveNullInstance  get=0x0ACE1C7C
  OnlyStaticMethodsHaveNullInstance  get=0x0ACE1C50
  PropertyTypeCannotBeVoid  get=0x0ACE1CD4
  InvalidUnboxType  get=0x0ACE1AC4
  ExpressionMustBeWriteable  get=0x0ACE1938
  ArgumentMustNotHaveValueType  get=0x0ACE1678
  MustBeReducible  get=0x0ACE1B74
  LabelMustBeVoidOrHaveExpression  get=0x0ACE1AF0
  LabelTypeMustBeVoid  get=0x0ACE1B1C
  QuotedExpressionMustBeLambda  get=0x0ACE1D58
  CollectionModifiedWhileEnumerating  get=0x0ACE17AC
  CollectionReadOnly  get=0x0ACE17D8
  BindingCannotBeNull  get=0x0ACE16D0
  NoOrInvalidRuleProduced  get=0x0ACE1BCC
  TypeMustBeDerivedFromSystemDelegate  get=0x0ACE1E8C
  FirstArgumentMustBeCallSite  get=0x0ACE1990
  FaultCannotHaveCatchOrFinally  get=0x0ACE1964
  TryMustHaveCatchFinallyOrFault  get=0x0ACE1E60
  BodyOfCatchMustHaveSameTypeAsBodyOfTry  get=0x0ACE16FC
  ConversionIsNotSupportedForArithmeticTypes  get=0x0ACE18B4
  ArgumentMustBeArray  get=0x0ACE15C8
  ArgumentMustBeBoolean  get=0x0ACE15F4
  ArgumentMustBeInteger  get=0x0ACE1620
  ArgumentMustBeArrayIndexType  get=0x0ACE159C
  ArgumentMustBeSingleDimensionalArrayType  get=0x0ACE164C
  ArgumentTypesMustMatch  get=0x0ACE16A4
  CoalesceUsedOnNonNullType  get=0x0ACE1780
  IncorrectNumberOfIndexes  get=0x0ACE19E8
  IncorrectNumberOfLambdaDeclarationParameters  get=0x0ACE1A40
  LambdaTypeMustBeDerivedFromSystemDelegate  get=0x0ACE1B48
  ArgumentCannotBeOfTypeVoid  get=0x0ACE1570
  ControlCannotLeaveFinally  get=0x0ACE1888
  ControlCannotLeaveFilterTest  get=0x0ACE185C
  ControlCannotEnterTry  get=0x0ACE1830
  ControlCannotEnterExpression  get=0x0ACE1804
  RethrowRequiresCatch  get=0x0ACE1DDC
  NonAbstractConstructorRequired  get=0x0ACE1BF8
  ExpressionMustBeReadable  get=0x0ACE190C
  EnumerationIsDone  get=0x0ACE18E0
  InvalidArgumentValue  get=0x0ACE1A98
  IncorrectNumberOfLambdaArguments  get=0x0ACE1A14
  IncorrectNumberOfConstructorArguments  get=0x0ACE19BC
METHODS:
  RVA=0x0ACE14CC  token=0x6000502  System.String VariableMustNotBeByRef(System.Object p0, System.Object p1)
  RVA=0x0ACE02F8  token=0x6000504  System.String AmbiguousMatchInExpandoObject(System.Object p0)
  RVA=0x0ACE1208  token=0x6000505  System.String SameKeyExistsInExpando(System.Object p0)
  RVA=0x0ACE0C24  token=0x6000506  System.String KeyDoesNotExistInExpando(System.Object p0)
  RVA=0x0ACE0B60  token=0x6000507  System.String InvalidMetaObjectCreated(System.Object p0)
  RVA=0x0ACE0394  token=0x6000508  System.String BinderNotCompatibleWithCallSite(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE0518  token=0x6000509  System.String DynamicBindingNeedsRestrictions(System.Object p0, System.Object p1)
  RVA=0x0ACE0564  token=0x600050A  System.String DynamicObjectResultNotAssignable(System.Object p0, System.Object p1, System.Object p2, System.Object p3)
  RVA=0x0ACE04B8  token=0x600050B  System.String DynamicBinderResultNotAssignable(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE047C  token=0x600050D  System.String DuplicateVariable(System.Object p0)
  RVA=0x0ACE12BC  token=0x600050E  System.String TypeParameterIsNotDelegate(System.Object p0)
  RVA=0x0ACE08CC  token=0x6000515  System.String ExtensionNodeMustOverrideProperty(System.Object p0)
  RVA=0x0ACE1454  token=0x6000516  System.String UserDefinedOperatorMustBeStatic(System.Object p0)
  RVA=0x0ACE1490  token=0x6000517  System.String UserDefinedOperatorMustNotBeVoid(System.Object p0)
  RVA=0x0ACE0430  token=0x6000518  System.String CoercionOperatorNotDefined(System.Object p0, System.Object p1)
  RVA=0x0ACE12F8  token=0x6000519  System.String UnaryOperatorNotDefined(System.Object p0, System.Object p1)
  RVA=0x0ACE0334  token=0x600051A  System.String BinaryOperatorNotDefined(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE11BC  token=0x600051B  System.String ReferenceEqualityNotDefined(System.Object p0, System.Object p1)
  RVA=0x0ACE1004  token=0x600051C  System.String OperandTypesDoNotMatchParameters(System.Object p0, System.Object p1)
  RVA=0x0ACE109C  token=0x600051D  System.String OverloadOperatorTypeDoesNotMatchConversionType(System.Object p0, System.Object p1)
  RVA=0x0ACE03F4  token=0x6000525  System.String CannotAutoInitializeValueTypeMemberThroughProperty(System.Object p0)
  RVA=0x0ACE09F0  token=0x6000526  System.String IncorrectTypeForTypeAs(System.Object p0)
  RVA=0x0ACE0668  token=0x6000528  System.String ExpressionTypeCannotInitializeArrayType(System.Object p0, System.Object p1)
  RVA=0x0ACE0844  token=0x6000529  System.String ExpressionTypeDoesNotMatchReturn(System.Object p0, System.Object p1)
  RVA=0x0ACE06B4  token=0x600052A  System.String ExpressionTypeDoesNotMatchAssignment(System.Object p0, System.Object p1)
  RVA=0x0ACE074C  token=0x600052B  System.String ExpressionTypeDoesNotMatchLabel(System.Object p0, System.Object p1)
  RVA=0x0ACE0890  token=0x600052C  System.String ExpressionTypeNotInvocable(System.Object p0)
  RVA=0x0ACE0A8C  token=0x600052D  System.String InstanceFieldNotDefinedForType(System.Object p0, System.Object p1)
  RVA=0x0ACE0908  token=0x600052E  System.String FieldInfoNotDefinedForType(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE0D24  token=0x6000532  System.String MemberNotFieldOrProperty(System.Object p0)
  RVA=0x0ACE0D60  token=0x6000533  System.String MethodContainsGenericParameters(System.Object p0)
  RVA=0x0ACE0D9C  token=0x6000534  System.String MethodIsGeneric(System.Object p0)
  RVA=0x0ACE0DD8  token=0x6000535  System.String MethodNotPropertyAccessor(System.Object p0, System.Object p1)
  RVA=0x0ACE1134  token=0x6000536  System.String PropertyDoesNotHaveAccessor(System.Object p0)
  RVA=0x0ACE10E8  token=0x6000537  System.String ParameterExpressionNotValidAsDelegate(System.Object p0, System.Object p1)
  RVA=0x0ACE1170  token=0x6000538  System.String PropertyNotDefinedForType(System.Object p0, System.Object p1)
  RVA=0x0ACE0AD8  token=0x6000539  System.String InstancePropertyNotDefinedForType(System.Object p0, System.Object p1)
  RVA=0x0ACE0A2C  token=0x600053A  System.String InstanceAndMethodTypeMismatch(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE1344  token=0x600053B  System.String UnhandledBinary(System.Object p0)
  RVA=0x0ACE1380  token=0x600053C  System.String UnhandledUnary(System.Object p0)
  RVA=0x0ACE13BC  token=0x600053D  System.String UserDefinedOpMustHaveConsistentTypes(System.Object p0, System.Object p1)
  RVA=0x0ACE1408  token=0x600053E  System.String UserDefinedOpMustHaveValidReturnType(System.Object p0, System.Object p1)
  RVA=0x0ACE0CD8  token=0x600053F  System.String LogicalOperatorMustHaveBooleanOperators(System.Object p0, System.Object p1)
  RVA=0x0ACE0E24  token=0x6000540  System.String MethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1)
  RVA=0x0ACE0968  token=0x6000541  System.String GenericMethodWithArgsDoesNotExistOnType(System.Object p0, System.Object p1)
  RVA=0x0ACE0E70  token=0x6000542  System.String MethodWithMoreThanOneMatch(System.Object p0, System.Object p1)
  RVA=0x0ACE1050  token=0x6000544  System.String OutOfRange(System.Object p0, System.Object p1)
  RVA=0x0ACE0C60  token=0x6000545  System.String LabelTargetAlreadyDefined(System.Object p0)
  RVA=0x0ACE0C9C  token=0x6000546  System.String LabelTargetUndefined(System.Object p0)
  RVA=0x0ACE02BC  token=0x6000549  System.String AmbiguousJump(System.Object p0)
  RVA=0x0ACE0FC8  token=0x600054C  System.String NonLocalJumpWithValue(System.Object p0)
  RVA=0x0ACE0B24  token=0x600054D  System.String InvalidLvalue(System.Object p0)
  RVA=0x0ACE0F1C  token=0x600054F  System.String MustRewriteToSameNode(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE0EBC  token=0x6000550  System.String MustRewriteChildToSameType(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE0F7C  token=0x6000551  System.String MustRewriteWithoutMethod(System.Object p0, System.Object p1)
  RVA=0x0ACE0700  token=0x6000554  System.String ExpressionTypeDoesNotMatchConstructorParameter(System.Object p0, System.Object p1)
  RVA=0x0ACE1244  token=0x6000556  System.String TypeContainsGenericParameters(System.Object p0)
  RVA=0x0ACE1280  token=0x6000557  System.String TypeIsGeneric(System.Object p0)
  RVA=0x0ACE0B9C  token=0x6000559  System.String InvalidNullValue(System.Object p0)
  RVA=0x0ACE0BD8  token=0x600055A  System.String InvalidObjectType(System.Object p0, System.Object p1)
  RVA=0x0ACE0798  token=0x600055B  System.String ExpressionTypeDoesNotMatchMethodParameter(System.Object p0, System.Object p1, System.Object p2)
  RVA=0x0ACE07F8  token=0x600055C  System.String ExpressionTypeDoesNotMatchParameter(System.Object p0, System.Object p1)
  RVA=0x0ACE09B4  token=0x600055D  System.String IncorrectNumberOfMethodCallArguments(System.Object p0)
END_CLASS

CLASS: System.Linq.Expressions.SwitchCase
TYPE:  sealed class
TOKEN: 0x20000C4
SIZE:  0x20
FIELDS:
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression><TestValues>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<Body>k__BackingField  // 0x18
PROPERTIES:
  TestValues  get=0x02B2ECC0
  Body  get=0x0385B100
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.SwitchExpression
TYPE:  sealed class
TOKEN: 0x20000C5
SIZE:  0x30
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.Expression<SwitchValue>k__BackingField  // 0x10
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.SwitchCase><Cases>k__BackingField  // 0x18
  private   readonly System.Linq.Expressions.Expression<DefaultBody>k__BackingField  // 0x20
  private   readonly System.Reflection.MethodInfo    <Comparison>k__BackingField  // 0x28
PROPERTIES:
  SwitchValue  get=0x02B2ECC0
  Cases  get=0x0385B100
  DefaultBody  get=0x04D862C0
  Comparison  get=0x04D86240
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.SymbolDocumentInfo
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x18
FIELDS:
  private   readonly System.String                   <FileName>k__BackingField  // 0x10
  private   static readonly System.Guid                     DocumentType_Text  // static @ 0x0
PROPERTIES:
  FileName  get=0x02B2ECC0
METHODS:
  RVA=0x0ACE1F10  token=0x6000567  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.TryExpression
TYPE:  sealed class
TOKEN: 0x20000C7
SIZE:  0x38
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<Body>k__BackingField  // 0x18
  private   readonly System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock><Handlers>k__BackingField  // 0x20
  private   readonly System.Linq.Expressions.Expression<Finally>k__BackingField  // 0x28
  private   readonly System.Linq.Expressions.Expression<Fault>k__BackingField  // 0x30
PROPERTIES:
  Type  get=0x02B2ECC0
  NodeType  get=0x04DA9FF0
  Body  get=0x0385B100
  Handlers  get=0x04D862C0
  Finally  get=0x04D86240
  Fault  get=0x011F36E0
METHODS:
  RVA=0x0ACE2364  token=0x6000568  System.Void .ctor(System.Type type, System.Linq.Expressions.Expression body, System.Linq.Expressions.Expression finally, System.Linq.Expressions.Expression fault, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock> handlers)
  RVA=0x0ACE2240  token=0x600056F  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACE2288  token=0x6000570  System.Linq.Expressions.TryExpression Update(System.Linq.Expressions.Expression body, System.Collections.Generic.IEnumerable<System.Linq.Expressions.CatchBlock> handlers, System.Linq.Expressions.Expression finally, System.Linq.Expressions.Expression fault)
END_CLASS

CLASS: System.Linq.Expressions.TypeBinaryExpression
TYPE:  sealed class
TOKEN: 0x20000C8
SIZE:  0x28
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Linq.Expressions.ExpressionType<NodeType>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.Expression<Expression>k__BackingField  // 0x18
  private   readonly System.Type                     <TypeOperand>k__BackingField  // 0x20
PROPERTIES:
  Type  get=0x0ACE2558
  NodeType  get=0x02B2E2D0
  Expression  get=0x0385B100
  TypeOperand  get=0x04D862C0
METHODS:
  RVA=0x0ACE24DC  token=0x6000571  System.Void .ctor(System.Linq.Expressions.Expression expression, System.Type typeOperand, System.Linq.Expressions.ExpressionType nodeType)
  RVA=0x0ACE240C  token=0x6000576  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACE2454  token=0x6000577  System.Linq.Expressions.TypeBinaryExpression Update(System.Linq.Expressions.Expression expression)
END_CLASS

CLASS: System.Linq.Expressions.UnaryExpression
TYPE:  sealed class
TOKEN: 0x20000C9
SIZE:  0x30
EXTENDS: System.Linq.Expressions.Expression
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.ExpressionType<NodeType>k__BackingField  // 0x18
  private   readonly System.Linq.Expressions.Expression<Operand>k__BackingField  // 0x20
  private   readonly System.Reflection.MethodInfo    <Method>k__BackingField  // 0x28
PROPERTIES:
  Type  get=0x02B2ECC0
  NodeType  get=0x011EF5B0
  Operand  get=0x04D862C0
  Method  get=0x04D86240
  IsLifted  get=0x0ACE34E8
  IsLiftedToNull  get=0x0ACE3484
  CanReduce  get=0x0ACE3464
  IsPrefix  get=0x0ACE36B0
METHODS:
  RVA=0x0ACE33D8  token=0x6000578  System.Void .ctor(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression expression, System.Type type, System.Reflection.MethodInfo method)
  RVA=0x0ACE2720  token=0x600057F  System.Linq.Expressions.Expression Accept(System.Linq.Expressions.ExpressionVisitor visitor)
  RVA=0x0ACE32D0  token=0x6000581  System.Linq.Expressions.Expression Reduce()
  RVA=0x0ACE2768  token=0x6000583  System.Linq.Expressions.UnaryExpression FunctionalOp(System.Linq.Expressions.Expression operand)
  RVA=0x0ACE3084  token=0x6000584  System.Linq.Expressions.Expression ReduceVariable()
  RVA=0x0ACE2CB8  token=0x6000585  System.Linq.Expressions.Expression ReduceMember()
  RVA=0x0ACE283C  token=0x6000586  System.Linq.Expressions.Expression ReduceIndex()
  RVA=0x0ACE333C  token=0x6000587  System.Linq.Expressions.UnaryExpression Update(System.Linq.Expressions.Expression operand)
END_CLASS

CLASS: System.Linq.Expressions.Utils
TYPE:  static class
TOKEN: 0x20000CA
SIZE:  0x10
FIELDS:
  public    static readonly System.Object                   BoxedFalse  // static @ 0x0
  public    static readonly System.Object                   BoxedTrue  // static @ 0x8
  public    static readonly System.Object                   BoxedIntM1  // static @ 0x10
  public    static readonly System.Object                   BoxedInt0  // static @ 0x18
  public    static readonly System.Object                   BoxedInt1  // static @ 0x20
  public    static readonly System.Object                   BoxedInt2  // static @ 0x28
  public    static readonly System.Object                   BoxedInt3  // static @ 0x30
  public    static readonly System.Object                   BoxedDefaultSByte  // static @ 0x38
  public    static readonly System.Object                   BoxedDefaultChar  // static @ 0x40
  public    static readonly System.Object                   BoxedDefaultInt16  // static @ 0x48
  public    static readonly System.Object                   BoxedDefaultInt64  // static @ 0x50
  public    static readonly System.Object                   BoxedDefaultByte  // static @ 0x58
  public    static readonly System.Object                   BoxedDefaultUInt16  // static @ 0x60
  public    static readonly System.Object                   BoxedDefaultUInt32  // static @ 0x68
  public    static readonly System.Object                   BoxedDefaultUInt64  // static @ 0x70
  public    static readonly System.Object                   BoxedDefaultSingle  // static @ 0x78
  public    static readonly System.Object                   BoxedDefaultDouble  // static @ 0x80
  public    static readonly System.Object                   BoxedDefaultDecimal  // static @ 0x88
  public    static readonly System.Object                   BoxedDefaultDateTime  // static @ 0x90
  private   static readonly System.Linq.Expressions.ConstantExpressions_true  // static @ 0x98
  private   static readonly System.Linq.Expressions.ConstantExpressions_false  // static @ 0xa0
  private   static readonly System.Linq.Expressions.ConstantExpressions_m1  // static @ 0xa8
  private   static readonly System.Linq.Expressions.ConstantExpressions_0  // static @ 0xb0
  private   static readonly System.Linq.Expressions.ConstantExpressions_1  // static @ 0xb8
  private   static readonly System.Linq.Expressions.ConstantExpressions_2  // static @ 0xc0
  private   static readonly System.Linq.Expressions.ConstantExpressions_3  // static @ 0xc8
  public    static readonly System.Linq.Expressions.DefaultExpressionEmpty  // static @ 0xd0
  public    static readonly System.Linq.Expressions.ConstantExpressionNull  // static @ 0xd8
METHODS:
  RVA=0x0ACE3834  token=0x6000588  System.Linq.Expressions.ConstantExpression Constant(System.Boolean value)
  RVA=0x0ACE36E8  token=0x6000589  System.Linq.Expressions.ConstantExpression Constant(System.Int32 value)
  RVA=0x0ACE388C  token=0x600058A  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.AddInstruction
TYPE:  abstract class
TOKEN: 0x20000CB
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACCB3D4
METHODS:
  RVA=0x041E1670  token=0x600058E  System.Void .ctor()
  RVA=0x0ACCAFC4  token=0x600058F  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.AddOvfInstruction
TYPE:  abstract class
TOKEN: 0x20000D4
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACCBA48
METHODS:
  RVA=0x041E1670  token=0x60005A3  System.Void .ctor()
  RVA=0x0ACCB72C  token=0x60005A4  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.AndInstruction
TYPE:  abstract class
TOKEN: 0x20000DB
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_Boolean  // static @ 0x40
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACCCE84
METHODS:
  RVA=0x041E1670  token=0x60005B4  System.Void .ctor()
  RVA=0x0ACCC9F4  token=0x60005B5  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NewArrayInitInstruction
TYPE:  sealed class
TOKEN: 0x20000E5
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Type                     _elementType  // 0x10
  private   readonly System.Int32                    _elementCount  // 0x18
PROPERTIES:
  ConsumedStack  get=0x011EF5B0
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACDF19C
METHODS:
  RVA=0x03B46FF0  token=0x60005C8  System.Void .ctor(System.Type elementType, System.Int32 elementCount)
  RVA=0x0ACDF114  token=0x60005CC  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NewArrayInstruction
TYPE:  sealed class
TOKEN: 0x20000E6
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Type                     _elementType  // 0x10
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACDF254
METHODS:
  RVA=0x053908C0  token=0x60005CD  System.Void .ctor(System.Type elementType)
  RVA=0x0ACDF1C8  token=0x60005D1  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NewArrayBoundsInstruction
TYPE:  sealed class
TOKEN: 0x20000E7
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Type                     _elementType  // 0x10
  private   readonly System.Int32                    _rank  // 0x18
PROPERTIES:
  ConsumedStack  get=0x011EF5B0
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACDEE48
METHODS:
  RVA=0x03B46FF0  token=0x60005D2  System.Void .ctor(System.Type elementType, System.Int32 rank)
  RVA=0x0ACDED3C  token=0x60005D6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.GetArrayItemInstruction
TYPE:  sealed class
TOKEN: 0x20000E8
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.GetArrayItemInstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACDB53C
METHODS:
  RVA=0x041E1670  token=0x60005D7  System.Void .ctor()
  RVA=0x0ACDB444  token=0x60005DB  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDB4D8  token=0x60005DC  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.SetArrayItemInstruction
TYPE:  sealed class
TOKEN: 0x20000E9
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.SetArrayItemInstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x04D8C9E0
  InstructionName  get=0x0ACE01A8
METHODS:
  RVA=0x041E1670  token=0x60005DD  System.Void .ctor()
  RVA=0x0ACE0090  token=0x60005E0  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACE0144  token=0x60005E1  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ArrayLengthInstruction
TYPE:  sealed class
TOKEN: 0x20000EA
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  public    static readonly System.Linq.Expressions.Interpreter.ArrayLengthInstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACCD49C
METHODS:
  RVA=0x041E1670  token=0x60005E5  System.Void .ctor()
  RVA=0x0ACCD3AC  token=0x60005E6  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACCD438  token=0x60005E7  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ConvertHelper
TYPE:  static class
TOKEN: 0x20000EB
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACD0090  token=0x60005E8  System.Int32 ToInt32NoNull(System.Object val)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.RuntimeLabel
TYPE:  sealed struct
TOKEN: 0x20000EC
SIZE:  0x1C
FIELDS:
  public    readonly System.Int32                    Index  // 0x10
  public    readonly System.Int32                    StackDepth  // 0x14
  public    readonly System.Int32                    ContinuationStackDepth  // 0x18
METHODS:
  RVA=0x04DA9FE0  token=0x60005E9  System.Void .ctor(System.Int32 index, System.Int32 continuationStackDepth, System.Int32 stackDepth)
  RVA=0x0ACDFA2C  token=0x60005EA  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.BranchLabel
TYPE:  sealed class
TOKEN: 0x20000ED
SIZE:  0x30
FIELDS:
  private           System.Int32                    _targetIndex  // 0x10
  private           System.Int32                    _stackDepth  // 0x14
  private           System.Int32                    _continuationStackDepth  // 0x18
  private           System.Collections.Generic.List<System.Int32>_forwardBranchFixups  // 0x20
  private           System.Int32                    <LabelIndex>k__BackingField  // 0x28
PROPERTIES:
  LabelIndex  get=0x011F2A90  set=0x011F2AA0
  HasRuntimeLabel  get=0x0ACCEA04
  TargetIndex  get=0x02B2E2D0
METHODS:
  RVA=0x04DA9F20  token=0x60005EF  System.Linq.Expressions.Interpreter.RuntimeLabel ToRuntimeLabel()
  RVA=0x0ACCE87C  token=0x60005F0  System.Void Mark(System.Linq.Expressions.Interpreter.InstructionList instructions)
  RVA=0x0ACCE780  token=0x60005F1  System.Void AddBranch(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Int32 branchIndex)
  RVA=0x0ACCE84C  token=0x60005F2  System.Void FixupBranch(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Int32 branchIndex)
  RVA=0x04DA9F40  token=0x60005F3  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.CallInstruction
TYPE:  abstract class
TOKEN: 0x20000EE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
PROPERTIES:
  ArgumentCount  get=-1  // abstract
  InstructionName  get=0x0ACCF878
  ConsumedStack  get=0x0ACCF868
METHODS:
  RVA=0x0ACCF208  token=0x60005F6  System.Linq.Expressions.Interpreter.CallInstruction Create(System.Reflection.MethodInfo info)
  RVA=0x0ACCF258  token=0x60005F7  System.Linq.Expressions.Interpreter.CallInstruction Create(System.Reflection.MethodInfo info, System.Reflection.ParameterInfo[] parameters)
  RVA=0x0ACCF3B0  token=0x60005F8  System.Linq.Expressions.Interpreter.CallInstruction GetArrayAccessor(System.Reflection.MethodInfo info, System.Int32 argumentCount)
  RVA=0x0ACCF180  token=0x60005F9  System.Void ArrayItemSetter1(System.Array array, System.Int32 index0, System.Object value)
  RVA=0x0ACCF1A4  token=0x60005FA  System.Void ArrayItemSetter2(System.Array array, System.Int32 index0, System.Int32 index1, System.Object value)
  RVA=0x0ACCF1D4  token=0x60005FB  System.Void ArrayItemSetter3(System.Array array, System.Int32 index0, System.Int32 index1, System.Int32 index2, System.Object value)
  RVA=0x0ACCF7A8  token=0x60005FD  System.Boolean TryGetLightLambdaTarget(System.Object instance, System.Linq.Expressions.Interpreter.LightLambda& lightLambda)
  RVA=0x0ACCF74C  token=0x60005FE  System.Object InterpretLambdaInvoke(System.Linq.Expressions.Interpreter.LightLambda targetLambda, System.Object[] args)
  RVA=0x041E1670  token=0x60005FF  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.MethodInfoCallInstruction
TYPE:  class
TOKEN: 0x20000EF
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.CallInstruction
FIELDS:
  protected readonly System.Reflection.MethodInfo    _target  // 0x10
  protected readonly System.Int32                    _argumentCount  // 0x18
PROPERTIES:
  ArgumentCount  get=0x011EF5B0
  ProducedStack  get=0x0ACDECA4
METHODS:
  RVA=0x03B46FF0  token=0x6000601  System.Void .ctor(System.Reflection.MethodInfo target, System.Int32 argumentCount)
  RVA=0x0ACDE97C  token=0x6000603  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDE888  token=0x6000604  System.Object[] GetArgs(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Int32 first, System.Int32 skip)
  RVA=0x0ACDEC30  token=0x6000605  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ByRefMethodInfoCallInstruction
TYPE:  class
TOKEN: 0x20000F0
SIZE:  0x28
EXTENDS: System.Linq.Expressions.Interpreter.MethodInfoCallInstruction
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.ByRefUpdater[]_byrefArgs  // 0x20
PROPERTIES:
  ProducedStack  get=0x0ACCF0E8
METHODS:
  RVA=0x032B2C30  token=0x6000606  System.Void .ctor(System.Reflection.MethodInfo target, System.Int32 argumentCount, System.Linq.Expressions.Interpreter.ByRefUpdater[] byrefArgs)
  RVA=0x0ACCEB30  token=0x6000608  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.OffsetInstruction
TYPE:  abstract class
TOKEN: 0x20000F1
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  protected         System.Int32                    _offset  // 0x10
PROPERTIES:
  Cache  get=-1  // abstract
METHODS:
  RVA=0x0ACDF2D4  token=0x600060A  System.Linq.Expressions.Interpreter.Instruction Fixup(System.Int32 offset)
  RVA=0x0ACDF35C  token=0x600060B  System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects)
  RVA=0x0ACDF400  token=0x600060C  System.String ToString()
  RVA=0x04DA9EF0  token=0x600060D  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.BranchFalseInstruction
TYPE:  sealed class
TOKEN: 0x20000F2
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.OffsetInstruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_cache  // static @ 0x0
PROPERTIES:
  Cache  get=0x0ACCE484
  InstructionName  get=0x0ACCE510
  ConsumedStack  get=0x04D85B80
METHODS:
  RVA=0x0ACCE41C  token=0x6000611  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x04DA9EF0  token=0x6000612  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.BranchTrueInstruction
TYPE:  sealed class
TOKEN: 0x20000F3
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.OffsetInstruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_cache  // static @ 0x0
PROPERTIES:
  Cache  get=0x0ACCEA78
  InstructionName  get=0x0ACCEB04
  ConsumedStack  get=0x04D85B80
METHODS:
  RVA=0x0ACCEA10  token=0x6000616  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x04DA9EF0  token=0x6000617  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.CoalescingBranchInstruction
TYPE:  sealed class
TOKEN: 0x20000F4
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.OffsetInstruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_cache  // static @ 0x0
PROPERTIES:
  Cache  get=0x0ACCF9B8
  InstructionName  get=0x0ACCFA44
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
METHODS:
  RVA=0x0ACCF984  token=0x600061C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x04DA9EF0  token=0x600061D  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.BranchInstruction
TYPE:  class
TOKEN: 0x20000F5
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.OffsetInstruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instruction[][][]s_caches  // static @ 0x0
  private   readonly System.Boolean                  _hasResult  // 0x18
  private   readonly System.Boolean                  _hasValue  // 0x19
PROPERTIES:
  Cache  get=0x0ACCE53C
  InstructionName  get=0x0ACCE748
  ConsumedStack  get=0x0ACCE73C
  ProducedStack  get=0x0ACCE774
METHODS:
  RVA=0x04DA9F00  token=0x600061F  System.Void .ctor()
  RVA=0x04DA9F10  token=0x6000620  System.Void .ctor(System.Boolean hasResult, System.Boolean hasValue)
  RVA=0x02B2E2D0  token=0x6000624  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.IndexedBranchInstruction
TYPE:  abstract class
TOKEN: 0x20000F6
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Int32                    _labelIndex  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x6000625  System.Void .ctor(System.Int32 labelIndex)
  RVA=0x0ACDBBA0  token=0x6000626  System.Linq.Expressions.Interpreter.RuntimeLabel GetLabel(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDBBDC  token=0x6000627  System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects)
  RVA=0x0ACDBC90  token=0x6000628  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.GotoInstruction
TYPE:  sealed class
TOKEN: 0x20000F7
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.IndexedBranchInstruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.GotoInstruction[]s_cache  // static @ 0x0
  private   readonly System.Boolean                  _hasResult  // 0x18
  private   readonly System.Boolean                  _hasValue  // 0x19
  private   readonly System.Boolean                  _labelTargetGetsValue  // 0x1a
PROPERTIES:
  InstructionName  get=0x0ACDB92C
  ConsumedStack  get=0x0ACCE73C
  ProducedStack  get=0x0ACCE774
METHODS:
  RVA=0x04DA9F90  token=0x600062C  System.Void .ctor(System.Int32 targetIndex, System.Boolean hasResult, System.Boolean hasValue, System.Boolean labelTargetGetsValue)
  RVA=0x0ACDB6D0  token=0x600062D  System.Linq.Expressions.Interpreter.GotoInstruction Create(System.Int32 labelIndex, System.Boolean hasResult, System.Boolean hasValue, System.Boolean labelTargetGetsValue)
  RVA=0x0ACDB808  token=0x600062E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDB8C4  token=0x600062F  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction
TYPE:  sealed class
TOKEN: 0x20000F8
SIZE:  0x28
EXTENDS: System.Linq.Expressions.Interpreter.IndexedBranchInstruction
FIELDS:
  private   readonly System.Boolean                  _hasFinally  // 0x18
  private           System.Linq.Expressions.Interpreter.TryCatchFinallyHandler_tryHandler  // 0x20
PROPERTIES:
  Handler  get=0x04D862C0
  ProducedContinuations  get=0x0ACCE774
  InstructionName  get=0x0ACD2500
METHODS:
  RVA=0x05390F40  token=0x6000630  System.Void SetTryHandler(System.Linq.Expressions.Interpreter.TryCatchFinallyHandler tryHandler)
  RVA=0x04DA9F70  token=0x6000633  System.Void .ctor(System.Int32 targetIndex, System.Boolean hasFinally)
  RVA=0x0ACD1E58  token=0x6000634  System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction CreateTryFinally(System.Int32 labelIndex)
  RVA=0x0ACD1E10  token=0x6000635  System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction CreateTryCatch()
  RVA=0x0ACD1EA0  token=0x6000636  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACD2484  token=0x6000638  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.EnterTryFaultInstruction
TYPE:  sealed class
TOKEN: 0x20000F9
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.IndexedBranchInstruction
FIELDS:
  private           System.Linq.Expressions.Interpreter.TryFaultHandler_tryHandler  // 0x18
PROPERTIES:
  InstructionName  get=0x0ACD2830
  ProducedContinuations  get=0x04D85B80
  Handler  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000639  System.Void .ctor(System.Int32 targetIndex)
  RVA=0x05392C40  token=0x600063D  System.Void SetTryHandler(System.Linq.Expressions.Interpreter.TryFaultHandler tryHandler)
  RVA=0x0ACD254C  token=0x600063E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.EnterFinallyInstruction
TYPE:  sealed class
TOKEN: 0x20000FA
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.IndexedBranchInstruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.EnterFinallyInstruction[]s_cache  // static @ 0x0
PROPERTIES:
  InstructionName  get=0x0ACD1DE4
  ProducedStack  get=0x02BBE540
  ConsumedContinuations  get=0x04D85B80
METHODS:
  RVA=0x014F51F0  token=0x600063F  System.Void .ctor(System.Int32 labelIndex)
  RVA=0x0ACD1C54  token=0x6000643  System.Linq.Expressions.Interpreter.EnterFinallyInstruction Create(System.Int32 labelIndex)
  RVA=0x0ACD1D28  token=0x6000644  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACD1D7C  token=0x6000645  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LeaveFinallyInstruction
TYPE:  sealed class
TOKEN: 0x20000FB
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.InstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  InstructionName  get=0x0ACDD750
METHODS:
  RVA=0x041E1670  token=0x6000646  System.Void .ctor()
  RVA=0x0ACDD6AC  token=0x6000649  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDD6EC  token=0x600064A  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.EnterFaultInstruction
TYPE:  sealed class
TOKEN: 0x20000FC
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.IndexedBranchInstruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.EnterFaultInstruction[]s_cache  // static @ 0x0
PROPERTIES:
  InstructionName  get=0x0ACD1C28
  ProducedStack  get=0x02BBE540
METHODS:
  RVA=0x014F51F0  token=0x600064B  System.Void .ctor(System.Int32 labelIndex)
  RVA=0x0ACD1A9C  token=0x600064E  System.Linq.Expressions.Interpreter.EnterFaultInstruction Create(System.Int32 labelIndex)
  RVA=0x0ACD1B70  token=0x600064F  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACD1BC0  token=0x6000650  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LeaveFaultInstruction
TYPE:  sealed class
TOKEN: 0x20000FD
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.InstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ConsumedContinuations  get=0x04D85B80
  InstructionName  get=0x0ACDD680
METHODS:
  RVA=0x041E1670  token=0x6000651  System.Void .ctor()
  RVA=0x0ACDD5F4  token=0x6000655  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDD61C  token=0x6000656  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.EnterExceptionFilterInstruction
TYPE:  sealed class
TOKEN: 0x20000FE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.EnterExceptionFilterInstructionInstance  // static @ 0x0
PROPERTIES:
  InstructionName  get=0x0ACD1994
  ProducedStack  get=0x04D85B80
METHODS:
  RVA=0x041E1670  token=0x6000657  System.Void .ctor()
  RVA=0x04D85B80  token=0x600065A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACD1930  token=0x600065B  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LeaveExceptionFilterInstruction
TYPE:  sealed class
TOKEN: 0x20000FF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.LeaveExceptionFilterInstructionInstance  // static @ 0x0
PROPERTIES:
  InstructionName  get=0x0ACDD404
  ConsumedStack  get=0x02BBE540
METHODS:
  RVA=0x041E1670  token=0x600065C  System.Void .ctor()
  RVA=0x04D85B80  token=0x600065F  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDD3A0  token=0x6000660  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.EnterExceptionHandlerInstruction
TYPE:  sealed class
TOKEN: 0x2000100
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.EnterExceptionHandlerInstructionVoid  // static @ 0x0
  private   static readonly System.Linq.Expressions.Interpreter.EnterExceptionHandlerInstructionNonVoid  // static @ 0x8
  private   readonly System.Boolean                  _hasValue  // 0x10
PROPERTIES:
  InstructionName  get=0x0ACD1A70
  ConsumedStack  get=0x0ACD1A64
  ProducedStack  get=0x04D85B80
METHODS:
  RVA=0x04D86130  token=0x6000661  System.Void .ctor(System.Boolean hasValue)
  RVA=0x04D85B80  token=0x6000665  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACD19C0  token=0x6000666  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LeaveExceptionHandlerInstruction
TYPE:  sealed class
TOKEN: 0x2000101
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.IndexedBranchInstruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.LeaveExceptionHandlerInstruction[]s_cache  // static @ 0x0
  private   readonly System.Boolean                  _hasValue  // 0x18
PROPERTIES:
  InstructionName  get=0x0ACDD5C8
  ConsumedStack  get=0x0ACCE774
  ProducedStack  get=0x0ACCE774
METHODS:
  RVA=0x04DA9F70  token=0x6000667  System.Void .ctor(System.Int32 labelIndex, System.Boolean hasValue)
  RVA=0x0ACDD430  token=0x600066B  System.Linq.Expressions.Interpreter.LeaveExceptionHandlerInstruction Create(System.Int32 labelIndex, System.Boolean hasValue)
  RVA=0x0ACDD52C  token=0x600066C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACDD560  token=0x600066D  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ThrowInstruction
TYPE:  sealed class
TOKEN: 0x2000102
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.ThrowInstructionThrow  // static @ 0x0
  private   static readonly System.Linq.Expressions.Interpreter.ThrowInstructionVoidThrow  // static @ 0x8
  private   static readonly System.Linq.Expressions.Interpreter.ThrowInstructionRethrow  // static @ 0x10
  private   static readonly System.Linq.Expressions.Interpreter.ThrowInstructionVoidRethrow  // static @ 0x18
  private   readonly System.Boolean                  _hasResult  // 0x10
  private   readonly System.Boolean                  _rethrow  // 0x11
PROPERTIES:
  InstructionName  get=0x0ACE2214
  ProducedStack  get=0x0ACD1A64
  ConsumedStack  get=0x04D85B80
METHODS:
  RVA=0x04DA3CB0  token=0x600066E  System.Void .ctor(System.Boolean hasResult, System.Boolean isRethrow)
  RVA=0x0ACE1FA4  token=0x6000672  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACE2050  token=0x6000673  System.Exception WrapThrownObject(System.Object thrown)
  RVA=0x0ACE20E8  token=0x6000674  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.IntSwitchInstruction`1
TYPE:  sealed class
TOKEN: 0x2000103
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<T,System.Int32>_cases  // 0x0
PROPERTIES:
  InstructionName  get=-1  // not resolved
  ConsumedStack  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000675  System.Void .ctor(System.Collections.Generic.Dictionary<T,System.Int32> cases)
  RVA=-1  // not resolved  token=0x6000678  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.StringSwitchInstruction
TYPE:  sealed class
TOKEN: 0x2000104
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.String,System.Int32>_cases  // 0x10
  private   readonly System.Runtime.CompilerServices.StrongBox<System.Int32>_nullCase  // 0x18
PROPERTIES:
  InstructionName  get=0x0ACE0290
  ConsumedStack  get=0x04D85B80
METHODS:
  RVA=0x032AF420  token=0x6000679  System.Void .ctor(System.Collections.Generic.Dictionary<System.String,System.Int32> cases, System.Runtime.CompilerServices.StrongBox<System.Int32> nullCase)
  RVA=0x0ACE01D4  token=0x600067C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.DecrementInstruction
TYPE:  abstract class
TOKEN: 0x2000105
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACD065C
METHODS:
  RVA=0x041E1670  token=0x6000680  System.Void .ctor()
  RVA=0x0ACD024C  token=0x6000681  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.DefaultValueInstruction
TYPE:  sealed class
TOKEN: 0x200010E
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Type                     _type  // 0x10
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACD0B88
METHODS:
  RVA=0x053908C0  token=0x6000692  System.Void .ctor(System.Type type)
  RVA=0x0ACD0AF0  token=0x6000695  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACD0B28  token=0x6000696  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.DivInstruction
TYPE:  abstract class
TOKEN: 0x200010F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACD10DC
METHODS:
  RVA=0x041E1670  token=0x600069A  System.Void .ctor()
  RVA=0x0ACD0CCC  token=0x600069B  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.EqualInstruction
TYPE:  abstract class
TOKEN: 0x2000118
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_reference  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Boolean  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Char  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.Instructions_BooleanLiftedToNull  // static @ 0x68
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByteLiftedToNull  // static @ 0x70
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16LiftedToNull  // static @ 0x78
  private   static  System.Linq.Expressions.Interpreter.Instructions_CharLiftedToNull  // static @ 0x80
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32LiftedToNull  // static @ 0x88
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64LiftedToNull  // static @ 0x90
  private   static  System.Linq.Expressions.Interpreter.Instructions_ByteLiftedToNull  // static @ 0x98
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16LiftedToNull  // static @ 0xa0
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32LiftedToNull  // static @ 0xa8
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64LiftedToNull  // static @ 0xb0
  private   static  System.Linq.Expressions.Interpreter.Instructions_SingleLiftedToNull  // static @ 0xb8
  private   static  System.Linq.Expressions.Interpreter.Instructions_DoubleLiftedToNull  // static @ 0xc0
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACD39CC
METHODS:
  RVA=0x041E1670  token=0x60006AF  System.Void .ctor()
  RVA=0x0ACD2E08  token=0x60006B0  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ExclusiveOrInstruction
TYPE:  abstract class
TOKEN: 0x2000132
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_Boolean  // static @ 0x40
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACD8550
METHODS:
  RVA=0x041E1670  token=0x60006E6  System.Void .ctor()
  RVA=0x0ACD80C0  token=0x60006E7  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.FieldInstruction
TYPE:  abstract class
TOKEN: 0x200013C
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  protected readonly System.Reflection.FieldInfo     _field  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60006FA  System.Void .ctor(System.Reflection.FieldInfo field)
  RVA=0x0ACE505C  token=0x60006FB  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadStaticFieldInstruction
TYPE:  sealed class
TOKEN: 0x200013D
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.FieldInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0ACFCB10
  ProducedStack  get=0x04D85B80
METHODS:
  RVA=0x053908C0  token=0x60006FC  System.Void .ctor(System.Reflection.FieldInfo field)
  RVA=0x0ACFCACC  token=0x60006FF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadFieldInstruction
TYPE:  sealed class
TOKEN: 0x200013E
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.FieldInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0ACFCAA0
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
METHODS:
  RVA=0x053908C0  token=0x6000700  System.Void .ctor(System.Reflection.FieldInfo field)
  RVA=0x0ACFCA40  token=0x6000704  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.StoreFieldInstruction
TYPE:  sealed class
TOKEN: 0x200013F
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.FieldInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0ACFCBF0
  ConsumedStack  get=0x02BBE540
METHODS:
  RVA=0x053908C0  token=0x6000705  System.Void .ctor(System.Reflection.FieldInfo field)
  RVA=0x0ACFCB88  token=0x6000708  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.StoreStaticFieldInstruction
TYPE:  sealed class
TOKEN: 0x2000140
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.FieldInstruction
FIELDS:
PROPERTIES:
  InstructionName  get=0x0ACFCC60
  ConsumedStack  get=0x04D85B80
METHODS:
  RVA=0x053908C0  token=0x6000709  System.Void .ctor(System.Reflection.FieldInfo field)
  RVA=0x0ACFCC1C  token=0x600070C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.GreaterThanInstruction
TYPE:  abstract class
TOKEN: 0x2000141
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Object                   _nullValue  // 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Char  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSByte  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt16  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullChar  // static @ 0x68
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt32  // static @ 0x70
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt64  // static @ 0x78
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullByte  // static @ 0x80
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt16  // static @ 0x88
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt32  // static @ 0x90
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt64  // static @ 0x98
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSingle  // static @ 0xa0
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullDouble  // static @ 0xa8
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACE5FC4
METHODS:
  RVA=0x053908C0  token=0x6000710  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE5314  token=0x6000711  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.GreaterThanOrEqualInstruction
TYPE:  abstract class
TOKEN: 0x200014D
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Object                   _nullValue  // 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Char  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSByte  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt16  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullChar  // static @ 0x68
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt32  // static @ 0x70
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt64  // static @ 0x78
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullByte  // static @ 0x80
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt16  // static @ 0x88
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt32  // static @ 0x90
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt64  // static @ 0x98
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSingle  // static @ 0xa0
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullDouble  // static @ 0xa8
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACE70F4
METHODS:
  RVA=0x053908C0  token=0x600072B  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACE6444  token=0x600072C  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.IncrementInstruction
TYPE:  abstract class
TOKEN: 0x2000159
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACE7F28
METHODS:
  RVA=0x041E1670  token=0x6000746  System.Void .ctor()
  RVA=0x0ACE7B18  token=0x6000747  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.Instruction
TYPE:  abstract class
TOKEN: 0x2000162
SIZE:  0x10
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x011EC580
  ProducedStack  get=0x011EC580
  ConsumedContinuations  get=0x011EC580
  ProducedContinuations  get=0x011EC580
  StackBalance  get=0x0ACEC2F4
  ContinuationsBalance  get=0x0ACEC2BC
  InstructionName  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600075E  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACEC270  token=0x6000760  System.String ToString()
  RVA=0x05397C78  token=0x6000761  System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects)
  RVA=0x09D3CD3C  token=0x6000762  System.Void NullCheck(System.Object o)
  RVA=0x041E1670  token=0x6000763  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.InstructionArray
TYPE:  sealed struct
TOKEN: 0x2000163
SIZE:  0x38
FIELDS:
  private   readonly System.Int32                    MaxStackDepth  // 0x10
  private   readonly System.Int32                    MaxContinuationDepth  // 0x14
  private   readonly System.Linq.Expressions.Interpreter.Instruction[]Instructions  // 0x18
  private   readonly System.Object[]                 Objects  // 0x20
  private   readonly System.Linq.Expressions.Interpreter.RuntimeLabel[]Labels  // 0x28
  private   readonly System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>>DebugCookies  // 0x30
METHODS:
  RVA=0x0ACE831C  token=0x6000764  System.Void .ctor(System.Int32 maxStackDepth, System.Int32 maxContinuationDepth, System.Linq.Expressions.Interpreter.Instruction[] instructions, System.Object[] objects, System.Linq.Expressions.Interpreter.RuntimeLabel[] labels, System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>> debugCookies)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.InstructionList
TYPE:  sealed class
TOKEN: 0x2000165
SIZE:  0x48
FIELDS:
  private   readonly System.Collections.Generic.List<System.Linq.Expressions.Interpreter.Instruction>_instructions  // 0x10
  private           System.Collections.Generic.List<System.Object>_objects  // 0x18
  private           System.Int32                    _currentStackDepth  // 0x20
  private           System.Int32                    _maxStackDepth  // 0x24
  private           System.Int32                    _currentContinuationsDepth  // 0x28
  private           System.Int32                    _maxContinuationDepth  // 0x2c
  private           System.Int32                    _runtimeLabelCount  // 0x30
  private           System.Collections.Generic.List<System.Linq.Expressions.Interpreter.BranchLabel>_labels  // 0x38
  private           System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32,System.Object>>_debugCookies  // 0x40
  private   static  System.Linq.Expressions.Interpreter.Instructions_null  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_true  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_false  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_Ints  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_loadObjectCached  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_loadLocal  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_loadLocalBoxed  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_loadLocalFromClosure  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_loadLocalFromClosureBoxed  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_assignLocal  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_storeLocal  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_assignLocalBoxed  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_storeLocalBoxed  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.Instruction[]s_assignLocalToClosure  // static @ 0x68
  private   static readonly System.Collections.Generic.Dictionary<System.Reflection.FieldInfo,System.Linq.Expressions.Interpreter.Instruction>s_loadFields  // static @ 0x70
  private   static readonly System.Linq.Expressions.Interpreter.RuntimeLabel[]s_emptyRuntimeLabels  // static @ 0x78
PROPERTIES:
  Count  get=0x0ACEC1F4
  CurrentStackDepth  get=0x02B70F90
  CurrentContinuationsDepth  get=0x011F2A90
METHODS:
  RVA=0x0ACEB348  token=0x6000768  System.Void Emit(System.Linq.Expressions.Interpreter.Instruction instruction)
  RVA=0x0ACEBFE0  token=0x6000769  System.Void UpdateStackDepth(System.Linq.Expressions.Interpreter.Instruction instruction)
  RVA=0x0ACEBEEC  token=0x600076A  System.Void UnEmit()
  RVA=0x0ACEB4A4  token=0x600076E  System.Linq.Expressions.Interpreter.Instruction GetInstruction(System.Int32 index)
  RVA=0x0ACEBE04  token=0x600076F  System.Linq.Expressions.Interpreter.InstructionArray ToArray()
  RVA=0x0ACEA700  token=0x6000770  System.Void EmitLoad(System.Object value)
  RVA=0x0ACEA568  token=0x6000771  System.Void EmitLoad(System.Boolean value)
  RVA=0x0ACEA0A4  token=0x6000772  System.Void EmitLoad(System.Object value, System.Type type)
  RVA=0x0ACE92D8  token=0x6000773  System.Void EmitDup()
  RVA=0x0ACEAC30  token=0x6000774  System.Void EmitPop()
  RVA=0x0ACEBC98  token=0x6000775  System.Void SwitchToBoxed(System.Int32 index, System.Int32 instructionIndex)
  RVA=0x0ACE9EF0  token=0x6000776  System.Void EmitLoadLocal(System.Int32 index)
  RVA=0x0ACE9B30  token=0x6000777  System.Void EmitLoadLocalBoxed(System.Int32 index)
  RVA=0x0ACEB7AC  token=0x6000778  System.Linq.Expressions.Interpreter.Instruction LoadLocalBoxed(System.Int32 index)
  RVA=0x0ACE9D3C  token=0x6000779  System.Void EmitLoadLocalFromClosure(System.Int32 index)
  RVA=0x0ACE9B88  token=0x600077A  System.Void EmitLoadLocalFromClosureBoxed(System.Int32 index)
  RVA=0x0ACE89E8  token=0x600077B  System.Void EmitAssignLocal(System.Int32 index)
  RVA=0x0ACEAEE0  token=0x600077C  System.Void EmitStoreLocal(System.Int32 index)
  RVA=0x0ACE87DC  token=0x600077D  System.Void EmitAssignLocalBoxed(System.Int32 index)
  RVA=0x0ACE8370  token=0x600077E  System.Linq.Expressions.Interpreter.Instruction AssignLocalBoxed(System.Int32 index)
  RVA=0x0ACEAE68  token=0x600077F  System.Void EmitStoreLocalBoxed(System.Int32 index)
  RVA=0x0ACEBB04  token=0x6000780  System.Linq.Expressions.Interpreter.Instruction StoreLocalBoxed(System.Int32 index)
  RVA=0x0ACE8834  token=0x6000781  System.Void EmitAssignLocalToClosure(System.Int32 index)
  RVA=0x0ACEAEC0  token=0x6000782  System.Void EmitStoreLocalToClosure(System.Int32 index)
  RVA=0x0ACE97B0  token=0x6000783  System.Void EmitInitializeLocal(System.Int32 index, System.Type type)
  RVA=0x0ACE98A8  token=0x6000784  System.Void EmitInitializeParameter(System.Int32 index)
  RVA=0x0ACEBAC0  token=0x6000785  System.Linq.Expressions.Interpreter.Instruction Parameter(System.Int32 index)
  RVA=0x0ACEBA7C  token=0x6000786  System.Linq.Expressions.Interpreter.Instruction ParameterBox(System.Int32 index)
  RVA=0x0ACEB768  token=0x6000787  System.Linq.Expressions.Interpreter.Instruction InitReference(System.Int32 index)
  RVA=0x0ACEB724  token=0x6000788  System.Linq.Expressions.Interpreter.Instruction InitImmutableRefBox(System.Int32 index)
  RVA=0x0ACEA958  token=0x6000789  System.Void EmitNewRuntimeVariables(System.Int32 count)
  RVA=0x0ACE9634  token=0x600078A  System.Void EmitGetArrayItem()
  RVA=0x0ACEAD58  token=0x600078B  System.Void EmitSetArrayItem()
  RVA=0x0ACEA8E0  token=0x600078C  System.Void EmitNewArray(System.Type elementType)
  RVA=0x0ACEA7D0  token=0x600078D  System.Void EmitNewArrayBounds(System.Type elementType, System.Int32 rank)
  RVA=0x0ACEA858  token=0x600078E  System.Void EmitNewArrayInit(System.Type elementType, System.Int32 elementCount)
  RVA=0x0ACE871C  token=0x600078F  System.Void EmitAdd(System.Type type, System.Boolean checked)
  RVA=0x0ACEB11C  token=0x6000790  System.Void EmitSub(System.Type type, System.Boolean checked)
  RVA=0x0ACEA738  token=0x6000791  System.Void EmitMul(System.Type type, System.Boolean checked)
  RVA=0x0ACE92AC  token=0x6000792  System.Void EmitDiv(System.Type type)
  RVA=0x0ACEA70C  token=0x6000793  System.Void EmitModulo(System.Type type)
  RVA=0x0ACE9608  token=0x6000794  System.Void EmitExclusiveOr(System.Type type)
  RVA=0x0ACE875C  token=0x6000795  System.Void EmitAnd(System.Type type)
  RVA=0x0ACEAC04  token=0x6000796  System.Void EmitOr(System.Type type)
  RVA=0x0ACE9A78  token=0x6000797  System.Void EmitLeftShift(System.Type type)
  RVA=0x0ACEAD2C  token=0x6000798  System.Void EmitRightShift(System.Type type)
  RVA=0x0ACE95D4  token=0x6000799  System.Void EmitEqual(System.Type type, System.Boolean liftedToNull)
  RVA=0x0ACEAA44  token=0x600079A  System.Void EmitNotEqual(System.Type type, System.Boolean liftedToNull)
  RVA=0x0ACE9AD8  token=0x600079B  System.Void EmitLessThan(System.Type type, System.Boolean liftedToNull)
  RVA=0x0ACE9AA4  token=0x600079C  System.Void EmitLessThanOrEqual(System.Type type, System.Boolean liftedToNull)
  RVA=0x0ACE9750  token=0x600079D  System.Void EmitGreaterThan(System.Type type, System.Boolean liftedToNull)
  RVA=0x0ACE971C  token=0x600079E  System.Void EmitGreaterThanOrEqual(System.Type type, System.Boolean liftedToNull)
  RVA=0x0ACEAB04  token=0x600079F  System.Void EmitNumericConvertChecked(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull)
  RVA=0x0ACEAB84  token=0x60007A0  System.Void EmitNumericConvertUnchecked(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull)
  RVA=0x0ACE911C  token=0x60007A1  System.Void EmitConvertToUnderlying(System.TypeCode to, System.Boolean isLiftedToNull)
  RVA=0x0ACE9088  token=0x60007A2  System.Void EmitCast(System.Type toType)
  RVA=0x0ACE9010  token=0x60007A3  System.Void EmitCastToEnum(System.Type toType)
  RVA=0x0ACE8F98  token=0x60007A4  System.Void EmitCastReferenceToEnum(System.Type toType)
  RVA=0x0ACEAA78  token=0x60007A5  System.Void EmitNot(System.Type type)
  RVA=0x0ACE9234  token=0x60007A6  System.Void EmitDefaultValue(System.Type type)
  RVA=0x0ACEA9B8  token=0x60007A7  System.Void EmitNew(System.Reflection.ConstructorInfo constructorInfo, System.Reflection.ParameterInfo[] parameters)
  RVA=0x0ACE8E68  token=0x60007A8  System.Void EmitByRefNew(System.Reflection.ConstructorInfo constructorInfo, System.Reflection.ParameterInfo[] parameters, System.Linq.Expressions.Interpreter.ByRefUpdater[] updaters)
  RVA=0x0ACE9190  token=0x60007A9  System.Void EmitCreateDelegate(System.Linq.Expressions.Interpreter.LightDelegateCreator creator)
  RVA=0x0ACEB27C  token=0x60007AA  System.Void EmitTypeEquals()
  RVA=0x0ACE8788  token=0x60007AB  System.Void EmitArrayLength()
  RVA=0x0ACEA7A4  token=0x60007AC  System.Void EmitNegate(System.Type type)
  RVA=0x0ACEA778  token=0x60007AD  System.Void EmitNegateChecked(System.Type type)
  RVA=0x0ACE9784  token=0x60007AE  System.Void EmitIncrement(System.Type type)
  RVA=0x0ACE9208  token=0x60007AF  System.Void EmitDecrement(System.Type type)
  RVA=0x0ACEB2D0  token=0x60007B0  System.Void EmitTypeIs(System.Type type)
  RVA=0x0ACEB204  token=0x60007B1  System.Void EmitTypeAs(System.Type type)
  RVA=0x0ACE9B0C  token=0x60007B2  System.Void EmitLoadField(System.Reflection.FieldInfo field)
  RVA=0x0ACEB4F8  token=0x60007B3  System.Linq.Expressions.Interpreter.Instruction GetLoadField(System.Reflection.FieldInfo field)
  RVA=0x0ACEADAC  token=0x60007B4  System.Void EmitStoreField(System.Reflection.FieldInfo field)
  RVA=0x0ACE8F38  token=0x60007B5  System.Void EmitCall(System.Reflection.MethodInfo method)
  RVA=0x0ACE8F08  token=0x60007B6  System.Void EmitCall(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters)
  RVA=0x0ACE8DAC  token=0x60007B7  System.Void EmitByRefCall(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters, System.Linq.Expressions.Interpreter.ByRefUpdater[] byrefArgs)
  RVA=0x0ACEAAA4  token=0x60007B8  System.Void EmitNullableCall(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters)
  RVA=0x0ACE8504  token=0x60007B9  System.Linq.Expressions.Interpreter.RuntimeLabel[] BuildRuntimeLabels()
  RVA=0x0ACEB940  token=0x60007BA  System.Linq.Expressions.Interpreter.BranchLabel MakeLabel()
  RVA=0x0ACEB3D8  token=0x60007BB  System.Void FixupBranch(System.Int32 branchIndex, System.Int32 offset)
  RVA=0x0ACEB3AC  token=0x60007BC  System.Int32 EnsureLabelIndex(System.Linq.Expressions.Interpreter.BranchLabel label)
  RVA=0x0ACEBA3C  token=0x60007BD  System.Int32 MarkRuntimeLabel()
  RVA=0x0ACEBA18  token=0x60007BE  System.Void MarkLabel(System.Linq.Expressions.Interpreter.BranchLabel label)
  RVA=0x0ACE9688  token=0x60007BF  System.Void EmitGoto(System.Linq.Expressions.Interpreter.BranchLabel label, System.Boolean hasResult, System.Boolean hasValue, System.Boolean labelTargetGetsValue)
  RVA=0x0ACE8D60  token=0x60007C0  System.Void EmitBranch(System.Linq.Expressions.Interpreter.OffsetInstruction instruction, System.Linq.Expressions.Interpreter.BranchLabel label)
  RVA=0x0ACE8C6C  token=0x60007C1  System.Void EmitBranch(System.Linq.Expressions.Interpreter.BranchLabel label)
  RVA=0x0ACE8CD8  token=0x60007C2  System.Void EmitBranch(System.Linq.Expressions.Interpreter.BranchLabel label, System.Boolean hasResult, System.Boolean hasValue)
  RVA=0x0ACE90B4  token=0x60007C3  System.Void EmitCoalescingBranch(System.Linq.Expressions.Interpreter.BranchLabel leftNotNull)
  RVA=0x0ACE8C04  token=0x60007C4  System.Void EmitBranchTrue(System.Linq.Expressions.Interpreter.BranchLabel elseLabel)
  RVA=0x0ACE8B9C  token=0x60007C5  System.Void EmitBranchFalse(System.Linq.Expressions.Interpreter.BranchLabel elseLabel)
  RVA=0x0ACEB1B0  token=0x60007C6  System.Void EmitThrow()
  RVA=0x0ACEB15C  token=0x60007C7  System.Void EmitThrowVoid()
  RVA=0x0ACEACD8  token=0x60007C8  System.Void EmitRethrow()
  RVA=0x0ACEAC84  token=0x60007C9  System.Void EmitRethrowVoid()
  RVA=0x0ACE95A4  token=0x60007CA  System.Void EmitEnterTryFinally(System.Linq.Expressions.Interpreter.BranchLabel finallyStartLabel)
  RVA=0x0ACE9500  token=0x60007CB  System.Void EmitEnterTryCatch()
  RVA=0x0ACE9524  token=0x60007CC  System.Linq.Expressions.Interpreter.EnterTryFaultInstruction EmitEnterTryFault(System.Linq.Expressions.Interpreter.BranchLabel tryEnd)
  RVA=0x0ACE9494  token=0x60007CD  System.Void EmitEnterFinally(System.Linq.Expressions.Interpreter.BranchLabel finallyStartLabel)
  RVA=0x0ACE9A24  token=0x60007CE  System.Void EmitLeaveFinally()
  RVA=0x0ACE9428  token=0x60007CF  System.Void EmitEnterFault(System.Linq.Expressions.Interpreter.BranchLabel faultStartLabel)
  RVA=0x0ACE99D0  token=0x60007D0  System.Void EmitLeaveFault()
  RVA=0x0ACE932C  token=0x60007D1  System.Void EmitEnterExceptionFilter()
  RVA=0x0ACE9900  token=0x60007D2  System.Void EmitLeaveExceptionFilter()
  RVA=0x0ACE9380  token=0x60007D3  System.Void EmitEnterExceptionHandlerNonVoid()
  RVA=0x0ACE93D4  token=0x60007D4  System.Void EmitEnterExceptionHandlerVoid()
  RVA=0x0ACE9954  token=0x60007D5  System.Void EmitLeaveExceptionHandler(System.Boolean hasValue, System.Linq.Expressions.Interpreter.BranchLabel tryExpressionEndLabel)
  RVA=-1  // generic def  token=0x60007D6  System.Void EmitIntSwitch(System.Collections.Generic.Dictionary<T,System.Int32> cases)
  RVA=0x0ACEB094  token=0x60007D7  System.Void EmitStringSwitch(System.Collections.Generic.Dictionary<System.String,System.Int32> cases, System.Runtime.CompilerServices.StrongBox<System.Int32> nullCase)
  RVA=0x0ACEC17C  token=0x60007D8  System.Void .ctor()
  RVA=0x0ACEC06C  token=0x60007D9  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.InterpretedFrame
TYPE:  sealed class
TOKEN: 0x2000168
SIZE:  0x50
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.InterpretedFrames_currentFrame  // static @ 0xffffffff
  private   readonly System.Linq.Expressions.Interpreter.InterpreterInterpreter  // 0x10
  private           System.Linq.Expressions.Interpreter.InterpretedFrame_parent  // 0x18
  private   readonly System.Int32[]                  _continuations  // 0x20
  private           System.Int32                    _continuationIndex  // 0x28
  private           System.Int32                    _pendingContinuation  // 0x2c
  private           System.Object                   _pendingValue  // 0x30
  public    readonly System.Object[]                 Data  // 0x38
  public    readonly System.Runtime.CompilerServices.IStrongBox[]Closure  // 0x40
  public            System.Int32                    StackIndex  // 0x48
  public            System.Int32                    InstructionIndex  // 0x4c
PROPERTIES:
  Name  get=0x0ACED078
  Parent  get=0x0385B100
METHODS:
  RVA=0x0ACECF7C  token=0x60007DD  System.Void .ctor(System.Linq.Expressions.Interpreter.Interpreter interpreter, System.Runtime.CompilerServices.IStrongBox[] closure)
  RVA=0x0ACEC49C  token=0x60007DE  System.Linq.Expressions.Interpreter.DebugInfo GetDebugInfo(System.Int32 instructionIndex)
  RVA=0x0ACEC840  token=0x60007E0  System.Void Push(System.Object value)
  RVA=0x0ACECAB8  token=0x60007E1  System.Void Push(System.Boolean value)
  RVA=0x0ACECA58  token=0x60007E2  System.Void Push(System.Int32 value)
  RVA=0x0ACEC928  token=0x60007E3  System.Void Push(System.Byte value)
  RVA=0x0ACEC9C0  token=0x60007E4  System.Void Push(System.SByte value)
  RVA=0x0ACEC890  token=0x60007E5  System.Void Push(System.Int16 value)
  RVA=0x0ACECB5C  token=0x60007E6  System.Void Push(System.UInt16 value)
  RVA=0x0ACEC75C  token=0x60007E7  System.Object Pop()
  RVA=0x0ACECD50  token=0x60007E8  System.Void SetStackDepth(System.Int32 depth)
  RVA=0x0ACEC6CC  token=0x60007E9  System.Object Peek()
  RVA=0x0ACEC3A0  token=0x60007EA  System.Void Dup()
  RVA=0x0ACEC500  token=0x60007EC  System.Collections.Generic.IEnumerable<System.Linq.Expressions.Interpreter.InterpretedFrameInfo> GetStackTraceDebugInfo()
  RVA=0x0ACECBF4  token=0x60007ED  System.Void SaveTraceToException(System.Exception exception)
  RVA=0x0ACEC414  token=0x60007EE  System.Linq.Expressions.Interpreter.InterpretedFrame Enter()
  RVA=0x0ACEC670  token=0x60007EF  System.Void Leave(System.Linq.Expressions.Interpreter.InterpretedFrame prevFrame)
  RVA=0x03BE2580  token=0x60007F0  System.Boolean IsJumpHappened()
  RVA=0x04D9EBE0  token=0x60007F1  System.Void RemoveContinuation()
  RVA=0x0ACEC794  token=0x60007F2  System.Void PushContinuation(System.Int32 continuation)
  RVA=0x0ACECD74  token=0x60007F3  System.Int32 YieldToCurrentContinuation()
  RVA=0x0ACECDF0  token=0x60007F4  System.Int32 YieldToPendingContinuation()
  RVA=0x0ACEC7C8  token=0x60007F5  System.Void PushPendingContinuation()
  RVA=0x0ACEC6FC  token=0x60007F6  System.Void PopPendingContinuation()
  RVA=0x0ACEC570  token=0x60007F7  System.Int32 Goto(System.Int32 labelIndex, System.Object value, System.Boolean gotoExceptionHandler)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.Interpreter
TYPE:  sealed class
TOKEN: 0x200016A
SIZE:  0x68
FIELDS:
  private   static readonly System.Object                   NoValue  // static @ 0x0
  private   readonly System.Linq.Expressions.Interpreter.InstructionArray_instructions  // 0x10
  private   readonly System.Object[]                 _objects  // 0x38
  private   readonly System.Linq.Expressions.Interpreter.RuntimeLabel[]_labels  // 0x40
  private   readonly System.Linq.Expressions.Interpreter.DebugInfo[]_debugInfos  // 0x48
  private   readonly System.String                   <Name>k__BackingField  // 0x50
  private   readonly System.Int32                    <LocalCount>k__BackingField  // 0x58
  private   readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable><ClosureVariables>k__BackingField  // 0x60
PROPERTIES:
  Name  get=0x04D86270
  LocalCount  get=0x04D86570
  ClosureSize  get=0x0ACED20C
  Instructions  get=0x02B2D2D0
  ClosureVariables  get=0x04D86280
METHODS:
  RVA=0x0ACED168  token=0x6000800  System.Void .ctor(System.String name, System.Linq.Expressions.Interpreter.LocalVariables locals, System.Linq.Expressions.Interpreter.InstructionArray instructions, System.Linq.Expressions.Interpreter.DebugInfo[] debugInfos)
  RVA=0x0ACED094  token=0x6000806  System.Void Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACED0F8  token=0x6000807  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LabelInfo
TYPE:  sealed class
TOKEN: 0x200016B
SIZE:  0x38
FIELDS:
  private   readonly System.Linq.Expressions.LabelTarget_node  // 0x10
  private           System.Linq.Expressions.Interpreter.BranchLabel_label  // 0x18
  private           System.Object                   _definitions  // 0x20
  private   readonly System.Collections.Generic.List<System.Linq.Expressions.Interpreter.LabelScopeInfo>_references  // 0x28
  private           System.Boolean                  _acrossBlockJump  // 0x30
PROPERTIES:
  HasDefinitions  get=0x09B00070
  HasMultipleDefinitions  get=0x0ACEDCB4
METHODS:
  RVA=0x0ACEDC24  token=0x6000808  System.Void .ctor(System.Linq.Expressions.LabelTarget node)
  RVA=0x0ACED83C  token=0x6000809  System.Linq.Expressions.Interpreter.BranchLabel GetLabel(System.Linq.Expressions.Interpreter.LightCompiler compiler)
  RVA=0x0ACED858  token=0x600080A  System.Void Reference(System.Linq.Expressions.Interpreter.LabelScopeInfo block)
  RVA=0x0ACED368  token=0x600080B  System.Void Define(System.Linq.Expressions.Interpreter.LabelScopeInfo block)
  RVA=0x0ACED93C  token=0x600080C  System.Void ValidateJump(System.Linq.Expressions.Interpreter.LabelScopeInfo reference)
  RVA=0x0ACED8C4  token=0x600080D  System.Void ValidateFinish()
  RVA=0x0ACED660  token=0x600080E  System.Void EnsureLabel(System.Linq.Expressions.Interpreter.LightCompiler compiler)
  RVA=0x0ACED5E8  token=0x600080F  System.Boolean DefinedIn(System.Linq.Expressions.Interpreter.LabelScopeInfo scope)
  RVA=0x0ACED6A0  token=0x6000811  System.Linq.Expressions.Interpreter.LabelScopeInfo FirstDefinition()
  RVA=0x0ACED24C  token=0x6000812  System.Void AddDefinition(System.Linq.Expressions.Interpreter.LabelScopeInfo scope)
  RVA=-1  // generic def  token=0x6000814  T CommonNode(T first, T second, System.Func<T,T> parent)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LabelScopeKind
TYPE:  sealed struct
TOKEN: 0x200016D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindStatement  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindBlock  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindSwitch  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindLambda  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindTry  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindCatch  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindFinally  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindFilter  // const
  public    static  System.Linq.Expressions.Interpreter.LabelScopeKindExpression  // const
METHODS:
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LabelScopeInfo
TYPE:  sealed class
TOKEN: 0x200016E
SIZE:  0x28
FIELDS:
  private           System.Linq.Expressions.Interpreter.HybridReferenceDictionary<System.Linq.Expressions.LabelTarget,System.Linq.Expressions.Interpreter.LabelInfo>_labels  // 0x10
  private   readonly System.Linq.Expressions.Interpreter.LabelScopeKindKind  // 0x18
  private   readonly System.Linq.Expressions.Interpreter.LabelScopeInfoParent  // 0x20
PROPERTIES:
  CanJumpInto  get=0x0ACEDE88
METHODS:
  RVA=0x0ACEDE68  token=0x6000818  System.Void .ctor(System.Linq.Expressions.Interpreter.LabelScopeInfo parent, System.Linq.Expressions.Interpreter.LabelScopeKind kind)
  RVA=0x0ACEDD9C  token=0x600081A  System.Boolean ContainsTarget(System.Linq.Expressions.LabelTarget target)
  RVA=0x0ACEDDF4  token=0x600081B  System.Boolean TryGetLabelInfo(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Interpreter.LabelInfo& info)
  RVA=0x0ACEDCF8  token=0x600081C  System.Void AddLabelInfo(System.Linq.Expressions.LabelTarget target, System.Linq.Expressions.Interpreter.LabelInfo info)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LeftShiftInstruction
TYPE:  abstract class
TOKEN: 0x200016F
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACEE360
METHODS:
  RVA=0x041E1670  token=0x6000820  System.Void .ctor()
  RVA=0x0ACEDF50  token=0x6000821  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LessThanInstruction
TYPE:  abstract class
TOKEN: 0x2000178
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Object                   _nullValue  // 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Char  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSByte  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt16  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullChar  // static @ 0x68
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt32  // static @ 0x70
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt64  // static @ 0x78
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullByte  // static @ 0x80
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt16  // static @ 0x88
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt32  // static @ 0x90
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt64  // static @ 0x98
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSingle  // static @ 0xa0
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullDouble  // static @ 0xa8
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACEF7C8
METHODS:
  RVA=0x053908C0  token=0x6000835  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEEB18  token=0x6000836  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LessThanOrEqualInstruction
TYPE:  abstract class
TOKEN: 0x2000184
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Object                   _nullValue  // 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Char  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSByte  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt16  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullChar  // static @ 0x68
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt32  // static @ 0x70
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullInt64  // static @ 0x78
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullByte  // static @ 0x80
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt16  // static @ 0x88
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt32  // static @ 0x90
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullUInt64  // static @ 0x98
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullSingle  // static @ 0xa0
  private   static  System.Linq.Expressions.Interpreter.Instructions_liftedToNullDouble  // static @ 0xa8
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACF08F8
METHODS:
  RVA=0x053908C0  token=0x6000850  System.Void .ctor(System.Object nullValue)
  RVA=0x0ACEFC48  token=0x6000851  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ExceptionFilter
TYPE:  sealed class
TOKEN: 0x2000190
SIZE:  0x20
FIELDS:
  public    readonly System.Int32                    LabelIndex  // 0x10
  public    readonly System.Int32                    StartIndex  // 0x14
  public    readonly System.Int32                    EndIndex  // 0x18
METHODS:
  RVA=0x04D88580  token=0x6000868  System.Void .ctor(System.Int32 labelIndex, System.Int32 start, System.Int32 end)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ExceptionHandler
TYPE:  sealed class
TOKEN: 0x2000191
SIZE:  0x30
FIELDS:
  private   readonly System.Type                     _exceptionType  // 0x10
  public    readonly System.Int32                    LabelIndex  // 0x18
  public    readonly System.Int32                    HandlerStartIndex  // 0x1c
  public    readonly System.Int32                    HandlerEndIndex  // 0x20
  public    readonly System.Linq.Expressions.Interpreter.ExceptionFilterFilter  // 0x28
METHODS:
  RVA=0x0ACE4C8C  token=0x6000869  System.Void .ctor(System.Int32 labelIndex, System.Int32 handlerStartIndex, System.Int32 handlerEndIndex, System.Type exceptionType, System.Linq.Expressions.Interpreter.ExceptionFilter filter)
  RVA=0x0ACE4B80  token=0x600086A  System.Boolean Matches(System.Type exceptionType)
  RVA=0x0ACE4BA4  token=0x600086B  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.TryCatchFinallyHandler
TYPE:  sealed class
TOKEN: 0x2000192
SIZE:  0x30
FIELDS:
  private   readonly System.Int32                    TryStartIndex  // 0x10
  private   readonly System.Int32                    TryEndIndex  // 0x14
  private   readonly System.Int32                    FinallyStartIndex  // 0x18
  private   readonly System.Int32                    FinallyEndIndex  // 0x1c
  private   readonly System.Int32                    GotoEndTargetIndex  // 0x20
  private   readonly System.Linq.Expressions.Interpreter.ExceptionHandler[]_handlers  // 0x28
PROPERTIES:
  IsFinallyBlockExist  get=0x0ACFD008
  Handlers  get=0x04D86240
  IsCatchBlockExist  get=0x068C60A8
METHODS:
  RVA=0x0ACFCFDC  token=0x600086F  System.Void .ctor(System.Int32 tryStart, System.Int32 tryEnd, System.Int32 gotoEndTargetIndex, System.Linq.Expressions.Interpreter.ExceptionHandler[] handlers)
  RVA=0x0ACFCFB0  token=0x6000870  System.Void .ctor(System.Int32 tryStart, System.Int32 tryEnd, System.Int32 gotoEndLabelIndex, System.Int32 finallyStart, System.Int32 finallyEnd, System.Linq.Expressions.Interpreter.ExceptionHandler[] handlers)
  RVA=0x0ACFCE54  token=0x6000871  System.Boolean HasHandler(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Exception exception, System.Linq.Expressions.Interpreter.ExceptionHandler& handler, System.Object& unwrappedException)
  RVA=0x0ACFCC8C  token=0x6000872  System.Boolean FilterPasses(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object& exception, System.Linq.Expressions.Interpreter.ExceptionFilter filter)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.TryFaultHandler
TYPE:  sealed class
TOKEN: 0x2000193
SIZE:  0x20
FIELDS:
  private   readonly System.Int32                    TryStartIndex  // 0x10
  private   readonly System.Int32                    TryEndIndex  // 0x14
  private   readonly System.Int32                    FinallyStartIndex  // 0x18
  private   readonly System.Int32                    FinallyEndIndex  // 0x1c
METHODS:
  RVA=0x04D8B5C0  token=0x6000873  System.Void .ctor(System.Int32 tryStart, System.Int32 tryEnd, System.Int32 finallyStart, System.Int32 finallyEnd)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.RethrowException
TYPE:  sealed class
TOKEN: 0x2000194
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0ACFCB3C  token=0x6000874  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.DebugInfo
TYPE:  sealed class
TOKEN: 0x2000195
SIZE:  0x30
FIELDS:
  public            System.Int32                    StartLine  // 0x10
  public            System.Int32                    EndLine  // 0x14
  public            System.Int32                    Index  // 0x18
  public            System.String                   FileName  // 0x20
  public            System.Boolean                  IsClear  // 0x28
  private   static readonly System.Linq.Expressions.Interpreter.DebugInfo.DebugInfoComparers_debugComparer  // static @ 0x0
METHODS:
  RVA=0x0ACE43B4  token=0x6000875  System.Linq.Expressions.Interpreter.DebugInfo GetMatchingDebugInfo(System.Linq.Expressions.Interpreter.DebugInfo[] debugInfos, System.Int32 index)
  RVA=0x0ACE4474  token=0x6000876  System.String ToString()
  RVA=0x041E1670  token=0x6000877  System.Void .ctor()
  RVA=0x0ACE463C  token=0x6000878  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.InterpretedFrameInfo
TYPE:  sealed struct
TOKEN: 0x2000197
SIZE:  0x20
FIELDS:
  private   readonly System.String                   _methodName  // 0x10
  private   readonly System.Linq.Expressions.Interpreter.DebugInfo_debugInfo  // 0x18
METHODS:
  RVA=0x039CF630  token=0x600087B  System.Void .ctor(System.String methodName, System.Linq.Expressions.Interpreter.DebugInfo info)
  RVA=0x0ACEC32C  token=0x600087C  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LightCompiler
TYPE:  sealed class
TOKEN: 0x2000198
SIZE:  0x50
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.InstructionList_instructions  // 0x10
  private   readonly System.Linq.Expressions.Interpreter.LocalVariables_locals  // 0x18
  private   readonly System.Collections.Generic.List<System.Linq.Expressions.Interpreter.DebugInfo>_debugInfos  // 0x20
  private   readonly System.Linq.Expressions.Interpreter.HybridReferenceDictionary<System.Linq.Expressions.LabelTarget,System.Linq.Expressions.Interpreter.LabelInfo>_treeLabels  // 0x28
  private           System.Linq.Expressions.Interpreter.LabelScopeInfo_labelBlock  // 0x30
  private   readonly System.Collections.Generic.Stack<System.Linq.Expressions.ParameterExpression>_exceptionForRethrowStack  // 0x38
  private   readonly System.Linq.Expressions.Interpreter.LightCompiler_parent  // 0x40
  private   readonly System.Linq.Expressions.StackGuard_guard  // 0x48
  private   static readonly System.Linq.Expressions.Interpreter.LocalDefinition[]s_emptyLocals  // static @ 0x0
PROPERTIES:
  Instructions  get=0x02B2ECC0
METHODS:
  RVA=0x0ACFC864  token=0x600087D  System.Void .ctor()
  RVA=0x0ACFC834  token=0x600087E  System.Void .ctor(System.Linq.Expressions.Interpreter.LightCompiler parent)
  RVA=0x0ACF9B64  token=0x6000880  System.Linq.Expressions.Interpreter.LightDelegateCreator CompileTop(System.Linq.Expressions.LambdaExpression node)
  RVA=0x0ACFBED4  token=0x6000881  System.Linq.Expressions.Interpreter.Interpreter MakeInterpreter(System.String lambdaName)
  RVA=0x0ACF3FC0  token=0x6000882  System.Void CompileConstantExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF4C34  token=0x6000883  System.Void CompileDefaultExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF4B54  token=0x6000884  System.Void CompileDefaultExpression(System.Type type)
  RVA=0x0ACFBB60  token=0x6000885  System.Linq.Expressions.Interpreter.LocalVariable EnsureAvailableForClosure(System.Linq.Expressions.ParameterExpression expr)
  RVA=0x0ACFC26C  token=0x6000886  System.Linq.Expressions.Interpreter.LocalVariable ResolveLocal(System.Linq.Expressions.ParameterExpression variable)
  RVA=0x0ACF4D34  token=0x6000887  System.Void CompileGetVariable(System.Linq.Expressions.ParameterExpression variable)
  RVA=0x0ACFB718  token=0x6000888  System.Void EmitCopyValueType(System.Type valueType)
  RVA=0x0ACFBE6C  token=0x6000889  System.Void LoadLocalNoValueTypeCopy(System.Linq.Expressions.ParameterExpression variable)
  RVA=0x0ACFC130  token=0x600088A  System.Boolean MaybeMutableValueType(System.Type type)
  RVA=0x0ACF4CE4  token=0x600088B  System.Void CompileGetBoxedVariable(System.Linq.Expressions.ParameterExpression variable)
  RVA=0x0ACF8CAC  token=0x600088C  System.Void CompileSetVariable(System.Linq.Expressions.ParameterExpression variable, System.Boolean isVoid)
  RVA=0x0ACF8794  token=0x600088D  System.Void CompileParameterExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF30B4  token=0x600088E  System.Void CompileBlockExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid)
  RVA=0x0ACF31CC  token=0x600088F  System.Linq.Expressions.Interpreter.LocalDefinition[] CompileBlockStart(System.Linq.Expressions.BlockExpression node)
  RVA=0x0ACF3028  token=0x6000890  System.Void CompileBlockEnd(System.Linq.Expressions.Interpreter.LocalDefinition[] locals)
  RVA=0x0ACF51B4  token=0x6000891  System.Void CompileIndexExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFB798  token=0x6000892  System.Void EmitIndexGet(System.Linq.Expressions.IndexExpression index)
  RVA=0x0ACF4F1C  token=0x6000893  System.Void CompileIndexAssignment(System.Linq.Expressions.BinaryExpression node, System.Boolean asVoid)
  RVA=0x0ACF69CC  token=0x6000894  System.Void CompileMemberAssignment(System.Linq.Expressions.BinaryExpression node, System.Boolean asVoid)
  RVA=0x0ACF6664  token=0x6000895  System.Void CompileMemberAssignment(System.Boolean asVoid, System.Reflection.MemberInfo refMember, System.Linq.Expressions.Expression value, System.Boolean forBinding)
  RVA=0x0ACFB488  token=0x6000896  System.Void CompileVariableAssignment(System.Linq.Expressions.BinaryExpression node, System.Boolean asVoid)
  RVA=0x0ACF2130  token=0x6000897  System.Void CompileAssignBinaryExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid)
  RVA=0x0ACF2234  token=0x6000898  System.Void CompileBinaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF4C68  token=0x6000899  System.Void CompileEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftedToNull)
  RVA=0x0ACF8664  token=0x600089A  System.Void CompileNotEqual(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Boolean liftedToNull)
  RVA=0x0ACF3C08  token=0x600089B  System.Void CompileComparison(System.Linq.Expressions.BinaryExpression node)
  RVA=0x0ACF1B6C  token=0x600089C  System.Void CompileArithmetic(System.Linq.Expressions.ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
  RVA=0x0ACF44D0  token=0x600089D  System.Void CompileConvertUnaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF403C  token=0x600089E  System.Void CompileConvertToType(System.Type typeFrom, System.Type typeTo, System.Boolean isChecked, System.Boolean isLiftedToNull)
  RVA=0x0ACF86E0  token=0x600089F  System.Void CompileNotExpression(System.Linq.Expressions.UnaryExpression node)
  RVA=0x0ACFAF78  token=0x60008A0  System.Void CompileUnaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFBA5C  token=0x60008A1  System.Void EmitUnaryMethodCall(System.Linq.Expressions.UnaryExpression node)
  RVA=0x0ACFB884  token=0x60008A2  System.Void EmitUnaryBoolCheck(System.Linq.Expressions.UnaryExpression node)
  RVA=0x0ACF1B14  token=0x60008A3  System.Void CompileAndAlsoBinaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF873C  token=0x60008A4  System.Void CompileOrElseBinaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF6380  token=0x60008A5  System.Void CompileLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression b, System.Boolean andAlso)
  RVA=0x0ACF7600  token=0x60008A6  System.Void CompileMethodLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression expr, System.Boolean andAlso)
  RVA=0x0ACF585C  token=0x60008A7  System.Void CompileLiftedLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression node, System.Boolean andAlso)
  RVA=0x0ACFB360  token=0x60008A8  System.Void CompileUnliftedLogicalBinaryExpression(System.Linq.Expressions.BinaryExpression expr, System.Boolean andAlso)
  RVA=0x0ACF3DD0  token=0x60008A9  System.Void CompileConditionalExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid)
  RVA=0x0ACF64C0  token=0x60008AA  System.Void CompileLoopExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF92F8  token=0x60008AB  System.Void CompileSwitchExpression(System.Linq.Expressions.Expression expr)
  RVA=-1  // generic def  token=0x60008AC  System.Void CompileIntSwitchExpression(System.Linq.Expressions.SwitchExpression node)
  RVA=0x0ACF8D58  token=0x60008AD  System.Void CompileStringSwitchExpression(System.Linq.Expressions.SwitchExpression node)
  RVA=0x0ACF5468  token=0x60008AE  System.Void CompileLabelExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF4D7C  token=0x60008AF  System.Void CompileGotoExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFC1A4  token=0x60008B0  System.Void PushLabelBlock(System.Linq.Expressions.Interpreter.LabelScopeKind type)
  RVA=0x0ACFC17C  token=0x60008B1  System.Void PopLabelBlock(System.Linq.Expressions.Interpreter.LabelScopeKind kind)
  RVA=0x0ACFBCA4  token=0x60008B2  System.Linq.Expressions.Interpreter.LabelInfo EnsureLabel(System.Linq.Expressions.LabelTarget node)
  RVA=0x0ACFC22C  token=0x60008B3  System.Linq.Expressions.Interpreter.LabelInfo ReferenceLabel(System.Linq.Expressions.LabelTarget node)
  RVA=0x0ACFB688  token=0x60008B4  System.Linq.Expressions.Interpreter.LabelInfo DefineLabel(System.Linq.Expressions.LabelTarget node)
  RVA=0x0ACFC400  token=0x60008B5  System.Boolean TryPushLabelBlock(System.Linq.Expressions.Expression node)
  RVA=0x0ACFB58C  token=0x60008B6  System.Void DefineBlockLabels(System.Linq.Expressions.Expression node)
  RVA=0x0ACF1284  token=0x60008B7  System.Void CheckRethrow()
  RVA=0x0ACF9A64  token=0x60008B8  System.Void CompileThrowUnaryExpression(System.Linq.Expressions.Expression expr, System.Boolean asVoid)
  RVA=0x0ACF9D10  token=0x60008B9  System.Void CompileTryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFA918  token=0x60008BA  System.Void CompileTryFaultExpression(System.Linq.Expressions.TryExpression expr)
  RVA=0x0ACF7588  token=0x60008BB  System.Void CompileMethodCallExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF713C  token=0x60008BC  System.Void CompileMethodCallExpression(System.Linq.Expressions.Expression object, System.Reflection.MethodInfo method, System.Linq.Expressions.IArgumentProvider arguments)
  RVA=0x0ACF1D90  token=0x60008BD  System.Linq.Expressions.Interpreter.ByRefUpdater CompileArrayIndexAddress(System.Linq.Expressions.Expression array, System.Linq.Expressions.Expression index, System.Int32 argumentIndex)
  RVA=0x0ACFB874  token=0x60008BE  System.Void EmitThisForMethodCall(System.Linq.Expressions.Expression node)
  RVA=0x0ACFC2C4  token=0x60008BF  System.Boolean ShouldWritebackNode(System.Linq.Expressions.Expression node)
  RVA=0x0ACF12D0  token=0x60008C0  System.Linq.Expressions.Interpreter.ByRefUpdater CompileAddress(System.Linq.Expressions.Expression node, System.Int32 index)
  RVA=0x0ACF7774  token=0x60008C1  System.Linq.Expressions.Interpreter.ByRefUpdater CompileMultiDimArrayAccess(System.Linq.Expressions.Expression array, System.Linq.Expressions.IArgumentProvider arguments, System.Int32 index)
  RVA=0x0ACF7D30  token=0x60008C2  System.Void CompileNewExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF6A78  token=0x60008C3  System.Void CompileMemberExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF6EA0  token=0x60008C4  System.Void CompileMember(System.Linq.Expressions.Expression from, System.Reflection.MemberInfo member, System.Boolean forBinding)
  RVA=0x0ACF7AD4  token=0x60008C5  System.Void CompileNewArrayExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF4AA8  token=0x60008C6  System.Void CompileDebugInfoExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF8AE4  token=0x60008C7  System.Void CompileRuntimeVariablesExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF5608  token=0x60008C8  System.Void CompileLambdaExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF36E8  token=0x60008C9  System.Void CompileCoalesceBinaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF5274  token=0x60008CA  System.Void CompileInvocationExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF6000  token=0x60008CB  System.Void CompileListInitExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF6074  token=0x60008CC  System.Void CompileListInit(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit> initializers)
  RVA=0x0ACF6AF0  token=0x60008CD  System.Void CompileMemberInitExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF6B64  token=0x60008CE  System.Void CompileMemberInit(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding> bindings)
  RVA=0x0ACFBD74  token=0x60008CF  System.Type GetMemberType(System.Reflection.MemberInfo member)
  RVA=0x0ACF87E8  token=0x60008D0  System.Void CompileQuoteUnaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFB25C  token=0x60008D1  System.Void CompileUnboxUnaryExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFABCC  token=0x60008D2  System.Void CompileTypeEqualExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFAB74  token=0x60008D3  System.Void CompileTypeAsExpression(System.Linq.Expressions.UnaryExpression node)
  RVA=0x0ACFAD70  token=0x60008D4  System.Void CompileTypeIsExpression(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFB568  token=0x60008D5  System.Void Compile(System.Linq.Expressions.Expression expr, System.Boolean asVoid)
  RVA=0x0ACF1FD0  token=0x60008D6  System.Void CompileAsVoid(System.Linq.Expressions.Expression expr)
  RVA=0x0ACF8004  token=0x60008D7  System.Void CompileNoLabelPush(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFB508  token=0x60008D8  System.Void Compile(System.Linq.Expressions.Expression expr)
  RVA=0x0ACFC7D4  token=0x60008D9  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ByRefUpdater
TYPE:  abstract class
TOKEN: 0x200019B
SIZE:  0x18
FIELDS:
  public    readonly System.Int32                    ArgumentIndex  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x60008E6  System.Void .ctor(System.Int32 argumentIndex)
  RVA=-1  // abstract  token=0x60008E7  System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value)
  RVA=0x041E1670  token=0x60008E8  System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ParameterByRefUpdater
TYPE:  sealed class
TOKEN: 0x200019C
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.ByRefUpdater
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.LocalVariable_parameter  // 0x18
METHODS:
  RVA=0x0AD10340  token=0x60008E9  System.Void .ctor(System.Linq.Expressions.Interpreter.LocalVariable parameter, System.Int32 argumentIndex)
  RVA=0x0AD10228  token=0x60008EA  System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ArrayByRefUpdater
TYPE:  sealed class
TOKEN: 0x200019D
SIZE:  0x38
EXTENDS: System.Linq.Expressions.Interpreter.ByRefUpdater
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.LocalDefinition_array  // 0x18
  private   readonly System.Linq.Expressions.Interpreter.LocalDefinition_index  // 0x28
METHODS:
  RVA=0x0ACFD3E0  token=0x60008EB  System.Void .ctor(System.Linq.Expressions.Interpreter.LocalDefinition array, System.Linq.Expressions.Interpreter.LocalDefinition index, System.Int32 argumentIndex)
  RVA=0x0ACFD308  token=0x60008EC  System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value)
  RVA=0x0ACFD278  token=0x60008ED  System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.FieldByRefUpdater
TYPE:  sealed class
TOKEN: 0x200019E
SIZE:  0x38
EXTENDS: System.Linq.Expressions.Interpreter.ByRefUpdater
FIELDS:
  private   readonly System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition>_object  // 0x18
  private   readonly System.Reflection.FieldInfo     _field  // 0x30
METHODS:
  RVA=0x0AD0560C  token=0x60008EE  System.Void .ctor(System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> obj, System.Reflection.FieldInfo field, System.Int32 argumentIndex)
  RVA=0x0AD05570  token=0x60008EF  System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value)
  RVA=0x0AD054D8  token=0x60008F0  System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.PropertyByRefUpdater
TYPE:  sealed class
TOKEN: 0x200019F
SIZE:  0x38
EXTENDS: System.Linq.Expressions.Interpreter.ByRefUpdater
FIELDS:
  private   readonly System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition>_object  // 0x18
  private   readonly System.Reflection.PropertyInfo  _property  // 0x30
METHODS:
  RVA=0x0AD0560C  token=0x60008F1  System.Void .ctor(System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> obj, System.Reflection.PropertyInfo property, System.Int32 argumentIndex)
  RVA=0x0AD10524  token=0x60008F2  System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value)
  RVA=0x0AD1048C  token=0x60008F3  System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.IndexMethodByRefUpdater
TYPE:  sealed class
TOKEN: 0x20001A0
SIZE:  0x40
EXTENDS: System.Linq.Expressions.Interpreter.ByRefUpdater
FIELDS:
  private   readonly System.Reflection.MethodInfo    _indexer  // 0x18
  private   readonly System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition>_obj  // 0x20
  private   readonly System.Linq.Expressions.Interpreter.LocalDefinition[]_args  // 0x38
METHODS:
  RVA=0x0AD05DAC  token=0x60008F4  System.Void .ctor(System.Nullable<System.Linq.Expressions.Interpreter.LocalDefinition> obj, System.Linq.Expressions.Interpreter.LocalDefinition[] args, System.Reflection.MethodInfo indexer, System.Int32 argumentIndex)
  RVA=0x0AD05BBC  token=0x60008F5  System.Void Update(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Object value)
  RVA=0x0AD05AC4  token=0x60008F6  System.Void UndefineTemps(System.Linq.Expressions.Interpreter.InstructionList instructions, System.Linq.Expressions.Interpreter.LocalVariables locals)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LightDelegateCreator
TYPE:  sealed class
TOKEN: 0x20001A1
SIZE:  0x20
FIELDS:
  private   readonly System.Linq.Expressions.LambdaExpression_lambda  // 0x10
  private   readonly System.Linq.Expressions.Interpreter.Interpreter<Interpreter>k__BackingField  // 0x18
PROPERTIES:
  Interpreter  get=0x0385B100
METHODS:
  RVA=0x03739160  token=0x60008F7  System.Void .ctor(System.Linq.Expressions.Interpreter.Interpreter interpreter, System.Linq.Expressions.LambdaExpression lambda)
  RVA=0x0AD06880  token=0x60008F9  System.Delegate CreateDelegate()
  RVA=0x0AD0688C  token=0x60008FA  System.Delegate CreateDelegate(System.Runtime.CompilerServices.IStrongBox[] closure)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LightLambda
TYPE:  class
TOKEN: 0x20001A2
SIZE:  0x28
FIELDS:
  private   readonly System.Runtime.CompilerServices.IStrongBox[]_closure  // 0x10
  private   readonly System.Linq.Expressions.Interpreter.Interpreter_interpreter  // 0x18
  private   static readonly System.Dynamic.Utils.CacheDict<System.Type,System.Func<System.Linq.Expressions.Interpreter.LightLambda,System.Delegate>>_runCache  // static @ 0x0
  private   readonly System.Linq.Expressions.Interpreter.LightDelegateCreator_delegateCreator  // 0x20
PROPERTIES:
  DebugView  get=0x0AD07F20
METHODS:
  RVA=-1  // generic def  token=0x60008FB  TRet Run0()
  RVA=0x0AD079C4  token=0x60008FC  System.Void RunVoid0()
  RVA=-1  // generic def  token=0x60008FD  System.Delegate MakeRun0(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=0x0AD07950  token=0x60008FE  System.Delegate MakeRunVoid0(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x60008FF  TRet Run1(T0 arg0)
  RVA=-1  // generic def  token=0x6000900  System.Void RunVoid1(T0 arg0)
  RVA=-1  // generic def  token=0x6000901  System.Delegate MakeRun1(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000902  System.Delegate MakeRunVoid1(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000903  TRet Run2(T0 arg0, T1 arg1)
  RVA=-1  // generic def  token=0x6000904  System.Void RunVoid2(T0 arg0, T1 arg1)
  RVA=-1  // generic def  token=0x6000905  System.Delegate MakeRun2(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000906  System.Delegate MakeRunVoid2(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000907  TRet Run3(T0 arg0, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x6000908  System.Void RunVoid3(T0 arg0, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x6000909  System.Delegate MakeRun3(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600090A  System.Delegate MakeRunVoid3(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600090B  TRet Run4(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // generic def  token=0x600090C  System.Void RunVoid4(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // generic def  token=0x600090D  System.Delegate MakeRun4(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600090E  System.Delegate MakeRunVoid4(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600090F  TRet Run5(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=-1  // generic def  token=0x6000910  System.Void RunVoid5(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=-1  // generic def  token=0x6000911  System.Delegate MakeRun5(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000912  System.Delegate MakeRunVoid5(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000913  TRet Run6(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
  RVA=-1  // generic def  token=0x6000914  System.Void RunVoid6(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
  RVA=-1  // generic def  token=0x6000915  System.Delegate MakeRun6(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000916  System.Delegate MakeRunVoid6(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000917  TRet Run7(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
  RVA=-1  // generic def  token=0x6000918  System.Void RunVoid7(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
  RVA=-1  // generic def  token=0x6000919  System.Delegate MakeRun7(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600091A  System.Delegate MakeRunVoid7(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600091B  TRet Run8(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
  RVA=-1  // generic def  token=0x600091C  System.Void RunVoid8(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
  RVA=-1  // generic def  token=0x600091D  System.Delegate MakeRun8(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600091E  System.Delegate MakeRunVoid8(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600091F  TRet Run9(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
  RVA=-1  // generic def  token=0x6000920  System.Void RunVoid9(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
  RVA=-1  // generic def  token=0x6000921  System.Delegate MakeRun9(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000922  System.Delegate MakeRunVoid9(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000923  TRet Run10(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
  RVA=-1  // generic def  token=0x6000924  System.Void RunVoid10(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
  RVA=-1  // generic def  token=0x6000925  System.Delegate MakeRun10(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000926  System.Delegate MakeRunVoid10(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000927  TRet Run11(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
  RVA=-1  // generic def  token=0x6000928  System.Void RunVoid11(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
  RVA=-1  // generic def  token=0x6000929  System.Delegate MakeRun11(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600092A  System.Delegate MakeRunVoid11(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600092B  TRet Run12(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
  RVA=-1  // generic def  token=0x600092C  System.Void RunVoid12(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
  RVA=-1  // generic def  token=0x600092D  System.Delegate MakeRun12(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600092E  System.Delegate MakeRunVoid12(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600092F  TRet Run13(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
  RVA=-1  // generic def  token=0x6000930  System.Void RunVoid13(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
  RVA=-1  // generic def  token=0x6000931  System.Delegate MakeRun13(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000932  System.Delegate MakeRunVoid13(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000933  TRet Run14(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
  RVA=-1  // generic def  token=0x6000934  System.Void RunVoid14(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
  RVA=-1  // generic def  token=0x6000935  System.Delegate MakeRun14(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000936  System.Delegate MakeRunVoid14(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x6000937  TRet Run15(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
  RVA=-1  // generic def  token=0x6000938  System.Void RunVoid15(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
  RVA=-1  // generic def  token=0x6000939  System.Delegate MakeRun15(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=-1  // generic def  token=0x600093A  System.Delegate MakeRunVoid15(System.Linq.Expressions.Interpreter.LightLambda lambda)
  RVA=0x0AD07ED8  token=0x600093B  System.Void .ctor(System.Linq.Expressions.Interpreter.LightDelegateCreator delegateCreator, System.Runtime.CompilerServices.IStrongBox[] closure)
  RVA=0x0AD0703C  token=0x600093D  System.Func<System.Linq.Expressions.Interpreter.LightLambda,System.Delegate> GetRunDelegateCtor(System.Type delegateType)
  RVA=0x0AD072A0  token=0x600093E  System.Func<System.Linq.Expressions.Interpreter.LightLambda,System.Delegate> MakeRunDelegateCtor(System.Type delegateType)
  RVA=0x0AD06918  token=0x600093F  System.Delegate CreateCustomDelegate(System.Type delegateType)
  RVA=0x0AD071BC  token=0x6000940  System.Delegate MakeDelegate(System.Type delegateType)
  RVA=0x0AD0722C  token=0x6000941  System.Linq.Expressions.Interpreter.InterpretedFrame MakeFrame()
  RVA=-1  // generic def  token=0x6000942  System.Void RunVoidRef2(T0& arg0, T1& arg1)
  RVA=0x0AD07C64  token=0x6000943  System.Object Run(System.Object[] arguments)
  RVA=0x0AD07AA0  token=0x6000944  System.Object RunVoid(System.Object[] arguments)
  RVA=0x0AD07E44  token=0x6000945  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.IBoxableInstruction
TYPE:  interface
TOKEN: 0x20001A5
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000950  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
TYPE:  abstract class
TOKEN: 0x20001A6
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Int32                    _index  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x6000951  System.Void .ctor(System.Int32 index)
  RVA=0x0AD085B8  token=0x6000952  System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadLocalInstruction
TYPE:  sealed class
TOKEN: 0x20001A7
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
IMPLEMENTS: System.Linq.Expressions.Interpreter.IBoxableInstruction
FIELDS:
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD08474
METHODS:
  RVA=0x014F51F0  token=0x6000953  System.Void .ctor(System.Int32 index)
  RVA=0x0AD083F8  token=0x6000956  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD083A4  token=0x6000957  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadLocalBoxedInstruction
TYPE:  sealed class
TOKEN: 0x20001A8
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
FIELDS:
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0821C
METHODS:
  RVA=0x014F51F0  token=0x6000958  System.Void .ctor(System.Int32 index)
  RVA=0x0AD08164  token=0x600095B  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadLocalFromClosureInstruction
TYPE:  sealed class
TOKEN: 0x20001A9
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
FIELDS:
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD08378
METHODS:
  RVA=0x014F51F0  token=0x600095C  System.Void .ctor(System.Int32 index)
  RVA=0x0AD082F0  token=0x600095F  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadLocalFromClosureBoxedInstruction
TYPE:  sealed class
TOKEN: 0x20001AA
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
FIELDS:
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD082C4
METHODS:
  RVA=0x014F51F0  token=0x6000960  System.Void .ctor(System.Int32 index)
  RVA=0x0AD08248  token=0x6000963  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.AssignLocalInstruction
TYPE:  sealed class
TOKEN: 0x20001AB
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
IMPLEMENTS: System.Linq.Expressions.Interpreter.IBoxableInstruction
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACFD59C
METHODS:
  RVA=0x014F51F0  token=0x6000964  System.Void .ctor(System.Int32 index)
  RVA=0x0ACFD534  token=0x6000968  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0ACFD4E0  token=0x6000969  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.StoreLocalInstruction
TYPE:  sealed class
TOKEN: 0x20001AC
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
IMPLEMENTS: System.Linq.Expressions.Interpreter.IBoxableInstruction
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  InstructionName  get=0x0AD120C4
METHODS:
  RVA=0x014F51F0  token=0x600096A  System.Void .ctor(System.Int32 index)
  RVA=0x0AD1205C  token=0x600096D  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD12008  token=0x600096E  System.Linq.Expressions.Interpreter.Instruction BoxIfIndexMatches(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.AssignLocalBoxedInstruction
TYPE:  sealed class
TOKEN: 0x20001AD
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACFD4B4
METHODS:
  RVA=0x014F51F0  token=0x600096F  System.Void .ctor(System.Int32 index)
  RVA=0x0ACFD414  token=0x6000973  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.StoreLocalBoxedInstruction
TYPE:  sealed class
TOKEN: 0x20001AE
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  InstructionName  get=0x0AD11FDC
METHODS:
  RVA=0x014F51F0  token=0x6000974  System.Void .ctor(System.Int32 index)
  RVA=0x0AD11F2C  token=0x6000977  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.AssignLocalToClosureInstruction
TYPE:  sealed class
TOKEN: 0x20001AF
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
FIELDS:
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACFD620
METHODS:
  RVA=0x014F51F0  token=0x6000978  System.Void .ctor(System.Int32 index)
  RVA=0x0ACFD5C8  token=0x600097C  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ValueTypeCopyInstruction
TYPE:  sealed class
TOKEN: 0x20001B0
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  public    static readonly System.Linq.Expressions.Interpreter.ValueTypeCopyInstructionInstruction  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD1559C
METHODS:
  RVA=0x0AD154E4  token=0x6000980  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x041E1670  token=0x6000981  System.Void .ctor()
  RVA=0x0AD15538  token=0x6000982  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.InitializeLocalInstruction
TYPE:  abstract class
TOKEN: 0x20001B1
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.LocalAccessInstruction
FIELDS:
METHODS:
  RVA=0x014F51F0  token=0x6000983  System.Void .ctor(System.Int32 index)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.RuntimeVariablesInstruction
TYPE:  sealed class
TOKEN: 0x20001BA
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Int32                    _count  // 0x10
PROPERTIES:
  ProducedStack  get=0x04D85B80
  ConsumedStack  get=0x02B2E2D0
  InstructionName  get=0x0AD11A90
METHODS:
  RVA=0x014F51F0  token=0x60009A0  System.Void .ctor(System.Int32 count)
  RVA=0x0AD1199C  token=0x60009A4  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LocalVariable
TYPE:  sealed class
TOKEN: 0x20001BB
SIZE:  0x18
FIELDS:
  public    readonly System.Int32                    Index  // 0x10
  private           System.Int32                    _flags  // 0x14
PROPERTIES:
  IsBoxed  get=0x0AD088A0  set=0x0AD088A8
  InClosure  get=0x0AD08898
METHODS:
  RVA=0x0AD08888  token=0x60009A8  System.Void .ctor(System.Int32 index, System.Boolean closure)
  RVA=0x0AD087B4  token=0x60009A9  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LocalDefinition
TYPE:  sealed struct
TOKEN: 0x20001BC
SIZE:  0x20
FIELDS:
  private   readonly System.Int32                    <Index>k__BackingField  // 0x10
  private   readonly System.Linq.Expressions.ParameterExpression<Parameter>k__BackingField  // 0x18
PROPERTIES:
  Index  get=0x02B77620
  Parameter  get=0x04D88320
METHODS:
  RVA=0x06B17430  token=0x60009AA  System.Void .ctor(System.Int32 localIndex, System.Linq.Expressions.ParameterExpression parameter)
  RVA=0x0AD08718  token=0x60009AD  System.Boolean Equals(System.Object obj)
  RVA=0x0AD08788  token=0x60009AE  System.Int32 GetHashCode()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LocalVariables
TYPE:  sealed class
TOKEN: 0x20001BD
SIZE:  0x28
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.HybridReferenceDictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariables.VariableScope>_variables  // 0x10
  private           System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable>_closureVariables  // 0x18
  private           System.Int32                    _localCount  // 0x20
  private           System.Int32                    _maxLocalCount  // 0x24
PROPERTIES:
  LocalCount  get=0x011F0020
  ClosureVariables  get=0x0385B100
METHODS:
  RVA=0x0AD08B24  token=0x60009AF  System.Linq.Expressions.Interpreter.LocalDefinition DefineLocal(System.Linq.Expressions.ParameterExpression variable, System.Int32 start)
  RVA=0x0AD08E14  token=0x60009B0  System.Void UndefineLocal(System.Linq.Expressions.Interpreter.LocalDefinition definition, System.Int32 end)
  RVA=0x0AD089D8  token=0x60009B1  System.Void Box(System.Linq.Expressions.ParameterExpression variable, System.Linq.Expressions.Interpreter.InstructionList instructions)
  RVA=0x0AD08D48  token=0x60009B3  System.Boolean TryGetLocalOrClosure(System.Linq.Expressions.ParameterExpression var, System.Linq.Expressions.Interpreter.LocalVariable& local)
  RVA=0x0AD088C4  token=0x60009B5  System.Linq.Expressions.Interpreter.LocalVariable AddClosureVariable(System.Linq.Expressions.ParameterExpression variable)
  RVA=0x0AD08ED8  token=0x60009B6  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ModuloInstruction
TYPE:  abstract class
TOKEN: 0x20001BF
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD09608
METHODS:
  RVA=0x041E1670  token=0x60009BB  System.Void .ctor()
  RVA=0x0AD091F8  token=0x60009BC  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.MulInstruction
TYPE:  abstract class
TOKEN: 0x20001C8
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0A304
METHODS:
  RVA=0x041E1670  token=0x60009D0  System.Void .ctor()
  RVA=0x0AD09EF4  token=0x60009D1  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.MulOvfInstruction
TYPE:  abstract class
TOKEN: 0x20001D1
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0A978
METHODS:
  RVA=0x041E1670  token=0x60009E5  System.Void .ctor()
  RVA=0x0AD0A65C  token=0x60009E6  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NegateInstruction
TYPE:  abstract class
TOKEN: 0x20001D8
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x20
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0C2F0
METHODS:
  RVA=0x041E1670  token=0x60009F6  System.Void .ctor()
  RVA=0x0AD0C01C  token=0x60009F7  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NegateCheckedInstruction
TYPE:  abstract class
TOKEN: 0x20001DE
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0BC74
METHODS:
  RVA=0x041E1670  token=0x6000A05  System.Void .ctor()
  RVA=0x0AD0BA98  token=0x6000A06  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NewInstruction
TYPE:  class
TOKEN: 0x20001E2
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  protected readonly System.Reflection.ConstructorInfo_constructor  // 0x10
  protected readonly System.Int32                    _argumentCount  // 0x18
PROPERTIES:
  ConsumedStack  get=0x011EF5B0
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0C85C
METHODS:
  RVA=0x03B46FF0  token=0x6000A0D  System.Void .ctor(System.Reflection.ConstructorInfo constructor, System.Int32 argumentCount)
  RVA=0x0AD0C624  token=0x6000A11  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD0C538  token=0x6000A12  System.Object[] GetArgs(System.Linq.Expressions.Interpreter.InterpretedFrame frame, System.Int32 first)
  RVA=0x0AD0C730  token=0x6000A13  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ByRefNewInstruction
TYPE:  class
TOKEN: 0x20001E3
SIZE:  0x28
EXTENDS: System.Linq.Expressions.Interpreter.NewInstruction
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.ByRefUpdater[]_byrefArgs  // 0x20
PROPERTIES:
  InstructionName  get=0x0ACFDEC0
METHODS:
  RVA=0x032B2C30  token=0x6000A14  System.Void .ctor(System.Reflection.ConstructorInfo target, System.Int32 argumentCount, System.Linq.Expressions.Interpreter.ByRefUpdater[] byrefArgs)
  RVA=0x0ACFDC6C  token=0x6000A16  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NotEqualInstruction
TYPE:  abstract class
TOKEN: 0x20001E4
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_reference  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Boolean  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Char  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByteLiftedToNull  // static @ 0x68
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16LiftedToNull  // static @ 0x70
  private   static  System.Linq.Expressions.Interpreter.Instructions_CharLiftedToNull  // static @ 0x78
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32LiftedToNull  // static @ 0x80
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64LiftedToNull  // static @ 0x88
  private   static  System.Linq.Expressions.Interpreter.Instructions_ByteLiftedToNull  // static @ 0x90
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16LiftedToNull  // static @ 0x98
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32LiftedToNull  // static @ 0xa0
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64LiftedToNull  // static @ 0xa8
  private   static  System.Linq.Expressions.Interpreter.Instructions_SingleLiftedToNull  // static @ 0xb0
  private   static  System.Linq.Expressions.Interpreter.Instructions_DoubleLiftedToNull  // static @ 0xb8
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0D9E8
METHODS:
  RVA=0x041E1670  token=0x6000A1A  System.Void .ctor()
  RVA=0x0AD0CE78  token=0x6000A1B  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type, System.Boolean liftedToNull)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NotInstruction
TYPE:  abstract class
TOKEN: 0x20001FD
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  public    static  System.Linq.Expressions.Interpreter.Instructions_Boolean  // static @ 0x0
  public    static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x8
  public    static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x10
  public    static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x18
  public    static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x20
  public    static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x28
  public    static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x30
  public    static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x38
  public    static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x40
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0EA30
METHODS:
  RVA=0x041E1670  token=0x6000A4C  System.Void .ctor()
  RVA=0x0AD0E5A0  token=0x6000A50  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NullCheckInstruction
TYPE:  sealed class
TOKEN: 0x2000207
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  public    static readonly System.Linq.Expressions.Interpreter.InstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0EEC4
METHODS:
  RVA=0x041E1670  token=0x6000A63  System.Void .ctor()
  RVA=0x0AD0EDF4  token=0x6000A67  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD0EE60  token=0x6000A68  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NumericConvertInstruction
TYPE:  abstract class
TOKEN: 0x2000208
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.TypeCode                 _from  // 0x10
  private   readonly System.TypeCode                 _to  // 0x14
  private   readonly System.Boolean                  _isLiftedToNull  // 0x18
PROPERTIES:
  InstructionName  get=0x0AD0F5A8
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
METHODS:
  RVA=0x04DA8680  token=0x6000A69  System.Void .ctor(System.TypeCode from, System.TypeCode to, System.Boolean isLiftedToNull)
  RVA=0x0AD0F388  token=0x6000A6A  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=-1  // abstract  token=0x6000A6B  System.Object Convert(System.Object obj)
  RVA=0x0AD0F45C  token=0x6000A6F  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.OrInstruction
TYPE:  abstract class
TOKEN: 0x200020C
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.Instructions_Boolean  // static @ 0x40
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0FC0C
METHODS:
  RVA=0x041E1670  token=0x6000A84  System.Void .ctor()
  RVA=0x0AD0F77C  token=0x6000A85  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.RightShiftInstruction
TYPE:  abstract class
TOKEN: 0x2000216
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_SByte  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_Byte  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD112C0
METHODS:
  RVA=0x041E1670  token=0x6000A9B  System.Void .ctor()
  RVA=0x0AD10EB0  token=0x6000A9C  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.RuntimeVariables
TYPE:  sealed class
TOKEN: 0x200021F
SIZE:  0x18
IMPLEMENTS: System.Runtime.CompilerServices.IRuntimeVariables
FIELDS:
  private   readonly System.Runtime.CompilerServices.IStrongBox[]_boxes  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000AAD  System.Void .ctor(System.Runtime.CompilerServices.IStrongBox[] boxes)
  RVA=0x0AD11ABC  token=0x6000AAE  System.Runtime.CompilerServices.IRuntimeVariables Create(System.Runtime.CompilerServices.IStrongBox[] boxes)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadObjectInstruction
TYPE:  sealed class
TOKEN: 0x2000220
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Object                   _value  // 0x10
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0858C
METHODS:
  RVA=0x053908C0  token=0x6000AAF  System.Void .ctor(System.Object value)
  RVA=0x0AD084A0  token=0x6000AB2  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD08500  token=0x6000AB3  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.LoadCachedObjectInstruction
TYPE:  sealed class
TOKEN: 0x2000221
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.UInt32                   _index  // 0x10
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD08138
METHODS:
  RVA=0x014F51F0  token=0x6000AB4  System.Void .ctor(System.UInt32 index)
  RVA=0x0AD07F8C  token=0x6000AB7  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD0800C  token=0x6000AB8  System.String ToDebugString(System.Int32 instructionIndex, System.Object cookie, System.Func<System.Int32,System.Int32> labelIndexer, System.Collections.Generic.IReadOnlyList<System.Object> objects)
  RVA=0x0AD080DC  token=0x6000AB9  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.PopInstruction
TYPE:  sealed class
TOKEN: 0x2000222
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.PopInstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  InstructionName  get=0x0AD10460
METHODS:
  RVA=0x041E1670  token=0x6000ABA  System.Void .ctor()
  RVA=0x0AD103D4  token=0x6000ABD  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD103FC  token=0x6000ABE  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.DupInstruction
TYPE:  sealed class
TOKEN: 0x2000223
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static readonly System.Linq.Expressions.Interpreter.DupInstructionInstance  // static @ 0x0
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD01764
METHODS:
  RVA=0x041E1670  token=0x6000ABF  System.Void .ctor()
  RVA=0x0AD016D8  token=0x6000AC2  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD01700  token=0x6000AC3  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.SubInstruction
TYPE:  abstract class
TOKEN: 0x2000224
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.Instructions_Single  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.Instructions_Double  // static @ 0x38
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD12624
METHODS:
  RVA=0x041E1670  token=0x6000AC7  System.Void .ctor()
  RVA=0x0AD12214  token=0x6000AC8  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.SubOvfInstruction
TYPE:  abstract class
TOKEN: 0x200022D
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int16  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int32  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.Instructions_Int64  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt16  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt32  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.Instructions_UInt64  // static @ 0x28
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD12C98
METHODS:
  RVA=0x041E1670  token=0x6000ADC  System.Void .ctor()
  RVA=0x0AD1297C  token=0x6000ADD  System.Linq.Expressions.Interpreter.Instruction Create(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.CreateDelegateInstruction
TYPE:  sealed class
TOKEN: 0x2000234
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Linq.Expressions.Interpreter.LightDelegateCreator_creator  // 0x10
PROPERTIES:
  ConsumedStack  get=0x0AD000DC
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD00104
METHODS:
  RVA=0x053908C0  token=0x6000AEA  System.Void .ctor(System.Linq.Expressions.Interpreter.LightDelegateCreator delegateCreator)
  RVA=0x0ACFFFB0  token=0x6000AEE  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.TypeIsInstruction
TYPE:  sealed class
TOKEN: 0x2000235
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Type                     _type  // 0x10
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD14400
METHODS:
  RVA=0x053908C0  token=0x6000AEF  System.Void .ctor(System.Type type)
  RVA=0x0AD14350  token=0x6000AF3  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD143A8  token=0x6000AF4  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.TypeAsInstruction
TYPE:  sealed class
TOKEN: 0x2000236
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Type                     _type  // 0x10
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD14224
METHODS:
  RVA=0x053908C0  token=0x6000AF5  System.Void .ctor(System.Type type)
  RVA=0x0AD14160  token=0x6000AF9  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD141CC  token=0x6000AFA  System.String ToString()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.TypeEqualsInstruction
TYPE:  sealed class
TOKEN: 0x2000237
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  public    static readonly System.Linq.Expressions.Interpreter.TypeEqualsInstructionInstance  // static @ 0x0
PROPERTIES:
  ConsumedStack  get=0x02BBE540
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD14324
METHODS:
  RVA=0x041E1670  token=0x6000AFE  System.Void .ctor()
  RVA=0x0AD14250  token=0x6000AFF  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
  RVA=0x0AD142C0  token=0x6000B00  System.Void .cctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.NullableMethodCallInstruction
TYPE:  abstract class
TOKEN: 0x2000238
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.NullableMethodCallInstructions_hasValue  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.NullableMethodCallInstructions_value  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.NullableMethodCallInstructions_equals  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.NullableMethodCallInstructions_getHashCode  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.NullableMethodCallInstructions_getValueOrDefault1  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.NullableMethodCallInstructions_toString  // static @ 0x28
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD0F35C
METHODS:
  RVA=0x041E1670  token=0x6000B04  System.Void .ctor()
  RVA=0x0AD0EF80  token=0x6000B05  System.Linq.Expressions.Interpreter.Instruction Create(System.String method, System.Int32 argCount, System.Reflection.MethodInfo mi)
  RVA=0x0AD0EEF0  token=0x6000B06  System.Linq.Expressions.Interpreter.Instruction CreateGetValue()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.CastInstruction
TYPE:  abstract class
TOKEN: 0x2000240
SIZE:  0x10
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Boolean  // static @ 0x0
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Byte  // static @ 0x8
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Char  // static @ 0x10
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_DateTime  // static @ 0x18
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Decimal  // static @ 0x20
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Double  // static @ 0x28
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Int16  // static @ 0x30
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Int32  // static @ 0x38
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Int64  // static @ 0x40
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_SByte  // static @ 0x48
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_Single  // static @ 0x50
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_String  // static @ 0x58
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_UInt16  // static @ 0x60
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_UInt32  // static @ 0x68
  private   static  System.Linq.Expressions.Interpreter.CastInstructions_UInt64  // static @ 0x70
PROPERTIES:
  ConsumedStack  get=0x04D85B80
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0ACFE9F4
METHODS:
  RVA=0x0ACFE24C  token=0x6000B1A  System.Linq.Expressions.Interpreter.Instruction Create(System.Type t)
  RVA=0x041E1670  token=0x6000B1B  System.Void .ctor()
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.CastToEnumInstruction
TYPE:  sealed class
TOKEN: 0x2000245
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.CastInstruction
FIELDS:
  private   readonly System.Type                     _t  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000B26  System.Void .ctor(System.Type t)
  RVA=0x0ACFED48  token=0x6000B27  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.CastReferenceToEnumInstruction
TYPE:  sealed class
TOKEN: 0x2000246
SIZE:  0x18
EXTENDS: System.Linq.Expressions.Interpreter.CastInstruction
FIELDS:
  private   readonly System.Type                     _t  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000B28  System.Void .ctor(System.Type t)
  RVA=0x0ACFEA20  token=0x6000B29  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.QuoteInstruction
TYPE:  sealed class
TOKEN: 0x2000247
SIZE:  0x20
EXTENDS: System.Linq.Expressions.Interpreter.Instruction
FIELDS:
  private   readonly System.Linq.Expressions.Expression_operand  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable>_hoistedVariables  // 0x18
PROPERTIES:
  ProducedStack  get=0x04D85B80
  InstructionName  get=0x0AD106EC
METHODS:
  RVA=0x032AF420  token=0x6000B2A  System.Void .ctor(System.Linq.Expressions.Expression operand, System.Collections.Generic.Dictionary<System.Linq.Expressions.ParameterExpression,System.Linq.Expressions.Interpreter.LocalVariable> hoistedVariables)
  RVA=0x0AD1063C  token=0x6000B2D  System.Int32 Run(System.Linq.Expressions.Interpreter.InterpretedFrame frame)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.DelegateHelpers
TYPE:  static class
TOKEN: 0x2000249
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AD00EA0  token=0x6000B34  System.Type MakeDelegate(System.Type[] types)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ScriptingRuntimeHelpers
TYPE:  static class
TOKEN: 0x200024B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AD11E18  token=0x6000B38  System.Object Int32ToObject(System.Int32 i)
  RVA=0x0AD11B1C  token=0x6000B39  System.Object GetPrimitiveDefaultValue(System.Type type)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.ExceptionHelpers
TYPE:  static class
TOKEN: 0x200024C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AD02DD8  token=0x6000B3A  System.Void UnwrapAndRethrow(System.Reflection.TargetInvocationException exception)
END_CLASS

CLASS: System.Linq.Expressions.Interpreter.HybridReferenceDictionary`2
TYPE:  class
TOKEN: 0x200024D
FIELDS:
  private           System.Collections.Generic.KeyValuePair<TKey,TValue>[]_keysAndValues  // 0x0
  private           System.Collections.Generic.Dictionary<TKey,TValue>_dict  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B3B  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000B3C  System.Void Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000B3D  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000B3E  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000B3F  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumeratorWorker()
  RVA=-1  // not resolved  token=0x6000B42  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.RuntimeOps
TYPE:  static class
TOKEN: 0x200024F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AD11928  token=0x6000B49  System.Boolean ExpandoTryGetValue(System.Dynamic.ExpandoObject expando, System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase, System.Object& value)
  RVA=0x0AD1195C  token=0x6000B4A  System.Object ExpandoTrySetValue(System.Dynamic.ExpandoObject expando, System.Object indexClass, System.Int32 index, System.Object value, System.String name, System.Boolean ignoreCase)
  RVA=0x0AD11888  token=0x6000B4B  System.Boolean ExpandoTryDeleteValue(System.Dynamic.ExpandoObject expando, System.Object indexClass, System.Int32 index, System.String name, System.Boolean ignoreCase)
  RVA=0x0AD11844  token=0x6000B4C  System.Boolean ExpandoCheckVersion(System.Dynamic.ExpandoObject expando, System.Object version)
  RVA=0x0AD1186C  token=0x6000B4D  System.Void ExpandoPromoteClass(System.Dynamic.ExpandoObject expando, System.Object oldClass, System.Object newClass)
END_CLASS

CLASS: System.Runtime.CompilerServices.CallSite
TYPE:  class
TOKEN: 0x2000250
SIZE:  0x20
FIELDS:
  private   readonly System.Runtime.CompilerServices.CallSiteBinder_binder  // 0x10
  private           System.Boolean                  _match  // 0x18
PROPERTIES:
  Binder  get=0x02B2ECC0
METHODS:
  RVA=0x053908C0  token=0x6000B4E  System.Void .ctor(System.Runtime.CompilerServices.CallSiteBinder binder)
END_CLASS

CLASS: System.Runtime.CompilerServices.CallSite`1
TYPE:  class
TOKEN: 0x2000251
EXTENDS: System.Runtime.CompilerServices.CallSite
FIELDS:
  public            T                               Target  // 0x0
  private           T[]                             Rules  // 0x0
  private   static  T                               s_cachedUpdate  // static @ 0x0
  private   static  T                               s_cachedNoMatch  // static @ 0x0
PROPERTIES:
  Update  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B51  System.Void .ctor(System.Runtime.CompilerServices.CallSiteBinder binder)
  RVA=-1  // not resolved  token=0x6000B52  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000B53  System.Runtime.CompilerServices.CallSite<T> CreateMatchMaker()
  RVA=-1  // not resolved  token=0x6000B54  System.Runtime.CompilerServices.CallSite<T> Create(System.Runtime.CompilerServices.CallSiteBinder binder)
  RVA=-1  // not resolved  token=0x6000B55  T GetUpdateDelegate()
  RVA=-1  // not resolved  token=0x6000B56  T GetUpdateDelegate(T& addr)
  RVA=-1  // not resolved  token=0x6000B57  System.Void AddRule(T newRule)
  RVA=-1  // not resolved  token=0x6000B58  System.Void MoveRule(System.Int32 i)
  RVA=-1  // not resolved  token=0x6000B59  T MakeUpdateDelegate()
  RVA=-1  // not resolved  token=0x6000B5A  T CreateCustomUpdateDelegate(System.Reflection.MethodInfo invoke)
  RVA=-1  // not resolved  token=0x6000B5B  T CreateCustomNoMatchDelegate(System.Reflection.MethodInfo invoke)
  RVA=-1  // not resolved  token=0x6000B5C  System.Linq.Expressions.Expression Convert(System.Linq.Expressions.Expression arg, System.Type type)
END_CLASS

CLASS: System.Runtime.CompilerServices.CallSiteBinder
TYPE:  abstract class
TOKEN: 0x2000253
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.Dictionary<System.Type,System.Object>Cache  // 0x10
  private   static readonly System.Linq.Expressions.LabelTarget<UpdateLabel>k__BackingField  // static @ 0x0
PROPERTIES:
  UpdateLabel  get=0x0ACFDF68
METHODS:
  RVA=0x041E1670  token=0x6000B62  System.Void .ctor()
  RVA=-1  // abstract  token=0x6000B64  System.Linq.Expressions.Expression Bind(System.Object[] args, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> parameters, System.Linq.Expressions.LabelTarget returnLabel)
  RVA=-1  // generic def  token=0x6000B65  T BindDelegate(System.Runtime.CompilerServices.CallSite<T> site, System.Object[] args)
  RVA=-1  // generic def  token=0x6000B66  T BindCore(System.Runtime.CompilerServices.CallSite<T> site, System.Object[] args)
  RVA=-1  // generic def  token=0x6000B67  System.Void CacheTarget(T target)
  RVA=-1  // generic def  token=0x6000B68  System.Linq.Expressions.Expression<T> Stitch(System.Linq.Expressions.Expression binding, System.Runtime.CompilerServices.CallSiteBinder.LambdaSignature<T> signature)
  RVA=-1  // generic def  token=0x6000B69  System.Runtime.CompilerServices.RuleCache<T> GetRuleCache()
  RVA=0x0ACFDEEC  token=0x6000B6A  System.Void .cctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.CallSiteOps
TYPE:  static class
TOKEN: 0x2000255
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000B6D  System.Runtime.CompilerServices.CallSite<T> CreateMatchmaker(System.Runtime.CompilerServices.CallSite<T> site)
  RVA=0x0ACFDFD0  token=0x6000B6E  System.Boolean SetNotMatched(System.Runtime.CompilerServices.CallSite site)
  RVA=0x0952EB04  token=0x6000B6F  System.Boolean GetMatch(System.Runtime.CompilerServices.CallSite site)
  RVA=0x0ACFDFB8  token=0x6000B70  System.Void ClearMatch(System.Runtime.CompilerServices.CallSite site)
  RVA=-1  // generic def  token=0x6000B71  System.Void AddRule(System.Runtime.CompilerServices.CallSite<T> site, T rule)
  RVA=-1  // generic def  token=0x6000B72  System.Void UpdateRules(System.Runtime.CompilerServices.CallSite<T> this, System.Int32 matched)
  RVA=-1  // generic def  token=0x6000B73  T[] GetRules(System.Runtime.CompilerServices.CallSite<T> site)
  RVA=-1  // generic def  token=0x6000B74  System.Runtime.CompilerServices.RuleCache<T> GetRuleCache(System.Runtime.CompilerServices.CallSite<T> site)
  RVA=-1  // generic def  token=0x6000B75  System.Void MoveRule(System.Runtime.CompilerServices.RuleCache<T> cache, T rule, System.Int32 i)
  RVA=-1  // generic def  token=0x6000B76  T[] GetCachedRules(System.Runtime.CompilerServices.RuleCache<T> cache)
  RVA=-1  // generic def  token=0x6000B77  T Bind(System.Runtime.CompilerServices.CallSiteBinder binder, System.Runtime.CompilerServices.CallSite<T> site, System.Object[] args)
END_CLASS

CLASS: System.Runtime.CompilerServices.IRuntimeVariables
TYPE:  interface
TOKEN: 0x2000256
FIELDS:
METHODS:
END_CLASS

CLASS: System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1
TYPE:  sealed class
TOKEN: 0x2000257
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.IList System.Collections.ICollection
FIELDS:
  private           T[]                             _items  // 0x0
  private           System.Int32                    _size  // 0x0
  private           System.Int32                    _version  // 0x0
PROPERTIES:
  Capacity  set=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
  System.Collections.Generic.ICollection<T>.IsReadOnly  get=-1  // not resolved
  System.Collections.IList.IsReadOnly  get=-1  // not resolved
  System.Collections.IList.IsFixedSize  get=-1  // not resolved
  System.Collections.IList.Item  get=-1  // not resolved  set=-1  // not resolved
  System.Collections.ICollection.IsSynchronized  get=-1  // not resolved
  System.Collections.ICollection.SyncRoot  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B78  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000B79  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000B7C  System.Int32 IndexOf(T item)
  RVA=-1  // not resolved  token=0x6000B7D  System.Void Insert(System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x6000B7E  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000B81  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x6000B82  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000B83  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000B84  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000B86  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x6000B87  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000B88  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000B8A  System.Int32 System.Collections.IList.Add(System.Object value)
  RVA=-1  // not resolved  token=0x6000B8B  System.Boolean System.Collections.IList.Contains(System.Object value)
  RVA=-1  // not resolved  token=0x6000B8C  System.Int32 System.Collections.IList.IndexOf(System.Object value)
  RVA=-1  // not resolved  token=0x6000B8D  System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value)
  RVA=-1  // not resolved  token=0x6000B8F  System.Void System.Collections.IList.Remove(System.Object value)
  RVA=-1  // not resolved  token=0x6000B92  System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index)
  RVA=-1  // not resolved  token=0x6000B95  T[] ToArray()
  RVA=-1  // not resolved  token=0x6000B96  System.Collections.ObjectModel.ReadOnlyCollection<T> ToReadOnlyCollection()
  RVA=-1  // not resolved  token=0x6000B97  System.Void EnsureCapacity(System.Int32 min)
  RVA=-1  // not resolved  token=0x6000B98  System.Boolean IsCompatibleObject(System.Object value)
  RVA=-1  // not resolved  token=0x6000B99  System.Void ValidateNullValue(System.Object value, System.String argument)
END_CLASS

CLASS: System.Runtime.CompilerServices.RuleCache`1
TYPE:  class
TOKEN: 0x2000259
FIELDS:
  private           T[]                             _rules  // 0x0
  private   readonly System.Object                   _cacheLock  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BA0  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BA1  T[] GetRules()
  RVA=-1  // not resolved  token=0x6000BA2  System.Void MoveRule(T rule, System.Int32 i)
  RVA=-1  // not resolved  token=0x6000BA3  System.Void AddRule(T newRule)
  RVA=-1  // not resolved  token=0x6000BA4  T[] AddOrInsert(T[] rules, T item)
END_CLASS

CLASS: System.Runtime.CompilerServices.TrueReadOnlyCollection`1
TYPE:  sealed class
TOKEN: 0x200025A
EXTENDS: System.Collections.ObjectModel.ReadOnlyCollection`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000BA5  System.Void .ctor(T[] list)
END_CLASS

CLASS: System.Runtime.CompilerServices.StrongBox`1
TYPE:  class
TOKEN: 0x200025B
IMPLEMENTS: System.Runtime.CompilerServices.IStrongBox
FIELDS:
  public            T                               Value  // 0x0
PROPERTIES:
  System.Runtime.CompilerServices.IStrongBox.Value  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BA6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BA7  System.Void .ctor(T value)
END_CLASS

CLASS: System.Runtime.CompilerServices.IStrongBox
TYPE:  interface
TOKEN: 0x200025C
FIELDS:
PROPERTIES:
  Value  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: System.Security.Cryptography.AesManaged
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x50
EXTENDS: System.Security.Cryptography.Aes
FIELDS:
  private           System.Security.Cryptography.RijndaelManagedm_rijndael  // 0x48
PROPERTIES:
  FeedbackSize  get=0x0ACAFC30
  IV  get=0x0ACAFC54  set=0x0ACAFD08
  Key  get=0x0ACAFC9C  set=0x0ACAFD50
  KeySize  get=0x0ACAFC78  set=0x0ACAFD2C
  Mode  get=0x0ACAFCC0  set=0x0ACAFD74
  Padding  get=0x0ACAFCE4  set=0x0ACAFE04
METHODS:
  RVA=0x0ACAFB58  token=0x600002C  System.Void .ctor()
  RVA=0x0ACAF870  token=0x6000038  System.Security.Cryptography.ICryptoTransform CreateDecryptor()
  RVA=0x0ACAF6E4  token=0x6000039  System.Security.Cryptography.ICryptoTransform CreateDecryptor(System.Byte[] key, System.Byte[] iv)
  RVA=0x0ACAFA20  token=0x600003A  System.Security.Cryptography.ICryptoTransform CreateEncryptor()
  RVA=0x0ACAF894  token=0x600003B  System.Security.Cryptography.ICryptoTransform CreateEncryptor(System.Byte[] key, System.Byte[] iv)
  RVA=0x0ACAFA44  token=0x600003C  System.Void Dispose(System.Boolean disposing)
  RVA=0x0ACAFB10  token=0x600003D  System.Void GenerateIV()
  RVA=0x0ACAFB34  token=0x600003E  System.Void GenerateKey()
END_CLASS

CLASS: System.Security.Cryptography.AesCryptoServiceProvider
TYPE:  sealed class
TOKEN: 0x2000017
SIZE:  0x48
EXTENDS: System.Security.Cryptography.Aes
FIELDS:
PROPERTIES:
  IV  get=0x04A389B0  set=0x0489A460
  Key  get=0x04A38940  set=0x041CF1C0
  KeySize  get=0x04D863B0  set=0x0ACAF6DC
  FeedbackSize  get=0x04D882A0
  Mode  get=0x04D86640  set=0x04B4EFB0
  Padding  get=0x04D865E0  set=0x04CC6110
METHODS:
  RVA=0x04891980  token=0x600003F  System.Void .ctor()
  RVA=0x0ACAF68C  token=0x6000040  System.Void GenerateIV()
  RVA=0x0ACAF6B4  token=0x6000041  System.Void GenerateKey()
  RVA=0x03FA4E20  token=0x6000042  System.Security.Cryptography.ICryptoTransform CreateDecryptor(System.Byte[] key, System.Byte[] iv)
  RVA=0x0ACAF594  token=0x6000043  System.Security.Cryptography.ICryptoTransform CreateEncryptor(System.Byte[] key, System.Byte[] iv)
  RVA=0x03121110  token=0x600004F  System.Security.Cryptography.ICryptoTransform CreateDecryptor()
  RVA=0x0A773E08  token=0x6000050  System.Security.Cryptography.ICryptoTransform CreateEncryptor()
  RVA=0x048B52F0  token=0x6000051  System.Void Dispose(System.Boolean disposing)
END_CLASS

CLASS: System.Security.Cryptography.AesTransform
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x68
EXTENDS: Mono.Security.Cryptography.SymmetricTransform
FIELDS:
  private           System.UInt32[]                 expandedKey  // 0x58
  private           System.Int32                    Nk  // 0x60
  private           System.Int32                    Nr  // 0x64
  private   static readonly System.UInt32[]                 Rcon  // static @ 0x0
  private   static readonly System.Byte[]                   SBox  // static @ 0x8
  private   static readonly System.Byte[]                   iSBox  // static @ 0x10
  private   static readonly System.UInt32[]                 T0  // static @ 0x18
  private   static readonly System.UInt32[]                 T1  // static @ 0x20
  private   static readonly System.UInt32[]                 T2  // static @ 0x28
  private   static readonly System.UInt32[]                 T3  // static @ 0x30
  private   static readonly System.UInt32[]                 iT0  // static @ 0x38
  private   static readonly System.UInt32[]                 iT1  // static @ 0x40
  private   static readonly System.UInt32[]                 iT2  // static @ 0x48
  private   static readonly System.UInt32[]                 iT3  // static @ 0x50
METHODS:
  RVA=0x03FA4ED0  token=0x6000052  System.Void .ctor(System.Security.Cryptography.Aes algo, System.Boolean encryption, System.Byte[] key, System.Byte[] iv)
  RVA=0x03774920  token=0x6000053  System.Void ECB(System.Byte[] input, System.Byte[] output)
  RVA=0x03FA53F0  token=0x6000054  System.UInt32 SubByte(System.UInt32 a)
  RVA=0x0ACAFE28  token=0x6000055  System.Void Encrypt128(System.Byte[] indata, System.Byte[] outdata, System.UInt32[] ekey)
  RVA=0x03774950  token=0x6000056  System.Void Decrypt128(System.Byte[] indata, System.Byte[] outdata, System.UInt32[] ekey)
  RVA=0x037DA710  token=0x6000057  System.Void .cctor()
END_CLASS

CLASS: System.Security.Cryptography.SHA256CryptoServiceProvider
TYPE:  sealed class
TOKEN: 0x2000019
SIZE:  0x30
EXTENDS: System.Security.Cryptography.SHA256
FIELDS:
  private   static  System.Byte[]                   Empty  // static @ 0x0
  private           System.Security.Cryptography.SHA256hash  // 0x28
METHODS:
  RVA=0x0ACCAB98  token=0x6000058  System.Void .ctor()
  RVA=0x0ACCAB10  token=0x6000059  System.Void Initialize()
  RVA=0x0ACCAA48  token=0x600005A  System.Void HashCore(System.Byte[] array, System.Int32 ibStart, System.Int32 cbSize)
  RVA=0x0ACCAA78  token=0x600005B  System.Byte[] HashFinal()
  RVA=0x0ACCA9E8  token=0x600005C  System.Void Dispose(System.Boolean disposing)
  RVA=0x0ACCAB34  token=0x600005D  System.Void .cctor()
END_CLASS

CLASS: System.Security.Cryptography.X509Certificates.RSACertificateExtensions
TYPE:  static class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACCA93C  token=0x600005E  System.Security.Cryptography.RSA GetRSAPublicKey(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
END_CLASS

CLASS: System.Threading.LockRecursionPolicy
TYPE:  sealed struct
TOKEN: 0x200029A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Threading.LockRecursionPolicyNoRecursion  // const
  public    static  System.Threading.LockRecursionPolicySupportsRecursion  // const
METHODS:
END_CLASS

CLASS: System.Threading.ReaderWriterCount
TYPE:  class
TOKEN: 0x200029B
SIZE:  0x30
FIELDS:
  public            System.Int64                    lockID  // 0x10
  public            System.Int32                    readercount  // 0x18
  public            System.Int32                    writercount  // 0x1c
  public            System.Int32                    upgradecount  // 0x20
  public            System.Threading.ReaderWriterCountnext  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000D4C  System.Void .ctor()
END_CLASS

CLASS: System.Threading.ReaderWriterLockSlim
TYPE:  class
TOKEN: 0x200029C
SIZE:  0x70
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Boolean                  fIsReentrant  // 0x10
  private           System.Int32                    myLock  // 0x14
  private           System.UInt32                   numWriteWaiters  // 0x18
  private           System.UInt32                   numReadWaiters  // 0x1c
  private           System.UInt32                   numWriteUpgradeWaiters  // 0x20
  private           System.UInt32                   numUpgradeWaiters  // 0x24
  private           System.Boolean                  fNoWaiters  // 0x28
  private           System.Int32                    upgradeLockOwnerId  // 0x2c
  private           System.Int32                    writeLockOwnerId  // 0x30
  private           System.Threading.EventWaitHandlewriteEvent  // 0x38
  private           System.Threading.EventWaitHandlereadEvent  // 0x40
  private           System.Threading.EventWaitHandleupgradeEvent  // 0x48
  private           System.Threading.EventWaitHandlewaitUpgradeEvent  // 0x50
  private   static  System.Int64                    s_nextLockID  // static @ 0x0
  private           System.Int64                    lockID  // 0x58
  private   static  System.Threading.ReaderWriterCountt_rwc  // static @ 0xffffffff
  private           System.Boolean                  fUpgradeThreadHoldingRead  // 0x60
  private           System.UInt32                   owners  // 0x64
  private           System.Boolean                  fDisposed  // 0x68
PROPERTIES:
  IsReadLockHeld  get=0x0AD18D34
  IsUpgradeableReadLockHeld  get=0x0AD18D4C
  IsWriteLockHeld  get=0x0AD18D64
  RecursiveReadCount  get=0x0AD18D7C
  RecursiveUpgradeCount  get=0x0AD18D94
  RecursiveWriteCount  get=0x0AD18DE8
  WaitingReadCount  get=0x04D86310
  WaitingUpgradeCount  get=0x011F0020
  WaitingWriteCount  get=0x011EF5B0
METHODS:
  RVA=0x04DAA070  token=0x6000D4D  System.Void InitializeThreadCounts()
  RVA=0x053973F4  token=0x6000D4E  System.Void .ctor()
  RVA=0x04D34B20  token=0x6000D4F  System.Void .ctor(System.Threading.LockRecursionPolicy recursionPolicy)
  RVA=0x0AD156C4  token=0x6000D50  System.Boolean IsRWEntryEmpty(System.Threading.ReaderWriterCount rwc)
  RVA=0x0AD18A78  token=0x6000D51  System.Boolean IsRwHashEntryChanged(System.Threading.ReaderWriterCount lrwc)
  RVA=0x0AD1894C  token=0x6000D52  System.Threading.ReaderWriterCount GetThreadRWCount(System.Boolean dontAllocate)
  RVA=0x05397E1C  token=0x6000D53  System.Void EnterWriteLock()
  RVA=0x03E6C150  token=0x6000D54  System.Boolean TryEnterWriteLock(System.Int32 millisecondsTimeout)
  RVA=0x0539640C  token=0x6000D55  System.Boolean TryEnterWriteLock(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout)
  RVA=0x04CA3420  token=0x6000D56  System.Boolean TryEnterWriteLockCore(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout)
  RVA=0x0539305C  token=0x6000D57  System.Void EnterUpgradeableReadLock()
  RVA=0x03E6C190  token=0x6000D58  System.Boolean TryEnterUpgradeableReadLock(System.Int32 millisecondsTimeout)
  RVA=0x05396414  token=0x6000D59  System.Boolean TryEnterUpgradeableReadLock(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout)
  RVA=0x03353B10  token=0x6000D5A  System.Boolean TryEnterUpgradeableReadLockCore(System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout)
  RVA=0x04D30460  token=0x6000D5B  System.Void ExitWriteLock()
  RVA=0x03353610  token=0x6000D5C  System.Void ExitUpgradeableReadLock()
  RVA=0x0AD18A98  token=0x6000D5D  System.Void LazyCreateEvent(System.Threading.EventWaitHandle& waitEvent, System.Boolean makeAutoResetEvent)
  RVA=0x0AD18BC8  token=0x6000D5E  System.Boolean WaitOnEvent(System.Threading.EventWaitHandle waitEvent, System.UInt32& numWaiters, System.Threading.ReaderWriterLockSlim.TimeoutTracker timeout, System.Boolean isWriteWaiter)
  RVA=0x033535F0  token=0x6000D5F  System.Void ExitAndWakeUpAppropriateWaiters()
  RVA=0x0AD188E0  token=0x6000D60  System.Void ExitAndWakeUpAppropriateWaitersPreferringWriters()
  RVA=0x0AD18860  token=0x6000D61  System.Void ExitAndWakeUpAppropriateReadWaiters()
  RVA=0x04CA3530  token=0x6000D62  System.Boolean IsWriterAcquired()
  RVA=0x04DAA090  token=0x6000D63  System.Void SetWriterAcquired()
  RVA=0x04DAA030  token=0x6000D64  System.Void ClearWriterAcquired()
  RVA=0x04DAA0A0  token=0x6000D65  System.Void SetWritersWaiting()
  RVA=0x04DAA040  token=0x6000D66  System.Void ClearWritersWaiting()
  RVA=0x04DAA080  token=0x6000D67  System.Void SetUpgraderWaiting()
  RVA=0x04DAA020  token=0x6000D68  System.Void ClearUpgraderWaiting()
  RVA=0x04DAA060  token=0x6000D69  System.UInt32 GetNumReaders()
  RVA=0x03354760  token=0x6000D6A  System.Void EnterMyLock()
  RVA=0x0AD187CC  token=0x6000D6B  System.Void EnterMyLockSpin()
  RVA=0x04DAA050  token=0x6000D6C  System.Void ExitMyLock()
  RVA=0x0AD18B6C  token=0x6000D6D  System.Void SpinWait(System.Int32 SpinCount)
  RVA=0x0AD187C0  token=0x6000D6E  System.Void Dispose()
  RVA=0x0AD18618  token=0x6000D6F  System.Void Dispose(System.Boolean disposing)
END_CLASS

