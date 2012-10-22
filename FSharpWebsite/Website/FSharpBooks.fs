﻿namespace FSharpWebsite

open IntelliFactory.WebSharper
open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets
open Mongo

module FSharpBooks =
     
    module Server =

        let fsharpBooks =
            Books.queryFsharpQuestions ()
            |> Array.map (fun x ->
                x.Url, x.Cover, x.Title, x.Authors, x.Publisher, x.ISBN, x.Pages.ToString())
            |> Utilities.toChunks 3
            |> Seq.toArray

        let inline makeThumbnailLi (url, cover, title, authors, publisher, isbn, pages) =
            LI [Class "span4"] -< [
                Div [Class "thumbnail"] -< [
                    Img [Class "cover"; Src cover; Alt title; Height "220"]
                    H4 [Text title]
                    P [Text <| "Authors: " + String.concat ", " authors]
                    P [Text <| "Publisher: " + publisher]
                    P [Text <| "ISBN: " + isbn]
                    P [Text <| "Pages: " + pages]
                    A [HRef url; Class "btn btn-block"; Target "_blank"] -< [Text "Book Website"]
                ]
            ]

        let inline makeBooksUl (lis : Element<_> list) =
            UL [Class "thumbnails"] -< lis

        let inline makeDiv (ul : Element<_>) = Div [Class "row-fluid"] -< [ul]

        let inline makeDiv' (lis : Element<_> []) = Div [] -< lis

        let inline divs () =
            fsharpBooks
            |> Array.map (fun x ->
                List.map makeThumbnailLi x)
            |> Array.map makeBooksUl
            |> Array.map makeDiv
      
        let inline booksDiv () = makeDiv' <| divs ()


