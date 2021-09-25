using System;

namespace Automated_AI_Video_Processing.AiProcessors.RCG
{
    public class RifeColabGuiFinishedEventArgs : EventArgs
    {
        private string outputFilename;

        public RifeColabGuiFinishedEventArgs(string outputFilename = null)
        {
            this.outputFilename = outputFilename;
        }
    }
}