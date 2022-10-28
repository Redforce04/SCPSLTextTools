using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Log
    {
        /// <summary>
        /// Log a warning message to the console.
        /// </summary>
        /// <param name="Message"></param>
        public static void Warn(string Message, string prefix = "[WARNING]") { Log.Raw(Message, prefix, ConsoleColor.DarkRed); }
        /// <summary>
        /// Log an Error message to the console.
        /// </summary>
        /// <param name="Message"></param>
        public static void Error(string Message, string prefix = "[ERROR]") { Log.Raw(Message, prefix, ConsoleColor.DarkRed); }
        /// <summary>
        /// Log a debug message to the console. If the bool CanSend is false, the message won't be sent.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="CanSend"></param>
        public static void Debug(string Message, bool CanSend = true, string prefix = "[Debug]") { if (CanSend) Log.Raw(Message, prefix, ConsoleColor.Green); }
        /// <summary>
        /// Log an info message to the console.
        /// </summary>
        /// <param name="Message"></param>
        public static void Info(string Message, string prefix = "[Info]") { Log.Raw(Message, prefix, ConsoleColor.Cyan); }
        /// <summary>
        /// Log a generic message, with the "[Message]" tag to the console.
        /// </summary>
        /// <param name="Message"></param>
        public static void Message(string Message, string prefix = "[Message]") { Log.Raw(Message, prefix, ConsoleColor.Magenta); }

        /// <summary>
        /// Log a generic message to the console.
        /// </summary>
        /// <param name="Message"></param>
        public static void Generic(string Message, string prefix = "[Generic]") { Log.Raw(Message, prefix, ConsoleColor.White); }
        /// <summary>
        /// Log a raw message to the console. Message is the message to log, InfoLevel is the info tag to prefix the message, and Color is the console color of the message.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="InfoLevel"></param>
        /// <param name="Color"></param>
        public static void Raw(string Message, string InfoLevel = "[INFO]", ConsoleColor Color = ConsoleColor.White)
        {
            foreach (string x in Message.Split($"\n"))
            {
                if (x == "") continue;
                Console.ForegroundColor = Color;
                Console.WriteLine(DateTime.Now.ToString("g") + $" ({DateTimeOffset.UtcNow.ToUnixTimeSeconds()}) {InfoLevel} " + x);
                WriteToLogFile(DateTime.Now.ToString("g") + $" ({DateTimeOffset.UtcNow.ToUnixTimeSeconds()}) {InfoLevel} " + x);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        internal static StringBuilder Logs = new StringBuilder();
        public static void WriteToLogFile(string x)
        {
            Logs.AppendLine(x);
        }
        public static void LoggingThread()
        {
            string ConfigLocation = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";

            while (true)
            {
                if (!initialized)
                    Thread.Sleep(1000);
                if (Logs.Length > 0)
                {
                    lock (Logs)
                    {
                        File.AppendAllText(ConfigLocation, Logs.ToString());
                        Logs.Clear();
                    }
                }
                Thread.Sleep(5000);
            }
        }
        internal static bool initialized = false;

        public static void Launch()
        {
            string[] header;
            if (Program.StartupProject == StartupArgs.ProjectGordon)
                header = new string[12]
                {
                    @"------------------------------------------------------------------------------------------------------------------------------------------------",
                    @" _____/\\\\\\\\\\\\_        _______________        _______________        _________/\\\__        _______________        _______________         ",
                    @"  ___/\\\//////////__        _______________        _______________        ________\/\\\__        _______________        _______________        ",
                    @"   __/\\\_____________        _______________        _______________        ________\/\\\__        _______________        _______________       ",
                    @"    _\/\\\____/\\\\\\\_        _____/\\\\\____        __/\\/\\\\\\\__        ________\/\\\__        _____/\\\\\____        __/\\/\\\\\\___      ",
                    @"     _\/\\\___\/////\\\_        ___/\\\///\\\__        _\/\\\/////\\\_        ___/\\\\\\\\\__        ___/\\\///\\\__        _\/\\\////\\\__     ",
                    @"      _\/\\\_______\/\\\_        __/\\\__\//\\\_        _\/\\\___\///__        __/\\\////\\\__        __/\\\__\//\\\_        _\/\\\__\//\\\_    ",
                    @"       _\/\\\_______\/\\\_        _\//\\\__/\\\__        _\/\\\_________        _\/\\\__\/\\\__        _\//\\\__/\\\__        _\/\\\___\/\\\_   ",
                    @"        _\//\\\\\\\\\\\\/__        __\///\\\\\/___        _\/\\\_________        _\//\\\\\\\/\\_        __\///\\\\\/___        _\/\\\___\/\\\_  ",
                    @"         __\////////////____        ____\/////_____        _\///__________        __\///////\//__        ____\/////_____        _\///____\///__ ",
                    @" Gordon (gorden) © - Developed by Redforce04#4091                                                                                               ",
                    @"------------------------------------------------------------------------------------------------------------------------------------------------"
                };
            else
                header = new string[14]
                {
                    @"---------------------------------------", 
                    @"=======================================",
                    @"=  ====  ==============================", 
                    @"=  ====  ==============================",
                    @"=  ====  ==============================", 
                    @"=  ====  ==  ===   ===  ===   ===  = ==",
                    @"=   ==   ======  =  ======     ==     =", 
                    @"==  ==  ===  ===  ====  ==  =  ==  =  =",
                    @"==  ==  ===  ====  ===  ==  =  ==  =  =", 
                    @"===    ====  ==  =  ==  ==  =  ==  =  =",
                    @"====  =====  ===   ===  ===   ===  =  =", 
                    @"=======================================",
                    @"Vision © - Developed by Redforce04#4091", 
                    @"---------------------------------------"
                };

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"\n");
            for (int i = 0; i < header.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("|");
                if (i == 0)
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                else if (i == header.Length - 2)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else if (i == header.Length - 1)
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                else
                    Console.ForegroundColor = ConsoleColor.Blue;


                Console.Write(header[i]); //Don't Log this - it will spam the logfile
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("|\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static async Task init()
        {
            Thread logger = new Thread(LoggingThread);
            logger.Start();
            string ConfigLocation = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";
            if (File.Exists(ConfigLocation))
                return;
            lock (Logs)
            {
                try
                {
                    Log.Info($"Log file not found. Creating log file: {ConfigLocation}"); // Logs first time generated.  " + $"\n" + @"
                                                                                          //string header = @"|---------------------------------------------------------------|" + $"\n" + @"|     _   __        __   ____          __                   __  |" + $"\n" + @"|    / | / /__  __ / /_ / __ ) ____   / /_    ____   ___   / /_ |" + $"\n" + @"|   /  |/ // / / // __// __  |/ __ \ / __/   / __ \ / _ \ / __/ |" + $"\n" + @"|  / /|  // /_/ // /_ / /_/ // /_/ // /_ _  / / / //  __// /_   |" + $"\n" + @"| /_/ |_/ \__,_/ \__//_____/ \____/ \__/(_)/_/ /_/ \___/ \__/   |" + $"\n" + @"|                                                               |" + $"\n" + @"|  NutBot.net © - Developed by Redforce04#4091 and zombie#4868  |" + $"\n" + @"|---------------------------------------------------------------|";
                    string[] header = new string[12] { @"------------------------------------------------------------------------------------------------------------------------------------------------", @" _____/\\\\\\\\\\\\_        _______________        _______________        _________/\\\__        _______________        _______________         ", @"  ___/\\\//////////__        _______________        _______________        ________\/\\\__        _______________        _______________        ", @"   __/\\\_____________        _______________        _______________        ________\/\\\__        _______________        _______________       ", @"    _\/\\\____/\\\\\\\_        _____/\\\\\____        __/\\/\\\\\\\__        ________\/\\\__        _____/\\\\\____        __/\\/\\\\\\___      ", @"     _\/\\\___\/////\\\_        ___/\\\///\\\__        _\/\\\/////\\\_        ___/\\\\\\\\\__        ___/\\\///\\\__        _\/\\\////\\\__     ", @"      _\/\\\_______\/\\\_        __/\\\__\//\\\_        _\/\\\___\///__        __/\\\////\\\__        __/\\\__\//\\\_        _\/\\\__\//\\\_    ", @"       _\/\\\_______\/\\\_        _\//\\\__/\\\__        _\/\\\_________        _\/\\\__\/\\\__        _\//\\\__/\\\__        _\/\\\___\/\\\_   ", @"        _\//\\\\\\\\\\\\/__        __\///\\\\\/___        _\/\\\_________        _\//\\\\\\\/\\_        __\///\\\\\/___        _\/\\\___\/\\\_  ", @"         __\////////////____        ____\/////_____        _\///__________        __\///////\//__        ____\/////_____        _\///____\///__ ", @" Gordon (gorden) © - Developed by Redforce04#4091                                                                                               ", @"------------------------------------------------------------------------------------------------------------------------------------------------" };

                    File.WriteAllText(ConfigLocation, "");
                    File.AppendAllLines(ConfigLocation, header);
                }
                catch (Exception e)
                {
                    Log.Error($"An error has occured in the logging system: {e}");
                }
            }
            initialized = true;
        }
    }

}
