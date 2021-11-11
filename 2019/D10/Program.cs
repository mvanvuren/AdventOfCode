using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace D10
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = File.ReadLines("astro.map").Select(l => l.ToCharArray().Select(c => c == '#').ToArray()).ToArray();

            var mapSize = map.GetLength(0);
            var stars = (from y in Enumerable.Range(0, mapSize) from x in Enumerable.Range(0, mapSize) where map[y][x] select (x, y)).ToList();

            var maxVisibleCount = 0;
            int mx = 0, my = 0;
            var deathStars = new Dictionary<double, List<(int x, int y, double angle, double distance)>>();
            foreach (var (x, y) in stars)
            {
                var visibleCount = stars.Count - 1;
                var remoteStars = new Dictionary<double, List<(int x, int y, double angle, double distance)>>();
                foreach (var remoteStar in stars
                    .Where(e => !(e.x == x && e.y == y))
                    .Select(rs => (rs.x, rs.y, angle: Math.Atan2(rs.y - y, rs.x - x) * 180.0 / Math.PI, distance: Math.Sqrt(Math.Pow(rs.x - x, 2) + Math.Pow(rs.y - y, 2))))
                    .OrderBy(o => o.distance) 
                )
                {
                    var foundAngles = remoteStars.Keys.Where(a => remoteStar.angle >= a - double.Epsilon && remoteStar.angle <= a + double.Epsilon).ToList();
                    if (foundAngles.Count > 0)
                    {
                        visibleCount--;
                        remoteStars[foundAngles.First()].Add(remoteStar);
                        continue;
                    }
                    remoteStars.Add(remoteStar.angle, new List<(int, int, double, double)>(new [] {remoteStar}));
                }

                if (visibleCount > maxVisibleCount)
                {
                    maxVisibleCount = visibleCount;
                    mx = x;
                    my = y;
                    deathStars = remoteStars;
                }
            }

            Console.WriteLine($"({mx},{my}): {maxVisibleCount}"); // D10P1: (11,13): 227

            var vaporizeCount = 0;
            var vaporizing = true;
            while (vaporizing)
            {
                vaporizing = false;
                foreach (var key in deathStars.Keys.Where(s => s >= -90.0).OrderBy(o => o).Concat(deathStars.Keys.Where(s => s < -90.0).OrderBy(o => o)))
                {
                    var deathStarsWithSameAngle = deathStars[key];
                    if (deathStarsWithSameAngle.Count == 0) continue;

                    vaporizing = true;
                    var deathStar = deathStarsWithSameAngle[0];
                    Console.WriteLine($"[{vaporizeCount+1}] ({deathStar.x},{deathStar.y}), angle={deathStar.angle}, distance={deathStar.distance}");

                    deathStarsWithSameAngle.RemoveAt(0);
                    vaporizeCount++;
                }
            }
        }
    }
}
