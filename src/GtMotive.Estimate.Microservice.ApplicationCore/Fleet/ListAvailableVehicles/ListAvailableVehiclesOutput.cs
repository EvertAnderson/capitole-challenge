using System.Collections.Generic;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Output message for the <see cref="IListAvailableVehiclesUseCase"/>.
    /// </summary>
    /// <param name="Vehicles">The vehicles currently available to be rented.</param>
    public sealed record ListAvailableVehiclesOutput(IReadOnlyCollection<AvailableVehicle> Vehicles) : IUseCaseOutput;
}
