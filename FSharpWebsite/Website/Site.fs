namespace FSharpWebsite

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

type Action =
    | Home
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
        Div [Class "navbar navbar-fixed-top"] -< [
            Div [Class "navbar-inner"] -< [
                UL [Class "nav"] -< [
                    LI [Class "active"] -< [A [ HRef "#"] -< [Text "Home"]]
                    LI [A [ HRef "#"] -< [Text "News"]]
                    LI [A [ HRef "#"] -< [Text "Books"]]
                    LI [A [ HRef "#"] -< [Text "Resources"]]
                    LI [A [ HRef "#"] -< [Text "Community"]]
                    LI [A [ HRef "#"] -< [Text "Links"]]
                    LI [A [ HRef "#"] -< [Text "Contact"]]
                ]
            ]
        ]

module HomeContent =
    
    let title = ""

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

module Site =

    let HomePage =
        Skin.WithTemplate HomeContent.title HomeContent.metaDescription <| fun ctx ->
            [
                SharedContent.navigation
                HomeContent.heroUnit
            ]

    let Custom404Page =
        Skin.WithTemplate "" "" <| fun ctx ->
            [
                 Div [
                    P [Text "The page you're trying to access doesn't exist."]
                    LI ["Home" => ctx.Link Home]
                ]
            ]

    let Main =
        Sitelet.Sum [
            Sitelet.Content "/" Home HomePage
        ]

type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Site.Main
        member this.Actions = [Home]

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()