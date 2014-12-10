module internal Sitelet.GitHub

open Octokit
open Octokit.Internal
open System.Configuration

let login = Credentials.gitHubLogin
let password = Credentials.gitHubPassword
let credentials = Credentials(login, password)
let credStore = InMemoryCredentialStore(credentials)

let client =
    new GitHubClient(
        ProductHeaderValue("FSharpHub"),
        credStore
    )