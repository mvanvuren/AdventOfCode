using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Aoc.S7
{
    public class Program
    {
        static Dictionary<string, Bag> _bags = new Dictionary<string, Bag>();

        public static void Main(string[] args)
        {
            // dark maroon bags contain 2 striped silver bags, 4 mirrored maroon bags, 5 shiny gold bags, 1 dotted gold bag.
            var regex = new Regex(@"(\w+\s\w+)\sbags\scontain(,{0,1}\s((\d+)\s(\w+\s\w+))\sbags{0,1})+\.", RegexOptions.Compiled);
            foreach (var line in File.ReadAllLines("data.txt"))
            {
                var matches = regex.Matches(line);
                if (matches.Count == 0) continue;

                var bag = GetBag(matches[0].Groups[1].Value);
                
                Console.WriteLine(bag.Name);
                for (var index = 0; index < matches[0].Groups[4].Captures.Count; index++)
                {
                    var childBag = GetBag(matches[0].Groups[5].Captures[index].Value);
                    if (!bag.ChildBags.ContainsKey(childBag.Name))
                    {
                        bag.ChildBags.Add(childBag.Name, childBag);
                        bag.ChildBagCount.Add(childBag.Name, int.Parse(matches[0].Groups[4].Captures[index].Value));
                    }

                    if (!childBag.ParentBags.ContainsKey(bag.Name))
                    {
                        childBag.ParentBags.Add(bag.Name, bag);
                    }
                    Console.WriteLine($"\t{matches[0].Groups[4].Captures[index].Value} {childBag.Name}");
                }
            }

            //var count = GetParentCount(GetBag("shiny gold")); // S7-1: 144
            var count = GetChildCount(GetBag("shiny gold"));
        }

        static long GetChildCount(Bag rootBag)
        {
            var count = 0L;
            foreach (var bag in rootBag.ChildBags.Values)
            {
                var c = rootBag.ChildBagCount[bag.Name];
                count += c + c * GetChildCount(bag);
            }

            return count;
        }

        static int GetParentCount(Bag rootBag)
        {
            var traversedBags = new List<string>(rootBag.ParentBags.Keys);
            var index = 0;
            while (index < traversedBags.Count)
            {
                foreach (var bag in GetBag(traversedBags[index]).ParentBags)
                {
                    if (!traversedBags.Contains(bag.Key))
                    {
                        traversedBags.Add(bag.Key);
                    }
                }
                index++;
            }

            return traversedBags.Count;
        }

        public static Bag GetBag(string name)
        {
            if (_bags.ContainsKey(name))
            {
                return _bags[name];
            }

            return _bags[name] = new Bag
            {
                Name = name
            };
        }
    }

    public class Bag
    {
        public string Name { get; set; }
        public Dictionary<string, Bag> ParentBags { get; set; } = new Dictionary<string, Bag>();
        public Dictionary<string, Bag> ChildBags { get; set; } = new Dictionary<string, Bag>();
        public Dictionary<string, int> ChildBagCount { get; set; } = new Dictionary<string, int>();
    }
}
