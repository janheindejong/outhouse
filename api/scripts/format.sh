#!/bin/bash

autoflake --remove-all-unused-imports -r --in-place tests/. outhouse/.;
isort tests/. outhouse/.;
black tests/. outhouse/.;
