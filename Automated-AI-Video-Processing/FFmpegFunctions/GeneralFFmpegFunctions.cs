using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Automated_AI_Video_Processing.ProcessExecution;
using Microsoft.VisualBasic.CompilerServices;

namespace Automated_AI_Video_Processing.FFmpegFunctions
{
    public static class GeneralFFmpegFunctions
    {
        public static Point getResolution(string path)
        {
            Point output = new Point();
            string args = $"-i {path}";
            LaunchProcess process = new LaunchProcess(ProgramFilePaths.FFmpeg, args);

            process.redirectStdErr = true;
            process.run();
            StreamReader stdOut = process.stdErr;


            string line = stdOut.ReadLine();
            while (line != null)
            {
                if (line.Contains("Stream") && line.Contains("Video:"))
                {
                    var regex = new Regex(@"([0-9]+)x([0-9]+)");
                    var matches = regex.Matches(line);
                    var match = matches[0];
                    var width = int.Parse(match.Groups[1].Value);
                    var height = int.Parse(match.Groups[2].Value);
                    output.X = width;
                    output.Y = height;
                    return output;
                }
                line = stdOut.ReadLine();
            }

            throw new Exception("Couldn't find resolution");
        }
    }
}