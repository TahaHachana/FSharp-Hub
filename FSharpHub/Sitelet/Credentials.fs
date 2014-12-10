module Sitelet.Credentials

open System.Configuration

let connectionString = ConfigurationManager.ConnectionStrings.["Mongo"].ConnectionString 

let appSettings = ConfigurationManager.AppSettings

let password = appSettings.Get "Password"
let consumerKey =  appSettings.Get "ConsumerKey"
let consumerSecret = appSettings.Get "ConsumerSecret"
let accessToken = appSettings.Get "AccessToken"
let accessTokenSecret = appSettings.Get "AccessTokenSecret"
let stackExchangeKey = appSettings.Get "StackExchangeKey"
let gitHubLogin = appSettings.Get "GitHubLogin"
let gitHubPassword = appSettings.Get "GitHubPassword"