## the NSW db

this database has 5 tables.

Post, PostCategory, PostalCode, User, LabelText.

It uses stored procedures for all CUD operations

Read operations are plain text with interpolated strings. (ripe for SQL injection), however no user input is ever accepted into read operation strings. identifiers only. Since this is now a WebApi and not a WebForms solution, it is easier for user input to make it to the database level, so parameterized queries should be employed for new read operations and a ticket will be created to convert current read operations to this.

A BaseRepository method was created to accept a string from another repository layer method and do the work of querying the database.

this database is very basic. it could use a lot of work which could improve the health of the application.

**/data**
the database docker container is mounted to this folder and puts the SQL Server database files here. the master and other databases are in here as well as the IDP Database when running on a localhost docker-compose container.

**/localData**
the idp
