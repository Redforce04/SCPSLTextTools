using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Exiled.API.Enums;
using IESLights;
using Newtonsoft.Json;
using ProjectVision.Classes;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using UnityEngine;
using Color = SixLabors.ImageSharp.Color;
using Point = SixLabors.ImageSharp.Point;

namespace ProjectVision
{
    public class API
    {
        public static API Api;
        public static void Enable()
        {
            Api = new API();
            Configuration.Default.MemoryAllocator = MemoryAllocator.Create(new MemoryAllocatorOptions()
            {
                MaximumPoolSizeMegabytes = 512
            });
            Api.Rooms = new List<RoomJson>();
            Api.photoInfos = new Dictionary<RoomType, PhotoInfo>();
            Api.loaded();
            Log.Debug($"Loaded {Api.photoInfos.Count} photo{(Api.photoInfos.Count == 1 ? "" : "s" )}");
        }

        public List<RoomJson> Rooms;

        internal readonly string ImagesDirectory = @"C:\Users\AJ Blessing\Downloads\Zone Photos\";
        internal readonly string TempDirectory = @"C:\Users\AJ Blessing\Downloads\Zone Photos\temp\";
        internal readonly string FontDirectory = @"C:\Users\AJ Blessing\Downloads\Zone Photos\fonts\";

        private Dictionary<RoomType, PhotoInfo> photoInfos;

        public void GenerateMap(ZoneType generateZone = ZoneType.Unspecified)
        {
            Log.Info($"Loading Map");
            if (!loaded())
            {
                Log.Error($"Map Could Not Generate. API not loaded.");
                return;
            }

            foreach(RoomJson room in Rooms)
            {
                if (!photoInfos.ContainsKey(room.Type))
                {
                    if(room.Type != RoomType.Pocket)
                       Log.Error($"Photo Info doesn't contain room {room.Type}");
                    continue;
                }

                PhotoInfo info = photoInfos[room.Type];
                if (generateZone == ZoneType.Unspecified || generateZone == info.Zone)
                {
                    info.Positions.Add(new Position((int)room.Position.X, (int)room.Position.Z, room.Type, info.Zone, (int) room.Rotation.Y));
                }   
            }

            if (generateZone == ZoneType.Unspecified)
            {
                foreach (ZoneType type in (ZoneType[])Enum.GetValues(typeof(ZoneType)))
                    if (type != ZoneType.Unspecified)
                    {
                        new Grid(Position.X_Values(type), Position.Y_Values(type), type);
                        GenerateZoneMap(type);
                    }
            }
            else
            {
                new Grid(Position.X_Values(generateZone), Position.Y_Values(generateZone), generateZone);
                GenerateZoneMap(generateZone);
            }


            foreach (var Grid in Grid.Grids)
            {
                string xValues = "";
                string yValues = "";
                foreach (var x in Grid.Value._xTranslations)
                    xValues += $"{(xValues.Length == 0 ? "" : ", ")}({x.Key},{x.Value})";

                foreach (var y in Grid.Value._yTranslations)
                    yValues += $"{(yValues.Length == 0 ? "" : ", ")}({y.Key},{y.Value})";
                Log.Debug($"Grid: {Grid.Key}, Min (X,Y): ({Grid.Value.MinWidth()},{Grid.Value.MinHeight()}) X-Values: {xValues}. Y-Values: {yValues}.");
            }
            
        }

