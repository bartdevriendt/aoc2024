using System.Drawing;
using System.Drawing.Imaging;
using AOC2024.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Spectre.Console;
using Color = System.Drawing.Color;

namespace AOC2024.Puzzles;


public class Puzzle14 : PuzzleBase
{

    private List<Robot> _robots = new List<Robot>();
    
    
    private void ProcessLine(string line)
    {
        Robot r = new Robot();
        string[] parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        string[] xy = parts[0][2..].Split(',');
        string[] dxdy = parts[1][2..].Split(',');
        r.X = int.Parse(xy[0]);
        r.Y = int.Parse(xy[1]);
        r.Dx = int.Parse(dxdy[0]);
        r.Dy = int.Parse(dxdy[1]);
        
        _robots.Add(r);
    }


    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 14 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle14.txt", s => ProcessLine(s));
        AnsiConsole.WriteLine("File read");

        int seconds = 1;

        StreamWriter sw = new StreamWriter("Data//output14.txt");
        
        
        while (seconds < 10000)
        {
            AnsiConsole.MarkupLine($"[green]Seconds: {seconds}[/]");
            
            
            
            
            MoveRobots(101, 103, seconds);

            Matrix<float> field = Matrix.Build.Sparse(103, 101);

            foreach (var robot in _robots)
            {
                field[robot.EndY, robot.EndX] = 1;
            }

            string output = "";
            bool isCandidate = false;

            for (int r = 0; r < field.RowCount; r++)
            {
                string line = "";
                for (int c = 0; c < field.ColumnCount; c++)
                {
                    line += (field[r, c] > 0 ? "*" : " ");

                }

                if (line.IndexOf("**********") >= 0)
                {
                    isCandidate = true;
                }

                output += line + "\n";
            }
            

            if (isCandidate)
            {
                sw.WriteLine($"Seconds: {seconds}");
                sw.WriteLine(output);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
            }

            seconds += 1;

        }
    }

    private void MoveRobots(int fieldSizeX, int fieldSizeY, int seconds)
    {
        _robots.ForEach(r =>
        {
            r.EndX = r.X + r.Dx * seconds;
            r.EndY = r.Y + r.Dy * seconds;

            if (r.EndX < 0) r.EndX = r.EndX % -fieldSizeX + fieldSizeX;
            if (r.EndY < 0) r.EndY = r.EndY % -fieldSizeY + fieldSizeY;

            r.EndX = r.EndX % fieldSizeX;
            r.EndY = r.EndY % fieldSizeY;

            // while (r.X < 0) r.X = r.X + fieldSizeX;
            // while (r.Y < 0) r.Y = r.Y + fieldSizeY;
            //
            // while(r.X >= fieldSizeX) r.X = r.X - fieldSizeX;
            // while(r.Y >= fieldSizeY) r.Y = r.Y - fieldSizeY;
            //

        });
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 14 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle14.txt", s => ProcessLine(s));
        AnsiConsole.WriteLine("File read");

        int seconds = 100;
        int fieldSizeX = 101;
        int fieldSizeY = 103;
        
        
        MoveRobots(fieldSizeX, fieldSizeY, seconds);

        int q1 = _robots.Where(r => r.EndX < fieldSizeX / 2 && r.EndY < fieldSizeY / 2).Count();
        int q2 = _robots.Where(r => r.EndX > fieldSizeX / 2 && r.EndY < fieldSizeY / 2).Count();
        int q3 = _robots.Where(r => r.EndX < fieldSizeX / 2 && r.EndY > fieldSizeY / 2).Count();
        int q4 = _robots.Where(r => r.EndX > fieldSizeX / 2 && r.EndY > fieldSizeY / 2).Count();
        
        AnsiConsole.WriteLine($"Score: {q1 * q2 * q3 * q4}");
    }
}