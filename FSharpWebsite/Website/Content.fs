﻿namespace Website

module Content =

    open IntelliFactory.WebSharper
    open IntelliFactory.Html
    open IntelliFactory.WebSharper.Sitelets
    open Utilities.Server

    module Shared =
        
        let navigation : Content.HtmlElement = navigation None

        let footer : Content.HtmlElement =
            Div [Id "footer"] -< [
                HTML5.Footer [Class "container"] -< [
                    Text "Powered by "
                    A [HRef "http://www.websharper.com/"] -< [Text "WebSharper"]
                ]
            ]

        let ( => ) anchor href = A [HRef href] -< [Text anchor]
    
        let randomizeUrl url = url + "?d=" + System.Uri.EscapeUriString (System.DateTime.Now.ToString())

        let loginInfo logoutAction loginAction (ctx: Context<_>) =
            let user = UserSession.GetLoggedInUser ()
            let link =
                match user with
                    | Some username -> "Log Out (" + username + ")" => (randomizeUrl <| ctx.Link logoutAction)
                    | None -> "Login" => (ctx.Link <| loginAction None)
            Div [Class "pull-right"] -< [link]

    module Home =
    
        let title = "FSharp Programming Language"

        let metaDescription = ""

        let navigation : Content.HtmlElement = navigation <| Some "Home"

        let definition : Content.HtmlElement =
                P [Id "definition"] -< [
                    Strong [Text "FSharp"]
                    Text " is an advanced, multi-paradigm, strongly typed open source programming language.
                        F# allows you to solve complex problems with simple, accurate and maintainable code
                        and to be more productive thanks to features like functions as values, type inference, 
                        pattern matching, computation expressions ... It's a general purpose language that you can
                        use to build desktop, Web and mobile applications and to perform cloud computations."
                ]

        let heroUnit : Content.HtmlElement =
            Div [Id "hero"; Class "hero-unit"] -< [
                Div [Class "container text-center"] -< [
                    H1 [Text "FSharp Programming Language"]
                    definition
                ]
            ]

        let row1 : Content.HtmlElement =
            Div [Class "row-fluid"; Style "margin-bottom: 30px;"] -< [
                Div [Class "span4 offset4"] -< [
                    A [Style "width: 120px;"; Class "btn btn-primary btn-large pull-left"; HRef "http://www.tryfsharp.org/"] -< [Text "Try F#"] 
                    A [Style "width: 120px;"; Class "btn btn-success btn-large pull-right"; HRef "/resources"] -< [Text "Download F#"] 
                ]
            ]

        let row2 : Content.HtmlElement =
            Div [Class "row-fluid"; Style "margin-top: 50px;"] -< [
                Div [Class "span6"] -< [
                    Div [Class "tabbable"] -< [
                        UL [Class "nav nav-tabs"] -< [
                            LI [Class "active"] -< [A [HRef "#tweets"; HTML5.Data "toggle" "tab"] -< [Text "Tweets"]]
                            LI [A [HRef "#questions"; HTML5.Data "toggle" "tab"] -< [Text "Questions"]]
                            LI [A [HRef "#snippets"; HTML5.Data "toggle" "tab"] -< [Text "Snippets"]]
                        ]
                        Div [Class "tab-content"] -< [
                            Div [Class "tab-pane active"; Id "tweets"] -< [new Tweets.FsharpTweetsViewer () :> INode<_>]
                            Div [Class "tab-pane"; Id "questions"] -< [new Questions.Client.QuestionsViewer () :> INode<_>]
                            Div [Class "tab-pane"; Id "snippets"] -< [new Snippets.Client.SnippetsViewer() :> INode<_>]
                        ]
                    ]
                ]
                Div [Class "span6"] -< [
                    H3 [Class "centered"] -< [Text "Latest News"]
                    Div [new News.Client.NewsViewer()]
                ]
            ]

    module Books =

        let title = ""
        let metaDescription = ""

        let navigation : Content.HtmlElement = navigation <| Some "Books"

        let header : Content.HtmlElement =
            header
                "FSharp Books"
                "Learn F# and explore advanced topics by reading books by experts
                from Microsoft and the language community."

    module Videos =

        let title pageId = sprintf "FSharp Videos - Page %d" pageId
        let metaDescription = ""

        let navigation : Content.HtmlElement = navigation <| Some "Videos"

        let header : Content.HtmlElement =
            header
                "FSharp Videos"
                "F# videos"

    module Resources =

        let title = "FSharp Resources"
        let metaDescription = ""

        let navigation : Content.HtmlElement = navigation <| Some "Resources"

        let header : Content.HtmlElement =
            header
                "FSharp Resources"
                "F# Resources"

        let downloadsTab : Content.HtmlElement =
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
                        A [HRef "http://www.microsoft.com/web/gallery/install.aspx?appid=FSharpVWD11"; Target "_blank"]
                            -< [Text "F# tools for Visual Studio Express 2012 for Web"]
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