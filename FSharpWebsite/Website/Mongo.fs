namespace Website

#if INTERACTIVE
#r "MongoDB.Bson.dll"
#r "MongoDB.Driver.dll"
#endif

open System
open System.Globalization
open System.Linq
open MongoDB.Bson
open MongoDB.Driver
open MongoDB.Driver.Builders

module Mongo =
    
    let culture = CultureInfo.CreateSpecificCulture "en-US"
    CultureInfo.DefaultThreadCurrentCulture <- culture

    [<AutoOpen>]
    module Utilities =

        let mongoClient (connectionString: string) = MongoClient connectionString
        let databaseByName (server : MongoServer) (name : string) = server.GetDatabase name
        let collectionByName<'T> (db : MongoDatabase) (name : string) = db.GetCollection<'T> name

    let client = mongoClient Secure.connectionString
    let server = client.GetServer()
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

            static member Newup url title authors publisher isbn pages releaseDate cover =
                {
                    _id         = ObjectId.GenerateNewId()
                    Url         = url
                    Title       = title
                    Authors     = authors
                    Publisher   = publisher
                    ISBN        = isbn
                    Pages       = pages
                    ReleaseDate = releaseDate
                    Cover       = cover
                }

        [<CLIMutableAttribute>]
        type NewsItem =
            {
                _id     : ObjectId
                Title   : string
                Summary : string
                Url     : string
                Date    : DateTime
            }

    [<AutoOpen>]
    module Collections =
    
        let tweets    = collectionByName<Tweet>          database "tweets"
        let questions = collectionByName<FSharpQuestion> database "questions"
        let books     = collectionByName<Book>           database "books"
        let snippets  = collectionByName<Snippet>        database "snippets"
        let videos    = collectionByName<Video>          database "videos"
        let news      = collectionByName<NewsItem>       database "news"

    [<AutoOpen>]
    module Queryable =
        
        let asQueryable (collection : MongoCollection<_>) = collection.FindAll().AsQueryable()
        
        let tweetsQueryable    = asQueryable tweets
        let questionsQueryable = asQueryable questions
        let booksQueryable     = asQueryable books
        let snippetsQueryable  = asQueryable snippets
        let videosQueryable    = asQueryable videos
        let newsQueryable      = asQueryable news

    module Tweets =

        let latest20() =
            query {
                for x in tweetsQueryable do
                    sortByDescending x.CreationDate
                    take 20
            }

        let skipLatest20 skipCount =
            query {
                for x in tweetsQueryable do
                    sortByDescending x.CreationDate
                    skip skipCount
                    take 20
            }

        let takeWhile latestTweetsId =
            query {
                for x in tweetsQueryable do
                    sortByDescending x.CreationDate
                    takeWhile (x.TweetID <> latestTweetsId)
            }

    module Questions =

        let latest20() =
            query {
                for x in questionsQueryable do
                    sortByDescending x.Date
                    take 20
            }

        let skipLatest20 skipCount =
            query {
                for x in questionsQueryable do
                    sortByDescending x.Date
                    skip skipCount
                    take 20
            }

        let inline takeWhile latestQuestionId =
            query {
                for x in questionsQueryable do
                    sortByDescending x.Date
                    takeWhile (x._id.ToString() <> latestQuestionId)
            }

    module Books =

        let all() =
            query {
                for x in booksQueryable do
                    sortByDescending x.ReleaseDate
            }

        let insert book =
            let result = books.Insert book
            result.Ok

    module Snippets =

        let latest20() =
            query {
                for x in snippetsQueryable do
                    sortByDescending x.Date
                    take 20
            }

        let skipLatest20 skipCount =
            query {
                for x in snippetsQueryable do
                    sortByDescending x.Date
                    skip skipCount
                    take 20
            }

    module Videos =

        let all() =
            query {
                for x in videosQueryable do
                    sortByDescending x.Date
            }

    module News =

        let latest10() =
            query {
                for x in newsQueryable do
                    sortByDescending x.Date
                    take 10
            }

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




