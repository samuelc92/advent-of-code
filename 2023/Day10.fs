open System
open System.IO
let input = Seq.cast<string> (File.ReadLines("./input.txt"))

// PART 1

type Tree =
    | Node of Tree * int * Tree
    | Leaf

let rec insert tree element =
    match element, tree with
    | x, Leaf -> Node(Leaf,x,Leaf)
    | x,Node(Leaf,y,r) -> Node((insert Leaf x), y, r)
    | x,Node(l,y,Leaf) -> Node(l, y, (insert Leaf x))
    | _ -> Leaf

let mutable sCoord = (0, 0)
input |> Seq.iteri (fun i x -> String.iteri (fun i2 x2 -> if x2 = 'S' then sCoord <- (i, i2) else ()) x)

let sLine = fst sCoord
let sColumn = snd sCoord

input
|> Seq.fold (fun acc x ->
    let element = Seq.item sLine input
    let pipe = element[sColumn]


    ) Node (Leaf, 0, Leaf)
    
printfn "Cood: %A" sCoord

Console.ReadKey() |> ignore
