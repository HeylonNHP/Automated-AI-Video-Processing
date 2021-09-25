using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Automated_AI_Video_Processing.ProcessExecution
{
    public class LaunchProcess
    {
        private Process selectedProcess = new Process();
        public DataReceivedEventHandler stdErrOutputEvent;
        public DataReceivedEventHandler stdOutOutputEvent;
        public EventHandler processExited;


        public LaunchProcess(string filename, string args)
        {
            selectedProcess.EnableRaisingEvents = true;
            selectedProcess.StartInfo.FileName = filename;
            selectedProcess.StartInfo.Arguments = args;

            selectedProcess.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                stdErrOutputEvent?.Invoke(sender, e);
            };
            selectedProcess.OutputDataReceived += (sender, eventArgs) =>
            {
                stdOutOutputEvent?.Invoke(sender, eventArgs);
            };
            selectedProcess.Exited += (sender, eventArgs) =>
            {
                processExited?.Invoke(sender,eventArgs);
            };
        }
        public ProcessStartInfo getStartInfo
        {
            get { return selectedProcess.StartInfo; }
        }

        public StreamWriter stdIn
        {
            get { return selectedProcess.StandardInput; }
        }

        public StreamReader stdErr
        {
            get { return selectedProcess.StandardError; }
        }

        public StreamReader stdOut
        {
            get { return selectedProcess.StandardOutput; }
        }

        public bool redirectStdIn
        {
            set { selectedProcess.StartInfo.RedirectStandardInput = value; }
        }

        public bool redirectStdErr
        {
            set { selectedProcess.StartInfo.RedirectStandardError = value; }
        }

        public bool redirectStdOut
        {
            set { selectedProcess.StartInfo.RedirectStandardOutput = value; }
        }

        public void run(bool waitForExit = true)
        {
            selectedProcess.Start();
            if (waitForExit)
            {
                selectedProcess.WaitForExit();
            }
        }

        public void WaitForExit()
        {
            selectedProcess.WaitForExit();
        }
    }
}