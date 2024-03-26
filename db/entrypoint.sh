#!/bin/bash

echo "Environment Variables"
printenv

apt-get update

apt-get install -y sudo

echo "starting db service config"
# this will start the script in a background process
sudo -n -b /tmp/configure-db.sh

echo "starting db server..."
# start the sql server
/opt/mssql/bin/sqlservr
