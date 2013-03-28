namespace Website

module Views =

    open IntelliFactory.Html
    open Content
    open Model
    open ExtSharper

    let mainTemplate = Skin.MakeDefaultTemplate "~/Main.html" Skin.LoadFrequency.Once 
    let withMainTemplate = Skin.WithTemplate<Action> mainTemplate

    let home =
        withMainTemplate Home.title Home.metaDescription <| fun ctx ->
            [
                Home.navigation
                Div [new Forkme.Viewer()]
                Home.heroUnit
                Div [Class "container"] -< [
                    Home.row1
                    Home.row2
                ]
                Shared.footer
            ]

    let custom404 =
        withMainTemplate "Error - Page Not Found" "" <| fun ctx ->
            [
                Div [Class "container"] -< [
                    P [Text "The page you're trying to access doesn't exist. "] -< [
                        A [HRef <| ctx.Link Home] -< [Text "Click here"]
                        Text " to return to the home page."
                    ]
                ]
            ]

    let books =
        withMainTemplate Books.title Books.metaDescription <| fun ctx ->
            [
                Books.navigation
                Div [new Forkme.Viewer()]
                Div [Class "container"] -< [
                    Books.header
                    Div [Books.Server.booksDiv ()]
                ]
                Shared.footer
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
                        Div [Class "offset6 span2"] -< [new Videos.Client.PagerViewer()]
                    ]
                    Shared.footer
                ]
            pageId, view)

    let videosView pageId =
        videos
        |> Array.find (fun (id, _) -> id = pageId)
        |> snd

    let resources =
        withMainTemplate Resources.title Resources.metaDescription <| fun ctx ->
            [
                Resources.navigation
                Div [new Forkme.Viewer()]
                Div [Class "container"] -< [
                    Resources.header
                    Resources.tabs
                ]
                Shared.footer
            ]

    let login (redirectAction: option<Action>) =
        withMainTemplate "Login" "" <| fun ctx ->
            let redirectLink =
                match redirectAction with
                | Some action -> action
                | None        -> Action.Admin
                |> ctx.Link
            [
                Div [Class "container"] -< [
                    Shared.navigation
                    Shared.loginInfo ctx
                    Div [Id "login"] -< [
                        H1 [Text "Login"]
                        Div [new Login.Client.Control(redirectLink)]
                    ]
                ]
            ]

    let admin =
        withMainTemplate "Admin Page" "" <| fun ctx ->
            [
                Shared.navigation
                Div [Class "container"] -< [
                    Shared.loginInfo ctx
                    Div [Id "admin"] -< [
                        Div [Class "row"] -< [
                            Div [Class "span4"] -< [A [Class "btn btn-large home-btn"; HRef <| ctx.Link BooksAdmin] -< [Text "Books"]]
                            Div [Class "span4"] -< [A [Class "btn btn-large home-btn"; HRef "#"] -< [Text "Videos"]]
                            Div [Class "span4"] -< [A [Class "btn btn-large home-btn"; HRef "#"] -< [Text "News"]]
                        ]
                    ]
                ]
            ]

    let booksAdmin =
        withMainTemplate "Admin Page" "" <| fun ctx ->
            [
                Shared.navigation
                Div [Id "books-admin"; Class "container"] -< [
                    Shared.loginInfo ctx
                    Div [Style "margin-top: 50px;"] -< [
                        Div [Class "row"] -< [Button [Class "btn btn-primary btn-large"; Id "add-book"] -< [Text "Add New Book"]]
                        Div [Class "row"] -< [Div [Style "margin-top: 20px;"; Class "span8"] -< [new BooksAdmin.Client.Control()]]
                    ]                  
                ]
            ]


