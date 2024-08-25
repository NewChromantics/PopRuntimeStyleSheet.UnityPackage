// Decompiled with JetBrains decompiler
// Type: UnityEditor.UIElements.StyleSheets.StyleValueImporter
// Assembly: UnityEditor.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5683C24-87E1-4576-BF30-D5A994145B0E
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEditor.CoreModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEditor.CoreModule.xml

using ExCSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEditor.UIElements.StyleSheets
{
  internal abstract class StyleValueImporter
  {
    private static StyleSheetImportGlossary s_Glossary;
    private const string k_ResourcePathFunctionName = "resource";
    private const string k_VariableFunctionName = "var";
    protected readonly AssetImportContext m_Context;
    protected readonly ExCSS.Parser m_Parser;
    protected readonly StyleSheetBuilder m_Builder;
    protected readonly StyleSheetImportErrors m_Errors;
    protected readonly StyleValidator m_Validator;
    protected string m_AssetPath;
    protected int m_CurrentLine;
    private static readonly string kThemePrefix = "unity-theme://";
    private static Dictionary<string, StyleValueKeyword> s_NameCache;

    internal static StyleSheetImportGlossary glossary => StyleValueImporter.s_Glossary ?? (StyleValueImporter.s_Glossary = new StyleSheetImportGlossary());

    public StyleValueImporter(AssetImportContext context)
    {
      this.m_Context = context != null ? context : throw new ArgumentNullException(nameof (context));
      this.m_AssetPath = context.assetPath;
      this.m_Parser = new ExCSS.Parser();
      this.m_Builder = new StyleSheetBuilder();
      this.m_Errors = new StyleSheetImportErrors()
      {
        assetPath = context.assetPath
      };
      this.m_Validator = new StyleValidator();
    }

    internal StyleValueImporter()
    {
      this.m_Context = (AssetImportContext) null;
      this.m_AssetPath = (string) null;
      this.m_Parser = new ExCSS.Parser();
      this.m_Builder = new StyleSheetBuilder();
      this.m_Errors = new StyleSheetImportErrors();
      this.m_Validator = new StyleValidator();
    }

    public bool disableValidation { get; set; }

    public StyleSheetImportErrors importErrors => this.m_Errors;

    public string assetPath => this.m_AssetPath;

    public virtual UnityEngine.Object DeclareDependencyAndLoad(string path) => this.DeclareDependencyAndLoad(path, (string) null);

    public virtual UnityEngine.Object DeclareDependencyAndLoad(
      string path,
      string subAssetPath)
    {
      if (path.StartsWith(StyleValueImporter.kThemePrefix))
      {
      throw new NotImplementedException();
      /*
        string key = path.Substring(StyleValueImporter.kThemePrefix.Length);
        string path1;
        if (!ThemeRegistry.themes.TryGetValue(key, out path1))
          return (UnityEngine.Object) null;
        UnityEngine.Object original = EditorGUIUtility.Load(path1);
        Debug.Assert((original != (UnityEngine.Object) null ? 1 : 0) != 0, "Theme not found searching for '" + key + "' at <" + path1 + ">.");
        if (original != (UnityEngine.Object) null)
        {
          List<UnityEngine.Object> objectList = StyleValueImporter.DeepCopyAsset(original);
          if (objectList.Count > 0)
          {
            objectList[0].name = key;
            int num = 0;
            foreach (UnityEngine.Object @object in objectList)
              this.m_Context.AddObjectToAsset(string.Format("asset {0}: clonedAsset.name", (object) num++), @object);
            return objectList[0];
          }
        }
        */
        return (UnityEngine.Object) null;
      }
      this.m_Context?.DependsOnSourceAsset(path);
      if (string.IsNullOrEmpty(subAssetPath))
        return AssetDatabase.LoadMainAssetAtPath(path);
      UnityEngine.Object object1 = AssetDatabase.LoadMainAssetAtPath(path);
      foreach (UnityEngine.Object object2 in AssetDatabase.LoadAllAssetsAtPath(path))
      {
        if (!(object2 == object1) && object2.name == subAssetPath)
          return object2;
      }
      return object1 != (UnityEngine.Object) null && object1.name == subAssetPath ? object1 : (UnityEngine.Object) null;
    }

    private static UnityEngine.Object LoadResource(string path) => throw new NotImplementedException();//StyleSheetResourceUtil.LoadResource(path, typeof (UnityEngine.Object));

    internal static List<UnityEngine.Object> DeepCopyAsset(UnityEngine.Object original)
    {
      UnityEngine.UIElements.StyleSheet original1 = original as UnityEngine.UIElements.StyleSheet;
      if ((UnityEngine.Object) original1 == (UnityEngine.Object) null)
        return new List<UnityEngine.Object>();


      UnityEngine.UIElements.StyleSheet styleSheet = UnityEngine.Object.Instantiate<UnityEngine.UIElements.StyleSheet>(original1);
      
      Dictionary<UnityEngine.Object, List<UnityEngine.Object>> dictionary1 = new Dictionary<UnityEngine.Object, List<UnityEngine.Object>>();
      List<UnityEngine.Object> objectList1 = new List<UnityEngine.Object>();
      List<ScalableImage> scalableImageList1 = new List<ScalableImage>();
      for (int index = 0; index < styleSheet.assets.Length; ++index)
      {
        UnityEngine.Object asset = styleSheet.assets[index];
        List<UnityEngine.Object> objectList2 = (List<UnityEngine.Object>) null;
        if (!dictionary1.TryGetValue(asset, out objectList2))
        {
          objectList2 = StyleValueImporter.CloneAsset(asset);
          if (objectList2.Count > 0)
            dictionary1[asset] = objectList2;
        }
        throw new NotImplementedException();
        /*
        // ISSUE: explicit non-virtual call
        if (objectList2 != null && __nonvirtual (objectList2.Count) > 0)
          objectList1.Add(objectList2[0]);
          */
      }
      ScalableImage scalableImage1;
      for (int index = 0; index < styleSheet.scalableImages.Length; ++index)
      {
        ScalableImage scalableImage2 = styleSheet.scalableImages[index];
        List<UnityEngine.Object> objectList3 = (List<UnityEngine.Object>) null;
        if (!dictionary1.TryGetValue((UnityEngine.Object) scalableImage2.normalImage, out objectList3))
        {
          List<UnityEngine.Object> objectList4 = StyleValueImporter.CloneAsset((UnityEngine.Object) scalableImage2.normalImage);
          List<UnityEngine.Object> objectList5 = StyleValueImporter.CloneAsset((UnityEngine.Object) scalableImage2.highResolutionImage);
          if (objectList4.Count > 0 && objectList5.Count > 0)
          {
            objectList3 = new List<UnityEngine.Object>()
            {
              objectList4[0],
              objectList5[0]
            };
            dictionary1[(UnityEngine.Object) scalableImage2.normalImage] = objectList3;
          }
        }
        // ISSUE: explicit non-virtual call
        if (objectList3 != null && /*__nonvirtual */(objectList3.Count) > 0)
        {
          List<ScalableImage> scalableImageList2 = scalableImageList1;
          scalableImage1 = new ScalableImage();
          scalableImage1.normalImage = objectList3[0] as Texture2D;
          scalableImage1.highResolutionImage = objectList3[1] as Texture2D;
          ScalableImage scalableImage3 = scalableImage1;
          scalableImageList2.Add(scalableImage3);
        }
      }
      Dictionary<string, StyleValueImporter.StoredAsset> dictionary2 = new Dictionary<string, StyleValueImporter.StoredAsset>();
      Dictionary<string, StyleValueImporter.StoredAsset> dictionary3 = new Dictionary<string, StyleValueImporter.StoredAsset>();
      foreach (UnityEngine.UIElements.StyleRule rule in styleSheet.rules)
      {
        foreach (StyleProperty property in rule.properties)
        {
          for (int index = 0; index < property.values.Length; ++index)
          {
            StyleValueHandle styleValueHandle = property.values[index];
            if (styleValueHandle.valueType == StyleValueType.ResourcePath)
            {
              string str = styleSheet.strings[styleValueHandle.valueIndex];
              bool flag1 = false;
              int num1 = -1;
              bool flag2 = false;
              int num2 = -1;
              StyleValueImporter.StoredAsset storedAsset1;
              if (dictionary3.TryGetValue(str, out storedAsset1))
              {
                num2 = storedAsset1.index;
                flag2 = true;
              }
              else if (dictionary2.TryGetValue(str, out storedAsset1))
              {
                num1 = storedAsset1.index;
                flag1 = true;
              }
              else
              {
                UnityEngine.Object object1 = StyleValueImporter.LoadResource(str);
                List<UnityEngine.Object> collection = StyleValueImporter.CloneAsset(object1);
                dictionary1[object1] = collection;
                StyleValueImporter.StoredAsset storedAsset2;
                if (object1 is Texture2D)
                {
                  UnityEngine.Object o = StyleValueImporter.LoadResource(Path.Combine(Path.GetDirectoryName(str), Path.GetFileNameWithoutExtension(str) + "@2x" + Path.GetExtension(str)));
                  if (o != (UnityEngine.Object) null)
                  {
                    num2 = scalableImageList1.Count;
                    List<UnityEngine.Object> objectList6 = StyleValueImporter.CloneAsset(o);
                    List<ScalableImage> scalableImageList3 = scalableImageList1;
                    scalableImage1 = new ScalableImage();
                    scalableImage1.normalImage = collection[0] as Texture2D;
                    scalableImage1.highResolutionImage = objectList6[0] as Texture2D;
                    ScalableImage scalableImage4 = scalableImage1;
                    scalableImageList3.Add(scalableImage4);
                    Dictionary<string, StyleValueImporter.StoredAsset> dictionary4 = dictionary3;
                    string key = str;
                    storedAsset2 = new StyleValueImporter.StoredAsset();
                    storedAsset2.si = scalableImageList1[scalableImageList1.Count - 1];
                    storedAsset2.index = num2;
                    StyleValueImporter.StoredAsset storedAsset3 = storedAsset2;
                    dictionary4[key] = storedAsset3;
                    collection.Add(objectList6[0]);
                    dictionary1[object1] = collection;
                    flag2 = true;
                  }
                }
                if (!flag2 && collection.Count > 0)
                {
                  num1 = objectList1.Count;
                  objectList1.AddRange((IEnumerable<UnityEngine.Object>) collection);
                  UnityEngine.Object object2 = collection[0];
                  Dictionary<string, StyleValueImporter.StoredAsset> dictionary5 = dictionary2;
                  string key = str;
                  storedAsset2 = new StyleValueImporter.StoredAsset();
                  storedAsset2.resource = object2;
                  storedAsset2.index = num1;
                  StyleValueImporter.StoredAsset storedAsset4 = storedAsset2;
                  dictionary5[key] = storedAsset4;
                  flag1 = true;
                }
              }
              if (flag1)
              {
                styleValueHandle.valueType = StyleValueType.AssetReference;
                styleValueHandle.valueIndex = num1;
                property.values[index] = styleValueHandle;
              }
              else if (flag2)
              {
                styleValueHandle.valueType = StyleValueType.ScalableImage;
                styleValueHandle.valueIndex = num2;
                property.values[index] = styleValueHandle;
              }
              else
                Debug.LogError((object) ("ResourcePath was not converted to AssetReference when converting stylesheet :  " + str));
            }
          }
        }
      }
      styleSheet.assets = objectList1.ToArray();
      styleSheet.scalableImages = scalableImageList1.ToArray();
      HashSet<UnityEngine.Object> source = new HashSet<UnityEngine.Object>();
      foreach (List<UnityEngine.Object> objectList7 in dictionary1.Values)
      {
        foreach (UnityEngine.Object @object in objectList7)
          source.Add(@object);
      }
      List<UnityEngine.Object> list = source.ToList<UnityEngine.Object>();
      throw new NotImplementedException();
      //list.Insert(0, (UnityEngine.Object) styleSheet);
      return list;
    }

    private static List<UnityEngine.Object> CloneAsset(UnityEngine.Object o)
    {
      if (o == (UnityEngine.Object) null)
        return (List<UnityEngine.Object>) null;
      List<UnityEngine.Object> objectList = new List<UnityEngine.Object>();
      switch (o)
      {
        case Texture2D _:
          Texture2D dest1 = new Texture2D(0, 0);
          EditorUtility.CopySerialized(o, (UnityEngine.Object) dest1);
          objectList.Add((UnityEngine.Object) dest1);
          break;
        case Font _:
          Font dest2 = new Font();
          EditorUtility.CopySerialized(o, (UnityEngine.Object) dest2);
          dest2.hideFlags = HideFlags.None;
          objectList.Add((UnityEngine.Object) dest2);
          if ((UnityEngine.Object) dest2.material != (UnityEngine.Object) null)
          {
            Material dest3 = new Material(dest2.material.shader);
            EditorUtility.CopySerialized((UnityEngine.Object) dest2.material, (UnityEngine.Object) dest3);
            dest3.hideFlags = HideFlags.None;
            dest2.material = dest3;
            objectList.Add((UnityEngine.Object) dest3);
            if ((UnityEngine.Object) dest3.mainTexture != (UnityEngine.Object) null)
            {
              Texture2D dest4 = new Texture2D(0, 0);
              EditorUtility.CopySerialized((UnityEngine.Object) dest3.mainTexture, (UnityEngine.Object) dest4);
              dest4.hideFlags = HideFlags.None;
              dest3.mainTexture = (Texture) dest4;
              objectList.Add((UnityEngine.Object) dest4);
            }
          }
          using (SerializedObject serializedObject = new SerializedObject((UnityEngine.Object) dest2))
          {
            UnityEngine.Object objectReferenceValue = serializedObject.FindProperty("m_Texture").objectReferenceValue;
            if (objectReferenceValue != (UnityEngine.Object) null)
            {
              if ((UnityEngine.Object) dest2.material != (UnityEngine.Object) null && objectReferenceValue == (UnityEngine.Object) (o as Font).material.mainTexture)
              {
                serializedObject.FindProperty("m_Texture").objectReferenceValue = (UnityEngine.Object) dest2.material.mainTexture;
              }
              else
              {
                Texture2D dest5 = new Texture2D(0, 0);
                EditorUtility.CopySerialized(objectReferenceValue, (UnityEngine.Object) dest5);
                dest5.hideFlags = HideFlags.None;
                serializedObject.FindProperty("m_Texture").objectReferenceValue = (UnityEngine.Object) dest2.material.mainTexture;
                objectList.Add((UnityEngine.Object) dest5);
              }
              serializedObject.ApplyModifiedProperties();
            }
          }
          break;
      }
      return objectList;
    }

    protected void VisitResourceFunction(GenericFunction funcTerm)
    {
      if (!(funcTerm.Arguments.FirstOrDefault<Term>() is PrimitiveTerm primitiveTerm))
        this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.MissingFunctionArgument, funcTerm.Name, this.m_CurrentLine);
      else
        this.m_Builder.AddValue(primitiveTerm.Value as string, StyleValueType.ResourcePath);
    }

    internal static (StyleSheetImportErrorCode, string) ConvertErrorCode(
      URIValidationResult result)
    {
      switch (result)
      {
        case URIValidationResult.InvalidURILocation:
          return (StyleSheetImportErrorCode.InvalidURILocation, StyleValueImporter.glossary.invalidUriLocation);
        case URIValidationResult.InvalidURIScheme:
          return (StyleSheetImportErrorCode.InvalidURIScheme, StyleValueImporter.glossary.invalidUriScheme);
        case URIValidationResult.InvalidURIProjectAssetPath:
          return (StyleSheetImportErrorCode.InvalidURIProjectAssetPath, StyleValueImporter.glossary.invalidAssetPath);
        default:
          return (StyleSheetImportErrorCode.Internal, StyleValueImporter.glossary.internalErrorWithStackTrace);
      }
    }

    protected void VisitUrlFunction(PrimitiveTerm term)
    {
      string path1 = (string) term.Value;
      URIHelpers.URIValidationResponse validationResponse = URIHelpers.ValidateAssetURL(this.assetPath, path1);
      if (validationResponse.hasWarningMessage)
        this.m_Errors.AddValidationWarning(validationResponse.warningMessage, this.m_CurrentLine);
      if (validationResponse.result != 0)
      {
        string format = StyleValueImporter.ConvertErrorCode(validationResponse.result).Item2;
        this.m_Builder.AddValue(path1, StyleValueType.MissingAssetReference);
        this.m_Errors.AddValidationWarning(string.Format(format, (object) validationResponse.errorToken), this.m_CurrentLine);
      }
      else
      {
        string projectRelativePath = validationResponse.resolvedProjectRelativePath;
        string resolvedSubAssetPath = validationResponse.resolvedSubAssetPath;
        UnityEngine.Object object1 = validationResponse.resolvedQueryAsset;
        if ((bool) object1)
        {
          if (validationResponse.isLibraryAsset)
          {
            this.m_Builder.AddValue(object1);
            return;
          }
          this.m_Context?.DependsOnSourceAsset(projectRelativePath);
        }
        else
          object1 = this.DeclareDependencyAndLoad(projectRelativePath, resolvedSubAssetPath);
        bool flag = object1 is Texture2D;
        Sprite sprite = object1 as Sprite;
        if (flag && string.IsNullOrEmpty(resolvedSubAssetPath))
          sprite = AssetDatabase.LoadAssetAtPath<Sprite>(projectRelativePath);
        if (object1 != (UnityEngine.Object) null)
        {
          if (flag)
          {
            string path2 = URIHelpers.InjectFileNameSuffix(projectRelativePath, "@2x");
            if (File.Exists(path2))
            {
              UnityEngine.Object object2 = this.DeclareDependencyAndLoad(path2);
              if (object2 is Texture2D)
              {
                this.m_Builder.AddValue(new ScalableImage()
                {
                  normalImage = object1 as Texture2D,
                  highResolutionImage = object2 as Texture2D
                });
                return;
              }
              this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidHighResolutionImage, string.Format(StyleValueImporter.glossary.invalidHighResAssetType, (object) object1.GetType().Name, (object) projectRelativePath), this.m_CurrentLine);
              return;
            }
            if ((UnityEngine.Object) sprite != (UnityEngine.Object) null)
              this.DeclareDependencyAndLoad(path2);
          }
          UnityEngine.Object object3 = (UnityEngine.Object) sprite != (UnityEngine.Object) null ? (UnityEngine.Object) sprite : object1;
          this.m_Builder.AddValue(object3);
          if (!this.disableValidation)
          {
            StylePropertyName stylePropertyName = new StylePropertyName(this.m_Builder.currentProperty.name);
            if (stylePropertyName.id == StylePropertyId.Unknown)
              return;
            IEnumerable<System.Type> typesForProperty = StylePropertyUtil.GetAllowedAssetTypesForProperty(stylePropertyName.id);
            if (!typesForProperty.Any<System.Type>())
              return;
            System.Type assetType = object3.GetType();
            if (!typesForProperty.Any<System.Type>((Func<System.Type, bool>) (t => t.IsAssignableFrom(assetType))))
            {
              string str = string.Join(", ", typesForProperty.Select<System.Type, string>((Func<System.Type, string>) (t => t.Name)));
              this.m_Errors.AddValidationWarning(string.Format(StyleValueImporter.glossary.invalidAssetType, (object) assetType.Name, (object) projectRelativePath, (object) str), this.m_CurrentLine);
            }
          }
        }
        else
        {
          string format = StyleValueImporter.ConvertErrorCode(URIValidationResult.InvalidURIProjectAssetPath).Item2;
          this.m_Builder.AddValue(path1, StyleValueType.MissingAssetReference);
          this.m_Errors.AddValidationWarning(string.Format(format, (object) path1), this.m_CurrentLine);
        }
      }
    }

    private bool ValidateFunction(GenericFunction term, out StyleValueFunction func)
    {
      func = StyleValueFunction.Unknown;
      if (term.Arguments.Length == 0)
      {
        this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.MissingFunctionArgument, string.Format(StyleValueImporter.glossary.missingFunctionArgument, (object) term.Name), this.m_CurrentLine);
        return false;
      }
      if (term.Name == "var")
      {
        func = StyleValueFunction.Var;
        return this.ValidateVarFunction(term);
      }
      try
      {
        func = StyleValueFunctionExtension.FromUssString(term.Name);
      }
      catch (Exception ex)
      {
        StyleProperty currentProperty = this.m_Builder.currentProperty;
        this.m_Errors.AddValidationWarning(string.Format(StyleValueImporter.glossary.unknownFunction, (object) term.Name, (object) currentProperty.name), currentProperty.line);
        return false;
      }
      return true;
    }

    private bool ValidateVarFunction(GenericFunction term)
    {
      int length = term.Arguments.Length;
      Term term1 = term.Arguments[0];
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < length; ++index)
      {
        Term term2 = term.Arguments[index];
        if (!(term2.GetType() == typeof (Whitespace)))
        {
          if (!flag1)
          {
            string str = (term.Arguments[index] is PrimitiveTerm primitiveTerm ? primitiveTerm.Value : (object) null) as string;
            if (string.IsNullOrEmpty(str))
            {
              this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidVarFunction, StyleValueImporter.glossary.missingVariableName, this.m_CurrentLine);
              return false;
            }
            if (!str.StartsWith("--"))
            {
              this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidVarFunction, string.Format(StyleValueImporter.glossary.missingVariablePrefix, (object) str), this.m_CurrentLine);
              return false;
            }
            if (str.Length < 3)
            {
              this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidVarFunction, StyleValueImporter.glossary.emptyVariableName, this.m_CurrentLine);
              return false;
            }
            flag1 = true;
          }
          else if (term2.GetType() == typeof (Comma))
          {
            if (flag2)
            {
              this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidVarFunction, StyleValueImporter.glossary.tooManyFunctionArguments, this.m_CurrentLine);
              return false;
            }
            flag2 = true;
            ++index;
            if (index >= length)
            {
              this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidVarFunction, StyleValueImporter.glossary.emptyFunctionArgument, this.m_CurrentLine);
              return false;
            }
          }
          else if (!flag2)
          {
            string str = "";
            while (term2.GetType() == typeof (Whitespace) && index + 1 < length)
              term2 = term.Arguments[++index];
            if (term2.GetType() != typeof (Whitespace))
              str = term2.ToString();
            this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.InvalidVarFunction, string.Format(StyleValueImporter.glossary.unexpectedTokenInFunction, (object) str), this.m_CurrentLine);
            return false;
          }
        }
      }
      return true;
    }

    protected void VisitValue(Term term)
    {
      PrimitiveTerm term1 = term as PrimitiveTerm;
      HtmlColor htmlColor = term as HtmlColor;
      GenericFunction genericFunction = term as GenericFunction;
      TermList termList = term as TermList;
      Comma comma = term as Comma;
      Whitespace whitespace = term as Whitespace;
      if (term == Term.Inherit)
        this.m_Builder.AddValue(StyleValueKeyword.Inherit);
      else if (term1 != null)
      {
        string rawStr = term.ToString();
        switch (term1.PrimitiveType)
        {
          case UnitType.Number:
            this.m_Builder.AddValue(term1.GetFloatValue(UnitType.Pixel).Value);
            break;
          case UnitType.Percentage:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Pixel).Value, Dimension.Unit.Percent));
            break;
          case UnitType.Pixel:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Pixel).Value, Dimension.Unit.Pixel));
            break;
          case UnitType.Degree:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Pixel).Value, Dimension.Unit.Degree));
            break;
          case UnitType.Radian:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Pixel).Value, Dimension.Unit.Radian));
            break;
          case UnitType.Grad:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Pixel).Value, Dimension.Unit.Gradian));
            break;
          case UnitType.Millisecond:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Millisecond).Value, Dimension.Unit.Millisecond));
            break;
          case UnitType.Second:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Second).Value, Dimension.Unit.Second));
            break;
          case UnitType.String:
            this.m_Builder.AddValue(rawStr.Trim('\'', '"'), StyleValueType.String);
            break;
          case UnitType.Uri:
            this.VisitUrlFunction(term1);
            break;
          case UnitType.Ident:
            StyleValueKeyword keyword;
            if (StyleValueImporter.TryParseKeyword(rawStr, out keyword))
            {
              this.m_Builder.AddValue(keyword);
              break;
            }
            if (rawStr.StartsWith("--"))
            {
              this.m_Builder.AddValue(rawStr, StyleValueType.Variable);
              break;
            }
            this.m_Builder.AddValue(rawStr, StyleValueType.Enum);
            break;
          case UnitType.Turn:
            this.m_Builder.AddValue(new Dimension(term1.GetFloatValue(UnitType.Pixel).Value, Dimension.Unit.Turn));
            break;
          default:
            this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.UnsupportedUnit, string.Format(StyleValueImporter.glossary.unsupportedUnit, (object) term1.ToString()), this.m_CurrentLine);
            break;
        }
      }
      else if (htmlColor != (HtmlColor) null)
        this.m_Builder.AddValue(new Color((float) htmlColor.R / (float) byte.MaxValue, (float) htmlColor.G / (float) byte.MaxValue, (float) htmlColor.B / (float) byte.MaxValue, (float) htmlColor.A / (float) byte.MaxValue));
      else if (genericFunction != null)
      {
        if (genericFunction.Name == "resource")
        {
          this.VisitResourceFunction(genericFunction);
        }
        else
        {
          StyleValueFunction func;
          if (!this.ValidateFunction(genericFunction, out func))
            return;
          this.m_Builder.AddValue(func);
          this.m_Builder.AddValue((float) genericFunction.Arguments.Count<Term>((Func<Term, bool>) (a => !(a is Whitespace))));
          foreach (Term term2 in genericFunction.Arguments)
            this.VisitValue(term2);
        }
      }
      else if (termList != null)
      {
        int num = 0;
        foreach (Term term3 in termList)
        {
          this.VisitValue(term3);
          ++num;
          if (num < termList.Length)
          {
            switch (termList.GetSeparatorAt(num - 1))
            {
              case TermList.TermSeparator.Comma:
                this.m_Builder.AddCommaSeparator();
                goto case TermList.TermSeparator.Space;
              case TermList.TermSeparator.Space:
              case TermList.TermSeparator.Colon:
                break;
              default:
                throw new ArgumentOutOfRangeException("termSeparator");
            }
          }
        }
      }
      else if (comma != null)
      {
        this.m_Builder.AddCommaSeparator();
      }
      else
      {
        if (whitespace != null)
          return;
        this.m_Errors.AddSemanticError(StyleSheetImportErrorCode.UnsupportedTerm, string.Format(StyleValueImporter.glossary.unsupportedTerm, (object) term.GetType().Name), this.m_CurrentLine);
      }
    }

    private static bool TryParseKeyword(string rawStr, out StyleValueKeyword value)
    {
      if (StyleValueImporter.s_NameCache == null)
      {
        StyleValueImporter.s_NameCache = new Dictionary<string, StyleValueKeyword>();
        foreach (StyleValueKeyword styleValueKeyword in Enum.GetValues(typeof (StyleValueKeyword)))
          StyleValueImporter.s_NameCache[styleValueKeyword.ToString().ToLower()] = styleValueKeyword;
      }
      return StyleValueImporter.s_NameCache.TryGetValue(rawStr.ToLower(), out value);
    }

    private struct StoredAsset
    {
      public UnityEngine.Object resource;
      public ScalableImage si;
      public int index;
    }
  }
}
