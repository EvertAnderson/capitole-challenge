using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle
{
    /// <summary>
    /// Input message for the <see cref="ICreateVehicleUseCase"/>.
    /// </summary>
    /// <param name="Brand">The vehicle brand.</param>
    /// <param name="Model">The vehicle model.</param>
    /// <param name="LicensePlate">The vehicle license plate.</param>
    /// <param name="ManufactureDate">The vehicle manufacture date.</param>
    public sealed record CreateVehicleInput(string Brand, string Model, string LicensePlate, DateOnly ManufactureDate) : IUseCaseInput;
}
