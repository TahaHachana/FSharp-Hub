module Website.GitHubGists

open IntelliFactory.WebSharper
open FSharpHub.Data

module private Server =
    
    open System.IO
    open Newtonsoft.Json

    [<Remote>]
    let gists() =
        async {
            let! gistsArray = GitHubGists.latestGists()
            match gistsArray with
            | None ->
                let gists =
                    let json = File.ReadAllText("~/JSON/GitHubGists.json")
                    JsonConvert.DeserializeObject(json, typeof<GitHubGists.Gist []>)
                    :?> GitHubGists.Gist []
                return gists
            | Some gists ->
                let json = JsonConvert.SerializeObject gists
                File.WriteAllText("~/JSON/GitHubGists.json", json)
                return gists
        }

/// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    let main() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender (fun elt ->
            async {
                let! gists = Server.gists()
                let ul = UL [Attr.Class "media-list"]
                gists |> Array.iter (fun gist ->
                    let li =
                        LI [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef gist.ownerLink; Attr.Target "_blank"] -< [
                                Img [Attr.Src gist.ownerAvatar]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"] -< [
                                    A [Attr.HRef gist.link; Attr.Target "_blank"; Text ("Gist by " + gist.ownerName)]                                        
                                ]
                            ]               
                        ]
                    ul.Append li)
                elt.Append ul
            }
            |> Async.Start)

/// A control for serving the main pagelet.
type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _

