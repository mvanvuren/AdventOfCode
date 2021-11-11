using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Combinatorics.Collections;

namespace D12
{
    class Program
    {
        const int MaxSteps = 1000;

        static readonly List<Moon> Moons = new List<Moon>(new[]
        {
            new Moon { P = new[] { -14, -4, -11 }},
            new Moon { P = new[] { -9, 6, -7 }},
            new Moon { P = new[] { 4, 1, 4 }},
            new Moon { P = new[] { 2, -14, -9 }}
        });

        static void Main(string[] args)
        {
            var pairs = (new Combinations<int>(Enumerable.Range(0, Moons.Count).ToArray(), 2)).Select(s =>  (m1: Moons[s[0]], m2: Moons[s[1]])).ToList();
            var pLength = Moons[0].P.Length;

            var pi = Enumerable.Range(0, pLength).Select(s => Moons.Select(p => p.P[s]).ToArray()).ToArray();
            var vi = new[] { 0, 0, 0, 0 };

            var periods = new[]{ 0, 0, 0};

            var step = 1;
            //while (step <= MaxSteps)
            while (periods.Count(p => p > 0) < 3)
            {
                Calculate(pairs);

                foreach(var index in Enumerable.Range(0, pLength))
                {
                    if (periods[index] > 0) continue;

                    if (pi[index].SequenceEqual(Moons.Select(s => s.P[index]).ToArray()) && vi.SequenceEqual(Moons.Select(s => s.V[index]).ToArray()))
                    {
                        periods[index] = step;
                    }
                }

                step++;
            }

            //var totalEnergy = Moons.Sum(m => m.P.Sum(Math.Abs) * m.V.Sum(Math.Abs));
            //Console.WriteLine($"Total Energy: {totalEnergy}");

            // D10P2
            var lcm = ((periods[0] / GetGCD(periods[0], periods[1])) * periods[1]);
            var total = (lcm / GetGCD(lcm, periods[2])) * periods[2];

            Console.WriteLine($"Total: {total}");
        }

        static void Calculate(List<(Moon m1, Moon m2)> pairs)
        {
            // velocity
            foreach (var (m1, m2) in pairs)
            {
                foreach (var ip in Enumerable.Range(0, m1.P.Length))
                {
                    var vp = m1.P[ip] < m2.P[ip] ? -1 : m1.P[ip] > m2.P[ip] ? 1 : 0;
                    m1.V[ip] -= vp;
                    m2.V[ip] += vp;
                }
            }
            //position
            foreach (var moon in Moons)
            {
                foreach (var ip in Enumerable.Range(0, moon.P.Length))
                {
                    moon.P[ip] += moon.V[ip];
                }
                moon.T++;
            }
        }

        static long GetGCD(long a, long b)
        {
            //Short cut to handling 0 case
            if (a == 0 || b == 0)
            {
                return a == 0 ? b : a;
            }

            if (b > a)
            {
                (a, b) = (b, a);
            }

            while (b != 0)
            {
                (a, b) = (b, a % b);
            }

            return a;
        }
    }

    public class Moon
    {
        public int[] P { get; set; }
        public int[] V { get; set; } = {0, 0, 0};
        public int T { get; set; }
    }
}
