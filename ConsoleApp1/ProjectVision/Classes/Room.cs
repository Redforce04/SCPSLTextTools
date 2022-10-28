using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;

namespace ProjectVision
{
    public class RoomJson
    {
        public RoomJson()
        {

        }

        public RoomJson(string name, RoomType type, ZoneType zone, Vector position, Vector rotation)
        {
            Name = name;
            Type = type;
            Zone = Zone;
            Position = position;
            Rotation = rotation;
        }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public ZoneType Zone { get; set; }
        public Vector Position { get; set; }
        public Vector Rotation { get; set; }
    }

    public class Vector
    {
        public Vector()
        {

        }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; } = 0f;
        public float Y { get; set; } = 0f;
        public float Z { get; set; } = 0f;
    }

    public class Vector2
    {
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
