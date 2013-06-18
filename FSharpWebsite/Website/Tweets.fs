namespace Website

module Tweets =
    open IntelliFactory.WebSharper
    
    module Server =
        open TweetSharp
        open System
 
        let svc = TwitterService(Secure.consKey, Secure.consSecret)
        svc.AuthenticateWith(Secure.token, Secure.tokenSecret)

        let options = SearchOptions()
        options.Q <- "#fsharp"
        options.Count <- Nullable(100)

        [<Rpc>]
        let tweets() =
            async {
                let data =
                    svc.Search(options).Statuses
                    |> Seq.toList
                    |> List.map (fun x -> x.Author.ScreenName, x.Id.ToString(), x.Author.ProfileImageUrl, x.User.Name, x.TextAsHtml, x.CreatedDate.ToLongDateString())
                return data }

    [<JavaScript>]
    module Client =
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        let tweetLi screenName tweetId profileImage fullName tweetHtml creationDate =
            let profileLink  = "https://twitter.com/"                          + screenName
            let replyLink    = "https://twitter.com/intent/tweet?in_reply_to=" + tweetId
            let retweetLink  = "https://twitter.com/intent/retweet?tweet_id="  + tweetId
            let favoriteLink = "https://twitter.com/intent/favorite?tweet_id=" + tweetId
            let p = P []
            p.Html <- tweetHtml
            LI [Attr.Class "tweet"; Attr.Style "clear: both;"] -< [
                Div [
                    A [HRef profileLink; Attr.Class "twitterProfileLink"; Attr.Target "_blank"] -< [
                        Img [Src profileImage; Alt fullName; Attr.Class "avatar"; Height "48"; Width "48"]
                        Strong [Text fullName]
                    ] -< [Text (" @" + screenName)]
                    Br []
                    Small [Text creationDate]
                    p
                    Div [Attr.Class "tweetActions"; Attr.Style "visibility: hidden;"] -< [
                        A [HRef replyLink   ; Attr.Class "tweet-action-link"; Attr.Style "margin-right: 5px;"] -< [Text "Reply"]
                        A [HRef retweetLink ; Attr.Class "tweet-action-link"; Attr.Style "margin-right: 5px;"] -< [Text "Retweet"]
                        A [HRef favoriteLink; Attr.Class "tweet-action-link"]                                  -< [Text "Favorite"]
                    ]
                ]
            ]

        let toggleActionsVisibility() =
            let jquery = JQuery.Of ".tweet"
            jquery.Mouseenter(fun x _ -> JQuery.Of(".tweetActions", x).Css("visibility", "visible").Ignore).Ignore
            jquery.Mouseleave(fun x _ -> JQuery.Of(".tweetActions", x).Css("visibility", "hidden").Ignore).Ignore

        let handleTweetActions() =
            let jquery = JQuery.Of "a.tweet-action-link"
            jquery.Click(fun elt event ->
                do event.PreventDefault()
                let href = elt.GetAttribute "href"
                Html5.Window.Self.ShowModalDialog href |> ignore).Ignore

        let main() =
            Div [Id "fsharp-tweets"]
            |>! OnAfterRender (fun elt ->
                async {
                    let ul = UL [Id "tweets-list"]
                    let! tweets = Server.tweets()
                    tweets
                    |> List.iter (fun (screenName, tweetId, profileImage, fullName, tweetHtml, creationDate) ->
                        let li = tweetLi screenName tweetId profileImage fullName tweetHtml creationDate
                        do ul.Append li)
                    do elt.Append ul
                    do toggleActionsVisibility()
                    do handleTweetActions() }
                |> Async.Start)

    type Control() =
        inherit Web.Control()

        [<JavaScript>]
        override this.Body = Client.main() :> _