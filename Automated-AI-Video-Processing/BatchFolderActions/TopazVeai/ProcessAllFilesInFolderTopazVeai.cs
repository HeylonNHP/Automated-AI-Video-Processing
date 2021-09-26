using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Automated_AI_Video_Processing.AiProcessors;
using Automated_AI_Video_Processing.AiProcessors.RCG;
using Automated_AI_Video_Processing.GeneralFunctions;

namespace Automated_AI_Video_Processing.BatchFolderActions.TopazVeai
{
    public class ProcessAllFilesInFolderTopazVeai
    {
        public const int DESIRED_HEIGHT = 1080;
        private bool moveNext = true;
        private string processingFolderPath;
        private int cudaDevice;
        private RifeColabGuiAiQueuedProcessing _rifeColabGuiAi;
        private RifeColabGuiSettings _rifeColabGuiSettings;

        public ProcessAllFilesInFolderTopazVeai(string path, int cudaDevice = 0, RifeColabGuiSettings rifeColabGuiAiSettings = null)
        {
            processingFolderPath = path;
            this.cudaDevice = cudaDevice;
            this._rifeColabGuiSettings = rifeColabGuiAiSettings;
            if (rifeColabGuiAiSettings != null)
            {
            _rifeColabGuiAi = new RifeColabGuiAiQueuedProcessing(new RifeColabGuiAI(_rifeColabGuiSettings));
            }
        }

        public void runAsync(int desiredHeight = DESIRED_HEIGHT)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (sender, args) =>
            {
                _rifeColabGuiAi?.startQueue();
                var files = RecursiveFolderSearch.Search(processingFolderPath);

                foreach (var file in files)
                {
                    while (!moveNext)
                    {
                        Thread.Sleep(1000);
                    }

                    moveNext = false;
                    runVeai(file, desiredHeight);
                }
            };
            backgroundWorker.RunWorkerCompleted += (sender, args) =>
            {
                Console.WriteLine("Finished processing folder");
                _rifeColabGuiAi?.allowQueueToCompleteThenStop();
            };
            backgroundWorker.RunWorkerAsync();
        }

        private void runVeai(string filepath, int desiredHeight)
        {
            // Get res
            var scaleToVideoRes = FFmpegFunctions.GeneralFFmpegFunctions.getResolution(filepath);
            if (DESIRED_HEIGHT > scaleToVideoRes.Y)
            {
                scaleToVideoRes = VideoCalculations.scaleToDesiredResHeightMOD2(scaleToVideoRes, desiredHeight);
            }

            var veaiInstance = new TopazVideoEnhanceAI(
                filepath, TopazVeaiOutputFormats.mov_proreshq,
                TopazVeaiModels.amq13,
                this.cudaDevice, new TopazVeaiScalingDetails(scaleToVideoRes.X, scaleToVideoRes.Y));

            veaiInstance.onTopazVeaiFinished += (sender, args) =>
            {
                if (_rifeColabGuiAi != null)
                {
                    RifeColabGuiSettings newSettings = _rifeColabGuiSettings.Clone();
                    newSettings.InputFile = args.outputFilePath;
                    _rifeColabGuiAi?.QueuedInterpolations.Enqueue(newSettings);
                }
                moveNext = true;
                Console.WriteLine($"Processed: {args.outputFilePath}");
            };

            veaiInstance.runAsync();
        }
    }
}