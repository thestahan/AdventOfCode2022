string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

var calories = new List<int>();

int currentElf = 0;

foreach (var line in inputData)
{
    if (string.IsNullOrEmpty(line))
    {
        calories.Add(currentElf);
        currentElf = 0;
    }
    else
    {
        currentElf += Int32.Parse(line);
    }
}

calories = calories.OrderByDescending(x => x).ToList();

// Part 1
int topCalories = calories.First();
Console.WriteLine(topCalories);

// Part 2
int sumOfTop3Calories = calories.Take(3).Sum();
Console.WriteLine(sumOfTop3Calories);