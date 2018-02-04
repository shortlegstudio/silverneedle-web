#!/bin/bash
UNITY=/Applications/Unity/Unity.app/Contents/MacOS/Unity
PROJECT_PATH=./silverneedle-package-project
rm -rf temp-unity-package
mkdir temp-unity-package
cd temp-unity-package

git clone https://github.com/shortlegstudio/silverneedle-web.git
git clone https://github.com/tredfern/Handlebars.Net.git
git clone --branch net20 https://github.com/aaubry/YamlDotNet.git

$UNITY -quit -batchmode -createproject $PROJECT_PATH

cp -r silverneedle-web/silverneedle $PROJECT_PATH/Assets/silverneedle
cp -r Handlebars.Net/source/Handlebars $PROJECT_PATH/Assets/Handlebars
cp -r YamlDotNet/YamlDotNet $PROJECT_PATH/Assets/YamlDotNet

gsed -i -e 's/scriptingDefineSymbols: {}/scriptingDefineSymbols:\n    0:\n    1: SILVERNEEDLE_UNITY/g' $PROJECT_PATH/ProjectSettings/ProjectSettings.asset
gsed -i -e 's/scriptingRuntimeVersion: 0/scriptingRuntimeVersion: 1/g' $PROJECT_PATH/ProjectSettings/ProjectSettings.asset

$UNITY -quit -batchmode -projectPath=$PROJECT_PATH -exportPackage Assets/silverneedle Assets/Handlebars Assets/YamlDotNet silverneedle-package.unitypackage