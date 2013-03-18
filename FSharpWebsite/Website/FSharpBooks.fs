namespace FSharpWebsite

open IntelliFactory.WebSharper

module FSharpBooks =

    open IntelliFactory.Html
    open IntelliFactory.WebSharper.Sitelets
    open Mongo

    module Server =

        let fsharpBooks =
            Books.queryFsharpBooks ()
            |> Array.map (fun x ->
                x.Url, x.Cover, x.Title, x.Authors, x.Publisher, x.ISBN, x.Pages.ToString())
            |> Utilities.Server.toChunks 3
            |> Seq.toArray

        let inline makeThumbnailLi (url, cover, title, authors, publisher, isbn, pages) =
            let authors' =
                let length = Seq.length authors
                let str = String.concat ", " authors
                match length with
                    | 1 -> "Author: "  + str
                    | _ -> "Authors: " + str
            LI [Class "span4"] -< [
                Div [Class "thumbnail"] -< [
                    Img [Class "cover"; Src "/Images/Books/pixel.png"; HTML5.Data "src" cover; Alt title; Height "220"]
                    H4 [Text title]
                    P [Text authors']
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

    module Client =
    
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        [<JavaScript>]
        let replaceSrc () =
            JQuery.Of(".cover").Each(fun (elem : Dom.Element) _ ->
                let dataSrc = elem.GetAttribute "data-src"
                elem.SetAttribute("src", dataSrc)).Ignore

        [<JavaScript>]
        let hiddenDiv () =
            Div [Attr.Style "display: none;"] -< [Text "test"]
            |>! OnAfterRender (fun _ -> replaceSrc ())

        type HiddenDivViewer () =
        
            inherit IntelliFactory.WebSharper.Web.Control ()

            [<JavaScript>]
            override __.Body = hiddenDiv () :> _

