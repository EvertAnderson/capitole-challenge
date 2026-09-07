using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ReturnVehicle
{
    /// <summary>
    /// View model returned after successfully returning a vehicle.
    /// </summary>
    public sealed class ReturnVehicleResponse
    {
        public ReturnVehicleResponse(ReturnVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            VehicleId = output.VehicleId.Value;
            ReturnedAtUtc = output.ReturnedAtUtc;
        }

        /// <summary>
        /// Gets the identifier of the returned vehicle.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the date and time, in UTC, the vehicle was returned.
        /// </summary>
        [Required]
        public DateTime ReturnedAtUtc { get; }
    }
}
