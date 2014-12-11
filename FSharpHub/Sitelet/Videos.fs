module Sitelet.Videos

open IntelliFactory.Html
open Records
     
let skip (videos:seq<Video>) n =
    videos
    |> Seq.skip n
    |> Seq.truncate 15
    |> Seq.map (fun x ->
        x.Url, x.Thumbnail, x.Title, x.Website)
    |> Utils.split 3
    |> List.map (fun x -> Seq.toList x)

let thumbnail (url, thumbnail, title, website) =
    Div [Class "col-lg-4"]
    -< [
        Div [Class "thumbnail"]
        -< [
            A [
                HRef url
                Target "_blank"
            ]
            -< [
                Img [
                    Class "thumb img-responsive"
                    Src thumbnail
                    Alt title
                ]
            ]
            H4 [Text title]
        ]
    ]

let rows divs =
    Div [Class "row videos-row"]
    -< divs

let prevLi pageId =
    match pageId with
    | 1 ->
        LI [Class "disabled"]
        -< [Utils.link "#" "«"]
    | _ -> LI [Utils.link ("/videos/" + string (pageId - 1)) "«"]

let nextLi pageId pagesLength =
    match pageId with
    | _ when pageId = pagesLength ->
        LI [Class "disabled"]
        -< [
            Utils.link "#" "»"
        ]
    | _ -> LI [Utils.link ("/videos/" + string (pageId + 1)) "»"]

let pageLi x pageId =
    let xStr = string x
    match x with
    | _ when x = pageId ->
        LI [Class "active"]
        -< [
            Utils.link ("/videos/" + xStr) xStr
        ]
    | _ -> LI [Utils.link ("/videos/" + xStr) xStr]

let pagesUl pageId pages length =
    UL [Class "pagination"]
    -< [
        yield prevLi pageId
        yield! Array.map (fun x -> pageLi x pageId) pages
        yield nextLi pageId length
    ]

let paginationDiv items pageId =
    let pages =
        float (Seq.length items) / 15.
        |> ceil |> int |> fun x -> [|1 .. x|]
    let length = pages.Length
    let pages' =
        match pageId with
        | _ when pageId < 7 ->
            pages
            |> Seq.truncate 10
            |> Seq.toArray
        | _ ->
            let tail =
                pages.[pageId ..]
                |> Seq.truncate 4
                |> Seq.toArray
            pages.[.. pageId - 1]
            |> Array.rev
            |> Seq.truncate (10 - tail.Length)
            |> Seq.toArray
            |> Array.rev
            |> fun x -> Array.append x tail
    match length with
        | 1 -> Div []
        | _ ->
            Div [Class "row"]
            -< [
                pagesUl pageId pages' length
            ]

let main videos n =
    skip videos n
    |> List.map (fun x ->
        List.map thumbnail x)
    |> List.map rows