module Sitelet.StackOverflow

open IntelliFactory.WebSharper

type SoQuestion =
    {
        id : int
        link : string
        title : string
        creationDate : string
        answerCount : int
        ownerAvatar : string
        ownerLink : string
        tags : string []
        score : int
        acceptedAnswerId : int option
    }

module Server =
    
    open IntelliFactory.Html
    open StackExchange.StacMan
    open System
    open System.Configuration
    open System.Globalization
    open System.Net
    open System.IO
    open Newtonsoft.Json
    open System.Web

    let culture = CultureInfo.CreateSpecificCulture "en-US"
    CultureInfo.DefaultThreadCurrentCulture <- culture

    let client =
        new StacManClient(
            key = Credentials.stackExchangeKey,
            version="2.1"
        )

    let ``f#Questions``() =
        client.Questions.GetAll(
            "stackoverflow",
            page = Nullable(1),
            pagesize = Nullable(50),
            sort = Nullable(Questions.AllSort.Creation),
            order = Nullable(Order.Desc),
            tagged = "f#"
        )

    let newQuestion (q:Question) =
        let acceptedAnswer = q.AcceptedAnswerId
        let acceptedAnwserId =
            match acceptedAnswer.HasValue with
            | false -> None
            | true -> Some acceptedAnswer.Value
        {
            id = q.QuestionId
            link = q.Link
            title = q.Title |> WebUtility.HtmlDecode
            creationDate = q.CreationDate.ToString()
            answerCount = q.AnswerCount
            ownerAvatar = q.Owner.ProfileImage
            ownerLink = q.Owner.Link
            tags = q.Tags
            score = q.Score
            acceptedAnswerId = acceptedAnwserId
        }
    
    let latestQuestions() =
        async {
            try
                let questions =
                    ``f#Questions``().Result.Data.Items
                    |> Array.map newQuestion
                return Some questions
            with _ -> return None
        }

    let questionDiv q =
        Div [Class "media"]
        -< [
            A [
                Class "media-left"
                HRef q.ownerLink
                Target "_blank"
            ]
            -< [
                Img [
                    HTML5.Data "original" q.ownerAvatar
                    Class "avatar lazy"
                ]
            ]
            Div [Class "media-body"]
            -< [
                H4 [
                    Class "media-heading"
                    Style "word-break: break-word;"
                ] 
                -< [
                    A [
                        HRef q.link
                        Target "_blank"
                    ]
                    -< [Text q.title]                                        
                ]
                P [Text q.creationDate]
                P [
                    Text "Score: "
                    Span [Class "badge"]
                    -< [Text (string q.score)]
                    Text " Answers: "
                    Span [Class "badge"]
                    -< [Text (string q.answerCount)]
                ]
                Div [
                    for x in q.tags ->
                        Span [Class "label label-info"]
                        -< [Text x]
                ]
            ]               
        ]

    let stackDiv() =
        Utils.dataDiv<SoQuestion>
            "~/JSON/StackOverflowQuestions.json"
            questionDiv

    let fetchNewQuestions jsonPath =
        async {
            let! questionsArray = latestQuestions()
            match questionsArray with
            | None -> ()
            | Some questions ->
                let json = JsonConvert.SerializeObject questions
                File.WriteAllText(jsonPath, json)
        } |> Async.RunSynchronously