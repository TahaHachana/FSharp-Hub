namespace Website

open IntelliFactory.WebSharper

module News =
    open IntelliFactory.Html
    open Mongo

    let itemData (newsItem : NewsItem) = newsItem.Title, newsItem.Url, newsItem.Summary

    let latest() =
        News.latest10()
        |> Seq.toArray
        |> Array.map itemData

    let li (title, url, summary) =
        LI [Class "news-item"] -< [
            A [HRef url; Rel "nofollow"; Target "_blank"] -< [Strong [Text title]]
            P [Text summary]
        ]

    let list() =
        UL [Class "unstyled"; Id "news-list"] -< [
            for x in latest() -> li x
        ]