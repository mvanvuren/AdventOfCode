using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S3
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<ushort>(File.ReadAllLines("data.txt").Select(s => (ushort)Convert.ToInt16(s, fromBase: 2)));
            var ogrNumbers = new List<ushort>(numbers);
            var csrNumbers = new List<ushort>(numbers);

            var maximumNumber = numbers.Max();
            ushort flag = 1;
            while ((maximumNumber >>= 1) > 0) flag <<= 1;
            ushort gamma = 0;
            ushort epsilon = 0;
            while (flag > 0)
            {
                var numberOfOnes = numbers.Count(n => (n & flag) == flag);
                if (numberOfOnes << 1 < numbers.Count)
                { // more 0's
                    epsilon |= flag;
                }
                else
                { // more 1's (or equal)
                    gamma |= flag;
                }
                // S3.2
                if (ogrNumbers.Count > 1)
                {
                    numberOfOnes = ogrNumbers.Count(n => (n & flag) == flag);
                    ogrNumbers = ogrNumbers.Where(n => (n & flag) == (numberOfOnes << 1 < ogrNumbers.Count ? 0 : flag)).ToList();
                }
                if (csrNumbers.Count > 1)
                {
                    numberOfOnes = csrNumbers.Count(n => (n & flag) == flag);
                    csrNumbers = csrNumbers.Where(n => (n & flag) == (numberOfOnes << 1 < csrNumbers.Count ? flag : 0)).ToList();
                }


                flag >>= 1;
            }

            Console.WriteLine((uint)gamma * epsilon); // S3.1: 3847100
            Console.WriteLine((uint)ogrNumbers[0] * csrNumbers[0]); // S3.2: 4105235
        }
    }
}
