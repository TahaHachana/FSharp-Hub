namespace Website

module Forkme =
    
    open IntelliFactory.WebSharper
    open IntelliFactory.WebSharper.Html

    [<JavaScript>]
    let ribbon() =
        A [HRef "https://github.com/TahaHachana/FSharpWebsite"] -< [
            Img [
                Src "https://s3.amazonaws.com/github/ribbons/forkme_right_green_007200.png"
                Alt "Fork me on GitHub"
                Id "forkme"
            ]
        ]

    type Viewer() =
                
        inherit Web.Control()

        [<JavaScript>]
        override __.Body = ribbon() :> _