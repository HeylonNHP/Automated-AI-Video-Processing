
namespace Automated_AI_Video_Processing.AiProcessors
{
    public class TopazVideoEnhanceAI
    {
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
    }
}