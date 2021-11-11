using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Aoc.S4
{
    public class Program
    {
        static string[] keys = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
        static string[] eyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        // S4: Valid passports: 210
        static void Main(string[] args)
        {
            var passport = new Dictionary<string, string>();
            var validPassports = 0L;
            foreach (var line in File.ReadAllLines("data.txt"))
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (IsValidPassport(passport))
                    {
                        validPassports++;
                    }
                    passport.Clear();
                    continue;
                }

                foreach (var kvs in line.Split(' '))
                {
                    var kv = kvs.Split(':');
                    passport.Add(kv[0], kv[1]);
                }
            }
            Console.WriteLine($"Valid passports: {validPassports}");
        }

        public static bool IsValidPassport(Dictionary<string, string> passport)
        {
            //var x = passport.Keys.Except(keys).ToList();
            //if (x.Count > 0)
            //{
            //    Console.WriteLine("O!O!");
            //}

            var exceptKeys = keys.Except(passport.Keys).ToList();
            if (!(exceptKeys.Count == 0 || (exceptKeys.Count == 1 && exceptKeys.Contains("cid"))))
            {
                return false;
            }

            if (!IsWithinRange(1920, 2002, int.Parse(passport["byr"])))
            {
                return false;
            }

            if (!IsWithinRange(2010, 2020, int.Parse(passport["iyr"])))
            {
                return false;
            }

            if (!IsWithinRange(2020, 2030, int.Parse(passport["eyr"])))
            {
                return false;
            }

            if (!IsValidHeight(passport["hgt"]))
            {
                return false;
            }

            if (!IsValidHairColor(passport["hcl"]))
            {
                return false;
            }

            if (!IsValidEyeColor(passport["ecl"]))
            {
                return false;
            }

            if (!IsValidPid(passport["pid"]))
            {
                return false;
            }

            Console.WriteLine(passport["pid"]);

            return true;
        }

        public static bool IsWithinRange(int start, int end, int value)
        {
            return value >= start && value <= end;
        }

        public static bool IsValidHeight(string height)
        {
            if (height.EndsWith("cm"))
            {
                return IsWithinRange(150, 193, int.Parse(height.Substring(0, height.Length - 2)));
            }

            if (height.EndsWith("in"))
            {
                return IsWithinRange(59, 76, int.Parse(height.Substring(0, height.Length - 2)));
            }

            return false;
        }

        public static bool IsValidHairColor(string hairColor)
        {
            if (hairColor.Length != 7)
            {
                return false;
            }

            if (hairColor[0] != '#')
            {
                return false;
            }

            if (!int.TryParse(hairColor.Substring(1), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var result))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidEyeColor(string eyeColor)
        {
            return eyeColors.Contains(eyeColor);
        }

        public static bool IsValidPid(string pid)
        {
            if (pid.Length == 9 && long.TryParse(pid, out var result))
            {
                return true;
            }

            return false;
        }
    }
}
