using AOC2024.Models.Day17;
using GeneticSharp;
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

        
        var programChromosome = new ProgramChromosome(program.Length);
        var programFitness = new ProgramFitness(program);
        var crossover = new UniformCrossover();
        var mutation = new UniformMutation(true);
        var selection = new RouletteWheelSelection();
        var population = new Population(5000, 5000, programChromosome);
       
        var ga = new GeneticAlgorithm(population, programFitness, selection, crossover, mutation);
        ga.Termination = new GenerationNumberTermination(100);
        ga.GenerationRan += (_, _) =>
        {
            var chrome = (ProgramChromosome) ga.BestChromosome;
        
            AnsiConsole.WriteLine("Best solution found has {0} fitness.", chrome.Fitness);
            AnsiConsole.WriteLine($"Register A: {chrome.RegisterValue()}");
        };
        ga.Start();

        
        
        
        ProgramChromosome best = (ProgramChromosome) ga.BestChromosome;

        foreach (var chrome in ga.Population.CurrentGeneration.Chromosomes)
        {
            ProgramChromosome newChromosome = (ProgramChromosome) chrome;
            if (newChromosome.Fitness == best.Fitness && newChromosome.RegisterValue() < best.RegisterValue())
            {
                AnsiConsole.WriteLine("Selecting a better chromosome");
                best = newChromosome;
            }
        }
        
        AnsiConsole.WriteLine("Best solution found has {0} fitness.", best.Fitness);
        AnsiConsole.WriteLine($"Register A: {best.RegisterValue()}");

        ActualProgram validate = new ActualProgram(program, best.RegisterValue(), 0, 0);
        validate.RunProgram();
        
        AnsiConsole.WriteLine(string.Join(",", program));
        AnsiConsole.WriteLine(string.Join(",", validate.Output));
    }
    
    
}