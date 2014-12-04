namespace Website

module Views =

    open IntelliFactory.Html
    open Content
    open Model
    
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
            Templates.home
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

    open IntelliFactory.Html
    open IntelliFactory.WebSharper.Sitelets
    open IntelliFactory.WebSharper.Sitelets.Content
    open System.Web

    let rss() : Content<Action> =
        let twitterPath = HttpContext.Current.Server.MapPath "~/JSON/Tweets.json"
        let soPath = HttpContext.Current.Server.MapPath "~/JSON/StackOverflowQuestions.json"
        let newReposPath = HttpContext.Current.Server.MapPath "~/JSON/NewGitHubRepos.json"
        let updatedReposPath = HttpContext.Current.Server.MapPath "~/JSON/UpdatedGitHubRepos.json"
        let gistsPath = HttpContext.Current.Server.MapPath "~/JSON/Gists.json"
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
                Headers = [] //Http.Header.Custom "Content-Type" "application/rss+xml"]
                WriteBody = fun stream ->
                    use tw = new System.IO.StreamWriter(stream)
                    tw.WriteLine "Fetched latest data!"
            }
    //            async {
    //    //            do! Twitter.Server.fetchNewTweets()
    //    //            do! StackOverflow.Server.fetchNewQuestions()
    //    //            do! GitHubRepos.Server.fetchNewRepos()
    //    //            do! GitHubRepos.Server.fetchUpdatedRepos()
    //    //            do! NuGet.Server.fetchPkgs()
    //                }
    //            |> Async.Start
