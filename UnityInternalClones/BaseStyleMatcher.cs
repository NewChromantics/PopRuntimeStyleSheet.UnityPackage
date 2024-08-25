// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.BaseStyleMatcher
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
  internal abstract class BaseStyleMatcher
  {
    protected static readonly Regex s_CustomIdentRegex = new Regex("^-?[_a-z][_a-z0-9-]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private Stack<BaseStyleMatcher.MatchContext> m_ContextStack = new Stack<BaseStyleMatcher.MatchContext>();
    private BaseStyleMatcher.MatchContext m_CurrentContext;

    protected abstract bool MatchKeyword(string keyword);

    protected abstract bool MatchNumber();

    protected abstract bool MatchInteger();

    protected abstract bool MatchLength();

    protected abstract bool MatchPercentage();

    protected abstract bool MatchColor();

    protected abstract bool MatchResource();

    protected abstract bool MatchUrl();

    protected abstract bool MatchTime();

    protected abstract bool MatchAngle();

    protected abstract bool MatchCustomIdent();

    public abstract int valueCount { get; }

    public abstract bool isCurrentVariable { get; }

    public abstract bool isCurrentComma { get; }

    public bool hasCurrent => this.m_CurrentContext.valueIndex < this.valueCount;

    public int currentIndex
    {
      get => this.m_CurrentContext.valueIndex;
      set => this.m_CurrentContext.valueIndex = value;
    }

    public int matchedVariableCount
    {
      get => this.m_CurrentContext.matchedVariableCount;
      set => this.m_CurrentContext.matchedVariableCount = value;
    }

    protected void Initialize()
    {
      this.m_CurrentContext = new BaseStyleMatcher.MatchContext();
      this.m_ContextStack.Clear();
    }

    public void MoveNext()
    {
      if (this.currentIndex + 1 > this.valueCount)
        return;
      ++this.currentIndex;
    }

    public void SaveContext() => this.m_ContextStack.Push(this.m_CurrentContext);

    public void RestoreContext() => this.m_CurrentContext = this.m_ContextStack.Pop();

    public void DropContext() => this.m_ContextStack.Pop();

    protected bool Match(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      bool flag;
      if (exp.multiplier.type == ExpressionMultiplierType.None)
      {
        flag = this.MatchExpression(exp);
      }
      else
      {
        Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.GroupAtLeastOne, "'!' multiplier in syntax expression is not supported");
        flag = this.MatchExpressionWithMultiplier(exp);
      }
      return flag;
    }

    private bool MatchExpression(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      bool flag = false;
      if (exp.type == ExpressionType.Combinator)
      {
        flag = this.MatchCombinator(exp);
      }
      else
      {
        if (this.isCurrentVariable)
        {
          flag = true;
          ++this.matchedVariableCount;
        }
        else if (exp.type == ExpressionType.Data)
          flag = this.MatchDataType(exp);
        else if (exp.type == ExpressionType.Keyword)
          flag = this.MatchKeyword(exp.keyword);
        if (flag)
          this.MoveNext();
      }
      if (!flag && !this.hasCurrent && this.matchedVariableCount > 0)
        flag = true;
      return flag;
    }

    private bool MatchExpressionWithMultiplier(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      bool flag1 = exp.multiplier.type == ExpressionMultiplierType.OneOrMoreComma;
      bool flag2 = true;
      int min = exp.multiplier.min;
      int max = exp.multiplier.max;
      int num = 0;
      for (int index = 0; flag2 && this.hasCurrent && index < max; ++index)
      {
        flag2 = this.MatchExpression(exp);
        if (flag2)
        {
          ++num;
          if (flag1)
          {
            if (this.isCurrentComma)
              this.MoveNext();
            else
              break;
          }
        }
      }
      bool flag3 = num >= min && num <= max;
      if (!flag3 && num <= max && this.matchedVariableCount > 0)
        flag3 = true;
      return flag3;
    }

    private bool MatchGroup(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      Debug.Assert(exp.subExpressions.Length == 1, "Group has invalid number of sub expressions");
      return this.Match(exp.subExpressions[0]);
    }

    private bool MatchCombinator(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      this.SaveContext();
      bool flag = false;
      switch (exp.combinator)
      {
        case ExpressionCombinator.Or:
          flag = this.MatchOr(exp);
          break;
        case ExpressionCombinator.OrOr:
          flag = this.MatchOrOr(exp);
          break;
        case ExpressionCombinator.AndAnd:
          flag = this.MatchAndAnd(exp);
          break;
        case ExpressionCombinator.Juxtaposition:
          flag = this.MatchJuxtaposition(exp);
          break;
        case ExpressionCombinator.Group:
          flag = this.MatchGroup(exp);
          break;
      }
      if (flag)
        this.DropContext();
      else
        this.RestoreContext();
      return flag;
    }

    private bool MatchOr(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      BaseStyleMatcher.MatchContext matchContext = new BaseStyleMatcher.MatchContext();
      int num1 = 0;
      for (int index = 0; index < exp.subExpressions.Length; ++index)
      {
        this.SaveContext();
        int currentIndex = this.currentIndex;
        bool flag = this.Match(exp.subExpressions[index]);
        int num2 = this.currentIndex - currentIndex;
        if (flag && num2 > num1)
        {
          num1 = num2;
          matchContext = this.m_CurrentContext;
        }
        this.RestoreContext();
      }
      if (num1 <= 0)
        return false;
      this.m_CurrentContext = matchContext;
      return true;
    }

    private bool MatchOrOr(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp) => this.MatchMany(exp) > 0;

    private bool MatchAndAnd(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp) => this.MatchMany(exp) == exp.subExpressions.Length;

    private unsafe int MatchMany(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      BaseStyleMatcher.MatchContext matchContext = new BaseStyleMatcher.MatchContext();
      int num1 = 0;
      int num2 = -1;
      int length = exp.subExpressions.Length;
      int* matchOrder = stackalloc int[length];
      do
      {
        this.SaveContext();
        ++num2;
        for (int index = 0; index < length; ++index)
        {
          int num3 = num2 > 0 ? (num2 + index) % length : index;
          matchOrder[index] = num3;
        }
        int num4 = this.MatchManyByOrder(exp, matchOrder);
        if (num4 > num1)
        {
          num1 = num4;
          matchContext = this.m_CurrentContext;
        }
        this.RestoreContext();
      }
      while (num1 < length && num2 < length);
      if (num1 > 0)
        this.m_CurrentContext = matchContext;
      return num1;
    }

    private unsafe int MatchManyByOrder(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp, int* matchOrder)
    {
      int length = exp.subExpressions.Length;
      int* numPtr = stackalloc int[length];
      int index1 = 0;
      int num = 0;
      int index2 = 0;
      while (index2 < length && index1 + num < length)
      {
        int index3 = matchOrder[index2];
        bool flag1 = false;
        for (int index4 = 0; index4 < index1; ++index4)
        {
          if (numPtr[index4] == index3)
          {
            flag1 = true;
            break;
          }
        }
        bool flag2 = false;
        if (!flag1)
          flag2 = this.Match(exp.subExpressions[index3]);
        if (flag2)
        {
          if (num == this.matchedVariableCount)
          {
            numPtr[index1] = index3;
            ++index1;
          }
          else
            num = this.matchedVariableCount;
          index2 = 0;
        }
        else
          ++index2;
      }
      return index1 + num;
    }

    private bool MatchJuxtaposition(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      bool flag = true;
      for (int index = 0; flag && index < exp.subExpressions.Length; ++index)
        flag = this.Match(exp.subExpressions[index]);
      return flag;
    }

    private bool MatchDataType(UnityEngine.UIElements.StyleSheets.Syntax.Expression exp)
    {
      bool flag = false;
      if (this.hasCurrent)
      {
        switch (exp.dataType)
        {
          case DataType.Number:
            flag = this.MatchNumber();
            break;
          case DataType.Integer:
            flag = this.MatchInteger();
            break;
          case DataType.Length:
            flag = this.MatchLength();
            break;
          case DataType.Percentage:
            flag = this.MatchPercentage();
            break;
          case DataType.Color:
            flag = this.MatchColor();
            break;
          case DataType.Resource:
            flag = this.MatchResource();
            break;
          case DataType.Url:
            flag = this.MatchUrl();
            break;
          case DataType.Time:
            flag = this.MatchTime();
            break;
          case DataType.Angle:
            flag = this.MatchAngle();
            break;
          case DataType.CustomIdent:
            flag = this.MatchCustomIdent();
            break;
        }
      }
      return flag;
    }

    private struct MatchContext
    {
      public int valueIndex;
      public int matchedVariableCount;
    }
  }
}
