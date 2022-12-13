using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

var indexesOfCorrectPairs = new List<int>();

int pairIndex = 1;

var packets = new List<JArray>();

for (int i = 0; i < inputData.Length; i += 3)
{
    var firstPacket = JsonConvert.DeserializeObject<JArray>(inputData[i]);

    var secondPacket = JsonConvert.DeserializeObject<JArray>(inputData[i + 1]);

    var correct = CompareJArrays(firstPacket, secondPacket);

    if (correct == true)
    {
        indexesOfCorrectPairs.Add(pairIndex);
    }

    pairIndex++;

    packets.Add(firstPacket);
    packets.Add(secondPacket);
}

Console.WriteLine($"Part one: {indexesOfCorrectPairs.Sum()}");

var dividerPackets = new List<JArray>()
{
    new JArray(new JArray(2)),
    new JArray(new JArray(6)),
};

packets.AddRange(dividerPackets);

packets.Sort((left, right) => CompareJArrays(left, right) == true ? -1 : 1);

int partTwoAns = ((packets.IndexOf(dividerPackets[0]) + 1) * (packets.IndexOf(dividerPackets[1]) + 1));

Console.WriteLine($"Part two: {partTwoAns}");

static bool? CompareJArrays(object left, object right)
{
    if (left is JValue leftValue && right is JValue rightValue)
    {
        int leftInt = (int)leftValue;
        int rightInt = (int)rightValue;

        return leftInt == rightInt ? null : leftInt < rightInt;
    }

    if (left is not JArray leftArray)
    {
        leftArray = new JArray(left);
    }

    if (right is not JArray rightArray)
    {
        rightArray = new JArray(right);
    }

    for (var i = 0; i < Math.Min(leftArray.Count, rightArray.Count); i++)
    {
        var result = CompareJArrays(leftArray[i], rightArray[i]);

        if (result.HasValue)
        {
            return result.Value;
        }
    }

    if (leftArray.Count < rightArray.Count)
    {
        return true;
    }

    if (leftArray.Count > rightArray.Count)
    {
        return false;
    }

    return null;
}