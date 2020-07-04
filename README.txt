To pack project, navigate to the project path and execute command:
dotnet pack -c Release -p:PackageVersion=version-number

To publish project, navigate to the bin\{config} path and execute command:
dotnet nuget push {pack-name}.nupkg -s https://{myserver-url}/nuget

see more in: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-pack