string filePath = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(filePath);

var monkeys = new List<Monkey>();

foreach (var monkeyData in inputData)
{
    monkeys.Add(new Monkey { Name = monkeyData[..4] });
}

foreach (var monkeyData in inputData)
{
    var monkeyDataArr = monkeyData.Split();

    var monkey = monkeys.Where(x => x.Name == monkeyData[..4]).First();

    if (monkey.Name == "root")
    {
        monkey.Operation = new Operation
        {
            Monkey1 = monkeys.Where(x => x.Name == monkeyDataArr[1]).First(),
            Monkey2 = monkeys.Where(x => x.Name == monkeyDataArr[3]).First(),
            Sign = '=',
        };
    }
    else if (monkey.Name == "humn")
    {
    }
    else if (monkeyDataArr.Length == 2)
    {
        monkey.Number = long.Parse(monkeyDataArr[1]);
    }
    else
    {
        monkey.Operation = new Operation
        {
            Monkey1 = monkeys.Where(x => x.Name == monkeyDataArr[1]).First(),
            Monkey2 = monkeys.Where(x => x.Name == monkeyDataArr[3]).First(),
            Sign = monkeyDataArr[2][0],
        };
    }
}

var rootMonkey = monkeys.First(x => x.Name == "root");
var human = monkeys.First(x => x.Name == "humn");

while (rootMonkey.Operation.Monkey1.Number != rootMonkey.Operation.Monkey2.Number)
{
    long diff = rootMonkey.Operation.Monkey1.Number - rootMonkey.Operation.Monkey2.Number;

    if (diff > 100)
    {
        human.Number += diff / 100;
    }
    else
    {
        human.Number++;
    }
}
Console.WriteLine($"Part two: {human.Number}");

class Monkey
{
    private long? _number;

    public string Name { get; set; }

    public Operation Operation { get; set; }

    public long Number
    {
        get
        {
            if (_number.HasValue)
            {
                return _number.Value;
            }

            if (Operation is null)
            {
                return 0;
            }

            if (Operation.Sign == '+')
            {
                return Operation.Monkey1.Number + Operation.Monkey2.Number;
            }
            else if (Operation.Sign == '-')
            {
                return Operation.Monkey1.Number - Operation.Monkey2.Number;
            }
            else if (Operation.Sign == '/')
            {
                return Operation.Monkey1.Number / Operation.Monkey2.Number;
            }
            else if (Operation.Sign == '*')
            {
                return Operation.Monkey1.Number * Operation.Monkey2.Number;
            }

            return 0;
        }

        set => _number = value;
    }
}

class Operation
{
    public char Sign { get; set; }

    public Monkey Monkey1 { get; set; }

    public Monkey Monkey2 { get; set; }
}