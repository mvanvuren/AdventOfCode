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

        static void Main(string[] args)
        {
            using var reader = new StreamReader("data.txt");

            var line = reader.ReadLine();
            if (line == null) throw new Exception();

            var numbers = line.Split(',').Select(byte.Parse).ToList();
            var boards = new List<byte[,]>();
            var numbersToBoards = new Dictionary<int, List<Postion>>();
            foreach (var n in Enumerable.Range(0, 100))
            {
                numbersToBoards[n] = new List<Postion>();
            }
            
            while ((line = reader.ReadLine()) != null)
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

                        numbersToBoards[number].Add(new Postion { BoardIndex = boards.Count, Row = row, Column = column});

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
                foreach (var position in numbersToBoards[number])
                {
                    if (bingoBoards.Contains(position.BoardIndex)) continue;
                    
                    var board = boards[position.BoardIndex];
                    board[position.Row, position.Column] = 0;
                    if (numberIndex < BoardSize - 1) continue;

                    var isBingo = IsBingoBoard(board, position.Row, position.Column);
                    if (isBingo)
                    {
                        bingoBoards.Add(position.BoardIndex);

                        uint sum = 0;
                        byte row = 0;
                        while (row < BoardSize)
                        {
                            byte column = 0;
                            while (column < BoardSize)
                            {
                                sum += board[row, column];
                                column++;
                            }

                            row++;
                        }
                        
                        Console.WriteLine($"board: {position.BoardIndex + 1}, number: {number}, result: {sum * number}");
                    }
                }

                numberIndex++;
            }
        }

        static bool IsBingoBoard(byte[,] board, byte row, byte column)
        {
            byte r;
            byte c;
            byte cc;

            if (row == column)
            { // diagonal from top left to bottom right
                r = 0;
                c = 0;
                cc = 0;
                while (r < BoardSize)
                {
                    if (board[r, c] > 0) break;
                    cc++;
                    r++;
                    c++;
                }
                if (cc == BoardSize) return true;
            }

            if (row + column == BoardSize - 1)
            { // diagonal from bottom left to top right
                r = BoardSize - 1;
                c = 0;
                cc = 0;
                while (r < BoardSize)
                {
                    if (board[r, c] > 0) break;
                    cc++;
                    r++;
                    c++;
                }
                if (cc == BoardSize) return true;
            }

            // vertical
            r = 0;
            c = column;
            cc = 0;
            while (r < BoardSize)
            {
                if (board[r, c] > 0) break;
                cc++;
                r++;
            }
            if (cc == BoardSize) return true;

            // horizontal
            r = row;
            c = 0;
            cc = 0;
            while (c < BoardSize)
            {
                if (board[r, c] > 0) break;
                cc++;
                c++;
            }
            if (cc == BoardSize) return true;

            return false;
        }
    }
    public class Postion
    {
        public int BoardIndex { get; set; }

        public byte Row { get; set; }

        public byte Column { get; set; }
    }
}
