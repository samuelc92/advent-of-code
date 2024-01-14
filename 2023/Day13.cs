var input = File.ReadAllLines("input.txt").ToList();

// PART 1

var i = 0;
var result = 0;
while (i < input.Count)
{
    var aux = input.Skip(i).TakeWhile(p => !string.IsNullOrEmpty(p)).ToList();
    var column = GetColumnMirror2(aux);

    if (column != -1)
        result += column;
    else
    {
        var line = GetLineMirror2(aux);
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
                if (list[below] != list[above])
                {
                    equal = !equal;
                    break;
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

int GetColumnMirror2(List<string> list)
{
    var element = list.FirstOrDefault();
    var result = -1;
    var isEqual = true;
    for (int col = 1; col < element?.Length; col++)
    {
        isEqual = true;
        var count = 0;
        foreach (var line in list)
        {
            if (line[col - 1] != line[col])
            {
                isEqual = false;
                count++;
                if (count > 1) break;
            }
        }
        if (count == 1 || isEqual)
        {
            var below = col - 2;
            var above = col + 1;
            var r = new List<int>();
            while (below >= 0 && above < element.Length)
            {
                var count2 = 0;
                foreach (var line in list)
                {
                    if (line[below] != line[above])
                    {
                        count2++;
                        if (count2 > 1) break;
                    }
                }
                r.Add(count2);
                below--;
                above++;
            }
            if ((isEqual && (r.Count(p => p == 1) == 1 && r.Count(p => p == 0) == r.Count - 1))
                || (count == 1 && r.All(p => p == 0)))
            {
                result = col;
                break;
            }
        }
    }
    return result;
}

int GetLineMirror2(List<string> list)
{
    var result = -1;
    for (int line = 1; line < list?.Count; line++)
    {
        var first = list[line - 1];
        var second = list[line];
        var count = 0;
        var isEqual = true;
        for (var i = 0; i < first.Length; i++)
        {
            if (!first[i].Equals(second[i]))
            {
                count++;
                isEqual = false;
                if (count > 1) break;
            }
        }

        if (count == 1 || isEqual)
        {
            var below = line - 2;
            var above = line + 1;
            var r = new List<int>();

            while (below >= 0 && above < list.Count)
            {
                var f = list[below];
                var s = list[above];
                var count2 = 0;
                for (var i = 0; i < f.Length; i++)
                {
                    if (!f[i].Equals(s[i]))
                    {
                        count2++;
                        if (count2 > 1) break;
                    }
                }
                r.Add(count2);
                below--;
                above++;
            }

            if ((isEqual && (r.Count(p => p == 1) == 1 && r.Count(p => p == 0) == r.Count - 1))
                || (count == 1 && r.All(p => p == 0)))
            {
                result = line;
                break;
            }
        }
    }

    return result;
}
