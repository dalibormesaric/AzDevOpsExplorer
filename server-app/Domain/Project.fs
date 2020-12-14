namespace Domain

open Common.Json
open Common.OSPlatformUtils
open Common.ProcessUtils

module Project =
    type Project = {
        Name: string
    }

    type ProjectList = {
        Value: Project[]
    }
        
    let GetProjectList organization =
        let command = [| "az"; "devops"; "project"; "list"; "--org"; organization |]
        let output = execSync (getOSPlatformCmd command) (getOSPlatformArgs command) None
        match output with
            | Ok (_, value) ->
                Some (getStdOutAsString value |> deserialize).Value
            | _ -> None