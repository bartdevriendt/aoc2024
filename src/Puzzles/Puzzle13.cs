using AOC2024.Models;
using MathNet.Numerics.LinearAlgebra;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle13 : PuzzleBase
{

    private List<MachineInfo> _machineInfos;
    private void ReadInput()
    {
        string[] data = ReadFullFile("Data//puzzle13.txt").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        _machineInfos = new List<MachineInfo>();


        for (int j = 0; j < data.Length; j += 3)
        {
            MachineInfo info = new MachineInfo();
            info.ButtonAX = int.Parse(data[j][12..14]);
            info.ButtonAY = int.Parse(data[j][18..]);
            info.ButtonBX = int.Parse(data[j+1][12..14]);
            info.ButtonBY = int.Parse(data[j+1][18..]);

            string[] parts = data[j + 2].Split(' ');
            info.PrizeX = int.Parse(parts[1].Replace(",", "")[2..]);
            info.PrizeY = int.Parse(parts[2][2..]);
            _machineInfos.Add(info);
        }
        
        
    }

    private long ProcessMachines(double offset = 0)
    {
        long tokens = 0;
        foreach (var machine in _machineInfos)
        {

            Matrix<double> a = Matrix<double>.Build.Sparse(2, 2);

            a[0, 0] = machine.ButtonAX;
            a[0, 1] = machine.ButtonBX;
            a[1, 0] = machine.ButtonAY;
            a[1, 1] = machine.ButtonBY;
            
            Matrix<double> b = Matrix<double>.Build.Sparse(2, 1);

            b[0, 0] = machine.PrizeX + offset;
            b[1, 0] = machine.PrizeY + offset;

            var inversea = a.Inverse();

            var result = inversea * b;

            bool solved = false;

            double pressA = Math.Round(result[0, 0], 2);
            double pressB = Math.Round(result[1, 0], 2);
            
            if (pressA % 1 == 0)
            {
                if (pressB % 1 == 0)
                {
                    tokens += (long)pressA * 3 + (long)pressB;


                    
                }
            }

        }

        return tokens;
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 13 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");

        //ProcessMachinesPart1();
        long tokens = ProcessMachines(0);
        AnsiConsole.WriteLine($"Total tokens = {tokens}");

    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 13 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");
        long tokens = ProcessMachines(10000000000000);
        AnsiConsole.WriteLine($"Total tokens = {tokens}");
    }
}