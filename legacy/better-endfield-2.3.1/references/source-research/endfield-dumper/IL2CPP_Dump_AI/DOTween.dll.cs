// ========================================================
// Dumped by @desirepro
// Assembly: DOTween.dll
// Classes:  201
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

CLASS: CubicBezier
TYPE:  static class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0695AA70  token=0x6000011  UnityEngine.Vector3 GetPointOnSegment(UnityEngine.Vector3 startPoint, UnityEngine.Vector3 startControlPoint, UnityEngine.Vector3 endPoint, UnityEngine.Vector3 endControlPoint, System.Single factor)
  RVA=0x0695AC00  token=0x6000012  UnityEngine.Vector3[] GetSegmentPointCloud(UnityEngine.Vector3 startPoint, UnityEngine.Vector3 startControlPoint, UnityEngine.Vector3 endPoint, UnityEngine.Vector3 endControlPoint, System.Int32 resolution)
  RVA=0x0695AD64  token=0x6000013  System.Void GetSegmentPointCloud(System.Collections.Generic.List<UnityEngine.Vector3> addToList, UnityEngine.Vector3 startPoint, UnityEngine.Vector3 startControlPoint, UnityEngine.Vector3 endPoint, UnityEngine.Vector3 endControlPoint, System.Int32 resolution)
END_CLASS

CLASS: <>c__DisplayClass67_0
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x20
FIELDS:
  public            System.Single                   v  // 0x10
  public            DG.Tweening.Core.DOSetter<System.Single>setter  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600006D  System.Void .ctor()
  RVA=0x04D85F70  token=0x600006E  System.Single <To>b__0()
  RVA=0x0696B4E8  token=0x600006F  System.Void <To>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x20
FIELDS:
  public            System.Single                   val  // 0x10
  public            DG.Tweening.TweenCallback<System.Single>onVirtualUpdate  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600007D  System.Void .ctor()
  RVA=0x04D85F70  token=0x600007E  System.Single <Float>b__0()
  RVA=0x04D88280  token=0x600007F  System.Void <Float>b__1(System.Single x)
  RVA=0x0696A6A4  token=0x6000080  System.Void <Float>b__2()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x20
FIELDS:
  public            System.Int32                    val  // 0x10
  public            DG.Tweening.TweenCallback<System.Int32>onVirtualUpdate  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000081  System.Void .ctor()
  RVA=0x02B2E2D0  token=0x6000082  System.Int32 <Int>b__0()
  RVA=0x014F51F0  token=0x6000083  System.Void <Int>b__1(System.Int32 x)
  RVA=0x0696AA60  token=0x6000084  System.Void <Int>b__2()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             val  // 0x10
  public            DG.Tweening.TweenCallback<UnityEngine.Vector3>onVirtualUpdate  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000085  System.Void .ctor()
  RVA=0x04D8C950  token=0x6000086  UnityEngine.Vector3 <Vector3>b__0()
  RVA=0x04D8C970  token=0x6000087  System.Void <Vector3>b__1(UnityEngine.Vector3 x)
  RVA=0x0696ADF0  token=0x6000088  System.Void <Vector3>b__2()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x28
FIELDS:
  public            UnityEngine.Color               val  // 0x10
  public            DG.Tweening.TweenCallback<UnityEngine.Color>onVirtualUpdate  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000089  System.Void .ctor()
  RVA=0x02B2D510  token=0x600008A  UnityEngine.Color <Color>b__0()
  RVA=0x04D8C980  token=0x600008B  System.Void <Color>b__1(UnityEngine.Color x)
  RVA=0x0696AFE8  token=0x600008C  System.Void <Color>b__2()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x20
