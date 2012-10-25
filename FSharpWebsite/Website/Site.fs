namespace FSharpWebsite

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

[<AutoOpenAttribute>]
module Helpers =

    let ( => ) text url =
        A [HRef url] -< [Text text]

[<AutoOpenAttribute>]
module Models =

    type PageId = int

    type Action =
        | Home
        | Books
        | Custom404
        | Resources
        | Videos of PageId

    type Page =
        {
            Title           : string
            MetaDescription : string
            Body            : list<Content.HtmlElement>
        }

module Skin =

    open System.Web

    let templateLoadFrequency = Content.Template.Once
//        #if DEBUG
//        Content.Template.PerRequest
//        #else
//        Content.Template.Once
//        #endif

    let mainTemplate =
        let path = HttpContext.Current.Server.MapPath("~/Main.html")
        Content.Template<Page>(path, templateLoadFrequency)
            .With("title", fun x -> x.Title)
            .With("metaDescription", fun x -> x.MetaDescription)
            .With("body", fun x -> x.Body)

    let withTemplate title metaDescription body : Content<Action> =
        Content.WithTemplate mainTemplate <| fun context ->
            {
                Title           = title
                MetaDescription = metaDescription
                Body            = body context
            }

module SharedContent =

    let navigation =
            Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    UL [Class "nav"] -< [
                        LI [A [HRef "/Home"] -< [Text "Home"]]
                        LI [A [HRef "/Books"] -< [Text "Books"]]
                        LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                        LI [A [HRef "/Resources"] -< [Text "Resources"]]
                    ]
                ]
                Div [Class "alert alert-info"; Id "alertDiv"] -< [
                    P [Class "centered"; Id "alertText"] -< [Text ""]
                ]
            ]

    let forkme =
        A [HRef "https://github.com/TahaHachana/FSharpWebsite"; Target "_blank"] -< [
            Img [
                Src "https://s3.amazonaws.com/github/ribbons/forkme_right_darkblue_121621.png"
                Alt "Fork me on GitHub"
                Id "forkme"]
        ]

module HomeContent =
    
    let title = "FSharp Programming Language"

    let metaDescription = ""

    let navigation =
            Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    UL [Class "nav"] -< [
                        LI [Class "active"] -< [A [HRef "/"] -< [Text "Home"]]
                        LI [A [HRef "/Books"] -< [Text "Books"]]
                        LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                        LI [A [HRef "/Resources"] -< [Text "Resources"]]
                    ]
                ]
                Div [Class "alert alert-info"; Id "alertDiv"] -< [
                    P [Class "centered"; Id "alertText"] -< [Text ""]
                ]
            ]

    let definition =
            P [
                Strong [Text "FSharp"]
                Text " is an advanced, multi-paradigm, strongly typed open source programming language.
                    F# allows you to solve complex problems with simple code."
            ]

    let heroUnit =
        Div [Class "hero-unit"] -< [
            Div [Class "container centered"] -< [
                H1 [Text "FSharp Programming Language"]
                definition
            ]
        ]

    let row1 =
        Div [Class "row-fluid"] -< [
            Div [Class "span4"] -< [
                H2 [Class "centered"] -< [Text "Succinct"]
                P [Text "FSharp's syntax is clean yet powerful and readable"]
            ]
            Div [Class "span4"] -< [
                H2 [Class "centered"] -< [Text "Multi-Paradigm"]
                P [Text "F# is a functional first language with support for object oriented and imperative programming"]
            ]
            Div [Class "span4"] -< [
                H2 [Class "centered"] -< [Text "F# Library"]
                P [Text "FSharp is a modern language that comes with it's own library"]
            ]
        ]

    let row2 =
        Div [Class "row-fluid"] -< [
            Div [Class "span4 offset4 centered"] -< [
                Button [Class "btn btn-primary btn-large pull-left"] -< [Text "Learn More"]
                Button [Class "btn btn-success btn-large pull-right"] -< [Text "Download F#"]
            ]
        ]

    let row3 =
        Div [Class "row-fluid"] -< [
            Div [Class "span4"] -< [
                H3 [Class "centered"] -< [Text "Tweets"]
                Div [
                    Id "fsharpTweets"
                    Attributes.HTML5.Data "tweets-count" "0"
                    Attributes.HTML5.Data "tweet-id" ""
                ] -< [new FSharpTweets.FsharpTweetsViewer ()]
            ]
            Div [Class "span4"] -< [
                H3 [Class "centered"] -< [Text "Questions"]
                Div [
                    Id "fsharpQuestions"
                    Attributes.HTML5.Data "questions-count" "0"
                    Attributes.HTML5.Data "question-id" ""
                ] -< [new FSharpQuestions.FsharpQuestionsViewer ()]
            ]
            Div [Class "span4"] -< [
                H3 [Class "centered"] -< [Text "Snippets"]
                Div [
                    Id "fsharpSnippets"
                    Attributes.HTML5.Data "snippets-count" "0"
                ] -< [new FSharpSnippets.FsharpSnippetsViewer ()]
            ]
        ]

