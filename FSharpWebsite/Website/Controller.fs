namespace Website

module Controller =

    open IntelliFactory.WebSharper.Sitelets
    open Model

    let protect view =
        let user = UserSession.GetLoggedInUser()
        match user with
            | None    -> Content.Redirect <| Login None
            | _       -> view

    let logout() =
        UserSession.Logout ()
        Content.Redirect Home
    
    let controller =

        let handle = function
            | Home          -> Views.home
            | Books         -> Views.books
            | Custom404     -> Views.custom404
            | Videos pageId -> Views.videosView pageId
            | Resources     -> Views.resources
            | Admin         -> protect Views.admin
            | Login action  -> Views.login action
            | Logout        -> logout()
            | BooksAdmin    -> protect Views.booksAdmin
            | VideosAdmin   -> protect Views.videosAdmin
            | NewsAdmin     -> protect Views.newsAdmin

        { Handle = handle }