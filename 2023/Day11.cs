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

Console.WriteLine("Result part 1: " + result);

// PART 2

var emptyColumns2 = new List<int>();
var emptyLines2 = new List<int>();
var galaxiesCoord2 = new List<(long, long)>();
var expand = 1000000L - 1L;

for (int i = 0; i < input.Count; i++)
{
    if (input[i].All(p => p == '.')) emptyLines2.Add(i);
}

for (int i = 0; i < input.First().Length; i++)
{
    if (input.All(p => p[i] == '.')) emptyColumns2.Add(i);
}

for (var i = 0; i < input.Count; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {
        if (input[i][j] == '#')
            galaxiesCoord2.Add((emptyLines2.Count(_ => i > _) * expand + i, emptyColumns2.Count(_ => j > _) * expand + j));
    }
}

var result2 = 0L;
for (var i = 0; i < galaxiesCoord2.Count - 1; i++)
{
    for (var j = i + 1; j < galaxiesCoord2.Count; j++)
    {
        var aux1 = galaxiesCoord2[j].Item1 - galaxiesCoord2[i].Item1;
        var aux2 = Math.Abs(galaxiesCoord2[j].Item2 - galaxiesCoord2[i].Item2);
        result2 += aux1 + aux2;
    }
}

Console.WriteLine("Result part 2: " + result2);