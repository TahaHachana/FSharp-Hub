namespace FSharpWebsite

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

module Utilities =

    [<JavaScriptAttribute>]
    let displayInfoAlert msg =
        JQuery.Of("#alertText").Text(msg).Ignore
        JQuery.Of("#alertDiv").Show(600).Ignore
        JQuery.Of("#alertDiv").FadeOut(7000.).Ignore



