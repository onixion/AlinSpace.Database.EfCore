#!/bin/bash

rm -rf AlinSpace.Database/bin/Release/*
rm -rf AlinSpace.Database.Ef/bin/Release/*

dotnet build -c Release

dotnet nuget push AlinSpace.Database/bin/Release/AlinSpace.Database.*.nupkg --source github --skip-duplicate
dotnet nuget push AlinSpace.Database.Ef/bin/Release/AlinSpace.Database.Ef.*.nupkg --source github --skip-duplicate