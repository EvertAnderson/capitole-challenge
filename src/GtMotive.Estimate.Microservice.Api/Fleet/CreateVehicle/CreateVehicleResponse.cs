using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle;

namespace GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle
{
    /// <summary>
    /// View model returned after successfully registering a vehicle in the fleet.
    /// </summary>
    public sealed class CreateVehicleResponse
    {
        public CreateVehicleResponse(CreateVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            VehicleId = output.VehicleId.Value;
            Brand = output.Brand;
            Model = output.Model;
            LicensePlate = output.LicensePlate;
            ManufactureDate = output.ManufactureDate;
            Status = output.Status.ToString();
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the vehicle brand.
        /// </summary>
        [Required]
        public string Brand { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        [Required]
        public string Model { get; }

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        [Required]
        public string LicensePlate { get; }

        /// <summary>
        /// Gets the vehicle manufacture date.
        /// </summary>
        [Required]
        public DateOnly ManufactureDate { get; }

        /// <summary>
        /// Gets the vehicle renting status.
        /// </summary>
        [Required]
        public string Status { get; }
    }
}
