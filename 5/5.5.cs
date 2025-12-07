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
                Console.WriteLine("Adding NEW range " + line);
                cafeteria.AddFreshProductRange(line);
                System.Console.WriteLine("_______________________________________");
                continue;
            }

            insertingRanges = false;
            continue;
        }

        continue;
    }
}

cafeteria.PrintFreshCount();

internal class Cafeteria
{
    List<(long, long)> FreshRanges = [];
    private long FreshIdsCount = 0;

    internal void AddFreshProductRange(string range)
    {
        Console.WriteLine("Adding range " + range);
        var minAndMax = range.Split("-");
        var min = long.Parse(minAndMax[0]);
        var max = long.Parse(minAndMax[1]);

        if (IsFullyCovered(min, max)) return;
        if (IsFullyCoveringExisting(min, max)) return;

        if (IsMergedLeft(min, max)) return;
        if (IsMergedRight(min, max)) return;

        FreshRanges.Add((min, max));

        Console.WriteLine("range just added");
    }

    private bool IsFullyCoveringExisting(long min, long max)
    {
        if (FreshRanges.Any(existingRange => min < existingRange.Item1
                                          && max > existingRange.Item2))
        {
            Console.WriteLine("range fully covering existing");

            FreshRanges.RemoveAll(existingRange => min < existingRange.Item1
                                                && max > existingRange.Item2);
            AddFreshProductRange(min.ToString() + "-" + max.ToString());

            return true;
        }

        return false;
    }

    private bool IsFullyCovered(long min, long max)
    {
        return FreshRanges.Any(existingRange => existingRange.Item1 <= min
                                             && existingRange.Item2 >= max);
    }

    private bool IsMergedLeft(long min, long max)
    {
        var lefties = FreshRanges.FindAll(existingRange => min <= existingRange.Item1
                                          && max >= existingRange.Item1);

        if (lefties is not [])
        {
            var maxest = long.MinValue;
            foreach (var item in lefties)
            {
                maxest = item.Item2 > maxest ? item.Item2 : maxest;
                FreshRanges.Remove(item);
            }
            Console.WriteLine("range merged left");

            var newMax = maxest > max ? maxest : max;
            AddFreshProductRange(min.ToString() + "-" + newMax);

            return true;
        }

        return false;
    }
    private bool IsMergedRight(long min, long max)
    {
        var maxies = FreshRanges.FindAll(existingRange => min <= existingRange.Item2
                                          && max > existingRange.Item2);

        if (maxies is not [])
        {
            var minimest = long.MaxValue;
            foreach (var item in maxies)
            {
                minimest = item.Item1 < minimest ? item.Item1 : minimest;
                FreshRanges.Remove(item); // check if this works correctly
            }
            Console.WriteLine("range merged right");

            var newMin = minimest < min ? minimest : min;
            AddFreshProductRange(newMin + "-" + max.ToString());

            return true;
        }

        return false;
    }

    internal void PrintFreshCount()
    {
        foreach (var range in FreshRanges)
        {
            FreshIdsCount += range.Item2 - range.Item1 + 1;
        }

        Console.WriteLine("Fresh Ids count: " + FreshIdsCount);
    }
}