using System.Text;

string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

var stacksCount = int.Parse(inputData
    .First(x => x.Contains('1'))
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Last());

var stacksTemp = new List<Stack<char>>();

for (int i = 0; i < stacksCount; i++)
{
    stacksTemp.Add(new Stack<char>());
}

var instructions = new List<(int move, int from, int to)>();

foreach (var line in inputData)
{
    if (line.Contains('['))
    {
        for (int i = 0; i < stacksCount; i++)
        {
            string crate = line.Substring(i * 4, 3);

            if (!string.IsNullOrWhiteSpace(crate))
            {
                stacksTemp[i].Push(crate[1]);
            }
        }
    }
    else if (line.StartsWith("move"))
    {
        var instructionsLineData = line.Split();

        var move = int.Parse(instructionsLineData.ElementAt(1));
        var from = int.Parse(instructionsLineData.ElementAt(3));
        var to = int.Parse(instructionsLineData.ElementAt(5));

        instructions.Add((move, from, to));
    }
}

var stacks = new List<Stack<char>>();

foreach (var stackTemp in stacksTemp)
{
    var stack = new Stack<char>();

    while (stackTemp.Any())
    {
        stack.Push(stackTemp.Pop());
    }

    stacks.Add(stack);
}

// Part 1
foreach (var (move, from, to) in instructions)
{
    for (int i = 0; i < move; i++)
    {
        stacks[to - 1].Push(stacks[from - 1].Pop());
    }
}

var messagePartOne = new StringBuilder();

foreach (var stack in stacks)
{
    messagePartOne.Append(stack.Pop());
}

Console.WriteLine($"Part one: {messagePartOne.ToString()}");

// Part 2
foreach (var (move, from, to) in instructions)
{
    var itemsQueue = new List<char>();

    for (int i = 0; i < move; i++)
    {
        var item = stacks[from - 1].Pop();

        itemsQueue.Add(item);
    }

    for (int i = itemsQueue.Count - 1; i >= 0; i--)
    {
        stacks[to - 1].Push(itemsQueue[i]);
    }
}

var messagePartTwo = new StringBuilder();

foreach (var stack in stacks)
{
    messagePartTwo.Append(stack.Pop());
}

Console.WriteLine($"Part one: {messagePartTwo.ToString()}");