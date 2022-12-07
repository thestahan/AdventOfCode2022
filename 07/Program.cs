string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

Directory baseDir = new Directory { Name = "/" };

Directory currentDirectory = baseDir;

Directory? newDirectory = null;

string currentDirName = "/";

foreach (var line in inputData.Skip(1))
{
    if (!line.StartsWith('$'))
    {
        if (line.StartsWith("dir"))
        {
            string dirName = line.Split()[1];

            if (!currentDirectory.Directories.Any(x => x.Name == dirName))
            {
                currentDirectory.Directories.Add(new Directory { Name = dirName, ParentDirectory = currentDirectory });
            }
        }
        else
        {
            var fileInfo = line.Split();

            if (!currentDirectory.Files.Any(x => x.name == fileInfo[1]))
            {
                currentDirectory.Files.Add((fileInfo[1], int.Parse(fileInfo[0])));
            }
        }

        continue;
    }

    if (line.StartsWith("$ cd"))
    {
        var dirName = line.Split()[2];

        currentDirName = dirName;

        if (dirName == "/")
        {
            currentDirectory = baseDir;
        }
        else if (dirName == "..")
        {
            currentDirectory = currentDirectory.ParentDirectory;
        }
        else
        {
            currentDirectory = currentDirectory.Directories.First(x => x.Name == dirName);
        }

        continue;
    }
}

currentDirectory = baseDir;

var dirsSizes = new List<int>();

int totalSizes = 0;

foreach (var dir in baseDir.Directories)
{
    GetTotalSizesRecursive(dir, ref totalSizes, dirsSizes);
}

Console.WriteLine($"Part one: {totalSizes}");

var neededForUpdate = 30000000;

var availableSpace = 70000000 - baseDir.Size;

var smallestRequiredDir = dirsSizes.Where(x => x >= (neededForUpdate - availableSpace)).Min();

Console.WriteLine($"Part two: {smallestRequiredDir}");

static void GetTotalSizesRecursive(Directory dir, ref int totalSizes, List<int> directoriesSizes)
{
    int dirSize = dir.Size;

    directoriesSizes.Add(dirSize);

    if (dirSize <= 100000)
    {
        totalSizes += dirSize;
    }

    foreach (var childDir in dir.Directories)
    {
        GetTotalSizesRecursive(childDir, ref totalSizes, directoriesSizes);
    }
}

class Directory
{
    public List<(string name, int size)> Files { get; set; }
        = new List<(string name, int size)>();

    public List<Directory> Directories { get; set; } = new List<Directory>();

    public Directory? ParentDirectory { get; set; } = null;

    public string Name { get; set; } = string.Empty;

    public int Size =>
        Files.Sum(x => x.size) + Directories.Sum(x => x.Size);
}