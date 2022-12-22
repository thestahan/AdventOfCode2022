string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

Console.WriteLine($"Part one: {SolvePartOne(inputData)}");
Console.WriteLine($"Part two: {SolvePartTwo(inputData, 811589153, 10)}");

static long SolvePartOne(IEnumerable<string> input)
{
    var numbersUnchanged = input.Select(x => new Number { Value = int.Parse(x) }).ToList();

    var numbers = numbersUnchanged.Select(x => x).ToList();

    MixNumbers(numbersUnchanged, numbers);

    int zeroIndex = numbers.IndexOf(numbers.First(x => x.Value == 0));

    return numbers[(1000 + zeroIndex) % numbers.Count].Value +
        numbers[(2000 + zeroIndex) % numbers.Count].Value +
        numbers[(3000 + zeroIndex) % numbers.Count].Value;
}

static long SolvePartTwo(IEnumerable<string> input, long decryptionKey, int iterations)
{
    var numbersUnchanged = input.Select(x => new Number { Value = int.Parse(x) * decryptionKey }).ToList();

    var numbers = numbersUnchanged.Select(x => x).ToList();

    for (int i = 0; i < iterations; i++)
    {
        MixNumbers(numbersUnchanged, numbers);
    }

    int zeroIndex = numbers.IndexOf(numbers.First(x => x.Value == 0));

    return numbers[(1000 + zeroIndex) % numbers.Count].Value +
        numbers[(2000 + zeroIndex) % numbers.Count].Value +
        numbers[(3000 + zeroIndex) % numbers.Count].Value;
}

static void MixNumbers(List<Number> numbersUnchanged, List<Number> numbers)
{
    for (int i = 0; i < numbers.Count; i++)
    {
        var number = numbersUnchanged[i];

        int oldIndex = numbers.IndexOf(number);
        int newIndex = (int)((oldIndex + number.Value) % (numbers.Count - 1));

        if (newIndex <= 0 && oldIndex + number.Value != 0)
        {
            newIndex = numbers.Count - 1 + newIndex;
        }

        numbers.RemoveAt(oldIndex);
        numbers.Insert(newIndex, number);
    }
}

class Number
{
    public long Value { get; set; }
}