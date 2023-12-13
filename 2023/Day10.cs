var input = File.ReadAllLines("input.txt").ToList();

// PART 1
var sCoord = (-1, -1);
for (int i = 0; i < input.Count; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        if (input[i][j] == 'S')
        {
            sCoord = (i, j);
            break;
        }
    }
    if (sCoord.Item1 != -1 && sCoord.Item2 != -1) break;
}

var pipeMap = new Dictionary<(char, char), char>
{
    { ('|', 'S'), 'N'},
    { ('|', 'N'), 'S'},
    { ('-', 'W'), 'E'},
    { ('-', 'E'), 'W'},
    { ('L', 'N'), 'E'},
    { ('L', 'E'), 'N'},
    { ('J', 'N'), 'W'},
    { ('J', 'W'), 'N'},
    { ('7', 'S'), 'W'},
    { ('7', 'W'), 'S'},
    { ('F', 'S'), 'E'},
    { ('F', 'E'), 'S'},
};

var queue = new Queue<Pipe>();
var visited = new List<(int, int)>();
queue.Enqueue(new Pipe(sCoord.Item1, sCoord.Item2, ' '));

while (queue.Count > 0)
{
    var pipe = queue.Dequeue();
    (var line, var col) = (pipe.Line, pipe.Col);
    if (visited.Any(p => p.Item1 == line && p.Item2 == col)) continue;
    visited.Add((line, col));

    if (input[line][col] == 'S')
    {
        // NORTH
        if (line > 0)
            if ("|7F".Contains(input[line - 1][col])) queue.Enqueue(new Pipe(line - 1, col, 'S'));
        // SOUTH
        if (line + 1 < input.Count)
            if ("|LJ".Contains(input[line + 1][col])) queue.Enqueue(new Pipe(line + 1, col, 'N'));
        // WEST  
        if (col > 0)
            if ("-LF".Contains(input[line][col - 1])) queue.Enqueue(new Pipe(line, col - 1, 'E'));
        // EAST 
        if (col + 1 < input[line].Length)
            if ("-J7F".Contains(input[line][col + 1])) queue.Enqueue(new Pipe(line, col + 1, 'W'));
    }
    else
    {
        var c = input[line][col];
        var to = pipeMap.GetValueOrDefault((c, pipe.From));
        switch (to)
        {
            case 'N':
                queue.Enqueue(new Pipe(line - 1, col, 'S'));
                break;
            case 'S':
                queue.Enqueue(new Pipe(line + 1, col, 'N'));
                break;
            case 'W':
                queue.Enqueue(new Pipe(line, col - 1, 'E'));
                break;
            case 'E':
                queue.Enqueue(new Pipe(line, col + 1, 'W'));
                break;
        }
    }
}

Console.WriteLine("Result part 1: " + visited.Count / 2);
record Pipe(int Line, int Col, char From);
