using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public static class Color
    {
        private static Dictionary<color, string> colorValues = new Dictionary<color, string>()
        {
            { color.red, "f24726" },
            { color.magenta, "da0063" },
            { color.violet, "9510ac" },
            { color.light_green, "cee741" },
            { color.green, "8fd14f" },
            { color.dark_green, "0ca789" },
            { color.cyan, "12cdd4" },
            { color.blue, "2d9bf0" },
            { color.dark_blue, "414bb2" },
            { color.yellow, "fac710" },
            { color.gray, "808080" },
            { color.black, "1a1a1a" }
        };

        public static string GetColorHex(color color) => colorValues[color];
        public enum color
        {
            red,
            magenta,
            violet,
            light_green,
            green,
            dark_green,
            cyan,
            blue,
            dark_blue,
            yellow,
            gray,
            black
        }
    }
}
