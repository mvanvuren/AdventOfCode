using System;
using System.IO;
using System.Linq;

namespace Aoc.S2
{
    class Program
    {
        static void Main(string[] args)
        {
            //// S2-A: Valid passwords: 628
            //var validPasswordCount = File.ReadAllLines("data.txt").Where(l =>
            //{
            //    var pp = l.Split(new []{'-', ' ', ':'}, StringSplitOptions.RemoveEmptyEntries);
            //    var cc = pp[3].Count(c => c == pp[2][0]);

            //    return cc >= int.Parse(pp[0]) && cc <= int.Parse(pp[1]);
            //}).Count();
            //Console.WriteLine($"Valid passwords: {validPasswordCount}");

            // S2-B: Valid passwords: 705
            var validPasswordCount = File.ReadAllLines("data.txt").Where(l =>
            {
                var pp = l.Split(new[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                var fp = int.Parse(pp[0]);
                var sp = int.Parse(pp[1]);
                var c = pp[2][0];
                var pwd = pp[3];

                return (pwd[fp - 1] == c && pwd[sp - 1] != c)
                    || (pwd[fp - 1] != c && pwd[sp - 1] == c);
            }).Count();
            Console.WriteLine($"Valid passwords: {validPasswordCount}");

        }
    }
}
