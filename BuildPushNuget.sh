#!/bin/bash

rm -rf AlinSpace.Database/bin/Release/*

dotnet build -c Release
dotnet nuget push AlinSpace.Database/bin/Release/AlinSpace.Database.*.nupkg --source github --skip-duplicate