module Sitelet.Site

open Controller
open IntelliFactory.WebSharper.Sitelets
open Model

let router =
    Router.Table
        [
            Home, "/"
            BooksAdmin, "/admin/books"
            VideosAdmin, "/admin/videos"
            Login None, "/login"
            Error, "/error"
            Books, "/books"
            Admin, "/admin"
            CheckNewData, "/check-new-data"
        ]
    <|>
    Router.Infer()

let Main =
    {
        Controller = controller
        Router = router
    }
    
type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Main
        member this.Actions = []

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()