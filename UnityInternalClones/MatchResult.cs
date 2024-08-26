// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleSheets.MatchResult
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

namespace RuntimeStyleSheet.UIElements.StyleSheets
{
  internal struct MatchResult
  {
    public MatchResultErrorCode errorCode;
    public string errorValue;

    public bool success => this.errorCode == MatchResultErrorCode.None;
  }
}
