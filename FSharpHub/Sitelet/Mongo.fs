module Sitelet.Mongo

open MongoDB.Driver
open Records
open System.Globalization
open System.Linq
    
let culture = CultureInfo.CreateSpecificCulture "en-US"
CultureInfo.DefaultThreadCurrentCulture <- culture

let client = MongoClient Credentials.connectionString
let server = client.GetServer()
let db = server.GetDatabase "fsharpwebsite"
let collByName<'T> (name:string) = db.GetCollection<'T> name
    
let books = collByName<Book> "books"
let videos = collByName<Video> "videos"
        
let asQueryable (collection:MongoCollection<_>) =
    collection.FindAll()
        .AsQueryable()
        
let booksQueryable = asQueryable books
let videosQueryable = asQueryable videos

module Books =

    let all() =
        query {
            for x in booksQueryable do
                sortByDescending x.ReleaseDate
        }

    let insert book =
        let result = books.Insert book
        result.Ok

module Videos =

    let all() =
        query {
            for x in videosQueryable do
                sortByDescending x.Date
        }

    let insert video =
        let result = videos.Insert video
        result.Ok