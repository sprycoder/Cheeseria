# Cheeseria 
This is an application for Cheeseria, with following features:

* .NET 5 Web API for CRUD operations for cheeses
* API documentation through Swagger
* Unit Tests
* React front end
* Docker for each client and server code
* Docker compose file to make using project easy.

## Prerequisites
To build and run project the only dependancies required are:

* Git
* Docker
* Docker Compose (installed as part of Docker for Windows on linux it is a seperate install)

## How To build
```bash
docker-compose build
```

## Usage
```bash
docker-compose up
```

If you want to run it without docker, you can do through command line by
API - in Cheeseria.API directory run command "dotnet run"
Client - in Client directory run command "npm start"

Application would be available as below:

Client: http://localhost:3000/ 

API : http://localhost:5001/swagger
