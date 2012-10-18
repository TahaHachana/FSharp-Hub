namespace FSharpWebsite

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open Mongo

module FSharpBooks =
     
    module Server =

        let toSeqTriplesFast (items:seq<_>) =
          use e = items.GetEnumerator()
          let rec loop() =
            seq {
              if e.MoveNext() then
                let a = e.Current
                if e.MoveNext() then 
                  let b = e.Current
                  if e.MoveNext() then
                    let c = e.Current
                    yield (a, b, c)
                    yield! loop()
            }
          loop()

        [<RpcAttribute>]
        let fsharpBooks () =
            async {
                let books = Books.queryFsharpQuestions ()
                let booksData =
                    books
                    |> Array.map (fun x ->
                        x.Url, x.Cover, x.Title, x.Authors, x.Publisher, x.ISBN, x.Pages.ToString())
                    |> toSeqTriplesFast
                    |> Seq.toArray
                return booksData
            }

    module Client =

        [<JavaScriptAttribute>]
        let makeThumbnailLi (url, cover, title, authors, publisher, isbn, pages) =
            LI [Attr.Class "span4"] -< [
                Div [Attr.Class "thumbnail"] -< [
                    Img [Src cover; Alt title; Width "180"; Height "220"]
                    H3 [Text title]
                    P [Text <| "Authors: " + String.concat ", " authors]
                    P [Text <| "Pbulisher: " + publisher]
                    P [Text <| "ISBN: " + isbn]
                    P [Text <| "Pages: " + pages]
                    A [HRef url; Attr.Class "btn btn-primary"; Attr.Target "_blank"] -< [Text "Book Website"]
                ]
            ]

        [<JavaScriptAttribute>]
        let booksDiv () =
            let makeBooksUl (li : Element) li' li'' =
                UL [Attr.Class "thumbnails"] -< [
                    li
                    li'
                    li''
                ]

            let makeDiv ul = Div [Attr.Class "row-fluid"] -< [ul]


            Div [] //-< [booksUl]
            |>! OnAfterRender (fun div ->
                async {
                    let! books = Server.fsharpBooks ()
                    books
                    |> Seq.map (fun (x, y, z) ->
                        let li = makeThumbnailLi x
                        let li' = makeThumbnailLi y
                        let li'' = makeThumbnailLi z
                        li, li', li'')
                    |> Seq.map (fun (x, y, z) -> makeBooksUl x y z)
                    |> Seq.map makeDiv
                    |> Seq.iter div.Append
                } |> Async.Start)
                
    type FsharpBooksViewer () =
        inherit Web.Control ()

        [<JavaScriptAttribute>]
        override this.Body =
            Client.booksDiv () :> _


