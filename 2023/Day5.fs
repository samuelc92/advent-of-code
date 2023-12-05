open System
open System.IO

// PART 1
let input = Seq.cast<string> (File.ReadLines("./input.txt"))

let sources =
    input
    |> Seq.head
    |> (fun x -> x.Split([| ':' |])[1])
    |> (fun x -> x.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries))
    |> Seq.map int64
    |> Seq.chunkBySize 2
    |> Seq.fold (fun acc x ->
        let a = x[0]
        let b = int x[1]
        let f = Seq.init b (fun v -> int64 v + a)
        Seq.append acc f) []
    |> Seq.toList

let getNumber (lines: string list) (source: int64) =
    lines
    |> Seq.skip 1
    |> Seq.fold (fun acc line ->
        let destinationRage = int64 (line.Split([|' '|])[0])
        let sourceRange = int64 (line.Split([|' '|])[1])
        let rangeLength = int64 (line.Split([|' '|])[2])
        if source >= sourceRange && source <= (sourceRange + rangeLength - 1L) then
            if source = sourceRange then destinationRage else (source - sourceRange + destinationRage)
        else acc
        ) source

let getLocationNumber input source =
    let rec loop acc count i =
        let map =
            i
            |> Seq.skip count
            |> Seq.takeWhile (fun x -> not (String.IsNullOrWhiteSpace(x))) 
            |> Seq.toList
        let s =
            acc 
            |> Seq.map (fun x -> getNumber map x)
        let c = count + (Seq.length map) + 1
        if c > (Seq.length i) then s else loop s c i
    loop source 2 input

let locations = getLocationNumber input sources 
printfn "Result part 1: %i" (Seq.min locations) 
Console.ReadKey() |> ignore