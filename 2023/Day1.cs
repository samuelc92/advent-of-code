var input = File.ReadAllLines("input.txt").ToList();

// PART 1
var result = input
    .Select(s =>
        s.Aggregate("",
                    (acc, c) =>
                        acc += char.IsDigit(c) ? c.ToString() : ""))
    .Select(s => int.Parse(s.First().ToString() + s.Last().ToString()))
    .Sum();
Console.WriteLine($"Result: {result}");

// PART 2
var result2 = 0;
foreach (var item in input)
{
    result2 += GetNumbers(item);
}
Console.WriteLine($"Result: {result2}");

int GetNumbers(string item)
{
    item = item.Replace("one", "o1e");
    item = item.Replace("two", "t2o");
    item = item.Replace("three", "t3e");
    item = item.Replace("four", "f4r");
    item = item.Replace("five", "f5e");
    item = item.Replace("six", "s6x");
    item = item.Replace("seven", "s7n");
    item = item.Replace("eight", "e8t");
    item = item.Replace("nine", "n9e");
    var digits = item.Where(char.IsDigit).ToList();
    return int.Parse(digits.First().ToString() + digits.Last().ToString());
}