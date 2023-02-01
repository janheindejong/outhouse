TAG=$(git rev-parse --short HEAD) &&
docker build --tag "janheindejong/outhouse-api:$TAG" . &&
cat << EOF 
Successfully built image janheindejong/outhouse-api:$TAG 
You run a container based off the image locally with the following command (PowerShell):  

    docker run \`
    -it --rm \`
    -p 8000:8000 \`
    --mount type=bind,source="\$(pwd)/data",target=/app/data \`
    janheindejong/outhouse-api:$TAG

...or for posix: 

    docker run \\
    -it --rm \\
    -p 8000:8000 \\
    --mount type=bind,source="\$(pwd)/data",target=/app/data \\
    janheindejong/outhouse-api:$TAG
EOF
