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
            /*
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            */
            Console.WriteLine("DS1/R Ai Timer");
            Console.Write("waiting 6s to init");

            Thread.Sleep(6000);
            while (true) //!Console.KeyAvailable)
            {
                // Console.ForegroundColor = ConsoleColor.Gray;
                int fps = 1;
                float ait = 0;
                //var hkd = hookDSR.Hooked || hookPTDE.Hooked;

                //PropertyHook.PHook ph1;

                if (hookDSR.Hooked)
                {
                    //ph1 = hookDSR;
                    fps = 60;
                }
                else if (hookPTDE.Hooked)
                {
                    //ph1 = hookPTDE;
                    fps = 30;
                }
                else
                {
                    var s1 = "not found, waiting 5s loop";
                    if (Console.CursorLeft != s1.Length) Console.Write(s1);
                    Thread.Sleep(5000);
                    //Console.CursorLeft -= s1.Length;
                    //Console.WriteLine(); // -= 1; // s1.Length; Clear(); // 
                    //Console.
                    continue;
                }

                Console.Clear();

                int ms = (int)(1.0 / fps * 1000);

                Console.WriteLine($"hooked.\n auto update rate: {ms} (ms).\n enter lower value to adjust. x to exit. "); 
                
                Console.CursorVisible = !false;
                var f1 = Console.ReadLine();

                if (f1 == "x") break;

                int i1;

                if (int.TryParse(f1, out i1))
                {
                    if (i1 < ms) ms = i1;
                }
                else { }

                // or just adjust gadget update rate?

                Console.WriteLine(" press to stop & re-hook.\n AiTimer: ");

                while (!Console.KeyAvailable)
                {
                    // Console.ForegroundColor = ConsoleColor.Blue;
                    Console.CursorVisible = false;
                    // bad:
                    if (hookDSR.Hooked)
                    {
                        ait = hookDSR.AiTimerValue;
                    }
                    else if (hookPTDE.Hooked)
                    {
                        ait = hookPTDE.AiTimerValue;
                    }
                    else
                    { 
                        break; // good
                    }

                    var s1 = $"{ait}"; // :0.00000
                    Console.Write(s1);

                    Thread.Sleep(ms);

                    Console.CursorLeft -= s1.Length;
                    //Console.Clear();
                }
            }

            // Not strictly necessary; the hooking thread is a background thread, so it will exit automatically
            hookDSR.Stop();
            hookPTDE.Stop();
        }
    }
}
