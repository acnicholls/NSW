#!/bin/bash

echo "booting up"
# need to create the local cert.
apt-get install -y ca-certificates openssl
echo "apt-get complete"



if [ ! -f /ssl/nsw.crt ] 
then
    echo "creating ssl file"
    openssl req \
    -newkey rsa:4096 \
    -x509 -sha256 \
    -days 365 \
    -nodes \
    -out /ssl/nsw.crt \
    -keyout /ssl/nsw.key \
    -subj="/C=CA/ST=Ontario/L=Waterloo/CN=nsw"
fi
echo "ssl file complete"

# need to install the local cert.
cp /ssl/nsw.crt /usr/local/share/ca-certificates
update-ca-certificates
echo "ca-certs updated"

# run the app
dotnet watch run --project /app/NSW_Api/NSW_Api.csproj