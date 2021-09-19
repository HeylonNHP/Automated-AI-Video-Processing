using System;

namespace Automated_AI_Video_Processing.AiProcessors
{
    public class TopazVeaiFinishedEventArgs: EventArgs
    {
        private string outputFilepathInternal;
        public TopazVeaiFinishedEventArgs(string outputFilepath) :base()
        {
            outputFilepathInternal = outputFilepath;
        }

        public string outputFilePath
        {
            get
            {
                return outputFilepathInternal;
            }
        }
    }
}