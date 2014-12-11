module Sitelet.Content

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets
open IntelliFactory.WebSharper.Sitelets.Content
open Model
open System

module Shared =

    let random url =
        let dateStr = Uri.EscapeUriString(DateTime.Now.ToString())
        url + "?d=" + dateStr     
        
    let loginInfo (ctx:Context<_>) =
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

module Home =
    let title = "FSharp Hub"

    let metaDesc = "Aggregator for the F# programming language community activity."

    let body() : HtmlElement =
        Div [
            Div [Id "twitter"; Class "anchor"]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "Twitter"] 
                ]
                Twitter.Server.tweetsDiv()
            ] 
            Div [
                Id "stackoverflow"
                Class "anchor"
            ]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "StackOverflow"]
                ]
                StackOverflow.Server.stackDiv()
            ]
            Div [Id "new-github-repos"; Class "anchor"]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "New GitHub Repos"]
                ]
                GitHubRepos.Server.newReposDiv()
            ]
            Div [
                Id "updated-github-repos"
                Class "anchor"
            ]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "Updated GitHub Repos"]
                ]
                GitHubRepos.Server.updatedReposDiv()
            ]
//            Div [Id "gists"; Class "anchor"] -< [
//                Div [Class "page-header"] -< [
//                    H2 [Text "GitHub Gists"]
//                ]
//                GitHubGists.Server.newGistsDiv()
//            ]
            Div [
                Id "nuget"
                Class "anchor"
            ]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "NuGet"]
                ]
                NuGet.Server.pkgsDiv()
            ]
            Div [
                Id "news"
                Class "anchor"
            ]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "News"]
                ] :> INode<_>
                new News.Control() :> _
            ]
            Div [
                Id "fpish"
                Class "anchor"
            ]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "FPish"]
                ] :> INode<_>
                new FPish.Control() :> _
            ]
            Div [
                Id "msdn"
                Class "anchor"
            ]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "MSDN"]
                ] :> INode<_>
                new Msdn.Control() :> _
            ]
            Div [
                Id "fssnip"
                Class "anchor"
            ]
            -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "F# Snippets"]
                ] :> INode<_>
                new FSSnip.Control() :> _
            ]
            Div [
                Id "google-group"
                Class "anchor"
            ] -< [
                Div [Class "page-header"]
                -< [
                    H2 [Text "Google Group"]
                ] :> INode<_>
                new GoogleGroup.Control() :> _
            ]
        ]

module Books =

    let title = "FSharp Books"

    let metaDesc = "Books about FSharp published by Apress, O'Reilly, PACKT, Manning..."

    let body : HtmlElement =
        Div [
            yield! Books.main()
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
        Div [
            nav pageId
            Div [Class "container"; Id "main"] -< [
                yield header
                yield! divs
                yield Videos.paginationDiv videos pageId
            ]
        ]

module Error =

    let body : HtmlElement =
        Div [Class "page-header"]
        -< [
            H1 [Text "The requested URL doesn't exist."]
        ]

module Login =
    let body action action' (ctx:Context<_>) =
        let link =
            match action with
            | Some action -> action
            | None -> action'
            |> ctx.Link
        Div [new Login.Control(link)]

module Admin =
    let btns : HtmlElement =
        Div [Class "row"] -< [
            Div [Class "col-lg-4"] -< [
                A [Class "btn btn-default btn-lg"; HRef "/admin/books"]
                -< [Text "Books"]]
            Div [Class "col-lg-4"] -< [
                A [Class "btn btn-default btn-lg"; HRef "/admin/videos"]
                -< [Text "Videos"]]
        ]

    let body ctx =
        Div [
            Div [Class "row"]
            -< [Shared.loginInfo ctx]
            btns
        ]

module BooksAdmin =
    let body ctx =
        Div [
            Div [Class "row"]
            -< [Shared.loginInfo ctx]
            Div [new BooksAdmin.Control()]                  
        ]

module VideosAdmin =
    let body ctx =
        Div [
            Div [Class "row"]
            -< [Shared.loginInfo ctx]
            H1 [Text "Add new video"]
            Div [new VideosAdmin.Control()]               
        ]


