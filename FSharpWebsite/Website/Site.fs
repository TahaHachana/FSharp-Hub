namespace Website

open IntelliFactory.WebSharper.Sitelets
open Model
open Controller

module Site =

    let router : Router<Action> =
        Router.Table
            [
                Home       , "/"
                BooksAdmin , "/admin/books"
                VideosAdmin, "/admin/videos"
                NewsAdmin  , "/admin/news"
                Login None , "/login"
                Custom404  , "/custom404"
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
        member this.Sitelet = Site.Main
        member this.Actions = []

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()