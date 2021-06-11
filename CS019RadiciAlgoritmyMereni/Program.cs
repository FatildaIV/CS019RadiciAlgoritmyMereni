using System;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace CS019RadiciAlgoritmyMereni
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter("vysledky.csv");   
            var start = HighResolutionDateTime.UtcNow;
            //var sw = Stopwatch.StartNew();

            //while (sw.Elapsed.TotalSeconds < 10)
            //{
            //    DateTime nowBasedOnStopwatch = start + sw.Elapsed;
            //    TimeSpan diff = HighResolutionDateTime.UtcNow - nowBasedOnStopwatch;

            //    Console.WriteLine("Diff: {0:0.000} ms", diff.TotalMilliseconds);  

            //    Thread.Sleep(1000);
            //}
            int a = 7518963, b = -736669222, n = 10;

            start = HighResolutionDateTime.UtcNow;
            for (int i = 0; i < 10000000; i++)
                ProhoditPromenna<int>(ref a, ref b);

            for (int i = 0; i < 10000000; i++)
                ProhoditAritmeticky<int>(ref a, ref b);

            for (int i = 0; i < 10000000; i++)
                ProhoditBitove<int>(ref a, ref b);

            sw.WriteLine("Pocet prohozeni; Prohozeni promenou; Prohozeni aritmeticky; Prohozeni bitove");
            while (n<=100000000)
            {

                sw.Write("{0};", n);
                Console.Write(".");

                start = HighResolutionDateTime.UtcNow;
                for (int i = 0; i < n; i++)
                    ProhoditPromenna<int>(ref a, ref b);
                sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
                Console.Write(".");

                start = HighResolutionDateTime.UtcNow;
                for (int i = 0; i < n; i++)
                    ProhoditAritmeticky<int>(ref a, ref b);
                sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
                Console.Write(".");

                start = HighResolutionDateTime.UtcNow;
                for (int i = 0; i < n; i++)
                    ProhoditBitove<int>(ref a, ref b);
                sw.Write("{0:0.0000};", (HighResolutionDateTime.UtcNow - start).TotalMilliseconds);
                Console.Write(".");

                sw.WriteLine();
                n *= 10;                    // Zvýšit N 10násobně
            }

            sw.Close();

            Console.WriteLine("Měření dokončeno");
        }
        static void ProhoditPromenna<T>(ref T a, ref T b)
        {
            T temp = a;     // do temp ulozit a
            a = b;          // do a ulozit b
            b = temp;       // do b ulozit temp
        }

        static void ProhoditAritmeticky<T>(ref T a, ref T b)
        {
            a = (dynamic)a - (dynamic)b;  // snížit a o velikost b
            b = (dynamic)b + (dynamic)a;  // zvýšit b o rozdil mezi a a b
            a = (dynamic)b - (dynamic)a;  // dostat do a původní hodnotu b
        }

        static void ProhoditBitove<T>(ref T a, ref T b)
        {
            a = (dynamic)a ^ (dynamic)b;  // do a bitový XOR mezi a a b
            b = (dynamic)b ^ (dynamic)a;  // do b bitový XOR mezi b a a
            a = (dynamic)b ^ (dynamic)a;  // do a bitový XOR mezi b a a
        }
    }
}
