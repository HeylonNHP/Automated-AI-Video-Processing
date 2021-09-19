using System;
using System.Threading;
using Automated_AI_Video_Processing.AiProcessors;

namespace Automated_AI_Video_Processing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TopazVideoEnhanceAI ai = new TopazVideoEnhanceAI(
                "N:\\stuph\\veai5\\26\\Wild.webm",
                TopazVeaiOutputFormats.mov_proreshq,
                TopazVeaiModels.amq13,
                1,
                new TopazVeaiScalingDetails(1));


            bool done = false;

            ai.onTopazVeaiFinished += (sender, eventArgs) =>
            {
                Console.WriteLine("Process has exited");
                done = true;
            };

            ai.runAsync();
            
            Console.WriteLine("Waiting");
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}