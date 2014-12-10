module Sitelet.GitHubRepos

open IntelliFactory.WebSharper
open Octokit

type Repo =
    {
        ownerLink : string
        ownerAvatar : string
        name : string
        description : string
        link : string
        createdAt : string
        pushedAt : string
        stargazers : int
        forks : int
    }

    static member New (r:Repository) =     
        {
            ownerLink = r.Owner.HtmlUrl
            ownerAvatar = r.Owner.AvatarUrl
            name = r.FullName
            description =
                match r.Description with
                | null -> ""
                | d -> d
            link = r.HtmlUrl
            createdAt = r.CreatedAt.ToString()
            pushedAt = r.PushedAt.ToString()
            stargazers = r.StargazersCount
            forks = r.ForksCount
        }

module Server =

    open IntelliFactory.Html
    open Newtonsoft.Json
    open Octokit.Internal
    open System
    open System.Globalization
    open System.IO
    open System.Web

    module NewRepos =

        let request = SearchRepositoriesRequest("")
        request.Language <- Nullable Language.FSharp
        request.Fork <- ForkQualifier.ExcludeForks        
        request.Created <- DateRange.GreaterThanOrEquals (DateTime.Now.AddDays -7.)

        let repos() =
            async {
                try
                    let reposArray =
                        GitHub.client.Search.SearchRepo(request).Result.Items
                        |> Seq.toArray
                        |> Array.sortBy (fun x -> x.CreatedAt)
                        |> Array.rev
                        |> fun arr ->
                            match arr.Length > 50 with
                            | false -> arr
                            | true -> arr.[..49]
                        |> Array.map Repo.New
                    return Some reposArray
                with _ -> return None
            }

    module RecentlyUpdated =

        let request = SearchRepositoriesRequest("")
        request.Language <- Nullable Language.FSharp
        request.Updated <-  DateRange.GreaterThanOrEquals (DateTime.Now.AddDays -7.)

        let repos() =
            async {
                try
                    let reposArray =
                        GitHub.client.Search.SearchRepo(request).Result.Items
                        |> Seq.toArray
                        |> Array.sortBy (fun x -> x.PushedAt.Value)
                        |> Array.rev
                        |> fun arr ->
                            match arr.Length > 50 with
                            | false -> arr
                            | true -> arr.[..49]
                        |> Array.map Repo.New
                    return Some reposArray
                with _ -> return None
            }

    let newRepoDiv repo =
        Div [Class "media"] -< [
            A [Class "media-left"; HRef repo.ownerLink; Target "_blank"] -< [
                Img [
                    HTML5.Data "original" repo.ownerAvatar
                    Class "avatar lazy"
                ]
            ]
            Div [Class "media-body"] -< [
                yield H4 [Class "media-heading"; Style "word-break: break-word;"] -< [
                    A [HRef repo.link; Target "_blank"] -< [Text repo.name]                                        
                ]
                yield P [Text repo.createdAt]
                match repo.description with
                | "" -> ()
                | d -> yield P [Text d]
            ]               
        ]
        
    let newReposDiv() =
        Utils.dataDiv<Repo>
            "~/JSON/NewGitHubRepos.json"
            newRepoDiv

    let fetchNewRepos jsonPath =
        async {
            let! reposArray = NewRepos.repos()
            match reposArray with
            | None -> ()
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText(jsonPath, json)
        } |> Async.RunSynchronously

    let updatedRepoDiv repo =
        Div [Class "media"] -< [
            A [Class "media-left"; HRef repo.ownerLink; Target "_blank"] -< [
                Img [
                    HTML5.Data "original" repo.ownerAvatar
                    Class "avatar lazy"
                ]
            ]
            Div [Class "media-body"] -< [
                yield H4 [Class "media-heading"; Style "word-break: break-word;"] -< [
                    A [HRef repo.link; Target "_blank"] -< [Text repo.name]                                        
                ]
                yield P [Text repo.pushedAt]
                match repo.description with
                | "" -> ()
                | d -> yield P [Text d]
                yield P [
                    Text "Stargazers: "
                    Span [Class "badge"]
                    -< [Text (string repo.stargazers)]
                    Text " Forks: "
                    Span [Class "badge"]
                    -< [Text (string repo.forks)]
                ]
            ]               
        ]

    let updatedReposDiv() =
        Utils.dataDiv<Repo>
            "~/JSON/UpdatedGitHubRepos.json"
            updatedRepoDiv

    let fetchUpdatedRepos jsonPath =
        async {
            let! reposArray = RecentlyUpdated.repos()
            match reposArray with
            | None -> ()
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText(jsonPath, json)
        } |> Async.RunSynchronously