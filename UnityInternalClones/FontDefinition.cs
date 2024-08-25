// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.FontDefinition
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
  /// <summary>
  ///        <para>
  /// Describes a VisualElement font.
  /// </para>
  ///      </summary>
  public struct FontDefinition : IEquatable<FontDefinition>
  {
    private Font m_Font;
    private FontAsset m_FontAsset;

    /// <summary>
    ///        <para>
    /// Font to use to display text. You cannot set this and FontDefinition.fontAsset at the same time.
    /// </para>
    ///      </summary>
    public Font font
    {
      get => this.m_Font;
      set => this.m_Font = !((UnityEngine.Object) value != (UnityEngine.Object) null) || !((UnityEngine.Object) this.fontAsset != (UnityEngine.Object) null) ? value : throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
    }

    /// <summary>
    ///        <para>
    /// SDF font to use to display text. You cannot set this and FontDefinition.font at the same time.
    /// </para>
    ///      </summary>
    public FontAsset fontAsset
    {
      get => this.m_FontAsset;
      set => this.m_FontAsset = !((UnityEngine.Object) value != (UnityEngine.Object) null) || !((UnityEngine.Object) this.font != (UnityEngine.Object) null) ? value : throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
    }

    /// <summary>
    ///        <para>
    /// Create a FontDefinition from Font.
    /// </para>
    ///      </summary>
    /// <param name="f">The font to use to display text.</param>
    /// <returns>
    ///   <para>A new FontDefinition object.</para>
    /// </returns>
    public static FontDefinition FromFont(Font f) => new FontDefinition()
    {
      m_Font = f
    };

    /// <summary>
    ///        <para>
    /// Create a FontDefinition from FontAsset.
    /// </para>
    ///      </summary>
    /// <param name="f">The SDF font to use to display text.</param>
    /// <returns>
    ///   <para>A new FontDefinition object.</para>
    /// </returns>
    public static FontDefinition FromSDFFont(FontAsset f) => new FontDefinition()
    {
      m_FontAsset = f
    };

    internal static FontDefinition FromObject(object obj)
    {
      Font f1 = obj as Font;
      if ((UnityEngine.Object) f1 != (UnityEngine.Object) null)
        return FontDefinition.FromFont(f1);
      FontAsset f2 = obj as FontAsset;
      return (UnityEngine.Object) f2 != (UnityEngine.Object) null ? FontDefinition.FromSDFFont(f2) : new FontDefinition();
    }

    internal static IEnumerable<System.Type> allowedAssetTypes
    {
      get
      {
        yield return typeof (Font);
        yield return typeof (FontAsset);
      }
    }

    internal bool IsEmpty() => (UnityEngine.Object) this.m_Font == (UnityEngine.Object) null && (UnityEngine.Object) this.m_FontAsset == (UnityEngine.Object) null;

    public override string ToString() => (UnityEngine.Object) this.font != (UnityEngine.Object) null ? string.Format("{0}", (object) this.font) : string.Format("{0}", (object) this.fontAsset);

    public bool Equals(FontDefinition other) => object.Equals((object) this.m_Font, (object) other.m_Font) && object.Equals((object) this.m_FontAsset, (object) other.m_FontAsset);

    public override bool Equals(object obj) => obj is FontDefinition other && this.Equals(other);

    public override int GetHashCode() => ((UnityEngine.Object) this.m_Font != (UnityEngine.Object) null ? this.m_Font.GetHashCode() : 0) * 397 ^ ((UnityEngine.Object) this.m_FontAsset != (UnityEngine.Object) null ? this.m_FontAsset.GetHashCode() : 0);

    public static bool operator ==(FontDefinition left, FontDefinition right) => left.Equals(right);

    public static bool operator !=(FontDefinition left, FontDefinition right) => !left.Equals(right);
  }
}
