module Sitelet.NuGet

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
        let projectUrl =
            match pkg.ProjectUrl with
            | null -> "#"
            | url -> url
        {
            id = pkg.Id
            version = pkg.Version
            iconUrl = iconUrl
            projectUrl = projectUrl
            galleryDetailsUrl = pkg.GalleryDetailsUrl
            lastUpdated = pkg.LastUpdated.ToString()
            downloadCount = pkg.DownloadCount
            tags = pkg.Tags.Split ' '
        }

module Server =

    open IntelliFactory.Html
    open Newtonsoft.Json
    open System
    open System.IO
    open System.Web

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
                        }
                    |> Seq.toArray
                    |> Array.map Package.New
                return Some pkgs
            with _ -> return None
        }

    let pkgDiv pkg =
        Div [Class "media"]
        -< [
            A [
                Class "media-left"
                HRef pkg.projectUrl
                Target "_blank"
            ]
            -< [
                Img [
                    HTML5.Data "original" pkg.iconUrl
                    Class "avatar lazy"
                ]
            ]
            Div [Class "media-body"]
            -< [
                H4 [Class "media-heading"]
                -< [
                    A [
                        HRef pkg.galleryDetailsUrl
                        Target "_blank"
                    ]
                    -< [Text (pkg.id + " " + pkg.version)]                                        
                ]
                P [Text ("Pushed on: " + pkg.lastUpdated)]
                P [
                    Text "Download Count: "
                    Span [Class "badge"]
                    -< [Text (string pkg.downloadCount)]
                ]
                Div [
                    for x in pkg.tags ->
                        Span [Class "label label-info"]
                        -< [Text x]
                ]
            ]               
        ]

    let pkgsDiv() =
        Utils.dataDiv<Package>
            "~/JSON/NuGet.json"
            pkgDiv

    let fetchPkgs jsonPath =
        async {
            let! pkgsArray = ``f#Pkgs``()
            match pkgsArray with
            | None -> ()
            | Some pkgs ->
                let json = JsonConvert.SerializeObject(pkgs)
                File.WriteAllText(jsonPath, json)
        } |> Async.RunSynchronously