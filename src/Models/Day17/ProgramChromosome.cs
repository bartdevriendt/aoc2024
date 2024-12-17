using System.Security.Cryptography;
using GeneticSharp;

namespace AOC2024.Models.Day17;

public class ProgramChromosome : ChromosomeBase
{
    public ProgramChromosome(int length) : base(length)
    {
        CreateGenes();
    }

    public long RegisterValue()
    {
        var genes = GetGenes();

        long registerA = 0;
        
        foreach (var gene in genes)
        {
            int value = (int)gene.Value;
            registerA = registerA | (uint)value;
            registerA <<= 3;
        }

        registerA >>= 3;
        
        return registerA;
    }
    
    public override Gene GenerateGene(int geneIndex)
    {
        return new Gene(RandomNumberGenerator.GetInt32(8));
    }

    public override IChromosome CreateNew()
    {
        return new ProgramChromosome(Length);
    }
}