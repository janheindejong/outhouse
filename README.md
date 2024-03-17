# Outhouse

Fun for the whole family...

## Domain model

The domain model of the application looks something like this: 

```mermaid 
classDiagram

class Outhouse {
   +Guid Id
   +string Name 
   +ICollection<Member> Members
   +ICollection<Booking> Bookings
}

class Member {
   +Guid Id 
   +string Name 
   +string Email
   +Role Role
}

class Booking {
    +Guid Id
    +Date FromDate
    +Date ToDate
    +string Email
    +bool IsApproved
}

class User {
    +string Email
    +string Password
}

Outhouse *-- "1..*" Member
Outhouse *-- "0..*" Booking
```

There is a weak coupling between the `User.Email` field, and the `Email` field in the bookings and membership. This means you can add bookings and memberships to outhouses based on an e-mail address, even if that user does not have an account yet. Let's see how this develops over time. 

Members can view, or modify an outhouse, and add or remove members and bookings, depening on their role (`Owner`, `Admin` or `Member`). 

## Architecture

The app uses ASP.NET to serve a React client-side SPA and a server-side API. The app connects to an MS SQL Server database. The back-end uses an Onion architecture pattern, somewhat akin to [this example](https://code-maze.com/onion-architecture-in-aspnetcore/). We define the following layers: 

- **Domain**
- **Service**
- **Infrastructure**
- **Presentation**

## Setup development environment

To develop, you need to have .NET 8, and node.js.

Local development requires an MS SQL Server to be reachable on localhost:1443. The easiest cross-platform way to do this is by running it in a Docker container, like so:

```PowerShell
docker run `
    --name mssql `
    -e "ACCEPT_EULA=Y" `
    -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" `
    -p 1433:1433 `
    -d `
    mcr.microsoft.com/mssql/server:2022-latest
```

...or on MacOS/Linux

```sh
docker run \
    --name mssql \
    -e "ACCEPT_EULA=Y" \
    -e "MSSQL_SA_PASSWORD=yourStrong(\!)Password" \
    -p 1433:1433 \
    -d \
    mcr.microsoft.com/mssql/server:2022-latest
```

This creates, runs and exposes a container with MS SQL Server running in it. Note that when you close your PC, the container is stopped but not removed. You can restart it subsequently by running `docker start mssql`.

Now you should be able to run the app with the following command: 

```sh
dotnet run --project ./OutHouse.Server/OutHouse.Server.csproj
```

This will launch both back-end, and the front-end in a seperate development server, with hot reloading. 

## Migrations and seed data

We're developing with a "code-first" approach, which means the models are defined in the code, and migrations are created from there (as opposed to defining them in a *.EDMX file). 

At startup, the migrations are applied to a database called `OuthouseLocalDb`, which includes seed data comprising 4 users, 1 outhouse and 3 members. The users are

- `owner@outhouse.com`
- `admin@outhouse.com`
- `member@outhouse.com`
- `guest@outhouse.com`

All have the password `Test123!`. 

If you have migration issues running locally, delete your MSSQL Server container by running `docker rm mssql`, and recreate it as stated above. 

## API 

The API has the following endpoints: 

Identity: 
- `POST api/register` - Create new user
- `PUT api/login` - Login user 

We use Microsoft ASP.NET Core Identity to manage user information, see [documentation](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0).

MeController: 
- `GET api/me` - Returns user information
- `GET api/me/outhouses` - Returns list of houses user is member of
- `GET api/me/bookings` - Returns list of all bookings of user (NOT IMPLEMENTED YET)

OuthouseController
- `POST api/outhouse` - create new outhouse
- `GET,DELETE api/outhouses/{id}` - get or delete outhouse

OuthouseMemberController
- `GET,POST api/outhouses/{id}/members` - get members of house, or create new
- `DELETE api/outhouses/{id}/members/{id}` - delete member

OuthouseBookingController (NOT IMPLEMENTED YET)
- `GET,POST api/outhouses/{id}/bookings` - get bookings of house, or create new
- `PUT,DELETE api/outhouses/{id}/bookings/{id}` - modify or delete booking
