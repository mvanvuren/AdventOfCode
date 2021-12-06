using System;
using System.IO;
using System.Linq;

namespace Aoc.S6
{
    class Program
    {
        static void Main()
        {
            const int maxDay = 256; // S6.1: 80
            const int firstCycle = 8;
            const int secondCycle = 6;

            var population = new ulong[firstCycle + 1];

            foreach (var number in File.ReadAllLines("data.txt").Select(s => s.Split(',').Select(int.Parse)).First())
                population[number]++;

            var afterDay = 0;
            while (afterDay++ < maxDay)
            { // a rotating pointer would be faster...
                var zeros = population[0];
                for (var index = 1; index < population.Length; index++)
                    population[index - 1] = population[index];
                population[firstCycle] = zeros;
                population[secondCycle] += zeros;
            }

            var sum = (ulong)population.Sum(n => (decimal)n);

            Console.WriteLine(sum); // S6.1: 362666, S6.2: 1640526601595
        }
    }
}
