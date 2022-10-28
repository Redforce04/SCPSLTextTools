using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Newtonsoft.Json;

namespace ProjectVision.Commands
{

    public class ImportData : Command
    {
        public override string Name => "ImportData";
        public override string Description => "Imports data from a file location.";
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
                
                if (!File.Exists(fileLoc))
                {
                    Response.Add($"Could not find file \'{fileLoc}\'.");
                    return false;
                }
                string json = File.ReadAllText(fileLoc);
                try
                {
                    API.Api.Rooms = JsonConvert.DeserializeObject<List<RoomJson>>(json);
                }
                catch (Exception e)
                {
                    Response.Add($"Json deserialization failed. Exception: {e}");
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
