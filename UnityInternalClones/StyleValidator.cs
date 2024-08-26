// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleSheets.StyleValidator
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using RuntimeStyleSheet.UIElements.StyleSheets.Syntax;
using UnityEngine;

namespace RuntimeStyleSheet.UIElements.StyleSheets
{
  internal class StyleValidator
  {
    private StyleSyntaxParser m_SyntaxParser;
    private StyleMatcher m_StyleMatcher;

    public StyleValidator()
    {
      this.m_SyntaxParser = new StyleSyntaxParser();
      this.m_StyleMatcher = new StyleMatcher();
    }

    public StyleValidationResult ValidateProperty(string name, string value)
    {
      StyleValidationResult validationResult = new StyleValidationResult()
      {
        status = StyleValidationStatus.Ok
      };
      if (name.StartsWith("--"))
        return validationResult;
      string syntax;
      if (!StylePropertyCache.TryGetSyntax(name, out syntax))
      {
        string closestPropertyName = StylePropertyCache.FindClosestPropertyName(name);
        validationResult.status = StyleValidationStatus.Error;
        validationResult.message = "Unknown property '" + name + "'";
        if (!string.IsNullOrEmpty(closestPropertyName))
          validationResult.message = validationResult.message + " (did you mean '" + closestPropertyName + "'?)";
        return validationResult;
      }
      RuntimeStyleSheet.UIElements.StyleSheets.Syntax.Expression exp = this.m_SyntaxParser.Parse(syntax);
      if (exp == null)
      {
        validationResult.status = StyleValidationStatus.Error;
        validationResult.message = "Invalid '" + name + "' property syntax '" + syntax + "'";
        return validationResult;
      }
      MatchResult matchResult = this.m_StyleMatcher.Match(exp, value);
      if (!matchResult.success)
      {
        validationResult.errorValue = matchResult.errorValue;
        switch (matchResult.errorCode)
        {
          case MatchResultErrorCode.Syntax:
            validationResult.status = StyleValidationStatus.Error;
            string unitHint;
            if (this.IsUnitMissing(syntax, value, out unitHint))
              validationResult.hint = "Property expects a unit. Did you forget to add " + unitHint + "?";
            else if (this.IsUnsupportedColor(syntax))
              validationResult.hint = "Unsupported color '" + value + "'.";
            validationResult.message = "Expected (" + syntax + ") but found '" + matchResult.errorValue + "'";
            break;
          case MatchResultErrorCode.EmptyValue:
            validationResult.status = StyleValidationStatus.Error;
            validationResult.message = "Expected (" + syntax + ") but found empty value";
            break;
          case MatchResultErrorCode.ExpectedEndOfValue:
            validationResult.status = StyleValidationStatus.Warning;
            validationResult.message = "Expected end of value but found '" + matchResult.errorValue + "'";
            break;
          default:
            Debug.LogAssertion((object) string.Format("Unexpected error code '{0}'", (object) matchResult.errorCode));
            break;
        }
      }
      return validationResult;
    }

    private bool IsUnitMissing(string propertySyntax, string propertyValue, out string unitHint)
    {
      unitHint = (string) null;
      if (!float.TryParse(propertyValue, out float _))
        return false;
      if (propertySyntax.Contains("<length>") || propertySyntax.Contains("<length-percentage>"))
        unitHint = "px or %";
      else if (propertySyntax.Contains("<time>"))
        unitHint = "s or ms";
      return !string.IsNullOrEmpty(unitHint);
    }

    private bool IsUnsupportedColor(string propertySyntax) => propertySyntax.StartsWith("<color>");
  }
}
