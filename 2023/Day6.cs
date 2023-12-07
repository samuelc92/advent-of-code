var input = File.ReadAllLines("input.txt").ToList();

//PART 1
var timesInput = input[0]
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Where(p => int.TryParse(p, out _))
    .Select(int.Parse)
    .ToList();

var distancesInput = input[1]
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Where(p => int.TryParse(p, out _))
    .Select(int.Parse)
    .ToList();

var results = new List<int>();
for (var i = 0; i < timesInput.Count; i ++)
{
    var times = Enumerable.Range(1, timesInput[i]).ToList();
    (var min, var max) = (0, 0);
    for (var j = 0; j < times.Count; j ++)
    {
         if (min > 0 && max > 0) break;
        var minAux = times[j];
        var maxAux = times[times.Count - times[j]];
        if (min == 0 && (timesInput[i] - minAux) * minAux > distancesInput[i])
            min = minAux;
        if (max == 0 && (timesInput[i] - maxAux) * maxAux > distancesInput[i])
            max = maxAux;
    }

    results.Add(max - min + 1);
}

Console.WriteLine($"Result part 1: {results.Aggregate(1, (acc, x) => acc * x)}");

// PART 2
var timesInput1 = input[0]
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Where(p => int.TryParse(p, out _))
    .Aggregate(string.Empty, (acc, x) => acc + x, int.Parse);

var distancesInput1 = input[1]
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Where(p => int.TryParse(p, out _))
    .Aggregate(string.Empty, (acc, x) => acc + x, long.Parse);

var times2 = Enumerable.Range(1, timesInput1).ToList();
(var min2, var max2) = (0L, 0L);
for (var j = 0; j < times2.Count; j ++)
{
    if (min2 > 0 && max2 > 0) break;
    var minAux = Convert.ToInt64(times2[j]);
    var maxAux = Convert.ToInt64(times2[times2.Count - times2[j]]);
    if (min2 == 0 && (timesInput1 - minAux) * minAux > distancesInput1)
        min2 = minAux;
    if (max2 == 0 && (timesInput1 - maxAux) * maxAux > distancesInput1)
        max2 = maxAux;
}

var result2 = max2 - min2 + 1;

Console.WriteLine($"Result part 2: {result2}");