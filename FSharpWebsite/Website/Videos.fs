namespace Website

open IntelliFactory.Html
open IntelliFactory.WebSharper
open Mongo

module Videos =
     
    module Server =

        let videos =
            Videos.all()
            |> Seq.toArray
            |> Array.map (fun x ->
                x.Url, x.Thumbnail, x.Title, x.Website)
            |> Utilities.Server.toChunks 4
            |> Seq.toArray
            |> Array.map (fun x -> Seq.toList x)

        let pagesCount = float (Array.length videos) / 4. |> int |> string //|> ceil |> int |> string

        let makeThumbnailLi (url, thumbnail, title, website) =
            LI [Class "span3"] -< [
                Div [Class "thumbnail"; Style "border: none;"] -< [
                    A [HRef url] -< [Img [Class "videoThumb"; Src thumbnail; Alt title; Height "120"]]
                    H4 [Text title]
                ]
            ]

        let makeVideosUl (lis : Element<_> list) = UL [Class "thumbnails"] -< lis

        let makeDiv (ul : Element<_>) = Div [Class "row-fluid"] -< [ul]

        let makeDiv' (lis : Element<_> list) = Div [] -< lis

        let divs() =
            videos
            |> Array.map (fun x ->
                List.map makeThumbnailLi x)
            |> Array.map makeVideosUl
            |> Array.map makeDiv
            |> Utilities.Server.toChunks 5
            |> Seq.toArray
            |> Array.map (fun x -> Seq.toList x)
            |> Array.mapi (fun idx x -> idx + 1, x)
            |> Array.map (fun (page, x) -> page, makeDiv' x)
      
    module Client =
        
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        [<JavaScriptAttribute>]
        let pager() =
            UL [Attr.Class "pager"] -< [
                LI [Id "previous"; Attr.Class "previous"] -< [A [Id "prevLink"; HRef <| ""] -< [Text "Prev"]]
                LI [Id "next"; Attr.Class "next"] -< [A [Id "nextLink"; HRef <| ""] -< [Text "Next"]]
            ] |>! OnAfterRender (fun _ ->
                    let jquery = JQuery.Of "#pager"
                    let previous = jquery.Attr "data-previous" |> int
                    let next = jquery.Attr "data-next" |> int
                    let pagesCount = jquery.Attr "data-pages-count" |> int
                    match previous with
                        | 0 -> JQuery.Of("#previous").AddClass("disabled").Ignore
                        | _ -> JQuery.Of("#prevLink").Attr("href", ("/videos/" + string previous)).Ignore
                    match next with
                        | x when x = pagesCount -> JQuery.Of("#next").AddClass("disabled").Ignore
                        | _ -> JQuery.Of("#nextLink").Attr("href", ("/videos/" + string next)).Ignore)
            
        type PagerViewer() =

            inherit Web.Control()

            [<JavaScriptAttribute>]
            override this.Body = pager() :> _