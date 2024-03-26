#!/bin/bash

echo "Environment Variables"
printenv

echo "booting up"
# need to create the local cert.
apt-get install -y ca-certificates openssl
echo "apt-get complete"

# if the api cert file exists in the attached volume and not in the proper place
if [ ! -f /ssl/bff.crt ] 
then
    echo "creating ssl file"
    openssl req \
    -newkey rsa:4096 \
    -x509 -sha256 \
    -days 365 \
    -nodes \
    -out /ssl/bff.crt \
    -keyout /ssl/bff.key \
    -subj="/C=${COUNTRYCODE}/ST=${STATE}/L=${LOCATION}/CN=bff"
fi
echo "ssl file complete"

# need to install the local cert.
cp /ssl/*.crt /usr/local/share/ca-certificates/
update-ca-certificates
echo "ca-certs updated"

# run the app
dotnet run --project /app/bff/src/NSW_BFF.csproj --launch-profile Docker