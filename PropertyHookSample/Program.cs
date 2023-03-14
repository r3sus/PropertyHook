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
                var ms = 100;
                float ait = 0;
                if (hookDSR.Hooked)
                {
                    ait = hookDSR.AiTimerValue;
                    ms = 16;
                }
                if (hookPTDE.Hooked)
                {
                    ait = hookPTDE.AiTimerValue;
                    ms = 32;
                }
                //Console.WriteLine($"Handle:\n DSR: {hookDSR.Handle:0.000}\n PTDE: {hookPTDE.Handle}");
                Console.WriteLine($"AiTimer: {hookPTDE}");
                /*
                if (hook.Health < 200)
                    hook.Health += 200;
                */
                Thread.Sleep(ms);

                Console.Clear();
            }

            // Not strictly necessary; the hooking thread is a background thread, so it will exit automatically
            hookDSR.Stop();
            hookPTDE.Stop();
        }
    }
}
