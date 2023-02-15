using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class StickyNoteItemStyle
    {
        /// <summary>
        /// Fill color of the sticky note.
        /// </summary>
        public Color.color fillColor { get; set; } = Color.color.light_yellow;

        /// <summary>
        /// Defines how the sticky note text is horizontally aligned.
        /// </summary>
        public TextAlign textAlign { get; set; } = TextAlign.center;
        
        /// <summary>
        /// Defines how the sticky note text is vertically aligned.
        /// </summary>
        public TextAlignVertical textAlignVertical { get; set; } = TextAlignVertical.top;
    }
}
