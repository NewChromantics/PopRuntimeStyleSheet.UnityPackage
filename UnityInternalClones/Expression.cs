// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.Syntax.Expression
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
  internal class Expression
  {
    public ExpressionType type;
    public ExpressionMultiplier multiplier;
    public DataType dataType;
    public ExpressionCombinator combinator;
    public Expression[] subExpressions;
    public string keyword;

    public Expression(ExpressionType type)
    {
      this.type = type;
      this.combinator = ExpressionCombinator.None;
      this.multiplier = new ExpressionMultiplier();
      this.subExpressions = (Expression[]) null;
      this.keyword = (string) null;
    }
  }
}
