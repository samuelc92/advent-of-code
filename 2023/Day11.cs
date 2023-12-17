var input = File.ReadAllLines("input.txt").ToList();

// PART 1
var emptyColumns = new List<int>();
var emptyLines = new List<int>();
var galaxiesCoord = new List<(int, int)>();

for (int i = 0; i < input.Count; i++)
{
    if (input[i].All(p => p == '.')) emptyLines.Add(i);
}

for (int i = 0; i < input.First().Length; i++)
{
    if (input.All(p => p[i] == '.')) emptyColumns.Add(i);
}

for (var i = 0; i < input.Count; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {
        if (input[i][j] == '#')
            galaxiesCoord.Add((i + emptyLines.Count(p => i > p), j + emptyColumns.Count(p => j > p)));
    }
}

var result = 0;
for (var i = 0; i < galaxiesCoord.Count - 1; i++)
{
    for (var j = i + 1; j < galaxiesCoord.Count; j++)
    {
        var aux1 = galaxiesCoord[j].Item1 - galaxiesCoord[i].Item1;
        var aux2 = Math.Abs(galaxiesCoord[j].Item2 - galaxiesCoord[i].Item2);
        result += aux1 + aux2;
    }
}

Console.WriteLine(result);
