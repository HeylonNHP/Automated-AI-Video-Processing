using System;
using Automated_AI_Video_Processing.ProcessExecution;

namespace Automated_AI_Video_Processing.UserInterface.Menus
{
    public class RifeSettingsMenu
    {
        public static CliMenuItem[] menuItems = new[]
        {
            new CliMenuItem("Rife-Colab-Gui Path", changeVeaiPath),
        };

        private static void changeVeaiPath()
        {
            Console.WriteLine("Paste Rife-Colab-Gui Path:");
            string path = CliMenu.getStringInput(ProgramFilePaths.RifeColabGuiFolder);
            ProgramFilePaths.setRifeColabGuiSingleAllSteps(path);
        }
        
        public static CliMenu menu = new CliMenu("Rife Settings Menu", menuItems);

    }
}