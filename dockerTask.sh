#!/bin/bash

projectName="angularmovie-aspnetcore"

# Kills all running containers of an image and then removes them.
cleanAll () {
    echo "Clean all"
    composeFileName="docker-compose.yml"
    docker-compose -f $composeFileName -p $projectName down --rmi all

    # Remove any dangling images (from previous builds)
    danglingImages=$(docker images -q --filter 'dangling=true')
    if [[ ! -z $danglingImages ]]; then
      docker rmi -f $danglingImages
    fi
}

# Builds the Docker image.
buildImage () {
  
    composeFileName="docker-compose.yml"
  
    echo "Building the project."
    npm install
    echo "Building the image."
    docker-compose -f $composeFileName -p $projectName build
}

# Runs docker-compose.
compose () {
  
    composeFileName="docker-compose.yml"
  
    echo "Running compose file $composeFileName"
    docker-compose -f $composeFileName -p $projectName kill
    docker-compose -f $composeFileName -p $projectName up -d
}

# Shows the usage for the script.
showUsage () {
  echo "Usage: dockerTask.sh [COMMAND]"
  echo "    Runs build or compose"
  echo ""
  echo "Example:"
  echo "    ./dockerTask.sh build debug"
  echo ""
}

if [ $# -eq 0 ]; then
  showUsage
else
  case "$1" in
    "compose")
            buildImage
            compose
            ;;
    "build")
            buildImage
            ;;
    "clean")
            cleanAll
            ;;
    *)
            showUsage
            ;;
  esac
fi