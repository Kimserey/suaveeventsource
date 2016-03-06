open Suave
open Suave.Operators
open Suave.Filters
open Suave.EventSource
open Suave.Files
open Suave.Sockets.Control


let app = 
    choose [ 
        GET >=> choose [
            path "/events" >=> request (fun r -> EventSource.handShake(fun out ->
                socket {
                    let msg = { id = "1"; data = "First Message"; ``type`` = None }
                    do! msg |> send out
                }))
            browseHome 
        ]
    ]

[<EntryPoint>]
let main argv = 
    startWebServer { defaultConfig with homeFolder = Some __SOURCE_DIRECTORY__ } app
    0
