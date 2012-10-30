namespace FSharpWebsite

open IntelliFactory.Html
open SiteContent
open Model

module View =

    let homeView =
        Skin.withTemplate HomeContent.title HomeContent.metaDescription <| fun ctx ->
            [
                HomeContent.navigation
                SharedContent.forkme
                HomeContent.heroUnit
                Div [Class "container"] -< [
                    HomeContent.row1
                    HomeContent.row2
                    HomeContent.row3
                ]
            ]

    let custom404View =
        Skin.withTemplate "" "" <| fun ctx ->
            [
                Div [
                    P [Text "The page you're trying to access doesn't exist."]
                    LI [A [HRef <| ctx.Link Home] -< [Text "Home"]]
                ]
            ]

    let booksView =
        Skin.withTemplate BooksPageContent.title BooksPageContent.metaDescription <| fun ctx ->
            [
                BooksPageContent.navigation
                SharedContent.forkme
                Div [Class "container"] -< [
                    BooksPageContent.header
                    Div [FSharpBooks.Server.booksDiv ()]
                ]
            ]

    let videosViews =
        FSharpVideos.Server.divs ()
        |> Array.map (fun (pageId, element) ->
            
            let title = VideosPageContent.title pageId
            let navigation =
                match pageId with
                    | 1 -> VideosPageContent.navigation
                    | _ -> SharedContent.navigation

            let view = Skin.withTemplate title VideosPageContent.metaDescription <| fun ctx ->
                [
                    navigation
                    SharedContent.forkme
                    Div [Class "container"] -< [
                        VideosPageContent.header
                        Div [element]
                        Span [
                            Id "pager"
                            Attributes.HTML5.Data "pages-count" FSharpVideos.Server.pagesCount
                            Attributes.HTML5.Data "previous" (string (pageId - 1))
                            Attributes.HTML5.Data "next" (string (pageId + 1))]
                        Div [Class "offset6 span2"] -< [new FSharpVideos.PagerViewer()]
                    ]
                ]
            pageId, view)


    let videosView pageId =
        videosViews
        |> Array.find (fun (id, _) -> id = pageId)
        |> snd

    let resourcesView =
        Skin.withTemplate ResourcesPageContent.title ResourcesPageContent.metaDescription <| fun ctx ->
            [
                ResourcesPageContent.navigation
                SharedContent.forkme
                Div [Class "container"] -< [
                    ResourcesPageContent.header
                    ResourcesPageContent.tabs
                ]
            ]

