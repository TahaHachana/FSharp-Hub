namespace Website

module Views =

    open IntelliFactory.Html
    open Content
    open Model
    open ExtSharper

    let mainTemplate = Skin.MakeDefaultTemplate "~/Main.html" Skin.LoadFrequency.Once 
    
    let withMainTemplate = Skin.WithTemplate<Action> mainTemplate
    
    let loginInfo' = Shared.loginInfo Logout Login

    let home =
        withMainTemplate Home.title Home.metaDescription <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Home.navigation
                    Div [new Forkme.Viewer()]
                    Home.heroUnit
                    Div [Class "container"] -< [
                        Home.row1
                        Home.row2()
                    ]
                ]
                Shared.footer
                Shared.ga
            ]

    let custom404 =
        withMainTemplate "Error - Page Not Found" "" <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Div [Class "container"] -< [
                        P [Text "The page you're trying to access doesn't exist. "] -< [
                            A [HRef <| ctx.Link Home] -< [Text "Click here"]
                            Text " to return to the home page."
                        ]
                    ]
                ]
                Shared.footer
            ]

    let books =
        withMainTemplate Books.title Books.metaDescription <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Books.navigation
                    Div [new Forkme.Viewer()]
                    Div [Class "container"] -< [
                        Books.header
                        Div [Books.Server.booksDiv ()]
                    ]
                ]
                Shared.footer
                Shared.ga
            ]

    let videos =
        Videos.Server.divs ()
        |> Array.map (fun (pageId, element) ->
            let title = Videos.title pageId
            let navigation =
                match pageId with
                    | 1 -> Videos.navigation
                    | _ -> Shared.navigation
            let view = withMainTemplate title Videos.metaDescription <| fun ctx ->
                [
                    Div [Id "wrap"] -< [
                        navigation
                        Div [new Forkme.Viewer()]
                        Div [Class "container"] -< [
                            Videos.header
                            Div [element]
                            Span [
                                Id "pager"
                                Attributes.HTML5.Data "pages-count" Videos.Server.pagesCount
                                Attributes.HTML5.Data "previous" (string (pageId - 1))
                                Attributes.HTML5.Data "next" (string (pageId + 1))]
                            Div [Class "offset5 span2"] -< [new Videos.Client.PagerViewer()]
                        ]
                    ]
                    Shared.footer
                    Shared.ga
                ]
            pageId, view)

    let videosView pageId =
        videos
        |> Array.find (fun (id, _) -> id = pageId)
        |> snd

    let resources =
        withMainTemplate Resources.title Resources.metaDescription <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Resources.navigation
                    Div [new Forkme.Viewer()]
                    Div [Class "container"] -< [
                        Resources.header
                        Resources.tabs
                    ]
                ]
                Shared.footer
                Shared.ga
            ]

    let ecosystem =
        withMainTemplate Ecosystem.title Ecosystem.metaDescription <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Ecosystem.navigation
                    Div [new Forkme.Viewer()]
                    Div [Class "container"] -< [
                        Ecosystem.header
                        Ecosystem.websharper
                        HR []
                        Ecosystem.fcore
                        HR []
                        Ecosystem.aleacubase
                    ]
                ]
                Shared.footer
                Shared.ga
            ]

    let login (redirectAction: option<Action>) =
        withMainTemplate "Login" "" <| fun ctx ->
            let redirectLink =
                match redirectAction with
                | Some action -> action
                | None        -> Action.Admin
                |> ctx.Link
            [
                Div [Id "wrap"] -< [
                    Div [Class "container"] -< [
                        Shared.navigation
                        Div [Id "login"] -< [
                            H1 [Text "Login"]
                            Div [new Login.Client.Control(redirectLink)]
                        ]
                    ]
                ]
                Shared.footer
                Shared.ga
            ]

    let admin =
        withMainTemplate "Admin Page" "" <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Shared.navigation
                    Div [Class "container"] -< [
                        loginInfo' ctx
                        Div [Id "admin"] -< [
                            Div [Class "row"] -< [
                                Div [Class "span4"] -< [A [Class "btn btn-large home-btn"; HRef <| ctx.Link BooksAdmin] -< [Text "Books"]]
                                Div [Class "span4"] -< [A [Class "btn btn-large home-btn"; HRef <| ctx.Link VideosAdmin] -< [Text "Videos"]]
                                Div [Class "span4"] -< [A [Class "btn btn-large home-btn"; HRef <| ctx.Link NewsAdmin] -< [Text "News"]]
                            ]
                        ]
                    ]
                ]
                Shared.footer
            ]

    let booksAdmin =
        withMainTemplate "" "" <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Shared.navigation
                    Div [Class "container"] -< [
                        loginInfo' ctx
                        Div [Id "books-admin"] -< [new BooksAdmin.Client.Control()]                  
                    ]
                ]
                Shared.footer
            ]

    let videosAdmin =
        withMainTemplate "" "" <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Shared.navigation
                    Div [Class "container"] -< [
                        loginInfo' ctx
                        Div [Id "videos-admin"] -< [
                            H2 [Text "Add new video"]
                            Div [new VideosAdmin.Client.Control()]
                        ]                  
                    ]
                ]
                Shared.footer
            ]

    let newsAdmin =
        withMainTemplate "" "" <| fun ctx ->
            [
                Div [Id "wrap"] -< [
                    Shared.navigation
                    Div [Class "container"] -< [
                        loginInfo' ctx
                        Div [Id "news-admin"] -< [
                            H2 [Text "Add news item"]
                            Div [new NewsAdmin.Client.Control()]
                        ]                  
                    ]
                ]
                Shared.footer
            ]