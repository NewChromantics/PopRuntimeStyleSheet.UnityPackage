// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleSelectorPart
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using System;
using UnityEngine;

namespace RuntimeStyleSheet.UIElements
{
  [Serializable]
  internal struct StyleSelectorPart
  {
    [SerializeField]
    private string m_Value;
    [SerializeField]
    private StyleSelectorType m_Type;
    internal object tempData;

    public string value
    {
      get => this.m_Value;
      internal set => this.m_Value = value;
    }

    public StyleSelectorType type
    {
      get => this.m_Type;
      internal set => this.m_Type = value;
    }

    public override string ToString() => $"[StyleSelectorPart: value={(object) this.value}, type={this.type}]";

    public static StyleSelectorPart CreateClass(string className) => new StyleSelectorPart()
    {
      m_Type = StyleSelectorType.Class,
      m_Value = className
    };

    public static StyleSelectorPart CreatePseudoClass(string className) => new StyleSelectorPart()
    {
      m_Type = StyleSelectorType.PseudoClass,
      m_Value = className
    };

    public static StyleSelectorPart CreateId(string Id) => new StyleSelectorPart()
    {
      m_Type = StyleSelectorType.ID,
      m_Value = Id
    };

    public static StyleSelectorPart CreateType(System.Type t) => new StyleSelectorPart()
    {
      m_Type = StyleSelectorType.Type,
      m_Value = t.Name
    };

    public static StyleSelectorPart CreateType(string typeName) => new StyleSelectorPart()
    {
      m_Type = StyleSelectorType.Type,
      m_Value = typeName
    };

    public static StyleSelectorPart CreatePredicate(object predicate) => new StyleSelectorPart()
    {
      m_Type = StyleSelectorType.Predicate,
      tempData = predicate
    };

    public static StyleSelectorPart CreateWildCard() => new StyleSelectorPart()
    {
      m_Type = StyleSelectorType.Wildcard
    };
  }
}
