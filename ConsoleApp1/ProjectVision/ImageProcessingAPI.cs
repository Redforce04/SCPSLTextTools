using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using SixLabors.Fonts;
using Image = SixLabors.ImageSharp.Image;
using Color = SixLabors.ImageSharp.Color;
using Path = SixLabors.ImageSharp.Drawing.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = SixLabors.ImageSharp.Point;


namespace ProjectVision
{
    internal class ImageProcessingAPI
    {
        public static async Task<Image> OverlayImage(Image Background, Image Foreground, SixLabors.ImageSharp.Point ForegroundPoint)
        {
            List<string> disposables = new List<string>();
            Image Composite = new Image<Rgba32>(Background.Width, Background.Height);
            Composite.Mutate(o => o
                    .DrawImage(Background, new SixLabors.ImageSharp.Point(0, 0), 1f)
                    .DrawImage(Foreground, ForegroundPoint, 1f)
                );
                return Composite;
            }
        public static async Task<Image> OverlayText(Image image, string text, float x, float y, int size, Color forecolor, Nullable<Color> backcolor, FontStyle style = FontStyle.Regular, BrushType brushtype = BrushType.Fill, HorizontalAlignment alignment = HorizontalAlignment.Left, string fontname = "8514oemr.ttf")
        {
            Image newImage = new Image<Rgba32>(image.Width, image.Height);
            FontCollection collection = new();
            FontFamily family = collection.Add($"{API.Api.FontDirectory}{fontname}");
            Font font = family.CreateFont((size * 0.75f), style);
            TextOptions options = new(font)
            {
                Origin = new PointF(x, y),
                TabWidth = 8,
                WrappingLength = 0,
                HorizontalAlignment = alignment
            };
            switch (brushtype)
            {
                case BrushType.Brush:
                    IBrush brush = backcolor.HasValue ? Brushes.Horizontal(forecolor, backcolor.Value) : Brushes.Horizontal(forecolor);
                    newImage = image.Clone(x => x.DrawText(options, text, brush));
                    break;
                case BrushType.Pen:
                    IPen pen = Pens.Solid(forecolor, 1);
                    newImage = image.Clone(x => x.DrawText(options, text, pen));
                    break;
                case BrushType.Fill:
                    newImage = image.Clone(x => x.DrawText(options, text, forecolor));
                    break;
            }


            return newImage;
        }
        public enum BrushType
        {
            Brush,
            Pen,
            Fill
        }

    }
}
