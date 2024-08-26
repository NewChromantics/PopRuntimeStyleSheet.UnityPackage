using ExCSS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using RuntimeStyleSheet.UIElements;
using RuntimeStyleSheet.UIElements.StyleSheets;

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
internal class StyleSheetImportErrors : IEnumerable<StyleSheetImportError>
{
  public string assetPath;
  public bool hasErrors => Errors.Count>0;
  public bool hasWarning => Warnings.Count>0;
  
  List<string> Errors = new();
  List<string> Warnings = new();
  
  public void AddSyntaxError(string Error,int Line)
  {
    Errors.Add(Error);
  }
  public void AddValidationWarning(string Message,int Line)
  {
    Warnings.Add(Message);
  }

  public void AddSemanticError(StyleSheetImportErrorCode error,string Error,int Line)
  {
    Errors.Add(Error);
  }
  
  public void AddInternalError(string Error,int Line)
  {
    Errors.Add(Error);
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
  public void BuildTo(RuntimeStyleSheet.UIElements.StyleSheet asset)
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