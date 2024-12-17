using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle7 : PuzzleBase
{

    private List<string> operations = new List<string>();

    private void ReadInput()
    {
        ReadFileLineByLine("Data//puzzle7.txt", s => operations.Add(s));
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 7 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");

        long calibResult = 0;
        
        foreach (var line in operations)
        {
            string[] parts = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int combinations = CountCombinations(long.Parse(parts[0]), parts[1], new[] { "+", "*" });
            if (combinations > 0)
            {
                calibResult += long.Parse(parts[0]);
            }
        } 
        
        AnsiConsole.WriteLine($"Total calib result = {calibResult}");
        
    }

    private int CountCombinations(long resultValue, string expression, string[] operators)
    {
        int totalCombinations = 0;

        int[] numbers =
            expression.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(s => Int32.Parse(s)).ToArray();

        int left = numbers[0];
        
        foreach(var op in operators)
        {
            totalCombinations += DoOperation(resultValue, left, numbers, 1, op, operators);
        }

        return totalCombinations;
    }

    private int DoOperation(long resultValue, long left, int[] numbers, int index, string operation, string[] operators)
    {
        long result = left;

        if (operation == "*")
        {
            result *= numbers[index];
        }
        else if (operation == "+")
        {
            result += numbers[index];
        }
        else if (operation == "||")
        {
            string lefthand = left.ToString();
            string rightHand = numbers[index].ToString();
            result = long.Parse(lefthand + rightHand);
        }

        index++;
        if (index == numbers.Length)
        {
            return resultValue == result ? 1 : 0;
        }

        int returnValue = 0;
        foreach(var op in operators)
        {
            returnValue += DoOperation(resultValue, result, numbers, index, op, operators);
        }

        return returnValue;
    } 
    

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 7 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");

        long calibResult = 0;
        
        foreach (var line in operations)
        {
            string[] parts = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int combinations = CountCombinations(long.Parse(parts[0]), parts[1], new[] { "+", "*","||" });
            if (combinations > 0)
            {
                calibResult += long.Parse(parts[0]);
            }
        } 
        
        AnsiConsole.WriteLine($"Total calib result = {calibResult}");
    }
}