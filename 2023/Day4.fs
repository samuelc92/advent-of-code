open System
open System.IO

// PART 1
let input = Seq.cast<string> (File.ReadLines("./input.txt"))
let getPoints (input: string) =
    let line = input.Split([| ':' |])[1]
    let numbers = line.Split([| '|' |])
    let winNumbersLine = numbers[0]
    let yourNumbersLine = numbers[1]
    let yourNumbers = yourNumbersLine.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int
    winNumbersLine.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.fold (fun acc v ->
        let n = int v
        if Seq.contains n yourNumbers then
            if acc = 0 then 1 else acc * 2
        else acc) 0

let result1 =
    input
    |> Seq.map getPoints
    |> Seq.sum

printfn "Result part 1: %i" result1

// PART 2
type ScratchCards = {MatchesNum:int seq; TotalMatches:int}

let buildCards  (input: string) =
    let line = input.Split([| ':' |])
    let cardNum = int (line[0].Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries)[1])
    let numbers = line[1].Split([| '|' |])
    let yourNumbers = numbers[1].Split([|' '|], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int
    numbers[0].Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.fold (fun (acc: Map<int, int seq>) v ->
        let n = int v
        if Seq.contains n yourNumbers then
            let value = acc.[cardNum]
            if Seq.isEmpty value then
                acc.Add(cardNum, Seq.append [] [cardNum + 1])
            else
                acc.Add(cardNum, Seq.append value [((Seq.last value) + 1)] )
        else acc) (Map [(cardNum, Seq.empty)])

let buildScratchCards (m: Map<int, int seq>) =
    m
    |> Map.fold (fun ((acc: Map<int, ScratchCards>), (mAux: Map<int, int seq>)) key value ->
        if acc.ContainsKey key then
            let v = acc.[key]
            let scratch = { v with MatchesNum=Seq.append v.MatchesNum value; TotalMatches=v.TotalMatches + 1 }
            let (aux: Map<int, ScratchCards>) = acc.Add(key, scratch)
            scratch.MatchesNum
            |> Seq.fold (fun ((acc2: Map<int, ScratchCards>), (m2: Map<int, int seq>)) x ->
                if acc2.ContainsKey x then
                    let s = acc2.[x]
                    ((acc2.Add(x, {s with TotalMatches=s.TotalMatches + 1; MatchesNum=Seq.append s.MatchesNum m2.[x]})), m2)
                else
                    ((acc2.Add(x, {TotalMatches=1; MatchesNum=m2.[x]})), m2)
            ) (aux, mAux)
        else
            let scratch = {MatchesNum=value;TotalMatches=1}
            let (aux: Map<int, ScratchCards>) = acc.Add(key, scratch)
            scratch.MatchesNum
            |> Seq.fold (fun ((acc2: Map<int, ScratchCards>), (m2: Map<int, int seq>)) x ->
                if acc2.ContainsKey x then
                    let s = acc2.[x]
                    ((acc2.Add(x, {s with TotalMatches=s.TotalMatches + 1; MatchesNum=Seq.append s.MatchesNum m2.[x]})), m2)
                else
                    ((acc2.Add(x, {TotalMatches=1; MatchesNum=m2.[x]})), m2)
            ) (aux, mAux)
        ) (Map.empty, m)
    |> (fun (x, y) -> x)

let result2 =
    input
    |> Seq.map buildCards
    |> Seq.fold (fun (acc: Map<int, int seq>) x ->
        Map.fold (fun ac key value -> Map.add key value ac) x acc) Map.empty
    |> buildScratchCards
    |> Map.fold (fun acc (k: int) (v: ScratchCards) -> acc + v.TotalMatches) 0

printfn "Result part 2: %i" result2
Console.ReadKey() |> ignore
