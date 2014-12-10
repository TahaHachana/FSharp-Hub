module Sitelet.Views

open Content
open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets
open IntelliFactory.WebSharper.Sitelets.Content
open Model
open System.Web
    
let home =
    Skin.withTemplate<Action> 
        Templates.home
        Home.title
        Home.metaDesc
        <| fun ctx -> Home.body()

let error =
    Skin.withTemplate<Action>
        Templates.error
        "Error - Page Not Found"
        ""
        <| fun ctx -> Error.body

let books =
    Skin.withTemplate<Action>
        Templates.books
        Books.title
        Books.metaDesc
        <| fun ctx -> Books.body

let videos pageId =
    Skin.withTemplate<Action>
        Templates.videos
        (Videos.title pageId)
        Videos.metaDesc
        <| fun ctx -> Videos.body pageId

let login (action:Action option) =
    Skin.withTemplate<Action>
        Templates.error
        "Login"
        ""
        <| fun ctx -> Login.body action Action.Admin ctx

let admin =
    Skin.withTemplate<Action>
        Templates.admin
        "Admin Page"
        ""
        <| fun ctx -> Admin.body ctx

let booksAdmin =
    Skin.withTemplate<Action>
        Templates.admin
        ""
        ""
        <| fun ctx -> BooksAdmin.body ctx

let videosAdmin =
    Skin.withTemplate<Action>
        Templates.admin
        ""
        ""
        <| fun ctx -> VideosAdmin.body ctx

open System.Diagnostics

let checkNewData() : Content<Action> =
    let stopwatch = Stopwatch()
    stopwatch.Start()
    let twitterPath = HttpContext.Current.Server.MapPath "~/JSON/Tweets.json"
    let soPath = HttpContext.Current.Server.MapPath "~/JSON/StackOverflowQuestions.json"
    let newReposPath = HttpContext.Current.Server.MapPath "~/JSON/NewGitHubRepos.json"
    let updatedReposPath = HttpContext.Current.Server.MapPath "~/JSON/UpdatedGitHubRepos.json"
//        let gistsPath = HttpContext.Current.Server.MapPath "~/JSON/Gists.json"
    let nugetPath = HttpContext.Current.Server.MapPath "~/JSON/NuGet.json"
#if DEBUG
    Twitter.Server.fetchNewTweets twitterPath
    StackOverflow.Server.fetchNewQuestions soPath
    GitHubRepos.Server.fetchNewRepos newReposPath
    GitHubRepos.Server.fetchUpdatedRepos updatedReposPath
//        GitHubGists.Server.fetchNewGists gistsPath
    NuGet.Server.fetchPkgs nugetPath
#else
    try
        Twitter.Server.fetchNewTweets twitterPath
        StackOverflow.Server.fetchNewQuestions soPath
        GitHubRepos.Server.fetchNewRepos newReposPath
        GitHubRepos.Server.fetchUpdatedRepos updatedReposPath
//            GitHubGists.Server.fetchNewGists gistsPath
        NuGet.Server.fetchPkgs nugetPath
    with _ -> ()
#endif
    CustomContent <| fun context ->
        {
            Status = Http.Status.Ok
            Headers = []
            WriteBody = fun stream ->
                use tw = new System.IO.StreamWriter(stream)
                let elapsedMilliseconds =
                    stopwatch.Stop()
                    stopwatch.ElapsedMilliseconds.ToString()
                tw.WriteLine ("Fetching the latest data took " + elapsedMilliseconds + " milliseconds.")
        }