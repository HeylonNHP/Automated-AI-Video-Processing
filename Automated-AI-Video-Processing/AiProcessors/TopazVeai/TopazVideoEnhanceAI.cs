
using System;
using System.Text.RegularExpressions;
using Automated_AI_Video_Processing.ProcessExecution;

namespace Automated_AI_Video_Processing.AiProcessors
{
    public class TopazVideoEnhanceAI
    {
        private static string VeaiExeLocation = "C:\\Program Files\\Topaz Labs LLC\\Topaz Video Enhance AI\\veai.exe";
        
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

            string args = String.Format("-i \"{0}\" -f {1} -s {2} -m {3} -c {4}",
                inputFilename,
                "mov_proreshq",
                1,
                modelName,
                CudaDevice);
            
            LaunchProcess process = new LaunchProcess(VeaiExeLocation, args);
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
    }
}