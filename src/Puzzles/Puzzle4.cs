using MathNet.Numerics.LinearAlgebra;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle4 : PuzzleBase
{
    
    private int x = 'X';
    private int m = 'M';
    private int a = 'A';
    private int s = 'S';
    
    private float HandleChar(char c)
    {
        return (int)c;
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 4 part 1");
        AnsiConsole.WriteLine("Reading file");
        Matrix<float> table = ReadMatrixChar("Data//puzzle4.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");
        
        //PrintMatrix(table);

        int count = 0;

        foreach (var element in table.EnumerateIndexed())
        {
            if ((int)element.Item3 == x)
            {
                AnsiConsole.WriteLine($"Checking position {element.Item2}, {element.Item1}");
                count += CheckPositionPart1(element.Item2, element.Item1, table);
            }
        }
        
        AnsiConsole.WriteLine($"Number of xmas: {count}");
    }

    private int CheckPositionPart1(int x, int y, Matrix<float> table)
    {

        int count = 0;

        if (IsValue(x - 1, y, m, table) && IsValue(x - 2, y, a, table) && IsValue(x - 3, y, s, table))
            count++;
        
        if (IsValue(x - 1, y - 1, m, table) && IsValue(x - 2, y - 2, a, table) && IsValue(x - 3, y - 3, s, table))
            count++;

        if (IsValue(x, y - 1, m, table) && IsValue(x, y - 2, a, table) && IsValue(x, y - 3, s, table))
            count++;

        if (IsValue(x + 1, y - 1, m, table) && IsValue(x + 2, y - 2, a, table) && IsValue(x + 3, y - 3, s, table))
            count++;
        
        if (IsValue(x + 1, y, m, table) && IsValue(x + 2, y, a, table) && IsValue(x + 3, y, s, table))
            count++;
        
        if (IsValue(x + 1, y + 1, m, table) && IsValue(x + 2, y + 2, a, table) && IsValue(x + 3, y + 3, s, table))
            count++;
        
        if (IsValue(x, y + 1, m, table) && IsValue(x, y + 2, a, table) && IsValue(x, y + 3, s, table))
            count++;
        
        if (IsValue(x - 1, y + 1, m, table) && IsValue(x - 2, y + 2, a, table) && IsValue(x - 3, y + 3, s, table))
            count++;
        
        return count;
    }

    private bool IsValue(int x, int y, float value, Matrix<float> table)
    {
        if (x >= 0 && x < table.ColumnCount)
        {
            if (y >= 0 && y < table.RowCount)
            {
                return (int)table[y, x] == (int)value;
            }
        }

        return false;
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 4 part 1");
        AnsiConsole.WriteLine("Reading file");
        Matrix<float> table = ReadMatrixChar("Data//puzzle4.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");
        
        //PrintMatrix(table);

        int count = 0;

        foreach (var element in table.EnumerateIndexed())
        {
            if ((int)element.Item3 == a)
            {
                AnsiConsole.WriteLine($"Checking position {element.Item2}, {element.Item1}");
                count += CheckPositionPart2(element.Item2, element.Item1, table);
            }
        }
        
        AnsiConsole.WriteLine($"Number of x-mas: {count}");
    }

    private int CheckPositionPart2(int x, int y, Matrix<float> table)
    {
        if (IsValue(x - 1, y - 1, m, table) && IsValue(x + 1, y + 1, s, table) ||
            IsValue(x + 1, y + 1, m, table) && IsValue(x - 1, y - 1, s, table))
        {
            if (IsValue(x + 1, y - 1, m, table) && IsValue(x - 1, y + 1, s, table) ||
                IsValue(x - 1, y + 1, m, table) && IsValue(x + 1, y - 1, s, table))
            {
                return 1;
            }
        }

        return 0;
    }
}