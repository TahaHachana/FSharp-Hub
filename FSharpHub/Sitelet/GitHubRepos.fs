module Website.GitHubRepos

open IntelliFactory.WebSharper

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
        request.Created <- DateRange.GreaterThanOrEquals (DateTime.Now.AddDays -7.)

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
                        |> fun x -> x.[..49]
                        |> Array.map newRepo
                    return Some reposArray
                with _ -> return None
            }

    let newRepoDiv repo =
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
        
    let newReposDiv() =
        let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/NewGitHubRepos.json"
        Utils.dataDiv<Repo> jsonPath newRepoDiv

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

    let updatedReposDiv() =
        let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/UpdatedGitHubRepos.json"
        Utils.dataDiv<Repo> jsonPath updatedRepoDiv

    let fetchUpdatedRepos jsonPath =
        async {
            let! reposArray = RecentlyUpdated.repos()
            match reposArray with
            | None -> ()
            | Some repos ->
                let json = JsonConvert.SerializeObject repos
                File.WriteAllText(jsonPath, json)
        } |> Async.RunSynchronously