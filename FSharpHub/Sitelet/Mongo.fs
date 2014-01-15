module Website.Mongo

open System.Globalization
open System.Linq
open MongoDB.Driver
open Records
    
let culture = CultureInfo.CreateSpecificCulture "en-US"
CultureInfo.DefaultThreadCurrentCulture <- culture

let client = MongoClient Secret.connectionString
let server = client.GetServer()
let db = server.GetDatabase "fsharpwebsite"
let collByName<'T> (name:string) = db.GetCollection<'T> name
    
let questions = collByName<Question> "questions"
let books     = collByName<Book>     "books"
let snippets  = collByName<Snippet>  "snippets"
let videos    = collByName<Video>    "videos"
        
let asQueryable (collection : MongoCollection<_>) = collection.FindAll().AsQueryable()
        
let questionsQueryable = asQueryable questions
let booksQueryable     = asQueryable books
let snippetsQueryable  = asQueryable snippets
let videosQueryable    = asQueryable videos

module Questions =

    let latest20() =
        query {
            for x in questionsQueryable do
                sortByDescending x.Date
                take 20
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

module Videos =

    let all() =
        query {
            for x in videosQueryable do
                sortByDescending x.Date
        }

    let insert video =
        let result = videos.Insert video
        result.Ok