FIELDS:
  public            System.Single                   motionDelay  // 0x10
  public            DG.Tweening.EaseFunction        customEase  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000091  System.Void .ctor()
  RVA=0x0696AD84  token=0x6000092  System.Single <StopMotion>b__0(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000133  System.Void .ctor()
  RVA=0x0696A664  token=0x6000134  System.Single <DOAspect>b__0()
  RVA=0x0696A684  token=0x6000135  System.Void <DOAspect>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000136  System.Void .ctor()
  RVA=0x0696A9FC  token=0x6000137  UnityEngine.Color <DOColor>b__0()
  RVA=0x0696AA30  token=0x6000138  System.Void <DOColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000139  System.Void .ctor()
  RVA=0x0696AD44  token=0x600013A  System.Single <DOFarClipPlane>b__0()
  RVA=0x0696AD64  token=0x600013B  System.Void <DOFarClipPlane>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600013C  System.Void .ctor()
  RVA=0x0696B01C  token=0x600013D  System.Single <DOFieldOfView>b__0()
  RVA=0x0696B03C  token=0x600013E  System.Void <DOFieldOfView>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600013F  System.Void .ctor()
  RVA=0x0696B198  token=0x6000140  System.Single <DONearClipPlane>b__0()
  RVA=0x0696B1B8  token=0x6000141  System.Void <DONearClipPlane>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000142  System.Void .ctor()
  RVA=0x0696B268  token=0x6000143  System.Single <DOOrthoSize>b__0()
  RVA=0x0696B288  token=0x6000144  System.Void <DOOrthoSize>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x2000025
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000145  System.Void .ctor()
  RVA=0x0696B654  token=0x6000146  UnityEngine.Rect <DOPixelRect>b__0()
  RVA=0x0696B688  token=0x6000147  System.Void <DOPixelRect>b__1(UnityEngine.Rect x)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000026
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000148  System.Void .ctor()
  RVA=0x0696BE80  token=0x6000149  UnityEngine.Rect <DORect>b__0()
  RVA=0x0696BEB4  token=0x600014A  System.Void <DORect>b__1(UnityEngine.Rect x)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000027
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600014B  System.Void .ctor()
  RVA=0x0696BEE4  token=0x600014C  UnityEngine.Vector3 <DOShakePosition>b__0()
  RVA=0x0696BF30  token=0x600014D  System.Void <DOShakePosition>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600014E  System.Void .ctor()
  RVA=0x0696BEE4  token=0x600014F  UnityEngine.Vector3 <DOShakePosition>b__0()
  RVA=0x0696BF30  token=0x6000150  System.Void <DOShakePosition>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000151  System.Void .ctor()
  RVA=0x0696A6CC  token=0x6000152  UnityEngine.Vector3 <DOShakeRotation>b__0()
  RVA=0x0696A718  token=0x6000153  System.Void <DOShakeRotation>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x200002A
SIZE:  0x18
FIELDS:
  public            UnityEngine.Camera              target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000154  System.Void .ctor()
  RVA=0x0696A6CC  token=0x6000155  UnityEngine.Vector3 <DOShakeRotation>b__0()
  RVA=0x0696A718  token=0x6000156  System.Void <DOShakeRotation>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass12_0
TYPE:  sealed class
TOKEN: 0x200002B
SIZE:  0x18
FIELDS:
  public            UnityEngine.Light               target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000157  System.Void .ctor()
  RVA=0x0696A790  token=0x6000158  UnityEngine.Color <DOColor>b__0()
  RVA=0x0696A7C4  token=0x6000159  System.Void <DOColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x200002C
SIZE:  0x18
FIELDS:
  public            UnityEngine.Light               target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600015A  System.Void .ctor()
  RVA=0x0696A7F4  token=0x600015B  System.Single <DOIntensity>b__0()
  RVA=0x0696A814  token=0x600015C  System.Void <DOIntensity>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x18
FIELDS:
  public            UnityEngine.Light               target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600015D  System.Void .ctor()
  RVA=0x0696A834  token=0x600015E  System.Single <DOShadowStrength>b__0()
  RVA=0x0696A854  token=0x600015F  System.Void <DOShadowStrength>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass15_0
TYPE:  sealed class
TOKEN: 0x200002E
SIZE:  0x38
FIELDS:
  public            DG.Tweening.Color2              startValue  // 0x10
  public            UnityEngine.LineRenderer        target  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000160  System.Void .ctor()
  RVA=0x02A8F1C0  token=0x6000161  DG.Tweening.Color2 <DOColor>b__0()
  RVA=0x0696A874  token=0x6000162  System.Void <DOColor>b__1(DG.Tweening.Color2 x)
END_CLASS

CLASS: <>c__DisplayClass16_0
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  public            UnityEngine.Material            target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000163  System.Void .ctor()
  RVA=0x0696A8B0  token=0x6000164  UnityEngine.Color <DOColor>b__0()
  RVA=0x0696A8E4  token=0x6000165  System.Void <DOColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass17_0
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.String                   property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000166  System.Void .ctor()
  RVA=0x0696A914  token=0x6000167  UnityEngine.Color <DOColor>b__0()
  RVA=0x0696A950  token=0x6000168  System.Void <DOColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass18_0
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.Int32                    propertyID  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000169  System.Void .ctor()
  RVA=0x0696A988  token=0x600016A  UnityEngine.Color <DOColor>b__0()
  RVA=0x0696A9C4  token=0x600016B  System.Void <DOColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass19_0
TYPE:  sealed class
TOKEN: 0x2000032
SIZE:  0x18
FIELDS:
  public            UnityEngine.Material            target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600016C  System.Void .ctor()
  RVA=0x0696A8B0  token=0x600016D  UnityEngine.Color <DOFade>b__0()
  RVA=0x0696A8E4  token=0x600016E  System.Void <DOFade>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x2000033
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.String                   property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600016F  System.Void .ctor()
  RVA=0x0696A914  token=0x6000170  UnityEngine.Color <DOFade>b__0()
  RVA=0x0696A950  token=0x6000171  System.Void <DOFade>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x2000034
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.Int32                    propertyID  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000172  System.Void .ctor()
  RVA=0x0696A988  token=0x6000173  UnityEngine.Color <DOFade>b__0()
  RVA=0x0696A9C4  token=0x6000174  System.Void <DOFade>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass22_0
TYPE:  sealed class
TOKEN: 0x2000035
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.String                   property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000175  System.Void .ctor()
  RVA=0x0696AA88  token=0x6000176  System.Single <DOFloat>b__0()
  RVA=0x0696AAB0  token=0x6000177  System.Void <DOFloat>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass23_0
TYPE:  sealed class
TOKEN: 0x2000036
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.Int32                    propertyID  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000178  System.Void .ctor()
  RVA=0x0696AADC  token=0x6000179  System.Single <DOFloat>b__0()
  RVA=0x0696AB04  token=0x600017A  System.Void <DOFloat>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x2000037
SIZE:  0x18
FIELDS:
  public            UnityEngine.Material            target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600017B  System.Void .ctor()
  RVA=0x0696AB2C  token=0x600017C  UnityEngine.Vector2 <DOOffset>b__0()
  RVA=0x0696AB4C  token=0x600017D  System.Void <DOOffset>b__1(UnityEngine.Vector2 x)
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x2000038
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.String                   property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600017E  System.Void .ctor()
  RVA=0x0696AB6C  token=0x600017F  UnityEngine.Vector2 <DOOffset>b__0()
  RVA=0x0696AB94  token=0x6000180  System.Void <DOOffset>b__1(UnityEngine.Vector2 x)
END_CLASS

CLASS: <>c__DisplayClass26_0
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x18
FIELDS:
  public            UnityEngine.Material            target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000181  System.Void .ctor()
  RVA=0x0696ABC4  token=0x6000182  UnityEngine.Vector2 <DOTiling>b__0()
  RVA=0x0696ABE4  token=0x6000183  System.Void <DOTiling>b__1(UnityEngine.Vector2 x)
END_CLASS

CLASS: <>c__DisplayClass27_0
TYPE:  sealed class
TOKEN: 0x200003A
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.String                   property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000184  System.Void .ctor()
  RVA=0x0696AC04  token=0x6000185  UnityEngine.Vector2 <DOTiling>b__0()
  RVA=0x0696AC2C  token=0x6000186  System.Void <DOTiling>b__1(UnityEngine.Vector2 x)
END_CLASS

CLASS: <>c__DisplayClass28_0
TYPE:  sealed class
TOKEN: 0x200003B
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.String                   property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000187  System.Void .ctor()
  RVA=0x0696AC5C  token=0x6000188  UnityEngine.Vector4 <DOVector>b__0()
  RVA=0x0696AC98  token=0x6000189  System.Void <DOVector>b__1(UnityEngine.Vector4 x)
END_CLASS

CLASS: <>c__DisplayClass29_0
TYPE:  sealed class
TOKEN: 0x200003C
SIZE:  0x20
FIELDS:
  public            UnityEngine.Material            target  // 0x10
  public            System.Int32                    propertyID  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600018A  System.Void .ctor()
  RVA=0x0696ACD0  token=0x600018B  UnityEngine.Vector4 <DOVector>b__0()
  RVA=0x0696AD0C  token=0x600018C  System.Void <DOVector>b__1(UnityEngine.Vector4 x)
END_CLASS

CLASS: <>c__DisplayClass30_0
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x18
FIELDS:
  public            UnityEngine.TrailRenderer       target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600018D  System.Void .ctor()
  RVA=0x0696AE2C  token=0x600018E  UnityEngine.Vector2 <DOResize>b__0()
  RVA=0x0696AE78  token=0x600018F  System.Void <DOResize>b__1(UnityEngine.Vector2 x)
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x18
FIELDS:
  public            UnityEngine.TrailRenderer       target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000190  System.Void .ctor()
  RVA=0x0696AEC0  token=0x6000191  System.Single <DOTime>b__0()
  RVA=0x0696AEE0  token=0x6000192  System.Void <DOTime>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass32_0
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000193  System.Void .ctor()
  RVA=0x0696AF00  token=0x6000194  UnityEngine.Vector3 <DOMove>b__0()
  RVA=0x0696AF3C  token=0x6000195  System.Void <DOMove>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass33_0
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000196  System.Void .ctor()
  RVA=0x0696AF00  token=0x6000197  UnityEngine.Vector3 <DOMoveX>b__0()
  RVA=0x0696AF3C  token=0x6000198  System.Void <DOMoveX>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass34_0
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000199  System.Void .ctor()
  RVA=0x0696AF00  token=0x600019A  UnityEngine.Vector3 <DOMoveY>b__0()
  RVA=0x0696AF3C  token=0x600019B  System.Void <DOMoveY>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass35_0
TYPE:  sealed class
TOKEN: 0x2000042
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600019C  System.Void .ctor()
  RVA=0x0696AF00  token=0x600019D  UnityEngine.Vector3 <DOMoveZ>b__0()
  RVA=0x0696AF3C  token=0x600019E  System.Void <DOMoveZ>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass36_0
TYPE:  sealed class
TOKEN: 0x2000043
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600019F  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001A0  UnityEngine.Vector3 <DOLocalMove>b__0()
  RVA=0x0696AFB0  token=0x60001A1  System.Void <DOLocalMove>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass37_0
TYPE:  sealed class
TOKEN: 0x2000044
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001A2  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001A3  UnityEngine.Vector3 <DOLocalMoveX>b__0()
  RVA=0x0696AFB0  token=0x60001A4  System.Void <DOLocalMoveX>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass38_0
TYPE:  sealed class
TOKEN: 0x2000045
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001A5  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001A6  UnityEngine.Vector3 <DOLocalMoveY>b__0()
  RVA=0x0696AFB0  token=0x60001A7  System.Void <DOLocalMoveY>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass39_0
TYPE:  sealed class
TOKEN: 0x2000046
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001A8  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001A9  UnityEngine.Vector3 <DOLocalMoveZ>b__0()
  RVA=0x0696AFB0  token=0x60001AA  System.Void <DOLocalMoveZ>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass40_0
TYPE:  sealed class
TOKEN: 0x2000047
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001AB  System.Void .ctor()
  RVA=0x0696B05C  token=0x60001AC  UnityEngine.Quaternion <DORotate>b__0()
  RVA=0x0696B090  token=0x60001AD  System.Void <DORotate>b__1(UnityEngine.Quaternion x)
END_CLASS

CLASS: <>c__DisplayClass41_0
TYPE:  sealed class
TOKEN: 0x2000048
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001AE  System.Void .ctor()
  RVA=0x0696B05C  token=0x60001AF  UnityEngine.Quaternion <DORotateQuaternion>b__0()
  RVA=0x0696B090  token=0x60001B0  System.Void <DORotateQuaternion>b__1(UnityEngine.Quaternion x)
END_CLASS

CLASS: <>c__DisplayClass42_0
TYPE:  sealed class
TOKEN: 0x2000049
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001B1  System.Void .ctor()
  RVA=0x0696B0C0  token=0x60001B2  UnityEngine.Quaternion <DOLocalRotate>b__0()
  RVA=0x0696B0F4  token=0x60001B3  System.Void <DOLocalRotate>b__1(UnityEngine.Quaternion x)
END_CLASS

CLASS: <>c__DisplayClass43_0
TYPE:  sealed class
TOKEN: 0x200004A
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001B4  System.Void .ctor()
  RVA=0x046383D0  token=0x60001B5  UnityEngine.Quaternion <DOLocalRotateQuaternion>b__0()
  RVA=0x036CBD00  token=0x60001B6  System.Void <DOLocalRotateQuaternion>b__1(UnityEngine.Quaternion x)
END_CLASS

CLASS: <>c__DisplayClass44_0
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001B7  System.Void .ctor()
  RVA=0x0696B124  token=0x60001B8  UnityEngine.Vector3 <DOScale>b__0()
  RVA=0x0696B160  token=0x60001B9  System.Void <DOScale>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass45_0
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001BA  System.Void .ctor()
  RVA=0x0696B124  token=0x60001BB  UnityEngine.Vector3 <DOScale>b__0()
  RVA=0x0696B160  token=0x60001BC  System.Void <DOScale>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass46_0
TYPE:  sealed class
TOKEN: 0x200004D
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001BD  System.Void .ctor()
  RVA=0x0696B124  token=0x60001BE  UnityEngine.Vector3 <DOScaleX>b__0()
  RVA=0x0696B160  token=0x60001BF  System.Void <DOScaleX>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass47_0
TYPE:  sealed class
TOKEN: 0x200004E
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001C0  System.Void .ctor()
  RVA=0x0696B124  token=0x60001C1  UnityEngine.Vector3 <DOScaleY>b__0()
  RVA=0x0696B160  token=0x60001C2  System.Void <DOScaleY>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass48_0
TYPE:  sealed class
TOKEN: 0x200004F
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001C3  System.Void .ctor()
  RVA=0x0696B124  token=0x60001C4  UnityEngine.Vector3 <DOScaleZ>b__0()
  RVA=0x0696B160  token=0x60001C5  System.Void <DOScaleZ>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass51_0
TYPE:  sealed class
TOKEN: 0x2000050
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001C6  System.Void .ctor()
  RVA=0x0696B05C  token=0x60001C7  UnityEngine.Quaternion <LookAt>b__0()
  RVA=0x0696B090  token=0x60001C8  System.Void <LookAt>b__1(UnityEngine.Quaternion x)
END_CLASS

CLASS: <>c__DisplayClass52_0
TYPE:  sealed class
TOKEN: 0x2000051
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001C9  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001CA  UnityEngine.Vector3 <DOPunchPosition>b__0()
  RVA=0x0696AFB0  token=0x60001CB  System.Void <DOPunchPosition>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass53_0
TYPE:  sealed class
TOKEN: 0x2000052
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001CC  System.Void .ctor()
  RVA=0x0696B124  token=0x60001CD  UnityEngine.Vector3 <DOPunchScale>b__0()
  RVA=0x0696B160  token=0x60001CE  System.Void <DOPunchScale>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass54_0
TYPE:  sealed class
TOKEN: 0x2000053
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001CF  System.Void .ctor()
  RVA=0x0696B1D8  token=0x60001D0  UnityEngine.Vector3 <DOPunchRotation>b__0()
  RVA=0x0696B214  token=0x60001D1  System.Void <DOPunchRotation>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass55_0
TYPE:  sealed class
TOKEN: 0x2000054
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001D2  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001D3  UnityEngine.Vector3 <DOShakePosition>b__0()
  RVA=0x0696AFB0  token=0x60001D4  System.Void <DOShakePosition>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass56_0
TYPE:  sealed class
TOKEN: 0x2000055
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001D5  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001D6  UnityEngine.Vector3 <DOShakePosition>b__0()
  RVA=0x0696AFB0  token=0x60001D7  System.Void <DOShakePosition>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass57_0
TYPE:  sealed class
TOKEN: 0x2000056
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001D8  System.Void .ctor()
  RVA=0x0696B1D8  token=0x60001D9  UnityEngine.Vector3 <DOShakeRotation>b__0()
  RVA=0x0696B214  token=0x60001DA  System.Void <DOShakeRotation>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass58_0
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001DB  System.Void .ctor()
  RVA=0x0696B1D8  token=0x60001DC  UnityEngine.Vector3 <DOShakeRotation>b__0()
  RVA=0x0696B214  token=0x60001DD  System.Void <DOShakeRotation>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass59_0
TYPE:  sealed class
TOKEN: 0x2000058
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001DE  System.Void .ctor()
  RVA=0x0696B124  token=0x60001DF  UnityEngine.Vector3 <DOShakeScale>b__0()
  RVA=0x0696B160  token=0x60001E0  System.Void <DOShakeScale>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass60_0
TYPE:  sealed class
TOKEN: 0x2000059
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001E1  System.Void .ctor()
  RVA=0x0696B124  token=0x60001E2  UnityEngine.Vector3 <DOShakeScale>b__0()
  RVA=0x0696B160  token=0x60001E3  System.Void <DOShakeScale>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass61_0
TYPE:  sealed class
TOKEN: 0x200005A
SIZE:  0x48
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
  public            System.Single                   startPosY  // 0x18
  public            System.Boolean                  offsetYSet  // 0x1c
  public            System.Single                   offsetY  // 0x20
  public            DG.Tweening.Sequence            s  // 0x28
  public            UnityEngine.Vector3             endValue  // 0x30
  public            DG.Tweening.Tween               yTween  // 0x40
METHODS:
  RVA=0x041E1670  token=0x60001E4  System.Void .ctor()
  RVA=0x0696AF00  token=0x60001E5  UnityEngine.Vector3 <DOJump>b__0()
  RVA=0x0696AF3C  token=0x60001E6  System.Void <DOJump>b__1(UnityEngine.Vector3 x)
  RVA=0x0696B2A8  token=0x60001E7  System.Void <DOJump>b__2()
  RVA=0x0696AF00  token=0x60001E8  UnityEngine.Vector3 <DOJump>b__3()
  RVA=0x0696AF3C  token=0x60001E9  System.Void <DOJump>b__4(UnityEngine.Vector3 x)
  RVA=0x0696AF00  token=0x60001EA  UnityEngine.Vector3 <DOJump>b__5()
  RVA=0x0696AF3C  token=0x60001EB  System.Void <DOJump>b__6(UnityEngine.Vector3 x)
  RVA=0x0696B2DC  token=0x60001EC  System.Void <DOJump>b__7()
END_CLASS

CLASS: <>c__DisplayClass62_0
TYPE:  sealed class
TOKEN: 0x200005B
SIZE:  0x48
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
  public            System.Single                   startPosY  // 0x18
  public            System.Boolean                  offsetYSet  // 0x1c
  public            System.Single                   offsetY  // 0x20
  public            DG.Tweening.Sequence            s  // 0x28
  public            UnityEngine.Vector3             endValue  // 0x30
  public            DG.Tweening.Tween               yTween  // 0x40
METHODS:
  RVA=0x041E1670  token=0x60001ED  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001EE  UnityEngine.Vector3 <DOLocalJump>b__0()
  RVA=0x0696AFB0  token=0x60001EF  System.Void <DOLocalJump>b__1(UnityEngine.Vector3 x)
  RVA=0x0696B3B8  token=0x60001F0  System.Void <DOLocalJump>b__2()
  RVA=0x0696AF74  token=0x60001F1  UnityEngine.Vector3 <DOLocalJump>b__3()
  RVA=0x0696AFB0  token=0x60001F2  System.Void <DOLocalJump>b__4(UnityEngine.Vector3 x)
  RVA=0x0696AF74  token=0x60001F3  UnityEngine.Vector3 <DOLocalJump>b__5()
  RVA=0x0696AFB0  token=0x60001F4  System.Void <DOLocalJump>b__6(UnityEngine.Vector3 x)
  RVA=0x0696B3EC  token=0x60001F5  System.Void <DOLocalJump>b__7()
END_CLASS

CLASS: <>c__DisplayClass63_0
TYPE:  sealed class
TOKEN: 0x200005C
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001F6  System.Void .ctor()
  RVA=0x0696AF00  token=0x60001F7  UnityEngine.Vector3 <DOPath>b__0()
  RVA=0x0696AF3C  token=0x60001F8  System.Void <DOPath>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass64_0
TYPE:  sealed class
TOKEN: 0x200005D
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001F9  System.Void .ctor()
  RVA=0x0696AF74  token=0x60001FA  UnityEngine.Vector3 <DOLocalPath>b__0()
  RVA=0x0696AFB0  token=0x60001FB  System.Void <DOLocalPath>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass65_0
TYPE:  sealed class
TOKEN: 0x200005E
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001FC  System.Void .ctor()
  RVA=0x0696AF00  token=0x60001FD  UnityEngine.Vector3 <DOPath>b__0()
  RVA=0x0696AF3C  token=0x60001FE  System.Void <DOPath>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass66_0
TYPE:  sealed class
TOKEN: 0x200005F
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001FF  System.Void .ctor()
  RVA=0x0696AF74  token=0x6000200  UnityEngine.Vector3 <DOLocalPath>b__0()
  RVA=0x0696AFB0  token=0x6000201  System.Void <DOLocalPath>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass67_0
TYPE:  sealed class
TOKEN: 0x2000060
SIZE:  0x18
FIELDS:
  public            DG.Tweening.Tween               target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000202  System.Void .ctor()
  RVA=0x0696B4C8  token=0x6000203  System.Single <DOTimeScale>b__0()
  RVA=0x053958D0  token=0x6000204  System.Void <DOTimeScale>b__1(System.Single x)
END_CLASS

CLASS: <>c__DisplayClass68_0
TYPE:  sealed class
TOKEN: 0x2000061
SIZE:  0x28
FIELDS:
  public            UnityEngine.Color               to  // 0x10
  public            UnityEngine.Light               target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000205  System.Void .ctor()
  RVA=0x02B2D510  token=0x6000206  UnityEngine.Color <DOBlendableColor>b__0()
  RVA=0x0696B50C  token=0x6000207  System.Void <DOBlendableColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass69_0
TYPE:  sealed class
TOKEN: 0x2000062
SIZE:  0x28
FIELDS:
  public            UnityEngine.Color               to  // 0x10
  public            UnityEngine.Material            target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000208  System.Void .ctor()
  RVA=0x02B2D510  token=0x6000209  UnityEngine.Color <DOBlendableColor>b__0()
  RVA=0x0696B5B0  token=0x600020A  System.Void <DOBlendableColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass70_0
TYPE:  sealed class
TOKEN: 0x2000063
SIZE:  0x30
FIELDS:
  public            UnityEngine.Color               to  // 0x10
  public            UnityEngine.Material            target  // 0x20
  public            System.String                   property  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600020B  System.Void .ctor()
  RVA=0x02B2D510  token=0x600020C  UnityEngine.Color <DOBlendableColor>b__0()
  RVA=0x0696B6B8  token=0x600020D  System.Void <DOBlendableColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass71_0
TYPE:  sealed class
TOKEN: 0x2000064
SIZE:  0x30
FIELDS:
  public            UnityEngine.Color               to  // 0x10
  public            UnityEngine.Material            target  // 0x20
  public            System.Int32                    propertyID  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600020E  System.Void .ctor()
  RVA=0x02B2D510  token=0x600020F  UnityEngine.Color <DOBlendableColor>b__0()
  RVA=0x0696B770  token=0x6000210  System.Void <DOBlendableColor>b__1(UnityEngine.Color x)
END_CLASS

CLASS: <>c__DisplayClass72_0
TYPE:  sealed class
TOKEN: 0x2000065
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             to  // 0x10
  public            UnityEngine.Transform           target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000211  System.Void .ctor()
  RVA=0x04D8C950  token=0x6000212  UnityEngine.Vector3 <DOBlendableMoveBy>b__0()
  RVA=0x0696B824  token=0x6000213  System.Void <DOBlendableMoveBy>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass73_0
TYPE:  sealed class
TOKEN: 0x2000066
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             to  // 0x10
  public            UnityEngine.Transform           target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000214  System.Void .ctor()
  RVA=0x04D8C950  token=0x6000215  UnityEngine.Vector3 <DOBlendableLocalMoveBy>b__0()
  RVA=0x0696B8F0  token=0x6000216  System.Void <DOBlendableLocalMoveBy>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: <>c__DisplayClass74_0
TYPE:  sealed class
TOKEN: 0x2000067
SIZE:  0x28
FIELDS:
  public            UnityEngine.Quaternion          to  // 0x10
  public            UnityEngine.Transform           target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000217  System.Void .ctor()
  RVA=0x02B2D510  token=0x6000218  UnityEngine.Quaternion <DOBlendableRotateBy>b__0()
  RVA=0x0696B9BC  token=0x6000219  System.Void <DOBlendableRotateBy>b__1(UnityEngine.Quaternion x)
END_CLASS

CLASS: <>c__DisplayClass75_0
TYPE:  sealed class
TOKEN: 0x2000068
SIZE:  0x28
FIELDS:
  public            UnityEngine.Quaternion          to  // 0x10
  public            UnityEngine.Transform           target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600021A  System.Void .ctor()
  RVA=0x02B2D510  token=0x600021B  UnityEngine.Quaternion <DOBlendableLocalRotateBy>b__0()
  RVA=0x0696BAFC  token=0x600021C  System.Void <DOBlendableLocalRotateBy>b__1(UnityEngine.Quaternion x)
END_CLASS

CLASS: <>c__DisplayClass76_0
TYPE:  sealed class
TOKEN: 0x2000069
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             to  // 0x10
  public            UnityEngine.Transform           target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600021D  System.Void .ctor()
  RVA=0x04D8C950  token=0x600021E  UnityEngine.Vector3 <DOBlendablePunchRotation>b__0()
  RVA=0x0696BC3C  token=0x600021F  System.Void <DOBlendablePunchRotation>b__1(UnityEngine.Vector3 v)
END_CLASS

CLASS: <>c__DisplayClass77_0
TYPE:  sealed class
TOKEN: 0x200006A
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             to  // 0x10
  public            UnityEngine.Transform           target  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000220  System.Void .ctor()
  RVA=0x04D8C950  token=0x6000221  UnityEngine.Vector3 <DOBlendableScaleBy>b__0()
  RVA=0x0696BDB4  token=0x6000222  System.Void <DOBlendableScaleBy>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: Sequence
TYPE:  static class
TOKEN: 0x20000A3
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x06981D60  token=0x60003D2  System.Void LogAddToNullSequence()
  RVA=0x06981CF0  token=0x60003D3  System.Void LogAddToInactiveSequence()
  RVA=0x06981D28  token=0x60003D4  System.Void LogAddToLockedSequence()
  RVA=0x06981CB8  token=0x60003D5  System.Void LogAddNullTween()
  RVA=0x06981C7C  token=0x60003D6  System.Void LogAddInactiveTween(DG.Tweening.Tween t)
  RVA=0x06981C40  token=0x60003D7  System.Void LogAddAlreadySequencedTween(DG.Tweening.Tween t)
END_CLASS

CLASS: <WaitForCompletion>d__17
TYPE:  sealed class
TOKEN: 0x20000A5
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            DG.Tweening.Tween               t  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003EB  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003EC  System.Void System.IDisposable.Dispose()
  RVA=0x06984EBC  token=0x60003ED  System.Boolean MoveNext()
  RVA=0x06984F10  token=0x60003EF  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <WaitForRewind>d__18
TYPE:  sealed class
TOKEN: 0x20000A6
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            DG.Tweening.Tween               t  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003F1  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003F2  System.Void System.IDisposable.Dispose()
  RVA=0x06985158  token=0x60003F3  System.Boolean MoveNext()
  RVA=0x069851D4  token=0x60003F5  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <WaitForKill>d__19
TYPE:  sealed class
TOKEN: 0x20000A7
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            DG.Tweening.Tween               t  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003F7  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003F8  System.Void System.IDisposable.Dispose()
  RVA=0x06985004  token=0x60003F9  System.Boolean MoveNext()
  RVA=0x06985050  token=0x60003FB  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <WaitForElapsedLoops>d__20
TYPE:  sealed class
TOKEN: 0x20000A8
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            DG.Tweening.Tween               t  // 0x20
  public            System.Int32                    elapsedLoops  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003FD  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003FE  System.Void System.IDisposable.Dispose()
  RVA=0x06984F5C  token=0x60003FF  System.Boolean MoveNext()
  RVA=0x06984FB8  token=0x6000401  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <WaitForPosition>d__21
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            DG.Tweening.Tween               t  // 0x20
  public            System.Single                   position  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000403  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000404  System.Void System.IDisposable.Dispose()
  RVA=0x0698509C  token=0x6000405  System.Boolean MoveNext()
  RVA=0x0698510C  token=0x6000407  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <WaitForStart>d__22
TYPE:  sealed class
TOKEN: 0x20000AA
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            DG.Tweening.Tween               t  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000409  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600040A  System.Void System.IDisposable.Dispose()
  RVA=0x06985220  token=0x600040B  System.Boolean MoveNext()
  RVA=0x06985274  token=0x600040D  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: SettingsLocation
TYPE:  sealed struct
TOKEN: 0x20000AC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.DOTweenSettings.SettingsLocationAssetsDirectory  // const
  public    static  DG.Tweening.Core.DOTweenSettings.SettingsLocationDOTweenDirectory  // const
  public    static  DG.Tweening.Core.DOTweenSettings.SettingsLocationDemigiantDirectory  // const
METHODS:
END_CLASS

CLASS: SafeModeOptions
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x18
FIELDS:
  public            DG.Tweening.Core.Enums.SafeModeLogBehaviourlogBehaviour  // 0x10
  public            DG.Tweening.Core.Enums.NestedTweenFailureBehaviournestedTweenFailureBehaviour  // 0x14
METHODS:
  RVA=0x04D8CAB0  token=0x6000410  System.Void .ctor()
END_CLASS

CLASS: ModulesSetup
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x20
FIELDS:
  public            System.Boolean                  showPanel  // 0x10
  public            System.Boolean                  audioEnabled  // 0x11
  public            System.Boolean                  physicsEnabled  // 0x12
  public            System.Boolean                  physics2DEnabled  // 0x13
  public            System.Boolean                  spriteEnabled  // 0x14
  public            System.Boolean                  uiEnabled  // 0x15
  public            System.Boolean                  textMeshProEnabled  // 0x16
  public            System.Boolean                  tk2DEnabled  // 0x17
  public            System.Boolean                  deAudioEnabled  // 0x18
  public            System.Boolean                  deUnityExtendedEnabled  // 0x19
  public            System.Boolean                  epoOutlineEnabled  // 0x1a
METHODS:
  RVA=0x04D8CA00  token=0x6000411  System.Void .ctor()
END_CLASS

CLASS: SafeModeReportType
TYPE:  sealed struct
TOKEN: 0x20000B2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.SafeModeReport.SafeModeReportTypeUnset  // const
  public    static  DG.Tweening.Core.SafeModeReport.SafeModeReportTypeTargetOrFieldMissing  // const
  public    static  DG.Tweening.Core.SafeModeReport.SafeModeReportTypeCallback  // const
  public    static  DG.Tweening.Core.SafeModeReport.SafeModeReportTypeStartupFailure  // const
METHODS:
END_CLASS

CLASS: CapacityIncreaseMode
TYPE:  sealed struct
TOKEN: 0x20000B6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.TweenManager.CapacityIncreaseModeTweenersAndSequences  // const
  public    static  DG.Tweening.Core.TweenManager.CapacityIncreaseModeTweenersOnly  // const
  public    static  DG.Tweening.Core.TweenManager.CapacityIncreaseModeSequencesOnly  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C3
SIZE:  0x10
FIELDS:
  public    static readonly DG.Tweening.Core.Easing.EaseManager.<>c<>9  // static @ 0x0
  public    static  DG.Tweening.EaseFunction        <>9__4_0  // static @ 0x8
  public    static  DG.Tweening.EaseFunction        <>9__4_1  // static @ 0x10
  public    static  DG.Tweening.EaseFunction        <>9__4_2  // static @ 0x18
  public    static  DG.Tweening.EaseFunction        <>9__4_3  // static @ 0x20
  public    static  DG.Tweening.EaseFunction        <>9__4_4  // static @ 0x28
  public    static  DG.Tweening.EaseFunction        <>9__4_5  // static @ 0x30
  public    static  DG.Tweening.EaseFunction        <>9__4_6  // static @ 0x38
  public    static  DG.Tweening.EaseFunction        <>9__4_7  // static @ 0x40
  public    static  DG.Tweening.EaseFunction        <>9__4_8  // static @ 0x48
  public    static  DG.Tweening.EaseFunction        <>9__4_9  // static @ 0x50
  public    static  DG.Tweening.EaseFunction        <>9__4_10  // static @ 0x58
  public    static  DG.Tweening.EaseFunction        <>9__4_11  // static @ 0x60
  public    static  DG.Tweening.EaseFunction        <>9__4_12  // static @ 0x68
  public    static  DG.Tweening.EaseFunction        <>9__4_13  // static @ 0x70
  public    static  DG.Tweening.EaseFunction        <>9__4_14  // static @ 0x78
  public    static  DG.Tweening.EaseFunction        <>9__4_15  // static @ 0x80
  public    static  DG.Tweening.EaseFunction        <>9__4_16  // static @ 0x88
  public    static  DG.Tweening.EaseFunction        <>9__4_17  // static @ 0x90
  public    static  DG.Tweening.EaseFunction        <>9__4_18  // static @ 0x98
  public    static  DG.Tweening.EaseFunction        <>9__4_19  // static @ 0xa0
  public    static  DG.Tweening.EaseFunction        <>9__4_20  // static @ 0xa8
  public    static  DG.Tweening.EaseFunction        <>9__4_21  // static @ 0xb0
  public    static  DG.Tweening.EaseFunction        <>9__4_22  // static @ 0xb8
  public    static  DG.Tweening.EaseFunction        <>9__4_23  // static @ 0xc0
  public    static  DG.Tweening.EaseFunction        <>9__4_24  // static @ 0xc8
  public    static  DG.Tweening.EaseFunction        <>9__4_25  // static @ 0xd0
  public    static  DG.Tweening.EaseFunction        <>9__4_26  // static @ 0xd8
  public    static  DG.Tweening.EaseFunction        <>9__4_27  // static @ 0xe0
  public    static  DG.Tweening.EaseFunction        <>9__4_28  // static @ 0xe8
  public    static  DG.Tweening.EaseFunction        <>9__4_29  // static @ 0xf0
  public    static  DG.Tweening.EaseFunction        <>9__4_30  // static @ 0xf8
  public    static  DG.Tweening.EaseFunction        <>9__4_31  // static @ 0x100
  public    static  DG.Tweening.EaseFunction        <>9__4_32  // static @ 0x108
  public    static  DG.Tweening.EaseFunction        <>9__4_33  // static @ 0x110
  public    static  DG.Tweening.EaseFunction        <>9__4_34  // static @ 0x118
  public    static  DG.Tweening.EaseFunction        <>9__4_35  // static @ 0x120
METHODS:
  RVA=0x0698AD38  token=0x6000470  System.Void .cctor()
  RVA=0x041E1670  token=0x6000471  System.Void .ctor()
  RVA=0x06989EC8  token=0x6000472  System.Single <ToEaseFunction>b__4_0(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A290  token=0x6000473  System.Single <ToEaseFunction>b__4_1(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AA80  token=0x6000474  System.Single <ToEaseFunction>b__4_2(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698ABF4  token=0x6000475  System.Single <ToEaseFunction>b__4_3(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x04D8CB60  token=0x6000476  System.Single <ToEaseFunction>b__4_4(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698ABD8  token=0x6000477  System.Single <ToEaseFunction>b__4_5(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AC6C  token=0x6000478  System.Single <ToEaseFunction>b__4_6(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698ACB8  token=0x6000479  System.Single <ToEaseFunction>b__4_7(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698ACC8  token=0x600047A  System.Single <ToEaseFunction>b__4_8(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698ACE8  token=0x600047B  System.Single <ToEaseFunction>b__4_9(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989ED0  token=0x600047C  System.Single <ToEaseFunction>b__4_10(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989EE4  token=0x600047D  System.Single <ToEaseFunction>b__4_11(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989F10  token=0x600047E  System.Single <ToEaseFunction>b__4_12(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989F6C  token=0x600047F  System.Single <ToEaseFunction>b__4_13(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989F84  token=0x6000480  System.Single <ToEaseFunction>b__4_14(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989FAC  token=0x6000481  System.Single <ToEaseFunction>b__4_15(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A00C  token=0x6000482  System.Single <ToEaseFunction>b__4_16(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A094  token=0x6000483  System.Single <ToEaseFunction>b__4_17(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A124  token=0x6000484  System.Single <ToEaseFunction>b__4_18(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A214  token=0x6000485  System.Single <ToEaseFunction>b__4_19(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A308  token=0x6000486  System.Single <ToEaseFunction>b__4_20(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A378  token=0x6000487  System.Single <ToEaseFunction>b__4_21(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A434  token=0x6000488  System.Single <ToEaseFunction>b__4_22(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A5B8  token=0x6000489  System.Single <ToEaseFunction>b__4_23(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A744  token=0x600048A  System.Single <ToEaseFunction>b__4_24(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A95C  token=0x600048B  System.Single <ToEaseFunction>b__4_25(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A97C  token=0x600048C  System.Single <ToEaseFunction>b__4_26(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698A9A8  token=0x600048D  System.Single <ToEaseFunction>b__4_27(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AA10  token=0x600048E  System.Single <ToEaseFunction>b__4_28(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AA50  token=0x600048F  System.Single <ToEaseFunction>b__4_29(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AAE8  token=0x6000490  System.Single <ToEaseFunction>b__4_30(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AB18  token=0x6000491  System.Single <ToEaseFunction>b__4_31(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AB48  token=0x6000492  System.Single <ToEaseFunction>b__4_32(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698AB78  token=0x6000493  System.Single <ToEaseFunction>b__4_33(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698ABA8  token=0x6000494  System.Single <ToEaseFunction>b__4_34(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0698ABD8  token=0x6000495  System.Single <ToEaseFunction>b__4_35(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
END_CLASS

CLASS: __StaticArrayInitTypeSize=20
TYPE:  sealed struct
TOKEN: 0x20000C7
SIZE:  0x24
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=50
TYPE:  sealed struct
TOKEN: 0x20000C8
SIZE:  0x42
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=120
TYPE:  sealed struct
TOKEN: 0x20000C9
SIZE:  0x88
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000C6
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=504EF0676A416B9CCF117C378BDCB6C5CA0EA9505154E3F0F7CC8570285DB79663  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20921D53955C220A863C9C5D33DD45329C6F74471C522EA5D46EA838D8D4838055  // static @ 0x32
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=120A203B1199E78DE3BB75B28FC520ED2F86ADB2749BFC52E3ACA275A3BE2587678  // static @ 0x46
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=50C845A807AA8EC73757DC1B07EA49311061FEDECA35AFFE91C5474E0814925020  // static @ 0xbe
METHODS:
END_CLASS

CLASS: DG.Tweening.AutoPlay
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.AutoPlay            None  // const
  public    static  DG.Tweening.AutoPlay            AutoPlaySequences  // const
  public    static  DG.Tweening.AutoPlay            AutoPlayTweeners  // const
  public    static  DG.Tweening.AutoPlay            All  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.AxisConstraint
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.AxisConstraint      None  // const
  public    static  DG.Tweening.AxisConstraint      X  // const
  public    static  DG.Tweening.AxisConstraint      Y  // const
  public    static  DG.Tweening.AxisConstraint      Z  // const
  public    static  DG.Tweening.AxisConstraint      W  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Color2
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x30
FIELDS:
  public            UnityEngine.Color               ca  // 0x10
  public            UnityEngine.Color               cb  // 0x20
METHODS:
  RVA=0x04D8C810  token=0x6000001  System.Void .ctor(UnityEngine.Color ca, UnityEngine.Color cb)
  RVA=0x0695A918  token=0x6000002  DG.Tweening.Color2 op_Addition(DG.Tweening.Color2 c1, DG.Tweening.Color2 c2)
  RVA=0x0695A9F4  token=0x6000003  DG.Tweening.Color2 op_Subtraction(DG.Tweening.Color2 c1, DG.Tweening.Color2 c2)
  RVA=0x0695A994  token=0x6000004  DG.Tweening.Color2 op_Multiply(DG.Tweening.Color2 c1, System.Single f)
END_CLASS

CLASS: DG.Tweening.TweenCallback
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x02F28650  token=0x6000005  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000006  System.Void Invoke()
  RVA=0x053DED94  token=0x6000007  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000008  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: DG.Tweening.TweenCallback`1
TYPE:  sealed class
TOKEN: 0x2000006
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000009  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600000A  System.Void Invoke(T value)
  RVA=-1  // runtime  token=0x600000B  System.IAsyncResult BeginInvoke(T value, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x600000C  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: DG.Tweening.EaseFunction
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0480E230  token=0x600000D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0695F0D4  token=0x600000E  System.Single Invoke(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x0695F020  token=0x600000F  System.IAsyncResult BeginInvoke(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD0828  token=0x6000010  System.Single EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: DG.Tweening.DOCurve
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: DG.Tweening.DOTween
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   Version  // static @ 0x0
  public    static  System.Boolean                  useSafeMode  // static @ 0x8
  public    static  DG.Tweening.Core.Enums.SafeModeLogBehavioursafeModeLogBehaviour  // static @ 0xc
  public    static  DG.Tweening.Core.Enums.NestedTweenFailureBehaviournestedTweenFailureBehaviour  // static @ 0x10
  public    static  System.Boolean                  showUnityEditorReport  // static @ 0x14
  public    static  System.Single                   timeScale  // static @ 0x18
  public    static  System.Single                   unscaledTimeScale  // static @ 0x1c
  public    static  System.Boolean                  useSmoothDeltaTime  // static @ 0x20
  public    static  System.Single                   maxSmoothUnscaledTime  // static @ 0x24
  private   static  DG.Tweening.Core.Enums.RewindCallbackModerewindCallbackMode  // static @ 0x28
  private   static  DG.Tweening.LogBehaviour        _logBehaviour  // static @ 0x2c
  public    static  System.Func<UnityEngine.LogType,System.Object,System.Boolean>onWillLog  // static @ 0x30
  public    static  System.Boolean                  drawGizmos  // static @ 0x38
  public    static  System.Boolean                  debugMode  // static @ 0x39
  private   static  System.Boolean                  _fooDebugStoreTargetId  // static @ 0x3a
  public    static  DG.Tweening.UpdateType          defaultUpdateType  // static @ 0x3c
  public    static  System.Boolean                  defaultTimeScaleIndependent  // static @ 0x40
  public    static  DG.Tweening.AutoPlay            defaultAutoPlay  // static @ 0x44
  public    static  System.Boolean                  defaultAutoKill  // static @ 0x48
  public    static  DG.Tweening.LoopType            defaultLoopType  // static @ 0x4c
  public    static  System.Boolean                  defaultRecyclable  // static @ 0x50
  public    static  DG.Tweening.Ease                defaultEaseType  // static @ 0x54
  public    static  System.Single                   defaultEaseOvershootOrAmplitude  // static @ 0x58
  public    static  System.Single                   defaultEasePeriod  // static @ 0x5c
  public    static  DG.Tweening.Core.DOTweenComponentinstance  // static @ 0x60
  private   static  System.Boolean                  _foo_isQuitting  // static @ 0x68
  private   static  System.Int32                    maxActiveTweenersReached  // static @ 0x6c
  private   static  System.Int32                    maxActiveSequencesReached  // static @ 0x70
  private   static  DG.Tweening.Core.SafeModeReport safeModeReport  // static @ 0x74
  private   static readonly System.Collections.Generic.List<DG.Tweening.TweenCallback>GizmosDelegates  // static @ 0x88
  private   static  System.Boolean                  initialized  // static @ 0x90
  private   static  System.Int32                    _isQuittingFrame  // static @ 0x94
PROPERTIES:
  logBehaviour  get=0x0695DDA8  set=0x04709CA0
  debugStoreTargetId  get=0x0429EC00  set=0x0695DDF8
  isQuitting  get=0x04D2B300  set=0x0695DE4C
METHODS:
  RVA=0x0695B624  token=0x600001A  DG.Tweening.IDOTweenInit Init(System.Nullable<System.Boolean> recycleAllByDefault, System.Nullable<System.Boolean> useSafeMode, System.Nullable<DG.Tweening.LogBehaviour> logBehaviour)
  RVA=0x041A7160  token=0x600001B  System.Void AutoInit()
  RVA=0x047097F0  token=0x600001C  DG.Tweening.IDOTweenInit Init(DG.Tweening.Core.DOTweenSettings settings, System.Nullable<System.Boolean> recycleAllByDefault, System.Nullable<System.Boolean> useSafeMode, System.Nullable<DG.Tweening.LogBehaviour> logBehaviour)
  RVA=0x0695C5C0  token=0x600001D  System.Void SetTweensCapacity(System.Int32 tweenersCapacity, System.Int32 sequencesCapacity)
  RVA=0x0695B188  token=0x600001E  System.Void Clear(System.Boolean destroy)
  RVA=0x0695AEF4  token=0x600001F  System.Void Clear(System.Boolean destroy, System.Boolean isApplicationQuitting)
  RVA=0x0695AEB4  token=0x6000020  System.Void ClearCachedTweens()
  RVA=0x0695DD68  token=0x6000021  System.Int32 Validate()
  RVA=0x0695B948  token=0x6000022  System.Void ManualUpdate(System.Single deltaTime, System.Single unscaledDeltaTime)
  RVA=0x032EE410  token=0x6000023  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> To(DG.Tweening.Core.DOGetter<System.Single> getter, DG.Tweening.Core.DOSetter<System.Single> setter, System.Single endValue, System.Single duration)
  RVA=0x0695D8E0  token=0x6000024  DG.Tweening.Core.TweenerCore<System.Double,System.Double,DG.Tweening.Plugins.Options.NoOptions> To(DG.Tweening.Core.DOGetter<System.Double> getter, DG.Tweening.Core.DOSetter<System.Double> setter, System.Double endValue, System.Single duration)
  RVA=0x0695D7B4  token=0x6000025  DG.Tweening.Core.TweenerCore<System.Int32,System.Int32,DG.Tweening.Plugins.Options.NoOptions> To(DG.Tweening.Core.DOGetter<System.Int32> getter, DG.Tweening.Core.DOSetter<System.Int32> setter, System.Int32 endValue, System.Single duration)
  RVA=0x0695D388  token=0x6000026  DG.Tweening.Core.TweenerCore<System.UInt32,System.UInt32,DG.Tweening.Plugins.Options.UintOptions> To(DG.Tweening.Core.DOGetter<System.UInt32> getter, DG.Tweening.Core.DOSetter<System.UInt32> setter, System.UInt32 endValue, System.Single duration)
  RVA=0x0695D144  token=0x6000027  DG.Tweening.Core.TweenerCore<System.Int64,System.Int64,DG.Tweening.Plugins.Options.NoOptions> To(DG.Tweening.Core.DOGetter<System.Int64> getter, DG.Tweening.Core.DOSetter<System.Int64> setter, System.Int64 endValue, System.Single duration)
  RVA=0x0695D5C4  token=0x6000028  DG.Tweening.Core.TweenerCore<System.UInt64,System.UInt64,DG.Tweening.Plugins.Options.NoOptions> To(DG.Tweening.Core.DOGetter<System.UInt64> getter, DG.Tweening.Core.DOSetter<System.UInt64> setter, System.UInt64 endValue, System.Single duration)
  RVA=0x0695D414  token=0x6000029  DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> To(DG.Tweening.Core.DOGetter<System.String> getter, DG.Tweening.Core.DOSetter<System.String> setter, System.String endValue, System.Single duration)
  RVA=0x0396B590  token=0x600002A  DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> To(DG.Tweening.Core.DOGetter<UnityEngine.Vector2> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector2> setter, UnityEngine.Vector2 endValue, System.Single duration)
  RVA=0x0695D1D0  token=0x600002B  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> To(DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, UnityEngine.Vector3 endValue, System.Single duration)
  RVA=0x0695D96C  token=0x600002C  DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> To(DG.Tweening.Core.DOGetter<UnityEngine.Vector4> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector4> setter, UnityEngine.Vector4 endValue, System.Single duration)
  RVA=0x0695D260  token=0x600002D  DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> To(DG.Tweening.Core.DOGetter<UnityEngine.Quaternion> getter, DG.Tweening.Core.DOSetter<UnityEngine.Quaternion> setter, UnityEngine.Vector3 endValue, System.Single duration)
  RVA=0x0695D300  token=0x600002E  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> To(DG.Tweening.Core.DOGetter<UnityEngine.Color> getter, DG.Tweening.Core.DOSetter<UnityEngine.Color> setter, UnityEngine.Color endValue, System.Single duration)
  RVA=0x0695D52C  token=0x600002F  DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> To(DG.Tweening.Core.DOGetter<UnityEngine.Rect> getter, DG.Tweening.Core.DOSetter<UnityEngine.Rect> setter, UnityEngine.Rect endValue, System.Single duration)
  RVA=0x0695D4A0  token=0x6000030  DG.Tweening.Tweener To(DG.Tweening.Core.DOGetter<UnityEngine.RectOffset> getter, DG.Tweening.Core.DOSetter<UnityEngine.RectOffset> setter, UnityEngine.RectOffset endValue, System.Single duration)
  RVA=-1  // generic def  token=0x6000031  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> To(DG.Tweening.Plugins.Core.ABSTweenPlugin<T1,T2,TPlugOptions> plugin, DG.Tweening.Core.DOGetter<T1> getter, DG.Tweening.Core.DOSetter<T1> setter, T2 endValue, System.Single duration)
  RVA=0x0695D098  token=0x6000032  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> ToAxis(DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single endValue, System.Single duration, DG.Tweening.AxisConstraint axisConstraint)
  RVA=0x045E32F0  token=0x6000033  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> ToAlpha(DG.Tweening.Core.DOGetter<UnityEngine.Color> getter, DG.Tweening.Core.DOSetter<UnityEngine.Color> setter, System.Single endValue, System.Single duration)
  RVA=0x0695D650  token=0x6000034  DG.Tweening.Tweener To(DG.Tweening.Core.DOSetter<System.Single> setter, System.Single startValue, System.Single endValue, System.Single duration)
  RVA=0x0695BFB8  token=0x6000035  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> Punch(DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, UnityEngine.Vector3 direction, System.Single duration, System.Int32 vibrato, System.Single elasticity)
  RVA=0x0695CCFC  token=0x6000036  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> Shake(DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single duration, System.Single strength, System.Int32 vibrato, System.Single randomness, System.Boolean ignoreZAxis, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x0695CC30  token=0x6000037  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> Shake(DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single duration, UnityEngine.Vector3 strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x0695C618  token=0x6000038  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> Shake(DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single duration, UnityEngine.Vector3 strength, System.Int32 vibrato, System.Single randomness, System.Boolean ignoreZAxis, System.Boolean vectorBased, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x0695CE9C  token=0x6000039  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> ToArray(DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, UnityEngine.Vector3[] endValues, System.Single[] durations)
  RVA=0x0695D840  token=0x600003A  DG.Tweening.Core.TweenerCore<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions> To(DG.Tweening.Core.DOGetter<DG.Tweening.Color2> getter, DG.Tweening.Core.DOSetter<DG.Tweening.Color2> setter, DG.Tweening.Color2 endValue, System.Single duration)
  RVA=0x03969080  token=0x600003B  DG.Tweening.Sequence Sequence()
  RVA=0x0695C564  token=0x600003C  DG.Tweening.Sequence Sequence(System.Object target)
  RVA=0x0695B1D4  token=0x600003D  System.Int32 CompleteAll(System.Boolean withCallbacks)
  RVA=0x0695B3EC  token=0x600003E  System.Int32 Complete(System.Object targetOrId, System.Boolean withCallbacks)
  RVA=0x0695B310  token=0x600003F  System.Int32 CompleteAndReturnKilledTot()
  RVA=0x0695B2A0  token=0x6000040  System.Int32 CompleteAndReturnKilledTot(System.Object targetOrId)
  RVA=0x0695B36C  token=0x6000041  System.Int32 CompleteAndReturnKilledTot(System.Object target, System.Object id)
  RVA=0x0695B23C  token=0x6000042  System.Int32 CompleteAndReturnKilledTotExceptFor(System.Object[] excludeTargetsOrIds)
  RVA=0x0695B468  token=0x6000043  System.Int32 FlipAll()
  RVA=0x0695B4C4  token=0x6000044  System.Int32 Flip(System.Object targetOrId)
  RVA=0x0695B534  token=0x6000045  System.Int32 GotoAll(System.Single to, System.Boolean andPlay)
  RVA=0x0695B5A0  token=0x6000046  System.Int32 Goto(System.Object targetOrId, System.Single to, System.Boolean andPlay)
  RVA=0x03C12410  token=0x6000047  System.Int32 KillAll(System.Boolean complete)
  RVA=0x0695B7A8  token=0x6000048  System.Int32 KillAll(System.Boolean complete, System.Object[] idsOrTargetsToExclude)
  RVA=0x036D4610  token=0x6000049  System.Int32 Kill(System.Object targetOrId, System.Boolean complete)
  RVA=0x0695B888  token=0x600004A  System.Int32 Kill(System.Object target, System.Object id, System.Boolean complete)
  RVA=0x0695BA40  token=0x600004B  System.Int32 PauseAll()
  RVA=0x0695BA9C  token=0x600004C  System.Int32 Pause(System.Object targetOrId)
  RVA=0x0695BB70  token=0x600004D  System.Int32 PlayAll()
  RVA=0x0695BE64  token=0x600004E  System.Int32 Play(System.Object targetOrId)
  RVA=0x0695BED4  token=0x600004F  System.Int32 Play(System.Object target, System.Object id)
  RVA=0x0695BBCC  token=0x6000050  System.Int32 PlayBackwardsAll()
  RVA=0x0695BC28  token=0x6000051  System.Int32 PlayBackwards(System.Object targetOrId)
  RVA=0x0695BC98  token=0x6000052  System.Int32 PlayBackwards(System.Object target, System.Object id)
  RVA=0x0695BD18  token=0x6000053  System.Int32 PlayForwardAll()
  RVA=0x0695BD74  token=0x6000054  System.Int32 PlayForward(System.Object targetOrId)
  RVA=0x0695BDE4  token=0x6000055  System.Int32 PlayForward(System.Object target, System.Object id)
  RVA=0x0695C304  token=0x6000056  System.Int32 RestartAll(System.Boolean includeDelay)
  RVA=0x0695C400  token=0x6000057  System.Int32 Restart(System.Object targetOrId, System.Boolean includeDelay, System.Single changeDelayTo)
  RVA=0x0695C368  token=0x6000058  System.Int32 Restart(System.Object target, System.Object id, System.Boolean includeDelay, System.Single changeDelayTo)
  RVA=0x0695C484  token=0x6000059  System.Int32 RewindAll(System.Boolean includeDelay)
  RVA=0x0695C4E8  token=0x600005A  System.Int32 Rewind(System.Object targetOrId, System.Boolean includeDelay)
  RVA=0x0695CDD0  token=0x600005B  System.Int32 SmoothRewindAll()
  RVA=0x0695CE2C  token=0x600005C  System.Int32 SmoothRewind(System.Object targetOrId)
  RVA=0x0695DA04  token=0x600005D  System.Int32 TogglePauseAll()
  RVA=0x0695DA60  token=0x600005E  System.Int32 TogglePause(System.Object targetOrId)
  RVA=0x0695B730  token=0x600005F  System.Boolean IsTweening(System.Object targetOrId, System.Boolean alsoCheckIfIsPlaying)
  RVA=0x0695DB70  token=0x6000060  System.Int32 TotalActiveTweens()
  RVA=0x0695DB20  token=0x6000061  System.Int32 TotalActiveTweeners()
  RVA=0x0695DAD0  token=0x6000062  System.Int32 TotalActiveSequences()
  RVA=0x0695DBC0  token=0x6000063  System.Int32 TotalPlayingTweens()
  RVA=0x0695DC00  token=0x6000064  System.Int32 TotalTweensById(System.Object id, System.Boolean playingOnly)
  RVA=0x0695BF54  token=0x6000065  System.Collections.Generic.List<DG.Tweening.Tween> PlayingTweens(System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x0695BB0C  token=0x6000066  System.Collections.Generic.List<DG.Tweening.Tween> PausedTweens(System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x0695DC5C  token=0x6000067  System.Collections.Generic.List<DG.Tweening.Tween> TweensById(System.Object id, System.Boolean playingOnly, System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x0695DCE8  token=0x6000068  System.Collections.Generic.List<DG.Tweening.Tween> TweensByTarget(System.Object target, System.Boolean playingOnly, System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x03969420  token=0x6000069  System.Void InitCheck()
  RVA=-1  // generic def  token=0x600006A  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> ApplyTo(DG.Tweening.Core.DOGetter<T1> getter, DG.Tweening.Core.DOSetter<T1> setter, T2 endValue, System.Single duration, DG.Tweening.Plugins.Core.ABSTweenPlugin<T1,T2,TPlugOptions> plugin)
  RVA=0x041E1670  token=0x600006B  System.Void .ctor()
  RVA=0x049DE140  token=0x600006C  System.Void .cctor()
END_CLASS

CLASS: DG.Tweening.DOVirtual
TYPE:  static class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0695E8F0  token=0x6000070  DG.Tweening.Tweener Float(System.Single from, System.Single to, System.Single duration, DG.Tweening.TweenCallback<System.Single> onVirtualUpdate)
  RVA=0x0695EAA4  token=0x6000071  DG.Tweening.Tweener Int(System.Int32 from, System.Int32 to, System.Single duration, DG.Tweening.TweenCallback<System.Int32> onVirtualUpdate)
  RVA=0x0695EC44  token=0x6000072  DG.Tweening.Tweener Vector3(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single duration, DG.Tweening.TweenCallback<UnityEngine.Vector3> onVirtualUpdate)
  RVA=0x0695DEC0  token=0x6000073  DG.Tweening.Tweener Color(UnityEngine.Color from, UnityEngine.Color to, System.Single duration, DG.Tweening.TweenCallback<UnityEngine.Color> onVirtualUpdate)
  RVA=0x03154E30  token=0x6000074  System.Single EasedValue(System.Single from, System.Single to, System.Single lifetimePercentage, DG.Tweening.Ease easeType)
  RVA=0x0695E4BC  token=0x6000075  System.Single EasedValue(System.Single from, System.Single to, System.Single lifetimePercentage, DG.Tweening.Ease easeType, System.Single overshoot)
  RVA=0x0695E888  token=0x6000076  System.Single EasedValue(System.Single from, System.Single to, System.Single lifetimePercentage, DG.Tweening.Ease easeType, System.Single amplitude, System.Single period)
  RVA=0x0695E23C  token=0x6000077  System.Single EasedValue(System.Single from, System.Single to, System.Single lifetimePercentage, UnityEngine.AnimationCurve easeCurve)
  RVA=0x0695E740  token=0x6000078  UnityEngine.Vector3 EasedValue(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single lifetimePercentage, DG.Tweening.Ease easeType)
  RVA=0x0695E374  token=0x6000079  UnityEngine.Vector3 EasedValue(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single lifetimePercentage, DG.Tweening.Ease easeType, System.Single overshoot)
  RVA=0x0695E134  token=0x600007A  UnityEngine.Vector3 EasedValue(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single lifetimePercentage, DG.Tweening.Ease easeType, System.Single amplitude, System.Single period)
  RVA=0x0695E56C  token=0x600007B  UnityEngine.Vector3 EasedValue(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single lifetimePercentage, UnityEngine.AnimationCurve easeCurve)
  RVA=0x0695E074  token=0x600007C  DG.Tweening.Tween DelayedCall(System.Single delay, DG.Tweening.TweenCallback callback, System.Boolean ignoreTimeScale)
END_CLASS

CLASS: DG.Tweening.Ease
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Ease                Unset  // const
  public    static  DG.Tweening.Ease                Linear  // const
  public    static  DG.Tweening.Ease                InSine  // const
  public    static  DG.Tweening.Ease                OutSine  // const
  public    static  DG.Tweening.Ease                InOutSine  // const
  public    static  DG.Tweening.Ease                InQuad  // const
  public    static  DG.Tweening.Ease                OutQuad  // const
  public    static  DG.Tweening.Ease                InOutQuad  // const
  public    static  DG.Tweening.Ease                InCubic  // const
  public    static  DG.Tweening.Ease                OutCubic  // const
  public    static  DG.Tweening.Ease                InOutCubic  // const
  public    static  DG.Tweening.Ease                InQuart  // const
  public    static  DG.Tweening.Ease                OutQuart  // const
  public    static  DG.Tweening.Ease                InOutQuart  // const
  public    static  DG.Tweening.Ease                InQuint  // const
  public    static  DG.Tweening.Ease                OutQuint  // const
  public    static  DG.Tweening.Ease                InOutQuint  // const
  public    static  DG.Tweening.Ease                InExpo  // const
  public    static  DG.Tweening.Ease                OutExpo  // const
  public    static  DG.Tweening.Ease                InOutExpo  // const
  public    static  DG.Tweening.Ease                InCirc  // const
  public    static  DG.Tweening.Ease                OutCirc  // const
  public    static  DG.Tweening.Ease                InOutCirc  // const
  public    static  DG.Tweening.Ease                InElastic  // const
  public    static  DG.Tweening.Ease                OutElastic  // const
  public    static  DG.Tweening.Ease                InOutElastic  // const
  public    static  DG.Tweening.Ease                InBack  // const
  public    static  DG.Tweening.Ease                OutBack  // const
  public    static  DG.Tweening.Ease                InOutBack  // const
  public    static  DG.Tweening.Ease                InBounce  // const
  public    static  DG.Tweening.Ease                OutBounce  // const
  public    static  DG.Tweening.Ease                InOutBounce  // const
  public    static  DG.Tweening.Ease                Flash  // const
  public    static  DG.Tweening.Ease                InFlash  // const
  public    static  DG.Tweening.Ease                OutFlash  // const
  public    static  DG.Tweening.Ease                InOutFlash  // const
  public    static  DG.Tweening.Ease                INTERNAL_Zero  // const
  public    static  DG.Tweening.Ease                INTERNAL_Custom  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.EaseFactory
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0695EECC  token=0x600008D  DG.Tweening.EaseFunction StopMotion(System.Int32 motionFps, System.Nullable<DG.Tweening.Ease> ease)
  RVA=0x0695EF68  token=0x600008E  DG.Tweening.EaseFunction StopMotion(System.Int32 motionFps, UnityEngine.AnimationCurve animCurve)
  RVA=0x0695EE08  token=0x600008F  DG.Tweening.EaseFunction StopMotion(System.Int32 motionFps, DG.Tweening.EaseFunction customEase)
  RVA=0x041E1670  token=0x6000090  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.IDOTweenInit
TYPE:  interface
TOKEN: 0x2000014
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000093  DG.Tweening.IDOTweenInit SetCapacity(System.Int32 tweenersCapacity, System.Int32 sequencesCapacity)
END_CLASS

CLASS: DG.Tweening.LinkBehaviour
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.LinkBehaviour       PauseOnDisable  // const
  public    static  DG.Tweening.LinkBehaviour       PauseOnDisablePlayOnEnable  // const
  public    static  DG.Tweening.LinkBehaviour       PauseOnDisableRestartOnEnable  // const
  public    static  DG.Tweening.LinkBehaviour       PlayOnEnable  // const
  public    static  DG.Tweening.LinkBehaviour       RestartOnEnable  // const
  public    static  DG.Tweening.LinkBehaviour       KillOnDisable  // const
  public    static  DG.Tweening.LinkBehaviour       KillOnDestroy  // const
  public    static  DG.Tweening.LinkBehaviour       CompleteOnDisable  // const
  public    static  DG.Tweening.LinkBehaviour       CompleteAndKillOnDisable  // const
  public    static  DG.Tweening.LinkBehaviour       RewindOnDisable  // const
  public    static  DG.Tweening.LinkBehaviour       RewindAndKillOnDisable  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.PathMode
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.PathMode            Ignore  // const
  public    static  DG.Tweening.PathMode            Full3D  // const
  public    static  DG.Tweening.PathMode            TopDown2D  // const
  public    static  DG.Tweening.PathMode            Sidescroller2D  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.PathType
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.PathType            Linear  // const
  public    static  DG.Tweening.PathType            CatmullRom  // const
  public    static  DG.Tweening.PathType            CubicBezier  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.RotateMode
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.RotateMode          Fast  // const
  public    static  DG.Tweening.RotateMode          FastBeyond360  // const
  public    static  DG.Tweening.RotateMode          WorldAxisAdd  // const
  public    static  DG.Tweening.RotateMode          LocalAxisAdd  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.ScrambleMode
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.ScrambleMode        None  // const
  public    static  DG.Tweening.ScrambleMode        All  // const
  public    static  DG.Tweening.ScrambleMode        Uppercase  // const
  public    static  DG.Tweening.ScrambleMode        Lowercase  // const
  public    static  DG.Tweening.ScrambleMode        Numerals  // const
  public    static  DG.Tweening.ScrambleMode        Custom  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.ShakeRandomnessMode
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.ShakeRandomnessMode Full  // const
  public    static  DG.Tweening.ShakeRandomnessMode Harmonic  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.TweenExtensions
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x069680D4  token=0x6000094  System.Void Complete(DG.Tweening.Tween t)
  RVA=0x069680E0  token=0x6000095  System.Void Complete(DG.Tweening.Tween t, System.Boolean withCallbacks)
  RVA=0x06968478  token=0x6000096  System.Void Flip(DG.Tweening.Tween t)
  RVA=0x0696851C  token=0x6000097  System.Void ForceInit(DG.Tweening.Tween t)
  RVA=0x06968848  token=0x6000098  System.Void Goto(DG.Tweening.Tween t, System.Single to, System.Boolean andPlay)
  RVA=0x0696882C  token=0x6000099  System.Void GotoWithCallbacks(DG.Tweening.Tween t, System.Single to, System.Boolean andPlay)
  RVA=0x03D307A0  token=0x600009A  System.Void DoGoto(DG.Tweening.Tween t, System.Single to, System.Boolean andPlay, System.Boolean withCallbacks)
  RVA=0x039698F0  token=0x600009B  System.Void Kill(DG.Tweening.Tween t, System.Boolean complete)
  RVA=0x06968924  token=0x600009C  System.Void ManualUpdate(DG.Tweening.Tween t, System.Single deltaTime, System.Single unscaledDeltaTime)
  RVA=-1  // generic def  token=0x600009D  T Pause(T t)
  RVA=-1  // generic def  token=0x600009E  T Play(T t)
  RVA=0x06968D60  token=0x600009F  System.Void PlayBackwards(DG.Tweening.Tween t)
  RVA=0x06968E04  token=0x60000A0  System.Void PlayForward(DG.Tweening.Tween t)
  RVA=0x03542AF0  token=0x60000A1  System.Void Restart(DG.Tweening.Tween t, System.Boolean includeDelay, System.Single changeDelayTo)
  RVA=0x06968EA8  token=0x60000A2  System.Void Rewind(DG.Tweening.Tween t, System.Boolean includeDelay)
  RVA=0x06968F54  token=0x60000A3  System.Void SmoothRewind(DG.Tweening.Tween t)
  RVA=0x06968FF8  token=0x60000A4  System.Void TogglePause(DG.Tweening.Tween t)
  RVA=0x069685C4  token=0x60000A5  System.Void GotoWaypoint(DG.Tweening.Tween t, System.Int32 waypointIndex, System.Boolean andPlay)
  RVA=0x0696909C  token=0x60000A6  UnityEngine.YieldInstruction WaitForCompletion(DG.Tweening.Tween t)
  RVA=0x0696933C  token=0x60000A7  UnityEngine.YieldInstruction WaitForRewind(DG.Tweening.Tween t)
  RVA=0x069691EC  token=0x60000A8  UnityEngine.YieldInstruction WaitForKill(DG.Tweening.Tween t)
  RVA=0x0696913C  token=0x60000A9  UnityEngine.YieldInstruction WaitForElapsedLoops(DG.Tweening.Tween t, System.Int32 elapsedLoops)
  RVA=0x0696928C  token=0x60000AA  UnityEngine.YieldInstruction WaitForPosition(DG.Tweening.Tween t, System.Single position)
  RVA=0x069693DC  token=0x60000AB  UnityEngine.Coroutine WaitForStart(DG.Tweening.Tween t)
  RVA=0x06968190  token=0x60000AC  System.Int32 CompletedLoops(DG.Tweening.Tween t)
  RVA=0x069681D0  token=0x60000AD  System.Single Delay(DG.Tweening.Tween t)
  RVA=0x06968284  token=0x60000AE  System.Single ElapsedDelay(DG.Tweening.Tween t)
  RVA=0x06968214  token=0x60000AF  System.Single Duration(DG.Tweening.Tween t, System.Boolean includeLoops)
  RVA=0x06968408  token=0x60000B0  System.Single Elapsed(DG.Tweening.Tween t, System.Boolean includeLoops)
  RVA=0x06968368  token=0x60000B1  System.Single ElapsedPercentage(DG.Tweening.Tween t, System.Boolean includeLoops)
  RVA=0x069682C8  token=0x60000B2  System.Single ElapsedDirectionalPercentage(DG.Tweening.Tween t)
  RVA=0x03405760  token=0x60000B3  System.Boolean IsActive(DG.Tweening.Tween t)
  RVA=0x06968864  token=0x60000B4  System.Boolean IsBackwards(DG.Tweening.Tween t)
  RVA=0x03405770  token=0x60000B5  System.Boolean IsComplete(DG.Tweening.Tween t)
  RVA=0x069688A4  token=0x60000B6  System.Boolean IsInitialized(DG.Tweening.Tween t)
  RVA=0x035432E0  token=0x60000B7  System.Boolean IsPlaying(DG.Tweening.Tween t)
  RVA=0x069688E4  token=0x60000B8  System.Int32 Loops(DG.Tweening.Tween t)
  RVA=0x06968AF4  token=0x60000B9  UnityEngine.Vector3 PathGetPoint(DG.Tweening.Tween t, System.Single pathPercentage)
  RVA=0x069689E4  token=0x60000BA  UnityEngine.Vector3[] PathGetDrawPoints(DG.Tweening.Tween t, System.Int32 subdivisionsXSegment)
  RVA=0x06968C58  token=0x60000BB  System.Single PathLength(DG.Tweening.Tween t)
END_CLASS

CLASS: DG.Tweening.LoopType
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.LoopType            Restart  // const
  public    static  DG.Tweening.LoopType            Yoyo  // const
  public    static  DG.Tweening.LoopType            Incremental  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Sequence
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x138
EXTENDS: DG.Tweening.Tween
FIELDS:
  private   readonly System.Collections.Generic.List<DG.Tweening.Tween>sequencedTweens  // 0x120
  private   readonly System.Collections.Generic.List<DG.Tweening.Core.ABSSequentiable>_sequencedObjs  // 0x128
  private           System.Single                   lastTweenInsertTime  // 0x130
METHODS:
  RVA=0x039694B0  token=0x60000BC  System.Void .ctor()
  RVA=0x0695F1C0  token=0x60000BD  DG.Tweening.Sequence DoPrepend(DG.Tweening.Sequence inSequence, DG.Tweening.Tween t)
  RVA=0x039696A0  token=0x60000BE  DG.Tweening.Sequence DoInsert(DG.Tweening.Sequence inSequence, DG.Tweening.Tween t, System.Single atPosition)
  RVA=0x0396A870  token=0x60000BF  DG.Tweening.Sequence DoAppendInterval(DG.Tweening.Sequence inSequence, System.Single interval)
  RVA=0x0695F0E8  token=0x60000C0  DG.Tweening.Sequence DoPrependInterval(DG.Tweening.Sequence inSequence, System.Single interval)
  RVA=0x045E3440  token=0x60000C1  DG.Tweening.Sequence DoInsertCallback(DG.Tweening.Sequence inSequence, DG.Tweening.TweenCallback callback, System.Single atPosition)
  RVA=0x0695F2FC  token=0x60000C2  System.Single UpdateDelay(System.Single elapsed)
  RVA=0x03C7B430  token=0x60000C3  System.Void Reset()
  RVA=0x0695F32C  token=0x60000C4  System.Boolean Validate()
  RVA=0x03D308C0  token=0x60000C5  System.Boolean Startup()
  RVA=0x03D30AE0  token=0x60000C6  System.Boolean ApplyTween(System.Single prevPosition, System.Int32 prevCompletedLoops, System.Int32 newCompletedSteps, System.Boolean useInversePosition, DG.Tweening.Core.Enums.UpdateMode updateMode, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x03969580  token=0x60000C7  System.Void Setup(DG.Tweening.Sequence s)
  RVA=0x03D308D0  token=0x60000C8  System.Boolean DoStartup(DG.Tweening.Sequence s)
  RVA=0x03D30B00  token=0x60000C9  System.Boolean DoApplyTween(DG.Tweening.Sequence s, System.Single prevPosition, System.Int32 prevCompletedLoops, System.Int32 newCompletedSteps, System.Boolean useInversePosition, DG.Tweening.Core.Enums.UpdateMode updateMode)
  RVA=0x03D30E10  token=0x60000CA  System.Boolean ApplyInternalCycle(DG.Tweening.Sequence s, System.Single fromPos, System.Single toPos, DG.Tweening.Core.Enums.UpdateMode updateMode, System.Boolean useInverse, System.Boolean prevPosIsInverse, System.Boolean multiCycleStep)
  RVA=0x03D309C0  token=0x60000CB  System.Void StableSortSequencedObjs(System.Collections.Generic.List<DG.Tweening.Core.ABSSequentiable> list)
  RVA=0x03D30A80  token=0x60000CC  System.Boolean IsAnyCallbackSet(DG.Tweening.Sequence s)
END_CLASS

CLASS: DG.Tweening.ShortcutExtensions
TYPE:  static class
TOKEN: 0x200001E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0695F3D0  token=0x60000CD  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOAspect(UnityEngine.Camera target, System.Single endValue, System.Single duration)
  RVA=0x06960D74  token=0x60000CE  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOColor(UnityEngine.Camera target, UnityEngine.Color endValue, System.Single duration)
  RVA=0x0696163C  token=0x60000CF  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOFarClipPlane(UnityEngine.Camera target, System.Single endValue, System.Single duration)
  RVA=0x06961798  token=0x60000D0  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOFieldOfView(UnityEngine.Camera target, System.Single endValue, System.Single duration)
  RVA=0x06963C30  token=0x60000D1  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DONearClipPlane(UnityEngine.Camera target, System.Single endValue, System.Single duration)
  RVA=0x0696409C  token=0x60000D2  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOOrthoSize(UnityEngine.Camera target, System.Single endValue, System.Single duration)
  RVA=0x06964638  token=0x60000D3  DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> DOPixelRect(UnityEngine.Camera target, UnityEngine.Rect endValue, System.Single duration)
  RVA=0x06964F10  token=0x60000D4  DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> DORect(UnityEngine.Camera target, UnityEngine.Rect endValue, System.Single duration)
  RVA=0x06966190  token=0x60000D5  DG.Tweening.Tweener DOShakePosition(UnityEngine.Camera target, System.Single duration, System.Single strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x069665A4  token=0x60000D6  DG.Tweening.Tweener DOShakePosition(UnityEngine.Camera target, System.Single duration, UnityEngine.Vector3 strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x06966DE0  token=0x60000D7  DG.Tweening.Tweener DOShakeRotation(UnityEngine.Camera target, System.Single duration, System.Single strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x069667B8  token=0x60000D8  DG.Tweening.Tweener DOShakeRotation(UnityEngine.Camera target, System.Single duration, UnityEngine.Vector3 strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x06960C0C  token=0x60000D9  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOColor(UnityEngine.Light target, UnityEngine.Color endValue, System.Single duration)
  RVA=0x06961D94  token=0x60000DA  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOIntensity(UnityEngine.Light target, System.Single endValue, System.Single duration)
  RVA=0x06965E10  token=0x60000DB  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOShadowStrength(UnityEngine.Light target, System.Single endValue, System.Single duration)
  RVA=0x06960780  token=0x60000DC  DG.Tweening.Tweener DOColor(UnityEngine.LineRenderer target, DG.Tweening.Color2 startValue, DG.Tweening.Color2 endValue, System.Single duration)
  RVA=0x069608FC  token=0x60000DD  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOColor(UnityEngine.Material target, UnityEngine.Color endValue, System.Single duration)
  RVA=0x06960EDC  token=0x60000DE  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOColor(UnityEngine.Material target, UnityEngine.Color endValue, System.String property, System.Single duration)
  RVA=0x06960A64  token=0x60000DF  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOColor(UnityEngine.Material target, UnityEngine.Color endValue, System.Int32 propertyID, System.Single duration)
  RVA=0x06961198  token=0x60000E0  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOFade(UnityEngine.Material target, System.Single endValue, System.Single duration)
  RVA=0x06961490  token=0x60000E1  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOFade(UnityEngine.Material target, System.Single endValue, System.String property, System.Single duration)
  RVA=0x069612F4  token=0x60000E2  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DOFade(UnityEngine.Material target, System.Single endValue, System.Int32 propertyID, System.Single duration)
  RVA=0x06961B28  token=0x60000E3  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOFloat(UnityEngine.Material target, System.Single endValue, System.String property, System.Single duration)
  RVA=0x0696198C  token=0x60000E4  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOFloat(UnityEngine.Material target, System.Single endValue, System.Int32 propertyID, System.Single duration)
  RVA=0x06963D8C  token=0x60000E5  DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> DOOffset(UnityEngine.Material target, UnityEngine.Vector2 endValue, System.Single duration)
  RVA=0x06963EEC  token=0x60000E6  DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> DOOffset(UnityEngine.Material target, UnityEngine.Vector2 endValue, System.String property, System.Single duration)
  RVA=0x069674D8  token=0x60000E7  DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> DOTiling(UnityEngine.Material target, UnityEngine.Vector2 endValue, System.Single duration)
  RVA=0x06967638  token=0x60000E8  DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> DOTiling(UnityEngine.Material target, UnityEngine.Vector2 endValue, System.String property, System.Single duration)
  RVA=0x06967CE0  token=0x60000E9  DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> DOVector(UnityEngine.Material target, UnityEngine.Vector4 endValue, System.String property, System.Single duration)
  RVA=0x06967B38  token=0x60000EA  DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> DOVector(UnityEngine.Material target, UnityEngine.Vector4 endValue, System.Int32 propertyID, System.Single duration)
  RVA=0x06965078  token=0x60000EB  DG.Tweening.Tweener DOResize(UnityEngine.TrailRenderer target, System.Single toStartWidth, System.Single toEndWidth, System.Single duration)
  RVA=0x06967944  token=0x60000EC  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOTime(UnityEngine.TrailRenderer target, System.Single endValue, System.Single duration)
  RVA=0x06963AA0  token=0x60000ED  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOMove(UnityEngine.Transform target, UnityEngine.Vector3 endValue, System.Single duration, System.Boolean snapping)
  RVA=0x069635B8  token=0x60000EE  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOMoveX(UnityEngine.Transform target, System.Single endValue, System.Single duration, System.Boolean snapping)
  RVA=0x06963758  token=0x60000EF  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOMoveY(UnityEngine.Transform target, System.Single endValue, System.Single duration, System.Boolean snapping)
  RVA=0x069638FC  token=0x60000F0  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOMoveZ(UnityEngine.Transform target, System.Single endValue, System.Single duration, System.Boolean snapping)
  RVA=0x06962E94  token=0x60000F1  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOLocalMove(UnityEngine.Transform target, UnityEngine.Vector3 endValue, System.Single duration, System.Boolean snapping)
  RVA=0x069629AC  token=0x60000F2  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOLocalMoveX(UnityEngine.Transform target, System.Single endValue, System.Single duration, System.Boolean snapping)
  RVA=0x06962B4C  token=0x60000F3  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOLocalMoveY(UnityEngine.Transform target, System.Single endValue, System.Single duration, System.Boolean snapping)
  RVA=0x06962CF0  token=0x60000F4  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOLocalMoveZ(UnityEngine.Transform target, System.Single endValue, System.Single duration, System.Boolean snapping)
  RVA=0x069654D8  token=0x60000F5  DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> DORotate(UnityEngine.Transform target, UnityEngine.Vector3 endValue, System.Single duration, DG.Tweening.RotateMode mode)
  RVA=0x0696534C  token=0x60000F6  DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> DORotateQuaternion(UnityEngine.Transform target, UnityEngine.Quaternion endValue, System.Single duration)
  RVA=0x069633DC  token=0x60000F7  DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> DOLocalRotate(UnityEngine.Transform target, UnityEngine.Vector3 endValue, System.Single duration, DG.Tweening.RotateMode mode)
  RVA=0x04860390  token=0x60000F8  DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> DOLocalRotateQuaternion(UnityEngine.Transform target, UnityEngine.Quaternion endValue, System.Single duration)
  RVA=0x06965CA0  token=0x60000F9  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOScale(UnityEngine.Transform target, UnityEngine.Vector3 endValue, System.Single duration)
  RVA=0x06965B30  token=0x60000FA  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOScale(UnityEngine.Transform target, System.Single endValue, System.Single duration)
  RVA=0x06965668  token=0x60000FB  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOScaleX(UnityEngine.Transform target, System.Single endValue, System.Single duration)
  RVA=0x06965800  token=0x60000FC  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOScaleY(UnityEngine.Transform target, System.Single endValue, System.Single duration)
  RVA=0x06965998  token=0x60000FD  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DOScaleZ(UnityEngine.Transform target, System.Single endValue, System.Single duration)
  RVA=0x0696356C  token=0x60000FE  DG.Tweening.Tweener DOLookAt(UnityEngine.Transform target, UnityEngine.Vector3 towards, System.Single duration, DG.Tweening.AxisConstraint axisConstraint, System.Nullable<UnityEngine.Vector3> up)
  RVA=0x0696114C  token=0x60000FF  DG.Tweening.Tweener DODynamicLookAt(UnityEngine.Transform target, UnityEngine.Vector3 towards, System.Single duration, DG.Tweening.AxisConstraint axisConstraint, System.Nullable<UnityEngine.Vector3> up)
  RVA=0x06967E98  token=0x6000100  DG.Tweening.Tweener LookAt(UnityEngine.Transform target, UnityEngine.Vector3 towards, System.Single duration, DG.Tweening.AxisConstraint axisConstraint, System.Nullable<UnityEngine.Vector3> up, System.Boolean dynamic)
  RVA=0x06964968  token=0x6000101  DG.Tweening.Tweener DOPunchPosition(UnityEngine.Transform target, UnityEngine.Vector3 punch, System.Single duration, System.Int32 vibrato, System.Single elasticity, System.Boolean snapping)
  RVA=0x06964D34  token=0x6000102  DG.Tweening.Tweener DOPunchScale(UnityEngine.Transform target, UnityEngine.Vector3 punch, System.Single duration, System.Int32 vibrato, System.Single elasticity)
  RVA=0x06964B58  token=0x6000103  DG.Tweening.Tweener DOPunchRotation(UnityEngine.Transform target, UnityEngine.Vector3 punch, System.Single duration, System.Int32 vibrato, System.Single elasticity)
  RVA=0x06966390  token=0x6000104  DG.Tweening.Tweener DOShakePosition(UnityEngine.Transform target, System.Single duration, System.Single strength, System.Int32 vibrato, System.Single randomness, System.Boolean snapping, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x06965F6C  token=0x6000105  DG.Tweening.Tweener DOShakePosition(UnityEngine.Transform target, System.Single duration, UnityEngine.Vector3 strength, System.Int32 vibrato, System.Single randomness, System.Boolean snapping, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x069669CC  token=0x6000106  DG.Tweening.Tweener DOShakeRotation(UnityEngine.Transform target, System.Single duration, System.Single strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x06966BCC  token=0x6000107  DG.Tweening.Tweener DOShakeRotation(UnityEngine.Transform target, System.Single duration, UnityEngine.Vector3 strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x069671F4  token=0x6000108  DG.Tweening.Tweener DOShakeScale(UnityEngine.Transform target, System.Single duration, System.Single strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x06966FE0  token=0x6000109  DG.Tweening.Tweener DOShakeScale(UnityEngine.Transform target, System.Single duration, UnityEngine.Vector3 strength, System.Int32 vibrato, System.Single randomness, System.Boolean fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode)
  RVA=0x06961EF0  token=0x600010A  DG.Tweening.Sequence DOJump(UnityEngine.Transform target, UnityEngine.Vector3 endValue, System.Single jumpPower, System.Int32 numJumps, System.Single duration, System.Boolean snapping)
  RVA=0x0696247C  token=0x600010B  DG.Tweening.Sequence DOLocalJump(UnityEngine.Transform target, UnityEngine.Vector3 endValue, System.Single jumpPower, System.Int32 numJumps, System.Single duration, System.Boolean snapping)
  RVA=0x0696438C  token=0x600010C  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> DOPath(UnityEngine.Transform target, UnityEngine.Vector3[] path, System.Single duration, DG.Tweening.PathType pathType, DG.Tweening.PathMode pathMode, System.Int32 resolution, System.Nullable<UnityEngine.Color> gizmoColor)
  RVA=0x06963024  token=0x600010D  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> DOLocalPath(UnityEngine.Transform target, UnityEngine.Vector3[] path, System.Single duration, DG.Tweening.PathType pathType, DG.Tweening.PathMode pathMode, System.Int32 resolution, System.Nullable<UnityEngine.Color> gizmoColor)
  RVA=0x069641F8  token=0x600010E  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> DOPath(UnityEngine.Transform target, DG.Tweening.Plugins.Core.PathCore.Path path, System.Single duration, DG.Tweening.PathMode pathMode)
  RVA=0x06963240  token=0x600010F  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> DOLocalPath(UnityEngine.Transform target, DG.Tweening.Plugins.Core.PathCore.Path path, System.Single duration, DG.Tweening.PathMode pathMode)
  RVA=0x069677E8  token=0x6000110  DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DOTimeScale(DG.Tweening.Tween target, System.Single endValue, System.Single duration)
  RVA=0x0695F73C  token=0x6000111  DG.Tweening.Tweener DOBlendableColor(UnityEngine.Light target, UnityEngine.Color endValue, System.Single duration)
  RVA=0x0695FB28  token=0x6000112  DG.Tweening.Tweener DOBlendableColor(UnityEngine.Material target, UnityEngine.Color endValue, System.Single duration)
  RVA=0x0695F908  token=0x6000113  DG.Tweening.Tweener DOBlendableColor(UnityEngine.Material target, UnityEngine.Color endValue, System.String property, System.Single duration)
  RVA=0x0695F52C  token=0x6000114  DG.Tweening.Tweener DOBlendableColor(UnityEngine.Material target, UnityEngine.Color endValue, System.Int32 propertyID, System.Single duration)
  RVA=0x06960064  token=0x6000115  DG.Tweening.Tweener DOBlendableMoveBy(UnityEngine.Transform target, UnityEngine.Vector3 byValue, System.Single duration, System.Boolean snapping)
  RVA=0x0695FCF4  token=0x6000116  DG.Tweening.Tweener DOBlendableLocalMoveBy(UnityEngine.Transform target, UnityEngine.Vector3 byValue, System.Single duration, System.Boolean snapping)
  RVA=0x0696042C  token=0x6000117  DG.Tweening.Tweener DOBlendableRotateBy(UnityEngine.Transform target, UnityEngine.Vector3 byValue, System.Single duration, DG.Tweening.RotateMode mode)
  RVA=0x0695FEB0  token=0x6000118  DG.Tweening.Tweener DOBlendableLocalRotateBy(UnityEngine.Transform target, UnityEngine.Vector3 byValue, System.Single duration, DG.Tweening.RotateMode mode)
  RVA=0x06960220  token=0x6000119  DG.Tweening.Tweener DOBlendablePunchRotation(UnityEngine.Transform target, UnityEngine.Vector3 punch, System.Single duration, System.Int32 vibrato, System.Single elasticity)
  RVA=0x069605E0  token=0x600011A  DG.Tweening.Tweener DOBlendableScaleBy(UnityEngine.Transform target, UnityEngine.Vector3 byValue, System.Single duration)
  RVA=0x069610F0  token=0x600011B  System.Int32 DOComplete(UnityEngine.Component target, System.Boolean withCallbacks)
  RVA=0x06961094  token=0x600011C  System.Int32 DOComplete(UnityEngine.Material target, System.Boolean withCallbacks)
  RVA=0x036D45D0  token=0x600011D  System.Int32 DOKill(UnityEngine.Component target, System.Boolean complete)
  RVA=0x06962420  token=0x600011E  System.Int32 DOKill(UnityEngine.Material target, System.Boolean complete)
  RVA=0x069618F4  token=0x600011F  System.Int32 DOFlip(UnityEngine.Component target)
  RVA=0x06961940  token=0x6000120  System.Int32 DOFlip(UnityEngine.Material target)
  RVA=0x06961D34  token=0x6000121  System.Int32 DOGoto(UnityEngine.Component target, System.Single to, System.Boolean andPlay)
  RVA=0x06961CD4  token=0x6000122  System.Int32 DOGoto(UnityEngine.Material target, System.Single to, System.Boolean andPlay)
  RVA=0x069645A0  token=0x6000123  System.Int32 DOPause(UnityEngine.Component target)
  RVA=0x069645EC  token=0x6000124  System.Int32 DOPause(UnityEngine.Material target)
  RVA=0x0696491C  token=0x6000125  System.Int32 DOPlay(UnityEngine.Component target)
  RVA=0x069648D0  token=0x6000126  System.Int32 DOPlay(UnityEngine.Material target)
  RVA=0x069647A0  token=0x6000127  System.Int32 DOPlayBackwards(UnityEngine.Component target)
  RVA=0x069647EC  token=0x6000128  System.Int32 DOPlayBackwards(UnityEngine.Material target)
  RVA=0x06964884  token=0x6000129  System.Int32 DOPlayForward(UnityEngine.Component target)
  RVA=0x06964838  token=0x600012A  System.Int32 DOPlayForward(UnityEngine.Material target)
  RVA=0x069651E4  token=0x600012B  System.Int32 DORestart(UnityEngine.Component target, System.Boolean includeDelay)
  RVA=0x0696523C  token=0x600012C  System.Int32 DORestart(UnityEngine.Material target, System.Boolean includeDelay)
  RVA=0x069652F0  token=0x600012D  System.Int32 DORewind(UnityEngine.Component target, System.Boolean includeDelay)
  RVA=0x06965294  token=0x600012E  System.Int32 DORewind(UnityEngine.Material target, System.Boolean includeDelay)
  RVA=0x06967440  token=0x600012F  System.Int32 DOSmoothRewind(UnityEngine.Component target)
  RVA=0x0696748C  token=0x6000130  System.Int32 DOSmoothRewind(UnityEngine.Material target)
  RVA=0x06967AA0  token=0x6000131  System.Int32 DOTogglePause(UnityEngine.Component target)
  RVA=0x06967AEC  token=0x6000132  System.Int32 DOTogglePause(UnityEngine.Material target)
END_CLASS

CLASS: DG.Tweening.TweenParams
TYPE:  class
TOKEN: 0x200006B
SIZE:  0xA0
FIELDS:
  public    static readonly DG.Tweening.TweenParams         Params  // static @ 0x0
  private           System.Object                   id  // 0x10
  private           System.String                   stringId  // 0x18
  private           System.Int32                    intId  // 0x20
  private           System.Object                   target  // 0x28
  private           DG.Tweening.UpdateType          updateType  // 0x30
  private           System.Boolean                  isIndependentUpdate  // 0x34
  private           DG.Tweening.TweenCallback       onStart  // 0x38
  private           DG.Tweening.TweenCallback       onPlay  // 0x40
  private           DG.Tweening.TweenCallback       onRewind  // 0x48
  private           DG.Tweening.TweenCallback       onUpdate  // 0x50
  private           DG.Tweening.TweenCallback       onStepComplete  // 0x58
  private           DG.Tweening.TweenCallback       onComplete  // 0x60
  private           DG.Tweening.TweenCallback       onKill  // 0x68
  private           DG.Tweening.TweenCallback<System.Int32>onWaypointChange  // 0x70
  private           System.Boolean                  isRecyclable  // 0x78
  private           System.Boolean                  isSpeedBased  // 0x79
  private           System.Boolean                  autoKill  // 0x7a
  private           System.Int32                    loops  // 0x7c
  private           DG.Tweening.LoopType            loopType  // 0x80
  private           System.Single                   delay  // 0x84
  private           System.Boolean                  isRelative  // 0x88
  private           DG.Tweening.Ease                easeType  // 0x8c
  private           DG.Tweening.EaseFunction        customEase  // 0x90
  private           System.Single                   easeOvershootOrAmplitude  // 0x98
  private           System.Single                   easePeriod  // 0x9c
METHODS:
  RVA=0x06969AA0  token=0x6000223  System.Void .ctor()
  RVA=0x0696947C  token=0x6000224  DG.Tweening.TweenParams Clear()
  RVA=0x04D8C820  token=0x6000225  DG.Tweening.TweenParams SetAutoKill(System.Boolean autoKillOnCompletion)
  RVA=0x069698F4  token=0x6000226  DG.Tweening.TweenParams SetId(System.Object objectId)
  RVA=0x06969910  token=0x6000227  DG.Tweening.TweenParams SetId(System.String stringId)
  RVA=0x04D8C840  token=0x6000228  DG.Tweening.TweenParams SetId(System.Int32 intId)
  RVA=0x069699B4  token=0x6000229  DG.Tweening.TweenParams SetTarget(System.Object target)
  RVA=0x0696992C  token=0x600022A  DG.Tweening.TweenParams SetLoops(System.Int32 loops, System.Nullable<DG.Tweening.LoopType> loopType)
  RVA=0x069697CC  token=0x600022B  DG.Tweening.TweenParams SetEase(DG.Tweening.Ease ease, System.Nullable<System.Single> overshootOrAmplitude, System.Nullable<System.Single> period)
  RVA=0x06969700  token=0x600022C  DG.Tweening.TweenParams SetEase(UnityEngine.AnimationCurve animCurve)
  RVA=0x069698C8  token=0x600022D  DG.Tweening.TweenParams SetEase(DG.Tweening.EaseFunction customEase)
  RVA=0x04D8C850  token=0x600022E  DG.Tweening.TweenParams SetRecyclable(System.Boolean recyclable)
  RVA=0x069699D0  token=0x600022F  DG.Tweening.TweenParams SetUpdate(System.Boolean isIndependentUpdate)
  RVA=0x04D8C880  token=0x6000230  DG.Tweening.TweenParams SetUpdate(DG.Tweening.UpdateType updateType, System.Boolean isIndependentUpdate)
  RVA=0x06969690  token=0x6000231  DG.Tweening.TweenParams OnStart(DG.Tweening.TweenCallback action)
  RVA=0x06969658  token=0x6000232  DG.Tweening.TweenParams OnPlay(DG.Tweening.TweenCallback action)
  RVA=0x06969674  token=0x6000233  DG.Tweening.TweenParams OnRewind(DG.Tweening.TweenCallback action)
  RVA=0x069696C8  token=0x6000234  DG.Tweening.TweenParams OnUpdate(DG.Tweening.TweenCallback action)
  RVA=0x069696AC  token=0x6000235  DG.Tweening.TweenParams OnStepComplete(DG.Tweening.TweenCallback action)
  RVA=0x06969620  token=0x6000236  DG.Tweening.TweenParams OnComplete(DG.Tweening.TweenCallback action)
  RVA=0x0696963C  token=0x6000237  DG.Tweening.TweenParams OnKill(DG.Tweening.TweenCallback action)
  RVA=0x069696E4  token=0x6000238  DG.Tweening.TweenParams OnWaypointChange(DG.Tweening.TweenCallback<System.Int32> action)
  RVA=0x04D8C830  token=0x6000239  DG.Tweening.TweenParams SetDelay(System.Single delay)
  RVA=0x04D8C860  token=0x600023A  DG.Tweening.TweenParams SetRelative(System.Boolean isRelative)
  RVA=0x04D8C870  token=0x600023B  DG.Tweening.TweenParams SetSpeedBased(System.Boolean isSpeedBased)
  RVA=0x06969A30  token=0x600023C  System.Void .cctor()
END_CLASS

CLASS: DG.Tweening.TweenSettingsExtensions
TYPE:  static class
TOKEN: 0x200006C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600023D  T SetAutoKill(T t)
  RVA=-1  // generic def  token=0x600023E  T SetAutoKill(T t, System.Boolean autoKillOnCompletion)
  RVA=-1  // generic def  token=0x600023F  T SetId(T t, System.Object objectId)
  RVA=-1  // generic def  token=0x6000240  T SetId(T t, System.String stringId)
  RVA=-1  // generic def  token=0x6000241  T SetId(T t, System.Int32 intId)
  RVA=-1  // generic def  token=0x6000242  T SetLink(T t, UnityEngine.GameObject gameObject)
  RVA=-1  // generic def  token=0x6000243  T SetLink(T t, UnityEngine.GameObject gameObject, DG.Tweening.LinkBehaviour behaviour)
  RVA=-1  // generic def  token=0x6000244  T SetTarget(T t, System.Object target)
  RVA=-1  // generic def  token=0x6000245  T SetLoops(T t, System.Int32 loops)
  RVA=-1  // generic def  token=0x6000246  T SetLoops(T t, System.Int32 loops, DG.Tweening.LoopType loopType)
  RVA=-1  // generic def  token=0x6000247  T SetEase(T t, DG.Tweening.Ease ease)
  RVA=-1  // generic def  token=0x6000248  T SetEase(T t, DG.Tweening.Ease ease, System.Single overshoot)
  RVA=-1  // generic def  token=0x6000249  T SetEase(T t, DG.Tweening.Ease ease, System.Single amplitude, System.Single period)
  RVA=-1  // generic def  token=0x600024A  T SetEase(T t, UnityEngine.AnimationCurve animCurve)
  RVA=-1  // generic def  token=0x600024B  T SetEase(T t, DG.Tweening.EaseFunction customEase)
  RVA=-1  // generic def  token=0x600024C  T SetRecyclable(T t)
  RVA=-1  // generic def  token=0x600024D  T SetRecyclable(T t, System.Boolean recyclable)
  RVA=-1  // generic def  token=0x600024E  T SetUpdate(T t, System.Boolean isIndependentUpdate)
  RVA=-1  // generic def  token=0x600024F  T SetUpdate(T t, DG.Tweening.UpdateType updateType)
  RVA=-1  // generic def  token=0x6000250  T SetUpdate(T t, DG.Tweening.UpdateType updateType, System.Boolean isIndependentUpdate)
  RVA=-1  // generic def  token=0x6000251  T SetInverted(T t)
  RVA=-1  // generic def  token=0x6000252  T SetInverted(T t, System.Boolean inverted)
  RVA=-1  // generic def  token=0x6000253  T OnStart(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x6000254  T OnPlay(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x6000255  T OnPause(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x6000256  T OnRewind(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x6000257  T OnUpdate(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x6000258  T OnStepComplete(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x6000259  T OnComplete(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x600025A  T OnKill(T t, DG.Tweening.TweenCallback action)
  RVA=-1  // generic def  token=0x600025B  T OnWaypointChange(T t, DG.Tweening.TweenCallback<System.Int32> action)
  RVA=-1  // generic def  token=0x600025C  T SetAs(T t, DG.Tweening.Tween asTween)
  RVA=-1  // generic def  token=0x600025D  T SetAs(T t, DG.Tweening.TweenParams tweenParams)
  RVA=0x03969030  token=0x600025E  DG.Tweening.Sequence Append(DG.Tweening.Sequence s, DG.Tweening.Tween t)
  RVA=0x06969DE4  token=0x600025F  DG.Tweening.Sequence Prepend(DG.Tweening.Sequence s, DG.Tweening.Tween t)
  RVA=0x0396B270  token=0x6000260  DG.Tweening.Sequence Join(DG.Tweening.Sequence s, DG.Tweening.Tween t)
  RVA=0x06969D0C  token=0x6000261  DG.Tweening.Sequence Insert(DG.Tweening.Sequence s, System.Single atPosition, DG.Tweening.Tween t)
  RVA=0x0396A830  token=0x6000262  DG.Tweening.Sequence AppendInterval(DG.Tweening.Sequence s, System.Single interval)
  RVA=0x06969DA4  token=0x6000263  DG.Tweening.Sequence PrependInterval(DG.Tweening.Sequence s, System.Single interval)
  RVA=0x045E33F0  token=0x6000264  DG.Tweening.Sequence AppendCallback(DG.Tweening.Sequence s, DG.Tweening.TweenCallback callback)
  RVA=0x06969D5C  token=0x6000265  DG.Tweening.Sequence PrependCallback(DG.Tweening.Sequence s, DG.Tweening.TweenCallback callback)
  RVA=0x06969CB8  token=0x6000266  DG.Tweening.Sequence InsertCallback(DG.Tweening.Sequence s, System.Single atPosition, DG.Tweening.TweenCallback callback)
  RVA=0x0396A7D0  token=0x6000267  System.Boolean ValidateAddToSequence(DG.Tweening.Sequence s, DG.Tweening.Tween t, System.Boolean ignoreTween)
  RVA=-1  // generic def  token=0x6000268  T From(T t)
  RVA=-1  // generic def  token=0x6000269  T From(T t, System.Boolean isRelative)
  RVA=-1  // generic def  token=0x600026A  T From(T t, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=-1  // generic def  token=0x600026B  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> From(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, T2 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x06969AB0  token=0x600026C  DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> From(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t, System.Single fromAlphaValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x06969B6C  token=0x600026D  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> From(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t, System.Single fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x06969C18  token=0x600026E  DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> From(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t, System.Single fromValueDegrees, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=-1  // generic def  token=0x600026F  T SetDelay(T t, System.Single delay)
  RVA=-1  // generic def  token=0x6000270  T SetDelay(T t, System.Single delay, System.Boolean asPrependedIntervalIfSequence)
  RVA=-1  // generic def  token=0x6000271  T SetRelative(T t)
  RVA=-1  // generic def  token=0x6000272  T SetRelative(T t, System.Boolean isRelative)
  RVA=-1  // generic def  token=0x6000273  T SetSpeedBased(T t)
  RVA=-1  // generic def  token=0x6000274  T SetSpeedBased(T t, System.Boolean isSpeedBased)
  RVA=0x0696A37C  token=0x6000275  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> t, System.Boolean snapping)
  RVA=0x0396B570  token=0x6000276  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t, System.Boolean snapping)
  RVA=0x046AE4E0  token=0x6000277  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, System.Boolean snapping)
  RVA=0x0696A344  token=0x6000278  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t, System.Boolean snapping)
  RVA=0x0696A35C  token=0x6000279  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, System.Boolean snapping)
  RVA=0x0696A1E4  token=0x600027A  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t, System.Boolean snapping)
  RVA=0x0696A1C4  token=0x600027B  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, System.Boolean snapping)
  RVA=0x0696A1FC  token=0x600027C  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t, System.Boolean useShortest360Route)
  RVA=0x045E33D0  token=0x600027D  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t, System.Boolean alphaOnly)
  RVA=0x045E33D0  token=0x600027E  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> t, System.Boolean snapping)
  RVA=0x0696A21C  token=0x600027F  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> t, System.Boolean richTextEnabled, DG.Tweening.ScrambleMode scrambleMode, System.String scrambleChars)
  RVA=0x0396B570  token=0x6000280  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, System.Boolean snapping)
  RVA=0x046AE4E0  token=0x6000281  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, DG.Tweening.AxisConstraint axisConstraint, System.Boolean snapping)
  RVA=0x0696A31C  token=0x6000282  DG.Tweening.Tweener SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t, System.Single endValueDegrees, System.Boolean relativeCenter, System.Boolean snapping)
  RVA=0x0696A394  token=0x6000283  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, DG.Tweening.AxisConstraint lockPosition, DG.Tweening.AxisConstraint lockRotation)
  RVA=0x0696A2F4  token=0x6000284  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetOptions(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, System.Boolean closePath, DG.Tweening.AxisConstraint lockPosition, DG.Tweening.AxisConstraint lockRotation)
  RVA=0x06969F24  token=0x6000285  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Vector3 lookAtPosition, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up)
  RVA=0x0696A014  token=0x6000286  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Vector3 lookAtPosition, System.Boolean stableZRotation)
  RVA=0x06969E9C  token=0x6000287  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Transform lookAtTransform, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up)
  RVA=0x0696A080  token=0x6000288  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Transform lookAtTransform, System.Boolean stableZRotation)
  RVA=0x06969F94  token=0x6000289  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, System.Single lookAhead, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up)
  RVA=0x06969E20  token=0x600028A  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, System.Single lookAhead, System.Boolean stableZRotation)
  RVA=0x0696A104  token=0x600028B  DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, DG.Tweening.Plugins.Options.OrientType orientType, UnityEngine.Vector3 lookAtPosition, UnityEngine.Transform lookAtTransform, System.Single lookAhead, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up, System.Boolean stableZRotation)
  RVA=0x0696A3B4  token=0x600028C  System.Void SetPathForwardDirection(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up)
END_CLASS

CLASS: DG.Tweening.LogBehaviour
TYPE:  sealed struct
TOKEN: 0x200006D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.LogBehaviour        Default  // const
  public    static  DG.Tweening.LogBehaviour        Verbose  // const
  public    static  DG.Tweening.LogBehaviour        ErrorsOnly  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Tween
TYPE:  abstract class
TOKEN: 0x200006E
SIZE:  0x120
EXTENDS: DG.Tweening.Core.ABSSequentiable
FIELDS:
  public            System.Single                   timeScale  // 0x28
  public            System.Boolean                  isBackwards  // 0x2c
  private           System.Boolean                  isInverted  // 0x2d
  public            System.Object                   id  // 0x30
  public            System.String                   stringId  // 0x38
  public            System.Int32                    intId  // 0x40
  public            System.Object                   target  // 0x48
  private           DG.Tweening.UpdateType          updateType  // 0x50
  private           System.Boolean                  isIndependentUpdate  // 0x54
  public            DG.Tweening.TweenCallback       onPlay  // 0x58
  public            DG.Tweening.TweenCallback       onPause  // 0x60
  public            DG.Tweening.TweenCallback       onRewind  // 0x68
  public            DG.Tweening.TweenCallback       onUpdate  // 0x70
  public            DG.Tweening.TweenCallback       onStepComplete  // 0x78
  public            DG.Tweening.TweenCallback       onComplete  // 0x80
  public            DG.Tweening.TweenCallback       onKill  // 0x88
  public            DG.Tweening.TweenCallback<System.Int32>onWaypointChange  // 0x90
  private           System.Boolean                  isFrom  // 0x98
  private           System.Boolean                  isBlendable  // 0x99
  private           System.Boolean                  isRecyclable  // 0x9a
  private           System.Boolean                  isSpeedBased  // 0x9b
  private           System.Boolean                  autoKill  // 0x9c
  private           System.Single                   duration  // 0xa0
  private           System.Int32                    loops  // 0xa4
  private           DG.Tweening.LoopType            loopType  // 0xa8
  private           System.Single                   delay  // 0xac
  private           System.Boolean                  <isRelative>k__BackingField  // 0xb0
  private           DG.Tweening.Ease                easeType  // 0xb4
  private           DG.Tweening.EaseFunction        customEase  // 0xb8
  public            System.Single                   easeOvershootOrAmplitude  // 0xc0
  public            System.Single                   easePeriod  // 0xc4
  public            System.String                   debugTargetId  // 0xc8
  private           System.Type                     typeofT1  // 0xd0
  private           System.Type                     typeofT2  // 0xd8
  private           System.Type                     typeofTPlugOptions  // 0xe0
  private           System.Boolean                  <active>k__BackingField  // 0xe8
  private           System.Boolean                  isSequenced  // 0xe9
  private           DG.Tweening.Sequence            sequenceParent  // 0xf0
  private           System.Int32                    activeId  // 0xf8
  private           DG.Tweening.Core.Enums.SpecialStartupModespecialStartupMode  // 0xfc
  private           System.Boolean                  creationLocked  // 0x100
  private           System.Boolean                  startupDone  // 0x101
  private           System.Boolean                  <playedOnce>k__BackingField  // 0x102
  private           System.Single                   <position>k__BackingField  // 0x104
  private           System.Single                   fullDuration  // 0x108
  private           System.Int32                    completedLoops  // 0x10c
  private           System.Boolean                  isPlaying  // 0x110
  private           System.Boolean                  isComplete  // 0x111
  private           System.Single                   elapsedDelay  // 0x114
  private           System.Boolean                  delayComplete  // 0x118
  private           System.Int32                    miscInt  // 0x11c
PROPERTIES:
  isRelative  get=0x04D88CC0  set=0x04D88D20
  active  get=0x04D8C8C0  set=0x04D8C8F0
  fullPosition  get=0x0696A648  set=0x0696A654
  hasLoops  get=0x03153250
  playedOnce  get=0x04D8C8D0  set=0x04D8C900
  position  get=0x04D8C8E0  set=0x04D8C910
METHODS:
  RVA=0x03C7B680  token=0x6000298  System.Void Reset()
  RVA=-1  // abstract  token=0x6000299  System.Boolean Validate()
  RVA=0x04D879D0  token=0x600029A  System.Single UpdateDelay(System.Single elapsed)
  RVA=-1  // abstract  token=0x600029B  System.Boolean Startup()
  RVA=-1  // abstract  token=0x600029C  System.Boolean ApplyTween(System.Single prevPosition, System.Int32 prevCompletedLoops, System.Int32 newCompletedSteps, System.Boolean useInversePosition, DG.Tweening.Core.Enums.UpdateMode updateMode, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x03152DF0  token=0x600029D  System.Boolean DoGoto(DG.Tweening.Tween t, System.Single toPosition, System.Int32 toCompletedLoops, DG.Tweening.Core.Enums.UpdateMode updateMode)
  RVA=0x035450A0  token=0x600029E  System.Boolean OnTweenCallback(DG.Tweening.TweenCallback callback, DG.Tweening.Tween t)
  RVA=-1  // generic def  token=0x600029F  System.Boolean OnTweenCallback(DG.Tweening.TweenCallback<T> callback, DG.Tweening.Tween t, T param)
  RVA=0x04D8C890  token=0x60002A0  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Tweener
TYPE:  abstract class
TOKEN: 0x200006F
SIZE:  0x128
EXTENDS: DG.Tweening.Tween
FIELDS:
  private           System.Boolean                  hasManuallySetStartValue  // 0x120
  private           System.Boolean                  isFromAllowed  // 0x121
METHODS:
  RVA=0x04D8C920  token=0x60002A1  System.Void .ctor()
  RVA=-1  // abstract  token=0x60002A2  DG.Tweening.Tweener ChangeStartValue(System.Object newStartValue, System.Single newDuration)
  RVA=-1  // abstract  token=0x60002A3  DG.Tweening.Tweener ChangeEndValue(System.Object newEndValue, System.Single newDuration, System.Boolean snapStartValue)
  RVA=-1  // abstract  token=0x60002A4  DG.Tweening.Tweener ChangeEndValue(System.Object newEndValue, System.Boolean snapStartValue)
  RVA=-1  // abstract  token=0x60002A5  DG.Tweening.Tweener ChangeValues(System.Object newStartValue, System.Object newEndValue, System.Single newDuration)
  RVA=-1  // abstract  token=0x60002A6  DG.Tweening.Tweener SetFrom(System.Boolean relative)
  RVA=-1  // generic def  token=0x60002A7  System.Boolean Setup(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, DG.Tweening.Core.DOGetter<T1> getter, DG.Tweening.Core.DOSetter<T1> setter, T2 endValue, System.Single duration, DG.Tweening.Plugins.Core.ABSTweenPlugin<T1,T2,TPlugOptions> plugin)
  RVA=-1  // generic def  token=0x60002A8  System.Single DoUpdateDelay(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, System.Single elapsed)
  RVA=-1  // generic def  token=0x60002A9  System.Boolean DoStartup(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
  RVA=-1  // generic def  token=0x60002AA  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> DoChangeStartValue(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, T2 newStartValue, System.Single newDuration)
  RVA=-1  // generic def  token=0x60002AB  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> DoChangeEndValue(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, T2 newEndValue, System.Single newDuration, System.Boolean snapStartValue)
  RVA=-1  // generic def  token=0x60002AC  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> DoChangeValues(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, T2 newStartValue, T2 newEndValue, System.Single newDuration)
  RVA=-1  // generic def  token=0x60002AD  System.Boolean DOStartupSpecials(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
  RVA=-1  // generic def  token=0x60002AE  System.Void DOStartupDurationBased(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
END_CLASS

CLASS: DG.Tweening.TweenType
TYPE:  sealed struct
TOKEN: 0x2000070
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.TweenType           Tweener  // const
  public    static  DG.Tweening.TweenType           Sequence  // const
  public    static  DG.Tweening.TweenType           Callback  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.UpdateType
TYPE:  sealed struct
TOKEN: 0x2000071
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.UpdateType          Normal  // const
  public    static  DG.Tweening.UpdateType          Late  // const
  public    static  DG.Tweening.UpdateType          Fixed  // const
  public    static  DG.Tweening.UpdateType          Manual  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.ABSSequentiable
TYPE:  abstract class
TOKEN: 0x200009F
SIZE:  0x28
FIELDS:
  private           DG.Tweening.TweenType           tweenType  // 0x10
  private           System.Single                   sequencedPosition  // 0x14
  private           System.Single                   sequencedEndPosition  // 0x18
  private           DG.Tweening.TweenCallback       onStart  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60003B6  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Core.DOGetter`1
TYPE:  sealed class
TOKEN: 0x20000A0
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60003B7  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60003B8  T Invoke()
  RVA=-1  // runtime  token=0x60003B9  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60003BA  T EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: DG.Tweening.Core.DOSetter`1
TYPE:  sealed class
TOKEN: 0x20000A1
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60003BB  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60003BC  System.Void Invoke(T pNewValue)
  RVA=-1  // runtime  token=0x60003BD  System.IAsyncResult BeginInvoke(T pNewValue, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60003BE  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: DG.Tweening.Core.Debugger
TYPE:  static class
TOKEN: 0x20000A2
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    _logPriority  // static @ 0x0
  private   static  System.String                   _LogPrefix  // const
PROPERTIES:
  logPriority  get=0x0697ACE0
METHODS:
  RVA=0x0697AB7C  token=0x60003C0  System.Void Log(System.Object message)
  RVA=0x040BA870  token=0x60003C1  System.Void LogWarning(System.Object message, DG.Tweening.Tween t)
  RVA=0x0697A4BC  token=0x60003C2  System.Void LogError(System.Object message, DG.Tweening.Tween t)
  RVA=0x0697A8E8  token=0x60003C3  System.Void LogSafeModeCapturedError(System.Object message, DG.Tweening.Tween t)
  RVA=0x0697A808  token=0x60003C4  System.Void LogReport(System.Object message)
  RVA=0x0697AA9C  token=0x60003C5  System.Void LogSafeModeReport(System.Object message)
  RVA=0x0697A610  token=0x60003C6  System.Void LogInvalidTween(DG.Tweening.Tween t)
  RVA=0x0697A700  token=0x60003C7  System.Void LogNestedTween(DG.Tweening.Tween t)
  RVA=0x0697A778  token=0x60003C8  System.Void LogNullTween(DG.Tweening.Tween t)
  RVA=0x0697A73C  token=0x60003C9  System.Void LogNonPathTween(DG.Tweening.Tween t)
  RVA=0x0697A648  token=0x60003CA  System.Void LogMissingMaterialProperty(System.String propertyName)
  RVA=0x0697A694  token=0x60003CB  System.Void LogMissingMaterialProperty(System.Int32 propertyId)
  RVA=0x0697A7B0  token=0x60003CC  System.Void LogRemoveActiveTweenError(System.String errorInfo, DG.Tweening.Tween t)
  RVA=0x0697A464  token=0x60003CD  System.Void LogAddActiveTweenError(System.String errorInfo, DG.Tweening.Tween t)
  RVA=0x04709D40  token=0x60003CE  System.Void SetLogPriority(DG.Tweening.LogBehaviour logBehaviour)
  RVA=0x0697AC6C  token=0x60003CF  System.Boolean ShouldLogSafeModeCapturedError()
  RVA=0x0697A418  token=0x60003D0  System.String GetDebugDataMessage(DG.Tweening.Tween t)
  RVA=0x0697A250  token=0x60003D1  System.Void AddDebugDataToMessage(System.String& message, DG.Tweening.Tween t)
END_CLASS

CLASS: DG.Tweening.Core.DOTweenComponent
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: DG.Tweening.IDOTweenInit
FIELDS:
  public            System.Int32                    inspectorUpdater  // 0x18
  private           System.Single                   _unscaledTime  // 0x1c
  private           System.Single                   _unscaledDeltaTime  // 0x20
  private           System.Boolean                  _paused  // 0x24
  private           System.Single                   _pausedTime  // 0x28
  private           System.Boolean                  _isQuitting  // 0x2c
  private           System.Boolean                  _duplicateToDestroy  // 0x2d
METHODS:
  RVA=0x04961210  token=0x60003D8  System.Void Awake()
  RVA=0x04D267A0  token=0x60003D9  System.Void Start()
  RVA=0x03152600  token=0x60003DA  System.Void Update()
  RVA=0x03EFA010  token=0x60003DB  System.Void LateUpdate()
  RVA=0x03E62500  token=0x60003DC  System.Void FixedUpdate()
  RVA=0x06979D10  token=0x60003DD  System.Void OnDrawGizmos()
  RVA=0x06979914  token=0x60003DE  System.Void OnDestroy()
  RVA=0x03F262A0  token=0x60003DF  System.Void OnApplicationPause(System.Boolean pauseStatus)
  RVA=0x069798C8  token=0x60003E0  System.Void OnApplicationQuit()
  RVA=0x06979E30  token=0x60003E1  DG.Tweening.IDOTweenInit SetCapacity(System.Int32 tweenersCapacity, System.Int32 sequencesCapacity)
  RVA=0x06979E90  token=0x60003E2  System.Collections.IEnumerator WaitForCompletion(DG.Tweening.Tween t)
  RVA=0x0697A020  token=0x60003E3  System.Collections.IEnumerator WaitForRewind(DG.Tweening.Tween t)
  RVA=0x06979F58  token=0x60003E4  System.Collections.IEnumerator WaitForKill(DG.Tweening.Tween t)
  RVA=0x06979EEC  token=0x60003E5  System.Collections.IEnumerator WaitForElapsedLoops(DG.Tweening.Tween t, System.Int32 elapsedLoops)
  RVA=0x06979FB4  token=0x60003E6  System.Collections.IEnumerator WaitForPosition(DG.Tweening.Tween t, System.Single position)
  RVA=0x0697A07C  token=0x60003E7  System.Collections.IEnumerator WaitForStart(DG.Tweening.Tween t)
  RVA=0x04709D90  token=0x60003E8  System.Void Create()
  RVA=0x069797D8  token=0x60003E9  System.Void DestroyInstance()
  RVA=0x05393520  token=0x60003EA  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Core.DOTweenSettings
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x80
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public    static  System.String                   AssetName  // const
  public    static  System.String                   AssetFullFilename  // const
  public            System.Boolean                  useSafeMode  // 0x18
  public            DG.Tweening.Core.DOTweenSettings.SafeModeOptionssafeModeOptions  // 0x20
  public            System.Single                   timeScale  // 0x28
  public            System.Single                   unscaledTimeScale  // 0x2c
  public            System.Boolean                  useSmoothDeltaTime  // 0x30
  public            System.Single                   maxSmoothUnscaledTime  // 0x34
  public            DG.Tweening.Core.Enums.RewindCallbackModerewindCallbackMode  // 0x38
  public            System.Boolean                  showUnityEditorReport  // 0x3c
  public            DG.Tweening.LogBehaviour        logBehaviour  // 0x40
  public            System.Boolean                  drawGizmos  // 0x44
  public            System.Boolean                  defaultRecyclable  // 0x45
  public            DG.Tweening.AutoPlay            defaultAutoPlay  // 0x48
  public            DG.Tweening.UpdateType          defaultUpdateType  // 0x4c
  public            System.Boolean                  defaultTimeScaleIndependent  // 0x50
  public            DG.Tweening.Ease                defaultEaseType  // 0x54
  public            System.Single                   defaultEaseOvershootOrAmplitude  // 0x58
  public            System.Single                   defaultEasePeriod  // 0x5c
  public            System.Boolean                  defaultAutoKill  // 0x60
  public            DG.Tweening.LoopType            defaultLoopType  // 0x64
  public            System.Boolean                  debugMode  // 0x68
  public            System.Boolean                  debugStoreTargetId  // 0x69
  public            System.Boolean                  showPreviewPanel  // 0x6a
  public            DG.Tweening.Core.DOTweenSettings.SettingsLocationstoreSettingsLocation  // 0x6c
  public            DG.Tweening.Core.DOTweenSettings.ModulesSetupmodules  // 0x70
  public            System.Boolean                  createASMDEF  // 0x78
  public            System.Boolean                  showPlayingTweens  // 0x79
  public            System.Boolean                  showPausedTweens  // 0x7a
METHODS:
  RVA=0x04CA69F0  token=0x600040F  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Core.Extensions
TYPE:  static class
TOKEN: 0x20000AF
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000412  T SetSpecialStartupMode(T t, DG.Tweening.Core.Enums.SpecialStartupMode mode)
  RVA=-1  // generic def  token=0x6000413  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> Blendable(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
  RVA=-1  // generic def  token=0x6000414  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> NoFrom(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
END_CLASS

CLASS: DG.Tweening.Core.DOTweenExternalCommand
TYPE:  static class
TOKEN: 0x20000B0
SIZE:  0x10
FIELDS:
  private   static  System.Action<DG.Tweening.Plugins.Options.PathOptions,DG.Tweening.Tween,UnityEngine.Quaternion,UnityEngine.Transform>SetOrientationOnPath  // static @ 0x0
EVENTS:
  SetOrientationOnPath  add=add_SetOrientationOnPath  remove=remove_SetOrientationOnPath
METHODS:
  RVA=0x0697A0D8  token=0x6000417  System.Void Dispatch_SetOrientationOnPath(DG.Tweening.Plugins.Options.PathOptions options, DG.Tweening.Tween t, UnityEngine.Quaternion newRot, UnityEngine.Transform trans)
END_CLASS

CLASS: DG.Tweening.Core.SafeModeReport
TYPE:  sealed struct
TOKEN: 0x20000B1
SIZE:  0x20
FIELDS:
  private           System.Int32                    <totMissingTargetOrFieldErrors>k__BackingField  // 0x10
  private           System.Int32                    <totCallbackErrors>k__BackingField  // 0x14
  private           System.Int32                    <totStartupErrors>k__BackingField  // 0x18
  private           System.Int32                    <totUnsetErrors>k__BackingField  // 0x1c
PROPERTIES:
  totMissingTargetOrFieldErrors  get=0x02B77620  set=0x04D86140
  totCallbackErrors  get=0x04D88B20  set=0x04D88B50
  totStartupErrors  get=0x04D88B10  set=0x04D88B40
  totUnsetErrors  get=0x04D8CAD0  set=0x04D8CAE0
METHODS:
  RVA=0x06981C20  token=0x6000420  System.Void Add(DG.Tweening.Core.SafeModeReport.SafeModeReportType type)
  RVA=0x04D8CAC0  token=0x6000421  System.Int32 GetTotErrors()
END_CLASS

CLASS: DG.Tweening.Core.SequenceCallback
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0x28
EXTENDS: DG.Tweening.Core.ABSSequentiable
FIELDS:
METHODS:
  RVA=0x045E3510  token=0x6000422  System.Void .ctor(System.Single sequencedPosition, DG.Tweening.TweenCallback callback)
END_CLASS

CLASS: DG.Tweening.Core.TweenLink
TYPE:  class
TOKEN: 0x20000B4
SIZE:  0x20
FIELDS:
  public    readonly UnityEngine.GameObject          target  // 0x10
  public    readonly DG.Tweening.LinkBehaviour       behaviour  // 0x18
  public            System.Boolean                  lastSeenActive  // 0x1c
METHODS:
  RVA=0x0698382C  token=0x6000423  System.Void .ctor(UnityEngine.GameObject target, DG.Tweening.LinkBehaviour behaviour)
END_CLASS

CLASS: DG.Tweening.Core.TweenManager
TYPE:  static class
TOKEN: 0x20000B5
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    _DefaultMaxTweeners  // const
  private   static  System.Int32                    _DefaultMaxSequences  // const
  private   static  System.String                   _MaxTweensReached  // const
  private   static  System.Single                   _EpsilonVsTimeCheck  // const
  private   static  System.Boolean                  isUnityEditor  // static @ 0x0
  private   static  System.Boolean                  isDebugBuild  // static @ 0x1
  private   static  System.Int32                    maxActive  // static @ 0x4
  private   static  System.Int32                    maxTweeners  // static @ 0x8
  private   static  System.Int32                    maxSequences  // static @ 0xc
  private   static  System.Boolean                  hasActiveTweens  // static @ 0x10
  private   static  System.Boolean                  hasActiveDefaultTweens  // static @ 0x11
  private   static  System.Boolean                  hasActiveLateTweens  // static @ 0x12
  private   static  System.Boolean                  hasActiveFixedTweens  // static @ 0x13
  private   static  System.Boolean                  hasActiveManualTweens  // static @ 0x14
  private   static  System.Int32                    totActiveTweens  // static @ 0x18
  private   static  System.Int32                    totActiveDefaultTweens  // static @ 0x1c
  private   static  System.Int32                    totActiveLateTweens  // static @ 0x20
  private   static  System.Int32                    totActiveFixedTweens  // static @ 0x24
  private   static  System.Int32                    totActiveManualTweens  // static @ 0x28
  private   static  System.Int32                    totActiveTweeners  // static @ 0x2c
  private   static  System.Int32                    totActiveSequences  // static @ 0x30
  private   static  System.Int32                    totPooledTweeners  // static @ 0x34
  private   static  System.Int32                    totPooledSequences  // static @ 0x38
  private   static  System.Int32                    totTweeners  // static @ 0x3c
  private   static  System.Int32                    totSequences  // static @ 0x40
  private   static  System.Boolean                  isUpdateLoop  // static @ 0x44
  private   static  DG.Tweening.Tween[]             _activeTweens  // static @ 0x48
  private   static  DG.Tweening.Tween[]             _pooledTweeners  // static @ 0x50
  private   static readonly System.Collections.Generic.Stack<DG.Tweening.Tween>_PooledSequences  // static @ 0x58
  private   static readonly System.Collections.Generic.List<DG.Tweening.Tween>_KillList  // static @ 0x60
  private   static readonly System.Collections.Generic.Dictionary<DG.Tweening.Tween,DG.Tweening.Core.TweenLink>_TweenLinks  // static @ 0x68
  private   static  System.Int32                    _totTweenLinks  // static @ 0x70
  private   static  System.Int32                    _maxActiveLookupId  // static @ 0x74
  private   static  System.Boolean                  _requiresActiveReorganization  // static @ 0x78
  private   static  System.Int32                    _reorganizeFromId  // static @ 0x7c
  private   static  System.Int32                    _minPooledTweenerId  // static @ 0x80
  private   static  System.Int32                    _maxPooledTweenerId  // static @ 0x84
  private   static  System.Boolean                  _despawnAllCalledFromUpdateLoopCallback  // static @ 0x88
METHODS:
  RVA=0x049B6DE0  token=0x6000424  System.Void .cctor()
  RVA=-1  // generic def  token=0x6000425  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> GetTweener()
  RVA=0x039690E0  token=0x6000426  DG.Tweening.Sequence GetSequence()
  RVA=0x045D4790  token=0x6000427  System.Void SetUpdateType(DG.Tweening.Tween t, DG.Tweening.UpdateType updateType, System.Boolean isIndependentUpdate)
  RVA=0x03969860  token=0x6000428  System.Void AddActiveTweenToSequence(DG.Tweening.Tween t)
  RVA=0x0396A8F0  token=0x6000429  System.Int32 DespawnAll()
  RVA=0x03969BC0  token=0x600042A  System.Void Despawn(DG.Tweening.Tween t, System.Boolean modifyActiveLists)
  RVA=0x0698446C  token=0x600042B  System.Void PurgeAll(System.Boolean isApplicationQuitting)
  RVA=0x069846B8  token=0x600042C  System.Void PurgePools()
  RVA=0x0698386C  token=0x600042D  System.Void AddTweenLink(DG.Tweening.Tween t, DG.Tweening.Core.TweenLink tweenLink)
  RVA=0x069847B4  token=0x600042E  System.Void RemoveTweenLink(DG.Tweening.Tween t)
  RVA=0x0698487C  token=0x600042F  System.Void ResetCapacities()
  RVA=0x069848C8  token=0x6000430  System.Void SetCapacities(System.Int32 tweenersCapacity, System.Int32 sequencesCapacity)
  RVA=0x06984D34  token=0x6000431  System.Int32 Validate()
  RVA=0x03152990  token=0x6000432  System.Void Update(DG.Tweening.UpdateType updateType, System.Single deltaTime, System.Single independentTime)
  RVA=0x03152C20  token=0x6000433  System.Boolean Update(DG.Tweening.Tween t, System.Single deltaTime, System.Single independentTime, System.Boolean isSingleTweenManualUpdate)
  RVA=0x031527C0  token=0x6000434  System.Int32 FilteredOperation(DG.Tweening.Core.Enums.OperationType operationType, DG.Tweening.Core.Enums.FilterType filterType, System.Object id, System.Boolean optionalBool, System.Single optionalFloat, System.Object optionalObj, System.Object[] optionalArray)
  RVA=0x03969A70  token=0x6000435  System.Boolean Complete(DG.Tweening.Tween t, System.Boolean modifyActiveLists, DG.Tweening.Core.Enums.UpdateMode updateMode)
  RVA=0x06983E18  token=0x6000436  System.Boolean Flip(DG.Tweening.Tween t)
  RVA=0x03D312E0  token=0x6000437  System.Void ForceInit(DG.Tweening.Tween t, System.Boolean isSequenced)
  RVA=0x03D311B0  token=0x6000438  System.Boolean Goto(DG.Tweening.Tween t, System.Single to, System.Boolean andPlay, DG.Tweening.Core.Enums.UpdateMode updateMode)
  RVA=0x044781A0  token=0x6000439  System.Boolean Pause(DG.Tweening.Tween t)
  RVA=0x04CE17D0  token=0x600043A  System.Boolean Play(DG.Tweening.Tween t)
  RVA=0x06984330  token=0x600043B  System.Boolean PlayBackwards(DG.Tweening.Tween t)
  RVA=0x069843E0  token=0x600043C  System.Boolean PlayForward(DG.Tweening.Tween t)
  RVA=0x03542B70  token=0x600043D  System.Boolean Restart(DG.Tweening.Tween t, System.Boolean includeDelay, System.Single changeDelayTo)
  RVA=0x03542C20  token=0x600043E  System.Boolean Rewind(DG.Tweening.Tween t, System.Boolean includeDelay)
  RVA=0x069849D8  token=0x600043F  System.Boolean SmoothRewind(DG.Tweening.Tween t)
  RVA=0x06984AC4  token=0x6000440  System.Boolean TogglePause(DG.Tweening.Tween t)
  RVA=0x06984C3C  token=0x6000441  System.Int32 TotalPooledTweens()
  RVA=0x06984B2C  token=0x6000442  System.Int32 TotalPlayingTweens()
  RVA=0x06984C90  token=0x6000443  System.Int32 TotalTweensById(System.Object id, System.Boolean playingOnly)
  RVA=0x06983E38  token=0x6000444  System.Collections.Generic.List<DG.Tweening.Tween> GetActiveTweens(System.Boolean playing, System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x06983FD0  token=0x6000445  System.Collections.Generic.List<DG.Tweening.Tween> GetTweensById(System.Object id, System.Boolean playingOnly, System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x069839D0  token=0x6000446  System.Int32 DoGetTweensById(System.Object id, System.Boolean playingOnly, System.Boolean addToList, System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x06984104  token=0x6000447  System.Collections.Generic.List<DG.Tweening.Tween> GetTweensByTarget(System.Object target, System.Boolean playingOnly, System.Collections.Generic.List<DG.Tweening.Tween> fillableList)
  RVA=0x039697F0  token=0x6000448  System.Void MarkForKilling(DG.Tweening.Tween t, System.Boolean isSingleTweenManualUpdate)
  RVA=0x06983B90  token=0x6000449  System.Void EvaluateTweenLink(DG.Tweening.Tween t)
  RVA=0x0396A1F0  token=0x600044A  System.Void AddActiveTween(DG.Tweening.Tween t)
  RVA=0x0396A4B0  token=0x600044B  System.Void ReorganizeActiveTweens()
  RVA=0x03969B40  token=0x600044C  System.Void DespawnActiveTweens(System.Collections.Generic.List<DG.Tweening.Tween> tweens)
  RVA=0x03969EA0  token=0x600044D  System.Void RemoveActiveTween(DG.Tweening.Tween t)
  RVA=0x03969890  token=0x600044E  System.Void ClearTweenArray(DG.Tweening.Tween[] tweens)
  RVA=0x04A29580  token=0x600044F  System.Void IncreaseCapacities(DG.Tweening.Core.TweenManager.CapacityIncreaseMode increaseMode)
  RVA=0x069842AC  token=0x6000450  System.Void ManageOnRewindCallbackWhenAlreadyRewinded(DG.Tweening.Tween t, System.Boolean isPlayBackwardsOrSmoothRewind)
END_CLASS

CLASS: DG.Tweening.Core.DOTweenUtils
TYPE:  static class
TOKEN: 0x20000B7
SIZE:  0x10
FIELDS:
  private   static  System.Reflection.Assembly[]    _loadedAssemblies  // static @ 0x0
  private   static readonly System.String[]                 _defAssembliesToQuery  // static @ 0x8
METHODS:
  RVA=0x04D8CB00  token=0x6000451  UnityEngine.Vector3 Vector3FromAngle(System.Single degrees, System.Single magnitude)
  RVA=0x06987E28  token=0x6000452  System.Single Angle2D(UnityEngine.Vector3 from, UnityEngine.Vector3 to)
  RVA=0x06987FAC  token=0x6000453  UnityEngine.Vector3 RotateAroundPivot(UnityEngine.Vector3 point, UnityEngine.Vector3 pivot, UnityEngine.Quaternion rotation)
  RVA=0x06987F28  token=0x6000454  UnityEngine.Vector2 GetPointOnCircle(UnityEngine.Vector2 center, System.Single radius, System.Single degrees)
  RVA=0x0698806C  token=0x6000455  System.Boolean Vector3AreApproximatelyEqual(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x049613C0  token=0x6000456  System.Type GetLooseScriptType(System.String typeName)
  RVA=0x04CA48F0  token=0x6000457  System.Void .cctor()
END_CLASS

CLASS: DG.Tweening.Core.TweenerCore`3
TYPE:  class
TOKEN: 0x20000B8
EXTENDS: DG.Tweening.Tweener
FIELDS:
  public            T2                              startValue  // 0x0
  public            T2                              endValue  // 0x0
  public            T2                              changeValue  // 0x0
  public            TPlugOptions                    plugOptions  // 0x0
  public            DG.Tweening.Core.DOGetter<T1>   getter  // 0x0
  public            DG.Tweening.Core.DOSetter<T1>   setter  // 0x0
  private           DG.Tweening.Plugins.Core.ABSTweenPlugin<T1,T2,TPlugOptions>tweenPlugin  // 0x0
  private   static  System.String                   _TxtCantChangeSequencedValues  // const
  private           System.Type                     _colorType  // 0x0
  private           System.Type                     _color32Type  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000458  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000459  DG.Tweening.Tweener ChangeStartValue(System.Object newStartValue, System.Single newDuration)
  RVA=-1  // not resolved  token=0x600045A  DG.Tweening.Tweener ChangeEndValue(System.Object newEndValue, System.Boolean snapStartValue)
  RVA=-1  // not resolved  token=0x600045B  DG.Tweening.Tweener ChangeEndValue(System.Object newEndValue, System.Single newDuration, System.Boolean snapStartValue)
  RVA=-1  // not resolved  token=0x600045C  DG.Tweening.Tweener ChangeValues(System.Object newStartValue, System.Object newEndValue, System.Single newDuration)
  RVA=-1  // not resolved  token=0x600045D  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> ChangeStartValue(T2 newStartValue, System.Single newDuration)
  RVA=-1  // not resolved  token=0x600045E  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> ChangeEndValue(T2 newEndValue, System.Boolean snapStartValue)
  RVA=-1  // not resolved  token=0x600045F  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> ChangeEndValue(T2 newEndValue, System.Single newDuration, System.Boolean snapStartValue)
  RVA=-1  // not resolved  token=0x6000460  DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> ChangeValues(T2 newStartValue, T2 newEndValue, System.Single newDuration)
  RVA=-1  // not resolved  token=0x6000461  DG.Tweening.Tweener SetFrom(System.Boolean relative)
  RVA=-1  // not resolved  token=0x6000462  DG.Tweening.Tweener SetFrom(T2 fromValue, System.Boolean setImmediately, System.Boolean relative)
  RVA=-1  // not resolved  token=0x6000463  System.Void Reset()
  RVA=-1  // not resolved  token=0x6000464  System.Boolean Validate()
  RVA=-1  // not resolved  token=0x6000465  System.Boolean ValidateChangeValueType(System.Type newType, System.Boolean& isColor32ToColor)
  RVA=-1  // not resolved  token=0x6000466  System.Single UpdateDelay(System.Single elapsed)
  RVA=-1  // not resolved  token=0x6000467  System.Boolean Startup()
  RVA=-1  // not resolved  token=0x6000468  System.Boolean ApplyTween(System.Single prevPosition, System.Int32 prevCompletedLoops, System.Int32 newCompletedSteps, System.Boolean useInversePosition, DG.Tweening.Core.Enums.UpdateMode updateMode, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
END_CLASS

CLASS: DG.Tweening.Core.Easing.Bounce
TYPE:  static class
TOKEN: 0x20000C1
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x06987D4C  token=0x6000469  System.Single EaseIn(System.Single time, System.Single duration, System.Single unusedOvershootOrAmplitude, System.Single unusedPeriod)
  RVA=0x06987D88  token=0x600046A  System.Single EaseOut(System.Single time, System.Single duration, System.Single unusedOvershootOrAmplitude, System.Single unusedPeriod)
  RVA=0x06987CD8  token=0x600046B  System.Single EaseInOut(System.Single time, System.Single duration, System.Single unusedOvershootOrAmplitude, System.Single unusedPeriod)
END_CLASS

CLASS: DG.Tweening.Core.Easing.EaseManager
TYPE:  static class
TOKEN: 0x20000C2
SIZE:  0x10
FIELDS:
  private   static  System.Single                   _PiOver2  // const
  private   static  System.Single                   _TwoPi  // const
METHODS:
  RVA=0x06988130  token=0x600046C  System.Single Evaluate(DG.Tweening.Tween t, System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x031534F0  token=0x600046D  System.Single Evaluate(DG.Tweening.Ease easeType, DG.Tweening.EaseFunction customEase, System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06988174  token=0x600046E  DG.Tweening.EaseFunction ToEaseFunction(DG.Tweening.Ease ease)
  RVA=0x03FD4B60  token=0x600046F  System.Boolean IsFlashEase(DG.Tweening.Ease ease)
END_CLASS

CLASS: DG.Tweening.Core.Easing.EaseCurve
TYPE:  class
TOKEN: 0x20000C4
SIZE:  0x18
FIELDS:
  private   readonly UnityEngine.AnimationCurve      _animCurve  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000496  System.Void .ctor(UnityEngine.AnimationCurve animCurve)
  RVA=0x03DF0AC0  token=0x6000497  System.Single Evaluate(System.Single time, System.Single duration, System.Single unusedOvershoot, System.Single unusedPeriod)
END_CLASS

CLASS: DG.Tweening.Core.Easing.Flash
TYPE:  static class
TOKEN: 0x20000C5
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x06989CB8  token=0x6000498  System.Single Ease(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989B24  token=0x6000499  System.Single EaseIn(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989BE4  token=0x600049A  System.Single EaseOut(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989A20  token=0x600049B  System.Single EaseInOut(System.Single time, System.Single duration, System.Single overshootOrAmplitude, System.Single period)
  RVA=0x06989D74  token=0x600049C  System.Single WeightedEase(System.Single overshootOrAmplitude, System.Single period, System.Int32 stepIndex, System.Single stepDuration, System.Single dir, System.Single res)
END_CLASS

CLASS: DG.Tweening.Core.Enums.FilterType
TYPE:  sealed struct
TOKEN: 0x20000B9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.FilterTypeAll  // const
  public    static  DG.Tweening.Core.Enums.FilterTypeTargetOrId  // const
  public    static  DG.Tweening.Core.Enums.FilterTypeTargetAndId  // const
  public    static  DG.Tweening.Core.Enums.FilterTypeAllExceptTargetsOrIds  // const
  public    static  DG.Tweening.Core.Enums.FilterTypeDOGetter  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.Enums.NestedTweenFailureBehaviour
TYPE:  sealed struct
TOKEN: 0x20000BA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.NestedTweenFailureBehaviourTryToPreserveSequence  // const
  public    static  DG.Tweening.Core.Enums.NestedTweenFailureBehaviourKillWholeSequence  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.Enums.OperationType
TYPE:  sealed struct
TOKEN: 0x20000BB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.OperationTypeComplete  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeDespawn  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeFlip  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeGoto  // const
  public    static  DG.Tweening.Core.Enums.OperationTypePause  // const
  public    static  DG.Tweening.Core.Enums.OperationTypePlay  // const
  public    static  DG.Tweening.Core.Enums.OperationTypePlayForward  // const
  public    static  DG.Tweening.Core.Enums.OperationTypePlayBackwards  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeRewind  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeSmoothRewind  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeRestart  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeTogglePause  // const
  public    static  DG.Tweening.Core.Enums.OperationTypeIsTweening  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.Enums.SafeModeLogBehaviour
TYPE:  sealed struct
TOKEN: 0x20000BC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.SafeModeLogBehaviourNone  // const
  public    static  DG.Tweening.Core.Enums.SafeModeLogBehaviourNormal  // const
  public    static  DG.Tweening.Core.Enums.SafeModeLogBehaviourWarning  // const
  public    static  DG.Tweening.Core.Enums.SafeModeLogBehaviourError  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.Enums.SpecialStartupMode
TYPE:  sealed struct
TOKEN: 0x20000BD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.SpecialStartupModeNone  // const
  public    static  DG.Tweening.Core.Enums.SpecialStartupModeSetLookAt  // const
  public    static  DG.Tweening.Core.Enums.SpecialStartupModeSetShake  // const
  public    static  DG.Tweening.Core.Enums.SpecialStartupModeSetPunch  // const
  public    static  DG.Tweening.Core.Enums.SpecialStartupModeSetCameraShakePosition  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.Enums.UpdateNotice
TYPE:  sealed struct
TOKEN: 0x20000BE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.UpdateNoticeNone  // const
  public    static  DG.Tweening.Core.Enums.UpdateNoticeRewindStep  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.Enums.UpdateMode
TYPE:  sealed struct
TOKEN: 0x20000BF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.UpdateModeUpdate  // const
  public    static  DG.Tweening.Core.Enums.UpdateModeGoto  // const
  public    static  DG.Tweening.Core.Enums.UpdateModeIgnoreOnUpdate  // const
  public    static  DG.Tweening.Core.Enums.UpdateModeIgnoreOnComplete  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.Enums.RewindCallbackMode
TYPE:  sealed struct
TOKEN: 0x20000C0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.Enums.RewindCallbackModeFireIfPositionChanged  // const
  public    static  DG.Tweening.Core.Enums.RewindCallbackModeFireAlwaysWithRewind  // const
  public    static  DG.Tweening.Core.Enums.RewindCallbackModeFireAlways  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.CustomPlugins.PureQuaternionPlugin
TYPE:  class
TOKEN: 0x200009E
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
  private   static  DG.Tweening.CustomPlugins.PureQuaternionPlugin_plug  // static @ 0x0
METHODS:
  RVA=0x04860530  token=0x60003AC  DG.Tweening.CustomPlugins.PureQuaternionPlugin Plug()
  RVA=0x041E1670  token=0x60003AD  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697F3C4  token=0x60003AE  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> t, System.Boolean isRelative)
  RVA=0x0697F2D0  token=0x60003AF  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> t, UnityEngine.Quaternion fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9D0  token=0x60003B0  UnityEngine.Quaternion ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> t, UnityEngine.Quaternion value)
  RVA=0x0697F474  token=0x60003B1  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x04820800  token=0x60003B2  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697F28C  token=0x60003B3  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.NoOptions options, System.Single unitsXSecond, UnityEngine.Quaternion changeValue)
  RVA=0x04404610  token=0x60003B4  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.NoOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Quaternion> getter, DG.Tweening.Core.DOSetter<UnityEngine.Quaternion> setter, System.Single elapsed, UnityEngine.Quaternion startValue, UnityEngine.Quaternion changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x048605D0  token=0x60003B5  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.CircleOptions
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x2C
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            System.Single                   endValueDegrees  // 0x10
  public            System.Boolean                  relativeCenter  // 0x14
  public            System.Boolean                  snapping  // 0x15
  private           UnityEngine.Vector2             center  // 0x18
  private           System.Single                   radius  // 0x20
  private           System.Single                   startValueDegrees  // 0x24
  private           System.Boolean                  initialized  // 0x28
METHODS:
  RVA=0x04D8C7D0  token=0x60002AF  System.Void Reset()
  RVA=0x06959A1C  token=0x60002B0  System.Void Initialize(UnityEngine.Vector2 startValue, UnityEngine.Vector2 endValue)
END_CLASS

CLASS: DG.Tweening.Plugins.CirclePlugin
TYPE:  class
TOKEN: 0x2000073
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002B1  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t)
  RVA=0x06959F8C  token=0x60002B2  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t, System.Boolean isRelative)
  RVA=0x06959E30  token=0x60002B3  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t, UnityEngine.Vector2 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x06959D7C  token=0x60002B4  DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> Get()
  RVA=0x04D8C7E0  token=0x60002B5  UnityEngine.Vector2 ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t, UnityEngine.Vector2 value)
  RVA=0x0695A0D4  token=0x60002B6  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t)
  RVA=0x06959DAC  token=0x60002B7  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> t)
  RVA=0x06959D64  token=0x60002B8  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.CircleOptions options, System.Single unitsXSecond, UnityEngine.Vector2 changeValue)
  RVA=0x06959AE8  token=0x60002B9  System.Void EvaluateAndApply(DG.Tweening.Plugins.CircleOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector2> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector2> setter, System.Single elapsed, UnityEngine.Vector2 startValue, UnityEngine.Vector2 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x06959C8C  token=0x60002BA  UnityEngine.Vector2 GetPositionOnCircle(DG.Tweening.Plugins.CircleOptions options, System.Single degrees)
  RVA=0x0695A150  token=0x60002BB  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Color2Plugin
TYPE:  class
TOKEN: 0x2000074
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002BC  System.Void Reset(DG.Tweening.Core.TweenerCore<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions> t)
  RVA=0x0695A6F4  token=0x60002BD  System.Void SetFrom(DG.Tweening.Core.TweenerCore<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions> t, System.Boolean isRelative)
  RVA=0x0695A564  token=0x60002BE  System.Void SetFrom(DG.Tweening.Core.TweenerCore<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions> t, DG.Tweening.Color2 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C7F0  token=0x60002BF  DG.Tweening.Color2 ConvertToStartValue(DG.Tweening.Core.TweenerCore<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions> t, DG.Tweening.Color2 value)
  RVA=0x0695A874  token=0x60002C0  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions> t)
  RVA=0x0695A4E8  token=0x60002C1  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions> t)
  RVA=0x0695A4D8  token=0x60002C2  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.ColorOptions options, System.Single unitsXSecond, DG.Tweening.Color2 changeValue)
  RVA=0x0695A178  token=0x60002C3  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.ColorOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<DG.Tweening.Color2> getter, DG.Tweening.Core.DOSetter<DG.Tweening.Color2> setter, System.Single elapsed, DG.Tweening.Color2 startValue, DG.Tweening.Color2 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0695A8F0  token=0x60002C4  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.DoublePlugin
TYPE:  class
TOKEN: 0x2000075
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002C5  System.Void Reset(DG.Tweening.Core.TweenerCore<System.Double,System.Double,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697AEC4  token=0x60002C6  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Double,System.Double,DG.Tweening.Plugins.Options.NoOptions> t, System.Boolean isRelative)
  RVA=0x0697AF50  token=0x60002C7  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Double,System.Double,DG.Tweening.Plugins.Options.NoOptions> t, System.Double fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9C0  token=0x60002C8  System.Double ConvertToStartValue(DG.Tweening.Core.TweenerCore<System.Double,System.Double,DG.Tweening.Plugins.Options.NoOptions> t, System.Double value)
  RVA=0x0697AFEC  token=0x60002C9  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<System.Double,System.Double,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697AE98  token=0x60002CA  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<System.Double,System.Double,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697AE7C  token=0x60002CB  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.NoOptions options, System.Single unitsXSecond, System.Double changeValue)
  RVA=0x0697AD18  token=0x60002CC  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.NoOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<System.Double> getter, DG.Tweening.Core.DOSetter<System.Double> setter, System.Single elapsed, System.Double startValue, System.Double changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0697B018  token=0x60002CD  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.LongPlugin
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002CE  System.Void Reset(DG.Tweening.Core.TweenerCore<System.Int64,System.Int64,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697BB04  token=0x60002CF  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Int64,System.Int64,DG.Tweening.Plugins.Options.NoOptions> t, System.Boolean isRelative)
  RVA=0x0697BB88  token=0x60002D0  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Int64,System.Int64,DG.Tweening.Plugins.Options.NoOptions> t, System.Int64 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C7E0  token=0x60002D1  System.Int64 ConvertToStartValue(DG.Tweening.Core.TweenerCore<System.Int64,System.Int64,DG.Tweening.Plugins.Options.NoOptions> t, System.Int64 value)
  RVA=0x0697BC14  token=0x60002D2  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<System.Int64,System.Int64,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697BAD8  token=0x60002D3  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<System.Int64,System.Int64,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697BABC  token=0x60002D4  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.NoOptions options, System.Single unitsXSecond, System.Int64 changeValue)
  RVA=0x0697B924  token=0x60002D5  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.NoOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<System.Int64> getter, DG.Tweening.Core.DOSetter<System.Int64> setter, System.Single elapsed, System.Int64 startValue, System.Int64 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0697BC38  token=0x60002D6  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.UlongPlugin
TYPE:  class
TOKEN: 0x2000077
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002D7  System.Void Reset(DG.Tweening.Core.TweenerCore<System.UInt64,System.UInt64,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x069857E0  token=0x60002D8  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.UInt64,System.UInt64,DG.Tweening.Plugins.Options.NoOptions> t, System.Boolean isRelative)
  RVA=0x0697BB88  token=0x60002D9  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.UInt64,System.UInt64,DG.Tweening.Plugins.Options.NoOptions> t, System.UInt64 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C7E0  token=0x60002DA  System.UInt64 ConvertToStartValue(DG.Tweening.Core.TweenerCore<System.UInt64,System.UInt64,DG.Tweening.Plugins.Options.NoOptions> t, System.UInt64 value)
  RVA=0x0697BC14  token=0x60002DB  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<System.UInt64,System.UInt64,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697BAD8  token=0x60002DC  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<System.UInt64,System.UInt64,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x069857A0  token=0x60002DD  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.NoOptions options, System.Single unitsXSecond, System.UInt64 changeValue)
  RVA=0x069855A4  token=0x60002DE  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.NoOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<System.UInt64> getter, DG.Tweening.Core.DOSetter<System.UInt64> setter, System.Single elapsed, System.UInt64 startValue, System.UInt64 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x06985864  token=0x60002DF  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Vector3ArrayPlugin
TYPE:  class
TOKEN: 0x2000078
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x06980CE0  token=0x60002E0  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
  RVA=0x041E1670  token=0x60002E1  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, System.Boolean isRelative)
  RVA=0x041E1670  token=0x60002E2  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, UnityEngine.Vector3[] fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x06985C50  token=0x60002E3  UnityEngine.Vector3[] ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, UnityEngine.Vector3 value)
  RVA=0x06986600  token=0x60002E4  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
  RVA=0x069864BC  token=0x60002E5  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
  RVA=0x06986428  token=0x60002E6  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.Vector3ArrayOptions options, System.Single unitsXSecond, UnityEngine.Vector3[] changeValue)
  RVA=0x06985D54  token=0x60002E7  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.Vector3ArrayOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single elapsed, UnityEngine.Vector3[] startValue, UnityEngine.Vector3[] changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x06986760  token=0x60002E8  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.PathPlugin
TYPE:  class
TOKEN: 0x2000079
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
  public    static  System.Single                   MinLookAhead  // const
METHODS:
  RVA=0x0697C198  token=0x60002E9  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t)
  RVA=0x041E1670  token=0x60002EA  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, System.Boolean isRelative)
  RVA=0x041E1670  token=0x60002EB  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, DG.Tweening.Plugins.Core.PathCore.Path fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x0697C168  token=0x60002EC  DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> Get()
  RVA=0x0697BCF8  token=0x60002ED  DG.Tweening.Plugins.Core.PathCore.Path ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Vector3 value)
  RVA=0x0697D790  token=0x60002EE  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t)
  RVA=0x0697C200  token=0x60002EF  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> t)
  RVA=0x0697C148  token=0x60002F0  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.PathOptions options, System.Single unitsXSecond, DG.Tweening.Plugins.Core.PathCore.Path changeValue)
  RVA=0x0697BD48  token=0x60002F1  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.PathOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single elapsed, DG.Tweening.Plugins.Core.PathCore.Path startValue, DG.Tweening.Plugins.Core.PathCore.Path changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0697C6BC  token=0x60002F2  System.Void SetOrientation(DG.Tweening.Plugins.Options.PathOptions options, DG.Tweening.Tween t, DG.Tweening.Plugins.Core.PathCore.Path path, System.Single pathPerc, UnityEngine.Vector3 tPos, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0697BD14  token=0x60002F3  UnityEngine.Vector3 DivideVectorByVector(UnityEngine.Vector3 vector, UnityEngine.Vector3 byVector)
  RVA=0x04D8CA10  token=0x60002F4  UnityEngine.Vector3 MultiplyVectorByVector(UnityEngine.Vector3 vector, UnityEngine.Vector3 byVector)
  RVA=0x0697D898  token=0x60002F5  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.ColorPlugin
TYPE:  class
TOKEN: 0x200007A
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002F6  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t)
  RVA=0x069785F8  token=0x60002F7  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t, System.Boolean isRelative)
  RVA=0x069786DC  token=0x60002F8  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t, UnityEngine.Color fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9D0  token=0x60002F9  UnityEngine.Color ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t, UnityEngine.Color value)
  RVA=0x06978814  token=0x60002FA  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t)
  RVA=0x049E2C60  token=0x60002FB  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> t)
  RVA=0x0695A4D8  token=0x60002FC  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.ColorOptions options, System.Single unitsXSecond, UnityEngine.Color changeValue)
  RVA=0x03C7AFE0  token=0x60002FD  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.ColorOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Color> getter, DG.Tweening.Core.DOSetter<UnityEngine.Color> setter, System.Single elapsed, UnityEngine.Color startValue, UnityEngine.Color changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x046FB340  token=0x60002FE  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.IntPlugin
TYPE:  class
TOKEN: 0x200007B
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002FF  System.Void Reset(DG.Tweening.Core.TweenerCore<System.Int32,System.Int32,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697B484  token=0x6000300  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Int32,System.Int32,DG.Tweening.Plugins.Options.NoOptions> t, System.Boolean isRelative)
  RVA=0x0697B3FC  token=0x6000301  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Int32,System.Int32,DG.Tweening.Plugins.Options.NoOptions> t, System.Int32 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9F0  token=0x6000302  System.Int32 ConvertToStartValue(DG.Tweening.Core.TweenerCore<System.Int32,System.Int32,DG.Tweening.Plugins.Options.NoOptions> t, System.Int32 value)
  RVA=0x0697B500  token=0x6000303  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<System.Int32,System.Int32,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697B3D4  token=0x6000304  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<System.Int32,System.Int32,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x0697B3B8  token=0x6000305  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.NoOptions options, System.Single unitsXSecond, System.Int32 changeValue)
  RVA=0x0697B230  token=0x6000306  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.NoOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<System.Int32> getter, DG.Tweening.Core.DOSetter<System.Int32> setter, System.Single elapsed, System.Int32 startValue, System.Int32 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0697B520  token=0x6000307  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.QuaternionPlugin
TYPE:  class
TOKEN: 0x200007C
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000308  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t)
  RVA=0x0697FFC8  token=0x6000309  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t, System.Boolean isRelative)
  RVA=0x069802B4  token=0x600030A  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t, UnityEngine.Vector3 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x0697F514  token=0x600030B  UnityEngine.Vector3 ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t, UnityEngine.Quaternion value)
  RVA=0x06980460  token=0x600030C  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t)
  RVA=0x0697FD40  token=0x600030D  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t)
  RVA=0x0697FD1C  token=0x600030E  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.QuaternionOptions options, System.Single unitsXSecond, UnityEngine.Vector3 changeValue)
  RVA=0x0697F544  token=0x600030F  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.QuaternionOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Quaternion> getter, DG.Tweening.Core.DOSetter<UnityEngine.Quaternion> setter, System.Single elapsed, UnityEngine.Vector3 startValue, UnityEngine.Vector3 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0697F9FC  token=0x6000310  UnityEngine.Vector3 GetEulerValForCalculations(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t, UnityEngine.Vector3 val, UnityEngine.Vector3 counterVal)
  RVA=0x04D8CA70  token=0x6000311  UnityEngine.Vector3 FlipEulerAngles(UnityEngine.Vector3 euler)
  RVA=0x069804D4  token=0x6000312  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.RectOffsetPlugin
TYPE:  class
TOKEN: 0x200007D
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
  private   static  UnityEngine.RectOffset          _r  // static @ 0x0
METHODS:
  RVA=0x06980CE0  token=0x6000313  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.RectOffset,UnityEngine.RectOffset,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x06980EB0  token=0x6000314  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.RectOffset,UnityEngine.RectOffset,DG.Tweening.Plugins.Options.NoOptions> t, System.Boolean isRelative)
  RVA=0x06981050  token=0x6000315  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.RectOffset,UnityEngine.RectOffset,DG.Tweening.Plugins.Options.NoOptions> t, UnityEngine.RectOffset fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x069804FC  token=0x6000316  UnityEngine.RectOffset ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.RectOffset,UnityEngine.RectOffset,DG.Tweening.Plugins.Options.NoOptions> t, UnityEngine.RectOffset value)
  RVA=0x0698126C  token=0x6000317  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.RectOffset,UnityEngine.RectOffset,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x06980D34  token=0x6000318  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.RectOffset,UnityEngine.RectOffset,DG.Tweening.Plugins.Options.NoOptions> t)
  RVA=0x06980C0C  token=0x6000319  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.NoOptions options, System.Single unitsXSecond, UnityEngine.RectOffset changeValue)
  RVA=0x069805C0  token=0x600031A  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.NoOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.RectOffset> getter, DG.Tweening.Core.DOSetter<UnityEngine.RectOffset> setter, System.Single elapsed, UnityEngine.RectOffset startValue, UnityEngine.RectOffset changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0698140C  token=0x600031B  System.Void .ctor()
  RVA=0x06981390  token=0x600031C  System.Void .cctor()
END_CLASS

CLASS: DG.Tweening.Plugins.RectPlugin
TYPE:  class
TOKEN: 0x200007E
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600031D  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> t)
  RVA=0x06981824  token=0x600031E  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> t, System.Boolean isRelative)
  RVA=0x069819C4  token=0x600031F  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> t, UnityEngine.Rect fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9D0  token=0x6000320  UnityEngine.Rect ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> t, UnityEngine.Rect value)
  RVA=0x06981B84  token=0x6000321  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> t)
  RVA=0x069817A8  token=0x6000322  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions> t)
  RVA=0x06981724  token=0x6000323  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.RectOptions options, System.Single unitsXSecond, UnityEngine.Rect changeValue)
  RVA=0x06981434  token=0x6000324  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.RectOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Rect> getter, DG.Tweening.Core.DOSetter<UnityEngine.Rect> setter, System.Single elapsed, UnityEngine.Rect startValue, UnityEngine.Rect changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x06981BF8  token=0x6000325  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.UintPlugin
TYPE:  class
TOKEN: 0x200007F
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000326  System.Void Reset(DG.Tweening.Core.TweenerCore<System.UInt32,System.UInt32,DG.Tweening.Plugins.Options.UintOptions> t)
  RVA=0x06985500  token=0x6000327  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.UInt32,System.UInt32,DG.Tweening.Plugins.Options.UintOptions> t, System.Boolean isRelative)
  RVA=0x0697B3FC  token=0x6000328  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.UInt32,System.UInt32,DG.Tweening.Plugins.Options.UintOptions> t, System.UInt32 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9F0  token=0x6000329  System.UInt32 ConvertToStartValue(DG.Tweening.Core.TweenerCore<System.UInt32,System.UInt32,DG.Tweening.Plugins.Options.UintOptions> t, System.UInt32 value)
  RVA=0x0697B500  token=0x600032A  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<System.UInt32,System.UInt32,DG.Tweening.Plugins.Options.UintOptions> t)
  RVA=0x069854B0  token=0x600032B  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<System.UInt32,System.UInt32,DG.Tweening.Plugins.Options.UintOptions> t)
  RVA=0x0698548C  token=0x600032C  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.UintOptions options, System.Single unitsXSecond, System.UInt32 changeValue)
  RVA=0x069852C0  token=0x600032D  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.UintOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<System.UInt32> getter, DG.Tweening.Core.DOSetter<System.UInt32> setter, System.Single elapsed, System.UInt32 startValue, System.UInt32 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0698557C  token=0x600032E  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Vector2Plugin
TYPE:  class
TOKEN: 0x2000080
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600032F  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x06985A74  token=0x6000330  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t, System.Boolean isRelative)
  RVA=0x069858B8  token=0x6000331  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t, UnityEngine.Vector2 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C7E0  token=0x6000332  UnityEngine.Vector2 ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t, UnityEngine.Vector2 value)
  RVA=0x06985BCC  token=0x6000333  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x03C7B800  token=0x6000334  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x0698588C  token=0x6000335  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.VectorOptions options, System.Single unitsXSecond, UnityEngine.Vector2 changeValue)
  RVA=0x03154F80  token=0x6000336  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.VectorOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector2> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector2> setter, System.Single elapsed, UnityEngine.Vector2 startValue, UnityEngine.Vector2 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x0437E8B0  token=0x6000337  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Vector4Plugin
TYPE:  class
TOKEN: 0x2000081
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000338  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x06987778  token=0x6000339  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t, System.Boolean isRelative)
  RVA=0x06987964  token=0x600033A  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t, UnityEngine.Vector4 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9D0  token=0x600033B  UnityEngine.Vector4 ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t, UnityEngine.Vector4 value)
  RVA=0x06978814  token=0x600033C  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x06987668  token=0x600033D  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x06987644  token=0x600033E  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.VectorOptions options, System.Single unitsXSecond, UnityEngine.Vector4 changeValue)
  RVA=0x0698713C  token=0x600033F  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.VectorOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector4> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector4> setter, System.Single elapsed, UnityEngine.Vector4 startValue, UnityEngine.Vector4 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x06987CB0  token=0x6000340  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.StringPlugin
TYPE:  class
TOKEN: 0x2000082
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
  private   static readonly System.Text.StringBuilder       _Buffer  // static @ 0x0
  private   static readonly System.Collections.Generic.List<System.Char>_OpenedTags  // static @ 0x8
METHODS:
  RVA=0x0698369C  token=0x6000341  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> t, System.Boolean isRelative)
  RVA=0x069835D4  token=0x6000342  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> t, System.String fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x06983228  token=0x6000343  System.Void Reset(DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> t)
  RVA=0x04D8C7E0  token=0x6000344  System.String ConvertToStartValue(DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> t, System.String value)
  RVA=0x041E1670  token=0x6000345  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> t)
  RVA=0x0698336C  token=0x6000346  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<System.String,System.String,DG.Tweening.Plugins.Options.StringOptions> t)
  RVA=0x069831D4  token=0x6000347  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.StringOptions options, System.Single unitsXSecond, System.String changeValue)
  RVA=0x06982D3C  token=0x6000348  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.StringOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<System.String> getter, DG.Tweening.Core.DOSetter<System.String> setter, System.Single elapsed, System.String startValue, System.String changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x069826D8  token=0x6000349  System.Text.StringBuilder Append(System.String value, System.Int32 startIndex, System.Int32 length, System.Boolean richTextEnabled)
  RVA=0x069832A0  token=0x600034A  System.Char[] ScrambledCharsToUse(DG.Tweening.Plugins.Options.StringOptions options)
  RVA=0x06983804  token=0x600034B  System.Void .ctor()
  RVA=0x06983724  token=0x600034C  System.Void .cctor()
END_CLASS

CLASS: DG.Tweening.Plugins.StringPluginExtensions
TYPE:  static class
TOKEN: 0x2000083
SIZE:  0x10
FIELDS:
  public    static readonly System.Char[]                   ScrambledCharsAll  // static @ 0x0
  public    static readonly System.Char[]                   ScrambledCharsUppercase  // static @ 0x8
  public    static readonly System.Char[]                   ScrambledCharsLowercase  // static @ 0x10
  public    static readonly System.Char[]                   ScrambledCharsNumerals  // static @ 0x18
  private   static  System.Int32                    _lastRndSeed  // static @ 0x20
METHODS:
  RVA=0x069824D8  token=0x600034D  System.Void .cctor()
  RVA=0x0698244C  token=0x600034E  System.Void ScrambleChars(System.Char[] chars)
  RVA=0x06982318  token=0x600034F  System.Text.StringBuilder AppendScrambledChars(System.Text.StringBuilder buffer, System.Int32 length, System.Char[] chars)
END_CLASS

CLASS: DG.Tweening.Plugins.FloatPlugin
TYPE:  class
TOKEN: 0x2000084
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000350  System.Void Reset(DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> t)
  RVA=0x0697B058  token=0x6000351  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> t, System.Boolean isRelative)
  RVA=0x0697B124  token=0x6000352  System.Void SetFrom(DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> t, System.Single fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9C0  token=0x6000353  System.Single ConvertToStartValue(DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> t, System.Single value)
  RVA=0x0697B204  token=0x6000354  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> t)
  RVA=0x04192F50  token=0x6000355  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> t)
  RVA=0x0697B040  token=0x6000356  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.FloatOptions options, System.Single unitsXSecond, System.Single changeValue)
  RVA=0x03153400  token=0x6000357  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.FloatOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<System.Single> getter, DG.Tweening.Core.DOSetter<System.Single> setter, System.Single elapsed, System.Single startValue, System.Single changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x03ECA1F0  token=0x6000358  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Vector3Plugin
TYPE:  class
TOKEN: 0x2000085
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000359  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x06986C98  token=0x600035A  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t, System.Boolean isRelative)
  RVA=0x06986E98  token=0x600035B  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t, UnityEngine.Vector3 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x04D8C9A0  token=0x600035C  UnityEngine.Vector3 ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t, UnityEngine.Vector3 value)
  RVA=0x06980460  token=0x600035D  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x06986BB4  token=0x600035E  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> t)
  RVA=0x0697FD1C  token=0x600035F  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.Options.VectorOptions options, System.Single unitsXSecond, UnityEngine.Vector3 changeValue)
  RVA=0x06986788  token=0x6000360  System.Void EvaluateAndApply(DG.Tweening.Plugins.Options.VectorOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single elapsed, UnityEngine.Vector3 startValue, UnityEngine.Vector3 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x06987114  token=0x6000361  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.ITPlugin`4
TYPE:  interface
TOKEN: 0x2000092
FIELDS:
METHODS:
END_CLASS

CLASS: DG.Tweening.Plugins.Core.SpecialPluginsUtils
TYPE:  static class
TOKEN: 0x2000093
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x06981FBC  token=0x600036D  System.Boolean SetLookAt(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions> t)
  RVA=0x0698210C  token=0x600036E  System.Boolean SetPunch(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
  RVA=0x069822B8  token=0x600036F  System.Boolean SetShake(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
  RVA=0x06981D98  token=0x6000370  System.Boolean SetCameraShakePosition(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3[],DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
END_CLASS

CLASS: DG.Tweening.Plugins.Core.IPlugSetter`4
TYPE:  interface
TOKEN: 0x2000094
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000371  DG.Tweening.Core.DOGetter<T1> Getter()
  RVA=-1  // abstract  token=0x6000372  DG.Tweening.Core.DOSetter<T1> Setter()
  RVA=-1  // abstract  token=0x6000373  T2 EndValue()
  RVA=-1  // abstract  token=0x6000374  TPlugOptions GetOptions()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.ITweenPlugin
TYPE:  interface
TOKEN: 0x2000095
FIELDS:
METHODS:
END_CLASS

CLASS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
TYPE:  abstract class
TOKEN: 0x2000096
IMPLEMENTS: DG.Tweening.Plugins.Core.ITweenPlugin
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000375  System.Void Reset(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
  RVA=-1  // abstract  token=0x6000376  System.Void SetFrom(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, System.Boolean isRelative)
  RVA=-1  // abstract  token=0x6000377  System.Void SetFrom(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, T2 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=-1  // abstract  token=0x6000378  T2 ConvertToStartValue(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t, T1 value)
  RVA=-1  // abstract  token=0x6000379  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
  RVA=-1  // abstract  token=0x600037A  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<T1,T2,TPlugOptions> t)
  RVA=-1  // abstract  token=0x600037B  System.Single GetSpeedBasedDuration(TPlugOptions options, System.Single unitsXSecond, T2 changeValue)
  RVA=-1  // abstract  token=0x600037C  System.Void EvaluateAndApply(TPlugOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<T1> getter, DG.Tweening.Core.DOSetter<T1> setter, System.Single elapsed, T2 startValue, T2 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=-1  // not resolved  token=0x600037D  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.PluginsManager
TYPE:  static class
TOKEN: 0x2000097
SIZE:  0x10
FIELDS:
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_floatPlugin  // static @ 0x0
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_doublePlugin  // static @ 0x8
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_intPlugin  // static @ 0x10
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_uintPlugin  // static @ 0x18
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_longPlugin  // static @ 0x20
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_ulongPlugin  // static @ 0x28
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_vector2Plugin  // static @ 0x30
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_vector3Plugin  // static @ 0x38
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_vector4Plugin  // static @ 0x40
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_quaternionPlugin  // static @ 0x48
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_colorPlugin  // static @ 0x50
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_rectPlugin  // static @ 0x58
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_rectOffsetPlugin  // static @ 0x60
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_stringPlugin  // static @ 0x68
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_vector3ArrayPlugin  // static @ 0x70
  private   static  DG.Tweening.Plugins.Core.ITweenPlugin_color2Plugin  // static @ 0x78
  private   static  System.Int32                    _MaxCustomPlugins  // const
  private   static  System.Collections.Generic.Dictionary<System.Type,DG.Tweening.Plugins.Core.ITweenPlugin>_customPlugins  // static @ 0x80
METHODS:
  RVA=-1  // generic def  token=0x600037E  DG.Tweening.Plugins.Core.ABSTweenPlugin<T1,T2,TPlugOptions> GetDefaultPlugin()
  RVA=-1  // generic def  token=0x600037F  DG.Tweening.Plugins.Core.ABSTweenPlugin<T1,T2,TPlugOptions> GetCustomPlugin()
  RVA=0x0697EFC8  token=0x6000380  System.Void PurgeAll()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.PathCore.CubicBezierDecoder
TYPE:  class
TOKEN: 0x2000098
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.PathCore.ABSPathDecoder
FIELDS:
  private   static readonly DG.Tweening.Plugins.Core.PathCore.ControlPoint[]_PartialControlPs  // static @ 0x0
  private   static readonly UnityEngine.Vector3[]           _PartialWps  // static @ 0x8
PROPERTIES:
  minInputWaypoints  get=0x04D8C9E0
METHODS:
  RVA=0x06978A04  token=0x6000382  System.Void FinalizePath(DG.Tweening.Plugins.Core.PathCore.Path p, UnityEngine.Vector3[] wps, System.Boolean isClosedPath)
  RVA=0x06978F4C  token=0x6000383  UnityEngine.Vector3 GetPoint(System.Single perc, UnityEngine.Vector3[] wps, DG.Tweening.Plugins.Core.PathCore.Path p, DG.Tweening.Plugins.Core.PathCore.ControlPoint[] controlPoints)
  RVA=0x069791E4  token=0x6000384  System.Void SetTimeToLengthTables(DG.Tweening.Plugins.Core.PathCore.Path p, System.Int32 subdivisions)
  RVA=0x069793DC  token=0x6000385  System.Void SetWaypointsLengths(DG.Tweening.Plugins.Core.PathCore.Path p, System.Int32 subdivisions)
  RVA=0x041E1670  token=0x6000386  System.Void .ctor()
  RVA=0x0697972C  token=0x6000387  System.Void .cctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.PathCore.ControlPoint
TYPE:  sealed struct
TOKEN: 0x2000099
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             a  // 0x10
  public            UnityEngine.Vector3             b  // 0x1c
METHODS:
  RVA=0x04D871E0  token=0x6000388  System.Void .ctor(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x06978958  token=0x6000389  DG.Tweening.Plugins.Core.PathCore.ControlPoint op_Addition(DG.Tweening.Plugins.Core.PathCore.ControlPoint cp, UnityEngine.Vector3 v)
  RVA=0x06978868  token=0x600038A  System.String ToString()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.PathCore.ABSPathDecoder
TYPE:  abstract class
TOKEN: 0x200009A
SIZE:  0x10
FIELDS:
PROPERTIES:
  minInputWaypoints  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600038B  System.Void FinalizePath(DG.Tweening.Plugins.Core.PathCore.Path p, UnityEngine.Vector3[] wps, System.Boolean isClosedPath)
  RVA=-1  // abstract  token=0x600038C  UnityEngine.Vector3 GetPoint(System.Single perc, UnityEngine.Vector3[] wps, DG.Tweening.Plugins.Core.PathCore.Path p, DG.Tweening.Plugins.Core.PathCore.ControlPoint[] controlPoints)
  RVA=0x041E1670  token=0x600038E  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.PathCore.CatmullRomDecoder
TYPE:  class
TOKEN: 0x200009B
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.PathCore.ABSPathDecoder
FIELDS:
  private   static readonly DG.Tweening.Plugins.Core.PathCore.ControlPoint[]_PartialControlPs  // static @ 0x0
  private   static readonly UnityEngine.Vector3[]           _PartialWps  // static @ 0x8
PROPERTIES:
  minInputWaypoints  get=0x04D85B80
METHODS:
  RVA=0x06977788  token=0x6000390  System.Void FinalizePath(DG.Tweening.Plugins.Core.PathCore.Path p, UnityEngine.Vector3[] wps, System.Boolean isClosedPath)
  RVA=0x06977A74  token=0x6000391  UnityEngine.Vector3 GetPoint(System.Single perc, UnityEngine.Vector3[] wps, DG.Tweening.Plugins.Core.PathCore.Path p, DG.Tweening.Plugins.Core.PathCore.ControlPoint[] controlPoints)
  RVA=0x06977F58  token=0x6000392  System.Void SetTimeToLengthTables(DG.Tweening.Plugins.Core.PathCore.Path p, System.Int32 subdivisions)
  RVA=0x06978150  token=0x6000393  System.Void SetWaypointsLengths(DG.Tweening.Plugins.Core.PathCore.Path p, System.Int32 subdivisions)
  RVA=0x041E1670  token=0x6000394  System.Void .ctor()
  RVA=0x0697854C  token=0x6000395  System.Void .cctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.PathCore.LinearDecoder
TYPE:  class
TOKEN: 0x200009C
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.PathCore.ABSPathDecoder
FIELDS:
PROPERTIES:
  minInputWaypoints  get=0x04D85B80
METHODS:
  RVA=0x0697B548  token=0x6000397  System.Void FinalizePath(DG.Tweening.Plugins.Core.PathCore.Path p, UnityEngine.Vector3[] wps, System.Boolean isClosedPath)
  RVA=0x0697B594  token=0x6000398  UnityEngine.Vector3 GetPoint(System.Single perc, UnityEngine.Vector3[] wps, DG.Tweening.Plugins.Core.PathCore.Path p, DG.Tweening.Plugins.Core.PathCore.ControlPoint[] controlPoints)
  RVA=0x0697B768  token=0x6000399  System.Void SetTimeToLengthTables(DG.Tweening.Plugins.Core.PathCore.Path p, System.Int32 subdivisions)
  RVA=0x041E1670  token=0x600039A  System.Void SetWaypointsLengths(DG.Tweening.Plugins.Core.PathCore.Path p, System.Int32 subdivisions)
  RVA=0x041E1670  token=0x600039B  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Plugins.Core.PathCore.Path
TYPE:  class
TOKEN: 0x200009D
SIZE:  0x120
FIELDS:
  private   static  DG.Tweening.Plugins.Core.PathCore.CatmullRomDecoder_catmullRomDecoder  // static @ 0x0
  private   static  DG.Tweening.Plugins.Core.PathCore.LinearDecoder_linearDecoder  // static @ 0x8
  private   static  DG.Tweening.Plugins.Core.PathCore.CubicBezierDecoder_cubicBezierDecoder  // static @ 0x10
  public            System.Single[]                 wpLengths  // 0x10
  public            UnityEngine.Vector3[]           wps  // 0x18
  private           DG.Tweening.PathType            type  // 0x20
  private           System.Int32                    subdivisionsXSegment  // 0x24
  private           System.Int32                    subdivisions  // 0x28
  private           DG.Tweening.Plugins.Core.PathCore.ControlPoint[]controlPoints  // 0x30
  private           System.Single                   length  // 0x38
  private           System.Boolean                  isFinalized  // 0x3c
  private           System.Single[]                 timesTable  // 0x40
  private           System.Single[]                 lengthsTable  // 0x48
  private           System.Int32                    linearWPIndex  // 0x50
  private           System.Boolean                  addedExtraStartWp  // 0x54
  private           System.Boolean                  addedExtraEndWp  // 0x55
  private           DG.Tweening.Plugins.Options.PathOptionsplugOptions  // 0x58
  private           DG.Tweening.Plugins.Core.PathCore.Path_incrementalClone  // 0xc8
  private           System.Int32                    _incrementalIndex  // 0xd0
  private           DG.Tweening.Plugins.Core.PathCore.ABSPathDecoder_decoder  // 0xd8
  private           System.Boolean                  _changed  // 0xe0
  private           UnityEngine.Vector3[]           nonLinearDrawWps  // 0xe8
  private           UnityEngine.Vector3             targetPosition  // 0xf0
  private           System.Nullable<UnityEngine.Vector3>lookAtPosition  // 0xfc
  private           UnityEngine.Color               gizmoColor  // 0x10c
PROPERTIES:
  minInputWaypoints  get=0x0697EFA4
METHODS:
  RVA=0x0697EE24  token=0x600039D  System.Void .ctor(DG.Tweening.PathType type, UnityEngine.Vector3[] waypoints, System.Int32 subdivisionsXSegment, System.Nullable<UnityEngine.Color> gizmoColor)
  RVA=0x04D8CA50  token=0x600039E  System.Void .ctor()
  RVA=0x0697EA10  token=0x600039F  System.Void FinalizePath(System.Boolean isClosedPath, DG.Tweening.AxisConstraint lockPositionAxes, UnityEngine.Vector3 currTargetVal)
  RVA=0x0697EC10  token=0x60003A0  UnityEngine.Vector3 GetPoint(System.Single perc, System.Boolean convertToConstantPerc)
  RVA=0x0697E168  token=0x60003A1  System.Single ConvertToConstantPathPerc(System.Single perc)
  RVA=0x0697EC88  token=0x60003A2  System.Int32 GetWaypointIndexFromPerc(System.Single perc, System.Boolean isMovingForward)
  RVA=0x0697EB18  token=0x60003A3  UnityEngine.Vector3[] GetDrawPoints(DG.Tweening.Plugins.Core.PathCore.Path p, System.Int32 drawSubdivisionsXSegment)
  RVA=0x0697ED1C  token=0x60003A4  System.Void RefreshNonLinearDrawWps(DG.Tweening.Plugins.Core.PathCore.Path p)
  RVA=0x0697E358  token=0x60003A5  System.Void Destroy()
  RVA=0x0697DB34  token=0x60003A6  DG.Tweening.Plugins.Core.PathCore.Path CloneIncremental(System.Int32 loopIncrement)
  RVA=0x0697DA4C  token=0x60003A7  System.Void AssignWaypoints(UnityEngine.Vector3[] newWps, System.Boolean cloneWps)
  RVA=0x0697D8C0  token=0x60003A8  System.Void AssignDecoder(DG.Tweening.PathType pathType)
  RVA=0x0697EA08  token=0x60003A9  System.Void Draw()
  RVA=0x0697E4A0  token=0x60003AA  System.Void Draw(DG.Tweening.Plugins.Core.PathCore.Path p)
  RVA=0x0697E290  token=0x60003AB  UnityEngine.Vector3 ConvertToDrawPoint(UnityEngine.Vector3 wp, DG.Tweening.Plugins.Options.PathOptions plugOptions)
END_CLASS

CLASS: DG.Tweening.Plugins.Options.IPlugOptions
TYPE:  interface
TOKEN: 0x2000086
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000362  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.OrientType
TYPE:  sealed struct
TOKEN: 0x2000087
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Plugins.Options.OrientTypeNone  // const
  public    static  DG.Tweening.Plugins.Options.OrientTypeToPath  // const
  public    static  DG.Tweening.Plugins.Options.OrientTypeLookAtTransform  // const
  public    static  DG.Tweening.Plugins.Options.OrientTypeLookAtPosition  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Plugins.Options.PathOptions
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x80
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            DG.Tweening.PathMode            mode  // 0x10
  public            DG.Tweening.Plugins.Options.OrientTypeorientType  // 0x14
  public            DG.Tweening.AxisConstraint      lockPositionAxis  // 0x18
  public            DG.Tweening.AxisConstraint      lockRotationAxis  // 0x1c
  public            System.Boolean                  isClosedPath  // 0x20
  public            UnityEngine.Vector3             lookAtPosition  // 0x24
  public            UnityEngine.Transform           lookAtTransform  // 0x30
  public            System.Single                   lookAhead  // 0x38
  public            System.Boolean                  hasCustomForwardDirection  // 0x3c
  public            UnityEngine.Quaternion          forward  // 0x40
  public            System.Boolean                  useLocalPosition  // 0x50
  public            UnityEngine.Transform           parent  // 0x58
  public            System.Boolean                  isRigidbody  // 0x60
  public            System.Boolean                  isRigidbody2D  // 0x61
  public            System.Boolean                  stableZRotation  // 0x62
  private           UnityEngine.Quaternion          startupRot  // 0x64
  private           System.Single                   startupZRot  // 0x74
  private           System.Boolean                  addedExtraStartWp  // 0x78
  private           System.Boolean                  addedExtraEndWp  // 0x79
METHODS:
  RVA=0x0697BC60  token=0x6000363  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.QuaternionOptions
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x34
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            DG.Tweening.RotateMode          rotateMode  // 0x10
  public            DG.Tweening.AxisConstraint      axisConstraint  // 0x14
  public            UnityEngine.Vector3             up  // 0x18
  public            System.Boolean                  dynamicLookAt  // 0x24
  public            UnityEngine.Vector3             dynamicLookAtWorldPosition  // 0x28
METHODS:
  RVA=0x0697F4C8  token=0x6000364  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.UintOptions
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x11
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            System.Boolean                  isNegativeChangeValue  // 0x10
METHODS:
  RVA=0x04D87170  token=0x6000365  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.Vector3ArrayOptions
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x20
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            DG.Tweening.AxisConstraint      axisConstraint  // 0x10
  public            System.Boolean                  snapping  // 0x14
  private           System.Single[]                 durations  // 0x18
METHODS:
  RVA=0x06985C3C  token=0x6000366  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.NoOptions
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x11
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000367  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.ColorOptions
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x11
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            System.Boolean                  alphaOnly  // 0x10
METHODS:
  RVA=0x04D87170  token=0x6000368  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.FloatOptions
TYPE:  sealed struct
TOKEN: 0x200008E
SIZE:  0x11
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            System.Boolean                  snapping  // 0x10
METHODS:
  RVA=0x04D87170  token=0x6000369  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.RectOptions
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x11
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            System.Boolean                  snapping  // 0x10
METHODS:
  RVA=0x04D87170  token=0x600036A  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.StringOptions
TYPE:  sealed struct
TOKEN: 0x2000090
SIZE:  0x28
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            System.Boolean                  richTextEnabled  // 0x10
  public            DG.Tweening.ScrambleMode        scrambleMode  // 0x14
  public            System.Char[]                   scrambledChars  // 0x18
  private           System.Int32                    startValueStrippedLength  // 0x20
  private           System.Int32                    changeValueStrippedLength  // 0x24
METHODS:
  RVA=0x069822F0  token=0x600036B  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.Options.VectorOptions
TYPE:  sealed struct
TOKEN: 0x2000091
SIZE:  0x18
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            DG.Tweening.AxisConstraint      axisConstraint  // 0x10
  public            System.Boolean                  snapping  // 0x14
METHODS:
  RVA=0x04D8CAF0  token=0x600036C  System.Void Reset()
END_CLASS

