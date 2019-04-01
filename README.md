# ASP.NET Core - Pet Clinic

This is the ASP.NET Core implementation of the Spring Pet Clinic project.

In order to run this project, you will need:

- .NET Core 2.2
- SQL Server OR Docker

## Database

### Launch the database from Docker

You will need a working instance of SQL Server in order to run the project. The `docker-compose.yml` file included can spawn one for you if you have Docker and Docker Compose.

1. Edit the `docker-compose.yml` file to insert your password.
2. Open a shell and navigate to the PetClinic project root.
3. Run `docker-compose up`

Your database instance should start.

### Insert the secrets

In order to run this project, you need to use the ASP.NET Core Secret Store. You can find (and change) the login information for the database in the `docker-compose.yml` file.

1. Open a shell and navigate to the PetClinic project root.
2. Run `dotnet user-secrets set "Sql:Username" "sa" -p PetClinic.Web`
3. Run `dotnet user-secrets set "Sql:Password" "<your_password>" -p PetClinic.Web`

### Run the database migration

In order to run the Database migrations, you need to use the Entity Framework Migration framework.

1. Open a shell and navigate to the PetClinic project root.
2. Run `dotnet ef database update -p PetClinic.Database -s PetClinic.Web`

### Run the database seed

The database will be seeded with data upon start when it is in Development mode. This process is indempotent should not insert duplicate data.
