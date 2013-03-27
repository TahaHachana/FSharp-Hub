namespace Website

module Controller =

    open IntelliFactory.WebSharper.Sitelets
    open Model

    let controller =

        let handle = function
            | Home          -> Views.home
            | Books         -> Views.books
            | Custom404     -> Views.custom404
            | Videos pageId -> Views.videosView pageId
            | Resources     -> Views.resources

        { Handle = handle }