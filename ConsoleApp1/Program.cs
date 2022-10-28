namespace ConsoleApp1
{
    public class Program
    {
        public static StartupArgs StartupProject = StartupArgs.ProjectVision;
        private static bool _startGordon = true;
        private static bool _startVision = true;
        private static bool _startMiro = true;
        private static bool _startGrandPuppeteer = true;

        public static void Main(string[] args)
        {
            Log.Launch();
            /*switch ((int) StartupProject)
            {
                case 0:
                {
                    ProjectGordon.API.Enable();
                    Log.Info("Started Gordon");
                    break;
                }
                case 1:
                {
                    ProjectVision.API.Enable();
                    Log.Info($"Started Vision");
                    break;
                }
            }*/
            if (_startGordon)
            {
                ProjectGordon.API.Enable();
                Log.Info("Started Gordon");
            }

            if (_startVision)
            {
                ProjectVision.API.Enable();
                Log.Info($"Started Vision");
            }

            if (_startGrandPuppeteer)
            {
                ProjectGrandPuppeteer.API.Enable();
                Log.Info($"Started Grand Puppeteer");
            }

            if (_startMiro)
            {
                ProjectMiro.API.Enable();
                Log.Info($"Started Miro");
            }
            ServerConsole.ConsoleCommandHandler();

        }

        public static void Execute()
        {
            Log.Debug($"Started Program");
        }
    }
}