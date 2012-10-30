namespace FSharpWebsite

open IntelliFactory.WebSharper.Sitelets

module Model =

    type PageId = int

    type Action =
        | Home
        | Books
        | Custom404
        | Resources
        | Videos of PageId

    type Page =
        {
            Title           : string
            MetaDescription : string
            Body            : Content.HtmlElement list
        }