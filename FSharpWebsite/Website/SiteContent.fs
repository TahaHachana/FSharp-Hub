namespace FSharpWebsite

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets

module SiteContent =

    module SharedContent =

        let navigation : Content.HtmlElement =
                Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                    Div [Class "navbar-inner"] -< [
                        UL [Class "nav"] -< [
                            LI [A [HRef "/Home"] -< [Text "Home"]]
                            LI [A [HRef "/Books"] -< [Text "Books"]]
                            LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                            LI [A [HRef "/Resources"] -< [Text "Resources"]]
                        ]
                    ]
                    Div [Class "alert alert-info"; Id "alertDiv"] -< [
                        P [Class "centered"; Id "alertText"] -< [Text ""]
                    ]
                ]

        let forkme : Content.HtmlElement =
            A [HRef "https://github.com/TahaHachana/FSharpWebsite"; Target "_blank"] -< [
                Img [
                    Src "https://s3.amazonaws.com/github/ribbons/forkme_right_darkblue_121621.png"
                    Alt "Fork me on GitHub"
                    Id "forkme"]
            ]

        let footer : Content.HtmlElement =
            Div [Id "footer"] -< [
                HTML5.Footer [Class "footer container"] -< [
                    Text "Powered by "
                    A [HRef "http://www.websharper.com/"; Target "_blank"] -< [
                        Text "WebSharper"
                    ]
                ]
            ]

    module HomeContent =
    
        let title = "FSharp Programming Language"

        let metaDescription = ""

        let navigation : Content.HtmlElement =
                Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                    Div [Class "navbar-inner"] -< [
                        UL [Class "nav"] -< [
                            LI [Class "active"] -< [A [HRef "/"] -< [Text "Home"]]
                            LI [A [HRef "/Books"] -< [Text "Books"]]
                            LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                            LI [A [HRef "/Resources"] -< [Text "Resources"]]
                        ]
                    ]
                    Div [Class "alert alert-info"; Id "alertDiv"] -< [
                        P [Class "centered"; Id "alertText"] -< [Text ""]
                    ]
                ]

        let definition : Content.HtmlElement =
                P [
                    Strong [Text "FSharp"]
                    Text " is an advanced, multi-paradigm, strongly typed open source programming language.
                        F# allows you to solve complex problems with simple, accurate and maintainable code
                        and to be more productive thanks to features like functions as values, type inference, 
                        pattern matching, computation expressions ... It's a general purpose language that you can
                        use to build desktop, Web and mobile applications and to perform cloud computations."
                ]

        let heroUnit : Content.HtmlElement =
            Div [Class "hero-unit"] -< [
                Div [Class "container centered"] -< [
                    H1 [Text "FSharp Programming Language"]
                    definition
                ]
            ]

        let row1 : Content.HtmlElement =
            Div [Class "row-fluid"] -< [
                Div [Class "span4"] -< [
                    H2 [Class "centered"] -< [Text "Succinct"]
                    P [Text "FSharp's syntax is clean yet powerful and readable"]
                ]
                Div [Class "span4"] -< [
                    H2 [Class "centered"] -< [Text "Multi-Paradigm"]
                    P [Text "F# is a functional first language with support for object oriented and imperative programming"]
                ]
                Div [Class "span4"] -< [
                    H2 [Class "centered"] -< [Text "F# Library"]
                    P [Text "FSharp is a modern language that comes with it's own library"]
                ]
            ]

        let row2 : Content.HtmlElement =
            Div [Class "row-fluid"] -< [
                Div [Class "span4 offset4 centered"] -< [
                    Button [Class "btn btn-primary btn-large pull-left"] -< [Text "Learn More"]
                    Button [Class "btn btn-success btn-large pull-right"] -< [Text "Download F#"]
                ]
            ]

        let row3 : Content.HtmlElement =
            Div [Class "row-fluid"] -< [
                Div [Class "span4"] -< [
                    H3 [Class "centered"] -< [Text "Tweets"] :> INode<_>
                    new FSharpTweets.FsharpTweetsViewer () :> _
//                    Div [
//                        Id "fsharpTweets"
//                        Attributes.HTML5.Data "tweets-count" "0"
//                        Attributes.HTML5.Data "tweet-id" ""
//                    ] -< [new FSharpTweets.FsharpTweetsViewer ()]
                ]
                Div [Class "span4"] -< [
                    H3 [Class "centered"] -< [Text "Questions"] :> INode<_>
                    new FSharpQuestions.FsharpQuestionsViewer () :> _
//                    Div [
//                        Id "fsharpQuestions"
//                        Attributes.HTML5.Data "questions-count" "0"
//                        Attributes.HTML5.Data "question-id" ""
//                    ] -< [new FSharpQuestions.FsharpQuestionsViewer ()]
                ]
                Div [Class "span4"] -< [
                    H3 [Class "centered"] -< [Text "Snippets"] :> INode<_>
                    new FSharpSnippets.FsharpSnippetsViewer () :> _
//                    Div [
//                        Id "fsharpSnippets"
//                        Attributes.HTML5.Data "snippets-count" "0"
//                    ] -< [new FSharpSnippets.FsharpSnippetsViewer ()]
                ]
            ]

    module BooksPageContent =

        let title = ""
        let metaDescription = ""

        let navigation : Content.HtmlElement =
                Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                    Div [Class "navbar-inner"] -< [
                        UL [Class "nav"] -< [
                            LI [A [HRef "/"] -< [Text "Home"]]
                            LI [Class "active"] -< [A [HRef "/Books"] -< [Text "Books"]]
                            LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                            LI [A [HRef "/Resources"] -< [Text "Resources"]]
                        ]
                    ]
                    Div [Class "alert alert-info"; Id "alertDiv"] -< [
                        P [Class "centered"; Id "alertText"] -< [Text ""]
                    ]
                ]

        let header : Content.HtmlElement =
            Utilities.Server.makeHeader
                "FSharp Books"
                "Learn F# or tackle advanced topics by reading books by experts
                from Microsoft and the language community."

    module VideosPageContent =

        let title pageId = sprintf "FSharp Videos - Page %d" pageId
        let metaDescription = ""

        let navigation : Content.HtmlElement =
                Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                    Div [Class "navbar-inner"] -< [
                        UL [Class "nav"] -< [
                            LI [A [HRef "/"] -< [Text "Home"]]
                            LI [A [HRef "/Books"] -< [Text "Books"]]
                            LI [Class "active"] -< [A [HRef "/Videos/1"] -< [Text "Videos"]]
                            LI [A [HRef "/Resources"] -< [Text "Resources"]]
                        ]
                    ]
                    Div [Class "alert alert-info"; Id "alertDiv"] -< [
                        P [Class "centered"; Id "alertText"] -< [Text ""]
                    ]
                ]

        let header : Content.HtmlElement =
            Utilities.Server.makeHeader
                "FSharp Videos"
                "F# videos"

    module ResourcesPageContent =

        let title = "FSharp Resources"
        let metaDescription = ""

        let navigation : Content.HtmlElement =
                Div [Class "navbar navbar-fixed-top"; Id "navigation"] -< [
                    Div [Class "navbar-inner"] -< [
                        UL [Class "nav"] -< [
                            LI [A [HRef "/"] -< [Text "Home"]]
                            LI [A [HRef "/Books"] -< [Text "Books"]]
                            LI [A [HRef "/Videos/1"] -< [Text "Videos"]]
                            LI [Class "active"] -< [A [HRef "/Resources"] -< [Text "Resources"]]
                        ]
                    ]
                    Div [Class "alert alert-info"; Id "alertDiv"] -< [
                        P [Class "centered"; Id "alertText"] -< [Text ""]
                    ]
                ]

        let header : Content.HtmlElement =
            Utilities.Server.makeHeader
                "FSharp Resources"
                "F# Resources"

        let downloadsTab : Content.HtmlElement =
            Div [
                H2 [Text "IDE"]
                H3 [Text ".NET"]
                UL [Class "unstyled"] -< [
                    LI [
                        A [HRef "http://www.microsoft.com/visualstudio/eng/downloads"; Target "_blank"]
                            -< [Text "Download a trial version of a Visual Studio 2012 edition"]
                    ]
                    LI [
                        A [HRef "http://www.microsoft.com/web/gallery/install.aspx?appid=FSharpVWD11"; Target "_blank"]
                            -< [Text "Download the F# tools for Visual Studio Express 2012 for Web"]
                    ]
                ]
                H3 [Text "Mono"]
                UL [Class "unstyled"] -< [
                    LI [
                        A [HRef "http://monodevelop.com/Download"; Target "_blank"]
                            -< [Text "Download MonoDevelop"]
                    ]
                    LI [
                        A [HRef "http://fsharp.github.com/fsharpbinding/"; Target "_blank"]
                            -< [Text "Download the F# language binding for MonoDevelop"]
                    ]
                ]
                HR []
                H2 [Text "Language Specification"]
                H3 [Text "F# 3.0"]
                UL [Class "unstyled"] -< [
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec.html"; Target "_blank"]
                            -< [Text "HTML"]
                    ]                
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec.pdf"; Target "_blank"]
                            -< [Text "PDF"]
                    ]                
                ]
                H3 [Text "F# 2.0"]
                UL [Class "unstyled"] -< [
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec-2.0-final.html"; Target "_blank"]
                            -< [Text "HTML"]
                    ]                
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec-2.0-final.pdf"; Target "_blank"]
                            -< [Text "PDF"]
                    ]                
                ]
            ]

        let mailingListsTab =
            UL [Class "unstyled"] -< [
                LI [
                    A [HRef "http://groups.google.com/group/fsharp-opensource"; Target "_blank"]
                        -< [Text "FSharp Open Source Community"]
                ]            
                LI [
                    A [HRef "http://groups.google.com/group/fsharpMake"; Target "_blank"]
                        -< [Text "FAKE - F# Make"]
                ]            
                LI [
                    A [HRef "http://groups.google.com/group/websharper"; Target "_blank"]
                        -< [Text "WebSharper"]
                ]            
            ]

        let codeSamplesTab =
            UL [Class "unstyled"] -< [
                LI [
                    A [HRef "http://fsharp3sample.codeplex.com/"; Target "_blank"]
                        -< [Text "F# 3.0 Sample Pack"]
                ]            
                LI [
                    A [HRef "http://code.msdn.microsoft.com/site/search?f%5B0%5D.Type=ProgrammingLanguage&f%5B0%5D.Value=F%23&f%5B0%5D.Text=F%23"; Target "_blank"]
                        -< [Text "MSDN Developer Code Samples"]
                ]            
            ]

        let tabs =
            Div [Class "tabbable tabs-left"] -< [
                UL [Class "nav nav-tabs"] -< [
                    LI [Class "active"] -< [A [HRef "#downloads"; HTML5.Data "toggle" "tab"] -< [Text "Downloads"]]
                    LI [A [HRef "#mailinglists"; HTML5.Data "toggle" "tab"] -< [Text "Mailing Lists"]]
                    LI [A [HRef "#codesamples"; HTML5.Data "toggle" "tab"] -< [Text "Code Samples"]]
                ]
                Div [Class "tab-content"] -< [
                    Div [Class "tab-pane active"; Id "downloads"] -< [downloadsTab]
                    Div [Class "tab-pane"; Id "mailinglists"] -< [mailingListsTab]
                    Div [Class "tab-pane"; Id "codesamples"] -< [codeSamplesTab]
                ]
            ]

