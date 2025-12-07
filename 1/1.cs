var dial = new Dial();

using (var file = new StreamReader("1-input.txt"))
{
    string? line;
    while ((line = file?.ReadLine()) != null)
    {
        dial.Turn(line);
    }
}

dial.PrintZeroLandedCounter();
dial.PrintZeroPassedCounter();

class Dial
{
    private int CurrentValue { get; set; } = 50;
    private int ZeroLandedCounter { get; set; } = 0;
    private int ZeroPassedCounter { get; set; } = 0;

    public void Turn(string instruction)
    {
        var direction = instruction[0];
        var amount = int.Parse(instruction.Substring(1));

        int fullRotations = amount / 100;
        bool crossedZero = false;

        Console.WriteLine($"Previous value = {CurrentValue}");
        Console.WriteLine($"Previous ZeroPassedCounter = {ZeroPassedCounter}");

        if (direction == 'L')
        {
            if (CurrentValue != 0 && (CurrentValue - amount % 100 <= 0))
            {
                crossedZero = true;
            }

            CurrentValue -= amount;
            if (CurrentValue < 0)
            {
                CurrentValue = (100 - (Math.Abs(CurrentValue) % 100)) % 100;
            }

            ZeroPassedCounter += fullRotations + (crossedZero ? 1 : 0);

            Console.WriteLine($"Instruction = {instruction}");
            Console.WriteLine($"New value = {CurrentValue}");
            Console.WriteLine($"crossedZero = {crossedZero}");
            Console.WriteLine($"fullRotations = {fullRotations}");
            Console.WriteLine($"ZeroPassedCounter = {ZeroPassedCounter}");
        }
        else if (direction == 'R')
        {
            if (CurrentValue + amount % 100 >= 100)
            {
                crossedZero = true;
            }

            CurrentValue += amount;
            if (CurrentValue >= 100)
            {
                CurrentValue %= 100;
            }

            ZeroPassedCounter += fullRotations + (crossedZero ? 1 : 0);

            Console.WriteLine($"Instruction = {instruction}");
            Console.WriteLine($"New value = {CurrentValue}");
            Console.WriteLine($"crossedZero = {crossedZero}");
            Console.WriteLine($"fullRotations = {fullRotations}");
            Console.WriteLine($"ZeroPassedCounter = {ZeroPassedCounter}");
        }

        Console.WriteLine("___________________________________");

        if (CurrentValue == 0)
            ZeroLandedCounter++;

        return;
    }

    public void PrintCurrentValue()
    {
        Console.WriteLine(CurrentValue);
    }

    public void PrintZeroLandedCounter()
    {
        Console.WriteLine("Landed on 0: " + ZeroLandedCounter);
    }

    public void PrintZeroPassedCounter()
    {
        Console.WriteLine("Passed 0: " + ZeroPassedCounter);
    }
}

