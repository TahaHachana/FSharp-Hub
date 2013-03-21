namespace Website

open System
open System.Text.RegularExpressions
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery
open Mongo

module Tweets =

    module Server =
        
        let atRegex     = Utilities.Server.compileRegex "^@[^\ :]+"
        let atRegex'    = Utilities.Server.compileRegex "@"
        let hashRegex   = Utilities.Server.compileRegex "^#[^\ ]+"
        let urlRegex    = Utilities.Server.compileRegex "^https?://.+"
        let colonRegex  = Utilities.Server.compileRegex "([^\ ]):\ "
        let colonRegex' = Utilities.Server.compileRegex " :"
        let hashRegex'  = Utilities.Server.compileRegex "#"

        let inline spaceBeforeColon str =
            colonRegex.Replace(str, (fun (x : Match) -> x.Groups.[1].Value + " : "))
        let inline spaceBeforeColon' str = colonRegex'.Replace(str, ":")

        let inline replaceAt x =
            let x' = atRegex'.Replace(x, "") 
            String.concat "" ["<a href=\"https://twitter.com/"; x'; "\" target=\"_blank\">"; x; "</a>"]

        let inline replaceHash x =
            let x' = hashRegex'.Replace(x, "")
            String.concat "" ["<a href=\"https://twitter.com/search/?q=%23"; x'; "&src=hash\" target=\"_blank\">"; x; "</a>"]
        
        let inline replaceUrl x =
            String.concat "" ["<a href=\""; x; "\" target=\"_blank\">"; x; "</a>"]

        let inline formatString (regex : Regex) (replacementFunc : string -> string) str =
            regex.Replace(str, replacementFunc str)
        
        let formatString' =
            formatString atRegex replaceAt
            >> formatString hashRegex replaceHash
            >> formatString urlRegex replaceUrl
        
        let inline linkifyText (text : string) =
            text
            |> spaceBeforeColon
            |> fun x -> x.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
            |> Array.map formatString'
            |> String.concat " "
            |> spaceBeforeColon'

        let inline tweetData x =
            let text' = linkifyText x.Text
            x.ScreenName, x.TweetID, x.ProfileImage, x.DisplayName, text', x.CreationDate.ToString()

        [<RpcAttribute>]
        let latestTweets () =
            async {
                return
                    Tweets.take20()
                    |> Array.map tweetData
            }

        [<RpcAttribute>]
        let tweetsAfterSkip skip =
            async {
                return
                    Tweets.skip skip
                    |> Array.map tweetData
            }

        [<RpcAttribute>]
        let newTweets latestTweetId =
            async {
                let newTweetsOption = Tweets.queryWhile latestTweetId
                match newTweetsOption with
                    | None -> return None
                    | Some tweets ->
                        return        
                            tweets
                            |> Array.map tweetData
                            |> Some
            }

    module Client =

        [<JavaScriptAttribute>]
        let makeTweetLi screenName tweetId profileImage fullName tweetHtml creationDate =
            let profileLink = "https://twitter.com/" + screenName
            let replyLink = "https://twitter.com/intent/tweet?in_reply_to=" + tweetId
            let retweetLink = "https://twitter.com/intent/retweet?tweet_id=" + tweetId
            let favoriteLink = "https://twitter.com/intent/retweet?tweet_id=" + tweetId
            let tweetP = P []
            tweetP.Html <- tweetHtml
            LI [Attr.Class "tweet"] -< [
                A [HRef profileLink; Attr.Class "twitterProfileLink"; Attr.Target "_blank"] -< [
                    Img [Src profileImage; Alt fullName; Attr.Class "avatar"; Height "48"; Width "48"]
                    Strong [Text fullName]
                ] -< [Text (" @" + screenName)]
                Br []
                Small [Text creationDate]
                tweetP
                Div [Attr.Class "pull-right"] -< [
                    UL [Attr.Class "tweetActions"] -< [
                        LI [Attr.Class "tweetAction"] -< [A [HRef replyLink; Attr.Target "_blank"]    -< [Text "Reply"]]
                        LI [Attr.Class "tweetAction"] -< [A [HRef retweetLink; Attr.Target "_blank"]  -< [Text "Retweet"]]
                        LI [Attr.Class "tweetAction"] -< [A [HRef favoriteLink; Attr.Target "_blank"] -< [Text "Favorite"]]
                    ]
                ]
            ]

        [<JavaScriptAttribute>]
        let incrementTweetsCount x =
            Utilities.Client.incrementDataCount "#fsharpTweets" "data-tweets-count" x

        [<JavaScriptAttribute>]
        let setTweetId id =
            Utilities.Client.setAttributeValue "#fsharpTweets" "data-tweet-id" id

        [<JavaScriptAttribute>]
        let toggleActionsVisibility () =
            let jquery = JQuery.Of ".tweet"
            jquery.Mouseenter(fun x _ ->
                JQuery.Of(".tweetActions", x).Css("visibility", "visible").Ignore).Ignore
            jquery.Mouseleave(fun x _ ->
                JQuery.Of(".tweetActions", x).Css("visibility", "hidden").Ignore).Ignore

        [<JavaScriptAttribute>]
        let checkNewTweets () =
            async {
                let jquery = JQuery.Of "#fsharpTweets"
                let latestTweetId = jquery.Attr "data-tweet-id"
                let! tweetsOption = Server.newTweets latestTweetId
                match tweetsOption with
                    | None -> ()
                    | Some tweets ->
                        let latestTweetId =
                            tweets.[0]
                            |> (fun (_, id, _, _, _, _) -> id)

                        tweets
                        |> Array.rev
                        |> Array.map (fun (screenName, tweetId, profileImage, displayName, text, creationDate) ->
                            makeTweetLi screenName tweetId profileImage displayName text creationDate)
                        |> Array.iter (Utilities.Client.prependElement "#tweetsList")
                        
                        let count = Array.length tweets
                        incrementTweetsCount count
                        setTweetId latestTweetId
                        toggleActionsVisibility ()
                        
                        let msg =
                            match count with
                                | 1 -> "1 new tweet"
                                | _ -> string count + " new tweets"
                        Utilities.Client.displayInfoAlert msg
            } |> Async.Start

        [<JavaScriptAttribute>]
        let tweetsDiv () =
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
                        x.RemoveAttribute "disabled"
                    } |> Async.Start)
                    
                    
//            Div [Id "tweetsDiv"] -< [tweetsList; loadMoreBtn]
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
                    JavaScript.SetInterval checkNewTweets 300000 |> ignore
                } |> Async.Start)

    type FsharpTweetsViewer () =
        inherit Web.Control ()

        [<JavaScriptAttribute>]
        override this.Body =
            Client.tweetsDiv () :> _