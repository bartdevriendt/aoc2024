using System.Drawing;
using System.Drawing.Drawing2D;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle10 : PuzzleBase
{

    private Matrix<float> _walkingGrid;
    private List<(int, int)> _trailHeadsVisited;
    private float HandleChar(char arg)
    {
        return arg - '0';
    }

    

    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 10 part 1");
        AnsiConsole.WriteLine("Reading file");
        _walkingGrid = ReadMatrixChar("Data//puzzle10.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");


        int totalPaths = 0;
        
        for (int r = 0; r < _walkingGrid.RowCount; r++)
        {
            for (int c = 0; c < _walkingGrid.ColumnCount; c++)
            {
                _trailHeadsVisited = new List<(int, int)>();
                totalPaths += WalkPath(r, c, 0);
            }
        }
        AnsiConsole.WriteLine($"Total paths: {totalPaths}");
        
    }

    private int WalkPath(int r, int c, int currHeight, bool checkCache= true)
    {
        if (r < 0 || r >= _walkingGrid.RowCount) return 0;
        if (c < 0 || c >= _walkingGrid.ColumnCount) return 0;


        if (_walkingGrid[r, c] != currHeight)
            return 0;


        if (currHeight == 9)
        {
            if (checkCache)
            {
                if (!_trailHeadsVisited.Contains((r, c)))
                {
                    _trailHeadsVisited.Add((r, c));
                    return 1;
                }
                return 0;
            }

            return 1;


        }

        int result = 0;
        
        result += WalkPath(r - 1, c, currHeight + 1, checkCache);
        result += WalkPath(r + 1, c, currHeight + 1, checkCache);
        result += WalkPath(r, c - 1, currHeight + 1, checkCache);
        result += WalkPath(r, c + 1, currHeight + 1, checkCache);

        return result;
    }

    
    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 10 part 2");
        AnsiConsole.WriteLine("Reading file");
        _walkingGrid = ReadMatrixChar("Data//puzzle10.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");


        int totalPaths = 0;
        
        for (int r = 0; r < _walkingGrid.RowCount; r++)
        {
            for (int c = 0; c < _walkingGrid.ColumnCount; c++)
            {
                _trailHeadsVisited = new List<(int, int)>();
                totalPaths += WalkPath(r, c, 0, false);
            }
        }
        AnsiConsole.WriteLine($"Total paths: {totalPaths}");
    
    }
}