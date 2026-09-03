// ========================================================
// Dumped by @desirepro
// Assembly: Sirenix.OdinInspector.Attributes.dll
// Classes:  13
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

CLASS: Sirenix.OdinInspector.DontApplyToListElementsAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: Sirenix.OdinInspector.RequiredListLengthAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x28
EXTENDS: System.Attribute
FIELDS:
  private           System.Int32                    minLength  // 0x10
  private           System.Int32                    maxLength  // 0x14
  private           System.Boolean                  minLengthIsSet  // 0x18
  private           System.Boolean                  maxLengthIsSet  // 0x19
  public            System.String                   MinLengthGetter  // 0x20
PROPERTIES:
  MinLength  set=0x04DA9AC0
  MaxLength  set=0x04DA9AB0
METHODS:
  RVA=0x04DA9AA0  token=0x6000004  System.Void .ctor(System.Int32 minLength, System.Int32 maxLength)
  RVA=0x0AC46C78  token=0x6000005  System.Void .ctor(System.String minLengthGetter, System.Int32 maxLength)
END_CLASS

CLASS: Sirenix.OdinInspector.ShowInInspectorAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: Sirenix.OdinInspector.UnitAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            Sirenix.OdinInspector.Units     Base  // 0x10
  public            Sirenix.OdinInspector.Units     Display  // 0x14
METHODS:
  RVA=0x03FC4800  token=0x6000007  System.Void .ctor(Sirenix.OdinInspector.Units unit)
  RVA=0x04D88290  token=0x6000008  System.Void .ctor(Sirenix.OdinInspector.Units base, Sirenix.OdinInspector.Units display)
END_CLASS

