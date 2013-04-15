namespace Website

open System
open System.Text.RegularExpressions
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery
open Mongo

module Tweets =

    module private Server =
        
        open Utilities.Server

        let hashTagRegex = compileRegex "^([\p{P}-[#]]*)#([a-zA-Z][a-zA-Z0-9_]+)(\p{P}*$)"
        let atRegex = compileRegex "^([\p{P}-[@]]*)@([a-zA-Z][a-zA-Z0-9_]+)(.*)"
        let urlRegex = compileRegex "^(\p{P}*)(https?://.+?)(\p{P}*$)"

        let matchGroups (matchObj : Match) =
            let groups = matchObj.Groups
            let g1 = groups.[1].Value
            let g2 = groups.[2].Value
            let g3 = groups.[3].Value
            g1, g2, g3

        let formatHashTags str =
            hashTagRegex.Replace(str, (fun (matchObj : Match) ->
                let g1, g2, g3 = matchGroups matchObj
                String.concat "" [g1; "<a href=\"https://twitter.com/search/?q=%23"; g2; "&src=hash\">#"; g2; "</a>"; g3]))

        let formatAts str =
            atRegex.Replace(str, (fun (matchObj : Match) ->
                let g1, g2, g3 = matchGroups matchObj
                String.concat "" [g1; "<a href=\"https://twitter.com/"; g2; "\">@"; g2; "</a>"; g3]))

        let formatUrls str =
            urlRegex.Replace(str, (fun (matchObj : Match) ->
                let g1, g2, g3 = matchGroups matchObj
                String.concat "" [g1; "<a href=\""; g2; "\">"; g2; "</a>"; g3]))

        let format = formatAts >> formatHashTags >> formatUrls

        let tweetData x =
            let text' =
                x.Text
                |> fun x -> x.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
                |> Array.map format
                |> String.concat " "
            x.ScreenName, x.TweetID, x.ProfileImage, x.DisplayName, text', x.CreationDate.ToString()

        [<Rpc>]
        let latestTweets () =
            async {
                let array =
                    Tweets.latest20()
                    |> Seq.toArray
                    |> Array.map tweetData
                return array
            }

        [<Rpc>]
        let tweetsAfterSkip skip =
            async {
                let array =
                    Tweets.skipLatest20 skip
                    |> Seq.toArray
                    |> Array.map tweetData
                return array
            }

        [<Rpc>]
        let newTweets latestTweetId =
            async {
                let arrayOption =
                    Tweets.takeWhile latestTweetId
                    |> Seq.toArray
                    |> function
                        | [||] -> None
                        | arr  ->
                            arr
                            |> Array.map tweetData
                            |> Some
                return arrayOption
            }

    module Client =

        open Utilities.Client

        [<JavaScript>]
        let makeTweetLi screenName tweetId profileImage fullName tweetHtml creationDate =
            let profileLink = "https://twitter.com/" + screenName
            let replyLink = "https://twitter.com/intent/tweet?in_reply_to=" + tweetId
            let retweetLink = "https://twitter.com/intent/retweet?tweet_id=" + tweetId
            let favoriteLink = "https://twitter.com/intent/favorite?tweet_id=" + tweetId
            let tweetP = P []
            tweetP.Html <- tweetHtml
            LI [Attr.Class "tweet"; Attr.Style "clear: both;"] -< [
                Div [
                    A [HRef profileLink; Attr.Class "twitterProfileLink"] -< [
                        Img [Src profileImage; Alt fullName; Attr.Class "avatar"; Height "48"; Width "48"]
                        Strong [Text fullName]
                    ] -< [Text (" @" + screenName)]
                    Br []
                    Small [Text creationDate]
                    tweetP
                    Div [Attr.Class "tweetActions"; Attr.Style "visibility: hidden;"] -< [
                        A [HRef replyLink; Attr.Class "tweet-action-link"; Attr.Style "margin-right: 5px;"] -< [Text "Reply"]
                        A [HRef retweetLink; Attr.Class "tweet-action-link"; Attr.Style "margin-right: 5px;"] -< [Text "Retweet"]
                        A [HRef favoriteLink; Attr.Class "tweet-action-link"] -< [Text "Favorite"]
                    ]
                ]
            ]

        [<JavaScriptAttribute>]
        let incrementTweetsCount x = incrementDataCount "#fsharpTweets" "data-tweets-count" x

        [<JavaScriptAttribute>]
        let setTweetId id = setAttributeValue "#fsharpTweets" "data-tweet-id" id

        [<JavaScriptAttribute>]
        let toggleActionsVisibility() =
            let jquery = JQuery.Of ".tweet"
            jquery.Mouseenter(fun x _ ->
                JQuery.Of(".tweetActions", x).Css("visibility", "visible").Ignore).Ignore
            jquery.Mouseleave(fun x _ ->
                JQuery.Of(".tweetActions", x).Css("visibility", "hidden").Ignore).Ignore

        [<JavaScript>]
        let handleTweetActions() =
            let jquery = JQuery.Of ".tweet-action-link"
            jquery.Mousedown(fun element event ->
                event.PreventDefault()
                let href = element.GetAttribute "href"
                Html5.Window.Self.ShowModalDialog href |> ignore).Ignore

