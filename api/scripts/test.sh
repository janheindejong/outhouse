#!/bin/bash 

set -e 

mypy outhouse/. 
autoflake -r --check tests/. outhouse/. 
black --check tests/. outhouse/.
