﻿module Website.Utils

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Web
open Newtonsoft.Json
open System.IO

[<ReflectedDefinition>]
let truncate xs count = Seq.truncate count xs

[<ReflectedDefinition>]
let skip xs count = Seq.skip count xs

[<ReflectedDefinition>]
let split<'T> count xs =        
    let rec loop (xs:seq<'T>) =
        [
            yield truncate xs count
            match Seq.length xs <= count with
            | false -> yield! loop <| skip xs count
            | true -> ()
        ]
    loop xs

let link href txt = A [HRef href] -< [Text txt]

let dataDiv<'T> jsonPath (dataElt:'T -> Element<Control>)  =
    let data =
        let json = File.ReadAllText jsonPath
        JsonConvert.DeserializeObject(json, typeof<'T []>)
        :?> 'T []
    data
    |> Array.mapi (fun idx x ->
        let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
        Div [Class cls] -< [dataElt x]
    )
    |> split 2
    |> Seq.map (fun x ->
        Div [Class "row data-row"]
        -< x)
    |> fun x -> Div [] -< x