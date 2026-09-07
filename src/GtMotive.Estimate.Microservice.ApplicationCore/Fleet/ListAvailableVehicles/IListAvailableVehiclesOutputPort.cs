using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Output port for the <see cref="IListAvailableVehiclesUseCase"/>.
    /// </summary>
    public interface IListAvailableVehiclesOutputPort : IOutputPortStandard<ListAvailableVehiclesOutput>
    {
    }
}
