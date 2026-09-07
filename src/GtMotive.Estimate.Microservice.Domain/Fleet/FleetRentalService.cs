using System;
using System.Threading;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Domain service that coordinates renting and returning vehicles, enforcing the fleet-wide
    /// rule that a renter cannot have more than one active rental at the same time.
    /// </summary>
    public class FleetRentalService(IVehicleRepository vehicleRepository)
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));

        /// <summary>
        /// Rents a vehicle to a renter, after checking that the vehicle exists, is available,
        /// and that the renter does not already have another active rental.
        /// </summary>
        /// <param name="vehicleId">The identifier of the vehicle to rent.</param>
        /// <param name="renterId">The identifier of the renter.</param>
        /// <param name="rentedAtUtc">The date and time, in UTC, the rental starts.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The rented vehicle.</returns>
        /// <exception cref="VehicleNotFoundException">Thrown when the vehicle does not exist.</exception>
        /// <exception cref="RenterAlreadyHasActiveRentalException">
        /// Thrown when the renter already has a vehicle rented.
        /// </exception>
        /// <exception cref="VehicleNotAvailableException">Thrown when the vehicle is already rented.</exception>
        public async Task<Vehicle> RentAsync(VehicleId vehicleId, RenterId renterId, DateTime rentedAtUtc, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, cancellationToken).ConfigureAwait(false) ??
                throw new VehicleNotFoundException($"The vehicle {vehicleId} does not exist in the fleet.");

            var renterHasActiveRental = await _vehicleRepository.HasActiveRentalAsync(renterId, cancellationToken).ConfigureAwait(false);

            if (renterHasActiveRental)
            {
                throw new RenterAlreadyHasActiveRentalException($"The renter {renterId} already has an active vehicle rental.");
            }

            vehicle.Rent(renterId, rentedAtUtc);

            await _vehicleRepository.UpdateAsync(vehicle, cancellationToken).ConfigureAwait(false);

            return vehicle;
        }

        /// <summary>
        /// Returns a rented vehicle, making it available again.
        /// </summary>
        /// <param name="vehicleId">The identifier of the vehicle to return.</param>
        /// <param name="returnedAtUtc">The date and time, in UTC, the vehicle is returned.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The returned vehicle.</returns>
        /// <exception cref="VehicleNotFoundException">Thrown when the vehicle does not exist.</exception>
        /// <exception cref="VehicleNotRentedException">Thrown when the vehicle is not currently rented.</exception>
        public async Task<Vehicle> ReturnAsync(VehicleId vehicleId, DateTime returnedAtUtc, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, cancellationToken).ConfigureAwait(false) ??
                throw new VehicleNotFoundException($"The vehicle {vehicleId} does not exist in the fleet.");

            vehicle.Return(returnedAtUtc);

            await _vehicleRepository.UpdateAsync(vehicle, cancellationToken).ConfigureAwait(false);

            return vehicle;
        }
    }
}
