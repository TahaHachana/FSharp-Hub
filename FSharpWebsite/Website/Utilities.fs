﻿namespace Website

open System.Text.RegularExpressions
open IntelliFactory.WebSharper

module Utilities =

    module Server =

        open IntelliFactory.Html

        let inline compileRegex pattern = Regex(pattern, RegexOptions.Compiled)

        /// Returns a sequence that yields chunks of length n. Each chunk is returned as a list.
        let inline toChunks length (source: seq<'T>) =
            use ie = source.GetEnumerator()
            let sourceIsEmpty = ref false
            let rec loop () =
                seq {
                    if ie.MoveNext () then
                        yield [
                                yield ie.Current
                                for x in 2 .. length do
                                    if ie.MoveNext() then
                                        yield ie.Current
                                    else
                                        sourceIsEmpty := true
                        ]
                        match !sourceIsEmpty with
                        | false -> yield! loop ()
                        | true  -> ()
                }
            loop ()

        let inline Header elements = IntelliFactory.Html.Html.NewElement("header") elements

        let inline makeHeader h1 p =
            Header [Class "page-header"] -< [
                H1 [Text h1]
                P [Text p]
            ]

        let makeLi activeLiOption href txt =
            match activeLiOption with
                | None -> LI [A [HRef href] -< [Text txt]]
                | Some activeLi ->
                    if txt = activeLi then
                        LI [Class "active"] -< [A [HRef href] -< [Text txt]]
                    else
                        LI [A [HRef href] -< [Text txt]]

        let makeNavigation activeLiOption =
            let makeLi' = makeLi activeLiOption
            Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                Div [Class "navbar-inner"] -< [
                    UL [Class "nav"] -< [
                        makeLi' "/"          "Home"
                        makeLi' "/Books"      "Books"
                        makeLi' "/Videos/1"  "Videos"
                        makeLi' "/Resources" "Resources"
                    ]
                ]
            ]

    module Client =
        
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        [<JavaScriptAttribute>]
        let displayInfoAlert msg =
            JQuery.Of("#alertText").Text(msg).Ignore
            JQuery.Of("#alertDiv").Show(600).Ignore
            JQuery.Of("#alertDiv").FadeOut(7000.).Ignore

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