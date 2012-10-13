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

module Mongo =

    [<CLIMutableAttribute>]
    type FSharpTweet =
        {
            _id           : ObjectId 
            TweetID       : string
            UserID        : string
            ProfileImage  : string
            DisplayName   : string
            ScreenName    : string
            CreationDate  : string
            Text          : string
        }

    /// Creates a mongo server instance.
    let createServer (connectionString: string) = MongoServer.Create connectionString

    /// Gets the database with the specified name.
    let databaseByName (server : MongoServer) (name : string) = server.GetDatabase name

    /// Gets the database collection with the specified name.
    let collectionByName<'T> (db : MongoDatabase) (name : string) = db.GetCollection<'T> name

    let connectionString = ""
    let server = createServer connectionString
    let database = databaseByName server "fsharpwebsite"
    let fsharpTweetsCollection = collectionByName<FSharpTweet> database "fsharptweets"

    let queryable = fsharpTweetsCollection.FindAll().AsQueryable()

    let queryFsharpTweets () =
        query {
            for x in queryable do
                sortByDescending (DateTime.Parse x.CreationDate)
                take 20
                select x
        }
        |> Seq.toArray

    let queryFsharpTweets' tweetsToSkip =
        query {
            for x in queryable do
                sortByDescending (DateTime.Parse x.CreationDate)
                skip tweetsToSkip
                take 20
                select x
        }
        |> Seq.toArray

    let queryFsharpTweets'' latestTweetsId =
        query {
            for x in queryable do
                sortByDescending (DateTime.Parse x.CreationDate)
                takeWhile (x.TweetID <> latestTweetsId)
        }
        |> Seq.toArray
        |> function
            | [||] -> None
            | arr  -> Some arr