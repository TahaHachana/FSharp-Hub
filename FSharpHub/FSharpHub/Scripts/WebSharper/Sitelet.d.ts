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
                (): __ABBREV.__Html.IPagelet;
            };
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
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
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module Snippets {
        module Client {
            var li : {
                (link: string, title: string, description: string): __ABBREV.__Html.Element;
            };
            var dispalySnippets : {
                (arr: any[], elt: __ABBREV.__Html.Element): void;
            };
            var main : {
                (): __ABBREV.__Html.Element;
            };
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module Questions {
        module Client {
            var li : {
                (link: string, title: string, date: string, website: string, summary: string): __ABBREV.__Html.Element;
            };
            var displayQuestions : {
                (arr: any[], elt: __ABBREV.__Html.Element): void;
            };
            var main : {
                (): __ABBREV.__Html.Element;
            };
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module Login {
        module Client {
            var loginForm : {
                (redirectUrl: string): __ABBREV.__Html.Element;
            };
            var passInput : {
                (): __ABBREV.__Html.Element;
            };
        }
        interface LoginInfo {
            Name: string;
            Password: string;
        }
        interface Access {
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module BooksAdmin {
        module Client {
            var tr : {
                (title: string, publisher: string): __ABBREV.__Html.Element;
            };
            var main : {
                (): __ABBREV.__Html.Element;
            };
            var addFormlet : {
                (): __ABBREV.__Html.IPagelet;
            };
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module Records {
        interface Question {
            _id: any;
            Link: string;
            Title: string;
            Date: __ABBREV.__WebSharper.DateTimeProxy;
            Website: string;
            Summary: string;
        }
        interface Snippet {
            _id: any;
            Link: string;
            Title: string;
            Description: string;
            Date: __ABBREV.__WebSharper.DateTimeProxy;
        }
        interface Video {
            _id: any;
            Title: string;
            Url: string;
            Thumbnail: string;
            Website: string;
            Date: __ABBREV.__WebSharper.DateTimeProxy;
        }
        interface Book {
            _id: any;
            Url: string;
            Title: string;
            Authors: string[];
            Publisher: string;
            ISBN: string;
            Pages: number;
            ReleaseDate: __ABBREV.__WebSharper.DateTimeProxy;
            Cover: string;
        }
    }
    module Skin {
        interface Page {
            Title: string;
            MetaDesc: string;
            Body: any;
        }
    }
}
declare module __ABBREV {
    
    export import __Html = IntelliFactory.WebSharper.Html;
    export import __WebSharper = IntelliFactory.WebSharper;
}
