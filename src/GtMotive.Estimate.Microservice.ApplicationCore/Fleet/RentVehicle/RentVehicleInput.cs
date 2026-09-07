using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle
{
    /// <summary>
    /// Input message for the <see cref="IRentVehicleUseCase"/>.
    /// </summary>
    /// <param name="VehicleId">The identifier of the vehicle to rent.</param>
    /// <param name="RenterId">The identifier of the renter.</param>
    public sealed record RentVehicleInput(string VehicleId, string RenterId) : IUseCaseInput;
}
