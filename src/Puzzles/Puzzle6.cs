using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle6 : PuzzleBase
{

    private Matrix<float> _field;
    private Matrix<float> _directions;

    private int direction = 0;
    private (int, int) _currentPos;
    
    private void ReadInput()
    {
        _field = ReadMatrixChar("Data//puzzle6.txt", c =>
        {
            if (c == '#') return -999;
            if (c == '^') return 1;
            return 0;
        });
    }
    
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 6 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");

        var pos = _field.Find(x => x == 1);
        _currentPos = (pos.Item1, pos.Item2);

        var nextPos = GetNextPos(_field);
        while (nextPos != (-1, -1))
        {
            _field[nextPos.Item1, nextPos.Item2]++;
            _currentPos = nextPos;
            nextPos = GetNextPos(_field);
        }

        int positionsVisited = 0;
        
        foreach (var cell in _field.Enumerate())
        {
            if (cell > 0)
                positionsVisited++;
        }
        
        AnsiConsole.WriteLine($"Positions visited = {positionsVisited}");
    }

    private (int, int) GetNextPos(Matrix<float> field)
    {
        (int, int) next = (0,0);
        if (direction == 0)
        {
            next = (_currentPos.Item1 - 1, _currentPos.Item2);
        }

        if (direction == 1)
        {
            next = (_currentPos.Item1, _currentPos.Item2 + 1);
        }

        if (direction == 2)
        {
            next = (_currentPos.Item1 + 1, _currentPos.Item2);
        }

        if (direction == 3)
        {
            next = (_currentPos.Item1, _currentPos.Item2 - 1);
        }

        if (next.Item1 < 0 || next.Item1 == _field.RowCount || next.Item2 < 0 || next.Item2 >= _field.ColumnCount)
        {
            return (-1, -1);
        }
        
        if ((int)field[next.Item1, next.Item2] == -999)
        {
            direction += 1;
            if (direction == 4) direction = 0;
            return GetNextPos(field);

        }

        return next;
    }
    

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 6 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");

        var pos = _field.Find(x => x == 1);
        _currentPos = (pos.Item1, pos.Item2);

        int loops = 0;
        
        for (int r = 0; r < _field.RowCount; r++)
        {
            for (int c = 0; c < _field.ColumnCount; c++)
            {
                //AnsiConsole.WriteLine($"Row = {r} - Col = {c}");
                direction = 0;
                Matrix<float> clone = _field.Clone();
                
                if(clone[r, c] != 0) continue;

                clone[r, c] = -999;
                
                pos = clone.Find(x => x == 1);
                _currentPos = (pos.Item1, pos.Item2);
                
                _directions = Matrix.Build.Sparse(clone.RowCount, clone.ColumnCount, -1f);
                _directions[_currentPos.Item1, _currentPos.Item2] = direction;
                var nextPos = GetNextPos(clone);
                while (nextPos != (-1, -1))
                {
                    clone[nextPos.Item1, nextPos.Item2]++;
                    if ((int)_directions[nextPos.Item1, nextPos.Item2] == direction)
                    {
                        loops++;
                        break;
                    }
                    _directions[nextPos.Item1, nextPos.Item2] = direction;
                    _currentPos = nextPos;
                    nextPos = GetNextPos(clone);
                }
            }
        }
        
        AnsiConsole.WriteLine($"Number of loops: {loops}");
    }
}