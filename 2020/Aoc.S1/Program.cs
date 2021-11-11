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
            const int year = 2020;

            var numbers = new SortedSet<int>(File.ReadAllLines("data.txt").Select(int.Parse));

            ////S1A: (267:1753) => 468051
            //foreach (var firstNumber in numbers)
            //{
            //    var secondNumber = year - firstNumber;
            //    if (!numbers.Contains(secondNumber)) continue;
            //    Console.WriteLine($"({firstNumber}:{secondNumber}) => {firstNumber * secondNumber}");
            //    break;
            //}

            //S1B: (523:551:946) => 272611658
            foreach (var firstNumber in numbers)
            {
                foreach (var secondNumber in numbers.Where(n => n < year - firstNumber))
                {
                    var thirdNumber = year - firstNumber - secondNumber;
                    if (numbers.Contains(thirdNumber))
                    {
                        Console.WriteLine(
                            $"({firstNumber}:{secondNumber}:{thirdNumber}) => {firstNumber * secondNumber * thirdNumber}");
                    }
                }
            }

        }
    }
}
