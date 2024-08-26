// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleSheets.Syntax.ExpressionMultiplier
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

namespace RuntimeStyleSheet.UIElements.StyleSheets.Syntax
{
  internal struct ExpressionMultiplier
  {
    public const int Infinity = 100;
    private ExpressionMultiplierType m_Type;
    public int min;
    public int max;

    public ExpressionMultiplierType type
    {
      get => this.m_Type;
      set => this.SetType(value);
    }

    public ExpressionMultiplier(ExpressionMultiplierType type = ExpressionMultiplierType.None)
    {
      this.m_Type = type;
      this.min = this.max = 1;
      this.SetType(type);
    }

    private void SetType(ExpressionMultiplierType value)
    {
      this.m_Type = value;
      switch (value)
      {
        case ExpressionMultiplierType.ZeroOrMore:
          this.min = 0;
          this.max = 100;
          break;
        case ExpressionMultiplierType.OneOrMore:
        case ExpressionMultiplierType.OneOrMoreComma:
        case ExpressionMultiplierType.GroupAtLeastOne:
          this.min = 1;
          this.max = 100;
          break;
        case ExpressionMultiplierType.ZeroOrOne:
          this.min = 0;
          this.max = 1;
          break;
        default:
          this.min = this.max = 1;
          break;
      }
    }
  }
}
