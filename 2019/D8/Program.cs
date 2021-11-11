using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;

namespace D8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // D8P1
            //var layer = File.ReadAllText("image.dat").Split(25 * 6)
            //    .Select(l => new { ZeroCount = l.Count(c => c == '0'), OneCount = l.Count(c => c == '1'), TwoCount = l.Count(c => c == '2') })
            //    .OrderBy(o => o.ZeroCount)
            //    .First();

            //Console.WriteLine(layer.OneCount * layer.TwoCount); // D8P1: 1862

            // D8P2: GCPHL
            var (width, height) = (25, 6);
            var layers = File.ReadAllText("image.dat").Split(width * height).ToArray();

            foreach (var y in Enumerable.Range(0, height))
            {
                var line = string.Empty;
                foreach (var x in Enumerable.Range(0, width))
                {
                    var l = 0;
                    while (layers[l][x + (y * width)] == '2') l++;
                    line += layers[l][x + (y * width)];
                }
                Console.WriteLine(line.Replace("0", " ").Replace("1", "X"));
            }
        }
    }

    public static class StringExtensions
    {
        public static IEnumerable<string> Split(this string value, int chunkSize)
        {
            return Enumerable.Range(0, value.Length / chunkSize)
                .Select(i => value.Substring(i * chunkSize, chunkSize));
        }
    }
}
