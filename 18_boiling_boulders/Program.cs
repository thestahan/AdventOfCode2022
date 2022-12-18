string filePath = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(filePath);

var map = inputData
    .Select(x => x.Split(','))
    .Select(x => new Cube { X = int.Parse(x[0]), Y = int.Parse(x[1]), Z = int.Parse(x[2]) })
    .ToList();

for (int i = 0; i < map.Count; i++)
{
    var currentCube = map[i];

    for (int j = i; j < map.Count; j++)
    {
        if (i == j)
        {
            continue;
        }

        var secondCube = map[j];

        if (AreCubesConnected(currentCube, secondCube))
        {
            currentCube.ConnectedSides++;
            secondCube.ConnectedSides++;
        }
    }
}

Console.WriteLine($"Part one: {map.Sum(x => x.DisconnectedSides)}");

bool AreCubesConnected(Cube firstCube, Cube secondCube)
{
    if ((firstCube.X == secondCube.X && firstCube.Y == secondCube.Y) &&
        Math.Abs(firstCube.Z - secondCube.Z) <= 1)
    {
        return true;
    }
    else if ((firstCube.X == secondCube.X && firstCube.Z == secondCube.Z) &&
            Math.Abs(firstCube.Y - secondCube.Y) <= 1)
    {
        return true;
    }
    else if ((firstCube.Y == secondCube.Y && firstCube.Z == secondCube.Z) &&
            Math.Abs(firstCube.X - secondCube.X) <= 1)
    {
        return true;
    }

    return false;
}

class Cube
{
    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public int ConnectedSides { get; set; }

    public int DisconnectedSides =>
        6 - ConnectedSides;
}