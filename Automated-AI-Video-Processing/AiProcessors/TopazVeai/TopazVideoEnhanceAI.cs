
using System;
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
            string args = String.Format("-i \"{0}\" -f {1} -s {2} -m {3} -c {4}",
                inputFilename,
                "mov_proreshq",
                1,
                "amq-13",
                CudaDevice);
            
            LaunchProcess process = new LaunchProcess(VeaiExeLocation, args);
            process.run(false);
        }
    }
}