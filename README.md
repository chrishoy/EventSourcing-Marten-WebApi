## Event Sourcing using Marten and PostgreSQL
Simple example of how to use Marten for event sourcing with projections.

## Event Sourcing using Marten and PostgreSQL
This is a simple example of how to use Marten for event sourcing with projections.
- docker-compose.yml: This file contains the PostgreSQL configuration.
- src/EventSourcingMartenWebApi.csproj: This is the Web Api project.

### How to run
1. Clone the repository
1. Modify docker-compose.yml to set the desired persistent volume location for PostgreSQL data
1. Run `docker-compose up -d` to start PostgreSQL
1. Run the project
