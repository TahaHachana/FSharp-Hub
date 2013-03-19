namespace Website

open IntelliFactory.WebSharper.Sitelets
open Model
open Views

module Controller =

    let controller =

        let handle = function
            | Home          -> homeView
            | Books         -> booksView
            | Custom404     -> custom404View
            | Videos pageId -> videosView pageId
            | Resources     -> resourcesView

        { Handle = handle }
