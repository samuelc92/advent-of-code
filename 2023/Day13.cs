var input = File.ReadAllLines("input.txt").ToList();

// PART 1

var i = 0;
var result = 0;
while (i < input.Count)
{
    var aux = input.Skip(i).TakeWhile(p => !string.IsNullOrEmpty(p)).ToList();
    var column = GetColumnMirror(aux);

    if (column != -1)
        result += column;
    else
    {
        var line = GetLineMirror(aux);
        if (line != -1) result += 100 * line;
    }

    i += aux.Count + 1;
}
Console.WriteLine($"Result part 1: {result}");
Console.WriteLine("Press any button to close...");

int GetColumnMirror(List<string> list)
{
    var element = list.FirstOrDefault();
    var result = -1;
    var isEqual = true;
    for (int col = 1; col < element?.Length; col++)
    {
        isEqual = true;
        foreach (var line in list)
        {
            if (line[col - 1] != line[col])
            {
                isEqual = false;
                break;
            }
        }
        if (isEqual)
        {
            var below = col - 2;
            var above = col + 1;
            var equal = true;
            while (below >= 0 && above < element.Length)
            {
                foreach (var line in list)
                {
                    if (line[below] != line[above])
                    {
                        equal = false;
                        break;
                    }
                }
                below--;
                above++;
            }
            if (equal)
            {
                result = col;
                break;
            }
        }
    }
    return result;
}

int GetLineMirror(List<string> list)
{
    var result = -1;
    for (int line = 1; line < list?.Count; line++)
    {
        if (list[line - 1] == list[line])
        {
            var below = line - 2;
            var above = line + 1;
            var equal = true;
            while (below >= 0 && above < list.Count)
            {
                for (int line2 = 1; line2 < list?.Count; line2++)
                {
                    if (list[below] != list[above])
                    {
                        equal = !equal;
                        break;
                    }
                }
                if (!equal) break;
                below--;
                above++;
            }
            if (equal)
            {
                result = line;
                break;
            }
        }
    }

    return result;
}

