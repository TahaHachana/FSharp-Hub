module Sitelet.News

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

[<JavaScript>]
let main() =
    Div [HTML5.Attr.Data "status" "loading"]
    |>! OnAfterRender (fun elt ->
        let url = "http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=http%3A%2F%2Ffpish.net%2Frss%2Fblogs%2Ftag%2F1%2Ff~23"
        JsUtils.displayFeed url JsUtils.itemLi elt)

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = main() :> _