namespace FSharpWebsite

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

type Action =
    | Home
    | Books
    | Custom404

module Skin =
    open System.Web

    let TemplateLoadFrequency =
        #if DEBUG
        Content.Template.PerRequest
        #else
        Content.Template.Once
        #endif

    type Page =
        {
            Title           : string
            MetaDescription : string
            Body            : list<Content.HtmlElement>
        }

    let MainTemplate =
        let path = HttpContext.Current.Server.MapPath("~/Main.html")
        Content.Template<Page>(path, TemplateLoadFrequency)
            .With("title", fun x -> x.Title)
            .With("metaDescription", fun x -> x.MetaDescription)
            .With("body", fun x -> x.Body)

    let WithTemplate title metaDescription body : Content<Action> =
        Content.WithTemplate MainTemplate <| fun context ->
            {
                Title           = title
                MetaDescription = metaDescription
                Body            = body context
            }

[<AutoOpenAttribute>]
module Helpers =

    let ( => ) text url =
        A [HRef url] -< [Text text]

module SharedContent =

    let navigation =
            Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    UL [Class "nav"] -< [
                        LI [Class "active"] -< [A [ HRef "/"] -< [Text "Home"]]
//                        LI [A [ HRef "#"] -< [Text "News"]]
                        LI [A [ HRef "/fsharp-books"] -< [Text "Books"]]
//                        LI [A [ HRef "#"] -< [Text "Videos"]]
//                        LI [A [ HRef "#"] -< [Text "Resources"]]
//                        LI [A [ HRef "#"] -< [Text "Community"]]
//                        LI [A [ HRef "#"] -< [Text "Links"]]
//                        LI [A [ HRef "#"] -< [Text "Contact"]]
                    ]
                ]
                Div [Class "alert alert-info"; Id "alertDiv"] -< [
                    P [Class "centered"; Id "alertText"] -< [Text ""]
                ]
            ]

    let forkme =
        A [HRef "https://github.com/TahaHachana/FSharpWebsite"; Target "_blank"] -< [
            Img [Src "https://s3.amazonaws.com/github/ribbons/forkme_right_darkblue_121621.png"; Alt "Fork me on GitHub"; Id "forkme"]
        ]
       
module HomeContent =
    
    let title = "FSharp Programming Language"

    let metaDescription = ""

    let definition =
            P [
                Strong [Text "FSharp"]
                Text " is an advanced, multi-paradigm, strongly typed programming language. F# allows you to solve complex problems with simple code."
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
            Div [Class "span6"] -< [
                H3 [Class "centered"] -< [Text "Tweets"]
                Div [Id "fsharpTweets"; Attributes.HTML5.Data "tweets-count" "0"; Attributes.HTML5.Data "tweet-id" ""] -< [new FSharpTweets.FsharpTweetsViewer ()]
            ]
            Div [Class "span6"] -< [
                H3 [Class "centered"] -< [Text "Questions"]
                Div [Id "fsharpQuestions"; Attributes.HTML5.Data "questions-count" "0"; Attributes.HTML5.Data "question-id" ""] -< [new FSharpQuestions.FsharpQuestionsViewer ()]
            ]
        ]

module BooksPageContent =

    let Header elements = IntelliFactory.Html.Html.NewElement("header") elements

    let header =
        Header [
            H1 [Text "FSharp Books"]
            P [Class "lead"] -< [Text "F# books"]
        ]

module Site =

    let HomePage =
        Skin.WithTemplate HomeContent.title HomeContent.metaDescription <| fun ctx ->
            [
                SharedContent.navigation
                SharedContent.forkme
                HomeContent.heroUnit
                Div [Class "container"] -< [
                    HomeContent.row1
                    HomeContent.row2
                    HomeContent.row3
                ]
            ]

    let Custom404Page =
        Skin.WithTemplate "" "" <| fun ctx ->
            [
                 Div [
                    P [Text "The page you're trying to access doesn't exist."]
                    LI ["Home" => ctx.Link Home]
                ]
            ]

    let BooksPage =
        Skin.WithTemplate HomeContent.title HomeContent.metaDescription <| fun ctx ->
            [
                SharedContent.navigation
                SharedContent.forkme
                Div [Class "container"] -< [
                    BooksPageContent.header
                    Div [new FSharpBooks.FsharpBooksViewer ()]
                ]
            ]

    let Main =
        Sitelet.Sum [
            Sitelet.Content "/" Home HomePage
            Sitelet.Content "/fsharp-books" Books BooksPage
            Sitelet.Content "/custom404" Custom404 Custom404Page
        ]

type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Site.Main
        member this.Actions = [Home; Custom404]

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()