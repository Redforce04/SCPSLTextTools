using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace ConsoleApp1
{
    class CommandProcessor
    {
        public static async void DoCommand(ConsoleCommand cmd)
        {
            bool failed = false;
            string arguments = "";
            Command command;
            foreach (var x in cmd.Arguments)
            {
                arguments = arguments + " " + x.ToString() + ",";
            }

            List<string> errors = new List<string>();
            if (ConsoleCommand.RegisteredCommands.ContainsKey(cmd.Command.ToLower()))
            {
                command = ConsoleCommand.RegisteredCommands[cmd.Command.ToLower()];
                Dictionary<object, object> args = new Dictionary<object, object>();
                int count = 0;
                command.RequiredArguments.ForEach(x =>
                {
                    if (x.Required)
                    {
                        count++;
                    }
                });
                int i = -1;
                foreach (var CmdReqArg in command.RequiredArguments)
                {
                    i++;
                    bool islist = false;
                    Type baseType = typeof(string);
                    try
                    {

                        islist = CmdReqArg.Type.IsGenericType &&
                                    CmdReqArg.Type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
                        if (islist)
                            baseType = CmdReqArg.Type.GetGenericArguments()[0];
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Issue checking if list: {e}.");
                    }

                    // REQUIRED ARGUMENTS
                    if (CmdReqArg.Required)
                    {
                        // CHECK IF ARGUMENTS ARE PRESENT
                        if ((cmd.Arguments.Count < (i + 1)))
                        {
                            errors.Add($"Required Argument {i + 1} {CmdReqArg.Name} ({CmdReqArg.Type.Name}) is not found.");
                            failed = true;
                            continue;
                        }

                        // CHECK IF BOOLEAN AND PARSE
                        if (CmdReqArg.Type == typeof(bool) || CmdReqArg.Type == typeof(List<bool>))
                        {
                            if (CmdReqArg.Remainder)
                            {
                                bool failure = false;
                                List<bool> list = new List<bool>();
                                for (int ii = i; ii < cmd.Arguments.Count - 1; ii++)
                                {
                                    var xx = ParseBool(Uri.UnescapeDataString(cmd.Arguments[ii]));
                                    if (!xx.HasValue)
                                    {
                                        errors.Add($"Required Argument Remainder {ii} {CmdReqArg.Name} (remainder), is not a valid boolean.");
                                        failure = true;
                                        break;
                                    }
                                    list.Add(xx.Value);
                                }
                                if (failure)
                                {
                                    failed = true;
                                    continue;
                                }
                                args.Add((string)CmdReqArg.Name, list);
                                args.Add((int)i, list);
                                continue;
                            }
                            var x = ParseBool(Uri.UnescapeDataString(cmd.Arguments[i]));
                            if (!x.HasValue)
                            {
                                errors.Add($"Required Argument {i + 1} {CmdReqArg.Name} is not a valid boolean.");
                                failed = true;
                                continue;
                            }
                            args.Add((string)CmdReqArg.Name, x);
                            args.Add((int)i, x);
                            continue;
                        }

                        // TRY TO PARSE NORMALLY
                        object zz;
                        try
                        {
                            if (CmdReqArg.Remainder)
                            {
                                string jsonadded = "";
                                bool failure = false;

                                Type GenericListType = typeof(List<>).MakeGenericType(baseType);
                                var list = (System.Collections.IList)Activator.CreateInstance(GenericListType);
                                for (int ii = i; ii <= cmd.Arguments.Count - 1; ii++)
                                {
                                    object y = Convert.ChangeType(Uri.UnescapeDataString(cmd.Arguments[ii]), baseType);
                                    list.Add(y);
                                }
                                if (failure)
                                {
                                    failed = true;
                                    continue;
                                }
                                if (!islist)
                                {
                                    foreach (var jj in list)
                                    {
                                        jsonadded += $" {jj}";
                                    }
                                    try
                                    {
                                        string json = Uri.UnescapeDataString(jsonadded);
                                        zz = typeof(JsonConvert).GetMethod("DeserializeObject", 1, new Type[] { typeof(string) }).MakeGenericMethod(CmdReqArg.Type).Invoke(null, new object[] { json });
                                    }
                                    catch (Exception f)
                                    {
                                        errors.Add($"Required Argument {i + 1} {CmdReqArg.Name} is not a valid {CmdReqArg.Type.Name}.");
                                        failed = true;
                                        continue;
                                    }
                                }
                                else
                                    zz = list;
                            }
                            else
                            {
                                zz = Convert.ChangeType(Uri.UnescapeDataString(cmd.Arguments[i]), CmdReqArg.Type);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Debug($"An Exception has occured: {e}");
                            try
                            {
                                string json = Uri.UnescapeDataString(Uri.UnescapeDataString(cmd.Arguments[i]));
                                zz = typeof(JsonConvert).GetMethod("DeserializeObject", 1, new Type[] { typeof(string) }).MakeGenericMethod(CmdReqArg.Type).Invoke(null, new object[] { json });
                            }
                            catch (Exception f)
                            {
                                errors.Add($"Required Argument {i + 1} {CmdReqArg.Name} is not a valid {CmdReqArg.Type.Name}.");
                                failed = true;
                                continue;
                            }
                        }

                        args.Add((string)CmdReqArg.Name, zz);
                        args.Add((int)i, zz);
                        continue;
                    }

                    // OPTIONAL ARGUMENTS
                    if (!(CmdReqArg.Required) && ((cmd.Arguments.Count) < i + 1))
                    {
                        args.Add((string)CmdReqArg.Name, null);
                        args.Add((int)i, null);
                        continue;
                    }

                    if (CmdReqArg.Type == typeof(bool) || CmdReqArg.Type == typeof(List<bool>))
                    {
                        if (CmdReqArg.Remainder)
                        {
                            bool failure = false;
                            List<bool> list = new List<bool>();
                            for (int ii = i; ii < cmd.Arguments.Count - 1; ii++)
                            {
                                var xx = ParseBool(Uri.UnescapeDataString(cmd.Arguments[ii]));
                                if (!xx.HasValue)
                                {
                                    errors.Add($"Optional Argument Remainder {ii} {CmdReqArg.Name} (remainder), is not a valid boolean.");
                                    failure = true;
                                    break;
                                }
                                list.Add(xx.Value);
                            }
                            if (failure)
                            {
                                failed = true;
                                continue;
                            }
                            args.Add((string)CmdReqArg.Name, list);
                            args.Add((int)i, list);
                            continue;
                        }
                        var x = ParseBool(Uri.UnescapeDataString(cmd.Arguments[i]));
                        if (!x.HasValue)
                        {
                            errors.Add($"Optional Argument {i + 1} {CmdReqArg.Name} is not a valid boolean.");
                            failed = true;
                            continue;
                        }
                        args.Add((string)CmdReqArg.Name, x);
                        args.Add((int)i, x);
                        continue;
                    }

                    object z;
                    try
                    {
                        if (CmdReqArg.Remainder)
                        {
                            string jsonadded = "";
                            bool failure = false;

                            Type GenericListType = typeof(List<>).MakeGenericType(baseType);
                            var list = (System.Collections.IList)Activator.CreateInstance(GenericListType);
                            for (int ii = i; ii <= cmd.Arguments.Count - 1; ii++)
                            {
                                object y = Convert.ChangeType(Uri.UnescapeDataString(cmd.Arguments[ii]), baseType);
                                list.Add(y);
                            }
                            if (failure)
                            {
                                failed = true;
                                continue;
                            }
                            if (!islist)
                            {
                                foreach (var jj in list)
                                {
                                    jsonadded += $" {jj}";
                                }
                                try
                                {
                                    string json = Uri.UnescapeDataString(jsonadded);
                                    z = typeof(JsonConvert).GetMethod("DeserializeObject", 1, new Type[] { typeof(string) }).MakeGenericMethod(CmdReqArg.Type).Invoke(null, new object[] { json });
                                }
                                catch (Exception f)
                                {
                                    errors.Add($"Optional Argument {i + 1} {CmdReqArg.Name} is not a valid {CmdReqArg.Type.Name}.");
                                    failed = true;
                                    continue;
                                }
                            }
                            else
                                z = list;
                        }
                        else
                        {
                            z = Convert.ChangeType(Uri.UnescapeDataString(cmd.Arguments[i]), CmdReqArg.Type);
                        }
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            string json = Uri.UnescapeDataString(cmd.Arguments[i]);
                            z = typeof(JsonConvert).GetMethod("DeserializeObject", 1, new Type[] { typeof(string) }).MakeGenericMethod(CmdReqArg.Type).Invoke(null, new object[] { json });
                        }
                        catch (Exception f)
                        {
                            errors.Add($"Optional Argument {i + 1} {CmdReqArg.Name} is not a valid {CmdReqArg.Type.Name}.");
                            failed = true;
                            continue;
                        }
                        failed = true;
                        continue;
                    }

                    args.Add((string)CmdReqArg.Name, z);
                    args.Add((int)i, z);
                }

                // Did argument checker fail?
                if (failed)
                {
                    string errorlist = "";
                    errors.ForEach(x => errorlist += "\n" + x.ToString());
                    string arg = "";
                    command.RequiredArguments.ForEach(y => arg += $" [({y.Type.Name}) {y.Name}{(y.Required ? "*" : "")}]");
                    errorlist += $"\nUsage: {ConsoleCommand.Prefix}{command.Name} {arg}";
                    Log.Raw($"Command {command.Name} could not be executed due to an error with the command arguments. {errorlist}", "[Command] [Error]", ConsoleColor.Yellow);
                    return;
                }
                command.Arguments = args;
                bool success = true;
                command.Response = new List<string>();
                try
                {
                    success = await command.Execute();
                }
                catch (Exception e)
                {
                    Log.Raw($"Command {command.Name} has failed to execute. \"{e}\" Location: {e.Source}", "[Command] [Error]", ConsoleColor.Yellow);
                }

                string response = "";
                command.Response.ForEach(x => response += (response == "") ? $"{x}" : $"\n{x}");
                try
                {
                    if (!success)
                    {
                        Log.Raw($"Command {command.Name} has failed to execute.", "[Command] [Error]", ConsoleColor.Yellow);
                        Log.Raw($"{response}", "[Command] [Response]", ConsoleColor.Yellow);
                        return;
                    }
                    else
                    {
                        Log.Raw($"Command {command.Name} has executed successfully.", "[Command]", ConsoleColor.Yellow);
                        Log.Raw($"{response}", "[Command] [Response]", ConsoleColor.Yellow);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Log.Raw($"An exception has occured while trying to execute command {command.Name}. Exception: {e}", "[Command] [Error]", ConsoleColor.Red);
                    return;
                }
            }
            else
            {
                Log.Raw($"Command \"{cmd.Command}\" does not exist.", "[Command] [Error]", ConsoleColor.Yellow);
                return;
            }
        }

        public static Nullable<bool> ParseBool(string value)
        {
            switch (value.ToLower())
            {
                default:
                    return null;
                case "true":
                    return true;
                case "false":
                    return false;
                case "1":
                    return true;
                case "0":
                    return false;
            }
        }
    }
}



