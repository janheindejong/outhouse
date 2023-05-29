#!/bin/bash

set -e

TAG=$(git rev-parse --short HEAD) 
REPO="janheindejong/outhouse-api"

poetry export -o requirements.txt
docker build --tag "$REPO:$TAG" . 
rm requirements.txt

cat << EOF 
Successfully built image $REPO:$TAG 
You run a container based off the image locally with the following command (PowerShell):  

    docker run \`
    -it --rm \`
    -p 8000:8000 \`
    --mount type=bind,source="\$(pwd)/data",target=/app/data \`
    $REPO:$TAG

...or for posix: 

    docker run \\
    -it --rm \\
    -p 8000:8000 \\
    --mount type=bind,source="\$(pwd)/data",target=/app/data \\
    $REPO:$TAG
EOF
