declare module Website {
    module Site {
        interface Website {
        }
    }
    module Model {
        interface Action {
        }
    }
    module VideosAdmin {
        module Client {
            var main : {
                (): _Html.IPagelet;
            };
        }
        interface Control {
            get_Body(): _Html.IPagelet;
        }
    }
    module Tweets {
        interface Tweet {
            Avatar: string;
            Date: string;
            Html: string;
            Id: string;
            Name: string;
            ScreenName: string;
        }
        interface SearchResult {
        }
        interface Control {
            get_Body(): _Html.IPagelet;
        }
    }
    module Snippets {
        module Client {
            var li : {
                (link: string, title: string, description: string): _Html.Element;
            };
            var dispalySnippets : {
                (arr: any[], elt: _Html.Element): void;
            };
            var main : {
                (): _Html.Element;
            };
        }
        interface Control {
            get_Body(): _Html.IPagelet;
        }
    }
    module Questions {
        module Client {
            var li : {
                (link: string, title: string, date: string, website: string, summary: string): _Html.Element;
            };
            var displayQuestions : {
                (arr: any[], elt: _Html.Element): void;
            };
            var main : {
                (): _Html.Element;
            };
        }
        interface Control {
            get_Body(): _Html.IPagelet;
        }
    }
    module Login {
        module Client {
            var loginForm : {
                (redirectUrl: string): _Html.Element;
            };
            var passInput : {
                (): _Html.Element;
            };
        }
        interface LoginInfo {
            Name: string;
            Password: string;
        }
        interface Access {
        }
        interface Control {
            get_Body(): _Html.IPagelet;
        }
    }
    module BooksAdmin {
        module Client {
            var tr : {
                (title: string, publisher: string): _Html.Element;
            };
            var main : {
                (): _Html.Element;
            };
            var addFormlet : {
                (): _Html.IPagelet;
            };
        }
        interface Control {
            get_Body(): _Html.IPagelet;
        }
    }
    module Records {
        interface Question {
            _id: any;
            Link: string;
            Title: string;
            Date: _WebSharper.DateTimeProxy;
            Website: string;
            Summary: string;
        }
        interface Snippet {
            _id: any;
            Link: string;
            Title: string;
            Description: string;
            Date: _WebSharper.DateTimeProxy;
        }
        interface Video {
            _id: any;
            Title: string;
            Url: string;
            Thumbnail: string;
            Website: string;
            Date: _WebSharper.DateTimeProxy;
        }
        interface Book {
            _id: any;
            Url: string;
            Title: string;
            Authors: string[];
            Publisher: string;
            ISBN: string;
            Pages: number;
            ReleaseDate: _WebSharper.DateTimeProxy;
            Cover: string;
        }
    }
    module Skin {
        interface Page {
            Title: string;
            MetaDesc: string;
            Body: _Html1.Element<_Web.Control>;
        }
    }
    
    import _Html = IntelliFactory.WebSharper.Html;
    import _WebSharper = IntelliFactory.WebSharper;
    import _Html1 = IntelliFactory.Html.Html;
    import _Web = IntelliFactory.WebSharper.Web;
}
