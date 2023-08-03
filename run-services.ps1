dotnet build

# Define the paths to the project files
$ServerAppPath = "Silo\Silo.csproj"
$WebApiAppPath = "WebApi\WebApi.csproj"

# Function to run the .NET Core application in a separate PowerShell window
function RunDotnetAppInNewWindow($projectPath) {
    $dotnetCmd = "dotnet run --project $projectPath"
    $process = Start-Process "powershell" -ArgumentList "-NoExit", "-Command", "$dotnetCmd" -PassThru
    return $process
}

Write-Host "Starting Orleans Silo"
$siloProcess = RunDotnetAppInNewWindow $ServerAppPath

$sleepValue = 5
write-host "Sleep $sleepValue seconds"
Start-Sleep -Seconds $sleepValue

Write-Host "Starting WebApi"
$webApiProcess = RunDotnetAppInNewWindow $WebApiAppPath

# Define the Swagger endpoint URL
$swaggerUrl = "http://localhost:5188/swagger/index.html"

# Function to check if the application is running by calling the Swagger endpoint
function CheckWebApiRunning {
    try {
        # Use curl (or Invoke-RestMethod in PowerShell) to call the Swagger endpoint
        $response = curl $swaggerUrl
        if ($response -match "<title>Swagger UI</title>") {
            return $true
        } else {
            return $false
        }
    } catch {
        return $false
    }
}

$sleepValue = 3
write-host "Sleep $sleepValue seconds"
Start-Sleep -Seconds $sleepValue

Write-Host "Checking Web API Health"
$isWebApiRunning = CheckWebApiRunning

# Output the result
if ($isWebApiRunning) {
    Write-Host "Web API is running and accessible at: " -NoNewline
    Write-Host -ForegroundColor White $swaggerUrl
    Start-Process $swaggerUrl
} else {
    Write-Host "Web API is not running or not accessible."
}
