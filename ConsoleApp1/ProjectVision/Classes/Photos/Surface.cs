using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using ProjectVision;
using ProjectVision.Classes;
using SixLabors.ImageSharp;

namespace ProjectVision.Photos
{
    internal class Surface : PhotoInfo
    {
        public override List<RoomType> Type { get; set; } = new List<RoomType>() { RoomType.Surface };
        public override string ImageName { get; set; } = "Surface_Zone.png";
        public override Vector2 Center { get; set; } = new Vector2(776.5f,512);
        public override Size Size { get; set; } = new Size(1553, 1024);
        public override ZoneType Zone { get; set; } = ZoneType.Surface;
    }
}
