namespace Website

module Content =

    open IntelliFactory.WebSharper
    open IntelliFactory.Html
    open IntelliFactory.WebSharper.Sitelets
    open IntelliFactory.WebSharper.Sitelets.Content
    open Utilities.Server

    module Shared =
        
        let navigation : HtmlElement = navigation None

        let ( => ) anchor href = A [HRef href] -< [Text anchor]

        let footer : HtmlElement =
            HTML5.Footer [Id "footer"] -< [
                Div [Class "container"; Style "padding-top: 20px;"] -< [
                    P [Text "Powered by "] -< [A ["WebSharper" => "http://www.websharper.com/"]]
                ]            
            ]

        let randomizeUrl url = url + "?d=" + System.Uri.EscapeUriString (System.DateTime.Now.ToString())

        let loginInfo logoutAction loginAction (ctx: Context<_>) =
            let user = UserSession.GetLoggedInUser ()
            let link =
                match user with
                    | Some username -> "Log Out (" + username + ")" => (randomizeUrl <| ctx.Link logoutAction)
                    | None -> "Login" => (ctx.Link <| loginAction None)
            Div [Class "pull-right"] -< [link]

        let ga : HtmlElement = Script [Src "/Scripts/ga.js"]

    module Home =
    
        let title = "FSharp Programming Language"

        let metaDescription = "Latest news, tweets and questions about the F# programming language."

        let navigation : HtmlElement = navigation <| Some "Home"

        let definition : HtmlElement =
                P [Id "definition"] -< [
                    Strong [Text "FSharp"]
                    Text " is an advanced, multi-paradigm, strongly typed open source programming language.
                        F# allows you to solve complex problems with simple, accurate and maintainable code
                        and to be more productive thanks to features like functions as values, type inference, 
                        pattern matching, computation expressions ... It's a general purpose language that you can
                        use to build desktop, Web and mobile applications and to perform cloud computations."
                ]

        let heroUnit : HtmlElement =
            Div [Id "hero"; Class "hero-unit"] -< [
                Div [Class "container text-center"] -< [
                    H1 [Text "FSharp Programming Language"]
                    definition
                ]
            ]

        let row1 : HtmlElement =
            Div [Class "row-fluid"; Style "margin-bottom: 30px;"] -< [
                Div [Class "span4 offset4"] -< [
                    A [Class "btn btn-primary btn-large pull-left home-btn"; HRef "http://www.tryfsharp.org/"; Rel "nofollow"] -< [Text "Try F#"] 
                    A [Class "btn btn-success btn-large pull-right home-btn"; HRef "/resources"] -< [Text "Download F#"] 
                ]
                Div [Class "span4"] -< [new AddThis.Control()]
            ]

        let row2() : HtmlElement =
            Div [Class "row-fluid"; Style "margin-top: 50px;"] -< [
                Div [Class "span6"] -< [
                    Div [Class "tabbable"] -< [
                        UL [Class "nav nav-tabs"] -< [
                            LI [Class "active"] -< [A [HRef "#tweets"; HTML5.Data "toggle" "tab"] -< [Text "Tweets"]]
                            LI [A [HRef "#questions"; HTML5.Data "toggle" "tab"] -< [Text "Questions"]]
                            LI [A [HRef "#snippets"; HTML5.Data "toggle" "tab"] -< [Text "Snippets"]]
                        ]
                        Div [Class "tab-content"] -< [
                            Div [Class "tab-pane active"; Id "tweets"] -< [new Tweets.Client.Control()]
                            Div [Class "tab-pane"; Id "questions"] -< [new Questions.Client.Control()]
                            Div [Class "tab-pane"; Id "snippets"] -< [new Snippets.Client.Control()]
                        ]
                    ]
                ]
                Div [Class "span6"] -< [
                    H3 [Class "centered"] -< [Text "Latest News"]
                    Div [News.Server.newsList()]
                ]
            ]

    module Books =

        let title = "FSharp Books - F#"
        let metaDescription = "Books about FSharp published by Apress, O'Reilly and Manning."

        let navigation : HtmlElement = navigation <| Some "Books"

        let header : HtmlElement =
            header
                "FSharp Books"
                "Learn F# and explore advanced topics by reading books by experts
                from Microsoft and the language community."

    module Videos =

        let title pageId = sprintf "FSharp Videos - Page %d" pageId
        let metaDescription = "Videos about F# available on YouTube, Vimeo, SkillsMatter ..."

        let navigation : HtmlElement = navigation <| Some "Videos"

        let header : HtmlElement =
            header
                "FSharp Videos"
                "Watch F# presentations, tutorials, podcasts and short videos."

    module Resources =

        let title = "FSharp Resources"
        let metaDescription = "F# downloads, user groups, mailing lists, code samples and forums."

        let navigation : HtmlElement = navigation <| Some "Resources"

        let header : HtmlElement =
            header
                "FSharp Resources"
                "Download F# IDEs and connect with the community through forums, mailing lists and user groups."

        let downloadsTab : HtmlElement =
            Div [
                H2 [Text "IDEs"]
                UL [Class "unstyled"] -< [
                    LI [A [HRef "http://www.cloudsharper.com/"] -< [Text "CloudSharper"]]
                    LI [A [HRef "http://www.tsunami.io/"] -< [Text "Tsunami IDE"]]
                    LI [
                        A [HRef "http://www.microsoft.com/visualstudio/eng/downloads"]
                            -< [Text "Visual Studio 2012"]
                    ]
                    LI [
                        A [HRef "http://www.microsoft.com/web/gallery/install.aspx?appid=FSharpVWD11"]
                            -< [Text "F# tools for Visual Studio Express 2012 for Web"]
                    ]
                ]
                HR []
                H2 [Text "Language Specification"]
                H3 [Text "F# 3.0"]
                UL [Class "unstyled"] -< [
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec.html"]
                            -< [Text "HTML"]
                    ]                
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec.pdf"]
                            -< [Text "PDF"]
                    ]                
                ]
                H3 [Text "F# 2.0"]
                UL [Class "unstyled"] -< [
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec-2.0-final.html"]
                            -< [Text "HTML"]
                    ]                
                    LI [
                        A [HRef "http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec-2.0-final.pdf"]
                            -< [Text "PDF"]
                    ]                
                ]
            ]

        let mailingListsTab =
            UL [Class "unstyled"] -< [
                LI [
                    A [HRef "http://groups.google.com/group/fsharp-opensource"]
                        -< [Text "FSharp Open Source Community"]
                ]            
                LI [
                    A [HRef "http://groups.google.com/group/fsharpMake"]
                        -< [Text "FAKE - F# Make"]
                ]            
                LI [
                    A [HRef "http://groups.google.com/group/websharper"]
                        -< [Text "WebSharper"]
                ]            
            ]

        let codeSamplesTab =
            UL [Class "unstyled"] -< [
                LI [
                    A [HRef "http://fsharp3sample.codeplex.com/"]
                        -< [Text "F# 3.0 Sample Pack"]
                ]            
                LI [
                    A [HRef "http://code.msdn.microsoft.com/site/search?f%5B0%5D.Type=ProgrammingLanguage&f%5B0%5D.Value=F%23&f%5B0%5D.Text=F%23"]
                        -< [Text "MSDN Developer Code Samples"]
                ]            
            ]

        let userGroupsTab =
            UL [Class "unstyled"] -< [
                LI [A [HRef "http://www.meetup.com/nyc-fsharp/"] -< [Text "New York City F# User Group"]]            
                LI [A [HRef "http://www.meetup.com/Chicago-F-Users/"] -< [Text "Chicago F# Users"]]            
                LI [A [HRef "http://www.meetup.com/FSharpHelsinki/"] -< [Text "The Greater Helsinki Area F# User Group"]]            
                LI [A [HRef "http://www.meetup.com/Houston-FSharp-User-Group/"] -< [Text "Houston F# User Group"]]            
                LI [A [HRef "http://www.meetup.com/FSharpLondon/"] -< [Text "F#unctional Londoners Meetup Group"]]            
                LI [A [HRef "http://www.meetup.com/New-England-F-Users-Group/"] -< [Text "New England F# Users Group"]]            
                LI [A [HRef "http://fpish.net/org/c4fs"] -< [Text "Community For F#"]]            
                LI [A [HRef "http://www.meetup.com/sfsharp/"] -< [Text "The San Francisco Bay Area F# User Group"]]            
                LI [A [HRef "http://www.meetup.com/FSharpSeattle/"] -< [Text "F# Seattle User Group"]]            
                LI [A [HRef "http://www.meetup.com/zurich-fsharp-users/"] -< [Text "Zurich FSharp Users"]]            
                LI [A [HRef "https://github.com/Nobuhisa/FSUG_JP/wiki"] -< [Text "F# User Group - Japan"]]
            ]

        let forumsTab =
            UL [Class "unstyled"] -< [
                LI [A [HRef "http://fpish.net/topics"] -< [Text "FPish"]]            
                LI [A [HRef "http://social.msdn.microsoft.com/Forums/en-US/fsharpgeneral/threads"] -< [Text "Visual F# forum"]]            
                LI [A [HRef "http://stackoverflow.com/questions/tagged/f%23"] -< [Text "StackOverflow"]]            
            ]            

        let tabs =
            Div [Class "tabbable tabs-left"] -< [
                UL [Class "nav nav-tabs"] -< [
                    LI [Class "active"] -< [A [HRef "#downloads"; HTML5.Data "toggle" "tab"] -< [Text "Downloads"]]
                    LI [A [HRef "#mailinglists"; HTML5.Data "toggle" "tab"] -< [Text "Mailing Lists"]]
                    LI [A [HRef "#codesamples"; HTML5.Data "toggle" "tab"] -< [Text "Code Samples"]]
                    LI [A [HRef "#user-groups"; HTML5.Data "toggle" "tab"] -< [Text "User Groups"]]
                    LI [A [HRef "#forums"; HTML5.Data "toggle" "tab"] -< [Text "Forums"]]
                ]
                Div [Class "tab-content"] -< [
                    Div [Class "tab-pane active"; Id "downloads"] -< [downloadsTab]
                    Div [Class "tab-pane"; Id "mailinglists"]     -< [mailingListsTab]
                    Div [Class "tab-pane"; Id "codesamples"]      -< [codeSamplesTab]
                    Div [Class "tab-pane"; Id "user-groups"]      -< [userGroupsTab]
                    Div [Class "tab-pane"; Id "forums"]           -< [forumsTab]
                ]
            ]

    module Ecosystem =

        let title = "FSharp Ecosystem"
        
        let metaDescription = "Third party F# products and services."
        
        let navigation : HtmlElement = navigation <| Some "Ecosystem"
        
        let header : HtmlElement = header "FSharp Ecosystem" "Third party F# tools, frameworks and consultancy services."

        let websharper : HtmlElement =
            Div [
                A [HRef "http://www.websharper.com/"] -< [H2 [Text "WebSharper"]]
                P [
                    Strong [Text "WebSharper"]
                    Text " is a HTML5 centric Web and mobile development framework that allows you to build an entire Web/mobile application using only the F# programming language. The client-side code is compiled to optimized JavaScript and the server-side backend runs on .NET. Among its powerful features we can mention:"
                ]
                UL [
                    LI [Text "F# to JavaScript compiler"]
                    LI [Text "Access to JavaScript libraries and TypeScript interoperability"]
                    LI [Text "Strongly-typed URLs"]
                    LI [Text "Seamless client-server AJAX communication"]
                    LI [Text "The FSharp library running on JavaScript"]
                    LI [Text "..."]
                ]
            ]

        let fcore : HtmlElement =
            Div [
                A [HRef "http://www.statfactory.co.uk/"] -< [H2 [Text "FCore"]]
                P [
                    Strong [Text "FCore"]
                    Text " is a F# numerics library by StatFactory. FCore allows you to develop numerical code in F# and get C++ performance on CPU and GPU. From the features of FCore we can highlight:"
                ]
                UL [
                    LI [Text "Intuitive F# API"]
                    LI [Text "Random Generators"]
                    LI [Text "Vector Functions"]
                    LI [Text "Descriptive Stats"]
                    LI [Text "Unlimited Memory"]
                    LI [Text "..."]
                ]
            ]

        let aleacubase : HtmlElement =
            Div [
                A [HRef "https://www.quantalea.net/"] -< [H2 [Text "Alea.cuBase"]]
                P [
                    Strong [Text "Alea.cuBase"]
                    Text " is a framework for building CUDA accelerated GPU applications on the .NET framework. Alea.cuBase features seamless integration with .NET, a solid framework, dynamic code generation and C/C++ performance."
                ]
            ]
