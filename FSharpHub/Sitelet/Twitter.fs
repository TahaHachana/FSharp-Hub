module Website.Twitter

open IntelliFactory.WebSharper

type Tweet =
    {
        id : uint64
        screenName : string
        avatar: string
        statusAsHtml : string
        createdAt : string
    }

module private Server =
    open System.IO
    open Newtonsoft.Json
    open LinqToTwitter
    open LinqToTwitter.Security
    open System
    open System.Collections.Generic
    open System.Configuration
    open System.Text.RegularExpressions
    open System.Web

    let entityDetails (entity:#EntityBase) formattedStr =
        entity.Start,
        entity.End,
        formattedStr
    
    let hashTagDetails (hashTag:HashTagEntity) =
        let format = "<a href=\"https://twitter.com/search?f=realtime&q=%23{0}\" target=\"_blank\">#{0}</a>"
        entityDetails hashTag <| String.Format(format, hashTag.Tag)

    let urlDetails (url:UrlEntity) =
        let format = "<a href=\"{0}\" target=\"_blank\">{1}</a>"
        entityDetails url <| String.Format(format, url.Url, url.DisplayUrl)

    let userMentionDetails (userMention:UserMentionEntity) =
        let format = "<a href=\"http://twitter.com/{0}\" target=\"_blank\">@{0}</a>"
        entityDetails userMention <| String.Format(format, userMention.ScreenName)

    let entityDetails' (entities:List<#EntityBase>) detailsFunc =
        entities
        |> Seq.toList
        |> List.map detailsFunc

    let statusEntities (status:Status) =
        let entities = status.Entities
        [
            entityDetails' entities.HashTagEntities hashTagDetails
            entityDetails' entities.UrlEntities urlDetails
            entityDetails' entities.UserMentionEntities userMentionDetails
        ]
        |> List.concat
        |> List.sortBy (fun (start, _, _) -> start)

    let skipTake (str:string) idx start =
        str
        |> Seq.skip idx
        |> Seq.take (start - idx)
        |> String.Concat

    let formatEntities statusText entities =
        let skipTake' = skipTake statusText
        let rec f entities idx (acc:string) =
            match entities with
            | [(start, ``end``, str)] ->
                match start with
                | 139 ->
                    acc.Substring(0, acc.LastIndexOf(' ') + 1)
                    + str
                | _ ->
                    [
                        acc
                        skipTake' idx start
                        str
                        (statusText |> Seq.skip ``end`` |> String.Concat)
                    ]
                    |> String.Concat
            | (start, ``end``, str) :: tail ->
                let acc' =
                    [
                        acc
                        skipTake' idx start
                        str
                    ]
                    |> String.Concat
                f tail ``end`` acc' 
            | [] -> statusText
        f entities 0 ""

    let statusHtml (status:Status) =
        status
        |> statusEntities
        |> formatEntities status.Text

    let credStore =
        SingleUserInMemoryCredentialStore(
            ConsumerKey = Credentials.consumerKey,
            ConsumerSecret = Credentials.consumerSecret,
            AccessToken = Credentials.accessToken,
            AccessTokenSecret = Credentials.accessTokenSecret
        )

    let authorizer =
        SingleUserAuthorizer(
            CredentialStore = credStore,
            SupportsCompression = true
        )

    let ``#fsharpSearch``() =
        let context = new TwitterContext(authorizer)

        query {
            for x in context.Search do
                where (x.Type = SearchType.Search && x.Query = "#fsharp" && x.Count = 50)
                select x
        }
        |> fun x -> x.SingleOrDefaultAsync()
        |> Async.AwaitTask
                            
    let newTweet (status:Status) =
        {
            id = status.StatusID
            screenName = status.User.ScreenNameResponse
            avatar = status.User.ProfileImageUrl
            statusAsHtml = statusHtml status
            createdAt = status.CreatedAt.ToShortDateString() + " " + status.CreatedAt.ToShortTimeString()
        }

    let latestTweets() =
        async {
            try
                let! search = ``#fsharpSearch``()
                let statuses =
                    search.Statuses.ToArray()
                    |> Array.map newTweet
                return Some statuses
            with _ -> return None
        }

//    open LinqToTwitter
//    open LinqToTwitter.Security
//    open System
//    open System.Text.RegularExpressions
//
//    let creds = SingleUserInMemoryCredentialStore()
//    creds.ConsumerKey <- Secret.consKey
//    creds.ConsumerSecret <- Secret.consSecret
//    creds.AccessToken <- Secret.token
//    creds.AccessTokenSecret <- Secret.tokenSecret
//
//    let authorizer = SingleUserAuthorizer()
//    authorizer.CredentialStore <- creds
//    authorizer.SupportsCompression <- true
//
//    let context = new TwitterContext(authorizer)
////    context.StreamingUrl <- config.StreamApiUrl.EnsureEndsWith("/")
////    context.SearchUrl <- config.SearchApiUrl.EnsureEndsWith("/")
////    context.Log <- new LinqToTwitterLog() :> IO.TextWriter
//    let search =
//        query {
//            for x in context.Search do
//                where (x.Type = SearchType.Search && x.Query = "#fsharp")
//                select x
//        }

//    let search' =
//        search
//        |> Seq.nth 0
//        
//    let tweet = search'.Statuses.[0]
//    let text = tweet.Text

//    let tweetHtml text =
//        let urlRegex = Regex("\\b(([\\w-]+://?|www[.])[^\\s()<>]+(?:\\([\\w\\d]+\\)|([^\\p{P}\\s]|/)))", RegexOptions.IgnoreCase ||| RegexOptions.Compiled)
//        let mentionRegex = Regex("(^|\\W)@([A-Za-z0-9_]+)", RegexOptions.IgnoreCase ||| RegexOptions.Compiled)
//        let hashtagRegex = Regex("[#]+[A-Za-z0-9-_]+", RegexOptions.IgnoreCase ||| RegexOptions.Compiled)
//
////        mentionRegex.Match(text)
//        let t1 = urlRegex.Replace(text, fun (m:Match) -> String.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", m.Value))
//        let t2 =
//            mentionRegex.Replace(t1, fun m ->            
//                let value = m.Groups.[2].Value
//                let text = "@" + value
//                let format =
//                    if m.Value.StartsWith " " then
//                        " <a href=\"http://twitter.com/{0}\" target=\"_blank\">{1}</a>"
//                    else
//                        "<a href=\"http://twitter.com/{0}\" target=\"_blank\">{1}</a>"
//                String.Format(format, value, text)
//            )
//        let t3 = hashtagRegex.Replace(t2, fun m ->
//            let query = Uri.EscapeDataString(m.Value);
//            String.Format("<a href=\"http://search.twitter.com/search?q={0}\" target=\"_blank\">{1}</a>", query, m.Value)    
//        )
//        t3

    [<Remote>]
    let tweets() =
        async {
            let! tweetsArray = latestTweets()
            let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/Tweets.json"
            match tweetsArray with
            | None ->
                let tweets =
                    let json = File.ReadAllText jsonPath
                    JsonConvert.DeserializeObject(json, typeof<Tweet []>)
                    :?> Tweet []
                return tweets
            | Some tweets ->
                let test =
                    {
                        id = 1UL
                        screenName = "test"
                        avatar = "test"
                        statusAsHtml = "test"
                        createdAt = "test"
                    }
                let json = JsonConvert.SerializeObject(tweets)
                File.WriteAllText(jsonPath, json)
                return tweets
        }

/// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    let main() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender (fun elt ->
            async {
                let! tweets = Server.tweets()
                JQuery.Of("#twitter-progress").FadeOut().Ignore
                tweets
                |> Array.mapi (fun idx tweet ->
                    let p = P []
                    p.Html <- tweet.statusAsHtml
                    let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                    Div [Attr.Class cls] -< [
                        Div [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef ("https://twitter.com/" + tweet.screenName); Attr.Target "_blank"] -< [
                                Img [Attr.Src tweet.avatar; Attr.Class "avatar"]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"] -< [
                                    A [Attr.HRef ("https://twitter.com/" + tweet.screenName); Attr.Target "_blank"; Text tweet.screenName]
                                    Span [Text " "]                                    
                                    Small [
                                        A [Attr.HRef ("https://twitter.com/" + tweet.screenName + "/status/" + string tweet.id); Attr.Target "_blank"; Text tweet.createdAt]
                                    ]
                                ]
                                p
                            ]                        
                        ]
                    ]
                )
                |> Utils.split 2
                |> Seq.iter (fun x ->
                    Div [Attr.Class "row data-row"]
                    -< x
                    |> elt.Append
                )
                JQuery.Of("[data-spy=\"scroll\"]").Each(fun x -> JQuery.Of(x)?scrollspy("refresh")).Ignore
            }
            |> Async.Start)


