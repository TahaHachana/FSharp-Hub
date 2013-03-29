namespace Website

open IntelliFactory.WebSharper

module NewsAdmin =

    module Server =
        
        open System
        open Mongo

        [<Rpc>]
        let insertNewsItem title summary url date =
            async {
                let isDate, datetime = DateTime.TryParse date
                match isDate with
                    | false -> return false
                    | true  ->
                        let newsItem = NewsItem.Make title summary url datetime
                        let isOk = News.insert newsItem
                        return isOk
            }

    module Client =

        open IntelliFactory.WebSharper.Formlet

        [<JavaScript>]
        let main() =
            let titleInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Title"
            let summaryInput =
                Controls.TextArea ""
                |> Enhance.WithTextLabel "Summary"
            let urlInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "URL"
            let dateInput =
                Controls.Input ""
                |> Enhance.WithTextLabel "Date"
            let formlet =
                Formlet.Yield (fun title summary url date -> title, summary, url, date)
                <*> titleInput
                <*> summaryInput
                <*> urlInput
                <*> dateInput
                |> Enhance.WithSubmitAndResetButtons
                |> Enhance.WithFormContainer
            Formlet.Run (fun (title, summary, url, date) ->
                async {
                    let! isOk = Server.insertNewsItem title summary url date
                    match isOk with
                        | false -> JavaScript.Alert "The query failed."
                        | true  -> JavaScript.Alert "News item inserted successfully."
                }
                |> Async.Start) formlet

        type Control() =

            inherit Web.Control()

            [<JavaScript>]
            override __.Body = main()


