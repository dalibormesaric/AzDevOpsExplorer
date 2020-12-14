namespace Domain

open System
open Common.Json
open Common.OSPlatformUtils
open Common.ProcessUtils

module Release =
    type Release = {
        CreatedOn: DateTime
        Name: string
    }

    let GetReleaseList organization project =
        let command = [| "az"; "pipelines"; "release"; "list"; "--org"; organization; "-p"; project |]
        let output = execSync (getOSPlatformCmd command) (getOSPlatformArgs command) None
        match output with
            | Ok (_, value) ->
                Some (getStdOutAsString value |> deserialize<Release[]>)
            | _ -> None
