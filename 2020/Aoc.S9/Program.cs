using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S9
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllLines("data.txt").Select(line => long.Parse(line)).ToArray();
            const int preamble = 25;
            var index = preamble;
            while (index < numbers.Length)
            {
                if (!IsValidNumber(numbers[index], numbers.Skip(index - preamble).Take(preamble).ToArray()))
                {
                    break;
                }

                index++;
            }

            var invalidNumber = numbers[index];
            for (var i = 0; i < numbers.Length - 1; i++)
            {
                var sum = numbers[i];
                for (var j = i + 1; j < numbers.Length; j++)
                {
                    sum += numbers[j];
                    if (sum < invalidNumber) continue;
                    if (sum == invalidNumber)
                    {
                        var sumNumbers = numbers.Skip(i).Take(j - i + 1).ToList();
                        var minNumber = sumNumbers.Min();
                        var maxNumber = sumNumbers.Max();
                        var answer = minNumber + maxNumber;
                    }

                    break;
                }
            }

        }

        static bool IsValidNumber(long number, IReadOnlyList<long> numbers)
        {
            for (var i = 0; i < numbers.Count - 1; i++)
            {
                for (var j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == number) return true;
                }
            }
            return false;
        }
    }
}
