using System;
using System.IO;
using System.Linq;

namespace Aoc.S7
{
    class Program
    {
        static void Main()
        {
            var numbers = File.ReadAllText("data.txt").Split(',').Select(uint.Parse).OrderBy(n => n).ToList();

            // S7.1
            //var median = numbers.Count % 2 == 0 ? (numbers[numbers.Count / 2 - 1] + numbers[numbers.Count / 2]) / 2 : numbers[numbers.Count / 2];
            //var fuel = numbers.Sum(n => median >= n ? median - n : n - median);
            //Console.WriteLine(fuel); // S7.1: 356922

            var mean = (uint)Math.Floor((decimal)numbers.Sum(n => n) / numbers.Count);
            var fuel = numbers.Sum(n => BinomialCoefficient(mean >= n ? mean - n : n - mean));
            Console.WriteLine(fuel); // S7.2: 100347031
        }

        static uint BinomialCoefficient(uint n)
        {
            return (n * n + n) / 2;
        }
    }
}
