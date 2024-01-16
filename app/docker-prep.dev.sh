
apt-get update 
apt-get upgrade -y

# prepare a certificate for the SSL port
if [ ! -f /ssl/cert.pem ] 
then
    echo "creating ssl file"
    openssl req \
    -newkey rsa:4096 \
    -x509 -sha256 \
    -days 365 \
    -nodes \
    -out /ssl/cert.pem \
    -keyout /ssl/key.pem \
    -subj="/C=CA/ST=Ontario/L=Waterloo/CN=nsw"
fi
echo "ssl file complete"

# need to install the local cert.
cp /ssl/cert.pem /usr/local/share/ca-certificates
update-ca-certificates
echo "ca-certs updated"

cd /app
npm install

npm start