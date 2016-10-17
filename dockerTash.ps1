<#

.PARAMETER Clean
Removes the image emptywebapplication and kills all containers based on that image.

.EXAMPLE
C:\PS> .\dockerTask.ps1 -Build
Build a Docker image
#>

Param(
    [Parameter(Mandatory=$True,ParameterSetName="Clean")]
    [switch]$Clean
)

# Kills all running containers of an image and then removes them.
function CleanAll () {
    $composeFileName = "docker-compose.yml"
    
    docker-compose -f "$composeFileName" -p $projectName down --rmi all

    $danglingImages = $(docker images -q --filter 'dangling=true')
    if (-not [String]::IsNullOrWhiteSpace($danglingImages)) {
        docker rmi -f $danglingImages
    }
}

# Call the correct function for the parameter that was used
if ($Clean) {
    CleanAll
}