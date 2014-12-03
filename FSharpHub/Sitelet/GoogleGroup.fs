module Website.GoogleGroup

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
        let url = "http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&callback=?&q=https%3A%2F%2Fgroups.google.com%2Fforum%2Ffeed%2Ffsharp-opensource%2Ftopics%2Frss.xml%3Fnum%3D15"
        JQuery.GetJSON(url, (fun (data, _) ->
            let data = As<obj []> data?responseData?feed?entries
            for x in data do
                let li =
                    LI [Attr.Class "list-group-item"] -< [
                        H4 [Attr.Class "list-group-item-heading"] -< [
                            A [Attr.HRef x?link; Attr.Target "_blank"; Text x?title]
                        ]
                        P [Text x?contentSnippet ]
                        P [Text ("Author: " + x?author)]
                    ]
                ul.Append li)) |> ignore
        elt.Append ul
        elt.RemoveAttribute "data-status"
        hideProress())

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = main() :> _