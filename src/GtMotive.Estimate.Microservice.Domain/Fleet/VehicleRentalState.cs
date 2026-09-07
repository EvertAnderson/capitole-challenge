using System;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Groups the mutable rental state of a <see cref="Vehicle"/> so it can be passed around
    /// without exceeding parameter count conventions. Intended to be used exclusively by
    /// infrastructure persistence adapters when reconstructing a <see cref="Vehicle"/>.
    /// </summary>
    internal readonly struct VehicleRentalState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRentalState"/> struct.
        /// </summary>
        /// <param name="status">The renting status.</param>
        /// <param name="currentRenterId">The current renter identifier, if any.</param>
        /// <param name="rentedAtUtc">The current rental start date and time, in UTC, if any.</param>
        /// <param name="lastReturnedAtUtc">The last return date and time, in UTC, if any.</param>
        public VehicleRentalState(VehicleStatus status, RenterId? currentRenterId, DateTime? rentedAtUtc, DateTime? lastReturnedAtUtc)
        {
            Status = status;
            CurrentRenterId = currentRenterId;
            RentedAtUtc = rentedAtUtc;
            LastReturnedAtUtc = lastReturnedAtUtc;
        }

        /// <summary>
        /// Gets the renting status.
        /// </summary>
        public VehicleStatus Status { get; }

        /// <summary>
        /// Gets the current renter identifier, if any.
        /// </summary>
        public RenterId? CurrentRenterId { get; }

        /// <summary>
        /// Gets the current rental start date and time, in UTC, if any.
        /// </summary>
        public DateTime? RentedAtUtc { get; }

        /// <summary>
        /// Gets the last return date and time, in UTC, if any.
        /// </summary>
        public DateTime? LastReturnedAtUtc { get; }
    }
}
