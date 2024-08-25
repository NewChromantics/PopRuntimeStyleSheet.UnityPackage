// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
  internal struct StyleSyntaxToken
  {
    public StyleSyntaxTokenType type;
    public string text;
    public int number;

    public StyleSyntaxToken(StyleSyntaxTokenType t)
    {
      this.type = t;
      this.text = (string) null;
      this.number = 0;
    }

    public StyleSyntaxToken(StyleSyntaxTokenType type, string text)
    {
      this.type = type;
      this.text = text;
      this.number = 0;
    }

    public StyleSyntaxToken(StyleSyntaxTokenType type, int number)
    {
      this.type = type;
      this.text = (string) null;
      this.number = number;
    }
  }
}
