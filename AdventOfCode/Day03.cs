using System.Diagnostics;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;

    private readonly string _testInput = @"A467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";


    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Attempt1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Attempt1Part2()}");

    public int Attempt1()
    {
        return 1;
    }

    public int Attempt1Part2()
    {
        return 2;
    }
}
