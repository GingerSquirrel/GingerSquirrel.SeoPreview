#!/bin/bash

# Build and pack the GingerSquirrel SEO Preview package

echo "Building client assets..."
cd Client
npm install
npm run build
cd ..

echo "Building .NET project..."
dotnet build --configuration Release

echo "Creating NuGet package..."
dotnet pack --configuration Release

echo "Package created at: bin/Release/GingerSquirrel.SeoPreview.0.0.2.nupkg"
