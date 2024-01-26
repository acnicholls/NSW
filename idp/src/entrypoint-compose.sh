#!/bin/bash

echo "Environment Variables"
printenv

echo "booting up"
# need to create the local cert.
apt-get install -y ca-certificates openssl
echo "apt-get complete"

# create a cert for this container
if [ ! -f /ssl/idp.crt ] 
then
    echo "creating ssl file"
    openssl req \
    -newkey rsa:4096 \
    -x509 -sha256 \
    -days 365 \
    -nodes \
    -out /ssl/idp.crt \
    -keyout /ssl/idp.key \
    -subj="/C=${COUNTRYCODE}/ST=${STATE}/L=${LOCATION}/CN=idp"
fi
echo "ssl file complete"

# need to install the local cert.
cp /ssl/idp.crt /usr/local/share/ca-certificates/idp.crt
update-ca-certificates
echo "ca-certs updated"

# run the app
dotnet watch run --project /app/idp/src/NSW_IDP.csproj -- --launch-profile DockerCompose