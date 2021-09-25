using System;
using System.IO;
using Automated_AI_Video_Processing.ProcessExecution;

namespace Automated_AI_Video_Processing.AiProcessors.RCG
{
    public class RifeColabGuiAI
    {
        public event EventHandler<RifeColabGuiFinishedEventArgs> rifeFinished; 
        private RifeColabGuiSettings settings;

        public RifeColabGuiAI(RifeColabGuiSettings settings)
        {
            this.settings = settings;
        }

        public void runInteroplationSingleFile(bool async = false)
        {
            LaunchProcess process = new LaunchProcess("python",
                $"{ProgramFilePaths.RifeColabGuiSingleAllSteps} {settings.ToString()}");
            process.getStartInfo.WorkingDirectory = ProgramFilePaths.RifeColabGuiFolder;
            process.redirectStdOut = true;
            process.run(!async);

            StreamReader stdOut = process.stdOut;

            Action EndOfExecutionProcessing = () =>
            {
                string line = stdOut.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = stdOut.ReadLine();
                }
                rifeFinished?.Invoke(this, new RifeColabGuiFinishedEventArgs());
            };

            if (async)
            {
                process.processExited += ((sender, args) => { EndOfExecutionProcessing(); });
            }
            else
            {
                EndOfExecutionProcessing();
            }
        }
    }
}