using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle;

namespace GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle
{
    /// <summary>
    /// View model returned after successfully renting a vehicle.
    /// </summary>
    public sealed class RentVehicleResponse
    {
        public RentVehicleResponse(RentVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            VehicleId = output.VehicleId.Value;
            RenterId = output.RenterId;
            RentedAtUtc = output.RentedAtUtc;
        }

        /// <summary>
        /// Gets the identifier of the rented vehicle.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the identifier of the renter.
        /// </summary>
        [Required]
        public string RenterId { get; }

        /// <summary>
        /// Gets the date and time, in UTC, the rental started.
        /// </summary>
        [Required]
        public DateTime RentedAtUtc { get; }
    }
}
