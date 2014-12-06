namespace NuGet

module Main =
    
    open System
    open Microsoft.FSharp.Data.TypeProviders

    type NuGet = ODataService<"http://www.nuget.org/api/v2">

    let context = NuGet.GetDataContext()

//    let pkgs() = //["", "", ""] //, "", "", "", ""]
//        query {
//            for pkg in context.Packages do
//                sortByDescending pkg.Published
//                where (pkg.Tags.Contains("F#"))
//                where (pkg.Published > (DateTime.Today.AddDays -1.))
//                select pkg
//            }
//        |> Seq.toList
//        |> List.map(fun pkg -> "", pkg.ProjectUrl, pkg.GalleryDetailsUrl, pkg.Id + pkg.Version, pkg.LastUpdated.ToString(), pkg.DownloadCount.ToString(), pkg.Tags)
