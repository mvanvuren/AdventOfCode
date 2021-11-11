using System;
using System.Linq;

namespace D2
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] memory = { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
            var p = new[] { 1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 6, 19, 1, 9, 19, 23, 1, 6, 23, 27, 1, 10, 27, 31, 1, 5, 31, 35, 2, 6, 35, 39, 1, 5, 39, 43, 1, 5, 43, 47, 2, 47, 6, 51, 1, 51, 5, 55, 1, 13, 55, 59, 2, 9, 59, 63, 1, 5, 63, 67, 2, 67, 9, 71, 1, 5, 71, 75, 2, 10, 75, 79, 1, 6, 79, 83, 1, 13, 83, 87, 1, 10, 87, 91, 1, 91, 5, 95, 2, 95, 10, 99, 2, 9, 99, 103, 1, 103, 6, 107, 1, 107, 10, 111, 2, 111, 10, 115, 1, 115, 6, 119, 2, 119, 9, 123, 1, 123, 6, 127, 2, 127, 10, 131, 1, 131, 6, 135, 2, 6, 135, 139, 1, 139, 5, 143, 1, 9, 143, 147, 1, 13, 147, 151, 1, 2, 151, 155, 1, 10, 155, 0, 99, 2, 14, 0, 0 };
            
            //// D2P1: 1202 program alarm => 6087827
            //memory[1] = 12;
            //memory[2] = 2;

            //D2P2: 
            for (var parameter1 = 0; parameter1 < 99; parameter1++)
            {
                for (var parameter2 = 0; parameter2 < 99; parameter2++)
                {
                    var memory = p.ToArray();

                    memory[1] = parameter1;
                    memory[2] = parameter2;

                    var ip = 0;
                    int code;

                    while ((code = memory[ip]) != 99)
                    {
                        switch (code)
                        {
                            case 1: // Add
                                memory[memory[ip + 3]] = memory[memory[ip + 1]] + memory[memory[ip + 2]];
                                break;
                            case 2: // Multiply
                                memory[memory[ip + 3]] = memory[memory[ip + 1]] * memory[memory[ip + 2]];
                                break;
                            default:
                                Console.WriteLine("Something is wrong!");
                                //DumpMemory();
                                break;
                        }

                        ip += 4;

                        //if (memory[0] == 19690720)
                        //{
                        //    Console.WriteLine($"{parameter1}, {parameter2}");
                        //}

                    }

                    if (memory[0] == 19690720)
                    {
                        Console.WriteLine($"{parameter1}, {parameter2}");
                    }
                    //DumpMemory();

                    //void DumpMemory()
                    //{
                    //    foreach (var value in memory)
                    //    {
                    //        Console.Write($"{value} ");
                    //    }
                    //    Console.WriteLine();
                    //}
                }
            }
        }
    }
}
