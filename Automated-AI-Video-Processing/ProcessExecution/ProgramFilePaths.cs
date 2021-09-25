using System;
using System.IO;

namespace Automated_AI_Video_Processing.ProcessExecution
{
    public static class ProgramFilePaths
    {
        public static string TopazVeaiLocation = @"C:\Program Files\Topaz Labs LLC\Topaz Video Enhance AI\veai.exe";

        public static string FFmpeg = "ffmpeg";

        public static string RifeColabGuiSingleAllSteps = new Func<string>(() =>
        {
            try
            {
                return getRifeColabGuiSingleAllSteps(@"C:\Users\Heylon\Documents\RIFEstuff\RIFE-Colab");
            }
            catch (Exception ex)
            {
                return "";
            }
        })();

        private static string getRifeColabGuiSingleAllSteps(string rcgPath)
        {
            string pyFile = "runInterpolationAllSteps.py";
            string fullPath = $"{rcgPath}\\{pyFile}";
            return File.Exists(fullPath) ? fullPath : throw new Exception("Could not find Rife Interpolation script");
        }

        public static void setRifeColabGuiSingleAllSteps(string rcgPath)
        {
            RifeColabGuiSingleAllSteps = getRifeColabGuiSingleAllSteps(rcgPath);
        }
    }
}