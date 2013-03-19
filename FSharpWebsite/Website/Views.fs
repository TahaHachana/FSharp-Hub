namespace Website

open IntelliFactory.Html
open Content
open Model
open ExtSharper

module Views =

    let mainTemplate = Skin.MakeDefaultTemplate "~/Main.html" Skin.LoadFrequency.Once 
    let withMainTemplate = Skin.WithTemplate<Action> mainTemplate

    let homeView =
        withMainTemplate HomeContent.title HomeContent.metaDescription <| fun ctx ->
            [
                HomeContent.navigation
                Div [new Shared.Client.ForkmeViewer()]
                HomeContent.heroUnit
                Div [Class "container"] -< [
                    HomeContent.row1
                    HomeContent.row2
                ]
                Shared.Server.footer
            ]

    let custom404View =
        withMainTemplate "Error - Page Not Found" "" <| fun ctx ->
            [
                Div [Class "container"] -< [
                    P [Text "The page you're trying to access doesn't exist. "] -< [
                        A [HRef <| ctx.Link Home] -< [Text "Click here"]
                        Text " to return to the home page."
                    ]
                ]
            ]

    let booksView =
        withMainTemplate BooksPageContent.title BooksPageContent.metaDescription <| fun ctx ->
            [
                BooksPageContent.navigation
                Div [new Shared.Client.ForkmeViewer()]
                Div [Class "container"] -< [
                    BooksPageContent.header
                    Div [FSharpBooks.Server.booksDiv ()]
                ]
                Shared.Server.footer
            ]

    let videosViews =
        FSharpVideos.Server.divs ()
        |> Array.map (fun (pageId, element) ->
            
            let title = VideosPageContent.title pageId
            let navigation =
                match pageId with
                    | 1 -> VideosPageContent.navigation
                    | _ -> Shared.Server.navigation

            let view = withMainTemplate title VideosPageContent.metaDescription <| fun ctx ->
                [
                    navigation
                    Div [new Shared.Client.ForkmeViewer()]
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
                    Shared.Server.footer
                ]
            pageId, view)

    let videosView pageId =
        videosViews
        |> Array.find (fun (id, _) -> id = pageId)
        |> snd

    let resourcesView =
        withMainTemplate ResourcesPageContent.title ResourcesPageContent.metaDescription <| fun ctx ->
            [
                ResourcesPageContent.navigation
                Div [new Shared.Client.ForkmeViewer()]
                Div [Class "container"] -< [
                    ResourcesPageContent.header
                    ResourcesPageContent.tabs
                ]
                Shared.Server.footer
            ]

