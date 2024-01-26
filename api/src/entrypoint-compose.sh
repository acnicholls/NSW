#!/bin/bash

echo "Environment Variables"
printenv

echo "booting up"
# need to create the local cert.
apt-get install -y ca-certificates openssl
echo "apt-get complete"

if [ ! -f /ssl/api.crt ] 
then
    echo "creating ssl file"
    openssl req \
    -newkey rsa:4096 \
    -x509 -sha256 \
    -days 365 \
    -nodes \
    -out /ssl/api.crt \
    -keyout /ssl/api.key \
    -subj="/C=${COUNTRYCODE}/ST=${STATE}/L=${LOCATION}/CN=api"
fi
echo "ssl file complete"

# need to install the local cert.
cp /ssl/api.crt /usr/local/share/ca-certificates/api.crt
update-ca-certificates
echo "ca-certs updated"

# run the app
dotnet watch run --project /app/api/src/NSW_Api/NSW_Api.csproj -- --launch-profile DockerCompose