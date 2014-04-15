module Website.BooksAdmin

open IntelliFactory.WebSharper

module Server =
    open System
    open Mongo
    open Records

    [<Remote>]
    let books() =
        async {
            let booksData =
                Books.all()
                |> Seq.toArray
                |> Array.map (fun x -> x.Title, x.Publisher)
            return booksData
        }

    [<Remote>]
    let addBook url title authors publisher isbn pages date cover =
        async {
            let authors' = Array.ofList authors
            let pages' = int pages
            let isDate, datetime = DateTime.TryParse date
            match isDate with
            | false -> return false
            | true  ->
                let book = Book.New url title authors' publisher isbn pages' datetime cover
                let isOk = Books.insert book
                return isOk
        }

[<JavaScript>]
module Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.Formlet

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
            Formlet.Yield
                (fun url title authors publisher isbn pages date cover ->
                    url, title, authors, publisher, isbn, pages, date, cover)
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

    let tr title publisher =
        TR [
            TD [Text title]
            TD [Text publisher]
        ]

    let main() =
        Div [Attr.Class "row"] -< [
            Div [
                H1 [Text "Add new book"]                
                Div [addFormlet]
            ]
        ]

type Control() =
            
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = Client.main() :> _