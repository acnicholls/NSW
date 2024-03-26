This application is a standard `dotnet new webapi` application.

I have added JWT Authentication

I have also added my own projects from the original solution. I migrated the data transfer logic to services and repositories, from the POCO classes that are still in the DataClasses project. The Info remains as is holding constant/variables that are used in multiple other classes or multiple places.

Portal has all the old UI logic that would pass objects to the DataClasses objects, which would then access the database. All the UI logic should be "replicated" in the new React solution.

## webforms to webapi conversion

in order to convert the webforms application to a webapi, i took all the data classes which had all the database logic, and I copied them fully into the services and repositories projects.

I then had 3 separate copies of each file. in the original data classes project I removed all the methods and just kept the POCO (plain-old class object). next I went to the repositories and decided how much logic to keep there in order to allow database interaction, and finally I created the services layer where most things are pass through from Controller to repo.

I lastly created the controllers where i have a private sync method that an async method will create a task of and run the task with the passed values.

current version has JWT included, but doesn't have [Authorize] tags everywhere yet. I wanted to get everything in the UI working first, since it's only in development and not yet anywhere in the web.
