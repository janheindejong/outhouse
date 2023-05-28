# Outhouse API

Very basic back-end API, for now it just handles users and calender end-points. 

## Structure 

The configuration of the API is found in [`outhouse/api/routers`](./outhouse/api/routers/). Each top-level end-point has its own `*.py` file, containing the definition of the end-point. Schemas can be found in [`outhouse/api/schemas.py`](./outhouse/api/schemas.py). 

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

I've setup some CI scripts, and linked them to poetry through poe. They require docker, and bash. 

```
poetry poe build  # Builds an image on your machine, that you can test locally
poetry poe publish  # Runs tests, creates an ARM64 image, and pushes it to Docker Hub
```
