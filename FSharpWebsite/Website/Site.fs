module Website.Site

open IntelliFactory.WebSharper.Sitelets
open Model
open Controller

let router =
    Router.Table
        [
            Home       , "/"
            BooksAdmin , "/admin/books"
            VideosAdmin, "/admin/videos"
            Login None , "/login"
            Error      , "/error"
            Books      , "/books"
            Admin      , "/admin"
        ]
    <|>
    Router.Infer()

let Main =
    {
        Controller = controller
        Router     = router
    }
    
type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Main
        member this.Actions = []

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()