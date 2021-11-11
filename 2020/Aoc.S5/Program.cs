using System;
using System.IO;
using System.Linq;

namespace Aoc.S5
{
    class Program
    {
        static void Main(string[] args)
        {
            // S5-1: 974
            //var maxSeatId = 0;
            //foreach (var line in File.ReadAllLines("data.txt"))
            //{
            //    var row = Convert.ToInt32(line.Substring(0, 7).Replace('F', '0').Replace('B', '1'), 2);
            //    var column = Convert.ToInt32(line.Substring(7, 3).Replace('L', '0').Replace('R', '1'), 2);
            //    var seatId = row * 8 + column;
            //    if (seatId > maxSeatId) maxSeatId = seatId;
            //}
            //Console.WriteLine(maxSeatId);

            var seatIds = Enumerable.Range(0, 1024).ToList();
            foreach (var line in File.ReadAllLines("data.txt"))
            {
                var row = Convert.ToInt32(line.Substring(0, 7).Replace('F', '0').Replace('B', '1'), 2);
                var column = Convert.ToInt32(line.Substring(7, 3).Replace('L', '0').Replace('R', '1'), 2);
                var seatId = row * 8 + column;
                seatIds.Remove(seatId);
            }
        }
    }
}
