using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;


static public class RuntimeStyleSheet
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
        var BaseStyle = Resources.Load(EmptyStyleSheetResourceFilename) as StyleSheet;
        if ( BaseStyle == null )
        {
            Debug.Log($"Failed to find empty style sheet resource");
            return null;
        }
        var AsJson = JsonUtility.ToJson(BaseStyle);
        var AsFake = JsonUtility.FromJson<FakeStyleSheet>(AsJson);
        
        //  modify fake sheet here
        AsFake = ParseCss(Css);
        
        //  load fake stylesheet as a stylesheet and write over the base one
        var FakeJson = JsonUtility.ToJson(AsFake);
        var LoadedSpreadSheet = ScriptableObject.Instantiate<StyleSheet>(BaseStyle);
        //var LoadedSpreadSheet = CustomStyle; 
        JsonUtility.FromJsonOverwrite(FakeJson,LoadedSpreadSheet);
        
        return LoadedSpreadSheet;
    }
    
    //  this should be mostly based on StyleSheetImporterImpl
    static FakeStyleSheet ParseCss(string Css)
    {
        var Parser = new ExCSS.Parser();
        var stylesheet = Parser.Parse(Css);
        
        ExCSS.StyleSheet styleSheet = Parser.Parse(Css);
        VisitSheet(styleSheet);
      
        return new FakeStyleSheet();
    }
    
    static void VisitSheet(ExCSS.StyleSheet styleSheet)
    {
        foreach (ExCSS.StyleRule styleRule in (IEnumerable<ExCSS.StyleRule>) styleSheet.StyleRules)
        {
            /*
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
            */
        }
    }
}


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


[Serializable]
public struct StyleRule
{
    public StyleProperty[]  m_Properties;
    public int              line;
}


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

[Serializable]
public struct StyleSelector
{
    public SelectorPart[]   m_Parts;
    public int  m_PreviousRelationship;
}

[Serializable]
public struct StyleComplexSelector
{
    public int m_Specificity;
    public StyleSelector[] m_Selectors;
    public int ruleIndex;
}

[Serializable]
internal struct ScalableImage
{
}


[Serializable]
internal struct FakeStyleSheet
{
    public bool m_ImportedWithErrors;
    public bool m_ImportedWithWarnings;
    public StyleRule[] m_Rules;
    public StyleComplexSelector[] m_ComplexSelectors;
    public float[] floats;
    public Dimension[] dimensions;
    public UnityEngine.Color[] colors;
    public string[] strings;
    public UnityEngine.Object[] assets;
    //public FakeStyleSheet.ImportStruct[] imports;
    public List<FakeStyleSheet> m_FlattenedImportedStyleSheets;
    public ScalableImage[] scalableImages;
    public int m_ContentHash;
}
