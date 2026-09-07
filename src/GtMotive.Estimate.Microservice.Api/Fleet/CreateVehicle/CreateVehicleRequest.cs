using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle
{
    /// <summary>
    /// HTTP request to register a new vehicle in the fleet.
    /// </summary>
    public sealed class CreateVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets or sets the vehicle brand.
        /// </summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the vehicle model.
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the vehicle license plate.
        /// </summary>
        [Required]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the vehicle manufacture date.
        /// </summary>
        [Required]
        required public DateOnly ManufactureDate { get; set; }
    }
}
