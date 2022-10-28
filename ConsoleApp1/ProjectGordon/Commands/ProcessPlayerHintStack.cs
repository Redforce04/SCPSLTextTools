using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class ProcessPlayerHintStack : Command
    {
        public override string Name => "ProcessPlayerHintStack";
        public override string Description => "Output a processed hint stack for a player.";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string playername = ((string)Arguments[0]).ToLower();
                if (!API.Api.PlayerHintStack.ContainsKey(playername))
                {
                    Response.Add($"Player {playername} not found.");
                    return false;
                }

                var output = API.Api.ProcessHint(API.Api.PlayerHintStack[playername]);
                Log.Raw(output, "[OUTPUT]", ConsoleColor.DarkMagenta);
                Response.Add("Succesfully Outputed Processed Hint Stack");
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
                Name = "Player",
                Type = typeof(string),
                Required = true
            }
        };
    }

}
