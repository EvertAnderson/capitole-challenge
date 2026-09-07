using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// View model for a single vehicle available to be rented.
    /// </summary>
    public sealed class AvailableVehicleResponse
    {
        public AvailableVehicleResponse(AvailableVehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            VehicleId = vehicle.VehicleId.Value;
            Brand = vehicle.Brand;
            Model = vehicle.Model;
            LicensePlate = vehicle.LicensePlate;
            ManufactureDate = vehicle.ManufactureDate;
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
    }
}
