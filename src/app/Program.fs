// Learn more about F# at http://fsharp.org

open System
open System.IO
open FSharp.Data

let home = System.Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")
let data = Path.Combine(home, "bikahve", "data", "people.csv")

type People = CsvProvider<"c:/repo/bikahve/data/people.csv">

let employee = People.Load(data)
let rows = Array.ofSeq employee.Rows

let rand = new System.Random()

let swap (a: _[]) x y =
    let tmp = a.[x]
    a.[x] <- a.[y]
    a.[y] <- tmp

// shuffle an array (in-place)
let shuffle a =
    Array.iteri (fun i _ -> swap a i (rand.Next(i, Array.length a))) a

[<EntryPoint>]
let main argv =
    shuffle rows
    let pairs = List.chunkBySize 2 (List.ofSeq rows)
    printf "%A" pairs
    0 // return an integer exit code
