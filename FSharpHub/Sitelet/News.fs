module Website.News

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

type Response =
    {
        responseData : data
//        responseDetails : obj
//        responseStatus : obj
    }

and data =
    {
        feed : feedDetails
    }

and feedDetails =
    {
//        author : string
//        description : string
        entries : Entry []
//        feedUrl : string
//        link : string
//        title : string
//        ``type`` : string
    }

and Entry =
    {
        title : string
        link : string
//        author : string
//        publishedDate : obj
        contentSnippet : string
//        content : string
//        categories : string []
    }

// TODO move this to last home section control
//[<Remote>]
//let fetchNewData() =
//    async {
//        do! Twitter.Server.fetchNewTweets()
//        do! StackOverflow.Server.fetchNewQuestions()
//        do! GitHubRepos.Server.fetchNewRepos()
//        do! GitHubRepos.Server.fetchUpdatedRepos()
//        do! NuGet.Server.fetchPkgs()
//    }

[<JavaScript>]
let hideProress() =
    match JQuery.Of("[data-status=\"loading\"]").Length with
    | 0 ->
        JQuery.Of("#progress-bar").SlideUp().Ignore
//        JQuery.Of("[data-spy=\"scroll\"]").Each(
//            fun x -> JQuery.Of(x)?scrollspy("refresh")
//        ).Ignore
    | _ ->
        JQuery.Of("[data-spy=\"scroll\"]").Each(
            fun x -> JQuery.Of(x)?scrollspy("refresh")
        ).Ignore

[<JavaScript>]
let main() =
//    async {
//        do! fetchNewData()
//        JavaScript.Log "Fetched new data"
//    } |> Async.Start
    Div [HTML5.Attr.Data "status" "loading"]
    |>! OnAfterRender (fun elt ->
        let ul = UL [Attr.Id "news-list"; Attr.Class "list-group"]
        let url = "http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=http%3A%2F%2Ffpish.net%2Frss%2Fblogs%2Ftag%2F1%2Ff~23"
        JQuery.GetJSON(url, (fun (data, _) ->
            let data = As<Response> data
            let entries = data.responseData.feed.entries
            entries
    //        |> fun x -> x.[..4]
            |> Array.iter (fun x ->
                let li =
                    LI [Attr.Class "list-group-item"] -< [
                        H4 [Attr.Class "list-group-item-heading"] -< [
                            A [Attr.HRef x.link; Attr.Target "_blank"; Text x.title]
                        ]
                        P [Text x.contentSnippet ]
                    ]
                ul.Append li))) |> ignore
        elt.Append ul
        JavaScript.Log "Appended news list"
        elt.RemoveAttribute "data-status"
        hideProress())

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = main() :> _