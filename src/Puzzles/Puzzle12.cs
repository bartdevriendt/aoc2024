using AOC2024.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle12 : PuzzleBase
{
    private Matrix<float> _land;
    private List<(int, int)> _visited = new List<(int, int)>();
    private List<LandBlock> _blocks = new List<LandBlock>();
    private void ReadInput()
    {
        _land = ReadMatrixChar("Data//puzzle12.txt", c => c);

        _land = _land.InsertColumn(0, new SparseVector(_land.RowCount));
        _land = _land.InsertColumn(_land.ColumnCount, new SparseVector(_land.RowCount));
        _land = _land.InsertRow(0, new SparseVector(_land.ColumnCount));
        _land = _land.InsertRow(_land.RowCount, new SparseVector(_land.ColumnCount));
        
        PrintMatrix(_land);

    }


    private void WalkBlock(LandBlock block, int r, int c, int val)
    {
        if ((int)_land[r, c] == val && !_visited.Contains((r, c)))
        {
            Plot plot = new Plot();
            plot.C = c;
            plot.R = r;
            plot.BottomFence = (int)_land[r + 1, c] != val;
            plot.TopFence = (int)_land[r - 1, c] != val;
            plot.LeftFence = (int)_land[r, c - 1] != val;
            plot.RightFence = (int)_land[r, c + 1] != val;
            block.Plots.Add(plot);
            _visited.Add((r, c));
            
            WalkBlock(block, r - 1, c, val);
            WalkBlock(block, r + 1, c, val);
            WalkBlock(block, r, c - 1, val);
            WalkBlock(block, r, c + 1, val);
        }
    }

    private void FindBlocks()
    {
        for (int r = 1; r < _land.RowCount - 1; r++)
        {
            for (int c = 1; c < _land.ColumnCount - 1; c++)
            {
                if (!_visited.Contains((r, c)))
                {
                    LandBlock block = new LandBlock();
                    WalkBlock(block, r, c, (int)_land[r, c]);
                    _blocks.Add(block);
                }
            }
        }
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 12 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");
        FindBlocks();
        
        AnsiConsole.WriteLine($"Total price: {_blocks.Select(b => b.Price()).Sum()} ");
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 12 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");
        FindBlocks();
        
        AnsiConsole.WriteLine($"Total price: {_blocks.Select(b => b.Edges() * b.Plots.Count).Sum()} ");
    }
}