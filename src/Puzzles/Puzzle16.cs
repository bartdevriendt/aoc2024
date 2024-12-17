using AOC2024.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using Spectre.Console;
using DenseMatrix = MathNet.Numerics.LinearAlgebra.Single.DenseMatrix;

namespace AOC2024.Puzzles;




public class Puzzle16: PuzzleBase
{

    private Matrix<float> _maze;
    private Matrix<float> _lowestScores;
    
    private Queue<MazeVisit> _queue = new Queue<MazeVisit>();
    private long _lowestScore = long.MaxValue;

    private List<(int r, int c)> _bestCells = new();

    private float HandleChar(char c)
    {
        return c switch
        {
            '#' => -999,
            '.' => 0,
            'S' => 1,
            'E' => 2
        };
    }

    private bool CanVisit(int r, int c, int direction)
    {
        if (direction < 0) direction += 4;
        if (direction == 4) direction = 0;

        if (direction == 0)
        {
            c++;
        }
        else if (direction == 1)
        {
            r++;
        }
        else if (direction == 2)
        {
            c--;
        }
        else if (direction == 3)
        {
            r--;
        }

        return (int)_maze[r, c] != -999;
    }

    private void WalkTheMaze()
    {
        while (_queue.Count > 0)
        {
            var visit = _queue.Dequeue();
            
            if(visit.Score > _lowestScores[visit.R, visit.C] + 1001) continue;

            if (visit.Score < _lowestScores[visit.R, visit.C]) _lowestScores[visit.R, visit.C] = visit.Score;

            if ((int)_maze[visit.R, visit.C] == 2)
            {
                if (visit.Score < _lowestScore)
                {
                    _lowestScore = visit.Score;
                    Console.WriteLine($"Lowest score: {_lowestScore}");
                    _bestCells = new List<(int r, int c)>();
                    _bestCells.AddRange(visit.Visited);
                }
                else if (visit.Score == _lowestScore)
                {
                    foreach (var v in visit.Visited)
                    {
                        if (!_bestCells.Contains(v))
                        {
                            _bestCells.Add(v);
                        }
                    }
                }

                continue;
            }
            
            if(_maze[visit.R, visit.C] == -999) continue;

            if (CanVisit(visit.R, visit.C, visit.Direction))
            {

                var visitForward = new MazeVisit()
                {
                    C = visit.Direction switch
                    {
                        0 => visit.C + 1,
                        1 => visit.C,
                        2 => visit.C - 1,
                        3 => visit.C
                    },
                    R = visit.Direction switch
                    {
                        0 => visit.R,
                        1 => visit.R + 1,
                        2 => visit.R,
                        3 => visit.R - 1
                    },
                    Direction = visit.Direction,
                    //Visited = new List<(int r, int c)>(),
                    Score = visit.Score + 1
                };
                visitForward.Visited.AddRange(visit.Visited);
                //if (visitForward.Visited.Contains((visitForward.R, visitForward.C))) continue;
                visitForward.Visited.Add((visitForward.R, visitForward.C));
                _queue.Enqueue(visitForward);
            }

            if (CanVisit(visit.R, visit.C, visit.Direction - 1))
            {
                var visitLeft = new MazeVisit()
                {
                    C = visit.Direction switch
                    {
                        0 => visit.C,
                        1 => visit.C + 1,
                        2 => visit.C,
                        3 => visit.C - 1
                    },
                    R = visit.Direction switch
                    {
                        0 => visit.R - 1,
                        1 => visit.R,
                        2 => visit.R + 1,
                        3 => visit.R
                    },
                    Direction = visit.Direction - 1,
                    Visited = new List<(int r, int c)>(),
                    Score = visit.Score + 1001
                };
                if (visitLeft.Direction < 0) visitLeft.Direction += 4;
                visitLeft.Visited.AddRange(visit.Visited);
                //if (visitLeft.Visited.Contains((visitLeft.R, visitLeft.C))) continue;
                visitLeft.Visited.Add((visitLeft.R, visitLeft.C));
                _queue.Enqueue(visitLeft);
            }

            if (CanVisit(visit.R, visit.C, visit.Direction + 1))
            {


                var visitRight = new MazeVisit()
                {
                    C = visit.Direction switch
                    {
                        0 => visit.C,
                        1 => visit.C - 1,
                        2 => visit.C,
                        3 => visit.C + 1
                    },
                    R = visit.Direction switch
                    {
                        0 => visit.R + 1,
                        1 => visit.R,
                        2 => visit.R - 1,
                        3 => visit.R
                    },
                    Direction = visit.Direction + 1,
                    Visited = new List<(int r, int c)>(),
                    Score = visit.Score + 1001
                };
                if (visitRight.Direction == 4) visitRight.Direction = 0;
                visitRight.Visited.AddRange(visit.Visited);
                //if (visitRight.Visited.Contains((visitRight.R, visitRight.C))) continue;
                visitRight.Visited.Add((visitRight.R, visitRight.C));

                _queue.Enqueue(visitRight);
            }
        }
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 16 part 1");
        AnsiConsole.WriteLine("Reading file");
        _maze = ReadMatrixChar("Data/puzzle16.txt", HandleChar);

        _lowestScores = Matrix<float>.Build.Dense(_maze.RowCount, _maze.ColumnCount, float.MaxValue);
        
        AnsiConsole.WriteLine("File read");

        var start = _maze.Find(c => c == 1);

        var visit = new MazeVisit()
        {
            R = start.Item1,
            C = start.Item2,
            Direction = 0,
            Score = 0,
            Visited = new List<(int r, int c)>()
        };
        
        visit.Visited.Add((start.Item1, start.Item2));
        
        _queue.Enqueue(visit);

        WalkTheMaze();
        
        AnsiConsole.WriteLine($"Lowest score: {_lowestScore}");
    }

    public override void Part2()
    {
        
        AnsiConsole.WriteLine("Puzzle 16 part 2");
        AnsiConsole.WriteLine("Reading file");
        _maze = ReadMatrixChar("Data/puzzle16.txt", HandleChar);

        _lowestScores = Matrix<float>.Build.Dense(_maze.RowCount, _maze.ColumnCount, float.MaxValue);
        
        AnsiConsole.WriteLine("File read");

        var start = _maze.Find(c => c == 1);

        var visit = new MazeVisit()
        {
            R = start.Item1,
            C = start.Item2,
            Direction = 0,
            Score = 0,
            Visited = new List<(int r, int c)>()
        };
        
        visit.Visited.Add((start.Item1, start.Item2));
        
        _queue.Enqueue(visit);

        WalkTheMaze();
        
        AnsiConsole.WriteLine($"Lowest score: {_lowestScore}");
        AnsiConsole.WriteLine($"Best cell count: {_bestCells.Count}");
        
        //PrintMatrix(_lowestScores);
        
    }
}