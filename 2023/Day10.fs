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

(* USE QUEUE INSTEAD OF TREE 
    QUEUE FIRST TWO PATHS, THEN DEQUEUE EVERY PATH AND ENQUEUE THE NEW ONE, FOR EVERY PATH VISITED SAVE IN A ARRAY (LINE, COLUMN)
    WHERE THERE IS NO MORE ITEM TO BE DEQUEUED DIVIDED THE LENGTH OF VISITED ARRAY BY 2, THIS IS THE RESULT
*) 
let navegate input t sCood =
    let sLine = fst sCoord
    let sCol = snd sCoord
    let visited = [(sLine, sCol)]
    let rec loop t visited line col h (input: string seq) =
        if line > 0 then
            let c = (Seq.item (line - 1) input)[col]
            if "|7F".Contains(c.ToString()) then // NORTH
                loop (insert t (h + 1)) (visited :: [(line-1, col)]) (line - 1) col (h + 1) input
        if (col + 1) < (Seq.item line input).Length then
            let c = (Seq.item line input)[col + 1]
            if "-J7".Contains(c.ToString()) then // EAST
                loop (insert t (h + 1)) (visited :: [(line, col + 1)]) line (col + 1) (h + 1) input
        if (col - 1) > 0 then
            let c = (Seq.item line input)[col - 1]
            if "-FL".Contains(c.ToString()) then // WEST 
                loop (insert t (h + 1)) (visited :: [(line, col - 1)]) line (col - 1) (h + 1) input

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

(*
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
*)