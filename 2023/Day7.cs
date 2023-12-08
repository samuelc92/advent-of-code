var input = File.ReadAllLines("input.txt").ToList();

//PART 1
var hands = new Dictionary<TypeOfHands, List<string>>();
foreach(var line in input)
{
    var hand = line.Split(" ")[0];
    var typeOfHand = GetTypeOfHand(hand);
    AddHand(typeOfHand, line, hands);
}

var multi = input.Count;
var result = 0;

if (hands.TryGetValue(TypeOfHands.FIVE_KIND, out var valueFive))
{
    var r = CalculateBid(valueFive, multi);
    result += r.Item1;
    multi = r.Item2;
}

if (hands.TryGetValue(TypeOfHands.FOUR_KIND, out var valueFour))
{
    var r = CalculateBid(valueFour, multi);
    result += r.Item1;
    multi = r.Item2;
}

if (hands.TryGetValue(TypeOfHands.FULL_HOUSE, out var valueFull))
{
    var r = CalculateBid(valueFull, multi);
    result += r.Item1;
    multi = r.Item2;
}

if (hands.TryGetValue(TypeOfHands.THREE_KIND, out var valueThree))
{
    var r = CalculateBid(valueThree, multi);
    result += r.Item1;
    multi = r.Item2;
}

if (hands.TryGetValue(TypeOfHands.TWO_PAIR, out var valueTwo))
{
    var r = CalculateBid(valueTwo, multi);
    result += r.Item1;
    multi = r.Item2;
}

if (hands.TryGetValue(TypeOfHands.ONE_PAIR, out var valueOne))
{
    var r = CalculateBid(valueOne, multi);
    result += r.Item1;
    multi = r.Item2;
}

if (hands.TryGetValue(TypeOfHands.HIGH_CARD, out var valueHigh))
{
    var r = CalculateBid(valueHigh, multi);
    result += r.Item1;
    multi = r.Item2;
}

Console.WriteLine($"Result part 1: {result}");

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

void AddHand(TypeOfHands typeOfHand, string hand, Dictionary<TypeOfHands, List<string>> hands)
{
    if (hands.TryGetValue(typeOfHand, out var value))
    {
        var hasInserted = false;
        var cards = hand.Split(" ")[0];
        for (var i = 0; i < value.Count; i++)
        {
            var hand2 = value[i].Split(" ")[0];
            if (!IsHandStronger(cards, hand2))
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
    var cardsWeight = new Dictionary<string, int>
    {
        { "T", 10 },
        { "J", 11 },
        { "Q", 12 },
        { "K", 13 },
        { "A", 14 },
    };
    for (int i = 0; i < hand1.Length; i++)
    {
        var weight1 = char.IsNumber(hand1[i]) ? int.Parse(hand1[i].ToString()) : cardsWeight[hand1[i].ToString()]; 
        var weight2 = char.IsNumber(hand2[i]) ? int.Parse(hand2[i].ToString()) : cardsWeight[hand2[i].ToString()]; 
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