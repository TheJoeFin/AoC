using System.Diagnostics;

namespace AdventOfCode;

public class Day01 : BaseDay
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


    Dictionary<string, int> _namedPairs = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Attempt1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Attempt1Part2()}");

    public int Attempt1()
    {
        var lines = _input.Split(Environment.NewLine);
        var sum = 0;
        foreach (var line in lines)
        {
            char firstChar = ' ';
            char secondChar = ' ';

            foreach (char c in line)
            {
                if (char.IsDigit(c))
                {
                    firstChar = c;
                    break;
                }
            }

            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    secondChar = line[i];
                    break;
                }
            }
            string number = $"{firstChar}{secondChar}";
            // Console.WriteLine(number);
            sum += int.Parse(number);
        }

        return sum;
    }

    public int Attempt1Part2()
    {
        var lines = _input.Split(Environment.NewLine);
        var sum = 0;

        foreach (var line in lines)
        {
            string numberString = GetFirstNumberStringStartingLeft(line);
            numberString += GetFirstNumberStringStartingRight(line);

            Console.WriteLine(numberString);
            bool successParse = int.TryParse(numberString, out int number);
            if (successParse)
                sum += number;
            else
                Console.WriteLine($"Failed to parse {numberString}");
        }

        return sum;
    }

    public string GetFirstNumberStringStartingLeft(string line)
    {
        int index = 0;
        string numberString = string.Empty;

        // look for a digit
        foreach (char c in line)
        {
            if (char.IsDigit(c))
            {
                index = line.IndexOf(c);
                numberString = c.ToString();
                break;
            }
        }

        // look for a word
        // sliding window
        foreach (KeyValuePair<string, int> pair in _namedPairs)
        {
            string word = pair.Key;
            int wordLength = word.Length;
            int value = pair.Value;

            int tempIndex = line.IndexOf(word);
            if (tempIndex > -1
                && tempIndex <= index)
            {
                index = tempIndex;
                numberString = value.ToString();
                //Console.WriteLine($"Found {word} at {tempIndex} in {line}");
            }
        }

        return numberString;
    }

    public string GetFirstNumberStringStartingRight(string rawLine)
    {
        string line = StringReverse(rawLine);

        int index = 0;
        string numberString = string.Empty;

        // look for a digit
        foreach (char c in line)
        {
            if (char.IsDigit(c))
            {
                index = line.IndexOf(c);
                numberString = c.ToString();
                break;
            }
        }

        // look for a word
        // sliding window
        foreach (KeyValuePair<string, int> pair in _namedPairs)
        {
            string wordRaw = pair.Key;
            string word = StringReverse(wordRaw);
            int value = pair.Value;

            int tempIndex = line.IndexOf(word);
            if (tempIndex > -1
                && tempIndex <= index)
            {
                index = tempIndex;
                numberString = value.ToString();
                // Console.WriteLine($"Found {word} at {tempIndex} in {line}");
            }

        }

        return numberString;
    }

    public static string StringReverse(string s)
    {
        if (s.Length <= 1)
            return s;

        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
