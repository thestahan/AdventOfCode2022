string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

(int x, int y) endPoint = (0, 0);

int rows = inputData.Length;
int columns = inputData.First().Length;

var heightmap = new int[rows, columns];

var startPoints = new List<(int x, int y)>();

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        heightmap[i, j] = inputData[i][j];

        if (heightmap[i, j] == 'a')
        {
            startPoints.Add((j, i));
        }
        else if (heightmap[i, j] == 'E')
        {
            endPoint.x = j;
            endPoint.y = i;

            heightmap[i, j] = (int)'z';
        }
    }
}

var shortestPath = startPoints.Select(x => GetShortestPath(x, endPoint)).Min();

Console.WriteLine($"Part two: {shortestPath}");

int GetShortestPath((int x, int y) startPoint, (int x, int y) endPoint)
{
    var visited = new bool[rows, columns];
    var distances = new int[rows, columns];
    bool foundEndpoint = false;

    var queue = new Queue<(int x, int y)>();
    queue.Enqueue(startPoint);
    distances[startPoint.y, startPoint.x] = 0;
    visited[startPoint.y, startPoint.x] = true;

    while (queue.Any())
    {
        var currentPoint = queue.Dequeue();

        var neighbours = GetAvailableNeighbours(currentPoint);

        foreach (var neighbour in neighbours)
        {
            if (!visited[neighbour.y, neighbour.x])
            {
                visited[neighbour.y, neighbour.x] = true;
                distances[neighbour.y, neighbour.x] = distances[currentPoint.y, currentPoint.x] + 1;

                if (neighbour.x == endPoint.x && neighbour.y == endPoint.y)
                {
                    foundEndpoint = true;
                }

                queue.Enqueue(neighbour);
            }
        }
    }

    return foundEndpoint ? distances[endPoint.y, endPoint.x] : int.MaxValue;
}

List<(int x, int y)> GetAvailableNeighbours((int x, int y) point)
{
    var availableNeighbours = new List<(int x, int y)>();

    // top point available
    if (point.y > 0 &&
        (heightmap[point.y - 1, point.x] - heightmap[point.y, point.x] <= 1))
    {
        availableNeighbours.Add((point.x, point.y - 1));
    }

    // botttom
    if (point.y < rows - 1 &&
        (heightmap[point.y + 1, point.x] - heightmap[point.y, point.x] <= 1))
    {
        availableNeighbours.Add((point.x, point.y + 1));
    }

    // left
    if (point.x > 0 &&
        (heightmap[point.y, point.x - 1] - heightmap[point.y, point.x] <= 1))
    {
        availableNeighbours.Add((point.x - 1, point.y));
    }


    // right
    if (point.x < columns - 1 &&
        (heightmap[point.y, point.x + 1] - heightmap[point.y, point.x] <= 1))
    {
        availableNeighbours.Add((point.x + 1, point.y));
    }

    return availableNeighbours;
}