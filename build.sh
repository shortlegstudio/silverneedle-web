#!/usr/bin/env bash
dotnet restore && dotnet build 
dotnet test ./silverneedle-tests/silverneedle-tests.csproj
