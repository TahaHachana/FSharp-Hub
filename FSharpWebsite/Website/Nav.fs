module Website.Nav

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets

let navToggle =
    Button [
        Class "navbar-toggle"
        HTML5.Data "toggle" "collapse"
        HTML5.Data "target" ".navbar-ex1-collapse"
        Type "button"
    ] -< [
        Span [Class "sr-only"] -< [
            Text "Toggle navigation"
        ]
        Span [Class "icon-bar"]
        Span [Class "icon-bar"]
        Span [Class "icon-bar"]
    ]

let navHeader =
    Div [Class "navbar-header"] -< [
        navToggle
        A [Class "navbar-brand"; HRef "/"] -< [
            Text "F# Hub"
        ]
    ]

let li activeLi href txt =
    match activeLi with
    | Some li when txt = li ->
        LI [Class "active"] -< [
            Utils.link href txt
        ]
    | _ -> LI [Utils.link href txt]

let navDiv activeLi =
    Div [Class "collapse navbar-collapse navbar-ex1-collapse"] -< [
        UL [Class "nav navbar-nav"] -< [
            li activeLi "/" "Home"
            li activeLi "/books" "Books"
            li activeLi "/videos/1" "Videos"
        ]
    ]

let navElt activeLi : Content.HtmlElement =
    HTML5.Nav [
        Class "navbar navbar-default navbar-fixed-top"
        NewAttribute "role" "navigation"
    ] -< [
        Div [Class "container"] -< [
            navHeader
            navDiv activeLi
        ]
    ]