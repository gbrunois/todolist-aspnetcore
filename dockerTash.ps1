<#
.SYNOPSIS
Builds and runs a Docker image.
.PARAMETER DockerComposeBuild
Builds a Docker image.
.PARAMETER DockerComposeMongoBuild
Builds a Docker image MongoDB.
.PARAMETER Clean
Removes the image angularmovie-aspnetcore and kills all containers based on that image.

.EXAMPLE
C:\PS> .\dockerTask.ps1 -Build
Build a Docker image
#>

Param(
    [Parameter(Mandatory=$True,ParameterSetName="Clean")]
    [switch]$Clean
)

$projectName="angularmovie-aspnetcore"

# Kills all running containers of an image and then removes them.
function CleanAll () {
    $composeFileName = "docker-compose.yml"
    
    docker-compose -f "$composeFileName" -p $projectName down --rmi all

    $danglingImages = $(docker images -q --filter 'dangling=true')
    if (-not [String]::IsNullOrWhiteSpace($danglingImages)) {
        docker rmi -f $danglingImages
    }
}

# Builds the Docker image.
function DockerComposeBuild () {
    $composeFileName = "docker-compose.yml"

    Write-Host "Building the project."
    npm install

    Write-Host "Building the image $imageName."
    docker-compose -f $composeFileName -p $projectName build
}

# Builds the Docker image MongoDB.
function DockerComposeMongoBuild () {
    $composeFileName="docker-compose.mongo.yml" 
  
    Write-Host "Building the image MongoDB."
    docker-compose -f $composeFileName -p $projectName build
}

# Call the correct function for the parameter that was used
if($DockerComposeBuild) {
    DockerComposeBuild
}
elseif($DockerComposeUp) {
    DockerComposeUp
}
elseif($DockerComposeMongoBuild) {
    DockerComposeMongoBuild
}
elseif($DockerComposeMongoUp) {
    DockerComposMongoUp
}
elseif ($Clean) {
    CleanAll
}