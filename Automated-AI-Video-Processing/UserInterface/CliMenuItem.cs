using System;

namespace Automated_AI_Video_Processing.UserInterface
{
    public class CliMenuItem
    {
        private string captionInternal;
        private Action runActionInternal;
        
        public CliMenuItem(string caption, Action runAction)
        {
            this.captionInternal = caption;
            this.runActionInternal = runAction;
        }

        public string caption
        {
            get
            {
                return captionInternal;
            }
        }

        public Action runAction
        {
            get
            {
                return runActionInternal;
            }
        }

        public void executeAction()
        {
            runActionInternal();
        }
    }
}