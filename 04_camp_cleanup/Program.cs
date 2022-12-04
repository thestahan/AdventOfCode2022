string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

int totalPartOne = 0;

int totalPartTwo = 0;

foreach (var item in inputData)
{
    var elvesData = item.Split(",");

    var firstElfData = elvesData.ElementAt(0).Split("-");

    var secondElfData = elvesData.ElementAt(1).Split("-");

    var firstElfFrom = Int32.Parse(firstElfData.ElementAt(0));
    var firstElfTo = Int32.Parse(firstElfData.ElementAt(1));

    var secondElfFrom = Int32.Parse(secondElfData.ElementAt(0));
    var secondElfTo = Int32.Parse(secondElfData.ElementAt(1));

    if (firstElfFrom >= secondElfFrom && firstElfTo <= secondElfTo ||
        secondElfFrom >= firstElfFrom && secondElfTo <= firstElfTo)
    {
        totalPartOne++;
    }

    if (firstElfFrom >= secondElfFrom && firstElfTo <= secondElfTo ||
        secondElfFrom >= firstElfFrom && secondElfTo <= firstElfTo ||
        firstElfTo >= secondElfFrom && firstElfFrom <= secondElfTo)
    {
        totalPartTwo++;
    }
}

Console.WriteLine(totalPartOne);
Console.WriteLine(totalPartTwo);