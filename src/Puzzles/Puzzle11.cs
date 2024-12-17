using System.Collections.Concurrent;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle11 : PuzzleBase
{

    private Dictionary<long, long> _numberCount = new Dictionary<long, long>();
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 11 part 1");
        AnsiConsole.WriteLine("Reading file");
        string start = ReadFullFile("Data//puzzle11.txt");
        AnsiConsole.WriteLine("File read");
        List<long> stones = start.Split(' ').Select(s => long.Parse(s)).ToList();

        AnsiConsole.WriteLine(string.Join(' ', stones));
        
        for (int j = 0; j < 25; j++)
        {
            stones = ApplyRules(stones);
            //AnsiConsole.WriteLine(string.Join(' ', stones));
        }
        
        AnsiConsole.WriteLine($"Number of stones: {stones.Count}");
    }

    
    
    private List<long> ApplyRules(List<long> stones)
    {

        List<long> result = new List<long>();
        
        foreach (var stone in stones)
        {
            if(stone == 0) result.Add(1);
            else
            {
                int digits = (int)Math.Floor(Math.Log10(stone) + 1);
        
                if ((digits % 2) == 0)
                {
                    string sstone = stone.ToString();
                    result.Add(long.Parse(sstone[0..(sstone.Length / 2)]));
                    result.Add(long.Parse(sstone[(sstone.Length / 2)..]));
                }
                else
                {
                    result.Add(stone * 2024);
                }
                
            }
        }

        return result.ToList();

    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 11 part 1");
        AnsiConsole.WriteLine("Reading file");
        string start = ReadFullFile("Data//puzzle11.txt");
        AnsiConsole.WriteLine("File read");
        List<long> stones = start.Split(' ').Select(s => long.Parse(s)).ToList();

        AnsiConsole.WriteLine(string.Join(' ', stones));

        foreach (var stone in stones)
        {
            if (!_numberCount.ContainsKey(stone))
            {
                _numberCount[stone] = 0;
            }

            _numberCount[stone]++;
        }   
        
        for (int j = 0; j < 75; j++)
        {
            
            AnsiConsole.WriteLine($"Iteration = {j+1} - Number of stones = {_numberCount.Values.Sum()}");

            Dictionary<long, long> newNumberCount = new Dictionary<long, long>();

            foreach (var key in _numberCount.Keys)
            {
                if (key == 0)
                {
                    if (!newNumberCount.ContainsKey(1))
                        newNumberCount[1] = 0;
                    newNumberCount[1] += _numberCount[key];
                }
                else
                {
                    int digits = (int)Math.Floor(Math.Log10(key) + 1);

                    if ((digits % 2) == 0)
                    {
                        string sstone = key.ToString();

                        long n1 = long.Parse(sstone[0..(sstone.Length / 2)]);
                        long n2 = long.Parse(sstone[(sstone.Length / 2)..]);

                        if (!newNumberCount.ContainsKey(n1))
                            newNumberCount[n1] = 0;
                        if (!newNumberCount.ContainsKey(n2))
                            newNumberCount[n2] = 0;

                        newNumberCount[n1] += _numberCount[key];
                        newNumberCount[n2] += _numberCount[key];
                    }
                    else
                    {
                        long newVal = key * 2024;
                        if (!newNumberCount.ContainsKey(newVal))
                            newNumberCount[newVal] = 0;
                        newNumberCount[newVal] += _numberCount[key];
                    }
                }

            }

            _numberCount = newNumberCount;


            //AnsiConsole.WriteLine(string.Join(' ', stones));
        }
        
        AnsiConsole.WriteLine($"Number of stones: {_numberCount.Values.Sum()}");
    }

    
    

}