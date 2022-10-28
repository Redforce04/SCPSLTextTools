using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class CreateHint : Command
    {
        public override string Name => "CreateHint";
        public override string Description => "Create a hint for a player";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string playername = ((string)Arguments[0]).ToLower();
                if (!API.Api.PlayerHintStack.ContainsKey(playername))
                {
                    API.Api.PlayerHintStack.Add(playername, new List<HintStack>());
                    Response.Add($"Made new Player");
                }

                var hint = new HintStack() { Priority = (Arguments["Priority"] != null) ? (int) Arguments["Priority"] : 400, StartRow = (Arguments["StartRow"] != null) ? (int)Arguments["StartRow"] : 0 };
                API.Api.PlayerHintStack[playername].Add(hint);
                int results = API.Api.PlayerHintStack[playername].Count;
                Response.Add($"Hint Id: {results - 1}");

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
            },
            new CommandArgument()
            {
                Name = "Priority",
                Type = typeof(int),
                Required = false
            },
            new CommandArgument()
            {
                Name = "StartRow",
                Type = typeof(int),
                Required = false
            }

        };
    }

}
