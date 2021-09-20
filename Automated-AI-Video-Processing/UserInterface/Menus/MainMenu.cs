using System;
using Automated_AI_Video_Processing.BatchFolderActions.TopazVeai;

namespace Automated_AI_Video_Processing.UserInterface.Menus
{
    public static class MainMenu
    {
        
        public static CliMenuItem[] mainMenuItems = new[]
        {
            new CliMenuItem("Run VEAI upscaler", BatchProcessFolderWithVeai)
        };

        public static void BatchProcessFolderWithVeai()
        {
            Console.WriteLine("Specify the folder path:");
            string path = CliMenu.getStringInput();
            Console.WriteLine("Target video height");
            int height = int.Parse(CliMenu.getStringInput(ProcessAllFilesInFolderTopazVeai.DESIRED_HEIGHT.ToString())); 
            ProcessAllFilesInFolderTopazVeai batchVeai = new ProcessAllFilesInFolderTopazVeai(path);
            batchVeai.runAsync(height);
            Console.WriteLine("Running Veai async...");
        }

        public static CliMenu mainMenu = new CliMenu("Main Menu", mainMenuItems);

    }
}