using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.S16
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var parts = input.Split(Environment.NewLine + Environment.NewLine);
            var regex = new Regex(@"\d+", RegexOptions.Compiled);
            //var ruleRanges = new List<RuleRange>();
            var rules = new List<Rule>();
            var position2 = 0;
            foreach (var rule in parts[0].Split(Environment.NewLine))
            {
                var matches = regex.Matches(rule);
                rules.Add(new Rule
                {
                    Name = rule.Split(':')[0],
                    Index = position2++,
                    RuleRange1 = new RuleRange
                    {
                        Start = int.Parse(matches[0].Value),
                        Stop = int.Parse(matches[1].Value)
                    },
                    RuleRange2 = new RuleRange
                    {
                        Start = int.Parse(matches[2].Value),
                        Stop = int.Parse(matches[3].Value)
                    }
                });
                //ruleRanges.Add(new RuleRange
                //{
                //    Start =  int.Parse(matches[0].Value),
                //    Stop = int.Parse(matches[1].Value)
                //});
                //ruleRanges.Add(new RuleRange
                //{
                //    Start = int.Parse(matches[2].Value),
                //    Stop = int.Parse(matches[3].Value)
                //});
            }

            //// S1 - 19060
            //var reducesRulesRanges = new List<RuleRange>();
            //var orderedRules = ruleRanges.OrderBy(r => r.Start).ToList();
            //var start = orderedRules[0].Start;
            //var stop = orderedRules[0].Stop;
            //foreach (var rule in orderedRules.Skip(1))
            //{
            //    if (stop < rule.Start)
            //    {
            //        reducesRulesRanges.Add(new RuleRange
            //        {
            //            Start = start,
            //            Stop = stop
            //        });
            //        start = rule.Start;
            //    }
            //    stop = rule.Stop;
            //}

            //reducesRulesRanges.Add(new RuleRange
            //{
            //    Start = start,
            //    Stop = stop
            //});



            var start = 29;
            var stop = 949;
            var tickets = new List<List<int>>();
            foreach (var ticket in parts[2].Split(Environment.NewLine).Skip(1))
            {
                var ticketValues = ticket.Split(',').Select(v => int.Parse(v)).ToList();
                if (ticketValues.All(value => value >= start && value <= stop))
                {
                    tickets.Add(ticketValues);
                }
            }

            var rs = Enumerable.Range(0, rules.Count).ToList();
            var validRules = rs.Select(i => Enumerable.Range(0, rules.Count).ToList()).ToList();

            var index = 0;
            while (index < rules.Count)
            {
                foreach (var ticketValues in tickets)
                {
                    var value = ticketValues[index];
                    foreach (var rule in rules)
                    {
                        if (!validRules[index].Contains(rule.Index)) continue;
                        if ((value >= rule.RuleRange1.Start && value <= rule.RuleRange1.Stop)
                            || (value >= rule.RuleRange2.Start && value <= rule.RuleRange2.Stop))
                            continue;
                        validRules[index].Remove(rule.Index);
                    }
                }
                index++;
            }

            var positions = new int[validRules.Count];
            var position = 0;
            while (position < validRules.Count)
            {
                for (position2 = 0; position2 < validRules.Count; position2++)
                {
                    if (validRules[position2].Count == 1)
                    {
                        positions[position2] = validRules[position2][0];
                        foreach (var validRule in validRules)
                        {
                            if (validRule.Count > 1) validRule.Remove(validRules[position2][0]);
                        }
                    }
                }
                position++;
            }

            // TODO: reverse lookup in positions to find columns for first 6 rules

            //var departureRules = rules.Where(r => r.Name.Contains("departure")).ToList();
            long answer = 103L * 59 * 193 * 97 * 101 * 83;
        }
    }

    public class Rule
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public RuleRange RuleRange1 { get; set; }
        public RuleRange RuleRange2 { get; set; }
    }


    public class RuleRange
    {
        public int Start { get; set; }
        public int Stop { get; set; }
    }
}
