using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class AddPlayer : Command
    {
        public override string Name => "AddPlayer";
        public override string Description => "Add a player to the PlayerList";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string playername = ((string)Arguments[0]).ToLower();
                if (!API.Api.PlayerHintStack.ContainsKey(playername))
                {
                    API.Api.PlayerHintStack.Add(playername, new List<HintStack>());
                    Response.Add($"Added Player {playername}.");
                    return true;
                }

                    Response.Add($"Player already present");
                    return false;

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
