using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Exiled.API.Enums;
using Newtonsoft.Json;

namespace ProjectVision.Commands
{

    public class GenerateMap : Command
    {
        public override string Name => "GenerateMap";
        public override string Description => "Generate a map based off of the imported json info";
        public override bool Hidden => false;

        public override async Task<bool> Execute()
        {
            try
            {
                if (Arguments["ZoneType"] == null)
                    API.Api.GenerateMap();
                else
                {
                    if (!ZoneType.TryParse((string)Arguments["ZoneType"], out ZoneType zone))
                    {
                        Response.Add($"Could not parse enum ZoneType from '{(string) Arguments["ZoneType"]}'");
                        return false;
                    }
                    API.Api.GenerateMap(zone);
                }

                Response.Add($"Map Generated");
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
                Name = "ZoneType",
                Type = typeof(string),
                Required = false
            }
        };
    }


}
