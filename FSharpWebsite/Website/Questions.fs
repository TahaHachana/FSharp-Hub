namespace Website

open IntelliFactory.WebSharper

module Questions =

    module private Server =
        open Mongo

        let quesData (question : Question) = question.Link, question.Title, question.Date.ToString(), question.Website, question.Summary

        let quesData'() =
            Questions.latest20()
            |> Seq.toArray
            |> Array.map quesData
            |> Some

        [<Rpc>]
        let latest() =
            async {
                let dataOption = try quesData'() with _ -> None
                return dataOption
            }

    [<JavaScript>]
    module Client =
        open IntelliFactory.WebSharper.Html
        open Utilities.Client

        let li (link, title, date, website, summary) =
            LI [Attr.Class "question"] -< [
                A [HRef link] -< [Strong [Text title]]
                Br []
                Small [Text <| date + ", " + website]
                P [Text summary]
            ]

        let displayQuestions arr (elt : Element) =
            let ul = UL [Id "questions-list"]
            arr
            |> Array.map li
            |> Array.iter ul.Append
            do elt.Append ul

        let main() =
            Div [Id "fsharp-questions"]
            |>! OnAfterRender(fun elt ->
                async {
                    let! questions = Server.latest()
                    match questions with
                        | None     -> do ()
                        | Some arr -> do displayQuestions arr elt
                } |> Async.Start)

    type Control() =
        inherit Web.Control()

        [<JavaScript>]
        override this.Body = Client.main() :> _