#!/bin/bash

echo "Environment Variables"
printenv

echo "booting up"
# need to create the local cert.
apt-get install -y ca-certificates
echo "apt-get complete"

# if the api cert file exists in the attached volume and not in the proper place
if [ -f /ssl/nsw.crt ] && [ ! -f /usr/local/share/ca-certificates/nsw.crt ]
then
    # need to install the local cert.
    cp /ssl/nsw.crt /usr/local/share/ca-certificates
    update-ca-certificates
    echo "ca-certs updated"
fi

# run the app
dotnet watch run --project /app/idp/src/NSW_IDP.csproj