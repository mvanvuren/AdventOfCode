using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.S4
{
    class Program
    {
        const byte BoardSize = 5;

        static void Main()
        {
            using var reader = new StreamReader("data.txt");

            var line = reader.ReadLine();
            if (line == null) throw new Exception();

            var numbers = line.Split(',').Select(byte.Parse).ToList();
            var boards = new List<byte[,]>();
            var numberPositions = new Dictionary<int, List<Postion>>();
            foreach (var n in Enumerable.Range(0, 100))
            {
                numberPositions[n] = new List<Postion>();
            }
            
            while (reader.ReadLine() != null)
            {
                var board = new byte[BoardSize, BoardSize];
                byte row = 0;
                while (row < BoardSize)
                {
                    line = reader.ReadLine();
                    if (line == null) throw new Exception();

                    byte column = 0;
                    foreach (var number in Regex.Split(line.TrimStart(), @"\s+").Select(byte.Parse))
                    {
                        board[row, column] = number;

                        numberPositions[number].Add(new Postion { BoardIndex = boards.Count, Row = row, Column = column});

                        column++;
                    }
                    row++;
                }

                boards.Add(board);
            }

            var bingoBoards = new List<int>();

            var numberIndex = 0;
            foreach (var number in numbers)
            {
                foreach (var position in numberPositions[number])
                {
                    if (bingoBoards.Contains(position.BoardIndex)) continue; // bingo board
                    
                    var board = boards[position.BoardIndex];
                    board[position.Row, position.Column] = 255;
                    if (numberIndex < BoardSize - 1) continue; // first 4 numbers no bingo possible

                    if (!IsBingoBoard(board, position.Row, position.Column)) continue;

                    bingoBoards.Add(position.BoardIndex);

                    var sum = SumBoard(board);

                    Console.WriteLine($"board: {position.BoardIndex + 1}, number: {number}, result: {sum * number}");
                }

                numberIndex++;
            }
        }

        static uint SumBoard(byte[,] board)
        {
            return (uint)Enumerable.Range(0, BoardSize).Sum(c =>
                Enumerable.Range(0, BoardSize).Sum(r => (uint)board[(byte)r, (byte)c] != 255 ? (uint)board[(byte)r, (byte)c] : 0));
        }

        static bool IsBingoBoard(byte[,] board, byte row, byte column)
        {
            // vertical
            if (IsBingoLine(board, 0, column, 1, 0)) return true;

            // horizontal
            if (IsBingoLine(board, row, 0, 0, 1)) return true;
            
            return false;
        }

        static bool IsBingoLine(byte[,] board, byte row, byte column, sbyte dr, sbyte dc)
        {
            byte count = 0;
            while (row < BoardSize && column < BoardSize)
            {
                if (board[row, column] != 255) break;
                
                count++;
                
                row = (byte)(row + dr);
                column = (byte)(column + dc);
            }

            return count == BoardSize;
        }
    }
    public class Postion
    {
        public int BoardIndex { get; set; }

        public byte Row { get; set; }

        public byte Column { get; set; }
    }
}
