module Website.GitHubRepos

open IntelliFactory.WebSharper
open FSharpHub.Data

module private Server =

    open System.IO
    open Newtonsoft.Json

    [<Remote>]
    let newRepos() =
        async {
            let! reposArray = GitHubRepos.New.repos()
            match reposArray with
            | None ->
                let repos =
                    let json = File.ReadAllText("~/JSON/NewGitHubRepos.json")
                    JsonConvert.DeserializeObject(json, typeof<GitHubRepos.Repo []>)
                    :?> GitHubRepos.Repo []
                return repos
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText("~/JSON/NewGitHubRepos.json", json)
                return repos
        }

    [<Remote>]
    let updatedRepos() =
        async {
            let! reposArray = GitHubRepos.RecentlyUpdated.repos()
            match reposArray with
            | None ->
                let repos =
                    let json = File.ReadAllText("~/JSON/RecentlyUpdatedGitHubRepos.json")
                    JsonConvert.DeserializeObject(json, typeof<GitHubRepos.Repo []>)
                    :?> GitHubRepos.Repo []
                return repos
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText("~/JSON/RecentlyUpdatedGitHubRepos.json", json)
                return repos
        }

/// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    let newRepos() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender (fun elt ->
            async {
                let! repos = Server.newRepos()
                let ul = UL [Attr.Class "media-list"; Attr.Id "tweets-ul"]
                repos |> Array.iter (fun repo ->
                    let li =
                        LI [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef repo.ownerLink; Attr.Target "_blank"] -< [
                                Img [Attr.Style "width: 30px; height: 30px;"; Attr.Src repo.ownerAvatar]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"] -< [
                                    A [Attr.HRef repo.link; Attr.Target "_blank"; Text repo.name]                                        
                                ]
                                P [Text repo.createdAt]
                            ]               
                        ]
                    ul.Append li)
                elt.Append ul
            }
            |> Async.Start)

    let updatedRepos() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender (fun elt ->
            async {
                let! repos = Server.updatedRepos()
                let ul = UL [Attr.Class "media-list"; Attr.Id "tweets-ul"]
                repos |> Array.iter (fun repo ->
                    let li =
                        LI [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef repo.ownerLink; Attr.Target "_blank"] -< [
                                Img [Attr.Style "width: 30px; height: 30px;"; Attr.Src repo.ownerAvatar]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"] -< [
                                    A [Attr.HRef repo.link; Attr.Target "_blank"; Text repo.name]                                        
                                ]
                                P [Text repo.pushedAt]
                            ]               
                        ]
                    ul.Append li)
                elt.Append ul
            }
            |> Async.Start)

type NewReposControl() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.newRepos() :> _

type UpdatedReposControl() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.updatedRepos() :> _
