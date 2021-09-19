using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Automated_AI_Video_Processing.AiProcessors;

namespace Automated_AI_Video_Processing.BatchFolderActions.TopazVeai
{
    public class ProcessAllFilesInFolderTopazVeai
    {
        private bool moveNext = true;
        private string processingFolderPath;

        public ProcessAllFilesInFolderTopazVeai(string path)
        {
            processingFolderPath = path;
        }

        public void runAsync()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (sender, args) =>
            {
                var files = RecursiveFolderSearch.Search(processingFolderPath);

                foreach (var file in files)
                {
                    while (!moveNext)
                    {
                        Thread.Sleep(1000);
                    }

                    moveNext = false;
                    runVeai(file);
                }
            };
            backgroundWorker.RunWorkerCompleted += (sender, args) =>
            {
                Console.WriteLine("Finished processing folder");
            };
            backgroundWorker.RunWorkerAsync();
        }

        private void runVeai(string filepath)
        {
            var veaiInstance = new TopazVideoEnhanceAI(
                filepath, TopazVeaiOutputFormats.mov_proreshq,
                TopazVeaiModels.amq13,
                1, new TopazVeaiScalingDetails(1));

            veaiInstance.onTopazVeaiFinished += (sender, args) =>
            {
                moveNext = true;
                Console.WriteLine($"Processed: {args.outputFilePath}");
            };

            veaiInstance.runAsync();
        }
    }
}