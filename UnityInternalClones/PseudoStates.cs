﻿// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.PseudoStates
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;

namespace UnityEngine.UIElements
{
  [Flags]
  internal enum PseudoStates
  {
    Active = 1,
    Hover = 2,
    Checked = 8,
    Disabled = 32, // 0x00000020
    Focus = 64, // 0x00000040
    Root = 128, // 0x00000080
  }
}
