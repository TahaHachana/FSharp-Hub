module Website.GitHubRepos

open IntelliFactory.WebSharper
//open FSharpHub.Data

type Repo =
    {
        ownerLink : string
        ownerAvatar : string
        name : string
        description : string
        link : string
        createdAt : string
        pushedAt : string
    }

module Server =

    open System.IO
    open Newtonsoft.Json
    open Octokit
    open Octokit.Internal
    open System
    open System.Globalization
    open System.Web
    open IntelliFactory.Html

    let newRepo (r:Repository) =
        
        {
            ownerLink = r.Owner.HtmlUrl
            ownerAvatar = r.Owner.AvatarUrl
            name = r.FullName
            // TODO replace with string option
            description = match r.Description with null -> "" | x -> x
            link = r.HtmlUrl
            createdAt = r.CreatedAt.ToString()
            pushedAt = r.PushedAt.ToString()
        }

    module NewRepos =

        let request = SearchRepositoriesRequest("")
        request.Language <- Nullable Language.FSharp
        request.Fork <- ForkQualifier.ExcludeForks
//        request.Created <- DateRange.LessThanOrEquals (DateTime.Now)
        
        request.Created <- DateRange.GreaterThanOrEquals (DateTime.Now.AddDays -7.)
//        request.PerPage <- 100
//        request.Page <- 1

        let repos() =
            async {
                try
                    let reposArray =
                        GitHub.client.Search.SearchRepo(request).Result.Items
                        |> Seq.toArray
                        |> Array.sortBy (fun x -> x.CreatedAt)
                        |> Array.rev
                        |> fun x -> x.[..49]
                        |> Array.map newRepo
                    return Some reposArray
                with _ -> return None
            }
//            |> Async.RunSynchronously

    module RecentlyUpdated =

        let request = SearchRepositoriesRequest("")
        request.Language <- Nullable Language.FSharp
        request.Updated <-  DateRange.GreaterThanOrEquals (DateTime.Now.AddDays -7.)
//        request.PerPage <- 100
//        request.Page <- 1

        let repos() =
            async {
                try
                    let reposArray =
                        GitHub.client.Search.SearchRepo(request).Result.Items
                        |> Seq.toArray
                        |> Array.sortBy (fun x -> x.PushedAt.Value)
                        |> Array.rev
                        |> fun x -> x.[..49]
                        |> Array.map newRepo
                    return Some reposArray
                with _ -> return None
            }
//            |> Async.RunSynchronously

    
    let newReposDiv() =
        let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/NewGitHubRepos.json"
        let repos =
            let json = File.ReadAllText jsonPath
            JsonConvert.DeserializeObject(json, typeof<Repo []>)
            :?> Repo []
        let rows =
            repos
            |> Array.mapi (fun idx repo ->
                let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                Div [Class cls] -< [
                    Div [Class "media"] -< [
                        A [Class "media-left"; HRef repo.ownerLink; Target "_blank"] -< [
                            Img [Style "width: 30px; height: 30px;"; Src repo.ownerAvatar]
                        ]
                        Div [Class "media-body"] -< [
                            H4 [Class "media-heading"; Style "word-break: break-word;"] -< [
                                A [HRef repo.link; Target "_blank"] -< [Text repo.name]                                        
                            ]
                            P [Text repo.createdAt]
                            // TODO remove this tag if the repo description is ""
                            P [Text repo.description]
                        ]               
                    ]
                ]
            )
            |> Utils.split 2
            |> Seq.map (fun x ->
                Div [Class "row data-row"]
                -< x)
            |> fun x -> Div [] -< x
        rows

    let fetchNewRepos() =
        async {
            let! reposArray = NewRepos.repos()
            let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/NewGitHubRepos.json"
            match reposArray with
            | None -> ()
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText(jsonPath, json)
        }

    let updatedReposDiv() =
        let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/UpdatedGitHubRepos.json"
        let repos =
            let json = File.ReadAllText jsonPath
            JsonConvert.DeserializeObject(json, typeof<Repo []>)
            :?> Repo []
        let rows =
            repos
            |> Array.mapi (fun idx repo ->
                let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                Div [Class cls] -< [
                    Div [Class "media"] -< [
                        A [Class "media-left"; HRef repo.ownerLink; Target "_blank"] -< [
                            Img [Style "width: 30px; height: 30px;"; Src repo.ownerAvatar]
                        ]
                        Div [Class "media-body"] -< [
                            H4 [Class "media-heading"; Style "word-break: break-word;"] -< [
                                A [HRef repo.link; Target "_blank"] -< [Text repo.name]                                        
                            ]
                            P [Text repo.pushedAt]
                            // TODO remove this tag if the repo description is ""
                            P [Text repo.description]                            
                        ]               
                    ]
                ]
            )
            |> Utils.split 2
            |> Seq.map (fun x ->
                Div [Class "row data-row"]
                -< x)
            |> fun x -> Div [] -< x
        rows

    let fetchUpdatedRepos() =
        async {
            let! reposArray = RecentlyUpdated.repos()
            let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/UpdatedGitHubRepos.json"
            match reposArray with
            | None -> ()
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText(jsonPath, json)
        }



    
    // TODO  remove this 
    [<Remote>]
    let newRepos() =
        async {
            let! reposArray = NewRepos.repos()
            let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/NewGitHubRepos.json"
            match reposArray with
            | None ->
                let repos =
                    let json = File.ReadAllText jsonPath
                    JsonConvert.DeserializeObject(json, typeof<Repo []>)
                    :?> Repo []
                return repos
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText(jsonPath, json)
                return repos
        }

    [<Remote>]
    let updatedRepos() =
        async {
            let! reposArray = RecentlyUpdated.repos()
            let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/UpdatedGitHubRepos.json"
            match reposArray with
            | None ->
                let repos =
                    let json = File.ReadAllText jsonPath
                    JsonConvert.DeserializeObject(json, typeof<Repo []>)
                    :?> Repo []
                return repos
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText(jsonPath, json)
                return repos
        }

/// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    let hideProress() =
        match JQuery.Of("[data-status=\"loading\"]").Length with
        | 0 ->
            JQuery.Of("#progress-bar").SlideUp().Ignore
            JQuery.Of("[data-spy=\"scroll\"]").Each(
                fun x -> JQuery.Of(x)?scrollspy("refresh")
            ).Ignore
        | _ ->
            JQuery.Of("[data-spy=\"scroll\"]").Each(
                fun x -> JQuery.Of(x)?scrollspy("refresh")
            ).Ignore

    let newRepos() =
        Div [HTML5.Attr.Data "status" "loading"]
        |>! OnAfterRender (fun elt ->
            async {
                let! repos = Server.newRepos()
                repos |> Array.mapi (fun idx repo ->
                    let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                    Div [Attr.Class cls] -< [
                        Div [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef repo.ownerLink; Attr.Target "_blank"] -< [
                                Img [Attr.Style "width: 30px; height: 30px;"; Attr.Src repo.ownerAvatar]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"; Attr.Style "word-break: break-word;"] -< [
                                    A [Attr.HRef repo.link; Attr.Target "_blank"; Text repo.name]                                        
                                ]
                                P [Text repo.createdAt]
                                // TODO remove this tag if the repo description is ""
                                P [Text repo.description]
                            ]               
                        ]
                    ]
                )
                |> Utils.split 2
                |> Seq.iter (fun x ->
                    Div [Attr.Class "row data-row"]
                    -< x
                    |> elt.Append
                )
                elt.RemoveAttribute "data-status"
                hideProress()
            }
            |> Async.Start)

    let updatedRepos() =
        Div [HTML5.Attr.Data "status" "loading"]
        |>! OnAfterRender (fun elt ->
            async {
                let! repos = Server.updatedRepos()
                repos |> Array.mapi (fun idx repo ->
                    let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                    Div [Attr.Class cls] -< [
                        Div [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef repo.ownerLink; Attr.Target "_blank"] -< [
                                Img [Attr.Style "width: 30px; height: 30px;"; Attr.Src repo.ownerAvatar]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"; Attr.Style "word-break: break-word;"] -< [
                                    A [Attr.HRef repo.link; Attr.Target "_blank"; Text repo.name]                                        
                                ]
                                P [Text repo.pushedAt]
                                // TODO remove this tag if the repo description is ""
                                P [Text repo.description]                            
                            ]               
                        ]
                    ]
                )
                |> Utils.split 2
                |> Seq.iter (fun x ->
                    Div [Attr.Class "row data-row"]
                    -< x
                    |> elt.Append
                )
                elt.RemoveAttribute "data-status"
                hideProress()
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
