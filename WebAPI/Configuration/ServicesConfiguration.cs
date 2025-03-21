namespace WebAPI.Configuration;

public record ServicesConfiguration
(
    MongoDBConfiguration MongoDB,
    PostgreSQLConfiguration PostgreSQL
);