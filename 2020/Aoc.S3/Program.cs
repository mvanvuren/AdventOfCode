using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Aoc.S3
{
    class Program
    {
        static Slope slope = new Slope();

        // S3-A: Trees encountered: 159
        static void Main(string[] args)
        {
            slope.Initialize("data.txt");

            var r1 = GetTreeCount(1, 1);
            var r2 = GetTreeCount(3, 1);
            var r3 = GetTreeCount(5, 1);
            var r4 = GetTreeCount(7, 1);
            var r5 = GetTreeCount(1, 2);

            //Console.WriteLine($"Trees encountered: {r2}");
            Console.WriteLine($"Result: {r1 * r2 * r3 * r4 * r5}"); // Result: 6419669520
        }

        static long GetTreeCount(int right, int down)
        {
            var treeCount = 0L;

            var x = 0;
            for (var y = 0; y < slope.Length; y += down)
            {
                treeCount += slope.GetPositionType(x, y) == PositionType.Tree ? 1 : 0;
                x += right;
            }

            return treeCount;
        }
    }

    public enum PositionType
    {
        Open,
        Tree,
        OffSlope
    }

    public class Slope
    {
        public int Width { get; set; }
        public int Length { get; set; }
        List<string> SlopeRows { get; set; }

        public void Initialize(string fileName)
        {
            SlopeRows = File.ReadAllLines(fileName).ToList();
            Width = SlopeRows.Max(line => line.Length);
            Length = SlopeRows.Count;
        }

        public PositionType GetPositionType(int x, int y)
        {
            if (y < 0 || y >= Length || x < 0 || x % Width >= SlopeRows[y].Length) return PositionType.OffSlope;

            return SlopeRows[y][x % Width] == '#' ? PositionType.Tree : PositionType.Open;
        }
    }
}
