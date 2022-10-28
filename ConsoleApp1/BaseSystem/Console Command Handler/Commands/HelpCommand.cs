using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class HelpCommand : Command
    {
        public override string Name { get; } = "Help";
        public override string Description { get; } = "Shows a list of all registered commands, their names, description, and usage.";
        public override async Task<bool> Execute()
        {
            try
            {

                Response.Add($"Server Console Commands Available: ");
                foreach (var x in ConsoleCommand.RegisteredCommands)
                {
                    if (!x.Value.Hidden)
                    {
                        string z = "";
                        x.Value.RequiredArguments.ForEach(y => z += $" [({y.Type.Name}) {y.Name}{(y.Required ? "*" : "")}]");
                        Response.Add($"{x.Value.Name} - {x.Value.Description} \n ({x.Value.Name} Usage: \'{ConsoleCommand.Prefix}{x.Value.Name}{z})\'");
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Response.Add($"An error has occured while trying to execute the help command. Exception: {e}");
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
