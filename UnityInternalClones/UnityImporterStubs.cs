using ExCSS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.StyleSheets;

using URIHelpers = UnityEditor.UIElements.StyleSheets.URIHelpers;
using URIValidationResult = UnityEditor.UIElements.StyleSheets.URIValidationResult;


enum StyleSheetImportErrorCode
{
InvalidSelectorListDelimiter,  
UnsupportedSelectorFormat,
RecursiveSelectorDetected,
MissingFunctionAssignment,
MissingFunctionArgument,
InvalidURILocation,
InvalidURIScheme,
InvalidURIProjectAssetPath,
Internal,
InvalidHighResolutionImage,
InvalidVarFunction,
UnsupportedUnit,
UnsupportedTerm,
}
struct StyleSheetImportErrors : IEnumerable<StyleSheetImportError>
{
  public string assetPath;
  public bool hasErrors => false;
  public bool hasWarning => false;
  
  public void AddSyntaxError(string Error,int Line)
  {
  }
  public void AddValidationWarning(string Message,int Line)
  {
  }

  public void AddSemanticError(StyleSheetImportErrorCode error,string Error,int Line)
  {
  }

  public IEnumerator<StyleSheetImportError> GetEnumerator()
  {
    throw new NotImplementedException();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}

internal struct StyleValidationResult
{
  public bool success => true;
  public string message => "Validation Result String";
}

internal class StyleSheetImportError
{
  public string assetPath;
}
/*
struct StyleSelectorPart
{
}
*/
class UnityImporterContext
{
  
}

/*
internal class StyleValueImporter
{
}


public struct StyleSheetBuilder
{
  public void BuildTo(UnityEngine.UIElements.StyleSheet asset)
  {
    throw new NotImplementedException();
  }
  
  public void BeginRule(int Line)
  {
  }

  public void EndRule()
  {
  }

  public void BeginProperty(string DeclarationName,int Line)
  {
  }
  
  public void EndProperty()
  {
  }
  
}
*/