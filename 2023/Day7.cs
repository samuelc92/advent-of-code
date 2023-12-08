var input = File.ReadAllLines("input.txt").ToList();

IEnumerable<TypeOfHands> typesOfHands = [TypeOfHands.FIVE_KIND, TypeOfHands.FOUR_KIND, TypeOfHands.FULL_HOUSE, TypeOfHands.THREE_KIND, TypeOfHands.TWO_PAIR, TypeOfHands.ONE_PAIR, TypeOfHands.HIGH_CARD];
var CardsWeight = new Dictionary<string, int>
{
    { "T", 10 },
    { "J", 11 },
    { "Q", 12 },
    { "K", 13 },
    { "A", 14 },
};

var CardsWeight2 = new Dictionary<string, int>
{
    { "T", 10 },
    { "J", 1 },
    { "Q", 12 },
    { "K", 13 },
    { "A", 14 },
};

//PART 1
var hands = new Dictionary<TypeOfHands, List<string>>();
foreach(var line in input)
{
    var hand = line.Split(" ")[0];
    var typeOfHand = GetTypeOfHand(hand);
    AddHand(typeOfHand, line, hands, IsHandStronger);
}

var multi = input.Count;
var result = 0;
foreach (var type in typesOfHands)
{
    if (hands.TryGetValue(type, out var valueHigh))
    {
        var r = CalculateBid(valueHigh, multi);
        result += r.Item1;
        multi = r.Item2;
    }
}

Console.WriteLine($"Result part 1: {result}");

// PART 2
var hands2 = new Dictionary<TypeOfHands, List<string>>();
foreach(var line in input)
{
    var newLine = TransformJoke(line);
    var hand = newLine.Split(" ")[0];
    var typeOfHand = GetTypeOfHand(hand);
    AddHand(typeOfHand, line, hands2, IsHandStronger2);
}

var multi2 = input.Count;
var result2 = 0;
foreach (var type in typesOfHands)
{
    if (hands2.TryGetValue(type, out var valueHigh))
    {
        var r = CalculateBid(valueHigh, multi2);
        result2 += r.Item1;
        multi2 = r.Item2;
    }
}

Console.WriteLine($"Result part 2: {result2}");

string TransformJoke(string hand)
{
    var cards = hand.Split(" ")[0];
    var distinct = cards.Where(p => p != 'J').Distinct();
    if (!distinct.Any())
        return hand;
    var c = distinct.Aggregate(("", 0),
                                (acc, c) =>
                                {
                                    var auxQtd = cards.Count(p => p == c);
                                    if (string.IsNullOrEmpty(acc.Item1)) return (c.ToString(), auxQtd);

                                    var weight1 = char.IsNumber(c) ? int.Parse(c.ToString()) : CardsWeight2[c.ToString()]; 
                                    var weight2 = char.IsNumber(char.Parse(acc.Item1)) ? int.Parse(acc.Item1) : CardsWeight[acc.Item1]; 

                                    if (auxQtd > acc.Item2) return (c.ToString(), auxQtd);
                                    else if (auxQtd == acc.Item2 && weight1 > weight2) return (c.ToString(), auxQtd);
                                    else return acc;
                                });
    return cards.Replace("J", c.Item1.ToString()) + " " + hand.Split(" ")[1];
}

(int, int) CalculateBid(List<string> value, int mult)
{
    var response = 0;
    foreach(var item in value)
    {
        var bid = int.Parse(item.Split(" ")[1]);
        response += bid * mult; 
        mult --;
    }
    return (response, mult);
}

void AddHand(TypeOfHands typeOfHand, string hand, Dictionary<TypeOfHands, List<string>> hands, Func<string, string, bool> isHandStronger)
{
    if (hands.TryGetValue(typeOfHand, out var value))
    {
        var hasInserted = false;
        var cards = hand.Split(" ")[0];
        for (var i = 0; i < value.Count; i++)
        {
            var hand2 = value[i].Split(" ")[0];
            if (!isHandStronger(cards, hand2))
                continue;
            value.Insert(i, hand);
            hasInserted = true;
            break;
        }
        if (!hasInserted) value.Add(hand);
    }
    else
        hands.Add(typeOfHand, [hand]);
}

bool IsHandStronger(string hand1, string hand2)
{
    for (int i = 0; i < hand1.Length; i++)
    {
        var weight1 = char.IsNumber(hand1[i]) ? int.Parse(hand1[i].ToString()) : CardsWeight[hand1[i].ToString()]; 
        var weight2 = char.IsNumber(hand2[i]) ? int.Parse(hand2[i].ToString()) : CardsWeight[hand2[i].ToString()]; 
        if (weight1 == weight2) continue;
        return weight1 > weight2; 
    }
    return false;
}

bool IsHandStronger2(string hand1, string hand2)
{
    for (int i = 0; i < hand1.Length; i++)
    {
        var weight1 = char.IsNumber(hand1[i]) ? int.Parse(hand1[i].ToString()) : CardsWeight2[hand1[i].ToString()]; 
        var weight2 = char.IsNumber(hand2[i]) ? int.Parse(hand2[i].ToString()) : CardsWeight2[hand2[i].ToString()]; 
        if (weight1 == weight2) continue;
        return weight1 > weight2; 
    }
    return false;
}

TypeOfHands GetTypeOfHand(string cards)
{
    var response = TypeOfHands.HIGH_CARD;
    var distinctCards = cards.Distinct(); 
    if (distinctCards.Count() == 1)
        response = TypeOfHands.FIVE_KIND;
    else if (distinctCards.Any(p => cards.Where(x => x == p).Count() == 4))
        response = TypeOfHands.FOUR_KIND;
    else if (distinctCards.Count() == 2)
        response = TypeOfHands.FULL_HOUSE;
    else if (distinctCards.Count() == 3 && distinctCards.Any(p => cards.Where(x => x == p).Count() == 3))
        response = TypeOfHands.THREE_KIND;
    else if (distinctCards.Count() == 3)
        response = TypeOfHands.TWO_PAIR;
    else if (distinctCards.Count() == 4)
        response = TypeOfHands.ONE_PAIR;

    return response;
}

enum TypeOfHands
{
    FIVE_KIND,
    FOUR_KIND,
    FULL_HOUSE,
    THREE_KIND,
    TWO_PAIR,
    ONE_PAIR,
    HIGH_CARD
}