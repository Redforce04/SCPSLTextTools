using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class Tag
    {
        /// <summary>
        /// Unique identifier of the tag.
        /// </summary>
        public string id { get; private set; }

        /// <summary>
        /// Background color of the tag.
        /// </summary>
        public Color.color color { get; private set; } = Color.color.red;

        /// <summary>
        /// Text of the tag: case-sensitive.
        /// Must be unique.
        /// Max.length: 120 characters, spaces included.
        /// </summary>
        public string title { get; private set; }
    }

    

}
