string filePath = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(filePath);

int maxSize = 1000;

var map = new int[maxSize, maxSize]; // 0 -> nothing, 1 -> path

int maxY = 0;

foreach (var path in inputData)
{
    var pathPoints = path
        .Split("->", StringSplitOptions.TrimEntries)
        .Select(x => x.Split(","))
        .Select(x => new Point { X = int.Parse(x[0]), Y = int.Parse(x[1]) });

    maxY = Math.Max(maxY, pathPoints.Max(x => x.Y));

    MarkPath(pathPoints);
}

maxY += 2;

for (int i = 0; i < maxSize; i++)
{
    map[maxY, i] = 1;
}

int unitsOfSand = 0;

while (true)
{
    var sandPoint = new Point { X = 500, Y = 0 };
    bool reachedMax = false;

    MoveSandOnePointDown(sandPoint, ref reachedMax);

    unitsOfSand++;

    if (reachedMax)
    {
        break;
    }
}

Console.WriteLine($"Part two: {unitsOfSand}");

void MarkPath(IEnumerable<Point> pathPoints)
{
    for (int i = 0; i < pathPoints.Count() - 1; i++)
    {
        var currentPoint = pathPoints.ElementAt(i);
        var nextPoint = pathPoints.ElementAt(i + 1);

        bool isLineHorizontal = currentPoint.Y == nextPoint.Y;

        if (isLineHorizontal)
        {
            int startX = currentPoint.X > nextPoint.X ? nextPoint.X : currentPoint.X;
            int endX = currentPoint.X > nextPoint.X ? currentPoint.X : nextPoint.X;

            for (int j = startX; j <= endX; j++)
            {
                map[currentPoint.Y, j] = 1;
            }
        }
        else
        {
            int startY = currentPoint.Y > nextPoint.Y ? nextPoint.Y : currentPoint.Y;
            int endY = currentPoint.Y > nextPoint.Y ? currentPoint.Y : nextPoint.Y;

            for (int j = startY; j <= endY; j++)
            {
                map[j, currentPoint.X] = 1;
            }
        }
    }
}

void MoveSandOnePointDown(Point sandPoint, ref bool reachedMax)
{
    //if (sandPoint.Y + 1 == maxSize)
    //{
    //    reachedMax = true;

    //    return;
    //}

    bool pointBelowIsFree = map[sandPoint.Y + 1, sandPoint.X] == 0;
    bool pointBelowLeftIsFree = map[sandPoint.Y + 1, sandPoint.X - 1] == 0;
    bool pointBelowRightIsFree = map[sandPoint.Y + 1, sandPoint.X + 1] == 0;

    if (pointBelowIsFree)
    {
        sandPoint.Y += 1;

        MoveSandOnePointDown(sandPoint, ref reachedMax);
    }
    else if (pointBelowLeftIsFree)
    {
        sandPoint.X -= 1;
        sandPoint.Y += 1;

        MoveSandOnePointDown(sandPoint, ref reachedMax);
    }
    else if (pointBelowRightIsFree)
    {
        sandPoint.X += 1;
        sandPoint.Y += 1;

        MoveSandOnePointDown(sandPoint, ref reachedMax);
    }
    else
    {
        map[sandPoint.Y, sandPoint.X] = 1;
    }

    if (sandPoint.X == 500 && sandPoint.Y == 0)
    {
        reachedMax = true;
    }
}

class Point
{
    public int X { get; set; }

    public int Y { get; set; }
}