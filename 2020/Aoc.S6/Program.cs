using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S6
{
    class Program
    {
        static void Main(string[] args)
        {
            //// S6-1: 6521
            //var total = File.ReadAllText("data.txt")
            //        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            //        .Sum(line => line.Replace(Environment.NewLine, string.Empty).Distinct().Count());

            // S6-2: 3305
            //var total = File.ReadAllText("data.txt")
            //    .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            //    .Sum(lines => GetSharedCount(lines.Split(Environment.NewLine)));

            var total = File.ReadAllText("data.txt")
                .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Sum(lines => lines.Split(Environment.NewLine).Cast<IEnumerable<char>>().Aggregate((current, next) => current.Intersect(next)).Count());

        }

        static int GetSharedCount(string[] lines)
        {
            return lines.Skip(1).Aggregate((IEnumerable<char>)lines[0], (current, line) => current.Intersect(line)).Count();
        }
    }
}
