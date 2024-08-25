// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheet
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
  /// <summary>
  ///        <para>
  /// Style sheets are applied to visual elements in order to control the layout and visual appearance of the user interface.
  /// </para>
  ///      </summary>
  [Serializable]
  public class StyleSheet : ScriptableObject
  {
    [SerializeField]
    private bool m_ImportedWithErrors;
    [SerializeField]
    private bool m_ImportedWithWarnings;
    [SerializeField]
    private StyleRule[] m_Rules;
    [SerializeField]
    private StyleComplexSelector[] m_ComplexSelectors;
    [SerializeField]
    internal float[] floats;
    [SerializeField]
    internal Dimension[] dimensions;
    [SerializeField]
    internal Color[] colors;
    [SerializeField]
    internal string[] strings;
    [SerializeField]
    internal UnityEngine.Object[] assets;
    [SerializeField]
    internal StyleSheet.ImportStruct[] imports;
    [SerializeField]
    private List<StyleSheet> m_FlattenedImportedStyleSheets;
    [SerializeField]
    private int m_ContentHash;
    [SerializeField]
    internal ScalableImage[] scalableImages;
    [NonSerialized]
    internal Dictionary<string, StyleComplexSelector> orderedNameSelectors;
    [NonSerialized]
    internal Dictionary<string, StyleComplexSelector> orderedTypeSelectors;
    [NonSerialized]
    internal Dictionary<string, StyleComplexSelector> orderedClassSelectors;
    [NonSerialized]
    private bool m_IsDefaultStyleSheet;
    private static string kCustomPropertyMarker = "--";

    /// <summary>
    ///        <para>
    /// Whether there were errors encountered while importing the StyleSheet
    /// </para>
    ///      </summary>
    public bool importedWithErrors
    {
      get => this.m_ImportedWithErrors;
      internal set => this.m_ImportedWithErrors = value;
    }

    /// <summary>
    ///        <para>
    /// Whether there were warnings encountered while importing the StyleSheet
    /// </para>
    ///      </summary>
    public bool importedWithWarnings
    {
      get => this.m_ImportedWithWarnings;
      internal set => this.m_ImportedWithWarnings = value;
    }

    internal StyleRule[] rules
    {
      get => this.m_Rules;
      set
      {
        this.m_Rules = value;
        this.SetupReferences();
      }
    }

    internal StyleComplexSelector[] complexSelectors
    {
      get => this.m_ComplexSelectors;
      set
      {
        this.m_ComplexSelectors = value;
        this.SetupReferences();
      }
    }

    internal List<StyleSheet> flattenedRecursiveImports => this.m_FlattenedImportedStyleSheets;

    /// <summary>
    ///        <para>
    /// A hash value computed from the stylesheet content.
    /// </para>
    ///      </summary>
    public int contentHash
    {
      get => this.m_ContentHash;
      set => this.m_ContentHash = value;
    }

    internal bool isDefaultStyleSheet
    {
      get => this.m_IsDefaultStyleSheet;
      set
      {
        this.m_IsDefaultStyleSheet = value;
        if (this.flattenedRecursiveImports == null)
          return;
        foreach (StyleSheet flattenedRecursiveImport in this.flattenedRecursiveImports)
          flattenedRecursiveImport.isDefaultStyleSheet = value;
      }
    }

    private static bool TryCheckAccess<T>(
      T[] list,
      StyleValueType type,
      StyleValueHandle handle,
      out T value)
    {
      bool flag = false;
      value = default (T);
      if (handle.valueType == type && handle.valueIndex >= 0 && handle.valueIndex < list.Length)
      {
        value = list[handle.valueIndex];
        flag = true;
      }
      else
        Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", (object) type, (object) handle.valueType);
      return flag;
    }

    private static T CheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle)
    {
      T obj = default (T);
      if (handle.valueType != type)
        Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", (object) type, (object) handle.valueType);
      else if (list == null || handle.valueIndex < 0 || handle.valueIndex >= list.Length)
        Debug.LogError((object) "Accessing invalid property");
      else
        obj = list[handle.valueIndex];
      return obj;
    }

    internal virtual void OnEnable() => this.SetupReferences();

    internal void FlattenImportedStyleSheetsRecursive()
    {
      this.m_FlattenedImportedStyleSheets = new List<StyleSheet>();
      this.FlattenImportedStyleSheetsRecursive(this);
    }

    private void FlattenImportedStyleSheetsRecursive(StyleSheet sheet)
    {
      if (sheet.imports == null)
        return;
      for (int index = 0; index < sheet.imports.Length; ++index)
      {
        StyleSheet styleSheet = sheet.imports[index].styleSheet;
        if (!((UnityEngine.Object) styleSheet == (UnityEngine.Object) null))
        {
          styleSheet.isDefaultStyleSheet = this.isDefaultStyleSheet;
          this.FlattenImportedStyleSheetsRecursive(styleSheet);
          this.m_FlattenedImportedStyleSheets.Add(styleSheet);
        }
      }
    }

    private void SetupReferences()
    {
      if (this.complexSelectors == null || this.rules == null)
        return;
      foreach (StyleRule rule in this.rules)
      {
        foreach (StyleProperty property in rule.properties)
        {
          if (StyleSheet.CustomStartsWith(property.name, StyleSheet.kCustomPropertyMarker))
          {
            ++rule.customPropertiesCount;
            property.isCustomProperty = true;
          }
          foreach (StyleValueHandle handle in property.values)
          {
            if (handle.IsVarFunction())
            {
              property.requireVariableResolve = true;
              break;
            }
          }
        }
      }
      int index1 = 0;
      for (int length = this.complexSelectors.Length; index1 < length; ++index1)
        this.complexSelectors[index1].CachePseudoStateMasks();
      this.orderedClassSelectors = new Dictionary<string, StyleComplexSelector>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.orderedNameSelectors = new Dictionary<string, StyleComplexSelector>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.orderedTypeSelectors = new Dictionary<string, StyleComplexSelector>((IEqualityComparer<string>) StringComparer.Ordinal);
      for (int index2 = 0; index2 < this.complexSelectors.Length; ++index2)
      {
        StyleComplexSelector complexSelector = this.complexSelectors[index2];
        if (complexSelector.ruleIndex < this.rules.Length)
          complexSelector.rule = this.rules[complexSelector.ruleIndex];
        complexSelector.CalculateHashes();
        complexSelector.orderInStyleSheet = index2;
        StyleSelectorPart part = complexSelector.selectors[complexSelector.selectors.Length - 1].parts[0];
        string key = part.value;
        Dictionary<string, StyleComplexSelector> dictionary = (Dictionary<string, StyleComplexSelector>) null;
        switch (part.type)
        {
          case StyleSelectorType.Wildcard:
          case StyleSelectorType.Type:
            key = part.value ?? "*";
            dictionary = this.orderedTypeSelectors;
            break;
          case StyleSelectorType.Class:
            dictionary = this.orderedClassSelectors;
            break;
          case StyleSelectorType.PseudoClass:
            key = "*";
            dictionary = this.orderedTypeSelectors;
            break;
          case StyleSelectorType.ID:
            dictionary = this.orderedNameSelectors;
            break;
          default:
            Debug.LogError((object) string.Format("Invalid first part type {0}", (object) part.type));
            break;
        }
        if (dictionary != null)
        {
          StyleComplexSelector styleComplexSelector;
          if (dictionary.TryGetValue(key, out styleComplexSelector))
            complexSelector.nextInTable = styleComplexSelector;
          dictionary[key] = complexSelector;
        }
      }
    }

    internal StyleValueKeyword ReadKeyword(StyleValueHandle handle) => (StyleValueKeyword) handle.valueIndex;

    internal float ReadFloat(StyleValueHandle handle) => handle.valueType == StyleValueType.Dimension ? StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle).value : StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);

    internal bool TryReadFloat(StyleValueHandle handle, out float value)
    {
      if (StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value))
        return true;
      Dimension dimension;
      bool flag = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Float, handle, out dimension);
      value = dimension.value;
      return flag;
    }

    internal Dimension ReadDimension(StyleValueHandle handle) => handle.valueType == StyleValueType.Float ? new Dimension(StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle), Dimension.Unit.Unitless) : StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);

    internal bool TryReadDimension(StyleValueHandle handle, out Dimension value)
    {
      if (StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle, out value))
        return true;
      float num = 0.0f;
      bool flag = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out num);
      value = new Dimension(num, Dimension.Unit.Unitless);
      return flag;
    }

    internal Color ReadColor(StyleValueHandle handle) => StyleSheet.CheckAccess<Color>(this.colors, StyleValueType.Color, handle);

    internal bool TryReadColor(StyleValueHandle handle, out Color value) => StyleSheet.TryCheckAccess<Color>(this.colors, StyleValueType.Color, handle, out value);

    internal string ReadString(StyleValueHandle handle) => StyleSheet.CheckAccess<string>(this.strings, StyleValueType.String, handle);

    internal bool TryReadString(StyleValueHandle handle, out string value) => StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.String, handle, out value);

    internal string ReadEnum(StyleValueHandle handle) => StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Enum, handle);

    internal bool TryReadEnum(StyleValueHandle handle, out string value) => StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Enum, handle, out value);

    internal string ReadVariable(StyleValueHandle handle) => StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Variable, handle);

    internal bool TryReadVariable(StyleValueHandle handle, out string value) => StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Variable, handle, out value);

    internal string ReadResourcePath(StyleValueHandle handle) => StyleSheet.CheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle);

    internal bool TryReadResourcePath(StyleValueHandle handle, out string value) => StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle, out value);

    internal UnityEngine.Object ReadAssetReference(StyleValueHandle handle) => StyleSheet.CheckAccess<UnityEngine.Object>(this.assets, StyleValueType.AssetReference, handle);

    internal string ReadMissingAssetReferenceUrl(StyleValueHandle handle) => StyleSheet.CheckAccess<string>(this.strings, StyleValueType.MissingAssetReference, handle);

    internal bool TryReadAssetReference(StyleValueHandle handle, out UnityEngine.Object value) => StyleSheet.TryCheckAccess<UnityEngine.Object>(this.assets, StyleValueType.AssetReference, handle, out value);

    internal StyleValueFunction ReadFunction(StyleValueHandle handle) => (StyleValueFunction) handle.valueIndex;

    internal string ReadFunctionName(StyleValueHandle handle)
    {
      if (handle.valueType == StyleValueType.Function)
        return ((StyleValueFunction) handle.valueIndex).ToUssString();
      Debug.LogErrorFormat(string.Format("Trying to read value of type {0} while reading a value of type {1}", (object) StyleValueType.Function, (object) handle.valueType));
      return string.Empty;
    }

    internal ScalableImage ReadScalableImage(StyleValueHandle handle) => StyleSheet.CheckAccess<ScalableImage>(this.scalableImages, StyleValueType.ScalableImage, handle);

    private static bool CustomStartsWith(string originalString, string pattern)
    {
      int length1 = originalString.Length;
      int length2 = pattern.Length;
      int index1 = 0;
      int index2;
      for (index2 = 0; index1 < length1 && index2 < length2 && (int) originalString[index1] == (int) pattern[index2]; ++index2)
        ++index1;
      return index2 == length2 && length1 >= length2 || index1 == length1 && length2 >= length1;
    }

    [Serializable]
    internal struct ImportStruct
    {
      public StyleSheet styleSheet;
      public string[] mediaQueries;
    }
  }
}
