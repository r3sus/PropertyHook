using System;
using System.Threading;

namespace PropertyHookSample
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var hookDSR = new SampleHookDSR();
            hookDSR.Start();
            
            var hookPTDE = new SampleHookPTDE();
            hookPTDE.Start();
            
            while (!Console.KeyAvailable)
            {
                int fps = 1;
                float ait = 0;
                if (hookDSR.Hooked)
                {
                    ait = hookDSR.AiTimerValue;
                    fps = 30;
                }
                if (hookPTDE.Hooked)
                {
                    ait = hookPTDE.AiTimerValue;
                    fps = 60;
                }
                //Console.WriteLine($"Handle:\n DSR: {hookDSR.Handle:0.000}\n PTDE: {hookPTDE.Handle}");
                Console.WriteLine($"AiTimer: {ait}");
                /*
                if (hook.Health < 200)
                    hook.Health += 200;
                */
                int ms = (int) (1.0 / fps * 1000);
                ms /= 5;
                Thread.Sleep(ms);

                Console.Clear();
            }

            // Not strictly necessary; the hooking thread is a background thread, so it will exit automatically
            hookDSR.Stop();
            hookPTDE.Stop();
        }
    }
}
