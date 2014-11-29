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
    module News {
        interface Response {
            responseData: any;
        }
        interface data {
            feed: any;
        }
        interface feedDetails {
            entries: any[];
        }
        interface Entry {
            title: string;
            link: string;
            contentSnippet: string;
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
        var main : {
            (): __ABBREV.__Html.Element;
        };
    }
    module NuGet {
        interface Package {
            id: string;
            version: string;
            iconUrl: string;
            projectUrl: string;
            galleryDetailsUrl: string;
            lastUpdated: string;
            downloadCount: number;
            tags: string[];
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module GitHubRepos {
        interface Repo {
            ownerLink: string;
            ownerAvatar: string;
            name: string;
            description: string;
            link: string;
            createdAt: string;
            pushedAt: string;
        }
        interface NewReposControl {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
        interface UpdatedReposControl {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module StackOverflow {
        interface SoQuestion {
            id: number;
            link: string;
            title: string;
            creationDate: string;
            answerCount: number;
            ownerAvatar: string;
            ownerLink: string;
            tags: string[];
            score: number;
            acceptedAnswerId: __ABBREV.__WebSharper.OptionProxy<number>;
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module Twitter {
        interface Tweet {
            id: string;
            screenName: string;
            avatar: string;
            statusAsHtml: string;
            createdAt: string;
            isRetweeted: boolean;
            retweetedId: __ABBREV.__WebSharper.OptionProxy<string>;
            retweetedScreenName: __ABBREV.__WebSharper.OptionProxy<string>;
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
    module Utils {
        var truncate : {
            <_M1>(xs: __ABBREV.__WebSharper.seq<_M1>, count: number): __ABBREV.__WebSharper.seq<_M1>;
        };
        var skip : {
            <_M1>(xs: __ABBREV.__WebSharper.seq<_M1>, count: number): __ABBREV.__WebSharper.seq<_M1>;
        };
        var split : {
            <_M1>(count: number, xs: __ABBREV.__WebSharper.seq<_M1>): __ABBREV.__List.T<__ABBREV.__WebSharper.seq<_M1>>;
        };
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
    export import __List = IntelliFactory.WebSharper.List;
}
