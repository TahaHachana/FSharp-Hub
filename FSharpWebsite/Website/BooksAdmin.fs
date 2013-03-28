namespace Website

open IntelliFactory.WebSharper

module BooksAdmin =

    module Server =

        open Mongo

        [<Rpc>]
        let books() =
            async {
                let booksData =
                    Books.all()
                    |> Seq.toArray
                    |> Array.map (fun x -> x.Title, x.Publisher)
                return booksData
            }

    module Client =
        
        open IntelliFactory.WebSharper.Html
        
        [<JavaScript>]
        let tr title publisher =
            TR [
                TD [Text title]
                TD [Text publisher]
            ]

        [<JavaScript>]
        let main() =
            Table [Attr.Class "table table-striped"] -< [
                TR [
                    TH [Text "Title"]
                    TH [Text "Publisher"]
                ]
            ]
            |>! OnAfterRender(fun elt ->
                async {
                    let! booksData = Server.books()
                    booksData
                    |> Array.map (fun (title, publisher) -> tr title publisher)
                    |> Array.iter elt.Append
                }
                |> Async.Start)

        type Control() =
            
            inherit Web.Control()

            [<JavaScript>]
            override __.Body = main() :> _