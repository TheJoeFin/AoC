using System.Diagnostics;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string _input;

    private readonly string _testInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

    private readonly string _testInput2 = @"";


    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Attempt1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Attempt1Part2()}");

    public int Attempt1()
    {
        var lines = _input.Split(Environment.NewLine);

        // The Elf would first like to know which games would have been possible if the bag contained only 
        // 12 red cubes
        // 13 green cubes
        // 14 blue cubes

        int redMax = 12;
        int greenMax = 13;
        int blueMax = 14;



        int sum = 0;
        // parse each line
        foreach (var line in lines)
        {
            List<int> reds = [0];
            List<int> greens = [0];
            List<int> blues = [0];

            string cleanedLine = line.Replace(';', ',');
            var split1 = cleanedLine.Split(':');
            var gameTitleSplit = split1[0].Split(' ');

            bool canParseGameNumber = int.TryParse(gameTitleSplit[1], out int gameNumber);
            if (!canParseGameNumber)
            {
                Console.WriteLine($"Could not parse game number from {gameTitleSplit[1]}");
                continue;
            }

            var results = split1[1].Trim().Split(',');

            foreach (var result in results)
            {
                var split2 = result.Trim().Split(' ');
                var color = split2[1];

                bool canParseCount = int.TryParse(split2[0], out int count);
                if (!canParseCount)
                {
                    Console.WriteLine($"Could not parse count from {split2[0]}");
                    continue;
                }

                if (color == "red")
                {
                    reds.Add(count);
                }
                else if (color == "green")
                {
                    greens.Add(count);
                }
                else if (color == "blue")
                {
                    blues.Add(count);
                }

            }
            int biggestRed = reds.Max();
            int biggestGreen = greens.Max();
            int biggestBlue = blues.Max();

            if (biggestRed <= redMax && biggestGreen <= greenMax && biggestBlue <= blueMax)
            {
                // Console.WriteLine($"Game {gameNumber} is possible");
                // Console.WriteLine($"Red: {biggestRed}, Green: {biggestGreen}, Blue: {biggestBlue}");
                sum += gameNumber;
            }
        }

        return sum;
    }

    public int Attempt1Part2()
    {
        var lines = _input.Split(Environment.NewLine);

        // The Elf would first like to know which games would have been possible if the bag contained only 
        // 12 red cubes
        // 13 green cubes
        // 14 blue cubes

        int redMax = 12;
        int greenMax = 13;
        int blueMax = 14;



        int sum = 0;
        // parse each line
        foreach (var line in lines)
        {
            List<int> reds = [0];
            List<int> greens = [0];
            List<int> blues = [0];

            string cleanedLine = line.Replace(';', ',');
            var split1 = cleanedLine.Split(':');
            var gameTitleSplit = split1[0].Split(' ');

            bool canParseGameNumber = int.TryParse(gameTitleSplit[1], out int gameNumber);
            if (!canParseGameNumber)
            {
                Console.WriteLine($"Could not parse game number from {gameTitleSplit[1]}");
                continue;
            }

            var results = split1[1].Trim().Split(',');

            foreach (var result in results)
            {
                var split2 = result.Trim().Split(' ');
                var color = split2[1];

                bool canParseCount = int.TryParse(split2[0], out int count);
                if (!canParseCount)
                {
                    Console.WriteLine($"Could not parse count from {split2[0]}");
                    continue;
                }

                if (color == "red")
                {
                    reds.Add(count);
                }
                else if (color == "green")
                {
                    greens.Add(count);
                }
                else if (color == "blue")
                {
                    blues.Add(count);
                }

            }
            int biggestRed = reds.Max();
            int biggestGreen = greens.Max();
            int biggestBlue = blues.Max();

            int gamePower = biggestRed * biggestGreen * biggestBlue;

            if (biggestRed <= redMax && biggestGreen <= greenMax && biggestBlue <= blueMax)
            {
                // Console.WriteLine($"Game {gameNumber} is possible");
                // Console.WriteLine($"Red: {biggestRed}, Green: {biggestGreen}, Blue: {biggestBlue}");
            }
            sum += gamePower;
        }

        return sum;
    }
}
