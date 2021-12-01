using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S1
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>(File.ReadAllLines("data.txt").Select(int.Parse));
            //// S1.1
            //var count = numbers.Skip(1).Zip(numbers, (x, y) => x > y).Count(c => c);
            //Console.WriteLine(count); // 1564

            // S1.2
            var count = 0;
            const int windowSize = 3;
            var previousSum = numbers.Take(windowSize).Sum();
            for(var index = windowSize; index < numbers.Count; index++)
            {
                var currentSum = -numbers[index - windowSize] + previousSum + numbers[index];
                if (currentSum > previousSum) count++;
                previousSum = currentSum;
            }
            Console.WriteLine(count); // 1611
        }
    }
}
