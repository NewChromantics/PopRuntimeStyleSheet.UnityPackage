// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleRule
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using System;
using UnityEngine;

namespace RuntimeStyleSheet.UIElements
{
  [Serializable]
  internal class StyleRule
  {
    [SerializeField]
    private StyleProperty[] m_Properties;
    [SerializeField]
    internal int line;
    [NonSerialized]
    internal int customPropertiesCount;

    public StyleProperty[] properties
    {
      get => this.m_Properties;
      internal set => this.m_Properties = value;
    }
  }
}
