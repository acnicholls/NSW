# NSW - Natsuko's Sharing Website

this site was commissioned in return for two tickets to Japan for my son and I. The trip was amazing. Ask me about it sometime.

## the purpose

this was to be a "Used Victoria" clone for use in an exclusive area of Japan. Nagano Prefecture. I found a list online of Nagano Prefecture Postal Codes and a way to show a map of the area of a postal code on a webpage as well as an image of something that someone wanted to share/swap.

the `master` branch is DotNet 4.0, but I have been exposed to React and I have seen the speed of modern dotnetcore applications with JS/TS UI apps written for modern browsers, and then gone to play with this and noticed how terribly slow and clunky it is. So I decided to test and portray my new found knowledge (thanks to my many past employers who allowed me to learn on the job!) and update this site. maybe even deploy it to Azure and regionalize it to Nagano and see if anyone uses it. It'll be good for my portfolio.

the `develop` branch contains a docker-compose solution that is described below.

## the docker-compose

in order to get a new dev up and running quickly at one of the companies i worked at, one of the dev-ops gyus would create a docker-compose file for the solution that would just mount the monolith repo folders to proper places on proper docker images and configure services to have everything running smoothly. this is my attempt to do that and it works in other solutions

each service has an overridden entrypoint to allow the devs to control how it starts.

- app
  has the docker-prep.dev.sh file which will run container updates prepare certs and run `npm install` before starting the container.

- api
- bff
- idp
  all have entrypoint.sh which will create an SSL cert if it doesn't already exist and then start the project with `dotnet watch` so that changes cause recompile of the binaries. they all use the same cert, so they can talk to each other without complication.

- db
  starts 2 processes at the same time, the configure script and the SQL server application. sometimes crashes after the initial configure is complete, commenting out the configure script in the entrypoint.sh file helps.

- proxy
  overwrites the entrypoint to create a `localhost` SSL cert for use in the docker container, as the production design would have only ports 80/443 exposed and pointed to the this service. this service's configuration is described more below.

**NOTE:** the `dotnet` services in this docker-compose solution all read from the same project (DataClasses in the API solution) and folder on the filesystem by mounting the same folder (project root) to their /app mountpoint, but using project files in different sub-folders, this is due to the dependency of each on the domain models in the DataClasses project. This can cause sudden restarts or crashes of the service within the container. A simple restart of the affected service (or waiting for the restart if it is caught by the compose restart in the override file and does so automatically) will solve the problem. This is a known problem and will be worked on in time.

## user experience

### step 1 - choosing your language

in order for the server to know what language to display on each page, the user was asked to choose before being able to see any content. the "Splash" page was displayed with BOTH languages and the user clicked the button that represented their choice.

### step 2 - anonymous search

the user is able to view posts, search for things and request help or information from dev/admin.

### step 3 - registration

the user is sent to a page asking for info, specifically email, phone and nagano postal code.

### step 4 - registered browsing

the user is able to add a post to the collection already displayed on the site, they are able to edit their profile details, and view a list of the posts that they have entered and make edits to those posts. they can also do what any anonymous user is able to do.

### step 5 - administration

the administrative user is able to add and/or edit new post categories and, new label texts. they are also able to perform all other user actions. this was to allow a japanese speaking person to create the text displayed in the japanese version, while the developer could create the english version.

## extras

/tools is a folder where support applications are stored. currently residing there

/tools/db_updater - is a small application to take one NSW database's LabelText table and update another's. for copying updated localhost values to a production database.

/tools/geo_coder - is a small application that will read in a list of postal codes and query a google api for the Latitude and Longitude of a "central" point within that postal code. used to feed the pin on the map in the WebForms version of the application.

# setting up the application

there are 6 parts to the application. they are meant to be run in a docker-compose configuration with load balancing and scaling

## the Database

SQL Server

the app was written in 2015 originally so any version past that. the scripts are in the /db folder.

- go into Updates and rev1.1, run the scripts there to get the database created

- the scripts in rev1.5 will update to the `develop` branch. if you want to run `master`, do not run these scripts. you need an extra database for the IdentityServer service.

### Connection strings

for development connectionstrings are stored in a collection and each service/environment has it's own named connection.

