using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S17
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var grid = new List<(int X, int Y, int Z)>();
        //    (int X, int Y, int Z) p = (0, 0, 0);
        //    foreach (var line in File.ReadAllLines("input.txt"))
        //    {
        //        foreach (var c in line)
        //        {
        //            if (c == '#')
        //            {
        //                grid.Add((p.X, p.Y, p.Z));
        //            }

        //            p.X++;
        //        }

        //        p.X = 0;
        //        p.Y++;
        //    }

        //    const int maxRound = 6;
        //    var round = 0;
        //    while (round < maxRound)
        //    {
        //        (int X, int Y, int Z) minP = (grid.Min(n => n.X), grid.Min(n => n.Y), grid.Min(n => n.Z));
        //        (int X, int Y, int Z) maxP = (grid.Max(n => n.X), grid.Max(n => n.Y), grid.Max(n => n.Z));

        //        // brute force
        //        var becomeInactive = new List<(int X, int Y, int Z)>();
        //        var becomeActive = new List<(int X, int Y, int Z)>();
        //        for (var x = minP.X - 1; x <= maxP.X + 1; x++)
        //        {
        //            for (var y = minP.Y - 1; y <= maxP.Y + 1; y++)
        //            {
        //                for (var z = minP.Z - 1; z <= maxP.Z + 1; z++)
        //                {
        //                    var count = grid.Count(n => 
        //                            n.X >= x - 1 && n.X <= x + 1
        //                            && n.Y >= y - 1 && n.Y <= y + 1
        //                            && n.Z >= z - 1 && n.Z <= z + 1
        //                        );
        //                    var isActive = grid.Count(n => n.X == x && n.Y == y && n.Z == z) == 1;
        //                    if (isActive)
        //                    {
        //                        count--; // itself
        //                        if (!(count == 2 || count == 3))
        //                        {
        //                            becomeInactive.Add((x, y, z));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (count == 3)
        //                        {
        //                            becomeActive.Add((x, y, z));
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        // update grid
        //        foreach (var inactive in becomeInactive)
        //        {
        //            grid.Remove(inactive);
        //        }
        //        grid.AddRange(becomeActive);

        //        round++;
        //    }
        static void Main(string[] args)
        {
            var grid = new List<(int X, int Y, int Z, int W)>();
            (int X, int Y, int Z, int W) p = (0, 0, 0, 0);
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                foreach (var c in line)
                {
                    if (c == '#')
                    {
                        grid.Add((p.X, p.Y, p.Z, p.W));
                    }

                    p.X++;
                }

                p.X = 0;
                p.Y++;
            }

            const int maxRound = 6;
            var round = 0;
            while (round < maxRound)
            {
                (int X, int Y, int Z, int W) minP = (grid.Min(n => n.X), grid.Min(n => n.Y), grid.Min(n => n.Z), grid.Min(n => n.W));
                (int X, int Y, int Z, int W) maxP = (grid.Max(n => n.X), grid.Max(n => n.Y), grid.Max(n => n.Z), grid.Max(n => n.W));

                // brute force
                var becomeInactive = new List<(int X, int Y, int Z, int W)>();
                var becomeActive = new List<(int X, int Y, int Z, int W)>();
                for (var x = minP.X - 1; x <= maxP.X + 1; x++)
                {
                    for (var y = minP.Y - 1; y <= maxP.Y + 1; y++)
                    {
                        for (var z = minP.Z - 1; z <= maxP.Z + 1; z++)
                        {
                            for (var w = minP.W - 1; w <= maxP.W + 1; w++)
                            {
                                var count = grid.Count(n =>
                                    n.X >= x - 1 && n.X <= x + 1
                                                 && n.Y >= y - 1 && n.Y <= y + 1
                                                 && n.Z >= z - 1 && n.Z <= z + 1
                                                 && n.W >= w - 1 && n.W <= w + 1
                                );
                                var isActive = grid.Count(n => n.X == x && n.Y == y && n.Z == z && n.W == w) == 1;
                                if (isActive)
                                {
                                    count--; // itself
                                    if (!(count == 2 || count == 3))
                                    {
                                        becomeInactive.Add((x, y, z, w));
                                    }
                                }
                                else
                                {
                                    if (count == 3)
                                    {
                                        becomeActive.Add((x, y, z, w));
                                    }
                                }

                            }
                        }
                    }
                }

                // update grid
                foreach (var inactive in becomeInactive)
                {
                    grid.Remove(inactive);
                }
                grid.AddRange(becomeActive);

                round++;
            }

        }
    }
}
