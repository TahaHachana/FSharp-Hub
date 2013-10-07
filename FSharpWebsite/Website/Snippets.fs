module Website.Snippets

open IntelliFactory.WebSharper

module Server =
    open Records
    open Mongo

    let snippetData (snippet:Snippet) =
        snippet.Link, snippet.Title, snippet.Description

    let latestSnippets() =
        Snippets.latest20()
        |> Seq.toArray
        |> Array.map snippetData
        |> Some

    [<Rpc>]
    let latest() =
        async {
            let dataOption = try latestSnippets() with _ -> None
            return dataOption
        }

[<JavaScript>]
module Client =
    open IntelliFactory.WebSharper.Html

    let li (link, title, description) =
        LI [Attr.Class "list-group-item"] -< [
            A [HRef link] -< [Strong [Text title]]
            P [Text description]
        ]

    let dispalySnippets arr (elt:Element) =
        let ul = UL [Attr.Class "list-group"]
        arr
        |> Array.map li
        |> Array.iter ul.Append
        elt.Append ul

    let main() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender(fun elt ->
            async {
                let! snippets = Server.latest()
                match snippets with
                | None -> ()
                | Some snippets -> dispalySnippets snippets elt
            } |> Async.Start)

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _