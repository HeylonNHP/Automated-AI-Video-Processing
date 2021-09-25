
using System;
using System.IO;
using System.Text.RegularExpressions;
using Automated_AI_Video_Processing.ProcessExecution;

namespace Automated_AI_Video_Processing.AiProcessors
{
    public struct SwapExtensionDetails
    {
        public string name;
        public bool swapped;
    }
    public class TopazVideoEnhanceAI
    {
        private string inputFilename;
        private TopazVeaiOutputFormats outputFormat;
        private TopazVeaiModels AiModel;
        private int CudaDevice;
        private TopazVeaiScalingDetails scalingDetails;

        public event EventHandler<TopazVeaiFinishedEventArgs> onTopazVeaiFinished; 

        public TopazVideoEnhanceAI(string inputFilename,
            TopazVeaiOutputFormats outputFormat,
            TopazVeaiModels aiModel, int cudaDevice,
            TopazVeaiScalingDetails scalingDetails)
        {
            this.inputFilename = inputFilename;
            this.outputFormat = outputFormat;
            this.AiModel = aiModel;
            this.CudaDevice = cudaDevice;
            this.scalingDetails = scalingDetails;
        }

        public void runAsync()
        {
            string modelName = getStringFromModelEnum(AiModel);

            string scalingArgs = "";
            switch (scalingDetails.scalingMode)
            {
                case TopazVeaiScalingMode.ScalingFactor:
                    scalingArgs = $"-s {scalingDetails.scalingFactor}";
                    break;
                case TopazVeaiScalingMode.TargetResolution:
                    scalingArgs = $"-d {scalingDetails.outputResolution.X}:{scalingDetails.outputResolution.Y}";
                    break;
            }

            SwapExtensionDetails nameSwapDetails = swapGifFileToAvi(inputFilename);
            inputFilename = nameSwapDetails.name;

            string args = String.Format("-i \"{0}\" -f {1} {2} -m {3} -c {4}",
                inputFilename,
                outputFormat.ToString(),
                scalingArgs,
                modelName,
                CudaDevice);
            
            LaunchProcess process = new LaunchProcess(ProgramFilePaths.TopazVeaiLocation, args);
            process.redirectStdOut = true;
            process.redirectStdErr = true;
            process.run(false);

            var stdOut = process.stdOut;

            process.processExited += (sender, eventArgs) =>
            {
                string markerText = "Saving: ";
                string? line = stdOut.ReadLine();
                while (line != null)
                {
                    if (line.Contains(markerText))
                    {
                        line = line.Replace(markerText, "");
                        onTopazVeaiFinished?.Invoke(this,new TopazVeaiFinishedEventArgs(line));
                        if (nameSwapDetails.swapped)
                        {
                            SwapExtensionDetails swapBackDetails = swapAviFileToGif(inputFilename);
                        }
                    }
                    line = stdOut.ReadLine();
                }
            };
        }

        private string getStringFromModelEnum(TopazVeaiModels model)
        {
            var regex = new Regex(@"([aA-zZ]+)([0-9]+)");
            var matches = regex.Matches(model.ToString());

            var match = matches[0];

            return match.Groups[1].Value + "-" + match.Groups[2].Value;
        }

        private SwapExtensionDetails swapAviFileToGif(string inputFilename)
        {
            if (getExtension(inputFilename).ToLower() == "avi")
            {
                
                return new SwapExtensionDetails(){name = setExtension(inputFilename,"gif"), swapped = true};
            }

            return new SwapExtensionDetails(){name = inputFilename,swapped = false};
        }

        private SwapExtensionDetails swapGifFileToAvi(string inputFilename)
        {
            if (getExtension(inputFilename).ToLower() == "gif")
            {
                return new SwapExtensionDetails(){name = setExtension(inputFilename,"avi"),swapped = true};
            }

            return new SwapExtensionDetails(){name = inputFilename,swapped = false};
        }

        private string getExtension(string inputFilename)
        {
            var extensionDotPos = inputFilename.LastIndexOf('.')+1;
            return inputFilename.Substring(extensionDotPos, inputFilename.Length - extensionDotPos);
        }

        private string setExtension(string inputFilename,string extension)
        {
            var extensionDotPos = inputFilename.LastIndexOf('.');
            string newName = inputFilename.Substring(0, extensionDotPos) + $".{extension}";
            File.Move(inputFilename,newName);
            return newName;
        }
    }
}