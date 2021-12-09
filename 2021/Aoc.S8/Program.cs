//   a b c d e f g
//   =============
//0  a b c   e f g   6
//1      c     f     2
//2  a   c d e   g   5
//3  a   c d   f g   5
//4    b c d   f     4
//5  a b   d   f g   5
//6  a b   d e f g   6
//7  a   c     f     3
//8  a b c d e f g   7
//9  a b c d   f g   6
//   =============
//   8 6 8 7 4 9 7
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S8
{
    class Program
    {
        static void Main()
        {
            var signalsAndDigits = File.ReadAllLines("data.txt")
                .Select(s => s.Replace(" | ", " ").Split(" ").Select(p => String.Concat(p.OrderBy(c => c))).ToList()).ToList();

            // S8.1
            //var count = signalsAndDigits.Sum(l => l.Skip(10).Count(c => c.Length is 2 or 3 or 4 or 7));
            //Console.WriteLine(count); // 367

            // S8.2
            var totalSum = 0;
            foreach (var sad in signalsAndDigits)
            {
                var signals = sad.Take(10).OrderBy(c => c.Length).ToList();

                var map = CreateMap(signals);

                var digits = BuildDigits(map, signals);
                
                var sum = sad.Skip(10).Aggregate(0, (current, d) => 10 * current + digits.IndexOf(d));
                
                totalSum += sum;
            }

            Console.WriteLine(totalSum);
        }

        static List<string> BuildDigits(IReadOnlyDictionary<char, char> map, IReadOnlyList<string> signals)
        {
            var digits = new List<string>(new string[10])
            {
                [0] = MapSignal(map, "abcefg"),
                [1] = signals[0],
                [2] = MapSignal(map, "acdeg"),
                [3] = MapSignal(map, "acdfg"),
                [4] = signals[2],
                [5] = MapSignal(map, "abdfg"),
                [6] = MapSignal(map, "abdefg"),
                [7] = signals[1],
                [8] = signals[9],
                [9] = MapSignal(map, "abcdfg")
            };
            return digits;
        }

        static Dictionary<char, char> CreateMap(IReadOnlyList<string> signals)
        {
            var frequency = CreateFrequencyTable(signals);
            var frequencySub = CreateFrequencyTable(signals.Take(3));

            var e = (char)((byte)'a' + frequency.IndexOf(4));
            var b = (char)((byte)'a' + frequency.IndexOf(6));
            var f = (char)((byte)'a' + frequency.IndexOf(9));
            var c = signals[0].Replace(f, ' ').Trim()[0]; // TODO
            var a = (char)((byte)'a' + frequency.Select((freq, index) => new { freq, index })
                .First(p => p.freq == 8 && p.index != c - (byte)'a').index);

            var g = (char)((byte)'a' + frequencySub.Select((freq, index) => new { freq, index })
                .First(p => p.freq == 0 && p.index != e - (byte)'a').index);
            var d = (char)((byte)'a' + frequencySub.Select((freq, index) => new { freq, index })
                .First(p => p.freq == 1 && p.index != a - (byte)'a' && p.index != b - (byte)'a').index);

            var map = new Dictionary<char, char>(Enumerable.Range(0, 7)
                .Select(ch => new KeyValuePair<char, char>((char)((byte)'a' + ch), ' ')))
            {
                ['a'] = a,
                ['b'] = b,
                ['c'] = c,
                ['d'] = d,
                ['e'] = e,
                ['f'] = f,
                ['g'] = g
            };
            return map;
        }

        static string MapSignal(IReadOnlyDictionary<char, char> map, string signal)
        {
            return string.Concat(signal.Select(c => map[c]).OrderBy(c => c));
        }

        static List<byte> CreateFrequencyTable(IEnumerable<string> signals)
        {
            var frequency = new List<byte>(new byte[(byte)'g' - (byte)'a' + 1]);
            foreach (var c in signals.SelectMany(signal => signal))
            {
                frequency[c - (byte)'a']++;
            }

            return frequency;
        }
    }
}
