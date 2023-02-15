using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ConsoleApp1;
using Exiled.API.Enums;
using Newtonsoft.Json;  
using ProjectVision;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using Color = ProjectMiro.Framework.Color;
using Point = SixLabors.ImageSharp.Point;

namespace ProjectMiro.Commands
{

    public class XMLify : Command
    {
        public override string Name => "XMLify";
        public override string Description => "Load xml to numeric representations.";
        public override bool Hidden => false;

        public override async Task<bool> Execute()
        {
            try
            {
                string fileLoc = "";

                foreach (string loc in (List<string>)Arguments["FileLocation"])
                {
                    fileLoc += $"{loc} ";
                }

                fileLoc = fileLoc.Trim();

                try
                {
                    string SaveLocation = "";
                    var array = fileLoc.Split('/');
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        SaveLocation += $"{array[i]}/";
                    }
                    var vwdata = VWXMLParser.ParseXML(fileLoc);
                    float scale = 1f / 4f;
                    Image bkgd = new Image<Rgba32>(5000,5000);
                    Image point = Image.Load($"{SaveLocation}point.png");
                    float xlength = 6;
                    float ylength = 6;
                    int maxX = 0;
                    int minX = 0;
                    int maxY = 0;
                    int minY = 0;
                    
                    foreach (LightingFixture fixture in vwdata.InstrumentData.LightingFixture)
                    {
                        
                        float x = (float) fixture.XLocationMm * scale;
                        float y = (float) fixture.ZLocationMm * scale;
                        if (x < minX)
                            minX = (int) x;
                        if (y < minY)
                            minY = (int) y;
                        if (x > maxX)
                            maxX = (int) x;
                        if (y > maxY)
                            maxY = (int) y;

                        PathBuilder path = new PathBuilder();
                        path.AddLine(new SixLabors.ImageSharp.Point((int) (x - xlength), (int) (y - ylength)), new SixLabors.ImageSharp.Point((int)(x + xlength), (int)(y + ylength)));
                        var finishedPath = path.Build();
                        IPathCollection collection = new PathCollection(finishedPath);
                        var placement = new SixLabors.ImageSharp.Point((int)x, (int)y);
                        try
                        {

                            bkgd = await ImageProcessingAPI.OverlayImage(bkgd, point, placement);
                        }
                        catch (Exception e)
                        {
                            Log.Error($"Could not overlay at point ({placement.X},{placement.Y}). Exception: {e}");
                            continue;
                        }
                        //bkgd = await ImageProcessingAPI.OverlayText(bkgd, "+", placement.X, placement.Y,
                        //    30, SixLabors.ImageSharp.Color.Red, null, default, default, default, "arial.ttf");

                    }
                    Log.Debug($"X: ({minX}, {maxX}) Y: ({minY}, {maxY})");
                    
                    //SaveLocation = (SaveLocation.Length < 2) ? SaveLocation : SaveLocation.Substring(0, SaveLocation.Length - 1);
                    SaveLocation += $"testing.png";
                    await bkgd.SaveAsync(SaveLocation);
                }
                catch (Exception e)
                {
                    Response.Add($"XML deserialization failed. Exception: {e}");
                }
                return true;

            }
            catch (Exception e)
            {
                Response.Add($"An error has occured while trying to execute the example command. Exception: {e}");
                return false;
            }
        }

        public override void Register()
        {
            return;
        }

        public override List<CommandArgument> RequiredArguments { get; } = new List<CommandArgument>()
        {
            new CommandArgument()
            {
                Name = "FileLocation",
                Type = typeof(List<string>),
                Required = true,
                Remainder = true
            }
        };
    }


}
