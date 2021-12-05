using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S5
{
    class Program
    {
        static void Main()
        {
            var points = new Dictionary<(int x, int y), int>();

            var lines = File.ReadAllLines("data.txt")
                    .Select(s => s.Replace(" -> ", ",")
                    .Split(',').Select(int.Parse).ToList())
                    .Select(c => (start: (x: c[0], y: c[1]), end: (x: c[2], y: c[3])));

            foreach (var line in lines)
            {
                (int x, int y) delta;

                if (line.start.x == line.end.x)
                    delta = (0, line.start.y <= line.end.y ? 1 : -1);
                else if (line.start.y == line.end.y)
                    delta = (line.start.x <= line.end.x ? 1 : -1, 0);
                else
                    // S5.1: continue;
                    delta = (line.start.x <= line.end.x ? 1 : -1, line.start.y <= line.end.y ? 1 : -1); // S5.2

                // draw line
                var c = line.start;
                while(c != line.end)
                {
                    AddPoint(points, c);

                    c.x += delta.x;
                    c.y += delta.y;
                }
                AddPoint(points, c);
            }

            var count = points.Keys.Count(k => points[k] > 1); // S5.1: 7269, S5.2: 21140

            Console.WriteLine(count);
        }

        static void AddPoint(IDictionary<(int x, int y), int> points, (int x, int y) point)
        {
            if (!points.ContainsKey(point))
                points[point] = 0;

            points[point]++;
        }
    }
}
