# Outhouse API

Very basic back-end API, for now it just handles users and calender end-points. 

## Structure 

The configuration of the API is found in [`outhouse/api/routers`](./outhouse/api/routers/). Each top-level end-point has its own `*.py` file, containing the definition of the end-point. Schemas can be found in [`outhouse/api/schemas.py`](./outhouse/api/schemas.py). 

## Architecture 

On a high level, the architecture of the app consists of the four levels described by Uncle Bob in [Clean Architecture](). 


```mermaid 
flowchart TB
layer1["`**Layer 1** - Entities`"]
layer2["`**Layer 2** - Use cases`"]
layer3["`**Layer 3** - Adapters`"]
layer4["`**Layer 4** - Frameworks and drivers`"]
layer4 --> layer3 --> layer2 --> layer1
```

Dependency only flows down (e.g. from layer 4 to 3, or from 2 to 1), never up. The lowest layer consists of the entities, in our case, simply the `User` class. 

```mermaid 
classDiagram 
class User {
    +name String
    +id Int
}
```

The second layer contains the use cases. For now, this is the `UserManager`, that handles the logic of creating, reading, updating and deleting a user. It also includes an interface for a `UserDbAdapter`. This is a form of dependency inversion. Since the database driver is a level 4 object, and the database adapter a level 3 object, and we can't depend on a concrete implementation, we specific an interface, that can be implemented in higher levels. 

```mermaid
classDiagram
class User

class UserManager {
    -user_db_handler
    +create(String name) Int
    +get(Int id) User
    +update(Int id, String name)
    +delete(Int id)
}

class UserDbAdapter {
    <<interface>>
    +create(String name) Int
    +get(Int id) Map
}

UserManager --> User 
UserDbAdapter <-- UserManager
```

The third layer contains adapters - in this case the SQL adapter, that implements `UserDbAdapter`. This class is responsible for implementing the SQL specific code that changes the database format, into the format that our level 2 classes understand. Now here's a bit of a tricky part: to be able to execute this, it has to know of a database driver. Python specifies PEP249, which in theory would be enough to define the interface with the specific driver. However, I noticed it is not really stringent enough for this purpose. Therefore, we have to create our own, more strict implementation of this protocol for our use case. using the `SQLConnection` and `SQLCursor` interfaces.  

```mermaid 
classDiagram 
class SQLConnection {
    <<interface>>
    +cursor() SQLCursor
    +commit()
}

class SQLCursor {
    <<interface>>
    +lastrowid Int | Null
    +execute(String operation) SQLCursor
    +fetchone() Map
}

class UserDbAdapter {
    <<interface>>
    +create(String name) Int
    +get(Int id) Map
}

class SQLUserDbAdapter {
    -conn SQLConnection
}

SQLUserDbAdapter --|> UserDbAdapter
SQLConnection <-- SQLUserDbAdapter
SQLCursor <-- SQLUserDbAdapter
SQLConnection --> SQLCursor
```

The fourth layer contains the implementations of the web framework (i.e. `FastAPI` and `APIRouter`), and the specific implementations of `SQLConnection` and `SQLCursor` (e.g. for SQLite or MySQL).

## Developing 

Development requires Python and Poetry. With that in place, run: 

```bash
poetry install
```

...and you should be good to go. 

The `scripts` folder contains several useful scripts: 

* `build.sh` - builds the Docker image 
* `format.sh` - applies auto-formatting 
* `test.sh` - runs tests 
* `serve.sh` - serves the app

Note that these scripts should either be run using `poetry run`, or with the created venv activated. 

A debug launcher is also configured for VSCode - simply hit F5. 

## CI/CD 

I'm using GitHub Actions on this project, configured in `../.github/workflows/`. The CI/CD is limited to testing, building and pushing of a Docker image. Deploying is (for now) still a manual task. The image is pushed to `janheindejong/outhouse-api:latest` for both `arm64` and `amd64`. 
