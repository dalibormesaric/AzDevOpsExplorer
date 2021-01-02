namespace Domain

open System
open Common.Json
open CliWrap
open CliWrap.Buffered

module Release =
    type Release = {
        CreatedOn: DateTime
        Name: string
    }

    let GetReleaseList organization project =
        let command = "az"
        let arguments = $"pipelines release list --org {organization} -p {project}"
        let output =
            Cli.Wrap(command).WithArguments(arguments).ExecuteBufferedAsync().Task
            |> Async.AwaitTask
            |> Async.RunSynchronously            
        match output.ExitCode with
            | 0 ->
                Some (output.StandardOutput |> deserialize<Release[]>)
            | _ -> None