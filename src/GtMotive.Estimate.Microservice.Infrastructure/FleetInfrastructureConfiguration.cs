using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Fleet;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    /// <summary>
    /// Adds the Fleet bounded context infrastructure adapters.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class FleetInfrastructureConfiguration
    {
        /// <summary>
        /// Adds the MongoDB backed <see cref="IVehicleRepository"/> and the system clock to the
        /// <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddFleetInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MongoService>();
            services.AddSingleton<IClock, SystemClock>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();

            return services;
        }
    }
}
