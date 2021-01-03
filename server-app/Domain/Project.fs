namespace Domain

open Common.Json
open CliWrap
open CliWrap.Buffered
open AzHelper

module Project =
    type Project = {
        Name: string
    }

    type ProjectList = {
        Value: Project[]
    }
        
    let GetProjectList organization =
        let arguments = $"devops project list --org {organization}"
        let output = 
            Cli.Wrap(azCommand).WithArguments(arguments).ExecuteBufferedAsync().Task
            |> Async.AwaitTask
            |> Async.RunSynchronously
        match output.ExitCode with
            | 0 ->
                Some (output.StandardOutput |> deserialize).Value
            | _ -> None