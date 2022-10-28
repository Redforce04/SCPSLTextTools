using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace ProjectVision.Classes
{
    using System.Runtime.InteropServices.ComTypes;
    using System.Xml;
    using System.Xml.Serialization;

    public class Position
    {
        internal static Dictionary<ZoneType, List<Position>> positions = new Dictionary<ZoneType, List<Position>>();
        private int _id = 0;
        public Position(int x, int y, RoomType type, ZoneType zone, int rotation = 0)
        {
            Id = _id;
            _id++;
            X = x;
            Y = y;
            Type = type;
            Zone = zone;
            Rotation = rotation;
            if(!positions.ContainsKey(zone))
                positions.Add(zone, new List<Position>());
            positions[zone].Add(this);
        }

        public static List<int> X_Values(ZoneType zone)
        {
            List<int> xList = new List<int>();
            foreach (var x in positions[zone].OrderBy(z => z.X))
            {
                if (!xList.Contains(x.X))
                    xList.Add(x.X);
            }

            return xList;
        }
        public static List<int> Y_Values(ZoneType zone)
        {
            List<int> yList = new List<int>();
            foreach (var y in positions[zone].OrderBy(z => z.Y))
            {
                if (!yList.Contains(y.Y))
                    yList.Add(y.Y);
            }

            return yList;
        }

        public static List<Position> GetPositionsByX(int x, ZoneType zone)
        {
            return positions[zone].Where(z => z.X == x).ToList();
        }

        public static List<Position> GetPositionsByY(int y, ZoneType zone)
        {
            return positions[zone].Where(z => z.Y == y).ToList();
        }

        public static Position GetPosition(int x, int y, ZoneType zone)
        {
            return positions[zone].FirstOrDefault(z => z.X == x && z.Y == y);
        }

        public static Position GetPosition(int id, ZoneType zone)
        {
            return positions[zone].FirstOrDefault(z => z.Id == id);
        }

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;
        public int Rotation { get; set; } = 0;
        public int Id { get; private set; }

        public RoomType Type { get; set; }
        public ZoneType Zone { get; set; }
    }

    public class Grid
    {
        public Grid(List<int> xValues, List<int> yValues, ZoneType zone)
        {
            Zone = zone;
            _xTranslations = new Dictionary<int, int>();
            _yTranslations = new Dictionary<int, int>();
            foreach (var x in xValues.OrderByDescending(z => z))
            {
                if(!_xTranslations.ContainsKey(x))
                    _xTranslations.Add(x, _xTranslations.Count);
            }
            foreach (var y in yValues.OrderBy(z => z))
            {
                if(!_yTranslations.ContainsKey(y))
                    _yTranslations.Add(y, _yTranslations.Count);
            }

            if (Grids.ContainsKey(Zone))
                Grids[Zone] = this;
            else
                Grids.Add(Zone, this);
        }

        public static Dictionary<ZoneType, Grid> Grids = new Dictionary<ZoneType, Grid>();
        public int TranslateX(int x)
        {
            return _xTranslations[x];
        }

        public int TranslateY(int y)
        {
            return _yTranslations[y];
        }

        public int MinHeight()
        {
            return _yTranslations.Count * 256;
        }
        public int MinWidth()
        {
            return _xTranslations.Count * 256;
        }
        internal Dictionary<int, int> _xTranslations;
        internal Dictionary<int, int> _yTranslations;
        public ZoneType Zone { get; }
    }
}
