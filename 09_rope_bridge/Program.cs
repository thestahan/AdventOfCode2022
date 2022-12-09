string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

var motions = inputData
    .Select(x => (x.First(), int.Parse(x.Split()[1]))).ToList();

Console.WriteLine($"Part one: {GetVisitedPlacesCount(motions, 10)}");

void GetNewCoordsByDirection(ref int x, ref int y, char direction)
{
    switch (direction)
    {
        case 'U':
            y--;
            break;
        case 'R':
            x++;
            break;
        case 'L':
            x--;
            break;
        case 'D':
            y++;
            break;
    }
}

void GetNewTailsCoordsByHeadsCoords(ref int tailsX, ref int tailsY, int headsX, int headsY)
{
    int xDifference = headsX - tailsX;
    int yDifference = headsY - tailsY;

    if ((xDifference == 1 || xDifference == -1 || xDifference == 0) &&
        (yDifference == 1 || yDifference == -1 || yDifference == 0))
    {
        return;
    }

    if (yDifference == 0)
    {
        if (xDifference > 0)
        {
            tailsX++;
        }
        else
        {
            tailsX--;
        }
    }
    else if (xDifference == 0)
    {
        if (yDifference > 0)
        {
            tailsY++;
        }
        else
        {
            tailsY--;
        }
    }
    else if (xDifference > 0)
    {
        if ((xDifference == 2 && yDifference == -1) ||
            (xDifference == 1 && yDifference == -2) ||
            (xDifference == 2 && yDifference == -2))
        {
            tailsX++;
            tailsY--;
        }
        else if ((xDifference == 2 && yDifference == 1) ||
                 (xDifference == 1 && yDifference == 2) ||
                 (xDifference == 2 && yDifference == 2))
        {
            tailsX++;
            tailsY++;
        }
    }
    else if (xDifference < 0)
    {
        if ((xDifference == -2 && yDifference == -1) ||
            (xDifference == -1 && yDifference == -2) ||
            (xDifference == -2 && yDifference == -2))
        {
            tailsX--;
            tailsY--;
        }
        else if ((xDifference == -2 && yDifference == 1) ||
                 (xDifference == -1 && yDifference == 2) ||
                 (xDifference == -2 && yDifference == 1))
        {
            tailsX--;
            tailsY++;
        }
    }
}

int GetVisitedPlacesCount(List<(char, int)> motions, int knotsCount)
{
    var rope = new (int x, int y)[knotsCount];

    for (int i = 0; i < knotsCount; i++)
    {
        rope[i].x = 0; rope[i].y = 0;
    }

    var visited = new List<(int x, int y)>();

    foreach ((char direction, int steps) in motions)
    {
        for (int i = 0; i < steps; i++)
        {
            GetNewCoordsByDirection(ref rope[0].x, ref rope[0].y, direction);

            for (int j = 1; j < knotsCount; j++)
            {
                GetNewTailsCoordsByHeadsCoords(ref rope[j].x, ref rope[j].y, rope[j - 1].x, rope[j - 1].y);

                if (j == knotsCount - 1)
                {
                    if (!visited.Contains((rope[j].x, rope[j].y)))
                    {
                        visited.Add((rope[j].x, rope[j].y));
                    }
                }
            }
        }
    }

    return visited.Count;
}