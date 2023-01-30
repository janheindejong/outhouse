# Outhouse API

Very basic back-end API, for now it just handles users and calender end-points. 

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

No automated CI yet, just building and pushing yourself: 

``` 
$VERSION = 0.5
docker build --tag "janheindejong/outhouse-api:$VERSION-arm64" --platform arm64 .
docker push "janheindejong/outhouse-api:$VERSION-arm64"
```
