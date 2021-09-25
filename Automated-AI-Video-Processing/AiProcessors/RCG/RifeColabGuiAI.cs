using System;
using System.IO;
using System.Threading;
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
            process.run(false);

            StreamReader stdOut = process.stdOut;

            bool isDone = false;
            string stdOutString = "";

            var consumeStdOut = new Thread(() =>
            {
                while (!isDone)
                {
                    string blah = stdOut.ReadToEnd();
                    stdOutString += blah;
                }
            });
            consumeStdOut.Start();

            if (!async)
            {
                process.WaitForExit();
                isDone = true;
            }

            Action EndOfExecutionProcessing = () =>
            {
                string outputfile = null;

                string[] lines = stdOutString.Split("\n");
                foreach (string line in lines)
                {
                    if (line.Contains("Created output file: "))
                    {
                        outputfile = line.Replace("Created output file: ", "");
                    }
                }

                rifeFinished?.Invoke(this, new RifeColabGuiFinishedEventArgs(outputfile));
            };

            if (async)
            {
                process.processExited += ((sender, args) =>
                {
                    isDone = true;
                    EndOfExecutionProcessing();
                });
            }
            else
            {
                EndOfExecutionProcessing();
            }
        }
    }
}