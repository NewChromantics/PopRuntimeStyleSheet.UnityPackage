// Decompiled with JetBrains decompiler
// Type: UnityEditor.UIElements.StyleSheets.URIHelpers
// Assembly: UnityEditor.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5683C24-87E1-4576-BF30-D5A994145B0E
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEditor.CoreModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEditor.CoreModule.xml

using System;
using System.IO;



namespace UnityEditor.UIElements.StyleSheets
{
  internal static class URIHelpers
  {
    private const string k_ProjectScheme = "project";
    private const string k_ProjectSchemeEmptyHint = "project://?";
    private const string k_AssetDatabaseHost = "database";
    private static readonly Uri s_ProjectRootUri = new UriBuilder("project", "").Uri;
    private static readonly Uri s_ThemeUri = new UriBuilder("unity-theme", "").Uri;
    private const string k_AttrFileId = "fileID";
    private const string k_AttrGuid = "guid";
    private const string k_AttrType = "type";
    private const string k_ContainerRefName = "o";
    private static readonly URIHelpers.PPtrContainer s_PPtrContainer = new URIHelpers.PPtrContainer();
    private static readonly URIHelpers.RawPPtrContainer s_RawPPtrContainer = new URIHelpers.RawPPtrContainer();
    private static readonly char[] s_Separator = new char[1]
    {
      '&'
    };

    public static string MakeAssetUri(UnityEngine.Object asset) => URIHelpers.MakeAssetUri(asset, false);

    public static string MakeAssetUri(UnityEngine.Object asset, bool compact)
    {
      if (!(bool) asset)
        return (string) null;
      URIHelpers.PPtrContainer pptrContainer = URIHelpers.s_PPtrContainer;
      pptrContainer.o = asset;
      string json = EditorJsonUtility.ToJson((object) pptrContainer);
      URIHelpers.RawPPtrContainer rawPptrContainer = URIHelpers.s_RawPPtrContainer;
      EditorJsonUtility.FromJsonOverwrite(json, (object) rawPptrContainer);
      string assetPath = AssetDatabase.GetAssetPath(asset);
      string str = string.Format("{0}={1}&{2}={3}&{4}={5}", (object) "fileID", (object) rawPptrContainer.o.fileID, (object) "guid", (object) rawPptrContainer.o.guid, (object) "type", (object) rawPptrContainer.o.type);
      if (compact)
        return "?" + str;
      string name = asset.name;
      return new UriBuilder("project", "database", -1, assetPath)
      {
        Query = str,
        Fragment = name
      }.ToString();
    }

    public static string EncodeUri(string uri)
    {
      uri = uri.Replace("&", "&amp;");
      uri = uri.Replace("\"", "&quot;");
      uri = uri.Replace("'", "&apos;");
      uri = uri.Replace("<", "&lt;");
      uri = uri.Replace(">", "&gt;");
      uri = uri.Replace("\n", "&#10;");
      uri = uri.Replace("\r", "");
      uri = uri.Replace("\t", "&#x9;");
      return uri;
    }

    public static URIValidationResult ValidAssetURL(
      string assetPath,
      string path,
      out string errorToken,
      out string resolvedProjectRelativePath)
    {
      URIHelpers.URIValidationResponse validationResponse = URIHelpers.ValidateAssetURL(assetPath, path);
      errorToken = validationResponse.errorToken;
      resolvedProjectRelativePath = validationResponse.resolvedProjectRelativePath;
      return validationResponse.result;
    }

    public static URIValidationResult ValidAssetURL(
      string assetPath,
      string path,
      out string errorToken,
      out string resolvedProjectRelativePath,
      out string resolvedSubAssetPath)
    {
      URIHelpers.URIValidationResponse validationResponse = URIHelpers.ValidateAssetURL(assetPath, path);
      errorToken = validationResponse.errorToken;
      resolvedProjectRelativePath = validationResponse.resolvedProjectRelativePath;
      resolvedSubAssetPath = validationResponse.resolvedSubAssetPath;
      return validationResponse.result;
    }

