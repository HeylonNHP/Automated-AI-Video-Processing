using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Automated_AI_Video_Processing.AiProcessors.RCG
{
    public class RifeColabGuiAiQueuedProcessing
    {
        private RifeColabGuiAI _rifeColabGuiAi;
        private bool runQueue = false;
        private bool stopOnQueueFinished = false;
        private Queue<RifeColabGuiSettings> queuedInterpolations = new Queue<RifeColabGuiSettings>();
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        public RifeColabGuiAiQueuedProcessing(RifeColabGuiAI inputAiObject)
        {
            _rifeColabGuiAi = inputAiObject;

            {
                // Init worker thread
                backgroundWorker.DoWork += ((sender, args) =>
                {
                    while (runQueue)
                    {
                        if (queuedInterpolations.Count > 0)
                        {
                            RifeColabGuiSettings currentSettings = queuedInterpolations.Dequeue();
                            _rifeColabGuiAi.Settings = currentSettings;
                            _rifeColabGuiAi.runInteroplationSingleFile();
                        }
                        else
                        {
                            if (stopOnQueueFinished)
                            {
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                    }
                });
            }
        }

        public void startQueue()
        {
            runQueue = true;
            backgroundWorker.RunWorkerAsync();
        }

        public void requestQueueStop()
        {
            runQueue = false;
            backgroundWorker.CancelAsync();
        }

        public void allowQueueToCompleteThenStop()
        {
            stopOnQueueFinished = true;
        }
        public void disableAllowQueueToCompleteThenStop()
        {
            stopOnQueueFinished = false;
        }

        public Queue<RifeColabGuiSettings> QueuedInterpolations
        {
            get => queuedInterpolations;
            set => queuedInterpolations = value;
        }
    }
}