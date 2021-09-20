using System;

namespace Automated_AI_Video_Processing.UserInterface.Menus
{
    public static class MainMenu
    {
        
        public static CliMenuItem[] mainMenuItems = new[]
        {
            new CliMenuItem("Run VEAI upscaler", () => { Console.WriteLine("Fuck"); })
        };

        public static CliMenu mainMenu = new CliMenu("Main Menu", mainMenuItems);

    }
}