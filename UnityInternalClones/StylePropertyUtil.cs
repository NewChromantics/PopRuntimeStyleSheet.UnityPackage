// Decompiled with JetBrains decompiler
// Type: UnityEngine.UIElements.StyleSheets.StylePropertyUtil
// Assembly: UnityEngine.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 254D9B30-C554-4364-9CE1-F4826DF541B4
// Assembly location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.dll
// XML documentation location: /Applications/2022.3.14f1/Unity.app/Contents/Managed/UnityEngine/UnityEngine.UIElementsModule.xml

using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements.StyleSheets
{
  internal static class StylePropertyUtil
  {
    public const int k_GroupOffset = 16;
    internal static readonly Dictionary<string, StylePropertyId> s_NameToId = new Dictionary<string, StylePropertyId>()
    {
      {
        "align-content",
        StylePropertyId.AlignContent
      },
      {
        "align-items",
        StylePropertyId.AlignItems
      },
      {
        "align-self",
        StylePropertyId.AlignSelf
      },
      {
        "all",
        StylePropertyId.All
      },
      {
        "background-color",
        StylePropertyId.BackgroundColor
      },
      {
        "background-image",
        StylePropertyId.BackgroundImage
      },
      {
        "background-position",
        StylePropertyId.BackgroundPosition
      },
      {
        "background-position-x",
        StylePropertyId.BackgroundPositionX
      },
      {
        "background-position-y",
        StylePropertyId.BackgroundPositionY
      },
      {
        "background-repeat",
        StylePropertyId.BackgroundRepeat
      },
      {
        "background-size",
        StylePropertyId.BackgroundSize
      },
      {
        "border-bottom-color",
        StylePropertyId.BorderBottomColor
      },
      {
        "border-bottom-left-radius",
        StylePropertyId.BorderBottomLeftRadius
      },
      {
        "border-bottom-right-radius",
        StylePropertyId.BorderBottomRightRadius
      },
      {
        "border-bottom-width",
        StylePropertyId.BorderBottomWidth
      },
      {
        "border-color",
        StylePropertyId.BorderColor
      },
      {
        "border-left-color",
        StylePropertyId.BorderLeftColor
      },
      {
        "border-left-width",
        StylePropertyId.BorderLeftWidth
      },
      {
        "border-radius",
        StylePropertyId.BorderRadius
      },
      {
        "border-right-color",
        StylePropertyId.BorderRightColor
      },
      {
        "border-right-width",
        StylePropertyId.BorderRightWidth
      },
      {
        "border-top-color",
        StylePropertyId.BorderTopColor
      },
      {
        "border-top-left-radius",
        StylePropertyId.BorderTopLeftRadius
      },
      {
        "border-top-right-radius",
        StylePropertyId.BorderTopRightRadius
      },
      {
        "border-top-width",
        StylePropertyId.BorderTopWidth
      },
      {
        "border-width",
        StylePropertyId.BorderWidth
      },
      {
        "bottom",
        StylePropertyId.Bottom
      },
      {
        "color",
        StylePropertyId.Color
      },
      {
        "cursor",
        StylePropertyId.Cursor
      },
      {
        "display",
        StylePropertyId.Display
      },
      {
        "flex",
        StylePropertyId.Flex
      },
      {
        "flex-basis",
        StylePropertyId.FlexBasis
      },
      {
        "flex-direction",
        StylePropertyId.FlexDirection
      },
      {
        "flex-grow",
        StylePropertyId.FlexGrow
      },
      {
        "flex-shrink",
        StylePropertyId.FlexShrink
      },
      {
        "flex-wrap",
        StylePropertyId.FlexWrap
      },
      {
        "font-size",
        StylePropertyId.FontSize
      },
      {
        "height",
        StylePropertyId.Height
      },
      {
        "justify-content",
        StylePropertyId.JustifyContent
      },
      {
        "left",
        StylePropertyId.Left
      },
      {
        "letter-spacing",
        StylePropertyId.LetterSpacing
      },
      {
        "margin",
        StylePropertyId.Margin
      },
      {
        "margin-bottom",
        StylePropertyId.MarginBottom
      },
      {
        "margin-left",
        StylePropertyId.MarginLeft
      },
      {
        "margin-right",
        StylePropertyId.MarginRight
      },
      {
        "margin-top",
        StylePropertyId.MarginTop
      },
      {
        "max-height",
        StylePropertyId.MaxHeight
      },
      {
        "max-width",
        StylePropertyId.MaxWidth
      },
      {
        "min-height",
        StylePropertyId.MinHeight
      },
      {
        "min-width",
        StylePropertyId.MinWidth
      },
      {
        "opacity",
        StylePropertyId.Opacity
      },
      {
        "overflow",
        StylePropertyId.Overflow
      },
      {
        "padding",
        StylePropertyId.Padding
      },
      {
        "padding-bottom",
        StylePropertyId.PaddingBottom
      },
      {
        "padding-left",
        StylePropertyId.PaddingLeft
      },
      {
        "padding-right",
        StylePropertyId.PaddingRight
      },
      {
        "padding-top",
        StylePropertyId.PaddingTop
      },
      {
        "position",
        StylePropertyId.Position
      },
      {
        "right",
        StylePropertyId.Right
      },
      {
        "rotate",
        StylePropertyId.Rotate
      },
      {
        "scale",
        StylePropertyId.Scale
      },
      {
        "text-overflow",
        StylePropertyId.TextOverflow
      },
      {
        "text-shadow",
        StylePropertyId.TextShadow
      },
      {
        "top",
        StylePropertyId.Top
      },
      {
        "transform-origin",
        StylePropertyId.TransformOrigin
      },
      {
        "transition",
        StylePropertyId.Transition
      },
      {
        "transition-delay",
        StylePropertyId.TransitionDelay
      },
      {
        "transition-duration",
        StylePropertyId.TransitionDuration
      },
      {
        "transition-property",
        StylePropertyId.TransitionProperty
      },
      {
        "transition-timing-function",
        StylePropertyId.TransitionTimingFunction
      },
      {
        "translate",
        StylePropertyId.Translate
      },
      {
        "-unity-background-image-tint-color",
        StylePropertyId.UnityBackgroundImageTintColor
      },
      {
        "-unity-background-scale-mode",
        StylePropertyId.UnityBackgroundScaleMode
      },
      {
        "-unity-font",
        StylePropertyId.UnityFont
      },
      {
        "-unity-font-definition",
        StylePropertyId.UnityFontDefinition
      },
      {
        "-unity-font-style",
        StylePropertyId.UnityFontStyleAndWeight
      },
      {
        "-unity-overflow-clip-box",
        StylePropertyId.UnityOverflowClipBox
      },
      {
        "-unity-paragraph-spacing",
        StylePropertyId.UnityParagraphSpacing
      },
      {
        "-unity-slice-bottom",
        StylePropertyId.UnitySliceBottom
      },
      {
        "-unity-slice-left",
        StylePropertyId.UnitySliceLeft
      },
      {
        "-unity-slice-right",
        StylePropertyId.UnitySliceRight
      },
      {
        "-unity-slice-scale",
        StylePropertyId.UnitySliceScale
      },
      {
        "-unity-slice-top",
        StylePropertyId.UnitySliceTop
      },
      {
        "-unity-text-align",
        StylePropertyId.UnityTextAlign
      },
      {
        "-unity-text-outline",
        StylePropertyId.UnityTextOutline
      },
      {
        "-unity-text-outline-color",
        StylePropertyId.UnityTextOutlineColor
      },
      {
        "-unity-text-outline-width",
        StylePropertyId.UnityTextOutlineWidth
      },
      {
        "-unity-text-overflow-position",
        StylePropertyId.UnityTextOverflowPosition
      },
      {
        "visibility",
        StylePropertyId.Visibility
      },
      {
        "white-space",
        StylePropertyId.WhiteSpace
      },
      {
        "width",
        StylePropertyId.Width
      },
      {
        "word-spacing",
        StylePropertyId.WordSpacing
      }
    };
    internal static readonly Dictionary<StylePropertyId, string> s_IdToName = new Dictionary<StylePropertyId, string>()
    {
      {
        StylePropertyId.AlignContent,
        "align-content"
      },
      {
        StylePropertyId.AlignItems,
        "align-items"
      },
      {
        StylePropertyId.AlignSelf,
        "align-self"
      },
      {
        StylePropertyId.All,
        "all"
      },
      {
        StylePropertyId.BackgroundColor,
        "background-color"
      },
      {
        StylePropertyId.BackgroundImage,
        "background-image"
      },
      {
        StylePropertyId.BackgroundPosition,
        "background-position"
      },
      {
        StylePropertyId.BackgroundPositionX,
        "background-position-x"
      },
      {
        StylePropertyId.BackgroundPositionY,
        "background-position-y"
      },
      {
        StylePropertyId.BackgroundRepeat,
        "background-repeat"
      },
      {
        StylePropertyId.BackgroundSize,
        "background-size"
      },
      {
        StylePropertyId.BorderBottomColor,
        "border-bottom-color"
      },
      {
        StylePropertyId.BorderBottomLeftRadius,
        "border-bottom-left-radius"
      },
      {
        StylePropertyId.BorderBottomRightRadius,
        "border-bottom-right-radius"
      },
      {
        StylePropertyId.BorderBottomWidth,
        "border-bottom-width"
      },
      {
        StylePropertyId.BorderColor,
        "border-color"
      },
      {
        StylePropertyId.BorderLeftColor,
        "border-left-color"
      },
      {
        StylePropertyId.BorderLeftWidth,
        "border-left-width"
      },
      {
        StylePropertyId.BorderRadius,
        "border-radius"
      },
      {
        StylePropertyId.BorderRightColor,
        "border-right-color"
      },
      {
        StylePropertyId.BorderRightWidth,
        "border-right-width"
      },
      {
        StylePropertyId.BorderTopColor,
        "border-top-color"
      },
      {
        StylePropertyId.BorderTopLeftRadius,
        "border-top-left-radius"
      },
      {
        StylePropertyId.BorderTopRightRadius,
        "border-top-right-radius"
      },
      {
        StylePropertyId.BorderTopWidth,
        "border-top-width"
      },
      {
        StylePropertyId.BorderWidth,
        "border-width"
      },
      {
        StylePropertyId.Bottom,
        "bottom"
      },
      {
        StylePropertyId.Color,
        "color"
      },
      {
        StylePropertyId.Cursor,
        "cursor"
      },
      {
        StylePropertyId.Display,
        "display"
      },
      {
        StylePropertyId.Flex,
        "flex"
      },
      {
        StylePropertyId.FlexBasis,
        "flex-basis"
      },
      {
        StylePropertyId.FlexDirection,
        "flex-direction"
      },
      {
        StylePropertyId.FlexGrow,
        "flex-grow"
      },
      {
        StylePropertyId.FlexShrink,
        "flex-shrink"
      },
      {
        StylePropertyId.FlexWrap,
        "flex-wrap"
      },
      {
        StylePropertyId.FontSize,
        "font-size"
      },
      {
        StylePropertyId.Height,
        "height"
      },
      {
        StylePropertyId.JustifyContent,
        "justify-content"
      },
      {
        StylePropertyId.Left,
        "left"
      },
      {
        StylePropertyId.LetterSpacing,
        "letter-spacing"
      },
      {
        StylePropertyId.Margin,
        "margin"
      },
      {
        StylePropertyId.MarginBottom,
        "margin-bottom"
      },
      {
        StylePropertyId.MarginLeft,
        "margin-left"
      },
      {
        StylePropertyId.MarginRight,
        "margin-right"
      },
      {
        StylePropertyId.MarginTop,
        "margin-top"
      },
      {
        StylePropertyId.MaxHeight,
        "max-height"
      },
      {
        StylePropertyId.MaxWidth,
        "max-width"
      },
      {
        StylePropertyId.MinHeight,
        "min-height"
      },
      {
        StylePropertyId.MinWidth,
        "min-width"
      },
      {
        StylePropertyId.Opacity,
        "opacity"
      },
      {
        StylePropertyId.Overflow,
        "overflow"
      },
      {
        StylePropertyId.Padding,
        "padding"
      },
      {
        StylePropertyId.PaddingBottom,
        "padding-bottom"
      },
      {
        StylePropertyId.PaddingLeft,
        "padding-left"
      },
      {
        StylePropertyId.PaddingRight,
        "padding-right"
      },
      {
        StylePropertyId.PaddingTop,
        "padding-top"
      },
      {
        StylePropertyId.Position,
        "position"
      },
      {
        StylePropertyId.Right,
        "right"
      },
      {
        StylePropertyId.Rotate,
        "rotate"
      },
      {
        StylePropertyId.Scale,
        "scale"
      },
      {
        StylePropertyId.TextOverflow,
        "text-overflow"
      },
      {
        StylePropertyId.TextShadow,
        "text-shadow"
      },
      {
        StylePropertyId.Top,
        "top"
      },
      {
        StylePropertyId.TransformOrigin,
        "transform-origin"
      },
      {
        StylePropertyId.Transition,
        "transition"
      },
      {
        StylePropertyId.TransitionDelay,
        "transition-delay"
      },
      {
        StylePropertyId.TransitionDuration,
        "transition-duration"
      },
      {
        StylePropertyId.TransitionProperty,
        "transition-property"
      },
      {
        StylePropertyId.TransitionTimingFunction,
        "transition-timing-function"
      },
      {
        StylePropertyId.Translate,
        "translate"
      },
      {
        StylePropertyId.UnityBackgroundImageTintColor,
        "-unity-background-image-tint-color"
      },
      {
        StylePropertyId.UnityBackgroundScaleMode,
        "-unity-background-scale-mode"
      },
      {
        StylePropertyId.UnityFont,
        "-unity-font"
      },
      {
        StylePropertyId.UnityFontDefinition,
        "-unity-font-definition"
      },
      {
        StylePropertyId.UnityFontStyleAndWeight,
        "-unity-font-style"
      },
      {
        StylePropertyId.UnityOverflowClipBox,
        "-unity-overflow-clip-box"
      },
      {
        StylePropertyId.UnityParagraphSpacing,
        "-unity-paragraph-spacing"
      },
      {
        StylePropertyId.UnitySliceBottom,
        "-unity-slice-bottom"
      },
      {
        StylePropertyId.UnitySliceLeft,
        "-unity-slice-left"
      },
      {
        StylePropertyId.UnitySliceRight,
        "-unity-slice-right"
      },
      {
        StylePropertyId.UnitySliceScale,
        "-unity-slice-scale"
      },
      {
        StylePropertyId.UnitySliceTop,
        "-unity-slice-top"
      },
      {
        StylePropertyId.UnityTextAlign,
        "-unity-text-align"
      },
      {
        StylePropertyId.UnityTextOutline,
        "-unity-text-outline"
      },
      {
        StylePropertyId.UnityTextOutlineColor,
        "-unity-text-outline-color"
      },
      {
        StylePropertyId.UnityTextOutlineWidth,
        "-unity-text-outline-width"
      },
      {
        StylePropertyId.UnityTextOverflowPosition,
        "-unity-text-overflow-position"
      },
      {
        StylePropertyId.Visibility,
        "visibility"
      },
      {
        StylePropertyId.WhiteSpace,
        "white-space"
      },
      {
        StylePropertyId.Width,
        "width"
      },
      {
        StylePropertyId.WordSpacing,
        "word-spacing"
      }
    };
    internal static readonly HashSet<StylePropertyId> s_AnimatableProperties = new HashSet<StylePropertyId>()
    {
      StylePropertyId.AlignContent,
      StylePropertyId.AlignItems,
      StylePropertyId.AlignSelf,
      StylePropertyId.All,
      StylePropertyId.BackgroundColor,
      StylePropertyId.BackgroundImage,
      StylePropertyId.BackgroundPosition,
      StylePropertyId.BackgroundPositionX,
      StylePropertyId.BackgroundPositionY,
      StylePropertyId.BackgroundRepeat,
      StylePropertyId.BackgroundSize,
      StylePropertyId.BorderBottomColor,
      StylePropertyId.BorderBottomLeftRadius,
      StylePropertyId.BorderBottomRightRadius,
      StylePropertyId.BorderBottomWidth,
      StylePropertyId.BorderColor,
      StylePropertyId.BorderLeftColor,
      StylePropertyId.BorderLeftWidth,
      StylePropertyId.BorderRadius,
      StylePropertyId.BorderRightColor,
      StylePropertyId.BorderRightWidth,
      StylePropertyId.BorderTopColor,
      StylePropertyId.BorderTopLeftRadius,
      StylePropertyId.BorderTopRightRadius,
      StylePropertyId.BorderTopWidth,
      StylePropertyId.BorderWidth,
      StylePropertyId.Bottom,
      StylePropertyId.Color,
      StylePropertyId.Display,
      StylePropertyId.Flex,
      StylePropertyId.FlexBasis,
      StylePropertyId.FlexDirection,
      StylePropertyId.FlexGrow,
      StylePropertyId.FlexShrink,
      StylePropertyId.FlexWrap,
      StylePropertyId.FontSize,
      StylePropertyId.Height,
      StylePropertyId.JustifyContent,
      StylePropertyId.Left,
      StylePropertyId.LetterSpacing,
      StylePropertyId.Margin,
      StylePropertyId.MarginBottom,
      StylePropertyId.MarginLeft,
      StylePropertyId.MarginRight,
      StylePropertyId.MarginTop,
      StylePropertyId.MaxHeight,
      StylePropertyId.MaxWidth,
      StylePropertyId.MinHeight,
      StylePropertyId.MinWidth,
      StylePropertyId.Opacity,
      StylePropertyId.Overflow,
      StylePropertyId.Padding,
      StylePropertyId.PaddingBottom,
      StylePropertyId.PaddingLeft,
      StylePropertyId.PaddingRight,
      StylePropertyId.PaddingTop,
      StylePropertyId.Position,
      StylePropertyId.Right,
      StylePropertyId.Rotate,
      StylePropertyId.Scale,
      StylePropertyId.TextOverflow,
      StylePropertyId.TextShadow,
      StylePropertyId.Top,
      StylePropertyId.TransformOrigin,
      StylePropertyId.Translate,
      StylePropertyId.UnityBackgroundImageTintColor,
      StylePropertyId.UnityBackgroundScaleMode,
      StylePropertyId.UnityFont,
      StylePropertyId.UnityFontDefinition,
      StylePropertyId.UnityFontStyleAndWeight,
      StylePropertyId.UnityOverflowClipBox,
      StylePropertyId.UnityParagraphSpacing,
      StylePropertyId.UnitySliceBottom,
      StylePropertyId.UnitySliceLeft,
      StylePropertyId.UnitySliceRight,
      StylePropertyId.UnitySliceScale,
      StylePropertyId.UnitySliceTop,
      StylePropertyId.UnityTextAlign,
      StylePropertyId.UnityTextOutline,
      StylePropertyId.UnityTextOutlineColor,
      StylePropertyId.UnityTextOutlineWidth,
      StylePropertyId.UnityTextOverflowPosition,
      StylePropertyId.Visibility,
      StylePropertyId.WhiteSpace,
      StylePropertyId.Width,
      StylePropertyId.WordSpacing
    };
    internal static readonly Dictionary<StylePropertyId, UsageHints> s_AnimatableWithUsageHintProperties = new Dictionary<StylePropertyId, UsageHints>()
    {
      {
        StylePropertyId.BackgroundColor,
        UsageHints.DynamicColor
      },
      {
        StylePropertyId.BorderBottomColor,
        UsageHints.DynamicColor
      },
      {
        StylePropertyId.BorderColor,
        UsageHints.DynamicColor
      },
      {
        StylePropertyId.BorderLeftColor,
        UsageHints.DynamicColor
      },
      {
        StylePropertyId.BorderRightColor,
        UsageHints.DynamicColor
      },
      {
        StylePropertyId.BorderTopColor,
        UsageHints.DynamicColor
      },
      {
        StylePropertyId.Color,
        UsageHints.DynamicColor
      },
      {
        StylePropertyId.Rotate,
        UsageHints.DynamicTransform
      },
      {
        StylePropertyId.Scale,
        UsageHints.DynamicTransform
      },
      {
        StylePropertyId.TransformOrigin,
        UsageHints.DynamicTransform
      },
      {
        StylePropertyId.Translate,
        UsageHints.DynamicTransform
      },
      {
        StylePropertyId.UnityBackgroundImageTintColor,
        UsageHints.DynamicColor
      }
    };

    public static bool TryGetEnumIntValue(StyleEnumType enumType, string value, out int intValue)
    {
      intValue = 0;
      switch (enumType)
      {
        case StyleEnumType.Align:
          if (string.Equals(value, "auto", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "flex-start", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "center", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "flex-end", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          if (string.Equals(value, "stretch", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 4;
            return true;
          }
          break;
        case StyleEnumType.BackgroundPositionKeyword:
          if (string.Equals(value, "center", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "top", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "bottom", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "left", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          if (string.Equals(value, "right", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 4;
            return true;
          }
          break;
        case StyleEnumType.BackgroundSizeType:
          if (string.Equals(value, "cover", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "contain", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          break;
        case StyleEnumType.DisplayStyle:
          if (string.Equals(value, "flex", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "none", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.EasingMode:
          if (string.Equals(value, "ease", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "ease-in", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "ease-out", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "ease-in-out", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          if (string.Equals(value, "linear", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 4;
            return true;
          }
          if (string.Equals(value, "ease-in-sine", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 5;
            return true;
          }
          if (string.Equals(value, "ease-out-sine", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 6;
            return true;
          }
          if (string.Equals(value, "ease-in-out-sine", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 7;
            return true;
          }
          if (string.Equals(value, "ease-in-cubic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 8;
            return true;
          }
          if (string.Equals(value, "ease-out-cubic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 9;
            return true;
          }
          if (string.Equals(value, "ease-in-out-cubic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 10;
            return true;
          }
          if (string.Equals(value, "ease-in-circ", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 11;
            return true;
          }
          if (string.Equals(value, "ease-out-circ", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 12;
            return true;
          }
          if (string.Equals(value, "ease-in-out-circ", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 13;
            return true;
          }
          if (string.Equals(value, "ease-in-elastic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 14;
            return true;
          }
          if (string.Equals(value, "ease-out-elastic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 15;
            return true;
          }
          if (string.Equals(value, "ease-in-out-elastic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 16;
            return true;
          }
          if (string.Equals(value, "ease-in-back", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 17;
            return true;
          }
          if (string.Equals(value, "ease-out-back", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 18;
            return true;
          }
          if (string.Equals(value, "ease-in-out-back", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 19;
            return true;
          }
          if (string.Equals(value, "ease-in-bounce", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 20;
            return true;
          }
          if (string.Equals(value, "ease-out-bounce", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 21;
            return true;
          }
          if (string.Equals(value, "ease-in-out-bounce", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 22;
            return true;
          }
          break;
        case StyleEnumType.FlexDirection:
          if (string.Equals(value, "column", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "column-reverse", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "row", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "row-reverse", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          break;
        case StyleEnumType.FontStyle:
          if (string.Equals(value, "normal", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "bold", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "italic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "bold-and-italic", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          break;
        case StyleEnumType.Justify:
          if (string.Equals(value, "flex-start", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "center", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "flex-end", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "space-between", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          if (string.Equals(value, "space-around", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 4;
            return true;
          }
          break;
        case StyleEnumType.Overflow:
          if (string.Equals(value, "visible", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "hidden", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.OverflowClipBox:
          if (string.Equals(value, "padding-box", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "content-box", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.OverflowInternal:
          if (string.Equals(value, "visible", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "hidden", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "scroll", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          break;
        case StyleEnumType.Position:
          if (string.Equals(value, "relative", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "absolute", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.Repeat:
          if (string.Equals(value, "no-repeat", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "repeat", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          if (string.Equals(value, "space", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "round", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          break;
        case StyleEnumType.RepeatXY:
          if (string.Equals(value, "repeat-x", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "repeat-y", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.ScaleMode:
          if (string.Equals(value, "stretch-to-fill", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "scale-and-crop", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "scale-to-fit", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          break;
        case StyleEnumType.TextAnchor:
          if (string.Equals(value, "upper-left", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "upper-center", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "upper-right", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "middle-left", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          if (string.Equals(value, "middle-center", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 4;
            return true;
          }
          if (string.Equals(value, "middle-right", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 5;
            return true;
          }
          if (string.Equals(value, "lower-left", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 6;
            return true;
          }
          if (string.Equals(value, "lower-center", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 7;
            return true;
          }
          if (string.Equals(value, "lower-right", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 8;
            return true;
          }
          break;
        case StyleEnumType.TextOverflow:
          if (string.Equals(value, "clip", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "ellipsis", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.TextOverflowPosition:
          if (string.Equals(value, "start", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "middle", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "end", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          break;
        case StyleEnumType.TransformOriginOffset:
          if (string.Equals(value, "left", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "right", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          if (string.Equals(value, "top", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 3;
            return true;
          }
          if (string.Equals(value, "bottom", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 4;
            return true;
          }
          if (string.Equals(value, "center", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 5;
            return true;
          }
          break;
        case StyleEnumType.Visibility:
          if (string.Equals(value, "visible", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "hidden", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.WhiteSpace:
          if (string.Equals(value, "normal", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "nowrap", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          break;
        case StyleEnumType.Wrap:
          if (string.Equals(value, "nowrap", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 0;
            return true;
          }
          if (string.Equals(value, "wrap", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 1;
            return true;
          }
          if (string.Equals(value, "wrap-reverse", StringComparison.OrdinalIgnoreCase))
          {
            intValue = 2;
            return true;
          }
          break;
      }
      return false;
    }

    public static bool IsMatchingShorthand(StylePropertyId shorthand, StylePropertyId id)
    {
      switch (shorthand)
      {
        case StylePropertyId.All:
          return true;
        case StylePropertyId.BackgroundPosition:
          return id == StylePropertyId.BackgroundPositionX || id == StylePropertyId.BackgroundPositionY;
        case StylePropertyId.BorderColor:
          return id == StylePropertyId.BorderTopColor || id == StylePropertyId.BorderRightColor || id == StylePropertyId.BorderBottomColor || id == StylePropertyId.BorderLeftColor;
        case StylePropertyId.BorderRadius:
          return id == StylePropertyId.BorderTopLeftRadius || id == StylePropertyId.BorderTopRightRadius || id == StylePropertyId.BorderBottomRightRadius || id == StylePropertyId.BorderBottomLeftRadius;
        case StylePropertyId.BorderWidth:
          return id == StylePropertyId.BorderTopWidth || id == StylePropertyId.BorderRightWidth || id == StylePropertyId.BorderBottomWidth || id == StylePropertyId.BorderLeftWidth;
        case StylePropertyId.Flex:
          return id == StylePropertyId.FlexGrow || id == StylePropertyId.FlexShrink || id == StylePropertyId.FlexBasis;
        case StylePropertyId.Margin:
          return id == StylePropertyId.MarginTop || id == StylePropertyId.MarginRight || id == StylePropertyId.MarginBottom || id == StylePropertyId.MarginLeft;
        case StylePropertyId.Padding:
          return id == StylePropertyId.PaddingTop || id == StylePropertyId.PaddingRight || id == StylePropertyId.PaddingBottom || id == StylePropertyId.PaddingLeft;
        case StylePropertyId.UnityBackgroundScaleMode:
          return id == StylePropertyId.BackgroundPositionX || id == StylePropertyId.BackgroundPositionY || id == StylePropertyId.BackgroundRepeat || id == StylePropertyId.BackgroundSize;
        case StylePropertyId.UnityTextOutline:
          return id == StylePropertyId.UnityTextOutlineColor || id == StylePropertyId.UnityTextOutlineWidth;
        default:
          return false;
      }
    }

    public static IEnumerable<System.Type> GetAllowedAssetTypesForProperty(
      StylePropertyId id)
    {
      switch (id)
      {
        case StylePropertyId.Custom:
          return (IEnumerable<System.Type>) new System.Type[1]
          {
            typeof (UnityEngine.Object)
          };
        case StylePropertyId.UnityFont:
          return (IEnumerable<System.Type>) new System.Type[1]
          {
            typeof (Font)
          };
        case StylePropertyId.UnityFontDefinition:
          return FontDefinition.allowedAssetTypes;
        case StylePropertyId.Cursor:
          return UnityEngine.UIElements.Cursor.allowedAssetTypes;
        case StylePropertyId.BackgroundImage:
          return Background.allowedAssetTypes;
        default:
          return Enumerable.Empty<System.Type>();
      }
    }

    public static bool IsAnimatable(StylePropertyId id) => StylePropertyUtil.s_AnimatableProperties.Contains(id);

    public static IEnumerable<StylePropertyId> AllPropertyIds() => (IEnumerable<StylePropertyId>) StylePropertyUtil.s_IdToName.Keys;
  }
}
