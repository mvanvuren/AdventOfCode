using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S8
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("data.txt")
                .Select(l =>
                {
                    var ls = l.Split(" ");
                    return new Instruction
                    {
                        Operation = ls[0],
                        Argument = int.Parse(ls[1]) 
                    };
                }).ToList();

            for (var ip = 0; ip < instructions.Count; ip++)
            {
                if (instructions[ip].Operation == "acc") continue;

                Console.WriteLine($"IP: {ip}");

                var modifiedInstructions = new List<Instruction>();
                instructions.ForEach(i =>
                {
                    modifiedInstructions.Add(new Instruction{Operation =  i.Operation, Argument = i.Argument});
                });
                
                switch (instructions[ip].Operation)
                {
                    case "nop":
                        modifiedInstructions[ip].Operation = "jmp";
                        break;
                    case "jmp":
                        modifiedInstructions[ip].Operation = "nop";
                        break;
                }

                var result = Execute(modifiedInstructions);
                if (result == true)
                {
                    Console.WriteLine("DONE!!!");
                }
            }

            Execute(instructions);
        }

        static bool Execute(IReadOnlyList<Instruction> instructions)
        {
            var ip = 0;
            var ips = new List<int>();
            var accumulator = 0L;
            while (true)
            {
                if (ip >= instructions.Count)
                {
                    Console.WriteLine(accumulator);
                    return true;
                }

                if (ips.Contains(ip))
                {
                    //Console.WriteLine(accumulator); // S8-1: 1744
                    return false;
                }

                ips.Add(ip);
                var instruction = instructions[ip];
                switch (instruction.Operation)
                {
                    case "nop":
                        ip++;
                        break;
                    case "acc":
                        accumulator += instruction.Argument;
                        ip++;
                        break;
                    case "jmp":
                        ip += instruction.Argument;
                        break;
                }
            }
        }
    }

    public class Instruction
    {
        public string Operation { get; set; }
        public int Argument { get; set; }
    }
}
