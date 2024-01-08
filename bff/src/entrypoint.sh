#!/bin/bash

echo "booting up"
# need to create the local cert.
apt-get install -y ca-certificates
echo "apt-get complete"

# if the api cert file exists in the attached volume and not in the proper place
if [ -f /ssl/api.crt ] && [ ! -f /usr/local/share/ca-certificates/api.crt ]
then
    # need to install the local cert.
    cp /ssl/api.crt /usr/local/share/ca-certificates
    update-ca-certificates
    echo "ca-certs updated"
fi

# run the app
dotnet watch run --project /app/BFF.csproj