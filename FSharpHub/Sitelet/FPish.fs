module Sitelet.FPish

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
                Text item?title]
        ]
        Div [
            for y in item?categories ->
                Span [Attr.Class "label label-info"]
                -< [Text y]
        ]
    ]

[<JavaScript>]
let main() =
    Div [HTML5.Attr.Data "status" "loading"]
    |>! OnAfterRender (fun elt ->
        let url = "http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=http%3A%2F%2Ffpish.net%2Frss%2Ftopics%2Fall"
        JsUtils.displayFeed url itemLi elt)

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = main() :> _