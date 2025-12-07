using System.Runtime.CompilerServices;

var grid = new PaperRollGrid();

using (var file = new StreamReader("4/4-input.txt"))
{
    string? line;
    while ((line = file?.ReadLine()) != null)
    {
        grid.AddRow(line);
    }

    grid.Forklift();

    // grid.GetRollNeighbourCount(0, 0);
    // grid.GetRollNeighbourCount(0, 2);
    // grid.PrintTheRug();
}

internal class PaperRollGrid
{
    private List<List<bool>> Map = [];
    private short RowsCount = 0;
    private short ForkliftedRolls = 0;

    internal void AddRow(string newRow)
    {
        Map.Add([]);
        for (int i = 0; i < newRow.Length; i++)
        {
            Map[RowsCount].Add(newRow[i] == '@');
        }
        RowsCount++;
    }

    internal void Forklift()
    {
        short? forkliftedThisTime = null;
        var removableRolls = new List<(short, short)>();

        while (forkliftedThisTime != 0)
        {
            forkliftedThisTime = 0;

            for (short y = 0; y < Map.Count; y++)
            {
                for (short x = 0; x < Map[y].Count; x++)
                {
                    if (!Map[y][x]) continue;

                    var neighbourCount = GetPaperRollNeighbourCount(y, x);

                    if (neighbourCount < 4)
                    {
                        removableRolls.Add((y, x));
                        forkliftedThisTime++;
                        ForkliftedRolls++;
                    }
                }
            }

            Console.WriteLine("Forklifted " + forkliftedThisTime + " rolls");

            RemoveRolls(removableRolls);
            removableRolls.Clear();
        }

        Console.WriteLine("Total " + ForkliftedRolls + " rolls");
    }

    private void RemoveRolls(List<(short, short)> rolls)
    {
        for (int i = 0; i < rolls.Count; i++)
        {
            Map[rolls[i].Item1][rolls[i].Item2] = false;
        }
    }

    private byte GetPaperRollNeighbourCount(short rollY, short rollX)
    {
        byte neighbourCount = 0;

        for (int y = rollY - 1; y <= rollY + 1; y++)
        {
            if (y < 0 || y >= Map.Count) continue;

            for (int x = rollX - 1; x <= rollX + 1; x++)
            {
                if (x == rollX && y == rollY // Don't count self lol
                    || x < 0
                    || x >= Map[y].Count)
                    continue;

                if (Map[y][x])
                {
                    neighbourCount++;
                }
            }

        }

        // Console.WriteLine("Roll " + rollY + ":" + rollX + "neighbour count: " + neighbourCount);
        return neighbourCount;
    }

    public void PrintTheRug()
    {
        Console.WriteLine(Map[0].Count);
        Console.WriteLine(Map.Count);
        foreach (var row in Map)
        {
            Console.WriteLine();
            foreach (var column in row)
            {
                Console.Write(column ? "@" : ".");
            }
        }
    }
}