the API and IDP projects each have a UserSecrets file that contains a `ConnectionStrings` key, and then several connection strings are added to that so that the solution can just have the name of the connectionstring in the configuration file that is checked into the Github repository.

an example secrets.json file

```
{
  "ConnectionStrings": {
    "DefaultConnection": "<connection string here>",
    "DevelopmentConnection": "<connection string here>",
    "DockerConnection": "<connection string here>",
    "NSW_Development": "<connection string here>"
  }
}
```

`NSW_` connectionstrings are used for the API, the `*Connection` ones are for the IdentityServer4 IdentityModel tables. the configuration tables have not been created yet. development is using in-memory configuration.

using a connectionstring
in order to use them like this you can create a single entry in your appsetting.json file where you put the name of the connectionstring, for example, using the above secrets.json file "DevelopmentConnection". like below

```
{
  "ConnectionString": "DevelopmentConnection"
}
```

in your application code, where you want to get the connectionstring you put

`_configuration.GetConnectionString(_configuration.GetValue("ConnectionString").ToString());`

## the UI (User Interface)

Create-React-App with customization

this is only just started, it is a React UI application. it was started in another repo just to be able to get the react application to authenicate to the BFF/IDP solution inside a docker-compose solution with a dotnetcore api. `acnicholls/idsrv-dotnet-react` is the repo, but it's private so I can use it for stuff like this.

as I have worked on it and upgraded the versions of all the dependcies the code has changed slightly from the original, espcially where related to react-router. damn breaking changes.

it is basic CRA with components made for react-query, because I wanted to learn it.

more information can be found on the /app/README.md file

#### packages - still in flux

    "@coreui/coreui": "^4.3.2",
    "@hookform/resolvers": "^3.3.2",
    "@testing-library/jest-dom": "^5.14.1",
    "@testing-library/react": "^11.2.7",
    "@testing-library/user-event": "^12.8.3",
    "axios": "^1.6.2",
    "react": "^18.2.0",
    "react-bootstrap": "^2.9.1",
    "react-bootstrap-icons": "^1.10.3",
    "react-cookie": "^4.0.3",
    "react-dom": "^18.2.0",
    "react-hook-form": "^7.47.0",
    "react-query": "^3.39.3",
    "react-router": "^6.4.0",
    "react-router-dom": "^6.4.0",
    "react-scripts": "^5.0.1",
    "uuid": "^9.0.1",
    "web-vitals": "^1.1.2",
    "yup": "^1.3.2"

## the API (Application Programming Interface)

dotnetcore 7.0 WebApi with Controllers

the basic CRUD is fleshed out, but not 100% implemented, the entire API is being expanded for the actual requirements of the UI and the rest of the domain (there's some data transfer from API to BFF/IDP for validation and dropdowns, etc.)

more information can be found on the /api/README.md file

## the IDP (IDentity Provider)

dotnetcore 7.0 WebApi with Controllers and IdentityServer4 NuGet

this is the identity and access management system, IdentityServer4 with IdentityModel UI. I have only added the Register function in order to allow people to signup. The Login page has external providers showing, as Google is configured. depending on costs for provider keys, more could be added to allow for external identity management such as Facebook, Micrsoft, Google, etc. perhaps Japan has one or more that could also be integrated.

more information can be found on the /idp/README.md file

## the BFF (Backend-For-Frontend)

dotnetcore 7.0 WebApi with Controllers

this is IdentityServer4's BFF implementation, with some modifications.
It handles identity and access tokens for the UI.
there are public controllers which use a client token internally to get data that should be publicly (anonymously) accessible from the database, such as posts, post categories, and label text.
once the user has started an identity session on the BFF via the IDP Account/Login route (which gives the browser Authentication Cookies to access the user's session with) all calls to `/api` are routed from the proxy to the BFF to acquire an access token and then on to the actual private API service to collect the private data.

more information can be found on the /bff/README.md file

## the Proxy

NGINX

this controls access to the lower level systems through configuration of only certain sub-domain folders. this could be expanded out as new features of the IDP are used (devices, consent, etc.), other routes might be needed on the BFF for external identity providers.

routed to the BFF
/bff
/api
/signin-oidc
/signout-callback-oidc

routed to the IDP
/Account
/connect
/.well-known
/home
/idp

routed to the UI (could be served from the proxy)
/
/\*

more information can be found on the /proxy/README.md file
