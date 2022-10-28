using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Exiled.API.Enums;
using ProjectVision;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ProjectVision.Classes
{
    public abstract class PhotoInfo
    {
        public PhotoInfo()
        {
            if (Type is null)
                Type = new List<RoomType>();
            
        }
        public abstract List<RoomType> Type { get; set; }
        public virtual List<Position> Positions { get; set; } = new List<Position>();
        public abstract ZoneType Zone { get; set; }
        public abstract string ImageName { get; set; }
        public virtual Vector2 Center { get; set; } = new Vector2(128, 128);
        public virtual Size Size { get; set; } = new Size(256, 256);
        public virtual Image<Rgba32> Image()
        {
            try
            {
                return SixLabors.ImageSharp.Image.Load<Rgba32>($"{API.Api.ImagesDirectory}" + ImageName);
            }
            catch (Exception e)
            {
                if (e is System.IO.FileNotFoundException)
                    Log.Error($"File {API.Api.ImagesDirectory}{ImageName} Not Found");
                else
                    Log.Error($"Couldn't load photo for room {Type}. Exception: {e}");
                return null;
            }
        }
    }
}
