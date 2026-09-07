using GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle;
using GtMotive.Estimate.Microservice.Api.Fleet.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateVehiclePresenter>();
            services.AddScoped<ICreateVehicleOutputPort>(sp => sp.GetRequiredService<CreateVehiclePresenter>());

            services.AddScoped<ListAvailableVehiclesPresenter>();
            services.AddScoped<IListAvailableVehiclesOutputPort>(sp => sp.GetRequiredService<ListAvailableVehiclesPresenter>());

            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<IRentVehicleOutputPort>(sp => sp.GetRequiredService<RentVehiclePresenter>());

            services.AddScoped<ReturnVehiclePresenter>();
            services.AddScoped<IReturnVehicleOutputPort>(sp => sp.GetRequiredService<ReturnVehiclePresenter>());

            return services;
        }
    }
}
