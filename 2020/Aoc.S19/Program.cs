using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aoc.S19
{
    class Program
    {
        static readonly Dictionary<int, List<int>> Rules = new Dictionary<int, List<int>>();
        static readonly List<string> Permutations = new List<string>();

        static void Main(string[] args)
        {
            var l = SplitList(new[] {1, 2, -1, 3, -1, 4}.ToList());

            var fileParts = File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine);
            foreach (var line in fileParts[0].Split(Environment.NewLine))
            {
                var rule = line.Split(":").Select(s => s.Trim()).ToArray();
                Rules.Add(int.Parse(rule[0]), rule[1].StartsWith("\"") 
                    ? new List<int>(new[] { -1 * rule[1][1] }) 
                    : rule[1].Split(' ').Select(s => int.Parse(s == "|" ? "-1" : s)).ToList());
            }

            //Traverse(0);

            //var validMessageCount = 0;
            //var messages = fileParts[1].Split(Environment.NewLine);
            //foreach (var message in messages)
            //{
            //    if (!Permutations.Contains(message)) continue;

            //    validMessageCount++;

            //    Console.WriteLine(message);
            //}
            //Console.WriteLine(validMessageCount);

            Traverse(42);
            var p42 = new List<string>(Permutations);
            Traverse(31);
            var p31 = new List<string>(Permutations);

            //foreach (var permutation in p42)
            //{
            //    Console.WriteLine(permutation);
            //}
            //Console.WriteLine();
            //foreach (var permutation in p31)
            //{
            //    Console.WriteLine(permutation);
            //}

            var validMessageCount = 0;
            var messages = fileParts[1].Split(Environment.NewLine);
            //var ip42 = p42.Intersect(p31).ToList();
            //var ip31 = p31.Intersect(p42).ToList();
            foreach (var message in messages)
            {
                var mp = Split(message, 8).ToList();
                var index = 0;
                var count42 = 0;
                var count31 = 0;
                while (index < mp.Count && p42.Contains(mp[index])) // TODO optimize
                {
                    count42++;
                    index++;
                }
                while (index < mp.Count && p31.Contains(mp[index])) // TODO optimize
                {
                    count31++;
                    index++;
                }
                if (count42 + count31 != mp.Count) continue;
                if (count42 - count31 < 1) continue;
                if (count31 == 0) continue;

                Console.WriteLine(message);
                validMessageCount++;

            }
            Console.WriteLine(validMessageCount);

        }

        static void Traverse(int startRule)
        {
            Permutations.Clear();

            var rules = new List<int>(new[] {startRule});
            var queue = new Queue<List<int>>(new [] {rules});
            while (queue.Count > 0)
            {
                var instance = queue.Dequeue();
                var index = 0;
                while (index < instance.Count)
                {
                    var currentRule = instance[index];
                    if (currentRule < 0)
                    { // already replaced
                        index++;
                        continue;
                    }

                    var replacementRule = Rules[currentRule];
                    if (replacementRule.Count == 1 && replacementRule[0] < 0)
                    {
                        instance[index] = replacementRule[0];
                        index++;
                        continue;
                    }

                    instance.RemoveAt(index);

                    var split = replacementRule.IndexOf(-1);
                    if (split >= 0)
                    {
                        //var splitInstance = new List<int>(instance);
                        //splitInstance.InsertRange(index, replacementRule.Skip(split + 1));
                        //queue.Enqueue(splitInstance);
                        foreach (var list in SplitList(replacementRule).Skip(1))
                        {
                            var splitInstance = new List<int>(instance);
                            splitInstance.InsertRange(index, list);
                            queue.Enqueue(splitInstance);
                        }
                    }

                    instance.InsertRange(index, replacementRule.TakeWhile(r => r != -1));
                }

                Permutations.Add(instance.Aggregate(string.Empty, (current, c) => current + (char)Math.Abs(c)));
            }
        }

        static List<List<int>> SplitList(List<int> list)
        {
            var index = list.IndexOf(-1);
            return index == -1 
                ? new List<List<int>>(new [] { list }) 
                : new List<List<int>>(new [] { list.Take(index).ToList() }).Union(SplitList(list.Skip(index + 1).ToList())).ToList();
        }

        static IEnumerable<string> Split(string value, int chunkSize)
        {
            return Enumerable.Range(0, value.Length / chunkSize)
                .Select(i => value.Substring(i * chunkSize, chunkSize));
        }
    }
}
