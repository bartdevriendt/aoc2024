using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle5 : PuzzleBase
{


    private Dictionary<string, List<string>> pageOrderMap = new Dictionary<string, List<string>>();
    private List<string[]> printInstructions = new List<string[]>();
    private void ReadInput()
    {

        int part = 1;
        
        ReadFileLineByLine("Data//puzzle5.txt", line =>
        {
            if (part == 1)
            {
                if (line.Trim() == "") part++;
                else
                {
                    string[] parts = line.Trim().Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    if (!pageOrderMap.ContainsKey(parts[0]))
                    {
                        pageOrderMap[parts[0]] = new List<string>();
                    }
                    
                    pageOrderMap[parts[0]].Add(parts[1]);
                }
            }
            else
            {
                printInstructions.Add(line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
            }
        });
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 5 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");

        int sum = 0;
        
        foreach (var pages in printInstructions)
        {
            if (IsCorrectlyOrdered(pages))
            {
                AnsiConsole.WriteLine(String.Join(',', pages));
                string middle = pages[(int)Math.Floor(pages.Length / 2.0)];
                sum += int.Parse(middle);
            }
        }
        
        AnsiConsole.WriteLine($"Total = {sum}");
        
    }

    private bool IsCorrectlyOrdered(string[] pages)
    {

        for (int j = 1; j < pages.Length; j++)
        {
            
            if(!pageOrderMap.ContainsKey(pages[j])) continue;
            
            for (int i = j - 1; i >= 0; i--)
            {
                if (pageOrderMap[pages[j]].Contains(pages[i]))
                    return false;
            }
        }

        return true;
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 5 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadInput();
        AnsiConsole.WriteLine("File read");

        int sum = 0;
        
        foreach (var pages in printInstructions)
        {
            if (!IsCorrectlyOrdered(pages))
            {
                AnsiConsole.Write(String.Join(',', pages) + " becomes ");

                string[] sorted = SortPages(pages);
                
                
                AnsiConsole.WriteLine(String.Join(',', sorted));
                string middle = sorted[(int)Math.Floor(sorted.Length / 2.0)];
                sum += int.Parse(middle);
            }
        }
        
        AnsiConsole.WriteLine($"Total = {sum}");
    }

    private string[] SortPages(string[] pages)
    {

        List<string> result = new List<string>();

        result.Add(pages[pages.Length - 1]);
        
        for (int j = pages.Length - 2; j >= 0; j--)
        {

            bool inserted = false;
            for (int i = result.Count - 1; i >= 0; i--)
            {
                if (pageOrderMap.ContainsKey(result[i]) && pageOrderMap[result[i]].Contains(pages[j]))
                {
                    result.Insert(i + 1, pages[j]);
                    inserted = true;
                    break;
                }
            }

            if (!inserted)
            {
                result.Insert(0, pages[j]);
            }
        }

        return result.ToArray();
    }
}