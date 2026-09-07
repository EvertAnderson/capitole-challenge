using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Persistence port for the <see cref="Vehicle"/> aggregate.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the fleet.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);

        /// <summary>
        /// Persists the current state of an existing vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a vehicle by its identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The vehicle, or <see langword="null"/> when it does not exist.</returns>
        Task<Vehicle> GetByIdAsync(VehicleId vehicleId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets every vehicle in the fleet that is currently available to be rented.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The read-only collection of available vehicles.</returns>
        Task<IReadOnlyCollection<Vehicle>> GetAvailableAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Determines whether a license plate is already registered in the fleet.
        /// </summary>
        /// <param name="licensePlate">The license plate to check.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see langword="true"/> when a vehicle with the given license plate already exists.</returns>
        Task<bool> ExistsByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken);

        /// <summary>
        /// Determines whether a renter currently has an active rental on any vehicle of the fleet.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see langword="true"/> when the renter already has a vehicle rented.</returns>
        Task<bool> HasActiveRentalAsync(RenterId renterId, CancellationToken cancellationToken);
    }
}
