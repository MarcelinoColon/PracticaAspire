using Projects;

var builder = DistributedApplication.CreateBuilder(args);

//var apiService = builder.AddProject<Projects.PracticaAspire_ApiService>("apiservice")
//    .WithHttpHealthCheck("/health");

//builder.AddProject<Projects.PracticaAspire_Web>("webfrontend")
//    .WithExternalHttpEndpoints()
//    .WithHttpHealthCheck("/health")
//    .WithReference(apiService)
//    .WaitFor(apiService);

var sqlServer = builder.AddSqlServer("sql-server")
                       .AddDatabase("AgenciesDb");

var mongoDb = builder.AddMongoDB("mongo-server")
                     .AddDatabase("CatalogDb");

var agencyApi = builder.AddProject<Agency_Api>("agency-api")
    .WithReference(sqlServer)
    .WaitFor(sqlServer);

var catalogApi = builder.AddProject<Catalog_Api>("catalog-api")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

var graphqlBff = builder.AddProject<GraphQl_Bff>("graphql-bff")
    .WithReference(agencyApi)
    .WithReference(catalogApi);

builder.AddNpmApp("react-client", "../react-client", "dev")
       .WithReference(graphqlBff)
       .WithEnvironment("VITE_GRAPHQL_URL", graphqlBff.GetEndpoint("https"))
       .WithHttpEndpoint(env: "PORT")
       .WithExternalHttpEndpoints();


builder.Build().Run();
