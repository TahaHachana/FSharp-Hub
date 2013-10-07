module Website.Questions

open IntelliFactory.WebSharper

module private Server =
    open Records
    open Mongo

    let quesData (q:Question) =
        q.Link, q.Title, q.Date.ToString(),
        q.Website, q.Summary

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

    let li (link, title, date, website, summary) =
        LI [Attr.Class "list-group-item"] -< [
            A [HRef link] -< [Strong [Text title]]
            Br []
            Small [Text <| date + ", " + website]
            P [Text summary]
        ]

    let displayQuestions arr (elt : Element) =
        let ul = UL [Attr.Class "list-group"; Id "questions-list"]
        arr
        |> Array.map li
        |> Array.iter ul.Append
        elt.Append ul

    let main() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender(fun elt ->
            async {
                let! questions = Server.latest()
                match questions with
                    | None -> ()
                    | Some questions -> displayQuestions questions elt
            } |> Async.Start)

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _