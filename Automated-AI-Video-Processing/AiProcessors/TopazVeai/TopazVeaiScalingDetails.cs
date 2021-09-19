using System.Drawing;

namespace Automated_AI_Video_Processing.AiProcessors
{
    public class TopazVeaiScalingDetails
    {
        private TopazVeaiScalingMode scaleMode;
        private float scalingFactor = 1;
        private int outputWidth = 0;
        private int outputHeight = 0;

        public TopazVeaiScalingDetails(int outputWidth, int outputHeight)
        {
            scaleMode = TopazVeaiScalingMode.TargetResolution;
            this.outputWidth = outputWidth;
            this.outputHeight = outputHeight;
        }

        public TopazVeaiScalingDetails(float scalingFactor)
        {
            scaleMode = TopazVeaiScalingMode.ScalingFactor;
            this.scalingFactor = scalingFactor;
        }

        public TopazVeaiScalingMode scalingMode
        {
            get
            {
                return scaleMode;
            }
        }

        public float setScalingFactor
        {
            get
            {
                return scalingFactor;
            }
        }

        public Point outputResolution
        {
            get
            {
                return new Point(outputWidth, outputHeight);
            }
        }
    }
}