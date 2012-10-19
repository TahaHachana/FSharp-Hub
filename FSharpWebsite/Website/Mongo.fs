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

        let connectionString = ""
        let server = createServer connectionString
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

        let inline queryFsharpQuestions () =
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






//        let book1 =
//                {
//                   _id = ObjectId.GenerateNewId()
//                   Url = "http://www.apress.com/9781590598504"
//                   Title  = "Expert F#"
//                   Authors = [|"Antonio Cisternino"; "Adam Granicz"; "Don Syme"|]
//                   Publisher = "Apress"
//                   ISBN = "978-1-59059-850-4"
//                   Pages = 609
//                   ReleaseDate = DateTime.Parse "10/10/2007"
//                   Cover = "http://www.apress.com/media/catalog/product/cache/9/image/9df78eab33525d08d6e5fb8d27136e95/A/9/A9781590598504-3d_3.png"
//                }
//
//        let book2 =
//            {
//                _id         = ObjectId.GenerateNewId()
//                Url = "http://www.apress.com/9781430246503"
//                Title       = "Expert F# 3.0"
//                Authors     = [|"Antonio Cisternino"; "Adam Granicz"; "Don Syme"|]
//                Publisher   = "Apress"
//                ISBN        = "978-1-4302-4650-3"
//                Pages       = 650
//                ReleaseDate = DateTime.Parse "10/31/2012"
//                Cover       = "http://www.apress.com/media/catalog/product/cache/9/image/9df78eab33525d08d6e5fb8d27136e95/A/9/A9781590598504-3d_3.png"
//            }
//
//        let book3 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://eu.wiley.com/WileyCDA/WileyTitle/productCd-047052801X.html"
//               Title = "Professional F# 2.0"
//               Authors = [|"Ted Neward"; "Aaron Erickson"; "Talbott Crowell"; "Rick Minerich"|]
//               Publisher = "Wiley"
//               ISBN = "978-0-470-52801-3"
//               Pages = 432
//               ReleaseDate = DateTime.Parse "November 2010"
//               Cover = "http://media.wiley.com/product_data/coverImage300/1X/04705280/047052801X.jpg"
//            }
//
//        let book4 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://www.apress.com/9781590597576"
//               Title = "Foundations of F#"
//               Authors = [|"Robert Pickering"|]
//               Publisher = "Apress"
//               ISBN = "978-1-59059-757-6"
//               Pages = 360
//               ReleaseDate = DateTime.Parse "06/01/2007"
//               Cover = "http://www.apress.com/media/catalog/product/cache/9/image/9df78eab33525d08d6e5fb8d27136e95/A/9/A9781590597576-3d_9.png"
//            }
//
//        let book5 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://www.apress.com/9781430224310"
//               Title = "Expert F# 2.0"
//               Authors = [|"Don Syme"; "Adam Granicz"; "Antonio Cisternino"|]
//               Publisher = "Apress"
//               ISBN = "978-1-4302-2431-0"
//               Pages = 624
//               ReleaseDate = DateTime.Parse "06/07/2010"
//               Cover = "http://www.apress.com/media/catalog/product/cache/9/image/9df78eab33525d08d6e5fb8d27136e95/A/9/A9781430224310-3d_10.png"
//            }
//
//        let book6 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://www.apress.com/9781430223894"
//               Title = "Beginning F#"
//               Authors = [|"Robert Pickering"|]
//               Publisher = "Apress"
//               ISBN = "978-1-4302-2389-4"
//               Pages = 448
//               ReleaseDate = DateTime.Parse "12/21/2009"
//               Cover = "http://www.apress.com/media/catalog/product/cache/9/image/9df78eab33525d08d6e5fb8d27136e95/A/9/A9781430223894-3d_10.png"
//            }
//
//        let book7 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://shop.oreilly.com/product/9780596153656.do"
//               Title = "Programming F#"
//               Authors = [|"Chris Smith"|]
//               Publisher = "O'Reilly"
//               ISBN = "978-0-596-15364-9"
//               Pages = 410
//               ReleaseDate = DateTime.Parse "October 2009"
//               Cover = "http://akamaicovers.oreilly.com/images/9780596153656/cat.gif"
//            }
//
//        let book8 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://eu.wiley.com/WileyCDA/WileyTitle/productCd-0470242116.html"
//               Title = "F# for Scientists"
//               Authors = [|"Jon Harrop"; "Don Syme"|]
//               Publisher = "Wiley"
//               ISBN = "978-0-470-24211-7"
//               Pages = 368
//               ReleaseDate = DateTime.Parse "August 2008"
//               Cover = "http://media.wiley.com/product_data/coverImage300/16/04702421/0470242116.jpg"
//            }
//
//        let book9 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://shop.oreilly.com/product/0636920024033.do"
//               Title = "Programming F# 3.0"
//               Authors = [|"Chris Smith"|]
//               Publisher = "O'Reilly"
//               ISBN = "978-1-4493-2029-4"
//               Pages = 476
//               ReleaseDate = DateTime.Parse "October 2012"
//               Cover = "http://akamaicovers.oreilly.com/images/0636920024033/cat.gif"
//            }
//
//        let book10 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://shop.oreilly.com/product/0636920026099.do"
//               Title = "Building Web, Cloud, and Mobile Solutions with F#"
//               Authors = [|"Daniel Mohl"|]
//               Publisher = "O'Reilly"
//               ISBN = "978-1-4493-3376-8"
//               Pages = 166
//               ReleaseDate = DateTime.Parse "October 2012"
//               Cover = "http://akamaicovers.oreilly.com/images/0636920026099/rc_cat.gif"
//            }
//
//        let book11 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://shop.oreilly.com/product/0790145366641.do"
//               Title = "F# for C# Developers"
//               Authors = [|"Tao Liu"|]
//               Publisher = "Microsoft Press"
//               ISBN = "978-0-7356-7026-6"
//               Pages = 608
//               ReleaseDate = DateTime.Parse "March 2013"
//               Cover = "http://akamaicovers.oreilly.com/images/0790145366641/cat.gif"
//            }
//
//        let book12 =
//            {
//               _id = ObjectId.GenerateNewId()
//               Url = "http://www.manning.com/petricek/"
//               Title = "Real-World Functional Programming"
//               Authors = [|"Tomas Petricek"; "Jon Skeet"|]
//               Publisher = "Manning"
//               ISBN = "9781933988924"
//               Pages = 560
//               ReleaseDate = DateTime.Parse "December 2009"
//               Cover = "http://www.manning.com/petricek/petricek_cover150.jpg"
//            }
//
//        let books =
//            [
//                book1
//                book2
//                book3
//                book4
//                book5
//                book6
//                book7
//                book8
//                book9
//                book10
//                book11
//                book12
//            ]
//
//        fsharpBooksCollection.InsertBatch books

