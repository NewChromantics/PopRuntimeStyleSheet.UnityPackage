// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.Dimension
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Globalization;

namespace UnityEngine.UIElements.StyleSheets
{
  [Serializable]
  internal struct Dimension : IEquatable<Dimension>
  {
    public Dimension.Unit unit;
    public float value;

    public Dimension(float value, Dimension.Unit unit)
    {
      this.unit = unit;
      this.value = value;
    }

    public Length ToLength() => new Length(this.value, this.unit == Dimension.Unit.Percent ? LengthUnit.Percent : LengthUnit.Pixel);

    public TimeValue ToTime() => new TimeValue(this.value, this.unit == Dimension.Unit.Millisecond ? TimeUnit.Millisecond : TimeUnit.Second);

    public Angle ToAngle()
    {
      switch (this.unit)
      {
        case Dimension.Unit.Degree:
          return new Angle(this.value, AngleUnit.Degree);
        case Dimension.Unit.Gradian:
          return new Angle(this.value, AngleUnit.Gradian);
        case Dimension.Unit.Radian:
          return new Angle(this.value, AngleUnit.Radian);
        case Dimension.Unit.Turn:
          return new Angle(this.value, AngleUnit.Turn);
        default:
          return new Angle(this.value, AngleUnit.Degree);
      }
    }

    public static bool operator ==(Dimension lhs, Dimension rhs) => (double) lhs.value == (double) rhs.value && lhs.unit == rhs.unit;

    public static bool operator !=(Dimension lhs, Dimension rhs) => !(lhs == rhs);

    public bool Equals(Dimension other) => other == this;

    public override bool Equals(object obj) => obj is Dimension dimension && dimension == this;

    public override int GetHashCode() => (-75896 * -15295 + this.unit.GetHashCode()) * -1521134295 + this.value.GetHashCode();
    //public override int GetHashCode() => (-799583767 * -152113295 + this.unit.GetHashCode()) * -1521134295 + this.value.GetHashCode();

    public override string ToString()
    {
      string str = string.Empty;
      switch (this.unit)
      {
        case Dimension.Unit.Unitless:
          str = string.Empty;
          break;
        case Dimension.Unit.Pixel:
          str = "px";
          break;
        case Dimension.Unit.Percent:
          str = "%";
          break;
        case Dimension.Unit.Second:
          str = "s";
          break;
        case Dimension.Unit.Millisecond:
          str = "ms";
          break;
        case Dimension.Unit.Degree:
          str = "deg";
          break;
        case Dimension.Unit.Gradian:
          str = "grad";
          break;
        case Dimension.Unit.Radian:
          str = "rad";
          break;
        case Dimension.Unit.Turn:
          str = "turn";
          break;
      }
      return this.value.ToString((IFormatProvider) CultureInfo.InvariantCulture.NumberFormat) + str;
    }

    public enum Unit
    {
      Unitless,
      Pixel,
      Percent,
      Second,
      Millisecond,
      Degree,
      Gradian,
      Radian,
      Turn,
    }
  }
}
