open System
open Suave
open Suave.Successful
open Suave.Operators
open Suave.Filters
open Suave.EventSource
open Suave.Files
open Suave.Sockets.Control

let sendMessage out =
   socket {
        for i in [0..100] do
            do! Suave.Sockets.SocketOp.ofAsync (Async.Sleep 2000)
            do! send out (mkMessage (string i) (string i + " message"))
    } 

let app = 
    GET >=> choose [ path "/"       >=> file    (__SOURCE_DIRECTORY__ + "/index.html")
                     path "/events" >=> request (fun _ -> EventSource.handShake sendMessage) 
                     path "/test"   >=> OK "That worked" ]

[<EntryPoint>]
let main argv = 
    startWebServer defaultConfig app
    0
