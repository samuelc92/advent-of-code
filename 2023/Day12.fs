open System
open System.IO
open System.Collections.Generic

let input = Seq.cast<string> (File.ReadLines("./input.txt"))

// PART 1
let arrangements (i: string) r d =
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

(*
let resul1 =
    input
    |> Seq.fold (fun acc x ->
        let a = x.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
        let r = arrangements x (a[0] |> Seq.toList) (a[1].Split([|','|], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int |> Seq.toList)
        acc + r) 0

printfn "Result part 1: %i" resul1
Console.ReadKey() |> ignore
*)

// PART 2
let arrangementsWithCache (i: string) r d =
    let cache = new Dictionary<string, int>()
    let rec loop cl s num (del: string) =
        if cache.ContainsKey del then
            cache.GetValueOrDefault(del)
        else
            match cl with
            | head :: tail when head = '.' ->
                if s = 0 then
                    loop tail 0 num (del + Char.ToString head) 
                elif (not (List.isEmpty num)) && s = List.head num then
                    loop tail 0 (List.tail num) (del + Char.ToString head)
                else
                    cache.Add(del, 0)
                    0
            | head :: tail when head = '#' -> loop tail (s+1) num (del + Char.ToString head) 
            | head :: tail when head = '?' -> (loop (List.append ['#'] tail) s num del) + (loop (List.append ['.'] tail) s num del) 
            | _ -> if (List.isEmpty num) && s = 0 then
                        cache.Add(del, 1)
                        1 
                    elif List.length num > 1 then
                        cache.Add(del, 0)
                        0
                    elif not (List.isEmpty num) && (s = List.head num) then
                        cache.Add(del, 1)
                        1
                    else
                        cache.Add(del, 0)
                        0
    loop r 0 d ""

let resul2 =
    input
    |> Seq.fold (fun acc x ->
        let a = x.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
        let records = Seq.fold (fun acc _ -> if acc = "" then a[0] else acc + "?" + a[0]) "" [1..5]
        let d = a[1].Split([|','|], StringSplitOptions.RemoveEmptyEntries) |> Seq.map int |> Seq.toList
        let damages = List.fold (fun acc _ -> List.append acc d) List.empty [1..5]
        let r = arrangementsWithCache x (records |> Seq.toList) damages 
        acc + r) 0

printfn "Result part 2: %i" resul2
Console.ReadKey() |> ignore
