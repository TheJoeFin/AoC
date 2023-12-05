using System.Diagnostics;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string _input;

    private readonly string _testInput = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Attempt1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Attempt1Part2()}");

    public int Attempt1()
    {
        var lines = _input.Split(Environment.NewLine);

        List<ScratchOffCard> cards = [];

        foreach (var line in lines)
        {
            var card = new ScratchOffCard(line);
            cards.Add(card);
        }

        int sum = 0;
        foreach (ScratchOffCard card1 in cards)
            sum += card1.Score;

        return sum;
    }

    public int Attempt1Part2()
    {
        return 2;
    }
}


public class ScratchOffCard
{
    public int CardNumber { get; set; }
    public HashSet<int> WinningNumbers { get; set; } = [];
    public HashSet<int> ScratchOffNumbers { get; set; } = [];
    public int Score { get; set; }

    public ScratchOffCard(string rawLine)
    {
        var split = rawLine.Split(":");
        CardNumber = int.Parse(split[0].Replace("Card ", "").Trim());
        var numbers = split[1].Split("|");

        var winningNumStrings = numbers[0].Split(" ");
        foreach (var num in winningNumStrings)
            if (int.TryParse(num, out int number))
                WinningNumbers.Add(number);

        var scratchOffNumStrings = numbers[1].Split(" ");
        foreach (var num in scratchOffNumStrings)
            if (int.TryParse(num, out int number))
                ScratchOffNumbers.Add(number);

        if (WinningNumbers.Count == 0 || ScratchOffNumbers.Count == 0)
            throw new Exception("Invalid card");

        Score = CalculateCardScore();
    }

    private int CalculateCardScore()
    {
        int numberOfWinners = -1;
        foreach (int winner in WinningNumbers)
            if (ScratchOffNumbers.Contains(winner))
                numberOfWinners++;

        if (numberOfWinners == -1)
            return 0;

        return (int)Math.Pow(2, numberOfWinners);
    }
}