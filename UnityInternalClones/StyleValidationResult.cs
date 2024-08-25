// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.StyleValidationResult
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

namespace UnityEngine.UIElements.StyleSheets
{
  internal struct StyleValidationResult
  {
    public StyleValidationStatus status;
    public string message;
    public string errorValue;
    public string hint;

    public bool success => this.status == StyleValidationStatus.Ok;
  }
}
