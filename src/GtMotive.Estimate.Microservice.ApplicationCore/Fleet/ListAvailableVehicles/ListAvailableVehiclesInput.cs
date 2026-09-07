using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Input message for the <see cref="IListAvailableVehiclesUseCase"/>. Carries no data.
    /// </summary>
    public sealed record ListAvailableVehiclesInput : IUseCaseInput;
}
