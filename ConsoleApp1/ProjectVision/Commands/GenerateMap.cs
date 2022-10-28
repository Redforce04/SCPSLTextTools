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

    public class Asciify : Command
    {
        public override string Name => "Asciify";
        public override string Description => "Convert the map into ascii";
        public override bool Hidden => false;

        public override async Task<bool> Execute()
        {
            try
            {
                API.Api.Asciify();
                Response.Add($"Ascii Generated");
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
        };
    }


}
