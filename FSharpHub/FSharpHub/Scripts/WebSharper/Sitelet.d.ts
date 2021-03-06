declare module Sitelet {
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
    module GoogleGroup {
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
        var itemLi : {
            <_M1>(item: _M1): __ABBREV.__Html.Element;
        };
        var main : {
            (): __ABBREV.__Html.Element;
        };
    }
    module FSSnip {
        module Client {
            var main : {
                (): __ABBREV.__Html.Element;
            };
        }
        interface Snippet {
            author: string;
            description: string;
            likes: number;
            link: string;
            published: string;
            title: string;
        }
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
    }
    module Msdn {
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
        var main : {
            (): __ABBREV.__Html.Element;
        };
    }
    module FPish {
        interface Control {
            get_Body(): __ABBREV.__Html.IPagelet;
        }
        var itemLi : {
            <_M1>(item: _M1): __ABBREV.__Html.Element;
        };
        var main : {
            (): __ABBREV.__Html.Element;
        };
    }
    module News {
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
            stargazers: number;
            forks: number;
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
    module JsUtils {
        var hideProress : {
            (): void;
        };
        var displayFeed : {
            <_M1>(url: string, itemLi: {
                (x: _M1): __ABBREV.__Html.Element;
            }, elt: __ABBREV.__Html.Element): void;
        };
        var itemLi : {
            <_M1>(item: _M1): __ABBREV.__Html.Element;
        };
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
