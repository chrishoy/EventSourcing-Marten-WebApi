# Event Sourcing using Marten and PostgreSQL
This is a simple example of how to use Marten for event sourcing with projections.
- `docker-compose.yml`: This file contains the `PostgreSQL` configuration. Used by Marten to store document data.
- `src/EventSourcingMartenWebApi.csproj`: This is the Web Api project.

### How to run
1. Clone the repository
1. Modify `docker-compose.yml` to set the desired persistent volume location for `PostgreSQL` data
1. Run `docker-compose up -d` to start `PostgreSQL`
1. Run the project

### References
- [Marten](https://martendb.io/) - Transactional Document DB using PostgreSQL
- [Quick Start](https://martendb.io/events/quickstart.html) - Marten Quick Start
- [Event Sourcing](https://martinfowler.com/eaaDev/EventSourcing.html) - Article by Martin Fowler
