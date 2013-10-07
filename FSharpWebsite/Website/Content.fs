module Website.Content

open System
open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets
open IntelliFactory.WebSharper.Sitelets.Content
open Model

module Shared =

    let random url =
        let dateStr = Uri.EscapeUriString(DateTime.Now.ToString())
        url + "?d=" + dateStr     
        
    let loginInfo (ctx: Context<_>) =
        let userOption = UserSession.GetLoggedInUser ()
        let link =
            match userOption with
                | Some user ->
                    Utils.link
                        (random <| ctx.Link Action.Logout)
                        <| "Sign out (" + user + ")"                    
                | None ->
                    A [
                        Class "navbar-right btn btn-default navbar-btn"
                        HRef ("/login")
                    ] -< [Text "Sign in"]                    
        Div [Class "pull-right"] -< [link]

    let ga = Script [Src "/Scripts/ga.js"]

module Home =
    let title = "FSharp Programming Language"

    let metaDesc = "Latest tweets, snippets and questions about the F# programming language."

    let definition : HtmlElement =
            P [Id "lead"] -< [
                Strong [Text "FSharp"]
                Text " is an advanced, multi-paradigm, strongly typed open source programming language.
                    F# allows you to solve complex problems with simple, accurate and maintainable code
                    and to be more productive thanks to features like functions as values, type inference, 
                    pattern matching and computation expressions. It's a general purpose language that you can
                    use to build desktop, web and mobile applications and to perform cloud computations."
            ]

    let jumbotron : HtmlElement =
        Div [Class "jumbotron"; Id "jumbotron"] -< [
            Div [Class "text-center"] -< [
                H1 [Text "FSharp Language"]
                definition
                P [
                    A [
                        Class "btn btn-success btn-lg"
                        HRef "http://www.tryfsharp.org/"
                        Target "_blank"
                    ] -< [Text "Try F#"] 
                ]
            ]
        ]

    let widgetsRow : HtmlElement =
        Div [Class "row"] -< [
            Div [Class "col-lg-4"] -< [
                H3 [Text "Tweets"] :> INode<_>
                new Tweets.Control() :> _
            ]
            Div [Class "col-lg-4"] -< [
                H3 [Text "Questions"] :> INode<_>
                new Questions.Control() :> _
            ]
            Div [Class "col-lg-4"] -< [
                H3 [Text "Snippets"] :> INode<_>
                new Snippets.Control() :> _
            ]
        ]

    let body =
        Div [Id "wrap"] -< [
            Nav.navElt <| Some "Home"
            Div [Class "container"; Id "main"] -< [
                jumbotron :> INode<_>
                AddThis.elt :> _
                widgetsRow :> _
            ]
            Div [Id "push"]
            Shared.ga
        ]

module Books =
    let title = "FSharp Books"

    let metaDesc = "Books about FSharp published by Apress, O'Reilly, PACKT and Manning."

    let header : HtmlElement =
        Div [Class "page-header"] -< [
            H1 [Text "FSharp Books"]
            P [Text "Learn F# and explore advanced topics by reading books written by experts from Microsoft and the language community."]
        ]

    let body =
        Div [Id "wrap"] -< [
            Nav.navElt <| Some "Books"
            Div [Class "container"; Id "main"] -< [
                yield header
                yield! Books.main()
            ]
            Div [Id "push"]
            Shared.ga
        ]

module Videos =
    let title pageId = sprintf "FSharp Videos - Page %d" pageId
    
    let metaDesc = "F# videos available on YouTube, Vimeo, SkillsMatter..."

    let header : HtmlElement =
        Div [Class "page-header"] -< [
            H1 [Text "FSharp Videos"]
            P [Text "Watch F# presentations, tutorials, podcasts and short videos."]
        ]
        
    let nav pageId =
        match pageId with
        | 1 -> Nav.navElt <| Some "Videos"
        | _ -> Nav.navElt None

    let body pageId =
        let videos = Mongo.Videos.all()
        let divs = Videos.main videos <| (pageId - 1) * 15
        Div [Id "wrap"] -< [
            nav pageId
            Div [Class "container"; Id "main"] -< [
                yield header
                yield! divs
                yield Videos.paginationDiv videos pageId
            ]
            Div [Id "push"]
            Shared.ga
        ]

module Error =
    let body =
        Div [Id "wrap"] -< [
            Nav.navElt None
            Div [Class "container"; Id "main"] -< [
                Div [Class "page-header"] -< [
                    H1 [Text "The requested URL doesn't exist."]
                ]
            ]
            Div [Id "push"]
            Shared.ga
        ]

module Login =
    let body action action' (ctx:Context<_>) =
            let link =
                match action with
                | Some action -> action
                | None -> action'
                |> ctx.Link
            Div [Id "wrap"] -< [
                Nav.navElt None
                Div [Class "container"; Id "main"] -< [
                    Div [new Login.Control(link)]
                ]
                Div [Id "push"]
                Shared.ga
            ]

module Admin =
    let btns =
        Div [Class "row"] -< [
            Div [Class "col-lg-4"] -< [
                A [Class "btn btn-default btn-lg"; HRef "/admin/books"]
                -< [Text "Books"]]
            Div [Class "col-lg-4"] -< [
                A [Class "btn btn-default btn-lg"; HRef "/admin/videos"]
                -< [Text "Videos"]]
        ]

    let body ctx =
        Div [Id "wrap"] -< [
            Nav.navElt None
            Div [Class "container"; Id "main"] -< [
                Div [Class "row"] -< [Shared.loginInfo ctx]
                btns
            ]
            Div [Id "push"]
            Shared.ga
        ]

module BooksAdmin =
    let body ctx =
        Div [Id "wrap"] -< [
            Nav.navElt None 
            Div [Class "container"; Id "main"] -< [
                Div [Class "row"] -< [Shared.loginInfo ctx]
                Div [new BooksAdmin.Control()]                  
            ]
            Div [Id "push"]
            Shared.ga
        ]

module VideosAdmin =
    let body ctx =
        Div [Id "wrap"] -< [
            Nav.navElt None 
            Div [Class "container"; Id "main"] -< [
                Div [Class "row"] -< [Shared.loginInfo ctx]
                H1 [Text "Add new video"]
                Div [new VideosAdmin.Control()]               
            ]
            Div [Id "push"]
            Shared.ga
        ]