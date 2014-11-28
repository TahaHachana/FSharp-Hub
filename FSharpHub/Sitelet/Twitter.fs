module Website.Twitter

open IntelliFactory.WebSharper

type Tweet =
    {
        id : string
        screenName : string
        avatar: string
        statusAsHtml : string
        createdAt : string
        isRetweeted : bool
        retweetedId : string option
        retweetedScreenName : string option

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
        |> List.filter (fun (start, _, _) -> start <> 139)
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
                                    
    let newTweet (status:LinqToTwitter.Status) =
        let retweetedStatus = status.RetweetedStatus
        let retweeted =
            match retweetedStatus.StatusID with
            | 0UL -> false
            | _ -> true
        let retweetedScreenName =
            match retweeted with
            | false -> None
            | true -> Some retweetedStatus.User.ScreenNameResponse
        let retweetedId = 
            match retweeted with
            | false -> None
            | true -> Some (string retweetedStatus.StatusID)
        {
            id = string status.StatusID
            screenName = status.User.ScreenNameResponse
            avatar = status.User.ProfileImageUrl
            statusAsHtml = statusHtml status
            createdAt = status.CreatedAt.ToShortDateString() + " " + status.CreatedAt.ToShortTimeString()
            isRetweeted = retweeted
            retweetedId = retweetedId
            retweetedScreenName = retweetedScreenName
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
                let json = JsonConvert.SerializeObject(tweets)
                File.WriteAllText(jsonPath, json)
                return tweets
        }

/// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    let hideProress() =
        match JQuery.Of("[data-status=\"loading\"]").Length with
        | 0 ->
            JQuery.Of("#progress-bar").SlideUp().Ignore
            JQuery.Of("[data-spy=\"scroll\"]").Each(
                fun x -> JQuery.Of(x)?scrollspy("refresh")
            ).Ignore
        | _ ->
            JQuery.Of("[data-spy=\"scroll\"]").Each(
                fun x -> JQuery.Of(x)?scrollspy("refresh")
            ).Ignore

    let main() =
        Div [HTML5.Attr.Data "status" "loading"]
        |>! OnAfterRender (fun elt ->
            async {
                let! tweets = Server.tweets()
                tweets
                |> Array.mapi (fun idx tweet ->
                    let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                    let p = P []
                    p.Html <- tweet.statusAsHtml
                    let screenName, statusId =
                        match tweet.isRetweeted with
                        | false -> tweet.screenName, tweet.id
                        | true -> Option.get tweet.retweetedScreenName, Option.get tweet.retweetedId
                    Div [Attr.Class cls] -< [
                        Div [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef ("https://twitter.com/" + tweet.screenName); Attr.Target "_blank"] -< [
                                Img [Attr.Src tweet.avatar; Attr.Class "avatar"]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"] -< [
                                    A [
                                        Attr.HRef ("https://twitter.com/" + tweet.screenName)
                                        Attr.Target "_blank"
                                        Text tweet.screenName
                                    ] :> IPagelet
                                    Text " "                                 
                                    Small [
                                        A [
                                            Attr.HRef ("https://twitter.com/" + screenName + "/status/" + statusId)
                                            Attr.Target "_blank"
                                            Text tweet.createdAt
                                        ]                                    
                                    ] :> _
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
                elt.RemoveAttribute "data-status"
                hideProress()
            }
            |> Async.Start)


/// A control for serving the main pagelet.
type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _