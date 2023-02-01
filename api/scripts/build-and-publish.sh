#!/bin/bash

set -e

TAG="$(git rev-parse --short HEAD)-arm64"
REPO="janheindejong/outhouse-api"
docker build --tag "$REPO:$TAG" --platform "linux/arm64" .
docker push "$REPO:$TAG"

cat << EOF 

=========================================
Hooray - successfully pushed new image!

    $REPO:$TAG

You can add it to your ops manifests now! 
=========================================
EOF
