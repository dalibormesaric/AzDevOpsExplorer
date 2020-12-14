module Common.OSPlatformUtils

open System.Runtime.InteropServices

let getOperatingSystem =
    if RuntimeInformation.IsOSPlatform(OSPlatform.Windows) then OSPlatform.Windows
    else OSPlatform.Linux

let getOSPlatformCmd (command: string[]) =
    match getOperatingSystem with
    | _ when getOperatingSystem = OSPlatform.Windows -> "cmd"
    | _ -> command.[0]

let getOSPlatformArgs command =
    match getOperatingSystem with
    | _ when getOperatingSystem = OSPlatform.Windows -> Array.append [| "/c" |] command
    | _ -> command.[1..]