    public static URIHelpers.URIValidationResponse ValidateAssetURL(
      string assetPath,
      string path)
    {
      URIHelpers.URIValidationResponse validationResponse = new URIHelpers.URIValidationResponse();
      if (string.IsNullOrEmpty(path))
      {
        validationResponse.errorToken = "''";
        validationResponse.result = URIValidationResult.InvalidURILocation;
        return validationResponse;
      }
      string str1 = path;
      validationResponse.resolvedSubAssetPath = URIHelpers.ExtractUrlFragment(ref path);
      Uri result = (Uri) null;
      bool flag1 = path.StartsWith("unity-theme://");
      if (path.StartsWith("/"))
        result = new UriBuilder(URIHelpers.s_ProjectRootUri.Scheme, "", 0, path).Uri;
      else if (flag1)
      {
        string pathValue = path.Substring((URIHelpers.s_ThemeUri.Scheme + "://").Length);
        result = new UriBuilder(URIHelpers.s_ThemeUri.Scheme, "", -1, pathValue).Uri;
      }
      else if (!Uri.TryCreate(path, UriKind.Absolute, out result))
      {
        if (!Uri.TryCreate(new Uri(URIHelpers.s_ProjectRootUri, assetPath), path, out result))
        {
          validationResponse.errorToken = assetPath;
          validationResponse.result = URIValidationResult.InvalidURILocation;
          return validationResponse;
        }
      }
      else if (result.Scheme != URIHelpers.s_ProjectRootUri.Scheme)
      {
        validationResponse.errorToken = result.Scheme;
        validationResponse.result = URIValidationResult.InvalidURIScheme;
        return validationResponse;
      }
      string str2 = Uri.UnescapeDataString(result.AbsolutePath);
      if (str2.StartsWith("/"))
        str2 = str2.Substring(1);
      if (flag1)
      {
        if (string.IsNullOrEmpty(path))
        {
          validationResponse.errorToken = string.Empty;
          validationResponse.result = URIValidationResult.InvalidURIProjectAssetPath;
          return validationResponse;
        }
        validationResponse.resolvedProjectRelativePath = path;
      }
      else
      {
        validationResponse.resolvedProjectRelativePath = str2;
        URIHelpers.UriQueryParameters uriQueryParameters = URIHelpers.ExtractUriQueryParameters(result);
        if (uriQueryParameters.hasAllReferenceParams)
        {
          string json = string.Format("{{\"{0}\":{{\"{1}\": {2}, \"{3}\":\"{4}\", \"{5}\": {6}}}}}", (object) "o", (object) "fileID", (object) uriQueryParameters.fileId, (object) "guid", (object) uriQueryParameters.guid, (object) "type", (object) uriQueryParameters.type);
          URIHelpers.PPtrContainer pptrContainer = URIHelpers.s_PPtrContainer;
          EditorJsonUtility.FromJsonOverwrite(json, (object) pptrContainer);
          UnityEngine.Object o = pptrContainer.o;
          validationResponse.resolvedQueryAsset = o;
          bool flag2 = str1.StartsWith("?", StringComparison.Ordinal) || str1.StartsWith("project://?", StringComparison.Ordinal);
          if (!(bool) o)
          {
            string assetPath1 = AssetDatabase.GUIDToAssetPath(uriQueryParameters.guid);
            if (!string.IsNullOrEmpty(assetPath1))
            {
              if (assetPath1 != validationResponse.resolvedProjectRelativePath)
              {
                if (!flag2)
                  validationResponse.warningMessage = string.Format(L10n.Tr("Asset reference to GUID '{0}' resolved to '{2}', but URL path hints at '{1}'. Update the URL '{3}' to remove this warning."), (object) uriQueryParameters.guid, (object) validationResponse.resolvedProjectRelativePath, (object) assetPath1, (object) str1);
                validationResponse.resolvedProjectRelativePath = assetPath1;
              }
            }
            else if (URIHelpers.AssetExistsAtPath(validationResponse.resolvedProjectRelativePath))
            {
              validationResponse.warningMessage = string.Format(L10n.Tr("Could not resolve asset with GUID '{0}' and file ID '{1}' from URL '{2}'. Using asset path '{3}' instead. Update the URL to remove this warning."), (object) uriQueryParameters.guid, (object) uriQueryParameters.fileId, (object) path, (object) validationResponse.resolvedProjectRelativePath);
            }
            else
            {
              validationResponse.warningMessage = string.Format(L10n.Tr("Invalid asset path hint \"{0}\" for referenced asset GUID \"{1}\""), (object) validationResponse.resolvedProjectRelativePath, (object) uriQueryParameters.guid);
              validationResponse.errorToken = str1;
              validationResponse.result = URIValidationResult.InvalidURIProjectAssetPath;
              return validationResponse;
            }
          }
          else
          {
            string assetPath2 = AssetDatabase.GetAssetPath(o);
            if (!flag2 && !string.IsNullOrEmpty(validationResponse.resolvedProjectRelativePath) && assetPath2 != validationResponse.resolvedProjectRelativePath)
            {
              if (URIHelpers.AssetExistsAtPath(validationResponse.resolvedProjectRelativePath))
                validationResponse.warningMessage = string.Format(L10n.Tr("Ambiguous asset reference detected. Asset reference to GUID '{0}' resolved to '{2}', but URL path hints at '{1}', which is also valid asset path. Update the URL '{3}' to remove this warning."), (object) uriQueryParameters.guid, (object) validationResponse.resolvedProjectRelativePath, (object) assetPath2, (object) str1);
              else
                validationResponse.warningMessage = string.Format(L10n.Tr("Asset reference to GUID '{0}' was moved from '{1}' to '{2}'. Update the URL '{3}' to remove this warning."), (object) uriQueryParameters.guid, (object) validationResponse.resolvedProjectRelativePath, (object) assetPath2, (object) str1);
            }
            validationResponse.resolvedProjectRelativePath = assetPath2;
            string name = o.name;
            if (!string.IsNullOrEmpty(validationResponse.resolvedSubAssetPath) && name != validationResponse.resolvedSubAssetPath)
              validationResponse.warningMessage = string.Format(L10n.Tr("Asset reference to GUID '{0}' and file ID '{1}' was renamed from '{2}' to '{3}'. Update the URL '{4}' to remove this warning."), (object) uriQueryParameters.guid, (object) uriQueryParameters.fileId, (object) validationResponse.resolvedSubAssetPath, (object) name, (object) str1);
            validationResponse.resolvedSubAssetPath = name;
          }
        }
        else if (!URIHelpers.AssetExistsAtPath(validationResponse.resolvedProjectRelativePath))
        {
          validationResponse.errorToken = string.IsNullOrEmpty(validationResponse.resolvedProjectRelativePath) ? str1 : validationResponse.resolvedProjectRelativePath;
          validationResponse.result = URIValidationResult.InvalidURIProjectAssetPath;
          return validationResponse;
        }
      }
      validationResponse.result = URIValidationResult.OK;
      return validationResponse;
    }

