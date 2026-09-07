using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle
{
    /// <summary>
    /// Output port for the <see cref="ICreateVehicleUseCase"/>.
    /// </summary>
    public interface ICreateVehicleOutputPort : IOutputPortStandard<CreateVehicleOutput>
    {
        /// <summary>
        /// Informs that a vehicle with the given license plate is already registered in the fleet.
        /// </summary>
        /// <param name="message">Text description.</param>
        void DuplicateLicensePlateHandle(string message);
    }
}
