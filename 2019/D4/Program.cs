using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D4
{
    class Program
    {
        static void Main(string[] args)
        {
            bool IsValid(string password)
            {
                if (password != new string(password.OrderBy(c => c).ToArray())) return false;

                var pairs = Regex.Matches(password, @"(.)\1*").OfType<Match>().Select(m => m.Value).ToArray();

                return pairs.Count(p => p.Length == 2) > 0;
            }

            var count = 0;
            for (var i = 206938; i <= 679128; i++)
            {
                if (!IsValid(i.ToString())) continue;

                count++;
                //Console.WriteLine(i);
            }
            Console.WriteLine(count);
        }
    }
}
