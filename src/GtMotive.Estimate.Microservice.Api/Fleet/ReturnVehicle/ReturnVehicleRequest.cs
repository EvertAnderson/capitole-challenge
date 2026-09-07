using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ReturnVehicle
{
    /// <summary>
    /// HTTP request to return a rented vehicle.
    /// </summary>
    /// <param name="VehicleId">The identifier of the vehicle to return, taken from the route.</param>
    public sealed record ReturnVehicleRequest(string VehicleId) : IRequest<IWebApiPresenter>;
}
