var generator = new BatteryGenerator();

using (var file = new StreamReader("3-input.txt"))
{
    string? line;
    while ((line = file?.ReadLine()) != null)
    {
        generator.AddBank(line);
    }

    generator.PrintTotalJoltage();
}

internal class BatteryGenerator
{
    private int TotalJoltage = 0;

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

    public void PrintTotalJoltage()
    {
        Console.WriteLine(TotalJoltage);
    }
}