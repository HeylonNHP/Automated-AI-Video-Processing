using System;
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
            ai.runAsync();
        }
    }
}