declare module FSharpHub {
    module Data {
        module GitHubRepos {
            interface Repo {
                ownerLink: string;
                ownerAvatar: string;
                name: string;
                link: string;
                createdAt: string;
                pushedAt: string;
            }
        }
        module GitHubGists {
            interface Gist {
                ownerAvatar: string;
                ownerName: string;
                ownerLink: string;
                link: string;
            }
        }
        module StackOverflow {
            interface Question {
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
    }
}
declare module __ABBREV {
    
    export import __WebSharper = IntelliFactory.WebSharper;
}
