namespace Website

module Controller =

    open IntelliFactory.WebSharper.Sitelets
    open Model

    let protect() =
        let user = UserSession.GetLoggedInUser()
        match user with
            | None    -> Content.Redirect <| Login None
            | _       -> Views.admin

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
            | Admin         -> protect()
            | Login action  -> Views.login action
            | Logout        -> logout()
            

        { Handle = handle }