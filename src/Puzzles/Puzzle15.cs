using MathNet.Numerics.LinearAlgebra;
using Spectre.Console;

namespace AOC2024.Puzzles;


public class Lens
{
    public string Label { get; set; }
    public int FocalLength { get; set; }
}

public class Puzzle15: PuzzleBase
{
    private Matrix<float> _field;
    private Matrix<float> _wideField;

    private float HandleChar(char c)
    {
        if (c == '@') return 9;
        if (c == '#') return -999;
        if (c == 'O') return 1;
        return 0;
    }

    private bool CanMoveBox(Matrix<float> field, (int r, int c) box, int direction)
    {
        bool canMove = true;

        if (field[box.r, box.c] == 0) return canMove;
        if (field[box.r, box.c] == -999) return false; 
        
        if (field[box.r, box.c] == 2)
        {
            return CanMoveBox(field,
                (box.r + direction, box.c), direction) && CanMoveBox(field, (box.r + direction, box.c + 1), direction);
        }
        else if (field[box.r, box.c] == 3)
        {
            return CanMoveBox(field,
                (box.r + direction, box.c), direction) && CanMoveBox(field, (box.r + direction, box.c - 1), direction);
        }

        return false;
    }

    private void MoveBoxes(Matrix<float> field, (int r, int c) box, int direction)
    {
        // determine left and right of current box
        int boxLeft = box.c;
        int boxRight = box.c;
        if (field[box.r, box.c] == 2)
        {
            boxRight++;
        }
        else
        {
            boxLeft--;
        }

        if (field[box.r + direction, boxLeft] != 0)
        {
            MoveBoxes(field, (box.r + direction, boxLeft), direction);
        }

        if (field[box.r + direction, boxRight] != 0)
        {
            MoveBoxes(field, (box.r + direction, boxRight), direction);
        }
        
        if (field[box.r + direction, boxLeft] == 0 && field[box.r + direction, boxRight] == 0)
        {
            field[box.r + direction, boxLeft] = 2;
            field[box.r + direction, boxRight] = 3;
            field[box.r, boxLeft] = 0;
            field[box.r, boxRight] = 0;
        }
        
    }
    

    private void MoveBot(Matrix<float> field, (int r, int c) pos, string moves)
    {
        foreach(var c in moves)
        {
            Console.WriteLine("Move " + c);
            
            (int r, int c) offset = (0, 0);
            if (c == '<')
            {
                offset = (0, -1);
            }
            else if (c == '>')
            {
                offset = (0, 1);
            }
            else if (c == 'v')
            {
                offset = (1, 0);
            }
            else if (c == '^')
            {
                offset = (-1, 0);
            }
 
            if (field[pos.r + offset.r, pos.c + offset.c] == -999)
            {
                // move to wall => do nothing
            }
            else if (field[pos.r + offset.r, pos.c + offset.c] == 0)
            {
                // free space => update pos
                field[pos.r, pos.c] = 0;
                pos = (pos.r + offset.r, pos.c + offset.c);
                field[pos.r, pos.c] = 9;
            }
            else if (field[pos.r + offset.r, pos.c + offset.c] == 1)
            {
                // box -> check if we can move boxes

                (int r, int c) searchPos = (pos.r + offset.r, pos.c + offset.c);
                while (true)
                {
                    if (field[searchPos.r, searchPos.c] == -999)
                    {
                        // boxes hit a wall so no update
                        break;
                    }

                    if (field[searchPos.r, searchPos.c] == 0)
                    {
                        // boxes can move as we have a free space
                        field[searchPos.r, searchPos.c] = 1;
                        field[pos.r + offset.r, pos.c + offset.c] = 9;
                        field[pos.r, pos.c] = 0;
                        pos = (pos.r + offset.r, pos.c + offset.c);
                        break;
                    }
                    searchPos = (searchPos.r + offset.r, searchPos.c + offset.c);                    
                }
            }
            else if (field[pos.r + offset.r, pos.c + offset.c] == 2 || field[pos.r + offset.r, pos.c + offset.c] == 3)
            {
                if (offset.r == 0)
                {
                    // horizontal movement

                    (int r, int c) searchPos = (pos.r + offset.r, pos.c + offset.c);
                    while (true)
                    {
                        if (field[searchPos.r, searchPos.c] == -999)
                        {
                            // boxes hit a wall so no update
                            break;
                        }

                        if (field[searchPos.r, searchPos.c] == 0)
                        {
                            // boxes can move as we have a free space

                            while (true)
                            {
                                field[searchPos.r, searchPos.c] = field[searchPos.r, searchPos.c - offset.c];
                                searchPos = (searchPos.r, searchPos.c - offset.c);
                                if (searchPos == (pos.r + offset.r, pos.c + offset.c))
                                {
                                    field[searchPos.r, searchPos.c] = 9;
                                    field[pos.r, pos.c] = 0;
                                    pos = searchPos;
                                    break;
                                }
                            }

                            break;
                        }

                        searchPos = (searchPos.r + offset.r, searchPos.c + offset.c);
                    }
                }
                else
                {
                    // vertical movement
                    bool canMove = CanMoveBox(field, (pos.r + offset.r, pos.c), offset.r);

                    if (canMove)
                    {
                        Console.WriteLine("Can move boxes");
                        MoveBoxes(field, (pos.r + offset.r, pos.c + offset.c), offset.r);
                        field[pos.r + offset.r, pos.c + offset.c] = 9;
                        field[pos.r , pos.c] = 0;
                        pos = (pos.r + offset.r, pos.c + offset.c);
                    }
                }
            }
            //  PrintMatrix(field, f =>
            //  {
            //      return f switch
            //      {
            //          0 => ".",
            //          1 => "O",
            //          2 => "[",
            //          3 => "]",
            //          -999 => "#",
            //          9 => "@",
            //          _ => throw new ArgumentOutOfRangeException(nameof(f), f, null)
            //      };
            //  });
            //
            // Console.ReadKey(false);
        }
    }

