string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

var registerValues = new List<int>();

int registerValue = 1;

foreach (var instruction in inputData)
{
    var instructionData = instruction.Split();

    if (instructionData.First() == "addx")
    {
        registerValues.Add(registerValue);

        registerValues.Add(registerValue);

        registerValue += int.Parse(instructionData[1]);
    }
    else if (instructionData.First() == "noop")
    {
        registerValues.Add(registerValue);
    }
}

int signalStrengths = 0;

for (int i = 19; i < registerValues.Count; i += 40)
{
    signalStrengths += ((i + 1) * registerValues[i]);
}

Console.WriteLine($"Part one: {signalStrengths}");

var litPixels = new List<(int x, int y)>();

int currentCycle = 0;

for (int i = 0; i < 6; i++)
{
    for (int j = 0; j < 40; j++)
    {
        var spritePosition = registerValues[currentCycle] - 1;

        if (spritePosition == j || spritePosition + 1 == j || spritePosition + 2 == j)
        {
            litPixels.Add((i, j));
        }

        currentCycle++;
    }
}

for (int i = 0; i < 6; i++)
{
    for (int j = 0; j < 40; j++)
    {
        if (litPixels.Contains((i, j)))
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }

    Console.WriteLine();
}