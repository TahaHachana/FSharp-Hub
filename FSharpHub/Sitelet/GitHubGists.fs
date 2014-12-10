module Sitelet.GitHubGists

do ()

//open IntelliFactory.WebSharper
//
//type GitHubGist =
//    {
//        ownerAvatar : string
//        ownerName : string
//        ownerLink : string
//        link : string
//    }
//
//module Server =
//    
//    open System.IO
//    open Newtonsoft.Json
//    open Octokit
//    open Octokit.Internal
//    open System
//    open System.Configuration
//
//    let newGist (g:Octokit.Gist) =
//        {
//            ownerAvatar = g.Owner.AvatarUrl
//            ownerName = g.Owner.Name
//            ownerLink = g.Owner.HtmlUrl
//            link = g.HtmlUrl
//        }
//
//    let ``f#Gists``() =
//        async {
//            try
//                let gists =
//                    GitHub.client.Gist.GetAllPublic(DateTimeOffset.Now.AddDays -1.).Result
//                    |> Seq.filter (fun x ->
//                        x.Files
//                        |> Seq.map (fun file -> file.Value)
//                        |> Seq.exists (fun f -> f.Language = "F#")    
//                    )
//                    |> Seq.take 50
//                    |> Seq.toArray
//                    |> Array.map newGist
//                return Some gists
//            with _ -> return None
//        }
//
//    let fetchNewGists jsonPath =
//        async {
//            let! gistsArray = ``f#Gists``()
//            match gistsArray with
//            | None -> ()
//            | Some gists ->
//                let json = JsonConvert.SerializeObject gists
//                File.WriteAllText(jsonPath, json)
//        }
//        |> Async.RunSynchronously
//
//    open System.Web
//    open IntelliFactory.Html
//
//    let newGistsDiv() =
//        let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/Gists.json"
//        let repos = [||]
////            let json = File.ReadAllText jsonPath
////            JsonConvert.DeserializeObject(json, typeof<GitHubGist []>)
////            :?> GitHubGist []
//        let rows =
//            repos
//            |> Array.mapi (fun idx gist ->
//                let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
//                Div [Class cls] -< [
//                    Div [Class "media"] -< [
//                        A [Class "media-left"; HRef gist.ownerLink; Target "_blank"] -< [
//                            Img [Src gist.ownerAvatar]
//                        ]
//                        Div [Class "media-body"] -< [
//                            H4 [Class "media-heading"] -< [
//                                A [HRef gist.link; Target "_blank"] -< [Text ("Gist by " + gist.ownerName)]                                        
//                            ]
//                        ]               
//                    ]
//                ]
//            )
//            |> Utils.split 2
//            |> Seq.map (fun x ->
//                Div [Class "row data-row"]
//                -< x)
//            |> fun x -> Div [] -< x
//        rows