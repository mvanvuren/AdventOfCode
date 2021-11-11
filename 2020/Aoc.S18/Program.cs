using System;
using System.IO;
using LoreSoft.MathExpressions;


namespace Aoc.S18
{
    class Program
    {
        static void Main(string[] args)
        {
            double sum = 0;
            var eval = new MathEvaluator();
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var result = eval.Evaluate(line);
                //Console.WriteLine(result);
                sum += result;

            }
            Console.WriteLine(sum);
        }
    }
}
