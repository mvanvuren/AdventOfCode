using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S22
{
    class Program
    {
        static void Main(string[] args)
        {
            var fp = File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine);
            var deck1 = GetDeck(fp[0]);
            var deck2 = GetDeck(fp[1]);

            while (deck1.Count > 0 && deck2.Count > 0)
            {
                var p1 = deck1.Dequeue();
                var p2 = deck2.Dequeue();

                if (p1 > p2)
                {
                    deck1.Enqueue(p1);
                    deck1.Enqueue(p2);
                }
                else
                {
                    deck2.Enqueue(p2);
                    deck2.Enqueue(p1);
                }
            }

            var sum = 0L;
            var deck = deck1.Count > 0 ? deck1 : deck2;
            var multiplier = deck.Count;
            while (multiplier > 0)
            {
                sum += multiplier * deck.Dequeue();
                multiplier--;
            }
            Console.WriteLine(sum);
        }

        static Queue<int> GetDeck(string text)
        {
            var deck = new Queue<int>();
            foreach (var line in text.Split(Environment.NewLine).Skip(1))
            {
                deck.Enqueue(int.Parse(line));
            }

            return deck;
        }
    }
}
