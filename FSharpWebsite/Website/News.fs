namespace Website

open IntelliFactory.WebSharper

module News =
    
    module Server =

        open IntelliFactory.Html
        open Mongo

        let newsItemData (newsItem : NewsItem) = newsItem.Title, newsItem.Url, newsItem.Summary

//        [<Rpc>]
        let latestNews() =
//            async {
//                let array =
            News.latest10()
            |> Seq.toArray
            |> Array.map newsItemData
//                return array
//            }
//
        let makeLi (title, url, summary) =
            LI [Class "news-item"] -< [
                A [HRef url] -< [Strong [Text title]]
                P [Text summary]
            ]

        let newsList() =
            UL [Class "unstyled"; Id "newsList"] -< [
//            |>! OnAfterRender(fun ul ->
//                async {
                for x in latestNews() do yield makeLi x
            ]
//                |> Array.iter (fun x -> ul.Append x)
//                } |> Async.Start)

//    module Client =
//
//        open IntelliFactory.WebSharper.Html
//
//        [<JavaScriptAttribute>]
//        let makeLi (title, url, summary) =
//            LI [
//                A [HRef url] -< [Strong [Text title]]
//                P [Text summary]
//            ]
//
//        [<JavaScript>]
//        let newsList() =
//            UL [Attr.Class "unstyled"; Id "newsList"]
//            |>! OnAfterRender(fun ul ->
//                async {
//                    let! news = Server.latestNews()
//                    news
//                    |> Array.map makeLi
//                    |> Array.iter (fun x -> ul.Append x)
//                } |> Async.Start)
//
//        type NewsViewer() =
//
//            inherit Web.Control()
//
//            [<JavaScript>]
//            override __.Body = newsList() :> _
