using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;

    private readonly string _testInput = @"467..114..
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
        var lines = _input.Split(Environment.NewLine);
        // parse into a 2d array
        var grid = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                grid[i, j] = line[j];
            }
        }

        Dictionary<(int, int), char> locationOfSymbols = [];

        // find the location of the symbols
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                char c = grid[i, j];
                if (c != '.' && !char.IsDigit(c))
                {
                    locationOfSymbols.Add((i, j), c);
                }
            }
        }

        List<(int, int)> coordinatesOfNumbersAdjacentToSymbol = [];

        foreach (var symbol in locationOfSymbols)
        {
            var (i, j) = symbol.Key;
            var c = symbol.Value;

            // find the numbers adjacent to the symbol


            // O X O
            // X S X
            // O X O

            if (i > 0 && char.IsDigit(grid[i - 1, j])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i - 1, j)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i - 1, j));
            }
            if (i < grid.GetLength(0) - 1 && char.IsDigit(grid[i + 1, j])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j)))
            {
                if (!coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j)))
                    coordinatesOfNumbersAdjacentToSymbol.Add((i + 1, j));
            }
            if (j > 0 && char.IsDigit(grid[i, j - 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i, j - 1)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i, j - 1));
            }
            if (j < grid.GetLength(1) - 1 && char.IsDigit(grid[i, j + 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i, j + 1)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i, j + 1));
            }


            // X O X
            // O S O
            // X O X

            if (i > 0 && j > 0 && char.IsDigit(grid[i - 1, j - 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i - 1, j - 1)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i - 1, j - 1));
            }
            if (i > 0 && j < grid.GetLength(1) - 1 && char.IsDigit(grid[i - 1, j + 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i - 1, j + 1)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i - 1, j + 1));
            }
            if (i < grid.GetLength(0) - 1 && j > 0 && char.IsDigit(grid[i + 1, j - 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j - 1)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i + 1, j - 1));
            }
            if (i < grid.GetLength(0) - 1 && j < grid.GetLength(1) - 1 && char.IsDigit(grid[i + 1, j + 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j + 1)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i + 1, j + 1));
            }
        }

        // get x and y coordinates of the numbers
        var coordinatesOfNumbers = new Dictionary<(int, int), int>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                char c = grid[i, j];
                if (char.IsDigit(c))
                {
                    int digit = int.Parse(c.ToString());
                    coordinatesOfNumbers.Add((i, j), digit);
                }
            }
        }

        List<NumberAndCoords> numbersAndCoords = [];
        int lastRow = -2;
        int lastCol = -2;
        string stringOfNumbers = string.Empty;
        List<(int, int)> coordsOfChars = [];

        (int, int) lastCoordinate = coordinatesOfNumbers.Keys.Last();
        foreach (var coordinate in coordinatesOfNumbers)
        {
            var (i, j) = coordinate.Key;
            var number = coordinate.Value;

            if (i != lastRow)
            {
                // starting a new row, save the last one if there is one
                if (!string.IsNullOrEmpty(stringOfNumbers))
                {
                    numbersAndCoords.Add(new NumberAndCoords
                    {
                        Number = int.Parse(stringOfNumbers),
                        Coordinates = new(coordsOfChars)
                    });
                    coordsOfChars.Clear();
                }

                stringOfNumbers = number.ToString();
                coordsOfChars.Add((i, j));
            }
            else
            {
                // not a new row, just a new column, need to see if it's adjacent to the last one
                if (j == lastCol + 1)
                {
                    // adjacent
                    stringOfNumbers += number.ToString();
                    coordsOfChars.Add((i, j));
                }
                else
                {
                    // not adjacent, save the last one if there is one
                    if (!string.IsNullOrEmpty(stringOfNumbers))
                    {
                        numbersAndCoords.Add(new NumberAndCoords
                        {
                            Number = int.Parse(stringOfNumbers),
                            Coordinates = new(coordsOfChars)
                        });
                        coordsOfChars.Clear();
                    }

                    stringOfNumbers = number.ToString();
                    coordsOfChars.Add((i, j));
                }
            }
            lastRow = i;
            lastCol = j;

            if (coordinate.Key == lastCoordinate)
            {
                // last coordinate, save the last one if there is one
                if (!string.IsNullOrEmpty(stringOfNumbers))
                {
                    numbersAndCoords.Add(new NumberAndCoords
                    {
                        Number = int.Parse(stringOfNumbers),
                        Coordinates = new(coordsOfChars)
                    });
                    coordsOfChars.Clear();
                }
            }
        }

        List<NumberAndCoords> correctNumberAndCoords = [];

        // console write the numbers and their coordinates
        foreach (var numberAndCoords in numbersAndCoords)
        {
            bool madeMatch = false;
            foreach (var coords in numberAndCoords.Coordinates)
            {
                foreach (var item in coordinatesOfNumbersAdjacentToSymbol)
                {
                    if (coords == item)
                    {
                        correctNumberAndCoords.Add(numberAndCoords);
                        madeMatch = true;
                        break;
                    }
                }
                if (madeMatch)
                    break;
            }
        }

        int sum = 0;
        foreach (var item in correctNumberAndCoords)
        {
            sum += item.Number;
        }

        return sum;
    }

    public int Attempt1Part2()
    {
        var lines = _input.Split(Environment.NewLine);
        // parse into a 2d array
        var grid = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                grid[i, j] = line[j];
            }
        }

        Dictionary<(int, int), int> locationOfSymbols = [];
        int firstInt = 1;

        // find the location of the symbols
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                char c = grid[i, j];
                if (c == '*')
                {
                    locationOfSymbols.Add((i, j), firstInt);
                    firstInt++;
                }
            }
        }

        List<(int, int, int)> coordinatesOfNumbersAdjacentToSymbol = [];

        foreach (var symbol in locationOfSymbols)
        {
            var (i, j) = symbol.Key;
            var c = symbol.Value;

            // find the numbers adjacent to the symbol

            // O X O
            // X S X
            // O X O

            if (i > 0 && char.IsDigit(grid[i - 1, j])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i - 1, j, c)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i - 1, j, c));
            }
            if (i < grid.GetLength(0) - 1 && char.IsDigit(grid[i + 1, j])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j, c)))
            {
                if (!coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j, c)))
                    coordinatesOfNumbersAdjacentToSymbol.Add((i + 1, j, c));
            }
            if (j > 0 && char.IsDigit(grid[i, j - 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i, j - 1, c)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i, j - 1, c));
            }
            if (j < grid.GetLength(1) - 1 && char.IsDigit(grid[i, j + 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i, j + 1, c)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i, j + 1, c));
            }


            // X O X
            // O S O
            // X O X

            if (i > 0 && j > 0 && char.IsDigit(grid[i - 1, j - 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i - 1, j - 1, c)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i - 1, j - 1, c));
            }
            if (i > 0 && j < grid.GetLength(1) - 1 && char.IsDigit(grid[i - 1, j + 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i - 1, j + 1, c)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i - 1, j + 1, c));
            }
            if (i < grid.GetLength(0) - 1 && j > 0 && char.IsDigit(grid[i + 1, j - 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j - 1, c)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i + 1, j - 1, c));
            }
            if (i < grid.GetLength(0) - 1 && j < grid.GetLength(1) - 1 && char.IsDigit(grid[i + 1, j + 1])
                && !coordinatesOfNumbersAdjacentToSymbol.Contains((i + 1, j + 1, c)))
            {
                coordinatesOfNumbersAdjacentToSymbol.Add((i + 1, j + 1, c));
            }
        }

        // get x and y coordinates of the numbers
        var coordinatesOfNumbers = new Dictionary<(int, int), int>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                char c = grid[i, j];
                if (char.IsDigit(c))
                {
                    int digit = int.Parse(c.ToString());
                    coordinatesOfNumbers.Add((i, j), digit);
                }
            }
        }

        List<NumberAndCoords> numbersAndCoords = [];
        int lastRow = -2;
        int lastCol = -2;
        string stringOfNumbers = string.Empty;
        List<(int, int)> coordsOfChars = [];

        (int, int) lastCoordinate = coordinatesOfNumbers.Keys.Last();
        foreach (var coordinate in coordinatesOfNumbers)
        {
            var (i, j) = coordinate.Key;
            var number = coordinate.Value;

            if (i != lastRow)
            {
                // starting a new row, save the last one if there is one
                if (!string.IsNullOrEmpty(stringOfNumbers))
                {
                    numbersAndCoords.Add(new NumberAndCoords
                    {
                        Number = int.Parse(stringOfNumbers),
                        Coordinates = new(coordsOfChars)
                    });
                    coordsOfChars.Clear();
                }

                stringOfNumbers = number.ToString();
                coordsOfChars.Add((i, j));
            }
            else
            {
                // not a new row, just a new column, need to see if it's adjacent to the last one
                if (j == lastCol + 1)
                {
                    // adjacent
                    stringOfNumbers += number.ToString();
                    coordsOfChars.Add((i, j));
                }
                else
                {
                    // not adjacent, save the last one if there is one
                    if (!string.IsNullOrEmpty(stringOfNumbers))
                    {
                        numbersAndCoords.Add(new NumberAndCoords
                        {
                            Number = int.Parse(stringOfNumbers),
                            Coordinates = new(coordsOfChars)
                        });
                        coordsOfChars.Clear();
                    }

                    stringOfNumbers = number.ToString();
                    coordsOfChars.Add((i, j));
                }
            }
            lastRow = i;
            lastCol = j;

            if (coordinate.Key == lastCoordinate)
            {
                // last coordinate, save the last one if there is one
                if (!string.IsNullOrEmpty(stringOfNumbers))
                {
                    numbersAndCoords.Add(new NumberAndCoords
                    {
                        Number = int.Parse(stringOfNumbers),
                        Coordinates = new(coordsOfChars)
                    });
                    coordsOfChars.Clear();
                }
            }
        }

        List<NumberAndCoords> correctNumberAndCoords = [];

        // console write the numbers and their coordinates
        foreach (var numberAndCoords in numbersAndCoords)
        {
            bool madeMatch = false;
            foreach (var coords in numberAndCoords.Coordinates)
            {
                foreach (var item in coordinatesOfNumbersAdjacentToSymbol)
                {
                    (int x, int y, int c) = item;
                    (int, int) itemCoords = (x, y);
                    if (coords == itemCoords)
                    {
                        numberAndCoords.SymbolNumber = c;
                        correctNumberAndCoords.Add(numberAndCoords);
                        madeMatch = true;
                        break;
                    }
                }
                if (madeMatch)
                    break;
            }
        }

        int sum = 0;


        foreach (var symbol in locationOfSymbols)
        {
            var (i, j) = symbol.Key;
            var c = symbol.Value;

            List<NumberAndCoords> numbersAndCoordsForSymbol = [];

            foreach (var item in correctNumberAndCoords)
            {
                if (item.SymbolNumber == c)
                {
                    numbersAndCoordsForSymbol.Add(item);
                }
            }

            if (numbersAndCoordsForSymbol.Count == 2)
            {
                int baseProduct = 1;
                foreach (var item in numbersAndCoordsForSymbol)
                {
                    baseProduct *= item.Number;
                }
                sum += baseProduct;
            }
        }

        return sum;
    }
}

public record NumberAndCoords
{
    public int Number { get; set; }
    public string NumberAsString { get { return Number.ToString(); } }
    public List<(int, int)> Coordinates { get; set; }

    public int SymbolNumber { get; set; }
}
