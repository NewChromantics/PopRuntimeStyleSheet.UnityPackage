// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleValueHandle
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;

namespace UnityEngine.UIElements
{
  [Serializable]
  internal struct StyleValueHandle
  {
    [SerializeField]
    private StyleValueType m_ValueType;
    [SerializeField]
    internal int valueIndex;

    public StyleValueType valueType
    {
      get => this.m_ValueType;
      internal set => this.m_ValueType = value;
    }

    internal StyleValueHandle(int valueIndex, StyleValueType valueType)
    {
      this.valueIndex = valueIndex;
      this.m_ValueType = valueType;
    }
  }
}
