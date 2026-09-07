using System;
using System.Collections.Generic;
using System.IO;
using EphemeralMongo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public sealed class GenericInfrastructureTestServerFixture : IDisposable
    {
        private readonly IMongoRunner _mongoRunner;

        public GenericInfrastructureTestServerFixture()
        {
            _mongoRunner = MongoRunner.Run(new MongoRunnerOptions { UseSingleNodeReplicaSet = false });

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("IntegrationTest")
                .UseDefaultServiceProvider(options => { options.ValidateScopes = true; })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["MongoDb:ConnectionString"] = _mongoRunner.ConnectionString,
                        ["MongoDb:MongoDbDatabaseName"] = "FleetInfrastructureTests",
                    });
                    builder.AddEnvironmentVariables();
                })
                .UseStartup<Startup>();

            Server = new TestServer(hostBuilder);
        }

        public TestServer Server { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            Server?.Dispose();
            _mongoRunner?.Dispose();
        }
    }
}
