﻿module Sitelet.Records

open MongoDB.Bson
open System
        
[<CLIMutable>]
type Video =
    {
        _id       : ObjectId
        Title     : string
        Url       : string
        Thumbnail : string
        Website   : string
        Date      : DateTime
    }

    static member New title url thumbnail website date =
        {
            _id       = ObjectId.GenerateNewId()
            Title     = title
            Url       = url
            Thumbnail = thumbnail
            Website   = website
            Date      = date
        }

[<CLIMutable>]
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

    static member New url title authors publisher isbn pages releaseDate cover =
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