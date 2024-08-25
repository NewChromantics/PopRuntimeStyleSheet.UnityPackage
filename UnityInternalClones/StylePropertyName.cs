// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StylePropertyName
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
  /// <summary>
  ///        <para>
  /// Defines the name of a style property.
  /// </para>
  ///      </summary>
  public struct StylePropertyName : IEquatable<StylePropertyName>
  {
    internal StylePropertyId id { get; }

    private string name { get; }

    internal static StylePropertyId StylePropertyIdFromString(string name)
    {
      StylePropertyId stylePropertyId;
      return StylePropertyUtil.s_NameToId.TryGetValue(name, out stylePropertyId) ? stylePropertyId : StylePropertyId.Unknown;
    }

    internal StylePropertyName(StylePropertyId stylePropertyId)
    {
      this.id = stylePropertyId;
      this.name = (string) null;
      string str;
      if (!StylePropertyUtil.s_IdToName.TryGetValue(stylePropertyId, out str))
        return;
      this.name = str;
    }

    /// <summary>
    ///        <para>
    /// Initializes and returns an instance of StylePropertyName from a string.
    /// </para>
    ///      </summary>
    /// <param name="name"></param>
    public StylePropertyName(string name)
    {
      this.id = StylePropertyName.StylePropertyIdFromString(name);
      this.name = (string) null;
      if (this.id == 0)
        return;
      this.name = name;
    }

    /// <summary>
    ///        <para>
    /// Checks if the StylePropertyName is null or empty.
    /// </para>
    ///      </summary>
    /// <param name="propertyName">StylePropertyName you want to check.</param>
    /// <returns>
    ///   <para>True if propertyName is invalid. False otherwise.</para>
    /// </returns>
    public static bool IsNullOrEmpty(StylePropertyName propertyName) => propertyName.id == StylePropertyId.Unknown;

    public static bool operator ==(StylePropertyName lhs, StylePropertyName rhs) => lhs.id == rhs.id;

    public static bool operator !=(StylePropertyName lhs, StylePropertyName rhs) => lhs.id != rhs.id;

    public static implicit operator StylePropertyName(string name) => new StylePropertyName(name);

    public override int GetHashCode() => (int) this.id;

    public override bool Equals(object other) => other is StylePropertyName other1 && this.Equals(other1);

    public bool Equals(StylePropertyName other) => this == other;

    public override string ToString() => this.name;
  }
}
