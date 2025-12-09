using System.Data;
using System.Dynamic;

var manifold = new TachyonManifold();

using (var file = new StreamReader("7/7-input.txt"))
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
    internal short Splits = 0;

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

        return new Beamer { X = x, Y = 0 };
    }

    internal void AndStartBeaminWithThe(Beamer beam)
    {
        List<Beamer> beamList = [beam];

        for (int i = 0; i < TachyonManifold.Instance.Griddy.Count - 1; i++)
        {
            List<Beamer> newBeamers = [];
            foreach (var beamer in beamList)
            {
                var beaminResult = beamer.Beam();
                if (beaminResult is not null)
                {
                    newBeamers.AddRange(beaminResult);
                }
            }

            beamList = newBeamers;
        }
    }

    internal void PrintResult()
    {
        Console.WriteLine("Splitted: " + TachyonManifold.Instance.Splits);
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
    internal List<List<char>> GetGriddy => TachyonManifold.Instance.Griddy;
    internal void IncrementSplits() => TachyonManifold.Instance.Splits++;

    internal List<Beamer> Beam()
    {
        var result = new List<Beamer>();

        var target = GetGriddy[Y + 1][X];
        if (target == '.')
        {
            GetGriddy[Y + 1][X] = '|';
            Y++;
            result.Add(this);
            return result;
        }

        if (target == '^')
        {
            var targetLeft = GetGriddy[Y + 1][X - 1];
            if (targetLeft == '.')
            {
                GetGriddy[Y + 1][X - 1] = '|';
                result.Add(new Beamer { Y = Y + 1, X = X - 1 });
            }

            // Right will always happen
            GetGriddy[Y + 1][X - 1] = '|';
            IncrementSplits();
            result.Add(new Beamer { Y = Y + 1, X = X + 1 });

            return result;
        }

        if (target == '|')
            return null;

        throw new DataException("Nothing is impossible");
    }
}