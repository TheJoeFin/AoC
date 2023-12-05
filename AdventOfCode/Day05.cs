using System.Diagnostics;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string _input;

    private readonly string _testInput = @"";

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Attempt1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Attempt1Part2()}");

    public int Attempt1()
    {
        var lines = _testInput.Split(Environment.NewLine);
        return 1;
    }

    public int Attempt1Part2()
    {
        var lines = _testInput.Split(Environment.NewLine);
        return 2;
    }
}
