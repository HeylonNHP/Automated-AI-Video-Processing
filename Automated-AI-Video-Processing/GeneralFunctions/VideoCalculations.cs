using System;
using System.Drawing;

namespace Automated_AI_Video_Processing.GeneralFunctions
{
    public static class VideoCalculations
    {
        public static Point scaleToDesiredResHeightMOD2(Point resolution, int desiredHeight)
        {
            Func<int,bool> isMod2 = (int dimension) =>
            {
                return dimension % 2 == 0;
            };

            if (!isMod2(desiredHeight))
            {
                throw new Exception("Height not MOD2");
            }
            
            float scaleFactor = (float)desiredHeight / resolution.Y;

            int outWidth = (int)Math.Floor(resolution.X * scaleFactor);

            if (isMod2(outWidth))
            {
                return new Point(desiredHeight, outWidth);
            }

            outWidth = (int)Math.Ceiling(resolution.X * scaleFactor);

            if (isMod2(outWidth))
            {
                return new Point(desiredHeight, outWidth);
            }

            return new Point(desiredHeight, outWidth + 1);
        }
    }
}