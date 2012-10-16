namespace FSharpWebsite

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery

module FSharpQuestions =

    module Server =

        [<RpcAttribute>]
        let latestFSharpQuestions () =
            async {
                let fsharpQuestions = Mongo.Questions.queryFsharpQuestions()
                let latestQuestionsId = fsharpQuestions.[0]._id.ToString()
                let questionsData = fsharpQuestions |> Array.map (fun x ->
                    x.Link, x.Title, x.Date.ToString(), x.Website, x.Summary)
                return latestQuestionsId, questionsData
            }

        [<RpcAttribute>]
        let questionsAfterSkip skip =
            async {
                let questions = Mongo.Questions.queryFsharpQuestions' skip
                let questionsData = questions |> Array.map (fun x ->
                    x.Link, x.Title, x.Date.ToString(), x.Website, x.Summary)
                return questionsData
            }

        [<RpcAttribute>]
        let newQuestions latestQuestionId =
            async {
                let newQuestionsOption = Mongo.Questions.queryFsharpQuestions'' latestQuestionId
                match newQuestionsOption with
                    | None -> return None
                    | Some questions ->
                        let latestQuestionId' = questions.[0]._id.ToString()
                        let questionsData = questions |> Array.map (fun x ->
                            x.Link, x.Title, x.Date.ToString(), x.Website, x.Summary)
                        return Some (latestQuestionId', questionsData)
            }

    module Client =
        
        [<JavaScriptAttribute>]
        let makeQuestionLi link title date website summary =
            LI [Attr.Class "question"] -< [
                A [HRef link; Attr.Target "_blank"] -< [
                    Strong [Text title]
                ]
                Small [Text <| " (" + date + ", " + website + ")"]
                P [Text summary]
            ]

        [<JavaScriptAttribute>]
        let incrementQuestionsCount x =
            let jquery = JQuery.Of("#fsharpQuestions")
            let count = jquery.Attr("data-questions-count") |> int
            let count' = x + count |> string
            jquery.Attr("data-questions-count", count').Ignore

        [<JavaScriptAttribute>]
        let setQuestionId id = JQuery.Of("#fsharpQuestions").Attr("data-question-id", id).Ignore

        [<JavaScriptAttribute>]
        let checkNewQuestions () =
            async {
                let jquery = JQuery.Of("#fsharpQuestions")
                let latestQuestionId = jquery.Attr("data-question-id")
                let! questionsOption = Server.newQuestions latestQuestionId
                match questionsOption with
                    | None -> ()
                    | Some (id, questions) ->

                        questions
                        |> Array.rev
                        |> Array.map (fun (link, title, date, website, summary) ->
                            makeQuestionLi link title date website summary)
                        |> Array.iter (fun x -> JQuery.Of("#questionsList").Prepend(x.Dom).Ignore)
                        let count = Array.length questions
                        incrementQuestionsCount count
                        setQuestionId id
                        let msg =
                            match count with
                                | 1 -> "1 new question"
                                | _ -> string count + " new question"
                        Utilities.displayInfoAlert msg
            } |> Async.Start

        [<JavaScriptAttribute>]
        let questionsDiv () =

            JavaScript.SetInterval checkNewQuestions 300000 |> ignore

            let questionsList = UL [Id "questionsList"]

            let loadMoreBtn =
                Button [Text "Load More"; Attr.Class "btn loadMore"]
                |>! OnClick (fun x _ ->
                    async {
                        x.SetAttribute("disabled", "disabled")
                        let jquery = JQuery.Of("#fsharpQuestions")
                        let count = jquery.Attr("data-questions-count") |> int
                        let! fsharpQuestions = Server.questionsAfterSkip count

                        fsharpQuestions
                        |> Array.map (fun (link, title, date, website, summary) ->
                            makeQuestionLi link title date website summary)
                        |> Array.iter questionsList.Append

                        let count' = Array.length fsharpQuestions
                        incrementQuestionsCount count'
                        x.RemoveAttribute("disabled")
                    } |> Async.Start)

            Div [Id "questionsDiv"] -< [questionsList; loadMoreBtn]
            |>! OnAfterRender(fun _ ->
                async {
                    let! id, fsharpQuestions = Server.latestFSharpQuestions ()
                    fsharpQuestions
                    |> Array.map (fun (link, title, date, website, summary) ->
                        makeQuestionLi link title date website summary)
                    |> Array.iter (fun x -> questionsList.Append x)
                    incrementQuestionsCount 20
                    setQuestionId id
                    loadMoreBtn.SetCss("visibility", "visible")
                } |> Async.Start)

    type FsharpQuestionsViewer () =
        inherit Web.Control ()

        [<JavaScriptAttribute>]
        override this.Body =
            Client.questionsDiv () :> _