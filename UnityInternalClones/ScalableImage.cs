﻿// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.ScalableImage
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;

namespace UnityEngine.UIElements.StyleSheets
{
  [Serializable]
  internal struct ScalableImage
  {
    public Texture2D normalImage;
    public Texture2D highResolutionImage;

    public override string ToString() => string.Format("{0}: {1}, {2}: {3}", (object) "normalImage", (object) this.normalImage, (object) "highResolutionImage", (object) this.highResolutionImage);
  }
}
