using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Use case that lists every vehicle currently available to be rented.
    /// </summary>
    public interface IListAvailableVehiclesUseCase : IUseCase<ListAvailableVehiclesInput>
    {
    }
}
