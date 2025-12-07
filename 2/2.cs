var fakeIdCatcher = new FakeIdCatcher();

using (var file = new StreamReader("2-input.txt"))
{
    string? line;
    line = file?.ReadLine();
    foreach (var range in line.Split(','))
    {
        var ids = range.Split("-");
        if (ids.Length != 2)
            throw new Exception("Yo somebody is lying");
        fakeIdCatcher.Catch(long.Parse(ids[0]), long.Parse(ids[1]));
    }

}

internal class FakeIdCatcher()
{
    private long InvalidIdSum = 0;

    internal void Catch(long firstId, long lastId)
    {
        for (var id = firstId; id <= lastId; id++)
        {
            if (IsInvalidId(id))
            {
                InvalidIdSum += id;
                Console.WriteLine("WINNER: " + id);
            }
        }

        Console.WriteLine(InvalidIdSum);
    }

    private static bool IsInvalidId(long id)
    {
        if (id < 10) return false;
        var word = id.ToString();

        if (IsEachDigitSame(word)) return true;

        return ProcessDigit(word);
    }

    private static bool IsEachDigitSame(string digits)
    {
        for (int index = 1; index < digits.Length; index++)
        {
            if (digits[index] != (digits[0]))
            {
                return false;
            }
        }
        // Console.WriteLine(digits + " each digit is same");
        return true;
    }

    private static bool IsSameInDigitPairs(string digits)
    {

        for (int index = digits.Length; index > 2; index--)
        {
            if (index % 2 == 1)
            {
                Console.WriteLine(digits[index - 1]);
                if (digits[index - 1] != digits[0] || digits[index] != digits[1])
                {
                    Console.WriteLine(digits + " not in pairs");
                    return false;
                }
            }
        }

        Console.WriteLine(digits + " in pairs");
        return true;
    }

    private static bool ProcessDigit(string digits)
    {

        // return IsEqualDividedByHalf(digits);

        for (int positions = digits.Length / 2; positions > 1; positions--)
        {
            if (digits.Length % positions != 0)
                continue;
            if (IsEqualDividedByPositions(digits, positions))
                return true;

        }

        return false;

        static bool IsEqualDividedByHalf(string digits)
        {
            return IsEqualDividedByPositions(digits, digits.Length / 2);
        }

        static bool IsEqualDividedByPositions(string digits, int positions)
        {
            for (int i = 0; i < digits.Length - positions; i++)
            {
                if (digits[i] != digits[i + positions])
                    return false;
            }

            return true;
        }
    }
}
