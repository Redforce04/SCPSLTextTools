using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.Fonts;

namespace ProjectMiro.Framework
{
    public class ShapeItemStyle
    {
        /// <summary>
        /// Fill color of the item.
        /// fillColor accepts any valid hex color code.
        /// Note: This property is not supported for the following shapes:
        /// - left_brace,
        /// - right_brace.
        /// </summary>
        public string fillColor { get; set; } = "#fff9b1";

        /// <summary>
        /// Opacity level of the fill color.
        /// Possible values: any number between 0 and 1, where 0 sets the fill color to be completely transparent or invisible; 1 sets the fill color to be completely opaque or solid.
        /// </summary>
        public float fillOpacity { get; set; } = 1.0f;

        /// <summary>
        /// Defines the type of font for the text on the shape.
        /// </summary>
        public FontEnum.FontFamily fontFamily { get; set; }

        /// <summary>
        /// Defines the font size, in dp, for the text on the shape.
        /// </summary>
        public int fontSize { get; set; } = 14;

        /// <summary>
        /// Defines the color of the border of the shape.
        /// </summary>
        public string borderColor { get; set; } = "#1a1a1a";

        /// <summary>
        /// Defines the thickness of the shape border, in dp.
        /// </summary>
        public int borderWidth { get; set; }

        /// <summary>
        /// Defines the opacity level of the shape border.
        /// Possible values: any number between 0 and 1, where 0 sets the border color to be completely transparent or invisible; 1 sets the border color to be completely opaque or solid.
        /// </summary>
        public float borderOpacity { get; set; } = 1.0f;

        /// <summary>
        /// Defines the style used to represent the border of the shape.
        /// </summary>
        public BorderStyle borderStyle { get; set; } = BorderStyle.normal;

        /// <summary>
        /// Defines how the shape text is horizontally aligned.
        /// </summary>
        public TextAlign textAlign { get; set; } = TextAlign.center;
    }
}
