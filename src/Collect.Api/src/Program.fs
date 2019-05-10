// Learn more about F# at http://fsharp.org

open Suave
open NameDoodle.Collect.Api.Rest
open NameDoodle.Collect.Gateway.Memory
open Suave.Swagger.Swagger
open Suave.Swagger.FunnyDsl

let publisherRestApi =
    {
        RestFul.GetAll = PublisherDb.getAll
        RestFul.Create = PublisherDb.add
    }

let publisherWebPart =
    RestFul.rest "publisher" publisherRestApi

// TODO not fully compatible with dotnet core 2.2
let publisherApiDocumentation =
    swagger {
        for route in getting (simpleUrl "/publisher" |> thenReturns publisherWebPart) do
            yield description Of route is "publisher"
    }

[<EntryPoint>]
let main argv =
    startWebServer defaultConfig publisherWebPart
    0 // return an integer exit code