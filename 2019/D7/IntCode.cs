using System;
using System.Linq;

namespace D7
{
    public class IntCode
    {
        public int IntCodeId { get; set; }

        readonly int[] _memory;
        int _ip;
        int _modeP1;
        int _modeP2;
        int _modeP3;
        int _operation;

        readonly Func<int, int> _inputHandler;
        readonly Func<int, int, int> _outputHandler;

        public IntCode(
            int intCodeId
            , int[] program
            , Func<int, int> inputHandler
            , Func<int, int, int> outputHandler)
        {
            IntCodeId = intCodeId;

            _memory = program.ToArray();

            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }

        public void Execute()
        {
            int Get(int mode, int index)
            {
                if (mode == 0) return _memory[_memory[_ip + index]];
                if (mode == 1) return _memory[_ip + index];

                throw new Exception($"HALT: mode {mode} not supported");
            }

            void Set(int mode, int index, int value)
            {
                if (mode == 0)
                {
                    _memory[_memory[_ip + index]] = value;
                    return;
                }
                if (mode == 1)
                {
                    _memory[_ip + index] = value;
                    return;
                }

                throw new Exception($"HALT: mode {mode} not supported");
            }

            void Add()
            {
                Set(_modeP3, 3, Get(_modeP1, 1) + Get(_modeP2, 2));
                _ip += 4;
            }

            void Multiply()
            {
                Set(_modeP3, 3, Get(_modeP1, 1) * Get(_modeP2, 2));
                _ip += 4;
            }

            void Input()
            {
                var value = _inputHandler(IntCodeId);

                Set(_modeP1, 1, value);
                _ip += 2;
            }

            void Output()
            {
                var value = Get(_modeP1, 1); _ip += 2;
                var unused = _outputHandler(IntCodeId, value);
            }

            void JumpIfTrue()
            {
                if (Get(_modeP1, 1) != 0)
                {
                    _ip = Get(_modeP2, 2);
                }
                else
                {
                    _ip += 3;
                }
            }

            void JumpIfFalse()
            {
                if (Get(_modeP1, 1) == 0)
                {
                    _ip = Get(_modeP2, 2);
                }
                else
                {
                    _ip += 3;
                }
            }

            void IsLessThan()
            {
                Set(_modeP3, 3, Get(_modeP1, 1) < Get(_modeP2, 2) ? 1 : 0);
                _ip += 4;
            }

            void IsEqual()
            {
                Set(_modeP3, 3, Get(_modeP1, 1) == Get(_modeP2, 2) ? 1 : 0);
                _ip += 4;
            }

            do
            {
                var opCode = _memory[_ip];
                _operation = opCode % 100;
                var modes = opCode / 100;
                _modeP1 = (modes % 10);
                _modeP2 = ((modes / 10) % 10);
                _modeP3 = ((modes / 100) % 10);

                switch (_operation)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Multiply();
                        break;
                    case 3:
                        Input();
                        break;
                    case 4:
                        Output();
                        break;
                    case 5:
                        JumpIfTrue();
                        break;
                    case 6:
                        JumpIfFalse();
                        break;
                    case 7:
                        IsLessThan();
                        break;
                    case 8:
                        IsEqual();
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception($"HALT: Opcode {_operation} not supported");
                }

            } while (true);

        }
    }
}
