module Website.Credentials

open System.Configuration

let connectionString = ConfigurationManager.ConnectionStrings.["Mongo"].ConnectionString 
let password = ConfigurationManager.AppSettings.Get "Password"
let consumerKey =  ConfigurationManager.AppSettings.Get "ConsumerKey"
let consumerSecret = ConfigurationManager.AppSettings.Get "ConsumerSecret"
let accessToken = ConfigurationManager.AppSettings.Get "AccessToken"
let accessTokenSecret = ConfigurationManager.AppSettings.Get "AccessTokenSecret"
let stackExchangeKey = ConfigurationManager.AppSettings.Get "StackExchangeKey"
let gitHubLogin = ConfigurationManager.AppSettings.Get "GitHubLogin"
let gitHubPassword = ConfigurationManager.AppSettings.Get "GitHubPassword"