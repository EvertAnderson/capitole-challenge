using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle
{
    /// <summary>
    /// HTTP request to rent a vehicle.
    /// </summary>
    /// <param name="VehicleId">The identifier of the vehicle to rent, taken from the route.</param>
    /// <param name="RenterId">The identifier of the renter, taken from the request body.</param>
    public sealed record RentVehicleRequest(string VehicleId, string RenterId) : IRequest<IWebApiPresenter>;
}
