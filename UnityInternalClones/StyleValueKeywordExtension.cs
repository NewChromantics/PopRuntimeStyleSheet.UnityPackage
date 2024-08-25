// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleValueKeywordExtension
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;

namespace UnityEngine.UIElements
{
  internal static class StyleValueKeywordExtension
  {
    public static string ToUssString(this StyleValueKeyword svk)
    {
      switch (svk)
      {
        case StyleValueKeyword.Inherit:
          return "inherit";
        case StyleValueKeyword.Initial:
          return "initial";
        case StyleValueKeyword.Auto:
          return "auto";
        case StyleValueKeyword.Unset:
          return "unset";
        case StyleValueKeyword.True:
          return "true";
        case StyleValueKeyword.False:
          return "false";
        case StyleValueKeyword.None:
          return "none";
        default:
          throw new ArgumentOutOfRangeException(nameof (svk), (object) svk, "Unknown StyleValueKeyword");
      }
    }
  }
}
