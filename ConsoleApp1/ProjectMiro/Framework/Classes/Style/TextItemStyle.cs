using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectMiro.Framework
{
    public class TextItemStyle
    {
        /// <summary>
        /// Fill color of the text item.
        /// fillColor accepts any valid hex color code.
        /// </summary>
        public string fillColor { get; set; }

        /// <summary>
        /// Opacity level of the fill color.
        /// Possible values: any number between 0 and 1, where 0 sets the fill color to be completely transparent or invisible; 1 sets the fill color to be completely opaque or solid.
        /// </summary>
        public float fillOpacity { get; set; } = 1.0f;

        /// <summary>
        /// Defines the type of font for the text on the item.
        /// </summary>
        public FontEnum.FontFamily fontFamily { get; set; } = FontEnum.FontFamily.OpenSans;

        /// <summary>
        /// Defines the font size, in dp, for the text on the shape.
        /// </summary>
        public int fontSize { get; set; } = 14;

        /// <summary>
        /// Defines how the text is horizontally aligned.
        /// </summary>
        public TextAlign textAlign { get; set; } = TextAlign.left;
    }
}