/// A control for serving the main pagelet.
type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _



//
//
//
//
//
//
//
//
//
//
//
//type Tweet =
//    {
//        Avatar     : string
//        Date       : string
//        Html       : string
//        Id         : string
//        Name       : string
//        ScreenName : string
//    }
//
//    static member New avatar date html id name screenName =
//        {
//            Avatar     = avatar
//            Date       = date
//            Html       = html
//            Id         = id
//            Name       = name
//            ScreenName = screenName
//        }
//
//type SearchResult = Failure | Success of Tweet list
//
//
///// Server-side code.
//
//
////    open TweetSharp
////    open System
////
////    // Twitter authentication
////    let ts = TwitterService(Secret.consKey, Secret.consSecret)
////    ts.AuthenticateWith(Secret.token, Secret.tokenSecret)
////
////    // search options
////    let options = SearchOptions()
////    options.Q <- "#fsharp"
////    options.Count <- Nullable 50
////
////    /// Returns the latest 100 "#fsharp" tweets.
////    [<Remote>]
////    let fetchTweets() =
////        async {
////            let searchResult =
////                try
////                    ts.Search(options).Statuses
////                    |> Seq.toList
////                    |> List.map (fun status ->
////                        Tweet.New
////                            status.Author.ProfileImageUrl
////                            (status.CreatedDate.ToLongDateString())
////                            status.TextAsHtml
////                            (status.Id.ToString())
////                            status.User.Name
////                            status.Author.ScreenName)
////                    |> Success
////                with _ -> Failure
////            return searchResult }
//
///// Client-side code.
//[<JavaScript>]
//module private Client =
//    open IntelliFactory.WebSharper.Html
//    open IntelliFactory.WebSharper.JQuery
//
//    /// Creates an <li> containing the details of a tweet (screen name, creation date...).
//    let li tweet =
//        let id = tweet.Id
//        let name = tweet.Name
//        let screenName = tweet.ScreenName
//        let profileLink = "https://twitter.com/" + screenName
//        let replyLink = "https://twitter.com/intent/tweet?in_reply_to=" + id
//        let retweetLink = "https://twitter.com/intent/retweet?tweet_id="  + id
//        let favoriteLink = "https://twitter.com/intent/favorite?tweet_id=" + id
//        let p = P []
//        p.Html <- tweet.Html
//        LI [Attr.Class "list-group-item"] -< [
//            Div [
//                A [HRef profileLink; Attr.Class "profile-link"; Attr.Target "_blank"] -< [
//                    Img [Src tweet.Avatar; Alt name; Attr.Class "avatar"]
//                    Strong [Text name]
//                ] -< [Text <| " @" + screenName]
//                Br []
//                Small [Text tweet.Date]
//                p
//                Div [Attr.Class "tweet-actions"] -< [
//                    A [HRef replyLink; Attr.Class "tweet-action"; Attr.Style "margin-right: 5px;"] -< [Text "Reply"]
//                    A [HRef retweetLink; Attr.Class "tweet-action"; Attr.Style "margin-right: 5px;"] -< [Text "Retweet"]
//                    A [HRef favoriteLink; Attr.Class "tweet-action"] -< [Text "Favorite"]
//                ]
//            ]
//        ]
//
//    /// Toggles the visibility of the reply, retweet and favorite links.
//    let toggleActionsVisibility() =
//        let jquery = JQuery.Of ".list-group-item"
//        jquery.Mouseenter(fun x _ -> JQuery.Of(".tweet-actions", x).Css("visibility", "visible").Ignore).Ignore
//        jquery.Mouseleave(fun x _ -> JQuery.Of(".tweet-actions", x).Css("visibility", "hidden").Ignore).Ignore
//
//    /// Opens the reply, retweet and favorite links in a modal dialog.
//    let handleTweetActions() =
//        let jquery = JQuery.Of "a.tweet-action"
//        jquery.Click(fun elt event ->
//            event.PreventDefault()
//            let href = elt.GetAttribute "href"
//            Html5.Window.Self.ShowModalDialog href |> ignore).Ignore
//
//    /// Appends a <div> containing a list of tweets to the DOM.
//    let main() =
//        Div [Attr.Class "home-widget"]
//        |>! OnAfterRender (fun elt ->
//            async {
//                let! tweets = Server.tweets()
//                let ul = UL [Attr.Class "list-unstyled"; Attr.Id "tweets-ul"]
//                tweets |> List.iter (fun tweet -> ul.Append (LI [Text tweet]))
//                elt.Append ul
//            }
//
////                match searchResults with
////                    | Failure -> JavaScript.Alert "Failed to fetch the latest tweets."
////                    | Success tweets ->
////                        let ul = UL [Attr.Class "list-group"; Attr.Id "tweets-ul"]
////                        tweets |> List.iter (fun tweet -> ul.Append (li tweet))
////                        elt.Append ul
////                        toggleActionsVisibility()
////                        handleTweetActions() }
//            |> Async.Start)
//
//
///// A control for serving the main pagelet.
//type Control() =
//    inherit Web.Control()
//
//    [<JavaScript>]
//    override this.Body = Client.main() :> _