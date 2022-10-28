using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using ConsoleApp1;
using Newtonsoft.Json;

namespace ProjectGordon
{
    /// <summary>
    /// A row made up of multiple strings that can have processing done for spacing and sizing.
    /// </summary>
    public class StringRow
    {
        /// <summary>
        /// Creates a string row.
        /// </summary>
        /// <param name="text">The spaced strings used for the row.</param>
        /// <param name="textAlign">What side everything should be spaced for.</param>
        /// <param name="lineSize">The size of the line to compute.</param>
        public StringRow(List<string> text, float lineSize = 35f)
        {
            Text = text;
            LineSize = lineSize;
        }

        /// <summary>
        /// A list of text in order from left to right that will be spaced evenly.
        /// </summary>
        public List<string> Text { get; set; } = new List<string>()
        {
            ""
        };

        /// <summary>
        /// The max line width before overflow occurs.
        /// </summary>
        private static int MaxLineSize => ProjectGordon.API.Api.MaxLineSize;

        /// <summary>
        /// The dictionary of character widths.
        /// </summary>
        private static Dictionary<char, int> CharWidth => ProjectGordon.API.Api.CharacterWidth;

        /// <summary>
        /// The line size of this row.
        /// </summary>
        public float LineSize = 35;

        /// <summary>
        /// The width of one monospaced character
        /// </summary>
        internal static float MonoSpacedWidth { get; } = 1870;

        /*public string Space(bool centerCenter = true)
        {
            string output = "";
            float totalLength = 0;
            List<float> Lengths = new List<float>(); // The list of spaces between multiple strings to make sure they are perfectly 
            foreach (var x in Text)
            {
                float stringwidth = 0;
                for (int c = 0; c < x.Length; c++)
                {
                    Char character = x[c];
                    bool isLowerCase = char.IsLower(x[c]);

                    if (!CharWidth.ContainsKey(character) && !isLowerCase) // if lower case then process it with multiplier
                    {
                        Log.Error($"Character: \'{character}\', not found.");
                        continue;
                    }

                    if (isLowerCase)
                        stringwidth += (CharWidth[char.ToUpper(character)] * 4) / 5;
                    else
                        stringwidth += CharWidth[character];
                }

                totalLength += stringwidth;
                Lengths.Add(stringwidth);
            }
            // A   B   C   D
            //  -1- -2- -3-
            // A+B+C+D
            if (centerCenter)
            {
                // split it into x quadrants (1 2 3 4) find perfect spacing for those, then calculate center + spacing
                for (int z = 0; z < Lengths.Count - 1; z++)
                {
                        
                }
            }
            int lspacer =
                Mathf.Clamp((int)(

                    ((MaxLineSize * .5f) - Lengths[TextAlign.Left] -
                     (Lengths[TextAlign.Center] * .5f)) / CharWidth[' ']), (int)0, (int)100000);
            int rspacer =
                Mathf.Clamp((int)(

                        ((MaxLineSize * .5f) - Lengths[TextAlign.Right] -
                        (Lengths[TextAlign.Center] * .5f)) / CharWidth[' ']), (int)0, (int)100000);
            output += Text[TextAlign.Left];
            for (int i = 0; i < lspacer; i++)
            {
                output += ' ';
            }

            float halfLine = MaxLineSize / 2f;
            float lspacewidth = lspacer * CharWidth[' '];
            float lwidth = Lengths[TextAlign.Left];
            float lcentercomp = (Lengths[TextAlign.Center] * .5f);
            float lcountercomp = lspacewidth + lwidth + lcentercomp;
            float L = halfLine - lcountercomp;
            float LFontSize = 0f;
            if (L > 14.3)
            {
                LFontSize = (float)Math.Truncate((double)L / (498d / 35d));
                output += $"<size={LFontSize - 1}> </size><size=35>";
            }




            output += Text[TextAlign.Center];
            float rspacewidth = rspacer * CharWidth[' '];
            float rwidth = Lengths[TextAlign.Right];
            float rcentercomp = (Lengths[TextAlign.Center] * .5f);
            float rcountercomp = rspacewidth + rwidth + rcentercomp;
            float R = halfLine - rcountercomp;
            float RFontSize = 0f;
            if (R > 14.3)
            {
                RFontSize = (float)Math.Truncate((double)R / (498d / 35d));
                output += $"<size={RFontSize - 1}> </size><size=35>";
            }
            for (int i = 0; i < rspacer; i++)
            {
                output += ' ';
            }
            output += Text[TextAlign.Right];
            //Log.SendRaw($"SpareSize[L,R]: ({L},{R}), FontSize[L,R]: ({LFontSize},{RFontSize}, L[1,2,3,4]: ({lspacewidth}, {lwidth}, {lcentercomp}, {lcountercomp}), R[1,2,3,4]: ({rspacewidth}, {rwidth}, {rcentercomp}, {rcountercomp})", ConsoleColor.Magenta);
            //Log.Debug($"Mathing rq. Done. Lengths[L,C,R] = ({Lengths[TextAlign.Left]},{Lengths[TextAlign.Center]},{Lengths[TextAlign.Right]}) Spacer[L,R]: ({lspacer},{rspacer}), total: {total}, Max: {MaxLineSize}) \n");
            return output;
        }
        /*public string Boxify()
        {

        }*/
        public static string MonoSpace(string Text)
        {
            // Just making everything perfectly spaced.

            string output = $""; // Output initializer
            float fontSize = 35f; // the largest font size acceptable

            string text = Text.Replace("\n", ""); // new lines will break things.
            List<CharSpacer> Spacing = new List<CharSpacer>(); // create an array that is used for tracking characters
            
            // foreach charcter in the text
            for (int c = 0; c < text.Length; c++)
            {
                // process the width automatically
                CharSpacer character = new CharSpacer(text[c]);
                //if (character.isSpace)
                //{
                //    continue;
                //}
                Spacing.Add(character);
            }

            // process spacing
            Regex expression = new Regex(@"\s+");
            var matches = expression.Matches(text);
            foreach (Match x in matches)
            {
                float mult = 1f;
                float spacerSize = (CharSpacer.CalculateCharacterWidth(' ', ref mult, fontSize) * x.Length);
                if (x.Index == 0)
                    Spacing[x.Index + 1].WidthBefore += spacerSize;
                else if (x.Index + 2 < Spacing.Count)
                    Spacing[x.Index - 1].WidthAfter += spacerSize;
                else 
                    Log.Error($"Spacing cannot be added because a lack of reference. Index: {x.Index}, SpacerSize: {spacerSize}, Spacing Count: {Spacing.Count}");
            }


            for (int i = 0; i < Spacing.Count; i++)
            {
                string Add = "";
                CharSpacer thisChar = Spacing[i];
                if(thisChar.isSpace)
                    continue;
                
                CharSpacer nextChar = (i + 2 > Spacing.Count) ? null : Spacing[i + 1];
                
                bool prevCharIsNull = (i - 1 < 0);
                float widthAfter = thisChar.WidthAfter;


                if (prevCharIsNull)
                {

                    Add += CharSpacer.GetSizer(thisChar.WidthBefore, fontSize, thisChar.FontSize);
                }
                
                if (nextChar != null)
                    widthAfter += nextChar.WidthBefore;
                
                Add += CharSpacer.GetSizer(thisChar.WidthAfter, fontSize, thisChar.FontSize);
                Add += $"{thisChar.Character}"; // Add character

                output += Add;
            }

            return output;

        }
        
    }

