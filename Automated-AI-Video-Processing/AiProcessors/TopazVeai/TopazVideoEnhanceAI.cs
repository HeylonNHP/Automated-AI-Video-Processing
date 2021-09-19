
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
            string[] arguments = new string[]
            {
                "-i",
                inputFilename,
                "-f",
                "mov_proreshq",
                "-s",
                "1",
                "-m",
                "amq-13",
                "-c",
                CudaDevice.ToString()
            };
            
            LaunchProcess process = new LaunchProcess(VeaiExeLocation, arguments);
            process.run();
        }
    }
}