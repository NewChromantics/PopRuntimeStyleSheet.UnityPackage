using ExCSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using RuntimeStyleSheet.UIElements;
using RuntimeStyleSheet.UIElements.StyleSheets;
using UnityEditor.UIElements.StyleSheets;//.StyleValueImporter

using URIHelpers = UnityEditor.UIElements.StyleSheets.URIHelpers;
using URIValidationResult = UnityEditor.UIElements.StyleSheets.URIValidationResult;


/*
  Copied and adapted from unity's internal importer 
*/
internal class StyleSheetImporterImpl : UnityEditor.UIElements.StyleSheets.StyleValueImporter
  {
    private static readonly ExCSS.Parser s_Parser = new ExCSS.Parser();
    private static readonly HashSet<string> s_StyleSheetsWithCircularImportDependencies = new HashSet<string>();
    private static readonly HashSet<string> s_StyleSheetsUnsortedDependencies = new HashSet<string>();
    private static readonly List<string> s_StyleSheetProjectRelativeImportPaths = new List<string>();

 
    //bool disableValidation => false;
  
    //string assetPath => "Fake Asset";
    StyleSheetImportErrors m_Errors = new();
/*
    ExCSS.Parser m_Parser = new();
    UnityImporterContext m_Context = new();
    StyleSheetBuilder m_Builder;
    int m_CurrentLine = 0;
*/
    public StyleSheetImporterImpl()
    {
    }


    internal static string[] PopulateDependencies(string assetPath)
    {
      StyleSheetImporterImpl.s_StyleSheetsUnsortedDependencies.Clear();
      StyleSheetImporterImpl.s_StyleSheetsUnsortedDependencies.Add(assetPath);
      StyleSheetImporterImpl.s_StyleSheetsWithCircularImportDependencies.Remove(assetPath);
      List<string> dependencies = new List<string>();
      StyleSheetImporterImpl.PopulateDependencies(assetPath, dependencies);
      return dependencies.ToArray();
    }

    internal static void PopulateDependencies(string assetPath, List<string> dependencies)
    {
      string css = File.ReadAllText(assetPath);
      if (string.IsNullOrEmpty(css))
        return;
      ExCSS.StyleSheet styleSheet = StyleSheetImporterImpl.s_Parser.Parse(css);
      int count = styleSheet.ImportDirectives.Count;
      StyleSheetImporterImpl.s_StyleSheetProjectRelativeImportPaths.Clear();
      for (int index = 0; index < count; ++index)
      {
        string href = styleSheet.ImportDirectives[index].Href;
        string resolvedProjectRelativePath;
        if (URIHelpers.ValidAssetURL(assetPath, href, out string _, out resolvedProjectRelativePath) == URIValidationResult.OK && !StyleSheetImporterImpl.s_StyleSheetProjectRelativeImportPaths.Contains(resolvedProjectRelativePath))
          StyleSheetImporterImpl.s_StyleSheetProjectRelativeImportPaths.Add(resolvedProjectRelativePath);
      }
      foreach (string relativeImportPath in StyleSheetImporterImpl.s_StyleSheetProjectRelativeImportPaths)
      {
        if (StyleSheetImporterImpl.s_StyleSheetsUnsortedDependencies.Contains(relativeImportPath))
        {
          StyleSheetImporterImpl.s_StyleSheetsWithCircularImportDependencies.Add(relativeImportPath);
          throw new InvalidDataException("Circular @import dependencies");
        }
        StyleSheetImporterImpl.s_StyleSheetsUnsortedDependencies.Add(relativeImportPath);
        StyleSheetImporterImpl.PopulateDependencies(relativeImportPath, dependencies);
        dependencies.Add(relativeImportPath);
      }
    }

    protected virtual void OnImportError(StyleSheetImportErrors errors)
    {
      if (this.m_Context == null)
        return;
      foreach (StyleSheetImportError error in errors)
      {
      /*
        UnityEngine.Object @object = string.IsNullOrEmpty(error.assetPath) ? (UnityEngine.Object) null : AssetDatabase.LoadMainAssetAtPath(error.assetPath);
        if (error.isWarning)
          this.m_Context.LogImportWarning(error.ToString(StyleValueImporter.glossary), error.assetPath, error.line, @object);
        else
          this.m_Context.LogImportError(error.ToString(StyleValueImporter.glossary), error.assetPath, error.line, @object);
          */
      }
    }

    protected virtual void OnImportSuccess(RuntimeStyleSheet.UIElements.StyleSheet asset)
    {
    }

    public void Import(RuntimeStyleSheet.UIElements.StyleSheet asset, string contents)
    {
      ExCSS.StyleSheet styleSheet = this.m_Parser.Parse(contents);
      this.ImportParserStyleSheet(asset, styleSheet);
      Hash128 hash = new Hash128();
      byte[] bytes = Encoding.UTF8.GetBytes(contents);
      if (bytes.Length != 0)
        HashUtilities.ComputeHash128(bytes, ref hash);
      asset.contentHash = hash.GetHashCode();
    }

    protected void ImportParserStyleSheet(RuntimeStyleSheet.UIElements.StyleSheet asset, ExCSS.StyleSheet styleSheet)
    {
      this.m_Errors.assetPath = this.assetPath;
      if (styleSheet.Errors.Count > 0)
      {
        throw new NotImplementedException();
        /*
        foreach (StylesheetParseError error in styleSheet.Errors)
          this.m_Errors.AddSyntaxError(string.Format(StyleValueImporter.glossary.ussParsingError, (object) error.Message), error.Line);
          */
      }
      else
      {
        try
        {
          this.VisitSheet(styleSheet);
        }
        catch (Exception ex)
        {
          this.m_Errors.AddInternalError(string.Format(StyleValueImporter.glossary.internalErrorWithStackTrace, (object) ex.Message, (object) ex.StackTrace), this.m_CurrentLine);
        }
      }
      bool hasErrors = this.m_Errors.hasErrors;
      if (!hasErrors)
      {
        this.m_Builder.BuildTo(asset);
        if (!StyleSheetImporterImpl.s_StyleSheetsWithCircularImportDependencies.Contains(this.assetPath))
        {
          int count = styleSheet.ImportDirectives.Count;
          
          asset.imports = new RuntimeStyleSheet.UIElements.StyleSheet.ImportStruct[count];
          for (int index = 0; index < count; ++index)
          {
            URIHelpers.URIValidationResponse validationResponse = URIHelpers.ValidateAssetURL(this.assetPath, styleSheet.ImportDirectives[index].Href);
            URIValidationResult result = validationResponse.result;
            string errorToken = validationResponse.errorToken;
            string projectRelativePath = validationResponse.resolvedProjectRelativePath;
            if (validationResponse.hasWarningMessage)
              this.m_Errors.AddValidationWarning(validationResponse.warningMessage, this.m_CurrentLine);
            RuntimeStyleSheet.UIElements.StyleSheet styleSheet1 = (RuntimeStyleSheet.UIElements.StyleSheet) null;
            if (result != 0)
            {
              (StyleSheetImportErrorCode, string) tuple = StyleValueImporter.ConvertErrorCode(result);
              this.m_Errors.AddSemanticWarning(tuple.Item1, string.Format(tuple.Item2, (object) errorToken), this.m_CurrentLine);
            }
            else
            {
              styleSheet1 = validationResponse.resolvedQueryAsset as RuntimeStyleSheet.UIElements.StyleSheet;
              if ((bool) (UnityEngine.Object) styleSheet1)
                this.m_Context.DependsOnSourceAsset(projectRelativePath);
              else
                styleSheet1 = this.DeclareDependencyAndLoad(projectRelativePath) as RuntimeStyleSheet.UIElements.StyleSheet;
              if (!validationResponse.isLibraryAsset)
              {
                throw new NotImplementedException();
                //this.m_Context.DependsOnImportedAsset(projectRelativePath);
                }
            }
            asset.imports[index] = new RuntimeStyleSheet.UIElements.StyleSheet.ImportStruct()
            {
              styleSheet = styleSheet1,
              mediaQueries = styleSheet.ImportDirectives[index].Media.ToArray<string>()
            };
          }
          
          if (count > 0)
            asset.FlattenImportedStyleSheetsRecursive();
        }
        else
        {
        //  gr: here we need to start using FakeStyleSheet
          throw new NotImplementedException();
          /*
          asset.imports = new RuntimeStyleSheet.UIElements.StyleSheet.ImportStruct[0];
          this.m_Errors.AddValidationWarning(StyleValueImporter.glossary.circularImport, -1);
          */
        }
        this.OnImportSuccess(asset);
      }
      bool hasWarning = this.m_Errors.hasWarning;
      //  gr: here we need to start using FakeStyleSheet
      asset.importedWithErrors = hasErrors;
      asset.importedWithWarnings = hasWarning;

      if (!(hasErrors | hasWarning))
        return;
      this.OnImportError(this.m_Errors);
    }

    private void ValidateProperty(Property property)
    {
      if (this.disableValidation)
        return;
      string name = property.Name;
      string str = property.Term.ToString();

      StyleValidationResult validationResult = this.m_Validator.ValidateProperty(name, str);
      if (!validationResult.success)
      {
        string message = validationResult.message + "\n    " + name + ": " + str;
        if (!string.IsNullOrEmpty(validationResult.hint))
          message = message + " -> " + validationResult.hint;
        this.m_Errors.AddValidationWarning(message, property.Line);
      }
    }

    private void VisitSheet(ExCSS.StyleSheet styleSheet)
    {
      foreach (ExCSS.StyleRule styleRule in (IEnumerable<ExCSS.StyleRule>) styleSheet.StyleRules)
      {
        this.m_Builder.BeginRule(styleRule.Line);
        this.m_CurrentLine = styleRule.Line;
        this.VisitBaseSelector(styleRule.Selector);
        foreach (Property declaration in styleRule.Declarations)
        {
          this.m_CurrentLine = declaration.Line;
          this.ValidateProperty(declaration);
          this.m_Builder.BeginProperty(declaration.Name, declaration.Line);
          this.VisitValue(declaration.Term);
          this.m_Builder.EndProperty();
        }
        this.m_Builder.EndRule();
      }
    }

    private void VisitBaseSelector(BaseSelector selector)
    {
      switch (selector)
      {
        case AggregateSelectorList selectorList:
          this.VisitSelectorList(selectorList);
          break;
        case ComplexSelector complexSelector:
          this.VisitComplexSelector(complexSelector);
          break;
        case SimpleSelector simpleSelector:
          this.VisitSimpleSelector(simpleSelector.ToString());
          break;
      }
    }

    private void VisitSelectorList(AggregateSelectorList selectorList)
    {
      if (selectorList.Delimiter == ",")
      {
        foreach (BaseSelector selector in (SelectorList) selectorList)
          this.VisitBaseSelector(selector);
      }
      else if (selectorList.Delimiter == string.Empty)
        this.VisitSimpleSelector(selectorList.ToString());
      else
        this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidSelectorListDelimiter, string.Format(StyleValueImporter.glossary.invalidSelectorListDelimiter, (object) selectorList.Delimiter), this.m_CurrentLine);
    }

    private void VisitComplexSelector(ComplexSelector complexSelector)
    {
    throw new NotImplementedException();
    /*
      int selectorSpecificity = CSSSpec.GetSelectorSpecificity(complexSelector.ToString());
      if (selectorSpecificity == 0)
      {
        this.m_Errors.AddInternalError(string.Format(StyleValueImporter.glossary.internalError, (object) ("Failed to calculate selector specificity " + complexSelector?.ToString())), this.m_CurrentLine);
      }
      else
      {
        using (this.m_Builder.BeginComplexSelector(selectorSpecificity))
        {
          StyleSelectorRelationship previousRelationsip = StyleSelectorRelationship.None;
          foreach (CombinatorSelector combinatorSelector in complexSelector)
          {
            string simpleSelector = this.ExtractSimpleSelector(combinatorSelector.Selector);
            if (string.IsNullOrEmpty(simpleSelector))
            {
              this.m_Errors.AddInternalError(string.Format(StyleValueImporter.glossary.internalError, (object) ("Expected simple selector inside complex selector " + simpleSelector)), this.m_CurrentLine);
              break;
            }
            StyleSelectorPart[] parts;
            if (!this.CheckSimpleSelector(simpleSelector, out parts))
              break;
            this.m_Builder.AddSimpleSelector(parts, previousRelationsip);
            switch (combinatorSelector.Delimiter)
            {
              case Combinator.Child:
                previousRelationsip = StyleSelectorRelationship.Child;
                break;
              case Combinator.Descendent:
                previousRelationsip = StyleSelectorRelationship.Descendent;
                break;
              default:
                this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidComplexSelectorDelimiter, string.Format(StyleValueImporter.glossary.invalidComplexSelectorDelimiter, (object) complexSelector), this.m_CurrentLine);
                return;
            }
          }
        }
      }
      */
    }

    private void VisitSimpleSelector(string selector)
    {
      StyleSelectorPart[] parts;
      if (!this.CheckSimpleSelector(selector, out parts))
        return;
      int selectorSpecificity = CSSSpec.GetSelectorSpecificity(parts);
      if (selectorSpecificity == 0)
      {
        this.m_Errors.AddInternalError(string.Format(StyleValueImporter.glossary.internalError, (object) ("Failed to calculate selector specificity " + selector)), this.m_CurrentLine);
      }
      else
      {
        using (this.m_Builder.BeginComplexSelector(selectorSpecificity))
          this.m_Builder.AddSimpleSelector(parts, StyleSelectorRelationship.None);
      }
    }

    private string ExtractSimpleSelector(BaseSelector selector)
    {
      int num;
      switch (selector)
      {
        case SimpleSelector _:
          return selector.ToString();
        case AggregateSelectorList aggregateSelectorList:
          num = aggregateSelectorList.Delimiter == string.Empty ? 1 : 0;
          return num != 0 ? aggregateSelectorList.ToString() : string.Empty;
          break;
        default:
          num = 0;
          break;
      }
      //  gr: changed here
      //return num != 0 ? aggregateSelectorList.ToString() : string.Empty;
      return String.Empty;
    }

    private bool CheckSimpleSelector(string selector, out StyleSelectorPart[] parts)
    {
      if (!CSSSpec.ParseSelector(selector, out parts))
      {
        this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.UnsupportedSelectorFormat, string.Format(StyleValueImporter.glossary.unsupportedSelectorFormat, (object) selector), this.m_CurrentLine);
        return false;
      }
      if (((IEnumerable<StyleSelectorPart>) parts).Any<StyleSelectorPart>((Func<StyleSelectorPart, bool>) (p => p.type == StyleSelectorType.Unknown)))
      {
        this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.UnsupportedSelectorFormat, string.Format(StyleValueImporter.glossary.unsupportedSelectorFormat, (object) selector), this.m_CurrentLine);
        return false;
      }
      if (!((IEnumerable<StyleSelectorPart>) parts).Any<StyleSelectorPart>((Func<StyleSelectorPart, bool>) (p => p.type == StyleSelectorType.RecursivePseudoClass)))
        return true;
      this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.RecursiveSelectorDetected, string.Format(StyleValueImporter.glossary.unsupportedSelectorFormat, (object) selector), this.m_CurrentLine);
      return false;
    }
    

  }