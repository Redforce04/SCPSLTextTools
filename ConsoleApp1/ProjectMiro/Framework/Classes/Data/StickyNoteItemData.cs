using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class StickyNoteItemData
    {
        /// <summary>
        /// The text you want to display on the shape.
        /// Besides plain text, content also supports the following HTML tags:
        /// - p,
        /// - a,
        /// - strong,
        /// - b,
        /// - em,
        /// - i,
        /// - u,
        /// - s,
        /// - span,
        /// - br.
        /// Unsupported HTML tags are automatically stripped.
        /// </summary>
        public string content { get; set; }

    }
}
