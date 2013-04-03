namespace Website

open IntelliFactory.WebSharper.Sitelets.Content

module Model =

    type PageId = int

    type Action =
        | Home
        | [<CompiledName("books")>] Books
        | BooksAdmin
        | [<CompiledName("custom404")>] Custom404
        | [<CompiledName("resources")>] Resources
        | [<CompiledName("videos")>] Videos of PageId
        | [<CompiledName("login")>] Login of Action option
        | [<CompiledName("logout")>] Logout
        | [<CompiledName("admin")>] Admin
        | VideosAdmin
        | NewsAdmin
        | [<CompiledName("ecosystem")>] Ecosystem

    type Page =
        {
            Title           : string
            MetaDescription : string
            Body            : HtmlElement list
        }