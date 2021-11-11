using System;
using System.IO;
using System.Linq;

namespace D16
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllText("signal.dat").ToCharArray().Select(c => c - '0').ToArray();
            //var numbers = "19617804207202209144916044189917".ToCharArray().Select(c => c - '0').ToArray();
            var basePattern = new[] {0, 1, 0, -1};

            foreach (var count in Enumerable.Range(1, 100))
            {
                foreach (var index in Enumerable.Range(0, numbers.Length))
                {
                    var pattern = basePattern.SelectMany(s => Enumerable.Repeat(s, index + 1)).ToArray();

                    numbers[index] = Math.Abs(Enumerable.Range(0, numbers.Length)
                                         .Sum(i => numbers[i] * pattern[(i + 1) % pattern.Length])) % 10;
                }
                Console.WriteLine(string.Join(" ", numbers));

            }
        }
    }
}
