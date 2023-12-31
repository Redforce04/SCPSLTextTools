﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectVision
{
    public class PicToAscii
    {
        static ColorSpaceConverter _colorSpaceConvert = new ColorSpaceConverter();
        public readonly PicToAsciiOptions Options = new PicToAsciiOptions();

        private PicToAscii() { }
        public static PicToAscii CreateDefault => new PicToAscii();

        public PicToAscii(PicToAsciiOptions options)
        {
            Options = options;
        }

        public IReadOnlyList<ColorTape> Convert(Stream stream)
        {
            using Image<Rgb24> source = Image.Load<Rgb24>(stream);

            int width, height;
            float sourceAspect = (float)source.Width / source.Height;

            switch (Options.FixedDimension)
            {
                case PicToAsciiOptions.Fix.Vertical:
                    height = Options.FixedSize;
                    width = (int)Math.Round(Options.FixedSize * sourceAspect / Options.SymbolAspectRatio);
                    break;

                default:
                case PicToAsciiOptions.Fix.Horizontal:
                    width = Options.FixedSize;
                    height = (int)Math.Round(Options.FixedSize / sourceAspect * Options.SymbolAspectRatio);
                    break;
            }

            return ConvertInternal(source, width, height);
        }

        /// <summary>
        /// Returns a list of the colored string chunks
        /// </summary>
        /// <param name="source">Bitmap source image</param>
        /// <param name="width">Result width in symbols</param>
        /// <param name="height">Result height in symbols</param>
        /// <returns>Colored tapes ready to print to the console</returns>
        private IReadOnlyList<ColorTape> ConvertInternal(Image<Rgb24> source, int width, int height)
        {
            using var reduced = source.Clone(x => x.Resize(width, height, Options.Resampler));

            var chunks = new List<ColorTape>();

            StringBuilder chunkBuilder = new StringBuilder();
            ConsoleColor lastColor = ConsoleColor.Black;
            for (int y = 0; y < reduced.Height; y++)
            {
                reduced.ProcessPixelRows( z =>
                {
                    foreach (var rgb in z.GetRowSpan(y))
                    {
                        ConsoleColor cc = ToConsoleColor(rgb);

                        if (lastColor != cc)
                        {
                            if (chunkBuilder.Length > 0)
                            {
                                var tape = new ColorTape(chunkBuilder.ToString(), lastColor);
                                chunks.Add(tape);

                                chunkBuilder.Clear();
                            }

                            lastColor = cc;
                        }

                        float bright = _colorSpaceConvert.ToHsl(rgb).L;
                        char symbol = BrightnessToChar(bright, Options.AsciiTable);
                        chunkBuilder.Append(symbol);
                    }
                });

                chunkBuilder.Append(Environment.NewLine);
            }

            if (chunkBuilder.Length > 0)
                chunks.Add(new ColorTape(chunkBuilder.ToString(), lastColor));

            return chunks;
        }

        private char BrightnessToChar(float bright, string symbols)
        {
            int charIndex = (int)(bright * (symbols.Length - 1));
            return symbols[charIndex];
        }

        private ConsoleColor ToConsoleColor(Rgb24 c)
        {
            // bright bit
            int index = (
                  c.R > Options.Threshold_RedBright
                | c.G > Options.Threshold_GreenBright
                | c.B > Options.Threshold_BlueBright
                ) ? 8 : 0;

            // color bits
            var t = Options.Threshold_ValuableColor;
            float max = Math.Max(c.R, Math.Max(c.G, c.B));
            index |= (c.R / max > t) ? 4 : 0;
            index |= (c.G / max > t) ? 2 : 0;
            index |= (c.B / max > t) ? 1 : 0;

            return (ConsoleColor)index;
        }
    }

    public class PicToAsciiOptions
    {
        // sorted ascending by brightness: darker --> lighter
        public const string ASCIITABLE_SOLID = " ░▒▓█";
        public const string ASCIITABLE_SYMBOLIC = " `'.,:;i+o*%&$#@";
        public const string ASCIITABLE_SYMBOLIC_LIGHT = " `'.,:;i+o*wW%&$#@▒▓█";

        public IResampler Resampler = new BicubicResampler();

        public float Threshold_ValuableColor = .8f;
        public int Threshold_RedBright = 200;
        public int Threshold_GreenBright = 170;
        public int Threshold_BlueBright = 220;

        public enum Fix { Horizontal = 0, Vertical }
        public Fix FixedDimension = Fix.Horizontal;
        public int FixedSize = 80;

        public string AsciiTable = ASCIITABLE_SOLID;
        public float SymbolAspectRatio = .5f;
    }

    public class ColorTape
    {
        public ColorTape(string chunk, ConsoleColor color)
        {
            ForeColor = color;
            Chunk = chunk;
        }

        public ConsoleColor ForeColor;
        public string Chunk;
    }
}
