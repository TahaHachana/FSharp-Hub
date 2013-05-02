namespace Website

open IntelliFactory.WebSharper

module Snippets =

    module Server =
        open Mongo

        let snipData (snippet : Snippet) = snippet.Link, snippet.Title, snippet.Description

        let snipData'() =
            Snippets.latest20()
            |> Seq.toArray
            |> Array.map snipData
            |> Some

        [<Rpc>]
        let latest() =
            async {
                let dataOption = try snipData'() with _ -> None
                return dataOption
            }

    [<JavaScript>]
    module Client =
        open IntelliFactory.WebSharper.Html
        open Utilities.Client

        let li (link, title, description) =
            LI [Attr.Class "snippet"] -< [
                A [HRef link] -< [Strong [Text title]]
                P [Text description]
            ]

        let dispalySnippets arr (elt : Element) =
            let ul = UL [Id "snippets-list"]
            arr
            |> Array.map li
            |> Array.iter ul.Append
            do elt.Append ul

        let main() =
            Div [Id "fsharp-snippets"]
            |>! OnAfterRender(fun elt ->
                async {
                    let! snippets = Server.latest()
                    match snippets with
                        | None     -> do ()
                        | Some arr -> do dispalySnippets arr elt
                } |> Async.Start)

    type Control() =
        inherit Web.Control()

        [<JavaScript>]
        override this.Body = Client.main() :> _