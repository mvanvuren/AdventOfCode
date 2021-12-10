using System;
using System.Collections.Generic;
using System.IO;

namespace Aoc.S10
{
    class Program
    {
        static void Main()
        {
            var parser = new Stack<char>();
            ulong score = 0;
            var points = new Dictionary<char, ulong> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            var match = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
            foreach (var line in File.ReadAllLines("data.txt"))
            {
                foreach (var c in line)
                {
                    if (c is ')' or ']' or '}' or '>')
                    {
                        var mc = parser.Peek();
                        if (c != mc)
                        {
                            Console.WriteLine($"{line} - Expected {mc}, but found {c} instead.");
                            score += points[c];
                            parser.Clear();
                            break;
                        }
                        parser.Pop();
                    }
                    else
                    {
                        parser.Push(match[c]);
                    }
                }
            }
            Console.WriteLine(score); // S10.1: 294195
        }
    }
}
