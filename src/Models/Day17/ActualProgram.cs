namespace AOC2024.Models.Day17;

public class ActualProgram
{
    private int[] _program;
    private long _registerA;
    private long _registerB;
    private long _registerC;

    public ActualProgram(int[] program, long registerA, long registerB, long registerC)
    {
        _program = program;
        _registerA = registerA;
        _registerB = registerB;
        _registerC = registerC;
    }

    public int[] Output {get; set;}

    public void RunProgram()
    {
        List<int> outputs = new();
        int outputPointer = 0;
        
        for (int j = 0; j < _program.Length; j+=2)
        {
            long opcode = _program[j];
            long operand = _program[j + 1];
            
            //Console.WriteLine($"{j} = Opcode: " + opcode + " Operand: " + operand);
            
            if (opcode == 0)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => _registerA,
                        5 => _registerB,
                        6 => _registerC,
                    };
                }
                _registerA = (long)(_registerA / Math.Pow(2, operand));
            }
            else if (opcode == 1)
            {
                _registerB = _registerB ^ operand;
            }
            else if (opcode == 2)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => _registerA,
                        5 => _registerB,
                        6 => _registerC,
                    };
                }
                _registerB = operand % 8;
            }
            else if (opcode == 3)
            {
                if (_registerA != 0)
                {
                    j = (int)operand - 2;
                }
            }
            else if (opcode == 4)
            {
                _registerB = _registerB ^ _registerC;
            }
            else if (opcode == 5)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => _registerA,
                        5 => _registerB,
                        6 => _registerC,
                    };
                }
                int output = (int)(operand % 8);
                outputs.Add(output);
                
            }
            else if (opcode == 6)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => _registerA,
                        5 => _registerB,
                        6 => _registerC,
                    };
                }
                _registerB = (long)(_registerA / Math.Pow(2, operand));
            }
            else if (opcode == 7)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => _registerA,
                        5 => _registerB,
                        6 => _registerC,
                    };
                }
                _registerC = (long)(_registerA / Math.Pow(2, operand));
            }
            
            //AnsiConsole.WriteLine($"Registers: {_registerA}, {_registerB}, {_registerC}");
            
        }
        
        Output = outputs.ToArray();
    }
}
