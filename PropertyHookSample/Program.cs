using System;
using System.Threading;

namespace PropertyHookSample
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleHook hook = new SampleHook();
            hook.Start();

            var rand = new Random();
            while (!Console.KeyAvailable)
            {
                Console.WriteLine($"AiTimer: {hook.AiTimerValue:0.000}");
                /*
                if (hook.Health < 200)
                    hook.Health += 200;
                */
                var s1 = rand.Next(100);
                Thread.Sleep(s1);

                Console.Clear();
            }

            // Not strictly necessary; the hooking thread is a background thread, so it will exit automatically
            hook.Stop();
        }
    }
}
