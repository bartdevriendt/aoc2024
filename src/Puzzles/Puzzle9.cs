using System.Reflection;
using AOC2024.Models;
using Spectre.Console;

namespace AOC2024.Puzzles;

public class Puzzle9 : PuzzleBase
{
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 9 part 1");
        AnsiConsole.WriteLine("Reading file");
        string diskmap = ReadFullFile("Data//puzzle9.txt");
        AnsiConsole.WriteLine("File read");


        var layout = ParseLayout(diskmap);
        layout = RearrangePart1(layout);
        //PrintLayout(layout);
        long sum = CalculateChecksum(layout);

        AnsiConsole.WriteLine($"Sum is {sum}");
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 9 part 2");
        AnsiConsole.WriteLine("Reading file");
        string diskmap = ReadFullFile("Data//puzzle9.txt");
        AnsiConsole.WriteLine("File read");

        var layout = ParseLayout(diskmap);
        layout = RearrangePart2(layout);
        PrintLayout(layout);
        long sum = CalculateChecksum(layout);

        AnsiConsole.WriteLine($"Sum is {sum}");
    }

    private long CalculateChecksum(SortedList<int, DiskFileInfo> layout)
    {
        long sum = 0;
        foreach (var pos in layout.Keys)
        {
            if (layout[pos].FileId == -1) continue;

            for (int j = pos; j < pos + layout[pos].Length; j++)
            {
                sum += layout[pos].FileId * j;
            }
        }

        return sum;
    }

    private static SortedList<int, DiskFileInfo> ParseLayout(string diskmap)
    {
        SortedList<int, DiskFileInfo> layout = new SortedList<int, DiskFileInfo>();

        bool isBlock = true;
        int fileId = 0;
        int currPos = 0;
        foreach (char c in diskmap)
        {
            if (isBlock)
            {
                int blockSize = c - '0';
                layout.Add(currPos, new DiskFileInfo()
                {
                    FileId = fileId++,
                    Length = blockSize,
                    StartPos = currPos
                });

                isBlock = false;
                currPos += blockSize;
            }
            else
            {
                int blockSize = c - '0';

                if (blockSize != 0)
                {
                    layout.Add(currPos, new DiskFileInfo()
                    {
                        FileId = -1,
                        Length = blockSize,
                        StartPos = currPos
                    });

                    currPos += blockSize;
                }

                isBlock = true;
            }
        }

        return layout;
    }

    private void PrintLayout(SortedList<int, DiskFileInfo> layout)
    {
        StreamWriter sw = new StreamWriter("E:\\Temp\\puzzle9.txt");

        foreach (var key in layout.Keys)
        {
            sw.WriteLine($"{key}={layout[key]}");
        }

        sw.Close();
    }

    private SortedList<int, DiskFileInfo> RearrangePart1(SortedList<int, DiskFileInfo> layout)
    {
        int maxIndex = layout.Count - 1;

        int currIndex = 0;


        SortedList<int, DiskFileInfo> result = new SortedList<int, DiskFileInfo>();


        while (currIndex < maxIndex)
        {
            var currKey = layout.GetKeyAtIndex(currIndex);
            var currDiskInfo = layout.GetValueAtIndex(currIndex);

            if (currDiskInfo.FileId != -1)
            {
                result.Add(currKey, currDiskInfo);
            }
            else
            {
                var fetchValue = layout.GetValueAtIndex(maxIndex);

                while (fetchValue.Length < currDiskInfo.Length)
                {
                    result.Add(currKey, new DiskFileInfo()
                    {
                        FileId = fetchValue.FileId,
                        Length = fetchValue.Length,
                        StartPos = currKey
                    });


                    currKey += fetchValue.Length;
                    currDiskInfo.StartPos = currKey;
                    currDiskInfo.Length -= fetchValue.Length;
                    maxIndex -= 1;

                    fetchValue = layout.GetValueAtIndex(maxIndex);
                    while (fetchValue.FileId == -1)
                    {
                        maxIndex--;
                        fetchValue = layout.GetValueAtIndex(maxIndex);
                    }
                }

                result.Add(currKey, new DiskFileInfo()
                {
                    FileId = fetchValue.FileId,
                    Length = currDiskInfo.Length,
                    StartPos = currKey
                });

                fetchValue.Length -= currDiskInfo.Length;
                if (fetchValue.Length == 0)
                {
                    maxIndex--;
                    fetchValue = layout.GetValueAtIndex(maxIndex);
                    while (fetchValue.FileId == -1)
                    {
                        maxIndex--;
                        fetchValue = layout.GetValueAtIndex(maxIndex);
                    }
                }
            }

            currIndex += 1;
        }


        if (layout.GetValueAtIndex(currIndex).FileId != -1)
        {
            result.Add(layout.GetKeyAtIndex(currIndex), layout.GetValueAtIndex(currIndex));
        }

        return result;
    }

    private SortedList<int, DiskFileInfo> RearrangePart2(SortedList<int, DiskFileInfo> layout)
    {
        int maxIndex = layout.Count - 1;

        while (maxIndex > 0)
        {
            var fetchValue = layout.GetValueAtIndex(maxIndex);
            var fetchKey = layout.GetKeyAtIndex(maxIndex);

            if (fetchValue.FileId == -1)
            {
                maxIndex--;
                continue;
            }

            for (int currIndex = 0; currIndex < maxIndex; currIndex++)
            {
                var currKey = layout.GetKeyAtIndex(currIndex);
                var currDiskInfo = layout.GetValueAtIndex(currIndex);

                if (currDiskInfo.FileId == -1)
                {
                    if (currDiskInfo.Length >= fetchValue.Length)
                    {
                        int remainingLength = currDiskInfo.Length - fetchValue.Length;

                        layout.Remove(currKey);
                        layout.Remove(fetchKey);
                        layout.Add(currKey, new DiskFileInfo()
                        {
                            FileId = fetchValue.FileId,
                            Length = fetchValue.Length,
                            StartPos = currKey
                        });

                        if (remainingLength > 0)
                        {
                            layout.Add(currKey + fetchValue.Length, new DiskFileInfo()
                            {
                                FileId = -1,
                                Length = remainingLength,
                                StartPos = currKey + fetchValue.Length
                            });
                            maxIndex++;
                        }

                        break;
                    }
                }
            }

            maxIndex--;
        }


        return layout;
    }
}