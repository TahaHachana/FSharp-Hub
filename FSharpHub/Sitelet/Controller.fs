module Website.Controller

open IntelliFactory.WebSharper.Sitelets
open Model

let protect view =
    let user = UserSession.GetLoggedInUser()
    match user with
    | None -> Content.Redirect <| Login None
    | _ -> view

let logout() =
    UserSession.Logout ()
    Content.Redirect Home
    
let controller =

    let handle = function
        | Home          -> Views.home
        | Books         -> Views.books
        | Error         -> Views.error
        | Videos pageId -> Views.videos pageId
        | Admin         -> protect Views.admin
        | Login action  -> Views.login action
        | Logout        -> logout()
        | BooksAdmin    -> protect Views.booksAdmin
        | VideosAdmin   -> protect Views.videosAdmin
        | Rss -> Views.rss()

    { Handle = handle }