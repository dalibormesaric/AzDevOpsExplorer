namespace Domain

open System
open Common.Json
open CliWrap
open CliWrap.Buffered
open AzHelper

module Release =
    type ReleaseArtifactDefinitionReferenceVersion = {
        Id: int
        Name: string
    }

    type ReleaseArtifactDefinitionReferenceBranches = {
        Id: string
        Name: string
    }

    type ReleaseArtifactDefinitionReference = {
        Version: ReleaseArtifactDefinitionReferenceVersion
        Branches: ReleaseArtifactDefinitionReferenceBranches
    }

    type ReleaseArtifact = {
        Alias: string
        DefinitionReference: ReleaseArtifactDefinitionReference
    }

    type ReleaseEnvironment = {
        Name: string
    }

    type Release = {
        CreatedOn: DateTime
        Name: string
        Id: int
        Artifacts: ReleaseArtifact[]
        Environments: ReleaseEnvironment[]
    }

    let GetReleaseList organization project =
        let arguments = $"pipelines release list --org {organization} -p {project}"
        let output =
            Cli.Wrap(azCommand).WithArguments(arguments).ExecuteBufferedAsync().Task
            |> Async.AwaitTask
            |> Async.RunSynchronously
        match output.ExitCode with
            | 0 ->
                Some (output.StandardOutput |> deserialize<Release[]>)
            | _ -> None
     
    let GetRelease organization project id =
        let arguments = $"pipelines release show --id {id} --org {organization} -p {project}"
        let output =
            Cli.Wrap(azCommand).WithArguments(arguments).ExecuteBufferedAsync().Task
            |> Async.AwaitTask
            |> Async.RunSynchronously
        match output.ExitCode with
            | 0 ->
                Some (output.StandardOutput |> deserialize<Release>)
            | _ -> None