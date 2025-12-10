using System.Data;
using System.Dynamic;

var manifold = new TachyonManifold();

using (var file = new StreamReader("7-input.txt"))
{
    string? line;
    while ((line = file?.ReadLine()) != null)
    {
        if (line != string.Empty)
        {
            manifold.AddLine(line);
        }
    }
}

var beam = manifold.FindTheBeam();
manifold.AndStartBeaminWithThe(beam);
manifold.PrintTheGriddy();
manifold.PrintResult();

internal class TachyonManifold
{
    internal static TachyonManifold Instance { get; } = new();

    internal List<List<char>> Griddy = [];
    private short RowCount = 0;
    internal long TotalPaths = 0;

    internal void AddLine(string line)
    {
        TachyonManifold.Instance.Griddy.Add([]);
        foreach (var c in line)
        {
            TachyonManifold.Instance.Griddy[RowCount].Add(c);
        }

        RowCount++;
    }

    internal Beamer FindTheBeam()
    {
        var starter = 0;
        var x = -1;

        while (starter == 0)
        {
            x++;
            starter = TachyonManifold.Instance.Griddy[0][x] == 'S' ? x : 0;
        }

        return new Beamer { X = x, Y = 0, PathCount = 1 };
    }

    internal void AndStartBeaminWithThe(Beamer beam)
    {
        Dictionary<(int X, int Y), Beamer> beamDict = new();
        beamDict[(beam.X, beam.Y)] = beam;

        for (int i = 0; i < TachyonManifold.Instance.Griddy.Count - 1; i++)
        {
            Dictionary<(int X, int Y), Beamer> newBeamDic = new();
            
            foreach (var beamer in beamDict.Values)
            {
                var beaminResult = beamer.Beam();
                if (beaminResult is not null)
                {
                    foreach (var newBeam in beaminResult)
                    {
                        var key = (newBeam.X, newBeam.Y);
                        if (newBeamDic.ContainsKey(key))
                        {
                            newBeamDic[key].PathCount += newBeam.PathCount;
                        }
                        else
                        {
                            newBeamDic[key] = newBeam;
                        }
                    }
                }
            }

            beamDict = newBeamDic;
        }

        Instance.TotalPaths = beamDict.Values.Sum(b => b.PathCount);
    }

    internal void PrintResult()
    {
        Console.WriteLine("Total paths: " + TachyonManifold.Instance.TotalPaths);
    }

    internal void PrintTheGriddy()
    {
        for (int y = 0; y < RowCount; y++)
        {
            for (int x = 0; x < TachyonManifold.Instance.Griddy[y].Count; x++)
            {
                Console.Write(TachyonManifold.Instance.Griddy[y][x]);
            }

            Console.WriteLine();
        }
    }
}

class Beamer
{
    internal required int X { get; set; }
    internal required int Y { get; set; }
    internal long PathCount { get; set; } = 1;
    internal List<List<char>> GetGriddy => TachyonManifold.Instance.Griddy;

    internal List<Beamer> Beam()
    {
        var result = new List<Beamer>();

        var target = GetGriddy[Y + 1][X];
        if (target == '.')
        {
            GetGriddy[Y + 1][X] = '|';
            result.Add(new Beamer { Y = Y + 1, X = X, PathCount = this.PathCount });
            return result;
        }

        if (target == '^')
        {
            GetGriddy[Y + 1][X - 1] = '|';
            result.Add(new Beamer { Y = Y + 1, X = X - 1, PathCount = this.PathCount });

            GetGriddy[Y + 1][X + 1] = '|';
            result.Add(new Beamer { Y = Y + 1, X = X + 1, PathCount = this.PathCount });

            return result;
        }

        if (target == '|'){
            result.Add(new Beamer { Y = Y + 1, X = X, PathCount = this.PathCount });
            return result;
        }

        throw new DataException("Nothing is impossible");
    }
}
