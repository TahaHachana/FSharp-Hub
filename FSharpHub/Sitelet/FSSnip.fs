module Website.FSSnip

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery
open System
open System.Net
open Newtonsoft.Json

type Snippet =
    {
        author: string
        description: string
        likes: int
        link: string
        published: string
        title: string        
    }

module Server =

    let fetchJson() =
        async {
            use client = new WebClient()
            let uri = Uri "http://api.fssnip.net/1/snippet"
            let! json = client.AsyncDownloadString uri
            return json
        }

    [<Remote>]
    let snippets() =
        async {
            let! json = fetchJson()
            let snippets =
                JsonConvert.DeserializeObject(json, typeof<Snippet []>)
                :?> Snippet []
            return snippets
        }

module Client =

    [<JavaScript>]
    let main() =
        Div [HTML5.Attr.Data "status" "loading"]
        |>! OnAfterRender (fun elt ->
            async {
                let! snippets = Server.snippets()
                let ul = UL [Attr.Class "list-group"]
                for x in snippets do
                    let li =
                        LI [Attr.Class "list-group-item"] -< [
                            H4 [Attr.Class "list-group-item-heading"] -< [
                                A [Attr.HRef x.link; Attr.Target "_blank"; Text x.title]
                            ]
                            P [Text x.description]
                            P [Text ("Published " + x.published + " by " + x.author)]
                        ]
                    ul.Append li
                elt.Append ul
                elt.RemoveAttribute "data-status"
                JsUtils.hideProress()
            } |> Async.Start)

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = Client.main() :> _