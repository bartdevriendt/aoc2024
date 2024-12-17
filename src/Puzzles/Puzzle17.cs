using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle17 : PuzzleBase
{
    private long registerA;
    private long registerB;
    private long registerC;
    private int[] program;

    private void ReadInput()
    {
        string[] lines = ReadFullFile("Data//puzzle17.txt").Split('\n',
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        registerA = int.Parse(lines[0]);
        registerB = int.Parse(lines[1]);
        registerC = int.Parse(lines[2]);
        
        program = lines[3].Split(',').Select(int.Parse).ToArray();

    }

    private int[] ProcessProgram(bool expectOutput = false)
    {

        List<int> outputs = new();
        int outputPointer = 0;
        
        for (int j = 0; j < program.Length; j+=2)
        {
            long opcode = program[j];
            long operand = program[j + 1];
            
            //Console.WriteLine($"{j} = Opcode: " + opcode + " Operand: " + operand);
            
            if (opcode == 0)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => registerA,
                        5 => registerB,
                        6 => registerC,
                    };
                }
                registerA = (long)(registerA / Math.Pow(2, operand));
            }
            else if (opcode == 1)
            {
                registerB = registerB ^ operand;
            }
            else if (opcode == 2)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => registerA,
                        5 => registerB,
                        6 => registerC,
                    };
                }
                registerB = operand % 8;
            }
            else if (opcode == 3)
            {
                if (registerA != 0)
                {
                    j = (int)operand - 2;
                }
            }
            else if (opcode == 4)
            {
                registerB = registerB ^ registerC;
            }
            else if (opcode == 5)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => registerA,
                        5 => registerB,
                        6 => registerC,
                    };
                }
                int output = (int)(operand % 8);
                outputs.Add(output);
                if (expectOutput)
                {
                    if (program[outputPointer] != output)
                    {
                        return [];
                    }
                    outputPointer++;
                }
                
            }
            else if (opcode == 6)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => registerA,
                        5 => registerB,
                        6 => registerC,
                    };
                }
                registerB = (long)(registerA / Math.Pow(2, operand));
            }
            else if (opcode == 7)
            {
                if (operand > 3)
                {
                    operand = operand switch
                    {
                        4 => registerA,
                        5 => registerB,
                        6 => registerC,
                    };
                }
                registerC = (long)(registerA / Math.Pow(2, operand));
            }
            
            //AnsiConsole.WriteLine($"Registers: {registerA}, {registerB}, {registerC}");
            
        }
        
        return outputs.ToArray();
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 17 part 1");
        AnsiConsole.WriteLine("Reading file");
        
        ReadInput();            
        
        
        AnsiConsole.WriteLine("File read");

        int[] output = ProcessProgram();
        
        AnsiConsole.WriteLine($"Output: {string.Join(",", output)}");
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 17 part 2");
        AnsiConsole.WriteLine("Reading file");
        
        ReadInput();
        
        AnsiConsole.WriteLine("File read");


        long regA = 401;
        registerA = regA;
        while (regA < 20000)
        {
            int[] result = ProcessProgram(false);
            
            AnsiConsole.WriteLine($"RegA: {regA}\t Registers: {registerA}, {registerB}, {registerC} \t\t Output: {string.Join(",", result)} \t Length: {result.Length}");
            
            // regA++;
            // registerA = regA;
            // registerB = 0;
            // registerC = 0;
            if (result.Length == 0 || result.Length != program.Length)
            {
                regA++;
                registerA = regA;
                registerB = 0;
                registerC = 0;
                if(regA % 1000000 == 0)
                    AnsiConsole.WriteLine($"RegA: {regA}");
            }
            else
            {
                AnsiConsole.WriteLine($"Output: {string.Join(",", result)}");
                break;
            }
        }
        
        AnsiConsole.WriteLine($"Register A: {regA}");

    }
}