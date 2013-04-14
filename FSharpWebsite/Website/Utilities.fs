namespace Website

open System.Text.RegularExpressions
open IntelliFactory.WebSharper

module Utilities =

    module Server =

        open IntelliFactory.Html

        let compileRegex pattern = Regex(pattern, RegexOptions.Compiled)

        /// Returns a sequence that yields chunks of length n. Each chunk is returned as a list.
        let rec toChunks count (source: seq<'T>) =           
            let rec loop source =
                seq {
                    yield Seq.truncate count source
                    let len = Seq.length source
                    match Seq.length source < count with
                        | false ->
                            let source' = Seq.skip count source
                            yield! loop source'
                        | true -> ()
                }
            loop source

        let header h1 p =
            HTML5.Header [Class "page-header"] -< [
                H1 [Text h1]
                P [Text p]
            ]

        let li activeLiOption href txt =
            match activeLiOption with
                | None -> LI [A [HRef href] -< [Text txt]]
                | Some activeLi ->
                    if txt = activeLi then
                        LI [Class "active"] -< [A [HRef href] -< [Text txt]]
                    else
                        LI [A [HRef href] -< [Text txt]]

        let navigation activeLiOption =
            let li' = li activeLiOption
            Div [Class "navbar navbar-fixed-top navbar-inverse"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    Div [Class "container"] -< [
                        UL [Class "nav"] -< [
                            li' "/"          "Home"
                            li' "/books"     "Books"
                            li' "/videos/1"  "Videos"
                            li' "/ecosystem" "Ecosystem"
                            li' "/resources" "Resources"
                        ]
                    ]
                ]
            ]

    module Client =
        
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

//        [<JavaScriptAttribute>]
//        let displayInfoAlert msg =
//            JQuery.Of("#alertText").Text(msg).Ignore
//            JQuery.Of("#alertDiv").Show(600).Ignore
//            JQuery.Of("#alertDiv").FadeOut(7000.).Ignore

        [<JavaScriptAttribute>]
        let incrementDataCount (selector : string) dataAttribute n =
            let jquery = JQuery.Of selector
            let count =
                jquery.Attr dataAttribute
                |> fun x -> int x + n
                |> string
            jquery.Attr(dataAttribute, count).Ignore

        [<JavaScriptAttribute>]
        let setAttributeValue (selector : string) (attribute : string) value =
            JQuery.Of(selector).Attr(attribute, value).Ignore

        [<JavaScriptAttribute>]
        let prependElement (selector : string) (element : Element) =
            JQuery.Of(selector).Prepend(element.Dom).Ignore