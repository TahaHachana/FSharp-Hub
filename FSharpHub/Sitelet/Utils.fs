module Website.Utils

open IntelliFactory.Html
open IntelliFactory.WebSharper

[<JavaScript>]
let truncate xs count = Seq.truncate count xs

[<JavaScript>]
let skip xs count = Seq.skip count xs

[<JavaScript>]
let split<'T> count xs =        
    let rec loop (xs:seq<'T>) =
        [
            yield truncate xs count
            match Seq.length xs <= count with
            | false -> yield! loop <| skip xs count
            | true -> ()
        ]
    loop xs

//[<JavaScript>]
//let splitElts count (xs:seq<IntelliFactory.WebSharper.Html.Element>) =        
//    let rec loop xs =
//        [
//            yield truncate xs count
//            match Seq.length xs <= count with
//            | false -> yield! loop <| skip xs count
//            | true -> ()
//        ]
//    loop xs

let link href txt = A [HRef href] -< [Text txt]