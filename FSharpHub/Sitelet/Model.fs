module Website.Model

type PageId = int

type Action =
    | Admin
    | Books
    | BooksAdmin
    | Error
    | Home
    | Login of Action option
    | Logout
    | [<CompiledName("videos")>] Videos of PageId
    | VideosAdmin
    | Rss