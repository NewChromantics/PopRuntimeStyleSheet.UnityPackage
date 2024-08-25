// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
  internal class StyleSyntaxParser
  {
    private List<Expression> m_ProcessExpressionList = new List<Expression>();
    private Stack<Expression> m_ExpressionStack = new Stack<Expression>();
    private Stack<ExpressionCombinator> m_CombinatorStack = new Stack<ExpressionCombinator>();
    private Dictionary<string, Expression> m_ParsedExpressionCache = new Dictionary<string, Expression>();

    public Expression Parse(string syntax)
    {
      if (string.IsNullOrEmpty(syntax))
        return (Expression) null;
      Expression expression = (Expression) null;
      if (!this.m_ParsedExpressionCache.TryGetValue(syntax, out expression))
      {
        StyleSyntaxTokenizer tokenizer = new StyleSyntaxTokenizer();
        tokenizer.Tokenize(syntax);
        try
        {
          expression = this.ParseExpression(tokenizer);
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
        }
        this.m_ParsedExpressionCache[syntax] = expression;
      }
      return expression;
    }

    private Expression ParseExpression(StyleSyntaxTokenizer tokenizer)
    {
      for (StyleSyntaxToken current = tokenizer.current; !StyleSyntaxParser.IsExpressionEnd(current); current = tokenizer.current)
      {
        Expression expression;
        if (current.type == StyleSyntaxTokenType.String || current.type == StyleSyntaxTokenType.LessThan)
        {
          expression = this.ParseTerm(tokenizer);
        }
        else
        {
          if (current.type != StyleSyntaxTokenType.OpenBracket)
            throw new Exception(string.Format("Unexpected token '{0}' in expression", (object) current.type));
          expression = this.ParseGroup(tokenizer);
        }
        this.m_ExpressionStack.Push(expression);
        ExpressionCombinator combinatorType = this.ParseCombinatorType(tokenizer);
        if (combinatorType != 0)
        {
          if (this.m_CombinatorStack.Count > 0)
          {
            ExpressionCombinator expressionCombinator = this.m_CombinatorStack.Peek();
            int num = (int) expressionCombinator;
            for (int index = (int) combinatorType; num > index && expressionCombinator != ExpressionCombinator.Group; num = (int) expressionCombinator)
            {
              this.ProcessCombinatorStack();
              expressionCombinator = this.m_CombinatorStack.Count > 0 ? this.m_CombinatorStack.Peek() : ExpressionCombinator.None;
            }
          }
          this.m_CombinatorStack.Push(combinatorType);
        }
      }
      while (this.m_CombinatorStack.Count > 0)
      {
        if (this.m_CombinatorStack.Peek() == ExpressionCombinator.Group)
        {
          int num = (int) this.m_CombinatorStack.Pop();
          break;
        }
        this.ProcessCombinatorStack();
      }
      return this.m_ExpressionStack.Pop();
    }

    private void ProcessCombinatorStack()
    {
      ExpressionCombinator expressionCombinator = this.m_CombinatorStack.Pop();
      Expression expression1 = this.m_ExpressionStack.Pop();
      Expression expression2 = this.m_ExpressionStack.Pop();
      this.m_ProcessExpressionList.Clear();
      this.m_ProcessExpressionList.Add(expression2);
      this.m_ProcessExpressionList.Add(expression1);
      while (this.m_CombinatorStack.Count > 0 && expressionCombinator == this.m_CombinatorStack.Peek())
      {
        this.m_ProcessExpressionList.Insert(0, this.m_ExpressionStack.Pop());
        int num = (int) this.m_CombinatorStack.Pop();
      }
      this.m_ExpressionStack.Push(new Expression(ExpressionType.Combinator)
      {
        combinator = expressionCombinator,
        subExpressions = this.m_ProcessExpressionList.ToArray()
      });
    }

    private Expression ParseTerm(StyleSyntaxTokenizer tokenizer)
    {
      StyleSyntaxToken current = tokenizer.current;
      Expression term;
      if (current.type == StyleSyntaxTokenType.LessThan)
      {
        term = this.ParseDataType(tokenizer);
      }
      else
      {
        if (current.type != StyleSyntaxTokenType.String)
          throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected term token", (object) current.type));
        term = new Expression(ExpressionType.Keyword);
        term.keyword = current.text.ToLower();
        tokenizer.MoveNext();
      }
      this.ParseMultiplier(tokenizer, ref term.multiplier);
      return term;
    }

    private ExpressionCombinator ParseCombinatorType(
      StyleSyntaxTokenizer tokenizer)
    {
      ExpressionCombinator combinatorType = ExpressionCombinator.None;
      for (StyleSyntaxToken token1 = tokenizer.current; !StyleSyntaxParser.IsExpressionEnd(token1) && combinatorType == ExpressionCombinator.None; token1 = tokenizer.MoveNext())
      {
        StyleSyntaxToken token2 = tokenizer.PeekNext();
        switch (token1.type)
        {
          case StyleSyntaxTokenType.Space:
            if (!StyleSyntaxParser.IsCombinator(token2) && token2.type != StyleSyntaxTokenType.CloseBracket)
            {
              combinatorType = ExpressionCombinator.Juxtaposition;
              break;
            }
            break;
          case StyleSyntaxTokenType.SingleBar:
            combinatorType = ExpressionCombinator.Or;
            break;
          case StyleSyntaxTokenType.DoubleBar:
            combinatorType = ExpressionCombinator.OrOr;
            break;
          case StyleSyntaxTokenType.DoubleAmpersand:
            combinatorType = ExpressionCombinator.AndAnd;
            break;
          default:
            throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected combinator token", (object) token1.type));
        }
      }
      StyleSyntaxParser.EatSpace(tokenizer);
      return combinatorType;
    }

    private Expression ParseGroup(StyleSyntaxTokenizer tokenizer)
    {
      StyleSyntaxToken current1 = tokenizer.current;
      if (current1.type != StyleSyntaxTokenType.OpenBracket)
        throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected '[' token", (object) current1.type));
      this.m_CombinatorStack.Push(ExpressionCombinator.Group);
      tokenizer.MoveNext();
      StyleSyntaxParser.EatSpace(tokenizer);
      Expression expression = this.ParseExpression(tokenizer);
      StyleSyntaxToken current2 = tokenizer.current;
      if (current2.type != StyleSyntaxTokenType.CloseBracket)
        throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected ']' token", (object) current2.type));
      tokenizer.MoveNext();
      Expression group = new Expression(ExpressionType.Combinator);
      group.combinator = ExpressionCombinator.Group;
      group.subExpressions = new Expression[1]{ expression };
      this.ParseMultiplier(tokenizer, ref group.multiplier);
      return group;
    }

    private Expression ParseDataType(StyleSyntaxTokenizer tokenizer)
    {
      StyleSyntaxToken current1 = tokenizer.current;
      if (current1.type != StyleSyntaxTokenType.LessThan)
        throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '<' token", (object) current1.type));
      StyleSyntaxToken styleSyntaxToken = tokenizer.MoveNext();
      Expression dataType1;
      switch (styleSyntaxToken.type)
      {
        case StyleSyntaxTokenType.String:
          string syntax;
          if (StylePropertyCache.TryGetNonTerminalValue(styleSyntaxToken.text, out syntax))
          {
            dataType1 = this.ParseNonTerminalValue(syntax);
          }
          else
          {
            DataType dataType2 = DataType.None;
            try
            {
              object obj = Enum.Parse(typeof (DataType), styleSyntaxToken.text.Replace("-", ""), true);
              if (obj != null)
                dataType2 = (DataType) obj;
            }
            catch (Exception ex)
            {
              throw new Exception("Unknown data type '" + styleSyntaxToken.text + "'");
            }
            dataType1 = new Expression(ExpressionType.Data);
            dataType1.dataType = dataType2;
          }
          tokenizer.MoveNext();
          break;
        case StyleSyntaxTokenType.SingleQuote:
          dataType1 = this.ParseProperty(tokenizer);
          break;
        default:
          throw new Exception(string.Format("Unexpected token '{0}' in data type expression", (object) styleSyntaxToken.type));
      }
      StyleSyntaxToken current2 = tokenizer.current;
      if (current2.type != StyleSyntaxTokenType.GreaterThan)
        throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '>' token", (object) current2.type));
      tokenizer.MoveNext();
      return dataType1;
    }

    private Expression ParseNonTerminalValue(string syntax)
    {
      Expression expression = (Expression) null;
      if (!this.m_ParsedExpressionCache.TryGetValue(syntax, out expression))
      {
        this.m_CombinatorStack.Push(ExpressionCombinator.Group);
        expression = this.Parse(syntax);
      }
      return new Expression(ExpressionType.Combinator)
      {
        combinator = ExpressionCombinator.Group,
        subExpressions = new Expression[1]{ expression }
      };
    }

    private Expression ParseProperty(StyleSyntaxTokenizer tokenizer)
    {
      Expression expression = (Expression) null;
      StyleSyntaxToken current = tokenizer.current;
      if (current.type != StyleSyntaxTokenType.SingleQuote)
        throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", (object) current.type));
      StyleSyntaxToken styleSyntaxToken1 = tokenizer.MoveNext();
      string name = styleSyntaxToken1.type == StyleSyntaxTokenType.String ? styleSyntaxToken1.text : throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected 'string' token", (object) styleSyntaxToken1.type));
      string syntax;
      if (!StylePropertyCache.TryGetSyntax(name, out syntax))
        throw new Exception("Unknown property '" + name + "' <''> expression.");
      if (!this.m_ParsedExpressionCache.TryGetValue(syntax, out expression))
      {
        this.m_CombinatorStack.Push(ExpressionCombinator.Group);
        expression = this.Parse(syntax);
      }
      StyleSyntaxToken styleSyntaxToken2 = tokenizer.MoveNext();
      if (styleSyntaxToken2.type != StyleSyntaxTokenType.SingleQuote)
        throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", (object) styleSyntaxToken2.type));
      StyleSyntaxToken styleSyntaxToken3 = tokenizer.MoveNext();
      if (styleSyntaxToken3.type != StyleSyntaxTokenType.GreaterThan)
        throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected '>' token", (object) styleSyntaxToken3.type));
      return new Expression(ExpressionType.Combinator)
      {
        combinator = ExpressionCombinator.Group,
        subExpressions = new Expression[1]{ expression }
      };
    }

    private void ParseMultiplier(
      StyleSyntaxTokenizer tokenizer,
      ref ExpressionMultiplier multiplier)
    {
      StyleSyntaxToken current = tokenizer.current;
      if (StyleSyntaxParser.IsMultiplier(current))
      {
        switch (current.type)
        {
          case StyleSyntaxTokenType.Asterisk:
            multiplier.type = ExpressionMultiplierType.ZeroOrMore;
            break;
          case StyleSyntaxTokenType.Plus:
            multiplier.type = ExpressionMultiplierType.OneOrMore;
            break;
          case StyleSyntaxTokenType.QuestionMark:
            multiplier.type = ExpressionMultiplierType.ZeroOrOne;
            break;
          case StyleSyntaxTokenType.HashMark:
            multiplier.type = ExpressionMultiplierType.OneOrMoreComma;
            break;
          case StyleSyntaxTokenType.ExclamationPoint:
            multiplier.type = ExpressionMultiplierType.GroupAtLeastOne;
            break;
          case StyleSyntaxTokenType.OpenBrace:
            multiplier.type = ExpressionMultiplierType.Ranges;
            break;
          default:
            throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected multiplier token", (object) current.type));
        }
        tokenizer.MoveNext();
      }
      if (multiplier.type != ExpressionMultiplierType.Ranges)
        return;
      this.ParseRanges(tokenizer, out multiplier.min, out multiplier.max);
    }

    private void ParseRanges(StyleSyntaxTokenizer tokenizer, out int min, out int max)
    {
      min = -1;
      max = -1;
      StyleSyntaxToken styleSyntaxToken = tokenizer.current;
      bool flag = false;
      for (; styleSyntaxToken.type != StyleSyntaxTokenType.CloseBrace; styleSyntaxToken = tokenizer.MoveNext())
      {
        switch (styleSyntaxToken.type)
        {
          case StyleSyntaxTokenType.Number:
            if (!flag)
            {
              min = styleSyntaxToken.number;
              break;
            }
            max = styleSyntaxToken.number;
            break;
          case StyleSyntaxTokenType.Comma:
            flag = true;
            break;
          default:
            throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected ranges token", (object) styleSyntaxToken.type));
        }
      }
      tokenizer.MoveNext();
    }

    private static void EatSpace(StyleSyntaxTokenizer tokenizer)
    {
      if (tokenizer.current.type != StyleSyntaxTokenType.Space)
        return;
      tokenizer.MoveNext();
    }

    private static bool IsExpressionEnd(StyleSyntaxToken token)
    {
      switch (token.type)
      {
        case StyleSyntaxTokenType.CloseBracket:
        case StyleSyntaxTokenType.End:
          return true;
        default:
          return false;
      }
    }

    private static bool IsCombinator(StyleSyntaxToken token)
    {
      switch (token.type)
      {
        case StyleSyntaxTokenType.Space:
        case StyleSyntaxTokenType.SingleBar:
        case StyleSyntaxTokenType.DoubleBar:
        case StyleSyntaxTokenType.DoubleAmpersand:
          return true;
        default:
          return false;
      }
    }

    private static bool IsMultiplier(StyleSyntaxToken token)
    {
      switch (token.type)
      {
        case StyleSyntaxTokenType.Asterisk:
        case StyleSyntaxTokenType.Plus:
        case StyleSyntaxTokenType.QuestionMark:
        case StyleSyntaxTokenType.HashMark:
        case StyleSyntaxTokenType.ExclamationPoint:
        case StyleSyntaxTokenType.OpenBrace:
          return true;
        default:
          return false;
      }
    }
  }
}
