using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle
{
    /// <summary>
    /// Output port for the <see cref="IReturnVehicleUseCase"/>.
    /// </summary>
    public interface IReturnVehicleOutputPort : IOutputPortStandard<ReturnVehicleOutput>, IOutputPortNotFound
    {
        /// <summary>
        /// Informs that the vehicle is not currently rented.
        /// </summary>
        /// <param name="message">Text description.</param>
        void VehicleNotRentedHandle(string message);
    }
}
