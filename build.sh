#!/usr/bin/env bash
dotnet restore && dotnet build 
dotnet test
