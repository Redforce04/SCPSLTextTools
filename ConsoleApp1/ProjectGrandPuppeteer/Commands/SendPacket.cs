using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ProjectGrandPuppeteer.Commands
{
    public class SendPacket : Command
    {
        public override string Name => "SendPacket";
        public override string Description => "Turn text into sl monospaced text";
        public override bool Hidden => false;
        public override async Task<bool> Execute()
        {
            try
            {
                string output = "";
                string args = "";
                if (Arguments["Arguments"] is not null)
                {

                    List<string> arguments = (List<string>)Arguments["Arguments"];
                    foreach (var x in arguments)
                    {
                        args += $"{x} ";
                    }
                }

                args = output.Trim();
                
                output = (string)Arguments["Input"];
                /*List<object> args = new List<object>()
                {
                };
                if (Arguments["Arguments"] is not null)
                {

                    foreach (var x in (List<string>)Arguments["Arguments"])
                    {
                        if (float.TryParse(x, out float floatVal))
                        {
                            args.Add(floatVal);
                            continue;
                        }

                        if (int.TryParse(x, out int intVal))
                        {
                            args.Add(intVal);
                            continue;
                        }

                        args.Add(x);


                    }
                }*/

                API.Api.SendOSC(output, args);
                Response.Add($"OSC Sent");
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
                Type = typeof(string),
                Required = true
            },
            new CommandArgument()
            {
                Name = "Arguments",
                Type = typeof(List<string>),
                Required = false,
                Remainder = true
            }
        };

    }

}
