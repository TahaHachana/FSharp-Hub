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

module Server =

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
        |> List.filter (fun (start, ``end`` , _) -> ``end`` - start <> 1)
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

    open IntelliFactory.Html

    let tweetDiv tweet =
        let p = VerbatimContent("<p>" + tweet.statusAsHtml + "</p>")
        let screenName, statusId =
            match tweet.isRetweeted with
            | false -> tweet.screenName, tweet.id
            | true -> Option.get tweet.retweetedScreenName, Option.get tweet.retweetedId
        Div [Class "media"] -< [
            A [
                Class "media-left"
                HRef ("https://twitter.com/" + tweet.screenName)
                Target "_blank"
            ] -< [
                Img [
                    Src tweet.avatar
                    Class "avatar"]
            ]
            Div [Class "media-body"] -< [
                H4 [Class "media-heading"] -< [
                    A [
                        HRef ("https://twitter.com/" + tweet.screenName)
                        Target "_blank"
                    ] -< [Text tweet.screenName]
                    Text " "                                 
                    Small [
                        A [
                            HRef ("https://twitter.com/" + screenName + "/status/" + statusId)
                            Target "_blank"
                        ] -< [Text tweet.createdAt]                                    
                    ]
                ]
                p
            ]                        
        ]
     
    let tweetsDiv() =
        let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/Tweets.json"
        Utils.dataDiv<Tweet> jsonPath tweetDiv

    let fetchNewTweets jsonPath =
        async {
            let! tweetsArray = latestTweets()
            match tweetsArray with
            | None -> ()
            | Some tweets ->
                let json = JsonConvert.SerializeObject(tweets)
                File.WriteAllText(jsonPath, json)
        } |> Async.RunSynchronously