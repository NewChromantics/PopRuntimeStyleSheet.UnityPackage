// Decompiled with JetBrains decompiler
// Type: RuntimeStyleSheet.UIElements.StyleSheets.StylePropertyValueParser
// Assembly: RuntimeStyleSheet.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/RuntimeStyleSheet.UIElementsModule.xml

using System.Collections.Generic;
using System.Text;

namespace RuntimeStyleSheet.UIElements.StyleSheets
{
  internal class StylePropertyValueParser
  {
    private string m_PropertyValue;
    private List<string> m_ValueList = new List<string>();
    private StringBuilder m_StringBuilder = new StringBuilder();
    private int m_ParseIndex = 0;

    public string[] Parse(string propertyValue)
    {
      this.m_PropertyValue = propertyValue;
      this.m_ValueList.Clear();
      this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
      for (this.m_ParseIndex = 0; this.m_ParseIndex < this.m_PropertyValue.Length; ++this.m_ParseIndex)
      {
        char ch = this.m_PropertyValue[this.m_ParseIndex];
        switch (ch)
        {
          case ' ':
            this.EatSpace();
            this.AddValuePart();
            break;
          case '(':
            this.AppendFunction();
            break;
          case ',':
            this.EatSpace();
            this.AddValuePart();
            this.m_ValueList.Add(",");
            break;
          default:
            this.m_StringBuilder.Append(ch);
            break;
        }
      }
      string str = this.m_StringBuilder.ToString();
      if (!string.IsNullOrEmpty(str))
        this.m_ValueList.Add(str);
      return this.m_ValueList.ToArray();
    }

    private void AddValuePart()
    {
      string str = this.m_StringBuilder.ToString();
      this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
      this.m_ValueList.Add(str);
    }

    private void AppendFunction()
    {
      for (; this.m_ParseIndex < this.m_PropertyValue.Length && this.m_PropertyValue[this.m_ParseIndex] != ')'; ++this.m_ParseIndex)
        this.m_StringBuilder.Append(this.m_PropertyValue[this.m_ParseIndex]);
      this.m_StringBuilder.Append(this.m_PropertyValue[this.m_ParseIndex]);
    }

    private void EatSpace()
    {
      while (this.m_ParseIndex + 1 < this.m_PropertyValue.Length && this.m_PropertyValue[this.m_ParseIndex + 1] == ' ')
        ++this.m_ParseIndex;
    }
  }
}
