namespace NameDoodle.Collect.Api.Rest

open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open Suave
open Suave.Filters
open Suave.Successful
open Suave.Operators

module RestFul =
    type RestResource<'a> = 
        {
            GetAll : unit -> 'a seq
            Create : 'a -> 'a
        }

    let JSON v =
        let jsonSerializerSettings = JsonSerializerSettings()
        jsonSerializerSettings.ContractResolver <- CamelCasePropertyNamesContractResolver()

        JsonConvert.SerializeObject(v, jsonSerializerSettings)
        |> OK
        >=> Writers.setMimeType "application/json; charset=utf-8"

    let fromJson<'a> json =
        JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a

    let getResourceFromReq<'a> (req : HttpRequest) =
        let getString (rawForm : byte[]) =
            System.Text.Encoding.UTF8.GetString(rawForm)
        req.rawForm |> getString |> fromJson<'a>

    let rest resourceName resource =
        let resourcePath = "/" + resourceName
        let getAll = warbler (fun _ -> resource.GetAll () |> JSON)
        let create = request (getResourceFromReq >> resource.Create >> JSON)
        path resourcePath >=> choose 
            [
                GET >=> getAll
                POST >=> create
            ]