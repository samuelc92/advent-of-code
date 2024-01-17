var input = File.ReadAllLines("input.txt").ToList();

// PART 1
var maxLine = input.Count;
var firstLine = input.First();
var result = 0;
for (var col = 0; col < firstLine.Length; col++)
{
    var positionsOccuppied = new List<int>();
    for (var line = 0; line < input.Count; line++)
    {
        if (input[line][col] == 'O')
        {
            var lastPos = positionsOccuppied.LastOrDefault();
            if (positionsOccuppied.Count == 0)
            {
                result += maxLine;
                positionsOccuppied.Add(maxLine);
            }
            else
            {
                result += lastPos - 1;
                positionsOccuppied.Add(lastPos - 1);
            }
        }
        else if (input[line][col] == '#')
            positionsOccuppied.Add(maxLine - line);
    }
}

Console.WriteLine($"Result part 1: {result}");