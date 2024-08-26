// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.Cursor
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeStyleSheet.UIElements
{
  /// <summary>
  ///        <para>
  /// Script interface for VisualElement cursor style property IStyle.cursor.
  /// </para>
  ///      </summary>
  public struct Cursor : IEquatable<Cursor>
  {
    /// <summary>
    ///        <para>
    /// The texture to use for the cursor style. To use a texture as a cursor, import the texture with "Read/Write enabled" in the texture importer (or using the "Cursor" defaults).
    /// </para>
    ///      </summary>
    public Texture2D texture { get; set; }

    /// <summary>
    ///        <para>
    /// The offset from the top left of the texture to use as the target point (must be within the bounds of the cursor).
    /// </para>
    ///      </summary>
    public Vector2 hotspot { get; set; }

    internal int defaultCursorId { get; set; }

    public override bool Equals(object obj) => obj is Cursor other && this.Equals(other);

    public bool Equals(Cursor other) => EqualityComparer<Texture2D>.Default.Equals(this.texture, other.texture) && this.hotspot.Equals(other.hotspot) && this.defaultCursorId == other.defaultCursorId;

    //public override int GetHashCode() => ((1500536833 * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot)) * -1521134295 + this.defaultCursorId.GetHashCode();
    public override int GetHashCode() => ((15833 * -15125 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot)) * -1521134295 + this.defaultCursorId.GetHashCode();

    internal static IEnumerable<System.Type> allowedAssetTypes
    {
      get
      {
        yield return typeof (Texture2D);
      }
    }

    public static bool operator ==(Cursor style1, Cursor style2) => style1.Equals(style2);

    public static bool operator !=(Cursor style1, Cursor style2) => !(style1 == style2);

    public override string ToString() => string.Format("texture={0}, hotspot={1}", (object) this.texture, (object) this.hotspot);
  }
}
