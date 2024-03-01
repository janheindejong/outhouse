# Outhouse

Fun for the whole family...

## Architecture

The app uses ASP.NET to serve a React client-side SPA and a server-side API. The app connects to an MS SQL Server database.

## Setup development environment

To develop, you need to have .NET 8 installed, and node.js. Furthermore, you need to have the .NET EF tools:

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
    -e "MSSQL_SA_PASSWORD=yourStrong(\!)Password" \
    -p 1433:1433 \
    -d \
    mcr.microsoft.com/mssql/server:2022-latest
```

Once you have done this once, after closing your PC, you can simply run `docker start mssql`.

When there is an update to the datamodel, or when you are with a fresh database, you might need to apply the required migrations to the database. We're developing with a "code-first" approach, which means the models are defined in the code, and migrated from there (as opposed to defining them in a `*.EDMX` file). To do so, run:

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

## API 

The API has the following endpoints: 

Identity: 
- `POST api/register` - Create new user
- `PUT api/login` - Login user 

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0

MeController: 
- `GET api/me` - Returns user information
- `GET api/me/outhouses` - Returns list of houses user is member of
- `GET api/me/bookings` - Returns list of all bookings of user (NOT IMPLEMENTED YET)

OuthouseController
- `POST api/outhouse` - create new outhouse
- `GET,PUT api/outhouses/{id}` - modify outhouse
- `GET,POST api/outhouses/{id}/members` - get members of house, or create new
- `GET,POST api/outhouses/{id}/members/{id}` - modify member (NOT IMPLEMENTED YET)
- `GET,POST api/outhouses/{id}/bookings` - get bookings of house, or create new (NOT IMPLEMENTED YET)
- `PUT,DELETE api/outhouses/{id}/bookings/{id}` - modify booking (NOT IMPLEMENTED YET)

We use Microsoft ASP.NET Core Identity to manage user information. 


