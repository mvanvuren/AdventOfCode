using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S9
{
    class Program
    {
        static void Main()
        {
            var board = File.ReadAllLines("data.txt").Select(l => l.Select(c => (byte)(c - '0')).ToArray())
                .ToArray();
            ulong riskLevel = 0;
            for (var x = 0; x < board.Length; x++)
            {
                for (var y = 0; y < board[x].Length; y++)
                {
                    riskLevel += CalculateRiskLevel(board, x, y);
                }
            }
            Console.WriteLine(riskLevel); // S9.1: 439
        }

        static byte CalculateRiskLevel(IReadOnlyList<byte[]> board, int x, int y)
        {
            var p = board[x][y];
            
            if ((x > 0 && board[x - 1][y] <= p)
                || (x < board.Count - 1 && board[x + 1][y] <= p)
                || (y > 0 && board[x][y - 1] <= p)
                || (y < board[x].Length - 1 && board[x][y + 1] <= p)) return 0;

            return (byte)(p + 1);
        }
    }
}
