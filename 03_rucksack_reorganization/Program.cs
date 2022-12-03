string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

int sumOfPriorities = 0;

foreach (var rucksack in inputData)
{
    var compartments = rucksack.Chunk(rucksack.Length / 2);

    char priority = compartments.ElementAt(0).Intersect(compartments.ElementAt(1)).First();

    if (char.IsUpper(priority))
    {
        sumOfPriorities += ((int)priority - 38);
    }
    else if (char.IsLower(priority))
    {
        sumOfPriorities += ((int)priority - 96);
    }
}

Console.WriteLine($"Part one answer: {sumOfPriorities}");

var elvesGroups = inputData.Chunk(3);

sumOfPriorities = 0;

foreach (var elvesGroup in elvesGroups)
{
    var priority = elvesGroup.Aggregate((previousGroup, nextGroup) => string.Concat(previousGroup.Intersect(nextGroup))).First();

    if (char.IsUpper(priority))
    {
        sumOfPriorities += ((int)priority - 38);
    }
    else if (char.IsLower(priority))
    {
        sumOfPriorities += ((int)priority - 96);
    }
}

Console.WriteLine($"Part two answer: {sumOfPriorities}");