//        [<JavaScriptAttribute>]
//        let checkNewTweets() =
//            async {
//                let jquery = JQuery.Of "#fsharpTweets"
//                let latestTweetId = jquery.Attr "data-tweet-id"
//                let! tweetsOption = Server.newTweets latestTweetId
//                match tweetsOption with
//                    | None -> ()
//                    | Some tweets ->
//                        let latestTweetId =
//                            tweets.[0]
//                            |> (fun (_, id, _, _, _, _) -> id)
//
//                        tweets
//                        |> Array.rev
//                        |> Array.map (fun (screenName, tweetId, profileImage, displayName, text, creationDate) ->
//                            makeTweetLi screenName tweetId profileImage displayName text creationDate)
//                        |> Array.iter (Utilities.Client.prependElement "#tweetsList")
//                        
//                        let count = Array.length tweets
//                        incrementTweetsCount count
//                        setTweetId latestTweetId
//                        toggleActionsVisibility ()
//                        
//                        let msg =
//                            match count with
//                                | 1 -> "1 new tweet"
//                                | _ -> string count + " new tweets"
//                        Utilities.Client.displayInfoAlert msg
//            } |> Async.Start

        [<JavaScriptAttribute>]
        let tweetsDiv() =
            let tweetsList = UL [Id "tweetsList"]

            let loadMoreBtn =
                Button [Text "Load More"; Attr.Class "btn loadMore"]
                |>! OnClick (fun x _ ->
                    async {
                        x.SetAttribute("disabled", "disabled")
                        let jquery = JQuery.Of "#fsharpTweets"
                        let count = jquery.Attr "data-tweets-count" |> int
                        let! fsharpTweets = Server.tweetsAfterSkip count

                        fsharpTweets
                        |> Array.map (fun (screenName, tweetId, profileImage, displayName, text, creationDate) ->
                            makeTweetLi screenName tweetId profileImage displayName text creationDate)
                        |> Array.iter tweetsList.Append

                        let count' = Array.length fsharpTweets
                        incrementTweetsCount count'
                        toggleActionsVisibility ()
                        handleTweetActions()
                        x.RemoveAttribute "disabled"
                    } |> Async.Start)
                    
            Div [Id "fsharpTweets"; HTML5.Attr.Data "tweets-count" "0"; HTML5.Attr.Data "tweet-id" ""] -< [tweetsList; loadMoreBtn]
            |>! OnAfterRender(fun _ ->
                async {
                    let! fsharpTweets = Server.latestTweets ()
                    let latestTweetId =
                        fsharpTweets.[0]
                        |> (fun (_, id, _, _, _, _) -> id)
                    fsharpTweets
                    |> Array.map (fun (screenName, tweetId, profileImage, displayName, text, creationDate) ->
                        makeTweetLi screenName tweetId profileImage displayName text creationDate)
                    |> Array.iter (fun x -> tweetsList.Append x)
                    incrementTweetsCount 20
                    setTweetId latestTweetId
                    loadMoreBtn.SetCss("visibility", "visible")
                    toggleActionsVisibility ()
                    handleTweetActions()
//                    JavaScript.SetInterval checkNewTweets 300000 |> ignore
                } |> Async.Start)

        type Control() =
            
            inherit Web.Control()

            [<JavaScript>]
            override this.Body = tweetsDiv() :> _