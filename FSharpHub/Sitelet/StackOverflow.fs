module Website.StackOverflow

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

module private Server =
    
    open StackExchange.StacMan
    open System
    open System.Configuration
    open System.Globalization
    open System.Net
    open System.IO
    open Newtonsoft.Json
    open System.Web

    [<AutoOpen>]
    module private Private =
        let culture = CultureInfo.CreateSpecificCulture "en-US"
        CultureInfo.DefaultThreadCurrentCulture <- culture

        let client =
            let key = Credentials.stackExchangeKey
            new StacManClient(key=key, version="2.1")

        let ``f#Questions``() =
            client.Questions.GetAll(
                "stackoverflow",
                page = Nullable(1),
                pagesize = Nullable(50),
                sort = Nullable(Questions.AllSort.Creation),
                order = Nullable(Order.Desc),
                tagged = "f#")

        let newQuestion (q:StackExchange.StacMan.Question) =
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

    [<Remote>]
    let questions() =
        async {
            let! questionsArray = latestQuestions()
            let jsonPath = HttpContext.Current.Server.MapPath "~/JSON/StackOverflowQuestions.json"
            match questionsArray with
            | None ->
                let questions =
                    let json = File.ReadAllText jsonPath
                    JsonConvert.DeserializeObject(json, typeof<SoQuestion []>)
                    :?> SoQuestion []
                return questions
            | Some questions ->
                let json = JsonConvert.SerializeObject questions
                File.WriteAllText(jsonPath, json)
                return questions
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
                let! questions = Server.questions()
                questions
                |> Array.mapi (fun idx q ->
                    let cls = if idx % 2 = 0 then "col-md-5" else "col-md-5 col-md-offset-1"
                    Div [Attr.Class cls] -< [
                        Div [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef q.ownerLink; Attr.Target "_blank"] -< [
                                Img [Attr.Src q.ownerAvatar; Attr.Class "avatar"]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                yield H4 [Attr.Class "media-heading"; Attr.Style "word-break: break-word;"] -< [
                                    A [Attr.HRef q.link; Attr.Target "_blank"; Text q.title]                                        
                                ]
                                yield P [Text q.creationDate]
                                yield P [
                                    Text "Score: "
                                    Span [Attr.Class "badge"; Text (string q.score)] :> IPagelet
                                    Text " Answers: "
                                    Span [Attr.Class "badge"; Text (string q.answerCount)] :> IPagelet
                                ]
//                                yield P [Text <| "Answers: " + string q.answerCount]
//                                if q.acceptedAnswerId.IsSome then yield A [Attr.HRef (q.link + "#answer-" + q.acceptedAnswerId.Value.ToString()); Attr.Target "_blank"; Text "Accepted Answer"]
                                yield Div [
                                    for x in q.tags -> Span [Attr.Class "label label-primary"; Text x]
                                ]
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