CLASS: Sirenix.OdinInspector.Units
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Sirenix.OdinInspector.Units     Unset  // const
  public    static  Sirenix.OdinInspector.Units     Nanometer  // const
  public    static  Sirenix.OdinInspector.Units     Micrometer  // const
  public    static  Sirenix.OdinInspector.Units     Millimeter  // const
  public    static  Sirenix.OdinInspector.Units     Centimeter  // const
  public    static  Sirenix.OdinInspector.Units     Meter  // const
  public    static  Sirenix.OdinInspector.Units     Kilometer  // const
  public    static  Sirenix.OdinInspector.Units     Inch  // const
  public    static  Sirenix.OdinInspector.Units     Feet  // const
  public    static  Sirenix.OdinInspector.Units     Mile  // const
  public    static  Sirenix.OdinInspector.Units     Yard  // const
  public    static  Sirenix.OdinInspector.Units     NauticalMile  // const
  public    static  Sirenix.OdinInspector.Units     LightYear  // const
  public    static  Sirenix.OdinInspector.Units     Parsec  // const
  public    static  Sirenix.OdinInspector.Units     AstronomicalUnit  // const
  public    static  Sirenix.OdinInspector.Units     CubicMeter  // const
  public    static  Sirenix.OdinInspector.Units     CubicKilometer  // const
  public    static  Sirenix.OdinInspector.Units     CubicCentimeter  // const
  public    static  Sirenix.OdinInspector.Units     CubicMillimeter  // const
  public    static  Sirenix.OdinInspector.Units     Liter  // const
  public    static  Sirenix.OdinInspector.Units     Milliliter  // const
  public    static  Sirenix.OdinInspector.Units     Centiliter  // const
  public    static  Sirenix.OdinInspector.Units     Deciliter  // const
  public    static  Sirenix.OdinInspector.Units     Hectoliter  // const
  public    static  Sirenix.OdinInspector.Units     CubicInch  // const
  public    static  Sirenix.OdinInspector.Units     CubicFeet  // const
  public    static  Sirenix.OdinInspector.Units     CubicYard  // const
  public    static  Sirenix.OdinInspector.Units     AcreFeet  // const
  public    static  Sirenix.OdinInspector.Units     BarrelOil  // const
  public    static  Sirenix.OdinInspector.Units     TeaspoonUS  // const
  public    static  Sirenix.OdinInspector.Units     TablespoonUS  // const
  public    static  Sirenix.OdinInspector.Units     CupUS  // const
  public    static  Sirenix.OdinInspector.Units     GillUS  // const
  public    static  Sirenix.OdinInspector.Units     PintUS  // const
  public    static  Sirenix.OdinInspector.Units     QuartUS  // const
  public    static  Sirenix.OdinInspector.Units     GallonUS  // const
  public    static  Sirenix.OdinInspector.Units     BarrelUS  // const
  public    static  Sirenix.OdinInspector.Units     FluidOunceUS  // const
  public    static  Sirenix.OdinInspector.Units     BarrelUK  // const
  public    static  Sirenix.OdinInspector.Units     FluidOunceUK  // const
  public    static  Sirenix.OdinInspector.Units     TeaspoonUK  // const
  public    static  Sirenix.OdinInspector.Units     TablespoonUK  // const
  public    static  Sirenix.OdinInspector.Units     CupUK  // const
  public    static  Sirenix.OdinInspector.Units     GillUK  // const
  public    static  Sirenix.OdinInspector.Units     PintUK  // const
  public    static  Sirenix.OdinInspector.Units     QuartUK  // const
  public    static  Sirenix.OdinInspector.Units     GallonUK  // const
  public    static  Sirenix.OdinInspector.Units     SquareMeter  // const
  public    static  Sirenix.OdinInspector.Units     SquareKilometer  // const
  public    static  Sirenix.OdinInspector.Units     SquareCentimeter  // const
  public    static  Sirenix.OdinInspector.Units     SquareMillimeter  // const
  public    static  Sirenix.OdinInspector.Units     SquareMicrometer  // const
  public    static  Sirenix.OdinInspector.Units     SquareInch  // const
  public    static  Sirenix.OdinInspector.Units     SquareFeet  // const
  public    static  Sirenix.OdinInspector.Units     SquareYard  // const
  public    static  Sirenix.OdinInspector.Units     SquareMile  // const
  public    static  Sirenix.OdinInspector.Units     Hectare  // const
  public    static  Sirenix.OdinInspector.Units     Acre  // const
  public    static  Sirenix.OdinInspector.Units     Are  // const
  public    static  Sirenix.OdinInspector.Units     Joule  // const
  public    static  Sirenix.OdinInspector.Units     Kilojoule  // const
  public    static  Sirenix.OdinInspector.Units     WattHour  // const
  public    static  Sirenix.OdinInspector.Units     KilowattHour  // const
  public    static  Sirenix.OdinInspector.Units     HorsepowerHour  // const
  public    static  Sirenix.OdinInspector.Units     Newton  // const
  public    static  Sirenix.OdinInspector.Units     Kilonewton  // const
  public    static  Sirenix.OdinInspector.Units     Meganewton  // const
  public    static  Sirenix.OdinInspector.Units     Giganewton  // const
  public    static  Sirenix.OdinInspector.Units     Teranewton  // const
  public    static  Sirenix.OdinInspector.Units     Centinewton  // const
  public    static  Sirenix.OdinInspector.Units     Millinewton  // const
  public    static  Sirenix.OdinInspector.Units     JouleMeter  // const
  public    static  Sirenix.OdinInspector.Units     JouleCentimeter  // const
  public    static  Sirenix.OdinInspector.Units     GramForce  // const
  public    static  Sirenix.OdinInspector.Units     KilogramForce  // const
  public    static  Sirenix.OdinInspector.Units     TonForce  // const
  public    static  Sirenix.OdinInspector.Units     PoundForce  // const
  public    static  Sirenix.OdinInspector.Units     KilopoundForce  // const
  public    static  Sirenix.OdinInspector.Units     OunceForce  // const
  public    static  Sirenix.OdinInspector.Units     MetersPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     MetersPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     MetersPerHour  // const
  public    static  Sirenix.OdinInspector.Units     KilometersPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     KilometersPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     KilometersPerHour  // const
  public    static  Sirenix.OdinInspector.Units     CentimetersPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     CentimetersPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     CentimetersPerHour  // const
  public    static  Sirenix.OdinInspector.Units     MillimetersPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     MillimetersPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     MillimetersPerHour  // const
  public    static  Sirenix.OdinInspector.Units     FeetPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     FeetPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     FeetPerHour  // const
  public    static  Sirenix.OdinInspector.Units     YardsPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     YardsPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     YardsPerHour  // const
  public    static  Sirenix.OdinInspector.Units     MilesPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     MilesPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     MilesPerHour  // const
  public    static  Sirenix.OdinInspector.Units     Knot  // const
  public    static  Sirenix.OdinInspector.Units     KnotUK  // const
  public    static  Sirenix.OdinInspector.Units     SpeedOfLight  // const
  public    static  Sirenix.OdinInspector.Units     Bit  // const
  public    static  Sirenix.OdinInspector.Units     Kilobit  // const
  public    static  Sirenix.OdinInspector.Units     Megabit  // const
  public    static  Sirenix.OdinInspector.Units     Gigabit  // const
  public    static  Sirenix.OdinInspector.Units     Terabit  // const
  public    static  Sirenix.OdinInspector.Units     Petabit  // const
  public    static  Sirenix.OdinInspector.Units     Byte  // const
  public    static  Sirenix.OdinInspector.Units     Kilobyte  // const
  public    static  Sirenix.OdinInspector.Units     Kibibyte  // const
  public    static  Sirenix.OdinInspector.Units     Megabyte  // const
  public    static  Sirenix.OdinInspector.Units     Mebibyte  // const
  public    static  Sirenix.OdinInspector.Units     Gigabyte  // const
  public    static  Sirenix.OdinInspector.Units     Gibibyte  // const
  public    static  Sirenix.OdinInspector.Units     Terabyte  // const
  public    static  Sirenix.OdinInspector.Units     Tebibyte  // const
  public    static  Sirenix.OdinInspector.Units     Petabyte  // const
  public    static  Sirenix.OdinInspector.Units     Pebibyte  // const
  public    static  Sirenix.OdinInspector.Units     Kilogram  // const
  public    static  Sirenix.OdinInspector.Units     Hectogram  // const
  public    static  Sirenix.OdinInspector.Units     Dekagram  // const
  public    static  Sirenix.OdinInspector.Units     Gram  // const
  public    static  Sirenix.OdinInspector.Units     Decigram  // const
  public    static  Sirenix.OdinInspector.Units     Centigram  // const
  public    static  Sirenix.OdinInspector.Units     Milligram  // const
  public    static  Sirenix.OdinInspector.Units     MetricTon  // const
  public    static  Sirenix.OdinInspector.Units     Pounds  // const
  public    static  Sirenix.OdinInspector.Units     ShortTon  // const
  public    static  Sirenix.OdinInspector.Units     LongTon  // const
  public    static  Sirenix.OdinInspector.Units     Ounce  // const
  public    static  Sirenix.OdinInspector.Units     StoneUS  // const
  public    static  Sirenix.OdinInspector.Units     StoneUK  // const
  public    static  Sirenix.OdinInspector.Units     QuarterUS  // const
  public    static  Sirenix.OdinInspector.Units     QuarterUK  // const
  public    static  Sirenix.OdinInspector.Units     Slug  // const
  public    static  Sirenix.OdinInspector.Units     Grain  // const
  public    static  Sirenix.OdinInspector.Units     Celsius  // const
  public    static  Sirenix.OdinInspector.Units     Fahrenheit  // const
  public    static  Sirenix.OdinInspector.Units     Kelvin  // const
  public    static  Sirenix.OdinInspector.Units     Pascal  // const
  public    static  Sirenix.OdinInspector.Units     Decipascal  // const
  public    static  Sirenix.OdinInspector.Units     Centipascal  // const
  public    static  Sirenix.OdinInspector.Units     Millipascal  // const
  public    static  Sirenix.OdinInspector.Units     Micropascal  // const
  public    static  Sirenix.OdinInspector.Units     Kilopascal  // const
  public    static  Sirenix.OdinInspector.Units     Megapascal  // const
  public    static  Sirenix.OdinInspector.Units     Gigapascal  // const
  public    static  Sirenix.OdinInspector.Units     Bar  // const
  public    static  Sirenix.OdinInspector.Units     Millibar  // const
  public    static  Sirenix.OdinInspector.Units     Microbar  // const
  public    static  Sirenix.OdinInspector.Units     PSI  // const
  public    static  Sirenix.OdinInspector.Units     KSI  // const
  public    static  Sirenix.OdinInspector.Units     StandardAtmosphere  // const
  public    static  Sirenix.OdinInspector.Units     Watt  // const
  public    static  Sirenix.OdinInspector.Units     Kilowatt  // const
  public    static  Sirenix.OdinInspector.Units     Megawatt  // const
  public    static  Sirenix.OdinInspector.Units     Gigawatt  // const
  public    static  Sirenix.OdinInspector.Units     Terawatt  // const
  public    static  Sirenix.OdinInspector.Units     Horsepower  // const
  public    static  Sirenix.OdinInspector.Units     JouleSecond  // const
  public    static  Sirenix.OdinInspector.Units     JouleMinute  // const
  public    static  Sirenix.OdinInspector.Units     JouleHour  // const
  public    static  Sirenix.OdinInspector.Units     KilojouleSecond  // const
  public    static  Sirenix.OdinInspector.Units     KilojouleMinute  // const
  public    static  Sirenix.OdinInspector.Units     KilojouleHour  // const
  public    static  Sirenix.OdinInspector.Units     Second  // const
  public    static  Sirenix.OdinInspector.Units     Millisecond  // const
  public    static  Sirenix.OdinInspector.Units     Microsecond  // const
  public    static  Sirenix.OdinInspector.Units     Nanosecond  // const
  public    static  Sirenix.OdinInspector.Units     Minute  // const
  public    static  Sirenix.OdinInspector.Units     Hour  // const
  public    static  Sirenix.OdinInspector.Units     Day  // const
  public    static  Sirenix.OdinInspector.Units     Week  // const
  public    static  Sirenix.OdinInspector.Units     Radian  // const
  public    static  Sirenix.OdinInspector.Units     Degree  // const
  public    static  Sirenix.OdinInspector.Units     Turn  // const
  public    static  Sirenix.OdinInspector.Units     Grad  // const
  public    static  Sirenix.OdinInspector.Units     SecondsOfAngle  // const
  public    static  Sirenix.OdinInspector.Units     MinutesOfAngle  // const
  public    static  Sirenix.OdinInspector.Units     Mil  // const
  public    static  Sirenix.OdinInspector.Units     MetersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     DecimetersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     CentimetersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     MillimetersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     MicrometersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     DekametersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     HectometersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     KilometersPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     MilePerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     YardPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     FeetPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     InchPerSecondSquared  // const
  public    static  Sirenix.OdinInspector.Units     GForce  // const
  public    static  Sirenix.OdinInspector.Units     NewtonMeter  // const
  public    static  Sirenix.OdinInspector.Units     NewtonCentimeter  // const
  public    static  Sirenix.OdinInspector.Units     NewtonMillimeter  // const
  public    static  Sirenix.OdinInspector.Units     KilonewtonMeter  // const
  public    static  Sirenix.OdinInspector.Units     KilogramForceMeter  // const
  public    static  Sirenix.OdinInspector.Units     KilogramForceCentimeter  // const
  public    static  Sirenix.OdinInspector.Units     KilogramForceMillimeter  // const
  public    static  Sirenix.OdinInspector.Units     GramForceMeter  // const
  public    static  Sirenix.OdinInspector.Units     GramForceCentimeter  // const
  public    static  Sirenix.OdinInspector.Units     GramForceMillimeter  // const
  public    static  Sirenix.OdinInspector.Units     PoundFeet  // const
  public    static  Sirenix.OdinInspector.Units     PoundInch  // const
  public    static  Sirenix.OdinInspector.Units     OuncecFeet  // const
  public    static  Sirenix.OdinInspector.Units     OuncecInch  // const
  public    static  Sirenix.OdinInspector.Units     RadiansPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     RadiansPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     RadiansPerHour  // const
  public    static  Sirenix.OdinInspector.Units     RadiansPerDay  // const
  public    static  Sirenix.OdinInspector.Units     DegreesPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     DegreesPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     DegreesPerHour  // const
  public    static  Sirenix.OdinInspector.Units     DegreesPerDay  // const
  public    static  Sirenix.OdinInspector.Units     RevolutionsPerSecond  // const
  public    static  Sirenix.OdinInspector.Units     RevolutionsPerMinute  // const
  public    static  Sirenix.OdinInspector.Units     RevolutionsPerHour  // const
  public    static  Sirenix.OdinInspector.Units     RevolutionsPerDay  // const
  public    static  Sirenix.OdinInspector.Units     Hertz  // const
  public    static  Sirenix.OdinInspector.Units     Kilohertz  // const
  public    static  Sirenix.OdinInspector.Units     Megahertz  // const
  public    static  Sirenix.OdinInspector.Units     Gigahertz  // const
  public    static  Sirenix.OdinInspector.Units     PercentMultiplier  // const
  public    static  Sirenix.OdinInspector.Units     Percent  // const
  public    static  Sirenix.OdinInspector.Units     Permille  // const
  public    static  Sirenix.OdinInspector.Units     Permyriad  // const
