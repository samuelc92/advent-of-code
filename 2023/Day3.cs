var input = File.ReadAllLines("input.txt").ToList();

// PART 1
var result = 0;
for (int i = 0; i < input.Count; i++)
{
    var line = input[i];
    var num = string.Empty;
    var isAdjacent = false;
    for (int j = 0; j < line.Length; j++)
    {
        var c = line[j];

        if (char.IsNumber(c))
        {
            num += c.ToString();

            // above line
            if (i > 0 && ((!char.IsNumber(input[i - 1][j]) && input[i - 1][j] != '.') ||
               (j > 0 && !char.IsNumber(input[i - 1][j - 1]) && input[i - 1][j - 1] != '.') || // above left
               (j + 1 < line.Length && !char.IsNumber(input[i - 1][j + 1]) && input[i - 1][j + 1] != '.'))) // above right  
            {
                isAdjacent = true;
            }

            // same line
            if ((j > 0 && !char.IsNumber(line[j - 1]) && line[j - 1] != '.') || // left
               (j + 1 < line.Length && !char.IsNumber(line[j + 1]) && line[j + 1] != '.')) // right  
            {
                isAdjacent = true;
            }

            // below line 
            if (i + 1 < input.Count && ((!char.IsNumber(input[i + 1][j]) && input[i + 1][j] != '.') ||
               (j > 0 && !char.IsNumber(input[i + 1][j - 1]) && input[i + 1][j - 1] != '.') || // above left
               (j + 1 < line.Length && !char.IsNumber(input[i + 1][j + 1]) && input[i + 1][j + 1] != '.'))) // above right  
            {
                isAdjacent = true;
            }
        }

        if (!char.IsNumber(c) || j == line.Length - 1)
        {
            if (isAdjacent && !string.IsNullOrEmpty(num))
                result += int.Parse(num);
            isAdjacent = false;
            num = string.Empty;
            continue;
        }
    }
}

Console.WriteLine($"Result Part 1: {result}");

// PART 2
var result2 = 0;
var dic = new Dictionary<(int, int), List<int>>();
for (int i = 0; i < input.Count; i++)
{
    var line = input[i];
    var num = string.Empty;
    var isAdjacent = false;
    (int, int) coord = (-1, -1);
    for (int j = 0; j < line.Length; j++)
    {
        var c = line[j];

        if (char.IsNumber(c))
        {
            num += c.ToString();

            // above line
            if (i > 0)
            {
                if (input[i - 1][j] == '*')
                {
                    isAdjacent = true;
                    coord = (i - 1, j);
                }
                else if (j > 0 && input[i - 1][j - 1] == '*')
                {
                    isAdjacent = true;
                    coord = (i - 1, j - 1);
                }
                else if (j + 1 < line.Length && input[i - 1][j + 1] == '*')
                {
                    isAdjacent = true;
                    coord = (i - 1, j + 1);
                }
            }

            if (j > 0 && line[j - 1] == '*')
            {
                isAdjacent = true;
                coord = (i, j - 1);
            }
            else if (j + 1 < line.Length && line[j + 1] == '*')
            {
                isAdjacent = true;
                coord = (i, j + 1);
            }

            if (i + 1 < input.Count)
            {
                if (input[i + 1][j] == '*')
                {
                    isAdjacent = true;
                    coord = (i + 1, j);
                }
                else if (j > 0 && input[i + 1][j - 1] == '*')
                {
                    isAdjacent = true;
                    coord = (i + 1, j - 1);
                }
                else if (j + 1 < line.Length && input[i + 1][j + 1] == '*')
                {
                    isAdjacent = true;
                    coord = (i + 1, j + 1);
                }
            }
        }

        if (!char.IsNumber(c) || j == line.Length - 1)
        {
            if (isAdjacent && !string.IsNullOrEmpty(num))
            {
                if (dic.TryGetValue(coord, out var value))
                    value.Add(int.Parse(num));
                else
                    dic[coord] = [int.Parse(num)];
            }
            isAdjacent = false;
            num = string.Empty;
            coord = (-1, -1);
            continue;
        }

    }
}

foreach(var data in dic.Where(d => d.Value.Count == 2))
    result2 += data.Value[0] * data.Value[1];

Console.WriteLine($"Result Part 2: {result2}");
