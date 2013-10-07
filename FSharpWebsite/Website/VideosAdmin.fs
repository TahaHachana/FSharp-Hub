module Website.VideosAdmin    

open IntelliFactory.WebSharper

module Server =
    open System
    open Mongo
    open Records

    [<Rpc>]
    let insertVideo title url thumbnail website date =
        async {
            let isDate, datetime = DateTime.TryParse date
            match isDate with
            | false -> return false
            | true  ->
                let video = Video.New title url thumbnail website datetime
                let isOk = Videos.insert video
                return isOk
        }

module Client =
    open IntelliFactory.WebSharper.Formlet

    [<JavaScript>]
    let main() =
        let titleInput =
            Controls.Input ""
            |> Enhance.WithTextLabel "Title"
        let urlInput =
            Controls.Input ""
            |> Enhance.WithTextLabel "URL"
        let thumbnailInput =
            Controls.Input ""
            |> Enhance.WithTextLabel "Thumbnail"
        let websiteInput =
            Controls.Input ""
            |> Enhance.WithTextLabel "Website"
        let dateInput =
            Controls.Input ""
            |> Enhance.WithTextLabel "Date"
        let formlet =
            Formlet.Yield (fun title url thumb website date -> title, url, thumb, website, date)
            <*> titleInput
            <*> urlInput
            <*> thumbnailInput
            <*> websiteInput
            <*> dateInput
            |> Enhance.WithSubmitAndResetButtons
            |> Enhance.WithFormContainer
        Formlet.Run (fun (title, url, thumb, website, date) ->
            async {
                let! isOk = Server.insertVideo title url thumb website date
                match isOk with
                | false -> JavaScript.Alert "The query failed."
                | true  -> JavaScript.Alert "New video inserted successfully."
            }
            |> Async.Start) formlet

type Control() =

    inherit Web.Control()

    [<JavaScript>]
    override __.Body = Client.main()