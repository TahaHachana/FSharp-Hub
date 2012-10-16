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

    /// Creates a mongo server instance.
    let createServer (connectionString: string) = MongoServer.Create connectionString

    /// Gets the database with the specified name.
    let databaseByName (server : MongoServer) (name : string) = server.GetDatabase name

    /// Gets the database collection with the specified name.
    let collectionByName<'T> (db : MongoDatabase) (name : string) = db.GetCollection<'T> name

    let connectionString = ""
    let server = createServer connectionString
    let database = databaseByName server "fsharpwebsite"
    
    [<AutoOpenAttribute>]
    module Tweets =
    
        [<CLIMutableAttribute>]
        type FSharpTweet =
            {
                _id           : ObjectId 
                TweetID       : string
                UserID        : string
                ProfileImage  : string
                DisplayName   : string
                ScreenName    : string
                CreationDate  : DateTime
                Text          : string
            }

        let fsharpTweetsCollection = collectionByName<FSharpTweet> database "fsharptweets"

        let queryable = fsharpTweetsCollection.FindAll().AsQueryable()

        let queryFsharpTweets () =
            query {
                for x in queryable do
                    sortByDescending x.CreationDate
                    take 20
            }
            |> Seq.toArray

        let queryFsharpTweets' tweetsToSkip =
            query {
                for x in queryable do
                    sortByDescending x.CreationDate
                    skip tweetsToSkip
                    take 20
            }
            |> Seq.toArray

        let queryFsharpTweets'' latestTweetsId =
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

        [<CLIMutableAttribute>]
        type FSharpQuestion =
            {
                _id : ObjectId
                Link : string
                Title : string
                Date : DateTime
                Website : string
                Summary : string
            }

        let fsharpQuestionsCollection = collectionByName<FSharpQuestion> database "fsharpquestions"

        let queryable = fsharpQuestionsCollection.FindAll().AsQueryable()

        let queryFsharpQuestions () =
            query {
                for x in queryable do
                    sortByDescending x.Date
                    take 20
            }
            |> Seq.toArray

        let queryFsharpQuestions' questionsToSkip =
            query {
                for x in queryable do
                    sortByDescending x.Date
                    skip questionsToSkip
                    take 20
            }
            |> Seq.toArray

        let queryFsharpQuestions'' latestQuestionId =
            query {
                for x in queryable do
                    sortByDescending x.Date
                    takeWhile (x._id.ToString() <> latestQuestionId)
            }
            |> Seq.toArray
            |> function
                | [||] -> None
                | arr  -> Some arr
