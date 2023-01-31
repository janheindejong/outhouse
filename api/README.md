# Outhouse API

Very basic back-end API, for now it just handles users and calender end-points. 

## Structure 

The configuration of the API is found in [`outhouse/routers`](./outhouse/routers/). Each top-level end-point has its own `*.py` file, containing the definition of the end-point. 

## Developing 

Development requires Python, Poetry and the `poethepoet` extension for Poetry. 

```
poetry install  # Creates venv and makes dependencies
poetry poe format  # Performs auto-formatting 
poetry poe test  # Runs tests 
poetry poe serve  # Serves the app
```

A debug launcher is also configured for VSCode - simply hit F5. 

## Building and pushing 

No automated CI yet, just building and pushing yourself. To build and run on your machine, do: 

```
$VERSION = git rev-parse --short HEAD
docker build --tag "janheindejong/outhouse-api:$VERSION" .
docker run -it --rm -p 8000:8000 --mount type=bind,source="$(pwd)/data",target=/app/data "janheindejong/outhouse-api:$VERSION"
```

To build and push for usage, run:

``` 
$VERSION = git rev-parse --short HEAD
docker build --tag "janheindejong/outhouse-api:$VERSION-arm64" --platform arm64 .
docker push "janheindejong/outhouse-api:$VERSION-arm64"
```
