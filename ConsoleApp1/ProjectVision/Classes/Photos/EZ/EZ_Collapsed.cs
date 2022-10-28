using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using ProjectVision;
using SixLabors.ImageSharp;
using ProjectVision.Classes;


namespace ProjectVision.Photos
{
    internal class EZ_Collapsed : PhotoInfo
    {
        public override List<RoomType> Type { get; set; } = new List<RoomType>() { RoomType.EzCollapsedTunnel };
        public override string ImageName { get; set; } = "EZ_Collapsed.png";
        public override Vector2 Center { get; set; } = new Vector2(128, 51.5f);
        public override Size Size { get; set; } = new Size(256, 256);
        public override ZoneType Zone { get; set; } = ZoneType.Entrance;
    }
}
