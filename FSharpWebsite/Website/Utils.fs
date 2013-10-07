module Website.Utils

open IntelliFactory.Html

let truncate xs count = Seq.truncate count xs

let skip xs count = Seq.skip count xs

let split count xs =        
    let rec loop xs =
        [
            yield truncate xs count
            match Seq.length xs <= count with
            | false -> yield! loop <| skip xs count
            | true -> ()
        ]
    loop xs

let link href txt = A [HRef href] -< [Text txt]