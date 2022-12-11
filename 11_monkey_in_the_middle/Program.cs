string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

var monkeys = new List<Monkey>();

for (int i = 0; i < inputData.Length; i += 7)
{
    var line = inputData[i];

    var itemsQueue = new Queue<long>();

    inputData[i + 1]
        .Split()
        .Where(x => !string.IsNullOrWhiteSpace(x) && char.IsDigit(x.First()))
        .Select(x => int.Parse(x.Replace(",", string.Empty)))
        .ToList()
        .ForEach(x => itemsQueue.Enqueue(x));

    var operationData = inputData[i + 2].Split().TakeLast(2).ToList();

    var operation = GetOperationByInputData(operationData);

    var testDivisibleBy = int.Parse(inputData[i + 3].Split().Last());

    var monkeyToThrowIfTrue = int.Parse(inputData[i + 4].Split().Last());
    var monkeyToThrowIfFalse = int.Parse(inputData[i + 5].Split().Last());

    monkeys.Add(new Monkey
    {
        Items = itemsQueue,
        Operation = operation,
        TestDivisibleBy = testDivisibleBy,
        MonkeyToThrowIfTrue = monkeyToThrowIfTrue,
        MonkeyToThrowIfFalse = monkeyToThrowIfFalse
    });
}

int roundsCount = 10000;

// multiply all the monkeys' mod values
int m = monkeys.Select(x => x.TestDivisibleBy).Aggregate((a, b) => a * b);

for (int i = 0; i < roundsCount; i++)
{
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.Any())
        {
            long item = monkey.Items.Dequeue();

            // getting mod of previously calculated m doesn't change division rules
            long newWorryLevel = GetNewWorryLevel(monkey.Operation, item) % m;

            if (newWorryLevel % monkey.TestDivisibleBy == 0)
            {
                monkeys[monkey.MonkeyToThrowIfTrue].Items.Enqueue(newWorryLevel);
            }
            else
            {
                monkeys[monkey.MonkeyToThrowIfFalse].Items.Enqueue(newWorryLevel);
            }

            monkey.TotalItemsInspectedCount++;
        }
    }
}

long monkeyBusiness = monkeys
    .Select(x => (long)x.TotalItemsInspectedCount)
    .OrderByDescending(x => x)
    .Take(2)
    .Aggregate((a, b) => a * b);

Console.WriteLine($"Part two: {monkeyBusiness}");

long GetNewWorryLevel(Operation operation, long currentWorryLevel)
{
    long newWorryLevel = currentWorryLevel;

    if (operation.Self)
    {
        newWorryLevel = operation.Sign == OperationSign.Multiply ?
        currentWorryLevel * currentWorryLevel :
        currentWorryLevel + currentWorryLevel;
    }
    else
    {
        newWorryLevel = operation.Sign == OperationSign.Multiply ?
        currentWorryLevel * (int)operation.Number! :
        currentWorryLevel + (int)operation.Number!;
    }

    return newWorryLevel;
}

Operation GetOperationByInputData(IEnumerable<string> data)
{
    var sign = data.ElementAt(0) == "*" ? OperationSign.Multiply : OperationSign.Add;

    if (int.TryParse(data.ElementAt(1), out int number))
    {
        return new Operation { Number = number, Sign = sign };
    }

    return new Operation { Sign = sign, Self = true };
}

class Monkey
{
    public Queue<long> Items { get; set; } = new Queue<long>();

    public int TotalItemsInspectedCount { get; set; }

    public Operation Operation { get; set; }

    public int TestDivisibleBy { get; set; }

    public int MonkeyToThrowIfTrue { get; set; }

    public int MonkeyToThrowIfFalse { get; set; }
}

class Operation
{
    public OperationSign Sign { get; set; }

    public int? Number { get; set; }

    public bool Self { get; set; }
}

enum OperationSign
{
    Add,
    Multiply
}