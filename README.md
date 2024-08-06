# EventHandler
This is a simple console app that illlustrates handling of different types of messages from a kafka broker in a modular and extensible manner

## Pre-requisites
1. Install [.NET 8](https://dotnet.microsoft.com/en-us/download)
2. Install [Docker](https://docs.docker.com/engine/install/)

## Running the app
1. Navigate to the root directory in command window
2. `cd EventHandlerService`
3. `docker-compose up -d`
4. `docker ps`
5. Ensure the kafka and zookeeper services are running
6. `dotnet build`
7. `dotnet run --project EventHandler/EventHandler.csproj`