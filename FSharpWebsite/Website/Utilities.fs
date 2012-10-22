namespace FSharpWebsite

open System.Text.RegularExpressions
open IntelliFactory.WebSharper
open IntelliFactory.Html
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

module Utilities =

    let inline compileRegex pattern = Regex(pattern, RegexOptions.Compiled)

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

