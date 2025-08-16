# Build and pack the GingerSquirrel SEO Preview package

Write-Host "Building client assets..." -ForegroundColor Green
Set-Location Client
npm install
npm run build
Set-Location ..

Write-Host "Building .NET project..." -ForegroundColor Green
dotnet build --configuration Release

Write-Host "Creating NuGet package..." -ForegroundColor Green
dotnet pack --configuration Release

Write-Host "Package created at: bin/Release/GingerSquirrel.SeoPreview.1.0.0.nupkg" -ForegroundColor Green
