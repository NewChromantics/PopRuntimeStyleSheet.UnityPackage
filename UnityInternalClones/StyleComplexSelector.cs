// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleComplexSelector
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
  [Serializable]
  internal class StyleComplexSelector : ISerializationCallbackReceiver
  {
    //[NonSerialized]
    //public Hashes ancestorHashes;
    [SerializeField]
    private int m_Specificity;
    [NonSerialized]
    private bool m_isSimple;
    [SerializeField]
    private StyleSelector[] m_Selectors;
    [SerializeField]
    internal int ruleIndex;
    [NonSerialized]
    internal StyleComplexSelector nextInTable;
    [NonSerialized]
    internal int orderInStyleSheet;
    private static Dictionary<string, StyleComplexSelector.PseudoStateData> s_PseudoStates;
    private static List<StyleSelectorPart> m_HashList = new List<StyleSelectorPart>();

    public int specificity
    {
      get => this.m_Specificity;
      internal set => this.m_Specificity = value;
    }

    public StyleRule rule { get; internal set; }

    public bool isSimple => this.m_isSimple;

    public StyleSelector[] selectors
    {
      get => this.m_Selectors;
      internal set
      {
        this.m_Selectors = value;
        this.m_isSimple = this.m_Selectors.Length == 1;
      }
    }

    public void OnBeforeSerialize()
    {
    }

    public virtual void OnAfterDeserialize() => this.m_isSimple = this.m_Selectors.Length == 1;

    internal void CachePseudoStateMasks()
    {
      if (StyleComplexSelector.s_PseudoStates == null)
      {
        StyleComplexSelector.s_PseudoStates = new Dictionary<string, StyleComplexSelector.PseudoStateData>();
        StyleComplexSelector.s_PseudoStates["active"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, false);
        StyleComplexSelector.s_PseudoStates["hover"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Hover, false);
        StyleComplexSelector.s_PseudoStates["checked"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
        StyleComplexSelector.s_PseudoStates["selected"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
        StyleComplexSelector.s_PseudoStates["disabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, false);
        StyleComplexSelector.s_PseudoStates["focus"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Focus, false);
        StyleComplexSelector.s_PseudoStates["root"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Root, false);
        StyleComplexSelector.s_PseudoStates["inactive"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, true);
        StyleComplexSelector.s_PseudoStates["enabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, true);
      }
      int index1 = 0;
      for (int length = this.selectors.Length; index1 < length; ++index1)
      {
        StyleSelector selector = this.selectors[index1];
        StyleSelectorPart[] parts = selector.parts;
        PseudoStates pseudoStates1 = (PseudoStates) 0;
        PseudoStates pseudoStates2 = (PseudoStates) 0;
        for (int index2 = 0; index2 < selector.parts.Length; ++index2)
        {
          if (selector.parts[index2].type == StyleSelectorType.PseudoClass)
          {
            StyleComplexSelector.PseudoStateData pseudoStateData;
            if (StyleComplexSelector.s_PseudoStates.TryGetValue(parts[index2].value, out pseudoStateData))
            {
              if (!pseudoStateData.negate)
                pseudoStates1 |= pseudoStateData.state;
              else
                pseudoStates2 |= pseudoStateData.state;
            }
            else
              Debug.LogWarningFormat("Unknown pseudo class \"{0}\"", (object) parts[index2].value);
          }
        }
        selector.pseudoStateMask = (int) pseudoStates1;
        selector.negatedPseudoStateMask = (int) pseudoStates2;
      }
    }

    public override string ToString() => string.Format("[{0}]", (object) string.Join(", ", ((IEnumerable<StyleSelector>) this.m_Selectors).Select<StyleSelector, string>((Func<StyleSelector, string>) (x => x.ToString())).ToArray<string>()));

    private static int StyleSelectorPartCompare(StyleSelectorPart x, StyleSelectorPart y)
    {
      if (y.type < x.type)
        return -1;
      return y.type > x.type ? 1 : y.value.CompareTo(x.value);
    }

    internal unsafe void CalculateHashes()
    {
      if (this.isSimple)
        return;
      for (int index = this.selectors.Length - 2; index > -1; --index)
        StyleComplexSelector.m_HashList.AddRange((IEnumerable<StyleSelectorPart>) this.selectors[index].parts);
      StyleComplexSelector.m_HashList.RemoveAll((Predicate<StyleSelectorPart>) (p => p.type != StyleSelectorType.Class && p.type != StyleSelectorType.ID && p.type != StyleSelectorType.Type));
      StyleComplexSelector.m_HashList.Sort(new Comparison<StyleSelectorPart>(StyleComplexSelector.StyleSelectorPartCompare));
      bool flag = true;
      StyleSelectorType styleSelectorType = StyleSelectorType.Unknown;
      string str = "";
      int index1 = 0;
      int num = Math.Min(4, StyleComplexSelector.m_HashList.Count);
      for (int index2 = 0; index2 < num; ++index2)
      {
        if (flag)
        {
          flag = false;
        }
        else
        {
          while (index1 < StyleComplexSelector.m_HashList.Count && StyleComplexSelector.m_HashList[index1].type == styleSelectorType && StyleComplexSelector.m_HashList[index1].value == str)
            ++index1;
          if (index1 == StyleComplexSelector.m_HashList.Count)
            break;
        }
        styleSelectorType = StyleComplexSelector.m_HashList[index1].type;
        str = StyleComplexSelector.m_HashList[index1].value;
        Salt salt;
        switch (styleSelectorType)
        {
          case StyleSelectorType.Class:
            salt = Salt.ClassSalt;
            break;
          case StyleSelectorType.ID:
            salt = Salt.IdSalt;
            break;
          default:
            salt = Salt.TagNameSalt;
            break;
        }
        this.ancestorHashes.hashes[index2] = str.GetHashCode() * (int) salt;
      }
      StyleComplexSelector.m_HashList.Clear();
    }

    private struct PseudoStateData
    {
      public readonly PseudoStates state;
      public readonly bool negate;

      public PseudoStateData(PseudoStates state, bool negate)
      {
        this.state = state;
        this.negate = negate;
      }
    }
  }
}
