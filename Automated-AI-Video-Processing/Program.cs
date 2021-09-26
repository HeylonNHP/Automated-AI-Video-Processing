using System;
using System.Threading;
using Automated_AI_Video_Processing.AiProcessors;
using Automated_AI_Video_Processing.AiProcessors.RCG;
using Automated_AI_Video_Processing.BatchFolderActions.TopazVeai;
using Automated_AI_Video_Processing.FFmpegFunctions;
using Automated_AI_Video_Processing.ProcessExecution;
using Automated_AI_Video_Processing.UserInterface.Menus;

namespace Automated_AI_Video_Processing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MainMenu.mainMenu.DisplayMenu();
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void testFFmpeg()
        {
            Console.WriteLine(GeneralFFmpegFunctions.getResolution(@"N:\stuph\veai5\25\Ind.webm"));
        }

        private static void testVeai()
        {
            TopazVideoEnhanceAI ai = new TopazVideoEnhanceAI(
                "N:\\stuph\\veai5\\26\\Wild.webm",
                TopazVeaiOutputFormats.mov_proreshq,
                TopazVeaiModels.amq13,
                1,
                new TopazVeaiScalingDetails(1));

            ai.onTopazVeaiFinished += (sender, eventArgs) =>
            {
                Console.WriteLine($"Process has exited - Output: {eventArgs.outputFilePath}");
            };

            ai.runAsync();
            
            Console.WriteLine("Waiting");
        }

        private static void runFolder()
        {
            ProcessAllFilesInFolderTopazVeai folderVeai = new ProcessAllFilesInFolderTopazVeai(@"N:\stuph\veai5\25");
            folderVeai.runAsync();
        }
    }
}