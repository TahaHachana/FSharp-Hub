namespace FSharpWebsite

#if INTERACTIVE
#r "MongoDB.Bson.dll"
#r "MongoDB.Driver.dll"
#endif

open System
open System.Linq
open MongoDB.Bson
open MongoDB.Driver
open MongoDB.Driver.Builders
open System.Globalization

module Mongo =
    
    let culture = CultureInfo.CreateSpecificCulture "en-US"
    CultureInfo.DefaultThreadCurrentCulture <- culture

    module Utilities =

        /// Creates a mongo server instance.
        let inline createServer (connectionString: string) = MongoServer.Create connectionString

        /// Gets the database with the specified name.
        let inline databaseByName (server : MongoServer) (name : string) = server.GetDatabase name

        /// Gets the database collection with the specified name.
        let inline collectionByName<'T> (db : MongoDatabase) (name : string) = db.GetCollection<'T> name

        let server = createServer Secure.mongoConnectionString
        let database = databaseByName server "fsharpwebsite"

    [<AutoOpenAttribute>]
    module Types =
        
        [<CLIMutableAttribute>]
        type FSharpTweet =
            {
                _id          : ObjectId 
                TweetID      : string
                UserID       : string
                ProfileImage : string
                DisplayName  : string
                ScreenName   : string
                CreationDate : DateTime
                Text         : string
            }

        [<CLIMutableAttribute>]
        type FSharpQuestion =
            {
                _id     : ObjectId
                Link    : string
                Title   : string
                Date    : DateTime
                Website : string
                Summary : string
            }

        [<CLIMutableAttribute>]
        type FSharpSnippet =
            {
                _id         : ObjectId
                Link        : string
                Title       : string
                Description : string
                Date        : DateTime
            }

        [<CLIMutableAttribute>]
        type FSharpVideo =
            {
                _id       : ObjectId
                Title     : string
                Url       : string
                Thumbnail : string
                Website   : string
                Date      : DateTime
            }

    [<AutoOpenAttribute>]              
    module Tweets =

        let fsharpTweetsCollection =
            Utilities.collectionByName<FSharpTweet> Utilities.database "fsharptweets"

        let queryable = fsharpTweetsCollection.FindAll().AsQueryable()

        let inline queryFsharpTweets () =
            query {
                for x in queryable do
                    sortByDescending x.CreationDate
                    take 20
            }
            |> Seq.toArray

        let inline queryFsharpTweets' tweetsToSkip =
            query {
                for x in queryable do
                    sortByDescending x.CreationDate
                    skip tweetsToSkip
                    take 20
            }
            |> Seq.toArray

        let inline queryFsharpTweets'' latestTweetsId =
            query {
                for x in queryable do
                    sortByDescending x.CreationDate
                    takeWhile (x.TweetID <> latestTweetsId)
            }
            |> Seq.toArray
            |> function
                | [||] -> None
                | arr  -> Some arr

    [<AutoOpenAttribute>]
    module Questions =

        let fsharpQuestionsCollection =
            Utilities.collectionByName<FSharpQuestion> Utilities.database "fsharpquestions"

        let queryable = fsharpQuestionsCollection.FindAll().AsQueryable()

        let inline queryFsharpQuestions () =
            query {
                for x in queryable do
                    sortByDescending x.Date
                    take 20
            }
            |> Seq.toArray

        let inline queryFsharpQuestions' questionsToSkip =
            query {
                for x in queryable do
                    sortByDescending x.Date
                    skip questionsToSkip
                    take 20
            }
            |> Seq.toArray

        let inline queryFsharpQuestions'' latestQuestionId =
            query {
                for x in queryable do
                    sortByDescending x.Date
                    takeWhile (x._id.ToString() <> latestQuestionId)
            }
            |> Seq.toArray
            |> function
                | [||] -> None
                | arr  -> Some arr

    [<AutoOpenAttribute>]
    module Books =

        [<CLIMutableAttribute>]
        type FSharpBook =
            {
                _id         : ObjectId
                Url         : string
                Title       : string
                Authors     : string []
                Publisher   : string
                ISBN        : string
                Pages       : int
                ReleaseDate : DateTime
                Cover       : string
            }

        let fsharpBooksCollection =
            Utilities.collectionByName<FSharpBook> Utilities.database "fsharpbooks"

        let queryable = fsharpBooksCollection.FindAll().AsQueryable()

        let inline queryFsharpBooks () =
            query {
                for x in queryable do
                    sortByDescending x.ReleaseDate
            }
            |> Seq.toArray

    [<AutoOpenAttribute>]
    module Snippets =

        let fsharpBooksCollection =
            Utilities.collectionByName<FSharpSnippet> Utilities.database "fsharpsnippets"

        let queryable = fsharpBooksCollection.FindAll().AsQueryable()

        let inline queryFsharpSnippets () =
            query {
                for x in queryable do
                    sortByDescending x.Date
            }
            |> Seq.toArray

        let inline queryFsharpSnippets' snippetsToSkip =
            query {
                for x in queryable do
                    sortByDescending x.Date
                    skip snippetsToSkip
                    take 20
            }
            |> Seq.toArray

    [<AutoOpenAttribute>]
    module Videos =

        let fsharpVideosCollection =
            Utilities.collectionByName<FSharpVideo> Utilities.database "fsharpvideos"

        let queryable = fsharpVideosCollection.FindAll().AsQueryable()

        let inline queryFsharpVideos () =
            query {
                for x in queryable do
                    sortByDescending x.Date
            }
            |> Seq.toArray

//        let video =
//            {
//                _id = ObjectId.GenerateNewId ()
//                Title       = ""
//                Url         = ""
//                Thumbnail   = ""
//                Website     = ""
//                Date    = DateTime.Parse ""
//            }
//
//        fsharpVideosCollection.Insert video




