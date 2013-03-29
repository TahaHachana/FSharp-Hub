namespace Website

open IntelliFactory.WebSharper

module BooksAdmin =

    module Server =
        
        open System
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

        [<Rpc>]
        let addBook url title authors publisher isbn pages date cover =
            async {
                let authors' = Array.ofList authors
                let pages' = int pages
                let isDate, datetime = DateTime.TryParse date
                match isDate with
                    | false -> return false
                    | true  ->
                        let book = Book.Make url title authors' publisher isbn pages' datetime cover
                        let isOk = Books.insert book
                        return isOk
            }

    module Client =
        
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.Formlet

        [<JavaScript>]
        let addFormlet =
            let urlInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "URL"
            let titleInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Title"
            let authorInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Author"
                |> Enhance.Many
                |> Enhance.WithFormContainer
            let publisherInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Publisher"
            let isbnInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "ISBN"
            let pagesCountInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Pages"
            let releaseDateInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Release Date"
            let coverInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Cover URL"
            let formlet =
                Formlet.Yield (fun url title authors publisher isbn pages date cover -> url, title, authors, publisher, isbn, pages, date, cover)
                <*> urlInput
                <*> titleInput
                <*> authorInput
                <*> publisherInput
                <*> isbnInput
                <*> pagesCountInput
                <*> releaseDateInput
                <*> coverInput
                |> Enhance.WithSubmitAndResetButtons
                |> Enhance.WithFormContainer
            Formlet.Run (fun (url, title, authors, publisher, isbn, pages, date, cover) ->
                async {
                    let! isOk = Server.addBook url title authors publisher isbn pages date cover
                    match isOk with
                        | false -> JavaScript.Alert "The query failed."
                        | true  -> JavaScript.Alert "New book inserted successfully."
                }
                |> Async.Start) formlet

        [<JavaScript>]
        let tr title publisher =
            TR [
                TD [Text title]
                TD [Text publisher]
            ]

        [<JavaScript>]
        let booksTable =
            Table [Attr.Class "table table-striped"] -< [
                TR [
                    TH [Text "Title"]
                    TH [Text "Publisher"]
                ]
            ] |>! OnAfterRender(fun elt ->
                async {
                    let! booksData = Server.books()
                    booksData
                    |> Array.map (fun (title, publisher) -> tr title publisher)
                    |> Array.iter elt.Append
                }
                |> Async.Start)

        [<JavaScript>]
        let main() =
            Div [Attr.Class "row"] -< [
                Div [Attr.Class "span6"] -< [
                    H2 [Text "Add new book"]                
                    Div [addFormlet]
                ]
                Div [Attr.Class "span6"] -< [
                    H2 [Text "Books in database"]                                
                    booksTable
                ]
            ]

        type Control() =
            
            inherit Web.Control()

            [<JavaScript>]
            override __.Body = main() :> _