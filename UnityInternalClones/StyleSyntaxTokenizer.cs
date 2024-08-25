// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
  internal class StyleSyntaxTokenizer
  {
    private List<StyleSyntaxToken> m_Tokens = new List<StyleSyntaxToken>();
    private int m_CurrentTokenIndex = -1;

    public StyleSyntaxToken current => this.m_CurrentTokenIndex < 0 || this.m_CurrentTokenIndex >= this.m_Tokens.Count ? new StyleSyntaxToken(StyleSyntaxTokenType.Unknown) : this.m_Tokens[this.m_CurrentTokenIndex];

    public StyleSyntaxToken MoveNext()
    {
      StyleSyntaxToken current1 = this.current;
      if (current1.type == StyleSyntaxTokenType.Unknown)
        return current1;
      ++this.m_CurrentTokenIndex;
      StyleSyntaxToken current2 = this.current;
      if (this.m_CurrentTokenIndex == this.m_Tokens.Count)
        this.m_CurrentTokenIndex = -1;
      return current2;
    }

    public StyleSyntaxToken PeekNext()
    {
      int index = this.m_CurrentTokenIndex + 1;
      return this.m_CurrentTokenIndex < 0 || index >= this.m_Tokens.Count ? new StyleSyntaxToken(StyleSyntaxTokenType.Unknown) : this.m_Tokens[index];
    }

    public void Tokenize(string syntax)
    {
      this.m_Tokens.Clear();
      this.m_CurrentTokenIndex = 0;
      syntax = syntax.Trim(' ').ToLower();
      for (int index = 0; index < syntax.Length; ++index)
      {
        char c = syntax[index];
        switch (c)
        {
          case ' ':
            index = StyleSyntaxTokenizer.GlobCharacter(syntax, index, ' ');
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Space));
            break;
          case '!':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.ExclamationPoint));
            break;
          case '#':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.HashMark));
            break;
          case '&':
            if (!StyleSyntaxTokenizer.IsNextCharacter(syntax, index, '&'))
            {
              Debug.LogAssertionFormat("Expected '&' got '{0}'", (object) (index + 1 < syntax.Length ? syntax[index + 1].ToString() : "EOF"));
              this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
              break;
            }
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleAmpersand));
            ++index;
            break;
          case '\'':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleQuote));
            break;
          case '*':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Asterisk));
            break;
          case '+':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Plus));
            break;
          case ',':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Comma));
            break;
          case '<':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.LessThan));
            break;
          case '>':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.GreaterThan));
            break;
          case '?':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.QuestionMark));
            break;
          case '[':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBracket));
            break;
          case ']':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBracket));
            break;
          case '{':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBrace));
            break;
          case '|':
            if (StyleSyntaxTokenizer.IsNextCharacter(syntax, index, '|'))
            {
              this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleBar));
              ++index;
              break;
            }
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleBar));
            break;
          case '}':
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBrace));
            break;
          default:
            if (char.IsNumber(c))
            {
              int startIndex = index;
              int length = 1;
              while (StyleSyntaxTokenizer.IsNextNumber(syntax, index))
              {
                ++index;
                ++length;
              }
              this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Number, int.Parse(syntax.Substring(startIndex, length))));
              break;
            }
            if (char.IsLetter(c))
            {
              int startIndex = index;
              int length = 1;
              while (StyleSyntaxTokenizer.IsNextLetterOrDash(syntax, index))
              {
                ++index;
                ++length;
              }
              this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.String, syntax.Substring(startIndex, length)));
              break;
            }
            Debug.LogAssertionFormat("Expected letter or number got '{0}'", (object) c);
            this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
            break;
        }
      }
      this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.End));
    }

    private static bool IsNextCharacter(string s, int index, char c) => index + 1 < s.Length && (int) s[index + 1] == (int) c;

    private static bool IsNextLetterOrDash(string s, int index) => index + 1 < s.Length && (char.IsLetter(s[index + 1]) || s[index + 1] == '-');

    private static bool IsNextNumber(string s, int index) => index + 1 < s.Length && char.IsNumber(s[index + 1]);

    private static int GlobCharacter(string s, int index, char c)
    {
      while (StyleSyntaxTokenizer.IsNextCharacter(s, index, c))
        ++index;
      return index;
    }
  }
}
