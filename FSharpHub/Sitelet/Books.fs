module Website.Books

open IntelliFactory.Html
open Records
open Mongo

let bookData book =
    book.Url, book.Cover, book.Title,
    book.Authors, book.Publisher,
    book.ISBN, book.Pages.ToString()

let booksData() =
    Books.all()
    |> Seq.map bookData
    |> Utils.split 3
    |> List.map (fun x -> Seq.toList x)

let authorsStr authors =
    let str = String.concat ", " authors
    match Seq.length authors with
    | 1 -> "Author: " + str
    | _ -> "Authors: " + str

let coverLink url cover title =
    A [HRef url; Target "_blank"] -< [
        Img [
            Class "cover"
            Src cover
            Alt title
            Height "220"
        ]
    ]

let caption title authors publisher isbn pages =
    Div [Class "caption"] -< [
        H4 [Text title]
        P [Text <| authorsStr authors]
        P [Text <| "Publisher: " + publisher]
        P [Text <| "ISBN: " + isbn]
        P [Text <| "Pages: " + pages]
    ]

let thumbnail (url, cover, title, authors, publisher, isbn, pages) =
    Div [Class "col-lg-4"] -< [
        Div [Class "thumbnail"] -< [
            coverLink url cover title
            caption title authors publisher isbn pages
        ]
    ]

let rows divs = Div [Class "row books-row"] -< divs

let main() =
    booksData()
    |> List.map (fun x ->
        List.map thumbnail x)
    |> List.map rows