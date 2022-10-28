using System;
using System.Collections.Generic;


namespace ConsoleApp1
{
    public class ConsoleCommand
    {
        public ConsoleCommand(string cmd)
        {
            try
            {
                TimeSent = DateTime.UtcNow;
                if (!cmd.StartsWith(ConsoleCommand.Prefix))
                    return;
                Command = cmd.Substring(1).Split(' ')[0];
                string argbase = "";
                if (!(cmd.Length < (Command.Length + ConsoleCommand.Prefix.Length + 1)))
                    argbase = cmd.Substring(Command.Length + ConsoleCommand.Prefix.Length + 1);
                else
                {
                    CommandProcessor.DoCommand(this);
                    return;
                }

                //if (cmd.Substring(Command.Length + ConsoleCommand.Prefix.Length + 1).Split(' ').Count() == 0)
                //{
                //    Arguments.Add("");
                //}
                //else
                //{
                string[] args = cmd.Substring(Command.Length + ConsoleCommand.Prefix.Length + 1).Split(' ');
                foreach (string arg in args)
                {
                    if (arg != "")
                        Arguments.Add(arg);
                }

                //}
                CommandProcessor.DoCommand(this);
            }
            catch (Exception e)
            {
                Log.Error("Error with console command handler. Exception: " + e);
            }
        }

        public static Dictionary<string, Command> RegisteredCommands = new Dictionary<string, Command>();
        public static string Prefix = "/";
        public string Command = "";
        public List<string> Arguments = new List<string>();
        public DateTime TimeSent = DateTime.UtcNow;
    }
}
