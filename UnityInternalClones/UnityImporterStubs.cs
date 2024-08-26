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
  
  public void ThrowErrors()
  {
    if ( !hasErrors )
      return;
    var OverallError = String.Join(",",Errors);
    throw new Exception($"CSS Import Errors: {OverallError}");
  }
  
  public void AddSyntaxError(string Error,int Line)
  {
    Errors.Add(Error);
  }
  public void AddValidationWarning(string Message,int Line)
  {
    Warnings.Add(Message);
  }

  public void AddSemanticWarning(StyleSheetImportErrorCode warning,string Message, int Line)
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


internal class StyleSheetImportError
{
  public string assetPath;
}

class UnityImporterContext
{
  
}
