using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    public class ServerConsole
    {
        public static void ConsoleCommandHandler()
        {
            Thread mainThread = new Thread(Program.Execute);
            mainThread.Start();

            Log.Info("Console Command Handler Started.");
            ServerConsole.LoadCommands();
            bool run = true;
            while (run)
            {
                string input = Console.ReadLine();
                ConsoleCommand cmd = new ConsoleCommand(input);
                input = "";
            }
        }

        public static void LoadCommands()
        {
            Log.Info("Loading Console Commands.");
            Log.Raw($"Console Command Prefix: {ConsoleCommand.Prefix}", "[Command]", ConsoleColor.Yellow);
            List<Command> cmds = LoadConsoleCommands();
            int commandsRegistered = 0;
            foreach (Command x in cmds)
            {
                if (x == null)
                {
                    Log.Error("Command is null.");
                    continue;
                }

                if (ConsoleCommand.RegisteredCommands.ContainsKey(x.Name.ToLower()))
                {
                    Log.Warn("Console Command \"" + x.Name.ToLower() + "\" Already Registered. This command will not be re-registered.");
                    continue;
                }

                bool ArgumentsInvalid = false;
                bool OptionalArgumentFound = false;
                foreach (var y in x.RequiredArguments)
                {
                    if (y.Required)
                        if (OptionalArgumentFound == true)
                        {
                            ArgumentsInvalid = true;
                            break;
                        }
                        else
                            continue;
                    else
                        OptionalArgumentFound = true;
                    continue;
                }
                if (ArgumentsInvalid)
                {
                    Log.Error($"Command {x.Name} has invalid required arguments. Optional requirements cannot be specified before required arguments. Command not registering.");
                    continue;

                }
                x.Register();
                ConsoleCommand.RegisteredCommands.Add(x.Name.ToLower(), x);
                commandsRegistered++;
            }

            Log.Debug("Registered " + commandsRegistered.ToString() + " total commands ");
        }

        public static List<Command> LoadConsoleCommands()
        {
            AppDomain app = AppDomain.CurrentDomain;
            Assembly[] ass = app.GetAssemblies();
            Type[] types;
            Type targetType = typeof(Command);
            List<Command> cmds = new List<Command>();
            foreach (Assembly a in ass)
            {
                try
                {
                    types = a.GetTypes();
                }
                catch (Exception e)
                {
                    var x = e;
                    continue;
                }

                foreach (Type t in types)
                {
                    if (t.IsInterface)
                        continue;
                    if (t.IsAbstract)
                        continue;
                    if (t.ContainsGenericParameters)
                        continue;
                    if (t.IsClass && t.IsSubclassOf(typeof(Command)))
                    {
                        cmds.Add((Command)Activator.CreateInstance(t));
                    }
                }
            }

            return cmds;
        }
    }
}
