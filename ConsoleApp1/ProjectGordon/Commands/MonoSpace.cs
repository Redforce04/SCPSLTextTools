using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class SendPacket : Command
    {
        public override string Name => "MonoSpace";
        public override string Description => "Turn text into sl monospaced text";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string output = "";
                List<string> arguments = (List<string>) Arguments["Input"];
                foreach (var x in arguments)
                {
                    output += $"{x} ";
                }

                output = output.Trim();
                Log.Raw($"\n {StringRow.MonoSpace(output)}", $"[Output]", ConsoleColor.DarkMagenta);
                Response.Add($"Line Outputted");
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
                Name = "Input",
                Type = typeof(List<string>),
                Required = true,
                Remainder = true
            }
        };

    }

}
