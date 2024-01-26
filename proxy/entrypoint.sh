#!/bin/bash

echo "Environment Variables"
printenv

echo "booting up"
# need to create the local cert.
# this certificate is for localhost, since it will be used
# by devs to access the "server"
apt-get install -y ca-certificates openssl
echo "apt-get complete"

if [ ! -f /ssl/proxy.crt ] 
then
    echo "creating ssl file"
    openssl req \
    -newkey rsa:4096 \
    -x509 -sha256 \
    -days 365 \
    -nodes \
    -out /ssl/proxy.crt \
    -keyout /ssl/proxy.key \
    -subj="/C=${COUNTRYCODE}/ST=${STATE}/L=${LOCATION}/CN=localhost"
fi
echo "ssl file complete"

# need to install the local cert.
cp /ssl/proxy.crt /usr/local/share/ca-certificates/proxy.crt
update-ca-certificates
echo "ca-certs updated"



nginx -g "daemon off;"