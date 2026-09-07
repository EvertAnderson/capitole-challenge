using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.ApplicationCore
{
    /// <summary>
    /// Adds Use Cases classes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<FleetRentalService>();

            services.AddScoped<ICreateVehicleUseCase, CreateVehicleUseCase>();
            services.AddScoped<IListAvailableVehiclesUseCase, ListAvailableVehiclesUseCase>();
            services.AddScoped<IRentVehicleUseCase, RentVehicleUseCase>();
            services.AddScoped<IReturnVehicleUseCase, ReturnVehicleUseCase>();

            return services;
        }
    }
}
