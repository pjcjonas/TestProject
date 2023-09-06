Gaming Global - Test Application

There are 2 projects. **core-api** is the .Net Core web api solution and **gaming-global** is the React Typescript front end.

## Installation `gaming-global`
### Pre-requisites
- NodeJS 18.17.1
- Yarn

In the root of the `./gaming-global/` forlder install the packages using `yarn install`. 

To start the application run: `yarn start`, this will start up the node server running the application on `http://localhost:3000`

### **NOTE:** I am not using env vars for this project, so that api urls are hardcoded for this task in the services file: `./Gaming Global/gaming-global/src/Services/API/index.ts`. On line 68 we need to update the port number once the .Net Core web api is running.

## Installation `core-api`
### Pre-requisites
- Visual Studio 2022
- Docker Desktop
- Api uses .Net Core 6
- SSMS (SQL Server management Service)

First we need to create the MsSQL bd. Open PowerShell and run the following docker run command: `docker run -e "SA_PASSWORD=asdqweDEFQ435fqgf" -e "ACCEPT_EULA=Y" -p 1433:1433 --name TestAuthDb mcr.microsoft.com/mssql/server:latest`. Once the DB Container is up you can connect to it using SSMS. You can then import the Application Tier DB from the file called: `TestDb.dacpac`

Next once the solution `./Gaming Global/core-api/api-gaming_global/api-gaming_global.sln` for the web api is open you need to install the nuget packages.

The `Gaming Global\core-api\api-gaming_global\api-gaming_global\Properties\launchSettings.json` credentials was sent to Stewart in the form of a LastPass share link. If not received I will resend.


