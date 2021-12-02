using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // S2.1/2
            var commands = new List<Command>(File.ReadAllLines("data.txt").Select(s => s.ParseCommand()));
            var position = new Position();

            foreach (var command in commands)
            {
                switch(command.Move) {
                    case Moves.Forward:
                        position.Horizontal += command.Count;
                        position.Vertical += position.Aim * command.Count; // S2.2
                        break;
                    case Moves.Down:
                        // position.Vertical += command.Count; // S2.1
                        position.Aim += command.Count;
                        break;
                    case Moves.Up:
                        // position.Vertical -= command.Count; // S2.1
                        position.Aim -= command.Count;
                        break;
                }
            }

            Console.WriteLine(position.Horizontal * position.Vertical); // S2.1: 1451208

        }
    }

    public class Position 
    {
        public int Horizontal { get; set; }
        
        public int Vertical { get; set; }

        public int Aim { get; set; }
    }

    public static class Extensions 
    {
        public static Command ParseCommand(this string s) 
        {
            var parts = s.Split(' ');

            return new Command {
                Move = (Moves)Enum.Parse(typeof(Moves), parts[0], true),
                Count = int.Parse(parts[1])
            };
        }
    }

    public class Command
    {
        public Moves Move { get; set; }
        public int Count { get; set; }
    }

    public enum Moves {
        Forward,
        Down,
        Up
    }
}
