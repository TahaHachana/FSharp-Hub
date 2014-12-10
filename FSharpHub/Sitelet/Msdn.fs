module Sitelet.Msdn

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

[<JavaScript>]
let main() =
    Div [HTML5.Attr.Data "status" "loading"]
    |>! OnAfterRender (fun elt ->
        let url = "http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=https%3A%2F%2Fsocial.msdn.microsoft.com%2FForums%2Fen-US%2Ffsharpgeneral%2Fthreads%3FoutputAs%3Drss"
        JsUtils.displayFeed url JsUtils.itemLi elt)

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = main() :> _