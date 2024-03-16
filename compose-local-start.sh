#!/bin/bash

# this compose solution will build actual containers
# "quickly" in a debug configuration
# so a developer can build and push images that will
# work, but not necessarily be permanent or clean

# and they log at the lowest level, so more information
# when things go wrong



# we want to ensure that the developer has run the docker-compose solution first
# so that things like SSL certs etc are already existing, and
# one big one is the database

# check for database files, if the don't exist, stop execution
if [ -z "./db/data/nswData.mdf" ]; then
    echo "run `compose-start.sh` first to ensure required files are created."
    exit 1
fi


docker-compose -f docker-compose.local.yml up --build 