    public class JSONStringRow
    {
        public string Left { get; set; } = "";
        public string Center { get; set; } = "";
        public string Right { get; set; } = "";

    }

    public class CharSpacer
    {
        private static Dictionary<char, int> CharWidth => ProjectGordon.API.Api.CharacterWidth;

        public CharSpacer(char character)
        {
            Character = character;
            if (character == ' ' || character == ' ')
                isSpace = true;
            CalculateWidth();
        }

        public void CalculateWidth()
        {
            float mult = 1f;
            Width = CalculateCharacterWidth(Character, ref mult);
            
            if (Width > (StringRow.MonoSpacedWidth * mult))
            {
                Log.Error($"Width is bigger than the maximum character size. Char: \'{Character}\' Width: {Width}, Max: {StringRow.MonoSpacedWidth} Mult: {mult}");
                return;
            }
            float spacer = (StringRow.MonoSpacedWidth - Width) / 2f;
            WidthBefore += spacer;
            WidthAfter += spacer;
        }

        public static string GetSizer(float width, float MaxFontSize = 35f, float NormalTextFontSize = 35f)
        {
            if (width < 0)
            {
                Log.Error($"Width {width} is negative at GetSizer.");
                return "";
            }
            float spaceBaseSize = CharWidth[' '];
            if(MaxFontSize != 35)
                spaceBaseSize *= MaxFontSize/35;
            int charactersNeeded = 1;
            
            if (width > spaceBaseSize)
            {
                charactersNeeded = ((int) (width / spaceBaseSize)) + 1;
            }

            float sizeNeeded = 1f;
            sizeNeeded = width / (charactersNeeded * (498f / 35f));
            if (sizeNeeded > MaxFontSize)
            {
                Log.Error($@"Size has exceeded max font size. Characters needed: {charactersNeeded}, Width: {width}, MaxFontSize: {MaxFontSize}, Size Needed: {sizeNeeded}");
                return "";
            }
            string outputSpaces = $"";

            for (int i = 0; i < charactersNeeded; i++)
            {
                outputSpaces += " ";
            }

            return $"<size={sizeNeeded}>" + outputSpaces + $"<size={NormalTextFontSize}>";
        }
        public static float CalculateCharacterWidth(char c, ref float Multiplier, float FontSize = 35f)
        {
            if (!CharWidth.ContainsKey(char.ToUpperInvariant(c)))
            {
                Log.Error($"CharacterWidth doesn't contain key ({char.GetNumericValue(c)}) \' {char.ToUpperInvariant(c)}\'");
                c = ' ';
            }
            float width = CharWidth[char.ToUpperInvariant(c)];
            if (FontSize != 35)
            {
                Multiplier *= FontSize / 35f;
            }

            // if it is lower case it is only 80% of the width
            if (Char.IsLower(c))
            {
                Multiplier *= 4f / 5f;
            }
            return width * Multiplier;

        }
        public float FontSize { get; set; } = 35f;
        public char Character { get; set; } = ' ';
        public float Width { get; set; } = 1000;
        public float WidthBefore { get; set; } = 0;
        public float WidthAfter { get; set; } = 0;
        public bool isSpace { get; set; } = false;
    }

}