    private static bool AssetExistsAtPath(string path) => !AssetDatabase.GUIDFromAssetPath(path).Empty() || File.Exists(path);

    private static URIHelpers.UriQueryParameters ExtractUriQueryParameters(Uri uri)
    {
      URIHelpers.UriQueryParameters uriQueryParameters = new URIHelpers.UriQueryParameters();
      string query = uri.Query;
      if (query == null || query.Length <= 1)
        return uriQueryParameters;
      string str1 = query.Substring(1);
      uriQueryParameters.query = str1;
      uriQueryParameters.hasQuery = true;
      uriQueryParameters.hasValidQuery = true;
      foreach (string str2 in str1.Split(URIHelpers.s_Separator))
      {
        int length = str2.IndexOf('=');
        if (length > 0 && length < str2.Length - 1)
        {
          string str3 = str2.Substring(0, length);
          string s = str2.Substring(length + 1);
          string str4 = str3;
          if (!(str4 == "guid"))
          {
            if (!(str4 == "fileID"))
            {
              if (str4 == "type")
                uriQueryParameters.hasType = int.TryParse(s, out uriQueryParameters.type);
              else
                uriQueryParameters.hasExtraQueryParams = true;
            }
            else
              uriQueryParameters.hasFileId = long.TryParse(s, out uriQueryParameters.fileId);
          }
          else
          {
            uriQueryParameters.guid = s;
            uriQueryParameters.hasGuid = !string.IsNullOrEmpty(uriQueryParameters.guid);
          }
        }
        else
        {
          uriQueryParameters.hasValidQuery = false;
          return uriQueryParameters;
        }
      }
      return uriQueryParameters;
    }

    private static string ExtractUrlFragment(ref string path)
    {
      int length = path.LastIndexOf('#');
      if (length == -1)
        return string.Empty;
      string urlFragment = Uri.UnescapeDataString(path.Substring(length + 1));
      path = path.Substring(0, length);
      return urlFragment;
    }

    public static string InjectFileNameSuffix(string path, string suffix)
    {
      string extension = Path.GetExtension(path);
      string path2 = Path.GetFileNameWithoutExtension(path) + suffix + extension;
      return Path.Combine(Path.GetDirectoryName(path), path2);
    }

    public struct URIValidationResponse
    {
      public URIValidationResult result;
      public string errorToken;
      public string warningMessage;
      public string resolvedProjectRelativePath;
      public string resolvedSubAssetPath;
      public UnityEngine.Object resolvedQueryAsset;

      public bool hasWarningMessage => !string.IsNullOrEmpty(this.warningMessage);

      public bool isLibraryAsset
      {
        get
        {
          string projectRelativePath = this.resolvedProjectRelativePath;
          return projectRelativePath != null && projectRelativePath.StartsWith("Library/", StringComparison.Ordinal);
        }
      }
    }

    private class PPtrContainer
    {
      public UnityEngine.Object o;
    }

    [Serializable]
    private struct RawPPtrReference
    {
      public long fileID;
      public string guid;
      public int type;
    }

    private class RawPPtrContainer
    {
      public URIHelpers.RawPPtrReference o;
    }

    private struct UriQueryParameters
    {
      public string query;
      public string guid;
      public long fileId;
      public int type;
      public bool hasQuery;
      public bool hasValidQuery;
      public bool hasExtraQueryParams;
      public bool hasGuid;
      public bool hasFileId;
      public bool hasType;

      public bool hasAllReferenceParams => this.hasValidQuery && this.hasGuid && this.hasFileId && this.hasType;
    }
  }
}
