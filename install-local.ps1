# SEO Preview Package Local Installation Script
param(
    [Parameter(Mandatory=$true)]
    [string]$TargetProjectPath,
    
    [string]$Version = "1.0.20"
)

Write-Host "Installing GingerSquirrel.SeoPreview v$Version to $TargetProjectPath" -ForegroundColor Green

# Get the current script directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$packageSource = Join-Path $scriptDir "bin\Release"

# Check if target project exists
if (-not (Test-Path $TargetProjectPath)) {
    Write-Error "Target project path does not exist: $TargetProjectPath"
    exit 1
}

# Check if package exists
$packageFile = Join-Path $packageSource "GingerSquirrel.SeoPreview.$Version.nupkg"
if (-not (Test-Path $packageFile)) {
    Write-Error "Package file not found: $packageFile"
    Write-Host "Available packages:"
    Get-ChildItem "$packageSource\*.nupkg" | ForEach-Object { Write-Host "  $($_.Name)" }
    exit 1
}

# Change to target project directory
Push-Location $TargetProjectPath

try {
    # Remove existing package if installed
    Write-Host "Removing existing package..." -ForegroundColor Yellow
    dotnet remove package GingerSquirrel.SeoPreview 2>$null

    # Install the package
    Write-Host "Installing package from local source..." -ForegroundColor Yellow
    dotnet add package GingerSquirrel.SeoPreview --version $Version --source $packageSource

    if ($LASTEXITCODE -eq 0) {
        Write-Host "Building project..." -ForegroundColor Yellow
        dotnet build
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ Installation completed successfully!" -ForegroundColor Green
            Write-Host ""
            Write-Host "Next steps:" -ForegroundColor Cyan
            Write-Host "1. Run 'dotnet run' to start your Umbraco application"
            Write-Host "2. Go to Settings > Data Types in Umbraco backoffice"
            Write-Host "3. Create a new data type with 'SEO Preview' property editor"
            Write-Host "4. Add the data type to your document types"
        } else {
            Write-Error "Build failed!"
        }
    } else {
        Write-Error "Package installation failed!"
    }
} finally {
    Pop-Location
}
