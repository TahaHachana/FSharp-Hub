namespace Website

open IntelliFactory.WebSharper

module News =
    
    module Server =

        open Mongo

        let newsItemData (newsItem : NewsItem) = newsItem.Title, newsItem.Url, newsItem.Summary

        [<Rpc>]
        let latestNews() =
            async {
                let array =
                    News.latest10()
                    |> Seq.toArray
                    |> Array.map newsItemData
                return array
            }

    module Client =

        open IntelliFactory.WebSharper.Html

        [<JavaScriptAttribute>]
        let makeLi (title, url, summary) =
            LI [
                A [HRef url] -< [Strong [Text title]]
                P [Text summary]
            ]

        [<JavaScript>]
        let newsList() =
            UL [Attr.Class "unstyled"; Id "newsList"]
            |>! OnAfterRender(fun ul ->
                async {
                    let! news = Server.latestNews()
                    news
                    |> Array.map makeLi
                    |> Array.iter (fun x -> ul.Append x)
                } |> Async.Start)

        type NewsViewer() =

            inherit Web.Control()

            [<JavaScript>]
            override __.Body = newsList() :> _
