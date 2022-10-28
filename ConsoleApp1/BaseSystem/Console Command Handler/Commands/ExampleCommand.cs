using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ExampleCommand : Command
    {
        public override string Name => "Example";
        public override string Description => "An example command that can be copied and modified. This command is hidden, but can still be executed.";
        public override bool Hidden => true;
        public override async Task<bool> Execute()
        {
            try
            {
                // No need to use logging commands unless you are debugging or catching minor exceptions deep into a method. The response will log things after the command executes in order of the responses added.
                Log.Debug("This is an example command.");


                // This is how you get a required argument out of the response.
                bool success = (bool)Arguments[0];
                // Additionally you can use the argument name. Note that this is case sensitive.
                success = (bool)Arguments["Success"];

                if (Arguments["Json Test"] != null)
                {
                    JsonTesting test = (JsonTesting)Arguments["Json Test"];
                    Log.Debug($"{test.Success}, {test.Test}");
                }



                // Another way to use optional arguments with a default argument.
                ulong FavoriteNumber = (Arguments["Favorite Number"] != null) ? (ulong)Arguments["Favorite Number"] : 69;
                Response.Add($"My favorite number is {FavoriteNumber}.");

                // Add a response to the command before you return.
                Response.Add($"Example command succesfully executed.");

                // Return true for success.
                return success;
            }
            //A try/catch is helpful to have, but everything will be caught by the executor regardless. This helps you localize the problem however.
            catch (Exception e)
            {
                //Add your response.
                Response.Add($"An error has occured while trying to execute the example command. Exception: {e}");

                // Return false for failure.
                return false;
            }
        }
        public override void Register()
        {
            // Executed on the command registration.
            return;
        }
        public override List<CommandArgument> RequiredArguments { get; } = new List<CommandArgument>()
        {
            // Required arguments first.
            new CommandArgument()
            {
                Name = "Success",
                Type = typeof(bool),
                Required = true
            },
            new CommandArgument()
            {
                Name = "test",
                Type = typeof(string),
                Required = true
            },
             //Classes can be autoserialized from json. if a class is the remainder, you can make it a List<> or json (with spaces instead of %20)
            new CommandArgument()
            {
                Name = "Json Test",
                Type = typeof(JsonTesting),
                Required = false,
                Remainder = true
            }
        };
    }
    public class JsonTesting
    {
        public string Test { get; set; } = "";
        public bool Success { get; set; } = false;
    }
}
