using ExCSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


static public class RuntimeStyleSheetLib
{
    //const string EmptyStyleSheetResourceFilename = "Packages/com.condense.runtimestylesheet/Resources/EmptyStyleSheet";
    const string EmptyStyleSheetResourceFilename = "EmptyStyleSheet";
    
    static public UnityEngine.UIElements.StyleSheet LoadStyleSheet(string Css)
    {
        //  https://github.com/Unity-Technologies/UnityCsReference/blob/master/Modules/StyleSheetsEditor/Converters/StyleSheetToUss.cs
        
        //  we cannot just create a new UIElements StyleSheet from scratch
        //  (for some reason it breaks some layout with no error)
        //  and we cannot make an incomplete one (null errors deep in uitoolkit)
        //  so always load a base one
        //  todo: cache this!
        //  todo: find out which nulls break uitoolkit and dont let the code proceed
        //          (although fields are private... so may have to check json :/)
        
        var BaseStyleReal = Resources.Load(EmptyStyleSheetResourceFilename) as UnityEngine.UIElements.StyleSheet;
        var BaseStyle = Resources.Load(EmptyStyleSheetResourceFilename) as RuntimeStyleSheet.UIElements.StyleSheet;
        if ( BaseStyleReal == null )
        {
            Debug.Log($"Failed to find empty style sheet resource");
            return null;
        }
        
        //var AsJson = JsonUtility.ToJson(BaseStyle);
        //var AsFake = JsonUtility.FromJson<FakeStyleSheet>(AsJson);

        //  empty vars here break the real one later....
        var FakeStyleSheet = ScriptableObject.CreateInstance<RuntimeStyleSheet.UIElements.StyleSheet>();
        //  ...so fill with base values
        {
            var RealJson = JsonUtility.ToJson(BaseStyleReal);
            JsonUtility.FromJsonOverwrite(RealJson,FakeStyleSheet);
        }

        //  modify the fake style sheet
        var Importer = new StyleSheetImporterImpl();
        Importer.Import(FakeStyleSheet,Css);
        Importer.importErrors.ThrowErrors();

        //  now write imported data over the real one        
        var RealStyleSheet = ScriptableObject.Instantiate<UnityEngine.UIElements.StyleSheet>(BaseStyleReal);
        var FakeJson = JsonUtility.ToJson(FakeStyleSheet);
        JsonUtility.FromJsonOverwrite(FakeJson,RealStyleSheet);

        return RealStyleSheet;
    }
    /*
    //  this should be mostly based on StyleSheetImporterImpl
    static StyleSheet ParseCss(string Css)
    {
        var Parser = new ExCSS.Parser();
        var stylesheet = Parser.Parse(Css);
        
        ExCSS.StyleSheet styleSheet = Parser.Parse(Css);
        VisitSheet(styleSheet);
      
        return new StyleSheet();
    }
    
    static void VisitSheet(ExCSS.StyleSheet styleSheet)
    {
        foreach (ExCSS.StyleRule styleRule in (IEnumerable<ExCSS.StyleRule>) styleSheet.StyleRules)
        {
            //m_Builder.BeginRule(styleRule.Line);
            //m_CurrentLine = styleRule.Line;
            VisitBaseSelector(styleRule.Selector);
            foreach (Property declaration in styleRule.Declarations)
            {
                //m_CurrentLine = declaration.Line;
                ValidateProperty(declaration);
                //m_Builder.BeginProperty(declaration.Name, declaration.Line);
                VisitValue(declaration.);
                //m_Builder.EndProperty();
            }
            m_Builder.EndRule();
        }
    }
    */
}

/*
//  unity internal classes
[Serializable]
public struct StylePropertyValue
{
    [Serializable]
    public enum ValueType
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Colour = 4
    };
    public ValueType      m_ValueType;    //
    public int            valueIndex;
}

[Serializable]
public struct StyleProperty
{
    public string   m_Name;
    public int      m_Line;
    public StylePropertyValue[] m_Values;
    
}
*/
/*
[Serializable]
public struct StyleRule
{
    public StyleProperty[]  m_Properties;
    public int              line;
}
*/
/*

[Serializable]
public struct Dimension
{
    [Serializable]
    public enum DimenionUnit
    {
        Zero = 0,
        Px = 1,
        Percent = 2,
    }
    public DimenionUnit  unit;
    public float        value;
}

[Serializable]
public struct StyleColour
{
    public float r,g,b,a;
}

[Serializable]
public struct SelectorPart
{
    [Serializable]
    public enum SelectorType
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,   //  for :root where value is "root"
    }
    public string       m_Value;
    public SelectorType m_Type;
}
*/
/*
[Serializable]
public struct StyleSelector
{
    public SelectorPart[]   m_Parts;
    public int  m_PreviousRelationship;
}
*/
/*
[Serializable]
public struct StyleComplexSelector
{
    public int m_Specificity;
    public StyleSelector[] m_Selectors;
    public int ruleIndex;
}
*/


/*
[Serializable]
internal class FakeStyleSheetXX
{
    static public FakeStyleSheetXX Instantiate(StyleSheet Original)
    {
        throw new NotImplementedException();
    } 

    public bool m_IsDefaultStyleSheet;
    public bool m_ImportedWithErrors;
    public bool m_ImportedWithWarnings;
    public StyleRule[] m_Rules;
    public StyleComplexSelector[] m_ComplexSelectors;
    public float[] floats;
    public Dimension[] dimensions;
    public UnityEngine.Color[] colors;
    public string[] strings;
    public UnityEngine.Object[] assets;
    public FakeStyleSheet.ImportStruct[] imports;
    public List<FakeStyleSheet> m_FlattenedImportedStyleSheets;
    public ScalableImage[] scalableImages;
    public int m_ContentHash;
    
    
    [Serializable]
    public struct ImportStruct
    {
      public FakeStyleSheet styleSheet;
      public string[] mediaQueries;
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
        foreach (var flattenedRecursiveImport in this.flattenedRecursiveImports)
          flattenedRecursiveImport.isDefaultStyleSheet = value;
      }
    }
    
    void SetupReferences()
    {
        throw new NotImplementedException();
    }
    
    public void FlattenImportedStyleSheetsRecursive()
    {
        throw new NotImplementedException();
    }
}
*/