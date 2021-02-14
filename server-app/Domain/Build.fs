namespace Domain

open System
open Common.Json
open CliWrap
open CliWrap.Buffered
open AzHelper

module Build =
    type Build = {
        FinishTime: DateTime
        BuildNumber: string
        Id: int
    }

    let GetBuildList organization project =
        let arguments = $"pipelines build list --org {organization} -p {project}"
        let output =
            Cli.Wrap(azCommand).WithArguments(arguments).ExecuteBufferedAsync().Task
            |> Async.AwaitTask
            |> Async.RunSynchronously
        match output.ExitCode with
            | 0 ->
                Some (output.StandardOutput |> deserialize<Build[]>)
            | _ -> None
     
    // let GetRelease organization project id =
    //     let arguments = $"pipelines release show --id {id} --org {organization} -p {project}"
    //     let output =
    //         Cli.Wrap(azCommand).WithArguments(arguments).ExecuteBufferedAsync().Task
    //         |> Async.AwaitTask
    //         |> Async.RunSynchronously
    //     match output.ExitCode with
    //         | 0 ->
    //             Some (output.StandardOutput |> deserialize<Release>)
    //         | _ -> None