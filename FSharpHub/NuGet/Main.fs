module NuGet.Main
    
open System
open Microsoft.FSharp.Data.TypeProviders

type NuGet = ODataService<"http://www.nuget.org/api/v2">

let context = NuGet.GetDataContext()