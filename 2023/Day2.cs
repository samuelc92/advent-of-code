var input = File.ReadAllLines("input.txt").ToList();

// PART 1
var result = 0;
foreach (var line in input)
{
    var game = line.Split(":")[0];
    var gameNum = int.Parse(game.Split(" ")[1]);
    var sets = line.Split(":")[1];
    var cubes = sets.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var possibleGame = true;

    for (int i = 0; i < cubes.Length; i += 2)
    {
        var n = int.Parse(cubes[i].ToString());
        if ((n > 12 && cubes[i+1].StartsWith("red")) ||
            (n > 13 && cubes[i+1].StartsWith("green")) ||
            (n > 14 && cubes[i+1].StartsWith("blue")))
        {
            possibleGame = false;
            break;
        }
    }

    result += possibleGame ? gameNum : 0;
}
Console.WriteLine($"Result part 1: {result}");

// PART 2
var result2 = 0;
foreach (var line in input)
{
    var sets = line.Split(":")[1];
    var cubes = sets.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    (var red, var blue, var green) = (0, 0, 0);

    for (int i = 0; i < cubes.Length; i += 2)
    {
        var n = int.Parse(cubes[i].ToString());
        if (cubes[i+1].StartsWith("red") && n > red)
            red = n;
        if (cubes[i+1].StartsWith("green") && n > green)
            green = n;
        if (cubes[i+1].StartsWith("blue") && n > blue)
            blue = n;
    }
    result2 += red * green * blue;
    (red, green, blue) = (0, 0, 0);
}

Console.WriteLine($"Result part 2: {result2}");
