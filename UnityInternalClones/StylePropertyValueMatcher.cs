// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleSheets.StylePropertyValueMatcher
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeStyleSheet.UIElements.StyleSheets
{
  internal class StylePropertyValueMatcher : BaseStyleMatcher
  {
    private List<StylePropertyValue> m_Values;

    private StylePropertyValue current => !this.hasCurrent ? new StylePropertyValue() : this.m_Values[this.currentIndex];

    public override int valueCount => this.m_Values.Count;

    public override bool isCurrentVariable => false;

    public override bool isCurrentComma => this.hasCurrent && this.m_Values[this.currentIndex].handle.valueType == StyleValueType.CommaSeparator;

    public MatchResult Match(RuntimeStyleSheet.UIElements.StyleSheets.Syntax.Expression exp, List<StylePropertyValue> values)
    {
      MatchResult matchResult = new MatchResult()
      {
        errorCode = MatchResultErrorCode.None
      };
      if (values == null || values.Count == 0)
      {
        matchResult.errorCode = MatchResultErrorCode.EmptyValue;
        return matchResult;
      }
      this.Initialize();
      this.m_Values = values;
      StyleValueHandle handle = this.m_Values[0].handle;
      bool flag;
      if (handle.valueType == StyleValueType.Keyword && handle.valueIndex == 1)
      {
        this.MoveNext();
        flag = true;
      }
      else
        flag = this.Match(exp);
      if (!flag)
      {
        var sheet = this.current.sheet;
        matchResult.errorCode = MatchResultErrorCode.Syntax;
        throw new NotImplementedException();
        //matchResult.errorValue = sheet.ReadAsString(this.current.handle);
      }
      else if (this.hasCurrent)
      {
        var sheet = this.current.sheet;
        matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
        throw new NotImplementedException();
        //matchResult.errorValue = sheet.ReadAsString(this.current.handle);
      }
      return matchResult;
    }

    protected override bool MatchKeyword(string keyword)
    {
      StylePropertyValue current = this.current;
      if (current.handle.valueType == StyleValueType.Keyword)
        return ((StyleValueKeyword) current.handle.valueIndex).ToUssString() == keyword.ToLower();
      return current.handle.valueType == StyleValueType.Enum && current.sheet.ReadEnum(current.handle) == keyword.ToLower();
    }

    protected override bool MatchNumber() => this.current.handle.valueType == StyleValueType.Float;

    protected override bool MatchInteger() => this.current.handle.valueType == StyleValueType.Float;

    protected override bool MatchLength()
    {
      StylePropertyValue current = this.current;
      if (current.handle.valueType == StyleValueType.Dimension)
        return current.sheet.ReadDimension(current.handle).unit == Dimension.Unit.Pixel;
      return current.handle.valueType == StyleValueType.Float && Mathf.Approximately(0.0f, current.sheet.ReadFloat(current.handle));
    }

    protected override bool MatchPercentage()
    {
      StylePropertyValue current = this.current;
      if (current.handle.valueType == StyleValueType.Dimension)
        return current.sheet.ReadDimension(current.handle).unit == Dimension.Unit.Percent;
      return current.handle.valueType == StyleValueType.Float && Mathf.Approximately(0.0f, current.sheet.ReadFloat(current.handle));
    }

    protected override bool MatchColor()
    {
      StylePropertyValue current = this.current;
      if (current.handle.valueType == StyleValueType.Color)
        return true;
      if (current.handle.valueType == StyleValueType.Enum)
      {
        Color color = Color.clear;
        if (StyleSheetColor.TryGetColor(current.sheet.ReadAsString(current.handle).ToLower(), out color))
          return true;
      }
      return false;
    }

    protected override bool MatchResource() => this.current.handle.valueType == StyleValueType.ResourcePath;

    protected override bool MatchUrl()
    {
      StyleValueType valueType = this.current.handle.valueType;
      return (valueType == StyleValueType.AssetReference ? 0 : (valueType != StyleValueType.ScalableImage ? 1 : 0)) == 0;
    }

    protected override bool MatchTime()
    {
      StylePropertyValue current = this.current;
      if (current.handle.valueType != StyleValueType.Dimension)
        return false;
      Dimension dimension = current.sheet.ReadDimension(current.handle);
      return dimension.unit == Dimension.Unit.Second || dimension.unit == Dimension.Unit.Millisecond;
    }

    protected override bool MatchCustomIdent()
    {
      StylePropertyValue current = this.current;
      if (current.handle.valueType != StyleValueType.Enum)
        return false;
      string input = current.sheet.ReadAsString(current.handle);
      System.Text.RegularExpressions.Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(input);
      return match.Success && match.Length == input.Length;
    }

    protected override bool MatchAngle()
    {
      StylePropertyValue current = this.current;
      if (current.handle.valueType == StyleValueType.Dimension)
      {
        switch (current.sheet.ReadDimension(current.handle).unit)
        {
          case Dimension.Unit.Degree:
          case Dimension.Unit.Gradian:
          case Dimension.Unit.Radian:
          case Dimension.Unit.Turn:
            return true;
        }
      }
      return current.handle.valueType == StyleValueType.Float && Mathf.Approximately(0.0f, current.sheet.ReadFloat(current.handle));
    }
  }
}
