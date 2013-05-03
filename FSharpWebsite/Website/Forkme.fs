namespace Website

module ForkMe =
    open IntelliFactory.WebSharper
    open IntelliFactory.WebSharper.Html

    [<JavaScript>]
    let main() =
        A [HRef "https://github.com/TahaHachana/FSharp-Hub"; Attr.Target "_blank"] -< [
            Img [
                Src "https://s3.amazonaws.com/github/ribbons/forkme_right_green_007200.png"
                Alt "Fork me on GitHub"
                Id "forkme"
            ]
        ]

    type Control() =  
        inherit Web.Control()

        [<JavaScript>]
        override __.Body = main() :> _