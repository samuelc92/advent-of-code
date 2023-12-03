var input = File.ReadAllLines("input-official.txt").ToList();

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
            {
                Console.WriteLine($"Number: {num}");
                result += int.Parse(num);
            }
            isAdjacent = false;
            num = string.Empty;
            continue;
        }

    }
}

Console.WriteLine($"Result Part 1: {result}");
