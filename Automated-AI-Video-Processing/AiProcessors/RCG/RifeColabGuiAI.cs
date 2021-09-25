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

        public RifeColabGuiSettings Settings
        {
            get => settings;
            set => settings = value;
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
                string outputfile = null;
                string line = stdOut.ReadLine();
                while (line != null)
                {
                    if (line.Contains("Created output file: "))
                    {
                        outputfile = line.Replace("Created output file: ", "");
                    }
                    line = stdOut.ReadLine();
                }
                rifeFinished?.Invoke(this, new RifeColabGuiFinishedEventArgs(outputfile));
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