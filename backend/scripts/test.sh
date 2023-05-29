#!/bin/bash 

set -e 

ruff check . 
black --check .
mypy .
pytest .

