// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.StyleSheetExtensions
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Globalization;



namespace UnityEngine.UIElements.StyleSheets
{
  internal static class StyleSheetExtensions
  {
    public static string ReadAsString(this StyleSheet sheet, StyleValueHandle handle)
    {
      string empty = string.Empty;
      string str;
      switch (handle.valueType)
      {
        case StyleValueType.Keyword:
          str = sheet.ReadKeyword(handle).ToUssString();
          break;
        case StyleValueType.Float:
          str = sheet.ReadFloat(handle).ToString((IFormatProvider) CultureInfo.InvariantCulture.NumberFormat);
          break;
        case StyleValueType.Dimension:
          str = sheet.ReadDimension(handle).ToString();
          break;
        case StyleValueType.Color:
          str = sheet.ReadColor(handle).ToString();
          break;
        case StyleValueType.ResourcePath:
          str = sheet.ReadResourcePath(handle);
          break;
        case StyleValueType.AssetReference:
          str = sheet.ReadAssetReference(handle).ToString();
          break;
        case StyleValueType.Enum:
          str = sheet.ReadEnum(handle);
          break;
        case StyleValueType.Variable:
          str = sheet.ReadVariable(handle);
          break;
        case StyleValueType.String:
          str = sheet.ReadString(handle);
          break;
        case StyleValueType.Function:
          str = sheet.ReadFunctionName(handle);
          break;
        case StyleValueType.CommaSeparator:
          str = ",";
          break;
        case StyleValueType.ScalableImage:
          str = sheet.ReadScalableImage(handle).ToString();
          break;
        default:
          str = "Error reading value type (" + handle.valueType.ToString() + ") at index " + handle.valueIndex.ToString();
          break;
      }
      return str;
    }

    public static bool IsVarFunction(this StyleValueHandle handle) => handle.valueType == StyleValueType.Function && handle.valueIndex == 1;
  }
}
