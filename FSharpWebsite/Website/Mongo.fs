namespace Website

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
        let createServer (connectionString: string) = MongoServer.Create connectionString

        /// Gets the database with the specified name.
        let databaseByName (server : MongoServer) (name : string) = server.GetDatabase name

        /// Gets the database collection with the specified name.
        let collectionByName<'T> (db : MongoDatabase) (name : string) = db.GetCollection<'T> name

        let server = createServer Secure.mongoConnectionString
        let database = databaseByName server "fsharpwebsite"

    [<AutoOpenAttribute>]
    module RecordTypes =
        
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
        type Snippet =
            {
                _id         : ObjectId
                Link        : string
                Title       : string
                Description : string
                Date        : DateTime
            }

        [<CLIMutableAttribute>]
        type Video =
            {
                _id       : ObjectId
                Title     : string
                Url       : string
                Thumbnail : string
                Website   : string
                Date      : DateTime
            }

        [<CLIMutableAttribute>]
        type Tweet =
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
        type Book =
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

    [<AutoOpen>]
    module Collections =
    
        let tweets    = Utilities.collectionByName<Tweet>          Utilities.database "tweets"
        let questions = Utilities.collectionByName<FSharpQuestion> Utilities.database "questions"
        let books     = Utilities.collectionByName<Book>           Utilities.database "books"
        let snippets  = Utilities.collectionByName<Snippet>        Utilities.database "snippets"
        let videos    = Utilities.collectionByName<Video>          Utilities.database "videos"

    [<AutoOpen>]
    module Queryable =
        
        let asQueryable (collection : MongoCollection<_>) = collection.FindAll().AsQueryable()
        
        let tweetsQueryable    = asQueryable tweets
        let questionsQueryable = asQueryable questions
        let booksQueryable     = asQueryable books
        let snippetsQueryable  = asQueryable snippets
        let videosQueryable    = asQueryable videos

    [<AutoOpenAttribute>]              
    module Tweets =

        let take20() =
            query {
                for x in tweetsQueryable do
                    sortByDescending x.CreationDate
                    take 20
            }
            |> Seq.toArray

        let skip skipCount =
            query {
                for x in tweetsQueryable do
                    sortByDescending x.CreationDate
                    skip skipCount
                    take 20
            }
            |> Seq.toArray

        let queryWhile latestTweetsId =
            query {
                for x in tweetsQueryable do
                    sortByDescending x.CreationDate
                    takeWhile (x.TweetID <> latestTweetsId)
            }
            |> Seq.toArray
            |> function
                | [||] -> None
                | arr  -> Some arr

    [<AutoOpenAttribute>]
    module Questions =

        let take20() =
            query {
                for x in questionsQueryable do
                    sortByDescending x.Date
                    take 20
            }
            |> Seq.toArray

        let skipTake20 skipCount =
            query {
                for x in questionsQueryable do
                    sortByDescending x.Date
                    skip skipCount
                    take 20
            }
            |> Seq.toArray

        let inline queryWhile latestQuestionId =
            query {
                for x in questionsQueryable do
                    sortByDescending x.Date
                    takeWhile (x._id.ToString() <> latestQuestionId)
            }
            |> Seq.toArray
            |> function
                | [||] -> None
                | arr  -> Some arr

    [<AutoOpenAttribute>]
    module Books =

        let inline queryAll() =
            query {
                for x in booksQueryable do
                    sortByDescending x.ReleaseDate
            }
            |> Seq.toArray

    module Snippets =

        let queryAll() =
            query {
                for x in snippetsQueryable do
                    sortByDescending x.Date
            }
            |> Seq.toArray

        let skip skipCount =
            query {
                for x in snippetsQueryable do
                    sortByDescending x.Date
                    skip skipCount
                    take 20
            }
            |> Seq.toArray

    module Videos =

        let queryAll() =
            query {
                for x in videosQueryable do
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
//        videos.Insert video




