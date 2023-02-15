using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public Color.color color
        {
            get { return _color; }
            set
            {
                if (!allowedColors.Contains(value))
                    throw new InvalidEnumArgumentException($"Color {value} is not allowed for this field.");
                    _color = value;
            }
        }

        private List<Color.color> allowedColors = new List<Color.color>()
        {
            Color.color.red,
            Color.color.magenta,
            Color.color.violet,
            Color.color.light_green,
            Color.color.green,
            Color.color.dark_green,
            Color.color.cyan,
            Color.color.blue,
            Color.color.dark_blue,
            Color.color.yellow,
            Color.color.gray,
            Color.color.black
        };
        private Color.color _color = Color.color.red;

        /// <summary>
        /// Text of the tag: case-sensitive.
        /// Must be unique.
        /// Max.length: 120 characters, spaces included.
        /// </summary>
        public string title { get; private set; }
    }

    

}
