namespace AOC2024.Models;

public class MazeVisit
{
    public int R { get; set; }
    public int C { get; set; }
    public int Score { get; set; }
    public int Direction { get; set; }
    public List<(int r, int c)> Visited { get; set; } = new List<(int r, int c)>();
}