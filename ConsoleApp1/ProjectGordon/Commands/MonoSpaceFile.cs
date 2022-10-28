using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGordon.Commands
{
    public class MonoSpaceFile : Command
    {
        public override string Name => "MonoSpaceFile";
        public override string Description => "Turn text file into sl monospaced text";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string directory = "";
                List<string> arguments = (List<string>) Arguments["FileDirectory"];
                foreach (var x in arguments)
                {
                    directory += $"{x} ";
                }

                if (!File.Exists(directory))
                {
                    Response.Add($"File \'{directory}\' does not exist.");
                    return false;
                }

                
                string output = "";
                List<string> outputStrings = new List<string>();
                var z = await File.ReadAllTextAsync(directory);
                foreach (var x in z)
                {
                    output += $"{x} ";
                }
                output = output.Trim();
                outputStrings = output.Split("\n").ToList();
                string newOutput = "";
                foreach (var y in outputStrings)
                {
                    newOutput += $"{StringRow.MonoSpace(y)}\n";
                }

                await File.WriteAllTextAsync(ProjectVision.API.Api.TempDirectory + "formatted.txt", newOutput);
                //Log.Raw($"\n {newOutput}", $"[Output]", ConsoleColor.DarkMagenta);
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
                Name = "FileDirectory",
                Type = typeof(List<string>),
                Required = true,
                Remainder = true
            }
        };

    }

}
