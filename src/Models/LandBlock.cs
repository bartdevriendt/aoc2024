namespace AOC2024.Models;

public class LandBlock
{
    public List<Plot> Plots { get; } = new List<Plot>();

    public int Price()
    {

        int numPlots = Plots.Count;
        int fenceCount = Plots.Select(p => p.FenceCount).Sum();

        return numPlots * fenceCount;

    }

    public int Edges()
    {
        int totalEdges = 1;
        
        var leftEdges = (from Plot p in Plots
                where p.LeftFence
                orderby p.C, p.R
                select p
            ).ToList();

        
        
        for (int i = 1; i < leftEdges.Count; i++)
        {
            Plot p1 = leftEdges[i - 1];
            Plot p2 = leftEdges[i];

            if (p1.C != p2.C)
            {
                totalEdges++;
            }
            else
            {
                if (p2.R - p1.R > 1)
                {
                    totalEdges++;
                }
            }
        }
        totalEdges++;
        var rightEdges = (from Plot p in Plots
                where p.RightFence
                orderby p.C, p.R
                select p
            ).ToList();

        
        
        for (int i = 1; i < rightEdges.Count; i++)
        {
            Plot p1 = rightEdges[i - 1];
            Plot p2 = rightEdges[i];

            if (p1.C != p2.C)
            {
                totalEdges++;
            }
            else
            {
                if (p2.R - p1.R > 1)
                {
                    totalEdges++;
                }
            }
        }
        totalEdges++;
        var topEdges = (from Plot p in Plots
                where p.TopFence
                orderby p.R, p.C
                select p
            ).ToList();

        
        
        for (int i = 1; i < topEdges.Count; i++)
        {
            Plot p1 = topEdges[i - 1];
            Plot p2 = topEdges[i];

            if (p1.R != p2.R)
            {
                totalEdges++;
            }
            else
            {
                if (p2.C - p1.C > 1)
                {
                    totalEdges++;
                }
            }
        }
        totalEdges++;
        var bottomEdges = (from Plot p in Plots
                where p.BottomFence
                orderby p.R, p.C
                select p
            ).ToList();

        
        
        for (int i = 1; i < bottomEdges.Count; i++)
        {
            Plot p1 = bottomEdges[i - 1];
            Plot p2 = bottomEdges[i];

            if (p1.R != p2.R)
            {
                totalEdges++;
            }
            else
            {
                if (p2.C - p1.C > 1)
                {
                    totalEdges++;
                }
            }
        }

        return totalEdges;
        
        
    }
}

public class Plot
{
    public int R { get; set; }
    public int C { get; set; }
    public bool TopFence { get; set; }
    public bool LeftFence { get; set; }
    public bool RightFence { get; set; }
    public bool BottomFence { get; set; }

    public int FenceCount
    {
        get
        {
            return (TopFence ? 1 : 0) + (LeftFence ? 1 : 0) + (RightFence ? 1 : 0) + (BottomFence ? 1 : 0);
        }
    }
}