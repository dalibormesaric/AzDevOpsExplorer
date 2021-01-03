open System
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open Suave.CORS
open Common.Json
open Domain
open Argu

let projectsRoute organization =
    match Project.GetProjectList organization with
    | Some s -> OK (serialize s)
    | None -> BAD_REQUEST ""

let releasesRoute organization project =
    match Release.GetReleaseList organization project with
    | Some s -> OK (serialize s)
    | None -> BAD_REQUEST ""

let app organization =
    choose
        [            
            GET >=> choose
                [ path "/projects" >=> request (fun _ -> projectsRoute organization)
                  pathScan "/releases/%s" (fun (project) -> releasesRoute organization project)
                ]
                // https://github.com/msarilar/EDEngineer/blob/d7fe6b9cf593a3e2f63434c1dede3df5b6b1e09f/EDEngineer.Server/Server.fs
                >=> cors defaultCORSConfig
        ]

type CliArguments =
    | [<AltCommandLine("-o")>] Organization of name:string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
                | Organization _ -> "Specify organization url."

let getOrganizationFromArgv argv =
    let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some ConsoleColor.Red)
    let parser = ArgumentParser.Create<CliArguments>(programName = "AzDevOpsExplorer", errorHandler = errorHandler)
    let results = parser.ParseCommandLine argv
    results.GetResult Organization

[<EntryPoint>]
let main argv =    
    startWebServer defaultConfig (getOrganizationFromArgv argv |> app)
    0 // return an integer exit code