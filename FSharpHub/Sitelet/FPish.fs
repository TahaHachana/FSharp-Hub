module Website.FPish

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

//type Response =
//    {
//        responseData : data
////        responseDetails : obj
////        responseStatus : obj
//    }
//
//and data =
//    {
//        feed : feedDetails
//    }
//
//and feedDetails =
//    {
////        author : string
////        description : string
//        entries : Entry []
////        feedUrl : string
////        link : string
////        title : string
////        ``type`` : string
//    }
//
//and Entry =
//    {
//        title : string
//        link : string
////        author : string
////        publishedDate : obj
//        contentSnippet : string
////        content : string
////        categories : string []
//    }

[<JavaScript>]
let hideProress() =
    match JQuery.Of("[data-status=\"loading\"]").Length with
    | 0 ->
        JQuery.Of("#progress-bar").SlideUp().Ignore
//        JQuery.Of("[data-spy=\"scroll\"]").Each(
//            fun x -> JQuery.Of(x)?scrollspy("refresh")
//        ).Ignore
    | _ ->
        JQuery.Of("[data-spy=\"scroll\"]").Each(
            fun x -> JQuery.Of(x)?scrollspy("refresh")
        ).Ignore

[<JavaScript>]
let main() =
    Div [HTML5.Attr.Data "status" "loading"]
    |>! OnAfterRender (fun elt ->
        let ul = UL [Attr.Class "list-group"]
        let url = "http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=http%3A%2F%2Ffpish.net%2Frss%2Ftopics%2Fall"
        JQuery.GetJSON(url, (fun (data, _) ->
            let data = As<obj []> data?responseData?feed?entries
            for x in data do
                let li =
                    LI [Attr.Class "list-group-item"] -< [
                        H4 [Attr.Class "list-group-item-heading"] -< [
                            A [Attr.HRef x?link; Attr.Target "_blank"; Text x?title]
                        ]
                        Div [
                            for y in x?categories -> Span [Attr.Class "label label-info"] -< [Text y]
                        ]
                    ]
                ul.Append li)) |> ignore
        elt.Append ul
        JavaScript.Log "Appended fpish list"
        elt.RemoveAttribute "data-status"
        hideProress())

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = main() :> _