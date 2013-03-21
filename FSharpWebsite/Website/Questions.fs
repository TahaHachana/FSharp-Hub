﻿namespace Website

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery
open Mongo

module Questions =

    module Server =
        
        let inline questionData (question : FSharpQuestion) =
            question.Link, question.Title, question.Date.ToString(), question.Website, question.Summary

        [<RpcAttribute>]
        let latestFSharpQuestions () =
            async {
                let questions = Questions.take20()
                let latestQuestionId = questions.[0]._id.ToString()
                let questions' = questions |> Array.map questionData
                return latestQuestionId, questions'
            }

        [<RpcAttribute>]
        let questionsAfterSkip skip =
            async {
                return
                    Mongo.Questions.skipTake20 skip
                    |> Array.map questionData
            }

        [<RpcAttribute>]
        let newQuestions latestQuestionId =
            async {
                let newQuestionsOption = Questions.queryWhile latestQuestionId
                match newQuestionsOption with
                    | None -> return None
                    | Some questions ->
                        let latestQuestionId' = questions.[0]._id.ToString()
                        let questions' = questions |> Array.map questionData
                        return Some (latestQuestionId', questions')
            }

    module Client =
        
        [<JavaScriptAttribute>]
        let makeQuestionLi link title date website summary =
            LI [Attr.Class "question"] -< [
                A [HRef link; Attr.Target "_blank"] -< [
                    Strong [Text title]
                ]
                Br []
                Small [Text <| date + ", " + website]
                P [Text summary]
            ]

        [<JavaScriptAttribute>]
        let incrementQuestionsCount x =
            Utilities.Client.incrementDataCount "#fsharpQuestions" "data-questions-count" x

        [<JavaScriptAttribute>]
        let setQuestionId id =
            Utilities.Client.setAttributeValue "#fsharpQuestions" "data-question-id" id

        [<JavaScriptAttribute>]
        let checkNewQuestions () =
            async {
                let jquery = JQuery.Of "#fsharpQuestions"
                let latestQuestionId = jquery.Attr "data-question-id"
                let! questionsOption = Server.newQuestions latestQuestionId
                match questionsOption with
                    | None -> ()
                    | Some (id, questions) ->
                        questions
                        |> Array.rev
                        |> Array.map (fun (link, title, date, website, summary) ->
                            makeQuestionLi link title date website summary)
                        |> Array.iter (Utilities.Client.prependElement "#questionsList")

                        let count = Array.length questions
                        incrementQuestionsCount count

                        setQuestionId id

                        let msg =
                            match count with
                                | 1 -> "1 new question"
                                | _ -> string count + " new questions"
                        Utilities.Client.displayInfoAlert msg
            } |> Async.Start

        [<JavaScriptAttribute>]
        let questionsDiv () =

            let questionsList = UL [Id "questionsList"]

            let loadMoreBtn =
                Button [Text "Load More"; Attr.Class "btn loadMore"]
                |>! OnClick (fun x _ ->
                    async {
                        x.SetAttribute("disabled", "disabled")
                        let jquery = JQuery.Of "#fsharpQuestions"
                        let count = jquery.Attr "data-questions-count" |> int
                        let! fsharpQuestions = Server.questionsAfterSkip count

                        fsharpQuestions
                        |> Array.map (fun (link, title, date, website, summary) ->
                            makeQuestionLi link title date website summary)
                        |> Array.iter questionsList.Append

                        let count' = Array.length fsharpQuestions
                        incrementQuestionsCount count'
                        x.RemoveAttribute("disabled")
                    } |> Async.Start)

//            Div [Id "questionsDiv"] -< [questionsList; loadMoreBtn]
            Div [Id "fsharpQuestions"; HTML5.Attr.Data "questions-count" "0"; HTML5.Attr.Data "question-id" ""] -< [questionsList; loadMoreBtn]
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
                    JavaScript.SetInterval checkNewQuestions 420000 |> ignore
                } |> Async.Start)

        type QuestionsViewer() =
            
            inherit Web.Control ()

            [<JavaScriptAttribute>]
            override this.Body = questionsDiv() :> _