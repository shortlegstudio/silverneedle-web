#!/usr/bin/env bash
set -e

./build-client.sh

dotnet restore && dotnet build 
SILVERNEEDLE_LOG_LEVEL=ERROR dotnet test ./silverneedle-tests/silverneedle-tests.csproj
