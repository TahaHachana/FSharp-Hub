namespace Website

open IntelliFactory.WebSharper

module Feedback =

    module private Server =

        open RestSharp

        [<Rpc>]
        let sendEmail (message : string) =
            async {
                let client = new RestClient()
                client.BaseUrl <- "https://api.mailgun.net/v2"
                client.Authenticator <- new HttpBasicAuthenticator("api", Secure.mailgunKey);
                let request = new RestRequest()
                request.AddParameter("domain", Secure.mailgunDomain, ParameterType.UrlSegment) |> ignore
                request.Resource <- "{domain}/messages"
                request.AddParameter("from", "noreply@fsharp-hub.apphb.com") |> ignore
                request.AddParameter("to", Secure.myEmail) |> ignore
                request.AddParameter("subject", "FSharp Hub Feedback") |> ignore
                request.AddParameter("text", message) |> ignore
                request.Method <- Method.POST
                let response = client.Execute(request)
                match response.ResponseStatus with
                    | ResponseStatus.Completed -> return true
                    | _                        -> return false
            }

    [<JavaScript>]
    module Client =

        open IntelliFactory.WebSharper.Formlet
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        let textarea = TextArea [Rows "5"; Attr.Style "width: 280px"; Id "message"]
                
        let sendFeedback() =
            async {
                JQuery.Of("#submit-btn").Attr("disabled", "disabled").Ignore
                let message = textarea.Value
                let! success = Server.sendEmail message
                match success with
                    | false -> JavaScript.Alert "Failed to send your feedback. Please try again."
                    | true  ->
                        textarea.Value <- ""
                        JQuery.Of("#form").Toggle().Ignore
                        JavaScript.Alert "Your feedback was sent."
                JQuery.Of("#submit-btn").RemoveAttr("disabled").Ignore
            } |> Async.Start

        let btn =
                Button [Attr.Type "button"; Attr.Class "btn btn-primary"; Id "submit-btn"] -< [Text "Submit"]
                |>! OnClick (fun _ _ -> sendFeedback())

        let form =
            Form [Attr.Style "width: 300px"] -< [
                textarea
                Br []
                btn
            ]

        let main() =
            Div [Attr.Style "position: fixed; bottom: 5px; right: 50px; background-color: white;"] -< [
                Div [Attr.Style "font-weight: bold; background-color: black; color: white; padding: 10px; cursor: pointer;"] -< [Text "Feedback"]
                |>! OnClick (fun elt _ ->
                    JQuery.Of("#form").Toggle().Ignore
                    JQuery.Of("#message").Focus().Ignore)
                Div [Id "form"; Attr.Style "display: none; margin: 10px;"] -< [form]
            ]

    type Control() =

        inherit Web.Control()

        [<JavaScript>]
        override __.Body = Client.main() :> _