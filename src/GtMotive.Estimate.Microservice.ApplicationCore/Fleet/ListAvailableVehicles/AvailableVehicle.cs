using System;
using GtMotive.Estimate.Microservice.Domain.Fleet;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Summary of a vehicle that is currently available to be rented.
    /// </summary>
    /// <param name="VehicleId">The vehicle identifier.</param>
    /// <param name="Brand">The vehicle brand.</param>
    /// <param name="Model">The vehicle model.</param>
    /// <param name="LicensePlate">The vehicle license plate.</param>
    /// <param name="ManufactureDate">The vehicle manufacture date.</param>
    public sealed record AvailableVehicle(VehicleId VehicleId, string Brand, string Model, string LicensePlate, DateOnly ManufactureDate);
}
