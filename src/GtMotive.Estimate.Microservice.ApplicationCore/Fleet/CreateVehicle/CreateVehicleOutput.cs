using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Fleet;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle
{
    /// <summary>
    /// Output message produced after successfully registering a vehicle in the fleet.
    /// </summary>
    /// <param name="VehicleId">The identifier assigned to the new vehicle.</param>
    /// <param name="Brand">The vehicle brand.</param>
    /// <param name="Model">The vehicle model.</param>
    /// <param name="LicensePlate">The vehicle license plate.</param>
    /// <param name="ManufactureDate">The vehicle manufacture date.</param>
    /// <param name="Status">The vehicle renting status.</param>
    public sealed record CreateVehicleOutput(
        VehicleId VehicleId,
        string Brand,
        string Model,
        string LicensePlate,
        DateOnly ManufactureDate,
        VehicleStatus Status) : IUseCaseOutput;
}
