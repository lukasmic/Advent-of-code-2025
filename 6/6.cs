using System.ComponentModel;

var homeworkMachine = new HomeworkMachine();

using (var file = new StreamReader("6/6-input.txt"))
{
    string? line;
    while ((line = file?.ReadLine()) != null)
    {
        if (line != string.Empty)
        {
            homeworkMachine.AddLine(line);
        }
    }
}

homeworkMachine.Math();
homeworkMachine.PrintResult();

internal class HomeworkMachine
{
    List<List<long>> Numbers = [];
    List<string> Symbols = [];
    private short RowCount = 0;
    private long Total = 0;

    internal void AddLine(string line)
    {

        var newElements = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (long.TryParse(newElements[0], out _))
        {
            Numbers.Add([]);

            foreach (var number in newElements)
            {
                Numbers[RowCount].Add(long.Parse(number));
            }
        }
        else
        {
            foreach (var item in newElements)
            {
                Symbols.Add(item);
            }
        }

        RowCount++;
    }

    internal void Math()
    {
        for (int column = 0; column < Numbers[0].Count; column++)
        {
            List<long> participants = [];

            for (int row = 0; row < RowCount - 1; row++)
            {
                participants.Add(Numbers[row][column]);
            }
            var result = Calculate(Symbols[column], participants);
            Console.WriteLine(result);
            Total += result;
        }

    }

    private long Calculate(string symbol, List<long> participants)
    {
        return symbol switch
        {
            "*" => participants.Aggregate((a, b) => a * b),
            "+" => participants.Aggregate((a, b) => a + b),
            _ => throw new InvalidDataException("What is this sign? " + symbol)
        };
    }

    internal void PrintResult()
    {
        Console.WriteLine("Mathed: " + Total);
    }
}