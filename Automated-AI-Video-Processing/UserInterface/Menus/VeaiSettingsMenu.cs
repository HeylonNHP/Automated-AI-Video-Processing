using System;
using Automated_AI_Video_Processing.ProcessExecution;

namespace Automated_AI_Video_Processing.UserInterface.Menus
{
    public class VeaiSettingsMenu
    {
        public static CliMenuItem[] menuItems = new[]
        {
            new CliMenuItem("VEAI Path", changeVeaiPath),
        };

        private static void changeVeaiPath()
        {
            Console.WriteLine("Paste VEAI Path:");
            string path = CliMenu.getStringInput(ProgramFilePaths.TopazVeaiLocation);
            ProgramFilePaths.TopazVeaiLocation = path;
        }
        
        public static CliMenu menu = new CliMenu("VEAI Settings Menu", menuItems);

    }
}