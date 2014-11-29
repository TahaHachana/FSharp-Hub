module internal Website.GitHub

open System.Configuration
open Octokit
open Octokit.Internal

let login = Credentials.gitHubLogin
let password = Credentials.gitHubPassword
let credentials = Credentials(login, password)
let credStore = InMemoryCredentialStore(credentials)

let client = new GitHubClient(new ProductHeaderValue("FSharpHub"), credStore)

