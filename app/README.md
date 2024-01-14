## setting up NSW UI

this will run the NSW UI on the localhost machine. you will need to run the BFF/API/IDP as well, and ensure the Base URLS are in the CRA folder

### REQUIRED ENVIRONMENT VARIABLES

REACT_APP_BFF_URL=https://localhost:5005
REACT_APP_IDP_URL=https://localhost:5007
REACT_APP_PUBLIC_URL=https://localhost:3000
HTTPS=true
PORT=3000
SSL_CRT_FILE=../../ssl/cert.pem
SSL_KEY_FILE=../../ssl/key.pem

**NOTE:** when running in production all three URLs will be the same. it is only when running in development with different visual studio instances using different ports that these URLS will be like this. The /cra/src/constants/RouteConstants file contains the exact URLs for backend/frontend UI.

**NOTE:** you will need to generate your own SSL keys and put them in a top level 'ssl' folder, as they are not included in the repository.

a description regarding how to do that can be found [here](https://gist.github.com/cecilemuller/9492b848eb8fe46d462abeb26656c4f8)

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `npm test`

not yet implemented

### `npm run build`

not yet implemented

### Deployment

not yet implemented
