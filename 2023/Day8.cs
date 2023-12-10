var input = File.ReadAllLines("input.txt").ToList();

// PART 1
var instructions = input.First();
var network = input.Skip(2).ToList();
var map = new Dictionary<string, (string, string)>();
var node = "AAA";
var steps = 0L;
var count = 0;

foreach (var n in network)
{
    var no = n.Split("=", StringSplitOptions.RemoveEmptyEntries)[0].Trim();
    var left = n.Split("=", StringSplitOptions.RemoveEmptyEntries)[1].Split(",", StringSplitOptions.RemoveEmptyEntries)[0].Replace("(", "").Trim();
    var right = n.Split("=", StringSplitOptions.RemoveEmptyEntries)[1].Split(",", StringSplitOptions.RemoveEmptyEntries)[1].Replace(")", "").Trim();
    map.Add(no, (left, right));
}

while (node != "ZZZ")
{
    var m = map[node];
    if (instructions[count] == 'L')
        node = m.Item1;
    else if (instructions[count] == 'R')
        node = m.Item2;
    steps++;
    count = count + 1 >= instructions.Length ? 0 : count + 1;
}

Console.WriteLine("Result part 1: " + steps);

// PART 2

var startPoints = new List<string>();
foreach (var n in network)
{
    var no = n.Split("=", StringSplitOptions.RemoveEmptyEntries)[0].Trim();
    if (no.EndsWith("A")) startPoints.Add(no);
}

var nodesSteps = new List<long>();

for (var i = 0; i < startPoints.Count; i++)
{
    var stepsAux = 0;
    var node2 = startPoints[i];
    while (!node2.EndsWith("Z"))
    {
        var m = map[node2];
        if (instructions[(int)stepsAux % instructions.Length] == 'L')
            node2 = m.Item1;
        else
            node2 = m.Item2;
        stepsAux++;
    }
    nodesSteps.Add(stepsAux);
}

Console.WriteLine("Result part 2: " + nodesSteps.Aggregate(1L, FindLCM));

long FindGCD(long a, long b)
{
    if (a == 0 || b == 0) return Math.Max(a, b);
    return (a % b == 0) ? b : FindGCD(b, a % b);
}

long FindLCM(long a, long b) => a * b / FindGCD(a, b);
