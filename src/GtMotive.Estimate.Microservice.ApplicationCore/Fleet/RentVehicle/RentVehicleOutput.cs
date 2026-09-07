using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Fleet;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle
{
    /// <summary>
    /// Output message produced after successfully renting a vehicle.
    /// </summary>
    /// <param name="VehicleId">The identifier of the rented vehicle.</param>
    /// <param name="RenterId">The identifier of the renter.</param>
    /// <param name="RentedAtUtc">The date and time, in UTC, the rental started.</param>
    public sealed record RentVehicleOutput(VehicleId VehicleId, string RenterId, DateTime RentedAtUtc) : IUseCaseOutput;
}