METHODS:
END_CLASS

CLASS: Sirenix.OdinInspector.ValueDropdownList`1
TYPE:  class
TOKEN: 0x2000007
EXTENDS: System.Collections.Generic.List`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000009  System.Void Add(System.String text, T value)
  RVA=-1  // not resolved  token=0x600000A  System.Void Add(T value)
  RVA=-1  // not resolved  token=0x600000B  System.Void .ctor()
END_CLASS

CLASS: Sirenix.OdinInspector.ValueDropdownItem
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x20
FIELDS:
  public            System.String                   Text  // 0x10
  public            System.Object                   Value  // 0x18
METHODS:
  RVA=0x0AC46CA0  token=0x600000C  System.String ToString()
END_CLASS

CLASS: Sirenix.OdinInspector.ValueDropdownItem`1
TYPE:  sealed struct
TOKEN: 0x2000009
FIELDS:
  public            System.String                   Text  // 0x0
  public            T                               Value  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600000D  System.Void .ctor(System.String text, T value)
  RVA=-1  // not resolved  token=0x600000E  System.String ToString()
END_CLASS

CLASS: Sirenix.OdinInspector.IconAlignment
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Sirenix.OdinInspector.IconAlignmentLeftOfText  // const
  public    static  Sirenix.OdinInspector.IconAlignmentRightOfText  // const
  public    static  Sirenix.OdinInspector.IconAlignmentLeftEdge  // const
  public    static  Sirenix.OdinInspector.IconAlignmentRightEdge  // const
METHODS:
END_CLASS

CLASS: Sirenix.OdinInspector.IncludeMyAttributesAttribute
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600000F  System.Void .ctor()
END_CLASS

CLASS: Sirenix.OdinInspector.ISearchFilterable
TYPE:  interface
TOKEN: 0x200000C
FIELDS:
METHODS:
END_CLASS

CLASS: Sirenix.OdinInspector.SdfIconType
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Sirenix.OdinInspector.SdfIconTypeNone  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlarmFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlarm  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlignBottom  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlignCenter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlignEnd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlignMiddle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlignStart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlignTop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAppIndicator  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeApp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArchiveFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArchive  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrow90degDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrow90degLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrow90degRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrow90degUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowBarDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowBarLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowBarRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowBarUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowClockwise  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowCounterclockwise  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownLeftCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownLeftCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownLeftSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownLeftSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownRightCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownRightCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownRightSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownRightSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownShort  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDownUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowLeftCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowLeftCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowLeftRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowLeftShort  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowLeftSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowLeftSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowRepeat  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowReturnLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowReturnRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowRightCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowRightCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowRightShort  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowRightSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowRightSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpLeftCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpLeftCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpLeftSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpLeftSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpRightCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpRightCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpRightSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpRightSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpShort  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUpSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowsAngleContract  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowsAngleExpand  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowsCollapse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowsExpand  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowsFullscreen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeArrowsMove  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAspectRatioFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAspectRatio  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAsterisk  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAwardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeAward  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBack  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBackspaceFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBackspaceReverseFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBackspaceReverse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBackspace  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadge3dFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadge3d  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadge4kFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadge4k  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadge8kFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadge8k  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeAdFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeAd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeArFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeAr  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeCcFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeCc  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeHdFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeHd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeTmFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeTm  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeVoFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeVo  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeVrFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeVr  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeWcFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBadgeWc  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagDashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBagX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBag  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBarChartFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBarChartLineFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBarChartLine  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBarChartSteps  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBarChart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBasketFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBasket  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBasket2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBasket2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBasket3Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBasket3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBatteryCharging  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBatteryFull  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBatteryHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBattery  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBellFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBell  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBezier  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBezier2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBicycle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBinocularsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBinoculars  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBlockquoteLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBlockquoteRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBook  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkDashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkHeartFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkHeart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkStarFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkStar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarkX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmark  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarksFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookmarks  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBookshelf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBootstrapFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBootstrapReboot  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBootstrap  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderAll  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderBottom  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderCenter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderInner  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderMiddle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderOuter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderStyle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderTop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorderWidth  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBorder  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoundingBoxCircles  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoundingBox  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowDownLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowDownRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInDownLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInDownRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInUpLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInUpRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowInUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowUpLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowUpRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxArrowUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxSeam  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBox  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBraces  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBricks  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBriefcaseFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBriefcase  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessAltHighFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessAltHigh  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessAltLowFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessAltLow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessHighFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessHigh  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessLowFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrightnessLow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBroadcastPin  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBroadcast  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrushFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBrush  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBucketFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBucket  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBugFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBug  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBuilding  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBullseye  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalculatorFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalculator  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarDateFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarDate  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarDayFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarDay  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarEventFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarEvent  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarMinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarMonthFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarMonth  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarRangeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarRange  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarWeekFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarWeek  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendarX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2CheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Check  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2DateFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Date  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2DayFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Day  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2EventFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Event  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2MinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Minus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2MonthFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Month  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2PlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Plus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2RangeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Range  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2WeekFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2Week  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2XFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2X  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3EventFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3Event  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3RangeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3Range  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3WeekFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3Week  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar4Event  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar4Range  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar4Week  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCalendar4  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCameraFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCameraReelsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCameraReels  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCameraVideoFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCameraVideoOffFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCameraVideoOff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCameraVideo  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCamera  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCamera2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCapslockFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCapslock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCardChecklist  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCardHeading  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCardImage  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCardList  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCardText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretDownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretDownSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretDownSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretLeftFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretLeftSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretLeftSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretRightFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretRightSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretRightSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretUpFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretUpSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretUpSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCaretUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartDashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCartX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCart2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCart3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCart4  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCashStack  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCast  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatDotsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatDots  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeftDotsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeftDots  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeftFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeftQuoteFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeftQuote  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeftTextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeftText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatQuoteFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatQuote  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRightDotsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRightDots  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRightFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRightQuoteFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRightQuote  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRightTextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRightText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquareDotsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquareDots  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquareQuoteFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquareQuote  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquareTextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquareText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatTextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChatText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChat  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheckAll  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheckCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheckCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheckSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheckSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheck2All  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheck2Circle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheck2Square  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheck2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronBarContract  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronBarDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronBarExpand  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronBarLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronBarRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronBarUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronCompactDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronCompactLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronCompactRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronCompactUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronContract  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronDoubleDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronDoubleLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronDoubleRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronDoubleUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronExpand  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeChevronUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCircleHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCircleSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClipboardCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClipboardData  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClipboardMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClipboardPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClipboardX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClipboard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClockFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClockHistory  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudArrowDownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudArrowDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudArrowUpFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudArrowUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudDownloadFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudDownload  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudDrizzleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudDrizzle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudFogFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudFog  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudFog2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudFog2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudHailFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudHail  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudHaze1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudHazeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudHaze  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudHaze2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudLightningFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudLightningRainFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudLightningRain  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudLightning  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudMinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudMoonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudMoon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudRainFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudRainHeavyFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudRainHeavy  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudRain  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSlashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSleetFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSleet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSnowFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSnow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSunFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudSun  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudUploadFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudUpload  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloud  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeClouds  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudyFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudy  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCodeSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCodeSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCode  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCollectionFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCollectionPlayFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCollectionPlay  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCollection  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeColumnsGap  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeColumns  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCommand  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCompassFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCompass  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeConeStriped  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCone  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeController  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCpuFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCpu  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCreditCard2BackFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCreditCard2Back  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCreditCard2FrontFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCreditCard2Front  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCreditCardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCreditCard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCrop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCupFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCupStraw  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCup  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCursorFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCursorText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCursor  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDashCircleDotted  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDashCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDashCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDashSquareDotted  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDashSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDashSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiagram2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiagram2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiagram3Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiagram3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiamondFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiamondHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiamond  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice1Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice3Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice4Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice4  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice5Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice5  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice6Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDice6  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiscFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDisc  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDiscord  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDisplayFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDisplay  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDistributeHorizontal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDistributeVertical  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDoorClosedFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDoorClosed  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDoorOpenFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDoorOpen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDot  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDownload  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDropletFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDropletHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDroplet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEarbuds  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEaselFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEasel  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEggFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEggFried  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEgg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEjectFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEject  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiAngryFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiAngry  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiDizzyFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiDizzy  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiExpressionlessFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiExpressionless  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiFrownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiFrown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiHeartEyesFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiHeartEyes  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiLaughingFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiLaughing  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiNeutralFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiNeutral  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiSmileFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiSmileUpsideDownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiSmileUpsideDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiSmile  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiSunglassesFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiSunglasses  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiWinkFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEmojiWink  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeOpenFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeOpen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelope  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEraserFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEraser  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationDiamondFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationDiamond  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationOctagonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationOctagon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationTriangleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationTriangle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamation  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclude  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEyeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEyeSlashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEyeSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEye  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEyedropper  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEyeglasses  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFacebook  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileArrowDownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileArrowDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileArrowUpFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileArrowUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileBarGraphFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileBarGraph  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileBinaryFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileBinary  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileBreakFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileBreak  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileCodeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileCode  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileDiffFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileDiff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkArrowDownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkArrowDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkArrowUpFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkArrowUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkBarGraphFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkBarGraph  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkBinaryFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkBinary  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkBreakFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkBreak  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkCodeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkCode  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkDiffFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkDiff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkEaselFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkEasel  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkExcelFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkExcel  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkFontFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkFont  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkImageFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkImage  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkLockFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkLock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkLock2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkLock2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkMedicalFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkMedical  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkMinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkMusicFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkMusic  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPersonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPerson  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPlayFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPlay  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPostFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPost  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPptFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPpt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkRichtextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkRichtext  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkRuledFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkRuled  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkSlidesFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkSlides  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkSpreadsheetFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkSpreadsheet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkTextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkWordFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkWord  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkZipFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkZip  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmark  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEaselFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEasel  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileExcelFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileExcel  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileFontFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileFont  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileImageFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileImage  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileLockFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileLock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileLock2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileLock2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileMedicalFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileMedical  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileMinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileMusicFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileMusic  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePersonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePerson  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePlayFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePlay  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePostFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePost  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePptFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePpt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileRichtextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileRichtext  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileRuledFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileRuled  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileSlidesFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileSlides  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileSpreadsheetFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileSpreadsheet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileTextFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileWordFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileWord  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileZipFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileZip  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFile  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilesAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFiles  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilm  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilterCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilterCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilterLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilterRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilterSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilterSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFlagFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFlag  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFlower1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFlower2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFlower3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolderCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolderFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolderMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolderPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolderSymlinkFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolderSymlink  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolderX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolder  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolder2Open  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFolder2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFonts  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeForwardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeForward  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFront  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFullscreenExit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFullscreen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFunnelFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFunnel  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGearFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGearWideConnected  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGearWide  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGear  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGem  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGeoAltFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGeoAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGeoFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGeo  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGiftFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGift  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGithub  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGlobe  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGlobe2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGoogle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGraphDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGraphUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid1x2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid1x2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid3x2GapFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid3x2Gap  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid3x2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid3x3GapFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid3x3Gap  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid3x3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGridFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGrid  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGripHorizontal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGripVertical  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHammer  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandIndexFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandIndexThumbFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandIndexThumb  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandIndex  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandThumbsDownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandThumbsDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandThumbsUpFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandThumbsUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandbagFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHandbag  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHddFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHddNetworkFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHddNetwork  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHddRackFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHddRack  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHddStackFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHddStack  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHdd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeadphones  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeadset  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeartFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeartHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeptagonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeptagonHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeptagon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHexagonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHexagonHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHexagon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHourglassBottom  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHourglassSplit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHourglassTop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHourglass  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHouseDoorFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHouseDoor  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHouseFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHouse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHr  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHurricane  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeImageAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeImageFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeImage  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeImages  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInboxFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInbox  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInboxesFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInboxes  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInfoCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInfoCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInfoSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInfoSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInfo  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInputCursorText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInputCursor  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInstagram  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeIntersect  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalAlbum  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalArrowDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalArrowUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalBookmarkFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalBookmark  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalCode  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalMedical  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalRichtext  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournalX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJournals  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJoystick  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJustifyLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJustifyRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeJustify  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeKanbanFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeKanban  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeKeyFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeKey  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeKeyboardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeKeyboard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLadder  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLampFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLamp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLaptopFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLaptop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayerBackward  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayerForward  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayersFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayersHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayers  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutSidebarInsetReverse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutSidebarInset  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutSidebarReverse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutSidebar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutSplit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutTextSidebarReverse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutTextSidebar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutTextWindowReverse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutTextWindow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutThreeColumns  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLayoutWtf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLifePreserver  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightbulbFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightbulbOffFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightbulbOff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightbulb  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightningChargeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightningCharge  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightningFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLightning  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLink45deg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLink  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLinkedin  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListNested  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListOl  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListStars  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListTask  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListUl  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeList  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLockFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMailbox  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMailbox2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMapFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMap  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMarkdownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMarkdown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMask  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMegaphoneFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMegaphone  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuAppFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuApp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuButtonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuButtonWideFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuButtonWide  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuButton  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMenuUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMicFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMicMuteFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMicMute  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMic  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMinecartLoaded  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMinecart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMoisture  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMoonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMoonStarsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMoonStars  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMoon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMouseFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMouse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMouse2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMouse2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMouse3Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMouse3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMusicNoteBeamed  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMusicNoteList  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMusicNote  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMusicPlayerFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMusicPlayer  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNewspaper  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNodeMinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNodeMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNodePlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNodePlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNutFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNut  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeOctagonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeOctagonHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeOctagon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeOption  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeOutlet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePaintBucket  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePaletteFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePalette  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePalette2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePaperclip  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeParagraph  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchExclamationFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchExclamation  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchMinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchQuestionFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePatchQuestion  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePauseBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePauseBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePauseCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePauseCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePauseFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePause  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePeaceFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePeace  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePenFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePencilFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePencilSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePencil  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePentagonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePentagonHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePentagon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePeopleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePeople  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePercent  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonBadgeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonBadge  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonBoundingBox  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonDashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonLinesFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePerson  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePhoneFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePhoneLandscapeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePhoneLandscape  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePhoneVibrateFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePhoneVibrate  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePhone  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePieChartFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePieChart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePinAngleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePinAngle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePinFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePin  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePipFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePip  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlayBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlayBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlayCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlayCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlayFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlay  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlugFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlug  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusCircleDotted  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusSquareDotted  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePower  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePrinterFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePrinter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePuzzleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePuzzle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionDiamondFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionDiamond  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionOctagonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionOctagon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestion  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRainbow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReceiptCutoff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReceipt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReception0  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReception1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReception2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReception3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReception4  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecordBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecordBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecordCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecordCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecordFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecord  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecord2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecord2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReplyAllFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReplyAll  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReplyFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReply  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRssFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRss  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRulers  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSaveFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSave  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSave2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSave2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeScissors  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeScrewdriver  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSearch  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSegmentedNav  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeServer  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldExclamation  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldFillCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldFillExclamation  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldFillMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldFillPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldFillX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldLockFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldLock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldShaded  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldSlashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShieldX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShield  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShiftFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShift  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShopWindow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeShuffle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSignpost2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSignpost2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSignpostFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSignpostSplitFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSignpostSplit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSignpost  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSimFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSim  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipBackwardBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipBackwardBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipBackwardCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipBackwardCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipBackwardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipBackward  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipEndBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipEndBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipEndCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipEndCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipEndFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipEnd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipForwardBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipForwardBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipForwardCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipForwardCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipForwardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipForward  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipStartBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipStartBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipStartCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipStartCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipStartFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkipStart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSlack  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSlashCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSlashCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSlashSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSlashSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSliders  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSmartwatch  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSnow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSnow2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSnow3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortAlphaDownAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortAlphaDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortAlphaUpAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortAlphaUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortDownAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortNumericDownAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortNumericDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortNumericUpAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortNumericUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortUpAlt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSortUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSoundwave  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSpeakerFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSpeaker  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSpeedometer  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSpeedometer2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSpellcheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSquareHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStack  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStarFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStarHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStars  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStickiesFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStickies  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStickyFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSticky  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStopBtnFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStopBtn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStopCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStopCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStopFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStoplightsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStoplights  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStopwatchFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStopwatch  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSubtract  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitClubFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitClub  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitDiamondFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitDiamond  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitHeartFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitHeart  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitSpadeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSuitSpade  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSunFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSun  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSunglasses  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSunriseFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSunrise  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSunsetFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSunset  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSymmetryHorizontal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSymmetryVertical  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTable  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTabletFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTabletLandscapeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTabletLandscape  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTablet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTagFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTag  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTagsFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTags  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelegram  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneForwardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneForward  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneInboundFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneInbound  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneMinusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneOutboundFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneOutbound  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephonePlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephonePlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephoneX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTelephone  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTerminalFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTerminal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextCenter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextIndentLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextIndentRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextLeft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextParagraph  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextRight  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextareaResize  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextareaT  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTextarea  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThermometerHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThermometerHigh  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThermometerLow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThermometerSnow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThermometerSun  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThermometer  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThreeDotsVertical  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThreeDots  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeToggleOff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeToggleOn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeToggle2Off  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeToggle2On  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeToggles  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeToggles2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTools  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTornado  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTrashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTrash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTrash2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTrash2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTreeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTree  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTriangleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTriangleHalf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTriangle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTrophyFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTrophy  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTropicalStorm  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTruckFlatbed  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTruck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTsunami  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTvFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTv  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTwitch  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTwitter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTypeBold  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTypeH1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTypeH2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTypeH3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTypeItalic  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTypeStrikethrough  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTypeUnderline  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeType  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUiChecksGrid  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUiChecks  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUiRadiosGrid  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUiRadios  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUmbrellaFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUmbrella  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUnion  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUnlockFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUnlock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUpcScan  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUpc  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUpload  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVectorPen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeViewList  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeViewStacked  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVinylFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVinyl  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVoicemail  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeDownFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeDown  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeMuteFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeMute  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeOffFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeOff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeUpFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVolumeUp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVr  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWalletFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWallet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWallet2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWatch  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWater  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWhatsapp  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWifi1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWifi2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWifiOff  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWifi  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWind  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowDock  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowSidebar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWrench  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXCircleFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXCircle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXDiamondFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXDiamond  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXOctagonFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXOctagon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXSquareFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXSquare  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeYoutube  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeZoomIn  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeZoomOut  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBank  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBank2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBellSlashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBellSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCashCoin  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCheckLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCoin  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCurrencyBitcoin  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCurrencyDollar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCurrencyEuro  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCurrencyExchange  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCurrencyPound  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCurrencyYen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDashLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExclamationLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPdfFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFileEarmarkPdf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePdfFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFilePdf  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGenderAmbiguous  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGenderFemale  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGenderMale  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGenderTrans  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHeadsetVr  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInfoLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMastodon  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMessenger  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePiggyBankFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePiggyBank  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePinMapFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePinMap  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuestionLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRecycle  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeReddit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSafeFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSafe2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSafe2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSdCardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSdCard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSkype  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSlashLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTranslate  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXLg  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSafe  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeApple  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMicrosoft  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindows  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBehance  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDribbble  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeLine  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMedium  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePaypal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePinterest  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSignal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSnapchat  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSpotify  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStackOverflow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeStrava  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWordpress  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeVimeo  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeActivity  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEasel2Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEasel2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEasel3Fill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEasel3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFan  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeFingerprint  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGraphDownArrow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGraphUpArrow  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHypnotize  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMagic  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonRolodex  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonVideo  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonVideo2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonVideo3  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePersonWorkspace  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRadioactive  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWebcamFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWebcam  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeYinYang  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBandaidFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBandaid  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBluetooth  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBodyText  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoombox  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoxes  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDpadFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDpad  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEarFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEar  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeCheck1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeDash1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeDashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeExclamation1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeExclamationFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeExclamation  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopePlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopePlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeSlash1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeSlashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeX1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEnvelopeX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExplicitFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeExplicit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeInfinity  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListColumnsReverse  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeListColumns  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMeta  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMortorboardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMortorboard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeNintendoSwitch  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePcDisplayHorizontal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePcDisplay  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePcHorizontal  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePc  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlaystation  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePlusSlashMinus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeProjectorFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeProjector  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQrCodeScan  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQrCode  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuora  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeQuote  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRobot  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendCheckFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendCheck  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendDashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendExclamation1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendExclamationFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendExclamation  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendPlusFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendSlashFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendSlash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendXFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSendX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSend  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSteam  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTerminalDash1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTerminalDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTerminalPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTerminalSplit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTicketDetailedFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTicketDetailed  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTicketFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTicketPerforatedFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTicketPerforated  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTicket  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTiktok  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowDash  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowDesktop  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowFullscreen  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowPlus  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowSplit  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowStack  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeWindowX  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeXbox  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeEthernet  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHdmiFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeHdmi  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbCFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbC  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbPlugFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbPlug  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbSymbol  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsb  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeBoomboxFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDisplayport1  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDisplayport  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeGpuCard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMemory  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeModemFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeModem  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMotherboardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMotherboard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeOpticalAudioFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeOpticalAudio  // const
  public    static  Sirenix.OdinInspector.SdfIconTypePciCard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRouterFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeRouter  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSsdFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeSsd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThunderboltFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeThunderbolt  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbDriveFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbDrive  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbMicroFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbMicro  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbMiniFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeUsbMini  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeCloudHaze2  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDeviceHddFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDeviceHdd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDeviceSsdFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDeviceSsd  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeDisplayportFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMortarboardFill  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeMortarboard  // const
  public    static  Sirenix.OdinInspector.SdfIconTypeTerminalX  // const
METHODS:
END_CLASS

