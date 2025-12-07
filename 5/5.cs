var cafeteria = new Cafeteria();
var insertingRanges = true;

using (var file = new StreamReader("5/5-input.txt"))
{
    string? line;
    while ((line = file?.ReadLine()) != null)
    {
        if (insertingRanges)
        {
            if (line != string.Empty)
            {
                cafeteria.AddFreshProductRange(line);
                continue;
            }

            insertingRanges = false;
            continue;
        }

        cafeteria.AddProduct(line);
    }
}

cafeteria.PrintFreshCount();

internal class Cafeteria
{
    List<(long, long)> FreshRanges = [];
    private int FreshIngredientsCount = 0;

    internal void AddFreshProductRange(string range)
    {
        Console.WriteLine("Adding range " + range);
        var minAndMax = range.Split("-");
        var min = long.Parse(minAndMax[0]);
        var max = long.Parse(minAndMax[1]);

        FreshRanges.Add((min, max));

        Console.WriteLine("range added");
    }

    internal void AddProduct(string product)
    {
        var productId = long.Parse(product);
        foreach (var range in FreshRanges)
        {
            if (productId >= range.Item1 && productId <= range.Item2)
            {
                FreshIngredientsCount++;
                return;
            }
        }
    }

    internal void PrintFreshCount()
    {
        Console.WriteLine(FreshIngredientsCount);
    }
}