using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Fleet;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle
{
    /// <summary>
    /// Output message produced after successfully returning a vehicle.
    /// </summary>
    /// <param name="VehicleId">The identifier of the returned vehicle.</param>
    /// <param name="ReturnedAtUtc">The date and time, in UTC, the vehicle was returned.</param>
    public sealed record ReturnVehicleOutput(VehicleId VehicleId, DateTime ReturnedAtUtc) : IUseCaseOutput;
}
