module Website.NuGet

do ()

//open IntelliFactory.WebSharper
//
//module private Server =
//
//    open System
////    open Microsoft.FSharp.Data.TypeProviders
////
////    type NuGet = ODataService<"http://www.nuget.org/api/v2">
////
////    let context = NuGet.GetDataContext()
//
////    let context = NuGet.Main.context
//
//    [<Remote>]
//    let pkgs() =
//        async {
//            return
//                query {
//                    for pkg in context.Packages do
//                        sortByDescending pkg.Published
//                        where (pkg.Tags.Contains("F#"))
//                        where (pkg.Published > (DateTime.Today.AddDays -1.))
//                        select pkg
//                 }
//                |> Seq.toList
//                |> List.map(fun pkg ->
//                    let iconUrl =
//                        match pkg.IconUrl with
//                        | null -> "https://nuget.org/Content/Images/packageDefaultIcon.png"
//                        | x -> x
//                    iconUrl, pkg.ProjectUrl, pkg.GalleryDetailsUrl, pkg.Id + " " + pkg.Version, pkg.LastUpdated.ToString(), pkg.DownloadCount.ToString(), pkg.Tags)
//        }
//
//// Client-side code.
//[<JavaScript>]
//module private Client =
//    open IntelliFactory.WebSharper.Html
//    open IntelliFactory.WebSharper.JQuery
//
//    let main() =
//        Div [Attr.Class "home-widget"]
//        |>! OnAfterRender (fun elt ->
//            async {
//                let! pkgs = Server.pkgs()
//                let ul = UL [Attr.Class "media-list"; Attr.Id "tweets-ul"]
//                pkgs |> List.iter (fun (iconUrl, project, url, idVersion, lastUpdated, downloads, tags) ->
//                    let li =
//                        LI [Attr.Class "media"] -< [
//                            A [Attr.Class "media-left"; Attr.HRef project; Attr.Target "_blank"] -< [
//                                Img [Attr.Src iconUrl]
//                            ]
//                            Div [Attr.Class "media-body"] -< [
//                                H4 [Attr.Class "media-heading"] -< [
//                                    A [Attr.HRef url; Attr.Target "_blank"; Text idVersion]                                        
//                                ]
//                                P [Text ("Published on " + lastUpdated)]
//                                P [Text ("Download: " + downloads)]
//                                P [Text ("Tags: " + tags)]
//                            ]               
//                        ]
//                    ul.Append li)
//                elt.Append ul
//            }
//            |> Async.Start)
//
///// A control for serving the main pagelet.
//type Control() =
//    inherit Web.Control()
//
//    [<JavaScript>]
//    override this.Body = Client.main() :> _
//
