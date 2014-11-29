module Website.NuGet

open IntelliFactory.WebSharper

type Package =
    {
        id : string
        version : string
        iconUrl : string
        projectUrl : string
        galleryDetailsUrl : string
        lastUpdated : string
        downloadCount : int
        tags : string []
    }

    static member New (pkg:NuGet.Main.NuGet.ServiceTypes.V2FeedPackage) =
        let iconUrl =
            match pkg.IconUrl with
            | null -> "https://nuget.org/Content/Images/packageDefaultIcon.png"
            | x -> x
        {
            id = pkg.Id
            version = pkg.Version
            iconUrl = iconUrl
            projectUrl = pkg.ProjectUrl
            galleryDetailsUrl = pkg.GalleryDetailsUrl
            lastUpdated = pkg.LastUpdated.ToString()
            downloadCount = pkg.DownloadCount
            tags = pkg.Tags.Split ' '
        }


module private Server =

    open System
    open System.IO
    open System.Web
    open Newtonsoft.Json

//    open Microsoft.FSharp.Data.TypeProviders
//
//    type NuGet = ODataService<"http://www.nuget.org/api/v2">
//
//    let context = NuGet.GetDataContext()

    let context = NuGet.Main.context

    let ``f#Pkgs``() =
        async {
            try
                let pkgs =
                    query {
                        for pkg in context.Packages do
                            sortByDescending pkg.Published
                            where (pkg.Tags.Contains("F#"))
                            where (pkg.Published > (DateTime.Today.AddDays -7.))
                            take 50
            //                        select pkg
                        }
                    |> Seq.toArray
                    |> Array.map Package.New
                return Some pkgs
            with _ -> return None
        }

    [<Remote>]
    let pkgs() =
        async {
            let! pkgsArray = ``f#Pkgs``()
            let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/NuGet.json"
            match pkgsArray with
            | None ->
                let tweets =
                    let json = File.ReadAllText jsonPath
                    JsonConvert.DeserializeObject(json, typeof<Package []>)
                    :?> Package []
                return tweets
            | Some pkgs ->
                let json = JsonConvert.SerializeObject(pkgs)
                File.WriteAllText(jsonPath, json)
                return pkgs
        }

// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    // TODO refactor this
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

    let main() =
        Div [HTML5.Attr.Data "status" "loading"]
        |>! OnAfterRender (fun elt ->
            async {
                let! pkgs = Server.pkgs()
                pkgs
                |> Array.mapi (fun idx pkg ->
                    let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                    Div [Attr.Class cls] -< [
                        Div [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef pkg.projectUrl; Attr.Target "_blank"] -< [
                                Img [Attr.Src pkg.iconUrl; Attr.Class "avatar"]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"] -< [
                                    A [Attr.HRef pkg.galleryDetailsUrl; Attr.Target "_blank"; Text (pkg.id + " " + pkg.version)]                                        
                                ]
                                P [Text ("Pushed on: " + pkg.lastUpdated)]
                                P [
                                    Text "Download Count: "
                                    Span [Attr.Class "badge"; Text (string pkg.downloadCount)] :> IPagelet
                                ]
                                Div [
                                    for x in pkg.tags -> Span [Attr.Class "label label-info"; Text x]
                                ]
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

/// A control for serving the main pagelet.
type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _

