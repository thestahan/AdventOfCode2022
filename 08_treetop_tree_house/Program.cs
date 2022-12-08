string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

int columnsCount = inputData.First().Length;
int rowsCount = inputData.Length;

var treesMap = new int[inputData.Length, columnsCount];

for (int i = 0; i < rowsCount; i++)
{
    for (int j = 0; j < columnsCount; j++)
    {
        treesMap[i, j] = int.Parse(inputData[i][j].ToString());
    }
}

int scenicScore = 0;

for (int i = 1; i < rowsCount - 1; i++)
{
    for (int j = 1; j < columnsCount - 1; j++)
    {
        int tree = treesMap[i, j];

        int treesVisibleFromTop = 0;
        int treesVisibleFromLeft = 0;
        int treesVisibleFromBottom = 0;
        int treesVisibleFromRight = 0;

        for (int k = i - 1; k >= 0; k--)
        {
            if (treesMap[k, j] >= tree)
            {
                treesVisibleFromTop++;

                break;
            }

            treesVisibleFromTop++;
        }

        for (int k = i + 1; k <= rowsCount - 1; k++)
        {
            if (treesMap[k, j] >= tree)
            {
                treesVisibleFromBottom++;

                break;
            }

            treesVisibleFromBottom++;
        }

        for (int k = j - 1; k >= 0; k--)
        {
            if (treesMap[i, k] >= tree)
            {
                treesVisibleFromLeft++;

                break;
            }

            treesVisibleFromLeft++;
        }

        for (int k = j + 1; k <= columnsCount - 1; k++)
        {
            if (treesMap[i, k] >= tree)
            {
                treesVisibleFromRight++;

                break;
            }

            treesVisibleFromRight++;
        }

        int totalVisibleTrees = treesVisibleFromLeft * treesVisibleFromRight * treesVisibleFromTop * treesVisibleFromBottom;

        scenicScore = Math.Max(scenicScore, totalVisibleTrees);
    }
}

Console.WriteLine($"Part two: {scenicScore}");