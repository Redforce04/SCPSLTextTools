using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.Fonts;

namespace ProjectMiro.Framework
{
    public static class FontEnum
    {
        public static Dictionary<string, FontFamily> Fonts = new Dictionary<string, FontFamily>()
        {
            { "arial", FontFamily.Arial },
            { "segoe_script", FontFamily.Segoe_Script },
            { "abril_fatface", FontFamily.Abril_Fatface },
            { "bangers", FontFamily.Bangers },
            { "eb_garamond", FontFamily.EB_Garamond },
            { "georgia", FontFamily.Georgia },
            { "graduate", FontFamily.Graduate },
            { "gravitas_one", FontFamily.Gravitas_One },
            { "fredoka_one", FontFamily.Fredoka_One },
            { "nixie_one", FontFamily.Nixie_One },
            { "opensans", FontFamily.OpenSans },
            { "permanent_marker", FontFamily.Permanent_Marker },
            { "pt_sans", FontFamily.PT_Sans },
            { "pt_sans_narrow", FontFamily.PT_Sans_Narrow },
            { "pt_serif", FontFamily.PT_Serif },
            { "rammetto_one", FontFamily.Rammetto_One },
            { "roboto", FontFamily.Roboto },
            { "roboto_condensed", FontFamily.Roboto_Condensed },
            { "roboto_slab", FontFamily.Roboto_Slab },
            { "caveat", FontFamily.Caveat },
            { "times_new_roman", FontFamily.Times_New_Roman },
            { "titan_one", FontFamily.Titan_One },
            { "lemon_tuesday", FontFamily.Lemon_Tuesday },
            { "roboto_mono", FontFamily.Roboto_Mono },
            { "noto_sans", FontFamily.Noto_Sans },
            { "ibm_plex_sans", FontFamily.IBM_Plex_Sans },
            { "ibm_plex_serif", FontFamily.IBM_Plex_Serif },
            { "ibm_plex_mono", FontFamily.IBM_Plex_Mono },
            { "spoof", FontFamily.Spoof },
            { "tiempos_text", FontFamily.Tiempos_Text },
        };
        public static FontFamily GetAccessEnum(string input)
        {
            input = input.ToLower().Replace(" ", "_");
            if (!Fonts.ContainsKey(input))
                throw new ArgumentNullException(input, "Fonts does not contain this enum.");
            return Fonts[input];
        }

        /// <summary>
        /// Possible font families
        /// </summary>
        public enum FontFamily
        {
            Arial,
            Segoe_Script,
            Abril_Fatface,
            Bangers,
            EB_Garamond,
            Georgia,
            Graduate,
            Gravitas_One,
            Fredoka_One,
            Nixie_One,
            OpenSans,
            Permanent_Marker,
            PT_Sans,
            PT_Sans_Narrow,
            PT_Serif,
            Rammetto_One,
            Roboto,
            Roboto_Condensed,
            Roboto_Slab,
            Caveat,
            Times_New_Roman,
            Titan_One,
            Lemon_Tuesday,
            Roboto_Mono,
            Noto_Sans,
            IBM_Plex_Sans,
            IBM_Plex_Serif,
            IBM_Plex_Mono,
            Spoof,
            Tiempos_Text
        }
    }
    
}
