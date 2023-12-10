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
