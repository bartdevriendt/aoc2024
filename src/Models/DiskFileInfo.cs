namespace AOC2024.Models;

public class DiskFileInfo
{
    public int StartPos { get; set; }
    public int Length { get; set; }
    public int FileId { get; set; }


    // public int Compare(DiskFileInfo? x, DiskFileInfo? y)
    // {
    //     if (ReferenceEquals(x, y)) return 0;
    //     if (y is null) return 1;
    //     if (x is null) return -1;
    //     return x.StartPos.CompareTo(y.StartPos);
    // }

    public override string ToString()
    {
        return $"{nameof(StartPos)}: {StartPos}, {nameof(Length)}: {Length}, {nameof(FileId)}: {FileId}";
    }
}