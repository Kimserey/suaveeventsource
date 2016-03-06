open Suave
open Suave.Operators
open Suave.Filters
open Suave.EventSource
open Suave.Files
open Suave.Sockets.Control

let sendMessage out =
   socket {
        let msg = { id = "1"; data = "First Message"; ``type`` = None }
        do! msg |> send out

        do! send out <| mkMessage "2" "Second message"
    } 

let app = 
    GET >=> choose [ path "/"       >=> file    (__SOURCE_DIRECTORY__ + "/index.html")
                     path "/events" >=> request (fun _ -> EventSource.handShake sendMessage) ]

[<EntryPoint>]
let main argv = 
    startWebServer defaultConfig app
    0
