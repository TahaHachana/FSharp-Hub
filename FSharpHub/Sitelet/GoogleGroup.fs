module Sitelet.GoogleGroup

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

[<JavaScript>]
let itemLi item =
    LI [Attr.Class "list-group-item"]
    -< [
        H4 [Attr.Class "list-group-item-heading"]
        -< [
            A [
                Attr.HRef item?link
                Attr.Target "_blank"
                Text item?title
            ]
        ]
        P [Text item?contentSnippet]
        P [Text ("Author: " + item?author)]
    ]

[<JavaScript>]
let main() =
    Div [HTML5.Attr.Data "status" "loading"]
    |>! OnAfterRender (fun elt ->
        let url = "http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&callback=?&q=https%3A%2F%2Fgroups.google.com%2Fforum%2Ffeed%2Ffsharp-opensource%2Ftopics%2Frss.xml%3Fnum%3D15"
        JsUtils.displayFeed url itemLi elt)

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = main() :> _