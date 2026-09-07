using System;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Aggregate root representing a vehicle that belongs to the rental fleet.
    /// </summary>
    public sealed class Vehicle
    {
        /// <summary>
        /// The maximum age, in years, a vehicle can have to be part of the fleet.
        /// </summary>
        public const int MaxFleetVehicleAgeInYears = 5;

        private Vehicle(
            VehicleId id,
            string brand,
            string model,
            LicensePlate licensePlate,
            DateOnly manufactureDate,
            VehicleRentalState rentalState)
        {
            Id = id;
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Status = rentalState.Status;
            CurrentRenterId = rentalState.CurrentRenterId;
            RentedAtUtc = rentalState.RentedAtUtc;
            LastReturnedAtUtc = rentalState.LastReturnedAtUtc;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public VehicleId Id { get; }

        /// <summary>
        /// Gets the vehicle brand.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        public LicensePlate LicensePlate { get; }

        /// <summary>
        /// Gets the vehicle manufacture date.
        /// </summary>
        public DateOnly ManufactureDate { get; }

        /// <summary>
        /// Gets the current renting status of the vehicle.
        /// </summary>
        public VehicleStatus Status { get; private set; }

        /// <summary>
        /// Gets the identifier of the renter currently renting the vehicle, if any.
        /// </summary>
        public RenterId? CurrentRenterId { get; private set; }

        /// <summary>
        /// Gets the date and time, in UTC, the current rental started, if any.
        /// </summary>
        public DateTime? RentedAtUtc { get; private set; }

        /// <summary>
        /// Gets the date and time, in UTC, the vehicle was last returned, if any.
        /// </summary>
        public DateTime? LastReturnedAtUtc { get; private set; }

        /// <summary>
        /// Registers a new vehicle in the fleet.
        /// </summary>
        /// <param name="id">The new vehicle identifier.</param>
        /// <param name="brand">The vehicle brand.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="licensePlate">The vehicle license plate.</param>
        /// <param name="manufactureDate">The vehicle manufacture date.</param>
        /// <param name="today">The current date, used to validate the maximum fleet vehicle age.</param>
        /// <returns>A new, available <see cref="Vehicle"/>.</returns>
        /// <exception cref="VehicleTooOldException">
        /// Thrown when <paramref name="manufactureDate"/> is older than <see cref="MaxFleetVehicleAgeInYears"/> years.
        /// </exception>
        public static Vehicle Register(
            VehicleId id,
            string brand,
            string model,
            LicensePlate licensePlate,
            DateOnly manufactureDate,
            DateOnly today)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(brand);
            ArgumentException.ThrowIfNullOrWhiteSpace(model);

            if (manufactureDate > today)
            {
                throw new ArgumentOutOfRangeException(nameof(manufactureDate), manufactureDate, "The manufacture date cannot be in the future.");
            }

            var oldestAllowedManufactureDate = today.AddYears(-MaxFleetVehicleAgeInYears);

            if (manufactureDate < oldestAllowedManufactureDate)
            {
                throw new VehicleTooOldException(
                    $"The vehicle manufacture date {manufactureDate:yyyy-MM-dd} exceeds the maximum fleet age of {MaxFleetVehicleAgeInYears} years.");
            }

            var initialState = new VehicleRentalState(VehicleStatus.Available, null, null, null);

            return new Vehicle(id, brand, model, licensePlate, manufactureDate, initialState);
        }

        /// <summary>
        /// Rents the vehicle to the given renter.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <param name="rentedAtUtc">The date and time, in UTC, the rental starts.</param>
        /// <exception cref="VehicleNotAvailableException">Thrown when the vehicle is already rented.</exception>
        public void Rent(RenterId renterId, DateTime rentedAtUtc)
        {
            if (Status == VehicleStatus.Rented)
            {
                throw new VehicleNotAvailableException($"The vehicle {Id} is already rented and cannot be rented again.");
            }

            Status = VehicleStatus.Rented;
            CurrentRenterId = renterId;
            RentedAtUtc = rentedAtUtc;
        }

        /// <summary>
        /// Returns the vehicle, making it available again.
        /// </summary>
        /// <param name="returnedAtUtc">The date and time, in UTC, the vehicle is returned.</param>
        /// <exception cref="VehicleNotRentedException">Thrown when the vehicle is not currently rented.</exception>
        public void Return(DateTime returnedAtUtc)
        {
            if (Status == VehicleStatus.Available)
            {
                throw new VehicleNotRentedException($"The vehicle {Id} is not currently rented and cannot be returned.");
            }

            Status = VehicleStatus.Available;
            CurrentRenterId = null;
            RentedAtUtc = null;
            LastReturnedAtUtc = returnedAtUtc;
        }

        /// <summary>
        /// Reconstructs a vehicle from previously persisted state, bypassing creation invariants.
        /// Intended to be used exclusively by infrastructure persistence adapters.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <param name="brand">The vehicle brand.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="licensePlate">The vehicle license plate.</param>
        /// <param name="manufactureDate">The vehicle manufacture date.</param>
        /// <param name="rentalState">The persisted rental state.</param>
        /// <returns>The reconstructed <see cref="Vehicle"/>.</returns>
        internal static Vehicle Restore(
            VehicleId id,
            string brand,
            string model,
            LicensePlate licensePlate,
            DateOnly manufactureDate,
            VehicleRentalState rentalState)
        {
            return new Vehicle(id, brand, model, licensePlate, manufactureDate, rentalState);
        }
    }
}
