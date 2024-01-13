open System
open System.IO
open System.Collections.Generic

let input = Seq.cast<string> (File.ReadLines("./input.txt"))

// PART 1
let arrangements r d =
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
    loop r 0 d

// PART 2
let arrangementsWithCache r d =
    let cache = new Dictionary<(int * int), int64>()
    let rec loop (springs: string) damages (cache: Dictionary<(int * int), int64>) i =
        if List.length damages = 0 then
            if i < springs.Length && springs.Substring(i).Contains("#") then int64 0
            else int64 1
        elif i > springs.Length - 1 then int64 0
        elif springs[i] = '.' then loop springs damages cache (i+1)
        elif cache.ContainsKey(i, List.length damages) then cache.GetValueOrDefault((i, List.length damages))
        else
            let mutable result = int64 0
            let damage = List.head damages
            let range = i + damage
            result <- result + if range <= springs.Length && (not (springs.Substring(i, damage).Contains('.'))) && (range = springs.Length || (not (springs[range] = '#'))) then
                                 loop springs (List.tail damages) cache (i + damage + 1)
                               else int64 0
            result <- result + if springs[i] = '?' then loop springs damages cache (i+1) else int64 0
            cache.Add((i, List.length damages), result)
            result

    loop r d cache 0

let resul2 =
    input
    |> Seq.fold (fun acc x ->
        let a = x.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
        let records = Seq.fold (fun acc _ -> if acc = "" then a[0] else acc + "?" + a[0]) "" [1..5]
        let d = a[1].Split([|','|], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int |> Seq.toList
        let damages = List.fold (fun acc _ -> List.append acc d) List.empty [1..5]
        let r = arrangementsWithCache records damages 
        acc + r) (int64 0)

printfn "Result part 2: %i" resul2
Console.ReadKey() |> ignore
