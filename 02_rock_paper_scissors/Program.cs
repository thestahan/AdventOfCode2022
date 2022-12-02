internal class Program
{
    private static void Main(string[] args)
    {
        string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

        var inputData = File.ReadAllLines(path);

        var rock = new Shape { Beats = "C", Draws = "A", Loses = "B" };

        var paper = new Shape { Beats = "A", Draws = "B", Loses = "C" };

        var scissors = new Shape { Beats = "B", Draws = "C", Loses = "A" };

        var shapes = new List<Shape> { rock, paper, scissors };

        string win = "Z";
        string draw = "Y";

        int totalScore = 0;

        foreach (var round in inputData)
        {
            var roundData = round.Split(" ");

            var oponent = roundData.ElementAt(0);
            var roundGoal = roundData.ElementAt(1);

            var oponentShape = shapes.First(x => x.Draws == oponent);

            if (roundGoal == win)
            {
                totalScore += GetPointsFromRound(roundGoal, oponentShape.Loses);
            }
            else if (roundGoal == draw)
            {
                totalScore += GetPointsFromRound(roundGoal, oponentShape.Draws);
            }
            else
            {
                totalScore += GetPointsFromRound(roundGoal, oponentShape.Beats);
            }
        }

        Console.WriteLine(totalScore);
    }

    private static int GetPointsFromRound(string roundEnd, string shape)
    {
        int pointsFromRoundEnd = roundEnd switch
        {
            "X" => 0,
            "Y" => 3,
            "Z" => 6
        };

        int pointsFromShape = shape switch
        {
            "A" => 1,
            "B" => 2,
            "C" => 3,
        };

        return pointsFromRoundEnd + pointsFromShape;
    }
}

class Shape
{
    public string Beats { get; set; }

    public string Loses { get; set; }

    public string Draws { get; set; }
}