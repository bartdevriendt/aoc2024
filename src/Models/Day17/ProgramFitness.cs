using GeneticSharp;

namespace AOC2024.Models.Day17;

public class ProgramFitness : IFitness
{
    
    private int[] _program;

    public ProgramFitness(int[] program)
    {
        _program = program;
    }

    public double Evaluate(IChromosome chromosome)
    {
        var chrome = (ProgramChromosome)chromosome;
        long regA = chrome.RegisterValue();
        var actualProgram = new ActualProgram( _program, regA, 0, 0);
        actualProgram.RunProgram();
        
        double fitness = ComparePrograms(_program, actualProgram.Output);
        
        fitness = fitness * (1 - (regA / (double)(long.MaxValue / 10)));
        
        return fitness;
        ;
    }

    private double ComparePrograms(int[] expected, int[] actual)
    {
        int correct = 0;
        int i = 0;
        while (i < expected.Length && i < actual.Length)
        {
            if(expected[i] == actual[i]) correct++;
            i++;
        }
        
        return correct / (double)expected.Length; 
    }
}