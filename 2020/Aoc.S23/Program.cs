using System;
using System.Linq;

namespace Aoc.S23
{
    class Program
    {
        //static void Main(string[] args)
        //{

        //    //var input = new byte[] { 3, 8, 9, 1, 2, 5, 4, 6, 7 };
        //    //var next = new byte[] { 3, 2, 5, 8, 6, 4, 7, 3, 9, 1 };
        //    var input = new byte[] { 9, 6, 3, 2, 7, 5, 4, 8, 1 };
        //    var next = new byte[] { 9, 9, 7, 2, 8, 4, 3, 5, 1, 6 };

        //    //Dump(next);

        //    var move = 0;
        //    while (move < 100)
        //    {
        //        var cv = next[0];
        //        //var cv = sv;
        //        var nv = cv;
        //        do
        //        {
        //            if (--nv < 1) nv = 9;
        //        } while (nv == next[cv] || nv == next[next[cv]] || nv == next[next[next[cv]]]);

        //        var lv = next[next[next[next[cv]]]];
        //        next[next[next[next[cv]]]] = next[nv];
        //        next[nv] = next[cv];
        //        next[cv] = lv;
        //        next[0] = next[next[0]];


        //        // 2 89154673
        //        Dump(next);

        //        move++;
        //    }
        //}

        static void Main(string[] args)
        {
            const int size = 1000000;
            //TEST: var input = new byte[] { 3, 8, 9, 1, 2, 5, 4, 6, 7 };
            //TEST: var next = new byte[] { 3, 2, 5, 8, 6, 4, 7, 3, 9, 1 };
            //var input = new byte[] { 9, 6, 3, 2, 7, 5, 4, 8, 1 };
            //var next = new byte[] { 9, 9, 7, 2, 8, 4, 3, 5, 1, 6 };
            var next = new int[size + 1];
            foreach(var i in Enumerable.Range(0, size + 1))
            {
                next[i] = i + 1;
            }
            // TEST: { 3, 2, 5, 8, 6, 4, 7, 3, 9, 1 }
            next[0] = 9;
            next[1] = 10;
            next[2] = 7;
            next[3] = 2;
            next[4] = 8;
            next[5] = 4;
            next[6] = 3;
            next[7] = 5;
            next[8] = 1;
            next[9] = 6;
            next[size] = 9;
            //{ 9, 9, 7, 2, 8, 4, 3, 5, 1, 6 }

            //Dump(next);

            var move = 0;
            while (move < 10 * size)
            {
                var cv = next[0];
                var nv = cv;
                do
                {
                    if (--nv < 1) nv = size;
                } while (nv == next[cv] || nv == next[next[cv]] || nv == next[next[next[cv]]]);

                var lv = next[next[next[next[cv]]]];
                next[next[next[next[cv]]]] = next[nv];
                next[nv] = next[cv];
                next[cv] = lv;
                next[0] = next[next[0]];

                move++;
            }

            var index = 0;
            while (++index < size + 1)
            {
                if (next[index] == 1)
                {
                    Console.WriteLine(next[next[index]]);
                    Console.WriteLine(next[next[next[index]]]);
                    Console.WriteLine((long)next[next[index]] * next[next[next[index]]]);
                }
            }
        }

        static void Dump(int[] next)
        {
            var sp = next[0];
            var cp = sp;
            do
            {
                Console.Write(cp);
                cp = next[cp];
            } while (sp != cp);

            Console.WriteLine();
        }
    }
}
