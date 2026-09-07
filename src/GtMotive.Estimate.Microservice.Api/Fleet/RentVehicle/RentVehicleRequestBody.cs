using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle
{
    /// <summary>
    /// HTTP request body to rent a vehicle.
    /// </summary>
    public sealed class RentVehicleRequestBody
    {
        /// <summary>
        /// Gets or sets the identifier of the renter.
        /// </summary>
        [Required]
        public string RenterId { get; set; }
    }
}
