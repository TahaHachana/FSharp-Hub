namespace Website

module Tweets =

    open IntelliFactory.WebSharper
    
    [<JavaScript>]
    module Client =
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        type Tweet =
            {
                created_at              : string
                from_user               : string
                from_user_id            : string
                from_user_id_str        : string
                from_user_name          : string
                geo                     : string
                id                      : string
                id_str                  : string
                iso_language_code       : string
                metadata                : obj
                profile_image_url       : string
                profile_image_url_https : string
                source                  : string
                text                    : string
            }

        type Result =
            {
                completed_in     : string
                max_id           : string
                max_id_str       : string
                next_page        : string
                page             : string
                query            : string
                refresh_url      : string
                results          : Tweet []
                results_per_page : string
                since_id         : string
                since_id_str     : string
            }

        let atRegex   = EcmaScript.RegExp("(@)([A-Za-z0-9-_]+)", "g")
        let hashRegex = EcmaScript.RegExp("(#)([A-Za-z0][A-Za-z0-9-_]+)", "g")
        let urlRegex  = EcmaScript.RegExp("([A-Za-z]+:\/\/[A-Za-z0-9-_]+\.[A-Za-z0-9-_:%&amp;;\?\/.=]+)", "g")

        let replaceUsers (str : string) = EcmaScript.String(str).Replace(atRegex, "<a href=\"https://twitter.com/$2\" target=\"_blank\">@$2</a>")
        let replaceHashs (str : string) = EcmaScript.String(str).Replace(hashRegex, "<a href=\"https://twitter.com/search/?q=%23$2\" target=\"_blank\">#$2</a>")
        let replaceUrls (str : string)  = EcmaScript.String(str).Replace(urlRegex, "<a href=\"$1\" target=\"_blank\">$1</a>")

        let linkify = replaceUrls >> replaceUsers >> replaceHashs

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

        let displayTweets (ul : Element) (elt : Element) =       
            JQuery.GetJSON("http://search.twitter.com/search.json?q=%23fsharp&amp;rpp=100&amp;callback=?", (fun (data, _) ->
                let data = As<Result> data
                data.results
                |> Array.iter (fun result ->
                    let tweetHtml = linkify <| result.text
                    tweetLi result.from_user result.id_str result.profile_image_url result.from_user_name tweetHtml result.created_at
                    |> ul.Append)
                do elt.Append ul)
            ).Then(
                (fun _ ->
                    do toggleActionsVisibility()
                    do handleTweetActions()),
                (fun _ -> do ()))

        let main() =
            Div [Id "fsharp-tweets"]
            |>! OnAfterRender (fun elt ->
                async {
                    let ul = UL [Id "tweets-list"]
                    displayTweets ul elt |> ignore
                } |> Async.Start)

    type Control() =
        inherit Web.Control()

        [<JavaScript>]
        override this.Body = Client.main() :> _