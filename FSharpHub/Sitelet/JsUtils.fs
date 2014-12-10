module Sitelet.JsUtils

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

[<JavaScript>]
let hideProress() =
    match JQuery.Of("[data-status=\"loading\"]").Length with
    | 0 ->
        JQuery.Of("#progress-bar")
            .SlideUp()
            .Ignore
    | _ ->
        JQuery.Of("[data-spy=\"scroll\"]").Each(
            fun x -> JQuery.Of(x)?scrollspy("refresh")
        ).Ignore

[<JavaScript>]
let displayFeed url (itemLi:'T -> Element) (elt:Element) =
    let ul = UL [Attr.Class "list-group"]
    JQuery.GetJSON(url, (fun (data, _) ->
        let data = As<obj []> data
        let entries = data?responseData?feed?entries
        entries
        |> Array.iter (fun x ->
            let li = itemLi x
            ul.Append li))) |> ignore
    elt.Append ul
    elt.RemoveAttribute "data-status"
    hideProress()

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
        P [Text item?contentSnippet ]
    ]