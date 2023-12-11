open System
open System.IO

let input = Seq.cast<string> (File.ReadLines("./input.txt"))

// PART 1
let getExtrapolatedValue (history: int seq list) =
    history
    |> Seq.fold (fun acc x -> acc + Seq.last x) 0 

// PART 2
let getExtrapolatedValue2 (history: int seq list) =
    history
    |> Seq.fold (fun acc x -> (Seq.head x) - acc ) 0 

let getHistory step func =
    let rec loop acc step =
        if Seq.forall (fun n -> n = 0) step then acc 
        else
            let s =
                step
                |> Seq.windowed 2
                |> Seq.map (fun x -> x[1] - x[0])
            loop (s :: acc) s
    loop (step :: []) step
    |> func

let result =
    input
    |> Seq.map (fun x -> x.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int)
    |> Seq.fold (fun acc x -> acc + (getHistory x getExtrapolatedValue)) 0

printfn "Result part 1: %i" result 

let result2 =
    input
    |> Seq.map (fun x -> x.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int)
    |> Seq.fold (fun acc x -> acc + (getHistory x getExtrapolatedValue2)) 0

printfn "Result part 2: %i" result2
Console.ReadKey() |> ignore