using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EphemeralMongo;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.Infrastructure;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    public sealed class CompositionRootTestFixture : IDisposable, IAsyncLifetime
    {
        private readonly IMongoRunner _mongoRunner;
        private readonly ServiceProvider _serviceProvider;

        public CompositionRootTestFixture()
        {
            _mongoRunner = MongoRunner.Run(new MongoRunnerOptions { UseSingleNodeReplicaSet = false });

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["MongoDb:ConnectionString"] = _mongoRunner.ConnectionString,
                    ["MongoDb:MongoDbDatabaseName"] = "FleetFunctionalTests",
                })
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            Configuration = configuration;
            ConfigureServices(services, configuration);
            services.AddSingleton<IConfiguration>(configuration);
            _serviceProvider = services.BuildServiceProvider();
        }

        public IConfiguration Configuration { get; }

        public async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            _mongoRunner.Dispose();
            await Task.CompletedTask;
        }

        public async Task UsingHandlerForRequest<TRequest>(Func<IRequestHandler<TRequest, Unit>, Task> handlerAction)
            where TRequest : IRequest
        {
            ArgumentNullException.ThrowIfNull(handlerAction);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, Unit>>();

            await handlerAction.Invoke(handler);
        }

        public async Task UsingHandlerForRequestResponse<TRequest, TResponse>(Func<IRequestHandler<TRequest, TResponse>, Task> handlerAction)
            where TRequest : IRequest<TResponse>
        {
            ArgumentNullException.ThrowIfNull(handlerAction);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            if (handler == null)
            {
                Debug.Fail("The requested handler has not been registered");
            }

            await handlerAction.Invoke(handler);
        }

        public async Task UsingRepository<TRepository>(Func<TRepository, Task> handlerAction)
        {
            ArgumentNullException.ThrowIfNull(handlerAction);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<TRepository>();

            if (handler == null)
            {
                Debug.Fail("The requested handler has not been registered");
            }

            await handlerAction.Invoke(handler);
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiDependencies();
            services.AddLogging();
            services.AddBaseInfrastructure(true);
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));
            services.AddFleetInfrastructure();
        }
    }
}
