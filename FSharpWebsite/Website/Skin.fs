module Website.Skin

open System.Web
open IntelliFactory.WebSharper.Sitelets

type Page =
    {
        Title    : string
        MetaDesc : string
        Body     : Content.HtmlElement
    }

    static member New title metaDesc makeBody ctx =
        {
            Title    = title
            MetaDesc = metaDesc
            Body     = makeBody ctx
        }

let loadFrequency =
    #if DEBUG
        Content.Template.PerRequest
    #else
        Content.Template.Once
    #endif

let template<'T> path =
    let path = HttpContext.Current.Server.MapPath path
    Content.Template<'T>(path, loadFrequency)
    
let pageTemplate path =
    (template<Page> path)
        .With("title"    , fun page -> page.Title)
        .With("meta-desc", fun page -> page.MetaDesc)
        .With("body"     , fun page -> page.Body)

let withTemplate<'T> template title metaDesc makeBody : Content<'T> =
    Content.WithTemplate
        template
        <| fun ctx -> Page.New title metaDesc makeBody ctx