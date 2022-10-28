using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// The inherited abstract class used to create a command.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// A method that is fired when the command is registered. This method will still run if hidden is set to true.
        /// </summary>
        public abstract void Register();
        /// <summary>
        /// A method that is fired when someone uses the command in the console. This method will still run if hidden is set to true.
        /// </summary>
        /// <returns><seealso cref="bool">True</seealso> if the command was executed successfully. <seealso cref="bool">False</seealso> if the command was not executed successfully.</returns>
        public virtual async Task<bool> Execute()
        {
            return false;
        }
        /// <summary>
        /// A response or responses that can be set in <seealso cref="Execute"/>. If no response is set, then the console will just state the success or failure of the command. 
        /// The response is logged in the order of the responses set.
        /// </summary>
        public virtual List<string> Response { get; set; } = new List<string>();
        /// <summary>
        /// Arguments that are supplied to the command when <seealso cref="Execute"/> is fired.
        /// </summary>
        public Dictionary<object, object> Arguments = new Dictionary<object, object>();
        /// <summary>
        /// The name of the command. When the command is registered and executed, the name is stored and found in lowercase.
        /// </summary>
        public virtual string Name { get; }
        /// <summary>
        /// A description of the command.
        /// </summary>
        public virtual string Description { get; }
        /// <summary>
        /// Whether or not the command should be hidden from the <seealso cref="HelpCommand"/>. The command will still be registered and executed normally, even if set to <seealso cref="bool">True</seealso>.
        /// </summary>
        public virtual bool Hidden { get; } = false;
        /// <summary>
        /// A list of <seealso cref="CommandArgument">Arguments</seealso> that the command takes. These can be required or non required, but required arguments must be specified before optional arguments.
        /// </summary>
        public virtual List<CommandArgument> RequiredArguments { get; } = new List<CommandArgument>();
    }
    /// <summary>
    /// A command argument specified for execution of a <seealso cref="Command"/>. These are auto parsed on command execution and accessible to the execute method in <seealso cref="Command.Arguments"/>.
    /// </summary>
    public class CommandArgument
    {
        /// <summary>
        /// The <seealso cref="System.Type"/> of argument required for the command argument. If this argument is not a generic c# argument, it will be autoserialized through json (Make sure to use web safe characters.) Checkout: <seealso cref="Uri.EscapeDataString(string)"/> for more info.
        /// </summary>
        public Type Type { get; set; } = typeof(string);

        /// <summary>
        /// The name of the command.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether or not the command is required to execute. If the command is required to execute but is not supplied, the <seealso cref="Command.Execute"/> method is not triggered. 
        /// </summary>
        public bool Required { get; set; } = true;

        /// <summary>
        /// If set to true, all characters after this are inserted into a list of this argument's type, or serialized into this argument as a class. Must be the last requirement.
        /// </summary>
        public bool Remainder { get; set; } = false;
    }
}
