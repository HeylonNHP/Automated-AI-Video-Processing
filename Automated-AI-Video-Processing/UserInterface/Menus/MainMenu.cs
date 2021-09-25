using System;
using Automated_AI_Video_Processing.AiProcessors.RCG;
using Automated_AI_Video_Processing.BatchFolderActions.TopazVeai;

namespace Automated_AI_Video_Processing.UserInterface.Menus
{
    public static class MainMenu
    {
        
        public static CliMenuItem[] mainMenuItems = new[]
        {
            new CliMenuItem("Run VEAI upscaler", BatchProcessFolderWithVeai),
            new CliMenuItem("Program settings", () => SettingsMenu.menu.DisplayMenu())
        };

        public static void BatchProcessFolderWithVeai()
        {
            Console.WriteLine("Specify the folder path:");
            string path = CliMenu.getStringInput();
            Console.WriteLine("Target video height");
            int height = int.Parse(CliMenu.getStringInput(ProcessAllFilesInFolderTopazVeai.DESIRED_HEIGHT.ToString()));
            int cudaDevice = int.Parse(CliMenu.getStringInput("0"));

            RifeColabGuiSettings rifeSettings = null;
            {
                Console.WriteLine("Use Rife?:");
                bool useRife = CliMenu.getStringInput("false").ToLower() == "true";
                if (useRife)
                {
                    rifeSettings = RifeSettings();
                }
            }
            
            ProcessAllFilesInFolderTopazVeai batchVeai = new ProcessAllFilesInFolderTopazVeai(path, cudaDevice, rifeSettings != null? new RifeColabGuiAI(rifeSettings):null);
            batchVeai.runAsync(height);
            Console.WriteLine("Running Veai async...");
        }

        public static RifeColabGuiSettings RifeSettings()
        {
            Console.WriteLine("Rife-Colab-Gui settings:");
            Console.WriteLine("Loop?:");
            bool loop = CliMenu.getStringInput("false").ToLower() == "false"? false:true;
            Console.WriteLine("GPU IDs (delimiter: ,):");
            int[] gpuIds = new Func<int[]>(() =>
            {
                string input = CliMenu.getStringInput("0");
                string[] inputs = input.Split(",");
                int[] intInputs = new int[inputs.Length];
                for (int i = 0; i < inputs.Length; ++i)
                {
                    intInputs[i] = int.Parse(inputs[i]);
                }

                return intInputs;
            })();
            RifeColabGuiSettings settings = new RifeColabGuiSettings(null,
                new InterpolationFactorOptions() { mode = 3, outputFPS = 60 }, loop,gpuIds);
            return settings;
        }

        public static CliMenu mainMenu = new CliMenu("Main Menu", mainMenuItems);

    }
}