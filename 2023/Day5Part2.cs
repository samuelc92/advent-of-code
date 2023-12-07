// See https://aka.ms/new-console-template for more information
var input = File.ReadAllLines("input.txt").ToList();

// PART 2
var sources = input
        .First()
        .Split(":")[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .Chunk(2)
        .Select(p => (p[0], p[0] + p[1] - 1))
        .ToList();

var count = 2;

while (count < input.Count)
{
    var map = input.Skip(count).TakeWhile(p => !string.IsNullOrWhiteSpace(p)).ToList();
    sources = GetDestination(map.Skip(1).ToList(), sources);
    count += map.Count + 1;
}

var result = sources
    .Aggregate(new List<long>(), (acc,x) =>
    {
        acc.Add(x.Item1); 
        acc.Add(x.Item2);
        return acc;
    })
    .Min();

Console.WriteLine($"Result part 2: {result}");

List<(long, long)> GetDestination(List<string> lines, List<(long, long)> chunks)
{
    var offset = 0;
    foreach(var line in lines)
    {
        var destinationRange = long.Parse(line.Split(" ")[0]);
        var sourceRange = long.Parse(line.Split(" ")[1]);
        var rangeLength = long.Parse(line.Split(" ")[2]);
        var newChunks = new List<(long, long)>();
        newChunks.AddRange(chunks.Take(offset).ToList());

        foreach(var chunk in chunks.Skip(offset))
        {
            var chunk1Changed = chunk.Item1 >= sourceRange && chunk.Item1 <= sourceRange + rangeLength - 1; 
            var chunk2Changed = chunk.Item2 >= sourceRange && chunk.Item2 <= sourceRange + rangeLength - 1; 
            if (chunk1Changed && chunk2Changed)
            {
                newChunks.Insert(0,(chunk.Item1 - sourceRange + destinationRange, chunk.Item2 - sourceRange + destinationRange));
                offset ++;
            }
            else if (chunk1Changed && !chunk2Changed)
            {
                var aux = sourceRange + rangeLength - 1 - chunk.Item1;
                var item1 = chunk.Item1 - sourceRange + destinationRange;
                var item2 = item1 + aux;
                offset ++;
                newChunks.Insert(0,(item1, item2));
                newChunks.Add((chunk.Item1 + aux + 1, chunk.Item2));
            }
            else if (!chunk1Changed && chunk2Changed)
            {
                var aux = chunk.Item2 - sourceRange + destinationRange;
                offset ++;
                newChunks.Insert(0, ( aux - (chunk.Item2 - sourceRange) , aux));
                newChunks.Add((chunk.Item1, chunk.Item1 + (sourceRange - chunk.Item1 - 1)));
            }
            else
            {
                if (chunk.Item1 < sourceRange && chunk.Item2 > sourceRange && chunk.Item2 > sourceRange + rangeLength - 1)
                {
                    var aux = destinationRange + (sourceRange + rangeLength - 1 - sourceRange);
                    offset ++;
                    newChunks.Insert(0, (destinationRange, aux));
                    newChunks.Add((chunk.Item1, sourceRange - 1));
                    newChunks.Add((sourceRange + rangeLength, chunk.Item2));
                }
                else
                    newChunks.Add(chunk);
            }
        }
        chunks.Clear();
        chunks.AddRange(newChunks);
    }
    return chunks;
}