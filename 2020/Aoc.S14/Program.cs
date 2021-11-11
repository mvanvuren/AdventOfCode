using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aoc.S14
{
    class Program
    {
        static void Main(string[] args)
        {
            var memory = new Dictionary<long, long>();

            //(long or, long and) mask = (0, 0);
            //foreach (var line in File.ReadAllLines("input.txt").Select(l => l.Replace(" ", string.Empty)))
            //{
            //    var ls = line.Split("=");
            //    if (ls[0].StartsWith("mask"))
            //    {
            //        mask = (Convert.ToUInt64(ls[1].Replace("X", "0"), 2), Convert.ToUInt64(ls[1].Replace("X", "1"), 2));
            //    }
            //    else
            //    {
            //        var address = int.Parse(ls[0].Substring(4).TrimEnd(']'));
            //        var value = Convert.ToUInt64(ls[1]);
            //        memory[address] = (value | mask.or) & mask.and;
            //    }
            //}

            //var sum = memory.Values.Aggregate(0UL, (current, value) => current + value);

            var permutations = new List<long>();
            var positions = new List<int>();
            var mask = string.Empty;
            foreach (var line in File.ReadAllLines("input.txt").Select(l => l.Replace(" ", string.Empty)))
            {
                var ls = line.Split("=");
                if (ls[0].StartsWith("mask"))
                {
                    mask = ls[1];
                    positions = GetFloatingPositions(mask);
                    permutations = GetPermutations(mask);
                }
                else
                {
                    var address = long.Parse(ls[0].Substring(4).TrimEnd(']'));
                    //var value = GetMaskedValue(mask, Convert.ToInt64(ls[1]));
                    var value = Convert.ToInt64(ls[1]);


                    var newAddress = new StringBuilder(ToBinary(address));
                    foreach (var p in positions)
                    {
                        newAddress[p] = '0';
                    }
                    address = Convert.ToInt64(newAddress.ToString(), 2);

                    foreach(var p in permutations)
                    {
                        memory[address | p] = value;
                    }
                }
            }

            var sum = memory.Values.Aggregate(0L, (current, value) => current + value);
        }

        static long GetMaskedValue(string mask, long value)
        {
            Dump(value);
            return (value | Convert.ToInt64(mask.Replace("X", "0"), 2)) & Convert.ToInt64(mask.Replace("X", "1"), 2);
        }

        static void Dump(long value)
        {
            Console.WriteLine(ToBinary(value));
        }

        static string ToBinary(long value)
        {
            var svalue = Convert.ToString(value, 2);

            return new string('0', 36 - svalue.Length) + svalue;
        }

        static List<int> GetFloatingPositions(string input)
        {
            var positions = new List<int>();
            var index = 0;
            foreach (var c in input)
            {
                if (c == 'X') positions.Add(index);
                index++;
            }

            return positions;
        }

        static List<long> GetPermutations(string input)
        {
            var positions = GetFloatingPositions(input);

            var masks = new List<long>();

            var sb = new StringBuilder(input);
            var max = 2 << positions.Count - 1;
            for (var i = 0; i < max; i++)
            {
                uint bitMask = 1;
                foreach (var p in positions)
                {
                    sb[p] = (i & bitMask) == 0 ? '0' : '1';
                    bitMask <<= 1;
                }
                masks.Add(Convert.ToInt64(sb.ToString(), 2));
            }

            return masks;
        }
    }
}
