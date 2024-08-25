// Decompiled with JetBrains decompiler
// Type: UnityEditor.UIElements.StyleSheets.StyleSheetImportGlossary
// Assembly: UnityEditor.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5683C24-87E1-4576-BF30-D5A994145B0E
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEditor.CoreModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEditor.CoreModule.xml

namespace UnityEditor.UIElements.StyleSheets
{
  internal class StyleSheetImportGlossary
  {
    public readonly string internalError = L10n.Tr("Internal import error: {0}");
    public readonly string internalErrorWithStackTrace = L10n.Tr("Internal import error: {0}\n{1}");
    public readonly string error = L10n.Tr(nameof (error));
    public readonly string warning = L10n.Tr(nameof (warning));
    public readonly string line = L10n.Tr(nameof (line));
    public readonly string unsupportedUnit = L10n.Tr("Unsupported unit: '{0}'");
    public readonly string ussParsingError = L10n.Tr("USS parsing error: {0}");
    public readonly string unsupportedTerm = L10n.Tr("Unsupported USS term: {0}");
    public readonly string missingFunctionArgument = L10n.Tr("Missing function argument: '{0}'");
    public readonly string missingVariableName = L10n.Tr("Missing variable name");
    public readonly string emptyVariableName = L10n.Tr("Empty variable name");
    public readonly string tooManyFunctionArguments = L10n.Tr("Too many function arguments");
    public readonly string emptyFunctionArgument = L10n.Tr("Empty function argument");
    public readonly string unexpectedTokenInFunction = L10n.Tr("Expected ',', got '{0}'");
    public readonly string missingVariablePrefix = L10n.Tr("Variable '{0}' is missing '--' prefix");
    public readonly string invalidHighResAssetType = L10n.Tr("Unsupported type {0} for asset at path '{1}' ; only Texture2D is supported for variants with @2x suffix\nSuggestion: verify the import settings of this asset.");
    public readonly string invalidSelectorListDelimiter = L10n.Tr("Invalid selector list delimiter: '{0}'");
    public readonly string invalidComplexSelectorDelimiter = L10n.Tr("Invalid complex selector delimiter: '{0}'");
    public readonly string unsupportedSelectorFormat = L10n.Tr("Unsupported selector format: '{0}'");
    public readonly string unknownFunction = L10n.Tr("Unknown function '{0}' in declaration '{1}: {0}'");
    public readonly string circularImport = L10n.Tr("Circular @import dependencies detected. All @import directives will be ignored for this StyleSheet.");
    public readonly string invalidUriLocation = L10n.Tr("Invalid URI location: '{0}'");
    public readonly string invalidUriScheme = L10n.Tr("Invalid URI scheme: '{0}'");
    public readonly string invalidAssetPath = L10n.Tr("Invalid asset path: '{0}'");
    public readonly string invalidAssetType = L10n.Tr("Unsupported type {0} for asset at path '{1}' ; only the following types are supported: {2}\nSuggestion: verify the import settings of this asset.");
  }
}
