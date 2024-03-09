This application was copied from here: https://github.com/leastprivilege/AspNetCoreSecuritySamples/tree/aspnetcore3/BFF

I have only modified it's settings to fit my environment

I have added "public" or "anonymous" controllers for specific routes that would normally require authenticating for but return required data for the application. In the webforms application this was code-behind logic, but is being retro-fitted for react/api communication rather than server processing.

the public controllers are for

**Label Text**
I used this class in the original application to convert an identifier string to either the english or japanese equivalent of something to display on-screen. There were upwards of 50 top-level identifiers then a dot "." and that was followed by the label identifier. The user was

**Post Category**
this is to allow anonymous users to pick a category of posts to view.

**Post**
this is to allow anonymous users to view posts

the private controller is

**User**
once the user has authenticated with the IDP, the built-in /signin-oidc method will create the user's authentication ticket/cookie and redirect the user back to the ui at /loggedin. this will call the /user/info route to get the user's information for the UI to authorize the user.
