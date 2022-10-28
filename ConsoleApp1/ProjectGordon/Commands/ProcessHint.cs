using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class ProcessHint : Command
    {
        public override string Name => "ProcessHint";
        public override string Description => "Output a hint for a player.";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string playername = ((string)Arguments[0]).ToLower();
                int hintid = (int)Arguments["HintId"];
                if (!API.Api.PlayerHintStack.ContainsKey(playername))
                {
                    Response.Add($"Player {playername} not found.");
                    return false;
                }

                if (API.Api.PlayerHintStack[playername].Count < hintid + 1)
                {
                    Response.Add($"Hint is not present");
                    return false;
                }

                string output = "";
                var x = API.Api.PlayerHintStack[playername][hintid].Hint;
                foreach (var y in x)
                {
                    output = StringRow.MonoSpace(y.Text[0]) + "<br>";
                }
                Log.Raw($"{output}", "[TEXT]", ConsoleColor.DarkMagenta);
                Response.Add($"Hint Outputted.");
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
                Name = "HintId",
                Type = typeof(int),
                Required = true
            }
        };
    }

}
