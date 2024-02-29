# Outhouse 

Fun for the whole family...

## Architecture 

The app uses ASP.NET to serve a React client-side SPA and a server-side API. The app connects to an MS SQL Server database. 

## Setup development environment 

To develop, you need to have .NET 8 installed, and perhaps node.js (maybe .NET installs this by itself, don't know). Furthermore, you need to have the .NET EF tools: 

```
dotnet tool install --global dotnet-ef
```

Local development and running the test project require a MS SQL Server to be reachable at localhost:1443. The easiest, cross-platform way to do this is by running it in a Docker container, like so: 

```PowerShell
docker run `
    --name mssql `
    -e "ACCEPT_EULA=Y" `
    -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" `
    -p 1433:1433 `
    -d `
    mcr.microsoft.com/mssql/server:2022-latest
```

...or 

```sh
docker run \
    --name mssql \
    -e "ACCEPT_EULA=Y" \
    -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" \
    -p 1433:1433 \
    -d \
    mcr.microsoft.com/mssql/server:2022-latest
```

You might need to apply the required migrations to the database. We're developing with a "code-first" approach, which means the models are defined in the code, and migrated from there (as opposed to defining them in a `*.EDMX` file). To do so, run: 

```PowerShell
cd .\OutHouse.Server
dotnet ef database update
```

...or 

```sh
cd ./OutHouse.Server
dotnet ef database update
```

This will ensure you have the correct tables in a database named "OutHouseDbLocal".

Now, you should be able to run the app, as follows: 

```PowerShell
cd .\OutHouse.Server
dotnet run
```

...or 

```sh
cd ./OutHouse.Server
dotnet run
```

This will launch both the front-end and the back-end separately.
