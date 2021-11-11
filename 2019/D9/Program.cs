using System;
using System.IO;
using System.Linq;

namespace D9
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = File.ReadAllText("program.ic").Split(',').Select(Int64.Parse).ToArray();

            var intCode = new IntCode(0, program, InputHandler, OutputHandler);

            intCode.Execute();

            // D9P1: 3839402290

            Int64 InputHandler(int intCodeId)
            {
                Console.Write($"[{intCodeId}] >> ");
                var input = Console.ReadLine();
                if (!Int64.TryParse(input, out var value))
                    throw new ArgumentException($"HALT: input value {input} not a valid integer");

                return value;
            }

            Int64 OutputHandler(int intCodeId, Int64 output)
            {
                Console.WriteLine($"[{intCodeId}] {output}");

                return 0;
            }
        }
    }
}
