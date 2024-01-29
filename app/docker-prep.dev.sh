#!/bin/bash
echo off

echo "Environment Variables"
printenv

echo "booting up"
# need to create the local cert.
apt-get install -y ca-certificates openssl
echo "apt-get complete"

# prepare a certificate for the SSL port
if [ ! -f /ssl/app.crt ] 
then
    echo "creating ssl file"
    openssl req \
    -newkey rsa:4096 \
    -x509 -sha256 \
    -days 365 \
    -nodes \
    -out /ssl/app.crt \
    -keyout /ssl/app.key \
    -subj="/C=${COUNTRYCODE}/ST=${STATE}/L=${LOCATION}/CN=app"
fi
echo "ssl file complete"

# need to install the local cert.
# putting this outside the if, since this might not be the 
# same image!
cp /ssl/*.crt /usr/local/share/ca-certificates/
update-ca-certificates
echo "ca-certs updated"

cd /app
npm install

npm start