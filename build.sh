#!/usr/bin/env bash
dotnet restore && dotnet build 
SILVERNEEDLE_LOG_LEVEL=ERROR dotnet test ./silverneedle-tests/silverneedle-tests.csproj
