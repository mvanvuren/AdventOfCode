using System;
using System.IO;
using System.Linq;
using Combinatorics.Collections;

namespace D7
{
    public class Program
    {
        static int[] _phaseSettings;
        static int[] _outputSignals;

        static InputSwitch _inputSwitch = InputSwitch.PhaseSettings;

        public static void Main()
        {
            var program = File.ReadAllText("program.ic").Split(',').Select(int.Parse).ToArray();

            var maxOutputSignal = int.MinValue;

            //const string ps = "01234"; // D7P1
            const string ps = "56789"; // D7P1

            foreach (var phaseSettings in new Permutations<char>(ps.ToCharArray()))
            {
                Console.Write(new string(phaseSettings.ToArray()) + ": ");

                _phaseSettings = phaseSettings.Select(p => int.Parse(p.ToString())).ToArray();
                _outputSignals = new[] { 0, 0, 0, 0, 0 };

                foreach (var index in Enumerable.Range(0, 5))
                {
                    _inputSwitch = InputSwitch.PhaseSettings;

                    var amplifier = new IntCode(index, program, InputHandler, OutputHandler);

                    amplifier.Execute();
                }

                if (_outputSignals[4] > maxOutputSignal)
                {
                    maxOutputSignal = _outputSignals[4];
                }

                Console.WriteLine(_outputSignals[4]);
            }

            Console.WriteLine($"Max output signal: {maxOutputSignal}"); // D7P1: 42301: 437860

            int InputHandler(int intCodeId)
            {
                var value = 0;

                switch (_inputSwitch)
                {
                    case InputSwitch.PhaseSettings:
                        value = _phaseSettings[intCodeId];
                        _inputSwitch = InputSwitch.InputSignal;
                        break;
                    case InputSwitch.InputSignal:
                        value = intCodeId == 0 ? 0 : _outputSignals[intCodeId];
                        //_inputSwitch = InputSwitch.PhaseSettings;
                        break;
                }

                return value;
            }

            //int InputHandler(int intCodeId)
            //{
            //    //Console.Write($"[{intCodeId}] >> ");
            //    //var input = Console.ReadLine();
            //    //if (!int.TryParse(input, out var value))
            //    //    throw new ArgumentException($"HALT: input value {input} not a valid integer");
            //}

            int OutputHandler(int intCodeId, int output)
            {
                _outputSignals[intCodeId] = output;

                //Console.WriteLine($"[{intCodeId}] {output}");

                return 0;
            }

        }
    }

    public enum InputSwitch
    {
        PhaseSettings,
        InputSignal
    }
}
