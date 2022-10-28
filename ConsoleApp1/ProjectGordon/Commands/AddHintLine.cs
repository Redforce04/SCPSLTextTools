using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class AddHintLine : Command
    {
        public override string Name => "AddHintLine";
        public override string Description => "Add a Line to a player's hint";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string playername = ((string)Arguments[0]).ToLower();
                int hintId = (int)Arguments["HintId"];
                if (!API.Api.PlayerHintStack.ContainsKey(playername))
                {
                    API.Api.PlayerHintStack.Add(playername, new List<HintStack>());
                    Response.Add($"Made new Player");
                }
                
                var x = API.Api.PlayerHintStack[playername];
                if (x.Count < hintId + 1)
                {
                    x[hintId] = new HintStack();
                    Response.Add($"Made new Hint Stack");
                }

                List<string> row = (List<string>)Arguments["Line"];
                var hint = x[hintId];
                hint.Hint.Add(new StringRow(row));

                x[hintId] = hint;
                API.Api.PlayerHintStack[playername] = x;

                Response.Add($"Line Added to Hint");
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
            }, new CommandArgument()
            {
                Name = "Line",
                Type = typeof(List<string>),
                Required = true,
                Remainder = true
            }
        };

    }

}
