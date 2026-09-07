using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle
{
    /// <summary>
    /// Input message for the <see cref="IReturnVehicleUseCase"/>.
    /// </summary>
    /// <param name="VehicleId">The identifier of the vehicle to return.</param>
    public sealed record ReturnVehicleInput(string VehicleId) : IUseCaseInput;
}
