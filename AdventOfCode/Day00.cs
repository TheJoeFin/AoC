using System.Diagnostics;

namespace AdventOfCode;

public class Day00 : BaseDay
{
    private readonly string _input;

    private readonly string _testInput = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

    private readonly string _testInput2 = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";


    public Day00()
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
