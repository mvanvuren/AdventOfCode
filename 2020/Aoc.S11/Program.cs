using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").ToList();
            var input = new char[lines.Count, lines[0].Length];
            for (var row = 0; row < lines.Count; row++)
            {
                for (var col = 0; col < lines[0].Length; col++)
                {
                    input[row, col] = lines[row][col];
                }
            }


            while (true)
            {
                input = Calculate(input);
                //Dump(input);
            }
        }

        static void Dump(char[,] input)
        {
            var rowCount = input.GetLength(0);
            var columnCount = input.GetLength(1);

            Console.WriteLine();
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < columnCount; col++)
                {
                    Console.Write(input[row, col]);
                }
                Console.WriteLine();
            }
        }

        static char[,] Calculate(char[,] input)
        {
            var rowCount = input.GetLength(0);
            var columnCount = input.GetLength(1);
            var changeCount = 0;
            var occupiedTotalCount = 0;
            var output = new char[rowCount, columnCount];
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < columnCount; col++)
                {
                    var status = input[row, col];
                    if (status != '.')
                    {
                        var occupiedCount = GetOccupiedCount2(input, row, col);

                        if (status == 'L' && occupiedCount == 0)
                        {
                            status = '#';
                            changeCount++;
                        }
                        else if (status == '#' && occupiedCount >= 5)
                        {
                            status = 'L';
                            changeCount++;
                        }
                        if (status == '#') occupiedTotalCount++;
                    }

                    output[row, col] = status;
                }
            }

            if (changeCount == 0)
            {
                Console.WriteLine("OK");
            }

            return output;
        }

        static int GetOccupiedCount(char[,] input, in int row, in int col)
        {
            var rowCount = input.GetLength(0);
            var columnCount = input.GetLength(1);
            var offsets = new int[] { -1, 0, 1 };
            var occupieCount = 0;

            foreach (var i in offsets)
            {
                if (row + i < 0 || row + i >= rowCount) continue;

                foreach (var j in offsets)
                {
                    if (i == 0 && j == 0) continue;
                    if (col + j < 0 || col + j >= columnCount) continue;
                    if (input[row + i, col + j] == '.') continue;
                    if (input[row + i, col + j] == '#') occupieCount++;
                }
            }

            return occupieCount;
        }

        static int GetOccupiedCount2(char[,] input, in int row, in int col)
        {
            var rowCount = input.GetLength(0);
            var columnCount = input.GetLength(1);
            var offsets = new int[] {-1, 0, 1};
            var occupieCount = 0;

            foreach (var i in offsets)
            {
                foreach (var j in offsets)
                {
                    if (i == 0 && j == 0) continue;

                    var d = 0;
                    while(true)
                    {
                        d++;

                        var newRow = row + d * i;
                        if (newRow < 0 || newRow >= rowCount) break;
                        var newCol = col + d * j;
                        if (newCol < 0 || newCol >= columnCount) break;

                        var status = input[newRow, newCol];
                        if (status == '.') continue;
                        if (status == '#')
                        {
                            occupieCount++;
                        }
                        break;
                    }
                }
            }

            return occupieCount;
        }
    }
}
