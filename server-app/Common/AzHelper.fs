// https://github.com/CompositionalIT/farmer/pull/116/files
module AzHelper

open System
open System.IO
open System.Runtime.InteropServices

let (|OperatingSystem|_|) platform () =
    if RuntimeInformation.IsOSPlatform platform then Some() else None

let azCommand =
    match () with
    | OperatingSystem OSPlatform.Windows ->
        Environment.GetEnvironmentVariable "PATH"
        |> fun s -> s.Split Path.PathSeparator
        |> Seq.map (fun s -> Path.Combine(s, "az.cmd"))
        |> Seq.tryFind File.Exists
        |> function Some s -> s | None -> invalidOp "Can't find Azure CLI"
    | OperatingSystem OSPlatform.Linux
    | OperatingSystem OSPlatform.OSX ->
        "az"
    | _ ->
        failwithf "OSPlatform: %s not supported" RuntimeInformation.OSDescription