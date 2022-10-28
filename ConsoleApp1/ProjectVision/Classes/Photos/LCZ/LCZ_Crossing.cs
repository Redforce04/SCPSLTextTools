using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using SixLabors.ImageSharp;
using ProjectVision;
using ProjectVision.Classes;


namespace ProjectVision.Photos
{
    internal class LCZ_Crossing : PhotoInfo
    {
        public override List<RoomType> Type { get; set; } = new List<RoomType>() { RoomType.LczCrossing };
        public override string ImageName { get; set; } = "LCZ_Crossing.png";
        public override Vector2 Center { get; set; } = new Vector2(0, 0);
        public override Size Size { get; set; } = new Size(256, 256);
        public override ZoneType Zone { get; set; } = ZoneType.LightContainment;
    }
}
