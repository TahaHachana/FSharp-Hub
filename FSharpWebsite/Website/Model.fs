namespace Website

open IntelliFactory.WebSharper.Sitelets.Content

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
            Body            : HtmlElement list
        }