    private long CalcSum(Matrix<float> field)
    {

        long sum = 0;
        foreach (var cell in field.EnumerateIndexed())
        {
            if (cell.Item3 == 1 || cell.Item3 == 2)
            {
                sum += cell.Item1 * 100 + cell.Item2;
            }
        }

        return sum;
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 15 part 1");
        AnsiConsole.WriteLine("Reading file");
        _field = ReadMatrixChar("Data//puzzle15.txt", HandleChar);
        var bot = _field.Find(p => p == 9);

        string moves = string.Join("" ,ReadFullFile("Data//puzzle15-moves.txt").Split('\n',
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
        AnsiConsole.WriteLine("File read");

        MoveBot(_field, (bot.Item1, bot.Item2), moves);

        

        AnsiConsole.WriteLine($"Total sum: {CalcSum(_field)}");
        
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 15 part 2");
        AnsiConsole.WriteLine("Reading file");
        _field = ReadMatrixChar("Data//puzzle15.txt", HandleChar);

        _wideField = Matrix<float>.Build.Sparse(_field.RowCount, _field.ColumnCount * 2);
        
        foreach (var cell in _field.EnumerateIndexed())
        {
            if (cell.Item3 == -999)
            {
                _wideField[cell.Item1, cell.Item2 * 2] = -999;
                _wideField[cell.Item1, cell.Item2 * 2 + 1] = -999;
            }
            else if (cell.Item3 == 1)
            {
                _wideField[cell.Item1, cell.Item2 * 2] = 2;
                _wideField[cell.Item1, cell.Item2 * 2 + 1] = 3;
            }
            else if (cell.Item3 == 9)
            {
                _wideField[cell.Item1, cell.Item2 * 2] = 9;
                _wideField[cell.Item1, cell.Item2 * 2 + 1] = 0;
            }
        }
        
        var bot = _wideField.Find(p => p == 9);

        string moves = string.Join("" ,ReadFullFile("Data//puzzle15-moves.txt").Split('\n',
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
        

        
        
        AnsiConsole.WriteLine("File read");

        MoveBot(_wideField, (bot.Item1, bot.Item2), moves);

        PrintMatrix(_wideField, f =>
        {
            return f switch
            {
                0 => ".",
                1 => "O",
                2 => "[",
                3 => "]",
                -999 => "#",
                9 => "@",
                _ => throw new ArgumentOutOfRangeException(nameof(f), f, null)
            };
        });


        AnsiConsole.WriteLine($"Total sum: {CalcSum(_wideField)}");
        
    }
}