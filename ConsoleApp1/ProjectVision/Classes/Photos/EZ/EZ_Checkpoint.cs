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
    internal class EZ_Checkpoint : PhotoInfo
    {
        public override List<RoomType> Type { get; set; } = new List<RoomType>() { RoomType.HczEzCheckpoint };
        public override string ImageName { get; set; } = "EZ_Checkpoint.png";
        public override Vector2 Center { get; set; } = new Vector2(0,0);
        public override Size Size { get; set; } = new Size(256, 256);
        public override ZoneType Zone { get; set; } = ZoneType.Entrance;
    }
}
