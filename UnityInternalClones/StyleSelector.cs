// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleSelector
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RuntimeStyleSheet.UIElements
{
  [Serializable]
  internal class StyleSelector
  {
    [SerializeField]
    private StyleSelectorPart[] m_Parts;
    [SerializeField]
    private StyleSelectorRelationship m_PreviousRelationship;
    internal int pseudoStateMask = -1;
    internal int negatedPseudoStateMask = -1;

    public StyleSelectorPart[] parts
    {
      get => this.m_Parts;
      internal set => this.m_Parts = value;
    }

    public StyleSelectorRelationship previousRelationship
    {
      get => this.m_PreviousRelationship;
      internal set => this.m_PreviousRelationship = value;
    }

    public override string ToString() => string.Join(", ", ((IEnumerable<StyleSelectorPart>) this.parts).Select<StyleSelectorPart, string>((Func<StyleSelectorPart, string>) (p => p.ToString())).ToArray<string>());
  }
}
