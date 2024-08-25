// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.StyleMatcher
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Text.RegularExpressions;

namespace UnityEngine.UIElements.StyleSheets
{
  internal class StyleMatcher : BaseStyleMatcher
  {
    private StylePropertyValueParser m_Parser = new StylePropertyValueParser();
    private string[] m_PropertyParts;
    private static readonly Regex s_NumberRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?$", RegexOptions.Compiled);
    private static readonly Regex s_IntegerRegex = new Regex("^[+-]?\\d+$", RegexOptions.Compiled);
    private static readonly Regex s_ZeroRegex = new Regex("^0(?:\\.0+)?$", RegexOptions.Compiled);
    private static readonly Regex s_LengthRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:px)$", RegexOptions.Compiled);
    private static readonly Regex s_PercentRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:%)$", RegexOptions.Compiled);
    private static readonly Regex s_HexColorRegex = new Regex("^#[a-fA-F0-9]{3}(?:[a-fA-F0-9]{3})?$", RegexOptions.Compiled);
    private static readonly Regex s_RgbRegex = new Regex("^rgb\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)$", RegexOptions.Compiled);
    private static readonly Regex s_RgbaRegex = new Regex("rgba\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*([\\d.]+)\\s*\\)$", RegexOptions.Compiled);
    private static readonly Regex s_VarFunctionRegex = new Regex("^var\\(.+\\)$", RegexOptions.Compiled);
    private static readonly Regex s_ResourceRegex = new Regex("^resource\\((.+)\\)$", RegexOptions.Compiled);
    private static readonly Regex s_UrlRegex = new Regex("^url\\((.+)\\)$", RegexOptions.Compiled);
    private static readonly Regex s_TimeRegex = new Regex("^[+-]?\\.?\\d+(?:\\.\\d+)?(?:s|ms)$", RegexOptions.Compiled);
    private static readonly Regex s_AngleRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:deg|grad|rad|turn)$", RegexOptions.Compiled);

    private string current => !this.hasCurrent ? (string) null : this.m_PropertyParts[this.currentIndex];

    public override int valueCount => this.m_PropertyParts.Length;

    public override bool isCurrentVariable => this.hasCurrent && this.current.StartsWith("var(");

    public override bool isCurrentComma => this.hasCurrent && this.current == ",";

    private void Initialize(string propertyValue)
    {
      this.Initialize();
      this.m_PropertyParts = this.m_Parser.Parse(propertyValue);
    }

    public MatchResult Match(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp, string propertyValue)
    {
      MatchResult matchResult = new MatchResult()
      {
        errorCode = MatchResultErrorCode.None
      };
      if (string.IsNullOrEmpty(propertyValue))
      {
        matchResult.errorCode = MatchResultErrorCode.EmptyValue;
        return matchResult;
      }
      this.Initialize(propertyValue);
      string current = this.current;
      bool flag;
      if (current == "initial" || current.StartsWith("env("))
      {
        this.MoveNext();
        flag = true;
      }
      else
        flag = this.Match(exp);
      if (!flag)
      {
        matchResult.errorCode = MatchResultErrorCode.Syntax;
        matchResult.errorValue = this.current;
      }
      else if (this.hasCurrent)
      {
        matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
        matchResult.errorValue = this.current;
      }
      return matchResult;
    }

    protected override bool MatchKeyword(string keyword) => string.Compare(this.current, keyword, StringComparison.OrdinalIgnoreCase) == 0;

    protected override bool MatchNumber()
    {
      string current = this.current;
      return StyleMatcher.s_NumberRegex.Match(current).Success;
    }

    protected override bool MatchInteger()
    {
      string current = this.current;
      return StyleMatcher.s_IntegerRegex.Match(current).Success;
    }

    protected override bool MatchLength()
    {
      string current = this.current;
      return StyleMatcher.s_LengthRegex.Match(current).Success || StyleMatcher.s_ZeroRegex.Match(current).Success;
    }

    protected override bool MatchPercentage()
    {
      string current = this.current;
      return StyleMatcher.s_PercentRegex.Match(current).Success || StyleMatcher.s_ZeroRegex.Match(current).Success;
    }

    protected override bool MatchColor()
    {
      string current = this.current;
      if (StyleMatcher.s_HexColorRegex.Match(current).Success || StyleMatcher.s_RgbRegex.Match(current).Success || StyleMatcher.s_RgbaRegex.Match(current).Success)
        return true;
      Color color = Color.clear;
      return StyleSheetColor.TryGetColor(current, out color);
    }

    protected override bool MatchResource()
    {
      string current = this.current;
      System.Text.RegularExpressions.Match match = StyleMatcher.s_ResourceRegex.Match(current);
      if (!match.Success)
        return false;
      string input = match.Groups[1].Value.Trim();
      return !StyleMatcher.s_VarFunctionRegex.Match(input).Success;
    }

    protected override bool MatchUrl()
    {
      string current = this.current;
      System.Text.RegularExpressions.Match match = StyleMatcher.s_UrlRegex.Match(current);
      if (!match.Success)
        return false;
      string input = match.Groups[1].Value.Trim();
      return !StyleMatcher.s_VarFunctionRegex.Match(input).Success;
    }

    protected override bool MatchTime()
    {
      string current = this.current;
      return StyleMatcher.s_TimeRegex.Match(current).Success;
    }

    protected override bool MatchAngle()
    {
      string current = this.current;
      return StyleMatcher.s_AngleRegex.Match(current).Success || StyleMatcher.s_ZeroRegex.Match(current).Success;
    }

    protected override bool MatchCustomIdent()
    {
      string current = this.current;
      System.Text.RegularExpressions.Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(current);
      return match.Success && match.Length == current.Length;
    }
  }
}
