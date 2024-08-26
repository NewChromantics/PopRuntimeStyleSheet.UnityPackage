// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleValueFunctionExtension
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using System;

namespace RuntimeStyleSheet.UIElements
{
  internal static class StyleValueFunctionExtension
  {
    public const string k_Var = "var";
    public const string k_Env = "env";
    public const string k_LinearGradient = "linear-gradient";

    public static StyleValueFunction FromUssString(string ussValue)
    {
      ussValue = ussValue.ToLower();
      string str = ussValue;
      if (str == "var")
        return StyleValueFunction.Var;
      if (str == "env")
        return StyleValueFunction.Env;
      if (str == "linear-gradient")
        return StyleValueFunction.LinearGradient;
      throw new ArgumentOutOfRangeException(nameof (ussValue), (object) ussValue, "Unknown function name");
    }

    public static string ToUssString(this StyleValueFunction svf)
    {
      switch (svf)
      {
        case StyleValueFunction.Var:
          return "var";
        case StyleValueFunction.Env:
          return "env";
        case StyleValueFunction.LinearGradient:
          return "linear-gradient";
        default:
          throw new ArgumentOutOfRangeException(nameof (svf), (object) svf, "Unknown StyleValueFunction");
      }
    }
  }
}
