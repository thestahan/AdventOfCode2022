string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path).First();

var charsQueue = new Queue<char>();

for (int i = 0; i < 14; i++)
{
    charsQueue.Enqueue(inputData[i]);
}

for (int i = 14; i < inputData.Length; i++)
{
    var test = charsQueue.Select(x => x).ToList();

    if (test.GroupBy(x => x).ToDictionary(y => y.Key, y => y.Count()).All(x => x.Value == 1))
    {
        Console.WriteLine(i);

        break;
    }

    charsQueue.Dequeue();
    charsQueue.Enqueue(inputData[i]);
}