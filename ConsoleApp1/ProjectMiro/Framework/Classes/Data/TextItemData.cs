using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dissonance;

namespace ProjectMiro.Framework
{
    public class TextItemData
    {
        /// <summary>
        /// The text you want to display on the text item.
        /// Besides plain text, content supports also the following HTML tags:
        /// - p,
        /// - a,
        /// - strong,
        /// - b,
        /// - em,
        /// - i,
        /// - u,
        /// - s,
        /// - span,
        /// - ol,
        /// - ul,
        /// - li,
        /// - br.
        /// Unsupported HTML tags are automatically stripped.
        /// </summary>
        public string content { get; set; }
    }

}
