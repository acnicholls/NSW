#!/bin/bash

echo "starting db service config"

# this will start a script and sql server at the same time
/tmp/configure-db.sh &

# start the sql server
/opt/mssql/bin/sqlservr
