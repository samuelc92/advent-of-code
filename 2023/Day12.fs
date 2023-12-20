open System
open System.IO

let input = Seq.cast<string> (File.ReadLines("./input.txt"))

// PART 1

let arrangements (i: string) =
    let rec loop cl s num =
        match cl with
        | head :: tail when head = '.' ->
            if s = 0 then
                loop tail 0 num
            elif (not (List.isEmpty num)) && s = List.head num then
                loop tail 0 (List.tail num)
            else 0
        | head :: tail when head = '#' -> loop tail (s+1) num 
        | head :: tail when head = '?' -> (loop (List.append ['#'] tail) s num) + (loop (List.append ['.'] tail) s num) 
        | _ -> if (List.isEmpty num) && s = 0 then 1 
                elif List.length num > 1 then 0
                elif not (List.isEmpty num) && (s = List.head num) then 1
                else 0
    let a = i.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    loop (a[0] |> Seq.toList) 0 (a[1].Split([|','|], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int |> Seq.toList)

let resul1 =
    input
    |> Seq.fold (fun acc x ->
        let r = arrangements x 
        acc + r) 0

printfn "Result part 1: %i" resul1
Console.ReadKey() |> ignore
