namespace FSharpWebsite

open IntelliFactory.Html
open IntelliFactory.WebSharper
open Mongo

module FSharpVideos =
     
    module Server =

        let fsharpVideos =
            Videos.queryFsharpVideos ()
            |> Array.map (fun x ->
                x.Url, x.Thumbnail, x.Title, x.Website)
            |> Utilities.Server.toChunks 4
            |> Seq.toArray

        let pagesCount = float (Array.length fsharpVideos) / 4. |> ceil |> int |> string

        let inline makeThumbnailLi (url, thumbnail, title, website) =
            LI [Class "span3"] -< [
                Div [Class "thumbnail"] -< [
                    Img [Class "videoThumb"; Src thumbnail; Alt title; Height "120"]
                    H4 [Text title]
                    A [HRef url; Class "btn btn-block"; Target "_blank"] -< [Text <| "Watch on " + website]
                ]
            ]

        let inline makeVideosUl (lis : Element<_> list) =
            UL [Class "thumbnails"] -< lis

        let inline makeDiv (ul : Element<_>) = Div [Class "row-fluid"] -< [ul]

        let inline makeDiv' (lis : Element<_> list) = Div [] -< lis

        let divs () =
            fsharpVideos
            |> Array.map (fun x ->
                List.map makeThumbnailLi x)
            |> Array.map makeVideosUl
            |> Array.map makeDiv
            |> Utilities.Server.toChunks 5
            |> Seq.toArray
            |> Array.mapi (fun idx x -> idx + 1, x)
            |> Array.map (fun (page, x) -> page, makeDiv' x)
      
//        let inline divById id = divs () |> Array.find (fun (x, content) -> x = id) |> snd


    module Client =
        
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        [<JavaScriptAttribute>]
        let pager () =
            UL [Attr.Class "pager"] -< [
                LI [Id "previous"; Attr.Class "previous"] -< [A [Id "prevLink"; HRef <| ""] -< [Text "Prev"]]
                LI [Id "next"; Attr.Class "next"] -< [A [Id "nextLink"; HRef <| ""] -< [Text "Next"]]
            ] |>! OnAfterRender (fun _ ->
                    let jquery = JQuery.Of "#pager"
                    let previous = jquery.Attr "data-previous" |> int
                    let next = jquery.Attr "data-next" |> int
                    let pagesCount = jquery.Attr "data-pages-count" |> int |> fun x -> x + 1
                    match previous with
                        | 0 -> JQuery.Of("#previous").AddClass("disabled").Ignore
                        | _ -> JQuery.Of("#prevLink").Attr("href", ("/Videos/" + string previous)).Ignore
                    match next with
                        | x when x = pagesCount -> JQuery.Of("#next").AddClass("disabled").Ignore
                        | _ -> JQuery.Of("#nextLink").Attr("href", ("/Videos/" + string next)).Ignore)
            
    type PagerViewer () =
        inherit Web.Control ()

        [<JavaScriptAttribute>]
        override this.Body =
            Client.pager () :> _