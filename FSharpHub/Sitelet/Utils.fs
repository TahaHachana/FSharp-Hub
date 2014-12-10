module Sitelet.Utils

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Web
open Newtonsoft.Json
open System.IO
open System.Web

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

let link href txt =
    A [HRef href]
    -< [Text txt]

let dataDiv<'T> jsonPath (dataElt:'T -> Element<Control>)  =
    let jsonPath = HttpContext.Current.Server.MapPath jsonPath
    let data =
        let json = File.ReadAllText jsonPath
        JsonConvert.DeserializeObject(json, typeof<'T []>)
        :?> 'T []
    data
    |> Array.mapi (fun idx x ->
        let cls =
            match idx % 2 = 0 with
            | false -> "col-md-5 col-md-offset-1"
            | true -> "col-md-5"
        Div [Class cls]
        -< [dataElt x]
    )
    |> split 2
    |> Seq.map (fun x ->
        Div [Class "row data-row"]
        -< x)
    |> fun x ->
        Div []
        -< x