using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Aoc.S20
{
    class Program
    {
        static void Main(string[] args)
        {
            var sides = new List<short>();
            var ids = new List<int>();
            var regex = new Regex(@"\d+", RegexOptions.Compiled);

            foreach (var tile in File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine))
            {
                var lines = tile.Split(Environment.NewLine);

                ids.Add(int.Parse(regex.Match(lines[0]).Value));

                var s1 = lines[1];
                var s2 = lines[10];
                var s3 = lines.Skip(1).Aggregate(string.Empty, (current, line) => current + line[0]);
                var s4 = lines.Skip(1).Aggregate(string.Empty, (current, line) => current + line[9]);

                sides.Add(CalculateSide(s1));
                sides.Add(CalculateSide(s2));
                sides.Add(CalculateSide(s3));
                sides.Add(CalculateSide(s4));

                sides.Add(CalculateSide(Reverse(s1)));
                sides.Add(CalculateSide(Reverse(s2)));
                sides.Add(CalculateSide(Reverse(s3)));
                sides.Add(CalculateSide(Reverse(s4)));
            }

            var matchCount = new int[sides.Count / 8];
            for (var i = 0; i < sides.Count / 8; i++)
            {
                var s = 8 * i;
                var e = s + 8;
                for (var j = s; j < e; j++)
                {
                    var otherSides = new List<int>();
                    for (var k = 0; k < sides.Count; k++)
                    {
                        if (sides[j] != sides[k]) continue;

                        var l = k / 8;
                        if (i != l)
                        {
                            otherSides.Add(l);
                        }
                    }

                    if (otherSides.Count > 1)
                    {
                        Console.WriteLine("Adjust algorithm, i guess");
                    }
                    matchCount[i] += otherSides.Count;
                }
            }

            var total = 1L;
            for (var index = 0; index < sides.Count / 8; index++)
            {
                if (matchCount[index] == 4)
                {
                    total = total * (long)ids[index];
                    Console.WriteLine(ids[index]);
                }
            }
            Console.WriteLine(total);
        }


        static short CalculateSide(string side)
        {
            short value = 0;
            foreach (var c in side)
            {
                value <<= 1;
                if (c == '#') value++;
            }

            return value;
        }

        static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
