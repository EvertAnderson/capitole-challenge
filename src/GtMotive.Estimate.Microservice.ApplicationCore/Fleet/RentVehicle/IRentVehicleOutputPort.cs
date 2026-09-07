using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle
{
    /// <summary>
    /// Output port for the <see cref="IRentVehicleUseCase"/>.
    /// </summary>
    public interface IRentVehicleOutputPort : IOutputPortStandard<RentVehicleOutput>, IOutputPortNotFound
    {
        /// <summary>
        /// Informs that the vehicle is already rented.
        /// </summary>
        /// <param name="message">Text description.</param>
        void VehicleNotAvailableHandle(string message);

        /// <summary>
        /// Informs that the renter already has another active rental.
        /// </summary>
        /// <param name="message">Text description.</param>
        void RenterAlreadyHasActiveRentalHandle(string message);
    }
}
