using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S15
{
    class Program
    {
        static void Main(string[] args)
        {
            var spoken = File.ReadAllText("input-test.txt").Split(',').Select(v => int.Parse(v)).ToList();
            var memory = new Dictionary<int, List<int>>();
            var index = 0;
            while (index < spoken.Count)
            {
                memory.Add(spoken[index], new List<int>(new [] {index}));
                index++;
            }

            var lastNumber = spoken[spoken.Count - 1];
            while (index < 30000000)
            {
                if (memory[lastNumber].Count == 1) // first time spoken
                {
                    lastNumber = 0;
                }
                else
                {
                    lastNumber = memory[lastNumber][memory[lastNumber].Count - 1] - memory[lastNumber][memory[lastNumber].Count - 2];
                }

                spoken.Add(lastNumber);
                if (!memory.ContainsKey(lastNumber))
                {
                    memory.Add(lastNumber, new List<int>(new[] { index }));
                }
                else
                {
                    memory[lastNumber].Add(index);
                }

                index++;
            }

            Console.WriteLine(spoken[spoken.Count - 1]);
        }
    }
}
