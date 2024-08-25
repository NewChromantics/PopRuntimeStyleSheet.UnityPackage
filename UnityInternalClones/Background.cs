// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.Background
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
  /// <summary>
  ///        <para>
  /// Describes a VisualElement background.
  /// </para>
  ///      </summary>
  public struct Background : IEquatable<Background>
  {
    private Texture2D m_Texture;
    private Sprite m_Sprite;
    private RenderTexture m_RenderTexture;
    private VectorImage m_VectorImage;

    /// <summary>
    ///        <para>
    /// The texture to display as a background.
    /// </para>
    ///      </summary>
    public Texture2D texture
    {
      get => this.m_Texture;
      set
      {
        if ((UnityEngine.Object) this.m_Texture == (UnityEngine.Object) value)
          return;
        this.m_Texture = value;
        this.m_Sprite = (Sprite) null;
        this.m_RenderTexture = (RenderTexture) null;
        this.m_VectorImage = (VectorImage) null;
      }
    }

    /// <summary>
    ///        <para>
    /// The sprite to display as a background.
    /// </para>
    ///      </summary>
    public Sprite sprite
    {
      get => this.m_Sprite;
      set
      {
        if ((UnityEngine.Object) this.m_Sprite == (UnityEngine.Object) value)
          return;
        this.m_Texture = (Texture2D) null;
        this.m_Sprite = value;
        this.m_RenderTexture = (RenderTexture) null;
        this.m_VectorImage = (VectorImage) null;
      }
    }

    /// <summary>
    ///        <para>
    /// The RenderTexture to display as a background.
    /// </para>
    ///      </summary>
    public RenderTexture renderTexture
    {
      get => this.m_RenderTexture;
      set
      {
        if ((UnityEngine.Object) this.m_RenderTexture == (UnityEngine.Object) value)
          return;
        this.m_Texture = (Texture2D) null;
        this.m_Sprite = (Sprite) null;
        this.m_RenderTexture = value;
        this.m_VectorImage = (VectorImage) null;
      }
    }

    /// <summary>
    ///        <para>
    /// The VectorImage to display as a background.
    /// </para>
    ///      </summary>
    public VectorImage vectorImage
    {
      get => this.m_VectorImage;
      set
      {
        if ((UnityEngine.Object) this.vectorImage == (UnityEngine.Object) value)
          return;
        this.m_Texture = (Texture2D) null;
        this.m_Sprite = (Sprite) null;
        this.m_RenderTexture = (RenderTexture) null;
        this.m_VectorImage = value;
      }
    }

    /// <summary>
    ///        <para>
    /// Creates from a Texture2D.
    /// </para>
    ///      </summary>
    /// <param name="t"></param>
    [Obsolete("Use Background.FromTexture2D instead")]
    public Background(Texture2D t)
    {
      this.m_Texture = t;
      this.m_Sprite = (Sprite) null;
      this.m_RenderTexture = (RenderTexture) null;
      this.m_VectorImage = (VectorImage) null;
    }

    /// <summary>
    ///        <para>
    /// Creates a background from a Texture2D.
    /// </para>
    ///      </summary>
    /// <param name="t">The texture to use as a background.</param>
    /// <returns>
    ///   <para>A new background object.</para>
    /// </returns>
    public static Background FromTexture2D(Texture2D t) => new Background()
    {
      texture = t
    };

    /// <summary>
    ///        <para>
    /// Creates a background from a RenderTexture.
    /// </para>
    ///      </summary>
    /// <param name="rt">The render texture to use as a background.</param>
    /// <returns>
    ///   <para>A new background object.</para>
    /// </returns>
    public static Background FromRenderTexture(RenderTexture rt) => new Background()
    {
      renderTexture = rt
    };

    /// <summary>
    ///        <para>
    /// Creates a background from a Sprite.
    /// </para>
    ///      </summary>
    /// <param name="s">The sprite to use as a background.</param>
    /// <returns>
    ///   <para>A new background object.</para>
    /// </returns>
    public static Background FromSprite(Sprite s) => new Background()
    {
      sprite = s
    };

    /// <summary>
    ///        <para>
    /// Creates a background from a VectorImage.
    /// </para>
    ///      </summary>
    /// <param name="vi">The vector image to use as a background.</param>
    /// <returns>
    ///   <para>A new background object.</para>
    /// </returns>
    public static Background FromVectorImage(VectorImage vi) => new Background()
    {
      vectorImage = vi
    };

    internal static Background FromObject(object obj)
    {
      Texture2D t = obj as Texture2D;
      if ((UnityEngine.Object) t != (UnityEngine.Object) null)
        return Background.FromTexture2D(t);
      RenderTexture rt = obj as RenderTexture;
      if ((UnityEngine.Object) rt != (UnityEngine.Object) null)
        return Background.FromRenderTexture(rt);
      Sprite s = obj as Sprite;
      if ((UnityEngine.Object) s != (UnityEngine.Object) null)
        return Background.FromSprite(s);
      VectorImage vi = obj as VectorImage;
      return (UnityEngine.Object) vi != (UnityEngine.Object) null ? Background.FromVectorImage(vi) : new Background();
    }

    internal static IEnumerable<System.Type> allowedAssetTypes
    {
      get
      {
        yield return typeof (Texture2D);
        yield return typeof (RenderTexture);
        yield return typeof (Sprite);
        yield return typeof (VectorImage);
      }
    }

    public static bool operator ==(Background lhs, Background rhs) => (UnityEngine.Object) lhs.texture == (UnityEngine.Object) rhs.texture && (UnityEngine.Object) lhs.sprite == (UnityEngine.Object) rhs.sprite && (UnityEngine.Object) lhs.renderTexture == (UnityEngine.Object) rhs.renderTexture && (UnityEngine.Object) lhs.vectorImage == (UnityEngine.Object) rhs.vectorImage;

    public static bool operator !=(Background lhs, Background rhs) => !(lhs == rhs);

    public static implicit operator Background(Texture2D v) => Background.FromTexture2D(v);

    public bool Equals(Background other) => other == this;

    public override bool Equals(object obj) => obj is Background background && background == this;

    public override int GetHashCode()
    {
      int hashCode = 851985039;
      if (this.texture != null)
        hashCode = hashCode * -1521134295 + this.texture.GetHashCode();
      if (this.sprite != null)
        hashCode = hashCode * -1521134295 + this.sprite.GetHashCode();
      if (this.renderTexture != null)
        hashCode = hashCode * -1521134295 + this.renderTexture.GetHashCode();
      if (this.vectorImage != null)
        hashCode = hashCode * -1521134295 + this.vectorImage.GetHashCode();
      return hashCode;
    }

    public override string ToString()
    {
      if ((UnityEngine.Object) this.texture != (UnityEngine.Object) null)
        return this.texture.ToString();
      if ((UnityEngine.Object) this.sprite != (UnityEngine.Object) null)
        return this.sprite.ToString();
      if ((UnityEngine.Object) this.renderTexture != (UnityEngine.Object) null)
        return this.renderTexture.ToString();
      return (UnityEngine.Object) this.vectorImage != (UnityEngine.Object) null ? this.vectorImage.ToString() : "";
    }
  }
}
