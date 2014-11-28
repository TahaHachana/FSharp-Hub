module Website.GitHubIssues

open IntelliFactory.WebSharper

module private Server =

    open Octokit
    open Octokit.Internal
    open System
    open System.Globalization

    let culture = CultureInfo.CreateSpecificCulture "en-US"
    CultureInfo.DefaultThreadCurrentCulture <- culture

    let credStore = InMemoryCredentialStore(Credentials("TahaHachana", "SX2BV31yDWcz2tDW"))
    let github = new GitHubClient(new ProductHeaderValue("FSharpHub"), credStore)

    let request = SearchIssuesRequest("")
    request.Language <- Nullable Language.FSharp
//    request.Fork <- ForkQualifier.ExcludeForks
    request.Created <- DateRange.GreaterThanOrEquals (DateTime.Today.AddDays -1.)
    request.PerPage <- 100
    request.Page <- 1

    [<Remote>]
    let issues() =
        async {
            return
                github.Search.SearchIssues(request).Result.Items
                |> Seq.toList
                |> List.map (fun x -> x.User.HtmlUrl, x.User.AvatarUrl, x.Title, x.HtmlUrl.ToString(), x.UpdatedAt.ToString())
        }

/// Client-side code.
[<JavaScript>]
module private Client =
    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    let main() =
        Div [Attr.Class "home-widget"]
        |>! OnAfterRender (fun elt ->
            async {
                let! repos = Server.issues()
                let ul = UL [Attr.Class "media-list"; Attr.Id "tweets-ul"]
                repos |> List.iter (fun (userUrl, userAvatar, title, url, updatedAt) ->
                    let li =
                        LI [Attr.Class "media"] -< [
                            A [Attr.Class "media-left"; Attr.HRef userUrl; Attr.Target "_blank"] -< [
                                Img [Attr.Style "width: 30px; height: 30px;"; Attr.Src userAvatar]
                            ]
                            Div [Attr.Class "media-body"] -< [
                                H4 [Attr.Class "media-heading"] -< [
                                    A [Attr.HRef url; Attr.Target "_blank"; Text title]                                        
                                ]
                                P [Text updatedAt]
                            ]               
                        ]
                    ul.Append li)
                elt.Append ul
            }
            |> Async.Start)


/// A control for serving the main pagelet.
type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Client.main() :> _