#!/bin/bash 

set -e

black .
ruff check . --fix
