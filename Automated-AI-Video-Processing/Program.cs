using System;
using System.Threading;
using Automated_AI_Video_Processing.AiProcessors;
using Automated_AI_Video_Processing.BatchFolderActions.TopazVeai;

namespace Automated_AI_Video_Processing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            runFolder();
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
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