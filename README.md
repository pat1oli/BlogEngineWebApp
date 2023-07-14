# Project to create an administrative interface for blog posts using a REST API
The aim of this project is to build an administrative interface for creating blog posts and a REST API for consulting them. The programming language used is C# with the .NET Framework or .NET Core. The database used is Microsoft SQL Server.

## Prerequisites
Before getting started, make sure you have the following installed:
- .NET (version x.x.x ou supérieure)
- Microsoft SQL Server (version x.x ou supérieure)

## Installation
1. Clone this repository to your local directory:

```
git clone https://github.com/pat1oli/BlogEngineWebApp.git
```
2. Navigate to the project directory:
```
cd your-project
```
3. Restore project dependencies:
```
dotnet restore
```
4. Configure the database:

 - Ensure that SQL Server is running.
 - Create a new database for the project.
 - Update the connection string in the appsettings.json file with your database connection information:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=<server>;Database=<database>;User Id=<user>;Password=<password>;"
}
```

5. Start the application
```
dotnet run
```

<sub>***NB: A swagger is available for the documentation of API***</sub>
