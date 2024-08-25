// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleProperty
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;

namespace UnityEngine.UIElements
{
  [Serializable]
  internal class StyleProperty
  {
    [SerializeField]
    private string m_Name;
    [SerializeField]
    private int m_Line;
    [SerializeField]
    private StyleValueHandle[] m_Values;
    [NonSerialized]
    internal bool isCustomProperty;
    [NonSerialized]
    internal bool requireVariableResolve;

    public string name
    {
      get => this.m_Name;
      internal set => this.m_Name = value;
    }

    public int line
    {
      get => this.m_Line;
      internal set => this.m_Line = value;
    }

    public StyleValueHandle[] values
    {
      get => this.m_Values;
      internal set => this.m_Values = value;
    }
  }
}
