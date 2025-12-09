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
    List<List<char>> Numbers = [];
    private short RowCount = 0;
    private long Total = 0;

    internal void AddLine(string line)
    {
        Numbers.Add([]);

        foreach (var character in line)
        {
            if (char.IsDigit(character))
            {
                Numbers[RowCount].Add(character);
            }
            else if (char.IsWhiteSpace(character))
            {
                Numbers[RowCount].Add(' ');
            }
            else if (!char.IsWhiteSpace(character))
            {
                Numbers[RowCount].Add(character);
            }
        }

        RowCount++;
    }

    internal void Math()
    {
        List<long> participants = [];

        for (int column = Numbers[0].Count - 1; column >= 0; column--)
        {
            string currentDigit = string.Empty;
            for (int row = 0; row < RowCount - 1; row++)
            {
                if (char.IsDigit(Numbers[row][column]))
                {
                    currentDigit += (Numbers[row][column]).ToString();
                }
            }

            if (currentDigit != string.Empty)
            {
                participants.Add(int.Parse(currentDigit));
                var mathSymbol = Numbers[RowCount - 1][column];

                if (!char.IsWhiteSpace(mathSymbol))
                {
                    var result = Calculate(mathSymbol, participants);
                    Total += result;

                    participants = [];
                }
            }

        }
    }

    private static long Calculate(char symbol, List<long> participants)
    {
        return symbol switch
        {
            '*' => participants.Aggregate((a, b) => a * b),
            '+' => participants.Aggregate((a, b) => a + b),
            _ => throw new InvalidDataException("What is this sign? " + symbol)
        };
    }

    internal void PrintResult()
    {
        Console.WriteLine("Mathed: " + Total);
    }
}