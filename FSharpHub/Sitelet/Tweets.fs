module Website.Tweets

open IntelliFactory.WebSharper

type Tweet =
    {
        Avatar     : string
        Date       : string
        Html       : string
        Id         : string
        Name       : string
        ScreenName : string
    }

    static member New avatar date html id name screenName =
        {
            Avatar     = avatar
            Date       = date
            Html       = html
            Id         = id
            Name       = name
            ScreenName = screenName
        }

type SearchResult = Failure | Success of Tweet list

/// Server-side code.
module private Server =
    open TweetSharp
    open System

    // Twitter authentication
    let ts = TwitterService(Secret.consKey, Secret.consSecret)
    ts.AuthenticateWith(Secret.token, Secret.tokenSecret)

    // search options
    let options = SearchOptions()
    options.Q <- "#fsharp"
    options.Count <- Nullable 50

    /// Returns the latest 100 "#fsharp" tweets.
    [<Remote>]
    let fetchTweets() =
        async {
            let searchResult =
                try
                    ts.Search(options).Statuses
                    |> Seq.toList
                    |> List.map (fun status ->
                        Tweet.New
                            status.Author.ProfileImageUrl
                            (status.CreatedDate.ToLongDateString())
                            status.TextAsHtml
                            (status.Id.ToString())
                            status.User.Name
                            status.Author.ScreenName)
                    |> Success
                with _ -> Failure
            return searchResult }

/// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    /// Creates an <li> containing the details of a tweet (screen name, creation date...).
    let li tweet =
        let id = tweet.Id
        let name = tweet.Name
        let screenName = tweet.ScreenName
        let profileLink = "https://twitter.com/" + screenName
        let replyLink = "https://twitter.com/intent/tweet?in_reply_to=" + id
        let retweetLink = "https://twitter.com/intent/retweet?tweet_id="  + id
        let favoriteLink = "https://twitter.com/intent/favorite?tweet_id=" + id
        let p = P []
        p.Html <- tweet.Html
        LI [Attr.Class "list-group-item"] -< [
            Div [
                A [HRef profileLink; Attr.Class "profile-link"; Attr.Target "_blank"] -< [
                    Img [Src tweet.Avatar; Alt name; Attr.Class "avatar"]
                    Strong [Text name]
                ] -< [Text <| " @" + screenName]
                Br []
                Small [Text tweet.Date]
                p
                Div [Attr.Class "tweet-actions"] -< [
                    A [HRef replyLink; Attr.Class "tweet-action"; Attr.Style "margin-right: 5px;"] -< [Text "Reply"]
                    A [HRef retweetLink; Attr.Class "tweet-action"; Attr.Style "margin-right: 5px;"] -< [Text "Retweet"]
                    A [HRef favoriteLink; Attr.Class "tweet-action"] -< [Text "Favorite"]
                ]
            ]
        ]

    /// Toggles the visibility of the reply, retweet and favorite links.
    let toggleActionsVisibility() =
        let jquery = JQuery.Of ".list-group-item"
        jquery.Mouseenter(fun x _ -> JQuery.Of(".tweet-actions", x).Css("visibility", "visible").Ignore).Ignore
        jquery.Mouseleave(fun x _ -> JQuery.Of(".tweet-actions", x).Css("visibility", "hidden").Ignore).Ignore

    /// Opens the reply, retweet and favorite links in a modal dialog.
    let handleTweetActions() =
        let jquery = JQuery.Of "a.tweet-action"
        jquery.Click(fun elt event ->
            event.PreventDefault()
            let href = elt.GetAttribute "href"
            Html5.Window.Self.ShowModalDialog href |> ignore).Ignore

    /// Appends a <div> containing a list of tweets to the DOM.
    let main() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender (fun elt ->
            async {
                let! searchResults = Server.fetchTweets()
                match searchResults with
                    | Failure -> JavaScript.Alert "Failed to fetch the latest tweets."
                    | Success tweets ->
                        let ul = UL [Attr.Class "list-group"; Attr.Id "tweets-ul"]
                        tweets |> List.iter (fun tweet -> ul.Append (li tweet))
                        elt.Append ul
                        toggleActionsVisibility()
                        handleTweetActions() }
            |> Async.Start)

/// A control for serving the main pagelet.
type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _