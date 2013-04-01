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
                A [HRef url; Rel "nofollow"] -< [Strong [Text title]]
                P [Text summary]
            ]

        let newsList() =
            UL [Class "unstyled"; Id "newsList"] -< [
                for x in latestNews() do yield makeLi x
            ]