        public async void GenerateZoneMap(ZoneType zone)
        {
            if (!Grid.Grids.ContainsKey(zone))
            {
                Log.Error($"Cannot generate grid. Grid does not exist. Type {zone}.");
                return;
            }

            List<RoomJson> rooms = Rooms.Where(r => r.Zone == zone && r.Type != RoomType.Pocket).ToList();
            Grid grid = Grid.Grids[zone];
            Image bkgd = new Image<Rgba32>(grid.MinWidth(), grid.MinHeight());
            foreach (var room in rooms)
            {
                try
                {

                    int x = ((grid.TranslateX((int)room.Position.X)) * 256);
                    int y = ((grid.TranslateY((int)room.Position.Z)) * 256);
                    SixLabors.ImageSharp.Point point = new SixLabors.ImageSharp.Point(x, y);
                    Image image = photoInfos[room.Type].Image();
                    //if (room.Type == RoomType.EzIntercom)
                    //room.Rotation.Y -= 90;
                    image.Mutate(o => o.Rotate(room.Rotation.Y + 180));
                    /*if (room.Rotation.Y != 0)
                        image.Mutate(o =>
                            o.Fill(
                                (room.Rotation.Y == 90 ? Color.Cyan :
                                    (room.Rotation.Y == 180) ? Color.Green : Color.Yellow),
                                new Star(128, 128, 5, 10, 20).RotateDegree(180)));
                    */
                    //Log.Debug($"Image Size: ({image.Width},{image.Height}) Point: ({point.X},{point.Y})");
                    bkgd = await ImageProcessingAPI.OverlayImage(bkgd, image, point);
                }
                catch (Exception e)
                {
                    if (e is KeyNotFoundException)
                    {
                        Log.Warn($"Key not found for room {room.Name}, Zone {room.Zone}, Type {room.Type} at position ({room.Position.X},{room.Position.Z})");
                        continue;
                    }
                    Log.Error($"An error has been caught at GenerateZoneMap. Exception: {e}");
                }
            }
            await bkgd.SaveAsPngAsync(Api.TempDirectory + $"{zone}-Map.png");
            
            Log.Debug($"Map of {zone} Generated");
        }
        


        private bool _loaded = false;
        internal bool loaded()
        {
            if (_loaded)
                return true;
            if (!Directory.Exists(ImagesDirectory))
            {
                Log.Info($"Images directory at {ImagesDirectory} missing. Creating directory. Images must be moved into this directory.");
                try
                {

                    Directory.CreateDirectory(ImagesDirectory);
                }
                catch (Exception e)
                {
                    Log.Error($"Couldn't create images directory. Error: {e}");
                }

                return false;
            }
            
            foreach (Type type in Assembly.GetAssembly(typeof(API)).GetTypes())                                                                                                                                                                                                                                        
            {
                if (type.BaseType == typeof(PhotoInfo))
                {
                    PhotoInfo photo = null;
                    ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                    if (!(constructorInfo is null))
                    {
                       photo = constructorInfo.Invoke(null) as PhotoInfo;
                    }
                    else
                    {
                        object value = Array
                            .Find(type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic),
                                info => info.PropertyType == type)?.GetValue(null);
                        if (!(value is null))
                            photo = value as PhotoInfo;
                    }

                    if (photo is null)
                    {
                        Log.Error($"{type.FullName} is a valid PhotoInfo, but it cannot be instantiated! It either doesn't have a public default constructor without any arguments or a static property of the {type.FullName} type!");
                        continue;
                    }
                    foreach(var photoType in photo.Type)
                        photoInfos.Add(photoType, photo);
                }

            }

            _loaded = true;
            return true;
        }
        private string imageDirectory => TempDirectory;   // path to image samples
        private float fontAspect = 8f / 12f;                 // symbol width divided by height in pixels
        private int consoleWidth = 80;                           // console width in chars

        public void Asciify()
        {
            var options = new PicToAsciiOptions()
            {
                FixedDimension = PicToAsciiOptions.Fix.Horizontal,
                FixedSize = consoleWidth,
                SymbolAspectRatio = fontAspect
            };

            var pic2ascii = new PicToAscii(options);

            foreach (var filename in ImageSamples)
            {
                IReadOnlyList<ColorTape> colorTapes;
                try
                {
                    using Stream stream = File.OpenRead(filename);

                    pic2ascii.Options.AsciiTable = Environment.TickCount % 2 == 0
                        ? PicToAsciiOptions.ASCIITABLE_SOLID
                        : PicToAsciiOptions.ASCIITABLE_SYMBOLIC;

                    //colorTapes = PicToAscii.CreateDefault.Convert(stream);
                    colorTapes = pic2ascii.Convert(stream);
                }
                catch
                {
                    continue;
                }

                PrintTapes(colorTapes);

            }
        }

        private IEnumerable<string> ImageSamples => Directory
            .GetFiles(imageDirectory, "*", SearchOption.TopDirectoryOnly)
            .Where(f => f.LastIndexOf(".jpg") > -1
                     || f.LastIndexOf(".jpeg") > -1
                     || f.LastIndexOf(".png") > -1);

        private void PrintTapes(IReadOnlyList<ColorTape> colorTapes)
        {
            foreach (var tape in colorTapes)
            {
                Console.ForegroundColor = tape.ForeColor;
                Console.Write(tape.Chunk);
            }

            Console.ResetColor();
        }

    }
}
