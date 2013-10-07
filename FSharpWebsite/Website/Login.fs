module Website.Login

open IntelliFactory.WebSharper

type LoginInfo =
    {
        Name     : string
        Password : string
    }

type Access = Denied | Granted

module Server =
    open IntelliFactory.WebSharper.Sitelets

    [<Rpc>]
    let login loginInfo =
        async {
            let access =
                match loginInfo.Password = Secret.password with
                | false -> Denied
                | true ->
                    UserSession.LoginUser loginInfo.Name
                    Granted
            return access
        }

module Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    [<JavaScript>]
    let passInput =
        Input [
            Attr.Class "form-control"
            Attr.Id "password"
            Attr.Type "password"
        ]
        |>! OnKeyDown (fun _ keyCode ->
            match keyCode.KeyCode with
            | 13 -> JQuery.Of("#login-btn").Click().Ignore
            | _  -> ())

    [<JavaScript>]
    let loginForm (redirectUrl : string) =
        let userInput =
            Input [
                Attr.Class "form-control"
                Attr.Id "username"
                Attr.Type "text"
                HTML5.Attr.AutoFocus "autofocus"
            ]
        let submitBtn =
            Button [
                Attr.Class "btn btn-primary btn-block"
                Attr.Id "login-btn"
                Attr.Type "button"
            ]
            -- Text "Submit"
            |>! OnClick (fun _ _ ->
                async {
                    let info = {Name = userInput.Value; Password = passInput.Value}
                    let! access = Server.login info
                    match access with
                    | Denied -> JavaScript.Alert "Login failed"
                    | Granted -> Html5.Window.Self.Location.Assign redirectUrl
                } |> Async.Start)
        Form [Attr.NewAttr "role" "form"; Attr.Id "signin"] -< [
            H2 [Text "Please sign in"]
            FieldSet [Attr.Class "form-group"] -< [
                Label [Text "Username"; Attr.For "username"]
                userInput
                Label [Text "Password"; Attr.For "password"]
                passInput
            ]
            FieldSet [
                submitBtn
            ]
        ]

type Control(redirectUrl) =
    inherit Web.Control ()

    [<JavaScript>]
    override __.Body = Client.loginForm redirectUrl :> _