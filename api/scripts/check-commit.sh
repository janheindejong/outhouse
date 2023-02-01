#!/bin/bash

git fetch origin 
git diff origin/main --quiet 2> /dev/null 
if [ $? -ne 0 ];
then 
    echo "It appears the current state of your workdir is not up-to-date with origin/main. See git diff origin/main."; 
    exit 1;
else
    echo "Workdir is up-to-date with origin/main";
    exit 0; 
fi
