#!/bin/bash

# Wait 180 seconds for SQL Server to start up by ensuring that 
# calling SQLCMD does not return an error code, which will ensure that sqlcmd is accessible
# and that system and user databases return "0" which means all databases are in an "online" state
# https://docs.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-databases-transact-sql?view=sql-server-2017 

# borrowed from here: https://github.com/microsoft/mssql-docker/blob/master/linux/preview/examples/mssql-customize/configure-db.sh

echo "starting database check and create script";

DBSTATUS=1
ERRCODE=1
i=0
IDSRVCONFIG=0
NSWDATA=0

while (([[ $ERRCODE -eq 1 ]] && [[ $(($DBSTATUS)) -eq 1 ]]) || ([[ -z "$DBSTATUS" ]] && [[ $(($ERRCODE)) -ne 0 ]]) || ([[ $ERRCODE -eq 0 ]] && [[ $(($DBSTATUS)) -ne 0 ]])) && [[ $i -lt 60 ]]; do
	i=$i+1
	DBSTATUS=$(/opt/mssql-tools/bin/sqlcmd -d master -h -1 -t 1 -U sa -P $MSSQL_SA_PASSWORD -Q "SET NOCOUNT ON; Select cast(SUM(state) as int) from sys.databases;")
	ERRCODE=$?

    echo 'dbstatus: '"$DBSTATUS"
    echo 'errcode: '"$ERRCODE"
	sleep 1
done

if [ $(($DBSTATUS)) -ne 0 ] || [ $ERRCODE -ne 0 ]; then 
	echo "SQL Server took more than 60 seconds to start up or one or more databases are not in an ONLINE state"
	exit 1
fi

echo "checking for and maybe installing project databases"

# check if the idp database exists.
IDSRVCONFIG=$(/opt/mssql-tools/bin/sqlcmd -h -1 -U sa -P $MSSQL_SA_PASSWORD -d master -Q "SET NOCOUNT ON; select cast(count(name) as int) from sys.databases where name='idsrvconfig';")
echo 'idsrvconfg:'"$IDSRVCONFIG"
# if it does not exist (count=0) then run the script to create and configure it.
if [[ $(($IDSRVCONFIG)) -eq 0 ]]; then
    echo "starting database one";
    /opt/mssql-tools/bin/sqlcmd -U sa -P $MSSQL_SA_PASSWORD -i /tmp/create_idpConfigDb.sql;
    echo "database one complete";
fi

# check if the api database exists.
NSWDATA=$(/opt/mssql-tools/bin/sqlcmd -h -1 -U sa -P $MSSQL_SA_PASSWORD -d master -Q "SET NOCOUNT ON; select cast(count(name) as int) from sys.databases where name='nswData';")
echo 'nswData:'"$NSWDATA"
# if it does not exist (count=0) then run the script to create and configure it.
if [[ $(($NSWDATA)) -eq 0 ]]; then
    echo "starting database two";
    /opt/mssql-tools/bin/sqlcmd -U sa -P $MSSQL_SA_PASSWORD -i /tmp/create_nswDataDb.sql;
    echo "database two complete";
fi
