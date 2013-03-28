namespace Website

open IntelliFactory.WebSharper.Sitelets.Content

module Model =

    type PageId = int

    type Action =
        | Home
        | [<CompiledName("books")>] Books
        | [<CompiledName("custom404")>] Custom404
        | [<CompiledName("resources")>] Resources
        | [<CompiledName("videos")>] Videos of PageId
        | [<CompiledName("login")>] Login of Action option
        | [<CompiledName("logout")>] Logout
        | [<CompiledName("admin")>] Admin
        | BooksAdmin

    type Page =
        {
            Title           : string
            MetaDescription : string
            Body            : HtmlElement list
        }