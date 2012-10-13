namespace FSharpWebsite

open System
open System.Text.RegularExpressions
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery
open Mongo

module FSharpTweets =

    module Server =
        
        let compileRegex pattern = Regex(pattern, RegexOptions.Compiled)

        let atRegex = compileRegex "^@[^\ :]+"
        let atRegex' = compileRegex "@"
        let hashRegex = compileRegex "^#[^\ ]+"
        let urlRegex = compileRegex "^https?://.+"
        let colonRegex = compileRegex "([^\ ]):\ "
        let colonRegex' = compileRegex " :"
        let hashRegex' = compileRegex "#"

        let spaceBeforeColon str = colonRegex.Replace(str, (fun (x : Match) -> x.Groups.[1].Value + " : "))
        let spaceBeforeColon' str = colonRegex'.Replace(str, ":")

        let replaceAt x =
            let x' = atRegex'.Replace(x, "")            
            "<a href=\"https://twitter.com/" + x' + "\" target=\"_blank\">" + x + "</a>"

        let replaceHash x =
            let x' = hashRegex'.Replace(x, "")
            "<a href=\"https://twitter.com/search/?q=%23" + x' + "&src=hash\" target=\"_blank\">" + x + "</a>"
        let replaceUrl x = "<a href=\"" + x + "\" target=\"_blank\">" + x + "</a>"

        let formatString (regex : Regex) (replacementFunc : string -> string) str =
            regex.Replace(str, replacementFunc str)
        
        let formatString' =
            formatString atRegex replaceAt
            >> formatString hashRegex replaceHash
            >> formatString urlRegex replaceUrl
        
        let linkifyText (text : string) =
            text
            |> spaceBeforeColon
            |> fun x -> x.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
            |> Array.map formatString'
            |> String.concat " "
            |> spaceBeforeColon'

        [<RpcAttribute>]
        let latestTweets () =
            async {
                let tweets = queryFsharpTweets ()
                let tweetsData = tweets |> Array.map (fun x ->
                    let text' = linkifyText x.Text
                    x.ScreenName, x.TweetID, x.ProfileImage, x.DisplayName, text', x.CreationDate)
                return tweetsData
            }

        [<RpcAttribute>]
        let tweetsAfterSkip skip =
            async {
                let tweets = queryFsharpTweets' skip
                let tweetsData = tweets |> Array.map (fun x ->
                    let text' = linkifyText x.Text
                    x.ScreenName, x.TweetID, x.ProfileImage, x.DisplayName, text', x.CreationDate)
                return tweetsData
            }

        [<RpcAttribute>]
        let newTweets latestTweetId =
            async {
                let newTweetsOption = queryFsharpTweets'' latestTweetId
                match newTweetsOption with
                    | None -> return None
                    | Some tweets ->
                        let tweetsData = tweets |> Array.map (fun x ->
                            let text' = linkifyText x.Text
                            x.ScreenName, x.TweetID, x.ProfileImage, x.DisplayName, text', x.CreationDate)
                        return Some tweetsData
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
                Small [Attr.Class "pull-right"] -< [Text <| "(" + creationDate + ")"]
                tweetP
                Div [Attr.Class "pull-right"] -< [
                    UL [Attr.Class "tweetActions"] -< [
                        LI [Attr.Class "tweetAction"] -< [A [HRef replyLink] -< [Text "Reply"]]
                        LI [Attr.Class "tweetAction"] -< [A [HRef retweetLink] -< [Text "Retweet"]]
                        LI [Attr.Class "tweetAction"] -< [A [HRef favoriteLink] -< [Text "Favorite"]]
                    ]
                ]
            ]

        [<JavaScriptAttribute>]
        let incrementTweetsCount x =
            let jquery = JQuery.Of("#fsharpTweets")
            let count = jquery.Attr("data-tweets-count") |> int
            let count' = x + count |> string
            jquery.Attr("data-tweets-count", count').Ignore

        [<JavaScriptAttribute>]
        let setTweetId id = JQuery.Of("#fsharpTweets").Attr("data-tweet-id", id).Ignore

        [<JavaScriptAttribute>]
        let toggleActionsVisibility () =
            let jquery = JQuery.Of ".tweet"
            jquery.Mouseenter(fun x _ ->
                JQuery.Of(".tweetActions", x).Css("visibility", "visible").Ignore).Ignore
            jquery.Mouseleave(fun x _ ->
                JQuery.Of(".tweetActions", x).Css("visibility", "hidden").Ignore).Ignore

        [<JavaScriptAttribute>]
        let displayInfoAlert msg =
            let alertDiv =
                Div [Attr.Class "alert alert-info"; Id "alertDiv"] -< [
                    Button [Attr.Type "button"; Attr.Class "close"; HTML5.Attr.Data "dismiss" "alert"] -< [Text "×"]
                    |>! OnClick (fun _ _ -> JQuery.Of("#alertDiv").Remove().Ignore)
                    P [Attr.Class "centered"] -< [Text msg]
                ]
            JQuery.Of("#navigation").Append(alertDiv.Dom).Ignore
            JQuery.Of("#alertDiv").Show().Ignore
        
        [<JavaScriptAttribute>]
        let checkNewTweets () =
            async {
                let jquery = JQuery.Of("#fsharpTweets")
                let latestTweetId = jquery.Attr("data-tweet-id")
                let! tweetsOption = Server.newTweets latestTweetId
                match tweetsOption with
                    | None -> ()
                    | Some tweets ->
                        let count = jquery.Attr("data-tweets-count") |> int
                        let latestTweetId =
                            tweets.[0]
                            |> (fun (_, id, _, _, _, _) -> id)

                        tweets
                        |> Array.rev
                        |> Array.map (fun (screenName, tweetId, profileImage, displayName, text, creationDate) ->
                            makeTweetLi screenName tweetId profileImage displayName text creationDate)
                        |> Array.iter (fun x -> JQuery.Of("#tweetsList").Prepend(x.Dom).Ignore)
                        let count' = Array.length tweets
                        incrementTweetsCount count'
                        setTweetId latestTweetId
                        toggleActionsVisibility ()
                        let msg =
                            match count' with
                                | 1 -> "1 new tweet"
                                | _ -> string count' + " new tweets"
                        displayInfoAlert msg
            } |> Async.Start

        [<JavaScriptAttribute>]
        let tweetsDiv () =
            let tweetsList = UL [Id "tweetsList"]

            JavaScript.SetInterval checkNewTweets 60000 |> ignore
            
            let loadMoreBtn =
                Button [Text "Load More"; Attr.Class "btn loadMore"]
                |>! OnClick (fun x _ ->
                    async {
                        x.SetAttribute("disabled", "disabled")
                        let jquery = JQuery.Of("#fsharpTweets")
                        let count = jquery.Attr("data-tweets-count") |> int
                        let! fsharpTweets = Server.tweetsAfterSkip count

                        fsharpTweets
                        |> Array.map (fun (screenName, tweetId, profileImage, displayName, text, creationDate) ->
                            makeTweetLi screenName tweetId profileImage displayName text creationDate)
                        |> Array.iter tweetsList.Append

                        let count' = Array.length fsharpTweets
                        incrementTweetsCount count'
                        toggleActionsVisibility ()
                        x.RemoveAttribute("disabled")
                    } |> Async.Start)
                    
                    
            Div [Id "tweetsDiv"] -< [tweetsList; loadMoreBtn]
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
                } |> Async.Start)

    type FsharpTweetsViewer () =
        inherit Web.Control ()

        [<JavaScriptAttribute>]
        override this.Body =
            Client.tweetsDiv () :> _