var generator = new BatteryGenerator();

using (var file = new StreamReader("3-input.txt"))
{
    string? line;
    while ((line = file?.ReadLine()) != null)
    {
        generator.AddBigBank(line, 12);
    }

    generator.PrintTotalJoltage();
}

internal class BatteryGenerator
{
    private long TotalJoltage = 0;

    internal void AddBank(string batteries)
    {
        var firstBattery = 0;
        var secondBattery = 0;
        for (int i = 0; i < batteries.Length - 1; i++)
        {
            var firstCandidate = batteries[i] - '0'; // some char to int bullshit
            var secondCandidate = batteries[i + 1] - '0';
            if (secondCandidate > secondBattery)
                secondBattery = secondCandidate;

            if (firstCandidate > firstBattery)
            {
                firstBattery = firstCandidate;
                secondBattery = secondCandidate;
            }
        }

        TotalJoltage += firstBattery * 10 + secondBattery;
    }

    internal void AddBigBank(string bank, int batteriesNeeded)
    {
        var newJoltage = "";
        var previousDigitIndex = -1;
        while (batteriesNeeded > 0)
        {
            var biggestDigit = 0;
            for (int i = previousDigitIndex + 1; i < bank.Length - batteriesNeeded + 1; i++)
            {
                var candidateValue = bank[i] - '0'; // some char to int bullshit again
                if (candidateValue > biggestDigit)
                {
                    biggestDigit = candidateValue;
                    previousDigitIndex = i;
                }
            }

            batteriesNeeded--;
            newJoltage += biggestDigit;
        }

        Console.WriteLine(newJoltage);
        TotalJoltage += long.Parse(newJoltage);
    }

    public void PrintTotalJoltage()
    {
        Console.WriteLine(TotalJoltage);
    }
}