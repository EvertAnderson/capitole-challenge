using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// View model returned when listing the vehicles available to be rented.
    /// </summary>
    public sealed class ListAvailableVehiclesResponse
    {
        public ListAvailableVehiclesResponse(IReadOnlyCollection<AvailableVehicleResponse> vehicles)
        {
            Vehicles = vehicles;
        }

        /// <summary>
        /// Gets the vehicles currently available to be rented.
        /// </summary>
        [Required]
        public IReadOnlyCollection<AvailableVehicleResponse> Vehicles { get; }
    }
}
