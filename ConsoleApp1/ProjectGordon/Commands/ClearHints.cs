using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class ClearHints : Command
    {
        public override string Name => "ClearHints";
        public override string Description => "Clear all hints";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {

                API.Api.PlayerHintStack = new Dictionary<string, List<HintStack>>();

                Response.Add($"Cleared All Players and player hints");
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
