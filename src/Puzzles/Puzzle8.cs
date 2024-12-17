using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle8 : PuzzleBase
{

    private Matrix<float> _field;
    private Matrix<float> _antiNodes;
    private void LoadField()
    {
        _field = ReadMatrixChar("Data//puzzle8.txt", c =>
        {
            if (c == '.') return -999;
            return c;
        });
        
        _antiNodes = Matrix.Build.Sparse(_field.RowCount, _field.ColumnCount, 0);
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 8 part 1");
        AnsiConsole.WriteLine("Reading file");
        LoadField();
        AnsiConsole.WriteLine("File read");

        for (int r = 0; r < _field.RowCount; r++)
        {
            for (int c = 0; c < _field.ColumnCount; c++)
            {
                FindAntiNodesPart1(r, c);
            }
        }

        PrintMatrix(_antiNodes);
        
        int totalAntinodes = 0;
        
        foreach (var node in _antiNodes.Enumerate())
        {
            if (node > 0) totalAntinodes++;
        }
        
        AnsiConsole.WriteLine($"Total antinodes = {totalAntinodes}");
    }

    private void FindAntiNodesPart1(int r0, int c0)
    {
        for (int r = 0; r < _field.RowCount; r++)
        {
            for (int c = 0; c < _field.ColumnCount; c++)
            {
                
                if(r == r0 && c == c0) continue;
                if((int)_field[r, c] != (int)_field[r0, c0]) continue;
                if((int)_field[r, c] == -999 ||  (int)_field[r0, c0] == -999) continue;

                int verticDelta = Math.Abs(r0 - r);
                int horizDelta = Math.Abs(c0 - c);

                if (r < r0)
                {
                    if (c < c0)
                    {
                        MarkAntiNode(r - verticDelta, c - horizDelta);
                        MarkAntiNode(r0 + verticDelta, c0 + horizDelta);
                    }
                    else
                    {
                        MarkAntiNode(r - verticDelta, c + horizDelta);
                        MarkAntiNode(r0 + verticDelta, c0 - horizDelta);
                    }
                }
                else
                {
                    if (c < c0)
                    {
                        MarkAntiNode(r + verticDelta, c - horizDelta);
                        MarkAntiNode(r0 - verticDelta, c0 + horizDelta);
                    }
                    else
                    {
                        MarkAntiNode(r + verticDelta, c + horizDelta);
                        MarkAntiNode(r0 - verticDelta, c0 - horizDelta);
                    }
                }
            }
        }  
    }

    private bool MarkAntiNode(int r, int c)
    {
        if (r < 0 || r >= _antiNodes.RowCount) return false;
        if (c < 0 || c >= _antiNodes.ColumnCount) return false;

        _antiNodes[r, c]++;
        return true;
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 8 part 2");
        AnsiConsole.WriteLine("Reading file");
        LoadField();
        AnsiConsole.WriteLine("File read");
        
        for (int r = 0; r < _field.RowCount; r++)
        {
            for (int c = 0; c < _field.ColumnCount; c++)
            {
                FindAntiNodesPart2(r, c);
            }
        }

        PrintMatrix(_antiNodes);
        
        int totalAntinodes = 0;
        
        foreach (var node in _antiNodes.Enumerate())
        {
            if (node > 0) totalAntinodes++;
        }
        
        AnsiConsole.WriteLine($"Total antinodes = {totalAntinodes}");
    }

    private void FindAntiNodesPart2(int r0, int c0)
    {
        for (int r = 0; r < _field.RowCount; r++)
        {
            for (int c = 0; c < _field.ColumnCount; c++)
            {
                
                if(r == r0 && c == c0) continue;
                if((int)_field[r, c] != (int)_field[r0, c0]) continue;
                if((int)_field[r, c] == -999 ||  (int)_field[r0, c0] == -999) continue;

                int verticDelta = Math.Abs(r0 - r);
                int horizDelta = Math.Abs(c0 - c);

                if (r < r0)
                {
                    if (c < c0)
                    {
                        MarkAllAntiNodes(r, c, -verticDelta, -horizDelta);
                        MarkAllAntiNodes(r0, c0, verticDelta, horizDelta);
                    }
                    else
                    {
                        MarkAllAntiNodes(r, c, -verticDelta, horizDelta);
                        MarkAllAntiNodes(r0, c0, verticDelta, -horizDelta);
                    }
                }
                else
                {
                    if (c < c0)
                    {
                        MarkAllAntiNodes(r, c, verticDelta, -horizDelta);
                        MarkAllAntiNodes(r0, c0, -verticDelta, +horizDelta);
                    }
                    else
                    {
                        MarkAllAntiNodes(r,c , verticDelta, horizDelta);
                        MarkAllAntiNodes(r0, c0, -verticDelta, -horizDelta);
                    }
                }
            }
        }  
    }

    private void MarkAllAntiNodes(int r0, int c0, int verticDelta, int horizDelta)
    {
        while (MarkAntiNode(r0, c0))
        {
            r0 += verticDelta;
            c0 += horizDelta;
        }
    }
    
}