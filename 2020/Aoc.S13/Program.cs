using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc.S13
{
    class Program
    {
        static void Main(string[] args)
        {
            //var lines = File.ReadAllLines("input.txt");
            //var ets = int.Parse(lines[0]);
            //var ids = lines[1].Split(',').Where(id => id != "x").Select(id => int.Parse(id)).ToArray();

            //var timetable = ids.Select(id => new Bus { Id = id, FirstTime = ets + id - (ets % id)}).ToList();
            //var bus = timetable.Single(t => t.FirstTime == timetable.Min(ft => ft.FirstTime));

            //var answer = bus.Id * (bus.FirstTime - ets);

            var lines = File.ReadAllLines("input.txt");

            FindSubsequentBusDepartures(lines);

            ////var ets = int.Parse(lines[0]);
            //var ids = lines[1].Split(',').Select(id => int.Parse(id == "x" ? "-1" : id)).ToArray();
            ////var m = 1L;
            //var m = 100000000000000L / ids[0];
            //var index = 0;
            //long t = 0;
            //while (index < ids.Length)
            //{
            //    var id = ids[index];

            //    if (index == 0)
            //    {
            //        t = m * id;
            //    } 
            //    else if (id != -1)
            //    {
            //        if ((t + index) % id != 0)
            //        {
            //            m++;
            //            index = 0;
            //            continue;
            //        }
            //    }
            //    index++;
            //}
        }

        private static void FindSubsequentBusDepartures(string[] data)
        {
            var busData = data[1].Split(",").Select(s => s == "x" ? 0 : uint.Parse(s)).ToArray();
            var departureDiffs = GetDepartureDiffs(busData);
            var busIds = busData.Skip(1).Where(d => d != 0).Select(d => Convert.ToUInt32(d)).ToArray();

            // start at first possible departure which is the first busId departing
            var time = (ulong)busData[0];
            var candidate = time;
            for (var i = 0; i < busIds.Length; i++)
            {
                while (time % busIds[i] != departureDiffs[i])
                {
                    time += candidate;
                }

                // only check numbers that statisfy time % busId == departureDiff (see below)
                // which is the LCM of the first matching time and the busId
                candidate = Convert.ToUInt64(FindLCM(candidate, busIds[i]));
            }

            Console.WriteLine($"(2) Earliest timestamp for subsequent departures: {time}");
        }

        // result contains time % busId results that match a given offset from the first ids departure
        // e.g.: id 13 departs i minutes later than the first, then a time candidate t must statisfy t % 13 = 13 - i
        private static uint[] GetDepartureDiffs(uint[] busData)
        {
            var diffs = new List<uint>();
            for (var i = 1U; i < busData.Length; i++)
            {
                if (busData[i] != 0)
                {
                    diffs.Add(busData[i] - i % busData[i]);
                }
            }

            return diffs.ToArray();
        }

        private static ulong FindLCM(ulong a, ulong b)
        {
            return a * b / FindGCD(a, b);
        }

        private static ulong FindGCD(ulong a, ulong b)
        {
            var h = 0UL;
            while (b != 0)
            {
                h = a % b;
                a = b;
                b = h;
            }

            return a;
        }
    }

    //public class Bus
    //{
    //    public int Id { get; set; }
    //    public int FirstTime { get; set; }
    //}
}
