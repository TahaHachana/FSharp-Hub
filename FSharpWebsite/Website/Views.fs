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
            <| fun ctx -> Home.body

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