module BooksPageContent =

    let title = ""
    let metaDescription = ""

    let navigation =
            Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    UL [Class "nav"] -< [
                        LI [A [HRef "/"] -< [Text "Home"]]
                        LI [Class "active"] -< [A [HRef "/Books"] -< [Text "Books"]]
                        LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                        LI [A [HRef "/Resources"] -< [Text "Resources"]]
                    ]
                ]
                Div [Class "alert alert-info"; Id "alertDiv"] -< [
                    P [Class "centered"; Id "alertText"] -< [Text ""]
                ]
            ]

    let header =
        Utilities.Header [
            H1 [Text "FSharp Books"]
            P [Class "lead"] -< [
                Text "Learn F# or dive into advanced topics by reading books by experts
                    from Microsoft and the language community."
            ]
        ]

module VideosPageContent =

    let title pageId = sprintf "FSharp Videos - Page %d" pageId
    let metaDescription = ""

    let navigation =
            Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    UL [Class "nav"] -< [
                        LI [A [HRef "/"] -< [Text "Home"]]
                        LI [A [HRef "/Books"] -< [Text "Books"]]
                        LI [Class "active"] -< [A [HRef "/Videos/1"] -< [Text "Videos"]]
                        LI [A [HRef "/Resources"] -< [Text "Resources"]]
                    ]
                ]
                Div [Class "alert alert-info"; Id "alertDiv"] -< [
                    P [Class "centered"; Id "alertText"] -< [Text ""]
                ]
            ]

    let header =
        Utilities.Header [
            H1 [Text "FSharp Videos"]
            P [Class "lead"] -< [Text "F# videos"]
        ]

module ResourcesPageContent =

    let title = "FSharp Resources"
    let metaDescription = ""

    let navigation =
            Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    UL [Class "nav"] -< [
                        LI [A [HRef "/"] -< [Text "Home"]]
                        LI [A [HRef "/Books"] -< [Text "Books"]]
                        LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                        LI [Class "active"] -< [A [HRef "/Resources"] -< [Text "Resources"]]
                    ]
                ]
                Div [Class "alert alert-info"; Id "alertDiv"] -< [
                    P [Class "centered"; Id "alertText"] -< [Text ""]
                ]
            ]

    let header =
        Utilities.Header [
            H1 [Text "FSharp Resources"]
            P [Class "lead"] -< [Text "F# Resources"]
        ]
       
    let tabs =
        Div [Class "tabbable tabs-left"] -< [
            UL [Class "nav nav-tabs"] -< [
                LI [Class "active"] -< [A [HRef "#downloads"; HTML5.Data "toggle" "tab"] -< [Text "Downloads"]]
                LI [A [HRef "#mailinglists"; HTML5.Data "toggle" "tab"] -< [Text "Mailing Lists"]]
                LI [A [HRef "#codesamples"; HTML5.Data "toggle" "tab"] -< [Text "Code Samples"]]
            ]
            Div [Class "tab-content"] -< [
                Div [Class "tab-pane active"; Id "downloads"] -< [P [Text "downloads"]]
                Div [Class "tab-pane"; Id "mailinglists"] -< [P [Text "mailing lists"]]
                Div [Class "tab-pane"; Id "codesamples"] -< [P [Text "code samples"]]
            ]
        ]

module Views =

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
                    LI ["Home" => ctx.Link Home]
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

module Controller =
    
    open Views

    let controller =

        let handle = function
            | Home          -> homeView
            | Books         -> booksView
            | Custom404     -> custom404View
            | Videos pageId -> videosView pageId
            | Resources     -> resourcesView

        { Handle = handle }

module Site =

    open Controller

    let router : Router<Action> =
        Router.Table
            [
                Home, "/"
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
        member this.Actions = [Home]

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()

