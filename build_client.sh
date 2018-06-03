#!/usr/bin/env bash
set -e

cd silverneedle-asp/client
npm install --only-dev
npm install
cd semantic
gulp build
cd ..
npm run build

cd ..
cp -r client/build wwwroot