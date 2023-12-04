open System
open System.IO

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

let input = Seq.cast<string> (File.ReadLines("./input.txt"))
let result1 =
    input
    |> Seq.map getPoints
    |> Seq.sum

printfn "Result part 1: %i" result1
Console.ReadKey() |> ignore