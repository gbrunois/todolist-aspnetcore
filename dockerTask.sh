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
dockerComposeBuild () {
  
    composeFileName="docker-compose.yml"
  
    echo "Building the project."
    npm install
    echo "Building the image $projectName."
    docker-compose -f $composeFileName -p $projectName build
}

# Builds the Docker image MongoDB.
dockerComposeMongoBuild () {
  
    composeFileName="docker-compose.mongo.yml"
  
    echo "Building the image MongoDB."
    docker-compose -f $composeFileName -p $projectName build
}

# Runs docker-compose.
dockerComposeUp () {
  
    composeFileName="docker-compose.yml"
  
    echo "Running compose file $composeFileName"
    docker-compose -f $composeFileName -p $projectName kill
    docker-compose -f $composeFileName -p $projectName up -d
}

# Runs docker-compose mongoDB only.
dockerComposeMongoUp () {
  
    composeFileName="docker-compose.mongo.yml"
  
    echo "Running compose file $composeFileName"
    docker-compose -f $composeFileName -p $projectName kill
    docker-compose -f $composeFileName -p $projectName up -d
}

# Test
testApi () {
    
}

# Shows the usage for the script.
showUsage () {
  echo "Usage: dockerTask.sh [COMMAND]"
  echo ""
  echo "Commands:"
  echo "    dockerComposeBuild: Builds a Docker image (Web server and MongoDB)."
  echo "    dockerComposeBuildMongo: Builds a Docker image (MongoDB)."
  echo "    dockerComposeUp: Runs docker-compose."
  echo "    dockerComposeMongoUp: Runs docker-compose.mongo"
  echo "    dockerComposeBuildUp: Build and run docker-compose"
  echo "    dockerComposeMongoBuildUp: Build and run docker-compose (MongoDB)"
  echo "    clean: Removes the image '$projectName' and kills all containers based on that image."
  echo "Example:"
  echo "    ./dockerTask.sh dockerComposeBuildUp"
  echo ""
}

if [ $# -eq 0 ]; then
  showUsage
else
  case "$1" in
    "dockerComposeBuild")
            dockerComposeBuild
            ;;
    "dockerComposeMongoBuild")
            dockerComposeMongoBuild
            ;;
    "dockerComposeUp")
            dockerComposeUp
            ;;
    "dockerComposeMongoUp")
            dockerComposeMongoUp
            ;;
    "dockerComposeBuildUp")
            dockerComposeBuild
            dockerComposeUp
            ;;
    "dockerComposeMongoBuildUp")
            dockerComposeMongoBuild
            dockerComposeMongoUp
            ;;
    "clean")
            cleanAll
            ;;
    "test")
            test
            ;;
    *)
            showUsage
            ;;
  esac
fi